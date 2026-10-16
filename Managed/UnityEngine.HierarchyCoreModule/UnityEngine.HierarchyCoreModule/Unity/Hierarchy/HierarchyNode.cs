using System;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace Unity.Hierarchy
{
	// Token: 0x02000027 RID: 39
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyNode.h")]
	public readonly struct HierarchyNode : IEquatable<HierarchyNode>
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00005701 File Offset: 0x00003901
		public static readonly ref HierarchyNode Null
		{
			get
			{
				return ref HierarchyNode.s_Null;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00005708 File Offset: 0x00003908
		public int Id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00005710 File Offset: 0x00003910
		public int Version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00005718 File Offset: 0x00003918
		public HierarchyNode()
		{
			this.m_Id = 0;
			this.m_Version = 0;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00005729 File Offset: 0x00003929
		[ExcludeFromDocs]
		public static bool operator ==(in HierarchyNode lhs, in HierarchyNode rhs)
		{
			return lhs.Id == rhs.Id && lhs.Version == rhs.Version;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000574A File Offset: 0x0000394A
		[ExcludeFromDocs]
		public static bool operator !=(in HierarchyNode lhs, in HierarchyNode rhs)
		{
			return !((in lhs) == (in rhs));
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005756 File Offset: 0x00003956
		[ExcludeFromDocs]
		public bool Equals(HierarchyNode other)
		{
			return other.Id == this.Id && other.Version == this.Version;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000577C File Offset: 0x0000397C
		[ExcludeFromDocs]
		public override string ToString()
		{
			return "HierarchyNode(" + (((in this) == HierarchyNode.Null) ? "Null" : string.Format("{0}:{1}", this.Id, this.Version)) + ")";
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000057CC File Offset: 0x000039CC
		[ExcludeFromDocs]
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is HierarchyNode)
			{
				HierarchyNode hierarchyNode = (HierarchyNode)obj;
				flag = this.Equals(hierarchyNode);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000057F2 File Offset: 0x000039F2
		[ExcludeFromDocs]
		public override int GetHashCode()
		{
			return HashCode.Combine<int, int>(this.Id, this.Version);
		}

		// Token: 0x0400005F RID: 95
		private const int k_HierarchyNodeIdNull = 0;

		// Token: 0x04000060 RID: 96
		private const int k_HierarchyNodeVersionNull = 0;

		// Token: 0x04000061 RID: 97
		private static readonly HierarchyNode s_Null;

		// Token: 0x04000062 RID: 98
		private readonly int m_Id;

		// Token: 0x04000063 RID: 99
		private readonly int m_Version;
	}
}
