using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000E RID: 14
	[AttributeUsage(2048, Inherited = false)]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000236B File Offset: 0x0000056B
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000237A File Offset: 0x0000057A
		public bool ReturnValue { get; }
	}
}
