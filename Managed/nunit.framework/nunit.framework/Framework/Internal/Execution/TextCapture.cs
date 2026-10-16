using System;
using System.IO;
using System.Text;

namespace NUnit.Framework.Internal.Execution
{
	// Token: 0x020000A5 RID: 165
	public class TextCapture : TextWriter
	{
		// Token: 0x06000619 RID: 1561 RVA: 0x0000E6AF File Offset: 0x0000C8AF
		public TextCapture(TextWriter defaultWriter)
		{
			this._defaultWriter = defaultWriter;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0000E6BE File Offset: 0x0000C8BE
		public override Encoding Encoding
		{
			get
			{
				return this._defaultWriter.Encoding;
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0000E6CC File Offset: 0x0000C8CC
		public override void Write(char value)
		{
			TestExecutionContext testExecutionContext = TestExecutionContext.GetTestExecutionContext();
			if (testExecutionContext != null && testExecutionContext.CurrentResult != null)
			{
				testExecutionContext.CurrentResult.OutWriter.Write(value);
				return;
			}
			this._defaultWriter.Write(value);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0000E708 File Offset: 0x0000C908
		public override void Write(string value)
		{
			TestExecutionContext testExecutionContext = TestExecutionContext.GetTestExecutionContext();
			if (testExecutionContext != null && testExecutionContext.CurrentResult != null)
			{
				testExecutionContext.CurrentResult.OutWriter.Write(value);
				return;
			}
			this._defaultWriter.Write(value);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0000E744 File Offset: 0x0000C944
		public override void WriteLine(string value)
		{
			TestExecutionContext testExecutionContext = TestExecutionContext.GetTestExecutionContext();
			if (testExecutionContext != null && testExecutionContext.CurrentResult != null)
			{
				testExecutionContext.CurrentResult.OutWriter.WriteLine(value);
				return;
			}
			this._defaultWriter.WriteLine(value);
		}

		// Token: 0x04000137 RID: 311
		private TextWriter _defaultWriter;
	}
}
