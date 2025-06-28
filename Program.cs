using System.CommandLine;

class Program
{
    static int Main(string[] args)
    {
        Argument<double[]> numbers = new("Input Numbers")
        {
            Description = "4 input numbers to generate a expression to equal 24."
        };

        // Validate that the user has given 4 inputs
        numbers.Validators.Add(result =>
        {
            double[] values = result.GetValueOrDefault<double[]>();
            
            if (values.Length != 4) 
            {
                result.AddError($"Please provide 4 numbers");
            }
        });

        RootCommand rootCommand = new("24 Game");
        rootCommand.Arguments.Add(numbers);

        rootCommand.SetAction(parseResult =>
        {
            double[] result = parseResult.GetValue(numbers)!;
            Console.WriteLine($"Input: {String.Join(",", result)}");
            return 0;
        });

        ParseResult parseResult = rootCommand.Parse(args);
        return parseResult.Invoke();
    }
}