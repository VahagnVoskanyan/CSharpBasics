namespace CSharpBasics.OOP
{
    internal class Interfaces
    {
        public static void Run()
        {
            IMyInterface1 myInterface1 = new MyInterfaceImpl();
            Console.WriteLine("IMyInterface1 myInterface1 Created");
            myInterface1.Method1();
            // All calls go to interface default implementations
            myInterface1.IntImplMethod1(); // Here we have static constructor called once
            myInterface1.IntImplMethod2();
            myInterface1.IntImplMethod3();

            Console.WriteLine();
            // This is faster since no interface dispatching
            MyInterfaceImpl myInterface2 = new MyInterfaceImpl();
            Console.WriteLine("MyInterfaceImpl myInterface2 Created");
            myInterface2.Method1();
            //myInterface2.IntImplMethod1(); // Doesn't exist here
            myInterface2.IntImplMethod2();
            myInterface2.IntImplMethod3();

            /*IMyInterface2 myInterface2 = new MyInterfaceImpl();
            myInterface2.InterfaceMethod2();
            // Can cast to either interface
            IMyInterface1 myInterface1From2 = (IMyInterface1)myInterface2;
            myInterface1From2.InterfaceMethod1();
            IMyInterface2 myInterface2From1 = (IMyInterface2)myInterface1;
            myInterface2From1.InterfaceMethod2();*/

            Console.WriteLine();
        }
    }

    interface IMyInterface1
    {
        //public int age = 10; // fields are not allowed in interfaces
        public int MyProperty { get; set; }
        public static int StatProp { get; set; }
        // Static Abstract
        public static abstract int StatAbsProp { get; set; }

        static IMyInterface1() // C# 11+ // Static constructor
        {
            StatProp = 15;
            Console.WriteLine("IMyInterface1 Static Constructor call");
        }

        // Static Abstract
        public static abstract int StatAbsMet();

        void Method1();

        void IntImplMethod1() // C# 8+ // Doesn't exist in MyInterfaceImpl
        {
            Console.WriteLine("IMyInterface1: IntImplMethod1");
        }

        void IntImplMethod2() // Exists in MyInterfaceImpl
        {
            Console.WriteLine("IMyInterface1: IntImplMethod2");
        }

        void IntImplMethod3() // Exists in MyInterfaceImpl with 'new' keyword
        {
            Console.WriteLine("IMyInterface1: IntImplMethod3");
        }
    }

    public class MyInterfaceImpl : IMyInterface1
    {
        public int MyProperty { get; set; }
        public static int StatAbsProp { get; set; }

        public static int StatAbsMet() { return 1; }

        public void Method1()
        {
            Console.WriteLine("MyImpl: Method1");
        }

        public void IntImplMethod2()
        {
            Console.WriteLine("MyImpl: IntImplMethod2");
        }

        public new void IntImplMethod3() // the 'new' keyword isn't required here
        {
            Console.WriteLine("MyImpl: IntImplMethod3");
        }
    }
}
