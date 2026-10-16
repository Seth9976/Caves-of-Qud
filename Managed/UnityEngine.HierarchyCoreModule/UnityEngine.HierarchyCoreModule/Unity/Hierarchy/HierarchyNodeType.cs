using System;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace Unity.Hierarchy
{
	// Token: 0x02000029 RID: 41
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyNodeType.h")]
	public readonly struct HierarchyNodeType : IEquatable<HierarchyNodeType>
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00005805 File Offset: 0x00003A05
		public static readonly ref HierarchyNodeType Null
		{
			get
			{
				return ref HierarchyNodeType.s_Null;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000580C File Offset: 0x00003A0C
		public int Id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005814 File Offset: 0x00003A14
		public HierarchyNodeType()
		{
			this.m_Id = 0;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000581E File Offset: 0x00003A1E
		internal HierarchyNodeType(int id)
		{
			this.m_Id = id;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00005828 File Offset: 0x00003A28
		[ExcludeFromDocs]
		public static bool operator ==(in HierarchyNodeType lhs, in HierarchyNodeType rhs)
		{
			return lhs.Id == rhs.Id;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005838 File Offset: 0x00003A38
		[ExcludeFromDocs]
		public static bool operator !=(in HierarchyNodeType lhs, in HierarchyNodeType rhs)
		{
			return !((in lhs) == (in rhs));
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00005844 File Offset: 0x00003A44
		[ExcludeFromDocs]
		public bool Equals(HierarchyNodeType other)
		{
			return other.Id == this.Id;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00005855 File Offset: 0x00003A55
		[ExcludeFromDocs]
		public override string ToString()
		{
			return string.Format("{0}({1})", "HierarchyNodeType", ((in this) == HierarchyNodeType.Null) ? "Null" : this.Id);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00005888 File Offset: 0x00003A88
		[ExcludeFromDocs]
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is HierarchyNodeType)
			{
				HierarchyNodeType hierarchyNodeType = (HierarchyNodeType)obj;
				flag = this.Equals(hierarchyNodeType);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000058B0 File Offset: 0x00003AB0
		[ExcludeFromDocs]
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		// Token: 0x0400006A RID: 106
		internal const int k_HierarchyNodeTypeNull = 0;

		// Token: 0x0400006B RID: 107
		private static readonly HierarchyNodeType s_Null;

		// Token: 0x0400006C RID: 108
		private readonly int m_Id;
	}
}
