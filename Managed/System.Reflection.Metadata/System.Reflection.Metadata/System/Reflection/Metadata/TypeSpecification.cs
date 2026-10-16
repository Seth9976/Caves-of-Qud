using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000DC RID: 220
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct TypeSpecification
	{
		// Token: 0x060007FD RID: 2045 RVA: 0x00013103 File Offset: 0x00011303
		internal TypeSpecification(MetadataReader reader, TypeSpecificationHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00013119 File Offset: 0x00011319
		private TypeSpecificationHandle Handle
		{
			get
			{
				return TypeSpecificationHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x00013126 File Offset: 0x00011326
		public BlobHandle Signature
		{
			get
			{
				return this._reader.TypeSpecTable.GetSignature(this.Handle);
			}
		}

		/// <param name="provider" />
		/// <param name="genericContext" />
		/// <typeparam name="TType" />
		/// <typeparam name="TGenericContext" />
		// Token: 0x06000800 RID: 2048 RVA: 0x00013140 File Offset: 0x00011340
		public TType DecodeSignature<[Nullable(2)] TType, [Nullable(2)] TGenericContext>(ISignatureTypeProvider<TType, TGenericContext> provider, TGenericContext genericContext)
		{
			SignatureDecoder<TType, TGenericContext> signatureDecoder = new SignatureDecoder<TType, TGenericContext>(provider, this._reader, genericContext);
			BlobReader blobReader = this._reader.GetBlobReader(this.Signature);
			return signatureDecoder.DecodeType(ref blobReader, false);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00013178 File Offset: 0x00011378
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x040004E7 RID: 1255
		private readonly MetadataReader _reader;

		// Token: 0x040004E8 RID: 1256
		private readonly int _rowId;
	}
}
