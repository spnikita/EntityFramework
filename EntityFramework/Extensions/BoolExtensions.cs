namespace EntityFramework.Extensions
{
    internal static class BoolExtensions
    {
        public static string ToYesNo(this bool value) => value ? "Да" : "Нет";
    }
}
