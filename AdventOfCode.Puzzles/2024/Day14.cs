using Utilities;

namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 14, CodeType.Original)]
public class Day14 : IPuzzle
{
	List<((int, int), (int, int))> robots = new List<((int, int), (int, int))>();
	List<(int, int)> finalPos = new List<(int, int)>();
	HashSet<(int, int)> robotTree = new HashSet<(int, int)>();
	public (string, string) Solve(PuzzleInput input)
	{
		robots.Clear();
		finalPos.Clear();
		robotTree.Clear();
		(long, long, List<(long, long)>) prize;
		prize.Item3 = new List<(long, long)>();
		foreach (var line in input.Lines)
		{
			var splits = line.Split(' ');
			var pos = splits[0].Split(',');
			var vec = splits[1].Split(',');
			var p1 = int.Parse(pos[0].Substring(2));
			var p2 = int.Parse(pos[1]);
			var v1 = int.Parse(vec[0].Substring(2));
			var v2 = int.Parse(vec[1]);

			robots.Add(new(new(p1, p2), new(v1, v2)));
		}

		var part1 = ProcessInput1(100);

		var part2 = ProcessInput2();

		return (part1, part2);
	}

    private string ProcessInput1(int time)
    {
        finalPos = new List<(int, int)>();
        long sum = 0;
        int width = 101;
        int height = 103;
        var midlinex = width / 2;
        var midliney = height / 2;
        int Q1 = 0;
        int Q2 = 0;
        int Q3 = 0;
        int Q4 = 0;
        foreach (var robot in robots)
        {
            var x = robot.Item1.Item1 + (robot.Item2.Item1 * time);
            var y = robot.Item1.Item2 + (robot.Item2.Item2 * time);
            var newx = x % width < 0 ? width + x % width : x % width;
            var newy = y % height < 0 ? height + y % height : y % height;
            finalPos.Add(new (Math.Abs(newx), Math.Abs(newy)));
        }

        foreach(var pos in finalPos)
        {
            if (pos.Item1 < midlinex && pos.Item2 < midliney)
            {
                Q1++;
            }

            if (pos.Item1 < midlinex && pos.Item2 > midliney)
            {
                Q2++;
            }

            if (pos.Item1 > midlinex && pos.Item2 < midliney)
            {
                Q3++;
            }

            if (pos.Item1 > midlinex && pos.Item2 > midliney)
            {
                Q4++;
            }
        }
        if (Q1 == 0) Q1 = 1;
        if (Q2 == 0) Q2 = 1;
        if (Q3 == 0) Q3 = 1;
        if (Q4 == 0) Q4 = 1;
        sum = Q1 * Q2 * Q3 * Q4;
        return $"{sum}";
    }


    private string ProcessInput2()
    {
        int sum = 0;
        var totalRobots = robots.Count;
        int width = 101;
        int height = 103;
        var overlap = true;
        while (overlap)
        {
            robotTree = new HashSet<(int, int)>();
            sum++;
            foreach (var robot in robots)
            {
                var x = robot.Item1.Item1 + (robot.Item2.Item1 * sum);
                var y = robot.Item1.Item2 + (robot.Item2.Item2 * sum);
                var newx = x % width < 0 ? width + x % width : x % width;
                var newy = y % height < 0 ? height + y % height : y % height;

                if (!robotTree.Add((Math.Abs(newx), Math.Abs(newy))))
                {
                    break;
                }
            }

            if(robotTree.Count == totalRobots)
            {
                overlap = false;
            }
        }
        return $"{sum}";
    }
}
