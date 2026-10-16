using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000155 RID: 341
	internal class ExitPointsWalker : AbstractRegionControlFlowPass
	{
		// Token: 0x06001BA6 RID: 7078 RVA: 0x0009105E File Offset: 0x0008F25E
		private ExitPointsWalker(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion)
			: base(compilation, member, node, firstInRegion, lastInRegion)
		{
			this._labelsInside = new ArrayBuilder<LabelSymbol>();
			this._branchesOutOf = ArrayBuilder<StatementSyntax>.GetInstance();
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x00091083 File Offset: 0x0008F283
		protected override void Free()
		{
			if (this._branchesOutOf != null)
			{
				this._branchesOutOf.Free();
			}
			this._labelsInside.Free();
			base.Free();
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x000910AC File Offset: 0x0008F2AC
		internal static IEnumerable<StatementSyntax> Analyze(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion)
		{
			ExitPointsWalker exitPointsWalker = new ExitPointsWalker(compilation, member, node, firstInRegion, lastInRegion);
			IEnumerable<StatementSyntax> enumerable;
			try
			{
				bool flag = false;
				exitPointsWalker.Analyze(ref flag);
				ImmutableArray<StatementSyntax> immutableArray = exitPointsWalker._branchesOutOf.ToImmutableAndFree();
				exitPointsWalker._branchesOutOf = null;
				IEnumerable<StatementSyntax> enumerable2;
				if (!flag)
				{
					enumerable = immutableArray;
					enumerable2 = enumerable;
				}
				else
				{
					enumerable2 = SpecializedCollections.EmptyEnumerable<StatementSyntax>();
				}
				enumerable = enumerable2;
			}
			finally
			{
				exitPointsWalker.Free();
			}
			return enumerable;
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x00091110 File Offset: 0x0008F310
		private void Analyze(ref bool badRegion)
		{
			this.Scan(ref badRegion);
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0009111A File Offset: 0x0008F31A
		public override BoundNode VisitLabelStatement(BoundLabelStatement node)
		{
			if (base.IsInside)
			{
				this._labelsInside.Add(node.Label);
			}
			return base.VisitLabelStatement(node);
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0009113C File Offset: 0x0008F33C
		public override BoundNode VisitDoStatement(BoundDoStatement node)
		{
			if (base.IsInside)
			{
				this._labelsInside.Add(node.BreakLabel);
				this._labelsInside.Add(node.ContinueLabel);
			}
			return base.VisitDoStatement(node);
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0009116F File Offset: 0x0008F36F
		public override BoundNode VisitForEachStatement(BoundForEachStatement node)
		{
			if (base.IsInside)
			{
				this._labelsInside.Add(node.BreakLabel);
				this._labelsInside.Add(node.ContinueLabel);
			}
			return base.VisitForEachStatement(node);
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x000911A2 File Offset: 0x0008F3A2
		public override BoundNode VisitForStatement(BoundForStatement node)
		{
			if (base.IsInside)
			{
				this._labelsInside.Add(node.BreakLabel);
				this._labelsInside.Add(node.ContinueLabel);
			}
			return base.VisitForStatement(node);
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x000911D5 File Offset: 0x0008F3D5
		public override BoundNode VisitWhileStatement(BoundWhileStatement node)
		{
			if (base.IsInside)
			{
				this._labelsInside.Add(node.BreakLabel);
			}
			return base.VisitWhileStatement(node);
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x000911F7 File Offset: 0x0008F3F7
		protected override void EnterRegion()
		{
			base.EnterRegion();
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x00091200 File Offset: 0x0008F400
		protected override void LeaveRegion()
		{
			foreach (AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.PendingBranch pendingBranch in base.PendingBranches)
			{
				if (pendingBranch.Branch != null && base.RegionContains(pendingBranch.Branch.Syntax.Span))
				{
					BoundKind kind = pendingBranch.Branch.Kind;
					if (kind <= BoundKind.ContinueStatement)
					{
						if (kind == BoundKind.AwaitExpression)
						{
							continue;
						}
						switch (kind)
						{
						case BoundKind.ReturnStatement:
						case BoundKind.YieldBreakStatement:
							break;
						case BoundKind.YieldReturnStatement:
							continue;
						case BoundKind.ThrowStatement:
						case BoundKind.ExpressionStatement:
							goto IL_0106;
						case BoundKind.BreakStatement:
							if (this._labelsInside.Contains(((BoundBreakStatement)pendingBranch.Branch).Label))
							{
								continue;
							}
							break;
						case BoundKind.ContinueStatement:
							if (this._labelsInside.Contains(((BoundContinueStatement)pendingBranch.Branch).Label))
							{
								continue;
							}
							break;
						default:
							goto IL_0106;
						}
					}
					else if (kind != BoundKind.ForEachStatement)
					{
						if (kind == BoundKind.UsingStatement)
						{
							continue;
						}
						if (kind != BoundKind.GotoStatement)
						{
							goto IL_0106;
						}
						if (this._labelsInside.Contains(((BoundGotoStatement)pendingBranch.Branch).Label))
						{
							continue;
						}
					}
					else
					{
						if (((BoundForEachStatement)pendingBranch.Branch).AwaitOpt == null)
						{
							goto IL_0106;
						}
						continue;
					}
					this._branchesOutOf.Add((StatementSyntax)pendingBranch.Branch.Syntax);
					continue;
					IL_0106:
					throw ExceptionUtilities.UnexpectedValue(pendingBranch.Branch.Kind);
				}
			}
			base.LeaveRegion();
		}

		// Token: 0x04000E41 RID: 3649
		private readonly ArrayBuilder<LabelSymbol> _labelsInside;

		// Token: 0x04000E42 RID: 3650
		private ArrayBuilder<StatementSyntax> _branchesOutOf;
	}
}
