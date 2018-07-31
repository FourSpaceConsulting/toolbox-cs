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
using System.Collections.Generic;

namespace Fourspace.Toolbox.Util.Compare
{
    /// <summary>
    /// Creates a key match group result using delegates to determine keys
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <typeparam name="I"></typeparam>
    /// <typeparam name="K"></typeparam>
    public class MatchKeyGrouper<L, R, K, I> : IKeyGrouper<L, R, K, I>
    {
        private readonly IKeyGrouper<L, K, I> leftGrouper;
        private readonly IKeyGrouper<R, K, I> rightGrouper;

        public MatchKeyGrouper(IKeyGrouper<L, K, I> leftGrouper, IKeyGrouper<R, K, I> rightGrouper)
        {
            this.leftGrouper = leftGrouper;
            this.rightGrouper = rightGrouper;
        }

        public GroupedItems<L, R, K, I> GroupItems(IEnumerable<L> left, IEnumerable<R> right)
        {
            var leftGroupItems = leftGrouper.GroupItems(left);
            var rightGroupItems = rightGrouper.GroupItems(right);
            // determine key matches
            var nonMatchRight = new Dictionary<K, IList<Pair<I, R>>>();
            var nonMatchLeft = new Dictionary<K, IList<Pair<I, L>>>();
            var match = new Dictionary<K, Pair<IList<Pair<I, L>>, IList<Pair<I, R>>>>();
            //
            var rightKeyedItems = rightGroupItems.GroupItems;
            ISet<K> remainingRightKeys = new HashSet<K>(rightKeyedItems.Keys);
            foreach (var pair in leftGroupItems.GroupItems)
            {
                var key = pair.Key;
                var rightIndexedItems = CollectionUtil.TryGetValue(rightKeyedItems, key);
                if (rightIndexedItems == null)
                {
                    // no 'right' key found for 'left' key
                    nonMatchLeft[key] = pair.Value;
                }
                else
                {
                    // 'right' key found matching 'left' key
                    match[key] = new Pair<IList<Pair<I, L>>, IList<Pair<I, R>>>(pair.Value, rightIndexedItems);
                    remainingRightKeys.Remove(key);
                }
            }
            // add remaining from keys
            foreach (var key in remainingRightKeys)
            {
                nonMatchRight[key] = rightKeyedItems[key];
            }
            // collate and return results
            return new GroupedItems<L, R, K, I>()
            {
                NullRight = rightGroupItems.NullItems,
                NullLeft = leftGroupItems.NullItems,
                NullKeyRight = rightGroupItems.NullKeyItems,
                NullKeyLeft = leftGroupItems.NullKeyItems,
                NonMatchRight = nonMatchRight,
                NonMatchLeft = nonMatchLeft,
                GroupItems = match
            };
        }

    }
}
