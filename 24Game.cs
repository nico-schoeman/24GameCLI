class Game
{
    public Game(double[] inputNumbers) 
    {
        List<string> results = new();

        GenerateNumberPermutations(inputNumbers);
        GenerateOperationSignPermutations();
    }
    
    private void GenerateNumberPermutations(double[] numbers)
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
                results.Add(numbers);
            }
            
            for(int i = startIndex; i < numbers.Length; i++) 
            {
                Swap(startIndex, i);
                RecursiveFind(startIndex + 1, results);
                
                // Restore original order for the next iteration
                Swap(startIndex, i);
            }
        }

        HashSet<double[]> results = new();
        RecursiveFind(0, results);
    }
    
    private void GenerateOperationSignPermutations()
    {
        char[] operations = new[] { '+', '-', '*', '/' };
        HashSet<char[]> results = new();
        
        foreach (char firstOperation in operations)
        {
            foreach (char secondOperation in operations)
            {
                foreach (char thirdOperation in operations)
                {
                    results.Add(new [] { firstOperation, secondOperation, thirdOperation });
                }
            }
        }
    }
    
    private List<string> GenerateExpressions(double[] numbers, char[] operations) 
    {
        return new List<string>
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