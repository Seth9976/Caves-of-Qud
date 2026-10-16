using System;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Internal;
using UnityEngine;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x02000003 RID: 3
	internal class CloudProjectId : ICloudProjectId, IServiceComponent
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public string GetCloudProjectId()
		{
			return Application.cloudProjectId;
		}
	}
}
