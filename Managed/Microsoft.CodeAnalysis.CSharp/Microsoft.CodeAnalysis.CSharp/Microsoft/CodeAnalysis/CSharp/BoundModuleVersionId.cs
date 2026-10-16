using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200017D RID: 381
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundModuleVersionId : BoundExpression
	{
		// Token: 0x06001E32 RID: 7730 RVA: 0x000A06BF File Offset: 0x0009E8BF
		public BoundModuleVersionId(SyntaxNode syntax, TypeSymbol type, bool hasErrors)
			: base(BoundKind.ModuleVersionId, syntax, type, hasErrors)
		{
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x000A06CC File Offset: 0x0009E8CC
		public BoundModuleVersionId(SyntaxNode syntax, TypeSymbol type)
			: base(BoundKind.ModuleVersionId, syntax, type)
		{
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x000A06D8 File Offset: 0x0009E8D8
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x000A06E0 File Offset: 0x0009E8E0
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitModuleVersionId(this);
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x000A06E9 File Offset: 0x0009E8E9
		public BoundModuleVersionId Update(TypeSymbol type)
		{
			if (!TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundModuleVersionId boundModuleVersionId = new BoundModuleVersionId(this.Syntax, type, base.HasErrors);
				boundModuleVersionId.CopyAttributes(this);
				return boundModuleVersionId;
			}
			return this;
		}
	}
}
