using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005A9 RID: 1449
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class TypeSymbolExtensions
	{
		// Token: 0x06007932 RID: 31026 RVA: 0x001BB934 File Offset: 0x001B9B34
		public static bool ImplementsInterface(this TypeSymbol subType, TypeSymbol superInterface, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			foreach (NamedTypeSymbol namedTypeSymbol in subType.AllInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo))
			{
				if (namedTypeSymbol.IsInterface && TypeSymbol.Equals(namedTypeSymbol, superInterface, TypeCompareKind.ConsiderEverything))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007933 RID: 31027 RVA: 0x001BB979 File Offset: 0x001B9B79
		public static bool CanBeAssignedNull(this TypeSymbol type)
		{
			return type.IsReferenceType || type.IsPointerOrFunctionPointer() || type.IsNullableType();
		}

		// Token: 0x06007934 RID: 31028 RVA: 0x001BB993 File Offset: 0x001B9B93
		public static bool CanContainNull(this TypeSymbol type)
		{
			return !type.IsValueType || type.IsNullableTypeOrTypeParameter();
		}

		// Token: 0x06007935 RID: 31029 RVA: 0x001BB9A5 File Offset: 0x001B9BA5
		public static bool CanBeConst(this TypeSymbol typeSymbol)
		{
			return typeSymbol.IsReferenceType || typeSymbol.IsEnumType() || typeSymbol.SpecialType.CanBeConst() || typeSymbol.IsNativeIntegerType;
		}

		// Token: 0x06007936 RID: 31030 RVA: 0x001BB9CC File Offset: 0x001B9BCC
		public static bool IsTypeParameterDisallowingAnnotationInCSharp8(this TypeSymbol type)
		{
			if (type.TypeKind != TypeKind.TypeParameter)
			{
				return false;
			}
			TypeParameterSymbol typeParameterSymbol = (TypeParameterSymbol)type;
			if (typeParameterSymbol.IsValueType)
			{
				return false;
			}
			if (typeParameterSymbol.IsReferenceType)
			{
				bool? isNotNullable = typeParameterSymbol.IsNotNullable;
				bool flag = true;
				return !((isNotNullable.GetValueOrDefault() == flag) & (isNotNullable != null));
			}
			return true;
		}

		// Token: 0x06007937 RID: 31031 RVA: 0x001BBA20 File Offset: 0x001B9C20
		public static bool IsPossiblyNullableReferenceTypeTypeParameter(this TypeSymbol type)
		{
			TypeParameterSymbol typeParameterSymbol = type as TypeParameterSymbol;
			if (typeParameterSymbol != null && !typeParameterSymbol.IsValueType)
			{
				bool? isNotNullable = typeParameterSymbol.IsNotNullable;
				if (isNotNullable != null)
				{
					return !isNotNullable.GetValueOrDefault();
				}
			}
			return false;
		}

		// Token: 0x06007938 RID: 31032 RVA: 0x001BBA5B File Offset: 0x001B9C5B
		public static bool IsNonNullableValueType(this TypeSymbol typeArgument)
		{
			return typeArgument.IsValueType && !typeArgument.IsNullableTypeOrTypeParameter();
		}

		// Token: 0x06007939 RID: 31033 RVA: 0x001BBA70 File Offset: 0x001B9C70
		public static bool IsVoidType(this TypeSymbol type)
		{
			return type.SpecialType == SpecialType.System_Void;
		}

		// Token: 0x0600793A RID: 31034 RVA: 0x001BBA7C File Offset: 0x001B9C7C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool IsNullableTypeOrTypeParameter(this TypeSymbol type)
		{
			if (type == null)
			{
				return false;
			}
			if (type.TypeKind == TypeKind.TypeParameter)
			{
				foreach (TypeWithAnnotations typeWithAnnotations in ((TypeParameterSymbol)type).ConstraintTypesNoUseSiteDiagnostics)
				{
					if (typeWithAnnotations.Type.IsNullableTypeOrTypeParameter())
					{
						return true;
					}
				}
				return false;
			}
			return type.IsNullableType();
		}

		// Token: 0x0600793B RID: 31035 RVA: 0x001BBAD7 File Offset: 0x001B9CD7
		public static bool IsNullableType(this TypeSymbol type)
		{
			return type.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T;
		}

		// Token: 0x0600793C RID: 31036 RVA: 0x001BBAE8 File Offset: 0x001B9CE8
		public static TypeSymbol GetNullableUnderlyingType(this TypeSymbol type)
		{
			return type.GetNullableUnderlyingTypeWithAnnotations().Type;
		}

		// Token: 0x0600793D RID: 31037 RVA: 0x001BBB04 File Offset: 0x001B9D04
		public static TypeWithAnnotations GetNullableUnderlyingTypeWithAnnotations(this TypeSymbol type)
		{
			return ((NamedTypeSymbol)type).TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[0];
		}

		// Token: 0x0600793E RID: 31038 RVA: 0x001BBB25 File Offset: 0x001B9D25
		public static TypeSymbol StrippedType(this TypeSymbol type)
		{
			if (!type.IsNullableType())
			{
				return type;
			}
			return type.GetNullableUnderlyingType();
		}

		// Token: 0x0600793F RID: 31039 RVA: 0x001BBB37 File Offset: 0x001B9D37
		public static TypeSymbol EnumUnderlyingTypeOrSelf(this TypeSymbol type)
		{
			return type.GetEnumUnderlyingType() ?? type;
		}

		// Token: 0x06007940 RID: 31040 RVA: 0x001BBB44 File Offset: 0x001B9D44
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool IsNativeIntegerOrNullableNativeIntegerType(this TypeSymbol type)
		{
			return type != null && type.StrippedType().IsNativeIntegerType;
		}

		// Token: 0x06007941 RID: 31041 RVA: 0x001BBB56 File Offset: 0x001B9D56
		public static bool IsObjectType(this TypeSymbol type)
		{
			return type.SpecialType == SpecialType.System_Object;
		}

		// Token: 0x06007942 RID: 31042 RVA: 0x001BBB61 File Offset: 0x001B9D61
		public static bool IsStringType(this TypeSymbol type)
		{
			return type.SpecialType == SpecialType.System_String;
		}

		// Token: 0x06007943 RID: 31043 RVA: 0x001BBB6D File Offset: 0x001B9D6D
		public static bool IsCharType(this TypeSymbol type)
		{
			return type.SpecialType == SpecialType.System_Char;
		}

		// Token: 0x06007944 RID: 31044 RVA: 0x001BBB78 File Offset: 0x001B9D78
		public static bool IsIntegralType(this TypeSymbol type)
		{
			return type.SpecialType.IsIntegralType();
		}

		// Token: 0x06007945 RID: 31045 RVA: 0x001BBB88 File Offset: 0x001B9D88
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static NamedTypeSymbol GetEnumUnderlyingType(this TypeSymbol type)
		{
			NamedTypeSymbol namedTypeSymbol = type as NamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				return null;
			}
			return namedTypeSymbol.EnumUnderlyingType;
		}

		// Token: 0x06007946 RID: 31046 RVA: 0x001BBBA7 File Offset: 0x001B9DA7
		public static bool IsEnumType(this TypeSymbol type)
		{
			return type.TypeKind == TypeKind.Enum;
		}

		// Token: 0x06007947 RID: 31047 RVA: 0x001BBBB4 File Offset: 0x001B9DB4
		public static bool IsValidEnumType(this TypeSymbol type)
		{
			NamedTypeSymbol enumUnderlyingType = type.GetEnumUnderlyingType();
			return enumUnderlyingType != null && enumUnderlyingType.SpecialType > SpecialType.None;
		}

		// Token: 0x06007948 RID: 31048 RVA: 0x001BBBD6 File Offset: 0x001B9DD6
		public static bool IsValidAttributeParameterType(this TypeSymbol type, CSharpCompilation compilation)
		{
			return type.GetAttributeParameterTypedConstantKind(compilation) > TypedConstantKind.Error;
		}

		// Token: 0x06007949 RID: 31049 RVA: 0x001BBBE4 File Offset: 0x001B9DE4
		public static TypedConstantKind GetAttributeParameterTypedConstantKind(this TypeSymbol type, CSharpCompilation compilation)
		{
			TypedConstantKind typedConstantKind = TypedConstantKind.Error;
			if (type == null)
			{
				return TypedConstantKind.Error;
			}
			if (type.Kind == SymbolKind.ArrayType)
			{
				ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)type;
				if (!arrayTypeSymbol.IsSZArray)
				{
					return TypedConstantKind.Error;
				}
				typedConstantKind = TypedConstantKind.Array;
				type = arrayTypeSymbol.ElementType;
			}
			if (type.IsEnumType())
			{
				if (typedConstantKind == TypedConstantKind.Error)
				{
					typedConstantKind = TypedConstantKind.Enum;
				}
				type = type.GetEnumUnderlyingType();
			}
			TypedConstantKind typedConstantKind2 = TypedConstant.GetTypedConstantKind(type, compilation);
			switch (typedConstantKind2)
			{
			case TypedConstantKind.Error:
			case TypedConstantKind.Enum:
			case TypedConstantKind.Array:
				return TypedConstantKind.Error;
			}
			if (typedConstantKind == TypedConstantKind.Array || typedConstantKind == TypedConstantKind.Enum)
			{
				return typedConstantKind;
			}
			return typedConstantKind2;
		}

		// Token: 0x0600794A RID: 31050 RVA: 0x001BBC64 File Offset: 0x001B9E64
		public static bool IsValidExtensionParameterType(this TypeSymbol type)
		{
			TypeKind typeKind = type.TypeKind;
			return typeKind != TypeKind.Dynamic && typeKind != TypeKind.Pointer && typeKind != TypeKind.FunctionPointer;
		}

		// Token: 0x0600794B RID: 31051 RVA: 0x001BBC89 File Offset: 0x001B9E89
		public static bool IsInterfaceType(this TypeSymbol type)
		{
			return type.Kind == SymbolKind.NamedType && ((NamedTypeSymbol)type).IsInterface;
		}

		// Token: 0x0600794C RID: 31052 RVA: 0x001BBCA2 File Offset: 0x001B9EA2
		public static bool IsClassType(this TypeSymbol type)
		{
			return type.TypeKind == TypeKind.Class;
		}

		// Token: 0x0600794D RID: 31053 RVA: 0x001BBCAD File Offset: 0x001B9EAD
		public static bool IsStructType(this TypeSymbol type)
		{
			return type.TypeKind == TypeKind.Struct;
		}

		// Token: 0x0600794E RID: 31054 RVA: 0x001BBCB9 File Offset: 0x001B9EB9
		public static bool IsErrorType(this TypeSymbol type)
		{
			return type.Kind == SymbolKind.ErrorType;
		}

		// Token: 0x0600794F RID: 31055 RVA: 0x001BBCC4 File Offset: 0x001B9EC4
		public static bool IsMethodTypeParameter(this TypeParameterSymbol p)
		{
			return p.ContainingSymbol.Kind == SymbolKind.Method;
		}

		// Token: 0x06007950 RID: 31056 RVA: 0x001BBCD5 File Offset: 0x001B9ED5
		public static bool IsDynamic(this TypeSymbol type)
		{
			return type.TypeKind == TypeKind.Dynamic;
		}

		// Token: 0x06007951 RID: 31057 RVA: 0x001BBCE0 File Offset: 0x001B9EE0
		public static bool IsTypeParameter(this TypeSymbol type)
		{
			return type.TypeKind == TypeKind.TypeParameter;
		}

		// Token: 0x06007952 RID: 31058 RVA: 0x001BBCEC File Offset: 0x001B9EEC
		public static bool IsArray(this TypeSymbol type)
		{
			return type.TypeKind == TypeKind.Array;
		}

		// Token: 0x06007953 RID: 31059 RVA: 0x001BBCF7 File Offset: 0x001B9EF7
		public static bool IsSZArray(this TypeSymbol type)
		{
			return type.TypeKind == TypeKind.Array && ((ArrayTypeSymbol)type).IsSZArray;
		}

		// Token: 0x06007954 RID: 31060 RVA: 0x001BBD0F File Offset: 0x001B9F0F
		public static bool IsFunctionPointer(this TypeSymbol type)
		{
			return type.TypeKind == TypeKind.FunctionPointer;
		}

		// Token: 0x06007955 RID: 31061 RVA: 0x001BBD1C File Offset: 0x001B9F1C
		public static bool IsPointerOrFunctionPointer(this TypeSymbol type)
		{
			TypeKind typeKind = type.TypeKind;
			return typeKind == TypeKind.Pointer || typeKind == TypeKind.FunctionPointer;
		}

		// Token: 0x06007956 RID: 31062 RVA: 0x001BBD40 File Offset: 0x001B9F40
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static NamedTypeSymbol GetDelegateType(this TypeSymbol type)
		{
			if (type == null)
			{
				return null;
			}
			if (type.IsExpressionTree())
			{
				type = ((NamedTypeSymbol)type).TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[0].Type;
			}
			if (!type.IsDelegateType())
			{
				return null;
			}
			return (NamedTypeSymbol)type;
		}

		// Token: 0x06007957 RID: 31063 RVA: 0x001BBD88 File Offset: 0x001B9F88
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static TypeSymbol GetDelegateOrFunctionPointerType(this TypeSymbol type)
		{
			return type.GetDelegateType() ?? (type as FunctionPointerTypeSymbol);
		}

		// Token: 0x06007958 RID: 31064 RVA: 0x001BBD9C File Offset: 0x001B9F9C
		public static bool IsExpressionTree(this TypeSymbol type)
		{
			bool flag;
			return type.IsGenericOrNonGenericExpressionType(out flag) && flag;
		}

		// Token: 0x06007959 RID: 31065 RVA: 0x001BBDB4 File Offset: 0x001B9FB4
		public static bool IsNonGenericExpressionType(this TypeSymbol type)
		{
			bool flag;
			return type.IsGenericOrNonGenericExpressionType(out flag) && !flag;
		}

		// Token: 0x0600795A RID: 31066 RVA: 0x001BBDD4 File Offset: 0x001B9FD4
		public static bool IsGenericOrNonGenericExpressionType(this TypeSymbol _type, out bool isGenericType)
		{
			NamedTypeSymbol namedTypeSymbol = _type.OriginalDefinition as NamedTypeSymbol;
			if (namedTypeSymbol != null && namedTypeSymbol.Name == "Expression" && TypeSymbolExtensions.CheckFullName(namedTypeSymbol.ContainingSymbol, TypeSymbolExtensions.s_expressionsNamespaceName))
			{
				if (namedTypeSymbol.Arity == 0)
				{
					isGenericType = false;
					return true;
				}
				if (namedTypeSymbol.Arity == 1 && namedTypeSymbol.MangleName)
				{
					isGenericType = true;
					return true;
				}
			}
			isGenericType = false;
			return false;
		}

		// Token: 0x0600795B RID: 31067 RVA: 0x001BBE3C File Offset: 0x001BA03C
		public static bool IsPossibleArrayGenericInterface(this TypeSymbol type)
		{
			NamedTypeSymbol namedTypeSymbol = type as NamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				return false;
			}
			namedTypeSymbol = namedTypeSymbol.OriginalDefinition;
			SpecialType specialType = namedTypeSymbol.SpecialType;
			return specialType == SpecialType.System_Collections_Generic_IList_T || specialType == SpecialType.System_Collections_Generic_ICollection_T || specialType == SpecialType.System_Collections_Generic_IEnumerable_T || specialType == SpecialType.System_Collections_Generic_IReadOnlyList_T || specialType == SpecialType.System_Collections_Generic_IReadOnlyCollection_T;
		}

		// Token: 0x0600795C RID: 31068 RVA: 0x001BBE80 File Offset: 0x001BA080
		private static bool CheckFullName(Symbol symbol, string[] names)
		{
			for (int i = 0; i < names.Length; i++)
			{
				if (symbol == null || symbol.Name != names[i])
				{
					return false;
				}
				symbol = symbol.ContainingSymbol;
			}
			return true;
		}

		// Token: 0x0600795D RID: 31069 RVA: 0x001BBEB9 File Offset: 0x001BA0B9
		public static bool IsDelegateType(this TypeSymbol type)
		{
			return type.TypeKind == TypeKind.Delegate;
		}

		// Token: 0x0600795E RID: 31070 RVA: 0x001BBEC4 File Offset: 0x001BA0C4
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<ParameterSymbol> DelegateParameters(this TypeSymbol type)
		{
			MethodSymbol methodSymbol = type.DelegateInvokeMethod();
			if (methodSymbol == null)
			{
				return default(ImmutableArray<ParameterSymbol>);
			}
			return methodSymbol.Parameters;
		}

		// Token: 0x0600795F RID: 31071 RVA: 0x001BBEEC File Offset: 0x001BA0EC
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<ParameterSymbol> DelegateOrFunctionPointerParameters(this TypeSymbol type)
		{
			FunctionPointerTypeSymbol functionPointerTypeSymbol = type as FunctionPointerTypeSymbol;
			if (functionPointerTypeSymbol != null)
			{
				FunctionPointerMethodSymbol signature = functionPointerTypeSymbol.Signature;
				if (signature != null)
				{
					return signature.Parameters;
				}
			}
			return type.DelegateParameters();
		}

		// Token: 0x06007960 RID: 31072 RVA: 0x001BBF1C File Offset: 0x001BA11C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static bool TryGetElementTypesWithAnnotationsIfTupleType([global::System.Runtime.CompilerServices.Nullable(1)] this TypeSymbol type, out ImmutableArray<TypeWithAnnotations> elementTypes)
		{
			if (type.IsTupleType)
			{
				elementTypes = ((NamedTypeSymbol)type).TupleElementTypesWithAnnotations;
				return true;
			}
			elementTypes = default(ImmutableArray<TypeWithAnnotations>);
			return false;
		}

		// Token: 0x06007961 RID: 31073 RVA: 0x001BBF41 File Offset: 0x001BA141
		public static MethodSymbol DelegateInvokeMethod(this TypeSymbol type)
		{
			return type.GetDelegateType().DelegateInvokeMethod;
		}

		// Token: 0x06007962 RID: 31074 RVA: 0x001BBF50 File Offset: 0x001BA150
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public static ConstantValue GetDefaultValue(this TypeSymbol type)
		{
			if (type.IsErrorType())
			{
				return null;
			}
			if (type.IsReferenceType)
			{
				return ConstantValue.Null;
			}
			if (type.IsValueType)
			{
				type = type.EnumUnderlyingTypeOrSelf();
				switch (type.SpecialType)
				{
				case SpecialType.System_Boolean:
				case SpecialType.System_Char:
				case SpecialType.System_SByte:
				case SpecialType.System_Byte:
				case SpecialType.System_Int16:
				case SpecialType.System_UInt16:
				case SpecialType.System_Int32:
				case SpecialType.System_UInt32:
				case SpecialType.System_Int64:
				case SpecialType.System_UInt64:
				case SpecialType.System_Decimal:
				case SpecialType.System_Single:
				case SpecialType.System_Double:
					break;
				case SpecialType.System_String:
					goto IL_0097;
				case SpecialType.System_IntPtr:
					if (!type.IsNativeIntegerType)
					{
						goto IL_0097;
					}
					break;
				case SpecialType.System_UIntPtr:
					if (!type.IsNativeIntegerType)
					{
						goto IL_0097;
					}
					break;
				default:
					goto IL_0097;
				}
				return ConstantValue.Default(type.SpecialType);
			}
			IL_0097:
			return null;
		}

		// Token: 0x06007963 RID: 31075 RVA: 0x001BBFF5 File Offset: 0x001BA1F5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static SpecialType GetSpecialTypeSafe(this TypeSymbol type)
		{
			if (type == null)
			{
				return SpecialType.None;
			}
			return type.SpecialType;
		}

		// Token: 0x06007964 RID: 31076 RVA: 0x001BC004 File Offset: 0x001BA204
		public static bool IsAtLeastAsVisibleAs(this TypeSymbol type, Symbol sym, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			CompoundUseSiteInfo<AssemblySymbol> localUseSiteInfo = useSiteInfo;
			object obj = type.VisitType(([global::System.Runtime.CompilerServices.Nullable(1)] TypeSymbol type1, Symbol symbol, bool unused) => TypeSymbolExtensions.IsTypeLessVisibleThan(type1, symbol, ref localUseSiteInfo), sym, true);
			useSiteInfo = localUseSiteInfo;
			return obj == null;
		}

		// Token: 0x06007965 RID: 31077 RVA: 0x001BC048 File Offset: 0x001BA248
		private static bool IsTypeLessVisibleThan(TypeSymbol type, Symbol sym, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			switch (type.TypeKind)
			{
			case TypeKind.Class:
			case TypeKind.Delegate:
			case TypeKind.Enum:
			case TypeKind.Interface:
			case TypeKind.Struct:
			case TypeKind.Submission:
				return !TypeSymbolExtensions.IsAsRestrictive((NamedTypeSymbol)type, sym, ref useSiteInfo);
			}
			return false;
		}

		// Token: 0x06007966 RID: 31078 RVA: 0x001BC0A4 File Offset: 0x001BA2A4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public static TypeSymbol VisitType<[global::System.Runtime.CompilerServices.Nullable(2)] T>(this TypeSymbol type, Func<TypeSymbol, T, bool, bool> predicate, T arg, bool canDigThroughNullable = false)
		{
			return default(TypeWithAnnotations).VisitType(type, null, predicate, arg, canDigThroughNullable, false);
		}

		// Token: 0x06007967 RID: 31079 RVA: 0x001BC0C8 File Offset: 0x001BA2C8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static TypeSymbol VisitType<T>(this TypeWithAnnotations typeWithAnnotationsOpt, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] Func<TypeWithAnnotations, T, bool, bool> typeWithAnnotationsPredicate, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })] Func<TypeSymbol, T, bool, bool> typePredicate, [global::System.Runtime.CompilerServices.Nullable(1)] T arg, bool canDigThroughNullable = false, bool useDefaultType = false)
		{
			TypeSymbol typeSymbol;
			TypeSymbol typeSymbol2;
			for (;;)
			{
				typeSymbol = type ?? (useDefaultType ? typeWithAnnotationsOpt.DefaultType : typeWithAnnotationsOpt.Type);
				bool flag = false;
				switch (typeSymbol.TypeKind)
				{
				case TypeKind.Class:
				case TypeKind.Delegate:
				case TypeKind.Enum:
				case TypeKind.Interface:
				case TypeKind.Struct:
				{
					NamedTypeSymbol containingType = typeSymbol.ContainingType;
					if (containingType != null)
					{
						flag = true;
						typeSymbol2 = default(TypeWithAnnotations).VisitType(containingType, typeWithAnnotationsPredicate, typePredicate, arg, canDigThroughNullable, useDefaultType);
						if (typeSymbol2 != null)
						{
							return typeSymbol2;
						}
					}
					break;
				}
				}
				if (typeWithAnnotationsOpt.HasType && typeWithAnnotationsPredicate != null)
				{
					if (typeWithAnnotationsPredicate.Invoke(typeWithAnnotationsOpt, arg, flag))
					{
						return typeSymbol;
					}
				}
				else if (typePredicate != null && typePredicate.Invoke(typeSymbol, arg, flag))
				{
					return typeSymbol;
				}
				TypeWithAnnotations typeWithAnnotations;
				switch (typeSymbol.TypeKind)
				{
				case TypeKind.Array:
					typeWithAnnotations = ((ArrayTypeSymbol)typeSymbol).ElementTypeWithAnnotations;
					goto IL_01BD;
				case TypeKind.Class:
				case TypeKind.Delegate:
				case TypeKind.Error:
				case TypeKind.Interface:
				case TypeKind.Struct:
				{
					ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotationsNoUseSiteDiagnostics = ((NamedTypeSymbol)typeSymbol).TypeArgumentsWithAnnotationsNoUseSiteDiagnostics;
					if (typeArgumentsWithAnnotationsNoUseSiteDiagnostics.IsEmpty)
					{
						goto Block_11;
					}
					int i;
					for (i = 0; i < typeArgumentsWithAnnotationsNoUseSiteDiagnostics.Length - 1; i++)
					{
						ValueTuple<TypeWithAnnotations, TypeSymbol> valueTuple = TypeSymbolExtensions.<VisitType>g__getNextIterationElements|54_0<T>(typeArgumentsWithAnnotationsNoUseSiteDiagnostics[i], canDigThroughNullable);
						TypeWithAnnotations item = valueTuple.Item1;
						TypeSymbol item2 = valueTuple.Item2;
						TypeSymbol typeSymbol3 = item.VisitType(item2, typeWithAnnotationsPredicate, typePredicate, arg, canDigThroughNullable, useDefaultType);
						if (typeSymbol3 != null)
						{
							return typeSymbol3;
						}
					}
					typeWithAnnotations = typeArgumentsWithAnnotationsNoUseSiteDiagnostics[i];
					goto IL_01BD;
				}
				case TypeKind.Dynamic:
				case TypeKind.Enum:
				case TypeKind.TypeParameter:
				case TypeKind.Submission:
					goto IL_00FB;
				case TypeKind.Pointer:
					typeWithAnnotations = ((PointerTypeSymbol)typeSymbol).PointedAtTypeWithAnnotations;
					goto IL_01BD;
				case TypeKind.FunctionPointer:
				{
					TypeSymbol typeSymbol4 = TypeSymbolExtensions.<VisitType>g__visitFunctionPointerType|54_1<T>((FunctionPointerTypeSymbol)typeSymbol, typeWithAnnotationsPredicate, typePredicate, arg, useDefaultType, canDigThroughNullable, out typeWithAnnotations);
					if (typeSymbol4 != null)
					{
						return typeSymbol4;
					}
					goto IL_01BD;
				}
				}
				goto Block_10;
				IL_01BD:
				typeWithAnnotationsOpt = (canDigThroughNullable ? default(TypeWithAnnotations) : typeWithAnnotations);
				type = (canDigThroughNullable ? typeWithAnnotations.NullableUnderlyingTypeOrSelf : null);
			}
			return typeSymbol2;
			Block_10:
			goto IL_01AC;
			IL_00FB:
			return null;
			Block_11:
			return null;
			IL_01AC:
			throw ExceptionUtilities.UnexpectedValue(typeSymbol.TypeKind);
		}

		// Token: 0x06007968 RID: 31080 RVA: 0x001BC2BC File Offset: 0x001BA4BC
		private static bool IsAsRestrictive(NamedTypeSymbol s1, Symbol sym2, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			Accessibility declaredAccessibility = s1.DeclaredAccessibility;
			if (declaredAccessibility == Accessibility.Public)
			{
				return true;
			}
			Symbol symbol = sym2;
			while (symbol.Kind != SymbolKind.Namespace)
			{
				Accessibility declaredAccessibility2 = symbol.DeclaredAccessibility;
				switch (declaredAccessibility)
				{
				case Accessibility.Private:
					if (declaredAccessibility2 == Accessibility.Private)
					{
						NamedTypeSymbol containingType = s1.ContainingType;
						if (containingType != null)
						{
							NamedTypeSymbol originalDefinition = containingType.OriginalDefinition;
							for (NamedTypeSymbol namedTypeSymbol = symbol.ContainingType; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.ContainingType)
							{
								if (namedTypeSymbol.OriginalDefinition == originalDefinition || (originalDefinition.TypeKind == TypeKind.Submission && namedTypeSymbol.TypeKind == TypeKind.Submission))
								{
									return true;
								}
							}
						}
					}
					break;
				case Accessibility.ProtectedAndInternal:
					if ((declaredAccessibility2 == Accessibility.Private || declaredAccessibility2 == Accessibility.Internal || declaredAccessibility2 == Accessibility.ProtectedAndInternal) && symbol.ContainingAssembly.HasInternalAccessTo(s1.ContainingAssembly))
					{
						goto IL_0088;
					}
					break;
				case Accessibility.Protected:
					goto IL_0088;
				case Accessibility.Internal:
					if ((declaredAccessibility2 == Accessibility.Private || declaredAccessibility2 == Accessibility.Internal || declaredAccessibility2 == Accessibility.ProtectedAndInternal) && symbol.ContainingAssembly.HasInternalAccessTo(s1.ContainingAssembly))
					{
						return true;
					}
					break;
				case Accessibility.ProtectedOrInternal:
				{
					NamedTypeSymbol containingType2 = s1.ContainingType;
					if (containingType2 != null)
					{
						switch (declaredAccessibility2)
						{
						case Accessibility.Private:
						{
							if (symbol.ContainingAssembly.HasInternalAccessTo(s1.ContainingAssembly))
							{
								return true;
							}
							for (NamedTypeSymbol namedTypeSymbol2 = symbol.ContainingType; namedTypeSymbol2 != null; namedTypeSymbol2 = namedTypeSymbol2.ContainingType)
							{
								if (containingType2.IsAccessibleViaInheritance(namedTypeSymbol2, ref useSiteInfo))
								{
									return true;
								}
							}
							break;
						}
						case Accessibility.ProtectedAndInternal:
							if (symbol.ContainingAssembly.HasInternalAccessTo(s1.ContainingAssembly) || containingType2.IsAccessibleViaInheritance(symbol.ContainingType, ref useSiteInfo))
							{
								return true;
							}
							break;
						case Accessibility.Protected:
							if (containingType2.IsAccessibleViaInheritance(symbol.ContainingType, ref useSiteInfo))
							{
								return true;
							}
							break;
						case Accessibility.Internal:
							if (symbol.ContainingAssembly.HasInternalAccessTo(s1.ContainingAssembly))
							{
								return true;
							}
							break;
						case Accessibility.ProtectedOrInternal:
							if (symbol.ContainingAssembly.HasInternalAccessTo(s1.ContainingAssembly) && containingType2.IsAccessibleViaInheritance(symbol.ContainingType, ref useSiteInfo))
							{
								return true;
							}
							break;
						}
					}
					break;
				}
				default:
					throw ExceptionUtilities.UnexpectedValue(declaredAccessibility);
				}
				IL_0236:
				symbol = symbol.ContainingSymbol;
				continue;
				IL_0088:
				NamedTypeSymbol containingType3 = s1.ContainingType;
				if (containingType3 == null)
				{
					goto IL_0236;
				}
				if (declaredAccessibility2 == Accessibility.Private)
				{
					for (NamedTypeSymbol namedTypeSymbol3 = symbol.ContainingType; namedTypeSymbol3 != null; namedTypeSymbol3 = namedTypeSymbol3.ContainingType)
					{
						if (containingType3.IsAccessibleViaInheritance(namedTypeSymbol3, ref useSiteInfo))
						{
							return true;
						}
					}
					goto IL_0236;
				}
				if (declaredAccessibility2 != Accessibility.Protected && declaredAccessibility2 != Accessibility.ProtectedAndInternal)
				{
					goto IL_0236;
				}
				NamedTypeSymbol containingType4 = symbol.ContainingType;
				if (containingType4 != null && containingType3.IsAccessibleViaInheritance(containingType4, ref useSiteInfo))
				{
					return true;
				}
				goto IL_0236;
			}
			return false;
		}

		// Token: 0x06007969 RID: 31081 RVA: 0x001BC514 File Offset: 0x001BA714
		public static bool IsUnboundGenericType(this TypeSymbol type)
		{
			NamedTypeSymbol namedTypeSymbol = type as NamedTypeSymbol;
			return namedTypeSymbol != null && namedTypeSymbol.IsUnboundGenericType;
		}

		// Token: 0x0600796A RID: 31082 RVA: 0x001BC533 File Offset: 0x001BA733
		public static bool IsTopLevelType(this NamedTypeSymbol type)
		{
			return type.ContainingType == null;
		}

		// Token: 0x0600796B RID: 31083 RVA: 0x001BC53E File Offset: 0x001BA73E
		public static bool ContainsTypeParameter(this TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterSymbol parameter = null)
		{
			return type.VisitType(TypeSymbolExtensions.s_containsTypeParameterPredicate, parameter, false) != null;
		}

		// Token: 0x0600796C RID: 31084 RVA: 0x001BC550 File Offset: 0x001BA750
		public static bool ContainsTypeParameter(this TypeSymbol type, MethodSymbol parameterContainer)
		{
			return type.VisitType(TypeSymbolExtensions.s_isTypeParameterWithSpecificContainerPredicate, parameterContainer, false) != null;
		}

		// Token: 0x0600796D RID: 31085 RVA: 0x001BC562 File Offset: 0x001BA762
		public static bool ContainsTypeParameters(this TypeSymbol type, HashSet<TypeParameterSymbol> parameters)
		{
			return type.VisitType(TypeSymbolExtensions.s_containsTypeParametersPredicate, parameters, false) != null;
		}

		// Token: 0x0600796E RID: 31086 RVA: 0x001BC574 File Offset: 0x001BA774
		public static bool ContainsDynamic(this TypeSymbol type)
		{
			return type.VisitType(TypeSymbolExtensions.s_containsDynamicPredicate, null, true) != null;
		}

		// Token: 0x0600796F RID: 31087 RVA: 0x001BC586 File Offset: 0x001BA786
		internal static bool ContainsNativeInteger(this TypeSymbol type)
		{
			return type.VisitType(([global::System.Runtime.CompilerServices.Nullable(1)] TypeSymbol type, object unused1, bool unused2) => type.IsNativeIntegerType, null, true) != null;
		}

		// Token: 0x06007970 RID: 31088 RVA: 0x001BC5B2 File Offset: 0x001BA7B2
		internal static bool ContainsNativeInteger(this TypeWithAnnotations type)
		{
			TypeSymbol type2 = type.Type;
			return type2 != null && type2.ContainsNativeInteger();
		}

		// Token: 0x06007971 RID: 31089 RVA: 0x001BC5C6 File Offset: 0x001BA7C6
		internal static bool ContainsErrorType(this TypeSymbol type)
		{
			return type.VisitType(([global::System.Runtime.CompilerServices.Nullable(1)] TypeSymbol type, object unused1, bool unused2) => type.IsErrorType(), null, true) != null;
		}

		// Token: 0x06007972 RID: 31090 RVA: 0x001BC5F2 File Offset: 0x001BA7F2
		internal static bool ContainsTuple(this TypeSymbol type)
		{
			return type.VisitType((TypeSymbol t, [global::System.Runtime.CompilerServices.Nullable(2)] object _1, bool _2) => t.IsTupleType, null, false) != null;
		}

		// Token: 0x06007973 RID: 31091 RVA: 0x001BC61E File Offset: 0x001BA81E
		internal static bool ContainsTupleNames(this TypeSymbol type)
		{
			return type.VisitType((TypeSymbol t, [global::System.Runtime.CompilerServices.Nullable(2)] object _1, bool _2) => !t.TupleElementNames.IsDefault, null, false) != null;
		}

		// Token: 0x06007974 RID: 31092 RVA: 0x001BC64A File Offset: 0x001BA84A
		internal static bool ContainsFunctionPointer(this TypeSymbol type)
		{
			return type.VisitType((TypeSymbol t, [global::System.Runtime.CompilerServices.Nullable(2)] object _, bool _) => t.IsFunctionPointer(), null, false) != null;
		}

		// Token: 0x06007975 RID: 31093 RVA: 0x001BC676 File Offset: 0x001BA876
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static TypeSymbol GetNonErrorGuess(this TypeSymbol type)
		{
			return ExtendedErrorTypeSymbol.ExtractNonErrorType(type);
		}

		// Token: 0x06007976 RID: 31094 RVA: 0x001BC67E File Offset: 0x001BA87E
		internal static TypeKind GetNonErrorTypeKindGuess(this TypeSymbol type)
		{
			return ExtendedErrorTypeSymbol.ExtractNonErrorTypeKind(type);
		}

		// Token: 0x06007977 RID: 31095 RVA: 0x001BC688 File Offset: 0x001BA888
		internal static bool IsValidV6SwitchGoverningType(this TypeSymbol type, bool isTargetTypeOfUserDefinedOp = false)
		{
			if (type.IsNullableType())
			{
				type = type.GetNullableUnderlyingType();
			}
			if (!isTargetTypeOfUserDefinedOp)
			{
				type = type.EnumUnderlyingTypeOrSelf();
			}
			switch (type.SpecialType)
			{
			case SpecialType.System_Boolean:
				return !isTargetTypeOfUserDefinedOp;
			case SpecialType.System_Char:
			case SpecialType.System_SByte:
			case SpecialType.System_Byte:
			case SpecialType.System_Int16:
			case SpecialType.System_UInt16:
			case SpecialType.System_Int32:
			case SpecialType.System_UInt32:
			case SpecialType.System_Int64:
			case SpecialType.System_UInt64:
			case SpecialType.System_String:
				return true;
			}
			return false;
		}

		// Token: 0x06007978 RID: 31096 RVA: 0x001BC704 File Offset: 0x001BA904
		internal static bool IsRestrictedType(this TypeSymbol type, bool ignoreSpanLikeTypes = false)
		{
			SpecialType specialType = type.SpecialType;
			return specialType - SpecialType.System_TypedReference <= 2 || (!ignoreSpanLikeTypes && type.IsRefLikeType);
		}

		// Token: 0x06007979 RID: 31097 RVA: 0x001BC72C File Offset: 0x001BA92C
		public static bool IsIntrinsicType(this TypeSymbol type)
		{
			switch (type.SpecialType)
			{
			case SpecialType.System_Boolean:
			case SpecialType.System_Char:
			case SpecialType.System_SByte:
			case SpecialType.System_Byte:
			case SpecialType.System_Int16:
			case SpecialType.System_UInt16:
			case SpecialType.System_Int32:
			case SpecialType.System_UInt32:
			case SpecialType.System_Int64:
			case SpecialType.System_UInt64:
			case SpecialType.System_Decimal:
			case SpecialType.System_Single:
			case SpecialType.System_Double:
				break;
			case SpecialType.System_String:
				return false;
			case SpecialType.System_IntPtr:
				if (!type.IsNativeIntegerType)
				{
					return false;
				}
				break;
			case SpecialType.System_UIntPtr:
				if (!type.IsNativeIntegerType)
				{
					return false;
				}
				break;
			default:
				return false;
			}
			return true;
		}

		// Token: 0x0600797A RID: 31098 RVA: 0x001BC7A0 File Offset: 0x001BA9A0
		public static bool IsPartial(this TypeSymbol type)
		{
			SourceNamedTypeSymbol sourceNamedTypeSymbol = type as SourceNamedTypeSymbol;
			return sourceNamedTypeSymbol != null && sourceNamedTypeSymbol.IsPartial;
		}

		// Token: 0x0600797B RID: 31099 RVA: 0x001BC7BF File Offset: 0x001BA9BF
		public static bool IsPointerType(this TypeSymbol type)
		{
			return type is PointerTypeSymbol;
		}

		// Token: 0x0600797C RID: 31100 RVA: 0x001BC7CA File Offset: 0x001BA9CA
		internal static int FixedBufferElementSizeInBytes(this TypeSymbol type)
		{
			return type.SpecialType.FixedBufferElementSizeInBytes();
		}

		// Token: 0x0600797D RID: 31101 RVA: 0x001BC7D8 File Offset: 0x001BA9D8
		internal static bool IsValidVolatileFieldType(this TypeSymbol type)
		{
			switch (type.TypeKind)
			{
			case TypeKind.Array:
			case TypeKind.Class:
			case TypeKind.Delegate:
			case TypeKind.Dynamic:
			case TypeKind.Error:
			case TypeKind.Interface:
			case TypeKind.Pointer:
			case TypeKind.FunctionPointer:
				return true;
			case TypeKind.Enum:
				return ((NamedTypeSymbol)type).EnumUnderlyingType.SpecialType.IsValidVolatileFieldType();
			case TypeKind.Struct:
				return type.SpecialType.IsValidVolatileFieldType();
			case TypeKind.TypeParameter:
				return type.IsReferenceType;
			case TypeKind.Submission:
				throw ExceptionUtilities.UnexpectedValue(type.TypeKind);
			}
			return false;
		}

		// Token: 0x0600797E RID: 31102 RVA: 0x001BC867 File Offset: 0x001BAA67
		public static bool MarkCheckedIfNecessary(this TypeSymbol type, ref HashSet<TypeSymbol> checkedTypes)
		{
			if (checkedTypes == null)
			{
				checkedTypes = new HashSet<TypeSymbol>();
			}
			return checkedTypes.Add(type);
		}

		// Token: 0x0600797F RID: 31103 RVA: 0x001BC87C File Offset: 0x001BAA7C
		internal static bool IsUnsafe(this TypeSymbol type)
		{
			TypeKind typeKind;
			for (;;)
			{
				typeKind = type.TypeKind;
				if (typeKind != TypeKind.Array)
				{
					break;
				}
				type = ((ArrayTypeSymbol)type).ElementType;
			}
			return typeKind == TypeKind.Pointer || typeKind == TypeKind.FunctionPointer;
		}

		// Token: 0x06007980 RID: 31104 RVA: 0x001BC8B0 File Offset: 0x001BAAB0
		internal static bool IsVoidPointer(this TypeSymbol type)
		{
			PointerTypeSymbol pointerTypeSymbol = type as PointerTypeSymbol;
			return pointerTypeSymbol != null && pointerTypeSymbol.PointedAtType.IsVoidType();
		}

		// Token: 0x06007981 RID: 31105 RVA: 0x001BC8D4 File Offset: 0x001BAAD4
		internal static bool IsPrimitiveRecursiveStruct(this TypeSymbol t)
		{
			return t.SpecialType.IsPrimitiveRecursiveStruct();
		}

		// Token: 0x06007982 RID: 31106 RVA: 0x001BC8E4 File Offset: 0x001BAAE4
		internal static int ComputeHashCode(this NamedTypeSymbol type)
		{
			if (TypeSymbolExtensions.<ComputeHashCode>g__wasConstructedForAnnotations|85_0(type))
			{
				return type.OriginalDefinition.GetHashCode();
			}
			int num = type.OriginalDefinition.GetHashCode();
			num = Hash.Combine<NamedTypeSymbol>(type.ContainingType, num);
			if (type.ConstructedFrom != type)
			{
				foreach (TypeWithAnnotations typeWithAnnotations in type.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics)
				{
					num = Hash.Combine<TypeSymbol>(typeWithAnnotations.Type, num);
				}
			}
			if (num == 0)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06007983 RID: 31107 RVA: 0x001BC960 File Offset: 0x001BAB60
		public static TypeSymbol AsDynamicIfNoPia(this TypeSymbol type, NamedTypeSymbol containingType)
		{
			TypeSymbol typeSymbol;
			if (!type.TryAsDynamicIfNoPia(containingType, out typeSymbol))
			{
				return type;
			}
			return typeSymbol;
		}

		// Token: 0x06007984 RID: 31108 RVA: 0x001BC97C File Offset: 0x001BAB7C
		public static bool TryAsDynamicIfNoPia(this TypeSymbol type, NamedTypeSymbol containingType, [global::System.Runtime.CompilerServices.Nullable(2)] [NotNullWhen(true)] out TypeSymbol result)
		{
			if (type.SpecialType == SpecialType.System_Object)
			{
				AssemblySymbol containingAssembly = containingType.ContainingAssembly;
				if (containingAssembly != null && containingAssembly.IsLinked && containingType.IsComImport)
				{
					result = DynamicTypeSymbol.Instance;
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06007985 RID: 31109 RVA: 0x001BC9B9 File Offset: 0x001BABB9
		internal static bool IsVerifierReference(this TypeSymbol type)
		{
			return type.IsReferenceType && type.TypeKind != TypeKind.TypeParameter;
		}

		// Token: 0x06007986 RID: 31110 RVA: 0x001BC9D2 File Offset: 0x001BABD2
		internal static bool IsVerifierValue(this TypeSymbol type)
		{
			return type.IsValueType && type.TypeKind != TypeKind.TypeParameter;
		}

		// Token: 0x06007987 RID: 31111 RVA: 0x001BC9EC File Offset: 0x001BABEC
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<TypeParameterSymbol> GetAllTypeParameters(this NamedTypeSymbol type)
		{
			if (type.ContainingType == null)
			{
				return type.TypeParameters;
			}
			ArrayBuilder<TypeParameterSymbol> instance = ArrayBuilder<TypeParameterSymbol>.GetInstance();
			type.GetAllTypeParameters(instance);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06007988 RID: 31112 RVA: 0x001BCA1C File Offset: 0x001BAC1C
		internal static void GetAllTypeParameters(this NamedTypeSymbol type, ArrayBuilder<TypeParameterSymbol> result)
		{
			NamedTypeSymbol containingType = type.ContainingType;
			if (containingType != null)
			{
				containingType.GetAllTypeParameters(result);
			}
			result.AddRange(type.TypeParameters);
		}

		// Token: 0x06007989 RID: 31113 RVA: 0x001BCA48 File Offset: 0x001BAC48
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static TypeParameterSymbol FindEnclosingTypeParameter(this NamedTypeSymbol type, string name)
		{
			ArrayBuilder<TypeParameterSymbol> instance = ArrayBuilder<TypeParameterSymbol>.GetInstance();
			type.GetAllTypeParameters(instance);
			TypeParameterSymbol typeParameterSymbol = null;
			foreach (TypeParameterSymbol typeParameterSymbol2 in instance)
			{
				if (name == typeParameterSymbol2.Name)
				{
					typeParameterSymbol = typeParameterSymbol2;
					break;
				}
			}
			instance.Free();
			return typeParameterSymbol;
		}

		// Token: 0x0600798A RID: 31114 RVA: 0x001BCA98 File Offset: 0x001BAC98
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static TypeParameterSymbol FindEnclosingTypeParameter(this Symbol methodOrType, string name)
		{
			while (methodOrType != null)
			{
				SymbolKind kind = methodOrType.Kind;
				switch (kind)
				{
				case SymbolKind.ErrorType:
				case SymbolKind.Event:
				case SymbolKind.Field:
				case SymbolKind.Method:
				case SymbolKind.NamedType:
					break;
				case SymbolKind.Label:
				case SymbolKind.Local:
				case SymbolKind.NetModule:
					goto IL_0036;
				default:
					if (kind != SymbolKind.Property)
					{
						goto IL_0036;
					}
					break;
				}
				foreach (TypeParameterSymbol typeParameterSymbol in methodOrType.GetMemberTypeParameters())
				{
					if (typeParameterSymbol.Name == name)
					{
						return typeParameterSymbol;
					}
				}
				methodOrType = methodOrType.ContainingSymbol;
				continue;
				IL_0036:
				return null;
			}
			return null;
		}

		// Token: 0x0600798B RID: 31115 RVA: 0x001BCB24 File Offset: 0x001BAD24
		internal static bool HasNameQualifier(this NamedTypeSymbol type, string qualifiedName)
		{
			Symbol containingSymbol = type.ContainingSymbol;
			if (containingSymbol.Kind != SymbolKind.Namespace)
			{
				return string.Equals(containingSymbol.ToDisplayString(SymbolDisplayFormat.QualifiedNameOnlyFormat), qualifiedName, 4);
			}
			NamespaceSymbol namespaceSymbol = (NamespaceSymbol)containingSymbol;
			if (namespaceSymbol.IsGlobalNamespace)
			{
				return qualifiedName.Length == 0;
			}
			return TypeSymbolExtensions.HasNamespaceName(namespaceSymbol, qualifiedName, 4, qualifiedName.Length);
		}

		// Token: 0x0600798C RID: 31116 RVA: 0x001BCB7C File Offset: 0x001BAD7C
		private static bool HasNamespaceName(NamespaceSymbol @namespace, string namespaceName, StringComparison comparison, int length)
		{
			if (length == 0)
			{
				return false;
			}
			NamespaceSymbol containingNamespace = @namespace.ContainingNamespace;
			int num = namespaceName.LastIndexOf('.', length - 1, length);
			int num2 = 0;
			if (num >= 0)
			{
				if (containingNamespace.IsGlobalNamespace)
				{
					return false;
				}
				if (!TypeSymbolExtensions.HasNamespaceName(containingNamespace, namespaceName, comparison, num))
				{
					return false;
				}
				int num3 = num + 1;
				num2 = num3;
				length -= num3;
			}
			else if (!containingNamespace.IsGlobalNamespace)
			{
				return false;
			}
			string name = @namespace.Name;
			return name.Length == length && string.Compare(name, 0, namespaceName, num2, length, comparison) == 0;
		}

		// Token: 0x0600798D RID: 31117 RVA: 0x001BCBFC File Offset: 0x001BADFC
		internal static bool IsNonGenericTaskType(this TypeSymbol type, CSharpCompilation compilation)
		{
			NamedTypeSymbol namedTypeSymbol = type as NamedTypeSymbol;
			object obj;
			return namedTypeSymbol != null && namedTypeSymbol.Arity == 0 && (namedTypeSymbol == compilation.GetWellKnownType(WellKnownType.System_Threading_Tasks_Task) || (!namedTypeSymbol.IsVoidType() && namedTypeSymbol.IsCustomTaskType(out obj)));
		}

		// Token: 0x0600798E RID: 31118 RVA: 0x001BCC3C File Offset: 0x001BAE3C
		internal static bool IsGenericTaskType(this TypeSymbol type, CSharpCompilation compilation)
		{
			NamedTypeSymbol namedTypeSymbol = type as NamedTypeSymbol;
			object obj;
			return namedTypeSymbol != null && namedTypeSymbol.Arity == 1 && (namedTypeSymbol.ConstructedFrom == compilation.GetWellKnownType(WellKnownType.System_Threading_Tasks_Task_T) || namedTypeSymbol.IsCustomTaskType(out obj));
		}

		// Token: 0x0600798F RID: 31119 RVA: 0x001BCC78 File Offset: 0x001BAE78
		internal static bool IsIAsyncEnumerableType(this TypeSymbol type, CSharpCompilation compilation)
		{
			NamedTypeSymbol namedTypeSymbol = type as NamedTypeSymbol;
			return namedTypeSymbol != null && namedTypeSymbol.Arity == 1 && namedTypeSymbol.ConstructedFrom == compilation.GetWellKnownType(WellKnownType.System_Collections_Generic_IAsyncEnumerable_T);
		}

		// Token: 0x06007990 RID: 31120 RVA: 0x001BCCB0 File Offset: 0x001BAEB0
		internal static bool IsIAsyncEnumeratorType(this TypeSymbol type, CSharpCompilation compilation)
		{
			NamedTypeSymbol namedTypeSymbol = type as NamedTypeSymbol;
			return namedTypeSymbol != null && namedTypeSymbol.Arity == 1 && namedTypeSymbol.ConstructedFrom == compilation.GetWellKnownType(WellKnownType.System_Collections_Generic_IAsyncEnumerator_T);
		}

		// Token: 0x06007991 RID: 31121 RVA: 0x001BCCE8 File Offset: 0x001BAEE8
		internal static bool IsCustomTaskType(this NamedTypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] [NotNullWhen(true)] out object builderArgument)
		{
			if (type.Arity < 2)
			{
				foreach (CSharpAttributeData csharpAttributeData in type.GetAttributes())
				{
					if (csharpAttributeData.IsTargetAttribute(type, AttributeDescription.AsyncMethodBuilderAttribute) && csharpAttributeData.CommonConstructorArguments.Length == 1 && csharpAttributeData.CommonConstructorArguments[0].Kind == TypedConstantKind.Type)
					{
						builderArgument = csharpAttributeData.CommonConstructorArguments[0].ValueInternal;
						return true;
					}
				}
			}
			builderArgument = null;
			return false;
		}

		// Token: 0x06007992 RID: 31122 RVA: 0x001BCD7A File Offset: 0x001BAF7A
		internal static TypeSymbol NormalizeTaskTypes(this TypeSymbol type, CSharpCompilation compilation)
		{
			TypeSymbolExtensions.NormalizeTaskTypesInType(compilation, ref type);
			return type;
		}

		// Token: 0x06007993 RID: 31123 RVA: 0x001BCD88 File Offset: 0x001BAF88
		private static bool NormalizeTaskTypesInType(CSharpCompilation compilation, ref TypeSymbol type)
		{
			SymbolKind kind = type.Kind;
			if (kind <= SymbolKind.ErrorType)
			{
				if (kind == SymbolKind.ArrayType)
				{
					ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)type;
					bool flag = TypeSymbolExtensions.NormalizeTaskTypesInArray(compilation, ref arrayTypeSymbol);
					type = arrayTypeSymbol;
					return flag;
				}
				if (kind != SymbolKind.ErrorType)
				{
					return false;
				}
			}
			else if (kind != SymbolKind.NamedType)
			{
				if (kind == SymbolKind.PointerType)
				{
					PointerTypeSymbol pointerTypeSymbol = (PointerTypeSymbol)type;
					bool flag2 = TypeSymbolExtensions.NormalizeTaskTypesInPointer(compilation, ref pointerTypeSymbol);
					type = pointerTypeSymbol;
					return flag2;
				}
				if (kind != SymbolKind.FunctionPointerType)
				{
					return false;
				}
				FunctionPointerTypeSymbol functionPointerTypeSymbol = (FunctionPointerTypeSymbol)type;
				bool flag3 = TypeSymbolExtensions.NormalizeTaskTypesInFunctionPointer(compilation, ref functionPointerTypeSymbol);
				type = functionPointerTypeSymbol;
				return flag3;
			}
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)type;
			bool flag4 = TypeSymbolExtensions.NormalizeTaskTypesInNamedType(compilation, ref namedTypeSymbol);
			type = namedTypeSymbol;
			return flag4;
		}

		// Token: 0x06007994 RID: 31124 RVA: 0x001BCE10 File Offset: 0x001BB010
		private static bool NormalizeTaskTypesInType(CSharpCompilation compilation, ref TypeWithAnnotations typeWithAnnotations)
		{
			TypeSymbol type = typeWithAnnotations.Type;
			if (TypeSymbolExtensions.NormalizeTaskTypesInType(compilation, ref type))
			{
				typeWithAnnotations = TypeWithAnnotations.Create(type, NullableAnnotation.Oblivious, typeWithAnnotations.CustomModifiers);
				return true;
			}
			return false;
		}

		// Token: 0x06007995 RID: 31125 RVA: 0x001BCE44 File Offset: 0x001BB044
		private static bool NormalizeTaskTypesInNamedType(CSharpCompilation compilation, ref NamedTypeSymbol type)
		{
			bool flag = false;
			if (!type.IsDefinition)
			{
				ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				type.GetAllTypeArguments(instance, ref discarded);
				for (int i = 0; i < instance.Count; i++)
				{
					TypeWithAnnotations typeWithAnnotations = instance[i];
					TypeSymbol type2 = typeWithAnnotations.Type;
					if (TypeSymbolExtensions.NormalizeTaskTypesInType(compilation, ref type2))
					{
						flag = true;
						instance[i] = TypeWithAnnotations.Create(type2, NullableAnnotation.Oblivious, typeWithAnnotations.CustomModifiers);
					}
				}
				if (flag)
				{
					NamedTypeSymbol namedTypeSymbol = type;
					NamedTypeSymbol originalDefinition = namedTypeSymbol.OriginalDefinition;
					TypeMap typeMap = new TypeMap(originalDefinition.GetAllTypeParameters(), instance.ToImmutable(), true);
					type = typeMap.SubstituteNamedType(originalDefinition).WithTupleDataFrom(namedTypeSymbol);
				}
				instance.Free();
			}
			object obj;
			if (type.OriginalDefinition.IsCustomTaskType(out obj))
			{
				int arity = type.Arity;
				NamedTypeSymbol wellKnownType = compilation.GetWellKnownType((arity == 0) ? WellKnownType.System_Threading_Tasks_Task : WellKnownType.System_Threading_Tasks_Task_T);
				if (wellKnownType.TypeKind == TypeKind.Error)
				{
					return false;
				}
				type = ((arity == 0) ? wellKnownType : wellKnownType.Construct(ImmutableArray.Create<TypeWithAnnotations>(type.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[0]), false));
				flag = true;
			}
			return flag;
		}

		// Token: 0x06007996 RID: 31126 RVA: 0x001BCF5C File Offset: 0x001BB15C
		private static bool NormalizeTaskTypesInArray(CSharpCompilation compilation, ref ArrayTypeSymbol arrayType)
		{
			TypeWithAnnotations elementTypeWithAnnotations = arrayType.ElementTypeWithAnnotations;
			if (!TypeSymbolExtensions.NormalizeTaskTypesInType(compilation, ref elementTypeWithAnnotations))
			{
				return false;
			}
			arrayType = arrayType.WithElementType(elementTypeWithAnnotations);
			return true;
		}

		// Token: 0x06007997 RID: 31127 RVA: 0x001BCF88 File Offset: 0x001BB188
		private static bool NormalizeTaskTypesInPointer(CSharpCompilation compilation, ref PointerTypeSymbol pointerType)
		{
			TypeWithAnnotations pointedAtTypeWithAnnotations = pointerType.PointedAtTypeWithAnnotations;
			if (!TypeSymbolExtensions.NormalizeTaskTypesInType(compilation, ref pointedAtTypeWithAnnotations))
			{
				return false;
			}
			pointerType = new PointerTypeSymbol(pointedAtTypeWithAnnotations);
			return true;
		}

		// Token: 0x06007998 RID: 31128 RVA: 0x001BCFB4 File Offset: 0x001BB1B4
		private static bool NormalizeTaskTypesInFunctionPointer(CSharpCompilation compilation, ref FunctionPointerTypeSymbol funcPtrType)
		{
			TypeWithAnnotations returnTypeWithAnnotations = funcPtrType.Signature.ReturnTypeWithAnnotations;
			bool flag = TypeSymbolExtensions.NormalizeTaskTypesInType(compilation, ref returnTypeWithAnnotations);
			ImmutableArray<TypeWithAnnotations> immutableArray = ImmutableArray<TypeWithAnnotations>.Empty;
			if (funcPtrType.Signature.ParameterCount > 0)
			{
				ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(funcPtrType.Signature.ParameterCount);
				bool flag2 = false;
				foreach (ParameterSymbol parameterSymbol in funcPtrType.Signature.Parameters)
				{
					TypeWithAnnotations typeWithAnnotations = parameterSymbol.TypeWithAnnotations;
					flag2 |= TypeSymbolExtensions.NormalizeTaskTypesInType(compilation, ref typeWithAnnotations);
					instance.Add(typeWithAnnotations);
				}
				if (flag2)
				{
					flag = true;
					immutableArray = instance.ToImmutableAndFree();
				}
				else
				{
					immutableArray = funcPtrType.Signature.ParameterTypesWithAnnotations;
					instance.Free();
				}
			}
			if (flag)
			{
				funcPtrType = funcPtrType.SubstituteTypeSymbol(returnTypeWithAnnotations, immutableArray, default(ImmutableArray<CustomModifier>), default(ImmutableArray<ImmutableArray<CustomModifier>>));
				return true;
			}
			return false;
		}

		// Token: 0x06007999 RID: 31129 RVA: 0x001BD090 File Offset: 0x001BB290
		internal static TypeReferenceWithAttributes GetTypeRefWithAttributes(this TypeWithAnnotations type, PEModuleBuilder moduleBuilder, Symbol declaringSymbol, ITypeReference typeRef)
		{
			ArrayBuilder<ICustomAttribute> instance = ArrayBuilder<ICustomAttribute>.GetInstance();
			CSharpCompilation declaringCompilation = declaringSymbol.DeclaringCompilation;
			if (declaringCompilation != null)
			{
				if (type.Type.ContainsTupleNames())
				{
					TypeSymbolExtensions.<GetTypeRefWithAttributes>g__addIfNotNull|108_0(instance, declaringCompilation.SynthesizeTupleNamesAttribute(type.Type));
				}
				if (type.Type.ContainsNativeInteger())
				{
					TypeSymbolExtensions.<GetTypeRefWithAttributes>g__addIfNotNull|108_0(instance, moduleBuilder.SynthesizeNativeIntegerAttribute(declaringSymbol, type.Type));
				}
				if (declaringCompilation.ShouldEmitNullableAttributes(declaringSymbol))
				{
					TypeSymbolExtensions.<GetTypeRefWithAttributes>g__addIfNotNull|108_0(instance, moduleBuilder.SynthesizeNullableAttributeIfNecessary(declaringSymbol, declaringSymbol.GetNullableContextValue(), type));
				}
			}
			return new TypeReferenceWithAttributes(typeRef, instance.ToImmutableAndFree());
		}

		// Token: 0x0600799A RID: 31130 RVA: 0x001BD119 File Offset: 0x001BB319
		internal static bool IsWellKnownTypeInAttribute(this TypeSymbol typeSymbol)
		{
			return typeSymbol.IsWellKnownInteropServicesTopLevelType("InAttribute");
		}

		// Token: 0x0600799B RID: 31131 RVA: 0x001BD126 File Offset: 0x001BB326
		internal static bool IsWellKnownTypeUnmanagedType(this TypeSymbol typeSymbol)
		{
			return typeSymbol.IsWellKnownInteropServicesTopLevelType("UnmanagedType");
		}

		// Token: 0x0600799C RID: 31132 RVA: 0x001BD133 File Offset: 0x001BB333
		internal static bool IsWellKnownTypeIsExternalInit(this TypeSymbol typeSymbol)
		{
			return typeSymbol.IsWellKnownCompilerServicesTopLevelType("IsExternalInit");
		}

		// Token: 0x0600799D RID: 31133 RVA: 0x001BD140 File Offset: 0x001BB340
		internal static bool IsWellKnownTypeOutAttribute(this TypeSymbol typeSymbol)
		{
			return typeSymbol.IsWellKnownInteropServicesTopLevelType("OutAttribute");
		}

		// Token: 0x0600799E RID: 31134 RVA: 0x001BD14D File Offset: 0x001BB34D
		private static bool IsWellKnownInteropServicesTopLevelType(this TypeSymbol typeSymbol, string name)
		{
			return !(typeSymbol.Name != name) && typeSymbol.ContainingType == null && typeSymbol.IsContainedInNamespace("System", "Runtime", "InteropServices");
		}

		// Token: 0x0600799F RID: 31135 RVA: 0x001BD17C File Offset: 0x001BB37C
		private static bool IsWellKnownCompilerServicesTopLevelType(this TypeSymbol typeSymbol, string name)
		{
			return !(typeSymbol.Name != name) && typeSymbol.IsCompilerServicesTopLevelType();
		}

		// Token: 0x060079A0 RID: 31136 RVA: 0x001BD194 File Offset: 0x001BB394
		internal static bool IsCompilerServicesTopLevelType(this TypeSymbol typeSymbol)
		{
			return typeSymbol.ContainingType == null && typeSymbol.IsContainedInNamespace("System", "Runtime", "CompilerServices");
		}

		// Token: 0x060079A1 RID: 31137 RVA: 0x001BD1B8 File Offset: 0x001BB3B8
		private static bool IsContainedInNamespace(this TypeSymbol typeSymbol, string outerNS, string midNS, string innerNS)
		{
			NamespaceSymbol containingNamespace = typeSymbol.ContainingNamespace;
			if (((containingNamespace != null) ? containingNamespace.Name : null) != innerNS)
			{
				return false;
			}
			NamespaceSymbol containingNamespace2 = containingNamespace.ContainingNamespace;
			if (((containingNamespace2 != null) ? containingNamespace2.Name : null) != midNS)
			{
				return false;
			}
			NamespaceSymbol containingNamespace3 = containingNamespace2.ContainingNamespace;
			if (((containingNamespace3 != null) ? containingNamespace3.Name : null) != outerNS)
			{
				return false;
			}
			NamespaceSymbol containingNamespace4 = containingNamespace3.ContainingNamespace;
			return containingNamespace4 != null && containingNamespace4.IsGlobalNamespace;
		}

		// Token: 0x060079A2 RID: 31138 RVA: 0x001BD234 File Offset: 0x001BB434
		public static bool IsBadAsyncReturn(this TypeSymbol returnType, CSharpCompilation declaringCompilation)
		{
			return !returnType.IsErrorType() && !returnType.IsVoidType() && !returnType.IsNonGenericTaskType(declaringCompilation) && !returnType.IsGenericTaskType(declaringCompilation) && !returnType.IsIAsyncEnumerableType(declaringCompilation) && !returnType.IsIAsyncEnumeratorType(declaringCompilation);
		}

		// Token: 0x060079A3 RID: 31139 RVA: 0x001BD270 File Offset: 0x001BB470
		internal static int TypeToIndex(this TypeSymbol type)
		{
			switch (type.GetSpecialTypeSafe())
			{
			case SpecialType.None:
				if (type != null && type.IsNullableType())
				{
					TypeSymbol nullableUnderlyingType = type.GetNullableUnderlyingType();
					switch (nullableUnderlyingType.GetSpecialTypeSafe())
					{
					case SpecialType.System_Boolean:
						return 17;
					case SpecialType.System_Char:
						return 18;
					case SpecialType.System_SByte:
						return 19;
					case SpecialType.System_Byte:
						return 23;
					case SpecialType.System_Int16:
						return 20;
					case SpecialType.System_UInt16:
						return 24;
					case SpecialType.System_Int32:
						return 21;
					case SpecialType.System_UInt32:
						return 25;
					case SpecialType.System_Int64:
						return 22;
					case SpecialType.System_UInt64:
						return 26;
					case SpecialType.System_Decimal:
						return 31;
					case SpecialType.System_Single:
						return 29;
					case SpecialType.System_Double:
						return 30;
					case SpecialType.System_IntPtr:
						if (nullableUnderlyingType.IsNativeIntegerType)
						{
							return 27;
						}
						break;
					case SpecialType.System_UIntPtr:
						if (nullableUnderlyingType.IsNativeIntegerType)
						{
							return 28;
						}
						break;
					}
				}
				break;
			case SpecialType.System_Object:
				return 0;
			case SpecialType.System_Boolean:
				return 2;
			case SpecialType.System_Char:
				return 3;
			case SpecialType.System_SByte:
				return 4;
			case SpecialType.System_Byte:
				return 8;
			case SpecialType.System_Int16:
				return 5;
			case SpecialType.System_UInt16:
				return 9;
			case SpecialType.System_Int32:
				return 6;
			case SpecialType.System_UInt32:
				return 10;
			case SpecialType.System_Int64:
				return 7;
			case SpecialType.System_UInt64:
				return 11;
			case SpecialType.System_Decimal:
				return 16;
			case SpecialType.System_Single:
				return 14;
			case SpecialType.System_Double:
				return 15;
			case SpecialType.System_String:
				return 1;
			case SpecialType.System_IntPtr:
				if (type.IsNativeIntegerType)
				{
					return 12;
				}
				break;
			case SpecialType.System_UIntPtr:
				if (type.IsNativeIntegerType)
				{
					return 13;
				}
				break;
			}
			return -1;
		}

		// Token: 0x060079A4 RID: 31140 RVA: 0x001BD3D4 File Offset: 0x001BB5D4
		public static NamedTypeSymbol AsUnboundGenericType(this NamedTypeSymbol type)
		{
			if (!type.IsGenericType)
			{
				throw new InvalidOperationException();
			}
			NamedTypeSymbol originalDefinition = type.OriginalDefinition;
			int arity = originalDefinition.Arity;
			NamedTypeSymbol containingType = originalDefinition.ContainingType;
			NamedTypeSymbol namedTypeSymbol = ((containingType == null) ? originalDefinition : originalDefinition.AsMember(containingType.IsGenericType ? containingType.AsUnboundGenericType() : containingType));
			if (arity == 0)
			{
				return namedTypeSymbol;
			}
			ImmutableArray<TypeWithAnnotations> immutableArray = UnboundArgumentErrorTypeSymbol.CreateTypeArguments(namedTypeSymbol.TypeParameters, arity, new CSDiagnosticInfo(ErrorCode.ERR_UnexpectedUnboundGenericName));
			return namedTypeSymbol.Construct(immutableArray, true);
		}

		// Token: 0x060079A5 RID: 31141 RVA: 0x001BD448 File Offset: 0x001BB648
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static int CustomModifierCount(this TypeSymbol type)
		{
			if (type == null)
			{
				return 0;
			}
			SymbolKind kind = type.Kind;
			if (kind <= SymbolKind.ErrorType)
			{
				if (kind == SymbolKind.ArrayType)
				{
					return TypeSymbolExtensions.<CustomModifierCount>g__customModifierCountForTypeWithAnnotations|120_0(((ArrayTypeSymbol)type).ElementTypeWithAnnotations);
				}
				if (kind != SymbolKind.ErrorType)
				{
					return 0;
				}
			}
			else if (kind != SymbolKind.NamedType)
			{
				if (kind == SymbolKind.PointerType)
				{
					return TypeSymbolExtensions.<CustomModifierCount>g__customModifierCountForTypeWithAnnotations|120_0(((PointerTypeSymbol)type).PointedAtTypeWithAnnotations);
				}
				if (kind != SymbolKind.FunctionPointerType)
				{
					return 0;
				}
				return ((FunctionPointerTypeSymbol)type).Signature.CustomModifierCount();
			}
			if (!type.IsDefinition)
			{
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)type;
				int num = 0;
				while (namedTypeSymbol != null)
				{
					foreach (TypeWithAnnotations typeWithAnnotations in namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics)
					{
						num += TypeSymbolExtensions.<CustomModifierCount>g__customModifierCountForTypeWithAnnotations|120_0(typeWithAnnotations);
					}
					namedTypeSymbol = namedTypeSymbol.ContainingType;
				}
				return num;
			}
			return 0;
		}

		// Token: 0x060079A6 RID: 31142 RVA: 0x001BD508 File Offset: 0x001BB708
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static bool HasCustomModifiers(this TypeSymbol type, bool flagNonDefaultArraySizesOrLowerBounds)
		{
			if (type == null)
			{
				return false;
			}
			SymbolKind kind = type.Kind;
			if (kind <= SymbolKind.ErrorType)
			{
				if (kind == SymbolKind.ArrayType)
				{
					ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)type;
					return TypeSymbolExtensions.<HasCustomModifiers>g__checkTypeWithAnnotations|121_0(arrayTypeSymbol.ElementTypeWithAnnotations, flagNonDefaultArraySizesOrLowerBounds) || (flagNonDefaultArraySizesOrLowerBounds && !arrayTypeSymbol.HasDefaultSizesAndLowerBounds);
				}
				if (kind != SymbolKind.ErrorType)
				{
					return false;
				}
			}
			else if (kind != SymbolKind.NamedType)
			{
				if (kind == SymbolKind.PointerType)
				{
					return TypeSymbolExtensions.<HasCustomModifiers>g__checkTypeWithAnnotations|121_0(((PointerTypeSymbol)type).PointedAtTypeWithAnnotations, flagNonDefaultArraySizesOrLowerBounds);
				}
				if (kind != SymbolKind.FunctionPointerType)
				{
					return false;
				}
				FunctionPointerTypeSymbol functionPointerTypeSymbol = (FunctionPointerTypeSymbol)type;
				if (!functionPointerTypeSymbol.Signature.RefCustomModifiers.IsEmpty || TypeSymbolExtensions.<HasCustomModifiers>g__checkTypeWithAnnotations|121_0(functionPointerTypeSymbol.Signature.ReturnTypeWithAnnotations, flagNonDefaultArraySizesOrLowerBounds))
				{
					return true;
				}
				foreach (ParameterSymbol parameterSymbol in functionPointerTypeSymbol.Signature.Parameters)
				{
					if (!parameterSymbol.RefCustomModifiers.IsEmpty || TypeSymbolExtensions.<HasCustomModifiers>g__checkTypeWithAnnotations|121_0(parameterSymbol.TypeWithAnnotations, flagNonDefaultArraySizesOrLowerBounds))
					{
						return true;
					}
				}
				return false;
			}
			if (!type.IsDefinition)
			{
				for (NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)type; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.ContainingType)
				{
					ImmutableArray<TypeWithAnnotations>.Enumerator enumerator2 = namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics.GetEnumerator();
					while (enumerator2.MoveNext())
					{
						if (TypeSymbolExtensions.<HasCustomModifiers>g__checkTypeWithAnnotations|121_0(enumerator2.Current, flagNonDefaultArraySizesOrLowerBounds))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060079A7 RID: 31143 RVA: 0x001BD651 File Offset: 0x001BB851
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static bool CanUnifyWith(this TypeSymbol thisType, TypeSymbol otherType)
		{
			return TypeUnification.CanUnify(thisType, otherType);
		}

		// Token: 0x060079A8 RID: 31144 RVA: 0x001BD65C File Offset: 0x001BB85C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static TypeSymbol GetNextBaseTypeNoUseSiteDiagnostics(this TypeSymbol type, ConsList<TypeSymbol> basesBeingResolved, CSharpCompilation compilation, ref PooledHashSet<NamedTypeSymbol> visited)
		{
			TypeKind typeKind = type.TypeKind;
			if (typeKind != TypeKind.Class)
			{
				switch (typeKind)
				{
				case TypeKind.Error:
				case TypeKind.Interface:
				case TypeKind.Struct:
					goto IL_0039;
				case TypeKind.TypeParameter:
					return ((TypeParameterSymbol)type).EffectiveBaseClassNoUseSiteDiagnostics;
				}
				return type.BaseTypeNoUseSiteDiagnostics;
			}
			IL_0039:
			return TypeSymbolExtensions.GetNextDeclaredBase((NamedTypeSymbol)type, basesBeingResolved, compilation, ref visited);
		}

		// Token: 0x060079A9 RID: 31145 RVA: 0x001BD6B8 File Offset: 0x001BB8B8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static TypeSymbol GetNextDeclaredBase(NamedTypeSymbol type, ConsList<TypeSymbol> basesBeingResolved, CSharpCompilation compilation, ref PooledHashSet<NamedTypeSymbol> visited)
		{
			if (basesBeingResolved != null && basesBeingResolved.ContainsReference(type.OriginalDefinition))
			{
				return null;
			}
			if (type.SpecialType == SpecialType.System_Object)
			{
				type.SetKnownToHaveNoDeclaredBaseCycles();
				return null;
			}
			NamedTypeSymbol declaredBaseType = type.GetDeclaredBaseType(basesBeingResolved);
			if (declaredBaseType == null)
			{
				TypeSymbolExtensions.SetKnownToHaveNoDeclaredBaseCycles(ref visited);
				return TypeSymbolExtensions.GetDefaultBaseOrNull(type, compilation);
			}
			NamedTypeSymbol originalDefinition = type.OriginalDefinition;
			if (declaredBaseType.KnownToHaveNoDeclaredBaseCycles)
			{
				originalDefinition.SetKnownToHaveNoDeclaredBaseCycles();
				TypeSymbolExtensions.SetKnownToHaveNoDeclaredBaseCycles(ref visited);
			}
			else
			{
				visited = visited ?? PooledHashSet<NamedTypeSymbol>.GetInstance();
				visited.Add(originalDefinition);
				if (visited.Contains(declaredBaseType.OriginalDefinition))
				{
					return TypeSymbolExtensions.GetDefaultBaseOrNull(type, compilation);
				}
			}
			return declaredBaseType;
		}

		// Token: 0x060079AA RID: 31146 RVA: 0x001BD750 File Offset: 0x001BB950
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static void SetKnownToHaveNoDeclaredBaseCycles(ref PooledHashSet<NamedTypeSymbol> visited)
		{
			if (visited != null)
			{
				foreach (NamedTypeSymbol namedTypeSymbol in visited)
				{
					namedTypeSymbol.SetKnownToHaveNoDeclaredBaseCycles();
				}
				visited.Free();
				visited = null;
			}
		}

		// Token: 0x060079AB RID: 31147 RVA: 0x001BD7AC File Offset: 0x001BB9AC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static NamedTypeSymbol GetDefaultBaseOrNull(NamedTypeSymbol type, CSharpCompilation compilation)
		{
			if (compilation == null)
			{
				return null;
			}
			TypeKind typeKind = type.TypeKind;
			if (typeKind != TypeKind.Class)
			{
				switch (typeKind)
				{
				case TypeKind.Error:
					goto IL_002E;
				case TypeKind.Interface:
					return null;
				case TypeKind.Struct:
					return compilation.Assembly.GetSpecialType(SpecialType.System_ValueType);
				}
				throw ExceptionUtilities.UnexpectedValue(type.TypeKind);
			}
			IL_002E:
			return compilation.Assembly.GetSpecialType(SpecialType.System_Object);
		}

		// Token: 0x060079AD RID: 31149 RVA: 0x001BD8A0 File Offset: 0x001BBAA0
		[CompilerGenerated]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		internal static ValueTuple<TypeWithAnnotations, TypeSymbol> <VisitType>g__getNextIterationElements|54_0<T>(TypeWithAnnotations type, bool canDigThroughNullable)
		{
			if (!canDigThroughNullable)
			{
				return new ValueTuple<TypeWithAnnotations, TypeSymbol>(type, null);
			}
			return new ValueTuple<TypeWithAnnotations, TypeSymbol>(default(TypeWithAnnotations), type.NullableUnderlyingTypeOrSelf);
		}

		// Token: 0x060079AE RID: 31150 RVA: 0x001BD8D0 File Offset: 0x001BBAD0
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static TypeSymbol <VisitType>g__visitFunctionPointerType|54_1<T>([global::System.Runtime.CompilerServices.Nullable(1)] FunctionPointerTypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0 })] Func<TypeWithAnnotations, T, bool, bool> typeWithAnnotationsPredicate, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 0 })] Func<TypeSymbol, T, bool, bool> typePredicate, T arg, bool useDefaultType, bool canDigThroughNullable, out TypeWithAnnotations next)
		{
			MethodSymbol signature = type.Signature;
			if (signature.ParameterCount == 0)
			{
				next = signature.ReturnTypeWithAnnotations;
				return null;
			}
			TypeSymbol typeSymbol = (canDigThroughNullable ? default(TypeWithAnnotations) : signature.ReturnTypeWithAnnotations).VisitType(canDigThroughNullable ? signature.ReturnTypeWithAnnotations.NullableUnderlyingTypeOrSelf : null, typeWithAnnotationsPredicate, typePredicate, arg, canDigThroughNullable, useDefaultType);
			if (typeSymbol != null)
			{
				next = default(TypeWithAnnotations);
				return typeSymbol;
			}
			int i;
			for (i = 0; i < signature.ParameterCount - 1; i++)
			{
				ValueTuple<TypeWithAnnotations, TypeSymbol> valueTuple = TypeSymbolExtensions.<VisitType>g__getNextIterationElements|54_0<T>(signature.Parameters[i].TypeWithAnnotations, canDigThroughNullable);
				TypeWithAnnotations item = valueTuple.Item1;
				TypeSymbol item2 = valueTuple.Item2;
				typeSymbol = item.VisitType(item2, typeWithAnnotationsPredicate, typePredicate, arg, canDigThroughNullable, useDefaultType);
				if (typeSymbol != null)
				{
					next = default(TypeWithAnnotations);
					return typeSymbol;
				}
			}
			next = signature.Parameters[i].TypeWithAnnotations;
			return null;
		}

		// Token: 0x060079AF RID: 31151 RVA: 0x001BD9B8 File Offset: 0x001BBBB8
		[CompilerGenerated]
		internal static bool <ComputeHashCode>g__wasConstructedForAnnotations|85_0(NamedTypeSymbol type)
		{
			for (;;)
			{
				ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotationsNoUseSiteDiagnostics = type.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics;
				ImmutableArray<TypeParameterSymbol> typeParameters = type.OriginalDefinition.TypeParameters;
				for (int i = 0; i < typeArgumentsWithAnnotationsNoUseSiteDiagnostics.Length; i++)
				{
					if (!typeParameters[i].Equals(typeArgumentsWithAnnotationsNoUseSiteDiagnostics[i].Type.OriginalDefinition, TypeCompareKind.ConsiderEverything))
					{
						return false;
					}
				}
				type = type.ContainingType;
				if (type == null || type.IsDefinition)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060079B0 RID: 31152 RVA: 0x001BDA25 File Offset: 0x001BBC25
		[CompilerGenerated]
		internal static void <GetTypeRefWithAttributes>g__addIfNotNull|108_0(ArrayBuilder<ICustomAttribute> builder, [global::System.Runtime.CompilerServices.Nullable(2)] SynthesizedAttributeData attr)
		{
			if (attr != null)
			{
				builder.Add(attr);
			}
		}

		// Token: 0x060079B1 RID: 31153 RVA: 0x001BDA34 File Offset: 0x001BBC34
		[CompilerGenerated]
		internal static int <CustomModifierCount>g__customModifierCountForTypeWithAnnotations|120_0(TypeWithAnnotations typeWithAnnotations)
		{
			return typeWithAnnotations.CustomModifiers.Length + typeWithAnnotations.Type.CustomModifierCount();
		}

		// Token: 0x060079B2 RID: 31154 RVA: 0x001BDA5D File Offset: 0x001BBC5D
		[CompilerGenerated]
		internal static bool <HasCustomModifiers>g__checkTypeWithAnnotations|121_0(TypeWithAnnotations typeWithAnnotations, bool flagNonDefaultArraySizesOrLowerBounds)
		{
			return typeWithAnnotations.CustomModifiers.Any<CustomModifier>() || typeWithAnnotations.Type.HasCustomModifiers(flagNonDefaultArraySizesOrLowerBounds);
		}

		// Token: 0x04001D1A RID: 7450
		private static readonly string[] s_expressionsNamespaceName = new string[] { "Expressions", "Linq", "System", "" };

		// Token: 0x04001D1B RID: 7451
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 2 })]
		private static readonly Func<TypeSymbol, TypeParameterSymbol, bool, bool> s_containsTypeParameterPredicate = (TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterSymbol parameter, bool unused) => type.TypeKind == TypeKind.TypeParameter && (parameter == null || TypeSymbol.Equals(type, parameter, TypeCompareKind.ConsiderEverything));

		// Token: 0x04001D1C RID: 7452
		private static readonly Func<TypeSymbol, Symbol, bool, bool> s_isTypeParameterWithSpecificContainerPredicate = (TypeSymbol type, Symbol parameterContainer, bool unused) => type.TypeKind == TypeKind.TypeParameter && type.ContainingSymbol == parameterContainer;

		// Token: 0x04001D1D RID: 7453
		private static readonly Func<TypeSymbol, HashSet<TypeParameterSymbol>, bool, bool> s_containsTypeParametersPredicate = (TypeSymbol type, HashSet<TypeParameterSymbol> parameters, bool unused) => type.TypeKind == TypeKind.TypeParameter && parameters.Contains((TypeParameterSymbol)type);

		// Token: 0x04001D1E RID: 7454
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 2 })]
		private static readonly Func<TypeSymbol, object, bool, bool> s_containsDynamicPredicate = (TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] object unused1, bool unused2) => type.TypeKind == TypeKind.Dynamic;
	}
}
