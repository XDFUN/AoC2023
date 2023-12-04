namespace AdventOfCode2023.Day3;

public static class Day3
{
    private static string[] ReadAllLines(string path)
    {
        return File.ReadAllLines(path);
    }

    private static int SumAdjacentNumbers(this string[] input)
    {
        var result = 0;

        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[i].Length; j++)
            {
                if (!char.IsDigit(input[i][j]) || !HasAdjacent(input, i, j))
                {
                    continue;
                }

                result += GetNumberFromIndex(input[i], j);

                j = JumpToNextNumber(input[i], j);
            }
        }

        return result;
    }

    private static bool HasAdjacent(string[] input, int arrayIndex, int stringIndex)
    {
        if (arrayIndex < 0 || arrayIndex >= input.Length)
        {
            throw new IndexOutOfRangeException($"Array index must be between 0 and {input.Length}!");
        }

        if (stringIndex < 0 || stringIndex >= input[0].Length)
        {
            throw new IndexOutOfRangeException($"String index must be between 0 and {input[0].Length}!");
        }

        bool atTop = arrayIndex == 0;
        bool atBottom = arrayIndex == input.Length - 1;

        bool atRight = stringIndex == input[0].Length - 1;
        bool atLeft = stringIndex == 0;

        var result = false;

        // . . .
        // . 1 .
        // . . .

        // top
        result |= !atTop
                  && !atRight
                  && input[arrayIndex - 1][stringIndex + 1] != '.'
                  && !char.IsDigit(input[arrayIndex - 1][stringIndex + 1]);

        result |= !atTop
                  && input[arrayIndex - 1][stringIndex] != '.'
                  && !char.IsDigit(input[arrayIndex - 1][stringIndex]);

        result |= !atTop
                  && !atLeft
                  && input[arrayIndex - 1][stringIndex - 1] != '.'
                  && !char.IsDigit(input[arrayIndex - 1][stringIndex - 1]);

        // just right
        result |= !atRight
                  && input[arrayIndex][stringIndex + 1] != '.'
                  && !char.IsDigit(input[arrayIndex][stringIndex + 1]);

        result |= !atLeft
                  && input[arrayIndex][stringIndex - 1] != '.'
                  && !char.IsDigit(input[arrayIndex][stringIndex - 1]);

        // bottom
        result |= !atBottom
                  && !atRight
                  && input[arrayIndex + 1][stringIndex + 1] != '.'
                  && !char.IsDigit(input[arrayIndex + 1][stringIndex + 1]);

        result |= !atBottom
                  && input[arrayIndex + 1][stringIndex] != '.'
                  && !char.IsDigit(input[arrayIndex + 1][stringIndex]);

        result |= !atBottom
                  && !atLeft
                  && input[arrayIndex + 1][stringIndex - 1] != '.'
                  && !char.IsDigit(input[arrayIndex + 1][stringIndex - 1]);

        return result;
    }

    private static int GetNumberFromIndex(string input, int stringIndex)
    {
        int forward = stringIndex;
        int backwards = stringIndex;

        for (; forward >= 0 && char.IsDigit(input[forward]); forward--) { }

        forward++;

        for (; backwards < input.Length && char.IsDigit(input[backwards]); backwards++) { }

        string number = input[forward..backwards];

        return int.Parse(number);
    }

    private static int JumpToNextNumber(string input, int index)
    {
        for (; index < input.Length && char.IsDigit(input[index]); index++) { }

        return index;
    }

    public class Challenge1 : IChallenge
    {
        public string Complete()
        {
            return ReadAllLines(Path.GetFullPath(@".\Day3\input.txt")).SumAdjacentNumbers().ToString();
        }
    }

    public class Challenge2 : IChallenge
    {
        public string Complete()
        {
            return "FUCK";
        }
    }
}