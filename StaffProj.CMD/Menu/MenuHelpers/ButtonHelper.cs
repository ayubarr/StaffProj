namespace StaffProj.CMD.Menu.MenuHelpers
{
    internal static class ButtonController
    {
        internal static string Button(string[] keys)
        {
            string input = "";
            do
            {
                var inputKey = Console.ReadKey(true);
                int keyAscii = (int)inputKey.Key;
                if (keyAscii <= 57 && keyAscii >= 49 || keyAscii >= 97 && keyAscii <= 105)
                {
                    input = inputKey.Key.ToString();
                    input = input[input.Length - 1].ToString();
                    if (keys.Contains(input))
                        return input;
                    Button(keys);
                }
                input = inputKey.Key.ToString();
            }
            while (!keys.Contains(input));
            return input;
        }
    }
}
