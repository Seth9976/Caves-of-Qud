using System;
using System.Security.Principal;

namespace System.Web
{
	// Token: 0x0200004B RID: 75
	internal interface IPrincipalContainer
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060003CE RID: 974
		// (set) Token: 0x060003CF RID: 975
		IPrincipal Principal { get; set; }
	}
}
