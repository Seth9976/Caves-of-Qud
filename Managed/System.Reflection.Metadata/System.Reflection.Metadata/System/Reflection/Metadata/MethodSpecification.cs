using System;
using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000AE RID: 174
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct MethodSpecification
	{
		// Token: 0x060006BD RID: 1725 RVA: 0x000112C0 File Offset: 0x0000F4C0
		internal MethodSpecification(MetadataReader reader, MethodSpecificationHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x000112D6 File Offset: 0x0000F4D6
		private MethodSpecificationHandle Handle
		{
			get
			{
				return MethodSpecificationHandle.FromRowId(this._rowId);
			}
		}

		/// <summary>Gets a <see langword="MethodDef" /> or <see langword="MemberRef" /> handle specifying which generic method this instance refers to (that is, which generic method it is an instantiation of).</summary>
		/// <returns>A <see langword="MethodDef" /> or <see langword="MemberRef" /> handle specifying which generic method this instance refers to.</returns>
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x000112E3 File Offset: 0x0000F4E3
		public EntityHandle Method
		{
			get
			{
				return this._reader.MethodSpecTable.GetMethod(this.Handle);
			}
		}

		/// <summary>Gets a handle to the signature blob.</summary>
		/// <returns>A handle to the signature blob.</returns>
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x000112FB File Offset: 0x0000F4FB
		public BlobHandle Signature
		{
			get
			{
				return this._reader.MethodSpecTable.GetInstantiation(this.Handle);
			}
		}

		/// <param name="provider" />
		/// <param name="genericContext" />
		/// <typeparam name="TType" />
		/// <typeparam name="TGenericContext" />
		// Token: 0x060006C1 RID: 1729 RVA: 0x00011314 File Offset: 0x0000F514
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<TType> DecodeSignature<[Nullable(2)] TType, [Nullable(2)] TGenericContext>(ISignatureTypeProvider<TType, TGenericContext> provider, TGenericContext genericContext)
		{
			SignatureDecoder<TType, TGenericContext> signatureDecoder = new SignatureDecoder<TType, TGenericContext>(provider, this._reader, genericContext);
			BlobReader blobReader = this._reader.GetBlobReader(this.Signature);
			return signatureDecoder.DecodeMethodSpecificationSignature(ref blobReader);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001134B File Offset: 0x0000F54B
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x04000434 RID: 1076
		private readonly MetadataReader _reader;

		// Token: 0x04000435 RID: 1077
		private readonly int _rowId;
	}
}
