using cricarba.RememberMe.bot.Implementations.ReminderStrategy;
using cricarba.RememberMe.bot.Implementations.Validates;
using NUnit.Framework;

namespace cricarba.RememberMe.Bot.Test
{
    public class DaysReminderTest
    {

        static object[] TestCasesSuccess =
        {
                new object[] { "-R Hacer más test en 4 dias" },
                //new object[] { "-R Hacer más test en 4 días" },
                //new object[] { "Recuerdame Hacer más test en 13 días" },
                new object[] { "Recuerdame Hacer más test en 13 dias" },
         };

        [Test, TestCaseSource("TestCasesSuccess")]
        public void IsValidDayReminderTest(string message)
        {
            var dayReminderValidated = new DaysReminderValidate();
            var result = dayReminderValidated.Validate(message);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.ReminderType == bot.Domain.ReminderType.DaysReminder);

        }

        [Test, TestCaseSource("TestCasesSuccess")]
        public void GetDayDayReminderTest(string message)
        {
            var todayReminderValidated = new DaysReminder();
            var result = todayReminderValidated.GetReminder(message);
            Assert.IsFalse(result.IsPast);
            Assert.IsTrue(result.ReminderDate != default);
            Assert.IsFalse(string.IsNullOrEmpty(result.ResponseChatMessage));
            Assert.IsFalse(string.IsNullOrEmpty(result.ReminderText));
        }
    }
}