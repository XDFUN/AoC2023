using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day2;

public partial class Day2
{
    #region Fields

    private static readonly Regex s_gameId = GetGameIdRegex();
    private static readonly Regex s_redAmount = GetRedAmountRegex();
    private static readonly Regex s_blueAmount = GetBlueAmountRegex();
    private static readonly Regex s_greenAmount = GetGreenAmountRegex();

    #endregion

    #region Methods

    private static string[] GetFileLines(string path)
    {
        return File.ReadAllLines(path);
    }

    private static Game[] GetGames(string[] inputs)
    {
        var games = new Game[inputs.Length];

        for (var i = 0; i < inputs.Length; i++)
        {
            games[i] = GetGame(inputs[i]);
        }

        return games;
    }

    private static Game GetGame(string input)
    {
        return new Game(GetGameId(input), GetGameResults(input));
    }

    private static int GetGameId(string input)
    {
        Match match = s_gameId.Match(input);

        if (!match.Success)
        {
            throw new InvalidOperationException("Input must start with 'Game (number):'");
        }

        return int.Parse(match.Groups["id"].Value);
    }

    private static GameResult[] GetGameResults(string input)
    {
        string rawGames = input.Split(':', StringSplitOptions.TrimEntries)[1];

        string[] games = rawGames.Split(';', StringSplitOptions.TrimEntries);

        var results = new GameResult[games.Length];

        for (var i = 0; i < games.Length; i++)
        {
            string game = games[i];

            Match redMatch = s_redAmount.Match(game);
            Match blueMatch = s_blueAmount.Match(game);
            Match greenMatch = s_greenAmount.Match(game);

            int red = redMatch.Success ? int.Parse(redMatch.Groups["amount"].Value) : 0;
            int blue = blueMatch.Success ? int.Parse(blueMatch.Groups["amount"].Value) : 0;
            int green = greenMatch.Success ? int.Parse(greenMatch.Groups["amount"].Value) : 0;

            results[i] = new GameResult(blue, red, green);
        }

        return results;
    }

    private static int SumGameIdsForPossibleGames(Game[] games, int redCubes, int blueCubes, int greenCubes)
    {
        return games.Where(game => game.IsPossible(blueCubes, redCubes, greenCubes)).Sum(x => x.Id);
    }

    private static long SumGameResultPowers(Game[] games)
    {
        return games.Sum(game => game.GetMinimumResult().Pow());
    }

    [GeneratedRegex("^Game (?<id>[0-9]+):", RegexOptions.Compiled)]
    private static partial Regex GetGameIdRegex();

    [GeneratedRegex("(?<amount>[0-9]+) red")]
    private static partial Regex GetRedAmountRegex();

    [GeneratedRegex("(?<amount>[0-9]+) blue")]
    private static partial Regex GetBlueAmountRegex();

    [GeneratedRegex("(?<amount>[0-9]+) green")]
    private static partial Regex GetGreenAmountRegex();

    #endregion

    #region Internal Types

    private readonly record struct Game(int Id, GameResult[] Results)
    {
        #region Methods

        public bool IsPossible(int blueCubes, int redCubes, int greenCubes)
        {
            return Results.All(x => x.IsPossible(blueCubes, redCubes, greenCubes));
        }

        public GameResult GetMinimumResult()
        {
            if (Results.Length == 0)
            {
                return new GameResult(0, 0, 0);
            }

            int minBlue = Results[0].BlueCubes;
            int minRed = Results[0].RedCubes;
            int minGreen = Results[0].GreenCubes;

            foreach (GameResult result in Results)
            {
                if (minBlue < result.BlueCubes)
                {
                    minBlue = result.BlueCubes;
                }

                if (minRed < result.RedCubes)
                {
                    minRed = result.RedCubes;
                }

                if (minGreen < result.GreenCubes)
                {
                    minGreen = result.GreenCubes;
                }
            }

            return new GameResult(minBlue, minRed, minGreen);
        }

        #endregion
    }

    private readonly record struct GameResult(int BlueCubes, int RedCubes, int GreenCubes)
    {
        #region Methods

        public bool IsPossible(int blueCubes, int redCubes, int greenCubes)
        {
            return BlueCubes <= blueCubes && RedCubes <= redCubes && GreenCubes <= greenCubes;
        }

        public long Pow()
        {
            return BlueCubes * RedCubes * GreenCubes;
        }

        #endregion
    }

    public class Challenge1 : IChallenge
    {
        #region IChallenge

        public string Complete()
        {
            return SumGameIdsForPossibleGames(GetGames(GetFileLines(Path.GetFullPath(@".\Day2\input.txt"))), 12, 14, 13)
                .ToString();
        }

        #endregion
    }

    public class Challenge2 : IChallenge
    {
        #region IChallenge

        public string Complete()
        {
            return SumGameResultPowers(GetGames(GetFileLines(Path.GetFullPath(@".\Day2\input.txt"))))
                .ToString();
        }

        #endregion
    }

    #endregion
}