using System.Reflection;

namespace CSharpBasics.Reflection
{
    internal class FieldPropReflection
    {
        public void Run()
        {
            Type myType = typeof(Person);

            Console.WriteLine("Fields:");
            foreach (FieldInfo field in myType.GetFields(
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
            {
                string modificator = "";

                // getting field access modifier
                if (field.IsPublic)
                    modificator += "public ";
                else if (field.IsPrivate)
                    modificator += "private ";
                else if (field.IsAssembly)
                    modificator += "internal ";
                else if (field.IsFamily)
                    modificator += "protected ";
                else if (field.IsFamilyAndAssembly)
                    modificator += "private protected ";
                else if (field.IsFamilyOrAssembly)
                    modificator += "protected internal ";

                if (field.IsStatic) modificator += "static ";

                Console.WriteLine($"{modificator}{field.FieldType.Name} {field.Name}");
            }
            Console.WriteLine();

            // Change private field value
            var name = myType.GetField("name", BindingFlags.Instance | BindingFlags.NonPublic);

            Person tom = new("Tom", 37);
            var value = name?.GetValue(tom);
            Console.WriteLine($"Got Value of name: {value}");
            Console.Write("First Value: ");
            tom.Print(); // Tom - 37

            name?.SetValue(tom, "Bob");
            Console.Write("Changed Value: ");
            tom.Print(); // Bob - 37
            Console.WriteLine();

            Console.WriteLine("Properties:");
            foreach (PropertyInfo prop in myType.GetProperties(BindingFlags.Instance |
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
            {
                Console.Write($"{prop.PropertyType} {prop.Name} {{");

                // if property is readable
                if (prop.CanRead) Console.Write("get; ");
                // if property is writable
                if (prop.CanWrite) Console.Write("set;");
                Console.WriteLine("}");
            }
            Console.WriteLine();

            // Change property value
        }
    }

    class Person
    {
        static int minAge = 0;
        string name;
        int age;
        public string NameProp { get; } = "Default Name";
        public int AgeProp { get; set; } = 25;

        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }
        public void Print() => Console.WriteLine($"{name} - {age}");
    }
}
