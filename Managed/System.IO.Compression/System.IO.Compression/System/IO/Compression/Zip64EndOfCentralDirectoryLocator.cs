using System;

namespace System.IO.Compression
{
	// Token: 0x02000035 RID: 53
	internal struct Zip64EndOfCentralDirectoryLocator
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00008D2B File Offset: 0x00006F2B
		public static bool TryReadBlock(BinaryReader reader, out Zip64EndOfCentralDirectoryLocator zip64EOCDLocator)
		{
			zip64EOCDLocator = default(Zip64EndOfCentralDirectoryLocator);
			if (reader.ReadUInt32() != 117853008U)
			{
				return false;
			}
			zip64EOCDLocator.NumberOfDiskWithZip64EOCD = reader.ReadUInt32();
			zip64EOCDLocator.OffsetOfZip64EOCD = reader.ReadUInt64();
			zip64EOCDLocator.TotalNumberOfDisks = reader.ReadUInt32();
			return true;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008D68 File Offset: 0x00006F68
		public static void WriteBlock(Stream stream, long zip64EOCDRecordStart)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(117853008U);
			binaryWriter.Write(0U);
			binaryWriter.Write(zip64EOCDRecordStart);
			binaryWriter.Write(1U);
		}

		// Token: 0x040001C1 RID: 449
		public const uint SignatureConstant = 117853008U;

		// Token: 0x040001C2 RID: 450
		public const int SizeOfBlockWithoutSignature = 16;

		// Token: 0x040001C3 RID: 451
		public uint NumberOfDiskWithZip64EOCD;

		// Token: 0x040001C4 RID: 452
		public ulong OffsetOfZip64EOCD;

		// Token: 0x040001C5 RID: 453
		public uint TotalNumberOfDisks;
	}
}
