using System;
using System.IO;
using System.Text;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Execution
{
	// Token: 0x0200009F RID: 159
	public class EventListenerTextWriter : TextWriter
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x0000D9D5 File Offset: 0x0000BBD5
		public EventListenerTextWriter(string streamName, TextWriter defaultWriter)
		{
			this._streamName = streamName;
			this._defaultWriter = defaultWriter;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000D9EB File Offset: 0x0000BBEB
		public override void Write(char aChar)
		{
			if (!this.TrySendToListener(aChar.ToString()))
			{
				this._defaultWriter.Write(aChar);
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000DA08 File Offset: 0x0000BC08
		public override void Write(string aString)
		{
			if (!this.TrySendToListener(aString))
			{
				this._defaultWriter.Write(aString);
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000DA1F File Offset: 0x0000BC1F
		public override void WriteLine(string aString)
		{
			if (!this.TrySendToListener(aString + Environment.NewLine))
			{
				this._defaultWriter.WriteLine(aString);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0000DA40 File Offset: 0x0000BC40
		public override Encoding Encoding
		{
			get
			{
				return Encoding.Default;
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000DA48 File Offset: 0x0000BC48
		private bool TrySendToListener(string text)
		{
			TestExecutionContext testExecutionContext = TestExecutionContext.GetTestExecutionContext();
			if (testExecutionContext == null || testExecutionContext.Listener == null)
			{
				return false;
			}
			string text2 = ((testExecutionContext.CurrentTest != null) ? testExecutionContext.CurrentTest.FullName : null);
			testExecutionContext.Listener.TestOutput(new TestOutput(text, this._streamName, text2));
			return true;
		}

		// Token: 0x04000127 RID: 295
		private TextWriter _defaultWriter;

		// Token: 0x04000128 RID: 296
		private string _streamName;
	}
}
