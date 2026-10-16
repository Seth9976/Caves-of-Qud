using System;
using System.Text;

namespace System.IO
{
	// Token: 0x02000B6B RID: 2923
	internal class UnexceptionalStreamWriter : StreamWriter
	{
		// Token: 0x06006A64 RID: 27236 RVA: 0x0016C3CC File Offset: 0x0016A5CC
		public UnexceptionalStreamWriter(Stream stream, Encoding encoding)
			: base(stream, encoding, 1024, true)
		{
		}

		// Token: 0x06006A65 RID: 27237 RVA: 0x0016C3DC File Offset: 0x0016A5DC
		public override void Flush()
		{
			try
			{
				base.Flush();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06006A66 RID: 27238 RVA: 0x0016C404 File Offset: 0x0016A604
		public override void Write(char[] buffer, int index, int count)
		{
			try
			{
				base.Write(buffer, index, count);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06006A67 RID: 27239 RVA: 0x0016C430 File Offset: 0x0016A630
		public override void Write(char value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06006A68 RID: 27240 RVA: 0x0016C45C File Offset: 0x0016A65C
		public override void Write(char[] value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06006A69 RID: 27241 RVA: 0x0016C488 File Offset: 0x0016A688
		public override void Write(string value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}
	}
}
