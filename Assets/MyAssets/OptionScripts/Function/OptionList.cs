#nullable enable

using UnityEngine;

namespace MobileLibrary.Function
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class OptionList<T> : ICollection<Option<T>>, IEnumerable<Option<T>>, IList<Option<T>>, IReadOnlyCollection<Option<T>>, IReadOnlyList<Option<T>>, IList
    {
        private readonly List<Option<T>> _list = new();

        public Option<T> this[int index]
        {
            get
            {
                if (index < 0 || index >= _list.Count)
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"<color=red>Index out of range: {index}</color>");
#endif
                    return new Option.None();
                }

                return _list[index];
            }
            set
            {
                if (index < 0 || index >= _list.Count)
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"<color=red>Index out of range: {index}</color>");
#endif
                    return;
                }

                _list[index] = value;
            }
        }

        Option<T> IList<Option<T>>.this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }

        public int Count => _list.Count;
        public bool IsReadOnly => false;

        public void Add(T item)
        {
            Add(Function.Some(item));
        }

        public void Add(Option<T> item)
        {
            _list.Add(item);
        }


        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(Option<T> item)
        {
            return item.Match(
                None: () => false,
                Some: element => _list.Contains(Function.Some(element))
            );
        }

        public bool Contains(T item)
        {
            return _list.Exists(opt =>
                opt.Match(
                    None: () => false,
                    Some: element => EqualityComparer<T>.Default.Equals(element, item))
            );
        }

        public void CopyTo(Option<T>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0 || arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            if (array.Length - arrayIndex < _list.Count)
                throw new ArgumentException("The destination array has insufficient space.");

            // Copy each Option<T> to the destination array
            for (int i = 0; i < _list.Count; i++)
            {
                array[arrayIndex + i] = _list[i]; // Copy Option<T> directly
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0 || arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            if (array.Length - arrayIndex < _list.Count)
                throw new ArgumentException("The destination array has insufficient space.");

            // Extract and copy T from Option<T> to the destination array
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].Do(
                    element => array[arrayIndex + i] = element // Copy the value of Some<T>
                );
            }
        }

        public IEnumerator<Option<T>> GetEnumerator()
        {
            foreach (var option in _list)
            {
                yield return option;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(Option<T> item)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;  // Return -1 if the item is not found
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                var index = _list[i].Match(
                    None: () => -1,  // Return -1 if it's None
                    Some: element => EqualityComparer<T>.Default.Equals(element, item) ? i : -1  // Return index if match is found
                );

                if (index != -1)
                {
                    return i;  // Return the index if a match is found
                }
            }

            return -1;  // Return -1 if the item is not found
        }

        public void Insert(int index, Option<T> item)
        {
            _list.Insert(index, item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, Function.Some(item)); // Fixed this line
        }

        public bool Remove(Option<T> item)
        {
            var index = IndexOf(item);
            if (index >= 0)
            {
                _list.RemoveAt(index);
                return true;
            }

            return false;
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index >= 0)
            {
                _list.RemoveAt(index);
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        // IList interface implementation (non-generic)
        object IList.this[int index]
        {
            get => _list[index];
            set
            {
                if (value is T t)
                    _list[index] = Function.Some(t); // Fixed this line
                else
                    throw new ArgumentException("Invalid value type");
            }
        }

        public bool IsFixedSize => false;
        public bool IsSynchronized => false;
        public object SyncRoot => this;

        public int Add(object value)
        {
            if (value is T t)
            {
                _list.Add(Function.Some(t)); // Fixed this line
                return _list.Count - 1;
            }

            throw new ArgumentException("Invalid value type");
        }

        public bool Contains(object value)
        {
            return value is T t && Contains(t);
        }

        public int IndexOf(object value)
        {
            return value is T t ? IndexOf(t) : -1;
        }

        public void Insert(int index, object value)
        {
            if (value is T t)
            {
                _list.Insert(index, Function.Some(t)); // Fixed this line
            }
            else
            {
                throw new ArgumentException("Invalid value type");
            }
        }

        public void Remove(object value)
        {
            if (value is T t)
            {
                Remove(t);
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (array is T[] tArray)
            {
                CopyTo(tArray, index);
            }
            else
            {
                throw new ArgumentException("Invalid array type");
            }
        }
    }
}