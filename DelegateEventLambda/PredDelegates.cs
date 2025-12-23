namespace CSharpBasics.DelegateEventLambda
{
    internal class PredDelegates
    {
        // Already defined in System namespace
        //public delegate void Action()
        //public delegate void Action<in T>(T obj)
        //public delegate bool Predicate<in T>(T obj)
        //public delegate TResult Func<out TResult>()
        //public delegate TResult Func<in T, out TResult>(T arg)

        void DoOperation(int a, int b, Action<int, int> op) => op(a, b);

        void Add(int x, int y) => Console.WriteLine($"{x} + {y} = {x + y}");
        void Multiply(int x, int y) => Console.WriteLine($"{x} * {y} = {x * y}");

        private readonly Predicate<int> isPositive = x => x > 0;

        // Func<int, int> Last parameter is the return type
        int DoOperation(int n, Func<int, int> operation) => operation(n);
        int DoubleNumber(int n) => 2 * n;
        int SquareNumber(int n) => n * n;

        public void Run()
        {
            Console.WriteLine("--> Action delegate \n");
            DoOperation(10, 6, Add);        // 10 + 6 = 16
            DoOperation(10, 6, Multiply);   // 10 * 6 = 60

            Console.WriteLine("--> Predicate delegate \n");
            Console.WriteLine(isPositive(20));
            Console.WriteLine(isPositive(-20));

            Console.WriteLine("--> Func delegate \n");
            int result = DoOperation(6, DoubleNumber); // 12
            Console.WriteLine(result);
            result = DoOperation(6, SquareNumber); // 36
            Console.WriteLine(result);
        }
    }
}
