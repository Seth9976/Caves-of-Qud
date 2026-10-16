using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace Ionic.Zip
{
	// Token: 0x02000027 RID: 39
	internal class WinZipAesCipherStream : Stream
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00004876 File Offset: 0x00002A76
		internal WinZipAesCipherStream(Stream s, WinZipAesCrypto cryptoParams, long length, CryptoMode mode)
			: this(s, cryptoParams, mode)
		{
			this._length = length;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000488C File Offset: 0x00002A8C
		internal WinZipAesCipherStream(Stream s, WinZipAesCrypto cryptoParams, CryptoMode mode)
		{
			this._params = cryptoParams;
			this._s = s;
			this._mode = mode;
			this._nonce = 1;
			if (this._params == null)
			{
				throw new BadPasswordException("Supply a password to use AES encryption.");
			}
			int num = this._params.KeyBytes.Length * 8;
			if (num != 256 && num != 128 && num != 192)
			{
				throw new ArgumentOutOfRangeException("keysize", "size of key must be 128, 192, or 256");
			}
			this._mac = new HMACSHA1(this._params.MacIv);
			this._aesCipher = new RijndaelManaged();
			this._aesCipher.BlockSize = 128;
			this._aesCipher.KeySize = num;
			this._aesCipher.Mode = 2;
			this._aesCipher.Padding = 1;
			byte[] array = new byte[16];
			this._xform = this._aesCipher.CreateEncryptor(this._params.KeyBytes, array);
			if (this._mode == CryptoMode.Encrypt)
			{
				this._iobuf = new byte[2048];
				this._PendingWriteBlock = new byte[16];
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000049CC File Offset: 0x00002BCC
		private void XorInPlace(byte[] buffer, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				buffer[offset + i] = this.counterOut[i] ^ buffer[offset + i];
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000049FC File Offset: 0x00002BFC
		private void WriteTransformOneBlock(byte[] buffer, int offset)
		{
			Array.Copy(BitConverter.GetBytes(this._nonce++), 0, this.counter, 0, 4);
			this._xform.TransformBlock(this.counter, 0, 16, this.counterOut, 0);
			this.XorInPlace(buffer, offset, 16);
			this._mac.TransformBlock(buffer, offset, 16, null, 0);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004A68 File Offset: 0x00002C68
		private void WriteTransformBlocks(byte[] buffer, int offset, int count)
		{
			int num = offset;
			int num2 = count + offset;
			while (num < buffer.Length && num < num2)
			{
				this.WriteTransformOneBlock(buffer, num);
				num += 16;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004A94 File Offset: 0x00002C94
		private void WriteTransformFinalBlock()
		{
			if (this._pendingCount == 0)
			{
				throw new InvalidOperationException("No bytes available.");
			}
			if (this._finalBlock)
			{
				throw new InvalidOperationException("The final block has already been transformed.");
			}
			Array.Copy(BitConverter.GetBytes(this._nonce++), 0, this.counter, 0, 4);
			this.counterOut = this._xform.TransformFinalBlock(this.counter, 0, 16);
			this.XorInPlace(this._PendingWriteBlock, 0, this._pendingCount);
			this._mac.TransformFinalBlock(this._PendingWriteBlock, 0, this._pendingCount);
			this._finalBlock = true;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004B38 File Offset: 0x00002D38
		private int ReadTransformOneBlock(byte[] buffer, int offset, int last)
		{
			if (this._finalBlock)
			{
				throw new NotSupportedException();
			}
			int num = last - offset;
			int num2 = ((num > 16) ? 16 : num);
			Array.Copy(BitConverter.GetBytes(this._nonce++), 0, this.counter, 0, 4);
			if (num2 == num && this._length > 0L && this._totalBytesXferred + (long)last == this._length)
			{
				this._mac.TransformFinalBlock(buffer, offset, num2);
				this.counterOut = this._xform.TransformFinalBlock(this.counter, 0, 16);
				this._finalBlock = true;
			}
			else
			{
				this._mac.TransformBlock(buffer, offset, num2, null, 0);
				this._xform.TransformBlock(this.counter, 0, 16, this.counterOut, 0);
			}
			this.XorInPlace(buffer, offset, num2);
			return num2;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004C10 File Offset: 0x00002E10
		private void ReadTransformBlocks(byte[] buffer, int offset, int count)
		{
			int num = offset;
			int num2 = count + offset;
			while (num < buffer.Length && num < num2)
			{
				int num3 = this.ReadTransformOneBlock(buffer, num, num2);
				num += num3;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004C40 File Offset: 0x00002E40
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._mode == CryptoMode.Encrypt)
			{
				throw new NotSupportedException();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must not be less than zero.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must not be less than zero.");
			}
			if (buffer.Length < offset + count)
			{
				throw new ArgumentException("The buffer is too small");
			}
			int num = count;
			if (this._totalBytesXferred >= this._length)
			{
				return 0;
			}
			long num2 = this._length - this._totalBytesXferred;
			if (num2 < (long)count)
			{
				num = (int)num2;
			}
			int num3 = this._s.Read(buffer, offset, num);
			this.ReadTransformBlocks(buffer, offset, num);
			this._totalBytesXferred += (long)num3;
			return num3;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004CF4 File Offset: 0x00002EF4
		public byte[] FinalAuthentication
		{
			get
			{
				if (!this._finalBlock)
				{
					if (this._totalBytesXferred != 0L)
					{
						throw new BadStateException("The final hash has not been computed.");
					}
					byte[] array = new byte[0];
					this._mac.ComputeHash(array);
				}
				byte[] array2 = new byte[10];
				Array.Copy(this._mac.Hash, 0, array2, 0, 10);
				return array2;
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004D50 File Offset: 0x00002F50
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._finalBlock)
			{
				throw new InvalidOperationException("The final block has already been transformed.");
			}
			if (this._mode == CryptoMode.Decrypt)
			{
				throw new NotSupportedException();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must not be less than zero.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must not be less than zero.");
			}
			if (buffer.Length < offset + count)
			{
				throw new ArgumentException("The offset and count are too large");
			}
			if (count == 0)
			{
				return;
			}
			if (count + this._pendingCount <= 16)
			{
				Buffer.BlockCopy(buffer, offset, this._PendingWriteBlock, this._pendingCount, count);
				this._pendingCount += count;
				return;
			}
			int num = count;
			int num2 = offset;
			if (this._pendingCount != 0)
			{
				int num3 = 16 - this._pendingCount;
				if (num3 > 0)
				{
					Buffer.BlockCopy(buffer, offset, this._PendingWriteBlock, this._pendingCount, num3);
					num -= num3;
					num2 += num3;
				}
				this.WriteTransformOneBlock(this._PendingWriteBlock, 0);
				this._s.Write(this._PendingWriteBlock, 0, 16);
				this._totalBytesXferred += 16L;
				this._pendingCount = 0;
			}
			int num4 = (num - 1) / 16;
			this._pendingCount = num - num4 * 16;
			Buffer.BlockCopy(buffer, num2 + num - this._pendingCount, this._PendingWriteBlock, 0, this._pendingCount);
			num -= this._pendingCount;
			this._totalBytesXferred += (long)num;
			if (num4 > 0)
			{
				do
				{
					int num5 = this._iobuf.Length;
					if (num5 > num)
					{
						num5 = num;
					}
					Buffer.BlockCopy(buffer, num2, this._iobuf, 0, num5);
					this.WriteTransformBlocks(this._iobuf, 0, num5);
					this._s.Write(this._iobuf, 0, num5);
					num -= num5;
					num2 += num5;
				}
				while (num > 0);
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004F08 File Offset: 0x00003108
		public override void Close()
		{
			if (this._pendingCount > 0)
			{
				this.WriteTransformFinalBlock();
				this._s.Write(this._PendingWriteBlock, 0, this._pendingCount);
				this._totalBytesXferred += (long)this._pendingCount;
				this._pendingCount = 0;
			}
			this._s.Close();
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004F62 File Offset: 0x00003162
		public override bool CanRead
		{
			get
			{
				return this._mode == CryptoMode.Decrypt;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004F70 File Offset: 0x00003170
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004F73 File Offset: 0x00003173
		public override bool CanWrite
		{
			get
			{
				return this._mode == CryptoMode.Encrypt;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004F7E File Offset: 0x0000317E
		public override void Flush()
		{
			this._s.Flush();
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004F8B File Offset: 0x0000318B
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004F92 File Offset: 0x00003192
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00004F99 File Offset: 0x00003199
		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004FA0 File Offset: 0x000031A0
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004FA7 File Offset: 0x000031A7
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004FB0 File Offset: 0x000031B0
		[Conditional("Trace")]
		private void TraceOutput(string format, params object[] varParams)
		{
			lock (this._outputLock)
			{
				int hashCode = Thread.CurrentThread.GetHashCode();
				Console.ForegroundColor = hashCode % 8 + 8;
				Console.Write("{0:000} WZACS ", hashCode);
				Console.WriteLine(format, varParams);
				Console.ResetColor();
			}
		}

		// Token: 0x04000071 RID: 113
		private const int BLOCK_SIZE_IN_BYTES = 16;

		// Token: 0x04000072 RID: 114
		private WinZipAesCrypto _params;

		// Token: 0x04000073 RID: 115
		private Stream _s;

		// Token: 0x04000074 RID: 116
		private CryptoMode _mode;

		// Token: 0x04000075 RID: 117
		private int _nonce;

		// Token: 0x04000076 RID: 118
		private bool _finalBlock;

		// Token: 0x04000077 RID: 119
		internal HMACSHA1 _mac;

		// Token: 0x04000078 RID: 120
		internal RijndaelManaged _aesCipher;

		// Token: 0x04000079 RID: 121
		internal ICryptoTransform _xform;

		// Token: 0x0400007A RID: 122
		private byte[] counter = new byte[16];

		// Token: 0x0400007B RID: 123
		private byte[] counterOut = new byte[16];

		// Token: 0x0400007C RID: 124
		private long _length;

		// Token: 0x0400007D RID: 125
		private long _totalBytesXferred;

		// Token: 0x0400007E RID: 126
		private byte[] _PendingWriteBlock;

		// Token: 0x0400007F RID: 127
		private int _pendingCount;

		// Token: 0x04000080 RID: 128
		private byte[] _iobuf;

		// Token: 0x04000081 RID: 129
		private object _outputLock = new object();
	}
}
