using System;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005E4 RID: 1508
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal struct NativeIntegerTypeDecoder
	{
		// Token: 0x06007DD1 RID: 32209 RVA: 0x001C49C8 File Offset: 0x001C2BC8
		internal static TypeSymbol TransformType(TypeSymbol type, EntityHandle handle, PEModuleSymbol containingModule)
		{
			ImmutableArray<bool> immutableArray;
			if (!containingModule.Module.HasNativeIntegerAttribute(handle, out immutableArray))
			{
				return type;
			}
			return NativeIntegerTypeDecoder.TransformType(type, immutableArray);
		}

		// Token: 0x06007DD2 RID: 32210 RVA: 0x001C49F0 File Offset: 0x001C2BF0
		internal static TypeSymbol TransformType(TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<bool> transformFlags)
		{
			NativeIntegerTypeDecoder nativeIntegerTypeDecoder = new NativeIntegerTypeDecoder(transformFlags);
			TypeSymbol typeSymbol2;
			try
			{
				TypeSymbol typeSymbol = nativeIntegerTypeDecoder.TransformType(type);
				if (nativeIntegerTypeDecoder._index == transformFlags.Length)
				{
					typeSymbol2 = typeSymbol;
				}
				else
				{
					typeSymbol2 = new UnsupportedMetadataTypeSymbol(null);
				}
			}
			catch (UnsupportedSignatureContent)
			{
				typeSymbol2 = new UnsupportedMetadataTypeSymbol(null);
			}
			catch (NativeIntegerTypeDecoder.ErrorTypeException)
			{
				typeSymbol2 = type;
			}
			return typeSymbol2;
		}

		// Token: 0x06007DD3 RID: 32211 RVA: 0x001C4A58 File Offset: 0x001C2C58
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private NativeIntegerTypeDecoder(ImmutableArray<bool> transformFlags)
		{
			this._transformFlags = transformFlags;
			this._index = 0;
		}

		// Token: 0x06007DD4 RID: 32212 RVA: 0x001C4A68 File Offset: 0x001C2C68
		private TypeWithAnnotations TransformTypeWithAnnotations(TypeWithAnnotations type)
		{
			return type.WithTypeAndModifiers(this.TransformType(type.Type), type.CustomModifiers);
		}

		// Token: 0x06007DD5 RID: 32213 RVA: 0x001C4A88 File Offset: 0x001C2C88
		private TypeSymbol TransformType(TypeSymbol type)
		{
			switch (type.TypeKind)
			{
			case TypeKind.Array:
				return this.TransformArrayType((ArrayTypeSymbol)type);
			case TypeKind.Class:
			case TypeKind.Delegate:
			case TypeKind.Enum:
			case TypeKind.Interface:
			case TypeKind.Struct:
				return this.TransformNamedType((NamedTypeSymbol)type);
			case TypeKind.Dynamic:
			case TypeKind.TypeParameter:
				return type;
			case TypeKind.Pointer:
				return this.TransformPointerType((PointerTypeSymbol)type);
			case TypeKind.FunctionPointer:
				return this.TransformFunctionPointerType((FunctionPointerTypeSymbol)type);
			}
			throw new NativeIntegerTypeDecoder.ErrorTypeException();
		}

		// Token: 0x06007DD6 RID: 32214 RVA: 0x001C4B18 File Offset: 0x001C2D18
		private NamedTypeSymbol TransformNamedType(NamedTypeSymbol type)
		{
			if (!type.IsGenericType)
			{
				SpecialType specialType = type.SpecialType;
				if (specialType - SpecialType.System_IntPtr <= 1)
				{
					if (this._index >= this._transformFlags.Length)
					{
						throw new UnsupportedSignatureContent();
					}
					ImmutableArray<bool> transformFlags = this._transformFlags;
					int index = this._index;
					this._index = index + 1;
					bool flag = transformFlags[index];
					bool isNativeIntegerType = type.IsNativeIntegerType;
					if (!flag)
					{
						if (isNativeIntegerType)
						{
							return type.NativeIntegerUnderlyingType;
						}
					}
					else if (!isNativeIntegerType)
					{
						return type.AsNativeInteger();
					}
					return type;
				}
			}
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			type.GetAllTypeArgumentsNoUseSiteDiagnostics(instance);
			bool flag2 = false;
			for (int i = 0; i < instance.Count; i++)
			{
				TypeWithAnnotations typeWithAnnotations = instance[i];
				TypeWithAnnotations typeWithAnnotations2 = this.TransformTypeWithAnnotations(typeWithAnnotations);
				if (!typeWithAnnotations.IsSameAs(typeWithAnnotations2))
				{
					instance[i] = typeWithAnnotations2;
					flag2 = true;
				}
			}
			NamedTypeSymbol namedTypeSymbol = (flag2 ? type.WithTypeArguments(instance.ToImmutable()) : type);
			instance.Free();
			return namedTypeSymbol;
		}

		// Token: 0x06007DD7 RID: 32215 RVA: 0x001C4C10 File Offset: 0x001C2E10
		private ArrayTypeSymbol TransformArrayType(ArrayTypeSymbol type)
		{
			return type.WithElementType(this.TransformTypeWithAnnotations(type.ElementTypeWithAnnotations));
		}

		// Token: 0x06007DD8 RID: 32216 RVA: 0x001C4C24 File Offset: 0x001C2E24
		private PointerTypeSymbol TransformPointerType(PointerTypeSymbol type)
		{
			return type.WithPointedAtType(this.TransformTypeWithAnnotations(type.PointedAtTypeWithAnnotations));
		}

		// Token: 0x06007DD9 RID: 32217 RVA: 0x001C4C38 File Offset: 0x001C2E38
		private FunctionPointerTypeSymbol TransformFunctionPointerType(FunctionPointerTypeSymbol type)
		{
			TypeWithAnnotations typeWithAnnotations = this.TransformTypeWithAnnotations(type.Signature.ReturnTypeWithAnnotations);
			ImmutableArray<TypeWithAnnotations> immutableArray = ImmutableArray<TypeWithAnnotations>.Empty;
			bool flag = false;
			if (type.Signature.ParameterCount > 0)
			{
				ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(type.Signature.ParameterCount);
				foreach (ParameterSymbol parameterSymbol in type.Signature.Parameters)
				{
					TypeWithAnnotations typeWithAnnotations2 = this.TransformTypeWithAnnotations(parameterSymbol.TypeWithAnnotations);
					flag = flag || !typeWithAnnotations2.IsSameAs(parameterSymbol.TypeWithAnnotations);
					instance.Add(typeWithAnnotations2);
				}
				if (flag)
				{
					immutableArray = instance.ToImmutableAndFree();
				}
				else
				{
					immutableArray = type.Signature.ParameterTypesWithAnnotations;
					instance.Free();
				}
			}
			if (flag || !typeWithAnnotations.IsSameAs(type.Signature.ReturnTypeWithAnnotations))
			{
				return type.SubstituteTypeSymbol(typeWithAnnotations, immutableArray, default(ImmutableArray<CustomModifier>), default(ImmutableArray<ImmutableArray<CustomModifier>>));
			}
			return type;
		}

		// Token: 0x04001DA4 RID: 7588
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private readonly ImmutableArray<bool> _transformFlags;

		// Token: 0x04001DA5 RID: 7589
		private int _index;

		// Token: 0x02000978 RID: 2424
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private sealed class ErrorTypeException : Exception
		{
		}
	}
}
