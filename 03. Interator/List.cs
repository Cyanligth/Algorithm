using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator
{
    public class List<T> : IEnumerable<T>, IList<T>
    {
        private const int baseCount = 20;       // 리스트 기본 길이를 20으로 지정
        private T[] array;      // 리스트로 미리 정할 배열 선언
        private int size;       // 정수형 크기 변수 선언
        public int Count { get { return size; } }       // 카운트 함수 선언, 현재 사용되고 있는 배열의 길이를 받아온다.
        public int Capacity { get { return array.Length; } }        // 카파시티 함수 선언, 배열의 총 길이를 가져온다.

        public bool IsReadOnly => throw new NotImplementedException();

        public List()     // 초기화
        {
            this.array = new T[baseCount];      // 어레이 배열을 일반화 형식의 베이스 카운트 길이를 가진 배열로 초기화
            this.size = 0;                      // 기본 길이를 0으로 초기화
        }

        public T this[int index]        // 인덱서
        {
            get
            {
                if (index < 0 || index >= Count) { throw new ArgumentOutOfRangeException("index"); }
                // 인덱스가 0보다 작거나 사용되고 있는 배열의 길이보다 크거나 같다면 범위를 벗어남 오류 반환
                return array[index];    // 인덱스 번째의 배열값 반환
            }
            set
            {
                if (index < 0 || index >= Count) { throw new ArgumentOutOfRangeException("index"); }
                // 인덱스가 0보다 작거나 사용되고 있는 배열의 길이보다 크거나 같다면 범위를 벗어남 오류 반환
                array[index] = value;   // 배열의 인덱스번째에 받아온 값을 저장한다.
            }
        }

        public void Add(T array)    // 배열에 값을 저장하는 함수
        {
            if (size < baseCount)   // 사용되고 있는 배열의 길이가 배열의 총 길이보다 작다면
            {
                this.array[size++] = array;     // 받아온 값을 사이즈 번째의 배열에 저장하고 사이즈를 1 올린다.
            }
            else        // 그 외의 경우(사용되고 있는 배열의 길이가 배열의 총 길이를 넘었을 경우)
            {
                Grow();     // 그로우 함수 호출 - 배열의 길이를 늘린다
                this.array[size++] = array;     // 받아온 값을 사이즈 번째의 배열에 저장하고 사이즈를 1 올린다.
            }
        }

        public bool Remove(T array)     // 참 거짓의 값을 반환하는 리무브 변수. 값을 제거하고 싶을떄 이게 호출된다.
        {
            int index = IndexOf(array);     // 인덱스오브 함수에 받아온 값을 넣어 반환받은 값을 정수형 인덱스 변수에 저장
            if (index >= 0)     // 인덱스가 0보다 크거나 같을 경우
            {
                RemoveAt(index);        // 리무브엣 함수에 인덱스 값을 넣어 실행하고 참 반환
                return true;
            }
            return false;       // 거짓 반환
        }

        public void RemoveAt(int index)     // 리무브엣 함수. 배열에서 값을 직접 제거하는 함수다.
        {
            if (index < 0 || index >= Count) { throw new ArgumentOutOfRangeException("index"); }
            // 받아온 인덱스가 0보다 작거나 사용되고 있는 배열의 길이보다 크거나 같다면 범위를 벗어남 오류 반환
            size--;     // 배열의 길이를 하나 줄인다.
            Array.Copy(array, index + 1, array, index, size - index);
            // 제거하기 원하는 값의 인덱스를 받아왔기에 기존 배열에서 해당 인덱스에서 1을 더한 부분부터 배열의 끝까지를
            // 인덱스부터 덮어씌운다 = 한칸씩 앞으로 당긴다.
        }

        public int IndexOf(T array)     // 인덱스오브 함수. 원하는 값의 위치를 찾는다.
        {
            return Array.IndexOf<T>(this.array, array, 0, size);    // 받아온 값이 배열의 몇번째에 있는지 찾아서 반환.
        }

        public void Clear()     // 리스트를 초기화하는 함수
        {
            array = new T[baseCount];       // 배열의 주소를 새로 만들어서 기존 배열은 가비지 컬렉터가 먹어버리게 한다.
            size = 0;
        }

        public void Grow()      // 배열의 길이를 늘리는 함수. 사실 늘리는게 아니라 새로 만드는거다.
        {
            int newLength = array.Length * 2;      // 새오운 배열 길이를 현재 배열의 길이의 2배로 정한다.
            T[] newArray = new T[newLength];        // 일반화 형식의 새로운 배열 선언. 길이는 위에서 정한대로 기존의 2배.
            Array.Copy(array, newArray, size);      // 기존 배열의 값을 새로운 배열로 복사한다
            array = newArray;       // 기존 배열의 주소를 새로운 배열로 바꾼다
        }

        public T? Find(Predicate<T> match)      // 해당 값이 배열에 존재하는지 찾는다.
        {
            if (match == null) { throw new ArgumentNullException("match"); }
            // 받아온 값이 비어있으면 공백 오류 반환
            for (int i = 0; i < size; i++)       // 현재 사용되는 배열의 길이만큼 반복
            {
                if (match(array[i])) { return array[i]; }
                // 배열의 i번째 값이 받아온 값과 같다면 배열의 i번째 값을 반환
            }
            return default(T);      // 그렇지 않다면 기본값 반환
        }

        public int FindIndex(Predicate<T> match)        // 해당 값이 배열의 몇번째 위치에 있는지 찾는다.
        {
            if (match == null) { throw new ArgumentNullException("match"); }
            // 받아온 값이 비어있으면 공백 오류 반환
            for (int i = 0; i < size; i++)      // 현재 사용되는 배열의 길이만큼 반복
            {
                if (match(array[i])) { return i; }
                // 배열의 i번째 값이 받아온 값과 같다면 i를 반환
            }
            return -1;      // 그렇지 않다면 -1 반환
        }

        public bool Contains(T array)       // 해당 값이 배열에 존재하는지 찾아서 참/거짓으로 반환
        {
            if (IndexOf(array) > -1)      // 인덱스오브 함수 호출(해당 값이 배열의 몇번째에 있는지 반환)
            {
                return true;        // 참이라면 참 반환
            }
            else { return false; }      // 그 외의 경우 거짓 반환
        }

        public void Insert(int index, T array)      // 인덱스번쨰의 배열에 받아온 값을 넣는다
        {
            this.array[index] = array;      // 위에 쓴 그대로 썼어요
        }

        public void CopyTo(T[] array)    // 리스트를 1차원 배열에 복사
        {
            Array.Copy(this.array, array, 0);
        }

        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(this.array, array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()       // 반복기 호출 함수
        {
            return new Enumerator(this);        // 반복기 새로 만들어서 반환
        }

        IEnumerator IEnumerable.GetEnumerator()     // 반복기 호출 오버로딩
        {
            return new Enumerator(this);        // 반복기 새로 만들어서 반환
        }

        public struct Enumerator : IEnumerator<T>       // 반복기 구조체 선언
        {
            private List<T> list;       // 리스트 형식 리스트 변수
            private int index;      // 정수형 인덱스
            private T current;      // 일반화 현재값

            internal Enumerator(List<T> list)       // 초기화
            {
                this.list = list;       
                this.index = 0;
                this.current = default(T);      
            }

            public T Current { get { return current; } }        // 호출되면 현재값을 반환하는 일반화 커런트 함수

            object IEnumerator.Current      // 오브젝트형 반복기의 현재값
            {
                get 
                {
                    if (index < 0 || index >= list.Count)       // 만약 반복기의 위치가 0보다 작거나 사용되고 있는 리스트의 길이보다 같거나 더 멀리에 있는 경우
                        throw new InvalidOperationException();      // 에러 반환
                    return Current;     // 커런트 함수 호출 = 현재값 반환
                }
            }

            public void Dispose() { }   // foreach문 종료시에 호출되는거. 대충 마무리 함수? 특별히 필요는 없기에 비워둔다.

            public bool MoveNext()      // 무브넥스트 함수, 반복기의 위치를 옮겨주고 참/거짓을 반환한다.
            {
                if (index < list.Count)     // 반복기의 위치가 사용되고 있는 리스트의 길이보다 작은 경우
                {
                    current = list[index++];        // 현재값은 리스트의 인덱스 번째(반복기의 위치)값이 된다. 그 후 인덱스 +1
                    return true;        // 참 반환
                }
                else        // 그 외의 경우 = 반복기가 리스트를 다 돌고 끝을 넘어간경우
                {
                    current = default(T);       // 현재값을 기본값으로
                    index = list.Count;     // 반복기를 제일 뒤로
                    return false;       // 거짓 반환
                }
            }

            public void Reset()     // 초기화
            {
                index = 0;      // 반복기 위치를 처음으로 초기회
                current = default(T);       // 현재값을 기본값으로
            }
        }
        public int Average(List<int> list)
        {
            int sum = 0;
            int ave = 0;
            foreach (int i in list) { sum = sum + i; }
            ave = sum / list.Count;
            return ave;
        }
    }
}
