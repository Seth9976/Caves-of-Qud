using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Hierarchy
{
	// Token: 0x0200000A RID: 10
	[StructLayout(LayoutKind.Explicit, Size = 32)]
	internal struct HierarchyNodeChildrenFixed
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000255F File Offset: 0x0000075F
		public unsafe HierarchyNode* Ptr
		{
			get
			{
				return (HierarchyNode*)UnsafeUtility.AddressOf<HierarchyNode>(ref this.m_Node1);
			}
		}

		// Token: 0x04000018 RID: 24
		public const int Capacity = 4;

		// Token: 0x04000019 RID: 25
		[FieldOffset(0)]
		private HierarchyNode m_Node1;

		// Token: 0x0400001A RID: 26
		[FieldOffset(8)]
		private HierarchyNode m_Node2;

		// Token: 0x0400001B RID: 27
		[FieldOffset(16)]
		private HierarchyNode m_Node3;

		// Token: 0x0400001C RID: 28
		[FieldOffset(24)]
		private HierarchyNode m_Node4;
	}
}
