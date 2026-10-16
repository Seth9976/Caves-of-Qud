using System;
using System.Collections.Generic;

namespace System.IO.Compression
{
	// Token: 0x02000037 RID: 55
	internal readonly struct ZipLocalFileHeader
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00008E80 File Offset: 0x00007080
		public static List<ZipGenericExtraField> GetExtraFields(BinaryReader reader)
		{
			reader.BaseStream.Seek(26L, SeekOrigin.Current);
			ushort num = reader.ReadUInt16();
			ushort num2 = reader.ReadUInt16();
			reader.BaseStream.Seek((long)((ulong)num), SeekOrigin.Current);
			List<ZipGenericExtraField> list;
			using (Stream stream = new SubReadStream(reader.BaseStream, reader.BaseStream.Position, (long)((ulong)num2)))
			{
				list = ZipGenericExtraField.ParseExtraField(stream);
			}
			Zip64ExtraField.RemoveZip64Blocks(list);
			return list;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008F00 File Offset: 0x00007100
		public static bool TrySkipBlock(BinaryReader reader)
		{
			if (reader.ReadUInt32() != 67324752U)
			{
				return false;
			}
			if (reader.BaseStream.Length < reader.BaseStream.Position + 22L)
			{
				return false;
			}
			reader.BaseStream.Seek(22L, SeekOrigin.Current);
			ushort num = reader.ReadUInt16();
			ushort num2 = reader.ReadUInt16();
			if (reader.BaseStream.Length < reader.BaseStream.Position + (long)((ulong)num) + (long)((ulong)num2))
			{
				return false;
			}
			reader.BaseStream.Seek((long)(num + num2), SeekOrigin.Current);
			return true;
		}

		// Token: 0x040001D1 RID: 465
		public const uint DataDescriptorSignature = 134695760U;

		// Token: 0x040001D2 RID: 466
		public const uint SignatureConstant = 67324752U;

		// Token: 0x040001D3 RID: 467
		public const int OffsetToCrcFromHeaderStart = 14;

		// Token: 0x040001D4 RID: 468
		public const int OffsetToBitFlagFromHeaderStart = 6;

		// Token: 0x040001D5 RID: 469
		public const int SizeOfLocalHeader = 30;
	}
}
