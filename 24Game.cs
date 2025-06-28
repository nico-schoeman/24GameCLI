class Game
{
    public Game(double[] inputNumbers) 
    {
        List<string> results = new();

        GenerateNumberPermutations(inputNumbers);
    }
    
    private void GenerateNumberPermutations(double[] numbers)
    {
        int count = 0;
        
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
                Console.WriteLine($"{count++}. {String.Join(',', numbers)}");
            }
            
            for(int i = startIndex; i < numbers.Length; i++) 
            {
                Swap(startIndex, i);
                RecursiveFind(startIndex + 1, results);
                
                // Restore original order for next iteration
                Swap(startIndex, i);
            }
        }

        HashSet<double[]> results = new();
        RecursiveFind(0, results);
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