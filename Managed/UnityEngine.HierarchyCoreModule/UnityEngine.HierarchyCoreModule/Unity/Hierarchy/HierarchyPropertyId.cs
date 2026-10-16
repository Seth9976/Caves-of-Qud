using System;
using UnityEngine.Bindings;

namespace Unity.Hierarchy
{
	// Token: 0x0200002B RID: 43
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyPropertyId.h")]
	internal readonly struct HierarchyPropertyId : IEquatable<HierarchyPropertyId>
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000058ED File Offset: 0x00003AED
		public static readonly ref HierarchyPropertyId Null
		{
			get
			{
				return ref HierarchyPropertyId.s_Null;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000058F4 File Offset: 0x00003AF4
		public int Id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000058FC File Offset: 0x00003AFC
		public HierarchyPropertyId()
		{
			this.m_Id = 0;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00005906 File Offset: 0x00003B06
		internal HierarchyPropertyId(int id)
		{
			this.m_Id = id;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00005910 File Offset: 0x00003B10
		public static bool operator ==(in HierarchyPropertyId lhs, in HierarchyPropertyId rhs)
		{
			return lhs.Id == rhs.Id;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005920 File Offset: 0x00003B20
		public static bool operator !=(in HierarchyPropertyId lhs, in HierarchyPropertyId rhs)
		{
			return !((in lhs) == (in rhs));
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000592C File Offset: 0x00003B2C
		public bool Equals(HierarchyPropertyId other)
		{
			return other.Id == this.Id;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000593D File Offset: 0x00003B3D
		public override string ToString()
		{
			return string.Format("{0}({1})", "HierarchyPropertyId", ((in this) == HierarchyPropertyId.Null) ? "Null" : this.Id);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005970 File Offset: 0x00003B70
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is HierarchyPropertyId)
			{
				HierarchyPropertyId hierarchyPropertyId = (HierarchyPropertyId)obj;
				flag = this.Equals(hierarchyPropertyId);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00005998 File Offset: 0x00003B98
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		// Token: 0x0400006F RID: 111
		private const int k_HierarchyPropertyIdNull = 0;

		// Token: 0x04000070 RID: 112
		private static readonly HierarchyPropertyId s_Null;

		// Token: 0x04000071 RID: 113
		private readonly int m_Id;
	}
}
