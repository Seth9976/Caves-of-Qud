using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000074 RID: 116
	public readonly struct Constant
	{
		// Token: 0x060004EC RID: 1260 RVA: 0x0000BF41 File Offset: 0x0000A141
		[NullableContext(1)]
		internal Constant(MetadataReader reader, int rowId)
		{
			this._reader = reader;
			this._rowId = rowId;
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000BF51 File Offset: 0x0000A151
		private ConstantHandle Handle
		{
			get
			{
				return ConstantHandle.FromRowId(this._rowId);
			}
		}

		/// <summary>Gets a type code that identifies the type of the constant value.</summary>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0000BF5E File Offset: 0x0000A15E
		public ConstantTypeCode TypeCode
		{
			get
			{
				return this._reader.ConstantTable.GetType(this.Handle);
			}
		}

		/// <summary>Gets the constant value.</summary>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000BF76 File Offset: 0x0000A176
		public BlobHandle Value
		{
			get
			{
				return this._reader.ConstantTable.GetValue(this.Handle);
			}
		}

		/// <summary>Gets the parent handle (<see cref="T:System.Reflection.Metadata.ParameterHandle" />, <see cref="T:System.Reflection.Metadata.FieldDefinitionHandle" />, or <see cref="T:System.Reflection.Metadata.PropertyDefinitionHandle" />).</summary>
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000BF8E File Offset: 0x0000A18E
		public EntityHandle Parent
		{
			get
			{
				return this._reader.ConstantTable.GetParent(this.Handle);
			}
		}

		// Token: 0x04000315 RID: 789
		private readonly MetadataReader _reader;

		// Token: 0x04000316 RID: 790
		private readonly int _rowId;
	}
}
