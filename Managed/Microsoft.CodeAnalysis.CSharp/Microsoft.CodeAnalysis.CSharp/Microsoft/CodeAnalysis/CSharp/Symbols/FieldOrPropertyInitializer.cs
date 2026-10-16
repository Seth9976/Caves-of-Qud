using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004E6 RID: 1254
	internal struct FieldOrPropertyInitializer
	{
		// Token: 0x060069FE RID: 27134 RVA: 0x0018B23B File Offset: 0x0018943B
		public FieldOrPropertyInitializer(FieldSymbol fieldOpt, SyntaxNode syntax)
		{
			this.FieldOpt = fieldOpt;
			this.Syntax = syntax.GetReference();
		}

		// Token: 0x04001A5B RID: 6747
		internal readonly FieldSymbol FieldOpt;

		// Token: 0x04001A5C RID: 6748
		internal readonly SyntaxReference Syntax;
	}
}
