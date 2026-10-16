using System;
using System.Collections.Generic;

namespace System.IO.Compression
{
	// Token: 0x02000034 RID: 52
	internal struct Zip64ExtraField
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00008788 File Offset: 0x00006988
		public ushort TotalSize
		{
			get
			{
				return this._size + 4;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00008793 File Offset: 0x00006993
		// (set) Token: 0x06000190 RID: 400 RVA: 0x0000879B File Offset: 0x0000699B
		public long? UncompressedSize
		{
			get
			{
				return this._uncompressedSize;
			}
			set
			{
				this._uncompressedSize = value;
				this.UpdateSize();
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000087AA File Offset: 0x000069AA
		// (set) Token: 0x06000192 RID: 402 RVA: 0x000087B2 File Offset: 0x000069B2
		public long? CompressedSize
		{
			get
			{
				return this._compressedSize;
			}
			set
			{
				this._compressedSize = value;
				this.UpdateSize();
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000087C1 File Offset: 0x000069C1
		// (set) Token: 0x06000194 RID: 404 RVA: 0x000087C9 File Offset: 0x000069C9
		public long? LocalHeaderOffset
		{
			get
			{
				return this._localHeaderOffset;
			}
			set
			{
				this._localHeaderOffset = value;
				this.UpdateSize();
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000087D8 File Offset: 0x000069D8
		public int? StartDiskNumber
		{
			get
			{
				return this._startDiskNumber;
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000087E0 File Offset: 0x000069E0
		private void UpdateSize()
		{
			this._size = 0;
			if (this._uncompressedSize != null)
			{
				this._size += 8;
			}
			if (this._compressedSize != null)
			{
				this._size += 8;
			}
			if (this._localHeaderOffset != null)
			{
				this._size += 8;
			}
			if (this._startDiskNumber != null)
			{
				this._size += 4;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00008864 File Offset: 0x00006A64
		public static Zip64ExtraField GetJustZip64Block(Stream extraFieldStream, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber)
		{
			Zip64ExtraField zip64ExtraField;
			using (BinaryReader binaryReader = new BinaryReader(extraFieldStream))
			{
				ZipGenericExtraField zipGenericExtraField;
				while (ZipGenericExtraField.TryReadBlock(binaryReader, extraFieldStream.Length, out zipGenericExtraField))
				{
					if (Zip64ExtraField.TryGetZip64BlockFromGenericExtraField(zipGenericExtraField, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber, out zip64ExtraField))
					{
						return zip64ExtraField;
					}
				}
			}
			zip64ExtraField = new Zip64ExtraField
			{
				_compressedSize = null,
				_uncompressedSize = null,
				_localHeaderOffset = null,
				_startDiskNumber = null
			};
			return zip64ExtraField;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000088F8 File Offset: 0x00006AF8
		private static bool TryGetZip64BlockFromGenericExtraField(ZipGenericExtraField extraField, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber, out Zip64ExtraField zip64Block)
		{
			zip64Block = default(Zip64ExtraField);
			zip64Block._compressedSize = null;
			zip64Block._uncompressedSize = null;
			zip64Block._localHeaderOffset = null;
			zip64Block._startDiskNumber = null;
			if (extraField.Tag != 1)
			{
				return false;
			}
			MemoryStream memoryStream = null;
			bool flag;
			try
			{
				memoryStream = new MemoryStream(extraField.Data);
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					memoryStream = null;
					zip64Block._size = extraField.Size;
					ushort num = 0;
					if (readUncompressedSize)
					{
						num += 8;
					}
					if (readCompressedSize)
					{
						num += 8;
					}
					if (readLocalHeaderOffset)
					{
						num += 8;
					}
					if (readStartDiskNumber)
					{
						num += 4;
					}
					if (num != zip64Block._size)
					{
						flag = false;
					}
					else
					{
						if (readUncompressedSize)
						{
							zip64Block._uncompressedSize = new long?(binaryReader.ReadInt64());
						}
						if (readCompressedSize)
						{
							zip64Block._compressedSize = new long?(binaryReader.ReadInt64());
						}
						if (readLocalHeaderOffset)
						{
							zip64Block._localHeaderOffset = new long?(binaryReader.ReadInt64());
						}
						if (readStartDiskNumber)
						{
							zip64Block._startDiskNumber = new int?(binaryReader.ReadInt32());
						}
						long? num2 = zip64Block._uncompressedSize;
						long num3 = 0L;
						if ((num2.GetValueOrDefault() < num3) & (num2 != null))
						{
							throw new InvalidDataException("Uncompressed Size cannot be held in an Int64.");
						}
						num2 = zip64Block._compressedSize;
						num3 = 0L;
						if ((num2.GetValueOrDefault() < num3) & (num2 != null))
						{
							throw new InvalidDataException("Compressed Size cannot be held in an Int64.");
						}
						num2 = zip64Block._localHeaderOffset;
						num3 = 0L;
						if ((num2.GetValueOrDefault() < num3) & (num2 != null))
						{
							throw new InvalidDataException("Local Header Offset cannot be held in an Int64.");
						}
						int? startDiskNumber = zip64Block._startDiskNumber;
						int num4 = 0;
						if ((startDiskNumber.GetValueOrDefault() < num4) & (startDiskNumber != null))
						{
							throw new InvalidDataException("Start Disk Number cannot be held in an Int64.");
						}
						flag = true;
					}
				}
			}
			finally
			{
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008AFC File Offset: 0x00006CFC
		public static Zip64ExtraField GetAndRemoveZip64Block(List<ZipGenericExtraField> extraFields, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber)
		{
			Zip64ExtraField zip64ExtraField = default(Zip64ExtraField);
			zip64ExtraField._compressedSize = null;
			zip64ExtraField._uncompressedSize = null;
			zip64ExtraField._localHeaderOffset = null;
			zip64ExtraField._startDiskNumber = null;
			List<ZipGenericExtraField> list = new List<ZipGenericExtraField>();
			bool flag = false;
			foreach (ZipGenericExtraField zipGenericExtraField in extraFields)
			{
				if (zipGenericExtraField.Tag == 1)
				{
					list.Add(zipGenericExtraField);
					if (!flag && Zip64ExtraField.TryGetZip64BlockFromGenericExtraField(zipGenericExtraField, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber, out zip64ExtraField))
					{
						flag = true;
					}
				}
			}
			foreach (ZipGenericExtraField zipGenericExtraField2 in list)
			{
				extraFields.Remove(zipGenericExtraField2);
			}
			return zip64ExtraField;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00008BF0 File Offset: 0x00006DF0
		public static void RemoveZip64Blocks(List<ZipGenericExtraField> extraFields)
		{
			List<ZipGenericExtraField> list = new List<ZipGenericExtraField>();
			foreach (ZipGenericExtraField zipGenericExtraField in extraFields)
			{
				if (zipGenericExtraField.Tag == 1)
				{
					list.Add(zipGenericExtraField);
				}
			}
			foreach (ZipGenericExtraField zipGenericExtraField2 in list)
			{
				extraFields.Remove(zipGenericExtraField2);
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008C8C File Offset: 0x00006E8C
		public void WriteBlock(Stream stream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(1);
			binaryWriter.Write(this._size);
			if (this._uncompressedSize != null)
			{
				binaryWriter.Write(this._uncompressedSize.Value);
			}
			if (this._compressedSize != null)
			{
				binaryWriter.Write(this._compressedSize.Value);
			}
			if (this._localHeaderOffset != null)
			{
				binaryWriter.Write(this._localHeaderOffset.Value);
			}
			if (this._startDiskNumber != null)
			{
				binaryWriter.Write(this._startDiskNumber.Value);
			}
		}

		// Token: 0x040001BA RID: 442
		public const int OffsetToFirstField = 4;

		// Token: 0x040001BB RID: 443
		private const ushort TagConstant = 1;

		// Token: 0x040001BC RID: 444
		private ushort _size;

		// Token: 0x040001BD RID: 445
		private long? _uncompressedSize;

		// Token: 0x040001BE RID: 446
		private long? _compressedSize;

		// Token: 0x040001BF RID: 447
		private long? _localHeaderOffset;

		// Token: 0x040001C0 RID: 448
		private int? _startDiskNumber;
	}
}
