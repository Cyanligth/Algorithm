using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class Queue<T>       // 큐 클래스
    {
        const int DefaultCount = 10;        // 큐의 기본길이를 10으로 설정
        int first;      // 큐의 처음부분 = 출력할 부분
        int last;       // 큐의 제일 마지막 = 입력받을 부분
        T[] array;      // 일반화 배열, 큐가 저장될 곳
        public Queue()      // 평범하고 무난한 초기화
        {
            this.array = new T[DefaultCount];
            this.first = 0;
            this.last = 0;
        }

        public int Count        // 큐에 몇개나 들었는지 세는 함수
        {
            get     // 호출되면
            {
                if (first <= last)      // 만약 큐의 머리가 꼬리보다 작으면 == 배열에서 더 앞쪽 인덱스에 있으면
                    return last - first;        // 꼬리-머리 == 제일 뒤의 인덱스-제일 앞의 인덱스 반환
                else         // 그 외의 경우 == 꼬리가 머리보다 앞쪽 인덱스에 있으면
                    return last - first + array.Length;     // 꼬리의 인덱스 - 머리의 인덱스 + 배열의 길이 반환
            }
        }

        public void Enqueue(T item)     // 큐에 받아온 값 저장
        {
            if (Full())     // 큐 배열이 꽉 찼는지 체크
            {
                Grow();     // 꽉찼으면 그로우함수 호출
            }
            array[last++] = item;       // 배열의 꼬리위치에 받아온 값을 넣고 꼬리값 +1
            if(last == array.Length)    // 만약 꼬리의 인덱스가 배열의 길이와 같을 경우 == 배열의 마지막에서 한칸 더 갔을경우
            {
                last = 0;       // 꼬리를 배열의 0번으로 옮긴다.
            }
        }
        
        public T Dequeue()      // 큐의 우선값 == 제일 처음에 들어온 값 빼내기 == 출력 후 삭제
        {
            if(Empty())     // 배열이 비어있는지 확인
                throw new InvalidOperationException();  // 비었으면 오류 반환
            T item = array[first++];        // 일반화 아이템 변수에 큐의 머리 저장 후 머리인덱스+1
            if (first == array.Length)      // 머리인덱스가 배열의 길이와 같다면 == 머리가 배열의 마지막을 넘어갔다면
            {
                first = 0;      // 머리인덱스를 0으로 == 배열의 제일 앞으로
            }
            return item;        // 아이템 반환
        }

        public T Peek()     // 디큐를 실행할때 제일 먼저 출력될 값을 보여주는 함수
        {
            if (Empty())        // 큐가 비어있으면
                throw new InvalidOperationException();  // 오류반환
            T item = array[first];      // 일반화 아이템 변수에 큐에 제일 먼저 들어온 값 저장
            return item;        // 아이템 반환
        }

        private void Grow()     // 큐의 크기를 늘려주는 함수
        {
            int newCount = array.Length * 2;        // 큐의 새 길이를 현재 큐 길이의 2배로 설정
            T[] newArray = new T[newCount];     // 새로운 일반화 배열 생성, 길이는 위에서 설정한대로
            if(first < last)        // 머리가 꼬리보다 작으면 == 배열상에서 머리인덱스가 꼬리인덱스보다 작으면
                Array.Copy(array, newArray, Count);     // 기존 배열를 그대로 새로운 배열에 복사
            else        // 그 외의 경우 == 꼬리인덱스가 머리인덱스보다 앞에 있을경우
            {
                Array.Copy(array, first, newArray, 0, array.Length - first);
                // 기존 배열을 머리인덱스부터 마지막까지 복사해서 새로만든 배열의 0번부터 붙여넣는다
                Array.Copy(array, 0, newArray, array.Length - first, last);
                // 기존 배열의 0번부터 꼬리인덱스까지 복사해서 새로만든 배열의 빈공간부터(==위에서 사용한 공간의 바로 다음부터) 붙여넣는다.
                first = 0;      // 머리를 배열의 제일 처음으로
                last = Count;       // 꼬리를 새로만든 배열의 제일 처음으로 비어있는 위치에 이동
            }
            array = newArray;       // 새로 만든 배열 반환
        }

        private bool Empty()        // 배열이 비었으면 참, 뭐라도 들어있으면 거짓 반환하는 함수
        {
            return first == last;       // 머리와 꼬리가 같은 위치면 참 반환
        }
        private bool Full()     // 배열이 가득 차있으면 참, 아니면 거짓 반환
        {
            if (first > last)       // 머리가 꼬리보타 크면 / 머리가 0번 이외의 위치에 있으면서 배열이 가득 차는 경우 
                return first == last + 1;       // 꼬리인덱스에 1을 더한값이 머리인덱스와 같으면 참 반환 아니면 거짓 == 꼬리가 머리 바로 뒤
            else        // 그 외의 경우, 머리가 꼬리보다 작으면
                return first == 0 && last == array.Length - 1;  // 머리인덱스가 0이고 꼬리인덱스가 배열의 제일 끝에 있으면 참 반환 아니면 거짓
        }
    }
}
