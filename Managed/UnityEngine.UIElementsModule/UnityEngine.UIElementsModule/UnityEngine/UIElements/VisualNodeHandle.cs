using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020005FE RID: 1534
	[NativeType(Header = "Modules/UIElements/VisualNodeHandle.h")]
	internal readonly struct VisualNodeHandle : IEquatable<VisualNodeHandle>
	{
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06002DAD RID: 11693 RVA: 0x000A2409 File Offset: 0x000A0609
		public int Id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06002DAE RID: 11694 RVA: 0x000A2411 File Offset: 0x000A0611
		public int Version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x000A2419 File Offset: 0x000A0619
		public VisualNodeHandle(int id, int version)
		{
			this.m_Id = id;
			this.m_Version = version;
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x000A242A File Offset: 0x000A062A
		public static bool operator ==(in VisualNodeHandle lhs, in VisualNodeHandle rhs)
		{
			return lhs.Id == rhs.Id && lhs.Version == rhs.Version;
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x000A244B File Offset: 0x000A064B
		public static bool operator !=(in VisualNodeHandle lhs, in VisualNodeHandle rhs)
		{
			return !((in lhs) == (in rhs));
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x000A2457 File Offset: 0x000A0657
		public bool Equals(VisualNodeHandle other)
		{
			return other.Id == this.Id && other.Version == this.Version;
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x000A247C File Offset: 0x000A067C
		public override string ToString()
		{
			return "VisualNodeHandle(" + (((in this) == (in VisualNodeHandle.Null)) ? "Null" : string.Format("{0}:{1}", this.Id, this.Version)) + ")";
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x000A24CC File Offset: 0x000A06CC
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is VisualNodeHandle)
			{
				VisualNodeHandle visualNodeHandle = (VisualNodeHandle)obj;
				flag = this.Equals(visualNodeHandle);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x000A24F2 File Offset: 0x000A06F2
		public override int GetHashCode()
		{
			return HashCode.Combine<int, int>(this.Id, this.Version);
		}

		// Token: 0x040014A5 RID: 5285
		public static readonly VisualNodeHandle Null;

		// Token: 0x040014A6 RID: 5286
		private readonly int m_Id;

		// Token: 0x040014A7 RID: 5287
		private readonly int m_Version;
	}
}
