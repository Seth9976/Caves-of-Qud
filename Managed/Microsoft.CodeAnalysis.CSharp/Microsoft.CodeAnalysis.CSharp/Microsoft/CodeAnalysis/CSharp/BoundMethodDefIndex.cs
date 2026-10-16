using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200017A RID: 378
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundMethodDefIndex : BoundExpression
	{
		// Token: 0x06001E21 RID: 7713 RVA: 0x000A0571 File Offset: 0x0009E771
		public BoundMethodDefIndex(SyntaxNode syntax, MethodSymbol method, TypeSymbol type, bool hasErrors)
			: base(BoundKind.MethodDefIndex, syntax, type, hasErrors)
		{
			this.Method = method;
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x000A0586 File Offset: 0x0009E786
		public BoundMethodDefIndex(SyntaxNode syntax, MethodSymbol method, TypeSymbol type)
			: base(BoundKind.MethodDefIndex, syntax, type)
		{
			this.Method = method;
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x000A0599 File Offset: 0x0009E799
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x000A05A1 File Offset: 0x0009E7A1
		public MethodSymbol Method { get; }

		// Token: 0x06001E25 RID: 7717 RVA: 0x000A05A9 File Offset: 0x0009E7A9
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitMethodDefIndex(this);
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x000A05B2 File Offset: 0x0009E7B2
		public BoundMethodDefIndex Update(MethodSymbol method, TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(method, this.Method) || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundMethodDefIndex boundMethodDefIndex = new BoundMethodDefIndex(this.Syntax, method, type, base.HasErrors);
				boundMethodDefIndex.CopyAttributes(this);
				return boundMethodDefIndex;
			}
			return this;
		}
	}
}
