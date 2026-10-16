using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A07 RID: 2567
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x06005B60 RID: 23392 RVA: 0x001349F3 File Offset: 0x00132BF3
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06005B61 RID: 23393 RVA: 0x00134A02 File Offset: 0x00132C02
		public bool ReturnValue { get; }
	}
}
