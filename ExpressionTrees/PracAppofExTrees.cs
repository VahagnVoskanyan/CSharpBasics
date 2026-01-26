using System.Linq.Expressions;

namespace CSharpBasics.ExpressionTrees
{
    internal class PracAppofExTrees
    {
        public static void Run()
        {
            Console.WriteLine("-- Property Mapper Test --\n");

            var person = new Person
            {
                Name = "John Doe",
                Age = 30,
                Email = "john.doe@example.com",
                Phone = "555-1234"
            };

            // Creating mapper
            var mapper = CreateMapper<Person, PersonDto>();

            // Creating target object
            var personDto = new PersonDto();

            // Execute mapping
            mapper(person, personDto);

            Console.WriteLine("Target PersonDto (after mapping):");
            Console.WriteLine($"  Name: {personDto.Name}");
            Console.WriteLine($"  Age: {personDto.Age}");
            Console.WriteLine($"  Email: {personDto.Email}");
            Console.WriteLine();
        }

        // Property Mapper using Expression Trees
        public static Action<TSource, TTarget> CreateMapper<TSource, TTarget>()
        {
            ParameterExpression sourceParam = Expression.Parameter(typeof(TSource), "source");
            ParameterExpression targetParam = Expression.Parameter(typeof(TTarget), "target");

            List<Expression> assignments = [];

            foreach (var targetProp in typeof(TTarget).GetProperties())
            {
                var sourceProp = typeof(TSource).GetProperty(targetProp.Name);
                if (sourceProp != null && sourceProp.PropertyType == targetProp.PropertyType)
                {
                    MemberExpression sourceProperty = Expression.Property(sourceParam, sourceProp);
                    MemberExpression targetProperty = Expression.Property(targetParam, targetProp);

                    BinaryExpression assign  = Expression.Assign(targetProperty, sourceProperty);
                    assignments.Add(assign);
                }
            }

            BlockExpression block = Expression.Block(assignments);
            var lambda = Expression.Lambda<Action<TSource, TTarget>>(block, sourceParam, targetParam);

            return lambda.Compile();
        }
    }

    
    public class Person
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }

    public class PersonDto
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string Email { get; set; } = null!;
    }
}
