using cricarba.RememberMe.bot.Domain;

namespace cricarba.RememberMe.bot.Interfaces
{
    interface IReminderValidate
    {
        ReminderValidate Validate(string message);
    }
}