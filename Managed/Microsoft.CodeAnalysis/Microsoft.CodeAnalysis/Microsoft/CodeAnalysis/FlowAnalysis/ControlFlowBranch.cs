using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.FlowAnalysis
{
	// Token: 0x020002D9 RID: 729
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ControlFlowBranch
	{
		// Token: 0x06001E44 RID: 7748 RVA: 0x0005726F File Offset: 0x0005546F
		internal ControlFlowBranch(BasicBlock source, [Nullable(2)] BasicBlock destination, ControlFlowBranchSemantics semantics, bool isConditionalSuccessor)
		{
			this.Source = source;
			this.Destination = destination;
			this.Semantics = semantics;
			this.IsConditionalSuccessor = isConditionalSuccessor;
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001E45 RID: 7749 RVA: 0x00057294 File Offset: 0x00055494
		public BasicBlock Source { get; }

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x0005729C File Offset: 0x0005549C
		[Nullable(2)]
		public BasicBlock Destination
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x000572A4 File Offset: 0x000554A4
		public ControlFlowBranchSemantics Semantics { get; }

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x000572AC File Offset: 0x000554AC
		public bool IsConditionalSuccessor { get; }

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001E49 RID: 7753 RVA: 0x000572B4 File Offset: 0x000554B4
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ControlFlowRegion> LeavingRegions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				if (this._lazyLeavingRegions.IsDefault)
				{
					ImmutableArray<ControlFlowRegion> immutableArray;
					if (this.Destination == null)
					{
						immutableArray = ImmutableArray<ControlFlowRegion>.Empty;
					}
					else
					{
						immutableArray = ControlFlowBranch.CollectRegions(this.Destination.Ordinal, this.Source.EnclosingRegion).ToImmutableAndFree();
					}
					ImmutableInterlocked.InterlockedInitialize<ControlFlowRegion>(ref this._lazyLeavingRegions, immutableArray);
				}
				return this._lazyLeavingRegions;
			}
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x00057314 File Offset: 0x00055514
		private static ArrayBuilder<ControlFlowRegion> CollectRegions(int destinationOrdinal, ControlFlowRegion source)
		{
			ArrayBuilder<ControlFlowRegion> instance = ArrayBuilder<ControlFlowRegion>.GetInstance();
			while (!source.ContainsBlock(destinationOrdinal))
			{
				instance.Add(source);
				source = source.EnclosingRegion;
			}
			return instance;
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001E4B RID: 7755 RVA: 0x00057344 File Offset: 0x00055544
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ControlFlowRegion> EnteringRegions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				if (this._lazyEnteringRegions.IsDefault)
				{
					ImmutableArray<ControlFlowRegion> immutableArray;
					if (this.Destination == null)
					{
						immutableArray = ImmutableArray<ControlFlowRegion>.Empty;
					}
					else
					{
						ArrayBuilder<ControlFlowRegion> arrayBuilder = ControlFlowBranch.CollectRegions(this.Source.Ordinal, this.Destination.EnclosingRegion);
						arrayBuilder.ReverseContents();
						immutableArray = arrayBuilder.ToImmutableAndFree();
					}
					ImmutableInterlocked.InterlockedInitialize<ControlFlowRegion>(ref this._lazyEnteringRegions, immutableArray);
				}
				return this._lazyEnteringRegions;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x000573A8 File Offset: 0x000555A8
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ControlFlowRegion> FinallyRegions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				if (this._lazyFinallyRegions.IsDefault)
				{
					ArrayBuilder<ControlFlowRegion> arrayBuilder = null;
					ImmutableArray<ControlFlowRegion> leavingRegions = this.LeavingRegions;
					int num = leavingRegions.Length - 1;
					for (int i = 0; i < num; i++)
					{
						if (leavingRegions[i].Kind == ControlFlowRegionKind.Try && leavingRegions[i + 1].Kind == ControlFlowRegionKind.TryAndFinally)
						{
							if (arrayBuilder == null)
							{
								arrayBuilder = ArrayBuilder<ControlFlowRegion>.GetInstance();
							}
							arrayBuilder.Add(leavingRegions[i + 1].NestedRegions.Last<ControlFlowRegion>());
						}
					}
					ImmutableArray<ControlFlowRegion> immutableArray = ((arrayBuilder == null) ? ImmutableArray<ControlFlowRegion>.Empty : arrayBuilder.ToImmutableAndFree());
					ImmutableInterlocked.InterlockedInitialize<ControlFlowRegion>(ref this._lazyFinallyRegions, immutableArray);
				}
				return this._lazyFinallyRegions;
			}
		}

		// Token: 0x04000D99 RID: 3481
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<ControlFlowRegion> _lazyLeavingRegions;

		// Token: 0x04000D9A RID: 3482
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<ControlFlowRegion> _lazyFinallyRegions;

		// Token: 0x04000D9B RID: 3483
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<ControlFlowRegion> _lazyEnteringRegions;
	}
}
