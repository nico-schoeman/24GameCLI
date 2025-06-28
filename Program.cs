using System.CommandLine;

class Program
{
    const string GameDescription = """
    24 Game
    Given 4 numbers, this program will try to find all possible expressions to make 24.
    """;

    static int Main(string[] args)
    {
        Argument<double[]> inputNumbersArgument = new("Input Numbers")
        {
            Description = "4 input numbers used to generate expressions to make 24."
        };

        // Validate that the user has given 4 inputs
        inputNumbersArgument.Validators.Add(result =>
        {
            double[] values = result.GetValueOrDefault<double[]>();
            
            if (values.Length != 4) 
            {
                result.AddError($"Please provide 4 numbers");
            }
        });

        RootCommand rootCommand = new(GameDescription);
        rootCommand.Arguments.Add(inputNumbersArgument);

        rootCommand.SetAction(parseResult =>
        {
            double[] inputNumbers = parseResult.GetValue(inputNumbersArgument)!;

            new Game(inputNumbers);
            
            return 0;
        });

        ParseResult parseResult = rootCommand.Parse(args);
        return parseResult.Invoke();
    }
}