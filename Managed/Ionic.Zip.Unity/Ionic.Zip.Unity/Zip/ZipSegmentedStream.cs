using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x0200003F RID: 63
	internal class ZipSegmentedStream : Stream
	{
		// Token: 0x06000311 RID: 785 RVA: 0x00010CD7 File Offset: 0x0000EED7
		private ZipSegmentedStream()
		{
			this._exceptionPending = false;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00010CE8 File Offset: 0x0000EEE8
		public static ZipSegmentedStream ForReading(string name, uint initialDiskNumber, uint maxDiskNumber)
		{
			ZipSegmentedStream zipSegmentedStream = new ZipSegmentedStream
			{
				rwMode = ZipSegmentedStream.RwMode.ReadOnly,
				CurrentSegment = initialDiskNumber,
				_maxDiskNumber = maxDiskNumber,
				_baseName = name
			};
			zipSegmentedStream._SetReadStream();
			return zipSegmentedStream;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00010D20 File Offset: 0x0000EF20
		public static ZipSegmentedStream ForWriting(string name, int maxSegmentSize)
		{
			ZipSegmentedStream zipSegmentedStream = new ZipSegmentedStream
			{
				rwMode = ZipSegmentedStream.RwMode.Write,
				CurrentSegment = 0U,
				_baseName = name,
				_maxSegmentSize = maxSegmentSize,
				_baseDir = Path.GetDirectoryName(name)
			};
			if (zipSegmentedStream._baseDir == "")
			{
				zipSegmentedStream._baseDir = ".";
			}
			zipSegmentedStream._SetWriteStream(0U);
			return zipSegmentedStream;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00010D84 File Offset: 0x0000EF84
		public static Stream ForUpdate(string name, uint diskNumber)
		{
			if (diskNumber >= 99U)
			{
				throw new ArgumentOutOfRangeException("diskNumber");
			}
			string text = string.Format("{0}.z{1:D2}", Path.Combine(Path.GetDirectoryName(name), Path.GetFileNameWithoutExtension(name)), diskNumber + 1U);
			return File.Open(text, 3, 3, 0);
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00010DCE File Offset: 0x0000EFCE
		// (set) Token: 0x06000316 RID: 790 RVA: 0x00010DD6 File Offset: 0x0000EFD6
		public bool ContiguousWrite { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00010DDF File Offset: 0x0000EFDF
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00010DE7 File Offset: 0x0000EFE7
		public uint CurrentSegment
		{
			get
			{
				return this._currentDiskNumber;
			}
			private set
			{
				this._currentDiskNumber = value;
				this._currentName = null;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00010DF7 File Offset: 0x0000EFF7
		public string CurrentName
		{
			get
			{
				if (this._currentName == null)
				{
					this._currentName = this._NameForSegment(this.CurrentSegment);
				}
				return this._currentName;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00010E19 File Offset: 0x0000F019
		public string CurrentTempName
		{
			get
			{
				return this._currentTempName;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00010E24 File Offset: 0x0000F024
		private string _NameForSegment(uint diskNumber)
		{
			if (diskNumber >= 99U)
			{
				this._exceptionPending = true;
				throw new OverflowException("The number of zip segments would exceed 99.");
			}
			return string.Format("{0}.z{1:D2}", Path.Combine(Path.GetDirectoryName(this._baseName), Path.GetFileNameWithoutExtension(this._baseName)), diskNumber + 1U);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00010E75 File Offset: 0x0000F075
		public uint ComputeSegment(int length)
		{
			if (this._innerStream.Position + (long)length > (long)this._maxSegmentSize)
			{
				return this.CurrentSegment + 1U;
			}
			return this.CurrentSegment;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00010EA0 File Offset: 0x0000F0A0
		public override string ToString()
		{
			return string.Format("{0}[{1}][{2}], pos=0x{3:X})", new object[]
			{
				"ZipSegmentedStream",
				this.CurrentName,
				this.rwMode.ToString(),
				this.Position
			});
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00010EF4 File Offset: 0x0000F0F4
		private void _SetReadStream()
		{
			if (this._innerStream != null)
			{
				this._innerStream.Dispose();
			}
			if (this.CurrentSegment + 1U == this._maxDiskNumber)
			{
				this._currentName = this._baseName;
			}
			this._innerStream = File.OpenRead(this.CurrentName);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00010F44 File Offset: 0x0000F144
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.rwMode != ZipSegmentedStream.RwMode.ReadOnly)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("Stream Error: Cannot Read.");
			}
			int num = this._innerStream.Read(buffer, offset, count);
			int num2 = num;
			while (num2 != count)
			{
				if (this._innerStream.Position != this._innerStream.Length)
				{
					this._exceptionPending = true;
					throw new ZipException(string.Format("Read error in file {0}", this.CurrentName));
				}
				if (this.CurrentSegment + 1U == this._maxDiskNumber)
				{
					return num;
				}
				this.CurrentSegment += 1U;
				this._SetReadStream();
				offset += num2;
				count -= num2;
				num2 = this._innerStream.Read(buffer, offset, count);
				num += num2;
			}
			return num;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00010FFC File Offset: 0x0000F1FC
		private void _SetWriteStream(uint increment)
		{
			if (this._innerStream != null)
			{
				this._innerStream.Dispose();
				if (File.Exists(this.CurrentName))
				{
					File.Delete(this.CurrentName);
				}
				File.Move(this._currentTempName, this.CurrentName);
			}
			if (increment > 0U)
			{
				this.CurrentSegment += increment;
			}
			SharedUtilities.CreateAndOpenUniqueTempFile(this._baseDir, out this._innerStream, out this._currentTempName);
			if (this.CurrentSegment == 0U)
			{
				this._innerStream.Write(BitConverter.GetBytes(134695760), 0, 4);
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00011090 File Offset: 0x0000F290
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.rwMode != ZipSegmentedStream.RwMode.Write)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("Stream Error: Cannot Write.");
			}
			if (this.ContiguousWrite)
			{
				if (this._innerStream.Position + (long)count > (long)this._maxSegmentSize)
				{
					this._SetWriteStream(1U);
				}
			}
			else
			{
				while (this._innerStream.Position + (long)count > (long)this._maxSegmentSize)
				{
					int num = this._maxSegmentSize - (int)this._innerStream.Position;
					this._innerStream.Write(buffer, offset, num);
					this._SetWriteStream(1U);
					count -= num;
					offset += num;
				}
			}
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00011138 File Offset: 0x0000F338
		public long TruncateBackward(uint diskNumber, long offset)
		{
			if (diskNumber >= 99U)
			{
				throw new ArgumentOutOfRangeException("diskNumber");
			}
			if (this.rwMode != ZipSegmentedStream.RwMode.Write)
			{
				this._exceptionPending = true;
				throw new ZipException("bad state.");
			}
			if (diskNumber == this.CurrentSegment)
			{
				return this._innerStream.Seek(offset, 0);
			}
			if (this._innerStream != null)
			{
				this._innerStream.Dispose();
				if (File.Exists(this._currentTempName))
				{
					File.Delete(this._currentTempName);
				}
			}
			for (uint num = this.CurrentSegment - 1U; num > diskNumber; num -= 1U)
			{
				string text = this._NameForSegment(num);
				if (File.Exists(text))
				{
					File.Delete(text);
				}
			}
			this.CurrentSegment = diskNumber;
			for (int i = 0; i < 3; i++)
			{
				try
				{
					this._currentTempName = SharedUtilities.InternalGetTempFileName();
					File.Move(this.CurrentName, this._currentTempName);
					break;
				}
				catch (IOException)
				{
					if (i == 2)
					{
						throw;
					}
				}
			}
			this._innerStream = new FileStream(this._currentTempName, 3);
			return this._innerStream.Seek(offset, 0);
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0001124C File Offset: 0x0000F44C
		public override bool CanRead
		{
			get
			{
				return this.rwMode == ZipSegmentedStream.RwMode.ReadOnly && this._innerStream != null && this._innerStream.CanRead;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0001126C File Offset: 0x0000F46C
		public override bool CanSeek
		{
			get
			{
				return this._innerStream != null && this._innerStream.CanSeek;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00011283 File Offset: 0x0000F483
		public override bool CanWrite
		{
			get
			{
				return this.rwMode == ZipSegmentedStream.RwMode.Write && this._innerStream != null && this._innerStream.CanWrite;
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000112A3 File Offset: 0x0000F4A3
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000327 RID: 807 RVA: 0x000112B0 File Offset: 0x0000F4B0
		public override long Length
		{
			get
			{
				return this._innerStream.Length;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000328 RID: 808 RVA: 0x000112BD File Offset: 0x0000F4BD
		// (set) Token: 0x06000329 RID: 809 RVA: 0x000112CA File Offset: 0x0000F4CA
		public override long Position
		{
			get
			{
				return this._innerStream.Position;
			}
			set
			{
				this._innerStream.Position = value;
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000112D8 File Offset: 0x0000F4D8
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._innerStream.Seek(offset, origin);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000112F4 File Offset: 0x0000F4F4
		public override void SetLength(long value)
		{
			if (this.rwMode != ZipSegmentedStream.RwMode.Write)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException();
			}
			this._innerStream.SetLength(value);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00011318 File Offset: 0x0000F518
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this._innerStream != null)
				{
					this._innerStream.Dispose();
					if (this.rwMode == ZipSegmentedStream.RwMode.Write)
					{
						bool exceptionPending = this._exceptionPending;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x04000198 RID: 408
		private ZipSegmentedStream.RwMode rwMode;

		// Token: 0x04000199 RID: 409
		private bool _exceptionPending;

		// Token: 0x0400019A RID: 410
		private string _baseName;

		// Token: 0x0400019B RID: 411
		private string _baseDir;

		// Token: 0x0400019C RID: 412
		private string _currentName;

		// Token: 0x0400019D RID: 413
		private string _currentTempName;

		// Token: 0x0400019E RID: 414
		private uint _currentDiskNumber;

		// Token: 0x0400019F RID: 415
		private uint _maxDiskNumber;

		// Token: 0x040001A0 RID: 416
		private int _maxSegmentSize;

		// Token: 0x040001A1 RID: 417
		private Stream _innerStream;

		// Token: 0x02000040 RID: 64
		private enum RwMode
		{
			// Token: 0x040001A4 RID: 420
			None,
			// Token: 0x040001A5 RID: 421
			ReadOnly,
			// Token: 0x040001A6 RID: 422
			Write
		}
	}
}
