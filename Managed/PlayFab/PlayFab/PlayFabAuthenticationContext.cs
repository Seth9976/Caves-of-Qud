using System;

namespace PlayFab
{
	// Token: 0x02000024 RID: 36
	public sealed class PlayFabAuthenticationContext
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x0000FD28 File Offset: 0x0000DF28
		public PlayFabAuthenticationContext()
		{
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000FD30 File Offset: 0x0000DF30
		public PlayFabAuthenticationContext(string clientSessionTicket, string entityToken, string playFabId, string entityId, string entityType)
			: this()
		{
			this.ClientSessionTicket = clientSessionTicket;
			this.PlayFabId = playFabId;
			this.EntityToken = entityToken;
			this.EntityId = entityId;
			this.EntityType = entityType;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000FD5D File Offset: 0x0000DF5D
		public void CopyFrom(PlayFabAuthenticationContext other)
		{
			this.ClientSessionTicket = other.ClientSessionTicket;
			this.PlayFabId = other.PlayFabId;
			this.EntityToken = other.EntityToken;
			this.EntityId = other.EntityId;
			this.EntityType = other.EntityType;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000FD9B File Offset: 0x0000DF9B
		public bool IsClientLoggedIn()
		{
			return !string.IsNullOrEmpty(this.ClientSessionTicket);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000FDAB File Offset: 0x0000DFAB
		public bool IsEntityLoggedIn()
		{
			return !string.IsNullOrEmpty(this.EntityToken);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000FDBB File Offset: 0x0000DFBB
		public void ForgetAllCredentials()
		{
			this.PlayFabId = null;
			this.ClientSessionTicket = null;
			this.EntityToken = null;
			this.EntityId = null;
			this.EntityType = null;
		}

		// Token: 0x0400028E RID: 654
		public string ClientSessionTicket;

		// Token: 0x0400028F RID: 655
		public string PlayFabId;

		// Token: 0x04000290 RID: 656
		public string EntityToken;

		// Token: 0x04000291 RID: 657
		public string EntityId;

		// Token: 0x04000292 RID: 658
		public string EntityType;
	}
}
