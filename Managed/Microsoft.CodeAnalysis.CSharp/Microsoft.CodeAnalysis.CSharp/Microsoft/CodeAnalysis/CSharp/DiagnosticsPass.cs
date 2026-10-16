using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001F7 RID: 503
	internal sealed class DiagnosticsPass : BoundTreeWalkerWithStackGuard
	{
		// Token: 0x0600224A RID: 8778 RVA: 0x000B4AA8 File Offset: 0x000B2CA8
		public static void IssueDiagnostics(CSharpCompilation compilation, BoundNode node, BindingDiagnosticBag diagnostics, MethodSymbol containingSymbol)
		{
			ExecutableCodeBinder.ValidateIteratorMethod(compilation, containingSymbol, diagnostics);
			try
			{
				new DiagnosticsPass(compilation, diagnostics, containingSymbol).Visit(node);
			}
			catch (BoundTreeVisitor.CancelledByStackGuardException ex)
			{
				ex.AddAnError(diagnostics);
			}
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x000B4AE8 File Offset: 0x000B2CE8
		private DiagnosticsPass(CSharpCompilation compilation, BindingDiagnosticBag diagnostics, MethodSymbol containingSymbol)
		{
			this._compilation = compilation;
			this._diagnostics = diagnostics;
			this._containingSymbol = containingSymbol;
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x000B4B05 File Offset: 0x000B2D05
		private void Error(ErrorCode code, BoundNode node, params object[] args)
		{
			this._diagnostics.Add(code, node.Syntax.Location, args);
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x000B4B20 File Offset: 0x000B2D20
		private void CheckUnsafeType(BoundExpression e)
		{
			if (e != null && e.Type != null && e.Type.IsPointerOrFunctionPointer())
			{
				this.NoteUnsafe(e);
			}
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x000B4B41 File Offset: 0x000B2D41
		private void NoteUnsafe(BoundNode node)
		{
			if (this._inExpressionLambda && !this._reportedUnsafe)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsPointerOp, node, Array.Empty<object>());
				this._reportedUnsafe = true;
			}
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x000B4B6C File Offset: 0x000B2D6C
		public override BoundNode VisitArrayCreation(BoundArrayCreation node)
		{
			ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)node.Type;
			if (this._inExpressionLambda && node.InitializerOpt != null && !arrayTypeSymbol.IsSZArray)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsMultiDimensionalArrayInitializer, node, Array.Empty<object>());
			}
			return base.VisitArrayCreation(node);
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x000B4BB8 File Offset: 0x000B2DB8
		public override BoundNode VisitArrayAccess(BoundArrayAccess node)
		{
			if (this._inExpressionLambda && node.Indices.Length == 1 && node.Indices[0].Type.SpecialType == SpecialType.None)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsPatternIndexOrRangeIndexer, node, Array.Empty<object>());
			}
			return base.VisitArrayAccess(node);
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000B4C11 File Offset: 0x000B2E11
		public override BoundNode VisitIndexOrRangePatternIndexerAccess(BoundIndexOrRangePatternIndexerAccess node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsPatternIndexOrRangeIndexer, node, Array.Empty<object>());
			}
			return base.VisitIndexOrRangePatternIndexerAccess(node);
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000B4C33 File Offset: 0x000B2E33
		public override BoundNode VisitFromEndIndexExpression(BoundFromEndIndexExpression node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsFromEndIndexExpression, node, Array.Empty<object>());
			}
			return base.VisitFromEndIndexExpression(node);
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x000B4C55 File Offset: 0x000B2E55
		public override BoundNode VisitRangeExpression(BoundRangeExpression node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsRangeExpression, node, Array.Empty<object>());
			}
			return base.VisitRangeExpression(node);
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000B4C77 File Offset: 0x000B2E77
		public override BoundNode VisitSizeOfOperator(BoundSizeOfOperator node)
		{
			if (this._inExpressionLambda && node.ConstantValue == null)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsPointerOp, node, Array.Empty<object>());
			}
			return base.VisitSizeOfOperator(node);
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000B4CA8 File Offset: 0x000B2EA8
		public override BoundNode VisitLocalFunctionStatement(BoundLocalFunctionStatement node)
		{
			ExecutableCodeBinder.ValidateIteratorMethod(this._compilation, node.Symbol, this._diagnostics);
			SourceMethodSymbol staticLocalOrAnonymousFunction = this._staticLocalOrAnonymousFunction;
			if (node.Symbol.IsStatic)
			{
				this._staticLocalOrAnonymousFunction = node.Symbol;
			}
			BoundNode boundNode = base.VisitLocalFunctionStatement(node);
			this._staticLocalOrAnonymousFunction = staticLocalOrAnonymousFunction;
			return boundNode;
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000B4CFA File Offset: 0x000B2EFA
		public override BoundNode VisitThisReference(BoundThisReference node)
		{
			this.CheckReferenceToThisOrBase(node);
			return base.VisitThisReference(node);
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000B4D0A File Offset: 0x000B2F0A
		public override BoundNode VisitBaseReference(BoundBaseReference node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsBaseAccess, node, Array.Empty<object>());
			}
			this.CheckReferenceToThisOrBase(node);
			return base.VisitBaseReference(node);
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000B4D33 File Offset: 0x000B2F33
		public override BoundNode VisitLocal(BoundLocal node)
		{
			this.CheckReferenceToVariable(node, node.LocalSymbol);
			return base.VisitLocal(node);
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000B4D49 File Offset: 0x000B2F49
		public override BoundNode VisitParameter(BoundParameter node)
		{
			this.CheckReferenceToVariable(node, node.ParameterSymbol);
			return base.VisitParameter(node);
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x000B4D60 File Offset: 0x000B2F60
		private void CheckReferenceToThisOrBase(BoundExpression node)
		{
			if (this._staticLocalOrAnonymousFunction != null)
			{
				ErrorCode errorCode = ((this._staticLocalOrAnonymousFunction.MethodKind == MethodKind.LocalFunction) ? ErrorCode.ERR_StaticLocalFunctionCannotCaptureThis : ErrorCode.ERR_StaticAnonymousFunctionCannotCaptureThis);
				this.Error(errorCode, node, Array.Empty<object>());
			}
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x000B4DA0 File Offset: 0x000B2FA0
		private void CheckReferenceToVariable(BoundExpression node, Symbol symbol)
		{
			if (this._staticLocalOrAnonymousFunction != null && Symbol.IsCaptured(symbol, this._staticLocalOrAnonymousFunction))
			{
				ErrorCode errorCode = ((this._staticLocalOrAnonymousFunction.MethodKind == MethodKind.LocalFunction) ? ErrorCode.ERR_StaticLocalFunctionCannotCaptureVariable : ErrorCode.ERR_StaticAnonymousFunctionCannotCaptureVariable);
				this.Error(errorCode, node, new object[]
				{
					new FormattedSymbol(symbol, SymbolDisplayFormat.ShortFormat)
				});
			}
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000B4DFC File Offset: 0x000B2FFC
		private void CheckReferenceToMethodIfLocalFunction(BoundExpression node, MethodSymbol method)
		{
			LocalFunctionSymbol localFunctionSymbol = ((method != null) ? method.OriginalDefinition : null) as LocalFunctionSymbol;
			if (localFunctionSymbol != null)
			{
				this.CheckReferenceToVariable(node, localFunctionSymbol);
			}
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000B4E26 File Offset: 0x000B3026
		public override BoundNode VisitConvertedSwitchExpression(BoundConvertedSwitchExpression node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsSwitchExpression, node, Array.Empty<object>());
			}
			return base.VisitConvertedSwitchExpression(node);
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000B4E48 File Offset: 0x000B3048
		public override BoundNode VisitDeconstructionAssignmentOperator(BoundDeconstructionAssignmentOperator node)
		{
			if (!node.HasAnyErrors)
			{
				this.CheckForDeconstructionAssignmentToSelf(node.Left, node.Right);
			}
			return base.VisitDeconstructionAssignmentOperator(node);
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000B4E6C File Offset: 0x000B306C
		public override BoundNode VisitAssignmentOperator(BoundAssignmentOperator node)
		{
			this.CheckForAssignmentToSelf(node);
			if (this._inExpressionLambda && node.Left.Kind != BoundKind.ObjectInitializerMember && node.Left.Kind != BoundKind.DynamicObjectInitializerMember)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsAssignment, node, Array.Empty<object>());
			}
			return base.VisitAssignmentOperator(node);
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x000B4EC5 File Offset: 0x000B30C5
		public override BoundNode VisitDynamicObjectInitializerMember(BoundDynamicObjectInitializerMember node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsDynamicOperation, node, Array.Empty<object>());
			}
			return base.VisitDynamicObjectInitializerMember(node);
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x000B4EE8 File Offset: 0x000B30E8
		public override BoundNode VisitEventAccess(BoundEventAccess node)
		{
			if (node.IsUsableAsField)
			{
				bool flag = node.ReceiverOpt != null && node.ReceiverOpt.Kind == BoundKind.BaseReference;
				Binder.ReportDiagnosticsIfObsolete(this._diagnostics, node.EventSymbol.AssociatedField, node.Syntax, flag, this._containingSymbol, this._containingSymbol.ContainingType, BinderFlags.None);
			}
			this.CheckReceiverIfField(node.ReceiverOpt);
			return base.VisitEventAccess(node);
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x000B4F60 File Offset: 0x000B3160
		public override BoundNode VisitEventAssignmentOperator(BoundEventAssignmentOperator node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsAssignment, node, Array.Empty<object>());
			}
			bool flag = node.ReceiverOpt != null && node.ReceiverOpt.Kind == BoundKind.BaseReference;
			Binder.ReportDiagnosticsIfObsolete(this._diagnostics, node.Event, ((AssignmentExpressionSyntax)node.Syntax).Left, flag, this._containingSymbol, this._containingSymbol.ContainingType, BinderFlags.None);
			this.CheckReceiverIfField(node.ReceiverOpt);
			return base.VisitEventAssignmentOperator(node);
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x000B4FED File Offset: 0x000B31ED
		public override BoundNode VisitCompoundAssignmentOperator(BoundCompoundAssignmentOperator node)
		{
			this.CheckCompoundAssignmentOperator(node);
			return base.VisitCompoundAssignmentOperator(node);
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x000B5000 File Offset: 0x000B3200
		private void VisitCall(MethodSymbol method, PropertySymbol propertyAccess, ImmutableArray<BoundExpression> arguments, ImmutableArray<RefKind> argumentRefKindsOpt, ImmutableArray<string> argumentNamesOpt, BitVector defaultArguments, BoundNode node)
		{
			this.CheckArguments(argumentRefKindsOpt, arguments, method);
			if (this._inExpressionLambda)
			{
				if (method.CallsAreOmitted(node.SyntaxTree))
				{
					this.Error(ErrorCode.ERR_PartialMethodInExpressionTree, node, Array.Empty<object>());
					return;
				}
				if (propertyAccess != null && propertyAccess.IsIndexedProperty() && !propertyAccess.IsIndexer)
				{
					this.Error(ErrorCode.ERR_ExpressionTreeContainsIndexedProperty, node, Array.Empty<object>());
					return;
				}
				if (DiagnosticsPass.<VisitCall>g__hasDefaultArgument|32_0(arguments, defaultArguments))
				{
					this.Error(ErrorCode.ERR_ExpressionTreeContainsOptionalArgument, node, Array.Empty<object>());
					return;
				}
				if (!argumentNamesOpt.IsDefaultOrEmpty)
				{
					this.Error(ErrorCode.ERR_ExpressionTreeContainsNamedArgument, node, Array.Empty<object>());
					return;
				}
				if (DiagnosticsPass.IsComCallWithRefOmitted(method, arguments, argumentRefKindsOpt))
				{
					this.Error(ErrorCode.ERR_ComRefCallInExpressionTree, node, Array.Empty<object>());
					return;
				}
				if (method.MethodKind == MethodKind.LocalFunction)
				{
					this.Error(ErrorCode.ERR_ExpressionTreeContainsLocalFunction, node, Array.Empty<object>());
					return;
				}
				if (method.RefKind != RefKind.None)
				{
					this.Error(ErrorCode.ERR_RefReturningCallInExpressionTree, node, Array.Empty<object>());
				}
			}
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000B50F8 File Offset: 0x000B32F8
		public override BoundNode Visit(BoundNode node)
		{
			if (this._inExpressionLambda && !(node is BoundConversion))
			{
				BoundExpression boundExpression = node as BoundExpression;
				if (boundExpression != null)
				{
					TypeSymbol type = boundExpression.Type;
					if (type != null && type.IsRestrictedType(false))
					{
						this.Error(ErrorCode.ERR_ExpressionTreeCantContainRefStruct, node, new object[] { type.Name });
					}
				}
			}
			return base.Visit(node);
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x000B5154 File Offset: 0x000B3354
		public override BoundNode VisitRefTypeOperator(BoundRefTypeOperator node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_FeatureNotValidInExpressionTree, node, new object[] { "__reftype" });
			}
			return base.VisitRefTypeOperator(node);
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x000B517F File Offset: 0x000B337F
		public override BoundNode VisitRefValueOperator(BoundRefValueOperator node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_FeatureNotValidInExpressionTree, node, new object[] { "__refvalue" });
			}
			return base.VisitRefValueOperator(node);
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x000B51AA File Offset: 0x000B33AA
		public override BoundNode VisitMakeRefOperator(BoundMakeRefOperator node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_FeatureNotValidInExpressionTree, node, new object[] { "__makeref" });
			}
			return base.VisitMakeRefOperator(node);
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x000B51D5 File Offset: 0x000B33D5
		public override BoundNode VisitArgListOperator(BoundArgListOperator node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_VarArgsInExpressionTree, node, Array.Empty<object>());
			}
			return base.VisitArgListOperator(node);
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x000B51F7 File Offset: 0x000B33F7
		public override BoundNode VisitConditionalAccess(BoundConditionalAccess node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_NullPropagatingOpInExpressionTree, node, Array.Empty<object>());
			}
			return base.VisitConditionalAccess(node);
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000B521C File Offset: 0x000B341C
		public override BoundNode VisitObjectInitializerMember(BoundObjectInitializerMember node)
		{
			if (this._inExpressionLambda && !node.Arguments.IsDefaultOrEmpty)
			{
				this.Error(ErrorCode.ERR_DictionaryInitializerInExpressionTree, node, Array.Empty<object>());
			}
			PropertySymbol propertySymbol = node.MemberSymbol as PropertySymbol;
			if (propertySymbol != null)
			{
				this.CheckRefReturningPropertyAccess(node, propertySymbol);
			}
			return base.VisitObjectInitializerMember(node);
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x000B5270 File Offset: 0x000B3470
		public override BoundNode VisitCall(BoundCall node)
		{
			this.VisitCall(node.Method, null, node.Arguments, node.ArgumentRefKindsOpt, node.ArgumentNamesOpt, node.DefaultArguments, node);
			this.CheckReceiverIfField(node.ReceiverOpt);
			this.CheckReferenceToMethodIfLocalFunction(node, node.Method);
			return base.VisitCall(node);
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x000B52C3 File Offset: 0x000B34C3
		private void CheckOutDeclaration(BoundLocal local)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsOutVariable, local, Array.Empty<object>());
			}
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000B52DE File Offset: 0x000B34DE
		private void CheckDiscard(BoundDiscardExpression argument)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsDiscard, argument, Array.Empty<object>());
			}
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x000B52FC File Offset: 0x000B34FC
		public override BoundNode VisitCollectionElementInitializer(BoundCollectionElementInitializer node)
		{
			if (this._inExpressionLambda && node.AddMethod.IsStatic)
			{
				this.Error(ErrorCode.ERR_ExtensionCollectionElementInitializerInExpressionTree, node, Array.Empty<object>());
			}
			this.VisitCall(node.AddMethod, null, node.Arguments, default(ImmutableArray<RefKind>), default(ImmutableArray<string>), node.DefaultArguments, node);
			return base.VisitCollectionElementInitializer(node);
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x000B5362 File Offset: 0x000B3562
		public override BoundNode VisitObjectCreationExpression(BoundObjectCreationExpression node)
		{
			this.VisitCall(node.Constructor, null, node.Arguments, node.ArgumentRefKindsOpt, node.ArgumentNamesOpt, node.DefaultArguments, node);
			return base.VisitObjectCreationExpression(node);
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000B5394 File Offset: 0x000B3594
		public override BoundNode VisitIndexerAccess(BoundIndexerAccess node)
		{
			PropertySymbol indexer = node.Indexer;
			MethodSymbol methodSymbol = indexer.GetOwnOrInheritedGetMethod() ?? indexer.GetOwnOrInheritedSetMethod();
			if (methodSymbol != null)
			{
				this.VisitCall(methodSymbol, indexer, node.Arguments, node.ArgumentRefKindsOpt, node.ArgumentNamesOpt, node.DefaultArguments, node);
			}
			this.CheckReceiverIfField(node.ReceiverOpt);
			return base.VisitIndexerAccess(node);
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000B53F0 File Offset: 0x000B35F0
		private void CheckRefReturningPropertyAccess(BoundNode node, PropertySymbol property)
		{
			if (this._inExpressionLambda && property.RefKind != RefKind.None)
			{
				this.Error(ErrorCode.ERR_RefReturningCallInExpressionTree, node, Array.Empty<object>());
			}
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x000B5414 File Offset: 0x000B3614
		public override BoundNode VisitPropertyAccess(BoundPropertyAccess node)
		{
			PropertySymbol propertySymbol = node.PropertySymbol;
			this.CheckRefReturningPropertyAccess(node, propertySymbol);
			this.CheckReceiverIfField(node.ReceiverOpt);
			return base.VisitPropertyAccess(node);
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x000B5444 File Offset: 0x000B3644
		public override BoundNode VisitLambda(BoundLambda node)
		{
			if (this._inExpressionLambda)
			{
				foreach (ParameterSymbol parameterSymbol in node.Symbol.Parameters)
				{
					if (parameterSymbol.RefKind != RefKind.None && parameterSymbol.Locations.Length != 0)
					{
						this._diagnostics.Add(ErrorCode.ERR_ByRefParameterInExpressionTree, parameterSymbol.Locations[0]);
					}
					if (parameterSymbol.TypeWithAnnotations.IsRestrictedType(false))
					{
						this._diagnostics.Add(ErrorCode.ERR_ExpressionTreeCantContainRefStruct, parameterSymbol.Locations[0], new object[] { parameterSymbol.Type.Name });
					}
				}
				switch (node.Syntax.Kind())
				{
				case SyntaxKind.AnonymousMethodExpression:
					this.Error(ErrorCode.ERR_ExpressionTreeContainsAnonymousMethod, node, Array.Empty<object>());
					break;
				case SyntaxKind.SimpleLambdaExpression:
				{
					SimpleLambdaExpressionSyntax simpleLambdaExpressionSyntax = (SimpleLambdaExpressionSyntax)node.Syntax;
					if (simpleLambdaExpressionSyntax.AsyncKeyword.Kind() == SyntaxKind.AsyncKeyword)
					{
						this.Error(ErrorCode.ERR_BadAsyncExpressionTree, node, Array.Empty<object>());
					}
					else if (simpleLambdaExpressionSyntax.Body.Kind() == SyntaxKind.Block)
					{
						this.Error(ErrorCode.ERR_StatementLambdaToExpressionTree, node, Array.Empty<object>());
					}
					else if (simpleLambdaExpressionSyntax.Body.Kind() == SyntaxKind.RefExpression)
					{
						this.Error(ErrorCode.ERR_BadRefReturnExpressionTree, node, Array.Empty<object>());
					}
					break;
				}
				case SyntaxKind.ParenthesizedLambdaExpression:
				{
					ParenthesizedLambdaExpressionSyntax parenthesizedLambdaExpressionSyntax = (ParenthesizedLambdaExpressionSyntax)node.Syntax;
					if (parenthesizedLambdaExpressionSyntax.AsyncKeyword.Kind() == SyntaxKind.AsyncKeyword)
					{
						this.Error(ErrorCode.ERR_BadAsyncExpressionTree, node, Array.Empty<object>());
					}
					else if (parenthesizedLambdaExpressionSyntax.Body.Kind() == SyntaxKind.Block)
					{
						this.Error(ErrorCode.ERR_StatementLambdaToExpressionTree, node, Array.Empty<object>());
					}
					else if (parenthesizedLambdaExpressionSyntax.Body.Kind() == SyntaxKind.RefExpression)
					{
						this.Error(ErrorCode.ERR_BadRefReturnExpressionTree, node, Array.Empty<object>());
					}
					break;
				}
				}
			}
			SourceMethodSymbol staticLocalOrAnonymousFunction = this._staticLocalOrAnonymousFunction;
			if (node.Symbol.IsStatic)
			{
				this._staticLocalOrAnonymousFunction = node.Symbol;
			}
			BoundNode boundNode = base.VisitLambda(node);
			this._staticLocalOrAnonymousFunction = staticLocalOrAnonymousFunction;
			return boundNode;
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x000B5680 File Offset: 0x000B3880
		public override BoundNode VisitBinaryOperator(BoundBinaryOperator node)
		{
			BoundBinaryOperator boundBinaryOperator = node;
			for (;;)
			{
				this.CheckBinaryOperator(boundBinaryOperator);
				this.Visit(boundBinaryOperator.Right);
				if (boundBinaryOperator.Left.Kind != BoundKind.BinaryOperator)
				{
					break;
				}
				boundBinaryOperator = (BoundBinaryOperator)boundBinaryOperator.Left;
			}
			this.Visit(boundBinaryOperator.Left);
			return null;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000B56CE File Offset: 0x000B38CE
		public override BoundNode VisitUserDefinedConditionalLogicalOperator(BoundUserDefinedConditionalLogicalOperator node)
		{
			this.CheckLiftedUserDefinedConditionalLogicalOperator(node);
			return base.VisitUserDefinedConditionalLogicalOperator(node);
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x000B56DE File Offset: 0x000B38DE
		private void CheckDynamic(BoundUnaryOperator node)
		{
			if (this._inExpressionLambda && node.OperatorKind.IsDynamic())
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsDynamicOperation, node, Array.Empty<object>());
			}
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x000B5706 File Offset: 0x000B3906
		private void CheckDynamic(BoundBinaryOperator node)
		{
			if (this._inExpressionLambda && node.OperatorKind.IsDynamic())
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsDynamicOperation, node, Array.Empty<object>());
			}
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x000B572E File Offset: 0x000B392E
		public override BoundNode VisitUnaryOperator(BoundUnaryOperator node)
		{
			this.CheckUnsafeType(node);
			this.CheckLiftedUnaryOp(node);
			this.CheckDynamic(node);
			return base.VisitUnaryOperator(node);
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x000B574C File Offset: 0x000B394C
		public override BoundNode VisitAddressOfOperator(BoundAddressOfOperator node)
		{
			this.CheckUnsafeType(node);
			BoundExpression operand = node.Operand;
			if (operand.Kind == BoundKind.FieldAccess)
			{
				this.CheckFieldAddress((BoundFieldAccess)operand, null);
			}
			return base.VisitAddressOfOperator(node);
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x000B5788 File Offset: 0x000B3988
		public override BoundNode VisitIncrementOperator(BoundIncrementOperator node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsAssignment, node, Array.Empty<object>());
			}
			return base.VisitIncrementOperator(node);
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x000B57AA File Offset: 0x000B39AA
		public override BoundNode VisitPointerElementAccess(BoundPointerElementAccess node)
		{
			this.NoteUnsafe(node);
			return base.VisitPointerElementAccess(node);
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x000B57BA File Offset: 0x000B39BA
		public override BoundNode VisitPointerIndirectionOperator(BoundPointerIndirectionOperator node)
		{
			this.NoteUnsafe(node);
			return base.VisitPointerIndirectionOperator(node);
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x000B57CC File Offset: 0x000B39CC
		public override BoundNode VisitConversion(BoundConversion node)
		{
			this.CheckUnsafeType(node.Operand);
			this.CheckUnsafeType(node);
			bool inExpressionLambda = this._inExpressionLambda;
			bool reportedUnsafe = this._reportedUnsafe;
			ConversionKind conversionKind = node.ConversionKind;
			if (conversionKind - ConversionKind.ImplicitTupleLiteral > 3)
			{
				switch (conversionKind)
				{
				case ConversionKind.ImplicitDynamic:
				case ConversionKind.ExplicitDynamic:
					if (this._inExpressionLambda)
					{
						this.Error(ErrorCode.ERR_ExpressionTreeContainsDynamicOperation, node, Array.Empty<object>());
					}
					break;
				case ConversionKind.AnonymousFunction:
					if (!inExpressionLambda && node.Type.IsExpressionTree())
					{
						this._inExpressionLambda = true;
						this._reportedUnsafe = false;
					}
					break;
				case ConversionKind.MethodGroup:
					this.CheckMethodGroup((BoundMethodGroup)node.Operand, node.Conversion.Method, true, node.Type);
					return node;
				}
			}
			else if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsTupleConversion, node, Array.Empty<object>());
			}
			BoundNode boundNode = base.VisitConversion(node);
			this._inExpressionLambda = inExpressionLambda;
			this._reportedUnsafe = reportedUnsafe;
			return boundNode;
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x000B58C0 File Offset: 0x000B3AC0
		public override BoundNode VisitDelegateCreationExpression(BoundDelegateCreationExpression node)
		{
			if (node.Argument.Kind != BoundKind.MethodGroup)
			{
				this.Visit(node.Argument);
			}
			else
			{
				this.CheckMethodGroup((BoundMethodGroup)node.Argument, node.MethodOpt, true, node.Type);
			}
			return null;
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x000B590D File Offset: 0x000B3B0D
		public override BoundNode VisitMethodGroup(BoundMethodGroup node)
		{
			this.CheckMethodGroup(node, null, false, null);
			return null;
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x000B591C File Offset: 0x000B3B1C
		private void CheckMethodGroup(BoundMethodGroup node, MethodSymbol method, bool parentIsConversion, TypeSymbol convertedToType)
		{
			if (this._inExpressionLambda)
			{
				MethodSymbol methodSymbol = node.LookupSymbolOpt as MethodSymbol;
				if (methodSymbol != null && methodSymbol.MethodKind == MethodKind.LocalFunction)
				{
					this.Error(ErrorCode.ERR_ExpressionTreeContainsLocalFunction, node, Array.Empty<object>());
				}
				else if (parentIsConversion && convertedToType.IsFunctionPointer())
				{
					this.Error(ErrorCode.ERR_AddressOfMethodGroupInExpressionTree, node, Array.Empty<object>());
				}
			}
			this.CheckReceiverIfField(node.ReceiverOpt);
			this.CheckReferenceToMethodIfLocalFunction(node, method);
			if (method == null || method.RequiresInstanceReceiver)
			{
				this.Visit(node.ReceiverOpt);
			}
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x000B59AA File Offset: 0x000B3BAA
		public override BoundNode VisitNameOfOperator(BoundNameOfOperator node)
		{
			return node;
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x000B59B0 File Offset: 0x000B3BB0
		public override BoundNode VisitNullCoalescingOperator(BoundNullCoalescingOperator node)
		{
			if (this._inExpressionLambda && (node.LeftOperand.IsLiteralNull() || node.LeftOperand.IsLiteralDefault()))
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsBadCoalesce, node.LeftOperand, Array.Empty<object>());
			}
			return base.VisitNullCoalescingOperator(node);
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x000B59FC File Offset: 0x000B3BFC
		public override BoundNode VisitNullCoalescingAssignmentOperator(BoundNullCoalescingAssignmentOperator node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeCantContainNullCoalescingAssignment, node, Array.Empty<object>());
			}
			return base.VisitNullCoalescingAssignmentOperator(node);
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x000B5A20 File Offset: 0x000B3C20
		public override BoundNode VisitDynamicInvocation(BoundDynamicInvocation node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsDynamicOperation, node, Array.Empty<object>());
				if (node.Expression.Kind == BoundKind.MethodGroup)
				{
					return base.VisitMethodGroup((BoundMethodGroup)node.Expression);
				}
			}
			return base.VisitDynamicInvocation(node);
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x000B5A71 File Offset: 0x000B3C71
		public override BoundNode VisitDynamicIndexerAccess(BoundDynamicIndexerAccess node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsDynamicOperation, node, Array.Empty<object>());
			}
			this.CheckReceiverIfField(node.Receiver);
			return base.VisitDynamicIndexerAccess(node);
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x000B5A9F File Offset: 0x000B3C9F
		public override BoundNode VisitDynamicMemberAccess(BoundDynamicMemberAccess node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsDynamicOperation, node, Array.Empty<object>());
			}
			return base.VisitDynamicMemberAccess(node);
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x000B5AC1 File Offset: 0x000B3CC1
		public override BoundNode VisitDynamicCollectionElementInitializer(BoundDynamicCollectionElementInitializer node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsDynamicOperation, node, Array.Empty<object>());
			}
			return base.VisitDynamicCollectionElementInitializer(node);
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x000B5AE3 File Offset: 0x000B3CE3
		public override BoundNode VisitDynamicObjectCreationExpression(BoundDynamicObjectCreationExpression node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsDynamicOperation, node, Array.Empty<object>());
			}
			return base.VisitDynamicObjectCreationExpression(node);
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x000B5B05 File Offset: 0x000B3D05
		public override BoundNode VisitIsPatternExpression(BoundIsPatternExpression node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsIsMatch, node, Array.Empty<object>());
			}
			return base.VisitIsPatternExpression(node);
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000B5B27 File Offset: 0x000B3D27
		public override BoundNode VisitConvertedTupleLiteral(BoundConvertedTupleLiteral node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsTupleLiteral, node, Array.Empty<object>());
			}
			return base.VisitConvertedTupleLiteral(node);
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x000B5B49 File Offset: 0x000B3D49
		public override BoundNode VisitTupleLiteral(BoundTupleLiteral node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsTupleLiteral, node, Array.Empty<object>());
			}
			return base.VisitTupleLiteral(node);
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000B5B6B File Offset: 0x000B3D6B
		public override BoundNode VisitTupleBinaryOperator(BoundTupleBinaryOperator node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsTupleBinOp, node, Array.Empty<object>());
			}
			return base.VisitTupleBinaryOperator(node);
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x000B5B8D File Offset: 0x000B3D8D
		public override BoundNode VisitThrowExpression(BoundThrowExpression node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsThrowExpression, node, Array.Empty<object>());
			}
			return base.VisitThrowExpression(node);
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x000B5BAF File Offset: 0x000B3DAF
		public override BoundNode VisitWithExpression(BoundWithExpression node)
		{
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsWithExpression, node, Array.Empty<object>());
			}
			return base.VisitWithExpression(node);
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x000B5BD4 File Offset: 0x000B3DD4
		private void CheckArguments(ImmutableArray<RefKind> argumentRefKindsOpt, ImmutableArray<BoundExpression> arguments, Symbol method)
		{
			if (!argumentRefKindsOpt.IsDefault)
			{
				for (int i = 0; i < arguments.Length; i++)
				{
					if (argumentRefKindsOpt[i] != RefKind.None)
					{
						BoundExpression boundExpression = arguments[i];
						BoundKind kind = boundExpression.Kind;
						if (kind != BoundKind.Local)
						{
							if (kind != BoundKind.FieldAccess)
							{
								if (kind == BoundKind.DiscardExpression)
								{
									this.CheckDiscard((BoundDiscardExpression)boundExpression);
								}
							}
							else
							{
								this.CheckFieldAddress((BoundFieldAccess)boundExpression, method);
							}
						}
						else
						{
							BoundLocal boundLocal = (BoundLocal)boundExpression;
							if (boundLocal.Syntax.Kind() == SyntaxKind.DeclarationExpression)
							{
								this.CheckOutDeclaration(boundLocal);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x000B5C70 File Offset: 0x000B3E70
		private void CheckFieldAddress(BoundFieldAccess fieldAccess, Symbol consumerOpt)
		{
			FieldSymbol fieldSymbol = fieldAccess.FieldSymbol;
			if (fieldSymbol.IsVolatile && (consumerOpt == null || !this.IsInterlockedAPI(consumerOpt)))
			{
				this.Error(ErrorCode.WRN_VolatileByRef, fieldAccess, new object[] { fieldSymbol });
			}
			if (DiagnosticsPass.IsNonAgileFieldAccess(fieldAccess, this._compilation))
			{
				this.Error(ErrorCode.WRN_ByRefNonAgileField, fieldAccess, new object[] { fieldSymbol });
			}
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x000B5CD4 File Offset: 0x000B3ED4
		private void CheckFieldAsReceiver(BoundFieldAccess fieldAccess)
		{
			FieldSymbol fieldSymbol = fieldAccess.FieldSymbol;
			if (DiagnosticsPass.IsNonAgileFieldAccess(fieldAccess, this._compilation) && !fieldSymbol.Type.IsReferenceType)
			{
				this.Error(ErrorCode.WRN_CallOnNonAgileField, fieldAccess, new object[] { fieldSymbol });
			}
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000B5D19 File Offset: 0x000B3F19
		private void CheckReceiverIfField(BoundExpression receiverOpt)
		{
			if (receiverOpt != null && receiverOpt.Kind == BoundKind.FieldAccess)
			{
				this.CheckFieldAsReceiver((BoundFieldAccess)receiverOpt);
			}
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x000B5D38 File Offset: 0x000B3F38
		internal static bool IsNonAgileFieldAccess(BoundFieldAccess fieldAccess, CSharpCompilation compilation)
		{
			if (DiagnosticsPass.IsInstanceFieldAccessWithNonThisReceiver(fieldAccess))
			{
				NamedTypeSymbol wellKnownType = compilation.GetWellKnownType(WellKnownType.System_MarshalByRefObject);
				for (TypeSymbol typeSymbol = fieldAccess.FieldSymbol.ContainingType; typeSymbol != null; typeSymbol = typeSymbol.BaseTypeNoUseSiteDiagnostics)
				{
					if (TypeSymbol.Equals(typeSymbol, wellKnownType, TypeCompareKind.ConsiderEverything))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000B5D7C File Offset: 0x000B3F7C
		private static bool IsInstanceFieldAccessWithNonThisReceiver(BoundFieldAccess fieldAccess)
		{
			BoundExpression boundExpression = fieldAccess.ReceiverOpt;
			if (boundExpression == null || fieldAccess.FieldSymbol.IsStatic)
			{
				return false;
			}
			while (boundExpression.Kind == BoundKind.Conversion)
			{
				BoundConversion boundConversion = (BoundConversion)boundExpression;
				if (boundConversion.ExplicitCastInCode)
				{
					break;
				}
				boundExpression = boundConversion.Operand;
			}
			return boundExpression.Kind != BoundKind.ThisReference && boundExpression.Kind != BoundKind.BaseReference;
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x000B5DDC File Offset: 0x000B3FDC
		private bool IsInterlockedAPI(Symbol method)
		{
			NamedTypeSymbol wellKnownType = this._compilation.GetWellKnownType(WellKnownType.System_Threading_Interlocked);
			return wellKnownType != null && TypeSymbol.Equals(wellKnownType, method.ContainingType, TypeCompareKind.ConsiderEverything);
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x000B5E0C File Offset: 0x000B400C
		private static BoundExpression StripImplicitCasts(BoundExpression expr)
		{
			BoundExpression boundExpression = expr;
			for (;;)
			{
				BoundConversion boundConversion = boundExpression as BoundConversion;
				if (boundConversion == null || !boundConversion.ConversionKind.IsImplicitConversion())
				{
					break;
				}
				boundExpression = boundConversion.Operand;
			}
			return boundExpression;
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x000B5E3C File Offset: 0x000B403C
		private static bool IsSameLocalOrField(BoundExpression expr1, BoundExpression expr2)
		{
			if (expr1 == null && expr2 == null)
			{
				return true;
			}
			if (expr1 == null || expr2 == null)
			{
				return false;
			}
			if (expr1.HasAnyErrors || expr2.HasAnyErrors)
			{
				return false;
			}
			expr1 = DiagnosticsPass.StripImplicitCasts(expr1);
			expr2 = DiagnosticsPass.StripImplicitCasts(expr2);
			if (expr1.Kind != expr2.Kind)
			{
				return false;
			}
			BoundKind kind = expr1.Kind;
			switch (kind)
			{
			case BoundKind.ThisReference:
			case BoundKind.PreviousSubmissionReference:
			case BoundKind.HostObjectMemberReference:
				return true;
			case BoundKind.BaseReference:
			case BoundKind.PseudoVariable:
				break;
			case BoundKind.Local:
			{
				BoundLocal boundLocal = (BoundLocal)expr1;
				BoundLocal boundLocal2 = (BoundLocal)expr2;
				return boundLocal.LocalSymbol == boundLocal2.LocalSymbol;
			}
			case BoundKind.RangeVariable:
			{
				BoundRangeVariable boundRangeVariable = (BoundRangeVariable)expr1;
				BoundRangeVariable boundRangeVariable2 = (BoundRangeVariable)expr2;
				return boundRangeVariable.RangeVariableSymbol == boundRangeVariable2.RangeVariableSymbol;
			}
			case BoundKind.Parameter:
			{
				BoundParameter boundParameter = (BoundParameter)expr1;
				BoundParameter boundParameter2 = (BoundParameter)expr2;
				return boundParameter.ParameterSymbol == boundParameter2.ParameterSymbol;
			}
			default:
				if (kind == BoundKind.FieldAccess)
				{
					BoundFieldAccess boundFieldAccess = (BoundFieldAccess)expr1;
					BoundFieldAccess boundFieldAccess2 = (BoundFieldAccess)expr2;
					return boundFieldAccess.FieldSymbol == boundFieldAccess2.FieldSymbol && (boundFieldAccess.FieldSymbol.IsStatic || DiagnosticsPass.IsSameLocalOrField(boundFieldAccess.ReceiverOpt, boundFieldAccess2.ReceiverOpt));
				}
				if (kind == BoundKind.EventAccess)
				{
					BoundEventAccess boundEventAccess = (BoundEventAccess)expr1;
					BoundEventAccess boundEventAccess2 = (BoundEventAccess)expr2;
					return boundEventAccess.EventSymbol == boundEventAccess2.EventSymbol && (boundEventAccess.EventSymbol.IsStatic || DiagnosticsPass.IsSameLocalOrField(boundEventAccess.ReceiverOpt, boundEventAccess2.ReceiverOpt));
				}
				break;
			}
			return false;
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x000B5FC0 File Offset: 0x000B41C0
		private static bool IsComCallWithRefOmitted(MethodSymbol method, ImmutableArray<BoundExpression> arguments, ImmutableArray<RefKind> argumentRefKindsOpt)
		{
			if (method.ParameterCount != arguments.Length || method.ContainingType == null || !method.ContainingType.IsComImport)
			{
				return false;
			}
			for (int i = 0; i < arguments.Length; i++)
			{
				if (method.Parameters[i].RefKind != RefKind.None && (argumentRefKindsOpt.IsDefault || argumentRefKindsOpt[i] == RefKind.None))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000B6034 File Offset: 0x000B4234
		private void CheckBinaryOperator(BoundBinaryOperator node)
		{
			if (node.MethodOpt == null)
			{
				this.CheckUnsafeType(node.Left);
				this.CheckUnsafeType(node.Right);
			}
			this.CheckForBitwiseOrSignExtend(node, node.OperatorKind, node.Left, node.Right);
			this.CheckNullableNullBinOp(node);
			this.CheckLiftedBinOp(node);
			this.CheckRelationals(node);
			this.CheckDynamic(node);
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x000B6098 File Offset: 0x000B4298
		private void CheckCompoundAssignmentOperator(BoundCompoundAssignmentOperator node)
		{
			BoundExpression boundExpression = node.Left;
			if (!node.Operator.Kind.IsDynamic() && !node.LeftConversion.IsIdentity && node.LeftConversion.Exists)
			{
				boundExpression = new BoundConversion(boundExpression.Syntax, boundExpression, node.LeftConversion, node.Operator.Kind.IsChecked(), false, null, null, node.Operator.LeftType, false);
			}
			this.CheckForBitwiseOrSignExtend(node, node.Operator.Kind, boundExpression, node.Right);
			this.CheckLiftedCompoundAssignment(node);
			if (this._inExpressionLambda)
			{
				this.Error(ErrorCode.ERR_ExpressionTreeContainsAssignment, node, Array.Empty<object>());
			}
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x000B614C File Offset: 0x000B434C
		private void CheckRelationals(BoundBinaryOperator node)
		{
			if (!node.OperatorKind.IsComparison())
			{
				return;
			}
			if (node.Left.ConstantValue != null && node.Right.ConstantValue == null && node.Right.Kind == BoundKind.Conversion)
			{
				this.CheckVacuousComparisons(node, node.Left.ConstantValue, node.Right);
			}
			if (node.Right.ConstantValue != null && node.Left.ConstantValue == null && node.Left.Kind == BoundKind.Conversion)
			{
				this.CheckVacuousComparisons(node, node.Right.ConstantValue, node.Left);
			}
			if (node.OperatorKind == BinaryOperatorKind.ObjectEqual || node.OperatorKind == BinaryOperatorKind.ObjectNotEqual)
			{
				TypeSymbol typeSymbol;
				if (node.Left.Type.SpecialType == SpecialType.System_Object && !DiagnosticsPass.IsExplicitCast(node.Left) && (!(node.Left.ConstantValue != null) || !node.Left.ConstantValue.IsNull) && DiagnosticsPass.ConvertedHasEqual(node.OperatorKind, node.Right, out typeSymbol))
				{
					this._diagnostics.Add(ErrorCode.WRN_BadRefCompareLeft, node.Syntax.Location, new object[] { typeSymbol });
				}
				else if (node.Right.Type.SpecialType == SpecialType.System_Object && !DiagnosticsPass.IsExplicitCast(node.Right) && (!(node.Right.ConstantValue != null) || !node.Right.ConstantValue.IsNull) && DiagnosticsPass.ConvertedHasEqual(node.OperatorKind, node.Left, out typeSymbol))
				{
					this._diagnostics.Add(ErrorCode.WRN_BadRefCompareRight, node.Syntax.Location, new object[] { typeSymbol });
				}
			}
			this.CheckSelfComparisons(node);
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x000B632A File Offset: 0x000B452A
		private static bool IsExplicitCast(BoundExpression node)
		{
			return node.Kind == BoundKind.Conversion && ((BoundConversion)node).ExplicitCastInCode;
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000B6344 File Offset: 0x000B4544
		private static bool ConvertedHasEqual(BinaryOperatorKind oldOperatorKind, BoundNode node, out TypeSymbol type)
		{
			type = null;
			if (node.Kind != BoundKind.Conversion)
			{
				return false;
			}
			BoundConversion boundConversion = (BoundConversion)node;
			if (boundConversion.ExplicitCastInCode)
			{
				return false;
			}
			NamedTypeSymbol namedTypeSymbol = boundConversion.Operand.Type as NamedTypeSymbol;
			if (namedTypeSymbol == null || !namedTypeSymbol.IsReferenceType || namedTypeSymbol.IsInterface)
			{
				return false;
			}
			string text = ((oldOperatorKind == BinaryOperatorKind.ObjectEqual) ? "op_Equality" : "op_Inequality");
			for (NamedTypeSymbol namedTypeSymbol2 = namedTypeSymbol; namedTypeSymbol2 != null; namedTypeSymbol2 = namedTypeSymbol2.BaseTypeNoUseSiteDiagnostics)
			{
				foreach (Symbol symbol in namedTypeSymbol2.GetMembers(text))
				{
					MethodSymbol methodSymbol = symbol as MethodSymbol;
					if (methodSymbol != null && methodSymbol.MethodKind == MethodKind.UserDefinedOperator)
					{
						ImmutableArray<ParameterSymbol> parameters = methodSymbol.GetParameters();
						if (parameters.Length == 2 && TypeSymbol.Equals(parameters[0].Type, namedTypeSymbol2, TypeCompareKind.ConsiderEverything) && TypeSymbol.Equals(parameters[1].Type, namedTypeSymbol2, TypeCompareKind.ConsiderEverything))
						{
							type = namedTypeSymbol2;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x000B643F File Offset: 0x000B463F
		private void CheckSelfComparisons(BoundBinaryOperator node)
		{
			if (!node.HasAnyErrors && DiagnosticsPass.IsSameLocalOrField(node.Left, node.Right))
			{
				this.Error(ErrorCode.WRN_ComparisonToSelf, node, Array.Empty<object>());
			}
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x000B6470 File Offset: 0x000B4670
		private void CheckVacuousComparisons(BoundBinaryOperator tree, ConstantValue constantValue, BoundNode operand)
		{
			for (BoundConversion boundConversion = operand as BoundConversion; boundConversion != null; boundConversion = boundConversion.Operand as BoundConversion)
			{
				if (boundConversion.ConversionKind != ConversionKind.ImplicitNumeric && boundConversion.ConversionKind != ConversionKind.ImplicitConstant)
				{
					return;
				}
				if (boundConversion.ExplicitCastInCode)
				{
					return;
				}
				if (!boundConversion.Operand.Type.SpecialType.IsIntegralType() || !boundConversion.Type.SpecialType.IsIntegralType())
				{
					return;
				}
				bool flag;
				if (!Binder.CheckConstantBounds(boundConversion.Operand.Type.SpecialType, constantValue, out flag))
				{
					this.Error(ErrorCode.WRN_VacuousIntegralComp, tree, new object[] { boundConversion.Operand.Type });
					return;
				}
			}
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x000B6520 File Offset: 0x000B4720
		private void CheckForBitwiseOrSignExtend(BoundExpression node, BinaryOperatorKind operatorKind, BoundExpression leftOperand, BoundExpression rightOperand)
		{
			if (operatorKind - BinaryOperatorKind.IntOr > 3 && operatorKind - BinaryOperatorKind.LiftedIntOr > 3)
			{
				return;
			}
			if (node.ConstantValue != null)
			{
				return;
			}
			ulong num = DiagnosticsPass.FindSurprisingSignExtensionBits(leftOperand);
			ulong num2 = DiagnosticsPass.FindSurprisingSignExtensionBits(rightOperand);
			if (num == num2)
			{
				return;
			}
			ConstantValue constantValue = DiagnosticsPass.GetConstantValueForBitwiseOrCheck(leftOperand);
			if (constantValue != null)
			{
				ulong uint64Value = constantValue.UInt64Value;
				if ((uint64Value & num2) == num2 || (~uint64Value & num2) == num2)
				{
					return;
				}
			}
			constantValue = DiagnosticsPass.GetConstantValueForBitwiseOrCheck(rightOperand);
			if (constantValue != null)
			{
				ulong uint64Value2 = constantValue.UInt64Value;
				if ((uint64Value2 & num) == num || (~uint64Value2 & num) == num)
				{
					return;
				}
			}
			this.Error(ErrorCode.WRN_BitwiseOrSignExtend, node, Array.Empty<object>());
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x000B65C4 File Offset: 0x000B47C4
		private static ConstantValue GetConstantValueForBitwiseOrCheck(BoundExpression operand)
		{
			if (operand.Kind == BoundKind.Conversion)
			{
				BoundConversion boundConversion = (BoundConversion)operand;
				if (boundConversion.ConversionKind == ConversionKind.ImplicitNullable)
				{
					operand = boundConversion.Operand;
				}
			}
			ConstantValue constantValue = operand.ConstantValue;
			if (constantValue == null || !constantValue.IsIntegral)
			{
				return null;
			}
			return constantValue;
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x000B6610 File Offset: 0x000B4810
		private static ulong FindSurprisingSignExtensionBits(BoundExpression expr)
		{
			if (expr.Kind != BoundKind.Conversion)
			{
				return 0UL;
			}
			BoundConversion boundConversion = (BoundConversion)expr;
			TypeSymbol typeSymbol = boundConversion.Operand.Type;
			TypeSymbol typeSymbol2 = boundConversion.Type;
			if (typeSymbol == null || typeSymbol2 == null)
			{
				return 0UL;
			}
			if (typeSymbol.IsNullableType())
			{
				typeSymbol = typeSymbol.GetNullableUnderlyingType();
			}
			if (typeSymbol2.IsNullableType())
			{
				typeSymbol2 = typeSymbol2.GetNullableUnderlyingType();
			}
			SpecialType specialType = typeSymbol.SpecialType;
			SpecialType specialType2 = typeSymbol2.SpecialType;
			if (!specialType.IsIntegralType() || !specialType2.IsIntegralType())
			{
				return 0UL;
			}
			int num = specialType.SizeInBytes();
			int num2 = specialType2.SizeInBytes();
			if (num == 0 || num2 == 0)
			{
				return 0UL;
			}
			ulong num3 = DiagnosticsPass.FindSurprisingSignExtensionBits(boundConversion.Operand);
			if (num == num2)
			{
				return num3;
			}
			if (num2 < num)
			{
				switch (num2)
				{
				case 1:
					return (ulong)((byte)num3);
				case 2:
					return (ulong)((ushort)num3);
				case 4:
					return (ulong)((uint)num3);
				}
				return num3;
			}
			if (!specialType.IsSignedIntegralType())
			{
				return num3;
			}
			if (boundConversion.ExplicitCastInCode && specialType2.IsSignedIntegralType())
			{
				return num3;
			}
			ulong num4 = num3;
			for (int i = num; i < num2; i++)
			{
				num4 |= 255UL << i * 8;
			}
			return num4;
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x000B673D File Offset: 0x000B493D
		private void CheckLiftedCompoundAssignment(BoundCompoundAssignmentOperator node)
		{
			if (!node.Operator.Kind.IsLifted())
			{
				return;
			}
			if (node.Right.NullableNeverHasValue())
			{
				this.Error(ErrorCode.WRN_AlwaysNull, node, new object[] { node.Type });
			}
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000B677A File Offset: 0x000B497A
		private void CheckLiftedUnaryOp(BoundUnaryOperator node)
		{
			if (!node.OperatorKind.IsLifted())
			{
				return;
			}
			if (node.Operand.NullableNeverHasValue())
			{
				this.Error(ErrorCode.WRN_AlwaysNull, node, new object[] { node.Type });
			}
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x000B67B4 File Offset: 0x000B49B4
		private void CheckNullableNullBinOp(BoundBinaryOperator node)
		{
			if (node.OperatorKind.OperandTypes() != BinaryOperatorKind.NullableNull)
			{
				return;
			}
			BinaryOperatorKind binaryOperatorKind = node.OperatorKind.Operator();
			if (binaryOperatorKind == BinaryOperatorKind.Equal || binaryOperatorKind == BinaryOperatorKind.NotEqual)
			{
				string text = ((node.OperatorKind.Operator() == BinaryOperatorKind.NotEqual) ? "true" : "false");
				if (node.Right.IsLiteralNull() && node.Left.NullableAlwaysHasValue())
				{
					this.Error(ErrorCode.WRN_NubExprIsConstBool, node, new object[]
					{
						text,
						node.Left.Type.GetNullableUnderlyingType(),
						node.Left.Type
					});
					return;
				}
				if (node.Left.IsLiteralNull() && node.Right.NullableAlwaysHasValue())
				{
					this.Error(ErrorCode.WRN_NubExprIsConstBool, node, new object[]
					{
						text,
						node.Right.Type.GetNullableUnderlyingType(),
						node.Right.Type
					});
				}
			}
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000B68B4 File Offset: 0x000B4AB4
		private void CheckLiftedBinOp(BoundBinaryOperator node)
		{
			if (!node.OperatorKind.IsLifted())
			{
				return;
			}
			BinaryOperatorKind binaryOperatorKind = node.OperatorKind.Operator();
			if (binaryOperatorKind <= BinaryOperatorKind.LessThan)
			{
				if (binaryOperatorKind <= BinaryOperatorKind.NotEqual)
				{
					if (binaryOperatorKind != BinaryOperatorKind.Equal && binaryOperatorKind != BinaryOperatorKind.NotEqual)
					{
						goto IL_0232;
					}
					string text = ((node.OperatorKind.Operator() == BinaryOperatorKind.NotEqual) ? "true" : "false");
					if (node.Right.NullableNeverHasValue() && node.Left.NullableAlwaysHasValue())
					{
						this.Error(node.OperatorKind.IsUserDefined() ? ErrorCode.WRN_NubExprIsConstBool2 : ErrorCode.WRN_NubExprIsConstBool, node, new object[]
						{
							text,
							node.Left.Type.GetNullableUnderlyingType(),
							DiagnosticsPass.GetTypeForLiftedComparisonWarning(node.Right)
						});
						return;
					}
					if (node.Left.NullableNeverHasValue() && node.Right.NullableAlwaysHasValue())
					{
						this.Error(node.OperatorKind.IsUserDefined() ? ErrorCode.WRN_NubExprIsConstBool2 : ErrorCode.WRN_NubExprIsConstBool, node, new object[]
						{
							text,
							node.Right.Type.GetNullableUnderlyingType(),
							DiagnosticsPass.GetTypeForLiftedComparisonWarning(node.Left)
						});
						return;
					}
					return;
				}
				else if (binaryOperatorKind != BinaryOperatorKind.GreaterThan && binaryOperatorKind != BinaryOperatorKind.LessThan)
				{
					goto IL_0232;
				}
			}
			else if (binaryOperatorKind <= BinaryOperatorKind.LessThanOrEqual)
			{
				if (binaryOperatorKind != BinaryOperatorKind.GreaterThanOrEqual && binaryOperatorKind != BinaryOperatorKind.LessThanOrEqual)
				{
					goto IL_0232;
				}
			}
			else
			{
				if (binaryOperatorKind != BinaryOperatorKind.And && binaryOperatorKind != BinaryOperatorKind.Or)
				{
					goto IL_0232;
				}
				if ((node.Left.NullableNeverHasValue() && node.Right.IsNullableNonBoolean()) || (node.Left.IsNullableNonBoolean() && node.Right.NullableNeverHasValue()))
				{
					this.Error(ErrorCode.WRN_AlwaysNull, node, new object[] { node.Type });
					return;
				}
				return;
			}
			if (node.Right.NullableNeverHasValue())
			{
				this.Error(ErrorCode.WRN_CmpAlwaysFalse, node, new object[] { DiagnosticsPass.GetTypeForLiftedComparisonWarning(node.Right) });
				return;
			}
			if (node.Left.NullableNeverHasValue())
			{
				this.Error(ErrorCode.WRN_CmpAlwaysFalse, node, new object[] { DiagnosticsPass.GetTypeForLiftedComparisonWarning(node.Left) });
				return;
			}
			return;
			IL_0232:
			if (node.Right.NullableNeverHasValue() || node.Left.NullableNeverHasValue())
			{
				this.Error(ErrorCode.WRN_AlwaysNull, node, new object[] { node.Type });
			}
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000B6B28 File Offset: 0x000B4D28
		private void CheckLiftedUserDefinedConditionalLogicalOperator(BoundUserDefinedConditionalLogicalOperator node)
		{
			if (node.Right.NullableNeverHasValue() || node.Left.NullableNeverHasValue())
			{
				this.Error(ErrorCode.WRN_AlwaysNull, node, new object[] { node.Type });
			}
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x000B6B60 File Offset: 0x000B4D60
		private static TypeSymbol GetTypeForLiftedComparisonWarning(BoundExpression node)
		{
			if (node.Type == null || !node.Type.IsNullableType())
			{
				return null;
			}
			TypeSymbol typeSymbol = null;
			if (node.Kind == BoundKind.Conversion)
			{
				BoundConversion boundConversion = (BoundConversion)node;
				if (boundConversion.ConversionKind == ConversionKind.ExplicitNullable || boundConversion.ConversionKind == ConversionKind.ImplicitNullable)
				{
					typeSymbol = DiagnosticsPass.GetTypeForLiftedComparisonWarning(boundConversion.Operand);
				}
			}
			return typeSymbol ?? node.Type;
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x000B6BC2 File Offset: 0x000B4DC2
		private bool CheckForAssignmentToSelf(BoundAssignmentOperator node)
		{
			if (!node.HasAnyErrors && DiagnosticsPass.IsSameLocalOrField(node.Left, node.Right))
			{
				this.Error(ErrorCode.WRN_AssignmentToSelf, node, Array.Empty<object>());
				return true;
			}
			return false;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000B6BF4 File Offset: 0x000B4DF4
		private void CheckForDeconstructionAssignmentToSelf(BoundTupleExpression leftTuple, BoundExpression right)
		{
			while (right.Kind == BoundKind.Conversion)
			{
				BoundConversion boundConversion = (BoundConversion)right;
				ConversionKind conversionKind = boundConversion.ConversionKind;
				if (conversionKind != ConversionKind.Identity && conversionKind != ConversionKind.ImplicitTupleLiteral && conversionKind != ConversionKind.Deconstruction)
				{
					return;
				}
				right = boundConversion.Operand;
			}
			if (right.Kind != BoundKind.ConvertedTupleLiteral && right.Kind != BoundKind.TupleLiteral)
			{
				return;
			}
			BoundTupleExpression boundTupleExpression = (BoundTupleExpression)right;
			ImmutableArray<BoundExpression> arguments = leftTuple.Arguments;
			int length = arguments.Length;
			for (int i = 0; i < length; i++)
			{
				BoundExpression boundExpression = arguments[i];
				BoundExpression boundExpression2 = boundTupleExpression.Arguments[i];
				BoundTupleExpression boundTupleExpression2 = boundExpression as BoundTupleExpression;
				if (boundTupleExpression2 != null)
				{
					this.CheckForDeconstructionAssignmentToSelf(boundTupleExpression2, boundExpression2);
				}
				else if (DiagnosticsPass.IsSameLocalOrField(boundExpression, boundExpression2))
				{
					this.Error(ErrorCode.WRN_AssignmentToSelf, boundExpression, Array.Empty<object>());
				}
			}
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x000B6CCC File Offset: 0x000B4ECC
		public override BoundNode VisitFieldAccess(BoundFieldAccess node)
		{
			this.CheckReceiverIfField(node.ReceiverOpt);
			return base.VisitFieldAccess(node);
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000B6CE1 File Offset: 0x000B4EE1
		public override BoundNode VisitPropertyGroup(BoundPropertyGroup node)
		{
			this.CheckReceiverIfField(node.ReceiverOpt);
			return base.VisitPropertyGroup(node);
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x000B6CF8 File Offset: 0x000B4EF8
		[CompilerGenerated]
		internal static bool <VisitCall>g__hasDefaultArgument|32_0(ImmutableArray<BoundExpression> arguments, BitVector defaultArguments)
		{
			for (int i = 0; i < arguments.Length; i++)
			{
				if (defaultArguments[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040010A0 RID: 4256
		private readonly BindingDiagnosticBag _diagnostics;

		// Token: 0x040010A1 RID: 4257
		private readonly CSharpCompilation _compilation;

		// Token: 0x040010A2 RID: 4258
		private bool _inExpressionLambda;

		// Token: 0x040010A3 RID: 4259
		private bool _reportedUnsafe;

		// Token: 0x040010A4 RID: 4260
		private readonly MethodSymbol _containingSymbol;

		// Token: 0x040010A5 RID: 4261
		private SourceMethodSymbol _staticLocalOrAnonymousFunction;
	}
}
