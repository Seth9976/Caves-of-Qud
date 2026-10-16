using System;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Wire.Internal
{
	// Token: 0x02000006 RID: 6
	[RequireImplementors]
	public interface IWire : IServiceComponent
	{
		// Token: 0x06000010 RID: 16
		IChannel CreateChannel(IChannelTokenProvider tokenProvider);
	}
}
