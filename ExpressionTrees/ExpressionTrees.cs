using System.Linq.Expressions;

namespace CSharpBasics.ExpressionTrees
{
    internal class ExpressionTrees
    {
        public void Run()
        {
            // Using Lambda Expressions
            Expression<Func<int, bool>> isPositive = num => num > 0;
            Expression<Func<int, int, int>> add = (x, y) => x + y;

            //Building Expression Trees Manually
            ParameterExpression numParam = Expression.Parameter(typeof(int), "num");
            ConstantExpression zero = Expression.Constant(0, typeof(int));
            BinaryExpression greaterThan = Expression.GreaterThan(numParam, zero);
            Expression<Func<int, bool>> isPositiveM = Expression.Lambda<Func<int, bool>>(
                greaterThan,
                new ParameterExpression[] { numParam }
            );

            Console.WriteLine($"NodeType: {add.NodeType}");
            Console.WriteLine($"Return Type: {add.ReturnType}");
            Console.WriteLine($"Parameters: {string.Join(", ", add.Parameters.Select(p => p.Name))}");
            Console.WriteLine($"Body Node Type: {add.Body.NodeType}");
        }
    }
}
