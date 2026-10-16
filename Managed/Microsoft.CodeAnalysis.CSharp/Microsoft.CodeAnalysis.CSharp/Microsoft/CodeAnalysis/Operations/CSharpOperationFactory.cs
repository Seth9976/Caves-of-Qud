using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000009 RID: 9
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class CSharpOperationFactory
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000023DF File Offset: 0x000005DF
		public CSharpOperationFactory(SemanticModel semanticModel)
		{
			this._semanticModel = semanticModel;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023F0 File Offset: 0x000005F0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("boundNode")]
		public IOperation Create(BoundNode boundNode)
		{
			if (boundNode == null)
			{
				return null;
			}
			switch (boundNode.Kind)
			{
			case BoundKind.FieldEqualsValue:
				return this.CreateBoundFieldEqualsValueOperation((BoundFieldEqualsValue)boundNode);
			case BoundKind.PropertyEqualsValue:
				return this.CreateBoundPropertyEqualsValueOperation((BoundPropertyEqualsValue)boundNode);
			case BoundKind.ParameterEqualsValue:
				return this.CreateBoundParameterEqualsValueOperation((BoundParameterEqualsValue)boundNode);
			case BoundKind.GlobalStatementInitializer:
			case BoundKind.TypeExpression:
			case BoundKind.TypeOrValueExpression:
			case BoundKind.NamespaceExpression:
			case BoundKind.PointerIndirectionOperator:
			case BoundKind.PointerElementAccess:
			case BoundKind.RefTypeOperator:
			case BoundKind.MakeRefOperator:
			case BoundKind.RefValueOperator:
			case BoundKind.ArgList:
			case BoundKind.ArgListOperator:
			case BoundKind.FixedLocalCollectionInitializer:
			case BoundKind.PreviousSubmissionReference:
			case BoundKind.HostObjectMemberReference:
			case BoundKind.Sequence:
			case BoundKind.MethodGroup:
			case BoundKind.Attribute:
			case BoundKind.StackAllocArrayCreation:
			case BoundKind.ConvertedStackAllocExpression:
			case BoundKind.IndexOrRangePatternIndexerAccess:
			{
				BoundExpression boundExpression = boundNode as BoundExpression;
				ConstantValue constantValue = ((boundExpression != null) ? boundExpression.ConstantValue : null);
				bool flag = boundNode.WasCompilerGenerated;
				if (!flag && boundNode.Kind == BoundKind.FixedLocalCollectionInitializer)
				{
					flag = true;
				}
				return new NoneOperation(this.GetIOperationChildren(boundNode), this._semanticModel, boundNode.Syntax, null, constantValue, flag);
			}
			case BoundKind.DeconstructValuePlaceholder:
				return this.CreateBoundDeconstructValuePlaceholderOperation((BoundDeconstructValuePlaceholder)boundNode);
			case BoundKind.ObjectOrCollectionValuePlaceholder:
				return this.CreateCollectionValuePlaceholderOperation((BoundObjectOrCollectionValuePlaceholder)boundNode);
			case BoundKind.BadExpression:
				return this.CreateBoundBadExpressionOperation((BoundBadExpression)boundNode);
			case BoundKind.BadStatement:
				return this.CreateBoundBadStatementOperation((BoundBadStatement)boundNode);
			case BoundKind.UnaryOperator:
				return this.CreateBoundUnaryOperatorOperation((BoundUnaryOperator)boundNode);
			case BoundKind.IncrementOperator:
				return this.CreateBoundIncrementOperatorOperation((BoundIncrementOperator)boundNode);
			case BoundKind.AddressOfOperator:
				return this.CreateBoundAddressOfOperatorOperation((BoundAddressOfOperator)boundNode);
			case BoundKind.UnconvertedAddressOfOperator:
				return this.CreateBoundUnconvertedAddressOfOperatorOperation((BoundUnconvertedAddressOfOperator)boundNode);
			case BoundKind.FunctionPointerInvocation:
				return this.CreateBoundFunctionPointerInvocationOperation((BoundFunctionPointerInvocation)boundNode);
			case BoundKind.FromEndIndexExpression:
				return this.CreateFromEndIndexExpressionOperation((BoundFromEndIndexExpression)boundNode);
			case BoundKind.RangeExpression:
				return this.CreateRangeExpressionOperation((BoundRangeExpression)boundNode);
			case BoundKind.BinaryOperator:
			case BoundKind.UserDefinedConditionalLogicalOperator:
				return this.CreateBoundBinaryOperatorBase((BoundBinaryOperatorBase)boundNode);
			case BoundKind.TupleBinaryOperator:
				return this.CreateBoundTupleBinaryOperatorOperation((BoundTupleBinaryOperator)boundNode);
			case BoundKind.CompoundAssignmentOperator:
				return this.CreateBoundCompoundAssignmentOperatorOperation((BoundCompoundAssignmentOperator)boundNode);
			case BoundKind.AssignmentOperator:
				return this.CreateBoundAssignmentOperatorOrMemberInitializerOperation((BoundAssignmentOperator)boundNode);
			case BoundKind.DeconstructionAssignmentOperator:
				return this.CreateBoundDeconstructionAssignmentOperator((BoundDeconstructionAssignmentOperator)boundNode);
			case BoundKind.NullCoalescingOperator:
				return this.CreateBoundNullCoalescingOperatorOperation((BoundNullCoalescingOperator)boundNode);
			case BoundKind.NullCoalescingAssignmentOperator:
				return this.CreateBoundNullCoalescingAssignmentOperatorOperation((BoundNullCoalescingAssignmentOperator)boundNode);
			case BoundKind.UnconvertedConditionalOperator:
				throw ExceptionUtilities.Unreachable;
			case BoundKind.ConditionalOperator:
				return this.CreateBoundConditionalOperatorOperation((BoundConditionalOperator)boundNode);
			case BoundKind.ArrayAccess:
				return this.CreateBoundArrayAccessOperation((BoundArrayAccess)boundNode);
			case BoundKind.AwaitExpression:
				return this.CreateBoundAwaitExpressionOperation((BoundAwaitExpression)boundNode);
			case BoundKind.TypeOfOperator:
				return this.CreateBoundTypeOfOperatorOperation((BoundTypeOfOperator)boundNode);
			case BoundKind.DefaultLiteral:
				return this.CreateBoundDefaultLiteralOperation((BoundDefaultLiteral)boundNode);
			case BoundKind.DefaultExpression:
				return this.CreateBoundDefaultExpressionOperation((BoundDefaultExpression)boundNode);
			case BoundKind.IsOperator:
				return this.CreateBoundIsOperatorOperation((BoundIsOperator)boundNode);
			case BoundKind.AsOperator:
				return this.CreateBoundAsOperatorOperation((BoundAsOperator)boundNode);
			case BoundKind.SizeOfOperator:
				return this.CreateBoundSizeOfOperatorOperation((BoundSizeOfOperator)boundNode);
			case BoundKind.Conversion:
				return this.CreateBoundConversionOperation((BoundConversion)boundNode);
			case BoundKind.Block:
				return this.CreateBoundBlockOperation((BoundBlock)boundNode);
			case BoundKind.LocalDeclaration:
				return this.CreateBoundLocalDeclarationOperation((BoundLocalDeclaration)boundNode);
			case BoundKind.MultipleLocalDeclarations:
			case BoundKind.UsingLocalDeclarations:
				return this.CreateBoundMultipleLocalDeclarationsBaseOperation((BoundMultipleLocalDeclarationsBase)boundNode);
			case BoundKind.LocalFunctionStatement:
				return this.CreateBoundLocalFunctionStatementOperation((BoundLocalFunctionStatement)boundNode);
			case BoundKind.NoOpStatement:
				return this.CreateBoundNoOpStatementOperation((BoundNoOpStatement)boundNode);
			case BoundKind.ReturnStatement:
				return this.CreateBoundReturnStatementOperation((BoundReturnStatement)boundNode);
			case BoundKind.YieldReturnStatement:
				return this.CreateBoundYieldReturnStatementOperation((BoundYieldReturnStatement)boundNode);
			case BoundKind.YieldBreakStatement:
				return this.CreateBoundYieldBreakStatementOperation((BoundYieldBreakStatement)boundNode);
			case BoundKind.ThrowStatement:
				return this.CreateBoundThrowStatementOperation((BoundThrowStatement)boundNode);
			case BoundKind.ExpressionStatement:
				return this.CreateBoundExpressionStatementOperation((BoundExpressionStatement)boundNode);
			case BoundKind.BreakStatement:
				return this.CreateBoundBreakStatementOperation((BoundBreakStatement)boundNode);
			case BoundKind.ContinueStatement:
				return this.CreateBoundContinueStatementOperation((BoundContinueStatement)boundNode);
			case BoundKind.SwitchStatement:
				return this.CreateBoundSwitchStatementOperation((BoundSwitchStatement)boundNode);
			case BoundKind.IfStatement:
				return this.CreateBoundIfStatementOperation((BoundIfStatement)boundNode);
			case BoundKind.DoStatement:
				return this.CreateBoundDoStatementOperation((BoundDoStatement)boundNode);
			case BoundKind.WhileStatement:
				return this.CreateBoundWhileStatementOperation((BoundWhileStatement)boundNode);
			case BoundKind.ForStatement:
				return this.CreateBoundForStatementOperation((BoundForStatement)boundNode);
			case BoundKind.ForEachStatement:
				return this.CreateBoundForEachStatementOperation((BoundForEachStatement)boundNode);
			case BoundKind.UsingStatement:
				return this.CreateBoundUsingStatementOperation((BoundUsingStatement)boundNode);
			case BoundKind.FixedStatement:
				return this.CreateBoundFixedStatementOperation((BoundFixedStatement)boundNode);
			case BoundKind.LockStatement:
				return this.CreateBoundLockStatementOperation((BoundLockStatement)boundNode);
			case BoundKind.TryStatement:
				return this.CreateBoundTryStatementOperation((BoundTryStatement)boundNode);
			case BoundKind.CatchBlock:
				return this.CreateBoundCatchBlockOperation((BoundCatchBlock)boundNode);
			case BoundKind.Literal:
				return this.CreateBoundLiteralOperation((BoundLiteral)boundNode, false);
			case BoundKind.ThisReference:
				return this.CreateBoundThisReferenceOperation((BoundThisReference)boundNode);
			case BoundKind.BaseReference:
				return this.CreateBoundBaseReferenceOperation((BoundBaseReference)boundNode);
			case BoundKind.Local:
				return this.CreateBoundLocalOperation((BoundLocal)boundNode, true);
			case BoundKind.RangeVariable:
				return this.CreateBoundRangeVariableOperation((BoundRangeVariable)boundNode);
			case BoundKind.Parameter:
				return this.CreateBoundParameterOperation((BoundParameter)boundNode);
			case BoundKind.LabelStatement:
				return this.CreateBoundLabelStatementOperation((BoundLabelStatement)boundNode);
			case BoundKind.GotoStatement:
				return this.CreateBoundGotoStatementOperation((BoundGotoStatement)boundNode);
			case BoundKind.LabeledStatement:
				return this.CreateBoundLabeledStatementOperation((BoundLabeledStatement)boundNode);
			case BoundKind.SwitchExpressionArm:
				return this.CreateBoundSwitchExpressionArmOperation((BoundSwitchExpressionArm)boundNode);
			case BoundKind.UnconvertedSwitchExpression:
				throw ExceptionUtilities.Unreachable;
			case BoundKind.ConvertedSwitchExpression:
				return this.CreateBoundSwitchExpressionOperation((BoundSwitchExpression)boundNode);
			case BoundKind.SwitchSection:
				return this.CreateBoundSwitchSectionOperation((BoundSwitchSection)boundNode);
			case BoundKind.SwitchLabel:
				return this.CreateBoundSwitchLabelOperation((BoundSwitchLabel)boundNode);
			case BoundKind.DynamicMemberAccess:
				return this.CreateBoundDynamicMemberAccessOperation((BoundDynamicMemberAccess)boundNode);
			case BoundKind.DynamicInvocation:
				return this.CreateBoundDynamicInvocationExpressionOperation((BoundDynamicInvocation)boundNode);
			case BoundKind.ConditionalAccess:
				return this.CreateBoundConditionalAccessOperation((BoundConditionalAccess)boundNode);
			case BoundKind.ConditionalReceiver:
				return this.CreateBoundConditionalReceiverOperation((BoundConditionalReceiver)boundNode);
			case BoundKind.Call:
				return this.CreateBoundCallOperation((BoundCall)boundNode);
			case BoundKind.EventAssignmentOperator:
				return this.CreateBoundEventAssignmentOperatorOperation((BoundEventAssignmentOperator)boundNode);
			case BoundKind.ObjectCreationExpression:
				return this.CreateBoundObjectCreationExpressionOperation((BoundObjectCreationExpression)boundNode);
			case BoundKind.TupleLiteral:
			case BoundKind.ConvertedTupleLiteral:
				return this.CreateBoundTupleOperation((BoundTupleExpression)boundNode, true);
			case BoundKind.DynamicObjectCreationExpression:
				return this.CreateBoundDynamicObjectCreationExpressionOperation((BoundDynamicObjectCreationExpression)boundNode);
			case BoundKind.NoPiaObjectCreationExpression:
				return this.CreateNoPiaObjectCreationExpressionOperation((BoundNoPiaObjectCreationExpression)boundNode);
			case BoundKind.ObjectInitializerExpression:
				return this.CreateBoundObjectInitializerExpressionOperation((BoundObjectInitializerExpression)boundNode);
			case BoundKind.ObjectInitializerMember:
				return this.CreateBoundObjectInitializerMemberOperation((BoundObjectInitializerMember)boundNode, false);
			case BoundKind.DynamicObjectInitializerMember:
				return this.CreateBoundDynamicObjectInitializerMemberOperation((BoundDynamicObjectInitializerMember)boundNode);
			case BoundKind.CollectionInitializerExpression:
				return this.CreateBoundCollectionInitializerExpressionOperation((BoundCollectionInitializerExpression)boundNode);
			case BoundKind.CollectionElementInitializer:
				return this.CreateBoundCollectionElementInitializerOperation((BoundCollectionElementInitializer)boundNode);
			case BoundKind.DynamicCollectionElementInitializer:
				return this.CreateBoundDynamicCollectionElementInitializerOperation((BoundDynamicCollectionElementInitializer)boundNode);
			case BoundKind.ImplicitReceiver:
				return this.CreateBoundImplicitReceiverOperation((BoundImplicitReceiver)boundNode);
			case BoundKind.AnonymousObjectCreationExpression:
				return this.CreateBoundAnonymousObjectCreationExpressionOperation((BoundAnonymousObjectCreationExpression)boundNode);
			case BoundKind.AnonymousPropertyDeclaration:
				throw ExceptionUtilities.Unreachable;
			case BoundKind.NewT:
				return this.CreateBoundNewTOperation((BoundNewT)boundNode);
			case BoundKind.DelegateCreationExpression:
				return this.CreateBoundDelegateCreationExpressionOperation((BoundDelegateCreationExpression)boundNode);
			case BoundKind.ArrayCreation:
				return this.CreateBoundArrayCreationOperation((BoundArrayCreation)boundNode);
			case BoundKind.ArrayInitialization:
				return this.CreateBoundArrayInitializationOperation((BoundArrayInitialization)boundNode);
			case BoundKind.FieldAccess:
				return this.CreateBoundFieldAccessOperation((BoundFieldAccess)boundNode, true);
			case BoundKind.PropertyAccess:
				return this.CreateBoundPropertyAccessOperation((BoundPropertyAccess)boundNode);
			case BoundKind.EventAccess:
				return this.CreateBoundEventAccessOperation((BoundEventAccess)boundNode);
			case BoundKind.IndexerAccess:
				return this.CreateBoundIndexerAccessOperation((BoundIndexerAccess)boundNode);
			case BoundKind.DynamicIndexerAccess:
				return this.CreateBoundDynamicIndexerAccessExpressionOperation((BoundDynamicIndexerAccess)boundNode);
			case BoundKind.Lambda:
				return this.CreateBoundLambdaOperation((BoundLambda)boundNode);
			case BoundKind.UnboundLambda:
				return this.CreateUnboundLambdaOperation((UnboundLambda)boundNode);
			case BoundKind.QueryClause:
				return this.CreateBoundQueryClauseOperation((BoundQueryClause)boundNode);
			case BoundKind.NameOfOperator:
				return this.CreateBoundNameOfOperatorOperation((BoundNameOfOperator)boundNode);
			case BoundKind.UnconvertedInterpolatedString:
				throw ExceptionUtilities.Unreachable;
			case BoundKind.InterpolatedString:
				return this.CreateBoundInterpolatedStringExpressionOperation((BoundInterpolatedString)boundNode);
			case BoundKind.StringInsert:
				return this.CreateBoundInterpolationOperation((BoundStringInsert)boundNode);
			case BoundKind.IsPatternExpression:
				return this.CreateBoundIsPatternExpressionOperation((BoundIsPatternExpression)boundNode);
			case BoundKind.ConstantPattern:
				return this.CreateBoundConstantPatternOperation((BoundConstantPattern)boundNode);
			case BoundKind.DiscardPattern:
				return this.CreateBoundDiscardPatternOperation((BoundDiscardPattern)boundNode);
			case BoundKind.DeclarationPattern:
				return this.CreateBoundDeclarationPatternOperation((BoundDeclarationPattern)boundNode);
			case BoundKind.RecursivePattern:
				return this.CreateBoundRecursivePatternOperation((BoundRecursivePattern)boundNode);
			case BoundKind.ITuplePattern:
				return this.CreateBoundRecursivePatternOperation((BoundITuplePattern)boundNode);
			case BoundKind.TypePattern:
				return this.CreateBoundTypePatternOperation((BoundTypePattern)boundNode);
			case BoundKind.BinaryPattern:
				return this.CreateBoundBinaryPatternOperation((BoundBinaryPattern)boundNode);
			case BoundKind.NegatedPattern:
				return this.CreateBoundNegatedPatternOperation((BoundNegatedPattern)boundNode);
			case BoundKind.RelationalPattern:
				return this.CreateBoundRelationalPatternOperation((BoundRelationalPattern)boundNode);
			case BoundKind.DiscardExpression:
				return this.CreateBoundDiscardExpressionOperation((BoundDiscardExpression)boundNode);
			case BoundKind.ThrowExpression:
				return this.CreateBoundThrowExpressionOperation((BoundThrowExpression)boundNode);
			case BoundKind.NonConstructorMethodBody:
				return this.CreateMethodBodyOperation((BoundNonConstructorMethodBody)boundNode);
			case BoundKind.ConstructorMethodBody:
				return this.CreateConstructorBodyOperation((BoundConstructorMethodBody)boundNode);
			case BoundKind.WithExpression:
				return this.CreateBoundWithExpressionOperation((BoundWithExpression)boundNode);
			}
			throw ExceptionUtilities.UnexpectedValue(boundNode.Kind);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002D88 File Offset: 0x00000F88
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<TOperation> CreateFromArray<[global::System.Runtime.CompilerServices.Nullable(0)] TBoundNode, TOperation>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<TBoundNode> boundNodes) where TBoundNode : BoundNode where TOperation : class, IOperation
		{
			if (boundNodes.IsDefault)
			{
				return ImmutableArray<TOperation>.Empty;
			}
			ArrayBuilder<TOperation> instance = ArrayBuilder<TOperation>.GetInstance(boundNodes.Length);
			foreach (TBoundNode tboundNode in boundNodes)
			{
				instance.AddIfNotNull((TOperation)((object)this.Create(tboundNode)));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002DE9 File Offset: 0x00000FE9
		private IMethodBodyOperation CreateMethodBodyOperation(BoundNonConstructorMethodBody boundNode)
		{
			return new MethodBodyOperation((IBlockOperation)this.Create(boundNode.BlockBody), (IBlockOperation)this.Create(boundNode.ExpressionBody), this._semanticModel, boundNode.Syntax, boundNode.WasCompilerGenerated);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002E24 File Offset: 0x00001024
		private IConstructorBodyOperation CreateConstructorBodyOperation(BoundConstructorMethodBody boundNode)
		{
			return new ConstructorBodyOperation(boundNode.Locals.GetPublicSymbols(), this.Create(boundNode.Initializer), (IBlockOperation)this.Create(boundNode.BlockBody), (IBlockOperation)this.Create(boundNode.ExpressionBody), this._semanticModel, boundNode.Syntax, boundNode.WasCompilerGenerated);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002E84 File Offset: 0x00001084
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<IOperation> GetIOperationChildren(IBoundNodeWithIOperationChildren boundNodeWithChildren)
		{
			ImmutableArray<BoundNode> children = boundNodeWithChildren.Children;
			if (children.IsDefaultOrEmpty)
			{
				return ImmutableArray<IOperation>.Empty;
			}
			ArrayBuilder<IOperation> instance = ArrayBuilder<IOperation>.GetInstance(children.Length);
			foreach (BoundNode boundNode in children)
			{
				if (boundNode != null)
				{
					IOperation operation = this.Create(boundNode);
					instance.Add(operation);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002EEC File Offset: 0x000010EC
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<IVariableDeclaratorOperation> CreateVariableDeclarator(BoundNode declaration, SyntaxNode declarationSyntax)
		{
			BoundKind kind = declaration.Kind;
			if (kind == BoundKind.LocalDeclaration)
			{
				BoundLocalDeclaration boundLocalDeclaration = (BoundLocalDeclaration)declaration;
				VariableDeclarationSyntax variableDeclarationSyntax = declarationSyntax as VariableDeclarationSyntax;
				return ImmutableArray.Create<IVariableDeclaratorOperation>(this.CreateVariableDeclaratorInternal(boundLocalDeclaration, ((variableDeclarationSyntax != null) ? variableDeclarationSyntax.Variables[0] : null) ?? declarationSyntax));
			}
			if (kind - BoundKind.MultipleLocalDeclarations > 1)
			{
				throw ExceptionUtilities.UnexpectedValue(declaration.Kind);
			}
			BoundMultipleLocalDeclarationsBase boundMultipleLocalDeclarationsBase = (BoundMultipleLocalDeclarationsBase)declaration;
			ArrayBuilder<IVariableDeclaratorOperation> instance = ArrayBuilder<IVariableDeclaratorOperation>.GetInstance(boundMultipleLocalDeclarationsBase.LocalDeclarations.Length);
			foreach (BoundLocalDeclaration boundLocalDeclaration2 in boundMultipleLocalDeclarationsBase.LocalDeclarations)
			{
				instance.Add(this.CreateVariableDeclaratorInternal(boundLocalDeclaration2, boundLocalDeclaration2.Syntax));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002FAC File Offset: 0x000011AC
		private IPlaceholderOperation CreateBoundDeconstructValuePlaceholderOperation(BoundDeconstructValuePlaceholder boundDeconstructValuePlaceholder)
		{
			SyntaxNode syntax = boundDeconstructValuePlaceholder.Syntax;
			ITypeSymbol publicTypeSymbol = boundDeconstructValuePlaceholder.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundDeconstructValuePlaceholder.WasCompilerGenerated;
			return new PlaceholderOperation(PlaceholderKind.Unspecified, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002FE0 File Offset: 0x000011E0
		private IDeconstructionAssignmentOperation CreateBoundDeconstructionAssignmentOperator(BoundDeconstructionAssignmentOperator boundDeconstructionAssignmentOperator)
		{
			IOperation operation = this.Create(boundDeconstructionAssignmentOperator.Left);
			IOperation operation2 = this.Create(boundDeconstructionAssignmentOperator.Right.Operand);
			SyntaxNode syntax = boundDeconstructionAssignmentOperator.Syntax;
			ITypeSymbol publicTypeSymbol = boundDeconstructionAssignmentOperator.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundDeconstructionAssignmentOperator.WasCompilerGenerated;
			return new DeconstructionAssignmentOperation(operation, operation2, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003030 File Offset: 0x00001230
		private IOperation CreateBoundCallOperation(BoundCall boundCall)
		{
			MethodSymbol method = boundCall.Method;
			SyntaxNode syntax = boundCall.Syntax;
			ITypeSymbol publicTypeSymbol = boundCall.GetPublicTypeSymbol();
			ConstantValue constantValue = boundCall.ConstantValue;
			bool wasCompilerGenerated = boundCall.WasCompilerGenerated;
			if (!boundCall.OriginalMethodsOpt.IsDefault || this.IsMethodInvalid(boundCall.ResultKind, method))
			{
				return new InvalidOperation(this.CreateFromArray<BoundNode, IOperation>(((IBoundInvalidNode)boundCall).InvalidNodeChildren), this._semanticModel, syntax, publicTypeSymbol, constantValue, wasCompilerGenerated);
			}
			bool flag = this.IsCallVirtual(method, boundCall.ReceiverOpt);
			IOperation operation = this.CreateReceiverOperation(boundCall.ReceiverOpt, method);
			ImmutableArray<IArgumentOperation> immutableArray = this.DeriveArguments(boundCall);
			return new InvocationOperation(method.GetPublicSymbol(), operation, flag, immutableArray, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000030E0 File Offset: 0x000012E0
		private IOperation CreateBoundFunctionPointerInvocationOperation(BoundFunctionPointerInvocation boundFunctionPointerInvocation)
		{
			ITypeSymbol publicTypeSymbol = boundFunctionPointerInvocation.GetPublicTypeSymbol();
			SyntaxNode syntax = boundFunctionPointerInvocation.Syntax;
			bool wasCompilerGenerated = boundFunctionPointerInvocation.WasCompilerGenerated;
			if (boundFunctionPointerInvocation.ResultKind != LookupResultKind.Viable)
			{
				return new InvalidOperation(this.CreateFromArray<BoundNode, IOperation>(((IBoundInvalidNode)boundFunctionPointerInvocation).InvalidNodeChildren), this._semanticModel, syntax, publicTypeSymbol, null, wasCompilerGenerated);
			}
			return new NoneOperation(this.GetIOperationChildren(boundFunctionPointerInvocation), this._semanticModel, syntax, publicTypeSymbol, null, wasCompilerGenerated);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000313E File Offset: 0x0000133E
		private IOperation CreateBoundUnconvertedAddressOfOperatorOperation(BoundUnconvertedAddressOfOperator boundUnconvertedAddressOf)
		{
			return new AddressOfOperation(this.Create(boundUnconvertedAddressOf.Operand), this._semanticModel, boundUnconvertedAddressOf.Syntax, boundUnconvertedAddressOf.GetPublicTypeSymbol(), boundUnconvertedAddressOf.WasCompilerGenerated);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000316C File Offset: 0x0000136C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<IOperation> CreateIgnoredDimensions(BoundNode declaration, SyntaxNode declarationSyntax)
		{
			BoundKind kind = declaration.Kind;
			if (kind == BoundKind.LocalDeclaration)
			{
				BoundTypeExpression declaredTypeOpt = ((BoundLocalDeclaration)declaration).DeclaredTypeOpt;
				return this.CreateFromArray<BoundExpression, IOperation>(declaredTypeOpt.BoundDimensionsOpt);
			}
			if (kind - BoundKind.MultipleLocalDeclarations > 1)
			{
				throw ExceptionUtilities.UnexpectedValue(declaration.Kind);
			}
			ImmutableArray<BoundLocalDeclaration> localDeclarations = ((BoundMultipleLocalDeclarationsBase)declaration).LocalDeclarations;
			ImmutableArray<BoundExpression> immutableArray;
			if (localDeclarations.Length > 0)
			{
				immutableArray = localDeclarations[0].DeclaredTypeOpt.BoundDimensionsOpt;
			}
			else
			{
				immutableArray = ImmutableArray<BoundExpression>.Empty;
			}
			return this.CreateFromArray<BoundExpression, IOperation>(immutableArray);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000031F0 File Offset: 0x000013F0
		internal IOperation CreateBoundLocalOperation(BoundLocal boundLocal, bool createDeclaration = true)
		{
			ILocalSymbol publicSymbol = boundLocal.LocalSymbol.GetPublicSymbol();
			bool flag = boundLocal.DeclarationKind > BoundLocalDeclarationKind.None;
			SyntaxNode syntaxNode = boundLocal.Syntax;
			ITypeSymbol publicTypeSymbol = boundLocal.GetPublicTypeSymbol();
			ConstantValue constantValue = boundLocal.ConstantValue;
			bool wasCompilerGenerated = boundLocal.WasCompilerGenerated;
			if (flag)
			{
				DeclarationExpressionSyntax declarationExpressionSyntax = syntaxNode as DeclarationExpressionSyntax;
				if (declarationExpressionSyntax != null)
				{
					syntaxNode = declarationExpressionSyntax.Designation;
					if (createDeclaration)
					{
						return new DeclarationExpressionOperation(this.CreateBoundLocalOperation(boundLocal, false), this._semanticModel, declarationExpressionSyntax, publicTypeSymbol, false);
					}
				}
			}
			return new LocalReferenceOperation(publicSymbol, flag, this._semanticModel, syntaxNode, publicTypeSymbol, constantValue, wasCompilerGenerated);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003278 File Offset: 0x00001478
		internal IOperation CreateBoundFieldAccessOperation(BoundFieldAccess boundFieldAccess, bool createDeclaration = true)
		{
			IFieldSymbol publicSymbol = boundFieldAccess.FieldSymbol.GetPublicSymbol();
			bool isDeclaration = boundFieldAccess.IsDeclaration;
			SyntaxNode syntaxNode = boundFieldAccess.Syntax;
			ITypeSymbol publicTypeSymbol = boundFieldAccess.GetPublicTypeSymbol();
			ConstantValue constantValue = boundFieldAccess.ConstantValue;
			bool wasCompilerGenerated = boundFieldAccess.WasCompilerGenerated;
			if (isDeclaration)
			{
				DeclarationExpressionSyntax declarationExpressionSyntax = syntaxNode as DeclarationExpressionSyntax;
				if (declarationExpressionSyntax != null)
				{
					syntaxNode = declarationExpressionSyntax.Designation;
					if (createDeclaration)
					{
						return new DeclarationExpressionOperation(this.CreateBoundFieldAccessOperation(boundFieldAccess, false), this._semanticModel, declarationExpressionSyntax, publicTypeSymbol, false);
					}
				}
			}
			IOperation operation = this.CreateReceiverOperation(boundFieldAccess.ReceiverOpt, boundFieldAccess.FieldSymbol);
			return new FieldReferenceOperation(publicSymbol, isDeclaration, operation, this._semanticModel, syntaxNode, publicTypeSymbol, constantValue, wasCompilerGenerated);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003314 File Offset: 0x00001514
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal IOperation CreateBoundPropertyReferenceInstance(BoundNode boundNode)
		{
			BoundPropertyAccess boundPropertyAccess = boundNode as BoundPropertyAccess;
			if (boundPropertyAccess != null)
			{
				return this.CreateReceiverOperation(boundPropertyAccess.ReceiverOpt, boundPropertyAccess.PropertySymbol);
			}
			BoundObjectInitializerMember boundObjectInitializerMember = boundNode as BoundObjectInitializerMember;
			if (boundObjectInitializerMember == null)
			{
				BoundIndexerAccess boundIndexerAccess = boundNode as BoundIndexerAccess;
				if (boundIndexerAccess == null)
				{
					throw ExceptionUtilities.UnexpectedValue(boundNode.Kind);
				}
				return this.CreateReceiverOperation(boundIndexerAccess.ReceiverOpt, boundIndexerAccess.ExpressionSymbol);
			}
			else
			{
				Symbol memberSymbol = boundObjectInitializerMember.MemberSymbol;
				if (memberSymbol == null || !memberSymbol.IsStatic)
				{
					return this.CreateImplicitReceiver(boundObjectInitializerMember.Syntax, boundObjectInitializerMember.ReceiverType);
				}
				return null;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000033A0 File Offset: 0x000015A0
		private IPropertyReferenceOperation CreateBoundPropertyAccessOperation(BoundPropertyAccess boundPropertyAccess)
		{
			IOperation operation = this.CreateReceiverOperation(boundPropertyAccess.ReceiverOpt, boundPropertyAccess.PropertySymbol);
			ImmutableArray<IArgumentOperation> empty = ImmutableArray<IArgumentOperation>.Empty;
			IPropertySymbol publicSymbol = boundPropertyAccess.PropertySymbol.GetPublicSymbol();
			SyntaxNode syntax = boundPropertyAccess.Syntax;
			ITypeSymbol publicTypeSymbol = boundPropertyAccess.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundPropertyAccess.WasCompilerGenerated;
			return new PropertyReferenceOperation(publicSymbol, empty, operation, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000033F8 File Offset: 0x000015F8
		private IOperation CreateBoundIndexerAccessOperation(BoundIndexerAccess boundIndexerAccess)
		{
			PropertySymbol indexer = boundIndexerAccess.Indexer;
			SyntaxNode syntax = boundIndexerAccess.Syntax;
			ITypeSymbol publicTypeSymbol = boundIndexerAccess.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundIndexerAccess.WasCompilerGenerated;
			if (!boundIndexerAccess.OriginalIndexersOpt.IsDefault || boundIndexerAccess.ResultKind == LookupResultKind.OverloadResolutionFailure)
			{
				return new InvalidOperation(this.CreateFromArray<BoundNode, IOperation>(((IBoundInvalidNode)boundIndexerAccess).InvalidNodeChildren), this._semanticModel, syntax, publicTypeSymbol, null, wasCompilerGenerated);
			}
			ImmutableArray<IArgumentOperation> immutableArray = this.DeriveArguments(boundIndexerAccess, false);
			IOperation operation = this.CreateReceiverOperation(boundIndexerAccess.ReceiverOpt, boundIndexerAccess.ExpressionSymbol);
			return new PropertyReferenceOperation(indexer.GetPublicSymbol(), immutableArray, operation, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003490 File Offset: 0x00001690
		private IEventReferenceOperation CreateBoundEventAccessOperation(BoundEventAccess boundEventAccess)
		{
			IEventSymbol publicSymbol = boundEventAccess.EventSymbol.GetPublicSymbol();
			IOperation operation = this.CreateReceiverOperation(boundEventAccess.ReceiverOpt, boundEventAccess.EventSymbol);
			SyntaxNode syntax = boundEventAccess.Syntax;
			ITypeSymbol publicTypeSymbol = boundEventAccess.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundEventAccess.WasCompilerGenerated;
			return new EventReferenceOperation(publicSymbol, operation, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000034E0 File Offset: 0x000016E0
		private IEventAssignmentOperation CreateBoundEventAssignmentOperatorOperation(BoundEventAssignmentOperator boundEventAssignmentOperator)
		{
			IOperation operation = this.CreateBoundEventAccessOperation(boundEventAssignmentOperator);
			IOperation operation2 = this.Create(boundEventAssignmentOperator.Argument);
			SyntaxNode syntax = boundEventAssignmentOperator.Syntax;
			bool isAddition = boundEventAssignmentOperator.IsAddition;
			ITypeSymbol publicTypeSymbol = boundEventAssignmentOperator.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundEventAssignmentOperator.WasCompilerGenerated;
			return new EventAssignmentOperation(operation, operation2, isAddition, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003530 File Offset: 0x00001730
		private IParameterReferenceOperation CreateBoundParameterOperation(BoundParameter boundParameter)
		{
			IParameterSymbol publicSymbol = boundParameter.ParameterSymbol.GetPublicSymbol();
			SyntaxNode syntax = boundParameter.Syntax;
			ITypeSymbol publicTypeSymbol = boundParameter.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundParameter.WasCompilerGenerated;
			return new ParameterReferenceOperation(publicSymbol, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000356C File Offset: 0x0000176C
		internal ILiteralOperation CreateBoundLiteralOperation(BoundLiteral boundLiteral, bool @implicit = false)
		{
			SyntaxNode syntax = boundLiteral.Syntax;
			ITypeSymbol publicTypeSymbol = boundLiteral.GetPublicTypeSymbol();
			ConstantValue constantValue = boundLiteral.ConstantValue;
			bool flag = boundLiteral.WasCompilerGenerated || @implicit;
			return new LiteralOperation(this._semanticModel, syntax, publicTypeSymbol, constantValue, flag);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000035A8 File Offset: 0x000017A8
		private IAnonymousObjectCreationOperation CreateBoundAnonymousObjectCreationExpressionOperation(BoundAnonymousObjectCreationExpression boundAnonymousObjectCreationExpression)
		{
			SyntaxNode syntax = boundAnonymousObjectCreationExpression.Syntax;
			ITypeSymbol publicTypeSymbol = boundAnonymousObjectCreationExpression.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundAnonymousObjectCreationExpression.WasCompilerGenerated;
			return new AnonymousObjectCreationOperation(this.GetAnonymousObjectCreationInitializers(boundAnonymousObjectCreationExpression.Arguments, boundAnonymousObjectCreationExpression.Declarations, syntax, publicTypeSymbol, wasCompilerGenerated), this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000035F0 File Offset: 0x000017F0
		private IOperation CreateBoundObjectCreationExpressionOperation(BoundObjectCreationExpression boundObjectCreationExpression)
		{
			MethodSymbol constructor = boundObjectCreationExpression.Constructor;
			SyntaxNode syntax = boundObjectCreationExpression.Syntax;
			ITypeSymbol publicTypeSymbol = boundObjectCreationExpression.GetPublicTypeSymbol();
			ConstantValue constantValue = boundObjectCreationExpression.ConstantValue;
			bool wasCompilerGenerated = boundObjectCreationExpression.WasCompilerGenerated;
			if (boundObjectCreationExpression.ResultKind == LookupResultKind.OverloadResolutionFailure || constructor == null || constructor.OriginalDefinition is ErrorMethodSymbol)
			{
				return new InvalidOperation(this.CreateFromArray<BoundNode, IOperation>(((IBoundInvalidNode)boundObjectCreationExpression).InvalidNodeChildren), this._semanticModel, syntax, publicTypeSymbol, constantValue, wasCompilerGenerated);
			}
			if (boundObjectCreationExpression.Type.IsAnonymousType)
			{
				return new AnonymousObjectCreationOperation(this.GetAnonymousObjectCreationInitializers(boundObjectCreationExpression.Arguments, ImmutableArray<BoundAnonymousPropertyDeclaration>.Empty, syntax, publicTypeSymbol, wasCompilerGenerated), this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
			}
			ImmutableArray<IArgumentOperation> immutableArray = this.DeriveArguments(boundObjectCreationExpression);
			IObjectOrCollectionInitializerOperation objectOrCollectionInitializerOperation = (IObjectOrCollectionInitializerOperation)this.Create(boundObjectCreationExpression.InitializerExpressionOpt);
			return new ObjectCreationOperation(constructor.GetPublicSymbol(), objectOrCollectionInitializerOperation, immutableArray, this._semanticModel, syntax, publicTypeSymbol, constantValue, wasCompilerGenerated);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000036C8 File Offset: 0x000018C8
		private IOperation CreateBoundWithExpressionOperation(BoundWithExpression boundWithExpression)
		{
			IOperation operation = this.Create(boundWithExpression.Receiver);
			IObjectOrCollectionInitializerOperation objectOrCollectionInitializerOperation = (IObjectOrCollectionInitializerOperation)this.Create(boundWithExpression.InitializerExpression);
			MethodSymbol cloneMethod = boundWithExpression.CloneMethod;
			SyntaxNode syntax = boundWithExpression.Syntax;
			ITypeSymbol publicTypeSymbol = boundWithExpression.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundWithExpression.WasCompilerGenerated;
			return new WithOperation(operation, cloneMethod.GetPublicSymbol(), objectOrCollectionInitializerOperation, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003728 File Offset: 0x00001928
		private IDynamicObjectCreationOperation CreateBoundDynamicObjectCreationExpressionOperation(BoundDynamicObjectCreationExpression boundDynamicObjectCreationExpression)
		{
			IObjectOrCollectionInitializerOperation objectOrCollectionInitializerOperation = (IObjectOrCollectionInitializerOperation)this.Create(boundDynamicObjectCreationExpression.InitializerExpressionOpt);
			ImmutableArray<IOperation> immutableArray = this.CreateFromArray<BoundExpression, IOperation>(boundDynamicObjectCreationExpression.Arguments);
			ImmutableArray<string> immutableArray2 = boundDynamicObjectCreationExpression.ArgumentNamesOpt.NullToEmpty<string>();
			ImmutableArray<RefKind> immutableArray3 = boundDynamicObjectCreationExpression.ArgumentRefKindsOpt.NullToEmpty<RefKind>();
			SyntaxNode syntax = boundDynamicObjectCreationExpression.Syntax;
			ITypeSymbol publicTypeSymbol = boundDynamicObjectCreationExpression.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundDynamicObjectCreationExpression.WasCompilerGenerated;
			return new DynamicObjectCreationOperation(objectOrCollectionInitializerOperation, immutableArray, immutableArray2, immutableArray3, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003798 File Offset: 0x00001998
		internal IOperation CreateBoundDynamicInvocationExpressionReceiver(BoundNode receiver)
		{
			BoundObjectOrCollectionValuePlaceholder boundObjectOrCollectionValuePlaceholder = receiver as BoundObjectOrCollectionValuePlaceholder;
			if (boundObjectOrCollectionValuePlaceholder != null)
			{
				return this.CreateBoundDynamicMemberAccessOperation(boundObjectOrCollectionValuePlaceholder, ImmutableArray<TypeSymbol>.Empty, "Add", boundObjectOrCollectionValuePlaceholder.Syntax, null, true);
			}
			BoundMethodGroup boundMethodGroup = receiver as BoundMethodGroup;
			if (boundMethodGroup == null)
			{
				return this.Create(receiver);
			}
			return this.CreateBoundDynamicMemberAccessOperation(boundMethodGroup.ReceiverOpt, TypeMap.AsTypeSymbols(boundMethodGroup.TypeArgumentsOpt), boundMethodGroup.Name, boundMethodGroup.Syntax, boundMethodGroup.GetPublicTypeSymbol(), boundMethodGroup.WasCompilerGenerated);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000380C File Offset: 0x00001A0C
		private IDynamicInvocationOperation CreateBoundDynamicInvocationExpressionOperation(BoundDynamicInvocation boundDynamicInvocation)
		{
			IOperation operation = this.CreateBoundDynamicInvocationExpressionReceiver(boundDynamicInvocation.Expression);
			ImmutableArray<IOperation> immutableArray = this.CreateFromArray<BoundExpression, IOperation>(boundDynamicInvocation.Arguments);
			ImmutableArray<string> immutableArray2 = boundDynamicInvocation.ArgumentNamesOpt.NullToEmpty<string>();
			ImmutableArray<RefKind> immutableArray3 = boundDynamicInvocation.ArgumentRefKindsOpt.NullToEmpty<RefKind>();
			SyntaxNode syntax = boundDynamicInvocation.Syntax;
			ITypeSymbol publicTypeSymbol = boundDynamicInvocation.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundDynamicInvocation.WasCompilerGenerated;
			return new DynamicInvocationOperation(operation, immutableArray, immutableArray2, immutableArray3, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003874 File Offset: 0x00001A74
		internal IOperation CreateBoundDynamicIndexerAccessExpressionReceiver(BoundExpression indexer)
		{
			BoundDynamicIndexerAccess boundDynamicIndexerAccess = indexer as BoundDynamicIndexerAccess;
			if (boundDynamicIndexerAccess != null)
			{
				return this.Create(boundDynamicIndexerAccess.Receiver);
			}
			BoundObjectInitializerMember boundObjectInitializerMember = indexer as BoundObjectInitializerMember;
			if (boundObjectInitializerMember == null)
			{
				throw ExceptionUtilities.UnexpectedValue(indexer.Kind);
			}
			return this.CreateImplicitReceiver(boundObjectInitializerMember.Syntax, boundObjectInitializerMember.ReceiverType);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000038C8 File Offset: 0x00001AC8
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<IOperation> CreateBoundDynamicIndexerAccessArguments(BoundExpression indexer)
		{
			BoundDynamicIndexerAccess boundDynamicIndexerAccess = indexer as BoundDynamicIndexerAccess;
			if (boundDynamicIndexerAccess != null)
			{
				return this.CreateFromArray<BoundExpression, IOperation>(boundDynamicIndexerAccess.Arguments);
			}
			BoundObjectInitializerMember boundObjectInitializerMember = indexer as BoundObjectInitializerMember;
			if (boundObjectInitializerMember == null)
			{
				throw ExceptionUtilities.UnexpectedValue(indexer.Kind);
			}
			return this.CreateFromArray<BoundExpression, IOperation>(boundObjectInitializerMember.Arguments);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003918 File Offset: 0x00001B18
		private IDynamicIndexerAccessOperation CreateBoundDynamicIndexerAccessExpressionOperation(BoundDynamicIndexerAccess boundDynamicIndexerAccess)
		{
			IOperation operation = this.CreateBoundDynamicIndexerAccessExpressionReceiver(boundDynamicIndexerAccess);
			ImmutableArray<IOperation> immutableArray = this.CreateBoundDynamicIndexerAccessArguments(boundDynamicIndexerAccess);
			ImmutableArray<string> immutableArray2 = boundDynamicIndexerAccess.ArgumentNamesOpt.NullToEmpty<string>();
			ImmutableArray<RefKind> immutableArray3 = boundDynamicIndexerAccess.ArgumentRefKindsOpt.NullToEmpty<RefKind>();
			SyntaxNode syntax = boundDynamicIndexerAccess.Syntax;
			ITypeSymbol publicTypeSymbol = boundDynamicIndexerAccess.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundDynamicIndexerAccess.WasCompilerGenerated;
			return new DynamicIndexerAccessOperation(operation, immutableArray, immutableArray2, immutableArray3, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003978 File Offset: 0x00001B78
		private IObjectOrCollectionInitializerOperation CreateBoundObjectInitializerExpressionOperation(BoundObjectInitializerExpression boundObjectInitializerExpression)
		{
			ImmutableArray<IOperation> immutableArray = this.CreateFromArray<BoundExpression, IOperation>(BoundObjectCreationExpression.GetChildInitializers(boundObjectInitializerExpression));
			SyntaxNode syntax = boundObjectInitializerExpression.Syntax;
			ITypeSymbol publicTypeSymbol = boundObjectInitializerExpression.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundObjectInitializerExpression.WasCompilerGenerated;
			return new ObjectOrCollectionInitializerOperation(immutableArray, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000039B4 File Offset: 0x00001BB4
		private IObjectOrCollectionInitializerOperation CreateBoundCollectionInitializerExpressionOperation(BoundCollectionInitializerExpression boundCollectionInitializerExpression)
		{
			ImmutableArray<IOperation> immutableArray = this.CreateFromArray<BoundExpression, IOperation>(BoundObjectCreationExpression.GetChildInitializers(boundCollectionInitializerExpression));
			SyntaxNode syntax = boundCollectionInitializerExpression.Syntax;
			ITypeSymbol publicTypeSymbol = boundCollectionInitializerExpression.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundCollectionInitializerExpression.WasCompilerGenerated;
			return new ObjectOrCollectionInitializerOperation(immutableArray, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000039F0 File Offset: 0x00001BF0
		private IOperation CreateBoundObjectInitializerMemberOperation(BoundObjectInitializerMember boundObjectInitializerMember, bool isObjectOrCollectionInitializer = false)
		{
			CSharpOperationFactory.<>c__DisplayClass34_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.boundObjectInitializerMember = boundObjectInitializerMember;
			CS$<>8__locals1.memberSymbol = CS$<>8__locals1.boundObjectInitializerMember.MemberSymbol;
			SyntaxNode syntax = CS$<>8__locals1.boundObjectInitializerMember.Syntax;
			ITypeSymbol publicTypeSymbol = CS$<>8__locals1.boundObjectInitializerMember.GetPublicTypeSymbol();
			bool wasCompilerGenerated = CS$<>8__locals1.boundObjectInitializerMember.WasCompilerGenerated;
			if (CS$<>8__locals1.memberSymbol == null)
			{
				IOperation operation = this.CreateBoundDynamicIndexerAccessExpressionReceiver(CS$<>8__locals1.boundObjectInitializerMember);
				ImmutableArray<IOperation> immutableArray = this.CreateBoundDynamicIndexerAccessArguments(CS$<>8__locals1.boundObjectInitializerMember);
				ImmutableArray<string> immutableArray2 = CS$<>8__locals1.boundObjectInitializerMember.ArgumentNamesOpt.NullToEmpty<string>();
				ImmutableArray<RefKind> immutableArray3 = CS$<>8__locals1.boundObjectInitializerMember.ArgumentRefKindsOpt.NullToEmpty<RefKind>();
				return new DynamicIndexerAccessOperation(operation, immutableArray, immutableArray2, immutableArray3, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
			}
			SymbolKind kind = CS$<>8__locals1.memberSymbol.Kind;
			if (kind == SymbolKind.Event)
			{
				return new EventReferenceOperation(((EventSymbol)CS$<>8__locals1.memberSymbol).GetPublicSymbol(), this.<CreateBoundObjectInitializerMemberOperation>g__createReceiver|34_0(ref CS$<>8__locals1), this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
			}
			if (kind == SymbolKind.Field)
			{
				FieldSymbol fieldSymbol = (FieldSymbol)CS$<>8__locals1.memberSymbol;
				bool flag = false;
				return new FieldReferenceOperation(fieldSymbol.GetPublicSymbol(), flag, this.<CreateBoundObjectInitializerMemberOperation>g__createReceiver|34_0(ref CS$<>8__locals1), this._semanticModel, syntax, publicTypeSymbol, null, wasCompilerGenerated);
			}
			if (kind != SymbolKind.Property)
			{
				throw ExceptionUtilities.Unreachable;
			}
			PropertySymbol propertySymbol = (PropertySymbol)CS$<>8__locals1.memberSymbol;
			ImmutableArray<IArgumentOperation> immutableArray4;
			if (!CS$<>8__locals1.boundObjectInitializerMember.Arguments.IsEmpty)
			{
				MethodSymbol methodSymbol = (isObjectOrCollectionInitializer ? propertySymbol.GetOwnOrInheritedGetMethod() : propertySymbol.GetOwnOrInheritedSetMethod());
				if (methodSymbol == null || CS$<>8__locals1.boundObjectInitializerMember.ResultKind == LookupResultKind.OverloadResolutionFailure || methodSymbol.OriginalDefinition is ErrorMethodSymbol)
				{
					return new InvalidOperation(this.CreateFromArray<BoundNode, IOperation>(((IBoundInvalidNode)CS$<>8__locals1.boundObjectInitializerMember).InvalidNodeChildren), this._semanticModel, syntax, publicTypeSymbol, null, wasCompilerGenerated);
				}
				immutableArray4 = this.DeriveArguments(CS$<>8__locals1.boundObjectInitializerMember, isObjectOrCollectionInitializer);
			}
			else
			{
				immutableArray4 = ImmutableArray<IArgumentOperation>.Empty;
			}
			return new PropertyReferenceOperation(propertySymbol.GetPublicSymbol(), immutableArray4, this.<CreateBoundObjectInitializerMemberOperation>g__createReceiver|34_0(ref CS$<>8__locals1), this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003BD4 File Offset: 0x00001DD4
		private IOperation CreateBoundDynamicObjectInitializerMemberOperation(BoundDynamicObjectInitializerMember boundDynamicObjectInitializerMember)
		{
			IOperation operation = this.CreateImplicitReceiver(boundDynamicObjectInitializerMember.Syntax, boundDynamicObjectInitializerMember.ReceiverType);
			string memberName = boundDynamicObjectInitializerMember.MemberName;
			ImmutableArray<ITypeSymbol> empty = ImmutableArray<ITypeSymbol>.Empty;
			ITypeSymbol publicSymbol = boundDynamicObjectInitializerMember.ReceiverType.GetPublicSymbol();
			SyntaxNode syntax = boundDynamicObjectInitializerMember.Syntax;
			ITypeSymbol publicTypeSymbol = boundDynamicObjectInitializerMember.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundDynamicObjectInitializerMember.WasCompilerGenerated;
			return new DynamicMemberReferenceOperation(operation, memberName, empty, publicSymbol, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003C38 File Offset: 0x00001E38
		private IOperation CreateBoundCollectionElementInitializerOperation(BoundCollectionElementInitializer boundCollectionElementInitializer)
		{
			MethodSymbol addMethod = boundCollectionElementInitializer.AddMethod;
			IOperation operation = this.CreateReceiverOperation(boundCollectionElementInitializer.ImplicitReceiverOpt, addMethod);
			ImmutableArray<IArgumentOperation> immutableArray = this.DeriveArguments(boundCollectionElementInitializer);
			SyntaxNode syntax = boundCollectionElementInitializer.Syntax;
			ITypeSymbol publicTypeSymbol = boundCollectionElementInitializer.GetPublicTypeSymbol();
			ConstantValue constantValue = boundCollectionElementInitializer.ConstantValue;
			bool wasCompilerGenerated = boundCollectionElementInitializer.WasCompilerGenerated;
			if (this.IsMethodInvalid(boundCollectionElementInitializer.ResultKind, addMethod))
			{
				return new InvalidOperation(this.CreateFromArray<BoundNode, IOperation>(((IBoundInvalidNode)boundCollectionElementInitializer).InvalidNodeChildren), this._semanticModel, syntax, publicTypeSymbol, constantValue, wasCompilerGenerated);
			}
			bool flag = this.IsCallVirtual(addMethod, boundCollectionElementInitializer.ImplicitReceiverOpt);
			return new InvocationOperation(addMethod.GetPublicSymbol(), operation, flag, immutableArray, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003CD8 File Offset: 0x00001ED8
		private IDynamicMemberReferenceOperation CreateBoundDynamicMemberAccessOperation(BoundDynamicMemberAccess boundDynamicMemberAccess)
		{
			return this.CreateBoundDynamicMemberAccessOperation(boundDynamicMemberAccess.Receiver, TypeMap.AsTypeSymbols(boundDynamicMemberAccess.TypeArgumentsOpt), boundDynamicMemberAccess.Name, boundDynamicMemberAccess.Syntax, boundDynamicMemberAccess.GetPublicTypeSymbol(), boundDynamicMemberAccess.WasCompilerGenerated);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003D0C File Offset: 0x00001F0C
		private IDynamicMemberReferenceOperation CreateBoundDynamicMemberAccessOperation([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiver, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<TypeSymbol> typeArgumentsOpt, string memberName, SyntaxNode syntaxNode, [global::System.Runtime.CompilerServices.Nullable(2)] ITypeSymbol type, bool isImplicit)
		{
			ITypeSymbol typeSymbol = null;
			if (receiver != null && receiver.Kind == BoundKind.TypeExpression)
			{
				typeSymbol = receiver.GetPublicTypeSymbol();
				receiver = null;
			}
			ImmutableArray<ITypeSymbol> immutableArray = ImmutableArray<ITypeSymbol>.Empty;
			if (!typeArgumentsOpt.IsDefault)
			{
				immutableArray = typeArgumentsOpt.GetPublicSymbols();
			}
			return new DynamicMemberReferenceOperation(this.Create(receiver), memberName, immutableArray, typeSymbol, this._semanticModel, syntaxNode, type, isImplicit);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003D64 File Offset: 0x00001F64
		private IDynamicInvocationOperation CreateBoundDynamicCollectionElementInitializerOperation(BoundDynamicCollectionElementInitializer boundCollectionElementInitializer)
		{
			IOperation operation = this.CreateBoundDynamicInvocationExpressionReceiver(boundCollectionElementInitializer.Expression);
			ImmutableArray<IOperation> immutableArray = this.CreateFromArray<BoundExpression, IOperation>(boundCollectionElementInitializer.Arguments);
			SyntaxNode syntax = boundCollectionElementInitializer.Syntax;
			ITypeSymbol publicTypeSymbol = boundCollectionElementInitializer.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundCollectionElementInitializer.WasCompilerGenerated;
			return new DynamicInvocationOperation(operation, immutableArray, ImmutableArray<string>.Empty, ImmutableArray<RefKind>.Empty, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003DB8 File Offset: 0x00001FB8
		private IOperation CreateUnboundLambdaOperation(UnboundLambda unboundLambda)
		{
			BoundLambda boundLambda = unboundLambda.BindForErrorRecovery();
			return this.Create(boundLambda);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003DD4 File Offset: 0x00001FD4
		private IAnonymousFunctionOperation CreateBoundLambdaOperation(BoundLambda boundLambda)
		{
			IMethodSymbol publicSymbol = boundLambda.Symbol.GetPublicSymbol();
			IBlockOperation blockOperation = (IBlockOperation)this.Create(boundLambda.Body);
			SyntaxNode syntax = boundLambda.Syntax;
			bool wasCompilerGenerated = boundLambda.WasCompilerGenerated;
			return new AnonymousFunctionOperation(publicSymbol, blockOperation, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003E1C File Offset: 0x0000201C
		private ILocalFunctionOperation CreateBoundLocalFunctionStatementOperation(BoundLocalFunctionStatement boundLocalFunctionStatement)
		{
			IBlockOperation blockOperation = (IBlockOperation)this.Create(boundLocalFunctionStatement.Body);
			IBlockOperation blockOperation2;
			if (boundLocalFunctionStatement != null && boundLocalFunctionStatement.BlockBody != null)
			{
				BoundBlock expressionBody = boundLocalFunctionStatement.ExpressionBody;
				if (expressionBody != null)
				{
					blockOperation2 = (IBlockOperation)this.Create(expressionBody);
					goto IL_0036;
				}
			}
			blockOperation2 = null;
			IL_0036:
			IBlockOperation blockOperation3 = blockOperation2;
			IMethodSymbol publicSymbol = boundLocalFunctionStatement.Symbol.GetPublicSymbol();
			SyntaxNode syntax = boundLocalFunctionStatement.Syntax;
			bool wasCompilerGenerated = boundLocalFunctionStatement.WasCompilerGenerated;
			return new LocalFunctionOperation(publicSymbol, blockOperation, blockOperation3, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003E8C File Offset: 0x0000208C
		private IOperation CreateBoundConversionOperation(BoundConversion boundConversion)
		{
			bool flag = boundConversion.WasCompilerGenerated || !boundConversion.ExplicitCastInCode;
			BoundExpression operand = boundConversion.Operand;
			if (boundConversion.ConversionKind == ConversionKind.MethodGroup)
			{
				SyntaxNode syntax = boundConversion.Syntax;
				ITypeSymbol publicTypeSymbol = boundConversion.GetPublicTypeSymbol();
				ConstantValue constantValue = boundConversion.ConstantValue;
				if (boundConversion.Type is FunctionPointerTypeSymbol)
				{
					return new AddressOfOperation(this.CreateBoundMethodGroupSingleMethodOperation((BoundMethodGroup)boundConversion.Operand, boundConversion.SymbolOpt, false), this._semanticModel, syntax, publicTypeSymbol, boundConversion.WasCompilerGenerated);
				}
				return new DelegateCreationOperation(this.CreateDelegateTargetOperation(boundConversion), this._semanticModel, syntax, publicTypeSymbol, flag);
			}
			else
			{
				SyntaxNode syntax2 = boundConversion.Syntax;
				if (syntax2.IsMissing)
				{
					return this.Create(operand);
				}
				BoundConversion boundConversion2 = boundConversion;
				Conversion conversion = boundConversion.Conversion;
				if (operand.Syntax == boundConversion.Syntax)
				{
					if (operand.Kind == BoundKind.ConvertedTupleLiteral && TypeSymbol.Equals(operand.Type, boundConversion.Type, TypeCompareKind.ConsiderEverything))
					{
						return this.Create(operand);
					}
					flag = true;
				}
				if (boundConversion.ExplicitCastInCode && conversion.IsIdentity && operand.Kind == BoundKind.Conversion)
				{
					BoundConversion boundConversion3 = (BoundConversion)operand;
					BoundExpression operand2 = boundConversion3.Operand;
					if (boundConversion3.Syntax == operand2.Syntax && boundConversion3.ExplicitCastInCode && operand2.Kind == BoundKind.ConvertedTupleLiteral && !TypeSymbol.Equals(boundConversion3.Type, operand2.Type, TypeCompareKind.ConsiderEverything))
					{
						conversion = boundConversion3.Conversion;
						boundConversion2 = boundConversion3;
					}
				}
				ITypeSymbol publicTypeSymbol2 = boundConversion.GetPublicTypeSymbol();
				ConstantValue constantValue2 = boundConversion.ConstantValue;
				if ((operand.Kind == BoundKind.Lambda || operand.Kind == BoundKind.UnboundLambda || operand.Kind == BoundKind.MethodGroup) && boundConversion.Type.IsDelegateType())
				{
					return new DelegateCreationOperation(this.CreateDelegateTargetOperation(boundConversion2), this._semanticModel, syntax2, publicTypeSymbol2, flag);
				}
				bool flag2 = false;
				bool flag3 = conversion.IsNumeric && boundConversion.Checked;
				return new ConversionOperation(this.Create(boundConversion2.Operand), conversion, flag2, flag3, this._semanticModel, syntax2, publicTypeSymbol2, constantValue2, flag);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004094 File Offset: 0x00002294
		private IConversionOperation CreateBoundAsOperatorOperation(BoundAsOperator boundAsOperator)
		{
			IOperation operation = this.Create(boundAsOperator.Operand);
			SyntaxNode syntax = boundAsOperator.Syntax;
			Conversion conversion = boundAsOperator.Conversion;
			bool flag = true;
			bool flag2 = false;
			ITypeSymbol publicTypeSymbol = boundAsOperator.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundAsOperator.WasCompilerGenerated;
			return new ConversionOperation(operation, conversion, flag, flag2, this._semanticModel, syntax, publicTypeSymbol, null, wasCompilerGenerated);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000040E8 File Offset: 0x000022E8
		private IDelegateCreationOperation CreateBoundDelegateCreationExpressionOperation(BoundDelegateCreationExpression boundDelegateCreationExpression)
		{
			IOperation operation = this.CreateDelegateTargetOperation(boundDelegateCreationExpression);
			SyntaxNode syntax = boundDelegateCreationExpression.Syntax;
			ITypeSymbol publicTypeSymbol = boundDelegateCreationExpression.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundDelegateCreationExpression.WasCompilerGenerated;
			return new DelegateCreationOperation(operation, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004120 File Offset: 0x00002320
		private IMethodReferenceOperation CreateBoundMethodGroupSingleMethodOperation(BoundMethodGroup boundMethodGroup, MethodSymbol methodSymbol, bool suppressVirtualCalls)
		{
			bool flag = (methodSymbol.IsAbstract || methodSymbol.IsOverride || methodSymbol.IsVirtual) && !suppressVirtualCalls;
			IOperation operation = this.CreateReceiverOperation(boundMethodGroup.ReceiverOpt, methodSymbol);
			SyntaxNode syntax = boundMethodGroup.Syntax;
			ITypeSymbol typeSymbol = null;
			bool wasCompilerGenerated = boundMethodGroup.WasCompilerGenerated;
			return new MethodReferenceOperation(methodSymbol.GetPublicSymbol(), flag, operation, this._semanticModel, syntax, typeSymbol, boundMethodGroup.WasCompilerGenerated);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004188 File Offset: 0x00002388
		private IIsTypeOperation CreateBoundIsOperatorOperation(BoundIsOperator boundIsOperator)
		{
			IOperation operation = this.Create(boundIsOperator.Operand);
			ITypeSymbol publicTypeSymbol = boundIsOperator.TargetType.GetPublicTypeSymbol();
			SyntaxNode syntax = boundIsOperator.Syntax;
			ITypeSymbol publicTypeSymbol2 = boundIsOperator.GetPublicTypeSymbol();
			bool flag = false;
			bool wasCompilerGenerated = boundIsOperator.WasCompilerGenerated;
			return new IsTypeOperation(operation, publicTypeSymbol, flag, this._semanticModel, syntax, publicTypeSymbol2, wasCompilerGenerated);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000041D8 File Offset: 0x000023D8
		private ISizeOfOperation CreateBoundSizeOfOperatorOperation(BoundSizeOfOperator boundSizeOfOperator)
		{
			ITypeSymbol publicTypeSymbol = boundSizeOfOperator.SourceType.GetPublicTypeSymbol();
			SyntaxNode syntax = boundSizeOfOperator.Syntax;
			ITypeSymbol publicTypeSymbol2 = boundSizeOfOperator.GetPublicTypeSymbol();
			ConstantValue constantValue = boundSizeOfOperator.ConstantValue;
			bool wasCompilerGenerated = boundSizeOfOperator.WasCompilerGenerated;
			return new SizeOfOperation(publicTypeSymbol, this._semanticModel, syntax, publicTypeSymbol2, constantValue, wasCompilerGenerated);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000421C File Offset: 0x0000241C
		private ITypeOfOperation CreateBoundTypeOfOperatorOperation(BoundTypeOfOperator boundTypeOfOperator)
		{
			ITypeSymbol publicTypeSymbol = boundTypeOfOperator.SourceType.GetPublicTypeSymbol();
			SyntaxNode syntax = boundTypeOfOperator.Syntax;
			ITypeSymbol publicTypeSymbol2 = boundTypeOfOperator.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundTypeOfOperator.WasCompilerGenerated;
			return new TypeOfOperation(publicTypeSymbol, this._semanticModel, syntax, publicTypeSymbol2, wasCompilerGenerated);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004258 File Offset: 0x00002458
		private IArrayCreationOperation CreateBoundArrayCreationOperation(BoundArrayCreation boundArrayCreation)
		{
			ImmutableArray<IOperation> immutableArray = this.CreateFromArray<BoundExpression, IOperation>(boundArrayCreation.Bounds);
			IArrayInitializerOperation arrayInitializerOperation = (IArrayInitializerOperation)this.Create(boundArrayCreation.InitializerOpt);
			SyntaxNode syntax = boundArrayCreation.Syntax;
			ITypeSymbol publicTypeSymbol = boundArrayCreation.GetPublicTypeSymbol();
			bool flag;
			if (!boundArrayCreation.WasCompilerGenerated)
			{
				BoundArrayInitialization initializerOpt = boundArrayCreation.InitializerOpt;
				flag = ((initializerOpt != null) ? initializerOpt.Syntax : null) == syntax && !boundArrayCreation.InitializerOpt.WasCompilerGenerated;
			}
			else
			{
				flag = true;
			}
			bool flag2 = flag;
			return new ArrayCreationOperation(immutableArray, arrayInitializerOperation, this._semanticModel, syntax, publicTypeSymbol, flag2);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000042D4 File Offset: 0x000024D4
		private IArrayInitializerOperation CreateBoundArrayInitializationOperation(BoundArrayInitialization boundArrayInitialization)
		{
			ImmutableArray<IOperation> immutableArray = this.CreateFromArray<BoundExpression, IOperation>(boundArrayInitialization.Initializers);
			SyntaxNode syntax = boundArrayInitialization.Syntax;
			bool wasCompilerGenerated = boundArrayInitialization.WasCompilerGenerated;
			return new ArrayInitializerOperation(immutableArray, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004308 File Offset: 0x00002508
		private IDefaultValueOperation CreateBoundDefaultLiteralOperation(BoundDefaultLiteral boundDefaultLiteral)
		{
			SyntaxNode syntax = boundDefaultLiteral.Syntax;
			ConstantValue constantValue = boundDefaultLiteral.ConstantValue;
			bool wasCompilerGenerated = boundDefaultLiteral.WasCompilerGenerated;
			return new DefaultValueOperation(this._semanticModel, syntax, null, constantValue, wasCompilerGenerated);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000433C File Offset: 0x0000253C
		private IDefaultValueOperation CreateBoundDefaultExpressionOperation(BoundDefaultExpression boundDefaultExpression)
		{
			SyntaxNode syntax = boundDefaultExpression.Syntax;
			ITypeSymbol publicTypeSymbol = boundDefaultExpression.GetPublicTypeSymbol();
			ConstantValue constantValue = boundDefaultExpression.ConstantValue;
			bool wasCompilerGenerated = boundDefaultExpression.WasCompilerGenerated;
			return new DefaultValueOperation(this._semanticModel, syntax, publicTypeSymbol, constantValue, wasCompilerGenerated);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004374 File Offset: 0x00002574
		private IInstanceReferenceOperation CreateBoundBaseReferenceOperation(BoundBaseReference boundBaseReference)
		{
			InstanceReferenceKind instanceReferenceKind = InstanceReferenceKind.ContainingTypeInstance;
			SyntaxNode syntax = boundBaseReference.Syntax;
			ITypeSymbol publicTypeSymbol = boundBaseReference.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundBaseReference.WasCompilerGenerated;
			return new InstanceReferenceOperation(instanceReferenceKind, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000043A8 File Offset: 0x000025A8
		private IInstanceReferenceOperation CreateBoundThisReferenceOperation(BoundThisReference boundThisReference)
		{
			InstanceReferenceKind instanceReferenceKind = InstanceReferenceKind.ContainingTypeInstance;
			SyntaxNode syntax = boundThisReference.Syntax;
			ITypeSymbol publicTypeSymbol = boundThisReference.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundThisReference.WasCompilerGenerated;
			return new InstanceReferenceOperation(instanceReferenceKind, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000043DC File Offset: 0x000025DC
		private IOperation CreateBoundAssignmentOperatorOrMemberInitializerOperation(BoundAssignmentOperator boundAssignmentOperator)
		{
			if (!CSharpOperationFactory.IsMemberInitializer(boundAssignmentOperator))
			{
				return this.CreateBoundAssignmentOperatorOperation(boundAssignmentOperator);
			}
			return this.CreateBoundMemberInitializerOperation(boundAssignmentOperator);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004404 File Offset: 0x00002604
		private static bool IsMemberInitializer(BoundAssignmentOperator boundAssignmentOperator)
		{
			BoundExpression right = boundAssignmentOperator.Right;
			if (right == null || right.Kind != BoundKind.ObjectInitializerExpression)
			{
				BoundExpression right2 = boundAssignmentOperator.Right;
				return right2 != null && right2.Kind == BoundKind.CollectionInitializerExpression;
			}
			return true;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000443C File Offset: 0x0000263C
		private ISimpleAssignmentOperation CreateBoundAssignmentOperatorOperation(BoundAssignmentOperator boundAssignmentOperator)
		{
			IOperation operation = this.Create(boundAssignmentOperator.Left);
			IOperation operation2 = this.Create(boundAssignmentOperator.Right);
			bool isRef = boundAssignmentOperator.IsRef;
			SyntaxNode syntax = boundAssignmentOperator.Syntax;
			ITypeSymbol publicTypeSymbol = boundAssignmentOperator.GetPublicTypeSymbol();
			ConstantValue constantValue = boundAssignmentOperator.ConstantValue;
			bool wasCompilerGenerated = boundAssignmentOperator.WasCompilerGenerated;
			return new SimpleAssignmentOperation(isRef, operation, operation2, this._semanticModel, syntax, publicTypeSymbol, constantValue, wasCompilerGenerated);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000449C File Offset: 0x0000269C
		private IMemberInitializerOperation CreateBoundMemberInitializerOperation(BoundAssignmentOperator boundAssignmentOperator)
		{
			IOperation operation = this.CreateMemberInitializerInitializedMember(boundAssignmentOperator.Left);
			IObjectOrCollectionInitializerOperation objectOrCollectionInitializerOperation = (IObjectOrCollectionInitializerOperation)this.Create(boundAssignmentOperator.Right);
			SyntaxNode syntax = boundAssignmentOperator.Syntax;
			ITypeSymbol publicTypeSymbol = boundAssignmentOperator.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundAssignmentOperator.WasCompilerGenerated;
			return new MemberInitializerOperation(operation, objectOrCollectionInitializerOperation, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000044EC File Offset: 0x000026EC
		private ICompoundAssignmentOperation CreateBoundCompoundAssignmentOperatorOperation(BoundCompoundAssignmentOperator boundCompoundAssignmentOperator)
		{
			IOperation operation = this.Create(boundCompoundAssignmentOperator.Left);
			IOperation operation2 = this.Create(boundCompoundAssignmentOperator.Right);
			BinaryOperatorKind binaryOperatorKind = CSharpOperationFactory.Helper.DeriveBinaryOperatorKind(boundCompoundAssignmentOperator.Operator.Kind);
			Conversion leftConversion = boundCompoundAssignmentOperator.LeftConversion;
			Conversion finalConversion = boundCompoundAssignmentOperator.FinalConversion;
			bool flag = boundCompoundAssignmentOperator.Operator.Kind.IsLifted();
			bool flag2 = boundCompoundAssignmentOperator.Operator.Kind.IsChecked();
			IMethodSymbol publicSymbol = boundCompoundAssignmentOperator.Operator.Method.GetPublicSymbol();
			SyntaxNode syntax = boundCompoundAssignmentOperator.Syntax;
			ITypeSymbol publicTypeSymbol = boundCompoundAssignmentOperator.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundCompoundAssignmentOperator.WasCompilerGenerated;
			return new CompoundAssignmentOperation(leftConversion, finalConversion, binaryOperatorKind, flag, flag2, publicSymbol, operation, operation2, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000045A4 File Offset: 0x000027A4
		private IIncrementOrDecrementOperation CreateBoundIncrementOperatorOperation(BoundIncrementOperator boundIncrementOperator)
		{
			OperationKind operationKind = (CSharpOperationFactory.Helper.IsDecrement(boundIncrementOperator.OperatorKind) ? OperationKind.Decrement : OperationKind.Increment);
			bool flag = CSharpOperationFactory.Helper.IsPostfixIncrementOrDecrement(boundIncrementOperator.OperatorKind);
			bool flag2 = boundIncrementOperator.OperatorKind.IsLifted();
			bool flag3 = boundIncrementOperator.OperatorKind.IsChecked();
			IOperation operation = this.Create(boundIncrementOperator.Operand);
			IMethodSymbol publicSymbol = boundIncrementOperator.MethodOpt.GetPublicSymbol();
			SyntaxNode syntax = boundIncrementOperator.Syntax;
			ITypeSymbol publicTypeSymbol = boundIncrementOperator.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundIncrementOperator.WasCompilerGenerated;
			return new IncrementOrDecrementOperation(flag, flag2, flag3, operation, publicSymbol, operationKind, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004634 File Offset: 0x00002834
		private IInvalidOperation CreateBoundBadExpressionOperation(BoundBadExpression boundBadExpression)
		{
			SyntaxNode syntax = boundBadExpression.Syntax;
			ITypeSymbol typeSymbol = (syntax.IsMissing ? null : boundBadExpression.GetPublicTypeSymbol());
			bool flag = boundBadExpression.WasCompilerGenerated || boundBadExpression.ChildBoundNodes.Any<BoundExpression>((BoundExpression e) => ((e != null) ? e.Syntax : null) == boundBadExpression.Syntax);
			return new InvalidOperation(this.CreateFromArray<BoundExpression, IOperation>(boundBadExpression.ChildBoundNodes), this._semanticModel, syntax, typeSymbol, null, flag);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000046C0 File Offset: 0x000028C0
		private ITypeParameterObjectCreationOperation CreateBoundNewTOperation(BoundNewT boundNewT)
		{
			IObjectOrCollectionInitializerOperation objectOrCollectionInitializerOperation = (IObjectOrCollectionInitializerOperation)this.Create(boundNewT.InitializerExpressionOpt);
			SyntaxNode syntax = boundNewT.Syntax;
			ITypeSymbol publicTypeSymbol = boundNewT.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundNewT.WasCompilerGenerated;
			return new TypeParameterObjectCreationOperation(objectOrCollectionInitializerOperation, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004704 File Offset: 0x00002904
		private INoPiaObjectCreationOperation CreateNoPiaObjectCreationExpressionOperation(BoundNoPiaObjectCreationExpression creation)
		{
			IObjectOrCollectionInitializerOperation objectOrCollectionInitializerOperation = (IObjectOrCollectionInitializerOperation)this.Create(creation.InitializerExpressionOpt);
			SyntaxNode syntax = creation.Syntax;
			ITypeSymbol publicTypeSymbol = creation.GetPublicTypeSymbol();
			bool wasCompilerGenerated = creation.WasCompilerGenerated;
			return new NoPiaObjectCreationOperation(objectOrCollectionInitializerOperation, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004748 File Offset: 0x00002948
		private IUnaryOperation CreateBoundUnaryOperatorOperation(BoundUnaryOperator boundUnaryOperator)
		{
			UnaryOperatorKind unaryOperatorKind = CSharpOperationFactory.Helper.DeriveUnaryOperatorKind(boundUnaryOperator.OperatorKind);
			IOperation operation = this.Create(boundUnaryOperator.Operand);
			IMethodSymbol publicSymbol = boundUnaryOperator.MethodOpt.GetPublicSymbol();
			SyntaxNode syntax = boundUnaryOperator.Syntax;
			ITypeSymbol publicTypeSymbol = boundUnaryOperator.GetPublicTypeSymbol();
			ConstantValue constantValue = boundUnaryOperator.ConstantValue;
			bool flag = boundUnaryOperator.OperatorKind.IsLifted();
			bool flag2 = boundUnaryOperator.OperatorKind.IsChecked();
			bool wasCompilerGenerated = boundUnaryOperator.WasCompilerGenerated;
			return new UnaryOperation(unaryOperatorKind, operation, flag, flag2, publicSymbol, this._semanticModel, syntax, publicTypeSymbol, constantValue, wasCompilerGenerated);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000047C8 File Offset: 0x000029C8
		private IOperation CreateBoundBinaryOperatorBase(BoundBinaryOperatorBase boundBinaryOperatorBase)
		{
			ArrayBuilder<BoundBinaryOperatorBase> instance = ArrayBuilder<BoundBinaryOperatorBase>.GetInstance();
			BoundBinaryOperatorBase boundBinaryOperatorBase2 = boundBinaryOperatorBase;
			do
			{
				instance.Push(boundBinaryOperatorBase2);
				boundBinaryOperatorBase2 = boundBinaryOperatorBase2.Left as BoundBinaryOperatorBase;
			}
			while (boundBinaryOperatorBase2 != null);
			IOperation operation = null;
			while (instance.TryPop<BoundBinaryOperatorBase>(out boundBinaryOperatorBase2))
			{
				operation = operation ?? this.Create(boundBinaryOperatorBase2.Left);
				IOperation operation2 = this.Create(boundBinaryOperatorBase2.Right);
				BoundBinaryOperator boundBinaryOperator = boundBinaryOperatorBase2 as BoundBinaryOperator;
				IBinaryOperation binaryOperation;
				if (boundBinaryOperator == null)
				{
					BoundUserDefinedConditionalLogicalOperator boundUserDefinedConditionalLogicalOperator = boundBinaryOperatorBase2 as BoundUserDefinedConditionalLogicalOperator;
					if (boundUserDefinedConditionalLogicalOperator == null)
					{
						if (boundBinaryOperatorBase2 != null)
						{
							BoundKind kind = boundBinaryOperatorBase2.Kind;
							throw ExceptionUtilities.UnexpectedValue(kind);
						}
						throw new InvalidOperationException();
					}
					else
					{
						binaryOperation = this.<CreateBoundBinaryOperatorBase>g__createBoundUserDefinedConditionalLogicalOperator|66_1(boundUserDefinedConditionalLogicalOperator, operation, operation2);
					}
				}
				else
				{
					binaryOperation = this.<CreateBoundBinaryOperatorBase>g__createBoundBinaryOperatorOperation|66_0(boundBinaryOperator, operation, operation2);
				}
				operation = binaryOperation;
			}
			instance.Free();
			return operation;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004880 File Offset: 0x00002A80
		private ITupleBinaryOperation CreateBoundTupleBinaryOperatorOperation(BoundTupleBinaryOperator boundTupleBinaryOperator)
		{
			IOperation operation = this.Create(boundTupleBinaryOperator.Left);
			IOperation operation2 = this.Create(boundTupleBinaryOperator.Right);
			BinaryOperatorKind binaryOperatorKind = CSharpOperationFactory.Helper.DeriveBinaryOperatorKind(boundTupleBinaryOperator.OperatorKind);
			SyntaxNode syntax = boundTupleBinaryOperator.Syntax;
			ITypeSymbol publicTypeSymbol = boundTupleBinaryOperator.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundTupleBinaryOperator.WasCompilerGenerated;
			return new TupleBinaryOperation(binaryOperatorKind, operation, operation2, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000048DC File Offset: 0x00002ADC
		private IConditionalOperation CreateBoundConditionalOperatorOperation(BoundConditionalOperator boundConditionalOperator)
		{
			IOperation operation = this.Create(boundConditionalOperator.Condition);
			IOperation operation2 = this.Create(boundConditionalOperator.Consequence);
			IOperation operation3 = this.Create(boundConditionalOperator.Alternative);
			bool isRef = boundConditionalOperator.IsRef;
			SyntaxNode syntax = boundConditionalOperator.Syntax;
			ITypeSymbol publicTypeSymbol = boundConditionalOperator.GetPublicTypeSymbol();
			ConstantValue constantValue = boundConditionalOperator.ConstantValue;
			bool wasCompilerGenerated = boundConditionalOperator.WasCompilerGenerated;
			return new ConditionalOperation(operation, operation2, operation3, isRef, this._semanticModel, syntax, publicTypeSymbol, constantValue, wasCompilerGenerated);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000494C File Offset: 0x00002B4C
		private ICoalesceOperation CreateBoundNullCoalescingOperatorOperation(BoundNullCoalescingOperator boundNullCoalescingOperator)
		{
			IOperation operation = this.Create(boundNullCoalescingOperator.LeftOperand);
			IOperation operation2 = this.Create(boundNullCoalescingOperator.RightOperand);
			SyntaxNode syntax = boundNullCoalescingOperator.Syntax;
			ITypeSymbol publicTypeSymbol = boundNullCoalescingOperator.GetPublicTypeSymbol();
			ConstantValue constantValue = boundNullCoalescingOperator.ConstantValue;
			bool wasCompilerGenerated = boundNullCoalescingOperator.WasCompilerGenerated;
			Conversion conversion = boundNullCoalescingOperator.LeftConversion;
			if (conversion.Exists && !conversion.IsIdentity)
			{
				TypeSymbol type = boundNullCoalescingOperator.Type;
				TypeSymbol type2 = boundNullCoalescingOperator.LeftOperand.Type;
				if (type.Equals((type2 != null) ? type2.StrippedType() : null, TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes))
				{
					conversion = Conversion.Identity;
				}
			}
			return new CoalesceOperation(operation, operation2, conversion, this._semanticModel, syntax, publicTypeSymbol, constantValue, wasCompilerGenerated);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000049F0 File Offset: 0x00002BF0
		private IOperation CreateBoundNullCoalescingAssignmentOperatorOperation(BoundNullCoalescingAssignmentOperator boundNode)
		{
			IOperation operation = this.Create(boundNode.LeftOperand);
			IOperation operation2 = this.Create(boundNode.RightOperand);
			SyntaxNode syntax = boundNode.Syntax;
			ITypeSymbol publicTypeSymbol = boundNode.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundNode.WasCompilerGenerated;
			return new CoalesceAssignmentOperation(operation, operation2, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004A3C File Offset: 0x00002C3C
		private IAwaitOperation CreateBoundAwaitExpressionOperation(BoundAwaitExpression boundAwaitExpression)
		{
			IOperation operation = this.Create(boundAwaitExpression.Expression);
			SyntaxNode syntax = boundAwaitExpression.Syntax;
			ITypeSymbol publicTypeSymbol = boundAwaitExpression.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundAwaitExpression.WasCompilerGenerated;
			return new AwaitOperation(operation, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004A78 File Offset: 0x00002C78
		private IArrayElementReferenceOperation CreateBoundArrayAccessOperation(BoundArrayAccess boundArrayAccess)
		{
			IOperation operation = this.Create(boundArrayAccess.Expression);
			ImmutableArray<IOperation> immutableArray = this.CreateFromArray<BoundExpression, IOperation>(boundArrayAccess.Indices);
			SyntaxNode syntax = boundArrayAccess.Syntax;
			ITypeSymbol publicTypeSymbol = boundArrayAccess.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundArrayAccess.WasCompilerGenerated;
			return new ArrayElementReferenceOperation(operation, immutableArray, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004AC4 File Offset: 0x00002CC4
		private INameOfOperation CreateBoundNameOfOperatorOperation(BoundNameOfOperator boundNameOfOperator)
		{
			IOperation operation = this.Create(boundNameOfOperator.Argument);
			SyntaxNode syntax = boundNameOfOperator.Syntax;
			ITypeSymbol publicTypeSymbol = boundNameOfOperator.GetPublicTypeSymbol();
			ConstantValue constantValue = boundNameOfOperator.ConstantValue;
			bool wasCompilerGenerated = boundNameOfOperator.WasCompilerGenerated;
			return new NameOfOperation(operation, this._semanticModel, syntax, publicTypeSymbol, constantValue, wasCompilerGenerated);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004B08 File Offset: 0x00002D08
		private IThrowOperation CreateBoundThrowExpressionOperation(BoundThrowExpression boundThrowExpression)
		{
			IOperation operation = this.Create(boundThrowExpression.Expression);
			SyntaxNode syntax = boundThrowExpression.Syntax;
			ITypeSymbol publicTypeSymbol = boundThrowExpression.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundThrowExpression.WasCompilerGenerated;
			return new ThrowOperation(operation, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004B44 File Offset: 0x00002D44
		private IAddressOfOperation CreateBoundAddressOfOperatorOperation(BoundAddressOfOperator boundAddressOfOperator)
		{
			IOperation operation = this.Create(boundAddressOfOperator.Operand);
			SyntaxNode syntax = boundAddressOfOperator.Syntax;
			ITypeSymbol publicTypeSymbol = boundAddressOfOperator.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundAddressOfOperator.WasCompilerGenerated;
			return new AddressOfOperation(operation, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004B80 File Offset: 0x00002D80
		private IInstanceReferenceOperation CreateBoundImplicitReceiverOperation(BoundImplicitReceiver boundImplicitReceiver)
		{
			InstanceReferenceKind instanceReferenceKind = InstanceReferenceKind.ImplicitReceiver;
			SyntaxNode syntax = boundImplicitReceiver.Syntax;
			ITypeSymbol publicTypeSymbol = boundImplicitReceiver.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundImplicitReceiver.WasCompilerGenerated;
			return new InstanceReferenceOperation(instanceReferenceKind, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004BB4 File Offset: 0x00002DB4
		private IConditionalAccessOperation CreateBoundConditionalAccessOperation(BoundConditionalAccess boundConditionalAccess)
		{
			IOperation operation = this.Create(boundConditionalAccess.Receiver);
			IOperation operation2 = this.Create(boundConditionalAccess.AccessExpression);
			SyntaxNode syntax = boundConditionalAccess.Syntax;
			ITypeSymbol publicTypeSymbol = boundConditionalAccess.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundConditionalAccess.WasCompilerGenerated;
			return new ConditionalAccessOperation(operation, operation2, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004C00 File Offset: 0x00002E00
		private IConditionalAccessInstanceOperation CreateBoundConditionalReceiverOperation(BoundConditionalReceiver boundConditionalReceiver)
		{
			SyntaxNode syntax = boundConditionalReceiver.Syntax;
			ITypeSymbol publicTypeSymbol = boundConditionalReceiver.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundConditionalReceiver.WasCompilerGenerated;
			return new ConditionalAccessInstanceOperation(this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004C30 File Offset: 0x00002E30
		private IFieldInitializerOperation CreateBoundFieldEqualsValueOperation(BoundFieldEqualsValue boundFieldEqualsValue)
		{
			ImmutableArray<IFieldSymbol> immutableArray = ImmutableArray.Create<IFieldSymbol>(boundFieldEqualsValue.Field.GetPublicSymbol());
			IOperation operation = this.Create(boundFieldEqualsValue.Value);
			SyntaxNode syntax = boundFieldEqualsValue.Syntax;
			bool wasCompilerGenerated = boundFieldEqualsValue.WasCompilerGenerated;
			return new FieldInitializerOperation(immutableArray, boundFieldEqualsValue.Locals.GetPublicSymbols(), operation, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004C84 File Offset: 0x00002E84
		private IPropertyInitializerOperation CreateBoundPropertyEqualsValueOperation(BoundPropertyEqualsValue boundPropertyEqualsValue)
		{
			ImmutableArray<IPropertySymbol> immutableArray = ImmutableArray.Create<IPropertySymbol>(boundPropertyEqualsValue.Property.GetPublicSymbol());
			IOperation operation = this.Create(boundPropertyEqualsValue.Value);
			SyntaxNode syntax = boundPropertyEqualsValue.Syntax;
			bool wasCompilerGenerated = boundPropertyEqualsValue.WasCompilerGenerated;
			return new PropertyInitializerOperation(immutableArray, boundPropertyEqualsValue.Locals.GetPublicSymbols(), operation, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004CD8 File Offset: 0x00002ED8
		private IParameterInitializerOperation CreateBoundParameterEqualsValueOperation(BoundParameterEqualsValue boundParameterEqualsValue)
		{
			IParameterSymbol publicSymbol = boundParameterEqualsValue.Parameter.GetPublicSymbol();
			IOperation operation = this.Create(boundParameterEqualsValue.Value);
			SyntaxNode syntax = boundParameterEqualsValue.Syntax;
			bool wasCompilerGenerated = boundParameterEqualsValue.WasCompilerGenerated;
			return new ParameterInitializerOperation(publicSymbol, boundParameterEqualsValue.Locals.GetPublicSymbols(), operation, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004D24 File Offset: 0x00002F24
		private IBlockOperation CreateBoundBlockOperation(BoundBlock boundBlock)
		{
			ImmutableArray<IOperation> immutableArray = this.CreateFromArray<BoundStatement, IOperation>(boundBlock.Statements);
			ImmutableArray<ILocalSymbol> publicSymbols = boundBlock.Locals.GetPublicSymbols();
			SyntaxNode syntax = boundBlock.Syntax;
			bool wasCompilerGenerated = boundBlock.WasCompilerGenerated;
			return new BlockOperation(immutableArray, publicSymbols, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004D68 File Offset: 0x00002F68
		private IBranchOperation CreateBoundContinueStatementOperation(BoundContinueStatement boundContinueStatement)
		{
			ILabelSymbol publicSymbol = boundContinueStatement.Label.GetPublicSymbol();
			BranchKind branchKind = BranchKind.Continue;
			SyntaxNode syntax = boundContinueStatement.Syntax;
			bool wasCompilerGenerated = boundContinueStatement.WasCompilerGenerated;
			return new BranchOperation(publicSymbol, branchKind, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004DA0 File Offset: 0x00002FA0
		private IBranchOperation CreateBoundBreakStatementOperation(BoundBreakStatement boundBreakStatement)
		{
			ILabelSymbol publicSymbol = boundBreakStatement.Label.GetPublicSymbol();
			BranchKind branchKind = BranchKind.Break;
			SyntaxNode syntax = boundBreakStatement.Syntax;
			bool wasCompilerGenerated = boundBreakStatement.WasCompilerGenerated;
			return new BranchOperation(publicSymbol, branchKind, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004DD8 File Offset: 0x00002FD8
		private IReturnOperation CreateBoundYieldBreakStatementOperation(BoundYieldBreakStatement boundYieldBreakStatement)
		{
			IOperation operation = null;
			SyntaxNode syntax = boundYieldBreakStatement.Syntax;
			bool wasCompilerGenerated = boundYieldBreakStatement.WasCompilerGenerated;
			return new ReturnOperation(operation, OperationKind.YieldBreak, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004E04 File Offset: 0x00003004
		private IBranchOperation CreateBoundGotoStatementOperation(BoundGotoStatement boundGotoStatement)
		{
			ILabelSymbol publicSymbol = boundGotoStatement.Label.GetPublicSymbol();
			BranchKind branchKind = BranchKind.GoTo;
			SyntaxNode syntax = boundGotoStatement.Syntax;
			bool wasCompilerGenerated = boundGotoStatement.WasCompilerGenerated;
			return new BranchOperation(publicSymbol, branchKind, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004E3C File Offset: 0x0000303C
		private IEmptyOperation CreateBoundNoOpStatementOperation(BoundNoOpStatement boundNoOpStatement)
		{
			SyntaxNode syntax = boundNoOpStatement.Syntax;
			bool wasCompilerGenerated = boundNoOpStatement.WasCompilerGenerated;
			return new EmptyOperation(this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004E64 File Offset: 0x00003064
		private IConditionalOperation CreateBoundIfStatementOperation(BoundIfStatement boundIfStatement)
		{
			IOperation operation = this.Create(boundIfStatement.Condition);
			IOperation operation2 = this.Create(boundIfStatement.Consequence);
			IOperation operation3 = this.Create(boundIfStatement.AlternativeOpt);
			bool flag = false;
			SyntaxNode syntax = boundIfStatement.Syntax;
			ITypeSymbol typeSymbol = null;
			ConstantValue constantValue = null;
			bool wasCompilerGenerated = boundIfStatement.WasCompilerGenerated;
			return new ConditionalOperation(operation, operation2, operation3, flag, this._semanticModel, syntax, typeSymbol, constantValue, wasCompilerGenerated);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004EC4 File Offset: 0x000030C4
		private IWhileLoopOperation CreateBoundWhileStatementOperation(BoundWhileStatement boundWhileStatement)
		{
			IOperation operation = this.Create(boundWhileStatement.Condition);
			IOperation operation2 = this.Create(boundWhileStatement.Body);
			ImmutableArray<ILocalSymbol> publicSymbols = boundWhileStatement.Locals.GetPublicSymbols();
			ILabelSymbol publicSymbol = boundWhileStatement.ContinueLabel.GetPublicSymbol();
			ILabelSymbol publicSymbol2 = boundWhileStatement.BreakLabel.GetPublicSymbol();
			bool flag = true;
			bool flag2 = false;
			SyntaxNode syntax = boundWhileStatement.Syntax;
			bool wasCompilerGenerated = boundWhileStatement.WasCompilerGenerated;
			return new WhileLoopOperation(operation, flag, flag2, null, operation2, publicSymbols, publicSymbol, publicSymbol2, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004F3C File Offset: 0x0000313C
		private IWhileLoopOperation CreateBoundDoStatementOperation(BoundDoStatement boundDoStatement)
		{
			IOperation operation = this.Create(boundDoStatement.Condition);
			IOperation operation2 = this.Create(boundDoStatement.Body);
			ILabelSymbol publicSymbol = boundDoStatement.ContinueLabel.GetPublicSymbol();
			ILabelSymbol publicSymbol2 = boundDoStatement.BreakLabel.GetPublicSymbol();
			bool flag = false;
			bool flag2 = false;
			ImmutableArray<ILocalSymbol> publicSymbols = boundDoStatement.Locals.GetPublicSymbols();
			SyntaxNode syntax = boundDoStatement.Syntax;
			bool wasCompilerGenerated = boundDoStatement.WasCompilerGenerated;
			return new WhileLoopOperation(operation, flag, flag2, null, operation2, publicSymbols, publicSymbol, publicSymbol2, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004FB4 File Offset: 0x000031B4
		private IForLoopOperation CreateBoundForStatementOperation(BoundForStatement boundForStatement)
		{
			ImmutableArray<IOperation> immutableArray = this.CreateFromArray<BoundStatement, IOperation>(this.ToStatements(boundForStatement.Initializer));
			IOperation operation = this.Create(boundForStatement.Condition);
			ImmutableArray<IOperation> immutableArray2 = this.CreateFromArray<BoundStatement, IOperation>(this.ToStatements(boundForStatement.Increment));
			IOperation operation2 = this.Create(boundForStatement.Body);
			ImmutableArray<ILocalSymbol> publicSymbols = boundForStatement.OuterLocals.GetPublicSymbols();
			ImmutableArray<ILocalSymbol> publicSymbols2 = boundForStatement.InnerLocals.GetPublicSymbols();
			ILabelSymbol publicSymbol = boundForStatement.ContinueLabel.GetPublicSymbol();
			ILabelSymbol publicSymbol2 = boundForStatement.BreakLabel.GetPublicSymbol();
			SyntaxNode syntax = boundForStatement.Syntax;
			bool wasCompilerGenerated = boundForStatement.WasCompilerGenerated;
			return new ForLoopOperation(immutableArray, publicSymbols2, operation, immutableArray2, operation2, publicSymbols, publicSymbol, publicSymbol2, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000505C File Offset: 0x0000325C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal ForEachLoopOperationInfo GetForEachLoopOperatorInfo(BoundForEachStatement boundForEachStatement)
		{
			ForEachEnumeratorInfo enumeratorInfoOpt = boundForEachStatement.EnumeratorInfoOpt;
			ForEachLoopOperationInfo forEachLoopOperationInfo;
			if (enumeratorInfoOpt != null)
			{
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				CSharpCompilation csharpCompilation = (CSharpCompilation)this._semanticModel.Compilation;
				NamedTypeSymbol namedTypeSymbol = (enumeratorInfoOpt.IsAsync ? csharpCompilation.GetWellKnownType(WellKnownType.System_IAsyncDisposable) : csharpCompilation.GetSpecialType(SpecialType.System_IDisposable));
				ITypeSymbol publicSymbol = enumeratorInfoOpt.ElementType.GetPublicSymbol();
				IMethodSymbol publicSymbol2 = enumeratorInfoOpt.GetEnumeratorInfo.Method.GetPublicSymbol();
				IPropertySymbol publicSymbol3 = ((PropertySymbol)enumeratorInfoOpt.CurrentPropertyGetter.AssociatedSymbol).GetPublicSymbol();
				IMethodSymbol publicSymbol4 = enumeratorInfoOpt.MoveNextInfo.Method.GetPublicSymbol();
				bool isAsync = enumeratorInfoOpt.IsAsync;
				bool needsDisposal = enumeratorInfoOpt.NeedsDisposal;
				bool flag = enumeratorInfoOpt.NeedsDisposal && csharpCompilation.Conversions.ClassifyImplicitConversionFromType(enumeratorInfoOpt.GetEnumeratorInfo.Method.ReturnType, namedTypeSymbol, ref discarded).IsImplicit;
				MethodArgumentInfo patternDisposeInfo = enumeratorInfoOpt.PatternDisposeInfo;
				IMethodSymbol methodSymbol = ((patternDisposeInfo != null) ? patternDisposeInfo.Method.GetPublicSymbol() : null);
				IConvertibleConversion convertibleConversion = enumeratorInfoOpt.CurrentConversion;
				IConvertibleConversion convertibleConversion2 = boundForEachStatement.ElementConversion;
				MethodArgumentInfo getEnumeratorInfo = enumeratorInfoOpt.GetEnumeratorInfo;
				ImmutableArray<IArgumentOperation> immutableArray;
				if (getEnumeratorInfo != null)
				{
					MethodSymbol method = getEnumeratorInfo.Method;
					if (method != null && method.IsExtensionMethod)
					{
						immutableArray = Operation.SetParentOperation<IArgumentOperation>(this.DeriveArguments(getEnumeratorInfo.Method, getEnumeratorInfo.Arguments, default(ImmutableArray<int>), getEnumeratorInfo.DefaultArguments, getEnumeratorInfo.Expanded, boundForEachStatement.Expression.Syntax, true), null);
						goto IL_015B;
					}
				}
				immutableArray = default(ImmutableArray<IArgumentOperation>);
				IL_015B:
				ImmutableArray<IArgumentOperation> immutableArray2 = ((enumeratorInfoOpt.PatternDisposeInfo != null) ? this.CreateDisposeArguments(enumeratorInfoOpt.PatternDisposeInfo, boundForEachStatement.Syntax) : default(ImmutableArray<IArgumentOperation>));
				forEachLoopOperationInfo = new ForEachLoopOperationInfo(publicSymbol, publicSymbol2, publicSymbol3, publicSymbol4, isAsync, needsDisposal, flag, methodSymbol, convertibleConversion, convertibleConversion2, immutableArray, default(ImmutableArray<IArgumentOperation>), default(ImmutableArray<IArgumentOperation>), immutableArray2);
			}
			else
			{
				forEachLoopOperationInfo = null;
			}
			return forEachLoopOperationInfo;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005210 File Offset: 0x00003410
		internal IOperation CreateBoundForEachStatementLoopControlVariable(BoundForEachStatement boundForEachStatement)
		{
			if (boundForEachStatement.DeconstructionOpt != null)
			{
				return this.Create(boundForEachStatement.DeconstructionOpt.DeconstructionAssignment.Left);
			}
			if (boundForEachStatement.IterationErrorExpressionOpt != null)
			{
				return this.Create(boundForEachStatement.IterationErrorExpressionOpt);
			}
			LocalSymbol localSymbol = boundForEachStatement.IterationVariables[0];
			SyntaxNode syntax = boundForEachStatement.IterationVariableType.Syntax;
			return new VariableDeclaratorOperation(localSymbol.GetPublicSymbol(), null, ImmutableArray<IOperation>.Empty, this._semanticModel, syntax, false);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005284 File Offset: 0x00003484
		private IForEachLoopOperation CreateBoundForEachStatementOperation(BoundForEachStatement boundForEachStatement)
		{
			IOperation operation = this.CreateBoundForEachStatementLoopControlVariable(boundForEachStatement);
			IOperation operation2 = this.Create(boundForEachStatement.Expression);
			ImmutableArray<IOperation> empty = ImmutableArray<IOperation>.Empty;
			IOperation operation3 = this.Create(boundForEachStatement.Body);
			ForEachLoopOperationInfo forEachLoopOperatorInfo = this.GetForEachLoopOperatorInfo(boundForEachStatement);
			ImmutableArray<ILocalSymbol> publicSymbols = boundForEachStatement.IterationVariables.GetPublicSymbols();
			ILabelSymbol publicSymbol = boundForEachStatement.ContinueLabel.GetPublicSymbol();
			ILabelSymbol publicSymbol2 = boundForEachStatement.BreakLabel.GetPublicSymbol();
			SyntaxNode syntax = boundForEachStatement.Syntax;
			bool wasCompilerGenerated = boundForEachStatement.WasCompilerGenerated;
			bool flag = boundForEachStatement.AwaitOpt != null;
			return new ForEachLoopOperation(operation, operation2, empty, forEachLoopOperatorInfo, flag, operation3, publicSymbols, publicSymbol, publicSymbol2, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005320 File Offset: 0x00003520
		private ITryOperation CreateBoundTryStatementOperation(BoundTryStatement boundTryStatement)
		{
			IBlockOperation blockOperation = (IBlockOperation)this.Create(boundTryStatement.TryBlock);
			ImmutableArray<ICatchClauseOperation> immutableArray = this.CreateFromArray<BoundCatchBlock, ICatchClauseOperation>(boundTryStatement.CatchBlocks);
			IBlockOperation blockOperation2 = (IBlockOperation)this.Create(boundTryStatement.FinallyBlockOpt);
			SyntaxNode syntax = boundTryStatement.Syntax;
			bool wasCompilerGenerated = boundTryStatement.WasCompilerGenerated;
			return new TryOperation(blockOperation, immutableArray, blockOperation2, null, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000537C File Offset: 0x0000357C
		private ICatchClauseOperation CreateBoundCatchBlockOperation(BoundCatchBlock boundCatchBlock)
		{
			IOperation operation = this.CreateVariableDeclarator((BoundLocal)boundCatchBlock.ExceptionSourceOpt);
			IOperation operation2 = this.Create(boundCatchBlock.ExceptionFilterOpt);
			IBlockOperation blockOperation = (IBlockOperation)this.Create(boundCatchBlock.Body);
			ITypeSymbol typeSymbol = boundCatchBlock.ExceptionTypeOpt.GetPublicSymbol() ?? this._semanticModel.Compilation.ObjectType;
			ImmutableArray<ILocalSymbol> publicSymbols = boundCatchBlock.Locals.GetPublicSymbols();
			SyntaxNode syntax = boundCatchBlock.Syntax;
			bool wasCompilerGenerated = boundCatchBlock.WasCompilerGenerated;
			return new CatchClauseOperation(operation, typeSymbol, publicSymbols, operation2, blockOperation, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005408 File Offset: 0x00003608
		private IFixedOperation CreateBoundFixedStatementOperation(BoundFixedStatement boundFixedStatement)
		{
			IVariableDeclarationGroupOperation variableDeclarationGroupOperation = (IVariableDeclarationGroupOperation)this.Create(boundFixedStatement.Declarations);
			IOperation operation = this.Create(boundFixedStatement.Body);
			ImmutableArray<ILocalSymbol> publicSymbols = boundFixedStatement.Locals.GetPublicSymbols();
			SyntaxNode syntax = boundFixedStatement.Syntax;
			bool wasCompilerGenerated = boundFixedStatement.WasCompilerGenerated;
			return new FixedOperation(publicSymbols, variableDeclarationGroupOperation, operation, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000545C File Offset: 0x0000365C
		private IUsingOperation CreateBoundUsingStatementOperation(BoundUsingStatement boundUsingStatement)
		{
			IOperation operation = this.Create(boundUsingStatement.DeclarationsOpt ?? boundUsingStatement.ExpressionOpt);
			IOperation operation2 = this.Create(boundUsingStatement.Body);
			ImmutableArray<ILocalSymbol> publicSymbols = boundUsingStatement.Locals.GetPublicSymbols();
			bool flag = boundUsingStatement.AwaitOpt != null;
			DisposeOperationInfo disposeOperationInfo = ((boundUsingStatement.PatternDisposeInfoOpt != null) ? new DisposeOperationInfo(boundUsingStatement.PatternDisposeInfoOpt.Method.GetPublicSymbol(), this.CreateDisposeArguments(boundUsingStatement.PatternDisposeInfoOpt, boundUsingStatement.Syntax)) : default(DisposeOperationInfo));
			SyntaxNode syntax = boundUsingStatement.Syntax;
			bool wasCompilerGenerated = boundUsingStatement.WasCompilerGenerated;
			return new UsingOperation(operation, operation2, publicSymbols, flag, disposeOperationInfo, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005504 File Offset: 0x00003704
		private IThrowOperation CreateBoundThrowStatementOperation(BoundThrowStatement boundThrowStatement)
		{
			IOperation operation = this.Create(boundThrowStatement.ExpressionOpt);
			SyntaxNode syntax = boundThrowStatement.Syntax;
			ITypeSymbol typeSymbol = null;
			bool wasCompilerGenerated = boundThrowStatement.WasCompilerGenerated;
			return new ThrowOperation(operation, this._semanticModel, syntax, typeSymbol, wasCompilerGenerated);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000553C File Offset: 0x0000373C
		private IReturnOperation CreateBoundReturnStatementOperation(BoundReturnStatement boundReturnStatement)
		{
			IOperation operation = this.Create(boundReturnStatement.ExpressionOpt);
			SyntaxNode syntax = boundReturnStatement.Syntax;
			bool wasCompilerGenerated = boundReturnStatement.WasCompilerGenerated;
			return new ReturnOperation(operation, OperationKind.Return, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005574 File Offset: 0x00003774
		private IReturnOperation CreateBoundYieldReturnStatementOperation(BoundYieldReturnStatement boundYieldReturnStatement)
		{
			IOperation operation = this.Create(boundYieldReturnStatement.Expression);
			SyntaxNode syntax = boundYieldReturnStatement.Syntax;
			bool wasCompilerGenerated = boundYieldReturnStatement.WasCompilerGenerated;
			return new ReturnOperation(operation, OperationKind.YieldReturn, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000055AC File Offset: 0x000037AC
		private ILockOperation CreateBoundLockStatementOperation(BoundLockStatement boundLockStatement)
		{
			ILocalSymbol localSymbol = ((this._semanticModel.Compilation.CommonGetWellKnownTypeMember(WellKnownMember.System_Threading_Monitor__Enter2) == null) ? null : new SynthesizedLocal((this._semanticModel.GetEnclosingSymbol(boundLockStatement.Syntax.SpanStart, default(CancellationToken)) as IMethodSymbol).GetSymbol(), TypeWithAnnotations.Create(((CSharpCompilation)this._semanticModel.Compilation).GetSpecialType(SpecialType.System_Boolean), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), SynthesizedLocalKind.LockTaken, boundLockStatement.Argument.Syntax, false, RefKind.None).GetPublicSymbol());
			IOperation operation = this.Create(boundLockStatement.Argument);
			IOperation operation2 = this.Create(boundLockStatement.Body);
			SyntaxNode syntax = boundLockStatement.Syntax;
			bool wasCompilerGenerated = boundLockStatement.WasCompilerGenerated;
			return new LockOperation(operation, operation2, localSymbol, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005678 File Offset: 0x00003878
		private IInvalidOperation CreateBoundBadStatementOperation(BoundBadStatement boundBadStatement)
		{
			SyntaxNode syntax = boundBadStatement.Syntax;
			bool flag = boundBadStatement.WasCompilerGenerated || boundBadStatement.ChildBoundNodes.Any<BoundNode>((BoundNode e) => ((e != null) ? e.Syntax : null) == boundBadStatement.Syntax);
			return new InvalidOperation(this.CreateFromArray<BoundNode, IOperation>(boundBadStatement.ChildBoundNodes), this._semanticModel, syntax, null, null, flag);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000056EC File Offset: 0x000038EC
		private IOperation CreateBoundLocalDeclarationOperation(BoundLocalDeclaration boundLocalDeclaration)
		{
			SyntaxNode syntax = boundLocalDeclaration.Syntax;
			SyntaxKind syntaxKind = syntax.Kind();
			SyntaxNode syntaxNode2;
			SyntaxNode syntaxNode;
			if (syntaxKind != SyntaxKind.LocalDeclarationStatement)
			{
				if (syntaxKind != SyntaxKind.VariableDeclarator)
				{
					syntaxNode = (syntaxNode2 = syntax);
				}
				else
				{
					syntaxNode2 = syntax.Parent;
					syntaxNode = syntax.Parent;
				}
			}
			else
			{
				syntaxNode = (syntaxNode2 = (LocalDeclarationStatementSyntax)syntax).Declaration;
			}
			bool wasCompilerGenerated = boundLocalDeclaration.WasCompilerGenerated;
			ImmutableArray<IVariableDeclaratorOperation> immutableArray = this.CreateVariableDeclarator(boundLocalDeclaration, syntaxNode);
			ImmutableArray<IOperation> immutableArray2 = this.CreateIgnoredDimensions(boundLocalDeclaration, syntaxNode);
			IVariableDeclarationOperation variableDeclarationOperation = new VariableDeclarationOperation(immutableArray, null, immutableArray2, this._semanticModel, syntaxNode, wasCompilerGenerated);
			bool flag = syntaxNode2 == syntaxNode || boundLocalDeclaration.WasCompilerGenerated;
			return new VariableDeclarationGroupOperation(ImmutableArray.Create<IVariableDeclarationOperation>(variableDeclarationOperation), this._semanticModel, syntaxNode2, flag);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000578C File Offset: 0x0000398C
		private IOperation CreateBoundMultipleLocalDeclarationsBaseOperation(BoundMultipleLocalDeclarationsBase boundMultipleLocalDeclarations)
		{
			SyntaxNode syntax = boundMultipleLocalDeclarations.Syntax;
			SyntaxNode syntaxNode = (syntax.IsKind(SyntaxKind.LocalDeclarationStatement) ? ((LocalDeclarationStatementSyntax)syntax).Declaration : syntax);
			bool wasCompilerGenerated = boundMultipleLocalDeclarations.WasCompilerGenerated;
			ImmutableArray<IVariableDeclaratorOperation> immutableArray = this.CreateVariableDeclarator(boundMultipleLocalDeclarations, syntaxNode);
			ImmutableArray<IOperation> immutableArray2 = this.CreateIgnoredDimensions(boundMultipleLocalDeclarations, syntaxNode);
			IVariableDeclarationOperation variableDeclarationOperation = new VariableDeclarationOperation(immutableArray, null, immutableArray2, this._semanticModel, syntaxNode, wasCompilerGenerated);
			bool flag = syntax == syntaxNode || boundMultipleLocalDeclarations.WasCompilerGenerated || boundMultipleLocalDeclarations is BoundUsingLocalDeclarations;
			VariableDeclarationGroupOperation variableDeclarationGroupOperation = new VariableDeclarationGroupOperation(ImmutableArray.Create<IVariableDeclarationOperation>(variableDeclarationOperation), this._semanticModel, syntax, flag);
			BoundUsingLocalDeclarations boundUsingLocalDeclarations = boundMultipleLocalDeclarations as BoundUsingLocalDeclarations;
			if (boundUsingLocalDeclarations != null)
			{
				return new UsingDeclarationOperation(variableDeclarationGroupOperation, boundUsingLocalDeclarations.AwaitOpt != null, (boundUsingLocalDeclarations.PatternDisposeInfoOpt != null) ? new DisposeOperationInfo(boundUsingLocalDeclarations.PatternDisposeInfoOpt.Method.GetPublicSymbol(), this.CreateDisposeArguments(boundUsingLocalDeclarations.PatternDisposeInfoOpt, boundUsingLocalDeclarations.Syntax)) : default(DisposeOperationInfo), this._semanticModel, syntax, boundMultipleLocalDeclarations.WasCompilerGenerated);
			}
			return variableDeclarationGroupOperation;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005880 File Offset: 0x00003A80
		private ILabeledOperation CreateBoundLabelStatementOperation(BoundLabelStatement boundLabelStatement)
		{
			ILabelSymbol publicSymbol = boundLabelStatement.Label.GetPublicSymbol();
			SyntaxNode syntax = boundLabelStatement.Syntax;
			bool wasCompilerGenerated = boundLabelStatement.WasCompilerGenerated;
			return new LabeledOperation(publicSymbol, null, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000058B4 File Offset: 0x00003AB4
		private ILabeledOperation CreateBoundLabeledStatementOperation(BoundLabeledStatement boundLabeledStatement)
		{
			ILabelSymbol publicSymbol = boundLabeledStatement.Label.GetPublicSymbol();
			IOperation operation = this.Create(boundLabeledStatement.Body);
			SyntaxNode syntax = boundLabeledStatement.Syntax;
			bool wasCompilerGenerated = boundLabeledStatement.WasCompilerGenerated;
			return new LabeledOperation(publicSymbol, operation, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000058F8 File Offset: 0x00003AF8
		private IExpressionStatementOperation CreateBoundExpressionStatementOperation(BoundExpressionStatement boundExpressionStatement)
		{
			bool flag = boundExpressionStatement.WasCompilerGenerated || boundExpressionStatement.Syntax == boundExpressionStatement.Expression.Syntax;
			SyntaxNode syntax = boundExpressionStatement.Syntax;
			IOperation operation = this.Create(boundExpressionStatement.Expression);
			if (boundExpressionStatement.Expression is BoundSequence)
			{
				flag = true;
			}
			return new ExpressionStatementOperation(operation, this._semanticModel, syntax, flag);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005954 File Offset: 0x00003B54
		internal IOperation CreateBoundTupleOperation(BoundTupleExpression boundTupleExpression, bool createDeclaration = true)
		{
			SyntaxNode syntaxNode = boundTupleExpression.Syntax;
			bool wasCompilerGenerated = boundTupleExpression.WasCompilerGenerated;
			ITypeSymbol publicTypeSymbol = boundTupleExpression.GetPublicTypeSymbol();
			DeclarationExpressionSyntax declarationExpressionSyntax = syntaxNode as DeclarationExpressionSyntax;
			if (declarationExpressionSyntax != null)
			{
				syntaxNode = declarationExpressionSyntax.Designation;
				if (createDeclaration)
				{
					return new DeclarationExpressionOperation(this.CreateBoundTupleOperation(boundTupleExpression, false), this._semanticModel, declarationExpressionSyntax, publicTypeSymbol, false);
				}
			}
			BoundTupleLiteral boundTupleLiteral = boundTupleExpression as BoundTupleLiteral;
			TypeSymbol typeSymbol;
			if (boundTupleLiteral != null)
			{
				TypeSymbol type = boundTupleLiteral.Type;
				typeSymbol = type;
			}
			else
			{
				BoundConvertedTupleLiteral boundConvertedTupleLiteral = boundTupleExpression as BoundConvertedTupleLiteral;
				if (boundConvertedTupleLiteral != null)
				{
					BoundTupleLiteral sourceTuple = boundConvertedTupleLiteral.SourceTuple;
					if (sourceTuple != null)
					{
						TypeSymbol type2 = sourceTuple.Type;
						typeSymbol = type2;
					}
					else
					{
						typeSymbol = null;
					}
				}
				else
				{
					if (boundTupleExpression != null)
					{
						BoundKind kind = boundTupleExpression.Kind;
						throw ExceptionUtilities.UnexpectedValue(kind);
					}
					throw new InvalidOperationException();
				}
			}
			TypeSymbol typeSymbol2 = typeSymbol;
			return new TupleOperation(this.CreateFromArray<BoundExpression, IOperation>(boundTupleExpression.Arguments), typeSymbol2.GetPublicSymbol(), this._semanticModel, syntaxNode, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005A34 File Offset: 0x00003C34
		private IInterpolatedStringOperation CreateBoundInterpolatedStringExpressionOperation(BoundInterpolatedString boundInterpolatedString)
		{
			ImmutableArray<IInterpolatedStringContentOperation> immutableArray = this.CreateBoundInterpolatedStringContentOperation(boundInterpolatedString.Parts);
			SyntaxNode syntax = boundInterpolatedString.Syntax;
			ITypeSymbol publicTypeSymbol = boundInterpolatedString.GetPublicTypeSymbol();
			ConstantValue constantValue = boundInterpolatedString.ConstantValue;
			bool wasCompilerGenerated = boundInterpolatedString.WasCompilerGenerated;
			return new InterpolatedStringOperation(immutableArray, this._semanticModel, syntax, publicTypeSymbol, constantValue, wasCompilerGenerated);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005A78 File Offset: 0x00003C78
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<IInterpolatedStringContentOperation> CreateBoundInterpolatedStringContentOperation([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> parts)
		{
			ArrayBuilder<IInterpolatedStringContentOperation> instance = ArrayBuilder<IInterpolatedStringContentOperation>.GetInstance(parts.Length);
			foreach (BoundExpression boundExpression in parts)
			{
				if (boundExpression.Kind == BoundKind.StringInsert)
				{
					instance.Add((IInterpolatedStringContentOperation)this.Create(boundExpression));
				}
				else
				{
					instance.Add(this.CreateBoundInterpolatedStringTextOperation((BoundLiteral)boundExpression));
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005AE8 File Offset: 0x00003CE8
		private IInterpolationOperation CreateBoundInterpolationOperation(BoundStringInsert boundStringInsert)
		{
			IOperation operation = this.Create(boundStringInsert.Value);
			IOperation operation2 = this.Create(boundStringInsert.Alignment);
			IOperation operation3 = this.Create(boundStringInsert.Format);
			SyntaxNode syntax = boundStringInsert.Syntax;
			bool wasCompilerGenerated = boundStringInsert.WasCompilerGenerated;
			return new InterpolationOperation(operation, operation2, operation3, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005B38 File Offset: 0x00003D38
		private IInterpolatedStringTextOperation CreateBoundInterpolatedStringTextOperation(BoundLiteral boundNode)
		{
			IOperation operation = this.CreateBoundLiteralOperation(boundNode, true);
			SyntaxNode syntax = boundNode.Syntax;
			bool wasCompilerGenerated = boundNode.WasCompilerGenerated;
			return new InterpolatedStringTextOperation(operation, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005B68 File Offset: 0x00003D68
		private IConstantPatternOperation CreateBoundConstantPatternOperation(BoundConstantPattern boundConstantPattern)
		{
			IOperation operation = this.Create(boundConstantPattern.Value);
			SyntaxNode syntax = boundConstantPattern.Syntax;
			bool wasCompilerGenerated = boundConstantPattern.WasCompilerGenerated;
			TypeSymbol inputType = boundConstantPattern.InputType;
			TypeSymbol narrowedType = boundConstantPattern.NarrowedType;
			return new ConstantPatternOperation(operation, inputType.GetPublicSymbol(), narrowedType.GetPublicSymbol(), this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005BB8 File Offset: 0x00003DB8
		private IOperation CreateBoundRelationalPatternOperation(BoundRelationalPattern boundRelationalPattern)
		{
			BinaryOperatorKind binaryOperatorKind = CSharpOperationFactory.Helper.DeriveBinaryOperatorKind(boundRelationalPattern.Relation);
			IOperation operation = this.Create(boundRelationalPattern.Value);
			SyntaxNode syntax = boundRelationalPattern.Syntax;
			bool wasCompilerGenerated = boundRelationalPattern.WasCompilerGenerated;
			TypeSymbol inputType = boundRelationalPattern.InputType;
			TypeSymbol narrowedType = boundRelationalPattern.NarrowedType;
			return new RelationalPatternOperation(binaryOperatorKind, operation, inputType.GetPublicSymbol(), narrowedType.GetPublicSymbol(), this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005C18 File Offset: 0x00003E18
		private IDeclarationPatternOperation CreateBoundDeclarationPatternOperation(BoundDeclarationPattern boundDeclarationPattern)
		{
			ISymbol symbol = boundDeclarationPattern.Variable.GetPublicSymbol();
			if (symbol == null)
			{
				BoundExpression variableAccess = boundDeclarationPattern.VariableAccess;
				if (variableAccess != null && variableAccess.Kind == BoundKind.DiscardExpression)
				{
					symbol = ((BoundDiscardExpression)boundDeclarationPattern.VariableAccess).ExpressionSymbol.GetPublicSymbol();
				}
			}
			ITypeSymbol publicSymbol = boundDeclarationPattern.InputType.GetPublicSymbol();
			ITypeSymbol publicSymbol2 = boundDeclarationPattern.NarrowedType.GetPublicSymbol();
			bool isVar = boundDeclarationPattern.IsVar;
			ITypeSymbol typeSymbol = (isVar ? null : boundDeclarationPattern.DeclaredType.GetPublicTypeSymbol());
			SyntaxNode syntax = boundDeclarationPattern.Syntax;
			bool wasCompilerGenerated = boundDeclarationPattern.WasCompilerGenerated;
			return new DeclarationPatternOperation(typeSymbol, isVar, symbol, publicSymbol, publicSymbol2, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005CB8 File Offset: 0x00003EB8
		private IRecursivePatternOperation CreateBoundRecursivePatternOperation(BoundRecursivePattern boundRecursivePattern)
		{
			BoundTypeExpression declaredType = boundRecursivePattern.DeclaredType;
			ITypeSymbol publicSymbol = (((declaredType != null) ? declaredType.Type : null) ?? boundRecursivePattern.InputType.StrippedType()).GetPublicSymbol();
			ImmutableArray<BoundSubpattern> deconstruction = boundRecursivePattern.Deconstruction;
			ImmutableArray<IPatternOperation> immutableArray;
			if (deconstruction.IsDefault)
			{
				immutableArray = ImmutableArray<IPatternOperation>.Empty;
			}
			else
			{
				immutableArray = deconstruction.SelectAsArray((BoundSubpattern p, CSharpOperationFactory fac) => (IPatternOperation)fac.Create(p.Pattern), this);
			}
			ImmutableArray<IPatternOperation> immutableArray2 = immutableArray;
			ImmutableArray<BoundSubpattern> properties = boundRecursivePattern.Properties;
			ImmutableArray<IPropertySubpatternOperation> immutableArray3;
			if (properties.IsDefault)
			{
				immutableArray3 = ImmutableArray<IPropertySubpatternOperation>.Empty;
			}
			else
			{
				immutableArray3 = properties.SelectAsArray((BoundSubpattern p, [TupleElementNames(new string[] { "Fac", "MatchedType" })] ValueTuple<CSharpOperationFactory, ITypeSymbol> arg) => arg.Item1.CreatePropertySubpattern(p, arg.Item2), new ValueTuple<CSharpOperationFactory, ITypeSymbol>(this, publicSymbol));
			}
			ImmutableArray<IPropertySubpatternOperation> immutableArray4 = immutableArray3;
			return new RecursivePatternOperation(publicSymbol, boundRecursivePattern.DeconstructMethod.GetPublicSymbol(), immutableArray2, immutableArray4, boundRecursivePattern.Variable.GetPublicSymbol(), boundRecursivePattern.InputType.GetPublicSymbol(), boundRecursivePattern.NarrowedType.GetPublicSymbol(), this._semanticModel, boundRecursivePattern.Syntax, boundRecursivePattern.WasCompilerGenerated);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005DB8 File Offset: 0x00003FB8
		private IRecursivePatternOperation CreateBoundRecursivePatternOperation(BoundITuplePattern boundITuplePattern)
		{
			ImmutableArray<BoundSubpattern> subpatterns = boundITuplePattern.Subpatterns;
			ImmutableArray<IPatternOperation> immutableArray;
			if (subpatterns.IsDefault)
			{
				immutableArray = ImmutableArray<IPatternOperation>.Empty;
			}
			else
			{
				immutableArray = subpatterns.SelectAsArray((BoundSubpattern p, CSharpOperationFactory fac) => (IPatternOperation)fac.Create(p.Pattern), this);
			}
			ImmutableArray<IPatternOperation> immutableArray2 = immutableArray;
			return new RecursivePatternOperation(boundITuplePattern.InputType.StrippedType().GetPublicSymbol(), boundITuplePattern.GetLengthMethod.ContainingType.GetPublicSymbol(), immutableArray2, ImmutableArray<IPropertySubpatternOperation>.Empty, null, boundITuplePattern.InputType.GetPublicSymbol(), boundITuplePattern.NarrowedType.GetPublicSymbol(), this._semanticModel, boundITuplePattern.Syntax, boundITuplePattern.WasCompilerGenerated);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005E57 File Offset: 0x00004057
		private IOperation CreateBoundTypePatternOperation(BoundTypePattern boundTypePattern)
		{
			return new TypePatternOperation(boundTypePattern.NarrowedType.GetPublicSymbol(), boundTypePattern.InputType.GetPublicSymbol(), boundTypePattern.NarrowedType.GetPublicSymbol(), this._semanticModel, boundTypePattern.Syntax, boundTypePattern.WasCompilerGenerated);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005E91 File Offset: 0x00004091
		private IOperation CreateBoundNegatedPatternOperation(BoundNegatedPattern boundNegatedPattern)
		{
			return new NegatedPatternOperation((IPatternOperation)this.Create(boundNegatedPattern.Negated), boundNegatedPattern.InputType.GetPublicSymbol(), boundNegatedPattern.NarrowedType.GetPublicSymbol(), this._semanticModel, boundNegatedPattern.Syntax, boundNegatedPattern.WasCompilerGenerated);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005ED4 File Offset: 0x000040D4
		private IOperation CreateBoundBinaryPatternOperation(BoundBinaryPattern boundBinaryPattern)
		{
			return new BinaryPatternOperation(boundBinaryPattern.Disjunction ? BinaryOperatorKind.Or : BinaryOperatorKind.And, (IPatternOperation)this.Create(boundBinaryPattern.Left), (IPatternOperation)this.Create(boundBinaryPattern.Right), boundBinaryPattern.InputType.GetPublicSymbol(), boundBinaryPattern.NarrowedType.GetPublicSymbol(), this._semanticModel, boundBinaryPattern.Syntax, boundBinaryPattern.WasCompilerGenerated);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005F40 File Offset: 0x00004140
		private ISwitchOperation CreateBoundSwitchStatementOperation(BoundSwitchStatement boundSwitchStatement)
		{
			IOperation operation = this.Create(boundSwitchStatement.Expression);
			ImmutableArray<ISwitchCaseOperation> immutableArray = this.CreateFromArray<BoundSwitchSection, ISwitchCaseOperation>(boundSwitchStatement.SwitchSections);
			ImmutableArray<ILocalSymbol> publicSymbols = boundSwitchStatement.InnerLocals.GetPublicSymbols();
			ILabelSymbol publicSymbol = boundSwitchStatement.BreakLabel.GetPublicSymbol();
			SyntaxNode syntax = boundSwitchStatement.Syntax;
			bool wasCompilerGenerated = boundSwitchStatement.WasCompilerGenerated;
			return new SwitchOperation(publicSymbols, operation, immutableArray, publicSymbol, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005FA0 File Offset: 0x000041A0
		private ISwitchCaseOperation CreateBoundSwitchSectionOperation(BoundSwitchSection boundSwitchSection)
		{
			ImmutableArray<ICaseClauseOperation> immutableArray = this.CreateFromArray<BoundSwitchLabel, ICaseClauseOperation>(boundSwitchSection.SwitchLabels);
			ImmutableArray<IOperation> immutableArray2 = this.CreateFromArray<BoundStatement, IOperation>(boundSwitchSection.Statements);
			ImmutableArray<ILocalSymbol> publicSymbols = boundSwitchSection.Locals.GetPublicSymbols();
			return new SwitchCaseOperation(immutableArray, immutableArray2, publicSymbols, null, this._semanticModel, boundSwitchSection.Syntax, boundSwitchSection.WasCompilerGenerated);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005FEC File Offset: 0x000041EC
		private ISwitchExpressionOperation CreateBoundSwitchExpressionOperation(BoundSwitchExpression boundSwitchExpression)
		{
			IOperation operation = this.Create(boundSwitchExpression.Expression);
			ImmutableArray<ISwitchExpressionArmOperation> immutableArray = this.CreateFromArray<BoundSwitchExpressionArm, ISwitchExpressionArmOperation>(boundSwitchExpression.SwitchArms);
			return new SwitchExpressionOperation(operation, immutableArray, this._semanticModel, boundSwitchExpression.Syntax, boundSwitchExpression.GetPublicTypeSymbol(), boundSwitchExpression.WasCompilerGenerated);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006030 File Offset: 0x00004230
		private ISwitchExpressionArmOperation CreateBoundSwitchExpressionArmOperation(BoundSwitchExpressionArm boundSwitchExpressionArm)
		{
			IPatternOperation patternOperation = (IPatternOperation)this.Create(boundSwitchExpressionArm.Pattern);
			IOperation operation = this.Create(boundSwitchExpressionArm.WhenClause);
			IOperation operation2 = this.Create(boundSwitchExpressionArm.Value);
			return new SwitchExpressionArmOperation(patternOperation, operation, operation2, boundSwitchExpressionArm.Locals.GetPublicSymbols(), this._semanticModel, boundSwitchExpressionArm.Syntax, boundSwitchExpressionArm.WasCompilerGenerated);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000608C File Offset: 0x0000428C
		private ICaseClauseOperation CreateBoundSwitchLabelOperation(BoundSwitchLabel boundSwitchLabel)
		{
			SyntaxNode syntax = boundSwitchLabel.Syntax;
			bool wasCompilerGenerated = boundSwitchLabel.WasCompilerGenerated;
			LabelSymbol label = boundSwitchLabel.Label;
			if (boundSwitchLabel.Syntax.Kind() == SyntaxKind.DefaultSwitchLabel)
			{
				return new DefaultCaseClauseOperation(label.GetPublicSymbol(), this._semanticModel, syntax, wasCompilerGenerated);
			}
			if (boundSwitchLabel.WhenClause == null && boundSwitchLabel.Pattern.Kind == BoundKind.ConstantPattern)
			{
				BoundConstantPattern boundConstantPattern = boundSwitchLabel.Pattern as BoundConstantPattern;
				if (boundConstantPattern != null && boundConstantPattern.InputType.IsValidV6SwitchGoverningType(false))
				{
					return new SingleValueCaseClauseOperation(this.Create(boundConstantPattern.Value), label.GetPublicSymbol(), this._semanticModel, syntax, wasCompilerGenerated);
				}
			}
			IPatternOperation patternOperation = (IPatternOperation)this.Create(boundSwitchLabel.Pattern);
			IOperation operation = this.Create(boundSwitchLabel.WhenClause);
			return new PatternCaseClauseOperation(label.GetPublicSymbol(), patternOperation, operation, this._semanticModel, syntax, wasCompilerGenerated);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006164 File Offset: 0x00004364
		private IIsPatternOperation CreateBoundIsPatternExpressionOperation(BoundIsPatternExpression boundIsPatternExpression)
		{
			IOperation operation = this.Create(boundIsPatternExpression.Expression);
			IPatternOperation patternOperation = (IPatternOperation)this.Create(boundIsPatternExpression.Pattern);
			SyntaxNode syntax = boundIsPatternExpression.Syntax;
			ITypeSymbol publicTypeSymbol = boundIsPatternExpression.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundIsPatternExpression.WasCompilerGenerated;
			return new IsPatternOperation(operation, patternOperation, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000061B4 File Offset: 0x000043B4
		private IOperation CreateBoundQueryClauseOperation(BoundQueryClause boundQueryClause)
		{
			if (boundQueryClause.Syntax.Kind() != SyntaxKind.QueryExpression)
			{
				return this.Create(boundQueryClause.Value);
			}
			IOperation operation = this.Create(boundQueryClause.Value);
			SyntaxNode syntax = boundQueryClause.Syntax;
			ITypeSymbol publicTypeSymbol = boundQueryClause.GetPublicTypeSymbol();
			bool wasCompilerGenerated = boundQueryClause.WasCompilerGenerated;
			return new TranslatedQueryOperation(operation, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000620F File Offset: 0x0000440F
		private IOperation CreateBoundRangeVariableOperation(BoundRangeVariable boundRangeVariable)
		{
			return this.Create(boundRangeVariable.Value);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000621D File Offset: 0x0000441D
		private IOperation CreateBoundDiscardExpressionOperation(BoundDiscardExpression boundNode)
		{
			return new DiscardOperation(((DiscardSymbol)boundNode.ExpressionSymbol).GetPublicSymbol(), this._semanticModel, boundNode.Syntax, boundNode.GetPublicTypeSymbol(), boundNode.WasCompilerGenerated);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000624C File Offset: 0x0000444C
		private IOperation CreateFromEndIndexExpressionOperation(BoundFromEndIndexExpression boundIndex)
		{
			return new UnaryOperation(UnaryOperatorKind.Hat, this.Create(boundIndex.Operand), boundIndex.Type.IsNullableType(), false, null, this._semanticModel, boundIndex.Syntax, boundIndex.GetPublicTypeSymbol(), null, boundIndex.WasCompilerGenerated);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006294 File Offset: 0x00004494
		private IOperation CreateRangeExpressionOperation(BoundRangeExpression boundRange)
		{
			IOperation operation = this.Create(boundRange.LeftOperandOpt);
			IOperation operation2 = this.Create(boundRange.RightOperandOpt);
			return new RangeOperation(operation, operation2, boundRange.Type.IsNullableType(), boundRange.MethodOpt.GetPublicSymbol(), this._semanticModel, boundRange.Syntax, boundRange.GetPublicTypeSymbol(), boundRange.WasCompilerGenerated);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000062EE File Offset: 0x000044EE
		private IOperation CreateBoundDiscardPatternOperation(BoundDiscardPattern boundNode)
		{
			return new DiscardPatternOperation(boundNode.InputType.GetPublicSymbol(), boundNode.NarrowedType.GetPublicSymbol(), this._semanticModel, boundNode.Syntax, boundNode.WasCompilerGenerated);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006320 File Offset: 0x00004520
		internal IPropertySubpatternOperation CreatePropertySubpattern(BoundSubpattern subpattern, ITypeSymbol matchedType)
		{
			SyntaxNode syntax = subpattern.Syntax;
			IOperation operation = this.CreatePropertySubpatternMember(subpattern.Symbol, matchedType, syntax);
			IPatternOperation patternOperation = (IPatternOperation)this.Create(subpattern.Pattern);
			return new PropertySubpatternOperation(operation, patternOperation, this._semanticModel, syntax, false);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00006364 File Offset: 0x00004564
		internal IOperation CreatePropertySubpatternMember([global::System.Runtime.CompilerServices.Nullable(2)] Symbol symbol, ITypeSymbol matchedType, SyntaxNode syntax)
		{
			CSharpOperationFactory.<>c__DisplayClass135_0 CS$<>8__locals1;
			CS$<>8__locals1.symbol = symbol;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.matchedType = matchedType;
			SubpatternSyntax subpatternSyntax = syntax as SubpatternSyntax;
			SyntaxNode syntaxNode;
			if (subpatternSyntax == null)
			{
				syntaxNode = null;
			}
			else
			{
				NameColonSyntax nameColon = subpatternSyntax.NameColon;
				syntaxNode = ((nameColon != null) ? nameColon.Name : null);
			}
			CS$<>8__locals1.nameSyntax = syntaxNode ?? syntax;
			bool flag = CS$<>8__locals1.nameSyntax == syntax;
			FieldSymbol fieldSymbol = CS$<>8__locals1.symbol as FieldSymbol;
			if (fieldSymbol != null)
			{
				ConstantValue constantValue = fieldSymbol.GetConstantValue(ConstantFieldsInProgress.Empty, false);
				return new FieldReferenceOperation(fieldSymbol.GetPublicSymbol(), false, this.<CreatePropertySubpatternMember>g__createReceiver|135_0(ref CS$<>8__locals1), this._semanticModel, CS$<>8__locals1.nameSyntax, fieldSymbol.Type.GetPublicSymbol(), constantValue, flag);
			}
			PropertySymbol propertySymbol = CS$<>8__locals1.symbol as PropertySymbol;
			if (propertySymbol == null)
			{
				return OperationFactory.CreateInvalidOperation(this._semanticModel, CS$<>8__locals1.nameSyntax, ImmutableArray<IOperation>.Empty, flag);
			}
			return new PropertyReferenceOperation(propertySymbol.GetPublicSymbol(), ImmutableArray<IArgumentOperation>.Empty, this.<CreatePropertySubpatternMember>g__createReceiver|135_0(ref CS$<>8__locals1), this._semanticModel, CS$<>8__locals1.nameSyntax, propertySymbol.Type.GetPublicSymbol(), flag);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006468 File Offset: 0x00004668
		private IInstanceReferenceOperation CreateCollectionValuePlaceholderOperation(BoundObjectOrCollectionValuePlaceholder placeholder)
		{
			InstanceReferenceKind instanceReferenceKind = InstanceReferenceKind.ImplicitReceiver;
			SyntaxNode syntax = placeholder.Syntax;
			ITypeSymbol publicTypeSymbol = placeholder.GetPublicTypeSymbol();
			bool wasCompilerGenerated = placeholder.WasCompilerGenerated;
			return new InstanceReferenceOperation(instanceReferenceKind, this._semanticModel, syntax, publicTypeSymbol, wasCompilerGenerated);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000649C File Offset: 0x0000469C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<IArgumentOperation> CreateDisposeArguments(MethodArgumentInfo patternDisposeInfo, SyntaxNode syntax)
		{
			if (patternDisposeInfo.Method.ParameterCount == 0)
			{
				return ImmutableArray<IArgumentOperation>.Empty;
			}
			return Operation.SetParentOperation<IArgumentOperation>(this.DeriveArguments(patternDisposeInfo.Method, patternDisposeInfo.Arguments, patternDisposeInfo.ArgsToParamsOpt, patternDisposeInfo.DefaultArguments, patternDisposeInfo.Expanded, syntax, false), null);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000064E8 File Offset: 0x000046E8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<BoundStatement> ToStatements(BoundStatement statement)
		{
			if (statement == null)
			{
				return ImmutableArray<BoundStatement>.Empty;
			}
			if (statement.Kind == BoundKind.StatementList)
			{
				return ((BoundStatementList)statement).Statements;
			}
			return ImmutableArray.Create<BoundStatement>(statement);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000650F File Offset: 0x0000470F
		private IInstanceReferenceOperation CreateImplicitReceiver(SyntaxNode syntax, TypeSymbol type)
		{
			return new InstanceReferenceOperation(InstanceReferenceKind.ImplicitReceiver, this._semanticModel, syntax, type.GetPublicSymbol(), true);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006528 File Offset: 0x00004728
		internal IArgumentOperation CreateArgumentOperation(ArgumentKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] IParameterSymbol parameter, BoundExpression expression)
		{
			IOperation operation = this.Create(expression);
			SyntaxNode syntax = expression.Syntax;
			SyntaxNode syntaxNode2;
			bool flag;
			if (syntax != null)
			{
				ArgumentSyntax argumentSyntax = syntax.Parent as ArgumentSyntax;
				if (argumentSyntax != null)
				{
					SyntaxNode syntaxNode = argumentSyntax;
					bool wasCompilerGenerated = expression.WasCompilerGenerated;
					syntaxNode2 = syntaxNode;
					flag = wasCompilerGenerated;
					goto IL_0040;
				}
			}
			syntaxNode2 = operation.Syntax;
			flag = true;
			IL_0040:
			return new ArgumentOperation(kind, parameter, operation, OperationFactory.IdentityConversion, OperationFactory.IdentityConversion, this._semanticModel, syntaxNode2, flag);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006590 File Offset: 0x00004790
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal IVariableInitializerOperation CreateVariableDeclaratorInitializer(BoundLocalDeclaration boundLocalDeclaration, SyntaxNode syntax)
		{
			if (boundLocalDeclaration.InitializerOpt != null)
			{
				SyntaxNode syntaxNode = null;
				bool flag = false;
				VariableDeclaratorSyntax variableDeclaratorSyntax = syntax as VariableDeclaratorSyntax;
				if (variableDeclaratorSyntax != null)
				{
					syntaxNode = variableDeclaratorSyntax.Initializer;
				}
				if (syntaxNode == null)
				{
					syntaxNode = boundLocalDeclaration.InitializerOpt.Syntax;
					flag = true;
				}
				IOperation operation = this.Create(boundLocalDeclaration.InitializerOpt);
				return new VariableInitializerOperation(ImmutableArray<ILocalSymbol>.Empty, operation, this._semanticModel, syntaxNode, flag);
			}
			return null;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000065F0 File Offset: 0x000047F0
		private IVariableDeclaratorOperation CreateVariableDeclaratorInternal(BoundLocalDeclaration boundLocalDeclaration, SyntaxNode syntax)
		{
			ILocalSymbol publicSymbol = boundLocalDeclaration.LocalSymbol.GetPublicSymbol();
			bool flag = false;
			IVariableInitializerOperation variableInitializerOperation = this.CreateVariableDeclaratorInitializer(boundLocalDeclaration, syntax);
			ImmutableArray<IOperation> immutableArray = this.CreateFromArray<BoundExpression, IOperation>(boundLocalDeclaration.ArgumentsOpt);
			return new VariableDeclaratorOperation(publicSymbol, variableInitializerOperation, immutableArray, this._semanticModel, syntax, flag);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000662F File Offset: 0x0000482F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("boundLocal")]
		internal IVariableDeclaratorOperation CreateVariableDeclarator(BoundLocal boundLocal)
		{
			if (boundLocal != null)
			{
				return new VariableDeclaratorOperation(boundLocal.LocalSymbol.GetPublicSymbol(), null, ImmutableArray<IOperation>.Empty, this._semanticModel, boundLocal.Syntax, false);
			}
			return null;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00006659 File Offset: 0x00004859
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal IOperation CreateReceiverOperation(BoundNode instance, Symbol symbol)
		{
			if (instance == null || instance.Kind == BoundKind.TypeExpression)
			{
				return null;
			}
			if (symbol != null && symbol.IsStatic && instance.WasCompilerGenerated && instance.Kind == BoundKind.ThisReference)
			{
				return null;
			}
			return this.Create(instance);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00006696 File Offset: 0x00004896
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private bool IsCallVirtual(MethodSymbol targetMethod, BoundExpression receiver)
		{
			return targetMethod != null && receiver != null && (targetMethod.IsVirtual || targetMethod.IsAbstract || targetMethod.IsOverride) && !receiver.SuppressVirtualCalls;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000066C1 File Offset: 0x000048C1
		private bool IsMethodInvalid(LookupResultKind resultKind, MethodSymbol targetMethod)
		{
			return resultKind == LookupResultKind.OverloadResolutionFailure || ((targetMethod != null) ? targetMethod.OriginalDefinition : null) is ErrorMethodSymbol;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000066E0 File Offset: 0x000048E0
		internal IEventReferenceOperation CreateBoundEventAccessOperation(BoundEventAssignmentOperator boundEventAssignmentOperator)
		{
			SyntaxNode syntax = boundEventAssignmentOperator.Syntax;
			IEventSymbol publicSymbol = boundEventAssignmentOperator.Event.GetPublicSymbol();
			IOperation operation = this.CreateReceiverOperation(boundEventAssignmentOperator.ReceiverOpt, boundEventAssignmentOperator.Event);
			SyntaxNode left = ((AssignmentExpressionSyntax)syntax).Left;
			bool wasCompilerGenerated = boundEventAssignmentOperator.WasCompilerGenerated;
			return new EventReferenceOperation(publicSymbol, operation, this._semanticModel, left, publicSymbol.Type, wasCompilerGenerated);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000673C File Offset: 0x0000493C
		internal IOperation CreateDelegateTargetOperation(BoundNode delegateNode)
		{
			BoundConversion boundConversion = delegateNode as BoundConversion;
			if (boundConversion != null)
			{
				if (boundConversion.ConversionKind == ConversionKind.MethodGroup)
				{
					return this.CreateBoundMethodGroupSingleMethodOperation((BoundMethodGroup)boundConversion.Operand, boundConversion.SymbolOpt, boundConversion.SuppressVirtualCalls);
				}
				return this.Create(boundConversion.Operand);
			}
			else
			{
				BoundDelegateCreationExpression boundDelegateCreationExpression = (BoundDelegateCreationExpression)delegateNode;
				if (boundDelegateCreationExpression.Argument.Kind == BoundKind.MethodGroup && boundDelegateCreationExpression.MethodOpt != null)
				{
					BoundMethodGroup boundMethodGroup = (BoundMethodGroup)boundDelegateCreationExpression.Argument;
					return this.CreateBoundMethodGroupSingleMethodOperation(boundMethodGroup, boundDelegateCreationExpression.MethodOpt, boundMethodGroup.SuppressVirtualCalls);
				}
				return this.Create(boundDelegateCreationExpression.Argument);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000067DC File Offset: 0x000049DC
		internal IOperation CreateMemberInitializerInitializedMember(BoundNode initializedMember)
		{
			BoundObjectInitializerMember boundObjectInitializerMember = initializedMember as BoundObjectInitializerMember;
			if (boundObjectInitializerMember != null)
			{
				return this.CreateBoundObjectInitializerMemberOperation(boundObjectInitializerMember, true);
			}
			BoundDynamicObjectInitializerMember boundDynamicObjectInitializerMember = initializedMember as BoundDynamicObjectInitializerMember;
			if (boundDynamicObjectInitializerMember == null)
			{
				return this.Create(initializedMember);
			}
			return this.CreateBoundDynamicObjectInitializerMemberOperation(boundDynamicObjectInitializerMember);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006818 File Offset: 0x00004A18
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<IArgumentOperation> DeriveArguments(BoundNode containingExpression, bool isObjectOrCollectionInitializer)
		{
			if (containingExpression.Kind == BoundKind.ObjectInitializerMember)
			{
				BoundObjectInitializerMember boundObjectInitializerMember = (BoundObjectInitializerMember)containingExpression;
				PropertySymbol propertySymbol = (PropertySymbol)boundObjectInitializerMember.MemberSymbol;
				return this.DeriveArguments(propertySymbol, boundObjectInitializerMember.Arguments, boundObjectInitializerMember.ArgsToParamsOpt, boundObjectInitializerMember.DefaultArguments, boundObjectInitializerMember.Expanded, boundObjectInitializerMember.Syntax, false);
			}
			return this.DeriveArguments(containingExpression);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00006874 File Offset: 0x00004A74
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<IArgumentOperation> DeriveArguments(BoundNode containingExpression)
		{
			BoundKind kind = containingExpression.Kind;
			if (kind <= BoundKind.ObjectCreationExpression)
			{
				if (kind == BoundKind.Call)
				{
					BoundCall boundCall = (BoundCall)containingExpression;
					return this.DeriveArguments(boundCall.Method, boundCall.Arguments, boundCall.ArgsToParamsOpt, boundCall.DefaultArguments, boundCall.Expanded, boundCall.Syntax, boundCall.InvokedAsExtensionMethod);
				}
				if (kind == BoundKind.ObjectCreationExpression)
				{
					BoundObjectCreationExpression boundObjectCreationExpression = (BoundObjectCreationExpression)containingExpression;
					return this.DeriveArguments(boundObjectCreationExpression.Constructor, boundObjectCreationExpression.Arguments, boundObjectCreationExpression.ArgsToParamsOpt, boundObjectCreationExpression.DefaultArguments, boundObjectCreationExpression.Expanded, boundObjectCreationExpression.Syntax, false);
				}
			}
			else
			{
				if (kind == BoundKind.CollectionElementInitializer)
				{
					BoundCollectionElementInitializer boundCollectionElementInitializer = (BoundCollectionElementInitializer)containingExpression;
					return this.DeriveArguments(boundCollectionElementInitializer.AddMethod, boundCollectionElementInitializer.Arguments, boundCollectionElementInitializer.ArgsToParamsOpt, boundCollectionElementInitializer.DefaultArguments, boundCollectionElementInitializer.Expanded, boundCollectionElementInitializer.Syntax, boundCollectionElementInitializer.InvokedAsExtensionMethod);
				}
				if (kind == BoundKind.IndexerAccess)
				{
					BoundIndexerAccess boundIndexerAccess = (BoundIndexerAccess)containingExpression;
					return this.DeriveArguments(boundIndexerAccess.Indexer, boundIndexerAccess.Arguments, boundIndexerAccess.ArgsToParamsOpt, boundIndexerAccess.DefaultArguments, boundIndexerAccess.Expanded, boundIndexerAccess.Syntax, false);
				}
			}
			throw ExceptionUtilities.UnexpectedValue(containingExpression.Kind);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000069AC File Offset: 0x00004BAC
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<IArgumentOperation> DeriveArguments(Symbol methodOrIndexer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> boundArguments, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argumentsToParametersOpt, BitVector defaultArguments, bool expanded, SyntaxNode invocationSyntax, bool invokedAsExtensionMethod = false)
		{
			if (methodOrIndexer.GetParameters().IsDefaultOrEmpty && boundArguments.IsDefaultOrEmpty)
			{
				return ImmutableArray<IArgumentOperation>.Empty;
			}
			return LocalRewriter.MakeArgumentsInEvaluationOrder(this, (CSharpCompilation)this._semanticModel.Compilation, invocationSyntax, boundArguments, methodOrIndexer, expanded, argumentsToParametersOpt, defaultArguments, invokedAsExtensionMethod);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000069FC File Offset: 0x00004BFC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<BoundNode> CreateInvalidChildrenFromArgumentsExpression(BoundNode receiverOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, BoundExpression additionalNodeOpt = null)
		{
			ArrayBuilder<BoundNode> instance = ArrayBuilder<BoundNode>.GetInstance();
			if (receiverOpt != null && (!receiverOpt.WasCompilerGenerated || (receiverOpt.Kind != BoundKind.ThisReference && receiverOpt.Kind != BoundKind.BaseReference && receiverOpt.Kind != BoundKind.ObjectOrCollectionValuePlaceholder)))
			{
				instance.Add(receiverOpt);
			}
			instance.AddRange(StaticCast<BoundNode>.From<BoundExpression>(arguments));
			instance.AddIfNotNull(additionalNodeOpt);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00006A58 File Offset: 0x00004C58
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<IOperation> GetAnonymousObjectCreationInitializers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundAnonymousPropertyDeclaration> declarations, SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
		{
			ArrayBuilder<IOperation> instance = ArrayBuilder<IOperation>.GetInstance(arguments.Length);
			int num = 0;
			for (int i = 0; i < arguments.Length; i++)
			{
				IOperation operation = this.Create(arguments[i]);
				InstanceReferenceOperation instanceReferenceOperation = new InstanceReferenceOperation(InstanceReferenceKind.ImplicitReceiver, this._semanticModel, syntax, type, true);
				PropertySymbol anonymousTypeProperty = AnonymousTypeManager.GetAnonymousTypeProperty(type.GetSymbol<NamedTypeSymbol>(), i);
				BoundAnonymousPropertyDeclaration boundAnonymousPropertyDeclaration = CSharpOperationFactory.<GetAnonymousObjectCreationInitializers>g__getDeclaration|154_0(declarations, anonymousTypeProperty, ref num);
				IOperation operation2;
				bool flag;
				if (boundAnonymousPropertyDeclaration == null)
				{
					operation2 = new PropertyReferenceOperation(anonymousTypeProperty.GetPublicSymbol(), ImmutableArray<IArgumentOperation>.Empty, instanceReferenceOperation, this._semanticModel, operation.Syntax, anonymousTypeProperty.Type.GetPublicSymbol(), true);
					flag = true;
				}
				else
				{
					operation2 = new PropertyReferenceOperation(boundAnonymousPropertyDeclaration.Property.GetPublicSymbol(), ImmutableArray<IArgumentOperation>.Empty, instanceReferenceOperation, this._semanticModel, boundAnonymousPropertyDeclaration.Syntax, boundAnonymousPropertyDeclaration.GetPublicTypeSymbol(), boundAnonymousPropertyDeclaration.WasCompilerGenerated);
					flag = isImplicit;
				}
				SyntaxNode syntax2 = operation.Syntax;
				SyntaxNode syntaxNode = ((syntax2 != null) ? syntax2.Parent : null) ?? syntax;
				ITypeSymbol type2 = operation2.Type;
				SimpleAssignmentOperation simpleAssignmentOperation = new SimpleAssignmentOperation(false, operation2, operation, this._semanticModel, syntaxNode, type2, operation.GetConstantValue(), flag);
				instance.Add(simpleAssignmentOperation);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00006B85 File Offset: 0x00004D85
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		private IOperation <CreateBoundObjectInitializerMemberOperation>g__createReceiver|34_0(ref CSharpOperationFactory.<>c__DisplayClass34_0 A_1)
		{
			Symbol memberSymbol = A_1.memberSymbol;
			if (memberSymbol == null || !memberSymbol.IsStatic)
			{
				return this.CreateImplicitReceiver(A_1.boundObjectInitializerMember.Syntax, A_1.boundObjectInitializerMember.ReceiverType);
			}
			return null;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006BBC File Offset: 0x00004DBC
		[CompilerGenerated]
		private IBinaryOperation <CreateBoundBinaryOperatorBase>g__createBoundBinaryOperatorOperation|66_0(BoundBinaryOperator boundBinaryOperator, IOperation left, IOperation right)
		{
			BinaryOperatorKind binaryOperatorKind = CSharpOperationFactory.Helper.DeriveBinaryOperatorKind(boundBinaryOperator.OperatorKind);
			IMethodSymbol methodSymbol = boundBinaryOperator.MethodOpt.GetPublicSymbol();
			IMethodSymbol methodSymbol2 = null;
			if (boundBinaryOperator.Type.IsDynamic() && (binaryOperatorKind == BinaryOperatorKind.ConditionalAnd || binaryOperatorKind == BinaryOperatorKind.ConditionalOr) && methodSymbol != null && methodSymbol.Parameters.Length == 1)
			{
				methodSymbol2 = methodSymbol;
				methodSymbol = null;
			}
			SyntaxNode syntax = boundBinaryOperator.Syntax;
			ITypeSymbol publicTypeSymbol = boundBinaryOperator.GetPublicTypeSymbol();
			ConstantValue constantValue = boundBinaryOperator.ConstantValue;
			bool flag = boundBinaryOperator.OperatorKind.IsLifted();
			bool flag2 = boundBinaryOperator.OperatorKind.IsChecked();
			bool flag3 = false;
			bool wasCompilerGenerated = boundBinaryOperator.WasCompilerGenerated;
			return new BinaryOperation(binaryOperatorKind, left, right, flag, flag2, flag3, methodSymbol, methodSymbol2, this._semanticModel, syntax, publicTypeSymbol, constantValue, wasCompilerGenerated);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00006C6C File Offset: 0x00004E6C
		[CompilerGenerated]
		private IBinaryOperation <CreateBoundBinaryOperatorBase>g__createBoundUserDefinedConditionalLogicalOperator|66_1(BoundUserDefinedConditionalLogicalOperator boundBinaryOperator, IOperation left, IOperation right)
		{
			BinaryOperatorKind binaryOperatorKind = CSharpOperationFactory.Helper.DeriveBinaryOperatorKind(boundBinaryOperator.OperatorKind);
			IMethodSymbol publicSymbol = boundBinaryOperator.LogicalOperator.GetPublicSymbol();
			IMethodSymbol methodSymbol = ((boundBinaryOperator.OperatorKind.Operator() == BinaryOperatorKind.And) ? boundBinaryOperator.FalseOperator.GetPublicSymbol() : boundBinaryOperator.TrueOperator.GetPublicSymbol());
			SyntaxNode syntax = boundBinaryOperator.Syntax;
			ITypeSymbol publicTypeSymbol = boundBinaryOperator.GetPublicTypeSymbol();
			ConstantValue constantValue = boundBinaryOperator.ConstantValue;
			bool flag = boundBinaryOperator.OperatorKind.IsLifted();
			bool flag2 = boundBinaryOperator.OperatorKind.IsChecked();
			bool flag3 = false;
			bool wasCompilerGenerated = boundBinaryOperator.WasCompilerGenerated;
			return new BinaryOperation(binaryOperatorKind, left, right, flag, flag2, flag3, publicSymbol, methodSymbol, this._semanticModel, syntax, publicTypeSymbol, constantValue, wasCompilerGenerated);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00006D11 File Offset: 0x00004F11
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		private IOperation <CreatePropertySubpatternMember>g__createReceiver|135_0(ref CSharpOperationFactory.<>c__DisplayClass135_0 A_1)
		{
			Symbol symbol = A_1.symbol;
			if (symbol == null || symbol.IsStatic)
			{
				return null;
			}
			return new InstanceReferenceOperation(InstanceReferenceKind.PatternInput, this._semanticModel, A_1.nameSyntax, A_1.matchedType, true);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006D48 File Offset: 0x00004F48
		[CompilerGenerated]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static BoundAnonymousPropertyDeclaration <GetAnonymousObjectCreationInitializers>g__getDeclaration|154_0([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundAnonymousPropertyDeclaration> declarations, PropertySymbol currentProperty, ref int currentDeclarationIndex)
		{
			if (currentDeclarationIndex >= declarations.Length)
			{
				return null;
			}
			BoundAnonymousPropertyDeclaration boundAnonymousPropertyDeclaration = declarations[currentDeclarationIndex];
			int? memberIndexOpt = currentProperty.MemberIndexOpt;
			int? memberIndexOpt2 = boundAnonymousPropertyDeclaration.Property.MemberIndexOpt;
			if ((memberIndexOpt.GetValueOrDefault() == memberIndexOpt2.GetValueOrDefault()) & (memberIndexOpt != null == (memberIndexOpt2 != null)))
			{
				currentDeclarationIndex++;
				return boundAnonymousPropertyDeclaration;
			}
			return null;
		}

		// Token: 0x04000007 RID: 7
		private readonly SemanticModel _semanticModel;

		// Token: 0x020005FD RID: 1533
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal class Helper
		{
			// Token: 0x06008161 RID: 33121 RVA: 0x001D70E0 File Offset: 0x001D52E0
			internal static bool IsPostfixIncrementOrDecrement(UnaryOperatorKind operatorKind)
			{
				UnaryOperatorKind unaryOperatorKind = operatorKind.Operator();
				return unaryOperatorKind == UnaryOperatorKind.PostfixIncrement || unaryOperatorKind == UnaryOperatorKind.PostfixDecrement;
			}

			// Token: 0x06008162 RID: 33122 RVA: 0x001D7108 File Offset: 0x001D5308
			internal static bool IsDecrement(UnaryOperatorKind operatorKind)
			{
				UnaryOperatorKind unaryOperatorKind = operatorKind.Operator();
				return unaryOperatorKind == UnaryOperatorKind.PostfixDecrement || unaryOperatorKind == UnaryOperatorKind.PrefixDecrement;
			}

			// Token: 0x06008163 RID: 33123 RVA: 0x001D7130 File Offset: 0x001D5330
			internal static UnaryOperatorKind DeriveUnaryOperatorKind(UnaryOperatorKind operatorKind)
			{
				UnaryOperatorKind unaryOperatorKind = operatorKind.Operator();
				if (unaryOperatorKind <= UnaryOperatorKind.LogicalNegation)
				{
					if (unaryOperatorKind == UnaryOperatorKind.UnaryPlus)
					{
						return UnaryOperatorKind.Plus;
					}
					if (unaryOperatorKind == UnaryOperatorKind.UnaryMinus)
					{
						return UnaryOperatorKind.Minus;
					}
					if (unaryOperatorKind == UnaryOperatorKind.LogicalNegation)
					{
						return UnaryOperatorKind.Not;
					}
				}
				else
				{
					if (unaryOperatorKind == UnaryOperatorKind.BitwiseComplement)
					{
						return UnaryOperatorKind.BitwiseNegation;
					}
					if (unaryOperatorKind == UnaryOperatorKind.True)
					{
						return UnaryOperatorKind.True;
					}
					if (unaryOperatorKind == UnaryOperatorKind.False)
					{
						return UnaryOperatorKind.False;
					}
				}
				return UnaryOperatorKind.None;
			}

			// Token: 0x06008164 RID: 33124 RVA: 0x001D7190 File Offset: 0x001D5390
			internal static BinaryOperatorKind DeriveBinaryOperatorKind(BinaryOperatorKind operatorKind)
			{
				BinaryOperatorKind binaryOperatorKind = operatorKind.OperatorWithLogical();
				if (binaryOperatorKind <= BinaryOperatorKind.NotEqual)
				{
					if (binaryOperatorKind <= BinaryOperatorKind.Division)
					{
						if (binaryOperatorKind <= BinaryOperatorKind.Addition)
						{
							if (binaryOperatorKind == BinaryOperatorKind.Multiplication)
							{
								return BinaryOperatorKind.Multiply;
							}
							if (binaryOperatorKind == BinaryOperatorKind.Addition)
							{
								return BinaryOperatorKind.Add;
							}
						}
						else
						{
							if (binaryOperatorKind == BinaryOperatorKind.Subtraction)
							{
								return BinaryOperatorKind.Subtract;
							}
							if (binaryOperatorKind == BinaryOperatorKind.Division)
							{
								return BinaryOperatorKind.Divide;
							}
						}
					}
					else if (binaryOperatorKind <= BinaryOperatorKind.LeftShift)
					{
						if (binaryOperatorKind == BinaryOperatorKind.Remainder)
						{
							return BinaryOperatorKind.Remainder;
						}
						if (binaryOperatorKind == BinaryOperatorKind.LeftShift)
						{
							return BinaryOperatorKind.LeftShift;
						}
					}
					else
					{
						if (binaryOperatorKind == BinaryOperatorKind.RightShift)
						{
							return BinaryOperatorKind.RightShift;
						}
						if (binaryOperatorKind == BinaryOperatorKind.Equal)
						{
							return BinaryOperatorKind.Equals;
						}
						if (binaryOperatorKind == BinaryOperatorKind.NotEqual)
						{
							return BinaryOperatorKind.NotEquals;
						}
					}
				}
				else if (binaryOperatorKind <= BinaryOperatorKind.LessThanOrEqual)
				{
					if (binaryOperatorKind <= BinaryOperatorKind.LessThan)
					{
						if (binaryOperatorKind == BinaryOperatorKind.GreaterThan)
						{
							return BinaryOperatorKind.GreaterThan;
						}
						if (binaryOperatorKind == BinaryOperatorKind.LessThan)
						{
							return BinaryOperatorKind.LessThan;
						}
					}
					else
					{
						if (binaryOperatorKind == BinaryOperatorKind.GreaterThanOrEqual)
						{
							return BinaryOperatorKind.GreaterThanOrEqual;
						}
						if (binaryOperatorKind == BinaryOperatorKind.LessThanOrEqual)
						{
							return BinaryOperatorKind.LessThanOrEqual;
						}
					}
				}
				else if (binaryOperatorKind <= BinaryOperatorKind.Xor)
				{
					if (binaryOperatorKind == BinaryOperatorKind.And)
					{
						return BinaryOperatorKind.And;
					}
					if (binaryOperatorKind == BinaryOperatorKind.Xor)
					{
						return BinaryOperatorKind.ExclusiveOr;
					}
				}
				else
				{
					if (binaryOperatorKind == BinaryOperatorKind.Or)
					{
						return BinaryOperatorKind.Or;
					}
					if (binaryOperatorKind == BinaryOperatorKind.LogicalAnd)
					{
						return BinaryOperatorKind.ConditionalAnd;
					}
					if (binaryOperatorKind == BinaryOperatorKind.LogicalOr)
					{
						return BinaryOperatorKind.ConditionalOr;
					}
				}
				return BinaryOperatorKind.None;
			}
		}
	}
}
