using System;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000E0 RID: 224
	public class TestOutput
	{
		// Token: 0x06000715 RID: 1813 RVA: 0x0001132D File Offset: 0x0000F52D
		public TestOutput(string text, string stream, string testName)
		{
			this.Text = text;
			this.Stream = stream;
			this.TestName = testName;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001134A File Offset: 0x0000F54A
		public override string ToString()
		{
			return this.Stream + ": " + this.Text;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00011362 File Offset: 0x0000F562
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0001136A File Offset: 0x0000F56A
		public string Text { get; private set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00011373 File Offset: 0x0000F573
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0001137B File Offset: 0x0000F57B
		public string Stream { get; private set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00011384 File Offset: 0x0000F584
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0001138C File Offset: 0x0000F58C
		public string TestName { get; private set; }

		// Token: 0x0600071D RID: 1821 RVA: 0x00011398 File Offset: 0x0000F598
		public string ToXml()
		{
			TNode tnode = new TNode("test-output", this.Text, true);
			tnode.AddAttribute("stream", this.Stream);
			if (this.TestName != null)
			{
				tnode.AddAttribute("testname", this.TestName);
			}
			return tnode.OuterXml;
		}
	}
}
