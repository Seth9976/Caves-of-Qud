using System;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000367 RID: 871
	internal class SyntaxFactoryContext
	{
		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06004B01 RID: 19201 RVA: 0x00136CA1 File Offset: 0x00134EA1
		internal bool IsInQuery
		{
			get
			{
				return this.QueryDepth > 0;
			}
		}

		// Token: 0x040015A8 RID: 5544
		internal bool IsInAsync;

		// Token: 0x040015A9 RID: 5545
		internal int QueryDepth;
	}
}
