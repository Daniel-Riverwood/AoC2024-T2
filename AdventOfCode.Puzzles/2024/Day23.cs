using System.Collections.Concurrent;
using Utilities;

namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 23, CodeType.Original)]
public class Day23 : IPuzzle
{
	private Dictionary<string, List<string>> Links = new Dictionary<string, List<string>>();
	public (string, string) Solve(PuzzleInput input)
	{
		foreach (var line in input.Lines)
		{
			var split = line.Split('-');
			if (!Links.TryAdd(split[0], new List<string>() { split[1] }))
			{
				Links[split[0]].Add(split[1]);
			}

			if (!Links.TryAdd(split[1], new List<string>() { split[0] }))
			{
				Links[split[1]].Add(split[0]);
			}
		}

		var part1 = ProcessInput1();

		var part2 = ProcessInput2();

		return (part1, part2);
	}

    private string ProcessInput1()
    {
        long sum = 0;
        foreach (var firstcon in Links)
        {
            if (firstcon.Value.Count >= 2)
            {
                foreach(var secondcon in firstcon.Value)
                {
                    foreach(var subset in Links[secondcon])
                    {
                        if (subset == firstcon.Key) continue;
                        if (Links[subset].Contains(firstcon.Key))
                        {
                            if(firstcon.Key.StartsWith('t') || secondcon.StartsWith('t') || subset.StartsWith('t'))
                            {
                                sum++;
                            }
                        }
                    }
                }

            }
        }
        return $"{sum/6}";
    }

    private string ProcessInput2()
    {
        var max = 0;
        var maxcomb = new HashSet<string>();
        foreach(var link in Links)
        {
            link.Value.Add(link.Key);
            var comb = new HashSet<string>(link.Value);

            foreach(var val in link.Value)
            {
                if (!comb.Contains(val)) continue;

                var temp = new HashSet<string>(Links[val]);
                temp.Add(val);

                var intersect = comb.Intersect(temp).ToList();
                intersect.Sort();

                comb = new HashSet<string>(intersect);
            }

            if(comb.Count > max)
            {
                max = comb.Count;
                maxcomb = new HashSet<string>(comb);
            }
        }
        return $"{string.Join(',', maxcomb)}";
    }
}
