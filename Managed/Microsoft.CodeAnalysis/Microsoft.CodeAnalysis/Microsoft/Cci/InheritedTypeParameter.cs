using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.Cci
{
	// Token: 0x02000090 RID: 144
	[NullableContext(2)]
	[Nullable(0)]
	internal class InheritedTypeParameter : IGenericTypeParameter, IGenericParameter, IGenericParameterReference, ITypeReference, IReference, INamedEntity, IParameterListEntry, IGenericTypeParameterReference
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x0000DA70 File Offset: 0x0000BC70
		[NullableContext(1)]
		internal InheritedTypeParameter(ushort index, ITypeDefinition inheritingType, IGenericTypeParameter parentParameter)
		{
			this._index = index;
			this._inheritingType = inheritingType;
			this._parentParameter = parentParameter;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000DA8D File Offset: 0x0000BC8D
		[Nullable(1)]
		public ITypeDefinition DefiningType
		{
			[NullableContext(1)]
			get
			{
				return this._inheritingType;
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000DA95 File Offset: 0x0000BC95
		[NullableContext(1)]
		public IEnumerable<TypeReferenceWithAttributes> GetConstraints(EmitContext context)
		{
			return this._parentParameter.GetConstraints(context);
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000DAA3 File Offset: 0x0000BCA3
		public bool MustBeReferenceType
		{
			get
			{
				return this._parentParameter.MustBeReferenceType;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000DAB0 File Offset: 0x0000BCB0
		public bool MustBeValueType
		{
			get
			{
				return this._parentParameter.MustBeValueType;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000DABD File Offset: 0x0000BCBD
		public bool MustHaveDefaultConstructor
		{
			get
			{
				return this._parentParameter.MustHaveDefaultConstructor;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000DACA File Offset: 0x0000BCCA
		public TypeParameterVariance Variance
		{
			get
			{
				if (!this._inheritingType.IsInterface && !this._inheritingType.IsDelegate)
				{
					return TypeParameterVariance.NonVariant;
				}
				return this._parentParameter.Variance;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000DAF3 File Offset: 0x0000BCF3
		public ushort Alignment
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000DAF6 File Offset: 0x0000BCF6
		public bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000DAF9 File Offset: 0x0000BCF9
		public bool IsEnum
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000DAFC File Offset: 0x0000BCFC
		public IArrayTypeReference AsArrayTypeReference
		{
			get
			{
				return this as IArrayTypeReference;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000DB04 File Offset: 0x0000BD04
		public IGenericMethodParameter AsGenericMethodParameter
		{
			get
			{
				return this as IGenericMethodParameter;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000DB0C File Offset: 0x0000BD0C
		public IGenericMethodParameterReference AsGenericMethodParameterReference
		{
			get
			{
				return this as IGenericMethodParameterReference;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000DB14 File Offset: 0x0000BD14
		public IGenericTypeInstanceReference AsGenericTypeInstanceReference
		{
			get
			{
				return this as IGenericTypeInstanceReference;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000DB1C File Offset: 0x0000BD1C
		public IGenericTypeParameter AsGenericTypeParameter
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000DB1F File Offset: 0x0000BD1F
		public IGenericTypeParameterReference AsGenericTypeParameterReference
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000DB22 File Offset: 0x0000BD22
		public INamespaceTypeDefinition AsNamespaceTypeDefinition(EmitContext context)
		{
			return this as INamespaceTypeDefinition;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000DB2A File Offset: 0x0000BD2A
		public INamespaceTypeReference AsNamespaceTypeReference
		{
			get
			{
				return this as INamespaceTypeReference;
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000DB32 File Offset: 0x0000BD32
		public INestedTypeDefinition AsNestedTypeDefinition(EmitContext context)
		{
			return this as INestedTypeDefinition;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000DB3A File Offset: 0x0000BD3A
		public INestedTypeReference AsNestedTypeReference
		{
			get
			{
				return this as INestedTypeReference;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000DB42 File Offset: 0x0000BD42
		public ISpecializedNestedTypeReference AsSpecializedNestedTypeReference
		{
			get
			{
				return this as ISpecializedNestedTypeReference;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000DB4A File Offset: 0x0000BD4A
		public IModifiedTypeReference AsModifiedTypeReference
		{
			get
			{
				return this as IModifiedTypeReference;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000DB52 File Offset: 0x0000BD52
		public IPointerTypeReference AsPointerTypeReference
		{
			get
			{
				return this as IPointerTypeReference;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000DB5A File Offset: 0x0000BD5A
		public ITypeDefinition AsTypeDefinition(EmitContext context)
		{
			return this as ITypeDefinition;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000DB62 File Offset: 0x0000BD62
		public IDefinition AsDefinition(EmitContext context)
		{
			return this as IDefinition;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000DB6A File Offset: 0x0000BD6A
		ISymbolInternal IReference.GetInternalSymbol()
		{
			return null;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000DB6D File Offset: 0x0000BD6D
		[NullableContext(1)]
		public IEnumerable<ICustomAttribute> GetAttributes(EmitContext context)
		{
			return this._parentParameter.GetAttributes(context);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000DB7B File Offset: 0x0000BD7B
		[NullableContext(1)]
		public void Dispatch(MetadataVisitor visitor)
		{
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000DB80 File Offset: 0x0000BD80
		public TypeDefinitionHandle TypeDef
		{
			get
			{
				return default(TypeDefinitionHandle);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000DB96 File Offset: 0x0000BD96
		public bool IsAlias
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000DB99 File Offset: 0x0000BD99
		public bool IsValueType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000DB9C File Offset: 0x0000BD9C
		[NullableContext(1)]
		public ITypeDefinition GetResolvedType(EmitContext context)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000DBA3 File Offset: 0x0000BDA3
		public PrimitiveTypeCode TypeCode
		{
			get
			{
				return PrimitiveTypeCode.NotPrimitive;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000DBA7 File Offset: 0x0000BDA7
		public ushort Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000DBAF File Offset: 0x0000BDAF
		public string Name
		{
			get
			{
				return this._parentParameter.Name;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000DBBC File Offset: 0x0000BDBC
		[Nullable(1)]
		ITypeReference IGenericTypeParameterReference.DefiningType
		{
			[NullableContext(1)]
			get
			{
				return this._inheritingType;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000DBC4 File Offset: 0x0000BDC4
		public bool MangleName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000DBC7 File Offset: 0x0000BDC7
		public bool IsNested
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000DBCE File Offset: 0x0000BDCE
		public bool IsSpecializedNested
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000DBD5 File Offset: 0x0000BDD5
		[Nullable(1)]
		public ITypeReference UnspecializedVersion
		{
			[NullableContext(1)]
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000DBDC File Offset: 0x0000BDDC
		public bool IsNamespaceTypeReference
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000DBE3 File Offset: 0x0000BDE3
		public bool IsGenericTypeInstance
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000DBEA File Offset: 0x0000BDEA
		public sealed override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000DBF1 File Offset: 0x0000BDF1
		public sealed override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04000190 RID: 400
		private readonly ushort _index;

		// Token: 0x04000191 RID: 401
		[Nullable(1)]
		private readonly ITypeDefinition _inheritingType;

		// Token: 0x04000192 RID: 402
		[Nullable(1)]
		private readonly IGenericTypeParameter _parentParameter;
	}
}
