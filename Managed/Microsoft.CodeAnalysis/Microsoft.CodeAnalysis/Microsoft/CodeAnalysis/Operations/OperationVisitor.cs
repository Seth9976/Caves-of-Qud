using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.FlowAnalysis;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200045A RID: 1114
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class OperationVisitor
	{
		// Token: 0x06002B22 RID: 11042 RVA: 0x0007C32C File Offset: 0x0007A52C
		[NullableContext(2)]
		public virtual void Visit(IOperation operation)
		{
			if (operation != null)
			{
				operation.Accept(this);
			}
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x0007C338 File Offset: 0x0007A538
		public virtual void DefaultVisit(IOperation operation)
		{
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x0007C33A File Offset: 0x0007A53A
		internal virtual void VisitNoneOperation(IOperation operation)
		{
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x0007C33C File Offset: 0x0007A53C
		public virtual void VisitInvalid(IInvalidOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x0007C345 File Offset: 0x0007A545
		public virtual void VisitBlock(IBlockOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x0007C34E File Offset: 0x0007A54E
		public virtual void VisitVariableDeclarationGroup(IVariableDeclarationGroupOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x0007C357 File Offset: 0x0007A557
		public virtual void VisitSwitch(ISwitchOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x0007C360 File Offset: 0x0007A560
		public virtual void VisitForEachLoop(IForEachLoopOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x0007C369 File Offset: 0x0007A569
		public virtual void VisitForLoop(IForLoopOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x0007C372 File Offset: 0x0007A572
		public virtual void VisitForToLoop(IForToLoopOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x0007C37B File Offset: 0x0007A57B
		public virtual void VisitWhileLoop(IWhileLoopOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x0007C384 File Offset: 0x0007A584
		public virtual void VisitLabeled(ILabeledOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x0007C38D File Offset: 0x0007A58D
		public virtual void VisitBranch(IBranchOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x0007C396 File Offset: 0x0007A596
		public virtual void VisitEmpty(IEmptyOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x0007C39F File Offset: 0x0007A59F
		public virtual void VisitReturn(IReturnOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x0007C3A8 File Offset: 0x0007A5A8
		public virtual void VisitLock(ILockOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x0007C3B1 File Offset: 0x0007A5B1
		public virtual void VisitTry(ITryOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x0007C3BA File Offset: 0x0007A5BA
		public virtual void VisitUsing(IUsingOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x0007C3C3 File Offset: 0x0007A5C3
		public virtual void VisitExpressionStatement(IExpressionStatementOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x0007C3CC File Offset: 0x0007A5CC
		public virtual void VisitLocalFunction(ILocalFunctionOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x0007C3D5 File Offset: 0x0007A5D5
		public virtual void VisitStop(IStopOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x0007C3DE File Offset: 0x0007A5DE
		public virtual void VisitEnd(IEndOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x0007C3E7 File Offset: 0x0007A5E7
		public virtual void VisitRaiseEvent(IRaiseEventOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x0007C3F0 File Offset: 0x0007A5F0
		public virtual void VisitLiteral(ILiteralOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x0007C3F9 File Offset: 0x0007A5F9
		public virtual void VisitConversion(IConversionOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x0007C402 File Offset: 0x0007A602
		public virtual void VisitInvocation(IInvocationOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x0007C40B File Offset: 0x0007A60B
		public virtual void VisitArrayElementReference(IArrayElementReferenceOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x0007C414 File Offset: 0x0007A614
		public virtual void VisitLocalReference(ILocalReferenceOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x0007C41D File Offset: 0x0007A61D
		public virtual void VisitParameterReference(IParameterReferenceOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x0007C426 File Offset: 0x0007A626
		public virtual void VisitFieldReference(IFieldReferenceOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x0007C42F File Offset: 0x0007A62F
		public virtual void VisitMethodReference(IMethodReferenceOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x0007C438 File Offset: 0x0007A638
		public virtual void VisitPropertyReference(IPropertyReferenceOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x0007C441 File Offset: 0x0007A641
		public virtual void VisitEventReference(IEventReferenceOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x0007C44A File Offset: 0x0007A64A
		public virtual void VisitUnaryOperator(IUnaryOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x0007C453 File Offset: 0x0007A653
		public virtual void VisitBinaryOperator(IBinaryOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x0007C45C File Offset: 0x0007A65C
		public virtual void VisitConditional(IConditionalOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x0007C465 File Offset: 0x0007A665
		public virtual void VisitCoalesce(ICoalesceOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x0007C46E File Offset: 0x0007A66E
		public virtual void VisitAnonymousFunction(IAnonymousFunctionOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x0007C477 File Offset: 0x0007A677
		public virtual void VisitObjectCreation(IObjectCreationOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x0007C480 File Offset: 0x0007A680
		public virtual void VisitTypeParameterObjectCreation(ITypeParameterObjectCreationOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x0007C489 File Offset: 0x0007A689
		public virtual void VisitArrayCreation(IArrayCreationOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x0007C492 File Offset: 0x0007A692
		public virtual void VisitInstanceReference(IInstanceReferenceOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x0007C49B File Offset: 0x0007A69B
		public virtual void VisitIsType(IIsTypeOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x0007C4A4 File Offset: 0x0007A6A4
		public virtual void VisitAwait(IAwaitOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x0007C4AD File Offset: 0x0007A6AD
		public virtual void VisitSimpleAssignment(ISimpleAssignmentOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x0007C4B6 File Offset: 0x0007A6B6
		public virtual void VisitCompoundAssignment(ICompoundAssignmentOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x0007C4BF File Offset: 0x0007A6BF
		public virtual void VisitParenthesized(IParenthesizedOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x0007C4C8 File Offset: 0x0007A6C8
		public virtual void VisitEventAssignment(IEventAssignmentOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x0007C4D1 File Offset: 0x0007A6D1
		public virtual void VisitConditionalAccess(IConditionalAccessOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x0007C4DA File Offset: 0x0007A6DA
		public virtual void VisitConditionalAccessInstance(IConditionalAccessInstanceOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x0007C4E3 File Offset: 0x0007A6E3
		public virtual void VisitInterpolatedString(IInterpolatedStringOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x0007C4EC File Offset: 0x0007A6EC
		public virtual void VisitAnonymousObjectCreation(IAnonymousObjectCreationOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x0007C4F5 File Offset: 0x0007A6F5
		public virtual void VisitObjectOrCollectionInitializer(IObjectOrCollectionInitializerOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x0007C4FE File Offset: 0x0007A6FE
		public virtual void VisitMemberInitializer(IMemberInitializerOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x0007C507 File Offset: 0x0007A707
		[Obsolete("ICollectionElementInitializerOperation has been replaced with IInvocationOperation and IDynamicInvocationOperation", true)]
		public virtual void VisitCollectionElementInitializer(ICollectionElementInitializerOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x0007C510 File Offset: 0x0007A710
		public virtual void VisitNameOf(INameOfOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x0007C519 File Offset: 0x0007A719
		public virtual void VisitTuple(ITupleOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x0007C522 File Offset: 0x0007A722
		public virtual void VisitDynamicObjectCreation(IDynamicObjectCreationOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x0007C52B File Offset: 0x0007A72B
		public virtual void VisitDynamicMemberReference(IDynamicMemberReferenceOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x0007C534 File Offset: 0x0007A734
		public virtual void VisitDynamicInvocation(IDynamicInvocationOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x0007C53D File Offset: 0x0007A73D
		public virtual void VisitDynamicIndexerAccess(IDynamicIndexerAccessOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x0007C546 File Offset: 0x0007A746
		public virtual void VisitTranslatedQuery(ITranslatedQueryOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x0007C54F File Offset: 0x0007A74F
		public virtual void VisitDelegateCreation(IDelegateCreationOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x0007C558 File Offset: 0x0007A758
		public virtual void VisitDefaultValue(IDefaultValueOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x0007C561 File Offset: 0x0007A761
		public virtual void VisitTypeOf(ITypeOfOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x0007C56A File Offset: 0x0007A76A
		public virtual void VisitSizeOf(ISizeOfOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x0007C573 File Offset: 0x0007A773
		public virtual void VisitAddressOf(IAddressOfOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x0007C57C File Offset: 0x0007A77C
		public virtual void VisitIsPattern(IIsPatternOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x0007C585 File Offset: 0x0007A785
		public virtual void VisitIncrementOrDecrement(IIncrementOrDecrementOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x0007C58E File Offset: 0x0007A78E
		public virtual void VisitThrow(IThrowOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x0007C597 File Offset: 0x0007A797
		public virtual void VisitDeconstructionAssignment(IDeconstructionAssignmentOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x0007C5A0 File Offset: 0x0007A7A0
		public virtual void VisitDeclarationExpression(IDeclarationExpressionOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x0007C5A9 File Offset: 0x0007A7A9
		public virtual void VisitOmittedArgument(IOmittedArgumentOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x0007C5B2 File Offset: 0x0007A7B2
		public virtual void VisitFieldInitializer(IFieldInitializerOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x0007C5BB File Offset: 0x0007A7BB
		public virtual void VisitVariableInitializer(IVariableInitializerOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x0007C5C4 File Offset: 0x0007A7C4
		public virtual void VisitPropertyInitializer(IPropertyInitializerOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x0007C5CD File Offset: 0x0007A7CD
		public virtual void VisitParameterInitializer(IParameterInitializerOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x0007C5D6 File Offset: 0x0007A7D6
		public virtual void VisitArrayInitializer(IArrayInitializerOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x0007C5DF File Offset: 0x0007A7DF
		public virtual void VisitVariableDeclarator(IVariableDeclaratorOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x0007C5E8 File Offset: 0x0007A7E8
		public virtual void VisitVariableDeclaration(IVariableDeclarationOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x0007C5F1 File Offset: 0x0007A7F1
		public virtual void VisitArgument(IArgumentOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x0007C5FA File Offset: 0x0007A7FA
		public virtual void VisitCatchClause(ICatchClauseOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x0007C603 File Offset: 0x0007A803
		public virtual void VisitSwitchCase(ISwitchCaseOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x0007C60C File Offset: 0x0007A80C
		public virtual void VisitDefaultCaseClause(IDefaultCaseClauseOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x0007C615 File Offset: 0x0007A815
		public virtual void VisitPatternCaseClause(IPatternCaseClauseOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x0007C61E File Offset: 0x0007A81E
		public virtual void VisitRangeCaseClause(IRangeCaseClauseOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x0007C627 File Offset: 0x0007A827
		public virtual void VisitRelationalCaseClause(IRelationalCaseClauseOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x0007C630 File Offset: 0x0007A830
		public virtual void VisitSingleValueCaseClause(ISingleValueCaseClauseOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x0007C639 File Offset: 0x0007A839
		public virtual void VisitInterpolatedStringText(IInterpolatedStringTextOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x0007C642 File Offset: 0x0007A842
		public virtual void VisitInterpolation(IInterpolationOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x0007C64B File Offset: 0x0007A84B
		public virtual void VisitConstantPattern(IConstantPatternOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x0007C654 File Offset: 0x0007A854
		public virtual void VisitDeclarationPattern(IDeclarationPatternOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x0007C65D File Offset: 0x0007A85D
		public virtual void VisitTupleBinaryOperator(ITupleBinaryOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x0007C666 File Offset: 0x0007A866
		public virtual void VisitMethodBodyOperation(IMethodBodyOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x0007C66F File Offset: 0x0007A86F
		public virtual void VisitConstructorBodyOperation(IConstructorBodyOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x0007C678 File Offset: 0x0007A878
		public virtual void VisitDiscardOperation(IDiscardOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x0007C681 File Offset: 0x0007A881
		public virtual void VisitFlowCapture(IFlowCaptureOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x0007C68A File Offset: 0x0007A88A
		public virtual void VisitFlowCaptureReference(IFlowCaptureReferenceOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x0007C693 File Offset: 0x0007A893
		public virtual void VisitIsNull(IIsNullOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x0007C69C File Offset: 0x0007A89C
		public virtual void VisitCaughtException(ICaughtExceptionOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x0007C6A5 File Offset: 0x0007A8A5
		public virtual void VisitStaticLocalInitializationSemaphore(IStaticLocalInitializationSemaphoreOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x0007C6AE File Offset: 0x0007A8AE
		public virtual void VisitFlowAnonymousFunction(IFlowAnonymousFunctionOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x0007C6B7 File Offset: 0x0007A8B7
		public virtual void VisitCoalesceAssignment(ICoalesceAssignmentOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x0007C6C0 File Offset: 0x0007A8C0
		public virtual void VisitRangeOperation(IRangeOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x0007C6C9 File Offset: 0x0007A8C9
		public virtual void VisitReDim(IReDimOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x0007C6D2 File Offset: 0x0007A8D2
		public virtual void VisitReDimClause(IReDimClauseOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x0007C6DB File Offset: 0x0007A8DB
		public virtual void VisitRecursivePattern(IRecursivePatternOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x0007C6E4 File Offset: 0x0007A8E4
		public virtual void VisitDiscardPattern(IDiscardPatternOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x0007C6ED File Offset: 0x0007A8ED
		public virtual void VisitSwitchExpression(ISwitchExpressionOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x0007C6F6 File Offset: 0x0007A8F6
		public virtual void VisitSwitchExpressionArm(ISwitchExpressionArmOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x0007C6FF File Offset: 0x0007A8FF
		public virtual void VisitPropertySubpattern(IPropertySubpatternOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x0007C708 File Offset: 0x0007A908
		internal virtual void VisitAggregateQuery(IAggregateQueryOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x0007C711 File Offset: 0x0007A911
		internal virtual void VisitNoPiaObjectCreation(INoPiaObjectCreationOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x0007C71A File Offset: 0x0007A91A
		internal virtual void VisitPlaceholder(IPlaceholderOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x0007C723 File Offset: 0x0007A923
		internal virtual void VisitPointerIndirectionReference(IPointerIndirectionReferenceOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x0007C72C File Offset: 0x0007A92C
		internal virtual void VisitWithStatement(IWithStatementOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x0007C735 File Offset: 0x0007A935
		public virtual void VisitUsingDeclaration(IUsingDeclarationOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x0007C73E File Offset: 0x0007A93E
		public virtual void VisitNegatedPattern(INegatedPatternOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x0007C747 File Offset: 0x0007A947
		public virtual void VisitBinaryPattern(IBinaryPatternOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x0007C750 File Offset: 0x0007A950
		public virtual void VisitTypePattern(ITypePatternOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x0007C759 File Offset: 0x0007A959
		public virtual void VisitRelationalPattern(IRelationalPatternOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x0007C762 File Offset: 0x0007A962
		public virtual void VisitWith(IWithOperation operation)
		{
			this.DefaultVisit(operation);
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x0007C76B File Offset: 0x0007A96B
		internal virtual void VisitFixed(IFixedOperation operation)
		{
			this.VisitNoneOperation(operation);
		}
	}
}
