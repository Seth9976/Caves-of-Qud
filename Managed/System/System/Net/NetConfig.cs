using System;

namespace System.Net
{
	// Token: 0x020006AC RID: 1708
	internal class NetConfig : ICloneable
	{
		// Token: 0x0600368A RID: 13962 RVA: 0x000BF7F6 File Offset: 0x000BD9F6
		internal NetConfig()
		{
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x000BF806 File Offset: 0x000BDA06
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x04001FF6 RID: 8182
		internal bool ipv6Enabled;

		// Token: 0x04001FF7 RID: 8183
		internal int MaxResponseHeadersLength = 64;
	}
}
