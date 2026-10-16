using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200015C RID: 348
	internal class UnassignedVariablesWalker : DefiniteAssignmentPass
	{
		// Token: 0x06001D84 RID: 7556 RVA: 0x0009F4B7 File Offset: 0x0009D6B7
		private UnassignedVariablesWalker(CSharpCompilation compilation, Symbol member, BoundNode node)
			: base(compilation, member, node, EmptyStructTypeCache.CreateNeverEmpty(), false, null)
		{
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0009F4D4 File Offset: 0x0009D6D4
		internal static HashSet<Symbol> Analyze(CSharpCompilation compilation, Symbol member, BoundNode node, bool convertInsufficientExecutionStackExceptionToCancelledByStackGuardException = false)
		{
			UnassignedVariablesWalker unassignedVariablesWalker = new UnassignedVariablesWalker(compilation, member, node);
			if (convertInsufficientExecutionStackExceptionToCancelledByStackGuardException)
			{
				unassignedVariablesWalker._convertInsufficientExecutionStackExceptionToCancelledByStackGuardException = true;
			}
			HashSet<Symbol> hashSet2;
			try
			{
				bool flag = false;
				HashSet<Symbol> hashSet = unassignedVariablesWalker.Analyze(ref flag);
				hashSet2 = (flag ? new HashSet<Symbol>() : hashSet);
			}
			finally
			{
				unassignedVariablesWalker.Free();
			}
			return hashSet2;
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x0009F528 File Offset: 0x0009D728
		private HashSet<Symbol> Analyze(ref bool badRegion)
		{
			base.Analyze(ref badRegion, null);
			return this._result;
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0009F538 File Offset: 0x0009D738
		protected override void ReportUnassigned(Symbol symbol, SyntaxNode node, int slot, bool skipIfUseBeforeDeclaration)
		{
			if (symbol.Kind != SymbolKind.Field)
			{
				this._result.Add(symbol);
				return;
			}
			this._result.Add(base.GetNonMemberSymbol(slot));
			base.ReportUnassigned(symbol, node, slot, skipIfUseBeforeDeclaration);
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x0009F56F File Offset: 0x0009D76F
		protected override void ReportUnassignedOutParameter(ParameterSymbol parameter, SyntaxNode node, Location location)
		{
			this._result.Add(parameter);
			base.ReportUnassignedOutParameter(parameter, node, location);
		}

		// Token: 0x04000E67 RID: 3687
		private readonly HashSet<Symbol> _result = new HashSet<Symbol>();
	}
}
