using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.FlowAnalysis
{
	// Token: 0x020002DD RID: 733
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	internal sealed class ControlFlowGraphBuilder : OperationVisitor<int?, IOperation>
	{
		// Token: 0x06001E5F RID: 7775 RVA: 0x00057795 File Offset: 0x00055995
		private ControlFlowGraphBuilder.Context GetCurrentContext()
		{
			IOperation implicitInstance = this._currentImplicitInstance.ImplicitInstance;
			INamedTypeSymbol anonymousType = this._currentImplicitInstance.AnonymousType;
			PooledDictionary<IPropertySymbol, IOperation> anonymousTypePropertyValues = this._currentImplicitInstance.AnonymousTypePropertyValues;
			return new ControlFlowGraphBuilder.Context(implicitInstance, anonymousType, (anonymousTypePropertyValues != null) ? anonymousTypePropertyValues.ToImmutableArray<KeyValuePair<IPropertySymbol, IOperation>>() : ImmutableArray<KeyValuePair<IPropertySymbol, IOperation>>.Empty);
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x000577CD File Offset: 0x000559CD
		private void SetCurrentContext(in ControlFlowGraphBuilder.Context context)
		{
			this._currentImplicitInstance = new ControlFlowGraphBuilder.ImplicitInstanceInfo(in context);
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x000577DC File Offset: 0x000559DC
		private ControlFlowGraphBuilder(Compilation compilation, [Nullable(2)] ControlFlowGraphBuilder.CaptureIdDispenser captureIdDispenser, ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder> blocks)
		{
			this._compilation = compilation;
			this._captureIdDispenser = captureIdDispenser ?? new ControlFlowGraphBuilder.CaptureIdDispenser();
			this._blocks = blocks;
			this._regionMap = PooledDictionary<ControlFlowGraphBuilder.BasicBlockBuilder, ControlFlowGraphBuilder.RegionBuilder>.GetInstance();
			this._evalStack = ArrayBuilder<ValueTuple<ControlFlowGraphBuilder.EvalStackFrame, IOperation>>.GetInstance();
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x0005783B File Offset: 0x00055A3B
		private ControlFlowGraphBuilder.RegionBuilder CurrentRegionRequired
		{
			get
			{
				return this._currentRegion;
			}
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x00057843 File Offset: 0x00055A43
		private bool IsImplicit(IOperation operation)
		{
			return this._forceImplicit || operation.IsImplicit;
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x00057858 File Offset: 0x00055A58
		[NullableContext(2)]
		[return: Nullable(1)]
		public static ControlFlowGraph Create([Nullable(1)] IOperation body, ControlFlowGraph parent = null, ControlFlowRegion enclosing = null, ControlFlowGraphBuilder.CaptureIdDispenser captureIdDispenser = null, in ControlFlowGraphBuilder.Context context = null)
		{
			ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder> instance = ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder>.GetInstance();
			ControlFlowGraphBuilder controlFlowGraphBuilder = new ControlFlowGraphBuilder(((Operation)body).OwningSemanticModel.Compilation, captureIdDispenser, instance);
			ControlFlowGraphBuilder.RegionBuilder regionBuilder = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.Root, null, default(ImmutableArray<ILocalSymbol>), false);
			controlFlowGraphBuilder.EnterRegion(regionBuilder, false);
			controlFlowGraphBuilder.AppendNewBlock(controlFlowGraphBuilder._entry, false);
			controlFlowGraphBuilder._currentBasicBlock = null;
			controlFlowGraphBuilder.SetCurrentContext(in context);
			controlFlowGraphBuilder.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, default(ImmutableArray<ILocalSymbol>), false), false);
			OperationKind kind = body.Kind;
			if (kind != OperationKind.LocalFunction)
			{
				if (kind != OperationKind.AnonymousFunction)
				{
					controlFlowGraphBuilder.VisitStatement(body);
				}
				else
				{
					IAnonymousFunctionOperation anonymousFunctionOperation = (IAnonymousFunctionOperation)body;
					controlFlowGraphBuilder.VisitStatement(anonymousFunctionOperation.Body);
				}
			}
			else
			{
				controlFlowGraphBuilder.VisitLocalFunctionAsRoot((ILocalFunctionOperation)body);
			}
			controlFlowGraphBuilder.LeaveRegion();
			controlFlowGraphBuilder.AppendNewBlock(controlFlowGraphBuilder._exit, true);
			controlFlowGraphBuilder.LeaveRegion();
			controlFlowGraphBuilder._currentImplicitInstance.Free();
			ControlFlowGraphBuilder.CheckUnresolvedBranches(instance, controlFlowGraphBuilder._labeledBlocks);
			ControlFlowGraphBuilder.Pack(instance, regionBuilder, controlFlowGraphBuilder._regionMap);
			ArrayBuilder<IMethodSymbol> instance2 = ArrayBuilder<IMethodSymbol>.GetInstance();
			ImmutableDictionary<IMethodSymbol, ValueTuple<ControlFlowRegion, ILocalFunctionOperation, int>>.Builder builder = ImmutableDictionary.CreateBuilder<IMethodSymbol, ValueTuple<ControlFlowRegion, ILocalFunctionOperation, int>>();
			ImmutableDictionary<IFlowAnonymousFunctionOperation, ValueTuple<ControlFlowRegion, int>>.Builder builder2 = null;
			if (controlFlowGraphBuilder._haveAnonymousFunction)
			{
				builder2 = ImmutableDictionary.CreateBuilder<IFlowAnonymousFunctionOperation, ValueTuple<ControlFlowRegion, int>>();
			}
			ControlFlowRegion controlFlowRegion = regionBuilder.ToImmutableRegionAndFree(instance, instance2, builder, builder2, enclosing);
			ControlFlowGraphBuilder.MarkReachableBlocks(instance);
			controlFlowGraphBuilder._evalStack.Free();
			controlFlowGraphBuilder._regionMap.Free();
			PooledDictionary<ILabelSymbol, ControlFlowGraphBuilder.BasicBlockBuilder> labeledBlocks = controlFlowGraphBuilder._labeledBlocks;
			if (labeledBlocks != null)
			{
				labeledBlocks.Free();
			}
			return new ControlFlowGraph(body, parent, controlFlowGraphBuilder._captureIdDispenser, ControlFlowGraphBuilder.ToImmutableBlocks(instance), controlFlowRegion, instance2.ToImmutableAndFree(), builder.ToImmutable(), ((builder2 != null) ? builder2.ToImmutable() : null) ?? ImmutableDictionary<IFlowAnonymousFunctionOperation, ValueTuple<ControlFlowRegion, int>>.Empty);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x000579EC File Offset: 0x00055BEC
		[return: Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<BasicBlock> ToImmutableBlocks(ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder> blockBuilders)
		{
			ControlFlowGraphBuilder.<>c__DisplayClass31_0 CS$<>8__locals1;
			CS$<>8__locals1.builder = ArrayBuilder<BasicBlock>.GetInstance(blockBuilders.Count);
			foreach (ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder in blockBuilders)
			{
				CS$<>8__locals1.builder.Add(basicBlockBuilder.ToImmutable());
			}
			foreach (ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 in blockBuilders)
			{
				ControlFlowBranch controlFlowBranch = ControlFlowGraphBuilder.<ToImmutableBlocks>g__getFallThroughSuccessor|31_0(basicBlockBuilder2, ref CS$<>8__locals1);
				ControlFlowBranch controlFlowBranch2 = ControlFlowGraphBuilder.<ToImmutableBlocks>g__getConditionalSuccessor|31_1(basicBlockBuilder2, ref CS$<>8__locals1);
				CS$<>8__locals1.builder[basicBlockBuilder2.Ordinal].SetSuccessors(controlFlowBranch, controlFlowBranch2);
			}
			foreach (ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder3 in blockBuilders)
			{
				CS$<>8__locals1.builder[basicBlockBuilder3.Ordinal].SetPredecessors(basicBlockBuilder3.ConvertPredecessorsToBranches(CS$<>8__locals1.builder));
			}
			return CS$<>8__locals1.builder.ToImmutableAndFree();
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x00057AC8 File Offset: 0x00055CC8
		private static void MarkReachableBlocks(ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder> blocks)
		{
			PooledDictionary<ControlFlowRegion, bool> instance = PooledDictionary<ControlFlowRegion, bool>.GetInstance();
			PooledHashSet<ControlFlowRegion> instance2 = PooledHashSet<ControlFlowRegion>.GetInstance();
			bool flag;
			ControlFlowGraphBuilder.MarkReachableBlocks(blocks, 0, blocks.Count - 1, null, instance, instance2, out flag);
			instance.Free();
			instance2.Free();
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x00057B04 File Offset: 0x00055D04
		private static BitVector MarkReachableBlocks(ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder> blocks, int firstBlockOrdinal, int lastBlockOrdinal, [Nullable(new byte[] { 2, 1 })] ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder> outOfRangeBlocksToVisit, PooledDictionary<ControlFlowRegion, bool> continueDispatchAfterFinally, PooledHashSet<ControlFlowRegion> dispatchedExceptionsFromRegions, out bool fellThrough)
		{
			ControlFlowGraphBuilder.<>c__DisplayClass33_0 CS$<>8__locals1;
			CS$<>8__locals1.continueDispatchAfterFinally = continueDispatchAfterFinally;
			CS$<>8__locals1.blocks = blocks;
			CS$<>8__locals1.dispatchedExceptionsFromRegions = dispatchedExceptionsFromRegions;
			CS$<>8__locals1.visited = BitVector.Empty;
			CS$<>8__locals1.toVisit = ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder>.GetInstance();
			fellThrough = false;
			CS$<>8__locals1.toVisit.Push(CS$<>8__locals1.blocks[firstBlockOrdinal]);
			do
			{
				ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = CS$<>8__locals1.toVisit.Pop<ControlFlowGraphBuilder.BasicBlockBuilder>();
				if (basicBlockBuilder.Ordinal < firstBlockOrdinal || basicBlockBuilder.Ordinal > lastBlockOrdinal)
				{
					outOfRangeBlocksToVisit.Push(basicBlockBuilder);
				}
				else if (!CS$<>8__locals1.visited[basicBlockBuilder.Ordinal])
				{
					CS$<>8__locals1.visited[basicBlockBuilder.Ordinal] = true;
					basicBlockBuilder.IsReachable = true;
					bool flag = true;
					if (basicBlockBuilder.HasCondition)
					{
						ConstantValue constantValue = basicBlockBuilder.BranchValue.GetConstantValue();
						if (constantValue != null && constantValue.IsBoolean)
						{
							bool booleanValue = constantValue.BooleanValue;
							if (booleanValue == (basicBlockBuilder.ConditionKind == ControlFlowConditionKind.WhenTrue))
							{
								ControlFlowGraphBuilder.<MarkReachableBlocks>g__followBranch|33_0(basicBlockBuilder, in basicBlockBuilder.Conditional, ref CS$<>8__locals1);
								flag = false;
							}
						}
						else
						{
							ControlFlowGraphBuilder.<MarkReachableBlocks>g__followBranch|33_0(basicBlockBuilder, in basicBlockBuilder.Conditional, ref CS$<>8__locals1);
						}
					}
					if (flag)
					{
						ControlFlowGraphBuilder.BasicBlockBuilder.Branch fallThrough = basicBlockBuilder.FallThrough;
						ControlFlowGraphBuilder.<MarkReachableBlocks>g__followBranch|33_0(basicBlockBuilder, in fallThrough, ref CS$<>8__locals1);
						if (basicBlockBuilder.Ordinal == lastBlockOrdinal && fallThrough.Kind != ControlFlowBranchSemantics.Throw && fallThrough.Kind != ControlFlowBranchSemantics.Rethrow)
						{
							fellThrough = true;
						}
					}
					ControlFlowGraphBuilder.<MarkReachableBlocks>g__dispatchException|33_3(basicBlockBuilder.Region, ref CS$<>8__locals1);
				}
			}
			while (CS$<>8__locals1.toVisit.Count != 0);
			CS$<>8__locals1.toVisit.Free();
			return CS$<>8__locals1.visited;
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x00057C78 File Offset: 0x00055E78
		private static void Pack(ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder> blocks, ControlFlowGraphBuilder.RegionBuilder root, PooledDictionary<ControlFlowGraphBuilder.BasicBlockBuilder, ControlFlowGraphBuilder.RegionBuilder> regionMap)
		{
			bool flag = true;
			for (;;)
			{
				flag |= ControlFlowGraphBuilder.PackRegions(root, blocks, regionMap);
				if (!flag || !ControlFlowGraphBuilder.PackBlocks(blocks, regionMap))
				{
					break;
				}
				flag = false;
			}
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x00057CA4 File Offset: 0x00055EA4
		private static bool PackRegions(ControlFlowGraphBuilder.RegionBuilder root, ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder> blocks, PooledDictionary<ControlFlowGraphBuilder.BasicBlockBuilder, ControlFlowGraphBuilder.RegionBuilder> regionMap)
		{
			ControlFlowGraphBuilder.<>c__DisplayClass35_0 CS$<>8__locals1;
			CS$<>8__locals1.blocks = blocks;
			CS$<>8__locals1.regionMap = regionMap;
			return ControlFlowGraphBuilder.<PackRegions>g__PackRegion|35_0(root, ref CS$<>8__locals1);
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x00057CCC File Offset: 0x00055ECC
		private static void MergeSubRegionAndFree(ControlFlowGraphBuilder.RegionBuilder subRegion, ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder> blocks, PooledDictionary<ControlFlowGraphBuilder.BasicBlockBuilder, ControlFlowGraphBuilder.RegionBuilder> regionMap, bool canHaveEmptyRegion = false)
		{
			ControlFlowGraphBuilder.RegionBuilder enclosing = subRegion.Enclosing;
			if (subRegion.IsEmpty)
			{
				enclosing.Remove(subRegion);
				subRegion.Free();
				return;
			}
			int num = subRegion.FirstBlock.Ordinal;
			if (subRegion.HasRegions)
			{
				foreach (ControlFlowGraphBuilder.RegionBuilder regionBuilder in subRegion.Regions)
				{
					for (int i = num; i < regionBuilder.FirstBlock.Ordinal; i++)
					{
						regionMap[blocks[i]] = enclosing;
					}
					num = regionBuilder.LastBlock.Ordinal + 1;
				}
				enclosing.ReplaceRegion(subRegion, subRegion.Regions);
			}
			else
			{
				enclosing.Remove(subRegion);
			}
			for (int j = num; j <= subRegion.LastBlock.Ordinal; j++)
			{
				regionMap[blocks[j]] = enclosing;
			}
			subRegion.Free();
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x00057DA8 File Offset: 0x00055FA8
		private static bool PackBlocks(ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder> blocks, PooledDictionary<ControlFlowGraphBuilder.BasicBlockBuilder, ControlFlowGraphBuilder.RegionBuilder> regionMap)
		{
			ControlFlowGraphBuilder.<>c__DisplayClass37_0 CS$<>8__locals1;
			CS$<>8__locals1.blocks = blocks;
			CS$<>8__locals1.regionMap = regionMap;
			CS$<>8__locals1.fromCurrent = null;
			CS$<>8__locals1.fromDestination = null;
			CS$<>8__locals1.fromPredecessor = null;
			ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder> arrayBuilder = null;
			bool flag = false;
			bool flag2;
			do
			{
				flag2 = false;
				int num = CS$<>8__locals1.blocks.Count - 1;
				int i = 1;
				while (i < num)
				{
					ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = CS$<>8__locals1.blocks[i];
					basicBlockBuilder.Ordinal = i;
					if (!basicBlockBuilder.HasStatements)
					{
						goto IL_00DE;
					}
					ControlFlowGraphBuilder.BasicBlockBuilder singletonPredecessorOrDefault = basicBlockBuilder.GetSingletonPredecessorOrDefault();
					if (singletonPredecessorOrDefault != null && !singletonPredecessorOrDefault.HasCondition && singletonPredecessorOrDefault.Ordinal < basicBlockBuilder.Ordinal && singletonPredecessorOrDefault.Kind != BasicBlockKind.Entry && singletonPredecessorOrDefault.FallThrough.Destination == basicBlockBuilder && CS$<>8__locals1.regionMap[singletonPredecessorOrDefault] == CS$<>8__locals1.regionMap[basicBlockBuilder])
					{
						singletonPredecessorOrDefault.MoveStatementsFrom(basicBlockBuilder);
						flag2 = true;
						goto IL_00DE;
					}
					IL_055C:
					i++;
					continue;
					IL_00DE:
					ref ControlFlowGraphBuilder.BasicBlockBuilder.Branch ptr = ref basicBlockBuilder.FallThrough;
					if (!basicBlockBuilder.HasCondition)
					{
						if (ptr.Destination == basicBlockBuilder)
						{
							goto IL_055C;
						}
						ControlFlowGraphBuilder.RegionBuilder regionBuilder = CS$<>8__locals1.regionMap[basicBlockBuilder];
						if (regionBuilder.FirstBlock != regionBuilder.LastBlock)
						{
							if (ptr.Kind == ControlFlowBranchSemantics.StructuredExceptionHandling)
							{
								if (basicBlockBuilder.HasPredecessors)
								{
									ControlFlowGraphBuilder.BasicBlockBuilder singletonPredecessorOrDefault2 = basicBlockBuilder.GetSingletonPredecessorOrDefault();
									if (singletonPredecessorOrDefault2 == null || singletonPredecessorOrDefault2.Ordinal != i - 1 || singletonPredecessorOrDefault2.FallThrough.Destination != basicBlockBuilder || singletonPredecessorOrDefault2.Conditional.Destination == basicBlockBuilder || CS$<>8__locals1.regionMap[singletonPredecessorOrDefault2] != regionBuilder)
									{
										goto IL_055C;
									}
									singletonPredecessorOrDefault2.FallThrough = basicBlockBuilder.FallThrough;
								}
							}
							else
							{
								IOperation branchValue = basicBlockBuilder.BranchValue;
								if (ControlFlowGraphBuilder.<PackBlocks>g__tryGetImplicitEntryRegion|37_0(basicBlockBuilder, regionBuilder) != null && (branchValue != null || ptr.Destination != CS$<>8__locals1.blocks[i + 1]))
								{
									goto IL_055C;
								}
								if (branchValue != null)
								{
									if (!basicBlockBuilder.HasPredecessors && ptr.Kind == ControlFlowBranchSemantics.Return)
									{
										if (ptr.Destination.Kind != BasicBlockKind.Exit || !branchValue.IsImplicit || branchValue.Kind != OperationKind.LocalReference)
										{
											goto IL_055C;
										}
										if (!((ILocalReferenceOperation)branchValue).Local.IsFunctionValue)
										{
											goto IL_055C;
										}
									}
									else
									{
										ControlFlowGraphBuilder.BasicBlockBuilder singletonPredecessorOrDefault3 = basicBlockBuilder.GetSingletonPredecessorOrDefault();
										if (singletonPredecessorOrDefault3 == null || singletonPredecessorOrDefault3.BranchValue != null || singletonPredecessorOrDefault3.Kind == BasicBlockKind.Entry || CS$<>8__locals1.regionMap[singletonPredecessorOrDefault3] != regionBuilder)
										{
											goto IL_055C;
										}
									}
								}
								ControlFlowGraphBuilder.RegionBuilder regionBuilder2 = ((ptr.Destination == null) ? null : CS$<>8__locals1.regionMap[ptr.Destination]);
								if (basicBlockBuilder.HasPredecessors)
								{
									if (arrayBuilder == null)
									{
										arrayBuilder = ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder>.GetInstance();
									}
									else
									{
										arrayBuilder.Clear();
									}
									basicBlockBuilder.GetPredecessors(arrayBuilder);
									if (regionBuilder != regionBuilder2)
									{
										ArrayBuilder<ControlFlowGraphBuilder.RegionBuilder> fromCurrent = CS$<>8__locals1.fromCurrent;
										if (fromCurrent != null)
										{
											fromCurrent.Clear();
										}
										ArrayBuilder<ControlFlowGraphBuilder.RegionBuilder> fromDestination = CS$<>8__locals1.fromDestination;
										if (fromDestination != null)
										{
											fromDestination.Clear();
										}
										if (!ControlFlowGraphBuilder.<PackBlocks>g__checkBranchesFromPredecessors|37_4(arrayBuilder, regionBuilder, regionBuilder2, ref CS$<>8__locals1))
										{
											goto IL_055C;
										}
									}
									foreach (ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 in arrayBuilder)
									{
										if (ControlFlowGraphBuilder.<PackBlocks>g__tryMergeBranch|37_2(basicBlockBuilder2, ref basicBlockBuilder2.FallThrough, basicBlockBuilder) && branchValue != null)
										{
											basicBlockBuilder2.BranchValue = branchValue;
										}
										ControlFlowGraphBuilder.<PackBlocks>g__tryMergeBranch|37_2(basicBlockBuilder2, ref basicBlockBuilder2.Conditional, basicBlockBuilder);
									}
								}
								ControlFlowGraphBuilder.BasicBlockBuilder destination = ptr.Destination;
								if (destination != null)
								{
									destination.RemovePredecessor(basicBlockBuilder);
								}
							}
							i--;
							num--;
							ControlFlowGraphBuilder.<PackBlocks>g__removeBlock|37_1(basicBlockBuilder, regionBuilder, ref CS$<>8__locals1);
							flag = true;
							flag2 = true;
							goto IL_055C;
						}
						if (regionBuilder.Kind == ControlFlowRegionKind.Finally && ptr.Destination == null && ptr.Kind == ControlFlowBranchSemantics.StructuredExceptionHandling && !basicBlockBuilder.HasPredecessors)
						{
							ControlFlowGraphBuilder.RegionBuilder enclosing = regionBuilder.Enclosing;
							ControlFlowGraphBuilder.RegionBuilder regionBuilder3 = enclosing.Regions.First();
							if (regionBuilder3.Locals.IsEmpty && !regionBuilder3.HasLocalFunctions && !regionBuilder3.HasCaptureIds)
							{
								i = regionBuilder3.FirstBlock.Ordinal - 1;
								ControlFlowGraphBuilder.MergeSubRegionAndFree(regionBuilder3, CS$<>8__locals1.blocks, CS$<>8__locals1.regionMap, false);
							}
							else
							{
								regionBuilder3.Kind = ControlFlowRegionKind.LocalLifetime;
								i--;
							}
							ControlFlowGraphBuilder.MergeSubRegionAndFree(regionBuilder, CS$<>8__locals1.blocks, CS$<>8__locals1.regionMap, false);
							ControlFlowGraphBuilder.RegionBuilder enclosing2 = enclosing.Enclosing;
							ControlFlowGraphBuilder.MergeSubRegionAndFree(enclosing, CS$<>8__locals1.blocks, CS$<>8__locals1.regionMap, false);
							num--;
							ControlFlowGraphBuilder.<PackBlocks>g__removeBlock|37_1(basicBlockBuilder, enclosing2, ref CS$<>8__locals1);
							flag = true;
							flag2 = true;
							goto IL_055C;
						}
						goto IL_055C;
					}
					else
					{
						if (ptr.Kind == ControlFlowBranchSemantics.StructuredExceptionHandling)
						{
							goto IL_055C;
						}
						ControlFlowGraphBuilder.BasicBlockBuilder singletonPredecessorOrDefault4 = basicBlockBuilder.GetSingletonPredecessorOrDefault();
						if (singletonPredecessorOrDefault4 == null)
						{
							goto IL_055C;
						}
						ControlFlowGraphBuilder.RegionBuilder regionBuilder4 = CS$<>8__locals1.regionMap[basicBlockBuilder];
						if (ControlFlowGraphBuilder.<PackBlocks>g__tryGetImplicitEntryRegion|37_0(basicBlockBuilder, regionBuilder4) == null && singletonPredecessorOrDefault4.Kind != BasicBlockKind.Entry && singletonPredecessorOrDefault4.FallThrough.Destination == basicBlockBuilder && !singletonPredecessorOrDefault4.HasCondition && CS$<>8__locals1.regionMap[singletonPredecessorOrDefault4] == regionBuilder4)
						{
							ControlFlowGraphBuilder.<PackBlocks>g__mergeBranch|37_3(singletonPredecessorOrDefault4, ref singletonPredecessorOrDefault4.FallThrough, ref ptr);
							ControlFlowGraphBuilder.BasicBlockBuilder destination2 = ptr.Destination;
							if (destination2 != null)
							{
								destination2.RemovePredecessor(basicBlockBuilder);
							}
							singletonPredecessorOrDefault4.BranchValue = basicBlockBuilder.BranchValue;
							singletonPredecessorOrDefault4.ConditionKind = basicBlockBuilder.ConditionKind;
							singletonPredecessorOrDefault4.Conditional = basicBlockBuilder.Conditional;
							ControlFlowGraphBuilder.BasicBlockBuilder destination3 = basicBlockBuilder.Conditional.Destination;
							if (destination3 != null)
							{
								destination3.AddPredecessor(singletonPredecessorOrDefault4);
								destination3.RemovePredecessor(basicBlockBuilder);
							}
							i--;
							num--;
							ControlFlowGraphBuilder.<PackBlocks>g__removeBlock|37_1(basicBlockBuilder, regionBuilder4, ref CS$<>8__locals1);
							flag = true;
							flag2 = true;
							goto IL_055C;
						}
						goto IL_055C;
					}
				}
				CS$<>8__locals1.blocks[0].Ordinal = 0;
				CS$<>8__locals1.blocks[num].Ordinal = num;
			}
			while (flag2);
			ArrayBuilder<ControlFlowGraphBuilder.RegionBuilder> fromCurrent2 = CS$<>8__locals1.fromCurrent;
			if (fromCurrent2 != null)
			{
				fromCurrent2.Free();
			}
			ArrayBuilder<ControlFlowGraphBuilder.RegionBuilder> fromDestination2 = CS$<>8__locals1.fromDestination;
			if (fromDestination2 != null)
			{
				fromDestination2.Free();
			}
			ArrayBuilder<ControlFlowGraphBuilder.RegionBuilder> fromPredecessor = CS$<>8__locals1.fromPredecessor;
			if (fromPredecessor != null)
			{
				fromPredecessor.Free();
			}
			if (arrayBuilder != null)
			{
				arrayBuilder.Free();
			}
			return flag;
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x0005838C File Offset: 0x0005658C
		private static void CheckUnresolvedBranches(ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder> blocks, [Nullable(new byte[] { 2, 1, 1 })] PooledDictionary<ILabelSymbol, ControlFlowGraphBuilder.BasicBlockBuilder> labeledBlocks)
		{
			if (labeledBlocks == null)
			{
				return;
			}
			ControlFlowGraphBuilder.<>c__DisplayClass38_0 CS$<>8__locals1;
			CS$<>8__locals1.unresolved = null;
			foreach (ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder in labeledBlocks.Values)
			{
				if (basicBlockBuilder.Ordinal == -1)
				{
					if (CS$<>8__locals1.unresolved == null)
					{
						CS$<>8__locals1.unresolved = PooledHashSet<ControlFlowGraphBuilder.BasicBlockBuilder>.GetInstance();
					}
					CS$<>8__locals1.unresolved.Add(basicBlockBuilder);
				}
			}
			if (CS$<>8__locals1.unresolved == null)
			{
				return;
			}
			foreach (ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 in blocks)
			{
				ControlFlowGraphBuilder.<CheckUnresolvedBranches>g__fixupBranch|38_0(ref basicBlockBuilder2.Conditional, ref CS$<>8__locals1);
				ControlFlowGraphBuilder.<CheckUnresolvedBranches>g__fixupBranch|38_0(ref basicBlockBuilder2.FallThrough, ref CS$<>8__locals1);
			}
			CS$<>8__locals1.unresolved.Free();
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x00058454 File Offset: 0x00056654
		[NullableContext(2)]
		private void VisitStatement(IOperation operation)
		{
			if (operation == null)
			{
				return;
			}
			IOperation currentStatement = this._currentStatement;
			this._currentStatement = operation;
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.AddStatement(base.Visit(operation, default(int?)));
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
			this._currentStatement = currentStatement;
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001E6E RID: 7790 RVA: 0x0005849E File Offset: 0x0005669E
		private ControlFlowGraphBuilder.BasicBlockBuilder CurrentBasicBlock
		{
			get
			{
				if (this._currentBasicBlock == null)
				{
					this.AppendNewBlock(new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block), true);
				}
				return this._currentBasicBlock;
			}
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x000584BB File Offset: 0x000566BB
		[NullableContext(2)]
		private void AddStatement(IOperation statement)
		{
			if (statement == null)
			{
				return;
			}
			Operation.SetParentOperation<IOperation>(statement, null);
			this.CurrentBasicBlock.AddStatement(statement);
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x000584D8 File Offset: 0x000566D8
		[MemberNotNull("_currentBasicBlock")]
		private void AppendNewBlock(ControlFlowGraphBuilder.BasicBlockBuilder block, bool linkToPrevious = true)
		{
			if (linkToPrevious)
			{
				ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = this._blocks.Last();
				if (basicBlockBuilder.FallThrough.Destination == null)
				{
					ControlFlowGraphBuilder.LinkBlocks(basicBlockBuilder, block, ControlFlowBranchSemantics.Regular);
				}
			}
			if (block.Ordinal != -1)
			{
				throw ExceptionUtilities.Unreachable;
			}
			block.Ordinal = this._blocks.Count;
			this._blocks.Add(block);
			this._currentBasicBlock = block;
			this._currentRegion.ExtendToInclude(block);
			this._regionMap.Add(block, this._currentRegion);
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x0005855A File Offset: 0x0005675A
		private void EnterRegion(ControlFlowGraphBuilder.RegionBuilder region, bool spillingStack = false)
		{
			if (!spillingStack)
			{
				this.SpillEvalStack();
			}
			ControlFlowGraphBuilder.RegionBuilder currentRegion = this._currentRegion;
			if (currentRegion != null)
			{
				currentRegion.Add(region);
			}
			this._currentRegion = region;
			this._currentBasicBlock = null;
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x00058588 File Offset: 0x00056788
		private void LeaveRegion()
		{
			if (this._currentRegion.IsEmpty)
			{
				this.AppendNewBlock(new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block), true);
			}
			ControlFlowGraphBuilder.RegionBuilder currentRegion = this._currentRegion;
			this._currentRegion = this._currentRegion.Enclosing;
			ControlFlowGraphBuilder.RegionBuilder currentRegion2 = this._currentRegion;
			if (currentRegion2 != null)
			{
				currentRegion2.ExtendToInclude(currentRegion.LastBlock);
			}
			this._currentBasicBlock = null;
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x000585E5 File Offset: 0x000567E5
		private static void LinkBlocks(ControlFlowGraphBuilder.BasicBlockBuilder prevBlock, ControlFlowGraphBuilder.BasicBlockBuilder nextBlock, ControlFlowBranchSemantics branchKind = ControlFlowBranchSemantics.Regular)
		{
			prevBlock.FallThrough.Destination = nextBlock;
			prevBlock.FallThrough.Kind = branchKind;
			nextBlock.AddPredecessor(prevBlock);
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x00058606 File Offset: 0x00056806
		private void UnconditionalBranch(ControlFlowGraphBuilder.BasicBlockBuilder nextBlock)
		{
			ControlFlowGraphBuilder.LinkBlocks(this.CurrentBasicBlock, nextBlock, ControlFlowBranchSemantics.Regular);
			this._currentBasicBlock = null;
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x0005861C File Offset: 0x0005681C
		[return: Nullable(2)]
		public override IOperation VisitBlock(IBlockOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, operation.Locals, false), false);
			this.VisitStatements(operation.Operations);
			this.LeaveRegion();
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x00058654 File Offset: 0x00056854
		private void StartVisitingStatement(IOperation operation)
		{
			this.SpillEvalStack();
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x0005865C File Offset: 0x0005685C
		[NullableContext(2)]
		[return: NotNullIfNotNull("result")]
		private IOperation FinishVisitingStatement([Nullable(1)] IOperation originalOperation, IOperation result = null)
		{
			if (this._currentStatement == originalOperation)
			{
				return result;
			}
			return result ?? ControlFlowGraphBuilder.MakeInvalidOperation(originalOperation.Syntax, originalOperation.Type, ImmutableArray<IOperation>.Empty);
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x00058684 File Offset: 0x00056884
		private void VisitStatements([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> statements)
		{
			int num = 0;
			while (num < statements.Length && !this.VisitStatementsOneOrAll(statements[num], statements, num))
			{
				num++;
			}
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x000586B8 File Offset: 0x000568B8
		[NullableContext(2)]
		private bool VisitStatementsOneOrAll(IOperation operation, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> statements, int startIndex)
		{
			ControlFlowGraphBuilder.<>c__DisplayClass52_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.statements = statements;
			CS$<>8__locals1.startIndex = startIndex;
			IUsingDeclarationOperation usingDeclarationOperation = operation as IUsingDeclarationOperation;
			if (usingDeclarationOperation == null)
			{
				ILabeledOperation labeledOperation = operation as ILabeledOperation;
				if (labeledOperation != null)
				{
					if (labeledOperation.Operation != null)
					{
						return this.<VisitStatementsOneOrAll>g__visitPossibleUsingDeclarationInLabel|52_0(labeledOperation, ref CS$<>8__locals1);
					}
				}
				this.VisitStatement(operation);
				return false;
			}
			IUsingDeclarationOperation usingDeclarationOperation2 = usingDeclarationOperation;
			ReadOnlySpan<IOperation> readOnlySpan = CS$<>8__locals1.statements.AsSpan();
			int length = readOnlySpan.Length;
			int num = CS$<>8__locals1.startIndex + 1;
			int num2 = length - num;
			this.VisitUsingVariableDeclarationOperation(usingDeclarationOperation2, readOnlySpan.Slice(num, num2));
			return true;
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x00058744 File Offset: 0x00056944
		[return: Nullable(2)]
		internal override IOperation VisitWithStatement(IWithStatementOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			ControlFlowGraphBuilder.ImplicitInstanceInfo currentImplicitInstance = this._currentImplicitInstance;
			this._currentImplicitInstance = new ControlFlowGraphBuilder.ImplicitInstanceInfo(this.VisitAndCapture(operation.Value));
			this.VisitStatement(operation.Body);
			this._currentImplicitInstance = currentImplicitInstance;
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x00058794 File Offset: 0x00056994
		[return: Nullable(2)]
		public override IOperation VisitConstructorBodyOperation(IConstructorBodyOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, operation.Locals, false), false);
			if (operation.Initializer != null)
			{
				this.VisitStatement(operation.Initializer);
			}
			this.VisitMethodBodyBaseOperation(operation);
			this.LeaveRegion();
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x000587E6 File Offset: 0x000569E6
		[return: Nullable(2)]
		public override IOperation VisitMethodBodyOperation(IMethodBodyOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			this.VisitMethodBodyBaseOperation(operation);
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x000587FE File Offset: 0x000569FE
		private void VisitMethodBodyBaseOperation(IMethodBodyBaseOperation operation)
		{
			this.VisitMethodBodies(operation.BlockBody, operation.ExpressionBody);
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x00058814 File Offset: 0x00056A14
		[NullableContext(2)]
		private void VisitMethodBodies(IBlockOperation blockBody, IBlockOperation expressionBody)
		{
			if (blockBody != null)
			{
				this.VisitStatement(blockBody);
				if (expressionBody != null)
				{
					this.UnconditionalBranch(this._exit);
					this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.ErroneousBody, null, default(ImmutableArray<ILocalSymbol>), false), false);
					this.VisitStatement(expressionBody);
					this.LeaveRegion();
					return;
				}
			}
			else if (expressionBody != null)
			{
				this.VisitStatement(expressionBody);
			}
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x0005886C File Offset: 0x00056A6C
		[return: Nullable(2)]
		public override IOperation VisitConditional(IConditionalOperation operation, int? captureIdForResult)
		{
			if (operation == this._currentStatement)
			{
				if (operation.WhenFalse == null)
				{
					ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = null;
					this.VisitConditionalBranch(operation.Condition, ref basicBlockBuilder, false);
					this.VisitStatement(operation.WhenTrue);
					this.AppendNewBlock(basicBlockBuilder, true);
				}
				else
				{
					ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 = null;
					this.VisitConditionalBranch(operation.Condition, ref basicBlockBuilder2, false);
					this.VisitStatement(operation.WhenTrue);
					ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder3 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
					this.UnconditionalBranch(basicBlockBuilder3);
					this.AppendNewBlock(basicBlockBuilder2, true);
					this.VisitStatement(operation.WhenFalse);
					this.AppendNewBlock(basicBlockBuilder3, true);
				}
				return null;
			}
			this.SpillEvalStack();
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder4 = null;
			this.VisitConditionalBranch(operation.Condition, ref basicBlockBuilder4, false);
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder5 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			IConversionOperation conversionOperation = operation.WhenTrue as IConversionOperation;
			IOperation operation2;
			if (conversionOperation != null && conversionOperation.Operand.Kind == OperationKind.Throw)
			{
				base.Visit(conversionOperation.Operand, default(int?));
				this.UnconditionalBranch(basicBlockBuilder5);
				this.AppendNewBlock(basicBlockBuilder4, true);
				operation2 = this.VisitRequired(operation.WhenFalse, default(int?));
			}
			else
			{
				IConversionOperation conversionOperation2 = operation.WhenFalse as IConversionOperation;
				if (conversionOperation2 != null && conversionOperation2.Operand.Kind == OperationKind.Throw)
				{
					operation2 = this.VisitRequired(operation.WhenTrue, default(int?));
					this.UnconditionalBranch(basicBlockBuilder5);
					this.AppendNewBlock(basicBlockBuilder4, true);
					this.BaseVisitRequired(conversionOperation2.Operand, default(int?));
				}
				else
				{
					ControlFlowGraphBuilder.RegionBuilder regionBuilder = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, default(ImmutableArray<ILocalSymbol>), true);
					this.EnterRegion(regionBuilder, false);
					int num = captureIdForResult ?? this.GetNextCaptureId(regionBuilder);
					this.VisitAndCapture(operation.WhenTrue, num);
					this.UnconditionalBranch(basicBlockBuilder5);
					this.AppendNewBlock(basicBlockBuilder4, true);
					this.VisitAndCapture(operation.WhenFalse, num);
					operation2 = ControlFlowGraphBuilder.GetCaptureReference(num, operation);
				}
			}
			this.AppendNewBlock(basicBlockBuilder5, true);
			return operation2;
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x00058A60 File Offset: 0x00056C60
		private void VisitAndCapture(IOperation operation, int captureId)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			IOperation operation2 = this.BaseVisitRequired(operation, new int?(captureId));
			this.PopStackFrame(evalStackFrame, true);
			this.CaptureResultIfNotAlready(operation.Syntax, captureId, operation2);
			this.LeaveRegionIfAny(evalStackFrame);
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x00058AA0 File Offset: 0x00056CA0
		private IOperation VisitAndCapture(IOperation operation)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.PushOperand(this.BaseVisitRequired(operation, default(int?)));
			this.SpillEvalStack();
			return this.PopStackFrame<IOperation>(evalStackFrame, this.PopOperand());
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x00058AE0 File Offset: 0x00056CE0
		private void CaptureResultIfNotAlready(SyntaxNode syntax, int captureId, IOperation result)
		{
			if (result.Kind != OperationKind.FlowCaptureReference || captureId != ((IFlowCaptureReferenceOperation)result).Id.Value)
			{
				this.SpillEvalStack();
				this.AddStatement(new FlowCaptureOperation(captureId, syntax, result));
			}
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x00058B24 File Offset: 0x00056D24
		private ControlFlowGraphBuilder.EvalStackFrame PushStackFrame()
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = new ControlFlowGraphBuilder.EvalStackFrame();
			this._evalStack.Push(new ValueTuple<ControlFlowGraphBuilder.EvalStackFrame, IOperation>(evalStackFrame, null));
			return evalStackFrame;
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x00058B4C File Offset: 0x00056D4C
		private void PopStackFrame(ControlFlowGraphBuilder.EvalStackFrame frame, bool mergeNestedRegions = true)
		{
			int count = this._evalStack.Count;
			if (this._startSpillingAt == count)
			{
				this._startSpillingAt--;
			}
			this._evalStack.Pop<ValueTuple<ControlFlowGraphBuilder.EvalStackFrame, IOperation>>();
			if (frame.RegionBuilderOpt != null && mergeNestedRegions)
			{
				while (this._currentRegion != frame.RegionBuilderOpt)
				{
					ControlFlowGraphBuilder.RegionBuilder currentRegion = this._currentRegion;
					this._currentRegion = currentRegion.Enclosing;
					this._currentRegion.AddCaptureIds(currentRegion.CaptureIds);
					if (!currentRegion.IsEmpty)
					{
						this._currentRegion.ExtendToInclude(currentRegion.LastBlock);
					}
					ControlFlowGraphBuilder.MergeSubRegionAndFree(currentRegion, this._blocks, this._regionMap, true);
				}
			}
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x00058BF5 File Offset: 0x00056DF5
		private void PopStackFrameAndLeaveRegion(ControlFlowGraphBuilder.EvalStackFrame frame)
		{
			this.PopStackFrame(frame, true);
			this.LeaveRegionIfAny(frame);
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x00058C08 File Offset: 0x00056E08
		private void LeaveRegionIfAny(ControlFlowGraphBuilder.EvalStackFrame frame)
		{
			ControlFlowGraphBuilder.RegionBuilder regionBuilderOpt = frame.RegionBuilderOpt;
			if (regionBuilderOpt != null)
			{
				while (this._currentRegion != regionBuilderOpt)
				{
					this.LeaveRegion();
				}
				this.LeaveRegion();
			}
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x00058C36 File Offset: 0x00056E36
		private T PopStackFrame<[Nullable(2)] T>(ControlFlowGraphBuilder.EvalStackFrame frame, T value)
		{
			this.PopStackFrame(frame, true);
			return value;
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x00058C41 File Offset: 0x00056E41
		private void LeaveRegionsUpTo(ControlFlowGraphBuilder.RegionBuilder resultCaptureRegion)
		{
			while (this._currentRegion != resultCaptureRegion)
			{
				this.LeaveRegion();
			}
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x00058C54 File Offset: 0x00056E54
		private int GetNextCaptureId(ControlFlowGraphBuilder.RegionBuilder owner)
		{
			int nextId = this._captureIdDispenser.GetNextId();
			owner.AddCaptureId(nextId);
			return nextId;
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x00058C78 File Offset: 0x00056E78
		private void SpillEvalStack()
		{
			int num = -1;
			for (int i = this._startSpillingAt - 1; i >= 0; i--)
			{
				if (this._evalStack[i].Item1 != null)
				{
					num = i;
					break;
				}
			}
			for (int j = this._startSpillingAt; j < this._evalStack.Count; j++)
			{
				ValueTuple<ControlFlowGraphBuilder.EvalStackFrame, IOperation> valueTuple = this._evalStack[j];
				ControlFlowGraphBuilder.EvalStackFrame item = valueTuple.Item1;
				IOperation item2 = valueTuple.Item2;
				if (item != null)
				{
					num = j;
					item.RegionBuilderOpt = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, default(ImmutableArray<ILocalSymbol>), true);
					this.EnterRegion(item.RegionBuilderOpt, true);
				}
				else if (item2.Kind != OperationKind.FlowCaptureReference && item2.Kind != OperationKind.DeclarationExpression && item2.Kind != OperationKind.Discard && item2.Kind != OperationKind.OmittedArgument)
				{
					ControlFlowGraphBuilder.RegionBuilder regionBuilder = this._evalStack[num].Item1.RegionBuilderOpt;
					if (this._currentRegion != regionBuilder)
					{
						PooledHashSet<CaptureId> instance = PooledHashSet<CaptureId>.GetInstance();
						for (int k = num + 1; k < this._evalStack.Count; k++)
						{
							IOperation item3 = this._evalStack[k].Item2;
							if (item3 != null)
							{
								if (k < j)
								{
									IFlowCaptureReferenceOperation flowCaptureReferenceOperation = item3 as IFlowCaptureReferenceOperation;
									if (flowCaptureReferenceOperation != null)
									{
										instance.Add(flowCaptureReferenceOperation.Id);
									}
								}
								else if (k > j)
								{
									foreach (IFlowCaptureReferenceOperation flowCaptureReferenceOperation2 in Enumerable.OfType<IFlowCaptureReferenceOperation>(item3.DescendantsAndSelf()))
									{
										instance.Add(flowCaptureReferenceOperation2.Id);
									}
								}
							}
						}
						ControlFlowGraphBuilder.RegionBuilder regionBuilder2 = this.CurrentRegionRequired;
						do
						{
							if (regionBuilder2.HasCaptureIds)
							{
								if (regionBuilder2.CaptureIds.Any((CaptureId id, PooledHashSet<CaptureId> set) => set.Contains(id), instance))
								{
									goto Block_16;
								}
							}
							regionBuilder2 = regionBuilder2.Enclosing;
						}
						while (regionBuilder2 != regionBuilder);
						IL_01E0:
						instance.Free();
						goto IL_01E7;
						Block_16:
						regionBuilder = regionBuilder2;
						goto IL_01E0;
					}
					IL_01E7:
					int nextCaptureId = this.GetNextCaptureId(regionBuilder);
					this.AddStatement(new FlowCaptureOperation(nextCaptureId, item2.Syntax, item2));
					this._evalStack[j] = new ValueTuple<ControlFlowGraphBuilder.EvalStackFrame, IOperation>(null, ControlFlowGraphBuilder.GetCaptureReference(nextCaptureId, item2));
					while (this._currentRegion != regionBuilder)
					{
						this.LeaveRegion();
					}
				}
			}
			this._startSpillingAt = this._evalStack.Count;
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x00058EF0 File Offset: 0x000570F0
		private void PushOperand(IOperation operation)
		{
			this._evalStack.Push(new ValueTuple<ControlFlowGraphBuilder.EvalStackFrame, IOperation>(null, operation));
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x00058F04 File Offset: 0x00057104
		private IOperation PopOperand()
		{
			int count = this._evalStack.Count;
			if (this._startSpillingAt == count)
			{
				this._startSpillingAt--;
			}
			return this._evalStack.Pop<ValueTuple<ControlFlowGraphBuilder.EvalStackFrame, IOperation>>().Item2;
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x00058F44 File Offset: 0x00057144
		private IOperation PeekOperand()
		{
			return this._evalStack.Peek<ValueTuple<ControlFlowGraphBuilder.EvalStackFrame, IOperation>>().Item2;
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x00058F58 File Offset: 0x00057158
		[NullableContext(0)]
		private void VisitAndPushArray<T>([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> array, [Nullable(new byte[] { 2, 1, 1 })] Func<T, IOperation> unwrapper = null) where T : IOperation
		{
			foreach (T t in array)
			{
				IOperation operation;
				if (unwrapper != null)
				{
					operation = unwrapper.Invoke(t);
				}
				else
				{
					IOperation operation2 = t;
					operation = operation2;
				}
				this.PushOperand(this.VisitRequired(operation, default(int?)));
			}
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x00058FAC File Offset: 0x000571AC
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<T> PopArray<T>([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> originalArray, [Nullable(new byte[] { 2, 1, 0, 1, 1 })] Func<IOperation, int, ImmutableArray<T>, T> wrapper = null) where T : IOperation
		{
			int length = originalArray.Length;
			if (length == 0)
			{
				return ImmutableArray<T>.Empty;
			}
			ArrayBuilder<T> instance = ArrayBuilder<T>.GetInstance(length);
			for (int i = length - 1; i >= 0; i--)
			{
				IOperation operation = this.PopOperand();
				instance.Add((wrapper != null) ? wrapper.Invoke(operation, i, originalArray) : ((T)((object)operation)));
			}
			instance.ReverseContents();
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x0005900C File Offset: 0x0005720C
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<T> VisitArray<T>([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> originalArray, [Nullable(new byte[] { 2, 1, 1 })] Func<T, IOperation> unwrapper = null, [Nullable(new byte[] { 2, 1, 0, 1, 1 })] Func<IOperation, int, ImmutableArray<T>, T> wrapper = null) where T : IOperation
		{
			this.VisitAndPushArray<T>(originalArray, unwrapper);
			return this.PopArray<T>(originalArray, wrapper);
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x0005901E File Offset: 0x0005721E
		[return: Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<IArgumentOperation> VisitArguments([Nullable(new byte[] { 0, 1 })] ImmutableArray<IArgumentOperation> arguments)
		{
			return this.VisitArray<IArgumentOperation>(arguments, new Func<IArgumentOperation, IOperation>(ControlFlowGraphBuilder.UnwrapArgument), new Func<IOperation, int, ImmutableArray<IArgumentOperation>, IArgumentOperation>(this.RewriteArgumentFromArray));
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x0005903F File Offset: 0x0005723F
		private static IOperation UnwrapArgument(IArgumentOperation argument)
		{
			return argument.Value;
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x00059048 File Offset: 0x00057248
		private IArgumentOperation RewriteArgumentFromArray(IOperation visitedArgument, int index, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IArgumentOperation> args)
		{
			ArgumentOperation argumentOperation = (ArgumentOperation)args[index];
			return new ArgumentOperation(argumentOperation.ArgumentKind, argumentOperation.Parameter, visitedArgument, argumentOperation.InConversionConvertible, argumentOperation.OutConversionConvertible, null, argumentOperation.Syntax, this.IsImplicit(argumentOperation));
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x00059090 File Offset: 0x00057290
		public override IOperation VisitSimpleAssignment(ISimpleAssignmentOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.PushOperand(this.VisitRequired(operation.Target, default(int?)));
			IOperation operation2 = this.VisitRequired(operation.Value, default(int?));
			return this.PopStackFrame<SimpleAssignmentOperation>(evalStackFrame, new SimpleAssignmentOperation(operation.IsRef, this.PopOperand(), operation2, null, operation.Syntax, operation.Type, operation.GetConstantValue(), this.IsImplicit(operation)));
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x00059108 File Offset: 0x00057308
		public override IOperation VisitCompoundAssignment(ICompoundAssignmentOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			CompoundAssignmentOperation compoundAssignmentOperation = (CompoundAssignmentOperation)operation;
			this.PushOperand(this.VisitRequired(compoundAssignmentOperation.Target, default(int?)));
			IOperation operation2 = this.VisitRequired(compoundAssignmentOperation.Value, default(int?));
			return this.PopStackFrame<CompoundAssignmentOperation>(evalStackFrame, new CompoundAssignmentOperation(compoundAssignmentOperation.InConversionConvertible, compoundAssignmentOperation.OutConversionConvertible, operation.OperatorKind, operation.IsLifted, operation.IsChecked, operation.OperatorMethod, this.PopOperand(), operation2, null, operation.Syntax, operation.Type, this.IsImplicit(operation)));
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x000591A0 File Offset: 0x000573A0
		public override IOperation VisitArrayElementReference(IArrayElementReferenceOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.PushOperand(this.VisitRequired(operation.ArrayReference, default(int?)));
			ImmutableArray<IOperation> immutableArray = this.VisitArray<IOperation>(operation.Indices, null, null);
			IOperation operation2 = this.PopOperand();
			this.PopStackFrame(evalStackFrame, true);
			return new ArrayElementReferenceOperation(operation2, immutableArray, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x00059208 File Offset: 0x00057408
		private static bool IsConditional(IBinaryOperation operation)
		{
			BinaryOperatorKind operatorKind = operation.OperatorKind;
			return operatorKind - BinaryOperatorKind.ConditionalAnd <= 1;
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x00059228 File Offset: 0x00057428
		public override IOperation VisitBinaryOperator(IBinaryOperation operation, int? captureIdForResult)
		{
			if (ControlFlowGraphBuilder.IsConditional(operation))
			{
				if (operation.OperatorMethod != null)
				{
					return this.VisitUserDefinedBinaryConditionalOperator(operation, captureIdForResult);
				}
				if (ITypeSymbolHelpers.IsBooleanType(operation.Type) && ITypeSymbolHelpers.IsBooleanType(operation.LeftOperand.Type) && ITypeSymbolHelpers.IsBooleanType(operation.RightOperand.Type))
				{
					return this.VisitBinaryConditionalOperator(operation, true, captureIdForResult, null, null);
				}
				if (operation.IsLifted && ITypeSymbolHelpers.IsNullableOfBoolean(operation.Type) && ITypeSymbolHelpers.IsNullableOfBoolean(operation.LeftOperand.Type) && ITypeSymbolHelpers.IsNullableOfBoolean(operation.RightOperand.Type))
				{
					return this.VisitNullableBinaryConditionalOperator(operation, captureIdForResult);
				}
				if (ITypeSymbolHelpers.IsObjectType(operation.Type) && ITypeSymbolHelpers.IsObjectType(operation.LeftOperand.Type) && ITypeSymbolHelpers.IsObjectType(operation.RightOperand.Type))
				{
					return this.VisitObjectBinaryConditionalOperator(operation, captureIdForResult);
				}
				if (ITypeSymbolHelpers.IsDynamicType(operation.Type) && (ITypeSymbolHelpers.IsDynamicType(operation.LeftOperand.Type) || ITypeSymbolHelpers.IsDynamicType(operation.RightOperand.Type)))
				{
					return this.VisitDynamicBinaryConditionalOperator(operation, captureIdForResult);
				}
			}
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.PushOperand(this.VisitRequired(operation.LeftOperand, default(int?)));
			IOperation operation2 = this.VisitRequired(operation.RightOperand, default(int?));
			return this.PopStackFrame<BinaryOperation>(evalStackFrame, new BinaryOperation(operation.OperatorKind, this.PopOperand(), operation2, operation.IsLifted, operation.IsChecked, operation.IsCompareText, operation.OperatorMethod, ((BinaryOperation)operation).UnaryOperatorMethod, null, operation.Syntax, operation.Type, operation.GetConstantValue(), this.IsImplicit(operation)));
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x000593D8 File Offset: 0x000575D8
		public override IOperation VisitTupleBinaryOperator(ITupleBinaryOperation operation, int? captureIdForResult)
		{
			ValueTuple<IOperation, IOperation> valueTuple = this.VisitPreservingTupleOperations(operation.LeftOperand, operation.RightOperand);
			IOperation item = valueTuple.Item1;
			IOperation item2 = valueTuple.Item2;
			return new TupleBinaryOperation(operation.OperatorKind, item, item2, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x00059428 File Offset: 0x00057628
		public override IOperation VisitUnaryOperator(IUnaryOperation operation, int? captureIdForResult)
		{
			if (ControlFlowGraphBuilder.IsBooleanLogicalNot(operation))
			{
				return this.VisitConditionalExpression(operation, true, captureIdForResult, null, null);
			}
			return new UnaryOperation(operation.OperatorKind, this.VisitRequired(operation.Operand, default(int?)), operation.IsLifted, operation.IsChecked, operation.OperatorMethod, null, operation.Syntax, operation.Type, operation.GetConstantValue(), this.IsImplicit(operation));
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x00059495 File Offset: 0x00057695
		private static bool IsBooleanLogicalNot(IUnaryOperation operation)
		{
			return operation.OperatorKind == UnaryOperatorKind.Not && operation.OperatorMethod == null && ITypeSymbolHelpers.IsBooleanType(operation.Type) && ITypeSymbolHelpers.IsBooleanType(operation.Operand.Type);
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x000594C8 File Offset: 0x000576C8
		private static bool CalculateAndOrSense(IBinaryOperation binOp, bool sense)
		{
			BinaryOperatorKind operatorKind = binOp.OperatorKind;
			if (operatorKind == BinaryOperatorKind.ConditionalAnd)
			{
				return sense;
			}
			if (operatorKind == BinaryOperatorKind.ConditionalOr)
			{
				return !sense;
			}
			throw ExceptionUtilities.UnexpectedValue(binOp.OperatorKind);
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x000594FD File Offset: 0x000576FD
		private IOperation VisitBinaryConditionalOperator(IBinaryOperation binOp, bool sense, int? captureIdForResult, [Nullable(2)] ControlFlowGraphBuilder.BasicBlockBuilder fallToTrueOpt, [Nullable(2)] ControlFlowGraphBuilder.BasicBlockBuilder fallToFalseOpt)
		{
			if (!ControlFlowGraphBuilder.CalculateAndOrSense(binOp, sense))
			{
				return this.VisitShortCircuitingOperator(binOp, sense, sense, true, captureIdForResult, fallToTrueOpt, fallToFalseOpt);
			}
			return this.VisitShortCircuitingOperator(binOp, sense, !sense, false, captureIdForResult, fallToTrueOpt, fallToFalseOpt);
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x0005952C File Offset: 0x0005772C
		private IOperation VisitNullableBinaryConditionalOperator(IBinaryOperation binOp, int? captureIdForResult)
		{
			this.SpillEvalStack();
			IOperation leftOperand = binOp.LeftOperand;
			IOperation rightOperand = binOp.RightOperand;
			bool flag = ControlFlowGraphBuilder.CalculateAndOrSense(binOp, true);
			ControlFlowGraphBuilder.RegionBuilder currentRegionRequired = this.CurrentRegionRequired;
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder3 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			IOperation operation = this.VisitAndCapture(leftOperand);
			IOperation operation2 = operation;
			if (flag)
			{
				operation2 = ControlFlowGraphBuilder.<VisitNullableBinaryConditionalOperator>g__negateNullable|90_0(operation2);
			}
			operation2 = this.CallNullableMember(operation2, SpecialMember.System_Nullable_T_GetValueOrDefault);
			this.ConditionalBranch(operation2, true, basicBlockBuilder3);
			this.UnconditionalBranch(basicBlockBuilder2);
			int num = captureIdForResult ?? this.GetNextCaptureId(currentRegionRequired);
			this.AppendNewBlock(basicBlockBuilder2, true);
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			IOperation operation3 = this.VisitAndCapture(rightOperand);
			operation2 = operation3;
			if (!flag)
			{
				operation2 = ControlFlowGraphBuilder.<VisitNullableBinaryConditionalOperator>g__negateNullable|90_0(operation2);
			}
			operation2 = this.CallNullableMember(operation2, SpecialMember.System_Nullable_T_GetValueOrDefault);
			this.ConditionalBranch(operation2, true, basicBlockBuilder3);
			this._currentBasicBlock = null;
			this.AddStatement(new FlowCaptureOperation(num, binOp.Syntax, OperationCloner.CloneOperation<IOperation>(operation3)));
			this.UnconditionalBranch(basicBlockBuilder);
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
			this.AppendNewBlock(basicBlockBuilder3, true);
			this.AddStatement(new FlowCaptureOperation(num, binOp.Syntax, OperationCloner.CloneOperation<IOperation>(operation)));
			this.LeaveRegionsUpTo(currentRegionRequired);
			this.AppendNewBlock(basicBlockBuilder, true);
			return ControlFlowGraphBuilder.GetCaptureReference(num, binOp);
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x0005966C File Offset: 0x0005786C
		private IOperation VisitObjectBinaryConditionalOperator(IBinaryOperation binOp, int? captureIdForResult)
		{
			this.SpillEvalStack();
			INamedTypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
			IOperation leftOperand = binOp.LeftOperand;
			IOperation rightOperand = binOp.RightOperand;
			bool flag = ControlFlowGraphBuilder.CalculateAndOrSense(binOp, true);
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			IOperation operation = this.CreateConversion(this.VisitRequired(leftOperand, default(int?)), specialType);
			this.ConditionalBranch(operation, flag, basicBlockBuilder2);
			this._currentBasicBlock = null;
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
			ControlFlowGraphBuilder.RegionBuilder currentRegionRequired = this.CurrentRegionRequired;
			int nextCaptureId = this.GetNextCaptureId(currentRegionRequired);
			ConstantValue constantValue = ConstantValue.Create(!flag);
			this.AddStatement(new FlowCaptureOperation(nextCaptureId, binOp.Syntax, new LiteralOperation(null, leftOperand.Syntax, specialType, constantValue, true)));
			this.UnconditionalBranch(basicBlockBuilder);
			this.AppendNewBlock(basicBlockBuilder2, true);
			evalStackFrame = this.PushStackFrame();
			operation = this.CreateConversion(this.VisitRequired(rightOperand, default(int?)), specialType);
			this.AddStatement(new FlowCaptureOperation(nextCaptureId, binOp.Syntax, operation));
			this.PopStackFrame(evalStackFrame, true);
			this.LeaveRegionsUpTo(currentRegionRequired);
			this.AppendNewBlock(basicBlockBuilder, true);
			operation = new FlowCaptureReferenceOperation(nextCaptureId, binOp.Syntax, specialType, null);
			ConstantValue constantValue2;
			return new ConversionOperation(operation, this._compilation.ClassifyConvertibleConversion(operation, binOp.Type, out constantValue2), false, false, null, binOp.Syntax, binOp.Type, binOp.GetConstantValue(), true);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x000597D0 File Offset: 0x000579D0
		private IOperation CreateConversion(IOperation operand, ITypeSymbol type)
		{
			ConstantValue constantValue;
			return new ConversionOperation(operand, this._compilation.ClassifyConvertibleConversion(operand, type, out constantValue), false, false, null, operand.Syntax, type, constantValue, true);
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00059800 File Offset: 0x00057A00
		private IOperation VisitDynamicBinaryConditionalOperator(IBinaryOperation binOp, int? captureIdForResult)
		{
			this.SpillEvalStack();
			ControlFlowGraphBuilder.RegionBuilder currentRegionRequired = this.CurrentRegionRequired;
			INamedTypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
			IOperation leftOperand = binOp.LeftOperand;
			IOperation rightOperand = binOp.RightOperand;
			IMethodSymbol unaryOperatorMethod = ((BinaryOperation)binOp).UnaryOperatorMethod;
			bool flag = ControlFlowGraphBuilder.CalculateAndOrSense(binOp, true);
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			IOperation operation = this.VisitAndCapture(leftOperand);
			IOperation operation2 = operation;
			bool flag2;
			if (ITypeSymbolHelpers.IsBooleanType(leftOperand.Type))
			{
				flag2 = flag;
			}
			else if (ITypeSymbolHelpers.IsDynamicType(leftOperand.Type) || unaryOperatorMethod != null)
			{
				flag2 = false;
				if (unaryOperatorMethod == null || (ITypeSymbolHelpers.IsBooleanType(unaryOperatorMethod.ReturnType) && (ITypeSymbolHelpers.IsNullableType(leftOperand.Type) || !ITypeSymbolHelpers.IsNullableType(unaryOperatorMethod.Parameters[0].Type))))
				{
					operation2 = new UnaryOperation(flag ? UnaryOperatorKind.False : UnaryOperatorKind.True, operation2, false, false, unaryOperatorMethod, null, operation2.Syntax, specialType, null, true);
				}
				else
				{
					operation2 = ControlFlowGraphBuilder.MakeInvalidOperation(specialType, operation2);
				}
			}
			else
			{
				operation2 = this.CreateConversion(operation2, specialType);
				flag2 = flag;
			}
			this.ConditionalBranch(operation2, flag2, basicBlockBuilder2);
			this._currentBasicBlock = null;
			int num = captureIdForResult ?? this.GetNextCaptureId(currentRegionRequired);
			IOperation operation3 = OperationCloner.CloneOperation<IOperation>(operation);
			if (!ITypeSymbolHelpers.IsDynamicType(leftOperand.Type))
			{
				operation3 = this.CreateConversion(operation3, binOp.Type);
			}
			this.AddStatement(new FlowCaptureOperation(num, binOp.Syntax, operation3));
			this.UnconditionalBranch(basicBlockBuilder);
			this.AppendNewBlock(basicBlockBuilder2, true);
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.PushOperand(OperationCloner.CloneOperation<IOperation>(operation));
			IOperation operation4 = this.VisitRequired(rightOperand, default(int?));
			this.AddStatement(new FlowCaptureOperation(num, binOp.Syntax, new BinaryOperation(flag ? BinaryOperatorKind.And : BinaryOperatorKind.Or, this.PopOperand(), operation4, false, binOp.IsChecked, binOp.IsCompareText, binOp.OperatorMethod, null, null, binOp.Syntax, binOp.Type, binOp.GetConstantValue(), this.IsImplicit(binOp))));
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
			this.LeaveRegionsUpTo(currentRegionRequired);
			this.AppendNewBlock(basicBlockBuilder, true);
			return ControlFlowGraphBuilder.GetCaptureReference(num, binOp);
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x00059A28 File Offset: 0x00057C28
		private IOperation VisitUserDefinedBinaryConditionalOperator(IBinaryOperation binOp, int? captureIdForResult)
		{
			this.SpillEvalStack();
			ControlFlowGraphBuilder.RegionBuilder currentRegionRequired = this.CurrentRegionRequired;
			INamedTypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
			bool isLifted = binOp.IsLifted;
			IOperation leftOperand = binOp.LeftOperand;
			IOperation rightOperand = binOp.RightOperand;
			IMethodSymbol unaryOperatorMethod = ((BinaryOperation)binOp).UnaryOperatorMethod;
			bool flag = ControlFlowGraphBuilder.CalculateAndOrSense(binOp, true);
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			IOperation operation = this.VisitAndCapture(leftOperand);
			IOperation operation2 = operation;
			if (ITypeSymbolHelpers.IsNullableType(leftOperand.Type))
			{
				if ((unaryOperatorMethod == null) ? isLifted : (!ITypeSymbolHelpers.IsNullableType(unaryOperatorMethod.Parameters[0].Type)))
				{
					operation2 = ControlFlowGraphBuilder.MakeIsNullOperation(operation2, specialType);
					this.ConditionalBranch(operation2, true, basicBlockBuilder2);
					this._currentBasicBlock = null;
					operation2 = this.CallNullableMember(OperationCloner.CloneOperation<IOperation>(operation), SpecialMember.System_Nullable_T_GetValueOrDefault);
				}
			}
			else if (unaryOperatorMethod != null && ITypeSymbolHelpers.IsNullableType(unaryOperatorMethod.Parameters[0].Type))
			{
				operation2 = ControlFlowGraphBuilder.MakeInvalidOperation(unaryOperatorMethod.Parameters[0].Type, operation2);
			}
			if (unaryOperatorMethod != null && ITypeSymbolHelpers.IsBooleanType(unaryOperatorMethod.ReturnType))
			{
				operation2 = new UnaryOperation(flag ? UnaryOperatorKind.False : UnaryOperatorKind.True, operation2, false, false, unaryOperatorMethod, null, operation2.Syntax, unaryOperatorMethod.ReturnType, null, true);
			}
			else
			{
				operation2 = ControlFlowGraphBuilder.MakeInvalidOperation(specialType, operation2);
			}
			this.ConditionalBranch(operation2, false, basicBlockBuilder2);
			this._currentBasicBlock = null;
			int num = captureIdForResult ?? this.GetNextCaptureId(currentRegionRequired);
			this.AddStatement(new FlowCaptureOperation(num, binOp.Syntax, OperationCloner.CloneOperation<IOperation>(operation)));
			this.UnconditionalBranch(basicBlockBuilder);
			this.AppendNewBlock(basicBlockBuilder2, true);
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.PushOperand(OperationCloner.CloneOperation<IOperation>(operation));
			IOperation operation3 = this.VisitRequired(rightOperand, default(int?));
			this.AddStatement(new FlowCaptureOperation(num, binOp.Syntax, new BinaryOperation(flag ? BinaryOperatorKind.And : BinaryOperatorKind.Or, this.PopOperand(), operation3, isLifted, binOp.IsChecked, binOp.IsCompareText, binOp.OperatorMethod, null, null, binOp.Syntax, binOp.Type, binOp.GetConstantValue(), this.IsImplicit(binOp))));
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
			this.LeaveRegionsUpTo(currentRegionRequired);
			this.AppendNewBlock(basicBlockBuilder, true);
			return ControlFlowGraphBuilder.GetCaptureReference(num, binOp);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x00059C7C File Offset: 0x00057E7C
		private IOperation VisitShortCircuitingOperator(IBinaryOperation condition, bool sense, bool stopSense, bool stopValue, int? captureIdForResult, [Nullable(2)] ControlFlowGraphBuilder.BasicBlockBuilder fallToTrueOpt, [Nullable(2)] ControlFlowGraphBuilder.BasicBlockBuilder fallToFalseOpt)
		{
			this.SpillEvalStack();
			ref ControlFlowGraphBuilder.BasicBlockBuilder ptr = (ref stopValue ? ref fallToTrueOpt : ref fallToFalseOpt);
			bool flag = ptr == null;
			this.VisitConditionalBranch(condition.LeftOperand, ref ptr, stopSense);
			ControlFlowGraphBuilder.RegionBuilder currentRegionRequired = this.CurrentRegionRequired;
			int num = captureIdForResult ?? this.GetNextCaptureId(currentRegionRequired);
			IOperation operation = this.VisitConditionalExpression(condition.RightOperand, sense, new int?(num), fallToTrueOpt, fallToFalseOpt);
			this.CaptureResultIfNotAlready(condition.RightOperand.Syntax, num, operation);
			this.LeaveRegionsUpTo(currentRegionRequired);
			if (flag)
			{
				ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
				this.UnconditionalBranch(basicBlockBuilder);
				this.AppendNewBlock(ptr, true);
				ConstantValue constantValue = ConstantValue.Create(stopValue);
				SyntaxNode syntax = ((ptr.GetSingletonPredecessorOrDefault() != null) ? condition.LeftOperand : condition).Syntax;
				this.AddStatement(new FlowCaptureOperation(num, syntax, new LiteralOperation(null, syntax, condition.Type, constantValue, true)));
				this.AppendNewBlock(basicBlockBuilder, true);
			}
			return ControlFlowGraphBuilder.GetCaptureReference(num, condition);
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x00059D74 File Offset: 0x00057F74
		private IOperation VisitConditionalExpression(IOperation condition, bool sense, int? captureIdForResult, [Nullable(2)] ControlFlowGraphBuilder.BasicBlockBuilder fallToTrueOpt, [Nullable(2)] ControlFlowGraphBuilder.BasicBlockBuilder fallToFalseOpt)
		{
			IUnaryOperation unaryOperation = null;
			for (;;)
			{
				IOperation operation = condition;
				IParenthesizedOperation parenthesizedOperation = operation as IParenthesizedOperation;
				if (parenthesizedOperation == null)
				{
					IUnaryOperation unaryOperation2 = operation as IUnaryOperation;
					if (unaryOperation2 == null)
					{
						break;
					}
					if (!ControlFlowGraphBuilder.IsBooleanLogicalNot(unaryOperation2))
					{
						break;
					}
					unaryOperation = unaryOperation2;
					condition = unaryOperation2.Operand;
					sense = !sense;
				}
				else
				{
					condition = parenthesizedOperation.Operand;
				}
			}
			if (condition.Kind == OperationKind.Binary)
			{
				IBinaryOperation binaryOperation = (IBinaryOperation)condition;
				if (ControlFlowGraphBuilder.IsBooleanConditionalOperator(binaryOperation))
				{
					return this.VisitBinaryConditionalOperator(binaryOperation, sense, captureIdForResult, fallToTrueOpt, fallToFalseOpt);
				}
			}
			condition = this.VisitRequired(condition, default(int?));
			if (sense)
			{
				return condition;
			}
			if (unaryOperation == null)
			{
				return new UnaryOperation(UnaryOperatorKind.Not, condition, false, false, null, null, condition.Syntax, condition.Type, null, true);
			}
			return new UnaryOperation(unaryOperation.OperatorKind, condition, unaryOperation.IsLifted, unaryOperation.IsChecked, unaryOperation.OperatorMethod, null, unaryOperation.Syntax, unaryOperation.Type, unaryOperation.GetConstantValue(), this.IsImplicit(unaryOperation));
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x00059E58 File Offset: 0x00058058
		private static bool IsBooleanConditionalOperator(IBinaryOperation binOp)
		{
			return ControlFlowGraphBuilder.IsConditional(binOp) && binOp.OperatorMethod == null && ITypeSymbolHelpers.IsBooleanType(binOp.Type) && ITypeSymbolHelpers.IsBooleanType(binOp.LeftOperand.Type) && ITypeSymbolHelpers.IsBooleanType(binOp.RightOperand.Type);
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x00059EA6 File Offset: 0x000580A6
		private void VisitConditionalBranch(IOperation condition, [Nullable(2)] [NotNull] ref ControlFlowGraphBuilder.BasicBlockBuilder dest, bool jumpIfTrue)
		{
			this.SpillEvalStack();
			this.VisitConditionalBranchCore(condition, ref dest, jumpIfTrue);
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x00059EB8 File Offset: 0x000580B8
		private void VisitConditionalBranchCore(IOperation condition, [Nullable(2)] [NotNull] ref ControlFlowGraphBuilder.BasicBlockBuilder dest, bool jumpIfTrue)
		{
			IBinaryOperation binaryOperation;
			for (;;)
			{
				if (condition.Kind != OperationKind.Parenthesized)
				{
					OperationKind kind = condition.Kind;
					if (kind != OperationKind.Conversion)
					{
						switch (kind)
						{
						case OperationKind.Unary:
						{
							IUnaryOperation unaryOperation = (IUnaryOperation)condition;
							if (ControlFlowGraphBuilder.IsBooleanLogicalNot(unaryOperation))
							{
								jumpIfTrue = !jumpIfTrue;
								condition = unaryOperation.Operand;
								continue;
							}
							break;
						}
						case OperationKind.Binary:
							binaryOperation = (IBinaryOperation)condition;
							if (ControlFlowGraphBuilder.IsBooleanConditionalOperator(binaryOperation))
							{
								if (ControlFlowGraphBuilder.CalculateAndOrSense(binaryOperation, jumpIfTrue))
								{
									goto Block_5;
								}
								this.VisitConditionalBranchCore(binaryOperation.LeftOperand, ref dest, jumpIfTrue);
								condition = binaryOperation.RightOperand;
								continue;
							}
							break;
						case OperationKind.Conditional:
							goto IL_00CE;
						case OperationKind.Coalesce:
							goto IL_0166;
						}
						break;
					}
					goto IL_020A;
				}
				else
				{
					condition = ((IParenthesizedOperation)condition).Operand;
				}
			}
			goto IL_0246;
			Block_5:
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = null;
			this.VisitConditionalBranchCore(binaryOperation.LeftOperand, ref basicBlockBuilder, !jumpIfTrue);
			this.VisitConditionalBranchCore(binaryOperation.RightOperand, ref dest, jumpIfTrue);
			this.AppendNewBlock(basicBlockBuilder, true);
			return;
			IL_00CE:
			if (!ITypeSymbolHelpers.IsBooleanType(condition.Type))
			{
				goto IL_0246;
			}
			IConditionalOperation conditionalOperation = (IConditionalOperation)condition;
			if (ITypeSymbolHelpers.IsBooleanType(conditionalOperation.WhenTrue.Type) && ITypeSymbolHelpers.IsBooleanType(conditionalOperation.WhenFalse.Type))
			{
				ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 = null;
				this.VisitConditionalBranchCore(conditionalOperation.Condition, ref basicBlockBuilder2, false);
				this.VisitConditionalBranchCore(conditionalOperation.WhenTrue, ref dest, jumpIfTrue);
				ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder3 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
				this.UnconditionalBranch(basicBlockBuilder3);
				this.AppendNewBlock(basicBlockBuilder2, true);
				this.VisitConditionalBranchCore(conditionalOperation.WhenFalse, ref dest, jumpIfTrue);
				this.AppendNewBlock(basicBlockBuilder3, true);
				return;
			}
			goto IL_0246;
			IL_0166:
			if (!ITypeSymbolHelpers.IsBooleanType(condition.Type))
			{
				goto IL_0246;
			}
			ICoalesceOperation coalesceOperation = (ICoalesceOperation)condition;
			if (ITypeSymbolHelpers.IsBooleanType(coalesceOperation.WhenNull.Type))
			{
				ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder4 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
				ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
				IOperation operation = this.NullCheckAndConvertCoalesceValue(coalesceOperation, basicBlockBuilder4);
				dest = dest ?? new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
				this.ConditionalBranch(operation, jumpIfTrue, dest);
				this._currentBasicBlock = null;
				ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder5 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
				this.UnconditionalBranch(basicBlockBuilder5);
				this.PopStackFrameAndLeaveRegion(evalStackFrame);
				this.AppendNewBlock(basicBlockBuilder4, true);
				this.VisitConditionalBranchCore(coalesceOperation.WhenNull, ref dest, jumpIfTrue);
				this.AppendNewBlock(basicBlockBuilder5, true);
				return;
			}
			goto IL_0246;
			IL_020A:
			IConversionOperation conversionOperation = (IConversionOperation)condition;
			if (conversionOperation.Operand.Kind == OperationKind.Throw)
			{
				base.Visit(conversionOperation.Operand, default(int?));
				dest = dest ?? new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
				return;
			}
			IL_0246:
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame2 = this.PushStackFrame();
			condition = this.VisitRequired(condition, default(int?));
			dest = dest ?? new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			this.ConditionalBranch(condition, jumpIfTrue, dest);
			this._currentBasicBlock = null;
			this.PopStackFrameAndLeaveRegion(evalStackFrame2);
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x0005A150 File Offset: 0x00058350
		private void ConditionalBranch(IOperation condition, bool jumpIfTrue, ControlFlowGraphBuilder.BasicBlockBuilder destination)
		{
			ControlFlowGraphBuilder.BasicBlockBuilder currentBasicBlock = this.CurrentBasicBlock;
			ControlFlowGraphBuilder.BasicBlockBuilder.Branch branch = ControlFlowGraphBuilder.RegularBranch(destination);
			Operation.SetParentOperation<IOperation>(condition, null);
			branch.Destination.AddPredecessor(currentBasicBlock);
			currentBasicBlock.BranchValue = condition;
			currentBasicBlock.ConditionKind = (jumpIfTrue ? ControlFlowConditionKind.WhenTrue : ControlFlowConditionKind.WhenFalse);
			currentBasicBlock.Conditional = branch;
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x0005A19C File Offset: 0x0005839C
		private IOperation NullCheckAndConvertCoalesceValue(ICoalesceOperation operation, ControlFlowGraphBuilder.BasicBlockBuilder whenNull)
		{
			IOperation value = operation.Value;
			SyntaxNode syntax = value.Syntax;
			ITypeSymbol type = value.Type;
			this.PushOperand(this.VisitRequired(value, default(int?)));
			this.SpillEvalStack();
			IOperation operation2 = this.PopOperand();
			this.ConditionalBranch(this.MakeIsNullOperation(operation2), true, whenNull);
			this._currentBasicBlock = null;
			CommonConversion valueConversion = operation.ValueConversion;
			IOperation operation3 = OperationCloner.CloneOperation<IOperation>(operation2);
			IOperation operation4 = null;
			if (valueConversion.Exists)
			{
				IOperation operation5;
				if (ITypeSymbolHelpers.IsNullableType(type) && (!valueConversion.IsIdentity || !ITypeSymbolHelpers.IsNullableType(operation.Type)))
				{
					operation5 = this.TryCallNullableMember(operation3, SpecialMember.System_Nullable_T_GetValueOrDefault);
				}
				else
				{
					operation5 = operation3;
				}
				if (operation5 != null)
				{
					if (valueConversion.IsIdentity)
					{
						operation4 = operation5;
					}
					else
					{
						operation4 = new ConversionOperation(operation5, ((CoalesceOperation)operation).ValueConversionConvertible, false, false, null, syntax, operation.Type, null, true);
					}
				}
			}
			if (operation4 == null)
			{
				operation4 = ControlFlowGraphBuilder.MakeInvalidOperation(operation.Type, operation3);
			}
			return operation4;
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x0005A28C File Offset: 0x0005848C
		public override IOperation VisitCoalesce(ICoalesceOperation operation, int? captureIdForResult)
		{
			this.SpillEvalStack();
			IConversionOperation conversionOperation = operation.WhenNull as IConversionOperation;
			bool flag = conversionOperation != null && conversionOperation.Operand.Kind == OperationKind.Throw;
			ControlFlowGraphBuilder.RegionBuilder currentRegionRequired = this.CurrentRegionRequired;
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			IOperation operation2 = this.NullCheckAndConvertCoalesceValue(operation, basicBlockBuilder);
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			IOperation operation3;
			if (flag)
			{
				operation3 = operation2;
				this.UnconditionalBranch(basicBlockBuilder2);
				this.PopStackFrame(evalStackFrame, true);
				this.AppendNewBlock(basicBlockBuilder, true);
				base.Visit(conversionOperation.Operand, default(int?));
			}
			else
			{
				int num = captureIdForResult ?? this.GetNextCaptureId(currentRegionRequired);
				this.AddStatement(new FlowCaptureOperation(num, operation.Value.Syntax, operation2));
				operation3 = ControlFlowGraphBuilder.GetCaptureReference(num, operation);
				this.UnconditionalBranch(basicBlockBuilder2);
				this.PopStackFrameAndLeaveRegion(evalStackFrame);
				this.AppendNewBlock(basicBlockBuilder, true);
				this.VisitAndCapture(operation.WhenNull, num);
				this.LeaveRegionsUpTo(currentRegionRequired);
			}
			this.AppendNewBlock(basicBlockBuilder2, true);
			return operation3;
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x0005A398 File Offset: 0x00058598
		[return: Nullable(2)]
		public override IOperation VisitCoalesceAssignment(ICoalesceAssignmentOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.<>c__DisplayClass103_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.operation = operation;
			this.SpillEvalStack();
			CS$<>8__locals1.isStatement = this._currentStatement == CS$<>8__locals1.operation || CS$<>8__locals1.operation.Parent.Kind == OperationKind.ExpressionStatement;
			ControlFlowGraphBuilder.RegionBuilder currentRegionRequired = this.CurrentRegionRequired;
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.PushOperand(this.VisitRequired(CS$<>8__locals1.operation.Target, default(int?)));
			this.SpillEvalStack();
			CS$<>8__locals1.locationCapture = this.PopOperand();
			CS$<>8__locals1.valueFrame = this.PushStackFrame();
			this.SpillEvalStack();
			int nextCaptureId = this.GetNextCaptureId(CS$<>8__locals1.valueFrame.RegionBuilderOpt);
			this.AddStatement(new FlowCaptureOperation(nextCaptureId, CS$<>8__locals1.locationCapture.Syntax, CS$<>8__locals1.locationCapture));
			CS$<>8__locals1.valueCapture = ControlFlowGraphBuilder.GetCaptureReference(nextCaptureId, CS$<>8__locals1.locationCapture);
			CS$<>8__locals1.whenNull = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			CS$<>8__locals1.afterCoalesce = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			CS$<>8__locals1.resultCaptureId = (CS$<>8__locals1.isStatement ? (-1) : (captureIdForResult ?? this.GetNextCaptureId(currentRegionRequired)));
			IOperation target = CS$<>8__locals1.operation.Target;
			bool flag;
			if (target == null)
			{
				flag = false;
			}
			else
			{
				ITypeSymbol type = target.Type;
				SpecialType? specialType = ((type != null) ? new SpecialType?(type.OriginalDefinition.SpecialType) : default(SpecialType?));
				SpecialType specialType2 = SpecialType.System_Nullable_T;
				flag = (specialType.GetValueOrDefault() == specialType2) & (specialType != null);
			}
			if (flag && ((INamedTypeSymbol)CS$<>8__locals1.operation.Target.Type).TypeArguments[0].Equals(CS$<>8__locals1.operation.Type))
			{
				this.<VisitCoalesceAssignment>g__nullableValueTypeReturn|103_0(ref CS$<>8__locals1);
			}
			else
			{
				this.<VisitCoalesceAssignment>g__standardReturn|103_1(ref CS$<>8__locals1);
			}
			this.PopStackFrame(evalStackFrame, true);
			this.LeaveRegionsUpTo(currentRegionRequired);
			this.AppendNewBlock(CS$<>8__locals1.afterCoalesce, true);
			if (!CS$<>8__locals1.isStatement)
			{
				return ControlFlowGraphBuilder.GetCaptureReference(CS$<>8__locals1.resultCaptureId, CS$<>8__locals1.operation);
			}
			return null;
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x0005A598 File Offset: 0x00058798
		private static ControlFlowGraphBuilder.BasicBlockBuilder.Branch RegularBranch(ControlFlowGraphBuilder.BasicBlockBuilder destination)
		{
			return new ControlFlowGraphBuilder.BasicBlockBuilder.Branch
			{
				Destination = destination,
				Kind = ControlFlowBranchSemantics.Regular
			};
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x0005A5BE File Offset: 0x000587BE
		private static IOperation MakeInvalidOperation([Nullable(2)] ITypeSymbol type, IOperation child)
		{
			return new InvalidOperation(ImmutableArray.Create<IOperation>(child), null, child.Syntax, type, null, true);
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x0005A5D5 File Offset: 0x000587D5
		private static IOperation MakeInvalidOperation(SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, IOperation child1, IOperation child2)
		{
			return ControlFlowGraphBuilder.MakeInvalidOperation(syntax, type, ImmutableArray.Create<IOperation>(child1, child2));
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x0005A5E5 File Offset: 0x000587E5
		private static IOperation MakeInvalidOperation(SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> children)
		{
			return new InvalidOperation(children, null, syntax, type, null, true);
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x0005A5F2 File Offset: 0x000587F2
		private IsNullOperation MakeIsNullOperation(IOperation operand)
		{
			return ControlFlowGraphBuilder.MakeIsNullOperation(operand, this._compilation.GetSpecialType(SpecialType.System_Boolean));
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x0005A608 File Offset: 0x00058808
		private static IsNullOperation MakeIsNullOperation(IOperation operand, ITypeSymbol booleanType)
		{
			ConstantValue constantValue = operand.GetConstantValue();
			ConstantValue constantValue2;
			if (constantValue != null)
			{
				bool isNull = constantValue.IsNull;
				constantValue2 = ConstantValue.Create(isNull);
			}
			else
			{
				constantValue2 = null;
			}
			ConstantValue constantValue3 = constantValue2;
			return new IsNullOperation(operand.Syntax, operand, booleanType, constantValue3);
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x0005A640 File Offset: 0x00058840
		[return: Nullable(2)]
		private IOperation TryCallNullableMember(IOperation value, SpecialMember nullableMember)
		{
			ITypeSymbol type = value.Type;
			ISymbolInternal symbolInternal = this._compilation.CommonGetSpecialTypeMember(nullableMember);
			IMethodSymbol methodSymbol = (IMethodSymbol)((symbolInternal != null) ? symbolInternal.GetISymbol() : null);
			if (methodSymbol != null)
			{
				foreach (ISymbol symbol in type.GetMembers(methodSymbol.Name))
				{
					if (symbol.OriginalDefinition.Equals(methodSymbol))
					{
						methodSymbol = (IMethodSymbol)symbol;
						return new InvocationOperation(methodSymbol, value, false, ImmutableArray<IArgumentOperation>.Empty, null, value.Syntax, methodSymbol.ReturnType, true);
					}
				}
			}
			return null;
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x0005A6D2 File Offset: 0x000588D2
		private IOperation CallNullableMember(IOperation value, SpecialMember nullableMember)
		{
			return this.TryCallNullableMember(value, nullableMember) ?? ControlFlowGraphBuilder.MakeInvalidOperation(ITypeSymbolHelpers.GetNullableUnderlyingType(value.Type), value);
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x0005A6F4 File Offset: 0x000588F4
		[return: Nullable(2)]
		public override IOperation VisitConditionalAccess(IConditionalAccessOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.<>c__DisplayClass112_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			this.SpillEvalStack();
			ControlFlowGraphBuilder.RegionBuilder currentRegionRequired = this.CurrentRegionRequired;
			bool flag;
			if (this._currentStatement != operation)
			{
				if (this._currentStatement == operation.Parent)
				{
					IOperation currentStatement = this._currentStatement;
					flag = currentStatement != null && currentStatement.Kind == OperationKind.ExpressionStatement;
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				flag = true;
			}
			bool flag2 = flag;
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = null;
			ArrayBuilder<IOperation> instance = ArrayBuilder<IOperation>.GetInstance();
			if (!flag2)
			{
				evalStackFrame = this.PushStackFrame();
			}
			IConditionalAccessOperation conditionalAccessOperation = operation;
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			CS$<>8__locals1.previousTracker = this._currentConditionalAccessTracker;
			this._currentConditionalAccessTracker = new ControlFlowGraphBuilder.ConditionalAccessOperationTracker(instance, basicBlockBuilder);
			IOperation operation2;
			for (;;)
			{
				operation2 = conditionalAccessOperation.Operation;
				if (!ControlFlowGraphBuilder.<VisitConditionalAccess>g__isConditionalAccessInstancePresentInChildren|112_1(conditionalAccessOperation.WhenNotNull))
				{
					break;
				}
				instance.Push(operation2);
				IConditionalAccessOperation conditionalAccessOperation2 = conditionalAccessOperation.WhenNotNull as IConditionalAccessOperation;
				if (conditionalAccessOperation2 == null)
				{
					goto IL_00C6;
				}
				conditionalAccessOperation = conditionalAccessOperation2;
			}
			this.VisitConditionalAccessTestExpression(operation2);
			IL_00C6:
			if (flag2)
			{
				IOperation operation3 = this.VisitRequired(conditionalAccessOperation.WhenNotNull, default(int?));
				this.<VisitConditionalAccess>g__resetConditionalAccessTracker|112_0(ref CS$<>8__locals1);
				if (this._currentStatement != operation)
				{
					IExpressionStatementOperation expressionStatementOperation = (IExpressionStatementOperation)this._currentStatement;
					operation3 = new ExpressionStatementOperation(operation3, null, expressionStatementOperation.Syntax, this.IsImplicit(expressionStatementOperation));
				}
				this.AddStatement(operation3);
				this.AppendNewBlock(basicBlockBuilder, true);
				return null;
			}
			int num = captureIdForResult ?? this.GetNextCaptureId(currentRegionRequired);
			if (ITypeSymbolHelpers.IsNullableType(operation.Type) && !ITypeSymbolHelpers.IsNullableType(conditionalAccessOperation.WhenNotNull.Type))
			{
				IOperation operation4 = this.VisitRequired(conditionalAccessOperation.WhenNotNull, default(int?));
				this.AddStatement(new FlowCaptureOperation(num, conditionalAccessOperation.WhenNotNull.Syntax, this.MakeNullable(operation4, operation.Type)));
			}
			else
			{
				this.CaptureResultIfNotAlready(conditionalAccessOperation.WhenNotNull.Syntax, num, this.VisitRequired(conditionalAccessOperation.WhenNotNull, new int?(num)));
			}
			this.PopStackFrame(evalStackFrame, true);
			this.LeaveRegionsUpTo(currentRegionRequired);
			this.<VisitConditionalAccess>g__resetConditionalAccessTracker|112_0(ref CS$<>8__locals1);
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			this.UnconditionalBranch(basicBlockBuilder2);
			this.AppendNewBlock(basicBlockBuilder, true);
			SyntaxNode syntax = ((operation.Operation == operation2) ? operation2 : operation).Syntax;
			this.AddStatement(new FlowCaptureOperation(num, syntax, new DefaultValueOperation(null, syntax, operation.Type, (operation.Type.IsReferenceType && !ITypeSymbolHelpers.IsNullableType(operation.Type)) ? ConstantValue.Null : null, true)));
			this.AppendNewBlock(basicBlockBuilder2, true);
			return ControlFlowGraphBuilder.GetCaptureReference(num, operation);
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x0005A970 File Offset: 0x00058B70
		public override IOperation VisitConditionalAccessInstance(IConditionalAccessInstanceOperation operation, int? captureIdForResult)
		{
			IOperation operation2 = this._currentConditionalAccessTracker.Operations.Pop<IOperation>();
			return this.VisitConditionalAccessTestExpression(operation2);
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x0005A998 File Offset: 0x00058B98
		private IOperation VisitConditionalAccessTestExpression(IOperation testExpression)
		{
			SyntaxNode syntax = testExpression.Syntax;
			ITypeSymbol type = testExpression.Type;
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.PushOperand(this.VisitRequired(testExpression, default(int?)));
			this.SpillEvalStack();
			IOperation operation = this.PopOperand();
			this.PopStackFrame(evalStackFrame, true);
			this.ConditionalBranch(this.MakeIsNullOperation(operation), true, this._currentConditionalAccessTracker.WhenNull);
			this._currentBasicBlock = null;
			IOperation operation2 = OperationCloner.CloneOperation<IOperation>(operation);
			if (ITypeSymbolHelpers.IsNullableType(type))
			{
				operation2 = this.CallNullableMember(operation2, SpecialMember.System_Nullable_T_GetValueOrDefault);
			}
			return operation2;
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x0005AA20 File Offset: 0x00058C20
		[return: Nullable(2)]
		public override IOperation VisitExpressionStatement(IExpressionStatementOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			IOperation operation2 = this.Visit(operation.Operation);
			if (operation2 == null)
			{
				return this.FinishVisitingStatement(operation, null);
			}
			if (operation.Operation.Kind == OperationKind.Throw)
			{
				return this.FinishVisitingStatement(operation, null);
			}
			return this.FinishVisitingStatement(operation, new ExpressionStatementOperation(operation2, null, operation.Syntax, this.IsImplicit(operation)));
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x0005AA80 File Offset: 0x00058C80
		[return: Nullable(2)]
		public override IOperation VisitWhileLoop(IWhileLoopOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			ControlFlowGraphBuilder.RegionBuilder regionBuilder = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, operation.Locals, false);
			ControlFlowGraphBuilder.BasicBlockBuilder labeledOrNewBlock = this.GetLabeledOrNewBlock(operation.ContinueLabel);
			ControlFlowGraphBuilder.BasicBlockBuilder labeledOrNewBlock2 = this.GetLabeledOrNewBlock(operation.ExitLabel);
			if (operation.ConditionIsTop)
			{
				this.AppendNewBlock(labeledOrNewBlock, true);
				this.EnterRegion(regionBuilder, false);
				this.VisitConditionalBranch(operation.Condition, ref labeledOrNewBlock2, operation.ConditionIsUntil);
				this.VisitStatement(operation.Body);
				this.UnconditionalBranch(labeledOrNewBlock);
			}
			else
			{
				ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
				this.AppendNewBlock(basicBlockBuilder, true);
				this.EnterRegion(regionBuilder, false);
				this.VisitStatement(operation.Body);
				this.AppendNewBlock(labeledOrNewBlock, true);
				if (operation.Condition != null)
				{
					this.VisitConditionalBranch(operation.Condition, ref basicBlockBuilder, !operation.ConditionIsUntil);
				}
				else
				{
					this.UnconditionalBranch(basicBlockBuilder);
				}
			}
			this.LeaveRegion();
			this.AppendNewBlock(labeledOrNewBlock2, true);
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x0005AB68 File Offset: 0x00058D68
		[return: Nullable(2)]
		public override IOperation VisitTry(ITryOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			ControlFlowGraphBuilder.BasicBlockBuilder labeledOrNewBlock = this.GetLabeledOrNewBlock(operation.ExitLabel);
			if (operation.Catches.IsEmpty && operation.Finally == null)
			{
				this.VisitStatement(operation.Body);
				this.AppendNewBlock(labeledOrNewBlock, true);
				return this.FinishVisitingStatement(operation, null);
			}
			bool flag = operation.Finally != null;
			if (flag)
			{
				ControlFlowGraphBuilder.RegionBuilder regionBuilder = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.TryAndFinally, null, default(ImmutableArray<ILocalSymbol>), false);
				this.EnterRegion(regionBuilder, false);
				this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.Try, null, default(ImmutableArray<ILocalSymbol>), false), false);
			}
			bool flag2 = !operation.Catches.IsEmpty;
			if (flag2)
			{
				this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.TryAndCatch, null, default(ImmutableArray<ILocalSymbol>), false), false);
				this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.Try, null, default(ImmutableArray<ILocalSymbol>), false), false);
			}
			this.VisitStatement(operation.Body);
			this.UnconditionalBranch(labeledOrNewBlock);
			if (flag2)
			{
				this.LeaveRegion();
				foreach (ICatchClauseOperation catchClauseOperation in operation.Catches)
				{
					IOperation exceptionDeclarationOrExpression = catchClauseOperation.ExceptionDeclarationOrExpression;
					IOperation filter = catchClauseOperation.Filter;
					bool flag3 = filter != null;
					ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
					if (flag3)
					{
						ControlFlowGraphBuilder.RegionBuilder regionBuilder2 = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.FilterAndHandler, catchClauseOperation.ExceptionType, catchClauseOperation.Locals, false);
						this.EnterRegion(regionBuilder2, false);
						ControlFlowGraphBuilder.RegionBuilder regionBuilder3 = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.Filter, catchClauseOperation.ExceptionType, default(ImmutableArray<ILocalSymbol>), false);
						this.EnterRegion(regionBuilder3, false);
						this.AddExceptionStore(catchClauseOperation.ExceptionType, exceptionDeclarationOrExpression);
						this.VisitConditionalBranch(filter, ref basicBlockBuilder, true);
						ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
						this.AppendNewBlock(basicBlockBuilder2, true);
						basicBlockBuilder2.FallThrough.Kind = ControlFlowBranchSemantics.StructuredExceptionHandling;
						this.LeaveRegion();
					}
					ControlFlowGraphBuilder.RegionBuilder regionBuilder4 = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.Catch, catchClauseOperation.ExceptionType, flag3 ? default(ImmutableArray<ILocalSymbol>) : catchClauseOperation.Locals, false);
					this.EnterRegion(regionBuilder4, false);
					this.AppendNewBlock(basicBlockBuilder, false);
					if (!flag3)
					{
						this.AddExceptionStore(catchClauseOperation.ExceptionType, exceptionDeclarationOrExpression);
					}
					this.VisitStatement(catchClauseOperation.Handler);
					this.UnconditionalBranch(labeledOrNewBlock);
					this.LeaveRegion();
					if (flag3)
					{
						this.LeaveRegion();
					}
				}
				this.LeaveRegion();
			}
			if (flag)
			{
				this.LeaveRegion();
				ControlFlowGraphBuilder.RegionBuilder regionBuilder5 = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.Finally, null, default(ImmutableArray<ILocalSymbol>), false);
				this.EnterRegion(regionBuilder5, false);
				this.AppendNewBlock(new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block), true);
				this.VisitStatement(operation.Finally);
				ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder3 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
				this.AppendNewBlock(basicBlockBuilder3, true);
				basicBlockBuilder3.FallThrough.Kind = ControlFlowBranchSemantics.StructuredExceptionHandling;
				this.LeaveRegion();
				this.LeaveRegion();
			}
			this.AppendNewBlock(labeledOrNewBlock, false);
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x0005AE2C File Offset: 0x0005902C
		private void AddExceptionStore(ITypeSymbol exceptionType, [Nullable(2)] IOperation exceptionDeclarationOrExpression)
		{
			if (exceptionDeclarationOrExpression != null)
			{
				SyntaxNode syntax = exceptionDeclarationOrExpression.Syntax;
				IOperation operation;
				if (exceptionDeclarationOrExpression.Kind == OperationKind.VariableDeclarator)
				{
					ILocalSymbol symbol = ((IVariableDeclaratorOperation)exceptionDeclarationOrExpression).Symbol;
					operation = new LocalReferenceOperation(symbol, true, null, syntax, symbol.Type, null, true);
				}
				else
				{
					operation = this.VisitRequired(exceptionDeclarationOrExpression, default(int?));
				}
				if (operation != null)
				{
					this.AddStatement(new SimpleAssignmentOperation(false, operation, new CaughtExceptionOperation(syntax, exceptionType), null, syntax, null, null, true));
				}
			}
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x0005AE9A File Offset: 0x0005909A
		public override IOperation VisitCatchClause(ICatchClauseOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x0005AEA4 File Offset: 0x000590A4
		[return: Nullable(2)]
		public override IOperation VisitReturn(IReturnOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			IOperation operation2 = this.Visit(operation.ReturnedValue);
			OperationKind kind = operation.Kind;
			if (kind - OperationKind.Return > 1)
			{
				if (kind != OperationKind.YieldReturn)
				{
					throw ExceptionUtilities.UnexpectedValue(operation.Kind);
				}
				this.AddStatement(new ReturnOperation(operation2, OperationKind.YieldReturn, null, operation.Syntax, this.IsImplicit(operation)));
			}
			else
			{
				ControlFlowGraphBuilder.BasicBlockBuilder currentBasicBlock = this.CurrentBasicBlock;
				ControlFlowGraphBuilder.LinkBlocks(this.CurrentBasicBlock, this._exit, (operation2 == null) ? ControlFlowBranchSemantics.Regular : ControlFlowBranchSemantics.Return);
				currentBasicBlock.BranchValue = Operation.SetParentOperation<IOperation>(operation2, null);
				this._currentBasicBlock = null;
			}
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x0005AF42 File Offset: 0x00059142
		[return: Nullable(2)]
		public override IOperation VisitLabeled(ILabeledOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			this.VisitLabel(operation.Label);
			this.VisitStatement(operation.Operation);
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x0005AF6C File Offset: 0x0005916C
		public void VisitLabel(ILabelSymbol operation)
		{
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = this.GetLabeledOrNewBlock(operation);
			if (basicBlockBuilder.Ordinal != -1)
			{
				basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			}
			this.AppendNewBlock(basicBlockBuilder, true);
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x0005AF9C File Offset: 0x0005919C
		private ControlFlowGraphBuilder.BasicBlockBuilder GetLabeledOrNewBlock([Nullable(2)] ILabelSymbol labelOpt)
		{
			if (labelOpt == null)
			{
				return new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			}
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder;
			if (this._labeledBlocks == null)
			{
				this._labeledBlocks = PooledDictionary<ILabelSymbol, ControlFlowGraphBuilder.BasicBlockBuilder>.GetInstance();
			}
			else if (this._labeledBlocks.TryGetValue(labelOpt, ref basicBlockBuilder))
			{
				return basicBlockBuilder;
			}
			basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			this._labeledBlocks.Add(labelOpt, basicBlockBuilder);
			return basicBlockBuilder;
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x0005AFEF File Offset: 0x000591EF
		[return: Nullable(2)]
		public override IOperation VisitBranch(IBranchOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			this.UnconditionalBranch(this.GetLabeledOrNewBlock(operation.Target));
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x0005B012 File Offset: 0x00059212
		[return: Nullable(2)]
		public override IOperation VisitEmpty(IEmptyOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x0005B024 File Offset: 0x00059224
		[return: Nullable(2)]
		public override IOperation VisitThrow(IThrowOperation operation, int? captureIdForResult)
		{
			bool flag = this._currentStatement == operation;
			if (!flag)
			{
				this.SpillEvalStack();
			}
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.LinkThrowStatement(this.Visit(operation.Exception));
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
			this.AppendNewBlock(new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block), false);
			if (flag)
			{
				return null;
			}
			return new NoneOperation(ImmutableArray<IOperation>.Empty, null, operation.Syntax, null, null, true);
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x0005B088 File Offset: 0x00059288
		[NullableContext(2)]
		private void LinkThrowStatement(IOperation exception)
		{
			ControlFlowGraphBuilder.BasicBlockBuilder currentBasicBlock = this.CurrentBasicBlock;
			currentBasicBlock.BranchValue = Operation.SetParentOperation<IOperation>(exception, null);
			currentBasicBlock.FallThrough.Kind = ((exception == null) ? ControlFlowBranchSemantics.Rethrow : ControlFlowBranchSemantics.Throw);
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x0005B0B0 File Offset: 0x000592B0
		[return: Nullable(2)]
		public override IOperation VisitUsing(IUsingOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			DisposeOperationInfo disposeInfo = ((UsingOperation)operation).DisposeInfo;
			this.HandleUsingOperationParts(operation.Resources, operation.Body, disposeInfo.DisposeMethod, disposeInfo.DisposeArguments, operation.Locals, operation.IsAsynchronous);
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x0005B104 File Offset: 0x00059304
		private void HandleUsingOperationParts(IOperation resources, IOperation body, [Nullable(2)] IMethodSymbol disposeMethod, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IArgumentOperation> disposeArguments, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, bool isAsynchronous)
		{
			ControlFlowGraphBuilder.<>c__DisplayClass129_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.body = body;
			CS$<>8__locals1.disposeMethod = disposeMethod;
			CS$<>8__locals1.disposeArguments = disposeArguments;
			CS$<>8__locals1.isAsynchronous = isAsynchronous;
			ControlFlowGraphBuilder.RegionBuilder regionBuilder = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, locals, false);
			this.EnterRegion(regionBuilder, false);
			ITypeSymbol typeSymbol;
			if (!CS$<>8__locals1.isAsynchronous)
			{
				ITypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_IDisposable);
				typeSymbol = specialType;
			}
			else
			{
				typeSymbol = this._compilation.CommonGetWellKnownType(WellKnownType.System_IAsyncDisposable).GetITypeSymbol();
			}
			CS$<>8__locals1.iDisposable = typeSymbol;
			IVariableDeclarationGroupOperation variableDeclarationGroupOperation = resources as IVariableDeclarationGroupOperation;
			if (variableDeclarationGroupOperation != null)
			{
				ArrayBuilder<ValueTuple<IVariableDeclarationOperation, IVariableDeclaratorOperation>> instance = ArrayBuilder<ValueTuple<IVariableDeclarationOperation, IVariableDeclaratorOperation>>.GetInstance(variableDeclarationGroupOperation.Declarations.Length);
				foreach (IVariableDeclarationOperation variableDeclarationOperation in variableDeclarationGroupOperation.Declarations)
				{
					foreach (IVariableDeclaratorOperation variableDeclaratorOperation in variableDeclarationOperation.Declarators)
					{
						instance.Add(new ValueTuple<IVariableDeclarationOperation, IVariableDeclaratorOperation>(variableDeclarationOperation, variableDeclaratorOperation));
					}
				}
				instance.ReverseContents();
				this.<HandleUsingOperationParts>g__processQueue|129_0(instance, ref CS$<>8__locals1);
			}
			else
			{
				ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
				IOperation operation = this.VisitRequired(resources, default(int?));
				if (ControlFlowGraphBuilder.<HandleUsingOperationParts>g__shouldConvertToIDisposableBeforeTry|129_1(operation))
				{
					operation = this.ConvertToIDisposable(operation, CS$<>8__locals1.iDisposable, false);
				}
				this.PushOperand(operation);
				this.SpillEvalStack();
				operation = this.PopOperand();
				this.PopStackFrame(evalStackFrame, true);
				this.<HandleUsingOperationParts>g__processResource|129_2(operation, null, ref CS$<>8__locals1);
				this.LeaveRegionIfAny(evalStackFrame);
			}
			this.LeaveRegion();
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x0005B284 File Offset: 0x00059484
		private void AddDisposingFinally(IOperation resource, bool requiresRuntimeConversion, ITypeSymbol iDisposable, [Nullable(2)] IMethodSymbol disposeMethod, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IArgumentOperation> disposeArguments, bool isAsynchronous)
		{
			ControlFlowGraphBuilder.<>c__DisplayClass130_0 CS$<>8__locals1;
			CS$<>8__locals1.disposeMethod = disposeMethod;
			CS$<>8__locals1.isAsynchronous = isAsynchronous;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.disposeArguments = disposeArguments;
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			basicBlockBuilder.FallThrough.Kind = ControlFlowBranchSemantics.StructuredExceptionHandling;
			ControlFlowGraphBuilder.RegionBuilder regionBuilder = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.Finally, null, default(ImmutableArray<ILocalSymbol>), false);
			this.EnterRegion(regionBuilder, false);
			this.AppendNewBlock(new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block), true);
			if (requiresRuntimeConversion)
			{
				resource = this.ConvertToIDisposable(resource, iDisposable, true);
				int nextCaptureId = this.GetNextCaptureId(regionBuilder);
				this.AddStatement(new FlowCaptureOperation(nextCaptureId, resource.Syntax, resource));
				resource = ControlFlowGraphBuilder.GetCaptureReference(nextCaptureId, resource);
			}
			if (requiresRuntimeConversion || !ControlFlowGraphBuilder.<AddDisposingFinally>g__isNotNullableValueType|130_1(resource.Type))
			{
				IOperation operation = this.MakeIsNullOperation(OperationCloner.CloneOperation<IOperation>(resource));
				this.ConditionalBranch(operation, true, basicBlockBuilder);
				this._currentBasicBlock = null;
			}
			if (!iDisposable.Equals(resource.Type) && CS$<>8__locals1.disposeMethod == null)
			{
				resource = this.ConvertToIDisposable(resource, iDisposable, false);
			}
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.AddStatement(this.<AddDisposingFinally>g__tryDispose|130_0(resource, ref CS$<>8__locals1) ?? ControlFlowGraphBuilder.MakeInvalidOperation(null, resource));
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
			this.AppendNewBlock(basicBlockBuilder, true);
			this.LeaveRegion();
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x0005B3AC File Offset: 0x000595AC
		private IOperation ConvertToIDisposable(IOperation operand, ITypeSymbol iDisposable, bool isTryCast = false)
		{
			ConstantValue constantValue;
			return new ConversionOperation(operand, this._compilation.ClassifyConvertibleConversion(operand, iDisposable, out constantValue), isTryCast, false, null, operand.Syntax, iDisposable, constantValue, true);
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x0005B3DC File Offset: 0x000595DC
		[return: Nullable(2)]
		public override IOperation VisitLock(ILockOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			ITypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Object);
			LockOperation lockOperation = (LockOperation)operation;
			ControlFlowGraphBuilder.RegionBuilder regionBuilder = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, (lockOperation.LockTakenSymbol != null) ? ImmutableArray.Create<ILocalSymbol>(lockOperation.LockTakenSymbol) : ImmutableArray<ILocalSymbol>.Empty, false);
			this.EnterRegion(regionBuilder, false);
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			IOperation operation2 = this.VisitRequired(operation.LockedValue, default(int?));
			if (!specialType.Equals(operation2.Type))
			{
				operation2 = this.CreateConversion(operation2, specialType);
			}
			this.PushOperand(operation2);
			this.SpillEvalStack();
			operation2 = this.PopOperand();
			this.PopStackFrame(evalStackFrame, true);
			ISymbolInternal symbolInternal = this._compilation.CommonGetWellKnownTypeMember(WellKnownMember.System_Threading_Monitor__Enter2);
			IMethodSymbol methodSymbol = (IMethodSymbol)((symbolInternal != null) ? symbolInternal.GetISymbol() : null);
			bool flag = methodSymbol == null;
			if (flag)
			{
				ISymbolInternal symbolInternal2 = this._compilation.CommonGetWellKnownTypeMember(WellKnownMember.System_Threading_Monitor__Enter);
				methodSymbol = (IMethodSymbol)((symbolInternal2 != null) ? symbolInternal2.GetISymbol() : null);
				if (methodSymbol == null)
				{
					this.AddStatement(ControlFlowGraphBuilder.MakeInvalidOperation(null, operation2));
				}
				else
				{
					this.AddStatement(new InvocationOperation(methodSymbol, null, false, ImmutableArray.Create<IArgumentOperation>(new ArgumentOperation(ArgumentKind.Explicit, methodSymbol.Parameters[0], operation2, OperationFactory.IdentityConversion, OperationFactory.IdentityConversion, null, operation2.Syntax, true)), null, operation2.Syntax, methodSymbol.ReturnType, true));
				}
			}
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.TryAndFinally, null, default(ImmutableArray<ILocalSymbol>), false), false);
			this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.Try, null, default(ImmutableArray<ILocalSymbol>), false), false);
			if (!flag)
			{
				IOperation operation3 = new LocalReferenceOperation(lockOperation.LockTakenSymbol, true, null, operation2.Syntax, lockOperation.LockTakenSymbol.Type, null, true);
				this.AddStatement(new InvocationOperation(methodSymbol, null, false, ImmutableArray.Create<IArgumentOperation>(new ArgumentOperation(ArgumentKind.Explicit, methodSymbol.Parameters[0], operation2, OperationFactory.IdentityConversion, OperationFactory.IdentityConversion, null, operation2.Syntax, true), new ArgumentOperation(ArgumentKind.Explicit, methodSymbol.Parameters[1], operation3, OperationFactory.IdentityConversion, OperationFactory.IdentityConversion, null, operation2.Syntax, true)), null, operation2.Syntax, methodSymbol.ReturnType, true));
			}
			this.VisitStatement(operation.Body);
			this.UnconditionalBranch(basicBlockBuilder);
			this.LeaveRegion();
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			basicBlockBuilder2.FallThrough.Kind = ControlFlowBranchSemantics.StructuredExceptionHandling;
			this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.Finally, null, default(ImmutableArray<ILocalSymbol>), false), false);
			this.AppendNewBlock(new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block), true);
			if (!flag)
			{
				IOperation operation4 = new LocalReferenceOperation(lockOperation.LockTakenSymbol, false, null, operation2.Syntax, lockOperation.LockTakenSymbol.Type, null, true);
				this.ConditionalBranch(operation4, false, basicBlockBuilder2);
				this._currentBasicBlock = null;
			}
			ISymbolInternal symbolInternal3 = this._compilation.CommonGetWellKnownTypeMember(WellKnownMember.System_Threading_Monitor__Exit);
			IMethodSymbol methodSymbol2 = (IMethodSymbol)((symbolInternal3 != null) ? symbolInternal3.GetISymbol() : null);
			operation2 = OperationCloner.CloneOperation<IOperation>(operation2);
			if (methodSymbol2 == null)
			{
				this.AddStatement(ControlFlowGraphBuilder.MakeInvalidOperation(null, operation2));
			}
			else
			{
				this.AddStatement(new InvocationOperation(methodSymbol2, null, false, ImmutableArray.Create<IArgumentOperation>(new ArgumentOperation(ArgumentKind.Explicit, methodSymbol2.Parameters[0], operation2, OperationFactory.IdentityConversion, OperationFactory.IdentityConversion, null, operation2.Syntax, true)), null, operation2.Syntax, methodSymbol2.ReturnType, true));
			}
			this.AppendNewBlock(basicBlockBuilder2, true);
			this.LeaveRegion();
			this.LeaveRegion();
			this.LeaveRegionsUpTo(regionBuilder);
			this.LeaveRegion();
			this.AppendNewBlock(basicBlockBuilder, false);
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x0005B76C File Offset: 0x0005996C
		[return: Nullable(2)]
		public override IOperation VisitForEachLoop(IForEachLoopOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.<>c__DisplayClass133_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.operation = operation;
			this.StartVisitingStatement(CS$<>8__locals1.operation);
			CS$<>8__locals1.enumeratorCaptureRegion = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, default(ImmutableArray<ILocalSymbol>), false);
			this.EnterRegion(CS$<>8__locals1.enumeratorCaptureRegion, false);
			CS$<>8__locals1.info = ((ForEachLoopOperation)CS$<>8__locals1.operation).Info;
			ControlFlowGraphBuilder.RegionBuilder regionBuilder = null;
			if (!CS$<>8__locals1.operation.Locals.IsEmpty && CS$<>8__locals1.operation.LoopControlVariable.Kind == OperationKind.VariableDeclarator)
			{
				ILocalSymbol symbol = ((IVariableDeclaratorOperation)CS$<>8__locals1.operation.LoopControlVariable).Symbol;
				foreach (IOperation operation2 in CS$<>8__locals1.operation.Collection.DescendantsAndSelf())
				{
					ILocalReferenceOperation localReferenceOperation = operation2 as ILocalReferenceOperation;
					if (localReferenceOperation != null && localReferenceOperation.Local.Equals(symbol))
					{
						regionBuilder = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, ImmutableArray.Create<ILocalSymbol>(symbol), false);
						this.EnterRegion(regionBuilder, false);
						break;
					}
				}
			}
			IOperation operation3 = this.<VisitForEachLoop>g__getEnumerator|133_1(ref CS$<>8__locals1);
			if (regionBuilder != null)
			{
				this.LeaveRegion();
			}
			ForEachLoopOperationInfo info = CS$<>8__locals1.info;
			if (info != null && info.NeedsDispose)
			{
				this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.TryAndFinally, null, default(ImmutableArray<ILocalSymbol>), false), false);
				this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.Try, null, default(ImmutableArray<ILocalSymbol>), false), false);
			}
			ControlFlowGraphBuilder.BasicBlockBuilder labeledOrNewBlock = this.GetLabeledOrNewBlock(CS$<>8__locals1.operation.ContinueLabel);
			ControlFlowGraphBuilder.BasicBlockBuilder labeledOrNewBlock2 = this.GetLabeledOrNewBlock(CS$<>8__locals1.operation.ExitLabel);
			this.AppendNewBlock(labeledOrNewBlock, true);
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.ConditionalBranch(this.<VisitForEachLoop>g__getCondition|133_2(operation3, ref CS$<>8__locals1), false, labeledOrNewBlock2);
			this._currentBasicBlock = null;
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
			ControlFlowGraphBuilder.RegionBuilder regionBuilder2 = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, CS$<>8__locals1.operation.Locals, false);
			this.EnterRegion(regionBuilder2, false);
			evalStackFrame = this.PushStackFrame();
			ForEachLoopOperationInfo info2 = CS$<>8__locals1.info;
			IConvertibleConversion convertibleConversion = ((info2 != null) ? info2.CurrentConversion : null);
			IOperation operation4 = this.<VisitForEachLoop>g__getCurrent|133_3(OperationCloner.CloneOperation<IOperation>(operation3), ref CS$<>8__locals1);
			ForEachLoopOperationInfo info3 = CS$<>8__locals1.info;
			this.AddStatement(this.<VisitForEachLoop>g__getLoopControlVariableAssignment|133_4(ControlFlowGraphBuilder.<VisitForEachLoop>g__applyConversion|133_0(convertibleConversion, operation4, (info3 != null) ? info3.ElementType : null), ref CS$<>8__locals1));
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
			this.VisitStatement(CS$<>8__locals1.operation.Body);
			this.UnconditionalBranch(labeledOrNewBlock);
			this.LeaveRegion();
			this.AppendNewBlock(labeledOrNewBlock2, true);
			ForEachLoopOperationInfo info4 = CS$<>8__locals1.info;
			if (info4 != null && info4.NeedsDispose)
			{
				ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
				this.UnconditionalBranch(basicBlockBuilder);
				this.LeaveRegion();
				bool isAsynchronous = CS$<>8__locals1.info.IsAsynchronous;
				ITypeSymbol typeSymbol;
				if (!isAsynchronous)
				{
					ITypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_IDisposable);
					typeSymbol = specialType;
				}
				else
				{
					typeSymbol = this._compilation.CommonGetWellKnownType(WellKnownType.System_IAsyncDisposable).GetITypeSymbol();
				}
				ITypeSymbol typeSymbol2 = typeSymbol;
				this.AddDisposingFinally(OperationCloner.CloneOperation<IOperation>(operation3), !CS$<>8__locals1.info.KnownToImplementIDisposable && CS$<>8__locals1.info.PatternDisposeMethod == null, typeSymbol2, CS$<>8__locals1.info.PatternDisposeMethod, CS$<>8__locals1.info.DisposeArguments, isAsynchronous);
				this.LeaveRegion();
				this.AppendNewBlock(basicBlockBuilder, false);
			}
			this.LeaveRegion();
			return this.FinishVisitingStatement(CS$<>8__locals1.operation, null);
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x0005BAB0 File Offset: 0x00059CB0
		[return: Nullable(2)]
		public override IOperation VisitForToLoop(IForToLoopOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.<>c__DisplayClass134_0 CS$<>8__locals1;
			CS$<>8__locals1.operation = operation;
			CS$<>8__locals1.<>4__this = this;
			this.StartVisitingStatement(CS$<>8__locals1.operation);
			ValueTuple<ILocalSymbol, ForToLoopOperationUserDefinedInfo> info = ((ForToLoopOperation)CS$<>8__locals1.operation).Info;
			CS$<>8__locals1.loopObject = info.Item1;
			CS$<>8__locals1.userDefinedInfo = info.Item2;
			CS$<>8__locals1.isObjectLoop = CS$<>8__locals1.loopObject != null;
			ImmutableArray<ILocalSymbol> immutableArray = CS$<>8__locals1.operation.Locals;
			if (CS$<>8__locals1.isObjectLoop)
			{
				immutableArray = immutableArray.Insert(0, CS$<>8__locals1.loopObject);
			}
			CS$<>8__locals1.booleanType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
			ControlFlowGraphBuilder.BasicBlockBuilder labeledOrNewBlock = this.GetLabeledOrNewBlock(CS$<>8__locals1.operation.ContinueLabel);
			CS$<>8__locals1.@break = this.GetLabeledOrNewBlock(CS$<>8__locals1.operation.ExitLabel);
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			CS$<>8__locals1.bodyBlock = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			CS$<>8__locals1.loopRegion = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, immutableArray, false);
			this.EnterRegion(CS$<>8__locals1.loopRegion, false);
			CS$<>8__locals1.limitValueId = -1;
			CS$<>8__locals1.stepValueId = -1;
			CS$<>8__locals1.positiveFlag = null;
			CS$<>8__locals1.stepEnumUnderlyingTypeOrSelf = ITypeSymbolHelpers.GetEnumUnderlyingTypeOrSelf(CS$<>8__locals1.operation.StepValue.Type);
			this.<VisitForToLoop>g__initializeLoop|134_1(ref CS$<>8__locals1);
			this.AppendNewBlock(basicBlockBuilder, true);
			this.<VisitForToLoop>g__checkLoopCondition|134_2(ref CS$<>8__locals1);
			this.AppendNewBlock(CS$<>8__locals1.bodyBlock, true);
			this.VisitStatement(CS$<>8__locals1.operation.Body);
			this.AppendNewBlock(labeledOrNewBlock, true);
			this.<VisitForToLoop>g__incrementLoopControlVariable|134_4(ref CS$<>8__locals1);
			this.UnconditionalBranch(basicBlockBuilder);
			this.LeaveRegion();
			this.AppendNewBlock(CS$<>8__locals1.@break, true);
			return this.FinishVisitingStatement(CS$<>8__locals1.operation, null);
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x0005BC49 File Offset: 0x00059E49
		private static FlowCaptureReferenceOperation GetCaptureReference(int id, IOperation underlying)
		{
			return new FlowCaptureReferenceOperation(id, underlying.Syntax, underlying.Type, underlying.GetConstantValue());
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x0005BC64 File Offset: 0x00059E64
		internal override IOperation VisitAggregateQuery(IAggregateQueryOperation operation, int? captureIdForResult)
		{
			this.SpillEvalStack();
			IOperation currentAggregationGroup = this._currentAggregationGroup;
			this._currentAggregationGroup = this.VisitAndCapture(operation.Group);
			IOperation operation2 = this.VisitRequired(operation.Aggregation, default(int?));
			this._currentAggregationGroup = currentAggregationGroup;
			return operation2;
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x0005BCAC File Offset: 0x00059EAC
		[return: Nullable(2)]
		public override IOperation VisitSwitch(ISwitchOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.<>c__DisplayClass137_0 CS$<>8__locals1;
			CS$<>8__locals1.operation = operation;
			CS$<>8__locals1.<>4__this = this;
			this.StartVisitingStatement(CS$<>8__locals1.operation);
			CS$<>8__locals1.booleanType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
			CS$<>8__locals1.switchValue = this.VisitAndCapture(CS$<>8__locals1.operation.Value);
			ImmutableArray<ILocalSymbol> immutableArray = this.<VisitSwitch>g__getLocals|137_0(ref CS$<>8__locals1);
			ControlFlowGraphBuilder.RegionBuilder regionBuilder = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, immutableArray, false);
			this.EnterRegion(regionBuilder, false);
			CS$<>8__locals1.defaultBody = null;
			CS$<>8__locals1.@break = this.GetLabeledOrNewBlock(CS$<>8__locals1.operation.ExitLabel);
			foreach (ISwitchCaseOperation switchCaseOperation in CS$<>8__locals1.operation.Cases)
			{
				this.<VisitSwitch>g__handleSection|137_1(switchCaseOperation, ref CS$<>8__locals1);
			}
			if (CS$<>8__locals1.defaultBody != null)
			{
				this.UnconditionalBranch(CS$<>8__locals1.defaultBody);
			}
			this.LeaveRegion();
			this.AppendNewBlock(CS$<>8__locals1.@break, true);
			return this.FinishVisitingStatement(CS$<>8__locals1.operation, null);
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x0005BDA2 File Offset: 0x00059FA2
		private IOperation MakeNullable(IOperation operand, ITypeSymbol type)
		{
			return this.CreateConversion(operand, type);
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x0005BDAC File Offset: 0x00059FAC
		public override IOperation VisitSwitchCase(ISwitchCaseOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x0005BDB3 File Offset: 0x00059FB3
		public override IOperation VisitSingleValueCaseClause(ISingleValueCaseClauseOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x0005BDBA File Offset: 0x00059FBA
		public override IOperation VisitDefaultCaseClause(IDefaultCaseClauseOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x0005BDC1 File Offset: 0x00059FC1
		public override IOperation VisitRelationalCaseClause(IRelationalCaseClauseOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x0005BDC8 File Offset: 0x00059FC8
		public override IOperation VisitRangeCaseClause(IRangeCaseClauseOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x0005BDCF File Offset: 0x00059FCF
		public override IOperation VisitPatternCaseClause(IPatternCaseClauseOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x0005BDD6 File Offset: 0x00059FD6
		[return: Nullable(2)]
		public override IOperation VisitEnd(IEndOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			ControlFlowGraphBuilder.BasicBlockBuilder currentBasicBlock = this.CurrentBasicBlock;
			this.AppendNewBlock(new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block), false);
			currentBasicBlock.FallThrough.Kind = ControlFlowBranchSemantics.ProgramTermination;
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x0005BE08 File Offset: 0x0005A008
		[return: Nullable(2)]
		public override IOperation VisitForLoop(IForLoopOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, operation.Locals, false), false);
			ImmutableArray<IOperation> before = operation.Before;
			if (before.Length == 1 && before[0].Kind == OperationKind.VariableDeclarationGroup)
			{
				this.HandleVariableDeclarations((VariableDeclarationGroupOperation)before.Single<IOperation>());
			}
			else
			{
				this.VisitStatements(before);
			}
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			this.AppendNewBlock(basicBlockBuilder, true);
			this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, operation.ConditionLocals, false), false);
			ControlFlowGraphBuilder.BasicBlockBuilder labeledOrNewBlock = this.GetLabeledOrNewBlock(operation.ExitLabel);
			if (operation.Condition != null)
			{
				this.VisitConditionalBranch(operation.Condition, ref labeledOrNewBlock, false);
			}
			this.VisitStatement(operation.Body);
			ControlFlowGraphBuilder.BasicBlockBuilder labeledOrNewBlock2 = this.GetLabeledOrNewBlock(operation.ContinueLabel);
			this.AppendNewBlock(labeledOrNewBlock2, true);
			this.VisitStatements(operation.AtLoopBottom);
			this.UnconditionalBranch(basicBlockBuilder);
			this.LeaveRegion();
			this.LeaveRegion();
			this.AppendNewBlock(labeledOrNewBlock, true);
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x0005BF04 File Offset: 0x0005A104
		[return: Nullable(2)]
		internal override IOperation VisitFixed(IFixedOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, operation.Locals, false), false);
			this.HandleVariableDeclarations(operation.Variables);
			this.VisitStatement(operation.Body);
			this.LeaveRegion();
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0005BF53 File Offset: 0x0005A153
		[return: Nullable(2)]
		public override IOperation VisitVariableDeclarationGroup(IVariableDeclarationGroupOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			this.HandleVariableDeclarations(operation);
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x0005BF6C File Offset: 0x0005A16C
		private void HandleVariableDeclarations(IVariableDeclarationGroupOperation operation)
		{
			foreach (IVariableDeclarationOperation variableDeclarationOperation in operation.Declarations)
			{
				this.HandleVariableDeclaration(variableDeclarationOperation);
			}
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x0005BFA4 File Offset: 0x0005A1A4
		private void HandleVariableDeclaration(IVariableDeclarationOperation operation)
		{
			foreach (IVariableDeclaratorOperation variableDeclaratorOperation in operation.Declarators)
			{
				this.HandleVariableDeclarator(operation, variableDeclaratorOperation);
			}
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x0005BFDC File Offset: 0x0005A1DC
		private void HandleVariableDeclarator(IVariableDeclarationOperation declaration, IVariableDeclaratorOperation declarator)
		{
			if (declarator.Initializer == null && declaration.Initializer == null)
			{
				return;
			}
			ILocalSymbol symbol = declarator.Symbol;
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = null;
			if (symbol.IsStatic)
			{
				basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
				ITypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
				StaticLocalInitializationSemaphoreOperation staticLocalInitializationSemaphoreOperation = new StaticLocalInitializationSemaphoreOperation(symbol, declarator.Syntax, specialType);
				this.ConditionalBranch(staticLocalInitializationSemaphoreOperation, false, basicBlockBuilder);
				this._currentBasicBlock = null;
				this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.StaticLocalInitializer, null, default(ImmutableArray<ILocalSymbol>), false), false);
			}
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			IOperation operation = null;
			SyntaxNode syntaxNode = null;
			if (declarator.Initializer != null)
			{
				operation = this.Visit(declarator.Initializer.Value);
				syntaxNode = declarator.Syntax;
			}
			if (declaration.Initializer != null)
			{
				IOperation operation2 = this.VisitRequired(declaration.Initializer.Value, default(int?));
				syntaxNode = declaration.Syntax;
				if (operation != null)
				{
					operation = new InvalidOperation(ImmutableArray.Create<IOperation>(operation, operation2), null, declaration.Syntax, symbol.Type, null, true);
				}
				else
				{
					operation = operation2;
				}
			}
			LocalReferenceOperation localReferenceOperation = new LocalReferenceOperation(symbol, true, null, declarator.Syntax, symbol.Type, null, true);
			SimpleAssignmentOperation simpleAssignmentOperation = new SimpleAssignmentOperation(symbol.IsRef, localReferenceOperation, operation, null, syntaxNode, localReferenceOperation.Type, null, true);
			this.AddStatement(simpleAssignmentOperation);
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
			if (symbol.IsStatic)
			{
				this.LeaveRegion();
				this.AppendNewBlock(basicBlockBuilder, true);
			}
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x0005C134 File Offset: 0x0005A334
		public override IOperation VisitVariableDeclaration(IVariableDeclarationOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x0005C13B File Offset: 0x0005A33B
		public override IOperation VisitVariableDeclarator(IVariableDeclaratorOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x0005C142 File Offset: 0x0005A342
		public override IOperation VisitVariableInitializer(IVariableInitializerOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x0005C149 File Offset: 0x0005A349
		public override IOperation VisitFlowCapture(IFlowCaptureOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x0005C150 File Offset: 0x0005A350
		public override IOperation VisitFlowCaptureReference(IFlowCaptureReferenceOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x0005C157 File Offset: 0x0005A357
		public override IOperation VisitIsNull(IIsNullOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x0005C15E File Offset: 0x0005A35E
		public override IOperation VisitCaughtException(ICaughtExceptionOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x0005C168 File Offset: 0x0005A368
		public override IOperation VisitInvocation(IInvocationOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			IOperation operation2 = (operation.TargetMethod.IsStatic ? null : operation.Instance);
			ValueTuple<IOperation, ImmutableArray<IArgumentOperation>> valueTuple = this.VisitInstanceWithArguments(operation2, operation.Arguments);
			IOperation item = valueTuple.Item1;
			ImmutableArray<IArgumentOperation> item2 = valueTuple.Item2;
			this.PopStackFrame(evalStackFrame, true);
			return new InvocationOperation(operation.TargetMethod, item, operation.IsVirtual, item2, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x0005C1DC File Offset: 0x0005A3DC
		[NullableContext(2)]
		[return: TupleElementNames(new string[] { "visitedInstance", "visitedArguments" })]
		[return: Nullable(new byte[] { 0, 2, 0, 1 })]
		private ValueTuple<IOperation, ImmutableArray<IArgumentOperation>> VisitInstanceWithArguments(IOperation instance, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IArgumentOperation> arguments)
		{
			if (instance != null)
			{
				this.PushOperand(this.VisitRequired(instance, default(int?)));
			}
			ImmutableArray<IArgumentOperation> immutableArray = this.VisitArguments(arguments);
			return new ValueTuple<IOperation, ImmutableArray<IArgumentOperation>>((instance == null) ? null : this.PopOperand(), immutableArray);
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x0005C21C File Offset: 0x0005A41C
		internal override IOperation VisitNoPiaObjectCreation(INoPiaObjectCreationOperation operation, int? argument)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			IOperation operation2 = new NoPiaObjectCreationOperation(null, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
			return this.PopStackFrame<IOperation>(evalStackFrame, this.HandleObjectOrCollectionInitializer(operation.Initializer, operation2));
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x0005C260 File Offset: 0x0005A460
		public override IOperation VisitObjectCreation(IObjectCreationOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame2 = this.PushStackFrame();
			ImmutableArray<IArgumentOperation> immutableArray = this.VisitArguments(operation.Arguments);
			this.PopStackFrame(evalStackFrame2, true);
			IOperation operation2 = new ObjectCreationOperation(operation.Constructor, null, immutableArray, null, operation.Syntax, operation.Type, operation.GetConstantValue(), this.IsImplicit(operation));
			return this.PopStackFrame<IOperation>(evalStackFrame, this.HandleObjectOrCollectionInitializer(operation.Initializer, operation2));
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x0005C2CC File Offset: 0x0005A4CC
		public override IOperation VisitTypeParameterObjectCreation(ITypeParameterObjectCreationOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			TypeParameterObjectCreationOperation typeParameterObjectCreationOperation = new TypeParameterObjectCreationOperation(null, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
			return this.PopStackFrame<IOperation>(evalStackFrame, this.HandleObjectOrCollectionInitializer(operation.Initializer, typeParameterObjectCreationOperation));
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x0005C310 File Offset: 0x0005A510
		public override IOperation VisitDynamicObjectCreation(IDynamicObjectCreationOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame2 = this.PushStackFrame();
			ImmutableArray<IOperation> immutableArray = this.VisitArray<IOperation>(operation.Arguments, null, null);
			this.PopStackFrame(evalStackFrame2, true);
			HasDynamicArgumentsExpression hasDynamicArgumentsExpression = (HasDynamicArgumentsExpression)operation;
			IOperation operation2 = new DynamicObjectCreationOperation(null, immutableArray, hasDynamicArgumentsExpression.ArgumentNames, hasDynamicArgumentsExpression.ArgumentRefKinds, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
			return this.PopStackFrame<IOperation>(evalStackFrame, this.HandleObjectOrCollectionInitializer(operation.Initializer, operation2));
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x0005C388 File Offset: 0x0005A588
		private IOperation HandleObjectOrCollectionInitializer([Nullable(2)] IObjectOrCollectionInitializerOperation initializer, IOperation objectCreation)
		{
			if (initializer == null || initializer.Initializers.IsEmpty)
			{
				return objectCreation;
			}
			this.PushOperand(objectCreation);
			this.SpillEvalStack();
			objectCreation = this.PopOperand();
			this.<HandleObjectOrCollectionInitializer>g__visitInitializer|165_0(initializer, objectCreation);
			return objectCreation;
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x0005C3C8 File Offset: 0x0005A5C8
		public override IOperation VisitObjectOrCollectionInitializer(IObjectOrCollectionInitializerOperation operation, int? captureIdForResult)
		{
			return ControlFlowGraphBuilder.MakeInvalidOperation(operation.Syntax, operation.Type, ImmutableArray<IOperation>.Empty);
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x0005C3E0 File Offset: 0x0005A5E0
		public override IOperation VisitMemberInitializer(IMemberInitializerOperation operation, int? captureIdForResult)
		{
			return ControlFlowGraphBuilder.MakeInvalidOperation(operation.Syntax, operation.Type, ImmutableArray<IOperation>.Empty);
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x0005C3F8 File Offset: 0x0005A5F8
		public override IOperation VisitAnonymousObjectCreation(IAnonymousObjectCreationOperation operation, int? captureIdForResult)
		{
			if (operation.Initializers.IsEmpty)
			{
				return new AnonymousObjectCreationOperation(ImmutableArray<IOperation>.Empty, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
			}
			ControlFlowGraphBuilder.ImplicitInstanceInfo currentImplicitInstance = this._currentImplicitInstance;
			this._currentImplicitInstance = new ControlFlowGraphBuilder.ImplicitInstanceInfo((INamedTypeSymbol)operation.Type);
			this.SpillEvalStack();
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			ArrayBuilder<IOperation> instance = ArrayBuilder<IOperation>.GetInstance(operation.Initializers.Length);
			for (int i = 0; i < operation.Initializers.Length; i++)
			{
				ISimpleAssignmentOperation simpleAssignmentOperation = (ISimpleAssignmentOperation)operation.Initializers[i];
				IPropertyReferenceOperation propertyReferenceOperation = (IPropertyReferenceOperation)simpleAssignmentOperation.Target;
				InstanceReferenceOperation instanceReferenceOperation = new InstanceReferenceOperation(InstanceReferenceKind.ImplicitReceiver, null, propertyReferenceOperation.Instance.Syntax, propertyReferenceOperation.Instance.Type, this.IsImplicit(propertyReferenceOperation.Instance));
				IOperation operation2 = new PropertyReferenceOperation(propertyReferenceOperation.Property, ImmutableArray<IArgumentOperation>.Empty, instanceReferenceOperation, null, propertyReferenceOperation.Syntax, propertyReferenceOperation.Type, this.IsImplicit(propertyReferenceOperation));
				IOperation operation3 = this.<VisitAnonymousObjectCreation>g__visitAndCaptureInitializer|168_0(propertyReferenceOperation.Property, simpleAssignmentOperation.Value);
				SimpleAssignmentOperation simpleAssignmentOperation2 = new SimpleAssignmentOperation(simpleAssignmentOperation.IsRef, operation2, operation3, null, simpleAssignmentOperation.Syntax, simpleAssignmentOperation.Type, simpleAssignmentOperation.GetConstantValue(), this.IsImplicit(simpleAssignmentOperation));
				instance.Add(simpleAssignmentOperation2);
			}
			this._currentImplicitInstance.Free();
			this._currentImplicitInstance = currentImplicitInstance;
			for (int j = 0; j < instance.Count; j++)
			{
				this.PopOperand();
			}
			this.PopStackFrame(evalStackFrame, true);
			return new AnonymousObjectCreationOperation(instance.ToImmutableAndFree(), null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x0005C5B8 File Offset: 0x0005A7B8
		[return: Nullable(2)]
		public override IOperation VisitLocalFunction(ILocalFunctionOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			ControlFlowGraphBuilder.RegionBuilder regionBuilder = this.CurrentRegionRequired;
			while (regionBuilder.IsStackSpillRegion)
			{
				regionBuilder = regionBuilder.Enclosing;
			}
			regionBuilder.Add(operation.Symbol, operation);
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x0005C5F9 File Offset: 0x0005A7F9
		[return: Nullable(2)]
		private IOperation VisitLocalFunctionAsRoot(ILocalFunctionOperation operation)
		{
			this.VisitMethodBodies(operation.Body, operation.IgnoredBody);
			return null;
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x0005C610 File Offset: 0x0005A810
		public override IOperation VisitAnonymousFunction(IAnonymousFunctionOperation operation, int? captureIdForResult)
		{
			this._haveAnonymousFunction = true;
			ControlFlowGraphBuilder.Context currentContext = this.GetCurrentContext();
			return new FlowAnonymousFunctionOperation(in currentContext, operation, this.IsImplicit(operation));
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x0005C63A File Offset: 0x0005A83A
		public override IOperation VisitFlowAnonymousFunction(IFlowAnonymousFunctionOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x0005C644 File Offset: 0x0005A844
		public override IOperation VisitArrayCreation(IArrayCreationOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.VisitAndPushArray<IOperation>(operation.DimensionSizes, null);
			IArrayInitializerOperation arrayInitializerOperation = (IArrayInitializerOperation)this.Visit(operation.Initializer);
			ImmutableArray<IOperation> immutableArray = this.PopArray<IOperation>(operation.DimensionSizes, null);
			this.PopStackFrame(evalStackFrame, true);
			return new ArrayCreationOperation(immutableArray, arrayInitializerOperation, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x0005C6A8 File Offset: 0x0005A8A8
		public override IOperation VisitArrayInitializer(IArrayInitializerOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.<VisitArrayInitializer>g__visitAndPushArrayInitializerValues|174_0(operation);
			return this.PopStackFrame<IArrayInitializerOperation>(evalStackFrame, this.<VisitArrayInitializer>g__popAndAssembleArrayInitializerValues|174_1(operation));
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x0005C6D4 File Offset: 0x0005A8D4
		public override IOperation VisitInstanceReference(IInstanceReferenceOperation operation, int? captureIdForResult)
		{
			if (operation.ReferenceKind != InstanceReferenceKind.ImplicitReceiver)
			{
				return new InstanceReferenceOperation(operation.ReferenceKind, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
			}
			if (this._currentImplicitInstance.ImplicitInstance != null)
			{
				return OperationCloner.CloneOperation<IOperation>(this._currentImplicitInstance.ImplicitInstance);
			}
			return ControlFlowGraphBuilder.MakeInvalidOperation(operation.Syntax, operation.Type, ImmutableArray<IOperation>.Empty);
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x0005C740 File Offset: 0x0005A940
		public override IOperation VisitDynamicInvocation(IDynamicInvocationOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			if (operation.Operation.Kind == OperationKind.DynamicMemberReference)
			{
				IOperation instance = ((IDynamicMemberReferenceOperation)operation.Operation).Instance;
				if (instance != null)
				{
					this.PushOperand(this.VisitRequired(instance, default(int?)));
				}
			}
			else
			{
				this.PushOperand(this.VisitRequired(operation.Operation, default(int?)));
			}
			ImmutableArray<IOperation> immutableArray = this.VisitArray<IOperation>(operation.Arguments, null, null);
			IOperation operation2;
			if (operation.Operation.Kind == OperationKind.DynamicMemberReference)
			{
				IDynamicMemberReferenceOperation dynamicMemberReferenceOperation = (IDynamicMemberReferenceOperation)operation.Operation;
				operation2 = new DynamicMemberReferenceOperation((dynamicMemberReferenceOperation.Instance != null) ? this.PopOperand() : null, dynamicMemberReferenceOperation.MemberName, dynamicMemberReferenceOperation.TypeArguments, dynamicMemberReferenceOperation.ContainingType, null, dynamicMemberReferenceOperation.Syntax, dynamicMemberReferenceOperation.Type, this.IsImplicit(dynamicMemberReferenceOperation));
			}
			else
			{
				operation2 = this.PopOperand();
			}
			this.PopStackFrame(evalStackFrame, true);
			return new DynamicInvocationOperation(operation2, immutableArray, ((HasDynamicArgumentsExpression)operation).ArgumentNames, ((HasDynamicArgumentsExpression)operation).ArgumentRefKinds, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x0005C860 File Offset: 0x0005AA60
		public override IOperation VisitDynamicIndexerAccess(IDynamicIndexerAccessOperation operation, int? captureIdForResult)
		{
			this.PushOperand(this.VisitRequired(operation.Operation, default(int?)));
			ImmutableArray<IOperation> immutableArray = this.VisitArray<IOperation>(operation.Arguments, null, null);
			return new DynamicIndexerAccessOperation(this.PopOperand(), immutableArray, ((HasDynamicArgumentsExpression)operation).ArgumentNames, ((HasDynamicArgumentsExpression)operation).ArgumentRefKinds, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x0005C8D0 File Offset: 0x0005AAD0
		public override IOperation VisitDynamicMemberReference(IDynamicMemberReferenceOperation operation, int? captureIdForResult)
		{
			return new DynamicMemberReferenceOperation(this.Visit(operation.Instance), operation.MemberName, operation.TypeArguments, operation.ContainingType, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x0005C914 File Offset: 0x0005AB14
		public override IOperation VisitDeconstructionAssignment(IDeconstructionAssignmentOperation operation, int? captureIdForResult)
		{
			ValueTuple<IOperation, IOperation> valueTuple = this.VisitPreservingTupleOperations(operation.Target, operation.Value);
			IOperation item = valueTuple.Item1;
			IOperation item2 = valueTuple.Item2;
			return new DeconstructionAssignmentOperation(item, item2, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x0005C95C File Offset: 0x0005AB5C
		private void PushTargetAndUnwrapTupleIfNecessary(IOperation value)
		{
			if (value.Kind == OperationKind.Tuple)
			{
				foreach (IOperation operation in ((ITupleOperation)value).Elements)
				{
					this.PushTargetAndUnwrapTupleIfNecessary(operation);
				}
				return;
			}
			this.PushOperand(this.VisitRequired(value, default(int?)));
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x0005C9B8 File Offset: 0x0005ABB8
		private IOperation PopTargetAndWrapTupleIfNecessary(IOperation value)
		{
			if (value.Kind == OperationKind.Tuple)
			{
				ITupleOperation tupleOperation = (ITupleOperation)value;
				int length = tupleOperation.Elements.Length;
				ArrayBuilder<IOperation> instance = ArrayBuilder<IOperation>.GetInstance(length);
				for (int i = length - 1; i >= 0; i--)
				{
					instance.Add(this.PopTargetAndWrapTupleIfNecessary(tupleOperation.Elements[i]));
				}
				instance.ReverseContents();
				return new TupleOperation(instance.ToImmutableAndFree(), tupleOperation.NaturalType, null, tupleOperation.Syntax, tupleOperation.Type, this.IsImplicit(tupleOperation));
			}
			return this.PopOperand();
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x0005CA45 File Offset: 0x0005AC45
		public override IOperation VisitDeclarationExpression(IDeclarationExpressionOperation operation, int? captureIdForResult)
		{
			return new DeclarationExpressionOperation(this.VisitPreservingTupleOperations(operation.Expression), null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x0005CA6C File Offset: 0x0005AC6C
		private IOperation VisitPreservingTupleOperations(IOperation operation)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.PushTargetAndUnwrapTupleIfNecessary(operation);
			return this.PopStackFrame<IOperation>(evalStackFrame, this.PopTargetAndWrapTupleIfNecessary(operation));
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x0005CA98 File Offset: 0x0005AC98
		[return: TupleElementNames(new string[] { "visitedLeft", "visitedRight" })]
		[return: Nullable(new byte[] { 0, 1, 1 })]
		private ValueTuple<IOperation, IOperation> VisitPreservingTupleOperations(IOperation left, IOperation right)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.PushTargetAndUnwrapTupleIfNecessary(left);
			IOperation operation = this.VisitRequired(right, default(int?));
			IOperation operation2 = this.PopTargetAndWrapTupleIfNecessary(left);
			this.PopStackFrame(evalStackFrame, true);
			return new ValueTuple<IOperation, IOperation>(operation2, operation);
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x0005CAD9 File Offset: 0x0005ACD9
		public override IOperation VisitTuple(ITupleOperation operation, int? captureIdForResult)
		{
			return this.VisitPreservingTupleOperations(operation);
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x0005CAE2 File Offset: 0x0005ACE2
		internal override IOperation VisitNoneOperation(IOperation operation, int? captureIdForResult)
		{
			if (this._currentStatement == operation)
			{
				return this.VisitNoneOperationStatement(operation);
			}
			return this.VisitNoneOperationExpression(operation);
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x0005CAFC File Offset: 0x0005ACFC
		private IOperation VisitNoneOperationStatement(IOperation operation)
		{
			this.VisitStatements(((Operation)operation).ChildOperations.ToImmutableArray());
			return new NoneOperation(ImmutableArray<IOperation>.Empty, null, operation.Syntax, operation.Type, operation.GetConstantValue(), this.IsImplicit(operation));
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x0005CB48 File Offset: 0x0005AD48
		private IOperation VisitNoneOperationExpression(IOperation operation)
		{
			return this.PopStackFrame<NoneOperation>(this.PushStackFrame(), new NoneOperation(this.VisitArray<IOperation>(((Operation)operation).ChildOperations.ToImmutableArray(), null, null), null, operation.Syntax, operation.Type, operation.GetConstantValue(), this.IsImplicit(operation)));
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x0005CB9C File Offset: 0x0005AD9C
		public override IOperation VisitInterpolatedString(IInterpolatedStringOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			foreach (IInterpolatedStringContentOperation interpolatedStringContentOperation in operation.Parts)
			{
				if (interpolatedStringContentOperation.Kind == OperationKind.Interpolation)
				{
					IInterpolationOperation interpolationOperation = (IInterpolationOperation)interpolatedStringContentOperation;
					this.PushOperand(this.VisitRequired(interpolationOperation.Expression, default(int?)));
					if (interpolationOperation.Alignment != null)
					{
						this.PushOperand(this.VisitRequired(interpolationOperation.Alignment, default(int?)));
					}
				}
			}
			ArrayBuilder<IInterpolatedStringContentOperation> instance = ArrayBuilder<IInterpolatedStringContentOperation>.GetInstance(operation.Parts.Length);
			for (int i = operation.Parts.Length - 1; i >= 0; i--)
			{
				IInterpolatedStringContentOperation interpolatedStringContentOperation2 = operation.Parts[i];
				IInterpolatedStringContentOperation interpolatedStringContentOperation3;
				if (interpolatedStringContentOperation2.Kind == OperationKind.Interpolation)
				{
					IInterpolationOperation interpolationOperation2 = (IInterpolationOperation)interpolatedStringContentOperation2;
					IOperation operation2;
					if (interpolationOperation2.FormatString != null)
					{
						operation2 = this.VisitLiteral((ILiteralOperation)interpolationOperation2.FormatString, default(int?));
					}
					else
					{
						operation2 = null;
					}
					IOperation operation3 = ((interpolationOperation2.Alignment != null) ? this.PopOperand() : null);
					interpolatedStringContentOperation3 = new InterpolationOperation(this.PopOperand(), operation3, operation2, null, interpolatedStringContentOperation2.Syntax, this.IsImplicit(interpolatedStringContentOperation2));
				}
				else
				{
					IInterpolatedStringTextOperation interpolatedStringTextOperation = (IInterpolatedStringTextOperation)interpolatedStringContentOperation2;
					interpolatedStringContentOperation3 = new InterpolatedStringTextOperation(this.VisitLiteral((ILiteralOperation)interpolatedStringTextOperation.Text, default(int?)), null, interpolatedStringContentOperation2.Syntax, this.IsImplicit(interpolatedStringContentOperation2));
				}
				instance.Add(interpolatedStringContentOperation3);
			}
			instance.ReverseContents();
			this.PopStackFrame(evalStackFrame, true);
			return new InterpolatedStringOperation(instance.ToImmutableAndFree(), null, operation.Syntax, operation.Type, operation.GetConstantValue(), this.IsImplicit(operation));
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x0005CD63 File Offset: 0x0005AF63
		public override IOperation VisitInterpolatedStringText(IInterpolatedStringTextOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x0005CD6A File Offset: 0x0005AF6A
		public override IOperation VisitInterpolation(IInterpolationOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x0005CD71 File Offset: 0x0005AF71
		public override IOperation VisitNameOf(INameOfOperation operation, int? captureIdForResult)
		{
			return new LiteralOperation(null, operation.Syntax, operation.Type, operation.GetConstantValue(), this.IsImplicit(operation));
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x0005CD92 File Offset: 0x0005AF92
		public override IOperation VisitLiteral(ILiteralOperation operation, int? captureIdForResult)
		{
			return new LiteralOperation(null, operation.Syntax, operation.Type, operation.GetConstantValue(), this.IsImplicit(operation));
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x0005CDB3 File Offset: 0x0005AFB3
		public override IOperation VisitLocalReference(ILocalReferenceOperation operation, int? captureIdForResult)
		{
			return new LocalReferenceOperation(operation.Local, operation.IsDeclaration, null, operation.Syntax, operation.Type, operation.GetConstantValue(), this.IsImplicit(operation));
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x0005CDE0 File Offset: 0x0005AFE0
		public override IOperation VisitParameterReference(IParameterReferenceOperation operation, int? captureIdForResult)
		{
			return new ParameterReferenceOperation(operation.Parameter, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0005CE04 File Offset: 0x0005B004
		public override IOperation VisitFieldReference(IFieldReferenceOperation operation, int? captureIdForResult)
		{
			IOperation operation2 = (operation.Field.IsStatic ? null : this.Visit(operation.Instance));
			return new FieldReferenceOperation(operation.Field, operation.IsDeclaration, operation2, null, operation.Syntax, operation.Type, operation.GetConstantValue(), this.IsImplicit(operation));
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x0005CE5C File Offset: 0x0005B05C
		public override IOperation VisitMethodReference(IMethodReferenceOperation operation, int? captureIdForResult)
		{
			IOperation operation2 = (operation.Method.IsStatic ? null : this.Visit(operation.Instance));
			return new MethodReferenceOperation(operation.Method, operation.IsVirtual, operation2, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x0005CEAC File Offset: 0x0005B0AC
		public override IOperation VisitPropertyReference(IPropertyReferenceOperation operation, int? captureIdForResult)
		{
			IInstanceReferenceOperation instanceReferenceOperation = operation.Instance as IInstanceReferenceOperation;
			if (instanceReferenceOperation == null || instanceReferenceOperation.ReferenceKind != InstanceReferenceKind.ImplicitReceiver || !operation.Property.ContainingType.IsAnonymousType || operation.Property.ContainingType != this._currentImplicitInstance.AnonymousType)
			{
				ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
				IOperation operation2 = (operation.Property.IsStatic ? null : operation.Instance);
				ValueTuple<IOperation, ImmutableArray<IArgumentOperation>> valueTuple = this.VisitInstanceWithArguments(operation2, operation.Arguments);
				IOperation item = valueTuple.Item1;
				ImmutableArray<IArgumentOperation> item2 = valueTuple.Item2;
				this.PopStackFrame(evalStackFrame, true);
				return new PropertyReferenceOperation(operation.Property, item2, item, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
			}
			IOperation operation3;
			if (!this._currentImplicitInstance.AnonymousTypePropertyValues.TryGetValue(operation.Property, ref operation3))
			{
				return ControlFlowGraphBuilder.MakeInvalidOperation(operation.Syntax, operation.Type, ImmutableArray<IOperation>.Empty);
			}
			IFlowCaptureReferenceOperation flowCaptureReferenceOperation = operation3 as IFlowCaptureReferenceOperation;
			if (flowCaptureReferenceOperation == null)
			{
				return OperationCloner.CloneOperation<IOperation>(operation3);
			}
			return ControlFlowGraphBuilder.GetCaptureReference(flowCaptureReferenceOperation.Id.Value, operation);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x0005CFC8 File Offset: 0x0005B1C8
		public override IOperation VisitEventReference(IEventReferenceOperation operation, int? captureIdForResult)
		{
			IOperation operation2 = (operation.Event.IsStatic ? null : this.Visit(operation.Instance));
			return new EventReferenceOperation(operation.Event, operation2, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0005D012 File Offset: 0x0005B212
		public override IOperation VisitTypeOf(ITypeOfOperation operation, int? captureIdForResult)
		{
			return new TypeOfOperation(operation.TypeOperand, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x0005D034 File Offset: 0x0005B234
		public override IOperation VisitParenthesized(IParenthesizedOperation operation, int? captureIdForResult)
		{
			return new ParenthesizedOperation(this.VisitRequired(operation.Operand, default(int?)), null, operation.Syntax, operation.Type, operation.GetConstantValue(), this.IsImplicit(operation));
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0005D078 File Offset: 0x0005B278
		public override IOperation VisitAwait(IAwaitOperation operation, int? captureIdForResult)
		{
			return new AwaitOperation(this.VisitRequired(operation.Operation, default(int?)), null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x0005D0B3 File Offset: 0x0005B2B3
		public override IOperation VisitSizeOf(ISizeOfOperation operation, int? captureIdForResult)
		{
			return new SizeOfOperation(operation.TypeOperand, null, operation.Syntax, operation.Type, operation.GetConstantValue(), this.IsImplicit(operation));
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x0005D0DA File Offset: 0x0005B2DA
		public override IOperation VisitStop(IStopOperation operation, int? captureIdForResult)
		{
			return new StopOperation(null, operation.Syntax, this.IsImplicit(operation));
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x0005D0F0 File Offset: 0x0005B2F0
		public override IOperation VisitIsType(IIsTypeOperation operation, int? captureIdForResult)
		{
			return new IsTypeOperation(this.VisitRequired(operation.ValueOperand, default(int?)), operation.TypeOperand, operation.IsNegated, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x0005D138 File Offset: 0x0005B338
		[return: Nullable(2)]
		public override IOperation VisitParameterInitializer(IParameterInitializerOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			ParameterReferenceOperation parameterReferenceOperation = new ParameterReferenceOperation(operation.Parameter, null, operation.Syntax, operation.Parameter.Type, true);
			this.VisitInitializer(parameterReferenceOperation, operation);
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x0005D17C File Offset: 0x0005B37C
		[return: Nullable(2)]
		public override IOperation VisitFieldInitializer(IFieldInitializerOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			foreach (IFieldSymbol fieldSymbol in operation.InitializedFields)
			{
				IInstanceReferenceOperation instanceReferenceOperation = (fieldSymbol.IsStatic ? null : new InstanceReferenceOperation(InstanceReferenceKind.ContainingTypeInstance, null, operation.Syntax, fieldSymbol.ContainingType, true));
				FieldReferenceOperation fieldReferenceOperation = new FieldReferenceOperation(fieldSymbol, false, instanceReferenceOperation, null, operation.Syntax, fieldSymbol.Type, null, true);
				this.VisitInitializer(fieldReferenceOperation, operation);
			}
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x0005D1FC File Offset: 0x0005B3FC
		[return: Nullable(2)]
		public override IOperation VisitPropertyInitializer(IPropertyInitializerOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			foreach (IPropertySymbol propertySymbol in operation.InitializedProperties)
			{
				InstanceReferenceOperation instanceReferenceOperation = (propertySymbol.IsStatic ? null : new InstanceReferenceOperation(InstanceReferenceKind.ContainingTypeInstance, null, operation.Syntax, propertySymbol.ContainingType, true));
				ImmutableArray<IArgumentOperation> immutableArray;
				if (!propertySymbol.Parameters.IsEmpty)
				{
					ArrayBuilder<IArgumentOperation> instance = ArrayBuilder<IArgumentOperation>.GetInstance(propertySymbol.Parameters.Length);
					foreach (IParameterSymbol parameterSymbol in propertySymbol.Parameters)
					{
						InvalidOperation invalidOperation = new InvalidOperation(ImmutableArray<IOperation>.Empty, null, operation.Syntax, parameterSymbol.Type, null, true);
						ArgumentOperation argumentOperation = new ArgumentOperation(ArgumentKind.Explicit, parameterSymbol, invalidOperation, OperationFactory.IdentityConversion, OperationFactory.IdentityConversion, null, operation.Syntax, true);
						instance.Add(argumentOperation);
					}
					immutableArray = instance.ToImmutableAndFree();
				}
				else
				{
					immutableArray = ImmutableArray<IArgumentOperation>.Empty;
				}
				IOperation operation2 = new PropertyReferenceOperation(propertySymbol, immutableArray, instanceReferenceOperation, null, operation.Syntax, propertySymbol.Type, true);
				this.VisitInitializer(operation2, operation);
			}
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x0005D328 File Offset: 0x0005B528
		private void VisitInitializer(IOperation rewrittenTarget, ISymbolInitializerOperation initializer)
		{
			this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, initializer.Locals, false), false);
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			SimpleAssignmentOperation simpleAssignmentOperation = new SimpleAssignmentOperation(false, rewrittenTarget, this.VisitRequired(initializer.Value, default(int?)), null, initializer.Syntax, rewrittenTarget.Type, null, true);
			this.AddStatement(simpleAssignmentOperation);
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
			this.LeaveRegion();
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x0005D394 File Offset: 0x0005B594
		public override IOperation VisitEventAssignment(IEventAssignmentOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.<>c__DisplayClass210_0 CS$<>8__locals1;
			CS$<>8__locals1.operation = operation;
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			IEventReferenceOperation eventReferenceOperation = ControlFlowGraphBuilder.<VisitEventAssignment>g__getEventReference|210_0(ref CS$<>8__locals1);
			IOperation operation3;
			IOperation operation5;
			if (eventReferenceOperation != null)
			{
				IOperation operation2 = (eventReferenceOperation.Event.IsStatic ? null : eventReferenceOperation.Instance);
				if (operation2 != null)
				{
					this.PushOperand(this.VisitRequired(operation2, default(int?)));
				}
				operation3 = this.VisitRequired(CS$<>8__locals1.operation.HandlerValue, default(int?));
				IOperation operation4 = ((operation2 == null) ? null : this.PopOperand());
				operation5 = new EventReferenceOperation(eventReferenceOperation.Event, operation4, null, CS$<>8__locals1.operation.EventReference.Syntax, CS$<>8__locals1.operation.EventReference.Type, this.IsImplicit(CS$<>8__locals1.operation.EventReference));
			}
			else
			{
				this.PushOperand(this.VisitRequired(CS$<>8__locals1.operation.EventReference, default(int?)));
				operation3 = this.VisitRequired(CS$<>8__locals1.operation.HandlerValue, default(int?));
				operation5 = this.PopOperand();
			}
			this.PopStackFrame(evalStackFrame, true);
			return new EventAssignmentOperation(operation5, operation3, CS$<>8__locals1.operation.Adds, null, CS$<>8__locals1.operation.Syntax, CS$<>8__locals1.operation.Type, this.IsImplicit(CS$<>8__locals1.operation));
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0005D4E8 File Offset: 0x0005B6E8
		public override IOperation VisitRaiseEvent(IRaiseEventOperation operation, int? captureIdForResult)
		{
			this.StartVisitingStatement(operation);
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			IOperation operation2 = (operation.EventReference.Event.IsStatic ? null : operation.EventReference.Instance);
			if (operation2 != null)
			{
				this.PushOperand(this.VisitRequired(operation2, default(int?)));
			}
			ImmutableArray<IArgumentOperation> immutableArray = this.VisitArguments(operation.Arguments);
			IOperation operation3 = ((operation2 == null) ? null : this.PopOperand());
			EventReferenceOperation eventReferenceOperation = new EventReferenceOperation(operation.EventReference.Event, operation3, null, operation.EventReference.Syntax, operation.EventReference.Type, this.IsImplicit(operation.EventReference));
			this.PopStackFrame(evalStackFrame, true);
			return this.FinishVisitingStatement(operation, new RaiseEventOperation(eventReferenceOperation, immutableArray, null, operation.Syntax, this.IsImplicit(operation)));
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x0005D5B4 File Offset: 0x0005B7B4
		public override IOperation VisitAddressOf(IAddressOfOperation operation, int? captureIdForResult)
		{
			return new AddressOfOperation(this.VisitRequired(operation.Reference, default(int?)), null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0005D5F0 File Offset: 0x0005B7F0
		public override IOperation VisitIncrementOrDecrement(IIncrementOrDecrementOperation operation, int? captureIdForResult)
		{
			return new IncrementOrDecrementOperation(operation.IsPostfix, operation.IsLifted, operation.IsChecked, this.VisitRequired(operation.Target, default(int?)), operation.OperatorMethod, operation.Kind, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x0005D649 File Offset: 0x0005B849
		public override IOperation VisitDiscardOperation(IDiscardOperation operation, int? captureIdForResult)
		{
			return new DiscardOperation(operation.DiscardSymbol, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0005D66A File Offset: 0x0005B86A
		public override IOperation VisitDiscardPattern(IDiscardPatternOperation pat, int? captureIdForResult)
		{
			return new DiscardPatternOperation(pat.InputType, pat.NarrowedType, null, pat.Syntax, this.IsImplicit(pat));
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0005D68B File Offset: 0x0005B88B
		public override IOperation VisitOmittedArgument(IOmittedArgumentOperation operation, int? captureIdForResult)
		{
			return new OmittedArgumentOperation(null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x0005D6A8 File Offset: 0x0005B8A8
		internal override IOperation VisitPlaceholder(IPlaceholderOperation operation, int? captureIdForResult)
		{
			switch (operation.PlaceholderKind)
			{
			case PlaceholderKind.SwitchOperationExpression:
				if (this._currentSwitchOperationExpression != null)
				{
					return OperationCloner.CloneOperation<IOperation>(this._currentSwitchOperationExpression);
				}
				break;
			case PlaceholderKind.ForToLoopBinaryOperatorLeftOperand:
				if (this._forToLoopBinaryOperatorLeftOperand != null)
				{
					return this._forToLoopBinaryOperatorLeftOperand;
				}
				break;
			case PlaceholderKind.ForToLoopBinaryOperatorRightOperand:
				if (this._forToLoopBinaryOperatorRightOperand != null)
				{
					return this._forToLoopBinaryOperatorRightOperand;
				}
				break;
			case PlaceholderKind.AggregationGroup:
				if (this._currentAggregationGroup != null)
				{
					return OperationCloner.CloneOperation<IOperation>(this._currentAggregationGroup);
				}
				break;
			}
			return new PlaceholderOperation(operation.PlaceholderKind, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x0005D73C File Offset: 0x0005B93C
		public override IOperation VisitConversion(IConversionOperation operation, int? captureIdForResult)
		{
			return new ConversionOperation(this.VisitRequired(operation.Operand, default(int?)), ((ConversionOperation)operation).ConversionConvertible, operation.IsTryCast, operation.IsChecked, null, operation.Syntax, operation.Type, operation.GetConstantValue(), this.IsImplicit(operation));
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0005D794 File Offset: 0x0005B994
		public override IOperation VisitDefaultValue(IDefaultValueOperation operation, int? captureIdForResult)
		{
			return new DefaultValueOperation(null, operation.Syntax, operation.Type, operation.GetConstantValue(), this.IsImplicit(operation));
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x0005D7B8 File Offset: 0x0005B9B8
		public override IOperation VisitIsPattern(IIsPatternOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.PushOperand(this.VisitRequired(operation.Value, default(int?)));
			IPatternOperation patternOperation = (IPatternOperation)this.VisitRequired(operation.Pattern, default(int?));
			IOperation operation2 = this.PopOperand();
			this.PopStackFrame(evalStackFrame, true);
			return new IsPatternOperation(operation2, patternOperation, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x0005D82C File Offset: 0x0005BA2C
		public override IOperation VisitInvalid(IInvalidOperation operation, int? captureIdForResult)
		{
			ControlFlowGraphBuilder.<>c__DisplayClass221_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.operation = operation;
			CS$<>8__locals1.children = ArrayBuilder<IOperation>.GetInstance();
			CS$<>8__locals1.children.AddRange(((InvalidOperation)CS$<>8__locals1.operation).Children);
			if (CS$<>8__locals1.children.Count != 0 && CS$<>8__locals1.children.Last().Kind == OperationKind.ObjectOrCollectionInitializer)
			{
				this.SpillEvalStack();
				ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
				IObjectOrCollectionInitializerOperation objectOrCollectionInitializerOperation = (IObjectOrCollectionInitializerOperation)CS$<>8__locals1.children.Last();
				CS$<>8__locals1.children.RemoveLast();
				ControlFlowGraphBuilder.EvalStackFrame evalStackFrame2 = this.PushStackFrame();
				foreach (IOperation operation2 in CS$<>8__locals1.children)
				{
					this.PushOperand(this.VisitRequired(operation2, default(int?)));
				}
				for (int i = CS$<>8__locals1.children.Count - 1; i >= 0; i--)
				{
					CS$<>8__locals1.children[i] = this.PopOperand();
				}
				this.PopStackFrame(evalStackFrame2, true);
				IOperation operation3 = new InvalidOperation(CS$<>8__locals1.children.ToImmutableAndFree(), null, CS$<>8__locals1.operation.Syntax, CS$<>8__locals1.operation.Type, CS$<>8__locals1.operation.GetConstantValue(), this.IsImplicit(CS$<>8__locals1.operation));
				operation3 = this.HandleObjectOrCollectionInitializer(objectOrCollectionInitializerOperation, operation3);
				this.PopStackFrame(evalStackFrame, true);
				return operation3;
			}
			IOperation operation4;
			if (this._currentStatement == CS$<>8__locals1.operation)
			{
				operation4 = this.<VisitInvalid>g__visitInvalidOperationStatement|221_0(CS$<>8__locals1.operation, ref CS$<>8__locals1);
			}
			else
			{
				operation4 = this.<VisitInvalid>g__visitInvalidOperationExpression|221_1(CS$<>8__locals1.operation, ref CS$<>8__locals1);
			}
			return operation4;
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x0005D9C0 File Offset: 0x0005BBC0
		[return: Nullable(2)]
		public override IOperation VisitReDim(IReDimOperation operation, int? argument)
		{
			this.StartVisitingStatement(operation);
			bool flag = operation.Clauses.Length > 1 || this.IsImplicit(operation);
			foreach (IReDimClauseOperation reDimClauseOperation in operation.Clauses)
			{
				ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
				ReDimOperation reDimOperation = new ReDimOperation(ImmutableArray.Create<IReDimClauseOperation>(this.<VisitReDim>g__visitReDimClause|222_0(reDimClauseOperation)), operation.Preserve, null, operation.Syntax, flag);
				this.AddStatement(reDimOperation);
				this.PopStackFrameAndLeaveRegion(evalStackFrame);
			}
			return this.FinishVisitingStatement(operation, null);
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x0005DA53 File Offset: 0x0005BC53
		public override IOperation VisitReDimClause(IReDimClauseOperation operation, int? argument)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x0005DA5C File Offset: 0x0005BC5C
		public override IOperation VisitTranslatedQuery(ITranslatedQueryOperation operation, int? captureIdForResult)
		{
			return new TranslatedQueryOperation(this.VisitRequired(operation.Operation, default(int?)), null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0005DA98 File Offset: 0x0005BC98
		public override IOperation VisitConstantPattern(IConstantPatternOperation operation, int? captureIdForResult)
		{
			return new ConstantPatternOperation(this.VisitRequired(operation.Value, default(int?)), operation.InputType, operation.NarrowedType, null, operation.Syntax, this.IsImplicit(operation));
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0005DADC File Offset: 0x0005BCDC
		public override IOperation VisitRelationalPattern(IRelationalPatternOperation operation, int? argument)
		{
			return new RelationalPatternOperation(operation.OperatorKind, this.VisitRequired(operation.Value, default(int?)), operation.InputType, operation.NarrowedType, null, operation.Syntax, this.IsImplicit(operation));
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x0005DB24 File Offset: 0x0005BD24
		public override IOperation VisitBinaryPattern(IBinaryPatternOperation operation, int? argument)
		{
			return new BinaryPatternOperation(operation.OperatorKind, (IPatternOperation)this.VisitRequired(operation.LeftPattern, default(int?)), (IPatternOperation)this.VisitRequired(operation.RightPattern, default(int?)), operation.InputType, operation.NarrowedType, null, operation.Syntax, this.IsImplicit(operation));
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0005DB8C File Offset: 0x0005BD8C
		public override IOperation VisitNegatedPattern(INegatedPatternOperation operation, int? argument)
		{
			return new NegatedPatternOperation((IPatternOperation)this.VisitRequired(operation.Pattern, default(int?)), operation.InputType, operation.NarrowedType, null, operation.Syntax, this.IsImplicit(operation));
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x0005DBD2 File Offset: 0x0005BDD2
		public override IOperation VisitTypePattern(ITypePatternOperation operation, int? argument)
		{
			return new TypePatternOperation(operation.MatchedType, operation.InputType, operation.NarrowedType, null, operation.Syntax, this.IsImplicit(operation));
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x0005DBFC File Offset: 0x0005BDFC
		public override IOperation VisitDeclarationPattern(IDeclarationPatternOperation operation, int? captureIdForResult)
		{
			return new DeclarationPatternOperation(operation.MatchedType, operation.MatchesNull, operation.DeclaredSymbol, operation.InputType, operation.NarrowedType, null, operation.Syntax, this.IsImplicit(operation));
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x0005DC3C File Offset: 0x0005BE3C
		public override IOperation VisitRecursivePattern(IRecursivePatternOperation operation, int? argument)
		{
			return new RecursivePatternOperation(operation.MatchedType, operation.DeconstructSymbol, operation.DeconstructionSubpatterns.SelectAsArray((IPatternOperation p, ControlFlowGraphBuilder @this) => (IPatternOperation)@this.VisitRequired(p, default(int?)), this), operation.PropertySubpatterns.SelectAsArray((IPropertySubpatternOperation p, ControlFlowGraphBuilder @this) => (IPropertySubpatternOperation)@this.VisitRequired(p, default(int?)), this), operation.DeclaredSymbol, operation.InputType, operation.NarrowedType, null, operation.Syntax, this.IsImplicit(operation));
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x0005DCD0 File Offset: 0x0005BED0
		public override IOperation VisitPropertySubpattern(IPropertySubpatternOperation operation, int? argument)
		{
			return new PropertySubpatternOperation(this.VisitRequired(operation.Member, default(int?)), (IPatternOperation)this.VisitRequired(operation.Pattern, default(int?)), null, operation.Syntax, this.IsImplicit(operation));
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x0005DD20 File Offset: 0x0005BF20
		public override IOperation VisitDelegateCreation(IDelegateCreationOperation operation, int? captureIdForResult)
		{
			return new DelegateCreationOperation(this.VisitRequired(operation.Target, default(int?)), null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x0005DD5C File Offset: 0x0005BF5C
		public override IOperation VisitRangeOperation(IRangeOperation operation, int? argument)
		{
			if (operation.LeftOperand != null)
			{
				this.PushOperand(this.VisitRequired(operation.LeftOperand, default(int?)));
			}
			IOperation operation2 = null;
			if (operation.RightOperand != null)
			{
				operation2 = this.Visit(operation.RightOperand);
			}
			return new RangeOperation((operation.LeftOperand == null) ? null : this.PopOperand(), operation2, operation.IsLifted, operation.Method, null, operation.Syntax, operation.Type, this.IsImplicit(operation));
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x0005DDDC File Offset: 0x0005BFDC
		public override IOperation VisitSwitchExpression(ISwitchExpressionOperation operation, int? captureIdForResult)
		{
			INamedTypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
			this.SpillEvalStack();
			ControlFlowGraphBuilder.RegionBuilder currentRegionRequired = this.CurrentRegionRequired;
			int num = captureIdForResult ?? this.GetNextCaptureId(currentRegionRequired);
			IOperation operation2 = this.VisitAndCapture(operation.Value);
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			foreach (ISwitchExpressionArmOperation switchExpressionArmOperation in operation.Arms)
			{
				ControlFlowGraphBuilder.RegionBuilder regionBuilder = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, switchExpressionArmOperation.Locals, false);
				this.EnterRegion(regionBuilder, false);
				ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
				ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
				IPatternOperation patternOperation = (IPatternOperation)this.VisitRequired(switchExpressionArmOperation.Pattern, default(int?));
				IsPatternOperation isPatternOperation = new IsPatternOperation(OperationCloner.CloneOperation<IOperation>(operation2), patternOperation, null, switchExpressionArmOperation.Syntax, specialType, this.IsImplicit(switchExpressionArmOperation));
				this.ConditionalBranch(isPatternOperation, false, basicBlockBuilder2);
				this._currentBasicBlock = null;
				this.PopStackFrameAndLeaveRegion(evalStackFrame);
				if (switchExpressionArmOperation.Guard != null)
				{
					ControlFlowGraphBuilder.EvalStackFrame evalStackFrame2 = this.PushStackFrame();
					this.VisitConditionalBranch(switchExpressionArmOperation.Guard, ref basicBlockBuilder2, false);
					this._currentBasicBlock = null;
					this.PopStackFrameAndLeaveRegion(evalStackFrame2);
				}
				this.VisitAndCapture(switchExpressionArmOperation.Value, num);
				this.UnconditionalBranch(basicBlockBuilder);
				this.AppendNewBlock(basicBlockBuilder2, true);
				this.LeaveRegion();
			}
			this.LeaveRegionsUpTo(currentRegionRequired);
			ISymbolInternal symbolInternal = this._compilation.CommonGetWellKnownTypeMember(WellKnownMember.System_Runtime_CompilerServices_SwitchExpressionException__ctor) ?? this._compilation.CommonGetWellKnownTypeMember(WellKnownMember.System_InvalidOperationException__ctor);
			IMethodSymbol methodSymbol = (IMethodSymbol)((symbolInternal != null) ? symbolInternal.GetISymbol() : null);
			IOperation operation4;
			if (methodSymbol != null)
			{
				IOperation operation3 = new ObjectCreationOperation(methodSymbol, null, ImmutableArray<IArgumentOperation>.Empty, null, operation.Syntax, methodSymbol.ContainingType, null, true);
				operation4 = operation3;
			}
			else
			{
				operation4 = ControlFlowGraphBuilder.MakeInvalidOperation(operation.Syntax, this._compilation.GetSpecialType(SpecialType.System_Object), ImmutableArray<IOperation>.Empty);
			}
			IOperation operation5 = operation4;
			this.LinkThrowStatement(operation5);
			this._currentBasicBlock = null;
			this.AppendNewBlock(basicBlockBuilder, false);
			return ControlFlowGraphBuilder.GetCaptureReference(num, operation);
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x0005DFDC File Offset: 0x0005C1DC
		private unsafe void VisitUsingVariableDeclarationOperation(IUsingDeclarationOperation operation, [Nullable(new byte[] { 0, 1 })] ReadOnlySpan<IOperation> statements)
		{
			IOperation currentStatement = this._currentStatement;
			this._currentStatement = operation;
			this.StartVisitingStatement(operation);
			ArrayBuilder<IOperation> instance = ArrayBuilder<IOperation>.GetInstance(statements.Length);
			ArrayBuilder<IOperation> arrayBuilder = null;
			ReadOnlySpan<IOperation> readOnlySpan = statements;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				IOperation operation2 = *readOnlySpan[i];
				if (operation2.Kind == OperationKind.LocalFunction)
				{
					ArrayBuilder<IOperation> arrayBuilder2;
					if ((arrayBuilder2 = arrayBuilder) == null)
					{
						arrayBuilder2 = (arrayBuilder = ArrayBuilder<IOperation>.GetInstance());
					}
					arrayBuilder2.Add(operation2);
				}
				else
				{
					instance.Add(operation2);
				}
			}
			BlockOperation blockOperation = BlockOperation.CreateTemporaryBlock(instance.ToImmutableAndFree(), ((Operation)operation).OwningSemanticModel, operation.Syntax);
			DisposeOperationInfo disposeInfo = ((UsingDeclarationOperation)operation).DisposeInfo;
			this.HandleUsingOperationParts(operation.DeclarationGroup, blockOperation, disposeInfo.DisposeMethod, disposeInfo.DisposeArguments, ImmutableArray<ILocalSymbol>.Empty, operation.IsAsynchronous);
			this.FinishVisitingStatement(operation, null);
			this._currentStatement = currentStatement;
			if (arrayBuilder != null)
			{
				this.VisitStatements(arrayBuilder.ToImmutableAndFree());
			}
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x0005E0CC File Offset: 0x0005C2CC
		[NullableContext(2)]
		public IOperation Visit(IOperation operation)
		{
			return this.Visit(operation, default(int?));
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x0005E0E9 File Offset: 0x0005C2E9
		[NullableContext(2)]
		[MethodImpl(256)]
		[return: NotNullIfNotNull("operation")]
		public IOperation VisitRequired(IOperation operation, int? argument = null)
		{
			return this.Visit(operation, argument);
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x0005E0F3 File Offset: 0x0005C2F3
		[NullableContext(2)]
		[MethodImpl(256)]
		[return: NotNullIfNotNull("operation")]
		public IOperation BaseVisitRequired(IOperation operation, int? argument)
		{
			return base.Visit(operation, argument);
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x0005E0FD File Offset: 0x0005C2FD
		[NullableContext(2)]
		public override IOperation Visit(IOperation operation, int? argument)
		{
			if (operation == null)
			{
				return null;
			}
			return this.PopStackFrame<IOperation>(this.PushStackFrame(), base.Visit(operation, argument));
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x0005E118 File Offset: 0x0005C318
		public override IOperation DefaultVisit(IOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x0005E11F File Offset: 0x0005C31F
		public override IOperation VisitArgument(IArgumentOperation operation, int? captureIdForResult)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x0005E126 File Offset: 0x0005C326
		public override IOperation VisitUsingDeclaration(IUsingDeclarationOperation operation, int? argument)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x0005E130 File Offset: 0x0005C330
		public override IOperation VisitWith(IWithOperation operation, int? argument)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			IOperation operation2 = this.VisitRequired(operation.Operand, default(int?));
			IOperation operation4;
			if (operation.CloneMethod != null)
			{
				IOperation operation3 = new InvocationOperation(operation.CloneMethod, operation2, true, ImmutableArray<IArgumentOperation>.Empty, null, operation.Syntax, operation.Type, true);
				operation4 = operation3;
			}
			else
			{
				operation4 = ControlFlowGraphBuilder.MakeInvalidOperation(operation2.Type, operation2);
			}
			IOperation operation5 = operation4;
			return this.PopStackFrame<IOperation>(evalStackFrame, this.HandleObjectOrCollectionInitializer(operation.Initializer, operation5));
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x0005E1A9 File Offset: 0x0005C3A9
		[CompilerGenerated]
		[return: Nullable(2)]
		internal static ControlFlowBranch <ToImmutableBlocks>g__getFallThroughSuccessor|31_0(ControlFlowGraphBuilder.BasicBlockBuilder blockBuilder, ref ControlFlowGraphBuilder.<>c__DisplayClass31_0 A_1)
		{
			if (blockBuilder.Kind == BasicBlockKind.Exit)
			{
				return null;
			}
			return ControlFlowGraphBuilder.<ToImmutableBlocks>g__getBranch|31_2(in blockBuilder.FallThrough, blockBuilder, false, ref A_1);
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x0005E1C4 File Offset: 0x0005C3C4
		[CompilerGenerated]
		[return: Nullable(2)]
		internal static ControlFlowBranch <ToImmutableBlocks>g__getConditionalSuccessor|31_1(ControlFlowGraphBuilder.BasicBlockBuilder blockBuilder, ref ControlFlowGraphBuilder.<>c__DisplayClass31_0 A_1)
		{
			if (!blockBuilder.HasCondition)
			{
				return null;
			}
			return ControlFlowGraphBuilder.<ToImmutableBlocks>g__getBranch|31_2(in blockBuilder.Conditional, blockBuilder, true, ref A_1);
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x0005E1DE File Offset: 0x0005C3DE
		[CompilerGenerated]
		internal static ControlFlowBranch <ToImmutableBlocks>g__getBranch|31_2(in ControlFlowGraphBuilder.BasicBlockBuilder.Branch branch, ControlFlowGraphBuilder.BasicBlockBuilder source, bool isConditionalSuccessor, ref ControlFlowGraphBuilder.<>c__DisplayClass31_0 A_3)
		{
			return new ControlFlowBranch(A_3.builder[source.Ordinal], (branch.Destination != null) ? A_3.builder[branch.Destination.Ordinal] : null, branch.Kind, isConditionalSuccessor);
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x0005E220 File Offset: 0x0005C420
		[CompilerGenerated]
		internal static void <MarkReachableBlocks>g__followBranch|33_0(ControlFlowGraphBuilder.BasicBlockBuilder current, in ControlFlowGraphBuilder.BasicBlockBuilder.Branch branch, ref ControlFlowGraphBuilder.<>c__DisplayClass33_0 A_2)
		{
			switch (branch.Kind)
			{
			case ControlFlowBranchSemantics.None:
			case ControlFlowBranchSemantics.StructuredExceptionHandling:
			case ControlFlowBranchSemantics.ProgramTermination:
			case ControlFlowBranchSemantics.Throw:
			case ControlFlowBranchSemantics.Rethrow:
			case ControlFlowBranchSemantics.Error:
				return;
			case ControlFlowBranchSemantics.Regular:
			case ControlFlowBranchSemantics.Return:
				if (ControlFlowGraphBuilder.<MarkReachableBlocks>g__stepThroughFinally|33_1(current.Region, branch.Destination, ref A_2))
				{
					A_2.toVisit.Add(branch.Destination);
				}
				return;
			default:
				throw ExceptionUtilities.UnexpectedValue(branch.Kind);
			}
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x0005E294 File Offset: 0x0005C494
		[CompilerGenerated]
		internal static bool <MarkReachableBlocks>g__stepThroughFinally|33_1(ControlFlowRegion region, ControlFlowGraphBuilder.BasicBlockBuilder destination, ref ControlFlowGraphBuilder.<>c__DisplayClass33_0 A_2)
		{
			int ordinal = destination.Ordinal;
			while (!region.ContainsBlock(ordinal))
			{
				ControlFlowRegion enclosingRegion = region.EnclosingRegion;
				if (region.Kind == ControlFlowRegionKind.Try && enclosingRegion.Kind == ControlFlowRegionKind.TryAndFinally && !ControlFlowGraphBuilder.<MarkReachableBlocks>g__stepThroughSingleFinally|33_2(enclosingRegion.NestedRegions[1], ref A_2))
				{
					return false;
				}
				region = enclosingRegion;
			}
			return true;
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x0005E2EC File Offset: 0x0005C4EC
		[CompilerGenerated]
		internal static bool <MarkReachableBlocks>g__stepThroughSingleFinally|33_2(ControlFlowRegion @finally, ref ControlFlowGraphBuilder.<>c__DisplayClass33_0 A_1)
		{
			bool flag;
			if (!A_1.continueDispatchAfterFinally.TryGetValue(@finally, ref flag))
			{
				bool flag2;
				BitVector bitVector = ControlFlowGraphBuilder.MarkReachableBlocks(A_1.blocks, @finally.FirstBlockOrdinal, @finally.LastBlockOrdinal, A_1.toVisit, A_1.continueDispatchAfterFinally, A_1.dispatchedExceptionsFromRegions, out flag2);
				A_1.visited.UnionWith(in bitVector);
				flag = flag2 && A_1.blocks[@finally.LastBlockOrdinal].FallThrough.Kind == ControlFlowBranchSemantics.StructuredExceptionHandling;
				A_1.continueDispatchAfterFinally.Add(@finally, flag);
			}
			return flag;
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x0005E378 File Offset: 0x0005C578
		[NullableContext(2)]
		[CompilerGenerated]
		internal static void <MarkReachableBlocks>g__dispatchException|33_3([DisallowNull] ControlFlowRegion fromRegion, ref ControlFlowGraphBuilder.<>c__DisplayClass33_0 A_1)
		{
			while (A_1.dispatchedExceptionsFromRegions.Add(fromRegion))
			{
				ControlFlowRegion controlFlowRegion = ((fromRegion.Kind == ControlFlowRegionKind.Root) ? null : fromRegion.EnclosingRegion);
				if (fromRegion.Kind == ControlFlowRegionKind.Try)
				{
					ControlFlowRegionKind kind = controlFlowRegion.Kind;
					if (kind == ControlFlowRegionKind.TryAndCatch)
					{
						ControlFlowGraphBuilder.<MarkReachableBlocks>g__dispatchExceptionThroughCatches|33_4(controlFlowRegion, 1, ref A_1);
						goto IL_00A9;
					}
					if (kind != ControlFlowRegionKind.TryAndFinally)
					{
						throw ExceptionUtilities.UnexpectedValue(controlFlowRegion.Kind);
					}
					if (!ControlFlowGraphBuilder.<MarkReachableBlocks>g__stepThroughSingleFinally|33_2(controlFlowRegion.NestedRegions[1], ref A_1))
					{
						return;
					}
					goto IL_00A9;
				}
				else
				{
					if (fromRegion.Kind != ControlFlowRegionKind.Filter)
					{
						goto IL_00A9;
					}
					ControlFlowRegion enclosingRegion = controlFlowRegion.EnclosingRegion;
					int num = enclosingRegion.NestedRegions.IndexOf(controlFlowRegion, 1);
					if (num <= 0)
					{
						throw ExceptionUtilities.Unreachable;
					}
					ControlFlowGraphBuilder.<MarkReachableBlocks>g__dispatchExceptionThroughCatches|33_4(enclosingRegion, num + 1, ref A_1);
					fromRegion = enclosingRegion;
				}
				IL_00AC:
				if (fromRegion == null)
				{
					return;
				}
				continue;
				IL_00A9:
				fromRegion = controlFlowRegion;
				goto IL_00AC;
			}
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x0005E438 File Offset: 0x0005C638
		[CompilerGenerated]
		internal static void <MarkReachableBlocks>g__dispatchExceptionThroughCatches|33_4(ControlFlowRegion tryAndCatch, int startAt, ref ControlFlowGraphBuilder.<>c__DisplayClass33_0 A_2)
		{
			for (int i = startAt; i < tryAndCatch.NestedRegions.Length; i++)
			{
				ControlFlowRegion controlFlowRegion = tryAndCatch.NestedRegions[i];
				ControlFlowRegionKind kind = controlFlowRegion.Kind;
				if (kind != ControlFlowRegionKind.Catch)
				{
					if (kind != ControlFlowRegionKind.FilterAndHandler)
					{
						throw ExceptionUtilities.UnexpectedValue(controlFlowRegion.Kind);
					}
					ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = A_2.blocks[controlFlowRegion.FirstBlockOrdinal];
					A_2.toVisit.Add(basicBlockBuilder);
				}
				else
				{
					A_2.toVisit.Add(A_2.blocks[controlFlowRegion.FirstBlockOrdinal]);
				}
			}
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x0005E4D4 File Offset: 0x0005C6D4
		[CompilerGenerated]
		internal static bool <PackRegions>g__PackRegion|35_0(ControlFlowGraphBuilder.RegionBuilder region, ref ControlFlowGraphBuilder.<>c__DisplayClass35_0 A_1)
		{
			bool flag = false;
			if (region.HasRegions)
			{
				for (int i = region.Regions.Count - 1; i >= 0; i--)
				{
					ControlFlowGraphBuilder.RegionBuilder regionBuilder = region.Regions[i];
					if (ControlFlowGraphBuilder.<PackRegions>g__PackRegion|35_0(regionBuilder, ref A_1))
					{
						flag = true;
					}
					if (regionBuilder.Kind == ControlFlowRegionKind.LocalLifetime && regionBuilder.Locals.IsEmpty && !regionBuilder.HasLocalFunctions && !regionBuilder.HasCaptureIds)
					{
						ControlFlowGraphBuilder.MergeSubRegionAndFree(regionBuilder, A_1.blocks, A_1.regionMap, false);
						flag = true;
					}
				}
			}
			switch (region.Kind)
			{
			case ControlFlowRegionKind.Root:
			case ControlFlowRegionKind.LocalLifetime:
			case ControlFlowRegionKind.Try:
			case ControlFlowRegionKind.Filter:
			case ControlFlowRegionKind.Catch:
			case ControlFlowRegionKind.Finally:
			case ControlFlowRegionKind.StaticLocalInitializer:
			case ControlFlowRegionKind.ErroneousBody:
			{
				ArrayBuilder<ControlFlowGraphBuilder.RegionBuilder> regions = region.Regions;
				if (regions != null && regions.Count == 1)
				{
					ControlFlowGraphBuilder.RegionBuilder regionBuilder2 = region.Regions[0];
					if (regionBuilder2.Kind == ControlFlowRegionKind.LocalLifetime && regionBuilder2.FirstBlock == region.FirstBlock && regionBuilder2.LastBlock == region.LastBlock)
					{
						region.Locals = region.Locals.Concat<ILocalSymbol>(regionBuilder2.Locals);
						region.AddRange(regionBuilder2.LocalFunctions);
						region.AddCaptureIds(regionBuilder2.CaptureIds);
						ControlFlowGraphBuilder.MergeSubRegionAndFree(regionBuilder2, A_1.blocks, A_1.regionMap, false);
						flag = true;
						break;
					}
				}
				if (region.HasRegions)
				{
					for (int j = region.Regions.Count - 1; j >= 0; j--)
					{
						ControlFlowGraphBuilder.RegionBuilder regionBuilder3 = region.Regions[j];
						if (regionBuilder3.Kind == ControlFlowRegionKind.LocalLifetime && !regionBuilder3.HasLocalFunctions && !regionBuilder3.HasRegions && regionBuilder3.FirstBlock == regionBuilder3.LastBlock)
						{
							ControlFlowGraphBuilder.BasicBlockBuilder firstBlock = regionBuilder3.FirstBlock;
							if (!firstBlock.HasStatements && firstBlock.BranchValue == null)
							{
								A_1.regionMap[firstBlock] = region;
								regionBuilder3.Free();
								region.Regions.RemoveAt(j);
								flag = true;
							}
						}
					}
				}
				break;
			}
			case ControlFlowRegionKind.FilterAndHandler:
			case ControlFlowRegionKind.TryAndCatch:
			case ControlFlowRegionKind.TryAndFinally:
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(region.Kind);
			}
			return flag;
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x0005E6EC File Offset: 0x0005C8EC
		[NullableContext(2)]
		[CompilerGenerated]
		internal static ControlFlowGraphBuilder.RegionBuilder <PackBlocks>g__tryGetImplicitEntryRegion|37_0([Nullable(1)] ControlFlowGraphBuilder.BasicBlockBuilder block, [DisallowNull] ControlFlowGraphBuilder.RegionBuilder currentRegion)
		{
			while (currentRegion.FirstBlock == block)
			{
				ControlFlowRegionKind kind = currentRegion.Kind;
				if (kind - ControlFlowRegionKind.Filter <= 1 || kind == ControlFlowRegionKind.Finally)
				{
					return currentRegion;
				}
				currentRegion = currentRegion.Enclosing;
				if (currentRegion == null)
				{
					return null;
				}
			}
			return null;
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x0005E724 File Offset: 0x0005C924
		[CompilerGenerated]
		internal static void <PackBlocks>g__removeBlock|37_1(ControlFlowGraphBuilder.BasicBlockBuilder block, ControlFlowGraphBuilder.RegionBuilder region, ref ControlFlowGraphBuilder.<>c__DisplayClass37_0 A_2)
		{
			if (region.FirstBlock == block)
			{
				ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = A_2.blocks[block.Ordinal + 1];
				region.FirstBlock = basicBlockBuilder;
				for (ControlFlowGraphBuilder.RegionBuilder regionBuilder = region.Enclosing; regionBuilder != null; regionBuilder = regionBuilder.Enclosing)
				{
					if (regionBuilder.FirstBlock != block)
					{
						break;
					}
					regionBuilder.FirstBlock = basicBlockBuilder;
				}
			}
			else if (region.LastBlock == block)
			{
				ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 = A_2.blocks[block.Ordinal - 1];
				region.LastBlock = basicBlockBuilder2;
				ControlFlowGraphBuilder.RegionBuilder regionBuilder2 = region.Enclosing;
				while (regionBuilder2 != null && regionBuilder2.LastBlock == block)
				{
					regionBuilder2.LastBlock = basicBlockBuilder2;
					regionBuilder2 = regionBuilder2.Enclosing;
				}
			}
			A_2.regionMap.Remove(block);
			A_2.blocks.RemoveAt(block.Ordinal);
			block.Free();
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x0005E7E5 File Offset: 0x0005C9E5
		[CompilerGenerated]
		internal static bool <PackBlocks>g__tryMergeBranch|37_2(ControlFlowGraphBuilder.BasicBlockBuilder predecessor, ref ControlFlowGraphBuilder.BasicBlockBuilder.Branch predecessorBranch, ControlFlowGraphBuilder.BasicBlockBuilder successor)
		{
			if (predecessorBranch.Destination == successor)
			{
				ControlFlowGraphBuilder.<PackBlocks>g__mergeBranch|37_3(predecessor, ref predecessorBranch, ref successor.FallThrough);
				return true;
			}
			return false;
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x0005E800 File Offset: 0x0005CA00
		[CompilerGenerated]
		internal static void <PackBlocks>g__mergeBranch|37_3(ControlFlowGraphBuilder.BasicBlockBuilder predecessor, ref ControlFlowGraphBuilder.BasicBlockBuilder.Branch predecessorBranch, ref ControlFlowGraphBuilder.BasicBlockBuilder.Branch successorBranch)
		{
			predecessorBranch.Destination = successorBranch.Destination;
			ControlFlowGraphBuilder.BasicBlockBuilder destination = successorBranch.Destination;
			if (destination != null)
			{
				destination.AddPredecessor(predecessor);
			}
			predecessorBranch.Kind = successorBranch.Kind;
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x0005E82C File Offset: 0x0005CA2C
		[CompilerGenerated]
		internal static bool <PackBlocks>g__checkBranchesFromPredecessors|37_4(ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder> predecessors, ControlFlowGraphBuilder.RegionBuilder currentRegion, [Nullable(2)] ControlFlowGraphBuilder.RegionBuilder destinationRegionOpt, ref ControlFlowGraphBuilder.<>c__DisplayClass37_0 A_3)
		{
			foreach (ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder in predecessors)
			{
				ControlFlowGraphBuilder.RegionBuilder regionBuilder = A_3.regionMap[basicBlockBuilder];
				if (regionBuilder != currentRegion)
				{
					if (destinationRegionOpt == null)
					{
						return false;
					}
					ArrayBuilder<ControlFlowGraphBuilder.RegionBuilder> fromPredecessor = A_3.fromPredecessor;
					if (fromPredecessor != null)
					{
						fromPredecessor.Clear();
					}
					ControlFlowGraphBuilder.<PackBlocks>g__collectAncestorsAndSelf|37_5(currentRegion, ref A_3.fromCurrent);
					ControlFlowGraphBuilder.<PackBlocks>g__collectAncestorsAndSelf|37_5(destinationRegionOpt, ref A_3.fromDestination);
					ControlFlowGraphBuilder.<PackBlocks>g__collectAncestorsAndSelf|37_5(regionBuilder, ref A_3.fromPredecessor);
					int num = ControlFlowGraphBuilder.<PackBlocks>g__getIndexOfLastLeftRegion|37_6(A_3.fromCurrent, A_3.fromDestination);
					int num2 = ControlFlowGraphBuilder.<PackBlocks>g__getIndexOfLastLeftRegion|37_6(A_3.fromPredecessor, A_3.fromDestination);
					int num3 = ControlFlowGraphBuilder.<PackBlocks>g__getIndexOfLastLeftRegion|37_6(A_3.fromPredecessor, A_3.fromCurrent);
					if (A_3.fromPredecessor.Count - num3 + A_3.fromCurrent.Count - num != A_3.fromPredecessor.Count - num2)
					{
						return false;
					}
				}
				else if (basicBlockBuilder.Kind == BasicBlockKind.Entry && destinationRegionOpt == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x0005E91B File Offset: 0x0005CB1B
		[NullableContext(2)]
		[CompilerGenerated]
		internal static void <PackBlocks>g__collectAncestorsAndSelf|37_5([DisallowNull] ControlFlowGraphBuilder.RegionBuilder from, [Nullable(new byte[] { 2, 1 })] [NotNull] ref ArrayBuilder<ControlFlowGraphBuilder.RegionBuilder> builder)
		{
			if (builder == null)
			{
				builder = ArrayBuilder<ControlFlowGraphBuilder.RegionBuilder>.GetInstance();
			}
			else if (builder.Count != 0)
			{
				return;
			}
			do
			{
				builder.Add(from);
				from = from.Enclosing;
			}
			while (from != null);
			builder.ReverseContents();
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x0005E950 File Offset: 0x0005CB50
		[CompilerGenerated]
		internal static int <PackBlocks>g__getIndexOfLastLeftRegion|37_6(ArrayBuilder<ControlFlowGraphBuilder.RegionBuilder> from, ArrayBuilder<ControlFlowGraphBuilder.RegionBuilder> to)
		{
			int num = 0;
			while (num < from.Count && num < to.Count && from[num] == to[num])
			{
				num++;
			}
			return num;
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x0005E988 File Offset: 0x0005CB88
		[CompilerGenerated]
		internal static void <CheckUnresolvedBranches>g__fixupBranch|38_0(ref ControlFlowGraphBuilder.BasicBlockBuilder.Branch branch, ref ControlFlowGraphBuilder.<>c__DisplayClass38_0 A_1)
		{
			if (branch.Destination != null && A_1.unresolved.Contains(branch.Destination))
			{
				branch.Destination = null;
				branch.Kind = ControlFlowBranchSemantics.Error;
			}
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x0005E9B4 File Offset: 0x0005CBB4
		[CompilerGenerated]
		private bool <VisitStatementsOneOrAll>g__visitPossibleUsingDeclarationInLabel|52_0(ILabeledOperation labelOperation, ref ControlFlowGraphBuilder.<>c__DisplayClass52_0 A_2)
		{
			IOperation currentStatement = this._currentStatement;
			this._currentStatement = labelOperation;
			this.StartVisitingStatement(labelOperation);
			this.VisitLabel(labelOperation.Label);
			bool flag = this.VisitStatementsOneOrAll(labelOperation.Operation, A_2.statements, A_2.startIndex);
			this.FinishVisitingStatement(labelOperation, null);
			this._currentStatement = currentStatement;
			return flag;
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x0005EA0C File Offset: 0x0005CC0C
		[CompilerGenerated]
		internal static IOperation <VisitNullableBinaryConditionalOperator>g__negateNullable|90_0(IOperation operand)
		{
			return new UnaryOperation(UnaryOperatorKind.Not, operand, true, false, null, null, operand.Syntax, operand.Type, null, true);
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x0005EA34 File Offset: 0x0005CC34
		[CompilerGenerated]
		private void <VisitCoalesceAssignment>g__nullableValueTypeReturn|103_0(ref ControlFlowGraphBuilder.<>c__DisplayClass103_0 A_1)
		{
			int num = -1;
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = null;
			if (!A_1.isStatement)
			{
				evalStackFrame = this.PushStackFrame();
				this.SpillEvalStack();
				num = this.GetNextCaptureId(evalStackFrame.RegionBuilderOpt);
				this.AddStatement(new FlowCaptureOperation(num, A_1.operation.Target.Syntax, this.CallNullableMember(A_1.valueCapture, SpecialMember.System_Nullable_T_GetValueOrDefault)));
			}
			this.ConditionalBranch(this.CallNullableMember(OperationCloner.CloneOperation<IOperation>(A_1.valueCapture), SpecialMember.System_Nullable_T_get_HasValue), false, A_1.whenNull);
			if (!A_1.isStatement)
			{
				this._currentBasicBlock = null;
				this.AddStatement(new FlowCaptureOperation(A_1.resultCaptureId, A_1.operation.Syntax, ControlFlowGraphBuilder.GetCaptureReference(num, A_1.operation.Target)));
				this.PopStackFrame(evalStackFrame, true);
			}
			this.PopStackFrame(A_1.valueFrame, true);
			this.UnconditionalBranch(A_1.afterCoalesce);
			this.AppendNewBlock(A_1.whenNull, true);
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame2 = this.PushStackFrame();
			this.SpillEvalStack();
			IOperation operation = this.VisitRequired(A_1.operation.Value, default(int?));
			if (!A_1.isStatement)
			{
				int nextCaptureId = this.GetNextCaptureId(evalStackFrame2.RegionBuilderOpt);
				this.AddStatement(new FlowCaptureOperation(nextCaptureId, operation.Syntax, operation));
				operation = ControlFlowGraphBuilder.GetCaptureReference(nextCaptureId, operation);
				this.AddStatement(new FlowCaptureOperation(A_1.resultCaptureId, A_1.operation.Syntax, ControlFlowGraphBuilder.GetCaptureReference(nextCaptureId, operation)));
			}
			this.AddStatement(new SimpleAssignmentOperation(false, OperationCloner.CloneOperation<IOperation>(A_1.locationCapture), this.CreateConversion(operation, A_1.operation.Target.Type), null, A_1.operation.Syntax, A_1.operation.Target.Type, A_1.operation.GetConstantValue(), true));
			this.PopStackFrameAndLeaveRegion(evalStackFrame2);
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x0005EBF8 File Offset: 0x0005CDF8
		[CompilerGenerated]
		private void <VisitCoalesceAssignment>g__standardReturn|103_1(ref ControlFlowGraphBuilder.<>c__DisplayClass103_0 A_1)
		{
			this.ConditionalBranch(this.MakeIsNullOperation(A_1.valueCapture), true, A_1.whenNull);
			if (!A_1.isStatement)
			{
				this._currentBasicBlock = null;
				this.AddStatement(new FlowCaptureOperation(A_1.resultCaptureId, A_1.operation.Syntax, OperationCloner.CloneOperation<IOperation>(A_1.valueCapture)));
			}
			this.PopStackFrameAndLeaveRegion(A_1.valueFrame);
			this.UnconditionalBranch(A_1.afterCoalesce);
			this.AppendNewBlock(A_1.whenNull, true);
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			IOperation operation = this.VisitRequired(A_1.operation.Value, default(int?));
			IOperation operation2 = new SimpleAssignmentOperation(false, OperationCloner.CloneOperation<IOperation>(A_1.locationCapture), operation, null, A_1.operation.Syntax, A_1.operation.Type, A_1.operation.GetConstantValue(), true);
			if (A_1.isStatement)
			{
				this.AddStatement(operation2);
			}
			else
			{
				this.AddStatement(new FlowCaptureOperation(A_1.resultCaptureId, A_1.operation.Syntax, operation2));
			}
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x0005ED06 File Offset: 0x0005CF06
		[CompilerGenerated]
		private void <VisitConditionalAccess>g__resetConditionalAccessTracker|112_0(ref ControlFlowGraphBuilder.<>c__DisplayClass112_0 A_1)
		{
			this._currentConditionalAccessTracker.Free();
			this._currentConditionalAccessTracker = A_1.previousTracker;
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0005ED20 File Offset: 0x0005CF20
		[CompilerGenerated]
		internal static bool <VisitConditionalAccess>g__isConditionalAccessInstancePresentInChildren|112_1(IOperation operation)
		{
			InvalidOperation invalidOperation = operation as InvalidOperation;
			if (invalidOperation != null)
			{
				return ControlFlowGraphBuilder.<VisitConditionalAccess>g__checkInvalidChildren|112_2(invalidOperation);
			}
			Operation operation2 = (Operation)operation;
			InvalidOperation invalidOperation2;
			for (;;)
			{
				Operation.Enumerator enumerator = operation2.ChildOperations.GetEnumerator();
				if (!enumerator.MoveNext())
				{
					return false;
				}
				if (enumerator.Current is IConditionalAccessInstanceOperation)
				{
					break;
				}
				invalidOperation2 = enumerator.Current as InvalidOperation;
				if (invalidOperation2 != null)
				{
					goto Block_4;
				}
				operation2 = (Operation)enumerator.Current;
			}
			return true;
			Block_4:
			return ControlFlowGraphBuilder.<VisitConditionalAccess>g__checkInvalidChildren|112_2(invalidOperation2);
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x0005ED98 File Offset: 0x0005CF98
		[CompilerGenerated]
		internal static bool <VisitConditionalAccess>g__checkInvalidChildren|112_2(InvalidOperation operation)
		{
			foreach (IOperation operation2 in operation.ChildOperations)
			{
				if (operation2 is IConditionalAccessInstanceOperation || ControlFlowGraphBuilder.<VisitConditionalAccess>g__isConditionalAccessInstancePresentInChildren|112_1(operation2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x0005EE00 File Offset: 0x0005D000
		[CompilerGenerated]
		private void <HandleUsingOperationParts>g__processQueue|129_0([Nullable(new byte[] { 2, 0, 1, 1 })] ArrayBuilder<ValueTuple<IVariableDeclarationOperation, IVariableDeclaratorOperation>> resourceQueueOpt, ref ControlFlowGraphBuilder.<>c__DisplayClass129_0 A_2)
		{
			if (resourceQueueOpt == null || resourceQueueOpt.Count == 0)
			{
				this.VisitStatement(A_2.body);
				return;
			}
			ValueTuple<IVariableDeclarationOperation, IVariableDeclaratorOperation> valueTuple = resourceQueueOpt.Pop<ValueTuple<IVariableDeclarationOperation, IVariableDeclaratorOperation>>();
			IVariableDeclarationOperation item = valueTuple.Item1;
			IVariableDeclaratorOperation item2 = valueTuple.Item2;
			this.HandleVariableDeclarator(item, item2);
			ILocalSymbol symbol = item2.Symbol;
			this.<HandleUsingOperationParts>g__processResource|129_2(new LocalReferenceOperation(symbol, false, null, item2.Syntax, symbol.Type, null, true), resourceQueueOpt, ref A_2);
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x0005EE65 File Offset: 0x0005D065
		[CompilerGenerated]
		internal static bool <HandleUsingOperationParts>g__shouldConvertToIDisposableBeforeTry|129_1(IOperation resource)
		{
			return resource.Type == null || resource.Type.Kind == SymbolKind.DynamicType;
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x0005EE80 File Offset: 0x0005D080
		[CompilerGenerated]
		private void <HandleUsingOperationParts>g__processResource|129_2(IOperation resource, [Nullable(new byte[] { 2, 0, 1, 1 })] ArrayBuilder<ValueTuple<IVariableDeclarationOperation, IVariableDeclaratorOperation>> resourceQueueOpt, ref ControlFlowGraphBuilder.<>c__DisplayClass129_0 A_3)
		{
			ControlFlowGraphBuilder.RegionBuilder regionBuilder = null;
			if (ControlFlowGraphBuilder.<HandleUsingOperationParts>g__shouldConvertToIDisposableBeforeTry|129_1(resource))
			{
				regionBuilder = new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.LocalLifetime, null, default(ImmutableArray<ILocalSymbol>), false);
				this.EnterRegion(regionBuilder, false);
				resource = this.ConvertToIDisposable(resource, A_3.iDisposable, false);
				int nextCaptureId = this.GetNextCaptureId(regionBuilder);
				this.AddStatement(new FlowCaptureOperation(nextCaptureId, resource.Syntax, resource));
				resource = ControlFlowGraphBuilder.GetCaptureReference(nextCaptureId, resource);
			}
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.TryAndFinally, null, default(ImmutableArray<ILocalSymbol>), false), false);
			this.EnterRegion(new ControlFlowGraphBuilder.RegionBuilder(ControlFlowRegionKind.Try, null, default(ImmutableArray<ILocalSymbol>), false), false);
			this.<HandleUsingOperationParts>g__processQueue|129_0(resourceQueueOpt, ref A_3);
			this.UnconditionalBranch(basicBlockBuilder);
			this.LeaveRegion();
			this.AddDisposingFinally(resource, false, A_3.iDisposable, A_3.disposeMethod, A_3.disposeArguments, A_3.isAsynchronous);
			this.LeaveRegion();
			if (regionBuilder != null)
			{
				this.LeaveRegion();
			}
			this.AppendNewBlock(basicBlockBuilder, false);
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x0005EF68 File Offset: 0x0005D168
		[CompilerGenerated]
		[return: Nullable(2)]
		private IOperation <AddDisposingFinally>g__tryDispose|130_0(IOperation value, ref ControlFlowGraphBuilder.<>c__DisplayClass130_0 A_2)
		{
			IMethodSymbol methodSymbol;
			if ((methodSymbol = A_2.disposeMethod) == null)
			{
				if (!A_2.isAsynchronous)
				{
					ISymbolInternal symbolInternal = this._compilation.CommonGetSpecialTypeMember(SpecialMember.System_IDisposable__Dispose);
					methodSymbol = (IMethodSymbol)((symbolInternal != null) ? symbolInternal.GetISymbol() : null);
				}
				else
				{
					ISymbolInternal symbolInternal2 = this._compilation.CommonGetWellKnownTypeMember(WellKnownMember.System_IAsyncDisposable__DisposeAsync);
					methodSymbol = (IMethodSymbol)((symbolInternal2 != null) ? symbolInternal2.GetISymbol() : null);
				}
			}
			IMethodSymbol methodSymbol2 = methodSymbol;
			if (methodSymbol2 == null)
			{
				return null;
			}
			ImmutableArray<IArgumentOperation> immutableArray = ((A_2.disposeMethod != null) ? this.VisitArguments(A_2.disposeArguments) : ImmutableArray<IArgumentOperation>.Empty);
			IMethodSymbol methodSymbol3 = methodSymbol2;
			IMethodSymbol disposeMethod = A_2.disposeMethod;
			InvocationOperation invocationOperation = new InvocationOperation(methodSymbol3, value, disposeMethod == null || disposeMethod.IsVirtual, immutableArray, null, value.Syntax, methodSymbol2.ReturnType, true);
			if (A_2.isAsynchronous)
			{
				return new AwaitOperation(invocationOperation, null, value.Syntax, this._compilation.GetSpecialType(SpecialType.System_Void), true);
			}
			return invocationOperation;
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x0005F037 File Offset: 0x0005D237
		[NullableContext(2)]
		[CompilerGenerated]
		internal static bool <AddDisposingFinally>g__isNotNullableValueType|130_1([NotNullWhen(true)] ITypeSymbol type)
		{
			return type != null && type.IsValueType && !ITypeSymbolHelpers.IsNullableType(type);
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x0005F050 File Offset: 0x0005D250
		[CompilerGenerated]
		internal static IOperation <VisitForEachLoop>g__applyConversion|133_0([Nullable(2)] IConvertibleConversion conversionOpt, IOperation operand, [Nullable(2)] ITypeSymbol targetType)
		{
			if (conversionOpt != null && !conversionOpt.ToCommonConversion().IsIdentity)
			{
				operand = new ConversionOperation(operand, conversionOpt, false, false, null, operand.Syntax, targetType, null, true);
			}
			return operand;
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x0005F088 File Offset: 0x0005D288
		[CompilerGenerated]
		private IOperation <VisitForEachLoop>g__getEnumerator|133_1(ref ControlFlowGraphBuilder.<>c__DisplayClass133_0 A_1)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			ForEachLoopOperationInfo info = A_1.info;
			IOperation operation2;
			if (((info != null) ? info.GetEnumeratorMethod : null) != null)
			{
				IOperation operation = this.<VisitForEachLoop>g__makeInvocation|133_6(A_1.operation.Collection.Syntax, A_1.info.GetEnumeratorMethod, A_1.info.GetEnumeratorMethod.IsStatic ? null : this.Visit(A_1.operation.Collection), A_1.info.GetEnumeratorArguments, ref A_1);
				int nextCaptureId = this.GetNextCaptureId(A_1.enumeratorCaptureRegion);
				this.AddStatement(new FlowCaptureOperation(nextCaptureId, A_1.operation.Collection.Syntax, operation));
				operation2 = new FlowCaptureReferenceOperation(nextCaptureId, A_1.operation.Collection.Syntax, A_1.info.GetEnumeratorMethod.ReturnType, null);
			}
			else
			{
				this.AddStatement(ControlFlowGraphBuilder.MakeInvalidOperation(null, this.VisitRequired(A_1.operation.Collection, default(int?))));
				operation2 = new InvalidOperation(ImmutableArray<IOperation>.Empty, null, A_1.operation.Collection.Syntax, null, null, true);
			}
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
			return operation2;
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x0005F1AC File Offset: 0x0005D3AC
		[CompilerGenerated]
		private IOperation <VisitForEachLoop>g__getCondition|133_2(IOperation enumeratorRef, ref ControlFlowGraphBuilder.<>c__DisplayClass133_0 A_2)
		{
			ForEachLoopOperationInfo info = A_2.info;
			if (((info != null) ? info.MoveNextMethod : null) == null)
			{
				return ControlFlowGraphBuilder.MakeInvalidOperation(this._compilation.GetSpecialType(SpecialType.System_Boolean), enumeratorRef);
			}
			InvocationOperation invocationOperation = this.<VisitForEachLoop>g__makeInvocationDroppingInstanceForStaticMethods|133_5(A_2.info.MoveNextMethod, enumeratorRef, A_2.info.MoveNextArguments, ref A_2);
			if (A_2.operation.IsAsynchronous)
			{
				return new AwaitOperation(invocationOperation, null, A_2.operation.Syntax, this._compilation.GetSpecialType(SpecialType.System_Boolean), true);
			}
			return invocationOperation;
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x0005F230 File Offset: 0x0005D430
		[CompilerGenerated]
		private IOperation <VisitForEachLoop>g__getCurrent|133_3(IOperation enumeratorRef, ref ControlFlowGraphBuilder.<>c__DisplayClass133_0 A_2)
		{
			ForEachLoopOperationInfo info = A_2.info;
			if (((info != null) ? info.CurrentProperty : null) != null)
			{
				return new PropertyReferenceOperation(A_2.info.CurrentProperty, this.<VisitForEachLoop>g__makeArguments|133_7(A_2.info.CurrentArguments, ref A_2), A_2.info.CurrentProperty.IsStatic ? null : enumeratorRef, null, A_2.operation.LoopControlVariable.Syntax, A_2.info.CurrentProperty.Type, true);
			}
			return ControlFlowGraphBuilder.MakeInvalidOperation(null, enumeratorRef);
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x0005F2B4 File Offset: 0x0005D4B4
		[CompilerGenerated]
		private IOperation <VisitForEachLoop>g__getLoopControlVariableAssignment|133_4(IOperation current, ref ControlFlowGraphBuilder.<>c__DisplayClass133_0 A_2)
		{
			OperationKind kind = A_2.operation.LoopControlVariable.Kind;
			if (kind == OperationKind.Tuple || kind == OperationKind.DeclarationExpression)
			{
				return new DeconstructionAssignmentOperation(this.VisitPreservingTupleOperations(A_2.operation.LoopControlVariable), current, null, A_2.operation.LoopControlVariable.Syntax, A_2.operation.LoopControlVariable.Type, true);
			}
			if (kind == OperationKind.VariableDeclarator)
			{
				IVariableDeclaratorOperation variableDeclaratorOperation = (IVariableDeclaratorOperation)A_2.operation.LoopControlVariable;
				ILocalSymbol symbol = variableDeclaratorOperation.Symbol;
				ForEachLoopOperationInfo info = A_2.info;
				current = ControlFlowGraphBuilder.<VisitForEachLoop>g__applyConversion|133_0((info != null) ? info.ElementConversion : null, current, symbol.Type);
				return new SimpleAssignmentOperation(symbol.RefKind > RefKind.None, new LocalReferenceOperation(symbol, true, null, variableDeclaratorOperation.Syntax, symbol.Type, null, true), current, null, variableDeclaratorOperation.Syntax, null, null, true);
			}
			return new SimpleAssignmentOperation(false, this.VisitRequired(A_2.operation.LoopControlVariable, default(int?)), current, null, A_2.operation.LoopControlVariable.Syntax, A_2.operation.LoopControlVariable.Type, null, true);
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x0005F3CA File Offset: 0x0005D5CA
		[CompilerGenerated]
		private InvocationOperation <VisitForEachLoop>g__makeInvocationDroppingInstanceForStaticMethods|133_5(IMethodSymbol method, IOperation instance, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IArgumentOperation> arguments, ref ControlFlowGraphBuilder.<>c__DisplayClass133_0 A_4)
		{
			return this.<VisitForEachLoop>g__makeInvocation|133_6(instance.Syntax, method, method.IsStatic ? null : instance, arguments, ref A_4);
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x0005F3E8 File Offset: 0x0005D5E8
		[CompilerGenerated]
		private InvocationOperation <VisitForEachLoop>g__makeInvocation|133_6(SyntaxNode syntax, IMethodSymbol method, [Nullable(2)] IOperation instanceOpt, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IArgumentOperation> arguments, ref ControlFlowGraphBuilder.<>c__DisplayClass133_0 A_5)
		{
			return new InvocationOperation(method, instanceOpt, method.IsVirtual || method.IsAbstract || method.IsOverride, this.<VisitForEachLoop>g__makeArguments|133_7(arguments, ref A_5), null, syntax, method.ReturnType, true);
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x0005F420 File Offset: 0x0005D620
		[CompilerGenerated]
		[return: Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<IArgumentOperation> <VisitForEachLoop>g__makeArguments|133_7([Nullable(new byte[] { 0, 1 })] ImmutableArray<IArgumentOperation> arguments, ref ControlFlowGraphBuilder.<>c__DisplayClass133_0 A_2)
		{
			if (new ImmutableArray<IArgumentOperation>?(arguments) != default(ImmutableArray<IArgumentOperation>?))
			{
				return this.VisitArguments(arguments);
			}
			return ImmutableArray<IArgumentOperation>.Empty;
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x0005F450 File Offset: 0x0005D650
		[CompilerGenerated]
		private IOperation <VisitForToLoop>g__tryCallObjectForLoopControlHelper|134_0(SyntaxNode syntax, WellKnownMember helper, ref ControlFlowGraphBuilder.<>c__DisplayClass134_0 A_3)
		{
			bool flag = helper == WellKnownMember.Microsoft_VisualBasic_CompilerServices_ObjectFlowControl_ForLoopControl__ForLoopInitObj;
			LocalReferenceOperation localReferenceOperation = new LocalReferenceOperation(A_3.loopObject, flag, null, A_3.operation.LoopControlVariable.Syntax, A_3.loopObject.Type, null, true);
			ISymbolInternal symbolInternal = this._compilation.CommonGetWellKnownTypeMember(helper);
			IMethodSymbol methodSymbol = (IMethodSymbol)((symbolInternal != null) ? symbolInternal.GetISymbol() : null);
			int num = WellKnownMembers.GetDescriptor(helper).ParametersCount;
			if (methodSymbol == null)
			{
				ArrayBuilder<IOperation> instance = ArrayBuilder<IOperation>.GetInstance(--num, null);
				instance[--num] = localReferenceOperation;
				do
				{
					instance[--num] = this.PopOperand();
				}
				while (num != 0);
				return ControlFlowGraphBuilder.MakeInvalidOperation(A_3.operation.LimitValue.Syntax, A_3.booleanType, instance.ToImmutableAndFree());
			}
			ArrayBuilder<IArgumentOperation> instance2 = ArrayBuilder<IArgumentOperation>.GetInstance(num, null);
			instance2[--num] = new ArgumentOperation(ArgumentKind.Explicit, methodSymbol.Parameters[num], this.<VisitForToLoop>g__visitLoopControlVariableReference|134_5(true, ref A_3), OperationFactory.IdentityConversion, OperationFactory.IdentityConversion, null, syntax, true);
			instance2[--num] = new ArgumentOperation(ArgumentKind.Explicit, methodSymbol.Parameters[num], localReferenceOperation, OperationFactory.IdentityConversion, OperationFactory.IdentityConversion, null, syntax, true);
			do
			{
				IOperation operation = this.PopOperand();
				instance2[--num] = new ArgumentOperation(ArgumentKind.Explicit, methodSymbol.Parameters[num], operation, OperationFactory.IdentityConversion, OperationFactory.IdentityConversion, null, flag ? operation.Syntax : syntax, true);
			}
			while (num != 0);
			return new InvocationOperation(methodSymbol, null, false, instance2.ToImmutableAndFree(), null, A_3.operation.LimitValue.Syntax, methodSymbol.ReturnType, true);
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x0005F5F4 File Offset: 0x0005D7F4
		[CompilerGenerated]
		private void <VisitForToLoop>g__initializeLoop|134_1(ref ControlFlowGraphBuilder.<>c__DisplayClass134_0 A_1)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.PushOperand(this.<VisitForToLoop>g__visitLoopControlVariableReference|134_5(false, ref A_1));
			this.PushOperand(this.VisitRequired(A_1.operation.InitialValue, default(int?)));
			if (A_1.isObjectLoop)
			{
				this.PushOperand(this.VisitRequired(A_1.operation.LimitValue, default(int?)));
				this.PushOperand(this.VisitRequired(A_1.operation.StepValue, default(int?)));
				IOperation operation = this.<VisitForToLoop>g__tryCallObjectForLoopControlHelper|134_0(A_1.operation.LoopControlVariable.Syntax, WellKnownMember.Microsoft_VisualBasic_CompilerServices_ObjectFlowControl_ForLoopControl__ForLoopInitObj, ref A_1);
				this.ConditionalBranch(operation, false, A_1.@break);
				this.UnconditionalBranch(A_1.bodyBlock);
			}
			else
			{
				this.SpillEvalStack();
				ControlFlowGraphBuilder.RegionBuilder currentRegionRequired = this.CurrentRegionRequired;
				A_1.limitValueId = this.GetNextCaptureId(A_1.loopRegion);
				this.VisitAndCapture(A_1.operation.LimitValue, A_1.limitValueId);
				A_1.stepValueId = this.GetNextCaptureId(A_1.loopRegion);
				this.VisitAndCapture(A_1.operation.StepValue, A_1.stepValueId);
				IOperation operation2 = ControlFlowGraphBuilder.GetCaptureReference(A_1.stepValueId, A_1.operation.StepValue);
				if (A_1.userDefinedInfo != null)
				{
					this._forToLoopBinaryOperatorLeftOperand = ControlFlowGraphBuilder.GetCaptureReference(A_1.stepValueId, A_1.operation.StepValue);
					this._forToLoopBinaryOperatorRightOperand = ControlFlowGraphBuilder.GetCaptureReference(A_1.stepValueId, A_1.operation.StepValue);
					IOperation operation3 = this.VisitRequired(A_1.userDefinedInfo.Subtraction, default(int?));
					this._forToLoopBinaryOperatorLeftOperand = operation2;
					this._forToLoopBinaryOperatorRightOperand = operation3;
					int nextCaptureId = this.GetNextCaptureId(A_1.loopRegion);
					this.VisitAndCapture(A_1.userDefinedInfo.GreaterThanOrEqual, nextCaptureId);
					A_1.positiveFlag = ControlFlowGraphBuilder.GetCaptureReference(nextCaptureId, A_1.userDefinedInfo.GreaterThanOrEqual);
					this._forToLoopBinaryOperatorLeftOperand = null;
					this._forToLoopBinaryOperatorRightOperand = null;
				}
				else
				{
					ConstantValue constantValue = A_1.operation.StepValue.GetConstantValue();
					if ((constantValue == null || constantValue.IsBad) && !ITypeSymbolHelpers.IsSignedIntegralType(A_1.stepEnumUnderlyingTypeOrSelf) && !ITypeSymbolHelpers.IsUnsignedIntegralType(A_1.stepEnumUnderlyingTypeOrSelf))
					{
						IOperation operation4 = null;
						if (ITypeSymbolHelpers.IsNullableType(operation2.Type))
						{
							operation4 = ControlFlowGraphBuilder.MakeIsNullOperation(ControlFlowGraphBuilder.GetCaptureReference(A_1.stepValueId, A_1.operation.StepValue), A_1.booleanType);
							operation2 = this.CallNullableMember(operation2, SpecialMember.System_Nullable_T_GetValueOrDefault);
						}
						ITypeSymbol enumUnderlyingTypeOrSelf = ITypeSymbolHelpers.GetEnumUnderlyingTypeOrSelf(operation2.Type);
						if (ITypeSymbolHelpers.IsNumericType(enumUnderlyingTypeOrSelf))
						{
							int nextCaptureId2 = this.GetNextCaptureId(A_1.loopRegion);
							ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
							IOperation operation5;
							if (operation4 != null)
							{
								ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
								this.ConditionalBranch(operation4, false, basicBlockBuilder2);
								this._currentBasicBlock = null;
								operation5 = new LiteralOperation(null, operation2.Syntax, A_1.booleanType, ConstantValue.Create(false), true);
								this.AddStatement(new FlowCaptureOperation(nextCaptureId2, operation5.Syntax, operation5));
								this.UnconditionalBranch(basicBlockBuilder);
								this.AppendNewBlock(basicBlockBuilder2, true);
							}
							IOperation operation6 = new LiteralOperation(null, operation2.Syntax, operation2.Type, ConstantValue.Default(enumUnderlyingTypeOrSelf.SpecialType), true);
							operation5 = new BinaryOperation(BinaryOperatorKind.GreaterThanOrEqual, operation2, operation6, false, false, false, null, null, null, operation2.Syntax, A_1.booleanType, null, true);
							this.AddStatement(new FlowCaptureOperation(nextCaptureId2, operation5.Syntax, operation5));
							this.AppendNewBlock(basicBlockBuilder, true);
							A_1.positiveFlag = ControlFlowGraphBuilder.GetCaptureReference(nextCaptureId2, operation5);
						}
					}
				}
				IOperation operation7 = this.PopOperand();
				this.AddStatement(new SimpleAssignmentOperation(false, this.PopOperand(), operation7, null, A_1.operation.InitialValue.Syntax, null, null, true));
			}
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x0005F9A0 File Offset: 0x0005DBA0
		[CompilerGenerated]
		private void <VisitForToLoop>g__checkLoopCondition|134_2(ref ControlFlowGraphBuilder.<>c__DisplayClass134_0 A_1)
		{
			if (A_1.isObjectLoop)
			{
				ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
				this.PushOperand(this.<VisitForToLoop>g__visitLoopControlVariableReference|134_5(true, ref A_1));
				IOperation operation = this.<VisitForToLoop>g__tryCallObjectForLoopControlHelper|134_0(A_1.operation.LimitValue.Syntax, WellKnownMember.Microsoft_VisualBasic_CompilerServices_ObjectFlowControl_ForLoopControl__ForNextCheckObj, ref A_1);
				this.ConditionalBranch(operation, false, A_1.@break);
				this.UnconditionalBranch(A_1.bodyBlock);
				this.PopStackFrameAndLeaveRegion(evalStackFrame);
				return;
			}
			if (A_1.userDefinedInfo != null)
			{
				ControlFlowGraphBuilder.EvalStackFrame evalStackFrame2 = this.PushStackFrame();
				this.PushOperand(this.<VisitForToLoop>g__visitLoopControlVariableReference|134_5(true, ref A_1));
				this.SpillEvalStack();
				IOperation operation2 = this.PopOperand();
				ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
				this.ConditionalBranch(A_1.positiveFlag, false, basicBlockBuilder);
				this._currentBasicBlock = null;
				this._forToLoopBinaryOperatorLeftOperand = operation2;
				this._forToLoopBinaryOperatorRightOperand = ControlFlowGraphBuilder.GetCaptureReference(A_1.limitValueId, A_1.operation.LimitValue);
				this.VisitConditionalBranch(A_1.userDefinedInfo.LessThanOrEqual, ref A_1.@break, false);
				this.UnconditionalBranch(A_1.bodyBlock);
				this.AppendNewBlock(basicBlockBuilder, true);
				this._forToLoopBinaryOperatorLeftOperand = OperationCloner.CloneOperation<IOperation>(this._forToLoopBinaryOperatorLeftOperand);
				this._forToLoopBinaryOperatorRightOperand = OperationCloner.CloneOperation<IOperation>(this._forToLoopBinaryOperatorRightOperand);
				this.VisitConditionalBranch(A_1.userDefinedInfo.GreaterThanOrEqual, ref A_1.@break, false);
				this.UnconditionalBranch(A_1.bodyBlock);
				this.PopStackFrameAndLeaveRegion(evalStackFrame2);
				this._forToLoopBinaryOperatorLeftOperand = null;
				this._forToLoopBinaryOperatorRightOperand = null;
				return;
			}
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame3 = this.PushStackFrame();
			this.PushOperand(this.<VisitForToLoop>g__visitLoopControlVariableReference|134_5(true, ref A_1));
			IOperation operation3 = ControlFlowGraphBuilder.GetCaptureReference(A_1.limitValueId, A_1.operation.LimitValue);
			BinaryOperatorKind binaryOperatorKind = BinaryOperatorKind.None;
			if (ITypeSymbolHelpers.IsUnsignedIntegralType(A_1.stepEnumUnderlyingTypeOrSelf))
			{
				binaryOperatorKind = BinaryOperatorKind.LessThanOrEqual;
			}
			else
			{
				ConstantValue constantValue = A_1.operation.StepValue.GetConstantValue();
				if (constantValue != null && !constantValue.IsBad)
				{
					if (constantValue.IsNegativeNumeric)
					{
						binaryOperatorKind = BinaryOperatorKind.GreaterThanOrEqual;
					}
					else if (constantValue.IsNumeric)
					{
						binaryOperatorKind = BinaryOperatorKind.LessThanOrEqual;
					}
				}
			}
			if (binaryOperatorKind == BinaryOperatorKind.None && ITypeSymbolHelpers.IsSignedIntegralType(A_1.stepEnumUnderlyingTypeOrSelf))
			{
				binaryOperatorKind = BinaryOperatorKind.LessThanOrEqual;
				this.PushOperand(this.<VisitForToLoop>g__negateIfStepNegative|134_3(this.PopOperand(), ref A_1));
				operation3 = this.<VisitForToLoop>g__negateIfStepNegative|134_3(operation3, ref A_1);
			}
			IOperation operation4;
			if (binaryOperatorKind != BinaryOperatorKind.None)
			{
				operation4 = new BinaryOperation(binaryOperatorKind, this.PopOperand(), operation3, false, false, false, null, null, null, A_1.operation.LimitValue.Syntax, A_1.booleanType, null, true);
				this.ConditionalBranch(operation4, false, A_1.@break);
				this.UnconditionalBranch(A_1.bodyBlock);
				this.PopStackFrameAndLeaveRegion(evalStackFrame3);
				return;
			}
			if (A_1.positiveFlag == null)
			{
				operation4 = ControlFlowGraphBuilder.MakeInvalidOperation(A_1.operation.LimitValue.Syntax, A_1.booleanType, this.PopOperand(), operation3);
				this.ConditionalBranch(operation4, false, A_1.@break);
				this.UnconditionalBranch(A_1.bodyBlock);
				this.PopStackFrameAndLeaveRegion(evalStackFrame3);
				return;
			}
			if (ITypeSymbolHelpers.IsNullableType(A_1.operation.LimitValue.Type))
			{
				IOperation operation5 = new BinaryOperation(BinaryOperatorKind.Or, ControlFlowGraphBuilder.MakeIsNullOperation(operation3, A_1.booleanType), ControlFlowGraphBuilder.MakeIsNullOperation(this.PopOperand(), A_1.booleanType), false, false, false, null, null, null, A_1.operation.StepValue.Syntax, this._compilation.GetSpecialType(SpecialType.System_Boolean), null, true);
				ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
				this.ConditionalBranch(operation5, false, basicBlockBuilder2);
				this.UnconditionalBranch(A_1.@break);
				this.PopStackFrameAndLeaveRegion(evalStackFrame3);
				this.AppendNewBlock(basicBlockBuilder2, true);
				evalStackFrame3 = this.PushStackFrame();
				this.PushOperand(this.CallNullableMember(this.<VisitForToLoop>g__visitLoopControlVariableReference|134_5(true, ref A_1), SpecialMember.System_Nullable_T_GetValueOrDefault));
				operation3 = this.CallNullableMember(ControlFlowGraphBuilder.GetCaptureReference(A_1.limitValueId, A_1.operation.LimitValue), SpecialMember.System_Nullable_T_GetValueOrDefault);
			}
			this.SpillEvalStack();
			IOperation operation6 = this.PopOperand();
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder3 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			this.ConditionalBranch(A_1.positiveFlag, false, basicBlockBuilder3);
			this._currentBasicBlock = null;
			operation4 = new BinaryOperation(BinaryOperatorKind.LessThanOrEqual, operation6, operation3, false, false, false, null, null, null, A_1.operation.LimitValue.Syntax, A_1.booleanType, null, true);
			this.ConditionalBranch(operation4, false, A_1.@break);
			this.UnconditionalBranch(A_1.bodyBlock);
			this.AppendNewBlock(basicBlockBuilder3, true);
			operation4 = new BinaryOperation(BinaryOperatorKind.GreaterThanOrEqual, OperationCloner.CloneOperation<IOperation>(operation6), OperationCloner.CloneOperation<IOperation>(operation3), false, false, false, null, null, null, A_1.operation.LimitValue.Syntax, A_1.booleanType, null, true);
			this.ConditionalBranch(operation4, false, A_1.@break);
			this.UnconditionalBranch(A_1.bodyBlock);
			this.PopStackFrameAndLeaveRegion(evalStackFrame3);
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x0005FE04 File Offset: 0x0005E004
		[CompilerGenerated]
		private IOperation <VisitForToLoop>g__negateIfStepNegative|134_3(IOperation operand, ref ControlFlowGraphBuilder.<>c__DisplayClass134_0 A_2)
		{
			int num = A_2.stepEnumUnderlyingTypeOrSelf.SpecialType.VBForToShiftBits();
			LiteralOperation literalOperation = new LiteralOperation(null, operand.Syntax, this._compilation.GetSpecialType(SpecialType.System_Int32), ConstantValue.Create(num), true);
			BinaryOperation binaryOperation = new BinaryOperation(BinaryOperatorKind.RightShift, ControlFlowGraphBuilder.GetCaptureReference(A_2.stepValueId, A_2.operation.StepValue), literalOperation, false, false, false, null, null, null, operand.Syntax, A_2.operation.StepValue.Type, null, true);
			return new BinaryOperation(BinaryOperatorKind.ExclusiveOr, binaryOperation, operand, false, false, false, null, null, null, operand.Syntax, operand.Type, null, true);
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x0005FEA0 File Offset: 0x0005E0A0
		[CompilerGenerated]
		private void <VisitForToLoop>g__incrementLoopControlVariable|134_4(ref ControlFlowGraphBuilder.<>c__DisplayClass134_0 A_1)
		{
			if (A_1.isObjectLoop)
			{
				return;
			}
			if (A_1.userDefinedInfo != null)
			{
				ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
				IOperation operation = this.<VisitForToLoop>g__visitLoopControlVariableReference|134_5(true, ref A_1);
				this.PushOperand(operation);
				this._forToLoopBinaryOperatorLeftOperand = this.<VisitForToLoop>g__visitLoopControlVariableReference|134_5(true, ref A_1);
				this._forToLoopBinaryOperatorRightOperand = ControlFlowGraphBuilder.GetCaptureReference(A_1.stepValueId, A_1.operation.StepValue);
				IOperation operation2 = this.VisitRequired(A_1.userDefinedInfo.Addition, default(int?));
				this._forToLoopBinaryOperatorLeftOperand = null;
				this._forToLoopBinaryOperatorRightOperand = null;
				operation = this.PopOperand();
				this.AddStatement(new SimpleAssignmentOperation(false, operation, operation2, null, operation.Syntax, null, null, true));
				this.PopStackFrameAndLeaveRegion(evalStackFrame);
				return;
			}
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			bool flag = ITypeSymbolHelpers.IsNullableType(A_1.operation.StepValue.Type);
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame2 = this.PushStackFrame();
			this.PushOperand(this.<VisitForToLoop>g__visitLoopControlVariableReference|134_5(true, ref A_1));
			IOperation operation4;
			if (flag)
			{
				this.SpillEvalStack();
				ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
				ControlFlowGraphBuilder.EvalStackFrame evalStackFrame3 = this.PushStackFrame();
				IOperation operation3 = new BinaryOperation(BinaryOperatorKind.Or, ControlFlowGraphBuilder.MakeIsNullOperation(ControlFlowGraphBuilder.GetCaptureReference(A_1.stepValueId, A_1.operation.StepValue), A_1.booleanType), ControlFlowGraphBuilder.MakeIsNullOperation(this.<VisitForToLoop>g__visitLoopControlVariableReference|134_5(true, ref A_1), A_1.booleanType), false, false, false, null, null, null, A_1.operation.StepValue.Syntax, this._compilation.GetSpecialType(SpecialType.System_Boolean), null, true);
				this.ConditionalBranch(operation3, false, basicBlockBuilder2);
				this._currentBasicBlock = null;
				this.PopStackFrameAndLeaveRegion(evalStackFrame3);
				operation4 = OperationCloner.CloneOperation<IOperation>(this.PeekOperand());
				this.AddStatement(new SimpleAssignmentOperation(false, operation4, new DefaultValueOperation(null, operation4.Syntax, operation4.Type, null, true), null, operation4.Syntax, null, null, true));
				this.UnconditionalBranch(basicBlockBuilder);
				this.AppendNewBlock(basicBlockBuilder2, true);
			}
			IOperation operation5 = this.<VisitForToLoop>g__visitLoopControlVariableReference|134_5(true, ref A_1);
			IOperation operation6 = ControlFlowGraphBuilder.GetCaptureReference(A_1.stepValueId, A_1.operation.StepValue);
			if (flag)
			{
				operation5 = this.CallNullableMember(operation5, SpecialMember.System_Nullable_T_GetValueOrDefault);
				operation6 = this.CallNullableMember(operation6, SpecialMember.System_Nullable_T_GetValueOrDefault);
			}
			IOperation operation7 = new BinaryOperation(BinaryOperatorKind.Add, operation5, operation6, false, A_1.operation.IsChecked, false, null, null, null, A_1.operation.StepValue.Syntax, operation5.Type, null, true);
			operation4 = this.PopOperand();
			if (flag)
			{
				operation7 = this.MakeNullable(operation7, operation4.Type);
			}
			this.AddStatement(new SimpleAssignmentOperation(false, operation4, operation7, null, operation4.Syntax, null, null, true));
			this.PopStackFrame(evalStackFrame2, !flag);
			this.LeaveRegionIfAny(evalStackFrame2);
			this.AppendNewBlock(basicBlockBuilder, true);
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x00060138 File Offset: 0x0005E338
		[CompilerGenerated]
		private IOperation <VisitForToLoop>g__visitLoopControlVariableReference|134_5(bool forceImplicit, ref ControlFlowGraphBuilder.<>c__DisplayClass134_0 A_2)
		{
			if (A_2.operation.LoopControlVariable.Kind == OperationKind.VariableDeclarator)
			{
				IVariableDeclaratorOperation variableDeclaratorOperation = (IVariableDeclaratorOperation)A_2.operation.LoopControlVariable;
				ILocalSymbol symbol = variableDeclaratorOperation.Symbol;
				return new LocalReferenceOperation(symbol, true, null, variableDeclaratorOperation.Syntax, symbol.Type, null, true);
			}
			this._forceImplicit = forceImplicit;
			IOperation operation = this.VisitRequired(A_2.operation.LoopControlVariable, default(int?));
			this._forceImplicit = false;
			return operation;
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x000601B0 File Offset: 0x0005E3B0
		[CompilerGenerated]
		[return: Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<ILocalSymbol> <VisitSwitch>g__getLocals|137_0(ref ControlFlowGraphBuilder.<>c__DisplayClass137_0 A_1)
		{
			ImmutableArray<ILocalSymbol> immutableArray = A_1.operation.Locals;
			foreach (ISwitchCaseOperation switchCaseOperation in A_1.operation.Cases)
			{
				immutableArray = immutableArray.Concat<ILocalSymbol>(switchCaseOperation.Locals);
			}
			return immutableArray;
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x00060200 File Offset: 0x0005E400
		[CompilerGenerated]
		private void <VisitSwitch>g__handleSection|137_1(ISwitchCaseOperation section, ref ControlFlowGraphBuilder.<>c__DisplayClass137_0 A_2)
		{
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder2 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
			IOperation condition = ((SwitchCaseOperation)section).Condition;
			if (condition != null)
			{
				this._currentSwitchOperationExpression = A_2.switchValue;
				this.VisitConditionalBranch(condition, ref basicBlockBuilder2, false);
				this._currentSwitchOperationExpression = null;
			}
			else
			{
				foreach (ICaseClauseOperation caseClauseOperation in section.Clauses)
				{
					ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder3 = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block);
					this.<VisitSwitch>g__handleCase|137_2(caseClauseOperation, basicBlockBuilder, basicBlockBuilder3, ref A_2);
					this.AppendNewBlock(basicBlockBuilder3, true);
				}
				this.UnconditionalBranch(basicBlockBuilder2);
			}
			this.AppendNewBlock(basicBlockBuilder, true);
			this.VisitStatements(section.Body);
			this.UnconditionalBranch(A_2.@break);
			this.AppendNewBlock(basicBlockBuilder2, true);
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x000602BC File Offset: 0x0005E4BC
		[CompilerGenerated]
		private void <VisitSwitch>g__handleCase|137_2(ICaseClauseOperation caseClause, ControlFlowGraphBuilder.BasicBlockBuilder body, [Nullable(2)] [DisallowNull] ControlFlowGraphBuilder.BasicBlockBuilder nextCase, ref ControlFlowGraphBuilder.<>c__DisplayClass137_0 A_4)
		{
			ControlFlowGraphBuilder.<>c__DisplayClass137_1 CS$<>8__locals1;
			CS$<>8__locals1.nextCase = nextCase;
			CS$<>8__locals1.labeled = this.GetLabeledOrNewBlock(caseClause.Label);
			ControlFlowGraphBuilder.LinkBlocks(CS$<>8__locals1.labeled, body, ControlFlowBranchSemantics.Regular);
			switch (caseClause.CaseKind)
			{
			case CaseKind.SingleValue:
				this.<VisitSwitch>g__handleEqualityCheck|137_3(((ISingleValueCaseClauseOperation)caseClause).Value, ref A_4, ref CS$<>8__locals1);
				return;
			case CaseKind.Relational:
			{
				IRelationalCaseClauseOperation relationalCaseClauseOperation = (IRelationalCaseClauseOperation)caseClause;
				if (relationalCaseClauseOperation.Relation == BinaryOperatorKind.Equals)
				{
					this.<VisitSwitch>g__handleEqualityCheck|137_3(relationalCaseClauseOperation.Value, ref A_4, ref CS$<>8__locals1);
					return;
				}
				throw ExceptionUtilities.UnexpectedValue(relationalCaseClauseOperation.Relation);
			}
			case CaseKind.Default:
			{
				IDefaultCaseClauseOperation defaultCaseClauseOperation = (IDefaultCaseClauseOperation)caseClause;
				if (A_4.defaultBody == null)
				{
					A_4.defaultBody = CS$<>8__locals1.labeled;
				}
				this.UnconditionalBranch(CS$<>8__locals1.nextCase);
				this.AppendNewBlock(CS$<>8__locals1.labeled, true);
				this._currentBasicBlock = null;
				return;
			}
			case CaseKind.Pattern:
			{
				IPatternCaseClauseOperation patternCaseClauseOperation = (IPatternCaseClauseOperation)caseClause;
				ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
				this.PushOperand(OperationCloner.CloneOperation<IOperation>(A_4.switchValue));
				IPatternOperation patternOperation = (IPatternOperation)this.VisitRequired(patternCaseClauseOperation.Pattern, default(int?));
				CS$<>8__locals1.condition = new IsPatternOperation(this.PopOperand(), patternOperation, null, patternCaseClauseOperation.Pattern.Syntax, A_4.booleanType, true);
				this.ConditionalBranch(CS$<>8__locals1.condition, false, CS$<>8__locals1.nextCase);
				this.PopStackFrameAndLeaveRegion(evalStackFrame);
				if (patternCaseClauseOperation.Guard != null)
				{
					this.AppendNewBlock(new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Block), true);
					this.VisitConditionalBranch(patternCaseClauseOperation.Guard, ref CS$<>8__locals1.nextCase, false);
				}
				this.AppendNewBlock(CS$<>8__locals1.labeled, true);
				this._currentBasicBlock = null;
				return;
			}
			}
			throw ExceptionUtilities.UnexpectedValue(caseClause.CaseKind);
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x00060470 File Offset: 0x0005E670
		[CompilerGenerated]
		private void <VisitSwitch>g__handleEqualityCheck|137_3(IOperation compareWith, ref ControlFlowGraphBuilder.<>c__DisplayClass137_0 A_2, ref ControlFlowGraphBuilder.<>c__DisplayClass137_1 A_3)
		{
			bool flag = ITypeSymbolHelpers.IsNullableType(A_2.operation.Value.Type);
			bool flag2 = ITypeSymbolHelpers.IsNullableType(compareWith.Type);
			bool flag3 = flag || flag2;
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.PushOperand(OperationCloner.CloneOperation<IOperation>(A_2.switchValue));
			IOperation operation = this.VisitRequired(compareWith, default(int?));
			IOperation operation2 = this.PopOperand();
			if (flag3)
			{
				if (!flag)
				{
					if (operation2.Type != null)
					{
						operation2 = this.MakeNullable(operation2, compareWith.Type);
					}
				}
				else if (!flag2 && operation.Type != null)
				{
					operation = this.MakeNullable(operation, A_2.operation.Value.Type);
				}
			}
			A_3.condition = new BinaryOperation(BinaryOperatorKind.Equals, operation2, operation, flag3, false, false, null, null, null, compareWith.Syntax, A_2.booleanType, null, true);
			this.ConditionalBranch(A_3.condition, false, A_3.nextCase);
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
			this.AppendNewBlock(A_3.labeled, true);
			this._currentBasicBlock = null;
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x00060570 File Offset: 0x0005E770
		[CompilerGenerated]
		private void <HandleObjectOrCollectionInitializer>g__visitInitializer|165_0(IObjectOrCollectionInitializerOperation initializerOperation, IOperation initializedInstance)
		{
			ControlFlowGraphBuilder.ImplicitInstanceInfo currentImplicitInstance = this._currentImplicitInstance;
			this._currentImplicitInstance = new ControlFlowGraphBuilder.ImplicitInstanceInfo(initializedInstance);
			foreach (IOperation operation in initializerOperation.Initializers)
			{
				this.<HandleObjectOrCollectionInitializer>g__handleInitializer|165_1(operation);
			}
			this._currentImplicitInstance = currentImplicitInstance;
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x000605C0 File Offset: 0x0005E7C0
		[CompilerGenerated]
		private void <HandleObjectOrCollectionInitializer>g__handleInitializer|165_1(IOperation innerInitializer)
		{
			OperationKind kind = innerInitializer.Kind;
			if (kind == OperationKind.SimpleAssignment)
			{
				this.<HandleObjectOrCollectionInitializer>g__handleSimpleAssignment|165_2((ISimpleAssignmentOperation)innerInitializer);
				return;
			}
			if (kind == OperationKind.MemberInitializer)
			{
				this.<HandleObjectOrCollectionInitializer>g__handleMemberInitializer|165_3((IMemberInitializerOperation)innerInitializer);
				return;
			}
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			this.AddStatement(this.Visit(innerInitializer));
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x00060614 File Offset: 0x0005E814
		[CompilerGenerated]
		private void <HandleObjectOrCollectionInitializer>g__handleSimpleAssignment|165_2(ISimpleAssignmentOperation assignmentOperation)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			IOperation operation;
			if (!this.<HandleObjectOrCollectionInitializer>g__tryPushTarget|165_4(assignmentOperation.Target))
			{
				operation = this.VisitRequired(assignmentOperation, default(int?));
			}
			else
			{
				IOperation operation2 = this.VisitRequired(assignmentOperation.Value, default(int?));
				IOperation operation3 = this.<HandleObjectOrCollectionInitializer>g__popTarget|165_5(assignmentOperation.Target);
				operation = new SimpleAssignmentOperation(assignmentOperation.IsRef, operation3, operation2, null, assignmentOperation.Syntax, assignmentOperation.Type, assignmentOperation.GetConstantValue(), this.IsImplicit(assignmentOperation));
			}
			this.AddStatement(operation);
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x000606A4 File Offset: 0x0005E8A4
		[CompilerGenerated]
		private void <HandleObjectOrCollectionInitializer>g__handleMemberInitializer|165_3(IMemberInitializerOperation memberInitializer)
		{
			ControlFlowGraphBuilder.EvalStackFrame evalStackFrame = this.PushStackFrame();
			IOperation operation = (this.<HandleObjectOrCollectionInitializer>g__tryPushTarget|165_4(memberInitializer.InitializedMember) ? this.<HandleObjectOrCollectionInitializer>g__popTarget|165_5(memberInitializer.InitializedMember) : this.VisitRequired(memberInitializer.InitializedMember, default(int?)));
			this.<HandleObjectOrCollectionInitializer>g__visitInitializer|165_0(memberInitializer.Initializer, operation);
			this.PopStackFrameAndLeaveRegion(evalStackFrame);
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x00060700 File Offset: 0x0005E900
		[CompilerGenerated]
		private bool <HandleObjectOrCollectionInitializer>g__tryPushTarget|165_4(IOperation instance)
		{
			OperationKind kind = instance.Kind;
			if (kind <= OperationKind.EventReference)
			{
				if (kind == OperationKind.ArrayElementReference)
				{
					IArrayElementReferenceOperation arrayElementReferenceOperation = (IArrayElementReferenceOperation)instance;
					this.VisitAndPushArray<IOperation>(arrayElementReferenceOperation.Indices, null);
					this.SpillEvalStack();
					this.PushOperand(this.VisitRequired(arrayElementReferenceOperation.ArrayReference, default(int?)));
					return true;
				}
				switch (kind)
				{
				case OperationKind.FieldReference:
				case OperationKind.PropertyReference:
				case OperationKind.EventReference:
				{
					IMemberReferenceOperation memberReferenceOperation = (IMemberReferenceOperation)instance;
					if (memberReferenceOperation.Kind == OperationKind.PropertyReference)
					{
						this.VisitAndPushArray<IArgumentOperation>(((IPropertyReferenceOperation)memberReferenceOperation).Arguments, new Func<IArgumentOperation, IOperation>(ControlFlowGraphBuilder.UnwrapArgument));
						this.SpillEvalStack();
					}
					if (!memberReferenceOperation.Member.IsStatic && memberReferenceOperation.Instance != null)
					{
						this.PushOperand(this.VisitRequired(memberReferenceOperation.Instance, default(int?)));
					}
					return true;
				}
				}
			}
			else
			{
				if (kind == OperationKind.DynamicMemberReference)
				{
					IDynamicMemberReferenceOperation dynamicMemberReferenceOperation = (IDynamicMemberReferenceOperation)instance;
					if (dynamicMemberReferenceOperation.Instance != null)
					{
						this.PushOperand(this.VisitRequired(dynamicMemberReferenceOperation.Instance, default(int?)));
					}
					return true;
				}
				if (kind == OperationKind.DynamicIndexerAccess)
				{
					IDynamicIndexerAccessOperation dynamicIndexerAccessOperation = (IDynamicIndexerAccessOperation)instance;
					this.VisitAndPushArray<IOperation>(dynamicIndexerAccessOperation.Arguments, null);
					this.SpillEvalStack();
					this.PushOperand(this.VisitRequired(dynamicIndexerAccessOperation.Operation, default(int?)));
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x00060864 File Offset: 0x0005EA64
		[CompilerGenerated]
		private IOperation <HandleObjectOrCollectionInitializer>g__popTarget|165_5(IOperation originalTarget)
		{
			OperationKind kind = originalTarget.Kind;
			if (kind <= OperationKind.EventReference)
			{
				if (kind == OperationKind.ArrayElementReference)
				{
					IArrayElementReferenceOperation arrayElementReferenceOperation = (IArrayElementReferenceOperation)originalTarget;
					IOperation operation = this.PopOperand();
					ImmutableArray<IOperation> immutableArray = this.PopArray<IOperation>(arrayElementReferenceOperation.Indices, null);
					return new ArrayElementReferenceOperation(operation, immutableArray, null, originalTarget.Syntax, originalTarget.Type, this.IsImplicit(originalTarget));
				}
				switch (kind)
				{
				case OperationKind.FieldReference:
				{
					IFieldReferenceOperation fieldReferenceOperation = (IFieldReferenceOperation)originalTarget;
					IOperation operation = ((!fieldReferenceOperation.Member.IsStatic && fieldReferenceOperation.Instance != null) ? this.PopOperand() : null);
					return new FieldReferenceOperation(fieldReferenceOperation.Field, fieldReferenceOperation.IsDeclaration, operation, null, fieldReferenceOperation.Syntax, fieldReferenceOperation.Type, fieldReferenceOperation.GetConstantValue(), this.IsImplicit(fieldReferenceOperation));
				}
				case OperationKind.PropertyReference:
				{
					IPropertyReferenceOperation propertyReferenceOperation = (IPropertyReferenceOperation)originalTarget;
					IOperation operation = ((!propertyReferenceOperation.Member.IsStatic && propertyReferenceOperation.Instance != null) ? this.PopOperand() : null);
					ImmutableArray<IArgumentOperation> immutableArray2 = this.PopArray<IArgumentOperation>(propertyReferenceOperation.Arguments, new Func<IOperation, int, ImmutableArray<IArgumentOperation>, IArgumentOperation>(this.RewriteArgumentFromArray));
					return new PropertyReferenceOperation(propertyReferenceOperation.Property, immutableArray2, operation, null, propertyReferenceOperation.Syntax, propertyReferenceOperation.Type, this.IsImplicit(propertyReferenceOperation));
				}
				case OperationKind.EventReference:
				{
					IEventReferenceOperation eventReferenceOperation = (IEventReferenceOperation)originalTarget;
					IOperation operation = ((!eventReferenceOperation.Member.IsStatic && eventReferenceOperation.Instance != null) ? this.PopOperand() : null);
					return new EventReferenceOperation(eventReferenceOperation.Event, operation, null, eventReferenceOperation.Syntax, eventReferenceOperation.Type, this.IsImplicit(eventReferenceOperation));
				}
				}
			}
			else
			{
				if (kind == OperationKind.DynamicMemberReference)
				{
					IDynamicMemberReferenceOperation dynamicMemberReferenceOperation = (IDynamicMemberReferenceOperation)originalTarget;
					IOperation operation = ((dynamicMemberReferenceOperation.Instance != null) ? this.PopOperand() : null);
					return new DynamicMemberReferenceOperation(operation, dynamicMemberReferenceOperation.MemberName, dynamicMemberReferenceOperation.TypeArguments, dynamicMemberReferenceOperation.ContainingType, null, dynamicMemberReferenceOperation.Syntax, dynamicMemberReferenceOperation.Type, this.IsImplicit(dynamicMemberReferenceOperation));
				}
				if (kind == OperationKind.DynamicIndexerAccess)
				{
					DynamicIndexerAccessOperation dynamicIndexerAccessOperation = (DynamicIndexerAccessOperation)originalTarget;
					IOperation operation = this.PopOperand();
					ImmutableArray<IOperation> immutableArray3 = this.PopArray<IOperation>(dynamicIndexerAccessOperation.Arguments, null);
					return new DynamicIndexerAccessOperation(operation, immutableArray3, dynamicIndexerAccessOperation.ArgumentNames, dynamicIndexerAccessOperation.ArgumentRefKinds, null, dynamicIndexerAccessOperation.Syntax, dynamicIndexerAccessOperation.Type, this.IsImplicit(dynamicIndexerAccessOperation));
				}
			}
			throw ExceptionUtilities.UnexpectedValue(originalTarget.Kind);
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x00060AA8 File Offset: 0x0005ECA8
		[CompilerGenerated]
		private IOperation <VisitAnonymousObjectCreation>g__visitAndCaptureInitializer|168_0(IPropertySymbol initializedProperty, IOperation initializer)
		{
			this.PushOperand(this.VisitRequired(initializer, default(int?)));
			this.SpillEvalStack();
			IOperation operation = this.PeekOperand();
			this._currentImplicitInstance.AnonymousTypePropertyValues[initializedProperty] = operation;
			return operation;
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x00060AEC File Offset: 0x0005ECEC
		[CompilerGenerated]
		private void <VisitArrayInitializer>g__visitAndPushArrayInitializerValues|174_0(IArrayInitializerOperation initializer)
		{
			foreach (IOperation operation in initializer.ElementValues)
			{
				if (operation.Kind == OperationKind.ArrayInitializer)
				{
					this.<VisitArrayInitializer>g__visitAndPushArrayInitializerValues|174_0((IArrayInitializerOperation)operation);
				}
				else
				{
					this.PushOperand(this.VisitRequired(operation, default(int?)));
				}
			}
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x00060B4C File Offset: 0x0005ED4C
		[CompilerGenerated]
		private IArrayInitializerOperation <VisitArrayInitializer>g__popAndAssembleArrayInitializerValues|174_1(IArrayInitializerOperation initializer)
		{
			ArrayBuilder<IOperation> instance = ArrayBuilder<IOperation>.GetInstance(initializer.ElementValues.Length);
			for (int i = initializer.ElementValues.Length - 1; i >= 0; i--)
			{
				IOperation operation = initializer.ElementValues[i];
				IOperation operation2;
				if (operation.Kind == OperationKind.ArrayInitializer)
				{
					operation2 = this.<VisitArrayInitializer>g__popAndAssembleArrayInitializerValues|174_1((IArrayInitializerOperation)operation);
				}
				else
				{
					operation2 = this.PopOperand();
				}
				instance.Add(operation2);
			}
			instance.ReverseContents();
			return new ArrayInitializerOperation(instance.ToImmutableAndFree(), null, initializer.Syntax, this.IsImplicit(initializer));
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x00060BE4 File Offset: 0x0005EDE4
		[NullableContext(2)]
		[CompilerGenerated]
		internal static IEventReferenceOperation <VisitEventAssignment>g__getEventReference|210_0(ref ControlFlowGraphBuilder.<>c__DisplayClass210_0 A_0)
		{
			IOperation operation = A_0.operation.EventReference;
			for (;;)
			{
				OperationKind kind = operation.Kind;
				if (kind == OperationKind.EventReference)
				{
					goto IL_001F;
				}
				if (kind != OperationKind.Parenthesized)
				{
					break;
				}
				operation = ((IParenthesizedOperation)operation).Operand;
			}
			return null;
			IL_001F:
			return (IEventReferenceOperation)operation;
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x00060C26 File Offset: 0x0005EE26
		[CompilerGenerated]
		private IOperation <VisitInvalid>g__visitInvalidOperationStatement|221_0(IInvalidOperation invalidOperation, ref ControlFlowGraphBuilder.<>c__DisplayClass221_0 A_2)
		{
			this.VisitStatements(A_2.children.ToImmutableAndFree());
			return new InvalidOperation(ImmutableArray<IOperation>.Empty, null, invalidOperation.Syntax, invalidOperation.Type, invalidOperation.GetConstantValue(), this.IsImplicit(invalidOperation));
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x00060C60 File Offset: 0x0005EE60
		[CompilerGenerated]
		private IOperation <VisitInvalid>g__visitInvalidOperationExpression|221_1(IInvalidOperation invalidOperation, ref ControlFlowGraphBuilder.<>c__DisplayClass221_0 A_2)
		{
			return this.PopStackFrame<InvalidOperation>(this.PushStackFrame(), new InvalidOperation(this.VisitArray<IOperation>(A_2.children.ToImmutableAndFree(), null, null), null, invalidOperation.Syntax, invalidOperation.Type, invalidOperation.GetConstantValue(), this.IsImplicit(A_2.operation)));
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x00060CB0 File Offset: 0x0005EEB0
		[CompilerGenerated]
		private IReDimClauseOperation <VisitReDim>g__visitReDimClause|222_0(IReDimClauseOperation clause)
		{
			this.PushOperand(this.VisitRequired(clause.Operand, default(int?)));
			ImmutableArray<IOperation> immutableArray = this.VisitArray<IOperation>(clause.DimensionSizes, null, null);
			return new ReDimClauseOperation(this.PopOperand(), immutableArray, null, clause.Syntax, this.IsImplicit(clause));
		}

		// Token: 0x04000DB7 RID: 3511
		private readonly Compilation _compilation;

		// Token: 0x04000DB8 RID: 3512
		private readonly ControlFlowGraphBuilder.BasicBlockBuilder _entry = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Entry);

		// Token: 0x04000DB9 RID: 3513
		private readonly ControlFlowGraphBuilder.BasicBlockBuilder _exit = new ControlFlowGraphBuilder.BasicBlockBuilder(BasicBlockKind.Exit);

		// Token: 0x04000DBA RID: 3514
		private readonly ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder> _blocks;

		// Token: 0x04000DBB RID: 3515
		private readonly PooledDictionary<ControlFlowGraphBuilder.BasicBlockBuilder, ControlFlowGraphBuilder.RegionBuilder> _regionMap;

		// Token: 0x04000DBC RID: 3516
		[Nullable(2)]
		private ControlFlowGraphBuilder.BasicBlockBuilder _currentBasicBlock;

		// Token: 0x04000DBD RID: 3517
		[Nullable(2)]
		private ControlFlowGraphBuilder.RegionBuilder _currentRegion;

		// Token: 0x04000DBE RID: 3518
		[Nullable(new byte[] { 2, 1, 1 })]
		private PooledDictionary<ILabelSymbol, ControlFlowGraphBuilder.BasicBlockBuilder> _labeledBlocks;

		// Token: 0x04000DBF RID: 3519
		private bool _haveAnonymousFunction;

		// Token: 0x04000DC0 RID: 3520
		[Nullable(2)]
		private IOperation _currentStatement;

		// Token: 0x04000DC1 RID: 3521
		[TupleElementNames(new string[] { "frameOpt", "operationOpt" })]
		[Nullable(new byte[] { 1, 0, 2, 2 })]
		private readonly ArrayBuilder<ValueTuple<ControlFlowGraphBuilder.EvalStackFrame, IOperation>> _evalStack;

		// Token: 0x04000DC2 RID: 3522
		private int _startSpillingAt;

		// Token: 0x04000DC3 RID: 3523
		private ControlFlowGraphBuilder.ConditionalAccessOperationTracker _currentConditionalAccessTracker;

		// Token: 0x04000DC4 RID: 3524
		[Nullable(2)]
		private IOperation _currentSwitchOperationExpression;

		// Token: 0x04000DC5 RID: 3525
		[Nullable(2)]
		private IOperation _forToLoopBinaryOperatorLeftOperand;

		// Token: 0x04000DC6 RID: 3526
		[Nullable(2)]
		private IOperation _forToLoopBinaryOperatorRightOperand;

		// Token: 0x04000DC7 RID: 3527
		[Nullable(2)]
		private IOperation _currentAggregationGroup;

		// Token: 0x04000DC8 RID: 3528
		private bool _forceImplicit;

		// Token: 0x04000DC9 RID: 3529
		private readonly ControlFlowGraphBuilder.CaptureIdDispenser _captureIdDispenser;

		// Token: 0x04000DCA RID: 3530
		private ControlFlowGraphBuilder.ImplicitInstanceInfo _currentImplicitInstance;

		// Token: 0x02000660 RID: 1632
		[Nullable(0)]
		internal sealed class BasicBlockBuilder
		{
			// Token: 0x060038F1 RID: 14577 RVA: 0x0009E1AD File Offset: 0x0009C3AD
			public BasicBlockBuilder(BasicBlockKind kind)
			{
				this.Kind = kind;
				this.Ordinal = -1;
				this.IsReachable = false;
			}

			// Token: 0x17000FC7 RID: 4039
			// (get) Token: 0x060038F2 RID: 14578 RVA: 0x0009E1CA File Offset: 0x0009C3CA
			[MemberNotNullWhen(true, "StatementsOpt")]
			public bool HasStatements
			{
				[MemberNotNullWhen(true, "StatementsOpt")]
				get
				{
					ArrayBuilder<IOperation> statements = this._statements;
					return statements != null && statements.Count > 0;
				}
			}

			// Token: 0x17000FC8 RID: 4040
			// (get) Token: 0x060038F3 RID: 14579 RVA: 0x0009E1E0 File Offset: 0x0009C3E0
			[Nullable(new byte[] { 2, 1 })]
			public ArrayBuilder<IOperation> StatementsOpt
			{
				[return: Nullable(new byte[] { 2, 1 })]
				get
				{
					return this._statements;
				}
			}

			// Token: 0x060038F4 RID: 14580 RVA: 0x0009E1E8 File Offset: 0x0009C3E8
			public void AddStatement(IOperation operation)
			{
				if (this._statements == null)
				{
					this._statements = ArrayBuilder<IOperation>.GetInstance();
				}
				this._statements.Add(operation);
			}

			// Token: 0x060038F5 RID: 14581 RVA: 0x0009E20C File Offset: 0x0009C40C
			public void MoveStatementsFrom(ControlFlowGraphBuilder.BasicBlockBuilder other)
			{
				if (other._statements == null)
				{
					return;
				}
				if (this._statements == null)
				{
					this._statements = other._statements;
					other._statements = null;
					return;
				}
				this._statements.AddRange(other._statements);
				other._statements.Clear();
			}

			// Token: 0x060038F6 RID: 14582 RVA: 0x0009E25C File Offset: 0x0009C45C
			public BasicBlock ToImmutable()
			{
				BasicBlockKind kind = this.Kind;
				ArrayBuilder<IOperation> statements = this._statements;
				BasicBlock basicBlock = new BasicBlock(kind, (statements != null) ? statements.ToImmutableAndFree() : ImmutableArray<IOperation>.Empty, this.BranchValue, this.ConditionKind, this.Ordinal, this.IsReachable, this.Region);
				this._statements = null;
				return basicBlock;
			}

			// Token: 0x17000FC9 RID: 4041
			// (get) Token: 0x060038F7 RID: 14583 RVA: 0x0009E2AF File Offset: 0x0009C4AF
			public bool HasPredecessors
			{
				get
				{
					if (this._predecessors != null)
					{
						return this._predecessors.Count > 0;
					}
					return this._predecessor1 != null || this._predecessor2 != null;
				}
			}

			// Token: 0x17000FCA RID: 4042
			// (get) Token: 0x060038F8 RID: 14584 RVA: 0x0009E2DB File Offset: 0x0009C4DB
			[MemberNotNullWhen(true, "BranchValue")]
			public bool HasCondition
			{
				[MemberNotNullWhen(true, "BranchValue")]
				get
				{
					return this.ConditionKind > ControlFlowConditionKind.None;
				}
			}

			// Token: 0x060038F9 RID: 14585 RVA: 0x0009E2E6 File Offset: 0x0009C4E6
			[NullableContext(2)]
			public ControlFlowGraphBuilder.BasicBlockBuilder GetSingletonPredecessorOrDefault()
			{
				if (this._predecessors != null)
				{
					return this._predecessors.AsSingleton<ControlFlowGraphBuilder.BasicBlockBuilder>();
				}
				if (this._predecessor2 == null)
				{
					return this._predecessor1;
				}
				if (this._predecessor1 == null)
				{
					return this._predecessor2;
				}
				return null;
			}

			// Token: 0x060038FA RID: 14586 RVA: 0x0009E31C File Offset: 0x0009C51C
			public void AddPredecessor(ControlFlowGraphBuilder.BasicBlockBuilder predecessor)
			{
				if (this._predecessors != null)
				{
					this._predecessors.Add(predecessor);
					return;
				}
				if (this._predecessor1 == predecessor)
				{
					return;
				}
				if (this._predecessor2 == predecessor)
				{
					return;
				}
				if (this._predecessor1 == null)
				{
					this._predecessor1 = predecessor;
					return;
				}
				if (this._predecessor2 == null)
				{
					this._predecessor2 = predecessor;
					return;
				}
				this._predecessors = PooledHashSet<ControlFlowGraphBuilder.BasicBlockBuilder>.GetInstance();
				this._predecessors.Add(this._predecessor1);
				this._predecessors.Add(this._predecessor2);
				this._predecessors.Add(predecessor);
				this._predecessor1 = null;
				this._predecessor2 = null;
			}

			// Token: 0x060038FB RID: 14587 RVA: 0x0009E3BD File Offset: 0x0009C5BD
			public void RemovePredecessor(ControlFlowGraphBuilder.BasicBlockBuilder predecessor)
			{
				if (this._predecessors != null)
				{
					this._predecessors.Remove(predecessor);
					return;
				}
				if (this._predecessor1 == predecessor)
				{
					this._predecessor1 = null;
					return;
				}
				if (this._predecessor2 == predecessor)
				{
					this._predecessor2 = null;
				}
			}

			// Token: 0x060038FC RID: 14588 RVA: 0x0009E3F8 File Offset: 0x0009C5F8
			public void GetPredecessors(ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder> builder)
			{
				if (this._predecessors != null)
				{
					foreach (ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder in this._predecessors)
					{
						builder.Add(basicBlockBuilder);
					}
					return;
				}
				if (this._predecessor1 != null)
				{
					builder.Add(this._predecessor1);
				}
				if (this._predecessor2 != null)
				{
					builder.Add(this._predecessor2);
				}
			}

			// Token: 0x060038FD RID: 14589 RVA: 0x0009E47C File Offset: 0x0009C67C
			[return: Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<ControlFlowBranch> ConvertPredecessorsToBranches(ArrayBuilder<BasicBlock> blocks)
			{
				ControlFlowGraphBuilder.BasicBlockBuilder.<>c__DisplayClass28_0 CS$<>8__locals1;
				CS$<>8__locals1.blocks = blocks;
				if (!this.HasPredecessors)
				{
					PooledHashSet<ControlFlowGraphBuilder.BasicBlockBuilder> predecessors = this._predecessors;
					if (predecessors != null)
					{
						predecessors.Free();
					}
					this._predecessors = null;
					return ImmutableArray<ControlFlowBranch>.Empty;
				}
				CS$<>8__locals1.block = CS$<>8__locals1.blocks[this.Ordinal];
				PooledHashSet<ControlFlowGraphBuilder.BasicBlockBuilder> predecessors2 = this._predecessors;
				CS$<>8__locals1.branches = ArrayBuilder<ControlFlowBranch>.GetInstance((predecessors2 != null) ? predecessors2.Count : 2);
				if (this._predecessors != null)
				{
					foreach (ControlFlowGraphBuilder.BasicBlockBuilder basicBlockBuilder in this._predecessors)
					{
						ControlFlowGraphBuilder.BasicBlockBuilder.<ConvertPredecessorsToBranches>g__addBranches|28_1(basicBlockBuilder, ref CS$<>8__locals1);
					}
					this._predecessors.Free();
					this._predecessors = null;
				}
				else
				{
					if (this._predecessor1 != null)
					{
						ControlFlowGraphBuilder.BasicBlockBuilder.<ConvertPredecessorsToBranches>g__addBranches|28_1(this._predecessor1, ref CS$<>8__locals1);
						this._predecessor1 = null;
					}
					if (this._predecessor2 != null)
					{
						ControlFlowGraphBuilder.BasicBlockBuilder.<ConvertPredecessorsToBranches>g__addBranches|28_1(this._predecessor2, ref CS$<>8__locals1);
						this._predecessor2 = null;
					}
				}
				CS$<>8__locals1.branches.Sort(delegate(ControlFlowBranch x, ControlFlowBranch y)
				{
					int num = x.Source.Ordinal - y.Source.Ordinal;
					if (num == 0 && x.IsConditionalSuccessor != y.IsConditionalSuccessor)
					{
						if (x.IsConditionalSuccessor)
						{
							num = -1;
						}
						else
						{
							num = 1;
						}
					}
					return num;
				});
				return CS$<>8__locals1.branches.ToImmutableAndFree();
			}

			// Token: 0x060038FE RID: 14590 RVA: 0x0009E5BC File Offset: 0x0009C7BC
			public void Free()
			{
				this.Ordinal = -1;
				ArrayBuilder<IOperation> statements = this._statements;
				if (statements != null)
				{
					statements.Free();
				}
				this._statements = null;
				PooledHashSet<ControlFlowGraphBuilder.BasicBlockBuilder> predecessors = this._predecessors;
				if (predecessors != null)
				{
					predecessors.Free();
				}
				this._predecessors = null;
				this._predecessor1 = null;
				this._predecessor2 = null;
			}

			// Token: 0x060038FF RID: 14591 RVA: 0x0009E610 File Offset: 0x0009C810
			[CompilerGenerated]
			internal static void <ConvertPredecessorsToBranches>g__addBranches|28_1(ControlFlowGraphBuilder.BasicBlockBuilder predecessorBlockBuilder, ref ControlFlowGraphBuilder.BasicBlockBuilder.<>c__DisplayClass28_0 A_1)
			{
				BasicBlock basicBlock = A_1.blocks[predecessorBlockBuilder.Ordinal];
				if (basicBlock.FallThroughSuccessor.Destination == A_1.block)
				{
					A_1.branches.Add(basicBlock.FallThroughSuccessor);
				}
				ControlFlowBranch conditionalSuccessor = basicBlock.ConditionalSuccessor;
				if (((conditionalSuccessor != null) ? conditionalSuccessor.Destination : null) == A_1.block)
				{
					A_1.branches.Add(basicBlock.ConditionalSuccessor);
				}
			}

			// Token: 0x04001878 RID: 6264
			public int Ordinal;

			// Token: 0x04001879 RID: 6265
			public readonly BasicBlockKind Kind;

			// Token: 0x0400187A RID: 6266
			[Nullable(new byte[] { 2, 1 })]
			private ArrayBuilder<IOperation> _statements;

			// Token: 0x0400187B RID: 6267
			[Nullable(2)]
			private ControlFlowGraphBuilder.BasicBlockBuilder _predecessor1;

			// Token: 0x0400187C RID: 6268
			[Nullable(2)]
			private ControlFlowGraphBuilder.BasicBlockBuilder _predecessor2;

			// Token: 0x0400187D RID: 6269
			[Nullable(new byte[] { 2, 1 })]
			private PooledHashSet<ControlFlowGraphBuilder.BasicBlockBuilder> _predecessors;

			// Token: 0x0400187E RID: 6270
			[Nullable(2)]
			public IOperation BranchValue;

			// Token: 0x0400187F RID: 6271
			public ControlFlowConditionKind ConditionKind;

			// Token: 0x04001880 RID: 6272
			public ControlFlowGraphBuilder.BasicBlockBuilder.Branch Conditional;

			// Token: 0x04001881 RID: 6273
			public ControlFlowGraphBuilder.BasicBlockBuilder.Branch FallThrough;

			// Token: 0x04001882 RID: 6274
			public bool IsReachable;

			// Token: 0x04001883 RID: 6275
			[Nullable(2)]
			public ControlFlowRegion Region;

			// Token: 0x020007D1 RID: 2001
			[NullableContext(2)]
			[Nullable(0)]
			internal struct Branch
			{
				// Token: 0x1700118F RID: 4495
				// (get) Token: 0x0600400B RID: 16395 RVA: 0x000B2C2D File Offset: 0x000B0E2D
				// (set) Token: 0x0600400C RID: 16396 RVA: 0x000B2C35 File Offset: 0x000B0E35
				public ControlFlowBranchSemantics Kind { readonly get; set; }

				// Token: 0x17001190 RID: 4496
				// (get) Token: 0x0600400D RID: 16397 RVA: 0x000B2C3E File Offset: 0x000B0E3E
				// (set) Token: 0x0600400E RID: 16398 RVA: 0x000B2C46 File Offset: 0x000B0E46
				public ControlFlowGraphBuilder.BasicBlockBuilder Destination { readonly get; set; }
			}
		}

		// Token: 0x02000661 RID: 1633
		[NullableContext(0)]
		internal class CaptureIdDispenser
		{
			// Token: 0x06003900 RID: 14592 RVA: 0x0009E67E File Offset: 0x0009C87E
			public int GetNextId()
			{
				return Interlocked.Increment(ref this._captureId);
			}

			// Token: 0x06003901 RID: 14593 RVA: 0x0009E68B File Offset: 0x0009C88B
			public int GetCurrentId()
			{
				return this._captureId;
			}

			// Token: 0x04001884 RID: 6276
			private int _captureId = -1;
		}

		// Token: 0x02000662 RID: 1634
		[NullableContext(0)]
		private readonly struct ConditionalAccessOperationTracker
		{
			// Token: 0x06003903 RID: 14595 RVA: 0x0009E6A2 File Offset: 0x0009C8A2
			[NullableContext(1)]
			public ConditionalAccessOperationTracker(ArrayBuilder<IOperation> operations, ControlFlowGraphBuilder.BasicBlockBuilder whenNull)
			{
				this.Operations = operations;
				this.WhenNull = whenNull;
			}

			// Token: 0x17000FCB RID: 4043
			// (get) Token: 0x06003904 RID: 14596 RVA: 0x0009E6B2 File Offset: 0x0009C8B2
			[MemberNotNullWhen(false, new string[] { "Operations", "WhenNull" })]
			public bool IsDefault
			{
				[MemberNotNullWhen(false, new string[] { "Operations", "WhenNull" })]
				get
				{
					return this.Operations == null;
				}
			}

			// Token: 0x06003905 RID: 14597 RVA: 0x0009E6BD File Offset: 0x0009C8BD
			public void Free()
			{
				ArrayBuilder<IOperation> operations = this.Operations;
				if (operations == null)
				{
					return;
				}
				operations.Free();
			}

			// Token: 0x04001885 RID: 6277
			[Nullable(new byte[] { 2, 1 })]
			public readonly ArrayBuilder<IOperation> Operations;

			// Token: 0x04001886 RID: 6278
			[Nullable(2)]
			public readonly ControlFlowGraphBuilder.BasicBlockBuilder WhenNull;
		}

		// Token: 0x02000663 RID: 1635
		[NullableContext(2)]
		[Nullable(0)]
		internal struct Context
		{
			// Token: 0x06003906 RID: 14598 RVA: 0x0009E6CF File Offset: 0x0009C8CF
			internal Context(IOperation implicitInstance, INamedTypeSymbol anonymousType, [Nullable(new byte[] { 0, 0, 1, 1 })] ImmutableArray<KeyValuePair<IPropertySymbol, IOperation>> anonymousTypePropertyValues)
			{
				this.ImplicitInstance = implicitInstance;
				this.AnonymousType = anonymousType;
				this.AnonymousTypePropertyValues = anonymousTypePropertyValues;
			}

			// Token: 0x04001887 RID: 6279
			public readonly IOperation ImplicitInstance;

			// Token: 0x04001888 RID: 6280
			public readonly INamedTypeSymbol AnonymousType;

			// Token: 0x04001889 RID: 6281
			[Nullable(new byte[] { 0, 0, 1, 1 })]
			public readonly ImmutableArray<KeyValuePair<IPropertySymbol, IOperation>> AnonymousTypePropertyValues;
		}

		// Token: 0x02000664 RID: 1636
		[NullableContext(2)]
		[Nullable(0)]
		private class EvalStackFrame
		{
			// Token: 0x17000FCC RID: 4044
			// (get) Token: 0x06003907 RID: 14599 RVA: 0x0009E6E6 File Offset: 0x0009C8E6
			// (set) Token: 0x06003908 RID: 14600 RVA: 0x0009E6EE File Offset: 0x0009C8EE
			public ControlFlowGraphBuilder.RegionBuilder RegionBuilderOpt
			{
				get
				{
					return this._lazyRegionBuilder;
				}
				set
				{
					this._lazyRegionBuilder = value;
				}
			}

			// Token: 0x0400188A RID: 6282
			private ControlFlowGraphBuilder.RegionBuilder _lazyRegionBuilder;
		}

		// Token: 0x02000665 RID: 1637
		[NullableContext(2)]
		[Nullable(0)]
		private struct ImplicitInstanceInfo
		{
			// Token: 0x17000FCD RID: 4045
			// (get) Token: 0x0600390A RID: 14602 RVA: 0x0009E6FF File Offset: 0x0009C8FF
			public readonly IOperation ImplicitInstance { get; }

			// Token: 0x17000FCE RID: 4046
			// (get) Token: 0x0600390B RID: 14603 RVA: 0x0009E707 File Offset: 0x0009C907
			public readonly INamedTypeSymbol AnonymousType { get; }

			// Token: 0x17000FCF RID: 4047
			// (get) Token: 0x0600390C RID: 14604 RVA: 0x0009E70F File Offset: 0x0009C90F
			[Nullable(new byte[] { 2, 1, 1 })]
			public readonly PooledDictionary<IPropertySymbol, IOperation> AnonymousTypePropertyValues
			{
				[return: Nullable(new byte[] { 2, 1, 1 })]
				get;
			}

			// Token: 0x0600390D RID: 14605 RVA: 0x0009E717 File Offset: 0x0009C917
			[NullableContext(1)]
			public ImplicitInstanceInfo(IOperation currentImplicitInstance)
			{
				this.ImplicitInstance = currentImplicitInstance;
				this.AnonymousType = null;
				this.AnonymousTypePropertyValues = null;
			}

			// Token: 0x0600390E RID: 14606 RVA: 0x0009E72E File Offset: 0x0009C92E
			[NullableContext(1)]
			public ImplicitInstanceInfo(INamedTypeSymbol currentInitializedAnonymousType)
			{
				this.ImplicitInstance = null;
				this.AnonymousType = currentInitializedAnonymousType;
				this.AnonymousTypePropertyValues = PooledDictionary<IPropertySymbol, IOperation>.GetInstance();
			}

			// Token: 0x0600390F RID: 14607 RVA: 0x0009E74C File Offset: 0x0009C94C
			public ImplicitInstanceInfo(in ControlFlowGraphBuilder.Context context)
			{
				if (context.ImplicitInstance != null)
				{
					this.ImplicitInstance = context.ImplicitInstance;
					this.AnonymousType = null;
					this.AnonymousTypePropertyValues = null;
					return;
				}
				if (context.AnonymousType != null)
				{
					this.ImplicitInstance = null;
					this.AnonymousType = context.AnonymousType;
					this.AnonymousTypePropertyValues = PooledDictionary<IPropertySymbol, IOperation>.GetInstance();
					foreach (KeyValuePair<IPropertySymbol, IOperation> keyValuePair in context.AnonymousTypePropertyValues)
					{
						this.AnonymousTypePropertyValues.Add(keyValuePair.Key, keyValuePair.Value);
					}
					return;
				}
				this.ImplicitInstance = null;
				this.AnonymousType = null;
				this.AnonymousTypePropertyValues = null;
			}

			// Token: 0x06003910 RID: 14608 RVA: 0x0009E7F3 File Offset: 0x0009C9F3
			public void Free()
			{
				PooledDictionary<IPropertySymbol, IOperation> anonymousTypePropertyValues = this.AnonymousTypePropertyValues;
				if (anonymousTypePropertyValues == null)
				{
					return;
				}
				anonymousTypePropertyValues.Free();
			}
		}

		// Token: 0x02000666 RID: 1638
		[NullableContext(2)]
		[Nullable(0)]
		private class RegionBuilder
		{
			// Token: 0x17000FD0 RID: 4048
			// (get) Token: 0x06003911 RID: 14609 RVA: 0x0009E805 File Offset: 0x0009CA05
			// (set) Token: 0x06003912 RID: 14610 RVA: 0x0009E80D File Offset: 0x0009CA0D
			public ControlFlowGraphBuilder.RegionBuilder Enclosing { get; private set; }

			// Token: 0x06003913 RID: 14611 RVA: 0x0009E816 File Offset: 0x0009CA16
			public RegionBuilder(ControlFlowRegionKind kind, ITypeSymbol exceptionType = null, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals = default(ImmutableArray<ILocalSymbol>), bool isStackSpillRegion = false)
			{
				this.Kind = kind;
				this.ExceptionType = exceptionType;
				this.Locals = locals.NullToEmpty<ILocalSymbol>();
				this.IsStackSpillRegion = isStackSpillRegion;
			}

			// Token: 0x17000FD1 RID: 4049
			// (get) Token: 0x06003914 RID: 14612 RVA: 0x0009E840 File Offset: 0x0009CA40
			[MemberNotNullWhen(false, new string[] { "FirstBlock", "LastBlock" })]
			public bool IsEmpty
			{
				[MemberNotNullWhen(false, new string[] { "FirstBlock", "LastBlock" })]
				get
				{
					return this.FirstBlock == null;
				}
			}

			// Token: 0x17000FD2 RID: 4050
			// (get) Token: 0x06003915 RID: 14613 RVA: 0x0009E84B File Offset: 0x0009CA4B
			[MemberNotNullWhen(true, "Regions")]
			public bool HasRegions
			{
				[MemberNotNullWhen(true, "Regions")]
				get
				{
					ArrayBuilder<ControlFlowGraphBuilder.RegionBuilder> regions = this.Regions;
					return regions != null && regions.Count > 0;
				}
			}

			// Token: 0x17000FD3 RID: 4051
			// (get) Token: 0x06003916 RID: 14614 RVA: 0x0009E861 File Offset: 0x0009CA61
			[MemberNotNullWhen(true, "LocalFunctions")]
			public bool HasLocalFunctions
			{
				[MemberNotNullWhen(true, "LocalFunctions")]
				get
				{
					ArrayBuilder<ValueTuple<IMethodSymbol, ILocalFunctionOperation>> localFunctions = this.LocalFunctions;
					return localFunctions != null && localFunctions.Count > 0;
				}
			}

			// Token: 0x17000FD4 RID: 4052
			// (get) Token: 0x06003917 RID: 14615 RVA: 0x0009E877 File Offset: 0x0009CA77
			[MemberNotNullWhen(true, "CaptureIds")]
			public bool HasCaptureIds
			{
				[MemberNotNullWhen(true, "CaptureIds")]
				get
				{
					ArrayBuilder<CaptureId> captureIds = this.CaptureIds;
					return captureIds != null && captureIds.Count > 0;
				}
			}

			// Token: 0x06003918 RID: 14616 RVA: 0x0009E88D File Offset: 0x0009CA8D
			[MemberNotNull("CaptureIds")]
			public void AddCaptureId(int captureId)
			{
				if (this.CaptureIds == null)
				{
					this.CaptureIds = ArrayBuilder<CaptureId>.GetInstance();
				}
				this.CaptureIds.Add(new CaptureId(captureId));
			}

			// Token: 0x06003919 RID: 14617 RVA: 0x0009E8B3 File Offset: 0x0009CAB3
			public void AddCaptureIds(ArrayBuilder<CaptureId> others)
			{
				if (others == null)
				{
					return;
				}
				if (this.CaptureIds == null)
				{
					this.CaptureIds = ArrayBuilder<CaptureId>.GetInstance();
				}
				this.CaptureIds.AddRange(others);
			}

			// Token: 0x0600391A RID: 14618 RVA: 0x0009E8D8 File Offset: 0x0009CAD8
			[NullableContext(1)]
			[MemberNotNull("LocalFunctions")]
			public void Add(IMethodSymbol symbol, ILocalFunctionOperation operation)
			{
				if (this.LocalFunctions == null)
				{
					this.LocalFunctions = ArrayBuilder<ValueTuple<IMethodSymbol, ILocalFunctionOperation>>.GetInstance();
				}
				this.LocalFunctions.Add(new ValueTuple<IMethodSymbol, ILocalFunctionOperation>(symbol, operation));
			}

			// Token: 0x0600391B RID: 14619 RVA: 0x0009E8FF File Offset: 0x0009CAFF
			public void AddRange([Nullable(new byte[] { 2, 0, 1, 1 })] ArrayBuilder<ValueTuple<IMethodSymbol, ILocalFunctionOperation>> others)
			{
				if (others == null)
				{
					return;
				}
				if (this.LocalFunctions == null)
				{
					this.LocalFunctions = ArrayBuilder<ValueTuple<IMethodSymbol, ILocalFunctionOperation>>.GetInstance();
				}
				this.LocalFunctions.AddRange(others);
			}

			// Token: 0x0600391C RID: 14620 RVA: 0x0009E924 File Offset: 0x0009CB24
			[NullableContext(1)]
			[MemberNotNull("Regions")]
			public void Add(ControlFlowGraphBuilder.RegionBuilder region)
			{
				if (this.Regions == null)
				{
					this.Regions = ArrayBuilder<ControlFlowGraphBuilder.RegionBuilder>.GetInstance();
				}
				region.Enclosing = this;
				this.Regions.Add(region);
			}

			// Token: 0x0600391D RID: 14621 RVA: 0x0009E94C File Offset: 0x0009CB4C
			[NullableContext(1)]
			public void Remove(ControlFlowGraphBuilder.RegionBuilder region)
			{
				if (this.Regions.Count == 1)
				{
					this.Regions.Clear();
				}
				else
				{
					this.Regions.RemoveAt(this.Regions.IndexOf(region));
				}
				region.Enclosing = null;
			}

			// Token: 0x0600391E RID: 14622 RVA: 0x0009E988 File Offset: 0x0009CB88
			[NullableContext(1)]
			public void ReplaceRegion(ControlFlowGraphBuilder.RegionBuilder toReplace, ArrayBuilder<ControlFlowGraphBuilder.RegionBuilder> replaceWith)
			{
				int num;
				if (this.Regions.Count == 1)
				{
					num = 0;
				}
				else
				{
					num = this.Regions.IndexOf(toReplace);
				}
				int count = replaceWith.Count;
				if (count == 1)
				{
					ControlFlowGraphBuilder.RegionBuilder regionBuilder = replaceWith[0];
					regionBuilder.Enclosing = this;
					this.Regions[num] = regionBuilder;
				}
				else
				{
					int count2 = this.Regions.Count;
					this.Regions.Count = count - 1 + count2;
					int i = count2 - 1;
					int num2 = this.Regions.Count - 1;
					while (i > num)
					{
						this.Regions[num2] = this.Regions[i];
						i--;
						num2--;
					}
					foreach (ControlFlowGraphBuilder.RegionBuilder regionBuilder2 in replaceWith)
					{
						regionBuilder2.Enclosing = this;
						this.Regions[num++] = regionBuilder2;
					}
				}
				toReplace.Enclosing = null;
			}

			// Token: 0x0600391F RID: 14623 RVA: 0x0009EA7A File Offset: 0x0009CC7A
			[NullableContext(1)]
			[MemberNotNull(new string[] { "FirstBlock", "LastBlock" })]
			public void ExtendToInclude(ControlFlowGraphBuilder.BasicBlockBuilder block)
			{
				if (this.FirstBlock == null)
				{
					if (!this.HasRegions)
					{
						this.FirstBlock = block;
						this.LastBlock = block;
						return;
					}
					this.FirstBlock = this.Regions.First().FirstBlock;
				}
				this.LastBlock = block;
			}

			// Token: 0x06003920 RID: 14624 RVA: 0x0009EAB8 File Offset: 0x0009CCB8
			public void Free()
			{
				this.Enclosing = null;
				this.FirstBlock = null;
				this.LastBlock = null;
				ArrayBuilder<ControlFlowGraphBuilder.RegionBuilder> regions = this.Regions;
				if (regions != null)
				{
					regions.Free();
				}
				this.Regions = null;
				ArrayBuilder<ValueTuple<IMethodSymbol, ILocalFunctionOperation>> localFunctions = this.LocalFunctions;
				if (localFunctions != null)
				{
					localFunctions.Free();
				}
				this.LocalFunctions = null;
				ArrayBuilder<CaptureId> captureIds = this.CaptureIds;
				if (captureIds != null)
				{
					captureIds.Free();
				}
				this.CaptureIds = null;
			}

			// Token: 0x06003921 RID: 14625 RVA: 0x0009EB24 File Offset: 0x0009CD24
			[NullableContext(1)]
			public ControlFlowRegion ToImmutableRegionAndFree(ArrayBuilder<ControlFlowGraphBuilder.BasicBlockBuilder> blocks, ArrayBuilder<IMethodSymbol> localFunctions, [TupleElementNames(new string[] { "region", "operation", "ordinal" })] [Nullable(new byte[] { 1, 1, 0, 1, 1 })] ImmutableDictionary<IMethodSymbol, ValueTuple<ControlFlowRegion, ILocalFunctionOperation, int>>.Builder localFunctionsMap, [TupleElementNames(new string[] { "region", "ordinal" })] [Nullable(new byte[] { 2, 1, 0, 1 })] ImmutableDictionary<IFlowAnonymousFunctionOperation, ValueTuple<ControlFlowRegion, int>>.Builder anonymousFunctionsMapOpt, [Nullable(2)] ControlFlowRegion enclosing)
			{
				ControlFlowGraphBuilder.RegionBuilder.<>c__DisplayClass31_0 CS$<>8__locals1;
				CS$<>8__locals1.anonymousFunctionsMapOpt = anonymousFunctionsMapOpt;
				int count = localFunctions.Count;
				if (this.HasLocalFunctions)
				{
					foreach (ValueTuple<IMethodSymbol, ILocalFunctionOperation> valueTuple in this.LocalFunctions)
					{
						IMethodSymbol item = valueTuple.Item1;
						localFunctions.Add(item);
					}
				}
				ImmutableArray<ControlFlowRegion> immutableArray;
				if (this.HasRegions)
				{
					ArrayBuilder<ControlFlowRegion> instance = ArrayBuilder<ControlFlowRegion>.GetInstance(this.Regions.Count);
					foreach (ControlFlowGraphBuilder.RegionBuilder regionBuilder in this.Regions)
					{
						instance.Add(regionBuilder.ToImmutableRegionAndFree(blocks, localFunctions, localFunctionsMap, CS$<>8__locals1.anonymousFunctionsMapOpt, null));
					}
					immutableArray = instance.ToImmutableAndFree();
				}
				else
				{
					immutableArray = ImmutableArray<ControlFlowRegion>.Empty;
				}
				ArrayBuilder<CaptureId> captureIds = this.CaptureIds;
				if (captureIds != null)
				{
					captureIds.Sort((CaptureId x, CaptureId y) => x.Value.CompareTo(y.Value));
				}
				ControlFlowRegionKind kind = this.Kind;
				int ordinal = this.FirstBlock.Ordinal;
				int ordinal2 = this.LastBlock.Ordinal;
				ImmutableArray<ControlFlowRegion> immutableArray2 = immutableArray;
				ImmutableArray<ILocalSymbol> locals = this.Locals;
				ArrayBuilder<ValueTuple<IMethodSymbol, ILocalFunctionOperation>> localFunctions2 = this.LocalFunctions;
				ImmutableArray<IMethodSymbol> immutableArray3;
				if (localFunctions2 == null)
				{
					immutableArray3 = default(ImmutableArray<IMethodSymbol>);
				}
				else
				{
					immutableArray3 = localFunctions2.SelectAsArray<ValueTuple<IMethodSymbol, ILocalFunctionOperation>, IMethodSymbol>(([Nullable(new byte[] { 0, 1, 1 })] ValueTuple<IMethodSymbol, ILocalFunctionOperation> tuple) => tuple.Item1);
				}
				ArrayBuilder<CaptureId> captureIds2 = this.CaptureIds;
				CS$<>8__locals1.result = new ControlFlowRegion(kind, ordinal, ordinal2, immutableArray2, locals, immutableArray3, (captureIds2 != null) ? captureIds2.ToImmutable() : default(ImmutableArray<CaptureId>), this.ExceptionType, enclosing);
				if (this.HasLocalFunctions)
				{
					foreach (ValueTuple<IMethodSymbol, ILocalFunctionOperation> valueTuple2 in this.LocalFunctions)
					{
						IMethodSymbol item2 = valueTuple2.Item1;
						ILocalFunctionOperation item3 = valueTuple2.Item2;
						localFunctionsMap.Add(item2, new ValueTuple<ControlFlowRegion, ILocalFunctionOperation, int>(CS$<>8__locals1.result, item3, count++));
					}
				}
				int num = this.FirstBlock.Ordinal;
				foreach (ControlFlowRegion controlFlowRegion in immutableArray)
				{
					for (int i = num; i < controlFlowRegion.FirstBlockOrdinal; i++)
					{
						ControlFlowGraphBuilder.RegionBuilder.<ToImmutableRegionAndFree>g__setRegion|31_2(blocks[i], ref CS$<>8__locals1);
					}
					num = controlFlowRegion.LastBlockOrdinal + 1;
				}
				for (int j = num; j <= this.LastBlock.Ordinal; j++)
				{
					ControlFlowGraphBuilder.RegionBuilder.<ToImmutableRegionAndFree>g__setRegion|31_2(blocks[j], ref CS$<>8__locals1);
				}
				this.Free();
				return CS$<>8__locals1.result;
			}

			// Token: 0x06003922 RID: 14626 RVA: 0x0009ED74 File Offset: 0x0009CF74
			[NullableContext(1)]
			[CompilerGenerated]
			internal static void <ToImmutableRegionAndFree>g__setRegion|31_2(ControlFlowGraphBuilder.BasicBlockBuilder block, ref ControlFlowGraphBuilder.RegionBuilder.<>c__DisplayClass31_0 A_1)
			{
				block.Region = A_1.result;
				if (A_1.anonymousFunctionsMapOpt != null)
				{
					ValueTuple<ImmutableDictionary<IFlowAnonymousFunctionOperation, ValueTuple<ControlFlowRegion, int>>.Builder, ControlFlowRegion> valueTuple = new ValueTuple<ImmutableDictionary<IFlowAnonymousFunctionOperation, ValueTuple<ControlFlowRegion, int>>.Builder, ControlFlowRegion>(A_1.anonymousFunctionsMapOpt, A_1.result);
					if (block.HasStatements)
					{
						foreach (IOperation operation in block.StatementsOpt)
						{
							ControlFlowGraphBuilder.RegionBuilder.AnonymousFunctionsMapBuilder.Instance.Visit(operation, valueTuple);
						}
					}
					ControlFlowGraphBuilder.RegionBuilder.AnonymousFunctionsMapBuilder.Instance.Visit(block.BranchValue, valueTuple);
				}
			}

			// Token: 0x0400188E RID: 6286
			public ControlFlowRegionKind Kind;

			// Token: 0x04001890 RID: 6288
			public readonly ITypeSymbol ExceptionType;

			// Token: 0x04001891 RID: 6289
			public ControlFlowGraphBuilder.BasicBlockBuilder FirstBlock;

			// Token: 0x04001892 RID: 6290
			public ControlFlowGraphBuilder.BasicBlockBuilder LastBlock;

			// Token: 0x04001893 RID: 6291
			[Nullable(new byte[] { 2, 1 })]
			public ArrayBuilder<ControlFlowGraphBuilder.RegionBuilder> Regions;

			// Token: 0x04001894 RID: 6292
			[Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<ILocalSymbol> Locals;

			// Token: 0x04001895 RID: 6293
			[Nullable(new byte[] { 2, 0, 1, 1 })]
			public ArrayBuilder<ValueTuple<IMethodSymbol, ILocalFunctionOperation>> LocalFunctions;

			// Token: 0x04001896 RID: 6294
			public ArrayBuilder<CaptureId> CaptureIds;

			// Token: 0x04001897 RID: 6295
			public readonly bool IsStackSpillRegion;

			// Token: 0x020007D4 RID: 2004
			[TupleElementNames(new string[] { "map", "region", "region", "ordinal" })]
			[NullableContext(1)]
			[Nullable(new byte[] { 0, 0, 1, 1, 0, 1, 1, 1 })]
			private sealed class AnonymousFunctionsMapBuilder : OperationVisitor<ValueTuple<ImmutableDictionary<IFlowAnonymousFunctionOperation, ValueTuple<ControlFlowRegion, int>>.Builder, ControlFlowRegion>, IOperation>
			{
				// Token: 0x06004012 RID: 16402 RVA: 0x000B2CA9 File Offset: 0x000B0EA9
				[return: Nullable(2)]
				public override IOperation VisitFlowAnonymousFunction(IFlowAnonymousFunctionOperation operation, [TupleElementNames(new string[] { "map", "region", "region", "ordinal" })] [Nullable(new byte[] { 0, 1, 1, 0, 1, 1 })] ValueTuple<ImmutableDictionary<IFlowAnonymousFunctionOperation, ValueTuple<ControlFlowRegion, int>>.Builder, ControlFlowRegion> argument)
				{
					argument.Item1.Add(operation, new ValueTuple<ControlFlowRegion, int>(argument.Item2, argument.Item1.Count));
					return base.VisitFlowAnonymousFunction(operation, argument);
				}

				// Token: 0x06004013 RID: 16403 RVA: 0x000B2CD5 File Offset: 0x000B0ED5
				[return: Nullable(2)]
				internal override IOperation VisitNoneOperation(IOperation operation, [TupleElementNames(new string[] { "map", "region", "region", "ordinal" })] [Nullable(new byte[] { 0, 1, 1, 0, 1, 1 })] ValueTuple<ImmutableDictionary<IFlowAnonymousFunctionOperation, ValueTuple<ControlFlowRegion, int>>.Builder, ControlFlowRegion> argument)
				{
					return this.DefaultVisit(operation, argument);
				}

				// Token: 0x06004014 RID: 16404 RVA: 0x000B2CE0 File Offset: 0x000B0EE0
				[return: Nullable(2)]
				public override IOperation DefaultVisit(IOperation operation, [TupleElementNames(new string[] { "map", "region", "region", "ordinal" })] [Nullable(new byte[] { 0, 1, 1, 0, 1, 1 })] ValueTuple<ImmutableDictionary<IFlowAnonymousFunctionOperation, ValueTuple<ControlFlowRegion, int>>.Builder, ControlFlowRegion> argument)
				{
					foreach (IOperation operation2 in ((Operation)operation).ChildOperations)
					{
						this.Visit(operation2, argument);
					}
					return null;
				}

				// Token: 0x04001E0B RID: 7691
				public static readonly ControlFlowGraphBuilder.RegionBuilder.AnonymousFunctionsMapBuilder Instance = new ControlFlowGraphBuilder.RegionBuilder.AnonymousFunctionsMapBuilder();
			}
		}
	}
}
