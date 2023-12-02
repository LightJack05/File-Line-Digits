namespace LiteralDigits;

public static class Program
{
    public static void Main(string[] args)
    {
        List<string> lines = new();
        List<int> lineValues = new();
        using (StreamReader sr = new("input.txt"))
        {
            lines = sr.ReadToEnd().ReplaceLineEndings("\n").Split("\n").ToList();
        }
        foreach (string line in lines)
        {
            int lineValue = GetLineValue(line);
            lineValues.Add(lineValue);
        }
        System.Console.WriteLine(lineValues.Sum());
    }

    static int GetLineValue(string line)
    {
        List<Digit> lineDigits = GetDigitDictionary(line);
        AddLiteralDigits(line, lineDigits);
        if (lineDigits.Count <= 0)
        {
            return 0;
        }
        else
        {
            char firstDigit = findFirstDigit(lineDigits);
            char lastDigit = findLastDigit(lineDigits);
            return Convert.ToInt32(Convert.ToString(firstDigit) + Convert.ToString(lastDigit));
        }
    }
    static List<Digit> GetDigitDictionary(string line)
    {
        List<Digit> digits = new();
        digits = GetSimpleDigits(line);
        return digits;
    }

    static List<Digit> GetSimpleDigits(string line)
    {
        List<Digit> digits = new();
        for (int i = 0; i < line.Length; i++)
        {
            Char character = line[i];
            if (Char.IsDigit(character) && !(digits.Where((x) => x.Equals(character)).Count() > 0))
            {
                Digit digit = new(character);
                digits.Add(digit);
                digit.LastIndex = i;
                digit.FirstIndex = i;
            }
            else if (Char.IsDigit(character))
            {
                digits.Where((x) => x.Equals(character)).FirstOrDefault().LastIndex = i;
            }
        }
        return digits;
    }

    static List<Digit> AddLiteralDigits(string line, List<Digit> digits)
    {
        List<string> digitWords = new List<string>
        {
            "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
        };

        int[] firstIndicies = new int[10];
        for (int i = 0; i < digitWords.Count; i++)
        {
            firstIndicies[i] = line.IndexOf(digitWords[i]);
        }
        int[] lastIndicies = new int[10];
        for (int i = 0; i < digitWords.Count; i++)
        {
            lastIndicies[i] = line.LastIndexOf(digitWords[i]);
        }
        for (int i = 0; i < firstIndicies.Length; i++)
        {
            int index = firstIndicies[i];
            if (index != -1)
            {
                if (digits.Where((x) => x.Equals((Convert.ToString(i)[0]))).Count() <= 0)
                {
                    Digit digit = new(Convert.ToString(i)[0]);
                    digit.LastIndex = index;
                    digit.FirstIndex = index;
                    digits.Add(digit);
                }
                int oldFirstIndex = digits.Where((x) => x.Equals((Convert.ToString(i)[0]))).FirstOrDefault().FirstIndex;
                digits.Where((x) => x.Equals((Convert.ToString(i)[0]))).FirstOrDefault().FirstIndex = oldFirstIndex > index ? index : oldFirstIndex;
            }
        }

        for (int i = 0; i < lastIndicies.Length; i++)
        {
            int index = lastIndicies[i];
            if (index != -1)
            {
                if (digits.Where((x) => x.Equals((Convert.ToString(i)[0]))).Count() <= 0)
                {
                    Digit digit = new(Convert.ToString(i)[0]);
                    digit.LastIndex = index;
                    digit.FirstIndex = index;
                    digits.Add(digit);
                }
                int oldLastIndex = digits.Where((x) => x.Equals((Convert.ToString(i)[0]))).FirstOrDefault().LastIndex;
                digits.Where((x) => x.Equals((Convert.ToString(i)[0]))).FirstOrDefault().LastIndex = oldLastIndex < index ? index : oldLastIndex;
            }
        }

        return digits;
    }

    static char findFirstDigit(List<Digit> digits)
    {

        Digit minDigit = digits[0];
        foreach (Digit digit in digits)
        {
            minDigit = minDigit.FirstIndex > digit.FirstIndex ? digit : minDigit;
        }
        return minDigit.Character;
    }

    static char findLastDigit(List<Digit> digits)
    {
        Digit maxDigit = digits[0];
        foreach (Digit digit in digits)
        {
            maxDigit = maxDigit.LastIndex < digit.LastIndex ? digit : maxDigit;
        }
        return maxDigit.Character;
    }
}