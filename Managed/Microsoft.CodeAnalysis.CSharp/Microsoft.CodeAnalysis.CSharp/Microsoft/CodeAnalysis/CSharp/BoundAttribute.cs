using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000A2 RID: 162
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundAttribute : BoundExpression
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x000550F3 File Offset: 0x000532F3
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ExpressionSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.Constructor;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x000550FC File Offset: 0x000532FC
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return StaticCast<BoundNode>.From<BoundExpression>(this.ConstructorArguments.AddRange(StaticCast<BoundExpression>.From<BoundAssignmentOperator>(this.NamedArguments)));
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00055128 File Offset: 0x00053328
		public BoundAttribute(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol constructor, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> constructorArguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> constructorArgumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> constructorArgumentsToParamsOpt, bool constructorExpanded, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundAssignmentOperator> namedArguments, LookupResultKind resultKind, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.Attribute, syntax, type, hasErrors || constructorArguments.HasErrors<BoundExpression>() || namedArguments.HasErrors<BoundAssignmentOperator>())
		{
			this.Constructor = constructor;
			this.ConstructorArguments = constructorArguments;
			this.ConstructorArgumentNamesOpt = constructorArgumentNamesOpt;
			this.ConstructorArgumentsToParamsOpt = constructorArgumentsToParamsOpt;
			this.ConstructorExpanded = constructorExpanded;
			this.NamedArguments = namedArguments;
			this._ResultKind = resultKind;
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0005518F File Offset: 0x0005338F
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x00055197 File Offset: 0x00053397
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public MethodSymbol Constructor
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0005519F File Offset: 0x0005339F
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> ConstructorArguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x000551A7 File Offset: 0x000533A7
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> ConstructorArgumentNamesOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x000551AF File Offset: 0x000533AF
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public ImmutableArray<int> ConstructorArgumentsToParamsOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get;
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x000551B7 File Offset: 0x000533B7
		public bool ConstructorExpanded { get; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x000551BF File Offset: 0x000533BF
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundAssignmentOperator> NamedArguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x000551C7 File Offset: 0x000533C7
		public override LookupResultKind ResultKind
		{
			get
			{
				return this._ResultKind;
			}
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x000551CF File Offset: 0x000533CF
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitAttribute(this);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x000551D8 File Offset: 0x000533D8
		public BoundAttribute Update([global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol constructor, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> constructorArguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> constructorArgumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> constructorArgumentsToParamsOpt, bool constructorExpanded, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundAssignmentOperator> namedArguments, LookupResultKind resultKind, TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(constructor, this.Constructor) || constructorArguments != this.ConstructorArguments || constructorArgumentNamesOpt != this.ConstructorArgumentNamesOpt || constructorArgumentsToParamsOpt != this.ConstructorArgumentsToParamsOpt || constructorExpanded != this.ConstructorExpanded || namedArguments != this.NamedArguments || resultKind != this.ResultKind || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundAttribute boundAttribute = new BoundAttribute(this.Syntax, constructor, constructorArguments, constructorArgumentNamesOpt, constructorArgumentsToParamsOpt, constructorExpanded, namedArguments, resultKind, type, base.HasErrors);
				boundAttribute.CopyAttributes(this);
				return boundAttribute;
			}
			return this;
		}

		// Token: 0x0400050B RID: 1291
		private readonly LookupResultKind _ResultKind;
	}
}
