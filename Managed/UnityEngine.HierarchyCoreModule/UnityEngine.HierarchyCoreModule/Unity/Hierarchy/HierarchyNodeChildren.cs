using System;
using System.Runtime.CompilerServices;

namespace Unity.Hierarchy
{
	// Token: 0x02000006 RID: 6
	public readonly struct HierarchyNodeChildren
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000234C File Offset: 0x0000054C
		internal unsafe HierarchyNodeChildren(Hierarchy hierarchy, IntPtr nodeChildrenPtr)
		{
			bool flag = hierarchy == null;
			if (flag)
			{
				throw new ArgumentNullException("hierarchy");
			}
			bool flag2 = nodeChildrenPtr == IntPtr.Zero;
			if (flag2)
			{
				throw new ArgumentNullException("nodeChildrenPtr");
			}
			this.m_Hierarchy = hierarchy;
			this.m_Version = hierarchy.Version;
			ref HierarchyNodeChildrenAlloc ptr = ref *(HierarchyNodeChildrenAlloc*)(void*)nodeChildrenPtr;
			bool flag3 = (ptr.Reserved.FixedElementField & int.MinValue) == int.MinValue;
			if (flag3)
			{
				this.m_Ptr = ptr.Ptr;
				this.m_Count = ptr.Size;
			}
			else
			{
				this.m_Ptr = (HierarchyNode*)(void*)nodeChildrenPtr;
				this.m_Count = 0;
				for (int i = 0; i < 4; i++)
				{
					bool flag4 = (in this.m_Ptr[i]) != HierarchyNode.Null;
					if (!flag4)
					{
						break;
					}
					this.m_Count++;
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000243C File Offset: 0x0000063C
		public int Count
		{
			get
			{
				this.ThrowIfVersionChanged();
				return this.m_Count;
			}
		}

		// Token: 0x17000005 RID: 5
		public ref HierarchyNode this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				bool flag = index < 0 || index >= this.m_Count;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.ThrowIfVersionChanged();
				return this.m_Ptr + index;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000024A7 File Offset: 0x000006A7
		public HierarchyNodeChildren.Enumerator GetEnumerator()
		{
			return new HierarchyNodeChildren.Enumerator(in this);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000024B0 File Offset: 0x000006B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ThrowIfVersionChanged()
		{
			bool flag = this.m_Version != this.m_Hierarchy.Version;
			if (flag)
			{
				throw new InvalidOperationException("Hierarchy was modified.");
			}
		}

		// Token: 0x0400000B RID: 11
		private const int k_HierarchyNodeChildrenIsAllocBit = -2147483648;

		// Token: 0x0400000C RID: 12
		private readonly Hierarchy m_Hierarchy;

		// Token: 0x0400000D RID: 13
		private unsafe readonly HierarchyNode* m_Ptr;

		// Token: 0x0400000E RID: 14
		private readonly int m_Version;

		// Token: 0x0400000F RID: 15
		private readonly int m_Count;

		// Token: 0x02000007 RID: 7
		public struct Enumerator
		{
			// Token: 0x06000010 RID: 16 RVA: 0x000024E3 File Offset: 0x000006E3
			internal Enumerator(in HierarchyNodeChildren enumerable)
			{
				this.m_Enumerable = enumerable;
				this.m_Index = -1;
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000011 RID: 17 RVA: 0x000024FC File Offset: 0x000006FC
			public readonly ref HierarchyNode Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					this.m_Enumerable.ThrowIfVersionChanged();
					return this.m_Enumerable.m_Ptr + this.m_Index;
				}
			}

			// Token: 0x06000012 RID: 18 RVA: 0x00002534 File Offset: 0x00000734
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				int num = this.m_Index + 1;
				this.m_Index = num;
				return num < this.m_Enumerable.m_Count;
			}

			// Token: 0x04000010 RID: 16
			private readonly HierarchyNodeChildren m_Enumerable;

			// Token: 0x04000011 RID: 17
			private int m_Index;
		}
	}
}
