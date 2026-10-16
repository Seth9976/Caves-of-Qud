using System;
using System.Threading.Tasks;

namespace Unity.Services.Vivox.Internal
{
	// Token: 0x02000009 RID: 9
	public interface IVivoxTokenProviderInternal
	{
		// Token: 0x06000012 RID: 18
		Task<string> GetTokenAsync(string issuer = null, TimeSpan? expiration = null, string userUri = null, string action = null, string conferenceUri = null, string fromUserUri = null, string realm = null);
	}
}
