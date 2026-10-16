using System;
using System.Text;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Cecil.PE;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000B4 RID: 180
	internal sealed class SignatureWriter : ByteBuffer
	{
		// Token: 0x06000769 RID: 1897 RVA: 0x00016C46 File Offset: 0x00014E46
		public SignatureWriter(MetadataBuilder metadata)
			: base(6)
		{
			this.metadata = metadata;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00016C56 File Offset: 0x00014E56
		public void WriteElementType(ElementType element_type)
		{
			base.WriteByte((byte)element_type);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00016C60 File Offset: 0x00014E60
		public void WriteUTF8String(string @string)
		{
			if (@string == null)
			{
				base.WriteByte(byte.MaxValue);
				return;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(@string);
			base.WriteCompressedUInt32((uint)bytes.Length);
			base.WriteBytes(bytes);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00016C98 File Offset: 0x00014E98
		public void WriteMethodSignature(IMethodSignature method)
		{
			byte b = (byte)method.CallingConvention;
			if (method.HasThis)
			{
				b |= 32;
			}
			if (method.ExplicitThis)
			{
				b |= 64;
			}
			IGenericParameterProvider genericParameterProvider = method as IGenericParameterProvider;
			int num = ((genericParameterProvider != null && genericParameterProvider.HasGenericParameters) ? genericParameterProvider.GenericParameters.Count : 0);
			if (num > 0)
			{
				b |= 16;
			}
			int num2 = (method.HasParameters ? method.Parameters.Count : 0);
			base.WriteByte(b);
			if (num > 0)
			{
				base.WriteCompressedUInt32((uint)num);
			}
			base.WriteCompressedUInt32((uint)num2);
			this.WriteTypeSignature(method.ReturnType);
			if (num2 != 0)
			{
				Collection<ParameterDefinition> parameters = method.Parameters;
				for (int i = 0; i < num2; i++)
				{
					this.WriteTypeSignature(parameters[i].ParameterType);
				}
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00016D5E File Offset: 0x00014F5E
		private uint MakeTypeDefOrRefCodedRID(TypeReference type)
		{
			return CodedIndex.TypeDefOrRef.CompressMetadataToken(this.metadata.LookupToken(type));
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00016D72 File Offset: 0x00014F72
		public void WriteTypeToken(TypeReference type)
		{
			base.WriteCompressedUInt32(this.MakeTypeDefOrRefCodedRID(type));
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00016D84 File Offset: 0x00014F84
		public void WriteTypeSignature(TypeReference type)
		{
			if (type == null)
			{
				throw new ArgumentNullException();
			}
			ElementType etype = type.etype;
			if (etype <= ElementType.GenericInst)
			{
				if (etype == ElementType.None)
				{
					this.WriteElementType(type.IsValueType ? ElementType.ValueType : ElementType.Class);
					base.WriteCompressedUInt32(this.MakeTypeDefOrRefCodedRID(type));
					return;
				}
				switch (etype)
				{
				case ElementType.Ptr:
				case ElementType.ByRef:
					goto IL_00D9;
				case ElementType.ValueType:
				case ElementType.Class:
					goto IL_0173;
				case ElementType.Var:
					break;
				case ElementType.Array:
				{
					ArrayType arrayType = (ArrayType)type;
					if (!arrayType.IsVector)
					{
						this.WriteArrayTypeSignature(arrayType);
						return;
					}
					this.WriteElementType(ElementType.SzArray);
					this.WriteTypeSignature(arrayType.ElementType);
					return;
				}
				case ElementType.GenericInst:
				{
					GenericInstanceType genericInstanceType = (GenericInstanceType)type;
					this.WriteElementType(ElementType.GenericInst);
					this.WriteElementType(genericInstanceType.IsValueType ? ElementType.ValueType : ElementType.Class);
					base.WriteCompressedUInt32(this.MakeTypeDefOrRefCodedRID(genericInstanceType.ElementType));
					this.WriteGenericInstanceSignature(genericInstanceType);
					return;
				}
				default:
					goto IL_0173;
				}
			}
			else
			{
				switch (etype)
				{
				case ElementType.FnPtr:
				{
					FunctionPointerType functionPointerType = (FunctionPointerType)type;
					this.WriteElementType(ElementType.FnPtr);
					this.WriteMethodSignature(functionPointerType);
					return;
				}
				case ElementType.Object:
				case ElementType.SzArray:
					goto IL_0173;
				case ElementType.MVar:
					break;
				case ElementType.CModReqD:
				case ElementType.CModOpt:
				{
					IModifierType modifierType = (IModifierType)type;
					this.WriteModifierSignature(etype, modifierType);
					return;
				}
				default:
					if (etype != ElementType.Sentinel && etype != ElementType.Pinned)
					{
						goto IL_0173;
					}
					goto IL_00D9;
				}
			}
			GenericParameter genericParameter = (GenericParameter)type;
			this.WriteElementType(etype);
			int position = genericParameter.Position;
			if (position == -1)
			{
				throw new NotSupportedException();
			}
			base.WriteCompressedUInt32((uint)position);
			return;
			IL_00D9:
			TypeSpecification typeSpecification = (TypeSpecification)type;
			this.WriteElementType(etype);
			this.WriteTypeSignature(typeSpecification.ElementType);
			return;
			IL_0173:
			if (!this.TryWriteElementType(type))
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00016F14 File Offset: 0x00015114
		private void WriteArrayTypeSignature(ArrayType array)
		{
			this.WriteElementType(ElementType.Array);
			this.WriteTypeSignature(array.ElementType);
			Collection<ArrayDimension> dimensions = array.Dimensions;
			int count = dimensions.Count;
			base.WriteCompressedUInt32((uint)count);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				ArrayDimension arrayDimension = dimensions[i];
				if (arrayDimension.UpperBound != null)
				{
					num++;
					num2++;
				}
				else if (arrayDimension.LowerBound != null)
				{
					num2++;
				}
			}
			int[] array2 = new int[num];
			int[] array3 = new int[num2];
			for (int j = 0; j < num2; j++)
			{
				ArrayDimension arrayDimension2 = dimensions[j];
				array3[j] = arrayDimension2.LowerBound.GetValueOrDefault();
				if (arrayDimension2.UpperBound != null)
				{
					array2[j] = arrayDimension2.UpperBound.Value - array3[j] + 1;
				}
			}
			base.WriteCompressedUInt32((uint)num);
			for (int k = 0; k < num; k++)
			{
				base.WriteCompressedUInt32((uint)array2[k]);
			}
			base.WriteCompressedUInt32((uint)num2);
			for (int l = 0; l < num2; l++)
			{
				base.WriteCompressedInt32(array3[l]);
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001704C File Offset: 0x0001524C
		public void WriteGenericInstanceSignature(IGenericInstance instance)
		{
			Collection<TypeReference> genericArguments = instance.GenericArguments;
			int count = genericArguments.Count;
			base.WriteCompressedUInt32((uint)count);
			for (int i = 0; i < count; i++)
			{
				this.WriteTypeSignature(genericArguments[i]);
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00017087 File Offset: 0x00015287
		private void WriteModifierSignature(ElementType element_type, IModifierType type)
		{
			this.WriteElementType(element_type);
			base.WriteCompressedUInt32(this.MakeTypeDefOrRefCodedRID(type.ModifierType));
			this.WriteTypeSignature(type.ElementType);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000170B0 File Offset: 0x000152B0
		private bool TryWriteElementType(TypeReference type)
		{
			ElementType etype = type.etype;
			if (etype == ElementType.None)
			{
				return false;
			}
			this.WriteElementType(etype);
			return true;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000170D1 File Offset: 0x000152D1
		public void WriteConstantString(string value)
		{
			if (value != null)
			{
				base.WriteBytes(Encoding.Unicode.GetBytes(value));
				return;
			}
			base.WriteByte(byte.MaxValue);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000170F3 File Offset: 0x000152F3
		public void WriteConstantPrimitive(object value)
		{
			this.WritePrimitiveValue(value);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000170FC File Offset: 0x000152FC
		public void WriteCustomAttributeConstructorArguments(CustomAttribute attribute)
		{
			if (attribute.HasConstructorArguments)
			{
				Collection<CustomAttributeArgument> constructorArguments = attribute.ConstructorArguments;
				Collection<ParameterDefinition> parameters = attribute.Constructor.Parameters;
				if (parameters.Count != constructorArguments.Count)
				{
					throw new InvalidOperationException();
				}
				for (int i = 0; i < constructorArguments.Count; i++)
				{
					this.WriteCustomAttributeFixedArgument(parameters[i].ParameterType, constructorArguments[i]);
				}
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00017162 File Offset: 0x00015362
		private void WriteCustomAttributeFixedArgument(TypeReference type, CustomAttributeArgument argument)
		{
			if (type.IsArray)
			{
				this.WriteCustomAttributeFixedArrayArgument((ArrayType)type, argument);
				return;
			}
			this.WriteCustomAttributeElement(type, argument);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00017184 File Offset: 0x00015384
		private void WriteCustomAttributeFixedArrayArgument(ArrayType type, CustomAttributeArgument argument)
		{
			CustomAttributeArgument[] array = argument.Value as CustomAttributeArgument[];
			if (array == null)
			{
				base.WriteUInt32(uint.MaxValue);
				return;
			}
			base.WriteInt32(array.Length);
			if (array.Length != 0)
			{
				TypeReference elementType = type.ElementType;
				for (int i = 0; i < array.Length; i++)
				{
					this.WriteCustomAttributeElement(elementType, array[i]);
				}
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000171DC File Offset: 0x000153DC
		private void WriteCustomAttributeElement(TypeReference type, CustomAttributeArgument argument)
		{
			if (type.IsArray)
			{
				this.WriteCustomAttributeFixedArrayArgument((ArrayType)type, argument);
				return;
			}
			if (type.etype == ElementType.Object)
			{
				argument = (CustomAttributeArgument)argument.Value;
				type = argument.Type;
				this.WriteCustomAttributeFieldOrPropType(type);
				this.WriteCustomAttributeElement(type, argument);
				return;
			}
			this.WriteCustomAttributeValue(type, argument.Value);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00017240 File Offset: 0x00015440
		private void WriteCustomAttributeValue(TypeReference type, object value)
		{
			ElementType etype = type.etype;
			if (etype != ElementType.None)
			{
				if (etype != ElementType.String)
				{
					this.WritePrimitiveValue(value);
					return;
				}
				string text = (string)value;
				if (text == null)
				{
					base.WriteByte(byte.MaxValue);
					return;
				}
				this.WriteUTF8String(text);
				return;
			}
			else
			{
				if (type.IsTypeOf("System", "Type"))
				{
					this.WriteCustomAttributeTypeValue((TypeReference)value);
					return;
				}
				this.WriteCustomAttributeEnumValue(type, value);
				return;
			}
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000172AC File Offset: 0x000154AC
		private void WriteCustomAttributeTypeValue(TypeReference value)
		{
			TypeDefinition typeDefinition;
			if ((typeDefinition = value as TypeDefinition) != null)
			{
				while (typeDefinition.DeclaringType != null)
				{
					typeDefinition = typeDefinition.DeclaringType;
				}
				if (WindowsRuntimeProjections.IsClrImplementationType(typeDefinition))
				{
					WindowsRuntimeProjections.Project(typeDefinition);
					this.WriteTypeReference(value);
					WindowsRuntimeProjections.RemoveProjection(typeDefinition);
					return;
				}
			}
			this.WriteTypeReference(value);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000172F8 File Offset: 0x000154F8
		private void WritePrimitiveValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			switch (value.GetType().GetTypeCode())
			{
			case 3:
				base.WriteByte(((bool)value) ? 1 : 0);
				return;
			case 4:
				base.WriteInt16((short)((char)value));
				return;
			case 5:
				base.WriteSByte((sbyte)value);
				return;
			case 6:
				base.WriteByte((byte)value);
				return;
			case 7:
				base.WriteInt16((short)value);
				return;
			case 8:
				base.WriteUInt16((ushort)value);
				return;
			case 9:
				base.WriteInt32((int)value);
				return;
			case 10:
				base.WriteUInt32((uint)value);
				return;
			case 11:
				base.WriteInt64((long)value);
				return;
			case 12:
				base.WriteUInt64((ulong)value);
				return;
			case 13:
				base.WriteSingle((float)value);
				return;
			case 14:
				base.WriteDouble((double)value);
				return;
			default:
				throw new NotSupportedException(value.GetType().FullName);
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001740C File Offset: 0x0001560C
		private void WriteCustomAttributeEnumValue(TypeReference enum_type, object value)
		{
			TypeDefinition typeDefinition = enum_type.CheckedResolve();
			if (!typeDefinition.IsEnum)
			{
				throw new ArgumentException();
			}
			this.WriteCustomAttributeValue(typeDefinition.GetEnumUnderlyingType(), value);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001743C File Offset: 0x0001563C
		private void WriteCustomAttributeFieldOrPropType(TypeReference type)
		{
			if (type.IsArray)
			{
				ArrayType arrayType = (ArrayType)type;
				this.WriteElementType(ElementType.SzArray);
				this.WriteCustomAttributeFieldOrPropType(arrayType.ElementType);
				return;
			}
			ElementType etype = type.etype;
			if (etype != ElementType.None)
			{
				if (etype == ElementType.Object)
				{
					this.WriteElementType(ElementType.Boxed);
					return;
				}
				this.WriteElementType(etype);
				return;
			}
			else
			{
				if (type.IsTypeOf("System", "Type"))
				{
					this.WriteElementType(ElementType.Type);
					return;
				}
				this.WriteElementType(ElementType.Enum);
				this.WriteTypeReference(type);
				return;
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x000174B8 File Offset: 0x000156B8
		public void WriteCustomAttributeNamedArguments(CustomAttribute attribute)
		{
			int namedArgumentCount = SignatureWriter.GetNamedArgumentCount(attribute);
			base.WriteUInt16((ushort)namedArgumentCount);
			if (namedArgumentCount != 0)
			{
				this.WriteICustomAttributeNamedArguments(attribute);
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000174E0 File Offset: 0x000156E0
		private static int GetNamedArgumentCount(ICustomAttribute attribute)
		{
			int num = 0;
			if (attribute.HasFields)
			{
				num += attribute.Fields.Count;
			}
			if (attribute.HasProperties)
			{
				num += attribute.Properties.Count;
			}
			return num;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001751C File Offset: 0x0001571C
		private void WriteICustomAttributeNamedArguments(ICustomAttribute attribute)
		{
			if (attribute.HasFields)
			{
				this.WriteCustomAttributeNamedArguments(83, attribute.Fields);
			}
			if (attribute.HasProperties)
			{
				this.WriteCustomAttributeNamedArguments(84, attribute.Properties);
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001754C File Offset: 0x0001574C
		private void WriteCustomAttributeNamedArguments(byte kind, Collection<CustomAttributeNamedArgument> named_arguments)
		{
			for (int i = 0; i < named_arguments.Count; i++)
			{
				this.WriteCustomAttributeNamedArgument(kind, named_arguments[i]);
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00017578 File Offset: 0x00015778
		private void WriteCustomAttributeNamedArgument(byte kind, CustomAttributeNamedArgument named_argument)
		{
			CustomAttributeArgument argument = named_argument.Argument;
			base.WriteByte(kind);
			this.WriteCustomAttributeFieldOrPropType(argument.Type);
			this.WriteUTF8String(named_argument.Name);
			this.WriteCustomAttributeFixedArgument(argument.Type, argument);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000175BC File Offset: 0x000157BC
		private void WriteSecurityAttribute(SecurityAttribute attribute)
		{
			this.WriteTypeReference(attribute.AttributeType);
			int namedArgumentCount = SignatureWriter.GetNamedArgumentCount(attribute);
			if (namedArgumentCount == 0)
			{
				base.WriteCompressedUInt32(1U);
				base.WriteCompressedUInt32(0U);
				return;
			}
			SignatureWriter signatureWriter = new SignatureWriter(this.metadata);
			signatureWriter.WriteCompressedUInt32((uint)namedArgumentCount);
			signatureWriter.WriteICustomAttributeNamedArguments(attribute);
			base.WriteCompressedUInt32((uint)signatureWriter.length);
			base.WriteBytes(signatureWriter);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001761C File Offset: 0x0001581C
		public void WriteSecurityDeclaration(SecurityDeclaration declaration)
		{
			base.WriteByte(46);
			Collection<SecurityAttribute> security_attributes = declaration.security_attributes;
			if (security_attributes == null)
			{
				throw new NotSupportedException();
			}
			base.WriteCompressedUInt32((uint)security_attributes.Count);
			for (int i = 0; i < security_attributes.Count; i++)
			{
				this.WriteSecurityAttribute(security_attributes[i]);
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001766C File Offset: 0x0001586C
		public void WriteXmlSecurityDeclaration(SecurityDeclaration declaration)
		{
			string xmlSecurityDeclaration = SignatureWriter.GetXmlSecurityDeclaration(declaration);
			if (xmlSecurityDeclaration == null)
			{
				throw new NotSupportedException();
			}
			base.WriteBytes(Encoding.Unicode.GetBytes(xmlSecurityDeclaration));
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001769C File Offset: 0x0001589C
		private static string GetXmlSecurityDeclaration(SecurityDeclaration declaration)
		{
			if (declaration.security_attributes == null || declaration.security_attributes.Count != 1)
			{
				return null;
			}
			SecurityAttribute securityAttribute = declaration.security_attributes[0];
			if (!securityAttribute.AttributeType.IsTypeOf("System.Security.Permissions", "PermissionSetAttribute"))
			{
				return null;
			}
			if (securityAttribute.properties == null || securityAttribute.properties.Count != 1)
			{
				return null;
			}
			CustomAttributeNamedArgument customAttributeNamedArgument = securityAttribute.properties[0];
			if (customAttributeNamedArgument.Name != "XML")
			{
				return null;
			}
			return (string)customAttributeNamedArgument.Argument.Value;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00017735 File Offset: 0x00015935
		private void WriteTypeReference(TypeReference type)
		{
			this.WriteUTF8String(TypeParser.ToParseable(type));
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00017744 File Offset: 0x00015944
		public void WriteMarshalInfo(MarshalInfo marshal_info)
		{
			this.WriteNativeType(marshal_info.native);
			NativeType native = marshal_info.native;
			if (native <= NativeType.SafeArray)
			{
				if (native != NativeType.FixedSysString)
				{
					if (native != NativeType.SafeArray)
					{
						return;
					}
					SafeArrayMarshalInfo safeArrayMarshalInfo = (SafeArrayMarshalInfo)marshal_info;
					if (safeArrayMarshalInfo.element_type != VariantType.None)
					{
						this.WriteVariantType(safeArrayMarshalInfo.element_type);
						return;
					}
				}
				else
				{
					FixedSysStringMarshalInfo fixedSysStringMarshalInfo = (FixedSysStringMarshalInfo)marshal_info;
					if (fixedSysStringMarshalInfo.size > -1)
					{
						base.WriteCompressedUInt32((uint)fixedSysStringMarshalInfo.size);
						return;
					}
				}
			}
			else if (native != NativeType.FixedArray)
			{
				if (native != NativeType.Array)
				{
					if (native != NativeType.CustomMarshaler)
					{
						return;
					}
					CustomMarshalInfo customMarshalInfo = (CustomMarshalInfo)marshal_info;
					this.WriteUTF8String((customMarshalInfo.guid != Guid.Empty) ? customMarshalInfo.guid.ToString() : string.Empty);
					this.WriteUTF8String(customMarshalInfo.unmanaged_type);
					this.WriteTypeReference(customMarshalInfo.managed_type);
					this.WriteUTF8String(customMarshalInfo.cookie);
				}
				else
				{
					ArrayMarshalInfo arrayMarshalInfo = (ArrayMarshalInfo)marshal_info;
					if (arrayMarshalInfo.element_type != NativeType.None)
					{
						this.WriteNativeType(arrayMarshalInfo.element_type);
					}
					if (arrayMarshalInfo.size_parameter_index > -1)
					{
						base.WriteCompressedUInt32((uint)arrayMarshalInfo.size_parameter_index);
					}
					if (arrayMarshalInfo.size > -1)
					{
						base.WriteCompressedUInt32((uint)arrayMarshalInfo.size);
					}
					if (arrayMarshalInfo.size_parameter_multiplier > -1)
					{
						base.WriteCompressedUInt32((uint)arrayMarshalInfo.size_parameter_multiplier);
						return;
					}
				}
			}
			else
			{
				FixedArrayMarshalInfo fixedArrayMarshalInfo = (FixedArrayMarshalInfo)marshal_info;
				if (fixedArrayMarshalInfo.size > -1)
				{
					base.WriteCompressedUInt32((uint)fixedArrayMarshalInfo.size);
				}
				if (fixedArrayMarshalInfo.element_type != NativeType.None)
				{
					this.WriteNativeType(fixedArrayMarshalInfo.element_type);
					return;
				}
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x000178C3 File Offset: 0x00015AC3
		private void WriteNativeType(NativeType native)
		{
			base.WriteByte((byte)native);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000178C3 File Offset: 0x00015AC3
		private void WriteVariantType(VariantType variant)
		{
			base.WriteByte((byte)variant);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x000178D0 File Offset: 0x00015AD0
		public void WriteSequencePoints(MethodDebugInformation info)
		{
			int num = -1;
			int num2 = -1;
			base.WriteCompressedUInt32(info.local_var_token.RID);
			Document document;
			if (!info.TryGetUniqueDocument(out document))
			{
				document = null;
			}
			for (int i = 0; i < info.SequencePoints.Count; i++)
			{
				SequencePoint sequencePoint = info.SequencePoints[i];
				Document document2 = sequencePoint.Document;
				if (document != document2)
				{
					MetadataToken documentToken = this.metadata.GetDocumentToken(document2);
					if (document != null)
					{
						base.WriteCompressedUInt32(0U);
					}
					base.WriteCompressedUInt32(documentToken.RID);
					document = document2;
				}
				if (i > 0)
				{
					base.WriteCompressedUInt32((uint)(sequencePoint.Offset - info.SequencePoints[i - 1].Offset));
				}
				else
				{
					base.WriteCompressedUInt32((uint)sequencePoint.Offset);
				}
				if (sequencePoint.IsHidden)
				{
					base.WriteInt16(0);
				}
				else
				{
					int num3 = sequencePoint.EndLine - sequencePoint.StartLine;
					int num4 = sequencePoint.EndColumn - sequencePoint.StartColumn;
					base.WriteCompressedUInt32((uint)num3);
					if (num3 == 0)
					{
						base.WriteCompressedUInt32((uint)num4);
					}
					else
					{
						base.WriteCompressedInt32(num4);
					}
					if (num < 0)
					{
						base.WriteCompressedUInt32((uint)sequencePoint.StartLine);
						base.WriteCompressedUInt32((uint)sequencePoint.StartColumn);
					}
					else
					{
						base.WriteCompressedInt32(sequencePoint.StartLine - num);
						base.WriteCompressedInt32(sequencePoint.StartColumn - num2);
					}
					num = sequencePoint.StartLine;
					num2 = sequencePoint.StartColumn;
				}
			}
		}

		// Token: 0x04000277 RID: 631
		private readonly MetadataBuilder metadata;
	}
}
