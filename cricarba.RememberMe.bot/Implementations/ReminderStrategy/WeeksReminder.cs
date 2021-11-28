using cricarba.RememberMe.bot.Domain;
using cricarba.RememberMe.bot.Interfaces;
using System;
using System.Text.RegularExpressions;

namespace cricarba.RememberMe.bot.Implementations.ReminderStrategy
{
    public class WeeksReminder : IReminderStrategy
    {
        private readonly string regex = @"(Recuerdame|-R)[\s][\s\S]+[\s](en)[\s]\d{1,2}[\s](semanas)";

        public Reminder GetReminder(string message)
        {
            Match match = Regex.Match(message, regex, RegexOptions.IgnoreCase);
            var remembermeIndex = match.Groups[1].Index;
            var inIndex = match.Groups[2].Index;
            var weeksIndex = match.Groups[3].Index;

            if (remembermeIndex >= 0 && weeksIndex > 0)
            {
                var rememberme = message.Substring(remembermeIndex, inIndex).ToLower().Replace(match.Groups[1].Value.ToLower(), string.Empty).Trim();
                var remeberDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                var endInIndex = inIndex + 2;
                var lenght = weeksIndex - endInIndex;
                var weeks = message.Substring(endInIndex, lenght).ToLower().Trim();
                int.TryParse(weeks, out int addWeeks);
                int addDays = (addWeeks * 7);
                remeberDate = remeberDate.AddDays(addDays);


                if (remeberDate <= DateTime.Now)
                {
                    return new Reminder(string.Empty, string.Empty, remeberDate, true);
                }
                else
                {
                    var reminderMessage = $"Me dijiste 👉 *{rememberme.ToLower()}* para hoy *{remeberDate.ToShortDateString()}*   \n\n Espero no lo hayas olvidado! 😉";

                    var responseMessage = $"Te recordare \n 📖 {rememberme.ToLower().Replace("recuerdame", string.Empty)} \n 📅 en *{weeks}* semanas";

                    return new Reminder(reminderMessage, responseMessage, remeberDate, false);
                }
            }
            return new Reminder(string.Empty, string.Empty, new DateTime(), true);
        }
    }
}
