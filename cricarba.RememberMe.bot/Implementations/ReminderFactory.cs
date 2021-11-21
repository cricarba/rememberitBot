using cricarba.RememberMe.bot.Domain;
using cricarba.RememberMe.bot.Implementations.ReminderStrategy;
using cricarba.RememberMe.bot.Interfaces;
using System.Collections.Generic;


namespace cricarba.RememberMe.bot.Implementations
{
    class ReminderFactory
    {
        Dictionary<ReminderType, IReminderStrategy> remiderStrategy = new Dictionary<ReminderType, IReminderStrategy>();
        public ReminderFactory()
        {
            remiderStrategy.Add(ReminderType.FullReminder, new FullReminder());
            remiderStrategy.Add(ReminderType.ShortReminder, new ShortReminder());
            remiderStrategy.Add(ReminderType.TomorrowReminder, new TomorrowReminder());
            remiderStrategy.Add(ReminderType.TodayReminder, new TodayReminder());
            remiderStrategy.Add(ReminderType.DaysReminder, new DaysReminder());
        }

        public Reminder GetReminder(ReminderType type, string message)
        {
           return remiderStrategy[type].GetReminder(message);
        }
    }
}
