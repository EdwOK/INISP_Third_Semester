using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Paprotski.Lab4
{
    /// <summary>
    /// The associative array.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    [Serializable]
    public class ArrayList<T> : IEnumerable<T>, IEquatable<T>
    { 
        #region Private Member Variables

        /// <summary>
        /// The _items of generic Type 
        /// </summary>
        private T[] _items;

        /// <summary>
        /// The _size of associative array 
        /// </summary>
        private int _size;

        [NonSerialized]
        private bool disposed = false;

        #endregion 
        
        #region Private Class

        [Serializable]
        private class ListEnumerator : IEnumerator<T>
        {
            private readonly ArrayList<T> arrayList;
            private int index;

            public ListEnumerator(ArrayList<T> data)
            {
                this.arrayList = data;
                this.Current = default(T);
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                var list = this.arrayList;

                if (this.index >= arrayList._size)
                {
                    return this.MoveNextRare();
                }

                this.Current = list._items[this.index];
                ++this.index;
                return true;
            }

            private bool MoveNextRare()
            {
                this.index = this.arrayList._size + 1;
                this.Current = default(T);
                return false;
            }

            public void Reset()
            {
                this.index = 0;
                this.Current = default(T);
            }

            public T Current { get; private set; }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }
        }
        #endregion
 
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayList{T}"/> class.
        /// </summary>
        public ArrayList()
        {
            this._size = 0; 
            this._items = new T[0];
        }

        ~ArrayList()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayList{T}"/> class.
        /// </summary>
        /// <param name="capacity">
        /// The capacity of the initial size of the array
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public ArrayList(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("Capasity < 0");
            }

            this._size = capacity; 
            this._items = capacity == 0 ? new T[0] : new T[capacity];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayList{T}"/> class.
        /// </summary>
        /// <param name="items">
        /// The items that is transmitted. 
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public ArrayList(T[] items)
        {
            if (ReferenceEquals(items, null))
            {
                throw new NullReferenceException("Object reference not set to an instance of the object");
            }

            this._size = items.Length;
            this._items = new T[items.Length];
            Array.Copy(items, this._items, items.Length);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds an object to the end of <see cref="ArrayList{T}"/>.
        /// </summary>
        /// <param name="item">object that is added to the end of <see cref="ArrayList{T}"/>. Reference data types supported by the value of null. </param>
        public void Add(T item)
        {
            if (ReferenceEquals(item, null))
            {
                throw new NullReferenceException("Object reference not set to an instance of the object");
            }

            if (this.Count <= this._size * 2)
            {
                var number = this.Count == 0 ? 4 : this.Count * 2;
                Array.Resize(ref this._items, number);
            }

            this._items[this._size++] = item;
        }
        
        /// <summary> 
        /// Removes the first occurrence of a specific object from the <see cref = "ArrayList{T}" />. 
        /// </summary> 
        /// <returns> 
        /// Value true, if the element <paramref name = "item" /> successfully deleted, otherwise - to false. This method also returns false, if the element <paramref name = "item" /> not found in the collection <see cref = "ArrayList{T}" />. 
        /// </returns> 
        /// <param name = "item"> 
        /// Object to remove from the <see cref = "ArrayList{T}" />. Reference data types support to null. 
        /// </param>
        public void Remove(T item)
        {
            if (ReferenceEquals(item, null))
            {
                throw new NullReferenceException("Object reference not set to an instance of the object");
            }

            var index = this.IndexOf(item);

            if (index < 0)
            {
                return;
            }

            this.RemoveAt(index);
        }

        /// <summary>
        /// Removes all items from the collection <see cref = "ArrayList{T}" />.
        /// </summary>
        public void Clear()
        {
            if (this._size > 0)
            {
                Array.Clear(this._items, 0, this._size);
                this._size = 0; 
            }
        }

        /// <summary> 
        /// Determines whether an element is in the <see cref = "ArrayList{T}" />. 
        /// </summary> 
        /// <returns> 
        /// Value true, if the element <paramref name = "item" /> found in the list <see cref = "ArrayList{T}" />, otherwise - to false. 
        /// </returns> 
        /// <param name = "item"> object list in which the <see cref = "ArrayList{T}" />. Reference data types supported by the value of null. </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool Contains(T item)
        {
            if (ReferenceEquals(item, null))
            {
                throw new NullReferenceException("Object reference not set to an instance of the object");
            }

            return Array.IndexOf(this._items, item) != -1;
        }

        /// <summary> 
        /// Searches for the specified object and returns the zero-based index of the first occurrence within the entire List <see cref = "ArrayList{T}" />. 
        /// </summary> 
        /// <Returns> 
        /// Zero-based index of the first occurrence of an item <paramref name = "item" /> within the entire collection <see cref = "ArrayList{T}" />, if the item is found; Otherwise - the value -1. 
        /// </returns> 
        /// <param name = "item"> object list in which the <see cref = "ArrayList{T}" />. Reference data types supported by the value of null. </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public int IndexOf(T item)
        {
            if (ReferenceEquals(item, null))
            {
                throw new NullReferenceException("Object reference not set to an instance of the object");
            }

            return Array.IndexOf(this._items, item);
        }

        /// <summary> 
        /// Removes the list <see cref = "ArrayList{T}" /> at the specified index. 
        /// </summary> 
        /// <param name = "index"> Index (zero based) of the element to remove. </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public void RemoveAt(int index)
        {
            if (index >= this._size)
            {
                throw new ArgumentOutOfRangeException("Index the bounds of the array");
            }
            --this._size;

            if (index < this._size)
            {
                Array.Copy(this._items, index + 1, this._items, index, this._size - index);
            }

            this._items[this._size] = default(T);
        }

        /// <summary> 
        /// Copy the entire list <see cref = "ArrayList{T}" /> to a compatible one-dimensional array, starting at the first element of the target array. 
        /// </summary> 
        /// <param name = "array"> dimensional array <see cref = "T: System.Array" />, in which the elements of the list are copied <see cref = "AssociativeArray{T}" />. Array <see cref = "T: System.Array" /> should be indexed starting from scratch. </param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (ReferenceEquals(array, null))
            {
                throw new NullReferenceException("Object reference not set to an instance of the object");
            }

            Array.Copy(this._items, 0, array, arrayIndex, this._size);
        }

        /// <summary> 
        /// Returns an enumerator that iterates through the list <see cref = "ArrayList{T}" />. 
        /// </summary> 
        /// <returns> 
        /// <see cref = "T: AssotiativeArray.Enumerator" /> to <see cref = "ArrayList{T}" />. 
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator(this);
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Determines whether the value of this instance and a specified object <see cref = "T" />.
        /// </summary>
        /// <param name="other">
        /// The other is an instance of an object T.
        /// </param>
        /// <returns>
        /// true <see cref="bool"/> is of type <see cref = "T"/> and the same value as this instance; otherwise - false.
        /// </returns>
        public bool Equals(T other)
        {
            return !ReferenceEquals(other, null) && other.Equals(this);
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get
            {
                return this._size;
            }
        }

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="index">
        /// The index of array.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > this.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return this._items[index];
            }

            set
            {
                if (index < 0 || index > this.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                value = this._items[index];
            }
        }

        /// <summary> 
        /// Performs application-defined tasks associated with the release or resetting unmanaged resources. 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    GC.ReRegisterForFinalize(this._items);
                }

                this.disposed = true;
            }
        }

        #endregion
    }
}
