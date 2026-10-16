using System;

namespace NAudio.CoreAudioApi
{
	// Token: 0x02000127 RID: 295
	public struct PropertyKey
	{
		// Token: 0x06000647 RID: 1607 RVA: 0x0001369C File Offset: 0x0001189C
		public PropertyKey(Guid formatId, int propertyId)
		{
			this.formatId = formatId;
			this.propertyId = propertyId;
		}

		// Token: 0x040006F8 RID: 1784
		public Guid formatId;

		// Token: 0x040006F9 RID: 1785
		public int propertyId;
	}
}
