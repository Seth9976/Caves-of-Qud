using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000D4 RID: 212
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundQueryClause : BoundExpression
	{
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0005DF9F File Offset: 0x0005C19F
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return ImmutableArray.Create<BoundNode>(this.Value);
			}
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0005DFAC File Offset: 0x0005C1AC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundQueryClause(SyntaxNode syntax, BoundExpression value, [global::System.Runtime.CompilerServices.Nullable(2)] RangeVariableSymbol definedSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression operation, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression cast, Binder binder, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression unoptimizedForm, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.QueryClause, syntax, type, hasErrors || value.HasErrors() || operation.HasErrors() || cast.HasErrors() || unoptimizedForm.HasErrors())
		{
			this.Value = value;
			this.DefinedSymbol = definedSymbol;
			this.Operation = operation;
			this.Cast = cast;
			this.Binder = binder;
			this.UnoptimizedForm = unoptimizedForm;
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x0005E01D File Offset: 0x0005C21D
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x0005E025 File Offset: 0x0005C225
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundExpression Value
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x0005E02D File Offset: 0x0005C22D
		public RangeVariableSymbol DefinedSymbol { get; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x0005E035 File Offset: 0x0005C235
		public BoundExpression Operation { get; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0005E03D File Offset: 0x0005C23D
		public BoundExpression Cast { get; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x0005E045 File Offset: 0x0005C245
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public Binder Binder
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x0005E04D File Offset: 0x0005C24D
		public BoundExpression UnoptimizedForm { get; }

		// Token: 0x0600114D RID: 4429 RVA: 0x0005E055 File Offset: 0x0005C255
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitQueryClause(this);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0005E060 File Offset: 0x0005C260
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundQueryClause Update(BoundExpression value, [global::System.Runtime.CompilerServices.Nullable(2)] RangeVariableSymbol definedSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression operation, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression cast, Binder binder, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression unoptimizedForm, TypeSymbol type)
		{
			if (value != this.Value || !SymbolEqualityComparer.ConsiderEverything.Equals(definedSymbol, this.DefinedSymbol) || operation != this.Operation || cast != this.Cast || binder != this.Binder || unoptimizedForm != this.UnoptimizedForm || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundQueryClause boundQueryClause = new BoundQueryClause(this.Syntax, value, definedSymbol, operation, cast, binder, unoptimizedForm, type, base.HasErrors);
				boundQueryClause.CopyAttributes(this);
				return boundQueryClause;
			}
			return this;
		}
	}
}
