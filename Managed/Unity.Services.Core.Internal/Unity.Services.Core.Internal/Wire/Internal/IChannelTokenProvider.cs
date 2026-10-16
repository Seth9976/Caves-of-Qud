using System;
using System.Threading.Tasks;

namespace Unity.Services.Wire.Internal
{
	// Token: 0x02000005 RID: 5
	public interface IChannelTokenProvider
	{
		// Token: 0x0600000F RID: 15
		Task<ChannelToken> GetTokenAsync();
	}
}
