using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using Telegram.Bot.Extensions.Polling;
using Hangfire;
using cricarba.RememberMe.bot.Domain;
using System.Collections.Generic;
using cricarba.RememberMe.bot.Interfaces;
using cricarba.RememberMe.bot.Implementations.Validates;
using cricarba.RememberMe.bot.Implementations;
using System.Text;

namespace cricarba.RememberMe.bot
{
    class Program
    {
        static ITelegramBotClient _botClient;
        static string _token =  Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");
        static string _cnn = Environment.GetEnvironmentVariable("scheduleCnnStr");
        static void Main(string[] args)
        {
            Task.Run(() => RunSchedule());
            _botClient = new TelegramBotClient(_token);
            var me = _botClient.GetMeAsync();
            using var cts = new CancellationTokenSource();
            _botClient.StartReceiving(new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync), cts.Token);
            Console.WriteLine($"Start listening for @{me.Result.Username}");
            Console.ReadLine();
        }

        static void RunSchedule()
        {
         
            GlobalConfiguration.Configuration
                  .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                  .UseColouredConsoleLogProvider()
                  .UseSimpleAssemblyNameTypeSerializer().UseSqlServerStorage(_cnn);

            using (var server = new BackgroundJobServer())
            {
                Console.Read();
            }
        }

        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
                return;
            if (update.Message.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;
            Console.WriteLine($"Received a '{update.Message.Text}' message in chat {chatId}.");

            var message = update.Message.Text;
            ReminderValidate validateMessage = new ReminderValidateRule().Validate(message);
            if (validateMessage.IsValid)
            {

                Reminder reminder = new ReminderFactory().GetReminder(validateMessage.ReminderType, message);

                if (reminder.IsPast)
                {
                    await botClient.SendPhotoAsync(
                      chatId: chatId,
                      photo: "https://ruizhealytimes.com/wp-content/uploads/2015/10/doc-brown.jpg",
                      parseMode: ParseMode.Markdown,
                      caption: $"*{reminder.ReminderDate.ToShortDateString()}* es una fecha en el pasado, no creo que el Doc este disponible");
                }
                else
                {
                    double totalMinutes = reminder.ReminderDate.Subtract(DateTime.Now).TotalMinutes;
                    BackgroundJob.Schedule(() => SendMessage(reminder.ReminderText, chatId), TimeSpan.FromMinutes(totalMinutes));


                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        parseMode: ParseMode.Markdown,
                        text: reminder.ResponseChatMessage);

                    Console.WriteLine($"Received a '{update.Message.Text}' message in chat {chatId}. Response {reminder.ResponseChatMessage}");
                }
            }
            else
            {
                StringBuilder messageError = new StringBuilder();
                messageError.Append("\n\n Para ayudarte a recordar puedes hacerlo de diferentes formas ");
                messageError.Append("\n\n 🐢 La forma larga 🐢 ");
                messageError.Append("\n\n *Recuerdame* lo que quieres que te recuerde *el día* dd/mm/aaaa *a las* hh:mm");
                messageError.Append("\n\n 👉 *Recuerdame* ir a cine *el día* 24/09/2021 *a las* 18:00");
                messageError.Append("\n\n 🚀 La forma corta 🚀");
                messageError.Append("\n\n *-R* lo que quieres que te recuerde *-D* dd/mm/aaaa *-H* hh:mm");
                messageError.Append("\n\n 👉 *-R* comprar leche *-D* 24/09/2021 *-H* 18:00");
                messageError.Append("\n\n Puedes hacerlo de diferentes formas");
                messageError.Append("\n\n 🐢 *Recuerdame* Ir al cine *mañana*");
                messageError.Append("\n\n 🚀 *-R* Ir al cine *mañana*");
                messageError.Append("\n\n 🐢 *Recuerdame* Ir al cine *mañana a las* 18:00");
                messageError.Append("\n\n 🚀 *-R* ir al cine *mañana -H* 18:00");



                await botClient.SendTextMessageAsync(
                     chatId: chatId,
                     parseMode: ParseMode.Markdown,
                     text: messageError.ToString()
                ); ;
            }
        }

        public static void SendMessage(string reminderMessage, ChatId chatId)
        {
            var messaje = _botClient.SendTextMessageAsync(chatId: chatId,parseMode: ParseMode.Markdown, text: reminderMessage);
            Console.WriteLine($"Send a '{reminderMessage}' message in chat {chatId}. {messaje.Result.MessageId}");
        }
        static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

       
    }
}


