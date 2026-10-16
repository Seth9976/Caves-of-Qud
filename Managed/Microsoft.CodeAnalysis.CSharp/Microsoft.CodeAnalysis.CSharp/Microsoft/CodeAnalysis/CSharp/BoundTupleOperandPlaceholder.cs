using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000167 RID: 359
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundTupleOperandPlaceholder : BoundValuePlaceholderBase
	{
		// Token: 0x06001DB4 RID: 7604 RVA: 0x0009FB79 File Offset: 0x0009DD79
		public BoundTupleOperandPlaceholder(SyntaxNode syntax, TypeSymbol type, bool hasErrors)
			: base(BoundKind.TupleOperandPlaceholder, syntax, type, hasErrors)
		{
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x0009FB85 File Offset: 0x0009DD85
		public BoundTupleOperandPlaceholder(SyntaxNode syntax, TypeSymbol type)
			: base(BoundKind.TupleOperandPlaceholder, syntax, type)
		{
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x0009FB90 File Offset: 0x0009DD90
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0009FB98 File Offset: 0x0009DD98
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitTupleOperandPlaceholder(this);
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0009FBA1 File Offset: 0x0009DDA1
		public BoundTupleOperandPlaceholder Update(TypeSymbol type)
		{
			if (!TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundTupleOperandPlaceholder boundTupleOperandPlaceholder = new BoundTupleOperandPlaceholder(this.Syntax, type, base.HasErrors);
				boundTupleOperandPlaceholder.CopyAttributes(this);
				return boundTupleOperandPlaceholder;
			}
			return this;
		}
	}
}
