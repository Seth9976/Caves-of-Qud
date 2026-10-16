using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000EE RID: 238
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct BlobEncoder
	{
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0001A47C File Offset: 0x0001867C
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x0600092F RID: 2351 RVA: 0x0001A484 File Offset: 0x00018684
		public BlobEncoder(BlobBuilder builder)
		{
			if (builder == null)
			{
				Throw.BuilderArgumentNull();
			}
			this.Builder = builder;
		}

		/// <summary>Encodes field signature blob.</summary>
		/// <returns>Encoder of the field type.</returns>
		// Token: 0x06000930 RID: 2352 RVA: 0x0001A495 File Offset: 0x00018695
		public SignatureTypeEncoder FieldSignature()
		{
			this.Builder.WriteByte(6);
			return new SignatureTypeEncoder(this.Builder);
		}

		/// <summary>Encodes method specification signature blob.</summary>
		/// <param name="genericArgumentCount">Number of generic arguments.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="genericArgumentCount" /> is not in range [0, 0xffff].</exception>
		/// <returns>Encoder of generic arguments.</returns>
		// Token: 0x06000931 RID: 2353 RVA: 0x0001A4AE File Offset: 0x000186AE
		public GenericTypeArgumentsEncoder MethodSpecificationSignature(int genericArgumentCount)
		{
			if (genericArgumentCount > 65535)
			{
				Throw.ArgumentOutOfRange("genericArgumentCount");
			}
			this.Builder.WriteByte(10);
			this.Builder.WriteCompressedInteger(genericArgumentCount);
			return new GenericTypeArgumentsEncoder(this.Builder);
		}

		/// <summary>Encodes method signature blob.</summary>
		/// <param name="convention">Calling convention.</param>
		/// <param name="genericParameterCount">Number of generic parameters.</param>
		/// <param name="isInstanceMethod">
		///   <see langword="true" /> to encode an instance method signature, <see langword="false" /> to encode a static method signature.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="genericParameterCount" /> is not in range [0, 0xffff].</exception>
		/// <returns>An encoder of the rest of the signature including return value and parameters.</returns>
		// Token: 0x06000932 RID: 2354 RVA: 0x0001A4E8 File Offset: 0x000186E8
		public MethodSignatureEncoder MethodSignature(SignatureCallingConvention convention = SignatureCallingConvention.Default, int genericParameterCount = 0, bool isInstanceMethod = false)
		{
			if (genericParameterCount > 65535)
			{
				Throw.ArgumentOutOfRange("genericParameterCount");
			}
			SignatureAttributes signatureAttributes = ((genericParameterCount != 0) ? SignatureAttributes.Generic : SignatureAttributes.None) | (isInstanceMethod ? SignatureAttributes.Instance : SignatureAttributes.None);
			this.Builder.WriteByte(new SignatureHeader(SignatureKind.Method, convention, signatureAttributes).RawValue);
			if (genericParameterCount != 0)
			{
				this.Builder.WriteCompressedInteger(genericParameterCount);
			}
			return new MethodSignatureEncoder(this.Builder, convention == SignatureCallingConvention.VarArgs);
		}

		/// <summary>Encodes property signature blob.</summary>
		/// <param name="isInstanceProperty">
		///   <see langword="true" /> to encode an instance property signature, <see langword="false" /> to encode a static property signature.</param>
		/// <returns>An encoder of the rest of the signature including return value and parameters, which has the same structure as method signature.</returns>
		// Token: 0x06000933 RID: 2355 RVA: 0x0001A554 File Offset: 0x00018754
		public MethodSignatureEncoder PropertySignature(bool isInstanceProperty = false)
		{
			this.Builder.WriteByte(new SignatureHeader(SignatureKind.Property, SignatureCallingConvention.Default, isInstanceProperty ? SignatureAttributes.Instance : SignatureAttributes.None).RawValue);
			return new MethodSignatureEncoder(this.Builder, false);
		}

		/// <summary>Encodes custom attribute signature blob.
		///  Returns a pair of encoders that must be used in the order they appear in the parameter list.</summary>
		/// <param name="fixedArguments">Use first, to encode fixed arguments.</param>
		/// <param name="namedArguments">Use second, to encode named arguments.</param>
		// Token: 0x06000934 RID: 2356 RVA: 0x0001A58F File Offset: 0x0001878F
		public void CustomAttributeSignature(out FixedArgumentsEncoder fixedArguments, out CustomAttributeNamedArgumentsEncoder namedArguments)
		{
			this.Builder.WriteUInt16(1);
			fixedArguments = new FixedArgumentsEncoder(this.Builder);
			namedArguments = new CustomAttributeNamedArgumentsEncoder(this.Builder);
		}

		/// <summary>Encodes custom attribute signature blob.</summary>
		/// <param name="fixedArguments">Called first, to encode fixed arguments.</param>
		/// <param name="namedArguments">Called second, to encode named arguments.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fixedArguments" /> or <paramref name="namedArguments" /> is <see langword="null" />.</exception>
		// Token: 0x06000935 RID: 2357 RVA: 0x0001A5C0 File Offset: 0x000187C0
		public void CustomAttributeSignature(Action<FixedArgumentsEncoder> fixedArguments, Action<CustomAttributeNamedArgumentsEncoder> namedArguments)
		{
			if (fixedArguments == null)
			{
				Throw.ArgumentNull("fixedArguments");
			}
			if (namedArguments == null)
			{
				Throw.ArgumentNull("namedArguments");
			}
			FixedArgumentsEncoder fixedArgumentsEncoder;
			CustomAttributeNamedArgumentsEncoder customAttributeNamedArgumentsEncoder;
			this.CustomAttributeSignature(out fixedArgumentsEncoder, out customAttributeNamedArgumentsEncoder);
			fixedArguments.Invoke(fixedArgumentsEncoder);
			namedArguments.Invoke(customAttributeNamedArgumentsEncoder);
		}

		/// <summary>Encodes local variable signature.</summary>
		/// <param name="variableCount">Number of local variables.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="variableCount" /> is not in range [0, 0x1fffffff].</exception>
		/// <returns>Encoder of a sequence of local variables.</returns>
		// Token: 0x06000936 RID: 2358 RVA: 0x0001A5FF File Offset: 0x000187FF
		public LocalVariablesEncoder LocalVariableSignature(int variableCount)
		{
			if (variableCount > 536870911)
			{
				Throw.ArgumentOutOfRange("variableCount");
			}
			this.Builder.WriteByte(7);
			this.Builder.WriteCompressedInteger(variableCount);
			return new LocalVariablesEncoder(this.Builder);
		}

		/// <summary>Encodes type specification signature.</summary>
		/// <returns>Type encoder of the structured type represented by the type specification (it shall not encode a primitive type).</returns>
		// Token: 0x06000937 RID: 2359 RVA: 0x0001A636 File Offset: 0x00018836
		public SignatureTypeEncoder TypeSpecificationSignature()
		{
			return new SignatureTypeEncoder(this.Builder);
		}

		/// <summary>Encodes a permission set blob.</summary>
		/// <param name="attributeCount">Number of attributes in the set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="attributeCount" /> is not in range [0, 0x1fffffff].</exception>
		/// <returns>Permission set encoder.</returns>
		// Token: 0x06000938 RID: 2360 RVA: 0x0001A643 File Offset: 0x00018843
		public PermissionSetEncoder PermissionSetBlob(int attributeCount)
		{
			if (attributeCount > 536870911)
			{
				Throw.ArgumentOutOfRange("attributeCount");
			}
			this.Builder.WriteByte(46);
			this.Builder.WriteCompressedInteger(attributeCount);
			return new PermissionSetEncoder(this.Builder);
		}

		/// <summary>Encodes permission set arguments.</summary>
		/// <param name="argumentCount">Number of arguments in the set.</param>
		/// <returns>Encoder of the arguments of the set.</returns>
		// Token: 0x06000939 RID: 2361 RVA: 0x0001A67B File Offset: 0x0001887B
		public NamedArgumentsEncoder PermissionSetArguments(int argumentCount)
		{
			if (argumentCount > 536870911)
			{
				Throw.ArgumentOutOfRange("argumentCount");
			}
			this.Builder.WriteCompressedInteger(argumentCount);
			return new NamedArgumentsEncoder(this.Builder);
		}
	}
}
