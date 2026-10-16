using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000E7 RID: 231
	internal readonly struct CustomAttributeDecoder<[Nullable(2)] TType>
	{
		// Token: 0x060008BA RID: 2234 RVA: 0x00018748 File Offset: 0x00016948
		[NullableContext(1)]
		public CustomAttributeDecoder(ICustomAttributeTypeProvider<TType> provider, MetadataReader reader)
		{
			this._reader = reader;
			this._provider = provider;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00018758 File Offset: 0x00016958
		[return: Nullable(new byte[] { 0, 1 })]
		public CustomAttributeValue<TType> DecodeValue(EntityHandle constructor, BlobHandle value)
		{
			HandleKind kind = constructor.Kind;
			BlobHandle blobHandle;
			if (kind != HandleKind.MethodDefinition)
			{
				if (kind != HandleKind.MemberReference)
				{
					throw new BadImageFormatException();
				}
				blobHandle = this._reader.GetMemberReference((MemberReferenceHandle)constructor).Signature;
			}
			else
			{
				blobHandle = this._reader.GetMethodDefinition((MethodDefinitionHandle)constructor).Signature;
			}
			BlobReader blobReader = this._reader.GetBlobReader(blobHandle);
			BlobReader blobReader2 = this._reader.GetBlobReader(value);
			ushort num = blobReader2.ReadUInt16();
			if (num != 1)
			{
				throw new BadImageFormatException();
			}
			SignatureHeader signatureHeader = blobReader.ReadSignatureHeader();
			if (signatureHeader.Kind != SignatureKind.Method || signatureHeader.IsGeneric)
			{
				throw new BadImageFormatException();
			}
			int num2 = blobReader.ReadCompressedInteger();
			SignatureTypeCode signatureTypeCode = blobReader.ReadSignatureTypeCode();
			if (signatureTypeCode != SignatureTypeCode.Void)
			{
				throw new BadImageFormatException();
			}
			ImmutableArray<CustomAttributeTypedArgument<TType>> immutableArray = this.DecodeFixedArguments(ref blobReader, ref blobReader2, num2);
			ImmutableArray<CustomAttributeNamedArgument<TType>> immutableArray2 = this.DecodeNamedArguments(ref blobReader2);
			return new CustomAttributeValue<TType>(immutableArray, immutableArray2);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00018848 File Offset: 0x00016A48
		private ImmutableArray<CustomAttributeTypedArgument<TType>> DecodeFixedArguments(ref BlobReader signatureReader, ref BlobReader valueReader, int count)
		{
			if (count == 0)
			{
				return ImmutableArray<CustomAttributeTypedArgument<TType>>.Empty;
			}
			ImmutableArray<CustomAttributeTypedArgument<TType>>.Builder builder = ImmutableArray.CreateBuilder<CustomAttributeTypedArgument<TType>>(count);
			for (int i = 0; i < count; i++)
			{
				CustomAttributeDecoder<TType>.ArgumentTypeInfo argumentTypeInfo = this.DecodeFixedArgumentType(ref signatureReader, false);
				builder.Add(this.DecodeArgument(ref valueReader, argumentTypeInfo));
			}
			return builder.MoveToImmutable();
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00018890 File Offset: 0x00016A90
		private ImmutableArray<CustomAttributeNamedArgument<TType>> DecodeNamedArguments(ref BlobReader valueReader)
		{
			int num = (int)valueReader.ReadUInt16();
			if (num == 0)
			{
				return ImmutableArray<CustomAttributeNamedArgument<TType>>.Empty;
			}
			ImmutableArray<CustomAttributeNamedArgument<TType>>.Builder builder = ImmutableArray.CreateBuilder<CustomAttributeNamedArgument<TType>>(num);
			for (int i = 0; i < num; i++)
			{
				CustomAttributeNamedArgumentKind customAttributeNamedArgumentKind = (CustomAttributeNamedArgumentKind)valueReader.ReadSerializationTypeCode();
				if (customAttributeNamedArgumentKind != CustomAttributeNamedArgumentKind.Field && customAttributeNamedArgumentKind != CustomAttributeNamedArgumentKind.Property)
				{
					throw new BadImageFormatException();
				}
				CustomAttributeDecoder<TType>.ArgumentTypeInfo argumentTypeInfo = this.DecodeNamedArgumentType(ref valueReader, false);
				string text = valueReader.ReadSerializedString();
				CustomAttributeTypedArgument<TType> customAttributeTypedArgument = this.DecodeArgument(ref valueReader, argumentTypeInfo);
				builder.Add(new CustomAttributeNamedArgument<TType>(text, customAttributeNamedArgumentKind, customAttributeTypedArgument.Type, customAttributeTypedArgument.Value));
			}
			return builder.MoveToImmutable();
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00018918 File Offset: 0x00016B18
		private CustomAttributeDecoder<TType>.ArgumentTypeInfo DecodeFixedArgumentType(ref BlobReader signatureReader, bool isElementType = false)
		{
			SignatureTypeCode signatureTypeCode = signatureReader.ReadSignatureTypeCode();
			CustomAttributeDecoder<TType>.ArgumentTypeInfo argumentTypeInfo = new CustomAttributeDecoder<TType>.ArgumentTypeInfo
			{
				TypeCode = (SerializationTypeCode)signatureTypeCode
			};
			switch (signatureTypeCode)
			{
			case SignatureTypeCode.Boolean:
			case SignatureTypeCode.Char:
			case SignatureTypeCode.SByte:
			case SignatureTypeCode.Byte:
			case SignatureTypeCode.Int16:
			case SignatureTypeCode.UInt16:
			case SignatureTypeCode.Int32:
			case SignatureTypeCode.UInt32:
			case SignatureTypeCode.Int64:
			case SignatureTypeCode.UInt64:
			case SignatureTypeCode.Single:
			case SignatureTypeCode.Double:
			case SignatureTypeCode.String:
				argumentTypeInfo.Type = this._provider.GetPrimitiveType((PrimitiveTypeCode)signatureTypeCode);
				return argumentTypeInfo;
			case SignatureTypeCode.Pointer:
			case SignatureTypeCode.ByReference:
			case (SignatureTypeCode)17:
			case (SignatureTypeCode)18:
			case SignatureTypeCode.GenericTypeParameter:
			case SignatureTypeCode.Array:
			case SignatureTypeCode.GenericTypeInstance:
			case SignatureTypeCode.TypedReference:
			case (SignatureTypeCode)23:
			case SignatureTypeCode.IntPtr:
			case SignatureTypeCode.UIntPtr:
			case (SignatureTypeCode)26:
			case SignatureTypeCode.FunctionPointer:
				break;
			case SignatureTypeCode.Object:
				argumentTypeInfo.TypeCode = SerializationTypeCode.TaggedObject;
				argumentTypeInfo.Type = this._provider.GetPrimitiveType(PrimitiveTypeCode.Object);
				return argumentTypeInfo;
			case SignatureTypeCode.SZArray:
			{
				if (isElementType)
				{
					throw new BadImageFormatException();
				}
				CustomAttributeDecoder<TType>.ArgumentTypeInfo argumentTypeInfo2 = this.DecodeFixedArgumentType(ref signatureReader, true);
				argumentTypeInfo.ElementType = argumentTypeInfo2.Type;
				argumentTypeInfo.ElementTypeCode = argumentTypeInfo2.TypeCode;
				argumentTypeInfo.Type = this._provider.GetSZArrayType(argumentTypeInfo.ElementType);
				return argumentTypeInfo;
			}
			default:
				if (signatureTypeCode == SignatureTypeCode.TypeHandle)
				{
					EntityHandle entityHandle = signatureReader.ReadTypeHandle();
					argumentTypeInfo.Type = this.GetTypeFromHandle(entityHandle);
					argumentTypeInfo.TypeCode = (SerializationTypeCode)(this._provider.IsSystemType(argumentTypeInfo.Type) ? ((PrimitiveTypeCode)80) : this._provider.GetUnderlyingEnumType(argumentTypeInfo.Type));
					return argumentTypeInfo;
				}
				break;
			}
			throw new BadImageFormatException();
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00018A90 File Offset: 0x00016C90
		private CustomAttributeDecoder<TType>.ArgumentTypeInfo DecodeNamedArgumentType(ref BlobReader valueReader, bool isElementType = false)
		{
			CustomAttributeDecoder<TType>.ArgumentTypeInfo argumentTypeInfo = new CustomAttributeDecoder<TType>.ArgumentTypeInfo
			{
				TypeCode = valueReader.ReadSerializationTypeCode()
			};
			SerializationTypeCode typeCode = argumentTypeInfo.TypeCode;
			if (typeCode <= SerializationTypeCode.SZArray)
			{
				if (typeCode - SerializationTypeCode.Boolean <= 12)
				{
					argumentTypeInfo.Type = this._provider.GetPrimitiveType((PrimitiveTypeCode)argumentTypeInfo.TypeCode);
					return argumentTypeInfo;
				}
				if (typeCode == SerializationTypeCode.SZArray)
				{
					if (isElementType)
					{
						throw new BadImageFormatException();
					}
					CustomAttributeDecoder<TType>.ArgumentTypeInfo argumentTypeInfo2 = this.DecodeNamedArgumentType(ref valueReader, true);
					argumentTypeInfo.ElementType = argumentTypeInfo2.Type;
					argumentTypeInfo.ElementTypeCode = argumentTypeInfo2.TypeCode;
					argumentTypeInfo.Type = this._provider.GetSZArrayType(argumentTypeInfo.ElementType);
					return argumentTypeInfo;
				}
			}
			else
			{
				if (typeCode == SerializationTypeCode.Type)
				{
					argumentTypeInfo.Type = this._provider.GetSystemType();
					return argumentTypeInfo;
				}
				if (typeCode == SerializationTypeCode.TaggedObject)
				{
					argumentTypeInfo.Type = this._provider.GetPrimitiveType(PrimitiveTypeCode.Object);
					return argumentTypeInfo;
				}
				if (typeCode == SerializationTypeCode.Enum)
				{
					string text = valueReader.ReadSerializedString();
					argumentTypeInfo.Type = this._provider.GetTypeFromSerializedName(text);
					argumentTypeInfo.TypeCode = (SerializationTypeCode)this._provider.GetUnderlyingEnumType(argumentTypeInfo.Type);
					return argumentTypeInfo;
				}
			}
			throw new BadImageFormatException();
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00018BC0 File Offset: 0x00016DC0
		private CustomAttributeTypedArgument<TType> DecodeArgument(ref BlobReader valueReader, CustomAttributeDecoder<TType>.ArgumentTypeInfo info)
		{
			if (info.TypeCode == SerializationTypeCode.TaggedObject)
			{
				info = this.DecodeNamedArgumentType(ref valueReader, false);
			}
			SerializationTypeCode typeCode = info.TypeCode;
			object obj;
			switch (typeCode)
			{
			case SerializationTypeCode.Boolean:
				obj = valueReader.ReadBoolean();
				break;
			case SerializationTypeCode.Char:
				obj = valueReader.ReadChar();
				break;
			case SerializationTypeCode.SByte:
				obj = valueReader.ReadSByte();
				break;
			case SerializationTypeCode.Byte:
				obj = valueReader.ReadByte();
				break;
			case SerializationTypeCode.Int16:
				obj = valueReader.ReadInt16();
				break;
			case SerializationTypeCode.UInt16:
				obj = valueReader.ReadUInt16();
				break;
			case SerializationTypeCode.Int32:
				obj = valueReader.ReadInt32();
				break;
			case SerializationTypeCode.UInt32:
				obj = valueReader.ReadUInt32();
				break;
			case SerializationTypeCode.Int64:
				obj = valueReader.ReadInt64();
				break;
			case SerializationTypeCode.UInt64:
				obj = valueReader.ReadUInt64();
				break;
			case SerializationTypeCode.Single:
				obj = valueReader.ReadSingle();
				break;
			case SerializationTypeCode.Double:
				obj = valueReader.ReadDouble();
				break;
			case SerializationTypeCode.String:
				obj = valueReader.ReadSerializedString();
				break;
			default:
				if (typeCode != SerializationTypeCode.SZArray)
				{
					if (typeCode != SerializationTypeCode.Type)
					{
						throw new BadImageFormatException();
					}
					string text = valueReader.ReadSerializedString();
					obj = this._provider.GetTypeFromSerializedName(text);
				}
				else
				{
					obj = this.DecodeArrayArgument(ref valueReader, info);
				}
				break;
			}
			return new CustomAttributeTypedArgument<TType>(info.Type, obj);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00018D3C File Offset: 0x00016F3C
		private ImmutableArray<CustomAttributeTypedArgument<TType>>? DecodeArrayArgument(ref BlobReader blobReader, CustomAttributeDecoder<TType>.ArgumentTypeInfo info)
		{
			int num = blobReader.ReadInt32();
			if (num == -1)
			{
				return default(ImmutableArray<CustomAttributeTypedArgument<TType>>?);
			}
			if (num == 0)
			{
				return new ImmutableArray<CustomAttributeTypedArgument<TType>>?(ImmutableArray<CustomAttributeTypedArgument<TType>>.Empty);
			}
			if (num < 0)
			{
				throw new BadImageFormatException();
			}
			CustomAttributeDecoder<TType>.ArgumentTypeInfo argumentTypeInfo = new CustomAttributeDecoder<TType>.ArgumentTypeInfo
			{
				Type = info.ElementType,
				TypeCode = info.ElementTypeCode
			};
			ImmutableArray<CustomAttributeTypedArgument<TType>>.Builder builder = ImmutableArray.CreateBuilder<CustomAttributeTypedArgument<TType>>(num);
			for (int i = 0; i < num; i++)
			{
				builder.Add(this.DecodeArgument(ref blobReader, argumentTypeInfo));
			}
			return new ImmutableArray<CustomAttributeTypedArgument<TType>>?(builder.MoveToImmutable());
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00018DCC File Offset: 0x00016FCC
		private TType GetTypeFromHandle(EntityHandle handle)
		{
			HandleKind kind = handle.Kind;
			TType ttype;
			if (kind != HandleKind.TypeReference)
			{
				if (kind != HandleKind.TypeDefinition)
				{
					throw new BadImageFormatException(SR.NotTypeDefOrRefHandle);
				}
				ttype = this._provider.GetTypeFromDefinition(this._reader, (TypeDefinitionHandle)handle, 0);
			}
			else
			{
				ttype = this._provider.GetTypeFromReference(this._reader, (TypeReferenceHandle)handle, 0);
			}
			return ttype;
		}

		// Token: 0x04000546 RID: 1350
		private readonly ICustomAttributeTypeProvider<TType> _provider;

		// Token: 0x04000547 RID: 1351
		private readonly MetadataReader _reader;

		// Token: 0x020001E9 RID: 489
		private struct ArgumentTypeInfo
		{
			// Token: 0x04000B2A RID: 2858
			public TType Type;

			// Token: 0x04000B2B RID: 2859
			public TType ElementType;

			// Token: 0x04000B2C RID: 2860
			public SerializationTypeCode TypeCode;

			// Token: 0x04000B2D RID: 2861
			public SerializationTypeCode ElementTypeCode;
		}
	}
}
