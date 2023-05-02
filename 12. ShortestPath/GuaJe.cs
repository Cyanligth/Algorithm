using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._ShortestPath
{
    internal class GuaJe
    {
        const int OVER = 99999999;      // 연결 안된걸 엄청 큰 숫자로 표현

        public static void Dijkstra(in int[,] molu, in int first, out int[] length, out int[] parent)
        {   // 그래프, 시작위치 받아오고 거리, 부모 반환
            int size = molu.GetLength(0);   // 반복할 크기를 그래프의 숫자들만큼 지정
            bool[] check = new bool[size];      // 앞으로 갈곳이 갔다온곳인지 확인할 불 배열
            length = new int[size];     // 거리 배열 초기화
            parent = new int[size];       // 부모 배열 초기화
            for(int i = 0; i < size; i++)   // 값 초기화용 반복문
            {
                length[i] = molu[first, i];     // i번째의 길이를 그래프의 시작부터 i까지의 길이로 저장
                parent[i] = molu[first, i] < OVER ? first : -1;     
                // i의 부모를 그래프의 시작부터 i까지의 길이가 오버보다 작다면 == 연결이 되어 있다면 시작숫자로 저장, 아니라면 -1 저장
            }

            for(int i = 0; i < size; i++)   // 반복
            {
                int next = -1;      // 부모가 될 무언가에 -1 저장
                int min = OVER;     // 제일 짧은 거리에 최대치 저장
                for(int j = 0; j < size; j++)   // 안가본곳중 가까운부터 반복
                {
                    if (check[j] == false && length[j] < min)
                    {   // 앞으로 탐색할곳이 가본곳이 아님과 동시에 거리가 제일 가까울 경우
                        min = length[j];    // 최단거리에 현재 거리 저장 
                        next = j;       // 부모가 될 무언가에 현재값 저장
                    }
                }
                if (next < 0) break;   // 만약 다 살펴봤는데 부모가 없다면 == 고립되어있다면 반복문 탈출

                for (int j = 0; j < size; j++)  // 거리갱신
                {
                    if (length[j] > length[next] + molu[next, j])
                    {   // 만약 시작부분부터 목적지까지의 직통 거리가 현재 탐색중인 부분을 거쳐서 가는 거리보다 길다면
                        length[j] = length[next] + molu[next, j];   
                        // 시작부분부터 목적지까지의 거리는 현재 탐색중인 부분을 거쳐서 가는 거리가 된다
                        parent[j] = next;   
                        // 목적지의 부모는 현재 탐색중인 부분이 된다.
                    }
                }
                check[next] = true;     // 현재 탐색한곳을 참으로 설정 == 가본곳으로 만든다
            }
        }
    }
}
