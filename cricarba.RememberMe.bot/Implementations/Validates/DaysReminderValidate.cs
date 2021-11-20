using cricarba.RememberMe.bot.Domain;
using cricarba.RememberMe.bot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cricarba.RememberMe.bot.Implementations.Validates
{
    class DaysReminderValidate : IReminderValidate
    {
        public ReminderValidate Validate(string message)
        {

            var isValid = false;
            if ((message.ToLower().Contains("recuerdame") || message.ToLower().Contains("-r")) && message.ToLower().Contains("días"))
            {
                var remembermeIndex = message.ToLower().IndexOf("recuerdame");
                var shortRemembermeIndex = message.ToLower().IndexOf("-r");
                var tomorrowIndex = message.ToLower().IndexOf("dias");
                isValid = (remembermeIndex >= 0 || shortRemembermeIndex >= 0) && tomorrowIndex > 0;
            }
            return new ReminderValidate(isValid, ReminderType.DaysReminder);
        }
    }
}
