using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001D9 RID: 473
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct GeneratedSourceText
	{
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x0003DD85 File Offset: 0x0003BF85
		public SourceText Text { get; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x0003DD8D File Offset: 0x0003BF8D
		public string HintName { get; }

		// Token: 0x060013BC RID: 5052 RVA: 0x0003DD95 File Offset: 0x0003BF95
		public GeneratedSourceText(string hintName, SourceText text)
		{
			this.Text = text;
			this.HintName = hintName;
		}
	}
}
