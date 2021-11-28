using cricarba.RememberMe.bot.Domain;
using cricarba.RememberMe.bot.Implementations.Validates;
using cricarba.RememberMe.bot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cricarba.RememberMe.bot.Implementations
{
    class ReminderValidateRule : IReminderValidate
    {
        List<IReminderValidate> reminderValidatesRules = new List<IReminderValidate>();

        public ReminderValidateRule()
        {
            reminderValidatesRules.Add(new FullReminderValidate());
            reminderValidatesRules.Add(new ShortReminderValidate());
            reminderValidatesRules.Add(new TomorrowReminderValidate());
            reminderValidatesRules.Add(new TodayReminderValidate());
            reminderValidatesRules.Add(new DaysReminderValidate());
            reminderValidatesRules.Add(new WeeksReminderValidate());

        }

        public ReminderValidate Validate(string message)
        {
            foreach (var validateRule in reminderValidatesRules)
            {
              var result =  validateRule.Validate(message);
                if (result.IsValid)
                {
                    return result;
                }                   
            }
            return new ReminderValidate(false, ReminderType.Unknown);
        }
    }
}
