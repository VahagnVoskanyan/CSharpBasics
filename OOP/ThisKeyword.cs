namespace CSharpBasics.OOP
{
    class Student
    {
        private string[] name = new string[3];

        // declaring an indexer
        public string this[int index]
        {
            // returns value of array element
            get
            {
                return name[index];
            }

            // sets value of array element
            set
            {
                name[index] = value;
            }
        }
    }

    internal class ThisKeyword
    {
        int num1;
        int num2;

        public ThisKeyword(int num1, int num2)
        {
            Console.WriteLine($"Constructor with two parameter: {num1}, {num2}");
            this.num1 = num1; // Using 'this' to refer to the instance variable
            this.num2 = num2;
        }

        public ThisKeyword(int num1) : this(num1, 33) // Calling another constructor in the same class
        {
            Console.WriteLine($"Constructor with one parameter: {num1}");
        }

        // method that accepts this as argument   
        void passParameter(ThisKeyword t1)
        {
            t1.num1 += 10;
            Console.WriteLine("num1: " + num1);
            Console.WriteLine("num2: " + num2);
        }

        void display()
        {
            // passing this as a parameter
            passParameter(this);
        }

        public static void Run()
        {
            ThisKeyword t1 = new(11);
            t1.display();

            Console.WriteLine("'this' as Indexer");

            Student s1 = new();
            s1[0] = "One";
            s1[1] = "Two";
            s1[2] = "Three";

            for (int i = 0; i < 3; i++)
            {

                Console.WriteLine(s1[i] + " ");
            }


        }
    }
}
