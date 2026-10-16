using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.CodeGen
{
	// Token: 0x020005F4 RID: 1524
	internal sealed class CodeGenerator
	{
		// Token: 0x06007FF6 RID: 32758 RVA: 0x001CCEB4 File Offset: 0x001CB0B4
		public CodeGenerator(MethodSymbol method, BoundStatement boundBody, ILBuilder builder, PEModuleBuilder moduleBuilder, DiagnosticBag diagnostics, OptimizationLevel optimizations, bool emittingPdb)
		{
			this._method = method;
			this._boundBody = boundBody;
			this._builder = builder;
			this._module = moduleBuilder;
			this._diagnostics = diagnostics;
			if (!method.GenerateDebugInfo)
			{
				this._ilEmitStyle = ILEmitStyle.Release;
			}
			else if (optimizations == OptimizationLevel.Debug)
			{
				this._ilEmitStyle = ILEmitStyle.Debug;
			}
			else
			{
				this._ilEmitStyle = (this.IsDebugPlus() ? ILEmitStyle.DebugFriendlyRelease : ILEmitStyle.Release);
			}
			this._emitPdbSequencePoints = emittingPdb && method.GenerateDebugInfo;
			try
			{
				this._boundBody = Optimizer.Optimize(boundBody, this._ilEmitStyle != ILEmitStyle.Release, out this._stackLocals);
			}
			catch (BoundTreeVisitor.CancelledByStackGuardException ex)
			{
				ex.AddAnError(diagnostics);
				this._boundBody = boundBody;
			}
			SourceMemberMethodSymbol sourceMemberMethodSymbol = method as SourceMemberMethodSymbol;
			ValueTuple<BlockSyntax, ArrowExpressionClauseSyntax> valueTuple = ((sourceMemberMethodSymbol != null) ? sourceMemberMethodSymbol.Bodies : default(ValueTuple<BlockSyntax, ArrowExpressionClauseSyntax>));
			BlockSyntax item = valueTuple.Item1;
			ArrowExpressionClauseSyntax item2 = valueTuple.Item2;
			BlockSyntax blockSyntax;
			if ((blockSyntax = item) == null)
			{
				blockSyntax = item2 ?? ((sourceMemberMethodSymbol != null) ? sourceMemberMethodSymbol.SyntaxNode : null);
			}
			this._methodBodySyntaxOpt = blockSyntax;
		}

		// Token: 0x06007FF7 RID: 32759 RVA: 0x001CCFC8 File Offset: 0x001CB1C8
		private bool IsDebugPlus()
		{
			return this._module.Compilation.Options.DebugPlusMode;
		}

		// Token: 0x06007FF8 RID: 32760 RVA: 0x001CCFDF File Offset: 0x001CB1DF
		private bool IsPeVerifyCompatEnabled()
		{
			return this._module.Compilation.IsPeVerifyCompatEnabled;
		}

		// Token: 0x17002647 RID: 9799
		// (get) Token: 0x06007FF9 RID: 32761 RVA: 0x001CCFF4 File Offset: 0x001CB1F4
		private LocalDefinition LazyReturnTemp
		{
			get
			{
				LocalDefinition localDefinition = this._returnTemp;
				if (localDefinition == null)
				{
					LocalSlotConstraints localSlotConstraints = ((this._method.RefKind == RefKind.None) ? LocalSlotConstraints.None : LocalSlotConstraints.ByRef);
					SyntaxNode methodBodySyntaxOpt = this._methodBodySyntaxOpt;
					if (this._ilEmitStyle == ILEmitStyle.Debug && methodBodySyntaxOpt != null)
					{
						int num = this._method.CalculateLocalSyntaxOffset(LambdaUtilities.GetDeclaratorPosition(methodBodySyntaxOpt), methodBodySyntaxOpt.SyntaxTree);
						SynthesizedLocal synthesizedLocal = new SynthesizedLocal(this._method, this._method.ReturnTypeWithAnnotations, SynthesizedLocalKind.FunctionReturnValue, methodBodySyntaxOpt, false, RefKind.None);
						localDefinition = this._builder.LocalSlotManager.DeclareLocal(this._module.Translate(synthesizedLocal.Type, methodBodySyntaxOpt, this._diagnostics), synthesizedLocal, null, synthesizedLocal.SynthesizedKind, new LocalDebugId(num, 0), synthesizedLocal.SynthesizedKind.PdbAttributes(), localSlotConstraints, ImmutableArray<bool>.Empty, ImmutableArray<string>.Empty, false);
					}
					else
					{
						localDefinition = this.AllocateTemp(this._method.ReturnType, this._boundBody.Syntax, localSlotConstraints);
					}
					this._returnTemp = localDefinition;
				}
				return localDefinition;
			}
		}

		// Token: 0x06007FFA RID: 32762 RVA: 0x001CD0E8 File Offset: 0x001CB2E8
		internal static bool IsStackLocal(LocalSymbol local, HashSet<LocalSymbol> stackLocalsOpt)
		{
			return stackLocalsOpt != null && stackLocalsOpt.Contains(local);
		}

		// Token: 0x06007FFB RID: 32763 RVA: 0x001CD0F6 File Offset: 0x001CB2F6
		private bool IsStackLocal(LocalSymbol local)
		{
			return CodeGenerator.IsStackLocal(local, this._stackLocals);
		}

		// Token: 0x06007FFC RID: 32764 RVA: 0x001CD104 File Offset: 0x001CB304
		public void Generate(out bool hasStackalloc)
		{
			this.GenerateImpl();
			hasStackalloc = this._sawStackalloc;
		}

		// Token: 0x06007FFD RID: 32765 RVA: 0x001CD114 File Offset: 0x001CB314
		public void Generate(out int asyncCatchHandlerOffset, out ImmutableArray<int> asyncYieldPoints, out ImmutableArray<int> asyncResumePoints, out bool hasStackAlloc)
		{
			this.GenerateImpl();
			hasStackAlloc = this._sawStackalloc;
			asyncCatchHandlerOffset = this._builder.GetILOffsetFromMarker(this._asyncCatchHandlerOffset);
			ArrayBuilder<int> asyncYieldPoints2 = this._asyncYieldPoints;
			ArrayBuilder<int> asyncResumePoints2 = this._asyncResumePoints;
			if (asyncYieldPoints2 == null)
			{
				asyncYieldPoints = ImmutableArray<int>.Empty;
				asyncResumePoints = ImmutableArray<int>.Empty;
				return;
			}
			ArrayBuilder<int> instance = ArrayBuilder<int>.GetInstance();
			ArrayBuilder<int> instance2 = ArrayBuilder<int>.GetInstance();
			int count = asyncYieldPoints2.Count;
			for (int i = 0; i < count; i++)
			{
				int iloffsetFromMarker = this._builder.GetILOffsetFromMarker(asyncYieldPoints2[i]);
				int iloffsetFromMarker2 = this._builder.GetILOffsetFromMarker(asyncResumePoints2[i]);
				if (iloffsetFromMarker > 0)
				{
					instance.Add(iloffsetFromMarker);
					instance2.Add(iloffsetFromMarker2);
				}
			}
			asyncYieldPoints = instance.ToImmutableAndFree();
			asyncResumePoints = instance2.ToImmutableAndFree();
			asyncYieldPoints2.Free();
			asyncResumePoints2.Free();
		}

		// Token: 0x06007FFE RID: 32766 RVA: 0x001CD1F4 File Offset: 0x001CB3F4
		private void GenerateImpl()
		{
			this.SetInitialDebugDocument();
			if (this._emitPdbSequencePoints && this._method.IsImplicitlyDeclared)
			{
				this._builder.DefineInitialHiddenSequencePoint();
			}
			try
			{
				this.EmitStatement(this._boundBody);
				if (this._indirectReturnState == CodeGenerator.IndirectReturnState.Needed)
				{
					this.HandleReturn();
				}
				if (!this._diagnostics.HasAnyErrors())
				{
					this._builder.Realize();
				}
			}
			catch (CodeGenerator.EmitCancelledException)
			{
			}
			this._synthesizedLocalOrdinals.Free();
			ArrayBuilder<LocalDefinition> expressionTemps = this._expressionTemps;
			if (expressionTemps != null)
			{
				expressionTemps.Free();
			}
			PooledDictionary<object, TextSpan> savedSequencePoints = this._savedSequencePoints;
			if (savedSequencePoints == null)
			{
				return;
			}
			savedSequencePoints.Free();
		}

		// Token: 0x06007FFF RID: 32767 RVA: 0x001CD29C File Offset: 0x001CB49C
		private void HandleReturn()
		{
			this._builder.MarkLabel(CodeGenerator.s_returnLabel);
			if (this._emitPdbSequencePoints && !this._method.IsIterator && !this._method.IsAsync)
			{
				BlockSyntax blockSyntax = this._methodBodySyntaxOpt as BlockSyntax;
				if (blockSyntax != null)
				{
					this.EmitSequencePoint(blockSyntax.SyntaxTree, blockSyntax.CloseBraceToken.Span);
				}
			}
			if (this._returnTemp != null)
			{
				this._builder.EmitLocalLoad(this.LazyReturnTemp);
				this._builder.EmitRet(false);
			}
			else
			{
				this._builder.EmitRet(true);
			}
			this._indirectReturnState = CodeGenerator.IndirectReturnState.Emitted;
		}

		// Token: 0x06008000 RID: 32768 RVA: 0x001CD33F File Offset: 0x001CB53F
		private void EmitTypeReferenceToken(ITypeReference symbol, SyntaxNode syntaxNode)
		{
			this._builder.EmitToken(symbol, syntaxNode, this._diagnostics, false);
		}

		// Token: 0x06008001 RID: 32769 RVA: 0x001CD355 File Offset: 0x001CB555
		private void EmitSymbolToken(TypeSymbol symbol, SyntaxNode syntaxNode)
		{
			this.EmitTypeReferenceToken(this._module.Translate(symbol, syntaxNode, this._diagnostics), syntaxNode);
		}

		// Token: 0x06008002 RID: 32770 RVA: 0x001CD371 File Offset: 0x001CB571
		private void EmitSymbolToken(MethodSymbol method, SyntaxNode syntaxNode, BoundArgListOperator optArgList, bool encodeAsRawDefinitionToken = false)
		{
			this._builder.EmitToken(this._module.Translate(method, syntaxNode, this._diagnostics, optArgList, encodeAsRawDefinitionToken), syntaxNode, this._diagnostics, encodeAsRawDefinitionToken);
		}

		// Token: 0x06008003 RID: 32771 RVA: 0x001CD39D File Offset: 0x001CB59D
		private void EmitSymbolToken(FieldSymbol symbol, SyntaxNode syntaxNode)
		{
			this._builder.EmitToken(this._module.Translate(symbol, syntaxNode, this._diagnostics, false), syntaxNode, this._diagnostics, false);
		}

		// Token: 0x06008004 RID: 32772 RVA: 0x001CD3C6 File Offset: 0x001CB5C6
		private void EmitSignatureToken(FunctionPointerTypeSymbol symbol, SyntaxNode syntaxNode)
		{
			this._builder.EmitToken(this._module.Translate(symbol).Signature, syntaxNode, this._diagnostics);
		}

		// Token: 0x06008005 RID: 32773 RVA: 0x001CD3EC File Offset: 0x001CB5EC
		private void EmitSequencePointStatement(BoundSequencePoint node)
		{
			SyntaxNode syntax = node.Syntax;
			if (this._emitPdbSequencePoints)
			{
				if (syntax == null)
				{
					this.EmitHiddenSequencePoint();
				}
				else
				{
					this.EmitSequencePoint(syntax);
				}
			}
			BoundStatement statementOpt = node.StatementOpt;
			int num = 0;
			if (statementOpt != null)
			{
				num = this.EmitStatementAndCountInstructions(statementOpt);
			}
			if (num == 0 && syntax != null && this._ilEmitStyle == ILEmitStyle.Debug)
			{
				this._builder.EmitOpCode(ILOpCode.Nop);
			}
		}

		// Token: 0x06008006 RID: 32774 RVA: 0x001CD448 File Offset: 0x001CB648
		private void EmitSequencePointStatement(BoundSequencePointWithSpan node)
		{
			TextSpan span = node.Span;
			if (span != default(TextSpan) && this._emitPdbSequencePoints)
			{
				this.EmitSequencePoint(node.SyntaxTree, span);
			}
			BoundStatement statementOpt = node.StatementOpt;
			int num = 0;
			if (statementOpt != null)
			{
				num = this.EmitStatementAndCountInstructions(statementOpt);
			}
			if (num == 0 && span != default(TextSpan) && this._ilEmitStyle == ILEmitStyle.Debug)
			{
				this._builder.EmitOpCode(ILOpCode.Nop);
			}
		}

		// Token: 0x06008007 RID: 32775 RVA: 0x001CD4C0 File Offset: 0x001CB6C0
		private void EmitSavePreviousSequencePoint(BoundSavePreviousSequencePoint statement)
		{
			if (!this._emitPdbSequencePoints)
			{
				return;
			}
			ArrayBuilder<RawSequencePoint> seqPointsOpt = this._builder.SeqPointsOpt;
			if (seqPointsOpt == null)
			{
				return;
			}
			for (int i = seqPointsOpt.Count - 1; i >= 0; i--)
			{
				TextSpan span = seqPointsOpt[i].Span;
				if (!(span == RawSequencePoint.HiddenSequencePointSpan))
				{
					if (this._savedSequencePoints == null)
					{
						this._savedSequencePoints = PooledDictionary<object, TextSpan>.GetInstance();
					}
					this._savedSequencePoints.Add(statement.Identifier, span);
					return;
				}
			}
		}

		// Token: 0x06008008 RID: 32776 RVA: 0x001CD53C File Offset: 0x001CB73C
		private void EmitRestorePreviousSequencePoint(BoundRestorePreviousSequencePoint node)
		{
			TextSpan textSpan;
			if (this._savedSequencePoints == null || !this._savedSequencePoints.TryGetValue(node.Identifier, ref textSpan))
			{
				return;
			}
			this.EmitStepThroughSequencePoint(node.Syntax.SyntaxTree, textSpan);
		}

		// Token: 0x06008009 RID: 32777 RVA: 0x001CD579 File Offset: 0x001CB779
		private void EmitStepThroughSequencePoint(BoundStepThroughSequencePoint node)
		{
			this.EmitStepThroughSequencePoint(node.Syntax.SyntaxTree, node.Span);
		}

		// Token: 0x0600800A RID: 32778 RVA: 0x001CD594 File Offset: 0x001CB794
		private void EmitStepThroughSequencePoint(SyntaxTree syntaxTree, TextSpan span)
		{
			if (!this._emitPdbSequencePoints)
			{
				return;
			}
			object obj = new object();
			this._builder.EmitConstantValue(ConstantValue.Create(true));
			this._builder.EmitBranch(ILOpCode.Brtrue, obj, ILOpCode.Nop);
			this.EmitSequencePoint(syntaxTree, span);
			this._builder.EmitOpCode(ILOpCode.Nop);
			this._builder.MarkLabel(obj);
			this.EmitHiddenSequencePoint();
		}

		// Token: 0x0600800B RID: 32779 RVA: 0x001CD5F7 File Offset: 0x001CB7F7
		private void SetInitialDebugDocument()
		{
			if (this._emitPdbSequencePoints && this._methodBodySyntaxOpt != null)
			{
				this._builder.SetInitialDebugDocument(this._methodBodySyntaxOpt.SyntaxTree);
			}
		}

		// Token: 0x0600800C RID: 32780 RVA: 0x001CD61F File Offset: 0x001CB81F
		private void EmitHiddenSequencePoint()
		{
			this._builder.DefineHiddenSequencePoint();
		}

		// Token: 0x0600800D RID: 32781 RVA: 0x001CD62C File Offset: 0x001CB82C
		private void EmitSequencePoint(SyntaxNode syntax)
		{
			this.EmitSequencePoint(syntax.SyntaxTree, syntax.Span);
		}

		// Token: 0x0600800E RID: 32782 RVA: 0x001CD641 File Offset: 0x001CB841
		private TextSpan EmitSequencePoint(SyntaxTree syntaxTree, TextSpan span)
		{
			this._builder.DefineSequencePoint(syntaxTree, span);
			return span;
		}

		// Token: 0x0600800F RID: 32783 RVA: 0x001CD654 File Offset: 0x001CB854
		private void AddExpressionTemp(LocalDefinition temp)
		{
			if (temp == null)
			{
				return;
			}
			ArrayBuilder<LocalDefinition> arrayBuilder = this._expressionTemps;
			if (arrayBuilder == null)
			{
				arrayBuilder = ArrayBuilder<LocalDefinition>.GetInstance();
				this._expressionTemps = arrayBuilder;
			}
			arrayBuilder.Add(temp);
		}

		// Token: 0x06008010 RID: 32784 RVA: 0x001CD684 File Offset: 0x001CB884
		private void ReleaseExpressionTemps()
		{
			ArrayBuilder<LocalDefinition> expressionTemps = this._expressionTemps;
			if (expressionTemps != null && expressionTemps.Count > 0)
			{
				for (int i = this._expressionTemps.Count - 1; i >= 0; i--)
				{
					LocalDefinition localDefinition = this._expressionTemps[i];
					this.FreeTemp(localDefinition);
				}
				this._expressionTemps.Clear();
			}
		}

		// Token: 0x06008011 RID: 32785 RVA: 0x001CD6E0 File Offset: 0x001CB8E0
		private LocalDefinition EmitAddress(BoundExpression expression, Binder.AddressKind addressKind)
		{
			BoundKind kind = expression.Kind;
			if (kind <= BoundKind.ConditionalOperator)
			{
				if (kind <= BoundKind.PointerIndirectionOperator)
				{
					if (kind == BoundKind.Dup)
					{
						return this.EmitDupAddress((BoundDup)expression, addressKind);
					}
					if (kind == BoundKind.PassByCopy)
					{
						return this.EmitPassByCopyAddress((BoundPassByCopy)expression, addressKind);
					}
					if (kind == BoundKind.PointerIndirectionOperator)
					{
						BoundExpression operand = ((BoundPointerIndirectionOperator)expression).Operand;
						this.EmitExpression(operand, true);
						goto IL_032A;
					}
				}
				else if (kind <= BoundKind.RefValueOperator)
				{
					if (kind != BoundKind.FunctionPointerInvocation)
					{
						if (kind == BoundKind.RefValueOperator)
						{
							this.EmitRefValueAddress((BoundRefValueOperator)expression);
							goto IL_032A;
						}
					}
					else
					{
						BoundFunctionPointerInvocation boundFunctionPointerInvocation = (BoundFunctionPointerInvocation)expression;
						RefKind refKind = boundFunctionPointerInvocation.FunctionPointer.Signature.RefKind;
						if (refKind == RefKind.Ref || (Binder.IsAnyReadOnly(addressKind) && refKind == RefKind.In))
						{
							this.EmitCalli(boundFunctionPointerInvocation, CodeGenerator.UseKind.UsedAsAddress);
							goto IL_032A;
						}
					}
				}
				else if (kind != BoundKind.AssignmentOperator)
				{
					if (kind == BoundKind.ConditionalOperator)
					{
						if (this.HasHome(expression, addressKind))
						{
							this.EmitConditionalOperatorAddress((BoundConditionalOperator)expression, addressKind);
							goto IL_032A;
						}
					}
				}
				else
				{
					BoundAssignmentOperator boundAssignmentOperator = (BoundAssignmentOperator)expression;
					if (boundAssignmentOperator.IsRef && this.HasHome(boundAssignmentOperator, addressKind))
					{
						this.EmitAssignmentExpression(boundAssignmentOperator, CodeGenerator.UseKind.UsedAsAddress);
						goto IL_032A;
					}
				}
			}
			else if (kind <= BoundKind.Parameter)
			{
				if (kind != BoundKind.ArrayAccess)
				{
					if (kind == BoundKind.DefaultExpression)
					{
						TypeSymbol type = expression.Type;
						LocalDefinition localDefinition = this.AllocateTemp(type, expression.Syntax, LocalSlotConstraints.None);
						this._builder.EmitLocalAddress(localDefinition);
						this._builder.EmitOpCode(ILOpCode.Dup);
						this._builder.EmitOpCode(ILOpCode.Initobj);
						this.EmitSymbolToken(type, expression.Syntax);
						return localDefinition;
					}
					switch (kind)
					{
					case BoundKind.ThisReference:
						if (!expression.Type.IsValueType)
						{
							this._builder.EmitLoadArgumentAddrOpcode(0);
							goto IL_032A;
						}
						if (this.HasHome(expression, addressKind))
						{
							this._builder.EmitLoadArgumentOpcode(0);
							goto IL_032A;
						}
						break;
					case BoundKind.PreviousSubmissionReference:
						throw ExceptionUtilities.UnexpectedValue(expression.Kind);
					case BoundKind.BaseReference:
						goto IL_032A;
					case BoundKind.Local:
						return this.EmitLocalAddress((BoundLocal)expression, addressKind);
					case BoundKind.PseudoVariable:
						this.EmitPseudoVariableAddress((BoundPseudoVariable)expression);
						goto IL_032A;
					case BoundKind.Parameter:
						return this.EmitParameterAddress((BoundParameter)expression, addressKind);
					}
				}
				else if (this.HasHome(expression, addressKind))
				{
					this.EmitArrayElementAddress((BoundArrayAccess)expression, addressKind);
					goto IL_032A;
				}
			}
			else if (kind <= BoundKind.Call)
			{
				if (kind == BoundKind.Sequence)
				{
					return this.EmitSequenceAddress((BoundSequence)expression, addressKind);
				}
				switch (kind)
				{
				case BoundKind.ConditionalReceiver:
					goto IL_032A;
				case BoundKind.ComplexConditionalReceiver:
					this.EmitComplexConditionalReceiverAddress((BoundComplexConditionalReceiver)expression);
					goto IL_032A;
				case BoundKind.Call:
				{
					BoundCall boundCall = (BoundCall)expression;
					RefKind refKind2 = boundCall.Method.RefKind;
					if (refKind2 == RefKind.Ref || (Binder.IsAnyReadOnly(addressKind) && refKind2 == RefKind.In))
					{
						this.EmitCallExpression(boundCall, CodeGenerator.UseKind.UsedAsAddress);
						goto IL_032A;
					}
					break;
				}
				}
			}
			else
			{
				if (kind == BoundKind.FieldAccess)
				{
					return this.EmitFieldAddress((BoundFieldAccess)expression, addressKind);
				}
				if (kind == BoundKind.ThrowExpression)
				{
					this.EmitExpression(expression, true);
					return null;
				}
			}
			return this.EmitAddressOfTempClone(expression);
			IL_032A:
			return null;
		}

		// Token: 0x06008012 RID: 32786 RVA: 0x001CDA18 File Offset: 0x001CBC18
		private LocalDefinition EmitPassByCopyAddress(BoundPassByCopy passByCopyExpr, Binder.AddressKind addressKind)
		{
			BoundSequence boundSequence = passByCopyExpr.Expression as BoundSequence;
			if (boundSequence != null && this.DigForValueLocal(boundSequence, boundSequence.Value) != null)
			{
				return this.EmitSequenceAddress(boundSequence, addressKind);
			}
			return this.EmitAddressOfTempClone(passByCopyExpr);
		}

		// Token: 0x06008013 RID: 32787 RVA: 0x001CDA5C File Offset: 0x001CBC5C
		private void EmitConditionalOperatorAddress(BoundConditionalOperator expr, Binder.AddressKind addressKind)
		{
			object obj = new object();
			object obj2 = new object();
			this.EmitCondBranch(expr.Condition, ref obj, true);
			this.AddExpressionTemp(this.EmitAddress(expr.Alternative, addressKind));
			this._builder.EmitBranch(ILOpCode.Br, obj2, ILOpCode.Nop);
			this._builder.AdjustStack(-1);
			this._builder.MarkLabel(obj);
			this.AddExpressionTemp(this.EmitAddress(expr.Consequence, addressKind));
			this._builder.MarkLabel(obj2);
		}

		// Token: 0x06008014 RID: 32788 RVA: 0x001CDAE0 File Offset: 0x001CBCE0
		private void EmitComplexConditionalReceiverAddress(BoundComplexConditionalReceiver expression)
		{
			TypeSymbol type = expression.Type;
			object obj = new object();
			object obj2 = new object();
			this.EmitInitObj(type, true, expression.Syntax);
			this.EmitBox(type, expression.Syntax);
			this._builder.EmitBranch(ILOpCode.Brtrue, obj, ILOpCode.Nop);
			this.EmitAddress(expression.ReferenceTypeReceiver, Binder.AddressKind.ReadOnly);
			this._builder.EmitBranch(ILOpCode.Br, obj2, ILOpCode.Nop);
			this._builder.AdjustStack(-1);
			this._builder.MarkLabel(obj);
			this.EmitReceiverRef(expression.ValueTypeReceiver, Binder.AddressKind.Constrained);
			this._builder.MarkLabel(obj2);
		}

		// Token: 0x06008015 RID: 32789 RVA: 0x001CDB7C File Offset: 0x001CBD7C
		private LocalDefinition EmitLocalAddress(BoundLocal localAccess, Binder.AddressKind addressKind)
		{
			LocalSymbol localSymbol = localAccess.LocalSymbol;
			if (!this.HasHome(localAccess, addressKind))
			{
				return this.EmitAddressOfTempClone(localAccess);
			}
			if (this.IsStackLocal(localSymbol))
			{
				if (localSymbol.RefKind == RefKind.None)
				{
					throw ExceptionUtilities.UnexpectedValue(localSymbol.RefKind);
				}
			}
			else
			{
				this._builder.EmitLocalAddress(this.GetLocal(localAccess));
			}
			return null;
		}

		// Token: 0x06008016 RID: 32790 RVA: 0x001CDBD7 File Offset: 0x001CBDD7
		private LocalDefinition EmitDupAddress(BoundDup dup, Binder.AddressKind addressKind)
		{
			if (!this.HasHome(dup, addressKind))
			{
				return this.EmitAddressOfTempClone(dup);
			}
			this._builder.EmitOpCode(ILOpCode.Dup);
			return null;
		}

		// Token: 0x06008017 RID: 32791 RVA: 0x001CDBF9 File Offset: 0x001CBDF9
		private void EmitPseudoVariableAddress(BoundPseudoVariable expression)
		{
			this.EmitExpression(expression.EmitExpressions.GetAddress(expression), true);
		}

		// Token: 0x06008018 RID: 32792 RVA: 0x001CDC0E File Offset: 0x001CBE0E
		private void EmitRefValueAddress(BoundRefValueOperator refValue)
		{
			this.EmitExpression(refValue.Operand, true);
			this._builder.EmitOpCode(ILOpCode.Refanyval);
			this.EmitSymbolToken(refValue.Type, refValue.Syntax);
		}

		// Token: 0x06008019 RID: 32793 RVA: 0x001CDC40 File Offset: 0x001CBE40
		private LocalDefinition EmitAddressOfTempClone(BoundExpression expression)
		{
			this.EmitExpression(expression, true);
			LocalDefinition localDefinition = this.AllocateTemp(expression.Type, expression.Syntax, LocalSlotConstraints.None);
			this._builder.EmitLocalStore(localDefinition);
			this._builder.EmitLocalAddress(localDefinition);
			return localDefinition;
		}

		// Token: 0x0600801A RID: 32794 RVA: 0x001CDC82 File Offset: 0x001CBE82
		private LocalDefinition EmitSequenceAddress(BoundSequence sequence, Binder.AddressKind addressKind)
		{
			this.DefineAndRecordLocals(sequence);
			this.EmitSideEffects(sequence);
			LocalDefinition localDefinition = this.EmitAddress(sequence.Value, addressKind);
			this.CloseScopeAndKeepLocals(sequence);
			return localDefinition;
		}

		// Token: 0x0600801B RID: 32795 RVA: 0x001CDCA8 File Offset: 0x001CBEA8
		private LocalSymbol DigForValueLocal(BoundSequence topSequence, BoundExpression value)
		{
			BoundKind kind = value.Kind;
			if (kind != BoundKind.Local)
			{
				if (kind == BoundKind.Sequence)
				{
					return this.DigForValueLocal(topSequence, ((BoundSequence)value).Value);
				}
				if (kind == BoundKind.FieldAccess)
				{
					BoundFieldAccess boundFieldAccess = (BoundFieldAccess)value;
					if (!boundFieldAccess.FieldSymbol.IsStatic)
					{
						BoundExpression receiverOpt = boundFieldAccess.ReceiverOpt;
						if (!receiverOpt.Type.IsReferenceType)
						{
							return this.DigForValueLocal(topSequence, receiverOpt);
						}
					}
				}
			}
			else
			{
				LocalSymbol localSymbol = ((BoundLocal)value).LocalSymbol;
				if (topSequence.Locals.Contains(localSymbol))
				{
					return localSymbol;
				}
			}
			return null;
		}

		// Token: 0x0600801C RID: 32796 RVA: 0x001CDD3C File Offset: 0x001CBF3C
		private void EmitArrayIndices(ImmutableArray<BoundExpression> indices)
		{
			for (int i = 0; i < indices.Length; i++)
			{
				BoundExpression boundExpression = indices[i];
				this.EmitExpression(boundExpression, true);
				this.TreatLongsAsNative(boundExpression.Type.PrimitiveTypeCode);
			}
		}

		// Token: 0x0600801D RID: 32797 RVA: 0x001CDD80 File Offset: 0x001CBF80
		private void EmitArrayElementAddress(BoundArrayAccess arrayAccess, Binder.AddressKind addressKind)
		{
			this.EmitExpression(arrayAccess.Expression, true);
			this.EmitArrayIndices(arrayAccess.Indices);
			if (this.ShouldEmitReadOnlyPrefix(arrayAccess, addressKind))
			{
				this._builder.EmitOpCode(ILOpCode.Readonly);
			}
			if (((ArrayTypeSymbol)arrayAccess.Expression.Type).IsSZArray)
			{
				this._builder.EmitOpCode(ILOpCode.Ldelema);
				TypeSymbol type = arrayAccess.Type;
				this.EmitSymbolToken(type, arrayAccess.Syntax);
				return;
			}
			this._builder.EmitArrayElementAddress(this._module.Translate((ArrayTypeSymbol)arrayAccess.Expression.Type), arrayAccess.Syntax, this._diagnostics);
		}

		// Token: 0x0600801E RID: 32798 RVA: 0x001CDE2E File Offset: 0x001CC02E
		private bool ShouldEmitReadOnlyPrefix(BoundArrayAccess arrayAccess, Binder.AddressKind addressKind)
		{
			return addressKind == Binder.AddressKind.Constrained || (Binder.IsAnyReadOnly(addressKind) && !arrayAccess.Type.IsValueType);
		}

		// Token: 0x0600801F RID: 32799 RVA: 0x001CDE50 File Offset: 0x001CC050
		private LocalDefinition EmitFieldAddress(BoundFieldAccess fieldAccess, Binder.AddressKind addressKind)
		{
			FieldSymbol fieldSymbol = fieldAccess.FieldSymbol;
			if (!this.HasHome(fieldAccess, addressKind))
			{
				return this.EmitAddressOfTempClone(fieldAccess);
			}
			if (fieldAccess.FieldSymbol.IsStatic)
			{
				this.EmitStaticFieldAddress(fieldSymbol, fieldAccess.Syntax);
				return null;
			}
			return this.EmitInstanceFieldAddress(fieldAccess, addressKind);
		}

		// Token: 0x06008020 RID: 32800 RVA: 0x001CDE9A File Offset: 0x001CC09A
		private void EmitStaticFieldAddress(FieldSymbol field, SyntaxNode syntaxNode)
		{
			this._builder.EmitOpCode(ILOpCode.Ldsflda);
			this.EmitSymbolToken(field, syntaxNode);
		}

		// Token: 0x06008021 RID: 32801 RVA: 0x001CDEB1 File Offset: 0x001CC0B1
		private bool HasHome(BoundExpression expression, Binder.AddressKind addressKind)
		{
			return Binder.HasHome(expression, addressKind, this._method, this.IsPeVerifyCompatEnabled(), this._stackLocals);
		}

		// Token: 0x06008022 RID: 32802 RVA: 0x001CDECC File Offset: 0x001CC0CC
		private LocalDefinition EmitParameterAddress(BoundParameter parameter, Binder.AddressKind addressKind)
		{
			ParameterSymbol parameterSymbol = parameter.ParameterSymbol;
			if (!this.HasHome(parameter, addressKind))
			{
				return this.EmitAddressOfTempClone(parameter);
			}
			int num = CodeGenerator.ParameterSlot(parameter);
			if (parameterSymbol.RefKind == RefKind.None)
			{
				this._builder.EmitLoadArgumentAddrOpcode(num);
			}
			else
			{
				this._builder.EmitLoadArgumentOpcode(num);
			}
			return null;
		}

		// Token: 0x06008023 RID: 32803 RVA: 0x001CDF1C File Offset: 0x001CC11C
		private LocalDefinition EmitReceiverRef(BoundExpression receiver, Binder.AddressKind addressKind)
		{
			TypeSymbol type = receiver.Type;
			if (type.IsVerifierReference())
			{
				this.EmitExpression(receiver, true);
				return null;
			}
			if (type.TypeKind != TypeKind.TypeParameter)
			{
				return this.EmitAddress(receiver, addressKind);
			}
			if (addressKind == Binder.AddressKind.Constrained)
			{
				return this.EmitAddress(receiver, addressKind);
			}
			this.EmitExpression(receiver, true);
			if (receiver.Kind != BoundKind.ConditionalReceiver)
			{
				this.EmitBox(receiver.Type, receiver.Syntax);
			}
			return null;
		}

		// Token: 0x06008024 RID: 32804 RVA: 0x001CDF8C File Offset: 0x001CC18C
		private LocalDefinition EmitInstanceFieldAddress(BoundFieldAccess fieldAccess, Binder.AddressKind addressKind)
		{
			FieldSymbol fieldSymbol = fieldAccess.FieldSymbol;
			LocalDefinition localDefinition = this.EmitReceiverRef(fieldAccess.ReceiverOpt, (addressKind == Binder.AddressKind.Constrained) ? Binder.AddressKind.Writeable : addressKind);
			this._builder.EmitOpCode(ILOpCode.Ldflda);
			this.EmitSymbolToken(fieldSymbol, fieldAccess.Syntax);
			if (fieldSymbol.IsFixedSizeBuffer)
			{
				FieldSymbol fixedElementField = fieldSymbol.FixedImplementationType(this._module).FixedElementField;
				if (fixedElementField != null)
				{
					this._builder.EmitOpCode(ILOpCode.Ldflda);
					this.EmitSymbolToken(fixedElementField, fieldAccess.Syntax);
				}
			}
			return localDefinition;
		}

		// Token: 0x06008025 RID: 32805 RVA: 0x001CE008 File Offset: 0x001CC208
		private void EmitArrayInitializers(ArrayTypeSymbol arrayType, BoundArrayInitialization inits)
		{
			ImmutableArray<BoundExpression> initializers = inits.Initializers;
			CodeGenerator.ArrayInitializerStyle arrayInitializerStyle = this.ShouldEmitBlockInitializer(arrayType.ElementType, initializers);
			if (arrayInitializerStyle == CodeGenerator.ArrayInitializerStyle.Element)
			{
				this.EmitElementInitializers(arrayType, initializers, true);
				return;
			}
			ImmutableArray<byte> rawData = this.GetRawData(initializers);
			this._builder.EmitArrayBlockInitializer(rawData, inits.Syntax, this._diagnostics);
			if (arrayInitializerStyle == CodeGenerator.ArrayInitializerStyle.Mixed)
			{
				this.EmitElementInitializers(arrayType, initializers, false);
			}
		}

		// Token: 0x06008026 RID: 32806 RVA: 0x001CE064 File Offset: 0x001CC264
		private void EmitElementInitializers(ArrayTypeSymbol arrayType, ImmutableArray<BoundExpression> inits, bool includeConstants)
		{
			if (!CodeGenerator.IsMultidimensionalInitializer(inits))
			{
				this.EmitVectorElementInitializers(arrayType, inits, includeConstants);
				return;
			}
			this.EmitMultidimensionalElementInitializers(arrayType, inits, includeConstants);
		}

		// Token: 0x06008027 RID: 32807 RVA: 0x001CE084 File Offset: 0x001CC284
		private void EmitVectorElementInitializers(ArrayTypeSymbol arrayType, ImmutableArray<BoundExpression> inits, bool includeConstants)
		{
			for (int i = 0; i < inits.Length; i++)
			{
				BoundExpression boundExpression = inits[i];
				if (CodeGenerator.ShouldEmitInitExpression(includeConstants, boundExpression))
				{
					this._builder.EmitOpCode(ILOpCode.Dup);
					this._builder.EmitIntConstant(i);
					this.EmitExpression(boundExpression, true);
					this.EmitVectorElementStore(arrayType, boundExpression.Syntax);
				}
			}
		}

		// Token: 0x06008028 RID: 32808 RVA: 0x001CE0E3 File Offset: 0x001CC2E3
		private static bool ShouldEmitInitExpression(bool includeConstants, BoundExpression init)
		{
			return !init.IsDefaultValue() && (includeConstants || init.ConstantValue == null);
		}

		// Token: 0x06008029 RID: 32809 RVA: 0x001CE100 File Offset: 0x001CC300
		private void EmitMultidimensionalElementInitializers(ArrayTypeSymbol arrayType, ImmutableArray<BoundExpression> inits, bool includeConstants)
		{
			ArrayBuilder<CodeGenerator.IndexDesc> arrayBuilder = new ArrayBuilder<CodeGenerator.IndexDesc>();
			for (int i = 0; i < inits.Length; i++)
			{
				arrayBuilder.Push(new CodeGenerator.IndexDesc(i, ((BoundArrayInitialization)inits[i]).Initializers));
				this.EmitAllElementInitializersRecursive(arrayType, arrayBuilder, includeConstants);
			}
		}

		// Token: 0x0600802A RID: 32810 RVA: 0x001CE14C File Offset: 0x001CC34C
		private void EmitAllElementInitializersRecursive(ArrayTypeSymbol arrayType, ArrayBuilder<CodeGenerator.IndexDesc> indices, bool includeConstants)
		{
			ImmutableArray<BoundExpression> initializers = indices.Peek<CodeGenerator.IndexDesc>().Initializers;
			if (CodeGenerator.IsMultidimensionalInitializer(initializers))
			{
				for (int i = 0; i < initializers.Length; i++)
				{
					indices.Push(new CodeGenerator.IndexDesc(i, ((BoundArrayInitialization)initializers[i]).Initializers));
					this.EmitAllElementInitializersRecursive(arrayType, indices, includeConstants);
				}
			}
			else
			{
				for (int j = 0; j < initializers.Length; j++)
				{
					BoundExpression boundExpression = initializers[j];
					if (CodeGenerator.ShouldEmitInitExpression(includeConstants, boundExpression))
					{
						this._builder.EmitOpCode(ILOpCode.Dup);
						foreach (CodeGenerator.IndexDesc indexDesc in indices)
						{
							this._builder.EmitIntConstant(indexDesc.Index);
						}
						this._builder.EmitIntConstant(j);
						BoundExpression boundExpression2 = initializers[j];
						this.EmitExpression(boundExpression2, true);
						this.EmitArrayElementStore(arrayType, boundExpression.Syntax);
					}
				}
			}
			indices.Pop<CodeGenerator.IndexDesc>();
		}

		// Token: 0x0600802B RID: 32811 RVA: 0x001CE240 File Offset: 0x001CC440
		private static ConstantValue AsConstOrDefault(BoundExpression init)
		{
			ConstantValue constantValue = init.ConstantValue;
			if (constantValue != null)
			{
				return constantValue;
			}
			return ConstantValue.Default(init.Type.EnumUnderlyingTypeOrSelf().SpecialType);
		}

		// Token: 0x17002648 RID: 9800
		// (get) Token: 0x0600802C RID: 32812 RVA: 0x001CE274 File Offset: 0x001CC474
		private bool EnableEnumArrayBlockInitialization
		{
			get
			{
				Symbol wellKnownTypeMember = this._module.Compilation.GetWellKnownTypeMember(WellKnownMember.System_Runtime_GCLatencyMode__SustainedLowLatency);
				return wellKnownTypeMember != null && wellKnownTypeMember.ContainingAssembly == this._module.Compilation.Assembly.CorLibrary;
			}
		}

		// Token: 0x0600802D RID: 32813 RVA: 0x001CE2C4 File Offset: 0x001CC4C4
		private CodeGenerator.ArrayInitializerStyle ShouldEmitBlockInitializer(TypeSymbol elementType, ImmutableArray<BoundExpression> inits)
		{
			if (!this._module.SupportsPrivateImplClass)
			{
				return CodeGenerator.ArrayInitializerStyle.Element;
			}
			if (elementType.IsEnumType())
			{
				if (!this.EnableEnumArrayBlockInitialization)
				{
					return CodeGenerator.ArrayInitializerStyle.Element;
				}
				elementType = elementType.EnumUnderlyingTypeOrSelf();
			}
			if (elementType.SpecialType.IsBlittable())
			{
				if (this._module.GetInitArrayHelper() == null)
				{
					return CodeGenerator.ArrayInitializerStyle.Element;
				}
				int num = 0;
				int num2 = 0;
				this.InitializerCountRecursive(inits, ref num, ref num2);
				if (num > 2)
				{
					if (num == num2)
					{
						return CodeGenerator.ArrayInitializerStyle.Block;
					}
					int num3 = Math.Max(3, num / 3);
					if (num2 >= num3)
					{
						return CodeGenerator.ArrayInitializerStyle.Mixed;
					}
				}
			}
			return CodeGenerator.ArrayInitializerStyle.Element;
		}

		// Token: 0x0600802E RID: 32814 RVA: 0x001CE340 File Offset: 0x001CC540
		private void InitializerCountRecursive(ImmutableArray<BoundExpression> inits, ref int initCount, ref int constInits)
		{
			if (inits.Length == 0)
			{
				return;
			}
			foreach (BoundExpression boundExpression in inits)
			{
				BoundArrayInitialization boundArrayInitialization = boundExpression as BoundArrayInitialization;
				if (boundArrayInitialization != null)
				{
					this.InitializerCountRecursive(boundArrayInitialization.Initializers, ref initCount, ref constInits);
				}
				else if (!boundExpression.IsDefaultValue())
				{
					initCount++;
					if (boundExpression.ConstantValue != null)
					{
						constInits++;
					}
				}
			}
		}

		// Token: 0x0600802F RID: 32815 RVA: 0x001CE3B0 File Offset: 0x001CC5B0
		private ImmutableArray<byte> GetRawData(ImmutableArray<BoundExpression> initializers)
		{
			BlobBuilder blobBuilder = new BlobBuilder(initializers.Length * 4);
			this.SerializeArrayRecursive(blobBuilder, initializers);
			return blobBuilder.ToImmutableArray();
		}

		// Token: 0x06008030 RID: 32816 RVA: 0x001CE3DC File Offset: 0x001CC5DC
		private void SerializeArrayRecursive(BlobBuilder bw, ImmutableArray<BoundExpression> inits)
		{
			if (inits.Length != 0)
			{
				if (inits[0].Kind == BoundKind.ArrayInitialization)
				{
					foreach (BoundExpression boundExpression in inits)
					{
						this.SerializeArrayRecursive(bw, ((BoundArrayInitialization)boundExpression).Initializers);
					}
					return;
				}
				foreach (BoundExpression boundExpression2 in inits)
				{
					CodeGenerator.AsConstOrDefault(boundExpression2).Serialize(bw);
				}
			}
		}

		// Token: 0x06008031 RID: 32817 RVA: 0x001CE459 File Offset: 0x001CC659
		private static bool IsMultidimensionalInitializer(ImmutableArray<BoundExpression> inits)
		{
			return inits.Length != 0 && inits[0].Kind == BoundKind.ArrayInitialization;
		}

		// Token: 0x06008032 RID: 32818 RVA: 0x001CE47C File Offset: 0x001CC67C
		private bool TryEmitReadonlySpanAsBlobWrapper(NamedTypeSymbol spanType, BoundExpression wrappedExpression, bool used, bool inPlace)
		{
			ImmutableArray<byte> immutableArray = default(ImmutableArray<byte>);
			int num = -1;
			if (!this._module.SupportsPrivateImplClass)
			{
				return false;
			}
			MethodSymbol methodSymbol = (MethodSymbol)this._module.Compilation.GetWellKnownTypeMember(WellKnownMember.System_ReadOnlySpan_T__ctor);
			if (methodSymbol == null)
			{
				return false;
			}
			BoundArrayCreation boundArrayCreation = wrappedExpression as BoundArrayCreation;
			if (boundArrayCreation != null)
			{
				if (((ArrayTypeSymbol)boundArrayCreation.Type).ElementType.EnumUnderlyingTypeOrSelf().SpecialType.SizeInBytes() != 1)
				{
					return false;
				}
				num = this.TryGetRawDataForArrayInit(boundArrayCreation.InitializerOpt, out immutableArray);
			}
			if (num < 0)
			{
				return false;
			}
			if (!inPlace && !used)
			{
				return true;
			}
			if (num == 0)
			{
				if (inPlace)
				{
					this._builder.EmitOpCode(ILOpCode.Initobj);
					this.EmitSymbolToken(spanType, wrappedExpression.Syntax);
				}
				else
				{
					this.EmitDefaultValue(spanType, used, wrappedExpression.Syntax);
				}
			}
			else
			{
				if (this.IsPeVerifyCompatEnabled())
				{
					return false;
				}
				this._builder.EmitArrayBlockFieldRef(immutableArray, wrappedExpression.Syntax, this._diagnostics);
				this._builder.EmitIntConstant(num);
				if (inPlace)
				{
					this._builder.EmitOpCode(ILOpCode.Call, -3);
				}
				else
				{
					this._builder.EmitOpCode(ILOpCode.Newobj, -1);
				}
				this.EmitSymbolToken(methodSymbol.AsMember(spanType), wrappedExpression.Syntax, null, false);
			}
			return true;
		}

		// Token: 0x06008033 RID: 32819 RVA: 0x001CE5B4 File Offset: 0x001CC7B4
		private int TryGetRawDataForArrayInit(BoundArrayInitialization initializer, out ImmutableArray<byte> data)
		{
			data = default(ImmutableArray<byte>);
			if (initializer == null)
			{
				return -1;
			}
			ImmutableArray<BoundExpression> initializers = initializer.Initializers;
			if (initializers.Any<BoundExpression>((BoundExpression init) => init.ConstantValue == null))
			{
				return -1;
			}
			int length = initializers.Length;
			if (length == 0)
			{
				data = ImmutableArray<byte>.Empty;
				return 0;
			}
			BlobBuilder blobBuilder = new BlobBuilder(initializers.Length * 4);
			foreach (BoundExpression boundExpression in initializer.Initializers)
			{
				boundExpression.ConstantValue.Serialize(blobBuilder);
			}
			data = blobBuilder.ToImmutableArray();
			return length;
		}

		// Token: 0x06008034 RID: 32820 RVA: 0x001CE660 File Offset: 0x001CC860
		private static bool IsNumeric(TypeSymbol type)
		{
			switch (type.PrimitiveTypeCode)
			{
			case Microsoft.Cci.PrimitiveTypeCode.Char:
			case Microsoft.Cci.PrimitiveTypeCode.Int8:
			case Microsoft.Cci.PrimitiveTypeCode.Float32:
			case Microsoft.Cci.PrimitiveTypeCode.Float64:
			case Microsoft.Cci.PrimitiveTypeCode.Int16:
			case Microsoft.Cci.PrimitiveTypeCode.Int32:
			case Microsoft.Cci.PrimitiveTypeCode.Int64:
			case Microsoft.Cci.PrimitiveTypeCode.UInt8:
			case Microsoft.Cci.PrimitiveTypeCode.UInt16:
			case Microsoft.Cci.PrimitiveTypeCode.UInt32:
			case Microsoft.Cci.PrimitiveTypeCode.UInt64:
				return true;
			case Microsoft.Cci.PrimitiveTypeCode.IntPtr:
			case Microsoft.Cci.PrimitiveTypeCode.UIntPtr:
				return type.IsNativeIntegerType;
			}
			return false;
		}

		// Token: 0x06008035 RID: 32821 RVA: 0x001CE6C8 File Offset: 0x001CC8C8
		private void EmitConversionExpression(BoundConversion conversion, bool used)
		{
			ConversionKind conversionKind = conversion.ConversionKind;
			if (conversionKind == ConversionKind.ImplicitNullToPointer)
			{
				this._builder.EmitIntConstant(0);
				this._builder.EmitOpCode(ILOpCode.Conv_u);
				this.EmitPopIfUnused(used);
				return;
			}
			if (conversionKind == ConversionKind.MethodGroup)
			{
				throw ExceptionUtilities.UnexpectedValue(conversion.ConversionKind);
			}
			BoundExpression operand = conversion.Operand;
			if (!used && !conversion.ConversionHasSideEffects())
			{
				this.EmitExpression(operand, false);
				return;
			}
			this.EmitExpression(operand, true);
			this.EmitConversion(conversion);
			this.EmitPopIfUnused(used);
		}

		// Token: 0x06008036 RID: 32822 RVA: 0x001CE74C File Offset: 0x001CC94C
		private void EmitReadOnlySpanFromArrayExpression(BoundReadOnlySpanFromArray expression, bool used)
		{
			BoundExpression operand = expression.Operand;
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)expression.Type;
			if (!this.TryEmitReadonlySpanAsBlobWrapper(namedTypeSymbol, operand, used, false))
			{
				this.EmitExpression(operand, used);
				if (used)
				{
					this._builder.EmitOpCode(ILOpCode.Call, 0);
					this.EmitSymbolToken(expression.ConversionMethod, expression.Syntax, null, false);
				}
			}
		}

		// Token: 0x06008037 RID: 32823 RVA: 0x001CE7A8 File Offset: 0x001CC9A8
		private void EmitConversion(BoundConversion conversion)
		{
			switch (conversion.ConversionKind)
			{
			case ConversionKind.Identity:
				this.EmitIdentityConversion(conversion);
				return;
			case ConversionKind.ImplicitNumeric:
			case ConversionKind.ExplicitNumeric:
				this.EmitNumericConversion(conversion);
				return;
			case ConversionKind.ImplicitEnumeration:
			case ConversionKind.ExplicitEnumeration:
				this.EmitEnumConversion(conversion);
				return;
			case ConversionKind.ImplicitThrow:
			case ConversionKind.ImplicitTupleLiteral:
			case ConversionKind.ImplicitTuple:
			case ConversionKind.ExplicitTupleLiteral:
			case ConversionKind.ExplicitTuple:
			case ConversionKind.ImplicitDynamic:
			case ConversionKind.ExplicitDynamic:
			case ConversionKind.ImplicitUserDefined:
			case ConversionKind.AnonymousFunction:
			case ConversionKind.MethodGroup:
			case ConversionKind.ExplicitUserDefined:
				throw ExceptionUtilities.UnexpectedValue(conversion.ConversionKind);
			case ConversionKind.ImplicitReference:
			case ConversionKind.Boxing:
				this.EmitImplicitReferenceConversion(conversion);
				return;
			case ConversionKind.ImplicitPointerToVoid:
			case ConversionKind.ImplicitPointer:
			case ConversionKind.ExplicitPointerToPointer:
				return;
			case ConversionKind.ImplicitNullToPointer:
				throw ExceptionUtilities.UnexpectedValue(conversion.ConversionKind);
			case ConversionKind.ExplicitReference:
			case ConversionKind.Unboxing:
				this.EmitExplicitReferenceConversion(conversion);
				return;
			case ConversionKind.ExplicitIntegerToPointer:
			case ConversionKind.ExplicitPointerToInteger:
			{
				Microsoft.Cci.PrimitiveTypeCode primitiveTypeCode = conversion.Operand.Type.PrimitiveTypeCode;
				Microsoft.Cci.PrimitiveTypeCode primitiveTypeCode2 = conversion.Type.PrimitiveTypeCode;
				this._builder.EmitNumericConversion(primitiveTypeCode, primitiveTypeCode2, conversion.Checked);
				return;
			}
			case ConversionKind.PinnedObjectToPointer:
				this._builder.EmitOpCode(ILOpCode.Conv_u);
				return;
			}
			throw ExceptionUtilities.UnexpectedValue(conversion.ConversionKind);
		}

		// Token: 0x06008038 RID: 32824 RVA: 0x001CE900 File Offset: 0x001CCB00
		private void EmitIdentityConversion(BoundConversion conversion)
		{
			if (conversion.ExplicitCastInCode)
			{
				Microsoft.Cci.PrimitiveTypeCode primitiveTypeCode = conversion.Type.PrimitiveTypeCode;
				if (primitiveTypeCode - Microsoft.Cci.PrimitiveTypeCode.Float32 <= 1 && conversion.Operand.ConstantValue == null)
				{
					this.EmitNumericConversion(conversion);
				}
			}
		}

		// Token: 0x06008039 RID: 32825 RVA: 0x001CE944 File Offset: 0x001CCB44
		private void EmitNumericConversion(BoundConversion conversion)
		{
			Microsoft.Cci.PrimitiveTypeCode primitiveTypeCode = conversion.Operand.Type.PrimitiveTypeCode;
			Microsoft.Cci.PrimitiveTypeCode primitiveTypeCode2 = conversion.Type.PrimitiveTypeCode;
			this._builder.EmitNumericConversion(primitiveTypeCode, primitiveTypeCode2, conversion.Checked);
		}

		// Token: 0x0600803A RID: 32826 RVA: 0x001CE984 File Offset: 0x001CCB84
		private void EmitImplicitReferenceConversion(BoundConversion conversion)
		{
			if (!conversion.Operand.Type.IsVerifierReference())
			{
				this.EmitBox(conversion.Operand.Type, conversion.Operand.Syntax);
			}
			TypeSymbol type = conversion.Type;
			if (!type.IsVerifierReference())
			{
				this._builder.EmitOpCode(ILOpCode.Unbox_any);
				this.EmitSymbolToken(conversion.Type, conversion.Syntax);
				return;
			}
			if (type.IsArray())
			{
				this.EmitStaticCast(conversion.Type, conversion.Syntax);
			}
		}

		// Token: 0x0600803B RID: 32827 RVA: 0x001CEA0C File Offset: 0x001CCC0C
		private void EmitExplicitReferenceConversion(BoundConversion conversion)
		{
			if (!conversion.Operand.Type.IsVerifierReference())
			{
				this.EmitBox(conversion.Operand.Type, conversion.Operand.Syntax);
			}
			if (conversion.Type.IsVerifierReference())
			{
				this._builder.EmitOpCode(ILOpCode.Castclass);
				this.EmitSymbolToken(conversion.Type, conversion.Syntax);
				return;
			}
			this._builder.EmitOpCode(ILOpCode.Unbox_any);
			this.EmitSymbolToken(conversion.Type, conversion.Syntax);
		}

		// Token: 0x0600803C RID: 32828 RVA: 0x001CEA98 File Offset: 0x001CCC98
		private void EmitEnumConversion(BoundConversion conversion)
		{
			TypeSymbol typeSymbol = conversion.Operand.Type;
			if (typeSymbol.IsEnumType())
			{
				typeSymbol = ((NamedTypeSymbol)typeSymbol).EnumUnderlyingType;
			}
			Microsoft.Cci.PrimitiveTypeCode primitiveTypeCode = typeSymbol.PrimitiveTypeCode;
			TypeSymbol typeSymbol2 = conversion.Type;
			if (typeSymbol2.IsEnumType())
			{
				typeSymbol2 = ((NamedTypeSymbol)typeSymbol2).EnumUnderlyingType;
			}
			Microsoft.Cci.PrimitiveTypeCode primitiveTypeCode2 = typeSymbol2.PrimitiveTypeCode;
			this._builder.EmitNumericConversion(primitiveTypeCode, primitiveTypeCode2, conversion.Checked);
		}

		// Token: 0x0600803D RID: 32829 RVA: 0x001CEB04 File Offset: 0x001CCD04
		private void EmitDelegateCreation(BoundExpression node, BoundExpression receiver, bool isExtensionMethod, MethodSymbol method, TypeSymbol delegateType, bool used)
		{
			bool flag = receiver == null || (!isExtensionMethod && method.IsStatic);
			if (!used)
			{
				if (!flag)
				{
					this.EmitExpression(receiver, false);
				}
				return;
			}
			if (flag)
			{
				this._builder.EmitNullConstant();
			}
			else
			{
				this.EmitExpression(receiver, true);
				if (!receiver.Type.IsVerifierReference())
				{
					this.EmitBox(receiver.Type, receiver.Syntax);
				}
			}
			if (method.IsMetadataVirtual(false) && !method.ContainingType.IsDelegateType() && !receiver.SuppressVirtualCalls)
			{
				this._builder.EmitOpCode(ILOpCode.Dup);
				this._builder.EmitOpCode(ILOpCode.Ldvirtftn);
				method = method.GetConstructedLeastOverriddenMethod(this._method.ContainingType, true);
			}
			else
			{
				this._builder.EmitOpCode(ILOpCode.Ldftn);
			}
			this.EmitSymbolToken(method, node.Syntax, null, false);
			this._builder.EmitOpCode(ILOpCode.Newobj, -1);
			MethodSymbol methodSymbol = this.DelegateConstructor(node.Syntax, delegateType);
			if (methodSymbol != null)
			{
				this.EmitSymbolToken(methodSymbol, node.Syntax, null, false);
			}
		}

		// Token: 0x0600803E RID: 32830 RVA: 0x001CEC10 File Offset: 0x001CCE10
		private MethodSymbol DelegateConstructor(SyntaxNode syntax, TypeSymbol delegateType)
		{
			foreach (Symbol symbol in delegateType.GetMembers(".ctor"))
			{
				MethodSymbol methodSymbol = symbol as MethodSymbol;
				if (methodSymbol != null)
				{
					ImmutableArray<ParameterSymbol> parameters = methodSymbol.Parameters;
					if (parameters.Length == 2 && parameters[0].Type.SpecialType == SpecialType.System_Object)
					{
						SpecialType specialType = parameters[1].Type.SpecialType;
						if (specialType == SpecialType.System_IntPtr || specialType == SpecialType.System_UIntPtr)
						{
							return methodSymbol;
						}
					}
				}
			}
			this._diagnostics.Add(ErrorCode.ERR_BadDelegateConstructor, syntax.Location, new object[] { delegateType });
			return null;
		}

		// Token: 0x0600803F RID: 32831 RVA: 0x001CECB8 File Offset: 0x001CCEB8
		private void EmitExpression(BoundExpression expression, bool used)
		{
			if (expression == null)
			{
				return;
			}
			ConstantValue constantValue = expression.ConstantValue;
			if (constantValue != null)
			{
				if (!used)
				{
					return;
				}
				if (expression.Type == null || expression.Type.SpecialType != SpecialType.System_Decimal)
				{
					this.EmitConstantExpression(expression.Type, constantValue, used, expression.Syntax);
					return;
				}
			}
			this._recursionDepth++;
			if (this._recursionDepth > 1)
			{
				StackGuard.EnsureSufficientExecutionStack(this._recursionDepth);
				this.EmitExpressionCore(expression, used);
			}
			else
			{
				this.EmitExpressionCoreWithStackGuard(expression, used);
			}
			this._recursionDepth--;
		}

		// Token: 0x06008040 RID: 32832 RVA: 0x001CED4C File Offset: 0x001CCF4C
		private void EmitExpressionCoreWithStackGuard(BoundExpression expression, bool used)
		{
			try
			{
				this.EmitExpressionCore(expression, used);
			}
			catch (InsufficientExecutionStackException)
			{
				this._diagnostics.Add(ErrorCode.ERR_InsufficientStack, BoundTreeVisitor.CancelledByStackGuardException.GetTooLongOrComplexExpressionErrorLocation(expression));
				throw new CodeGenerator.EmitCancelledException();
			}
		}

		// Token: 0x06008041 RID: 32833 RVA: 0x001CED94 File Offset: 0x001CCF94
		private void EmitExpressionCore(BoundExpression expression, bool used)
		{
			BoundKind kind = expression.Kind;
			if (kind <= BoundKind.ComplexConditionalReceiver)
			{
				switch (kind)
				{
				case BoundKind.Dup:
					this.EmitDupExpression((BoundDup)expression, used);
					return;
				case BoundKind.PassByCopy:
					this.EmitExpression(((BoundPassByCopy)expression).Expression, used);
					return;
				case BoundKind.BadExpression:
				case BoundKind.BadStatement:
				case BoundKind.ExtractedFinallyBlock:
				case BoundKind.TypeExpression:
				case BoundKind.TypeOrValueExpression:
				case BoundKind.NamespaceExpression:
				case BoundKind.IncrementOperator:
				case BoundKind.UnconvertedAddressOfOperator:
				case BoundKind.PointerElementAccess:
				case BoundKind.FromEndIndexExpression:
				case BoundKind.RangeExpression:
				case BoundKind.TupleBinaryOperator:
				case BoundKind.UserDefinedConditionalLogicalOperator:
				case BoundKind.CompoundAssignmentOperator:
				case BoundKind.DeconstructionAssignmentOperator:
				case BoundKind.NullCoalescingAssignmentOperator:
				case BoundKind.UnconvertedConditionalOperator:
				case BoundKind.AwaitableInfo:
				case BoundKind.AwaitExpression:
				case BoundKind.DefaultLiteral:
					goto IL_04C9;
				case BoundKind.UnaryOperator:
					this.EmitUnaryOperatorExpression((BoundUnaryOperator)expression, used);
					return;
				case BoundKind.AddressOfOperator:
					this.EmitAddressOfExpression((BoundAddressOfOperator)expression, used);
					return;
				case BoundKind.FunctionPointerLoad:
					this.EmitLoadFunction((BoundFunctionPointerLoad)expression, used);
					return;
				case BoundKind.PointerIndirectionOperator:
					this.EmitPointerIndirectionOperator((BoundPointerIndirectionOperator)expression, used);
					return;
				case BoundKind.FunctionPointerInvocation:
					this.EmitCalli((BoundFunctionPointerInvocation)expression, used ? CodeGenerator.UseKind.UsedAsValue : CodeGenerator.UseKind.Unused);
					return;
				case BoundKind.RefTypeOperator:
					this.EmitRefTypeOperator((BoundRefTypeOperator)expression, used);
					return;
				case BoundKind.MakeRefOperator:
					this.EmitMakeRefOperator((BoundMakeRefOperator)expression, used);
					return;
				case BoundKind.RefValueOperator:
					this.EmitRefValueOperator((BoundRefValueOperator)expression, used);
					return;
				case BoundKind.BinaryOperator:
					this.EmitBinaryOperatorExpression((BoundBinaryOperator)expression, used);
					return;
				case BoundKind.AssignmentOperator:
					this.EmitAssignmentExpression((BoundAssignmentOperator)expression, used ? CodeGenerator.UseKind.UsedAsValue : CodeGenerator.UseKind.Unused);
					return;
				case BoundKind.NullCoalescingOperator:
					this.EmitNullCoalescingOperator((BoundNullCoalescingOperator)expression, used);
					return;
				case BoundKind.ConditionalOperator:
					this.EmitConditionalOperator((BoundConditionalOperator)expression, used);
					return;
				case BoundKind.ArrayAccess:
					this.EmitArrayElementLoad((BoundArrayAccess)expression, used);
					return;
				case BoundKind.ArrayLength:
					this.EmitArrayLength((BoundArrayLength)expression, used);
					return;
				case BoundKind.TypeOfOperator:
					if (used)
					{
						this.EmitTypeOfExpression((BoundTypeOfOperator)expression);
						return;
					}
					break;
				case BoundKind.MethodDefIndex:
					this.EmitMethodDefIndexExpression((BoundMethodDefIndex)expression);
					return;
				case BoundKind.MaximumMethodDefIndex:
					this.EmitMaximumMethodDefIndexExpression((BoundMaximumMethodDefIndex)expression);
					return;
				case BoundKind.InstrumentationPayloadRoot:
					this.EmitInstrumentationPayloadRootLoad((BoundInstrumentationPayloadRoot)expression);
					return;
				case BoundKind.ModuleVersionId:
					this.EmitModuleVersionIdLoad((BoundModuleVersionId)expression);
					return;
				case BoundKind.ModuleVersionIdString:
					this.EmitModuleVersionIdStringLoad((BoundModuleVersionIdString)expression);
					return;
				case BoundKind.SourceDocumentIndex:
					this.EmitSourceDocumentIndex((BoundSourceDocumentIndex)expression);
					return;
				case BoundKind.MethodInfo:
					if (used)
					{
						this.EmitMethodInfoExpression((BoundMethodInfo)expression);
						return;
					}
					break;
				case BoundKind.FieldInfo:
					if (used)
					{
						this.EmitFieldInfoExpression((BoundFieldInfo)expression);
						return;
					}
					break;
				case BoundKind.DefaultExpression:
					this.EmitDefaultExpression((BoundDefaultExpression)expression, used);
					return;
				case BoundKind.IsOperator:
					this.EmitIsExpression((BoundIsOperator)expression, used);
					return;
				case BoundKind.AsOperator:
					this.EmitAsExpression((BoundAsOperator)expression, used);
					return;
				case BoundKind.SizeOfOperator:
					if (used)
					{
						this.EmitSizeOfExpression((BoundSizeOfOperator)expression);
						return;
					}
					break;
				case BoundKind.Conversion:
					this.EmitConversionExpression((BoundConversion)expression, used);
					return;
				case BoundKind.ReadOnlySpanFromArray:
					this.EmitReadOnlySpanFromArrayExpression((BoundReadOnlySpanFromArray)expression, used);
					return;
				case BoundKind.ArgList:
					this.EmitArgList(used);
					return;
				case BoundKind.ArgListOperator:
					this.EmitArgListOperator((BoundArgListOperator)expression);
					return;
				default:
					switch (kind)
					{
					case BoundKind.ThisReference:
						if (used)
						{
							this.EmitThisReferenceExpression((BoundThisReference)expression);
							return;
						}
						break;
					case BoundKind.PreviousSubmissionReference:
						throw ExceptionUtilities.UnexpectedValue(expression.Kind);
					case BoundKind.HostObjectMemberReference:
					case BoundKind.RangeVariable:
						goto IL_04C9;
					case BoundKind.BaseReference:
						if (used)
						{
							NamedTypeSymbol containingType = this._method.ContainingType;
							this._builder.EmitOpCode(ILOpCode.Ldarg_0);
							if (containingType.IsValueType)
							{
								this.EmitLoadIndirect(containingType, expression.Syntax);
								this.EmitBox(containingType, expression.Syntax);
								return;
							}
						}
						break;
					case BoundKind.Local:
						this.EmitLocalLoad((BoundLocal)expression, used);
						return;
					case BoundKind.PseudoVariable:
						this.EmitPseudoVariableValue((BoundPseudoVariable)expression, used);
						return;
					case BoundKind.Parameter:
						if (used)
						{
							this.EmitParameterLoad((BoundParameter)expression);
							return;
						}
						break;
					default:
						switch (kind)
						{
						case BoundKind.SequencePointExpression:
							this.EmitSequencePointExpression((BoundSequencePointExpression)expression, used);
							return;
						case BoundKind.Sequence:
							this.EmitSequenceExpression((BoundSequence)expression, used);
							return;
						case BoundKind.SpillSequence:
						case BoundKind.DynamicMemberAccess:
						case BoundKind.DynamicInvocation:
						case BoundKind.ConditionalAccess:
							goto IL_04C9;
						case BoundKind.LoweredConditionalAccess:
							this.EmitLoweredConditionalAccessExpression((BoundLoweredConditionalAccess)expression, used);
							return;
						case BoundKind.ConditionalReceiver:
							this.EmitConditionalReceiver((BoundConditionalReceiver)expression, used);
							return;
						case BoundKind.ComplexConditionalReceiver:
							this.EmitComplexConditionalReceiver((BoundComplexConditionalReceiver)expression, used);
							return;
						default:
							goto IL_04C9;
						}
						break;
					}
					break;
				}
				return;
			}
			if (kind <= BoundKind.ObjectCreationExpression)
			{
				if (kind == BoundKind.Call)
				{
					this.EmitCallExpression((BoundCall)expression, used ? CodeGenerator.UseKind.UsedAsValue : CodeGenerator.UseKind.Unused);
					return;
				}
				if (kind == BoundKind.ObjectCreationExpression)
				{
					this.EmitObjectCreationExpression((BoundObjectCreationExpression)expression, used);
					return;
				}
			}
			else
			{
				switch (kind)
				{
				case BoundKind.DelegateCreationExpression:
					this.EmitDelegateCreationExpression((BoundDelegateCreationExpression)expression, used);
					return;
				case BoundKind.ArrayCreation:
					this.EmitArrayCreationExpression((BoundArrayCreation)expression, used);
					return;
				case BoundKind.ArrayInitialization:
				case BoundKind.StackAllocArrayCreation:
					break;
				case BoundKind.ConvertedStackAllocExpression:
					this.EmitConvertedStackAllocExpression((BoundConvertedStackAllocExpression)expression, used);
					return;
				case BoundKind.FieldAccess:
					this.EmitFieldLoad((BoundFieldAccess)expression, used);
					return;
				default:
					if (kind == BoundKind.ThrowExpression)
					{
						this.EmitThrowExpression((BoundThrowExpression)expression, used);
						return;
					}
					break;
				}
			}
			IL_04C9:
			throw ExceptionUtilities.UnexpectedValue(expression.Kind);
		}

		// Token: 0x06008042 RID: 32834 RVA: 0x001CF27B File Offset: 0x001CD47B
		private void EmitThrowExpression(BoundThrowExpression node, bool used)
		{
			this.EmitThrow(node.Expression);
			this.EmitDefaultValue(node.Type, used, node.Syntax);
		}

		// Token: 0x06008043 RID: 32835 RVA: 0x001CF29C File Offset: 0x001CD49C
		private void EmitComplexConditionalReceiver(BoundComplexConditionalReceiver expression, bool used)
		{
			TypeSymbol type = expression.Type;
			object obj = new object();
			object obj2 = new object();
			this.EmitInitObj(type, true, expression.Syntax);
			this.EmitBox(type, expression.Syntax);
			this._builder.EmitBranch(ILOpCode.Brtrue, obj, ILOpCode.Nop);
			this.EmitExpression(expression.ReferenceTypeReceiver, used);
			this._builder.EmitBranch(ILOpCode.Br, obj2, ILOpCode.Nop);
			this._builder.AdjustStack(-1);
			this._builder.MarkLabel(obj);
			this.EmitExpression(expression.ValueTypeReceiver, used);
			this._builder.MarkLabel(obj2);
		}

		// Token: 0x06008044 RID: 32836 RVA: 0x001CF334 File Offset: 0x001CD534
		private void EmitLoweredConditionalAccessExpression(BoundLoweredConditionalAccess expression, bool used)
		{
			BoundExpression receiver = expression.Receiver;
			TypeSymbol type = receiver.Type;
			ConstantValue constantValue = receiver.ConstantValue;
			LocalDefinition localDefinition;
			if (constantValue != null && !constantValue.IsNull)
			{
				localDefinition = this.EmitReceiverRef(receiver, Binder.AddressKind.ReadOnly);
				this.EmitExpression(expression.WhenNotNull, used);
				if (localDefinition != null)
				{
					this.FreeTemp(localDefinition);
				}
				return;
			}
			object obj = new object();
			object obj2 = new object();
			LocalDefinition localDefinition2 = null;
			bool flag = !type.IsReferenceType && !type.IsValueType;
			bool flag2 = LocalRewriter.CanChangeValueBetweenReads(receiver, false, false) || (type.IsReferenceType && type.TypeKind == TypeKind.TypeParameter) || (receiver.Kind == BoundKind.Local && this.IsStackLocal(((BoundLocal)receiver).LocalSymbol));
			if (flag2)
			{
				if (flag)
				{
					localDefinition = this.EmitReceiverRef(receiver, Binder.AddressKind.Constrained);
					if (localDefinition == null)
					{
						this.EmitDefaultValue(type, true, receiver.Syntax);
						this.EmitBox(type, receiver.Syntax);
						this._builder.EmitBranch(ILOpCode.Brtrue, obj, ILOpCode.Nop);
						this.EmitLoadIndirect(type, receiver.Syntax);
						localDefinition2 = this.AllocateTemp(type, receiver.Syntax, LocalSlotConstraints.None);
						this._builder.EmitLocalStore(localDefinition2);
						this._builder.EmitLocalAddress(localDefinition2);
						this._builder.EmitLocalLoad(localDefinition2);
						this.EmitBox(type, receiver.Syntax);
					}
					else
					{
						this._builder.EmitOpCode(ILOpCode.Dup);
						this.EmitLoadIndirect(type, receiver.Syntax);
						this.EmitBox(type, receiver.Syntax);
					}
				}
				else
				{
					Binder.AddressKind addressKind = Binder.AddressKind.ReadOnly;
					localDefinition = this.EmitReceiverRef(receiver, addressKind);
					this._builder.EmitOpCode(ILOpCode.Dup);
				}
			}
			else
			{
				localDefinition = this.EmitReceiverRef(receiver, Binder.AddressKind.ReadOnly);
			}
			MethodSymbol hasValueMethodOpt = expression.HasValueMethodOpt;
			if (hasValueMethodOpt != null)
			{
				this._builder.EmitOpCode(ILOpCode.Call, 0);
				this.EmitSymbolToken(hasValueMethodOpt, expression.Syntax, null, false);
			}
			this._builder.EmitBranch(ILOpCode.Brtrue, obj, ILOpCode.Nop);
			if (localDefinition != null && !flag2)
			{
				this.FreeTemp(localDefinition);
				localDefinition = null;
			}
			if (flag2)
			{
				this._builder.EmitOpCode(ILOpCode.Pop);
			}
			BoundExpression whenNullOpt = expression.WhenNullOpt;
			if (whenNullOpt == null)
			{
				this.EmitDefaultValue(expression.Type, used, expression.Syntax);
			}
			else
			{
				this.EmitExpression(whenNullOpt, used);
			}
			this._builder.EmitBranch(ILOpCode.Br, obj2, ILOpCode.Nop);
			if (flag2)
			{
				this._builder.AdjustStack(1);
			}
			if (used)
			{
				this._builder.AdjustStack(-1);
			}
			this._builder.MarkLabel(obj);
			if (!flag2)
			{
				localDefinition = this.EmitReceiverRef(receiver, Binder.AddressKind.Constrained);
			}
			this.EmitExpression(expression.WhenNotNull, used);
			this._builder.MarkLabel(obj2);
			if (localDefinition2 != null)
			{
				this.FreeTemp(localDefinition2);
			}
			if (localDefinition != null)
			{
				this.FreeTemp(localDefinition);
			}
		}

		// Token: 0x06008045 RID: 32837 RVA: 0x001CF5D5 File Offset: 0x001CD7D5
		private void EmitConditionalReceiver(BoundConditionalReceiver expression, bool used)
		{
			if (!expression.Type.IsReferenceType)
			{
				this.EmitLoadIndirect(expression.Type, expression.Syntax);
			}
			this.EmitPopIfUnused(used);
		}

		// Token: 0x06008046 RID: 32838 RVA: 0x001CF5FD File Offset: 0x001CD7FD
		private void EmitRefValueOperator(BoundRefValueOperator expression, bool used)
		{
			this.EmitRefValueAddress(expression);
			this.EmitLoadIndirect(expression.Type, expression.Syntax);
			this.EmitPopIfUnused(used);
		}

		// Token: 0x06008047 RID: 32839 RVA: 0x001CF620 File Offset: 0x001CD820
		private void EmitMakeRefOperator(BoundMakeRefOperator expression, bool used)
		{
			this.EmitAddress(expression.Operand, Binder.AddressKind.Writeable);
			this._builder.EmitOpCode(ILOpCode.Mkrefany);
			this.EmitSymbolToken(expression.Operand.Type, expression.Operand.Syntax);
			this.EmitPopIfUnused(used);
		}

		// Token: 0x06008048 RID: 32840 RVA: 0x001CF670 File Offset: 0x001CD870
		private void EmitRefTypeOperator(BoundRefTypeOperator expression, bool used)
		{
			this.EmitExpression(expression.Operand, true);
			this._builder.EmitOpCode(ILOpCode.Refanytype);
			this._builder.EmitOpCode(ILOpCode.Call, 0);
			MethodSymbol getTypeFromHandle = expression.GetTypeFromHandle;
			this.EmitSymbolToken(getTypeFromHandle, expression.Syntax, null, false);
			this.EmitPopIfUnused(used);
		}

		// Token: 0x06008049 RID: 32841 RVA: 0x001CF6C5 File Offset: 0x001CD8C5
		private void EmitArgList(bool used)
		{
			this._builder.EmitOpCode(ILOpCode.Arglist);
			this.EmitPopIfUnused(used);
		}

		// Token: 0x0600804A RID: 32842 RVA: 0x001CF6E0 File Offset: 0x001CD8E0
		private void EmitArgListOperator(BoundArgListOperator expression)
		{
			for (int i = 0; i < expression.Arguments.Length; i++)
			{
				BoundExpression boundExpression = expression.Arguments[i];
				RefKind refKind = (expression.ArgumentRefKindsOpt.IsDefaultOrEmpty ? RefKind.None : expression.ArgumentRefKindsOpt[i]);
				this.EmitArgument(boundExpression, refKind);
			}
		}

		// Token: 0x0600804B RID: 32843 RVA: 0x001CF744 File Offset: 0x001CD944
		private void EmitArgument(BoundExpression argument, RefKind refKind)
		{
			if (refKind == RefKind.None)
			{
				this.EmitExpression(argument, true);
				return;
			}
			if (refKind != RefKind.In)
			{
				LocalDefinition localDefinition = this.EmitAddress(argument, (refKind == (RefKind)4) ? Binder.AddressKind.ReadOnlyStrict : Binder.AddressKind.Writeable);
				if (localDefinition != null)
				{
					this.AddExpressionTemp(localDefinition);
				}
				return;
			}
			LocalDefinition localDefinition2 = this.EmitAddress(argument, Binder.AddressKind.ReadOnly);
			this.AddExpressionTemp(localDefinition2);
		}

		// Token: 0x0600804C RID: 32844 RVA: 0x001CF78E File Offset: 0x001CD98E
		private void EmitAddressOfExpression(BoundAddressOfOperator expression, bool used)
		{
			this.EmitAddress(expression.Operand, Binder.AddressKind.ReadOnlyStrict);
			if (used && !expression.IsManaged)
			{
				this._builder.EmitOpCode(ILOpCode.Conv_u);
			}
			this.EmitPopIfUnused(used);
		}

		// Token: 0x0600804D RID: 32845 RVA: 0x001CF7C0 File Offset: 0x001CD9C0
		private void EmitPointerIndirectionOperator(BoundPointerIndirectionOperator expression, bool used)
		{
			this.EmitExpression(expression.Operand, true);
			this.EmitLoadIndirect(expression.Type, expression.Syntax);
			this.EmitPopIfUnused(used);
		}

		// Token: 0x0600804E RID: 32846 RVA: 0x001CF7E8 File Offset: 0x001CD9E8
		private void EmitDupExpression(BoundDup expression, bool used)
		{
			if (expression.RefKind == RefKind.None)
			{
				if (used)
				{
					this._builder.EmitOpCode(ILOpCode.Dup);
					return;
				}
			}
			else
			{
				this._builder.EmitOpCode(ILOpCode.Dup);
				this.EmitLoadIndirect(expression.Type, expression.Syntax);
				this.EmitPopIfUnused(used);
			}
		}

		// Token: 0x0600804F RID: 32847 RVA: 0x001CF834 File Offset: 0x001CDA34
		private void EmitDelegateCreationExpression(BoundDelegateCreationExpression expression, bool used)
		{
			BoundMethodGroup boundMethodGroup = expression.Argument as BoundMethodGroup;
			BoundExpression boundExpression = ((boundMethodGroup != null) ? boundMethodGroup.ReceiverOpt : expression.Argument);
			MethodSymbol methodSymbol = expression.MethodOpt ?? boundExpression.Type.DelegateInvokeMethod();
			this.EmitDelegateCreation(expression, boundExpression, expression.IsExtensionMethod, methodSymbol, expression.Type, used);
		}

		// Token: 0x06008050 RID: 32848 RVA: 0x001CF88C File Offset: 0x001CDA8C
		private void EmitThisReferenceExpression(BoundThisReference thisRef)
		{
			TypeSymbol type = thisRef.Type;
			this._builder.EmitOpCode(ILOpCode.Ldarg_0);
			if (type.IsValueType)
			{
				this.EmitLoadIndirect(type, thisRef.Syntax);
			}
		}

		// Token: 0x06008051 RID: 32849 RVA: 0x001CF8C1 File Offset: 0x001CDAC1
		private void EmitPseudoVariableValue(BoundPseudoVariable expression, bool used)
		{
			this.EmitExpression(expression.EmitExpressions.GetValue(expression, this._diagnostics), used);
		}

		// Token: 0x06008052 RID: 32850 RVA: 0x001CF8DC File Offset: 0x001CDADC
		private void EmitSequencePointExpression(BoundSequencePointExpression node, bool used)
		{
			this.EmitSequencePoint(node);
			this.EmitExpression(node.Expression, true);
			this.EmitPopIfUnused(used);
		}

		// Token: 0x06008053 RID: 32851 RVA: 0x001CF8FC File Offset: 0x001CDAFC
		private void EmitSequencePoint(BoundSequencePointExpression node)
		{
			SyntaxNode syntax = node.Syntax;
			if (this._emitPdbSequencePoints)
			{
				if (syntax == null)
				{
					this.EmitHiddenSequencePoint();
					return;
				}
				this.EmitSequencePoint(syntax);
			}
		}

		// Token: 0x06008054 RID: 32852 RVA: 0x001CF929 File Offset: 0x001CDB29
		private void EmitSequenceExpression(BoundSequence sequence, bool used)
		{
			this.DefineLocals(sequence);
			this.EmitSideEffects(sequence);
			if (sequence.Value.Kind != BoundKind.TypeExpression)
			{
				this.EmitExpression(sequence.Value, used);
			}
			this.FreeLocals(sequence);
		}

		// Token: 0x06008055 RID: 32853 RVA: 0x001CF95C File Offset: 0x001CDB5C
		private void DefineLocals(BoundSequence sequence)
		{
			if (sequence.Locals.IsEmpty)
			{
				return;
			}
			this._builder.OpenLocalScope(ScopeType.Variable, null);
			foreach (LocalSymbol localSymbol in sequence.Locals)
			{
				this.DefineLocal(localSymbol, sequence.Syntax);
			}
		}

		// Token: 0x06008056 RID: 32854 RVA: 0x001CF9B8 File Offset: 0x001CDBB8
		private void FreeLocals(BoundSequence sequence)
		{
			if (sequence.Locals.IsEmpty)
			{
				return;
			}
			this._builder.CloseLocalScope();
			foreach (LocalSymbol localSymbol in sequence.Locals)
			{
				this.FreeLocal(localSymbol);
			}
		}

		// Token: 0x06008057 RID: 32855 RVA: 0x001CFA0C File Offset: 0x001CDC0C
		private void DefineAndRecordLocals(BoundSequence sequence)
		{
			if (sequence.Locals.IsEmpty)
			{
				return;
			}
			this._builder.OpenLocalScope(ScopeType.Variable, null);
			foreach (LocalSymbol localSymbol in sequence.Locals)
			{
				LocalDefinition localDefinition = this.DefineLocal(localSymbol, sequence.Syntax);
				this.AddExpressionTemp(localDefinition);
			}
		}

		// Token: 0x06008058 RID: 32856 RVA: 0x001CFA70 File Offset: 0x001CDC70
		private void CloseScopeAndKeepLocals(BoundSequence sequence)
		{
			if (sequence.Locals.IsEmpty)
			{
				return;
			}
			this._builder.CloseLocalScope();
		}

		// Token: 0x06008059 RID: 32857 RVA: 0x001CFA9C File Offset: 0x001CDC9C
		private void EmitSideEffects(BoundSequence sequence)
		{
			ImmutableArray<BoundExpression> sideEffects = sequence.SideEffects;
			if (!sideEffects.IsDefaultOrEmpty)
			{
				foreach (BoundExpression boundExpression in sideEffects)
				{
					this.EmitExpression(boundExpression, false);
				}
			}
		}

		// Token: 0x0600805A RID: 32858 RVA: 0x001CFADC File Offset: 0x001CDCDC
		private void EmitArguments(ImmutableArray<BoundExpression> arguments, ImmutableArray<ParameterSymbol> parameters, ImmutableArray<RefKind> argRefKindsOpt)
		{
			for (int i = 0; i < arguments.Length; i++)
			{
				RefKind argumentRefKind = CodeGenerator.GetArgumentRefKind(arguments, parameters, argRefKindsOpt, i);
				this.EmitArgument(arguments[i], argumentRefKind);
			}
		}

		// Token: 0x0600805B RID: 32859 RVA: 0x001CFB14 File Offset: 0x001CDD14
		internal static RefKind GetArgumentRefKind(ImmutableArray<BoundExpression> arguments, ImmutableArray<ParameterSymbol> parameters, ImmutableArray<RefKind> argRefKindsOpt, int i)
		{
			RefKind refKind;
			if (i < parameters.Length)
			{
				if (!argRefKindsOpt.IsDefault && i < argRefKindsOpt.Length)
				{
					refKind = argRefKindsOpt[i];
				}
				else
				{
					refKind = parameters[i].RefKind;
				}
			}
			else
			{
				refKind = RefKind.None;
			}
			return refKind;
		}

		// Token: 0x0600805C RID: 32860 RVA: 0x001CFB5C File Offset: 0x001CDD5C
		private void EmitArrayElementLoad(BoundArrayAccess arrayAccess, bool used)
		{
			this.EmitExpression(arrayAccess.Expression, true);
			this.EmitArrayIndices(arrayAccess.Indices);
			if (((ArrayTypeSymbol)arrayAccess.Expression.Type).IsSZArray)
			{
				TypeSymbol typeSymbol = arrayAccess.Type;
				if (typeSymbol.IsEnumType())
				{
					typeSymbol = ((NamedTypeSymbol)typeSymbol).EnumUnderlyingType;
				}
				switch (typeSymbol.PrimitiveTypeCode)
				{
				case Microsoft.Cci.PrimitiveTypeCode.Boolean:
				case Microsoft.Cci.PrimitiveTypeCode.UInt8:
					this._builder.EmitOpCode(ILOpCode.Ldelem_u1);
					goto IL_0224;
				case Microsoft.Cci.PrimitiveTypeCode.Char:
				case Microsoft.Cci.PrimitiveTypeCode.UInt16:
					this._builder.EmitOpCode(ILOpCode.Ldelem_u2);
					goto IL_0224;
				case Microsoft.Cci.PrimitiveTypeCode.Int8:
					this._builder.EmitOpCode(ILOpCode.Ldelem_i1);
					goto IL_0224;
				case Microsoft.Cci.PrimitiveTypeCode.Float32:
					this._builder.EmitOpCode(ILOpCode.Ldelem_r4);
					goto IL_0224;
				case Microsoft.Cci.PrimitiveTypeCode.Float64:
					this._builder.EmitOpCode(ILOpCode.Ldelem_r8);
					goto IL_0224;
				case Microsoft.Cci.PrimitiveTypeCode.Int16:
					this._builder.EmitOpCode(ILOpCode.Ldelem_i2);
					goto IL_0224;
				case Microsoft.Cci.PrimitiveTypeCode.Int32:
					this._builder.EmitOpCode(ILOpCode.Ldelem_i4);
					goto IL_0224;
				case Microsoft.Cci.PrimitiveTypeCode.Int64:
				case Microsoft.Cci.PrimitiveTypeCode.UInt64:
					this._builder.EmitOpCode(ILOpCode.Ldelem_i8);
					goto IL_0224;
				case Microsoft.Cci.PrimitiveTypeCode.IntPtr:
				case Microsoft.Cci.PrimitiveTypeCode.Pointer:
				case Microsoft.Cci.PrimitiveTypeCode.UIntPtr:
				case Microsoft.Cci.PrimitiveTypeCode.FunctionPointer:
					this._builder.EmitOpCode(ILOpCode.Ldelem_i);
					goto IL_0224;
				case Microsoft.Cci.PrimitiveTypeCode.UInt32:
					this._builder.EmitOpCode(ILOpCode.Ldelem_u4);
					goto IL_0224;
				}
				if (typeSymbol.IsVerifierReference())
				{
					this._builder.EmitOpCode(ILOpCode.Ldelem_ref);
				}
				else
				{
					if (used)
					{
						this._builder.EmitOpCode(ILOpCode.Ldelem);
					}
					else
					{
						if (typeSymbol.TypeKind == TypeKind.TypeParameter)
						{
							this._builder.EmitOpCode(ILOpCode.Readonly);
						}
						this._builder.EmitOpCode(ILOpCode.Ldelema);
					}
					this.EmitSymbolToken(typeSymbol, arrayAccess.Syntax);
				}
			}
			else
			{
				this._builder.EmitArrayElementLoad(this._module.Translate((ArrayTypeSymbol)arrayAccess.Expression.Type), arrayAccess.Expression.Syntax, this._diagnostics);
			}
			IL_0224:
			this.EmitPopIfUnused(used);
		}

		// Token: 0x0600805D RID: 32861 RVA: 0x001CFD94 File Offset: 0x001CDF94
		private void EmitFieldLoad(BoundFieldAccess fieldAccess, bool used)
		{
			FieldSymbol fieldSymbol = fieldAccess.FieldSymbol;
			if (!used)
			{
				if (fieldSymbol.IsCapturedFrame)
				{
					return;
				}
				if (!fieldSymbol.IsVolatile && !fieldSymbol.IsStatic && fieldAccess.ReceiverOpt.Type.IsVerifierValue())
				{
					this.EmitExpression(fieldAccess.ReceiverOpt, false);
					return;
				}
			}
			if (fieldSymbol.IsStatic)
			{
				if (fieldSymbol.IsVolatile)
				{
					this._builder.EmitOpCode(ILOpCode.Volatile);
				}
				this._builder.EmitOpCode(ILOpCode.Ldsfld);
				this.EmitSymbolToken(fieldSymbol, fieldAccess.Syntax);
			}
			else
			{
				BoundExpression receiverOpt = fieldAccess.ReceiverOpt;
				TypeSymbol type = fieldSymbol.Type;
				if (type.IsValueType && type == receiverOpt.Type)
				{
					this.EmitExpression(receiverOpt, used);
				}
				else
				{
					LocalDefinition localDefinition = this.EmitFieldLoadReceiver(receiverOpt);
					if (localDefinition != null)
					{
						this.FreeTemp(localDefinition);
					}
					if (fieldSymbol.IsVolatile)
					{
						this._builder.EmitOpCode(ILOpCode.Volatile);
					}
					this._builder.EmitOpCode(ILOpCode.Ldfld);
					this.EmitSymbolToken(fieldSymbol, fieldAccess.Syntax);
				}
			}
			this.EmitPopIfUnused(used);
		}

		// Token: 0x0600805E RID: 32862 RVA: 0x001CFE94 File Offset: 0x001CE094
		private LocalDefinition EmitFieldLoadReceiver(BoundExpression receiver)
		{
			if (!CodeGenerator.FieldLoadMustUseRef(receiver) && !this.FieldLoadPrefersRef(receiver))
			{
				this.EmitExpression(receiver, true);
				return null;
			}
			if (!this.EmitFieldLoadReceiverAddress(receiver))
			{
				return this.EmitReceiverRef(receiver, Binder.AddressKind.ReadOnly);
			}
			return null;
		}

		// Token: 0x0600805F RID: 32863 RVA: 0x001CFEC4 File Offset: 0x001CE0C4
		private bool EmitFieldLoadReceiverAddress(BoundExpression receiver)
		{
			if (receiver == null || !receiver.Type.IsValueType)
			{
				return false;
			}
			if (receiver.Kind == BoundKind.Conversion)
			{
				BoundConversion boundConversion = (BoundConversion)receiver;
				if (boundConversion.ConversionKind == ConversionKind.Unboxing)
				{
					this.EmitExpression(boundConversion.Operand, true);
					this._builder.EmitOpCode(ILOpCode.Unbox);
					this.EmitSymbolToken(receiver.Type, receiver.Syntax);
					return true;
				}
			}
			else if (receiver.Kind == BoundKind.FieldAccess)
			{
				BoundFieldAccess boundFieldAccess = (BoundFieldAccess)receiver;
				FieldSymbol fieldSymbol = boundFieldAccess.FieldSymbol;
				if (!fieldSymbol.IsStatic && this.EmitFieldLoadReceiverAddress(boundFieldAccess.ReceiverOpt))
				{
					this._builder.EmitOpCode(ILOpCode.Ldflda);
					this.EmitSymbolToken(fieldSymbol, boundFieldAccess.Syntax);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008060 RID: 32864 RVA: 0x001CFF7C File Offset: 0x001CE17C
		private bool FieldLoadPrefersRef(BoundExpression receiver)
		{
			if (!receiver.Type.IsVerifierValue())
			{
				return true;
			}
			if (receiver.Kind == BoundKind.Conversion && ((BoundConversion)receiver).ConversionKind == ConversionKind.Unboxing)
			{
				return true;
			}
			if (!this.HasHome(receiver, Binder.AddressKind.ReadOnly))
			{
				return false;
			}
			BoundKind kind = receiver.Kind;
			if (kind <= BoundKind.Parameter)
			{
				if (kind == BoundKind.Local)
				{
					return ((BoundLocal)receiver).LocalSymbol.RefKind > RefKind.None;
				}
				if (kind == BoundKind.Parameter)
				{
					return ((BoundParameter)receiver).ParameterSymbol.RefKind > RefKind.None;
				}
			}
			else
			{
				if (kind == BoundKind.Sequence)
				{
					return this.FieldLoadPrefersRef(((BoundSequence)receiver).Value);
				}
				if (kind == BoundKind.FieldAccess)
				{
					BoundFieldAccess boundFieldAccess = (BoundFieldAccess)receiver;
					return boundFieldAccess.FieldSymbol.IsStatic || (!DiagnosticsPass.IsNonAgileFieldAccess(boundFieldAccess, this._module.Compilation) && this.FieldLoadPrefersRef(boundFieldAccess.ReceiverOpt));
				}
			}
			return true;
		}

		// Token: 0x06008061 RID: 32865 RVA: 0x001D0060 File Offset: 0x001CE260
		internal static bool FieldLoadMustUseRef(BoundExpression expr)
		{
			TypeSymbol type = expr.Type;
			if (type.IsTypeParameter())
			{
				return true;
			}
			switch (type.SpecialType)
			{
			case SpecialType.System_Boolean:
			case SpecialType.System_Char:
			case SpecialType.System_SByte:
			case SpecialType.System_Byte:
			case SpecialType.System_Int16:
			case SpecialType.System_UInt16:
			case SpecialType.System_Int32:
			case SpecialType.System_UInt32:
			case SpecialType.System_Int64:
			case SpecialType.System_UInt64:
			case SpecialType.System_Single:
			case SpecialType.System_Double:
			case SpecialType.System_IntPtr:
			case SpecialType.System_UIntPtr:
			case SpecialType.System_RuntimeArgumentHandle:
			case SpecialType.System_RuntimeFieldHandle:
			case SpecialType.System_RuntimeMethodHandle:
			case SpecialType.System_RuntimeTypeHandle:
				return true;
			}
			return type.IsEnumType();
		}

		// Token: 0x06008062 RID: 32866 RVA: 0x001D0124 File Offset: 0x001CE324
		private static int ParameterSlot(BoundParameter parameter)
		{
			ParameterSymbol parameterSymbol = parameter.ParameterSymbol;
			int num = parameterSymbol.Ordinal;
			if (!parameterSymbol.ContainingSymbol.IsStatic)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06008063 RID: 32867 RVA: 0x001D0150 File Offset: 0x001CE350
		private void EmitLocalLoad(BoundLocal local, bool used)
		{
			if (this.IsStackLocal(local.LocalSymbol))
			{
				this.EmitPopIfUnused(used);
			}
			else
			{
				if (!used)
				{
					return;
				}
				LocalDefinition local2 = this.GetLocal(local);
				this._builder.EmitLocalLoad(local2);
			}
			if (used && local.LocalSymbol.RefKind != RefKind.None)
			{
				this.EmitLoadIndirect(local.LocalSymbol.Type, local.Syntax);
			}
		}

		// Token: 0x06008064 RID: 32868 RVA: 0x001D01B8 File Offset: 0x001CE3B8
		private void EmitParameterLoad(BoundParameter parameter)
		{
			int num = CodeGenerator.ParameterSlot(parameter);
			this._builder.EmitLoadArgumentOpcode(num);
			if (parameter.ParameterSymbol.RefKind != RefKind.None)
			{
				TypeSymbol type = parameter.ParameterSymbol.Type;
				this.EmitLoadIndirect(type, parameter.Syntax);
			}
		}

		// Token: 0x06008065 RID: 32869 RVA: 0x001D0200 File Offset: 0x001CE400
		private void EmitLoadIndirect(TypeSymbol type, SyntaxNode syntaxNode)
		{
			if (type.IsEnumType())
			{
				type = ((NamedTypeSymbol)type).EnumUnderlyingType;
			}
			switch (type.PrimitiveTypeCode)
			{
			case Microsoft.Cci.PrimitiveTypeCode.Boolean:
			case Microsoft.Cci.PrimitiveTypeCode.UInt8:
				this._builder.EmitOpCode(ILOpCode.Ldind_u1);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Char:
			case Microsoft.Cci.PrimitiveTypeCode.UInt16:
				this._builder.EmitOpCode(ILOpCode.Ldind_u2);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Int8:
				this._builder.EmitOpCode(ILOpCode.Ldind_i1);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Float32:
				this._builder.EmitOpCode(ILOpCode.Ldind_r4);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Float64:
				this._builder.EmitOpCode(ILOpCode.Ldind_r8);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Int16:
				this._builder.EmitOpCode(ILOpCode.Ldind_i2);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Int32:
				this._builder.EmitOpCode(ILOpCode.Ldind_i4);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Int64:
			case Microsoft.Cci.PrimitiveTypeCode.UInt64:
				this._builder.EmitOpCode(ILOpCode.Ldind_i8);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.IntPtr:
			case Microsoft.Cci.PrimitiveTypeCode.Pointer:
			case Microsoft.Cci.PrimitiveTypeCode.UIntPtr:
			case Microsoft.Cci.PrimitiveTypeCode.FunctionPointer:
				this._builder.EmitOpCode(ILOpCode.Ldind_i);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.UInt32:
				this._builder.EmitOpCode(ILOpCode.Ldind_u4);
				return;
			}
			if (type.IsVerifierReference())
			{
				this._builder.EmitOpCode(ILOpCode.Ldind_ref);
				return;
			}
			this._builder.EmitOpCode(ILOpCode.Ldobj);
			this.EmitSymbolToken(type, syntaxNode);
		}

		// Token: 0x06008066 RID: 32870 RVA: 0x001D033C File Offset: 0x001CE53C
		private bool CanUseCallOnRefTypeReceiver(BoundExpression receiver)
		{
			if (receiver.Type.IsTypeParameter())
			{
				return false;
			}
			ConstantValue constantValue = receiver.ConstantValue;
			if (constantValue != null)
			{
				return !constantValue.IsNull;
			}
			BoundKind kind = receiver.Kind;
			if (kind <= BoundKind.Local)
			{
				if (kind <= BoundKind.TypeOfOperator)
				{
					if (kind == BoundKind.AssignmentOperator)
					{
						BoundExpression right = ((BoundAssignmentOperator)receiver).Right;
						return this.CanUseCallOnRefTypeReceiver(right);
					}
					if (kind == BoundKind.TypeOfOperator)
					{
						return true;
					}
				}
				else
				{
					if (kind == BoundKind.Conversion)
					{
						BoundConversion boundConversion = (BoundConversion)receiver;
						ConversionKind conversionKind = boundConversion.ConversionKind;
						if (conversionKind <= ConversionKind.Boxing)
						{
							if (conversionKind != ConversionKind.ImplicitReference)
							{
								if (conversionKind != ConversionKind.Boxing)
								{
									return false;
								}
								return true;
							}
						}
						else
						{
							if (conversionKind - ConversionKind.AnonymousFunction <= 1)
							{
								return true;
							}
							if (conversionKind != ConversionKind.ExplicitReference)
							{
								return false;
							}
						}
						return this.CanUseCallOnRefTypeReceiver(boundConversion.Operand);
					}
					if (kind == BoundKind.ThisReference)
					{
						return true;
					}
					if (kind == BoundKind.Local)
					{
						return ((BoundLocal)receiver).LocalSymbol.SynthesizedKind == SynthesizedLocalKind.FrameCache;
					}
				}
			}
			else if (kind <= BoundKind.ObjectCreationExpression)
			{
				if (kind == BoundKind.Sequence)
				{
					BoundExpression value = ((BoundSequence)receiver).Value;
					return this.CanUseCallOnRefTypeReceiver(value);
				}
				if (kind == BoundKind.ConditionalReceiver)
				{
					return true;
				}
				if (kind == BoundKind.ObjectCreationExpression)
				{
					return true;
				}
			}
			else
			{
				if (kind == BoundKind.DelegateCreationExpression)
				{
					return true;
				}
				if (kind == BoundKind.ArrayCreation)
				{
					return true;
				}
				if (kind == BoundKind.FieldAccess)
				{
					return ((BoundFieldAccess)receiver).FieldSymbol.IsCapturedFrame;
				}
			}
			return false;
		}

		// Token: 0x06008067 RID: 32871 RVA: 0x001D04AC File Offset: 0x001CE6AC
		private bool IsThisReceiver(BoundExpression receiver)
		{
			BoundKind kind = receiver.Kind;
			if (kind == BoundKind.ThisReference)
			{
				return true;
			}
			if (kind != BoundKind.Sequence)
			{
				return false;
			}
			BoundExpression value = ((BoundSequence)receiver).Value;
			return this.IsThisReceiver(value);
		}

		// Token: 0x06008068 RID: 32872 RVA: 0x001D04E8 File Offset: 0x001CE6E8
		private void EmitCallExpression(BoundCall call, CodeGenerator.UseKind useKind)
		{
			MethodSymbol method = call.Method;
			BoundExpression receiverOpt = call.ReceiverOpt;
			LocalDefinition localDefinition = null;
			if (method.IsDefaultValueTypeConstructor())
			{
				localDefinition = this.EmitReceiverRef(receiverOpt, Binder.AddressKind.Writeable);
				this._builder.EmitOpCode(ILOpCode.Initobj);
				this.EmitSymbolToken(method.ContainingType, call.Syntax);
				this.FreeOptTemp(localDefinition);
				return;
			}
			ImmutableArray<BoundExpression> arguments = call.Arguments;
			CodeGenerator.CallKind callKind;
			if (!method.RequiresInstanceReceiver)
			{
				callKind = CodeGenerator.CallKind.Call;
			}
			else
			{
				TypeSymbol type = receiverOpt.Type;
				if (type.IsVerifierReference())
				{
					this.EmitExpression(receiverOpt, true);
					if (receiverOpt.SuppressVirtualCalls || (!method.IsMetadataVirtual(false) && this.CanUseCallOnRefTypeReceiver(receiverOpt)))
					{
						callKind = CodeGenerator.CallKind.Call;
					}
					else
					{
						callKind = CodeGenerator.CallKind.CallVirt;
					}
				}
				else if (type.IsVerifierValue())
				{
					NamedTypeSymbol containingType = method.ContainingType;
					if (containingType.IsVerifierValue())
					{
						Binder.AddressKind addressKind = (this.IsReadOnlyCall(method, containingType) ? Binder.AddressKind.ReadOnly : Binder.AddressKind.Writeable);
						if (CodeGenerator.MayUseCallForStructMethod(method))
						{
							localDefinition = this.EmitReceiverRef(receiverOpt, addressKind);
							callKind = CodeGenerator.CallKind.Call;
						}
						else
						{
							localDefinition = this.EmitReceiverRef(receiverOpt, addressKind);
							callKind = CodeGenerator.CallKind.ConstrainedCallVirt;
						}
					}
					else if (method.IsMetadataVirtual(false))
					{
						localDefinition = this.EmitReceiverRef(receiverOpt, Binder.AddressKind.ReadOnly);
						callKind = CodeGenerator.CallKind.ConstrainedCallVirt;
					}
					else
					{
						this.EmitExpression(receiverOpt, true);
						this.EmitBox(type, receiverOpt.Syntax);
						callKind = CodeGenerator.CallKind.Call;
					}
				}
				else
				{
					callKind = ((type.IsReferenceType && !this.IsRef(receiverOpt)) ? CodeGenerator.CallKind.CallVirt : CodeGenerator.CallKind.ConstrainedCallVirt);
					localDefinition = this.EmitReceiverRef(receiverOpt, (callKind == CodeGenerator.CallKind.ConstrainedCallVirt) ? Binder.AddressKind.Constrained : Binder.AddressKind.Writeable);
				}
			}
			MethodSymbol methodSymbol = method;
			if (method.IsOverride && callKind != CodeGenerator.CallKind.Call)
			{
				methodSymbol = method.GetConstructedLeastOverriddenMethod(this._method.ContainingType, true);
			}
			if (callKind == CodeGenerator.CallKind.ConstrainedCallVirt && methodSymbol.ContainingType.IsValueType)
			{
				callKind = CodeGenerator.CallKind.Call;
			}
			if (callKind == CodeGenerator.CallKind.CallVirt)
			{
				if (this.IsThisReceiver(receiverOpt) && methodSymbol.ContainingType.IsSealed && methodSymbol.ContainingModule == this._method.ContainingModule)
				{
					callKind = CodeGenerator.CallKind.Call;
				}
				else if (methodSymbol.IsMetadataFinal && this.CanUseCallOnRefTypeReceiver(receiverOpt))
				{
					callKind = CodeGenerator.CallKind.Call;
				}
			}
			this.EmitArguments(arguments, method.Parameters, call.ArgumentRefKindsOpt);
			int callStackBehavior = CodeGenerator.GetCallStackBehavior(call.Method, call.Arguments);
			switch (callKind)
			{
			case CodeGenerator.CallKind.Call:
				this._builder.EmitOpCode(ILOpCode.Call, callStackBehavior);
				break;
			case CodeGenerator.CallKind.CallVirt:
				this._builder.EmitOpCode(ILOpCode.Callvirt, callStackBehavior);
				break;
			case CodeGenerator.CallKind.ConstrainedCallVirt:
				this._builder.EmitOpCode(ILOpCode.Constrained);
				this.EmitSymbolToken(receiverOpt.Type, receiverOpt.Syntax);
				this._builder.EmitOpCode(ILOpCode.Callvirt, callStackBehavior);
				break;
			}
			this.EmitSymbolToken(methodSymbol, call.Syntax, methodSymbol.IsVararg ? ((BoundArgListOperator)call.Arguments[call.Arguments.Length - 1]) : null, false);
			this.EmitCallCleanup(call.Syntax, useKind, method);
			this.FreeOptTemp(localDefinition);
		}

		// Token: 0x06008069 RID: 32873 RVA: 0x001D07AC File Offset: 0x001CE9AC
		private bool IsReadOnlyCall(MethodSymbol method, NamedTypeSymbol methodContainingType)
		{
			if (method.IsEffectivelyReadOnly && method.MethodKind != MethodKind.Constructor)
			{
				return true;
			}
			if (methodContainingType.IsNullableType())
			{
				MethodSymbol originalDefinition = method.OriginalDefinition;
				if (originalDefinition == this._module.Compilation.GetSpecialTypeMember(SpecialMember.System_Nullable_T_GetValueOrDefault) || originalDefinition == this._module.Compilation.GetSpecialTypeMember(SpecialMember.System_Nullable_T_get_Value) || originalDefinition == this._module.Compilation.GetSpecialTypeMember(SpecialMember.System_Nullable_T_get_HasValue))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600806A RID: 32874 RVA: 0x001D0820 File Offset: 0x001CEA20
		private bool IsRef(BoundExpression receiver)
		{
			BoundKind kind = receiver.Kind;
			if (kind <= BoundKind.Local)
			{
				if (kind == BoundKind.Dup)
				{
					return ((BoundDup)receiver).RefKind > RefKind.None;
				}
				if (kind == BoundKind.FunctionPointerInvocation)
				{
					return ((BoundFunctionPointerInvocation)receiver).FunctionPointer.Signature.RefKind > RefKind.None;
				}
				if (kind == BoundKind.Local)
				{
					return ((BoundLocal)receiver).LocalSymbol.RefKind > RefKind.None;
				}
			}
			else
			{
				if (kind == BoundKind.Parameter)
				{
					return ((BoundParameter)receiver).ParameterSymbol.RefKind > RefKind.None;
				}
				if (kind == BoundKind.Sequence)
				{
					return this.IsRef(((BoundSequence)receiver).Value);
				}
				if (kind == BoundKind.Call)
				{
					return ((BoundCall)receiver).Method.RefKind > RefKind.None;
				}
			}
			return false;
		}

		// Token: 0x0600806B RID: 32875 RVA: 0x001D08DC File Offset: 0x001CEADC
		private static int GetCallStackBehavior(MethodSymbol method, ImmutableArray<BoundExpression> arguments)
		{
			int num = 0;
			if (!method.ReturnsVoid)
			{
				num++;
			}
			if (method.RequiresInstanceReceiver)
			{
				num--;
			}
			if (method.IsVararg)
			{
				int num2 = arguments.Length - 1;
				int length = ((BoundArgListOperator)arguments[num2]).Arguments.Length;
				num -= num2;
				num -= length;
			}
			else
			{
				num -= arguments.Length;
			}
			return num;
		}

		// Token: 0x0600806C RID: 32876 RVA: 0x001D0948 File Offset: 0x001CEB48
		private static int GetObjCreationStackBehavior(BoundObjectCreationExpression objCreation)
		{
			int num = 0;
			num++;
			if (objCreation.Constructor.IsVararg)
			{
				int num2 = objCreation.Arguments.Length - 1;
				int length = ((BoundArgListOperator)objCreation.Arguments[num2]).Arguments.Length;
				num -= num2;
				num -= length;
			}
			else
			{
				num -= objCreation.Arguments.Length;
			}
			return num;
		}

		// Token: 0x0600806D RID: 32877 RVA: 0x001D09B8 File Offset: 0x001CEBB8
		internal static bool MayUseCallForStructMethod(MethodSymbol method)
		{
			if (!method.IsMetadataVirtual(false))
			{
				return true;
			}
			MethodSymbol overriddenMethod = method.OverriddenMethod;
			return overriddenMethod == null || overriddenMethod.IsAbstract || method.ContainingType.SpecialType > SpecialType.None;
		}

		// Token: 0x0600806E RID: 32878 RVA: 0x001D09F2 File Offset: 0x001CEBF2
		private void TreatLongsAsNative(Microsoft.Cci.PrimitiveTypeCode tc)
		{
			if (tc == Microsoft.Cci.PrimitiveTypeCode.Int64)
			{
				this._builder.EmitOpCode(ILOpCode.Conv_ovf_i);
				return;
			}
			if (tc == Microsoft.Cci.PrimitiveTypeCode.UInt64)
			{
				this._builder.EmitOpCode(ILOpCode.Conv_ovf_i_un);
			}
		}

		// Token: 0x0600806F RID: 32879 RVA: 0x001D0A20 File Offset: 0x001CEC20
		private void EmitArrayLength(BoundArrayLength expression, bool used)
		{
			this.EmitExpression(expression.Expression, true);
			this._builder.EmitOpCode(ILOpCode.Ldlen);
			Microsoft.Cci.PrimitiveTypeCode primitiveTypeCode = expression.Type.PrimitiveTypeCode;
			Microsoft.Cci.PrimitiveTypeCode primitiveTypeCode2 = (primitiveTypeCode.IsUnsigned() ? Microsoft.Cci.PrimitiveTypeCode.UIntPtr : Microsoft.Cci.PrimitiveTypeCode.IntPtr);
			this._builder.EmitNumericConversion(primitiveTypeCode2, primitiveTypeCode, false);
			this.EmitPopIfUnused(used);
		}

		// Token: 0x06008070 RID: 32880 RVA: 0x001D0A7C File Offset: 0x001CEC7C
		private void EmitArrayCreationExpression(BoundArrayCreation expression, bool used)
		{
			ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)expression.Type;
			this.EmitArrayIndices(expression.Bounds);
			if (arrayTypeSymbol.IsSZArray)
			{
				this._builder.EmitOpCode(ILOpCode.Newarr);
				this.EmitSymbolToken(arrayTypeSymbol.ElementType, expression.Syntax);
			}
			else
			{
				this._builder.EmitArrayCreation(this._module.Translate(arrayTypeSymbol), expression.Syntax, this._diagnostics);
			}
			if (expression.InitializerOpt != null)
			{
				this.EmitArrayInitializers(arrayTypeSymbol, expression.InitializerOpt);
			}
			this.EmitPopIfUnused(used);
		}

		// Token: 0x06008071 RID: 32881 RVA: 0x001D0B0C File Offset: 0x001CED0C
		private void EmitConvertedStackAllocExpression(BoundConvertedStackAllocExpression expression, bool used)
		{
			this.EmitExpression(expression.Count, used);
			if (used)
			{
				this._sawStackalloc = true;
				this._builder.EmitOpCode(ILOpCode.Localloc);
			}
			BoundArrayInitialization initializerOpt = expression.InitializerOpt;
			if (initializerOpt != null)
			{
				if (used)
				{
					this.EmitStackAllocInitializers(expression.Type, initializerOpt);
					return;
				}
				foreach (BoundExpression boundExpression in initializerOpt.Initializers)
				{
					this.EmitExpression(boundExpression, false);
				}
			}
		}

		// Token: 0x06008072 RID: 32882 RVA: 0x001D0B88 File Offset: 0x001CED88
		private void EmitObjectCreationExpression(BoundObjectCreationExpression expression, bool used)
		{
			MethodSymbol constructor = expression.Constructor;
			if (constructor.IsDefaultValueTypeConstructor())
			{
				this.EmitInitObj(expression.Type, used, expression.Syntax);
				return;
			}
			if (!used && this.ConstructorNotSideEffecting(constructor))
			{
				foreach (BoundExpression boundExpression in expression.Arguments)
				{
					this.EmitExpression(boundExpression, false);
				}
				return;
			}
			if (this._module.Compilation.IsReadOnlySpanType(expression.Type) && expression.Arguments.Length == 1 && this.TryEmitReadonlySpanAsBlobWrapper((NamedTypeSymbol)expression.Type, expression.Arguments[0], used, false))
			{
				return;
			}
			this.EmitArguments(expression.Arguments, constructor.Parameters, expression.ArgumentRefKindsOpt);
			int objCreationStackBehavior = CodeGenerator.GetObjCreationStackBehavior(expression);
			this._builder.EmitOpCode(ILOpCode.Newobj, objCreationStackBehavior);
			this.EmitSymbolToken(constructor, expression.Syntax, constructor.IsVararg ? ((BoundArgListOperator)expression.Arguments[expression.Arguments.Length - 1]) : null, false);
			this.EmitPopIfUnused(used);
		}

		// Token: 0x06008073 RID: 32883 RVA: 0x001D0CB0 File Offset: 0x001CEEB0
		private bool ConstructorNotSideEffecting(MethodSymbol constructor)
		{
			MethodSymbol originalDefinition = constructor.OriginalDefinition;
			CSharpCompilation compilation = this._module.Compilation;
			return originalDefinition == compilation.GetSpecialTypeMember(SpecialMember.System_Nullable_T__ctor) || (originalDefinition.ContainingType.Name == "ValueTuple" && (originalDefinition == compilation.GetWellKnownTypeMember(WellKnownMember.System_ValueTuple_T2__ctor) || originalDefinition == compilation.GetWellKnownTypeMember(WellKnownMember.System_ValueTuple_T3__ctor) || originalDefinition == compilation.GetWellKnownTypeMember(WellKnownMember.System_ValueTuple_T4__ctor) || originalDefinition == compilation.GetWellKnownTypeMember(WellKnownMember.System_ValueTuple_T5__ctor) || originalDefinition == compilation.GetWellKnownTypeMember(WellKnownMember.System_ValueTuple_T6__ctor) || originalDefinition == compilation.GetWellKnownTypeMember(WellKnownMember.System_ValueTuple_T7__ctor) || originalDefinition == compilation.GetWellKnownTypeMember(WellKnownMember.System_ValueTuple_TRest__ctor) || originalDefinition == compilation.GetWellKnownTypeMember(WellKnownMember.System_ValueTuple_T1__ctor)));
		}

		// Token: 0x06008074 RID: 32884 RVA: 0x001D0D9C File Offset: 0x001CEF9C
		private void EmitAssignmentExpression(BoundAssignmentOperator assignmentOperator, CodeGenerator.UseKind useKind)
		{
			if (this.TryEmitAssignmentInPlace(assignmentOperator, useKind > CodeGenerator.UseKind.Unused))
			{
				return;
			}
			bool flag = this.EmitAssignmentPreamble(assignmentOperator);
			this.EmitAssignmentValue(assignmentOperator);
			LocalDefinition localDefinition = this.EmitAssignmentDuplication(assignmentOperator, useKind, flag);
			this.EmitStore(assignmentOperator);
			this.EmitAssignmentPostfix(assignmentOperator, localDefinition, useKind);
		}

		// Token: 0x06008075 RID: 32885 RVA: 0x001D0DE0 File Offset: 0x001CEFE0
		private bool TryEmitAssignmentInPlace(BoundAssignmentOperator assignmentOperator, bool used)
		{
			if (assignmentOperator.IsRef)
			{
				return false;
			}
			BoundExpression left = assignmentOperator.Left;
			if (used && !CodeGenerator.TargetIsNotOnHeap(left))
			{
				return false;
			}
			if (!this.SafeToGetWriteableReference(left))
			{
				return false;
			}
			BoundExpression right = assignmentOperator.Right;
			TypeSymbol type = right.Type;
			if (!type.IsTypeParameter() && (type.IsReferenceType || (right.ConstantValue != null && type.SpecialType != SpecialType.System_Decimal)))
			{
				return false;
			}
			if (right.IsDefaultValue())
			{
				this.InPlaceInit(left, used);
				return true;
			}
			BoundObjectCreationExpression boundObjectCreationExpression = right as BoundObjectCreationExpression;
			if (boundObjectCreationExpression != null)
			{
				if (boundObjectCreationExpression.Arguments.Length > 0 && boundObjectCreationExpression.Arguments[0].Kind == BoundKind.ConvertedStackAllocExpression)
				{
					return false;
				}
				if (this.PartialCtorResultCannotEscape(left))
				{
					MethodSymbol constructor = boundObjectCreationExpression.Constructor;
					if (constructor.Parameters.All<ParameterSymbol>((ParameterSymbol p) => p.RefKind == RefKind.None) && !constructor.IsVararg)
					{
						this.InPlaceCtorCall(left, boundObjectCreationExpression, used);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06008076 RID: 32886 RVA: 0x001D0EF0 File Offset: 0x001CF0F0
		private bool SafeToGetWriteableReference(BoundExpression left)
		{
			if (!this.HasHome(left, Binder.AddressKind.Writeable))
			{
				return false;
			}
			if (left.Kind == BoundKind.ArrayAccess && left.Type.TypeKind == TypeKind.TypeParameter && !left.Type.IsValueType)
			{
				return false;
			}
			if (left.Kind == BoundKind.FieldAccess)
			{
				BoundFieldAccess boundFieldAccess = (BoundFieldAccess)left;
				if (boundFieldAccess.FieldSymbol.IsVolatile || DiagnosticsPass.IsNonAgileFieldAccess(boundFieldAccess, this._module.Compilation))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06008077 RID: 32887 RVA: 0x001D0F68 File Offset: 0x001CF168
		private void InPlaceInit(BoundExpression target, bool used)
		{
			this.EmitAddress(target, Binder.AddressKind.Writeable);
			this._builder.EmitOpCode(ILOpCode.Initobj);
			this.EmitSymbolToken(target.Type, target.Syntax);
			if (used)
			{
				this.EmitExpression(target, used);
			}
		}

		// Token: 0x06008078 RID: 32888 RVA: 0x001D0FA0 File Offset: 0x001CF1A0
		private void InPlaceCtorCall(BoundExpression target, BoundObjectCreationExpression objCreation, bool used)
		{
			this.EmitAddress(target, Binder.AddressKind.Writeable);
			if (this._module.Compilation.IsReadOnlySpanType(objCreation.Type) && objCreation.Arguments.Length == 1 && this.TryEmitReadonlySpanAsBlobWrapper((NamedTypeSymbol)objCreation.Type, objCreation.Arguments[0], used, true))
			{
				if (used)
				{
					this.EmitExpression(target, true);
				}
				return;
			}
			MethodSymbol constructor = objCreation.Constructor;
			this.EmitArguments(objCreation.Arguments, constructor.Parameters, objCreation.ArgumentRefKindsOpt);
			int num = CodeGenerator.GetObjCreationStackBehavior(objCreation) - 2;
			this._builder.EmitOpCode(ILOpCode.Call, num);
			this.EmitSymbolToken(constructor, objCreation.Syntax, constructor.IsVararg ? ((BoundArgListOperator)objCreation.Arguments[objCreation.Arguments.Length - 1]) : null, false);
			if (used)
			{
				this.EmitExpression(target, true);
			}
		}

		// Token: 0x06008079 RID: 32889 RVA: 0x001D108C File Offset: 0x001CF28C
		private bool PartialCtorResultCannotEscape(BoundExpression left)
		{
			if (!CodeGenerator.TargetIsNotOnHeap(left))
			{
				return false;
			}
			if (this._tryNestingLevel != 0)
			{
				BoundLocal boundLocal = left as BoundLocal;
				return boundLocal != null && !this._builder.PossiblyDefinedOutsideOfTry(this.GetLocal(boundLocal));
			}
			return true;
		}

		// Token: 0x0600807A RID: 32890 RVA: 0x001D10D0 File Offset: 0x001CF2D0
		private static bool TargetIsNotOnHeap(BoundExpression left)
		{
			BoundKind kind = left.Kind;
			if (kind != BoundKind.Local)
			{
				return kind == BoundKind.Parameter && ((BoundParameter)left).ParameterSymbol.RefKind == RefKind.None;
			}
			return ((BoundLocal)left).LocalSymbol.RefKind == RefKind.None;
		}

		// Token: 0x0600807B RID: 32891 RVA: 0x001D1118 File Offset: 0x001CF318
		private bool EmitAssignmentPreamble(BoundAssignmentOperator assignmentOperator)
		{
			BoundExpression left = assignmentOperator.Left;
			bool flag = false;
			BoundKind kind = left.Kind;
			if (kind > BoundKind.ConditionalOperator)
			{
				if (kind <= BoundKind.Parameter)
				{
					if (kind == BoundKind.ArrayAccess)
					{
						BoundArrayAccess boundArrayAccess = (BoundArrayAccess)left;
						this.EmitExpression(boundArrayAccess.Expression, true);
						this.EmitArrayIndices(boundArrayAccess.Indices);
						return true;
					}
					if (kind - BoundKind.InstrumentationPayloadRoot <= 1)
					{
						return flag;
					}
					switch (kind)
					{
					case BoundKind.ThisReference:
					{
						BoundThisReference boundThisReference = (BoundThisReference)left;
						this.EmitAddress(boundThisReference, Binder.AddressKind.Writeable);
						return true;
					}
					case BoundKind.PreviousSubmissionReference:
						break;
					case BoundKind.HostObjectMemberReference:
					case BoundKind.BaseReference:
					case BoundKind.RangeVariable:
						goto IL_02E0;
					case BoundKind.Local:
					{
						BoundLocal boundLocal = (BoundLocal)left;
						if (boundLocal.LocalSymbol.RefKind != RefKind.None && !assignmentOperator.IsRef)
						{
							if (!this.IsStackLocal(boundLocal.LocalSymbol))
							{
								LocalDefinition local = this.GetLocal(boundLocal);
								this._builder.EmitLocalLoad(local);
							}
							return true;
						}
						return flag;
					}
					case BoundKind.PseudoVariable:
						this.EmitPseudoVariableAddress((BoundPseudoVariable)left);
						return true;
					case BoundKind.Parameter:
					{
						BoundParameter boundParameter = (BoundParameter)left;
						if (boundParameter.ParameterSymbol.RefKind != RefKind.None && !assignmentOperator.IsRef)
						{
							this._builder.EmitLoadArgumentOpcode(CodeGenerator.ParameterSlot(boundParameter));
							return true;
						}
						return flag;
					}
					default:
						goto IL_02E0;
					}
				}
				else
				{
					if (kind == BoundKind.Sequence)
					{
						BoundSequence boundSequence = (BoundSequence)left;
						this.DefineAndRecordLocals(boundSequence);
						this.EmitSideEffects(boundSequence);
						flag = this.EmitAssignmentPreamble(assignmentOperator.Update(boundSequence.Value, assignmentOperator.Right, assignmentOperator.IsRef, assignmentOperator.Type));
						this.CloseScopeAndKeepLocals(boundSequence);
						return flag;
					}
					if (kind == BoundKind.Call)
					{
						BoundCall boundCall = (BoundCall)left;
						this.EmitCallExpression(boundCall, CodeGenerator.UseKind.UsedAsAddress);
						return true;
					}
					switch (kind)
					{
					case BoundKind.FieldAccess:
					{
						BoundFieldAccess boundFieldAccess = (BoundFieldAccess)left;
						if (!boundFieldAccess.FieldSymbol.IsStatic)
						{
							this.EmitReceiverRef(boundFieldAccess.ReceiverOpt, Binder.AddressKind.Writeable);
							return true;
						}
						return flag;
					}
					case BoundKind.HoistedFieldAccess:
					case BoundKind.EventAccess:
						goto IL_02E0;
					case BoundKind.PropertyAccess:
					case BoundKind.IndexerAccess:
						break;
					default:
						goto IL_02E0;
					}
				}
				throw ExceptionUtilities.UnexpectedValue(left.Kind);
			}
			if (kind <= BoundKind.FunctionPointerInvocation)
			{
				if (kind == BoundKind.Dup)
				{
					BoundDup boundDup = (BoundDup)left;
					this.EmitAddress(boundDup, Binder.AddressKind.Writeable);
					return true;
				}
				if (kind == BoundKind.PointerIndirectionOperator)
				{
					BoundPointerIndirectionOperator boundPointerIndirectionOperator = (BoundPointerIndirectionOperator)left;
					this.EmitExpression(boundPointerIndirectionOperator.Operand, true);
					return true;
				}
				if (kind == BoundKind.FunctionPointerInvocation)
				{
					BoundFunctionPointerInvocation boundFunctionPointerInvocation = (BoundFunctionPointerInvocation)left;
					this.EmitCalli(boundFunctionPointerInvocation, CodeGenerator.UseKind.UsedAsAddress);
					return true;
				}
			}
			else
			{
				if (kind == BoundKind.RefValueOperator)
				{
					this.EmitRefValueAddress((BoundRefValueOperator)left);
					return flag;
				}
				if (kind != BoundKind.AssignmentOperator)
				{
					if (kind == BoundKind.ConditionalOperator)
					{
						BoundConditionalOperator boundConditionalOperator = (BoundConditionalOperator)left;
						this.EmitAddress(boundConditionalOperator, Binder.AddressKind.Writeable);
						return true;
					}
				}
				else
				{
					BoundAssignmentOperator boundAssignmentOperator = (BoundAssignmentOperator)left;
					if (boundAssignmentOperator.IsRef)
					{
						this.EmitAssignmentExpression(boundAssignmentOperator, CodeGenerator.UseKind.UsedAsAddress);
						return flag;
					}
				}
			}
			IL_02E0:
			throw ExceptionUtilities.UnexpectedValue(left.Kind);
		}

		// Token: 0x0600807C RID: 32892 RVA: 0x001D1418 File Offset: 0x001CF618
		private void EmitAssignmentValue(BoundAssignmentOperator assignmentOperator)
		{
			if (!assignmentOperator.IsRef)
			{
				this.EmitExpression(assignmentOperator.Right, true);
				return;
			}
			ArrayBuilder<LocalDefinition> expressionTemps = this._expressionTemps;
			int num = ((expressionTemps != null) ? expressionTemps.Count : 0);
			BoundExpression left = assignmentOperator.Left;
			LocalDefinition localDefinition = this.EmitAddress(assignmentOperator.Right, (left.GetRefKind() == RefKind.In) ? Binder.AddressKind.ReadOnlyStrict : Binder.AddressKind.Writeable);
			this.AddExpressionTemp(localDefinition);
			ArrayBuilder<LocalDefinition> expressionTemps2 = this._expressionTemps;
			int num2 = ((expressionTemps2 != null) ? expressionTemps2.Count : 0);
			if (left.Kind == BoundKind.Local && ((BoundLocal)left).LocalSymbol.SynthesizedKind.IsLongLived() && num2 > num)
			{
				this._expressionTemps.Count = num;
			}
		}

		// Token: 0x0600807D RID: 32893 RVA: 0x001D14BC File Offset: 0x001CF6BC
		private LocalDefinition EmitAssignmentDuplication(BoundAssignmentOperator assignmentOperator, CodeGenerator.UseKind useKind, bool lhsUsesStack)
		{
			LocalDefinition localDefinition = null;
			if (useKind != CodeGenerator.UseKind.Unused)
			{
				this._builder.EmitOpCode(ILOpCode.Dup);
				if (lhsUsesStack)
				{
					localDefinition = this.AllocateTemp(assignmentOperator.Left.Type, assignmentOperator.Left.Syntax, assignmentOperator.IsRef ? LocalSlotConstraints.ByRef : LocalSlotConstraints.None);
					this._builder.EmitLocalStore(localDefinition);
				}
			}
			return localDefinition;
		}

		// Token: 0x0600807E RID: 32894 RVA: 0x001D1514 File Offset: 0x001CF714
		private void EmitStore(BoundAssignmentOperator assignment)
		{
			BoundExpression left = assignment.Left;
			BoundKind kind = left.Kind;
			if (kind <= BoundKind.ConditionalOperator)
			{
				if (kind <= BoundKind.FunctionPointerInvocation)
				{
					if (kind == BoundKind.Dup)
					{
						this.EmitIndirectStore(left.Type, left.Syntax);
						return;
					}
					if (kind != BoundKind.PointerIndirectionOperator)
					{
						if (kind != BoundKind.FunctionPointerInvocation)
						{
							goto IL_024F;
						}
						this.EmitIndirectStore(left.Type, left.Syntax);
						return;
					}
				}
				else if (kind != BoundKind.RefValueOperator)
				{
					if (kind != BoundKind.AssignmentOperator)
					{
						if (kind != BoundKind.ConditionalOperator)
						{
							goto IL_024F;
						}
						this.EmitIndirectStore(left.Type, left.Syntax);
						return;
					}
					else
					{
						BoundAssignmentOperator boundAssignmentOperator = (BoundAssignmentOperator)left;
						if (boundAssignmentOperator.IsRef)
						{
							this.EmitIndirectStore(boundAssignmentOperator.Type, left.Syntax);
							return;
						}
						goto IL_024F;
					}
				}
			}
			else if (kind <= BoundKind.ModuleVersionId)
			{
				if (kind == BoundKind.ArrayAccess)
				{
					ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)((BoundArrayAccess)left).Expression.Type;
					this.EmitArrayElementStore(arrayTypeSymbol, left.Syntax);
					return;
				}
				if (kind == BoundKind.InstrumentationPayloadRoot)
				{
					this.EmitInstrumentationPayloadRootStore((BoundInstrumentationPayloadRoot)left);
					return;
				}
				if (kind != BoundKind.ModuleVersionId)
				{
					goto IL_024F;
				}
				this.EmitModuleVersionIdStore((BoundModuleVersionId)left);
				return;
			}
			else if (kind <= BoundKind.Sequence)
			{
				switch (kind)
				{
				case BoundKind.ThisReference:
					this.EmitThisStore((BoundThisReference)left);
					return;
				case BoundKind.PreviousSubmissionReference:
				case BoundKind.HostObjectMemberReference:
				case BoundKind.BaseReference:
				case BoundKind.RangeVariable:
					goto IL_024F;
				case BoundKind.Local:
				{
					BoundLocal boundLocal = (BoundLocal)left;
					if (boundLocal.LocalSymbol.RefKind != RefKind.None && !assignment.IsRef)
					{
						this.EmitIndirectStore(boundLocal.LocalSymbol.Type, boundLocal.Syntax);
						return;
					}
					if (!this.IsStackLocal(boundLocal.LocalSymbol))
					{
						this._builder.EmitLocalStore(this.GetLocal(boundLocal));
						return;
					}
					return;
				}
				case BoundKind.PseudoVariable:
					break;
				case BoundKind.Parameter:
					this.EmitParameterStore((BoundParameter)left, assignment.IsRef);
					return;
				default:
				{
					if (kind != BoundKind.Sequence)
					{
						goto IL_024F;
					}
					BoundSequence boundSequence = (BoundSequence)left;
					this.EmitStore(assignment.Update(boundSequence.Value, assignment.Right, assignment.IsRef, assignment.Type));
					return;
				}
				}
			}
			else
			{
				if (kind == BoundKind.Call)
				{
					this.EmitIndirectStore(left.Type, left.Syntax);
					return;
				}
				if (kind == BoundKind.FieldAccess)
				{
					this.EmitFieldStore((BoundFieldAccess)left);
					return;
				}
				goto IL_024F;
			}
			this.EmitIndirectStore(left.Type, left.Syntax);
			return;
			IL_024F:
			throw ExceptionUtilities.UnexpectedValue(left.Kind);
		}

		// Token: 0x0600807F RID: 32895 RVA: 0x001D1784 File Offset: 0x001CF984
		private void EmitAssignmentPostfix(BoundAssignmentOperator assignment, LocalDefinition temp, CodeGenerator.UseKind useKind)
		{
			if (temp != null)
			{
				if (useKind == CodeGenerator.UseKind.UsedAsAddress)
				{
					this._builder.EmitLocalAddress(temp);
				}
				else
				{
					this._builder.EmitLocalLoad(temp);
				}
				this.FreeTemp(temp);
			}
			if (useKind == CodeGenerator.UseKind.UsedAsValue && assignment.IsRef)
			{
				this.EmitLoadIndirect(assignment.Type, assignment.Syntax);
			}
		}

		// Token: 0x06008080 RID: 32896 RVA: 0x001D17D7 File Offset: 0x001CF9D7
		private void EmitThisStore(BoundThisReference thisRef)
		{
			this._builder.EmitOpCode(ILOpCode.Stobj);
			this.EmitSymbolToken(thisRef.Type, thisRef.Syntax);
		}

		// Token: 0x06008081 RID: 32897 RVA: 0x001D17FB File Offset: 0x001CF9FB
		private void EmitArrayElementStore(ArrayTypeSymbol arrayType, SyntaxNode syntaxNode)
		{
			if (arrayType.IsSZArray)
			{
				this.EmitVectorElementStore(arrayType, syntaxNode);
				return;
			}
			this._builder.EmitArrayElementStore(this._module.Translate(arrayType), syntaxNode, this._diagnostics);
		}

		// Token: 0x06008082 RID: 32898 RVA: 0x001D182C File Offset: 0x001CFA2C
		private void EmitVectorElementStore(ArrayTypeSymbol arrayType, SyntaxNode syntaxNode)
		{
			TypeSymbol typeSymbol = arrayType.ElementType;
			if (typeSymbol.IsEnumType())
			{
				typeSymbol = ((NamedTypeSymbol)typeSymbol).EnumUnderlyingType;
			}
			switch (typeSymbol.PrimitiveTypeCode)
			{
			case Microsoft.Cci.PrimitiveTypeCode.Boolean:
			case Microsoft.Cci.PrimitiveTypeCode.Int8:
			case Microsoft.Cci.PrimitiveTypeCode.UInt8:
				this._builder.EmitOpCode(ILOpCode.Stelem_i1);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Char:
			case Microsoft.Cci.PrimitiveTypeCode.Int16:
			case Microsoft.Cci.PrimitiveTypeCode.UInt16:
				this._builder.EmitOpCode(ILOpCode.Stelem_i2);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Float32:
				this._builder.EmitOpCode(ILOpCode.Stelem_r4);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Float64:
				this._builder.EmitOpCode(ILOpCode.Stelem_r8);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Int32:
			case Microsoft.Cci.PrimitiveTypeCode.UInt32:
				this._builder.EmitOpCode(ILOpCode.Stelem_i4);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Int64:
			case Microsoft.Cci.PrimitiveTypeCode.UInt64:
				this._builder.EmitOpCode(ILOpCode.Stelem_i8);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.IntPtr:
			case Microsoft.Cci.PrimitiveTypeCode.Pointer:
			case Microsoft.Cci.PrimitiveTypeCode.UIntPtr:
			case Microsoft.Cci.PrimitiveTypeCode.FunctionPointer:
				this._builder.EmitOpCode(ILOpCode.Stelem_i);
				return;
			}
			if (typeSymbol.IsVerifierReference())
			{
				this._builder.EmitOpCode(ILOpCode.Stelem_ref);
				return;
			}
			this._builder.EmitOpCode(ILOpCode.Stelem);
			this.EmitSymbolToken(typeSymbol, syntaxNode);
		}

		// Token: 0x06008083 RID: 32899 RVA: 0x001D195C File Offset: 0x001CFB5C
		private void EmitFieldStore(BoundFieldAccess fieldAccess)
		{
			FieldSymbol fieldSymbol = fieldAccess.FieldSymbol;
			if (fieldSymbol.IsVolatile)
			{
				this._builder.EmitOpCode(ILOpCode.Volatile);
			}
			this._builder.EmitOpCode(fieldSymbol.IsStatic ? ILOpCode.Stsfld : ILOpCode.Stfld);
			this.EmitSymbolToken(fieldSymbol, fieldAccess.Syntax);
		}

		// Token: 0x06008084 RID: 32900 RVA: 0x001D19B4 File Offset: 0x001CFBB4
		private void EmitParameterStore(BoundParameter parameter, bool refAssign)
		{
			int num = CodeGenerator.ParameterSlot(parameter);
			if (parameter.ParameterSymbol.RefKind != RefKind.None && !refAssign)
			{
				this.EmitIndirectStore(parameter.ParameterSymbol.Type, parameter.Syntax);
				return;
			}
			this._builder.EmitStoreArgumentOpcode(num);
		}

		// Token: 0x06008085 RID: 32901 RVA: 0x001D19FC File Offset: 0x001CFBFC
		private void EmitIndirectStore(TypeSymbol type, SyntaxNode syntaxNode)
		{
			if (type.IsEnumType())
			{
				type = ((NamedTypeSymbol)type).EnumUnderlyingType;
			}
			switch (type.PrimitiveTypeCode)
			{
			case Microsoft.Cci.PrimitiveTypeCode.Boolean:
			case Microsoft.Cci.PrimitiveTypeCode.Int8:
			case Microsoft.Cci.PrimitiveTypeCode.UInt8:
				this._builder.EmitOpCode(ILOpCode.Stind_i1);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Char:
			case Microsoft.Cci.PrimitiveTypeCode.Int16:
			case Microsoft.Cci.PrimitiveTypeCode.UInt16:
				this._builder.EmitOpCode(ILOpCode.Stind_i2);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Float32:
				this._builder.EmitOpCode(ILOpCode.Stind_r4);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Float64:
				this._builder.EmitOpCode(ILOpCode.Stind_r8);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Int32:
			case Microsoft.Cci.PrimitiveTypeCode.UInt32:
				this._builder.EmitOpCode(ILOpCode.Stind_i4);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Int64:
			case Microsoft.Cci.PrimitiveTypeCode.UInt64:
				this._builder.EmitOpCode(ILOpCode.Stind_i8);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.IntPtr:
			case Microsoft.Cci.PrimitiveTypeCode.Pointer:
			case Microsoft.Cci.PrimitiveTypeCode.UIntPtr:
			case Microsoft.Cci.PrimitiveTypeCode.FunctionPointer:
				this._builder.EmitOpCode(ILOpCode.Stind_i);
				return;
			}
			if (type.IsVerifierReference())
			{
				this._builder.EmitOpCode(ILOpCode.Stind_ref);
				return;
			}
			this._builder.EmitOpCode(ILOpCode.Stobj);
			this.EmitSymbolToken(type, syntaxNode);
		}

		// Token: 0x06008086 RID: 32902 RVA: 0x001D1B10 File Offset: 0x001CFD10
		private void EmitPopIfUnused(bool used)
		{
			if (!used)
			{
				this._builder.EmitOpCode(ILOpCode.Pop);
			}
		}

		// Token: 0x06008087 RID: 32903 RVA: 0x001D1B24 File Offset: 0x001CFD24
		private void EmitIsExpression(BoundIsOperator isOp, bool used)
		{
			BoundExpression operand = isOp.Operand;
			this.EmitExpression(operand, used);
			if (used)
			{
				if (!operand.Type.IsVerifierReference())
				{
					this.EmitBox(operand.Type, operand.Syntax);
				}
				this._builder.EmitOpCode(ILOpCode.Isinst);
				this.EmitSymbolToken(isOp.TargetType.Type, isOp.Syntax);
				this._builder.EmitOpCode(ILOpCode.Ldnull);
				this._builder.EmitOpCode(ILOpCode.Cgt_un);
			}
		}

		// Token: 0x06008088 RID: 32904 RVA: 0x001D1BA4 File Offset: 0x001CFDA4
		private void EmitAsExpression(BoundAsOperator asOp, bool used)
		{
			BoundExpression operand = asOp.Operand;
			this.EmitExpression(operand, used);
			if (used)
			{
				TypeSymbol type = operand.Type;
				TypeSymbol type2 = asOp.Type;
				if (type != null && !type.IsVerifierReference())
				{
					this.EmitBox(type, operand.Syntax);
				}
				this._builder.EmitOpCode(ILOpCode.Isinst);
				this.EmitSymbolToken(type2, asOp.Syntax);
				if (!type2.IsVerifierReference())
				{
					this._builder.EmitOpCode(ILOpCode.Unbox_any);
					this.EmitSymbolToken(type2, asOp.Syntax);
				}
			}
		}

		// Token: 0x06008089 RID: 32905 RVA: 0x001D1C28 File Offset: 0x001CFE28
		private void EmitDefaultValue(TypeSymbol type, bool used, SyntaxNode syntaxNode)
		{
			if (used)
			{
				if (!type.IsTypeParameter() && type.SpecialType != SpecialType.System_Decimal)
				{
					ConstantValue defaultValue = type.GetDefaultValue();
					if (defaultValue != null)
					{
						this._builder.EmitConstantValue(defaultValue);
						return;
					}
				}
				if (type.IsPointerOrFunctionPointer() || type.SpecialType == SpecialType.System_UIntPtr)
				{
					this._builder.EmitOpCode(ILOpCode.Ldc_i4_0);
					this._builder.EmitOpCode(ILOpCode.Conv_u);
					return;
				}
				if (type.SpecialType == SpecialType.System_IntPtr)
				{
					this._builder.EmitOpCode(ILOpCode.Ldc_i4_0);
					this._builder.EmitOpCode(ILOpCode.Conv_i);
					return;
				}
				this.EmitInitObj(type, true, syntaxNode);
			}
		}

		// Token: 0x0600808A RID: 32906 RVA: 0x001D1CCB File Offset: 0x001CFECB
		private void EmitDefaultExpression(BoundDefaultExpression expression, bool used)
		{
			this.EmitDefaultValue(expression.Type, used, expression.Syntax);
		}

		// Token: 0x0600808B RID: 32907 RVA: 0x001D1CE0 File Offset: 0x001CFEE0
		private void EmitConstantExpression(TypeSymbol type, ConstantValue constantValue, bool used, SyntaxNode syntaxNode)
		{
			if (used)
			{
				if (type != null && type.TypeKind == TypeKind.TypeParameter && constantValue.IsNull)
				{
					this.EmitInitObj(type, used, syntaxNode);
					return;
				}
				this._builder.EmitConstantValue(constantValue);
			}
		}

		// Token: 0x0600808C RID: 32908 RVA: 0x001D1D14 File Offset: 0x001CFF14
		private void EmitInitObj(TypeSymbol type, bool used, SyntaxNode syntaxNode)
		{
			if (used)
			{
				LocalDefinition localDefinition = this.AllocateTemp(type, syntaxNode, LocalSlotConstraints.None);
				this._builder.EmitLocalAddress(localDefinition);
				this._builder.EmitOpCode(ILOpCode.Initobj);
				this.EmitSymbolToken(type, syntaxNode);
				this._builder.EmitLocalLoad(localDefinition);
				this.FreeTemp(localDefinition);
			}
		}

		// Token: 0x0600808D RID: 32909 RVA: 0x001D1D68 File Offset: 0x001CFF68
		private void EmitGetTypeFromHandle(BoundTypeOf boundTypeOf)
		{
			this._builder.EmitOpCode(ILOpCode.Call, 0);
			MethodSymbol getTypeFromHandle = boundTypeOf.GetTypeFromHandle;
			this.EmitSymbolToken(getTypeFromHandle, boundTypeOf.Syntax, null, false);
		}

		// Token: 0x0600808E RID: 32910 RVA: 0x001D1D9C File Offset: 0x001CFF9C
		private void EmitTypeOfExpression(BoundTypeOfOperator boundTypeOfOperator)
		{
			TypeSymbol type = boundTypeOfOperator.SourceType.Type;
			this._builder.EmitOpCode(ILOpCode.Ldtoken);
			this.EmitSymbolToken(type, boundTypeOfOperator.SourceType.Syntax);
			this.EmitGetTypeFromHandle(boundTypeOfOperator);
		}

		// Token: 0x0600808F RID: 32911 RVA: 0x001D1DE0 File Offset: 0x001CFFE0
		private void EmitSizeOfExpression(BoundSizeOfOperator boundSizeOfOperator)
		{
			TypeSymbol type = boundSizeOfOperator.SourceType.Type;
			this._builder.EmitOpCode(ILOpCode.Sizeof);
			this.EmitSymbolToken(type, boundSizeOfOperator.SourceType.Syntax);
		}

		// Token: 0x06008090 RID: 32912 RVA: 0x001D1E1C File Offset: 0x001D001C
		private void EmitMethodDefIndexExpression(BoundMethodDefIndex node)
		{
			this._builder.EmitOpCode(ILOpCode.Ldtoken);
			MethodSymbol methodSymbol = node.Method.PartialDefinitionPart ?? node.Method;
			this.EmitSymbolToken(methodSymbol, node.Syntax, null, true);
		}

		// Token: 0x06008091 RID: 32913 RVA: 0x001D1E5E File Offset: 0x001D005E
		private void EmitMaximumMethodDefIndexExpression(BoundMaximumMethodDefIndex node)
		{
			this._builder.EmitOpCode(ILOpCode.Ldtoken);
			this._builder.EmitGreatestMethodToken();
		}

		// Token: 0x06008092 RID: 32914 RVA: 0x001D1E7B File Offset: 0x001D007B
		private void EmitModuleVersionIdLoad(BoundModuleVersionId node)
		{
			this._builder.EmitOpCode(ILOpCode.Ldsfld);
			this.EmitModuleVersionIdToken(node);
		}

		// Token: 0x06008093 RID: 32915 RVA: 0x001D1E91 File Offset: 0x001D0091
		private void EmitModuleVersionIdStore(BoundModuleVersionId node)
		{
			this._builder.EmitOpCode(ILOpCode.Stsfld);
			this.EmitModuleVersionIdToken(node);
		}

		// Token: 0x06008094 RID: 32916 RVA: 0x001D1EAC File Offset: 0x001D00AC
		private void EmitModuleVersionIdToken(BoundModuleVersionId node)
		{
			this._builder.EmitToken(this._module.GetModuleVersionId(this._module.Translate(node.Type, node.Syntax, this._diagnostics), node.Syntax, this._diagnostics), node.Syntax, this._diagnostics, false);
		}

		// Token: 0x06008095 RID: 32917 RVA: 0x001D1F05 File Offset: 0x001D0105
		private void EmitModuleVersionIdStringLoad(BoundModuleVersionIdString node)
		{
			this._builder.EmitOpCode(ILOpCode.Ldstr);
			this._builder.EmitModuleVersionIdStringToken();
		}

		// Token: 0x06008096 RID: 32918 RVA: 0x001D1F1F File Offset: 0x001D011F
		private void EmitInstrumentationPayloadRootLoad(BoundInstrumentationPayloadRoot node)
		{
			this._builder.EmitOpCode(ILOpCode.Ldsfld);
			this.EmitInstrumentationPayloadRootToken(node);
		}

		// Token: 0x06008097 RID: 32919 RVA: 0x001D1F35 File Offset: 0x001D0135
		private void EmitInstrumentationPayloadRootStore(BoundInstrumentationPayloadRoot node)
		{
			this._builder.EmitOpCode(ILOpCode.Stsfld);
			this.EmitInstrumentationPayloadRootToken(node);
		}

		// Token: 0x06008098 RID: 32920 RVA: 0x001D1F50 File Offset: 0x001D0150
		private void EmitInstrumentationPayloadRootToken(BoundInstrumentationPayloadRoot node)
		{
			this._builder.EmitToken(this._module.GetInstrumentationPayloadRoot(node.AnalysisKind, this._module.Translate(node.Type, node.Syntax, this._diagnostics), node.Syntax, this._diagnostics), node.Syntax, this._diagnostics, false);
		}

		// Token: 0x06008099 RID: 32921 RVA: 0x001D1FAF File Offset: 0x001D01AF
		private void EmitSourceDocumentIndex(BoundSourceDocumentIndex node)
		{
			this._builder.EmitOpCode(ILOpCode.Ldtoken);
			this._builder.EmitSourceDocumentIndexToken(node.Document);
		}

		// Token: 0x0600809A RID: 32922 RVA: 0x001D1FD4 File Offset: 0x001D01D4
		private void EmitMethodInfoExpression(BoundMethodInfo node)
		{
			this._builder.EmitOpCode(ILOpCode.Ldtoken);
			this.EmitSymbolToken(node.Method, node.Syntax, null, false);
			MethodSymbol getMethodFromHandle = node.GetMethodFromHandle;
			if (getMethodFromHandle.ParameterCount == 1)
			{
				this._builder.EmitOpCode(ILOpCode.Call, 0);
			}
			else
			{
				this._builder.EmitOpCode(ILOpCode.Ldtoken);
				this.EmitSymbolToken(node.Method.ContainingType, node.Syntax);
				this._builder.EmitOpCode(ILOpCode.Call, -1);
			}
			this.EmitSymbolToken(getMethodFromHandle, node.Syntax, null, false);
			if (!TypeSymbol.Equals(node.Type, getMethodFromHandle.ReturnType, TypeCompareKind.ConsiderEverything))
			{
				this._builder.EmitOpCode(ILOpCode.Castclass);
				this.EmitSymbolToken(node.Type, node.Syntax);
			}
		}

		// Token: 0x0600809B RID: 32923 RVA: 0x001D209C File Offset: 0x001D029C
		private void EmitFieldInfoExpression(BoundFieldInfo node)
		{
			this._builder.EmitOpCode(ILOpCode.Ldtoken);
			this.EmitSymbolToken(node.Field, node.Syntax);
			MethodSymbol getFieldFromHandle = node.GetFieldFromHandle;
			if (getFieldFromHandle.ParameterCount == 1)
			{
				this._builder.EmitOpCode(ILOpCode.Call, 0);
			}
			else
			{
				this._builder.EmitOpCode(ILOpCode.Ldtoken);
				this.EmitSymbolToken(node.Field.ContainingType, node.Syntax);
				this._builder.EmitOpCode(ILOpCode.Call, -1);
			}
			this.EmitSymbolToken(getFieldFromHandle, node.Syntax, null, false);
			if (!TypeSymbol.Equals(node.Type, getFieldFromHandle.ReturnType, TypeCompareKind.ConsiderEverything))
			{
				this._builder.EmitOpCode(ILOpCode.Castclass);
				this.EmitSymbolToken(node.Type, node.Syntax);
			}
		}

		// Token: 0x0600809C RID: 32924 RVA: 0x001D2164 File Offset: 0x001D0364
		private void EmitConditionalOperator(BoundConditionalOperator expr, bool used)
		{
			object obj = new object();
			object obj2 = new object();
			this.EmitCondBranch(expr.Condition, ref obj, true);
			this.EmitExpression(expr.Alternative, used);
			TypeSymbol typeSymbol = this.StackMergeType(expr.Alternative);
			if (used)
			{
				if (CodeGenerator.IsVarianceCast(expr.Type, typeSymbol))
				{
					this.EmitStaticCast(expr.Type, expr.Syntax);
					typeSymbol = expr.Type;
				}
				else if (expr.Type.IsInterfaceType() && !TypeSymbol.Equals(expr.Type, typeSymbol, TypeCompareKind.ConsiderEverything))
				{
					this.EmitStaticCast(expr.Type, expr.Syntax);
				}
			}
			this._builder.EmitBranch(ILOpCode.Br, obj2, ILOpCode.Nop);
			if (used)
			{
				this._builder.AdjustStack(-1);
			}
			this._builder.MarkLabel(obj);
			this.EmitExpression(expr.Consequence, used);
			if (used)
			{
				TypeSymbol typeSymbol2 = this.StackMergeType(expr.Consequence);
				if (CodeGenerator.IsVarianceCast(expr.Type, typeSymbol2))
				{
					this.EmitStaticCast(expr.Type, expr.Syntax);
					typeSymbol2 = expr.Type;
				}
				else if (expr.Type.IsInterfaceType() && !TypeSymbol.Equals(expr.Type, typeSymbol2, TypeCompareKind.ConsiderEverything))
				{
					this.EmitStaticCast(expr.Type, expr.Syntax);
				}
			}
			this._builder.MarkLabel(obj2);
		}

		// Token: 0x0600809D RID: 32925 RVA: 0x001D22AC File Offset: 0x001D04AC
		private void EmitNullCoalescingOperator(BoundNullCoalescingOperator expr, bool used)
		{
			this.EmitExpression(expr.LeftOperand, true);
			TypeSymbol typeSymbol = this.StackMergeType(expr.LeftOperand);
			if (used)
			{
				if (CodeGenerator.IsVarianceCast(expr.Type, typeSymbol))
				{
					this.EmitStaticCast(expr.Type, expr.Syntax);
					typeSymbol = expr.Type;
				}
				else if (expr.Type.IsInterfaceType() && !TypeSymbol.Equals(expr.Type, typeSymbol, TypeCompareKind.ConsiderEverything))
				{
					this.EmitStaticCast(expr.Type, expr.Syntax);
				}
				this._builder.EmitOpCode(ILOpCode.Dup);
			}
			if (expr.Type.IsTypeParameter())
			{
				this.EmitBox(expr.Type, expr.LeftOperand.Syntax);
			}
			object obj = new object();
			this._builder.EmitBranch(ILOpCode.Brtrue, obj, ILOpCode.Nop);
			if (used)
			{
				this._builder.EmitOpCode(ILOpCode.Pop);
			}
			this.EmitExpression(expr.RightOperand, used);
			if (used)
			{
				TypeSymbol typeSymbol2 = this.StackMergeType(expr.RightOperand);
				if (CodeGenerator.IsVarianceCast(expr.Type, typeSymbol2))
				{
					this.EmitStaticCast(expr.Type, expr.Syntax);
					typeSymbol2 = expr.Type;
				}
			}
			this._builder.MarkLabel(obj);
		}

		// Token: 0x0600809E RID: 32926 RVA: 0x001D23D4 File Offset: 0x001D05D4
		private TypeSymbol StackMergeType(BoundExpression expr)
		{
			if (!expr.Type.IsInterfaceType() && !expr.Type.IsDelegateType())
			{
				return expr.Type;
			}
			BoundKind kind = expr.Kind;
			if (kind <= BoundKind.AssignmentOperator)
			{
				if (kind == BoundKind.Dup)
				{
					return null;
				}
				if (kind == BoundKind.AssignmentOperator)
				{
					BoundAssignmentOperator boundAssignmentOperator = (BoundAssignmentOperator)expr;
					return this.StackMergeType(boundAssignmentOperator.Right);
				}
			}
			else if (kind != BoundKind.Conversion)
			{
				if (kind != BoundKind.Local)
				{
					if (kind == BoundKind.Sequence)
					{
						BoundSequence boundSequence = (BoundSequence)expr;
						return this.StackMergeType(boundSequence.Value);
					}
				}
				else
				{
					BoundLocal boundLocal = (BoundLocal)expr;
					if (this.IsStackLocal(boundLocal.LocalSymbol))
					{
						return null;
					}
				}
			}
			else
			{
				BoundConversion boundConversion = (BoundConversion)expr;
				ConversionKind conversionKind = boundConversion.ConversionKind;
				if (conversionKind.IsImplicitConversion() && conversionKind != ConversionKind.MethodGroup && conversionKind != ConversionKind.NullLiteral && conversionKind != ConversionKind.DefaultLiteral)
				{
					return this.StackMergeType(boundConversion.Operand);
				}
			}
			return expr.Type;
		}

		// Token: 0x0600809F RID: 32927 RVA: 0x001D24B8 File Offset: 0x001D06B8
		private static bool IsVarianceCast(TypeSymbol to, TypeSymbol from)
		{
			if (TypeSymbol.Equals(to, from, TypeCompareKind.ConsiderEverything))
			{
				return false;
			}
			if (from == null)
			{
				return true;
			}
			if (to.IsArray())
			{
				return CodeGenerator.IsVarianceCast(((ArrayTypeSymbol)to).ElementType, ((ArrayTypeSymbol)from).ElementType);
			}
			return (to.IsDelegateType() && !TypeSymbol.Equals(to, from, TypeCompareKind.ConsiderEverything)) || (to.IsInterfaceType() && from.IsInterfaceType() && !from.InterfacesAndTheirBaseInterfacesNoUseSiteDiagnostics.ContainsKey((NamedTypeSymbol)to));
		}

		// Token: 0x060080A0 RID: 32928 RVA: 0x001D2534 File Offset: 0x001D0734
		private void EmitStaticCast(TypeSymbol to, SyntaxNode syntax)
		{
			LocalDefinition localDefinition = this.AllocateTemp(to, syntax, LocalSlotConstraints.None);
			this._builder.EmitLocalStore(localDefinition);
			this._builder.EmitLocalLoad(localDefinition);
			this.FreeTemp(localDefinition);
		}

		// Token: 0x060080A1 RID: 32929 RVA: 0x001D256A File Offset: 0x001D076A
		private void EmitBox(TypeSymbol type, SyntaxNode syntaxNode)
		{
			this._builder.EmitOpCode(ILOpCode.Box);
			this.EmitSymbolToken(type, syntaxNode);
		}

		// Token: 0x060080A2 RID: 32930 RVA: 0x001D2584 File Offset: 0x001D0784
		private void EmitCalli(BoundFunctionPointerInvocation ptrInvocation, CodeGenerator.UseKind useKind)
		{
			this.EmitExpression(ptrInvocation.InvokedExpression, true);
			LocalDefinition localDefinition = null;
			if (ptrInvocation.Arguments.Length > 0)
			{
				localDefinition = this.AllocateTemp(ptrInvocation.InvokedExpression.Type, ptrInvocation.Syntax, LocalSlotConstraints.None);
				this._builder.EmitLocalStore(localDefinition);
			}
			FunctionPointerMethodSymbol signature = ptrInvocation.FunctionPointer.Signature;
			this.EmitArguments(ptrInvocation.Arguments, signature.Parameters, ptrInvocation.ArgumentRefKindsOpt);
			int callStackBehavior = CodeGenerator.GetCallStackBehavior(ptrInvocation.FunctionPointer.Signature, ptrInvocation.Arguments);
			if (localDefinition != null)
			{
				this._builder.EmitLocalLoad(localDefinition);
				this.FreeTemp(localDefinition);
			}
			this._builder.EmitOpCode(ILOpCode.Calli, callStackBehavior);
			this.EmitSignatureToken(ptrInvocation.FunctionPointer, ptrInvocation.Syntax);
			this.EmitCallCleanup(ptrInvocation.Syntax, useKind, signature);
		}

		// Token: 0x060080A3 RID: 32931 RVA: 0x001D2658 File Offset: 0x001D0858
		private void EmitCallCleanup(SyntaxNode syntax, CodeGenerator.UseKind useKind, MethodSymbol method)
		{
			if (!method.ReturnsVoid)
			{
				this.EmitPopIfUnused(useKind > CodeGenerator.UseKind.Unused);
			}
			else if (this._ilEmitStyle == ILEmitStyle.Debug)
			{
				this._builder.EmitOpCode(ILOpCode.Nop);
			}
			if (useKind == CodeGenerator.UseKind.UsedAsValue && method.RefKind != RefKind.None)
			{
				this.EmitLoadIndirect(method.ReturnType, syntax);
				return;
			}
		}

		// Token: 0x060080A4 RID: 32932 RVA: 0x001D26AB File Offset: 0x001D08AB
		private void EmitLoadFunction(BoundFunctionPointerLoad load, bool used)
		{
			if (used)
			{
				this._builder.EmitOpCode(ILOpCode.Ldftn);
				this.EmitSymbolToken(load.TargetMethod, load.Syntax, null, false);
			}
		}

		// Token: 0x060080A5 RID: 32933 RVA: 0x001D26D4 File Offset: 0x001D08D4
		private void EmitUnaryOperatorExpression(BoundUnaryOperator expression, bool used)
		{
			UnaryOperatorKind operatorKind = expression.OperatorKind;
			if (operatorKind.IsChecked())
			{
				this.EmitUnaryCheckedOperatorExpression(expression, used);
				return;
			}
			if (!used)
			{
				this.EmitExpression(expression.Operand, false);
				return;
			}
			if (operatorKind == UnaryOperatorKind.BoolLogicalNegation)
			{
				this.EmitCondExpr(expression.Operand, false);
				return;
			}
			this.EmitExpression(expression.Operand, true);
			UnaryOperatorKind unaryOperatorKind = operatorKind.Operator();
			if (unaryOperatorKind == UnaryOperatorKind.UnaryPlus)
			{
				return;
			}
			if (unaryOperatorKind == UnaryOperatorKind.UnaryMinus)
			{
				this._builder.EmitOpCode(ILOpCode.Neg);
				return;
			}
			if (unaryOperatorKind != UnaryOperatorKind.BitwiseComplement)
			{
				throw ExceptionUtilities.UnexpectedValue(operatorKind.Operator());
			}
			this._builder.EmitOpCode(ILOpCode.Not);
		}

		// Token: 0x060080A6 RID: 32934 RVA: 0x001D277C File Offset: 0x001D097C
		private void EmitBinaryOperatorExpression(BoundBinaryOperator expression, bool used)
		{
			BinaryOperatorKind operatorKind = expression.OperatorKind;
			if (operatorKind.EmitsAsCheckedInstruction())
			{
				this.EmitBinaryOperator(expression);
			}
			else
			{
				if (!used && !operatorKind.IsLogical() && !CodeGenerator.OperatorHasSideEffects(operatorKind))
				{
					this.EmitExpression(expression.Left, false);
					this.EmitExpression(expression.Right, false);
					return;
				}
				if (CodeGenerator.IsConditional(operatorKind))
				{
					this.EmitBinaryCondOperator(expression, true);
				}
				else
				{
					this.EmitBinaryOperator(expression);
				}
			}
			this.EmitPopIfUnused(used);
		}

		// Token: 0x060080A7 RID: 32935 RVA: 0x001D27F0 File Offset: 0x001D09F0
		private void EmitBinaryOperator(BoundBinaryOperator expression)
		{
			BoundExpression boundExpression = expression.Left;
			if (boundExpression.Kind != BoundKind.BinaryOperator || boundExpression.ConstantValue != null)
			{
				this.EmitBinaryOperatorSimple(expression);
				return;
			}
			BoundBinaryOperator boundBinaryOperator = (BoundBinaryOperator)boundExpression;
			BinaryOperatorKind binaryOperatorKind = boundBinaryOperator.OperatorKind;
			if (!binaryOperatorKind.EmitsAsCheckedInstruction() && CodeGenerator.IsConditional(binaryOperatorKind))
			{
				this.EmitBinaryOperatorSimple(expression);
				return;
			}
			ArrayBuilder<BoundBinaryOperator> instance = ArrayBuilder<BoundBinaryOperator>.GetInstance();
			instance.Push(expression);
			do
			{
				instance.Push(boundBinaryOperator);
				boundExpression = boundBinaryOperator.Left;
				if (boundExpression.Kind != BoundKind.BinaryOperator || boundExpression.ConstantValue != null)
				{
					break;
				}
				boundBinaryOperator = (BoundBinaryOperator)boundExpression;
				binaryOperatorKind = boundBinaryOperator.OperatorKind;
			}
			while (binaryOperatorKind.EmitsAsCheckedInstruction() || !CodeGenerator.IsConditional(binaryOperatorKind));
			this.EmitExpression(boundExpression, true);
			do
			{
				boundBinaryOperator = instance.Pop<BoundBinaryOperator>();
				this.EmitExpression(boundBinaryOperator.Right, true);
				bool flag = boundBinaryOperator.OperatorKind.EmitsAsCheckedInstruction();
				if (flag)
				{
					this.EmitBinaryCheckedOperatorInstruction(boundBinaryOperator);
				}
				else
				{
					this.EmitBinaryOperatorInstruction(boundBinaryOperator);
				}
				this.EmitConversionToEnumUnderlyingType(boundBinaryOperator, flag);
			}
			while (instance.Count > 0);
			instance.Free();
		}

		// Token: 0x060080A8 RID: 32936 RVA: 0x001D28F0 File Offset: 0x001D0AF0
		private void EmitBinaryOperatorSimple(BoundBinaryOperator expression)
		{
			this.EmitExpression(expression.Left, true);
			this.EmitExpression(expression.Right, true);
			bool flag = expression.OperatorKind.EmitsAsCheckedInstruction();
			if (flag)
			{
				this.EmitBinaryCheckedOperatorInstruction(expression);
			}
			else
			{
				this.EmitBinaryOperatorInstruction(expression);
			}
			this.EmitConversionToEnumUnderlyingType(expression, flag);
		}

		// Token: 0x060080A9 RID: 32937 RVA: 0x001D2940 File Offset: 0x001D0B40
		private void EmitBinaryOperatorInstruction(BoundBinaryOperator expression)
		{
			BinaryOperatorKind binaryOperatorKind = expression.OperatorKind.Operator();
			if (binaryOperatorKind <= BinaryOperatorKind.Remainder)
			{
				if (binaryOperatorKind <= BinaryOperatorKind.Addition)
				{
					if (binaryOperatorKind == BinaryOperatorKind.Multiplication)
					{
						this._builder.EmitOpCode(ILOpCode.Mul);
						return;
					}
					if (binaryOperatorKind == BinaryOperatorKind.Addition)
					{
						this._builder.EmitOpCode(ILOpCode.Add);
						return;
					}
				}
				else
				{
					if (binaryOperatorKind == BinaryOperatorKind.Subtraction)
					{
						this._builder.EmitOpCode(ILOpCode.Sub);
						return;
					}
					if (binaryOperatorKind != BinaryOperatorKind.Division)
					{
						if (binaryOperatorKind == BinaryOperatorKind.Remainder)
						{
							if (CodeGenerator.IsUnsignedBinaryOperator(expression))
							{
								this._builder.EmitOpCode(ILOpCode.Rem_un);
								return;
							}
							this._builder.EmitOpCode(ILOpCode.Rem);
							return;
						}
					}
					else
					{
						if (CodeGenerator.IsUnsignedBinaryOperator(expression))
						{
							this._builder.EmitOpCode(ILOpCode.Div_un);
							return;
						}
						this._builder.EmitOpCode(ILOpCode.Div);
						return;
					}
				}
			}
			else if (binaryOperatorKind <= BinaryOperatorKind.RightShift)
			{
				if (binaryOperatorKind == BinaryOperatorKind.LeftShift)
				{
					this._builder.EmitOpCode(ILOpCode.Shl);
					return;
				}
				if (binaryOperatorKind == BinaryOperatorKind.RightShift)
				{
					if (CodeGenerator.IsUnsignedBinaryOperator(expression))
					{
						this._builder.EmitOpCode(ILOpCode.Shr_un);
						return;
					}
					this._builder.EmitOpCode(ILOpCode.Shr);
					return;
				}
			}
			else
			{
				if (binaryOperatorKind == BinaryOperatorKind.And)
				{
					this._builder.EmitOpCode(ILOpCode.And);
					return;
				}
				if (binaryOperatorKind == BinaryOperatorKind.Xor)
				{
					this._builder.EmitOpCode(ILOpCode.Xor);
					return;
				}
				if (binaryOperatorKind == BinaryOperatorKind.Or)
				{
					this._builder.EmitOpCode(ILOpCode.Or);
					return;
				}
			}
			throw ExceptionUtilities.UnexpectedValue(expression.OperatorKind.Operator());
		}

		// Token: 0x060080AA RID: 32938 RVA: 0x001D2AD4 File Offset: 0x001D0CD4
		private void EmitShortCircuitingOperator(BoundBinaryOperator condition, bool sense, bool stopSense, bool stopValue)
		{
			object obj = null;
			this.EmitCondBranch(condition.Left, ref obj, stopSense);
			this.EmitCondExpr(condition.Right, sense);
			if (obj == null)
			{
				return;
			}
			object obj2 = new object();
			this._builder.EmitBranch(ILOpCode.Br, obj2, ILOpCode.Nop);
			this._builder.AdjustStack(-1);
			this._builder.MarkLabel(obj);
			this._builder.EmitBoolConstant(stopValue);
			this._builder.MarkLabel(obj2);
		}

		// Token: 0x060080AB RID: 32939 RVA: 0x001D2B4C File Offset: 0x001D0D4C
		private void EmitBinaryCondOperator(BoundBinaryOperator binOp, bool sense)
		{
			bool flag = sense;
			BinaryOperatorKind binaryOperatorKind = binOp.OperatorKind.OperatorWithLogical();
			int num;
			if (binaryOperatorKind <= BinaryOperatorKind.GreaterThanOrEqual)
			{
				if (binaryOperatorKind <= BinaryOperatorKind.NotEqual)
				{
					if (binaryOperatorKind != BinaryOperatorKind.Equal)
					{
						if (binaryOperatorKind != BinaryOperatorKind.NotEqual)
						{
							goto IL_01DB;
						}
						sense = !sense;
					}
					ConstantValue constantValue = binOp.Left.ConstantValue;
					BoundExpression boundExpression = binOp.Right;
					if (constantValue == null)
					{
						constantValue = boundExpression.ConstantValue;
						boundExpression = binOp.Left;
					}
					if (constantValue != null)
					{
						if (constantValue.IsDefaultValue)
						{
							if (!constantValue.IsFloating)
							{
								if (sense)
								{
									this.EmitIsNullOrZero(boundExpression, constantValue);
									return;
								}
								this.EmitIsNotNullOrZero(boundExpression, constantValue);
								return;
							}
						}
						else if (constantValue.IsBoolean)
						{
							this.EmitExpression(boundExpression, true);
							this.EmitIsSense(sense);
							return;
						}
					}
					this.EmitBinaryCondOperatorHelper(ILOpCode.Ceq, binOp.Left, binOp.Right, sense);
					return;
				}
				if (binaryOperatorKind == BinaryOperatorKind.GreaterThan)
				{
					num = 2;
					goto IL_01F1;
				}
				if (binaryOperatorKind == BinaryOperatorKind.LessThan)
				{
					num = 0;
					goto IL_01F1;
				}
				if (binaryOperatorKind == BinaryOperatorKind.GreaterThanOrEqual)
				{
					num = 3;
					sense = !sense;
					goto IL_01F1;
				}
			}
			else if (binaryOperatorKind <= BinaryOperatorKind.Xor)
			{
				if (binaryOperatorKind == BinaryOperatorKind.LessThanOrEqual)
				{
					num = 1;
					sense = !sense;
					goto IL_01F1;
				}
				if (binaryOperatorKind == BinaryOperatorKind.And)
				{
					this.EmitBinaryCondOperatorHelper(ILOpCode.And, binOp.Left, binOp.Right, sense);
					return;
				}
				if (binaryOperatorKind == BinaryOperatorKind.Xor)
				{
					if (sense)
					{
						this.EmitBinaryCondOperatorHelper(ILOpCode.Xor, binOp.Left, binOp.Right, true);
						return;
					}
					this.EmitBinaryCondOperatorHelper(ILOpCode.Ceq, binOp.Left, binOp.Right, true);
					return;
				}
			}
			else
			{
				if (binaryOperatorKind == BinaryOperatorKind.Or)
				{
					this.EmitBinaryCondOperatorHelper(ILOpCode.Or, binOp.Left, binOp.Right, sense);
					return;
				}
				if (binaryOperatorKind != BinaryOperatorKind.LogicalAnd)
				{
					if (binaryOperatorKind != BinaryOperatorKind.LogicalOr)
					{
						goto IL_01DB;
					}
					flag = !flag;
				}
				if (!flag)
				{
					this.EmitShortCircuitingOperator(binOp, sense, sense, true);
					return;
				}
				this.EmitShortCircuitingOperator(binOp, sense, !sense, false);
				return;
			}
			IL_01DB:
			throw ExceptionUtilities.UnexpectedValue(binOp.OperatorKind.OperatorWithLogical());
			IL_01F1:
			if (CodeGenerator.IsUnsignedBinaryOperator(binOp))
			{
				num += 4;
			}
			else if (CodeGenerator.IsFloat(binOp.OperatorKind))
			{
				num += 8;
			}
			this.EmitBinaryCondOperatorHelper(CodeGenerator.s_compOpCodes[num], binOp.Left, binOp.Right, sense);
		}

		// Token: 0x060080AC RID: 32940 RVA: 0x001D2D84 File Offset: 0x001D0F84
		private void EmitIsNotNullOrZero(BoundExpression comparand, ConstantValue nullOrZero)
		{
			this.EmitExpression(comparand, true);
			TypeSymbol type = comparand.Type;
			if (type.IsReferenceType && !type.IsVerifierReference())
			{
				this.EmitBox(type, comparand.Syntax);
			}
			this._builder.EmitConstantValue(nullOrZero);
			this._builder.EmitOpCode(ILOpCode.Cgt_un);
		}

		// Token: 0x060080AD RID: 32941 RVA: 0x001D2DDC File Offset: 0x001D0FDC
		private void EmitIsNullOrZero(BoundExpression comparand, ConstantValue nullOrZero)
		{
			this.EmitExpression(comparand, true);
			TypeSymbol type = comparand.Type;
			if (type.IsReferenceType && !type.IsVerifierReference())
			{
				this.EmitBox(type, comparand.Syntax);
			}
			this._builder.EmitConstantValue(nullOrZero);
			this._builder.EmitOpCode(ILOpCode.Ceq);
		}

		// Token: 0x060080AE RID: 32942 RVA: 0x001D2E31 File Offset: 0x001D1031
		private void EmitBinaryCondOperatorHelper(ILOpCode opCode, BoundExpression left, BoundExpression right, bool sense)
		{
			this.EmitExpression(left, true);
			this.EmitExpression(right, true);
			this._builder.EmitOpCode(opCode);
			this.EmitIsSense(sense);
		}

		// Token: 0x060080AF RID: 32943 RVA: 0x001D2E58 File Offset: 0x001D1058
		private void EmitCondExpr(BoundExpression condition, bool sense)
		{
			while (condition.Kind == BoundKind.UnaryOperator)
			{
				condition = ((BoundUnaryOperator)condition).Operand;
				sense = !sense;
			}
			ConstantValue constantValue = condition.ConstantValue;
			if (constantValue != null)
			{
				bool booleanValue = constantValue.BooleanValue;
				this._builder.EmitBoolConstant(booleanValue == sense);
				return;
			}
			if (condition.Kind == BoundKind.BinaryOperator)
			{
				BoundBinaryOperator boundBinaryOperator = (BoundBinaryOperator)condition;
				if (CodeGenerator.IsConditional(boundBinaryOperator.OperatorKind))
				{
					this.EmitBinaryCondOperator(boundBinaryOperator, sense);
					return;
				}
			}
			this.EmitExpression(condition, true);
			this.EmitIsSense(sense);
		}

		// Token: 0x060080B0 RID: 32944 RVA: 0x001D2EE4 File Offset: 0x001D10E4
		private void EmitUnaryCheckedOperatorExpression(BoundUnaryOperator expression, bool used)
		{
			int num = (int)expression.OperatorKind.OperandTypes();
			this._builder.EmitOpCode(ILOpCode.Ldc_i4_0);
			if (num == 7)
			{
				this._builder.EmitOpCode(ILOpCode.Conv_i8);
			}
			this.EmitExpression(expression.Operand, true);
			this._builder.EmitOpCode(ILOpCode.Sub_ovf);
			this.EmitPopIfUnused(used);
		}

		// Token: 0x060080B1 RID: 32945 RVA: 0x001D2F40 File Offset: 0x001D1140
		private void EmitConversionToEnumUnderlyingType(BoundBinaryOperator expression, bool @checked)
		{
			BinaryOperatorKind binaryOperatorKind = expression.OperatorKind.Operator() | expression.OperatorKind.OperandTypes();
			TypeSymbol typeSymbol;
			if (binaryOperatorKind <= BinaryOperatorKind.EnumAndUnderlyingSubtraction)
			{
				if (binaryOperatorKind != BinaryOperatorKind.EnumAndUnderlyingAddition)
				{
					if (binaryOperatorKind == BinaryOperatorKind.UnderlyingAndEnumAddition)
					{
						goto IL_007A;
					}
					if (binaryOperatorKind - BinaryOperatorKind.EnumSubtraction > 1)
					{
						goto IL_0088;
					}
				}
				typeSymbol = expression.Left.Type;
				goto IL_008A;
			}
			if (binaryOperatorKind <= BinaryOperatorKind.EnumAnd)
			{
				if (binaryOperatorKind == BinaryOperatorKind.UnderlyingAndEnumSubtraction)
				{
					goto IL_007A;
				}
				if (binaryOperatorKind != BinaryOperatorKind.EnumAnd)
				{
					goto IL_0088;
				}
			}
			else if (binaryOperatorKind != BinaryOperatorKind.EnumXor && binaryOperatorKind != BinaryOperatorKind.EnumOr)
			{
				goto IL_0088;
			}
			typeSymbol = null;
			goto IL_008A;
			IL_007A:
			typeSymbol = expression.Right.Type;
			goto IL_008A;
			IL_0088:
			typeSymbol = null;
			IL_008A:
			if (typeSymbol == null)
			{
				return;
			}
			switch (typeSymbol.GetEnumUnderlyingType().SpecialType)
			{
			case SpecialType.System_SByte:
				this._builder.EmitNumericConversion(Microsoft.Cci.PrimitiveTypeCode.Int32, Microsoft.Cci.PrimitiveTypeCode.Int8, @checked);
				return;
			case SpecialType.System_Byte:
				this._builder.EmitNumericConversion(Microsoft.Cci.PrimitiveTypeCode.Int32, Microsoft.Cci.PrimitiveTypeCode.UInt8, @checked);
				return;
			case SpecialType.System_Int16:
				this._builder.EmitNumericConversion(Microsoft.Cci.PrimitiveTypeCode.Int32, Microsoft.Cci.PrimitiveTypeCode.Int16, @checked);
				return;
			case SpecialType.System_UInt16:
				this._builder.EmitNumericConversion(Microsoft.Cci.PrimitiveTypeCode.Int32, Microsoft.Cci.PrimitiveTypeCode.UInt16, @checked);
				return;
			default:
				return;
			}
		}

		// Token: 0x060080B2 RID: 32946 RVA: 0x001D3040 File Offset: 0x001D1240
		private void EmitBinaryCheckedOperatorInstruction(BoundBinaryOperator expression)
		{
			bool flag = CodeGenerator.IsUnsignedBinaryOperator(expression);
			BinaryOperatorKind binaryOperatorKind = expression.OperatorKind.Operator();
			if (binaryOperatorKind != BinaryOperatorKind.Multiplication)
			{
				if (binaryOperatorKind != BinaryOperatorKind.Addition)
				{
					if (binaryOperatorKind != BinaryOperatorKind.Subtraction)
					{
						throw ExceptionUtilities.UnexpectedValue(expression.OperatorKind.Operator());
					}
					if (flag)
					{
						this._builder.EmitOpCode(ILOpCode.Sub_ovf_un);
						return;
					}
					this._builder.EmitOpCode(ILOpCode.Sub_ovf);
					return;
				}
				else
				{
					if (flag)
					{
						this._builder.EmitOpCode(ILOpCode.Add_ovf_un);
						return;
					}
					this._builder.EmitOpCode(ILOpCode.Add_ovf);
					return;
				}
			}
			else
			{
				if (flag)
				{
					this._builder.EmitOpCode(ILOpCode.Mul_ovf_un);
					return;
				}
				this._builder.EmitOpCode(ILOpCode.Mul_ovf);
				return;
			}
		}

		// Token: 0x060080B3 RID: 32947 RVA: 0x001D3100 File Offset: 0x001D1300
		private static bool OperatorHasSideEffects(BinaryOperatorKind kind)
		{
			BinaryOperatorKind binaryOperatorKind = kind.Operator();
			return binaryOperatorKind == BinaryOperatorKind.Division || binaryOperatorKind == BinaryOperatorKind.Remainder || kind.IsChecked();
		}

		// Token: 0x060080B4 RID: 32948 RVA: 0x001D312C File Offset: 0x001D132C
		private void EmitIsSense(bool sense)
		{
			if (!sense)
			{
				this._builder.EmitOpCode(ILOpCode.Ldc_i4_0);
				this._builder.EmitOpCode(ILOpCode.Ceq);
			}
		}

		// Token: 0x060080B5 RID: 32949 RVA: 0x001D314E File Offset: 0x001D134E
		private static bool IsUnsigned(SpecialType type)
		{
			switch (type)
			{
			case SpecialType.System_Byte:
			case SpecialType.System_UInt16:
			case SpecialType.System_UInt32:
			case SpecialType.System_UInt64:
				return true;
			}
			return false;
		}

		// Token: 0x060080B6 RID: 32950 RVA: 0x001D317C File Offset: 0x001D137C
		private static bool IsUnsignedBinaryOperator(BoundBinaryOperator op)
		{
			BinaryOperatorKind binaryOperatorKind = op.OperatorKind.OperandTypes();
			switch (binaryOperatorKind)
			{
			case BinaryOperatorKind.UInt:
			case BinaryOperatorKind.ULong:
			case BinaryOperatorKind.NUInt:
				break;
			case BinaryOperatorKind.Long:
			case BinaryOperatorKind.NInt:
				return false;
			default:
				switch (binaryOperatorKind)
				{
				case BinaryOperatorKind.Enum:
				case BinaryOperatorKind.EnumAndUnderlying:
					return CodeGenerator.IsUnsigned(Binder.GetEnumPromotedType(op.Left.Type.GetEnumUnderlyingType().SpecialType));
				case BinaryOperatorKind.UnderlyingAndEnum:
					return CodeGenerator.IsUnsigned(Binder.GetEnumPromotedType(op.Right.Type.GetEnumUnderlyingType().SpecialType));
				case BinaryOperatorKind.Delegate:
				case (BinaryOperatorKind)26:
				case (BinaryOperatorKind)27:
				case (BinaryOperatorKind)28:
				case BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Object:
				case BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Object:
				case (BinaryOperatorKind)31:
				case BinaryOperatorKind.IntAndPointer:
				case BinaryOperatorKind.UIntAndPointer:
				case BinaryOperatorKind.LongAndPointer:
					return false;
				case BinaryOperatorKind.Pointer:
				case BinaryOperatorKind.PointerAndInt:
				case BinaryOperatorKind.PointerAndUInt:
				case BinaryOperatorKind.PointerAndLong:
				case BinaryOperatorKind.PointerAndULong:
				case BinaryOperatorKind.ULongAndPointer:
					break;
				default:
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x060080B7 RID: 32951 RVA: 0x001D324C File Offset: 0x001D144C
		private static bool IsConditional(BinaryOperatorKind opKind)
		{
			BinaryOperatorKind binaryOperatorKind = opKind.OperatorWithLogical();
			if (binaryOperatorKind > BinaryOperatorKind.GreaterThanOrEqual)
			{
				if (binaryOperatorKind <= BinaryOperatorKind.Xor)
				{
					if (binaryOperatorKind == BinaryOperatorKind.LessThanOrEqual)
					{
						return true;
					}
					if (binaryOperatorKind != BinaryOperatorKind.And && binaryOperatorKind != BinaryOperatorKind.Xor)
					{
						return false;
					}
				}
				else if (binaryOperatorKind != BinaryOperatorKind.Or)
				{
					if (binaryOperatorKind == BinaryOperatorKind.LogicalAnd || binaryOperatorKind == BinaryOperatorKind.LogicalOr)
					{
						return true;
					}
					return false;
				}
				return opKind.OperandTypes() == BinaryOperatorKind.Bool;
			}
			if (binaryOperatorKind <= BinaryOperatorKind.NotEqual)
			{
				if (binaryOperatorKind != BinaryOperatorKind.Equal && binaryOperatorKind != BinaryOperatorKind.NotEqual)
				{
					return false;
				}
			}
			else if (binaryOperatorKind != BinaryOperatorKind.GreaterThan && binaryOperatorKind != BinaryOperatorKind.LessThan && binaryOperatorKind != BinaryOperatorKind.GreaterThanOrEqual)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060080B8 RID: 32952 RVA: 0x001D32E4 File Offset: 0x001D14E4
		private static bool IsFloat(BinaryOperatorKind opKind)
		{
			BinaryOperatorKind binaryOperatorKind = opKind.OperandTypes();
			return binaryOperatorKind - BinaryOperatorKind.Float <= 1;
		}

		// Token: 0x060080B9 RID: 32953 RVA: 0x001D3304 File Offset: 0x001D1504
		private void EmitStackAllocInitializers(TypeSymbol type, BoundArrayInitialization inits)
		{
			TypeSymbol type2 = ((type.TypeKind == TypeKind.Pointer) ? ((PointerTypeSymbol)type).PointedAtTypeWithAnnotations : ((NamedTypeSymbol)type).TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[0]).Type;
			ImmutableArray<BoundExpression> initializers = inits.Initializers;
			CodeGenerator.ArrayInitializerStyle arrayInitializerStyle = this.ShouldEmitBlockInitializerForStackAlloc(type2, initializers);
			if (arrayInitializerStyle == CodeGenerator.ArrayInitializerStyle.Element)
			{
				this.EmitElementStackAllocInitializers(type2, initializers, true);
				return;
			}
			ImmutableArray<byte> data = this.GetRawData(initializers);
			if (data.All<byte>((byte datum) => datum == data[0]))
			{
				this._builder.EmitStackAllocBlockInitializer(data, inits.Syntax, true, this._diagnostics);
				if (arrayInitializerStyle == CodeGenerator.ArrayInitializerStyle.Mixed)
				{
					this.EmitElementStackAllocInitializers(type2, initializers, false);
					return;
				}
			}
			else if (type2.SpecialType.SizeInBytes() == 1)
			{
				this._builder.EmitStackAllocBlockInitializer(data, inits.Syntax, false, this._diagnostics);
				if (arrayInitializerStyle == CodeGenerator.ArrayInitializerStyle.Mixed)
				{
					this.EmitElementStackAllocInitializers(type2, initializers, false);
					return;
				}
			}
			else
			{
				this.EmitElementStackAllocInitializers(type2, initializers, true);
			}
		}

		// Token: 0x060080BA RID: 32954 RVA: 0x001D3404 File Offset: 0x001D1604
		private CodeGenerator.ArrayInitializerStyle ShouldEmitBlockInitializerForStackAlloc(TypeSymbol elementType, ImmutableArray<BoundExpression> inits)
		{
			if (!this._module.SupportsPrivateImplClass)
			{
				return CodeGenerator.ArrayInitializerStyle.Element;
			}
			elementType = elementType.EnumUnderlyingTypeOrSelf();
			if (elementType.SpecialType.IsBlittable())
			{
				int num = 0;
				int num2 = 0;
				this.StackAllocInitializerCount(inits, ref num, ref num2);
				if (num > 2)
				{
					if (num == num2)
					{
						return CodeGenerator.ArrayInitializerStyle.Block;
					}
					int num3 = Math.Max(3, num / 3);
					if (num2 >= num3)
					{
						return CodeGenerator.ArrayInitializerStyle.Mixed;
					}
				}
			}
			return CodeGenerator.ArrayInitializerStyle.Element;
		}

		// Token: 0x060080BB RID: 32955 RVA: 0x001D3460 File Offset: 0x001D1660
		private void StackAllocInitializerCount(ImmutableArray<BoundExpression> inits, ref int initCount, ref int constInits)
		{
			if (inits.Length == 0)
			{
				return;
			}
			foreach (BoundExpression boundExpression in inits)
			{
				initCount++;
				if (boundExpression.ConstantValue != null)
				{
					constInits++;
				}
			}
		}

		// Token: 0x060080BC RID: 32956 RVA: 0x001D34AC File Offset: 0x001D16AC
		private void EmitElementStackAllocInitializers(TypeSymbol elementType, ImmutableArray<BoundExpression> inits, bool includeConstants)
		{
			int num = 0;
			int num2 = elementType.SpecialType.SizeInBytes();
			foreach (BoundExpression boundExpression in inits)
			{
				if (includeConstants || boundExpression.ConstantValue == null)
				{
					this._builder.EmitOpCode(ILOpCode.Dup);
					this.EmitPointerElementAccess(boundExpression, elementType, num2, num);
					this.EmitExpression(boundExpression, true);
					this.EmitIndirectStore(elementType, boundExpression.Syntax);
				}
				num++;
			}
		}

		// Token: 0x060080BD RID: 32957 RVA: 0x001D3524 File Offset: 0x001D1724
		private void EmitPointerElementAccess(BoundExpression init, TypeSymbol elementType, int elementTypeSizeInBytes, int index)
		{
			if (index == 0)
			{
				return;
			}
			if (elementTypeSizeInBytes == 1)
			{
				this._builder.EmitIntConstant(index);
				this._builder.EmitOpCode(ILOpCode.Add);
				return;
			}
			if (index == 1)
			{
				this.EmitIntConstantOrSizeOf(init, elementType, elementTypeSizeInBytes);
				this._builder.EmitOpCode(ILOpCode.Add);
				return;
			}
			this._builder.EmitIntConstant(index);
			this._builder.EmitOpCode(ILOpCode.Conv_i);
			this.EmitIntConstantOrSizeOf(init, elementType, elementTypeSizeInBytes);
			this._builder.EmitOpCode(ILOpCode.Mul);
			this._builder.EmitOpCode(ILOpCode.Add);
		}

		// Token: 0x060080BE RID: 32958 RVA: 0x001D35B1 File Offset: 0x001D17B1
		private void EmitIntConstantOrSizeOf(BoundExpression init, TypeSymbol elementType, int elementTypeSizeInBytes)
		{
			if (elementTypeSizeInBytes == 0)
			{
				this._builder.EmitOpCode(ILOpCode.Sizeof);
				this.EmitSymbolToken(elementType, init.Syntax);
				return;
			}
			this._builder.EmitIntConstant(elementTypeSizeInBytes);
		}

		// Token: 0x060080BF RID: 32959 RVA: 0x001D35E0 File Offset: 0x001D17E0
		private void EmitStatement(BoundStatement statement)
		{
			BoundKind kind = statement.Kind;
			switch (kind)
			{
			case BoundKind.SequencePoint:
				this.EmitSequencePointStatement((BoundSequencePoint)statement);
				goto IL_01C8;
			case BoundKind.SequencePointWithSpan:
				this.EmitSequencePointStatement((BoundSequencePointWithSpan)statement);
				goto IL_01C8;
			case BoundKind.SavePreviousSequencePoint:
				this.EmitSavePreviousSequencePoint((BoundSavePreviousSequencePoint)statement);
				goto IL_01C8;
			case BoundKind.RestorePreviousSequencePoint:
				this.EmitRestorePreviousSequencePoint((BoundRestorePreviousSequencePoint)statement);
				goto IL_01C8;
			case BoundKind.StepThroughSequencePoint:
				this.EmitStepThroughSequencePoint((BoundStepThroughSequencePoint)statement);
				goto IL_01C8;
			case BoundKind.Block:
				this.EmitBlock((BoundBlock)statement);
				goto IL_01C8;
			case BoundKind.Scope:
				this.EmitScope((BoundScope)statement);
				goto IL_01C8;
			case BoundKind.StateMachineScope:
				this.EmitStateMachineScope((BoundStateMachineScope)statement);
				goto IL_01C8;
			case BoundKind.LocalDeclaration:
			case BoundKind.MultipleLocalDeclarations:
			case BoundKind.UsingLocalDeclarations:
			case BoundKind.LocalFunctionStatement:
			case BoundKind.YieldReturnStatement:
			case BoundKind.YieldBreakStatement:
			case BoundKind.BreakStatement:
			case BoundKind.ContinueStatement:
			case BoundKind.SwitchStatement:
				break;
			case BoundKind.NoOpStatement:
				this.EmitNoOpStatement((BoundNoOpStatement)statement);
				goto IL_01C8;
			case BoundKind.ReturnStatement:
				this.EmitReturnStatement((BoundReturnStatement)statement);
				goto IL_01C8;
			case BoundKind.ThrowStatement:
				this.EmitThrowStatement((BoundThrowStatement)statement);
				goto IL_01C8;
			case BoundKind.ExpressionStatement:
				this.EmitExpression(((BoundExpressionStatement)statement).Expression, false);
				goto IL_01C8;
			case BoundKind.SwitchDispatch:
				this.EmitSwitchDispatch((BoundSwitchDispatch)statement);
				goto IL_01C8;
			default:
				if (kind == BoundKind.TryStatement)
				{
					this.EmitTryStatement((BoundTryStatement)statement, false);
					goto IL_01C8;
				}
				switch (kind)
				{
				case BoundKind.LabelStatement:
					this.EmitLabelStatement((BoundLabelStatement)statement);
					goto IL_01C8;
				case BoundKind.GotoStatement:
					this.EmitGotoStatement((BoundGotoStatement)statement);
					goto IL_01C8;
				case BoundKind.StatementList:
					this.EmitStatementList((BoundStatementList)statement);
					goto IL_01C8;
				case BoundKind.ConditionalGoto:
					this.EmitConditionalGoto((BoundConditionalGoto)statement);
					goto IL_01C8;
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(statement.Kind);
			IL_01C8:
			this.ReleaseExpressionTemps();
		}

		// Token: 0x060080C0 RID: 32960 RVA: 0x001D37BC File Offset: 0x001D19BC
		private int EmitStatementAndCountInstructions(BoundStatement statement)
		{
			int instructionsEmitted = this._builder.InstructionsEmitted;
			this.EmitStatement(statement);
			return this._builder.InstructionsEmitted - instructionsEmitted;
		}

		// Token: 0x060080C1 RID: 32961 RVA: 0x001D37EC File Offset: 0x001D19EC
		private void EmitStatementList(BoundStatementList list)
		{
			int i = 0;
			int length = list.Statements.Length;
			while (i < length)
			{
				this.EmitStatement(list.Statements[i]);
				i++;
			}
		}

		// Token: 0x060080C2 RID: 32962 RVA: 0x001D382C File Offset: 0x001D1A2C
		private void EmitNoOpStatement(BoundNoOpStatement statement)
		{
			switch (statement.Flavor)
			{
			case NoOpStatementFlavor.Default:
				if (this._ilEmitStyle == ILEmitStyle.Debug)
				{
					this._builder.EmitOpCode(ILOpCode.Nop);
					return;
				}
				return;
			case NoOpStatementFlavor.AwaitYieldPoint:
				if (this._asyncYieldPoints == null)
				{
					this._asyncYieldPoints = ArrayBuilder<int>.GetInstance();
					this._asyncResumePoints = ArrayBuilder<int>.GetInstance();
				}
				this._asyncYieldPoints.Add(this._builder.AllocateILMarker());
				return;
			case NoOpStatementFlavor.AwaitResumePoint:
				this._asyncResumePoints.Add(this._builder.AllocateILMarker());
				return;
			default:
				throw ExceptionUtilities.UnexpectedValue(statement.Flavor);
			}
		}

		// Token: 0x060080C3 RID: 32963 RVA: 0x001D38C6 File Offset: 0x001D1AC6
		private void EmitThrowStatement(BoundThrowStatement node)
		{
			this.EmitThrow(node.ExpressionOpt);
		}

		// Token: 0x060080C4 RID: 32964 RVA: 0x001D38D4 File Offset: 0x001D1AD4
		private void EmitThrow(BoundExpression thrown)
		{
			if (thrown != null)
			{
				this.EmitExpression(thrown, true);
				TypeSymbol type = thrown.Type;
				if (type != null && type.TypeKind == TypeKind.TypeParameter)
				{
					this.EmitBox(type, thrown.Syntax);
				}
			}
			this._builder.EmitThrow(thrown == null);
		}

		// Token: 0x060080C5 RID: 32965 RVA: 0x001D391C File Offset: 0x001D1B1C
		private void EmitConditionalGoto(BoundConditionalGoto boundConditionalGoto)
		{
			object label = boundConditionalGoto.Label;
			this.EmitCondBranch(boundConditionalGoto.Condition, ref label, boundConditionalGoto.JumpIfTrue);
		}

		// Token: 0x060080C6 RID: 32966 RVA: 0x001D3944 File Offset: 0x001D1B44
		private static bool CanPassToBrfalse(TypeSymbol ts)
		{
			if (ts.IsEnumType())
			{
				return true;
			}
			Microsoft.Cci.PrimitiveTypeCode primitiveTypeCode = ts.PrimitiveTypeCode;
			return primitiveTypeCode - Microsoft.Cci.PrimitiveTypeCode.Float32 > 1 && (primitiveTypeCode != Microsoft.Cci.PrimitiveTypeCode.NotPrimitive || ts.IsReferenceType);
		}

		// Token: 0x060080C7 RID: 32967 RVA: 0x001D397C File Offset: 0x001D1B7C
		private static BoundExpression TryReduce(BoundBinaryOperator condition, ref bool sense)
		{
			BinaryOperatorKind binaryOperatorKind = condition.OperatorKind.Operator();
			BoundExpression boundExpression = ((condition.Left.ConstantValue != null) ? condition.Left : null);
			BoundExpression boundExpression2;
			if (boundExpression != null)
			{
				boundExpression2 = condition.Right;
			}
			else
			{
				boundExpression = ((condition.Right.ConstantValue != null) ? condition.Right : null);
				if (boundExpression == null)
				{
					return null;
				}
				boundExpression2 = condition.Left;
			}
			TypeSymbol type = boundExpression2.Type;
			if (!CodeGenerator.CanPassToBrfalse(type))
			{
				return null;
			}
			bool flag = type.PrimitiveTypeCode == Microsoft.Cci.PrimitiveTypeCode.Boolean;
			bool isDefaultValue = boundExpression.ConstantValue.IsDefaultValue;
			if (!flag && !isDefaultValue)
			{
				return null;
			}
			if (isDefaultValue)
			{
				sense = !sense;
			}
			if (binaryOperatorKind == BinaryOperatorKind.NotEqual)
			{
				sense = !sense;
			}
			return boundExpression2;
		}

		// Token: 0x060080C8 RID: 32968 RVA: 0x001D3A34 File Offset: 0x001D1C34
		private static ILOpCode CodeForJump(BoundBinaryOperator op, bool sense, out ILOpCode revOpCode)
		{
			BinaryOperatorKind binaryOperatorKind = op.OperatorKind.Operator();
			int num;
			if (binaryOperatorKind <= BinaryOperatorKind.GreaterThan)
			{
				if (binaryOperatorKind != BinaryOperatorKind.Equal)
				{
					if (binaryOperatorKind != BinaryOperatorKind.NotEqual)
					{
						if (binaryOperatorKind == BinaryOperatorKind.GreaterThan)
						{
							num = 2;
							goto IL_0096;
						}
					}
					else
					{
						revOpCode = ((!sense) ? ILOpCode.Bne_un : ILOpCode.Beq);
						if (!sense)
						{
							return ILOpCode.Beq;
						}
						return ILOpCode.Bne_un;
					}
				}
				else
				{
					revOpCode = ((!sense) ? ILOpCode.Beq : ILOpCode.Bne_un);
					if (!sense)
					{
						return ILOpCode.Bne_un;
					}
					return ILOpCode.Beq;
				}
			}
			else
			{
				if (binaryOperatorKind == BinaryOperatorKind.LessThan)
				{
					num = 0;
					goto IL_0096;
				}
				if (binaryOperatorKind == BinaryOperatorKind.GreaterThanOrEqual)
				{
					num = 3;
					goto IL_0096;
				}
				if (binaryOperatorKind == BinaryOperatorKind.LessThanOrEqual)
				{
					num = 1;
					goto IL_0096;
				}
			}
			throw ExceptionUtilities.UnexpectedValue(op.OperatorKind.Operator());
			IL_0096:
			if (CodeGenerator.IsUnsignedBinaryOperator(op))
			{
				num += 8;
			}
			else if (CodeGenerator.IsFloat(op.OperatorKind))
			{
				num += 16;
			}
			int num2 = num;
			if (!sense)
			{
				num += 4;
			}
			else
			{
				num2 += 4;
			}
			revOpCode = CodeGenerator.s_condJumpOpCodes[num2];
			return CodeGenerator.s_condJumpOpCodes[num];
		}

		// Token: 0x060080C9 RID: 32969 RVA: 0x001D3B18 File Offset: 0x001D1D18
		private void EmitCondBranch(BoundExpression condition, ref object dest, bool sense)
		{
			this._recursionDepth++;
			if (this._recursionDepth > 1)
			{
				StackGuard.EnsureSufficientExecutionStack(this._recursionDepth);
				this.EmitCondBranchCore(condition, ref dest, sense);
			}
			else
			{
				this.EmitCondBranchCoreWithStackGuard(condition, ref dest, sense);
			}
			this._recursionDepth--;
		}

		// Token: 0x060080CA RID: 32970 RVA: 0x001D3B6C File Offset: 0x001D1D6C
		private void EmitCondBranchCoreWithStackGuard(BoundExpression condition, ref object dest, bool sense)
		{
			try
			{
				this.EmitCondBranchCore(condition, ref dest, sense);
			}
			catch (InsufficientExecutionStackException)
			{
				this._diagnostics.Add(ErrorCode.ERR_InsufficientStack, BoundTreeVisitor.CancelledByStackGuardException.GetTooLongOrComplexExpressionErrorLocation(condition));
				throw new CodeGenerator.EmitCancelledException();
			}
		}

		// Token: 0x060080CB RID: 32971 RVA: 0x001D3BB4 File Offset: 0x001D1DB4
		private void EmitCondBranchCore(BoundExpression condition, ref object dest, bool sense)
		{
			while (!(condition.ConstantValue != null))
			{
				BoundKind kind = condition.Kind;
				ILOpCode ilopCode;
				if (kind <= BoundKind.BinaryOperator)
				{
					if (kind != BoundKind.UnaryOperator)
					{
						if (kind == BoundKind.BinaryOperator)
						{
							BoundBinaryOperator boundBinaryOperator = (BoundBinaryOperator)condition;
							bool flag = sense;
							BinaryOperatorKind binaryOperatorKind = boundBinaryOperator.OperatorKind.OperatorWithLogical();
							if (binaryOperatorKind <= BinaryOperatorKind.LessThan)
							{
								if (binaryOperatorKind <= BinaryOperatorKind.NotEqual)
								{
									if (binaryOperatorKind != BinaryOperatorKind.Equal && binaryOperatorKind != BinaryOperatorKind.NotEqual)
									{
										goto IL_0367;
									}
									BoundExpression boundExpression = CodeGenerator.TryReduce(boundBinaryOperator, ref sense);
									if (boundExpression != null)
									{
										condition = boundExpression;
										continue;
									}
								}
								else if (binaryOperatorKind != BinaryOperatorKind.GreaterThan && binaryOperatorKind != BinaryOperatorKind.LessThan)
								{
									goto IL_0367;
								}
							}
							else if (binaryOperatorKind <= BinaryOperatorKind.LessThanOrEqual)
							{
								if (binaryOperatorKind != BinaryOperatorKind.GreaterThanOrEqual && binaryOperatorKind != BinaryOperatorKind.LessThanOrEqual)
								{
									goto IL_0367;
								}
							}
							else
							{
								if (binaryOperatorKind != BinaryOperatorKind.LogicalAnd)
								{
									if (binaryOperatorKind != BinaryOperatorKind.LogicalOr)
									{
										goto IL_0367;
									}
									flag = !flag;
								}
								if (!flag)
								{
									this.EmitCondBranch(boundBinaryOperator.Left, ref dest, sense);
									condition = boundBinaryOperator.Right;
									continue;
								}
								object obj = null;
								this.EmitCondBranch(boundBinaryOperator.Left, ref obj, !sense);
								this.EmitCondBranch(boundBinaryOperator.Right, ref dest, sense);
								if (obj != null)
								{
									this._builder.MarkLabel(obj);
									return;
								}
								return;
							}
							this.EmitExpression(boundBinaryOperator.Left, true);
							this.EmitExpression(boundBinaryOperator.Right, true);
							ILOpCode ilopCode2;
							ilopCode = CodeGenerator.CodeForJump(boundBinaryOperator, sense, out ilopCode2);
							dest = dest ?? new object();
							this._builder.EmitBranch(ilopCode, dest, ilopCode2);
							return;
						}
					}
					else
					{
						BoundUnaryOperator boundUnaryOperator = (BoundUnaryOperator)condition;
						if (boundUnaryOperator.OperatorKind == UnaryOperatorKind.BoolLogicalNegation)
						{
							sense = !sense;
							condition = boundUnaryOperator.Operand;
							continue;
						}
					}
				}
				else
				{
					if (kind == BoundKind.IsOperator)
					{
						BoundIsOperator boundIsOperator = (BoundIsOperator)condition;
						BoundExpression operand = boundIsOperator.Operand;
						this.EmitExpression(operand, true);
						if (!operand.Type.IsVerifierReference())
						{
							this.EmitBox(operand.Type, operand.Syntax);
						}
						this._builder.EmitOpCode(ILOpCode.Isinst);
						this.EmitSymbolToken(boundIsOperator.TargetType.Type, boundIsOperator.TargetType.Syntax);
						ilopCode = (sense ? ILOpCode.Brtrue : ILOpCode.Brfalse);
						dest = dest ?? new object();
						this._builder.EmitBranch(ilopCode, dest, ILOpCode.Nop);
						return;
					}
					if (kind == BoundKind.Sequence)
					{
						BoundSequence boundSequence = (BoundSequence)condition;
						this.EmitSequenceCondBranch(boundSequence, ref dest, sense);
						return;
					}
					if (kind == BoundKind.LoweredConditionalAccess)
					{
						BoundLoweredConditionalAccess boundLoweredConditionalAccess = (BoundLoweredConditionalAccess)condition;
						BoundExpression receiver = boundLoweredConditionalAccess.Receiver;
						bool flag2;
						if (receiver.Type.IsReferenceType && !LocalRewriter.CanChangeValueBetweenReads(receiver, false, false) && (receiver.Kind != BoundKind.Local || !this.IsStackLocal(((BoundLocal)receiver).LocalSymbol)))
						{
							BoundExpression whenNullOpt = boundLoweredConditionalAccess.WhenNullOpt;
							flag2 = whenNullOpt != null && !whenNullOpt.IsDefaultValue();
						}
						else
						{
							flag2 = true;
						}
						if (!flag2)
						{
							if (!sense)
							{
								this.EmitCondBranch(receiver, ref dest, false);
								this.EmitReceiverRef(receiver, Binder.AddressKind.ReadOnly);
								condition = boundLoweredConditionalAccess.WhenNotNull;
								continue;
							}
							object obj2 = null;
							this.EmitCondBranch(receiver, ref obj2, false);
							this.EmitReceiverRef(receiver, Binder.AddressKind.ReadOnly);
							this.EmitCondBranch(boundLoweredConditionalAccess.WhenNotNull, ref dest, true);
							if (obj2 != null)
							{
								this._builder.MarkLabel(obj2);
								return;
							}
							return;
						}
					}
				}
				IL_0367:
				this.EmitExpression(condition, true);
				TypeSymbol type = condition.Type;
				if (type.IsReferenceType && !type.IsVerifierReference())
				{
					this.EmitBox(type, condition.Syntax);
				}
				ilopCode = (sense ? ILOpCode.Brtrue : ILOpCode.Brfalse);
				dest = dest ?? new object();
				this._builder.EmitBranch(ilopCode, dest, ILOpCode.Nop);
				return;
			}
			if (condition.ConstantValue.IsDefaultValue != sense)
			{
				dest = dest ?? new object();
				this._builder.EmitBranch(ILOpCode.Br, dest, ILOpCode.Nop);
			}
		}

		// Token: 0x060080CC RID: 32972 RVA: 0x001D3F7E File Offset: 0x001D217E
		private void EmitSequenceCondBranch(BoundSequence sequence, ref object dest, bool sense)
		{
			this.DefineLocals(sequence);
			this.EmitSideEffects(sequence);
			this.EmitCondBranch(sequence.Value, ref dest, sense);
			this.FreeLocals(sequence);
		}

		// Token: 0x060080CD RID: 32973 RVA: 0x001D3FA3 File Offset: 0x001D21A3
		private void EmitLabelStatement(BoundLabelStatement boundLabelStatement)
		{
			this._builder.MarkLabel(boundLabelStatement.Label);
		}

		// Token: 0x060080CE RID: 32974 RVA: 0x001D3FB6 File Offset: 0x001D21B6
		private void EmitGotoStatement(BoundGotoStatement boundGotoStatement)
		{
			this._builder.EmitBranch(ILOpCode.Br, boundGotoStatement.Label, ILOpCode.Nop);
		}

		// Token: 0x060080CF RID: 32975 RVA: 0x001D3FCC File Offset: 0x001D21CC
		private bool IsLastBlockInMethod(BoundBlock block)
		{
			if (this._boundBody == block)
			{
				return true;
			}
			BoundStatementList boundStatementList = this._boundBody as BoundStatementList;
			return boundStatementList != null && boundStatementList.Statements.LastOrDefault<BoundStatement>() == block;
		}

		// Token: 0x060080D0 RID: 32976 RVA: 0x001D4004 File Offset: 0x001D2204
		private void EmitBlock(BoundBlock block)
		{
			bool flag = !block.Locals.IsEmpty;
			if (flag)
			{
				this._builder.OpenLocalScope(ScopeType.Variable, null);
				foreach (LocalSymbol localSymbol in block.Locals)
				{
					ImmutableArray<SyntaxReference> declaringSyntaxReferences = localSymbol.DeclaringSyntaxReferences;
					this.DefineLocal(localSymbol, (!declaringSyntaxReferences.IsEmpty) ? ((CSharpSyntaxNode)declaringSyntaxReferences[0].GetSyntax(default(CancellationToken))) : block.Syntax);
				}
			}
			this.EmitStatements(block.Statements);
			if (this._indirectReturnState == CodeGenerator.IndirectReturnState.Needed && this.IsLastBlockInMethod(block))
			{
				this.HandleReturn();
			}
			if (flag)
			{
				foreach (LocalSymbol localSymbol2 in block.Locals)
				{
					this.FreeLocal(localSymbol2);
				}
				this._builder.CloseLocalScope();
			}
		}

		// Token: 0x060080D1 RID: 32977 RVA: 0x001D40F0 File Offset: 0x001D22F0
		private void EmitStatements(ImmutableArray<BoundStatement> statements)
		{
			foreach (BoundStatement boundStatement in statements)
			{
				this.EmitStatement(boundStatement);
			}
		}

		// Token: 0x060080D2 RID: 32978 RVA: 0x001D4120 File Offset: 0x001D2320
		private void EmitScope(BoundScope block)
		{
			this._builder.OpenLocalScope(ScopeType.Variable, null);
			foreach (LocalSymbol localSymbol in block.Locals)
			{
				if (!localSymbol.IsConst && !this.IsStackLocal(localSymbol))
				{
					this._builder.AddLocalToScope(this._builder.LocalSlotManager.GetLocal(localSymbol));
				}
			}
			this.EmitStatements(block.Statements);
			this._builder.CloseLocalScope();
		}

		// Token: 0x060080D3 RID: 32979 RVA: 0x001D41A0 File Offset: 0x001D23A0
		private void EmitStateMachineScope(BoundStateMachineScope scope)
		{
			this._builder.OpenLocalScope(ScopeType.StateMachineVariable, null);
			foreach (StateMachineFieldSymbol stateMachineFieldSymbol in scope.Fields)
			{
				this._builder.DefineUserDefinedStateMachineHoistedLocal(stateMachineFieldSymbol.SlotIndex);
			}
			this.EmitStatement(scope.Statement);
			this._builder.CloseLocalScope();
		}

		// Token: 0x060080D4 RID: 32980 RVA: 0x001D4204 File Offset: 0x001D2404
		private bool ShouldUseIndirectReturn()
		{
			if (this._ilEmitStyle == ILEmitStyle.Debug && this._method.GenerateDebugInfo)
			{
				SyntaxNode methodBodySyntaxOpt = this._methodBodySyntaxOpt;
				if (methodBodySyntaxOpt != null && methodBodySyntaxOpt.IsKind(SyntaxKind.Block))
				{
					return true;
				}
			}
			return this._builder.InExceptionHandler;
		}

		// Token: 0x060080D5 RID: 32981 RVA: 0x001D4241 File Offset: 0x001D2441
		private bool CanHandleReturnLabel(BoundReturnStatement boundReturnStatement)
		{
			if (boundReturnStatement.WasCompilerGenerated)
			{
				if (!boundReturnStatement.Syntax.IsKind(SyntaxKind.Block))
				{
					MethodSymbol method = this._method;
					if (method == null || !method.IsImplicitConstructor)
					{
						return false;
					}
				}
				return !this._builder.InExceptionHandler;
			}
			return false;
		}

		// Token: 0x060080D6 RID: 32982 RVA: 0x001D4284 File Offset: 0x001D2484
		private void EmitReturnStatement(BoundReturnStatement boundReturnStatement)
		{
			BoundExpression expressionOpt = boundReturnStatement.ExpressionOpt;
			if (boundReturnStatement.RefKind == RefKind.None)
			{
				this.EmitExpression(expressionOpt, true);
			}
			else
			{
				this.EmitAddress(expressionOpt, (this._method.RefKind == RefKind.In) ? Binder.AddressKind.ReadOnlyStrict : Binder.AddressKind.Writeable);
			}
			if (this.ShouldUseIndirectReturn())
			{
				if (expressionOpt != null)
				{
					this._builder.EmitLocalStore(this.LazyReturnTemp);
				}
				if (this._indirectReturnState != CodeGenerator.IndirectReturnState.Emitted && this.CanHandleReturnLabel(boundReturnStatement))
				{
					this.HandleReturn();
					return;
				}
				this._builder.EmitBranch(ILOpCode.Br, CodeGenerator.s_returnLabel, ILOpCode.Nop);
				if (this._indirectReturnState == CodeGenerator.IndirectReturnState.NotNeeded)
				{
					this._indirectReturnState = CodeGenerator.IndirectReturnState.Needed;
					return;
				}
			}
			else
			{
				if (this._indirectReturnState == CodeGenerator.IndirectReturnState.Needed && this.CanHandleReturnLabel(boundReturnStatement))
				{
					if (expressionOpt != null)
					{
						this._builder.EmitLocalStore(this.LazyReturnTemp);
					}
					this.HandleReturn();
					return;
				}
				if (expressionOpt != null)
				{
					this._module.Translate(expressionOpt.Type, boundReturnStatement.Syntax, this._diagnostics);
				}
				this._builder.EmitRet(expressionOpt == null);
			}
		}

		// Token: 0x060080D7 RID: 32983 RVA: 0x001D437C File Offset: 0x001D257C
		private void EmitTryStatement(BoundTryStatement statement, bool emitCatchesOnly = false)
		{
			object obj = !emitCatchesOnly && statement.CatchBlocks.Length > 0 && statement.FinallyBlockOpt != null;
			this._builder.OpenLocalScope(ScopeType.TryCatchFinally, null);
			this._builder.OpenLocalScope(ScopeType.Try, null);
			this._tryNestingLevel++;
			object obj2 = obj;
			if (obj2 != null)
			{
				this.EmitTryStatement(statement, true);
			}
			else
			{
				this.EmitBlock(statement.TryBlock);
			}
			this._tryNestingLevel--;
			this._builder.CloseLocalScope();
			if (obj2 == null)
			{
				foreach (BoundCatchBlock boundCatchBlock in statement.CatchBlocks)
				{
					this.EmitCatchBlock(boundCatchBlock);
				}
			}
			if (!emitCatchesOnly && statement.FinallyBlockOpt != null)
			{
				this._builder.OpenLocalScope(statement.PreferFaultHandler ? ScopeType.Fault : ScopeType.Finally, null);
				this.EmitBlock(statement.FinallyBlockOpt);
				this._builder.CloseLocalScope();
				this._builder.CloseLocalScope();
				if (statement.PreferFaultHandler)
				{
					BoundBlock boundBlock = CodeGenerator.FinallyCloner.MakeFinallyClone(statement);
					this.EmitBlock(boundBlock);
					return;
				}
			}
			else
			{
				this._builder.CloseLocalScope();
			}
		}

		// Token: 0x060080D8 RID: 32984 RVA: 0x001D4498 File Offset: 0x001D2698
		private void EmitCatchBlock(BoundCatchBlock catchBlock)
		{
			object obj = null;
			this._builder.AdjustStack(1);
			if (catchBlock.ExceptionFilterOpt == null)
			{
				ITypeReference typeReference;
				if (catchBlock.ExceptionTypeOpt == null)
				{
					ITypeReference specialType = this._module.GetSpecialType(SpecialType.System_Object, catchBlock.Syntax, this._diagnostics);
					typeReference = specialType;
				}
				else
				{
					typeReference = this._module.Translate(catchBlock.ExceptionTypeOpt, catchBlock.Syntax, this._diagnostics);
				}
				ITypeReference typeReference2 = typeReference;
				this._builder.OpenLocalScope(ScopeType.Catch, typeReference2);
				this.RecordAsyncCatchHandlerOffset(catchBlock);
				if (this._emitPdbSequencePoints)
				{
					CatchClauseSyntax catchClauseSyntax = catchBlock.Syntax as CatchClauseSyntax;
					if (catchClauseSyntax != null)
					{
						TextSpan textSpan;
						if (catchClauseSyntax.Declaration == null)
						{
							textSpan = catchClauseSyntax.CatchKeyword.Span;
						}
						else
						{
							textSpan = TextSpan.FromBounds(catchClauseSyntax.SpanStart, catchClauseSyntax.Declaration.Span.End);
						}
						this.EmitSequencePoint(catchBlock.SyntaxTree, textSpan);
					}
				}
			}
			else
			{
				this._builder.OpenLocalScope(ScopeType.Filter, null);
				this.RecordAsyncCatchHandlerOffset(catchBlock);
				object obj2 = new object();
				obj = new object();
				if (catchBlock.ExceptionTypeOpt != null)
				{
					ITypeReference typeReference3 = this._module.Translate(catchBlock.ExceptionTypeOpt, catchBlock.Syntax, this._diagnostics);
					this._builder.EmitOpCode(ILOpCode.Isinst);
					this._builder.EmitToken(typeReference3, catchBlock.Syntax, this._diagnostics, false);
					this._builder.EmitOpCode(ILOpCode.Dup);
					this._builder.EmitBranch(ILOpCode.Brtrue, obj2, ILOpCode.Nop);
					this._builder.EmitOpCode(ILOpCode.Pop);
					this._builder.EmitIntConstant(0);
					this._builder.EmitBranch(ILOpCode.Br, obj, ILOpCode.Nop);
				}
				this._builder.MarkLabel(obj2);
			}
			foreach (LocalSymbol localSymbol in catchBlock.Locals)
			{
				ImmutableArray<SyntaxReference> declaringSyntaxReferences = localSymbol.DeclaringSyntaxReferences;
				SyntaxNode syntaxNode = ((!declaringSyntaxReferences.IsEmpty) ? ((CSharpSyntaxNode)declaringSyntaxReferences[0].GetSyntax(default(CancellationToken))) : catchBlock.Syntax);
				this.DefineLocal(localSymbol, syntaxNode);
			}
			BoundExpression exceptionSourceOpt = catchBlock.ExceptionSourceOpt;
			if (exceptionSourceOpt != null)
			{
				if (!exceptionSourceOpt.Type.IsVerifierReference())
				{
					this._builder.EmitOpCode(ILOpCode.Unbox_any);
					this.EmitSymbolToken(exceptionSourceOpt.Type, exceptionSourceOpt.Syntax);
				}
				BoundExpression boundExpression = exceptionSourceOpt;
				while (boundExpression.Kind == BoundKind.Sequence)
				{
					BoundSequence boundSequence = (BoundSequence)boundExpression;
					this.EmitSideEffects(boundSequence);
					boundExpression = boundSequence.Value;
				}
				BoundKind kind = boundExpression.Kind;
				if (kind != BoundKind.Local)
				{
					if (kind != BoundKind.FieldAccess)
					{
						throw ExceptionUtilities.UnexpectedValue(boundExpression.Kind);
					}
					BoundFieldAccess boundFieldAccess = (BoundFieldAccess)boundExpression;
					StateMachineFieldSymbol stateMachineFieldSymbol = boundFieldAccess.FieldSymbol as StateMachineFieldSymbol;
					if (stateMachineFieldSymbol != null && stateMachineFieldSymbol.SlotIndex >= 0)
					{
						this._builder.DefineUserDefinedStateMachineHoistedLocal(stateMachineFieldSymbol.SlotIndex);
					}
					LocalDefinition localDefinition = this.AllocateTemp(boundExpression.Type, boundExpression.Syntax, LocalSlotConstraints.None);
					this._builder.EmitLocalStore(localDefinition);
					this.EmitReceiverRef(boundFieldAccess.ReceiverOpt, Binder.AddressKind.Writeable);
					this._builder.EmitLocalLoad(localDefinition);
					this.FreeTemp(localDefinition);
					this.EmitFieldStore(boundFieldAccess);
				}
				else
				{
					BoundLocal boundLocal = (BoundLocal)boundExpression;
					if (!this.IsStackLocal(boundLocal.LocalSymbol))
					{
						this._builder.EmitLocalStore(this.GetLocal(boundLocal));
					}
				}
			}
			else
			{
				this._builder.EmitOpCode(ILOpCode.Pop);
			}
			if (catchBlock.ExceptionFilterPrologueOpt != null)
			{
				this.EmitStatements(catchBlock.ExceptionFilterPrologueOpt.Statements);
			}
			if (catchBlock.ExceptionFilterOpt != null)
			{
				this.EmitCondExpr(catchBlock.ExceptionFilterOpt, true);
				this._builder.EmitIntConstant(0);
				this._builder.EmitOpCode(ILOpCode.Cgt_un);
				this._builder.MarkLabel(obj);
				this._builder.MarkFilterConditionEnd();
				this._builder.EmitOpCode(ILOpCode.Pop);
			}
			this.EmitBlock(catchBlock.Body);
			this._builder.CloseLocalScope();
		}

		// Token: 0x060080D9 RID: 32985 RVA: 0x001D4899 File Offset: 0x001D2A99
		private void RecordAsyncCatchHandlerOffset(BoundCatchBlock catchBlock)
		{
			if (catchBlock.IsSynthesizedAsyncCatchAll)
			{
				this._asyncCatchHandlerOffset = this._builder.AllocateILMarker();
			}
		}

		// Token: 0x060080DA RID: 32986 RVA: 0x001D48B4 File Offset: 0x001D2AB4
		private void EmitSwitchDispatch(BoundSwitchDispatch dispatch)
		{
			this.EmitSwitchHeader(dispatch.Expression, Enumerable.ToArray<KeyValuePair<ConstantValue, object>>(dispatch.Cases.Select<ValueTuple<ConstantValue, LabelSymbol>, KeyValuePair<ConstantValue, object>>(([TupleElementNames(new string[] { "value", "label" })] ValueTuple<ConstantValue, LabelSymbol> p) => new KeyValuePair<ConstantValue, object>(p.Item1, p.Item2))), dispatch.DefaultLabel, dispatch.EqualityMethod);
		}

		// Token: 0x060080DB RID: 32987 RVA: 0x001D4908 File Offset: 0x001D2B08
		private void EmitSwitchHeader(BoundExpression expression, KeyValuePair<ConstantValue, object>[] switchCaseLabels, LabelSymbol fallThroughLabel, MethodSymbol equalityMethod)
		{
			LocalDefinition localDefinition = null;
			BoundSequence boundSequence = null;
			if (expression.Kind == BoundKind.Sequence)
			{
				boundSequence = (BoundSequence)expression;
				this.DefineLocals(boundSequence);
				this.EmitSideEffects(boundSequence);
				expression = boundSequence.Value;
			}
			if (expression.Kind == BoundKind.SequencePointExpression)
			{
				BoundSequencePointExpression boundSequencePointExpression = (BoundSequencePointExpression)expression;
				this.EmitSequencePoint(boundSequencePointExpression);
				expression = boundSequencePointExpression.Expression;
			}
			BoundKind kind = expression.Kind;
			LocalOrParameter localOrParameter;
			if (kind != BoundKind.Local)
			{
				if (kind == BoundKind.Parameter)
				{
					BoundParameter boundParameter = (BoundParameter)expression;
					if (boundParameter.ParameterSymbol.RefKind == RefKind.None)
					{
						localOrParameter = CodeGenerator.ParameterSlot(boundParameter);
						goto IL_00EB;
					}
				}
			}
			else
			{
				LocalSymbol localSymbol = ((BoundLocal)expression).LocalSymbol;
				if (localSymbol.RefKind == RefKind.None && !this.IsStackLocal(localSymbol))
				{
					localOrParameter = this.GetLocal(localSymbol);
					goto IL_00EB;
				}
			}
			this.EmitExpression(expression, true);
			localDefinition = this.AllocateTemp(expression.Type, expression.Syntax, LocalSlotConstraints.None);
			this._builder.EmitLocalStore(localDefinition);
			localOrParameter = localDefinition;
			IL_00EB:
			if (expression.Type.SpecialType != SpecialType.System_String)
			{
				this._builder.EmitIntegerSwitchJumpTable(switchCaseLabels, fallThroughLabel, localOrParameter, expression.Type.EnumUnderlyingTypeOrSelf().PrimitiveTypeCode);
			}
			else
			{
				this.EmitStringSwitchJumpTable(switchCaseLabels, fallThroughLabel, localOrParameter, expression.Syntax, equalityMethod);
			}
			if (localDefinition != null)
			{
				this.FreeTemp(localDefinition);
			}
			if (boundSequence != null)
			{
				this.FreeLocals(boundSequence);
			}
		}

		// Token: 0x060080DC RID: 32988 RVA: 0x001D4A54 File Offset: 0x001D2C54
		private void EmitStringSwitchJumpTable(KeyValuePair<ConstantValue, object>[] switchCaseLabels, LabelSymbol fallThroughLabel, LocalOrParameter key, SyntaxNode syntaxNode, MethodSymbol equalityMethod)
		{
			LocalDefinition localDefinition = null;
			if (SwitchStringJumpTableEmitter.ShouldGenerateHashTableSwitch(this._module, switchCaseLabels.Length))
			{
				IReference method = this._module.GetPrivateImplClass(syntaxNode, this._diagnostics).GetMethod("ComputeStringHash");
				if (method != null)
				{
					this._builder.EmitLoad(key);
					this._builder.EmitOpCode(ILOpCode.Call, 0);
					this._builder.EmitToken(method, syntaxNode, this._diagnostics, false);
					NamedTypeSymbol specialType = this._module.Compilation.GetSpecialType(SpecialType.System_UInt32);
					localDefinition = this.AllocateTemp(specialType, syntaxNode, LocalSlotConstraints.None);
					this._builder.EmitLocalStore(localDefinition);
				}
			}
			IReference stringEqualityMethodRef = this._module.Translate(equalityMethod, syntaxNode, this._diagnostics, null, false);
			IMethodReference stringLengthRef = null;
			MethodSymbol methodSymbol = this._module.Compilation.GetSpecialTypeMember(SpecialMember.System_String__Length) as MethodSymbol;
			if (methodSymbol != null && !methodSymbol.HasUseSiteError)
			{
				stringLengthRef = this._module.Translate(methodSymbol, syntaxNode, this._diagnostics, null, false);
			}
			SwitchStringJumpTableEmitter.EmitStringCompareAndBranch emitStringCompareAndBranch = delegate(LocalOrParameter keyArg, ConstantValue stringConstant, object targetLabel)
			{
				if (stringConstant == ConstantValue.Null)
				{
					this._builder.EmitLoad(keyArg);
					this._builder.EmitBranch(ILOpCode.Brfalse, targetLabel, ILOpCode.Brtrue);
					return;
				}
				if (stringConstant.StringValue.Length == 0 && stringLengthRef != null)
				{
					object obj = new object();
					this._builder.EmitLoad(keyArg);
					this._builder.EmitBranch(ILOpCode.Brfalse, obj, ILOpCode.Brtrue);
					this._builder.EmitLoad(keyArg);
					this._builder.EmitOpCode(ILOpCode.Call, 0);
					DiagnosticBag instance = DiagnosticBag.GetInstance();
					this._builder.EmitToken(stringLengthRef, null, instance);
					instance.Free();
					this._builder.EmitBranch(ILOpCode.Brfalse, targetLabel, ILOpCode.Brtrue);
					this._builder.MarkLabel(obj);
					return;
				}
				this.EmitStringCompareAndBranch(key, syntaxNode, stringConstant, targetLabel, stringEqualityMethodRef);
			};
			this._builder.EmitStringSwitchJumpTable(switchCaseLabels, fallThroughLabel, key, localDefinition, emitStringCompareAndBranch, new SwitchStringJumpTableEmitter.GetStringHashCode(SynthesizedStringSwitchHashMethod.ComputeStringHash));
			if (localDefinition != null)
			{
				this.FreeTemp(localDefinition);
			}
		}

		// Token: 0x060080DD RID: 32989 RVA: 0x001D4BCC File Offset: 0x001D2DCC
		private void EmitStringCompareAndBranch(LocalOrParameter key, SyntaxNode syntaxNode, ConstantValue stringConstant, object targetLabel, IReference stringEqualityMethodRef)
		{
			this._builder.EmitLoad(key);
			this._builder.EmitConstantValue(stringConstant);
			this._builder.EmitOpCode(ILOpCode.Call, -1);
			this._builder.EmitToken(stringEqualityMethodRef, syntaxNode, this._diagnostics, false);
			this._builder.EmitBranch(ILOpCode.Brtrue, targetLabel, ILOpCode.Brfalse);
		}

		// Token: 0x060080DE RID: 32990 RVA: 0x001D4C28 File Offset: 0x001D2E28
		private LocalDefinition GetLocal(BoundLocal localExpression)
		{
			LocalSymbol localSymbol = localExpression.LocalSymbol;
			return this.GetLocal(localSymbol);
		}

		// Token: 0x060080DF RID: 32991 RVA: 0x001D4C43 File Offset: 0x001D2E43
		private LocalDefinition GetLocal(LocalSymbol symbol)
		{
			return this._builder.LocalSlotManager.GetLocal(symbol);
		}

		// Token: 0x060080E0 RID: 32992 RVA: 0x001D4C58 File Offset: 0x001D2E58
		private LocalDefinition DefineLocal(LocalSymbol local, SyntaxNode syntaxNode)
		{
			ImmutableArray<bool> immutableArray = ((!local.IsCompilerGenerated && local.Type.ContainsDynamic()) ? CSharpCompilation.DynamicTransformsEncoder.Encode(local.Type, RefKind.None, 0) : ImmutableArray<bool>.Empty);
			ImmutableArray<string> immutableArray2 = ((!local.IsCompilerGenerated && local.Type.ContainsTupleNames()) ? CSharpCompilation.TupleNamesEncoder.Encode(local.Type) : ImmutableArray<string>.Empty);
			if (local.IsConst)
			{
				MetadataConstant metadataConstant = this._module.CreateConstant(local.Type, local.ConstantValue, syntaxNode, this._diagnostics);
				LocalConstantDefinition localConstantDefinition = new LocalConstantDefinition(local.Name, local.Locations.FirstOrDefault<Location>() ?? Location.None, metadataConstant, immutableArray, immutableArray2);
				this._builder.AddLocalConstantToScope(localConstantDefinition);
				return null;
			}
			if (this.IsStackLocal(local))
			{
				return null;
			}
			LocalSlotConstraints localSlotConstraints;
			ITypeReference typeReference2;
			if (local.DeclarationKind == LocalDeclarationKind.FixedVariable && local.IsPinned)
			{
				localSlotConstraints = LocalSlotConstraints.ByRef | LocalSlotConstraints.Pinned;
				TypeSymbol pointedAtType = ((PointerTypeSymbol)local.Type).PointedAtType;
				ITypeReference typeReference;
				if (!pointedAtType.IsVoidType())
				{
					typeReference = this._module.Translate(pointedAtType, syntaxNode, this._diagnostics);
				}
				else
				{
					ITypeReference specialType = this._module.GetSpecialType(SpecialType.System_IntPtr, syntaxNode, this._diagnostics);
					typeReference = specialType;
				}
				typeReference2 = typeReference;
			}
			else
			{
				localSlotConstraints = (local.IsPinned ? LocalSlotConstraints.Pinned : LocalSlotConstraints.None) | ((local.RefKind != RefKind.None) ? LocalSlotConstraints.ByRef : LocalSlotConstraints.None);
				typeReference2 = this._module.Translate(local.Type, syntaxNode, this._diagnostics);
			}
			this._module.GetFakeSymbolTokenForIL(typeReference2, syntaxNode, this._diagnostics);
			LocalDebugId localDebugId;
			string localDebugName = this.GetLocalDebugName(local, out localDebugId);
			LocalDefinition localDefinition = this._builder.LocalSlotManager.DeclareLocal(typeReference2, local, localDebugName, local.SynthesizedKind, localDebugId, local.SynthesizedKind.PdbAttributes(), localSlotConstraints, immutableArray, immutableArray2, local.SynthesizedKind.IsSlotReusable(this._ilEmitStyle != ILEmitStyle.Release));
			bool flag = localDefinition.Name != null;
			if (flag)
			{
				bool flag2 = local.SynthesizedKind == SynthesizedLocalKind.UserDefined;
				if (flag2)
				{
					SyntaxNode scopeDesignatorOpt = local.ScopeDesignatorOpt;
					SyntaxKind? syntaxKind = ((scopeDesignatorOpt != null) ? new SyntaxKind?(scopeDesignatorOpt.Kind()) : default(SyntaxKind?));
					bool flag3;
					if (syntaxKind != null)
					{
						SyntaxKind valueOrDefault = syntaxKind.GetValueOrDefault();
						if (valueOrDefault == SyntaxKind.SwitchSection || valueOrDefault == SyntaxKind.SwitchExpressionArm)
						{
							flag3 = true;
							goto IL_021A;
						}
					}
					flag3 = false;
					IL_021A:
					flag2 = flag3;
				}
				flag = !flag2;
			}
			if (flag)
			{
				this._builder.AddLocalToScope(localDefinition);
			}
			return localDefinition;
		}

		// Token: 0x060080E1 RID: 32993 RVA: 0x001D4EA0 File Offset: 0x001D30A0
		private string GetLocalDebugName(ILocalSymbolInternal local, out LocalDebugId localId)
		{
			localId = LocalDebugId.None;
			if (local.IsImportedFromMetadata)
			{
				return local.Name;
			}
			SynthesizedLocalKind synthesizedKind = local.SynthesizedKind;
			if (!synthesizedKind.IsLongLived() || synthesizedKind == SynthesizedLocalKind.InstrumentationPayload)
			{
				return null;
			}
			if (this._ilEmitStyle == ILEmitStyle.Debug)
			{
				SyntaxNode declaratorSyntax = local.GetDeclaratorSyntax();
				int num = this._method.CalculateLocalSyntaxOffset(LambdaUtilities.GetDeclaratorPosition(declaratorSyntax), declaratorSyntax.SyntaxTree);
				int num2 = this._synthesizedLocalOrdinals.AssignLocalOrdinal(synthesizedKind, num);
				localId = new LocalDebugId(num, num2);
			}
			return local.Name ?? GeneratedNames.MakeSynthesizedLocalName(synthesizedKind, ref this._uniqueNameId);
		}

		// Token: 0x060080E2 RID: 32994 RVA: 0x001D4F35 File Offset: 0x001D3135
		private bool IsSlotReusable(LocalSymbol local)
		{
			return local.SynthesizedKind.IsSlotReusable(this._ilEmitStyle != ILEmitStyle.Release);
		}

		// Token: 0x060080E3 RID: 32995 RVA: 0x001D4F4E File Offset: 0x001D314E
		private void FreeLocal(LocalSymbol local)
		{
			if (local.Name == null && this.IsSlotReusable(local) && !this.IsStackLocal(local))
			{
				this._builder.LocalSlotManager.FreeLocal(local);
			}
		}

		// Token: 0x060080E4 RID: 32996 RVA: 0x001D4F7C File Offset: 0x001D317C
		private LocalDefinition AllocateTemp(TypeSymbol type, SyntaxNode syntaxNode, LocalSlotConstraints slotConstraints = LocalSlotConstraints.None)
		{
			return this._builder.LocalSlotManager.AllocateSlot(this._module.Translate(type, syntaxNode, this._diagnostics), slotConstraints, default(ImmutableArray<bool>), default(ImmutableArray<string>));
		}

		// Token: 0x060080E5 RID: 32997 RVA: 0x001D4FBF File Offset: 0x001D31BF
		private void FreeTemp(LocalDefinition temp)
		{
			this._builder.LocalSlotManager.FreeSlot(temp);
		}

		// Token: 0x060080E6 RID: 32998 RVA: 0x001D4FD2 File Offset: 0x001D31D2
		private void FreeOptTemp(LocalDefinition temp)
		{
			if (temp != null)
			{
				this.FreeTemp(temp);
			}
		}

		// Token: 0x04001E2D RID: 7725
		private readonly MethodSymbol _method;

		// Token: 0x04001E2E RID: 7726
		private readonly SyntaxNode _methodBodySyntaxOpt;

		// Token: 0x04001E2F RID: 7727
		private readonly BoundStatement _boundBody;

		// Token: 0x04001E30 RID: 7728
		private readonly ILBuilder _builder;

		// Token: 0x04001E31 RID: 7729
		private readonly PEModuleBuilder _module;

		// Token: 0x04001E32 RID: 7730
		private readonly DiagnosticBag _diagnostics;

		// Token: 0x04001E33 RID: 7731
		private readonly ILEmitStyle _ilEmitStyle;

		// Token: 0x04001E34 RID: 7732
		private readonly bool _emitPdbSequencePoints;

		// Token: 0x04001E35 RID: 7733
		private readonly HashSet<LocalSymbol> _stackLocals;

		// Token: 0x04001E36 RID: 7734
		private ArrayBuilder<LocalDefinition> _expressionTemps;

		// Token: 0x04001E37 RID: 7735
		private int _tryNestingLevel;

		// Token: 0x04001E38 RID: 7736
		private readonly SynthesizedLocalOrdinalsDispenser _synthesizedLocalOrdinals = new SynthesizedLocalOrdinalsDispenser();

		// Token: 0x04001E39 RID: 7737
		private int _uniqueNameId;

		// Token: 0x04001E3A RID: 7738
		private static readonly object s_returnLabel = new object();

		// Token: 0x04001E3B RID: 7739
		private int _asyncCatchHandlerOffset = -1;

		// Token: 0x04001E3C RID: 7740
		private ArrayBuilder<int> _asyncYieldPoints;

		// Token: 0x04001E3D RID: 7741
		private ArrayBuilder<int> _asyncResumePoints;

		// Token: 0x04001E3E RID: 7742
		private CodeGenerator.IndirectReturnState _indirectReturnState;

		// Token: 0x04001E3F RID: 7743
		private PooledDictionary<object, TextSpan> _savedSequencePoints;

		// Token: 0x04001E40 RID: 7744
		private LocalDefinition _returnTemp;

		// Token: 0x04001E41 RID: 7745
		private bool _sawStackalloc;

		// Token: 0x04001E42 RID: 7746
		private int _recursionDepth;

		// Token: 0x04001E43 RID: 7747
		private static readonly ILOpCode[] s_compOpCodes = new ILOpCode[]
		{
			ILOpCode.Clt,
			ILOpCode.Cgt,
			ILOpCode.Cgt,
			ILOpCode.Clt,
			ILOpCode.Clt_un,
			ILOpCode.Cgt_un,
			ILOpCode.Cgt_un,
			ILOpCode.Clt_un,
			ILOpCode.Clt,
			ILOpCode.Cgt_un,
			ILOpCode.Cgt,
			ILOpCode.Clt_un
		};

		// Token: 0x04001E44 RID: 7748
		private const int IL_OP_CODE_ROW_LENGTH = 4;

		// Token: 0x04001E45 RID: 7749
		private static readonly ILOpCode[] s_condJumpOpCodes = new ILOpCode[]
		{
			ILOpCode.Blt,
			ILOpCode.Ble,
			ILOpCode.Bgt,
			ILOpCode.Bge,
			ILOpCode.Bge,
			ILOpCode.Bgt,
			ILOpCode.Ble,
			ILOpCode.Blt,
			ILOpCode.Blt_un,
			ILOpCode.Ble_un,
			ILOpCode.Bgt_un,
			ILOpCode.Bge_un,
			ILOpCode.Bge_un,
			ILOpCode.Bgt_un,
			ILOpCode.Ble_un,
			ILOpCode.Blt_un,
			ILOpCode.Blt,
			ILOpCode.Ble,
			ILOpCode.Bgt,
			ILOpCode.Bge,
			ILOpCode.Bge_un,
			ILOpCode.Bgt_un,
			ILOpCode.Ble_un,
			ILOpCode.Blt_un
		};

		// Token: 0x02000995 RID: 2453
		private enum IndirectReturnState : byte
		{
			// Token: 0x0400296E RID: 10606
			NotNeeded,
			// Token: 0x0400296F RID: 10607
			Needed,
			// Token: 0x04002970 RID: 10608
			Emitted
		}

		// Token: 0x02000996 RID: 2454
		private enum ArrayInitializerStyle
		{
			// Token: 0x04002972 RID: 10610
			Element,
			// Token: 0x04002973 RID: 10611
			Block,
			// Token: 0x04002974 RID: 10612
			Mixed
		}

		// Token: 0x02000997 RID: 2455
		private struct IndexDesc
		{
			// Token: 0x0600929C RID: 37532 RVA: 0x00200CEB File Offset: 0x001FEEEB
			public IndexDesc(int index, ImmutableArray<BoundExpression> initializers)
			{
				this.Index = index;
				this.Initializers = initializers;
			}

			// Token: 0x04002975 RID: 10613
			public readonly int Index;

			// Token: 0x04002976 RID: 10614
			public readonly ImmutableArray<BoundExpression> Initializers;
		}

		// Token: 0x02000998 RID: 2456
		private class EmitCancelledException : Exception
		{
		}

		// Token: 0x02000999 RID: 2457
		private enum UseKind
		{
			// Token: 0x04002978 RID: 10616
			Unused,
			// Token: 0x04002979 RID: 10617
			UsedAsValue,
			// Token: 0x0400297A RID: 10618
			UsedAsAddress
		}

		// Token: 0x0200099A RID: 2458
		private enum CallKind
		{
			// Token: 0x0400297C RID: 10620
			Call,
			// Token: 0x0400297D RID: 10621
			CallVirt,
			// Token: 0x0400297E RID: 10622
			ConstrainedCallVirt
		}

		// Token: 0x0200099B RID: 2459
		private class FinallyCloner : BoundTreeRewriterWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator
		{
			// Token: 0x0600929E RID: 37534 RVA: 0x00200D03 File Offset: 0x001FEF03
			private FinallyCloner()
			{
			}

			// Token: 0x0600929F RID: 37535 RVA: 0x00200D0B File Offset: 0x001FEF0B
			public static BoundBlock MakeFinallyClone(BoundTryStatement node)
			{
				return (BoundBlock)new CodeGenerator.FinallyCloner().Visit(node.FinallyBlockOpt);
			}

			// Token: 0x060092A0 RID: 37536 RVA: 0x00200D22 File Offset: 0x001FEF22
			public override BoundNode VisitLabelStatement(BoundLabelStatement node)
			{
				return node.Update(this.GetLabelClone(node.Label));
			}

			// Token: 0x060092A1 RID: 37537 RVA: 0x00200D38 File Offset: 0x001FEF38
			public override BoundNode VisitGotoStatement(BoundGotoStatement node)
			{
				GeneratedLabelSymbol labelClone = this.GetLabelClone(node.Label);
				BoundExpression caseExpressionOpt = node.CaseExpressionOpt;
				BoundLabel labelExpressionOpt = node.LabelExpressionOpt;
				return node.Update(labelClone, caseExpressionOpt, labelExpressionOpt);
			}

			// Token: 0x060092A2 RID: 37538 RVA: 0x00200D6C File Offset: 0x001FEF6C
			public override BoundNode VisitConditionalGoto(BoundConditionalGoto node)
			{
				GeneratedLabelSymbol labelClone = this.GetLabelClone(node.Label);
				BoundExpression condition = node.Condition;
				return node.Update(condition, node.JumpIfTrue, labelClone);
			}

			// Token: 0x060092A3 RID: 37539 RVA: 0x00200D9C File Offset: 0x001FEF9C
			public override BoundNode VisitSwitchDispatch(BoundSwitchDispatch node)
			{
				BoundExpression expression = node.Expression;
				GeneratedLabelSymbol labelClone = this.GetLabelClone(node.DefaultLabel);
				ArrayBuilder<ValueTuple<ConstantValue, LabelSymbol>> instance = ArrayBuilder<ValueTuple<ConstantValue, LabelSymbol>>.GetInstance();
				foreach (ValueTuple<ConstantValue, LabelSymbol> valueTuple in node.Cases)
				{
					ConstantValue item = valueTuple.Item1;
					LabelSymbol item2 = valueTuple.Item2;
					instance.Add(new ValueTuple<ConstantValue, LabelSymbol>(item, this.GetLabelClone(item2)));
				}
				return node.Update(expression, instance.ToImmutableAndFree(), labelClone, node.EqualityMethod);
			}

			// Token: 0x060092A4 RID: 37540 RVA: 0x00200E1D File Offset: 0x001FF01D
			public override BoundNode VisitExpressionStatement(BoundExpressionStatement node)
			{
				return node;
			}

			// Token: 0x060092A5 RID: 37541 RVA: 0x00200E20 File Offset: 0x001FF020
			private GeneratedLabelSymbol GetLabelClone(LabelSymbol label)
			{
				Dictionary<LabelSymbol, GeneratedLabelSymbol> dictionary = this._labelClones;
				if (dictionary == null)
				{
					dictionary = (this._labelClones = new Dictionary<LabelSymbol, GeneratedLabelSymbol>());
				}
				GeneratedLabelSymbol generatedLabelSymbol;
				if (!dictionary.TryGetValue(label, ref generatedLabelSymbol))
				{
					generatedLabelSymbol = new GeneratedLabelSymbol("cloned_" + label.Name);
					dictionary.Add(label, generatedLabelSymbol);
				}
				return generatedLabelSymbol;
			}

			// Token: 0x0400297F RID: 10623
			private Dictionary<LabelSymbol, GeneratedLabelSymbol> _labelClones;
		}
	}
}
