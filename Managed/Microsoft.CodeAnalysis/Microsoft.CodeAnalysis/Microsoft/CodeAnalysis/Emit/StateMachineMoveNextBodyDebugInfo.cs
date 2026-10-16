using System;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000496 RID: 1174
	internal abstract class StateMachineMoveNextBodyDebugInfo
	{
		// Token: 0x06002E39 RID: 11833 RVA: 0x00082F9F File Offset: 0x0008119F
		public StateMachineMoveNextBodyDebugInfo(IMethodDefinition kickoffMethod)
		{
			this.KickoffMethod = kickoffMethod;
		}

		// Token: 0x04001269 RID: 4713
		public readonly IMethodDefinition KickoffMethod;
	}
}
