using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000016 RID: 22
	[AttributeUsage(2048, Inherited = false)]
	internal sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002CE8 File Offset: 0x00000EE8
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002CF7 File Offset: 0x00000EF7
		public bool ReturnValue { get; }
	}
}
