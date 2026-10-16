using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000A2 RID: 162
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct MemberReference
	{
		// Token: 0x060005E4 RID: 1508 RVA: 0x0000D3B0 File Offset: 0x0000B5B0
		internal MemberReference(MetadataReader reader, uint treatmentAndRowId)
		{
			this._reader = reader;
			this._treatmentAndRowId = treatmentAndRowId;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0000D3C0 File Offset: 0x0000B5C0
		private int RowId
		{
			get
			{
				return (int)(this._treatmentAndRowId & 16777215U);
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0000D3CE File Offset: 0x0000B5CE
		private MemberRefTreatment Treatment
		{
			get
			{
				return (MemberRefTreatment)(this._treatmentAndRowId >> 24);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0000D3DA File Offset: 0x0000B5DA
		private MemberReferenceHandle Handle
		{
			get
			{
				return MemberReferenceHandle.FromRowId(this.RowId);
			}
		}

		/// <summary>Gets the parent entity handle.</summary>
		/// <returns>An entity handle instance. If the <see cref="P:System.Reflection.Metadata.EntityHandle.IsNil" /> property is <see langword="true" />, the returned handle will have default values.</returns>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0000D3E7 File Offset: 0x0000B5E7
		public EntityHandle Parent
		{
			get
			{
				if (this.Treatment == MemberRefTreatment.None)
				{
					return this._reader.MemberRefTable.GetClass(this.Handle);
				}
				return this.GetProjectedParent();
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0000D40E File Offset: 0x0000B60E
		public StringHandle Name
		{
			get
			{
				if (this.Treatment == MemberRefTreatment.None)
				{
					return this._reader.MemberRefTable.GetName(this.Handle);
				}
				return this.GetProjectedName();
			}
		}

		/// <summary>Gets a handle to the signature blob.</summary>
		/// <returns>A handle to the signature blob.</returns>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0000D435 File Offset: 0x0000B635
		public BlobHandle Signature
		{
			get
			{
				if (this.Treatment == MemberRefTreatment.None)
				{
					return this._reader.MemberRefTable.GetSignature(this.Handle);
				}
				return this.GetProjectedSignature();
			}
		}

		/// <param name="provider" />
		/// <param name="genericContext" />
		/// <typeparam name="TType" />
		/// <typeparam name="TGenericContext" />
		// Token: 0x060005EB RID: 1515 RVA: 0x0000D45C File Offset: 0x0000B65C
		public TType DecodeFieldSignature<[Nullable(2)] TType, [Nullable(2)] TGenericContext>(ISignatureTypeProvider<TType, TGenericContext> provider, TGenericContext genericContext)
		{
			SignatureDecoder<TType, TGenericContext> signatureDecoder = new SignatureDecoder<TType, TGenericContext>(provider, this._reader, genericContext);
			BlobReader blobReader = this._reader.GetBlobReader(this.Signature);
			return signatureDecoder.DecodeFieldSignature(ref blobReader);
		}

		/// <param name="provider" />
		/// <param name="genericContext" />
		/// <typeparam name="TType" />
		/// <typeparam name="TGenericContext" />
		// Token: 0x060005EC RID: 1516 RVA: 0x0000D494 File Offset: 0x0000B694
		[return: Nullable(new byte[] { 0, 1 })]
		public MethodSignature<TType> DecodeMethodSignature<[Nullable(2)] TType, [Nullable(2)] TGenericContext>(ISignatureTypeProvider<TType, TGenericContext> provider, TGenericContext genericContext)
		{
			SignatureDecoder<TType, TGenericContext> signatureDecoder = new SignatureDecoder<TType, TGenericContext>(provider, this._reader, genericContext);
			BlobReader blobReader = this._reader.GetBlobReader(this.Signature);
			return signatureDecoder.DecodeMethodSignature(ref blobReader);
		}

		/// <summary>Determines if the member reference is to a method or field.</summary>
		/// <exception cref="T:System.BadImageFormatException">The member reference signature is invalid.</exception>
		/// <returns>One of the enumeration values that indicates the kind of member reference.</returns>
		// Token: 0x060005ED RID: 1517 RVA: 0x0000D4CC File Offset: 0x0000B6CC
		public MemberReferenceKind GetKind()
		{
			SignatureKind kind = this._reader.GetBlobReader(this.Signature).ReadSignatureHeader().Kind;
			MemberReferenceKind memberReferenceKind;
			if (kind != SignatureKind.Method)
			{
				if (kind != SignatureKind.Field)
				{
					throw new BadImageFormatException();
				}
				memberReferenceKind = MemberReferenceKind.Field;
			}
			else
			{
				memberReferenceKind = MemberReferenceKind.Method;
			}
			return memberReferenceKind;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0000D513 File Offset: 0x0000B713
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0000D52B File Offset: 0x0000B72B
		private EntityHandle GetProjectedParent()
		{
			return this._reader.MemberRefTable.GetClass(this.Handle);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0000D543 File Offset: 0x0000B743
		private StringHandle GetProjectedName()
		{
			if (this.Treatment == MemberRefTreatment.Dispose)
			{
				return StringHandle.FromVirtualIndex(StringHandle.VirtualIndex.Dispose);
			}
			return this._reader.MemberRefTable.GetName(this.Handle);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0000D56B File Offset: 0x0000B76B
		private BlobHandle GetProjectedSignature()
		{
			return this._reader.MemberRefTable.GetSignature(this.Handle);
		}

		// Token: 0x040003BD RID: 957
		private readonly MetadataReader _reader;

		// Token: 0x040003BE RID: 958
		private readonly uint _treatmentAndRowId;
	}
}
