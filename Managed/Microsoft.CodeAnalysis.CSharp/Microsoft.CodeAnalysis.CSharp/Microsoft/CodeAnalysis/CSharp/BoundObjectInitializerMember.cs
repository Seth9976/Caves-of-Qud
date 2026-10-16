using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000A9 RID: 169
	internal sealed class BoundObjectInitializerMember : BoundExpression, IBoundInvalidNode
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00055736 File Offset: 0x00053936
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ExpressionSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.MemberSymbol;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0005573E File Offset: 0x0005393E
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<BoundNode> IBoundInvalidNode.InvalidNodeChildren
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return StaticCast<BoundNode>.From<BoundExpression>(this.Arguments);
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0005574C File Offset: 0x0005394C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundObjectInitializerMember(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] Symbol memberSymbol, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, LookupResultKind resultKind, TypeSymbol receiverType, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.ObjectInitializerMember, syntax, type, hasErrors || arguments.HasErrors<BoundExpression>())
		{
			this.MemberSymbol = memberSymbol;
			this.Arguments = arguments;
			this.ArgumentNamesOpt = argumentNamesOpt;
			this.ArgumentRefKindsOpt = argumentRefKindsOpt;
			this.Expanded = expanded;
			this.ArgsToParamsOpt = argsToParamsOpt;
			this.DefaultArguments = defaultArguments;
			this._ResultKind = resultKind;
			this.ReceiverType = receiverType;
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x000557BA File Offset: 0x000539BA
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x000557C2 File Offset: 0x000539C2
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public Symbol MemberSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x000557CA File Offset: 0x000539CA
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x000557D2 File Offset: 0x000539D2
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> ArgumentNamesOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x000557DA File Offset: 0x000539DA
		public ImmutableArray<RefKind> ArgumentRefKindsOpt { get; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x000557E2 File Offset: 0x000539E2
		public bool Expanded { get; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x000557EA File Offset: 0x000539EA
		public ImmutableArray<int> ArgsToParamsOpt { get; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x000557F2 File Offset: 0x000539F2
		public BitVector DefaultArguments { get; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x000557FA File Offset: 0x000539FA
		public override LookupResultKind ResultKind
		{
			get
			{
				return this._ResultKind;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00055802 File Offset: 0x00053A02
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public TypeSymbol ReceiverType
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0005580A File Offset: 0x00053A0A
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitObjectInitializerMember(this);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00055814 File Offset: 0x00053A14
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundObjectInitializerMember Update([global::System.Runtime.CompilerServices.Nullable(2)] Symbol memberSymbol, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, LookupResultKind resultKind, TypeSymbol receiverType, TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(memberSymbol, this.MemberSymbol) || arguments != this.Arguments || argumentNamesOpt != this.ArgumentNamesOpt || argumentRefKindsOpt != this.ArgumentRefKindsOpt || expanded != this.Expanded || argsToParamsOpt != this.ArgsToParamsOpt || defaultArguments != this.DefaultArguments || resultKind != this.ResultKind || !TypeSymbol.Equals(receiverType, this.ReceiverType, TypeCompareKind.ConsiderEverything) || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundObjectInitializerMember boundObjectInitializerMember = new BoundObjectInitializerMember(this.Syntax, memberSymbol, arguments, argumentNamesOpt, argumentRefKindsOpt, expanded, argsToParamsOpt, defaultArguments, resultKind, receiverType, type, base.HasErrors);
				boundObjectInitializerMember.CopyAttributes(this);
				return boundObjectInitializerMember;
			}
			return this;
		}

		// Token: 0x04000524 RID: 1316
		private readonly LookupResultKind _ResultKind;
	}
}
