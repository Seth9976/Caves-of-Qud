using System;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Vivox.Internal
{
	// Token: 0x02000008 RID: 8
	[RequireImplementors]
	public interface IVivox : IServiceComponent
	{
		// Token: 0x06000011 RID: 17
		void RegisterTokenProvider(IVivoxTokenProviderInternal tokenProvider);
	}
}
