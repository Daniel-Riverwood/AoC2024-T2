namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 02, CodeType.Original)]
public class Day02 : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		var part1 = ProcessInput1(input.Lines);

		var part2 = ProcessInput2(input.Lines);

		return (part1, part2);
	}

    private string ProcessInput1(string[] input)
    {
        int sum = 0;
        foreach (var line in input)
        {
            var segment = line.Split(" ");
            bool isAsc = false;
            bool isDesc = false;
            for (var i = 0; i < segment.Length; i++)
            {
                if (i == segment.Length - 1)
                {
                    sum++;
                    break;
                }
                var current = Convert.ToInt32(segment[i]);
                var next = Convert.ToInt32(segment[i + 1]);
                var diff = current - next;
                if (0 < diff && diff < 4)
                {
                    if (isDesc) break;
                    else if (!isAsc && !isDesc) isAsc = true;
                }
                else if (-4 < diff && diff < 0)
                {
                    if (isAsc) break;
                    else if (!isAsc && !isDesc) isDesc = true;
                }
                else break;
            }
        }

        return $"{sum}";
    }

    private string ProcessInput2(string[] input)
    {
        int sum = 0;
        foreach (var line in input)
        {
            var segment = line.Split(" ");
            bool hasDamp = false;
            bool isAsc = false;
            bool isDesc = false;
            for (int i = 0; i < segment.Length; i++)
            {
                if (i == segment.Length - 1)
                {
                    sum++;
                    break;
                }
                var current = Convert.ToInt32(segment[i]);
                var next = Convert.ToInt32(segment[i + 1]);
                var diff = current - next;
                if (0 < diff && diff < 4)
                {
                    if (isDesc) hasDamp = true;
                    else if (!isAsc && !isDesc) isAsc = true;
                }
                else if (-4 < diff && diff < 0)
                {
                    if (isAsc) hasDamp = true;
                    else if (!isAsc && !isDesc) isDesc = true;
                }
                else hasDamp = true;

                if (hasDamp) break;
            }
            
            if(hasDamp)
            {
                for (int x = 0; x < segment.Length; x++)
                {
                    isAsc = false;
                    isDesc = false;
                    bool canSatisfy = false;
                    var newList = new List<string>(segment);
                    newList.RemoveAt(x);
                    for (int i = 0; i < newList.Count; i++)
                    {
                        if (i == newList.Count - 1)
                        {
                            canSatisfy = true;
                            break;
                        }
                        int current = Convert.ToInt32(newList[i]);
                        int next = Convert.ToInt32(newList[i + 1]);
                        int diff = current - next;
                        if (0 < diff && diff < 4)
                        {
                            if (isDesc) break;
                            else if (!isAsc && !isDesc) isAsc = true;
                        }
                        else if (-4 < diff && diff < 0)
                        {
                            if (isAsc) break;
                            else if (!isAsc && !isDesc) isDesc = true;
                        }
                        else break;
                    }

                    if (canSatisfy)
                    {
                        sum++;
                        break;
                    }
                }
            }
        }

        return $"{sum}";
    }
}
