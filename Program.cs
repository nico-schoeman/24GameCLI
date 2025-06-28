using System.CommandLine;

class Program
{
    static int Main(string[] args)
    {
        Argument<int[]> numbers = new("Input Numbers")
        {
            Description = "The input numbers to generate a equasion for to equal 24.",
            Arity = new ArgumentArity(4, 4)
        };

        RootCommand rootCommand = new("24 Game");
        rootCommand.Arguments.Add(numbers);

        rootCommand.SetAction(parseResult =>
        {
            int[] result = parseResult.GetValue(numbers);
            Console.WriteLine($"{String.Join(",", result)}");
            return 0;
        });

        ParseResult parseResult = rootCommand.Parse(args);
        return parseResult.Invoke();
    }
}