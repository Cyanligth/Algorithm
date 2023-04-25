using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataStructure
{
    public class BinarySearchTree<T> where T : IComparable<T>   // 이진탐색나무, 일반화, 비교가능
    {
        private Node root;      // 나무의 시작=기준=뿌리 변수 선언

        public BinarySearchTree()   // 초기화
        {
            this.root = null;
        } 

        public void Add(T item)     // 나무에 가지 추가하기
        {
            Node node = new Node(item, null, null, null);       // 새로운 노드 만들어서 받아온 값 저장

            if(root == null)        // 만약 루트가 공백이면 == 나무가 없으면
            {
                root = node;        // 루트에 새로 만든 노드 저장 == 새로만든 노드가 이제부터 루트
                return;     // 반환
            }

            Node now = root;    // 새로운 노드(현재값 저장) 선언하고 뿌리값 저장
            while(now != null)      // 현재 노드가 공백이 아닌 한 반복 == 처음부터 끝까지 반복
            {
                if (item.CompareTo(now.item) < 0)   // 받아온 값을 현재 노드의 값과 비교, 받아온 값이 더 작은 경우
                {
                    if(now.left != null)    // 현재 노드의 왼쪽자식노드가 존재하면 
                    {
                        now = now.left;     // 현재 노드는 방금까지 현재였던 노드의 왼쪽자식노드가 된다.
                                            // == 내려가서 다시 비교
                    }
                    else        // 그외, 왼쪽자식노드가 없다면
                    {
                        now.left = node;    // 현재 노드의 왼쪽자식노드는 새로 만든 노드가 되고
                        node.parent = now;  // 새로만든 노드의 부모노드는 현재 노드가 된다.
                        return;     // 반환
                    }
                }
                else if (item.CompareTo(now.item) > 0)      // 받아온 값을 현재 노드의 값과 비교, 받아온 값이 더 큰 경우
                {
                    if(now.right != null)   // 현재 노드의 오른쪽 자식노드가 존재하면
                    {
                        now = now.right;    // 현재 노드는 방금까지 현재였던 노드의 오른쪽자식노드가 된다.
                                            // == 내려가서 다시 비교
                    }
                    else        // 그외, 오른쪽자식노드가 없다면
                    {
                        now.right = node;   // 현재 노드의 오른쪽 자식노드는 새로 만든 노드가 되고
                        node.parent = now;  // 새로만든 노드의 부모노드는 현재 노드가 된다.
                        return;     // 탈주
                    }
                }
                else return;        // 탈주
            }
        }   // 결과, 뿌리부터 하나씩 비교해가며 핀볼처럼 튕기면서 더 못내려갈때까지 아래로 내려간다.

        public bool TryGetValue(T item, out T outValue)     // 값 찾기 시도, 찾으면 참 반환하고 값 출력, 못찾으면 거짓 반환
        {
            Node node = Find(item);     // 새로운 노드 선언, 찾기함수에 받아온 값 넣어서 호출
            if (node == null)   // 노드가 공백이면
            {
                outValue = default;     // 반환값에 기본값 저장
                return false;   // 거짓 반환
            }
            else    // 그 외의 경우, 노드에 뭔가 들어있으면
            {
                outValue = node.item;   // 반환값에 노드의 값 저장
                return true;        // 참 반환
            }

        }

        public Node Find(T item)    // 입력받는 값 찾는 함수
        {
            if (root == null)       // 루트가 공백이면
            {
                return null;        // 공백 반환
            }
            Node now = root;    // 현재노드선언, 루트 저장
            while (now != null)     // 현재노드가 공백이 아닌 한 반복
            {
                if (item.CompareTo(now.item) < 0)   // 받아온 값을 현재노드의 값과 비교, 받아온쪽이 더 작으면
                {
                    now = now.left;     // 현재 노드는 현재노드 였던 것의 왼쪽자식노드가 된다.
                }
                else if (item.CompareTo(now.item) > 0)  // 받아온 값을 현재노드의 값과 비교, 받아온쪽이 더 크면
                {
                    now = now.right;    // 현재 노드는 현재노드 였던 것의 오른쪽자식노드가 된다.
                }
                else return now;    // 그외 == 받아온 값이랑 현재노드의 값이 같으면 현재 노드 반환 
            }
            return null;    // 반복문 끝날때까지 반환이 안되면 == 못찾으면 공백 반환
        }

        public bool Remove(T item)      // 받아온 값 지우고 성공여부 알려주기
        {
            Node node = Find(item);     // 새로 선언한 노드에 받아온 값 찾아서 넣기
            if(node == null)        // 노드가 공백이면 == 지울 값이 존재하지 않으면
                return false;       // 거짓 반환
            else        // 그 외, 노드에 뭔가 들어있으면
            {
                Erase(node);        // 지우기 함수에 노드 넣어서 호출
                return true;        // 참 반환
            }
        }

        private void Erase(Node node)   // 받아온 노드 지우기
        {
            if(node.NoChild)        // 받아온 노드의 자식노드가 없으면
            {
                if (node.IfLeft)    // 받아온 노드가 부모노드의 왼쪽 자식이면
                    node.parent.left = null;    // 받아온 노드의 부모노드의 왼쪽자식주소를 공백으로 설정
                else if (node.IfRight)  // 받아온 노드가 부모노드의 오른쪽 자식이면
                    node.parent.right = null;   // 받아온 노드의 부모노드의 오른쪽자식주소를 공백으로 설정
                else root = null;   // 그 외 == 받아온 노드의 부모노드가 없으면 == 받아온 노드가 루트라면 루트를 공백으로
            }
            else if(node.RightOnly || node.Leftonly)    // 받아온 노드가 자식노드를 하나만 가지고있으면
            {
                Node parent = node.parent;  // 부모노드 선언, 받아온 노드의 부모노드 저장
                Node child = node.Leftonly ? node.left : node.right;    
                // 자식노드 선언, 받아온 노드가 왼쪽자식노드만 가지고 있다면 왼쪽자식노드 저장, 그 외의 경우 오른쪽 자식노드 저장
                if (node.IfLeft)    // 받아온 노드가 부모노드의 왼쪽 자식이면
                {
                    parent.left = child;    // 부모노드의 왼쪽아래자식주소에 자식노드 저장
                    child.parent = parent;  // 자식노드의 부모노드주소에 부모노드 저장
                }
                else if (node.IfRight)      // 받아온 노드가 부모노드의 오른쪽 자식이면
                {
                    parent.right = child;   // 부모노드의 오른쪽아래자식주소에 자식노드 저장
                    child.parent = parent;  // 자식노드의 부모노드주소에 부모노드 저장
                }
                else        // 그 외 == 받아온 노드의 부모노드가 없으면 == 받아온 노드가 루트라면
                {
                    root = child;   // 루트에 자식노드 저장
                    child.parent = null;    // 자식노드의 부모노드 주소를 공백으로
                    // == 자식노드를 루트로 만든다.
                }
            }
            else         // 받아온 노드가 자식노드를 양쪽 다 가지고 있으면
            {
                Node big = node.left;   // 빅 노드 선언, 받아온 노드의 왼쪽자식노드 저장
                while(big.right != null)    // 빅노드의 오른쪽자식노드가 공백이 아닌 한 반복
                    big = big.right;    // 빅노드에 빅노드의 오른쪽자식노드 저장
                node.item = big.item;   // 노드의 값에 빅노드의 값 저장
                Erase(big);     // 빅노드 지우기
            }   // 결과, 받아온 노드에서 한칸 왼쪽으로 갔다가 오른쪽으로 끝까지 내려가서 나온 값을 
                // 지우고 싶은 노드에 덮어씌우고
                // 복사당한 노드를 본 함수에 넣어서 지워버린다.
        }

        public void Clear()     // 초기화
        {
            root = null;        
            // 뿌리를 공백으로 == 뿌리를 기준으로 참조되고 있던 가지와 잎들을 가비지 컬렉터가 정말 맛있게 먹어버린다.
        }

        public void Print()     // 오름차순 정렬 오버로딩
        {
            Print(root);    // 뿌리 기준으로 정렬
        }
        public void ReversePrint()      // 내림차순 정렬 오버로딩
        {
            ReversePrint(root);     // 뿌리 기준으로 정렬
        }
        public void Print(Node node)        // 받아온 값 기준으로 중위순회 오름차순 정렬
        {
            if(node.left != null) Print(node.left);     // 기준노드의 왼쪽이 공백이 아니라면 기준노드의 왼쪽을 본 함수에 넣어서 실행
            Console.WriteLine(node.item);       // 노드값 출력
            if(node.right != null) Print(node.right);   // 기준노드의 오른쪽이 공백이 아니라면 기준노드의 오른쪽을 본 함수에 넣어서 실행
        }   // 결과, 제일 왼쪽부터 하나씩 출력됨
        public void ReversePrint(Node node)     // 받아온 값 기준으로 중위순회 내림차순 정렬
        {
            if (node.right != null) Print(node.right);  // 기준노드의 오른쪽이 공백이 아니라면 기준노드의 오른쪽을 본 함수에 넣어서 실행
            Console.WriteLine(node.item);       // 노드값 출력
            if (node.left != null) Print(node.left);    // 기준노드의 왼쪽이 공백이 아니라면 기준노드의 왼쪽을 본 함수에 넣어서 실행
        }   // 결과, 제일 오른쪽부터 하나씩 출력됨


        public class Node       // 노드 클래스
        {
            public T item;      // 일반화 노드값
            public Node parent;     // 부모 노드 주소
            public Node left;       // 왼쪽 아래 노드 주소
            public Node right;      // 오른쪽 아래 노드 주소

            public Node(T item, Node parent, Node left, Node right) // 초기화
            {
                this.item = item;
                this.parent = parent;
                this.left = left;
                this.right = right;
            }
            public T Item { get { return item; } }      // 노드값 반환
            public Node Parent { get { return parent; } }       // 부모노드 반환
            public Node Left { get { return left; } }       // 왼쪽자식노드반환
            public Node Right { get { return right; } }     // 오른쪽자식노드반환
            public bool NoChild { get { return left == null && right == null; } }   // 자식노드가 없으면 참 반환
            public bool Leftonly { get { return left != null && right == null; } }  // 자식노드가 왼쪽만 있으면 참 반환
            public bool RightOnly { get { return left == null && right != null; } } // 자식노드가 오른쪽만 있으면 참 반환
            public bool IfLeft { get { return parent != null && parent.left != this; } }
            // 부모노드가 존재하고=루트가 아니고 부모노드의 왼쪽자식노드가 자기자신이라면 참 반환
            public bool IfRight { get { return parent != null && parent.right != this; } }
            // 부모노드가 존재하고=루트가 아니고 부모노드의 오른쪽자식노드가 자기자신이라면 참 반환
        }
    }
}
