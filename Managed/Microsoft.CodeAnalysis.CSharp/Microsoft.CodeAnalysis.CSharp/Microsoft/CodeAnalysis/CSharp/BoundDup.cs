using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200016B RID: 363
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDup : BoundExpression
	{
		// Token: 0x06001DCA RID: 7626 RVA: 0x0009FD0B File Offset: 0x0009DF0B
		public BoundDup(SyntaxNode syntax, RefKind refKind, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors)
			: base(BoundKind.Dup, syntax, type, hasErrors)
		{
			this.RefKind = refKind;
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x0009FD20 File Offset: 0x0009DF20
		public BoundDup(SyntaxNode syntax, RefKind refKind, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
			: base(BoundKind.Dup, syntax, type)
		{
			this.RefKind = refKind;
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x0009FD33 File Offset: 0x0009DF33
		public RefKind RefKind { get; }

		// Token: 0x06001DCD RID: 7629 RVA: 0x0009FD3B File Offset: 0x0009DF3B
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDup(this);
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x0009FD44 File Offset: 0x0009DF44
		public BoundDup Update(RefKind refKind, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (refKind != this.RefKind || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundDup boundDup = new BoundDup(this.Syntax, refKind, type, base.HasErrors);
				boundDup.CopyAttributes(this);
				return boundDup;
			}
			return this;
		}
	}
}
