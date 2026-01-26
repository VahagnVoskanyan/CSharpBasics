using System.Linq.Expressions;

namespace CSharpBasics.ExpressionTrees
{
    // Most usecases
    // LINQ to SQL / Entity Framework (Database Queries)
    // Mocking Frameworks
    // Serialization Libraries
    // Dynamic Query Building

    internal class ExpressionTrees
    {
        public static void Run()
        {
            // Using Lambda Expressions
            Expression<Func<int, bool>> isPositive = num => num > 0; // (ex 1)
            Expression<Func<int, int, int>> add = (x, y) => x + y; // (ex 2)

            // Building Expression Trees Manually (ex 1)
            ParameterExpression numParam = Expression.Parameter(typeof(int), "num");
            ConstantExpression zero = Expression.Constant(0, typeof(int));
            BinaryExpression greaterThan = Expression.GreaterThan(numParam, zero);
            Expression<Func<int, bool>> isPositiveM = Expression.Lambda<Func<int, bool>>(
                greaterThan,
                new ParameterExpression[] { numParam }
            );

            // (ex 2) 
            Console.WriteLine($"NodeType: {add.NodeType}");
            Console.WriteLine($"Return Type: {add.ReturnType}");
            Console.WriteLine($"Parameters: {string.Join(", ", add.Parameters.Select(p => p.Name))}");
            Console.WriteLine($"Body Node Type: {add.Body.NodeType}");
            Console.WriteLine();

            Console.WriteLine("--> Using Expression Visitor");
            var expressionPrinter = new ExpressionPrinter();
            expressionPrinter.Visit(add.Body); // (ex 2)
            Console.WriteLine();
            expressionPrinter.Visit(isPositive.Body); // (ex 1)
            Console.WriteLine();
            expressionPrinter.Visit(isPositiveM.Body); // (ex 1)
        }

    }

    public class ExpressionPrinter : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Console.Write("(");
            Visit(node.Left);
            Console.Write($" {GetOperatorSymbol(node.NodeType)} ");
            Visit(node.Right);
            Console.Write(")");
            return node;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            Console.Write(node.Name);
            return node;
        }

        private string GetOperatorSymbol(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.Add: return "+";
                case ExpressionType.Subtract: return "-";
                case ExpressionType.Multiply: return "*";
                case ExpressionType.Divide: return "/";
                // Add more cases as needed
                default: return type.ToString();
            }
        }
    }
}
