namespace test
{
    internal class Program
    {
        public static int[,] result = new int[2, 100000000];
        public static int n = 0;
        public static Stack<int>[] stick = new Stack<int>[3];
        public static void Move(int count, int start, int end)
        {
            if (count == 1)
            {
                int plate = stick[start].Pop();
                stick[end].Push(plate);
                result[0, n] = start;
                result[1, n] = end;
                n++;
                return;
            }
            int other = 3 - start - end;

            Move(count - 1, start, other);
            Move(1, start, end);
            Move(count - 1, other, end);
        }
        static void Main(string[] args)
        {
            int a = int.Parse(Console.ReadLine());
            for (int i = 0; i < stick.Length; i++)
                stick[i] = new Stack<int>();
            for (int i = a; i > 0; i--)
                stick[0].Push(i);
            Move(a, 0, 2);
            Console.WriteLine(n);
            for (int i = 0; i < n; i++)
            {
                Console.Write(result[0, i] + 1 + " ");
                Console.Write(result[1, i] + 1);
                Console.WriteLine("");
            }
        }
    }
}