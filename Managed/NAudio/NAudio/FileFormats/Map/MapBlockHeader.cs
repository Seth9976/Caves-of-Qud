using System;
using System.IO;

namespace NAudio.FileFormats.Map
{
	// Token: 0x020000A3 RID: 163
	internal class MapBlockHeader
	{
		// Token: 0x06000390 RID: 912 RVA: 0x0000C354 File Offset: 0x0000A554
		public static MapBlockHeader Read(BinaryReader reader)
		{
			return new MapBlockHeader
			{
				length = reader.ReadInt32(),
				value2 = reader.ReadInt32(),
				value3 = reader.ReadInt16(),
				value4 = reader.ReadInt16()
			};
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000C398 File Offset: 0x0000A598
		public override string ToString()
		{
			return string.Format("{0} {1:X8} {2:X4} {3:X4}", new object[] { this.length, this.value2, this.value3, this.value4 });
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000C3EF File Offset: 0x0000A5EF
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x04000453 RID: 1107
		private int length;

		// Token: 0x04000454 RID: 1108
		private int value2;

		// Token: 0x04000455 RID: 1109
		private short value3;

		// Token: 0x04000456 RID: 1110
		private short value4;
	}
}
