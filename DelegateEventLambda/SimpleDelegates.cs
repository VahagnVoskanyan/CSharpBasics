namespace CSharpBasics.DelegateEventLambda
{
    delegate void MessageOut(); // outside class delegate

    internal class SimpleDelegates
    {
        delegate void Message();

        delegate void SomeDel1(int a, double b);
        delegate void SomeDel2(ref int a, double b);
        delegate int SomeDel3(int a, int b);
        delegate T GenDel<T, K>(K val); // Generic

        void Hello() => Console.WriteLine("Hello Delegate");

        void SomeMethod1(int g, double n) { Console.WriteLine(g + n); }
        void SomeMethod2(ref int g, double n) { Console.WriteLine(g + n); }

        int AddM(int x, int y) => x + y;
        int SubtractM(int x, int y) => x - y;
        int MultiplyM(int x, int y) => x * y;
        decimal SquareM(int x) => x * x;
        int DoubleM(int x) => x + x;

        void DelAsParam(int a, int b, SomeDel3 del)
        {
            Console.WriteLine(del(a, b));
        }

        // returns Delegate
        SomeDel3 SelectOperation(OperationType opType)
        {
            return opType switch
            {
                OperationType.AddM => AddM,
                OperationType.SubtractM => SubtractM,
                _ => MultiplyM,
            };
        }

        void PrintSimpleMessage(string message) => Console.WriteLine(message);
        void PrintAddedMessage(string message) => Console.WriteLine(message + " Some addition");
        void PrintMessageLenght(string message) => Console.WriteLine(message.Length.ToString());

        enum OperationType
        {
            AddM, SubtractM, MultiplyM
        }

        public void Run()
        {
            Message message = Hello; // Hello has same return type and parameters as Message delegate
            Message message1 = new(Hello); // same as above
            Console.WriteLine("--> one delegate:");
            message();
            Console.WriteLine("--> one delegate:");
            message1();

            message = new OtherClass().Display;
            Console.WriteLine("--> method from other class: ");
            message();
            message += Hello; // Adds in invocation list
                              // Each time it's creating a new delegate instance when we use +=
            message += Hello; // Now Hello() method will be called twice
            Console.WriteLine("--> same method twice");
            message();

            Message message2 = message + message1;
            Console.WriteLine("--> unified delegate: ");
            message2();

            MessageOut messageDel = Hello;
            Console.WriteLine("--> delegate declared outside of method: ");
            messageDel();

            SomeDel1 someDel1 = SomeMethod1;
            Console.WriteLine("--> delegate with parameters");
            someDel1(5, 3.14); // 8.14

            //someDel1 = SomeMethod2; // Not allowed because ref,in,out parameters are not supported in delegate SomeDel1
            SomeDel2? someDel2 = SomeMethod2;
            int x = 4;
            Console.WriteLine("--> delegate with ref parameter");
            someDel2(ref x, 6); // 10
            someDel2 -= SomeMethod2; // Removes from invocation list
                                     // Each time it's creating a new delegate instance when we use -=
            someDel2?.Invoke(ref x, 6); // If not check null => Exception // won't invoke

            SomeDel3 someDel3 = AddM;
            someDel3 += SubtractM;
            Console.WriteLine("--> Returns last added method return value");
            Console.WriteLine(someDel3(3, 2)); // 1

            GenDel<decimal, int> squareOperation = SquareM;
            Console.WriteLine("--> Generic delegate op1: ");
            Console.WriteLine(squareOperation(5)); // 25
            GenDel<int, int> doubleOperation = DoubleM;
            Console.WriteLine("--> Generic delegate op2: ");
            Console.WriteLine(doubleOperation(5)); // 10

            Console.WriteLine("--> Delegate as param:");
            DelAsParam(3, 2, someDel3); // 1

            Console.WriteLine("--> Delegate as method return type:");
            someDel3 = SelectOperation(OperationType.AddM);
            Console.WriteLine(someDel3(10, 4));    // 14
            someDel3 = SelectOperation(OperationType.SubtractM);
            Console.WriteLine(someDel3(10, 4));    // 6
            someDel3 = SelectOperation(OperationType.MultiplyM);
            Console.WriteLine(someDel3(10, 4));    // 40

            Account account = new(300);
            Console.WriteLine("--> Passing simple print");
            account.RegisterHandler(PrintSimpleMessage);
            account.Take(100);
            account.Take(250);
            Console.WriteLine("--> Passing added print");
            account.RegisterHandler(PrintAddedMessage);
            account.Take(100);
            account.Take(250);
            Console.WriteLine("--> Passing lenght print");
            account.RegisterHandler(PrintMessageLenght);
            account.Take(100);
            account.Take(250);
        }
    }

    public delegate void AccountHandler(string message);
    public class Account
    {
        int sum;
        AccountHandler? taken;

        public Account(int sum) => this.sum = sum;

        // Register the delegate
        public void RegisterHandler(AccountHandler del)
        {
            taken = del;
        }

        public void Add(int money) => sum += money;
        public void Take(int money)
        {
            if (sum >= money)
            {
                sum -= money;
                
                taken?.Invoke($"Balance decreased: {money} $");
            }
            else
            {
                taken?.Invoke($"Not much money: {sum} $");
            }
        }
    }

    public class OtherClass
    {
        public void Display() => Console.WriteLine("Calls other class method");
    }
}
