using cricarba.RememberMe.bot.Domain;
using cricarba.RememberMe.bot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace cricarba.RememberMe.bot.Implementations.Validates
{
    class FullReminderValidate : IReminderValidate
    {
        /*
         (Recuerdame|-R) Comience con Reccuerdame ó -R
         [\s][\s\S]+[\s] Cualquier cadena de texto
         (el) contien el
         [\s] espacion
         (d)[ií](a)[\s] la palabra dia o día más espacio
         [0-3][0-9](\/)[0-1][0-9](\/)[\d]{4}[\s]  Feha tipo 01/01/2001
         (a)[\s](las)[\s] a las 
         [0-2][0-9](:)[0-5][0-9] hora formato 22:59
         */
        private readonly string regex = @"(Recuerdame|-R)[\s][\s\S]+[\s](el)[\s](d)[ií](a)[\s][0-3][0-9](\/)[0-1][0-9](\/)[\d]{4}[\s](a)[\s](las)[\s][0-2][0-9](:)[0-5][0-9]";
        public ReminderValidate Validate(string message)
        {
            Match match = Regex.Match(message, regex, RegexOptions.IgnoreCase);
            var isValid = match.Success;
            return new ReminderValidate(isValid, ReminderType.FullReminder);
        }

    }
}
