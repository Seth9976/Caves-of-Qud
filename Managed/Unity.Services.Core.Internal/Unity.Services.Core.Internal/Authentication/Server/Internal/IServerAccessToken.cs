using System;
using Unity.Services.Authentication.Internal;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Authentication.Server.Internal
{
	// Token: 0x0200000C RID: 12
	[RequireImplementors]
	public interface IServerAccessToken : IAccessToken, IServiceComponent, IAccessTokenObserver
	{
	}
}
