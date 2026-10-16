using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Debugging;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004BF RID: 1215
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal sealed class ILBuilder
	{
		// Token: 0x060030AF RID: 12463 RVA: 0x0008A830 File Offset: 0x00088A30
		internal ILBuilder(ITokenDeferral module, LocalSlotManager localSlotManager, OptimizationLevel optimizations, bool areLocalsZeroed)
		{
			this.module = module;
			this.LocalSlotManager = localSlotManager;
			this._emitState = default(ILBuilder.EmitState);
			this._scopeManager = new ILBuilder.LocalScopeManager();
			this.leaderBlock = (this._currentBlock = this._scopeManager.CreateBlock(this));
			this._labelInfos = new SmallDictionary<object, ILBuilder.LabelInfo>(ReferenceEqualityComparer.Instance);
			this._optimizations = optimizations;
			this._areLocalsZeroed = areLocalsZeroed;
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x060030B0 RID: 12464 RVA: 0x0008A8B0 File Offset: 0x00088AB0
		public bool AreLocalsZeroed
		{
			get
			{
				return this._areLocalsZeroed;
			}
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x0008A8B8 File Offset: 0x00088AB8
		private ILBuilder.BasicBlock GetCurrentBlock()
		{
			if (this._currentBlock == null)
			{
				this.CreateBlock();
			}
			return this._currentBlock;
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x0008A8D0 File Offset: 0x00088AD0
		private void CreateBlock()
		{
			ILBuilder.BasicBlock basicBlock = this._scopeManager.CreateBlock(this);
			this.UpdatesForCreatedBlock(basicBlock);
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x0008A8F4 File Offset: 0x00088AF4
		private ILBuilder.SwitchBlock CreateSwitchBlock()
		{
			this.EndBlock();
			ILBuilder.SwitchBlock switchBlock = this._scopeManager.CreateSwitchBlock(this);
			this.UpdatesForCreatedBlock(switchBlock);
			return switchBlock;
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x0008A91C File Offset: 0x00088B1C
		private void UpdatesForCreatedBlock(ILBuilder.BasicBlock block)
		{
			this._currentBlock = block;
			this._lastCompleteBlock.NextBlock = block;
			this._pendingBlockCreate = false;
			this.ReconcileTrailingMarkers();
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x0008A93E File Offset: 0x00088B3E
		private void CreateBlockIfPending()
		{
			if (this._pendingBlockCreate)
			{
				this.CreateBlock();
			}
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x0008A94E File Offset: 0x00088B4E
		private void EndBlock()
		{
			this.CreateBlockIfPending();
			if (this._currentBlock != null)
			{
				this._lastCompleteBlock = this._currentBlock;
				this._currentBlock = null;
			}
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x0008A974 File Offset: 0x00088B74
		private void ReconcileTrailingMarkers()
		{
			if (this._lastCompleteBlock != null && this._lastCompleteBlock.BranchCode == ILOpCode.Nop && this._lastCompleteBlock.LastILMarker >= 0 && this._allocatedILMarkers[this._lastCompleteBlock.LastILMarker].BlockOffset == this._lastCompleteBlock.RegularInstructionsLength)
			{
				int num = -1;
				int num2 = -1;
				while (this._lastCompleteBlock.LastILMarker >= 0 && this._allocatedILMarkers[this._lastCompleteBlock.LastILMarker].BlockOffset == this._lastCompleteBlock.RegularInstructionsLength)
				{
					num = this._lastCompleteBlock.LastILMarker;
					if (num2 < 0)
					{
						num2 = this._lastCompleteBlock.LastILMarker;
					}
					this._lastCompleteBlock.RemoveTailILMarker(this._lastCompleteBlock.LastILMarker);
				}
				ILBuilder.BasicBlock currentBlock = this.GetCurrentBlock();
				for (int i = num; i <= num2; i++)
				{
					currentBlock.AddILMarker(i);
					this._allocatedILMarkers[i] = new ILBuilder.ILMarker
					{
						BlockOffset = currentBlock.RegularInstructionsLength,
						AbsoluteOffset = -1
					};
				}
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x0008AA8B File Offset: 0x00088C8B
		private ILBuilder.ExceptionHandlerScope EnclosingExceptionHandler
		{
			get
			{
				return this._scopeManager.EnclosingExceptionHandler;
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x060030B9 RID: 12473 RVA: 0x0008AA98 File Offset: 0x00088C98
		internal bool InExceptionHandler
		{
			get
			{
				return this.EnclosingExceptionHandler != null;
			}
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x0008AAA3 File Offset: 0x00088CA3
		internal void Realize()
		{
			if (this.RealizedIL.IsDefault)
			{
				this.RealizeBlocks();
				this._currentBlock = null;
				this._lastCompleteBlock = null;
			}
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x0008AAC6 File Offset: 0x00088CC6
		internal ImmutableArray<Microsoft.Cci.LocalScope> GetAllScopes()
		{
			return this._scopeManager.GetAllScopesWithLocals();
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x0008AAD3 File Offset: 0x00088CD3
		internal ImmutableArray<StateMachineHoistedLocalScope> GetHoistedLocalScopes()
		{
			return this._scopeManager.GetHoistedLocalScopes();
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x0008AAE0 File Offset: 0x00088CE0
		internal void FreeBasicBlocks()
		{
			this._scopeManager.FreeBasicBlocks();
			if (this.SeqPointsOpt != null)
			{
				this.SeqPointsOpt.Free();
				this.SeqPointsOpt = null;
			}
			if (this._allocatedILMarkers != null)
			{
				this._allocatedILMarkers.Free();
				this._allocatedILMarkers = null;
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x060030BE RID: 12478 RVA: 0x0008AB2C File Offset: 0x00088D2C
		internal ushort MaxStack
		{
			get
			{
				return (ushort)this._emitState.MaxStack;
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x060030BF RID: 12479 RVA: 0x0008AB3A File Offset: 0x00088D3A
		internal int InstructionsEmitted
		{
			get
			{
				return this._emitState.InstructionsEmitted;
			}
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x0008AB48 File Offset: 0x00088D48
		private void MarkReachableBlocks()
		{
			ArrayBuilder<ILBuilder.BasicBlock> instance = ArrayBuilder<ILBuilder.BasicBlock>.GetInstance();
			ILBuilder.MarkReachableFrom(instance, this.leaderBlock);
			while (instance.Count != 0)
			{
				ILBuilder.MarkReachableFrom(instance, instance.Pop<ILBuilder.BasicBlock>());
			}
			instance.Free();
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x0008AB83 File Offset: 0x00088D83
		private static void PushReachableBlockToProcess(ArrayBuilder<ILBuilder.BasicBlock> reachableBlocks, ILBuilder.BasicBlock block)
		{
			if (block.Reachability == ILBuilder.Reachability.NotReachable)
			{
				reachableBlocks.Push(block);
			}
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x0008AB94 File Offset: 0x00088D94
		private static void MarkReachableFrom(ArrayBuilder<ILBuilder.BasicBlock> reachableBlocks, ILBuilder.BasicBlock block)
		{
			while (block != null && block.Reachability == ILBuilder.Reachability.NotReachable)
			{
				block.Reachability = ILBuilder.Reachability.Reachable;
				ILOpCode branchCode = block.BranchCode;
				if (branchCode == ILOpCode.Nop && block.Type == ILBuilder.BlockType.Normal)
				{
					block = block.NextBlock;
				}
				else
				{
					if (branchCode.CanFallThrough())
					{
						ILBuilder.PushReachableBlockToProcess(reachableBlocks, block.NextBlock);
					}
					else if (branchCode == ILOpCode.Endfinally)
					{
						ILBuilder.ExceptionHandlerScope enclosingHandler = block.EnclosingHandler;
						if (enclosingHandler != null)
						{
							enclosingHandler.UnblockFinally();
						}
					}
					ILBuilder.BlockType type = block.Type;
					if (type == ILBuilder.BlockType.Try)
					{
						ILBuilder.MarkReachableFromTry(reachableBlocks, block);
						return;
					}
					if (type == ILBuilder.BlockType.Switch)
					{
						ILBuilder.MarkReachableFromSwitch(reachableBlocks, block);
						return;
					}
					ILBuilder.MarkReachableFromBranch(reachableBlocks, block);
					break;
				}
			}
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x0008AC28 File Offset: 0x00088E28
		private static void MarkReachableFromBranch(ArrayBuilder<ILBuilder.BasicBlock> reachableBlocks, ILBuilder.BasicBlock block)
		{
			ILBuilder.BasicBlock branchBlock = block.BranchBlock;
			if (branchBlock != null)
			{
				object obj = ILBuilder.BlockedBranchDestination(block, branchBlock);
				if (obj == null)
				{
					ILBuilder.PushReachableBlockToProcess(reachableBlocks, branchBlock);
					return;
				}
				ILBuilder.RedirectBranchToBlockedDestination(block, obj);
			}
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x0008AC59 File Offset: 0x00088E59
		private static void RedirectBranchToBlockedDestination(ILBuilder.BasicBlock block, object blockedDest)
		{
			block.SetBranch(blockedDest, block.BranchCode);
			if (block.BranchBlock.Reachability == ILBuilder.Reachability.NotReachable)
			{
				block.BranchBlock.Reachability = ILBuilder.Reachability.BlockedByFinally;
			}
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x0008AC84 File Offset: 0x00088E84
		private static object BlockedBranchDestination(ILBuilder.BasicBlock src, ILBuilder.BasicBlock dest)
		{
			ILBuilder.ExceptionHandlerScope enclosingHandler = src.EnclosingHandler;
			if (enclosingHandler == null)
			{
				return null;
			}
			return ILBuilder.BlockedBranchDestinationSlow(dest.EnclosingHandler, enclosingHandler);
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x0008ACAC File Offset: 0x00088EAC
		private static object BlockedBranchDestinationSlow(ILBuilder.ExceptionHandlerScope destHandler, ILBuilder.ExceptionHandlerScope srcHandler)
		{
			ILBuilder.ScopeInfo scopeInfo = null;
			if (destHandler != null)
			{
				scopeInfo = destHandler.ContainingExceptionScope;
			}
			while (srcHandler != destHandler && srcHandler.ContainingExceptionScope != scopeInfo)
			{
				if (srcHandler.Type == ScopeType.Try)
				{
					ILBuilder.ExceptionHandlerLeaderBlock nextExceptionHandler = srcHandler.LeaderBlock.NextExceptionHandler;
					if (nextExceptionHandler.Type == ILBuilder.BlockType.Finally)
					{
						object blockedByFinallyDestination = nextExceptionHandler.EnclosingHandler.BlockedByFinallyDestination;
						if (blockedByFinallyDestination != null)
						{
							return blockedByFinallyDestination;
						}
					}
				}
				srcHandler = srcHandler.ContainingExceptionScope.ContainingHandler;
			}
			return null;
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x0008AD14 File Offset: 0x00088F14
		private static void MarkReachableFromTry(ArrayBuilder<ILBuilder.BasicBlock> reachableBlocks, ILBuilder.BasicBlock block)
		{
			ILBuilder.ExceptionHandlerLeaderBlock exceptionHandlerLeaderBlock = ((ILBuilder.ExceptionHandlerLeaderBlock)block).NextExceptionHandler;
			if (exceptionHandlerLeaderBlock.Type == ILBuilder.BlockType.Finally)
			{
				if (exceptionHandlerLeaderBlock.Reachability != ILBuilder.Reachability.Reachable)
				{
					block.Reachability = ILBuilder.Reachability.NotReachable;
					ILBuilder.PushReachableBlockToProcess(reachableBlocks, block);
					ILBuilder.PushReachableBlockToProcess(reachableBlocks, exceptionHandlerLeaderBlock);
					return;
				}
			}
			else
			{
				while (exceptionHandlerLeaderBlock != null)
				{
					ILBuilder.PushReachableBlockToProcess(reachableBlocks, exceptionHandlerLeaderBlock);
					exceptionHandlerLeaderBlock = exceptionHandlerLeaderBlock.NextExceptionHandler;
				}
			}
			ILBuilder.MarkReachableFromBranch(reachableBlocks, block);
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x0008AD70 File Offset: 0x00088F70
		private static void MarkReachableFromSwitch(ArrayBuilder<ILBuilder.BasicBlock> reachableBlocks, ILBuilder.BasicBlock block)
		{
			ILBuilder.SwitchBlock switchBlock = (ILBuilder.SwitchBlock)block;
			ArrayBuilder<ILBuilder.BasicBlock> instance = ArrayBuilder<ILBuilder.BasicBlock>.GetInstance();
			switchBlock.GetBranchBlocks(instance);
			foreach (ILBuilder.BasicBlock basicBlock in instance)
			{
				ILBuilder.PushReachableBlockToProcess(reachableBlocks, basicBlock);
			}
			instance.Free();
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x0008ADB6 File Offset: 0x00088FB6
		private bool OptimizeLabels()
		{
			return this.ForwardLabelsNoLeaving() | this.ForwardLabelsAllowLeaving();
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x0008ADC8 File Offset: 0x00088FC8
		private bool ForwardLabelsNoLeaving()
		{
			bool flag = false;
			SmallDictionary<object, ILBuilder.LabelInfo>.KeyCollection keys = this._labelInfos.Keys;
			bool flag2;
			do
			{
				flag2 = true;
				foreach (object obj in keys)
				{
					ILBuilder.LabelInfo labelInfo = this._labelInfos[obj];
					ILBuilder.BasicBlock bb = labelInfo.bb;
					if (bb.HasNoRegularInstructions)
					{
						ILBuilder.BasicBlock basicBlock = null;
						ILOpCode branchCode = bb.BranchCode;
						if (branchCode != ILOpCode.Nop)
						{
							if (branchCode == ILOpCode.Br)
							{
								basicBlock = bb.BranchBlock;
							}
						}
						else
						{
							basicBlock = bb.NextBlock;
						}
						if (basicBlock != null && basicBlock != bb)
						{
							ILBuilder.ExceptionHandlerScope enclosingHandler = bb.EnclosingHandler;
							ILBuilder.ExceptionHandlerScope enclosingHandler2 = basicBlock.EnclosingHandler;
							if (enclosingHandler == enclosingHandler2)
							{
								this._labelInfos[obj] = labelInfo.WithNewTarget(basicBlock);
								flag = true;
								flag2 = false;
							}
						}
					}
				}
			}
			while (!flag2);
			return flag;
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x0008AE94 File Offset: 0x00089094
		private bool ForwardLabelsAllowLeaving()
		{
			bool flag = false;
			SmallDictionary<object, ILBuilder.LabelInfo>.KeyCollection keys = this._labelInfos.Keys;
			bool flag2;
			do
			{
				flag2 = true;
				foreach (object obj in keys)
				{
					ILBuilder.LabelInfo labelInfo = this._labelInfos[obj];
					if (!labelInfo.targetOfConditionalBranches)
					{
						ILBuilder.BasicBlock bb = labelInfo.bb;
						if (bb.HasNoRegularInstructions)
						{
							ILBuilder.BasicBlock basicBlock = null;
							ILOpCode branchCode = bb.BranchCode;
							if (branchCode != ILOpCode.Nop)
							{
								if (branchCode == ILOpCode.Br)
								{
									basicBlock = bb.BranchBlock;
								}
							}
							else
							{
								basicBlock = bb.NextBlock;
							}
							if (basicBlock != null && basicBlock != bb)
							{
								ILBuilder.ExceptionHandlerScope enclosingHandler = bb.EnclosingHandler;
								ILBuilder.ExceptionHandlerScope enclosingHandler2 = basicBlock.EnclosingHandler;
								if (ILBuilder.CanMoveLabelToAnotherHandler(enclosingHandler, enclosingHandler2))
								{
									this._labelInfos[obj] = labelInfo.WithNewTarget(basicBlock);
									flag = true;
									flag2 = false;
								}
							}
						}
					}
				}
			}
			while (!flag2);
			return flag;
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x0008AF6C File Offset: 0x0008916C
		private static bool CanMoveLabelToAnotherHandler(ILBuilder.ExceptionHandlerScope currentHandler, ILBuilder.ExceptionHandlerScope newHandler)
		{
			if (newHandler == null && currentHandler.ContainingExceptionScope.FinallyOnly())
			{
				return true;
			}
			while (currentHandler != newHandler)
			{
				ILBuilder.ExceptionHandlerContainerScope containingExceptionScope = currentHandler.ContainingExceptionScope;
				if (!containingExceptionScope.FinallyOnly())
				{
					return false;
				}
				currentHandler = containingExceptionScope.ContainingHandler;
				if (currentHandler == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x0008AFB0 File Offset: 0x000891B0
		private bool DropUnreachableBlocks()
		{
			bool flag = false;
			ILBuilder.BasicBlock nextBlock = this.leaderBlock;
			while (nextBlock.NextBlock != null)
			{
				if (nextBlock.NextBlock.Reachability == ILBuilder.Reachability.NotReachable)
				{
					nextBlock.NextBlock = nextBlock.NextBlock.NextBlock;
					flag = true;
				}
				else
				{
					nextBlock = nextBlock.NextBlock;
				}
			}
			return flag;
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x0008AFFC File Offset: 0x000891FC
		private void MarkAllBlocksUnreachable()
		{
			for (ILBuilder.BasicBlock nextBlock = this.leaderBlock; nextBlock != null; nextBlock = nextBlock.NextBlock)
			{
				nextBlock.Reachability = ILBuilder.Reachability.NotReachable;
			}
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x0008B024 File Offset: 0x00089224
		private void ComputeOffsets()
		{
			ILBuilder.BasicBlock nextBlock = this.leaderBlock;
			while (nextBlock.NextBlock != null)
			{
				nextBlock.NextBlock.Start = nextBlock.Start + nextBlock.TotalSize;
				nextBlock = nextBlock.NextBlock;
			}
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x0008B064 File Offset: 0x00089264
		private void RewriteSpecialBlocks()
		{
			for (ILBuilder.BasicBlock nextBlock = this.leaderBlock; nextBlock != null; nextBlock = nextBlock.NextBlock)
			{
				if (ILBuilder.IsSpecialEndHandlerBlock(nextBlock))
				{
					if (nextBlock.Reachability == ILBuilder.Reachability.BlockedByFinally)
					{
						nextBlock.SetBranchCode(ILOpCode.Br_s);
					}
					else
					{
						nextBlock.SetBranch(null, ILOpCode.Nop);
					}
				}
			}
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x0008B0A7 File Offset: 0x000892A7
		private static bool IsSpecialEndHandlerBlock(ILBuilder.BasicBlock block)
		{
			return block.BranchCode == ILOpCode.Nop && block.BranchLabel != null;
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x0008B0BC File Offset: 0x000892BC
		private void RewriteBranchesAcrossExceptionHandlers()
		{
			for (ILBuilder.BasicBlock nextBlock = this.leaderBlock; nextBlock != null; nextBlock = nextBlock.NextBlock)
			{
				nextBlock.RewriteBranchesAcrossExceptionHandlers();
			}
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x0008B0E4 File Offset: 0x000892E4
		private bool ComputeOffsetsAndAdjustBranches()
		{
			this.ComputeOffsets();
			bool flag = false;
			int num;
			do
			{
				num = 0;
				for (ILBuilder.BasicBlock nextBlock = this.leaderBlock; nextBlock != null; nextBlock = nextBlock.NextBlock)
				{
					nextBlock.AdjustForDelta(num);
					if (this._optimizations == OptimizationLevel.Release)
					{
						flag |= nextBlock.OptimizeBranches(ref num);
					}
					nextBlock.ShortenBranches(ref num);
				}
			}
			while (num < 0);
			return flag;
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x0008B138 File Offset: 0x00089338
		private void RealizeBlocks()
		{
			this.MarkReachableBlocks();
			this.RewriteSpecialBlocks();
			this.DropUnreachableBlocks();
			if (this._optimizations == OptimizationLevel.Release && this.OptimizeLabels())
			{
				this.MarkAllBlocksUnreachable();
				this.MarkReachableBlocks();
				this.DropUnreachableBlocks();
			}
			this.RewriteBranchesAcrossExceptionHandlers();
			while (this.ComputeOffsetsAndAdjustBranches())
			{
				this.MarkAllBlocksUnreachable();
				this.MarkReachableBlocks();
				if (!this.DropUnreachableBlocks())
				{
					break;
				}
			}
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance(1024);
			for (ILBuilder.BasicBlock nextBlock = this.leaderBlock; nextBlock != null; nextBlock = nextBlock.NextBlock)
			{
				int firstILMarker = nextBlock.FirstILMarker;
				if (firstILMarker >= 0)
				{
					int lastILMarker = nextBlock.LastILMarker;
					for (int i = firstILMarker; i <= lastILMarker; i++)
					{
						int blockOffset = this._allocatedILMarkers[i].BlockOffset;
						int num = instance.Count + blockOffset;
						this._allocatedILMarkers[i] = new ILBuilder.ILMarker
						{
							BlockOffset = blockOffset,
							AbsoluteOffset = num
						};
					}
				}
				BlobBuilder regularInstructions = nextBlock.RegularInstructions;
				if (regularInstructions != null)
				{
					regularInstructions.WriteContentTo(instance);
				}
				ILOpCode branchCode = nextBlock.BranchCode;
				if (branchCode != ILOpCode.Nop)
				{
					if (branchCode == ILOpCode.Switch)
					{
						ILBuilder.WriteOpCode(instance, ILOpCode.Switch);
						ILBuilder.SwitchBlock switchBlock = (ILBuilder.SwitchBlock)nextBlock;
						instance.WriteUInt32(switchBlock.BranchesCount);
						int num2 = switchBlock.Start + switchBlock.TotalSize;
						ArrayBuilder<ILBuilder.BasicBlock> instance2 = ArrayBuilder<ILBuilder.BasicBlock>.GetInstance();
						switchBlock.GetBranchBlocks(instance2);
						foreach (ILBuilder.BasicBlock basicBlock in instance2)
						{
							instance.WriteInt32(basicBlock.Start - num2);
						}
						instance2.Free();
					}
					else
					{
						ILBuilder.WriteOpCode(instance, nextBlock.BranchCode);
						if (nextBlock.BranchLabel != null)
						{
							int start = nextBlock.BranchBlock.Start;
							int num3 = nextBlock.Start + nextBlock.TotalSize;
							int num4 = start - num3;
							if (nextBlock.BranchCode.GetBranchOperandSize() == 1)
							{
								sbyte b = (sbyte)num4;
								instance.WriteSByte(b);
							}
							else
							{
								instance.WriteInt32(num4);
							}
						}
					}
				}
			}
			this.RealizedIL = instance.ToImmutableArray();
			instance.Free();
			this.RealizeSequencePoints();
			this.RealizedExceptionHandlers = this._scopeManager.GetExceptionHandlerRegions();
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x0008B34C File Offset: 0x0008954C
		private void RealizeSequencePoints()
		{
			if (this.SeqPointsOpt != null)
			{
				int num = -1;
				ArrayBuilder<RawSequencePoint> instance = ArrayBuilder<RawSequencePoint>.GetInstance();
				foreach (RawSequencePoint rawSequencePoint in this.SeqPointsOpt)
				{
					int iloffsetFromMarker = this.GetILOffsetFromMarker(rawSequencePoint.ILMarker);
					if (iloffsetFromMarker >= 0)
					{
						if (num != iloffsetFromMarker)
						{
							num = iloffsetFromMarker;
							instance.Add(rawSequencePoint);
						}
						else
						{
							instance[instance.Count - 1] = rawSequencePoint;
						}
					}
				}
				if (instance.Count > 0)
				{
					this.RealizedSequencePoints = SequencePointList.Create(instance, this);
				}
				instance.Free();
			}
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x0008B3D8 File Offset: 0x000895D8
		internal void DefineSequencePoint(SyntaxTree syntaxTree, TextSpan span)
		{
			this.GetCurrentBlock();
			this._lastSeqPointTree = syntaxTree;
			if (this.SeqPointsOpt == null)
			{
				this.SeqPointsOpt = ArrayBuilder<RawSequencePoint>.GetInstance();
			}
			if (this._initialHiddenSequencePointMarker >= 0)
			{
				this.SeqPointsOpt.Add(new RawSequencePoint(syntaxTree, this._initialHiddenSequencePointMarker, RawSequencePoint.HiddenSequencePointSpan));
				this._initialHiddenSequencePointMarker = -1;
			}
			this.SeqPointsOpt.Add(new RawSequencePoint(syntaxTree, this.AllocateILMarker(), span));
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x0008B44C File Offset: 0x0008964C
		internal void DefineHiddenSequencePoint()
		{
			SyntaxTree lastSeqPointTree = this._lastSeqPointTree;
			if (lastSeqPointTree != null)
			{
				this.DefineSequencePoint(lastSeqPointTree, RawSequencePoint.HiddenSequencePointSpan);
			}
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x0008B46F File Offset: 0x0008966F
		internal void DefineInitialHiddenSequencePoint()
		{
			this._initialHiddenSequencePointMarker = this.AllocateILMarker();
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x0008B47D File Offset: 0x0008967D
		internal void SetInitialDebugDocument(SyntaxTree initialSequencePointTree)
		{
			this._lastSeqPointTree = initialSequencePointTree;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x0008B486 File Offset: 0x00089686
		[Conditional("DEBUG")]
		internal void AssertStackEmpty()
		{
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x0008B488 File Offset: 0x00089688
		internal bool IsJustPastLabel()
		{
			return this._emitState.InstructionsEmitted == this._instructionCountAtLastLabel;
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x0008B4A0 File Offset: 0x000896A0
		internal void OpenLocalScope(ScopeType scopeType = ScopeType.Variable, ITypeReference exceptionType = null)
		{
			if (scopeType == ScopeType.TryCatchFinally && this.IsJustPastLabel())
			{
				this.DefineHiddenSequencePoint();
				this.EmitOpCode(ILOpCode.Nop);
			}
			if (scopeType == ScopeType.Finally)
			{
				this._instructionCountAtLastLabel = this._emitState.InstructionsEmitted;
			}
			this.EndBlock();
			this._scopeManager.OpenScope(scopeType, exceptionType);
			switch (scopeType)
			{
			case ScopeType.Variable:
			case ScopeType.TryCatchFinally:
			case ScopeType.StateMachineVariable:
				return;
			case ScopeType.Try:
				this._pendingBlockCreate = true;
				return;
			case ScopeType.Catch:
			case ScopeType.Filter:
			case ScopeType.Finally:
			case ScopeType.Fault:
				this._pendingBlockCreate = true;
				this.DefineHiddenSequencePoint();
				return;
			default:
				throw ExceptionUtilities.UnexpectedValue(scopeType);
			}
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x0008B539 File Offset: 0x00089739
		internal bool PossiblyDefinedOutsideOfTry(LocalDefinition local)
		{
			return this._scopeManager.PossiblyDefinedOutsideOfTry(local);
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x0008B547 File Offset: 0x00089747
		internal void MarkFilterConditionEnd()
		{
			this._scopeManager.FinishFilterCondition(this);
			this.DefineHiddenSequencePoint();
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x0008B55B File Offset: 0x0008975B
		internal void CloseLocalScope()
		{
			this._scopeManager.ClosingScope(this);
			this.EndBlock();
			this._scopeManager.CloseScope(this);
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x0008B57B File Offset: 0x0008977B
		internal void DefineUserDefinedStateMachineHoistedLocal(int slotIndex)
		{
			this._scopeManager.AddUserHoistedLocal(slotIndex);
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x0008B58C File Offset: 0x0008978C
		internal void AddLocalToScope(LocalDefinition local)
		{
			this.HasDynamicLocal |= !local.DynamicTransformFlags.IsEmpty;
			this._scopeManager.AddLocal(local);
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x0008B5C4 File Offset: 0x000897C4
		internal void AddLocalConstantToScope(LocalConstantDefinition localConstant)
		{
			this.HasDynamicLocal |= !localConstant.DynamicTransformFlags.IsEmpty;
			this._scopeManager.AddLocalConstant(localConstant);
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x060030E3 RID: 12515 RVA: 0x0008B5FB File Offset: 0x000897FB
		// (set) Token: 0x060030E4 RID: 12516 RVA: 0x0008B603 File Offset: 0x00089803
		internal bool HasDynamicLocal { get; private set; }

		// Token: 0x060030E5 RID: 12517 RVA: 0x0008B60C File Offset: 0x0008980C
		internal ILBuilder GetSnapshot()
		{
			ILBuilder ilbuilder = (ILBuilder)base.MemberwiseClone();
			ilbuilder.RealizedIL = this.RealizedIL;
			return ilbuilder;
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x0008B628 File Offset: 0x00089828
		private bool AllBlocks(Func<ILBuilder.BasicBlock, bool> predicate)
		{
			for (ILBuilder.BasicBlock nextBlock = this.leaderBlock; nextBlock != null; nextBlock = nextBlock.NextBlock)
			{
				if (!predicate.Invoke(nextBlock))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x0008B654 File Offset: 0x00089854
		internal int AllocateILMarker()
		{
			if (this._allocatedILMarkers == null)
			{
				this._allocatedILMarkers = ArrayBuilder<ILBuilder.ILMarker>.GetInstance();
			}
			ILBuilder.BasicBlock currentBlock = this.GetCurrentBlock();
			int count = this._allocatedILMarkers.Count;
			currentBlock.AddILMarker(count);
			this._allocatedILMarkers.Add(new ILBuilder.ILMarker
			{
				BlockOffset = currentBlock.RegularInstructionsLength,
				AbsoluteOffset = -1
			});
			return count;
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x0008B6B8 File Offset: 0x000898B8
		public int GetILOffsetFromMarker(int ilMarker)
		{
			return this._allocatedILMarkers[ilMarker].AbsoluteOffset;
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x0008B6CB File Offset: 0x000898CB
		private string GetDebuggerDisplay()
		{
			return "";
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x0008B6D4 File Offset: 0x000898D4
		public void EmitNumericConversion(Microsoft.Cci.PrimitiveTypeCode fromPredefTypeKind, Microsoft.Cci.PrimitiveTypeCode toPredefTypeKind, bool @checked)
		{
			bool flag = fromPredefTypeKind.IsUnsigned();
			switch (toPredefTypeKind)
			{
			case Microsoft.Cci.PrimitiveTypeCode.Char:
			case Microsoft.Cci.PrimitiveTypeCode.UInt16:
				if (fromPredefTypeKind == Microsoft.Cci.PrimitiveTypeCode.Char || fromPredefTypeKind - Microsoft.Cci.PrimitiveTypeCode.UInt8 <= 1)
				{
					return;
				}
				if (@checked)
				{
					this.EmitOpCode(flag ? ILOpCode.Conv_ovf_u2_un : ILOpCode.Conv_ovf_u2);
					return;
				}
				this.EmitOpCode(ILOpCode.Conv_u2);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Int8:
				if (fromPredefTypeKind == Microsoft.Cci.PrimitiveTypeCode.Int8)
				{
					return;
				}
				if (@checked)
				{
					this.EmitOpCode(flag ? ILOpCode.Conv_ovf_i1_un : ILOpCode.Conv_ovf_i1);
					return;
				}
				this.EmitOpCode(ILOpCode.Conv_i1);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Float32:
				if (fromPredefTypeKind - Microsoft.Cci.PrimitiveTypeCode.UInt32 <= 1)
				{
					this.EmitOpCode(ILOpCode.Conv_r_un);
				}
				this.EmitOpCode(ILOpCode.Conv_r4);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Float64:
				if (fromPredefTypeKind - Microsoft.Cci.PrimitiveTypeCode.UInt32 <= 1)
				{
					this.EmitOpCode(ILOpCode.Conv_r_un);
				}
				this.EmitOpCode(ILOpCode.Conv_r8);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Int16:
				if (fromPredefTypeKind == Microsoft.Cci.PrimitiveTypeCode.Int8 || fromPredefTypeKind == Microsoft.Cci.PrimitiveTypeCode.Int16 || fromPredefTypeKind == Microsoft.Cci.PrimitiveTypeCode.UInt8)
				{
					return;
				}
				if (@checked)
				{
					this.EmitOpCode(flag ? ILOpCode.Conv_ovf_i2_un : ILOpCode.Conv_ovf_i2);
					return;
				}
				this.EmitOpCode(ILOpCode.Conv_i2);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Int32:
				if (fromPredefTypeKind <= Microsoft.Cci.PrimitiveTypeCode.Int32)
				{
					if (fromPredefTypeKind - Microsoft.Cci.PrimitiveTypeCode.Char <= 1 || fromPredefTypeKind - Microsoft.Cci.PrimitiveTypeCode.Int16 <= 1)
					{
						return;
					}
				}
				else
				{
					if (fromPredefTypeKind - Microsoft.Cci.PrimitiveTypeCode.UInt8 <= 1)
					{
						return;
					}
					if (fromPredefTypeKind == Microsoft.Cci.PrimitiveTypeCode.UInt32)
					{
						if (@checked)
						{
							this.EmitOpCode(ILOpCode.Conv_ovf_i4_un);
							return;
						}
						return;
					}
				}
				if (@checked)
				{
					this.EmitOpCode(flag ? ILOpCode.Conv_ovf_i4_un : ILOpCode.Conv_ovf_i4);
					return;
				}
				this.EmitOpCode(ILOpCode.Conv_i4);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Int64:
				switch (fromPredefTypeKind)
				{
				case Microsoft.Cci.PrimitiveTypeCode.Char:
				case Microsoft.Cci.PrimitiveTypeCode.UInt8:
				case Microsoft.Cci.PrimitiveTypeCode.UInt16:
				case Microsoft.Cci.PrimitiveTypeCode.UInt32:
					this.EmitOpCode(ILOpCode.Conv_u8);
					return;
				case Microsoft.Cci.PrimitiveTypeCode.Int8:
				case Microsoft.Cci.PrimitiveTypeCode.Int16:
				case Microsoft.Cci.PrimitiveTypeCode.Int32:
				case Microsoft.Cci.PrimitiveTypeCode.IntPtr:
					this.EmitOpCode(ILOpCode.Conv_i8);
					return;
				case Microsoft.Cci.PrimitiveTypeCode.Int64:
					return;
				case Microsoft.Cci.PrimitiveTypeCode.Pointer:
				case Microsoft.Cci.PrimitiveTypeCode.UIntPtr:
				case Microsoft.Cci.PrimitiveTypeCode.FunctionPointer:
					if (@checked)
					{
						this.EmitOpCode(ILOpCode.Conv_ovf_i8_un);
						return;
					}
					this.EmitOpCode(ILOpCode.Conv_u8);
					return;
				case Microsoft.Cci.PrimitiveTypeCode.UInt64:
					if (@checked)
					{
						this.EmitOpCode(ILOpCode.Conv_ovf_i8_un);
						return;
					}
					return;
				}
				if (@checked)
				{
					this.EmitOpCode(ILOpCode.Conv_ovf_i8);
					return;
				}
				this.EmitOpCode(ILOpCode.Conv_i8);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.IntPtr:
				switch (fromPredefTypeKind)
				{
				case Microsoft.Cci.PrimitiveTypeCode.Char:
				case Microsoft.Cci.PrimitiveTypeCode.UInt8:
				case Microsoft.Cci.PrimitiveTypeCode.UInt16:
					this.EmitOpCode(ILOpCode.Conv_u);
					return;
				case Microsoft.Cci.PrimitiveTypeCode.Int8:
				case Microsoft.Cci.PrimitiveTypeCode.Int16:
				case Microsoft.Cci.PrimitiveTypeCode.Int32:
					this.EmitOpCode(ILOpCode.Conv_i);
					return;
				case Microsoft.Cci.PrimitiveTypeCode.IntPtr:
					return;
				case Microsoft.Cci.PrimitiveTypeCode.Pointer:
				case Microsoft.Cci.PrimitiveTypeCode.FunctionPointer:
					if (!@checked)
					{
						return;
					}
					break;
				case Microsoft.Cci.PrimitiveTypeCode.UInt32:
					if (@checked)
					{
						this.EmitOpCode(ILOpCode.Conv_ovf_i_un);
						return;
					}
					this.EmitOpCode(ILOpCode.Conv_u);
					return;
				}
				if (@checked)
				{
					this.EmitOpCode(flag ? ILOpCode.Conv_ovf_i_un : ILOpCode.Conv_ovf_i);
					return;
				}
				this.EmitOpCode(ILOpCode.Conv_i);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.Pointer:
			case Microsoft.Cci.PrimitiveTypeCode.FunctionPointer:
				if (@checked)
				{
					switch (fromPredefTypeKind)
					{
					case Microsoft.Cci.PrimitiveTypeCode.Int8:
					case Microsoft.Cci.PrimitiveTypeCode.Int16:
					case Microsoft.Cci.PrimitiveTypeCode.Int32:
					case Microsoft.Cci.PrimitiveTypeCode.Int64:
						this.EmitOpCode(ILOpCode.Conv_ovf_u);
						return;
					case Microsoft.Cci.PrimitiveTypeCode.IntPtr:
						this.EmitOpCode(ILOpCode.Conv_ovf_u);
						return;
					case Microsoft.Cci.PrimitiveTypeCode.UInt8:
					case Microsoft.Cci.PrimitiveTypeCode.UInt16:
					case Microsoft.Cci.PrimitiveTypeCode.UInt32:
						this.EmitOpCode(ILOpCode.Conv_u);
						return;
					case Microsoft.Cci.PrimitiveTypeCode.UInt64:
						this.EmitOpCode(ILOpCode.Conv_ovf_u_un);
						return;
					case Microsoft.Cci.PrimitiveTypeCode.UIntPtr:
						return;
					}
					throw ExceptionUtilities.UnexpectedValue(fromPredefTypeKind);
				}
				switch (fromPredefTypeKind)
				{
				case Microsoft.Cci.PrimitiveTypeCode.Int8:
				case Microsoft.Cci.PrimitiveTypeCode.Int16:
				case Microsoft.Cci.PrimitiveTypeCode.Int32:
					this.EmitOpCode(ILOpCode.Conv_i);
					return;
				case Microsoft.Cci.PrimitiveTypeCode.Int64:
				case Microsoft.Cci.PrimitiveTypeCode.UInt8:
				case Microsoft.Cci.PrimitiveTypeCode.UInt16:
				case Microsoft.Cci.PrimitiveTypeCode.UInt32:
				case Microsoft.Cci.PrimitiveTypeCode.UInt64:
					this.EmitOpCode(ILOpCode.Conv_u);
					return;
				case Microsoft.Cci.PrimitiveTypeCode.IntPtr:
				case Microsoft.Cci.PrimitiveTypeCode.UIntPtr:
					return;
				}
				throw ExceptionUtilities.UnexpectedValue(fromPredefTypeKind);
			case Microsoft.Cci.PrimitiveTypeCode.UInt8:
				if (fromPredefTypeKind == Microsoft.Cci.PrimitiveTypeCode.UInt8)
				{
					return;
				}
				if (@checked)
				{
					this.EmitOpCode(flag ? ILOpCode.Conv_ovf_u1_un : ILOpCode.Conv_ovf_u1);
					return;
				}
				this.EmitOpCode(ILOpCode.Conv_u1);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.UInt32:
				switch (fromPredefTypeKind)
				{
				case Microsoft.Cci.PrimitiveTypeCode.Char:
					return;
				case Microsoft.Cci.PrimitiveTypeCode.Int8:
				case Microsoft.Cci.PrimitiveTypeCode.Int16:
				case Microsoft.Cci.PrimitiveTypeCode.Int32:
					if (@checked)
					{
						this.EmitOpCode(ILOpCode.Conv_ovf_u4);
						return;
					}
					return;
				case Microsoft.Cci.PrimitiveTypeCode.Float32:
				case Microsoft.Cci.PrimitiveTypeCode.Float64:
					break;
				default:
					if (fromPredefTypeKind - Microsoft.Cci.PrimitiveTypeCode.UInt8 <= 2)
					{
						return;
					}
					break;
				}
				if (@checked)
				{
					this.EmitOpCode(flag ? ILOpCode.Conv_ovf_u4_un : ILOpCode.Conv_ovf_u4);
					return;
				}
				this.EmitOpCode(ILOpCode.Conv_u4);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.UInt64:
				switch (fromPredefTypeKind)
				{
				case Microsoft.Cci.PrimitiveTypeCode.Char:
				case Microsoft.Cci.PrimitiveTypeCode.Pointer:
				case Microsoft.Cci.PrimitiveTypeCode.UInt8:
				case Microsoft.Cci.PrimitiveTypeCode.UInt16:
				case Microsoft.Cci.PrimitiveTypeCode.UInt32:
				case Microsoft.Cci.PrimitiveTypeCode.UIntPtr:
				case Microsoft.Cci.PrimitiveTypeCode.FunctionPointer:
					this.EmitOpCode(ILOpCode.Conv_u8);
					return;
				case Microsoft.Cci.PrimitiveTypeCode.Int8:
				case Microsoft.Cci.PrimitiveTypeCode.Int16:
				case Microsoft.Cci.PrimitiveTypeCode.Int32:
				case Microsoft.Cci.PrimitiveTypeCode.IntPtr:
					if (@checked)
					{
						this.EmitOpCode(ILOpCode.Conv_ovf_u8);
						return;
					}
					this.EmitOpCode(ILOpCode.Conv_i8);
					return;
				case Microsoft.Cci.PrimitiveTypeCode.Int64:
					if (@checked)
					{
						this.EmitOpCode(ILOpCode.Conv_ovf_u8);
						return;
					}
					return;
				case Microsoft.Cci.PrimitiveTypeCode.UInt64:
					return;
				}
				if (@checked)
				{
					this.EmitOpCode(ILOpCode.Conv_ovf_u8);
					return;
				}
				this.EmitOpCode(ILOpCode.Conv_u8);
				return;
			case Microsoft.Cci.PrimitiveTypeCode.UIntPtr:
				switch (fromPredefTypeKind)
				{
				case Microsoft.Cci.PrimitiveTypeCode.Char:
				case Microsoft.Cci.PrimitiveTypeCode.UInt8:
				case Microsoft.Cci.PrimitiveTypeCode.UInt16:
				case Microsoft.Cci.PrimitiveTypeCode.UInt32:
					this.EmitOpCode(ILOpCode.Conv_u);
					return;
				case Microsoft.Cci.PrimitiveTypeCode.Int8:
				case Microsoft.Cci.PrimitiveTypeCode.Int16:
				case Microsoft.Cci.PrimitiveTypeCode.Int32:
					if (@checked)
					{
						this.EmitOpCode(ILOpCode.Conv_ovf_u);
						return;
					}
					this.EmitOpCode(ILOpCode.Conv_i);
					return;
				case Microsoft.Cci.PrimitiveTypeCode.Pointer:
				case Microsoft.Cci.PrimitiveTypeCode.UIntPtr:
				case Microsoft.Cci.PrimitiveTypeCode.FunctionPointer:
					return;
				}
				if (@checked)
				{
					this.EmitOpCode(flag ? ILOpCode.Conv_ovf_u_un : ILOpCode.Conv_ovf_u);
					return;
				}
				this.EmitOpCode(ILOpCode.Conv_u);
				return;
			}
			throw ExceptionUtilities.UnexpectedValue(toPredefTypeKind);
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x0008BC8F File Offset: 0x00089E8F
		internal void AdjustStack(int stackAdjustment)
		{
			this._emitState.AdjustStack(stackAdjustment);
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x060030EC RID: 12524 RVA: 0x0008BC9D File Offset: 0x00089E9D
		internal bool IsStackEmpty
		{
			get
			{
				return this._emitState.CurStack == 0;
			}
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x0008BCAD File Offset: 0x00089EAD
		internal void EmitOpCode(ILOpCode code)
		{
			this.EmitOpCode(code, code.NetStackBehavior());
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x0008BCBC File Offset: 0x00089EBC
		internal void EmitOpCode(ILOpCode code, int stackAdjustment)
		{
			ILBuilder.WriteOpCode(this.GetCurrentWriter(), code);
			this._emitState.AdjustStack(stackAdjustment);
			this._emitState.InstructionAdded();
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x0008BCE4 File Offset: 0x00089EE4
		[NullableContext(1)]
		internal void EmitToken(string value)
		{
			ITokenDeferral tokenDeferral = this.module;
			uint num = ((tokenDeferral != null) ? tokenDeferral.GetFakeStringTokenForIL(value) : 65535U);
			this.GetCurrentWriter().WriteUInt32(num);
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x0008BD18 File Offset: 0x00089F18
		[NullableContext(1)]
		internal void EmitToken(IReference value, SyntaxNode syntaxNode, DiagnosticBag diagnostics, bool encodeAsRawToken = false)
		{
			ITokenDeferral tokenDeferral = this.module;
			uint num = ((tokenDeferral != null) ? tokenDeferral.GetFakeSymbolTokenForIL(value, syntaxNode, diagnostics) : 65535U);
			if (encodeAsRawToken)
			{
				num |= 2147483648U;
			}
			this.GetCurrentWriter().WriteUInt32(num);
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x0008BD58 File Offset: 0x00089F58
		[NullableContext(1)]
		internal void EmitToken(ISignature value, SyntaxNode syntaxNode, DiagnosticBag diagnostics)
		{
			ITokenDeferral tokenDeferral = this.module;
			uint num = ((tokenDeferral != null) ? tokenDeferral.GetFakeSymbolTokenForIL(value, syntaxNode, diagnostics) : 65535U);
			this.GetCurrentWriter().WriteUInt32(num);
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x0008BD8B File Offset: 0x00089F8B
		internal void EmitGreatestMethodToken()
		{
			this.GetCurrentWriter().WriteUInt32(1073741824U);
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x0008BD9D File Offset: 0x00089F9D
		internal void EmitModuleVersionIdStringToken()
		{
			this.GetCurrentWriter().WriteUInt32(2147483648U);
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x0008BDAF File Offset: 0x00089FAF
		[NullableContext(1)]
		internal void EmitSourceDocumentIndexToken(DebugSourceDocument document)
		{
			BlobBuilder currentWriter = this.GetCurrentWriter();
			ITokenDeferral tokenDeferral = this.module;
			currentWriter.WriteUInt32(((tokenDeferral != null) ? tokenDeferral.GetSourceDocumentIndexForIL(document) : 65535U) | 536870912U);
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x0008BDDC File Offset: 0x00089FDC
		[NullableContext(1)]
		internal void EmitArrayBlockInitializer([Nullable(0)] ImmutableArray<byte> data, SyntaxNode syntaxNode, DiagnosticBag diagnostics)
		{
			IMethodReference initArrayHelper = this.module.GetInitArrayHelper();
			IFieldReference fieldForData = this.module.GetFieldForData(data, syntaxNode, diagnostics);
			this.EmitOpCode(ILOpCode.Dup);
			this.EmitOpCode(ILOpCode.Ldtoken);
			this.EmitToken(fieldForData, syntaxNode, diagnostics, false);
			this.EmitOpCode(ILOpCode.Call, -2);
			this.EmitToken(initArrayHelper, syntaxNode, diagnostics);
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x0008BE34 File Offset: 0x0008A034
		[NullableContext(1)]
		internal void EmitStackAllocBlockInitializer([Nullable(0)] ImmutableArray<byte> data, SyntaxNode syntaxNode, bool emitInitBlock, DiagnosticBag diagnostics)
		{
			if (emitInitBlock)
			{
				this.EmitOpCode(ILOpCode.Dup);
				this.EmitIntConstant((int)data[0]);
				this.EmitIntConstant(data.Length);
				this.EmitOpCode(ILOpCode.Initblk, -3);
				return;
			}
			IFieldReference fieldForData = this.module.GetFieldForData(data, syntaxNode, diagnostics);
			this.EmitOpCode(ILOpCode.Dup);
			this.EmitOpCode(ILOpCode.Ldsflda);
			this.EmitToken(fieldForData, syntaxNode, diagnostics, false);
			this.EmitIntConstant(data.Length);
			this.EmitOpCode(ILOpCode.Cpblk, -3);
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x0008BEBC File Offset: 0x0008A0BC
		[NullableContext(1)]
		internal void EmitArrayBlockFieldRef([Nullable(0)] ImmutableArray<byte> data, SyntaxNode syntaxNode, DiagnosticBag diagnostics)
		{
			IFieldReference fieldForData = this.module.GetFieldForData(data, syntaxNode, diagnostics);
			this.EmitOpCode(ILOpCode.Ldsflda);
			this.EmitToken(fieldForData, syntaxNode, diagnostics, false);
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x0008BEEC File Offset: 0x0008A0EC
		[NullableContext(1)]
		internal void MarkLabel(object label)
		{
			this.EndBlock();
			ILBuilder.BasicBlock currentBlock = this.GetCurrentBlock();
			ILBuilder.LabelInfo labelInfo;
			if (this._labelInfos.TryGetValue(label, out labelInfo))
			{
				int curStack = this._emitState.CurStack;
				this._labelInfos[label] = labelInfo.WithNewTarget(currentBlock);
			}
			else
			{
				int curStack2 = this._emitState.CurStack;
				this._labelInfos[label] = new ILBuilder.LabelInfo(currentBlock, curStack2, false);
			}
			this._instructionCountAtLastLabel = this._emitState.InstructionsEmitted;
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x0008BF6C File Offset: 0x0008A16C
		[NullableContext(1)]
		internal void EmitBranch(ILOpCode code, object label, ILOpCode revOpCode = ILOpCode.Nop)
		{
			if (code != ILOpCode.Nop)
			{
				code.IsBranch();
			}
			this._emitState.AdjustStack(code.NetStackBehavior());
			bool flag = code.IsConditionalBranch();
			ILBuilder.LabelInfo labelInfo;
			if (!this._labelInfos.TryGetValue(label, out labelInfo))
			{
				this._labelInfos.Add(label, new ILBuilder.LabelInfo(this._emitState.CurStack, flag));
			}
			this.GetCurrentBlock().SetBranch(label, code, revOpCode);
			if (code != ILOpCode.Nop)
			{
				this._emitState.InstructionAdded();
			}
			this.EndBlock();
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x0008BFF0 File Offset: 0x0008A1F0
		[NullableContext(1)]
		internal void EmitStringSwitchJumpTable([Nullable(new byte[] { 1, 0, 1, 1 })] KeyValuePair<ConstantValue, object>[] caseLabels, object fallThroughLabel, LocalOrParameter key, [Nullable(2)] LocalDefinition keyHash, SwitchStringJumpTableEmitter.EmitStringCompareAndBranch emitStringCondBranchDelegate, SwitchStringJumpTableEmitter.GetStringHashCode computeStringHashcodeDelegate)
		{
			SwitchStringJumpTableEmitter switchStringJumpTableEmitter = new SwitchStringJumpTableEmitter(this, key, caseLabels, fallThroughLabel, keyHash, emitStringCondBranchDelegate, computeStringHashcodeDelegate);
			switchStringJumpTableEmitter.EmitJumpTable();
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x0008C018 File Offset: 0x0008A218
		[NullableContext(1)]
		internal void EmitIntegerSwitchJumpTable([Nullable(new byte[] { 1, 0, 1, 1 })] KeyValuePair<ConstantValue, object>[] caseLabels, object fallThroughLabel, LocalOrParameter key, Microsoft.Cci.PrimitiveTypeCode keyTypeCode)
		{
			SwitchIntegralJumpTableEmitter switchIntegralJumpTableEmitter = new SwitchIntegralJumpTableEmitter(this, caseLabels, fallThroughLabel, keyTypeCode, key);
			switchIntegralJumpTableEmitter.EmitJumpTable();
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x0008C03C File Offset: 0x0008A23C
		[NullableContext(1)]
		internal void EmitSwitch(object[] labels)
		{
			this._emitState.AdjustStack(-1);
			int curStack = this._emitState.CurStack;
			foreach (object obj in labels)
			{
				ILBuilder.LabelInfo labelInfo;
				if (!this._labelInfos.TryGetValue(obj, out labelInfo))
				{
					this._labelInfos.Add(obj, new ILBuilder.LabelInfo(curStack, true));
				}
				else if (!labelInfo.targetOfConditionalBranches)
				{
					this._labelInfos[obj] = labelInfo.SetTargetOfConditionalBranches();
				}
			}
			this.CreateSwitchBlock().BranchLabels = labels;
			this.EndBlock();
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x0008C0C8 File Offset: 0x0008A2C8
		internal void EmitRet(bool isVoid)
		{
			if (!isVoid)
			{
				this._emitState.AdjustStack(-1);
			}
			this.GetCurrentBlock().SetBranchCode(ILOpCode.Ret);
			this._emitState.InstructionAdded();
			this.EndBlock();
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x0008C0F8 File Offset: 0x0008A2F8
		internal void EmitThrow(bool isRethrow)
		{
			ILBuilder.BasicBlock currentBlock = this.GetCurrentBlock();
			if (isRethrow)
			{
				currentBlock.SetBranchCode(ILOpCode.Rethrow);
			}
			else
			{
				currentBlock.SetBranchCode(ILOpCode.Throw);
				this._emitState.AdjustStack(-1);
			}
			this._emitState.InstructionAdded();
			this.EndBlock();
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x0008C141 File Offset: 0x0008A341
		private void EmitEndFinally()
		{
			this.GetCurrentBlock().SetBranchCode(ILOpCode.Endfinally);
			this.EndBlock();
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x0008C159 File Offset: 0x0008A359
		[NullableContext(1)]
		private ILBuilder.BasicBlock FinishFilterCondition()
		{
			ILBuilder.BasicBlock currentBlock = this.GetCurrentBlock();
			currentBlock.SetBranchCode(ILOpCode.Endfilter);
			this.EndBlock();
			return currentBlock;
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x0008C174 File Offset: 0x0008A374
		[NullableContext(1)]
		internal void EmitArrayCreation(IArrayTypeReference arrayType, SyntaxNode syntaxNode, DiagnosticBag diagnostics)
		{
			ArrayMethod arrayConstructor = this.module.ArrayMethods.GetArrayConstructor(arrayType);
			this.EmitOpCode(ILOpCode.Newobj, 1 - arrayType.Rank);
			this.EmitToken(arrayConstructor, syntaxNode, diagnostics);
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x0008C1AC File Offset: 0x0008A3AC
		[NullableContext(1)]
		internal void EmitArrayElementLoad(IArrayTypeReference arrayType, SyntaxNode syntaxNode, DiagnosticBag diagnostics)
		{
			ArrayMethod arrayGet = this.module.ArrayMethods.GetArrayGet(arrayType);
			this.EmitOpCode(ILOpCode.Call, -arrayType.Rank);
			this.EmitToken(arrayGet, syntaxNode, diagnostics);
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x0008C1E4 File Offset: 0x0008A3E4
		[NullableContext(1)]
		internal void EmitArrayElementAddress(IArrayTypeReference arrayType, SyntaxNode syntaxNode, DiagnosticBag diagnostics)
		{
			ArrayMethod arrayAddress = this.module.ArrayMethods.GetArrayAddress(arrayType);
			this.EmitOpCode(ILOpCode.Call, -arrayType.Rank);
			this.EmitToken(arrayAddress, syntaxNode, diagnostics);
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x0008C21C File Offset: 0x0008A41C
		[NullableContext(1)]
		internal void EmitArrayElementStore(IArrayTypeReference arrayType, SyntaxNode syntaxNode, DiagnosticBag diagnostics)
		{
			ArrayMethod arraySet = this.module.ArrayMethods.GetArraySet(arrayType);
			this.EmitOpCode(ILOpCode.Call, -(2 + arrayType.Rank));
			this.EmitToken(arraySet, syntaxNode, diagnostics);
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x0008C255 File Offset: 0x0008A455
		internal void EmitLoad(LocalOrParameter localOrParameter)
		{
			if (localOrParameter.Local != null)
			{
				this.EmitLocalLoad(localOrParameter.Local);
				return;
			}
			this.EmitLoadArgumentOpcode(localOrParameter.ParameterIndex);
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x0008C278 File Offset: 0x0008A478
		[NullableContext(1)]
		internal void EmitLocalLoad(LocalDefinition local)
		{
			int slotIndex = local.SlotIndex;
			switch (slotIndex)
			{
			case 0:
				this.EmitOpCode(ILOpCode.Ldloc_0);
				return;
			case 1:
				this.EmitOpCode(ILOpCode.Ldloc_1);
				return;
			case 2:
				this.EmitOpCode(ILOpCode.Ldloc_2);
				return;
			case 3:
				this.EmitOpCode(ILOpCode.Ldloc_3);
				return;
			default:
				if (slotIndex < 255)
				{
					this.EmitOpCode(ILOpCode.Ldloc_s);
					this.EmitInt8((sbyte)slotIndex);
					return;
				}
				this.EmitOpCode(ILOpCode.Ldloc);
				this.EmitInt32(slotIndex);
				return;
			}
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x0008C2F0 File Offset: 0x0008A4F0
		[NullableContext(1)]
		internal void EmitLocalStore(LocalDefinition local)
		{
			int slotIndex = local.SlotIndex;
			switch (slotIndex)
			{
			case 0:
				this.EmitOpCode(ILOpCode.Stloc_0);
				return;
			case 1:
				this.EmitOpCode(ILOpCode.Stloc_1);
				return;
			case 2:
				this.EmitOpCode(ILOpCode.Stloc_2);
				return;
			case 3:
				this.EmitOpCode(ILOpCode.Stloc_3);
				return;
			default:
				if (slotIndex < 255)
				{
					this.EmitOpCode(ILOpCode.Stloc_s);
					this.EmitInt8((sbyte)slotIndex);
					return;
				}
				this.EmitOpCode(ILOpCode.Stloc);
				this.EmitInt32(slotIndex);
				return;
			}
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x0008C36C File Offset: 0x0008A56C
		[NullableContext(1)]
		internal void EmitLocalAddress(LocalDefinition local)
		{
			if (local.IsReference)
			{
				this.EmitLocalLoad(local);
				return;
			}
			int slotIndex = local.SlotIndex;
			if (slotIndex < 255)
			{
				this.EmitOpCode(ILOpCode.Ldloca_s);
				this.EmitInt8((sbyte)slotIndex);
				return;
			}
			this.EmitOpCode(ILOpCode.Ldloca);
			this.EmitInt32(slotIndex);
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x0008C3BC File Offset: 0x0008A5BC
		internal void EmitLoadArgumentOpcode(int argNumber)
		{
			switch (argNumber)
			{
			case 0:
				this.EmitOpCode(ILOpCode.Ldarg_0);
				return;
			case 1:
				this.EmitOpCode(ILOpCode.Ldarg_1);
				return;
			case 2:
				this.EmitOpCode(ILOpCode.Ldarg_2);
				return;
			case 3:
				this.EmitOpCode(ILOpCode.Ldarg_3);
				return;
			default:
				if (argNumber < 255)
				{
					this.EmitOpCode(ILOpCode.Ldarg_s);
					this.EmitInt8((sbyte)argNumber);
					return;
				}
				this.EmitOpCode(ILOpCode.Ldarg);
				this.EmitInt32(argNumber);
				return;
			}
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x0008C42C File Offset: 0x0008A62C
		internal void EmitLoadArgumentAddrOpcode(int argNumber)
		{
			if (argNumber < 255)
			{
				this.EmitOpCode(ILOpCode.Ldarga_s);
				this.EmitInt8((sbyte)argNumber);
				return;
			}
			this.EmitOpCode(ILOpCode.Ldarga);
			this.EmitInt32(argNumber);
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x0008C459 File Offset: 0x0008A659
		internal void EmitStoreArgumentOpcode(int argNumber)
		{
			if (argNumber < 255)
			{
				this.EmitOpCode(ILOpCode.Starg_s);
				this.EmitInt8((sbyte)argNumber);
				return;
			}
			this.EmitOpCode(ILOpCode.Starg);
			this.EmitInt32(argNumber);
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x0008C488 File Offset: 0x0008A688
		[NullableContext(1)]
		internal void EmitConstantValue(ConstantValue value)
		{
			ConstantValueTypeDiscriminator discriminator = value.Discriminator;
			switch (discriminator)
			{
			case ConstantValueTypeDiscriminator.Nothing:
				this.EmitNullConstant();
				return;
			case ConstantValueTypeDiscriminator.SByte:
				this.EmitSByteConstant(value.SByteValue);
				return;
			case ConstantValueTypeDiscriminator.Byte:
				this.EmitByteConstant(value.ByteValue);
				return;
			case ConstantValueTypeDiscriminator.Int16:
				this.EmitShortConstant(value.Int16Value);
				return;
			case ConstantValueTypeDiscriminator.UInt16:
				this.EmitUShortConstant(value.UInt16Value);
				return;
			case ConstantValueTypeDiscriminator.Int32:
			case ConstantValueTypeDiscriminator.UInt32:
				this.EmitIntConstant(value.Int32Value);
				return;
			case ConstantValueTypeDiscriminator.Int64:
			case ConstantValueTypeDiscriminator.UInt64:
				this.EmitLongConstant(value.Int64Value);
				return;
			case ConstantValueTypeDiscriminator.NInt:
				this.EmitNativeIntConstant((long)value.Int32Value);
				return;
			case ConstantValueTypeDiscriminator.NUInt:
				this.EmitNativeIntConstant((long)((ulong)value.UInt32Value));
				return;
			case ConstantValueTypeDiscriminator.Char:
				this.EmitUShortConstant((ushort)value.CharValue);
				return;
			case ConstantValueTypeDiscriminator.Boolean:
				this.EmitBoolConstant(value.BooleanValue);
				return;
			case ConstantValueTypeDiscriminator.Single:
				this.EmitSingleConstant(value.SingleValue);
				return;
			case ConstantValueTypeDiscriminator.Double:
				this.EmitDoubleConstant(value.DoubleValue);
				return;
			case ConstantValueTypeDiscriminator.String:
				this.EmitStringConstant(value.StringValue);
				return;
			}
			throw ExceptionUtilities.UnexpectedValue(discriminator);
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x0008C5A8 File Offset: 0x0008A7A8
		internal void EmitIntConstant(int value)
		{
			ILOpCode ilopCode = ILOpCode.Nop;
			switch (value)
			{
			case -1:
				ilopCode = ILOpCode.Ldc_i4_m1;
				break;
			case 0:
				ilopCode = ILOpCode.Ldc_i4_0;
				break;
			case 1:
				ilopCode = ILOpCode.Ldc_i4_1;
				break;
			case 2:
				ilopCode = ILOpCode.Ldc_i4_2;
				break;
			case 3:
				ilopCode = ILOpCode.Ldc_i4_3;
				break;
			case 4:
				ilopCode = ILOpCode.Ldc_i4_4;
				break;
			case 5:
				ilopCode = ILOpCode.Ldc_i4_5;
				break;
			case 6:
				ilopCode = ILOpCode.Ldc_i4_6;
				break;
			case 7:
				ilopCode = ILOpCode.Ldc_i4_7;
				break;
			case 8:
				ilopCode = ILOpCode.Ldc_i4_8;
				break;
			}
			if (ilopCode != ILOpCode.Nop)
			{
				this.EmitOpCode(ilopCode);
				return;
			}
			if ((int)((sbyte)value) == value)
			{
				this.EmitOpCode(ILOpCode.Ldc_i4_s);
				this.EmitInt8((sbyte)value);
				return;
			}
			this.EmitOpCode(ILOpCode.Ldc_i4);
			this.EmitInt32(value);
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x0008C649 File Offset: 0x0008A849
		internal void EmitBoolConstant(bool value)
		{
			this.EmitIntConstant(value ? 1 : 0);
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x0008C658 File Offset: 0x0008A858
		internal void EmitByteConstant(byte value)
		{
			this.EmitIntConstant((int)value);
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x0008C661 File Offset: 0x0008A861
		internal void EmitSByteConstant(sbyte value)
		{
			this.EmitIntConstant((int)value);
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x0008C66A File Offset: 0x0008A86A
		internal void EmitShortConstant(short value)
		{
			this.EmitIntConstant((int)value);
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x0008C673 File Offset: 0x0008A873
		internal void EmitUShortConstant(ushort value)
		{
			this.EmitIntConstant((int)value);
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x0008C67C File Offset: 0x0008A87C
		internal void EmitLongConstant(long value)
		{
			if (value >= -2147483648L && value <= 2147483647L)
			{
				this.EmitIntConstant((int)value);
				this.EmitOpCode(ILOpCode.Conv_i8);
				return;
			}
			if (value >= 0L && value <= (long)((ulong)(-1)))
			{
				this.EmitIntConstant((int)value);
				this.EmitOpCode(ILOpCode.Conv_u8);
				return;
			}
			this.EmitOpCode(ILOpCode.Ldc_i8);
			this.EmitInt64(value);
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x0008C6D8 File Offset: 0x0008A8D8
		internal void EmitNativeIntConstant(long value)
		{
			if (value >= -2147483648L && value <= 2147483647L)
			{
				this.EmitIntConstant((int)value);
				this.EmitOpCode(ILOpCode.Conv_i);
				return;
			}
			if (value >= 0L && value <= (long)((ulong)(-1)))
			{
				this.EmitIntConstant((int)value);
				this.EmitOpCode(ILOpCode.Conv_u);
				return;
			}
			throw ExceptionUtilities.UnexpectedValue(value);
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x0008C734 File Offset: 0x0008A934
		internal void EmitSingleConstant(float value)
		{
			this.EmitOpCode(ILOpCode.Ldc_r4);
			this.EmitFloat(value);
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x0008C745 File Offset: 0x0008A945
		internal void EmitDoubleConstant(double value)
		{
			this.EmitOpCode(ILOpCode.Ldc_r8);
			this.EmitDouble(value);
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x0008C756 File Offset: 0x0008A956
		internal void EmitNullConstant()
		{
			this.EmitOpCode(ILOpCode.Ldnull);
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x0008C760 File Offset: 0x0008A960
		[NullableContext(2)]
		internal void EmitStringConstant(string value)
		{
			if (value == null)
			{
				this.EmitNullConstant();
				return;
			}
			this.EmitOpCode(ILOpCode.Ldstr);
			this.EmitToken(value);
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x0008C77B File Offset: 0x0008A97B
		private void EmitInt8(sbyte int8)
		{
			this.GetCurrentWriter().WriteSByte(int8);
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x0008C789 File Offset: 0x0008A989
		private void EmitInt32(int int32)
		{
			this.GetCurrentWriter().WriteInt32(int32);
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x0008C797 File Offset: 0x0008A997
		private void EmitInt64(long int64)
		{
			this.GetCurrentWriter().WriteInt64(int64);
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x0008C7A8 File Offset: 0x0008A9A8
		private void EmitFloat(float floatValue)
		{
			int num = BitConverter.ToInt32(BitConverter.GetBytes(floatValue), 0);
			this.GetCurrentWriter().WriteInt32(num);
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x0008C7D0 File Offset: 0x0008A9D0
		private void EmitDouble(double doubleValue)
		{
			long num = BitConverter.DoubleToInt64Bits(doubleValue);
			this.GetCurrentWriter().WriteInt64(num);
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x0008C7F0 File Offset: 0x0008A9F0
		[NullableContext(1)]
		private static void WriteOpCode(BlobBuilder writer, ILOpCode code)
		{
			if (code.Size() == 1)
			{
				writer.WriteByte((byte)code);
				return;
			}
			writer.WriteByte((byte)(code >> 8));
			writer.WriteByte((byte)(code & (ILOpCode)255));
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x0008C81C File Offset: 0x0008AA1C
		[NullableContext(1)]
		private BlobBuilder GetCurrentWriter()
		{
			return this.GetCurrentBlock().Writer;
		}

		// Token: 0x040012D9 RID: 4825
		private readonly OptimizationLevel _optimizations;

		// Token: 0x040012DA RID: 4826
		internal readonly LocalSlotManager LocalSlotManager;

		// Token: 0x040012DB RID: 4827
		private readonly ILBuilder.LocalScopeManager _scopeManager;

		// Token: 0x040012DC RID: 4828
		internal readonly ITokenDeferral module;

		// Token: 0x040012DD RID: 4829
		internal readonly ILBuilder.BasicBlock leaderBlock;

		// Token: 0x040012DE RID: 4830
		private ILBuilder.EmitState _emitState;

		// Token: 0x040012DF RID: 4831
		private ILBuilder.BasicBlock _lastCompleteBlock;

		// Token: 0x040012E0 RID: 4832
		private ILBuilder.BasicBlock _currentBlock;

		// Token: 0x040012E1 RID: 4833
		private SyntaxTree _lastSeqPointTree;

		// Token: 0x040012E2 RID: 4834
		private readonly SmallDictionary<object, ILBuilder.LabelInfo> _labelInfos;

		// Token: 0x040012E3 RID: 4835
		private readonly bool _areLocalsZeroed;

		// Token: 0x040012E4 RID: 4836
		private int _instructionCountAtLastLabel = -1;

		// Token: 0x040012E5 RID: 4837
		internal ImmutableArray<byte> RealizedIL;

		// Token: 0x040012E6 RID: 4838
		internal ImmutableArray<ExceptionHandlerRegion> RealizedExceptionHandlers;

		// Token: 0x040012E7 RID: 4839
		internal SequencePointList RealizedSequencePoints;

		// Token: 0x040012E8 RID: 4840
		public ArrayBuilder<RawSequencePoint> SeqPointsOpt;

		// Token: 0x040012E9 RID: 4841
		private ArrayBuilder<ILBuilder.ILMarker> _allocatedILMarkers;

		// Token: 0x040012EA RID: 4842
		private bool _pendingBlockCreate;

		// Token: 0x040012EB RID: 4843
		private int _initialHiddenSequencePointMarker = -1;

		// Token: 0x02000768 RID: 1896
		internal enum BlockType
		{
			// Token: 0x04001D38 RID: 7480
			Normal,
			// Token: 0x04001D39 RID: 7481
			Try,
			// Token: 0x04001D3A RID: 7482
			Catch,
			// Token: 0x04001D3B RID: 7483
			Filter,
			// Token: 0x04001D3C RID: 7484
			Finally,
			// Token: 0x04001D3D RID: 7485
			Fault,
			// Token: 0x04001D3E RID: 7486
			Switch
		}

		// Token: 0x02000769 RID: 1897
		internal enum Reachability : byte
		{
			// Token: 0x04001D40 RID: 7488
			NotReachable,
			// Token: 0x04001D41 RID: 7489
			Reachable,
			// Token: 0x04001D42 RID: 7490
			BlockedByFinally
		}

		// Token: 0x0200076A RID: 1898
		[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
		internal class BasicBlock
		{
			// Token: 0x06003E35 RID: 15925 RVA: 0x000AF630 File Offset: 0x000AD830
			private static ObjectPool<ILBuilder.BasicBlock> CreatePool(int size)
			{
				return new ObjectPool<ILBuilder.BasicBlock>(() => new ILBuilder.BasicBlock.PooledBasicBlock(), size);
			}

			// Token: 0x06003E36 RID: 15926 RVA: 0x000AF657 File Offset: 0x000AD857
			protected BasicBlock()
			{
			}

			// Token: 0x06003E37 RID: 15927 RVA: 0x000AF65F File Offset: 0x000AD85F
			internal BasicBlock(ILBuilder builder)
			{
				this.Initialize(builder);
			}

			// Token: 0x06003E38 RID: 15928 RVA: 0x000AF66E File Offset: 0x000AD86E
			internal void Initialize(ILBuilder builder)
			{
				this.builder = builder;
				this.FirstILMarker = -1;
				this.LastILMarker = -1;
			}

			// Token: 0x17001134 RID: 4404
			// (get) Token: 0x06003E39 RID: 15929 RVA: 0x000AF685 File Offset: 0x000AD885
			public PooledBlobBuilder Writer
			{
				get
				{
					if (this._lazyRegularInstructions == null)
					{
						this._lazyRegularInstructions = PooledBlobBuilder.GetInstance(1024);
					}
					return this._lazyRegularInstructions;
				}
			}

			// Token: 0x17001135 RID: 4405
			// (get) Token: 0x06003E3A RID: 15930 RVA: 0x000AF6A5 File Offset: 0x000AD8A5
			// (set) Token: 0x06003E3B RID: 15931 RVA: 0x000AF6AD File Offset: 0x000AD8AD
			public int FirstILMarker { get; private set; }

			// Token: 0x17001136 RID: 4406
			// (get) Token: 0x06003E3C RID: 15932 RVA: 0x000AF6B6 File Offset: 0x000AD8B6
			// (set) Token: 0x06003E3D RID: 15933 RVA: 0x000AF6BE File Offset: 0x000AD8BE
			public int LastILMarker { get; private set; }

			// Token: 0x06003E3E RID: 15934 RVA: 0x000AF6C7 File Offset: 0x000AD8C7
			public void AddILMarker(int marker)
			{
				if (this.FirstILMarker < 0)
				{
					this.FirstILMarker = marker;
				}
				this.LastILMarker = marker;
			}

			// Token: 0x06003E3F RID: 15935 RVA: 0x000AF6E0 File Offset: 0x000AD8E0
			public void RemoveTailILMarker(int marker)
			{
				if (this.FirstILMarker == this.LastILMarker)
				{
					this.FirstILMarker = -1;
					this.LastILMarker = -1;
					return;
				}
				int lastILMarker = this.LastILMarker;
				this.LastILMarker = lastILMarker - 1;
			}

			// Token: 0x17001137 RID: 4407
			// (get) Token: 0x06003E40 RID: 15936 RVA: 0x000AF71A File Offset: 0x000AD91A
			public virtual ILBuilder.ExceptionHandlerScope EnclosingHandler
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06003E41 RID: 15937 RVA: 0x000AF71D File Offset: 0x000AD91D
			internal virtual void Free()
			{
				if (this._lazyRegularInstructions != null)
				{
					this._lazyRegularInstructions.Free();
					this._lazyRegularInstructions = null;
				}
			}

			// Token: 0x17001138 RID: 4408
			// (get) Token: 0x06003E42 RID: 15938 RVA: 0x000AF739 File Offset: 0x000AD939
			public object BranchLabel
			{
				get
				{
					return this._branchLabel;
				}
			}

			// Token: 0x17001139 RID: 4409
			// (get) Token: 0x06003E43 RID: 15939 RVA: 0x000AF741 File Offset: 0x000AD941
			// (set) Token: 0x06003E44 RID: 15940 RVA: 0x000AF749 File Offset: 0x000AD949
			public ILOpCode BranchCode
			{
				get
				{
					return this._branchCode;
				}
				set
				{
					this._branchCode = value;
				}
			}

			// Token: 0x1700113A RID: 4410
			// (get) Token: 0x06003E45 RID: 15941 RVA: 0x000AF752 File Offset: 0x000AD952
			// (set) Token: 0x06003E46 RID: 15942 RVA: 0x000AF75A File Offset: 0x000AD95A
			public ILOpCode RevBranchCode
			{
				get
				{
					return (ILOpCode)this._revBranchCode;
				}
				set
				{
					this._revBranchCode = (byte)value;
				}
			}

			// Token: 0x1700113B RID: 4411
			// (get) Token: 0x06003E47 RID: 15943 RVA: 0x000AF764 File Offset: 0x000AD964
			public ILBuilder.BasicBlock BranchBlock
			{
				get
				{
					ILBuilder.BasicBlock basicBlock = null;
					if (this.BranchLabel != null)
					{
						basicBlock = this.builder._labelInfos[this.BranchLabel].bb;
					}
					return basicBlock;
				}
			}

			// Token: 0x06003E48 RID: 15944 RVA: 0x000AF798 File Offset: 0x000AD998
			public void SetBranchCode(ILOpCode newBranchCode)
			{
				this.BranchCode = newBranchCode;
			}

			// Token: 0x06003E49 RID: 15945 RVA: 0x000AF7A1 File Offset: 0x000AD9A1
			public void SetBranch(object newLabel, ILOpCode branchCode, ILOpCode revBranchCode)
			{
				this.SetBranch(newLabel, branchCode);
				this.RevBranchCode = revBranchCode;
			}

			// Token: 0x06003E4A RID: 15946 RVA: 0x000AF7B4 File Offset: 0x000AD9B4
			public void SetBranch(object newLabel, ILOpCode branchCode)
			{
				this.BranchCode = branchCode;
				if (this._branchLabel != newLabel)
				{
					this._branchLabel = newLabel;
					if (this.BranchCode.IsConditionalBranch())
					{
						ILBuilder.LabelInfo labelInfo = this.builder._labelInfos[newLabel];
						if (!labelInfo.targetOfConditionalBranches)
						{
							this.builder._labelInfos[newLabel] = labelInfo.SetTargetOfConditionalBranches();
						}
					}
				}
			}

			// Token: 0x1700113C RID: 4412
			// (get) Token: 0x06003E4B RID: 15947 RVA: 0x000AF817 File Offset: 0x000ADA17
			private bool IsBranchToLabel
			{
				get
				{
					return this.BranchLabel != null && this.BranchCode > ILOpCode.Nop;
				}
			}

			// Token: 0x1700113D RID: 4413
			// (get) Token: 0x06003E4C RID: 15948 RVA: 0x000AF82C File Offset: 0x000ADA2C
			public virtual ILBuilder.BlockType Type
			{
				get
				{
					return ILBuilder.BlockType.Normal;
				}
			}

			// Token: 0x1700113E RID: 4414
			// (get) Token: 0x06003E4D RID: 15949 RVA: 0x000AF82F File Offset: 0x000ADA2F
			public BlobBuilder RegularInstructions
			{
				get
				{
					return this._lazyRegularInstructions;
				}
			}

			// Token: 0x1700113F RID: 4415
			// (get) Token: 0x06003E4E RID: 15950 RVA: 0x000AF837 File Offset: 0x000ADA37
			public bool HasNoRegularInstructions
			{
				get
				{
					return this._lazyRegularInstructions == null;
				}
			}

			// Token: 0x17001140 RID: 4416
			// (get) Token: 0x06003E4F RID: 15951 RVA: 0x000AF842 File Offset: 0x000ADA42
			public int RegularInstructionsLength
			{
				get
				{
					PooledBlobBuilder lazyRegularInstructions = this._lazyRegularInstructions;
					if (lazyRegularInstructions == null)
					{
						return 0;
					}
					return lazyRegularInstructions.Count;
				}
			}

			// Token: 0x06003E50 RID: 15952 RVA: 0x000AF855 File Offset: 0x000ADA55
			internal void AdjustForDelta(int delta)
			{
				if (delta != 0)
				{
					this.Start += delta;
				}
			}

			// Token: 0x06003E51 RID: 15953 RVA: 0x000AF868 File Offset: 0x000ADA68
			internal void RewriteBranchesAcrossExceptionHandlers()
			{
				ILBuilder.ExceptionHandlerScope enclosingHandler = this.EnclosingHandler;
				ILBuilder.BasicBlock branchBlock = this.BranchBlock;
				if (branchBlock == null)
				{
					return;
				}
				if (branchBlock.EnclosingHandler != this.EnclosingHandler)
				{
					this.SetBranchCode(this.BranchCode.GetLeaveOpcode());
				}
			}

			// Token: 0x06003E52 RID: 15954 RVA: 0x000AF8A8 File Offset: 0x000ADAA8
			internal void ShortenBranches(ref int delta)
			{
				if (!this.IsBranchToLabel)
				{
					return;
				}
				ILOpCode branchCode = this.BranchCode;
				if (branchCode.GetBranchOperandSize() == 1)
				{
					return;
				}
				int start = this.BranchBlock.Start;
				int num;
				if (start > this.Start)
				{
					num = start - this.NextBlock.Start;
				}
				else
				{
					num = start - (this.Start + this.TotalSize + -3);
				}
				if ((int)((sbyte)num) == num)
				{
					this.SetBranchCode(branchCode.GetShortBranch());
					delta += -3;
				}
			}

			// Token: 0x06003E53 RID: 15955 RVA: 0x000AF920 File Offset: 0x000ADB20
			internal bool OptimizeBranches(ref int delta)
			{
				if (this.IsBranchToLabel)
				{
					ILBuilder.BasicBlock nextNontrivial = this.NextNontrivial;
					if (nextNontrivial != null)
					{
						if (this.TryOptimizeSameAsNext(nextNontrivial, ref delta))
						{
							return true;
						}
						if (this.TryOptimizeBranchToNextOrRet(nextNontrivial, ref delta))
						{
							return true;
						}
						if (this.TryOptimizeBranchOverUncondBranch(nextNontrivial, ref delta))
						{
							return true;
						}
						if (this.TryOptimizeBranchToEquivalent(nextNontrivial, ref delta))
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x17001141 RID: 4417
			// (get) Token: 0x06003E54 RID: 15956 RVA: 0x000AF970 File Offset: 0x000ADB70
			private ILBuilder.BasicBlock NextNontrivial
			{
				get
				{
					ILBuilder.BasicBlock basicBlock = this.NextBlock;
					while (basicBlock != null && basicBlock.BranchCode == ILOpCode.Nop && basicBlock.HasNoRegularInstructions)
					{
						basicBlock = basicBlock.NextBlock;
					}
					return basicBlock;
				}
			}

			// Token: 0x06003E55 RID: 15957 RVA: 0x000AF9A4 File Offset: 0x000ADBA4
			private bool TryOptimizeSameAsNext(ILBuilder.BasicBlock next, ref int delta)
			{
				if (next.HasNoRegularInstructions && next.BranchCode == this.BranchCode && next.BranchBlock.Start == this.BranchBlock.Start && next.EnclosingHandler == this.EnclosingHandler)
				{
					int num = this.BranchCode.Size() + this.BranchCode.GetBranchOperandSize();
					delta -= num;
					this.SetBranch(null, ILOpCode.Nop);
					if (this.HasNoRegularInstructions)
					{
						SmallDictionary<object, ILBuilder.LabelInfo> labelInfos = this.builder._labelInfos;
						foreach (object obj in labelInfos.Keys)
						{
							ILBuilder.LabelInfo labelInfo = labelInfos[obj];
							if (labelInfo.bb == this)
							{
								labelInfos[obj] = labelInfo.WithNewTarget(next);
							}
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x06003E56 RID: 15958 RVA: 0x000AFA80 File Offset: 0x000ADC80
			private bool TryOptimizeBranchOverUncondBranch(ILBuilder.BasicBlock next, ref int delta)
			{
				if (next.HasNoRegularInstructions && next.NextBlock != null && next.NextBlock.Start == this.BranchBlock.Start && (next.BranchCode == ILOpCode.Br || next.BranchCode == ILOpCode.Br_s) && next.BranchBlock != next)
				{
					ILOpCode ilopCode = this.GetReversedBranchOp();
					if (ilopCode != ILOpCode.Nop)
					{
						ILBuilder.BasicBlock basicBlock = this.NextBlock;
						ILBuilder.BasicBlock branchBlock = this.BranchBlock;
						while (basicBlock != branchBlock)
						{
							basicBlock.Reachability = ILBuilder.Reachability.NotReachable;
							basicBlock = basicBlock.NextBlock;
						}
						next.Reachability = ILBuilder.Reachability.NotReachable;
						delta -= next.TotalSize;
						if (next.BranchCode == ILOpCode.Br_s)
						{
							ilopCode = ilopCode.GetShortBranch();
						}
						this.NextBlock = this.BranchBlock;
						ILOpCode branchCode = this.BranchCode;
						this.SetBranch(next.BranchLabel, ilopCode, branchCode);
						return true;
					}
				}
				return false;
			}

			// Token: 0x06003E57 RID: 15959 RVA: 0x000AFB50 File Offset: 0x000ADD50
			private bool TryOptimizeBranchToNextOrRet(ILBuilder.BasicBlock next, ref int delta)
			{
				ILOpCode branchCode = this.BranchCode;
				if (branchCode == ILOpCode.Br || branchCode == ILOpCode.Br_s)
				{
					if (this.BranchBlock.Start - next.Start == 0)
					{
						this.SetBranch(null, ILOpCode.Nop);
						delta -= branchCode.Size() + branchCode.GetBranchOperandSize();
						return true;
					}
					if (this.BranchBlock.HasNoRegularInstructions && this.BranchBlock.BranchCode == ILOpCode.Ret)
					{
						this.SetBranch(null, ILOpCode.Ret);
						delta -= branchCode.Size() + branchCode.GetBranchOperandSize() - 1;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06003E58 RID: 15960 RVA: 0x000AFBDC File Offset: 0x000ADDDC
			private bool TryOptimizeBranchToEquivalent(ILBuilder.BasicBlock next, ref int delta)
			{
				ILOpCode branchCode = this.BranchCode;
				if (branchCode.IsConditionalBranch() && next.EnclosingHandler == this.EnclosingHandler && (this.BranchBlock.Start - next.Start == 0 || ILBuilder.BasicBlock.AreIdentical(this.BranchBlock, next)))
				{
					this.SetBranch(null, ILOpCode.Nop);
					this.Writer.WriteByte(38);
					delta -= branchCode.Size() + branchCode.GetBranchOperandSize() - 1;
					if (branchCode.IsRelationalBranch())
					{
						this.Writer.WriteByte(38);
						delta++;
					}
					return true;
				}
				return false;
			}

			// Token: 0x06003E59 RID: 15961 RVA: 0x000AFC70 File Offset: 0x000ADE70
			private static bool AreIdentical(ILBuilder.BasicBlock one, ILBuilder.BasicBlock another)
			{
				if (one._branchCode == another._branchCode && !one._branchCode.CanFallThrough() && one._branchLabel == another._branchLabel)
				{
					BlobBuilder regularInstructions = one.RegularInstructions;
					BlobBuilder regularInstructions2 = another.RegularInstructions;
					return regularInstructions == regularInstructions2 || (regularInstructions != null && regularInstructions.ContentEquals(regularInstructions2));
				}
				return false;
			}

			// Token: 0x06003E5A RID: 15962 RVA: 0x000AFCC8 File Offset: 0x000ADEC8
			private ILOpCode GetReversedBranchOp()
			{
				ILOpCode ilopCode = this.RevBranchCode;
				if (ilopCode != ILOpCode.Nop)
				{
					return ilopCode;
				}
				ILOpCode branchCode = this.BranchCode;
				if (branchCode <= ILOpCode.Bne_un_s)
				{
					switch (branchCode)
					{
					case ILOpCode.Brfalse_s:
						break;
					case ILOpCode.Brtrue_s:
						goto IL_0055;
					case ILOpCode.Beq_s:
						goto IL_005A;
					default:
						if (branchCode != ILOpCode.Bne_un_s)
						{
							return ilopCode;
						}
						goto IL_005F;
					}
				}
				else
				{
					switch (branchCode)
					{
					case ILOpCode.Brfalse:
						break;
					case ILOpCode.Brtrue:
						goto IL_0055;
					case ILOpCode.Beq:
						goto IL_005A;
					default:
						if (branchCode != ILOpCode.Bne_un)
						{
							return ilopCode;
						}
						goto IL_005F;
					}
				}
				return ILOpCode.Brtrue;
				IL_0055:
				return ILOpCode.Brfalse;
				IL_005A:
				return ILOpCode.Bne_un;
				IL_005F:
				ilopCode = ILOpCode.Beq;
				return ilopCode;
			}

			// Token: 0x17001142 RID: 4418
			// (get) Token: 0x06003E5B RID: 15963 RVA: 0x000AFD38 File Offset: 0x000ADF38
			public virtual int TotalSize
			{
				get
				{
					ILOpCode branchCode = this.BranchCode;
					int num;
					if (branchCode <= ILOpCode.Throw)
					{
						if (branchCode == ILOpCode.Nop)
						{
							num = 0;
							goto IL_004F;
						}
						if (branchCode != ILOpCode.Ret && branchCode != ILOpCode.Throw)
						{
							goto IL_0041;
						}
					}
					else if (branchCode != ILOpCode.Endfinally)
					{
						if (branchCode != ILOpCode.Endfilter && branchCode != ILOpCode.Rethrow)
						{
							goto IL_0041;
						}
						num = 2;
						goto IL_004F;
					}
					num = 1;
					goto IL_004F;
					IL_0041:
					num = 1 + this.BranchCode.GetBranchOperandSize();
					IL_004F:
					return this.RegularInstructionsLength + num;
				}
			}

			// Token: 0x06003E5C RID: 15964 RVA: 0x000AFD9C File Offset: 0x000ADF9C
			private string GetDebuggerDisplay()
			{
				return "";
			}

			// Token: 0x04001D43 RID: 7491
			public static readonly ObjectPool<ILBuilder.BasicBlock> Pool = ILBuilder.BasicBlock.CreatePool(32);

			// Token: 0x04001D44 RID: 7492
			internal ILBuilder builder;

			// Token: 0x04001D45 RID: 7493
			private PooledBlobBuilder _lazyRegularInstructions;

			// Token: 0x04001D48 RID: 7496
			public ILBuilder.BasicBlock NextBlock;

			// Token: 0x04001D49 RID: 7497
			private object _branchLabel;

			// Token: 0x04001D4A RID: 7498
			public int Start;

			// Token: 0x04001D4B RID: 7499
			private byte _revBranchCode;

			// Token: 0x04001D4C RID: 7500
			private ILOpCode _branchCode;

			// Token: 0x04001D4D RID: 7501
			internal ILBuilder.Reachability Reachability;

			// Token: 0x02000801 RID: 2049
			private class PooledBasicBlock : ILBuilder.BasicBlock
			{
				// Token: 0x060040E2 RID: 16610 RVA: 0x000B4E74 File Offset: 0x000B3074
				internal override void Free()
				{
					base.Free();
					this._branchLabel = null;
					base.BranchCode = ILOpCode.Nop;
					this._revBranchCode = 0;
					this.NextBlock = null;
					this.builder = null;
					this.Reachability = ILBuilder.Reachability.NotReachable;
					this.Start = 0;
					ILBuilder.BasicBlock.Pool.Free(this);
				}
			}
		}

		// Token: 0x0200076B RID: 1899
		internal class BasicBlockWithHandlerScope : ILBuilder.BasicBlock
		{
			// Token: 0x06003E5E RID: 15966 RVA: 0x000AFDB1 File Offset: 0x000ADFB1
			public BasicBlockWithHandlerScope(ILBuilder builder, ILBuilder.ExceptionHandlerScope enclosingHandler)
				: base(builder)
			{
				this.enclosingHandler = enclosingHandler;
			}

			// Token: 0x17001143 RID: 4419
			// (get) Token: 0x06003E5F RID: 15967 RVA: 0x000AFDC1 File Offset: 0x000ADFC1
			public override ILBuilder.ExceptionHandlerScope EnclosingHandler
			{
				get
				{
					return this.enclosingHandler;
				}
			}

			// Token: 0x04001D4E RID: 7502
			public readonly ILBuilder.ExceptionHandlerScope enclosingHandler;
		}

		// Token: 0x0200076C RID: 1900
		internal sealed class ExceptionHandlerLeaderBlock : ILBuilder.BasicBlockWithHandlerScope
		{
			// Token: 0x06003E60 RID: 15968 RVA: 0x000AFDC9 File Offset: 0x000ADFC9
			public ExceptionHandlerLeaderBlock(ILBuilder builder, ILBuilder.ExceptionHandlerScope enclosingHandler, ILBuilder.BlockType type)
				: base(builder, enclosingHandler)
			{
				this._type = type;
			}

			// Token: 0x17001144 RID: 4420
			// (get) Token: 0x06003E61 RID: 15969 RVA: 0x000AFDDA File Offset: 0x000ADFDA
			public override ILBuilder.BlockType Type
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x06003E62 RID: 15970 RVA: 0x000AFDE2 File Offset: 0x000ADFE2
			public override string ToString()
			{
				return string.Format("[{0}] {1}", this._type, base.ToString());
			}

			// Token: 0x04001D4F RID: 7503
			private readonly ILBuilder.BlockType _type;

			// Token: 0x04001D50 RID: 7504
			public ILBuilder.ExceptionHandlerLeaderBlock NextExceptionHandler;
		}

		// Token: 0x0200076D RID: 1901
		internal sealed class SwitchBlock : ILBuilder.BasicBlockWithHandlerScope
		{
			// Token: 0x06003E63 RID: 15971 RVA: 0x000AFDFF File Offset: 0x000ADFFF
			public SwitchBlock(ILBuilder builder, ILBuilder.ExceptionHandlerScope enclosingHandler)
				: base(builder, enclosingHandler)
			{
				base.SetBranchCode(ILOpCode.Switch);
			}

			// Token: 0x17001145 RID: 4421
			// (get) Token: 0x06003E64 RID: 15972 RVA: 0x000AFE11 File Offset: 0x000AE011
			public override ILBuilder.BlockType Type
			{
				get
				{
					return ILBuilder.BlockType.Switch;
				}
			}

			// Token: 0x17001146 RID: 4422
			// (get) Token: 0x06003E65 RID: 15973 RVA: 0x000AFE14 File Offset: 0x000AE014
			public uint BranchesCount
			{
				get
				{
					return (uint)this.BranchLabels.Length;
				}
			}

			// Token: 0x06003E66 RID: 15974 RVA: 0x000AFE20 File Offset: 0x000AE020
			public void GetBranchBlocks(ArrayBuilder<ILBuilder.BasicBlock> branchBlocksBuilder)
			{
				foreach (object obj in this.BranchLabels)
				{
					branchBlocksBuilder.Add(this.builder._labelInfos[obj].bb);
				}
			}

			// Token: 0x17001147 RID: 4423
			// (get) Token: 0x06003E67 RID: 15975 RVA: 0x000AFE64 File Offset: 0x000AE064
			public override int TotalSize
			{
				get
				{
					uint num = 5U + 4U * this.BranchesCount;
					return (int)((long)base.RegularInstructionsLength + (long)((ulong)num));
				}
			}

			// Token: 0x04001D51 RID: 7505
			public object[] BranchLabels;
		}

		// Token: 0x0200076E RID: 1902
		private struct EmitState
		{
			// Token: 0x17001148 RID: 4424
			// (get) Token: 0x06003E68 RID: 15976 RVA: 0x000AFE87 File Offset: 0x000AE087
			internal int InstructionsEmitted
			{
				get
				{
					return this._instructionsEmitted;
				}
			}

			// Token: 0x06003E69 RID: 15977 RVA: 0x000AFE8F File Offset: 0x000AE08F
			internal void InstructionAdded()
			{
				this._instructionsEmitted++;
			}

			// Token: 0x17001149 RID: 4425
			// (get) Token: 0x06003E6A RID: 15978 RVA: 0x000AFE9F File Offset: 0x000AE09F
			// (set) Token: 0x06003E6B RID: 15979 RVA: 0x000AFEA7 File Offset: 0x000AE0A7
			internal int MaxStack
			{
				get
				{
					return this._maxStack;
				}
				private set
				{
					this._maxStack = value;
				}
			}

			// Token: 0x1700114A RID: 4426
			// (get) Token: 0x06003E6C RID: 15980 RVA: 0x000AFEB0 File Offset: 0x000AE0B0
			// (set) Token: 0x06003E6D RID: 15981 RVA: 0x000AFEB8 File Offset: 0x000AE0B8
			internal int CurStack
			{
				get
				{
					return this._curStack;
				}
				private set
				{
					this._curStack = value;
				}
			}

			// Token: 0x06003E6E RID: 15982 RVA: 0x000AFEC1 File Offset: 0x000AE0C1
			internal void AdjustStack(int count)
			{
				this.CurStack += count;
				this.MaxStack = Math.Max(this.MaxStack, this.CurStack);
			}

			// Token: 0x04001D52 RID: 7506
			private int _maxStack;

			// Token: 0x04001D53 RID: 7507
			private int _curStack;

			// Token: 0x04001D54 RID: 7508
			private int _instructionsEmitted;
		}

		// Token: 0x0200076F RID: 1903
		private struct ILMarker
		{
			// Token: 0x04001D55 RID: 7509
			public int BlockOffset;

			// Token: 0x04001D56 RID: 7510
			public int AbsoluteOffset;
		}

		// Token: 0x02000770 RID: 1904
		[NullableContext(2)]
		[Nullable(0)]
		private struct LabelInfo
		{
			// Token: 0x06003E6F RID: 15983 RVA: 0x000AFEE8 File Offset: 0x000AE0E8
			internal LabelInfo(int stack, bool targetOfConditionalBranches)
			{
				this = new ILBuilder.LabelInfo(null, stack, targetOfConditionalBranches);
			}

			// Token: 0x06003E70 RID: 15984 RVA: 0x000AFEF3 File Offset: 0x000AE0F3
			internal LabelInfo(ILBuilder.BasicBlock bb, int stack, bool targetOfConditionalBranches)
			{
				this.stack = stack;
				this.bb = bb;
				this.targetOfConditionalBranches = targetOfConditionalBranches;
			}

			// Token: 0x06003E71 RID: 15985 RVA: 0x000AFF0A File Offset: 0x000AE10A
			internal ILBuilder.LabelInfo WithNewTarget(ILBuilder.BasicBlock bb)
			{
				return new ILBuilder.LabelInfo(bb, this.stack, this.targetOfConditionalBranches);
			}

			// Token: 0x06003E72 RID: 15986 RVA: 0x000AFF1E File Offset: 0x000AE11E
			internal ILBuilder.LabelInfo SetTargetOfConditionalBranches()
			{
				return new ILBuilder.LabelInfo(this.bb, this.stack, true);
			}

			// Token: 0x04001D57 RID: 7511
			internal readonly int stack;

			// Token: 0x04001D58 RID: 7512
			internal readonly ILBuilder.BasicBlock bb;

			// Token: 0x04001D59 RID: 7513
			internal readonly bool targetOfConditionalBranches;
		}

		// Token: 0x02000771 RID: 1905
		private sealed class LocalScopeManager
		{
			// Token: 0x06003E73 RID: 15987 RVA: 0x000AFF32 File Offset: 0x000AE132
			internal LocalScopeManager()
			{
				this._rootScope = new ILBuilder.LocalScopeInfo();
				this._scopes = new Stack<ILBuilder.ScopeInfo>(1);
				this._scopes.Push(this._rootScope);
			}

			// Token: 0x1700114B RID: 4427
			// (get) Token: 0x06003E74 RID: 15988 RVA: 0x000AFF62 File Offset: 0x000AE162
			private ILBuilder.ScopeInfo CurrentScope
			{
				get
				{
					return this._scopes.Peek();
				}
			}

			// Token: 0x06003E75 RID: 15989 RVA: 0x000AFF70 File Offset: 0x000AE170
			internal ILBuilder.ScopeInfo OpenScope(ScopeType scopeType, ITypeReference exceptionType)
			{
				ILBuilder.ScopeInfo scopeInfo = this.CurrentScope.OpenScope(scopeType, exceptionType, this._enclosingExceptionHandler);
				this._scopes.Push(scopeInfo);
				if (scopeInfo.IsExceptionHandler)
				{
					this._enclosingExceptionHandler = (ILBuilder.ExceptionHandlerScope)scopeInfo;
				}
				return scopeInfo;
			}

			// Token: 0x06003E76 RID: 15990 RVA: 0x000AFFB2 File Offset: 0x000AE1B2
			internal void FinishFilterCondition(ILBuilder builder)
			{
				this.CurrentScope.FinishFilterCondition(builder);
			}

			// Token: 0x06003E77 RID: 15991 RVA: 0x000AFFC0 File Offset: 0x000AE1C0
			internal void ClosingScope(ILBuilder builder)
			{
				this.CurrentScope.ClosingScope(builder);
			}

			// Token: 0x06003E78 RID: 15992 RVA: 0x000AFFCE File Offset: 0x000AE1CE
			internal void CloseScope(ILBuilder builder)
			{
				ILBuilder.ScopeInfo scopeInfo = this._scopes.Pop();
				scopeInfo.CloseScope(builder);
				if (scopeInfo.IsExceptionHandler)
				{
					this._enclosingExceptionHandler = this.GetEnclosingExceptionHandler();
				}
			}

			// Token: 0x1700114C RID: 4428
			// (get) Token: 0x06003E79 RID: 15993 RVA: 0x000AFFF5 File Offset: 0x000AE1F5
			internal ILBuilder.ExceptionHandlerScope EnclosingExceptionHandler
			{
				get
				{
					return this._enclosingExceptionHandler;
				}
			}

			// Token: 0x06003E7A RID: 15994 RVA: 0x000B0000 File Offset: 0x000AE200
			private ILBuilder.ExceptionHandlerScope GetEnclosingExceptionHandler()
			{
				foreach (ILBuilder.ScopeInfo scopeInfo in this._scopes)
				{
					ScopeType type = scopeInfo.Type;
					if (type - ScopeType.Try <= 4)
					{
						return (ILBuilder.ExceptionHandlerScope)scopeInfo;
					}
				}
				return null;
			}

			// Token: 0x06003E7B RID: 15995 RVA: 0x000B0068 File Offset: 0x000AE268
			internal ILBuilder.BasicBlock CreateBlock(ILBuilder builder)
			{
				return ((ILBuilder.LocalScopeInfo)this.CurrentScope).CreateBlock(builder);
			}

			// Token: 0x06003E7C RID: 15996 RVA: 0x000B007B File Offset: 0x000AE27B
			internal ILBuilder.SwitchBlock CreateSwitchBlock(ILBuilder builder)
			{
				return ((ILBuilder.LocalScopeInfo)this.CurrentScope).CreateSwitchBlock(builder);
			}

			// Token: 0x06003E7D RID: 15997 RVA: 0x000B008E File Offset: 0x000AE28E
			internal void AddLocal(LocalDefinition variable)
			{
				((ILBuilder.LocalScopeInfo)this.CurrentScope).AddLocal(variable);
			}

			// Token: 0x06003E7E RID: 15998 RVA: 0x000B00A1 File Offset: 0x000AE2A1
			internal void AddLocalConstant(LocalConstantDefinition constant)
			{
				((ILBuilder.LocalScopeInfo)this.CurrentScope).AddLocalConstant(constant);
			}

			// Token: 0x06003E7F RID: 15999 RVA: 0x000B00B4 File Offset: 0x000AE2B4
			internal ImmutableArray<Microsoft.Cci.LocalScope> GetAllScopesWithLocals()
			{
				ArrayBuilder<Microsoft.Cci.LocalScope> instance = ArrayBuilder<Microsoft.Cci.LocalScope>.GetInstance();
				ILBuilder.ScopeBounds localScopes = this._rootScope.GetLocalScopes(instance);
				int num = localScopes.End - localScopes.Begin;
				if (instance.Count > 0 && instance[instance.Count - 1].Length != num)
				{
					instance.Add(new Microsoft.Cci.LocalScope(0, num, ImmutableArray<ILocalDefinition>.Empty, ImmutableArray<ILocalDefinition>.Empty));
				}
				instance.Sort(ILBuilder.ScopeComparer.Instance);
				return instance.ToImmutableAndFree();
			}

			// Token: 0x06003E80 RID: 16000 RVA: 0x000B012C File Offset: 0x000AE32C
			internal ImmutableArray<ExceptionHandlerRegion> GetExceptionHandlerRegions()
			{
				ArrayBuilder<ExceptionHandlerRegion> instance = ArrayBuilder<ExceptionHandlerRegion>.GetInstance();
				this._rootScope.GetExceptionHandlerRegions(instance);
				return instance.ToImmutableAndFree();
			}

			// Token: 0x06003E81 RID: 16001 RVA: 0x000B0154 File Offset: 0x000AE354
			internal ImmutableArray<StateMachineHoistedLocalScope> GetHoistedLocalScopes()
			{
				ArrayBuilder<StateMachineHoistedLocalScope> instance = ArrayBuilder<StateMachineHoistedLocalScope>.GetInstance();
				this._rootScope.GetHoistedLocalScopes(instance);
				return instance.ToImmutableAndFree();
			}

			// Token: 0x06003E82 RID: 16002 RVA: 0x000B017A File Offset: 0x000AE37A
			internal void AddUserHoistedLocal(int slotIndex)
			{
				((ILBuilder.LocalScopeInfo)this.CurrentScope).AddUserHoistedLocal(slotIndex);
			}

			// Token: 0x06003E83 RID: 16003 RVA: 0x000B018D File Offset: 0x000AE38D
			internal void FreeBasicBlocks()
			{
				this._rootScope.FreeBasicBlocks();
			}

			// Token: 0x06003E84 RID: 16004 RVA: 0x000B019C File Offset: 0x000AE39C
			internal bool PossiblyDefinedOutsideOfTry(LocalDefinition local)
			{
				foreach (ILBuilder.ScopeInfo scopeInfo in this._scopes)
				{
					if (scopeInfo.ContainsLocal(local))
					{
						return false;
					}
					if (scopeInfo.Type == ScopeType.Try)
					{
						return true;
					}
				}
				return true;
			}

			// Token: 0x04001D5A RID: 7514
			private readonly ILBuilder.LocalScopeInfo _rootScope;

			// Token: 0x04001D5B RID: 7515
			private readonly Stack<ILBuilder.ScopeInfo> _scopes;

			// Token: 0x04001D5C RID: 7516
			private ILBuilder.ExceptionHandlerScope _enclosingExceptionHandler;
		}

		// Token: 0x02000772 RID: 1906
		internal abstract class ScopeInfo
		{
			// Token: 0x1700114D RID: 4429
			// (get) Token: 0x06003E85 RID: 16005
			public abstract ScopeType Type { get; }

			// Token: 0x06003E86 RID: 16006 RVA: 0x000B0208 File Offset: 0x000AE408
			public virtual ILBuilder.ScopeInfo OpenScope(ScopeType scopeType, ITypeReference exceptionType, ILBuilder.ExceptionHandlerScope currentHandler)
			{
				if (scopeType == ScopeType.TryCatchFinally)
				{
					return new ILBuilder.ExceptionHandlerContainerScope(currentHandler);
				}
				return new ILBuilder.LocalScopeInfo();
			}

			// Token: 0x06003E87 RID: 16007 RVA: 0x000B021A File Offset: 0x000AE41A
			public virtual void ClosingScope(ILBuilder builder)
			{
			}

			// Token: 0x06003E88 RID: 16008 RVA: 0x000B021C File Offset: 0x000AE41C
			public virtual void CloseScope(ILBuilder builder)
			{
			}

			// Token: 0x06003E89 RID: 16009 RVA: 0x000B021E File Offset: 0x000AE41E
			public virtual void FinishFilterCondition(ILBuilder builder)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x1700114E RID: 4430
			// (get) Token: 0x06003E8A RID: 16010 RVA: 0x000B0228 File Offset: 0x000AE428
			public bool IsExceptionHandler
			{
				get
				{
					ScopeType type = this.Type;
					return type - ScopeType.Try <= 4;
				}
			}

			// Token: 0x06003E8B RID: 16011
			internal abstract void GetExceptionHandlerRegions(ArrayBuilder<ExceptionHandlerRegion> regions);

			// Token: 0x06003E8C RID: 16012
			internal abstract ILBuilder.ScopeBounds GetLocalScopes(ArrayBuilder<Microsoft.Cci.LocalScope> result);

			// Token: 0x06003E8D RID: 16013 RVA: 0x000B0248 File Offset: 0x000AE448
			protected static ILBuilder.ScopeBounds GetLocalScopes<TScopeInfo>(ArrayBuilder<Microsoft.Cci.LocalScope> result, ImmutableArray<TScopeInfo>.Builder scopes) where TScopeInfo : ILBuilder.ScopeInfo
			{
				int num = int.MaxValue;
				int num2 = 0;
				foreach (TScopeInfo tscopeInfo in scopes)
				{
					ILBuilder.ScopeBounds localScopes = tscopeInfo.GetLocalScopes(result);
					num = Math.Min(num, localScopes.Begin);
					num2 = Math.Max(num2, localScopes.End);
				}
				return new ILBuilder.ScopeBounds(num, num2);
			}

			// Token: 0x06003E8E RID: 16014
			internal abstract ILBuilder.ScopeBounds GetHoistedLocalScopes(ArrayBuilder<StateMachineHoistedLocalScope> result);

			// Token: 0x06003E8F RID: 16015 RVA: 0x000B02C0 File Offset: 0x000AE4C0
			protected static ILBuilder.ScopeBounds GetHoistedLocalScopes<TScopeInfo>(ArrayBuilder<StateMachineHoistedLocalScope> result, ImmutableArray<TScopeInfo>.Builder scopes) where TScopeInfo : ILBuilder.ScopeInfo
			{
				int num = int.MaxValue;
				int num2 = 0;
				foreach (TScopeInfo tscopeInfo in scopes)
				{
					ILBuilder.ScopeBounds hoistedLocalScopes = tscopeInfo.GetHoistedLocalScopes(result);
					num = Math.Min(num, hoistedLocalScopes.Begin);
					num2 = Math.Max(num2, hoistedLocalScopes.End);
				}
				return new ILBuilder.ScopeBounds(num, num2);
			}

			// Token: 0x06003E90 RID: 16016
			public abstract void FreeBasicBlocks();

			// Token: 0x06003E91 RID: 16017 RVA: 0x000B0338 File Offset: 0x000AE538
			internal virtual bool ContainsLocal(LocalDefinition local)
			{
				return false;
			}
		}

		// Token: 0x02000773 RID: 1907
		internal class LocalScopeInfo : ILBuilder.ScopeInfo
		{
			// Token: 0x1700114F RID: 4431
			// (get) Token: 0x06003E93 RID: 16019 RVA: 0x000B0343 File Offset: 0x000AE543
			public override ScopeType Type
			{
				get
				{
					return ScopeType.Variable;
				}
			}

			// Token: 0x06003E94 RID: 16020 RVA: 0x000B0348 File Offset: 0x000AE548
			public override ILBuilder.ScopeInfo OpenScope(ScopeType scopeType, ITypeReference exceptionType, ILBuilder.ExceptionHandlerScope currentExceptionHandler)
			{
				ILBuilder.ScopeInfo scopeInfo = base.OpenScope(scopeType, exceptionType, currentExceptionHandler);
				if (this._nestedScopes == null)
				{
					this._nestedScopes = ImmutableArray.CreateBuilder<ILBuilder.ScopeInfo>(1);
				}
				this._nestedScopes.Add(scopeInfo);
				return scopeInfo;
			}

			// Token: 0x06003E95 RID: 16021 RVA: 0x000B0380 File Offset: 0x000AE580
			internal void AddLocal(LocalDefinition variable)
			{
				if (this._localVariables == null)
				{
					this._localVariables = ImmutableArray.CreateBuilder<LocalDefinition>(1);
				}
				this._localVariables.Add(variable);
			}

			// Token: 0x06003E96 RID: 16022 RVA: 0x000B03A2 File Offset: 0x000AE5A2
			internal void AddLocalConstant(LocalConstantDefinition constant)
			{
				if (this._localConstants == null)
				{
					this._localConstants = ImmutableArray.CreateBuilder<LocalConstantDefinition>(1);
				}
				this._localConstants.Add(constant);
			}

			// Token: 0x06003E97 RID: 16023 RVA: 0x000B03C4 File Offset: 0x000AE5C4
			internal void AddUserHoistedLocal(int slotIndex)
			{
				if (this._stateMachineUserHoistedLocalSlotIndices == null)
				{
					this._stateMachineUserHoistedLocalSlotIndices = ImmutableArray.CreateBuilder<int>(1);
				}
				this._stateMachineUserHoistedLocalSlotIndices.Add(slotIndex);
			}

			// Token: 0x06003E98 RID: 16024 RVA: 0x000B03E8 File Offset: 0x000AE5E8
			internal override bool ContainsLocal(LocalDefinition local)
			{
				ImmutableArray<LocalDefinition>.Builder localVariables = this._localVariables;
				return localVariables != null && localVariables.Contains(local);
			}

			// Token: 0x06003E99 RID: 16025 RVA: 0x000B0408 File Offset: 0x000AE608
			public virtual ILBuilder.BasicBlock CreateBlock(ILBuilder builder)
			{
				ILBuilder.ExceptionHandlerScope enclosingExceptionHandler = builder.EnclosingExceptionHandler;
				ILBuilder.BasicBlock basicBlock = ((enclosingExceptionHandler == null) ? ILBuilder.LocalScopeInfo.AllocatePooledBlock(builder) : new ILBuilder.BasicBlockWithHandlerScope(builder, enclosingExceptionHandler));
				this.AddBlock(basicBlock);
				return basicBlock;
			}

			// Token: 0x06003E9A RID: 16026 RVA: 0x000B0437 File Offset: 0x000AE637
			private static ILBuilder.BasicBlock AllocatePooledBlock(ILBuilder builder)
			{
				ILBuilder.BasicBlock basicBlock = ILBuilder.BasicBlock.Pool.Allocate();
				basicBlock.Initialize(builder);
				return basicBlock;
			}

			// Token: 0x06003E9B RID: 16027 RVA: 0x000B044C File Offset: 0x000AE64C
			public ILBuilder.SwitchBlock CreateSwitchBlock(ILBuilder builder)
			{
				ILBuilder.SwitchBlock switchBlock = new ILBuilder.SwitchBlock(builder, builder.EnclosingExceptionHandler);
				this.AddBlock(switchBlock);
				return switchBlock;
			}

			// Token: 0x06003E9C RID: 16028 RVA: 0x000B046E File Offset: 0x000AE66E
			protected void AddBlock(ILBuilder.BasicBlock block)
			{
				if (this.Blocks == null)
				{
					this.Blocks = ImmutableArray.CreateBuilder<ILBuilder.BasicBlock>(4);
				}
				this.Blocks.Add(block);
			}

			// Token: 0x06003E9D RID: 16029 RVA: 0x000B0490 File Offset: 0x000AE690
			internal override void GetExceptionHandlerRegions(ArrayBuilder<ExceptionHandlerRegion> regions)
			{
				if (this._nestedScopes != null)
				{
					int i = 0;
					int count = this._nestedScopes.Count;
					while (i < count)
					{
						this._nestedScopes[i].GetExceptionHandlerRegions(regions);
						i++;
					}
				}
			}

			// Token: 0x06003E9E RID: 16030 RVA: 0x000B04D0 File Offset: 0x000AE6D0
			internal override ILBuilder.ScopeBounds GetLocalScopes(ArrayBuilder<Microsoft.Cci.LocalScope> result)
			{
				int num = int.MaxValue;
				int num2 = 0;
				if (this.Blocks != null)
				{
					for (int i = 0; i < this.Blocks.Count; i++)
					{
						ILBuilder.BasicBlock basicBlock = this.Blocks[i];
						if (basicBlock.Reachability != ILBuilder.Reachability.NotReachable)
						{
							num = Math.Min(num, basicBlock.Start);
							num2 = Math.Max(num2, basicBlock.Start + basicBlock.TotalSize);
						}
					}
				}
				if (this._nestedScopes != null)
				{
					ILBuilder.ScopeBounds localScopes = ILBuilder.ScopeInfo.GetLocalScopes<ILBuilder.ScopeInfo>(result, this._nestedScopes);
					num = Math.Min(num, localScopes.Begin);
					num2 = Math.Max(num2, localScopes.End);
				}
				if ((this._localVariables != null || this._localConstants != null) && num2 > num)
				{
					Microsoft.Cci.LocalScope localScope = new Microsoft.Cci.LocalScope(num, num2, this._localConstants.AsImmutableOrEmpty<ILocalDefinition>(), this._localVariables.AsImmutableOrEmpty<ILocalDefinition>());
					result.Add(localScope);
				}
				return new ILBuilder.ScopeBounds(num, num2);
			}

			// Token: 0x06003E9F RID: 16031 RVA: 0x000B05B0 File Offset: 0x000AE7B0
			internal override ILBuilder.ScopeBounds GetHoistedLocalScopes(ArrayBuilder<StateMachineHoistedLocalScope> result)
			{
				int num = int.MaxValue;
				int num2 = 0;
				if (this.Blocks != null)
				{
					for (int i = 0; i < this.Blocks.Count; i++)
					{
						ILBuilder.BasicBlock basicBlock = this.Blocks[i];
						if (basicBlock.Reachability != ILBuilder.Reachability.NotReachable)
						{
							num = Math.Min(num, basicBlock.Start);
							num2 = Math.Max(num2, basicBlock.Start + basicBlock.TotalSize);
						}
					}
				}
				if (this._nestedScopes != null)
				{
					ILBuilder.ScopeBounds hoistedLocalScopes = ILBuilder.ScopeInfo.GetHoistedLocalScopes<ILBuilder.ScopeInfo>(result, this._nestedScopes);
					num = Math.Min(num, hoistedLocalScopes.Begin);
					num2 = Math.Max(num2, hoistedLocalScopes.End);
				}
				if (this._stateMachineUserHoistedLocalSlotIndices != null && num2 > num)
				{
					StateMachineHoistedLocalScope stateMachineHoistedLocalScope = new StateMachineHoistedLocalScope(num, num2);
					foreach (int num3 in this._stateMachineUserHoistedLocalSlotIndices)
					{
						while (result.Count <= num3)
						{
							result.Add(default(StateMachineHoistedLocalScope));
						}
						result[num3] = stateMachineHoistedLocalScope;
					}
				}
				return new ILBuilder.ScopeBounds(num, num2);
			}

			// Token: 0x06003EA0 RID: 16032 RVA: 0x000B06CC File Offset: 0x000AE8CC
			public override void FreeBasicBlocks()
			{
				if (this.Blocks != null)
				{
					int i = 0;
					int count = this.Blocks.Count;
					while (i < count)
					{
						this.Blocks[i].Free();
						i++;
					}
				}
				if (this._nestedScopes != null)
				{
					int j = 0;
					int count2 = this._nestedScopes.Count;
					while (j < count2)
					{
						this._nestedScopes[j].FreeBasicBlocks();
						j++;
					}
				}
			}

			// Token: 0x04001D5D RID: 7517
			private ImmutableArray<LocalDefinition>.Builder _localVariables;

			// Token: 0x04001D5E RID: 7518
			private ImmutableArray<LocalConstantDefinition>.Builder _localConstants;

			// Token: 0x04001D5F RID: 7519
			private ImmutableArray<int>.Builder _stateMachineUserHoistedLocalSlotIndices;

			// Token: 0x04001D60 RID: 7520
			private ImmutableArray<ILBuilder.ScopeInfo>.Builder _nestedScopes;

			// Token: 0x04001D61 RID: 7521
			protected ImmutableArray<ILBuilder.BasicBlock>.Builder Blocks;
		}

		// Token: 0x02000774 RID: 1908
		internal sealed class ExceptionHandlerScope : ILBuilder.LocalScopeInfo
		{
			// Token: 0x06003EA2 RID: 16034 RVA: 0x000B0743 File Offset: 0x000AE943
			public ExceptionHandlerScope(ILBuilder.ExceptionHandlerContainerScope containingScope, ScopeType type, ITypeReference exceptionType)
			{
				this._containingScope = containingScope;
				this._type = type;
				this._exceptionType = exceptionType;
			}

			// Token: 0x17001150 RID: 4432
			// (get) Token: 0x06003EA3 RID: 16035 RVA: 0x000B0760 File Offset: 0x000AE960
			public ILBuilder.ExceptionHandlerContainerScope ContainingExceptionScope
			{
				get
				{
					return this._containingScope;
				}
			}

			// Token: 0x17001151 RID: 4433
			// (get) Token: 0x06003EA4 RID: 16036 RVA: 0x000B0768 File Offset: 0x000AE968
			public override ScopeType Type
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x17001152 RID: 4434
			// (get) Token: 0x06003EA5 RID: 16037 RVA: 0x000B0770 File Offset: 0x000AE970
			public ITypeReference ExceptionType
			{
				get
				{
					return this._exceptionType;
				}
			}

			// Token: 0x06003EA6 RID: 16038 RVA: 0x000B0778 File Offset: 0x000AE978
			public void SetBlockedByFinallyDestination(object label)
			{
				this._blockedByFinallyDestination = label;
			}

			// Token: 0x17001153 RID: 4435
			// (get) Token: 0x06003EA7 RID: 16039 RVA: 0x000B0781 File Offset: 0x000AE981
			public object BlockedByFinallyDestination
			{
				get
				{
					return this._blockedByFinallyDestination;
				}
			}

			// Token: 0x06003EA8 RID: 16040 RVA: 0x000B0789 File Offset: 0x000AE989
			public void UnblockFinally()
			{
				this._blockedByFinallyDestination = null;
			}

			// Token: 0x17001154 RID: 4436
			// (get) Token: 0x06003EA9 RID: 16041 RVA: 0x000B0792 File Offset: 0x000AE992
			public int FilterHandlerStart
			{
				get
				{
					return this._lastFilterConditionBlock.Start + this._lastFilterConditionBlock.TotalSize;
				}
			}

			// Token: 0x06003EAA RID: 16042 RVA: 0x000B07AB File Offset: 0x000AE9AB
			public override void FinishFilterCondition(ILBuilder builder)
			{
				this._lastFilterConditionBlock = builder.FinishFilterCondition();
			}

			// Token: 0x06003EAB RID: 16043 RVA: 0x000B07BC File Offset: 0x000AE9BC
			public override void ClosingScope(ILBuilder builder)
			{
				ScopeType type = this._type;
				if (type - ScopeType.Finally <= 1)
				{
					builder.EmitEndFinally();
					return;
				}
				object endLabel = this._containingScope.EndLabel;
				builder.EmitBranch(ILOpCode.Br, endLabel, ILOpCode.Nop);
			}

			// Token: 0x06003EAC RID: 16044 RVA: 0x000B07F3 File Offset: 0x000AE9F3
			public override void CloseScope(ILBuilder builder)
			{
			}

			// Token: 0x06003EAD RID: 16045 RVA: 0x000B07F8 File Offset: 0x000AE9F8
			public override ILBuilder.BasicBlock CreateBlock(ILBuilder builder)
			{
				ILBuilder.BasicBlockWithHandlerScope basicBlockWithHandlerScope = ((this.Blocks == null) ? new ILBuilder.ExceptionHandlerLeaderBlock(builder, this, this.GetLeaderBlockType()) : new ILBuilder.BasicBlockWithHandlerScope(builder, this));
				base.AddBlock(basicBlockWithHandlerScope);
				return basicBlockWithHandlerScope;
			}

			// Token: 0x17001155 RID: 4437
			// (get) Token: 0x06003EAE RID: 16046 RVA: 0x000B082C File Offset: 0x000AEA2C
			public ILBuilder.ExceptionHandlerLeaderBlock LeaderBlock
			{
				get
				{
					ImmutableArray<ILBuilder.BasicBlock>.Builder blocks = this.Blocks;
					return (ILBuilder.ExceptionHandlerLeaderBlock)((blocks != null) ? blocks[0] : null);
				}
			}

			// Token: 0x06003EAF RID: 16047 RVA: 0x000B0848 File Offset: 0x000AEA48
			private ILBuilder.BlockType GetLeaderBlockType()
			{
				switch (this._type)
				{
				case ScopeType.Try:
					return ILBuilder.BlockType.Try;
				case ScopeType.Catch:
					return ILBuilder.BlockType.Catch;
				case ScopeType.Filter:
					return ILBuilder.BlockType.Filter;
				case ScopeType.Finally:
					return ILBuilder.BlockType.Finally;
				default:
					return ILBuilder.BlockType.Fault;
				}
			}

			// Token: 0x06003EB0 RID: 16048 RVA: 0x000B087F File Offset: 0x000AEA7F
			public override void FreeBasicBlocks()
			{
				base.FreeBasicBlocks();
			}

			// Token: 0x04001D62 RID: 7522
			private readonly ILBuilder.ExceptionHandlerContainerScope _containingScope;

			// Token: 0x04001D63 RID: 7523
			private readonly ScopeType _type;

			// Token: 0x04001D64 RID: 7524
			private readonly ITypeReference _exceptionType;

			// Token: 0x04001D65 RID: 7525
			private ILBuilder.BasicBlock _lastFilterConditionBlock;

			// Token: 0x04001D66 RID: 7526
			private object _blockedByFinallyDestination;
		}

		// Token: 0x02000775 RID: 1909
		internal sealed class ExceptionHandlerContainerScope : ILBuilder.ScopeInfo
		{
			// Token: 0x06003EB1 RID: 16049 RVA: 0x000B0887 File Offset: 0x000AEA87
			public ExceptionHandlerContainerScope(ILBuilder.ExceptionHandlerScope containingHandler)
			{
				this._handlers = ImmutableArray.CreateBuilder<ILBuilder.ExceptionHandlerScope>(2);
				this._containingHandler = containingHandler;
				this._endLabel = new object();
			}

			// Token: 0x17001156 RID: 4438
			// (get) Token: 0x06003EB2 RID: 16050 RVA: 0x000B08AD File Offset: 0x000AEAAD
			public ILBuilder.ExceptionHandlerScope ContainingHandler
			{
				get
				{
					return this._containingHandler;
				}
			}

			// Token: 0x17001157 RID: 4439
			// (get) Token: 0x06003EB3 RID: 16051 RVA: 0x000B08B5 File Offset: 0x000AEAB5
			public object EndLabel
			{
				get
				{
					return this._endLabel;
				}
			}

			// Token: 0x17001158 RID: 4440
			// (get) Token: 0x06003EB4 RID: 16052 RVA: 0x000B08BD File Offset: 0x000AEABD
			public override ScopeType Type
			{
				get
				{
					return ScopeType.TryCatchFinally;
				}
			}

			// Token: 0x06003EB5 RID: 16053 RVA: 0x000B08C0 File Offset: 0x000AEAC0
			public override ILBuilder.ScopeInfo OpenScope(ScopeType scopeType, ITypeReference exceptionType, ILBuilder.ExceptionHandlerScope currentExceptionHandler)
			{
				ILBuilder.ExceptionHandlerScope exceptionHandlerScope = new ILBuilder.ExceptionHandlerScope(this, scopeType, exceptionType);
				this._handlers.Add(exceptionHandlerScope);
				return exceptionHandlerScope;
			}

			// Token: 0x06003EB6 RID: 16054 RVA: 0x000B08E4 File Offset: 0x000AEAE4
			public override void CloseScope(ILBuilder builder)
			{
				ILBuilder.ExceptionHandlerLeaderBlock exceptionHandlerLeaderBlock = this._handlers[0].LeaderBlock;
				for (int i = 1; i < this._handlers.Count; i++)
				{
					ILBuilder.ExceptionHandlerLeaderBlock leaderBlock = this._handlers[i].LeaderBlock;
					exceptionHandlerLeaderBlock.NextExceptionHandler = leaderBlock;
					exceptionHandlerLeaderBlock = leaderBlock;
				}
				builder.MarkLabel(this._endLabel);
				builder.DefineHiddenSequencePoint();
				if (this._handlers[1].Type == ScopeType.Finally)
				{
					builder.EmitBranch(ILOpCode.Nop, this._endLabel, ILOpCode.Nop);
					this._handlers[1].SetBlockedByFinallyDestination(this._endLabel);
				}
			}

			// Token: 0x06003EB7 RID: 16055 RVA: 0x000B0980 File Offset: 0x000AEB80
			internal override void GetExceptionHandlerRegions(ArrayBuilder<ExceptionHandlerRegion> regions)
			{
				ILBuilder.ExceptionHandlerScope exceptionHandlerScope = null;
				ILBuilder.ScopeBounds scopeBounds = default(ILBuilder.ScopeBounds);
				foreach (ILBuilder.ExceptionHandlerScope exceptionHandlerScope2 in this._handlers)
				{
					exceptionHandlerScope2.GetExceptionHandlerRegions(regions);
					ILBuilder.ScopeBounds bounds = ILBuilder.ExceptionHandlerContainerScope.GetBounds(exceptionHandlerScope2);
					if (exceptionHandlerScope == null)
					{
						exceptionHandlerScope = exceptionHandlerScope2;
						scopeBounds = bounds;
						if (exceptionHandlerScope.LeaderBlock.Reachability != ILBuilder.Reachability.Reachable)
						{
							break;
						}
					}
					else
					{
						ExceptionHandlerRegion exceptionHandlerRegion;
						switch (exceptionHandlerScope2.Type)
						{
						case ScopeType.Catch:
							exceptionHandlerRegion = new ExceptionHandlerRegionCatch(scopeBounds.Begin, scopeBounds.End, bounds.Begin, bounds.End, exceptionHandlerScope2.ExceptionType);
							break;
						case ScopeType.Filter:
							exceptionHandlerRegion = new ExceptionHandlerRegionFilter(scopeBounds.Begin, scopeBounds.End, exceptionHandlerScope2.FilterHandlerStart, bounds.End, bounds.Begin);
							break;
						case ScopeType.Finally:
							exceptionHandlerRegion = new ExceptionHandlerRegionFinally(scopeBounds.Begin, scopeBounds.End, bounds.Begin, bounds.End);
							break;
						case ScopeType.Fault:
							exceptionHandlerRegion = new ExceptionHandlerRegionFault(scopeBounds.Begin, scopeBounds.End, bounds.Begin, bounds.End);
							break;
						default:
							throw ExceptionUtilities.UnexpectedValue(exceptionHandlerScope2.Type);
						}
						regions.Add(exceptionHandlerRegion);
					}
				}
			}

			// Token: 0x06003EB8 RID: 16056 RVA: 0x000B0AEC File Offset: 0x000AECEC
			internal override ILBuilder.ScopeBounds GetLocalScopes(ArrayBuilder<Microsoft.Cci.LocalScope> scopesWithVariables)
			{
				return ILBuilder.ScopeInfo.GetLocalScopes<ILBuilder.ExceptionHandlerScope>(scopesWithVariables, this._handlers);
			}

			// Token: 0x06003EB9 RID: 16057 RVA: 0x000B0AFA File Offset: 0x000AECFA
			internal override ILBuilder.ScopeBounds GetHoistedLocalScopes(ArrayBuilder<StateMachineHoistedLocalScope> result)
			{
				return ILBuilder.ScopeInfo.GetHoistedLocalScopes<ILBuilder.ExceptionHandlerScope>(result, this._handlers);
			}

			// Token: 0x06003EBA RID: 16058 RVA: 0x000B0B08 File Offset: 0x000AED08
			private static ILBuilder.ScopeBounds GetBounds(ILBuilder.ExceptionHandlerScope scope)
			{
				ArrayBuilder<Microsoft.Cci.LocalScope> instance = ArrayBuilder<Microsoft.Cci.LocalScope>.GetInstance();
				ILBuilder.ScopeBounds localScopes = scope.GetLocalScopes(instance);
				instance.Free();
				return localScopes;
			}

			// Token: 0x06003EBB RID: 16059 RVA: 0x000B0B28 File Offset: 0x000AED28
			public override void FreeBasicBlocks()
			{
				foreach (ILBuilder.ExceptionHandlerScope exceptionHandlerScope in this._handlers)
				{
					exceptionHandlerScope.FreeBasicBlocks();
				}
			}

			// Token: 0x06003EBC RID: 16060 RVA: 0x000B0B74 File Offset: 0x000AED74
			internal bool FinallyOnly()
			{
				ILBuilder.ExceptionHandlerContainerScope exceptionHandlerContainerScope = this;
				for (;;)
				{
					ImmutableArray<ILBuilder.ExceptionHandlerScope>.Builder handlers = exceptionHandlerContainerScope._handlers;
					if (handlers.Count != 2 || handlers[1].Type != ScopeType.Finally)
					{
						break;
					}
					ILBuilder.ExceptionHandlerScope containingHandler = exceptionHandlerContainerScope._containingHandler;
					exceptionHandlerContainerScope = ((containingHandler != null) ? containingHandler.ContainingExceptionScope : null);
					if (exceptionHandlerContainerScope == null)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04001D67 RID: 7527
			private readonly ImmutableArray<ILBuilder.ExceptionHandlerScope>.Builder _handlers;

			// Token: 0x04001D68 RID: 7528
			private readonly object _endLabel;

			// Token: 0x04001D69 RID: 7529
			private readonly ILBuilder.ExceptionHandlerScope _containingHandler;
		}

		// Token: 0x02000776 RID: 1910
		internal struct ScopeBounds
		{
			// Token: 0x06003EBD RID: 16061 RVA: 0x000B0BBB File Offset: 0x000AEDBB
			internal ScopeBounds(int begin, int end)
			{
				this.Begin = begin;
				this.End = end;
			}

			// Token: 0x04001D6A RID: 7530
			internal readonly int Begin;

			// Token: 0x04001D6B RID: 7531
			internal readonly int End;
		}

		// Token: 0x02000777 RID: 1911
		private sealed class ScopeComparer : IComparer<Microsoft.Cci.LocalScope>
		{
			// Token: 0x06003EBE RID: 16062 RVA: 0x000B0BCB File Offset: 0x000AEDCB
			private ScopeComparer()
			{
			}

			// Token: 0x06003EBF RID: 16063 RVA: 0x000B0BD4 File Offset: 0x000AEDD4
			public int Compare(Microsoft.Cci.LocalScope x, Microsoft.Cci.LocalScope y)
			{
				int num = x.StartOffset.CompareTo(y.StartOffset);
				if (num != 0)
				{
					return num;
				}
				return y.EndOffset.CompareTo(x.EndOffset);
			}

			// Token: 0x04001D6C RID: 7532
			public static readonly ILBuilder.ScopeComparer Instance = new ILBuilder.ScopeComparer();
		}
	}
}
