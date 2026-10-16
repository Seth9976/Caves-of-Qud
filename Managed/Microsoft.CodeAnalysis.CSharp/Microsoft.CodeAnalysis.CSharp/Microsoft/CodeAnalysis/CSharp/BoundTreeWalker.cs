using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000C5 RID: 197
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundTreeWalker : BoundTreeVisitor
	{
		// Token: 0x06001018 RID: 4120 RVA: 0x0005C68C File Offset: 0x0005A88C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public void VisitList<T>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<T> list) where T : BoundNode
		{
			if (!list.IsDefault)
			{
				for (int i = 0; i < list.Length; i++)
				{
					this.Visit(list[i]);
				}
			}
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0005C6C8 File Offset: 0x0005A8C8
		protected void VisitUnoptimizedForm(BoundQueryClause queryClause)
		{
			BoundExpression boundExpression = queryClause.UnoptimizedForm;
			BoundQueryClause boundQueryClause = boundExpression as BoundQueryClause;
			if (boundQueryClause != null)
			{
				boundExpression = boundQueryClause.Value;
			}
			BoundCall boundCall = boundExpression as BoundCall;
			if (boundCall != null && boundCall.Method != null)
			{
				ImmutableArray<BoundExpression> arguments = boundCall.Arguments;
				if (boundCall.Method.Name == "Select")
				{
					this.Visit(arguments[arguments.Length - 1]);
					return;
				}
				if (boundCall.Method.Name == "GroupBy")
				{
					this.Visit(arguments[arguments.Length - 2]);
				}
			}
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0005C763 File Offset: 0x0005A963
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFieldEqualsValue(BoundFieldEqualsValue node)
		{
			this.Visit(node.Value);
			return null;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0005C773 File Offset: 0x0005A973
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPropertyEqualsValue(BoundPropertyEqualsValue node)
		{
			this.Visit(node.Value);
			return null;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0005C783 File Offset: 0x0005A983
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitParameterEqualsValue(BoundParameterEqualsValue node)
		{
			this.Visit(node.Value);
			return null;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0005C793 File Offset: 0x0005A993
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitGlobalStatementInitializer(BoundGlobalStatementInitializer node)
		{
			this.Visit(node.Statement);
			return null;
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0005C7A3 File Offset: 0x0005A9A3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDeconstructValuePlaceholder(BoundDeconstructValuePlaceholder node)
		{
			return null;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0005C7A6 File Offset: 0x0005A9A6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTupleOperandPlaceholder(BoundTupleOperandPlaceholder node)
		{
			return null;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0005C7A9 File Offset: 0x0005A9A9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAwaitableValuePlaceholder(BoundAwaitableValuePlaceholder node)
		{
			return null;
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0005C7AC File Offset: 0x0005A9AC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDisposableValuePlaceholder(BoundDisposableValuePlaceholder node)
		{
			return null;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0005C7AF File Offset: 0x0005A9AF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitObjectOrCollectionValuePlaceholder(BoundObjectOrCollectionValuePlaceholder node)
		{
			return null;
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0005C7B2 File Offset: 0x0005A9B2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDup(BoundDup node)
		{
			return null;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0005C7B5 File Offset: 0x0005A9B5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPassByCopy(BoundPassByCopy node)
		{
			this.Visit(node.Expression);
			return null;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0005C7C5 File Offset: 0x0005A9C5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBadExpression(BoundBadExpression node)
		{
			this.VisitList<BoundExpression>(node.ChildBoundNodes);
			return null;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0005C7D4 File Offset: 0x0005A9D4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBadStatement(BoundBadStatement node)
		{
			this.VisitList<BoundNode>(node.ChildBoundNodes);
			return null;
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0005C7E3 File Offset: 0x0005A9E3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitExtractedFinallyBlock(BoundExtractedFinallyBlock node)
		{
			this.Visit(node.FinallyBlock);
			return null;
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0005C7F3 File Offset: 0x0005A9F3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTypeExpression(BoundTypeExpression node)
		{
			this.Visit(node.BoundContainingTypeOpt);
			this.VisitList<BoundExpression>(node.BoundDimensionsOpt);
			return null;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0005C80F File Offset: 0x0005AA0F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTypeOrValueExpression(BoundTypeOrValueExpression node)
		{
			return null;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0005C812 File Offset: 0x0005AA12
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNamespaceExpression(BoundNamespaceExpression node)
		{
			return null;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0005C815 File Offset: 0x0005AA15
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnaryOperator(BoundUnaryOperator node)
		{
			this.Visit(node.Operand);
			return null;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0005C825 File Offset: 0x0005AA25
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIncrementOperator(BoundIncrementOperator node)
		{
			this.Visit(node.Operand);
			return null;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0005C835 File Offset: 0x0005AA35
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAddressOfOperator(BoundAddressOfOperator node)
		{
			this.Visit(node.Operand);
			return null;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0005C845 File Offset: 0x0005AA45
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnconvertedAddressOfOperator(BoundUnconvertedAddressOfOperator node)
		{
			this.Visit(node.Operand);
			return null;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0005C855 File Offset: 0x0005AA55
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFunctionPointerLoad(BoundFunctionPointerLoad node)
		{
			return null;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0005C858 File Offset: 0x0005AA58
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPointerIndirectionOperator(BoundPointerIndirectionOperator node)
		{
			this.Visit(node.Operand);
			return null;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0005C868 File Offset: 0x0005AA68
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPointerElementAccess(BoundPointerElementAccess node)
		{
			this.Visit(node.Expression);
			this.Visit(node.Index);
			return null;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0005C885 File Offset: 0x0005AA85
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFunctionPointerInvocation(BoundFunctionPointerInvocation node)
		{
			this.Visit(node.InvokedExpression);
			this.VisitList<BoundExpression>(node.Arguments);
			return null;
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0005C8A1 File Offset: 0x0005AAA1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRefTypeOperator(BoundRefTypeOperator node)
		{
			this.Visit(node.Operand);
			return null;
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0005C8B1 File Offset: 0x0005AAB1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMakeRefOperator(BoundMakeRefOperator node)
		{
			this.Visit(node.Operand);
			return null;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0005C8C1 File Offset: 0x0005AAC1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRefValueOperator(BoundRefValueOperator node)
		{
			this.Visit(node.Operand);
			return null;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0005C8D1 File Offset: 0x0005AAD1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFromEndIndexExpression(BoundFromEndIndexExpression node)
		{
			this.Visit(node.Operand);
			return null;
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0005C8E1 File Offset: 0x0005AAE1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRangeExpression(BoundRangeExpression node)
		{
			this.Visit(node.LeftOperandOpt);
			this.Visit(node.RightOperandOpt);
			return null;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0005C8FE File Offset: 0x0005AAFE
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBinaryOperator(BoundBinaryOperator node)
		{
			this.Visit(node.Left);
			this.Visit(node.Right);
			return null;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0005C91B File Offset: 0x0005AB1B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTupleBinaryOperator(BoundTupleBinaryOperator node)
		{
			this.Visit(node.Left);
			this.Visit(node.Right);
			return null;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0005C938 File Offset: 0x0005AB38
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUserDefinedConditionalLogicalOperator(BoundUserDefinedConditionalLogicalOperator node)
		{
			this.Visit(node.Left);
			this.Visit(node.Right);
			return null;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0005C955 File Offset: 0x0005AB55
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCompoundAssignmentOperator(BoundCompoundAssignmentOperator node)
		{
			this.Visit(node.Left);
			this.Visit(node.Right);
			return null;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0005C972 File Offset: 0x0005AB72
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAssignmentOperator(BoundAssignmentOperator node)
		{
			this.Visit(node.Left);
			this.Visit(node.Right);
			return null;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0005C98F File Offset: 0x0005AB8F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDeconstructionAssignmentOperator(BoundDeconstructionAssignmentOperator node)
		{
			this.Visit(node.Left);
			this.Visit(node.Right);
			return null;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0005C9AC File Offset: 0x0005ABAC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNullCoalescingOperator(BoundNullCoalescingOperator node)
		{
			this.Visit(node.LeftOperand);
			this.Visit(node.RightOperand);
			return null;
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0005C9C9 File Offset: 0x0005ABC9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNullCoalescingAssignmentOperator(BoundNullCoalescingAssignmentOperator node)
		{
			this.Visit(node.LeftOperand);
			this.Visit(node.RightOperand);
			return null;
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0005C9E6 File Offset: 0x0005ABE6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnconvertedConditionalOperator(BoundUnconvertedConditionalOperator node)
		{
			this.Visit(node.Condition);
			this.Visit(node.Consequence);
			this.Visit(node.Alternative);
			return null;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0005CA10 File Offset: 0x0005AC10
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConditionalOperator(BoundConditionalOperator node)
		{
			this.Visit(node.Condition);
			this.Visit(node.Consequence);
			this.Visit(node.Alternative);
			return null;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0005CA3A File Offset: 0x0005AC3A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArrayAccess(BoundArrayAccess node)
		{
			this.Visit(node.Expression);
			this.VisitList<BoundExpression>(node.Indices);
			return null;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0005CA56 File Offset: 0x0005AC56
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArrayLength(BoundArrayLength node)
		{
			this.Visit(node.Expression);
			return null;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0005CA66 File Offset: 0x0005AC66
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAwaitableInfo(BoundAwaitableInfo node)
		{
			this.Visit(node.AwaitableInstancePlaceholder);
			this.Visit(node.GetAwaiter);
			return null;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0005CA83 File Offset: 0x0005AC83
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAwaitExpression(BoundAwaitExpression node)
		{
			this.Visit(node.Expression);
			this.Visit(node.AwaitableInfo);
			return null;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0005CAA0 File Offset: 0x0005ACA0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTypeOfOperator(BoundTypeOfOperator node)
		{
			this.Visit(node.SourceType);
			return null;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0005CAB0 File Offset: 0x0005ACB0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMethodDefIndex(BoundMethodDefIndex node)
		{
			return null;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0005CAB3 File Offset: 0x0005ACB3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMaximumMethodDefIndex(BoundMaximumMethodDefIndex node)
		{
			return null;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0005CAB6 File Offset: 0x0005ACB6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitInstrumentationPayloadRoot(BoundInstrumentationPayloadRoot node)
		{
			return null;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0005CAB9 File Offset: 0x0005ACB9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitModuleVersionId(BoundModuleVersionId node)
		{
			return null;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0005CABC File Offset: 0x0005ACBC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitModuleVersionIdString(BoundModuleVersionIdString node)
		{
			return null;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0005CABF File Offset: 0x0005ACBF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSourceDocumentIndex(BoundSourceDocumentIndex node)
		{
			return null;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0005CAC2 File Offset: 0x0005ACC2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMethodInfo(BoundMethodInfo node)
		{
			return null;
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0005CAC5 File Offset: 0x0005ACC5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFieldInfo(BoundFieldInfo node)
		{
			return null;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0005CAC8 File Offset: 0x0005ACC8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDefaultLiteral(BoundDefaultLiteral node)
		{
			return null;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0005CACB File Offset: 0x0005ACCB
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDefaultExpression(BoundDefaultExpression node)
		{
			return null;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0005CACE File Offset: 0x0005ACCE
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIsOperator(BoundIsOperator node)
		{
			this.Visit(node.Operand);
			this.Visit(node.TargetType);
			return null;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0005CAEB File Offset: 0x0005ACEB
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAsOperator(BoundAsOperator node)
		{
			this.Visit(node.Operand);
			this.Visit(node.TargetType);
			return null;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0005CB08 File Offset: 0x0005AD08
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSizeOfOperator(BoundSizeOfOperator node)
		{
			this.Visit(node.SourceType);
			return null;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0005CB18 File Offset: 0x0005AD18
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConversion(BoundConversion node)
		{
			this.Visit(node.Operand);
			return null;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0005CB28 File Offset: 0x0005AD28
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitReadOnlySpanFromArray(BoundReadOnlySpanFromArray node)
		{
			this.Visit(node.Operand);
			return null;
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0005CB38 File Offset: 0x0005AD38
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArgList(BoundArgList node)
		{
			return null;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0005CB3B File Offset: 0x0005AD3B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArgListOperator(BoundArgListOperator node)
		{
			this.VisitList<BoundExpression>(node.Arguments);
			return null;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0005CB4A File Offset: 0x0005AD4A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFixedLocalCollectionInitializer(BoundFixedLocalCollectionInitializer node)
		{
			this.Visit(node.Expression);
			return null;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0005CB5A File Offset: 0x0005AD5A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSequencePoint(BoundSequencePoint node)
		{
			this.Visit(node.StatementOpt);
			return null;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0005CB6A File Offset: 0x0005AD6A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSequencePointWithSpan(BoundSequencePointWithSpan node)
		{
			this.Visit(node.StatementOpt);
			return null;
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0005CB7A File Offset: 0x0005AD7A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSavePreviousSequencePoint(BoundSavePreviousSequencePoint node)
		{
			return null;
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0005CB7D File Offset: 0x0005AD7D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRestorePreviousSequencePoint(BoundRestorePreviousSequencePoint node)
		{
			return null;
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0005CB80 File Offset: 0x0005AD80
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitStepThroughSequencePoint(BoundStepThroughSequencePoint node)
		{
			return null;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0005CB83 File Offset: 0x0005AD83
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBlock(BoundBlock node)
		{
			this.VisitList<BoundStatement>(node.Statements);
			return null;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0005CB92 File Offset: 0x0005AD92
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitScope(BoundScope node)
		{
			this.VisitList<BoundStatement>(node.Statements);
			return null;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0005CBA1 File Offset: 0x0005ADA1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitStateMachineScope(BoundStateMachineScope node)
		{
			this.Visit(node.Statement);
			return null;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0005CBB1 File Offset: 0x0005ADB1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLocalDeclaration(BoundLocalDeclaration node)
		{
			this.Visit(node.DeclaredTypeOpt);
			this.Visit(node.InitializerOpt);
			this.VisitList<BoundExpression>(node.ArgumentsOpt);
			return null;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0005CBDA File Offset: 0x0005ADDA
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMultipleLocalDeclarations(BoundMultipleLocalDeclarations node)
		{
			this.VisitList<BoundLocalDeclaration>(node.LocalDeclarations);
			return null;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0005CBE9 File Offset: 0x0005ADE9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUsingLocalDeclarations(BoundUsingLocalDeclarations node)
		{
			this.Visit(node.AwaitOpt);
			this.VisitList<BoundLocalDeclaration>(node.LocalDeclarations);
			return null;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0005CC05 File Offset: 0x0005AE05
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLocalFunctionStatement(BoundLocalFunctionStatement node)
		{
			this.Visit(node.BlockBody);
			this.Visit(node.ExpressionBody);
			return null;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0005CC22 File Offset: 0x0005AE22
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNoOpStatement(BoundNoOpStatement node)
		{
			return null;
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0005CC25 File Offset: 0x0005AE25
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitReturnStatement(BoundReturnStatement node)
		{
			this.Visit(node.ExpressionOpt);
			return null;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0005CC35 File Offset: 0x0005AE35
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitYieldReturnStatement(BoundYieldReturnStatement node)
		{
			this.Visit(node.Expression);
			return null;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0005CC45 File Offset: 0x0005AE45
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitYieldBreakStatement(BoundYieldBreakStatement node)
		{
			return null;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0005CC48 File Offset: 0x0005AE48
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitThrowStatement(BoundThrowStatement node)
		{
			this.Visit(node.ExpressionOpt);
			return null;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0005CC58 File Offset: 0x0005AE58
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitExpressionStatement(BoundExpressionStatement node)
		{
			this.Visit(node.Expression);
			return null;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0005CC68 File Offset: 0x0005AE68
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBreakStatement(BoundBreakStatement node)
		{
			return null;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0005CC6B File Offset: 0x0005AE6B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitContinueStatement(BoundContinueStatement node)
		{
			return null;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0005CC6E File Offset: 0x0005AE6E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSwitchStatement(BoundSwitchStatement node)
		{
			this.Visit(node.Expression);
			this.VisitList<BoundSwitchSection>(node.SwitchSections);
			this.Visit(node.DefaultLabel);
			return null;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0005CC97 File Offset: 0x0005AE97
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSwitchDispatch(BoundSwitchDispatch node)
		{
			this.Visit(node.Expression);
			return null;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0005CCA7 File Offset: 0x0005AEA7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIfStatement(BoundIfStatement node)
		{
			this.Visit(node.Condition);
			this.Visit(node.Consequence);
			this.Visit(node.AlternativeOpt);
			return null;
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0005CCD1 File Offset: 0x0005AED1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDoStatement(BoundDoStatement node)
		{
			this.Visit(node.Condition);
			this.Visit(node.Body);
			return null;
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0005CCEE File Offset: 0x0005AEEE
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitWhileStatement(BoundWhileStatement node)
		{
			this.Visit(node.Condition);
			this.Visit(node.Body);
			return null;
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0005CD0B File Offset: 0x0005AF0B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitForStatement(BoundForStatement node)
		{
			this.Visit(node.Initializer);
			this.Visit(node.Condition);
			this.Visit(node.Increment);
			this.Visit(node.Body);
			return null;
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0005CD44 File Offset: 0x0005AF44
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitForEachStatement(BoundForEachStatement node)
		{
			this.Visit(node.IterationVariableType);
			this.Visit(node.IterationErrorExpressionOpt);
			this.Visit(node.Expression);
			this.Visit(node.DeconstructionOpt);
			this.Visit(node.AwaitOpt);
			this.Visit(node.Body);
			return null;
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0005CDA0 File Offset: 0x0005AFA0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitForEachDeconstructStep(BoundForEachDeconstructStep node)
		{
			this.Visit(node.DeconstructionAssignment);
			this.Visit(node.TargetPlaceholder);
			return null;
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0005CDBD File Offset: 0x0005AFBD
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUsingStatement(BoundUsingStatement node)
		{
			this.Visit(node.DeclarationsOpt);
			this.Visit(node.ExpressionOpt);
			this.Visit(node.Body);
			this.Visit(node.AwaitOpt);
			return null;
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0005CDF4 File Offset: 0x0005AFF4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFixedStatement(BoundFixedStatement node)
		{
			this.Visit(node.Declarations);
			this.Visit(node.Body);
			return null;
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0005CE11 File Offset: 0x0005B011
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLockStatement(BoundLockStatement node)
		{
			this.Visit(node.Argument);
			this.Visit(node.Body);
			return null;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0005CE2E File Offset: 0x0005B02E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTryStatement(BoundTryStatement node)
		{
			this.Visit(node.TryBlock);
			this.VisitList<BoundCatchBlock>(node.CatchBlocks);
			this.Visit(node.FinallyBlockOpt);
			return null;
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0005CE57 File Offset: 0x0005B057
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCatchBlock(BoundCatchBlock node)
		{
			this.Visit(node.ExceptionSourceOpt);
			this.Visit(node.ExceptionFilterPrologueOpt);
			this.Visit(node.ExceptionFilterOpt);
			this.Visit(node.Body);
			return null;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0005CE8E File Offset: 0x0005B08E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLiteral(BoundLiteral node)
		{
			return null;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0005CE91 File Offset: 0x0005B091
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitThisReference(BoundThisReference node)
		{
			return null;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0005CE94 File Offset: 0x0005B094
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPreviousSubmissionReference(BoundPreviousSubmissionReference node)
		{
			return null;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0005CE97 File Offset: 0x0005B097
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitHostObjectMemberReference(BoundHostObjectMemberReference node)
		{
			return null;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0005CE9A File Offset: 0x0005B09A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBaseReference(BoundBaseReference node)
		{
			return null;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0005CE9D File Offset: 0x0005B09D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLocal(BoundLocal node)
		{
			return null;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0005CEA0 File Offset: 0x0005B0A0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPseudoVariable(BoundPseudoVariable node)
		{
			return null;
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0005CEA3 File Offset: 0x0005B0A3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRangeVariable(BoundRangeVariable node)
		{
			this.Visit(node.Value);
			return null;
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0005CEB3 File Offset: 0x0005B0B3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitParameter(BoundParameter node)
		{
			return null;
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0005CEB6 File Offset: 0x0005B0B6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLabelStatement(BoundLabelStatement node)
		{
			return null;
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0005CEB9 File Offset: 0x0005B0B9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitGotoStatement(BoundGotoStatement node)
		{
			this.Visit(node.CaseExpressionOpt);
			this.Visit(node.LabelExpressionOpt);
			return null;
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0005CED6 File Offset: 0x0005B0D6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLabeledStatement(BoundLabeledStatement node)
		{
			this.Visit(node.Body);
			return null;
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0005CEE6 File Offset: 0x0005B0E6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLabel(BoundLabel node)
		{
			return null;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0005CEE9 File Offset: 0x0005B0E9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitStatementList(BoundStatementList node)
		{
			this.VisitList<BoundStatement>(node.Statements);
			return null;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0005CEF8 File Offset: 0x0005B0F8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConditionalGoto(BoundConditionalGoto node)
		{
			this.Visit(node.Condition);
			return null;
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0005CF08 File Offset: 0x0005B108
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSwitchExpressionArm(BoundSwitchExpressionArm node)
		{
			this.Visit(node.Pattern);
			this.Visit(node.WhenClause);
			this.Visit(node.Value);
			return null;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0005CF32 File Offset: 0x0005B132
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnconvertedSwitchExpression(BoundUnconvertedSwitchExpression node)
		{
			this.Visit(node.Expression);
			this.VisitList<BoundSwitchExpressionArm>(node.SwitchArms);
			return null;
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0005CF4E File Offset: 0x0005B14E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConvertedSwitchExpression(BoundConvertedSwitchExpression node)
		{
			this.Visit(node.Expression);
			this.VisitList<BoundSwitchExpressionArm>(node.SwitchArms);
			return null;
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0005CF6A File Offset: 0x0005B16A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDecisionDag(BoundDecisionDag node)
		{
			this.Visit(node.RootNode);
			return null;
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0005CF7A File Offset: 0x0005B17A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitEvaluationDecisionDagNode(BoundEvaluationDecisionDagNode node)
		{
			this.Visit(node.Evaluation);
			this.Visit(node.Next);
			return null;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0005CF97 File Offset: 0x0005B197
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTestDecisionDagNode(BoundTestDecisionDagNode node)
		{
			this.Visit(node.Test);
			this.Visit(node.WhenTrue);
			this.Visit(node.WhenFalse);
			return null;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0005CFC1 File Offset: 0x0005B1C1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitWhenDecisionDagNode(BoundWhenDecisionDagNode node)
		{
			this.Visit(node.WhenExpression);
			this.Visit(node.WhenTrue);
			this.Visit(node.WhenFalse);
			return null;
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0005CFEB File Offset: 0x0005B1EB
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLeafDecisionDagNode(BoundLeafDecisionDagNode node)
		{
			return null;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0005CFEE File Offset: 0x0005B1EE
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagTemp(BoundDagTemp node)
		{
			this.Visit(node.Source);
			return null;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0005CFFE File Offset: 0x0005B1FE
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagTypeTest(BoundDagTypeTest node)
		{
			this.Visit(node.Input);
			return null;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0005D00E File Offset: 0x0005B20E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagNonNullTest(BoundDagNonNullTest node)
		{
			this.Visit(node.Input);
			return null;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0005D01E File Offset: 0x0005B21E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagExplicitNullTest(BoundDagExplicitNullTest node)
		{
			this.Visit(node.Input);
			return null;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0005D02E File Offset: 0x0005B22E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagValueTest(BoundDagValueTest node)
		{
			this.Visit(node.Input);
			return null;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0005D03E File Offset: 0x0005B23E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagRelationalTest(BoundDagRelationalTest node)
		{
			this.Visit(node.Input);
			return null;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0005D04E File Offset: 0x0005B24E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagDeconstructEvaluation(BoundDagDeconstructEvaluation node)
		{
			this.Visit(node.Input);
			return null;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0005D05E File Offset: 0x0005B25E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagTypeEvaluation(BoundDagTypeEvaluation node)
		{
			this.Visit(node.Input);
			return null;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0005D06E File Offset: 0x0005B26E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagFieldEvaluation(BoundDagFieldEvaluation node)
		{
			this.Visit(node.Input);
			return null;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0005D07E File Offset: 0x0005B27E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagPropertyEvaluation(BoundDagPropertyEvaluation node)
		{
			this.Visit(node.Input);
			return null;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0005D08E File Offset: 0x0005B28E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagIndexEvaluation(BoundDagIndexEvaluation node)
		{
			this.Visit(node.Input);
			return null;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0005D09E File Offset: 0x0005B29E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSwitchSection(BoundSwitchSection node)
		{
			this.VisitList<BoundSwitchLabel>(node.SwitchLabels);
			this.VisitList<BoundStatement>(node.Statements);
			return null;
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0005D0B9 File Offset: 0x0005B2B9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSwitchLabel(BoundSwitchLabel node)
		{
			this.Visit(node.Pattern);
			this.Visit(node.WhenClause);
			return null;
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0005D0D6 File Offset: 0x0005B2D6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSequencePointExpression(BoundSequencePointExpression node)
		{
			this.Visit(node.Expression);
			return null;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0005D0E6 File Offset: 0x0005B2E6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSequence(BoundSequence node)
		{
			this.VisitList<BoundExpression>(node.SideEffects);
			this.Visit(node.Value);
			return null;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0005D102 File Offset: 0x0005B302
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSpillSequence(BoundSpillSequence node)
		{
			this.VisitList<BoundStatement>(node.SideEffects);
			this.Visit(node.Value);
			return null;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0005D11E File Offset: 0x0005B31E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicMemberAccess(BoundDynamicMemberAccess node)
		{
			this.Visit(node.Receiver);
			return null;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0005D12E File Offset: 0x0005B32E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicInvocation(BoundDynamicInvocation node)
		{
			this.Visit(node.Expression);
			this.VisitList<BoundExpression>(node.Arguments);
			return null;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0005D14A File Offset: 0x0005B34A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConditionalAccess(BoundConditionalAccess node)
		{
			this.Visit(node.Receiver);
			this.Visit(node.AccessExpression);
			return null;
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0005D167 File Offset: 0x0005B367
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLoweredConditionalAccess(BoundLoweredConditionalAccess node)
		{
			this.Visit(node.Receiver);
			this.Visit(node.WhenNotNull);
			this.Visit(node.WhenNullOpt);
			return null;
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0005D191 File Offset: 0x0005B391
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConditionalReceiver(BoundConditionalReceiver node)
		{
			return null;
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0005D194 File Offset: 0x0005B394
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitComplexConditionalReceiver(BoundComplexConditionalReceiver node)
		{
			this.Visit(node.ValueTypeReceiver);
			this.Visit(node.ReferenceTypeReceiver);
			return null;
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0005D1B1 File Offset: 0x0005B3B1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMethodGroup(BoundMethodGroup node)
		{
			this.Visit(node.ReceiverOpt);
			return null;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0005D1C1 File Offset: 0x0005B3C1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPropertyGroup(BoundPropertyGroup node)
		{
			this.Visit(node.ReceiverOpt);
			return null;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0005D1D1 File Offset: 0x0005B3D1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCall(BoundCall node)
		{
			this.Visit(node.ReceiverOpt);
			this.VisitList<BoundExpression>(node.Arguments);
			return null;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0005D1ED File Offset: 0x0005B3ED
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitEventAssignmentOperator(BoundEventAssignmentOperator node)
		{
			this.Visit(node.ReceiverOpt);
			this.Visit(node.Argument);
			return null;
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0005D20A File Offset: 0x0005B40A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAttribute(BoundAttribute node)
		{
			this.VisitList<BoundExpression>(node.ConstructorArguments);
			this.VisitList<BoundAssignmentOperator>(node.NamedArguments);
			return null;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0005D225 File Offset: 0x0005B425
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnconvertedObjectCreationExpression(BoundUnconvertedObjectCreationExpression node)
		{
			this.VisitList<BoundExpression>(node.Arguments);
			return null;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0005D234 File Offset: 0x0005B434
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitObjectCreationExpression(BoundObjectCreationExpression node)
		{
			this.VisitList<BoundExpression>(node.Arguments);
			this.Visit(node.InitializerExpressionOpt);
			return null;
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0005D250 File Offset: 0x0005B450
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTupleLiteral(BoundTupleLiteral node)
		{
			this.VisitList<BoundExpression>(node.Arguments);
			return null;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0005D25F File Offset: 0x0005B45F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConvertedTupleLiteral(BoundConvertedTupleLiteral node)
		{
			this.VisitList<BoundExpression>(node.Arguments);
			return null;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0005D26E File Offset: 0x0005B46E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicObjectCreationExpression(BoundDynamicObjectCreationExpression node)
		{
			this.VisitList<BoundExpression>(node.Arguments);
			this.Visit(node.InitializerExpressionOpt);
			return null;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0005D28A File Offset: 0x0005B48A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNoPiaObjectCreationExpression(BoundNoPiaObjectCreationExpression node)
		{
			this.Visit(node.InitializerExpressionOpt);
			return null;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0005D29A File Offset: 0x0005B49A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitObjectInitializerExpression(BoundObjectInitializerExpression node)
		{
			this.Visit(node.Placeholder);
			this.VisitList<BoundExpression>(node.Initializers);
			return null;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0005D2B6 File Offset: 0x0005B4B6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitObjectInitializerMember(BoundObjectInitializerMember node)
		{
			this.VisitList<BoundExpression>(node.Arguments);
			return null;
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0005D2C5 File Offset: 0x0005B4C5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicObjectInitializerMember(BoundDynamicObjectInitializerMember node)
		{
			return null;
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0005D2C8 File Offset: 0x0005B4C8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCollectionInitializerExpression(BoundCollectionInitializerExpression node)
		{
			this.Visit(node.Placeholder);
			this.VisitList<BoundExpression>(node.Initializers);
			return null;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0005D2E4 File Offset: 0x0005B4E4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCollectionElementInitializer(BoundCollectionElementInitializer node)
		{
			this.VisitList<BoundExpression>(node.Arguments);
			this.Visit(node.ImplicitReceiverOpt);
			return null;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0005D300 File Offset: 0x0005B500
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicCollectionElementInitializer(BoundDynamicCollectionElementInitializer node)
		{
			this.Visit(node.Expression);
			this.VisitList<BoundExpression>(node.Arguments);
			return null;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0005D31C File Offset: 0x0005B51C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitImplicitReceiver(BoundImplicitReceiver node)
		{
			return null;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0005D31F File Offset: 0x0005B51F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAnonymousObjectCreationExpression(BoundAnonymousObjectCreationExpression node)
		{
			this.VisitList<BoundExpression>(node.Arguments);
			this.VisitList<BoundAnonymousPropertyDeclaration>(node.Declarations);
			return null;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0005D33A File Offset: 0x0005B53A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAnonymousPropertyDeclaration(BoundAnonymousPropertyDeclaration node)
		{
			return null;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0005D33D File Offset: 0x0005B53D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNewT(BoundNewT node)
		{
			this.Visit(node.InitializerExpressionOpt);
			return null;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0005D34D File Offset: 0x0005B54D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDelegateCreationExpression(BoundDelegateCreationExpression node)
		{
			this.Visit(node.Argument);
			return null;
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0005D35D File Offset: 0x0005B55D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArrayCreation(BoundArrayCreation node)
		{
			this.VisitList<BoundExpression>(node.Bounds);
			this.Visit(node.InitializerOpt);
			return null;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0005D379 File Offset: 0x0005B579
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArrayInitialization(BoundArrayInitialization node)
		{
			this.VisitList<BoundExpression>(node.Initializers);
			return null;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0005D388 File Offset: 0x0005B588
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitStackAllocArrayCreation(BoundStackAllocArrayCreation node)
		{
			this.Visit(node.Count);
			this.Visit(node.InitializerOpt);
			return null;
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0005D3A5 File Offset: 0x0005B5A5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConvertedStackAllocExpression(BoundConvertedStackAllocExpression node)
		{
			this.Visit(node.Count);
			this.Visit(node.InitializerOpt);
			return null;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0005D3C2 File Offset: 0x0005B5C2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFieldAccess(BoundFieldAccess node)
		{
			this.Visit(node.ReceiverOpt);
			return null;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0005D3D2 File Offset: 0x0005B5D2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitHoistedFieldAccess(BoundHoistedFieldAccess node)
		{
			return null;
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0005D3D5 File Offset: 0x0005B5D5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPropertyAccess(BoundPropertyAccess node)
		{
			this.Visit(node.ReceiverOpt);
			return null;
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0005D3E5 File Offset: 0x0005B5E5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitEventAccess(BoundEventAccess node)
		{
			this.Visit(node.ReceiverOpt);
			return null;
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0005D3F5 File Offset: 0x0005B5F5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIndexerAccess(BoundIndexerAccess node)
		{
			this.Visit(node.ReceiverOpt);
			this.VisitList<BoundExpression>(node.Arguments);
			return null;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0005D411 File Offset: 0x0005B611
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIndexOrRangePatternIndexerAccess(BoundIndexOrRangePatternIndexerAccess node)
		{
			this.Visit(node.Receiver);
			this.Visit(node.Argument);
			return null;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0005D42E File Offset: 0x0005B62E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicIndexerAccess(BoundDynamicIndexerAccess node)
		{
			this.Visit(node.Receiver);
			this.VisitList<BoundExpression>(node.Arguments);
			return null;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0005D44A File Offset: 0x0005B64A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLambda(BoundLambda node)
		{
			this.Visit(node.Body);
			return null;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0005D45A File Offset: 0x0005B65A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnboundLambda(UnboundLambda node)
		{
			return null;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0005D45D File Offset: 0x0005B65D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitQueryClause(BoundQueryClause node)
		{
			this.Visit(node.Value);
			return null;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0005D46D File Offset: 0x0005B66D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTypeOrInstanceInitializers(BoundTypeOrInstanceInitializers node)
		{
			this.VisitList<BoundStatement>(node.Statements);
			return null;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0005D47C File Offset: 0x0005B67C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNameOfOperator(BoundNameOfOperator node)
		{
			this.Visit(node.Argument);
			return null;
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0005D48C File Offset: 0x0005B68C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnconvertedInterpolatedString(BoundUnconvertedInterpolatedString node)
		{
			this.VisitList<BoundExpression>(node.Parts);
			return null;
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0005D49B File Offset: 0x0005B69B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitInterpolatedString(BoundInterpolatedString node)
		{
			this.VisitList<BoundExpression>(node.Parts);
			return null;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0005D4AA File Offset: 0x0005B6AA
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitStringInsert(BoundStringInsert node)
		{
			this.Visit(node.Value);
			this.Visit(node.Alignment);
			this.Visit(node.Format);
			return null;
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0005D4D4 File Offset: 0x0005B6D4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIsPatternExpression(BoundIsPatternExpression node)
		{
			this.Visit(node.Expression);
			this.Visit(node.Pattern);
			return null;
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0005D4F1 File Offset: 0x0005B6F1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConstantPattern(BoundConstantPattern node)
		{
			this.Visit(node.Value);
			return null;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0005D501 File Offset: 0x0005B701
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDiscardPattern(BoundDiscardPattern node)
		{
			return null;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0005D504 File Offset: 0x0005B704
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDeclarationPattern(BoundDeclarationPattern node)
		{
			this.Visit(node.VariableAccess);
			this.Visit(node.DeclaredType);
			return null;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0005D521 File Offset: 0x0005B721
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRecursivePattern(BoundRecursivePattern node)
		{
			this.Visit(node.DeclaredType);
			this.VisitList<BoundSubpattern>(node.Deconstruction);
			this.VisitList<BoundSubpattern>(node.Properties);
			this.Visit(node.VariableAccess);
			return null;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0005D556 File Offset: 0x0005B756
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitITuplePattern(BoundITuplePattern node)
		{
			this.VisitList<BoundSubpattern>(node.Subpatterns);
			return null;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0005D565 File Offset: 0x0005B765
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSubpattern(BoundSubpattern node)
		{
			this.Visit(node.Pattern);
			return null;
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0005D575 File Offset: 0x0005B775
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTypePattern(BoundTypePattern node)
		{
			this.Visit(node.DeclaredType);
			return null;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0005D585 File Offset: 0x0005B785
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBinaryPattern(BoundBinaryPattern node)
		{
			this.Visit(node.Left);
			this.Visit(node.Right);
			return null;
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0005D5A2 File Offset: 0x0005B7A2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNegatedPattern(BoundNegatedPattern node)
		{
			this.Visit(node.Negated);
			return null;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0005D5B2 File Offset: 0x0005B7B2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRelationalPattern(BoundRelationalPattern node)
		{
			this.Visit(node.Value);
			return null;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0005D5C2 File Offset: 0x0005B7C2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDiscardExpression(BoundDiscardExpression node)
		{
			return null;
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0005D5C5 File Offset: 0x0005B7C5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitThrowExpression(BoundThrowExpression node)
		{
			this.Visit(node.Expression);
			return null;
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0005D5D5 File Offset: 0x0005B7D5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitOutVariablePendingInference(OutVariablePendingInference node)
		{
			this.Visit(node.ReceiverOpt);
			return null;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0005D5E5 File Offset: 0x0005B7E5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDeconstructionVariablePendingInference(DeconstructionVariablePendingInference node)
		{
			this.Visit(node.ReceiverOpt);
			return null;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0005D5F5 File Offset: 0x0005B7F5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitOutDeconstructVarPendingInference(OutDeconstructVarPendingInference node)
		{
			return null;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0005D5F8 File Offset: 0x0005B7F8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNonConstructorMethodBody(BoundNonConstructorMethodBody node)
		{
			this.Visit(node.BlockBody);
			this.Visit(node.ExpressionBody);
			return null;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0005D615 File Offset: 0x0005B815
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConstructorMethodBody(BoundConstructorMethodBody node)
		{
			this.Visit(node.Initializer);
			this.Visit(node.BlockBody);
			this.Visit(node.ExpressionBody);
			return null;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0005D63F File Offset: 0x0005B83F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitExpressionWithNullability(BoundExpressionWithNullability node)
		{
			this.Visit(node.Expression);
			return null;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0005D64F File Offset: 0x0005B84F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitWithExpression(BoundWithExpression node)
		{
			this.Visit(node.Receiver);
			this.Visit(node.InitializerExpression);
			return null;
		}
	}
}
