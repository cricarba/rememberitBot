using cricarba.RememberMe.bot.Domain;
using cricarba.RememberMe.bot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cricarba.RememberMe.bot.Implementations.Validates
{
    public class DaysReminderValidate : IReminderValidate
    {
        private readonly string regex = @"(Recuerdame|-R)[\s][\s\S]+[\s](en)[\s]\d{1,2}[\s](d)[ií](as)";
        public ReminderValidate Validate(string message)
        {
            Match match = Regex.Match(message, regex, RegexOptions.IgnoreCase);
            var isValid = match.Success;
            return new ReminderValidate(isValid, ReminderType.DaysReminder);
        }
    }
}
