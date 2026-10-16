using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000085 RID: 133
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct FieldDefinition
	{
		// Token: 0x06000530 RID: 1328 RVA: 0x0000C5D2 File Offset: 0x0000A7D2
		internal FieldDefinition(MetadataReader reader, uint treatmentAndRowId)
		{
			this._reader = reader;
			this._treatmentAndRowId = treatmentAndRowId;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0000C5E2 File Offset: 0x0000A7E2
		private int RowId
		{
			get
			{
				return (int)(this._treatmentAndRowId & 16777215U);
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
		private FieldDefTreatment Treatment
		{
			get
			{
				return (FieldDefTreatment)(this._treatmentAndRowId >> 24);
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000C5FC File Offset: 0x0000A7FC
		private FieldDefinitionHandle Handle
		{
			get
			{
				return FieldDefinitionHandle.FromRowId(this.RowId);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0000C609 File Offset: 0x0000A809
		public StringHandle Name
		{
			get
			{
				if (this.Treatment == FieldDefTreatment.None)
				{
					return this._reader.FieldTable.GetName(this.Handle);
				}
				return this.GetProjectedName();
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0000C630 File Offset: 0x0000A830
		public FieldAttributes Attributes
		{
			get
			{
				if (this.Treatment == FieldDefTreatment.None)
				{
					return this._reader.FieldTable.GetFlags(this.Handle);
				}
				return this.GetProjectedFlags();
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0000C657 File Offset: 0x0000A857
		public BlobHandle Signature
		{
			get
			{
				if (this.Treatment == FieldDefTreatment.None)
				{
					return this._reader.FieldTable.GetSignature(this.Handle);
				}
				return this.GetProjectedSignature();
			}
		}

		/// <param name="provider" />
		/// <param name="genericContext" />
		/// <typeparam name="TType" />
		/// <typeparam name="TGenericContext" />
		// Token: 0x06000537 RID: 1335 RVA: 0x0000C680 File Offset: 0x0000A880
		public TType DecodeSignature<[Nullable(2)] TType, [Nullable(2)] TGenericContext>(ISignatureTypeProvider<TType, TGenericContext> provider, TGenericContext genericContext)
		{
			SignatureDecoder<TType, TGenericContext> signatureDecoder = new SignatureDecoder<TType, TGenericContext>(provider, this._reader, genericContext);
			BlobReader blobReader = this._reader.GetBlobReader(this.Signature);
			return signatureDecoder.DecodeFieldSignature(ref blobReader);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000C6B7 File Offset: 0x0000A8B7
		public TypeDefinitionHandle GetDeclaringType()
		{
			return this._reader.GetDeclaringType(this.Handle);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000C6CA File Offset: 0x0000A8CA
		public ConstantHandle GetDefaultValue()
		{
			return this._reader.ConstantTable.FindConstant(this.Handle);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000C6E8 File Offset: 0x0000A8E8
		public int GetRelativeVirtualAddress()
		{
			int num = this._reader.FieldRvaTable.FindFieldRvaRowId(this.Handle.RowId);
			if (num == 0)
			{
				return 0;
			}
			return this._reader.FieldRvaTable.GetRva(num);
		}

		/// <summary>Returns the field layout offset, or -1 if it is not available.</summary>
		/// <returns>The field definition offset, or -1 if it is not available.</returns>
		// Token: 0x0600053B RID: 1339 RVA: 0x0000C72C File Offset: 0x0000A92C
		public int GetOffset()
		{
			int num = this._reader.FieldLayoutTable.FindFieldLayoutRowId(this.Handle);
			if (num == 0)
			{
				return -1;
			}
			uint offset = this._reader.FieldLayoutTable.GetOffset(num);
			if (offset > 2147483647U)
			{
				return -1;
			}
			return (int)offset;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000C774 File Offset: 0x0000A974
		public BlobHandle GetMarshallingDescriptor()
		{
			int num = this._reader.FieldMarshalTable.FindFieldMarshalRowId(this.Handle);
			if (num == 0)
			{
				return default(BlobHandle);
			}
			return this._reader.FieldMarshalTable.GetNativeType(num);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000C7BB File Offset: 0x0000A9BB
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000C7D3 File Offset: 0x0000A9D3
		private StringHandle GetProjectedName()
		{
			return this._reader.FieldTable.GetName(this.Handle);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000C7EC File Offset: 0x0000A9EC
		private FieldAttributes GetProjectedFlags()
		{
			FieldAttributes flags = this._reader.FieldTable.GetFlags(this.Handle);
			if (this.Treatment == FieldDefTreatment.EnumValue)
			{
				return (flags & -8) | 6;
			}
			return flags;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000C821 File Offset: 0x0000AA21
		private BlobHandle GetProjectedSignature()
		{
			return this._reader.FieldTable.GetSignature(this.Handle);
		}

		// Token: 0x0400035C RID: 860
		private readonly MetadataReader _reader;

		// Token: 0x0400035D RID: 861
		private readonly uint _treatmentAndRowId;
	}
}
