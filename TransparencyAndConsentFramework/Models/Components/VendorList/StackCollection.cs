using System;
using System.Collections;
using System.Collections.Generic;

namespace Bidtellect.Tcf.Models.Components.VendorList
{
    /// <summary>
    /// Represents a collection of Stacks.
    /// </summary>
    public class StackCollection : IEnumerable<KeyValuePair<int, Stack>>
    {
        protected Dictionary<int, Stack> stacks;

        /// <summary>
        /// Gets the number of elements contained in this collection.
        /// </summary>
        public int Count => stacks.Count;

        /// <summary>
        /// Initializes a new instance of <c>StackCollection</c>.
        /// </summary>
        public StackCollection()
        {
            stacks = new Dictionary<int, Stack>();
        }

        /// <inheritdoc cref="StackCollection.StackCollection()"/>
        /// <param name="capacity">The initial capacity of the collection.</param>
        public StackCollection(int capacity)
        {
            stacks = new Dictionary<int, Stack>(capacity);
        }

        /// <summary>
        /// Adds a Stack to this collection.
        /// </summary>
        /// <param name="stack">The Stack to be added.</param>
        /// <exception cref="System.ArgumentNullException"/>
        public void Add(Stack stack)
        {
            if (stack == null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            stacks[stack.Id] = stack;
        }

        /// <summary>
        /// Removes a Stack with the given ID from this collection.
        /// </summary>
        /// <param name="stackId">The ID of the stack.</param>
        /// <returns>
        /// A value indicating whether the Stack was successfully found and removed.
        /// </returns>
        public bool Remove(int stackId)
        {
            return stacks.Remove(stackId);
        }

        /// <summary>
        /// Removes a Stack from this collection.
        /// </summary>
        /// <param name="stack">The Stack to be removed.</param>
        /// <returns>
        /// A value indicating whether the Stack was successfully found and removed.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"/>
        public bool Remove(Stack stack)
        {
            if (stack == null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            return Remove(stack.Id);
        }

        /// <summary>
        /// Determines whether the collection contains a Stack with the given ID.
        /// </summary>
        /// <param name="stackId">The ID of the Stack.</param>
        /// <returns>
        /// <c>true</c> if this collection contains a Stack with the given ID;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(int stackId)
        {
            return stacks.ContainsKey(stackId);
        }

        /// <summary>
        /// Gets a Stack with the given ID.
        /// </summary>
        /// <param name="stackId">The ID of the Stack.</param>
        /// <param name="stack">
        /// When this method returns, contains the Stack,
        /// if the ID is found; otherwise, <c>null</c>.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c> if this collection contains a Feature with the given ID;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool TryGet(int stackId, out Stack stack)
        {
            return stacks.TryGetValue(stackId, out stack);
        }

        public IEnumerator<KeyValuePair<int, Stack>> GetEnumerator()
        {
            return stacks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
