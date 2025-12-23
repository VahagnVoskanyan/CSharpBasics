using System.Reflection;

namespace CSharpBasics.Reflection
{
    internal class MethodReflection
    {
        public void Run()
        {
            Type myType = typeof(Printer);

            Console.WriteLine("--> Getting Methods Info");
            // Get all public methods
            foreach (MethodInfo method in myType.GetMethods())
            {
                string modificator = "";
                
                if (method.IsStatic) modificator += "static ";
                
                if (method.IsVirtual) modificator += "virtual ";

                Console.WriteLine($"{modificator}{method.ReturnType.Name} {method.Name} ()");
            }

            Console.WriteLine("--> Invoke Message");
            var myPrinter = new Printer();
            var type = myPrinter.GetType();
            var methodI = type.GetMethod("PrintMessage");
            methodI?.Invoke(myPrinter, parameters: [ "messgae", 2 ] );
        }
    }

    class Printer
    {
        public string DefaultMessage { get; set; } = "Hello";
        public void PrintMessage(string message, int times = 1)
        {
            while (times-- > 0) Console.WriteLine(message);
        }
        public string CreateMessage() => DefaultMessage;
    }
}
