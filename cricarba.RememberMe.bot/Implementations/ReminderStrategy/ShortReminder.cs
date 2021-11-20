using cricarba.RememberMe.bot.Domain;
using cricarba.RememberMe.bot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace cricarba.RememberMe.bot.Implementations
{
    class ShortReminder : IReminderStrategy
    {
        public Reminder GetReminder(string message)
        {

            var remembermeIndex = message.ToLower().IndexOf("-r");
            var dateIndex = message.ToLower().IndexOf("-d");
            var hourIndex = message.ToLower().IndexOf("-h");
            if (remembermeIndex >= 0 && dateIndex > 0 && hourIndex > 0)
            {
                var rememberme = message.Substring(remembermeIndex, dateIndex).ToLower().Replace("-r", string.Empty).Trim();

                var date = message.Substring(dateIndex, (hourIndex - dateIndex)).ToLower().Replace("-d", string.Empty).Trim();

                var hour = message.Substring(hourIndex).ToLower().Replace("-h", string.Empty).Trim();
                var hoursMinuts = hour.Split(":");
                var hours = hoursMinuts[0];
                var minuts = hoursMinuts[1];
                double.TryParse(hours, out double addHours);
                double.TryParse(minuts, out double addMinuts);


                DateTime.TryParse(date, out DateTime remeberDate);
                remeberDate = remeberDate.AddHours(addHours);
                remeberDate = remeberDate.AddMinutes(addMinuts);


                if (remeberDate <= DateTime.Now)
                {
                    return new Reminder(string.Empty, string.Empty, remeberDate, true);
                }
                else
                {
                    var reminderMessage = $"Me dijiste 👉 *{rememberme.ToLower()}* para hoy *{date.Trim()}* a las *{hour.ToLower()}* \n\n Espero no lo hayas olvidado! 😉";
                    var responseMessage = $"Te recordare \n 📖 {rememberme.ToLower().Replace("recuerdame", string.Empty)} \n 📅 *{date.Trim()}* \n 🕕 *{hour.ToLower()}*";
                    return new Reminder(reminderMessage, responseMessage, remeberDate, false);
                }
            }
            return new Reminder(string.Empty, string.Empty, new DateTime(), true);

        }
    }
}
