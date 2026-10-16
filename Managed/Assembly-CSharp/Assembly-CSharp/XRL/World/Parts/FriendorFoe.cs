using System;

namespace XRL.World.Parts
{
	// Token: 0x02000EBD RID: 3773
	[Serializable]
	public class FriendorFoe
	{
		// Token: 0x06009758 RID: 38744 RVA: 0x0036E734 File Offset: 0x0036C934
		public FriendorFoe()
		{
		}

		// Token: 0x06009759 RID: 38745 RVA: 0x0036E73C File Offset: 0x0036C93C
		public FriendorFoe(string faction, string status, string reason)
		{
			this.faction = faction;
			this.status = status;
			this.reason = reason;
		}

		// Token: 0x0600975A RID: 38746 RVA: 0x0036E759 File Offset: 0x0036C959
		public FriendorFoe(FriendorFoe p)
		{
			this.faction = p.faction;
			this.status = p.status;
			this.reason = p.reason;
		}

		// Token: 0x04003EEB RID: 16107
		public string faction;

		// Token: 0x04003EEC RID: 16108
		public string status;

		// Token: 0x04003EED RID: 16109
		public string reason;
	}
}
