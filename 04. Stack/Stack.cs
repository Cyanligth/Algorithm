using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class Stack<T>       // 일반화 스택 클래스
    {
        private List<T> list;       // 리스트 형식 리스트 변수
        public Stack()      // 초기화
        {
            this.list = new List<T> ();
        }
        public void Push(T item)    // 스택에 받아온 값 저장
        {
            list.Add(item);     // 리스트 안에 저장
        }

        public T Pop()      // 스택의 우선값=제일 나중에 저장된 값 출력 후 삭제
        {
            T item = list[list.Count - 1];      // 일반화 아이템 변수에 리스트의 마지막 값을 저장
            list.RemoveAt(list.Count - 1);      // 리스트의 마지막 값 삭제
            return item;        // 아이템 변수 값 반환
        }

        public T Peek()     // 스택의 제일 나중에 입력된 값 출력만 하기
        {
            return list[list.Count - 1];    // 리스트의 제일 마지막 값 반환
        }

        public int Count()      // 현재 스택에 몇개 들었는지 알려주는 함수
        {
            return list.Count;      // 리스트에서 사용되고 있는 칸수 반환
        }
    }
}
