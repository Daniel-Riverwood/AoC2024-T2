using Utilities;

namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 09, CodeType.Original)]
public class Day09 : IPuzzle
{
	int[] fullIterations = [];
	List<(int id, int index, int length)> reserved = [];
	List<(int index, int length)> freeSpace = [];

	public (string, string) Solve(PuzzleInput input)
	{
		fullIterations = new int[input.Text.ToIntList().Sum()];

		var ind = 0;
		var curblock = -1;
		for (var x = 0; x < input.Text.Length; x++)
		{
			int c = input.Text[x] - '0';
			bool space = x % 2 == 1;

			if (!space)
			{
				curblock++;
				reserved.Add((curblock, ind, c));
			}
			else if (c != 0)
			{
				freeSpace.Add((ind, c));
			}

			for (int y = 0; y < c; y++)
			{
				fullIterations[ind++] = space ? -1 : curblock;
			}
		}

		var part1 = ProcessInput1();

		var part2 = ProcessInput2();

		return (part1, part2);
	}

    private string ProcessInput1()
    {
        var fileList = new List<int>(fullIterations);
        int start = 0;
        int end = fullIterations.Length - 1;
        long sum = 0;

        while (start < end)
        {
            if (fileList[start] != -1)
            {
                start++;
                continue;
            }

            while (fileList[end] == -1) end--;
            fileList[start++] = fileList[end];
            fileList[end--] = -1;
        }

        for (var x = 0; x < fileList.Count; x++)
        {
            if (fileList[x] == -1) break;
            sum += (x * fileList[x]);
        }

        return $"{sum}";
    }

    private string ProcessInput2()
    {
        var fileList = new List<int>(fullIterations);
        long sum = 0;
        for(int z = reserved.Count -1; z >= 0; z--)
        {
            var taken = reserved[z];
            for(int y = 0; y < freeSpace.Count; y++)
            {
                var free = freeSpace[y];
                if (free.index >= taken.index) break;
                if (free.length < taken.length) continue;
            
                for (var x = 0; x < taken.length; x++)
                {
                    fileList[free.index + x] = taken.id;
                    fileList[taken.index + x] = -1;
                }

                if (free.length == taken.length)
                    freeSpace.Remove(free);
                else
                    freeSpace[freeSpace.IndexOf(free)] = (free.index + taken.length, free.length - taken.length);
                break;
            }
        }

        for (var x = 0; x < fileList.Count; x++)
        {
            if (fileList[x] == -1) continue;

            sum += (x * fileList[x]);
        }
        return $"{sum}";
    }
}
