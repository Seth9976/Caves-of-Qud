using System;
using UnityEngine.Identifiers;

namespace Unity.Services.Core.Device
{
	// Token: 0x02000007 RID: 7
	internal class UnityEngineIdentifier : IUserIdentifierProvider
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000226C File Offset: 0x0000046C
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002273 File Offset: 0x00000473
		public string UserId
		{
			get
			{
				return Identifiers.installationId;
			}
			set
			{
			}
		}
	}
}
