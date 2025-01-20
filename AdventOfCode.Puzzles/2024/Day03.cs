using System.Text.RegularExpressions;
namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 03, CodeType.Original)]
public class Day03 : IPuzzle
{
    private readonly static Regex mulReg = new Regex(@"(mul\(\d{1,3},\d{1,3}\))");
    private readonly static Regex commands = new Regex(@"(mul\(\d{1,3},\d{1,3}\)|do\(\)|don\'t\(\))");
    private MatchCollection matches;
    private MatchCollection commandOrder;

	public (string, string) Solve(PuzzleInput input)
	{
		matches = mulReg.Matches(input.Text);
		commandOrder = commands.Matches(input.Text);
		var part1 = ProcessInput1(input.Text);

		var part2 = ProcessInput2(input.Text);

		return (part1, part2);
	}

    private string ProcessInput1(string input)
    {
        int sum = 0;
        for(int i = 0; i < matches.Count; i++)
        {
            var pattern = matches[i].Value;
            var split = pattern.Split(',');
            var val1 = int.Parse(split[0].Replace("mul(", ""));
            var val2 = int.Parse(split[1].Trim(')'));
            sum += val1 * val2;
        }
        return $"{sum}";
    }

    private string ProcessInput2(string input)
    {
        int sum = 0; 
        Match previousCommand = null;
        for(int i = 0; i < commandOrder.Count; i++)
        {
            var pattern = commandOrder[i].Value;
            bool canCompute = true;
            bool isMul = false;
            if(previousCommand != null && previousCommand.Value == "don't()") canCompute = false;

            if (!pattern.Contains("mul(")) previousCommand = commandOrder[i];
            else isMul = true;

            if (canCompute && isMul)
            {
                var split = pattern.Split(',');
                var val1 = int.Parse(split[0].Replace("mul(", ""));
                var val2 = int.Parse(split[1].Trim(')'));
                sum += val1 * val2;
            }
        }
        return $"{sum}";
    }
}
