namespace AdventOfCode2023.Day4;

public static class Day4
{
    private static string[] ReadAllLines(string path)
    {
        return File.ReadAllLines(path);
    }

    private static string SumCardScores(this string[] input)
    {
        long score = (from card in input
                     select card.Split(':', StringSplitOptions.TrimEntries)[1]
                                .Split('|', StringSplitOptions.TrimEntries)
                     into values
                     let winningNumbers = values[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToHashSet()
                     select values[1]
                            .Split(' ')
                            .Where(winningNumbers.Contains)
                            .Aggregate(0L, (current, number) => current == 0 ? 1 : current * 2)).Sum();

        return score.ToString();
    }

    public class Challenge1 : IChallenge
    {
        public string Complete()
        {
            return ReadAllLines(Path.GetFullPath(@".\Day4\input.txt")).SumCardScores();
        }
    }

    public class Challenge2 : IChallenge
    {
        public string Complete()
        {
            return "";
        }
    }
}