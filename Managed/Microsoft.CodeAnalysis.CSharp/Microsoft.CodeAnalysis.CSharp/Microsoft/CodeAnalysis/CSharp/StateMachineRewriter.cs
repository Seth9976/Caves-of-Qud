using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200020F RID: 527
	internal abstract class StateMachineRewriter
	{
		// Token: 0x060025F6 RID: 9718 RVA: 0x000CF9A4 File Offset: 0x000CDBA4
		protected StateMachineRewriter(BoundStatement body, MethodSymbol method, SynthesizedContainer stateMachineType, VariableSlotAllocator slotAllocatorOpt, TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			this.body = body;
			this.method = method;
			this.stateMachineType = stateMachineType;
			this.slotAllocatorOpt = slotAllocatorOpt;
			this.synthesizedLocalOrdinals = new SynthesizedLocalOrdinalsDispenser();
			this.diagnostics = diagnostics;
			this.F = new SyntheticBoundNodeFactory(method, body.Syntax, compilationState, diagnostics);
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060025F7 RID: 9719
		protected abstract bool PreserveInitialParameterValuesAndThreadId { get; }

		// Token: 0x060025F8 RID: 9720
		protected abstract void GenerateControlFields();

		// Token: 0x060025F9 RID: 9721
		protected abstract void InitializeStateMachine(ArrayBuilder<BoundStatement> bodyBuilder, NamedTypeSymbol frameType, LocalSymbol stateMachineLocal);

		// Token: 0x060025FA RID: 9722
		protected abstract BoundStatement GenerateStateMachineCreation(LocalSymbol stateMachineVariable, NamedTypeSymbol frameType, IReadOnlyDictionary<Symbol, CapturedSymbolReplacement> proxies);

		// Token: 0x060025FB RID: 9723
		protected abstract void GenerateMethodImplementations();

		// Token: 0x060025FC RID: 9724 RVA: 0x000CFA00 File Offset: 0x000CDC00
		protected BoundStatement Rewrite()
		{
			if (this.body.HasErrors)
			{
				return this.body;
			}
			this.F.OpenNestedType(this.stateMachineType);
			this.GenerateControlFields();
			if (this.PreserveInitialParameterValuesAndThreadId && this.CanGetThreadId())
			{
				this.initialThreadIdField = this.F.StateMachineField(this.F.SpecialType(SpecialType.System_Int32), GeneratedNames.MakeIteratorCurrentThreadIdFieldName(), false, false);
			}
			if (this.PreserveInitialParameterValuesAndThreadId)
			{
				this.initialParameters = new Dictionary<Symbol, CapturedSymbolReplacement>();
			}
			OrderedSet<Symbol> orderedSet = IteratorAndAsyncCaptureWalker.Analyze(this.F.Compilation, this.method, this.body, this.diagnostics.DiagnosticBag);
			if (this.diagnostics.HasAnyErrors())
			{
				return new BoundBadStatement(this.F.Syntax, ImmutableArray<BoundNode>.Empty, true);
			}
			this.CreateNonReusableLocalProxies(orderedSet, out this.nonReusableLocalProxies, out this.nextFreeHoistedLocalSlot);
			this.hoistedVariables = orderedSet;
			this.GenerateMethodImplementations();
			return this.GenerateKickoffMethodBody();
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x000CFAF4 File Offset: 0x000CDCF4
		private void CreateNonReusableLocalProxies(IEnumerable<Symbol> variablesToHoist, out IReadOnlyDictionary<Symbol, CapturedSymbolReplacement> proxies, out int nextFreeHoistedLocalSlot)
		{
			Dictionary<Symbol, CapturedSymbolReplacement> dictionary = new Dictionary<Symbol, CapturedSymbolReplacement>();
			TypeMap typeMap = this.stateMachineType.TypeMap;
			bool flag = this.F.Compilation.Options.OptimizationLevel == OptimizationLevel.Debug;
			bool flag2 = flag && this.slotAllocatorOpt != null;
			nextFreeHoistedLocalSlot = (flag2 ? this.slotAllocatorOpt.PreviousHoistedLocalSlotCount : 0);
			foreach (Symbol symbol in variablesToHoist)
			{
				if (symbol.Kind == SymbolKind.Local)
				{
					LocalSymbol localSymbol = (LocalSymbol)symbol;
					SynthesizedLocalKind synthesizedKind = localSymbol.SynthesizedKind;
					if (synthesizedKind.MustSurviveStateMachineSuspension() && !localSymbol.IsConst && localSymbol.RefKind == RefKind.None)
					{
						StateMachineFieldSymbol stateMachineFieldSymbol = null;
						if (this.ShouldPreallocateNonReusableProxy(localSymbol))
						{
							TypeSymbol type = typeMap.SubstituteType(localSymbol.Type).Type;
							int num = -1;
							LocalDebugId none;
							if (flag)
							{
								SyntaxNode declaratorSyntax = localSymbol.GetDeclaratorSyntax();
								int num2 = this.method.CalculateLocalSyntaxOffset(LambdaUtilities.GetDeclaratorPosition(declaratorSyntax), declaratorSyntax.SyntaxTree);
								int num3 = this.synthesizedLocalOrdinals.AssignLocalOrdinal(synthesizedKind, num2);
								none = new LocalDebugId(num2, num3);
								int num4;
								if (flag2 && this.slotAllocatorOpt.TryGetPreviousHoistedLocalSlotIndex(declaratorSyntax, this.F.ModuleBuilderOpt.Translate(type, declaratorSyntax, this.diagnostics.DiagnosticBag), synthesizedKind, none, this.diagnostics.DiagnosticBag, out num4))
								{
									num = num4;
								}
							}
							else
							{
								none = LocalDebugId.None;
							}
							if (num == -1)
							{
								int num5 = nextFreeHoistedLocalSlot;
								nextFreeHoistedLocalSlot = num5 + 1;
								num = num5;
							}
							string text = GeneratedNames.MakeHoistedLocalFieldName(synthesizedKind, num, localSymbol.Name);
							stateMachineFieldSymbol = this.F.StateMachineField(type, text, new LocalSlotDebugInfo(synthesizedKind, none), num);
						}
						if (stateMachineFieldSymbol != null)
						{
							dictionary.Add(localSymbol, new CapturedToStateMachineFieldReplacement(stateMachineFieldSymbol, false));
						}
					}
				}
				else
				{
					ParameterSymbol parameterSymbol = (ParameterSymbol)symbol;
					if (parameterSymbol.IsThis)
					{
						NamedTypeSymbol containingType = this.method.ContainingType;
						StateMachineFieldSymbol stateMachineFieldSymbol2 = this.F.StateMachineField(containingType, GeneratedNames.ThisProxyFieldName(), true, true);
						dictionary.Add(parameterSymbol, new CapturedToStateMachineFieldReplacement(stateMachineFieldSymbol2, false));
						if (this.PreserveInitialParameterValuesAndThreadId)
						{
							StateMachineFieldSymbol stateMachineFieldSymbol3 = (containingType.IsStructType() ? this.F.StateMachineField(containingType, GeneratedNames.StateMachineThisParameterProxyName(), true, true) : stateMachineFieldSymbol2);
							this.initialParameters.Add(parameterSymbol, new CapturedToStateMachineFieldReplacement(stateMachineFieldSymbol3, false));
						}
					}
					else
					{
						StateMachineFieldSymbol stateMachineFieldSymbol4 = this.F.StateMachineField(typeMap.SubstituteType(parameterSymbol.Type).Type, parameterSymbol.Name, !this.PreserveInitialParameterValuesAndThreadId, false);
						dictionary.Add(parameterSymbol, new CapturedToStateMachineFieldReplacement(stateMachineFieldSymbol4, false));
						if (this.PreserveInitialParameterValuesAndThreadId)
						{
							StateMachineFieldSymbol stateMachineFieldSymbol5 = this.F.StateMachineField(typeMap.SubstituteType(parameterSymbol.Type).Type, GeneratedNames.StateMachineParameterProxyFieldName(parameterSymbol.Name), true, false);
							this.initialParameters.Add(parameterSymbol, new CapturedToStateMachineFieldReplacement(stateMachineFieldSymbol5, false));
						}
					}
				}
			}
			proxies = dictionary;
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x000CFE24 File Offset: 0x000CE024
		private bool ShouldPreallocateNonReusableProxy(LocalSymbol local)
		{
			SynthesizedLocalKind synthesizedKind = local.SynthesizedKind;
			OptimizationLevel optimizationLevel = this.F.Compilation.Options.OptimizationLevel;
			return (optimizationLevel != OptimizationLevel.Release || synthesizedKind != SynthesizedLocalKind.UserDefined) && !synthesizedKind.IsSlotReusable(optimizationLevel);
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000CFE64 File Offset: 0x000CE064
		private BoundStatement GenerateKickoffMethodBody()
		{
			this.F.CurrentFunction = this.method;
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			NamedTypeSymbol namedTypeSymbol = (this.method.IsGenericMethod ? this.stateMachineType.Construct(this.method.TypeArgumentsWithAnnotations, false) : this.stateMachineType);
			LocalSymbol localSymbol = this.F.SynthesizedLocal(namedTypeSymbol, null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
			this.InitializeStateMachine(instance, namedTypeSymbol, localSymbol);
			IReadOnlyDictionary<Symbol, CapturedSymbolReplacement> readOnlyDictionary;
			if (!this.PreserveInitialParameterValuesAndThreadId)
			{
				readOnlyDictionary = this.nonReusableLocalProxies;
			}
			else
			{
				IReadOnlyDictionary<Symbol, CapturedSymbolReplacement> readOnlyDictionary2 = this.initialParameters;
				readOnlyDictionary = readOnlyDictionary2;
			}
			IReadOnlyDictionary<Symbol, CapturedSymbolReplacement> readOnlyDictionary3 = readOnlyDictionary;
			instance.Add(this.GenerateStateMachineCreation(localSymbol, namedTypeSymbol, readOnlyDictionary3));
			return this.F.Block(ImmutableArray.Create<LocalSymbol>(localSymbol), instance.ToImmutableAndFree());
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x000CFF14 File Offset: 0x000CE114
		protected BoundStatement GenerateParameterStorage(LocalSymbol stateMachineVariable, IReadOnlyDictionary<Symbol, CapturedSymbolReplacement> proxies)
		{
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			CapturedSymbolReplacement capturedSymbolReplacement;
			if (!this.method.IsStatic && proxies.TryGetValue(this.method.ThisParameter, ref capturedSymbolReplacement))
			{
				instance.Add(this.F.Assignment(capturedSymbolReplacement.Replacement(this.F.Syntax, (NamedTypeSymbol frameType1) => this.F.Local(stateMachineVariable)), this.F.This(), false));
			}
			Func<NamedTypeSymbol, BoundExpression> <>9__1;
			foreach (ParameterSymbol parameterSymbol in this.method.Parameters)
			{
				CapturedSymbolReplacement capturedSymbolReplacement2;
				if (proxies.TryGetValue(parameterSymbol, ref capturedSymbolReplacement2))
				{
					ArrayBuilder<BoundStatement> arrayBuilder = instance;
					SyntheticBoundNodeFactory f = this.F;
					CapturedSymbolReplacement capturedSymbolReplacement3 = capturedSymbolReplacement2;
					SyntaxNode syntax = this.F.Syntax;
					Func<NamedTypeSymbol, BoundExpression> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (NamedTypeSymbol frameType1) => this.F.Local(stateMachineVariable));
					}
					arrayBuilder.Add(f.Assignment(capturedSymbolReplacement3.Replacement(syntax, func), this.F.Parameter(parameterSymbol), false));
				}
			}
			return this.F.Block(instance.ToImmutableAndFree());
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x000D0030 File Offset: 0x000CE230
		protected SynthesizedImplementationMethod OpenMethodImplementation(MethodSymbol methodToImplement, string methodName = null, bool hasMethodBodyDependency = false)
		{
			SynthesizedStateMachineDebuggerHiddenMethod synthesizedStateMachineDebuggerHiddenMethod = new SynthesizedStateMachineDebuggerHiddenMethod(methodName, methodToImplement, (StateMachineTypeSymbol)this.F.CurrentType, null, hasMethodBodyDependency);
			this.F.ModuleBuilderOpt.AddSynthesizedDefinition(this.F.CurrentType, synthesizedStateMachineDebuggerHiddenMethod.GetCciAdapter());
			this.F.CurrentFunction = synthesizedStateMachineDebuggerHiddenMethod;
			return synthesizedStateMachineDebuggerHiddenMethod;
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x000D0088 File Offset: 0x000CE288
		protected MethodSymbol OpenPropertyImplementation(MethodSymbol getterToImplement)
		{
			SynthesizedStateMachineProperty synthesizedStateMachineProperty = new SynthesizedStateMachineProperty(getterToImplement, (StateMachineTypeSymbol)this.F.CurrentType);
			this.F.ModuleBuilderOpt.AddSynthesizedDefinition(this.F.CurrentType, synthesizedStateMachineProperty.GetCciAdapter());
			MethodSymbol getMethod = synthesizedStateMachineProperty.GetMethod;
			this.F.ModuleBuilderOpt.AddSynthesizedDefinition(this.F.CurrentType, getMethod.GetCciAdapter());
			this.F.CurrentFunction = getMethod;
			return getMethod;
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x000D0104 File Offset: 0x000CE304
		protected SynthesizedImplementationMethod OpenMoveNextMethodImplementation(MethodSymbol methodToImplement)
		{
			SynthesizedStateMachineMoveNextMethod synthesizedStateMachineMoveNextMethod = new SynthesizedStateMachineMoveNextMethod(methodToImplement, (StateMachineTypeSymbol)this.F.CurrentType);
			this.F.ModuleBuilderOpt.AddSynthesizedDefinition(this.F.CurrentType, synthesizedStateMachineMoveNextMethod.GetCciAdapter());
			this.F.CurrentFunction = synthesizedStateMachineMoveNextMethod;
			return synthesizedStateMachineMoveNextMethod;
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x000D0158 File Offset: 0x000CE358
		protected BoundExpression MakeCurrentThreadId()
		{
			PropertySymbol propertySymbol = (PropertySymbol)this.F.WellKnownMember(WellKnownMember.System_Environment__CurrentManagedThreadId, true);
			if (propertySymbol != null)
			{
				MethodSymbol getMethod = propertySymbol.GetMethod;
				if (getMethod != null)
				{
					return this.F.Call(null, getMethod);
				}
			}
			return this.F.Property(this.F.Property(WellKnownMember.System_Threading_Thread__CurrentThread), WellKnownMember.System_Threading_Thread__ManagedThreadId);
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x000D01B8 File Offset: 0x000CE3B8
		protected SynthesizedImplementationMethod GenerateIteratorGetEnumerator(MethodSymbol getEnumeratorMethod, ref BoundExpression managedThreadId, int initialState)
		{
			SynthesizedImplementationMethod synthesizedImplementationMethod = this.OpenMethodImplementation(getEnumeratorMethod, null, false);
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			LocalSymbol resultVariable = this.F.SynthesizedLocal(this.stateMachineType, null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
			BoundStatement boundStatement = this.F.Assignment(this.F.Local(resultVariable), this.F.New(this.stateMachineType.Constructor, new BoundExpression[] { this.F.Literal(initialState) }), false);
			GeneratedLabelSymbol generatedLabelSymbol = this.F.GenerateLabel("thisInitialized");
			if (this.initialThreadIdField != null)
			{
				managedThreadId = this.MakeCurrentThreadId();
				ArrayBuilder<BoundStatement> instance2 = ArrayBuilder<BoundStatement>.GetInstance(4);
				this.GenerateResetInstance(instance2, initialState);
				instance2.Add(this.F.Assignment(this.F.Local(resultVariable), this.F.This(), false));
				if (this.method.IsStatic || this.method.ThisParameter.Type.IsReferenceType)
				{
					instance2.Add(this.F.Goto(generatedLabelSymbol));
				}
				boundStatement = this.F.If(this.F.LogicalAnd(this.F.IntEqual(this.F.Field(this.F.This(), this.stateField), this.F.Literal(-2)), this.F.IntEqual(this.F.Field(this.F.This(), this.initialThreadIdField), managedThreadId)), this.F.Block(instance2.ToImmutableAndFree()), boundStatement);
			}
			instance.Add(boundStatement);
			Dictionary<Symbol, CapturedSymbolReplacement> dictionary = this.initialParameters;
			IReadOnlyDictionary<Symbol, CapturedSymbolReplacement> readOnlyDictionary = this.nonReusableLocalProxies;
			CapturedSymbolReplacement capturedSymbolReplacement;
			if (!this.method.IsStatic && readOnlyDictionary.TryGetValue(this.method.ThisParameter, ref capturedSymbolReplacement))
			{
				instance.Add(this.F.Assignment(capturedSymbolReplacement.Replacement(this.F.Syntax, (NamedTypeSymbol stateMachineType) => this.F.Local(resultVariable)), dictionary[this.method.ThisParameter].Replacement(this.F.Syntax, (NamedTypeSymbol stateMachineType) => this.F.This()), false));
			}
			instance.Add(this.F.Label(generatedLabelSymbol));
			Func<NamedTypeSymbol, BoundExpression> <>9__2;
			Func<NamedTypeSymbol, BoundExpression> <>9__3;
			foreach (ParameterSymbol parameterSymbol in this.method.Parameters)
			{
				CapturedSymbolReplacement capturedSymbolReplacement2;
				if (readOnlyDictionary.TryGetValue(parameterSymbol, ref capturedSymbolReplacement2))
				{
					CapturedSymbolReplacement capturedSymbolReplacement3 = capturedSymbolReplacement2;
					SyntaxNode syntax = this.F.Syntax;
					Func<NamedTypeSymbol, BoundExpression> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = (NamedTypeSymbol stateMachineType) => this.F.Local(resultVariable));
					}
					BoundExpression boundExpression = capturedSymbolReplacement3.Replacement(syntax, func);
					CapturedSymbolReplacement capturedSymbolReplacement4 = dictionary[parameterSymbol];
					SyntaxNode syntax2 = this.F.Syntax;
					Func<NamedTypeSymbol, BoundExpression> func2;
					if ((func2 = <>9__3) == null)
					{
						func2 = (<>9__3 = (NamedTypeSymbol stateMachineType) => this.F.This());
					}
					BoundExpression boundExpression2 = capturedSymbolReplacement4.Replacement(syntax2, func2);
					BoundStatement boundStatement2 = this.InitializeParameterField(getEnumeratorMethod, parameterSymbol, boundExpression, boundExpression2);
					instance.Add(boundStatement2);
				}
			}
			instance.Add(this.F.Return(this.F.Local(resultVariable)));
			this.F.CloseMethod(this.F.Block(ImmutableArray.Create<LocalSymbol>(resultVariable), instance.ToImmutableAndFree()));
			return synthesizedImplementationMethod;
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x000D0528 File Offset: 0x000CE728
		protected virtual void GenerateResetInstance(ArrayBuilder<BoundStatement> builder, int initialState)
		{
			builder.Add(this.F.Assignment(this.F.Field(this.F.This(), this.stateField), this.F.Literal(initialState), false));
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x000D0564 File Offset: 0x000CE764
		protected virtual BoundStatement InitializeParameterField(MethodSymbol getEnumeratorMethod, ParameterSymbol parameter, BoundExpression resultParameter, BoundExpression parameterProxy)
		{
			return this.F.Assignment(resultParameter, parameterProxy, false);
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x000D0575 File Offset: 0x000CE775
		protected bool CanGetThreadId()
		{
			return this.F.WellKnownMember(WellKnownMember.System_Threading_Thread__ManagedThreadId, true) != null || this.F.WellKnownMember(WellKnownMember.System_Environment__CurrentManagedThreadId, true) != null;
		}

		// Token: 0x04001103 RID: 4355
		protected readonly BoundStatement body;

		// Token: 0x04001104 RID: 4356
		protected readonly MethodSymbol method;

		// Token: 0x04001105 RID: 4357
		protected readonly BindingDiagnosticBag diagnostics;

		// Token: 0x04001106 RID: 4358
		protected readonly SyntheticBoundNodeFactory F;

		// Token: 0x04001107 RID: 4359
		protected readonly SynthesizedContainer stateMachineType;

		// Token: 0x04001108 RID: 4360
		protected readonly VariableSlotAllocator slotAllocatorOpt;

		// Token: 0x04001109 RID: 4361
		protected readonly SynthesizedLocalOrdinalsDispenser synthesizedLocalOrdinals;

		// Token: 0x0400110A RID: 4362
		protected FieldSymbol stateField;

		// Token: 0x0400110B RID: 4363
		protected IReadOnlyDictionary<Symbol, CapturedSymbolReplacement> nonReusableLocalProxies;

		// Token: 0x0400110C RID: 4364
		protected int nextFreeHoistedLocalSlot;

		// Token: 0x0400110D RID: 4365
		protected IOrderedReadOnlySet<Symbol> hoistedVariables;

		// Token: 0x0400110E RID: 4366
		protected Dictionary<Symbol, CapturedSymbolReplacement> initialParameters;

		// Token: 0x0400110F RID: 4367
		protected FieldSymbol initialThreadIdField;
	}
}
