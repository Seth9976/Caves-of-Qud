using System;

namespace Steamworks
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
	internal class CallbackIdentityAttribute : Attribute
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002C27 File Offset: 0x00000E27
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002C2F File Offset: 0x00000E2F
		public int Identity { get; set; }

		// Token: 0x06000035 RID: 53 RVA: 0x00002C38 File Offset: 0x00000E38
		public CallbackIdentityAttribute(int callbackNum)
		{
			this.Identity = callbackNum;
		}
	}
}
