using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.CodeGen
{
	// Token: 0x020005F9 RID: 1529
	internal sealed class StackOptimizerPass1 : BoundTreeRewriter
	{
		// Token: 0x060080FF RID: 33023 RVA: 0x001D5548 File Offset: 0x001D3748
		private StackOptimizerPass1(Dictionary<LocalSymbol, LocalDefUseInfo> locals, ArrayBuilder<ValueTuple<BoundExpression, ExprContext>> evalStack, bool debugFriendly)
		{
			this._locals = locals;
			this._evalStack = evalStack;
			this._debugFriendly = debugFriendly;
			this.DeclareLocal(StackOptimizerPass1.empty, 0);
			this.RecordDummyWrite(StackOptimizerPass1.empty);
		}

		// Token: 0x06008100 RID: 33024 RVA: 0x001D55A4 File Offset: 0x001D37A4
		public static BoundNode Analyze(BoundNode node, Dictionary<LocalSymbol, LocalDefUseInfo> locals, bool debugFriendly)
		{
			ArrayBuilder<ValueTuple<BoundExpression, ExprContext>> instance = ArrayBuilder<ValueTuple<BoundExpression, ExprContext>>.GetInstance();
			BoundNode boundNode = new StackOptimizerPass1(locals, instance, debugFriendly).Visit(node);
			instance.Free();
			return boundNode;
		}

		// Token: 0x06008101 RID: 33025 RVA: 0x001D55CC File Offset: 0x001D37CC
		public override BoundNode Visit(BoundNode node)
		{
			BoundExpression boundExpression = node as BoundExpression;
			BoundNode boundNode;
			if (boundExpression != null)
			{
				boundNode = this.VisitExpression(boundExpression, ExprContext.Value);
			}
			else
			{
				boundNode = this.VisitStatement(node);
			}
			return boundNode;
		}

		// Token: 0x06008102 RID: 33026 RVA: 0x001D55F8 File Offset: 0x001D37F8
		private BoundExpression VisitExpressionCore(BoundExpression node, ExprContext context)
		{
			ExprContext context2 = this._context;
			int num = this.StackDepth();
			this._context = context;
			BoundExpression boundExpression = ((node.ConstantValue == null) ? (node = (BoundExpression)base.Visit(node)) : node);
			this._context = context2;
			this._counter++;
			switch (context)
			{
			case ExprContext.Sideeffects:
				this.SetStackDepth(num);
				break;
			case ExprContext.Value:
			case ExprContext.Address:
			case ExprContext.Box:
				this.SetStackDepth(num);
				this.PushEvalStack(node, context);
				break;
			case ExprContext.AssignmentTarget:
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(context);
			}
			return boundExpression;
		}

		// Token: 0x06008103 RID: 33027 RVA: 0x001D5698 File Offset: 0x001D3898
		private BoundExpression VisitExpression(BoundExpression node, ExprContext context)
		{
			this._recursionDepth++;
			BoundExpression boundExpression;
			if (this._recursionDepth > 1)
			{
				StackGuard.EnsureSufficientExecutionStack(this._recursionDepth);
				boundExpression = this.VisitExpressionCore(node, context);
			}
			else
			{
				boundExpression = this.VisitExpressionCoreWithStackGuard(node, context);
			}
			this._recursionDepth--;
			return boundExpression;
		}

		// Token: 0x06008104 RID: 33028 RVA: 0x001D56EC File Offset: 0x001D38EC
		private BoundExpression VisitExpressionCoreWithStackGuard(BoundExpression node, ExprContext context)
		{
			BoundExpression boundExpression;
			try
			{
				boundExpression = this.VisitExpressionCore(node, context);
			}
			catch (InsufficientExecutionStackException ex)
			{
				throw new BoundTreeVisitor.CancelledByStackGuardException(ex, node);
			}
			return boundExpression;
		}

		// Token: 0x06008105 RID: 33029 RVA: 0x001D571C File Offset: 0x001D391C
		protected override BoundExpression VisitExpressionWithoutStackGuard(BoundExpression node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06008106 RID: 33030 RVA: 0x001D5723 File Offset: 0x001D3923
		private void PushEvalStack(BoundExpression result, ExprContext context)
		{
			this._evalStack.Add(new ValueTuple<BoundExpression, ExprContext>(result, context));
		}

		// Token: 0x06008107 RID: 33031 RVA: 0x001D5737 File Offset: 0x001D3937
		private int StackDepth()
		{
			return this._evalStack.Count;
		}

		// Token: 0x06008108 RID: 33032 RVA: 0x001D5744 File Offset: 0x001D3944
		private bool EvalStackIsEmpty()
		{
			return this.StackDepth() == 0;
		}

		// Token: 0x06008109 RID: 33033 RVA: 0x001D574F File Offset: 0x001D394F
		private void SetStackDepth(int depth)
		{
			this._evalStack.Clip(depth);
		}

		// Token: 0x0600810A RID: 33034 RVA: 0x001D575D File Offset: 0x001D395D
		private void PopEvalStack()
		{
			this.SetStackDepth(this._evalStack.Count - 1);
		}

		// Token: 0x0600810B RID: 33035 RVA: 0x001D5772 File Offset: 0x001D3972
		private void ClearEvalStack()
		{
			this._evalStack.Clear();
		}

		// Token: 0x0600810C RID: 33036 RVA: 0x001D577F File Offset: 0x001D397F
		public BoundNode VisitStatement(BoundNode node)
		{
			return this.VisitSideEffect(node);
		}

		// Token: 0x0600810D RID: 33037 RVA: 0x001D5788 File Offset: 0x001D3988
		public BoundNode VisitSideEffect(BoundNode node)
		{
			int num = this.StackDepth();
			ExprContext context = this._context;
			BoundNode boundNode = base.Visit(node);
			if (this._debugFriendly)
			{
				this.EnsureOnlyEvalStack();
			}
			this._context = context;
			this.SetStackDepth(num);
			this._counter++;
			return boundNode;
		}

		// Token: 0x0600810E RID: 33038 RVA: 0x001D57D4 File Offset: 0x001D39D4
		public override BoundNode VisitConversion(BoundConversion node)
		{
			ExprContext exprContext = ((this._context == ExprContext.Sideeffects && !node.ConversionHasSideEffects()) ? ExprContext.Sideeffects : ExprContext.Value);
			return node.UpdateOperand(this.VisitExpression(node.Operand, exprContext));
		}

		// Token: 0x0600810F RID: 33039 RVA: 0x001D580C File Offset: 0x001D3A0C
		public override BoundNode VisitPassByCopy(BoundPassByCopy node)
		{
			ExprContext exprContext = ((this._context == ExprContext.Sideeffects) ? ExprContext.Sideeffects : ExprContext.Value);
			return node.Update(this.VisitExpression(node.Expression, exprContext), node.Type);
		}

		// Token: 0x06008110 RID: 33040 RVA: 0x001D5840 File Offset: 0x001D3A40
		public override BoundNode VisitBlock(BoundBlock node)
		{
			this.DeclareLocals(node.Locals, 0);
			return base.VisitBlock(node);
		}

		// Token: 0x06008111 RID: 33041 RVA: 0x001D5858 File Offset: 0x001D3A58
		public override BoundNode VisitSequence(BoundSequence node)
		{
			int num = this.StackDepth();
			ImmutableArray<LocalSymbol> locals = node.Locals;
			if (!locals.IsDefaultOrEmpty)
			{
				if (this._context == ExprContext.Sideeffects)
				{
					foreach (LocalSymbol localSymbol in locals)
					{
						if (this.IsNestedLocalOfCompoundOperator(localSymbol, node))
						{
							this.DeclareLocal(localSymbol, num + 1);
						}
						else
						{
							this.DeclareLocal(localSymbol, num);
						}
					}
				}
				else
				{
					this.DeclareLocals(locals, num);
				}
			}
			ExprContext context = this._context;
			ImmutableArray<BoundExpression> sideEffects = node.SideEffects;
			ArrayBuilder<BoundExpression> arrayBuilder = null;
			if (!sideEffects.IsDefault)
			{
				for (int i = 0; i < sideEffects.Length; i++)
				{
					BoundExpression boundExpression = sideEffects[i];
					BoundExpression boundExpression2 = this.VisitExpression(boundExpression, ExprContext.Sideeffects);
					if (arrayBuilder == null && boundExpression2 != boundExpression)
					{
						arrayBuilder = ArrayBuilder<BoundExpression>.GetInstance();
						arrayBuilder.AddRange(sideEffects, i);
					}
					if (arrayBuilder != null)
					{
						arrayBuilder.Add(boundExpression2);
					}
				}
			}
			BoundExpression boundExpression3 = this.VisitExpression(node.Value, context);
			return node.Update(node.Locals, (arrayBuilder != null) ? arrayBuilder.ToImmutableAndFree() : sideEffects, boundExpression3, node.Type);
		}

		// Token: 0x06008112 RID: 33042 RVA: 0x001D596C File Offset: 0x001D3B6C
		private bool IsNestedLocalOfCompoundOperator(LocalSymbol local, BoundSequence node)
		{
			BoundExpression value = node.Value;
			if (value != null && value.Kind == BoundKind.Local && ((BoundLocal)value).LocalSymbol == local)
			{
				ImmutableArray<BoundExpression> sideEffects = node.SideEffects;
				BoundExpression boundExpression = sideEffects.LastOrDefault<BoundExpression>();
				if (boundExpression != null && boundExpression.Kind == BoundKind.AssignmentOperator)
				{
					BoundAssignmentOperator boundAssignmentOperator = (BoundAssignmentOperator)boundExpression;
					if (StackOptimizerPass1.IsIndirectOrInstanceFieldAssignment(boundAssignmentOperator) && boundAssignmentOperator.Right.Kind == BoundKind.Sequence)
					{
						StackOptimizerPass1.LocalUsedWalker localUsedWalker = new StackOptimizerPass1.LocalUsedWalker(local, this._recursionDepth);
						for (int i = 0; i < sideEffects.Length - 1; i++)
						{
							if (localUsedWalker.IsLocalUsedIn(sideEffects[i]))
							{
								return false;
							}
						}
						return !localUsedWalker.IsLocalUsedIn(boundAssignmentOperator.Left);
					}
				}
			}
			return false;
		}

		// Token: 0x06008113 RID: 33043 RVA: 0x001D5A33 File Offset: 0x001D3C33
		public override BoundNode VisitExpressionStatement(BoundExpressionStatement node)
		{
			return node.Update(this.VisitExpression(node.Expression, ExprContext.Sideeffects));
		}

		// Token: 0x06008114 RID: 33044 RVA: 0x001D5A48 File Offset: 0x001D3C48
		public override BoundNode VisitLocal(BoundLocal node)
		{
			if (node.ConstantValueOpt == null)
			{
				switch (this._context)
				{
				case ExprContext.Value:
				case ExprContext.Box:
					this.RecordVarRead(node.LocalSymbol);
					break;
				case ExprContext.Address:
					if (node.LocalSymbol.RefKind != RefKind.None)
					{
						this.RecordVarRead(node.LocalSymbol);
					}
					else
					{
						this.RecordVarRef(node.LocalSymbol);
					}
					break;
				case ExprContext.AssignmentTarget:
					this._assignmentLocal = node;
					break;
				}
			}
			return base.VisitLocal(node);
		}

		// Token: 0x06008115 RID: 33045 RVA: 0x001D5AD0 File Offset: 0x001D3CD0
		public override BoundNode VisitAssignmentOperator(BoundAssignmentOperator node)
		{
			BoundSequence boundSequence = node.Left as BoundSequence;
			if (boundSequence != null)
			{
				BoundExpression boundExpression = boundSequence.Update(boundSequence.Locals, boundSequence.SideEffects, node.Update(boundSequence.Value, node.Right, node.IsRef, node.Type), boundSequence.Type);
				boundExpression = (BoundExpression)this.Visit(boundExpression);
				this._counter--;
				return boundExpression;
			}
			bool flag = StackOptimizerPass1.IsIndirectAssignment(node);
			BoundExpression boundExpression2 = this.VisitExpression(node.Left, flag ? ExprContext.Address : ExprContext.AssignmentTarget);
			BoundLocal assignmentLocal = this._assignmentLocal;
			this._assignmentLocal = null;
			ExprContext exprContext;
			if (node.IsRef || this._context == ExprContext.Address)
			{
				exprContext = ExprContext.Address;
			}
			else
			{
				exprContext = ExprContext.Value;
			}
			BoundExpression boundExpression3 = node.Right;
			bool flag2 = boundExpression3.Kind == BoundKind.ObjectCreationExpression && boundExpression3.Type.IsVerifierValue() && ((BoundObjectCreationExpression)boundExpression3).Constructor.ParameterCount != 0;
			if (flag2)
			{
				this.PushEvalStack(null, ExprContext.None);
			}
			boundExpression3 = this.VisitExpression(node.Right, exprContext);
			if (flag2)
			{
				this.PopEvalStack();
			}
			if (assignmentLocal != null)
			{
				LocalSymbol localSymbol = assignmentLocal.LocalSymbol;
				if (localSymbol.RefKind == RefKind.In && (this._context == ExprContext.Address || this._context == ExprContext.Value))
				{
					this.ShouldNotSchedule(localSymbol);
				}
				if (this.CanScheduleToStack(localSymbol) && assignmentLocal.Type.IsPointerOrFunctionPointer() && boundExpression3.Kind == BoundKind.Conversion && ((BoundConversion)boundExpression3).ConversionKind.IsPointerConversion())
				{
					this.ShouldNotSchedule(localSymbol);
				}
				this.RecordVarWrite(localSymbol);
			}
			return node.Update(boundExpression2, boundExpression3, node.IsRef, node.Type);
		}

		// Token: 0x06008116 RID: 33046 RVA: 0x001D5C70 File Offset: 0x001D3E70
		internal static bool IsFixedBufferAssignmentToRefLocal(BoundExpression left, BoundExpression right, bool isRef)
		{
			if (isRef)
			{
				BoundFieldAccess boundFieldAccess = right as BoundFieldAccess;
				if (boundFieldAccess != null && boundFieldAccess.FieldSymbol.IsFixedSizeBuffer)
				{
					return left.Type.Equals(((PointerTypeSymbol)right.Type).PointedAtType, TypeCompareKind.AllIgnoreOptions);
				}
			}
			return false;
		}

		// Token: 0x06008117 RID: 33047 RVA: 0x001D5CB8 File Offset: 0x001D3EB8
		private static bool IsIndirectAssignment(BoundAssignmentOperator node)
		{
			BoundExpression left = node.Left;
			BoundKind kind = left.Kind;
			if (kind <= BoundKind.ArrayAccess)
			{
				if (kind <= BoundKind.RefValueOperator)
				{
					if (kind != BoundKind.PointerIndirectionOperator)
					{
						if (kind == BoundKind.FunctionPointerInvocation)
						{
							return true;
						}
						if (kind != BoundKind.RefValueOperator)
						{
							goto IL_00E7;
						}
					}
				}
				else
				{
					if (kind == BoundKind.AssignmentOperator)
					{
						return true;
					}
					if (kind == BoundKind.ConditionalOperator)
					{
						return true;
					}
					if (kind != BoundKind.ArrayAccess)
					{
						goto IL_00E7;
					}
					return false;
				}
			}
			else if (kind <= BoundKind.Parameter)
			{
				if (kind - BoundKind.InstrumentationPayloadRoot <= 1)
				{
					return false;
				}
				if (kind == BoundKind.ThisReference)
				{
					return true;
				}
				switch (kind)
				{
				case BoundKind.Local:
					return ((BoundLocal)left).LocalSymbol.RefKind != RefKind.None && !node.IsRef;
				case BoundKind.PseudoVariable:
					break;
				case BoundKind.RangeVariable:
					goto IL_00E7;
				case BoundKind.Parameter:
					return ((BoundParameter)left).ParameterSymbol.RefKind != RefKind.None && !node.IsRef;
				default:
					goto IL_00E7;
				}
			}
			else
			{
				if (kind == BoundKind.Sequence)
				{
					return false;
				}
				if (kind == BoundKind.Call)
				{
					return true;
				}
				if (kind != BoundKind.FieldAccess)
				{
					goto IL_00E7;
				}
				return false;
			}
			return true;
			IL_00E7:
			throw ExceptionUtilities.UnexpectedValue(left.Kind);
		}

		// Token: 0x06008118 RID: 33048 RVA: 0x001D5DBC File Offset: 0x001D3FBC
		private static bool IsIndirectOrInstanceFieldAssignment(BoundAssignmentOperator node)
		{
			BoundExpression left = node.Left;
			if (left.Kind == BoundKind.FieldAccess)
			{
				return !((BoundFieldAccess)left).FieldSymbol.IsStatic;
			}
			return StackOptimizerPass1.IsIndirectAssignment(node);
		}

		// Token: 0x06008119 RID: 33049 RVA: 0x001D5DF8 File Offset: 0x001D3FF8
		public override BoundNode VisitCall(BoundCall node)
		{
			BoundExpression boundExpression = node.ReceiverOpt;
			if (node.Method.RequiresInstanceReceiver)
			{
				boundExpression = this.VisitCallReceiver(boundExpression);
			}
			else
			{
				this._counter++;
				boundExpression = null;
			}
			MethodSymbol method = node.Method;
			ImmutableArray<BoundExpression> immutableArray = this.VisitArguments(node.Arguments, method.Parameters, node.ArgumentRefKindsOpt);
			return node.Update(boundExpression, method, immutableArray);
		}

		// Token: 0x0600811A RID: 33050 RVA: 0x001D5E5C File Offset: 0x001D405C
		private BoundExpression VisitCallReceiver(BoundExpression receiver)
		{
			TypeSymbol type = receiver.Type;
			ExprContext exprContext;
			if (type.IsReferenceType)
			{
				if (type.IsTypeParameter())
				{
					exprContext = ExprContext.Box;
				}
				else
				{
					exprContext = ExprContext.Value;
				}
			}
			else
			{
				exprContext = ExprContext.Address;
			}
			receiver = this.VisitExpression(receiver, exprContext);
			return receiver;
		}

		// Token: 0x0600811B RID: 33051 RVA: 0x001D5E98 File Offset: 0x001D4098
		private ImmutableArray<BoundExpression> VisitArguments(ImmutableArray<BoundExpression> arguments, ImmutableArray<ParameterSymbol> parameters, ImmutableArray<RefKind> argRefKindsOpt)
		{
			ArrayBuilder<BoundExpression> arrayBuilder = null;
			for (int i = 0; i < arguments.Length; i++)
			{
				RefKind argumentRefKind = CodeGenerator.GetArgumentRefKind(arguments, parameters, argRefKindsOpt, i);
				this.VisitArgument(arguments, ref arrayBuilder, i, argumentRefKind);
			}
			if (arrayBuilder == null)
			{
				return arguments;
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x0600811C RID: 33052 RVA: 0x001D5EDC File Offset: 0x001D40DC
		private void VisitArgument(ImmutableArray<BoundExpression> arguments, ref ArrayBuilder<BoundExpression> rewrittenArguments, int i, RefKind argRefKind)
		{
			ExprContext exprContext = ((argRefKind == RefKind.None) ? ExprContext.Value : ExprContext.Address);
			BoundExpression boundExpression = arguments[i];
			BoundExpression boundExpression2 = this.VisitExpression(boundExpression, exprContext);
			if (rewrittenArguments == null && boundExpression != boundExpression2)
			{
				rewrittenArguments = ArrayBuilder<BoundExpression>.GetInstance();
				rewrittenArguments.AddRange(arguments, i);
			}
			if (rewrittenArguments != null)
			{
				rewrittenArguments.Add(boundExpression2);
			}
		}

		// Token: 0x0600811D RID: 33053 RVA: 0x001D5F28 File Offset: 0x001D4128
		public override BoundNode VisitArgListOperator(BoundArgListOperator node)
		{
			ArrayBuilder<BoundExpression> arrayBuilder = null;
			ImmutableArray<BoundExpression> arguments = node.Arguments;
			ImmutableArray<RefKind> argumentRefKindsOpt = node.ArgumentRefKindsOpt;
			for (int i = 0; i < arguments.Length; i++)
			{
				RefKind refKind = (argumentRefKindsOpt.IsDefaultOrEmpty ? RefKind.None : argumentRefKindsOpt[i]);
				this.VisitArgument(arguments, ref arrayBuilder, i, refKind);
			}
			return node.Update((arrayBuilder != null) ? arrayBuilder.ToImmutableAndFree() : arguments, argumentRefKindsOpt, node.Type);
		}

		// Token: 0x0600811E RID: 33054 RVA: 0x001D5F94 File Offset: 0x001D4194
		public override BoundNode VisitMakeRefOperator(BoundMakeRefOperator node)
		{
			BoundExpression boundExpression = this.VisitExpression(node.Operand, ExprContext.Address);
			return node.Update(boundExpression, node.Type);
		}

		// Token: 0x0600811F RID: 33055 RVA: 0x001D5FBC File Offset: 0x001D41BC
		public override BoundNode VisitObjectCreationExpression(BoundObjectCreationExpression node)
		{
			MethodSymbol constructor = node.Constructor;
			ImmutableArray<BoundExpression> immutableArray = this.VisitArguments(node.Arguments, constructor.Parameters, node.ArgumentRefKindsOpt);
			return node.Update(constructor, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.Expanded, node.ArgsToParamsOpt, node.DefaultArguments, node.ConstantValue, null, node.Type);
		}

		// Token: 0x06008120 RID: 33056 RVA: 0x001D601C File Offset: 0x001D421C
		public override BoundNode VisitArrayAccess(BoundArrayAccess node)
		{
			ExprContext context = this._context;
			this._context = ExprContext.Value;
			BoundNode boundNode = base.VisitArrayAccess(node);
			this._context = context;
			return boundNode;
		}

		// Token: 0x06008121 RID: 33057 RVA: 0x001D6048 File Offset: 0x001D4248
		public override BoundNode VisitFieldAccess(BoundFieldAccess node)
		{
			FieldSymbol fieldSymbol = node.FieldSymbol;
			BoundExpression boundExpression = node.ReceiverOpt;
			if (!fieldSymbol.IsStatic)
			{
				if (boundExpression.Type.IsTypeParameter())
				{
					boundExpression = this.VisitExpression(boundExpression, ExprContext.Box);
				}
				else if (boundExpression.Type.IsValueType && (this._context == ExprContext.AssignmentTarget || this._context == ExprContext.Address || CodeGenerator.FieldLoadMustUseRef(boundExpression)))
				{
					boundExpression = this.VisitExpression(boundExpression, ExprContext.Address);
				}
				else
				{
					boundExpression = this.VisitExpression(boundExpression, ExprContext.Value);
				}
			}
			else
			{
				this._counter++;
				boundExpression = null;
			}
			return node.Update(boundExpression, fieldSymbol, node.ConstantValueOpt, node.ResultKind, node.Type);
		}

		// Token: 0x06008122 RID: 33058 RVA: 0x001D60EA File Offset: 0x001D42EA
		public override BoundNode VisitLabelStatement(BoundLabelStatement node)
		{
			this.RecordLabel(node.Label);
			return base.VisitLabelStatement(node);
		}

		// Token: 0x06008123 RID: 33059 RVA: 0x001D60FF File Offset: 0x001D42FF
		public override BoundNode VisitLabel(BoundLabel node)
		{
			return node;
		}

		// Token: 0x06008124 RID: 33060 RVA: 0x001D6102 File Offset: 0x001D4302
		public override BoundNode VisitIsPatternExpression(BoundIsPatternExpression node)
		{
			return node;
		}

		// Token: 0x06008125 RID: 33061 RVA: 0x001D6105 File Offset: 0x001D4305
		public override BoundNode VisitGotoStatement(BoundGotoStatement node)
		{
			BoundNode boundNode = base.VisitGotoStatement(node);
			this.RecordBranch(node.Label);
			return boundNode;
		}

		// Token: 0x06008126 RID: 33062 RVA: 0x001D611A File Offset: 0x001D431A
		public override BoundNode VisitConditionalGoto(BoundConditionalGoto node)
		{
			BoundNode boundNode = base.VisitConditionalGoto(node);
			this.PopEvalStack();
			this.RecordBranch(node.Label);
			return boundNode;
		}

		// Token: 0x06008127 RID: 33063 RVA: 0x001D6138 File Offset: 0x001D4338
		public override BoundNode VisitSwitchDispatch(BoundSwitchDispatch node)
		{
			BoundExpression boundExpression = node.Expression;
			if (boundExpression.Kind == BoundKind.Local)
			{
				LocalSymbol localSymbol = ((BoundLocal)boundExpression).LocalSymbol;
				if (localSymbol.RefKind == RefKind.None)
				{
					this.ShouldNotSchedule(localSymbol);
				}
			}
			boundExpression = (BoundExpression)this.Visit(boundExpression);
			this.PopEvalStack();
			this.EnsureOnlyEvalStack();
			this.RecordBranch(node.DefaultLabel);
			foreach (ValueTuple<ConstantValue, LabelSymbol> valueTuple in node.Cases)
			{
				LabelSymbol item = valueTuple.Item2;
				this.RecordBranch(item);
			}
			return node.Update(boundExpression, node.Cases, node.DefaultLabel, node.EqualityMethod);
		}

		// Token: 0x06008128 RID: 33064 RVA: 0x001D61E0 File Offset: 0x001D43E0
		public override BoundNode VisitConditionalOperator(BoundConditionalOperator node)
		{
			int num = this.StackDepth();
			BoundExpression boundExpression = this.VisitExpression(node.Condition, ExprContext.Value);
			object stackStateCookie = this.GetStackStateCookie();
			ExprContext exprContext = (node.IsRef ? ExprContext.Address : ExprContext.Value);
			this.SetStackDepth(num);
			BoundExpression boundExpression2 = this.VisitExpression(node.Consequence, exprContext);
			this.EnsureStackState(stackStateCookie);
			this.SetStackDepth(num);
			BoundExpression boundExpression3 = this.VisitExpression(node.Alternative, exprContext);
			this.EnsureStackState(stackStateCookie);
			return node.Update(node.IsRef, boundExpression, boundExpression2, boundExpression3, node.ConstantValueOpt, node.NaturalTypeOpt, node.WasCompilerGenerated, node.Type);
		}

		// Token: 0x06008129 RID: 33065 RVA: 0x001D627C File Offset: 0x001D447C
		public override BoundNode VisitBinaryOperator(BoundBinaryOperator node)
		{
			BoundExpression boundExpression = node.Left;
			if (boundExpression.Kind != BoundKind.BinaryOperator || boundExpression.ConstantValue != null)
			{
				return this.VisitBinaryOperatorSimple(node);
			}
			ArrayBuilder<BoundBinaryOperator> instance = ArrayBuilder<BoundBinaryOperator>.GetInstance();
			instance.Push(node);
			BoundBinaryOperator boundBinaryOperator = (BoundBinaryOperator)boundExpression;
			for (;;)
			{
				instance.Push(boundBinaryOperator);
				boundExpression = boundBinaryOperator.Left;
				if (boundExpression.Kind != BoundKind.BinaryOperator || boundExpression.ConstantValue != null)
				{
					break;
				}
				boundBinaryOperator = (BoundBinaryOperator)boundExpression;
			}
			ExprContext context = this._context;
			int num = this.StackDepth();
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(boundExpression);
			for (;;)
			{
				boundBinaryOperator = instance.Pop<BoundBinaryOperator>();
				bool flag = (boundBinaryOperator.OperatorKind & BinaryOperatorKind.Logical) > BinaryOperatorKind.Error;
				object obj = null;
				if (flag)
				{
					obj = this.GetStackStateCookie();
					this.SetStackDepth(num);
				}
				BoundExpression boundExpression3 = (BoundExpression)this.Visit(boundBinaryOperator.Right);
				if (flag)
				{
					this.EnsureStackState(obj);
				}
				TypeSymbol typeSymbol = this.VisitType(boundBinaryOperator.Type);
				boundExpression2 = boundBinaryOperator.Update(boundBinaryOperator.OperatorKind, boundBinaryOperator.ConstantValueOpt, boundBinaryOperator.MethodOpt, boundBinaryOperator.ResultKind, boundExpression2, boundExpression3, typeSymbol);
				if (instance.Count == 0)
				{
					break;
				}
				this._context = context;
				this._counter++;
				this.SetStackDepth(num);
				this.PushEvalStack(boundBinaryOperator, ExprContext.Value);
			}
			instance.Free();
			return boundExpression2;
		}

		// Token: 0x0600812A RID: 33066 RVA: 0x001D63C8 File Offset: 0x001D45C8
		private BoundNode VisitBinaryOperatorSimple(BoundBinaryOperator node)
		{
			if ((node.OperatorKind & BinaryOperatorKind.Logical) > BinaryOperatorKind.Error)
			{
				int num = this.StackDepth();
				BoundExpression boundExpression = (BoundExpression)this.Visit(node.Left);
				object stackStateCookie = this.GetStackStateCookie();
				this.SetStackDepth(num);
				BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Right);
				this.EnsureStackState(stackStateCookie);
				return node.Update(node.OperatorKind, node.ConstantValueOpt, node.MethodOpt, node.ResultKind, boundExpression, boundExpression2, node.Type);
			}
			return base.VisitBinaryOperator(node);
		}

		// Token: 0x0600812B RID: 33067 RVA: 0x001D6454 File Offset: 0x001D4654
		public override BoundNode VisitNullCoalescingOperator(BoundNullCoalescingOperator node)
		{
			int num = this.StackDepth();
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.LeftOperand);
			object stackStateCookie = this.GetStackStateCookie();
			this.SetStackDepth(num);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.RightOperand);
			this.EnsureStackState(stackStateCookie);
			return node.Update(boundExpression, boundExpression2, node.LeftConversion, node.OperatorResultKind, node.Type);
		}

		// Token: 0x0600812C RID: 33068 RVA: 0x001D64BC File Offset: 0x001D46BC
		public override BoundNode VisitLoweredConditionalAccess(BoundLoweredConditionalAccess node)
		{
			int num = this.StackDepth();
			BoundExpression boundExpression = this.VisitCallReceiver(node.Receiver);
			object stackStateCookie = this.GetStackStateCookie();
			this.SetStackDepth(num);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.WhenNotNull);
			this.EnsureStackState(stackStateCookie);
			BoundExpression boundExpression3 = node.WhenNullOpt;
			if (boundExpression3 != null)
			{
				this.SetStackDepth(num);
				boundExpression3 = (BoundExpression)this.Visit(boundExpression3);
				this.EnsureStackState(stackStateCookie);
			}
			else
			{
				this._counter++;
			}
			return node.Update(boundExpression, node.HasValueMethodOpt, boundExpression2, boundExpression3, node.Id, node.Type);
		}

		// Token: 0x0600812D RID: 33069 RVA: 0x001D655C File Offset: 0x001D475C
		public override BoundNode VisitComplexConditionalReceiver(BoundComplexConditionalReceiver node)
		{
			this.EnsureOnlyEvalStack();
			int num = this.StackDepth();
			this.PushEvalStack(null, ExprContext.None);
			object stackStateCookie = this.GetStackStateCookie();
			this.SetStackDepth(num);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ValueTypeReceiver);
			this.EnsureStackState(stackStateCookie);
			this.SetStackDepth(num);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.ReferenceTypeReceiver);
			this.EnsureStackState(stackStateCookie);
			return node.Update(boundExpression, boundExpression2, node.Type);
		}

		// Token: 0x0600812E RID: 33070 RVA: 0x001D65D4 File Offset: 0x001D47D4
		public override BoundNode VisitUnaryOperator(BoundUnaryOperator node)
		{
			if (node.OperatorKind.IsChecked() && node.OperatorKind.Operator() == UnaryOperatorKind.UnaryMinus)
			{
				this.StackDepth();
				this.PushEvalStack(new BoundDefaultExpression(node.Syntax, node.Operand.Type, false), ExprContext.Value);
				BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
				return node.Update(node.OperatorKind, boundExpression, node.ConstantValueOpt, node.MethodOpt, node.ResultKind, node.Type);
			}
			return base.VisitUnaryOperator(node);
		}

		// Token: 0x0600812F RID: 33071 RVA: 0x001D6664 File Offset: 0x001D4864
		public override BoundNode VisitTryStatement(BoundTryStatement node)
		{
			this.EnsureOnlyEvalStack();
			BoundBlock boundBlock = (BoundBlock)this.Visit(node.TryBlock);
			ImmutableArray<BoundCatchBlock> immutableArray = base.VisitList<BoundCatchBlock>(node.CatchBlocks);
			this.EnsureOnlyEvalStack();
			BoundBlock boundBlock2 = (BoundBlock)this.Visit(node.FinallyBlockOpt);
			this.EnsureOnlyEvalStack();
			return node.Update(boundBlock, immutableArray, boundBlock2, node.FinallyLabelOpt, node.PreferFaultHandler);
		}

		// Token: 0x06008130 RID: 33072 RVA: 0x001D66CC File Offset: 0x001D48CC
		public override BoundNode VisitCatchBlock(BoundCatchBlock node)
		{
			this.EnsureOnlyEvalStack();
			BoundExpression boundExpression = node.ExceptionSourceOpt;
			this.DeclareLocals(node.Locals, 0);
			if (boundExpression != null)
			{
				this.PushEvalStack(null, ExprContext.None);
				this._counter++;
				if (boundExpression.Kind == BoundKind.Local)
				{
					this.RecordVarWrite(((BoundLocal)boundExpression).LocalSymbol);
				}
				else
				{
					int num = this.StackDepth();
					boundExpression = this.VisitExpression(boundExpression, ExprContext.AssignmentTarget);
					this._assignmentLocal = null;
					this.SetStackDepth(num);
				}
				this.PopEvalStack();
				this._counter++;
			}
			BoundStatementList boundStatementList;
			if (node.ExceptionFilterPrologueOpt != null)
			{
				this.EnsureOnlyEvalStack();
				boundStatementList = (BoundStatementList)this.Visit(node.ExceptionFilterPrologueOpt);
			}
			else
			{
				boundStatementList = null;
			}
			BoundExpression boundExpression2;
			if (node.ExceptionFilterOpt != null)
			{
				boundExpression2 = (BoundExpression)this.Visit(node.ExceptionFilterOpt);
				this.PopEvalStack();
				this._counter++;
				this.EnsureOnlyEvalStack();
			}
			else
			{
				boundExpression2 = null;
			}
			BoundBlock boundBlock = (BoundBlock)this.Visit(node.Body);
			TypeSymbol typeSymbol = this.VisitType(node.ExceptionTypeOpt);
			return node.Update(node.Locals, boundExpression, typeSymbol, boundStatementList, boundExpression2, boundBlock, node.IsSynthesizedAsyncCatchAll);
		}

		// Token: 0x06008131 RID: 33073 RVA: 0x001D67F1 File Offset: 0x001D49F1
		public override BoundNode VisitConvertedStackAllocExpression(BoundConvertedStackAllocExpression node)
		{
			this.EnsureOnlyEvalStack();
			return base.VisitConvertedStackAllocExpression(node);
		}

		// Token: 0x06008132 RID: 33074 RVA: 0x001D6800 File Offset: 0x001D4A00
		public override BoundNode VisitArrayInitialization(BoundArrayInitialization node)
		{
			this.EnsureOnlyEvalStack();
			ImmutableArray<BoundExpression> initializers = node.Initializers;
			ArrayBuilder<BoundExpression> arrayBuilder = null;
			if (!initializers.IsDefault)
			{
				for (int i = 0; i < initializers.Length; i++)
				{
					this.EnsureOnlyEvalStack();
					BoundExpression boundExpression = initializers[i];
					BoundExpression boundExpression2 = this.VisitExpression(boundExpression, ExprContext.Value);
					if (arrayBuilder == null && boundExpression2 != boundExpression)
					{
						arrayBuilder = ArrayBuilder<BoundExpression>.GetInstance();
						arrayBuilder.AddRange(initializers, i);
					}
					if (arrayBuilder != null)
					{
						arrayBuilder.Add(boundExpression2);
					}
				}
			}
			return node.Update((arrayBuilder != null) ? arrayBuilder.ToImmutableAndFree() : initializers);
		}

		// Token: 0x06008133 RID: 33075 RVA: 0x001D6884 File Offset: 0x001D4A84
		public override BoundNode VisitAddressOfOperator(BoundAddressOfOperator node)
		{
			BoundExpression boundExpression = this.VisitExpression(node.Operand, ExprContext.Address);
			return node.Update(boundExpression, node.IsManaged, node.Type);
		}

		// Token: 0x06008134 RID: 33076 RVA: 0x001D68B4 File Offset: 0x001D4AB4
		public override BoundNode VisitReturnStatement(BoundReturnStatement node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ExpressionOpt);
			this.EnsureOnlyEvalStack();
			return node.Update(node.RefKind, boundExpression);
		}

		// Token: 0x06008135 RID: 33077 RVA: 0x001D68E6 File Offset: 0x001D4AE6
		private void EnsureOnlyEvalStack()
		{
			this.RecordVarRead(StackOptimizerPass1.empty);
		}

		// Token: 0x06008136 RID: 33078 RVA: 0x001D68F4 File Offset: 0x001D4AF4
		private object GetStackStateCookie()
		{
			DummyLocal dummyLocal = new DummyLocal();
			this._dummyVariables.Add(dummyLocal, dummyLocal);
			this._locals.Add(dummyLocal, LocalDefUseInfo.GetInstance(this.StackDepth()));
			this.RecordDummyWrite(dummyLocal);
			return dummyLocal;
		}

		// Token: 0x06008137 RID: 33079 RVA: 0x001D6933 File Offset: 0x001D4B33
		private void EnsureStackState(object cookie)
		{
			this.RecordVarRead(this._dummyVariables[cookie]);
		}

		// Token: 0x06008138 RID: 33080 RVA: 0x001D6948 File Offset: 0x001D4B48
		private void RecordBranch(LabelSymbol label)
		{
			DummyLocal dummyLocal;
			if (this._dummyVariables.TryGetValue(label, out dummyLocal))
			{
				this.RecordVarRead(dummyLocal);
				return;
			}
			dummyLocal = new DummyLocal();
			this._dummyVariables.Add(label, dummyLocal);
			this._locals.Add(dummyLocal, LocalDefUseInfo.GetInstance(this.StackDepth()));
			this.RecordDummyWrite(dummyLocal);
		}

		// Token: 0x06008139 RID: 33081 RVA: 0x001D69A0 File Offset: 0x001D4BA0
		private void RecordLabel(LabelSymbol label)
		{
			DummyLocal dummyLocal;
			if (this._dummyVariables.TryGetValue(label, out dummyLocal))
			{
				this.RecordVarRead(dummyLocal);
				return;
			}
			dummyLocal = StackOptimizerPass1.empty;
			this._dummyVariables.Add(label, dummyLocal);
			this.RecordVarRead(dummyLocal);
		}

		// Token: 0x0600813A RID: 33082 RVA: 0x001D69E0 File Offset: 0x001D4BE0
		private void ShouldNotSchedule(LocalSymbol localSymbol)
		{
			LocalDefUseInfo localDefUseInfo;
			if (this._locals.TryGetValue(localSymbol, ref localDefUseInfo))
			{
				localDefUseInfo.ShouldNotSchedule();
			}
		}

		// Token: 0x0600813B RID: 33083 RVA: 0x001D6A03 File Offset: 0x001D4C03
		private void RecordVarRef(LocalSymbol local)
		{
			if (!this.CanScheduleToStack(local))
			{
				return;
			}
			this.ShouldNotSchedule(local);
		}

		// Token: 0x0600813C RID: 33084 RVA: 0x001D6A18 File Offset: 0x001D4C18
		private void RecordVarRead(LocalSymbol local)
		{
			if (!this.CanScheduleToStack(local))
			{
				return;
			}
			LocalDefUseInfo localDefUseInfo = this._locals[local];
			if (localDefUseInfo.CannotSchedule)
			{
				return;
			}
			ArrayBuilder<LocalDefUseSpan> localDefs = localDefUseInfo.LocalDefs;
			if (localDefs.Count == 0)
			{
				localDefUseInfo.ShouldNotSchedule();
				return;
			}
			if (local.SynthesizedKind != SynthesizedLocalKind.OptimizerTemp && localDefUseInfo.StackAtDeclaration != this.StackDepth() && !this.EvalStackHasLocal(local))
			{
				localDefUseInfo.ShouldNotSchedule();
				return;
			}
			int num = localDefs.Count - 1;
			localDefs[num] = localDefs[num].WithEnd(this._counter);
			LocalDefUseSpan localDefUseSpan = new LocalDefUseSpan(this._counter);
			localDefs.Add(localDefUseSpan);
		}

		// Token: 0x0600813D RID: 33085 RVA: 0x001D6AC0 File Offset: 0x001D4CC0
		private bool EvalStackHasLocal(LocalSymbol local)
		{
			ValueTuple<BoundExpression, ExprContext> valueTuple = this._evalStack.Last();
			return valueTuple.Item2 == ((local.RefKind == RefKind.None) ? ExprContext.Value : ExprContext.Address) && valueTuple.Item1.Kind == BoundKind.Local && ((BoundLocal)valueTuple.Item1).LocalSymbol == local;
		}

		// Token: 0x0600813E RID: 33086 RVA: 0x001D6B14 File Offset: 0x001D4D14
		private void RecordDummyWrite(LocalSymbol local)
		{
			LocalDefUseInfo localDefUseInfo = this._locals[local];
			LocalDefUseSpan localDefUseSpan = new LocalDefUseSpan(this._counter);
			localDefUseInfo.LocalDefs.Add(localDefUseSpan);
		}

		// Token: 0x0600813F RID: 33087 RVA: 0x001D6B48 File Offset: 0x001D4D48
		private void RecordVarWrite(LocalSymbol local)
		{
			if (!this.CanScheduleToStack(local))
			{
				return;
			}
			LocalDefUseInfo localDefUseInfo = this._locals[local];
			if (localDefUseInfo.CannotSchedule)
			{
				return;
			}
			int num = this.StackDepth() - 1;
			if (localDefUseInfo.StackAtDeclaration != num)
			{
				localDefUseInfo.ShouldNotSchedule();
				return;
			}
			LocalDefUseSpan localDefUseSpan = new LocalDefUseSpan(this._counter);
			localDefUseInfo.LocalDefs.Add(localDefUseSpan);
		}

		// Token: 0x06008140 RID: 33088 RVA: 0x001D6BA7 File Offset: 0x001D4DA7
		private bool CanScheduleToStack(LocalSymbol local)
		{
			return local.CanScheduleToStack && (!this._debugFriendly || !local.SynthesizedKind.IsLongLived());
		}

		// Token: 0x06008141 RID: 33089 RVA: 0x001D6BCC File Offset: 0x001D4DCC
		private void DeclareLocals(ImmutableArray<LocalSymbol> locals, int stack)
		{
			foreach (LocalSymbol localSymbol in locals)
			{
				this.DeclareLocal(localSymbol, stack);
			}
		}

		// Token: 0x06008142 RID: 33090 RVA: 0x001D6BFC File Offset: 0x001D4DFC
		private void DeclareLocal(LocalSymbol local, int stack)
		{
			if (local != null && this.CanScheduleToStack(local))
			{
				LocalDefUseInfo localDefUseInfo;
				if (!this._locals.TryGetValue(local, ref localDefUseInfo))
				{
					this._locals.Add(local, LocalDefUseInfo.GetInstance(stack));
					return;
				}
				if (localDefUseInfo.StackAtDeclaration != stack)
				{
					localDefUseInfo.ShouldNotSchedule();
				}
			}
		}

		// Token: 0x04001E54 RID: 7764
		private readonly bool _debugFriendly;

		// Token: 0x04001E55 RID: 7765
		private readonly ArrayBuilder<ValueTuple<BoundExpression, ExprContext>> _evalStack;

		// Token: 0x04001E56 RID: 7766
		private int _counter;

		// Token: 0x04001E57 RID: 7767
		private ExprContext _context;

		// Token: 0x04001E58 RID: 7768
		private BoundLocal _assignmentLocal;

		// Token: 0x04001E59 RID: 7769
		private readonly Dictionary<LocalSymbol, LocalDefUseInfo> _locals = new Dictionary<LocalSymbol, LocalDefUseInfo>();

		// Token: 0x04001E5A RID: 7770
		private readonly SmallDictionary<object, DummyLocal> _dummyVariables = new SmallDictionary<object, DummyLocal>(ReferenceEqualityComparer.Instance);

		// Token: 0x04001E5B RID: 7771
		public static readonly DummyLocal empty = new DummyLocal();

		// Token: 0x04001E5C RID: 7772
		private int _recursionDepth;

		// Token: 0x020009A1 RID: 2465
		private sealed class LocalUsedWalker : BoundTreeWalkerWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator
		{
			// Token: 0x060092B8 RID: 37560 RVA: 0x00201083 File Offset: 0x001FF283
			internal LocalUsedWalker(LocalSymbol local, int recursionDepth)
				: base(recursionDepth)
			{
				this._local = local;
			}

			// Token: 0x060092B9 RID: 37561 RVA: 0x00201093 File Offset: 0x001FF293
			public bool IsLocalUsedIn(BoundNode node)
			{
				this._found = false;
				this.Visit(node);
				return this._found;
			}

			// Token: 0x060092BA RID: 37562 RVA: 0x002010AA File Offset: 0x001FF2AA
			public override BoundNode Visit(BoundNode node)
			{
				if (!this._found)
				{
					return base.Visit(node);
				}
				return null;
			}

			// Token: 0x060092BB RID: 37563 RVA: 0x002010BD File Offset: 0x001FF2BD
			public override BoundNode VisitLocal(BoundLocal node)
			{
				if (node.LocalSymbol == this._local)
				{
					this._found = true;
				}
				return null;
			}

			// Token: 0x04002991 RID: 10641
			private readonly LocalSymbol _local;

			// Token: 0x04002992 RID: 10642
			private bool _found;
		}
	}
}
