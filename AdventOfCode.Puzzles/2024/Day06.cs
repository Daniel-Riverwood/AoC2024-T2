using System.Collections.Concurrent;
using Utilities;

namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 06, CodeType.Original)]
public class Day06 : IPuzzle
{
	private char[][] map;
	readonly Dictionary<(int, int), CompassDirection> seenPositions = [];
	(int, int) startingPosition = new(0, 0);

	public (string, string) Solve(PuzzleInput input)
	{
		seenPositions.Clear();
		map = new char[input.Lines.Length][];
		for (var x = 0; x < input.Lines.Length; x++)
		{
			map[x] = new char[input.Lines[x].Length];
			for (var y = 0; y < input.Lines[x].Length; y++)
			{
				map[x][y] = input.Lines[x][y];
				if (map[x][y] == '^')
					startingPosition = new(x, y);
			}
		}

		var part1 = ProcessInput1();

		var part2 = ProcessInput2();

		return (part1, part2);
	}

	private string ProcessInput1()
	{
		var currentDirection = CompassDirection.N;
		seenPositions.Add(startingPosition, currentDirection);
		(int, int) nextPosition = new(0, 0);
		if (startingPosition.Item1 - 1 > -1)
		{
			nextPosition = (startingPosition.Item1 - 1, startingPosition.Item2);
		}
		var previous = nextPosition;
		var reachedEdge = false;
		while (!reachedEdge)
		{
			if (map[nextPosition.Item1][nextPosition.Item2] == '#')
			{
				nextPosition = previous;
				currentDirection = currentDirection.TurnClockwise();
			}
			else
			{
				seenPositions.TryAdd(nextPosition, currentDirection);
			}
			previous = nextPosition;

			nextPosition = GetNextPosition(nextPosition, currentDirection);

			if (!IsValidPosition(nextPosition.Item1, nextPosition.Item2))
			{
				reachedEdge = true;
			}
		}
		return $"{seenPositions.Count}";
	}

	private string ProcessInput2()
	{
		ConcurrentBag<int> result = new ConcurrentBag<int>();
		seenPositions.AsParallel().ForAll(position =>
		{
			if (IsLoop(startingPosition, CompassDirection.N, position.Key))
			{
				result.Add(1);
			}
		});

		return $"{result.Sum()}";
	}

	private bool IsLoop((int, int) start, CompassDirection lastdir, (int x, int y) blocked)
    {
        var visited = new Dictionary<(int, int), int>();
        var currentPosition = start;
        var currentDirection = lastdir;
        var previous = currentPosition;

        while (true)
        {
            if (map[currentPosition.Item1][currentPosition.Item2] == '#' || 
				(currentPosition.Item1 == blocked.x && currentPosition.Item2 == blocked.y && blocked.y > -1 && blocked.x > -1))
            {
                if(!visited.TryAdd(currentPosition, 1))
                {
                    if (visited[currentPosition] > 2) return true;
                    visited[currentPosition]++;
                }

                currentPosition = previous;
                currentDirection = currentDirection.TurnClockwise();
            }
            
            previous = currentPosition;

            currentPosition = GetNextPosition(currentPosition, currentDirection);

            if (!IsValidPosition(currentPosition.Item1, currentPosition.Item2))
            {
                return false;
            }
        }
    }

    private static (int, int) GetNextPosition((int, int) currentPosition, CompassDirection currentDirection)
    {
        return currentDirection switch
        {
            CompassDirection.N => (currentPosition.Item1 - 1, currentPosition.Item2),
            CompassDirection.E => (currentPosition.Item1, currentPosition.Item2 + 1),
            CompassDirection.S => (currentPosition.Item1 + 1, currentPosition.Item2),
            CompassDirection.W => (currentPosition.Item1, currentPosition.Item2 - 1),
        };
    }

    private bool IsValidPosition(int x, int y)
    {
        return x > -1 && x < map.Length && y > -1 && y < map[x].Length;
    }
}
