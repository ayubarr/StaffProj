namespace StaffProj.CMD.Menu.ConsoleHelpers
{
    internal static class ConsoleReader<T>
    {
        internal static T Read(string fieldName)
        {
            bool isIncorrectDataEntered = true;

            while (isIncorrectDataEntered)
            {
                try
                {
                    T data = ReadData(fieldName);
                    isIncorrectDataEntered = false;
                    return data;
                }
                catch
                {
                    Console.WriteLine($"An error while getting value received. Please enter {fieldName} again");
                }
            }

            return default(T);
        }

        private static T ReadData(string fieldName)
        {
            if (typeof(T) == typeof(string))
                return (T)(object)ConsoleHelper.GetStringFromConsole(fieldName);

            if (typeof(T) == typeof(int))
                return (T)(object)ConsoleHelper.GetIntFromConsole(fieldName);

            if (typeof(T) == typeof(DateTime))
                return (T)(object)ConsoleHelper.GetDateTimeFromConsole(fieldName);

            if (typeof(T) == typeof(float))
                return (T)(object)ConsoleHelper.GetFloatFromConsole(fieldName);

            return default(T);

        }
    }
}
