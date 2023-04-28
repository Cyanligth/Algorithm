using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10._Sorting
{
    internal class Sort
    {
        /******************************************************
		 * 선형 정렬
		 * 
		 * 1개의 요소를 재위치시키기 위해 전체를 확인하는 정렬
		 * n개의 요소를 재위치시키기 위해 n개를 확인하는 정렬
		 * 시간복잡도 : O(N^2)
		 ******************************************************/

        // <선택정렬>
        // 데이터 중 가장 작은 값부터 하나씩 선택하여 정렬

        // 1. 인덱스 0부터 Length까지 둘러보면서 제일 작은 값을 구함
        // 2. 제일 작은 값을 인덱스 0번과 교환
        // 3. 인덱스값 +1 하고 전부 정렬될때까지 1번부터 반복
        public static void SelectionSort(IList<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[j] < list[minIndex])
                        minIndex = j;
                }
                Swap(list, i, minIndex);
            }
        }

        // <삽입정렬>
        // 데이터를 하나씩 꺼내어 정렬된 자료 중 적합한 위치에 삽입하여 정렬

        // 1. 인덱스 0번부터 하나를 꺼내본다.
        // 2. 꺼낸 데이터보다 앞쪽에 있는 데이터들을 확인
        // 3. 꺼낸 데이터보다 큰 데이터들을 전부 1칸씩 뒤로 민다
        // 4. 밀려서 생긴 자리에 꺼낸 데이터를 넣는다.
        // 5. 인덱스 +1 하고 정렬될때까지 1번부터 반복
        public static void InsertionSort(IList<int> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                int key = list[i];
                int j;
                for (j = i - 1; j >= 0 && key < list[j]; j--)
                {
                    list[j + 1] = list[j];
                }
                list[j + 1] = key;
            }
        }

        // <버블정렬>
        // 서로 인접한 데이터를 비교하여 정렬

        // 1. 인덱스 0번을 인덱스+1의 값과 비교
        // 2. 더 큰쪽이 뒤로 가도록 자리를 바꾼다.
        // 3. 인덱스 +1 하고 배열 전체를 순회해 제일 큰 값 하나가 제일 뒤로 옮겨질때까지 반복
        // 4. 정렬될때까지 1번부터 3번을 반복 
        public static void BubbleSort(IList<int> list)
        {
            int count = 0;
            for (int i = list.Count; i > 0; i--)
            {
                count = 0;
                for (int j = 1; j < i; j++)
                {
                    if (list[j - 1] > list[j])
                        Swap(list, j - 1, j); count++;
                }
                if (count == 0) break;
            }
        }
        // 최적화, 제일 뒤쪽은 돌때마다 고정되니 굳이 갈 필요 없게 만들었고
        // 추가로 한번의 반복동안 아무것도 교체되지 않을 시 굳이 한번 더 반복하지 않고 탈출

        /******************************************************
		 * 분할정복 정렬
		 * 
		 * 1개의 요소를 재위치시키기 위해 전체의 1/2를 확인하는 정렬
		 * n개의 요소를 재위치시키기 위해 n/2개를 확인하는 정렬
		 * 시간복잡도 : O(NlogN)
		 ******************************************************/

        // <힙정렬>
        // 힙을 이용하여 우선순위가 가장 높은 요소부터 가져와 정렬
        // 같은 데이터가 들어오면 정렬이 깨질수도 있음
        // 캐시를 잘 못써서 평균적으로 느려짐
        // 배열을 차례댜로 한번에 받아오는게 아니라 힙이라는 특성떄문에 띄엄띄엄 여러번 받아와야함

        // 배열을 전부 힙에 넣는다. enqueue
        // 힙에 넣는 도중에 힙이 알아서 순서대로 바꿔준다.
        // 넣은 만큼 Dequeue해서 순서대로 배열에 넣는다
        public static void HeapSort(IList<int> list)
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();

            for (int i = 0; i < list.Count; i++)
            {
                pq.Enqueue(list[i], list[i]);
            }

            for (int i = 0; i < list.Count; i++)
            {
                list[i] = pq.Dequeue();
            }
        }

        // <합병정렬>
        // 데이터를 2분할하여 정렬 후 합병
        // 메모리 부담 있음
        // 캐시가 아니라 메모리(램)을 쓰기때문에 조금 퀵정렬에 비해 평균적으로 조금 느림
        // 안정 배열이라서 안깨짐

        // 배열을 각각 1개가 될때까지 반갈죽 한다.
        // 서로 같은 크기의 배열끼리 비교해서 작은건 앞쪽, 큰건 뒤쪽에 넣는다.
        // 전부 합쳐질때까지 반복
        public static void MergeSort(IList<int> list, int left, int right)
        {
            if (left == right) return;

            int mid = (left + right) / 2;
            MergeSort(list, left, mid);
            MergeSort(list, mid + 1, right);
            Merge(list, left, mid, right);
        }

        public static void Merge(IList<int> list, int left, int mid, int right)
        {
            List<int> sortedList = new List<int>();
            int leftIndex = left;
            int rightIndex = mid + 1;

            // 분할 정렬된 List를 병합
            while (leftIndex <= mid && rightIndex <= right)
            {
                if (list[leftIndex] < list[rightIndex])
                    sortedList.Add(list[leftIndex++]);
                else
                    sortedList.Add(list[rightIndex++]);
            }

            if (leftIndex > mid)    // 왼쪽 List가 먼저 소진 됐을 경우
            {
                for (int i = rightIndex; i <= right; i++)
                    sortedList.Add(list[i]);
            }
            else  // 오른쪽 List가 먼저 소진 됐을 경우
            {
                for (int i = leftIndex; i <= mid; i++)
                    sortedList.Add(list[i]);
            }

            // 정렬된 sortedList를 list로 재복사
            for (int i = left; i <= right; i++)
            {
                list[i] = sortedList[i - left];
            }
        }

        // <퀵정렬>
        // 하나의 피벗을 기준으로 작은값과 큰값을 2분할하여 정렬
        // 정렬이 깨질 수 있음
        // 최악의 경우(9 8 7 6 5 4 처럼 역순으로 정렬되어잇는 경우) n제곱만큼 오래걸림
        // 캐시를 기깔나게 써서 평균속도가 speeeeeeeed함

        // 1. 배열에서 숫자 하나를 기준으로 잡는다
        // 2. 오른쪽 끝에서부터 처음으로 나온 기준보다 작은 숫자와 왼쪽 끝에서부터 처음으로 나온 기준보다 큰 숫자를 교체
        // 3. 교체한 숫자부터 2번 반복
        // 4. 만약 작은 숫자가 왼쪽에, 큰 숫자가 오른쪽에 있는 경우가 생겼다면
        // == 양쪽에서 중심을 향해 다가오다가 엇갈렸다면 기준과 작은쪽의 숫자를 교체
        // 5. 정렬될때까지 1번부터 반복
        public static void QuickSort(IList<int> list, int start, int end)
        {
            if (start >= end) return;

            int pivotIndex = start;
            int leftIndex = pivotIndex + 1;
            int rightIndex = end;

            while (leftIndex <= rightIndex) // 엇갈릴때까지 반복
            {
                // pivot보다 큰 값을 만날때까지
                while (list[leftIndex] <= list[pivotIndex] && leftIndex < end)
                    leftIndex++;
                while (list[rightIndex] >= list[pivotIndex] && rightIndex > start)
                    rightIndex--;

                if (leftIndex < rightIndex)     // 엇갈리지 않았다면
                    Swap(list, leftIndex, rightIndex);
                else    // 엇갈렸다면
                    Swap(list, pivotIndex, rightIndex);
            }

            QuickSort(list, start, rightIndex - 1);
            QuickSort(list, rightIndex + 1, end);
        }


        private static void Swap(IList<int> list, int left, int right)
        {
            int temp = list[left];
            list[left] = list[right];
            list[right] = temp;
        }
    }
}
