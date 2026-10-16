using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000158 RID: 344
	internal class ReadWriteWalker : AbstractRegionDataFlowPass
	{
		// Token: 0x06001D6C RID: 7532 RVA: 0x0009ED80 File Offset: 0x0009CF80
		internal static void Analyze(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion, HashSet<PrefixUnaryExpressionSyntax> unassignedVariableAddressOfSyntaxes, out IEnumerable<Symbol> readInside, out IEnumerable<Symbol> writtenInside, out IEnumerable<Symbol> readOutside, out IEnumerable<Symbol> writtenOutside, out IEnumerable<Symbol> captured, out IEnumerable<Symbol> unsafeAddressTaken, out IEnumerable<Symbol> capturedInside, out IEnumerable<Symbol> capturedOutside, out IEnumerable<MethodSymbol> usedLocalFunctions)
		{
			ReadWriteWalker readWriteWalker = new ReadWriteWalker(compilation, member, node, firstInRegion, lastInRegion, unassignedVariableAddressOfSyntaxes);
			try
			{
				bool flag = false;
				readWriteWalker.Analyze(ref flag, default(Optional<DefiniteAssignmentPass.LocalState>));
				if (flag)
				{
					IEnumerable<Symbol> enumerable;
					capturedOutside = (enumerable = Enumerable.Empty<Symbol>());
					capturedInside = (enumerable = enumerable);
					unsafeAddressTaken = (enumerable = enumerable);
					captured = (enumerable = enumerable);
					writtenOutside = (enumerable = enumerable);
					readOutside = (enumerable = enumerable);
					writtenInside = (enumerable = enumerable);
					readInside = enumerable;
					usedLocalFunctions = Enumerable.Empty<MethodSymbol>();
				}
				else
				{
					readInside = readWriteWalker._readInside;
					writtenInside = readWriteWalker._writtenInside;
					readOutside = readWriteWalker._readOutside;
					writtenOutside = readWriteWalker._writtenOutside;
					captured = readWriteWalker.GetCaptured();
					capturedInside = readWriteWalker.GetCapturedInside();
					capturedOutside = readWriteWalker.GetCapturedOutside();
					unsafeAddressTaken = readWriteWalker.GetUnsafeAddressTaken();
					usedLocalFunctions = readWriteWalker.GetUsedLocalFunctions();
				}
			}
			finally
			{
				readWriteWalker.Free();
			}
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0009EE58 File Offset: 0x0009D058
		private ReadWriteWalker(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion, HashSet<PrefixUnaryExpressionSyntax> unassignedVariableAddressOfSyntaxes)
			: base(compilation, member, node, firstInRegion, lastInRegion, null, unassignedVariableAddressOfSyntaxes, false)
		{
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x0009EEA4 File Offset: 0x0009D0A4
		protected override void EnterRegion()
		{
			for (MethodSymbol methodSymbol = this.CurrentSymbol as MethodSymbol; methodSymbol != null; methodSymbol = methodSymbol.ContainingSymbol as MethodSymbol)
			{
				foreach (ParameterSymbol parameterSymbol in methodSymbol.Parameters)
				{
					if (parameterSymbol.RefKind != RefKind.None)
					{
						this._readOutside.Add(parameterSymbol);
					}
				}
				ParameterSymbol thisParameter = methodSymbol.ThisParameter;
				if (thisParameter != null && thisParameter.RefKind != RefKind.None)
				{
					this._readOutside.Add(thisParameter);
				}
			}
			base.EnterRegion();
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x0009EF2D File Offset: 0x0009D12D
		protected override void NoteRead(Symbol variable, ParameterSymbol rangeVariableUnderlyingParameter = null)
		{
			if (variable == null)
			{
				return;
			}
			if (variable.Kind != SymbolKind.Field)
			{
				(base.IsInside ? this._readInside : this._readOutside).Add(variable);
			}
			base.NoteRead(variable, rangeVariableUnderlyingParameter);
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x0009EF61 File Offset: 0x0009D161
		protected override void NoteWrite(Symbol variable, BoundExpression value, bool read)
		{
			if (variable == null)
			{
				return;
			}
			(base.IsInside ? this._writtenInside : this._writtenOutside).Add(variable);
			base.NoteWrite(variable, value, read);
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x0009EF90 File Offset: 0x0009D190
		protected override void CheckAssigned(BoundExpression expr, FieldSymbol fieldSymbol, SyntaxNode node)
		{
			base.CheckAssigned(expr, fieldSymbol, node);
			if (!base.IsInside && node.Span.Contains(this.RegionSpan) && expr.Kind == BoundKind.FieldAccess)
			{
				this.NoteReceiverRead((BoundFieldAccess)expr);
			}
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0009EFDD File Offset: 0x0009D1DD
		private void NoteReceiverWritten(BoundFieldAccess expr)
		{
			this.NoteReceiverReadOrWritten(expr, this._writtenInside);
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x0009EFEC File Offset: 0x0009D1EC
		private void NoteReceiverRead(BoundFieldAccess expr)
		{
			this.NoteReceiverReadOrWritten(expr, this._readInside);
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0009EFFC File Offset: 0x0009D1FC
		private void NoteReceiverReadOrWritten(BoundFieldAccess expr, HashSet<Symbol> readOrWritten)
		{
			if (expr.FieldSymbol.IsStatic)
			{
				return;
			}
			if (expr.FieldSymbol.ContainingType.IsReferenceType)
			{
				return;
			}
			BoundExpression receiverOpt = expr.ReceiverOpt;
			if (receiverOpt == null)
			{
				return;
			}
			SyntaxNode syntax = receiverOpt.Syntax;
			if (syntax == null)
			{
				return;
			}
			BoundKind kind = receiverOpt.Kind;
			switch (kind)
			{
			case BoundKind.ThisReference:
				if (base.RegionContains(syntax.Span))
				{
					readOrWritten.Add(base.MethodThisParameter);
					return;
				}
				break;
			case BoundKind.PreviousSubmissionReference:
			case BoundKind.HostObjectMemberReference:
			case BoundKind.PseudoVariable:
				break;
			case BoundKind.BaseReference:
				if (base.RegionContains(syntax.Span))
				{
					readOrWritten.Add(base.MethodThisParameter);
					return;
				}
				break;
			case BoundKind.Local:
				if (base.RegionContains(syntax.Span))
				{
					readOrWritten.Add(((BoundLocal)receiverOpt).LocalSymbol);
					return;
				}
				break;
			case BoundKind.RangeVariable:
				if (base.RegionContains(syntax.Span))
				{
					readOrWritten.Add(((BoundRangeVariable)receiverOpt).RangeVariableSymbol);
					return;
				}
				break;
			case BoundKind.Parameter:
				if (base.RegionContains(syntax.Span))
				{
					readOrWritten.Add(((BoundParameter)receiverOpt).ParameterSymbol);
					return;
				}
				break;
			default:
				if (kind != BoundKind.FieldAccess)
				{
					return;
				}
				if (receiverOpt.Type.IsStructType() && syntax.Span.OverlapsWith(this.RegionSpan))
				{
					this.NoteReceiverReadOrWritten(receiverOpt as BoundFieldAccess, readOrWritten);
				}
				break;
			}
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0009F150 File Offset: 0x0009D350
		protected override void AssignImpl(BoundNode node, BoundExpression value, bool isRef, bool written, bool read)
		{
			BoundKind kind = node.Kind;
			if (kind != BoundKind.RangeVariable)
			{
				if (kind != BoundKind.FieldAccess)
				{
					if (kind != BoundKind.QueryClause)
					{
						base.AssignImpl(node, value, isRef, written, read);
					}
					else
					{
						base.AssignImpl(node, value, isRef, written, read);
						RangeVariableSymbol definedSymbol = ((BoundQueryClause)node).DefinedSymbol;
						if (definedSymbol != null && written)
						{
							this.NoteWrite(definedSymbol, value, read);
							return;
						}
					}
				}
				else
				{
					base.AssignImpl(node, value, isRef, written, read);
					BoundFieldAccess boundFieldAccess = node as BoundFieldAccess;
					if (!base.IsInside && node.Syntax != null && node.Syntax.Span.Contains(this.RegionSpan))
					{
						this.NoteReceiverWritten(boundFieldAccess);
						return;
					}
				}
			}
			else if (written)
			{
				this.NoteWrite(((BoundRangeVariable)node).RangeVariableSymbol, value, read);
				return;
			}
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0009F219 File Offset: 0x0009D419
		public override BoundNode VisitUnboundLambda(UnboundLambda node)
		{
			return this.VisitLambda(node.BindForErrorRecovery());
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0009F228 File Offset: 0x0009D428
		public override BoundNode VisitRangeVariable(BoundRangeVariable node)
		{
			ParameterSymbol rangeVariableUnderlyingParameter = ReadWriteWalker.GetRangeVariableUnderlyingParameter(node.Value);
			this.NoteRead(node.RangeVariableSymbol, rangeVariableUnderlyingParameter);
			return null;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0009F250 File Offset: 0x0009D450
		private static ParameterSymbol GetRangeVariableUnderlyingParameter(BoundNode underlying)
		{
			while (underlying != null)
			{
				BoundKind kind = underlying.Kind;
				if (kind == BoundKind.Parameter)
				{
					return ((BoundParameter)underlying).ParameterSymbol;
				}
				if (kind != BoundKind.PropertyAccess)
				{
					return null;
				}
				underlying = ((BoundPropertyAccess)underlying).ReceiverOpt;
			}
			return null;
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x0009F296 File Offset: 0x0009D496
		public override BoundNode VisitQueryClause(BoundQueryClause node)
		{
			base.Assign(node, null, false, true);
			return base.VisitQueryClause(node);
		}

		// Token: 0x04000E5A RID: 3674
		private readonly HashSet<Symbol> _readInside = new HashSet<Symbol>();

		// Token: 0x04000E5B RID: 3675
		private readonly HashSet<Symbol> _writtenInside = new HashSet<Symbol>();

		// Token: 0x04000E5C RID: 3676
		private readonly HashSet<Symbol> _readOutside = new HashSet<Symbol>();

		// Token: 0x04000E5D RID: 3677
		private readonly HashSet<Symbol> _writtenOutside = new HashSet<Symbol>();
	}
}
