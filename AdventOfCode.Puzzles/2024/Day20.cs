using System.Collections.Concurrent;
using Utilities;

namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 20, CodeType.Original)]
public class Day20 : IPuzzle
{
	(Dictionary<Coordinate2D, char> map, int maxX, int maxY) map;
	ConcurrentDictionary<Coordinate2D, int> moves = new ConcurrentDictionary<Coordinate2D, int>();
	Coordinate2D start = new Coordinate2D(0, 0);
	Coordinate2D end = new Coordinate2D(0, 0);
	public (string, string) Solve(PuzzleInput input)
	{
		moves.Clear();
		map = input.Text.GenerateMap(false);

		start = map.map.First(q => q.Value == 'S').Key;
		end = map.map.First(q => q.Value == 'E').Key;

		var part1 = ProcessInput1();

		var part2 = ProcessInput2();

		return (part1, part2);
	}

    private static int ReachableCoords(Coordinate2D cur, int dist, ConcurrentDictionary<Coordinate2D, int> moves)
    {
        int total = 0;
        for (int x = -dist; x <= dist; x++)
        {
            int remain = dist - Math.Abs(x);
            for (int y = -remain; y <= remain; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                var diff = moves.GetValueOrDefault((cur.x + x, cur.y + y), -1);
                if (diff > 0 &&
                    moves[cur] - diff - (cur.x + x, cur.y + y).ManhattanDistance(cur) >= 100)
                    total++;
            }
        }
        return total;
    }

    private void BuildRoute()
    {
        var cur = start;
        int count = 0;
        moves[start] = 0;
        while (cur != end)
        {
            var next = cur.Neighbors().FirstOrDefault(q => map.map[q] != '#' &&
                    !moves.ContainsKey(q));
            moves[next] = ++count;
            cur = next;
        }
    }

    private string ProcessInput1()
    {
        ConcurrentBag<long> sum = [];
        BuildRoute();
        moves.Keys.AsParallel().ForAll(move =>
        {
            sum.Add(ReachableCoords(move, 2, moves));
        });
        
        return $"{sum.Sum()}";
    }

    private string ProcessInput2()
    {
        ConcurrentBag<long> sum = [];
        moves.Keys.AsParallel().ForAll(move =>
        {
            sum.Add(ReachableCoords(move, 20, moves));
        });

        return $"{sum.Sum()}";
    }
}
