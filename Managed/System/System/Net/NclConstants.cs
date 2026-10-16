using System;

namespace System.Net
{
	// Token: 0x020005E1 RID: 1505
	internal static class NclConstants
	{
		// Token: 0x04001B24 RID: 6948
		internal static readonly object Sentinel = new object();

		// Token: 0x04001B25 RID: 6949
		internal static readonly object[] EmptyObjectArray = new object[0];

		// Token: 0x04001B26 RID: 6950
		internal static readonly Uri[] EmptyUriArray = new Uri[0];

		// Token: 0x04001B27 RID: 6951
		internal static readonly byte[] CRLF = new byte[] { 13, 10 };

		// Token: 0x04001B28 RID: 6952
		internal static readonly byte[] ChunkTerminator = new byte[] { 48, 13, 10, 13, 10 };
	}
}
