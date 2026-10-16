using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cysharp.Text
{
	// Token: 0x02000043 RID: 67
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ZStringWriter : TextWriter
	{
		// Token: 0x06000357 RID: 855 RVA: 0x00022881 File Offset: 0x00020A81
		public ZStringWriter()
			: this(CultureInfo.CurrentCulture)
		{
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0002288E File Offset: 0x00020A8E
		public ZStringWriter(IFormatProvider formatProvider)
			: base(formatProvider)
		{
			this.sb = ZString.CreateStringBuilder();
			this.isOpen = true;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000228A9 File Offset: 0x00020AA9
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000228B2 File Offset: 0x00020AB2
		protected override void Dispose(bool disposing)
		{
			this.sb.Dispose();
			this.isOpen = false;
			base.Dispose(disposing);
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600035B RID: 859 RVA: 0x000228D0 File Offset: 0x00020AD0
		public override Encoding Encoding
		{
			get
			{
				return this.encoding = this.encoding ?? new UnicodeEncoding(false, false);
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x000228F7 File Offset: 0x00020AF7
		public override void Write(char value)
		{
			this.AssertNotDisposed();
			this.sb.Append(value);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0002290C File Offset: 0x00020B0C
		public override void Write(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException();
			}
			this.AssertNotDisposed();
			this.sb.Append(buffer, index, count);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00022967 File Offset: 0x00020B67
		public override void Write(string value)
		{
			this.AssertNotDisposed();
			if (value != null)
			{
				this.sb.Append(value);
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0002297E File Offset: 0x00020B7E
		public override Task WriteAsync(char value)
		{
			this.Write(value);
			return Task.CompletedTask;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0002298C File Offset: 0x00020B8C
		public override Task WriteAsync(string value)
		{
			this.Write(value);
			return Task.CompletedTask;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0002299A File Offset: 0x00020B9A
		public override Task WriteAsync(char[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
			return Task.CompletedTask;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000229AA File Offset: 0x00020BAA
		public override Task WriteLineAsync(char value)
		{
			this.WriteLine(value);
			return Task.CompletedTask;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000229B8 File Offset: 0x00020BB8
		public override Task WriteLineAsync(string value)
		{
			this.WriteLine(value);
			return Task.CompletedTask;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000229C6 File Offset: 0x00020BC6
		public override Task WriteLineAsync(char[] buffer, int index, int count)
		{
			this.WriteLine(buffer, index, count);
			return Task.CompletedTask;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000229D6 File Offset: 0x00020BD6
		public override void Write(bool value)
		{
			this.AssertNotDisposed();
			this.sb.Append<bool>(value);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000229EA File Offset: 0x00020BEA
		public override void Write(decimal value)
		{
			this.AssertNotDisposed();
			this.sb.Append(value);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000229FE File Offset: 0x00020BFE
		public override Task FlushAsync()
		{
			return Task.CompletedTask;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00022A05 File Offset: 0x00020C05
		public override string ToString()
		{
			return this.sb.ToString();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00022A18 File Offset: 0x00020C18
		private void AssertNotDisposed()
		{
			if (!this.isOpen)
			{
				throw new ObjectDisposedException("sb");
			}
		}

		// Token: 0x040000D1 RID: 209
		private Utf16ValueStringBuilder sb;

		// Token: 0x040000D2 RID: 210
		private bool isOpen;

		// Token: 0x040000D3 RID: 211
		[Nullable(2)]
		private UnicodeEncoding encoding;
	}
}
