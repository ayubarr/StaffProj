namespace StaffProj.CMD.Menu.ConsoleHelpers
{
    internal static class ConsoleHelper
    {
        internal static string GetStringFromConsole(string fieldName)
        {
            Console.WriteLine($"Please enter {fieldName}");
            string value = Console.ReadLine();
            if (value == " " || value == "") throw new Exception();

            return value;
        }

        internal static int GetIntFromConsole(string fieldName)
        {
            string value = GetStringFromConsole(fieldName);
            return int.Parse(value);
        }

        internal static DateTime GetDateTimeFromConsole(string fieldName)
        {
            string value = GetStringFromConsole(fieldName);
            return DateTime
                .ParseExact(value, ConsoleConstants.DateTimePattern, null);
        }
        internal static float GetFloatFromConsole(string fieldName)
        {
            string value = GetStringFromConsole(fieldName);
            return float.Parse(value);
        }
    }
}
