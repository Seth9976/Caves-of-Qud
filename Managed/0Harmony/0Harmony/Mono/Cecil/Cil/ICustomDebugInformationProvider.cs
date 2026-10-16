using System;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002E0 RID: 736
	internal interface ICustomDebugInformationProvider : IMetadataTokenProvider
	{
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060012BA RID: 4794
		bool HasCustomDebugInformations { get; }

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060012BB RID: 4795
		Collection<CustomDebugInformation> CustomDebugInformations { get; }
	}
}
