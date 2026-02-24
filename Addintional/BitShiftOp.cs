namespace CSharpBasics.Addintional
{
    internal static class BitShiftOp
    {
        public static void Run()
        {
            int a = 8; // 0000 1000 in binary
            int leftShift = a << 2; // Shift left by 2 bits (0000 1000 becomes 0010 0000)
            int rightShift = a >> 2; // Shift right by 2 bits (0000 1000 becomes 0000 0010)
            Console.WriteLine($"Original value: {a}");
            Console.WriteLine($"Left shift (a << 2): {leftShift}"); // like a * 2^n
            Console.WriteLine($"Right shift (a >> 2): {rightShift}"); // like a / 2^n

            int a1 = 12500; // 11000011010100 in binary
            int leftShift1 = a1 << 3; // Shift left by 3 bits (11000011010100 becomes 11000011010100000)
            int rightShift1 = a1 >> 3; // Shift right by 3 bits (11000011010100 becomes 11000011010)
            Console.WriteLine($"Original value: {a1}");
            Console.WriteLine($"Left shift (a << 3): {leftShift1}");
            Console.WriteLine($"Right shift (a >> 3): {rightShift1}");

            int a2 = int.MaxValue;
            int leftShift2 = a2 << 1;   // overflow // -2 
            Console.WriteLine($"Original value: {a2}");
            Console.WriteLine($"Left shift (a << 1): {leftShift2}");

            // Note: Right shifting a negative number will fill the leftmost bits with 1s (arithmetic shift)
            int a3 = -8; // 1111 1000 in binary
            int rightShift3 = a3 >> 2; // Shift right by 2 bits (1111 1110)
            Console.WriteLine($"Original value: {a3}");
            Console.WriteLine($"Right shift (a >> 2): {rightShift3}");

            // But if -7. Right shift rounds towards negative infinity, so -7 >> 2 is -2, not -1. 
            int a4 = -7; // 1111 1001 in binary
            int rightShift4 = a4 >> 2; // Shift right by 2 bits (1111 1110) (-2)
            var div = a4 / 4; // -7 / 4 = -1
            Console.WriteLine($"Original value: {a4}");
            Console.WriteLine($"Right shift (a >> 2): {rightShift4}");
            Console.WriteLine($"Division: {div}");

            // C# also has an unsigned right shift operator (>>>) that fills the leftmost bits with 0s (logical shift)
            int a5 = -8; // 1111 1000 in binary
            var c = (uint)a5; // 1111 1000 in binary is 4294967288 in unsigned representation
            int unsignedRightShift = a5 >>> 2; // Shift right by 2 bits (0011 1110) // From C# 11
            int rightShift2 = (int)((uint)a5 >> 2); // Result is same as above
            Console.WriteLine($"Original value: {a5}");
            Console.WriteLine($"Unsigned right shift (a >>> 2): {unsignedRightShift}");
            Console.WriteLine($"Right shift (int)((uint)a >> 2): {rightShift2}");
        }
    }
}
