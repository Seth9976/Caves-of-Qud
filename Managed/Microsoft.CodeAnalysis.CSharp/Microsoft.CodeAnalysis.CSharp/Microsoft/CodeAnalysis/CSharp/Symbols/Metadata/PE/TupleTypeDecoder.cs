using System;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005F3 RID: 1523
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal struct TupleTypeDecoder
	{
		// Token: 0x06007FE9 RID: 32745 RVA: 0x001CC99E File Offset: 0x001CAB9E
		private TupleTypeDecoder([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<string> elementNames)
		{
			this._elementNames = elementNames;
			this._namesIndex = (elementNames.IsDefault ? 0 : elementNames.Length);
			this._decodingFailed = false;
			this._foundUsableErrorType = false;
		}

		// Token: 0x06007FEA RID: 32746 RVA: 0x001CC9D0 File Offset: 0x001CABD0
		public static TypeSymbol DecodeTupleTypesIfApplicable(TypeSymbol metadataType, EntityHandle targetHandle, PEModuleSymbol containingModule)
		{
			ImmutableArray<string> immutableArray;
			bool flag = containingModule.Module.HasTupleElementNamesAttribute(targetHandle, out immutableArray);
			if (flag && immutableArray.IsDefaultOrEmpty)
			{
				return new UnsupportedMetadataTypeSymbol(null);
			}
			return TupleTypeDecoder.DecodeTupleTypesInternal(metadataType, immutableArray, flag);
		}

		// Token: 0x06007FEB RID: 32747 RVA: 0x001CCA08 File Offset: 0x001CAC08
		public static TypeWithAnnotations DecodeTupleTypesIfApplicable(TypeWithAnnotations metadataType, EntityHandle targetHandle, PEModuleSymbol containingModule)
		{
			ImmutableArray<string> immutableArray;
			bool flag = containingModule.Module.HasTupleElementNamesAttribute(targetHandle, out immutableArray);
			if (flag && immutableArray.IsDefaultOrEmpty)
			{
				return TypeWithAnnotations.Create(new UnsupportedMetadataTypeSymbol(null), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}
			TypeSymbol type = metadataType.Type;
			TypeSymbol typeSymbol = TupleTypeDecoder.DecodeTupleTypesInternal(type, immutableArray, flag);
			if (typeSymbol != type)
			{
				return TypeWithAnnotations.Create(typeSymbol, metadataType.NullableAnnotation, metadataType.CustomModifiers);
			}
			return metadataType;
		}

		// Token: 0x06007FEC RID: 32748 RVA: 0x001CCA71 File Offset: 0x001CAC71
		public static TypeSymbol DecodeTupleTypesIfApplicable(TypeSymbol metadataType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<string> elementNames)
		{
			return TupleTypeDecoder.DecodeTupleTypesInternal(metadataType, elementNames, !elementNames.IsDefaultOrEmpty);
		}

		// Token: 0x06007FED RID: 32749 RVA: 0x001CCA84 File Offset: 0x001CAC84
		private static TypeSymbol DecodeTupleTypesInternal(TypeSymbol metadataType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<string> elementNames, bool hasTupleElementNamesAttribute)
		{
			TupleTypeDecoder tupleTypeDecoder = new TupleTypeDecoder(elementNames);
			TypeSymbol typeSymbol = tupleTypeDecoder.DecodeType(metadataType);
			if (!tupleTypeDecoder._decodingFailed && (!hasTupleElementNamesAttribute || tupleTypeDecoder._namesIndex == 0))
			{
				return typeSymbol;
			}
			if (tupleTypeDecoder._foundUsableErrorType)
			{
				return metadataType;
			}
			return new UnsupportedMetadataTypeSymbol(null);
		}

		// Token: 0x06007FEE RID: 32750 RVA: 0x001CCAC8 File Offset: 0x001CACC8
		private TypeSymbol DecodeType(TypeSymbol type)
		{
			SymbolKind kind = type.Kind;
			if (kind <= SymbolKind.NamedType)
			{
				switch (kind)
				{
				case SymbolKind.ArrayType:
					return this.DecodeArrayType((ArrayTypeSymbol)type);
				case SymbolKind.Assembly:
					goto IL_007B;
				case SymbolKind.DynamicType:
					break;
				case SymbolKind.ErrorType:
					this._foundUsableErrorType = true;
					return type;
				default:
					if (kind != SymbolKind.NamedType)
					{
						goto IL_007B;
					}
					return this.DecodeNamedType((NamedTypeSymbol)type);
				}
			}
			else
			{
				if (kind == SymbolKind.PointerType)
				{
					return this.DecodePointerType((PointerTypeSymbol)type);
				}
				if (kind != SymbolKind.TypeParameter)
				{
					if (kind != SymbolKind.FunctionPointerType)
					{
						goto IL_007B;
					}
					return this.DecodeFunctionPointerType((FunctionPointerTypeSymbol)type);
				}
			}
			return type;
			IL_007B:
			throw ExceptionUtilities.UnexpectedValue(type.TypeKind);
		}

		// Token: 0x06007FEF RID: 32751 RVA: 0x001CCB60 File Offset: 0x001CAD60
		private PointerTypeSymbol DecodePointerType(PointerTypeSymbol type)
		{
			return type.WithPointedAtType(this.DecodeTypeInternal(type.PointedAtTypeWithAnnotations));
		}

		// Token: 0x06007FF0 RID: 32752 RVA: 0x001CCB74 File Offset: 0x001CAD74
		private FunctionPointerTypeSymbol DecodeFunctionPointerType(FunctionPointerTypeSymbol type)
		{
			ImmutableArray<TypeWithAnnotations> immutableArray = ImmutableArray<TypeWithAnnotations>.Empty;
			bool flag = false;
			if (type.Signature.ParameterCount > 0)
			{
				ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(type.Signature.ParameterCount);
				for (int i = type.Signature.ParameterCount - 1; i >= 0; i--)
				{
					ParameterSymbol parameterSymbol = type.Signature.Parameters[i];
					TypeWithAnnotations typeWithAnnotations = this.DecodeTypeInternal(parameterSymbol.TypeWithAnnotations);
					flag = flag || !typeWithAnnotations.IsSameAs(parameterSymbol.TypeWithAnnotations);
					instance.Add(typeWithAnnotations);
				}
				if (flag)
				{
					instance.ReverseContents();
					immutableArray = instance.ToImmutableAndFree();
				}
				else
				{
					immutableArray = type.Signature.ParameterTypesWithAnnotations;
					instance.Free();
				}
			}
			TypeWithAnnotations typeWithAnnotations2 = this.DecodeTypeInternal(type.Signature.ReturnTypeWithAnnotations);
			if (flag || !typeWithAnnotations2.IsSameAs(type.Signature.ReturnTypeWithAnnotations))
			{
				return type.SubstituteTypeSymbol(typeWithAnnotations2, immutableArray, default(ImmutableArray<CustomModifier>), default(ImmutableArray<ImmutableArray<CustomModifier>>));
			}
			return type;
		}

		// Token: 0x06007FF1 RID: 32753 RVA: 0x001CCC7C File Offset: 0x001CAE7C
		private NamedTypeSymbol DecodeNamedType(NamedTypeSymbol type)
		{
			ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotationsNoUseSiteDiagnostics = type.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics;
			ImmutableArray<TypeWithAnnotations> immutableArray = this.DecodeTypeArguments(typeArgumentsWithAnnotationsNoUseSiteDiagnostics);
			NamedTypeSymbol namedTypeSymbol = type;
			NamedTypeSymbol containingType = type.ContainingType;
			NamedTypeSymbol namedTypeSymbol2;
			if (containingType != null && containingType.IsGenericType)
			{
				namedTypeSymbol2 = this.DecodeNamedType(containingType);
			}
			else
			{
				namedTypeSymbol2 = containingType;
			}
			bool flag = namedTypeSymbol2 != containingType;
			if (typeArgumentsWithAnnotationsNoUseSiteDiagnostics != immutableArray || flag)
			{
				if (flag)
				{
					namedTypeSymbol = namedTypeSymbol.OriginalDefinition.AsMember(namedTypeSymbol2);
					return namedTypeSymbol.ConstructIfGeneric(immutableArray);
				}
				namedTypeSymbol = type.ConstructedFrom.Construct(immutableArray, false);
			}
			if (namedTypeSymbol.IsTupleType)
			{
				int length = namedTypeSymbol.TupleElementTypesWithAnnotations.Length;
				if (length > 0)
				{
					ImmutableArray<string> immutableArray2 = this.EatElementNamesIfAvailable(length);
					namedTypeSymbol = NamedTypeSymbol.CreateTuple(namedTypeSymbol, immutableArray2, default(ImmutableArray<bool>), default(ImmutableArray<Location>), default(ImmutableArray<Location>));
				}
			}
			return namedTypeSymbol;
		}

		// Token: 0x06007FF2 RID: 32754 RVA: 0x001CCD48 File Offset: 0x001CAF48
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private ImmutableArray<TypeWithAnnotations> DecodeTypeArguments(ImmutableArray<TypeWithAnnotations> typeArgs)
		{
			if (typeArgs.IsEmpty)
			{
				return typeArgs;
			}
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(typeArgs.Length);
			bool flag = false;
			for (int i = typeArgs.Length - 1; i >= 0; i--)
			{
				TypeWithAnnotations typeWithAnnotations = typeArgs[i];
				TypeWithAnnotations typeWithAnnotations2 = this.DecodeTypeInternal(typeWithAnnotations);
				flag |= !typeWithAnnotations2.IsSameAs(typeWithAnnotations);
				instance.Add(typeWithAnnotations2);
			}
			if (!flag)
			{
				instance.Free();
				return typeArgs;
			}
			instance.ReverseContents();
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06007FF3 RID: 32755 RVA: 0x001CCDC4 File Offset: 0x001CAFC4
		private ArrayTypeSymbol DecodeArrayType(ArrayTypeSymbol type)
		{
			TypeWithAnnotations typeWithAnnotations = this.DecodeTypeInternal(type.ElementTypeWithAnnotations);
			return type.WithElementType(typeWithAnnotations);
		}

		// Token: 0x06007FF4 RID: 32756 RVA: 0x001CCDE8 File Offset: 0x001CAFE8
		private TypeWithAnnotations DecodeTypeInternal(TypeWithAnnotations typeWithAnnotations)
		{
			TypeSymbol type = typeWithAnnotations.Type;
			TypeSymbol typeSymbol = this.DecodeType(type);
			if (typeSymbol != type)
			{
				return TypeWithAnnotations.Create(typeSymbol, typeWithAnnotations.NullableAnnotation, typeWithAnnotations.CustomModifiers);
			}
			return typeWithAnnotations;
		}

		// Token: 0x06007FF5 RID: 32757 RVA: 0x001CCE20 File Offset: 0x001CB020
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		private ImmutableArray<string> EatElementNamesIfAvailable(int numberOfElements)
		{
			if (this._elementNames.IsDefault)
			{
				return this._elementNames;
			}
			if (numberOfElements > this._namesIndex)
			{
				this._namesIndex = 0;
				this._decodingFailed = true;
				return default(ImmutableArray<string>);
			}
			int num = this._namesIndex - numberOfElements;
			this._namesIndex = num;
			bool flag = true;
			for (int i = 0; i < numberOfElements; i++)
			{
				if (this._elementNames[num + i] != null)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				return default(ImmutableArray<string>);
			}
			return ImmutableArray.Create<string>(this._elementNames, num, numberOfElements);
		}

		// Token: 0x04001E29 RID: 7721
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		private readonly ImmutableArray<string> _elementNames;

		// Token: 0x04001E2A RID: 7722
		private int _namesIndex;

		// Token: 0x04001E2B RID: 7723
		private bool _foundUsableErrorType;

		// Token: 0x04001E2C RID: 7724
		private bool _decodingFailed;
	}
}
