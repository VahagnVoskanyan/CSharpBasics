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
        public static void Run()
        {

        }

        // Method Overloading - Compile Time Polymorphism
        void TotalSum() { }
        void TotalSum(int a) { }
        // Changing number of parameters
        void TotalSum(int a, int b) { }
        // Changing type of parameters
        void TotalSum(int a, double b) { }
        // Different return type alone is not sufficient for method overloading
        //double TotalSum(int a, int b) { return  (double)(a + b); }
        // Different return type also is overloading if parameters are different
        // Changing parameter places
        double TotalSum(double a, int b) { return a + b; }
        float TotalSum(float a, float b) { return a + b; }
    }


}
