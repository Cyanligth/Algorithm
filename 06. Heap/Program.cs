﻿using System.ComponentModel;

namespace _06._Heap
{
    internal class Program
    {
        /******************************************************
		 * 힙 (Heap)
		 * 
		 * 부모 노드가 항상 자식노드보다 우선순위가 높은 속성을 만족하는 트리기반의 자료구조
		 * 많은 자료 중 우선순위가 가장 높은 요소를 빠르게 가져오기 위해 사용
		 ******************************************************/

        void PriorityQueue()
        {
            // 기본 int 우선순위(오름차순) 적용
            PriorityQueue<string, int> pq1 = new PriorityQueue<string, int>();

            pq1.Enqueue("데이터1", 1);     // 우선순위와 데이터를 추가
            pq1.Enqueue("데이터2", 3);
            pq1.Enqueue("데이터3", 5);
            pq1.Enqueue("데이터4", 2);
            pq1.Enqueue("데이터5", 4);

            while (pq1.Count > 0) Console.WriteLine(pq1.Dequeue()); // 우선순위가 높은 순서대로 데이터 출력


            // 수정 int 우선순위 적용
            PriorityQueue<string, int> pq2 = new PriorityQueue<string, int>(Comparer<int>.Create((a, b) => b - a));

            pq2.Enqueue("데이터1", 1);     // 우선순위와 데이터를 추가
            pq2.Enqueue("데이터2", 3);
            pq2.Enqueue("데이터3", 5);
            pq2.Enqueue("데이터4", 2);
            pq2.Enqueue("데이터5", 4);

            while (pq2.Count > 0) Console.WriteLine(pq2.Dequeue()); // 우선순위가 높은 순서대로 데이터 출력
        }

        // 시간복잡도
        // 탐색 = 1~log n
        // 추가 = log n
        // 삭제 = log n

        static void Main(string[] args)
        {
            /*
            // 기본 int 우선순위(오름차순) 적용
            DataStructure.PriorityQueue<string, int> pq1 = new DataStructure.PriorityQueue<string, int>();

            pq1.Enqueue("데이터1", 1);     // 우선순위와 데이터를 추가
            pq1.Enqueue("데이터2", 3);
            pq1.Enqueue("데이터3", 5);
            pq1.Enqueue("데이터4", 2);
            pq1.Enqueue("데이터5", 4);

            while (pq1.Count > 0) Console.WriteLine(pq1.Dequeue()); // 우선순위가 높은 순서대로 데이터 출력

            // 수정 int 우선순위 적용
            DataStructure.PriorityQueue<string, int> pq2 = new DataStructure.PriorityQueue<string, int>(Comparer<int>.Create((a, b) => b - a));

            pq2.Enqueue("데이터1", 1);     // 우선순위와 데이터를 추가
            pq2.Enqueue("데이터2", 3);
            pq2.Enqueue("데이터3", 5);
            pq2.Enqueue("데이터4", 2);
            pq2.Enqueue("데이터5", 4);

            while (pq2.Count > 0) Console.WriteLine(pq2.Dequeue()); // 우선순위가 높은 순서대로 데이터 출력
            */
            PriorityQueue<string, int> amb = new PriorityQueue<string, int>();
            string name;
            int goldenTime;
            Console.Write("환자 수 입력 : ");
            int count = int.Parse(Console.ReadLine());
            int a = 0;
            while(a < count)
            {
                Console.Write("환자 이름 : ");
                name = Console.ReadLine();
                Console.Write("골든타임 : ");
                goldenTime = int.Parse(Console.ReadLine());
                amb.Enqueue(name, goldenTime);
                a++;
                Console.Clear();
            }
            Console.WriteLine("치료순서");
            for (int i = 0; i < count; i++)
                Console.WriteLine(i+1 +". "+ AMB(amb));
            Console.WriteLine("치료를 시작합니다");

        }

        static string AMB(PriorityQueue<string, int> amb)
        {
            string name = amb.Dequeue();
            return name;
        }
    }
}