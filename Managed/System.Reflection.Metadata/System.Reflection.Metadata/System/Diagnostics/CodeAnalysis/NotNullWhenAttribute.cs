using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000F RID: 15
	[AttributeUsage(2048, Inherited = false)]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x0600008E RID: 142 RVA: 0x000028DB File Offset: 0x00000ADB
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000028EA File Offset: 0x00000AEA
		public bool ReturnValue { get; }
	}
}
