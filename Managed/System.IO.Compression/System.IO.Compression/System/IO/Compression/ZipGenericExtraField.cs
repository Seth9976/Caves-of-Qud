using System;
using System.Collections.Generic;

namespace System.IO.Compression
{
	// Token: 0x02000033 RID: 51
	internal struct ZipGenericExtraField
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000085E0 File Offset: 0x000067E0
		public ushort Tag
		{
			get
			{
				return this._tag;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000085E8 File Offset: 0x000067E8
		public ushort Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000085F0 File Offset: 0x000067F0
		public byte[] Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000085F8 File Offset: 0x000067F8
		public void WriteBlock(Stream stream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(this.Tag);
			binaryWriter.Write(this.Size);
			binaryWriter.Write(this.Data);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00008624 File Offset: 0x00006824
		public static bool TryReadBlock(BinaryReader reader, long endExtraField, out ZipGenericExtraField field)
		{
			field = default(ZipGenericExtraField);
			if (endExtraField - reader.BaseStream.Position < 4L)
			{
				return false;
			}
			field._tag = reader.ReadUInt16();
			field._size = reader.ReadUInt16();
			if (endExtraField - reader.BaseStream.Position < (long)((ulong)field._size))
			{
				return false;
			}
			field._data = reader.ReadBytes((int)field._size);
			return true;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008690 File Offset: 0x00006890
		public static List<ZipGenericExtraField> ParseExtraField(Stream extraFieldData)
		{
			List<ZipGenericExtraField> list = new List<ZipGenericExtraField>();
			using (BinaryReader binaryReader = new BinaryReader(extraFieldData))
			{
				ZipGenericExtraField zipGenericExtraField;
				while (ZipGenericExtraField.TryReadBlock(binaryReader, extraFieldData.Length, out zipGenericExtraField))
				{
					list.Add(zipGenericExtraField);
				}
			}
			return list;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000086E0 File Offset: 0x000068E0
		public static int TotalSize(List<ZipGenericExtraField> fields)
		{
			int num = 0;
			foreach (ZipGenericExtraField zipGenericExtraField in fields)
			{
				num += (int)(zipGenericExtraField.Size + 4);
			}
			return num;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008738 File Offset: 0x00006938
		public static void WriteAllBlocks(List<ZipGenericExtraField> fields, Stream stream)
		{
			foreach (ZipGenericExtraField zipGenericExtraField in fields)
			{
				zipGenericExtraField.WriteBlock(stream);
			}
		}

		// Token: 0x040001B6 RID: 438
		private const int SizeOfHeader = 4;

		// Token: 0x040001B7 RID: 439
		private ushort _tag;

		// Token: 0x040001B8 RID: 440
		private ushort _size;

		// Token: 0x040001B9 RID: 441
		private byte[] _data;
	}
}
