using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000201 RID: 513
	internal class IteratorRewriter : StateMachineRewriter
	{
		// Token: 0x06002399 RID: 9113 RVA: 0x000B990B File Offset: 0x000B7B0B
		private IteratorRewriter(BoundStatement body, MethodSymbol method, bool isEnumerable, IteratorStateMachine stateMachineType, VariableSlotAllocator slotAllocatorOpt, TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
			: base(body, method, stateMachineType, slotAllocatorOpt, compilationState, diagnostics)
		{
			this._elementType = stateMachineType.ElementType;
			this._isEnumerable = isEnumerable;
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000B9934 File Offset: 0x000B7B34
		internal static BoundStatement Rewrite(BoundStatement body, MethodSymbol method, int methodOrdinal, VariableSlotAllocator slotAllocatorOpt, TypeCompilationState compilationState, BindingDiagnosticBag diagnostics, out IteratorStateMachine stateMachineType)
		{
			TypeWithAnnotations iteratorElementTypeWithAnnotations = method.IteratorElementTypeWithAnnotations;
			if (iteratorElementTypeWithAnnotations.IsDefault || method.IsAsync)
			{
				stateMachineType = null;
				return body;
			}
			SpecialType specialType = method.ReturnType.OriginalDefinition.SpecialType;
			bool flag;
			if (specialType - SpecialType.System_Collections_IEnumerable > 1)
			{
				if (specialType - SpecialType.System_Collections_IEnumerator > 1)
				{
					throw ExceptionUtilities.UnexpectedValue(method.ReturnType.OriginalDefinition.SpecialType);
				}
				flag = false;
			}
			else
			{
				flag = true;
			}
			stateMachineType = new IteratorStateMachine(slotAllocatorOpt, compilationState, method, methodOrdinal, flag, iteratorElementTypeWithAnnotations);
			compilationState.ModuleBuilderOpt.CompilationState.SetStateMachineType(method, stateMachineType);
			IteratorRewriter iteratorRewriter = new IteratorRewriter(body, method, flag, stateMachineType, slotAllocatorOpt, compilationState, diagnostics);
			if (!iteratorRewriter.VerifyPresenceOfRequiredAPIs())
			{
				return body;
			}
			return iteratorRewriter.Rewrite();
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000B99E8 File Offset: 0x000B7BE8
		protected bool VerifyPresenceOfRequiredAPIs()
		{
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(true, this.diagnostics.AccumulatesDependencies);
			this.EnsureSpecialType(SpecialType.System_Int32, instance);
			this.EnsureSpecialType(SpecialType.System_IDisposable, instance);
			this.EnsureSpecialMember(SpecialMember.System_IDisposable__Dispose, instance);
			this.EnsureSpecialType(SpecialType.System_Collections_IEnumerator, instance);
			this.EnsureSpecialPropertyGetter(SpecialMember.System_Collections_IEnumerator__Current, instance);
			this.EnsureSpecialMember(SpecialMember.System_Collections_IEnumerator__MoveNext, instance);
			this.EnsureSpecialMember(SpecialMember.System_Collections_IEnumerator__Reset, instance);
			this.EnsureSpecialType(SpecialType.System_Collections_Generic_IEnumerator_T, instance);
			this.EnsureSpecialPropertyGetter(SpecialMember.System_Collections_Generic_IEnumerator_T__Current, instance);
			if (this._isEnumerable)
			{
				this.EnsureSpecialType(SpecialType.System_Collections_IEnumerable, instance);
				this.EnsureSpecialMember(SpecialMember.System_Collections_IEnumerable__GetEnumerator, instance);
				this.EnsureSpecialType(SpecialType.System_Collections_Generic_IEnumerable_T, instance);
				this.EnsureSpecialMember(SpecialMember.System_Collections_Generic_IEnumerable_T__GetEnumerator, instance);
			}
			bool flag = instance.HasAnyErrors();
			if (!flag)
			{
				this.diagnostics.AddDependencies(instance, false);
			}
			else
			{
				this.diagnostics.AddRange(instance, false);
			}
			instance.Free();
			return !flag;
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000B9AB8 File Offset: 0x000B7CB8
		private Symbol EnsureSpecialMember(SpecialMember member, BindingDiagnosticBag bag)
		{
			Symbol symbol;
			Binder.TryGetSpecialTypeMember<Symbol>(this.F.Compilation, member, this.body.Syntax, bag, out symbol);
			return symbol;
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000B9AE6 File Offset: 0x000B7CE6
		private void EnsureSpecialType(SpecialType type, BindingDiagnosticBag bag)
		{
			Binder.GetSpecialType(this.F.Compilation, type, this.body.Syntax, bag);
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000B9B08 File Offset: 0x000B7D08
		private void EnsureSpecialPropertyGetter(SpecialMember member, BindingDiagnosticBag bag)
		{
			PropertySymbol propertySymbol = (PropertySymbol)this.EnsureSpecialMember(member, bag);
			if (propertySymbol != null)
			{
				MethodSymbol getMethod = propertySymbol.GetMethod;
				if (getMethod == null)
				{
					Binder.Error(bag, ErrorCode.ERR_PropertyLacksGet, this.body.Syntax, new object[] { propertySymbol });
					return;
				}
				bag.ReportUseSite(getMethod, this.body.Syntax.Location);
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x0600239F RID: 9119 RVA: 0x000B9B6E File Offset: 0x000B7D6E
		protected override bool PreserveInitialParameterValuesAndThreadId
		{
			get
			{
				return this._isEnumerable;
			}
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x000B9B78 File Offset: 0x000B7D78
		protected override void GenerateControlFields()
		{
			this.stateField = this.F.StateMachineField(this.F.SpecialType(SpecialType.System_Int32), GeneratedNames.MakeStateMachineStateFieldName(), false, false);
			this._currentField = this.F.StateMachineField(this._elementType, GeneratedNames.MakeIteratorCurrentFieldName(), false, false);
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000B9BC8 File Offset: 0x000B7DC8
		protected override void GenerateMethodImplementations()
		{
			try
			{
				BoundExpression boundExpression = null;
				this.GenerateEnumeratorImplementation();
				if (this._isEnumerable)
				{
					this.GenerateEnumerableImplementation(ref boundExpression);
				}
				this.GenerateConstructor(boundExpression);
			}
			catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
			{
				this.diagnostics.Add(missingPredefinedMember.Diagnostic);
			}
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x000B9C1C File Offset: 0x000B7E1C
		private void GenerateEnumeratorImplementation()
		{
			MethodSymbol methodSymbol = this.F.SpecialMethod(SpecialMember.System_IDisposable__Dispose);
			MethodSymbol methodSymbol2 = this.F.SpecialMethod(SpecialMember.System_Collections_IEnumerator__MoveNext);
			MethodSymbol methodSymbol3 = this.F.SpecialMethod(SpecialMember.System_Collections_IEnumerator__Reset);
			MethodSymbol getMethod = this.F.SpecialProperty(SpecialMember.System_Collections_IEnumerator__Current).GetMethod;
			NamedTypeSymbol namedTypeSymbol = this.F.SpecialType(SpecialType.System_Collections_Generic_IEnumerator_T).Construct(ImmutableArray.Create<TypeWithAnnotations>(this._elementType));
			MethodSymbol methodSymbol4 = this.F.SpecialProperty(SpecialMember.System_Collections_Generic_IEnumerator_T__Current).GetMethod.AsMember(namedTypeSymbol);
			SynthesizedImplementationMethod synthesizedImplementationMethod = base.OpenMethodImplementation(methodSymbol, null, true);
			SynthesizedImplementationMethod synthesizedImplementationMethod2 = base.OpenMoveNextMethodImplementation(methodSymbol2);
			this.GenerateMoveNextAndDispose(synthesizedImplementationMethod2, synthesizedImplementationMethod);
			base.OpenPropertyImplementation(methodSymbol4);
			this.F.CloseMethod(this.F.Return(this.F.Field(this.F.This(), this._currentField)));
			base.OpenMethodImplementation(methodSymbol3, null, false);
			this.F.CloseMethod(this.F.Throw(this.F.New(this.F.WellKnownType(WellKnownType.System_NotSupportedException), Array.Empty<BoundExpression>())));
			base.OpenPropertyImplementation(getMethod);
			this.F.CloseMethod(this.F.Return(this.F.Field(this.F.This(), this._currentField)));
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000B9D74 File Offset: 0x000B7F74
		private void GenerateEnumerableImplementation(ref BoundExpression managedThreadId)
		{
			MethodSymbol methodSymbol = this.F.SpecialMethod(SpecialMember.System_Collections_IEnumerable__GetEnumerator);
			NamedTypeSymbol namedTypeSymbol = this.F.SpecialType(SpecialType.System_Collections_Generic_IEnumerable_T).Construct(new TypeSymbol[] { this._elementType.Type });
			MethodSymbol methodSymbol2 = this.F.SpecialMethod(SpecialMember.System_Collections_Generic_IEnumerable_T__GetEnumerator).AsMember(namedTypeSymbol);
			SynthesizedImplementationMethod synthesizedImplementationMethod = base.GenerateIteratorGetEnumerator(methodSymbol2, ref managedThreadId, 0);
			base.OpenMethodImplementation(methodSymbol, null, false);
			this.F.CloseMethod(this.F.Return(this.F.Call(this.F.This(), synthesizedImplementationMethod)));
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000B9E0C File Offset: 0x000B800C
		private void GenerateConstructor(BoundExpression managedThreadId)
		{
			this.F.CurrentFunction = this.stateMachineType.Constructor;
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			instance.Add(this.F.BaseInitialization());
			instance.Add(this.F.Assignment(this.F.Field(this.F.This(), this.stateField), this.F.Parameter(this.F.CurrentFunction.Parameters[0]), false));
			if (managedThreadId != null)
			{
				instance.Add(this.F.Assignment(this.F.Field(this.F.This(), this.initialThreadIdField), managedThreadId, false));
			}
			instance.Add(this.F.Return(null));
			this.F.CloseMethod(this.F.Block(instance.ToImmutableAndFree()));
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x000B9EFC File Offset: 0x000B80FC
		protected override void InitializeStateMachine(ArrayBuilder<BoundStatement> bodyBuilder, NamedTypeSymbol frameType, LocalSymbol stateMachineLocal)
		{
			int num = (this._isEnumerable ? (-2) : 0);
			bodyBuilder.Add(this.F.Assignment(this.F.Local(stateMachineLocal), this.F.New(this.stateMachineType.Constructor.AsMember(frameType), new BoundExpression[] { this.F.Literal(num) }), false));
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x000B9F68 File Offset: 0x000B8168
		protected override BoundStatement GenerateStateMachineCreation(LocalSymbol stateMachineVariable, NamedTypeSymbol frameType, IReadOnlyDictionary<Symbol, CapturedSymbolReplacement> proxies)
		{
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			instance.Add(base.GenerateParameterStorage(stateMachineVariable, proxies));
			instance.Add(this.F.Return(this.F.Local(stateMachineVariable)));
			return this.F.Block(instance.ToImmutableAndFree());
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x000B9FB8 File Offset: 0x000B81B8
		private void GenerateMoveNextAndDispose(SynthesizedImplementationMethod moveNextMethod, SynthesizedImplementationMethod disposeMethod)
		{
			new IteratorMethodToStateMachineRewriter(this.F, this.method, this.stateField, this._currentField, this.hoistedVariables, this.nonReusableLocalProxies, this.synthesizedLocalOrdinals, this.slotAllocatorOpt, this.nextFreeHoistedLocalSlot, this.diagnostics).GenerateMoveNextAndDispose(this.body, moveNextMethod, disposeMethod);
		}

		// Token: 0x040010C0 RID: 4288
		private readonly TypeWithAnnotations _elementType;

		// Token: 0x040010C1 RID: 4289
		private readonly bool _isEnumerable;

		// Token: 0x040010C2 RID: 4290
		private FieldSymbol _currentField;
	}
}
