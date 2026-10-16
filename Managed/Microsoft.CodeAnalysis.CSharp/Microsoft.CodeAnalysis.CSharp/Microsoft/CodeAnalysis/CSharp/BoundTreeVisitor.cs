using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000C3 RID: 195
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundTreeVisitor<[global::System.Runtime.CompilerServices.Nullable(2)] A, [global::System.Runtime.CompilerServices.Nullable(2)] R>
	{
		// Token: 0x06000E78 RID: 3704 RVA: 0x0005A3BC File Offset: 0x000585BC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public virtual R Visit(BoundNode node, A arg)
		{
			if (node == null)
			{
				return default(R);
			}
			BoundKind kind = node.Kind;
			if (kind <= BoundKind.SequencePointExpression)
			{
				if (kind <= BoundKind.TypeOfOperator)
				{
					switch (kind)
					{
					case BoundKind.TypeExpression:
						return this.VisitTypeExpression(node as BoundTypeExpression, arg);
					case BoundKind.TypeOrValueExpression:
						break;
					case BoundKind.NamespaceExpression:
						return this.VisitNamespaceExpression(node as BoundNamespaceExpression, arg);
					case BoundKind.UnaryOperator:
						return this.VisitUnaryOperator(node as BoundUnaryOperator, arg);
					case BoundKind.IncrementOperator:
						return this.VisitIncrementOperator(node as BoundIncrementOperator, arg);
					default:
						switch (kind)
						{
						case BoundKind.BinaryOperator:
							return this.VisitBinaryOperator(node as BoundBinaryOperator, arg);
						case BoundKind.TupleBinaryOperator:
						case BoundKind.UserDefinedConditionalLogicalOperator:
						case BoundKind.DeconstructionAssignmentOperator:
						case BoundKind.NullCoalescingAssignmentOperator:
						case BoundKind.UnconvertedConditionalOperator:
							break;
						case BoundKind.CompoundAssignmentOperator:
							return this.VisitCompoundAssignmentOperator(node as BoundCompoundAssignmentOperator, arg);
						case BoundKind.AssignmentOperator:
							return this.VisitAssignmentOperator(node as BoundAssignmentOperator, arg);
						case BoundKind.NullCoalescingOperator:
							return this.VisitNullCoalescingOperator(node as BoundNullCoalescingOperator, arg);
						case BoundKind.ConditionalOperator:
							return this.VisitConditionalOperator(node as BoundConditionalOperator, arg);
						case BoundKind.ArrayAccess:
							return this.VisitArrayAccess(node as BoundArrayAccess, arg);
						default:
							if (kind == BoundKind.TypeOfOperator)
							{
								return this.VisitTypeOfOperator(node as BoundTypeOfOperator, arg);
							}
							break;
						}
						break;
					}
				}
				else if (kind <= BoundKind.ForEachStatement)
				{
					switch (kind)
					{
					case BoundKind.DefaultLiteral:
						return this.VisitDefaultLiteral(node as BoundDefaultLiteral, arg);
					case BoundKind.DefaultExpression:
						return this.VisitDefaultExpression(node as BoundDefaultExpression, arg);
					case BoundKind.IsOperator:
						return this.VisitIsOperator(node as BoundIsOperator, arg);
					case BoundKind.AsOperator:
						return this.VisitAsOperator(node as BoundAsOperator, arg);
					case BoundKind.SizeOfOperator:
					case BoundKind.ReadOnlySpanFromArray:
					case BoundKind.ArgList:
					case BoundKind.ArgListOperator:
					case BoundKind.FixedLocalCollectionInitializer:
					case BoundKind.SavePreviousSequencePoint:
					case BoundKind.RestorePreviousSequencePoint:
					case BoundKind.StepThroughSequencePoint:
					case BoundKind.Scope:
					case BoundKind.StateMachineScope:
					case BoundKind.UsingLocalDeclarations:
					case BoundKind.LocalFunctionStatement:
					case BoundKind.YieldReturnStatement:
					case BoundKind.YieldBreakStatement:
					case BoundKind.SwitchStatement:
					case BoundKind.SwitchDispatch:
						break;
					case BoundKind.Conversion:
						return this.VisitConversion(node as BoundConversion, arg);
					case BoundKind.SequencePoint:
						return this.VisitSequencePoint(node as BoundSequencePoint, arg);
					case BoundKind.SequencePointWithSpan:
						return this.VisitSequencePointWithSpan(node as BoundSequencePointWithSpan, arg);
					case BoundKind.Block:
						return this.VisitBlock(node as BoundBlock, arg);
					case BoundKind.LocalDeclaration:
						return this.VisitLocalDeclaration(node as BoundLocalDeclaration, arg);
					case BoundKind.MultipleLocalDeclarations:
						return this.VisitMultipleLocalDeclarations(node as BoundMultipleLocalDeclarations, arg);
					case BoundKind.NoOpStatement:
						return this.VisitNoOpStatement(node as BoundNoOpStatement, arg);
					case BoundKind.ReturnStatement:
						return this.VisitReturnStatement(node as BoundReturnStatement, arg);
					case BoundKind.ThrowStatement:
						return this.VisitThrowStatement(node as BoundThrowStatement, arg);
					case BoundKind.ExpressionStatement:
						return this.VisitExpressionStatement(node as BoundExpressionStatement, arg);
					case BoundKind.BreakStatement:
						return this.VisitBreakStatement(node as BoundBreakStatement, arg);
					case BoundKind.ContinueStatement:
						return this.VisitContinueStatement(node as BoundContinueStatement, arg);
					case BoundKind.IfStatement:
						return this.VisitIfStatement(node as BoundIfStatement, arg);
					default:
						if (kind == BoundKind.ForEachStatement)
						{
							return this.VisitForEachStatement(node as BoundForEachStatement, arg);
						}
						break;
					}
				}
				else
				{
					switch (kind)
					{
					case BoundKind.TryStatement:
						return this.VisitTryStatement(node as BoundTryStatement, arg);
					case BoundKind.CatchBlock:
					case BoundKind.PreviousSubmissionReference:
					case BoundKind.HostObjectMemberReference:
					case BoundKind.BaseReference:
					case BoundKind.PseudoVariable:
					case BoundKind.RangeVariable:
					case BoundKind.Label:
						break;
					case BoundKind.Literal:
						return this.VisitLiteral(node as BoundLiteral, arg);
					case BoundKind.ThisReference:
						return this.VisitThisReference(node as BoundThisReference, arg);
					case BoundKind.Local:
						return this.VisitLocal(node as BoundLocal, arg);
					case BoundKind.Parameter:
						return this.VisitParameter(node as BoundParameter, arg);
					case BoundKind.LabelStatement:
						return this.VisitLabelStatement(node as BoundLabelStatement, arg);
					case BoundKind.GotoStatement:
						return this.VisitGotoStatement(node as BoundGotoStatement, arg);
					case BoundKind.LabeledStatement:
						return this.VisitLabeledStatement(node as BoundLabeledStatement, arg);
					case BoundKind.StatementList:
						return this.VisitStatementList(node as BoundStatementList, arg);
					case BoundKind.ConditionalGoto:
						return this.VisitConditionalGoto(node as BoundConditionalGoto, arg);
					default:
						if (kind == BoundKind.SequencePointExpression)
						{
							return this.VisitSequencePointExpression(node as BoundSequencePointExpression, arg);
						}
						break;
					}
				}
			}
			else if (kind <= BoundKind.DelegateCreationExpression)
			{
				if (kind <= BoundKind.Call)
				{
					if (kind == BoundKind.Sequence)
					{
						return this.VisitSequence(node as BoundSequence, arg);
					}
					if (kind == BoundKind.Call)
					{
						return this.VisitCall(node as BoundCall, arg);
					}
				}
				else
				{
					if (kind == BoundKind.ObjectCreationExpression)
					{
						return this.VisitObjectCreationExpression(node as BoundObjectCreationExpression, arg);
					}
					if (kind == BoundKind.DelegateCreationExpression)
					{
						return this.VisitDelegateCreationExpression(node as BoundDelegateCreationExpression, arg);
					}
				}
			}
			else if (kind <= BoundKind.PropertyAccess)
			{
				if (kind == BoundKind.FieldAccess)
				{
					return this.VisitFieldAccess(node as BoundFieldAccess, arg);
				}
				if (kind == BoundKind.PropertyAccess)
				{
					return this.VisitPropertyAccess(node as BoundPropertyAccess, arg);
				}
			}
			else
			{
				if (kind == BoundKind.Lambda)
				{
					return this.VisitLambda(node as BoundLambda, arg);
				}
				if (kind == BoundKind.NameOfOperator)
				{
					return this.VisitNameOfOperator(node as BoundNameOfOperator, arg);
				}
			}
			return this.VisitInternal(node, arg);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0005A878 File Offset: 0x00058A78
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public virtual R DefaultVisit(BoundNode node, A arg)
		{
			return default(R);
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0005A890 File Offset: 0x00058A90
		[DebuggerStepThrough]
		[MethodImpl(8)]
		internal R VisitInternal(BoundNode node, A arg)
		{
			switch (node.Kind)
			{
			case BoundKind.FieldEqualsValue:
				return this.VisitFieldEqualsValue((BoundFieldEqualsValue)node, arg);
			case BoundKind.PropertyEqualsValue:
				return this.VisitPropertyEqualsValue((BoundPropertyEqualsValue)node, arg);
			case BoundKind.ParameterEqualsValue:
				return this.VisitParameterEqualsValue((BoundParameterEqualsValue)node, arg);
			case BoundKind.GlobalStatementInitializer:
				return this.VisitGlobalStatementInitializer((BoundGlobalStatementInitializer)node, arg);
			case BoundKind.DeconstructValuePlaceholder:
				return this.VisitDeconstructValuePlaceholder((BoundDeconstructValuePlaceholder)node, arg);
			case BoundKind.TupleOperandPlaceholder:
				return this.VisitTupleOperandPlaceholder((BoundTupleOperandPlaceholder)node, arg);
			case BoundKind.AwaitableValuePlaceholder:
				return this.VisitAwaitableValuePlaceholder((BoundAwaitableValuePlaceholder)node, arg);
			case BoundKind.DisposableValuePlaceholder:
				return this.VisitDisposableValuePlaceholder((BoundDisposableValuePlaceholder)node, arg);
			case BoundKind.ObjectOrCollectionValuePlaceholder:
				return this.VisitObjectOrCollectionValuePlaceholder((BoundObjectOrCollectionValuePlaceholder)node, arg);
			case BoundKind.Dup:
				return this.VisitDup((BoundDup)node, arg);
			case BoundKind.PassByCopy:
				return this.VisitPassByCopy((BoundPassByCopy)node, arg);
			case BoundKind.BadExpression:
				return this.VisitBadExpression((BoundBadExpression)node, arg);
			case BoundKind.BadStatement:
				return this.VisitBadStatement((BoundBadStatement)node, arg);
			case BoundKind.ExtractedFinallyBlock:
				return this.VisitExtractedFinallyBlock((BoundExtractedFinallyBlock)node, arg);
			case BoundKind.TypeExpression:
				return this.VisitTypeExpression((BoundTypeExpression)node, arg);
			case BoundKind.TypeOrValueExpression:
				return this.VisitTypeOrValueExpression((BoundTypeOrValueExpression)node, arg);
			case BoundKind.NamespaceExpression:
				return this.VisitNamespaceExpression((BoundNamespaceExpression)node, arg);
			case BoundKind.UnaryOperator:
				return this.VisitUnaryOperator((BoundUnaryOperator)node, arg);
			case BoundKind.IncrementOperator:
				return this.VisitIncrementOperator((BoundIncrementOperator)node, arg);
			case BoundKind.AddressOfOperator:
				return this.VisitAddressOfOperator((BoundAddressOfOperator)node, arg);
			case BoundKind.UnconvertedAddressOfOperator:
				return this.VisitUnconvertedAddressOfOperator((BoundUnconvertedAddressOfOperator)node, arg);
			case BoundKind.FunctionPointerLoad:
				return this.VisitFunctionPointerLoad((BoundFunctionPointerLoad)node, arg);
			case BoundKind.PointerIndirectionOperator:
				return this.VisitPointerIndirectionOperator((BoundPointerIndirectionOperator)node, arg);
			case BoundKind.PointerElementAccess:
				return this.VisitPointerElementAccess((BoundPointerElementAccess)node, arg);
			case BoundKind.FunctionPointerInvocation:
				return this.VisitFunctionPointerInvocation((BoundFunctionPointerInvocation)node, arg);
			case BoundKind.RefTypeOperator:
				return this.VisitRefTypeOperator((BoundRefTypeOperator)node, arg);
			case BoundKind.MakeRefOperator:
				return this.VisitMakeRefOperator((BoundMakeRefOperator)node, arg);
			case BoundKind.RefValueOperator:
				return this.VisitRefValueOperator((BoundRefValueOperator)node, arg);
			case BoundKind.FromEndIndexExpression:
				return this.VisitFromEndIndexExpression((BoundFromEndIndexExpression)node, arg);
			case BoundKind.RangeExpression:
				return this.VisitRangeExpression((BoundRangeExpression)node, arg);
			case BoundKind.BinaryOperator:
				return this.VisitBinaryOperator((BoundBinaryOperator)node, arg);
			case BoundKind.TupleBinaryOperator:
				return this.VisitTupleBinaryOperator((BoundTupleBinaryOperator)node, arg);
			case BoundKind.UserDefinedConditionalLogicalOperator:
				return this.VisitUserDefinedConditionalLogicalOperator((BoundUserDefinedConditionalLogicalOperator)node, arg);
			case BoundKind.CompoundAssignmentOperator:
				return this.VisitCompoundAssignmentOperator((BoundCompoundAssignmentOperator)node, arg);
			case BoundKind.AssignmentOperator:
				return this.VisitAssignmentOperator((BoundAssignmentOperator)node, arg);
			case BoundKind.DeconstructionAssignmentOperator:
				return this.VisitDeconstructionAssignmentOperator((BoundDeconstructionAssignmentOperator)node, arg);
			case BoundKind.NullCoalescingOperator:
				return this.VisitNullCoalescingOperator((BoundNullCoalescingOperator)node, arg);
			case BoundKind.NullCoalescingAssignmentOperator:
				return this.VisitNullCoalescingAssignmentOperator((BoundNullCoalescingAssignmentOperator)node, arg);
			case BoundKind.UnconvertedConditionalOperator:
				return this.VisitUnconvertedConditionalOperator((BoundUnconvertedConditionalOperator)node, arg);
			case BoundKind.ConditionalOperator:
				return this.VisitConditionalOperator((BoundConditionalOperator)node, arg);
			case BoundKind.ArrayAccess:
				return this.VisitArrayAccess((BoundArrayAccess)node, arg);
			case BoundKind.ArrayLength:
				return this.VisitArrayLength((BoundArrayLength)node, arg);
			case BoundKind.AwaitableInfo:
				return this.VisitAwaitableInfo((BoundAwaitableInfo)node, arg);
			case BoundKind.AwaitExpression:
				return this.VisitAwaitExpression((BoundAwaitExpression)node, arg);
			case BoundKind.TypeOfOperator:
				return this.VisitTypeOfOperator((BoundTypeOfOperator)node, arg);
			case BoundKind.MethodDefIndex:
				return this.VisitMethodDefIndex((BoundMethodDefIndex)node, arg);
			case BoundKind.MaximumMethodDefIndex:
				return this.VisitMaximumMethodDefIndex((BoundMaximumMethodDefIndex)node, arg);
			case BoundKind.InstrumentationPayloadRoot:
				return this.VisitInstrumentationPayloadRoot((BoundInstrumentationPayloadRoot)node, arg);
			case BoundKind.ModuleVersionId:
				return this.VisitModuleVersionId((BoundModuleVersionId)node, arg);
			case BoundKind.ModuleVersionIdString:
				return this.VisitModuleVersionIdString((BoundModuleVersionIdString)node, arg);
			case BoundKind.SourceDocumentIndex:
				return this.VisitSourceDocumentIndex((BoundSourceDocumentIndex)node, arg);
			case BoundKind.MethodInfo:
				return this.VisitMethodInfo((BoundMethodInfo)node, arg);
			case BoundKind.FieldInfo:
				return this.VisitFieldInfo((BoundFieldInfo)node, arg);
			case BoundKind.DefaultLiteral:
				return this.VisitDefaultLiteral((BoundDefaultLiteral)node, arg);
			case BoundKind.DefaultExpression:
				return this.VisitDefaultExpression((BoundDefaultExpression)node, arg);
			case BoundKind.IsOperator:
				return this.VisitIsOperator((BoundIsOperator)node, arg);
			case BoundKind.AsOperator:
				return this.VisitAsOperator((BoundAsOperator)node, arg);
			case BoundKind.SizeOfOperator:
				return this.VisitSizeOfOperator((BoundSizeOfOperator)node, arg);
			case BoundKind.Conversion:
				return this.VisitConversion((BoundConversion)node, arg);
			case BoundKind.ReadOnlySpanFromArray:
				return this.VisitReadOnlySpanFromArray((BoundReadOnlySpanFromArray)node, arg);
			case BoundKind.ArgList:
				return this.VisitArgList((BoundArgList)node, arg);
			case BoundKind.ArgListOperator:
				return this.VisitArgListOperator((BoundArgListOperator)node, arg);
			case BoundKind.FixedLocalCollectionInitializer:
				return this.VisitFixedLocalCollectionInitializer((BoundFixedLocalCollectionInitializer)node, arg);
			case BoundKind.SequencePoint:
				return this.VisitSequencePoint((BoundSequencePoint)node, arg);
			case BoundKind.SequencePointWithSpan:
				return this.VisitSequencePointWithSpan((BoundSequencePointWithSpan)node, arg);
			case BoundKind.SavePreviousSequencePoint:
				return this.VisitSavePreviousSequencePoint((BoundSavePreviousSequencePoint)node, arg);
			case BoundKind.RestorePreviousSequencePoint:
				return this.VisitRestorePreviousSequencePoint((BoundRestorePreviousSequencePoint)node, arg);
			case BoundKind.StepThroughSequencePoint:
				return this.VisitStepThroughSequencePoint((BoundStepThroughSequencePoint)node, arg);
			case BoundKind.Block:
				return this.VisitBlock((BoundBlock)node, arg);
			case BoundKind.Scope:
				return this.VisitScope((BoundScope)node, arg);
			case BoundKind.StateMachineScope:
				return this.VisitStateMachineScope((BoundStateMachineScope)node, arg);
			case BoundKind.LocalDeclaration:
				return this.VisitLocalDeclaration((BoundLocalDeclaration)node, arg);
			case BoundKind.MultipleLocalDeclarations:
				return this.VisitMultipleLocalDeclarations((BoundMultipleLocalDeclarations)node, arg);
			case BoundKind.UsingLocalDeclarations:
				return this.VisitUsingLocalDeclarations((BoundUsingLocalDeclarations)node, arg);
			case BoundKind.LocalFunctionStatement:
				return this.VisitLocalFunctionStatement((BoundLocalFunctionStatement)node, arg);
			case BoundKind.NoOpStatement:
				return this.VisitNoOpStatement((BoundNoOpStatement)node, arg);
			case BoundKind.ReturnStatement:
				return this.VisitReturnStatement((BoundReturnStatement)node, arg);
			case BoundKind.YieldReturnStatement:
				return this.VisitYieldReturnStatement((BoundYieldReturnStatement)node, arg);
			case BoundKind.YieldBreakStatement:
				return this.VisitYieldBreakStatement((BoundYieldBreakStatement)node, arg);
			case BoundKind.ThrowStatement:
				return this.VisitThrowStatement((BoundThrowStatement)node, arg);
			case BoundKind.ExpressionStatement:
				return this.VisitExpressionStatement((BoundExpressionStatement)node, arg);
			case BoundKind.BreakStatement:
				return this.VisitBreakStatement((BoundBreakStatement)node, arg);
			case BoundKind.ContinueStatement:
				return this.VisitContinueStatement((BoundContinueStatement)node, arg);
			case BoundKind.SwitchStatement:
				return this.VisitSwitchStatement((BoundSwitchStatement)node, arg);
			case BoundKind.SwitchDispatch:
				return this.VisitSwitchDispatch((BoundSwitchDispatch)node, arg);
			case BoundKind.IfStatement:
				return this.VisitIfStatement((BoundIfStatement)node, arg);
			case BoundKind.DoStatement:
				return this.VisitDoStatement((BoundDoStatement)node, arg);
			case BoundKind.WhileStatement:
				return this.VisitWhileStatement((BoundWhileStatement)node, arg);
			case BoundKind.ForStatement:
				return this.VisitForStatement((BoundForStatement)node, arg);
			case BoundKind.ForEachStatement:
				return this.VisitForEachStatement((BoundForEachStatement)node, arg);
			case BoundKind.ForEachDeconstructStep:
				return this.VisitForEachDeconstructStep((BoundForEachDeconstructStep)node, arg);
			case BoundKind.UsingStatement:
				return this.VisitUsingStatement((BoundUsingStatement)node, arg);
			case BoundKind.FixedStatement:
				return this.VisitFixedStatement((BoundFixedStatement)node, arg);
			case BoundKind.LockStatement:
				return this.VisitLockStatement((BoundLockStatement)node, arg);
			case BoundKind.TryStatement:
				return this.VisitTryStatement((BoundTryStatement)node, arg);
			case BoundKind.CatchBlock:
				return this.VisitCatchBlock((BoundCatchBlock)node, arg);
			case BoundKind.Literal:
				return this.VisitLiteral((BoundLiteral)node, arg);
			case BoundKind.ThisReference:
				return this.VisitThisReference((BoundThisReference)node, arg);
			case BoundKind.PreviousSubmissionReference:
				return this.VisitPreviousSubmissionReference((BoundPreviousSubmissionReference)node, arg);
			case BoundKind.HostObjectMemberReference:
				return this.VisitHostObjectMemberReference((BoundHostObjectMemberReference)node, arg);
			case BoundKind.BaseReference:
				return this.VisitBaseReference((BoundBaseReference)node, arg);
			case BoundKind.Local:
				return this.VisitLocal((BoundLocal)node, arg);
			case BoundKind.PseudoVariable:
				return this.VisitPseudoVariable((BoundPseudoVariable)node, arg);
			case BoundKind.RangeVariable:
				return this.VisitRangeVariable((BoundRangeVariable)node, arg);
			case BoundKind.Parameter:
				return this.VisitParameter((BoundParameter)node, arg);
			case BoundKind.LabelStatement:
				return this.VisitLabelStatement((BoundLabelStatement)node, arg);
			case BoundKind.GotoStatement:
				return this.VisitGotoStatement((BoundGotoStatement)node, arg);
			case BoundKind.LabeledStatement:
				return this.VisitLabeledStatement((BoundLabeledStatement)node, arg);
			case BoundKind.Label:
				return this.VisitLabel((BoundLabel)node, arg);
			case BoundKind.StatementList:
				return this.VisitStatementList((BoundStatementList)node, arg);
			case BoundKind.ConditionalGoto:
				return this.VisitConditionalGoto((BoundConditionalGoto)node, arg);
			case BoundKind.SwitchExpressionArm:
				return this.VisitSwitchExpressionArm((BoundSwitchExpressionArm)node, arg);
			case BoundKind.UnconvertedSwitchExpression:
				return this.VisitUnconvertedSwitchExpression((BoundUnconvertedSwitchExpression)node, arg);
			case BoundKind.ConvertedSwitchExpression:
				return this.VisitConvertedSwitchExpression((BoundConvertedSwitchExpression)node, arg);
			case BoundKind.DecisionDag:
				return this.VisitDecisionDag((BoundDecisionDag)node, arg);
			case BoundKind.EvaluationDecisionDagNode:
				return this.VisitEvaluationDecisionDagNode((BoundEvaluationDecisionDagNode)node, arg);
			case BoundKind.TestDecisionDagNode:
				return this.VisitTestDecisionDagNode((BoundTestDecisionDagNode)node, arg);
			case BoundKind.WhenDecisionDagNode:
				return this.VisitWhenDecisionDagNode((BoundWhenDecisionDagNode)node, arg);
			case BoundKind.LeafDecisionDagNode:
				return this.VisitLeafDecisionDagNode((BoundLeafDecisionDagNode)node, arg);
			case BoundKind.DagTemp:
				return this.VisitDagTemp((BoundDagTemp)node, arg);
			case BoundKind.DagTypeTest:
				return this.VisitDagTypeTest((BoundDagTypeTest)node, arg);
			case BoundKind.DagNonNullTest:
				return this.VisitDagNonNullTest((BoundDagNonNullTest)node, arg);
			case BoundKind.DagExplicitNullTest:
				return this.VisitDagExplicitNullTest((BoundDagExplicitNullTest)node, arg);
			case BoundKind.DagValueTest:
				return this.VisitDagValueTest((BoundDagValueTest)node, arg);
			case BoundKind.DagRelationalTest:
				return this.VisitDagRelationalTest((BoundDagRelationalTest)node, arg);
			case BoundKind.DagDeconstructEvaluation:
				return this.VisitDagDeconstructEvaluation((BoundDagDeconstructEvaluation)node, arg);
			case BoundKind.DagTypeEvaluation:
				return this.VisitDagTypeEvaluation((BoundDagTypeEvaluation)node, arg);
			case BoundKind.DagFieldEvaluation:
				return this.VisitDagFieldEvaluation((BoundDagFieldEvaluation)node, arg);
			case BoundKind.DagPropertyEvaluation:
				return this.VisitDagPropertyEvaluation((BoundDagPropertyEvaluation)node, arg);
			case BoundKind.DagIndexEvaluation:
				return this.VisitDagIndexEvaluation((BoundDagIndexEvaluation)node, arg);
			case BoundKind.SwitchSection:
				return this.VisitSwitchSection((BoundSwitchSection)node, arg);
			case BoundKind.SwitchLabel:
				return this.VisitSwitchLabel((BoundSwitchLabel)node, arg);
			case BoundKind.SequencePointExpression:
				return this.VisitSequencePointExpression((BoundSequencePointExpression)node, arg);
			case BoundKind.Sequence:
				return this.VisitSequence((BoundSequence)node, arg);
			case BoundKind.SpillSequence:
				return this.VisitSpillSequence((BoundSpillSequence)node, arg);
			case BoundKind.DynamicMemberAccess:
				return this.VisitDynamicMemberAccess((BoundDynamicMemberAccess)node, arg);
			case BoundKind.DynamicInvocation:
				return this.VisitDynamicInvocation((BoundDynamicInvocation)node, arg);
			case BoundKind.ConditionalAccess:
				return this.VisitConditionalAccess((BoundConditionalAccess)node, arg);
			case BoundKind.LoweredConditionalAccess:
				return this.VisitLoweredConditionalAccess((BoundLoweredConditionalAccess)node, arg);
			case BoundKind.ConditionalReceiver:
				return this.VisitConditionalReceiver((BoundConditionalReceiver)node, arg);
			case BoundKind.ComplexConditionalReceiver:
				return this.VisitComplexConditionalReceiver((BoundComplexConditionalReceiver)node, arg);
			case BoundKind.MethodGroup:
				return this.VisitMethodGroup((BoundMethodGroup)node, arg);
			case BoundKind.PropertyGroup:
				return this.VisitPropertyGroup((BoundPropertyGroup)node, arg);
			case BoundKind.Call:
				return this.VisitCall((BoundCall)node, arg);
			case BoundKind.EventAssignmentOperator:
				return this.VisitEventAssignmentOperator((BoundEventAssignmentOperator)node, arg);
			case BoundKind.Attribute:
				return this.VisitAttribute((BoundAttribute)node, arg);
			case BoundKind.UnconvertedObjectCreationExpression:
				return this.VisitUnconvertedObjectCreationExpression((BoundUnconvertedObjectCreationExpression)node, arg);
			case BoundKind.ObjectCreationExpression:
				return this.VisitObjectCreationExpression((BoundObjectCreationExpression)node, arg);
			case BoundKind.TupleLiteral:
				return this.VisitTupleLiteral((BoundTupleLiteral)node, arg);
			case BoundKind.ConvertedTupleLiteral:
				return this.VisitConvertedTupleLiteral((BoundConvertedTupleLiteral)node, arg);
			case BoundKind.DynamicObjectCreationExpression:
				return this.VisitDynamicObjectCreationExpression((BoundDynamicObjectCreationExpression)node, arg);
			case BoundKind.NoPiaObjectCreationExpression:
				return this.VisitNoPiaObjectCreationExpression((BoundNoPiaObjectCreationExpression)node, arg);
			case BoundKind.ObjectInitializerExpression:
				return this.VisitObjectInitializerExpression((BoundObjectInitializerExpression)node, arg);
			case BoundKind.ObjectInitializerMember:
				return this.VisitObjectInitializerMember((BoundObjectInitializerMember)node, arg);
			case BoundKind.DynamicObjectInitializerMember:
				return this.VisitDynamicObjectInitializerMember((BoundDynamicObjectInitializerMember)node, arg);
			case BoundKind.CollectionInitializerExpression:
				return this.VisitCollectionInitializerExpression((BoundCollectionInitializerExpression)node, arg);
			case BoundKind.CollectionElementInitializer:
				return this.VisitCollectionElementInitializer((BoundCollectionElementInitializer)node, arg);
			case BoundKind.DynamicCollectionElementInitializer:
				return this.VisitDynamicCollectionElementInitializer((BoundDynamicCollectionElementInitializer)node, arg);
			case BoundKind.ImplicitReceiver:
				return this.VisitImplicitReceiver((BoundImplicitReceiver)node, arg);
			case BoundKind.AnonymousObjectCreationExpression:
				return this.VisitAnonymousObjectCreationExpression((BoundAnonymousObjectCreationExpression)node, arg);
			case BoundKind.AnonymousPropertyDeclaration:
				return this.VisitAnonymousPropertyDeclaration((BoundAnonymousPropertyDeclaration)node, arg);
			case BoundKind.NewT:
				return this.VisitNewT((BoundNewT)node, arg);
			case BoundKind.DelegateCreationExpression:
				return this.VisitDelegateCreationExpression((BoundDelegateCreationExpression)node, arg);
			case BoundKind.ArrayCreation:
				return this.VisitArrayCreation((BoundArrayCreation)node, arg);
			case BoundKind.ArrayInitialization:
				return this.VisitArrayInitialization((BoundArrayInitialization)node, arg);
			case BoundKind.StackAllocArrayCreation:
				return this.VisitStackAllocArrayCreation((BoundStackAllocArrayCreation)node, arg);
			case BoundKind.ConvertedStackAllocExpression:
				return this.VisitConvertedStackAllocExpression((BoundConvertedStackAllocExpression)node, arg);
			case BoundKind.FieldAccess:
				return this.VisitFieldAccess((BoundFieldAccess)node, arg);
			case BoundKind.HoistedFieldAccess:
				return this.VisitHoistedFieldAccess((BoundHoistedFieldAccess)node, arg);
			case BoundKind.PropertyAccess:
				return this.VisitPropertyAccess((BoundPropertyAccess)node, arg);
			case BoundKind.EventAccess:
				return this.VisitEventAccess((BoundEventAccess)node, arg);
			case BoundKind.IndexerAccess:
				return this.VisitIndexerAccess((BoundIndexerAccess)node, arg);
			case BoundKind.IndexOrRangePatternIndexerAccess:
				return this.VisitIndexOrRangePatternIndexerAccess((BoundIndexOrRangePatternIndexerAccess)node, arg);
			case BoundKind.DynamicIndexerAccess:
				return this.VisitDynamicIndexerAccess((BoundDynamicIndexerAccess)node, arg);
			case BoundKind.Lambda:
				return this.VisitLambda((BoundLambda)node, arg);
			case BoundKind.UnboundLambda:
				return this.VisitUnboundLambda((UnboundLambda)node, arg);
			case BoundKind.QueryClause:
				return this.VisitQueryClause((BoundQueryClause)node, arg);
			case BoundKind.TypeOrInstanceInitializers:
				return this.VisitTypeOrInstanceInitializers((BoundTypeOrInstanceInitializers)node, arg);
			case BoundKind.NameOfOperator:
				return this.VisitNameOfOperator((BoundNameOfOperator)node, arg);
			case BoundKind.UnconvertedInterpolatedString:
				return this.VisitUnconvertedInterpolatedString((BoundUnconvertedInterpolatedString)node, arg);
			case BoundKind.InterpolatedString:
				return this.VisitInterpolatedString((BoundInterpolatedString)node, arg);
			case BoundKind.StringInsert:
				return this.VisitStringInsert((BoundStringInsert)node, arg);
			case BoundKind.IsPatternExpression:
				return this.VisitIsPatternExpression((BoundIsPatternExpression)node, arg);
			case BoundKind.ConstantPattern:
				return this.VisitConstantPattern((BoundConstantPattern)node, arg);
			case BoundKind.DiscardPattern:
				return this.VisitDiscardPattern((BoundDiscardPattern)node, arg);
			case BoundKind.DeclarationPattern:
				return this.VisitDeclarationPattern((BoundDeclarationPattern)node, arg);
			case BoundKind.RecursivePattern:
				return this.VisitRecursivePattern((BoundRecursivePattern)node, arg);
			case BoundKind.ITuplePattern:
				return this.VisitITuplePattern((BoundITuplePattern)node, arg);
			case BoundKind.Subpattern:
				return this.VisitSubpattern((BoundSubpattern)node, arg);
			case BoundKind.TypePattern:
				return this.VisitTypePattern((BoundTypePattern)node, arg);
			case BoundKind.BinaryPattern:
				return this.VisitBinaryPattern((BoundBinaryPattern)node, arg);
			case BoundKind.NegatedPattern:
				return this.VisitNegatedPattern((BoundNegatedPattern)node, arg);
			case BoundKind.RelationalPattern:
				return this.VisitRelationalPattern((BoundRelationalPattern)node, arg);
			case BoundKind.DiscardExpression:
				return this.VisitDiscardExpression((BoundDiscardExpression)node, arg);
			case BoundKind.ThrowExpression:
				return this.VisitThrowExpression((BoundThrowExpression)node, arg);
			case BoundKind.OutVariablePendingInference:
				return this.VisitOutVariablePendingInference((OutVariablePendingInference)node, arg);
			case BoundKind.DeconstructionVariablePendingInference:
				return this.VisitDeconstructionVariablePendingInference((DeconstructionVariablePendingInference)node, arg);
			case BoundKind.OutDeconstructVarPendingInference:
				return this.VisitOutDeconstructVarPendingInference((OutDeconstructVarPendingInference)node, arg);
			case BoundKind.NonConstructorMethodBody:
				return this.VisitNonConstructorMethodBody((BoundNonConstructorMethodBody)node, arg);
			case BoundKind.ConstructorMethodBody:
				return this.VisitConstructorMethodBody((BoundConstructorMethodBody)node, arg);
			case BoundKind.ExpressionWithNullability:
				return this.VisitExpressionWithNullability((BoundExpressionWithNullability)node, arg);
			case BoundKind.WithExpression:
				return this.VisitWithExpression((BoundWithExpression)node, arg);
			default:
				return default(R);
			}
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0005B6EC File Offset: 0x000598EC
		public virtual R VisitFieldEqualsValue(BoundFieldEqualsValue node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0005B6F6 File Offset: 0x000598F6
		public virtual R VisitPropertyEqualsValue(BoundPropertyEqualsValue node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0005B700 File Offset: 0x00059900
		public virtual R VisitParameterEqualsValue(BoundParameterEqualsValue node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0005B70A File Offset: 0x0005990A
		public virtual R VisitGlobalStatementInitializer(BoundGlobalStatementInitializer node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0005B714 File Offset: 0x00059914
		public virtual R VisitDeconstructValuePlaceholder(BoundDeconstructValuePlaceholder node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0005B71E File Offset: 0x0005991E
		public virtual R VisitTupleOperandPlaceholder(BoundTupleOperandPlaceholder node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0005B728 File Offset: 0x00059928
		public virtual R VisitAwaitableValuePlaceholder(BoundAwaitableValuePlaceholder node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0005B732 File Offset: 0x00059932
		public virtual R VisitDisposableValuePlaceholder(BoundDisposableValuePlaceholder node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0005B73C File Offset: 0x0005993C
		public virtual R VisitObjectOrCollectionValuePlaceholder(BoundObjectOrCollectionValuePlaceholder node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0005B746 File Offset: 0x00059946
		public virtual R VisitDup(BoundDup node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0005B750 File Offset: 0x00059950
		public virtual R VisitPassByCopy(BoundPassByCopy node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0005B75A File Offset: 0x0005995A
		public virtual R VisitBadExpression(BoundBadExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0005B764 File Offset: 0x00059964
		public virtual R VisitBadStatement(BoundBadStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0005B76E File Offset: 0x0005996E
		public virtual R VisitExtractedFinallyBlock(BoundExtractedFinallyBlock node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0005B778 File Offset: 0x00059978
		public virtual R VisitTypeExpression(BoundTypeExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0005B782 File Offset: 0x00059982
		public virtual R VisitTypeOrValueExpression(BoundTypeOrValueExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0005B78C File Offset: 0x0005998C
		public virtual R VisitNamespaceExpression(BoundNamespaceExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0005B796 File Offset: 0x00059996
		public virtual R VisitUnaryOperator(BoundUnaryOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0005B7A0 File Offset: 0x000599A0
		public virtual R VisitIncrementOperator(BoundIncrementOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0005B7AA File Offset: 0x000599AA
		public virtual R VisitAddressOfOperator(BoundAddressOfOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0005B7B4 File Offset: 0x000599B4
		public virtual R VisitUnconvertedAddressOfOperator(BoundUnconvertedAddressOfOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0005B7BE File Offset: 0x000599BE
		public virtual R VisitFunctionPointerLoad(BoundFunctionPointerLoad node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0005B7C8 File Offset: 0x000599C8
		public virtual R VisitPointerIndirectionOperator(BoundPointerIndirectionOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0005B7D2 File Offset: 0x000599D2
		public virtual R VisitPointerElementAccess(BoundPointerElementAccess node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0005B7DC File Offset: 0x000599DC
		public virtual R VisitFunctionPointerInvocation(BoundFunctionPointerInvocation node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0005B7E6 File Offset: 0x000599E6
		public virtual R VisitRefTypeOperator(BoundRefTypeOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0005B7F0 File Offset: 0x000599F0
		public virtual R VisitMakeRefOperator(BoundMakeRefOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0005B7FA File Offset: 0x000599FA
		public virtual R VisitRefValueOperator(BoundRefValueOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0005B804 File Offset: 0x00059A04
		public virtual R VisitFromEndIndexExpression(BoundFromEndIndexExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0005B80E File Offset: 0x00059A0E
		public virtual R VisitRangeExpression(BoundRangeExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0005B818 File Offset: 0x00059A18
		public virtual R VisitBinaryOperator(BoundBinaryOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0005B822 File Offset: 0x00059A22
		public virtual R VisitTupleBinaryOperator(BoundTupleBinaryOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0005B82C File Offset: 0x00059A2C
		public virtual R VisitUserDefinedConditionalLogicalOperator(BoundUserDefinedConditionalLogicalOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0005B836 File Offset: 0x00059A36
		public virtual R VisitCompoundAssignmentOperator(BoundCompoundAssignmentOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0005B840 File Offset: 0x00059A40
		public virtual R VisitAssignmentOperator(BoundAssignmentOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0005B84A File Offset: 0x00059A4A
		public virtual R VisitDeconstructionAssignmentOperator(BoundDeconstructionAssignmentOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0005B854 File Offset: 0x00059A54
		public virtual R VisitNullCoalescingOperator(BoundNullCoalescingOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0005B85E File Offset: 0x00059A5E
		public virtual R VisitNullCoalescingAssignmentOperator(BoundNullCoalescingAssignmentOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0005B868 File Offset: 0x00059A68
		public virtual R VisitUnconvertedConditionalOperator(BoundUnconvertedConditionalOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0005B872 File Offset: 0x00059A72
		public virtual R VisitConditionalOperator(BoundConditionalOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0005B87C File Offset: 0x00059A7C
		public virtual R VisitArrayAccess(BoundArrayAccess node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0005B886 File Offset: 0x00059A86
		public virtual R VisitArrayLength(BoundArrayLength node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0005B890 File Offset: 0x00059A90
		public virtual R VisitAwaitableInfo(BoundAwaitableInfo node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0005B89A File Offset: 0x00059A9A
		public virtual R VisitAwaitExpression(BoundAwaitExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0005B8A4 File Offset: 0x00059AA4
		public virtual R VisitTypeOfOperator(BoundTypeOfOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0005B8AE File Offset: 0x00059AAE
		public virtual R VisitMethodDefIndex(BoundMethodDefIndex node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0005B8B8 File Offset: 0x00059AB8
		public virtual R VisitMaximumMethodDefIndex(BoundMaximumMethodDefIndex node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0005B8C2 File Offset: 0x00059AC2
		public virtual R VisitInstrumentationPayloadRoot(BoundInstrumentationPayloadRoot node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0005B8CC File Offset: 0x00059ACC
		public virtual R VisitModuleVersionId(BoundModuleVersionId node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0005B8D6 File Offset: 0x00059AD6
		public virtual R VisitModuleVersionIdString(BoundModuleVersionIdString node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0005B8E0 File Offset: 0x00059AE0
		public virtual R VisitSourceDocumentIndex(BoundSourceDocumentIndex node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0005B8EA File Offset: 0x00059AEA
		public virtual R VisitMethodInfo(BoundMethodInfo node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0005B8F4 File Offset: 0x00059AF4
		public virtual R VisitFieldInfo(BoundFieldInfo node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0005B8FE File Offset: 0x00059AFE
		public virtual R VisitDefaultLiteral(BoundDefaultLiteral node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0005B908 File Offset: 0x00059B08
		public virtual R VisitDefaultExpression(BoundDefaultExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0005B912 File Offset: 0x00059B12
		public virtual R VisitIsOperator(BoundIsOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0005B91C File Offset: 0x00059B1C
		public virtual R VisitAsOperator(BoundAsOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0005B926 File Offset: 0x00059B26
		public virtual R VisitSizeOfOperator(BoundSizeOfOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0005B930 File Offset: 0x00059B30
		public virtual R VisitConversion(BoundConversion node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0005B93A File Offset: 0x00059B3A
		public virtual R VisitReadOnlySpanFromArray(BoundReadOnlySpanFromArray node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0005B944 File Offset: 0x00059B44
		public virtual R VisitArgList(BoundArgList node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0005B94E File Offset: 0x00059B4E
		public virtual R VisitArgListOperator(BoundArgListOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0005B958 File Offset: 0x00059B58
		public virtual R VisitFixedLocalCollectionInitializer(BoundFixedLocalCollectionInitializer node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0005B962 File Offset: 0x00059B62
		public virtual R VisitSequencePoint(BoundSequencePoint node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0005B96C File Offset: 0x00059B6C
		public virtual R VisitSequencePointWithSpan(BoundSequencePointWithSpan node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0005B976 File Offset: 0x00059B76
		public virtual R VisitSavePreviousSequencePoint(BoundSavePreviousSequencePoint node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0005B980 File Offset: 0x00059B80
		public virtual R VisitRestorePreviousSequencePoint(BoundRestorePreviousSequencePoint node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0005B98A File Offset: 0x00059B8A
		public virtual R VisitStepThroughSequencePoint(BoundStepThroughSequencePoint node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0005B994 File Offset: 0x00059B94
		public virtual R VisitBlock(BoundBlock node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0005B99E File Offset: 0x00059B9E
		public virtual R VisitScope(BoundScope node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0005B9A8 File Offset: 0x00059BA8
		public virtual R VisitStateMachineScope(BoundStateMachineScope node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0005B9B2 File Offset: 0x00059BB2
		public virtual R VisitLocalDeclaration(BoundLocalDeclaration node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0005B9BC File Offset: 0x00059BBC
		public virtual R VisitMultipleLocalDeclarations(BoundMultipleLocalDeclarations node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0005B9C6 File Offset: 0x00059BC6
		public virtual R VisitUsingLocalDeclarations(BoundUsingLocalDeclarations node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0005B9D0 File Offset: 0x00059BD0
		public virtual R VisitLocalFunctionStatement(BoundLocalFunctionStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0005B9DA File Offset: 0x00059BDA
		public virtual R VisitNoOpStatement(BoundNoOpStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0005B9E4 File Offset: 0x00059BE4
		public virtual R VisitReturnStatement(BoundReturnStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0005B9EE File Offset: 0x00059BEE
		public virtual R VisitYieldReturnStatement(BoundYieldReturnStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0005B9F8 File Offset: 0x00059BF8
		public virtual R VisitYieldBreakStatement(BoundYieldBreakStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0005BA02 File Offset: 0x00059C02
		public virtual R VisitThrowStatement(BoundThrowStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0005BA0C File Offset: 0x00059C0C
		public virtual R VisitExpressionStatement(BoundExpressionStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0005BA16 File Offset: 0x00059C16
		public virtual R VisitBreakStatement(BoundBreakStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0005BA20 File Offset: 0x00059C20
		public virtual R VisitContinueStatement(BoundContinueStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0005BA2A File Offset: 0x00059C2A
		public virtual R VisitSwitchStatement(BoundSwitchStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0005BA34 File Offset: 0x00059C34
		public virtual R VisitSwitchDispatch(BoundSwitchDispatch node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0005BA3E File Offset: 0x00059C3E
		public virtual R VisitIfStatement(BoundIfStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0005BA48 File Offset: 0x00059C48
		public virtual R VisitDoStatement(BoundDoStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0005BA52 File Offset: 0x00059C52
		public virtual R VisitWhileStatement(BoundWhileStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0005BA5C File Offset: 0x00059C5C
		public virtual R VisitForStatement(BoundForStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0005BA66 File Offset: 0x00059C66
		public virtual R VisitForEachStatement(BoundForEachStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0005BA70 File Offset: 0x00059C70
		public virtual R VisitForEachDeconstructStep(BoundForEachDeconstructStep node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0005BA7A File Offset: 0x00059C7A
		public virtual R VisitUsingStatement(BoundUsingStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0005BA84 File Offset: 0x00059C84
		public virtual R VisitFixedStatement(BoundFixedStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0005BA8E File Offset: 0x00059C8E
		public virtual R VisitLockStatement(BoundLockStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0005BA98 File Offset: 0x00059C98
		public virtual R VisitTryStatement(BoundTryStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0005BAA2 File Offset: 0x00059CA2
		public virtual R VisitCatchBlock(BoundCatchBlock node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0005BAAC File Offset: 0x00059CAC
		public virtual R VisitLiteral(BoundLiteral node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0005BAB6 File Offset: 0x00059CB6
		public virtual R VisitThisReference(BoundThisReference node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0005BAC0 File Offset: 0x00059CC0
		public virtual R VisitPreviousSubmissionReference(BoundPreviousSubmissionReference node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0005BACA File Offset: 0x00059CCA
		public virtual R VisitHostObjectMemberReference(BoundHostObjectMemberReference node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0005BAD4 File Offset: 0x00059CD4
		public virtual R VisitBaseReference(BoundBaseReference node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0005BADE File Offset: 0x00059CDE
		public virtual R VisitLocal(BoundLocal node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0005BAE8 File Offset: 0x00059CE8
		public virtual R VisitPseudoVariable(BoundPseudoVariable node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0005BAF2 File Offset: 0x00059CF2
		public virtual R VisitRangeVariable(BoundRangeVariable node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0005BAFC File Offset: 0x00059CFC
		public virtual R VisitParameter(BoundParameter node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0005BB06 File Offset: 0x00059D06
		public virtual R VisitLabelStatement(BoundLabelStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0005BB10 File Offset: 0x00059D10
		public virtual R VisitGotoStatement(BoundGotoStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0005BB1A File Offset: 0x00059D1A
		public virtual R VisitLabeledStatement(BoundLabeledStatement node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0005BB24 File Offset: 0x00059D24
		public virtual R VisitLabel(BoundLabel node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0005BB2E File Offset: 0x00059D2E
		public virtual R VisitStatementList(BoundStatementList node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0005BB38 File Offset: 0x00059D38
		public virtual R VisitConditionalGoto(BoundConditionalGoto node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0005BB42 File Offset: 0x00059D42
		public virtual R VisitSwitchExpressionArm(BoundSwitchExpressionArm node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0005BB4C File Offset: 0x00059D4C
		public virtual R VisitUnconvertedSwitchExpression(BoundUnconvertedSwitchExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0005BB56 File Offset: 0x00059D56
		public virtual R VisitConvertedSwitchExpression(BoundConvertedSwitchExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0005BB60 File Offset: 0x00059D60
		public virtual R VisitDecisionDag(BoundDecisionDag node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0005BB6A File Offset: 0x00059D6A
		public virtual R VisitEvaluationDecisionDagNode(BoundEvaluationDecisionDagNode node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0005BB74 File Offset: 0x00059D74
		public virtual R VisitTestDecisionDagNode(BoundTestDecisionDagNode node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0005BB7E File Offset: 0x00059D7E
		public virtual R VisitWhenDecisionDagNode(BoundWhenDecisionDagNode node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0005BB88 File Offset: 0x00059D88
		public virtual R VisitLeafDecisionDagNode(BoundLeafDecisionDagNode node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0005BB92 File Offset: 0x00059D92
		public virtual R VisitDagTemp(BoundDagTemp node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0005BB9C File Offset: 0x00059D9C
		public virtual R VisitDagTypeTest(BoundDagTypeTest node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0005BBA6 File Offset: 0x00059DA6
		public virtual R VisitDagNonNullTest(BoundDagNonNullTest node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0005BBB0 File Offset: 0x00059DB0
		public virtual R VisitDagExplicitNullTest(BoundDagExplicitNullTest node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0005BBBA File Offset: 0x00059DBA
		public virtual R VisitDagValueTest(BoundDagValueTest node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0005BBC4 File Offset: 0x00059DC4
		public virtual R VisitDagRelationalTest(BoundDagRelationalTest node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0005BBCE File Offset: 0x00059DCE
		public virtual R VisitDagDeconstructEvaluation(BoundDagDeconstructEvaluation node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0005BBD8 File Offset: 0x00059DD8
		public virtual R VisitDagTypeEvaluation(BoundDagTypeEvaluation node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0005BBE2 File Offset: 0x00059DE2
		public virtual R VisitDagFieldEvaluation(BoundDagFieldEvaluation node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0005BBEC File Offset: 0x00059DEC
		public virtual R VisitDagPropertyEvaluation(BoundDagPropertyEvaluation node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0005BBF6 File Offset: 0x00059DF6
		public virtual R VisitDagIndexEvaluation(BoundDagIndexEvaluation node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0005BC00 File Offset: 0x00059E00
		public virtual R VisitSwitchSection(BoundSwitchSection node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0005BC0A File Offset: 0x00059E0A
		public virtual R VisitSwitchLabel(BoundSwitchLabel node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0005BC14 File Offset: 0x00059E14
		public virtual R VisitSequencePointExpression(BoundSequencePointExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0005BC1E File Offset: 0x00059E1E
		public virtual R VisitSequence(BoundSequence node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0005BC28 File Offset: 0x00059E28
		public virtual R VisitSpillSequence(BoundSpillSequence node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0005BC32 File Offset: 0x00059E32
		public virtual R VisitDynamicMemberAccess(BoundDynamicMemberAccess node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0005BC3C File Offset: 0x00059E3C
		public virtual R VisitDynamicInvocation(BoundDynamicInvocation node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0005BC46 File Offset: 0x00059E46
		public virtual R VisitConditionalAccess(BoundConditionalAccess node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0005BC50 File Offset: 0x00059E50
		public virtual R VisitLoweredConditionalAccess(BoundLoweredConditionalAccess node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0005BC5A File Offset: 0x00059E5A
		public virtual R VisitConditionalReceiver(BoundConditionalReceiver node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0005BC64 File Offset: 0x00059E64
		public virtual R VisitComplexConditionalReceiver(BoundComplexConditionalReceiver node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0005BC6E File Offset: 0x00059E6E
		public virtual R VisitMethodGroup(BoundMethodGroup node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0005BC78 File Offset: 0x00059E78
		public virtual R VisitPropertyGroup(BoundPropertyGroup node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0005BC82 File Offset: 0x00059E82
		public virtual R VisitCall(BoundCall node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0005BC8C File Offset: 0x00059E8C
		public virtual R VisitEventAssignmentOperator(BoundEventAssignmentOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0005BC96 File Offset: 0x00059E96
		public virtual R VisitAttribute(BoundAttribute node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0005BCA0 File Offset: 0x00059EA0
		public virtual R VisitUnconvertedObjectCreationExpression(BoundUnconvertedObjectCreationExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0005BCAA File Offset: 0x00059EAA
		public virtual R VisitObjectCreationExpression(BoundObjectCreationExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0005BCB4 File Offset: 0x00059EB4
		public virtual R VisitTupleLiteral(BoundTupleLiteral node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0005BCBE File Offset: 0x00059EBE
		public virtual R VisitConvertedTupleLiteral(BoundConvertedTupleLiteral node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0005BCC8 File Offset: 0x00059EC8
		public virtual R VisitDynamicObjectCreationExpression(BoundDynamicObjectCreationExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0005BCD2 File Offset: 0x00059ED2
		public virtual R VisitNoPiaObjectCreationExpression(BoundNoPiaObjectCreationExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0005BCDC File Offset: 0x00059EDC
		public virtual R VisitObjectInitializerExpression(BoundObjectInitializerExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0005BCE6 File Offset: 0x00059EE6
		public virtual R VisitObjectInitializerMember(BoundObjectInitializerMember node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0005BCF0 File Offset: 0x00059EF0
		public virtual R VisitDynamicObjectInitializerMember(BoundDynamicObjectInitializerMember node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0005BCFA File Offset: 0x00059EFA
		public virtual R VisitCollectionInitializerExpression(BoundCollectionInitializerExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0005BD04 File Offset: 0x00059F04
		public virtual R VisitCollectionElementInitializer(BoundCollectionElementInitializer node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0005BD0E File Offset: 0x00059F0E
		public virtual R VisitDynamicCollectionElementInitializer(BoundDynamicCollectionElementInitializer node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0005BD18 File Offset: 0x00059F18
		public virtual R VisitImplicitReceiver(BoundImplicitReceiver node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0005BD22 File Offset: 0x00059F22
		public virtual R VisitAnonymousObjectCreationExpression(BoundAnonymousObjectCreationExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0005BD2C File Offset: 0x00059F2C
		public virtual R VisitAnonymousPropertyDeclaration(BoundAnonymousPropertyDeclaration node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0005BD36 File Offset: 0x00059F36
		public virtual R VisitNewT(BoundNewT node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0005BD40 File Offset: 0x00059F40
		public virtual R VisitDelegateCreationExpression(BoundDelegateCreationExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0005BD4A File Offset: 0x00059F4A
		public virtual R VisitArrayCreation(BoundArrayCreation node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0005BD54 File Offset: 0x00059F54
		public virtual R VisitArrayInitialization(BoundArrayInitialization node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0005BD5E File Offset: 0x00059F5E
		public virtual R VisitStackAllocArrayCreation(BoundStackAllocArrayCreation node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0005BD68 File Offset: 0x00059F68
		public virtual R VisitConvertedStackAllocExpression(BoundConvertedStackAllocExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0005BD72 File Offset: 0x00059F72
		public virtual R VisitFieldAccess(BoundFieldAccess node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0005BD7C File Offset: 0x00059F7C
		public virtual R VisitHoistedFieldAccess(BoundHoistedFieldAccess node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0005BD86 File Offset: 0x00059F86
		public virtual R VisitPropertyAccess(BoundPropertyAccess node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0005BD90 File Offset: 0x00059F90
		public virtual R VisitEventAccess(BoundEventAccess node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0005BD9A File Offset: 0x00059F9A
		public virtual R VisitIndexerAccess(BoundIndexerAccess node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0005BDA4 File Offset: 0x00059FA4
		public virtual R VisitIndexOrRangePatternIndexerAccess(BoundIndexOrRangePatternIndexerAccess node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0005BDAE File Offset: 0x00059FAE
		public virtual R VisitDynamicIndexerAccess(BoundDynamicIndexerAccess node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0005BDB8 File Offset: 0x00059FB8
		public virtual R VisitLambda(BoundLambda node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0005BDC2 File Offset: 0x00059FC2
		public virtual R VisitUnboundLambda(UnboundLambda node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0005BDCC File Offset: 0x00059FCC
		public virtual R VisitQueryClause(BoundQueryClause node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0005BDD6 File Offset: 0x00059FD6
		public virtual R VisitTypeOrInstanceInitializers(BoundTypeOrInstanceInitializers node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0005BDE0 File Offset: 0x00059FE0
		public virtual R VisitNameOfOperator(BoundNameOfOperator node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0005BDEA File Offset: 0x00059FEA
		public virtual R VisitUnconvertedInterpolatedString(BoundUnconvertedInterpolatedString node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0005BDF4 File Offset: 0x00059FF4
		public virtual R VisitInterpolatedString(BoundInterpolatedString node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0005BDFE File Offset: 0x00059FFE
		public virtual R VisitStringInsert(BoundStringInsert node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0005BE08 File Offset: 0x0005A008
		public virtual R VisitIsPatternExpression(BoundIsPatternExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0005BE12 File Offset: 0x0005A012
		public virtual R VisitConstantPattern(BoundConstantPattern node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0005BE1C File Offset: 0x0005A01C
		public virtual R VisitDiscardPattern(BoundDiscardPattern node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0005BE26 File Offset: 0x0005A026
		public virtual R VisitDeclarationPattern(BoundDeclarationPattern node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0005BE30 File Offset: 0x0005A030
		public virtual R VisitRecursivePattern(BoundRecursivePattern node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0005BE3A File Offset: 0x0005A03A
		public virtual R VisitITuplePattern(BoundITuplePattern node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0005BE44 File Offset: 0x0005A044
		public virtual R VisitSubpattern(BoundSubpattern node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0005BE4E File Offset: 0x0005A04E
		public virtual R VisitTypePattern(BoundTypePattern node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0005BE58 File Offset: 0x0005A058
		public virtual R VisitBinaryPattern(BoundBinaryPattern node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0005BE62 File Offset: 0x0005A062
		public virtual R VisitNegatedPattern(BoundNegatedPattern node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0005BE6C File Offset: 0x0005A06C
		public virtual R VisitRelationalPattern(BoundRelationalPattern node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0005BE76 File Offset: 0x0005A076
		public virtual R VisitDiscardExpression(BoundDiscardExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0005BE80 File Offset: 0x0005A080
		public virtual R VisitThrowExpression(BoundThrowExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0005BE8A File Offset: 0x0005A08A
		public virtual R VisitOutVariablePendingInference(OutVariablePendingInference node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0005BE94 File Offset: 0x0005A094
		public virtual R VisitDeconstructionVariablePendingInference(DeconstructionVariablePendingInference node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0005BE9E File Offset: 0x0005A09E
		public virtual R VisitOutDeconstructVarPendingInference(OutDeconstructVarPendingInference node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0005BEA8 File Offset: 0x0005A0A8
		public virtual R VisitNonConstructorMethodBody(BoundNonConstructorMethodBody node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0005BEB2 File Offset: 0x0005A0B2
		public virtual R VisitConstructorMethodBody(BoundConstructorMethodBody node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0005BEBC File Offset: 0x0005A0BC
		public virtual R VisitExpressionWithNullability(BoundExpressionWithNullability node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0005BEC6 File Offset: 0x0005A0C6
		public virtual R VisitWithExpression(BoundWithExpression node, A arg)
		{
			return this.DefaultVisit(node, arg);
		}
	}
}
