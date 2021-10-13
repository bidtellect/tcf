using System;
using System.Collections;
using System.Collections.Generic;

namespace Bidtellect.Tcf.Models.Components.VendorList
{
    public class StackCollection : IEnumerable<Stack>
    {
        protected Dictionary<int, Stack> stacks = new();

        public int Count => stacks.Count;

        public void Add(Stack stack)
        {
            stacks[stack.Id] = stack;
        }

        public bool Remove(int stackId)
        {
            return stacks.Remove(stackId);
        }

        public bool Remove(int stackId, out Stack stack)
        {
            return stacks.Remove(stackId, out stack);
        }

        public bool Remove(Stack stack)
        {
            if (stack == null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            return Remove(stack.Id);
        }

        public bool Contains(int stackId)
        {
            return stacks.ContainsKey(stackId);
        }

        public bool TryGet(int stackId, out Stack stack)
        {
            return stacks.TryGetValue(stackId, out stack);
        }

        public IEnumerator<Stack> GetEnumerator()
        {
            return stacks.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
