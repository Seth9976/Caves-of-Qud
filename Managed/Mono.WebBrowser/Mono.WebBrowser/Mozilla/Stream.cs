using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Mozilla
{
	// Token: 0x02000062 RID: 98
	internal class Stream : nsIInputStream, nsIOutputStream
	{
		// Token: 0x06000278 RID: 632 RVA: 0x000040B8 File Offset: 0x000022B8
		public Stream(Stream stream)
		{
			this.back = stream;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000040C7 File Offset: 0x000022C7
		public Stream BaseStream
		{
			get
			{
				return this.back;
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000040CF File Offset: 0x000022CF
		public int close()
		{
			this.back.Close();
			return 0;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000040DD File Offset: 0x000022DD
		public int flush()
		{
			this.back.Flush();
			return 0;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000040EC File Offset: 0x000022EC
		public int write([MarshalAs(UnmanagedType.LPStr)] string str, uint count, out uint ret)
		{
			ret = count;
			if (count <= 0U)
			{
				return 0;
			}
			byte[] bytes = Encoding.ASCII.GetBytes(str);
			this.back.Write(bytes, 0, (int)count);
			return 0;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000411D File Offset: 0x0000231D
		public int writeFrom([MarshalAs(UnmanagedType.Interface)] nsIInputStream aFromStream, uint aCount, out uint ret)
		{
			ret = 0U;
			return 0;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00004123 File Offset: 0x00002323
		public int writeSegments(nsIReadSegmentFunDelegate aReader, IntPtr aClosure, uint aCount, out uint ret)
		{
			ret = 0U;
			return 0;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000412A File Offset: 0x0000232A
		public int isNonBlocking(out bool ret)
		{
			ret = false;
			return 0;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00004130 File Offset: 0x00002330
		public int available(out uint ret)
		{
			ret = 0U;
			return 0;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00004138 File Offset: 0x00002338
		public int read(HandleRef str, uint count, out uint ret)
		{
			byte[] array = new byte[count];
			ret = (uint)this.back.Read(array, 0, (int)count);
			string @string = Encoding.ASCII.GetString(array);
			Base.StringSet(str, @string);
			return 0;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00004170 File Offset: 0x00002370
		public int readSegments(nsIWriteSegmentFunDelegate aWriter, IntPtr aClosure, uint aCount, out uint ret)
		{
			ret = 0U;
			return 0;
		}

		// Token: 0x040000D4 RID: 212
		private Stream back;
	}
}
