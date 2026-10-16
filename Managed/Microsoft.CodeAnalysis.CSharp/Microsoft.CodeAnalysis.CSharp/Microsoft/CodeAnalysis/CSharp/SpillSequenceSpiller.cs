using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000207 RID: 519
	internal sealed class SpillSequenceSpiller : BoundTreeRewriterWithStackGuard
	{
		// Token: 0x06002576 RID: 9590 RVA: 0x000CC472 File Offset: 0x000CA672
		private SpillSequenceSpiller(MethodSymbol method, SyntaxNode syntaxNode, TypeCompilationState compilationState, PooledDictionary<LocalSymbol, LocalSymbol> tempSubstitution, BindingDiagnosticBag diagnostics)
		{
			this._F = new SyntheticBoundNodeFactory(method, syntaxNode, compilationState, diagnostics);
			this._F.CurrentFunction = method;
			this._tempSubstitution = tempSubstitution;
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000CC4A0 File Offset: 0x000CA6A0
		internal static BoundStatement Rewrite(BoundStatement body, MethodSymbol method, TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			PooledDictionary<LocalSymbol, LocalSymbol> instance = PooledDictionary<LocalSymbol, LocalSymbol>.GetInstance();
			BoundNode boundNode = new SpillSequenceSpiller(method, body.Syntax, compilationState, instance, diagnostics).Visit(body);
			boundNode = SpillSequenceSpiller.LocalSubstituter.Rewrite(instance, boundNode);
			instance.Free();
			return (BoundStatement)boundNode;
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000CC4E0 File Offset: 0x000CA6E0
		private BoundExpression VisitExpression(ref SpillSequenceSpiller.BoundSpillSequenceBuilder builder, BoundExpression expression)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(expression);
			if (boundExpression == null || boundExpression.Kind != (BoundKind)255)
			{
				return boundExpression;
			}
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = (SpillSequenceSpiller.BoundSpillSequenceBuilder)boundExpression;
			if (builder == null)
			{
				builder = boundSpillSequenceBuilder.Update(null);
			}
			else
			{
				builder.Include(boundSpillSequenceBuilder);
			}
			return boundSpillSequenceBuilder.Value;
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000CC530 File Offset: 0x000CA730
		private static BoundExpression UpdateExpression(SpillSequenceSpiller.BoundSpillSequenceBuilder builder, BoundExpression expression)
		{
			if (builder == null)
			{
				return expression;
			}
			if (!builder.HasLocals && !builder.HasStatements)
			{
				builder.Free();
				return expression;
			}
			return builder.Update(expression);
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000CC556 File Offset: 0x000CA756
		private BoundStatement UpdateStatement(SpillSequenceSpiller.BoundSpillSequenceBuilder builder, BoundStatement statement)
		{
			if (builder == null)
			{
				return statement;
			}
			if (statement != null)
			{
				builder.AddStatement(statement);
			}
			BoundBlock boundBlock = new BoundBlock(statement.Syntax, builder.GetLocals(), builder.GetStatements(), false);
			boundBlock.WasCompilerGenerated = true;
			builder.Free();
			return boundBlock;
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000CC58C File Offset: 0x000CA78C
		private BoundExpression Spill(SpillSequenceSpiller.BoundSpillSequenceBuilder builder, BoundExpression expression, RefKind refKind = RefKind.None, bool sideEffectsOnly = false)
		{
			if (builder.Syntax != null)
			{
				this._F.Syntax = builder.Syntax;
			}
			BoundKind kind;
			for (;;)
			{
				kind = expression.Kind;
				if (kind <= BoundKind.Sequence)
				{
					break;
				}
				if (kind <= BoundKind.ArrayInitialization)
				{
					goto Block_8;
				}
				if (kind == BoundKind.FieldAccess)
				{
					goto IL_0180;
				}
				if (kind != (BoundKind)255)
				{
					goto Block_12;
				}
				SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = (SpillSequenceSpiller.BoundSpillSequenceBuilder)expression;
				builder.Include(boundSpillSequenceBuilder);
				expression = boundSpillSequenceBuilder.Value;
			}
			if (kind <= BoundKind.ArgListOperator)
			{
				if (kind != BoundKind.TypeExpression)
				{
					if (kind != BoundKind.ArgListOperator)
					{
						goto IL_01F4;
					}
					BoundArgListOperator boundArgListOperator = (BoundArgListOperator)expression;
					ImmutableArray<BoundExpression> immutableArray = this.VisitExpressionList(ref builder, boundArgListOperator.Arguments, boundArgListOperator.ArgumentRefKindsOpt, true, false);
					return boundArgListOperator.Update(immutableArray, boundArgListOperator.ArgumentRefKindsOpt, boundArgListOperator.Type);
				}
			}
			else
			{
				switch (kind)
				{
				case BoundKind.Literal:
					break;
				case BoundKind.ThisReference:
				case BoundKind.BaseReference:
					if (refKind != RefKind.None || expression.Type.IsReferenceType)
					{
						return expression;
					}
					goto IL_01F4;
				case BoundKind.PreviousSubmissionReference:
				case BoundKind.HostObjectMemberReference:
				case BoundKind.PseudoVariable:
				case BoundKind.RangeVariable:
					goto IL_01F4;
				case BoundKind.Local:
				{
					BoundLocal boundLocal = (BoundLocal)expression;
					if (boundLocal.LocalSymbol.SynthesizedKind == SynthesizedLocalKind.Spill || refKind != RefKind.None)
					{
						return boundLocal;
					}
					goto IL_01F4;
				}
				case BoundKind.Parameter:
					if (refKind != RefKind.None)
					{
						return expression;
					}
					goto IL_01F4;
				default:
					if (kind != BoundKind.Sequence)
					{
						goto IL_01F4;
					}
					if (refKind != RefKind.None)
					{
						return expression;
					}
					goto IL_01F4;
				}
			}
			return expression;
			Block_8:
			if (kind == BoundKind.ConditionalReceiver)
			{
				return expression;
			}
			if (kind == BoundKind.ArrayInitialization)
			{
				BoundArrayInitialization boundArrayInitialization = (BoundArrayInitialization)expression;
				ImmutableArray<BoundExpression> immutableArray2 = this.VisitExpressionList(ref builder, boundArrayInitialization.Initializers, default(ImmutableArray<RefKind>), true, false);
				return boundArrayInitialization.Update(immutableArray2);
			}
			Block_12:
			goto IL_01F4;
			IL_0180:
			BoundFieldAccess boundFieldAccess = (BoundFieldAccess)expression;
			FieldSymbol fieldSymbol = boundFieldAccess.FieldSymbol;
			if (fieldSymbol.IsStatic)
			{
				if (refKind != RefKind.None || fieldSymbol.IsReadOnly)
				{
					return boundFieldAccess;
				}
			}
			else if (refKind != RefKind.None)
			{
				BoundExpression boundExpression = this.Spill(builder, boundFieldAccess.ReceiverOpt, fieldSymbol.ContainingType.IsValueType ? refKind : RefKind.None, false);
				return boundFieldAccess.Update(boundExpression, fieldSymbol, boundFieldAccess.ConstantValueOpt, boundFieldAccess.ResultKind, boundFieldAccess.Type);
			}
			IL_01F4:
			if (expression.Type.IsVoidType() || sideEffectsOnly)
			{
				builder.AddStatement(this._F.ExpressionStatement(expression));
				return null;
			}
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal2 = this._F.StoreToTemp(expression, out boundAssignmentOperator, refKind, SynthesizedLocalKind.Spill, this._F.Syntax);
			builder.AddLocal(boundLocal2.LocalSymbol);
			builder.AddStatement(this._F.ExpressionStatement(boundAssignmentOperator));
			return boundLocal2;
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000CC7F4 File Offset: 0x000CA9F4
		private ImmutableArray<BoundExpression> VisitExpressionList(ref SpillSequenceSpiller.BoundSpillSequenceBuilder builder, ImmutableArray<BoundExpression> args, ImmutableArray<RefKind> refKinds = default(ImmutableArray<RefKind>), bool forceSpill = false, bool sideEffectsOnly = false)
		{
			if (args.Length == 0)
			{
				return args;
			}
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(args);
			int num;
			if (forceSpill)
			{
				num = immutableArray.Length;
			}
			else
			{
				num = -1;
				for (int i = immutableArray.Length - 1; i >= 0; i--)
				{
					if (immutableArray[i].Kind == (BoundKind)255)
					{
						num = i;
						break;
					}
				}
			}
			if (num == -1)
			{
				return immutableArray;
			}
			if (builder == null)
			{
				SyntaxNode syntaxNode;
				if (num >= immutableArray.Length)
				{
					syntaxNode = null;
				}
				else
				{
					SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = immutableArray[num] as SpillSequenceSpiller.BoundSpillSequenceBuilder;
					syntaxNode = ((boundSpillSequenceBuilder != null) ? boundSpillSequenceBuilder.Syntax : null);
				}
				builder = new SpillSequenceSpiller.BoundSpillSequenceBuilder(syntaxNode, null);
			}
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(immutableArray.Length);
			for (int j = 0; j < num; j++)
			{
				RefKind refKind = (refKinds.IsDefault ? RefKind.None : refKinds[j]);
				BoundExpression boundExpression = this.Spill(builder, immutableArray[j], refKind, sideEffectsOnly);
				if (!sideEffectsOnly)
				{
					instance.Add(boundExpression);
				}
			}
			if (num < immutableArray.Length)
			{
				SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder2 = (SpillSequenceSpiller.BoundSpillSequenceBuilder)immutableArray[num];
				builder.Include(boundSpillSequenceBuilder2);
				instance.Add(boundSpillSequenceBuilder2.Value);
				for (int k = num + 1; k < immutableArray.Length; k++)
				{
					instance.Add(immutableArray[k]);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x000CC93C File Offset: 0x000CAB3C
		public override BoundNode VisitSwitchDispatch(BoundSwitchDispatch node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Expression);
			return this.UpdateStatement(boundSpillSequenceBuilder, node.Update(boundExpression, node.Cases, node.DefaultLabel, node.EqualityMethod));
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000CC97C File Offset: 0x000CAB7C
		public override BoundNode VisitThrowStatement(BoundThrowStatement node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.ExpressionOpt);
			return this.UpdateStatement(boundSpillSequenceBuilder, node.Update(boundExpression));
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000CC9A8 File Offset: 0x000CABA8
		public override BoundNode VisitExpressionStatement(BoundExpressionStatement node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Expression);
			return this.UpdateStatement(boundSpillSequenceBuilder, node.Update(boundExpression));
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000CC9D4 File Offset: 0x000CABD4
		public override BoundNode VisitConditionalGoto(BoundConditionalGoto node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Condition);
			return this.UpdateStatement(boundSpillSequenceBuilder, node.Update(boundExpression, node.JumpIfTrue, node.Label));
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000CCA0C File Offset: 0x000CAC0C
		public override BoundNode VisitReturnStatement(BoundReturnStatement node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.ExpressionOpt);
			return this.UpdateStatement(boundSpillSequenceBuilder, node.Update(node.RefKind, boundExpression));
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000CCA40 File Offset: 0x000CAC40
		public override BoundNode VisitYieldReturnStatement(BoundYieldReturnStatement node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Expression);
			return this.UpdateStatement(boundSpillSequenceBuilder, node.Update(boundExpression));
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000CCA6C File Offset: 0x000CAC6C
		public override BoundNode VisitCatchBlock(BoundCatchBlock node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ExceptionSourceOpt);
			ImmutableArray<LocalSymbol> immutableArray = node.Locals;
			BoundStatementList boundStatementList = node.ExceptionFilterPrologueOpt;
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression2 = this.VisitExpression(ref boundSpillSequenceBuilder, node.ExceptionFilterOpt);
			if (boundSpillSequenceBuilder != null)
			{
				immutableArray = immutableArray.AddRange(boundSpillSequenceBuilder.GetLocals());
				boundStatementList = new BoundStatementList(node.Syntax, boundSpillSequenceBuilder.GetStatements(), false);
			}
			BoundBlock boundBlock = (BoundBlock)this.Visit(node.Body);
			TypeSymbol typeSymbol = this.VisitType(node.ExceptionTypeOpt);
			return node.Update(immutableArray, boundExpression, typeSymbol, boundStatementList, boundExpression2, boundBlock, node.IsSynthesizedAsyncCatchAll);
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000CCB08 File Offset: 0x000CAD08
		public override BoundNode VisitAwaitExpression(BoundAwaitExpression node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Expression);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(boundExpression, node.AwaitableInfo, node.Type));
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x000CCB40 File Offset: 0x000CAD40
		public override BoundNode VisitSpillSequence(BoundSpillSequence node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = new SpillSequenceSpiller.BoundSpillSequenceBuilder(node.Syntax, null);
			this._F.Syntax = node.Syntax;
			boundSpillSequenceBuilder.AddStatements(base.VisitList<BoundStatement>(node.SideEffects));
			boundSpillSequenceBuilder.AddLocals(node.Locals);
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Value);
			return boundSpillSequenceBuilder.Update(boundExpression);
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000CCBA0 File Offset: 0x000CADA0
		public override BoundNode VisitAddressOfOperator(BoundAddressOfOperator node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Operand);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(boundExpression, node.IsManaged, node.Type));
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x000CCBD8 File Offset: 0x000CADD8
		public override BoundNode VisitArgListOperator(BoundArgListOperator node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			ImmutableArray<BoundExpression> immutableArray = this.VisitExpressionList(ref boundSpillSequenceBuilder, node.Arguments, default(ImmutableArray<RefKind>), false, false);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(immutableArray, node.ArgumentRefKindsOpt, node.Type));
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000CCC1C File Offset: 0x000CAE1C
		public override BoundNode VisitArrayAccess(BoundArrayAccess node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Expression);
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder2 = null;
			ImmutableArray<BoundExpression> immutableArray = this.VisitExpressionList(ref boundSpillSequenceBuilder2, node.Indices, default(ImmutableArray<RefKind>), false, false);
			if (boundSpillSequenceBuilder2 != null)
			{
				if (boundSpillSequenceBuilder == null)
				{
					boundSpillSequenceBuilder = new SpillSequenceSpiller.BoundSpillSequenceBuilder(boundSpillSequenceBuilder2.Syntax, null);
				}
				boundExpression = this.Spill(boundSpillSequenceBuilder, boundExpression, RefKind.None, false);
			}
			if (boundSpillSequenceBuilder != null)
			{
				boundSpillSequenceBuilder.Include(boundSpillSequenceBuilder2);
				boundSpillSequenceBuilder2 = boundSpillSequenceBuilder;
				boundSpillSequenceBuilder = null;
			}
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder2, node.Update(boundExpression, immutableArray, node.Type));
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x000CCC98 File Offset: 0x000CAE98
		public override BoundNode VisitArrayCreation(BoundArrayCreation node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundArrayInitialization boundArrayInitialization = (BoundArrayInitialization)this.VisitExpression(ref boundSpillSequenceBuilder, node.InitializerOpt);
			ImmutableArray<BoundExpression> immutableArray;
			if (boundSpillSequenceBuilder == null)
			{
				immutableArray = this.VisitExpressionList(ref boundSpillSequenceBuilder, node.Bounds, default(ImmutableArray<RefKind>), false, false);
			}
			else
			{
				SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder2 = new SpillSequenceSpiller.BoundSpillSequenceBuilder(boundSpillSequenceBuilder.Syntax, null);
				immutableArray = this.VisitExpressionList(ref boundSpillSequenceBuilder2, node.Bounds, default(ImmutableArray<RefKind>), true, false);
				boundSpillSequenceBuilder2.Include(boundSpillSequenceBuilder);
				boundSpillSequenceBuilder = boundSpillSequenceBuilder2;
			}
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(immutableArray, boundArrayInitialization, node.Type));
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x000CCD24 File Offset: 0x000CAF24
		public override BoundNode VisitArrayInitialization(BoundArrayInitialization node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			ImmutableArray<BoundExpression> immutableArray = this.VisitExpressionList(ref boundSpillSequenceBuilder, node.Initializers, default(ImmutableArray<RefKind>), false, false);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(immutableArray));
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x000CCD5C File Offset: 0x000CAF5C
		public override BoundNode VisitConvertedStackAllocExpression(BoundConvertedStackAllocExpression node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Count);
			BoundArrayInitialization boundArrayInitialization = (BoundArrayInitialization)this.VisitExpression(ref boundSpillSequenceBuilder, node.InitializerOpt);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(node.ElementType, boundExpression, boundArrayInitialization, node.Type));
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x000CCDA8 File Offset: 0x000CAFA8
		public override BoundNode VisitArrayLength(BoundArrayLength node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Expression);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(boundExpression, node.Type));
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x000CCDDC File Offset: 0x000CAFDC
		public override BoundNode VisitAsOperator(BoundAsOperator node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Operand);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(boundExpression, node.TargetType, node.Conversion, node.Type));
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x000CCE1C File Offset: 0x000CB01C
		public override BoundNode VisitAssignmentOperator(BoundAssignmentOperator node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Right);
			BoundExpression boundExpression2 = node.Left;
			if (boundSpillSequenceBuilder == null)
			{
				boundExpression2 = this.VisitExpression(ref boundSpillSequenceBuilder, boundExpression2);
			}
			else
			{
				SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder2 = new SpillSequenceSpiller.BoundSpillSequenceBuilder(boundSpillSequenceBuilder.Syntax, null);
				BoundKind kind = boundExpression2.Kind;
				if (kind <= BoundKind.Local)
				{
					if (kind == BoundKind.ArrayAccess)
					{
						BoundArrayAccess boundArrayAccess = (BoundArrayAccess)boundExpression2;
						BoundExpression boundExpression3 = this.VisitExpression(ref boundSpillSequenceBuilder2, boundArrayAccess.Expression);
						boundExpression3 = this.Spill(boundSpillSequenceBuilder2, boundExpression3, RefKind.None, false);
						ImmutableArray<BoundExpression> immutableArray = this.VisitExpressionList(ref boundSpillSequenceBuilder2, boundArrayAccess.Indices, default(ImmutableArray<RefKind>), true, false);
						boundExpression2 = boundArrayAccess.Update(boundExpression3, immutableArray, boundArrayAccess.Type);
						goto IL_00FA;
					}
					if (kind == BoundKind.Local)
					{
						goto IL_00FA;
					}
				}
				else
				{
					if (kind == BoundKind.Parameter)
					{
						goto IL_00FA;
					}
					if (kind == BoundKind.FieldAccess)
					{
						BoundFieldAccess boundFieldAccess = (BoundFieldAccess)boundExpression2;
						if (!boundFieldAccess.FieldSymbol.IsStatic)
						{
							boundExpression2 = this.<VisitAssignmentOperator>g__fieldWithSpilledReceiver|29_0(boundFieldAccess, ref boundSpillSequenceBuilder2, true);
							goto IL_00FA;
						}
						goto IL_00FA;
					}
				}
				boundExpression2 = this.Spill(boundSpillSequenceBuilder2, this.VisitExpression(ref boundSpillSequenceBuilder2, boundExpression2), RefKind.Ref, false);
				IL_00FA:
				boundSpillSequenceBuilder2.Include(boundSpillSequenceBuilder);
				boundSpillSequenceBuilder = boundSpillSequenceBuilder2;
			}
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(boundExpression2, boundExpression, node.IsRef, node.Type));
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x000CCF46 File Offset: 0x000CB146
		public override BoundNode VisitBadExpression(BoundBadExpression node)
		{
			return node;
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x000CCF49 File Offset: 0x000CB149
		public override BoundNode VisitUserDefinedConditionalLogicalOperator(BoundUserDefinedConditionalLogicalOperator node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000CCF50 File Offset: 0x000CB150
		public override BoundNode VisitBinaryOperator(BoundBinaryOperator node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Right);
			BoundExpression boundExpression2;
			if (boundSpillSequenceBuilder == null)
			{
				boundExpression2 = this.VisitExpression(ref boundSpillSequenceBuilder, node.Left);
			}
			else
			{
				SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder2 = new SpillSequenceSpiller.BoundSpillSequenceBuilder(boundSpillSequenceBuilder.Syntax, null);
				boundExpression2 = this.VisitExpression(ref boundSpillSequenceBuilder2, node.Left);
				boundExpression2 = this.Spill(boundSpillSequenceBuilder2, boundExpression2, RefKind.None, false);
				if (node.OperatorKind == BinaryOperatorKind.LogicalBoolOr || node.OperatorKind == BinaryOperatorKind.LogicalBoolAnd)
				{
					LocalSymbol localSymbol = this._F.SynthesizedLocal(node.Type, this._F.Syntax, false, RefKind.None, SynthesizedLocalKind.Spill);
					boundSpillSequenceBuilder2.AddLocal(localSymbol);
					boundSpillSequenceBuilder2.AddStatement(this._F.Assignment(this._F.Local(localSymbol), boundExpression2, false));
					boundSpillSequenceBuilder2.AddStatement(this._F.If((node.OperatorKind == BinaryOperatorKind.LogicalBoolAnd) ? this._F.Local(localSymbol) : this._F.Not(this._F.Local(localSymbol)), this.UpdateStatement(boundSpillSequenceBuilder, this._F.Assignment(this._F.Local(localSymbol), boundExpression, false)), null));
					return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder2, this._F.Local(localSymbol));
				}
				boundSpillSequenceBuilder2.Include(boundSpillSequenceBuilder);
				boundSpillSequenceBuilder = boundSpillSequenceBuilder2;
			}
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(node.OperatorKind, node.ConstantValue, node.MethodOpt, node.ResultKind, boundExpression2, boundExpression, node.Type));
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000CD0C4 File Offset: 0x000CB2C4
		public override BoundNode VisitCall(BoundCall node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			ImmutableArray<BoundExpression> immutableArray = this.VisitExpressionList(ref boundSpillSequenceBuilder, node.Arguments, node.ArgumentRefKindsOpt, false, false);
			BoundExpression boundExpression = null;
			if (boundSpillSequenceBuilder == null)
			{
				boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.ReceiverOpt);
			}
			else if (node.Method.RequiresInstanceReceiver)
			{
				SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder2 = new SpillSequenceSpiller.BoundSpillSequenceBuilder(boundSpillSequenceBuilder.Syntax, null);
				boundExpression = node.ReceiverOpt;
				RefKind refKind = SpillSequenceSpiller.ReceiverSpillRefKind(boundExpression);
				boundExpression = this.Spill(boundSpillSequenceBuilder2, this.VisitExpression(ref boundSpillSequenceBuilder2, boundExpression), refKind, false);
				boundSpillSequenceBuilder2.Include(boundSpillSequenceBuilder);
				boundSpillSequenceBuilder = boundSpillSequenceBuilder2;
			}
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(boundExpression, node.Method, immutableArray));
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000CD15C File Offset: 0x000CB35C
		private static RefKind ReceiverSpillRefKind(BoundExpression receiver)
		{
			RefKind refKind = RefKind.None;
			if (!receiver.Type.IsReferenceType && LocalRewriter.CanBePassedByReference(receiver))
			{
				refKind = (receiver.Type.IsReadOnly ? RefKind.In : RefKind.Ref);
			}
			return refKind;
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000CD194 File Offset: 0x000CB394
		public override BoundNode VisitConditionalOperator(BoundConditionalOperator node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Condition);
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder2 = null;
			BoundExpression boundExpression2 = this.VisitExpression(ref boundSpillSequenceBuilder2, node.Consequence);
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder3 = null;
			BoundExpression boundExpression3 = this.VisitExpression(ref boundSpillSequenceBuilder3, node.Alternative);
			if (boundSpillSequenceBuilder2 == null && boundSpillSequenceBuilder3 == null)
			{
				return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(node.IsRef, boundExpression, boundExpression2, boundExpression3, node.ConstantValueOpt, node.NaturalTypeOpt, node.WasTargetTyped, node.Type));
			}
			if (boundSpillSequenceBuilder == null)
			{
				boundSpillSequenceBuilder = new SpillSequenceSpiller.BoundSpillSequenceBuilder((boundSpillSequenceBuilder2 ?? boundSpillSequenceBuilder3).Syntax, null);
			}
			if (boundSpillSequenceBuilder2 == null)
			{
				boundSpillSequenceBuilder2 = new SpillSequenceSpiller.BoundSpillSequenceBuilder(boundSpillSequenceBuilder3.Syntax, null);
			}
			if (boundSpillSequenceBuilder3 == null)
			{
				boundSpillSequenceBuilder3 = new SpillSequenceSpiller.BoundSpillSequenceBuilder(boundSpillSequenceBuilder2.Syntax, null);
			}
			if (node.Type.IsVoidType())
			{
				boundSpillSequenceBuilder.AddStatement(this._F.If(boundExpression, this.UpdateStatement(boundSpillSequenceBuilder2, this._F.ExpressionStatement(boundExpression2)), this.UpdateStatement(boundSpillSequenceBuilder3, this._F.ExpressionStatement(boundExpression3))));
				return boundSpillSequenceBuilder.Update(this._F.Default(node.Type));
			}
			LocalSymbol localSymbol = this._F.SynthesizedLocal(node.Type, this._F.Syntax, false, RefKind.None, SynthesizedLocalKind.Spill);
			boundSpillSequenceBuilder.AddLocal(localSymbol);
			boundSpillSequenceBuilder.AddStatement(this._F.If(boundExpression, this.UpdateStatement(boundSpillSequenceBuilder2, this._F.Assignment(this._F.Local(localSymbol), boundExpression2, false)), this.UpdateStatement(boundSpillSequenceBuilder3, this._F.Assignment(this._F.Local(localSymbol), boundExpression3, false))));
			return boundSpillSequenceBuilder.Update(this._F.Local(localSymbol));
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000CD338 File Offset: 0x000CB538
		public override BoundNode VisitConversion(BoundConversion node)
		{
			if (node.ConversionKind == ConversionKind.AnonymousFunction && node.Type.IsExpressionTree())
			{
				return node;
			}
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Operand);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.UpdateOperand(boundExpression));
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x000CD37C File Offset: 0x000CB57C
		public override BoundNode VisitPassByCopy(BoundPassByCopy node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Expression);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(boundExpression, node.Type));
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x000CD3AD File Offset: 0x000CB5AD
		public override BoundNode VisitMethodGroup(BoundMethodGroup node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x000CD3B4 File Offset: 0x000CB5B4
		public override BoundNode VisitDelegateCreationExpression(BoundDelegateCreationExpression node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Argument);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(boundExpression, node.MethodOpt, node.IsExtensionMethod, node.Type));
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x000CD3F4 File Offset: 0x000CB5F4
		public override BoundNode VisitFieldAccess(BoundFieldAccess node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.ReceiverOpt);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(boundExpression, node.FieldSymbol, node.ConstantValueOpt, node.ResultKind, node.Type));
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x000CD438 File Offset: 0x000CB638
		public override BoundNode VisitIsOperator(BoundIsOperator node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Operand);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(boundExpression, node.TargetType, node.Conversion, node.Type));
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x000CD475 File Offset: 0x000CB675
		public override BoundNode VisitMakeRefOperator(BoundMakeRefOperator node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x000CD47C File Offset: 0x000CB67C
		public override BoundNode VisitNullCoalescingOperator(BoundNullCoalescingOperator node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.RightOperand);
			BoundExpression boundExpression2;
			if (boundSpillSequenceBuilder == null)
			{
				boundExpression2 = this.VisitExpression(ref boundSpillSequenceBuilder, node.LeftOperand);
				return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(boundExpression2, boundExpression, node.LeftConversion, node.OperatorResultKind, node.Type));
			}
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder2 = new SpillSequenceSpiller.BoundSpillSequenceBuilder(boundSpillSequenceBuilder.Syntax, null);
			boundExpression2 = this.VisitExpression(ref boundSpillSequenceBuilder2, node.LeftOperand);
			boundExpression2 = this.Spill(boundSpillSequenceBuilder2, boundExpression2, RefKind.None, false);
			LocalSymbol localSymbol = this._F.SynthesizedLocal(node.Type, this._F.Syntax, false, RefKind.None, SynthesizedLocalKind.Spill);
			boundSpillSequenceBuilder2.AddLocal(localSymbol);
			boundSpillSequenceBuilder2.AddStatement(this._F.Assignment(this._F.Local(localSymbol), boundExpression2, false));
			boundSpillSequenceBuilder2.AddStatement(this._F.If(this._F.ObjectEqual(this._F.Local(localSymbol), this._F.Null(boundExpression2.Type)), this.UpdateStatement(boundSpillSequenceBuilder, this._F.Assignment(this._F.Local(localSymbol), boundExpression, false)), null));
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder2, this._F.Local(localSymbol));
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x000CD5B4 File Offset: 0x000CB7B4
		public override BoundNode VisitLoweredConditionalAccess(BoundLoweredConditionalAccess node)
		{
			RefKind refKind = SpillSequenceSpiller.ReceiverSpillRefKind(node.Receiver);
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Receiver);
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder2 = null;
			BoundExpression boundExpression2 = this.VisitExpression(ref boundSpillSequenceBuilder2, node.WhenNotNull);
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder3 = null;
			BoundExpression boundExpression3 = this.VisitExpression(ref boundSpillSequenceBuilder3, node.WhenNullOpt);
			if (boundSpillSequenceBuilder2 == null && boundSpillSequenceBuilder3 == null)
			{
				return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(boundExpression, node.HasValueMethodOpt, boundExpression2, boundExpression3, node.Id, node.Type));
			}
			if (boundSpillSequenceBuilder == null)
			{
				boundSpillSequenceBuilder = new SpillSequenceSpiller.BoundSpillSequenceBuilder((boundSpillSequenceBuilder2 ?? boundSpillSequenceBuilder3).Syntax, null);
			}
			if (boundSpillSequenceBuilder2 == null)
			{
				boundSpillSequenceBuilder2 = new SpillSequenceSpiller.BoundSpillSequenceBuilder(boundSpillSequenceBuilder3.Syntax, null);
			}
			if (boundSpillSequenceBuilder3 == null)
			{
				boundSpillSequenceBuilder3 = new SpillSequenceSpiller.BoundSpillSequenceBuilder(boundSpillSequenceBuilder2.Syntax, null);
			}
			BoundExpression boundExpression4;
			if (boundExpression.Type.IsReferenceType || boundExpression.Type.IsValueType || refKind == RefKind.None)
			{
				boundExpression = this.Spill(boundSpillSequenceBuilder, boundExpression, RefKind.None, false);
				MethodSymbol hasValueMethodOpt = node.HasValueMethodOpt;
				if (hasValueMethodOpt == null)
				{
					boundExpression4 = this._F.ObjectNotEqual(this._F.Convert(this._F.SpecialType(SpecialType.System_Object), boundExpression), this._F.Null(this._F.SpecialType(SpecialType.System_Object)));
				}
				else
				{
					boundExpression4 = this._F.Call(boundExpression, hasValueMethodOpt);
				}
			}
			else
			{
				boundExpression = this.Spill(boundSpillSequenceBuilder, boundExpression, RefKind.Ref, false);
				LocalSymbol localSymbol = this._F.SynthesizedLocal(boundExpression.Type, this._F.Syntax, false, RefKind.None, SynthesizedLocalKind.Spill);
				boundSpillSequenceBuilder.AddLocal(localSymbol);
				BoundBinaryOperator boundBinaryOperator = this._F.ObjectNotEqual(this._F.Convert(this._F.SpecialType(SpecialType.System_Object), this._F.Default(boundExpression.Type)), this._F.Null(this._F.SpecialType(SpecialType.System_Object)));
				boundExpression4 = this._F.LogicalOr(boundBinaryOperator, this._F.MakeSequence(new BoundExpression[]
				{
					this._F.AssignmentExpression(this._F.Local(localSymbol), boundExpression, false),
					this._F.ObjectNotEqual(this._F.Convert(this._F.SpecialType(SpecialType.System_Object), this._F.Local(localSymbol)), this._F.Null(this._F.SpecialType(SpecialType.System_Object)))
				}));
				boundExpression = this._F.ComplexConditionalReceiver(boundExpression, this._F.Local(localSymbol));
			}
			if (node.Type.IsVoidType())
			{
				BoundStatement boundStatement = this.UpdateStatement(boundSpillSequenceBuilder2, this._F.ExpressionStatement(boundExpression2));
				boundStatement = SpillSequenceSpiller.ConditionalReceiverReplacer.Replace(boundStatement, boundExpression, node.Id, base.RecursionDepth);
				boundSpillSequenceBuilder.AddStatement(this._F.If(boundExpression4, boundStatement, null));
				return boundSpillSequenceBuilder.Update(this._F.Default(node.Type));
			}
			LocalSymbol localSymbol2 = this._F.SynthesizedLocal(node.Type, this._F.Syntax, false, RefKind.None, SynthesizedLocalKind.Spill);
			BoundStatement boundStatement2 = this.UpdateStatement(boundSpillSequenceBuilder2, this._F.Assignment(this._F.Local(localSymbol2), boundExpression2, false));
			boundStatement2 = SpillSequenceSpiller.ConditionalReceiverReplacer.Replace(boundStatement2, boundExpression, node.Id, base.RecursionDepth);
			boundExpression3 = boundExpression3 ?? this._F.Default(node.Type);
			boundSpillSequenceBuilder.AddLocal(localSymbol2);
			boundSpillSequenceBuilder.AddStatement(this._F.If(boundExpression4, boundStatement2, this.UpdateStatement(boundSpillSequenceBuilder3, this._F.Assignment(this._F.Local(localSymbol2), boundExpression3, false))));
			return boundSpillSequenceBuilder.Update(this._F.Local(localSymbol2));
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000CD948 File Offset: 0x000CBB48
		public override BoundNode VisitLambda(BoundLambda node)
		{
			MethodSymbol currentFunction = this._F.CurrentFunction;
			this._F.CurrentFunction = node.Symbol;
			BoundNode boundNode = base.VisitLambda(node);
			this._F.CurrentFunction = currentFunction;
			return boundNode;
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000CD988 File Offset: 0x000CBB88
		public override BoundNode VisitLocalFunctionStatement(BoundLocalFunctionStatement node)
		{
			MethodSymbol currentFunction = this._F.CurrentFunction;
			this._F.CurrentFunction = node.Symbol;
			BoundNode boundNode = base.VisitLocalFunctionStatement(node);
			this._F.CurrentFunction = currentFunction;
			return boundNode;
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000CD9C8 File Offset: 0x000CBBC8
		public override BoundNode VisitObjectCreationExpression(BoundObjectCreationExpression node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			ImmutableArray<BoundExpression> immutableArray = this.VisitExpressionList(ref boundSpillSequenceBuilder, node.Arguments, node.ArgumentRefKindsOpt, false, false);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(node.Constructor, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.Expanded, node.ArgsToParamsOpt, node.DefaultArguments, node.ConstantValueOpt, node.InitializerExpressionOpt, node.Type));
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000CDA34 File Offset: 0x000CBC34
		public override BoundNode VisitPointerElementAccess(BoundPointerElementAccess node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Index);
			BoundExpression boundExpression2;
			if (boundSpillSequenceBuilder == null)
			{
				boundExpression2 = this.VisitExpression(ref boundSpillSequenceBuilder, node.Expression);
			}
			else
			{
				SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder2 = new SpillSequenceSpiller.BoundSpillSequenceBuilder(boundSpillSequenceBuilder.Syntax, null);
				boundExpression2 = this.VisitExpression(ref boundSpillSequenceBuilder2, node.Expression);
				boundExpression2 = this.Spill(boundSpillSequenceBuilder2, boundExpression2, RefKind.None, false);
				boundSpillSequenceBuilder2.Include(boundSpillSequenceBuilder);
				boundSpillSequenceBuilder = boundSpillSequenceBuilder2;
			}
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(boundExpression2, boundExpression, node.Checked, node.Type));
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000CDAB0 File Offset: 0x000CBCB0
		public override BoundNode VisitPointerIndirectionOperator(BoundPointerIndirectionOperator node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Operand);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(boundExpression, node.Type));
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x000CDAE4 File Offset: 0x000CBCE4
		public override BoundNode VisitSequence(BoundSequence node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Value);
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder2 = null;
			ImmutableArray<BoundExpression> sideEffects = node.SideEffects;
			bool flag = boundSpillSequenceBuilder != null;
			ImmutableArray<BoundExpression> immutableArray = this.VisitExpressionList(ref boundSpillSequenceBuilder2, sideEffects, default(ImmutableArray<RefKind>), flag, true);
			if (boundSpillSequenceBuilder2 == null && boundSpillSequenceBuilder == null)
			{
				return node.Update(node.Locals, immutableArray, boundExpression, node.Type);
			}
			if (boundSpillSequenceBuilder2 == null)
			{
				boundSpillSequenceBuilder2 = new SpillSequenceSpiller.BoundSpillSequenceBuilder(boundSpillSequenceBuilder.Syntax, null);
			}
			this.PromoteAndAddLocals(boundSpillSequenceBuilder2, node.Locals);
			boundSpillSequenceBuilder2.AddExpressions(immutableArray);
			boundSpillSequenceBuilder2.Include(boundSpillSequenceBuilder);
			return boundSpillSequenceBuilder2.Update(boundExpression);
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x000CDB74 File Offset: 0x000CBD74
		public override BoundNode VisitThrowExpression(BoundThrowExpression node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Expression);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(boundExpression, node.Type));
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x000CDBA8 File Offset: 0x000CBDA8
		private void PromoteAndAddLocals(SpillSequenceSpiller.BoundSpillSequenceBuilder builder, ImmutableArray<LocalSymbol> locals)
		{
			foreach (LocalSymbol localSymbol in locals)
			{
				if (localSymbol.SynthesizedKind.IsLongLived())
				{
					builder.AddLocal(localSymbol);
				}
				else
				{
					LocalSymbol localSymbol2 = localSymbol.WithSynthesizedLocalKindAndSyntax(SynthesizedLocalKind.Spill, this._F.Syntax);
					this._tempSubstitution.Add(localSymbol, localSymbol2);
					builder.AddLocal(localSymbol2);
				}
			}
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000CDC10 File Offset: 0x000CBE10
		public override BoundNode VisitUnaryOperator(BoundUnaryOperator node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Operand);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(node.OperatorKind, boundExpression, node.ConstantValueOpt, node.MethodOpt, node.ResultKind, node.Type));
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000CDC5C File Offset: 0x000CBE5C
		public override BoundNode VisitReadOnlySpanFromArray(BoundReadOnlySpanFromArray node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Operand);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(boundExpression, node.ConversionMethod, node.Type));
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000CDC94 File Offset: 0x000CBE94
		public override BoundNode VisitSequencePointExpression(BoundSequencePointExpression node)
		{
			SpillSequenceSpiller.BoundSpillSequenceBuilder boundSpillSequenceBuilder = null;
			BoundExpression boundExpression = this.VisitExpression(ref boundSpillSequenceBuilder, node.Expression);
			return SpillSequenceSpiller.UpdateExpression(boundSpillSequenceBuilder, node.Update(boundExpression, node.Type));
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x000CDCC8 File Offset: 0x000CBEC8
		[CompilerGenerated]
		private BoundExpression <VisitAssignmentOperator>g__fieldWithSpilledReceiver|29_0(BoundFieldAccess field, ref SpillSequenceSpiller.BoundSpillSequenceBuilder leftBuilder, bool isAssignmentTarget)
		{
			bool flag = false;
			if (!field.FieldSymbol.IsStatic)
			{
				BoundExpression boundExpression;
				if (field.FieldSymbol.ContainingType.IsReferenceType)
				{
					boundExpression = this.Spill(leftBuilder, this.VisitExpression(ref leftBuilder, field.ReceiverOpt), RefKind.None, false);
					flag = !isAssignmentTarget;
				}
				else
				{
					BoundArrayAccess boundArrayAccess = field.ReceiverOpt as BoundArrayAccess;
					if (boundArrayAccess != null)
					{
						BoundExpression boundExpression2 = this.VisitExpression(ref leftBuilder, boundArrayAccess.Expression);
						boundExpression2 = this.Spill(leftBuilder, boundExpression2, RefKind.None, false);
						ImmutableArray<BoundExpression> immutableArray = this.VisitExpressionList(ref leftBuilder, boundArrayAccess.Indices, default(ImmutableArray<RefKind>), true, false);
						boundExpression = boundArrayAccess.Update(boundExpression2, immutableArray, boundArrayAccess.Type);
						this.Spill(leftBuilder, boundExpression, RefKind.None, true);
					}
					else
					{
						BoundFieldAccess boundFieldAccess = field.ReceiverOpt as BoundFieldAccess;
						if (boundFieldAccess != null)
						{
							boundExpression = this.<VisitAssignmentOperator>g__fieldWithSpilledReceiver|29_0(boundFieldAccess, ref leftBuilder, false);
						}
						else
						{
							boundExpression = this.Spill(leftBuilder, this.VisitExpression(ref leftBuilder, field.ReceiverOpt), RefKind.Ref, false);
						}
					}
				}
				field = field.Update(boundExpression, field.FieldSymbol, field.ConstantValueOpt, field.ResultKind, field.Type);
			}
			if (flag)
			{
				this.Spill(leftBuilder, field, RefKind.None, true);
			}
			return field;
		}

		// Token: 0x040010E3 RID: 4323
		private const BoundKind SpillSequenceBuilderKind = (BoundKind)255;

		// Token: 0x040010E4 RID: 4324
		private readonly SyntheticBoundNodeFactory _F;

		// Token: 0x040010E5 RID: 4325
		private readonly PooledDictionary<LocalSymbol, LocalSymbol> _tempSubstitution;

		// Token: 0x0200073D RID: 1853
		private sealed class BoundSpillSequenceBuilder : BoundExpression
		{
			// Token: 0x0600863C RID: 34364 RVA: 0x001E80E9 File Offset: 0x001E62E9
			public BoundSpillSequenceBuilder(SyntaxNode syntax, BoundExpression value = null)
				: base((BoundKind)255, syntax, (value != null) ? value.Type : null)
			{
				this.Value = value;
			}

			// Token: 0x170026C7 RID: 9927
			// (get) Token: 0x0600863D RID: 34365 RVA: 0x001E810A File Offset: 0x001E630A
			public bool HasStatements
			{
				get
				{
					return this._statements != null;
				}
			}

			// Token: 0x170026C8 RID: 9928
			// (get) Token: 0x0600863E RID: 34366 RVA: 0x001E8115 File Offset: 0x001E6315
			public bool HasLocals
			{
				get
				{
					return this._locals != null;
				}
			}

			// Token: 0x0600863F RID: 34367 RVA: 0x001E8120 File Offset: 0x001E6320
			public ImmutableArray<LocalSymbol> GetLocals()
			{
				if (this._locals != null)
				{
					return this._locals.ToImmutable();
				}
				return ImmutableArray<LocalSymbol>.Empty;
			}

			// Token: 0x06008640 RID: 34368 RVA: 0x001E813B File Offset: 0x001E633B
			public ImmutableArray<BoundStatement> GetStatements()
			{
				if (this._statements == null)
				{
					return ImmutableArray<BoundStatement>.Empty;
				}
				return this._statements.ToImmutable();
			}

			// Token: 0x06008641 RID: 34369 RVA: 0x001E8156 File Offset: 0x001E6356
			internal SpillSequenceSpiller.BoundSpillSequenceBuilder Update(BoundExpression value)
			{
				return new SpillSequenceSpiller.BoundSpillSequenceBuilder(this.Syntax, value)
				{
					_locals = this._locals,
					_statements = this._statements
				};
			}

			// Token: 0x06008642 RID: 34370 RVA: 0x001E817C File Offset: 0x001E637C
			public void Free()
			{
				if (this._locals != null)
				{
					this._locals.Free();
				}
				if (this._statements != null)
				{
					this._statements.Free();
				}
			}

			// Token: 0x06008643 RID: 34371 RVA: 0x001E81A4 File Offset: 0x001E63A4
			internal void Include(SpillSequenceSpiller.BoundSpillSequenceBuilder other)
			{
				if (other != null)
				{
					SpillSequenceSpiller.BoundSpillSequenceBuilder.IncludeAndFree<LocalSymbol>(ref this._locals, ref other._locals);
					SpillSequenceSpiller.BoundSpillSequenceBuilder.IncludeAndFree<BoundStatement>(ref this._statements, ref other._statements);
				}
			}

			// Token: 0x06008644 RID: 34372 RVA: 0x001E81CB File Offset: 0x001E63CB
			private static void IncludeAndFree<T>(ref ArrayBuilder<T> left, ref ArrayBuilder<T> right)
			{
				if (right == null)
				{
					return;
				}
				if (left == null)
				{
					left = right;
					return;
				}
				left.AddRange(right);
				right.Free();
			}

			// Token: 0x06008645 RID: 34373 RVA: 0x001E81EB File Offset: 0x001E63EB
			public void AddLocal(LocalSymbol local)
			{
				if (this._locals == null)
				{
					this._locals = ArrayBuilder<LocalSymbol>.GetInstance();
				}
				this._locals.Add(local);
			}

			// Token: 0x06008646 RID: 34374 RVA: 0x001E820C File Offset: 0x001E640C
			public void AddLocals(ImmutableArray<LocalSymbol> locals)
			{
				foreach (LocalSymbol localSymbol in locals)
				{
					this.AddLocal(localSymbol);
				}
			}

			// Token: 0x06008647 RID: 34375 RVA: 0x001E823B File Offset: 0x001E643B
			public void AddStatement(BoundStatement statement)
			{
				if (this._statements == null)
				{
					this._statements = ArrayBuilder<BoundStatement>.GetInstance();
				}
				this._statements.Add(statement);
			}

			// Token: 0x06008648 RID: 34376 RVA: 0x001E825C File Offset: 0x001E645C
			public void AddStatements(ImmutableArray<BoundStatement> statements)
			{
				foreach (BoundStatement boundStatement in statements)
				{
					this.AddStatement(boundStatement);
				}
			}

			// Token: 0x06008649 RID: 34377 RVA: 0x001E828C File Offset: 0x001E648C
			internal void AddExpressions(ImmutableArray<BoundExpression> expressions)
			{
				foreach (BoundExpression boundExpression in expressions)
				{
					this.AddStatement(new BoundExpressionStatement(boundExpression.Syntax, boundExpression, false)
					{
						WasCompilerGenerated = true
					});
				}
			}

			// Token: 0x04002305 RID: 8965
			public readonly BoundExpression Value;

			// Token: 0x04002306 RID: 8966
			private ArrayBuilder<LocalSymbol> _locals;

			// Token: 0x04002307 RID: 8967
			private ArrayBuilder<BoundStatement> _statements;
		}

		// Token: 0x0200073E RID: 1854
		private sealed class LocalSubstituter : BoundTreeRewriterWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator
		{
			// Token: 0x0600864A RID: 34378 RVA: 0x001E82CE File Offset: 0x001E64CE
			private LocalSubstituter(PooledDictionary<LocalSymbol, LocalSymbol> tempSubstitution, int recursionDepth = 0)
				: base(recursionDepth)
			{
				this._tempSubstitution = tempSubstitution;
			}

			// Token: 0x0600864B RID: 34379 RVA: 0x001E82DE File Offset: 0x001E64DE
			public static BoundNode Rewrite(PooledDictionary<LocalSymbol, LocalSymbol> tempSubstitution, BoundNode node)
			{
				if (tempSubstitution.Count == 0)
				{
					return node;
				}
				return new SpillSequenceSpiller.LocalSubstituter(tempSubstitution, 0).Visit(node);
			}

			// Token: 0x0600864C RID: 34380 RVA: 0x001E82F8 File Offset: 0x001E64F8
			public override BoundNode VisitLocal(BoundLocal node)
			{
				LocalSymbol localSymbol;
				if (!node.LocalSymbol.SynthesizedKind.IsLongLived() && this._tempSubstitution.TryGetValue(node.LocalSymbol, ref localSymbol))
				{
					return node.Update(localSymbol, node.ConstantValueOpt, node.Type);
				}
				return base.VisitLocal(node);
			}

			// Token: 0x04002308 RID: 8968
			private readonly PooledDictionary<LocalSymbol, LocalSymbol> _tempSubstitution;
		}

		// Token: 0x0200073F RID: 1855
		private sealed class ConditionalReceiverReplacer : BoundTreeRewriterWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator
		{
			// Token: 0x0600864D RID: 34381 RVA: 0x001E8347 File Offset: 0x001E6547
			private ConditionalReceiverReplacer(BoundExpression receiver, int receiverId, int recursionDepth)
				: base(recursionDepth)
			{
				this._receiver = receiver;
				this._receiverId = receiverId;
			}

			// Token: 0x0600864E RID: 34382 RVA: 0x001E835E File Offset: 0x001E655E
			public static BoundStatement Replace(BoundNode node, BoundExpression receiver, int receiverID, int recursionDepth)
			{
				return (BoundStatement)new SpillSequenceSpiller.ConditionalReceiverReplacer(receiver, receiverID, recursionDepth).Visit(node);
			}

			// Token: 0x0600864F RID: 34383 RVA: 0x001E8373 File Offset: 0x001E6573
			public override BoundNode VisitConditionalReceiver(BoundConditionalReceiver node)
			{
				if (node.Id == this._receiverId)
				{
					return this._receiver;
				}
				return node;
			}

			// Token: 0x04002309 RID: 8969
			private readonly BoundExpression _receiver;

			// Token: 0x0400230A RID: 8970
			private readonly int _receiverId;
		}
	}
}
