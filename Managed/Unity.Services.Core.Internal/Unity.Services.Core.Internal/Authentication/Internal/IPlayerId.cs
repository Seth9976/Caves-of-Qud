using System;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Authentication.Internal
{
	// Token: 0x02000011 RID: 17
	[RequireImplementors]
	public interface IPlayerId : IServiceComponent
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24
		string PlayerId { get; }

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000019 RID: 25
		// (remove) Token: 0x0600001A RID: 26
		event Action<string> PlayerIdChanged;
	}
}
