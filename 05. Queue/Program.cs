namespace _05._Queue
{
    internal class Program
    {
        /******************************************************
		 * 큐 (Queue)
		 * 
		 * 선입선출(FIFO), 후입후출(LILO) 방식의 자료구조
		 * 입력된 순서대로 처리해야 하는 상황에 이용
		 ******************************************************/

        void Queue()
        {
            Queue<int> queue = new Queue<int>();

            for (int i = 0; i < 5; i++) queue.Enqueue(i);                   // 입력순서 : 0, 1, 2, 3, 4

            Console.WriteLine(queue.Peek());                                // 최전방 : 0

            while (queue.Count > 0) Console.WriteLine(queue.Dequeue());     // 출력순서 : 0, 1, 2, 3, 4
        }

        static void Main(string[] args)
        {
            DataStructure.Queue<int> queue = new DataStructure.Queue<int>();
            /*
            for (int i = 0; i < 20; i++) queue.Enqueue(i);

            Console.WriteLine(queue.Peek());

            while (queue.Count > 10) Console.WriteLine(queue.Dequeue());     // 출력순서 : 0, 1, 2, 3, 4
            Console.WriteLine("");
            for (int i = 0; i < 5; i++) queue.Enqueue(i);
            while (queue.Count > 0) Console.WriteLine(queue.Dequeue());
            */

            
            Player player1 = new Player("1번", 10);
            Player player2 = new Player("2번", 30);
            Player player3 = new Player("3번", 20);
            Queue<string> ts = new Queue<string>();
            ts = Ready(player1, player2, player3);
            Action(ts);
            

            int[] a = yosefus(10,3);
            foreach (int i in a) { Console.WriteLine(i); }
        }

        static int[] yosefus(int human, int num)
        {
            Queue<int> main = new Queue<int>();
            Queue<int> sub = new Queue<int>();
            int[] result = new int[human];
            for(int i = 1; i <= human; i++)
                main.Enqueue(i);
            for(int x = 0; x < human; x++)
            {
                for(int y = 0; y < num-1; y++)
                {
                    if (sub.Count != 0)
                    {
                        main.Enqueue(sub.Dequeue());
                    }
                    sub.Enqueue(main.Dequeue());
                    
                }
                if(main.Count == 0)
                    main.Enqueue(sub.Dequeue());
                result[x] = main.Dequeue();
            }
            return result;
        }

        static void Action(Queue<string> queue)
        {
            foreach (string s in queue) { Console.WriteLine(s); }
        }
        static Queue<string> Ready(Player player1, Player player2, Player player3)
        {
            Queue<string> ready = new Queue<string>();
            if (player1.speed > player2.speed && player1.speed > player3.speed)
            {
                ready.Enqueue(player1.name);
                if (player2.speed > player3.speed)
                {
                    ready.Enqueue(player2.name);
                    ready.Enqueue(player3.name);
                }
                else
                {
                    ready.Enqueue(player3.name);
                    ready.Enqueue(player2.name);
                }
            }
            else if(player2.speed > player1.speed && player2.speed > player3.speed)
            {
                ready.Enqueue(player2.name);
                if (player1.speed > player3.speed)
                {
                    ready.Enqueue(player1.name);
                    ready.Enqueue(player3.name);
                }
                else
                {
                    ready.Enqueue(player3.name);
                    ready.Enqueue(player1.name);
                }
            }
            else
            {
                ready.Enqueue(player3.name);
                if (player2.speed > player1.speed)
                {
                    ready.Enqueue(player2.name);
                    ready.Enqueue(player1.name);
                }
                else
                {
                    ready.Enqueue(player1.name);
                    ready.Enqueue(player2.name);
                }
            }
            return ready;
        }
    }
}