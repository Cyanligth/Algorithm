﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11._Searching
{
    internal class Search
    {
        // <순차 탐색>
        public static int SequentialSearch<T>(in IList<T> list, in T item) where T : IEquatable<T>
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (item.Equals(list[i]))
                    return i;
            }
            return -1;
        }

        // <이진 탐색>
        // 정렬이 보장되어 있을때만 사용 가능
        // 연결리스트는 그냥 못씀
        public static int BinarySearch<T>(in IList<T> list, in T item) where T : IComparable<T>
        {
            return BinarySearch(list, item, 0, list.Count);
        }

        public static int BinarySearch<T>(in IList<T> list, in T item, int index, int count) where T : IComparable<T>
        {
            if (index < 0)
                throw new IndexOutOfRangeException();
            if (count < 0)
                throw new ArgumentOutOfRangeException();
            if (index + count > list.Count)
                throw new ArgumentOutOfRangeException();

            int low = index;
            int high = index + count - 1;
            while (low <= high)
            {
                int middle = (low+high) >> 1;      // 나누기보다 곱하기가 빠르고 곱하기보다 비트 밀기가 더 빠름
                //      n/2 < n*0.5f < n>>1
                int compare = list[middle].CompareTo(item);

                if (compare < 0)
                    low = middle + 1;
                else if (compare > 0)
                    high = middle - 1;
                else
                    return middle;
            }
            return -1;
        }

        // <깊이 우선 탐색 (Depth-First Search)>
        // 그래프의 분기를 만났을 때 최대한 깊이 내려간 뒤,
        // 더이상 깊이 갈 곳이 없을 경우 다음 분기를 탐색
        // =백트래킹 방식
        public static void DFS(in bool[,] graph, int start, out bool[] visited, out int[] parent)
        {
            visited = new bool[graph.GetLength(0)];
            parent = new int[graph.GetLength(0)];

            for (int i = 0; i < graph.GetLength(0); i++)
            {
                visited[i] = false;
                parent[i] = -1;
            }

            SearchNode(graph, start, visited, parent);
        }

        private static void SearchNode(in bool[,] graph, int start, bool[] visited, int[] parent)
        {
            visited[start] = true;
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                if (graph[start, i] &&      // 연결되어 있는 정점이며,
                    !visited[i])            // 방문한적 없는 정점
                {
                    parent[i] = start;
                    SearchNode(graph, i, visited, parent);
                }
            }
        }

        // <너비 우선 탐색 (Breadth-First Search)>
        // 그래프의 분기를 만났을 때 모든 분기를 저장한 뒤,
        // 저장한 분기를 하나씩 탐색
        public static void BFS(in bool[,] graph, int start, out bool[] visited, out int[] parents)
        {
            visited = new bool[graph.GetLength(0)];
            parents = new int[graph.GetLength(0)];

            for (int i = 0; i < graph.GetLength(0); i++)
            {
                visited[i] = false;
                parents[i] = -1;
            }

            Queue<int> bfsQueue = new Queue<int>();

            bfsQueue.Enqueue(start);
            while (bfsQueue.Count > 0)
            {
                int next = bfsQueue.Dequeue();
                visited[next] = true;

                for (int i = 0; i < graph.GetLength(0); i++)
                {
                    if (graph[next, i] &&       // 연결되어 있는 정점이며,
                        !visited[i])            // 방문한적 없는 정점
                    {
                        parents[i] = next;
                        bfsQueue.Enqueue(i);
                    }
                }
            }
        }
    }
}
