using System;

namespace System.IO.Compression
{
	// Token: 0x02000039 RID: 57
	internal struct ZipEndOfCentralDirectoryBlock
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00009210 File Offset: 0x00007410
		public static void WriteBlock(Stream stream, long numberOfEntries, long startOfCentralDirectory, long sizeOfCentralDirectory, byte[] archiveComment)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			ushort num = ((numberOfEntries > 65535L) ? ushort.MaxValue : ((ushort)numberOfEntries));
			uint num2 = ((startOfCentralDirectory > (long)((ulong)(-1))) ? uint.MaxValue : ((uint)startOfCentralDirectory));
			uint num3 = ((sizeOfCentralDirectory > (long)((ulong)(-1))) ? uint.MaxValue : ((uint)sizeOfCentralDirectory));
			binaryWriter.Write(101010256U);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(num);
			binaryWriter.Write(num);
			binaryWriter.Write(num3);
			binaryWriter.Write(num2);
			binaryWriter.Write((archiveComment != null) ? ((ushort)archiveComment.Length) : 0);
			if (archiveComment != null)
			{
				binaryWriter.Write(archiveComment);
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000092A0 File Offset: 0x000074A0
		public static bool TryReadBlock(BinaryReader reader, out ZipEndOfCentralDirectoryBlock eocdBlock)
		{
			eocdBlock = default(ZipEndOfCentralDirectoryBlock);
			if (reader.ReadUInt32() != 101010256U)
			{
				return false;
			}
			eocdBlock.Signature = 101010256U;
			eocdBlock.NumberOfThisDisk = reader.ReadUInt16();
			eocdBlock.NumberOfTheDiskWithTheStartOfTheCentralDirectory = reader.ReadUInt16();
			eocdBlock.NumberOfEntriesInTheCentralDirectoryOnThisDisk = reader.ReadUInt16();
			eocdBlock.NumberOfEntriesInTheCentralDirectory = reader.ReadUInt16();
			eocdBlock.SizeOfCentralDirectory = reader.ReadUInt32();
			eocdBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber = reader.ReadUInt32();
			ushort num = reader.ReadUInt16();
			eocdBlock.ArchiveComment = reader.ReadBytes((int)num);
			return true;
		}

		// Token: 0x040001EA RID: 490
		public const uint SignatureConstant = 101010256U;

		// Token: 0x040001EB RID: 491
		public const int SizeOfBlockWithoutSignature = 18;

		// Token: 0x040001EC RID: 492
		public uint Signature;

		// Token: 0x040001ED RID: 493
		public ushort NumberOfThisDisk;

		// Token: 0x040001EE RID: 494
		public ushort NumberOfTheDiskWithTheStartOfTheCentralDirectory;

		// Token: 0x040001EF RID: 495
		public ushort NumberOfEntriesInTheCentralDirectoryOnThisDisk;

		// Token: 0x040001F0 RID: 496
		public ushort NumberOfEntriesInTheCentralDirectory;

		// Token: 0x040001F1 RID: 497
		public uint SizeOfCentralDirectory;

		// Token: 0x040001F2 RID: 498
		public uint OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber;

		// Token: 0x040001F3 RID: 499
		public byte[] ArchiveComment;
	}
}
