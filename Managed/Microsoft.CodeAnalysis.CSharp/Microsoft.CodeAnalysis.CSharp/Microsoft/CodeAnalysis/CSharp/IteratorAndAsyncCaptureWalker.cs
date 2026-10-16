using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200020C RID: 524
	internal sealed class IteratorAndAsyncCaptureWalker : DefiniteAssignmentPass
	{
		// Token: 0x060025B2 RID: 9650 RVA: 0x000CDEB3 File Offset: 0x000CC0B3
		private IteratorAndAsyncCaptureWalker(CSharpCompilation compilation, MethodSymbol method, BoundNode node, HashSet<Symbol> initiallyAssignedVariables)
			: base(compilation, method, node, EmptyStructTypeCache.CreateNeverEmpty(), true, initiallyAssignedVariables)
		{
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x000CDEDC File Offset: 0x000CC0DC
		public static OrderedSet<Symbol> Analyze(CSharpCompilation compilation, MethodSymbol method, BoundNode node, DiagnosticBag diagnostics)
		{
			HashSet<Symbol> hashSet = UnassignedVariablesWalker.Analyze(compilation, method, node, true);
			IteratorAndAsyncCaptureWalker iteratorAndAsyncCaptureWalker = new IteratorAndAsyncCaptureWalker(compilation, method, node, hashSet);
			iteratorAndAsyncCaptureWalker._convertInsufficientExecutionStackExceptionToCancelledByStackGuardException = true;
			bool flag = false;
			iteratorAndAsyncCaptureWalker.Analyze(ref flag, default(Optional<DefiniteAssignmentPass.LocalState>));
			if (!method.IsStatic && method.ContainingType.TypeKind == TypeKind.Struct)
			{
				iteratorAndAsyncCaptureWalker.CaptureVariable(method.ThisParameter, node.Syntax);
			}
			MultiDictionary<Symbol, SyntaxNode> lazyDisallowedCaptures = iteratorAndAsyncCaptureWalker._lazyDisallowedCaptures;
			ArrayBuilder<LocalDataFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.VariableIdentifier> variableBySlot = iteratorAndAsyncCaptureWalker.variableBySlot;
			if (lazyDisallowedCaptures != null)
			{
				foreach (KeyValuePair<Symbol, MultiDictionary<Symbol, SyntaxNode>.ValueSet> keyValuePair in lazyDisallowedCaptures)
				{
					Symbol key = keyValuePair.Key;
					TypeSymbol typeSymbol = ((key.Kind == SymbolKind.Local) ? ((LocalSymbol)key).Type : ((ParameterSymbol)key).Type);
					SynthesizedLocal synthesizedLocal = key as SynthesizedLocal;
					if (synthesizedLocal != null && synthesizedLocal.SynthesizedKind == SynthesizedLocalKind.Spill)
					{
						diagnostics.Add(ErrorCode.ERR_ByRefTypeAndAwait, synthesizedLocal.Locations[0], new object[] { synthesizedLocal.TypeWithAnnotations });
					}
					else
					{
						foreach (SyntaxNode syntaxNode in keyValuePair.Value)
						{
							CSharpSyntaxNode csharpSyntaxNode = (CSharpSyntaxNode)syntaxNode;
							diagnostics.Add(ErrorCode.ERR_SpecialByRefInLambda, csharpSyntaxNode.Location, new object[] { typeSymbol });
						}
					}
				}
			}
			OrderedSet<Symbol> orderedSet = new OrderedSet<Symbol>();
			if (compilation.Options.OptimizationLevel != OptimizationLevel.Release)
			{
				foreach (LocalDataFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.VariableIdentifier variableIdentifier in variableBySlot)
				{
					Symbol symbol = variableIdentifier.Symbol;
					if (symbol != null && IteratorAndAsyncCaptureWalker.HoistInDebugBuild(symbol))
					{
						orderedSet.Add(symbol);
					}
				}
			}
			orderedSet.AddRange(iteratorAndAsyncCaptureWalker._variablesToHoist);
			iteratorAndAsyncCaptureWalker.Free();
			return orderedSet;
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x000CE0DC File Offset: 0x000CC2DC
		private static bool HoistInDebugBuild(Symbol symbol)
		{
			ParameterSymbol parameterSymbol = symbol as ParameterSymbol;
			bool flag;
			if (parameterSymbol == null)
			{
				LocalSymbol localSymbol = symbol as LocalSymbol;
				if (localSymbol != null && !localSymbol.IsConst && !localSymbol.IsPinned)
				{
					if (!localSymbol.IsRef)
					{
						return localSymbol.SynthesizedKind.MustSurviveStateMachineSuspension() && !localSymbol.Type.IsRestrictedType(false);
					}
				}
				flag = false;
			}
			else
			{
				flag = !parameterSymbol.Type.IsRestrictedType(false);
			}
			return flag;
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x000CE150 File Offset: 0x000CC350
		private void MarkLocalsUnassigned()
		{
			for (int i = 0; i < this.variableBySlot.Count; i++)
			{
				Symbol symbol = this.variableBySlot[i].Symbol;
				if (symbol != null)
				{
					SymbolKind kind = symbol.Kind;
					if (kind != SymbolKind.Field)
					{
						if (kind != SymbolKind.Local)
						{
							if (kind != SymbolKind.Parameter)
							{
								throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
							}
							base.SetSlotState(i, false);
						}
						else if (!((LocalSymbol)symbol).IsConst)
						{
							base.SetSlotState(i, false);
						}
					}
					else if (!((FieldSymbol)symbol).IsConst)
					{
						base.SetSlotState(i, false);
					}
				}
			}
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x000CE1EA File Offset: 0x000CC3EA
		public override BoundNode VisitAwaitExpression(BoundAwaitExpression node)
		{
			base.VisitAwaitExpression(node);
			this.MarkLocalsUnassigned();
			return null;
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x000CE1FB File Offset: 0x000CC3FB
		public override BoundNode VisitYieldReturnStatement(BoundYieldReturnStatement node)
		{
			base.VisitYieldReturnStatement(node);
			this.MarkLocalsUnassigned();
			this._seenYieldInCurrentTry = true;
			return null;
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000CE213 File Offset: 0x000CC413
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		protected override ImmutableArray<AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.PendingBranch> Scan(ref bool badRegion)
		{
			this._variablesToHoist.Clear();
			MultiDictionary<Symbol, SyntaxNode> lazyDisallowedCaptures = this._lazyDisallowedCaptures;
			if (lazyDisallowedCaptures != null)
			{
				lazyDisallowedCaptures.Clear();
			}
			return base.Scan(ref badRegion);
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x000CE238 File Offset: 0x000CC438
		private void CaptureVariable(Symbol variable, SyntaxNode syntax)
		{
			if (((variable.Kind == SymbolKind.Local) ? ((LocalSymbol)variable).Type : ((ParameterSymbol)variable).Type).IsRestrictedType(false))
			{
				MultiDictionary<Symbol, SyntaxNode> multiDictionary;
				if ((multiDictionary = this._lazyDisallowedCaptures) == null)
				{
					multiDictionary = (this._lazyDisallowedCaptures = new MultiDictionary<Symbol, SyntaxNode>());
				}
				multiDictionary.Add(variable, syntax);
				return;
			}
			if (this._variablesToHoist.Add(variable))
			{
				LocalSymbol localSymbol = variable as LocalSymbol;
				BoundExpression boundExpression;
				if (localSymbol != null && this._boundRefLocalInitializers.TryGetValue(localSymbol, ref boundExpression))
				{
					this.CaptureRefInitializer(boundExpression, syntax);
				}
			}
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x000CE2C0 File Offset: 0x000CC4C0
		private void CaptureRefInitializer(BoundExpression variableInitializer, SyntaxNode syntax)
		{
			BoundLocal boundLocal = variableInitializer as BoundLocal;
			if (boundLocal != null)
			{
				LocalSymbol localSymbol = boundLocal.LocalSymbol;
				this.CaptureVariable(localSymbol, syntax);
				return;
			}
			BoundParameter boundParameter = variableInitializer as BoundParameter;
			if (boundParameter != null)
			{
				ParameterSymbol parameterSymbol = boundParameter.ParameterSymbol;
				this.CaptureVariable(parameterSymbol, syntax);
				return;
			}
			BoundFieldAccess boundFieldAccess = variableInitializer as BoundFieldAccess;
			if (boundFieldAccess != null)
			{
				FieldSymbol fieldSymbol = boundFieldAccess.FieldSymbol;
				if (fieldSymbol != null && !fieldSymbol.IsStatic)
				{
					NamedTypeSymbol containingType = fieldSymbol.ContainingType;
					if (containingType != null && containingType.IsValueType)
					{
						BoundExpression receiverOpt = boundFieldAccess.ReceiverOpt;
						if (receiverOpt == null)
						{
							return;
						}
						this.CaptureRefInitializer(receiverOpt, syntax);
					}
				}
			}
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x000CE354 File Offset: 0x000CC554
		protected override void EnterParameter(ParameterSymbol parameter)
		{
			this.GetOrCreateSlot(parameter, 0, false, true);
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x000CE364 File Offset: 0x000CC564
		protected override void ReportUnassigned(Symbol symbol, SyntaxNode node, int slot, bool skipIfUseBeforeDeclaration)
		{
			SymbolKind kind = symbol.Kind;
			if (kind != SymbolKind.Field)
			{
				if (kind != SymbolKind.Local && kind != SymbolKind.Parameter)
				{
					return;
				}
			}
			else
			{
				symbol = base.GetNonMemberSymbol(slot);
			}
			this.CaptureVariable(symbol, node);
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x000CE397 File Offset: 0x000CC597
		protected override void VisitLvalueParameter(BoundParameter node)
		{
			this.TryHoistTopLevelParameter(node);
			base.VisitLvalueParameter(node);
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x000CE3A7 File Offset: 0x000CC5A7
		public override BoundNode VisitParameter(BoundParameter node)
		{
			this.TryHoistTopLevelParameter(node);
			return base.VisitParameter(node);
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x000CE3B7 File Offset: 0x000CC5B7
		private void TryHoistTopLevelParameter(BoundParameter node)
		{
			if (node.ParameterSymbol.ContainingSymbol == this.topLevelMethod)
			{
				this.CaptureVariable(node.ParameterSymbol, node.Syntax);
			}
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x000CE3E4 File Offset: 0x000CC5E4
		public override BoundNode VisitFieldAccess(BoundFieldAccess node)
		{
			if (node.ReceiverOpt != null && node.ReceiverOpt.Kind == BoundKind.ThisReference)
			{
				ParameterSymbol thisParameter = this.topLevelMethod.ThisParameter;
				this.CaptureVariable(thisParameter, node.Syntax);
			}
			return base.VisitFieldAccess(node);
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x000CE428 File Offset: 0x000CC628
		public override BoundNode VisitThisReference(BoundThisReference node)
		{
			this.CaptureVariable(this.topLevelMethod.ThisParameter, node.Syntax);
			return base.VisitThisReference(node);
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x000CE448 File Offset: 0x000CC648
		public override BoundNode VisitBaseReference(BoundBaseReference node)
		{
			this.CaptureVariable(this.topLevelMethod.ThisParameter, node.Syntax);
			return base.VisitBaseReference(node);
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000CE468 File Offset: 0x000CC668
		public override BoundNode VisitTryStatement(BoundTryStatement node)
		{
			bool seenYieldInCurrentTry = this._seenYieldInCurrentTry;
			this._seenYieldInCurrentTry = false;
			base.VisitTryStatement(node);
			this._seenYieldInCurrentTry = this._seenYieldInCurrentTry || seenYieldInCurrentTry;
			return null;
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x000CE49A File Offset: 0x000CC69A
		protected override void VisitFinallyBlock(BoundStatement finallyBlock, ref DefiniteAssignmentPass.LocalState unsetInFinally)
		{
			if (this._seenYieldInCurrentTry)
			{
				new IteratorAndAsyncCaptureWalker.OutsideVariablesUsedInside(this, this.topLevelMethod, this).Visit(finallyBlock);
			}
			base.VisitFinallyBlock(finallyBlock, ref unsetInFinally);
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x000CE4C0 File Offset: 0x000CC6C0
		public override BoundNode VisitAssignmentOperator(BoundAssignmentOperator node)
		{
			base.VisitAssignmentOperator(node);
			if (node != null && node.IsRef)
			{
				BoundLocal boundLocal = node.Left as BoundLocal;
				if (boundLocal != null)
				{
					LocalSymbol localSymbol = boundLocal.LocalSymbol;
					if (localSymbol != null && localSymbol.IsCompilerGenerated)
					{
						this._boundRefLocalInitializers[localSymbol] = node.Right;
					}
				}
			}
			return null;
		}

		// Token: 0x040010EB RID: 4331
		private readonly OrderedSet<Symbol> _variablesToHoist = new OrderedSet<Symbol>();

		// Token: 0x040010EC RID: 4332
		private MultiDictionary<Symbol, SyntaxNode> _lazyDisallowedCaptures;

		// Token: 0x040010ED RID: 4333
		private bool _seenYieldInCurrentTry;

		// Token: 0x040010EE RID: 4334
		private readonly Dictionary<LocalSymbol, BoundExpression> _boundRefLocalInitializers = new Dictionary<LocalSymbol, BoundExpression>();

		// Token: 0x02000740 RID: 1856
		private sealed class OutsideVariablesUsedInside : BoundTreeWalkerWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator
		{
			// Token: 0x06008650 RID: 34384 RVA: 0x001E838B File Offset: 0x001E658B
			public OutsideVariablesUsedInside(IteratorAndAsyncCaptureWalker analyzer, MethodSymbol topLevelMethod, IteratorAndAsyncCaptureWalker parent)
				: base(parent._recursionDepth)
			{
				this._analyzer = analyzer;
				this._topLevelMethod = topLevelMethod;
				this._localsInScope = new HashSet<Symbol>();
				this._parent = parent;
			}

			// Token: 0x06008651 RID: 34385 RVA: 0x001E83B9 File Offset: 0x001E65B9
			protected override bool ConvertInsufficientExecutionStackExceptionToCancelledByStackGuardException()
			{
				return this._parent.ConvertInsufficientExecutionStackExceptionToCancelledByStackGuardException();
			}

			// Token: 0x06008652 RID: 34386 RVA: 0x001E83C6 File Offset: 0x001E65C6
			public override BoundNode VisitBlock(BoundBlock node)
			{
				this.AddVariables(node.Locals);
				return base.VisitBlock(node);
			}

			// Token: 0x06008653 RID: 34387 RVA: 0x001E83DC File Offset: 0x001E65DC
			private void AddVariables(ImmutableArray<LocalSymbol> locals)
			{
				foreach (LocalSymbol localSymbol in locals)
				{
					this.AddVariable(localSymbol);
				}
			}

			// Token: 0x06008654 RID: 34388 RVA: 0x001E840B File Offset: 0x001E660B
			public override BoundNode VisitCatchBlock(BoundCatchBlock node)
			{
				this.AddVariables(node.Locals);
				return base.VisitCatchBlock(node);
			}

			// Token: 0x06008655 RID: 34389 RVA: 0x001E8420 File Offset: 0x001E6620
			private void AddVariable(Symbol local)
			{
				if (local != null)
				{
					this._localsInScope.Add(local);
				}
			}

			// Token: 0x06008656 RID: 34390 RVA: 0x001E8432 File Offset: 0x001E6632
			public override BoundNode VisitSequence(BoundSequence node)
			{
				this.AddVariables(node.Locals);
				return base.VisitSequence(node);
			}

			// Token: 0x06008657 RID: 34391 RVA: 0x001E8447 File Offset: 0x001E6647
			public override BoundNode VisitThisReference(BoundThisReference node)
			{
				this.Capture(this._topLevelMethod.ThisParameter, node.Syntax);
				return base.VisitThisReference(node);
			}

			// Token: 0x06008658 RID: 34392 RVA: 0x001E8467 File Offset: 0x001E6667
			public override BoundNode VisitBaseReference(BoundBaseReference node)
			{
				this.Capture(this._topLevelMethod.ThisParameter, node.Syntax);
				return base.VisitBaseReference(node);
			}

			// Token: 0x06008659 RID: 34393 RVA: 0x001E8487 File Offset: 0x001E6687
			public override BoundNode VisitLocal(BoundLocal node)
			{
				this.Capture(node.LocalSymbol, node.Syntax);
				return base.VisitLocal(node);
			}

			// Token: 0x0600865A RID: 34394 RVA: 0x001E84A2 File Offset: 0x001E66A2
			public override BoundNode VisitParameter(BoundParameter node)
			{
				this.Capture(node.ParameterSymbol, node.Syntax);
				return base.VisitParameter(node);
			}

			// Token: 0x0600865B RID: 34395 RVA: 0x001E84BD File Offset: 0x001E66BD
			private void Capture(Symbol s, SyntaxNode syntax)
			{
				if (s != null && !this._localsInScope.Contains(s))
				{
					this._analyzer.CaptureVariable(s, syntax);
				}
			}

			// Token: 0x0400230B RID: 8971
			private readonly HashSet<Symbol> _localsInScope;

			// Token: 0x0400230C RID: 8972
			private readonly IteratorAndAsyncCaptureWalker _analyzer;

			// Token: 0x0400230D RID: 8973
			private readonly MethodSymbol _topLevelMethod;

			// Token: 0x0400230E RID: 8974
			private readonly IteratorAndAsyncCaptureWalker _parent;
		}
	}
}
