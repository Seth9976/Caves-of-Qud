using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000025 RID: 37
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
	internal class PreventExecutionInStateAttribute : Attribute, IBindingsPreventExecution
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002580 File Offset: 0x00000780
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002588 File Offset: 0x00000788
		public object singleFlagValue { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002591 File Offset: 0x00000791
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002599 File Offset: 0x00000799
		public PreventExecutionSeverity severity { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000025A2 File Offset: 0x000007A2
		// (set) Token: 0x06000077 RID: 119 RVA: 0x000025AA File Offset: 0x000007AA
		public string howToFix { get; set; }

		// Token: 0x06000078 RID: 120 RVA: 0x000025B3 File Offset: 0x000007B3
		public PreventExecutionInStateAttribute(object systemAndFlags, PreventExecutionSeverity reportSeverity, string howToString = "")
		{
			this.singleFlagValue = systemAndFlags;
			this.severity = reportSeverity;
			this.howToFix = howToString;
		}
	}
}
