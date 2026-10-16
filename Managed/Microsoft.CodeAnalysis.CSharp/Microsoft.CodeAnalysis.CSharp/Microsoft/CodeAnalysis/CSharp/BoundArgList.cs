using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000185 RID: 389
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundArgList : BoundExpression
	{
		// Token: 0x06001E64 RID: 7780 RVA: 0x000A0B71 File Offset: 0x0009ED71
		public BoundArgList(SyntaxNode syntax, TypeSymbol type, bool hasErrors)
			: base(BoundKind.ArgList, syntax, type, hasErrors)
		{
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x000A0B7E File Offset: 0x0009ED7E
		public BoundArgList(SyntaxNode syntax, TypeSymbol type)
			: base(BoundKind.ArgList, syntax, type)
		{
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001E66 RID: 7782 RVA: 0x000A0B8A File Offset: 0x0009ED8A
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x000A0B92 File Offset: 0x0009ED92
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitArgList(this);
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x000A0B9B File Offset: 0x0009ED9B
		public BoundArgList Update(TypeSymbol type)
		{
			if (!TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundArgList boundArgList = new BoundArgList(this.Syntax, type, base.HasErrors);
				boundArgList.CopyAttributes(this);
				return boundArgList;
			}
			return this;
		}
	}
}
