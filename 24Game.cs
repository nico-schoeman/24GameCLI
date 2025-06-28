using NCalc;

class Game
{
    /// <summary>
    /// Represents an instance of the 24 game.
    /// Given 4 numbers it will try to find and list all possible solutions to make 24
    /// </summary>
    public Game(double[] inputNumbers)
    {
        Console.WriteLine($"Input: {String.Join(',', inputNumbers)}" + Environment.NewLine);
        
        HashSet<string> solutions = new();

        HashSet<double[]> numberPermutations = GenerateNumberPermutations(inputNumbers);
        HashSet<char[]> operationPermutations = GenerateOperationSignPermutations();
        
        CalculateSolutions(solutions, numberPermutations, operationPermutations);

        PrintSolutions(solutions);
    }

    private void CalculateSolutions(HashSet<string> solutions, HashSet<double[]> numberPermutations, HashSet<char[]> operationPermutations)
    {
        foreach (double[] numberSet in numberPermutations)
        {
            foreach (char[] operatorSet in operationPermutations)
            {
                HashSet<string> expressions = GenerateExpressions(numberSet, operatorSet);

                foreach (string expression in expressions)
                {
                    double result = Convert.ToDouble(new Expression(expression).Evaluate());
                    if (result == 24)
                    {
                        solutions.Add(expression);
                    }
                }
            }
        }
    }

    private void PrintSolutions(HashSet<string> solutions)
    {
        if (solutions.Count == 0)
        {
            Console.WriteLine("No solution");
        }
        else
        {
            Console.WriteLine($"{solutions.Count} solutions found" + Environment.NewLine);
            Console.WriteLine("These Expressions result in 24:");
            foreach (string solution in solutions)
            {
                Console.WriteLine(solution);
            }
        }
    }

    private HashSet<double[]> GenerateNumberPermutations(double[] numbers)
    {
        void Swap(int index1, int index2)
        {
            double temp = numbers[index1];
            numbers[index1] = numbers[index2];
            numbers[index2] = temp;
        }

        void RecursiveFind(int startIndex, HashSet<double[]> results)
        {
            if (startIndex == numbers.Length)
            {
                results.Add(new[] { numbers[0], numbers[1], numbers[2], numbers[3] });
            }

            for (int i = startIndex; i < numbers.Length; i++)
            {
                Swap(startIndex, i);
                RecursiveFind(startIndex + 1, results);

                // Restore original order for the next iteration
                Swap(startIndex, i);
            }
        }

        HashSet<double[]> results = new();
        RecursiveFind(0, results);

        return results;
    }

    private HashSet<char[]> GenerateOperationSignPermutations()
    {
        char[] operations = new[] { '+', '-', '*', '/' };
        HashSet<char[]> results = new();

        foreach (char firstOperation in operations)
        {
            foreach (char secondOperation in operations)
            {
                foreach (char thirdOperation in operations)
                {
                    results.Add(new[] { firstOperation, secondOperation, thirdOperation });
                }
            }
        }

        return results;
    }

    private HashSet<string> GenerateExpressions(double[] numbers, char[] operations)
    {
        return new HashSet<string>
        {
            $"{numbers[0]}{operations[0]}{numbers[1]}{operations[1]}{numbers[2]}{operations[2]}{numbers[3]}", // x _ x _ x _ x
            $"({numbers[0]}{operations[0]}{numbers[1]}){operations[1]}{numbers[2]}{operations[2]}{numbers[3]}", // (x _ x) _ x _ x
            $"{numbers[0]}{operations[0]}({numbers[1]}{operations[1]}{numbers[2]}){operations[2]}{numbers[3]}", // x _ (x _ x) _ x
            $"{numbers[0]}{operations[0]}{numbers[1]}{operations[1]}({numbers[2]}{operations[2]}{numbers[3]})", // x _ x _ (x _ x)
            $"({numbers[0]}{operations[0]}{numbers[1]}){operations[1]}({numbers[2]}{operations[2]}{numbers[3]})", // (x _ x) _ (x _ x)
            $"({numbers[0]}{operations[0]}{numbers[1]}{operations[1]}{numbers[2]}){operations[2]}{numbers[3]}", // (x _ x _ x) _ x
            $"(({numbers[0]}{operations[0]}{numbers[1]}){operations[1]}{numbers[2]}){operations[2]}{numbers[3]}", // ((x _ x) _ x) _ x
            $"({numbers[0]}{operations[0]}({numbers[1]}{operations[1]}{numbers[2]})){operations[2]}{numbers[3]}", // (x _ (x _ x)) _ x
            $"{numbers[0]}{operations[0]}({numbers[1]}{operations[1]}{numbers[2]}{operations[2]}{numbers[3]})", // x _ (x _ x _ x)
            $"{numbers[0]}{operations[0]}(({numbers[1]}{operations[1]}{numbers[2]}){operations[2]}{numbers[3]})", // x _ ((x _ x) _ x)
            $"{numbers[0]}{operations[0]}({numbers[1]}{operations[1]}({numbers[2]}{operations[2]}{numbers[3]}))", // x _ (x _ (x _ x))
        };
    }
}