using System.Reflection;

namespace CSharpBasics.Reflection
{
    internal class SystemTypeClass
    {
        public void Run()
        {
            Type type1 = typeof(int);
            Type? type2 = Type.GetType("System.String");
            Type type3 = 42.GetType();
            Console.WriteLine($"Type1: {type1}, FullName: {type1.FullName}");
            Console.WriteLine($"Type2: {type2}, FullName: {type2?.FullName}");
            Console.WriteLine($"Type3: {type3}, FullName: {type3.FullName}");

            Console.WriteLine("--> Custom Type");

            Type myType = typeof(PeopleTypes.Person);

            Console.WriteLine($"Name: {myType.Name}");              
            Console.WriteLine($"Full Name: {myType.FullName}");     
            Console.WriteLine($"Namespace: {myType.Namespace}");    
            Console.WriteLine($"Is struct: {myType.IsValueType}");  
            Console.WriteLine($"Is class: {myType.IsClass}");
            Console.WriteLine("Implemented Interfaces");
            foreach (Type i in myType.GetInterfaces())
            {
                Console.WriteLine(i.Name);
            }

            Console.WriteLine("Type of Type: " + myType.GetInterfaces().FirstOrDefault()!.GetType().GetType());

            Console.WriteLine("--> Getting Member Info");
            foreach (MemberInfo member in myType.GetMembers())
            {
                // Here we not see private members
                Console.WriteLine($"{member.DeclaringType} - {member.MemberType} - {member.Name}");
            }
            Console.WriteLine();
            foreach (MemberInfo member in myType.GetMembers(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                Console.WriteLine($"{member.DeclaringType} - {member.MemberType} - {member.Name}");
            }
            Console.WriteLine();
            MemberInfo[] print = myType.GetMember("Print", BindingFlags.Instance | BindingFlags.Public); // Bindings are optional
            foreach (MemberInfo member in print)
            {
                Console.WriteLine($"{member.MemberType} - {member.Name}");
            }

            Console.WriteLine("--> Getting member Types");
        }
    }
}

namespace PeopleTypes
{
    class Person : IEater, IMovable
    {
        string? Name;
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public void Eat() => Console.WriteLine($"{Name} eats");

        public void Move() => Console.WriteLine($"{Name} moves");

        public void Print() => Console.WriteLine($"Name: {Name} Age: {Age}");
    }

    interface IEater
    {
        void Eat();
    }
    interface IMovable
    {
        void Move();
    }
}
