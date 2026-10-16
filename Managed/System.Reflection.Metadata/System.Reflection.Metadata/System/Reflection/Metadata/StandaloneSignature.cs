using System;
using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000D8 RID: 216
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct StandaloneSignature
	{
		// Token: 0x060007CB RID: 1995 RVA: 0x00012A8C File Offset: 0x00010C8C
		internal StandaloneSignature(MetadataReader reader, StandaloneSignatureHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00012AA2 File Offset: 0x00010CA2
		private StandaloneSignatureHandle Handle
		{
			get
			{
				return StandaloneSignatureHandle.FromRowId(this._rowId);
			}
		}

		/// <summary>Gets a handle to the signature blob.</summary>
		/// <returns>A handle to the signature blob.</returns>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00012AAF File Offset: 0x00010CAF
		public BlobHandle Signature
		{
			get
			{
				return this._reader.StandAloneSigTable.GetSignature(this._rowId);
			}
		}

		/// <param name="provider" />
		/// <param name="genericContext" />
		/// <typeparam name="TType" />
		/// <typeparam name="TGenericContext" />
		// Token: 0x060007CE RID: 1998 RVA: 0x00012AC8 File Offset: 0x00010CC8
		[return: Nullable(new byte[] { 0, 1 })]
		public MethodSignature<TType> DecodeMethodSignature<[Nullable(2)] TType, [Nullable(2)] TGenericContext>(ISignatureTypeProvider<TType, TGenericContext> provider, TGenericContext genericContext)
		{
			SignatureDecoder<TType, TGenericContext> signatureDecoder = new SignatureDecoder<TType, TGenericContext>(provider, this._reader, genericContext);
			BlobReader blobReader = this._reader.GetBlobReader(this.Signature);
			return signatureDecoder.DecodeMethodSignature(ref blobReader);
		}

		/// <param name="provider" />
		/// <param name="genericContext" />
		/// <typeparam name="TType" />
		/// <typeparam name="TGenericContext" />
		// Token: 0x060007CF RID: 1999 RVA: 0x00012B00 File Offset: 0x00010D00
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<TType> DecodeLocalSignature<[Nullable(2)] TType, [Nullable(2)] TGenericContext>(ISignatureTypeProvider<TType, TGenericContext> provider, TGenericContext genericContext)
		{
			SignatureDecoder<TType, TGenericContext> signatureDecoder = new SignatureDecoder<TType, TGenericContext>(provider, this._reader, genericContext);
			BlobReader blobReader = this._reader.GetBlobReader(this.Signature);
			return signatureDecoder.DecodeLocalSignature(ref blobReader);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00012B37 File Offset: 0x00010D37
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		/// <summary>Determines the kind of signature, which can be <see cref="F:System.Reflection.Metadata.SignatureKind.Method" /> or <see cref="F:System.Reflection.Metadata.SignatureKind.LocalVariables" />.</summary>
		/// <exception cref="T:System.BadImageFormatException">The signature is invalid.</exception>
		/// <returns>An enumeration value that indicates the signature kind.</returns>
		// Token: 0x060007D1 RID: 2001 RVA: 0x00012B50 File Offset: 0x00010D50
		public StandaloneSignatureKind GetKind()
		{
			SignatureKind kind = this._reader.GetBlobReader(this.Signature).ReadSignatureHeader().Kind;
			StandaloneSignatureKind standaloneSignatureKind;
			if (kind != SignatureKind.Method)
			{
				if (kind != SignatureKind.LocalVariables)
				{
					throw new BadImageFormatException();
				}
				standaloneSignatureKind = StandaloneSignatureKind.LocalVariables;
			}
			else
			{
				standaloneSignatureKind = StandaloneSignatureKind.Method;
			}
			return standaloneSignatureKind;
		}

		// Token: 0x040004DF RID: 1247
		private readonly MetadataReader _reader;

		// Token: 0x040004E0 RID: 1248
		private readonly int _rowId;
	}
}
