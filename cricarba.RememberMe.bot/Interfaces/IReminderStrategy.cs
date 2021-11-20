using cricarba.RememberMe.bot.Domain;

namespace cricarba.RememberMe.bot.Interfaces
{
    interface IReminderStrategy
    {
        Reminder GetReminder(string message);
    }
}