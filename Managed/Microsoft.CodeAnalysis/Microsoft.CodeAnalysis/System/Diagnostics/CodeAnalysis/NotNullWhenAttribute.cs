using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000017 RID: 23
	[AttributeUsage(2048, Inherited = false)]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002CFF File Offset: 0x00000EFF
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002D0E File Offset: 0x00000F0E
		public bool ReturnValue { get; }
	}
}
