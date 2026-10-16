using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x02000B6C RID: 2924
	internal class CStreamReader : StreamReader
	{
		// Token: 0x06006A6A RID: 27242 RVA: 0x0016C4B4 File Offset: 0x0016A6B4
		public CStreamReader(Stream stream, Encoding encoding)
			: base(stream, encoding)
		{
			this.driver = (TermInfoDriver)ConsoleDriver.driver;
		}

		// Token: 0x06006A6B RID: 27243 RVA: 0x0016C4D0 File Offset: 0x0016A6D0
		public override int Peek()
		{
			try
			{
				return base.Peek();
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x06006A6C RID: 27244 RVA: 0x0016C4FC File Offset: 0x0016A6FC
		public override int Read()
		{
			try
			{
				return (int)Console.ReadKey().KeyChar;
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x06006A6D RID: 27245 RVA: 0x0016C530 File Offset: 0x0016A730
		public override int Read([In] [Out] char[] dest, int index, int count)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (index > dest.Length - count)
			{
				throw new ArgumentException("index + count > dest.Length");
			}
			try
			{
				return this.driver.Read(dest, index, count);
			}
			catch (IOException)
			{
			}
			return 0;
		}

		// Token: 0x06006A6E RID: 27246 RVA: 0x0016C5B0 File Offset: 0x0016A7B0
		public override string ReadLine()
		{
			try
			{
				return this.driver.ReadLine();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x06006A6F RID: 27247 RVA: 0x0016C5E4 File Offset: 0x0016A7E4
		public override string ReadToEnd()
		{
			try
			{
				return this.driver.ReadToEnd();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x04003D8B RID: 15755
		private TermInfoDriver driver;
	}
}
