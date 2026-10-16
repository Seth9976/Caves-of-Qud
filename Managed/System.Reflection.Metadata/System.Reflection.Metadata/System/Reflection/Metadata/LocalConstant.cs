using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Provides information about local constants. This information is stored in debug metadata.</summary>
	// Token: 0x020000C8 RID: 200
	public readonly struct LocalConstant
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x0001219F File Offset: 0x0001039F
		[NullableContext(1)]
		internal LocalConstant(MetadataReader reader, LocalConstantHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x000121B5 File Offset: 0x000103B5
		private LocalConstantHandle Handle
		{
			get
			{
				return LocalConstantHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x000121C2 File Offset: 0x000103C2
		public StringHandle Name
		{
			get
			{
				return this._reader.LocalConstantTable.GetName(this.Handle);
			}
		}

		/// <summary>Gets the constant signature.</summary>
		/// <returns>The constant signature.</returns>
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x000121DA File Offset: 0x000103DA
		public BlobHandle Signature
		{
			get
			{
				return this._reader.LocalConstantTable.GetSignature(this.Handle);
			}
		}

		// Token: 0x0400047D RID: 1149
		private readonly MetadataReader _reader;

		// Token: 0x0400047E RID: 1150
		private readonly int _rowId;
	}
}
