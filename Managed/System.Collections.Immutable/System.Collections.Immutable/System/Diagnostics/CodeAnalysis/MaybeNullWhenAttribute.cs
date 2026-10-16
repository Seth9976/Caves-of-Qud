using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000D RID: 13
	[AttributeUsage(2048, Inherited = false)]
	internal sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002354 File Offset: 0x00000554
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002363 File Offset: 0x00000563
		public bool ReturnValue { get; }
	}
}
