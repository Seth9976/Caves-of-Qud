using System;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Authentication.Internal
{
	// Token: 0x0200000F RID: 15
	[RequireImplementors]
	public interface IAccessTokenObserver : IServiceComponent
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000015 RID: 21
		// (remove) Token: 0x06000016 RID: 22
		event Action<string> AccessTokenChanged;
	}
}
