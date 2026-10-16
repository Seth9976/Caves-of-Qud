using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000027 RID: 39
	internal sealed class ForEachLoopBinder : LoopBinder
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x000364DC File Offset: 0x000346DC
		private SourceLocalSymbol IterationVariable
		{
			get
			{
				if (this._syntax.Kind() != SyntaxKind.ForEachStatement)
				{
					return null;
				}
				return (SourceLocalSymbol)this.Locals[0];
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00036514 File Offset: 0x00034714
		private bool IsAsync
		{
			get
			{
				return this._syntax.AwaitKeyword != default(SyntaxToken);
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0003653A File Offset: 0x0003473A
		public ForEachLoopBinder(Binder enclosing, CommonForEachStatementSyntax syntax)
			: base(enclosing)
		{
			this._syntax = syntax;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0003654C File Offset: 0x0003474C
		protected override ImmutableArray<LocalSymbol> BuildLocals()
		{
			SyntaxKind syntaxKind = this._syntax.Kind();
			if (syntaxKind == SyntaxKind.ForEachStatement)
			{
				ForEachStatementSyntax forEachStatementSyntax = (ForEachStatementSyntax)this._syntax;
				return ImmutableArray.Create<LocalSymbol>(SourceLocalSymbol.MakeForeachLocal((MethodSymbol)this.ContainingMemberOrLambda, this, forEachStatementSyntax.Type, forEachStatementSyntax.Identifier, forEachStatementSyntax.Expression));
			}
			if (syntaxKind == SyntaxKind.ForEachVariableStatement)
			{
				ForEachVariableStatementSyntax forEachVariableStatementSyntax = (ForEachVariableStatementSyntax)this._syntax;
				ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
				this.CollectLocalsFromDeconstruction(forEachVariableStatementSyntax.Variable, LocalDeclarationKind.ForEachIterationVariable, instance, forEachVariableStatementSyntax, null);
				return instance.ToImmutableAndFree();
			}
			throw ExceptionUtilities.UnexpectedValue(this._syntax.Kind());
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000365E8 File Offset: 0x000347E8
		internal void CollectLocalsFromDeconstruction(ExpressionSyntax declaration, LocalDeclarationKind kind, ArrayBuilder<LocalSymbol> locals, SyntaxNode deconstructionStatement, Binder enclosingBinderOpt = null)
		{
			SyntaxKind syntaxKind = declaration.Kind();
			if (syntaxKind != SyntaxKind.IdentifierName)
			{
				if (syntaxKind == SyntaxKind.TupleExpression)
				{
					foreach (ArgumentSyntax argumentSyntax in ((TupleExpressionSyntax)declaration).Arguments)
					{
						this.CollectLocalsFromDeconstruction(argumentSyntax.Expression, kind, locals, deconstructionStatement, enclosingBinderOpt);
					}
					return;
				}
				if (syntaxKind == SyntaxKind.DeclarationExpression)
				{
					DeclarationExpressionSyntax declarationExpressionSyntax = (DeclarationExpressionSyntax)declaration;
					this.CollectLocalsFromDeconstruction(declarationExpressionSyntax.Designation, declarationExpressionSyntax.Type, kind, locals, deconstructionStatement, enclosingBinderOpt);
					return;
				}
				ExpressionVariableFinder.FindExpressionVariables(this, locals, declaration, null);
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0003667C File Offset: 0x0003487C
		internal void CollectLocalsFromDeconstruction(VariableDesignationSyntax designation, TypeSyntax closestTypeSyntax, LocalDeclarationKind kind, ArrayBuilder<LocalSymbol> locals, SyntaxNode deconstructionStatement, Binder enclosingBinderOpt)
		{
			SyntaxKind syntaxKind = designation.Kind();
			if (syntaxKind == SyntaxKind.SingleVariableDesignation)
			{
				SingleVariableDesignationSyntax singleVariableDesignationSyntax = (SingleVariableDesignationSyntax)designation;
				SourceLocalSymbol sourceLocalSymbol = SourceLocalSymbol.MakeDeconstructionLocal(this.ContainingMemberOrLambda, this, enclosingBinderOpt ?? this, closestTypeSyntax, singleVariableDesignationSyntax.Identifier, kind, deconstructionStatement);
				locals.Add(sourceLocalSymbol);
				return;
			}
			if (syntaxKind == SyntaxKind.ParenthesizedVariableDesignation)
			{
				foreach (VariableDesignationSyntax variableDesignationSyntax in ((ParenthesizedVariableDesignationSyntax)designation).Variables)
				{
					this.CollectLocalsFromDeconstruction(variableDesignationSyntax, closestTypeSyntax, kind, locals, deconstructionStatement, enclosingBinderOpt);
				}
				return;
			}
			if (syntaxKind != SyntaxKind.DiscardDesignation)
			{
				throw ExceptionUtilities.UnexpectedValue(designation.Kind());
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00036723 File Offset: 0x00034923
		internal override BoundStatement BindForEachParts(BindingDiagnosticBag diagnostics, Binder originalBinder)
		{
			return this.BindForEachPartsWorker(diagnostics, originalBinder);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00036730 File Offset: 0x00034930
		internal override BoundStatement BindForEachDeconstruction(BindingDiagnosticBag diagnostics, Binder originalBinder)
		{
			BoundExpression boundExpression = originalBinder.GetBinder(this._syntax.Expression).BindRValueWithoutTargetType(this._syntax.Expression, diagnostics, true);
			ForEachEnumeratorInfo.Builder builder = default(ForEachEnumeratorInfo.Builder);
			TypeWithAnnotations typeWithAnnotations;
			this.GetEnumeratorInfoAndInferCollectionElementType(ref builder, ref boundExpression, diagnostics, out typeWithAnnotations);
			ExpressionSyntax variable = ((ForEachVariableStatementSyntax)this._syntax).Variable;
			BoundDeconstructValuePlaceholder boundDeconstructValuePlaceholder = new BoundDeconstructValuePlaceholder(this._syntax.Expression, this.LocalScopeDepth, typeWithAnnotations.Type ?? base.CreateErrorType("var"));
			DeclarationExpressionSyntax declarationExpressionSyntax = null;
			ExpressionSyntax expressionSyntax = null;
			BoundDeconstructionAssignmentOperator boundDeconstructionAssignmentOperator = base.BindDeconstruction(variable, variable, this._syntax.Expression, diagnostics, ref declarationExpressionSyntax, ref expressionSyntax, false, boundDeconstructValuePlaceholder);
			return new BoundExpressionStatement(this._syntax, boundDeconstructionAssignmentOperator, false);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000367E8 File Offset: 0x000349E8
		private BoundForEachStatement BindForEachPartsWorker(BindingDiagnosticBag diagnostics, Binder originalBinder)
		{
			BoundExpression boundExpression = originalBinder.GetBinder(this._syntax.Expression).BindRValueWithoutTargetType(this._syntax.Expression, diagnostics, true);
			ForEachEnumeratorInfo.Builder builder = default(ForEachEnumeratorInfo.Builder);
			TypeWithAnnotations typeWithAnnotations;
			bool flag = !this.GetEnumeratorInfoAndInferCollectionElementType(ref builder, ref boundExpression, diagnostics, out typeWithAnnotations);
			flag |= builder.IsIncomplete;
			BoundAwaitableInfo boundAwaitableInfo = null;
			MethodArgumentInfo getEnumeratorInfo = builder.GetEnumeratorInfo;
			MethodSymbol methodSymbol = ((getEnumeratorInfo != null) ? getEnumeratorInfo.Method : null);
			if (methodSymbol != null)
			{
				originalBinder.CheckImplicitThisCopyInReadOnlyMember(boundExpression, methodSymbol, diagnostics);
				if (methodSymbol.IsExtensionMethod && !flag)
				{
					MessageID messageID = (this.IsAsync ? MessageID.IDS_FeatureExtensionGetAsyncEnumerator : MessageID.IDS_FeatureExtensionGetEnumerator);
					flag |= !messageID.CheckFeatureAvailability(diagnostics, base.Compilation, boundExpression.Syntax.Location);
					ImmutableArray<RefKind> parameterRefKinds = methodSymbol.ParameterRefKinds;
					if (!parameterRefKinds.IsDefault && parameterRefKinds[0] == RefKind.Ref)
					{
						Binder.Error(diagnostics, ErrorCode.ERR_RefLvalueExpected, boundExpression.Syntax);
						flag = true;
					}
				}
			}
			if (this.IsAsync)
			{
				ExpressionSyntax expression = this._syntax.Expression;
				base.ReportBadAwaitDiagnostics(expression, this._syntax.AwaitKeyword.GetLocation(), diagnostics, ref flag);
				SyntaxNode syntaxNode = expression;
				uint localScopeDepth = this.LocalScopeDepth;
				MethodArgumentInfo moveNextInfo = builder.MoveNextInfo;
				BoundAwaitableValuePlaceholder boundAwaitableValuePlaceholder = new BoundAwaitableValuePlaceholder(syntaxNode, localScopeDepth, ((moveNextInfo != null) ? moveNextInfo.Method.ReturnType : null) ?? base.CreateErrorType(""));
				boundAwaitableInfo = base.BindAwaitInfo(boundAwaitableValuePlaceholder, expression, diagnostics, ref flag, null);
				if (!flag)
				{
					MethodSymbol getResult = boundAwaitableInfo.GetResult;
					if (getResult == null || getResult.ReturnType.SpecialType != SpecialType.System_Boolean)
					{
						diagnostics.Add(ErrorCode.ERR_BadGetAsyncEnumerator, expression.Location, new object[] { methodSymbol.ReturnTypeWithAnnotations, methodSymbol });
						flag = true;
					}
				}
			}
			bool flag2 = false;
			BoundForEachDeconstructStep boundForEachDeconstructStep = null;
			BoundExpression boundExpression2 = null;
			uint valEscape = Binder.GetValEscape(boundExpression, this.LocalScopeDepth);
			SyntaxKind syntaxKind = this._syntax.Kind();
			TypeWithAnnotations typeWithAnnotations2;
			BoundTypeExpression boundTypeExpression;
			if (syntaxKind != SyntaxKind.ForEachStatement)
			{
				if (syntaxKind != SyntaxKind.ForEachVariableStatement)
				{
					throw ExceptionUtilities.UnexpectedValue(this._syntax.Kind());
				}
				ForEachVariableStatementSyntax forEachVariableStatementSyntax = (ForEachVariableStatementSyntax)this._syntax;
				typeWithAnnotations2 = (typeWithAnnotations.HasType ? typeWithAnnotations : TypeWithAnnotations.Create(base.CreateErrorType("var"), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
				ExpressionSyntax variable = forEachVariableStatementSyntax.Variable;
				if (variable.IsDeconstructionLeft())
				{
					BoundDeconstructValuePlaceholder boundDeconstructValuePlaceholder = new BoundDeconstructValuePlaceholder(this._syntax.Expression, valEscape, typeWithAnnotations2.Type).MakeCompilerGenerated<BoundDeconstructValuePlaceholder>();
					DeclarationExpressionSyntax declarationExpressionSyntax = null;
					ExpressionSyntax expressionSyntax = null;
					BoundDeconstructionAssignmentOperator boundDeconstructionAssignmentOperator = base.BindDeconstruction(variable, variable, this._syntax.Expression, diagnostics, ref declarationExpressionSyntax, ref expressionSyntax, false, boundDeconstructValuePlaceholder);
					if (expressionSyntax != null)
					{
						Binder.Error(diagnostics, ErrorCode.ERR_MustDeclareForeachIteration, variable);
						flag = true;
					}
					boundForEachDeconstructStep = new BoundForEachDeconstructStep(variable, boundDeconstructionAssignmentOperator, boundDeconstructValuePlaceholder, false).MakeCompilerGenerated<BoundForEachDeconstructStep>();
				}
				else
				{
					boundExpression2 = base.BindExpression(forEachVariableStatementSyntax.Variable, BindingDiagnosticBag.Discarded);
					if (boundExpression2.Kind == BoundKind.DiscardExpression)
					{
						boundExpression2 = ((BoundDiscardExpression)boundExpression2).FailInference(this, null);
					}
					flag = true;
					if (!forEachVariableStatementSyntax.HasErrors)
					{
						Binder.Error(diagnostics, ErrorCode.ERR_MustDeclareForeachIteration, variable);
					}
				}
				boundTypeExpression = new BoundTypeExpression(variable, null, typeWithAnnotations2, false).MakeCompilerGenerated<BoundTypeExpression>();
			}
			else
			{
				ForEachStatementSyntax forEachStatementSyntax = (ForEachStatementSyntax)this._syntax;
				flag2 = originalBinder.ValidateDeclarationNameConflictsInScope(this.IterationVariable, diagnostics);
				RefKind refKind;
				TypeSyntax typeSyntax = forEachStatementSyntax.Type.SkipRef(out refKind);
				bool flag3;
				AliasSymbol aliasSymbol;
				TypeWithAnnotations typeWithAnnotations3 = base.BindTypeOrVarKeyword(typeSyntax, diagnostics, out flag3, out aliasSymbol);
				if (flag3)
				{
					typeWithAnnotations3 = (typeWithAnnotations.HasType ? typeWithAnnotations : TypeWithAnnotations.Create(base.CreateErrorType("var"), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
				}
				typeWithAnnotations2 = typeWithAnnotations3;
				boundTypeExpression = new BoundTypeExpression(typeSyntax, aliasSymbol, typeWithAnnotations2, false);
				SourceLocalSymbol iterationVariable = this.IterationVariable;
				iterationVariable.SetTypeWithAnnotations(typeWithAnnotations3);
				iterationVariable.SetValEscape(valEscape);
				if (iterationVariable.RefKind != RefKind.None)
				{
					iterationVariable.SetRefEscape(valEscape);
					if (base.CheckRefLocalInAsyncOrIteratorMethod(iterationVariable.IdentifierToken, diagnostics))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					refKind = iterationVariable.RefKind;
					Binder.BindValueKind bindValueKind;
					switch (refKind)
					{
					case RefKind.None:
						bindValueKind = Binder.BindValueKind.RValue;
						goto IL_02F7;
					case RefKind.Ref:
						bindValueKind = Binder.BindValueKind.Assignable | Binder.BindValueKind.RefersToLocation;
						goto IL_02F7;
					case RefKind.In:
						bindValueKind = Binder.BindValueKind.RefersToLocation;
						goto IL_02F7;
					}
					throw ExceptionUtilities.UnexpectedValue(iterationVariable.RefKind);
					IL_02F7:
					flag |= !base.CheckMethodReturnValueKind(builder.CurrentPropertyGetter, null, boundExpression.Syntax, bindValueKind, false, diagnostics);
				}
			}
			BoundStatement boundStatement = originalBinder.BindPossibleEmbeddedStatement(this._syntax.Statement, diagnostics);
			ImmutableArray<LocalSymbol> locals = this.Locals;
			flag = flag || boundTypeExpression.HasErrors || typeWithAnnotations2.Type.IsErrorType();
			if (flag)
			{
				return new BoundForEachStatement(this._syntax, null, default(Conversion), boundTypeExpression, locals, boundExpression2, boundExpression, boundForEachDeconstructStep, boundAwaitableInfo, boundStatement, base.CheckOverflowAtRuntime, this.BreakLabel, this.ContinueLabel, flag);
			}
			flag = flag || flag2;
			SyntaxToken forEachKeyword = this._syntax.ForEachKeyword;
			base.ReportDiagnosticsIfObsolete(diagnostics, methodSymbol, forEachKeyword, false);
			Binder.ReportDiagnosticsIfUnmanagedCallersOnly(diagnostics, methodSymbol, forEachKeyword.GetLocation(), false);
			base.ReportDiagnosticsIfObsolete(diagnostics, builder.MoveNextInfo.Method, forEachKeyword, false);
			base.ReportDiagnosticsIfObsolete(diagnostics, builder.CurrentPropertyGetter, forEachKeyword, false);
			base.ReportDiagnosticsIfObsolete(diagnostics, builder.CurrentPropertyGetter.AssociatedSymbol, forEachKeyword, false);
			CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = base.GetNewCompoundUseSiteInfo(diagnostics);
			Conversion conversion = base.Conversions.ClassifyConversionFromType(typeWithAnnotations.Type, typeWithAnnotations2.Type, ref newCompoundUseSiteInfo, true);
			if (!conversion.IsValid)
			{
				ImmutableArray<MethodSymbol> originalUserDefinedConversions = conversion.OriginalUserDefinedConversions;
				if (originalUserDefinedConversions.Length > 1)
				{
					diagnostics.Add(ErrorCode.ERR_AmbigUDConv, forEachKeyword.GetLocation(), new object[]
					{
						originalUserDefinedConversions[0],
						originalUserDefinedConversions[1],
						typeWithAnnotations.Type,
						typeWithAnnotations2
					});
				}
				else
				{
					SymbolDistinguisher symbolDistinguisher = new SymbolDistinguisher(base.Compilation, typeWithAnnotations.Type, typeWithAnnotations2.Type);
					diagnostics.Add(ErrorCode.ERR_NoExplicitConv, forEachKeyword.GetLocation(), new object[] { symbolDistinguisher.First, symbolDistinguisher.Second });
				}
				flag = true;
			}
			else
			{
				base.ReportDiagnosticsIfObsolete(diagnostics, conversion, this._syntax.ForEachKeyword, false);
			}
			builder.CollectionConversion = base.Conversions.ClassifyConversionFromExpression(boundExpression, builder.CollectionType, ref newCompoundUseSiteInfo, false);
			builder.CurrentConversion = base.Conversions.ClassifyConversionFromType(builder.CurrentPropertyGetter.ReturnType, builder.ElementType, ref newCompoundUseSiteInfo, false);
			TypeSymbol returnType = methodSymbol.ReturnType;
			builder.EnumeratorConversion = (returnType.IsValueType ? Conversion.Identity : base.Conversions.ClassifyConversionFromType(returnType, base.GetSpecialType(SpecialType.System_Object, diagnostics, this._syntax), ref newCompoundUseSiteInfo, false));
			if (returnType.IsRestrictedType(false) && (this.IsDirectlyInIterator || base.IsInAsyncMethod()))
			{
				diagnostics.Add(ErrorCode.ERR_BadSpecialByRefIterator, forEachKeyword.GetLocation(), new object[] { returnType });
			}
			diagnostics.Add(this._syntax.ForEachKeyword.GetLocation(), newCompoundUseSiteInfo);
			BoundConversion boundConversion = new BoundConversion(boundExpression.Syntax, boundExpression, builder.CollectionConversion, base.CheckOverflowAtRuntime, false, null, null, builder.CollectionType, false);
			if (builder.NeedsDisposal && this.IsAsync)
			{
				flag |= this.GetAwaitDisposeAsyncInfo(ref builder, diagnostics);
			}
			return new BoundForEachStatement(this._syntax, builder.Build(this.Flags), conversion, boundTypeExpression, locals, boundExpression2, boundConversion, boundForEachDeconstructStep, boundAwaitableInfo, boundStatement, base.CheckOverflowAtRuntime, this.BreakLabel, this.ContinueLabel, flag);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00036F64 File Offset: 0x00035164
		private bool GetAwaitDisposeAsyncInfo(ref ForEachEnumeratorInfo.Builder builder, BindingDiagnosticBag diagnostics)
		{
			TypeSymbol typeSymbol = ((builder.PatternDisposeInfo == null) ? base.GetWellKnownType(WellKnownType.System_Threading_Tasks_ValueTask, diagnostics, this._syntax) : builder.PatternDisposeInfo.Method.ReturnType);
			bool flag = false;
			ExpressionSyntax expression = this._syntax.Expression;
			base.ReportBadAwaitDiagnostics(expression, this._syntax.AwaitKeyword.GetLocation(), diagnostics, ref flag);
			BoundAwaitableValuePlaceholder boundAwaitableValuePlaceholder = new BoundAwaitableValuePlaceholder(expression, this.LocalScopeDepth, typeSymbol);
			builder.DisposeAwaitableInfo = base.BindAwaitInfo(boundAwaitableValuePlaceholder, expression, diagnostics, ref flag, null);
			return flag;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00036FEC File Offset: 0x000351EC
		internal TypeWithAnnotations InferCollectionElementType(BindingDiagnosticBag diagnostics, ExpressionSyntax collectionSyntax)
		{
			BoundExpression boundExpression = this.GetBinder(collectionSyntax).BindValue(collectionSyntax, diagnostics, Binder.BindValueKind.RValue);
			ForEachEnumeratorInfo.Builder builder = default(ForEachEnumeratorInfo.Builder);
			TypeWithAnnotations typeWithAnnotations;
			this.GetEnumeratorInfoAndInferCollectionElementType(ref builder, ref boundExpression, diagnostics, out typeWithAnnotations);
			return typeWithAnnotations;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00037020 File Offset: 0x00035220
		private bool GetEnumeratorInfoAndInferCollectionElementType(ref ForEachEnumeratorInfo.Builder builder, ref BoundExpression collectionExpr, BindingDiagnosticBag diagnostics, out TypeWithAnnotations inferredType)
		{
			bool enumeratorInfo = this.GetEnumeratorInfo(ref builder, ref collectionExpr, diagnostics);
			if (!enumeratorInfo)
			{
				inferredType = default(TypeWithAnnotations);
				return enumeratorInfo;
			}
			if (collectionExpr.HasDynamicType())
			{
				inferredType = TypeWithAnnotations.Create(DynamicTypeSymbol.Instance, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				return enumeratorInfo;
			}
			if (collectionExpr.Type.SpecialType == SpecialType.System_String && builder.CollectionType.SpecialType == SpecialType.System_Collections_IEnumerable)
			{
				inferredType = TypeWithAnnotations.Create(base.GetSpecialType(SpecialType.System_Char, diagnostics, collectionExpr.Syntax), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				return enumeratorInfo;
			}
			inferredType = builder.ElementTypeWithAnnotations;
			return enumeratorInfo;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000370BC File Offset: 0x000352BC
		private BoundExpression UnwrapCollectionExpressionIfNullable(BoundExpression collectionExpr, BindingDiagnosticBag diagnostics)
		{
			TypeSymbol type = collectionExpr.Type;
			if (type == null || !type.IsNullableType())
			{
				return collectionExpr;
			}
			SyntaxNode syntax = collectionExpr.Syntax;
			MethodSymbol methodSymbol = (MethodSymbol)base.GetSpecialTypeMember(SpecialMember.System_Nullable_T_get_Value, diagnostics, syntax);
			if (methodSymbol != null)
			{
				methodSymbol = methodSymbol.AsMember((NamedTypeSymbol)type);
				return BoundCall.Synthesized(syntax, collectionExpr, methodSymbol);
			}
			return new BoundBadExpression(syntax, LookupResultKind.Empty, ImmutableArray<Symbol>.Empty, ImmutableArray.Create<BoundExpression>(collectionExpr), type.GetNullableUnderlyingType())
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0003712C File Offset: 0x0003532C
		private bool GetEnumeratorInfo(ref ForEachEnumeratorInfo.Builder builder, ref BoundExpression collectionExpr, BindingDiagnosticBag diagnostics)
		{
			bool isAsync = this.IsAsync;
			builder.IsAsync = isAsync;
			ForEachLoopBinder.EnumeratorResult enumeratorInfo = this.GetEnumeratorInfo(ref builder, ref collectionExpr, isAsync, diagnostics);
			if (enumeratorInfo == ForEachLoopBinder.EnumeratorResult.Succeeded)
			{
				return true;
			}
			if (enumeratorInfo == ForEachLoopBinder.EnumeratorResult.FailedAndReported)
			{
				return false;
			}
			TypeSymbol type = collectionExpr.Type;
			if (string.IsNullOrEmpty(type.Name) && collectionExpr.HasErrors)
			{
				return false;
			}
			if (type.IsErrorType())
			{
				return false;
			}
			ForEachEnumeratorInfo.Builder builder2 = default(ForEachEnumeratorInfo.Builder);
			ErrorCode errorCode = ((this.GetEnumeratorInfo(ref builder2, ref collectionExpr, !isAsync, BindingDiagnosticBag.Discarded) == ForEachLoopBinder.EnumeratorResult.Succeeded) ? (isAsync ? ErrorCode.ERR_AwaitForEachMissingMemberWrongAsync : ErrorCode.ERR_ForEachMissingMemberWrongAsync) : (isAsync ? ErrorCode.ERR_AwaitForEachMissingMember : ErrorCode.ERR_ForEachMissingMember));
			diagnostics.Add(errorCode, this._syntax.Expression.Location, new object[]
			{
				type,
				isAsync ? "GetAsyncEnumerator" : "GetEnumerator"
			});
			return false;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00037200 File Offset: 0x00035400
		private ForEachLoopBinder.EnumeratorResult GetEnumeratorInfo(ref ForEachEnumeratorInfo.Builder builder, ref BoundExpression collectionExpr, bool isAsync, BindingDiagnosticBag diagnostics)
		{
			TypeSymbol type = collectionExpr.Type;
			if (type == null)
			{
				if (!this.ReportConstantNullCollectionExpr(collectionExpr, diagnostics))
				{
					diagnostics.Add(ErrorCode.ERR_AnonMethGrpInForEach, this._syntax.Expression.Location, new object[] { collectionExpr.Display });
				}
				return ForEachLoopBinder.EnumeratorResult.FailedAndReported;
			}
			if (collectionExpr.ResultKind == LookupResultKind.NotAValue)
			{
				return ForEachLoopBinder.EnumeratorResult.FailedAndReported;
			}
			if (type.Kind == SymbolKind.DynamicType && this.IsAsync)
			{
				diagnostics.Add(ErrorCode.ERR_BadDynamicAwaitForEach, this._syntax.Expression.Location);
				return ForEachLoopBinder.EnumeratorResult.FailedAndReported;
			}
			if (type.Kind == SymbolKind.ArrayType || type.Kind == SymbolKind.DynamicType)
			{
				if (this.ReportConstantNullCollectionExpr(collectionExpr, diagnostics))
				{
					return ForEachLoopBinder.EnumeratorResult.FailedAndReported;
				}
				builder = this.GetDefaultEnumeratorInfo(builder, diagnostics, type);
				return ForEachLoopBinder.EnumeratorResult.Succeeded;
			}
			else
			{
				BoundExpression boundExpression = this.UnwrapCollectionExpressionIfNullable(collectionExpr, diagnostics);
				TypeSymbol type2 = boundExpression.Type;
				if (this.SatisfiesGetEnumeratorPattern(ref builder, boundExpression, isAsync, false, diagnostics))
				{
					collectionExpr = boundExpression;
					if (this.ReportConstantNullCollectionExpr(collectionExpr, diagnostics))
					{
						return ForEachLoopBinder.EnumeratorResult.FailedAndReported;
					}
					return this.<GetEnumeratorInfo>g__createPatternBasedEnumeratorResult|23_0(ref builder, boundExpression, isAsync, false, diagnostics);
				}
				else
				{
					if (!isAsync && ForEachLoopBinder.IsIEnumerable(type2))
					{
						collectionExpr = boundExpression;
						diagnostics.Add(ErrorCode.ERR_ForEachMissingMember, this._syntax.Expression.Location, new object[] { type2, "GetEnumerator" });
						return ForEachLoopBinder.EnumeratorResult.FailedAndReported;
					}
					if (isAsync && this.IsIAsyncEnumerable(type2))
					{
						collectionExpr = boundExpression;
						diagnostics.Add(ErrorCode.ERR_AwaitForEachMissingMember, this._syntax.Expression.Location, new object[] { type2, "GetAsyncEnumerator" });
						return ForEachLoopBinder.EnumeratorResult.FailedAndReported;
					}
					ForEachLoopBinder.EnumeratorResult enumeratorResult = this.SatisfiesIEnumerableInterfaces(ref builder, boundExpression, isAsync, diagnostics, type2);
					if (enumeratorResult != ForEachLoopBinder.EnumeratorResult.FailedNotReported)
					{
						collectionExpr = boundExpression;
						return enumeratorResult;
					}
					if (!isAsync && type.SpecialType == SpecialType.System_String)
					{
						if (this.ReportConstantNullCollectionExpr(collectionExpr, diagnostics))
						{
							return ForEachLoopBinder.EnumeratorResult.FailedAndReported;
						}
						builder = this.GetDefaultEnumeratorInfo(builder, diagnostics, type);
						return ForEachLoopBinder.EnumeratorResult.Succeeded;
					}
					else
					{
						if (this.SatisfiesGetEnumeratorPattern(ref builder, collectionExpr, isAsync, true, diagnostics))
						{
							return this.<GetEnumeratorInfo>g__createPatternBasedEnumeratorResult|23_0(ref builder, collectionExpr, isAsync, true, diagnostics);
						}
						return ForEachLoopBinder.EnumeratorResult.FailedNotReported;
					}
				}
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000373EC File Offset: 0x000355EC
		private ForEachLoopBinder.EnumeratorResult SatisfiesIEnumerableInterfaces(ref ForEachEnumeratorInfo.Builder builder, BoundExpression collectionExpr, bool isAsync, BindingDiagnosticBag diagnostics, TypeSymbol unwrappedCollectionExprType)
		{
			bool flag;
			if (!this.AllInterfacesContainsIEnumerable(ref builder, unwrappedCollectionExprType, isAsync, diagnostics, out flag))
			{
				return ForEachLoopBinder.EnumeratorResult.FailedNotReported;
			}
			if (this.ReportConstantNullCollectionExpr(collectionExpr, diagnostics))
			{
				return ForEachLoopBinder.EnumeratorResult.FailedAndReported;
			}
			CSharpSyntaxNode expression = this._syntax.Expression;
			if (flag)
			{
				diagnostics.Add(isAsync ? ErrorCode.ERR_MultipleIAsyncEnumOfT : ErrorCode.ERR_MultipleIEnumOfT, expression.Location, new object[]
				{
					unwrappedCollectionExprType,
					isAsync ? base.Compilation.GetWellKnownType(WellKnownType.System_Collections_Generic_IAsyncEnumerable_T) : base.Compilation.GetSpecialType(SpecialType.System_Collections_Generic_IEnumerable_T)
				});
				return ForEachLoopBinder.EnumeratorResult.FailedAndReported;
			}
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)builder.CollectionType;
			if (namedTypeSymbol.IsGenericType)
			{
				builder.ElementTypeWithAnnotations = namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics.Single<TypeWithAnnotations>();
				MethodSymbol methodSymbol;
				if (isAsync)
				{
					methodSymbol = (MethodSymbol)Binder.GetWellKnownTypeMember(base.Compilation, WellKnownMember.System_Collections_Generic_IAsyncEnumerable_T__GetAsyncEnumerator, diagnostics, expression.Location, null, false);
					if (methodSymbol != null && !methodSymbol.Parameters[0].IsOptional)
					{
						diagnostics.Add(ErrorCode.ERR_AwaitForEachMissingMember, this._syntax.Expression.Location, new object[] { unwrappedCollectionExprType, "GetAsyncEnumerator" });
						return ForEachLoopBinder.EnumeratorResult.FailedAndReported;
					}
				}
				else
				{
					methodSymbol = (MethodSymbol)base.GetSpecialTypeMember(SpecialMember.System_Collections_Generic_IEnumerable_T__GetEnumerator, diagnostics, expression);
				}
				MethodSymbol methodSymbol2 = null;
				if (methodSymbol != null)
				{
					MethodSymbol methodSymbol3 = methodSymbol.AsMember(namedTypeSymbol);
					TypeSymbol returnType = methodSymbol3.ReturnType;
					builder.GetEnumeratorInfo = this.BindDefaultArguments(methodSymbol3, null, false, collectionExpr.Syntax, diagnostics, false);
					MethodSymbol methodSymbol5;
					if (isAsync)
					{
						MethodSymbol methodSymbol4 = (MethodSymbol)base.GetWellKnownTypeMember(WellKnownMember.System_Collections_Generic_IAsyncEnumerator_T__MoveNextAsync, diagnostics, expression.Location, null, false);
						if (methodSymbol4 != null)
						{
							methodSymbol2 = methodSymbol4.AsMember((NamedTypeSymbol)returnType);
						}
						methodSymbol5 = (MethodSymbol)Binder.GetWellKnownTypeMember(base.Compilation, WellKnownMember.System_Collections_Generic_IAsyncEnumerator_T__get_Current, diagnostics, expression.Location, null, false);
					}
					else
					{
						methodSymbol5 = (MethodSymbol)base.GetSpecialTypeMember(SpecialMember.System_Collections_Generic_IEnumerator_T__get_Current, diagnostics, expression);
					}
					if (methodSymbol5 != null)
					{
						builder.CurrentPropertyGetter = methodSymbol5.AsMember((NamedTypeSymbol)returnType);
					}
				}
				if (!isAsync)
				{
					methodSymbol2 = (MethodSymbol)base.GetSpecialTypeMember(SpecialMember.System_Collections_IEnumerator__MoveNext, diagnostics, expression);
				}
				if (methodSymbol2 != null)
				{
					builder.MoveNextInfo = MethodArgumentInfo.CreateParameterlessMethod(methodSymbol2);
				}
			}
			else
			{
				builder.GetEnumeratorInfo = this.GetParameterlessSpecialTypeMemberInfo(SpecialMember.System_Collections_IEnumerable__GetEnumerator, expression, diagnostics);
				builder.CurrentPropertyGetter = (MethodSymbol)base.GetSpecialTypeMember(SpecialMember.System_Collections_IEnumerator__get_Current, diagnostics, expression);
				builder.MoveNextInfo = this.GetParameterlessSpecialTypeMemberInfo(SpecialMember.System_Collections_IEnumerator__MoveNext, expression, diagnostics);
				MethodSymbol currentPropertyGetter = builder.CurrentPropertyGetter;
				builder.ElementTypeWithAnnotations = ((currentPropertyGetter != null) ? currentPropertyGetter.ReturnTypeWithAnnotations : TypeWithAnnotations.Create(base.GetSpecialType(SpecialType.System_Object, diagnostics, expression), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
			}
			builder.NeedsDisposal = true;
			return ForEachLoopBinder.EnumeratorResult.Succeeded;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0003766C File Offset: 0x0003586C
		private bool ReportConstantNullCollectionExpr(BoundExpression collectionExpr, BindingDiagnosticBag diagnostics)
		{
			ConstantValue constantValue = collectionExpr.ConstantValue;
			if (constantValue != null && constantValue.IsNull)
			{
				diagnostics.Add(ErrorCode.ERR_NullNotValid, this._syntax.Expression.Location);
				return true;
			}
			return false;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000376AC File Offset: 0x000358AC
		private void GetDisposalInfoForEnumerator(ref ForEachEnumeratorInfo.Builder builder, BoundExpression expr, bool isAsync, BindingDiagnosticBag diagnostics)
		{
			TypeSymbol returnType = builder.GetEnumeratorInfo.Method.ReturnType;
			CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = base.GetNewCompoundUseSiteInfo(diagnostics);
			if ((!returnType.IsSealed && !isAsync) || base.Conversions.ClassifyImplicitConversionFromType(returnType, isAsync ? base.Compilation.GetWellKnownType(WellKnownType.System_IAsyncDisposable) : base.Compilation.GetSpecialType(SpecialType.System_IDisposable), ref newCompoundUseSiteInfo).IsImplicit)
			{
				builder.NeedsDisposal = true;
			}
			else if (base.Compilation.IsFeatureEnabled(MessageID.IDS_FeatureUsingDeclarations) && (returnType.IsRefLikeType || isAsync))
			{
				BoundDisposableValuePlaceholder boundDisposableValuePlaceholder = new BoundDisposableValuePlaceholder(this._syntax, returnType);
				MethodSymbol methodSymbol = base.TryFindDisposePatternMethod(boundDisposableValuePlaceholder, this._syntax, isAsync, BindingDiagnosticBag.Discarded);
				if (methodSymbol != null)
				{
					ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(methodSymbol.ParameterCount);
					ImmutableArray<int> immutableArray = default(ImmutableArray<int>);
					bool flag = methodSymbol.HasParamsParameter();
					BitVector bitVector;
					base.BindDefaultArguments(this._syntax, methodSymbol.Parameters, instance, null, ref immutableArray, out bitVector, flag, true, diagnostics, true);
					builder.NeedsDisposal = true;
					builder.PatternDisposeInfo = new MethodArgumentInfo(methodSymbol, instance.ToImmutableAndFree(), immutableArray, bitVector, flag);
				}
			}
			diagnostics.Add(this._syntax, newCompoundUseSiteInfo);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000377DC File Offset: 0x000359DC
		private ForEachEnumeratorInfo.Builder GetDefaultEnumeratorInfo(ForEachEnumeratorInfo.Builder builder, BindingDiagnosticBag diagnostics, TypeSymbol collectionExprType)
		{
			builder.CollectionType = base.GetSpecialType(SpecialType.System_Collections_IEnumerable, diagnostics, this._syntax);
			if (collectionExprType.IsDynamic())
			{
				ForEachStatementSyntax forEachStatementSyntax = this._syntax as ForEachStatementSyntax;
				builder.ElementTypeWithAnnotations = TypeWithAnnotations.Create((forEachStatementSyntax != null && forEachStatementSyntax.Type.IsVar) ? DynamicTypeSymbol.Instance : base.GetSpecialType(SpecialType.System_Object, diagnostics, this._syntax), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}
			else
			{
				builder.ElementTypeWithAnnotations = ((collectionExprType.SpecialType == SpecialType.System_String) ? TypeWithAnnotations.Create(base.GetSpecialType(SpecialType.System_Char, diagnostics, this._syntax), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)) : ((ArrayTypeSymbol)collectionExprType).ElementTypeWithAnnotations);
			}
			builder.GetEnumeratorInfo = this.GetParameterlessSpecialTypeMemberInfo(SpecialMember.System_Collections_IEnumerable__GetEnumerator, this._syntax, diagnostics);
			builder.CurrentPropertyGetter = (MethodSymbol)base.GetSpecialTypeMember(SpecialMember.System_Collections_IEnumerator__get_Current, diagnostics, this._syntax);
			builder.MoveNextInfo = this.GetParameterlessSpecialTypeMemberInfo(SpecialMember.System_Collections_IEnumerator__MoveNext, this._syntax, diagnostics);
			builder.NeedsDisposal = true;
			return builder;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000378E0 File Offset: 0x00035AE0
		private bool SatisfiesGetEnumeratorPattern(ref ForEachEnumeratorInfo.Builder builder, BoundExpression collectionExpr, bool isAsync, bool viaExtensionMethod, BindingDiagnosticBag diagnostics)
		{
			string text = (isAsync ? "GetAsyncEnumerator" : "GetEnumerator");
			MethodArgumentInfo methodArgumentInfo;
			if (viaExtensionMethod)
			{
				methodArgumentInfo = this.FindForEachPatternMethodViaExtension(collectionExpr, text, diagnostics);
			}
			else
			{
				LookupResult instance = LookupResult.GetInstance();
				methodArgumentInfo = this.FindForEachPatternMethod(collectionExpr.Type, text, instance, true, diagnostics, isAsync);
				instance.Free();
			}
			builder.GetEnumeratorInfo = methodArgumentInfo;
			return methodArgumentInfo != null;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00037938 File Offset: 0x00035B38
		private MethodArgumentInfo FindForEachPatternMethod(TypeSymbol patternType, string methodName, LookupResult lookupResult, bool warningsOnly, BindingDiagnosticBag diagnostics, bool isAsync)
		{
			CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = base.GetNewCompoundUseSiteInfo(diagnostics);
			base.LookupMembersInType(lookupResult, patternType, methodName, 0, null, LookupOptions.Default, this, false, ref newCompoundUseSiteInfo);
			diagnostics.Add(this._syntax.Expression, newCompoundUseSiteInfo);
			if (!lookupResult.IsMultiViable)
			{
				this.ReportPatternMemberLookupDiagnostics(lookupResult, patternType, methodName, warningsOnly, diagnostics);
				return null;
			}
			ArrayBuilder<MethodSymbol> instance = ArrayBuilder<MethodSymbol>.GetInstance();
			foreach (Symbol symbol in lookupResult.Symbols)
			{
				if (symbol.Kind != SymbolKind.Method)
				{
					instance.Free();
					if (warningsOnly)
					{
						this.ReportEnumerableWarning(diagnostics, patternType, symbol);
					}
					return null;
				}
				if (((MethodSymbol)symbol).ParameterCount == 0 || isAsync)
				{
					instance.Add((MethodSymbol)symbol);
				}
			}
			MethodArgumentInfo methodArgumentInfo = this.PerformForEachPatternOverloadResolution(patternType, instance, warningsOnly, diagnostics, isAsync);
			instance.Free();
			return methodArgumentInfo;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00037A04 File Offset: 0x00035C04
		private MethodArgumentInfo PerformForEachPatternOverloadResolution(TypeSymbol patternType, ArrayBuilder<MethodSymbol> candidateMethods, bool warningsOnly, BindingDiagnosticBag diagnostics, bool isAsync)
		{
			AnalyzedArguments instance = AnalyzedArguments.GetInstance();
			ArrayBuilder<TypeWithAnnotations> instance2 = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			OverloadResolutionResult<MethodSymbol> instance3 = OverloadResolutionResult<MethodSymbol>.GetInstance();
			CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = base.GetNewCompoundUseSiteInfo(diagnostics);
			BoundImplicitReceiver boundImplicitReceiver = new BoundImplicitReceiver(this._syntax.Expression, patternType);
			OverloadResolution overloadResolution = base.OverloadResolution;
			ArrayBuilder<TypeWithAnnotations> arrayBuilder = instance2;
			BoundExpression boundExpression = boundImplicitReceiver;
			AnalyzedArguments analyzedArguments = instance;
			OverloadResolutionResult<MethodSymbol> overloadResolutionResult = instance3;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = true;
			RefKind refKind = RefKind.None;
			TypeSymbol typeSymbol = null;
			bool flag5 = false;
			CallingConventionInfo callingConventionInfo = default(CallingConventionInfo);
			overloadResolution.MethodInvocationOverloadResolution(candidateMethods, arrayBuilder, boundExpression, analyzedArguments, overloadResolutionResult, ref newCompoundUseSiteInfo, flag, flag2, flag3, flag4, refKind, typeSymbol, flag5, in callingConventionInfo);
			diagnostics.Add(this._syntax.Expression, newCompoundUseSiteInfo);
			MethodArgumentInfo methodArgumentInfo = null;
			if (instance3.Succeeded)
			{
				MethodSymbol member = instance3.ValidResult.Member;
				if (member.IsStatic || member.DeclaredAccessibility != Accessibility.Public)
				{
					if (warningsOnly)
					{
						MessageID messageID = (isAsync ? MessageID.IDS_FeatureAsyncStreams : MessageID.IDS_Collection);
						diagnostics.Add(ErrorCode.WRN_PatternNotPublicOrNotInstance, this._syntax.Expression.Location, new object[]
						{
							patternType,
							messageID.Localize(),
							member
						});
					}
				}
				else if (!member.CallsAreOmitted(this._syntax.SyntaxTree))
				{
					ImmutableArray<int> argsToParamsOpt = instance3.ValidResult.Result.ArgsToParamsOpt;
					bool flag6 = instance3.ValidResult.Result.Kind == MemberResolutionKind.ApplicableInExpandedForm;
					BitVector bitVector;
					base.BindDefaultArguments(this._syntax, member.Parameters, instance.Arguments, instance.RefKinds, ref argsToParamsOpt, out bitVector, flag6, true, diagnostics, true);
					methodArgumentInfo = new MethodArgumentInfo(member, instance.Arguments.ToImmutable(), argsToParamsOpt, bitVector, flag6);
				}
			}
			else
			{
				ImmutableArray<MethodSymbol> allApplicableMembers = instance3.GetAllApplicableMembers();
				if (allApplicableMembers.Length > 1 && warningsOnly)
				{
					diagnostics.Add(ErrorCode.WRN_PatternIsAmbiguous, this._syntax.Expression.Location, new object[]
					{
						patternType,
						MessageID.IDS_Collection.Localize(),
						allApplicableMembers[0],
						allApplicableMembers[1]
					});
				}
			}
			instance3.Free();
			instance.Free();
			instance2.Free();
			return methodArgumentInfo;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00037C0C File Offset: 0x00035E0C
		private MethodArgumentInfo FindForEachPatternMethodViaExtension(BoundExpression collectionExpr, string methodName, BindingDiagnosticBag diagnostics)
		{
			AnalyzedArguments instance = AnalyzedArguments.GetInstance();
			MethodGroupResolution methodGroupResolution = base.BindExtensionMethod(this._syntax.Expression, methodName, instance, collectionExpr, default(ImmutableArray<TypeWithAnnotations>), false, RefKind.None, null, diagnostics.AccumulatesDependencies);
			diagnostics.AddRange(methodGroupResolution.Diagnostics, false);
			OverloadResolutionResult<MethodSymbol> overloadResolutionResult = methodGroupResolution.OverloadResolutionResult;
			if (overloadResolutionResult == null || !overloadResolutionResult.Succeeded)
			{
				ImmutableArray<MethodSymbol>? immutableArray = ((overloadResolutionResult != null) ? new ImmutableArray<MethodSymbol>?(overloadResolutionResult.GetAllApplicableMembers()) : default(ImmutableArray<MethodSymbol>?));
				if (immutableArray != null)
				{
					ImmutableArray<MethodSymbol> valueOrDefault = immutableArray.GetValueOrDefault();
					if (valueOrDefault.Length > 1)
					{
						diagnostics.Add(ErrorCode.WRN_PatternIsAmbiguous, this._syntax.Expression.Location, new object[]
						{
							collectionExpr.Type,
							MessageID.IDS_Collection.Localize(),
							valueOrDefault[0],
							valueOrDefault[1]
						});
						goto IL_0210;
					}
				}
				if (overloadResolutionResult != null)
				{
					overloadResolutionResult.ReportDiagnostics<MethodSymbol>(this, this._syntax.Expression.Location, this._syntax.Expression, diagnostics, methodName, null, this._syntax.Expression, methodGroupResolution.AnalyzedArguments, methodGroupResolution.MethodGroup.Methods.ToImmutable(), null, null, null, false, default(RefKind?), null);
				}
				IL_0210:
				methodGroupResolution.Free();
				instance.Free();
				return null;
			}
			MethodSymbol member = overloadResolutionResult.ValidResult.Member;
			if (member.CallsAreOmitted(this._syntax.SyntaxTree))
			{
				methodGroupResolution.Free();
				instance.Free();
				return null;
			}
			CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = base.GetNewCompoundUseSiteInfo(diagnostics);
			Conversion conversion = base.Conversions.ClassifyConversionFromExpression(collectionExpr, member.Parameters[0].Type, ref newCompoundUseSiteInfo, false);
			diagnostics.Add(this._syntax, newCompoundUseSiteInfo);
			collectionExpr = new BoundConversion(collectionExpr.Syntax, collectionExpr, conversion, base.CheckOverflowAtRuntime, false, null, null, member.Parameters[0].Type, false);
			MethodArgumentInfo methodArgumentInfo = this.BindDefaultArguments(member, collectionExpr, overloadResolutionResult.ValidResult.Result.Kind == MemberResolutionKind.ApplicableInExpandedForm, collectionExpr.Syntax, diagnostics, true);
			methodGroupResolution.Free();
			instance.Free();
			return methodArgumentInfo;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00037E38 File Offset: 0x00036038
		private bool SatisfiesForEachPattern(ref ForEachEnumeratorInfo.Builder builder, bool isAsync, BindingDiagnosticBag diagnostics)
		{
			TypeSymbol returnType = builder.GetEnumeratorInfo.Method.ReturnType;
			switch (returnType.TypeKind)
			{
			case TypeKind.Class:
			case TypeKind.Dynamic:
			case TypeKind.Interface:
			case TypeKind.Struct:
			case TypeKind.TypeParameter:
			{
				LookupResult instance = LookupResult.GetInstance();
				bool flag;
				try
				{
					CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = base.GetNewCompoundUseSiteInfo(diagnostics);
					base.LookupMembersInType(instance, returnType, "Current", 0, null, LookupOptions.Default, this, false, ref newCompoundUseSiteInfo);
					diagnostics.Add(this._syntax.Expression, newCompoundUseSiteInfo);
					newCompoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(newCompoundUseSiteInfo);
					if (!instance.IsSingleViable)
					{
						this.ReportPatternMemberLookupDiagnostics(instance, returnType, "Current", false, diagnostics);
						flag = false;
					}
					else
					{
						Symbol singleSymbolOrDefault = instance.SingleSymbolOrDefault;
						if (singleSymbolOrDefault.IsStatic || singleSymbolOrDefault.DeclaredAccessibility != Accessibility.Public || singleSymbolOrDefault.Kind != SymbolKind.Property)
						{
							flag = false;
						}
						else
						{
							MethodSymbol ownOrInheritedGetMethod = ((PropertySymbol)singleSymbolOrDefault).GetOwnOrInheritedGetMethod();
							if (ownOrInheritedGetMethod == null)
							{
								flag = false;
							}
							else
							{
								bool flag2 = base.IsAccessible(ownOrInheritedGetMethod, ref newCompoundUseSiteInfo, null, null);
								diagnostics.Add(this._syntax.Expression, newCompoundUseSiteInfo);
								if (!flag2)
								{
									flag = false;
								}
								else
								{
									builder.CurrentPropertyGetter = ownOrInheritedGetMethod;
									instance.Clear();
									MethodArgumentInfo methodArgumentInfo = this.FindForEachPatternMethod(returnType, isAsync ? "MoveNextAsync" : "MoveNext", instance, false, diagnostics, isAsync);
									if (methodArgumentInfo == null || methodArgumentInfo.Method.IsStatic || methodArgumentInfo.Method.DeclaredAccessibility != Accessibility.Public || this.IsInvalidMoveNextMethod(methodArgumentInfo.Method, isAsync))
									{
										flag = false;
									}
									else
									{
										builder.MoveNextInfo = methodArgumentInfo;
										flag = true;
									}
								}
							}
						}
					}
				}
				finally
				{
					instance.Free();
				}
				return flag;
			}
			case TypeKind.Submission:
				throw ExceptionUtilities.UnexpectedValue(returnType.TypeKind);
			}
			return false;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00038004 File Offset: 0x00036204
		private bool IsInvalidMoveNextMethod(MethodSymbol moveNextMethodCandidate, bool isAsync)
		{
			return !isAsync && moveNextMethodCandidate.OriginalDefinition.ReturnType.SpecialType != SpecialType.System_Boolean;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00038024 File Offset: 0x00036224
		private void ReportEnumerableWarning(BindingDiagnosticBag diagnostics, TypeSymbol enumeratorType, Symbol patternMemberCandidate)
		{
			CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = base.GetNewCompoundUseSiteInfo(diagnostics);
			if (base.IsAccessible(patternMemberCandidate, ref newCompoundUseSiteInfo, null, null))
			{
				diagnostics.Add(ErrorCode.WRN_PatternBadSignature, this._syntax.Expression.Location, new object[]
				{
					enumeratorType,
					MessageID.IDS_Collection.Localize(),
					patternMemberCandidate
				});
			}
			diagnostics.Add(this._syntax.Expression, newCompoundUseSiteInfo);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00038098 File Offset: 0x00036298
		private static bool IsIEnumerable(TypeSymbol type)
		{
			SpecialType specialType = type.OriginalDefinition.SpecialType;
			return specialType - SpecialType.System_Collections_IEnumerable <= 1;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000380BB File Offset: 0x000362BB
		private bool IsIAsyncEnumerable(TypeSymbol type)
		{
			return type.OriginalDefinition.Equals(base.Compilation.GetWellKnownType(WellKnownType.System_Collections_Generic_IAsyncEnumerable_T));
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000380D8 File Offset: 0x000362D8
		private bool AllInterfacesContainsIEnumerable(ref ForEachEnumeratorInfo.Builder builder, TypeSymbol type, bool isAsync, BindingDiagnosticBag diagnostics, out bool foundMultiple)
		{
			CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = base.GetNewCompoundUseSiteInfo(diagnostics);
			NamedTypeSymbol namedTypeSymbol = ForEachLoopBinder.GetIEnumerableOfT(type, isAsync, base.Compilation, ref newCompoundUseSiteInfo, out foundMultiple);
			if (namedTypeSymbol == null || !base.IsAccessible(namedTypeSymbol, ref newCompoundUseSiteInfo, null, null))
			{
				namedTypeSymbol = null;
				if (!isAsync)
				{
					NamedTypeSymbol specialType = base.Compilation.GetSpecialType(SpecialType.System_Collections_IEnumerable);
					if (specialType != null && base.Conversions.ClassifyImplicitConversionFromType(type, specialType, ref newCompoundUseSiteInfo).IsImplicit)
					{
						namedTypeSymbol = specialType;
					}
				}
			}
			diagnostics.Add(this._syntax.Expression, newCompoundUseSiteInfo);
			builder.CollectionType = namedTypeSymbol;
			return namedTypeSymbol != null;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00038160 File Offset: 0x00036360
		internal static NamedTypeSymbol GetIEnumerableOfT(TypeSymbol type, bool isAsync, CSharpCompilation compilation, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, out bool foundMultiple)
		{
			NamedTypeSymbol namedTypeSymbol = null;
			foundMultiple = false;
			if (type.TypeKind == TypeKind.TypeParameter)
			{
				TypeParameterSymbol typeParameterSymbol = (TypeParameterSymbol)type;
				ForEachLoopBinder.GetIEnumerableOfT(typeParameterSymbol.EffectiveBaseClass(ref useSiteInfo).AllInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo).Concat<NamedTypeSymbol>(typeParameterSymbol.AllEffectiveInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo)), isAsync, compilation, ref namedTypeSymbol, ref foundMultiple);
			}
			else
			{
				ForEachLoopBinder.GetIEnumerableOfT(type.AllInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo), isAsync, compilation, ref namedTypeSymbol, ref foundMultiple);
			}
			return namedTypeSymbol;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000381C0 File Offset: 0x000363C0
		private static void GetIEnumerableOfT(ImmutableArray<NamedTypeSymbol> interfaces, bool isAsync, CSharpCompilation compilation, ref NamedTypeSymbol result, ref bool foundMultiple)
		{
			if (foundMultiple)
			{
				return;
			}
			interfaces = MethodTypeInferrer.ModuloReferenceTypeNullabilityDifferences(interfaces, VarianceKind.In);
			foreach (NamedTypeSymbol namedTypeSymbol in interfaces)
			{
				if (ForEachLoopBinder.IsIEnumerableT(namedTypeSymbol.OriginalDefinition, isAsync, compilation))
				{
					if (result != null && !TypeSymbol.Equals(namedTypeSymbol, result, TypeCompareKind.IgnoreTupleNames))
					{
						foundMultiple = true;
						return;
					}
					result = namedTypeSymbol;
				}
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0003821F File Offset: 0x0003641F
		internal static bool IsIEnumerableT(TypeSymbol type, bool isAsync, CSharpCompilation compilation)
		{
			if (isAsync)
			{
				return type.Equals(compilation.GetWellKnownType(WellKnownType.System_Collections_Generic_IAsyncEnumerable_T));
			}
			return type.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00038240 File Offset: 0x00036440
		private void ReportPatternMemberLookupDiagnostics(LookupResult lookupResult, TypeSymbol patternType, string memberName, bool warningsOnly, BindingDiagnosticBag diagnostics)
		{
			if (lookupResult.Symbols.Any())
			{
				if (warningsOnly)
				{
					this.ReportEnumerableWarning(diagnostics, patternType, lookupResult.Symbols.First());
					return;
				}
				lookupResult.Clear();
				CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = base.GetNewCompoundUseSiteInfo(diagnostics);
				base.LookupMembersInType(lookupResult, patternType, memberName, 0, null, LookupOptions.Default, this, true, ref newCompoundUseSiteInfo);
				diagnostics.Add(this._syntax.Expression, newCompoundUseSiteInfo);
				if (lookupResult.Error != null)
				{
					diagnostics.Add(lookupResult.Error, this._syntax.Expression.Location);
					return;
				}
			}
			else if (!warningsOnly)
			{
				diagnostics.Add(ErrorCode.ERR_NoSuchMember, this._syntax.Expression.Location, new object[] { patternType, memberName });
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000382F8 File Offset: 0x000364F8
		internal override ImmutableArray<LocalSymbol> GetDeclaredLocalsForScope(SyntaxNode scopeDesignator)
		{
			if (this._syntax == scopeDesignator)
			{
				return this.Locals;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0003830F File Offset: 0x0003650F
		internal override ImmutableArray<LocalFunctionSymbol> GetDeclaredLocalFunctionsForScope(CSharpSyntaxNode scopeDesignator)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00038316 File Offset: 0x00036516
		internal override SyntaxNode ScopeDesignator
		{
			get
			{
				return this._syntax;
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00038320 File Offset: 0x00036520
		private MethodArgumentInfo GetParameterlessSpecialTypeMemberInfo(SpecialMember member, SyntaxNode syntax, BindingDiagnosticBag diagnostics)
		{
			MethodSymbol methodSymbol = (MethodSymbol)base.GetSpecialTypeMember(member, diagnostics, syntax);
			if (methodSymbol == null)
			{
				return null;
			}
			return MethodArgumentInfo.CreateParameterlessMethod(methodSymbol);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00038348 File Offset: 0x00036548
		private MethodArgumentInfo BindDefaultArguments(MethodSymbol method, BoundExpression extensionReceiverOpt, bool expanded, SyntaxNode syntax, BindingDiagnosticBag diagnostics, bool assertMissingParametersAreOptional = true)
		{
			if (method.ParameterCount == 0)
			{
				return MethodArgumentInfo.CreateParameterlessMethod(method);
			}
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(method.ParameterCount);
			if (method.IsExtensionMethod)
			{
				instance.Add(extensionReceiverOpt);
			}
			ImmutableArray<int> immutableArray = default(ImmutableArray<int>);
			BitVector bitVector;
			base.BindDefaultArguments(syntax, method.Parameters, instance, null, ref immutableArray, out bitVector, expanded, true, diagnostics, assertMissingParametersAreOptional);
			return new MethodArgumentInfo(method, instance.ToImmutableAndFree(), immutableArray, bitVector, expanded);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000383B0 File Offset: 0x000365B0
		[CompilerGenerated]
		private ForEachLoopBinder.EnumeratorResult <GetEnumeratorInfo>g__createPatternBasedEnumeratorResult|23_0(ref ForEachEnumeratorInfo.Builder builder, BoundExpression collectionExpr, bool isAsync, bool viaExtensionMethod, BindingDiagnosticBag diagnostics)
		{
			builder.CollectionType = (viaExtensionMethod ? builder.GetEnumeratorInfo.Method.Parameters[0].Type : collectionExpr.Type);
			if (this.SatisfiesForEachPattern(ref builder, isAsync, diagnostics))
			{
				builder.ElementTypeWithAnnotations = ((PropertySymbol)builder.CurrentPropertyGetter.AssociatedSymbol).TypeWithAnnotations;
				this.GetDisposalInfoForEnumerator(ref builder, collectionExpr, isAsync, diagnostics);
				return ForEachLoopBinder.EnumeratorResult.Succeeded;
			}
			MethodSymbol method = builder.GetEnumeratorInfo.Method;
			diagnostics.Add(isAsync ? ErrorCode.ERR_BadGetAsyncEnumerator : ErrorCode.ERR_BadGetEnumerator, this._syntax.Expression.Location, new object[] { method.ReturnType, method });
			return ForEachLoopBinder.EnumeratorResult.FailedAndReported;
		}

		// Token: 0x04000072 RID: 114
		private const string GetEnumeratorMethodName = "GetEnumerator";

		// Token: 0x04000073 RID: 115
		private const string CurrentPropertyName = "Current";

		// Token: 0x04000074 RID: 116
		private const string MoveNextMethodName = "MoveNext";

		// Token: 0x04000075 RID: 117
		private const string GetAsyncEnumeratorMethodName = "GetAsyncEnumerator";

		// Token: 0x04000076 RID: 118
		private const string MoveNextAsyncMethodName = "MoveNextAsync";

		// Token: 0x04000077 RID: 119
		private readonly CommonForEachStatementSyntax _syntax;

		// Token: 0x0200063E RID: 1598
		private enum EnumeratorResult
		{
			// Token: 0x04001F7E RID: 8062
			Succeeded,
			// Token: 0x04001F7F RID: 8063
			FailedNotReported,
			// Token: 0x04001F80 RID: 8064
			FailedAndReported
		}
	}
}
