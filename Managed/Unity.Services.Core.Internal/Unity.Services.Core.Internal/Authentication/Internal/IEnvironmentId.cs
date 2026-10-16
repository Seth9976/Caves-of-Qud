using System;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Authentication.Internal
{
	// Token: 0x02000010 RID: 16
	[RequireImplementors]
	public interface IEnvironmentId : IServiceComponent
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23
		string EnvironmentId { get; }
	}
}
