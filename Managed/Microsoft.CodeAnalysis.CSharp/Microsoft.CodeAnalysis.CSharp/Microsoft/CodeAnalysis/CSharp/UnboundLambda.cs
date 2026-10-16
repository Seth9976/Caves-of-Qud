using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000E5 RID: 229
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class UnboundLambda : BoundExpression
	{
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x0005ED9F File Offset: 0x0005CF9F
		public override object Display
		{
			get
			{
				return this.MessageID.Localize();
			}
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0005EDB4 File Offset: 0x0005CFB4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public UnboundLambda([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(1)] Binder binder, bool withDependencies, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<SyntaxList<AttributeListSyntax>> attributes, ImmutableArray<RefKind> refKinds, ImmutableArray<TypeWithAnnotations> types, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> names, ImmutableArray<bool> discardsOpt, bool isAsync, bool isStatic)
		{
			UnboundLambdaState unboundLambdaState = new PlainUnboundLambdaState(binder, attributes, names, discardsOpt, types, refKinds, isAsync, isStatic, true);
			bool flag;
			if (!types.IsDefault)
			{
				flag = types.Any<TypeWithAnnotations>(delegate(TypeWithAnnotations t)
				{
					TypeSymbol type = t.Type;
					return type != null && type.Kind == SymbolKind.ErrorType;
				});
			}
			else
			{
				flag = false;
			}
			this..ctor(syntax, unboundLambdaState, withDependencies, flag);
			this.Data.SetUnboundLambda(this);
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0005EE1C File Offset: 0x0005D01C
		private UnboundLambda(SyntaxNode syntax, UnboundLambdaState state, bool withDependencies, [global::System.Runtime.CompilerServices.Nullable(2)] NullableWalker.VariableState nullableState, bool hasErrors)
			: this(syntax, state, withDependencies, hasErrors)
		{
			this._nullableState = nullableState;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0005EE34 File Offset: 0x0005D034
		internal UnboundLambda WithNullableState(NullableWalker.VariableState nullableState)
		{
			UnboundLambdaState unboundLambdaState = this.Data.WithCaching(true);
			UnboundLambda unboundLambda = new UnboundLambda(this.Syntax, unboundLambdaState, this.WithDependencies, nullableState, base.HasErrors);
			unboundLambdaState.SetUnboundLambda(unboundLambda);
			return unboundLambda;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0005EE70 File Offset: 0x0005D070
		internal UnboundLambda WithNoCache()
		{
			UnboundLambdaState unboundLambdaState = this.Data.WithCaching(false);
			if (unboundLambdaState == this.Data)
			{
				return this;
			}
			UnboundLambda unboundLambda = new UnboundLambda(this.Syntax, unboundLambdaState, this.WithDependencies, this._nullableState, base.HasErrors);
			unboundLambdaState.SetUnboundLambda(unboundLambda);
			return unboundLambda;
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0005EEBC File Offset: 0x0005D0BC
		public MessageID MessageID
		{
			get
			{
				return this.Data.MessageID;
			}
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0005EEC9 File Offset: 0x0005D0C9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public NamedTypeSymbol InferDelegateType([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.Data.InferDelegateType(ref useSiteInfo);
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0005EED7 File Offset: 0x0005D0D7
		public BoundLambda Bind(NamedTypeSymbol delegateType)
		{
			return this.SuppressIfNeeded(this.Data.Bind(delegateType));
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0005EEEB File Offset: 0x0005D0EB
		public BoundLambda BindForErrorRecovery()
		{
			return this.SuppressIfNeeded(this.Data.BindForErrorRecovery());
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0005EEFE File Offset: 0x0005D0FE
		public BoundLambda BindForReturnTypeInference(NamedTypeSymbol delegateType)
		{
			return this.SuppressIfNeeded(this.Data.BindForReturnTypeInference(delegateType));
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0005EF12 File Offset: 0x0005D112
		private BoundLambda SuppressIfNeeded(BoundLambda lambda)
		{
			if (!base.IsSuppressed)
			{
				return lambda;
			}
			return (BoundLambda)lambda.WithSuppression(true);
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0005EF2A File Offset: 0x0005D12A
		public bool HasSignature
		{
			get
			{
				return this.Data.HasSignature;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x0005EF37 File Offset: 0x0005D137
		public bool HasExplicitlyTypedParameterList
		{
			get
			{
				return this.Data.HasExplicitlyTypedParameterList;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x0005EF44 File Offset: 0x0005D144
		public int ParameterCount
		{
			get
			{
				return this.Data.ParameterCount;
			}
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0005EF51 File Offset: 0x0005D151
		public TypeWithAnnotations InferReturnType(ConversionsBase conversions, NamedTypeSymbol delegateType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.BindForReturnTypeInference(delegateType).GetInferredReturnType(conversions, this._nullableState, ref useSiteInfo);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0005EF67 File Offset: 0x0005D167
		public RefKind RefKind(int index)
		{
			return this.Data.RefKind(index);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0005EF75 File Offset: 0x0005D175
		public void GenerateAnonymousFunctionConversionError(BindingDiagnosticBag diagnostics, TypeSymbol targetType)
		{
			this.Data.GenerateAnonymousFunctionConversionError(diagnostics, targetType);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0005EF84 File Offset: 0x0005D184
		public bool GenerateSummaryErrors(BindingDiagnosticBag diagnostics)
		{
			return this.Data.GenerateSummaryErrors(diagnostics);
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x0005EF92 File Offset: 0x0005D192
		public bool IsAsync
		{
			get
			{
				return this.Data.IsAsync;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x0005EF9F File Offset: 0x0005D19F
		public bool IsStatic
		{
			get
			{
				return this.Data.IsStatic;
			}
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0005EFAC File Offset: 0x0005D1AC
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<AttributeListSyntax> ParameterAttributes(int index)
		{
			return this.Data.ParameterAttributes(index);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0005EFBA File Offset: 0x0005D1BA
		public TypeWithAnnotations ParameterTypeWithAnnotations(int index)
		{
			return this.Data.ParameterTypeWithAnnotations(index);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0005EFC8 File Offset: 0x0005D1C8
		public TypeSymbol ParameterType(int index)
		{
			return this.ParameterTypeWithAnnotations(index).Type;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0005EFE4 File Offset: 0x0005D1E4
		public Location ParameterLocation(int index)
		{
			return this.Data.ParameterLocation(index);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0005EFF2 File Offset: 0x0005D1F2
		public string ParameterName(int index)
		{
			return this.Data.ParameterName(index);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0005F000 File Offset: 0x0005D200
		public bool ParameterIsDiscard(int index)
		{
			return this.Data.ParameterIsDiscard(index);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0005F00E File Offset: 0x0005D20E
		public UnboundLambda(SyntaxNode syntax, UnboundLambdaState data, bool withDependencies, bool hasErrors)
			: base(BoundKind.UnboundLambda, syntax, null, hasErrors)
		{
			this.Data = data;
			this.WithDependencies = withDependencies;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0005F02D File Offset: 0x0005D22D
		public UnboundLambda(SyntaxNode syntax, UnboundLambdaState data, bool withDependencies)
			: base(BoundKind.UnboundLambda, syntax, null)
		{
			this.Data = data;
			this.WithDependencies = withDependencies;
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0005F04A File Offset: 0x0005D24A
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x0005F052 File Offset: 0x0005D252
		public UnboundLambdaState Data { get; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x0005F05A File Offset: 0x0005D25A
		public bool WithDependencies { get; }

		// Token: 0x060011E3 RID: 4579 RVA: 0x0005F062 File Offset: 0x0005D262
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitUnboundLambda(this);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0005F06B File Offset: 0x0005D26B
		public UnboundLambda Update(UnboundLambdaState data, bool withDependencies)
		{
			if (data != this.Data || withDependencies != this.WithDependencies)
			{
				UnboundLambda unboundLambda = new UnboundLambda(this.Syntax, data, withDependencies, base.HasErrors);
				unboundLambda.CopyAttributes(this);
				return unboundLambda;
			}
			return this;
		}

		// Token: 0x040005B7 RID: 1463
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly NullableWalker.VariableState _nullableState;
	}
}
