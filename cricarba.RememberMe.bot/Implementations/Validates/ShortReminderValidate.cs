    using cricarba.RememberMe.bot.Domain;
using cricarba.RememberMe.bot.Interfaces;
using System.Text.RegularExpressions;

namespace cricarba.RememberMe.bot.Implementations.Validates
{
    class ShortReminderValidate : IReminderValidate
    {

        private readonly string regex = @"(-R)[\s][\s\S]+[\s](-D)[\s][0-3][0-9](\/)[0-1][0-9](\/)[\d]{4}[\s](-H)[\s][0-2][0-9](:)[0-5][0-9]";
        public ReminderValidate Validate(string message)
        {
            Match match = Regex.Match(message, regex, RegexOptions.IgnoreCase);
            var isValid = match.Success;
            return new ReminderValidate(isValid, ReminderType.ShortReminder );
        }
    }
}
