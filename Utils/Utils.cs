using System.Globalization;

namespace Bts.Utils;

static class Utils
{
    public static int GetNumberInputUtil(int lowerBound, int upperBound, string title = "Your option") // Get number input
    {
        Console.Write($"{title} (number between {lowerBound} - {upperBound.ToString("#,##0")}) : ");
        string inputStr = Console.ReadLine();
        while (inputStr == "" || inputStr.All(Char.IsAsciiDigit) == false)
        {
            Console.WriteLine("\nINPUT IS NOT VALID!!!");
            Console.Write($"{title} (number between {lowerBound} - {upperBound.ToString("#,##0")}) : ");
            inputStr = Console.ReadLine();
        }

        int input = Convert.ToInt32(inputStr);
        if (input < lowerBound || input > upperBound)
        {
            Console.WriteLine("\nINPUT IS NOT VALID!!!");
            input = GetNumberInputUtil(lowerBound, upperBound, title);
        }

        return input;
    }

    public static string GetStringInputUtil(string title)
    {
        Console.Write(title + " : ");
        var input = Console.ReadLine();
        while (input == "")
        {
            Console.WriteLine("Please input minimum 1 character!");
            Console.Write(title + " : ");
            input = Console.ReadLine();
        }
        return input;
    }

    public static DateTime GetDateTimeInputUtil(string title, string dateTimeFormat)
    {
        Console.Write($"{title} (ex: {DateTime.Now.ToString(dateTimeFormat)}): ");
        DateTime result;
        var input = Console.ReadLine();
        var success = DateTime.TryParseExact(input, dateTimeFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out result);
        while (success == false)
        {
            Console.WriteLine("Please input the datetime in the correct format!");
            Console.Write($"{title} (ex: {DateTime.Now.ToString(dateTimeFormat)}): ");
            input = Console.ReadLine();
            success = DateTime.TryParseExact(input, dateTimeFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out result);
        }
        return result;
    }

    public static string GenerateRandomAlphaNumericUtil()
    {
        Random rand = new Random();

        int letterLength = 3;
        int randomDigit;
        string code = "";
        char letter;
        for (int i = 0; i < letterLength; i++)
        {
            randomDigit = rand.Next(0, 26);
            letter = Convert.ToChar(randomDigit + 65);
            code = code + letter;
        }

        randomDigit = rand.Next(10, 99);
        code = code + randomDigit;

        return code;
    }
}