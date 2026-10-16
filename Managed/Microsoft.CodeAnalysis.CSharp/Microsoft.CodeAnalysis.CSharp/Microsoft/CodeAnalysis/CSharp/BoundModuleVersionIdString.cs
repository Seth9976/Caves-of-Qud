using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200017E RID: 382
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundModuleVersionIdString : BoundExpression
	{
		// Token: 0x06001E37 RID: 7735 RVA: 0x000A0715 File Offset: 0x0009E915
		public BoundModuleVersionIdString(SyntaxNode syntax, TypeSymbol type, bool hasErrors)
			: base(BoundKind.ModuleVersionIdString, syntax, type, hasErrors)
		{
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x000A0722 File Offset: 0x0009E922
		public BoundModuleVersionIdString(SyntaxNode syntax, TypeSymbol type)
			: base(BoundKind.ModuleVersionIdString, syntax, type)
		{
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x000A072E File Offset: 0x0009E92E
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x000A0736 File Offset: 0x0009E936
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitModuleVersionIdString(this);
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x000A073F File Offset: 0x0009E93F
		public BoundModuleVersionIdString Update(TypeSymbol type)
		{
			if (!TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundModuleVersionIdString boundModuleVersionIdString = new BoundModuleVersionIdString(this.Syntax, type, base.HasErrors);
				boundModuleVersionIdString.CopyAttributes(this);
				return boundModuleVersionIdString;
			}
			return this;
		}
	}
}
