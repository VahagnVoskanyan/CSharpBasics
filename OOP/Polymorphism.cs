namespace CSharpBasics.OOP
{
    // Polymorphism is when the same entity (method or operator or object) can perform different operations in different scenarios.
    // Types of Polymorphism
    // 1. Compile Time Polymorphism / Static Polymorphism
    // Method, Operator overloading
    // 2. Run-Time Polymorphism / Dynamic Polymorphism
    // Method Overriding

    internal class Polymorphism
    {
        public int A { get; set; }

        public int B { get; set; } = 10;

        public Polymorphism(int a)
        {
            A = a;
        }
        // Constructor Overloading
        public Polymorphism(int a, int b)
        {
            A = a;
            B = b;
        }

        public static void Run()
        {
            var poly1 = new Polymorphism(5);
            Console.WriteLine($"A: {poly1.A}, B: {poly1.B}");
            var poly2 = new Polymorphism(15, 25);
            Console.WriteLine($"A: {poly2.A}, B: {poly2.B}");
        }

        // Method Overloading - Compile Time Polymorphism
        void TotalSum() { }
        void TotalSum(int a) { }
        // Changing number of parameters
        void TotalSum(int a, int b) { }
        // Changing Data types of parameters
        void TotalSum(int a, double b) { }
        // Different return type alone is not sufficient for method overloading
        //double TotalSum(int a, int b) { return  (double)(a + b); }
        // Different return type also is overloading if parameters are different
        // Changing parameter places
        double TotalSum(double a, int b) { return a + b; }
        float TotalSum(float a, float b) { return a + b; }
    }
}
