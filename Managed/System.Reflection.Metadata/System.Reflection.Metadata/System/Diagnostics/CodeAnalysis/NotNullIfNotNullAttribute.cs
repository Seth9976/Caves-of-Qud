using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000010 RID: 16
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(10368, AllowMultiple = true, Inherited = false)]
	internal sealed class NotNullIfNotNullAttribute : Attribute
	{
		// Token: 0x06000090 RID: 144 RVA: 0x000028F2 File Offset: 0x00000AF2
		public NotNullIfNotNullAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002901 File Offset: 0x00000B01
		public string ParameterName { get; }
	}
}
