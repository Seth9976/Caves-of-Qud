using System;
using System.IO;
using System.Text;

namespace NUnit.Framework.Internal
{
	// Token: 0x0200006C RID: 108
	public class InternalTraceWriter : TextWriter
	{
		// Token: 0x0600039B RID: 923 RVA: 0x0000810C File Offset: 0x0000630C
		public InternalTraceWriter(string logPath)
		{
			this.writer = new StreamWriter(new FileStream(logPath, FileMode.Append, FileAccess.Write, FileShare.Write))
			{
				AutoFlush = true
			};
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00008147 File Offset: 0x00006347
		public InternalTraceWriter(TextWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00008161 File Offset: 0x00006361
		public override Encoding Encoding
		{
			get
			{
				return this.writer.Encoding;
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00008170 File Offset: 0x00006370
		public override void Write(char value)
		{
			object obj = this.myLock;
			lock (obj)
			{
				this.writer.Write(value);
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000081B8 File Offset: 0x000063B8
		public override void Write(string value)
		{
			object obj = this.myLock;
			lock (obj)
			{
				base.Write(value);
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000081FC File Offset: 0x000063FC
		public override void WriteLine(string value)
		{
			this.writer.WriteLine(value);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000820A File Offset: 0x0000640A
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.writer != null)
			{
				this.writer.Flush();
				this.writer.Dispose();
				this.writer = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000823B File Offset: 0x0000643B
		public override void Flush()
		{
			if (this.writer != null)
			{
				this.writer.Flush();
			}
		}

		// Token: 0x04000081 RID: 129
		private TextWriter writer;

		// Token: 0x04000082 RID: 130
		private object myLock = new object();
	}
}
