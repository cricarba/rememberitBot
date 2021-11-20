using cricarba.RememberMe.bot.Domain;
using cricarba.RememberMe.bot.Interfaces;

namespace cricarba.RememberMe.bot.Implementations.Validates
{
    class ShortReminderValidate : IReminderValidate
    {
        ReminderValidate IReminderValidate.Validate(string message)
        {
            var isValid = false;
            if (message.ToLower().Contains("-r"))
            {
                var remembermeIndex = message.ToLower().IndexOf("-r");
                var dateIndex = message.ToLower().IndexOf("-d");
                var hourIndex = message.ToLower().IndexOf("-h");
                isValid = remembermeIndex >= 0 && dateIndex > 0 && hourIndex > 0;
            }
            return new ReminderValidate(isValid, ReminderType.ShortReminder); 
        }
    }
}
