using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000227 RID: 551
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public static class TypedConstantExtensions
	{
		// Token: 0x06002836 RID: 10294 RVA: 0x000DAE98 File Offset: 0x000D9098
		public static string ToCSharpString(this TypedConstant constant)
		{
			if (constant.IsNull)
			{
				return "null";
			}
			if (constant.Kind == TypedConstantKind.Array)
			{
				return "{" + string.Join(", ", constant.Values.Select<TypedConstant, string>((TypedConstant v) => v.ToCSharpString())) + "}";
			}
			if (constant.Kind == TypedConstantKind.Type || constant.TypeInternal.SpecialType == SpecialType.System_Object)
			{
				return "typeof(" + constant.Value.ToString() + ")";
			}
			if (constant.Kind == TypedConstantKind.Enum)
			{
				return TypedConstantExtensions.DisplayEnumConstant(constant);
			}
			return SymbolDisplay.FormatPrimitive(constant.ValueInternal, true, false);
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x000DAF58 File Offset: 0x000D9158
		private static string DisplayEnumConstant(TypedConstant constant)
		{
			SpecialType specialType = ((INamedTypeSymbol)constant.Type).EnumUnderlyingType.SpecialType;
			ConstantValue constantValue = ConstantValue.Create(constant.ValueInternal, specialType);
			string text = constant.Type.ToDisplayString(SymbolDisplayFormat.QualifiedNameOnlyFormat);
			if (constantValue.IsUnsigned)
			{
				return TypedConstantExtensions.DisplayUnsignedEnumConstant(constant, specialType, constantValue.UInt64Value, text);
			}
			return TypedConstantExtensions.DisplaySignedEnumConstant(constant, specialType, constantValue.Int64Value, text);
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000DAFC4 File Offset: 0x000D91C4
		private static string DisplayUnsignedEnumConstant(TypedConstant constant, SpecialType specialType, ulong constantToDecode, string typeName)
		{
			ulong num = 0UL;
			PooledStringBuilder pooledStringBuilder = null;
			StringBuilder stringBuilder = null;
			foreach (ISymbol symbol in constant.Type.GetMembers())
			{
				IFieldSymbol fieldSymbol = symbol as IFieldSymbol;
				if (fieldSymbol != null && fieldSymbol.HasConstantValue)
				{
					ulong uint64Value = ConstantValue.Create(fieldSymbol.ConstantValue, specialType).UInt64Value;
					if (uint64Value == constantToDecode)
					{
						if (pooledStringBuilder != null)
						{
							pooledStringBuilder.Free();
						}
						return typeName + "." + fieldSymbol.Name;
					}
					if ((uint64Value & constantToDecode) == uint64Value)
					{
						num |= uint64Value;
						if (stringBuilder == null)
						{
							pooledStringBuilder = PooledStringBuilder.GetInstance();
							stringBuilder = pooledStringBuilder.Builder;
						}
						else
						{
							stringBuilder.Append(" | ");
						}
						stringBuilder.Append(typeName);
						stringBuilder.Append(".");
						stringBuilder.Append(fieldSymbol.Name);
					}
				}
			}
			if (pooledStringBuilder != null)
			{
				if (num == constantToDecode)
				{
					return pooledStringBuilder.ToStringAndFree();
				}
				pooledStringBuilder.Free();
			}
			return constant.ValueInternal.ToString();
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x000DB0C4 File Offset: 0x000D92C4
		private static string DisplaySignedEnumConstant(TypedConstant constant, SpecialType specialType, long constantToDecode, string typeName)
		{
			long num = 0L;
			PooledStringBuilder pooledStringBuilder = null;
			StringBuilder stringBuilder = null;
			foreach (ISymbol symbol in constant.Type.GetMembers())
			{
				IFieldSymbol fieldSymbol = symbol as IFieldSymbol;
				if (fieldSymbol != null && fieldSymbol.HasConstantValue)
				{
					long int64Value = ConstantValue.Create(fieldSymbol.ConstantValue, specialType).Int64Value;
					if (int64Value == constantToDecode)
					{
						if (pooledStringBuilder != null)
						{
							pooledStringBuilder.Free();
						}
						return typeName + "." + fieldSymbol.Name;
					}
					if ((int64Value & constantToDecode) == int64Value)
					{
						num |= int64Value;
						if (stringBuilder == null)
						{
							pooledStringBuilder = PooledStringBuilder.GetInstance();
							stringBuilder = pooledStringBuilder.Builder;
						}
						else
						{
							stringBuilder.Append(" | ");
						}
						stringBuilder.Append(typeName);
						stringBuilder.Append(".");
						stringBuilder.Append(fieldSymbol.Name);
					}
				}
			}
			if (pooledStringBuilder != null)
			{
				if (num == constantToDecode)
				{
					return pooledStringBuilder.ToStringAndFree();
				}
				pooledStringBuilder.Free();
			}
			return constant.ValueInternal.ToString();
		}
	}
}
