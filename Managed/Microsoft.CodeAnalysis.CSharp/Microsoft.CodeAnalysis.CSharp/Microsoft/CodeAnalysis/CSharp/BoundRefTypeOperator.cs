using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000D7 RID: 215
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundRefTypeOperator : BoundExpression
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x0005E267 File Offset: 0x0005C467
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return ImmutableArray.Create<BoundNode>(this.Operand);
			}
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0005E274 File Offset: 0x0005C474
		public BoundRefTypeOperator(SyntaxNode syntax, BoundExpression operand, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol getTypeFromHandle, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.RefTypeOperator, syntax, type, hasErrors || operand.HasErrors())
		{
			this.Operand = operand;
			this.GetTypeFromHandle = getTypeFromHandle;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x0005E29C File Offset: 0x0005C49C
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x0005E2A4 File Offset: 0x0005C4A4
		public BoundExpression Operand { get; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x0005E2AC File Offset: 0x0005C4AC
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public MethodSymbol GetTypeFromHandle
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0005E2B4 File Offset: 0x0005C4B4
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitRefTypeOperator(this);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0005E2C0 File Offset: 0x0005C4C0
		public BoundRefTypeOperator Update(BoundExpression operand, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol getTypeFromHandle, TypeSymbol type)
		{
			if (operand != this.Operand || !SymbolEqualityComparer.ConsiderEverything.Equals(getTypeFromHandle, this.GetTypeFromHandle) || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundRefTypeOperator boundRefTypeOperator = new BoundRefTypeOperator(this.Syntax, operand, getTypeFromHandle, type, base.HasErrors);
				boundRefTypeOperator.CopyAttributes(this);
				return boundRefTypeOperator;
			}
			return this;
		}
	}
}
