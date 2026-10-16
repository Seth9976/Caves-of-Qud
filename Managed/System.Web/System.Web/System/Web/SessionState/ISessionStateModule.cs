using System;
using System.Threading.Tasks;

namespace System.Web.SessionState
{
	// Token: 0x020006DB RID: 1755
	public interface ISessionStateModule : IHttpModule
	{
		// Token: 0x06004A75 RID: 19061
		void ReleaseSessionState(HttpContext context);

		// Token: 0x06004A76 RID: 19062
		Task ReleaseSessionStateAsync(HttpContext context);
	}
}
