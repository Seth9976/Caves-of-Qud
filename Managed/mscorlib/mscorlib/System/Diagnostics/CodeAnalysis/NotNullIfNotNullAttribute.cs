using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A09 RID: 2569
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	public sealed class NotNullIfNotNullAttribute : Attribute
	{
		// Token: 0x06005B64 RID: 23396 RVA: 0x00134A21 File Offset: 0x00132C21
		public NotNullIfNotNullAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06005B65 RID: 23397 RVA: 0x00134A30 File Offset: 0x00132C30
		public string ParameterName { get; }
	}
}
