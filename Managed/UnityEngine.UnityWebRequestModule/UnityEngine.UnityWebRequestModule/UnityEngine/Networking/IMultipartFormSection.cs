using System;

namespace UnityEngine.Networking
{
	// Token: 0x0200000F RID: 15
	public interface IMultipartFormSection
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000073 RID: 115
		string sectionName { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000074 RID: 116
		byte[] sectionData { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000075 RID: 117
		string fileName { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000076 RID: 118
		string contentType { get; }
	}
}
