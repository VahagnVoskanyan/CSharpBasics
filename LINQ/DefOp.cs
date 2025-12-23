namespace CSharpBasics.LINQ
{
    internal class DefOp
    {
        public void Run()
        {
            string[] people = ["Tom", "Sam", "Bob"];

            // Deferred
            // Can say- query declaration 
            var selectedPeople = people.Where(s => s.Length == 3).OrderBy(s => s);

            // LINQ query executes here not before 
            foreach (string s in selectedPeople)
                Console.WriteLine(s);

            people[1] = "Mike";
            Console.WriteLine("After changing the source array:");
            // Executes second time
            foreach (string s in selectedPeople)
                Console.WriteLine(s);

            Console.WriteLine();

            // Immediate
            string[] peopleC = ["Tom", "Sam", "Bob"];
            var selectedPeopleC = peopleC.Where(s => s.Length == 3).OrderBy(s => s).Count();

            Console.WriteLine("Before Change: " + selectedPeopleC);

            people[2] = "Mike";
            Console.WriteLine("After Change: " + selectedPeopleC);

            // 
            string[] peopleI = ["Tom", "Sam", "Bob"];
            var selectedPeopleI = peopleI.Where(s => s.Length == 3).OrderBy(s => s).ToList();

            foreach (string s in selectedPeopleI)
                Console.WriteLine(s);

            peopleI[1] = "Max";
            Console.WriteLine("After changing the source array:");
            foreach (string s in selectedPeopleI)
                Console.WriteLine(s);
        }
    }
}
