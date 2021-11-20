using cricarba.RememberMe.bot.Domain;
using cricarba.RememberMe.bot.Interfaces;
using System;

namespace cricarba.RememberMe.bot.Implementations.ReminderStrategy
{
    class TomorrowReminder : IReminderStrategy
    {
        public Reminder GetReminder(string message)
        {
            var remembermeIndex = message.ToLower().IndexOf("recuerdame");
            var shortRemembermeIndex = message.ToLower().IndexOf("-r");
            var tomorrowIndex = message.ToLower().IndexOf("mañana");
            bool hasHour = false;
            if ((remembermeIndex >= 0 || shortRemembermeIndex >= 0) && tomorrowIndex > 0)
            {
                var isShort = remembermeIndex == -1 && shortRemembermeIndex >= 0;
                var rememberme = isShort ? message.Substring(shortRemembermeIndex, tomorrowIndex).ToLower().Replace("-r", string.Empty).Trim() :
                                           message.Substring(remembermeIndex, tomorrowIndex).ToLower().Replace("recuerdame", string.Empty).Trim();
                var hourIndex = isShort ? message.ToLower().IndexOf("-h") : message.ToLower().IndexOf("a las");
                var remeberDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                var hour = string.Empty;
                if (hourIndex > 0)
                {
                    hour = isShort ? message.Substring(hourIndex).ToLower().Replace("-h", string.Empty).Trim() :
                                     message.Substring(hourIndex).ToLower().Replace("a las", string.Empty).Trim();
                    var hoursMinuts = hour.Split(":");
                    var hours = hoursMinuts[0];
                    var minuts = hoursMinuts[1];
                    double.TryParse(hours, out double addHours);
                    double.TryParse(minuts, out double addMinuts);
                    remeberDate = remeberDate.AddHours(addHours);
                    remeberDate = remeberDate.AddMinutes(addMinuts);
                    hasHour = true;
                }
                else
                {
                    remeberDate = DateTime.Now;
                }

                remeberDate = remeberDate.AddDays(1);

                if (remeberDate <= DateTime.Now)
                {
                    return new Reminder(string.Empty, string.Empty, remeberDate, true);
                }
                else
                {
                    var reminderMessage = hasHour ? $"Me dijiste 👉 *{rememberme.ToLower()}* para hoy *{remeberDate.ToShortDateString()}* a las *{hour.ToLower()}* \n\n Espero no lo hayas olvidado! 😉" :
                                                    $"Me dijiste 👉 *{rememberme.ToLower()}* para hoy *{remeberDate.ToShortDateString()}* * \n\n Espero no lo hayas olvidado! 😉";

                    var responseMessage = hasHour ? $"Te recordare \n 📖 {rememberme.ToLower().Replace("recuerdame", string.Empty)} \n 📅 *Mañana* \n 🕕 *{hour.ToLower()}*" :
                                                    $"Te recordare \n 📖 {rememberme.ToLower().Replace("recuerdame", string.Empty)} \n 📅 *Mañana* \n ";

                    return new Reminder(reminderMessage, responseMessage, remeberDate, false);
                }
            }
            return new Reminder(string.Empty, string.Empty, new DateTime(), true);
        }
    }
}
