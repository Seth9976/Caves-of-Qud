using System;
using UnityEngine.Bindings;

namespace UnityEngine.Animations
{
	// Token: 0x0200006A RID: 106
	[NativeHeader("Modules/Animation/BoundProperty.h")]
	public readonly struct BoundProperty : IEquatable<BoundProperty>, IComparable<BoundProperty>
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0000C3CE File Offset: 0x0000A5CE
		public int index
		{
			get
			{
				return this.m_Index;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0000C3D6 File Offset: 0x0000A5D6
		public int version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		public static BoundProperty Null
		{
			get
			{
				return default(BoundProperty);
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0000C3F8 File Offset: 0x0000A5F8
		public static bool operator ==(BoundProperty lhs, BoundProperty rhs)
		{
			return lhs.m_Index == rhs.m_Index && lhs.m_Version == rhs.m_Version;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0000C42C File Offset: 0x0000A62C
		public static bool operator !=(BoundProperty lhs, BoundProperty rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0000C448 File Offset: 0x0000A648
		public override bool Equals(object compare)
		{
			bool flag;
			if (compare is BoundProperty)
			{
				BoundProperty boundProperty = (BoundProperty)compare;
				flag = this.Equals(boundProperty);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0000C474 File Offset: 0x0000A674
		public bool Equals(BoundProperty boundProperty)
		{
			return boundProperty.m_Index == this.m_Index && boundProperty.m_Version == this.m_Version;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
		public int CompareTo(BoundProperty other)
		{
			return this.m_Index - other.m_Index;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0000C4C8 File Offset: 0x0000A6C8
		public override int GetHashCode()
		{
			return (this.m_Version * 397) ^ this.m_Index;
		}

		// Token: 0x0400018D RID: 397
		private readonly int m_Index;

		// Token: 0x0400018E RID: 398
		private readonly int m_Version;
	}
}
