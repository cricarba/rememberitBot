using System;

namespace cricarba.RememberMe.bot.Domain
{
    public record Reminder(string ReminderText,string ResponseChatMessage, DateTime ReminderDate, bool IsPast);
}
