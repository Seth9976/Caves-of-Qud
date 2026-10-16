using System;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Authentication.Internal
{
	// Token: 0x0200000E RID: 14
	[RequireImplementors]
	public interface IAccessToken : IServiceComponent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000014 RID: 20
		string AccessToken { get; }
	}
}
