using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class Dictionary<TKey, TValue> where TKey: IEquatable<TKey>  // 사전 클래스, 일반화 키와 일반화 값 사용, 키는 비교 가능한 값
    {
        private const int defaultCount = 1000;  // 리스트의 기본길이 설정

        private struct Table    // 테이블 구조체
        {
            public enum State { None, Using, Deleted }  // 상태 열거형

            public State state;     // 상태 변수
            public TKey key;        // 키 변수
            public TValue value;    // 값 변수
        }

        private Table[] table;      // 테이블형식 테이블 선언

        public Dictionary()     // 초기화
        {
            table = new Table[defaultCount];
        }

        public void Add(TKey key, TValue value)     // 받아온 키랑 값으로 사전에 추가하기
        {
            int index = Math.Abs(key.GetHashCode() % table.Length);     // 열쇠를 인덱스로 해시ing

            while (table[index].state == Table.State.Using)     // 현재 테이블이 사용되고 있는 한 == 값이 들어있는 한 반복
            {
                if (key.Equals(table[index].key))       // 비교, 받아온 키랑 현재 테이블의 키, 같을 경우
                    throw new ArgumentException();  // 오류반환, 이미 존재하는 키를 다시 입력함.
                else        // 그 외, 받아온 키랑 현재 테이블의 키가 다를경우
                    index = index < table.Length ? index+1 : 0; // 인덱스 +1, 만약 인덱스가 테이블의 끝을 넘어갔으면 0번으로 되돌림
            }
            // 위를 다 지나왔다면 == 입력받은 키와 값을 저장할 수 있는 빈 테이블을 만났다면
            table[index].key = key;     // 현재 테이블의 키 자리에 받아온 키 저장
            table[index].value = value;     // 현재 테이블의 값 자리에 받아온 값 저장
            table[index].state = Table.State.Using;     // 현재 테이블의 상태를 사용중으로 변경
        }

        public TValue this[TKey key]    // 입력받은 값 찾아오거나 덮어쓰기
        {
            get
            {
                int index = Math.Abs(key.GetHashCode() % table.Length);     // 열쇠를 인덱스로 해시ing

                while (table[index].state != Table.State.None)     // 현재 테이블이 비어있지 않은 한 반복
                {
                    if (key.Equals(table[index].key))       // 비교, 받아온 키랑 현재 테이블의 키, 같을 경우
                        return table[index].value;      // 현재 테이블의 값 반환
                    index = index < table.Length ? index + 1 : 0;   // 인덱스+1, 만약 인덱스가 테이블의 끝을 넘어갔으면 0번으로 되돌림
                }
                throw new KeyNotFoundException();   // 오류반환, 입력받은 값을 찾을 수 없음
            }
            set
            {
                int index = Math.Abs(key.GetHashCode() % table.Length);     // 열쇠를 인덱스로 해시ing

                while (table[index].state != Table.State.None)     // 현재 테이블이 비어있지 않은 한 반복
                {
                    if (key.Equals(table[index].key))       // 비교, 받아온 키랑 현재 테이블의 키, 같을 경우
                    {
                        table[index].value = value;     // 현재 테이블에 값을 받아온 값으로 덮어쓰기
                        return;     // 반환
                    }
                    index = index < table.Length ? index + 1 : 0;   // 인덱스+1, 만약 인덱스가 테이블의 끝을 넘어갔으면 0번으로 되돌림
                }
            }
        }

        public bool Remove(TKey key)        // 받아온 값 사전에서 지우고 성공여부 반환
        {
            int index = Math.Abs(key.GetHashCode() % table.Length);     // 열쇠를 인덱스로 해시ing
            while (table[index].state != Table.State.None)     // 현재 테이블이 비어있지 않은 한 반복
            {
                if (key.Equals(table[index].key))       // 비교, 받아온 키랑 현재 테이블의 키, 같을 경우
                {
                    table[index].state = Table.State.Deleted;       // 현재 테이블의 상태를 지워짐으로 변경
                    return true;        // 참 반환
                }
                index = index < table.Length ? index + 1 : 0;       // 인덱스+1, 만약 인덱스가 테이블의 끝을 넘어갔으면 0번으로 되돌림
            }
            return false;       // 여기까지 왔으면 삭제 실패니까 거짓 반환
        }

    }
}
