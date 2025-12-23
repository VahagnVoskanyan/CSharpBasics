using System.ComponentModel.DataAnnotations;

namespace CSharpBasics.Reflection
{
    internal class AttributeReflection
    {
        public void Run()
        {
            PersonA tom = new("Tom", 35);
            PersonA bob = new("Bob", 16);
            bool tomIsValid = ValidateUser(tom);    // true
            bool bobIsValid = ValidateUser(bob);    // false

            Console.WriteLine($"Tom : {tomIsValid}");
            Console.WriteLine($"Bob: {bobIsValid}");
        }

        bool ValidateUser(PersonA person)
        {
            Type type = typeof(PersonA);
            // Get all attribute of class Person
            object[] attributes = type.GetCustomAttributes(false);

            foreach (Attribute attr in attributes)
            {
                
                if (attr is AgeValidationAttribute ageAttribute)
                {
                    return ageAttribute.IsValid(person.Age);
                }
            }
            return true;
        }
    }

    [AgeValidation(18)]
    public class PersonA
    {
        public string Name { get; }
        public int Age { get; set; }
        public PersonA(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    class AgeValidationAttribute : ValidationAttribute
    {
        public int Age { get; }
        public AgeValidationAttribute() { }
        public AgeValidationAttribute(int age) => Age = age;

        public override bool IsValid(object? value)
        {
            if (value is int age)
            {
                return age > Age;
            }
            return base.IsValid(value);
        }
    }
}
