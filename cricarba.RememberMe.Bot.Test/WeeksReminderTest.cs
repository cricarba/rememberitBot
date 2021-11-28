using cricarba.RememberMe.bot.Implementations.ReminderStrategy;
using cricarba.RememberMe.bot.Implementations.Validates;
using NUnit.Framework;

namespace cricarba.RememberMe.Bot.Test
{
    public class WeeksReminderTest
    {

        static object[] TestCasesSuccess =
        {
                new object[] { "-R Hacer más test en 4 semanas" },
                new object[] { "Recuerdame Hacer más test en 13 semanas" },            
         };

        [Test, TestCaseSource("TestCasesSuccess")]
        public void IsValidWeeksReminderTest(string message)
        {
            var weekReminderValidated = new WeeksReminderValidate();
            var result = weekReminderValidated.Validate(message);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.ReminderType == bot.Domain.ReminderType.WeeksReminder);

        }

        [Test, TestCaseSource("TestCasesSuccess")]
        public void GetWeeksReminderTest(string message)
        {
            var weekReminderValidated = new WeeksReminder();
            var result = weekReminderValidated.GetReminder(message);
            Assert.IsFalse(result.IsPast);
            Assert.IsTrue(result.ReminderDate != default);
            Assert.IsFalse(string.IsNullOrEmpty(result.ResponseChatMessage));
            Assert.IsFalse(string.IsNullOrEmpty(result.ReminderText));
        }
    }
}