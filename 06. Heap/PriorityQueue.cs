using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class PriorityQueue<TElement, TPriority> // 뒤에꺼 인트 써도 됨
    {
        private struct Node     // 노드 구조체
        {
            public TElement Element;        // 들어올 값
            public TPriority Priority;      // 우선도
        }

        private List<Node> node;        // 리스트 노드변수
        private IComparer<TPriority> comparer;      // 아직 무슨 형식일지 정해지지 않은 우선도를 비교하기 위한 변수

        public PriorityQueue()      // 초기화
        {
            this.node = new List<Node>();
            this.comparer = Comparer<TPriority>.Default;        // 기본값으로
        }

        public PriorityQueue(IComparer<TPriority> comparer)     // 오버로딩
        {
            this.node = new List<Node>();
            this.comparer = comparer;
        }

        public IComparer<TPriority> Comparer { get { return comparer; } }       // 비교 함수

        public int Count { get { return node.Count; } }     // 지금 몇개 들었는지 알려주는 함수

        public void Enqueue(TElement element, TPriority priority)       // 값 넣기
        {
            Node newNode = new Node() { Element = element, Priority = priority };
            // 새로운 변수, 저장할 값이랑 우선도 받아옴
            node.Add(newNode);      // 위에서 만든 변수를 제일 마지막에 더한다
            int newNodeIndex =  node.Count - 1;     // 정수형 변수, 노드의 제일 마지막 주소를 가리킨다

            while(newNodeIndex > 0)     // 위에서 만든 변수가 0보다 큰 한 반복 == 노드의 마지막부터 첫번째까지 반복
            {
                int parantIndex = GetParentIndex(newNodeIndex);     
                // 부모인덱스 정수형 변수, 위에서 만든 뉴노드인덱스 넣어서 함수 호출해서 받은 값 저장
                Node parentNode = node[parantIndex];        // 부모노드 선언, 위에서 받은 주소의 노드의 값 저장
                if (comparer.Compare(newNode.Priority, parentNode.Priority)<0)  // 비교한다, 뉴노드의 값과 부모노드의 값, 뉴노드가 부모노드보다 작다면
                {
                    node[newNodeIndex] = parentNode;    // 노드의 뉴노드인덱스 주소는 부모노드가 된다
                    node[parantIndex] = newNode;        // 노드의 부모노드 주소는 뉴노드가 된다 == 둘이 자리를 바꾼다
                    newNodeIndex = parantIndex;         // 뉴노드의 주소를 부모노드의 주소로 한다.
                }
                else break;     // 아니라면 반복문 탈주
            }
        }   // 원리, 추가할 값을 리스트의 제일 마지막에 추가하고 부모값과 비교하며 힙이 성립될때까지 하나씩 타고 올려보내는것

        public TElement Dequeue()       // 우선도 높은 순으로 빼내기
        {
            Node root = node[0];        // 뿌리변수 선언, 노드의 제일 앞 값 == 우선도가 제일 높은 값 저장
            Node last =  node[node.Count-1];        // 마지막 노드, 노드의 제일 마지막 값 저징
            node[0] = last;     // 노드의 첫번쨰에 마지막의 값을 저장
            node.RemoveAt(node.Count-1);        // 마지막 값 삭제
            int index = 0;      // 정수형 인덱스 변수 0으로 초기화
            while(index < node.Count)       // 인덱스가 노드의 수보다 작은 한 반복
            {
                int leftIndex = GetLeftIndex(index);        // 왼쪽인덱스 선언, 인덱스 넣어서 함수호출
                int rightIndex = GetRightIndex(index);      // 오른쪽인덱스 선언, 인덱스 넣어서 함수호출

                if (rightIndex < node.Count)        // 오른쪽 인덱스가 노드의 수보다 작다면 == 자식노드가 둘 다 있는 경우
                {
                    int lowIndex = comparer.Compare(node[leftIndex].Priority, node[rightIndex].Priority) < 0 ? leftIndex : rightIndex;
                    // 로우인덱스 변수, 비교한다, 왼쪽인덱스번쨰 노드의 값과 오른쪽인덱스번째 노드의 값,
                    // 왼쪽이 더 크면 왼쪽인덱스, 오른쪽이 더 크면 오른쪽인덱스 저장
                    // == 둘중 더 작은거랑 위치 바꿀래
                    if (comparer.Compare(node[lowIndex].Priority, node[index].Priority) < 0)    
                    {
                        // 비교, 로우인덱스번째 노드의 값이 인덱스번쨰 노드 값보다 작다면
                        node[index] = node[lowIndex];       // 인덱스번째 노드에 로우인덱스번째 노드값 저장
                        node[lowIndex] = last;      // 로우인덱스번쨰 노드에 라스트 저장.
                        index = lowIndex;       // 인덱스에 로우인덱스 값 저장
                    }
                    else break;     // 그 외의 경우 반복문 탈출
                }
                else if (leftIndex < node.Count)        // 왼쪽 인덱스가 노드의 수보다 적다면 == 자식노드가 하나만 있는 경우
                {
                    if (comparer.Compare(node[leftIndex].Priority, node[index].Priority) < 0)
                    {
                        // 비교, 노드의 왼쪽인덱스번쨰 값이 노드의 인덱스번째 값보다 작다면
                        node[index] = node[leftIndex];      // 노드의 인덱스번째에 노드의 왼쪽인덱스번쨰 저장
                        node[leftIndex] = last;     // 노드의 왼쪽인덱스번쨰에 라스트 저장
                        index = leftIndex;      // 인덱스에 왼쪽인덱스값 저장
                    }
                    else break;     // 그 외의 경우 반복문 탈출
                }
                else break;     // 그 외의 경우 == 자식노드가 없는 경우 반복문 탈주
            }
            return root.Element;        // 뿌리값 반환
        }       // 원리, 첫번쨰 값을 따로 뺴놓은 다음 제일 마지막 값을 복사해서 첫번째 위치에 넣고 마지막 값 삭제,
                // 자식클래스와 비교하면서 힙이 성립할때까지 한칸씩 내려보낸다.

        public TElement Peek()      // 제일 위에 값 보기
        {
            return node[0].Element;     // 노드의 우선도가 제일 높은 값 출력
        }

        private int GetParentIndex(int index)    // 부모노드주소구하기  
        {
            return (index - 1) / 2;
        }
        private int GetLeftIndex(int index)     // 왼쪽자식노드주소구하기
        {
            return index * 2 + 1;
        }
        private int GetRightIndex(int index)        // 오른쪽자식노드주소구하기
        {
            return index * 2 + 2;
        }

        public TElement InTheMax()
        {
            TElement higher;
            int index = 0;
            while (index > node.Count)
            {
                int left = index * 2 + 1;
                int right = index * 2 + 2;
                if (right < node.Count)
                {
                    int high = comparer.Compare(node[left].Priority, node[right].Priority) > 0 ? left : right;
                    index = high;
                }
                else if (left < node.Count)
                {
                    index = left;
                }
                else break;
            }
            higher = node[index].Element;
            return higher;
        }

    }
}
