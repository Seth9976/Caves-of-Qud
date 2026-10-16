using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000122 RID: 290
	[InterfaceType(1)]
	[Guid("1BE09788-6894-4089-8586-9A2A6C265AC5")]
	internal interface IMMEndpoint
	{
		// Token: 0x0600063B RID: 1595
		int GetDataFlow(out DataFlow dataFlow);
	}
}
