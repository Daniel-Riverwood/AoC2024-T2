using System.Collections.Concurrent;
using Utilities;

namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 25, CodeType.Original)]
public class Day25 : IPuzzle
{
	private List<List<int>> keys = new List<List<int>>();
	private List<List<int>> locks = new List<List<int>>();
	public (string, string) Solve(PuzzleInput input)
	{
		var splits = input.Text.SplitByDoubleNewline();
		foreach (var split in splits)
		{
			var keynum = new List<int>();
			var keysplit = split.SplitIntoColumns();
			if (split.StartsWith('#'))
			{
				foreach (var key in keysplit)
				{
					var col = key.Count(q => q == '#') - 1;
					keynum.Add(col);
				}
				locks.Add(keynum);
			}
			else
			{
				foreach (var key in keysplit)
				{
					var col = key.Count(q => q == '#') - 1;
					keynum.Add(col);
				}
				keys.Add(keynum);
			}
		}

		var part1 = ProcessInput1();

		var part2 = ProcessInput2();

		return (part1, part2);
	}

    private string ProcessInput1()
    {
        long sum = 0;
        foreach(var loc in locks)
        {
            foreach(var key in keys)
            {
                var match = true;
                for(var i = 0; i < key.Count(); i++)
                {
                    var tot = loc[i] + key[i];
                    if(tot > 5)
                    {
                        match = false;
                        break;
                    }
                }

                if(match)
                {
                    sum++;
                }
            }
        }
        return $"{sum}";
    }
    private string ProcessInput2()
    {
        long sum = 50;
        return $"{sum}";
    }
}
