using System;
using System.Text;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Cecil.PE;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000B3 RID: 179
	internal sealed class SignatureReader : ByteBuffer
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00015E35 File Offset: 0x00014035
		private TypeSystem TypeSystem
		{
			get
			{
				return this.reader.module.TypeSystem;
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00015E47 File Offset: 0x00014047
		public SignatureReader(uint blob, MetadataReader reader)
			: base(reader.image.BlobHeap.data)
		{
			this.reader = reader;
			this.position = (int)blob;
			this.sig_length = base.ReadCompressedUInt32();
			this.start = (uint)this.position;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00015E85 File Offset: 0x00014085
		private MetadataToken ReadTypeTokenSignature()
		{
			return CodedIndex.TypeDefOrRef.GetMetadataToken(base.ReadCompressedUInt32());
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00015E94 File Offset: 0x00014094
		private GenericParameter GetGenericParameter(GenericParameterType type, uint var)
		{
			IGenericContext context = this.reader.context;
			if (context == null)
			{
				return this.GetUnboundGenericParameter(type, (int)var);
			}
			IGenericParameterProvider genericParameterProvider;
			if (type != GenericParameterType.Type)
			{
				if (type != GenericParameterType.Method)
				{
					throw new NotSupportedException();
				}
				genericParameterProvider = context.Method;
			}
			else
			{
				genericParameterProvider = context.Type;
			}
			if (!context.IsDefinition)
			{
				SignatureReader.CheckGenericContext(genericParameterProvider, (int)var);
			}
			if (var >= (uint)genericParameterProvider.GenericParameters.Count)
			{
				return this.GetUnboundGenericParameter(type, (int)var);
			}
			return genericParameterProvider.GenericParameters[(int)var];
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00015F0C File Offset: 0x0001410C
		private GenericParameter GetUnboundGenericParameter(GenericParameterType type, int index)
		{
			return new GenericParameter(index, type, this.reader.module);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00015F20 File Offset: 0x00014120
		private static void CheckGenericContext(IGenericParameterProvider owner, int index)
		{
			Collection<GenericParameter> genericParameters = owner.GenericParameters;
			for (int i = genericParameters.Count; i <= index; i++)
			{
				genericParameters.Add(new GenericParameter(owner));
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00015F54 File Offset: 0x00014154
		public void ReadGenericInstanceSignature(IGenericParameterProvider provider, IGenericInstance instance)
		{
			uint num = base.ReadCompressedUInt32();
			if (!provider.IsDefinition)
			{
				SignatureReader.CheckGenericContext(provider, (int)(num - 1U));
			}
			Collection<TypeReference> genericArguments = instance.GenericArguments;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				genericArguments.Add(this.ReadTypeSignature());
				num2++;
			}
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00015F9C File Offset: 0x0001419C
		private ArrayType ReadArrayTypeSignature()
		{
			ArrayType arrayType = new ArrayType(this.ReadTypeSignature());
			uint num = base.ReadCompressedUInt32();
			uint[] array = new uint[base.ReadCompressedUInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = base.ReadCompressedUInt32();
			}
			int[] array2 = new int[base.ReadCompressedUInt32()];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = base.ReadCompressedInt32();
			}
			arrayType.Dimensions.Clear();
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				int? num3 = default(int?);
				int? num4 = default(int?);
				if (num2 < array2.Length)
				{
					num3 = new int?(array2[num2]);
				}
				if (num2 < array.Length)
				{
					int? num5 = num3;
					int num6 = (int)array[num2];
					num4 = ((num5 != null) ? new int?(num5.GetValueOrDefault() + num6 - 1) : default(int?));
				}
				arrayType.Dimensions.Add(new ArrayDimension(num3, num4));
				num2++;
			}
			return arrayType;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00016099 File Offset: 0x00014299
		private TypeReference GetTypeDefOrRef(MetadataToken token)
		{
			return this.reader.GetTypeDefOrRef(token);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000160A7 File Offset: 0x000142A7
		public TypeReference ReadTypeSignature()
		{
			return this.ReadTypeSignature((ElementType)base.ReadByte());
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000160B5 File Offset: 0x000142B5
		public TypeReference ReadTypeToken()
		{
			return this.GetTypeDefOrRef(this.ReadTypeTokenSignature());
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000160C4 File Offset: 0x000142C4
		private TypeReference ReadTypeSignature(ElementType etype)
		{
			switch (etype)
			{
			case ElementType.Void:
				return this.TypeSystem.Void;
			case ElementType.Boolean:
			case ElementType.Char:
			case ElementType.I1:
			case ElementType.U1:
			case ElementType.I2:
			case ElementType.U2:
			case ElementType.I4:
			case ElementType.U4:
			case ElementType.I8:
			case ElementType.U8:
			case ElementType.R4:
			case ElementType.R8:
			case ElementType.String:
			case (ElementType)23:
			case (ElementType)26:
				break;
			case ElementType.Ptr:
				return new PointerType(this.ReadTypeSignature());
			case ElementType.ByRef:
				return new ByReferenceType(this.ReadTypeSignature());
			case ElementType.ValueType:
			{
				TypeReference typeDefOrRef = this.GetTypeDefOrRef(this.ReadTypeTokenSignature());
				typeDefOrRef.IsValueType = true;
				return typeDefOrRef;
			}
			case ElementType.Class:
				return this.GetTypeDefOrRef(this.ReadTypeTokenSignature());
			case ElementType.Var:
				return this.GetGenericParameter(GenericParameterType.Type, base.ReadCompressedUInt32());
			case ElementType.Array:
				return this.ReadArrayTypeSignature();
			case ElementType.GenericInst:
			{
				bool flag = base.ReadByte() == 17;
				TypeReference typeDefOrRef2 = this.GetTypeDefOrRef(this.ReadTypeTokenSignature());
				GenericInstanceType genericInstanceType = new GenericInstanceType(typeDefOrRef2);
				this.ReadGenericInstanceSignature(typeDefOrRef2, genericInstanceType);
				if (flag)
				{
					genericInstanceType.IsValueType = true;
					typeDefOrRef2.GetElementType().IsValueType = true;
				}
				return genericInstanceType;
			}
			case ElementType.TypedByRef:
				return this.TypeSystem.TypedReference;
			case ElementType.I:
				return this.TypeSystem.IntPtr;
			case ElementType.U:
				return this.TypeSystem.UIntPtr;
			case ElementType.FnPtr:
			{
				FunctionPointerType functionPointerType = new FunctionPointerType();
				this.ReadMethodSignature(functionPointerType);
				return functionPointerType;
			}
			case ElementType.Object:
				return this.TypeSystem.Object;
			case ElementType.SzArray:
				return new ArrayType(this.ReadTypeSignature());
			case ElementType.MVar:
				return this.GetGenericParameter(GenericParameterType.Method, base.ReadCompressedUInt32());
			case ElementType.CModReqD:
				return new RequiredModifierType(this.GetTypeDefOrRef(this.ReadTypeTokenSignature()), this.ReadTypeSignature());
			case ElementType.CModOpt:
				return new OptionalModifierType(this.GetTypeDefOrRef(this.ReadTypeTokenSignature()), this.ReadTypeSignature());
			default:
				if (etype == ElementType.Sentinel)
				{
					return new SentinelType(this.ReadTypeSignature());
				}
				if (etype == ElementType.Pinned)
				{
					return new PinnedType(this.ReadTypeSignature());
				}
				break;
			}
			return this.GetPrimitiveType(etype);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000162B4 File Offset: 0x000144B4
		public void ReadMethodSignature(IMethodSignature method)
		{
			byte b = base.ReadByte();
			if ((b & 32) != 0)
			{
				method.HasThis = true;
				b = (byte)((int)b & -33);
			}
			if ((b & 64) != 0)
			{
				method.ExplicitThis = true;
				b = (byte)((int)b & -65);
			}
			method.CallingConvention = (MethodCallingConvention)b;
			MethodReference methodReference = method as MethodReference;
			if (methodReference != null && !methodReference.DeclaringType.IsArray)
			{
				this.reader.context = methodReference;
			}
			if ((b & 16) != 0)
			{
				uint num = base.ReadCompressedUInt32();
				if (methodReference != null && !methodReference.IsDefinition)
				{
					SignatureReader.CheckGenericContext(methodReference, (int)(num - 1U));
				}
			}
			uint num2 = base.ReadCompressedUInt32();
			method.MethodReturnType.ReturnType = this.ReadTypeSignature();
			if (num2 != 0U)
			{
				MethodReference methodReference2 = method as MethodReference;
				Collection<ParameterDefinition> collection = ((methodReference2 == null) ? method.Parameters : (methodReference2.parameters = new ParameterDefinitionCollection(method, (int)num2)));
				int num3 = 0;
				while ((long)num3 < (long)((ulong)num2))
				{
					collection.Add(new ParameterDefinition(this.ReadTypeSignature()));
					num3++;
				}
			}
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x000163A1 File Offset: 0x000145A1
		public object ReadConstantSignature(ElementType type)
		{
			return this.ReadPrimitiveValue(type);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x000163AC File Offset: 0x000145AC
		public void ReadCustomAttributeConstructorArguments(CustomAttribute attribute, Collection<ParameterDefinition> parameters)
		{
			int count = parameters.Count;
			if (count != 0)
			{
				attribute.arguments = new Collection<CustomAttributeArgument>(count);
				for (int i = 0; i < count; i++)
				{
					attribute.arguments.Add(this.ReadCustomAttributeFixedArgument(parameters[i].ParameterType));
				}
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x000163F8 File Offset: 0x000145F8
		private CustomAttributeArgument ReadCustomAttributeFixedArgument(TypeReference type)
		{
			if (type.IsArray)
			{
				return this.ReadCustomAttributeFixedArrayArgument((ArrayType)type);
			}
			return this.ReadCustomAttributeElement(type);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00016418 File Offset: 0x00014618
		public void ReadCustomAttributeNamedArguments(ushort count, ref Collection<CustomAttributeNamedArgument> fields, ref Collection<CustomAttributeNamedArgument> properties)
		{
			int num = 0;
			while (num < (int)count && this.CanReadMore())
			{
				this.ReadCustomAttributeNamedArgument(ref fields, ref properties);
				num++;
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00016444 File Offset: 0x00014644
		private void ReadCustomAttributeNamedArgument(ref Collection<CustomAttributeNamedArgument> fields, ref Collection<CustomAttributeNamedArgument> properties)
		{
			byte b = base.ReadByte();
			TypeReference typeReference = this.ReadCustomAttributeFieldOrPropType();
			string text = this.ReadUTF8String();
			Collection<CustomAttributeNamedArgument> collection;
			if (b != 83)
			{
				if (b != 84)
				{
					throw new NotSupportedException();
				}
				collection = SignatureReader.GetCustomAttributeNamedArgumentCollection(ref properties);
			}
			else
			{
				collection = SignatureReader.GetCustomAttributeNamedArgumentCollection(ref fields);
			}
			collection.Add(new CustomAttributeNamedArgument(text, this.ReadCustomAttributeFixedArgument(typeReference)));
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000164A0 File Offset: 0x000146A0
		private static Collection<CustomAttributeNamedArgument> GetCustomAttributeNamedArgumentCollection(ref Collection<CustomAttributeNamedArgument> collection)
		{
			if (collection != null)
			{
				return collection;
			}
			Collection<CustomAttributeNamedArgument> collection2;
			collection = (collection2 = new Collection<CustomAttributeNamedArgument>());
			return collection2;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000164C0 File Offset: 0x000146C0
		private CustomAttributeArgument ReadCustomAttributeFixedArrayArgument(ArrayType type)
		{
			uint num = base.ReadUInt32();
			if (num == 0U)
			{
				return new CustomAttributeArgument(type, Empty<CustomAttributeArgument>.Array);
			}
			if (num == 4294967295U)
			{
				return new CustomAttributeArgument(type, null);
			}
			CustomAttributeArgument[] array = new CustomAttributeArgument[num];
			TypeReference elementType = type.ElementType;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				array[num2] = this.ReadCustomAttributeElement(elementType);
				num2++;
			}
			return new CustomAttributeArgument(type, array);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00016520 File Offset: 0x00014720
		private CustomAttributeArgument ReadCustomAttributeElement(TypeReference type)
		{
			if (type.IsArray)
			{
				return this.ReadCustomAttributeFixedArrayArgument((ArrayType)type);
			}
			return new CustomAttributeArgument(type, (type.etype == ElementType.Object) ? this.ReadCustomAttributeElement(this.ReadCustomAttributeFieldOrPropType()) : this.ReadCustomAttributeElementValue(type));
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001656C File Offset: 0x0001476C
		private object ReadCustomAttributeElementValue(TypeReference type)
		{
			ElementType etype = type.etype;
			if (etype != ElementType.None)
			{
				if (etype == ElementType.String)
				{
					return this.ReadUTF8String();
				}
				return this.ReadPrimitiveValue(etype);
			}
			else
			{
				if (type.IsTypeOf("System", "Type"))
				{
					return this.ReadTypeReference();
				}
				return this.ReadCustomAttributeEnum(type);
			}
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000165B8 File Offset: 0x000147B8
		private object ReadPrimitiveValue(ElementType type)
		{
			switch (type)
			{
			case ElementType.Boolean:
				return base.ReadByte() == 1;
			case ElementType.Char:
				return (char)base.ReadUInt16();
			case ElementType.I1:
				return (sbyte)base.ReadByte();
			case ElementType.U1:
				return base.ReadByte();
			case ElementType.I2:
				return base.ReadInt16();
			case ElementType.U2:
				return base.ReadUInt16();
			case ElementType.I4:
				return base.ReadInt32();
			case ElementType.U4:
				return base.ReadUInt32();
			case ElementType.I8:
				return base.ReadInt64();
			case ElementType.U8:
				return base.ReadUInt64();
			case ElementType.R4:
				return base.ReadSingle();
			case ElementType.R8:
				return base.ReadDouble();
			default:
				throw new NotImplementedException(type.ToString());
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x000166A8 File Offset: 0x000148A8
		private TypeReference GetPrimitiveType(ElementType etype)
		{
			switch (etype)
			{
			case ElementType.Boolean:
				return this.TypeSystem.Boolean;
			case ElementType.Char:
				return this.TypeSystem.Char;
			case ElementType.I1:
				return this.TypeSystem.SByte;
			case ElementType.U1:
				return this.TypeSystem.Byte;
			case ElementType.I2:
				return this.TypeSystem.Int16;
			case ElementType.U2:
				return this.TypeSystem.UInt16;
			case ElementType.I4:
				return this.TypeSystem.Int32;
			case ElementType.U4:
				return this.TypeSystem.UInt32;
			case ElementType.I8:
				return this.TypeSystem.Int64;
			case ElementType.U8:
				return this.TypeSystem.UInt64;
			case ElementType.R4:
				return this.TypeSystem.Single;
			case ElementType.R8:
				return this.TypeSystem.Double;
			case ElementType.String:
				return this.TypeSystem.String;
			default:
				throw new NotImplementedException(etype.ToString());
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000167A4 File Offset: 0x000149A4
		private TypeReference ReadCustomAttributeFieldOrPropType()
		{
			ElementType elementType = (ElementType)base.ReadByte();
			if (elementType <= ElementType.Type)
			{
				if (elementType == ElementType.SzArray)
				{
					return new ArrayType(this.ReadCustomAttributeFieldOrPropType());
				}
				if (elementType == ElementType.Type)
				{
					return this.TypeSystem.LookupType("System", "Type");
				}
			}
			else
			{
				if (elementType == ElementType.Boxed)
				{
					return this.TypeSystem.Object;
				}
				if (elementType == ElementType.Enum)
				{
					return this.ReadTypeReference();
				}
			}
			return this.GetPrimitiveType(elementType);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00016811 File Offset: 0x00014A11
		public TypeReference ReadTypeReference()
		{
			return TypeParser.ParseType(this.reader.module, this.ReadUTF8String());
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001682C File Offset: 0x00014A2C
		private object ReadCustomAttributeEnum(TypeReference enum_type)
		{
			TypeDefinition typeDefinition = enum_type.CheckedResolve();
			if (!typeDefinition.IsEnum)
			{
				throw new ArgumentException();
			}
			return this.ReadCustomAttributeElementValue(typeDefinition.GetEnumUnderlyingType());
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001685C File Offset: 0x00014A5C
		public SecurityAttribute ReadSecurityAttribute()
		{
			SecurityAttribute securityAttribute = new SecurityAttribute(this.ReadTypeReference());
			base.ReadCompressedUInt32();
			this.ReadCustomAttributeNamedArguments((ushort)base.ReadCompressedUInt32(), ref securityAttribute.fields, ref securityAttribute.properties);
			return securityAttribute;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00016898 File Offset: 0x00014A98
		public MarshalInfo ReadMarshalInfo()
		{
			NativeType nativeType = this.ReadNativeType();
			if (nativeType <= NativeType.SafeArray)
			{
				if (nativeType == NativeType.FixedSysString)
				{
					FixedSysStringMarshalInfo fixedSysStringMarshalInfo = new FixedSysStringMarshalInfo();
					if (this.CanReadMore())
					{
						fixedSysStringMarshalInfo.size = (int)base.ReadCompressedUInt32();
					}
					return fixedSysStringMarshalInfo;
				}
				if (nativeType == NativeType.SafeArray)
				{
					SafeArrayMarshalInfo safeArrayMarshalInfo = new SafeArrayMarshalInfo();
					if (this.CanReadMore())
					{
						safeArrayMarshalInfo.element_type = this.ReadVariantType();
					}
					return safeArrayMarshalInfo;
				}
			}
			else
			{
				if (nativeType == NativeType.FixedArray)
				{
					FixedArrayMarshalInfo fixedArrayMarshalInfo = new FixedArrayMarshalInfo();
					if (this.CanReadMore())
					{
						fixedArrayMarshalInfo.size = (int)base.ReadCompressedUInt32();
					}
					if (this.CanReadMore())
					{
						fixedArrayMarshalInfo.element_type = this.ReadNativeType();
					}
					return fixedArrayMarshalInfo;
				}
				if (nativeType == NativeType.Array)
				{
					ArrayMarshalInfo arrayMarshalInfo = new ArrayMarshalInfo();
					if (this.CanReadMore())
					{
						arrayMarshalInfo.element_type = this.ReadNativeType();
					}
					if (this.CanReadMore())
					{
						arrayMarshalInfo.size_parameter_index = (int)base.ReadCompressedUInt32();
					}
					if (this.CanReadMore())
					{
						arrayMarshalInfo.size = (int)base.ReadCompressedUInt32();
					}
					if (this.CanReadMore())
					{
						arrayMarshalInfo.size_parameter_multiplier = (int)base.ReadCompressedUInt32();
					}
					return arrayMarshalInfo;
				}
				if (nativeType == NativeType.CustomMarshaler)
				{
					CustomMarshalInfo customMarshalInfo = new CustomMarshalInfo();
					string text = this.ReadUTF8String();
					customMarshalInfo.guid = ((!string.IsNullOrEmpty(text)) ? new Guid(text) : Guid.Empty);
					customMarshalInfo.unmanaged_type = this.ReadUTF8String();
					customMarshalInfo.managed_type = this.ReadTypeReference();
					customMarshalInfo.cookie = this.ReadUTF8String();
					return customMarshalInfo;
				}
			}
			return new MarshalInfo(nativeType);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000169FD File Offset: 0x00014BFD
		private NativeType ReadNativeType()
		{
			return (NativeType)base.ReadByte();
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000169FD File Offset: 0x00014BFD
		private VariantType ReadVariantType()
		{
			return (VariantType)base.ReadByte();
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00016A08 File Offset: 0x00014C08
		private string ReadUTF8String()
		{
			if (this.buffer[this.position] == 255)
			{
				this.position++;
				return null;
			}
			int num = (int)base.ReadCompressedUInt32();
			if (num == 0)
			{
				return string.Empty;
			}
			string @string = Encoding.UTF8.GetString(this.buffer, this.position, (this.buffer[this.position + num - 1] == 0) ? (num - 1) : num);
			this.position += num;
			return @string;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00016A88 File Offset: 0x00014C88
		public string ReadDocumentName()
		{
			char c = (char)this.buffer[this.position];
			this.position++;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (this.CanReadMore())
			{
				if (num > 0 && c != '\0')
				{
					stringBuilder.Append(c);
				}
				uint num2 = base.ReadCompressedUInt32();
				if (num2 != 0U)
				{
					stringBuilder.Append(this.reader.ReadUTF8StringBlob(num2));
				}
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00016AFC File Offset: 0x00014CFC
		public Collection<SequencePoint> ReadSequencePoints(Document document)
		{
			Collection<SequencePoint> collection = new Collection<SequencePoint>();
			base.ReadCompressedUInt32();
			if (document == null)
			{
				document = this.reader.GetDocument(base.ReadCompressedUInt32());
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			bool flag = true;
			int num4 = 0;
			while (this.CanReadMore())
			{
				int num5 = (int)base.ReadCompressedUInt32();
				if (num4 > 0 && num5 == 0)
				{
					document = this.reader.GetDocument(base.ReadCompressedUInt32());
				}
				else
				{
					num += num5;
					int num6 = (int)base.ReadCompressedUInt32();
					int num7 = (int)((num6 == 0) ? base.ReadCompressedUInt32() : ((uint)base.ReadCompressedInt32()));
					if (num6 == 0 && num7 == 0)
					{
						collection.Add(new SequencePoint(num, document)
						{
							StartLine = 16707566,
							EndLine = 16707566,
							StartColumn = 0,
							EndColumn = 0
						});
					}
					else
					{
						if (flag)
						{
							num2 = (int)base.ReadCompressedUInt32();
							num3 = (int)base.ReadCompressedUInt32();
						}
						else
						{
							num2 += base.ReadCompressedInt32();
							num3 += base.ReadCompressedInt32();
						}
						collection.Add(new SequencePoint(num, document)
						{
							StartLine = num2,
							StartColumn = num3,
							EndLine = num2 + num6,
							EndColumn = num3 + num7
						});
						flag = false;
					}
				}
				num4++;
			}
			return collection;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00016C2C File Offset: 0x00014E2C
		public bool CanReadMore()
		{
			return (long)this.position - (long)((ulong)this.start) < (long)((ulong)this.sig_length);
		}

		// Token: 0x04000274 RID: 628
		private readonly MetadataReader reader;

		// Token: 0x04000275 RID: 629
		internal readonly uint start;

		// Token: 0x04000276 RID: 630
		internal readonly uint sig_length;
	}
}
