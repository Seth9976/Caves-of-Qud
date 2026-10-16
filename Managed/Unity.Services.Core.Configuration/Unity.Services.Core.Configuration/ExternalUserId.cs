using System;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x02000007 RID: 7
	internal class ExternalUserId : IExternalUserId, IServiceComponent
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002239 File Offset: 0x00000439
		public string UserId
		{
			get
			{
				return UnityServices.ExternalUserIdProperty.UserId;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000014 RID: 20 RVA: 0x00002245 File Offset: 0x00000445
		// (remove) Token: 0x06000015 RID: 21 RVA: 0x00002252 File Offset: 0x00000452
		public event Action<string> UserIdChanged
		{
			add
			{
				UnityServices.ExternalUserIdProperty.UserIdChanged += value;
			}
			remove
			{
				UnityServices.ExternalUserIdProperty.UserIdChanged -= value;
			}
		}
	}
}
