using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ionic.Crc;
using Ionic.Zlib;

namespace Ionic.Zip
{
	// Token: 0x0200003D RID: 61
	public class ZipOutputStream : Stream
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x00010232 File Offset: 0x0000E432
		public ZipOutputStream(Stream stream)
			: this(stream, false)
		{
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0001023C File Offset: 0x0000E43C
		public ZipOutputStream(string fileName)
		{
			this._alternateEncoding = Encoding.UTF8;
			this._maxBufferPairs = 16;
			base..ctor();
			Stream stream = File.Open(fileName, 2, 3, 0);
			this._Init(stream, false, fileName);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00010275 File Offset: 0x0000E475
		public ZipOutputStream(Stream stream, bool leaveOpen)
		{
			this._alternateEncoding = Encoding.UTF8;
			this._maxBufferPairs = 16;
			base..ctor();
			this._Init(stream, leaveOpen, null);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0001029C File Offset: 0x0000E49C
		private void _Init(Stream stream, bool leaveOpen, string name)
		{
			this._outputStream = (stream.CanRead ? stream : new CountingStream(stream));
			this.CompressionLevel = CompressionLevel.Default;
			this.CompressionMethod = CompressionMethod.Deflate;
			this._encryption = EncryptionAlgorithm.None;
			this._entriesWritten = new Dictionary<string, ZipEntry>(StringComparer.Ordinal);
			this._zip64 = Zip64Option.Default;
			this._leaveUnderlyingStreamOpen = leaveOpen;
			this.Strategy = CompressionStrategy.Default;
			this._name = name ?? "(stream)";
			this.ParallelDeflateThreshold = -1L;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00010312 File Offset: 0x0000E512
		public override string ToString()
		{
			return string.Format("ZipOutputStream::{0}(leaveOpen({1})))", this._name, this._leaveUnderlyingStreamOpen);
		}

		// Token: 0x170000AC RID: 172
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00010330 File Offset: 0x0000E530
		public string Password
		{
			set
			{
				if (this._disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._password = value;
				if (this._password == null)
				{
					this._encryption = EncryptionAlgorithm.None;
					return;
				}
				if (this._encryption == EncryptionAlgorithm.None)
				{
					this._encryption = EncryptionAlgorithm.PkzipWeak;
				}
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0001037D File Offset: 0x0000E57D
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00010385 File Offset: 0x0000E585
		public EncryptionAlgorithm Encryption
		{
			get
			{
				return this._encryption;
			}
			set
			{
				if (this._disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				if (value == EncryptionAlgorithm.Unsupported)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("You may not set Encryption to that value.");
				}
				this._encryption = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002CE RID: 718 RVA: 0x000103BE File Offset: 0x0000E5BE
		// (set) Token: 0x060002CF RID: 719 RVA: 0x000103C6 File Offset: 0x0000E5C6
		public int CodecBufferSize { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x000103CF File Offset: 0x0000E5CF
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x000103D7 File Offset: 0x0000E5D7
		public CompressionStrategy Strategy { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x000103E0 File Offset: 0x0000E5E0
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x000103E8 File Offset: 0x0000E5E8
		public ZipEntryTimestamp Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if (this._disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._timestamp = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0001040B File Offset: 0x0000E60B
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x00010413 File Offset: 0x0000E613
		public CompressionLevel CompressionLevel { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0001041C File Offset: 0x0000E61C
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x00010424 File Offset: 0x0000E624
		public CompressionMethod CompressionMethod { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0001042D File Offset: 0x0000E62D
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x00010435 File Offset: 0x0000E635
		public string Comment
		{
			get
			{
				return this._comment;
			}
			set
			{
				if (this._disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._comment = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00010458 File Offset: 0x0000E658
		// (set) Token: 0x060002DB RID: 731 RVA: 0x00010460 File Offset: 0x0000E660
		public Zip64Option EnableZip64
		{
			get
			{
				return this._zip64;
			}
			set
			{
				if (this._disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._zip64 = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00010483 File Offset: 0x0000E683
		public bool OutputUsedZip64
		{
			get
			{
				return this._anyEntriesUsedZip64 || this._directoryNeededZip64;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00010495 File Offset: 0x0000E695
		// (set) Token: 0x060002DE RID: 734 RVA: 0x000104A0 File Offset: 0x0000E6A0
		public bool IgnoreCase
		{
			get
			{
				return !this._DontIgnoreCase;
			}
			set
			{
				this._DontIgnoreCase = !value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002DF RID: 735 RVA: 0x000104AC File Offset: 0x0000E6AC
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x000104C6 File Offset: 0x0000E6C6
		[Obsolete("Beginning with v1.9.1.6 of DotNetZip, this property is obsolete. It will be removed in a future version of the library. Use AlternateEncoding and AlternateEncodingUsage instead.")]
		public bool UseUnicodeAsNecessary
		{
			get
			{
				return this._alternateEncoding == Encoding.UTF8 && this.AlternateEncodingUsage == ZipOption.AsNecessary;
			}
			set
			{
				if (value)
				{
					this._alternateEncoding = Encoding.UTF8;
					this._alternateEncodingUsage = ZipOption.AsNecessary;
					return;
				}
				this._alternateEncoding = ZipOutputStream.DefaultEncoding;
				this._alternateEncodingUsage = ZipOption.Default;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x000104F0 File Offset: 0x0000E6F0
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x00010503 File Offset: 0x0000E703
		[Obsolete("use AlternateEncoding and AlternateEncodingUsage instead.")]
		public Encoding ProvisionalAlternateEncoding
		{
			get
			{
				if (this._alternateEncodingUsage == ZipOption.AsNecessary)
				{
					return this._alternateEncoding;
				}
				return null;
			}
			set
			{
				this._alternateEncoding = value;
				this._alternateEncodingUsage = ZipOption.AsNecessary;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00010513 File Offset: 0x0000E713
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0001051B File Offset: 0x0000E71B
		public Encoding AlternateEncoding
		{
			get
			{
				return this._alternateEncoding;
			}
			set
			{
				this._alternateEncoding = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00010524 File Offset: 0x0000E724
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x0001052C File Offset: 0x0000E72C
		public ZipOption AlternateEncodingUsage
		{
			get
			{
				return this._alternateEncodingUsage;
			}
			set
			{
				this._alternateEncodingUsage = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00010535 File Offset: 0x0000E735
		public static Encoding DefaultEncoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00010563 File Offset: 0x0000E763
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x0001053C File Offset: 0x0000E73C
		public long ParallelDeflateThreshold
		{
			get
			{
				return this._ParallelDeflateThreshold;
			}
			set
			{
				if (value != 0L && value != -1L && value < 65536L)
				{
					throw new ArgumentOutOfRangeException("value must be greater than 64k, or 0, or -1");
				}
				this._ParallelDeflateThreshold = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0001056B File Offset: 0x0000E76B
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00010573 File Offset: 0x0000E773
		public int ParallelDeflateMaxBufferPairs
		{
			get
			{
				return this._maxBufferPairs;
			}
			set
			{
				if (value < 4)
				{
					throw new ArgumentOutOfRangeException("ParallelDeflateMaxBufferPairs", "Value must be 4 or greater.");
				}
				this._maxBufferPairs = value;
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00010590 File Offset: 0x0000E790
		private void InsureUniqueEntry(ZipEntry ze1)
		{
			if (this._entriesWritten.ContainsKey(ze1.FileName))
			{
				this._exceptionPending = true;
				throw new ArgumentException(string.Format("The entry '{0}' already exists in the zip archive.", ze1.FileName));
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000105C2 File Offset: 0x0000E7C2
		internal Stream OutputStream
		{
			get
			{
				return this._outputStream;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002EE RID: 750 RVA: 0x000105CA File Offset: 0x0000E7CA
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000105D2 File Offset: 0x0000E7D2
		public bool ContainsEntry(string name)
		{
			return this._entriesWritten.ContainsKey(SharedUtilities.NormalizePathForUseInZipFile(name));
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000105E8 File Offset: 0x0000E7E8
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			if (buffer == null)
			{
				this._exceptionPending = true;
				throw new ArgumentNullException("buffer");
			}
			if (this._currentEntry == null)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("You must call PutNextEntry() before calling Write().");
			}
			if (this._currentEntry.IsDirectory)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("You cannot Write() data for an entry that is a directory.");
			}
			if (this._needToWriteEntryHeader)
			{
				this._InitiateCurrentEntry(false);
			}
			if (count != 0)
			{
				this._entryOutputStream.Write(buffer, offset, count);
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00010680 File Offset: 0x0000E880
		public ZipEntry PutNextEntry(string entryName)
		{
			if (string.IsNullOrEmpty(entryName))
			{
				throw new ArgumentNullException("entryName");
			}
			if (this._disposed)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			this._FinishCurrentEntry();
			this._currentEntry = ZipEntry.CreateForZipOutputStream(entryName);
			this._currentEntry._container = new ZipContainer(this);
			ZipEntry currentEntry = this._currentEntry;
			currentEntry._BitField |= 8;
			this._currentEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			this._currentEntry.CompressionLevel = this.CompressionLevel;
			this._currentEntry.CompressionMethod = this.CompressionMethod;
			this._currentEntry.Password = this._password;
			this._currentEntry.Encryption = this.Encryption;
			this._currentEntry.AlternateEncoding = this.AlternateEncoding;
			this._currentEntry.AlternateEncodingUsage = this.AlternateEncodingUsage;
			if (entryName.EndsWith("/"))
			{
				this._currentEntry.MarkAsDirectory();
			}
			this._currentEntry.EmitTimesInWindowsFormatWhenSaving = (this._timestamp & ZipEntryTimestamp.Windows) != ZipEntryTimestamp.None;
			this._currentEntry.EmitTimesInUnixFormatWhenSaving = (this._timestamp & ZipEntryTimestamp.Unix) != ZipEntryTimestamp.None;
			this.InsureUniqueEntry(this._currentEntry);
			this._needToWriteEntryHeader = true;
			return this._currentEntry;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000107D4 File Offset: 0x0000E9D4
		private void _InitiateCurrentEntry(bool finishing)
		{
			this._entriesWritten.Add(this._currentEntry.FileName, this._currentEntry);
			this._entryCount++;
			if (this._entryCount > 65534 && this._zip64 == Zip64Option.Default)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("Too many entries. Consider setting ZipOutputStream.EnableZip64.");
			}
			this._currentEntry.WriteHeader(this._outputStream, finishing ? 99 : 0);
			this._currentEntry.StoreRelativeOffset();
			if (!this._currentEntry.IsDirectory)
			{
				this._currentEntry.WriteSecurityMetadata(this._outputStream);
				this._currentEntry.PrepOutputStream(this._outputStream, finishing ? 0L : (-1L), out this._outputCounter, out this._encryptor, out this._deflater, out this._entryOutputStream);
			}
			this._needToWriteEntryHeader = false;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000108AC File Offset: 0x0000EAAC
		private void _FinishCurrentEntry()
		{
			if (this._currentEntry != null)
			{
				if (this._needToWriteEntryHeader)
				{
					this._InitiateCurrentEntry(true);
				}
				this._currentEntry.FinishOutputStream(this._outputStream, this._outputCounter, this._encryptor, this._deflater, this._entryOutputStream);
				this._currentEntry.PostProcessOutput(this._outputStream);
				if (this._currentEntry.OutputUsedZip64 != null)
				{
					this._anyEntriesUsedZip64 |= this._currentEntry.OutputUsedZip64.Value;
				}
				this._outputCounter = null;
				this._encryptor = (this._deflater = null);
				this._entryOutputStream = null;
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00010960 File Offset: 0x0000EB60
		protected override void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing && !this._exceptionPending)
			{
				this._FinishCurrentEntry();
				this._directoryNeededZip64 = ZipOutput.WriteCentralDirectoryStructure(this._outputStream, this._entriesWritten.Values, 1U, this._zip64, this.Comment, new ZipContainer(this));
				CountingStream countingStream = this._outputStream as CountingStream;
				Stream stream;
				if (countingStream != null)
				{
					stream = countingStream.WrappedStream;
					countingStream.Dispose();
				}
				else
				{
					stream = this._outputStream;
				}
				if (!this._leaveUnderlyingStreamOpen)
				{
					stream.Dispose();
				}
				this._outputStream = null;
			}
			this._disposed = true;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x000109F9 File Offset: 0x0000EBF9
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x000109FC File Offset: 0x0000EBFC
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x000109FF File Offset: 0x0000EBFF
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00010A02 File Offset: 0x0000EC02
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00010A09 File Offset: 0x0000EC09
		// (set) Token: 0x060002FA RID: 762 RVA: 0x00010A16 File Offset: 0x0000EC16
		public override long Position
		{
			get
			{
				return this._outputStream.Position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00010A1D File Offset: 0x0000EC1D
		public override void Flush()
		{
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00010A1F File Offset: 0x0000EC1F
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("Read");
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00010A2B File Offset: 0x0000EC2B
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("Seek");
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00010A37 File Offset: 0x0000EC37
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000177 RID: 375
		private EncryptionAlgorithm _encryption;

		// Token: 0x04000178 RID: 376
		private ZipEntryTimestamp _timestamp;

		// Token: 0x04000179 RID: 377
		internal string _password;

		// Token: 0x0400017A RID: 378
		private string _comment;

		// Token: 0x0400017B RID: 379
		private Stream _outputStream;

		// Token: 0x0400017C RID: 380
		private ZipEntry _currentEntry;

		// Token: 0x0400017D RID: 381
		internal Zip64Option _zip64;

		// Token: 0x0400017E RID: 382
		private Dictionary<string, ZipEntry> _entriesWritten;

		// Token: 0x0400017F RID: 383
		private int _entryCount;

		// Token: 0x04000180 RID: 384
		private ZipOption _alternateEncodingUsage;

		// Token: 0x04000181 RID: 385
		private Encoding _alternateEncoding;

		// Token: 0x04000182 RID: 386
		private bool _leaveUnderlyingStreamOpen;

		// Token: 0x04000183 RID: 387
		private bool _disposed;

		// Token: 0x04000184 RID: 388
		private bool _exceptionPending;

		// Token: 0x04000185 RID: 389
		private bool _anyEntriesUsedZip64;

		// Token: 0x04000186 RID: 390
		private bool _directoryNeededZip64;

		// Token: 0x04000187 RID: 391
		private CountingStream _outputCounter;

		// Token: 0x04000188 RID: 392
		private Stream _encryptor;

		// Token: 0x04000189 RID: 393
		private Stream _deflater;

		// Token: 0x0400018A RID: 394
		private CrcCalculatorStream _entryOutputStream;

		// Token: 0x0400018B RID: 395
		private bool _needToWriteEntryHeader;

		// Token: 0x0400018C RID: 396
		private string _name;

		// Token: 0x0400018D RID: 397
		private bool _DontIgnoreCase;

		// Token: 0x0400018E RID: 398
		internal ParallelDeflateOutputStream ParallelDeflater;

		// Token: 0x0400018F RID: 399
		private long _ParallelDeflateThreshold;

		// Token: 0x04000190 RID: 400
		private int _maxBufferPairs;
	}
}
