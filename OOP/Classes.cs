namespace CSharpBasics.OOP
{
    internal class Classes
    {
        // Class modifiers: static, abstract, sealed, partial.
        // sealed - a class that cannot be inherited from. 
        // partial - allows a class definition to be split across multiple files.
        
        public static void Run()
        {
            MyClass1 myClass1_1 = new ();
            MyClass1 myClass1_2 = new MyClass2(); // Can convert derived to Base
            //MyClass2 myClass2 = new MyClass1(); // Can't convert base to Derived

            myClass1_1.TestVirt(); // Calls Base version
            myClass1_2.TestVirt(); // Base MyClass1 but calls overridden version in Derived MyClass2
            myClass1_2.TestVirt1(); // Calls Base version since not overridden in Derived class

            Console.WriteLine("\n->> Properties");
            Console.WriteLine($"myClass1_1.MyProperty: {myClass1_1.MyProperty}"); // Calls Base version
            Console.WriteLine($"myClass1_2.MyProperty: {myClass1_2.MyProperty}"); // Calls Overridden version in Derived class

            Console.WriteLine("\n->> Abstract Class");
            MyAbsDerivedClass myAbsDerived = new ();
            MyAbstractClass2 myAbstractClassDer = new MyAbsDerivedClass();

            myAbsDerived.AbsImplemented(); // Calls Abstract class method
            myAbstractClassDer.AbsImplemented(); // Calls Abstract class method
            Console.WriteLine(myAbsDerived.MyAbsProperty);
            Console.WriteLine(myAbstractClassDer.MyAbsProperty);

            Console.WriteLine("\n->> 'base' keyword");
            myClass1_2.TestVirt2(5); // Calls Derived version only
            myClass1_2.TestVirt2(15); // Calls Base and Derived versions

            Console.WriteLine();
            

            Console.ReadLine();
        }
    }

    internal abstract class MyAbstractClass1 : MyAbstractClass2
    {
        public string Name { get; set; }

        protected MyAbstractClass1(string name) : base("Base Abstract") // Can have constructor
        {
            Name = name;
        }

        // Implement abstract method in abstract class from base abstract class
        public override void TestAbs() { }
    }

    internal abstract class MyAbstractClass2
    {
        public abstract int MyAbsProperty { get; } // Abstract property - no body, must be implemented in derived class
        private readonly string Name;

        protected MyAbstractClass2(string name)
        {
            Name = name;
        }

        public abstract void TestAbs(); // Abstract method - no body, must be implemented in derived class

        public void AbsImplemented() // Implemented method 
        {
            Console.WriteLine("MyAbstractClass 'AbsImplemented' method");
        }

        public virtual void TestVirt()
        {
            Console.WriteLine("MyAbstractClass 'TestVirt' Method");
        }
    }

    internal class MyAbsDerivedClass : MyAbstractClass2
    {
        public MyAbsDerivedClass() : base("Abstract") { } // Must set value for base abstract class constructor

        public override int MyAbsProperty { get { return 10; } } // Implement abstract property
        public override void TestAbs() // Implement abstract method
        {
            Console.WriteLine("MyDerivedClass 'TestAbs' Method");
        }
        public override void TestVirt()
        {
            Console.WriteLine("MyDerivedClass 'TestVirt' Method");
        }
    }

    internal class MyClass1
    {
        public virtual int MyProperty { get; set; } = 5; // Virtual property

        public void Display()
        {
            Console.WriteLine("MyClass1 Display method");
        }

        //public abstract void TestAbs(); // Abstract method can only be in abstract class
        //public virtual void TestVirt3(); // Must have body unless method is abstract, extern or partial

        public virtual void TestVirt()
        {
            Console.WriteLine("Base 'TestVirt' Method");
        }

        public virtual void TestVirt1()
        {
            Console.WriteLine("Base 'TestVirt1' Method");
        }

        public virtual void TestVirt2(int b)
        {
            Console.WriteLine($"Base 'TestVirt2' Method int: {b}");
        }
    }

    internal class MyClass2 : MyClass1
    {
        // If overriding a property, must provide(override) both get and set accessors.
        //public override int MyProperty { get { return 10; } set; }

        public override int MyProperty { get { return 10; } set { } } // Override virtual property

        // Can't override because it's not marked as virtual, abstract, or override.
        //public override void Display() { }

        public void Show()
        {
            Console.WriteLine("MyClass2 Show method");
        }

        public override void TestVirt()
        {
            Console.WriteLine("Derived 'TestVirt' Method");
        }

        // Sealed override method - prevents further overriding in derived classes
        public sealed override void TestVirt2(int a)
        {
            if (a > 10) base.TestVirt2(a); // Call base class version
            
            Console.WriteLine($"Derived 'TestVirt2' Method. int: {a}");
        }
    }
}
