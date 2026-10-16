using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200020D RID: 525
	internal abstract class MethodToStateMachineRewriter : MethodToClassRewriter
	{
		// Token: 0x060025C6 RID: 9670
		protected abstract BoundStatement GenerateReturn(bool finished);

		// Token: 0x060025C7 RID: 9671 RVA: 0x000CE514 File Offset: 0x000CC714
		public MethodToStateMachineRewriter(SyntheticBoundNodeFactory F, MethodSymbol originalMethod, FieldSymbol state, IReadOnlySet<Symbol> hoistedVariables, IReadOnlyDictionary<Symbol, CapturedSymbolReplacement> nonReusableLocalProxies, SynthesizedLocalOrdinalsDispenser synthesizedLocalOrdinals, VariableSlotAllocator slotAllocatorOpt, int nextFreeHoistedLocalSlot, BindingDiagnosticBag diagnostics, bool useFinalizerBookkeeping)
			: base(slotAllocatorOpt, F.CompilationState, diagnostics)
		{
			this.F = F;
			this.stateField = state;
			this.cachedState = F.SynthesizedLocal(F.SpecialType(SpecialType.System_Int32), F.Syntax, false, RefKind.None, SynthesizedLocalKind.StateMachineCachedState);
			this._useFinalizerBookkeeping = useFinalizerBookkeeping;
			this._hasFinalizerState = useFinalizerBookkeeping;
			this.OriginalMethod = originalMethod;
			this._hoistedVariables = hoistedVariables;
			this._synthesizedLocalOrdinals = synthesizedLocalOrdinals;
			this._nextFreeHoistedLocalSlot = nextFreeHoistedLocalSlot;
			foreach (KeyValuePair<Symbol, CapturedSymbolReplacement> keyValuePair in nonReusableLocalProxies)
			{
				this.proxies.Add(keyValuePair.Key, keyValuePair.Value);
			}
			ParameterSymbol thisParameter = originalMethod.ThisParameter;
			CapturedSymbolReplacement capturedSymbolReplacement;
			if (thisParameter != null && thisParameter.Type.IsReferenceType && this.proxies.TryGetValue(thisParameter, ref capturedSymbolReplacement) && F.Compilation.Options.OptimizationLevel == OptimizationLevel.Release)
			{
				BoundExpression boundExpression = capturedSymbolReplacement.Replacement(F.Syntax, (NamedTypeSymbol frameType) => F.This());
				this.cachedThis = F.SynthesizedLocal(boundExpression.Type, F.Syntax, false, RefKind.None, SynthesizedLocalKind.FrameCache);
			}
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x000CE6B4 File Offset: 0x000CC8B4
		protected override bool NeedsProxy(Symbol localOrParameter)
		{
			return this._hoistedVariables.Contains(localOrParameter);
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x060025C9 RID: 9673 RVA: 0x000CE6C2 File Offset: 0x000CC8C2
		protected override TypeMap TypeMap
		{
			get
			{
				return ((SynthesizedContainer)this.F.CurrentType).TypeMap;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x000CE6D9 File Offset: 0x000CC8D9
		protected override MethodSymbol CurrentMethod
		{
			get
			{
				return this.F.CurrentFunction;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x000CE6E6 File Offset: 0x000CC8E6
		protected override NamedTypeSymbol ContainingType
		{
			get
			{
				return this.OriginalMethod.ContainingType;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060025CC RID: 9676 RVA: 0x000CE6F3 File Offset: 0x000CC8F3
		internal IReadOnlySet<Symbol> HoistedVariables
		{
			get
			{
				return this._hoistedVariables;
			}
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000CE6FC File Offset: 0x000CC8FC
		protected override BoundExpression FramePointer(SyntaxNode syntax, NamedTypeSymbol frameClass)
		{
			SyntaxNode syntax2 = this.F.Syntax;
			this.F.Syntax = syntax;
			BoundExpression boundExpression = this.F.This();
			this.F.Syntax = syntax2;
			return boundExpression;
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000CE738 File Offset: 0x000CC938
		protected void AddState(out int stateNumber, out GeneratedLabelSymbol resumeLabel)
		{
			int num = this._nextState;
			this._nextState = num + 1;
			stateNumber = num;
			if (this._useFinalizerBookkeeping && !this._hasFinalizerState)
			{
				num = this._nextState;
				this._nextState = num + 1;
				this._currentFinalizerState = num;
				this._hasFinalizerState = true;
			}
			this.AddState(stateNumber, out resumeLabel);
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000CE790 File Offset: 0x000CC990
		protected void AddState(int stateNumber, out GeneratedLabelSymbol resumeLabel)
		{
			if (this._dispatches == null)
			{
				this._dispatches = new Dictionary<LabelSymbol, List<int>>();
			}
			resumeLabel = this.F.GenerateLabel("stateMachine");
			List<int> list = new List<int>();
			list.Add(stateNumber);
			this._dispatches.Add(resumeLabel, list);
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000CE7E0 File Offset: 0x000CC9E0
		protected BoundStatement Dispatch()
		{
			return this.F.Switch(this.F.Local(this.cachedState), Enumerable.Select<KeyValuePair<LabelSymbol, List<int>>, SyntheticBoundNodeFactory.SyntheticSwitchSection>(Enumerable.OrderBy<KeyValuePair<LabelSymbol, List<int>>, int>(this._dispatches, delegate(KeyValuePair<LabelSymbol, List<int>> kv)
			{
				KeyValuePair<LabelSymbol, List<int>> keyValuePair = kv;
				return keyValuePair.Value[0];
			}), delegate(KeyValuePair<LabelSymbol, List<int>> kv)
			{
				SyntheticBoundNodeFactory f = this.F;
				KeyValuePair<LabelSymbol, List<int>> keyValuePair2 = kv;
				List<int> value = keyValuePair2.Value;
				BoundStatement[] array = new BoundStatement[1];
				int num = 0;
				SyntheticBoundNodeFactory f2 = this.F;
				keyValuePair2 = kv;
				array[num] = f2.Goto(keyValuePair2.Key);
				return f.SwitchSection(value, array);
			}).ToImmutableArray<SyntheticBoundNodeFactory.SyntheticSwitchSection>());
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x000CE84C File Offset: 0x000CCA4C
		private BoundStatement PossibleIteratorScope(ImmutableArray<LocalSymbol> locals, Func<BoundStatement> wrapped)
		{
			if (locals.IsDefaultOrEmpty)
			{
				return wrapped.Invoke();
			}
			ArrayBuilder<StateMachineFieldSymbol> instance = ArrayBuilder<StateMachineFieldSymbol>.GetInstance();
			foreach (LocalSymbol localSymbol in locals)
			{
				if (this.NeedsProxy(localSymbol) && localSymbol.RefKind == RefKind.None)
				{
					bool flag = false;
					CapturedSymbolReplacement capturedSymbolReplacement;
					if (!this.proxies.TryGetValue(localSymbol, ref capturedSymbolReplacement))
					{
						capturedSymbolReplacement = new CapturedToStateMachineFieldReplacement(this.GetOrAllocateReusableHoistedField(this.TypeMap.SubstituteType(localSymbol.Type).Type, out flag, localSymbol), true);
						this.proxies.Add(localSymbol, capturedSymbolReplacement);
					}
					if (localSymbol.SynthesizedKind == SynthesizedLocalKind.UserDefined)
					{
						SyntaxNode scopeDesignatorOpt = localSymbol.ScopeDesignatorOpt;
						if (scopeDesignatorOpt == null || scopeDesignatorOpt.Kind() != SyntaxKind.SwitchSection)
						{
							goto IL_00C8;
						}
					}
					if (localSymbol.SynthesizedKind != SynthesizedLocalKind.LambdaDisplayClass)
					{
						continue;
					}
					IL_00C8:
					if (!flag)
					{
						instance.Add(((CapturedToStateMachineFieldReplacement)capturedSymbolReplacement).HoistedField);
					}
				}
			}
			BoundStatement boundStatement = wrapped.Invoke();
			ArrayBuilder<BoundAssignmentOperator> instance2 = ArrayBuilder<BoundAssignmentOperator>.GetInstance();
			foreach (LocalSymbol localSymbol2 in locals)
			{
				CapturedSymbolReplacement capturedSymbolReplacement2;
				if (this.proxies.TryGetValue(localSymbol2, ref capturedSymbolReplacement2))
				{
					CapturedToStateMachineFieldReplacement capturedToStateMachineFieldReplacement = capturedSymbolReplacement2 as CapturedToStateMachineFieldReplacement;
					if (capturedToStateMachineFieldReplacement != null)
					{
						this.AddVariableCleanup(instance2, capturedToStateMachineFieldReplacement.HoistedField);
						if (capturedSymbolReplacement2.IsReusable)
						{
							this.FreeReusableHoistedField(capturedToStateMachineFieldReplacement.HoistedField);
						}
					}
					else
					{
						foreach (StateMachineFieldSymbol stateMachineFieldSymbol in ((CapturedToExpressionSymbolReplacement)capturedSymbolReplacement2).HoistedFields)
						{
							this.AddVariableCleanup(instance2, stateMachineFieldSymbol);
							if (capturedSymbolReplacement2.IsReusable)
							{
								this.FreeReusableHoistedField(stateMachineFieldSymbol);
							}
						}
					}
				}
			}
			if (instance2.Count != 0)
			{
				SyntheticBoundNodeFactory f2 = this.F;
				BoundStatement[] array = new BoundStatement[2];
				array[0] = boundStatement;
				array[1] = this.F.Block(instance2.SelectAsArray((BoundAssignmentOperator e, SyntheticBoundNodeFactory f) => f.ExpressionStatement(e), this.F));
				boundStatement = f2.Block(array);
			}
			instance2.Free();
			if (instance.Count != 0)
			{
				boundStatement = this.MakeStateMachineScope(instance.ToImmutable(), boundStatement);
			}
			instance.Free();
			return boundStatement;
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x000CEA75 File Offset: 0x000CCC75
		internal BoundBlock MakeStateMachineScope(ImmutableArray<StateMachineFieldSymbol> hoistedLocals, BoundStatement statement)
		{
			return this.F.Block(new BoundStatement[]
			{
				new BoundStateMachineScope(this.F.Syntax, hoistedLocals, statement, false)
			});
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x000CEAA0 File Offset: 0x000CCCA0
		internal static bool TryUnwrapBoundStateMachineScope(ref BoundStatement statement, out ImmutableArray<StateMachineFieldSymbol> hoistedLocals)
		{
			if (statement.Kind == BoundKind.Block)
			{
				ImmutableArray<BoundStatement> statements = ((BoundBlock)statement).Statements;
				if (statements.Length == 1 && statements[0].Kind == BoundKind.StateMachineScope)
				{
					BoundStateMachineScope boundStateMachineScope = (BoundStateMachineScope)statements[0];
					statement = boundStateMachineScope.Statement;
					hoistedLocals = boundStateMachineScope.Fields;
					return true;
				}
			}
			hoistedLocals = ImmutableArray<StateMachineFieldSymbol>.Empty;
			return false;
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x000CEB10 File Offset: 0x000CCD10
		private void AddVariableCleanup(ArrayBuilder<BoundAssignmentOperator> cleanup, FieldSymbol field)
		{
			if (this.MightContainReferences(field.Type))
			{
				cleanup.Add(this.F.AssignmentExpression(this.F.Field(this.F.This(), field), this.F.NullOrDefault(field.Type), false));
			}
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000CEB68 File Offset: 0x000CCD68
		private bool MightContainReferences(TypeSymbol type)
		{
			if (type.IsReferenceType || type.TypeKind == TypeKind.TypeParameter)
			{
				return true;
			}
			if (type.TypeKind != TypeKind.Struct)
			{
				return false;
			}
			if (type.SpecialType == SpecialType.System_TypedReference)
			{
				return true;
			}
			if (type.SpecialType != SpecialType.None)
			{
				return false;
			}
			if (!type.IsFromCompilation(this.CompilationState.ModuleBuilderOpt.Compilation))
			{
				return true;
			}
			foreach (FieldSymbol fieldSymbol in this._emptyStructTypeCache.GetStructInstanceFields(type))
			{
				if (this.MightContainReferences(fieldSymbol.Type))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x000CEC1C File Offset: 0x000CCE1C
		private StateMachineFieldSymbol GetOrAllocateReusableHoistedField(TypeSymbol type, out bool reused, LocalSymbol local = null)
		{
			ArrayBuilder<StateMachineFieldSymbol> arrayBuilder;
			if (this._lazyAvailableReusableHoistedFields != null && this._lazyAvailableReusableHoistedFields.TryGetValue(type, ref arrayBuilder) && arrayBuilder.Count > 0)
			{
				StateMachineFieldSymbol stateMachineFieldSymbol = arrayBuilder.Last();
				arrayBuilder.RemoveLast();
				reused = true;
				return stateMachineFieldSymbol;
			}
			reused = false;
			int nextHoistedFieldId = this._nextHoistedFieldId;
			this._nextHoistedFieldId = nextHoistedFieldId + 1;
			int num = nextHoistedFieldId;
			if (local != null && local.SynthesizedKind == SynthesizedLocalKind.UserDefined)
			{
				string text = GeneratedNames.MakeHoistedLocalFieldName(SynthesizedLocalKind.UserDefined, num, local.Name);
				return this.F.StateMachineField(type, text, SynthesizedLocalKind.UserDefined, num);
			}
			return this.F.StateMachineField(type, GeneratedNames.ReusableHoistedLocalFieldName(num), false, false);
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000CECAC File Offset: 0x000CCEAC
		private void FreeReusableHoistedField(StateMachineFieldSymbol field)
		{
			ArrayBuilder<StateMachineFieldSymbol> arrayBuilder;
			if (this._lazyAvailableReusableHoistedFields == null || !this._lazyAvailableReusableHoistedFields.TryGetValue(field.Type, ref arrayBuilder))
			{
				if (this._lazyAvailableReusableHoistedFields == null)
				{
					this._lazyAvailableReusableHoistedFields = new Dictionary<TypeSymbol, ArrayBuilder<StateMachineFieldSymbol>>(SymbolEqualityComparer.IgnoringDynamicTupleNamesAndNullability);
				}
				this._lazyAvailableReusableHoistedFields.Add(field.Type, arrayBuilder = new ArrayBuilder<StateMachineFieldSymbol>());
			}
			arrayBuilder.Add(field);
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x000CED10 File Offset: 0x000CCF10
		private BoundExpression HoistRefInitialization(SynthesizedLocal local, BoundAssignmentOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Right);
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
			bool flag = false;
			ArrayBuilder<StateMachineFieldSymbol> instance2 = ArrayBuilder<StateMachineFieldSymbol>.GetInstance();
			AwaitExpressionSyntax awaitExpressionSyntax;
			int num;
			if (this.F.Compilation.Options.OptimizationLevel == OptimizationLevel.Debug)
			{
				awaitExpressionSyntax = (AwaitExpressionSyntax)local.GetDeclaratorSyntax();
				num = this.OriginalMethod.CalculateLocalSyntaxOffset(LambdaUtilities.GetDeclaratorPosition(awaitExpressionSyntax), awaitExpressionSyntax.SyntaxTree);
			}
			else
			{
				awaitExpressionSyntax = null;
				num = -1;
			}
			BoundExpression boundExpression2 = this.HoistExpression(boundExpression, awaitExpressionSyntax, num, local.RefKind, instance, instance2, ref flag);
			this.proxies.Add(local, new CapturedToExpressionSymbolReplacement(boundExpression2, instance2.ToImmutableAndFree(), true));
			if (flag)
			{
				TypeSymbol type = this.TypeMap.SubstituteType(local.Type).Type;
				LocalSymbol localSymbol = this.F.SynthesizedLocal(type, null, false, RefKind.Ref, SynthesizedLocalKind.LoweringTemp);
				return this.F.Sequence(ImmutableArray.Create<LocalSymbol>(localSymbol), instance.ToImmutableAndFree(), this.F.AssignmentExpression(this.F.Local(localSymbol), boundExpression2, true));
			}
			if (instance.Count == 0)
			{
				instance.Free();
				return null;
			}
			BoundExpression boundExpression3 = instance.Last();
			instance.RemoveLast();
			return this.F.Sequence(ImmutableArray<LocalSymbol>.Empty, instance.ToImmutableAndFree(), boundExpression3);
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000CEE54 File Offset: 0x000CD054
		private BoundExpression HoistExpression(BoundExpression expr, AwaitExpressionSyntax awaitSyntaxOpt, int syntaxOffset, RefKind refKind, ArrayBuilder<BoundExpression> sideEffects, ArrayBuilder<StateMachineFieldSymbol> hoistedFields, ref bool needsSacrificialEvaluation)
		{
			BoundKind kind = expr.Kind;
			if (kind <= BoundKind.DefaultExpression)
			{
				if (kind == BoundKind.ConditionalOperator)
				{
					BoundConditionalOperator boundConditionalOperator = (BoundConditionalOperator)expr;
					if (refKind != RefKind.None && refKind != RefKind.In)
					{
						this.F.Diagnostics.Add(ErrorCode.ERR_RefConditionalAndAwait, this.F.Syntax.Location);
					}
					refKind = RefKind.None;
					goto IL_01FD;
				}
				if (kind == BoundKind.ArrayAccess)
				{
					BoundArrayAccess boundArrayAccess = (BoundArrayAccess)expr;
					BoundExpression boundExpression = this.HoistExpression(boundArrayAccess.Expression, awaitSyntaxOpt, syntaxOffset, RefKind.None, sideEffects, hoistedFields, ref needsSacrificialEvaluation);
					ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
					foreach (BoundExpression boundExpression2 in boundArrayAccess.Indices)
					{
						instance.Add(this.HoistExpression(boundExpression2, awaitSyntaxOpt, syntaxOffset, RefKind.None, sideEffects, hoistedFields, ref needsSacrificialEvaluation));
					}
					needsSacrificialEvaluation = true;
					return boundArrayAccess.Update(boundExpression, instance.ToImmutableAndFree(), boundArrayAccess.Type);
				}
				if (kind != BoundKind.DefaultExpression)
				{
					goto IL_01FD;
				}
			}
			else if (kind <= BoundKind.BaseReference)
			{
				if (kind != BoundKind.ThisReference && kind != BoundKind.BaseReference)
				{
					goto IL_01FD;
				}
			}
			else
			{
				if (kind == BoundKind.Call)
				{
					BoundCall boundCall = (BoundCall)expr;
					if (refKind != RefKind.None && refKind != RefKind.In)
					{
						this.F.Diagnostics.Add(ErrorCode.ERR_RefReturningCallAndAwait, this.F.Syntax.Location, new object[] { boundCall.Method });
					}
					refKind = RefKind.None;
					goto IL_01FD;
				}
				if (kind != BoundKind.FieldAccess)
				{
					goto IL_01FD;
				}
				BoundFieldAccess boundFieldAccess = (BoundFieldAccess)expr;
				if (boundFieldAccess.FieldSymbol.IsStatic)
				{
					if (refKind != RefKind.None || boundFieldAccess.FieldSymbol.IsReadOnly)
					{
						return expr;
					}
					goto IL_01FD;
				}
				else
				{
					if (refKind != RefKind.None)
					{
						bool flag = !boundFieldAccess.FieldSymbol.ContainingType.IsReferenceType;
						BoundExpression boundExpression3 = this.HoistExpression(boundFieldAccess.ReceiverOpt, awaitSyntaxOpt, syntaxOffset, flag ? refKind : RefKind.None, sideEffects, hoistedFields, ref needsSacrificialEvaluation);
						if (boundExpression3.Kind != BoundKind.ThisReference && !flag)
						{
							needsSacrificialEvaluation = true;
						}
						return this.F.Field(boundExpression3, boundFieldAccess.FieldSymbol);
					}
					goto IL_01FD;
				}
			}
			return expr;
			IL_01FD:
			if (expr.ConstantValue != null)
			{
				return expr;
			}
			if (refKind != RefKind.None)
			{
				throw ExceptionUtilities.UnexpectedValue(expr.Kind);
			}
			TypeSymbol type = expr.Type;
			StateMachineFieldSymbol stateMachineFieldSymbol;
			if (this.F.Compilation.Options.OptimizationLevel == OptimizationLevel.Debug)
			{
				int num = this._synthesizedLocalOrdinals.AssignLocalOrdinal(SynthesizedLocalKind.AwaitByRefSpill, syntaxOffset);
				LocalDebugId localDebugId = new LocalDebugId(syntaxOffset, num);
				int num2;
				if (this.slotAllocatorOpt == null || !this.slotAllocatorOpt.TryGetPreviousHoistedLocalSlotIndex(awaitSyntaxOpt, this.F.ModuleBuilderOpt.Translate(type, awaitSyntaxOpt, this.Diagnostics.DiagnosticBag), SynthesizedLocalKind.AwaitByRefSpill, localDebugId, this.Diagnostics.DiagnosticBag, out num2))
				{
					int nextFreeHoistedLocalSlot = this._nextFreeHoistedLocalSlot;
					this._nextFreeHoistedLocalSlot = nextFreeHoistedLocalSlot + 1;
					num2 = nextFreeHoistedLocalSlot;
				}
				string text = GeneratedNames.MakeHoistedLocalFieldName(SynthesizedLocalKind.AwaitByRefSpill, num2, null);
				stateMachineFieldSymbol = this.F.StateMachineField(expr.Type, text, new LocalSlotDebugInfo(SynthesizedLocalKind.AwaitByRefSpill, localDebugId), num2);
			}
			else
			{
				bool flag2;
				stateMachineFieldSymbol = this.GetOrAllocateReusableHoistedField(type, out flag2, null);
			}
			hoistedFields.Add(stateMachineFieldSymbol);
			BoundFieldAccess boundFieldAccess2 = this.F.Field(this.F.This(), stateMachineFieldSymbol);
			sideEffects.Add(this.F.AssignmentExpression(boundFieldAccess2, expr, false));
			return boundFieldAccess2;
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x000CF188 File Offset: 0x000CD388
		public override BoundNode Visit(BoundNode node)
		{
			if (node == null)
			{
				return node;
			}
			SyntaxNode syntax = this.F.Syntax;
			this.F.Syntax = node.Syntax;
			BoundNode boundNode = base.Visit(node);
			this.F.Syntax = syntax;
			return boundNode;
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x000CF1CC File Offset: 0x000CD3CC
		public override BoundNode VisitBlock(BoundBlock node)
		{
			return this.PossibleIteratorScope(node.Locals, () => (BoundStatement)this.<>n__0(node));
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x000CF20C File Offset: 0x000CD40C
		public override BoundNode VisitScope(BoundScope node)
		{
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			ArrayBuilder<StateMachineFieldSymbol> instance2 = ArrayBuilder<StateMachineFieldSymbol>.GetInstance();
			bool flag = false;
			foreach (LocalSymbol localSymbol in node.Locals)
			{
				LocalSymbol localSymbol2;
				if (base.TryRewriteLocal(localSymbol, out localSymbol2))
				{
					instance.Add(localSymbol2);
					flag |= localSymbol != localSymbol2;
				}
				else
				{
					instance2.Add(((CapturedToStateMachineFieldReplacement)this.proxies[localSymbol]).HoistedField);
				}
			}
			ImmutableArray<BoundStatement> immutableArray = base.VisitList<BoundStatement>(node.Statements);
			if (instance2.Count != 0)
			{
				BoundStatement boundStatement;
				if (instance.Count == 0)
				{
					instance.Free();
					boundStatement = new BoundStatementList(node.Syntax, immutableArray, false);
				}
				else
				{
					boundStatement = node.Update(instance.ToImmutableAndFree(), immutableArray);
				}
				return this.MakeStateMachineScope(instance2.ToImmutable(), boundStatement);
			}
			instance2.Free();
			ImmutableArray<LocalSymbol> immutableArray2;
			if (flag)
			{
				immutableArray2 = instance.ToImmutableAndFree();
			}
			else
			{
				instance.Free();
				immutableArray2 = node.Locals;
			}
			return node.Update(immutableArray2, immutableArray);
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x000CF307 File Offset: 0x000CD507
		public override BoundNode VisitForStatement(BoundForStatement node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x000CF30E File Offset: 0x000CD50E
		public override BoundNode VisitUsingStatement(BoundUsingStatement node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x000CF318 File Offset: 0x000CD518
		public override BoundNode VisitExpressionStatement(BoundExpressionStatement node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			if (boundExpression != null)
			{
				return node.Update(boundExpression);
			}
			return null;
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x000CF344 File Offset: 0x000CD544
		public override BoundNode VisitAssignmentOperator(BoundAssignmentOperator node)
		{
			if (node.Left.Kind != BoundKind.Local)
			{
				return base.VisitAssignmentOperator(node);
			}
			LocalSymbol localSymbol = ((BoundLocal)node.Left).LocalSymbol;
			if (!this.NeedsProxy(localSymbol))
			{
				return base.VisitAssignmentOperator(node);
			}
			if (this.proxies.ContainsKey(localSymbol))
			{
				return base.VisitAssignmentOperator(node);
			}
			return this.HoistRefInitialization((SynthesizedLocal)localSymbol, node);
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000CF3B0 File Offset: 0x000CD5B0
		public override BoundNode VisitTryStatement(BoundTryStatement node)
		{
			Dictionary<LabelSymbol, List<int>> dictionary = this._dispatches;
			int currentFinalizerState = this._currentFinalizerState;
			bool hasFinalizerState = this._hasFinalizerState;
			this._dispatches = null;
			this._currentFinalizerState = -1;
			this._hasFinalizerState = false;
			BoundBlock boundBlock = this.F.Block(new BoundStatement[] { (BoundStatement)this.Visit(node.TryBlock) });
			GeneratedLabelSymbol generatedLabelSymbol = null;
			if (this._dispatches != null)
			{
				generatedLabelSymbol = this.F.GenerateLabel("tryDispatch");
				if (this._hasFinalizerState)
				{
					GeneratedLabelSymbol generatedLabelSymbol2 = this.F.GenerateLabel("finalizer");
					Dictionary<LabelSymbol, List<int>> dispatches = this._dispatches;
					LabelSymbol labelSymbol = generatedLabelSymbol2;
					List<int> list = new List<int>();
					list.Add(this._currentFinalizerState);
					dispatches.Add(labelSymbol, list);
					GeneratedLabelSymbol generatedLabelSymbol3 = this.F.GenerateLabel("skipFinalizer");
					boundBlock = this.F.Block(new BoundStatement[]
					{
						this.F.HiddenSequencePoint(null),
						this.Dispatch(),
						this.F.Goto(generatedLabelSymbol3),
						this.F.Label(generatedLabelSymbol2),
						this.GenerateSetBothStates(-1),
						this.GenerateReturn(false),
						this.F.Label(generatedLabelSymbol3),
						boundBlock
					});
				}
				else
				{
					boundBlock = this.F.Block(new BoundStatement[]
					{
						this.F.HiddenSequencePoint(null),
						this.Dispatch(),
						boundBlock
					});
				}
				if (dictionary == null)
				{
					dictionary = new Dictionary<LabelSymbol, List<int>>();
				}
				dictionary.Add(generatedLabelSymbol, new List<int>(Enumerable.Select(Enumerable.OrderBy(Enumerable.SelectMany(this._dispatches.Values, (List<int> kv) => kv, (List<int> kv, int n) => new { kv, n }), <>h__TransparentIdentifier0 => <>h__TransparentIdentifier0.n), <>h__TransparentIdentifier0 => <>h__TransparentIdentifier0.n)));
			}
			this._hasFinalizerState = hasFinalizerState;
			this._currentFinalizerState = currentFinalizerState;
			this._dispatches = dictionary;
			ImmutableArray<BoundCatchBlock> immutableArray = base.VisitList<BoundCatchBlock>(node.CatchBlocks);
			BoundBlock boundBlock2 = ((node.FinallyBlockOpt == null) ? null : this.F.Block(new BoundStatement[]
			{
				this.F.HiddenSequencePoint(null),
				this.F.If(this.ShouldEnterFinallyBlock(), this.VisitFinally(node.FinallyBlockOpt), null),
				this.F.HiddenSequencePoint(null)
			}));
			BoundStatement boundStatement = node.Update(boundBlock, immutableArray, boundBlock2, node.FinallyLabelOpt, node.PreferFaultHandler);
			if (generatedLabelSymbol != null)
			{
				boundStatement = this.F.Block(new BoundStatement[]
				{
					this.F.HiddenSequencePoint(null),
					this.F.Label(generatedLabelSymbol),
					boundStatement
				});
			}
			return boundStatement;
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x000CF69E File Offset: 0x000CD89E
		protected virtual BoundBlock VisitFinally(BoundBlock finallyBlock)
		{
			return (BoundBlock)this.Visit(finallyBlock);
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x000CF6AC File Offset: 0x000CD8AC
		protected virtual BoundBinaryOperator ShouldEnterFinallyBlock()
		{
			return this.F.IntLessThan(this.F.Local(this.cachedState), this.F.Literal(0));
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x000CF6D8 File Offset: 0x000CD8D8
		protected BoundExpressionStatement GenerateSetBothStates(int stateNumber)
		{
			return this.F.Assignment(this.F.Field(this.F.This(), this.stateField), this.F.AssignmentExpression(this.F.Local(this.cachedState), this.F.Literal(stateNumber), false), false);
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x000CF738 File Offset: 0x000CD938
		protected BoundStatement CacheThisIfNeeded()
		{
			if (this.cachedThis != null)
			{
				BoundExpression boundExpression = this.proxies[this.OriginalMethod.ThisParameter].Replacement(this.F.Syntax, (NamedTypeSymbol frameType) => this.F.This());
				return this.F.Assignment(this.F.Local(this.cachedThis), boundExpression, false);
			}
			return this.F.StatementList();
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x000CF7AC File Offset: 0x000CD9AC
		public sealed override BoundNode VisitThisReference(BoundThisReference node)
		{
			if (this.cachedThis != null)
			{
				return this.F.Local(this.cachedThis);
			}
			ParameterSymbol thisParameter = this.OriginalMethod.ThisParameter;
			CapturedSymbolReplacement capturedSymbolReplacement;
			if (thisParameter == null || !this.proxies.TryGetValue(thisParameter, ref capturedSymbolReplacement))
			{
				return node.Update(this.VisitType(node.Type));
			}
			return capturedSymbolReplacement.Replacement(this.F.Syntax, (NamedTypeSymbol frameType) => this.F.This());
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x000CF824 File Offset: 0x000CDA24
		public override BoundNode VisitBaseReference(BoundBaseReference node)
		{
			if (this.cachedThis != null)
			{
				return this.F.Local(this.cachedThis);
			}
			return this.proxies[this.OriginalMethod.ThisParameter].Replacement(this.F.Syntax, (NamedTypeSymbol frameType) => this.F.This());
		}

		// Token: 0x040010EF RID: 4335
		internal readonly MethodSymbol OriginalMethod;

		// Token: 0x040010F0 RID: 4336
		private readonly bool _useFinalizerBookkeeping;

		// Token: 0x040010F1 RID: 4337
		protected readonly SyntheticBoundNodeFactory F;

		// Token: 0x040010F2 RID: 4338
		protected readonly FieldSymbol stateField;

		// Token: 0x040010F3 RID: 4339
		protected readonly LocalSymbol cachedState;

		// Token: 0x040010F4 RID: 4340
		protected readonly LocalSymbol cachedThis;

		// Token: 0x040010F5 RID: 4341
		private int _nextState;

		// Token: 0x040010F6 RID: 4342
		private Dictionary<LabelSymbol, List<int>> _dispatches = new Dictionary<LabelSymbol, List<int>>();

		// Token: 0x040010F7 RID: 4343
		private bool _hasFinalizerState = true;

		// Token: 0x040010F8 RID: 4344
		private int _currentFinalizerState = -1;

		// Token: 0x040010F9 RID: 4345
		private Dictionary<TypeSymbol, ArrayBuilder<StateMachineFieldSymbol>> _lazyAvailableReusableHoistedFields;

		// Token: 0x040010FA RID: 4346
		private int _nextHoistedFieldId = 1;

		// Token: 0x040010FB RID: 4347
		private readonly EmptyStructTypeCache _emptyStructTypeCache = EmptyStructTypeCache.CreateNeverEmpty();

		// Token: 0x040010FC RID: 4348
		private readonly IReadOnlySet<Symbol> _hoistedVariables;

		// Token: 0x040010FD RID: 4349
		private readonly SynthesizedLocalOrdinalsDispenser _synthesizedLocalOrdinals;

		// Token: 0x040010FE RID: 4350
		private int _nextFreeHoistedLocalSlot;
	}
}
