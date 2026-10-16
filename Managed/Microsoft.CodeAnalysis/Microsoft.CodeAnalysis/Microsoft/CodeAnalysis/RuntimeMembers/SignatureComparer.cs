using System;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.RuntimeMembers
{
	// Token: 0x020002CA RID: 714
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class SignatureComparer<MethodSymbol, FieldSymbol, PropertySymbol, TypeSymbol, ParameterSymbol> where MethodSymbol : class where FieldSymbol : class where PropertySymbol : class where TypeSymbol : class where ParameterSymbol : class
	{
		// Token: 0x06001DD4 RID: 7636 RVA: 0x00056838 File Offset: 0x00054A38
		[NullableContext(0)]
		public bool MatchFieldSignature([Nullable(1)] FieldSymbol field, ImmutableArray<byte> signature)
		{
			int num = 0;
			return this.MatchType(this.GetFieldType(field), signature, ref num);
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00056858 File Offset: 0x00054A58
		[NullableContext(0)]
		public bool MatchPropertySignature([Nullable(1)] PropertySymbol property, ImmutableArray<byte> signature)
		{
			int num = 0;
			int num2 = (int)signature[num++];
			ImmutableArray<ParameterSymbol> parameters = this.GetParameters(property);
			if (num2 != parameters.Length)
			{
				return false;
			}
			bool flag = SignatureComparer<MethodSymbol, FieldSymbol, PropertySymbol, TypeSymbol, ParameterSymbol>.IsByRef(signature, ref num);
			if (this.IsByRefProperty(property) != flag)
			{
				return false;
			}
			if (!this.MatchType(this.GetPropertyType(property), signature, ref num))
			{
				return false;
			}
			foreach (ParameterSymbol parameterSymbol in parameters)
			{
				if (!this.MatchParameter(parameterSymbol, signature, ref num))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x000568DC File Offset: 0x00054ADC
		[NullableContext(0)]
		public bool MatchMethodSignature([Nullable(1)] MethodSymbol method, ImmutableArray<byte> signature)
		{
			int num = 0;
			int num2 = (int)signature[num++];
			ImmutableArray<ParameterSymbol> parameters = this.GetParameters(method);
			if (num2 != parameters.Length)
			{
				return false;
			}
			bool flag = SignatureComparer<MethodSymbol, FieldSymbol, PropertySymbol, TypeSymbol, ParameterSymbol>.IsByRef(signature, ref num);
			if (this.IsByRefMethod(method) != flag)
			{
				return false;
			}
			if (!this.MatchType(this.GetReturnType(method), signature, ref num))
			{
				return false;
			}
			foreach (ParameterSymbol parameterSymbol in parameters)
			{
				if (!this.MatchParameter(parameterSymbol, signature, ref num))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x00056960 File Offset: 0x00054B60
		[NullableContext(0)]
		private bool MatchParameter([Nullable(1)] ParameterSymbol parameter, ImmutableArray<byte> signature, ref int position)
		{
			bool flag = SignatureComparer<MethodSymbol, FieldSymbol, PropertySymbol, TypeSymbol, ParameterSymbol>.IsByRef(signature, ref position);
			return this.IsByRefParam(parameter) == flag && this.MatchType(this.GetParamType(parameter), signature, ref position);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x00056990 File Offset: 0x00054B90
		[NullableContext(0)]
		private static bool IsByRef(ImmutableArray<byte> signature, ref int position)
		{
			if (signature[position] == 16)
			{
				position++;
				return true;
			}
			return false;
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x000569A8 File Offset: 0x00054BA8
		[NullableContext(0)]
		private bool MatchType([Nullable(2)] TypeSymbol type, ImmutableArray<byte> signature, ref int position)
		{
			if (type == null)
			{
				return false;
			}
			int num = position;
			position = num + 1;
			SignatureTypeCode signatureTypeCode = (SignatureTypeCode)signature[num];
			if (signatureTypeCode <= SignatureTypeCode.SZArray)
			{
				switch (signatureTypeCode)
				{
				case SignatureTypeCode.Pointer:
					return this.MatchType(this.GetPointedToType(type), signature, ref position);
				case SignatureTypeCode.ByReference:
				case (SignatureTypeCode)17:
				case (SignatureTypeCode)18:
					break;
				case SignatureTypeCode.GenericTypeParameter:
				{
					num = position;
					position = num + 1;
					int num2 = (int)signature[num];
					return this.IsGenericTypeParam(type, num2);
				}
				case SignatureTypeCode.Array:
				{
					if (!this.MatchType(this.GetMDArrayElementType(type), signature, ref position))
					{
						return false;
					}
					num = position;
					position = num + 1;
					int num3 = (int)signature[num];
					return this.MatchArrayRank(type, num3);
				}
				case SignatureTypeCode.GenericTypeInstance:
				{
					if (!this.MatchType(this.GetGenericTypeDefinition(type), signature, ref position))
					{
						return false;
					}
					num = position;
					position = num + 1;
					int num4 = (int)signature[num];
					for (int i = 0; i < num4; i++)
					{
						if (!this.MatchType(this.GetGenericTypeArgument(type, i), signature, ref position))
						{
							return false;
						}
					}
					return true;
				}
				default:
					if (signatureTypeCode == SignatureTypeCode.SZArray)
					{
						return this.MatchType(this.GetSZArrayElementType(type), signature, ref position);
					}
					break;
				}
			}
			else
			{
				if (signatureTypeCode == SignatureTypeCode.GenericMethodParameter)
				{
					num = position;
					position = num + 1;
					int num2 = (int)signature[num];
					return this.IsGenericMethodTypeParam(type, num2);
				}
				if (signatureTypeCode == SignatureTypeCode.TypeHandle)
				{
					short num5 = SignatureComparer<MethodSymbol, FieldSymbol, PropertySymbol, TypeSymbol, ParameterSymbol>.ReadTypeId(signature, ref position);
					return this.MatchTypeToTypeId(type, (int)num5);
				}
			}
			throw ExceptionUtilities.UnexpectedValue(signatureTypeCode);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x00056B00 File Offset: 0x00054D00
		[NullableContext(0)]
		private static short ReadTypeId(ImmutableArray<byte> signature, ref int position)
		{
			int num = position;
			position = num + 1;
			byte b = signature[num];
			if (b == 255)
			{
				num = position;
				position = num + 1;
				return (short)(signature[num] + byte.MaxValue);
			}
			return (short)b;
		}

		// Token: 0x06001DDB RID: 7643
		[return: Nullable(2)]
		protected abstract TypeSymbol GetGenericTypeArgument(TypeSymbol type, int argumentIndex);

		// Token: 0x06001DDC RID: 7644
		[return: Nullable(2)]
		protected abstract TypeSymbol GetGenericTypeDefinition(TypeSymbol type);

		// Token: 0x06001DDD RID: 7645
		protected abstract bool IsGenericMethodTypeParam(TypeSymbol type, int paramPosition);

		// Token: 0x06001DDE RID: 7646
		protected abstract bool IsGenericTypeParam(TypeSymbol type, int paramPosition);

		// Token: 0x06001DDF RID: 7647
		[return: Nullable(2)]
		protected abstract TypeSymbol GetPointedToType(TypeSymbol type);

		// Token: 0x06001DE0 RID: 7648
		[return: Nullable(2)]
		protected abstract TypeSymbol GetSZArrayElementType(TypeSymbol type);

		// Token: 0x06001DE1 RID: 7649
		protected abstract bool MatchArrayRank(TypeSymbol type, int countOfDimensions);

		// Token: 0x06001DE2 RID: 7650
		[return: Nullable(2)]
		protected abstract TypeSymbol GetMDArrayElementType(TypeSymbol type);

		// Token: 0x06001DE3 RID: 7651
		protected abstract bool MatchTypeToTypeId(TypeSymbol type, int typeId);

		// Token: 0x06001DE4 RID: 7652
		protected abstract TypeSymbol GetReturnType(MethodSymbol method);

		// Token: 0x06001DE5 RID: 7653
		[return: Nullable(new byte[] { 0, 1 })]
		protected abstract ImmutableArray<ParameterSymbol> GetParameters(MethodSymbol method);

		// Token: 0x06001DE6 RID: 7654
		protected abstract TypeSymbol GetPropertyType(PropertySymbol property);

		// Token: 0x06001DE7 RID: 7655
		[return: Nullable(new byte[] { 0, 1 })]
		protected abstract ImmutableArray<ParameterSymbol> GetParameters(PropertySymbol property);

		// Token: 0x06001DE8 RID: 7656
		protected abstract TypeSymbol GetParamType(ParameterSymbol parameter);

		// Token: 0x06001DE9 RID: 7657
		protected abstract bool IsByRefParam(ParameterSymbol parameter);

		// Token: 0x06001DEA RID: 7658
		protected abstract bool IsByRefMethod(MethodSymbol method);

		// Token: 0x06001DEB RID: 7659
		protected abstract bool IsByRefProperty(PropertySymbol property);

		// Token: 0x06001DEC RID: 7660
		protected abstract TypeSymbol GetFieldType(FieldSymbol field);
	}
}
