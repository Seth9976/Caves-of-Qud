using System;
using System.Runtime.CompilerServices;

namespace Unity.Hierarchy
{
	// Token: 0x02000015 RID: 21
	public readonly struct HierarchyViewNodesEnumerable
	{
		// Token: 0x0600007F RID: 127 RVA: 0x0000343B File Offset: 0x0000163B
		internal HierarchyViewNodesEnumerable(HierarchyViewModel viewModel, HierarchyNodeFlags flags, HierarchyViewNodesEnumerable.Predicate predicate)
		{
			if (viewModel == null)
			{
				throw new ArgumentNullException("viewModel");
			}
			this.m_HierarchyViewModel = viewModel;
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			this.m_Predicate = predicate;
			this.m_Flags = flags;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003471 File Offset: 0x00001671
		public HierarchyViewNodesEnumerable.Enumerator GetEnumerator()
		{
			return new HierarchyViewNodesEnumerable.Enumerator(this);
		}

		// Token: 0x0400002E RID: 46
		private readonly HierarchyViewModel m_HierarchyViewModel;

		// Token: 0x0400002F RID: 47
		private readonly HierarchyViewNodesEnumerable.Predicate m_Predicate;

		// Token: 0x04000030 RID: 48
		private readonly HierarchyNodeFlags m_Flags;

		// Token: 0x02000016 RID: 22
		// (Invoke) Token: 0x06000082 RID: 130
		internal delegate bool Predicate(in HierarchyNode node, HierarchyNodeFlags flags);

		// Token: 0x02000017 RID: 23
		public struct Enumerator
		{
			// Token: 0x06000085 RID: 133 RVA: 0x00003480 File Offset: 0x00001680
			internal Enumerator(HierarchyViewNodesEnumerable enumerable)
			{
				this.m_HierarchyFlattened = enumerable.m_HierarchyViewModel.HierarchyFlattened;
				this.m_Predicate = enumerable.m_Predicate;
				this.m_Flags = enumerable.m_Flags;
				this.m_NodesPtr = this.m_HierarchyFlattened.NodesPtr;
				this.m_NodesCount = this.m_HierarchyFlattened.Count;
				this.m_Version = this.m_HierarchyFlattened.Version;
				this.m_Index = 0;
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000086 RID: 134 RVA: 0x000034F4 File Offset: 0x000016F4
			public unsafe readonly ref HierarchyNode Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					this.ThrowIfVersionChanged();
					return HierarchyFlattenedNode.GetNodeByRef(in this.m_NodesPtr[this.m_Index]);
				}
			}

			// Token: 0x06000087 RID: 135 RVA: 0x00003528 File Offset: 0x00001728
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public unsafe bool MoveNext()
			{
				this.ThrowIfVersionChanged();
				for (;;)
				{
					int num = this.m_Index + 1;
					this.m_Index = num;
					bool flag = num >= this.m_NodesCount;
					if (flag)
					{
						break;
					}
					bool flag2 = this.m_Predicate(HierarchyFlattenedNode.GetNodeByRef(in this.m_NodesPtr[this.m_Index]), this.m_Flags);
					if (flag2)
					{
						goto Block_2;
					}
				}
				return false;
				Block_2:
				return true;
			}

			// Token: 0x06000088 RID: 136 RVA: 0x0000359C File Offset: 0x0000179C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void ThrowIfVersionChanged()
			{
				bool flag = this.m_Version != this.m_HierarchyFlattened.Version;
				if (flag)
				{
					throw new InvalidOperationException("HierarchyFlattened was modified.");
				}
			}

			// Token: 0x04000031 RID: 49
			private readonly HierarchyFlattened m_HierarchyFlattened;

			// Token: 0x04000032 RID: 50
			private readonly HierarchyViewNodesEnumerable.Predicate m_Predicate;

			// Token: 0x04000033 RID: 51
			private readonly HierarchyNodeFlags m_Flags;

			// Token: 0x04000034 RID: 52
			private unsafe readonly HierarchyFlattenedNode* m_NodesPtr;

			// Token: 0x04000035 RID: 53
			private readonly int m_NodesCount;

			// Token: 0x04000036 RID: 54
			private readonly int m_Version;

			// Token: 0x04000037 RID: 55
			private int m_Index;
		}
	}
}
