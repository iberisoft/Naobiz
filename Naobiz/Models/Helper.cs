using Blazorise;
using System;
using System.Linq;

namespace Naobiz.Models
{
    static class Helper
    {
        public static string SizeToString(long value) => $"{value / (float)0x100000:f2} MB";

        public static string AgeToString(int value, string unit)
        {
            if (value > 1)
            {
                unit += "s";
            }
            return $"Hace {value} {unit}";
        }

        public static void IsAny(ValidatorEventArgs e) => e.Status = ValidationStatus.Success;

        public static void IsTrue(ValidatorEventArgs e) => e.Status = (bool)e.Value ? ValidationStatus.Success : ValidationStatus.Error;

        public static void IsTrimmedNotEmpty(ValidatorEventArgs e) => e.Status = ((string)e.Value)?.Trim()?.Length > 0 ? ValidationStatus.Success : ValidationStatus.Error;

        public static void IsPasswordValid(ValidatorEventArgs e) => e.Status = IsPasswordValid((string)e.Value) ? ValidationStatus.Success : ValidationStatus.Error;

        public static bool IsPasswordValid(string password) => password?.Length >= 8 && password.Any(c => char.IsUpper(c)) && password.Any(c => char.IsDigit(c));

        public static string PasswordRuleDescription => "8 símbolos mínimo incluyendo una letra mayúscula y un número";

        public static void IsPasswordConfirmed(ValidatorEventArgs e, string password) => e.Status = password != null ? (string)e.Value == password ? ValidationStatus.Success : ValidationStatus.Error : ValidationStatus.None;

        public static void IsUrl(ValidatorEventArgs e) => e.Status = e.Value != null ? Uri.TryCreate((string)e.Value, UriKind.Absolute, out Uri _) ? ValidationStatus.Success : ValidationStatus.Error : ValidationStatus.None;
    }
}
