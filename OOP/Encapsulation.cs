namespace CSharpBasics.OOP
{
    public enum BasicAccessModifiers
    {
        Public,
        Private, // only accessed within the same class or struct
        Protected, // accessed from the same class and its derived classes
        Internal // accessed within the same assembly
    }

    public enum OtherAccessModifiers
    {
        Protected_Internal, // accessed from the same assembly and the derived class of the containing class from any other assembly
        Private_Protected // accessed within the same class, and its derived class within the same assembly
    }

    public class MyClass
    {
        // class level variable
        private readonly string str = "Class Level";

        public void Method()
        {
            Console.WriteLine(str);
        }

        public void Display()
        {
            Console.WriteLine(str);

            // method level variable
            var methodVar = "Method Variable\n";
            Console.WriteLine(methodVar);

            // 'i' is block level variable
            for (int i = 0; i < 10; i++) { }
        }

        public static void StaticMethod()
        {
            // Local variable not accessible in static method
            //Console.WriteLine(str);
        }   
    }

    public static class TestClass
    {
        public static void Run()
        {
            var myClass = new MyClass();
            myClass.Display();
        }
    }
}
