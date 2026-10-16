using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000C4 RID: 196
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundTreeVisitor
	{
		// Token: 0x06000F46 RID: 3910 RVA: 0x0005BED8 File Offset: 0x0005A0D8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[DebuggerHidden]
		public virtual BoundNode Visit(BoundNode node)
		{
			if (node != null)
			{
				return node.Accept(this);
			}
			return null;
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0005BEE6 File Offset: 0x0005A0E6
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[DebuggerHidden]
		public virtual BoundNode DefaultVisit(BoundNode node)
		{
			return null;
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0005BEEC File Offset: 0x0005A0EC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[DebuggerStepThrough]
		protected BoundExpression VisitExpressionWithStackGuard(ref int recursionDepth, BoundExpression node)
		{
			recursionDepth++;
			BoundExpression boundExpression;
			if (recursionDepth > 1 || !this.ConvertInsufficientExecutionStackExceptionToCancelledByStackGuardException())
			{
				this.EnsureSufficientExecutionStack(recursionDepth);
				boundExpression = this.VisitExpressionWithoutStackGuard(node);
			}
			else
			{
				boundExpression = this.VisitExpressionWithStackGuard(node);
			}
			recursionDepth--;
			return boundExpression;
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0005BF2D File Offset: 0x0005A12D
		protected virtual void EnsureSufficientExecutionStack(int recursionDepth)
		{
			StackGuard.EnsureSufficientExecutionStack(recursionDepth);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0005BF35 File Offset: 0x0005A135
		protected virtual bool ConvertInsufficientExecutionStackExceptionToCancelledByStackGuardException()
		{
			return true;
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0005BF38 File Offset: 0x0005A138
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression VisitExpressionWithStackGuard(BoundExpression node)
		{
			BoundExpression boundExpression;
			try
			{
				boundExpression = this.VisitExpressionWithoutStackGuard(node);
			}
			catch (InsufficientExecutionStackException ex)
			{
				throw new BoundTreeVisitor.CancelledByStackGuardException(ex, node);
			}
			return boundExpression;
		}

		// Token: 0x06000F4C RID: 3916
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected abstract BoundExpression VisitExpressionWithoutStackGuard(BoundExpression node);

		// Token: 0x06000F4D RID: 3917 RVA: 0x0005BF68 File Offset: 0x0005A168
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitFieldEqualsValue(BoundFieldEqualsValue node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0005BF71 File Offset: 0x0005A171
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitPropertyEqualsValue(BoundPropertyEqualsValue node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0005BF7A File Offset: 0x0005A17A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitParameterEqualsValue(BoundParameterEqualsValue node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0005BF83 File Offset: 0x0005A183
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitGlobalStatementInitializer(BoundGlobalStatementInitializer node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0005BF8C File Offset: 0x0005A18C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDeconstructValuePlaceholder(BoundDeconstructValuePlaceholder node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0005BF95 File Offset: 0x0005A195
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitTupleOperandPlaceholder(BoundTupleOperandPlaceholder node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0005BF9E File Offset: 0x0005A19E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitAwaitableValuePlaceholder(BoundAwaitableValuePlaceholder node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0005BFA7 File Offset: 0x0005A1A7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDisposableValuePlaceholder(BoundDisposableValuePlaceholder node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0005BFB0 File Offset: 0x0005A1B0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitObjectOrCollectionValuePlaceholder(BoundObjectOrCollectionValuePlaceholder node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0005BFB9 File Offset: 0x0005A1B9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDup(BoundDup node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0005BFC2 File Offset: 0x0005A1C2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitPassByCopy(BoundPassByCopy node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0005BFCB File Offset: 0x0005A1CB
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitBadExpression(BoundBadExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0005BFD4 File Offset: 0x0005A1D4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitBadStatement(BoundBadStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0005BFDD File Offset: 0x0005A1DD
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitExtractedFinallyBlock(BoundExtractedFinallyBlock node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0005BFE6 File Offset: 0x0005A1E6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitTypeExpression(BoundTypeExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0005BFEF File Offset: 0x0005A1EF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitTypeOrValueExpression(BoundTypeOrValueExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0005BFF8 File Offset: 0x0005A1F8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitNamespaceExpression(BoundNamespaceExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0005C001 File Offset: 0x0005A201
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitUnaryOperator(BoundUnaryOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0005C00A File Offset: 0x0005A20A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitIncrementOperator(BoundIncrementOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0005C013 File Offset: 0x0005A213
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitAddressOfOperator(BoundAddressOfOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0005C01C File Offset: 0x0005A21C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitUnconvertedAddressOfOperator(BoundUnconvertedAddressOfOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0005C025 File Offset: 0x0005A225
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitFunctionPointerLoad(BoundFunctionPointerLoad node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0005C02E File Offset: 0x0005A22E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitPointerIndirectionOperator(BoundPointerIndirectionOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0005C037 File Offset: 0x0005A237
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitPointerElementAccess(BoundPointerElementAccess node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0005C040 File Offset: 0x0005A240
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitFunctionPointerInvocation(BoundFunctionPointerInvocation node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0005C049 File Offset: 0x0005A249
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitRefTypeOperator(BoundRefTypeOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0005C052 File Offset: 0x0005A252
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitMakeRefOperator(BoundMakeRefOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0005C05B File Offset: 0x0005A25B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitRefValueOperator(BoundRefValueOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0005C064 File Offset: 0x0005A264
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitFromEndIndexExpression(BoundFromEndIndexExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0005C06D File Offset: 0x0005A26D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitRangeExpression(BoundRangeExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0005C076 File Offset: 0x0005A276
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitBinaryOperator(BoundBinaryOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0005C07F File Offset: 0x0005A27F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitTupleBinaryOperator(BoundTupleBinaryOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0005C088 File Offset: 0x0005A288
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitUserDefinedConditionalLogicalOperator(BoundUserDefinedConditionalLogicalOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0005C091 File Offset: 0x0005A291
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitCompoundAssignmentOperator(BoundCompoundAssignmentOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0005C09A File Offset: 0x0005A29A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitAssignmentOperator(BoundAssignmentOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0005C0A3 File Offset: 0x0005A2A3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDeconstructionAssignmentOperator(BoundDeconstructionAssignmentOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0005C0AC File Offset: 0x0005A2AC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitNullCoalescingOperator(BoundNullCoalescingOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0005C0B5 File Offset: 0x0005A2B5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitNullCoalescingAssignmentOperator(BoundNullCoalescingAssignmentOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0005C0BE File Offset: 0x0005A2BE
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitUnconvertedConditionalOperator(BoundUnconvertedConditionalOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0005C0C7 File Offset: 0x0005A2C7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitConditionalOperator(BoundConditionalOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0005C0D0 File Offset: 0x0005A2D0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitArrayAccess(BoundArrayAccess node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0005C0D9 File Offset: 0x0005A2D9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitArrayLength(BoundArrayLength node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0005C0E2 File Offset: 0x0005A2E2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitAwaitableInfo(BoundAwaitableInfo node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0005C0EB File Offset: 0x0005A2EB
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitAwaitExpression(BoundAwaitExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0005C0F4 File Offset: 0x0005A2F4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitTypeOfOperator(BoundTypeOfOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0005C0FD File Offset: 0x0005A2FD
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitMethodDefIndex(BoundMethodDefIndex node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0005C106 File Offset: 0x0005A306
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitMaximumMethodDefIndex(BoundMaximumMethodDefIndex node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0005C10F File Offset: 0x0005A30F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitInstrumentationPayloadRoot(BoundInstrumentationPayloadRoot node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0005C118 File Offset: 0x0005A318
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitModuleVersionId(BoundModuleVersionId node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0005C121 File Offset: 0x0005A321
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitModuleVersionIdString(BoundModuleVersionIdString node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0005C12A File Offset: 0x0005A32A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitSourceDocumentIndex(BoundSourceDocumentIndex node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0005C133 File Offset: 0x0005A333
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitMethodInfo(BoundMethodInfo node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0005C13C File Offset: 0x0005A33C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitFieldInfo(BoundFieldInfo node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0005C145 File Offset: 0x0005A345
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDefaultLiteral(BoundDefaultLiteral node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0005C14E File Offset: 0x0005A34E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDefaultExpression(BoundDefaultExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0005C157 File Offset: 0x0005A357
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitIsOperator(BoundIsOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0005C160 File Offset: 0x0005A360
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitAsOperator(BoundAsOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0005C169 File Offset: 0x0005A369
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitSizeOfOperator(BoundSizeOfOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0005C172 File Offset: 0x0005A372
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitConversion(BoundConversion node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0005C17B File Offset: 0x0005A37B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitReadOnlySpanFromArray(BoundReadOnlySpanFromArray node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0005C184 File Offset: 0x0005A384
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitArgList(BoundArgList node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0005C18D File Offset: 0x0005A38D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitArgListOperator(BoundArgListOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0005C196 File Offset: 0x0005A396
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitFixedLocalCollectionInitializer(BoundFixedLocalCollectionInitializer node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0005C19F File Offset: 0x0005A39F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitSequencePoint(BoundSequencePoint node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0005C1A8 File Offset: 0x0005A3A8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitSequencePointWithSpan(BoundSequencePointWithSpan node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0005C1B1 File Offset: 0x0005A3B1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitSavePreviousSequencePoint(BoundSavePreviousSequencePoint node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0005C1BA File Offset: 0x0005A3BA
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitRestorePreviousSequencePoint(BoundRestorePreviousSequencePoint node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0005C1C3 File Offset: 0x0005A3C3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitStepThroughSequencePoint(BoundStepThroughSequencePoint node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0005C1CC File Offset: 0x0005A3CC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitBlock(BoundBlock node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0005C1D5 File Offset: 0x0005A3D5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitScope(BoundScope node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0005C1DE File Offset: 0x0005A3DE
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitStateMachineScope(BoundStateMachineScope node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0005C1E7 File Offset: 0x0005A3E7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitLocalDeclaration(BoundLocalDeclaration node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0005C1F0 File Offset: 0x0005A3F0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitMultipleLocalDeclarations(BoundMultipleLocalDeclarations node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0005C1F9 File Offset: 0x0005A3F9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitUsingLocalDeclarations(BoundUsingLocalDeclarations node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0005C202 File Offset: 0x0005A402
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitLocalFunctionStatement(BoundLocalFunctionStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0005C20B File Offset: 0x0005A40B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitNoOpStatement(BoundNoOpStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0005C214 File Offset: 0x0005A414
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitReturnStatement(BoundReturnStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0005C21D File Offset: 0x0005A41D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitYieldReturnStatement(BoundYieldReturnStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0005C226 File Offset: 0x0005A426
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitYieldBreakStatement(BoundYieldBreakStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0005C22F File Offset: 0x0005A42F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitThrowStatement(BoundThrowStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0005C238 File Offset: 0x0005A438
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitExpressionStatement(BoundExpressionStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0005C241 File Offset: 0x0005A441
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitBreakStatement(BoundBreakStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0005C24A File Offset: 0x0005A44A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitContinueStatement(BoundContinueStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0005C253 File Offset: 0x0005A453
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitSwitchStatement(BoundSwitchStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0005C25C File Offset: 0x0005A45C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitSwitchDispatch(BoundSwitchDispatch node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0005C265 File Offset: 0x0005A465
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitIfStatement(BoundIfStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0005C26E File Offset: 0x0005A46E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDoStatement(BoundDoStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0005C277 File Offset: 0x0005A477
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitWhileStatement(BoundWhileStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0005C280 File Offset: 0x0005A480
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitForStatement(BoundForStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0005C289 File Offset: 0x0005A489
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitForEachStatement(BoundForEachStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0005C292 File Offset: 0x0005A492
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitForEachDeconstructStep(BoundForEachDeconstructStep node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0005C29B File Offset: 0x0005A49B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitUsingStatement(BoundUsingStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0005C2A4 File Offset: 0x0005A4A4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitFixedStatement(BoundFixedStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0005C2AD File Offset: 0x0005A4AD
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitLockStatement(BoundLockStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0005C2B6 File Offset: 0x0005A4B6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitTryStatement(BoundTryStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0005C2BF File Offset: 0x0005A4BF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitCatchBlock(BoundCatchBlock node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0005C2C8 File Offset: 0x0005A4C8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitLiteral(BoundLiteral node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0005C2D1 File Offset: 0x0005A4D1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitThisReference(BoundThisReference node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0005C2DA File Offset: 0x0005A4DA
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitPreviousSubmissionReference(BoundPreviousSubmissionReference node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0005C2E3 File Offset: 0x0005A4E3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitHostObjectMemberReference(BoundHostObjectMemberReference node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0005C2EC File Offset: 0x0005A4EC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitBaseReference(BoundBaseReference node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0005C2F5 File Offset: 0x0005A4F5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitLocal(BoundLocal node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0005C2FE File Offset: 0x0005A4FE
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitPseudoVariable(BoundPseudoVariable node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0005C307 File Offset: 0x0005A507
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitRangeVariable(BoundRangeVariable node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0005C310 File Offset: 0x0005A510
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitParameter(BoundParameter node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0005C319 File Offset: 0x0005A519
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitLabelStatement(BoundLabelStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0005C322 File Offset: 0x0005A522
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitGotoStatement(BoundGotoStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0005C32B File Offset: 0x0005A52B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitLabeledStatement(BoundLabeledStatement node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0005C334 File Offset: 0x0005A534
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitLabel(BoundLabel node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0005C33D File Offset: 0x0005A53D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitStatementList(BoundStatementList node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0005C346 File Offset: 0x0005A546
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitConditionalGoto(BoundConditionalGoto node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0005C34F File Offset: 0x0005A54F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitSwitchExpressionArm(BoundSwitchExpressionArm node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0005C358 File Offset: 0x0005A558
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitUnconvertedSwitchExpression(BoundUnconvertedSwitchExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0005C361 File Offset: 0x0005A561
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitConvertedSwitchExpression(BoundConvertedSwitchExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0005C36A File Offset: 0x0005A56A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDecisionDag(BoundDecisionDag node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0005C373 File Offset: 0x0005A573
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitEvaluationDecisionDagNode(BoundEvaluationDecisionDagNode node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0005C37C File Offset: 0x0005A57C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitTestDecisionDagNode(BoundTestDecisionDagNode node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0005C385 File Offset: 0x0005A585
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitWhenDecisionDagNode(BoundWhenDecisionDagNode node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0005C38E File Offset: 0x0005A58E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitLeafDecisionDagNode(BoundLeafDecisionDagNode node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0005C397 File Offset: 0x0005A597
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDagTemp(BoundDagTemp node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0005C3A0 File Offset: 0x0005A5A0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDagTypeTest(BoundDagTypeTest node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0005C3A9 File Offset: 0x0005A5A9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDagNonNullTest(BoundDagNonNullTest node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0005C3B2 File Offset: 0x0005A5B2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDagExplicitNullTest(BoundDagExplicitNullTest node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0005C3BB File Offset: 0x0005A5BB
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDagValueTest(BoundDagValueTest node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0005C3C4 File Offset: 0x0005A5C4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDagRelationalTest(BoundDagRelationalTest node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0005C3CD File Offset: 0x0005A5CD
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDagDeconstructEvaluation(BoundDagDeconstructEvaluation node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0005C3D6 File Offset: 0x0005A5D6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDagTypeEvaluation(BoundDagTypeEvaluation node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0005C3DF File Offset: 0x0005A5DF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDagFieldEvaluation(BoundDagFieldEvaluation node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0005C3E8 File Offset: 0x0005A5E8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDagPropertyEvaluation(BoundDagPropertyEvaluation node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0005C3F1 File Offset: 0x0005A5F1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDagIndexEvaluation(BoundDagIndexEvaluation node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0005C3FA File Offset: 0x0005A5FA
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitSwitchSection(BoundSwitchSection node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0005C403 File Offset: 0x0005A603
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitSwitchLabel(BoundSwitchLabel node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0005C40C File Offset: 0x0005A60C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitSequencePointExpression(BoundSequencePointExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0005C415 File Offset: 0x0005A615
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitSequence(BoundSequence node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0005C41E File Offset: 0x0005A61E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitSpillSequence(BoundSpillSequence node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0005C427 File Offset: 0x0005A627
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDynamicMemberAccess(BoundDynamicMemberAccess node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0005C430 File Offset: 0x0005A630
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDynamicInvocation(BoundDynamicInvocation node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0005C439 File Offset: 0x0005A639
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitConditionalAccess(BoundConditionalAccess node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0005C442 File Offset: 0x0005A642
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitLoweredConditionalAccess(BoundLoweredConditionalAccess node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0005C44B File Offset: 0x0005A64B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitConditionalReceiver(BoundConditionalReceiver node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0005C454 File Offset: 0x0005A654
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitComplexConditionalReceiver(BoundComplexConditionalReceiver node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0005C45D File Offset: 0x0005A65D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitMethodGroup(BoundMethodGroup node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0005C466 File Offset: 0x0005A666
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitPropertyGroup(BoundPropertyGroup node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0005C46F File Offset: 0x0005A66F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitCall(BoundCall node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0005C478 File Offset: 0x0005A678
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitEventAssignmentOperator(BoundEventAssignmentOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0005C481 File Offset: 0x0005A681
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitAttribute(BoundAttribute node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0005C48A File Offset: 0x0005A68A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitUnconvertedObjectCreationExpression(BoundUnconvertedObjectCreationExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0005C493 File Offset: 0x0005A693
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitObjectCreationExpression(BoundObjectCreationExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0005C49C File Offset: 0x0005A69C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitTupleLiteral(BoundTupleLiteral node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0005C4A5 File Offset: 0x0005A6A5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitConvertedTupleLiteral(BoundConvertedTupleLiteral node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0005C4AE File Offset: 0x0005A6AE
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDynamicObjectCreationExpression(BoundDynamicObjectCreationExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0005C4B7 File Offset: 0x0005A6B7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitNoPiaObjectCreationExpression(BoundNoPiaObjectCreationExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0005C4C0 File Offset: 0x0005A6C0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitObjectInitializerExpression(BoundObjectInitializerExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0005C4C9 File Offset: 0x0005A6C9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitObjectInitializerMember(BoundObjectInitializerMember node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0005C4D2 File Offset: 0x0005A6D2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDynamicObjectInitializerMember(BoundDynamicObjectInitializerMember node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0005C4DB File Offset: 0x0005A6DB
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitCollectionInitializerExpression(BoundCollectionInitializerExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0005C4E4 File Offset: 0x0005A6E4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitCollectionElementInitializer(BoundCollectionElementInitializer node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0005C4ED File Offset: 0x0005A6ED
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDynamicCollectionElementInitializer(BoundDynamicCollectionElementInitializer node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0005C4F6 File Offset: 0x0005A6F6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitImplicitReceiver(BoundImplicitReceiver node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0005C4FF File Offset: 0x0005A6FF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitAnonymousObjectCreationExpression(BoundAnonymousObjectCreationExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0005C508 File Offset: 0x0005A708
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitAnonymousPropertyDeclaration(BoundAnonymousPropertyDeclaration node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0005C511 File Offset: 0x0005A711
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitNewT(BoundNewT node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0005C51A File Offset: 0x0005A71A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDelegateCreationExpression(BoundDelegateCreationExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0005C523 File Offset: 0x0005A723
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitArrayCreation(BoundArrayCreation node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0005C52C File Offset: 0x0005A72C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitArrayInitialization(BoundArrayInitialization node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0005C535 File Offset: 0x0005A735
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitStackAllocArrayCreation(BoundStackAllocArrayCreation node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0005C53E File Offset: 0x0005A73E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitConvertedStackAllocExpression(BoundConvertedStackAllocExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0005C547 File Offset: 0x0005A747
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitFieldAccess(BoundFieldAccess node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0005C550 File Offset: 0x0005A750
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitHoistedFieldAccess(BoundHoistedFieldAccess node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0005C559 File Offset: 0x0005A759
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitPropertyAccess(BoundPropertyAccess node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0005C562 File Offset: 0x0005A762
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitEventAccess(BoundEventAccess node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0005C56B File Offset: 0x0005A76B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitIndexerAccess(BoundIndexerAccess node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0005C574 File Offset: 0x0005A774
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitIndexOrRangePatternIndexerAccess(BoundIndexOrRangePatternIndexerAccess node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0005C57D File Offset: 0x0005A77D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDynamicIndexerAccess(BoundDynamicIndexerAccess node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0005C586 File Offset: 0x0005A786
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitLambda(BoundLambda node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0005C58F File Offset: 0x0005A78F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitUnboundLambda(UnboundLambda node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0005C598 File Offset: 0x0005A798
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitQueryClause(BoundQueryClause node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0005C5A1 File Offset: 0x0005A7A1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitTypeOrInstanceInitializers(BoundTypeOrInstanceInitializers node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0005C5AA File Offset: 0x0005A7AA
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitNameOfOperator(BoundNameOfOperator node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0005C5B3 File Offset: 0x0005A7B3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitUnconvertedInterpolatedString(BoundUnconvertedInterpolatedString node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0005C5BC File Offset: 0x0005A7BC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitInterpolatedString(BoundInterpolatedString node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0005C5C5 File Offset: 0x0005A7C5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitStringInsert(BoundStringInsert node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0005C5CE File Offset: 0x0005A7CE
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitIsPatternExpression(BoundIsPatternExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0005C5D7 File Offset: 0x0005A7D7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitConstantPattern(BoundConstantPattern node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0005C5E0 File Offset: 0x0005A7E0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDiscardPattern(BoundDiscardPattern node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0005C5E9 File Offset: 0x0005A7E9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDeclarationPattern(BoundDeclarationPattern node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0005C5F2 File Offset: 0x0005A7F2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitRecursivePattern(BoundRecursivePattern node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0005C5FB File Offset: 0x0005A7FB
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitITuplePattern(BoundITuplePattern node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0005C604 File Offset: 0x0005A804
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitSubpattern(BoundSubpattern node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0005C60D File Offset: 0x0005A80D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitTypePattern(BoundTypePattern node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0005C616 File Offset: 0x0005A816
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitBinaryPattern(BoundBinaryPattern node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0005C61F File Offset: 0x0005A81F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitNegatedPattern(BoundNegatedPattern node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0005C628 File Offset: 0x0005A828
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitRelationalPattern(BoundRelationalPattern node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0005C631 File Offset: 0x0005A831
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDiscardExpression(BoundDiscardExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0005C63A File Offset: 0x0005A83A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitThrowExpression(BoundThrowExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0005C643 File Offset: 0x0005A843
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitOutVariablePendingInference(OutVariablePendingInference node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0005C64C File Offset: 0x0005A84C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitDeconstructionVariablePendingInference(DeconstructionVariablePendingInference node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0005C655 File Offset: 0x0005A855
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitOutDeconstructVarPendingInference(OutDeconstructVarPendingInference node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0005C65E File Offset: 0x0005A85E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitNonConstructorMethodBody(BoundNonConstructorMethodBody node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0005C667 File Offset: 0x0005A867
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitConstructorMethodBody(BoundConstructorMethodBody node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0005C670 File Offset: 0x0005A870
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitExpressionWithNullability(BoundExpressionWithNullability node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0005C679 File Offset: 0x0005A879
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode VisitWithExpression(BoundWithExpression node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x02000688 RID: 1672
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public class CancelledByStackGuardException : Exception
		{
			// Token: 0x06008325 RID: 33573 RVA: 0x001DB97B File Offset: 0x001D9B7B
			public CancelledByStackGuardException(Exception inner, BoundNode node)
				: base(inner.Message, inner)
			{
				this.Node = node;
			}

			// Token: 0x06008326 RID: 33574 RVA: 0x001DB991 File Offset: 0x001D9B91
			public void AddAnError(DiagnosticBag diagnostics)
			{
				diagnostics.Add(ErrorCode.ERR_InsufficientStack, BoundTreeVisitor.CancelledByStackGuardException.GetTooLongOrComplexExpressionErrorLocation(this.Node));
			}

			// Token: 0x06008327 RID: 33575 RVA: 0x001DB9AA File Offset: 0x001D9BAA
			public void AddAnError(BindingDiagnosticBag diagnostics)
			{
				diagnostics.Add(ErrorCode.ERR_InsufficientStack, BoundTreeVisitor.CancelledByStackGuardException.GetTooLongOrComplexExpressionErrorLocation(this.Node));
			}

			// Token: 0x06008328 RID: 33576 RVA: 0x001DB9C4 File Offset: 0x001D9BC4
			public static Location GetTooLongOrComplexExpressionErrorLocation(BoundNode node)
			{
				SyntaxNode syntaxNode = node.Syntax;
				if (!(syntaxNode is ExpressionSyntax))
				{
					syntaxNode = Enumerable.FirstOrDefault<ExpressionSyntax>(Enumerable.OfType<ExpressionSyntax>(syntaxNode.DescendantNodes((SyntaxNode n) => !(n is ExpressionSyntax), false))) ?? syntaxNode;
				}
				return syntaxNode.GetFirstToken(false, false, false, false).GetLocation();
			}

			// Token: 0x040020A0 RID: 8352
			public readonly BoundNode Node;
		}
	}
}
