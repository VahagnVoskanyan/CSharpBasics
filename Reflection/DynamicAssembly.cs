using System.Reflection;

namespace CSharpBasics.Reflection
{
    internal class DynamicAssembly
    {
        public void Run()
        {
            Assembly asm = Assembly.LoadFrom("NotficationLib.dll");

            Console.WriteLine(asm.FullName);
            // Get all types from NotficationLib.dll
            Type[] types = asm.GetTypes();
            foreach (Type t in types)
            {
                Console.WriteLine(t.Name);

                foreach (var item in t.GetMethods())
                {
                    Console.WriteLine("Method: " + item.Name);
                }
            }

            // Create instance of EmailNotification class
            Type? notType = asm.GetType("NotficationLib.NotificationService");
            if (notType is not null)
            {
                MethodInfo? secondMethod = notType.GetMethod("SecondMethod", BindingFlags.NonPublic | BindingFlags.Instance);
                
                object? notTypeInstance = Activator.CreateInstance(notType);

                // If static method, pass null as first parameter
                secondMethod?.Invoke(notTypeInstance, null);

                MethodInfo? sendEmail = notType.GetMethod("SendEmail", BindingFlags.Public | BindingFlags.Instance);

                sendEmail?.Invoke(notTypeInstance, ["Bob", "IT", "Hello Team!!"]);
            }
            else Console.WriteLine("Error: no NotficationLib.NotificationService type exists");
        }
    }
}
