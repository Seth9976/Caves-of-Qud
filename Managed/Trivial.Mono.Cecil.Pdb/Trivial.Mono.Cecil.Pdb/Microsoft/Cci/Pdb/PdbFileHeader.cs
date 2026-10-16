using System;
using System.IO;
using System.Text;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000C1 RID: 193
	internal class PdbFileHeader
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00005CAE File Offset: 0x00003EAE
		private string Magic
		{
			get
			{
				return PdbFileHeader.StringFromBytesUTF8(this.magic, 0, "Microsoft C/C++ MSF 7.00".Length);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005CC8 File Offset: 0x00003EC8
		internal PdbFileHeader(Stream reader, BitAccess bits)
		{
			bits.MinCapacity(56);
			reader.Seek(0L, 0);
			bits.FillBuffer(reader, 52);
			this.magic = new byte[32];
			bits.ReadBytes(this.magic);
			bits.ReadInt32(out this.pageSize);
			bits.ReadInt32(out this.freePageMap);
			bits.ReadInt32(out this.pagesUsed);
			bits.ReadInt32(out this.directorySize);
			bits.ReadInt32(out this.zero);
			if (this.Magic != "Microsoft C/C++ MSF 7.00")
			{
				throw new InvalidOperationException("Magic is wrong.");
			}
			int num = ((this.directorySize + this.pageSize - 1) / this.pageSize * 4 + this.pageSize - 1) / this.pageSize;
			this.directoryRoot = new int[num];
			bits.FillBuffer(reader, num * 4);
			bits.ReadInt32(this.directoryRoot);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005DB3 File Offset: 0x00003FB3
		private static string StringFromBytesUTF8(byte[] bytes)
		{
			return PdbFileHeader.StringFromBytesUTF8(bytes, 0, bytes.Length);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005DC0 File Offset: 0x00003FC0
		private static string StringFromBytesUTF8(byte[] bytes, int offset, int length)
		{
			for (int i = 0; i < length; i++)
			{
				if (bytes[offset + i] < 32)
				{
					length = i;
				}
			}
			return Encoding.UTF8.GetString(bytes, offset, length);
		}

		// Token: 0x04000362 RID: 866
		private const string MAGIC = "Microsoft C/C++ MSF 7.00";

		// Token: 0x04000363 RID: 867
		internal readonly byte[] magic;

		// Token: 0x04000364 RID: 868
		internal readonly int pageSize;

		// Token: 0x04000365 RID: 869
		internal int freePageMap;

		// Token: 0x04000366 RID: 870
		internal int pagesUsed;

		// Token: 0x04000367 RID: 871
		internal int directorySize;

		// Token: 0x04000368 RID: 872
		internal readonly int zero;

		// Token: 0x04000369 RID: 873
		internal int[] directoryRoot;
	}
}
