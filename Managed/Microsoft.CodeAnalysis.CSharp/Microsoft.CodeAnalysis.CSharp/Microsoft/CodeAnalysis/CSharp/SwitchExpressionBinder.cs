using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000074 RID: 116
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class SwitchExpressionBinder : Binder
	{
		// Token: 0x06000AB1 RID: 2737 RVA: 0x00050273 File Offset: 0x0004E473
		internal SwitchExpressionBinder(SwitchExpressionSyntax switchExpressionSyntax, Binder next)
			: base(next, null)
		{
			this.SwitchExpressionSyntax = switchExpressionSyntax;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00050284 File Offset: 0x0004E484
		internal override BoundExpression BindSwitchExpressionCore(SwitchExpressionSyntax node, Binder originalBinder, BindingDiagnosticBag diagnostics)
		{
			BoundExpression boundExpression = this.BindSwitchGoverningExpression(diagnostics);
			ImmutableArray<BoundSwitchExpressionArm> immutableArray = this.BindSwitchExpressionArms(node, originalBinder, boundExpression, diagnostics);
			TypeSymbol typeSymbol = this.InferResultType(immutableArray, diagnostics);
			BoundDecisionDag boundDecisionDag;
			LabelSymbol labelSymbol;
			bool flag = this.CheckSwitchExpressionExhaustive(node, boundExpression, immutableArray, out boundDecisionDag, out labelSymbol, diagnostics);
			boundDecisionDag = boundDecisionDag.SimplifyDecisionDagIfConstantInput(boundExpression);
			return new BoundUnconvertedSwitchExpression(node, boundExpression, immutableArray, boundDecisionDag, labelSymbol, flag, typeSymbol, false);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x000502D8 File Offset: 0x0004E4D8
		private bool CheckSwitchExpressionExhaustive(SwitchExpressionSyntax node, BoundExpression boundInputExpression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSwitchExpressionArm> switchArms, out BoundDecisionDag decisionDag, [global::System.Runtime.CompilerServices.Nullable(2)] [NotNullWhen(true)] out LabelSymbol defaultLabel, BindingDiagnosticBag diagnostics)
		{
			defaultLabel = new GeneratedLabelSymbol("default");
			decisionDag = DecisionDagBuilder.CreateDecisionDagForSwitchExpression(base.Compilation, node, boundInputExpression, switchArms, defaultLabel, diagnostics);
			ImmutableHashSet<LabelSymbol> reachableLabels = decisionDag.ReachableLabels;
			bool flag = false;
			foreach (BoundSwitchExpressionArm boundSwitchExpressionArm in switchArms)
			{
				flag |= boundSwitchExpressionArm.HasErrors;
				if (!flag && !reachableLabels.Contains(boundSwitchExpressionArm.Label))
				{
					diagnostics.Add(ErrorCode.ERR_SwitchArmSubsumed, boundSwitchExpressionArm.Pattern.Syntax.Location);
				}
			}
			if (!reachableLabels.Contains(defaultLabel))
			{
				defaultLabel = null;
				return false;
			}
			if (flag)
			{
				return true;
			}
			ImmutableArray<BoundDecisionDagNode> immutableArray;
			TopologicalSort.TryIterativeSort<BoundDecisionDagNode>(new BoundDecisionDagNode[] { decisionDag.RootNode }, new Func<BoundDecisionDagNode, ImmutableArray<BoundDecisionDagNode>>(SwitchExpressionBinder.<CheckSwitchExpressionExhaustive>g__nonNullSuccessors|3_0), out immutableArray);
			foreach (BoundDecisionDagNode boundDecisionDagNode in immutableArray)
			{
				BoundLeafDecisionDagNode boundLeafDecisionDagNode = boundDecisionDagNode as BoundLeafDecisionDagNode;
				if (boundLeafDecisionDagNode != null && boundLeafDecisionDagNode.Label == defaultLabel)
				{
					bool flag2;
					bool flag3;
					string text = PatternExplainer.SamplePatternForPathToDagNode(BoundDagTemp.ForOriginalInput(boundInputExpression), immutableArray, boundDecisionDagNode, false, out flag2, out flag3);
					ErrorCode errorCode = (flag2 ? ErrorCode.WRN_SwitchExpressionNotExhaustiveWithWhen : (flag3 ? ErrorCode.WRN_SwitchExpressionNotExhaustiveWithUnnamedEnumValue : ErrorCode.WRN_SwitchExpressionNotExhaustive));
					diagnostics.Add(errorCode, node.SwitchKeyword.GetLocation(), new object[] { text });
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0005043C File Offset: 0x0004E63C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSymbol InferResultType([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSwitchExpressionArm> switchCases, BindingDiagnosticBag diagnostics)
		{
			PooledHashSet<TypeSymbol> pooledSymbolHashSetInstance = SpecializedSymbolCollections.GetPooledSymbolHashSetInstance<TypeSymbol>();
			ArrayBuilder<TypeSymbol> instance = ArrayBuilder<TypeSymbol>.GetInstance();
			foreach (BoundSwitchExpressionArm boundSwitchExpressionArm in switchCases)
			{
				TypeSymbol type = boundSwitchExpressionArm.Value.Type;
				if (type != null && pooledSymbolHashSetInstance.Add(type))
				{
					instance.Add(type);
				}
			}
			pooledSymbolHashSetInstance.Free();
			CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = base.GetNewCompoundUseSiteInfo(diagnostics);
			TypeSymbol typeSymbol = BestTypeInferrer.GetBestType(instance, base.Conversions, ref newCompoundUseSiteInfo);
			instance.Free();
			if (typeSymbol != null)
			{
				foreach (BoundSwitchExpressionArm boundSwitchExpressionArm2 in switchCases)
				{
					if (!base.Conversions.ClassifyImplicitConversionFromExpression(boundSwitchExpressionArm2.Value, typeSymbol, ref newCompoundUseSiteInfo).Exists)
					{
						typeSymbol = null;
						break;
					}
				}
			}
			diagnostics.Add(this.SwitchExpressionSyntax, newCompoundUseSiteInfo);
			return typeSymbol;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00050508 File Offset: 0x0004E708
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<BoundSwitchExpressionArm> BindSwitchExpressionArms(SwitchExpressionSyntax node, Binder originalBinder, BoundExpression inputExpression, BindingDiagnosticBag diagnostics)
		{
			ArrayBuilder<BoundSwitchExpressionArm> instance = ArrayBuilder<BoundSwitchExpressionArm>.GetInstance();
			ValueTuple<TypeSymbol, uint> inputTypeAndValEscape = this.GetInputTypeAndValEscape(inputExpression);
			TypeSymbol item = inputTypeAndValEscape.Item1;
			uint item2 = inputTypeAndValEscape.Item2;
			foreach (SwitchExpressionArmSyntax switchExpressionArmSyntax in node.Arms)
			{
				BoundSwitchExpressionArm boundSwitchExpressionArm = originalBinder.GetRequiredBinder(switchExpressionArmSyntax).BindSwitchExpressionArm(switchExpressionArmSyntax, item, item2, diagnostics);
				instance.Add(boundSwitchExpressionArm);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00050576 File Offset: 0x0004E776
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: TupleElementNames(new string[] { "GoverningType", "GoverningValEscape" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal ValueTuple<TypeSymbol, uint> GetInputTypeAndValEscape(BoundExpression inputExpression = null)
		{
			if (inputExpression == null)
			{
				inputExpression = this.BindSwitchGoverningExpression(BindingDiagnosticBag.Discarded);
			}
			return new ValueTuple<TypeSymbol, uint>(inputExpression.Type, Binder.GetValEscape(inputExpression, this.LocalScopeDepth));
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000505A0 File Offset: 0x0004E7A0
		private BoundExpression BindSwitchGoverningExpression(BindingDiagnosticBag diagnostics)
		{
			BoundExpression boundExpression = base.BindRValueWithoutTargetType(this.SwitchExpressionSyntax.GoverningExpression, diagnostics, true);
			if (boundExpression.Type == null || boundExpression.Type.IsVoidType())
			{
				diagnostics.Add(ErrorCode.ERR_BadPatternExpression, this.SwitchExpressionSyntax.GoverningExpression.Location, new object[] { boundExpression.Display });
				boundExpression = base.GenerateConversionForAssignment(base.CreateErrorType(""), boundExpression, diagnostics, false, false);
			}
			return boundExpression;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00050618 File Offset: 0x0004E818
		[CompilerGenerated]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<BoundDecisionDagNode> <CheckSwitchExpressionExhaustive>g__nonNullSuccessors|3_0(BoundDecisionDagNode n)
		{
			BoundTestDecisionDagNode boundTestDecisionDagNode = n as BoundTestDecisionDagNode;
			if (boundTestDecisionDagNode == null)
			{
				return BoundDecisionDag.Successors(n);
			}
			BoundDagTest test = boundTestDecisionDagNode.Test;
			if (test is BoundDagNonNullTest)
			{
				return ImmutableArray.Create<BoundDecisionDagNode>(boundTestDecisionDagNode.WhenTrue);
			}
			if (!(test is BoundDagExplicitNullTest))
			{
				return BoundDecisionDag.Successors(n);
			}
			return ImmutableArray.Create<BoundDecisionDagNode>(boundTestDecisionDagNode.WhenFalse);
		}

		// Token: 0x04000472 RID: 1138
		private readonly SwitchExpressionSyntax SwitchExpressionSyntax;
	}
}
