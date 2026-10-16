using System;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000591 RID: 1425
	[RequiredByNativeCode]
	internal class ScriptableRuntimeReflectionSystemWrapper
	{
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06003703 RID: 14083 RVA: 0x0006C907 File Offset: 0x0006AB07
		// (set) Token: 0x06003704 RID: 14084 RVA: 0x0006C90F File Offset: 0x0006AB0F
		internal IScriptableRuntimeReflectionSystem implementation { get; set; }

		// Token: 0x06003705 RID: 14085 RVA: 0x0006C918 File Offset: 0x0006AB18
		[RequiredByNativeCode]
		private void Internal_ScriptableRuntimeReflectionSystemWrapper_TickRealtimeProbes(out bool result)
		{
			result = this.implementation != null && this.implementation.TickRealtimeProbes();
		}
	}
}
