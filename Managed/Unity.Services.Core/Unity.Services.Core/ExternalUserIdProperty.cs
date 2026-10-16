using System;

namespace Unity.Services.Core
{
	// Token: 0x02000005 RID: 5
	internal class ExternalUserIdProperty
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000008 RID: 8 RVA: 0x000020EC File Offset: 0x000002EC
		// (remove) Token: 0x06000009 RID: 9 RVA: 0x00002124 File Offset: 0x00000324
		public event Action<string> UserIdChanged;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002159 File Offset: 0x00000359
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002161 File Offset: 0x00000361
		public string UserId
		{
			get
			{
				return this.m_UserId;
			}
			set
			{
				this.m_UserId = value;
				Action<string> userIdChanged = this.UserIdChanged;
				if (userIdChanged == null)
				{
					return;
				}
				userIdChanged(this.m_UserId);
			}
		}

		// Token: 0x04000012 RID: 18
		private string m_UserId;
	}
}
