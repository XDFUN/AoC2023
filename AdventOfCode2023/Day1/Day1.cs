namespace AdventOfCode2023.Day1;

public class Day1
{
    #region Fields

    private static readonly string[] s_writtenOutDigits =
    {
        "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
    };

    #endregion

    #region Methods

    private static string[] GetFileLines(string path)
    {
        return File.ReadLines(path).ToArray();
    }

    private static int CombineDigits(char tens, char ones)
    {
        return int.Parse(tens.ToString() + ones);
    }

    private static char GetFirstDigit(string input)
    {
        return input.First(char.IsDigit);
    }

    private static char GetLastDigit(string input)
    {
        return input.Last(char.IsNumber);
    }

    private static char GetFirstCompleteDigit(string input)
    {
        int index = GetFirstDigitIndex(input);

        return char.IsDigit(input[index]) ? input[index] : GetWrittenDigit(input, index);
    }

    private static char GetLastCompleteDigit(string input)
    {
        int index = GetLastDigitIndex(input);

        return char.IsDigit(input[index]) ? input[index] : GetWrittenDigit(input, index);
    }

    private static char GetWrittenDigit(string input, int index)
    {
        string writtenDigit = s_writtenOutDigits.First(x => input.Length >= index + x.Length && input.Substring(index, x.Length) == x);

        return writtenDigit switch
        {
            "one" => '1',
            "two" => '2',
            "three" => '3',
            "four" => '4',
            "five" => '5',
            "six" => '6',
            "seven" => '7',
            "eight" => '8',
            "nine" => '9',
            _ => throw new NotSupportedException("Not supported digit.")
        };
    }

    private static int GetFirstDigitIndex(string input)
    {
        int writtenIndex = GetFirstWrittenIndex(input);
        int digitIndex = input.IndexOf(GetFirstDigit(input));

        if (writtenIndex == -1)
        {
            return digitIndex;
        }

        if (digitIndex == -1)
        {
            return writtenIndex;
        }

        return writtenIndex < digitIndex ? writtenIndex : digitIndex;
    }

    private static int GetLastDigitIndex(string input)
    {
        int writtenIndex = GetLastWrittenIndex(input);
        int digitIndex = input.LastIndexOf(GetLastDigit(input));

        if (writtenIndex == -1)
        {
            return digitIndex;
        }

        if (digitIndex == -1)
        {
            return writtenIndex;
        }

        return writtenIndex > digitIndex ? writtenIndex : digitIndex;
    }
    
    private static int GetFirstWrittenIndex(string input)
    {
        HashSet<int> indexes = s_writtenOutDigits.Select
                                                 (
                                                     writtenOutDigit => input.IndexOf
                                                         (writtenOutDigit, StringComparison.Ordinal)
                                                 )
                                                 .ToHashSet();

        indexes.Remove(-1);

        return indexes.Count == 0 ? -1 : indexes.Min();
    }

    private static int GetLastWrittenIndex(string input)
    {
        HashSet<int> indexes = s_writtenOutDigits.Select
                                                 (
                                                     writtenOutDigit => input.LastIndexOf
                                                         (writtenOutDigit, StringComparison.Ordinal)
                                                 )
                                                 .ToHashSet();

        indexes.Remove(-1);

        return indexes.Count == 0 ? -1 : indexes.Max();
    }

    #endregion

    #region Internal Types

    public class Challenge1 : IChallenge
    {
        #region IDay

        public string Complete()
        {
            int result = GetFileLines
                    (Path.GetFullPath(@".\Day1\input.txt"))
                .Sum(str => CombineDigits(GetFirstDigit(str), GetLastDigit(str)));

            return result.ToString();
        }

        #endregion
    }

    public class Challenge2 : IChallenge
    {
        #region IDay

        public string Complete()
        {
            int result = GetFileLines
                    (Path.GetFullPath(@".\Day1\input.txt"))
                .Sum(str => CombineDigits(GetFirstCompleteDigit(str), GetLastCompleteDigit(str)));

            return result.ToString();
        }

        #endregion
    }

    #endregion
}