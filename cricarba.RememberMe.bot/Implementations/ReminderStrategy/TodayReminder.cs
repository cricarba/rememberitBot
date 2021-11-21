using cricarba.RememberMe.bot.Domain;
using cricarba.RememberMe.bot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cricarba.RememberMe.bot.Implementations.ReminderStrategy
{
    public class TodayReminder : IReminderStrategy
    {
        public Reminder GetReminder(string message)
        {

            var remembermeIndex = message.ToLower().IndexOf("recuerdame");
            var shortRemembermeIndex = message.ToLower().IndexOf("-r");
            var hourIndex = message.ToLower().IndexOf("a las");
            if ((remembermeIndex >= 0 || shortRemembermeIndex >= 0) && hourIndex > 0)
            {
                var isShort = remembermeIndex == -1 && shortRemembermeIndex >= 0;
                var rememberme = isShort ? message.Substring(shortRemembermeIndex, hourIndex).ToLower().Replace("-r", string.Empty).Trim() :
                                           message.Substring(remembermeIndex, hourIndex).ToLower().Replace("recuerdame", string.Empty).Trim();
                var hour = message.Substring(hourIndex).ToLower().Replace("a las", string.Empty).Trim();
                var hoursMinuts = hour.Split(":");
                var hours = hoursMinuts[0];
                var minuts = hoursMinuts[1];
                double.TryParse(hours, out double addHours);
                double.TryParse(minuts, out double addMinuts);

                DateTime remeberDate = DateTime.Now;
                remeberDate = remeberDate.AddHours(addHours);
                remeberDate = remeberDate.AddMinutes(addMinuts);

                if (remeberDate <= DateTime.Now)
                {
                    return new Reminder(string.Empty, string.Empty, remeberDate, true);
                }
                else
                {
                    var reminderMessage = $"Me dijiste 👉 *{rememberme.ToLower()}* para *hoy* a las *{hour.ToLower()}* \n\n Espero no lo hayas olvidado! 😉";
                    var responseMessage = $"Te recordare \n 📖 {rememberme.ToLower().Replace("recuerdame", string.Empty)} \n 📅 *Hoy* \n 🕕 *{hour.ToLower()}*";
                    return new Reminder(reminderMessage, responseMessage, remeberDate, false);
                }
            }
            return new Reminder(string.Empty, string.Empty, new DateTime(), true);

        }
    }
}