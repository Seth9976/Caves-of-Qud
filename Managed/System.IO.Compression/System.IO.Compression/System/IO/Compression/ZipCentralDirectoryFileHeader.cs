using System;
using System.Collections.Generic;

namespace System.IO.Compression
{
	// Token: 0x02000038 RID: 56
	internal struct ZipCentralDirectoryFileHeader
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x00008F8C File Offset: 0x0000718C
		public static bool TryReadBlock(BinaryReader reader, bool saveExtraFieldsAndComments, out ZipCentralDirectoryFileHeader header)
		{
			header = default(ZipCentralDirectoryFileHeader);
			if (reader.ReadUInt32() != 33639248U)
			{
				return false;
			}
			header.VersionMadeBySpecification = reader.ReadByte();
			header.VersionMadeByCompatibility = reader.ReadByte();
			header.VersionNeededToExtract = reader.ReadUInt16();
			header.GeneralPurposeBitFlag = reader.ReadUInt16();
			header.CompressionMethod = reader.ReadUInt16();
			header.LastModified = reader.ReadUInt32();
			header.Crc32 = reader.ReadUInt32();
			uint num = reader.ReadUInt32();
			uint num2 = reader.ReadUInt32();
			header.FilenameLength = reader.ReadUInt16();
			header.ExtraFieldLength = reader.ReadUInt16();
			header.FileCommentLength = reader.ReadUInt16();
			ushort num3 = reader.ReadUInt16();
			header.InternalFileAttributes = reader.ReadUInt16();
			header.ExternalFileAttributes = reader.ReadUInt32();
			uint num4 = reader.ReadUInt32();
			header.Filename = reader.ReadBytes((int)header.FilenameLength);
			bool flag = num2 == uint.MaxValue;
			bool flag2 = num == uint.MaxValue;
			bool flag3 = num4 == uint.MaxValue;
			bool flag4 = num3 == ushort.MaxValue;
			long num5 = reader.BaseStream.Position + (long)((ulong)header.ExtraFieldLength);
			Zip64ExtraField zip64ExtraField;
			using (Stream stream = new SubReadStream(reader.BaseStream, reader.BaseStream.Position, (long)((ulong)header.ExtraFieldLength)))
			{
				if (saveExtraFieldsAndComments)
				{
					header.ExtraFields = ZipGenericExtraField.ParseExtraField(stream);
					zip64ExtraField = Zip64ExtraField.GetAndRemoveZip64Block(header.ExtraFields, flag, flag2, flag3, flag4);
				}
				else
				{
					header.ExtraFields = null;
					zip64ExtraField = Zip64ExtraField.GetJustZip64Block(stream, flag, flag2, flag3, flag4);
				}
			}
			reader.BaseStream.AdvanceToPosition(num5);
			if (saveExtraFieldsAndComments)
			{
				header.FileComment = reader.ReadBytes((int)header.FileCommentLength);
			}
			else
			{
				reader.BaseStream.Position += (long)((ulong)header.FileCommentLength);
				header.FileComment = null;
			}
			header.UncompressedSize = (long)((zip64ExtraField.UncompressedSize == null) ? ((ulong)num2) : ((ulong)zip64ExtraField.UncompressedSize.Value));
			header.CompressedSize = (long)((zip64ExtraField.CompressedSize == null) ? ((ulong)num) : ((ulong)zip64ExtraField.CompressedSize.Value));
			header.RelativeOffsetOfLocalHeader = (long)((zip64ExtraField.LocalHeaderOffset == null) ? ((ulong)num4) : ((ulong)zip64ExtraField.LocalHeaderOffset.Value));
			header.DiskNumberStart = ((zip64ExtraField.StartDiskNumber == null) ? ((int)num3) : zip64ExtraField.StartDiskNumber.Value);
			return true;
		}

		// Token: 0x040001D6 RID: 470
		public const uint SignatureConstant = 33639248U;

		// Token: 0x040001D7 RID: 471
		public byte VersionMadeByCompatibility;

		// Token: 0x040001D8 RID: 472
		public byte VersionMadeBySpecification;

		// Token: 0x040001D9 RID: 473
		public ushort VersionNeededToExtract;

		// Token: 0x040001DA RID: 474
		public ushort GeneralPurposeBitFlag;

		// Token: 0x040001DB RID: 475
		public ushort CompressionMethod;

		// Token: 0x040001DC RID: 476
		public uint LastModified;

		// Token: 0x040001DD RID: 477
		public uint Crc32;

		// Token: 0x040001DE RID: 478
		public long CompressedSize;

		// Token: 0x040001DF RID: 479
		public long UncompressedSize;

		// Token: 0x040001E0 RID: 480
		public ushort FilenameLength;

		// Token: 0x040001E1 RID: 481
		public ushort ExtraFieldLength;

		// Token: 0x040001E2 RID: 482
		public ushort FileCommentLength;

		// Token: 0x040001E3 RID: 483
		public int DiskNumberStart;

		// Token: 0x040001E4 RID: 484
		public ushort InternalFileAttributes;

		// Token: 0x040001E5 RID: 485
		public uint ExternalFileAttributes;

		// Token: 0x040001E6 RID: 486
		public long RelativeOffsetOfLocalHeader;

		// Token: 0x040001E7 RID: 487
		public byte[] Filename;

		// Token: 0x040001E8 RID: 488
		public byte[] FileComment;

		// Token: 0x040001E9 RID: 489
		public List<ZipGenericExtraField> ExtraFields;
	}
}
