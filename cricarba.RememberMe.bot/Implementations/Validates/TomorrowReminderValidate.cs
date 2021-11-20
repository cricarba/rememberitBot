using cricarba.RememberMe.bot.Domain;
using cricarba.RememberMe.bot.Interfaces;
using System.Text.RegularExpressions;

namespace cricarba.RememberMe.bot.Implementations.Validates
{
    class TomorrowReminderValidate : IReminderValidate
    {
        private readonly string regex = @"(Recuerdame|-R)[\s][\s\S]+[\s](mañana|tomorrow)";
        public ReminderValidate Validate(string message)
        {
            Match match = Regex.Match(message, regex, RegexOptions.IgnoreCase);
            var isValid = match.Success;
            return new ReminderValidate(isValid, ReminderType.TomorrowReminder);
        }
    }
}
