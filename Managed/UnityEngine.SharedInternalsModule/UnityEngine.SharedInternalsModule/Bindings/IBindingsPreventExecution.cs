using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000024 RID: 36
	[VisibleToOtherModules]
	internal interface IBindingsPreventExecution
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006C RID: 108
		// (set) Token: 0x0600006D RID: 109
		object singleFlagValue { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006E RID: 110
		// (set) Token: 0x0600006F RID: 111
		PreventExecutionSeverity severity { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000070 RID: 112
		// (set) Token: 0x06000071 RID: 113
		string howToFix { get; set; }
	}
}
