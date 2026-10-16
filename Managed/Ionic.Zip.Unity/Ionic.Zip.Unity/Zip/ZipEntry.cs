using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Ionic.BZip2;
using Ionic.Crc;
using Ionic.Zlib;

namespace Ionic.Zip
{
	// Token: 0x0200002C RID: 44
	[ClassInterface(1)]
	[ComVisible(true)]
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00004")]
	public class ZipEntry
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000053EF File Offset: 0x000035EF
		internal bool AttributesIndicateDirectory
		{
			get
			{
				return this._InternalFileAttrs == 0 && (this._ExternalFileAttrs & 16) == 16;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005408 File Offset: 0x00003608
		internal void ResetDirEntry()
		{
			this.__FileDataPosition = -1L;
			this._LengthOfHeader = 0;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000541C File Offset: 0x0000361C
		public string Info
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(string.Format("          ZipEntry: {0}\n", this.FileName)).Append(string.Format("   Version Made By: {0}\n", this._VersionMadeBy)).Append(string.Format(" Needed to extract: {0}\n", this.VersionNeeded));
				if (this._IsDirectory)
				{
					stringBuilder.Append("        Entry type: directory\n");
				}
				else
				{
					stringBuilder.Append(string.Format("         File type: {0}\n", this._IsText ? "text" : "binary")).Append(string.Format("       Compression: {0}\n", this.CompressionMethod)).Append(string.Format("        Compressed: 0x{0:X}\n", this.CompressedSize))
						.Append(string.Format("      Uncompressed: 0x{0:X}\n", this.UncompressedSize))
						.Append(string.Format("             CRC32: 0x{0:X8}\n", this._Crc32));
				}
				stringBuilder.Append(string.Format("       Disk Number: {0}\n", this._diskNumber));
				if (this._RelativeOffsetOfLocalHeader > (long)((ulong)(-1)))
				{
					stringBuilder.Append(string.Format("   Relative Offset: 0x{0:X16}\n", this._RelativeOffsetOfLocalHeader));
				}
				else
				{
					stringBuilder.Append(string.Format("   Relative Offset: 0x{0:X8}\n", this._RelativeOffsetOfLocalHeader));
				}
				stringBuilder.Append(string.Format("         Bit Field: 0x{0:X4}\n", this._BitField)).Append(string.Format("        Encrypted?: {0}\n", this._sourceIsEncrypted)).Append(string.Format("          Timeblob: 0x{0:X8}\n", this._TimeBlob))
					.Append(string.Format("              Time: {0}\n", SharedUtilities.PackedToDateTime(this._TimeBlob)));
				stringBuilder.Append(string.Format("         Is Zip64?: {0}\n", this._InputUsesZip64));
				if (!string.IsNullOrEmpty(this._Comment))
				{
					stringBuilder.Append(string.Format("           Comment: {0}\n", this._Comment));
				}
				stringBuilder.Append("\n");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005644 File Offset: 0x00003844
		internal static ZipEntry ReadDirEntry(ZipFile zf, Dictionary<string, object> previouslySeen)
		{
			Stream readStream = zf.ReadStream;
			Encoding encoding = ((zf.AlternateEncodingUsage == ZipOption.Always) ? zf.AlternateEncoding : ZipFile.DefaultEncoding);
			int num = SharedUtilities.ReadSignature(readStream);
			if (ZipEntry.IsNotValidZipDirEntrySig(num))
			{
				readStream.Seek(-4L, 1);
				if ((long)num != 101010256L && (long)num != 101075792L && num != 67324752)
				{
					throw new BadReadException(string.Format("  Bad signature (0x{0:X8}) at position 0x{1:X8}", num, readStream.Position));
				}
				return null;
			}
			else
			{
				int num2 = 46;
				byte[] array = new byte[42];
				int num3 = readStream.Read(array, 0, array.Length);
				if (num3 != array.Length)
				{
					return null;
				}
				int num4 = 0;
				ZipEntry zipEntry = new ZipEntry();
				zipEntry.AlternateEncoding = encoding;
				zipEntry._Source = ZipEntrySource.ZipFile;
				zipEntry._container = new ZipContainer(zf);
				zipEntry._VersionMadeBy = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._VersionNeeded = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._BitField = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._CompressionMethod = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._TimeBlob = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
				zipEntry._LastModified = SharedUtilities.PackedToDateTime(zipEntry._TimeBlob);
				zipEntry._timestamp |= ZipEntryTimestamp.DOS;
				zipEntry._Crc32 = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
				zipEntry._CompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
				zipEntry._UncompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
				zipEntry._CompressionMethod_FromZipFile = zipEntry._CompressionMethod;
				zipEntry._filenameLength = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._extraFieldLength = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._commentLength = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._diskNumber = (uint)array[num4++] + (uint)array[num4++] * 256U;
				zipEntry._InternalFileAttrs = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._ExternalFileAttrs = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
				zipEntry._RelativeOffsetOfLocalHeader = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
				zipEntry.IsText = (zipEntry._InternalFileAttrs & 1) == 1;
				array = new byte[(int)zipEntry._filenameLength];
				num3 = readStream.Read(array, 0, array.Length);
				num2 += num3;
				if ((zipEntry._BitField & 2048) == 2048)
				{
					zipEntry._FileNameInArchive = SharedUtilities.Utf8StringFromBuffer(array);
				}
				else
				{
					zipEntry._FileNameInArchive = SharedUtilities.StringFromBuffer(array, encoding);
				}
				while (previouslySeen.ContainsKey(zipEntry._FileNameInArchive))
				{
					zipEntry._FileNameInArchive = ZipEntry.CopyHelper.AppendCopyToFileName(zipEntry._FileNameInArchive);
					zipEntry._metadataChanged = true;
				}
				if (zipEntry.AttributesIndicateDirectory)
				{
					zipEntry.MarkAsDirectory();
				}
				else if (zipEntry._FileNameInArchive.EndsWith("/"))
				{
					zipEntry.MarkAsDirectory();
				}
				zipEntry._CompressedFileDataSize = zipEntry._CompressedSize;
				if ((zipEntry._BitField & 1) == 1)
				{
					zipEntry._Encryption_FromZipFile = (zipEntry._Encryption = EncryptionAlgorithm.PkzipWeak);
					zipEntry._sourceIsEncrypted = true;
				}
				if (zipEntry._extraFieldLength > 0)
				{
					zipEntry._InputUsesZip64 = zipEntry._CompressedSize == (long)((ulong)(-1)) || zipEntry._UncompressedSize == (long)((ulong)(-1)) || zipEntry._RelativeOffsetOfLocalHeader == (long)((ulong)(-1));
					num2 += zipEntry.ProcessExtraField(readStream, zipEntry._extraFieldLength);
					zipEntry._CompressedFileDataSize = zipEntry._CompressedSize;
				}
				if (zipEntry._Encryption == EncryptionAlgorithm.PkzipWeak)
				{
					zipEntry._CompressedFileDataSize -= 12L;
				}
				else if (zipEntry.Encryption == EncryptionAlgorithm.WinZipAes128 || zipEntry.Encryption == EncryptionAlgorithm.WinZipAes256)
				{
					zipEntry._CompressedFileDataSize = zipEntry.CompressedSize - (long)(ZipEntry.GetLengthOfCryptoHeaderBytes(zipEntry.Encryption) + 10);
					zipEntry._LengthOfTrailer = 10;
				}
				if ((zipEntry._BitField & 8) == 8)
				{
					if (zipEntry._InputUsesZip64)
					{
						zipEntry._LengthOfTrailer += 24;
					}
					else
					{
						zipEntry._LengthOfTrailer += 16;
					}
				}
				zipEntry.AlternateEncoding = (((zipEntry._BitField & 2048) == 2048) ? Encoding.UTF8 : encoding);
				zipEntry.AlternateEncodingUsage = ZipOption.Always;
				if (zipEntry._commentLength > 0)
				{
					array = new byte[(int)zipEntry._commentLength];
					num3 = readStream.Read(array, 0, array.Length);
					num2 += num3;
					if ((zipEntry._BitField & 2048) == 2048)
					{
						zipEntry._Comment = SharedUtilities.Utf8StringFromBuffer(array);
					}
					else
					{
						zipEntry._Comment = SharedUtilities.StringFromBuffer(array, encoding);
					}
				}
				return zipEntry;
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005CEC File Offset: 0x00003EEC
		internal static bool IsNotValidZipDirEntrySig(int signature)
		{
			return signature != 33639248;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005CFC File Offset: 0x00003EFC
		public ZipEntry()
		{
			this._CompressionMethod = 8;
			this._CompressionLevel = CompressionLevel.Default;
			this._Encryption = EncryptionAlgorithm.None;
			this._Source = ZipEntrySource.None;
			this.AlternateEncoding = Encoding.UTF8;
			this.AlternateEncodingUsage = ZipOption.Default;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00005D5E File Offset: 0x00003F5E
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00005D6C File Offset: 0x00003F6C
		public DateTime LastModified
		{
			get
			{
				return this._LastModified.ToLocalTime();
			}
			set
			{
				this._LastModified = ((value.Kind == null) ? DateTime.SpecifyKind(value, 2) : value.ToLocalTime());
				this._Mtime = SharedUtilities.AdjustTime_Reverse(this._LastModified).ToUniversalTime();
				this._metadataChanged = true;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005DB8 File Offset: 0x00003FB8
		private int BufferSize
		{
			get
			{
				return this._container.BufferSize;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005DC5 File Offset: 0x00003FC5
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00005DCD File Offset: 0x00003FCD
		public DateTime ModifiedTime
		{
			get
			{
				return this._Mtime;
			}
			set
			{
				this.SetEntryTimes(this._Ctime, this._Atime, value);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00005DE2 File Offset: 0x00003FE2
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00005DEA File Offset: 0x00003FEA
		public DateTime AccessedTime
		{
			get
			{
				return this._Atime;
			}
			set
			{
				this.SetEntryTimes(this._Ctime, value, this._Mtime);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00005DFF File Offset: 0x00003FFF
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00005E07 File Offset: 0x00004007
		public DateTime CreationTime
		{
			get
			{
				return this._Ctime;
			}
			set
			{
				this.SetEntryTimes(value, this._Atime, this._Mtime);
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005E1C File Offset: 0x0000401C
		public void SetEntryTimes(DateTime created, DateTime accessed, DateTime modified)
		{
			this._ntfsTimesAreSet = true;
			if (created == ZipEntry._zeroHour && created.Kind == ZipEntry._zeroHour.Kind)
			{
				created = ZipEntry._win32Epoch;
			}
			if (accessed == ZipEntry._zeroHour && accessed.Kind == ZipEntry._zeroHour.Kind)
			{
				accessed = ZipEntry._win32Epoch;
			}
			if (modified == ZipEntry._zeroHour && modified.Kind == ZipEntry._zeroHour.Kind)
			{
				modified = ZipEntry._win32Epoch;
			}
			this._Ctime = created.ToUniversalTime();
			this._Atime = accessed.ToUniversalTime();
			this._Mtime = modified.ToUniversalTime();
			this._LastModified = this._Mtime;
			if (!this._emitUnixTimes && !this._emitNtfsTimes)
			{
				this._emitNtfsTimes = true;
			}
			this._metadataChanged = true;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005EF6 File Offset: 0x000040F6
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00005EFE File Offset: 0x000040FE
		public bool EmitTimesInWindowsFormatWhenSaving
		{
			get
			{
				return this._emitNtfsTimes;
			}
			set
			{
				this._emitNtfsTimes = value;
				this._metadataChanged = true;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005F0E File Offset: 0x0000410E
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00005F16 File Offset: 0x00004116
		public bool EmitTimesInUnixFormatWhenSaving
		{
			get
			{
				return this._emitUnixTimes;
			}
			set
			{
				this._emitUnixTimes = value;
				this._metadataChanged = true;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005F26 File Offset: 0x00004126
		public ZipEntryTimestamp Timestamp
		{
			get
			{
				return this._timestamp;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005F2E File Offset: 0x0000412E
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00005F36 File Offset: 0x00004136
		public FileAttributes Attributes
		{
			get
			{
				return this._ExternalFileAttrs;
			}
			set
			{
				this._ExternalFileAttrs = value;
				this._VersionMadeBy = 45;
				this._metadataChanged = true;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005F4E File Offset: 0x0000414E
		internal string LocalFileName
		{
			get
			{
				return this._LocalFileName;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00005F56 File Offset: 0x00004156
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00005F60 File Offset: 0x00004160
		public string FileName
		{
			get
			{
				return this._FileNameInArchive;
			}
			set
			{
				if (this._container.ZipFile == null)
				{
					throw new ZipException("Cannot rename; this is not supported in ZipOutputStream/ZipInputStream.");
				}
				if (string.IsNullOrEmpty(value))
				{
					throw new ZipException("The FileName must be non empty and non-null.");
				}
				string text = ZipEntry.NameInArchive(value, null);
				if (this._FileNameInArchive == text)
				{
					return;
				}
				this._container.ZipFile.RemoveEntry(this);
				this._container.ZipFile.InternalAddEntry(text, this);
				this._FileNameInArchive = text;
				this._container.ZipFile.NotifyEntryChanged();
				this._metadataChanged = true;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005FF0 File Offset: 0x000041F0
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00005FF8 File Offset: 0x000041F8
		public Stream InputStream
		{
			get
			{
				return this._sourceStream;
			}
			set
			{
				if (this._Source != ZipEntrySource.Stream)
				{
					throw new ZipException("You must not set the input stream for this entry.");
				}
				this._sourceWasJitProvided = true;
				this._sourceStream = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000601C File Offset: 0x0000421C
		public bool InputStreamWasJitProvided
		{
			get
			{
				return this._sourceWasJitProvided;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00006024 File Offset: 0x00004224
		public ZipEntrySource Source
		{
			get
			{
				return this._Source;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000602C File Offset: 0x0000422C
		public short VersionNeeded
		{
			get
			{
				return this._VersionNeeded;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00006034 File Offset: 0x00004234
		// (set) Token: 0x0600011F RID: 287 RVA: 0x0000603C File Offset: 0x0000423C
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				this._Comment = value;
				this._metadataChanged = true;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000604C File Offset: 0x0000424C
		public bool? RequiresZip64
		{
			get
			{
				return this._entryRequiresZip64;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006054 File Offset: 0x00004254
		public bool? OutputUsedZip64
		{
			get
			{
				return this._OutputUsesZip64;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000605C File Offset: 0x0000425C
		public short BitField
		{
			get
			{
				return this._BitField;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00006064 File Offset: 0x00004264
		// (set) Token: 0x06000124 RID: 292 RVA: 0x0000606C File Offset: 0x0000426C
		public CompressionMethod CompressionMethod
		{
			get
			{
				return (CompressionMethod)this._CompressionMethod;
			}
			set
			{
				if (value == (CompressionMethod)this._CompressionMethod)
				{
					return;
				}
				if (value != CompressionMethod.None && value != CompressionMethod.Deflate && value != CompressionMethod.BZip2)
				{
					throw new InvalidOperationException("Unsupported compression method.");
				}
				this._CompressionMethod = (short)value;
				if (this._CompressionMethod == 0)
				{
					this._CompressionLevel = CompressionLevel.None;
				}
				else if (this.CompressionLevel == CompressionLevel.None)
				{
					this._CompressionLevel = CompressionLevel.Default;
				}
				if (this._container.ZipFile != null)
				{
					this._container.ZipFile.NotifyEntryChanged();
				}
				this._restreamRequiredOnSave = true;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000060E6 File Offset: 0x000042E6
		// (set) Token: 0x06000126 RID: 294 RVA: 0x000060F0 File Offset: 0x000042F0
		public CompressionLevel CompressionLevel
		{
			get
			{
				return this._CompressionLevel;
			}
			set
			{
				if (this._CompressionMethod != 8 && this._CompressionMethod != 0)
				{
					return;
				}
				if (value == CompressionLevel.Default && this._CompressionMethod == 8)
				{
					return;
				}
				this._CompressionLevel = value;
				if (value == CompressionLevel.None && this._CompressionMethod == 0)
				{
					return;
				}
				if (this._CompressionLevel == CompressionLevel.None)
				{
					this._CompressionMethod = 0;
				}
				else
				{
					this._CompressionMethod = 8;
				}
				if (this._container.ZipFile != null)
				{
					this._container.ZipFile.NotifyEntryChanged();
				}
				this._restreamRequiredOnSave = true;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000616C File Offset: 0x0000436C
		public long CompressedSize
		{
			get
			{
				return this._CompressedSize;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00006174 File Offset: 0x00004374
		public long UncompressedSize
		{
			get
			{
				return this._UncompressedSize;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000617C File Offset: 0x0000437C
		public double CompressionRatio
		{
			get
			{
				if (this.UncompressedSize == 0L)
				{
					return 0.0;
				}
				return 100.0 * (1.0 - 1.0 * (double)this.CompressedSize / (1.0 * (double)this.UncompressedSize));
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000061D4 File Offset: 0x000043D4
		public int Crc
		{
			get
			{
				return this._Crc32;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000061DC File Offset: 0x000043DC
		public bool IsDirectory
		{
			get
			{
				return this._IsDirectory;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000061E4 File Offset: 0x000043E4
		public bool UsesEncryption
		{
			get
			{
				return this._Encryption_FromZipFile != EncryptionAlgorithm.None;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000061F2 File Offset: 0x000043F2
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000061FC File Offset: 0x000043FC
		public EncryptionAlgorithm Encryption
		{
			get
			{
				return this._Encryption;
			}
			set
			{
				if (value == this._Encryption)
				{
					return;
				}
				if (value == EncryptionAlgorithm.Unsupported)
				{
					throw new InvalidOperationException("You may not set Encryption to that value.");
				}
				this._Encryption = value;
				this._restreamRequiredOnSave = true;
				if (this._container.ZipFile != null)
				{
					this._container.ZipFile.NotifyEntryChanged();
				}
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000628D File Offset: 0x0000448D
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000624D File Offset: 0x0000444D
		public string Password
		{
			private get
			{
				return this._Password;
			}
			set
			{
				this._Password = value;
				if (this._Password == null)
				{
					this._Encryption = EncryptionAlgorithm.None;
					return;
				}
				if (this._Source == ZipEntrySource.ZipFile && !this._sourceIsEncrypted)
				{
					this._restreamRequiredOnSave = true;
				}
				if (this.Encryption == EncryptionAlgorithm.None)
				{
					this._Encryption = EncryptionAlgorithm.PkzipWeak;
				}
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006295 File Offset: 0x00004495
		internal bool IsChanged
		{
			get
			{
				return this._restreamRequiredOnSave | this._metadataChanged;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000062A4 File Offset: 0x000044A4
		// (set) Token: 0x06000133 RID: 307 RVA: 0x000062AC File Offset: 0x000044AC
		public ExtractExistingFileAction ExtractExistingFile { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000062B5 File Offset: 0x000044B5
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000062BD File Offset: 0x000044BD
		public ZipErrorAction ZipErrorAction { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000062C6 File Offset: 0x000044C6
		public bool IncludedInMostRecentSave
		{
			get
			{
				return !this._skippedDuringSave;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000062D1 File Offset: 0x000044D1
		// (set) Token: 0x06000138 RID: 312 RVA: 0x000062D9 File Offset: 0x000044D9
		public SetCompressionCallback SetCompression { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000062E2 File Offset: 0x000044E2
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00006301 File Offset: 0x00004501
		[Obsolete("Beginning with v1.9.1.6 of DotNetZip, this property is obsolete.  It will be removed in a future version of the library. Your applications should  use AlternateEncoding and AlternateEncodingUsage instead.")]
		public bool UseUnicodeAsNecessary
		{
			get
			{
				return this.AlternateEncoding == Encoding.GetEncoding("UTF-8") && this.AlternateEncodingUsage == ZipOption.AsNecessary;
			}
			set
			{
				if (value)
				{
					this.AlternateEncoding = Encoding.GetEncoding("UTF-8");
					this.AlternateEncodingUsage = ZipOption.AsNecessary;
					return;
				}
				this.AlternateEncoding = ZipFile.DefaultEncoding;
				this.AlternateEncodingUsage = ZipOption.Default;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00006330 File Offset: 0x00004530
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00006338 File Offset: 0x00004538
		[Obsolete("This property is obsolete since v1.9.1.6. Use AlternateEncoding and AlternateEncodingUsage instead.", true)]
		public Encoding ProvisionalAlternateEncoding { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00006341 File Offset: 0x00004541
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00006349 File Offset: 0x00004549
		public Encoding AlternateEncoding { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00006352 File Offset: 0x00004552
		// (set) Token: 0x06000140 RID: 320 RVA: 0x0000635A File Offset: 0x0000455A
		public ZipOption AlternateEncodingUsage { get; set; }

		// Token: 0x06000141 RID: 321 RVA: 0x00006364 File Offset: 0x00004564
		internal static string NameInArchive(string filename, string directoryPathInArchive)
		{
			string text;
			if (directoryPathInArchive == null)
			{
				text = filename;
			}
			else if (string.IsNullOrEmpty(directoryPathInArchive))
			{
				text = Path.GetFileName(filename);
			}
			else
			{
				text = Path.Combine(directoryPathInArchive, Path.GetFileName(filename));
			}
			return SharedUtilities.NormalizePathForUseInZipFile(text);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000063A0 File Offset: 0x000045A0
		internal static ZipEntry CreateFromNothing(string nameInArchive)
		{
			return ZipEntry.Create(nameInArchive, ZipEntrySource.None, null, null);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000063AB File Offset: 0x000045AB
		internal static ZipEntry CreateFromFile(string filename, string nameInArchive)
		{
			return ZipEntry.Create(nameInArchive, ZipEntrySource.FileSystem, filename, null);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000063B6 File Offset: 0x000045B6
		internal static ZipEntry CreateForStream(string entryName, Stream s)
		{
			return ZipEntry.Create(entryName, ZipEntrySource.Stream, s, null);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000063C1 File Offset: 0x000045C1
		internal static ZipEntry CreateForWriter(string entryName, WriteDelegate d)
		{
			return ZipEntry.Create(entryName, ZipEntrySource.WriteDelegate, d, null);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000063CC File Offset: 0x000045CC
		internal static ZipEntry CreateForJitStreamProvider(string nameInArchive, OpenDelegate opener, CloseDelegate closer)
		{
			return ZipEntry.Create(nameInArchive, ZipEntrySource.JitStream, opener, closer);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000063D7 File Offset: 0x000045D7
		internal static ZipEntry CreateForZipOutputStream(string nameInArchive)
		{
			return ZipEntry.Create(nameInArchive, ZipEntrySource.ZipOutputStream, null, null);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000063E4 File Offset: 0x000045E4
		private static ZipEntry Create(string nameInArchive, ZipEntrySource source, object arg1, object arg2)
		{
			if (string.IsNullOrEmpty(nameInArchive))
			{
				throw new ZipException("The entry name must be non-null and non-empty.");
			}
			ZipEntry zipEntry = new ZipEntry();
			zipEntry._VersionMadeBy = 45;
			zipEntry._Source = source;
			zipEntry._Mtime = (zipEntry._Atime = (zipEntry._Ctime = DateTime.UtcNow));
			if (source == ZipEntrySource.Stream)
			{
				zipEntry._sourceStream = arg1 as Stream;
			}
			else if (source == ZipEntrySource.WriteDelegate)
			{
				zipEntry._WriteDelegate = arg1 as WriteDelegate;
			}
			else if (source == ZipEntrySource.JitStream)
			{
				zipEntry._OpenDelegate = arg1 as OpenDelegate;
				zipEntry._CloseDelegate = arg2 as CloseDelegate;
			}
			else if (source != ZipEntrySource.ZipOutputStream)
			{
				if (source == ZipEntrySource.None)
				{
					zipEntry._Source = ZipEntrySource.FileSystem;
				}
				else
				{
					string text = arg1 as string;
					if (string.IsNullOrEmpty(text))
					{
						throw new ZipException("The filename must be non-null and non-empty.");
					}
					try
					{
						zipEntry._Mtime = (zipEntry._Ctime = (zipEntry._Atime = DateTime.UtcNow));
						zipEntry._ExternalFileAttrs = 0;
						zipEntry._ntfsTimesAreSet = true;
						zipEntry._LocalFileName = Path.GetFullPath(text);
					}
					catch (PathTooLongException ex)
					{
						string text2 = string.Format("The path is too long, filename={0}", text);
						throw new ZipException(text2, ex);
					}
				}
			}
			zipEntry._LastModified = zipEntry._Mtime;
			zipEntry._FileNameInArchive = SharedUtilities.NormalizePathForUseInZipFile(nameInArchive);
			return zipEntry;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006528 File Offset: 0x00004728
		internal void MarkAsDirectory()
		{
			this._IsDirectory = true;
			if (!this._FileNameInArchive.EndsWith("/"))
			{
				this._FileNameInArchive += "/";
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00006559 File Offset: 0x00004759
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00006561 File Offset: 0x00004761
		public bool IsText
		{
			get
			{
				return this._IsText;
			}
			set
			{
				this._IsText = value;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000656A File Offset: 0x0000476A
		public override string ToString()
		{
			return string.Format("ZipEntry::{0}", this.FileName);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000657C File Offset: 0x0000477C
		internal Stream ArchiveStream
		{
			get
			{
				if (this._archiveStream == null)
				{
					if (this._container.ZipFile != null)
					{
						ZipFile zipFile = this._container.ZipFile;
						zipFile.Reset(false);
						this._archiveStream = zipFile.StreamForDiskNumber(this._diskNumber);
					}
					else
					{
						this._archiveStream = this._container.ZipOutputStream.OutputStream;
					}
				}
				return this._archiveStream;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000065E4 File Offset: 0x000047E4
		private void SetFdpLoh()
		{
			long position = this.ArchiveStream.Position;
			try
			{
				this.ArchiveStream.Seek(this._RelativeOffsetOfLocalHeader, 0);
			}
			catch (IOException ex)
			{
				string text = string.Format("Exception seeking  entry({0}) offset(0x{1:X8}) len(0x{2:X8})", this.FileName, this._RelativeOffsetOfLocalHeader, this.ArchiveStream.Length);
				throw new BadStateException(text, ex);
			}
			byte[] array = new byte[30];
			this.ArchiveStream.Read(array, 0, array.Length);
			short num = (short)((int)array[26] + (int)array[27] * 256);
			short num2 = (short)((int)array[28] + (int)array[29] * 256);
			this.ArchiveStream.Seek((long)(num + num2), 1);
			this._LengthOfHeader = (int)(30 + num2 + num) + ZipEntry.GetLengthOfCryptoHeaderBytes(this._Encryption_FromZipFile);
			this.__FileDataPosition = this._RelativeOffsetOfLocalHeader + (long)this._LengthOfHeader;
			this.ArchiveStream.Seek(position, 0);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000066E4 File Offset: 0x000048E4
		private static int GetKeyStrengthInBits(EncryptionAlgorithm a)
		{
			if (a == EncryptionAlgorithm.WinZipAes256)
			{
				return 256;
			}
			if (a == EncryptionAlgorithm.WinZipAes128)
			{
				return 128;
			}
			return -1;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000066FC File Offset: 0x000048FC
		internal static int GetLengthOfCryptoHeaderBytes(EncryptionAlgorithm a)
		{
			if (a == EncryptionAlgorithm.None)
			{
				return 0;
			}
			if (a == EncryptionAlgorithm.WinZipAes128 || a == EncryptionAlgorithm.WinZipAes256)
			{
				int keyStrengthInBits = ZipEntry.GetKeyStrengthInBits(a);
				return keyStrengthInBits / 8 / 2 + 2;
			}
			if (a == EncryptionAlgorithm.PkzipWeak)
			{
				return 12;
			}
			throw new ZipException("internal error");
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006738 File Offset: 0x00004938
		internal long FileDataPosition
		{
			get
			{
				if (this.__FileDataPosition == -1L)
				{
					this.SetFdpLoh();
				}
				return this.__FileDataPosition;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006750 File Offset: 0x00004950
		private int LengthOfHeader
		{
			get
			{
				if (this._LengthOfHeader == 0)
				{
					this.SetFdpLoh();
				}
				return this._LengthOfHeader;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006766 File Offset: 0x00004966
		public void Extract()
		{
			this.InternalExtract(".", null, null);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006775 File Offset: 0x00004975
		public void Extract(ExtractExistingFileAction extractExistingFile)
		{
			this.ExtractExistingFile = extractExistingFile;
			this.InternalExtract(".", null, null);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000678B File Offset: 0x0000498B
		public void Extract(Stream stream)
		{
			this.InternalExtract(null, stream, null);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006796 File Offset: 0x00004996
		public void Extract(string baseDirectory)
		{
			this.InternalExtract(baseDirectory, null, null);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000067A1 File Offset: 0x000049A1
		public void Extract(string baseDirectory, ExtractExistingFileAction extractExistingFile)
		{
			this.ExtractExistingFile = extractExistingFile;
			this.InternalExtract(baseDirectory, null, null);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000067B3 File Offset: 0x000049B3
		public void ExtractWithPassword(string password)
		{
			this.InternalExtract(".", null, password);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000067C2 File Offset: 0x000049C2
		public void ExtractWithPassword(string baseDirectory, string password)
		{
			this.InternalExtract(baseDirectory, null, password);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000067CD File Offset: 0x000049CD
		public void ExtractWithPassword(ExtractExistingFileAction extractExistingFile, string password)
		{
			this.ExtractExistingFile = extractExistingFile;
			this.InternalExtract(".", null, password);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000067E3 File Offset: 0x000049E3
		public void ExtractWithPassword(string baseDirectory, ExtractExistingFileAction extractExistingFile, string password)
		{
			this.ExtractExistingFile = extractExistingFile;
			this.InternalExtract(baseDirectory, null, password);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000067F5 File Offset: 0x000049F5
		public void ExtractWithPassword(Stream stream, string password)
		{
			this.InternalExtract(null, stream, password);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006800 File Offset: 0x00004A00
		public CrcCalculatorStream OpenReader()
		{
			if (this._container.ZipFile == null)
			{
				throw new InvalidOperationException("Use OpenReader() only with ZipFile.");
			}
			return this.InternalOpenReader(this._Password ?? this._container.Password);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006835 File Offset: 0x00004A35
		public CrcCalculatorStream OpenReader(string password)
		{
			if (this._container.ZipFile == null)
			{
				throw new InvalidOperationException("Use OpenReader() only with ZipFile.");
			}
			return this.InternalOpenReader(password);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006858 File Offset: 0x00004A58
		internal CrcCalculatorStream InternalOpenReader(string password)
		{
			this.ValidateCompression();
			this.ValidateEncryption();
			this.SetupCryptoForExtract(password);
			if (this._Source != ZipEntrySource.ZipFile)
			{
				throw new BadStateException("You must call ZipFile.Save before calling OpenReader");
			}
			long num = ((this._CompressionMethod_FromZipFile == 0) ? this._CompressedFileDataSize : this.UncompressedSize);
			Stream archiveStream = this.ArchiveStream;
			this.ArchiveStream.Seek(this.FileDataPosition, 0);
			this._inputDecryptorStream = this.GetExtractDecryptor(archiveStream);
			Stream extractDecompressor = this.GetExtractDecompressor(this._inputDecryptorStream);
			return new CrcCalculatorStream(extractDecompressor, num);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000068DE File Offset: 0x00004ADE
		private void OnExtractProgress(long bytesWritten, long totalBytesToWrite)
		{
			if (this._container.ZipFile != null)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnExtractBlock(this, bytesWritten, totalBytesToWrite);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006906 File Offset: 0x00004B06
		private void OnBeforeExtract(string path)
		{
			if (this._container.ZipFile != null && !this._container.ZipFile._inExtractAll)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnSingleEntryExtract(this, path, true);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006940 File Offset: 0x00004B40
		private void OnAfterExtract(string path)
		{
			if (this._container.ZipFile != null && !this._container.ZipFile._inExtractAll)
			{
				this._container.ZipFile.OnSingleEntryExtract(this, path, false);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006975 File Offset: 0x00004B75
		private void OnExtractExisting(string path)
		{
			if (this._container.ZipFile != null)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnExtractExisting(this, path);
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000699C File Offset: 0x00004B9C
		private static void ReallyDelete(string fileName)
		{
			File.Delete(fileName);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000069A4 File Offset: 0x00004BA4
		private void WriteStatus(string format, params object[] args)
		{
			if (this._container.ZipFile != null && this._container.ZipFile.Verbose)
			{
				this._container.ZipFile.StatusMessageTextWriter.WriteLine(format, args);
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000069DC File Offset: 0x00004BDC
		private void InternalExtract(string baseDir, Stream outstream, string password)
		{
			if (this._container == null)
			{
				throw new BadStateException("This entry is an orphan");
			}
			if (this._container.ZipFile == null)
			{
				throw new InvalidOperationException("Use Extract() only with ZipFile.");
			}
			this._container.ZipFile.Reset(false);
			if (this._Source != ZipEntrySource.ZipFile)
			{
				throw new BadStateException("You must call ZipFile.Save before calling any Extract method");
			}
			this.OnBeforeExtract(baseDir);
			this._ioOperationCanceled = false;
			string text = null;
			Stream stream = null;
			bool flag = false;
			bool flag2 = false;
			try
			{
				this.ValidateCompression();
				this.ValidateEncryption();
				if (this.ValidateOutput(baseDir, outstream, out text))
				{
					this.WriteStatus("extract dir {0}...", new object[] { text });
					this.OnAfterExtract(baseDir);
				}
				else
				{
					if (text != null && File.Exists(text))
					{
						flag = true;
						int num = this.CheckExtractExistingFile(baseDir, text);
						if (num == 2)
						{
							goto IL_028E;
						}
						if (num == 1)
						{
							return;
						}
					}
					string text2 = password ?? this._Password ?? this._container.Password;
					if (this._Encryption_FromZipFile != EncryptionAlgorithm.None)
					{
						if (text2 == null)
						{
							throw new BadPasswordException();
						}
						this.SetupCryptoForExtract(text2);
					}
					if (text != null)
					{
						this.WriteStatus("extract file {0}...", new object[] { text });
						text += ".tmp";
						string directoryName = Path.GetDirectoryName(text);
						if (!Directory.Exists(directoryName))
						{
							Directory.CreateDirectory(directoryName);
						}
						else if (this._container.ZipFile != null)
						{
							flag2 = this._container.ZipFile._inExtractAll;
						}
						stream = new FileStream(text, 1);
					}
					else
					{
						this.WriteStatus("extract entry {0} to stream...", new object[] { this.FileName });
						stream = outstream;
					}
					if (!this._ioOperationCanceled)
					{
						int num2 = this.ExtractOne(stream);
						if (!this._ioOperationCanceled)
						{
							this.VerifyCrcAfterExtract(num2);
							if (text != null)
							{
								stream.Close();
								stream = null;
								string text3 = text;
								string text4 = null;
								text = text3.Substring(0, text3.Length - 4);
								if (flag)
								{
									text4 = text + ".PendingOverwrite";
									File.Move(text, text4);
								}
								File.Move(text3, text);
								this._SetTimes(text, true);
								if (text4 != null && File.Exists(text4))
								{
									ZipEntry.ReallyDelete(text4);
								}
								if (flag2 && this.FileName.IndexOf('/') != -1)
								{
									string directoryName2 = Path.GetDirectoryName(this.FileName);
									if (this._container.ZipFile[directoryName2] == null)
									{
										this._SetTimes(Path.GetDirectoryName(text), false);
									}
								}
								if (((int)this._VersionMadeBy & 65280) == 2560 || ((int)this._VersionMadeBy & 65280) == 0)
								{
									File.SetAttributes(text, this._ExternalFileAttrs);
								}
							}
							this.OnAfterExtract(baseDir);
						}
					}
					IL_028E:;
				}
			}
			catch (Exception)
			{
				this._ioOperationCanceled = true;
				throw;
			}
			finally
			{
				if (this._ioOperationCanceled && text != null)
				{
					if (stream != null)
					{
						stream.Close();
					}
					if (File.Exists(text) && !flag)
					{
						File.Delete(text);
					}
				}
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006CE0 File Offset: 0x00004EE0
		internal void VerifyCrcAfterExtract(int actualCrc32)
		{
			if (actualCrc32 != this._Crc32 && ((this.Encryption != EncryptionAlgorithm.WinZipAes128 && this.Encryption != EncryptionAlgorithm.WinZipAes256) || this._WinZipAesMethod != 2))
			{
				throw new BadCrcException("CRC error: the file being extracted appears to be corrupted. " + string.Format("Expected 0x{0:X8}, Actual 0x{1:X8}", this._Crc32, actualCrc32));
			}
			if (this.UncompressedSize == 0L)
			{
				return;
			}
			if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				WinZipAesCipherStream winZipAesCipherStream = this._inputDecryptorStream as WinZipAesCipherStream;
				this._aesCrypto_forExtract.CalculatedMac = winZipAesCipherStream.FinalAuthentication;
				this._aesCrypto_forExtract.ReadAndVerifyMac(this.ArchiveStream);
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006D88 File Offset: 0x00004F88
		private int CheckExtractExistingFile(string baseDir, string targetFileName)
		{
			int num = 0;
			for (;;)
			{
				switch (this.ExtractExistingFile)
				{
				case ExtractExistingFileAction.OverwriteSilently:
					goto IL_0021;
				case ExtractExistingFileAction.DoNotOverwrite:
					goto IL_003A;
				case ExtractExistingFileAction.InvokeExtractProgressEvent:
					if (num > 0)
					{
						goto Block_2;
					}
					this.OnExtractExisting(baseDir);
					if (this._ioOperationCanceled)
					{
						return 2;
					}
					num++;
					continue;
				}
				break;
			}
			goto IL_0085;
			IL_0021:
			this.WriteStatus("the file {0} exists; will overwrite it...", new object[] { targetFileName });
			return 0;
			IL_003A:
			this.WriteStatus("the file {0} exists; not extracting entry...", new object[] { this.FileName });
			this.OnAfterExtract(baseDir);
			return 1;
			Block_2:
			throw new ZipException(string.Format("The file {0} already exists.", targetFileName));
			IL_0085:
			throw new ZipException(string.Format("The file {0} already exists.", targetFileName));
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006E33 File Offset: 0x00005033
		private void _CheckRead(int nbytes)
		{
			if (nbytes == 0)
			{
				throw new BadReadException(string.Format("bad read of entry {0} from compressed archive.", this.FileName));
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006E50 File Offset: 0x00005050
		private int ExtractOne(Stream output)
		{
			int num = 0;
			Stream archiveStream = this.ArchiveStream;
			try
			{
				archiveStream.Seek(this.FileDataPosition, 0);
				byte[] array = new byte[this.BufferSize];
				long num2 = ((this._CompressionMethod_FromZipFile != 0) ? this.UncompressedSize : this._CompressedFileDataSize);
				this._inputDecryptorStream = this.GetExtractDecryptor(archiveStream);
				Stream extractDecompressor = this.GetExtractDecompressor(this._inputDecryptorStream);
				long num3 = 0L;
				using (CrcCalculatorStream crcCalculatorStream = new CrcCalculatorStream(extractDecompressor))
				{
					while (num2 > 0L)
					{
						int num4 = ((num2 > (long)array.Length) ? array.Length : ((int)num2));
						int num5 = crcCalculatorStream.Read(array, 0, num4);
						this._CheckRead(num5);
						output.Write(array, 0, num5);
						num2 -= (long)num5;
						num3 += (long)num5;
						this.OnExtractProgress(num3, this.UncompressedSize);
						if (this._ioOperationCanceled)
						{
							break;
						}
					}
					num = crcCalculatorStream.Crc;
				}
			}
			finally
			{
				ZipSegmentedStream zipSegmentedStream = archiveStream as ZipSegmentedStream;
				if (zipSegmentedStream != null)
				{
					zipSegmentedStream.Dispose();
					this._archiveStream = null;
				}
			}
			return num;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006F6C File Offset: 0x0000516C
		internal Stream GetExtractDecompressor(Stream input2)
		{
			short compressionMethod_FromZipFile = this._CompressionMethod_FromZipFile;
			if (compressionMethod_FromZipFile == 0)
			{
				return input2;
			}
			if (compressionMethod_FromZipFile == 8)
			{
				return new DeflateStream(input2, CompressionMode.Decompress, true);
			}
			if (compressionMethod_FromZipFile != 12)
			{
				return null;
			}
			return new BZip2InputStream(input2, true);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006FA4 File Offset: 0x000051A4
		internal Stream GetExtractDecryptor(Stream input)
		{
			Stream stream;
			if (this._Encryption_FromZipFile == EncryptionAlgorithm.PkzipWeak)
			{
				stream = new ZipCipherStream(input, this._zipCrypto_forExtract, CryptoMode.Decrypt);
			}
			else if (this._Encryption_FromZipFile == EncryptionAlgorithm.WinZipAes128 || this._Encryption_FromZipFile == EncryptionAlgorithm.WinZipAes256)
			{
				stream = new WinZipAesCipherStream(input, this._aesCrypto_forExtract, this._CompressedFileDataSize, CryptoMode.Decrypt);
			}
			else
			{
				stream = input;
			}
			return stream;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006FF7 File Offset: 0x000051F7
		internal void _SetTimes(string fileOrDirectory, bool isFile)
		{
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00006FFC File Offset: 0x000051FC
		private string UnsupportedAlgorithm
		{
			get
			{
				string empty = string.Empty;
				uint unsupportedAlgorithmId = this._UnsupportedAlgorithmId;
				if (unsupportedAlgorithmId <= 26128U)
				{
					if (unsupportedAlgorithmId <= 26115U)
					{
						if (unsupportedAlgorithmId == 0U)
						{
							return "--";
						}
						switch (unsupportedAlgorithmId)
						{
						case 26113U:
							return "DES";
						case 26114U:
							return "RC2";
						case 26115U:
							return "3DES-168";
						}
					}
					else
					{
						if (unsupportedAlgorithmId == 26121U)
						{
							return "3DES-112";
						}
						switch (unsupportedAlgorithmId)
						{
						case 26126U:
							return "PKWare AES128";
						case 26127U:
							return "PKWare AES192";
						case 26128U:
							return "PKWare AES256";
						}
					}
				}
				else if (unsupportedAlgorithmId <= 26401U)
				{
					if (unsupportedAlgorithmId == 26370U)
					{
						return "RC2";
					}
					switch (unsupportedAlgorithmId)
					{
					case 26400U:
						return "Blowfish";
					case 26401U:
						return "Twofish";
					}
				}
				else
				{
					if (unsupportedAlgorithmId == 26625U)
					{
						return "RC4";
					}
					if (unsupportedAlgorithmId != 65535U)
					{
					}
				}
				return string.Format("Unknown (0x{0:X4})", this._UnsupportedAlgorithmId);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000711C File Offset: 0x0000531C
		private string UnsupportedCompressionMethod
		{
			get
			{
				string empty = string.Empty;
				int compressionMethod = (int)this._CompressionMethod;
				if (compressionMethod <= 14)
				{
					switch (compressionMethod)
					{
					case 0:
						return "Store";
					case 1:
						return "Shrink";
					default:
						switch (compressionMethod)
						{
						case 8:
							return "DEFLATE";
						case 9:
							return "Deflate64";
						case 12:
							return "BZIP2";
						case 14:
							return "LZMA";
						}
						break;
					}
				}
				else
				{
					if (compressionMethod == 19)
					{
						return "LZ77";
					}
					if (compressionMethod == 98)
					{
						return "PPMd";
					}
				}
				return string.Format("Unknown (0x{0:X4})", this._CompressionMethod);
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000071D4 File Offset: 0x000053D4
		internal void ValidateEncryption()
		{
			if (this.Encryption == EncryptionAlgorithm.PkzipWeak || this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256 || this.Encryption == EncryptionAlgorithm.None)
			{
				return;
			}
			if (this._UnsupportedAlgorithmId != 0U)
			{
				throw new ZipException(string.Format("Cannot extract: Entry {0} is encrypted with an algorithm not supported by DotNetZip: {1}", this.FileName, this.UnsupportedAlgorithm));
			}
			throw new ZipException(string.Format("Cannot extract: Entry {0} uses an unsupported encryption algorithm ({1:X2})", this.FileName, (int)this.Encryption));
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000724C File Offset: 0x0000544C
		private void ValidateCompression()
		{
			if (this._CompressionMethod_FromZipFile != 0 && this._CompressionMethod_FromZipFile != 8 && this._CompressionMethod_FromZipFile != 12)
			{
				throw new ZipException(string.Format("Entry {0} uses an unsupported compression method (0x{1:X2}, {2})", this.FileName, this._CompressionMethod_FromZipFile, this.UnsupportedCompressionMethod));
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000729C File Offset: 0x0000549C
		private void SetupCryptoForExtract(string password)
		{
			if (this._Encryption_FromZipFile == EncryptionAlgorithm.None)
			{
				return;
			}
			if (this._Encryption_FromZipFile != EncryptionAlgorithm.PkzipWeak)
			{
				if (this._Encryption_FromZipFile == EncryptionAlgorithm.WinZipAes128 || this._Encryption_FromZipFile == EncryptionAlgorithm.WinZipAes256)
				{
					if (password == null)
					{
						throw new ZipException("Missing password.");
					}
					if (this._aesCrypto_forExtract != null)
					{
						this._aesCrypto_forExtract.Password = password;
						return;
					}
					int lengthOfCryptoHeaderBytes = ZipEntry.GetLengthOfCryptoHeaderBytes(this._Encryption_FromZipFile);
					this.ArchiveStream.Seek(this.FileDataPosition - (long)lengthOfCryptoHeaderBytes, 0);
					int keyStrengthInBits = ZipEntry.GetKeyStrengthInBits(this._Encryption_FromZipFile);
					this._aesCrypto_forExtract = WinZipAesCrypto.ReadFromStream(password, keyStrengthInBits, this.ArchiveStream);
				}
				return;
			}
			if (password == null)
			{
				throw new ZipException("Missing password.");
			}
			this.ArchiveStream.Seek(this.FileDataPosition - 12L, 0);
			this._zipCrypto_forExtract = ZipCrypto.ForRead(password, this);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007364 File Offset: 0x00005564
		private bool ValidateOutput(string basedir, Stream outstream, out string outFileName)
		{
			if (basedir != null)
			{
				string text = this.FileName.Replace(Path.DirectorySeparatorChar, '/');
				if (text.IndexOf(':') == 1)
				{
					text = text.Substring(2);
				}
				if (text.StartsWith("/"))
				{
					text = text.Substring(1);
				}
				if (this._container.ZipFile.FlattenFoldersOnExtract)
				{
					outFileName = Path.Combine(basedir, (text.IndexOf('/') != -1) ? Path.GetFileName(text) : text);
				}
				else
				{
					outFileName = Path.Combine(basedir, text);
				}
				outFileName = outFileName.Replace('/', Path.DirectorySeparatorChar);
				if (this.IsDirectory || this.FileName.EndsWith("/"))
				{
					if (!Directory.Exists(outFileName))
					{
						Directory.CreateDirectory(outFileName);
						this._SetTimes(outFileName, false);
					}
					else if (this.ExtractExistingFile == ExtractExistingFileAction.OverwriteSilently)
					{
						this._SetTimes(outFileName, false);
					}
					return true;
				}
				return false;
			}
			else
			{
				if (outstream != null)
				{
					outFileName = null;
					return this.IsDirectory || this.FileName.EndsWith("/");
				}
				throw new ArgumentNullException("outstream");
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007478 File Offset: 0x00005678
		private void ReadExtraField()
		{
			this._readExtraDepth++;
			long position = this.ArchiveStream.Position;
			this.ArchiveStream.Seek(this._RelativeOffsetOfLocalHeader, 0);
			byte[] array = new byte[30];
			this.ArchiveStream.Read(array, 0, array.Length);
			int num = 26;
			short num2 = (short)((int)array[num++] + (int)array[num++] * 256);
			short num3 = (short)((int)array[num++] + (int)array[num++] * 256);
			this.ArchiveStream.Seek((long)num2, 1);
			this.ProcessExtraField(this.ArchiveStream, num3);
			this.ArchiveStream.Seek(position, 0);
			this._readExtraDepth--;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007538 File Offset: 0x00005738
		private static bool ReadHeader(ZipEntry ze, Encoding defaultEncoding)
		{
			int num = 0;
			ze._RelativeOffsetOfLocalHeader = ze.ArchiveStream.Position;
			int num2 = SharedUtilities.ReadEntrySignature(ze.ArchiveStream);
			num += 4;
			if (ZipEntry.IsNotValidSig(num2))
			{
				ze.ArchiveStream.Seek(-4L, 1);
				if (ZipEntry.IsNotValidZipDirEntrySig(num2) && (long)num2 != 101010256L)
				{
					throw new BadReadException(string.Format("  Bad signature (0x{0:X8}) at position  0x{1:X8}", num2, ze.ArchiveStream.Position));
				}
				return false;
			}
			else
			{
				byte[] array = new byte[26];
				int num3 = ze.ArchiveStream.Read(array, 0, array.Length);
				if (num3 != array.Length)
				{
					return false;
				}
				num += num3;
				int num4 = 0;
				ze._VersionNeeded = (short)((int)array[num4++] + (int)array[num4++] * 256);
				ze._BitField = (short)((int)array[num4++] + (int)array[num4++] * 256);
				ze._CompressionMethod_FromZipFile = (ze._CompressionMethod = (short)((int)array[num4++] + (int)array[num4++] * 256));
				ze._TimeBlob = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
				ze._LastModified = SharedUtilities.PackedToDateTime(ze._TimeBlob);
				ze._timestamp |= ZipEntryTimestamp.DOS;
				if ((ze._BitField & 1) == 1)
				{
					ze._Encryption_FromZipFile = (ze._Encryption = EncryptionAlgorithm.PkzipWeak);
					ze._sourceIsEncrypted = true;
				}
				ze._Crc32 = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
				ze._CompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
				ze._UncompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
				if ((uint)ze._CompressedSize == 4294967295U || (uint)ze._UncompressedSize == 4294967295U)
				{
					ze._InputUsesZip64 = true;
				}
				short num5 = (short)((int)array[num4++] + (int)array[num4++] * 256);
				short num6 = (short)((int)array[num4++] + (int)array[num4++] * 256);
				array = new byte[(int)num5];
				num3 = ze.ArchiveStream.Read(array, 0, array.Length);
				num += num3;
				if ((ze._BitField & 2048) == 2048)
				{
					ze.AlternateEncoding = Encoding.UTF8;
					ze.AlternateEncodingUsage = ZipOption.Always;
				}
				ze._FileNameInArchive = ze.AlternateEncoding.GetString(array, 0, array.Length);
				if (ze._FileNameInArchive.EndsWith("/"))
				{
					ze.MarkAsDirectory();
				}
				num += ze.ProcessExtraField(ze.ArchiveStream, num6);
				ze._LengthOfTrailer = 0;
				if (!ze._FileNameInArchive.EndsWith("/") && (ze._BitField & 8) == 8)
				{
					long position = ze.ArchiveStream.Position;
					bool flag = true;
					long num7 = 0L;
					int num8 = 0;
					while (flag)
					{
						num8++;
						if (ze._container.ZipFile != null)
						{
							ze._container.ZipFile.OnReadBytes(ze);
						}
						long num9 = SharedUtilities.FindSignature(ze.ArchiveStream, 134695760);
						if (num9 == -1L)
						{
							return false;
						}
						num7 += num9;
						if (ze._InputUsesZip64)
						{
							array = new byte[20];
							num3 = ze.ArchiveStream.Read(array, 0, array.Length);
							if (num3 != 20)
							{
								return false;
							}
							num4 = 0;
							ze._Crc32 = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
							ze._CompressedSize = BitConverter.ToInt64(array, num4);
							num4 += 8;
							ze._UncompressedSize = BitConverter.ToInt64(array, num4);
							num4 += 8;
							ze._LengthOfTrailer += 24;
						}
						else
						{
							array = new byte[12];
							num3 = ze.ArchiveStream.Read(array, 0, array.Length);
							if (num3 != 12)
							{
								return false;
							}
							num4 = 0;
							ze._Crc32 = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
							ze._CompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
							ze._UncompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
							ze._LengthOfTrailer += 16;
						}
						flag = num7 != ze._CompressedSize;
						if (flag)
						{
							ze.ArchiveStream.Seek(-12L, 1);
							num7 += 4L;
						}
					}
					ze.ArchiveStream.Seek(position, 0);
				}
				ze._CompressedFileDataSize = ze._CompressedSize;
				if ((ze._BitField & 1) == 1)
				{
					if (ze.Encryption == EncryptionAlgorithm.WinZipAes128 || ze.Encryption == EncryptionAlgorithm.WinZipAes256)
					{
						int keyStrengthInBits = ZipEntry.GetKeyStrengthInBits(ze._Encryption_FromZipFile);
						ze._aesCrypto_forExtract = WinZipAesCrypto.ReadFromStream(null, keyStrengthInBits, ze.ArchiveStream);
						num += ze._aesCrypto_forExtract.SizeOfEncryptionMetadata - 10;
						ze._CompressedFileDataSize -= (long)ze._aesCrypto_forExtract.SizeOfEncryptionMetadata;
						ze._LengthOfTrailer += 10;
					}
					else
					{
						ze._WeakEncryptionHeader = new byte[12];
						num += ZipEntry.ReadWeakEncryptionHeader(ze._archiveStream, ze._WeakEncryptionHeader);
						ze._CompressedFileDataSize -= 12L;
					}
				}
				ze._LengthOfHeader = num;
				ze._TotalEntrySize = (long)ze._LengthOfHeader + ze._CompressedFileDataSize + (long)ze._LengthOfTrailer;
				return true;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007C34 File Offset: 0x00005E34
		internal static int ReadWeakEncryptionHeader(Stream s, byte[] buffer)
		{
			int num = s.Read(buffer, 0, 12);
			if (num != 12)
			{
				throw new ZipException(string.Format("Unexpected end of data at position 0x{0:X8}", s.Position));
			}
			return num;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007C6D File Offset: 0x00005E6D
		private static bool IsNotValidSig(int signature)
		{
			return signature != 67324752;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007C7C File Offset: 0x00005E7C
		internal static ZipEntry ReadEntry(ZipContainer zc, bool first)
		{
			ZipFile zipFile = zc.ZipFile;
			Stream readStream = zc.ReadStream;
			Encoding alternateEncoding = zc.AlternateEncoding;
			ZipEntry zipEntry = new ZipEntry();
			zipEntry._Source = ZipEntrySource.ZipFile;
			zipEntry._container = zc;
			zipEntry._archiveStream = readStream;
			if (zipFile != null)
			{
				zipFile.OnReadEntry(true, null);
			}
			if (first)
			{
				ZipEntry.HandlePK00Prefix(readStream);
			}
			if (!ZipEntry.ReadHeader(zipEntry, alternateEncoding))
			{
				return null;
			}
			zipEntry.__FileDataPosition = zipEntry.ArchiveStream.Position;
			readStream.Seek(zipEntry._CompressedFileDataSize + (long)zipEntry._LengthOfTrailer, 1);
			ZipEntry.HandleUnexpectedDataDescriptor(zipEntry);
			if (zipFile != null)
			{
				zipFile.OnReadBytes(zipEntry);
				zipFile.OnReadEntry(false, zipEntry);
			}
			return zipEntry;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007D18 File Offset: 0x00005F18
		internal static void HandlePK00Prefix(Stream s)
		{
			uint num = (uint)SharedUtilities.ReadInt(s);
			if (num != 808471376U)
			{
				s.Seek(-4L, 1);
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007D40 File Offset: 0x00005F40
		private static void HandleUnexpectedDataDescriptor(ZipEntry entry)
		{
			Stream archiveStream = entry.ArchiveStream;
			uint num = (uint)SharedUtilities.ReadInt(archiveStream);
			if ((ulong)num != (ulong)((long)entry._Crc32))
			{
				archiveStream.Seek(-4L, 1);
				return;
			}
			int num2 = SharedUtilities.ReadInt(archiveStream);
			if ((long)num2 != entry._CompressedSize)
			{
				archiveStream.Seek(-8L, 1);
				return;
			}
			num2 = SharedUtilities.ReadInt(archiveStream);
			if ((long)num2 == entry._UncompressedSize)
			{
				return;
			}
			archiveStream.Seek(-12L, 1);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007DAC File Offset: 0x00005FAC
		internal static int FindExtraFieldSegment(byte[] extra, int offx, ushort targetHeaderId)
		{
			int num = offx;
			while (num + 3 < extra.Length)
			{
				ushort num2 = (ushort)((int)extra[num++] + (int)extra[num++] * 256);
				if (num2 == targetHeaderId)
				{
					return num - 2;
				}
				short num3 = (short)((int)extra[num++] + (int)extra[num++] * 256);
				num += (int)num3;
			}
			return -1;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007E00 File Offset: 0x00006000
		internal int ProcessExtraField(Stream s, short extraFieldLength)
		{
			int num = 0;
			if (extraFieldLength > 0)
			{
				byte[] array = (this._Extra = new byte[(int)extraFieldLength]);
				num = s.Read(array, 0, array.Length);
				long num2 = s.Position - (long)num;
				int num3 = 0;
				while (num3 + 3 < array.Length)
				{
					int num4 = num3;
					ushort num5 = (ushort)((int)array[num3++] + (int)array[num3++] * 256);
					short num6 = (short)((int)array[num3++] + (int)array[num3++] * 256);
					ushort num7 = num5;
					if (num7 <= 21589)
					{
						if (num7 <= 10)
						{
							if (num7 != 1)
							{
								if (num7 == 10)
								{
									num3 = this.ProcessExtraFieldWindowsTimes(array, num3, num6, num2);
								}
							}
							else
							{
								num3 = this.ProcessExtraFieldZip64(array, num3, num6, num2);
							}
						}
						else if (num7 != 23)
						{
							if (num7 == 21589)
							{
								num3 = this.ProcessExtraFieldUnixTimes(array, num3, num6, num2);
							}
						}
						else
						{
							num3 = this.ProcessExtraFieldPkwareStrongEncryption(array, num3);
						}
					}
					else if (num7 <= 30805)
					{
						if (num7 != 22613)
						{
							if (num7 != 30805)
							{
							}
						}
						else
						{
							num3 = this.ProcessExtraFieldInfoZipTimes(array, num3, num6, num2);
						}
					}
					else if (num7 != 30837)
					{
						if (num7 == 39169)
						{
							num3 = this.ProcessExtraFieldWinZipAes(array, num3, num6, num2);
						}
					}
					num3 = num4 + (int)num6 + 4;
				}
			}
			return num;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007F48 File Offset: 0x00006148
		private int ProcessExtraFieldPkwareStrongEncryption(byte[] Buffer, int j)
		{
			j += 2;
			this._UnsupportedAlgorithmId = (uint)((ushort)((int)Buffer[j++] + (int)Buffer[j++] * 256));
			this._Encryption_FromZipFile = (this._Encryption = EncryptionAlgorithm.Unsupported);
			return j;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007F8C File Offset: 0x0000618C
		private int ProcessExtraFieldWinZipAes(byte[] buffer, int j, short dataSize, long posn)
		{
			if (this._CompressionMethod == 99)
			{
				if ((this._BitField & 1) != 1)
				{
					throw new BadReadException(string.Format("  Inconsistent metadata at position 0x{0:X16}", posn));
				}
				this._sourceIsEncrypted = true;
				if (dataSize != 7)
				{
					throw new BadReadException(string.Format("  Inconsistent size (0x{0:X4}) in WinZip AES field at position 0x{1:X16}", dataSize, posn));
				}
				this._WinZipAesMethod = BitConverter.ToInt16(buffer, j);
				j += 2;
				if (this._WinZipAesMethod != 1 && this._WinZipAesMethod != 2)
				{
					throw new BadReadException(string.Format("  Unexpected vendor version number (0x{0:X4}) for WinZip AES metadata at position 0x{1:X16}", this._WinZipAesMethod, posn));
				}
				short num = BitConverter.ToInt16(buffer, j);
				j += 2;
				if (num != 17729)
				{
					throw new BadReadException(string.Format("  Unexpected vendor ID (0x{0:X4}) for WinZip AES metadata at position 0x{1:X16}", num, posn));
				}
				int num2 = ((buffer[j] == 1) ? 128 : ((buffer[j] == 3) ? 256 : (-1)));
				if (num2 < 0)
				{
					throw new BadReadException(string.Format("Invalid key strength ({0})", num2));
				}
				this._Encryption_FromZipFile = (this._Encryption = ((num2 == 128) ? EncryptionAlgorithm.WinZipAes128 : EncryptionAlgorithm.WinZipAes256));
				j++;
				this._CompressionMethod_FromZipFile = (this._CompressionMethod = BitConverter.ToInt16(buffer, j));
				j += 2;
			}
			return j;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000080DA File Offset: 0x000062DA
		private int ProcessExtraFieldZip64(byte[] buffer, int j, short dataSize, long posn)
		{
			this._InputUsesZip64 = true;
			if (dataSize > 28)
			{
				throw new BadReadException(string.Format("  Inconsistent size (0x{0:X4}) for ZIP64 extra field at position 0x{1:X16}", dataSize, posn));
			}
			return j;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008108 File Offset: 0x00006308
		private int ProcessExtraFieldInfoZipTimes(byte[] buffer, int j, short dataSize, long posn)
		{
			if (dataSize != 12 && dataSize != 8)
			{
				throw new BadReadException(string.Format("  Unexpected size (0x{0:X4}) for InfoZip v1 extra field at position 0x{1:X16}", dataSize, posn));
			}
			int num = BitConverter.ToInt32(buffer, j);
			this._Mtime = ZipEntry._unixEpoch.AddSeconds((double)num);
			j += 4;
			num = BitConverter.ToInt32(buffer, j);
			this._Atime = ZipEntry._unixEpoch.AddSeconds((double)num);
			j += 4;
			this._Ctime = DateTime.UtcNow;
			this._ntfsTimesAreSet = true;
			this._timestamp |= ZipEntryTimestamp.InfoZip1;
			return j;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000819A File Offset: 0x0000639A
		private int ProcessExtraFieldUnixTimes(byte[] buffer, int j, short dataSize, long posn)
		{
			if (dataSize != 13 && dataSize != 9 && dataSize != 5)
			{
				throw new BadReadException(string.Format("  Unexpected size (0x{0:X4}) for Extended Timestamp extra field at position 0x{1:X16}", dataSize, posn));
			}
			return j;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000081C8 File Offset: 0x000063C8
		private int ProcessExtraFieldWindowsTimes(byte[] buffer, int j, short dataSize, long posn)
		{
			if (dataSize != 32)
			{
				throw new BadReadException(string.Format("  Unexpected size (0x{0:X4}) for NTFS times extra field at position 0x{1:X16}", dataSize, posn));
			}
			j += 4;
			short num = (short)((int)buffer[j] + (int)buffer[j + 1] * 256);
			short num2 = (short)((int)buffer[j + 2] + (int)buffer[j + 3] * 256);
			j += 4;
			if (num == 1 && num2 == 24)
			{
				long num3 = BitConverter.ToInt64(buffer, j);
				this._Mtime = DateTime.FromFileTimeUtc(num3);
				j += 8;
				num3 = BitConverter.ToInt64(buffer, j);
				this._Atime = DateTime.FromFileTimeUtc(num3);
				j += 8;
				num3 = BitConverter.ToInt64(buffer, j);
				this._Ctime = DateTime.FromFileTimeUtc(num3);
				j += 8;
				this._ntfsTimesAreSet = true;
				this._timestamp |= ZipEntryTimestamp.Windows;
				this._emitNtfsTimes = true;
			}
			return j;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00008298 File Offset: 0x00006498
		internal void WriteCentralDirectoryEntry(Stream s)
		{
			byte[] array = new byte[4096];
			int num = 0;
			array[num++] = 80;
			array[num++] = 75;
			array[num++] = 1;
			array[num++] = 2;
			array[num++] = (byte)(this._VersionMadeBy & 255);
			array[num++] = (byte)(((int)this._VersionMadeBy & 65280) >> 8);
			short num2 = ((this.VersionNeeded != 0) ? this.VersionNeeded : 20);
			if (this._OutputUsesZip64 == null)
			{
				this._OutputUsesZip64 = new bool?(this._container.Zip64 == Zip64Option.Always);
			}
			short num3 = (this._OutputUsesZip64.Value ? 45 : num2);
			if (this.CompressionMethod == CompressionMethod.BZip2)
			{
				num3 = 46;
			}
			array[num++] = (byte)(num3 & 255);
			array[num++] = (byte)(((int)num3 & 65280) >> 8);
			array[num++] = (byte)(this._BitField & 255);
			array[num++] = (byte)(((int)this._BitField & 65280) >> 8);
			array[num++] = (byte)(this._CompressionMethod & 255);
			array[num++] = (byte)(((int)this._CompressionMethod & 65280) >> 8);
			if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				num -= 2;
				array[num++] = 99;
				array[num++] = 0;
			}
			array[num++] = (byte)(this._TimeBlob & 255);
			array[num++] = (byte)((this._TimeBlob & 65280) >> 8);
			array[num++] = (byte)((this._TimeBlob & 16711680) >> 16);
			array[num++] = (byte)(((long)this._TimeBlob & (long)((ulong)(-16777216))) >> 24);
			array[num++] = (byte)(this._Crc32 & 255);
			array[num++] = (byte)((this._Crc32 & 65280) >> 8);
			array[num++] = (byte)((this._Crc32 & 16711680) >> 16);
			array[num++] = (byte)(((long)this._Crc32 & (long)((ulong)(-16777216))) >> 24);
			if (this._OutputUsesZip64.Value)
			{
				for (int i = 0; i < 8; i++)
				{
					array[num++] = byte.MaxValue;
				}
			}
			else
			{
				array[num++] = (byte)(this._CompressedSize & 255L);
				array[num++] = (byte)((this._CompressedSize & 65280L) >> 8);
				array[num++] = (byte)((this._CompressedSize & 16711680L) >> 16);
				array[num++] = (byte)((this._CompressedSize & (long)((ulong)(-16777216))) >> 24);
				array[num++] = (byte)(this._UncompressedSize & 255L);
				array[num++] = (byte)((this._UncompressedSize & 65280L) >> 8);
				array[num++] = (byte)((this._UncompressedSize & 16711680L) >> 16);
				array[num++] = (byte)((this._UncompressedSize & (long)((ulong)(-16777216))) >> 24);
			}
			byte[] encodedFileNameBytes = this.GetEncodedFileNameBytes();
			short num4 = (short)encodedFileNameBytes.Length;
			array[num++] = (byte)(num4 & 255);
			array[num++] = (byte)(((int)num4 & 65280) >> 8);
			this._presumeZip64 = this._OutputUsesZip64.Value;
			this._Extra = this.ConstructExtraField(true);
			short num5 = (short)((this._Extra == null) ? 0 : this._Extra.Length);
			array[num++] = (byte)(num5 & 255);
			array[num++] = (byte)(((int)num5 & 65280) >> 8);
			int num6 = ((this._CommentBytes == null) ? 0 : this._CommentBytes.Length);
			if (num6 + num > array.Length)
			{
				num6 = array.Length - num;
			}
			array[num++] = (byte)(num6 & 255);
			array[num++] = (byte)((num6 & 65280) >> 8);
			bool flag = this._container.ZipFile != null && this._container.ZipFile.MaxOutputSegmentSize != 0;
			if (flag)
			{
				array[num++] = (byte)(this._diskNumber & 255U);
				array[num++] = (byte)((this._diskNumber & 65280U) >> 8);
			}
			else
			{
				array[num++] = 0;
				array[num++] = 0;
			}
			array[num++] = (this._IsText ? 1 : 0);
			array[num++] = 0;
			array[num++] = (byte)(this._ExternalFileAttrs & 255);
			array[num++] = (byte)((this._ExternalFileAttrs & 65280) >> 8);
			array[num++] = (byte)((this._ExternalFileAttrs & 16711680) >> 16);
			array[num++] = (byte)(((long)this._ExternalFileAttrs & (long)((ulong)(-16777216))) >> 24);
			if (this._RelativeOffsetOfLocalHeader > (long)((ulong)(-1)))
			{
				array[num++] = byte.MaxValue;
				array[num++] = byte.MaxValue;
				array[num++] = byte.MaxValue;
				array[num++] = byte.MaxValue;
			}
			else
			{
				array[num++] = (byte)(this._RelativeOffsetOfLocalHeader & 255L);
				array[num++] = (byte)((this._RelativeOffsetOfLocalHeader & 65280L) >> 8);
				array[num++] = (byte)((this._RelativeOffsetOfLocalHeader & 16711680L) >> 16);
				array[num++] = (byte)((this._RelativeOffsetOfLocalHeader & (long)((ulong)(-16777216))) >> 24);
			}
			Buffer.BlockCopy(encodedFileNameBytes, 0, array, num, (int)num4);
			num += (int)num4;
			if (this._Extra != null)
			{
				byte[] extra = this._Extra;
				int num7 = 0;
				Buffer.BlockCopy(extra, num7, array, num, (int)num5);
				num += (int)num5;
			}
			if (num6 != 0)
			{
				Buffer.BlockCopy(this._CommentBytes, 0, array, num, num6);
				num += num6;
			}
			s.Write(array, 0, num);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008830 File Offset: 0x00006A30
		private byte[] ConstructExtraField(bool forCentralDirectory)
		{
			List<byte[]> list = new List<byte[]>();
			if (this._container.Zip64 == Zip64Option.Always || (this._container.Zip64 == Zip64Option.AsNecessary && (!forCentralDirectory || this._entryRequiresZip64.Value)))
			{
				int num = 4 + (forCentralDirectory ? 28 : 16);
				byte[] array = new byte[num];
				int num2 = 0;
				if (this._presumeZip64 || forCentralDirectory)
				{
					array[num2++] = 1;
					array[num2++] = 0;
				}
				else
				{
					array[num2++] = 153;
					array[num2++] = 153;
				}
				array[num2++] = (byte)(num - 4);
				array[num2++] = 0;
				Array.Copy(BitConverter.GetBytes(this._UncompressedSize), 0, array, num2, 8);
				num2 += 8;
				Array.Copy(BitConverter.GetBytes(this._CompressedSize), 0, array, num2, 8);
				num2 += 8;
				if (forCentralDirectory)
				{
					Array.Copy(BitConverter.GetBytes(this._RelativeOffsetOfLocalHeader), 0, array, num2, 8);
					num2 += 8;
					Array.Copy(BitConverter.GetBytes(0), 0, array, num2, 4);
				}
				list.Add(array);
			}
			if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				byte[] array = new byte[11];
				int num3 = 0;
				array[num3++] = 1;
				array[num3++] = 153;
				array[num3++] = 7;
				array[num3++] = 0;
				array[num3++] = 1;
				array[num3++] = 0;
				array[num3++] = 65;
				array[num3++] = 69;
				int keyStrengthInBits = ZipEntry.GetKeyStrengthInBits(this.Encryption);
				if (keyStrengthInBits == 128)
				{
					array[num3] = 1;
				}
				else if (keyStrengthInBits == 256)
				{
					array[num3] = 3;
				}
				else
				{
					array[num3] = byte.MaxValue;
				}
				num3++;
				array[num3++] = (byte)(this._CompressionMethod & 255);
				array[num3++] = (byte)((int)this._CompressionMethod & 65280);
				list.Add(array);
			}
			if (this._ntfsTimesAreSet && this._emitNtfsTimes)
			{
				byte[] array = new byte[36];
				int num4 = 0;
				array[num4++] = 10;
				array[num4++] = 0;
				array[num4++] = 32;
				array[num4++] = 0;
				num4 += 4;
				array[num4++] = 1;
				array[num4++] = 0;
				array[num4++] = 24;
				array[num4++] = 0;
				long num5 = this._Mtime.ToFileTime();
				Array.Copy(BitConverter.GetBytes(num5), 0, array, num4, 8);
				num4 += 8;
				num5 = this._Atime.ToFileTime();
				Array.Copy(BitConverter.GetBytes(num5), 0, array, num4, 8);
				num4 += 8;
				num5 = this._Ctime.ToFileTime();
				Array.Copy(BitConverter.GetBytes(num5), 0, array, num4, 8);
				num4 += 8;
				list.Add(array);
			}
			if (this._ntfsTimesAreSet && this._emitUnixTimes)
			{
				int num6 = 9;
				if (!forCentralDirectory)
				{
					num6 += 8;
				}
				byte[] array = new byte[num6];
				int num7 = 0;
				array[num7++] = 85;
				array[num7++] = 84;
				array[num7++] = (byte)(num6 - 4);
				array[num7++] = 0;
				array[num7++] = 7;
				int num8 = (int)(this._Mtime - ZipEntry._unixEpoch).TotalSeconds;
				Array.Copy(BitConverter.GetBytes(num8), 0, array, num7, 4);
				num7 += 4;
				if (!forCentralDirectory)
				{
					num8 = (int)(this._Atime - ZipEntry._unixEpoch).TotalSeconds;
					Array.Copy(BitConverter.GetBytes(num8), 0, array, num7, 4);
					num7 += 4;
					num8 = (int)(this._Ctime - ZipEntry._unixEpoch).TotalSeconds;
					Array.Copy(BitConverter.GetBytes(num8), 0, array, num7, 4);
					num7 += 4;
				}
				list.Add(array);
			}
			byte[] array2 = null;
			if (list.Count > 0)
			{
				int num9 = 0;
				int num10 = 0;
				for (int i = 0; i < list.Count; i++)
				{
					num9 += list[i].Length;
				}
				array2 = new byte[num9];
				for (int i = 0; i < list.Count; i++)
				{
					Array.Copy(list[i], 0, array2, num10, list[i].Length);
					num10 += list[i].Length;
				}
			}
			return array2;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00008C90 File Offset: 0x00006E90
		private string NormalizeFileName()
		{
			string text = this.FileName.Replace("\\", "/");
			string text2;
			if (this._TrimVolumeFromFullyQualifiedPaths && this.FileName.Length >= 3 && this.FileName.get_Chars(1) == ':' && text.get_Chars(2) == '/')
			{
				text2 = text.Substring(3);
			}
			else if (this.FileName.Length >= 4 && text.get_Chars(0) == '/' && text.get_Chars(1) == '/')
			{
				int num = text.IndexOf('/', 2);
				if (num == -1)
				{
					throw new ArgumentException("The path for that entry appears to be badly formatted");
				}
				text2 = text.Substring(num + 1);
			}
			else if (this.FileName.Length >= 3 && text.get_Chars(0) == '.' && text.get_Chars(1) == '/')
			{
				text2 = text.Substring(2);
			}
			else
			{
				text2 = text;
			}
			return text2;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00008D6C File Offset: 0x00006F6C
		private byte[] GetEncodedFileNameBytes()
		{
			string text = this.NormalizeFileName();
			switch (this.AlternateEncodingUsage)
			{
			case ZipOption.Default:
				if (this._Comment != null && this._Comment.Length != 0)
				{
					this._CommentBytes = ZipEntry.ibm437.GetBytes(this._Comment);
				}
				this._actualEncoding = ZipEntry.ibm437;
				return ZipEntry.ibm437.GetBytes(text);
			case ZipOption.Always:
				if (this._Comment != null && this._Comment.Length != 0)
				{
					this._CommentBytes = this.AlternateEncoding.GetBytes(this._Comment);
				}
				this._actualEncoding = this.AlternateEncoding;
				return this.AlternateEncoding.GetBytes(text);
			}
			byte[] array = ZipEntry.ibm437.GetBytes(text);
			string @string = ZipEntry.ibm437.GetString(array, 0, array.Length);
			this._CommentBytes = null;
			if (@string != text)
			{
				array = this.AlternateEncoding.GetBytes(text);
				if (this._Comment != null && this._Comment.Length != 0)
				{
					this._CommentBytes = this.AlternateEncoding.GetBytes(this._Comment);
				}
				this._actualEncoding = this.AlternateEncoding;
				return array;
			}
			this._actualEncoding = ZipEntry.ibm437;
			if (this._Comment == null || this._Comment.Length == 0)
			{
				return array;
			}
			byte[] bytes = ZipEntry.ibm437.GetBytes(this._Comment);
			string string2 = ZipEntry.ibm437.GetString(bytes, 0, bytes.Length);
			if (string2 != this.Comment)
			{
				array = this.AlternateEncoding.GetBytes(text);
				this._CommentBytes = this.AlternateEncoding.GetBytes(this._Comment);
				this._actualEncoding = this.AlternateEncoding;
				return array;
			}
			this._CommentBytes = bytes;
			return array;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00008F28 File Offset: 0x00007128
		private bool WantReadAgain()
		{
			return this._UncompressedSize >= 16L && this._CompressionMethod != 0 && this.CompressionLevel != CompressionLevel.None && this._CompressedSize >= this._UncompressedSize && (this._Source != ZipEntrySource.Stream || this._sourceStream.CanSeek) && (this._aesCrypto_forWrite == null || this.CompressedSize - (long)this._aesCrypto_forWrite.SizeOfEncryptionMetadata > this.UncompressedSize + 16L) && (this._zipCrypto_forWrite == null || this.CompressedSize - 12L > this.UncompressedSize);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00008FC4 File Offset: 0x000071C4
		private void MaybeUnsetCompressionMethodForWriting(int cycle)
		{
			if (cycle > 1)
			{
				this._CompressionMethod = 0;
				return;
			}
			if (this.IsDirectory)
			{
				this._CompressionMethod = 0;
				return;
			}
			if (this._Source == ZipEntrySource.ZipFile)
			{
				return;
			}
			if (this._Source == ZipEntrySource.Stream)
			{
				if (this._sourceStream != null && this._sourceStream.CanSeek)
				{
					long length = this._sourceStream.Length;
					if (length == 0L)
					{
						this._CompressionMethod = 0;
						return;
					}
				}
			}
			else if (this._Source == ZipEntrySource.FileSystem && SharedUtilities.GetFileLength(this.LocalFileName) == 0L)
			{
				this._CompressionMethod = 0;
				return;
			}
			if (this.SetCompression != null)
			{
				this.CompressionLevel = this.SetCompression(this.LocalFileName, this._FileNameInArchive);
			}
			if (this.CompressionLevel == CompressionLevel.None && this.CompressionMethod == CompressionMethod.Deflate)
			{
				this._CompressionMethod = 0;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000908C File Offset: 0x0000728C
		internal void WriteHeader(Stream s, int cycle)
		{
			CountingStream countingStream = s as CountingStream;
			this._future_ROLH = ((countingStream != null) ? countingStream.ComputedPosition : s.Position);
			int num = 0;
			byte[] array = new byte[30];
			array[num++] = 80;
			array[num++] = 75;
			array[num++] = 3;
			array[num++] = 4;
			this._presumeZip64 = this._container.Zip64 == Zip64Option.Always || (this._container.Zip64 == Zip64Option.AsNecessary && !s.CanSeek);
			short num2 = (this._presumeZip64 ? 45 : 20);
			if (this.CompressionMethod == CompressionMethod.BZip2)
			{
				num2 = 46;
			}
			array[num++] = (byte)(num2 & 255);
			array[num++] = (byte)(((int)num2 & 65280) >> 8);
			byte[] encodedFileNameBytes = this.GetEncodedFileNameBytes();
			short num3 = (short)encodedFileNameBytes.Length;
			if (this._Encryption == EncryptionAlgorithm.None)
			{
				this._BitField &= -2;
			}
			else
			{
				this._BitField |= 1;
			}
			if (this._actualEncoding.WebName == "utf-8")
			{
				this._BitField |= 2048;
			}
			if (this.IsDirectory || cycle == 99)
			{
				this._BitField &= -9;
				this._BitField &= -2;
				this.Encryption = EncryptionAlgorithm.None;
				this.Password = null;
			}
			else if (!s.CanSeek)
			{
				this._BitField |= 8;
			}
			array[num++] = (byte)(this._BitField & 255);
			array[num++] = (byte)(((int)this._BitField & 65280) >> 8);
			if (this.__FileDataPosition == -1L)
			{
				this._CompressedSize = 0L;
				this._crcCalculated = false;
			}
			this.MaybeUnsetCompressionMethodForWriting(cycle);
			array[num++] = (byte)(this._CompressionMethod & 255);
			array[num++] = (byte)(((int)this._CompressionMethod & 65280) >> 8);
			if (cycle == 99)
			{
				this.SetZip64Flags();
			}
			else if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				num -= 2;
				array[num++] = 99;
				array[num++] = 0;
			}
			this._TimeBlob = SharedUtilities.DateTimeToPacked(this.LastModified);
			array[num++] = (byte)(this._TimeBlob & 255);
			array[num++] = (byte)((this._TimeBlob & 65280) >> 8);
			array[num++] = (byte)((this._TimeBlob & 16711680) >> 16);
			array[num++] = (byte)(((long)this._TimeBlob & (long)((ulong)(-16777216))) >> 24);
			array[num++] = (byte)(this._Crc32 & 255);
			array[num++] = (byte)((this._Crc32 & 65280) >> 8);
			array[num++] = (byte)((this._Crc32 & 16711680) >> 16);
			array[num++] = (byte)(((long)this._Crc32 & (long)((ulong)(-16777216))) >> 24);
			if (this._presumeZip64)
			{
				for (int i = 0; i < 8; i++)
				{
					array[num++] = byte.MaxValue;
				}
			}
			else
			{
				array[num++] = (byte)(this._CompressedSize & 255L);
				array[num++] = (byte)((this._CompressedSize & 65280L) >> 8);
				array[num++] = (byte)((this._CompressedSize & 16711680L) >> 16);
				array[num++] = (byte)((this._CompressedSize & (long)((ulong)(-16777216))) >> 24);
				array[num++] = (byte)(this._UncompressedSize & 255L);
				array[num++] = (byte)((this._UncompressedSize & 65280L) >> 8);
				array[num++] = (byte)((this._UncompressedSize & 16711680L) >> 16);
				array[num++] = (byte)((this._UncompressedSize & (long)((ulong)(-16777216))) >> 24);
			}
			array[num++] = (byte)(num3 & 255);
			array[num++] = (byte)(((int)num3 & 65280) >> 8);
			this._Extra = this.ConstructExtraField(false);
			short num4 = (short)((this._Extra == null) ? 0 : this._Extra.Length);
			array[num++] = (byte)(num4 & 255);
			array[num++] = (byte)(((int)num4 & 65280) >> 8);
			byte[] array2 = new byte[num + (int)num3 + (int)num4];
			Buffer.BlockCopy(array, 0, array2, 0, num);
			Buffer.BlockCopy(encodedFileNameBytes, 0, array2, num, encodedFileNameBytes.Length);
			num += encodedFileNameBytes.Length;
			if (this._Extra != null)
			{
				Buffer.BlockCopy(this._Extra, 0, array2, num, this._Extra.Length);
				num += this._Extra.Length;
			}
			this._LengthOfHeader = num;
			ZipSegmentedStream zipSegmentedStream = s as ZipSegmentedStream;
			if (zipSegmentedStream != null)
			{
				zipSegmentedStream.ContiguousWrite = true;
				uint num5 = zipSegmentedStream.ComputeSegment(num);
				if (num5 != zipSegmentedStream.CurrentSegment)
				{
					this._future_ROLH = 0L;
				}
				else
				{
					this._future_ROLH = zipSegmentedStream.Position;
				}
				this._diskNumber = num5;
			}
			if (this._container.Zip64 == Zip64Option.Default && (uint)this._RelativeOffsetOfLocalHeader >= 4294967295U)
			{
				throw new ZipException("Offset within the zip archive exceeds 0xFFFFFFFF. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
			}
			s.Write(array2, 0, num);
			if (zipSegmentedStream != null)
			{
				zipSegmentedStream.ContiguousWrite = false;
			}
			this._EntryHeader = array2;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000095A8 File Offset: 0x000077A8
		private int FigureCrc32()
		{
			if (!this._crcCalculated)
			{
				Stream stream = null;
				if (this._Source == ZipEntrySource.WriteDelegate)
				{
					CrcCalculatorStream crcCalculatorStream = new CrcCalculatorStream(Stream.Null);
					this._WriteDelegate(this.FileName, crcCalculatorStream);
					this._Crc32 = crcCalculatorStream.Crc;
				}
				else if (this._Source != ZipEntrySource.ZipFile)
				{
					if (this._Source == ZipEntrySource.Stream)
					{
						this.PrepSourceStream();
						stream = this._sourceStream;
					}
					else if (this._Source == ZipEntrySource.JitStream)
					{
						if (this._sourceStream == null)
						{
							this._sourceStream = this._OpenDelegate(this.FileName);
						}
						this.PrepSourceStream();
						stream = this._sourceStream;
					}
					else if (this._Source != ZipEntrySource.ZipOutputStream)
					{
						stream = File.Open(this.LocalFileName, 3, 1, 3);
					}
					CRC32 crc = new CRC32();
					this._Crc32 = crc.GetCrc32(stream);
					if (this._sourceStream == null)
					{
						stream.Dispose();
					}
				}
				this._crcCalculated = true;
			}
			return this._Crc32;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000969C File Offset: 0x0000789C
		private void PrepSourceStream()
		{
			if (this._sourceStream == null)
			{
				throw new ZipException(string.Format("The input stream is null for entry '{0}'.", this.FileName));
			}
			if (this._sourceStreamOriginalPosition != null)
			{
				this._sourceStream.Position = this._sourceStreamOriginalPosition.Value;
				return;
			}
			if (this._sourceStream.CanSeek)
			{
				this._sourceStreamOriginalPosition = new long?(this._sourceStream.Position);
				return;
			}
			if (this.Encryption == EncryptionAlgorithm.PkzipWeak && this._Source != ZipEntrySource.ZipFile && (this._BitField & 8) != 8)
			{
				throw new ZipException("It is not possible to use PKZIP encryption on a non-seekable input stream");
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00009738 File Offset: 0x00007938
		internal void CopyMetaData(ZipEntry source)
		{
			this.__FileDataPosition = source.__FileDataPosition;
			this.CompressionMethod = source.CompressionMethod;
			this._CompressionMethod_FromZipFile = source._CompressionMethod_FromZipFile;
			this._CompressedFileDataSize = source._CompressedFileDataSize;
			this._UncompressedSize = source._UncompressedSize;
			this._BitField = source._BitField;
			this._Source = source._Source;
			this._LastModified = source._LastModified;
			this._Mtime = source._Mtime;
			this._Atime = source._Atime;
			this._Ctime = source._Ctime;
			this._ntfsTimesAreSet = source._ntfsTimesAreSet;
			this._emitUnixTimes = source._emitUnixTimes;
			this._emitNtfsTimes = source._emitNtfsTimes;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000097ED File Offset: 0x000079ED
		private void OnWriteBlock(long bytesXferred, long totalBytesToXfer)
		{
			if (this._container.ZipFile != null)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnSaveBlock(this, bytesXferred, totalBytesToXfer);
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00009818 File Offset: 0x00007A18
		private void _WriteEntryData(Stream s)
		{
			Stream stream = null;
			long num = -1L;
			try
			{
				num = s.Position;
			}
			catch (Exception)
			{
			}
			try
			{
				long num2 = this.SetInputAndFigureFileLength(ref stream);
				CountingStream countingStream = new CountingStream(s);
				Stream stream2;
				Stream stream3;
				if (num2 != 0L)
				{
					stream2 = this.MaybeApplyEncryption(countingStream);
					stream3 = this.MaybeApplyCompression(stream2, num2);
				}
				else
				{
					stream3 = (stream2 = countingStream);
				}
				CrcCalculatorStream crcCalculatorStream = new CrcCalculatorStream(stream3, true);
				if (this._Source == ZipEntrySource.WriteDelegate)
				{
					this._WriteDelegate(this.FileName, crcCalculatorStream);
				}
				else
				{
					byte[] array = new byte[this.BufferSize];
					int num3;
					while ((num3 = SharedUtilities.ReadWithRetry(stream, array, 0, array.Length, this.FileName)) != 0)
					{
						crcCalculatorStream.Write(array, 0, num3);
						this.OnWriteBlock(crcCalculatorStream.TotalBytesSlurped, num2);
						if (this._ioOperationCanceled)
						{
							break;
						}
					}
				}
				this.FinishOutputStream(s, countingStream, stream2, stream3, crcCalculatorStream);
			}
			finally
			{
				if (this._Source == ZipEntrySource.JitStream)
				{
					if (this._CloseDelegate != null)
					{
						this._CloseDelegate(this.FileName, stream);
					}
				}
				else if (stream is FileStream)
				{
					stream.Dispose();
				}
			}
			if (this._ioOperationCanceled)
			{
				return;
			}
			this.__FileDataPosition = num;
			this.PostProcessOutput(s);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00009950 File Offset: 0x00007B50
		private long SetInputAndFigureFileLength(ref Stream input)
		{
			long num = -1L;
			if (this._Source == ZipEntrySource.Stream)
			{
				this.PrepSourceStream();
				input = this._sourceStream;
				try
				{
					return this._sourceStream.Length;
				}
				catch (NotSupportedException)
				{
					return num;
				}
			}
			if (this._Source == ZipEntrySource.ZipFile)
			{
				string text = ((this._Encryption_FromZipFile == EncryptionAlgorithm.None) ? null : (this._Password ?? this._container.Password));
				this._sourceStream = this.InternalOpenReader(text);
				this.PrepSourceStream();
				input = this._sourceStream;
				num = this._sourceStream.Length;
			}
			else
			{
				if (this._Source == ZipEntrySource.JitStream)
				{
					if (this._sourceStream == null)
					{
						this._sourceStream = this._OpenDelegate(this.FileName);
					}
					this.PrepSourceStream();
					input = this._sourceStream;
					try
					{
						return this._sourceStream.Length;
					}
					catch (NotSupportedException)
					{
						return num;
					}
				}
				if (this._Source == ZipEntrySource.FileSystem)
				{
					FileShare fileShare = 3;
					fileShare |= 4;
					input = File.Open(this.LocalFileName, 3, 1, fileShare);
					num = input.Length;
				}
			}
			return num;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00009A6C File Offset: 0x00007C6C
		internal void FinishOutputStream(Stream s, CountingStream entryCounter, Stream encryptor, Stream compressor, CrcCalculatorStream output)
		{
			if (output == null)
			{
				return;
			}
			output.Close();
			if (compressor is DeflateStream)
			{
				compressor.Close();
			}
			else if (compressor is BZip2OutputStream)
			{
				compressor.Close();
			}
			else if (compressor is ParallelBZip2OutputStream)
			{
				compressor.Close();
			}
			else if (compressor is ParallelDeflateOutputStream)
			{
				compressor.Close();
			}
			encryptor.Flush();
			encryptor.Close();
			this._LengthOfTrailer = 0;
			this._UncompressedSize = output.TotalBytesSlurped;
			WinZipAesCipherStream winZipAesCipherStream = encryptor as WinZipAesCipherStream;
			if (winZipAesCipherStream != null && this._UncompressedSize > 0L)
			{
				s.Write(winZipAesCipherStream.FinalAuthentication, 0, 10);
				this._LengthOfTrailer += 10;
			}
			this._CompressedFileDataSize = entryCounter.BytesWritten;
			this._CompressedSize = this._CompressedFileDataSize;
			this._Crc32 = output.Crc;
			this.StoreRelativeOffset();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00009B48 File Offset: 0x00007D48
		internal void PostProcessOutput(Stream s)
		{
			CountingStream countingStream = s as CountingStream;
			if (this._UncompressedSize == 0L && this._CompressedSize == 0L)
			{
				if (this._Source == ZipEntrySource.ZipOutputStream)
				{
					return;
				}
				if (this._Password != null)
				{
					int num = 0;
					if (this.Encryption == EncryptionAlgorithm.PkzipWeak)
					{
						num = 12;
					}
					else if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
					{
						num = this._aesCrypto_forWrite._Salt.Length + this._aesCrypto_forWrite.GeneratedPV.Length;
					}
					if (this._Source == ZipEntrySource.ZipOutputStream && !s.CanSeek)
					{
						throw new ZipException("Zero bytes written, encryption in use, and non-seekable output.");
					}
					if (this.Encryption != EncryptionAlgorithm.None)
					{
						s.Seek((long)(-1 * num), 1);
						s.SetLength(s.Position);
						if (countingStream != null)
						{
							countingStream.Adjust((long)num);
						}
						this._LengthOfHeader -= num;
						this.__FileDataPosition -= (long)num;
					}
					this._Password = null;
					this._BitField &= -2;
					int num2 = 6;
					this._EntryHeader[num2++] = (byte)(this._BitField & 255);
					this._EntryHeader[num2++] = (byte)(((int)this._BitField & 65280) >> 8);
					if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
					{
						short num3 = (short)((int)this._EntryHeader[26] + (int)this._EntryHeader[27] * 256);
						int num4 = (int)(30 + num3);
						int num5 = ZipEntry.FindExtraFieldSegment(this._EntryHeader, num4, 39169);
						if (num5 >= 0)
						{
							this._EntryHeader[num5++] = 153;
							this._EntryHeader[num5++] = 153;
						}
					}
				}
				this.CompressionMethod = CompressionMethod.None;
				this.Encryption = EncryptionAlgorithm.None;
			}
			else if (this._zipCrypto_forWrite != null || this._aesCrypto_forWrite != null)
			{
				if (this.Encryption == EncryptionAlgorithm.PkzipWeak)
				{
					this._CompressedSize += 12L;
				}
				else if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
				{
					this._CompressedSize += (long)this._aesCrypto_forWrite.SizeOfEncryptionMetadata;
				}
			}
			int num6 = 8;
			this._EntryHeader[num6++] = (byte)(this._CompressionMethod & 255);
			this._EntryHeader[num6++] = (byte)(((int)this._CompressionMethod & 65280) >> 8);
			num6 = 14;
			this._EntryHeader[num6++] = (byte)(this._Crc32 & 255);
			this._EntryHeader[num6++] = (byte)((this._Crc32 & 65280) >> 8);
			this._EntryHeader[num6++] = (byte)((this._Crc32 & 16711680) >> 16);
			this._EntryHeader[num6++] = (byte)(((long)this._Crc32 & (long)((ulong)(-16777216))) >> 24);
			this.SetZip64Flags();
			short num7 = (short)((int)this._EntryHeader[26] + (int)this._EntryHeader[27] * 256);
			short num8 = (short)((int)this._EntryHeader[28] + (int)this._EntryHeader[29] * 256);
			if (this._OutputUsesZip64.Value)
			{
				this._EntryHeader[4] = 45;
				this._EntryHeader[5] = 0;
				for (int i = 0; i < 8; i++)
				{
					this._EntryHeader[num6++] = byte.MaxValue;
				}
				num6 = (int)(30 + num7);
				this._EntryHeader[num6++] = 1;
				this._EntryHeader[num6++] = 0;
				num6 += 2;
				Array.Copy(BitConverter.GetBytes(this._UncompressedSize), 0, this._EntryHeader, num6, 8);
				num6 += 8;
				Array.Copy(BitConverter.GetBytes(this._CompressedSize), 0, this._EntryHeader, num6, 8);
			}
			else
			{
				this._EntryHeader[4] = 20;
				this._EntryHeader[5] = 0;
				num6 = 18;
				this._EntryHeader[num6++] = (byte)(this._CompressedSize & 255L);
				this._EntryHeader[num6++] = (byte)((this._CompressedSize & 65280L) >> 8);
				this._EntryHeader[num6++] = (byte)((this._CompressedSize & 16711680L) >> 16);
				this._EntryHeader[num6++] = (byte)((this._CompressedSize & (long)((ulong)(-16777216))) >> 24);
				this._EntryHeader[num6++] = (byte)(this._UncompressedSize & 255L);
				this._EntryHeader[num6++] = (byte)((this._UncompressedSize & 65280L) >> 8);
				this._EntryHeader[num6++] = (byte)((this._UncompressedSize & 16711680L) >> 16);
				this._EntryHeader[num6++] = (byte)((this._UncompressedSize & (long)((ulong)(-16777216))) >> 24);
				if (num8 != 0)
				{
					num6 = (int)(30 + num7);
					short num9 = (short)((int)this._EntryHeader[num6 + 2] + (int)this._EntryHeader[num6 + 3] * 256);
					if (num9 == 16)
					{
						this._EntryHeader[num6++] = 153;
						this._EntryHeader[num6++] = 153;
					}
				}
			}
			if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				num6 = 8;
				this._EntryHeader[num6++] = 99;
				this._EntryHeader[num6++] = 0;
				num6 = (int)(30 + num7);
				do
				{
					ushort num10 = (ushort)((int)this._EntryHeader[num6] + (int)this._EntryHeader[num6 + 1] * 256);
					short num11 = (short)((int)this._EntryHeader[num6 + 2] + (int)this._EntryHeader[num6 + 3] * 256);
					if (num10 != 39169)
					{
						num6 += (int)(num11 + 4);
					}
					else
					{
						num6 += 9;
						this._EntryHeader[num6++] = (byte)(this._CompressionMethod & 255);
						this._EntryHeader[num6++] = (byte)((int)this._CompressionMethod & 65280);
					}
				}
				while (num6 < (int)(num8 - 30 - num7));
			}
			if ((this._BitField & 8) != 8 || (this._Source == ZipEntrySource.ZipOutputStream && s.CanSeek))
			{
				ZipSegmentedStream zipSegmentedStream = s as ZipSegmentedStream;
				if (zipSegmentedStream != null && this._diskNumber != zipSegmentedStream.CurrentSegment)
				{
					using (Stream stream = ZipSegmentedStream.ForUpdate(this._container.ZipFile.Name, this._diskNumber))
					{
						stream.Seek(this._RelativeOffsetOfLocalHeader, 0);
						stream.Write(this._EntryHeader, 0, this._EntryHeader.Length);
						goto IL_06AA;
					}
				}
				s.Seek(this._RelativeOffsetOfLocalHeader, 0);
				s.Write(this._EntryHeader, 0, this._EntryHeader.Length);
				if (countingStream != null)
				{
					countingStream.Adjust((long)this._EntryHeader.Length);
				}
				s.Seek(this._CompressedSize, 1);
			}
			IL_06AA:
			if ((this._BitField & 8) == 8 && !this.IsDirectory)
			{
				byte[] array = new byte[16 + (this._OutputUsesZip64.Value ? 8 : 0)];
				num6 = 0;
				Array.Copy(BitConverter.GetBytes(134695760), 0, array, num6, 4);
				num6 += 4;
				Array.Copy(BitConverter.GetBytes(this._Crc32), 0, array, num6, 4);
				num6 += 4;
				if (this._OutputUsesZip64.Value)
				{
					Array.Copy(BitConverter.GetBytes(this._CompressedSize), 0, array, num6, 8);
					num6 += 8;
					Array.Copy(BitConverter.GetBytes(this._UncompressedSize), 0, array, num6, 8);
					num6 += 8;
				}
				else
				{
					array[num6++] = (byte)(this._CompressedSize & 255L);
					array[num6++] = (byte)((this._CompressedSize & 65280L) >> 8);
					array[num6++] = (byte)((this._CompressedSize & 16711680L) >> 16);
					array[num6++] = (byte)((this._CompressedSize & (long)((ulong)(-16777216))) >> 24);
					array[num6++] = (byte)(this._UncompressedSize & 255L);
					array[num6++] = (byte)((this._UncompressedSize & 65280L) >> 8);
					array[num6++] = (byte)((this._UncompressedSize & 16711680L) >> 16);
					array[num6++] = (byte)((this._UncompressedSize & (long)((ulong)(-16777216))) >> 24);
				}
				s.Write(array, 0, array.Length);
				this._LengthOfTrailer += array.Length;
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000A3B8 File Offset: 0x000085B8
		private void SetZip64Flags()
		{
			this._entryRequiresZip64 = new bool?(this._CompressedSize >= (long)((ulong)(-1)) || this._UncompressedSize >= (long)((ulong)(-1)) || this._RelativeOffsetOfLocalHeader >= (long)((ulong)(-1)));
			if (this._container.Zip64 == Zip64Option.Default && this._entryRequiresZip64.Value)
			{
				throw new ZipException("Compressed or Uncompressed size, or offset exceeds the maximum value. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
			}
			this._OutputUsesZip64 = new bool?(this._container.Zip64 == Zip64Option.Always || this._entryRequiresZip64.Value);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000A440 File Offset: 0x00008640
		internal void PrepOutputStream(Stream s, long streamLength, out CountingStream outputCounter, out Stream encryptor, out Stream compressor, out CrcCalculatorStream output)
		{
			outputCounter = new CountingStream(s);
			if (streamLength != 0L)
			{
				encryptor = this.MaybeApplyEncryption(outputCounter);
				compressor = this.MaybeApplyCompression(encryptor, streamLength);
			}
			else
			{
				Stream stream;
				compressor = (stream = outputCounter);
				encryptor = stream;
			}
			output = new CrcCalculatorStream(compressor, true);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000A48C File Offset: 0x0000868C
		private Stream MaybeApplyCompression(Stream s, long streamLength)
		{
			if (this._CompressionMethod == 8 && this.CompressionLevel != CompressionLevel.None)
			{
				if (this._container.ParallelDeflateThreshold == 0L || (streamLength > this._container.ParallelDeflateThreshold && this._container.ParallelDeflateThreshold > 0L))
				{
					if (this._container.ParallelDeflater == null)
					{
						this._container.ParallelDeflater = new ParallelDeflateOutputStream(s, this.CompressionLevel, this._container.Strategy, true);
						if (this._container.CodecBufferSize > 0)
						{
							this._container.ParallelDeflater.BufferSize = this._container.CodecBufferSize;
						}
						if (this._container.ParallelDeflateMaxBufferPairs > 0)
						{
							this._container.ParallelDeflater.MaxBufferPairs = this._container.ParallelDeflateMaxBufferPairs;
						}
					}
					ParallelDeflateOutputStream parallelDeflater = this._container.ParallelDeflater;
					parallelDeflater.Reset(s);
					return parallelDeflater;
				}
				DeflateStream deflateStream = new DeflateStream(s, CompressionMode.Compress, this.CompressionLevel, true);
				if (this._container.CodecBufferSize > 0)
				{
					deflateStream.BufferSize = this._container.CodecBufferSize;
				}
				deflateStream.Strategy = this._container.Strategy;
				return deflateStream;
			}
			else
			{
				if (this._CompressionMethod != 12)
				{
					return s;
				}
				if (this._container.ParallelDeflateThreshold == 0L || (streamLength > this._container.ParallelDeflateThreshold && this._container.ParallelDeflateThreshold > 0L))
				{
					return new ParallelBZip2OutputStream(s, true);
				}
				return new BZip2OutputStream(s, true);
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000A605 File Offset: 0x00008805
		private Stream MaybeApplyEncryption(Stream s)
		{
			if (this.Encryption == EncryptionAlgorithm.PkzipWeak)
			{
				return new ZipCipherStream(s, this._zipCrypto_forWrite, CryptoMode.Encrypt);
			}
			if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				return new WinZipAesCipherStream(s, this._aesCrypto_forWrite, CryptoMode.Encrypt);
			}
			return s;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000A63F File Offset: 0x0000883F
		private void OnZipErrorWhileSaving(Exception e)
		{
			if (this._container.ZipFile != null)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnZipErrorSaving(this, e);
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000A668 File Offset: 0x00008868
		internal void Write(Stream s)
		{
			CountingStream countingStream = s as CountingStream;
			ZipSegmentedStream zipSegmentedStream = s as ZipSegmentedStream;
			bool flag = false;
			do
			{
				try
				{
					if (this._Source == ZipEntrySource.ZipFile && !this._restreamRequiredOnSave)
					{
						this.CopyThroughOneEntry(s);
						break;
					}
					if (this.IsDirectory)
					{
						this.WriteHeader(s, 1);
						this.StoreRelativeOffset();
						this._entryRequiresZip64 = new bool?(this._RelativeOffsetOfLocalHeader >= (long)((ulong)(-1)));
						this._OutputUsesZip64 = new bool?(this._container.Zip64 == Zip64Option.Always || this._entryRequiresZip64.Value);
						if (zipSegmentedStream != null)
						{
							this._diskNumber = zipSegmentedStream.CurrentSegment;
						}
						break;
					}
					int num = 0;
					bool flag2;
					do
					{
						num++;
						this.WriteHeader(s, num);
						this.WriteSecurityMetadata(s);
						this._WriteEntryData(s);
						this._TotalEntrySize = (long)this._LengthOfHeader + this._CompressedFileDataSize + (long)this._LengthOfTrailer;
						flag2 = num <= 1 && s.CanSeek && this.WantReadAgain();
						if (flag2)
						{
							if (zipSegmentedStream != null)
							{
								zipSegmentedStream.TruncateBackward(this._diskNumber, this._RelativeOffsetOfLocalHeader);
							}
							else
							{
								s.Seek(this._RelativeOffsetOfLocalHeader, 0);
							}
							s.SetLength(s.Position);
							if (countingStream != null)
							{
								countingStream.Adjust(this._TotalEntrySize);
							}
						}
					}
					while (flag2);
					this._skippedDuringSave = false;
					flag = true;
				}
				catch (Exception ex)
				{
					ZipErrorAction zipErrorAction = this.ZipErrorAction;
					int num2 = 0;
					while (this.ZipErrorAction != ZipErrorAction.Throw)
					{
						if (this.ZipErrorAction == ZipErrorAction.Skip || this.ZipErrorAction == ZipErrorAction.Retry)
						{
							long num3 = ((countingStream != null) ? countingStream.ComputedPosition : s.Position);
							long num4 = num3 - this._future_ROLH;
							if (num4 > 0L)
							{
								s.Seek(num4, 1);
								long position = s.Position;
								s.SetLength(s.Position);
								if (countingStream != null)
								{
									countingStream.Adjust(num3 - position);
								}
							}
							if (this.ZipErrorAction == ZipErrorAction.Skip)
							{
								this.WriteStatus("Skipping file {0} (exception: {1})", new object[]
								{
									this.LocalFileName,
									ex.ToString()
								});
								this._skippedDuringSave = true;
								flag = true;
							}
							else
							{
								this.ZipErrorAction = zipErrorAction;
							}
						}
						else
						{
							if (num2 > 0)
							{
								throw;
							}
							if (this.ZipErrorAction == ZipErrorAction.InvokeErrorEvent)
							{
								this.OnZipErrorWhileSaving(ex);
								if (this._ioOperationCanceled)
								{
									flag = true;
									goto IL_023B;
								}
							}
							num2++;
							continue;
						}
						IL_023B:
						goto IL_023D;
					}
					throw;
				}
				IL_023D:;
			}
			while (!flag);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000A8D4 File Offset: 0x00008AD4
		internal void StoreRelativeOffset()
		{
			this._RelativeOffsetOfLocalHeader = this._future_ROLH;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000A8E2 File Offset: 0x00008AE2
		internal void NotifySaveComplete()
		{
			this._Encryption_FromZipFile = this._Encryption;
			this._CompressionMethod_FromZipFile = this._CompressionMethod;
			this._restreamRequiredOnSave = false;
			this._metadataChanged = false;
			this._Source = ZipEntrySource.ZipFile;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000A914 File Offset: 0x00008B14
		internal void WriteSecurityMetadata(Stream outstream)
		{
			if (this.Encryption == EncryptionAlgorithm.None)
			{
				return;
			}
			string text = this._Password;
			if (this._Source == ZipEntrySource.ZipFile && text == null)
			{
				text = this._container.Password;
			}
			if (text == null)
			{
				this._zipCrypto_forWrite = null;
				this._aesCrypto_forWrite = null;
				return;
			}
			if (this.Encryption == EncryptionAlgorithm.PkzipWeak)
			{
				this._zipCrypto_forWrite = ZipCrypto.ForWrite(text);
				Random random = new Random();
				byte[] array = new byte[12];
				random.NextBytes(array);
				if ((this._BitField & 8) == 8)
				{
					this._TimeBlob = SharedUtilities.DateTimeToPacked(this.LastModified);
					array[11] = (byte)((this._TimeBlob >> 8) & 255);
				}
				else
				{
					this.FigureCrc32();
					array[11] = (byte)((this._Crc32 >> 24) & 255);
				}
				byte[] array2 = this._zipCrypto_forWrite.EncryptMessage(array, array.Length);
				outstream.Write(array2, 0, array2.Length);
				this._LengthOfHeader += array2.Length;
				return;
			}
			if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				int keyStrengthInBits = ZipEntry.GetKeyStrengthInBits(this.Encryption);
				this._aesCrypto_forWrite = WinZipAesCrypto.Generate(text, keyStrengthInBits);
				outstream.Write(this._aesCrypto_forWrite.Salt, 0, this._aesCrypto_forWrite._Salt.Length);
				outstream.Write(this._aesCrypto_forWrite.GeneratedPV, 0, this._aesCrypto_forWrite.GeneratedPV.Length);
				this._LengthOfHeader += this._aesCrypto_forWrite._Salt.Length + this._aesCrypto_forWrite.GeneratedPV.Length;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000AA98 File Offset: 0x00008C98
		private void CopyThroughOneEntry(Stream outStream)
		{
			if (this.LengthOfHeader == 0)
			{
				throw new BadStateException("Bad header length.");
			}
			bool flag = this._metadataChanged || this.ArchiveStream is ZipSegmentedStream || outStream is ZipSegmentedStream || (this._InputUsesZip64 && this._container.UseZip64WhenSaving == Zip64Option.Default) || (!this._InputUsesZip64 && this._container.UseZip64WhenSaving == Zip64Option.Always);
			if (flag)
			{
				this.CopyThroughWithRecompute(outStream);
			}
			else
			{
				this.CopyThroughWithNoChange(outStream);
			}
			this._entryRequiresZip64 = new bool?(this._CompressedSize >= (long)((ulong)(-1)) || this._UncompressedSize >= (long)((ulong)(-1)) || this._RelativeOffsetOfLocalHeader >= (long)((ulong)(-1)));
			this._OutputUsesZip64 = new bool?(this._container.Zip64 == Zip64Option.Always || this._entryRequiresZip64.Value);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000AB70 File Offset: 0x00008D70
		private void CopyThroughWithRecompute(Stream outstream)
		{
			byte[] array = new byte[this.BufferSize];
			CountingStream countingStream = new CountingStream(this.ArchiveStream);
			long relativeOffsetOfLocalHeader = this._RelativeOffsetOfLocalHeader;
			int lengthOfHeader = this.LengthOfHeader;
			this.WriteHeader(outstream, 0);
			this.StoreRelativeOffset();
			if (!this.FileName.EndsWith("/"))
			{
				long num = relativeOffsetOfLocalHeader + (long)lengthOfHeader;
				int num2 = ZipEntry.GetLengthOfCryptoHeaderBytes(this._Encryption_FromZipFile);
				num -= (long)num2;
				this._LengthOfHeader += num2;
				countingStream.Seek(num, 0);
				long num3 = this._CompressedSize;
				while (num3 > 0L)
				{
					num2 = ((num3 > (long)array.Length) ? array.Length : ((int)num3));
					int num4 = countingStream.Read(array, 0, num2);
					outstream.Write(array, 0, num4);
					num3 -= (long)num4;
					this.OnWriteBlock(countingStream.BytesRead, this._CompressedSize);
					if (this._ioOperationCanceled)
					{
						break;
					}
				}
				if ((this._BitField & 8) == 8)
				{
					int num5 = 16;
					if (this._InputUsesZip64)
					{
						num5 += 8;
					}
					byte[] array2 = new byte[num5];
					countingStream.Read(array2, 0, num5);
					if (this._InputUsesZip64 && this._container.UseZip64WhenSaving == Zip64Option.Default)
					{
						outstream.Write(array2, 0, 8);
						if (this._CompressedSize > (long)((ulong)(-1)))
						{
							throw new InvalidOperationException("ZIP64 is required");
						}
						outstream.Write(array2, 8, 4);
						if (this._UncompressedSize > (long)((ulong)(-1)))
						{
							throw new InvalidOperationException("ZIP64 is required");
						}
						outstream.Write(array2, 16, 4);
						this._LengthOfTrailer -= 8;
					}
					else if (!this._InputUsesZip64 && this._container.UseZip64WhenSaving == Zip64Option.Always)
					{
						byte[] array3 = new byte[4];
						outstream.Write(array2, 0, 8);
						outstream.Write(array2, 8, 4);
						outstream.Write(array3, 0, 4);
						outstream.Write(array2, 12, 4);
						outstream.Write(array3, 0, 4);
						this._LengthOfTrailer += 8;
					}
					else
					{
						outstream.Write(array2, 0, num5);
					}
				}
			}
			this._TotalEntrySize = (long)this._LengthOfHeader + this._CompressedFileDataSize + (long)this._LengthOfTrailer;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000AD80 File Offset: 0x00008F80
		private void CopyThroughWithNoChange(Stream outstream)
		{
			byte[] array = new byte[this.BufferSize];
			CountingStream countingStream = new CountingStream(this.ArchiveStream);
			countingStream.Seek(this._RelativeOffsetOfLocalHeader, 0);
			if (this._TotalEntrySize == 0L)
			{
				this._TotalEntrySize = (long)this._LengthOfHeader + this._CompressedFileDataSize + (long)this._LengthOfTrailer;
			}
			CountingStream countingStream2 = outstream as CountingStream;
			this._RelativeOffsetOfLocalHeader = ((countingStream2 != null) ? countingStream2.ComputedPosition : outstream.Position);
			long num = this._TotalEntrySize;
			while (num > 0L)
			{
				int num2 = ((num > (long)array.Length) ? array.Length : ((int)num));
				int num3 = countingStream.Read(array, 0, num2);
				outstream.Write(array, 0, num3);
				num -= (long)num3;
				this.OnWriteBlock(countingStream.BytesRead, this._TotalEntrySize);
				if (this._ioOperationCanceled)
				{
					return;
				}
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000AE50 File Offset: 0x00009050
		[Conditional("Trace")]
		private void TraceWriteLine(string format, params object[] varParams)
		{
			lock (this._outputLock)
			{
				int hashCode = Thread.CurrentThread.GetHashCode();
				Console.Write("{0:000} ZipEntry.Write ", hashCode);
				Console.WriteLine(format, varParams);
			}
		}

		// Token: 0x04000097 RID: 151
		private short _VersionMadeBy;

		// Token: 0x04000098 RID: 152
		private short _InternalFileAttrs;

		// Token: 0x04000099 RID: 153
		private int _ExternalFileAttrs;

		// Token: 0x0400009A RID: 154
		private short _filenameLength;

		// Token: 0x0400009B RID: 155
		private short _extraFieldLength;

		// Token: 0x0400009C RID: 156
		private short _commentLength;

		// Token: 0x0400009D RID: 157
		private ZipCrypto _zipCrypto_forExtract;

		// Token: 0x0400009E RID: 158
		private ZipCrypto _zipCrypto_forWrite;

		// Token: 0x0400009F RID: 159
		private WinZipAesCrypto _aesCrypto_forExtract;

		// Token: 0x040000A0 RID: 160
		private WinZipAesCrypto _aesCrypto_forWrite;

		// Token: 0x040000A1 RID: 161
		private short _WinZipAesMethod;

		// Token: 0x040000A2 RID: 162
		internal DateTime _LastModified;

		// Token: 0x040000A3 RID: 163
		private DateTime _Mtime;

		// Token: 0x040000A4 RID: 164
		private DateTime _Atime;

		// Token: 0x040000A5 RID: 165
		private DateTime _Ctime;

		// Token: 0x040000A6 RID: 166
		private bool _ntfsTimesAreSet;

		// Token: 0x040000A7 RID: 167
		private bool _emitNtfsTimes = true;

		// Token: 0x040000A8 RID: 168
		private bool _emitUnixTimes;

		// Token: 0x040000A9 RID: 169
		private bool _TrimVolumeFromFullyQualifiedPaths = true;

		// Token: 0x040000AA RID: 170
		internal string _LocalFileName;

		// Token: 0x040000AB RID: 171
		private string _FileNameInArchive;

		// Token: 0x040000AC RID: 172
		internal short _VersionNeeded;

		// Token: 0x040000AD RID: 173
		internal short _BitField;

		// Token: 0x040000AE RID: 174
		internal short _CompressionMethod;

		// Token: 0x040000AF RID: 175
		private short _CompressionMethod_FromZipFile;

		// Token: 0x040000B0 RID: 176
		private CompressionLevel _CompressionLevel;

		// Token: 0x040000B1 RID: 177
		internal string _Comment;

		// Token: 0x040000B2 RID: 178
		private bool _IsDirectory;

		// Token: 0x040000B3 RID: 179
		private byte[] _CommentBytes;

		// Token: 0x040000B4 RID: 180
		internal long _CompressedSize;

		// Token: 0x040000B5 RID: 181
		internal long _CompressedFileDataSize;

		// Token: 0x040000B6 RID: 182
		internal long _UncompressedSize;

		// Token: 0x040000B7 RID: 183
		internal int _TimeBlob;

		// Token: 0x040000B8 RID: 184
		private bool _crcCalculated;

		// Token: 0x040000B9 RID: 185
		internal int _Crc32;

		// Token: 0x040000BA RID: 186
		internal byte[] _Extra;

		// Token: 0x040000BB RID: 187
		private bool _metadataChanged;

		// Token: 0x040000BC RID: 188
		private bool _restreamRequiredOnSave;

		// Token: 0x040000BD RID: 189
		private bool _sourceIsEncrypted;

		// Token: 0x040000BE RID: 190
		private bool _skippedDuringSave;

		// Token: 0x040000BF RID: 191
		private uint _diskNumber;

		// Token: 0x040000C0 RID: 192
		private static Encoding ibm437 = Encoding.UTF8;

		// Token: 0x040000C1 RID: 193
		private Encoding _actualEncoding;

		// Token: 0x040000C2 RID: 194
		internal ZipContainer _container;

		// Token: 0x040000C3 RID: 195
		private long __FileDataPosition = -1L;

		// Token: 0x040000C4 RID: 196
		private byte[] _EntryHeader;

		// Token: 0x040000C5 RID: 197
		internal long _RelativeOffsetOfLocalHeader;

		// Token: 0x040000C6 RID: 198
		private long _future_ROLH;

		// Token: 0x040000C7 RID: 199
		private long _TotalEntrySize;

		// Token: 0x040000C8 RID: 200
		private int _LengthOfHeader;

		// Token: 0x040000C9 RID: 201
		private int _LengthOfTrailer;

		// Token: 0x040000CA RID: 202
		internal bool _InputUsesZip64;

		// Token: 0x040000CB RID: 203
		private uint _UnsupportedAlgorithmId;

		// Token: 0x040000CC RID: 204
		internal string _Password;

		// Token: 0x040000CD RID: 205
		internal ZipEntrySource _Source;

		// Token: 0x040000CE RID: 206
		internal EncryptionAlgorithm _Encryption;

		// Token: 0x040000CF RID: 207
		internal EncryptionAlgorithm _Encryption_FromZipFile;

		// Token: 0x040000D0 RID: 208
		internal byte[] _WeakEncryptionHeader;

		// Token: 0x040000D1 RID: 209
		internal Stream _archiveStream;

		// Token: 0x040000D2 RID: 210
		private Stream _sourceStream;

		// Token: 0x040000D3 RID: 211
		private long? _sourceStreamOriginalPosition;

		// Token: 0x040000D4 RID: 212
		private bool _sourceWasJitProvided;

		// Token: 0x040000D5 RID: 213
		private bool _ioOperationCanceled;

		// Token: 0x040000D6 RID: 214
		private bool _presumeZip64;

		// Token: 0x040000D7 RID: 215
		private bool? _entryRequiresZip64;

		// Token: 0x040000D8 RID: 216
		private bool? _OutputUsesZip64;

		// Token: 0x040000D9 RID: 217
		private bool _IsText;

		// Token: 0x040000DA RID: 218
		private ZipEntryTimestamp _timestamp;

		// Token: 0x040000DB RID: 219
		private static DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 1);

		// Token: 0x040000DC RID: 220
		private static DateTime _win32Epoch = DateTime.FromFileTimeUtc(0L);

		// Token: 0x040000DD RID: 221
		private static DateTime _zeroHour = new DateTime(1, 1, 1, 0, 0, 0, 1);

		// Token: 0x040000DE RID: 222
		private WriteDelegate _WriteDelegate;

		// Token: 0x040000DF RID: 223
		private OpenDelegate _OpenDelegate;

		// Token: 0x040000E0 RID: 224
		private CloseDelegate _CloseDelegate;

		// Token: 0x040000E1 RID: 225
		private Stream _inputDecryptorStream;

		// Token: 0x040000E2 RID: 226
		private int _readExtraDepth;

		// Token: 0x040000E3 RID: 227
		private object _outputLock = new object();

		// Token: 0x0200002D RID: 45
		private class CopyHelper
		{
			// Token: 0x060001A0 RID: 416 RVA: 0x0000AEE8 File Offset: 0x000090E8
			internal static string AppendCopyToFileName(string f)
			{
				ZipEntry.CopyHelper.callCount++;
				if (ZipEntry.CopyHelper.callCount > 25)
				{
					throw new OverflowException("overflow while creating filename");
				}
				int num = 1;
				int num2 = f.LastIndexOf(".");
				if (num2 == -1)
				{
					Match match = ZipEntry.CopyHelper.re.Match(f);
					if (match.Success)
					{
						num = int.Parse(match.Groups[1].Value) + 1;
						string text = string.Format(" (copy {0})", num);
						f = f.Substring(0, match.Index) + text;
					}
					else
					{
						string text2 = string.Format(" (copy {0})", num);
						f += text2;
					}
				}
				else
				{
					Match match2 = ZipEntry.CopyHelper.re.Match(f.Substring(0, num2));
					if (match2.Success)
					{
						num = int.Parse(match2.Groups[1].Value) + 1;
						string text3 = string.Format(" (copy {0})", num);
						f = f.Substring(0, match2.Index) + text3 + f.Substring(num2);
					}
					else
					{
						string text4 = string.Format(" (copy {0})", num);
						f = f.Substring(0, num2) + text4 + f.Substring(num2);
					}
				}
				return f;
			}

			// Token: 0x040000EA RID: 234
			private static Regex re = new Regex(" \\(copy (\\d+)\\)$");

			// Token: 0x040000EB RID: 235
			private static int callCount = 0;
		}

		// Token: 0x0200002E RID: 46
		// (Invoke) Token: 0x060001A4 RID: 420
		private delegate T Func<T>();
	}
}
