using cricarba.RememberMe.bot.Domain;
using cricarba.RememberMe.bot.Interfaces;
using System;

namespace cricarba.RememberMe.bot.Implementations.ReminderStrategy
{
    public class DaysReminder : IReminderStrategy
    {
        public Reminder GetReminder(string message)
        {
            var remembermeIndex = message.ToLower().IndexOf("recuerdame");
            var shortRemembermeIndex = message.ToLower().IndexOf("-r");
            var inIndex = message.ToLower().IndexOf("en");
            var daysIndex = message.ToLower().IndexOf("días");
            daysIndex = daysIndex == -1 ? message.ToLower().IndexOf("dias") : daysIndex;
            if ((remembermeIndex >= 0 || shortRemembermeIndex >= 0) && daysIndex > 0)
            {
                var isShort = remembermeIndex == -1 && shortRemembermeIndex >= 0;
                var rememberme = isShort ? message.Substring(shortRemembermeIndex, inIndex).ToLower().Replace("-r", string.Empty).Trim() :
                                           message.Substring(remembermeIndex, inIndex).ToLower().Replace("recuerdame", string.Empty).Trim();

                var remeberDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                var endInIndex = inIndex + 2;
                var lenght = daysIndex - endInIndex;
                var days = message.Substring(endInIndex, lenght).ToLower().Trim();
                int.TryParse(days, out int addDay);
                remeberDate = remeberDate.AddDays(addDay);


                if (remeberDate <= DateTime.Now)
                {
                    return new Reminder(string.Empty, string.Empty, remeberDate, true);
                }
                else
                {
                    var reminderMessage = $"Me dijiste 👉 *{rememberme.ToLower()}* para hoy *{remeberDate.ToShortDateString()}*   \n\n Espero no lo hayas olvidado! 😉";


                    var responseMessage = $"Te recordare \n 📖 {rememberme.ToLower().Replace("recuerdame", string.Empty)} \n 📅 en *{days}* dias";


                    return new Reminder(reminderMessage, responseMessage, remeberDate, false);
                }
            }
            return new Reminder(string.Empty, string.Empty, new DateTime(), true);
        }
    }
}
