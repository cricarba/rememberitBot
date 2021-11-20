using System;
using System.Collections.Generic;
using System.Text;

namespace cricarba.RememberMe.bot.Domain
{
    public record ReminderValidate(bool IsValid, ReminderType ReminderType);
}
