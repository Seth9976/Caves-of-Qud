using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000CF RID: 207
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDefaultExpression : BoundExpression
	{
		// Token: 0x06001122 RID: 4386 RVA: 0x0005DC58 File Offset: 0x0005BE58
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundDefaultExpression(SyntaxNode syntax, TypeSymbol type, bool hasErrors = false)
			: this(syntax, null, type.GetDefaultValue(), type, hasErrors)
		{
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x0005DC6A File Offset: 0x0005BE6A
		public override ConstantValue ConstantValue
		{
			get
			{
				return this.ConstantValueOpt;
			}
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0005DC72 File Offset: 0x0005BE72
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundDefaultExpression(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundTypeExpression targetType, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.DefaultExpression, syntax, type, hasErrors || targetType.HasErrors())
		{
			this.TargetType = targetType;
			this.ConstantValueOpt = constantValueOpt;
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x0005DC9A File Offset: 0x0005BE9A
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x0005DCA2 File Offset: 0x0005BEA2
		public BoundTypeExpression TargetType { get; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x0005DCAA File Offset: 0x0005BEAA
		public ConstantValue ConstantValueOpt { get; }

		// Token: 0x06001128 RID: 4392 RVA: 0x0005DCB2 File Offset: 0x0005BEB2
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDefaultExpression(this);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0005DCBC File Offset: 0x0005BEBC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundDefaultExpression Update([global::System.Runtime.CompilerServices.Nullable(2)] BoundTypeExpression targetType, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, TypeSymbol type)
		{
			if (targetType != this.TargetType || constantValueOpt != this.ConstantValueOpt || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundDefaultExpression boundDefaultExpression = new BoundDefaultExpression(this.Syntax, targetType, constantValueOpt, type, base.HasErrors);
				boundDefaultExpression.CopyAttributes(this);
				return boundDefaultExpression;
			}
			return this;
		}
	}
}
