namespace CSharpBasics.DelegateEventLambda
{
    // Types of Lambda Expression
    // 1. Expression Lambda ((int num) => num * 5;)
    // 2. Statement Lambda ((int a, int b) => { var sum = a + b; return sum; };)

    internal class LambdaExpressions
    {
        public static void Run()
        {
            // here square is Func<int, int>
            var square = (int num) => num * num; // Expression Lambda
            Console.WriteLine("Square of number: " + square(5));

            Func<int, int, int> resultingSum = (int a, int b) => // Statement Lambda
            {
                int calculatedSum = a + b;
                return calculatedSum;
            };

            Console.WriteLine("Total sum: " + resultingSum(5, 6));

            int[] numbers = { 2, 13, 1, 4, 13, 5 };

            // lambda expression as method parameter
            int totalCount = numbers.Count(x => x == 13);

            Console.WriteLine("Total number of 13: " + totalCount);
        }


    }
}
