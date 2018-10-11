using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IteratorPatternApply
{
    public class Vector<T>
    {
        public class Iterator
        {
            protected bool Equals(Iterator other)
            {
                return CurIndex == other.CurIndex && Equals(Vector, other.Vector);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Iterator) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (CurIndex*397) ^ (Vector != null ? Vector.GetHashCode() : 0);
                }
            }

            public int CurIndex { get; set; }
            public Vector<T> Vector { get; set; }

            public Iterator(Vector<T> vector)
            {
                CurIndex = 0;
                Vector = vector;
            }

            public bool HasNext()
            {
                return CurIndex < Vector.Size;
            }

            public void Next()
            {
                ++CurIndex;
            }

            public T GetIndexValue()
            {
                return Vector.At(CurIndex);
            }

            public static Iterator operator +(Iterator iterator, int offset)
            {
                Iterator newIterator = new Iterator(iterator.Vector) {CurIndex = iterator.CurIndex + offset};
                return newIterator;
            }

            public static Iterator operator ++(Iterator iterator)
            {
                Iterator newIterator = new Iterator(iterator.Vector) {CurIndex = iterator.CurIndex + 1};
                return newIterator;
            }

            public static Iterator operator --(Iterator iterator)
            {
                Iterator newIterator = new Iterator(iterator.Vector) {CurIndex = iterator.CurIndex - 1};
                return newIterator;
            }

            public static bool operator ==(Iterator iterator1, Iterator iterator2)
            {
                return iterator1.Vector == iterator2.Vector && iterator1.CurIndex == iterator2.CurIndex;
            }

            public static bool operator !=(Iterator iterator1, Iterator iterator2)
            {
                return !(iterator1 == iterator2);
            }
        }

        private T[] _datas;
        private int _size;
        private int _maxSize;

        public Vector()
        {
            _size = 0;
            _maxSize = 8;
            _datas = new T[MaxSize];
        }

        public int Size
        {
            get { return _size; }
        }

        public int MaxSize
        {
            get { return _maxSize; }
        }

        public void Resize(int size)
        {
            if (size > Size && size < MaxSize)
                return;

            _maxSize = size > MaxSize ? size : MaxSize;
            _size = size < Size ? size : Size;

            T[] tdata = new T[MaxSize];
            for (int i = 0; i < Size; ++i)
            {
                tdata[i] = _datas[i];
            }
            _datas = tdata;
        }

        public void PushBack(T t)
        {
            Insert(End(), t);
        }

        public void Clear()
        {
            _size = 0;
        }

        public T At(int index)
        {
            return _datas[index];
        }

        public T Back()
        {
            return _datas[Size - 1];
        }

        public Iterator Begin()
        {
            Iterator it = new Iterator(this) {CurIndex = 0};
            return it;
        }

        public Iterator End()
        {
            Iterator it = new Iterator(this) {CurIndex = Size};
            return it;
        }

        public Iterator Erase(Iterator it)
        {
            int index = it.CurIndex;
            for (int i = index; i < Size - 1; ++i)
            {
                _datas[i] = _datas[i + 1];
            }
            _size--;
            Iterator newiIterator = new Iterator(this) {CurIndex = index > _size - 1 ? _size - 1 : index};
            return newiIterator;
        }

        public Iterator Insert(Iterator it, T data)
        {
            if (Size + 1 == MaxSize)
            {
                Resize(MaxSize * 2);
            }

            int index = it.CurIndex;

            if (index > Size)
            {
                _datas[_size++] = data;
                Iterator newIterator1 = new Iterator(this);
                newIterator1.CurIndex = Size - 1;
                return newIterator1;
            }

            for (int i = Size; i > index; --i)
            {
                _datas[i] = _datas[i - 1];
            }

            _datas[index] = data;
            _size += 1;
            Iterator newIterator2 = new Iterator(this);
            newIterator2.CurIndex = index;
            return newIterator2;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Vector<int> vector = new Vector<int>();
            for (int i = 0; i < 10; ++i)
            {
                vector.PushBack(i);
            }

            for (Vector<int>.Iterator it = vector.Begin(); it != vector.End(); it++)
            {
                Console.WriteLine(it.GetIndexValue());
            }

            vector.Insert(vector.Begin() + 5, 100);

            for (Vector<int>.Iterator it = vector.Begin(); it != vector.End(); it++)
            {
                if (it.GetIndexValue() == 2)
                    it = vector.Erase(it);
                Console.WriteLine(it.GetIndexValue());
            }

            vector.Clear();

            for (Vector<int>.Iterator it = vector.Begin(); it != vector.End(); it++)
            {
                Console.WriteLine(it.GetIndexValue());
            }
        }
    }
}
