using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200016E RID: 366
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundFunctionPointerLoad : BoundExpression
	{
		// Token: 0x06001DD9 RID: 7641 RVA: 0x0009FE4B File Offset: 0x0009E04B
		public BoundFunctionPointerLoad(SyntaxNode syntax, MethodSymbol targetMethod, TypeSymbol type, bool hasErrors)
			: base(BoundKind.FunctionPointerLoad, syntax, type, hasErrors)
		{
			this.TargetMethod = targetMethod;
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x0009FE60 File Offset: 0x0009E060
		public BoundFunctionPointerLoad(SyntaxNode syntax, MethodSymbol targetMethod, TypeSymbol type)
			: base(BoundKind.FunctionPointerLoad, syntax, type)
		{
			this.TargetMethod = targetMethod;
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x0009FE73 File Offset: 0x0009E073
		public MethodSymbol TargetMethod { get; }

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x0009FE7B File Offset: 0x0009E07B
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x0009FE83 File Offset: 0x0009E083
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitFunctionPointerLoad(this);
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x0009FE8C File Offset: 0x0009E08C
		public BoundFunctionPointerLoad Update(MethodSymbol targetMethod, TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(targetMethod, this.TargetMethod) || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundFunctionPointerLoad boundFunctionPointerLoad = new BoundFunctionPointerLoad(this.Syntax, targetMethod, type, base.HasErrors);
				boundFunctionPointerLoad.CopyAttributes(this);
				return boundFunctionPointerLoad;
			}
			return this;
		}
	}
}
