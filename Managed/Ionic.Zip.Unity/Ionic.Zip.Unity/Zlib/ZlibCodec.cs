using System;
using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
	// Token: 0x0200006C RID: 108
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000D")]
	[ComVisible(true)]
	[ClassInterface(1)]
	public sealed class ZlibCodec
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00020DFC File Offset: 0x0001EFFC
		public int Adler32
		{
			get
			{
				return (int)this._Adler32;
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00020E04 File Offset: 0x0001F004
		public ZlibCodec()
		{
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00020E1C File Offset: 0x0001F01C
		public ZlibCodec(CompressionMode mode)
		{
			if (mode == CompressionMode.Compress)
			{
				int num = this.InitializeDeflate();
				if (num != 0)
				{
					throw new ZlibException("Cannot initialize for deflate.");
				}
			}
			else
			{
				if (mode != CompressionMode.Decompress)
				{
					throw new ZlibException("Invalid ZlibStreamFlavor.");
				}
				int num2 = this.InitializeInflate();
				if (num2 != 0)
				{
					throw new ZlibException("Cannot initialize for inflate.");
				}
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00020E7A File Offset: 0x0001F07A
		public int InitializeInflate()
		{
			return this.InitializeInflate(this.WindowBits);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00020E88 File Offset: 0x0001F088
		public int InitializeInflate(bool expectRfc1950Header)
		{
			return this.InitializeInflate(this.WindowBits, expectRfc1950Header);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00020E97 File Offset: 0x0001F097
		public int InitializeInflate(int windowBits)
		{
			this.WindowBits = windowBits;
			return this.InitializeInflate(windowBits, true);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00020EA8 File Offset: 0x0001F0A8
		public int InitializeInflate(int windowBits, bool expectRfc1950Header)
		{
			this.WindowBits = windowBits;
			if (this.dstate != null)
			{
				throw new ZlibException("You may not call InitializeInflate() after calling InitializeDeflate().");
			}
			this.istate = new InflateManager(expectRfc1950Header);
			return this.istate.Initialize(this, windowBits);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00020EDD File Offset: 0x0001F0DD
		public int Inflate(FlushType flush)
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Inflate(flush);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00020F00 File Offset: 0x0001F100
		public int EndInflate()
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			int num = this.istate.End();
			this.istate = null;
			return num;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00020F34 File Offset: 0x0001F134
		public int SyncInflate()
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Sync();
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00020F54 File Offset: 0x0001F154
		public int InitializeDeflate()
		{
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00020F5D File Offset: 0x0001F15D
		public int InitializeDeflate(CompressionLevel level)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00020F6D File Offset: 0x0001F16D
		public int InitializeDeflate(CompressionLevel level, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00020F7D File Offset: 0x0001F17D
		public int InitializeDeflate(CompressionLevel level, int bits)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00020F94 File Offset: 0x0001F194
		public int InitializeDeflate(CompressionLevel level, int bits, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00020FAC File Offset: 0x0001F1AC
		private int _InternalInitializeDeflate(bool wantRfc1950Header)
		{
			if (this.istate != null)
			{
				throw new ZlibException("You may not call InitializeDeflate() after calling InitializeInflate().");
			}
			this.dstate = new DeflateManager();
			this.dstate.WantRfc1950HeaderBytes = wantRfc1950Header;
			return this.dstate.Initialize(this, this.CompressLevel, this.WindowBits, this.Strategy);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00021001 File Offset: 0x0001F201
		public int Deflate(FlushType flush)
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.Deflate(flush);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00021022 File Offset: 0x0001F222
		public int EndDeflate()
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate = null;
			return 0;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0002103F File Offset: 0x0001F23F
		public void ResetDeflate()
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate.Reset();
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0002105F File Offset: 0x0001F25F
		public int SetDeflateParams(CompressionLevel level, CompressionStrategy strategy)
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.SetParams(level, strategy);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00021081 File Offset: 0x0001F281
		public int SetDictionary(byte[] dictionary)
		{
			if (this.istate != null)
			{
				return this.istate.SetDictionary(dictionary);
			}
			if (this.dstate != null)
			{
				return this.dstate.SetDictionary(dictionary);
			}
			throw new ZlibException("No Inflate or Deflate state!");
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000210B8 File Offset: 0x0001F2B8
		internal void flush_pending()
		{
			int num = this.dstate.pendingCount;
			if (num > this.AvailableBytesOut)
			{
				num = this.AvailableBytesOut;
			}
			if (num == 0)
			{
				return;
			}
			if (this.dstate.pending.Length <= this.dstate.nextPending || this.OutputBuffer.Length <= this.NextOut || this.dstate.pending.Length < this.dstate.nextPending + num || this.OutputBuffer.Length < this.NextOut + num)
			{
				throw new ZlibException(string.Format("Invalid State. (pending.Length={0}, pendingCount={1})", this.dstate.pending.Length, this.dstate.pendingCount));
			}
			Array.Copy(this.dstate.pending, this.dstate.nextPending, this.OutputBuffer, this.NextOut, num);
			this.NextOut += num;
			this.dstate.nextPending += num;
			this.TotalBytesOut += (long)num;
			this.AvailableBytesOut -= num;
			this.dstate.pendingCount -= num;
			if (this.dstate.pendingCount == 0)
			{
				this.dstate.nextPending = 0;
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00021204 File Offset: 0x0001F404
		internal int read_buf(byte[] buf, int start, int size)
		{
			int num = this.AvailableBytesIn;
			if (num > size)
			{
				num = size;
			}
			if (num == 0)
			{
				return 0;
			}
			this.AvailableBytesIn -= num;
			if (this.dstate.WantRfc1950HeaderBytes)
			{
				this._Adler32 = Adler.Adler32(this._Adler32, this.InputBuffer, this.NextIn, num);
			}
			Array.Copy(this.InputBuffer, this.NextIn, buf, start, num);
			this.NextIn += num;
			this.TotalBytesIn += (long)num;
			return num;
		}

		// Token: 0x040003B6 RID: 950
		public byte[] InputBuffer;

		// Token: 0x040003B7 RID: 951
		public int NextIn;

		// Token: 0x040003B8 RID: 952
		public int AvailableBytesIn;

		// Token: 0x040003B9 RID: 953
		public long TotalBytesIn;

		// Token: 0x040003BA RID: 954
		public byte[] OutputBuffer;

		// Token: 0x040003BB RID: 955
		public int NextOut;

		// Token: 0x040003BC RID: 956
		public int AvailableBytesOut;

		// Token: 0x040003BD RID: 957
		public long TotalBytesOut;

		// Token: 0x040003BE RID: 958
		public string Message;

		// Token: 0x040003BF RID: 959
		internal DeflateManager dstate;

		// Token: 0x040003C0 RID: 960
		internal InflateManager istate;

		// Token: 0x040003C1 RID: 961
		internal uint _Adler32;

		// Token: 0x040003C2 RID: 962
		public CompressionLevel CompressLevel = CompressionLevel.Default;

		// Token: 0x040003C3 RID: 963
		public int WindowBits = 15;

		// Token: 0x040003C4 RID: 964
		public CompressionStrategy Strategy;
	}
}
