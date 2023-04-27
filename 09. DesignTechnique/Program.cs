namespace _09._DesignTechnique
{
    internal class Program
    {
        /******************************************************
		 * 알고리즘 설계기법(Algorithm Design Technique)
		 * 
		 * 어떤 문제를 해결하는 과정에서 해당 문제의 답을 효과적으로 찾아가기 위한 전략과 접근 방식
         * 문제를 풀 때 어떤 알고리즘 설계 기법을 쓰는지에 따라 효율성이 막대하게 차이
         * 문제의 성질과 조건에 따라 알맞은 알고리즘 설계기법을 선택하여 사용
		 ******************************************************/
        public enum Place { Left, Middle, Right }
        public static int[,] result = new int[2,1000];
        public static int n = 0;
        public static void Move(int count, int start, int end)
        {
            if (count == 1)
            {
                int plate = stick[(int)start].Pop();
                stick[(int)end].Push(plate);
                result[0,n] = start;
                result[1,n] = end;
                n++;
                return;
            }
            int other = (int)(3 - (int)start - (int)end);

            Move(count - 1, start, other);
            Move(1, start, end);
            Move(count - 1, other, end);
        }

        public static Stack<int>[] stick;

        static void Main(string[] args)
        {
            stick = new Stack<int>[3];
            for(int i = 0; i < stick.Length; i++)
            {
                stick[i] = new Stack<int>();
            }
            for(int i = 3; i > 0; i--)
            {
                stick[0].Push(i);
            }
            Move(3, 0, 2);

            /*
            Backtracking backtracking = new Backtracking();
            bool[,] b = new bool[8, 8];
            backtracking.NQueen(b);
            */
            Console.WriteLine(n);
            for (int i = 0; i < n; i++)
            {
                for(int j = 0; j < result.GetLength(0); j++)
                {
                    Console.Write(result[j,i]+1 + " ");
                }
                Console.WriteLine("");
            }
        }
    }
}