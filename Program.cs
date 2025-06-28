using System.CommandLine;

class Program
{
    static int Main(string[] args)
    {
        Argument<double[]> inputNumbersArgument = new("Input Numbers")
        {
            Description = "4 input numbers to generate a expression to equal 24."
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

        RootCommand rootCommand = new("24 Game");
        rootCommand.Arguments.Add(inputNumbersArgument);

        rootCommand.SetAction(parseResult =>
        {
            double[] inputNumbers = parseResult.GetValue(inputNumbersArgument)!;
            Console.WriteLine($"Input: {String.Join(',', inputNumbers)}");

            new Game(inputNumbers);
            
            return 0;
        });

        ParseResult parseResult = rootCommand.Parse(args);
        return parseResult.Invoke();
    }
}