using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	/// <summary>Emits branches and exception blocks in a method body.</summary>
	// Token: 0x020000E8 RID: 232
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ControlFlowBuilder
	{
		// Token: 0x060008C3 RID: 2243 RVA: 0x00018E2B File Offset: 0x0001702B
		public ControlFlowBuilder()
		{
			this._branches = ImmutableArray.CreateBuilder<ControlFlowBuilder.BranchInfo>();
			this._labels = ImmutableArray.CreateBuilder<int>();
		}

		/// <summary>Clears the object's internal state, allowing the same instance to be reused.</summary>
		// Token: 0x060008C4 RID: 2244 RVA: 0x00018E49 File Offset: 0x00017049
		internal void Clear()
		{
			this._branches.Clear();
			this._labels.Clear();
			ImmutableArray<ControlFlowBuilder.ExceptionHandlerInfo>.Builder lazyExceptionHandlers = this._lazyExceptionHandlers;
			if (lazyExceptionHandlers == null)
			{
				return;
			}
			lazyExceptionHandlers.Clear();
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00018E71 File Offset: 0x00017071
		internal LabelHandle AddLabel()
		{
			this._labels.Add(-1);
			return new LabelHandle(this._labels.Count);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00018E8F File Offset: 0x0001708F
		internal void AddBranch(int ilOffset, LabelHandle label, ILOpCode opCode)
		{
			this.ValidateLabel(label, "label");
			this._branches.Add(new ControlFlowBuilder.BranchInfo(ilOffset, label, opCode));
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00018EB0 File Offset: 0x000170B0
		internal void MarkLabel(int ilOffset, LabelHandle label)
		{
			this.ValidateLabel(label, "label");
			this._labels[label.Id - 1] = ilOffset;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00018ED4 File Offset: 0x000170D4
		private int GetLabelOffsetChecked(LabelHandle label)
		{
			int num = this._labels[label.Id - 1];
			if (num < 0)
			{
				Throw.InvalidOperation_LabelNotMarked(label.Id);
			}
			return num;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00018F07 File Offset: 0x00017107
		private void ValidateLabel(LabelHandle label, string parameterName)
		{
			if (label.IsNil)
			{
				Throw.ArgumentNull(parameterName);
			}
			if (label.Id > this._labels.Count)
			{
				Throw.LabelDoesntBelongToBuilder(parameterName);
			}
		}

		/// <summary>Adds finally region.</summary>
		/// <param name="tryStart">Label marking the first instruction of the try block.</param>
		/// <param name="tryEnd">Label marking the instruction immediately following the try block.</param>
		/// <param name="handlerStart">Label marking the first instruction of the handler.</param>
		/// <param name="handlerEnd">Label marking the instruction immediately following the handler.</param>
		/// <exception cref="T:System.ArgumentException">A label was not defined by an instruction encoder this builder is associated with.</exception>
		/// <exception cref="T:System.ArgumentNullException">A label has default value.</exception>
		// Token: 0x060008CA RID: 2250 RVA: 0x00018F34 File Offset: 0x00017134
		public void AddFinallyRegion(LabelHandle tryStart, LabelHandle tryEnd, LabelHandle handlerStart, LabelHandle handlerEnd)
		{
			this.AddExceptionRegion(ExceptionRegionKind.Finally, tryStart, tryEnd, handlerStart, handlerEnd, default(LabelHandle), default(EntityHandle));
		}

		/// <summary>Adds fault region.</summary>
		/// <param name="tryStart">Label marking the first instruction of the try block.</param>
		/// <param name="tryEnd">Label marking the instruction immediately following the try block.</param>
		/// <param name="handlerStart">Label marking the first instruction of the handler.</param>
		/// <param name="handlerEnd">Label marking the instruction immediately following the handler.</param>
		/// <exception cref="T:System.ArgumentException">A label was not defined by an instruction encoder this builder is associated with.</exception>
		/// <exception cref="T:System.ArgumentNullException">A label has default value.</exception>
		// Token: 0x060008CB RID: 2251 RVA: 0x00018F60 File Offset: 0x00017160
		public void AddFaultRegion(LabelHandle tryStart, LabelHandle tryEnd, LabelHandle handlerStart, LabelHandle handlerEnd)
		{
			this.AddExceptionRegion(ExceptionRegionKind.Fault, tryStart, tryEnd, handlerStart, handlerEnd, default(LabelHandle), default(EntityHandle));
		}

		/// <summary>Adds catch region.</summary>
		/// <param name="tryStart">Label marking the first instruction of the try block.</param>
		/// <param name="tryEnd">Label marking the instruction immediately following the try block.</param>
		/// <param name="handlerStart">Label marking the first instruction of the handler.</param>
		/// <param name="handlerEnd">Label marking the instruction immediately following the handler.</param>
		/// <param name="catchType">The type of exception to be caught: <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" /> or <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">A label was not defined by an instruction encoder this builder is associated with.
		///
		/// -or-
		///
		/// <paramref name="catchType" /> is not a valid type handle.</exception>
		/// <exception cref="T:System.ArgumentNullException">A label has default value.</exception>
		// Token: 0x060008CC RID: 2252 RVA: 0x00018F8C File Offset: 0x0001718C
		public void AddCatchRegion(LabelHandle tryStart, LabelHandle tryEnd, LabelHandle handlerStart, LabelHandle handlerEnd, EntityHandle catchType)
		{
			if (!ExceptionRegionEncoder.IsValidCatchTypeHandle(catchType))
			{
				Throw.InvalidArgument_Handle("catchType");
			}
			this.AddExceptionRegion(ExceptionRegionKind.Catch, tryStart, tryEnd, handlerStart, handlerEnd, default(LabelHandle), catchType);
		}

		/// <summary>Adds catch region.</summary>
		/// <param name="tryStart">Label marking the first instruction of the try block.</param>
		/// <param name="tryEnd">Label marking the instruction immediately following the try block.</param>
		/// <param name="handlerStart">Label marking the first instruction of the handler.</param>
		/// <param name="handlerEnd">Label marking the instruction immediately following the handler.</param>
		/// <param name="filterStart">Label marking the first instruction of the filter block.</param>
		/// <exception cref="T:System.ArgumentException">A label was not defined by an instruction encoder this builder is associated with.</exception>
		/// <exception cref="T:System.ArgumentNullException">A label has default value.</exception>
		// Token: 0x060008CD RID: 2253 RVA: 0x00018FC4 File Offset: 0x000171C4
		public void AddFilterRegion(LabelHandle tryStart, LabelHandle tryEnd, LabelHandle handlerStart, LabelHandle handlerEnd, LabelHandle filterStart)
		{
			this.ValidateLabel(filterStart, "filterStart");
			this.AddExceptionRegion(ExceptionRegionKind.Filter, tryStart, tryEnd, handlerStart, handlerEnd, filterStart, default(EntityHandle));
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00018FF8 File Offset: 0x000171F8
		private void AddExceptionRegion(ExceptionRegionKind kind, LabelHandle tryStart, LabelHandle tryEnd, LabelHandle handlerStart, LabelHandle handlerEnd, LabelHandle filterStart = default(LabelHandle), EntityHandle catchType = default(EntityHandle))
		{
			this.ValidateLabel(tryStart, "tryStart");
			this.ValidateLabel(tryEnd, "tryEnd");
			this.ValidateLabel(handlerStart, "handlerStart");
			this.ValidateLabel(handlerEnd, "handlerEnd");
			if (this._lazyExceptionHandlers == null)
			{
				this._lazyExceptionHandlers = ImmutableArray.CreateBuilder<ControlFlowBuilder.ExceptionHandlerInfo>();
			}
			this._lazyExceptionHandlers.Add(new ControlFlowBuilder.ExceptionHandlerInfo(kind, tryStart, tryEnd, handlerStart, handlerEnd, filterStart, catchType));
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00019065 File Offset: 0x00017265
		internal IEnumerable<ControlFlowBuilder.BranchInfo> Branches
		{
			get
			{
				return this._branches;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0001906D File Offset: 0x0001726D
		internal IEnumerable<int> Labels
		{
			get
			{
				return this._labels;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00019075 File Offset: 0x00017275
		internal int BranchCount
		{
			get
			{
				return this._branches.Count;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00019082 File Offset: 0x00017282
		internal int ExceptionHandlerCount
		{
			get
			{
				ImmutableArray<ControlFlowBuilder.ExceptionHandlerInfo>.Builder lazyExceptionHandlers = this._lazyExceptionHandlers;
				if (lazyExceptionHandlers == null)
				{
					return 0;
				}
				return lazyExceptionHandlers.Count;
			}
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00019098 File Offset: 0x00017298
		internal void CopyCodeAndFixupBranches(BlobBuilder srcBuilder, BlobBuilder dstBuilder)
		{
			ControlFlowBuilder.BranchInfo branchInfo = this._branches[0];
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			foreach (Blob blob in srcBuilder.GetBlobs())
			{
				int branchOperandSize;
				for (;;)
				{
					int num4 = Math.Min(branchInfo.ILOffset - num2, blob.Length - num3);
					dstBuilder.WriteBytes(blob.Buffer, num3, num4);
					num2 += num4;
					num3 += num4;
					if (num3 == blob.Length)
					{
						break;
					}
					branchOperandSize = branchInfo.OpCode.GetBranchOperandSize();
					bool flag = branchOperandSize == 1;
					dstBuilder.WriteByte(blob.Buffer[num3]);
					int branchDistance = branchInfo.GetBranchDistance(this._labels, branchInfo.OpCode, num2, flag);
					if (flag)
					{
						dstBuilder.WriteSByte((sbyte)branchDistance);
					}
					else
					{
						dstBuilder.WriteInt32(branchDistance);
					}
					num2 += 1 + branchOperandSize;
					num++;
					if (num == this._branches.Count)
					{
						branchInfo = new ControlFlowBuilder.BranchInfo(int.MaxValue, default(LabelHandle), ILOpCode.Nop);
					}
					else
					{
						branchInfo = this._branches[num];
					}
					if (num3 == blob.Length - 1)
					{
						goto Block_6;
					}
					num3 += 1 + branchOperandSize;
				}
				num3 = 0;
				continue;
				Block_6:
				num3 = branchOperandSize;
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00019204 File Offset: 0x00017404
		internal void SerializeExceptionTable(BlobBuilder builder)
		{
			if (this._lazyExceptionHandlers == null || this._lazyExceptionHandlers.Count == 0)
			{
				return;
			}
			ExceptionRegionEncoder exceptionRegionEncoder = ExceptionRegionEncoder.SerializeTableHeader(builder, this._lazyExceptionHandlers.Count, this.HasSmallExceptionRegions());
			foreach (ControlFlowBuilder.ExceptionHandlerInfo exceptionHandlerInfo in this._lazyExceptionHandlers)
			{
				int labelOffsetChecked = this.GetLabelOffsetChecked(exceptionHandlerInfo.TryStart);
				int labelOffsetChecked2 = this.GetLabelOffsetChecked(exceptionHandlerInfo.TryEnd);
				int labelOffsetChecked3 = this.GetLabelOffsetChecked(exceptionHandlerInfo.HandlerStart);
				int labelOffsetChecked4 = this.GetLabelOffsetChecked(exceptionHandlerInfo.HandlerEnd);
				if (labelOffsetChecked > labelOffsetChecked2)
				{
					Throw.InvalidOperation(SR.Format(SR.InvalidExceptionRegionBounds, labelOffsetChecked, labelOffsetChecked2));
				}
				if (labelOffsetChecked3 > labelOffsetChecked4)
				{
					Throw.InvalidOperation(SR.Format(SR.InvalidExceptionRegionBounds, labelOffsetChecked3, labelOffsetChecked4));
				}
				ExceptionRegionKind kind = exceptionHandlerInfo.Kind;
				int num;
				if (kind != ExceptionRegionKind.Catch)
				{
					if (kind != ExceptionRegionKind.Filter)
					{
						num = 0;
					}
					else
					{
						num = this.GetLabelOffsetChecked(exceptionHandlerInfo.FilterStart);
					}
				}
				else
				{
					num = MetadataTokens.GetToken(exceptionHandlerInfo.CatchType);
				}
				int num2 = num;
				exceptionRegionEncoder.AddUnchecked(exceptionHandlerInfo.Kind, labelOffsetChecked, labelOffsetChecked2 - labelOffsetChecked, labelOffsetChecked3, labelOffsetChecked4 - labelOffsetChecked3, num2);
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00019350 File Offset: 0x00017550
		private bool HasSmallExceptionRegions()
		{
			if (!ExceptionRegionEncoder.IsSmallRegionCount(this._lazyExceptionHandlers.Count))
			{
				return false;
			}
			foreach (ControlFlowBuilder.ExceptionHandlerInfo exceptionHandlerInfo in this._lazyExceptionHandlers)
			{
				if (!ExceptionRegionEncoder.IsSmallExceptionRegionFromBounds(this.GetLabelOffsetChecked(exceptionHandlerInfo.TryStart), this.GetLabelOffsetChecked(exceptionHandlerInfo.TryEnd)) || !ExceptionRegionEncoder.IsSmallExceptionRegionFromBounds(this.GetLabelOffsetChecked(exceptionHandlerInfo.HandlerStart), this.GetLabelOffsetChecked(exceptionHandlerInfo.HandlerEnd)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000548 RID: 1352
		private readonly ImmutableArray<ControlFlowBuilder.BranchInfo>.Builder _branches;

		// Token: 0x04000549 RID: 1353
		private readonly ImmutableArray<int>.Builder _labels;

		// Token: 0x0400054A RID: 1354
		private ImmutableArray<ControlFlowBuilder.ExceptionHandlerInfo>.Builder _lazyExceptionHandlers;

		// Token: 0x020001EA RID: 490
		[NullableContext(0)]
		internal readonly struct BranchInfo
		{
			// Token: 0x17000352 RID: 850
			// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00026576 File Offset: 0x00024776
			internal ILOpCode OpCode
			{
				get
				{
					return (ILOpCode)this._opCode;
				}
			}

			// Token: 0x06000D17 RID: 3351 RVA: 0x0002657E File Offset: 0x0002477E
			internal BranchInfo(int ilOffset, LabelHandle label, ILOpCode opCode)
			{
				this.ILOffset = ilOffset;
				this.Label = label;
				this._opCode = (byte)opCode;
			}

			// Token: 0x06000D18 RID: 3352 RVA: 0x00026598 File Offset: 0x00024798
			[NullableContext(1)]
			internal int GetBranchDistance(ImmutableArray<int>.Builder labels, ILOpCode branchOpCode, int branchILOffset, bool isShortBranch)
			{
				int num = labels[this.Label.Id - 1];
				if (num < 0)
				{
					Throw.InvalidOperation_LabelNotMarked(this.Label.Id);
				}
				int num2 = 1 + (isShortBranch ? 1 : 4);
				int num3 = num - (this.ILOffset + num2);
				if (isShortBranch && (int)((sbyte)num3) != num3)
				{
					throw new InvalidOperationException(SR.Format(SR.DistanceBetweenInstructionAndLabelTooBig, branchOpCode, branchILOffset, num3));
				}
				return num3;
			}

			// Token: 0x04000B2E RID: 2862
			internal readonly int ILOffset;

			// Token: 0x04000B2F RID: 2863
			internal readonly LabelHandle Label;

			// Token: 0x04000B30 RID: 2864
			private readonly byte _opCode;
		}

		// Token: 0x020001EB RID: 491
		[NullableContext(0)]
		internal readonly struct ExceptionHandlerInfo
		{
			// Token: 0x06000D19 RID: 3353 RVA: 0x0002660F File Offset: 0x0002480F
			public ExceptionHandlerInfo(ExceptionRegionKind kind, LabelHandle tryStart, LabelHandle tryEnd, LabelHandle handlerStart, LabelHandle handlerEnd, LabelHandle filterStart, EntityHandle catchType)
			{
				this.Kind = kind;
				this.TryStart = tryStart;
				this.TryEnd = tryEnd;
				this.HandlerStart = handlerStart;
				this.HandlerEnd = handlerEnd;
				this.FilterStart = filterStart;
				this.CatchType = catchType;
			}

			// Token: 0x04000B31 RID: 2865
			public readonly ExceptionRegionKind Kind;

			// Token: 0x04000B32 RID: 2866
			public readonly LabelHandle TryStart;

			// Token: 0x04000B33 RID: 2867
			public readonly LabelHandle TryEnd;

			// Token: 0x04000B34 RID: 2868
			public readonly LabelHandle HandlerStart;

			// Token: 0x04000B35 RID: 2869
			public readonly LabelHandle HandlerEnd;

			// Token: 0x04000B36 RID: 2870
			public readonly LabelHandle FilterStart;

			// Token: 0x04000B37 RID: 2871
			public readonly EntityHandle CatchType;
		}
	}
}
