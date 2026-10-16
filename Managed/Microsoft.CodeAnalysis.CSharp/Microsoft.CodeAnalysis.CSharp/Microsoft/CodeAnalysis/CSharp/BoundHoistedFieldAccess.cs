using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001CF RID: 463
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundHoistedFieldAccess : BoundExpression
	{
		// Token: 0x06001FED RID: 8173 RVA: 0x000A3476 File Offset: 0x000A1676
		public BoundHoistedFieldAccess(SyntaxNode syntax, FieldSymbol fieldSymbol, TypeSymbol type, bool hasErrors)
			: base(BoundKind.HoistedFieldAccess, syntax, type, hasErrors)
		{
			this.FieldSymbol = fieldSymbol;
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x000A348E File Offset: 0x000A168E
		public BoundHoistedFieldAccess(SyntaxNode syntax, FieldSymbol fieldSymbol, TypeSymbol type)
			: base(BoundKind.HoistedFieldAccess, syntax, type)
		{
			this.FieldSymbol = fieldSymbol;
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x000A34A4 File Offset: 0x000A16A4
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x000A34AC File Offset: 0x000A16AC
		public FieldSymbol FieldSymbol { get; }

		// Token: 0x06001FF1 RID: 8177 RVA: 0x000A34B4 File Offset: 0x000A16B4
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitHoistedFieldAccess(this);
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x000A34BD File Offset: 0x000A16BD
		public BoundHoistedFieldAccess Update(FieldSymbol fieldSymbol, TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(fieldSymbol, this.FieldSymbol) || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundHoistedFieldAccess boundHoistedFieldAccess = new BoundHoistedFieldAccess(this.Syntax, fieldSymbol, type, base.HasErrors);
				boundHoistedFieldAccess.CopyAttributes(this);
				return boundHoistedFieldAccess;
			}
			return this;
		}
	}
}
