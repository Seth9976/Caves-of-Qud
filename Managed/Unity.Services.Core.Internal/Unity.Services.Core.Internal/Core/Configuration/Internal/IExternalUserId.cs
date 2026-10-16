using System;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Core.Configuration.Internal
{
	// Token: 0x02000024 RID: 36
	[RequireImplementors]
	public interface IExternalUserId : IServiceComponent
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000064 RID: 100
		string UserId { get; }

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000065 RID: 101
		// (remove) Token: 0x06000066 RID: 102
		event Action<string> UserIdChanged;
	}
}
