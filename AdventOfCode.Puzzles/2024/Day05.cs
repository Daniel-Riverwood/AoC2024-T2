using SuperLinq;
using Utilities;

namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 05, CodeType.Original)]
public class Day05 : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		for (var i = 0; i < input.Lines.Length; i++)
		{
			if (input.Lines[i].Contains('|'))
			{
				var split = input.Lines[i].Split('|');
				order.Add((int.Parse(split[0]), int.Parse(split[1])));
			}
			else if (input.Lines[i].Contains(","))
			{
				var split = input.Lines[i].ToIntList(",");
				inputs.Add(split);
			}
		}

		var part1 = ProcessInput1(input.Lines);

		var part2 = ProcessInput2(input.Lines);

		return (part1, part2);
	}

    private List<(int, int)> order = new List<(int, int)>();
    private List<List<int>> inputs = new List<List<int>>();
    private List<List<int>> invalids = new List<List<int>>();

    private string ProcessInput1(string[] input)
    {
        int sum = 0;

        for(var x = 0; x < inputs.Count; x++)
        {
            var isValid = true;
            for(var y = 0; y < inputs[x].Count; y++)
            {
                var checkval = inputs[x][y];
                var hasChecks = order.FindAll(q => q.Item2 == checkval);
                foreach(var check in hasChecks)
                {
                    if (inputs[x].IndexOf(check.Item1) > y)
                    {
                        isValid = false;
                        invalids.Add(inputs[x]);
                        break;
                    }
                }
                if (!isValid) break;
            }
            if(isValid)
            {
                var middle = Math.Ceiling(Convert.ToDecimal(inputs[x].Count / 2));
                sum += inputs[x][Convert.ToInt32(middle)];
            }
        }
        return $"{sum}";
    }
	private string ProcessInput2(string[] input)
	{
		if (invalids.Count == 0)
		{
			ProcessInput1(input);
		}
		int sum = 0;

		foreach (var invalid in invalids)
		{
			bool sorted = false;
			int x = 0;
			while (!sorted)
			{
				bool wasChecked = false;
				var checkval = invalid[x];
				var hasChecks = order.FindAll(q => q.Item2 == checkval);
				foreach (var check in hasChecks)
				{
					var checkind = invalid.IndexOf(check.Item1);
					if (checkind > x)
					{
						wasChecked = true;
						invalid[x] = invalid[x] + invalid[checkind];
						invalid[checkind] = invalid[x] - invalid[checkind];
						invalid[x] = invalid[x] - invalid[checkind];
					}
				}

				if (x == invalid.Count / 2 + 1) sorted = true;
				else x = wasChecked ? x : x + 1;
				wasChecked = false;
			}
			var middle = invalid.Count / 2;
			sum += invalid[middle];
		}
		return $"{sum}";
	}
}
