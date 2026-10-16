using System;

namespace System.Net
{
	// Token: 0x020005E8 RID: 1512
	internal enum ContextAttribute
	{
		// Token: 0x04001B68 RID: 7016
		Sizes,
		// Token: 0x04001B69 RID: 7017
		Names,
		// Token: 0x04001B6A RID: 7018
		Lifespan,
		// Token: 0x04001B6B RID: 7019
		DceInfo,
		// Token: 0x04001B6C RID: 7020
		StreamSizes,
		// Token: 0x04001B6D RID: 7021
		Authority = 6,
		// Token: 0x04001B6E RID: 7022
		PackageInfo = 10,
		// Token: 0x04001B6F RID: 7023
		NegotiationInfo = 12,
		// Token: 0x04001B70 RID: 7024
		UniqueBindings = 25,
		// Token: 0x04001B71 RID: 7025
		EndpointBindings,
		// Token: 0x04001B72 RID: 7026
		ClientSpecifiedSpn,
		// Token: 0x04001B73 RID: 7027
		RemoteCertificate = 83,
		// Token: 0x04001B74 RID: 7028
		LocalCertificate,
		// Token: 0x04001B75 RID: 7029
		RootStore,
		// Token: 0x04001B76 RID: 7030
		IssuerListInfoEx = 89,
		// Token: 0x04001B77 RID: 7031
		ConnectionInfo,
		// Token: 0x04001B78 RID: 7032
		UiInfo = 104
	}
}
