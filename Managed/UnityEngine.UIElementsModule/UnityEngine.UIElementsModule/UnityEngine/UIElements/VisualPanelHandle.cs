using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000609 RID: 1545
	[NativeType(Header = "Modules/UIElements/VisualPanelHandle.h")]
	internal readonly struct VisualPanelHandle : IEquatable<VisualPanelHandle>
	{
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x000A2790 File Offset: 0x000A0990
		public int Id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06002DC3 RID: 11715 RVA: 0x000A2798 File Offset: 0x000A0998
		public int Version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000A27A0 File Offset: 0x000A09A0
		public VisualPanelHandle(int id, int version)
		{
			this.m_Id = id;
			this.m_Version = version;
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000A27B1 File Offset: 0x000A09B1
		public static bool operator ==(in VisualPanelHandle lhs, in VisualPanelHandle rhs)
		{
			return lhs.Id == rhs.Id && lhs.Version == rhs.Version;
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x000A27D2 File Offset: 0x000A09D2
		public static bool operator !=(in VisualPanelHandle lhs, in VisualPanelHandle rhs)
		{
			return !((in lhs) == (in rhs));
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000A27DE File Offset: 0x000A09DE
		public bool Equals(VisualPanelHandle other)
		{
			return other.Id == this.Id && other.Version == this.Version;
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x000A2804 File Offset: 0x000A0A04
		public override string ToString()
		{
			return "VisualPanelHandle(" + (((in this) == (in VisualPanelHandle.Null)) ? "Null" : string.Format("{0}:{1}", this.Id, this.Version)) + ")";
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x000A2854 File Offset: 0x000A0A54
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is VisualPanelHandle)
			{
				VisualPanelHandle visualPanelHandle = (VisualPanelHandle)obj;
				flag = this.Equals(visualPanelHandle);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000A287A File Offset: 0x000A0A7A
		public override int GetHashCode()
		{
			return HashCode.Combine<int, int>(this.Id, this.Version);
		}

		// Token: 0x040014BB RID: 5307
		public static readonly VisualPanelHandle Null;

		// Token: 0x040014BC RID: 5308
		private readonly int m_Id;

		// Token: 0x040014BD RID: 5309
		private readonly int m_Version;
	}
}
