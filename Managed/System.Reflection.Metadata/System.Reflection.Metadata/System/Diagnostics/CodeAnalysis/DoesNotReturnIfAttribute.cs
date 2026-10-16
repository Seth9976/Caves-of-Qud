using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(2048, Inherited = false)]
	internal sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00002911 File Offset: 0x00000B11
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002920 File Offset: 0x00000B20
		public bool ParameterValue { get; }
	}
}
