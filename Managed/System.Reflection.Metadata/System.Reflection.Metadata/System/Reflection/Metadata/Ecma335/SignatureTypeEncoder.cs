using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	/// <summary>Encodes a type in a signature.</summary>
	// Token: 0x02000100 RID: 256
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct SignatureTypeEncoder
	{
		/// <summary>The <see cref="T:System.Reflection.Metadata.BlobBuilder" /> where the signature is written to.</summary>
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0001ADC5 File Offset: 0x00018FC5
		public BlobBuilder Builder { get; }

		/// <summary>Creates a <see cref="T:System.Reflection.Metadata.Ecma335.SignatureTypeEncoder" />.</summary>
		/// <param name="builder">The <see cref="T:System.Reflection.Metadata.BlobBuilder" /> where the signature will be written.</param>
		// Token: 0x06000991 RID: 2449 RVA: 0x0001ADCD File Offset: 0x00018FCD
		public SignatureTypeEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001ADD6 File Offset: 0x00018FD6
		private void WriteTypeCode(SignatureTypeCode value)
		{
			this.Builder.WriteByte((byte)value);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0001ADE4 File Offset: 0x00018FE4
		private void ClassOrValue(bool isValueType)
		{
			this.Builder.WriteByte(isValueType ? 17 : 18);
		}

		/// <summary>Encodes <see cref="T:System.Boolean" />.</summary>
		// Token: 0x06000994 RID: 2452 RVA: 0x0001ADFA File Offset: 0x00018FFA
		public void Boolean()
		{
			this.WriteTypeCode(SignatureTypeCode.Boolean);
		}

		/// <summary>Encodes <see cref="T:System.Char" />.</summary>
		// Token: 0x06000995 RID: 2453 RVA: 0x0001AE03 File Offset: 0x00019003
		public void Char()
		{
			this.WriteTypeCode(SignatureTypeCode.Char);
		}

		/// <summary>Encodes <see cref="T:System.SByte" />.</summary>
		// Token: 0x06000996 RID: 2454 RVA: 0x0001AE0C File Offset: 0x0001900C
		public void SByte()
		{
			this.WriteTypeCode(SignatureTypeCode.SByte);
		}

		/// <summary>Encodes <see cref="T:System.Byte" />.</summary>
		// Token: 0x06000997 RID: 2455 RVA: 0x0001AE15 File Offset: 0x00019015
		public void Byte()
		{
			this.WriteTypeCode(SignatureTypeCode.Byte);
		}

		/// <summary>Encodes <see cref="T:System.Int16" />.</summary>
		// Token: 0x06000998 RID: 2456 RVA: 0x0001AE1E File Offset: 0x0001901E
		public void Int16()
		{
			this.WriteTypeCode(SignatureTypeCode.Int16);
		}

		/// <summary>Encodes <see cref="T:System.UInt16" />.</summary>
		// Token: 0x06000999 RID: 2457 RVA: 0x0001AE27 File Offset: 0x00019027
		public void UInt16()
		{
			this.WriteTypeCode(SignatureTypeCode.UInt16);
		}

		/// <summary>Encodes <see cref="T:System.Int32" />.</summary>
		// Token: 0x0600099A RID: 2458 RVA: 0x0001AE30 File Offset: 0x00019030
		public void Int32()
		{
			this.WriteTypeCode(SignatureTypeCode.Int32);
		}

		/// <summary>Encodes <see cref="T:System.UInt32" />.</summary>
		// Token: 0x0600099B RID: 2459 RVA: 0x0001AE39 File Offset: 0x00019039
		public void UInt32()
		{
			this.WriteTypeCode(SignatureTypeCode.UInt32);
		}

		/// <summary>Encodes <see cref="T:System.Int64" />.</summary>
		// Token: 0x0600099C RID: 2460 RVA: 0x0001AE43 File Offset: 0x00019043
		public void Int64()
		{
			this.WriteTypeCode(SignatureTypeCode.Int64);
		}

		/// <summary>Encodes <see cref="T:System.UInt64" />.</summary>
		// Token: 0x0600099D RID: 2461 RVA: 0x0001AE4D File Offset: 0x0001904D
		public void UInt64()
		{
			this.WriteTypeCode(SignatureTypeCode.UInt64);
		}

		/// <summary>Encodes <see cref="T:System.Single" />.</summary>
		// Token: 0x0600099E RID: 2462 RVA: 0x0001AE57 File Offset: 0x00019057
		public void Single()
		{
			this.WriteTypeCode(SignatureTypeCode.Single);
		}

		/// <summary>Encodes <see cref="T:System.Double" />.</summary>
		// Token: 0x0600099F RID: 2463 RVA: 0x0001AE61 File Offset: 0x00019061
		public void Double()
		{
			this.WriteTypeCode(SignatureTypeCode.Double);
		}

		/// <summary>Encodes <see cref="T:System.String" />.</summary>
		// Token: 0x060009A0 RID: 2464 RVA: 0x0001AE6B File Offset: 0x0001906B
		public void String()
		{
			this.WriteTypeCode(SignatureTypeCode.String);
		}

		/// <summary>Encodes <see cref="T:System.IntPtr" />.</summary>
		// Token: 0x060009A1 RID: 2465 RVA: 0x0001AE75 File Offset: 0x00019075
		public void IntPtr()
		{
			this.WriteTypeCode(SignatureTypeCode.IntPtr);
		}

		/// <summary>Encodes <see cref="T:System.UIntPtr" />.</summary>
		// Token: 0x060009A2 RID: 2466 RVA: 0x0001AE7F File Offset: 0x0001907F
		public void UIntPtr()
		{
			this.WriteTypeCode(SignatureTypeCode.UIntPtr);
		}

		/// <summary>Encodes <see cref="T:System.Object" />.</summary>
		// Token: 0x060009A3 RID: 2467 RVA: 0x0001AE89 File Offset: 0x00019089
		public void Object()
		{
			this.WriteTypeCode(SignatureTypeCode.Object);
		}

		/// <summary>Writes primitive type code.</summary>
		/// <param name="type">Any primitive type code except for <see cref="F:System.Reflection.Metadata.PrimitiveTypeCode.TypedReference" /> and <see cref="F:System.Reflection.Metadata.PrimitiveTypeCode.Void" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="type" /> is not valid in this context.</exception>
		// Token: 0x060009A4 RID: 2468 RVA: 0x0001AE94 File Offset: 0x00019094
		public void PrimitiveType(PrimitiveTypeCode type)
		{
			switch (type)
			{
			case PrimitiveTypeCode.Boolean:
			case PrimitiveTypeCode.Char:
			case PrimitiveTypeCode.SByte:
			case PrimitiveTypeCode.Byte:
			case PrimitiveTypeCode.Int16:
			case PrimitiveTypeCode.UInt16:
			case PrimitiveTypeCode.Int32:
			case PrimitiveTypeCode.UInt32:
			case PrimitiveTypeCode.Int64:
			case PrimitiveTypeCode.UInt64:
			case PrimitiveTypeCode.Single:
			case PrimitiveTypeCode.Double:
			case PrimitiveTypeCode.String:
			case PrimitiveTypeCode.IntPtr:
			case PrimitiveTypeCode.UIntPtr:
			case PrimitiveTypeCode.Object:
				this.Builder.WriteByte((byte)type);
				return;
			}
			Throw.ArgumentOutOfRange("type");
		}

		/// <summary>Encodes an array type. Returns a pair of encoders that must be used in the order they appear in the parameter list.</summary>
		/// <param name="elementType">Use first, to encode the type of the element.</param>
		/// <param name="arrayShape">Use second, to encode the shape of the array.</param>
		// Token: 0x060009A5 RID: 2469 RVA: 0x0001AF32 File Offset: 0x00019132
		public void Array(out SignatureTypeEncoder elementType, out ArrayShapeEncoder arrayShape)
		{
			this.Builder.WriteByte(20);
			elementType = this;
			arrayShape = new ArrayShapeEncoder(this.Builder);
		}

		/// <summary>Encodes an array type.</summary>
		/// <param name="elementType">Called first, to encode the type of the element.</param>
		/// <param name="arrayShape">Called second, to encode the shape of the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="elementType" /> or <paramref name="arrayShape" /> is <see langword="null" />.</exception>
		// Token: 0x060009A6 RID: 2470 RVA: 0x0001AF60 File Offset: 0x00019160
		public void Array(Action<SignatureTypeEncoder> elementType, Action<ArrayShapeEncoder> arrayShape)
		{
			if (elementType == null)
			{
				Throw.ArgumentNull("elementType");
			}
			if (arrayShape == null)
			{
				Throw.ArgumentNull("arrayShape");
			}
			SignatureTypeEncoder signatureTypeEncoder;
			ArrayShapeEncoder arrayShapeEncoder;
			this.Array(out signatureTypeEncoder, out arrayShapeEncoder);
			elementType.Invoke(signatureTypeEncoder);
			arrayShape.Invoke(arrayShapeEncoder);
		}

		/// <summary>Encodes a reference to a type.</summary>
		/// <param name="type">
		///   <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" /> or <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" />.</param>
		/// <param name="isValueType">
		///   <see langword="true" /> to mark the type as value type, <see langword="false" /> to mark it as a reference type in the signature.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> doesn't have the expected handle kind.</exception>
		// Token: 0x060009A7 RID: 2471 RVA: 0x0001AFA0 File Offset: 0x000191A0
		public void Type(EntityHandle type, bool isValueType)
		{
			int num = CodedIndex.TypeDefOrRef(type);
			this.ClassOrValue(isValueType);
			this.Builder.WriteCompressedInteger(num);
		}

		/// <summary>Starts a function pointer signature.</summary>
		/// <param name="convention">Calling convention.</param>
		/// <param name="attributes">Function pointer attributes.</param>
		/// <param name="genericParameterCount">Generic parameter count.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributes" /> is invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="genericParameterCount" /> is not in range [0, 0xffff].</exception>
		// Token: 0x060009A8 RID: 2472 RVA: 0x0001AFC8 File Offset: 0x000191C8
		public MethodSignatureEncoder FunctionPointer(SignatureCallingConvention convention = SignatureCallingConvention.Default, FunctionPointerAttributes attributes = FunctionPointerAttributes.None, int genericParameterCount = 0)
		{
			if (attributes != FunctionPointerAttributes.None && attributes != FunctionPointerAttributes.HasThis && attributes != FunctionPointerAttributes.HasExplicitThis)
			{
				throw new ArgumentException(SR.InvalidSignature, "attributes");
			}
			if (genericParameterCount > 65535)
			{
				Throw.ArgumentOutOfRange("genericParameterCount");
			}
			this.Builder.WriteByte(27);
			this.Builder.WriteByte(new SignatureHeader(SignatureKind.Method, convention, (SignatureAttributes)attributes).RawValue);
			if (genericParameterCount != 0)
			{
				this.Builder.WriteCompressedInteger(genericParameterCount);
			}
			return new MethodSignatureEncoder(this.Builder, convention == SignatureCallingConvention.VarArgs);
		}

		/// <summary>Starts a generic instantiation signature.</summary>
		/// <param name="genericType">
		///   <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" /> or <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" />.</param>
		/// <param name="genericArgumentCount">Generic argument count.</param>
		/// <param name="isValueType">
		///   <see langword="true" /> to mark the type as value type, <see langword="false" /> to mark it as a reference type in the signature.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="genericType" /> doesn't have the expected handle kind.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="genericArgumentCount" /> is not in range [1, 0xffff].</exception>
		// Token: 0x060009A9 RID: 2473 RVA: 0x0001B04C File Offset: 0x0001924C
		public GenericTypeArgumentsEncoder GenericInstantiation(EntityHandle genericType, int genericArgumentCount, bool isValueType)
		{
			if (genericArgumentCount - 1 > 65534)
			{
				Throw.ArgumentOutOfRange("genericArgumentCount");
			}
			int num = CodedIndex.TypeDefOrRef(genericType);
			this.Builder.WriteByte(21);
			this.ClassOrValue(isValueType);
			this.Builder.WriteCompressedInteger(num);
			this.Builder.WriteCompressedInteger(genericArgumentCount);
			return new GenericTypeArgumentsEncoder(this.Builder);
		}

		/// <summary>Encodes a reference to type parameter of a containing generic method.</summary>
		/// <param name="parameterIndex">Parameter index.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="parameterIndex" /> is not in range [0, 0xffff].</exception>
		// Token: 0x060009AA RID: 2474 RVA: 0x0001B0AB File Offset: 0x000192AB
		public void GenericMethodTypeParameter(int parameterIndex)
		{
			if (parameterIndex > 65535)
			{
				Throw.ArgumentOutOfRange("parameterIndex");
			}
			this.Builder.WriteByte(30);
			this.Builder.WriteCompressedInteger(parameterIndex);
		}

		/// <summary>Encodes a reference to type parameter of a containing generic type.</summary>
		/// <param name="parameterIndex">Parameter index.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="parameterIndex" /> is not in range [0, 0xffff].</exception>
		// Token: 0x060009AB RID: 2475 RVA: 0x0001B0D8 File Offset: 0x000192D8
		public void GenericTypeParameter(int parameterIndex)
		{
			if (parameterIndex > 65535)
			{
				Throw.ArgumentOutOfRange("parameterIndex");
			}
			this.Builder.WriteByte(19);
			this.Builder.WriteCompressedInteger(parameterIndex);
		}

		/// <summary>Starts pointer signature.</summary>
		// Token: 0x060009AC RID: 2476 RVA: 0x0001B105 File Offset: 0x00019305
		public SignatureTypeEncoder Pointer()
		{
			this.Builder.WriteByte(15);
			return this;
		}

		/// <summary>Encodes a void pointer (<c>void*</c>).</summary>
		// Token: 0x060009AD RID: 2477 RVA: 0x0001B11A File Offset: 0x0001931A
		public void VoidPointer()
		{
			this.Builder.WriteByte(15);
			this.Builder.WriteByte(1);
		}

		/// <summary>Starts SZ array (vector) signature.</summary>
		// Token: 0x060009AE RID: 2478 RVA: 0x0001B135 File Offset: 0x00019335
		public SignatureTypeEncoder SZArray()
		{
			this.Builder.WriteByte(29);
			return this;
		}

		/// <summary>Starts a signature of a type with custom modifiers.</summary>
		// Token: 0x060009AF RID: 2479 RVA: 0x0001B14A File Offset: 0x0001934A
		public CustomModifiersEncoder CustomModifiers()
		{
			return new CustomModifiersEncoder(this.Builder);
		}
	}
}
