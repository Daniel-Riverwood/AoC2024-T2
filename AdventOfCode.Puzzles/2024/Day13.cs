using Utilities;

namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 13, CodeType.Original)]
public class Day13 : IPuzzle
{
	Dictionary<(long, long), List<(long, long)>> Prizes = new Dictionary<(long, long), List<(long, long)>>();
	public (string, string) Solve(PuzzleInput input)
	{
		Prizes.Clear();
		(long, long, List<(long, long)>) prize;
		prize.Item3 = new List<(long, long)>();
		foreach (var line in input.Lines)
		{
			if (line.Contains("Prize:"))
			{
				var splits = line.Split(':');
				var nums = splits[1].Split(',');
				var x = long.Parse(nums[0].Trim().Substring(2));
				var y = long.Parse(nums[1].Trim().Substring(2));
				prize.Item1 = x;
				prize.Item2 = y;
				Prizes.Add((prize.Item1, prize.Item2), prize.Item3);
				prize.Item3 = new List<(long, long)>();
			}
			else if (!string.IsNullOrWhiteSpace(line))
			{
				var splits = line.Split(':');
				var nums = splits[1].Split(',');
				var x = long.Parse(nums[0].Trim().Substring(2));
				var y = long.Parse(nums[1].Trim().Substring(2));
				prize.Item3.Add((x, y));
			}
		}

		var part1 = ProcessInput1(100);

		var part2 = ProcessInput2();

		return (part1, part2);
	}

    private string ProcessInput1(int max)
    {
        long sum = 0;
        foreach(var prize in Prizes)
        {
            var firstVal = prize.Value.First();
            var lastVal = prize.Value.Last();
            var prizex = prize.Key.Item1;
            var prizey = prize.Key.Item2;

            var check1 = (prizey * firstVal.Item1 - prizex * firstVal.Item2) / (lastVal.Item2 * firstVal.Item1 - lastVal.Item1 * firstVal.Item2);
            var check2 = (prizex - check1 * lastVal.Item1) / firstVal.Item1;

            var validx = check1 * lastVal.Item1 + check2 * firstVal.Item1 == prizex;
            var validy = check1 * lastVal.Item2 + check2 * firstVal.Item2 == prizey;
            if (check1 >= 0 && check2 >= 0 && validx && validy)
            {
                sum += (3 * check2 + check1);
            }
        }
        return $"{sum}";
    }


    private string ProcessInput2()
    {
        long sum = 0;
        foreach (var prize in Prizes)
        {
            var firstVal = prize.Value.First();
            var lastVal = prize.Value.Last();
            var prizex = prize.Key.Item1 + 10000000000000;
            var prizey = prize.Key.Item2 + 10000000000000;

            var check1 = (prizey * firstVal.Item1 - prizex * firstVal.Item2) / (lastVal.Item2 * firstVal.Item1 - lastVal.Item1 * firstVal.Item2);
            var check2 = (prizex - check1 * lastVal.Item1) / firstVal.Item1;

            var validx = check1 * lastVal.Item1 + check2 * firstVal.Item1 == prizex;
            var validy = check1 * lastVal.Item2 + check2 * firstVal.Item2 == prizey;
            if (check1 >= 0 && check2 >= 0 && validx && validy)
            {
                sum += (3 * check2 + check1);
            }
        }
        return $"{sum}";
    }
}
