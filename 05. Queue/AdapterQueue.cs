using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class AdapterQueue<T>
    {
        private LinkedList<T> linkList;

        public AdapterQueue()
        {
            linkList = new LinkedList<T>();
        }

        public void Enqueue(T item) 
        {
            linkList.AddLast(item);
        }

        public T Dequeue()
        {
            T item = linkList.First<T>();
            linkList.RemoveFirst();
            return item; 
        }

        public T Peek()
        {
            T item = linkList.First<T>();
            return item;
        }
    }
}
