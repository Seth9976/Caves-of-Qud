using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A08 RID: 2568
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x06005B62 RID: 23394 RVA: 0x00134A0A File Offset: 0x00132C0A
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06005B63 RID: 23395 RVA: 0x00134A19 File Offset: 0x00132C19
		public bool ReturnValue { get; }
	}
}
