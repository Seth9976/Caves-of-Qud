using System;

namespace System.Web.Hosting
{
	// Token: 0x02000531 RID: 1329
	internal struct HTTP_COOKED_URL
	{
		// Token: 0x04001FC0 RID: 8128
		internal readonly ushort FullUrlLength;

		// Token: 0x04001FC1 RID: 8129
		internal readonly ushort HostLength;

		// Token: 0x04001FC2 RID: 8130
		internal readonly ushort AbsPathLength;

		// Token: 0x04001FC3 RID: 8131
		internal readonly ushort QueryStringLength;

		// Token: 0x04001FC4 RID: 8132
		internal unsafe readonly char* pFullUrl;

		// Token: 0x04001FC5 RID: 8133
		internal unsafe readonly char* pHost;

		// Token: 0x04001FC6 RID: 8134
		internal unsafe readonly char* pAbsPath;

		// Token: 0x04001FC7 RID: 8135
		internal unsafe readonly char* pQueryString;
	}
}
