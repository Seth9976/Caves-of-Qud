using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000B3 RID: 179
	public readonly struct CustomDebugInformation
	{
		// Token: 0x060006DC RID: 1756 RVA: 0x00011563 File Offset: 0x0000F763
		[NullableContext(1)]
		internal CustomDebugInformation(MetadataReader reader, CustomDebugInformationHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00011579 File Offset: 0x0000F779
		private CustomDebugInformationHandle Handle
		{
			get
			{
				return CustomDebugInformationHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00011586 File Offset: 0x0000F786
		public EntityHandle Parent
		{
			get
			{
				return this._reader.CustomDebugInformationTable.GetParent(this.Handle);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0001159E File Offset: 0x0000F79E
		public GuidHandle Kind
		{
			get
			{
				return this._reader.CustomDebugInformationTable.GetKind(this.Handle);
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x000115B6 File Offset: 0x0000F7B6
		public BlobHandle Value
		{
			get
			{
				return this._reader.CustomDebugInformationTable.GetValue(this.Handle);
			}
		}

		// Token: 0x0400043C RID: 1084
		private readonly MetadataReader _reader;

		// Token: 0x0400043D RID: 1085
		private readonly int _rowId;
	}
}
