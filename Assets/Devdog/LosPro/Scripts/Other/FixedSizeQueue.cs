using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Devdog.LosPro
{
    public class FixedSizeQueue<T> : IEnumerable<T>
    {
        private Queue<T> _queue;

        public int maxCount = 1;
        public int count
        {
            get { return _queue.Count; }
        }

        public FixedSizeQueue(int maxCount)
        {
            this.maxCount = Mathf.Max(maxCount, 1);
            _queue = new Queue<T>(maxCount + 1); // +1 because it's + 1 before dequeue
        }

        public void Clear()
        {
            _queue.Clear();
        }

        public void Enqueue(T obj)
        {
            _queue.Enqueue(obj);
            if (count > maxCount)
            {
                Dequeue();
            }
        }

        public void Dequeue()
        {
            _queue.Dequeue();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
