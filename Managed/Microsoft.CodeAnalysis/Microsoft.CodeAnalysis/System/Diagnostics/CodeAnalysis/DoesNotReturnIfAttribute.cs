using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200001A RID: 26
	[AttributeUsage(2048, Inherited = false)]
	internal sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00002D35 File Offset: 0x00000F35
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002D44 File Offset: 0x00000F44
		public bool ParameterValue { get; }
	}
}
