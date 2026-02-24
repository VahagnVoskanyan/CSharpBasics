namespace CSharpBasics.Addintional
{
    internal class Comparison
    {
        public static void Run()
        {
            string str1 = "Hello";
            string str2 = "Hello";

            Console.WriteLine(str1.Equals(str2));        // True - compares content
            Console.WriteLine(str1.GetHashCode() == str2.GetHashCode()); // True - same hash

            var dict = new Dictionary<string, int>();
            dict[str1] = 100;
            Console.WriteLine(dict[str2]); // ✅ Works perfectly! Returns 100

            string a = "ab";
            string b = "ab";

            Console.WriteLine(a == b);                    // True - content is same
            Console.WriteLine(Object.ReferenceEquals(a, b)); // True - SAME reference!

            string c = "ab";
            string d = new string(new char[] { 'a', 'b' }); // Force new object

            Console.WriteLine(c == d);                    // True - content is same
            Console.WriteLine(Object.ReferenceEquals(c, d)); // False - DIFFERENT references!

            string a1 = "ab";
            string b1 = "a" + "b";  // Compiler optimizes to "ab" - same reference
            string c1 = "a";
            string d1 = c1 + "b";    // Runtime concatenation - NEW object!

            Console.WriteLine(Object.ReferenceEquals(a1, b1)); // True - compiler optimization
            Console.WriteLine(Object.ReferenceEquals(a1, d1)); // False - runtime creation

            var person1 = new Person { Name = "John" };
            var person2 = new Person { Name = "John" };

            Console.WriteLine("Person Class");
            Console.WriteLine(person1 == person2);                           // False - different Person objects
            Console.WriteLine(Object.ReferenceEquals(person1, person2));     // False - different Person objects

            Console.WriteLine(person1.Name == person2.Name);                 // True - same string content
            Console.WriteLine(Object.ReferenceEquals(person1.Name, person2.Name)); // True - SAME string reference!

            string? input = Console.ReadLine(); // User types "John"
            var person3 = new Person { Name = input ?? "John" }; // Runtime string - NOT in pool

            Console.WriteLine(Object.ReferenceEquals(person1.Name, person3.Name)); // False
        }
    }

    internal class Person
    {
        public string Name { get; set; } = string.Empty;
    }
}
