/*
MIT License

Copyright (c) 2017 Richard Steward

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fourspace.Toolbox.Util
{
    public static class CollectionUtil
    {
        /// <summary>
        /// Determines whether any values are present in enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty<T>(IEnumerable<T> col)
        {
            return !(col == null || !col.Any());
        }

        /// <summary>
        /// Determines whether any values are present in collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty<T>(ICollection<T> col)
        {
            return !(col == null || col.Count == 0);
        }


        /// <summary>
        /// Return enumerable as a hash set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col"></param>
        /// <returns></returns>
        public static ISet<T> AsSet<T>(IEnumerable<T> col)
        {
            ISet<T> set = col as ISet<T>;
            return set == null ? new HashSet<T>(col) : set;
        }


        /// <summary>
        /// Pops an item off the end of a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static T Pop<T>(IList<T> collection)
        {
            if (!IsNotNullOrEmpty(collection)) return default(T);
            int i = collection.Count - 1;
            T item = collection[i];
            collection.RemoveAt(i);
            return item;
        }


        /// <summary>
        /// Get next or default for enumerator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iter"></param>
        /// <returns></returns>
        public static T GetNextOrDefault<T>(IEnumerator<T> iter)
        {
            return iter.MoveNext() ? iter.Current : default(T);
        }

        /// <summary>
        /// Look up key in dicionary, checking nulls and not throwing exception.
        /// Returns default value if dict or key is null or key not found in dictionary. 
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="map"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T TryGetValue<K, T>(IDictionary<K, T> map, K key)
        {
            if (map != null && key != null)
            {
                T output;
                if (map.TryGetValue(key, out output))
                {
                    return output;
                }
            }
            return default(T);
        }

        /// <summary>
        /// Look up key in dicionary, checking nulls and not throwing exception.
        /// Returns default value if dict or key is null or key not found in dictionary. 
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="map"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T TryGetValue<K, T>(IReadOnlyDictionary<K, T> map, K key)
        {
            if (map != null && key != null)
            {
                T output;
                if (map.TryGetValue(key, out output))
                {
                    return output;
                }
            }
            return default(T);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="map"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddToMappedList<K, V>(IDictionary<K, IList<V>> map, K key, V value)
        {
            GetOrCreateCollection(map, key, () => new List<V>()).Add(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="map"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddToMappedList<K, V>(IDictionary<K, IList<V>> map, K key, IEnumerable<V> values)
        {
            AddToMappedCollection(map, key, values, () => new List<V>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="map"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddToMappedList<K, V>(IDictionary<K, List<V>> map, K key, IEnumerable<V> values)
        {
            GetOrCreateCollection(map, key, () => new List<V>()).AddRange(values);
        }

        /// <summary>
        /// Add a value to a collection in a keyed dictionary
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="map"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="create"></param>
        public static void AddToMappedCollection<K, C, V>(IDictionary<K, C> map, K key, V value, Func<C> create)
            where C : ICollection<V>
        {
            GetOrCreateCollection(map, key, create).Add(value);
        }

        /// <summary>
        /// Add a value to a collection in a keyed dictionary
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="map"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="create"></param>
        public static void AddToMappedCollection<K, C, V>(IDictionary<K, C> map, K key, IEnumerable<V> values, Func<C> create)
            where C : ICollection<V>
        {
            var collection = GetOrCreateCollection(map, key, create);
            foreach (var value in values)
            {
                collection.Add(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="map"></param>
        /// <param name="key"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        private static C GetOrCreateCollection<K, C>(IDictionary<K, C> map, K key, Func<C> create)
        {
            var collection = TryGetValue(map, key);
            if (collection == null)
            {
                collection = create();
                map[key] = collection;
            }
            return collection;
        }

        /// <summary>
        /// Add a value to an object in a keyed dictionary
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="map"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="create"></param>
        /// <param name="add"></param>
        public static void AddToMappedObject<K, C, V>(IDictionary<K, C> map, K key, V value, Func<C> create, Action<C, V> add)
        {
            var obj = TryGetValue(map, key);
            if (obj == null)
            {
                obj = create();
                map[key] = obj;
            }
            add(obj, value);
        }

        /// <summary>
        /// Add range from a collection to a dictionary.
        /// Uses Add so will throw exception if key already exists.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="target"></param>
        /// <param name="collection"></param>
        public static void AddUniqueRange<K, V>(IDictionary<K, V> target, IEnumerable<KeyValuePair<K, V>> collection)
        {
            AddUniqueRange(target, collection, i => i.Key, i => i.Value);
        }

        /// <summary>
        /// Add range from a collection to a dictionary.
        /// Uses Add so will throw exception if key already exists.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="target"></param>
        /// <param name="collection"></param>
        public static void AddUniqueRange<K, V, S>(IDictionary<K, V> target, IEnumerable<S> collection, Func<S, K> keyGetter, Func<S, V> valueGetter)
        {
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    target.Add(keyGetter(item), valueGetter(item));
                }
            }
        }

        /// <summary>
        /// Add range from a collection to a dictionary.
        /// Will overwrite if key already exists.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="target"></param>
        /// <param name="collection"></param>
        public static void AddRange<K, V, S>(IDictionary<K, V> target, IEnumerable<S> collection, Func<S, K> keyGetter, Func<S, V> valueGetter)
        {
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    target[keyGetter(item)] = valueGetter(item);
                }
            }
        }

        /// <summary>
        /// Add range from a collection to a dictionary.
        /// Will overwrite if key already exists.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="target"></param>
        /// <param name="collection"></param>
        public static void AddRange<K, V>(IDictionary<K, V> target, IEnumerable<V> collection, Func<V, K> keyGetter)
        {
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    target[keyGetter(item)] = item;
                }
            }
        }

        #region chained versions
        /// <summary>
        /// Add range from a collection to a dictionary.
        /// Uses Add so will throw exception if key already exists.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="target"></param>
        /// <param name="collection"></param>
        public static T AddUniqueRangeChain<T, K, V>(T target, IEnumerable<KeyValuePair<K, V>> collection)
            where T : IDictionary<K, V>
        {
            AddUniqueRange(target, collection, i => i.Key, i => i.Value);
            return target;
        }

        /// <summary>
        /// Add range from a collection to a dictionary.
        /// Uses Add so will throw exception if key already exists.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="target"></param>
        /// <param name="collection"></param>
        public static T AddUniqueRangeChain<T, K, V, S>(T target, IEnumerable<S> collection, Func<S, K> keyGetter, Func<S, V> valueGetter)
            where T : IDictionary<K, V>
        {
            AddUniqueRange(target, collection, keyGetter, valueGetter);
            return target;
        }
        /// <summary>
        /// Add range from a collection to a dictionary.
        /// Will overwrite if key already exists.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="target"></param>
        /// <param name="collection"></param>
        public static T AddRangeChain<T, K, V, S>(T target, IEnumerable<S> collection, Func<S, K> keyGetter, Func<S, V> valueGetter)
            where T : IDictionary<K, V>
        {
            AddRange(target, collection, keyGetter, valueGetter);
            return target;
        }

        /// <summary>
        /// Add range from a collection to a dictionary.
        /// Will overwrite if key already exists.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="target"></param>
        /// <param name="collection"></param>
        public static T AddRangeChain<T, K, V>(T target, IEnumerable<V> collection, Func<V, K> keyGetter)
            where T : IDictionary<K, V>
        {
            AddRange(target, collection, keyGetter);
            return target;
        }
        #endregion

        /// <summary>
        /// Appends next 'count' items from iter to append, returning the number of items added
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="append"></param>
        /// <param name="iter"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int AddNextItems<T>(ICollection<T> append, IEnumerator<T> iter, int count)
        {
            int i = 0;
            while (count > i && iter.MoveNext())
            {
                append.Add(iter.Current);
                ++i;
            }
            return i;
        }

        /// <summary>
        /// Batch items in a collection to a list of collections based on batch size.
        /// Uses a tolerance in the case where the collection size does not divide into the batch size exactly.
        /// This determines if the first collection will be larger to reduce the number of batches.
        /// E.g. if batching 17 items with a batch size of 4:
        /// a tolerance of 0 will result in 5 batches [0..3],[4..7],[8..11],[12..15],[16]
        /// a tolerance of 1 or more will result in 4 batches [0..4],[5..8],[9..12],[13..16]
        /// If the collection size divides into the batch size exactly the tolerance is irrelevant 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="col">collection of objects to batch</param>
        /// <param name="create">creator for the new collections</param>
        /// <param name="batchSize"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static IList<U> BatchCollection<T, U>(ICollection<T> col, Func<U> create, int batchSize, int tolerance = 0)
            where U : ICollection<T>
        {
            IList<U> batched = new List<U>();
            var count = 0;
            // determine first batch size
            var length = col.Count;
            var remainder = length % batchSize;
            var currentBatchSize = remainder > tolerance ? batchSize : batchSize + remainder;
            // batch
            var iter = col.GetEnumerator();
            while (count < length)
            {
                var nextCollection = create();
                count += AddNextItems(nextCollection, iter, currentBatchSize);
                batched.Add(nextCollection);
                currentBatchSize = batchSize;
            }
            return batched;
        }
    }
}
