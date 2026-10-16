using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000FA RID: 250
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PlainUnboundLambdaState : UnboundLambdaState
	{
		// Token: 0x06001304 RID: 4868 RVA: 0x00064A88 File Offset: 0x00062C88
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal PlainUnboundLambdaState([global::System.Runtime.CompilerServices.Nullable(1)] Binder binder, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<SyntaxList<AttributeListSyntax>> parameterAttributes, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> parameterNames, ImmutableArray<bool> parameterIsDiscardOpt, ImmutableArray<TypeWithAnnotations> parameterTypesWithAnnotations, ImmutableArray<RefKind> parameterRefKinds, bool isAsync, bool isStatic, bool includeCache)
			: base(binder, includeCache)
		{
			this._parameterAttributes = parameterAttributes;
			this._parameterNames = parameterNames;
			this._parameterIsDiscardOpt = parameterIsDiscardOpt;
			this._parameterTypesWithAnnotations = parameterTypesWithAnnotations;
			this._parameterRefKinds = parameterRefKinds;
			this._isAsync = isAsync;
			this._isStatic = isStatic;
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x00064AD4 File Offset: 0x00062CD4
		public override bool HasNames
		{
			get
			{
				return !this._parameterNames.IsDefault;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x00064AF4 File Offset: 0x00062CF4
		public override bool HasSignature
		{
			get
			{
				return !this._parameterNames.IsDefault;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x00064B14 File Offset: 0x00062D14
		public override bool HasExplicitlyTypedParameterList
		{
			get
			{
				return !this._parameterTypesWithAnnotations.IsDefault;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x00064B34 File Offset: 0x00062D34
		public override int ParameterCount
		{
			get
			{
				if (!this._parameterNames.IsDefault)
				{
					return this._parameterNames.Length;
				}
				return 0;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x00064B61 File Offset: 0x00062D61
		public override bool IsAsync
		{
			get
			{
				return this._isAsync;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x00064B69 File Offset: 0x00062D69
		public override bool IsStatic
		{
			get
			{
				return this._isStatic;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x00064B71 File Offset: 0x00062D71
		public override MessageID MessageID
		{
			get
			{
				if (base.UnboundLambda.Syntax.Kind() != SyntaxKind.AnonymousMethodExpression)
				{
					return MessageID.IDS_Lambda;
				}
				return MessageID.IDS_AnonMethod;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x00064B95 File Offset: 0x00062D95
		private CSharpSyntaxNode Body
		{
			get
			{
				return base.UnboundLambda.Syntax.AnonymousFunctionBody();
			}
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00064BA8 File Offset: 0x00062DA8
		public override Location ParameterLocation(int index)
		{
			SyntaxNode syntax = base.UnboundLambda.Syntax;
			switch (syntax.Kind())
			{
			case SyntaxKind.AnonymousMethodExpression:
				return ((AnonymousMethodExpressionSyntax)syntax).ParameterList.Parameters[index].Identifier.GetLocation();
			default:
				return ((SimpleLambdaExpressionSyntax)syntax).Parameter.Identifier.GetLocation();
			case SyntaxKind.ParenthesizedLambdaExpression:
				return ((ParenthesizedLambdaExpressionSyntax)syntax).ParameterList.Parameters[index].Identifier.GetLocation();
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x00064C46 File Offset: 0x00062E46
		private bool IsExpressionLambda
		{
			get
			{
				return this.Body.Kind() != SyntaxKind.Block;
			}
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00064C60 File Offset: 0x00062E60
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> ParameterAttributes(int index)
		{
			if (!this._parameterAttributes.IsDefault)
			{
				return this._parameterAttributes[index];
			}
			return default(SyntaxList<AttributeListSyntax>);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00064C98 File Offset: 0x00062E98
		public override string ParameterName(int index)
		{
			return this._parameterNames[index];
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00064CB4 File Offset: 0x00062EB4
		public override bool ParameterIsDiscard(int index)
		{
			return !this._parameterIsDiscardOpt.IsDefault && this._parameterIsDiscardOpt[index];
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00064CE4 File Offset: 0x00062EE4
		public override RefKind RefKind(int index)
		{
			if (!this._parameterRefKinds.IsDefault)
			{
				return this._parameterRefKinds[index];
			}
			return Microsoft.CodeAnalysis.RefKind.None;
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00064D14 File Offset: 0x00062F14
		public override TypeWithAnnotations ParameterTypeWithAnnotations(int index)
		{
			return this._parameterTypesWithAnnotations[index];
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00064D30 File Offset: 0x00062F30
		protected override UnboundLambdaState WithCachingCore(bool includeCache)
		{
			return new PlainUnboundLambdaState(this.Binder, this._parameterAttributes, this._parameterNames, this._parameterIsDiscardOpt, this._parameterTypesWithAnnotations, this._parameterRefKinds, this._isAsync, this._isStatic, includeCache);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00064D74 File Offset: 0x00062F74
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected override BoundExpression GetLambdaExpressionBody(BoundBlock body)
		{
			if (this.IsExpressionLambda)
			{
				ImmutableArray<BoundStatement> statements = body.Statements;
				if (statements.Length == 1)
				{
					BoundReturnStatement boundReturnStatement = statements[0] as BoundReturnStatement;
					if (boundReturnStatement != null && boundReturnStatement.RefKind == Microsoft.CodeAnalysis.RefKind.None)
					{
						BoundExpression expressionOpt = boundReturnStatement.ExpressionOpt;
						if (expressionOpt != null)
						{
							return expressionOpt;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00064DC1 File Offset: 0x00062FC1
		protected override BoundBlock CreateBlockFromLambdaExpressionBody(Binder lambdaBodyBinder, BoundExpression expression, BindingDiagnosticBag diagnostics)
		{
			return lambdaBodyBinder.CreateBlockFromExpression((ExpressionSyntax)this.Body, expression, diagnostics);
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00064DD6 File Offset: 0x00062FD6
		protected override BoundBlock BindLambdaBody(LambdaSymbol lambdaSymbol, Binder lambdaBodyBinder, BindingDiagnosticBag diagnostics)
		{
			if (this.IsExpressionLambda)
			{
				return lambdaBodyBinder.BindLambdaExpressionAsBlock((ExpressionSyntax)this.Body, diagnostics);
			}
			return lambdaBodyBinder.BindEmbeddedBlock((BlockSyntax)this.Body, diagnostics);
		}

		// Token: 0x040005DE RID: 1502
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		private readonly ImmutableArray<SyntaxList<AttributeListSyntax>> _parameterAttributes;

		// Token: 0x040005DF RID: 1503
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<string> _parameterNames;

		// Token: 0x040005E0 RID: 1504
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private readonly ImmutableArray<bool> _parameterIsDiscardOpt;

		// Token: 0x040005E1 RID: 1505
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private readonly ImmutableArray<TypeWithAnnotations> _parameterTypesWithAnnotations;

		// Token: 0x040005E2 RID: 1506
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private readonly ImmutableArray<RefKind> _parameterRefKinds;

		// Token: 0x040005E3 RID: 1507
		private readonly bool _isAsync;

		// Token: 0x040005E4 RID: 1508
		private readonly bool _isStatic;
	}
}
