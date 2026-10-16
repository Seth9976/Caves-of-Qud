using System;
using System.IO;
using System.Linq;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000403 RID: 1027
	internal class PdbFileHeader
	{
		// Token: 0x060015CC RID: 5580 RVA: 0x0004584C File Offset: 0x00043A4C
		internal PdbFileHeader(Stream reader, BitAccess bits)
		{
			bits.MinCapacity(56);
			reader.Seek(0L, SeekOrigin.Begin);
			bits.FillBuffer(reader, 52);
			this.magic = new byte[32];
			bits.ReadBytes(this.magic);
			bits.ReadInt32(out this.pageSize);
			bits.ReadInt32(out this.freePageMap);
			bits.ReadInt32(out this.pagesUsed);
			bits.ReadInt32(out this.directorySize);
			bits.ReadInt32(out this.zero);
			if (!this.magic.SequenceEqual<byte>(this.windowsPdbMagic))
			{
				throw new PdbException("The PDB file is not recognized as a Windows PDB file", new object[0]);
			}
			int num = ((this.directorySize + this.pageSize - 1) / this.pageSize * 4 + this.pageSize - 1) / this.pageSize;
			this.directoryRoot = new int[num];
			bits.FillBuffer(reader, num * 4);
			bits.ReadInt32(this.directoryRoot);
		}

		// Token: 0x04000F45 RID: 3909
		private readonly byte[] windowsPdbMagic = new byte[]
		{
			77, 105, 99, 114, 111, 115, 111, 102, 116, 32,
			67, 47, 67, 43, 43, 32, 77, 83, 70, 32,
			55, 46, 48, 48, 13, 10, 26, 68, 83, 0,
			0, 0
		};

		// Token: 0x04000F46 RID: 3910
		internal readonly byte[] magic;

		// Token: 0x04000F47 RID: 3911
		internal readonly int pageSize;

		// Token: 0x04000F48 RID: 3912
		internal int freePageMap;

		// Token: 0x04000F49 RID: 3913
		internal int pagesUsed;

		// Token: 0x04000F4A RID: 3914
		internal int directorySize;

		// Token: 0x04000F4B RID: 3915
		internal readonly int zero;

		// Token: 0x04000F4C RID: 3916
		internal int[] directoryRoot;
	}
}
