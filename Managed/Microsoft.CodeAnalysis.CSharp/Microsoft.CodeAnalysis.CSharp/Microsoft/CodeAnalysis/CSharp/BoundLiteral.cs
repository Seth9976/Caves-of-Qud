using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200009C RID: 156
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundLiteral : BoundExpression
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00054283 File Offset: 0x00052483
		public override ConstantValue ConstantValue
		{
			get
			{
				return this.ConstantValueOpt;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x0005428B File Offset: 0x0005248B
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public override object Display
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				ConstantValue constantValue = this.ConstantValue;
				if (constantValue == null || !constantValue.IsNull)
				{
					return base.Display;
				}
				return MessageID.IDS_NULL.Localize();
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x000542B7 File Offset: 0x000524B7
		public BoundLiteral([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, ConstantValue constantValueOpt, TypeSymbol type, bool hasErrors)
			: base(BoundKind.Literal, syntax, type, hasErrors)
		{
			this.ConstantValueOpt = constantValueOpt;
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x000542CC File Offset: 0x000524CC
		public BoundLiteral([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, ConstantValue constantValueOpt, TypeSymbol type)
			: base(BoundKind.Literal, syntax, type)
		{
			this.ConstantValueOpt = constantValueOpt;
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x000542DF File Offset: 0x000524DF
		public ConstantValue ConstantValueOpt { get; }

		// Token: 0x06000C44 RID: 3140 RVA: 0x000542E7 File Offset: 0x000524E7
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitLiteral(this);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000542F0 File Offset: 0x000524F0
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundLiteral Update(ConstantValue constantValueOpt, TypeSymbol type)
		{
			if (constantValueOpt != this.ConstantValueOpt || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundLiteral boundLiteral = new BoundLiteral(this.Syntax, constantValueOpt, type, base.HasErrors);
				boundLiteral.CopyAttributes(this);
				return boundLiteral;
			}
			return this;
		}
	}
}
