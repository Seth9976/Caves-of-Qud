using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Unity.Hierarchy
{
	// Token: 0x0200000F RID: 15
	public readonly struct HierarchyNodeTypeHandlerBaseEnumerable
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002B9E File Offset: 0x00000D9E
		internal HierarchyNodeTypeHandlerBaseEnumerable(Hierarchy hierarchy)
		{
			this.m_Hierarchy = hierarchy;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002BA8 File Offset: 0x00000DA8
		public HierarchyNodeTypeHandlerBaseEnumerable.Enumerator GetEnumerator()
		{
			return new HierarchyNodeTypeHandlerBaseEnumerable.Enumerator(this.m_Hierarchy);
		}

		// Token: 0x04000025 RID: 37
		private readonly Hierarchy m_Hierarchy;

		// Token: 0x02000010 RID: 16
		public struct Enumerator : IDisposable
		{
			// Token: 0x0600005A RID: 90 RVA: 0x00002BB8 File Offset: 0x00000DB8
			internal Enumerator(Hierarchy hierarchy)
			{
				this.m_Handlers = MemoryPool<IntPtr>.Shared.Rent(hierarchy.GetNodeTypeHandlersBaseCount());
				this.m_Count = hierarchy.GetNodeTypeHandlersBaseSpan(this.m_Handlers.Memory.Span);
				this.m_Index = -1;
			}

			// Token: 0x0600005B RID: 91 RVA: 0x00002C02 File Offset: 0x00000E02
			public void Dispose()
			{
				this.m_Handlers.Dispose();
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x0600005C RID: 92 RVA: 0x00002C14 File Offset: 0x00000E14
			public unsafe HierarchyNodeTypeHandlerBase Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return HierarchyNodeTypeHandlerBase.FromIntPtr(*this.m_Handlers.Memory.Span[this.m_Index]);
				}
			}

			// Token: 0x0600005D RID: 93 RVA: 0x00002C48 File Offset: 0x00000E48
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				int num = this.m_Index + 1;
				this.m_Index = num;
				return num < this.m_Count;
			}

			// Token: 0x04000026 RID: 38
			private readonly IMemoryOwner<IntPtr> m_Handlers;

			// Token: 0x04000027 RID: 39
			private readonly int m_Count;

			// Token: 0x04000028 RID: 40
			private int m_Index;
		}
	}
}
