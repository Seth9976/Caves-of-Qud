using System;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Authentication.Internal
{
	// Token: 0x02000012 RID: 18
	[RequireImplementors]
	public interface IPlayerNameComponent : IServiceComponent
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27
		string PlayerName { get; }

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600001C RID: 28
		// (remove) Token: 0x0600001D RID: 29
		event Action<string> PlayerNameChanged;
	}
}
