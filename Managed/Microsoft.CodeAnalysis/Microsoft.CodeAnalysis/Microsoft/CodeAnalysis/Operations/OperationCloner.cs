using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000459 RID: 1113
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 2, 1 })]
	internal sealed class OperationCloner : OperationVisitor<object, IOperation>
	{
		// Token: 0x06002AA3 RID: 10915 RVA: 0x0007A149 File Offset: 0x00078349
		public static T CloneOperation<[Nullable(0)] T>(T operation) where T : IOperation
		{
			return OperationCloner.s_instance.Visit<T>(operation);
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x0007A15E File Offset: 0x0007835E
		[NullableContext(2)]
		[return: NotNullIfNotNull("node")]
		private T Visit<[Nullable(0)] T>(T node) where T : IOperation
		{
			return (T)((object)this.Visit(node, null));
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x0007A172 File Offset: 0x00078372
		public override IOperation DefaultVisit(IOperation operation, [Nullable(2)] object argument)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x0007A179 File Offset: 0x00078379
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<T> VisitArray<T>([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> nodes) where T : IOperation
		{
			return nodes.SelectAsArray((T n, OperationCloner @this) => @this.Visit<T>(n), this);
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x0007A1A1 File Offset: 0x000783A1
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 0, 1, 1 })]
		private ImmutableArray<ValueTuple<ISymbol, T>> VisitArray<T>([Nullable(new byte[] { 0, 0, 1, 1 })] ImmutableArray<ValueTuple<ISymbol, T>> nodes) where T : IOperation
		{
			return nodes.SelectAsArray((ValueTuple<ISymbol, T> n, OperationCloner @this) => new ValueTuple<ISymbol, T>(n.Item1, @this.Visit<T>(n.Item2)), this);
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x0007A1CC File Offset: 0x000783CC
		public override IOperation VisitBlock(IBlockOperation operation, [Nullable(2)] object argument)
		{
			BlockOperation blockOperation = (BlockOperation)operation;
			return new BlockOperation(this.VisitArray<IOperation>(blockOperation.Operations), blockOperation.Locals, blockOperation.OwningSemanticModel, blockOperation.Syntax, blockOperation.IsImplicit);
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x0007A20C File Offset: 0x0007840C
		public override IOperation VisitVariableDeclarationGroup(IVariableDeclarationGroupOperation operation, [Nullable(2)] object argument)
		{
			VariableDeclarationGroupOperation variableDeclarationGroupOperation = (VariableDeclarationGroupOperation)operation;
			return new VariableDeclarationGroupOperation(this.VisitArray<IVariableDeclarationOperation>(variableDeclarationGroupOperation.Declarations), variableDeclarationGroupOperation.OwningSemanticModel, variableDeclarationGroupOperation.Syntax, variableDeclarationGroupOperation.IsImplicit);
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x0007A244 File Offset: 0x00078444
		public override IOperation VisitSwitch(ISwitchOperation operation, [Nullable(2)] object argument)
		{
			SwitchOperation switchOperation = (SwitchOperation)operation;
			return new SwitchOperation(switchOperation.Locals, this.Visit(switchOperation.Value), this.VisitArray<ISwitchCaseOperation>(switchOperation.Cases), switchOperation.ExitLabel, switchOperation.OwningSemanticModel, switchOperation.Syntax, switchOperation.IsImplicit);
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x0007A294 File Offset: 0x00078494
		public override IOperation VisitForEachLoop(IForEachLoopOperation operation, [Nullable(2)] object argument)
		{
			ForEachLoopOperation forEachLoopOperation = (ForEachLoopOperation)operation;
			return new ForEachLoopOperation(this.Visit(forEachLoopOperation.LoopControlVariable), this.Visit(forEachLoopOperation.Collection), this.VisitArray<IOperation>(forEachLoopOperation.NextVariables), forEachLoopOperation.Info, forEachLoopOperation.IsAsynchronous, this.Visit(forEachLoopOperation.Body), forEachLoopOperation.Locals, forEachLoopOperation.ContinueLabel, forEachLoopOperation.ExitLabel, forEachLoopOperation.OwningSemanticModel, forEachLoopOperation.Syntax, forEachLoopOperation.IsImplicit);
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x0007A310 File Offset: 0x00078510
		public override IOperation VisitForLoop(IForLoopOperation operation, [Nullable(2)] object argument)
		{
			ForLoopOperation forLoopOperation = (ForLoopOperation)operation;
			return new ForLoopOperation(this.VisitArray<IOperation>(forLoopOperation.Before), forLoopOperation.ConditionLocals, this.Visit(forLoopOperation.Condition), this.VisitArray<IOperation>(forLoopOperation.AtLoopBottom), this.Visit(forLoopOperation.Body), forLoopOperation.Locals, forLoopOperation.ContinueLabel, forLoopOperation.ExitLabel, forLoopOperation.OwningSemanticModel, forLoopOperation.Syntax, forLoopOperation.IsImplicit);
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x0007A384 File Offset: 0x00078584
		public override IOperation VisitForToLoop(IForToLoopOperation operation, [Nullable(2)] object argument)
		{
			ForToLoopOperation forToLoopOperation = (ForToLoopOperation)operation;
			return new ForToLoopOperation(this.Visit(forToLoopOperation.LoopControlVariable), this.Visit(forToLoopOperation.InitialValue), this.Visit(forToLoopOperation.LimitValue), this.Visit(forToLoopOperation.StepValue), forToLoopOperation.IsChecked, this.VisitArray<IOperation>(forToLoopOperation.NextVariables), forToLoopOperation.Info, this.Visit(forToLoopOperation.Body), forToLoopOperation.Locals, forToLoopOperation.ContinueLabel, forToLoopOperation.ExitLabel, forToLoopOperation.OwningSemanticModel, forToLoopOperation.Syntax, forToLoopOperation.IsImplicit);
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x0007A418 File Offset: 0x00078618
		public override IOperation VisitWhileLoop(IWhileLoopOperation operation, [Nullable(2)] object argument)
		{
			WhileLoopOperation whileLoopOperation = (WhileLoopOperation)operation;
			return new WhileLoopOperation(this.Visit(whileLoopOperation.Condition), whileLoopOperation.ConditionIsTop, whileLoopOperation.ConditionIsUntil, this.Visit(whileLoopOperation.IgnoredCondition), this.Visit(whileLoopOperation.Body), whileLoopOperation.Locals, whileLoopOperation.ContinueLabel, whileLoopOperation.ExitLabel, whileLoopOperation.OwningSemanticModel, whileLoopOperation.Syntax, whileLoopOperation.IsImplicit);
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x0007A488 File Offset: 0x00078688
		public override IOperation VisitLabeled(ILabeledOperation operation, [Nullable(2)] object argument)
		{
			LabeledOperation labeledOperation = (LabeledOperation)operation;
			return new LabeledOperation(labeledOperation.Label, this.Visit(labeledOperation.Operation), labeledOperation.OwningSemanticModel, labeledOperation.Syntax, labeledOperation.IsImplicit);
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x0007A4C8 File Offset: 0x000786C8
		public override IOperation VisitBranch(IBranchOperation operation, [Nullable(2)] object argument)
		{
			BranchOperation branchOperation = (BranchOperation)operation;
			return new BranchOperation(branchOperation.Target, branchOperation.BranchKind, branchOperation.OwningSemanticModel, branchOperation.Syntax, branchOperation.IsImplicit);
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x0007A500 File Offset: 0x00078700
		public override IOperation VisitEmpty(IEmptyOperation operation, [Nullable(2)] object argument)
		{
			EmptyOperation emptyOperation = (EmptyOperation)operation;
			return new EmptyOperation(emptyOperation.OwningSemanticModel, emptyOperation.Syntax, emptyOperation.IsImplicit);
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x0007A52C File Offset: 0x0007872C
		public override IOperation VisitReturn(IReturnOperation operation, [Nullable(2)] object argument)
		{
			ReturnOperation returnOperation = (ReturnOperation)operation;
			return new ReturnOperation(this.Visit(returnOperation.ReturnedValue), returnOperation.Kind, returnOperation.OwningSemanticModel, returnOperation.Syntax, returnOperation.IsImplicit);
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x0007A56C File Offset: 0x0007876C
		public override IOperation VisitLock(ILockOperation operation, [Nullable(2)] object argument)
		{
			LockOperation lockOperation = (LockOperation)operation;
			return new LockOperation(this.Visit(lockOperation.LockedValue), this.Visit(lockOperation.Body), lockOperation.LockTakenSymbol, lockOperation.OwningSemanticModel, lockOperation.Syntax, lockOperation.IsImplicit);
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x0007A5B8 File Offset: 0x000787B8
		public override IOperation VisitTry(ITryOperation operation, [Nullable(2)] object argument)
		{
			TryOperation tryOperation = (TryOperation)operation;
			return new TryOperation(this.Visit<IBlockOperation>(tryOperation.Body), this.VisitArray<ICatchClauseOperation>(tryOperation.Catches), this.Visit<IBlockOperation>(tryOperation.Finally), tryOperation.ExitLabel, tryOperation.OwningSemanticModel, tryOperation.Syntax, tryOperation.IsImplicit);
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x0007A610 File Offset: 0x00078810
		public override IOperation VisitUsing(IUsingOperation operation, [Nullable(2)] object argument)
		{
			UsingOperation usingOperation = (UsingOperation)operation;
			return new UsingOperation(this.Visit(usingOperation.Resources), this.Visit(usingOperation.Body), usingOperation.Locals, usingOperation.IsAsynchronous, usingOperation.DisposeInfo, usingOperation.OwningSemanticModel, usingOperation.Syntax, usingOperation.IsImplicit);
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x0007A668 File Offset: 0x00078868
		public override IOperation VisitExpressionStatement(IExpressionStatementOperation operation, [Nullable(2)] object argument)
		{
			ExpressionStatementOperation expressionStatementOperation = (ExpressionStatementOperation)operation;
			return new ExpressionStatementOperation(this.Visit(expressionStatementOperation.Operation), expressionStatementOperation.OwningSemanticModel, expressionStatementOperation.Syntax, expressionStatementOperation.IsImplicit);
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x0007A6A0 File Offset: 0x000788A0
		public override IOperation VisitLocalFunction(ILocalFunctionOperation operation, [Nullable(2)] object argument)
		{
			LocalFunctionOperation localFunctionOperation = (LocalFunctionOperation)operation;
			return new LocalFunctionOperation(localFunctionOperation.Symbol, this.Visit<IBlockOperation>(localFunctionOperation.Body), this.Visit<IBlockOperation>(localFunctionOperation.IgnoredBody), localFunctionOperation.OwningSemanticModel, localFunctionOperation.Syntax, localFunctionOperation.IsImplicit);
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x0007A6EC File Offset: 0x000788EC
		public override IOperation VisitStop(IStopOperation operation, [Nullable(2)] object argument)
		{
			StopOperation stopOperation = (StopOperation)operation;
			return new StopOperation(stopOperation.OwningSemanticModel, stopOperation.Syntax, stopOperation.IsImplicit);
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x0007A718 File Offset: 0x00078918
		public override IOperation VisitEnd(IEndOperation operation, [Nullable(2)] object argument)
		{
			EndOperation endOperation = (EndOperation)operation;
			return new EndOperation(endOperation.OwningSemanticModel, endOperation.Syntax, endOperation.IsImplicit);
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x0007A744 File Offset: 0x00078944
		public override IOperation VisitRaiseEvent(IRaiseEventOperation operation, [Nullable(2)] object argument)
		{
			RaiseEventOperation raiseEventOperation = (RaiseEventOperation)operation;
			return new RaiseEventOperation(this.Visit<IEventReferenceOperation>(raiseEventOperation.EventReference), this.VisitArray<IArgumentOperation>(raiseEventOperation.Arguments), raiseEventOperation.OwningSemanticModel, raiseEventOperation.Syntax, raiseEventOperation.IsImplicit);
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x0007A788 File Offset: 0x00078988
		public override IOperation VisitLiteral(ILiteralOperation operation, [Nullable(2)] object argument)
		{
			LiteralOperation literalOperation = (LiteralOperation)operation;
			return new LiteralOperation(literalOperation.OwningSemanticModel, literalOperation.Syntax, literalOperation.Type, literalOperation.OperationConstantValue, literalOperation.IsImplicit);
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x0007A7C0 File Offset: 0x000789C0
		public override IOperation VisitConversion(IConversionOperation operation, [Nullable(2)] object argument)
		{
			ConversionOperation conversionOperation = (ConversionOperation)operation;
			return new ConversionOperation(this.Visit(conversionOperation.Operand), conversionOperation.ConversionConvertible, conversionOperation.IsTryCast, conversionOperation.IsChecked, conversionOperation.OwningSemanticModel, conversionOperation.Syntax, conversionOperation.Type, conversionOperation.OperationConstantValue, conversionOperation.IsImplicit);
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x0007A818 File Offset: 0x00078A18
		public override IOperation VisitInvocation(IInvocationOperation operation, [Nullable(2)] object argument)
		{
			InvocationOperation invocationOperation = (InvocationOperation)operation;
			return new InvocationOperation(invocationOperation.TargetMethod, this.Visit(invocationOperation.Instance), invocationOperation.IsVirtual, this.VisitArray<IArgumentOperation>(invocationOperation.Arguments), invocationOperation.OwningSemanticModel, invocationOperation.Syntax, invocationOperation.Type, invocationOperation.IsImplicit);
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x0007A870 File Offset: 0x00078A70
		public override IOperation VisitArrayElementReference(IArrayElementReferenceOperation operation, [Nullable(2)] object argument)
		{
			ArrayElementReferenceOperation arrayElementReferenceOperation = (ArrayElementReferenceOperation)operation;
			return new ArrayElementReferenceOperation(this.Visit(arrayElementReferenceOperation.ArrayReference), this.VisitArray<IOperation>(arrayElementReferenceOperation.Indices), arrayElementReferenceOperation.OwningSemanticModel, arrayElementReferenceOperation.Syntax, arrayElementReferenceOperation.Type, arrayElementReferenceOperation.IsImplicit);
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x0007A8BC File Offset: 0x00078ABC
		public override IOperation VisitLocalReference(ILocalReferenceOperation operation, [Nullable(2)] object argument)
		{
			LocalReferenceOperation localReferenceOperation = (LocalReferenceOperation)operation;
			return new LocalReferenceOperation(localReferenceOperation.Local, localReferenceOperation.IsDeclaration, localReferenceOperation.OwningSemanticModel, localReferenceOperation.Syntax, localReferenceOperation.Type, localReferenceOperation.OperationConstantValue, localReferenceOperation.IsImplicit);
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x0007A900 File Offset: 0x00078B00
		public override IOperation VisitParameterReference(IParameterReferenceOperation operation, [Nullable(2)] object argument)
		{
			ParameterReferenceOperation parameterReferenceOperation = (ParameterReferenceOperation)operation;
			return new ParameterReferenceOperation(parameterReferenceOperation.Parameter, parameterReferenceOperation.OwningSemanticModel, parameterReferenceOperation.Syntax, parameterReferenceOperation.Type, parameterReferenceOperation.IsImplicit);
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x0007A938 File Offset: 0x00078B38
		public override IOperation VisitFieldReference(IFieldReferenceOperation operation, [Nullable(2)] object argument)
		{
			FieldReferenceOperation fieldReferenceOperation = (FieldReferenceOperation)operation;
			return new FieldReferenceOperation(fieldReferenceOperation.Field, fieldReferenceOperation.IsDeclaration, this.Visit(fieldReferenceOperation.Instance), fieldReferenceOperation.OwningSemanticModel, fieldReferenceOperation.Syntax, fieldReferenceOperation.Type, fieldReferenceOperation.OperationConstantValue, fieldReferenceOperation.IsImplicit);
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x0007A988 File Offset: 0x00078B88
		public override IOperation VisitMethodReference(IMethodReferenceOperation operation, [Nullable(2)] object argument)
		{
			MethodReferenceOperation methodReferenceOperation = (MethodReferenceOperation)operation;
			return new MethodReferenceOperation(methodReferenceOperation.Method, methodReferenceOperation.IsVirtual, this.Visit(methodReferenceOperation.Instance), methodReferenceOperation.OwningSemanticModel, methodReferenceOperation.Syntax, methodReferenceOperation.Type, methodReferenceOperation.IsImplicit);
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x0007A9D4 File Offset: 0x00078BD4
		public override IOperation VisitPropertyReference(IPropertyReferenceOperation operation, [Nullable(2)] object argument)
		{
			PropertyReferenceOperation propertyReferenceOperation = (PropertyReferenceOperation)operation;
			return new PropertyReferenceOperation(propertyReferenceOperation.Property, this.VisitArray<IArgumentOperation>(propertyReferenceOperation.Arguments), this.Visit(propertyReferenceOperation.Instance), propertyReferenceOperation.OwningSemanticModel, propertyReferenceOperation.Syntax, propertyReferenceOperation.Type, propertyReferenceOperation.IsImplicit);
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x0007AA24 File Offset: 0x00078C24
		public override IOperation VisitEventReference(IEventReferenceOperation operation, [Nullable(2)] object argument)
		{
			EventReferenceOperation eventReferenceOperation = (EventReferenceOperation)operation;
			return new EventReferenceOperation(eventReferenceOperation.Event, this.Visit(eventReferenceOperation.Instance), eventReferenceOperation.OwningSemanticModel, eventReferenceOperation.Syntax, eventReferenceOperation.Type, eventReferenceOperation.IsImplicit);
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x0007AA68 File Offset: 0x00078C68
		public override IOperation VisitUnaryOperator(IUnaryOperation operation, [Nullable(2)] object argument)
		{
			UnaryOperation unaryOperation = (UnaryOperation)operation;
			return new UnaryOperation(unaryOperation.OperatorKind, this.Visit(unaryOperation.Operand), unaryOperation.IsLifted, unaryOperation.IsChecked, unaryOperation.OperatorMethod, unaryOperation.OwningSemanticModel, unaryOperation.Syntax, unaryOperation.Type, unaryOperation.OperationConstantValue, unaryOperation.IsImplicit);
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x0007AAC4 File Offset: 0x00078CC4
		public override IOperation VisitBinaryOperator(IBinaryOperation operation, [Nullable(2)] object argument)
		{
			BinaryOperation binaryOperation = (BinaryOperation)operation;
			return new BinaryOperation(binaryOperation.OperatorKind, this.Visit(binaryOperation.LeftOperand), this.Visit(binaryOperation.RightOperand), binaryOperation.IsLifted, binaryOperation.IsChecked, binaryOperation.IsCompareText, binaryOperation.OperatorMethod, binaryOperation.UnaryOperatorMethod, binaryOperation.OwningSemanticModel, binaryOperation.Syntax, binaryOperation.Type, binaryOperation.OperationConstantValue, binaryOperation.IsImplicit);
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x0007AB38 File Offset: 0x00078D38
		public override IOperation VisitConditional(IConditionalOperation operation, [Nullable(2)] object argument)
		{
			ConditionalOperation conditionalOperation = (ConditionalOperation)operation;
			return new ConditionalOperation(this.Visit(conditionalOperation.Condition), this.Visit(conditionalOperation.WhenTrue), this.Visit(conditionalOperation.WhenFalse), conditionalOperation.IsRef, conditionalOperation.OwningSemanticModel, conditionalOperation.Syntax, conditionalOperation.Type, conditionalOperation.OperationConstantValue, conditionalOperation.IsImplicit);
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x0007AB9C File Offset: 0x00078D9C
		public override IOperation VisitCoalesce(ICoalesceOperation operation, [Nullable(2)] object argument)
		{
			CoalesceOperation coalesceOperation = (CoalesceOperation)operation;
			return new CoalesceOperation(this.Visit(coalesceOperation.Value), this.Visit(coalesceOperation.WhenNull), coalesceOperation.ValueConversionConvertible, coalesceOperation.OwningSemanticModel, coalesceOperation.Syntax, coalesceOperation.Type, coalesceOperation.OperationConstantValue, coalesceOperation.IsImplicit);
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x0007ABF4 File Offset: 0x00078DF4
		public override IOperation VisitAnonymousFunction(IAnonymousFunctionOperation operation, [Nullable(2)] object argument)
		{
			AnonymousFunctionOperation anonymousFunctionOperation = (AnonymousFunctionOperation)operation;
			return new AnonymousFunctionOperation(anonymousFunctionOperation.Symbol, this.Visit<IBlockOperation>(anonymousFunctionOperation.Body), anonymousFunctionOperation.OwningSemanticModel, anonymousFunctionOperation.Syntax, anonymousFunctionOperation.IsImplicit);
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x0007AC34 File Offset: 0x00078E34
		public override IOperation VisitObjectCreation(IObjectCreationOperation operation, [Nullable(2)] object argument)
		{
			ObjectCreationOperation objectCreationOperation = (ObjectCreationOperation)operation;
			return new ObjectCreationOperation(objectCreationOperation.Constructor, this.Visit<IObjectOrCollectionInitializerOperation>(objectCreationOperation.Initializer), this.VisitArray<IArgumentOperation>(objectCreationOperation.Arguments), objectCreationOperation.OwningSemanticModel, objectCreationOperation.Syntax, objectCreationOperation.Type, objectCreationOperation.OperationConstantValue, objectCreationOperation.IsImplicit);
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x0007AC8C File Offset: 0x00078E8C
		public override IOperation VisitTypeParameterObjectCreation(ITypeParameterObjectCreationOperation operation, [Nullable(2)] object argument)
		{
			TypeParameterObjectCreationOperation typeParameterObjectCreationOperation = (TypeParameterObjectCreationOperation)operation;
			return new TypeParameterObjectCreationOperation(this.Visit<IObjectOrCollectionInitializerOperation>(typeParameterObjectCreationOperation.Initializer), typeParameterObjectCreationOperation.OwningSemanticModel, typeParameterObjectCreationOperation.Syntax, typeParameterObjectCreationOperation.Type, typeParameterObjectCreationOperation.IsImplicit);
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x0007ACCC File Offset: 0x00078ECC
		public override IOperation VisitArrayCreation(IArrayCreationOperation operation, [Nullable(2)] object argument)
		{
			ArrayCreationOperation arrayCreationOperation = (ArrayCreationOperation)operation;
			return new ArrayCreationOperation(this.VisitArray<IOperation>(arrayCreationOperation.DimensionSizes), this.Visit<IArrayInitializerOperation>(arrayCreationOperation.Initializer), arrayCreationOperation.OwningSemanticModel, arrayCreationOperation.Syntax, arrayCreationOperation.Type, arrayCreationOperation.IsImplicit);
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x0007AD18 File Offset: 0x00078F18
		public override IOperation VisitInstanceReference(IInstanceReferenceOperation operation, [Nullable(2)] object argument)
		{
			InstanceReferenceOperation instanceReferenceOperation = (InstanceReferenceOperation)operation;
			return new InstanceReferenceOperation(instanceReferenceOperation.ReferenceKind, instanceReferenceOperation.OwningSemanticModel, instanceReferenceOperation.Syntax, instanceReferenceOperation.Type, instanceReferenceOperation.IsImplicit);
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x0007AD50 File Offset: 0x00078F50
		public override IOperation VisitIsType(IIsTypeOperation operation, [Nullable(2)] object argument)
		{
			IsTypeOperation isTypeOperation = (IsTypeOperation)operation;
			return new IsTypeOperation(this.Visit(isTypeOperation.ValueOperand), isTypeOperation.TypeOperand, isTypeOperation.IsNegated, isTypeOperation.OwningSemanticModel, isTypeOperation.Syntax, isTypeOperation.Type, isTypeOperation.IsImplicit);
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x0007AD9C File Offset: 0x00078F9C
		public override IOperation VisitAwait(IAwaitOperation operation, [Nullable(2)] object argument)
		{
			AwaitOperation awaitOperation = (AwaitOperation)operation;
			return new AwaitOperation(this.Visit(awaitOperation.Operation), awaitOperation.OwningSemanticModel, awaitOperation.Syntax, awaitOperation.Type, awaitOperation.IsImplicit);
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x0007ADDC File Offset: 0x00078FDC
		public override IOperation VisitSimpleAssignment(ISimpleAssignmentOperation operation, [Nullable(2)] object argument)
		{
			SimpleAssignmentOperation simpleAssignmentOperation = (SimpleAssignmentOperation)operation;
			return new SimpleAssignmentOperation(simpleAssignmentOperation.IsRef, this.Visit(simpleAssignmentOperation.Target), this.Visit(simpleAssignmentOperation.Value), simpleAssignmentOperation.OwningSemanticModel, simpleAssignmentOperation.Syntax, simpleAssignmentOperation.Type, simpleAssignmentOperation.OperationConstantValue, simpleAssignmentOperation.IsImplicit);
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x0007AE34 File Offset: 0x00079034
		public override IOperation VisitCompoundAssignment(ICompoundAssignmentOperation operation, [Nullable(2)] object argument)
		{
			CompoundAssignmentOperation compoundAssignmentOperation = (CompoundAssignmentOperation)operation;
			return new CompoundAssignmentOperation(compoundAssignmentOperation.InConversionConvertible, compoundAssignmentOperation.OutConversionConvertible, compoundAssignmentOperation.OperatorKind, compoundAssignmentOperation.IsLifted, compoundAssignmentOperation.IsChecked, compoundAssignmentOperation.OperatorMethod, this.Visit(compoundAssignmentOperation.Target), this.Visit(compoundAssignmentOperation.Value), compoundAssignmentOperation.OwningSemanticModel, compoundAssignmentOperation.Syntax, compoundAssignmentOperation.Type, compoundAssignmentOperation.IsImplicit);
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x0007AEA4 File Offset: 0x000790A4
		public override IOperation VisitParenthesized(IParenthesizedOperation operation, [Nullable(2)] object argument)
		{
			ParenthesizedOperation parenthesizedOperation = (ParenthesizedOperation)operation;
			return new ParenthesizedOperation(this.Visit(parenthesizedOperation.Operand), parenthesizedOperation.OwningSemanticModel, parenthesizedOperation.Syntax, parenthesizedOperation.Type, parenthesizedOperation.OperationConstantValue, parenthesizedOperation.IsImplicit);
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x0007AEE8 File Offset: 0x000790E8
		public override IOperation VisitEventAssignment(IEventAssignmentOperation operation, [Nullable(2)] object argument)
		{
			EventAssignmentOperation eventAssignmentOperation = (EventAssignmentOperation)operation;
			return new EventAssignmentOperation(this.Visit(eventAssignmentOperation.EventReference), this.Visit(eventAssignmentOperation.HandlerValue), eventAssignmentOperation.Adds, eventAssignmentOperation.OwningSemanticModel, eventAssignmentOperation.Syntax, eventAssignmentOperation.Type, eventAssignmentOperation.IsImplicit);
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x0007AF38 File Offset: 0x00079138
		public override IOperation VisitConditionalAccess(IConditionalAccessOperation operation, [Nullable(2)] object argument)
		{
			ConditionalAccessOperation conditionalAccessOperation = (ConditionalAccessOperation)operation;
			return new ConditionalAccessOperation(this.Visit(conditionalAccessOperation.Operation), this.Visit(conditionalAccessOperation.WhenNotNull), conditionalAccessOperation.OwningSemanticModel, conditionalAccessOperation.Syntax, conditionalAccessOperation.Type, conditionalAccessOperation.IsImplicit);
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x0007AF84 File Offset: 0x00079184
		public override IOperation VisitConditionalAccessInstance(IConditionalAccessInstanceOperation operation, [Nullable(2)] object argument)
		{
			ConditionalAccessInstanceOperation conditionalAccessInstanceOperation = (ConditionalAccessInstanceOperation)operation;
			return new ConditionalAccessInstanceOperation(conditionalAccessInstanceOperation.OwningSemanticModel, conditionalAccessInstanceOperation.Syntax, conditionalAccessInstanceOperation.Type, conditionalAccessInstanceOperation.IsImplicit);
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x0007AFB8 File Offset: 0x000791B8
		public override IOperation VisitInterpolatedString(IInterpolatedStringOperation operation, [Nullable(2)] object argument)
		{
			InterpolatedStringOperation interpolatedStringOperation = (InterpolatedStringOperation)operation;
			return new InterpolatedStringOperation(this.VisitArray<IInterpolatedStringContentOperation>(interpolatedStringOperation.Parts), interpolatedStringOperation.OwningSemanticModel, interpolatedStringOperation.Syntax, interpolatedStringOperation.Type, interpolatedStringOperation.OperationConstantValue, interpolatedStringOperation.IsImplicit);
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x0007AFFC File Offset: 0x000791FC
		public override IOperation VisitAnonymousObjectCreation(IAnonymousObjectCreationOperation operation, [Nullable(2)] object argument)
		{
			AnonymousObjectCreationOperation anonymousObjectCreationOperation = (AnonymousObjectCreationOperation)operation;
			return new AnonymousObjectCreationOperation(this.VisitArray<IOperation>(anonymousObjectCreationOperation.Initializers), anonymousObjectCreationOperation.OwningSemanticModel, anonymousObjectCreationOperation.Syntax, anonymousObjectCreationOperation.Type, anonymousObjectCreationOperation.IsImplicit);
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x0007B03C File Offset: 0x0007923C
		public override IOperation VisitObjectOrCollectionInitializer(IObjectOrCollectionInitializerOperation operation, [Nullable(2)] object argument)
		{
			ObjectOrCollectionInitializerOperation objectOrCollectionInitializerOperation = (ObjectOrCollectionInitializerOperation)operation;
			return new ObjectOrCollectionInitializerOperation(this.VisitArray<IOperation>(objectOrCollectionInitializerOperation.Initializers), objectOrCollectionInitializerOperation.OwningSemanticModel, objectOrCollectionInitializerOperation.Syntax, objectOrCollectionInitializerOperation.Type, objectOrCollectionInitializerOperation.IsImplicit);
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x0007B07C File Offset: 0x0007927C
		public override IOperation VisitMemberInitializer(IMemberInitializerOperation operation, [Nullable(2)] object argument)
		{
			MemberInitializerOperation memberInitializerOperation = (MemberInitializerOperation)operation;
			return new MemberInitializerOperation(this.Visit(memberInitializerOperation.InitializedMember), this.Visit<IObjectOrCollectionInitializerOperation>(memberInitializerOperation.Initializer), memberInitializerOperation.OwningSemanticModel, memberInitializerOperation.Syntax, memberInitializerOperation.Type, memberInitializerOperation.IsImplicit);
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x0007B0C8 File Offset: 0x000792C8
		public override IOperation VisitNameOf(INameOfOperation operation, [Nullable(2)] object argument)
		{
			NameOfOperation nameOfOperation = (NameOfOperation)operation;
			return new NameOfOperation(this.Visit(nameOfOperation.Argument), nameOfOperation.OwningSemanticModel, nameOfOperation.Syntax, nameOfOperation.Type, nameOfOperation.OperationConstantValue, nameOfOperation.IsImplicit);
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x0007B10C File Offset: 0x0007930C
		public override IOperation VisitTuple(ITupleOperation operation, [Nullable(2)] object argument)
		{
			TupleOperation tupleOperation = (TupleOperation)operation;
			return new TupleOperation(this.VisitArray<IOperation>(tupleOperation.Elements), tupleOperation.NaturalType, tupleOperation.OwningSemanticModel, tupleOperation.Syntax, tupleOperation.Type, tupleOperation.IsImplicit);
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x0007B150 File Offset: 0x00079350
		public override IOperation VisitDynamicMemberReference(IDynamicMemberReferenceOperation operation, [Nullable(2)] object argument)
		{
			DynamicMemberReferenceOperation dynamicMemberReferenceOperation = (DynamicMemberReferenceOperation)operation;
			return new DynamicMemberReferenceOperation(this.Visit(dynamicMemberReferenceOperation.Instance), dynamicMemberReferenceOperation.MemberName, dynamicMemberReferenceOperation.TypeArguments, dynamicMemberReferenceOperation.ContainingType, dynamicMemberReferenceOperation.OwningSemanticModel, dynamicMemberReferenceOperation.Syntax, dynamicMemberReferenceOperation.Type, dynamicMemberReferenceOperation.IsImplicit);
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x0007B1A0 File Offset: 0x000793A0
		public override IOperation VisitTranslatedQuery(ITranslatedQueryOperation operation, [Nullable(2)] object argument)
		{
			TranslatedQueryOperation translatedQueryOperation = (TranslatedQueryOperation)operation;
			return new TranslatedQueryOperation(this.Visit(translatedQueryOperation.Operation), translatedQueryOperation.OwningSemanticModel, translatedQueryOperation.Syntax, translatedQueryOperation.Type, translatedQueryOperation.IsImplicit);
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x0007B1E0 File Offset: 0x000793E0
		public override IOperation VisitDelegateCreation(IDelegateCreationOperation operation, [Nullable(2)] object argument)
		{
			DelegateCreationOperation delegateCreationOperation = (DelegateCreationOperation)operation;
			return new DelegateCreationOperation(this.Visit(delegateCreationOperation.Target), delegateCreationOperation.OwningSemanticModel, delegateCreationOperation.Syntax, delegateCreationOperation.Type, delegateCreationOperation.IsImplicit);
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x0007B220 File Offset: 0x00079420
		public override IOperation VisitDefaultValue(IDefaultValueOperation operation, [Nullable(2)] object argument)
		{
			DefaultValueOperation defaultValueOperation = (DefaultValueOperation)operation;
			return new DefaultValueOperation(defaultValueOperation.OwningSemanticModel, defaultValueOperation.Syntax, defaultValueOperation.Type, defaultValueOperation.OperationConstantValue, defaultValueOperation.IsImplicit);
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x0007B258 File Offset: 0x00079458
		public override IOperation VisitTypeOf(ITypeOfOperation operation, [Nullable(2)] object argument)
		{
			TypeOfOperation typeOfOperation = (TypeOfOperation)operation;
			return new TypeOfOperation(typeOfOperation.TypeOperand, typeOfOperation.OwningSemanticModel, typeOfOperation.Syntax, typeOfOperation.Type, typeOfOperation.IsImplicit);
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x0007B290 File Offset: 0x00079490
		public override IOperation VisitSizeOf(ISizeOfOperation operation, [Nullable(2)] object argument)
		{
			SizeOfOperation sizeOfOperation = (SizeOfOperation)operation;
			return new SizeOfOperation(sizeOfOperation.TypeOperand, sizeOfOperation.OwningSemanticModel, sizeOfOperation.Syntax, sizeOfOperation.Type, sizeOfOperation.OperationConstantValue, sizeOfOperation.IsImplicit);
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x0007B2D0 File Offset: 0x000794D0
		public override IOperation VisitAddressOf(IAddressOfOperation operation, [Nullable(2)] object argument)
		{
			AddressOfOperation addressOfOperation = (AddressOfOperation)operation;
			return new AddressOfOperation(this.Visit(addressOfOperation.Reference), addressOfOperation.OwningSemanticModel, addressOfOperation.Syntax, addressOfOperation.Type, addressOfOperation.IsImplicit);
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x0007B310 File Offset: 0x00079510
		public override IOperation VisitIsPattern(IIsPatternOperation operation, [Nullable(2)] object argument)
		{
			IsPatternOperation isPatternOperation = (IsPatternOperation)operation;
			return new IsPatternOperation(this.Visit(isPatternOperation.Value), this.Visit<IPatternOperation>(isPatternOperation.Pattern), isPatternOperation.OwningSemanticModel, isPatternOperation.Syntax, isPatternOperation.Type, isPatternOperation.IsImplicit);
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x0007B35C File Offset: 0x0007955C
		public override IOperation VisitIncrementOrDecrement(IIncrementOrDecrementOperation operation, [Nullable(2)] object argument)
		{
			IncrementOrDecrementOperation incrementOrDecrementOperation = (IncrementOrDecrementOperation)operation;
			return new IncrementOrDecrementOperation(incrementOrDecrementOperation.IsPostfix, incrementOrDecrementOperation.IsLifted, incrementOrDecrementOperation.IsChecked, this.Visit(incrementOrDecrementOperation.Target), incrementOrDecrementOperation.OperatorMethod, incrementOrDecrementOperation.Kind, incrementOrDecrementOperation.OwningSemanticModel, incrementOrDecrementOperation.Syntax, incrementOrDecrementOperation.Type, incrementOrDecrementOperation.IsImplicit);
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x0007B3B8 File Offset: 0x000795B8
		public override IOperation VisitThrow(IThrowOperation operation, [Nullable(2)] object argument)
		{
			ThrowOperation throwOperation = (ThrowOperation)operation;
			return new ThrowOperation(this.Visit(throwOperation.Exception), throwOperation.OwningSemanticModel, throwOperation.Syntax, throwOperation.Type, throwOperation.IsImplicit);
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x0007B3F8 File Offset: 0x000795F8
		public override IOperation VisitDeconstructionAssignment(IDeconstructionAssignmentOperation operation, [Nullable(2)] object argument)
		{
			DeconstructionAssignmentOperation deconstructionAssignmentOperation = (DeconstructionAssignmentOperation)operation;
			return new DeconstructionAssignmentOperation(this.Visit(deconstructionAssignmentOperation.Target), this.Visit(deconstructionAssignmentOperation.Value), deconstructionAssignmentOperation.OwningSemanticModel, deconstructionAssignmentOperation.Syntax, deconstructionAssignmentOperation.Type, deconstructionAssignmentOperation.IsImplicit);
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x0007B444 File Offset: 0x00079644
		public override IOperation VisitDeclarationExpression(IDeclarationExpressionOperation operation, [Nullable(2)] object argument)
		{
			DeclarationExpressionOperation declarationExpressionOperation = (DeclarationExpressionOperation)operation;
			return new DeclarationExpressionOperation(this.Visit(declarationExpressionOperation.Expression), declarationExpressionOperation.OwningSemanticModel, declarationExpressionOperation.Syntax, declarationExpressionOperation.Type, declarationExpressionOperation.IsImplicit);
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x0007B484 File Offset: 0x00079684
		public override IOperation VisitOmittedArgument(IOmittedArgumentOperation operation, [Nullable(2)] object argument)
		{
			OmittedArgumentOperation omittedArgumentOperation = (OmittedArgumentOperation)operation;
			return new OmittedArgumentOperation(omittedArgumentOperation.OwningSemanticModel, omittedArgumentOperation.Syntax, omittedArgumentOperation.Type, omittedArgumentOperation.IsImplicit);
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x0007B4B8 File Offset: 0x000796B8
		public override IOperation VisitFieldInitializer(IFieldInitializerOperation operation, [Nullable(2)] object argument)
		{
			FieldInitializerOperation fieldInitializerOperation = (FieldInitializerOperation)operation;
			return new FieldInitializerOperation(fieldInitializerOperation.InitializedFields, fieldInitializerOperation.Locals, this.Visit(fieldInitializerOperation.Value), fieldInitializerOperation.OwningSemanticModel, fieldInitializerOperation.Syntax, fieldInitializerOperation.IsImplicit);
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x0007B4FC File Offset: 0x000796FC
		public override IOperation VisitVariableInitializer(IVariableInitializerOperation operation, [Nullable(2)] object argument)
		{
			VariableInitializerOperation variableInitializerOperation = (VariableInitializerOperation)operation;
			return new VariableInitializerOperation(variableInitializerOperation.Locals, this.Visit(variableInitializerOperation.Value), variableInitializerOperation.OwningSemanticModel, variableInitializerOperation.Syntax, variableInitializerOperation.IsImplicit);
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x0007B53C File Offset: 0x0007973C
		public override IOperation VisitPropertyInitializer(IPropertyInitializerOperation operation, [Nullable(2)] object argument)
		{
			PropertyInitializerOperation propertyInitializerOperation = (PropertyInitializerOperation)operation;
			return new PropertyInitializerOperation(propertyInitializerOperation.InitializedProperties, propertyInitializerOperation.Locals, this.Visit(propertyInitializerOperation.Value), propertyInitializerOperation.OwningSemanticModel, propertyInitializerOperation.Syntax, propertyInitializerOperation.IsImplicit);
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x0007B580 File Offset: 0x00079780
		public override IOperation VisitParameterInitializer(IParameterInitializerOperation operation, [Nullable(2)] object argument)
		{
			ParameterInitializerOperation parameterInitializerOperation = (ParameterInitializerOperation)operation;
			return new ParameterInitializerOperation(parameterInitializerOperation.Parameter, parameterInitializerOperation.Locals, this.Visit(parameterInitializerOperation.Value), parameterInitializerOperation.OwningSemanticModel, parameterInitializerOperation.Syntax, parameterInitializerOperation.IsImplicit);
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x0007B5C4 File Offset: 0x000797C4
		public override IOperation VisitArrayInitializer(IArrayInitializerOperation operation, [Nullable(2)] object argument)
		{
			ArrayInitializerOperation arrayInitializerOperation = (ArrayInitializerOperation)operation;
			return new ArrayInitializerOperation(this.VisitArray<IOperation>(arrayInitializerOperation.ElementValues), arrayInitializerOperation.OwningSemanticModel, arrayInitializerOperation.Syntax, arrayInitializerOperation.IsImplicit);
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x0007B5FC File Offset: 0x000797FC
		public override IOperation VisitVariableDeclarator(IVariableDeclaratorOperation operation, [Nullable(2)] object argument)
		{
			VariableDeclaratorOperation variableDeclaratorOperation = (VariableDeclaratorOperation)operation;
			return new VariableDeclaratorOperation(variableDeclaratorOperation.Symbol, this.Visit<IVariableInitializerOperation>(variableDeclaratorOperation.Initializer), this.VisitArray<IOperation>(variableDeclaratorOperation.IgnoredArguments), variableDeclaratorOperation.OwningSemanticModel, variableDeclaratorOperation.Syntax, variableDeclaratorOperation.IsImplicit);
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x0007B648 File Offset: 0x00079848
		public override IOperation VisitVariableDeclaration(IVariableDeclarationOperation operation, [Nullable(2)] object argument)
		{
			VariableDeclarationOperation variableDeclarationOperation = (VariableDeclarationOperation)operation;
			return new VariableDeclarationOperation(this.VisitArray<IVariableDeclaratorOperation>(variableDeclarationOperation.Declarators), this.Visit<IVariableInitializerOperation>(variableDeclarationOperation.Initializer), this.VisitArray<IOperation>(variableDeclarationOperation.IgnoredDimensions), variableDeclarationOperation.OwningSemanticModel, variableDeclarationOperation.Syntax, variableDeclarationOperation.IsImplicit);
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x0007B698 File Offset: 0x00079898
		public override IOperation VisitArgument(IArgumentOperation operation, [Nullable(2)] object argument)
		{
			ArgumentOperation argumentOperation = (ArgumentOperation)operation;
			return new ArgumentOperation(argumentOperation.ArgumentKind, argumentOperation.Parameter, this.Visit(argumentOperation.Value), argumentOperation.InConversionConvertible, argumentOperation.OutConversionConvertible, argumentOperation.OwningSemanticModel, argumentOperation.Syntax, argumentOperation.IsImplicit);
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x0007B6E8 File Offset: 0x000798E8
		public override IOperation VisitCatchClause(ICatchClauseOperation operation, [Nullable(2)] object argument)
		{
			CatchClauseOperation catchClauseOperation = (CatchClauseOperation)operation;
			return new CatchClauseOperation(this.Visit(catchClauseOperation.ExceptionDeclarationOrExpression), catchClauseOperation.ExceptionType, catchClauseOperation.Locals, this.Visit(catchClauseOperation.Filter), this.Visit<IBlockOperation>(catchClauseOperation.Handler), catchClauseOperation.OwningSemanticModel, catchClauseOperation.Syntax, catchClauseOperation.IsImplicit);
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x0007B744 File Offset: 0x00079944
		public override IOperation VisitSwitchCase(ISwitchCaseOperation operation, [Nullable(2)] object argument)
		{
			SwitchCaseOperation switchCaseOperation = (SwitchCaseOperation)operation;
			return new SwitchCaseOperation(this.VisitArray<ICaseClauseOperation>(switchCaseOperation.Clauses), this.VisitArray<IOperation>(switchCaseOperation.Body), switchCaseOperation.Locals, this.Visit(switchCaseOperation.Condition), switchCaseOperation.OwningSemanticModel, switchCaseOperation.Syntax, switchCaseOperation.IsImplicit);
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x0007B79C File Offset: 0x0007999C
		public override IOperation VisitDefaultCaseClause(IDefaultCaseClauseOperation operation, [Nullable(2)] object argument)
		{
			DefaultCaseClauseOperation defaultCaseClauseOperation = (DefaultCaseClauseOperation)operation;
			return new DefaultCaseClauseOperation(defaultCaseClauseOperation.Label, defaultCaseClauseOperation.OwningSemanticModel, defaultCaseClauseOperation.Syntax, defaultCaseClauseOperation.IsImplicit);
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x0007B7D0 File Offset: 0x000799D0
		public override IOperation VisitPatternCaseClause(IPatternCaseClauseOperation operation, [Nullable(2)] object argument)
		{
			PatternCaseClauseOperation patternCaseClauseOperation = (PatternCaseClauseOperation)operation;
			return new PatternCaseClauseOperation(patternCaseClauseOperation.Label, this.Visit<IPatternOperation>(patternCaseClauseOperation.Pattern), this.Visit(patternCaseClauseOperation.Guard), patternCaseClauseOperation.OwningSemanticModel, patternCaseClauseOperation.Syntax, patternCaseClauseOperation.IsImplicit);
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x0007B81C File Offset: 0x00079A1C
		public override IOperation VisitRangeCaseClause(IRangeCaseClauseOperation operation, [Nullable(2)] object argument)
		{
			RangeCaseClauseOperation rangeCaseClauseOperation = (RangeCaseClauseOperation)operation;
			return new RangeCaseClauseOperation(this.Visit(rangeCaseClauseOperation.MinimumValue), this.Visit(rangeCaseClauseOperation.MaximumValue), rangeCaseClauseOperation.Label, rangeCaseClauseOperation.OwningSemanticModel, rangeCaseClauseOperation.Syntax, rangeCaseClauseOperation.IsImplicit);
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x0007B868 File Offset: 0x00079A68
		public override IOperation VisitRelationalCaseClause(IRelationalCaseClauseOperation operation, [Nullable(2)] object argument)
		{
			RelationalCaseClauseOperation relationalCaseClauseOperation = (RelationalCaseClauseOperation)operation;
			return new RelationalCaseClauseOperation(this.Visit(relationalCaseClauseOperation.Value), relationalCaseClauseOperation.Relation, relationalCaseClauseOperation.Label, relationalCaseClauseOperation.OwningSemanticModel, relationalCaseClauseOperation.Syntax, relationalCaseClauseOperation.IsImplicit);
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x0007B8AC File Offset: 0x00079AAC
		public override IOperation VisitSingleValueCaseClause(ISingleValueCaseClauseOperation operation, [Nullable(2)] object argument)
		{
			SingleValueCaseClauseOperation singleValueCaseClauseOperation = (SingleValueCaseClauseOperation)operation;
			return new SingleValueCaseClauseOperation(this.Visit(singleValueCaseClauseOperation.Value), singleValueCaseClauseOperation.Label, singleValueCaseClauseOperation.OwningSemanticModel, singleValueCaseClauseOperation.Syntax, singleValueCaseClauseOperation.IsImplicit);
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x0007B8EC File Offset: 0x00079AEC
		public override IOperation VisitInterpolatedStringText(IInterpolatedStringTextOperation operation, [Nullable(2)] object argument)
		{
			InterpolatedStringTextOperation interpolatedStringTextOperation = (InterpolatedStringTextOperation)operation;
			return new InterpolatedStringTextOperation(this.Visit(interpolatedStringTextOperation.Text), interpolatedStringTextOperation.OwningSemanticModel, interpolatedStringTextOperation.Syntax, interpolatedStringTextOperation.IsImplicit);
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x0007B924 File Offset: 0x00079B24
		public override IOperation VisitInterpolation(IInterpolationOperation operation, [Nullable(2)] object argument)
		{
			InterpolationOperation interpolationOperation = (InterpolationOperation)operation;
			return new InterpolationOperation(this.Visit(interpolationOperation.Expression), this.Visit(interpolationOperation.Alignment), this.Visit(interpolationOperation.FormatString), interpolationOperation.OwningSemanticModel, interpolationOperation.Syntax, interpolationOperation.IsImplicit);
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x0007B974 File Offset: 0x00079B74
		public override IOperation VisitConstantPattern(IConstantPatternOperation operation, [Nullable(2)] object argument)
		{
			ConstantPatternOperation constantPatternOperation = (ConstantPatternOperation)operation;
			return new ConstantPatternOperation(this.Visit(constantPatternOperation.Value), constantPatternOperation.InputType, constantPatternOperation.NarrowedType, constantPatternOperation.OwningSemanticModel, constantPatternOperation.Syntax, constantPatternOperation.IsImplicit);
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x0007B9B8 File Offset: 0x00079BB8
		public override IOperation VisitDeclarationPattern(IDeclarationPatternOperation operation, [Nullable(2)] object argument)
		{
			DeclarationPatternOperation declarationPatternOperation = (DeclarationPatternOperation)operation;
			return new DeclarationPatternOperation(declarationPatternOperation.MatchedType, declarationPatternOperation.MatchesNull, declarationPatternOperation.DeclaredSymbol, declarationPatternOperation.InputType, declarationPatternOperation.NarrowedType, declarationPatternOperation.OwningSemanticModel, declarationPatternOperation.Syntax, declarationPatternOperation.IsImplicit);
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x0007BA04 File Offset: 0x00079C04
		public override IOperation VisitTupleBinaryOperator(ITupleBinaryOperation operation, [Nullable(2)] object argument)
		{
			TupleBinaryOperation tupleBinaryOperation = (TupleBinaryOperation)operation;
			return new TupleBinaryOperation(tupleBinaryOperation.OperatorKind, this.Visit(tupleBinaryOperation.LeftOperand), this.Visit(tupleBinaryOperation.RightOperand), tupleBinaryOperation.OwningSemanticModel, tupleBinaryOperation.Syntax, tupleBinaryOperation.Type, tupleBinaryOperation.IsImplicit);
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x0007BA54 File Offset: 0x00079C54
		public override IOperation VisitMethodBodyOperation(IMethodBodyOperation operation, [Nullable(2)] object argument)
		{
			MethodBodyOperation methodBodyOperation = (MethodBodyOperation)operation;
			return new MethodBodyOperation(this.Visit<IBlockOperation>(methodBodyOperation.BlockBody), this.Visit<IBlockOperation>(methodBodyOperation.ExpressionBody), methodBodyOperation.OwningSemanticModel, methodBodyOperation.Syntax, methodBodyOperation.IsImplicit);
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x0007BA98 File Offset: 0x00079C98
		public override IOperation VisitConstructorBodyOperation(IConstructorBodyOperation operation, [Nullable(2)] object argument)
		{
			ConstructorBodyOperation constructorBodyOperation = (ConstructorBodyOperation)operation;
			return new ConstructorBodyOperation(constructorBodyOperation.Locals, this.Visit(constructorBodyOperation.Initializer), this.Visit<IBlockOperation>(constructorBodyOperation.BlockBody), this.Visit<IBlockOperation>(constructorBodyOperation.ExpressionBody), constructorBodyOperation.OwningSemanticModel, constructorBodyOperation.Syntax, constructorBodyOperation.IsImplicit);
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x0007BAF0 File Offset: 0x00079CF0
		public override IOperation VisitDiscardOperation(IDiscardOperation operation, [Nullable(2)] object argument)
		{
			DiscardOperation discardOperation = (DiscardOperation)operation;
			return new DiscardOperation(discardOperation.DiscardSymbol, discardOperation.OwningSemanticModel, discardOperation.Syntax, discardOperation.Type, discardOperation.IsImplicit);
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x0007BB28 File Offset: 0x00079D28
		public override IOperation VisitFlowCaptureReference(IFlowCaptureReferenceOperation operation, [Nullable(2)] object argument)
		{
			FlowCaptureReferenceOperation flowCaptureReferenceOperation = (FlowCaptureReferenceOperation)operation;
			return new FlowCaptureReferenceOperation(flowCaptureReferenceOperation.Id, flowCaptureReferenceOperation.OwningSemanticModel, flowCaptureReferenceOperation.Syntax, flowCaptureReferenceOperation.Type, flowCaptureReferenceOperation.OperationConstantValue, flowCaptureReferenceOperation.IsImplicit);
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x0007BB68 File Offset: 0x00079D68
		public override IOperation VisitCoalesceAssignment(ICoalesceAssignmentOperation operation, [Nullable(2)] object argument)
		{
			CoalesceAssignmentOperation coalesceAssignmentOperation = (CoalesceAssignmentOperation)operation;
			return new CoalesceAssignmentOperation(this.Visit(coalesceAssignmentOperation.Target), this.Visit(coalesceAssignmentOperation.Value), coalesceAssignmentOperation.OwningSemanticModel, coalesceAssignmentOperation.Syntax, coalesceAssignmentOperation.Type, coalesceAssignmentOperation.IsImplicit);
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x0007BBB4 File Offset: 0x00079DB4
		public override IOperation VisitRangeOperation(IRangeOperation operation, [Nullable(2)] object argument)
		{
			RangeOperation rangeOperation = (RangeOperation)operation;
			return new RangeOperation(this.Visit(rangeOperation.LeftOperand), this.Visit(rangeOperation.RightOperand), rangeOperation.IsLifted, rangeOperation.Method, rangeOperation.OwningSemanticModel, rangeOperation.Syntax, rangeOperation.Type, rangeOperation.IsImplicit);
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x0007BC0C File Offset: 0x00079E0C
		public override IOperation VisitReDim(IReDimOperation operation, [Nullable(2)] object argument)
		{
			ReDimOperation reDimOperation = (ReDimOperation)operation;
			return new ReDimOperation(this.VisitArray<IReDimClauseOperation>(reDimOperation.Clauses), reDimOperation.Preserve, reDimOperation.OwningSemanticModel, reDimOperation.Syntax, reDimOperation.IsImplicit);
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x0007BC4C File Offset: 0x00079E4C
		public override IOperation VisitReDimClause(IReDimClauseOperation operation, [Nullable(2)] object argument)
		{
			ReDimClauseOperation reDimClauseOperation = (ReDimClauseOperation)operation;
			return new ReDimClauseOperation(this.Visit(reDimClauseOperation.Operand), this.VisitArray<IOperation>(reDimClauseOperation.DimensionSizes), reDimClauseOperation.OwningSemanticModel, reDimClauseOperation.Syntax, reDimClauseOperation.IsImplicit);
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x0007BC90 File Offset: 0x00079E90
		public override IOperation VisitRecursivePattern(IRecursivePatternOperation operation, [Nullable(2)] object argument)
		{
			RecursivePatternOperation recursivePatternOperation = (RecursivePatternOperation)operation;
			return new RecursivePatternOperation(recursivePatternOperation.MatchedType, recursivePatternOperation.DeconstructSymbol, this.VisitArray<IPatternOperation>(recursivePatternOperation.DeconstructionSubpatterns), this.VisitArray<IPropertySubpatternOperation>(recursivePatternOperation.PropertySubpatterns), recursivePatternOperation.DeclaredSymbol, recursivePatternOperation.InputType, recursivePatternOperation.NarrowedType, recursivePatternOperation.OwningSemanticModel, recursivePatternOperation.Syntax, recursivePatternOperation.IsImplicit);
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x0007BCF4 File Offset: 0x00079EF4
		public override IOperation VisitDiscardPattern(IDiscardPatternOperation operation, [Nullable(2)] object argument)
		{
			DiscardPatternOperation discardPatternOperation = (DiscardPatternOperation)operation;
			return new DiscardPatternOperation(discardPatternOperation.InputType, discardPatternOperation.NarrowedType, discardPatternOperation.OwningSemanticModel, discardPatternOperation.Syntax, discardPatternOperation.IsImplicit);
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x0007BD2C File Offset: 0x00079F2C
		public override IOperation VisitSwitchExpression(ISwitchExpressionOperation operation, [Nullable(2)] object argument)
		{
			SwitchExpressionOperation switchExpressionOperation = (SwitchExpressionOperation)operation;
			return new SwitchExpressionOperation(this.Visit(switchExpressionOperation.Value), this.VisitArray<ISwitchExpressionArmOperation>(switchExpressionOperation.Arms), switchExpressionOperation.OwningSemanticModel, switchExpressionOperation.Syntax, switchExpressionOperation.Type, switchExpressionOperation.IsImplicit);
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x0007BD78 File Offset: 0x00079F78
		public override IOperation VisitSwitchExpressionArm(ISwitchExpressionArmOperation operation, [Nullable(2)] object argument)
		{
			SwitchExpressionArmOperation switchExpressionArmOperation = (SwitchExpressionArmOperation)operation;
			return new SwitchExpressionArmOperation(this.Visit<IPatternOperation>(switchExpressionArmOperation.Pattern), this.Visit(switchExpressionArmOperation.Guard), this.Visit(switchExpressionArmOperation.Value), switchExpressionArmOperation.Locals, switchExpressionArmOperation.OwningSemanticModel, switchExpressionArmOperation.Syntax, switchExpressionArmOperation.IsImplicit);
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x0007BDD0 File Offset: 0x00079FD0
		public override IOperation VisitPropertySubpattern(IPropertySubpatternOperation operation, [Nullable(2)] object argument)
		{
			PropertySubpatternOperation propertySubpatternOperation = (PropertySubpatternOperation)operation;
			return new PropertySubpatternOperation(this.Visit(propertySubpatternOperation.Member), this.Visit<IPatternOperation>(propertySubpatternOperation.Pattern), propertySubpatternOperation.OwningSemanticModel, propertySubpatternOperation.Syntax, propertySubpatternOperation.IsImplicit);
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x0007BE14 File Offset: 0x0007A014
		internal override IOperation VisitAggregateQuery(IAggregateQueryOperation operation, [Nullable(2)] object argument)
		{
			AggregateQueryOperation aggregateQueryOperation = (AggregateQueryOperation)operation;
			return new AggregateQueryOperation(this.Visit(aggregateQueryOperation.Group), this.Visit(aggregateQueryOperation.Aggregation), aggregateQueryOperation.OwningSemanticModel, aggregateQueryOperation.Syntax, aggregateQueryOperation.Type, aggregateQueryOperation.IsImplicit);
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x0007BE60 File Offset: 0x0007A060
		internal override IOperation VisitFixed(IFixedOperation operation, [Nullable(2)] object argument)
		{
			FixedOperation fixedOperation = (FixedOperation)operation;
			return new FixedOperation(fixedOperation.Locals, this.Visit<IVariableDeclarationGroupOperation>(fixedOperation.Variables), this.Visit(fixedOperation.Body), fixedOperation.OwningSemanticModel, fixedOperation.Syntax, fixedOperation.IsImplicit);
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x0007BEAC File Offset: 0x0007A0AC
		internal override IOperation VisitNoPiaObjectCreation(INoPiaObjectCreationOperation operation, [Nullable(2)] object argument)
		{
			NoPiaObjectCreationOperation noPiaObjectCreationOperation = (NoPiaObjectCreationOperation)operation;
			return new NoPiaObjectCreationOperation(this.Visit<IObjectOrCollectionInitializerOperation>(noPiaObjectCreationOperation.Initializer), noPiaObjectCreationOperation.OwningSemanticModel, noPiaObjectCreationOperation.Syntax, noPiaObjectCreationOperation.Type, noPiaObjectCreationOperation.IsImplicit);
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x0007BEEC File Offset: 0x0007A0EC
		internal override IOperation VisitPlaceholder(IPlaceholderOperation operation, [Nullable(2)] object argument)
		{
			PlaceholderOperation placeholderOperation = (PlaceholderOperation)operation;
			return new PlaceholderOperation(placeholderOperation.PlaceholderKind, placeholderOperation.OwningSemanticModel, placeholderOperation.Syntax, placeholderOperation.Type, placeholderOperation.IsImplicit);
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x0007BF24 File Offset: 0x0007A124
		internal override IOperation VisitWithStatement(IWithStatementOperation operation, [Nullable(2)] object argument)
		{
			WithStatementOperation withStatementOperation = (WithStatementOperation)operation;
			return new WithStatementOperation(this.Visit(withStatementOperation.Body), this.Visit(withStatementOperation.Value), withStatementOperation.OwningSemanticModel, withStatementOperation.Syntax, withStatementOperation.IsImplicit);
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x0007BF68 File Offset: 0x0007A168
		public override IOperation VisitUsingDeclaration(IUsingDeclarationOperation operation, [Nullable(2)] object argument)
		{
			UsingDeclarationOperation usingDeclarationOperation = (UsingDeclarationOperation)operation;
			return new UsingDeclarationOperation(this.Visit<IVariableDeclarationGroupOperation>(usingDeclarationOperation.DeclarationGroup), usingDeclarationOperation.IsAsynchronous, usingDeclarationOperation.DisposeInfo, usingDeclarationOperation.OwningSemanticModel, usingDeclarationOperation.Syntax, usingDeclarationOperation.IsImplicit);
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x0007BFAC File Offset: 0x0007A1AC
		public override IOperation VisitNegatedPattern(INegatedPatternOperation operation, [Nullable(2)] object argument)
		{
			NegatedPatternOperation negatedPatternOperation = (NegatedPatternOperation)operation;
			return new NegatedPatternOperation(this.Visit<IPatternOperation>(negatedPatternOperation.Pattern), negatedPatternOperation.InputType, negatedPatternOperation.NarrowedType, negatedPatternOperation.OwningSemanticModel, negatedPatternOperation.Syntax, negatedPatternOperation.IsImplicit);
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x0007BFF0 File Offset: 0x0007A1F0
		public override IOperation VisitBinaryPattern(IBinaryPatternOperation operation, [Nullable(2)] object argument)
		{
			BinaryPatternOperation binaryPatternOperation = (BinaryPatternOperation)operation;
			return new BinaryPatternOperation(binaryPatternOperation.OperatorKind, this.Visit<IPatternOperation>(binaryPatternOperation.LeftPattern), this.Visit<IPatternOperation>(binaryPatternOperation.RightPattern), binaryPatternOperation.InputType, binaryPatternOperation.NarrowedType, binaryPatternOperation.OwningSemanticModel, binaryPatternOperation.Syntax, binaryPatternOperation.IsImplicit);
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x0007C048 File Offset: 0x0007A248
		public override IOperation VisitTypePattern(ITypePatternOperation operation, [Nullable(2)] object argument)
		{
			TypePatternOperation typePatternOperation = (TypePatternOperation)operation;
			return new TypePatternOperation(typePatternOperation.MatchedType, typePatternOperation.InputType, typePatternOperation.NarrowedType, typePatternOperation.OwningSemanticModel, typePatternOperation.Syntax, typePatternOperation.IsImplicit);
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x0007C088 File Offset: 0x0007A288
		public override IOperation VisitRelationalPattern(IRelationalPatternOperation operation, [Nullable(2)] object argument)
		{
			RelationalPatternOperation relationalPatternOperation = (RelationalPatternOperation)operation;
			return new RelationalPatternOperation(relationalPatternOperation.OperatorKind, this.Visit(relationalPatternOperation.Value), relationalPatternOperation.InputType, relationalPatternOperation.NarrowedType, relationalPatternOperation.OwningSemanticModel, relationalPatternOperation.Syntax, relationalPatternOperation.IsImplicit);
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x0007C0D4 File Offset: 0x0007A2D4
		public override IOperation VisitWith(IWithOperation operation, [Nullable(2)] object argument)
		{
			WithOperation withOperation = (WithOperation)operation;
			return new WithOperation(this.Visit(withOperation.Operand), withOperation.CloneMethod, this.Visit<IObjectOrCollectionInitializerOperation>(withOperation.Initializer), withOperation.OwningSemanticModel, withOperation.Syntax, withOperation.Type, withOperation.IsImplicit);
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x0007C123 File Offset: 0x0007A323
		[NullableContext(2)]
		[return: NotNullIfNotNull("operation")]
		public IOperation Visit(IOperation operation)
		{
			return this.Visit(operation, null);
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x0007C130 File Offset: 0x0007A330
		internal override IOperation VisitNoneOperation(IOperation operation, [Nullable(2)] object argument)
		{
			return new NoneOperation(this.VisitArray<IOperation>(((Operation)operation).ChildOperations.ToImmutableArray()), ((Operation)operation).OwningSemanticModel, operation.Syntax, operation.Type, operation.GetConstantValue(), operation.IsImplicit);
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x0007C180 File Offset: 0x0007A380
		public override IOperation VisitFlowAnonymousFunction(IFlowAnonymousFunctionOperation operation, [Nullable(2)] object argument)
		{
			FlowAnonymousFunctionOperation flowAnonymousFunctionOperation = (FlowAnonymousFunctionOperation)operation;
			return new FlowAnonymousFunctionOperation(in flowAnonymousFunctionOperation.Context, flowAnonymousFunctionOperation.Original, operation.IsImplicit);
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x0007C1AC File Offset: 0x0007A3AC
		public override IOperation VisitDynamicObjectCreation(IDynamicObjectCreationOperation operation, [Nullable(2)] object argument)
		{
			return new DynamicObjectCreationOperation(this.Visit<IObjectOrCollectionInitializerOperation>(operation.Initializer), this.VisitArray<IOperation>(operation.Arguments), ((HasDynamicArgumentsExpression)operation).ArgumentNames, ((HasDynamicArgumentsExpression)operation).ArgumentRefKinds, ((Operation)operation).OwningSemanticModel, operation.Syntax, operation.Type, operation.IsImplicit);
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x0007C20C File Offset: 0x0007A40C
		public override IOperation VisitDynamicInvocation(IDynamicInvocationOperation operation, [Nullable(2)] object argument)
		{
			return new DynamicInvocationOperation(this.Visit(operation.Operation), this.VisitArray<IOperation>(operation.Arguments), ((HasDynamicArgumentsExpression)operation).ArgumentNames, ((HasDynamicArgumentsExpression)operation).ArgumentRefKinds, ((Operation)operation).OwningSemanticModel, operation.Syntax, operation.Type, operation.IsImplicit);
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x0007C26C File Offset: 0x0007A46C
		public override IOperation VisitDynamicIndexerAccess(IDynamicIndexerAccessOperation operation, [Nullable(2)] object argument)
		{
			return new DynamicIndexerAccessOperation(this.Visit(operation.Operation), this.VisitArray<IOperation>(operation.Arguments), ((HasDynamicArgumentsExpression)operation).ArgumentNames, ((HasDynamicArgumentsExpression)operation).ArgumentRefKinds, ((Operation)operation).OwningSemanticModel, operation.Syntax, operation.Type, operation.IsImplicit);
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x0007C2C9 File Offset: 0x0007A4C9
		public override IOperation VisitInvalid(IInvalidOperation operation, [Nullable(2)] object argument)
		{
			return new InvalidOperation(this.VisitArray<IOperation>(((InvalidOperation)operation).Children), ((Operation)operation).OwningSemanticModel, operation.Syntax, operation.Type, operation.GetConstantValue(), operation.IsImplicit);
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x0007C304 File Offset: 0x0007A504
		public override IOperation VisitFlowCapture(IFlowCaptureOperation operation, [Nullable(2)] object argument)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x0007C30B File Offset: 0x0007A50B
		public override IOperation VisitIsNull(IIsNullOperation operation, [Nullable(2)] object argument)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x0007C312 File Offset: 0x0007A512
		public override IOperation VisitCaughtException(ICaughtExceptionOperation operation, [Nullable(2)] object argument)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x0007C319 File Offset: 0x0007A519
		public override IOperation VisitStaticLocalInitializationSemaphore(IStaticLocalInitializationSemaphoreOperation operation, [Nullable(2)] object argument)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04001141 RID: 4417
		private static readonly OperationCloner s_instance = new OperationCloner();
	}
}
