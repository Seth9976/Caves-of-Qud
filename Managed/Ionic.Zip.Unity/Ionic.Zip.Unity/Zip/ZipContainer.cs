using System;
using System.IO;
using System.Text;
using Ionic.Zlib;

namespace Ionic.Zip
{
	// Token: 0x0200003E RID: 62
	internal class ZipContainer
	{
		// Token: 0x060002FF RID: 767 RVA: 0x00010A3E File Offset: 0x0000EC3E
		public ZipContainer(object o)
		{
			this._zf = o as ZipFile;
			this._zos = o as ZipOutputStream;
			this._zis = o as ZipInputStream;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00010A6A File Offset: 0x0000EC6A
		public ZipFile ZipFile
		{
			get
			{
				return this._zf;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00010A72 File Offset: 0x0000EC72
		public ZipOutputStream ZipOutputStream
		{
			get
			{
				return this._zos;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00010A7A File Offset: 0x0000EC7A
		public string Name
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.Name;
				}
				if (this._zis != null)
				{
					throw new NotSupportedException();
				}
				return this._zos.Name;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00010AA9 File Offset: 0x0000ECA9
		public string Password
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf._Password;
				}
				if (this._zis != null)
				{
					return this._zis._Password;
				}
				return this._zos._password;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00010ADE File Offset: 0x0000ECDE
		public Zip64Option Zip64
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf._zip64;
				}
				if (this._zis != null)
				{
					throw new NotSupportedException();
				}
				return this._zos._zip64;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00010B0D File Offset: 0x0000ED0D
		public int BufferSize
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.BufferSize;
				}
				if (this._zis != null)
				{
					throw new NotSupportedException();
				}
				return 0;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00010B32 File Offset: 0x0000ED32
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00010B5D File Offset: 0x0000ED5D
		public ParallelDeflateOutputStream ParallelDeflater
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.ParallelDeflater;
				}
				if (this._zis != null)
				{
					return null;
				}
				return this._zos.ParallelDeflater;
			}
			set
			{
				if (this._zf != null)
				{
					this._zf.ParallelDeflater = value;
					return;
				}
				if (this._zos != null)
				{
					this._zos.ParallelDeflater = value;
				}
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00010B88 File Offset: 0x0000ED88
		public long ParallelDeflateThreshold
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.ParallelDeflateThreshold;
				}
				return this._zos.ParallelDeflateThreshold;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00010BA9 File Offset: 0x0000EDA9
		public int ParallelDeflateMaxBufferPairs
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.ParallelDeflateMaxBufferPairs;
				}
				return this._zos.ParallelDeflateMaxBufferPairs;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00010BCA File Offset: 0x0000EDCA
		public int CodecBufferSize
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.CodecBufferSize;
				}
				if (this._zis != null)
				{
					return this._zis.CodecBufferSize;
				}
				return this._zos.CodecBufferSize;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00010BFF File Offset: 0x0000EDFF
		public CompressionStrategy Strategy
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.Strategy;
				}
				return this._zos.Strategy;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00010C20 File Offset: 0x0000EE20
		public Zip64Option UseZip64WhenSaving
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.UseZip64WhenSaving;
				}
				return this._zos.EnableZip64;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00010C41 File Offset: 0x0000EE41
		public Encoding AlternateEncoding
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.AlternateEncoding;
				}
				if (this._zos != null)
				{
					return this._zos.AlternateEncoding;
				}
				return null;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00010C6C File Offset: 0x0000EE6C
		public Encoding DefaultEncoding
		{
			get
			{
				if (this._zf != null)
				{
					return ZipFile.DefaultEncoding;
				}
				if (this._zos != null)
				{
					return ZipOutputStream.DefaultEncoding;
				}
				return null;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00010C8B File Offset: 0x0000EE8B
		public ZipOption AlternateEncodingUsage
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.AlternateEncodingUsage;
				}
				if (this._zos != null)
				{
					return this._zos.AlternateEncodingUsage;
				}
				return ZipOption.Default;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00010CB6 File Offset: 0x0000EEB6
		public Stream ReadStream
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.ReadStream;
				}
				return this._zis.ReadStream;
			}
		}

		// Token: 0x04000195 RID: 405
		private ZipFile _zf;

		// Token: 0x04000196 RID: 406
		private ZipOutputStream _zos;

		// Token: 0x04000197 RID: 407
		private ZipInputStream _zis;
	}
}
