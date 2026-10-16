using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000076 RID: 118
	internal sealed class UsingStatementBinder : LockOrUsingBinder
	{
		// Token: 0x06000ABC RID: 2748 RVA: 0x000506C4 File Offset: 0x0004E8C4
		public UsingStatementBinder(Binder enclosing, UsingStatementSyntax syntax)
			: base(enclosing)
		{
			this._syntax = syntax;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000506D4 File Offset: 0x0004E8D4
		protected override ImmutableArray<LocalSymbol> BuildLocals()
		{
			ExpressionSyntax targetExpressionSyntax = this.TargetExpressionSyntax;
			VariableDeclarationSyntax declaration = this._syntax.Declaration;
			if (targetExpressionSyntax != null)
			{
				ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
				ExpressionVariableFinder.FindExpressionVariables(this, instance, targetExpressionSyntax, null);
				return instance.ToImmutableAndFree();
			}
			ArrayBuilder<LocalSymbol> instance2 = ArrayBuilder<LocalSymbol>.GetInstance(declaration.Variables.Count);
			TypeSyntax type = declaration.Type;
			Action<ArrayRankSpecifierSyntax, ValueTuple<UsingStatementBinder, ArrayBuilder<LocalSymbol>>> action = delegate([global::System.Runtime.CompilerServices.Nullable(1)] ArrayRankSpecifierSyntax rankSpecifier, [TupleElementNames(new string[] { "binder", "locals" })] ValueTuple<UsingStatementBinder, ArrayBuilder<LocalSymbol>> args)
			{
				foreach (ExpressionSyntax expressionSyntax in rankSpecifier.Sizes)
				{
					if (expressionSyntax.Kind() != SyntaxKind.OmittedArraySizeExpression)
					{
						ExpressionVariableFinder.FindExpressionVariables(args.Item1, args.Item2, expressionSyntax, null);
					}
				}
			};
			ValueTuple<UsingStatementBinder, ArrayBuilder<LocalSymbol>> valueTuple = new ValueTuple<UsingStatementBinder, ArrayBuilder<LocalSymbol>>(this, instance2);
			type.VisitRankSpecifiers<ValueTuple<UsingStatementBinder, ArrayBuilder<LocalSymbol>>>(action, in valueTuple);
			foreach (VariableDeclaratorSyntax variableDeclaratorSyntax in declaration.Variables)
			{
				instance2.Add(base.MakeLocal(declaration, variableDeclaratorSyntax, LocalDeclarationKind.UsingVariable, null));
				ExpressionVariableFinder.FindExpressionVariables(this, instance2, variableDeclaratorSyntax, null);
			}
			return instance2.ToImmutableAndFree();
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0005079D File Offset: 0x0004E99D
		protected override ExpressionSyntax TargetExpressionSyntax
		{
			get
			{
				return this._syntax.Expression;
			}
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x000507AC File Offset: 0x0004E9AC
		internal override BoundStatement BindUsingStatementParts(BindingDiagnosticBag diagnostics, Binder originalBinder)
		{
			SyntaxNode targetExpressionSyntax = this.TargetExpressionSyntax;
			VariableDeclarationSyntax declaration = this._syntax.Declaration;
			this._syntax.AwaitKeyword.Kind();
			return UsingStatementBinder.BindUsingStatementOrDeclarationFromParts(targetExpressionSyntax ?? declaration, this._syntax.UsingKeyword, this._syntax.AwaitKeyword, originalBinder, this, diagnostics);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00050800 File Offset: 0x0004EA00
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static BoundStatement BindUsingStatementOrDeclarationFromParts(SyntaxNode syntax, SyntaxToken usingKeyword, SyntaxToken awaitKeyword, Binder originalBinder, [global::System.Runtime.CompilerServices.Nullable(2)] UsingStatementBinder usingBinderOpt, BindingDiagnosticBag diagnostics)
		{
			UsingStatementBinder.<>c__DisplayClass6_0 CS$<>8__locals1;
			CS$<>8__locals1.originalBinder = originalBinder;
			CS$<>8__locals1.diagnostics = diagnostics;
			CS$<>8__locals1.syntax = syntax;
			CS$<>8__locals1.usingBinderOpt = usingBinderOpt;
			bool flag = CS$<>8__locals1.syntax.Kind() == SyntaxKind.LocalDeclarationStatement;
			bool flag2 = !flag && CS$<>8__locals1.syntax.Kind() != SyntaxKind.VariableDeclaration;
			CS$<>8__locals1.hasAwait = awaitKeyword != default(SyntaxToken);
			CS$<>8__locals1.disposableInterface = UsingStatementBinder.<BindUsingStatementOrDeclarationFromParts>g__getDisposableInterface|6_2(CS$<>8__locals1.hasAwait, ref CS$<>8__locals1);
			bool flag3 = Binder.ReportUseSite(CS$<>8__locals1.disposableInterface, CS$<>8__locals1.diagnostics, CS$<>8__locals1.hasAwait ? awaitKeyword : usingKeyword);
			CS$<>8__locals1.declarationsOpt = default(ImmutableArray<BoundLocalDeclaration>);
			BoundMultipleLocalDeclarations boundMultipleLocalDeclarations = null;
			CS$<>8__locals1.expressionOpt = null;
			CS$<>8__locals1.declarationTypeOpt = null;
			Conversion implicitDynamic;
			MethodArgumentInfo methodArgumentInfo;
			TypeSymbol typeSymbol;
			if (flag2)
			{
				CS$<>8__locals1.expressionOpt = CS$<>8__locals1.usingBinderOpt.BindTargetExpression(CS$<>8__locals1.diagnostics, CS$<>8__locals1.originalBinder, null);
				flag3 |= !UsingStatementBinder.<BindUsingStatementOrDeclarationFromParts>g__populateDisposableConversionOrDisposeMethod|6_0(true, out implicitDynamic, out methodArgumentInfo, out typeSymbol, ref CS$<>8__locals1);
			}
			else
			{
				VariableDeclarationSyntax variableDeclarationSyntax = (flag ? ((LocalDeclarationStatementSyntax)CS$<>8__locals1.syntax).Declaration : ((VariableDeclarationSyntax)CS$<>8__locals1.syntax));
				CS$<>8__locals1.originalBinder.BindForOrUsingOrFixedDeclarations(variableDeclarationSyntax, LocalDeclarationKind.UsingVariable, CS$<>8__locals1.diagnostics, out CS$<>8__locals1.declarationsOpt);
				boundMultipleLocalDeclarations = new BoundMultipleLocalDeclarations(variableDeclarationSyntax, CS$<>8__locals1.declarationsOpt, false);
				CS$<>8__locals1.declarationTypeOpt = CS$<>8__locals1.declarationsOpt[0].DeclaredTypeOpt.Type;
				if (CS$<>8__locals1.declarationTypeOpt.IsDynamic())
				{
					implicitDynamic = Conversion.ImplicitDynamic;
					methodArgumentInfo = null;
					typeSymbol = null;
				}
				else
				{
					flag3 |= !UsingStatementBinder.<BindUsingStatementOrDeclarationFromParts>g__populateDisposableConversionOrDisposeMethod|6_0(false, out implicitDynamic, out methodArgumentInfo, out typeSymbol, ref CS$<>8__locals1);
				}
			}
			BoundAwaitableInfo boundAwaitableInfo = null;
			if (CS$<>8__locals1.hasAwait)
			{
				CS$<>8__locals1.originalBinder.ReportBadAwaitDiagnostics(CS$<>8__locals1.syntax, awaitKeyword.GetLocation(), CS$<>8__locals1.diagnostics, ref flag3);
				if (typeSymbol == null)
				{
					boundAwaitableInfo = new BoundAwaitableInfo(CS$<>8__locals1.syntax, null, true, null, null, null, false)
					{
						WasCompilerGenerated = true
					};
				}
				else
				{
					flag3 |= Binder.ReportUseSite(typeSymbol, CS$<>8__locals1.diagnostics, awaitKeyword);
					BoundAwaitableValuePlaceholder boundAwaitableValuePlaceholder = new BoundAwaitableValuePlaceholder(CS$<>8__locals1.syntax, CS$<>8__locals1.originalBinder.LocalScopeDepth, typeSymbol).MakeCompilerGenerated<BoundAwaitableValuePlaceholder>();
					boundAwaitableInfo = CS$<>8__locals1.originalBinder.BindAwaitInfo(boundAwaitableValuePlaceholder, CS$<>8__locals1.syntax, CS$<>8__locals1.diagnostics, ref flag3, null);
				}
			}
			if (flag)
			{
				return new BoundUsingLocalDeclarations(CS$<>8__locals1.syntax, methodArgumentInfo, implicitDynamic, boundAwaitableInfo, CS$<>8__locals1.declarationsOpt, flag3);
			}
			BoundStatement boundStatement = CS$<>8__locals1.originalBinder.BindPossibleEmbeddedStatement(CS$<>8__locals1.usingBinderOpt._syntax.Statement, CS$<>8__locals1.diagnostics);
			return new BoundUsingStatement(CS$<>8__locals1.usingBinderOpt._syntax, CS$<>8__locals1.usingBinderOpt.Locals, boundMultipleLocalDeclarations, CS$<>8__locals1.expressionOpt, implicitDynamic, boundStatement, boundAwaitableInfo, methodArgumentInfo, flag3);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00050AA0 File Offset: 0x0004ECA0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<LocalSymbol> GetDeclaredLocalsForScope(SyntaxNode scopeDesignator)
		{
			if (this._syntax == scopeDesignator)
			{
				return this.Locals;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00050AB7 File Offset: 0x0004ECB7
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<LocalFunctionSymbol> GetDeclaredLocalFunctionsForScope(CSharpSyntaxNode scopeDesignator)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00050ABE File Offset: 0x0004ECBE
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal override SyntaxNode ScopeDesignator
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this._syntax;
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00050AC8 File Offset: 0x0004ECC8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		internal static bool <BindUsingStatementOrDeclarationFromParts>g__populateDisposableConversionOrDisposeMethod|6_0(bool fromExpression, out Conversion iDisposableConversion, out MethodArgumentInfo patternDisposeInfo, out TypeSymbol awaitableType, ref UsingStatementBinder.<>c__DisplayClass6_0 A_4)
		{
			CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = A_4.originalBinder.GetNewCompoundUseSiteInfo(A_4.diagnostics);
			iDisposableConversion = UsingStatementBinder.<BindUsingStatementOrDeclarationFromParts>g__classifyConversion|6_1(fromExpression, A_4.disposableInterface, ref newCompoundUseSiteInfo, ref A_4);
			patternDisposeInfo = null;
			awaitableType = null;
			A_4.diagnostics.Add(A_4.syntax, newCompoundUseSiteInfo);
			if (iDisposableConversion.IsImplicit)
			{
				if (A_4.hasAwait)
				{
					awaitableType = A_4.originalBinder.Compilation.GetWellKnownType(WellKnownType.System_Threading_Tasks_ValueTask);
				}
				return true;
			}
			TypeSymbol typeSymbol = (fromExpression ? A_4.expressionOpt.Type : A_4.declarationTypeOpt);
			if (typeSymbol != null && (typeSymbol.IsRefLikeType | A_4.hasAwait))
			{
				BoundLocal boundLocal;
				if (!fromExpression)
				{
					(boundLocal = new BoundLocal(A_4.syntax, A_4.declarationsOpt[0].LocalSymbol, null, typeSymbol, false)).WasCompilerGenerated = true;
				}
				else
				{
					boundLocal = A_4.expressionOpt;
				}
				BoundExpression boundExpression = boundLocal;
				BindingDiagnosticBag bindingDiagnosticBag = (A_4.originalBinder.Compilation.IsFeatureEnabled(MessageID.IDS_FeatureUsingDeclarations) ? A_4.diagnostics : BindingDiagnosticBag.Discarded);
				MethodSymbol methodSymbol = A_4.originalBinder.TryFindDisposePatternMethod(boundExpression, A_4.syntax, A_4.hasAwait, bindingDiagnosticBag);
				if (methodSymbol != null)
				{
					MessageID.IDS_FeatureUsingDeclarations.CheckFeatureAvailability(A_4.diagnostics, A_4.originalBinder.Compilation, A_4.syntax.Location);
					ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(methodSymbol.ParameterCount);
					ImmutableArray<int> immutableArray = default(ImmutableArray<int>);
					bool flag = methodSymbol.HasParamsParameter();
					Binder originalBinder = A_4.originalBinder;
					UsingStatementBinder usingBinderOpt = A_4.usingBinderOpt;
					BitVector bitVector;
					originalBinder.BindDefaultArguments(((usingBinderOpt != null) ? usingBinderOpt._syntax : null) ?? A_4.syntax, methodSymbol.Parameters, instance, null, ref immutableArray, out bitVector, flag, true, bindingDiagnosticBag, true);
					patternDisposeInfo = new MethodArgumentInfo(methodSymbol, instance.ToImmutableAndFree(), immutableArray, bitVector, flag);
					if (A_4.hasAwait)
					{
						awaitableType = methodSymbol.ReturnType;
					}
					return true;
				}
			}
			if (typeSymbol == null || !typeSymbol.IsErrorType())
			{
				TypeSymbol typeSymbol2 = UsingStatementBinder.<BindUsingStatementOrDeclarationFromParts>g__getDisposableInterface|6_2(!A_4.hasAwait, ref A_4);
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				ErrorCode errorCode = (UsingStatementBinder.<BindUsingStatementOrDeclarationFromParts>g__classifyConversion|6_1(fromExpression, typeSymbol2, ref discarded, ref A_4).IsImplicit ? (A_4.hasAwait ? ErrorCode.ERR_NoConvToIAsyncDispWrongAsync : ErrorCode.ERR_NoConvToIDispWrongAsync) : (A_4.hasAwait ? ErrorCode.ERR_NoConvToIAsyncDisp : ErrorCode.ERR_NoConvToIDisp));
				Binder.Error(A_4.diagnostics, errorCode, A_4.syntax, new object[] { A_4.declarationTypeOpt ?? A_4.expressionOpt.Display });
			}
			return false;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00050D51 File Offset: 0x0004EF51
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[CompilerGenerated]
		internal static Conversion <BindUsingStatementOrDeclarationFromParts>g__classifyConversion|6_1(bool fromExpression, TypeSymbol targetInterface, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, ref UsingStatementBinder.<>c__DisplayClass6_0 A_3)
		{
			if (!fromExpression)
			{
				return A_3.originalBinder.Conversions.ClassifyImplicitConversionFromType(A_3.declarationTypeOpt, targetInterface, ref useSiteInfo);
			}
			return A_3.originalBinder.Conversions.ClassifyImplicitConversionFromExpression(A_3.expressionOpt, targetInterface, ref useSiteInfo);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00050D87 File Offset: 0x0004EF87
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[CompilerGenerated]
		internal static TypeSymbol <BindUsingStatementOrDeclarationFromParts>g__getDisposableInterface|6_2(bool isAsync, ref UsingStatementBinder.<>c__DisplayClass6_0 A_1)
		{
			if (!isAsync)
			{
				return A_1.originalBinder.Compilation.GetSpecialType(SpecialType.System_IDisposable);
			}
			return A_1.originalBinder.Compilation.GetWellKnownType(WellKnownType.System_IAsyncDisposable);
		}

		// Token: 0x04000475 RID: 1141
		private readonly UsingStatementSyntax _syntax;
	}
}
