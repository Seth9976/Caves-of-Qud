using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200017B RID: 379
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundMaximumMethodDefIndex : BoundExpression
	{
		// Token: 0x06001E27 RID: 7719 RVA: 0x000A05F2 File Offset: 0x0009E7F2
		public BoundMaximumMethodDefIndex(SyntaxNode syntax, TypeSymbol type, bool hasErrors)
			: base(BoundKind.MaximumMethodDefIndex, syntax, type, hasErrors)
		{
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x000A05FF File Offset: 0x0009E7FF
		public BoundMaximumMethodDefIndex(SyntaxNode syntax, TypeSymbol type)
			: base(BoundKind.MaximumMethodDefIndex, syntax, type)
		{
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x000A060B File Offset: 0x0009E80B
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x000A0613 File Offset: 0x0009E813
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitMaximumMethodDefIndex(this);
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x000A061C File Offset: 0x0009E81C
		public BoundMaximumMethodDefIndex Update(TypeSymbol type)
		{
			if (!TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundMaximumMethodDefIndex boundMaximumMethodDefIndex = new BoundMaximumMethodDefIndex(this.Syntax, type, base.HasErrors);
				boundMaximumMethodDefIndex.CopyAttributes(this);
				return boundMaximumMethodDefIndex;
			}
			return this;
		}
	}
}
