using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.FlowAnalysis
{
	// Token: 0x020002DF RID: 735
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class ControlFlowRegion
	{
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x00060DA1 File Offset: 0x0005EFA1
		public ControlFlowRegionKind Kind { get; }

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x00060DA9 File Offset: 0x0005EFA9
		// (set) Token: 0x06001F79 RID: 8057 RVA: 0x00060DB1 File Offset: 0x0005EFB1
		public ControlFlowRegion EnclosingRegion { get; private set; }

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x00060DBA File Offset: 0x0005EFBA
		public ITypeSymbol ExceptionType { get; }

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x00060DC2 File Offset: 0x0005EFC2
		public int FirstBlockOrdinal { get; }

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x00060DCA File Offset: 0x0005EFCA
		public int LastBlockOrdinal { get; }

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001F7D RID: 8061 RVA: 0x00060DD2 File Offset: 0x0005EFD2
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ControlFlowRegion> NestedRegions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x00060DDA File Offset: 0x0005EFDA
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x00060DE2 File Offset: 0x0005EFE2
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IMethodSymbol> LocalFunctions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x00060DEA File Offset: 0x0005EFEA
		[Nullable(0)]
		public ImmutableArray<CaptureId> CaptureIds
		{
			[NullableContext(0)]
			get;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x00060DF4 File Offset: 0x0005EFF4
		internal ControlFlowRegion(ControlFlowRegionKind kind, int firstBlockOrdinal, int lastBlockOrdinal, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ControlFlowRegion> nestedRegions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IMethodSymbol> methods, [Nullable(0)] ImmutableArray<CaptureId> captureIds, ITypeSymbol exceptionType, ControlFlowRegion enclosingRegion)
		{
			this.Kind = kind;
			this.FirstBlockOrdinal = firstBlockOrdinal;
			this.LastBlockOrdinal = lastBlockOrdinal;
			this.ExceptionType = exceptionType;
			this.Locals = locals.NullToEmpty<ILocalSymbol>();
			this.LocalFunctions = methods.NullToEmpty<IMethodSymbol>();
			this.CaptureIds = captureIds.NullToEmpty<CaptureId>();
			this.NestedRegions = nestedRegions.NullToEmpty<ControlFlowRegion>();
			this.EnclosingRegion = enclosingRegion;
			foreach (ControlFlowRegion controlFlowRegion in this.NestedRegions)
			{
				controlFlowRegion.EnclosingRegion = this;
			}
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x00060E87 File Offset: 0x0005F087
		internal bool ContainsBlock(int destinationOrdinal)
		{
			return this.FirstBlockOrdinal <= destinationOrdinal && this.LastBlockOrdinal >= destinationOrdinal;
		}
	}
}
