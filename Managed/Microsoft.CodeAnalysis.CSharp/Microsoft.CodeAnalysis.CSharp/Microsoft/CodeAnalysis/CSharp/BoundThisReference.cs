using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001A3 RID: 419
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundThisReference : BoundExpression
	{
		// Token: 0x06001F05 RID: 7941 RVA: 0x000A1C64 File Offset: 0x0009FE64
		public BoundThisReference(SyntaxNode syntax, TypeSymbol type, bool hasErrors)
			: base(BoundKind.ThisReference, syntax, type, hasErrors)
		{
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x000A1C71 File Offset: 0x0009FE71
		public BoundThisReference(SyntaxNode syntax, TypeSymbol type)
			: base(BoundKind.ThisReference, syntax, type)
		{
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x000A1C7D File Offset: 0x0009FE7D
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x000A1C85 File Offset: 0x0009FE85
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitThisReference(this);
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x000A1C8E File Offset: 0x0009FE8E
		public BoundThisReference Update(TypeSymbol type)
		{
			if (!TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundThisReference boundThisReference = new BoundThisReference(this.Syntax, type, base.HasErrors);
				boundThisReference.CopyAttributes(this);
				return boundThisReference;
			}
			return this;
		}
	}
}
