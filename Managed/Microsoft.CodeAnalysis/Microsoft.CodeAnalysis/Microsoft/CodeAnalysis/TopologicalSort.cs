using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000122 RID: 290
	[NullableContext(1)]
	[Nullable(0)]
	internal static class TopologicalSort
	{
		// Token: 0x06000A2C RID: 2604 RVA: 0x0001D494 File Offset: 0x0001B694
		public static bool TryIterativeSort<TNode>(IEnumerable<TNode> nodes, [Nullable(new byte[] { 1, 1, 0, 1 })] Func<TNode, ImmutableArray<TNode>> successors, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<TNode> result)
		{
			ImmutableArray<TNode> immutableArray;
			PooledDictionary<TNode, int> pooledDictionary = TopologicalSort.PredecessorCounts<TNode>(nodes, successors, out immutableArray);
			ArrayBuilder<TNode> instance = ArrayBuilder<TNode>.GetInstance();
			foreach (TNode tnode in immutableArray)
			{
				if (pooledDictionary[tnode] == 0)
				{
					instance.Push(tnode);
				}
			}
			ArrayBuilder<TNode> instance2 = ArrayBuilder<TNode>.GetInstance();
			while (instance.Count != 0)
			{
				TNode tnode2 = instance.Pop<TNode>();
				instance2.Add(tnode2);
				foreach (TNode tnode3 in successors.Invoke(tnode2))
				{
					int num = pooledDictionary[tnode3];
					pooledDictionary[tnode3] = num - 1;
					if (num == 1)
					{
						instance.Push(tnode3);
					}
				}
			}
			bool flag = pooledDictionary.Count != instance2.Count;
			result = (flag ? ImmutableArray<TNode>.Empty : instance2.ToImmutable());
			pooledDictionary.Free();
			instance.Free();
			instance2.Free();
			return !flag;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0001D58C File Offset: 0x0001B78C
		private static PooledDictionary<TNode, int> PredecessorCounts<TNode>(IEnumerable<TNode> nodes, [Nullable(new byte[] { 1, 1, 0, 1 })] Func<TNode, ImmutableArray<TNode>> successors, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<TNode> allNodes)
		{
			PooledDictionary<TNode, int> instance = PooledDictionary<TNode, int>.GetInstance();
			PooledHashSet<TNode> instance2 = PooledHashSet<TNode>.GetInstance();
			ArrayBuilder<TNode> instance3 = ArrayBuilder<TNode>.GetInstance();
			ArrayBuilder<TNode> instance4 = ArrayBuilder<TNode>.GetInstance();
			instance3.AddRange(nodes);
			while (instance3.Count != 0)
			{
				TNode tnode = instance3.Pop<TNode>();
				if (instance2.Add(tnode))
				{
					instance4.Add(tnode);
					if (!instance.ContainsKey(tnode))
					{
						instance.Add(tnode, 0);
					}
					foreach (TNode tnode2 in successors.Invoke(tnode))
					{
						instance3.Push(tnode2);
						int num;
						if (instance.TryGetValue(tnode2, ref num))
						{
							instance[tnode2] = num + 1;
						}
						else
						{
							instance.Add(tnode2, 1);
						}
					}
				}
			}
			instance2.Free();
			instance3.Free();
			allNodes = instance4.ToImmutableAndFree();
			return instance;
		}
	}
}
