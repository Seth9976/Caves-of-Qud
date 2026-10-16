using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(2048, Inherited = false)]
	internal sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000023A1 File Offset: 0x000005A1
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000023B0 File Offset: 0x000005B0
		public bool ParameterValue { get; }
	}
}
