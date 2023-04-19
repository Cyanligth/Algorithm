using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class LinkedListNode<T>
    {
        internal LinkedList<T> linkList;        // 연결리스트의 링크리스트 변수
        internal LinkedListNode<T> next;        // 노드의 넥스트 변수
        internal LinkedListNode<T> prev;        // 노드의 프리브 변수
        private T item;     // 일반화 아이템 변수

        public LinkedList<T> List { get { return linkList; } }      // 리스트 함수, 호출되면 링크리스트 반환
        public LinkedListNode<T> Prev { get { return prev; } }      // 프리브 함수, 호출되면 프리브 반환
        public LinkedListNode<T> Next { get { return next; } }      // 넥스트 함수, 호출되면 넥스트 반환
        public T Value { get { return item; } set { item = value; } }       // 일반화 벨류 함수, 호출되면 아이템을 반환하고 아이템에 받아온 값을 저장

        public LinkedListNode(T value)      // 초기화
        {
            this.linkList = null;
            this.prev = null;
            this.next = null;
            this.item = value;
        }

        public LinkedListNode(LinkedList<T> linkList, T value)      // 오버로딩
        {
            this.linkList = linkList;
            this.prev = null;
            this.next = null;
            this.item = value;
        }

        public LinkedListNode(LinkedList<T> linkList, LinkedListNode<T> prev, LinkedListNode<T> next, T value)      // 오버로딩22
        {
            this.linkList = linkList;
            this.prev = prev;
            this.next = next;
            this.item = value;
        }
    }

    public class LinkedList<T>
    {
        private LinkedListNode<T> head;     // 머리 변수, 연결리스트 제일 앞 주소 저장
        private LinkedListNode<T> tail;     // 꼬리 변수, 연결리스트 제일 끝 주소 저장
        private int count;      // 카운트 변수. 현재 연결리스트에 몇개나 들어잇는지 체크

        public LinkedList()     // 초기화
        {
            this.head = null;
            this.tail = null;
            this.count = 0;
        }

        public LinkedListNode<T> First { get { return head; } }     // 퍼스트 함수, 호출되면 머리 반환
        public LinkedListNode<T> Last { get { return tail; } }      // 라스트 함수, 호출되면 꼬리 반환
        public int Count { get { return count; } }      // 카운트 함수, 호출되면 카운트 반환

        public LinkedListNode<T> AddFirst(T value)      // 제일 앞에 끼워넣기
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);     // 뉴노드 변수에 받아온 값 저장
            if(head != null)    // 머리가 공백이 아닌 경우
            {
                head.prev = newNode;        // 머리의 이전 노드가 뉴노드를 가리킨다
                newNode.next = head;        // 뉴노드의 다음 노드가 머리를 가리킨다
                head = newNode;     // 지금부터 뉴노드가 머리다.
            }
            else        // 그 외의 경우
            {
                head = newNode;     // 뉴노드가 머리이자
                tail = newNode;     // 동시에 꼬리이다
            }
            count++;        // 카운트 +1
            return newNode;     // 뉴노드 반환
        }

        public LinkedListNode<T> AddLast(T value)       // 제일 뒤에 끼워넣기
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);     // 뉴노드 변수에 받아온 값 저장
            if (tail != null)       // 머리가 공백이 아닌 경우
            {
                tail.next = newNode;        // 꼬리의 다음 노드가 뉴노드를 가리킨다
                newNode.prev = tail;        // 뉴노드의 이전 노드가 꼬리를 가리킨다
                tail = newNode;     // 지금부터 뉴노드가 꼬리다
            }
            else        // 그 외의 경우
            {
                head = newNode;     // 뉴노드가 머리이자
                tail = newNode;     // 동시에 꼬리이다
            }
            count++;        // 카운트 +1
            return newNode;     // 뉴노드 반환
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)     // 지정된 노드의 앞에 끼워넣기
        {
            if (node.linkList != this)      // 지정된 노드가 다른 연결리스트에 속해있다면
                throw new InvalidOperationException("node");        // 오류반환
            if (node == null)       // 지정된 노드의 값이 없으면
                throw new ArgumentNullException(nameof(node));      // 오류반환
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);     // 뉴노드 선언하고 받아온 값 저장
            newNode.next = node;        //  뉴노드의 다음은 지정된 노드를 가리킨다
            newNode.prev = node.prev;       // 뉴노드의 이전은 지정된 노드가 이전으로 가리키던 노드를 가리킨다
            node.prev = newNode;        // 지정된 노드의 이전은 뉴노드를 가리킨다
            if (node.prev != null)      // 지정된 노드의 이전이 가리키던 것이 공백이 아니라면
                node.prev.next = newNode;       // 지정된 노드의 이전이 가리키던 노드의 다음은 뉴노드를 가리킨다
            else        // 그 외의 경우
                head = newNode;     // 뉴노드는 지금부터 머리다.
            count++;        // 카운트+1
            return newNode;     // 뉴노드 반환
        }

        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)     // 지정된 노드의 앞에 끼워넣기
        {
            if (node.linkList != this)      // 지정된 노드가 다른 연결리스트에 속해있다면
                throw new InvalidOperationException("node");        // 오류반환
            if (node == null)       // 지정된 노드의 값이 없으면
                throw new ArgumentNullException(nameof(node));      // 오류반환
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);     // 뉴노드 선언하고 받아온 값 저장
            newNode.prev = node;        //  뉴노드의 이전은 지정된 노드를 가리킨다
            newNode.next = node.next;       // 뉴노드의 다음은 지정된 노드가 다음으로 가리키던 노드를 가리킨다
            node.next = newNode;        // 지정된 노드의 다음은 뉴노드를 가리킨다
            if (node.next != null)      // 지정된 노드의 다음이 가리키던 것이 공백이 아니라면
                node.next.prev = newNode;       // 지정된 노드의 다음이 가리키던 노드의 이전은 뉴노드를 가리킨다
            else        // 그 외의 경우
                tail = newNode;     // 뉴노드는 지금부터 꼬리다.
            count++;        // 카운트+1
            return newNode;     // 뉴노드 반환
        }

        public void Remove(LinkedListNode<T> node)      // 받아온 값을 연결리스트에서 지우기
        {
            if (node.linkList != this)      // 받아온 값이 현재 연결리스트에 없는 경우
                throw new InvalidOperationException("node");        // 오류 반환
            if (node == null)       // 받아온 값이 공백이면
                throw new ArgumentNullException(nameof(node));      // 오류 반환           

            if (node.prev != null)      // 받아온 노드의 이전이 공백을 가리키고 있지 않다면 == 머리가 아니라면
                node.prev.next = node.next;     // 받아온 노드의 이전이 가리키는 노드의 다음은 노드의 다음이 가리키는 주소를 가리킨다.
            else        // 그 외의 경우 == 받아온 값이 머리면
                head = node.next;       // 다음 노드가 머리가 된다
            if (node.next != null)      // 받아온 노드의 다음이 공백을 가리키고 있지 않다면 == 꼬리가 아니라면
                node.next.prev = node.prev;     // 받아온 노드의 다음이 가리키는 노드의 이전은 노드의 이전이 가리키는 주소를 가리킨다.
            else        // 그 외의 경우 == 받아온 값이 꼬리면
                tail = node.prev;       // 이전 노드가 꼬리가 된다
            count--;        // 카운트 -1
        }

        public bool Remove(T value)     // 지우기
        {
            LinkedListNode<T> find = Find(value);       // 찾기 함수 호출해서 파인드 변수에 결과 저장
            if (find != null)       // 파인드가 공백이 아니라면 == 찾기 함수에서 뭔가를 찾았으면
            {
                Remove(find);       // 이거 말고 위에 지우기 함수에 파인드 넣어서 지운다 
                return true;        // 참 반환 == 지우는거 성공
            }
            else return false;      // 거짓 반환 == 못지움
        }

        public void RemoveFirst()       // 연결리스트 머리 지우기
        {
            if (head == null && tail == null)       // 머리랑 꼬리가 둘 다 공백이면 == 리스트가 비어있으면
                throw new InvalidOperationException();      // 오류반환
            head.next.prev = null;      // 머리의 다음이 가리키는 노드의 이전이 공백을 가리키게 한다
            head = head.next;       // 머리의 다음이 가리키는 노드를 머리로 설정
            count--;        // 카운트 -1
        }

        public void RemoveLast()        // 연결리스트 꼬리 지우기
        {
            if (head == null && tail == null)       // 머리랑 꼬리가 둘 다 공백이면 == 리스트가 비어있으면
                throw new InvalidOperationException();      // 오류반환
            tail.prev.next = null;      // 꼬리의 이전이 가리키는 노드의 다음이 공백을 가리키게 한다
            tail = tail.prev;       // 머리의 이전이 가리키는 노드를 꼬리로 설정
            count--;        // 카운트 -1
        }

        public LinkedListNode<T> Find(T value)      // 연결리스트에서 받아온 값 찾기
        {
            LinkedListNode<T> target = head;        // 시작부분을 머리로 정한다.
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;     // 일반화 형식 비교를 위한 무언가
            while (target != null)      // 타겟이 공백이 될때까지 반복
            {
                if (comparer.Equals(value, target.Value))   // 현재 값(시작은 머리)과 받아온 값을 비교한 결과가 참이라면
                    return target;      // 현재 값 반환
                target = target.next;       // 타겟을 타겟의 다음이 가리키는 값으로 바꾼다.
            }
            return null;        // 똑같은걸 한번도 못찾으면 공백 반환
        }

        public LinkedListNode<T> FindLast(T value)      // 연결리스트에서 받아온 값 찾기
        {
            LinkedListNode<T> target = tail;        // 시작부분을 꼬리로 정한다.
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;     // 일반화 형식 비교를 위한 무언가
            while (target != null)      // 타겟이 공백이 될때까지 반복
            {
                if (comparer.Equals(value, target.Value))   // 현재 값(시작은 꼬리)과 받아온 값을 비교한 결과가 참이라면
                    return target;      // 현재 값 반환
                target = target.prev;       // 타겟을 타겟의 이전이 가리키는 값으로 바꾼다.
            }
            return null;        // 똑같은걸 한번도 못찾으면 공백 반환
        }

        public void Clear()     // 연결리스트 초기화
        {
            head = null;        // 머리주소를 공백으로
            tail = null;        // 꼬리주소를 공백으로
            count = 0;      // 카운트를 0으로
        } // 결과 = 연결리스트를 가리키는 포인터가 안남아서 GC가 맛있게 먹어버립니다

        public bool Contains(T value)       // 받아온 값을 연결리스트가 포함하고 있는지 참/거짓으로 반환
        {
            if (Find(value) != null)        // 찾기 함수 호출, 결과가 공백이 아니라면 == 같은걸 찾았으면
                return true;        // 참 반환
            return false;       // 못찾으면 거짓 반환
        }
    }
}
