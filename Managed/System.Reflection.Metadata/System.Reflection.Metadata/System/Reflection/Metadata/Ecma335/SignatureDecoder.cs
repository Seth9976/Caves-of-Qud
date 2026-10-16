using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	/// <summary>Decodes signature blobs.</summary>
	/// <typeparam name="TType" />
	/// <typeparam name="TGenericContext" />
	// Token: 0x02000108 RID: 264
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct SignatureDecoder<[Nullable(2)] TType, [Nullable(2)] TGenericContext>
	{
		/// <summary>Creates a new <see cref="T:System.Reflection.Metadata.Ecma335.SignatureDecoder`2" />.</summary>
		/// <param name="provider">The provider used to obtain type symbols as the signature is decoded.</param>
		/// <param name="metadataReader">The metadata reader from which the signature was obtained. It may be <see langword="null" /> if the given provider allows it.</param>
		/// <param name="genericContext">Additional context needed to resolve generic parameters.</param>
		// Token: 0x060009D3 RID: 2515 RVA: 0x0001BF8B File Offset: 0x0001A18B
		public SignatureDecoder(ISignatureTypeProvider<TType, TGenericContext> provider, MetadataReader metadataReader, TGenericContext genericContext)
		{
			if (provider == null)
			{
				Throw.ArgumentNull("provider");
			}
			this._metadataReaderOpt = metadataReader;
			this._provider = provider;
			this._genericContext = genericContext;
		}

		/// <summary>Decodes a type embedded in a signature and advances the reader past the type.</summary>
		/// <param name="blobReader">The blob reader positioned at the leading <see cref="T:System.Reflection.Metadata.SignatureTypeCode" />.</param>
		/// <param name="allowTypeSpecifications">
		///   <see langword="true" /> to allow a <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" /> to follow a (CLASS | VALUETYPE) in the signature; <see langword="false" /> otherwise.</param>
		/// <exception cref="T:System.BadImageFormatException">The reader was not positioned at a valid signature type.</exception>
		/// <returns>The decoded type.</returns>
		// Token: 0x060009D4 RID: 2516 RVA: 0x0001BFAF File Offset: 0x0001A1AF
		public TType DecodeType(ref BlobReader blobReader, bool allowTypeSpecifications = false)
		{
			return this.DecodeType(ref blobReader, allowTypeSpecifications, blobReader.ReadCompressedInteger());
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0001BFC0 File Offset: 0x0001A1C0
		private TType DecodeType(ref BlobReader blobReader, bool allowTypeSpecifications, int typeCode)
		{
			switch (typeCode)
			{
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
			case 11:
			case 12:
			case 13:
			case 14:
			case 22:
			case 24:
			case 25:
			case 28:
				return this._provider.GetPrimitiveType((PrimitiveTypeCode)typeCode);
			case 15:
			{
				TType ttype = this.DecodeType(ref blobReader, false);
				return this._provider.GetPointerType(ttype);
			}
			case 16:
			{
				TType ttype = this.DecodeType(ref blobReader, false);
				return this._provider.GetByReferenceType(ttype);
			}
			case 17:
			case 18:
				return this.DecodeTypeHandle(ref blobReader, (byte)typeCode, allowTypeSpecifications);
			case 19:
			{
				int num = blobReader.ReadCompressedInteger();
				return this._provider.GetGenericTypeParameter(this._genericContext, num);
			}
			case 20:
				return this.DecodeArrayType(ref blobReader);
			case 21:
				return this.DecodeGenericTypeInstance(ref blobReader);
			case 23:
			case 26:
				break;
			case 27:
			{
				MethodSignature<TType> methodSignature = this.DecodeMethodSignature(ref blobReader);
				return this._provider.GetFunctionPointerType(methodSignature);
			}
			case 29:
			{
				TType ttype = this.DecodeType(ref blobReader, false);
				return this._provider.GetSZArrayType(ttype);
			}
			case 30:
			{
				int num = blobReader.ReadCompressedInteger();
				return this._provider.GetGenericMethodParameter(this._genericContext, num);
			}
			case 31:
				return this.DecodeModifiedType(ref blobReader, true);
			case 32:
				return this.DecodeModifiedType(ref blobReader, false);
			default:
				if (typeCode == 69)
				{
					TType ttype = this.DecodeType(ref blobReader, false);
					return this._provider.GetPinnedType(ttype);
				}
				break;
			}
			throw new BadImageFormatException(SR.Format(SR.UnexpectedSignatureTypeCode, typeCode));
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0001C150 File Offset: 0x0001A350
		private ImmutableArray<TType> DecodeTypeSequence(ref BlobReader blobReader)
		{
			int num = blobReader.ReadCompressedInteger();
			if (num == 0)
			{
				throw new BadImageFormatException(SR.SignatureTypeSequenceMustHaveAtLeastOneElement);
			}
			ImmutableArray<TType>.Builder builder = ImmutableArray.CreateBuilder<TType>(num);
			for (int i = 0; i < num; i++)
			{
				builder.Add(this.DecodeType(ref blobReader, false));
			}
			return builder.MoveToImmutable();
		}

		/// <summary>Decodes a method (definition, reference, or standalone) or a property signature blob.</summary>
		/// <param name="blobReader">A blob reader positioned at a method signature.</param>
		/// <exception cref="T:System.BadImageFormatException">The method signature is invalid.</exception>
		/// <returns>The decoded method signature.</returns>
		// Token: 0x060009D7 RID: 2519 RVA: 0x0001C19C File Offset: 0x0001A39C
		[return: Nullable(new byte[] { 0, 1 })]
		public MethodSignature<TType> DecodeMethodSignature(ref BlobReader blobReader)
		{
			SignatureHeader signatureHeader = blobReader.ReadSignatureHeader();
			this.CheckMethodOrPropertyHeader(signatureHeader);
			int num = 0;
			if (signatureHeader.IsGeneric)
			{
				num = blobReader.ReadCompressedInteger();
			}
			int num2 = blobReader.ReadCompressedInteger();
			TType ttype = this.DecodeType(ref blobReader, false);
			int num3;
			ImmutableArray<TType> immutableArray;
			if (num2 == 0)
			{
				num3 = 0;
				immutableArray = ImmutableArray<TType>.Empty;
			}
			else
			{
				ImmutableArray<TType>.Builder builder = ImmutableArray.CreateBuilder<TType>(num2);
				int i;
				for (i = 0; i < num2; i++)
				{
					int num4 = blobReader.ReadCompressedInteger();
					if (num4 == 65)
					{
						break;
					}
					builder.Add(this.DecodeType(ref blobReader, false, num4));
				}
				num3 = i;
				while (i < num2)
				{
					builder.Add(this.DecodeType(ref blobReader, false));
					i++;
				}
				immutableArray = builder.MoveToImmutable();
			}
			return new MethodSignature<TType>(signatureHeader, ttype, num3, num, immutableArray);
		}

		/// <summary>Decodes a method specification signature blob and advances the reader past the signature.</summary>
		/// <param name="blobReader">A blob reader positioned at a valid method specification signature.</param>
		/// <returns>The types used to instantiate a generic method via the method specification.</returns>
		// Token: 0x060009D8 RID: 2520 RVA: 0x0001C254 File Offset: 0x0001A454
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<TType> DecodeMethodSpecificationSignature(ref BlobReader blobReader)
		{
			SignatureHeader signatureHeader = blobReader.ReadSignatureHeader();
			this.CheckHeader(signatureHeader, SignatureKind.MethodSpecification);
			return this.DecodeTypeSequence(ref blobReader);
		}

		/// <summary>Decodes a local variable signature blob and advances the reader past the signature.</summary>
		/// <param name="blobReader">The blob reader positioned at a local variable signature.</param>
		/// <exception cref="T:System.BadImageFormatException">The local variable signature is invalid.</exception>
		/// <returns>The local variable types.</returns>
		// Token: 0x060009D9 RID: 2521 RVA: 0x0001C278 File Offset: 0x0001A478
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<TType> DecodeLocalSignature(ref BlobReader blobReader)
		{
			SignatureHeader signatureHeader = blobReader.ReadSignatureHeader();
			this.CheckHeader(signatureHeader, SignatureKind.LocalVariables);
			return this.DecodeTypeSequence(ref blobReader);
		}

		/// <summary>Decodes a field signature blob and advances the reader past the signature.</summary>
		/// <param name="blobReader">The blob reader positioned at a field signature.</param>
		/// <returns>The decoded field type.</returns>
		// Token: 0x060009DA RID: 2522 RVA: 0x0001C29C File Offset: 0x0001A49C
		public TType DecodeFieldSignature(ref BlobReader blobReader)
		{
			SignatureHeader signatureHeader = blobReader.ReadSignatureHeader();
			this.CheckHeader(signatureHeader, SignatureKind.Field);
			return this.DecodeType(ref blobReader, false);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0001C2C0 File Offset: 0x0001A4C0
		private TType DecodeArrayType(ref BlobReader blobReader)
		{
			TType ttype = this.DecodeType(ref blobReader, false);
			int num = blobReader.ReadCompressedInteger();
			ImmutableArray<int> immutableArray = ImmutableArray<int>.Empty;
			ImmutableArray<int> immutableArray2 = ImmutableArray<int>.Empty;
			int num2 = blobReader.ReadCompressedInteger();
			if (num2 > 0)
			{
				ImmutableArray<int>.Builder builder = ImmutableArray.CreateBuilder<int>(num2);
				for (int i = 0; i < num2; i++)
				{
					builder.Add(blobReader.ReadCompressedInteger());
				}
				immutableArray = builder.MoveToImmutable();
			}
			int num3 = blobReader.ReadCompressedInteger();
			if (num3 > 0)
			{
				ImmutableArray<int>.Builder builder2 = ImmutableArray.CreateBuilder<int>(num3);
				for (int j = 0; j < num3; j++)
				{
					builder2.Add(blobReader.ReadCompressedSignedInteger());
				}
				immutableArray2 = builder2.MoveToImmutable();
			}
			ArrayShape arrayShape = new ArrayShape(num, immutableArray, immutableArray2);
			return this._provider.GetArrayType(ttype, arrayShape);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0001C37C File Offset: 0x0001A57C
		private TType DecodeGenericTypeInstance(ref BlobReader blobReader)
		{
			TType ttype = this.DecodeType(ref blobReader, false);
			ImmutableArray<TType> immutableArray = this.DecodeTypeSequence(ref blobReader);
			return this._provider.GetGenericInstantiation(ttype, immutableArray);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		private TType DecodeModifiedType(ref BlobReader blobReader, bool isRequired)
		{
			TType ttype = this.DecodeTypeHandle(ref blobReader, 0, true);
			TType ttype2 = this.DecodeType(ref blobReader, false);
			return this._provider.GetModifiedType(ttype, ttype2, isRequired);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0001C3D8 File Offset: 0x0001A5D8
		private TType DecodeTypeHandle(ref BlobReader blobReader, byte rawTypeKind, bool allowTypeSpecifications)
		{
			EntityHandle entityHandle = blobReader.ReadTypeHandle();
			if (!entityHandle.IsNil)
			{
				HandleKind kind = entityHandle.Kind;
				if (kind == HandleKind.TypeReference)
				{
					return this._provider.GetTypeFromReference(this._metadataReaderOpt, (TypeReferenceHandle)entityHandle, rawTypeKind);
				}
				if (kind == HandleKind.TypeDefinition)
				{
					return this._provider.GetTypeFromDefinition(this._metadataReaderOpt, (TypeDefinitionHandle)entityHandle, rawTypeKind);
				}
				if (kind == HandleKind.TypeSpecification)
				{
					if (!allowTypeSpecifications)
					{
						throw new BadImageFormatException(SR.NotTypeDefOrRefHandle);
					}
					return this._provider.GetTypeFromSpecification(this._metadataReaderOpt, this._genericContext, (TypeSpecificationHandle)entityHandle, rawTypeKind);
				}
			}
			throw new BadImageFormatException(SR.NotTypeDefOrRefOrSpecHandle);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0001C475 File Offset: 0x0001A675
		private void CheckHeader(SignatureHeader header, SignatureKind expectedKind)
		{
			if (header.Kind != expectedKind)
			{
				throw new BadImageFormatException(SR.Format(SR.UnexpectedSignatureHeader, expectedKind, header.Kind, header.RawValue));
			}
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0001C4B0 File Offset: 0x0001A6B0
		private void CheckMethodOrPropertyHeader(SignatureHeader header)
		{
			SignatureKind kind = header.Kind;
			if (kind != SignatureKind.Method && kind != SignatureKind.Property)
			{
				throw new BadImageFormatException(SR.Format(SR.UnexpectedSignatureHeader2, new object[]
				{
					SignatureKind.Property,
					SignatureKind.Method,
					header.Kind,
					header.RawValue
				}));
			}
		}

		// Token: 0x040005A6 RID: 1446
		private readonly ISignatureTypeProvider<TType, TGenericContext> _provider;

		// Token: 0x040005A7 RID: 1447
		private readonly MetadataReader _metadataReaderOpt;

		// Token: 0x040005A8 RID: 1448
		private readonly TGenericContext _genericContext;
	}
}
