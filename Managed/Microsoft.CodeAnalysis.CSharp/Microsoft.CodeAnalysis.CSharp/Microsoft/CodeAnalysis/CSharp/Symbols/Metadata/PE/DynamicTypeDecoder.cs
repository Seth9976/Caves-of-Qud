using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005E1 RID: 1505
	internal struct DynamicTypeDecoder
	{
		// Token: 0x06007D9F RID: 32159 RVA: 0x001C36C7 File Offset: 0x001C18C7
		private DynamicTypeDecoder(ImmutableArray<bool> dynamicTransformFlags, bool haveCustomModifierFlags, bool checkLength, AssemblySymbol containingAssembly)
		{
			this._dynamicTransformFlags = dynamicTransformFlags;
			this._containingAssembly = containingAssembly;
			this._haveCustomModifierFlags = haveCustomModifierFlags;
			this._checkLength = checkLength;
			this._index = 0;
		}

		// Token: 0x06007DA0 RID: 32160 RVA: 0x001C36F0 File Offset: 0x001C18F0
		internal static TypeSymbol TransformType(TypeSymbol metadataType, int targetSymbolCustomModifierCount, EntityHandle targetSymbolToken, PEModuleSymbol containingModule, RefKind targetSymbolRefKind = RefKind.None)
		{
			ImmutableArray<bool> immutableArray;
			if (containingModule.Module.HasDynamicAttribute(targetSymbolToken, out immutableArray))
			{
				return DynamicTypeDecoder.TransformTypeInternal(metadataType, containingModule.ContainingAssembly, targetSymbolCustomModifierCount, targetSymbolRefKind, immutableArray, true, true);
			}
			return metadataType;
		}

		// Token: 0x06007DA1 RID: 32161 RVA: 0x001C3721 File Offset: 0x001C1921
		internal static TypeSymbol TransformTypeWithoutCustomModifierFlags(TypeSymbol type, AssemblySymbol containingAssembly, RefKind targetSymbolRefKind, ImmutableArray<bool> dynamicTransformFlags, bool checkLength = true)
		{
			return DynamicTypeDecoder.TransformTypeInternal(type, containingAssembly, 0, targetSymbolRefKind, dynamicTransformFlags, false, checkLength);
		}

		// Token: 0x06007DA2 RID: 32162 RVA: 0x001C3730 File Offset: 0x001C1930
		private static TypeSymbol TransformTypeInternal(TypeSymbol metadataType, AssemblySymbol containingAssembly, int targetSymbolCustomModifierCount, RefKind targetSymbolRefKind, ImmutableArray<bool> dynamicTransformFlags, bool haveCustomModifierFlags, bool checkLength)
		{
			if (dynamicTransformFlags.Length == 0)
			{
				return new UnsupportedMetadataTypeSymbol(null);
			}
			DynamicTypeDecoder dynamicTypeDecoder = new DynamicTypeDecoder(dynamicTransformFlags, haveCustomModifierFlags, checkLength, containingAssembly);
			if (dynamicTypeDecoder.HandleCustomModifiers(targetSymbolCustomModifierCount) && dynamicTypeDecoder.HandleRefKind(targetSymbolRefKind))
			{
				TypeSymbol typeSymbol = dynamicTypeDecoder.TransformType(metadataType);
				if (typeSymbol != null && (!checkLength || dynamicTypeDecoder._index == dynamicTransformFlags.Length))
				{
					return typeSymbol;
				}
			}
			return metadataType;
		}

		// Token: 0x06007DA3 RID: 32163 RVA: 0x001C3794 File Offset: 0x001C1994
		private TypeSymbol TransformType(TypeSymbol type)
		{
			if (!this.HasFlag || (this.PeekFlag() && type.SpecialType != SpecialType.System_Object && !type.IsDynamic()))
			{
				return null;
			}
			SymbolKind kind = type.Kind;
			if (kind <= SymbolKind.NamedType)
			{
				switch (kind)
				{
				case SymbolKind.ArrayType:
					return this.TransformArrayType((ArrayTypeSymbol)type);
				case SymbolKind.Assembly:
					goto IL_00BF;
				case SymbolKind.DynamicType:
					if (!this.ConsumeFlag())
					{
						return this._containingAssembly.GetSpecialType(SpecialType.System_Object);
					}
					return type;
				case SymbolKind.ErrorType:
					break;
				default:
					if (kind != SymbolKind.NamedType)
					{
						goto IL_00BF;
					}
					break;
				}
				if (type.SpecialType != SpecialType.System_Object)
				{
					return this.TransformNamedType((NamedTypeSymbol)type, false);
				}
				if (!this.ConsumeFlag())
				{
					return type;
				}
				return DynamicTypeSymbol.Instance;
			}
			else
			{
				if (kind == SymbolKind.PointerType)
				{
					return this.TransformPointerType((PointerTypeSymbol)type);
				}
				if (kind == SymbolKind.FunctionPointerType)
				{
					return this.TransformFunctionPointerType((FunctionPointerTypeSymbol)type);
				}
			}
			IL_00BF:
			this.ConsumeFlag();
			if (!this.HandleCustomModifiers(type.CustomModifierCount()))
			{
				return null;
			}
			return type;
		}

		// Token: 0x06007DA4 RID: 32164 RVA: 0x001C3878 File Offset: 0x001C1A78
		private bool HandleCustomModifiers(int customModifiersCount)
		{
			if (!this._haveCustomModifierFlags)
			{
				return true;
			}
			for (int i = 0; i < customModifiersCount; i++)
			{
				if (!this.HasFlag || this.ConsumeFlag())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06007DA5 RID: 32165 RVA: 0x001C38AE File Offset: 0x001C1AAE
		private bool HandleRefKind(RefKind refKind)
		{
			return refKind == RefKind.None || !this.ConsumeFlag();
		}

		// Token: 0x06007DA6 RID: 32166 RVA: 0x001C38C0 File Offset: 0x001C1AC0
		private NamedTypeSymbol TransformNamedType(NamedTypeSymbol namedType, bool isContaining = false)
		{
			if (!isContaining)
			{
				this.ConsumeFlag();
			}
			NamedTypeSymbol containingType = namedType.ContainingType;
			NamedTypeSymbol namedTypeSymbol;
			if (containingType != null && containingType.IsGenericType)
			{
				namedTypeSymbol = this.TransformNamedType(namedType.ContainingType, true);
				if (namedTypeSymbol == null)
				{
					return null;
				}
			}
			else
			{
				namedTypeSymbol = containingType;
			}
			ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotationsNoUseSiteDiagnostics = namedType.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics;
			ImmutableArray<TypeWithAnnotations> immutableArray = this.TransformTypeArguments(typeArgumentsWithAnnotationsNoUseSiteDiagnostics);
			if (immutableArray.IsDefault)
			{
				return null;
			}
			bool flag = !TypeSymbol.Equals(namedTypeSymbol, containingType, TypeCompareKind.ConsiderEverything);
			if (!flag && !(immutableArray != typeArgumentsWithAnnotationsNoUseSiteDiagnostics))
			{
				return namedType;
			}
			if (flag)
			{
				namedType = namedType.OriginalDefinition.AsMember(namedTypeSymbol);
				return namedType.ConstructIfGeneric(immutableArray);
			}
			return namedType.ConstructedFrom.Construct(immutableArray, false).WithTupleDataFrom(namedType);
		}

		// Token: 0x06007DA7 RID: 32167 RVA: 0x001C3964 File Offset: 0x001C1B64
		private ImmutableArray<TypeWithAnnotations> TransformTypeArguments(ImmutableArray<TypeWithAnnotations> typeArguments)
		{
			if (!typeArguments.Any<TypeWithAnnotations>())
			{
				return typeArguments;
			}
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			bool flag = false;
			foreach (TypeWithAnnotations typeWithAnnotations in typeArguments)
			{
				TypeSymbol typeSymbol = this.TransformType(typeWithAnnotations.Type);
				if (typeSymbol == null)
				{
					instance.Free();
					return default(ImmutableArray<TypeWithAnnotations>);
				}
				instance.Add(typeWithAnnotations.WithTypeAndModifiers(typeSymbol, typeWithAnnotations.CustomModifiers));
				flag |= !TypeSymbol.Equals(typeSymbol, typeWithAnnotations.Type, TypeCompareKind.ConsiderEverything);
			}
			if (!flag)
			{
				instance.Free();
				return typeArguments;
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06007DA8 RID: 32168 RVA: 0x001C3A00 File Offset: 0x001C1C00
		private ArrayTypeSymbol TransformArrayType(ArrayTypeSymbol arrayType)
		{
			this.ConsumeFlag();
			if (!this.HandleCustomModifiers(arrayType.ElementTypeWithAnnotations.CustomModifiers.Length))
			{
				return null;
			}
			TypeSymbol typeSymbol = this.TransformType(arrayType.ElementType);
			if (typeSymbol == null)
			{
				return null;
			}
			if (TypeSymbol.Equals(typeSymbol, arrayType.ElementType, TypeCompareKind.ConsiderEverything))
			{
				return arrayType;
			}
			if (!arrayType.IsSZArray)
			{
				return ArrayTypeSymbol.CreateMDArray(this._containingAssembly, arrayType.ElementTypeWithAnnotations.WithTypeAndModifiers(typeSymbol, arrayType.ElementTypeWithAnnotations.CustomModifiers), arrayType.Rank, arrayType.Sizes, arrayType.LowerBounds);
			}
			return ArrayTypeSymbol.CreateSZArray(this._containingAssembly, arrayType.ElementTypeWithAnnotations.WithTypeAndModifiers(typeSymbol, arrayType.ElementTypeWithAnnotations.CustomModifiers));
		}

		// Token: 0x06007DA9 RID: 32169 RVA: 0x001C3AC4 File Offset: 0x001C1CC4
		private PointerTypeSymbol TransformPointerType(PointerTypeSymbol pointerType)
		{
			this.ConsumeFlag();
			if (!this.HandleCustomModifiers(pointerType.PointedAtTypeWithAnnotations.CustomModifiers.Length))
			{
				return null;
			}
			TypeSymbol typeSymbol = this.TransformType(pointerType.PointedAtType);
			if (typeSymbol == null)
			{
				return null;
			}
			if (!TypeSymbol.Equals(typeSymbol, pointerType.PointedAtType, TypeCompareKind.ConsiderEverything))
			{
				return new PointerTypeSymbol(pointerType.PointedAtTypeWithAnnotations.WithTypeAndModifiers(typeSymbol, pointerType.PointedAtTypeWithAnnotations.CustomModifiers));
			}
			return pointerType;
		}

		// Token: 0x06007DAA RID: 32170 RVA: 0x001C3B40 File Offset: 0x001C1D40
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private FunctionPointerTypeSymbol TransformFunctionPointerType(FunctionPointerTypeSymbol type)
		{
			this.ConsumeFlag();
			FunctionPointerMethodSymbol signature = type.Signature;
			ValueTuple<TypeWithAnnotations, bool> valueTuple = DynamicTypeDecoder.<TransformFunctionPointerType>g__handle|16_0(ref this, signature.RefKind, signature.RefCustomModifiers, signature.ReturnTypeWithAnnotations);
			TypeWithAnnotations item = valueTuple.Item1;
			bool flag = valueTuple.Item2;
			if (item.IsDefault)
			{
				return null;
			}
			ImmutableArray<TypeWithAnnotations> immutableArray = ImmutableArray<TypeWithAnnotations>.Empty;
			if (signature.ParameterCount > 0)
			{
				bool flag2 = false;
				ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(signature.ParameterCount);
				try
				{
					foreach (ParameterSymbol parameterSymbol in signature.Parameters)
					{
						ValueTuple<TypeWithAnnotations, bool> valueTuple2 = DynamicTypeDecoder.<TransformFunctionPointerType>g__handle|16_0(ref this, parameterSymbol.RefKind, parameterSymbol.RefCustomModifiers, parameterSymbol.TypeWithAnnotations);
						TypeWithAnnotations item2 = valueTuple2.Item1;
						bool item3 = valueTuple2.Item2;
						if (item2.IsDefault)
						{
							return null;
						}
						instance.Add(item2);
						flag2 = flag2 || item3;
					}
					immutableArray = (flag2 ? instance.ToImmutable() : signature.ParameterTypesWithAnnotations);
					flag = flag || flag2;
				}
				finally
				{
					instance.Free();
				}
			}
			if (flag)
			{
				return type.SubstituteTypeSymbol(item, immutableArray, default(ImmutableArray<CustomModifier>), default(ImmutableArray<ImmutableArray<CustomModifier>>));
			}
			return type;
		}

		// Token: 0x1700251E RID: 9502
		// (get) Token: 0x06007DAB RID: 32171 RVA: 0x001C3C74 File Offset: 0x001C1E74
		private bool HasFlag
		{
			get
			{
				return this._index < this._dynamicTransformFlags.Length || !this._checkLength;
			}
		}

		// Token: 0x06007DAC RID: 32172 RVA: 0x001C3CA4 File Offset: 0x001C1EA4
		private bool PeekFlag()
		{
			return this._index < this._dynamicTransformFlags.Length && this._dynamicTransformFlags[this._index];
		}

		// Token: 0x06007DAD RID: 32173 RVA: 0x001C3CDD File Offset: 0x001C1EDD
		private bool ConsumeFlag()
		{
			bool flag = this.PeekFlag();
			this._index++;
			return flag;
		}

		// Token: 0x06007DAE RID: 32174 RVA: 0x001C3CF4 File Offset: 0x001C1EF4
		[CompilerGenerated]
		[return: TupleElementNames(new string[] { null, "madeChanges" })]
		internal static ValueTuple<TypeWithAnnotations, bool> <TransformFunctionPointerType>g__handle|16_0(ref DynamicTypeDecoder decoder, RefKind refKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> refCustomModifiers, TypeWithAnnotations typeWithAnnotations)
		{
			if (!decoder.HandleCustomModifiers(refCustomModifiers.Length) || !decoder.HandleRefKind(refKind) || !decoder.HandleCustomModifiers(typeWithAnnotations.CustomModifiers.Length))
			{
				return new ValueTuple<TypeWithAnnotations, bool>(default(TypeWithAnnotations), false);
			}
			TypeSymbol typeSymbol = decoder.TransformType(typeWithAnnotations.Type);
			if (typeSymbol == null)
			{
				return new ValueTuple<TypeWithAnnotations, bool>(default(TypeWithAnnotations), false);
			}
			if (typeSymbol.Equals(typeWithAnnotations.Type, TypeCompareKind.ConsiderEverything))
			{
				return new ValueTuple<TypeWithAnnotations, bool>(typeWithAnnotations, false);
			}
			return new ValueTuple<TypeWithAnnotations, bool>(typeWithAnnotations.WithType(typeSymbol), true);
		}

		// Token: 0x04001D9C RID: 7580
		private readonly ImmutableArray<bool> _dynamicTransformFlags;

		// Token: 0x04001D9D RID: 7581
		private readonly AssemblySymbol _containingAssembly;

		// Token: 0x04001D9E RID: 7582
		private readonly bool _haveCustomModifierFlags;

		// Token: 0x04001D9F RID: 7583
		private readonly bool _checkLength;

		// Token: 0x04001DA0 RID: 7584
		private int _index;
	}
}
