using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000E RID: 14
	[AttributeUsage(2048, Inherited = false)]
	internal sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x0600008C RID: 140 RVA: 0x000028C4 File Offset: 0x00000AC4
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000028D3 File Offset: 0x00000AD3
		public bool ReturnValue { get; }
	}
}
