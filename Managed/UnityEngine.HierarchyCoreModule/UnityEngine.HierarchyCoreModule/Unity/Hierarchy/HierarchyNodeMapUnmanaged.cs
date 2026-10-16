using System;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace Unity.Hierarchy
{
	// Token: 0x0200000B RID: 11
	public struct HierarchyNodeMapUnmanaged<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : IDisposable where T : struct, ValueType
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000256C File Offset: 0x0000076C
		public bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Values.IsCreated;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002579 File Offset: 0x00000779
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002586 File Offset: 0x00000786
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Values.Capacity;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Values.Capacity = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002595 File Offset: 0x00000795
		public int Count
		{
			get
			{
				return this.m_Values.Count;
			}
		}

		// Token: 0x1700000B RID: 11
		public T this[in HierarchyNode node]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Values[ref node];
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Values[ref node] = value;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025C0 File Offset: 0x000007C0
		public HierarchyNodeMapUnmanaged(Allocator allocator)
		{
			this.m_Values = new NativeSparseArray<HierarchyNode, T>(new NativeSparseArray<HierarchyNode, T>.KeyIndex(HierarchyNodeMapUnmanaged<T>.KeyIndex), new NativeSparseArray<HierarchyNode, T>.KeyEqual(HierarchyNodeMapUnmanaged<T>.KeyEqual), allocator);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025E7 File Offset: 0x000007E7
		public HierarchyNodeMapUnmanaged(in T initValue, Allocator allocator)
		{
			this.m_Values = new NativeSparseArray<HierarchyNode, T>(in initValue, new NativeSparseArray<HierarchyNode, T>.KeyIndex(HierarchyNodeMapUnmanaged<T>.KeyIndex), new NativeSparseArray<HierarchyNode, T>.KeyEqual(HierarchyNodeMapUnmanaged<T>.KeyEqual), allocator);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000260F File Offset: 0x0000080F
		public void Dispose()
		{
			this.m_Values.Dispose();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000261E File Offset: 0x0000081E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Reserve(int capacity)
		{
			this.m_Values.Reserve(capacity);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002630 File Offset: 0x00000830
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool ContainsKey(in HierarchyNode node)
		{
			return this.m_Values.ContainsKey(in node);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000264E File Offset: 0x0000084E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(in HierarchyNode node, in T value)
		{
			this.m_Values.Add(in node, in value, NativeSparseArrayResizePolicy.ExactSize);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002660 File Offset: 0x00000860
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddNoResize(in HierarchyNode node, in T value)
		{
			this.m_Values.AddNoResize(in node, in value);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002674 File Offset: 0x00000874
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryAdd(in HierarchyNode node, in T value)
		{
			return this.m_Values.TryAdd(in node, in value, NativeSparseArrayResizePolicy.ExactSize);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002694 File Offset: 0x00000894
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryAddNoResize(in HierarchyNode node, in T value)
		{
			return this.m_Values.TryAddNoResize(in node, in value);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026B4 File Offset: 0x000008B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryGetValue(in HierarchyNode node, out T value)
		{
			return this.m_Values.TryGetValue(in node, out value);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026D4 File Offset: 0x000008D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Remove(in HierarchyNode node)
		{
			return this.m_Values.Remove(in node);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026F2 File Offset: 0x000008F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			this.m_Values.Clear();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002704 File Offset: 0x00000904
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int KeyIndex(in HierarchyNode node)
		{
			return node.Id - 1;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002720 File Offset: 0x00000920
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool KeyEqual(in HierarchyNode lhs, in HierarchyNode rhs)
		{
			return lhs.Version == rhs.Version;
		}

		// Token: 0x0400001D RID: 29
		private NativeSparseArray<HierarchyNode, T> m_Values;
	}
}
