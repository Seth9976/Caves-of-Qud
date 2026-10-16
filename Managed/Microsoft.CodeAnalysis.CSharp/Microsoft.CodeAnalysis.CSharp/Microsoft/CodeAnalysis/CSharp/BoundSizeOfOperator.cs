using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000A6 RID: 166
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundSizeOfOperator : BoundExpression
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00055568 File Offset: 0x00053768
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ConstantValue ConstantValue
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.ConstantValueOpt;
			}
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00055570 File Offset: 0x00053770
		public BoundSizeOfOperator(SyntaxNode syntax, BoundTypeExpression sourceType, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.SizeOfOperator, syntax, type, hasErrors || sourceType.HasErrors())
		{
			this.SourceType = sourceType;
			this.ConstantValueOpt = constantValueOpt;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x00055598 File Offset: 0x00053798
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x000555A0 File Offset: 0x000537A0
		public BoundTypeExpression SourceType { get; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x000555A8 File Offset: 0x000537A8
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ConstantValue ConstantValueOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x000555B0 File Offset: 0x000537B0
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitSizeOfOperator(this);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x000555BC File Offset: 0x000537BC
		public BoundSizeOfOperator Update(BoundTypeExpression sourceType, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, TypeSymbol type)
		{
			if (sourceType != this.SourceType || constantValueOpt != this.ConstantValueOpt || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundSizeOfOperator boundSizeOfOperator = new BoundSizeOfOperator(this.Syntax, sourceType, constantValueOpt, type, base.HasErrors);
				boundSizeOfOperator.CopyAttributes(this);
				return boundSizeOfOperator;
			}
			return this;
		}
	}
}
