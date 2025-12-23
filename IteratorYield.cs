namespace CSharpBasics
{
    // yield return - returns an expression at each iteration
    // yield break - terminates the iteration

    internal class IteratorYield
    {
        public static void Run()
        {
            foreach (var number in GetPosNumbers())
            {
                Console.WriteLine(number);
            }
        }

        // define an iterator method (iterator because we use 'yield')
        static IEnumerable<int> GetPosNumbers()
        {
            List<int> myList = new() { -1, -4, 3, 5, 7 , 8 };

            foreach (var num in myList)
            {
                if (num >= 0)
                {
                    //if (num == 7) break;  // break will terminate the loop

                    // will terminate the entire method execution
                    // similar when return no data in normal method
                    if (num == 8) yield break;

                    yield return num;

                    // location of the code is preserved 
                    // so on the next iteration getNumber() is executed from here 
                    Console.WriteLine("....");
                }
            }

            // this code won't execute if 'yield break' is hit
            Console.WriteLine("!! Code After Loop inside Interator method");
        }
    }
}
