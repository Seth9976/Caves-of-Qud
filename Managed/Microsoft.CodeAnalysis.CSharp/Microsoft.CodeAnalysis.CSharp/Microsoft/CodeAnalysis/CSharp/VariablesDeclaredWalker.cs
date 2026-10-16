using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200015D RID: 349
	internal class VariablesDeclaredWalker : AbstractRegionControlFlowPass
	{
		// Token: 0x06001D89 RID: 7561 RVA: 0x0009F588 File Offset: 0x0009D788
		internal static IEnumerable<Symbol> Analyze(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion)
		{
			VariablesDeclaredWalker variablesDeclaredWalker = new VariablesDeclaredWalker(compilation, member, node, firstInRegion, lastInRegion);
			IEnumerable<Symbol> enumerable;
			try
			{
				bool flag = false;
				variablesDeclaredWalker.Analyze(ref flag, default(Optional<ControlFlowPass.LocalState>));
				IEnumerable<Symbol> enumerable2;
				if (!flag)
				{
					enumerable = variablesDeclaredWalker._variablesDeclared;
					enumerable2 = enumerable;
				}
				else
				{
					enumerable2 = SpecializedCollections.EmptyEnumerable<Symbol>();
				}
				enumerable = enumerable2;
			}
			finally
			{
				variablesDeclaredWalker.Free();
			}
			return enumerable;
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x0009F5E4 File Offset: 0x0009D7E4
		internal VariablesDeclaredWalker(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion)
			: base(compilation, member, node, firstInRegion, lastInRegion)
		{
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x0009F5FE File Offset: 0x0009D7FE
		protected override void Free()
		{
			base.Free();
			this._variablesDeclared = null;
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x0009F60D File Offset: 0x0009D80D
		public override void VisitPattern(BoundPattern pattern)
		{
			base.VisitPattern(pattern);
			this.NoteDeclaredPatternVariables(pattern);
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x0009F620 File Offset: 0x0009D820
		protected override void VisitSwitchSection(BoundSwitchSection node, bool isLastSection)
		{
			foreach (BoundSwitchLabel boundSwitchLabel in node.SwitchLabels)
			{
				this.NoteDeclaredPatternVariables(boundSwitchLabel.Pattern);
			}
			base.VisitSwitchSection(node, isLastSection);
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x0009F664 File Offset: 0x0009D864
		private void NoteDeclaredPatternVariables(BoundPattern pattern)
		{
			if (base.IsInside)
			{
				BoundDeclarationPattern boundDeclarationPattern = pattern as BoundDeclarationPattern;
				if (boundDeclarationPattern == null)
				{
					BoundRecursivePattern boundRecursivePattern = pattern as BoundRecursivePattern;
					if (boundRecursivePattern == null)
					{
						return;
					}
					Symbol variable = boundRecursivePattern.Variable;
					if (variable != null && variable.Kind == SymbolKind.Local)
					{
						this._variablesDeclared.Add(boundRecursivePattern.Variable);
					}
				}
				else
				{
					Symbol variable2 = boundDeclarationPattern.Variable;
					if (variable2 != null && variable2.Kind == SymbolKind.Local)
					{
						this._variablesDeclared.Add(boundDeclarationPattern.Variable);
						return;
					}
				}
			}
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0009F6E1 File Offset: 0x0009D8E1
		public override BoundNode VisitLocalDeclaration(BoundLocalDeclaration node)
		{
			if (base.IsInside)
			{
				this._variablesDeclared.Add(node.LocalSymbol);
			}
			return base.VisitLocalDeclaration(node);
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x0009F704 File Offset: 0x0009D904
		public override BoundNode VisitLambda(BoundLambda node)
		{
			if (base.IsInside && !node.WasCompilerGenerated)
			{
				foreach (ParameterSymbol parameterSymbol in node.Symbol.Parameters)
				{
					this._variablesDeclared.Add(parameterSymbol);
				}
			}
			return base.VisitLambda(node);
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x0009F75C File Offset: 0x0009D95C
		public override BoundNode VisitLocalFunctionStatement(BoundLocalFunctionStatement node)
		{
			if (base.IsInside && !node.WasCompilerGenerated)
			{
				foreach (ParameterSymbol parameterSymbol in node.Symbol.Parameters)
				{
					this._variablesDeclared.Add(parameterSymbol);
				}
			}
			return base.VisitLocalFunctionStatement(node);
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0009F7B4 File Offset: 0x0009D9B4
		public override void VisitForEachIterationVariables(BoundForEachStatement node)
		{
			if (base.IsInside)
			{
				BoundForEachDeconstructStep deconstructionOpt = node.DeconstructionOpt;
				BoundDeconstructionAssignmentOperator boundDeconstructionAssignmentOperator = ((deconstructionOpt != null) ? deconstructionOpt.DeconstructionAssignment : null);
				if (boundDeconstructionAssignmentOperator == null)
				{
					this._variablesDeclared.AddAll<Symbol>(node.IterationVariables);
					return;
				}
				boundDeconstructionAssignmentOperator.Left.VisitAllElements<VariablesDeclaredWalker>(delegate([global::System.Runtime.CompilerServices.Nullable(1)] BoundExpression x, VariablesDeclaredWalker self)
				{
					self.Visit(x);
				}, this);
			}
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x0009F824 File Offset: 0x0009DA24
		protected override void VisitCatchBlock(BoundCatchBlock catchBlock, ref ControlFlowPass.LocalState finallyState)
		{
			if (base.IsInside)
			{
				LocalSymbol localSymbol = catchBlock.Locals.FirstOrDefault<LocalSymbol>();
				if (localSymbol != null && localSymbol.DeclarationKind == LocalDeclarationKind.CatchVariable)
				{
					this._variablesDeclared.Add(localSymbol);
				}
			}
			base.VisitCatchBlock(catchBlock, ref finallyState);
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x0009F866 File Offset: 0x0009DA66
		public override BoundNode VisitQueryClause(BoundQueryClause node)
		{
			if (base.IsInside && node.DefinedSymbol != null)
			{
				this._variablesDeclared.Add(node.DefinedSymbol);
			}
			return base.VisitQueryClause(node);
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x0009F891 File Offset: 0x0009DA91
		protected override void VisitLvalue(BoundLocal node)
		{
			this.VisitLocal(node);
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x0009F89B File Offset: 0x0009DA9B
		public override BoundNode VisitLocal(BoundLocal node)
		{
			if (base.IsInside && node.DeclarationKind != BoundLocalDeclarationKind.None)
			{
				this._variablesDeclared.Add(node.LocalSymbol);
			}
			return null;
		}

		// Token: 0x04000E68 RID: 3688
		private HashSet<Symbol> _variablesDeclared = new HashSet<Symbol>();
	}
}
