using cricarba.RememberMe.bot.Implementations.ReminderStrategy;
using cricarba.RememberMe.bot.Implementations.Validates;
using NUnit.Framework;

namespace cricarba.RememberMe.Bot.Test
{
    public class TodayReminderTest
    {

        static object[] TestCasesSuccess =
        {
                new object[] { "-R Hacer más test hoy a las 04:00" },
                new object[] { "-R Hacer más test hoy a las 23:00" },
                new object[] { "Recuerdame Hacer más test hoy a las 17:15" },
                new object[] { "Recuerdame Hacer más test hoy a las 07:15" },
         };

        [Test, TestCaseSource("TestCasesSuccess")]
        public void IsValidDayReminderTest(string message)
        {
            var todayReminderValidated = new TodayReminderValidate();
            var result = todayReminderValidated.Validate(message);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.ReminderType == bot.Domain.ReminderType.TodayReminder);

        }

        [Test, TestCaseSource("TestCasesSuccess")]
        public void GetTodayDayReminderTest(string message)
        {
            var todayReminderValidated = new TodayReminder();
            var result = todayReminderValidated.GetReminder(message);
            Assert.IsFalse(result.IsPast);
            Assert.IsTrue(result.ReminderDate != default);
            Assert.IsFalse(string.IsNullOrEmpty(result.ResponseChatMessage));
            Assert.IsFalse(string.IsNullOrEmpty(result.ReminderText));
        }
    }
}
