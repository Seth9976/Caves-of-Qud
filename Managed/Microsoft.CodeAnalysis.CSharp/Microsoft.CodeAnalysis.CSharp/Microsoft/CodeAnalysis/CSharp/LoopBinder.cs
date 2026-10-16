using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200003C RID: 60
	internal abstract class LoopBinder : LocalScopeBinder
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x0003B6DA File Offset: 0x000398DA
		protected LoopBinder(Binder enclosing)
			: base(enclosing)
		{
			this._breakLabel = new GeneratedLabelSymbol("break");
			this._continueLabel = new GeneratedLabelSymbol("continue");
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0003B703 File Offset: 0x00039903
		internal override GeneratedLabelSymbol BreakLabel
		{
			get
			{
				return this._breakLabel;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0003B70B File Offset: 0x0003990B
		internal override GeneratedLabelSymbol ContinueLabel
		{
			get
			{
				return this._continueLabel;
			}
		}

		// Token: 0x040000C0 RID: 192
		private readonly GeneratedLabelSymbol _breakLabel;

		// Token: 0x040000C1 RID: 193
		private readonly GeneratedLabelSymbol _continueLabel;
	}
}
