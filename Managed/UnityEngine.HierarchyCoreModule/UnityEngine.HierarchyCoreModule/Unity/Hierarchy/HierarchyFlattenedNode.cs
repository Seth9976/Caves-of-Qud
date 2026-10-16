using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace Unity.Hierarchy
{
	// Token: 0x02000026 RID: 38
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyFlattenedNode.h")]
	public readonly struct HierarchyFlattenedNode : IEquatable<HierarchyFlattenedNode>
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00005582 File Offset: 0x00003782
		public static readonly ref HierarchyFlattenedNode Null
		{
			get
			{
				return ref HierarchyFlattenedNode.s_Null;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00005589 File Offset: 0x00003789
		public HierarchyNode Node
		{
			get
			{
				return this.m_Node;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00005591 File Offset: 0x00003791
		public HierarchyNodeType Type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00005599 File Offset: 0x00003799
		public int ParentOffset
		{
			get
			{
				return this.m_ParentOffset;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000055A1 File Offset: 0x000037A1
		public int NextSiblingOffset
		{
			get
			{
				return this.m_NextSiblingOffset;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000055A9 File Offset: 0x000037A9
		public int ChildrenCount
		{
			get
			{
				return this.m_ChildrenCount;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000055B1 File Offset: 0x000037B1
		public int Depth
		{
			get
			{
				return this.m_Depth;
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000055B9 File Offset: 0x000037B9
		public unsafe HierarchyFlattenedNode()
		{
			this.m_Node = *HierarchyNode.Null;
			this.m_Type = *HierarchyNodeType.Null;
			this.m_ParentOffset = 0;
			this.m_NextSiblingOffset = 0;
			this.m_ChildrenCount = 0;
			this.m_Depth = 0;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000055F8 File Offset: 0x000037F8
		[ExcludeFromDocs]
		public static bool operator ==(in HierarchyFlattenedNode lhs, in HierarchyFlattenedNode rhs)
		{
			HierarchyNode node = lhs.Node;
			HierarchyNode node2 = rhs.Node;
			return (in node) == (in node2);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000561C File Offset: 0x0000381C
		[ExcludeFromDocs]
		public static bool operator !=(in HierarchyFlattenedNode lhs, in HierarchyFlattenedNode rhs)
		{
			return !((in lhs) == (in rhs));
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005628 File Offset: 0x00003828
		[ExcludeFromDocs]
		public bool Equals(HierarchyFlattenedNode other)
		{
			HierarchyNode node = other.Node;
			HierarchyNode node2 = this.Node;
			return (in node) == (in node2);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005650 File Offset: 0x00003850
		[ExcludeFromDocs]
		public override string ToString()
		{
			return "HierarchyFlattenedNode(" + (((in this) == HierarchyFlattenedNode.Null) ? "Null" : string.Format("{0}:{1}", this.Node.Id, this.Node.Version)) + ")";
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000056B0 File Offset: 0x000038B0
		[ExcludeFromDocs]
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is HierarchyFlattenedNode)
			{
				HierarchyFlattenedNode hierarchyFlattenedNode = (HierarchyFlattenedNode)obj;
				flag = this.Equals(hierarchyFlattenedNode);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000056D8 File Offset: 0x000038D8
		[ExcludeFromDocs]
		public override int GetHashCode()
		{
			return this.Node.GetHashCode();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000056F9 File Offset: 0x000038F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static readonly ref HierarchyNode GetNodeByRef(in HierarchyFlattenedNode hierarchyFlattenedNode)
		{
			return ref hierarchyFlattenedNode.m_Node;
		}

		// Token: 0x04000058 RID: 88
		private static readonly HierarchyFlattenedNode s_Null;

		// Token: 0x04000059 RID: 89
		private readonly HierarchyNode m_Node;

		// Token: 0x0400005A RID: 90
		private readonly HierarchyNodeType m_Type;

		// Token: 0x0400005B RID: 91
		private readonly int m_ParentOffset;

		// Token: 0x0400005C RID: 92
		private readonly int m_NextSiblingOffset;

		// Token: 0x0400005D RID: 93
		private readonly int m_ChildrenCount;

		// Token: 0x0400005E RID: 94
		private readonly int m_Depth;
	}
}
