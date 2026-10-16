using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000B2 RID: 178
	public readonly struct Parameter
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x0001147A File Offset: 0x0000F67A
		[NullableContext(1)]
		internal Parameter(MetadataReader reader, ParameterHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00011490 File Offset: 0x0000F690
		private ParameterHandle Handle
		{
			get
			{
				return ParameterHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001149D File Offset: 0x0000F69D
		public ParameterAttributes Attributes
		{
			get
			{
				return this._reader.ParamTable.GetFlags(this.Handle);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x000114B5 File Offset: 0x0000F6B5
		public int SequenceNumber
		{
			get
			{
				return (int)this._reader.ParamTable.GetSequence(this.Handle);
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x000114CD File Offset: 0x0000F6CD
		public StringHandle Name
		{
			get
			{
				return this._reader.ParamTable.GetName(this.Handle);
			}
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000114E5 File Offset: 0x0000F6E5
		public ConstantHandle GetDefaultValue()
		{
			return this._reader.ConstantTable.FindConstant(this.Handle);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00011504 File Offset: 0x0000F704
		public BlobHandle GetMarshallingDescriptor()
		{
			int num = this._reader.FieldMarshalTable.FindFieldMarshalRowId(this.Handle);
			if (num == 0)
			{
				return default(BlobHandle);
			}
			return this._reader.FieldMarshalTable.GetNativeType(num);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001154B File Offset: 0x0000F74B
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x0400043A RID: 1082
		private readonly MetadataReader _reader;

		// Token: 0x0400043B RID: 1083
		private readonly int _rowId;
	}
}
