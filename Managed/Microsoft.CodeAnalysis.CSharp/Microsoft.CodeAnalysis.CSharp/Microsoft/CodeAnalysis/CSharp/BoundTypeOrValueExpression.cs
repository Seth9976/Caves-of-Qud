using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200016D RID: 365
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundTypeOrValueExpression : BoundExpression
	{
		// Token: 0x06001DD3 RID: 7635 RVA: 0x0009FDCF File Offset: 0x0009DFCF
		public BoundTypeOrValueExpression(SyntaxNode syntax, BoundTypeOrValueData data, TypeSymbol type, bool hasErrors)
			: base(BoundKind.TypeOrValueExpression, syntax, type, hasErrors)
		{
			this.Data = data;
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0009FDE4 File Offset: 0x0009DFE4
		public BoundTypeOrValueExpression(SyntaxNode syntax, BoundTypeOrValueData data, TypeSymbol type)
			: base(BoundKind.TypeOrValueExpression, syntax, type)
		{
			this.Data = data;
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x0009FDF7 File Offset: 0x0009DFF7
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x0009FDFF File Offset: 0x0009DFFF
		public BoundTypeOrValueData Data { get; }

		// Token: 0x06001DD7 RID: 7639 RVA: 0x0009FE07 File Offset: 0x0009E007
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitTypeOrValueExpression(this);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x0009FE10 File Offset: 0x0009E010
		public BoundTypeOrValueExpression Update(BoundTypeOrValueData data, TypeSymbol type)
		{
			if (data != this.Data || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundTypeOrValueExpression boundTypeOrValueExpression = new BoundTypeOrValueExpression(this.Syntax, data, type, base.HasErrors);
				boundTypeOrValueExpression.CopyAttributes(this);
				return boundTypeOrValueExpression;
			}
			return this;
		}
	}
}
