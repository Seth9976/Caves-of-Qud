using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A0B RID: 2571
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x06005B67 RID: 23399 RVA: 0x00134A38 File Offset: 0x00132C38
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06005B68 RID: 23400 RVA: 0x00134A47 File Offset: 0x00132C47
		public bool ParameterValue { get; }
	}
}
