using AdventOfCode2023.Day1;
using AdventOfCode2023.Day2;
using AdventOfCode2023.Day3;
using AdventOfCode2023.Day4;

int days = 1;

WriteDay(days++);
WriteChallenge(1, new Day1.Challenge1().Complete());
WriteChallenge(2, new Day1.Challenge2().Complete());

WriteDay(days++);
WriteChallenge(1, new Day2.Challenge1().Complete());
WriteChallenge(2, new Day2.Challenge2().Complete());

WriteDay(days++);
WriteChallenge(1, new Day3.Challenge1().Complete());
WriteChallenge(2, new Day3.Challenge2().Complete());

WriteDay(days++);
WriteChallenge(1, new Day4.Challenge1().Complete());
WriteChallenge(2, new Day4.Challenge2().Complete());

return;

void WriteDay(int day)
{
    Console.WriteLine($"--- Day {day} ---");
}

void WriteChallenge(int number, string result)
{
    Console.Write($"Challenge {number}: ");
    Console.WriteLine(result);
}