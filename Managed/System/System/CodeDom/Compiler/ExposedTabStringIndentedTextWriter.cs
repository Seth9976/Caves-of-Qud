using System;
using System.IO;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000351 RID: 849
	internal sealed class ExposedTabStringIndentedTextWriter : IndentedTextWriter
	{
		// Token: 0x06001C10 RID: 7184 RVA: 0x0006681E File Offset: 0x00064A1E
		public ExposedTabStringIndentedTextWriter(TextWriter writer, string tabString)
			: base(writer, tabString)
		{
			this.TabString = tabString ?? "    ";
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x00066838 File Offset: 0x00064A38
		internal void InternalOutputTabs()
		{
			TextWriter innerWriter = base.InnerWriter;
			for (int i = 0; i < base.Indent; i++)
			{
				innerWriter.Write(this.TabString);
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x00066869 File Offset: 0x00064A69
		internal string TabString { get; }
	}
}
