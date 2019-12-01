using Blazorise;

namespace Naobiz.Models
{
    static class Helper
    {
        public static string SizeToString(long value) => $"{value / (float)0x100000:f2} MB";

        public static void IsAny(ValidatorEventArgs e) => e.Status = ValidationStatus.Success;

        public static void IsTrue(ValidatorEventArgs e) => e.Status = (bool)e.Value ? ValidationStatus.Success : ValidationStatus.Error;

        public static void IsTrimmedNotEmpty(ValidatorEventArgs e) => e.Status = ((string)e.Value)?.Trim()?.Length > 0 ? ValidationStatus.Success : ValidationStatus.Error;

        public static void IsPasswordConfirmed(ValidatorEventArgs e, string password) => e.Status = password != null ? (string)e.Value == password ? ValidationStatus.Success : ValidationStatus.Error : ValidationStatus.None;
    }
}
