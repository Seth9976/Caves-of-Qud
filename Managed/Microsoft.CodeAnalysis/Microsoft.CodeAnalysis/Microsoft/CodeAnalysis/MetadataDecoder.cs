using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001A3 RID: 419
	[Nullable(new byte[] { 0, 1, 1 })]
	internal abstract class MetadataDecoder<[Nullable(1)] ModuleSymbol, [Nullable(1)] TypeSymbol, [Nullable(1)] MethodSymbol, [Nullable(1)] FieldSymbol, [Nullable(1)] Symbol> : TypeNameDecoder<ModuleSymbol, TypeSymbol>, IAttributeNamedArgumentDecoder where ModuleSymbol : class, IModuleSymbolInternal where TypeSymbol : class, Symbol, ITypeSymbolInternal where MethodSymbol : class, Symbol, IMethodSymbolInternal where FieldSymbol : class, Symbol, IFieldSymbolInternal where Symbol : class, ISymbolInternal
	{
		// Token: 0x0600108A RID: 4234 RVA: 0x000302F1 File Offset: 0x0002E4F1
		internal MetadataDecoder(PEModule module, AssemblyIdentity containingAssemblyIdentity, SymbolFactory<ModuleSymbol, TypeSymbol> factory, ModuleSymbol moduleSymbol)
			: base(factory, moduleSymbol)
		{
			this.Module = module;
			this._containingAssemblyIdentity = containingAssemblyIdentity;
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0003030C File Offset: 0x0002E50C
		internal TypeSymbol GetTypeOfToken(EntityHandle token)
		{
			bool flag;
			return this.GetTypeOfToken(token, out flag);
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00030324 File Offset: 0x0002E524
		internal TypeSymbol GetTypeOfToken(EntityHandle token, out bool isNoPiaLocalType)
		{
			HandleKind kind = token.Kind;
			TypeSymbol typeSymbol;
			if (kind != HandleKind.TypeReference)
			{
				if (kind != HandleKind.TypeDefinition)
				{
					if (kind != HandleKind.TypeSpecification)
					{
						isNoPiaLocalType = false;
						typeSymbol = base.GetUnsupportedMetadataTypeSymbol(null);
					}
					else
					{
						isNoPiaLocalType = false;
						typeSymbol = this.GetTypeOfTypeSpec((TypeSpecificationHandle)token);
					}
				}
				else
				{
					typeSymbol = this.GetTypeOfTypeDef((TypeDefinitionHandle)token, out isNoPiaLocalType, false);
				}
			}
			else
			{
				typeSymbol = this.GetTypeOfTypeRef((TypeReferenceHandle)token, out isNoPiaLocalType);
			}
			return typeSymbol;
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00030388 File Offset: 0x0002E588
		private TypeSymbol GetTypeOfTypeSpec(TypeSpecificationHandle typeSpec)
		{
			TypeSymbol typeSymbol;
			try
			{
				BlobReader typeSpecificationSignatureReaderOrThrow = this.Module.GetTypeSpecificationSignatureReaderOrThrow(typeSpec);
				bool flag;
				typeSymbol = this.DecodeTypeOrThrow(ref typeSpecificationSignatureReaderOrThrow, out flag);
			}
			catch (BadImageFormatException ex)
			{
				typeSymbol = base.GetUnsupportedMetadataTypeSymbol(ex);
			}
			catch (UnsupportedSignatureContent)
			{
				typeSymbol = base.GetUnsupportedMetadataTypeSymbol(null);
			}
			return typeSymbol;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x000303E4 File Offset: 0x0002E5E4
		private TypeSymbol DecodeTypeOrThrow(ref BlobReader ppSig, out bool refersToNoPiaLocalType)
		{
			SignatureTypeCode signatureTypeCode = ppSig.ReadSignatureTypeCode();
			return this.DecodeTypeOrThrow(ref ppSig, signatureTypeCode, out refersToNoPiaLocalType);
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00030404 File Offset: 0x0002E604
		private TypeSymbol DecodeTypeOrThrow(ref BlobReader ppSig, SignatureTypeCode typeCode, out bool refersToNoPiaLocalType)
		{
			refersToNoPiaLocalType = false;
			switch (typeCode)
			{
			case SignatureTypeCode.Void:
			case SignatureTypeCode.Boolean:
			case SignatureTypeCode.Char:
			case SignatureTypeCode.SByte:
			case SignatureTypeCode.Byte:
			case SignatureTypeCode.Int16:
			case SignatureTypeCode.UInt16:
			case SignatureTypeCode.Int32:
			case SignatureTypeCode.UInt32:
			case SignatureTypeCode.Int64:
			case SignatureTypeCode.UInt64:
			case SignatureTypeCode.Single:
			case SignatureTypeCode.Double:
			case SignatureTypeCode.String:
			case SignatureTypeCode.TypedReference:
			case SignatureTypeCode.IntPtr:
			case SignatureTypeCode.UIntPtr:
			case SignatureTypeCode.Object:
				return base.GetSpecialType(typeCode.ToSpecialType());
			case SignatureTypeCode.Pointer:
			{
				ImmutableArray<ModifierInfo<TypeSymbol>> immutableArray = this.DecodeModifiersOrThrow(ref ppSig, out typeCode);
				TypeSymbol typeSymbol = this.DecodeTypeOrThrow(ref ppSig, typeCode, out refersToNoPiaLocalType);
				return base.MakePointerTypeSymbol(typeSymbol, immutableArray);
			}
			case SignatureTypeCode.ByReference:
			case (SignatureTypeCode)17:
			case (SignatureTypeCode)18:
			case (SignatureTypeCode)23:
			case (SignatureTypeCode)26:
				break;
			case SignatureTypeCode.GenericTypeParameter:
			{
				int num;
				if (!ppSig.TryReadCompressedInteger(out num))
				{
					throw new UnsupportedSignatureContent();
				}
				return this.GetGenericTypeParamSymbol(num);
			}
			case SignatureTypeCode.Array:
			{
				ImmutableArray<ModifierInfo<TypeSymbol>> immutableArray = this.DecodeModifiersOrThrow(ref ppSig, out typeCode);
				TypeSymbol typeSymbol = this.DecodeTypeOrThrow(ref ppSig, typeCode, out refersToNoPiaLocalType);
				int num2;
				int num3;
				if (!ppSig.TryReadCompressedInteger(out num2) || !ppSig.TryReadCompressedInteger(out num3))
				{
					throw new UnsupportedSignatureContent();
				}
				ImmutableArray<int> immutableArray2;
				if (num3 == 0)
				{
					immutableArray2 = ImmutableArray<int>.Empty;
				}
				else
				{
					ArrayBuilder<int> instance = ArrayBuilder<int>.GetInstance(num3);
					for (int i = 0; i < num3; i++)
					{
						int num4;
						if (!ppSig.TryReadCompressedInteger(out num4))
						{
							throw new UnsupportedSignatureContent();
						}
						instance.Add(num4);
					}
					immutableArray2 = instance.ToImmutableAndFree();
				}
				int num5;
				if (!ppSig.TryReadCompressedInteger(out num5))
				{
					throw new UnsupportedSignatureContent();
				}
				ImmutableArray<int> immutableArray3 = default(ImmutableArray<int>);
				if (num5 == 0)
				{
					immutableArray3 = ImmutableArray<int>.Empty;
				}
				else
				{
					ArrayBuilder<int> arrayBuilder = ((num5 != num2) ? ArrayBuilder<int>.GetInstance(num5, 0) : null);
					for (int j = 0; j < num5; j++)
					{
						int num6;
						if (!ppSig.TryReadCompressedSignedInteger(out num6))
						{
							throw new UnsupportedSignatureContent();
						}
						if (num6 != 0)
						{
							if (arrayBuilder == null)
							{
								arrayBuilder = ArrayBuilder<int>.GetInstance(num5, 0);
							}
							arrayBuilder[j] = num6;
						}
					}
					if (arrayBuilder != null)
					{
						immutableArray3 = arrayBuilder.ToImmutableAndFree();
					}
				}
				return base.GetMDArrayTypeSymbol(num2, typeSymbol, immutableArray, immutableArray2, immutableArray3);
			}
			case SignatureTypeCode.GenericTypeInstance:
				return this.DecodeGenericTypeInstanceOrThrow(ref ppSig, out refersToNoPiaLocalType);
			case SignatureTypeCode.FunctionPointer:
			{
				SignatureHeader signatureHeader = ppSig.ReadSignatureHeader();
				int num7;
				ParamInfo<TypeSymbol>[] array = this.DecodeSignatureParametersOrThrow(ref ppSig, signatureHeader, out num7, false, true);
				if (num7 != 0)
				{
					throw new UnsupportedSignatureContent();
				}
				return base.MakeFunctionPointerTypeSymbol(signatureHeader.CallingConvention.FromSignatureConvention(), ImmutableArray.Create<ParamInfo<TypeSymbol>>(array));
			}
			case SignatureTypeCode.SZArray:
			{
				ImmutableArray<ModifierInfo<TypeSymbol>> immutableArray = this.DecodeModifiersOrThrow(ref ppSig, out typeCode);
				TypeSymbol typeSymbol = this.DecodeTypeOrThrow(ref ppSig, typeCode, out refersToNoPiaLocalType);
				return base.GetSZArrayTypeSymbol(typeSymbol, immutableArray);
			}
			case SignatureTypeCode.GenericMethodParameter:
			{
				int num;
				if (!ppSig.TryReadCompressedInteger(out num))
				{
					throw new UnsupportedSignatureContent();
				}
				return this.GetGenericMethodTypeParamSymbol(num);
			}
			default:
				if (typeCode == SignatureTypeCode.TypeHandle)
				{
					return this.GetSymbolForTypeHandleOrThrow(ppSig.ReadTypeHandle(), out refersToNoPiaLocalType, false, true);
				}
				break;
			}
			throw new UnsupportedSignatureContent();
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0003069C File Offset: 0x0002E89C
		private TypeSymbol DecodeGenericTypeInstanceOrThrow(ref BlobReader ppSig, out bool refersToNoPiaLocalType)
		{
			if (ppSig.ReadSignatureTypeCode() != SignatureTypeCode.TypeHandle)
			{
				throw new UnsupportedSignatureContent();
			}
			EntityHandle entityHandle = ppSig.ReadTypeHandle();
			int num;
			if (!ppSig.TryReadCompressedInteger(out num))
			{
				throw new UnsupportedSignatureContent();
			}
			TypeSymbol typeOfToken = this.GetTypeOfToken(entityHandle, out refersToNoPiaLocalType);
			ArrayBuilder<KeyValuePair<TypeSymbol, ImmutableArray<ModifierInfo<TypeSymbol>>>> instance = ArrayBuilder<KeyValuePair<TypeSymbol, ImmutableArray<ModifierInfo<TypeSymbol>>>>.GetInstance(num);
			ArrayBuilder<bool> instance2 = ArrayBuilder<bool>.GetInstance(num);
			for (int i = 0; i < num; i++)
			{
				SignatureTypeCode signatureTypeCode;
				ImmutableArray<ModifierInfo<TypeSymbol>> immutableArray = this.DecodeModifiersOrThrow(ref ppSig, out signatureTypeCode);
				bool flag;
				instance.Add(KeyValuePairUtil.Create<TypeSymbol, ImmutableArray<ModifierInfo<TypeSymbol>>>(this.DecodeTypeOrThrow(ref ppSig, signatureTypeCode, out flag), immutableArray));
				instance2.Add(flag);
			}
			ImmutableArray<KeyValuePair<TypeSymbol, ImmutableArray<ModifierInfo<TypeSymbol>>>> immutableArray2 = instance.ToImmutableAndFree();
			ImmutableArray<bool> immutableArray3 = instance2.ToImmutableAndFree();
			TypeSymbol typeSymbol = base.SubstituteTypeParameters(typeOfToken, immutableArray2, immutableArray3);
			ImmutableArray<bool>.Enumerator enumerator = immutableArray3.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current)
				{
					refersToNoPiaLocalType = true;
					break;
				}
			}
			return typeSymbol;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00030768 File Offset: 0x0002E968
		internal TypeSymbol GetSymbolForTypeHandleOrThrow(EntityHandle handle, out bool isNoPiaLocalType, bool allowTypeSpec, bool requireShortForm)
		{
			if (handle.IsNil)
			{
				throw new UnsupportedSignatureContent();
			}
			HandleKind kind = handle.Kind;
			TypeSymbol typeSymbol;
			if (kind != HandleKind.TypeReference)
			{
				if (kind != HandleKind.TypeDefinition)
				{
					if (kind != HandleKind.TypeSpecification)
					{
						throw ExceptionUtilities.UnexpectedValue(handle.Kind);
					}
					if (!allowTypeSpec)
					{
						throw new UnsupportedSignatureContent();
					}
					isNoPiaLocalType = false;
					typeSymbol = this.GetTypeOfTypeSpec((TypeSpecificationHandle)handle);
				}
				else
				{
					typeSymbol = this.GetTypeOfTypeDef((TypeDefinitionHandle)handle, out isNoPiaLocalType, false);
				}
			}
			else
			{
				typeSymbol = this.GetTypeOfTypeRef((TypeReferenceHandle)handle, out isNoPiaLocalType);
			}
			if (requireShortForm && typeSymbol.SpecialType.HasShortFormSignatureEncoding())
			{
				throw new UnsupportedSignatureContent();
			}
			return typeSymbol;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00030808 File Offset: 0x0002EA08
		private TypeSymbol GetTypeOfTypeRef(TypeReferenceHandle typeRef, out bool isNoPiaLocalType)
		{
			ConcurrentDictionary<TypeReferenceHandle, TypeSymbol> typeRefHandleToTypeMap = this.GetTypeRefHandleToTypeMap();
			TypeSymbol typeSymbol;
			if (typeRefHandleToTypeMap != null && typeRefHandleToTypeMap.TryGetValue(typeRef, ref typeSymbol))
			{
				isNoPiaLocalType = false;
				return typeSymbol;
			}
			try
			{
				string text;
				string text2;
				EntityHandle entityHandle;
				this.Module.GetTypeRefPropsOrThrow(typeRef, out text, out text2, out entityHandle);
				MetadataTypeName metadataTypeName = ((text2.Length > 0) ? MetadataTypeName.FromNamespaceAndTypeName(text2, text, false, -1) : MetadataTypeName.FromTypeName(text, false, -1));
				typeSymbol = this.GetTypeByNameOrThrow(ref metadataTypeName, entityHandle, out isNoPiaLocalType);
			}
			catch (BadImageFormatException ex)
			{
				typeSymbol = base.GetUnsupportedMetadataTypeSymbol(ex);
				isNoPiaLocalType = false;
			}
			if (typeRefHandleToTypeMap != null && !isNoPiaLocalType)
			{
				typeRefHandleToTypeMap.GetOrAdd(typeRef, typeSymbol);
			}
			return typeSymbol;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x000308A0 File Offset: 0x0002EAA0
		private TypeSymbol GetTypeByNameOrThrow(ref MetadataTypeName fullName, EntityHandle tokenResolutionScope, out bool isNoPiaLocalType)
		{
			HandleKind kind = tokenResolutionScope.Kind;
			if (kind == HandleKind.TypeReference)
			{
				if (tokenResolutionScope.IsNil)
				{
					throw new BadImageFormatException();
				}
				TypeSymbol typeOfToken = this.GetTypeOfToken(tokenResolutionScope);
				isNoPiaLocalType = false;
				return this.LookupNestedTypeDefSymbol(typeOfToken, ref fullName);
			}
			else if (kind == HandleKind.AssemblyReference)
			{
				isNoPiaLocalType = false;
				AssemblyReferenceHandle assemblyReferenceHandle = (AssemblyReferenceHandle)tokenResolutionScope;
				if (assemblyReferenceHandle.IsNil)
				{
					throw new BadImageFormatException();
				}
				return this.LookupTopLevelTypeDefSymbol(this.Module.GetAssemblyReferenceIndexOrThrow(assemblyReferenceHandle), ref fullName);
			}
			else if (kind == HandleKind.ModuleReference)
			{
				ModuleReferenceHandle moduleReferenceHandle = (ModuleReferenceHandle)tokenResolutionScope;
				if (moduleReferenceHandle.IsNil)
				{
					throw new BadImageFormatException();
				}
				return this.LookupTopLevelTypeDefSymbol(this.Module.GetModuleRefNameOrThrow(moduleReferenceHandle), ref fullName, out isNoPiaLocalType);
			}
			else
			{
				if (tokenResolutionScope == EntityHandle.ModuleDefinition)
				{
					return this.LookupTopLevelTypeDefSymbol(ref fullName, out isNoPiaLocalType);
				}
				isNoPiaLocalType = false;
				return base.GetUnsupportedMetadataTypeSymbol(null);
			}
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00030964 File Offset: 0x0002EB64
		private TypeSymbol GetTypeOfTypeDef(TypeDefinitionHandle typeDef)
		{
			bool flag;
			return this.GetTypeOfTypeDef(typeDef, out flag, false);
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0003097C File Offset: 0x0002EB7C
		private TypeSymbol GetTypeOfTypeDef(TypeDefinitionHandle typeDef, out bool isNoPiaLocalType, bool isContainingType)
		{
			TypeSymbol typeSymbol2;
			try
			{
				ConcurrentDictionary<TypeDefinitionHandle, TypeSymbol> typeHandleToTypeMap = this.GetTypeHandleToTypeMap();
				TypeSymbol typeSymbol;
				if (typeHandleToTypeMap != null && typeHandleToTypeMap.TryGetValue(typeDef, ref typeSymbol))
				{
					if (!this.Module.IsNestedTypeDefOrThrow(typeDef) && this.Module.IsNoPiaLocalType(typeDef))
					{
						isNoPiaLocalType = true;
					}
					else
					{
						isNoPiaLocalType = false;
					}
					typeSymbol2 = typeSymbol;
				}
				else
				{
					string typeDefNameOrThrow = this.Module.GetTypeDefNameOrThrow(typeDef);
					if (this.Module.IsNestedTypeDefOrThrow(typeDef))
					{
						TypeDefinitionHandle containingTypeOrThrow = this.Module.GetContainingTypeOrThrow(typeDef);
						if (containingTypeOrThrow.IsNil)
						{
							isNoPiaLocalType = false;
							typeSymbol2 = base.GetUnsupportedMetadataTypeSymbol(null);
						}
						else
						{
							TypeSymbol typeOfTypeDef = this.GetTypeOfTypeDef(containingTypeOrThrow, out isNoPiaLocalType, true);
							if (isNoPiaLocalType)
							{
								if (!isContainingType)
								{
									isNoPiaLocalType = false;
								}
								typeSymbol2 = base.GetUnsupportedMetadataTypeSymbol(null);
							}
							else
							{
								MetadataTypeName metadataTypeName = MetadataTypeName.FromTypeName(typeDefNameOrThrow, false, -1);
								typeSymbol2 = this.LookupNestedTypeDefSymbol(typeOfTypeDef, ref metadataTypeName);
							}
						}
					}
					else
					{
						string typeDefNamespaceOrThrow = this.Module.GetTypeDefNamespaceOrThrow(typeDef);
						MetadataTypeName metadataTypeName = ((typeDefNamespaceOrThrow.Length > 0) ? MetadataTypeName.FromNamespaceAndTypeName(typeDefNamespaceOrThrow, typeDefNameOrThrow, false, -1) : MetadataTypeName.FromTypeName(typeDefNameOrThrow, false, -1));
						string text;
						string text2;
						string text3;
						if (this.Module.IsNoPiaLocalType(typeDef, out text, out text2, out text3))
						{
							isNoPiaLocalType = true;
							if (!this.Module.HasGenericParametersOrThrow(typeDef))
							{
								MetadataTypeName metadataTypeName2 = MetadataTypeName.FromNamespaceAndTypeName(metadataTypeName.NamespaceName, metadataTypeName.TypeName, false, 0);
								typeSymbol = this.SubstituteNoPiaLocalType(typeDef, ref metadataTypeName2, text, text2, text3);
								typeSymbol2 = typeSymbol;
							}
							else
							{
								typeSymbol = base.GetUnsupportedMetadataTypeSymbol(null);
								if (typeHandleToTypeMap != null)
								{
									typeSymbol = typeHandleToTypeMap.GetOrAdd(typeDef, typeSymbol);
								}
								typeSymbol2 = typeSymbol;
							}
						}
						else
						{
							isNoPiaLocalType = false;
							typeSymbol = this.LookupTopLevelTypeDefSymbol(ref metadataTypeName, out isNoPiaLocalType);
							typeSymbol2 = typeSymbol;
						}
					}
				}
			}
			catch (BadImageFormatException ex)
			{
				isNoPiaLocalType = false;
				typeSymbol2 = base.GetUnsupportedMetadataTypeSymbol(ex);
			}
			return typeSymbol2;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00030B24 File Offset: 0x0002ED24
		private ImmutableArray<ModifierInfo<TypeSymbol>> DecodeModifiersOrThrow(ref BlobReader signatureReader, out SignatureTypeCode typeCode)
		{
			ArrayBuilder<ModifierInfo<TypeSymbol>> arrayBuilder = null;
			for (;;)
			{
				typeCode = signatureReader.ReadSignatureTypeCode();
				bool flag;
				if (typeCode == SignatureTypeCode.RequiredModifier)
				{
					flag = false;
				}
				else
				{
					if (typeCode != SignatureTypeCode.OptionalModifier)
					{
						break;
					}
					flag = true;
				}
				TypeSymbol typeSymbol = this.DecodeModifierTypeOrThrow(ref signatureReader);
				ModifierInfo<TypeSymbol> modifierInfo = new ModifierInfo<TypeSymbol>(flag, typeSymbol);
				if (arrayBuilder == null)
				{
					arrayBuilder = ArrayBuilder<ModifierInfo<TypeSymbol>>.GetInstance();
				}
				arrayBuilder.Add(modifierInfo);
			}
			if (arrayBuilder == null)
			{
				return default(ImmutableArray<ModifierInfo<TypeSymbol>>);
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00030B84 File Offset: 0x0002ED84
		private TypeSymbol DecodeModifierTypeOrThrow(ref BlobReader signatureReader)
		{
			EntityHandle entityHandle = signatureReader.ReadTypeHandle();
			BlobReader typeSpecificationSignatureReaderOrThrow;
			SignatureTypeCode signatureTypeCode;
			for (;;)
			{
				HandleKind kind = entityHandle.Kind;
				if (kind == HandleKind.TypeReference)
				{
					goto IL_0042;
				}
				if (kind == HandleKind.TypeDefinition)
				{
					goto IL_0025;
				}
				if (kind != HandleKind.TypeSpecification)
				{
					break;
				}
				typeSpecificationSignatureReaderOrThrow = this.Module.GetTypeSpecificationSignatureReaderOrThrow((TypeSpecificationHandle)entityHandle);
				signatureTypeCode = typeSpecificationSignatureReaderOrThrow.ReadSignatureTypeCode();
				switch (signatureTypeCode)
				{
				case SignatureTypeCode.Void:
				case SignatureTypeCode.Boolean:
				case SignatureTypeCode.Char:
				case SignatureTypeCode.SByte:
				case SignatureTypeCode.Byte:
				case SignatureTypeCode.Int16:
				case SignatureTypeCode.UInt16:
				case SignatureTypeCode.Int32:
				case SignatureTypeCode.UInt32:
				case SignatureTypeCode.Int64:
				case SignatureTypeCode.UInt64:
				case SignatureTypeCode.Single:
				case SignatureTypeCode.Double:
				case SignatureTypeCode.String:
				case SignatureTypeCode.TypedReference:
				case SignatureTypeCode.IntPtr:
				case SignatureTypeCode.UIntPtr:
				case SignatureTypeCode.Object:
					goto IL_00FA;
				case SignatureTypeCode.Pointer:
				case SignatureTypeCode.ByReference:
				case (SignatureTypeCode)17:
				case (SignatureTypeCode)18:
				case SignatureTypeCode.GenericTypeParameter:
				case SignatureTypeCode.Array:
				case (SignatureTypeCode)23:
				case (SignatureTypeCode)26:
				case SignatureTypeCode.FunctionPointer:
					goto IL_0124;
				case SignatureTypeCode.GenericTypeInstance:
					goto IL_0117;
				default:
					if (signatureTypeCode != SignatureTypeCode.TypeHandle)
					{
						goto Block_5;
					}
					entityHandle = typeSpecificationSignatureReaderOrThrow.ReadTypeHandle();
					break;
				}
			}
			throw new UnsupportedSignatureContent();
			IL_0025:
			bool flag;
			TypeSymbol typeSymbol = this.GetTypeOfTypeDef((TypeDefinitionHandle)entityHandle, out flag, false);
			return base.SubstituteWithUnboundIfGeneric(typeSymbol);
			IL_0042:
			typeSymbol = this.GetTypeOfTypeRef((TypeReferenceHandle)entityHandle, out flag);
			return base.SubstituteWithUnboundIfGeneric(typeSymbol);
			Block_5:
			goto IL_0124;
			IL_00FA:
			return base.GetSpecialType(signatureTypeCode.ToSpecialType());
			IL_0117:
			bool flag2;
			return this.DecodeGenericTypeInstanceOrThrow(ref typeSpecificationSignatureReaderOrThrow, out flag2);
			IL_0124:
			throw new UnsupportedSignatureContent();
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00030CC4 File Offset: 0x0002EEC4
		internal ImmutableArray<LocalInfo<TypeSymbol>> DecodeLocalSignatureOrThrow(ref BlobReader signatureReader)
		{
			SignatureHeader signatureHeader = signatureReader.ReadSignatureHeader();
			if (signatureHeader.Kind != SignatureKind.LocalVariables)
			{
				throw new UnsupportedSignatureContent();
			}
			int num;
			int num2;
			MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.GetSignatureCountsOrThrow(ref signatureReader, signatureHeader, out num, out num2);
			ArrayBuilder<LocalInfo<TypeSymbol>> instance = ArrayBuilder<LocalInfo<TypeSymbol>>.GetInstance(num);
			ArrayBuilder<int> instance2 = ArrayBuilder<int>.GetInstance(num);
			ImmutableArray<LocalInfo<TypeSymbol>> immutableArray;
			try
			{
				for (int i = 0; i < num; i++)
				{
					instance2.Add(signatureReader.Offset);
					instance.Add(this.DecodeLocalVariableOrThrow(ref signatureReader));
				}
				if (signatureReader.RemainingBytes > 0)
				{
					throw new UnsupportedSignatureContent();
				}
				signatureReader.Reset();
				for (int j = 0; j < num; j++)
				{
					int num3 = instance2[j];
					while (signatureReader.Offset < num3)
					{
						signatureReader.ReadByte();
					}
					int num4 = ((j < num - 1) ? (instance2[j + 1] - num3) : signatureReader.RemainingBytes);
					byte[] array = signatureReader.ReadBytes(num4);
					instance[j] = instance[j].WithSignature(array);
				}
				immutableArray = instance.ToImmutable();
			}
			finally
			{
				instance2.Free();
				instance.Free();
			}
			return immutableArray;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00030DE0 File Offset: 0x0002EFE0
		internal TypeSymbol DecodeGenericParameterConstraint(EntityHandle token, out ImmutableArray<ModifierInfo<TypeSymbol>> modifiers)
		{
			modifiers = ImmutableArray<ModifierInfo<TypeSymbol>>.Empty;
			HandleKind kind = token.Kind;
			bool flag;
			if (kind != HandleKind.TypeReference)
			{
				if (kind == HandleKind.TypeDefinition)
				{
					return this.GetTypeOfTypeDef((TypeDefinitionHandle)token);
				}
				if (kind != HandleKind.TypeSpecification)
				{
					return base.GetUnsupportedMetadataTypeSymbol(null);
				}
				try
				{
					BlobReader typeSpecificationSignatureReaderOrThrow = this.Module.GetTypeSpecificationSignatureReaderOrThrow((TypeSpecificationHandle)token);
					SignatureTypeCode signatureTypeCode;
					modifiers = this.DecodeModifiersOrThrow(ref typeSpecificationSignatureReaderOrThrow, out signatureTypeCode);
					return this.DecodeTypeOrThrow(ref typeSpecificationSignatureReaderOrThrow, signatureTypeCode, out flag);
				}
				catch (BadImageFormatException ex)
				{
					return base.GetUnsupportedMetadataTypeSymbol(ex);
				}
				catch (UnsupportedSignatureContent)
				{
					return base.GetUnsupportedMetadataTypeSymbol(null);
				}
			}
			return this.GetTypeOfTypeRef((TypeReferenceHandle)token, out flag);
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00030E9C File Offset: 0x0002F09C
		internal LocalInfo<TypeSymbol> DecodeLocalVariableOrThrow(ref BlobReader signatureReader)
		{
			SignatureTypeCode signatureTypeCode;
			ImmutableArray<ModifierInfo<TypeSymbol>> immutableArray = this.DecodeModifiersOrThrow(ref signatureReader, out signatureTypeCode);
			if (immutableArray.AnyRequired<TypeSymbol>())
			{
				throw new UnsupportedSignatureContent();
			}
			LocalSlotConstraints localSlotConstraints = LocalSlotConstraints.None;
			if (signatureTypeCode == SignatureTypeCode.Pinned)
			{
				localSlotConstraints |= LocalSlotConstraints.Pinned;
				signatureTypeCode = signatureReader.ReadSignatureTypeCode();
			}
			if (signatureTypeCode == SignatureTypeCode.ByReference)
			{
				localSlotConstraints |= LocalSlotConstraints.ByRef;
				signatureTypeCode = signatureReader.ReadSignatureTypeCode();
			}
			TypeSymbol typeSymbol;
			if (signatureTypeCode == SignatureTypeCode.TypedReference && localSlotConstraints != LocalSlotConstraints.None)
			{
				typeSymbol = base.GetUnsupportedMetadataTypeSymbol(null);
			}
			else
			{
				try
				{
					bool flag;
					typeSymbol = this.DecodeTypeOrThrow(ref signatureReader, signatureTypeCode, out flag);
				}
				catch (UnsupportedSignatureContent)
				{
					typeSymbol = base.GetUnsupportedMetadataTypeSymbol(null);
				}
			}
			return new LocalInfo<TypeSymbol>(typeSymbol, immutableArray, localSlotConstraints, null);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00030F28 File Offset: 0x0002F128
		internal void DecodeLocalConstantBlobOrThrow(ref BlobReader sigReader, out TypeSymbol type, out ConstantValue value)
		{
			SignatureTypeCode signatureTypeCode;
			if (this.DecodeModifiersOrThrow(ref sigReader, out signatureTypeCode).AnyRequired<TypeSymbol>())
			{
				throw new UnsupportedSignatureContent();
			}
			if (signatureTypeCode == SignatureTypeCode.TypeHandle)
			{
				bool flag;
				type = this.GetSymbolForTypeHandleOrThrow(sigReader.ReadTypeHandle(), out flag, true, true);
				if (type.SpecialType == SpecialType.System_Decimal)
				{
					value = ConstantValue.Create(sigReader.ReadDecimal());
					return;
				}
				if (type.SpecialType == SpecialType.System_DateTime)
				{
					value = ConstantValue.Create(sigReader.ReadDateTime());
					return;
				}
				if (sigReader.RemainingBytes == 0)
				{
					value = ((type.IsReferenceType || type.TypeKind == TypeKind.Pointer || type.TypeKind == TypeKind.FunctionPointer) ? ConstantValue.Null : ConstantValue.Bad);
					return;
				}
				value = ConstantValue.Bad;
				return;
			}
			else
			{
				bool flag2;
				value = MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.DecodePrimitiveConstantValue(ref sigReader, signatureTypeCode, out flag2);
				SpecialType specialType = signatureTypeCode.ToSpecialType();
				if (flag2 && sigReader.RemainingBytes > 0)
				{
					bool flag3;
					type = this.GetSymbolForTypeHandleOrThrow(sigReader.ReadTypeHandle(), out flag3, true, true);
					TypeSymbol typeSymbol = base.GetEnumUnderlyingType(type);
					if (typeSymbol == null || typeSymbol.SpecialType != specialType)
					{
						throw new UnsupportedSignatureContent();
					}
				}
				else
				{
					type = base.GetSpecialType(specialType);
				}
				if (sigReader.RemainingBytes > 0)
				{
					throw new UnsupportedSignatureContent();
				}
				return;
			}
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00031070 File Offset: 0x0002F270
		private static ConstantValue DecodePrimitiveConstantValue(ref BlobReader sigReader, SignatureTypeCode typeCode, out bool isEnumTypeCode)
		{
			switch (typeCode)
			{
			case SignatureTypeCode.Boolean:
				isEnumTypeCode = true;
				return ConstantValue.Create(sigReader.ReadBoolean());
			case SignatureTypeCode.Char:
				isEnumTypeCode = true;
				return ConstantValue.Create(sigReader.ReadChar());
			case SignatureTypeCode.SByte:
				isEnumTypeCode = true;
				return ConstantValue.Create(sigReader.ReadSByte());
			case SignatureTypeCode.Byte:
				isEnumTypeCode = true;
				return ConstantValue.Create(sigReader.ReadByte());
			case SignatureTypeCode.Int16:
				isEnumTypeCode = true;
				return ConstantValue.Create(sigReader.ReadInt16());
			case SignatureTypeCode.UInt16:
				isEnumTypeCode = true;
				return ConstantValue.Create(sigReader.ReadUInt16());
			case SignatureTypeCode.Int32:
				isEnumTypeCode = true;
				return ConstantValue.Create(sigReader.ReadInt32());
			case SignatureTypeCode.UInt32:
				isEnumTypeCode = true;
				return ConstantValue.Create(sigReader.ReadUInt32());
			case SignatureTypeCode.Int64:
				isEnumTypeCode = true;
				return ConstantValue.Create(sigReader.ReadInt64());
			case SignatureTypeCode.UInt64:
				isEnumTypeCode = true;
				return ConstantValue.Create(sigReader.ReadUInt64());
			case SignatureTypeCode.Single:
				isEnumTypeCode = false;
				return ConstantValue.Create(sigReader.ReadSingle());
			case SignatureTypeCode.Double:
				isEnumTypeCode = false;
				return ConstantValue.Create(sigReader.ReadDouble());
			case SignatureTypeCode.String:
				isEnumTypeCode = false;
				if (sigReader.RemainingBytes == 1)
				{
					if (sigReader.ReadByte() != 255)
					{
						return ConstantValue.Bad;
					}
					return ConstantValue.Null;
				}
				else
				{
					if (sigReader.RemainingBytes % 2 != 0)
					{
						return ConstantValue.Bad;
					}
					return ConstantValue.Create(sigReader.ReadUTF16(sigReader.RemainingBytes));
				}
				break;
			case SignatureTypeCode.Object:
				isEnumTypeCode = false;
				return ConstantValue.Null;
			}
			throw new UnsupportedSignatureContent();
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00031200 File Offset: 0x0002F400
		internal ImmutableArray<LocalInfo<TypeSymbol>> GetLocalsOrThrow(StandaloneSignatureHandle handle)
		{
			BlobHandle signature = this.Module.MetadataReader.GetStandaloneSignature(handle).Signature;
			BlobReader blobReader = this.Module.MetadataReader.GetBlobReader(signature);
			return this.DecodeLocalSignatureOrThrow(ref blobReader);
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00031244 File Offset: 0x0002F444
		internal unsafe TypeSymbol DecodeLocalVariableTypeOrThrow(ImmutableArray<byte> signature)
		{
			if (signature.IsDefaultOrEmpty)
			{
				throw new UnsupportedSignatureContent();
			}
			fixed (byte* pinnableReference = signature.AsSpan().GetPinnableReference())
			{
				byte* ptr = pinnableReference;
				BlobReader blobReader = new BlobReader(ptr, signature.Length);
				LocalInfo<TypeSymbol> localInfo = this.DecodeLocalVariableOrThrow(ref blobReader);
				if (localInfo.IsByRef || localInfo.IsPinned)
				{
					throw new UnsupportedSignatureContent();
				}
				return localInfo.Type;
			}
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x000312AC File Offset: 0x0002F4AC
		internal ImmutableArray<LocalInfo<TypeSymbol>> GetLocalInfo(StandaloneSignatureHandle localSignatureHandle)
		{
			if (localSignatureHandle.IsNil)
			{
				return ImmutableArray<LocalInfo<TypeSymbol>>.Empty;
			}
			MetadataReader metadataReader = this.Module.MetadataReader;
			BlobHandle signature = metadataReader.GetStandaloneSignature(localSignatureHandle).Signature;
			BlobReader blobReader = metadataReader.GetBlobReader(signature);
			return this.DecodeLocalSignatureOrThrow(ref blobReader);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x000312F4 File Offset: 0x0002F4F4
		private void DecodeParameterOrThrow(ref BlobReader signatureReader, ref ParamInfo<TypeSymbol> info)
		{
			SignatureTypeCode signatureTypeCode;
			info.CustomModifiers = this.DecodeModifiersOrThrow(ref signatureReader, out signatureTypeCode);
			if (signatureTypeCode == SignatureTypeCode.ByReference)
			{
				info.IsByRef = true;
				info.RefCustomModifiers = info.CustomModifiers;
				info.CustomModifiers = this.DecodeModifiersOrThrow(ref signatureReader, out signatureTypeCode);
			}
			bool flag;
			info.Type = this.DecodeTypeOrThrow(ref signatureReader, signatureTypeCode, out flag);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00031348 File Offset: 0x0002F548
		internal ParamInfo<TypeSymbol>[] GetSignatureForMethod(MethodDefinitionHandle methodDef, out SignatureHeader signatureHeader, out BadImageFormatException metadataException, bool setParamHandles = true)
		{
			ParamInfo<TypeSymbol>[] array = null;
			signatureHeader = default(SignatureHeader);
			try
			{
				BlobHandle methodSignatureOrThrow = this.Module.GetMethodSignatureOrThrow(methodDef);
				BlobReader blobReader = this.DecodeSignatureHeaderOrThrow(methodSignatureOrThrow, out signatureHeader);
				int num;
				array = this.DecodeSignatureParametersOrThrow(ref blobReader, signatureHeader, out num, true, false);
				if (setParamHandles)
				{
					int num2 = array.Length;
					foreach (ParameterHandle parameterHandle in this.Module.GetParametersOfMethodOrThrow(methodDef))
					{
						int parameterSequenceNumberOrThrow = this.Module.GetParameterSequenceNumberOrThrow(parameterHandle);
						if (parameterSequenceNumberOrThrow >= 0 && parameterSequenceNumberOrThrow < num2 && array[parameterSequenceNumberOrThrow].Handle.IsNil)
						{
							array[parameterSequenceNumberOrThrow].Handle = parameterHandle;
						}
					}
				}
				metadataException = null;
			}
			catch (BadImageFormatException ex)
			{
				metadataException = ex;
				if (array == null)
				{
					array = new ParamInfo<TypeSymbol>[1];
					array[0].Type = base.GetUnsupportedMetadataTypeSymbol(ex);
				}
			}
			return array;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00031450 File Offset: 0x0002F650
		internal static void GetSignatureCountsOrThrow(PEModule module, MethodDefinitionHandle methodDef, out int parameterCount, out int typeParameterCount)
		{
			BlobHandle methodSignatureOrThrow = module.GetMethodSignatureOrThrow(methodDef);
			SignatureHeader signatureHeader;
			BlobReader blobReader = MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.DecodeSignatureHeaderOrThrow(module, methodSignatureOrThrow, out signatureHeader);
			MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.GetSignatureCountsOrThrow(ref blobReader, signatureHeader, out parameterCount, out typeParameterCount);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0003147C File Offset: 0x0002F67C
		internal ParamInfo<TypeSymbol>[] GetSignatureForProperty(PropertyDefinitionHandle handle, out SignatureHeader signatureHeader, out BadImageFormatException BadImageFormatException)
		{
			ParamInfo<TypeSymbol>[] array = null;
			signatureHeader = default(SignatureHeader);
			try
			{
				BlobHandle propertySignatureOrThrow = this.Module.GetPropertySignatureOrThrow(handle);
				BlobReader blobReader = this.DecodeSignatureHeaderOrThrow(propertySignatureOrThrow, out signatureHeader);
				int num;
				array = this.DecodeSignatureParametersOrThrow(ref blobReader, signatureHeader, out num, true, false);
				BadImageFormatException = null;
			}
			catch (BadImageFormatException ex)
			{
				BadImageFormatException = ex;
				if (array == null)
				{
					array = new ParamInfo<TypeSymbol>[1];
					array[0].Type = base.GetUnsupportedMetadataTypeSymbol(ex);
				}
			}
			return array;
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x000314F8 File Offset: 0x0002F6F8
		internal SignatureHeader GetSignatureHeaderForProperty(PropertyDefinitionHandle handle)
		{
			SignatureHeader signatureHeader2;
			try
			{
				BlobHandle propertySignatureOrThrow = this.Module.GetPropertySignatureOrThrow(handle);
				SignatureHeader signatureHeader;
				this.DecodeSignatureHeaderOrThrow(propertySignatureOrThrow, out signatureHeader);
				signatureHeader2 = signatureHeader;
			}
			catch (BadImageFormatException)
			{
				signatureHeader2 = default(SignatureHeader);
			}
			return signatureHeader2;
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00031540 File Offset: 0x0002F740
		private void DecodeCustomAttributeParameterTypeOrThrow(ref BlobReader sigReader, out SerializationTypeCode typeCode, out TypeSymbol type, out SerializationTypeCode elementTypeCode, out TypeSymbol elementType, bool isElementType)
		{
			SignatureTypeCode signatureTypeCode = sigReader.ReadSignatureTypeCode();
			if (signatureTypeCode == SignatureTypeCode.SZArray)
			{
				if (isElementType)
				{
					throw new UnsupportedSignatureContent();
				}
				SerializationTypeCode serializationTypeCode;
				TypeSymbol typeSymbol;
				this.DecodeCustomAttributeParameterTypeOrThrow(ref sigReader, out elementTypeCode, out elementType, out serializationTypeCode, out typeSymbol, true);
				type = base.GetSZArrayTypeSymbol(elementType, default(ImmutableArray<ModifierInfo<TypeSymbol>>));
				typeCode = SerializationTypeCode.SZArray;
				return;
			}
			else
			{
				elementTypeCode = SerializationTypeCode.Invalid;
				elementType = default(TypeSymbol);
				if (signatureTypeCode - SignatureTypeCode.Boolean <= 12)
				{
					type = base.GetSpecialType(signatureTypeCode.ToSpecialType());
					typeCode = (SerializationTypeCode)signatureTypeCode;
					return;
				}
				if (signatureTypeCode != SignatureTypeCode.Object)
				{
					if (signatureTypeCode == SignatureTypeCode.TypeHandle)
					{
						bool flag;
						type = this.GetSymbolForTypeHandleOrThrow(sigReader.ReadTypeHandle(), out flag, true, true);
						TypeSymbol enumUnderlyingType = base.GetEnumUnderlyingType(type);
						if (enumUnderlyingType != null)
						{
							typeCode = enumUnderlyingType.SpecialType.ToSerializationType();
							return;
						}
						if (type == base.SystemTypeSymbol)
						{
							typeCode = SerializationTypeCode.Type;
							return;
						}
					}
					throw new UnsupportedSignatureContent();
				}
				type = base.GetSpecialType(SpecialType.System_Object);
				typeCode = SerializationTypeCode.TaggedObject;
				return;
			}
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00031644 File Offset: 0x0002F844
		private void DecodeCustomAttributeFieldOrPropTypeOrThrow(ref BlobReader argReader, out SerializationTypeCode typeCode, out TypeSymbol type, out SerializationTypeCode elementTypeCode, out TypeSymbol elementType, bool isElementType)
		{
			typeCode = argReader.ReadSerializationTypeCode();
			if (typeCode != SerializationTypeCode.SZArray)
			{
				elementTypeCode = SerializationTypeCode.Invalid;
				elementType = default(TypeSymbol);
				SerializationTypeCode serializationTypeCode = typeCode;
				if (serializationTypeCode <= SerializationTypeCode.Type)
				{
					if (serializationTypeCode - SerializationTypeCode.Boolean <= 12)
					{
						type = base.GetSpecialType(((SignatureTypeCode)typeCode).ToSpecialType());
						return;
					}
					if (serializationTypeCode == SerializationTypeCode.Type)
					{
						type = base.SystemTypeSymbol;
						return;
					}
				}
				else
				{
					if (serializationTypeCode == SerializationTypeCode.TaggedObject)
					{
						type = base.GetSpecialType(SpecialType.System_Object);
						return;
					}
					if (serializationTypeCode == SerializationTypeCode.Enum)
					{
						string text;
						if (!PEModule.CrackStringInAttributeValue(out text, ref argReader))
						{
							throw new UnsupportedSignatureContent();
						}
						type = base.GetTypeSymbolForSerializedType(text);
						TypeSymbol enumUnderlyingType = base.GetEnumUnderlyingType(type);
						if (enumUnderlyingType == null)
						{
							throw new UnsupportedSignatureContent();
						}
						typeCode = enumUnderlyingType.SpecialType.ToSerializationType();
						return;
					}
				}
				throw new UnsupportedSignatureContent();
			}
			if (isElementType)
			{
				throw new UnsupportedSignatureContent();
			}
			SerializationTypeCode serializationTypeCode2;
			TypeSymbol typeSymbol;
			this.DecodeCustomAttributeFieldOrPropTypeOrThrow(ref argReader, out elementTypeCode, out elementType, out serializationTypeCode2, out typeSymbol, true);
			type = base.GetSZArrayTypeSymbol(elementType, default(ImmutableArray<ModifierInfo<TypeSymbol>>));
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00031754 File Offset: 0x0002F954
		private TypedConstant DecodeCustomAttributeFixedArgumentOrThrow(ref BlobReader sigReader, ref BlobReader argReader)
		{
			SerializationTypeCode serializationTypeCode;
			TypeSymbol typeSymbol;
			SerializationTypeCode serializationTypeCode2;
			TypeSymbol typeSymbol2;
			this.DecodeCustomAttributeParameterTypeOrThrow(ref sigReader, out serializationTypeCode, out typeSymbol, out serializationTypeCode2, out typeSymbol2, false);
			if (serializationTypeCode == SerializationTypeCode.SZArray)
			{
				return this.DecodeCustomAttributeElementArrayOrThrow(ref argReader, serializationTypeCode2, typeSymbol2, typeSymbol);
			}
			return this.DecodeCustomAttributeElementOrThrow(ref argReader, serializationTypeCode, typeSymbol);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0003178C File Offset: 0x0002F98C
		private TypedConstant DecodeCustomAttributeElementOrThrow(ref BlobReader argReader, SerializationTypeCode typeCode, TypeSymbol type)
		{
			if (typeCode == SerializationTypeCode.TaggedObject)
			{
				SerializationTypeCode serializationTypeCode;
				TypeSymbol typeSymbol;
				this.DecodeCustomAttributeFieldOrPropTypeOrThrow(ref argReader, out typeCode, out type, out serializationTypeCode, out typeSymbol, false);
				if (typeCode == SerializationTypeCode.SZArray)
				{
					return this.DecodeCustomAttributeElementArrayOrThrow(ref argReader, serializationTypeCode, typeSymbol, type);
				}
			}
			return this.DecodeCustomAttributePrimitiveElementOrThrow(ref argReader, typeCode, type);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x000317C8 File Offset: 0x0002F9C8
		private TypedConstant DecodeCustomAttributeElementArrayOrThrow(ref BlobReader argReader, SerializationTypeCode elementTypeCode, TypeSymbol elementType, TypeSymbol arrayType)
		{
			int num = argReader.ReadInt32();
			TypedConstant[] array;
			if (num == -1)
			{
				array = null;
			}
			else if (num == 0)
			{
				array = Array.Empty<TypedConstant>();
			}
			else
			{
				array = new TypedConstant[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = this.DecodeCustomAttributeElementOrThrow(ref argReader, elementTypeCode, elementType);
				}
			}
			return MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.CreateArrayTypedConstant(arrayType, array.AsImmutableOrNull<TypedConstant>());
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00031820 File Offset: 0x0002FA20
		private TypedConstant DecodeCustomAttributePrimitiveElementOrThrow(ref BlobReader argReader, SerializationTypeCode typeCode, TypeSymbol type)
		{
			switch (typeCode)
			{
			case SerializationTypeCode.Boolean:
				return MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.CreateTypedConstant(type, MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.GetPrimitiveOrEnumTypedConstantKind(type), argReader.ReadSByte() != 0);
			case SerializationTypeCode.Char:
				return MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.CreateTypedConstant(type, MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.GetPrimitiveOrEnumTypedConstantKind(type), argReader.ReadChar());
			case SerializationTypeCode.SByte:
				return MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.CreateTypedConstant(type, MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.GetPrimitiveOrEnumTypedConstantKind(type), argReader.ReadSByte());
			case SerializationTypeCode.Byte:
				return MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.CreateTypedConstant(type, MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.GetPrimitiveOrEnumTypedConstantKind(type), argReader.ReadByte());
			case SerializationTypeCode.Int16:
				return MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.CreateTypedConstant(type, MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.GetPrimitiveOrEnumTypedConstantKind(type), argReader.ReadInt16());
			case SerializationTypeCode.UInt16:
				return MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.CreateTypedConstant(type, MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.GetPrimitiveOrEnumTypedConstantKind(type), argReader.ReadUInt16());
			case SerializationTypeCode.Int32:
				return MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.CreateTypedConstant(type, MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.GetPrimitiveOrEnumTypedConstantKind(type), argReader.ReadInt32());
			case SerializationTypeCode.UInt32:
				return MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.CreateTypedConstant(type, MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.GetPrimitiveOrEnumTypedConstantKind(type), argReader.ReadUInt32());
			case SerializationTypeCode.Int64:
				return MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.CreateTypedConstant(type, MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.GetPrimitiveOrEnumTypedConstantKind(type), argReader.ReadInt64());
			case SerializationTypeCode.UInt64:
				return MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.CreateTypedConstant(type, MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.GetPrimitiveOrEnumTypedConstantKind(type), argReader.ReadUInt64());
			case SerializationTypeCode.Single:
				return MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.CreateTypedConstant(type, MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.GetPrimitiveOrEnumTypedConstantKind(type), argReader.ReadSingle());
			case SerializationTypeCode.Double:
				return MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.CreateTypedConstant(type, MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.GetPrimitiveOrEnumTypedConstantKind(type), argReader.ReadDouble());
			case SerializationTypeCode.String:
			{
				string text;
				TypedConstantKind typedConstantKind = (PEModule.CrackStringInAttributeValue(out text, ref argReader) ? TypedConstantKind.Primitive : TypedConstantKind.Error);
				return MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.CreateTypedConstant(type, typedConstantKind, text);
			}
			default:
			{
				if (typeCode != SerializationTypeCode.Type)
				{
					throw new UnsupportedSignatureContent();
				}
				string text2;
				TypeSymbol typeSymbol = (PEModule.CrackStringInAttributeValue(out text2, ref argReader) ? ((text2 != null) ? base.GetTypeSymbolForSerializedType(text2) : default(TypeSymbol)) : base.GetUnsupportedMetadataTypeSymbol(null));
				return MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.CreateTypedConstant(type, TypedConstantKind.Type, typeSymbol);
			}
			}
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x000319E9 File Offset: 0x0002FBE9
		private static TypedConstantKind GetPrimitiveOrEnumTypedConstantKind(TypeSymbol type)
		{
			if (type.TypeKind != TypeKind.Enum)
			{
				return TypedConstantKind.Primitive;
			}
			return TypedConstantKind.Enum;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x000319FC File Offset: 0x0002FBFC
		[return: TupleElementNames(new string[] { "nameValuePair", "isProperty", "typeCode", "elementTypeCode" })]
		public ValueTuple<KeyValuePair<string, TypedConstant>, bool, SerializationTypeCode, SerializationTypeCode> DecodeCustomAttributeNamedArgumentOrThrow(ref BlobReader argReader)
		{
			CustomAttributeNamedArgumentKind customAttributeNamedArgumentKind = (CustomAttributeNamedArgumentKind)argReader.ReadCompressedInteger();
			if (customAttributeNamedArgumentKind != CustomAttributeNamedArgumentKind.Field && customAttributeNamedArgumentKind != CustomAttributeNamedArgumentKind.Property)
			{
				throw new UnsupportedSignatureContent();
			}
			SerializationTypeCode serializationTypeCode;
			TypeSymbol typeSymbol;
			SerializationTypeCode serializationTypeCode2;
			TypeSymbol typeSymbol2;
			this.DecodeCustomAttributeFieldOrPropTypeOrThrow(ref argReader, out serializationTypeCode, out typeSymbol, out serializationTypeCode2, out typeSymbol2, false);
			string text;
			if (!PEModule.CrackStringInAttributeValue(out text, ref argReader))
			{
				throw new UnsupportedSignatureContent();
			}
			TypedConstant typedConstant = ((serializationTypeCode == SerializationTypeCode.SZArray) ? this.DecodeCustomAttributeElementArrayOrThrow(ref argReader, serializationTypeCode2, typeSymbol2, typeSymbol) : this.DecodeCustomAttributeElementOrThrow(ref argReader, serializationTypeCode, typeSymbol));
			return new ValueTuple<KeyValuePair<string, TypedConstant>, bool, SerializationTypeCode, SerializationTypeCode>(new KeyValuePair<string, TypedConstant>(text, typedConstant), customAttributeNamedArgumentKind == CustomAttributeNamedArgumentKind.Property, serializationTypeCode, serializationTypeCode2);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00031A74 File Offset: 0x0002FC74
		internal bool IsTargetAttribute(CustomAttributeHandle customAttribute, string namespaceName, string typeName, bool ignoreCase = false)
		{
			bool flag;
			try
			{
				EntityHandle entityHandle;
				flag = this.Module.IsTargetAttribute(customAttribute, namespaceName, typeName, out entityHandle, ignoreCase);
			}
			catch (BadImageFormatException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00031AAC File Offset: 0x0002FCAC
		internal int GetTargetAttributeSignatureIndex(CustomAttributeHandle customAttribute, AttributeDescription description)
		{
			int num;
			try
			{
				num = this.Module.GetTargetAttributeSignatureIndex(customAttribute, description);
			}
			catch (BadImageFormatException)
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00031AE0 File Offset: 0x0002FCE0
		internal bool GetCustomAttribute(CustomAttributeHandle handle, out TypedConstant[] positionalArgs, out KeyValuePair<string, TypedConstant>[] namedArgs)
		{
			try
			{
				positionalArgs = Array.Empty<TypedConstant>();
				namedArgs = Array.Empty<KeyValuePair<string, TypedConstant>>();
				EntityHandle entityHandle;
				EntityHandle entityHandle2;
				if (this.Module.GetTypeAndConstructor(handle, out entityHandle, out entityHandle2))
				{
					BlobReader memoryReaderOrThrow = this.Module.GetMemoryReaderOrThrow(this.Module.GetCustomAttributeValueOrThrow(handle));
					BlobReader memoryReaderOrThrow2 = this.Module.GetMemoryReaderOrThrow(this.Module.GetMethodSignatureOrThrow(entityHandle2));
					if (memoryReaderOrThrow.ReadUInt16() != 1)
					{
						return false;
					}
					if (memoryReaderOrThrow2.ReadSignatureHeader().IsGeneric && memoryReaderOrThrow2.ReadCompressedInteger() != 0)
					{
						return false;
					}
					int num = memoryReaderOrThrow2.ReadCompressedInteger();
					if (memoryReaderOrThrow2.ReadSignatureTypeCode() != SignatureTypeCode.Void)
					{
						return false;
					}
					if (num > 0)
					{
						positionalArgs = new TypedConstant[num];
						for (int i = 0; i < positionalArgs.Length; i++)
						{
							positionalArgs[i] = this.DecodeCustomAttributeFixedArgumentOrThrow(ref memoryReaderOrThrow2, ref memoryReaderOrThrow);
						}
					}
					short num2 = memoryReaderOrThrow.ReadInt16();
					if (num2 > 0)
					{
						namedArgs = new KeyValuePair<string, TypedConstant>[(int)num2];
						for (int j = 0; j < namedArgs.Length; j++)
						{
							KeyValuePair<string, TypedConstant>[] array = namedArgs;
							int num3 = j;
							ValueTuple<KeyValuePair<string, TypedConstant>, bool, SerializationTypeCode, SerializationTypeCode> valueTuple = this.DecodeCustomAttributeNamedArgumentOrThrow(ref memoryReaderOrThrow);
							array[num3] = valueTuple.Item1;
						}
					}
					return true;
				}
			}
			catch (Exception ex) when (ex is UnsupportedSignatureContent || ex is BadImageFormatException)
			{
				positionalArgs = Array.Empty<TypedConstant>();
				namedArgs = Array.Empty<KeyValuePair<string, TypedConstant>>();
			}
			return false;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00031C70 File Offset: 0x0002FE70
		[NullableContext(2)]
		internal bool GetCustomAttribute(CustomAttributeHandle handle, [NotNullWhen(true)] out TypeSymbol attributeClass, [NotNullWhen(true)] out MethodSymbol attributeCtor)
		{
			EntityHandle entityHandle;
			EntityHandle entityHandle2;
			try
			{
				if (!this.Module.GetTypeAndConstructor(handle, out entityHandle, out entityHandle2))
				{
					attributeClass = default(TypeSymbol);
					attributeCtor = default(MethodSymbol);
					return false;
				}
			}
			catch (BadImageFormatException)
			{
				attributeClass = default(TypeSymbol);
				attributeCtor = default(MethodSymbol);
				return false;
			}
			attributeClass = this.GetTypeOfToken(entityHandle);
			attributeCtor = this.GetMethodSymbolForMethodDefOrMemberRef(entityHandle2, attributeClass);
			return true;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00031CEC File Offset: 0x0002FEEC
		internal bool GetCustomAttributeWellKnownType(CustomAttributeHandle handle, out WellKnownType wellKnownAttribute)
		{
			wellKnownAttribute = WellKnownType.Unknown;
			bool flag;
			try
			{
				EntityHandle entityHandle;
				EntityHandle entityHandle2;
				StringHandle stringHandle;
				StringHandle stringHandle2;
				if (!this.Module.GetTypeAndConstructor(handle, out entityHandle, out entityHandle2))
				{
					flag = false;
				}
				else if (!this.Module.GetAttributeNamespaceAndName(entityHandle, out stringHandle, out stringHandle2))
				{
					flag = false;
				}
				else
				{
					string fullNameOrThrow = this.Module.GetFullNameOrThrow(stringHandle, stringHandle2);
					wellKnownAttribute = WellKnownTypes.GetTypeFromMetadataName(fullNameOrThrow);
					flag = true;
				}
			}
			catch (BadImageFormatException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00031D60 File Offset: 0x0002FF60
		private TypeSymbol[] DecodeMethodSpecTypeArgumentsOrThrow(BlobHandle signature)
		{
			SignatureHeader signatureHeader;
			BlobReader blobReader = this.DecodeSignatureHeaderOrThrow(signature, out signatureHeader);
			if (signatureHeader.Kind != SignatureKind.MethodSpecification)
			{
				throw new BadImageFormatException();
			}
			int num = blobReader.ReadCompressedInteger();
			if (num == 0)
			{
				throw new BadImageFormatException();
			}
			TypeSymbol[] array = new TypeSymbol[num];
			for (int i = 0; i < array.Length; i++)
			{
				bool flag;
				array[i] = this.DecodeTypeOrThrow(ref blobReader, out flag);
			}
			return array;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00031DBE File Offset: 0x0002FFBE
		internal BlobReader DecodeSignatureHeaderOrThrow(BlobHandle signature, out SignatureHeader signatureHeader)
		{
			return MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.DecodeSignatureHeaderOrThrow(this.Module, signature, out signatureHeader);
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00031DD0 File Offset: 0x0002FFD0
		internal static BlobReader DecodeSignatureHeaderOrThrow(PEModule module, BlobHandle signature, out SignatureHeader signatureHeader)
		{
			BlobReader memoryReaderOrThrow = module.GetMemoryReaderOrThrow(signature);
			signatureHeader = memoryReaderOrThrow.ReadSignatureHeader();
			return memoryReaderOrThrow;
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00031DF4 File Offset: 0x0002FFF4
		protected ParamInfo<TypeSymbol>[] DecodeSignatureParametersOrThrow(ref BlobReader signatureReader, SignatureHeader signatureHeader, out int typeParameterCount, bool shouldProcessAllBytes = true, bool isFunctionPointerSignature = false)
		{
			int num;
			MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.GetSignatureCountsOrThrow(ref signatureReader, signatureHeader, out num, out typeParameterCount);
			ParamInfo<TypeSymbol>[] array = new ParamInfo<TypeSymbol>[num + 1];
			uint num2 = 0U;
			try
			{
				this.DecodeParameterOrThrow(ref signatureReader, ref array[0]);
				num2 = 1U;
				while ((ulong)num2 <= (ulong)((long)num))
				{
					this.DecodeParameterOrThrow(ref signatureReader, ref array[(int)num2]);
					num2 += 1U;
				}
				if (shouldProcessAllBytes && signatureReader.RemainingBytes > 0)
				{
					throw new UnsupportedSignatureContent();
				}
			}
			catch (Exception ex) when ((ex is UnsupportedSignatureContent || ex is BadImageFormatException) && !isFunctionPointerSignature)
			{
				while ((ulong)num2 <= (ulong)((long)num))
				{
					array[(int)num2].Type = base.GetUnsupportedMetadataTypeSymbol(ex as BadImageFormatException);
					num2 += 1U;
				}
			}
			return array;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00031EB8 File Offset: 0x000300B8
		private static void GetSignatureCountsOrThrow(ref BlobReader signatureReader, SignatureHeader signatureHeader, out int parameterCount, out int typeParameterCount)
		{
			typeParameterCount = (signatureHeader.IsGeneric ? signatureReader.ReadCompressedInteger() : 0);
			parameterCount = signatureReader.ReadCompressedInteger();
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00031ED8 File Offset: 0x000300D8
		internal TypeSymbol DecodeFieldSignature(FieldDefinitionHandle fieldHandle, out ImmutableArray<ModifierInfo<TypeSymbol>> customModifiers)
		{
			TypeSymbol typeSymbol;
			try
			{
				BlobHandle fieldSignatureOrThrow = this.Module.GetFieldSignatureOrThrow(fieldHandle);
				SignatureHeader signatureHeader;
				BlobReader blobReader = this.DecodeSignatureHeaderOrThrow(fieldSignatureOrThrow, out signatureHeader);
				if (signatureHeader.Kind != SignatureKind.Field)
				{
					customModifiers = default(ImmutableArray<ModifierInfo<TypeSymbol>>);
					typeSymbol = base.GetUnsupportedMetadataTypeSymbol(null);
				}
				else
				{
					typeSymbol = this.DecodeFieldSignature(ref blobReader, out customModifiers);
				}
			}
			catch (BadImageFormatException ex)
			{
				customModifiers = default(ImmutableArray<ModifierInfo<TypeSymbol>>);
				typeSymbol = base.GetUnsupportedMetadataTypeSymbol(ex);
			}
			return typeSymbol;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00031F48 File Offset: 0x00030148
		protected TypeSymbol DecodeFieldSignature(ref BlobReader signatureReader, out ImmutableArray<ModifierInfo<TypeSymbol>> customModifiers)
		{
			customModifiers = default(ImmutableArray<ModifierInfo<TypeSymbol>>);
			TypeSymbol typeSymbol;
			try
			{
				SignatureTypeCode signatureTypeCode;
				customModifiers = this.DecodeModifiersOrThrow(ref signatureReader, out signatureTypeCode);
				bool flag;
				typeSymbol = this.DecodeTypeOrThrow(ref signatureReader, signatureTypeCode, out flag);
			}
			catch (UnsupportedSignatureContent)
			{
				typeSymbol = base.GetUnsupportedMetadataTypeSymbol(null);
			}
			catch (BadImageFormatException ex)
			{
				typeSymbol = base.GetUnsupportedMetadataTypeSymbol(ex);
			}
			return typeSymbol;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00031FAC File Offset: 0x000301AC
		internal ImmutableArray<MethodSymbol> GetExplicitlyOverriddenMethods(TypeDefinitionHandle implementingTypeDef, MethodDefinitionHandle implementingMethodDef, TypeSymbol implementingTypeSymbol)
		{
			ArrayBuilder<MethodSymbol> instance = ArrayBuilder<MethodSymbol>.GetInstance();
			try
			{
				foreach (MethodImplementationHandle methodImplementationHandle in this.Module.GetMethodImplementationsOrThrow(implementingTypeDef))
				{
					EntityHandle entityHandle;
					EntityHandle entityHandle2;
					this.Module.GetMethodImplPropsOrThrow(methodImplementationHandle, out entityHandle, out entityHandle2);
					if (entityHandle.Kind == HandleKind.MemberReference)
					{
						MethodSymbol methodSymbolForMemberRef = this.GetMethodSymbolForMemberRef((MemberReferenceHandle)entityHandle, implementingTypeSymbol);
						if (methodSymbolForMemberRef != null)
						{
							entityHandle = this.GetMethodHandle(methodSymbolForMemberRef);
						}
					}
					if (entityHandle == implementingMethodDef && !entityHandle2.IsNil)
					{
						HandleKind kind = entityHandle2.Kind;
						MethodSymbol methodSymbol = default(MethodSymbol);
						if (kind == HandleKind.MethodDefinition)
						{
							methodSymbol = this.FindMethodSymbolInSuperType(implementingTypeDef, (MethodDefinitionHandle)entityHandle2);
						}
						else if (kind == HandleKind.MemberReference)
						{
							methodSymbol = this.GetMethodSymbolForMemberRef((MemberReferenceHandle)entityHandle2, implementingTypeSymbol);
						}
						if (methodSymbol != null)
						{
							instance.Add(methodSymbol);
						}
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x000320D0 File Offset: 0x000302D0
		private MethodSymbol FindMethodSymbolInSuperType(TypeDefinitionHandle searchTypeDef, MethodDefinitionHandle targetMethodDef)
		{
			try
			{
				Queue<TypeDefinitionHandle> queue = new Queue<TypeDefinitionHandle>();
				Queue<TypeSymbol> queue2 = new Queue<TypeSymbol>();
				this.EnqueueTypeDefInterfacesAndBaseTypeOrThrow(queue, queue2, searchTypeDef);
				HashSet<TypeDefinitionHandle> hashSet = new HashSet<TypeDefinitionHandle>();
				HashSet<TypeSymbol> hashSet2 = new HashSet<TypeSymbol>();
				bool flag;
				while ((flag = queue.Count > 0) || queue2.Count > 0)
				{
					if (flag)
					{
						TypeDefinitionHandle typeDefinitionHandle = queue.Dequeue();
						if (!hashSet.Contains(typeDefinitionHandle))
						{
							hashSet.Add(typeDefinitionHandle);
							using (MethodDefinitionHandleCollection.Enumerator enumerator = this.Module.GetMethodsOfTypeOrThrow(typeDefinitionHandle).GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									if (enumerator.Current == targetMethodDef)
									{
										TypeSymbol typeOfToken = this.GetTypeOfToken(typeDefinitionHandle);
										return this.FindMethodSymbolInType(typeOfToken, targetMethodDef);
									}
								}
							}
							this.EnqueueTypeDefInterfacesAndBaseTypeOrThrow(queue, queue2, typeDefinitionHandle);
						}
					}
					else
					{
						TypeSymbol typeSymbol = queue2.Dequeue();
						if (!hashSet2.Contains(typeSymbol))
						{
							hashSet2.Add(typeSymbol);
							this.EnqueueTypeSymbolInterfacesAndBaseTypes(queue, queue2, typeSymbol);
						}
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			return default(MethodSymbol);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00032204 File Offset: 0x00030404
		private void EnqueueTypeDefInterfacesAndBaseTypeOrThrow(Queue<TypeDefinitionHandle> typeDefsToSearch, Queue<TypeSymbol> typeSymbolsToSearch, TypeDefinitionHandle searchTypeDef)
		{
			foreach (InterfaceImplementationHandle interfaceImplementationHandle in this.Module.GetInterfaceImplementationsOrThrow(searchTypeDef))
			{
				this.EnqueueTypeToken(typeDefsToSearch, typeSymbolsToSearch, this.Module.MetadataReader.GetInterfaceImplementation(interfaceImplementationHandle).Interface);
			}
			this.EnqueueTypeToken(typeDefsToSearch, typeSymbolsToSearch, this.Module.GetBaseTypeOfTypeOrThrow(searchTypeDef));
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00032290 File Offset: 0x00030490
		private void EnqueueTypeToken(Queue<TypeDefinitionHandle> typeDefsToSearch, Queue<TypeSymbol> typeSymbolsToSearch, EntityHandle typeToken)
		{
			if (!typeToken.IsNil)
			{
				if (typeToken.Kind == HandleKind.TypeDefinition)
				{
					typeDefsToSearch.Enqueue((TypeDefinitionHandle)typeToken);
					return;
				}
				this.EnqueueTypeSymbol(typeDefsToSearch, typeSymbolsToSearch, this.GetTypeOfToken(typeToken));
			}
		}

		// Token: 0x060010BD RID: 4285
		protected abstract void EnqueueTypeSymbolInterfacesAndBaseTypes(Queue<TypeDefinitionHandle> typeDefsToSearch, Queue<TypeSymbol> typeSymbolsToSearch, TypeSymbol typeSymbol);

		// Token: 0x060010BE RID: 4286
		protected abstract void EnqueueTypeSymbol(Queue<TypeDefinitionHandle> typeDefsToSearch, Queue<TypeSymbol> typeSymbolsToSearch, TypeSymbol typeSymbol);

		// Token: 0x060010BF RID: 4287
		protected abstract MethodSymbol FindMethodSymbolInType(TypeSymbol type, MethodDefinitionHandle methodDef);

		// Token: 0x060010C0 RID: 4288
		protected abstract FieldSymbol FindFieldSymbolInType(TypeSymbol type, FieldDefinitionHandle fieldDef);

		// Token: 0x060010C1 RID: 4289
		internal abstract Symbol GetSymbolForMemberRef(MemberReferenceHandle memberRef, TypeSymbol implementingTypeSymbol = default(TypeSymbol), bool methodsOnly = false);

		// Token: 0x060010C2 RID: 4290 RVA: 0x000322C1 File Offset: 0x000304C1
		internal MethodSymbol GetMethodSymbolForMemberRef(MemberReferenceHandle methodRef, TypeSymbol implementingTypeSymbol)
		{
			return (MethodSymbol)((object)this.GetSymbolForMemberRef(methodRef, implementingTypeSymbol, true));
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x000322D6 File Offset: 0x000304D6
		internal FieldSymbol GetFieldSymbolForMemberRef(MemberReferenceHandle methodRef, TypeSymbol implementingTypeSymbol)
		{
			return (FieldSymbol)((object)this.GetSymbolForMemberRef(methodRef, implementingTypeSymbol, true));
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x000322EB File Offset: 0x000304EB
		protected override bool IsContainingAssembly(AssemblyIdentity identity)
		{
			return this._containingAssemblyIdentity != null && this._containingAssemblyIdentity.Equals(identity);
		}

		// Token: 0x060010C5 RID: 4293
		protected abstract MethodDefinitionHandle GetMethodHandle(MethodSymbol method);

		// Token: 0x060010C6 RID: 4294
		protected abstract ConcurrentDictionary<TypeDefinitionHandle, TypeSymbol> GetTypeHandleToTypeMap();

		// Token: 0x060010C7 RID: 4295
		protected abstract ConcurrentDictionary<TypeReferenceHandle, TypeSymbol> GetTypeRefHandleToTypeMap();

		// Token: 0x060010C8 RID: 4296
		protected abstract TypeSymbol SubstituteNoPiaLocalType(TypeDefinitionHandle typeDef, ref MetadataTypeName name, string interfaceGuid, string scope, string identifier);

		// Token: 0x060010C9 RID: 4297
		protected abstract TypeSymbol LookupTopLevelTypeDefSymbol(string moduleName, ref MetadataTypeName emittedName, out bool isNoPiaLocalType);

		// Token: 0x060010CA RID: 4298
		protected abstract TypeSymbol GetGenericTypeParamSymbol(int position);

		// Token: 0x060010CB RID: 4299
		protected abstract TypeSymbol GetGenericMethodTypeParamSymbol(int position);

		// Token: 0x060010CC RID: 4300 RVA: 0x00032309 File Offset: 0x00030509
		private static TypedConstant CreateArrayTypedConstant(TypeSymbol type, ImmutableArray<TypedConstant> array)
		{
			if (type.TypeKind == TypeKind.Error)
			{
				return new TypedConstant(type, TypedConstantKind.Error, null);
			}
			return new TypedConstant(type, array);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00032333 File Offset: 0x00030533
		private static TypedConstant CreateTypedConstant(TypeSymbol type, TypedConstantKind kind, object value)
		{
			if (type.TypeKind == TypeKind.Error)
			{
				return new TypedConstant(type, TypedConstantKind.Error, null);
			}
			return new TypedConstant(type, kind, value);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0003235E File Offset: 0x0003055E
		private static TypedConstant CreateTypedConstant(TypeSymbol type, TypedConstantKind kind, bool value)
		{
			return MetadataDecoder<ModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.CreateTypedConstant(type, kind, Boxes.Box(value));
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00032370 File Offset: 0x00030570
		internal Symbol GetSymbolForILToken(EntityHandle token)
		{
			try
			{
				HandleKind kind = token.Kind;
				if (kind <= HandleKind.MemberReference)
				{
					switch (kind)
					{
					case HandleKind.TypeReference:
					case HandleKind.TypeDefinition:
						break;
					case (HandleKind)3:
					case (HandleKind)5:
						goto IL_01C8;
					case HandleKind.FieldDefinition:
					{
						TypeDefinitionHandle typeDefinitionHandle = this.Module.FindContainingTypeOrThrow((FieldDefinitionHandle)token);
						if (typeDefinitionHandle.IsNil)
						{
							return default(Symbol);
						}
						TypeSymbol typeOfToken = this.GetTypeOfToken(typeDefinitionHandle);
						if (typeOfToken == null)
						{
							return default(Symbol);
						}
						return (Symbol)((object)this.GetFieldSymbolForFieldDefOrMemberRef(token, typeOfToken));
					}
					case HandleKind.MethodDefinition:
					{
						TypeDefinitionHandle typeDefinitionHandle2 = this.Module.FindContainingTypeOrThrow((MethodDefinitionHandle)token);
						if (typeDefinitionHandle2.IsNil)
						{
							return default(Symbol);
						}
						TypeSymbol typeOfTypeDef = this.GetTypeOfTypeDef(typeDefinitionHandle2);
						if (typeOfTypeDef == null)
						{
							return default(Symbol);
						}
						return (Symbol)((object)this.GetMethodSymbolForMethodDefOrMemberRef(token, typeOfTypeDef));
					}
					default:
						if (kind != HandleKind.MemberReference)
						{
							goto IL_01C8;
						}
						return this.GetSymbolForMemberRef((MemberReferenceHandle)token, default(TypeSymbol), false);
					}
				}
				else if (kind != HandleKind.TypeSpecification)
				{
					if (kind != HandleKind.MethodSpecification)
					{
						goto IL_01C8;
					}
					EntityHandle entityHandle;
					BlobHandle blobHandle;
					this.Module.GetMethodSpecificationOrThrow((MethodSpecificationHandle)token, out entityHandle, out blobHandle);
					MethodSymbol methodSymbol = (MethodSymbol)((object)this.GetSymbolForILToken(entityHandle));
					if (methodSymbol == null)
					{
						return default(Symbol);
					}
					TypeSymbol[] array = this.DecodeMethodSpecTypeArgumentsOrThrow(blobHandle);
					IMethodSymbolInternal methodSymbolInternal = methodSymbol;
					ITypeSymbolInternal[] array2 = array;
					return (Symbol)((object)((MethodSymbol)((object)methodSymbolInternal.Construct(array2))));
				}
				return (Symbol)((object)this.GetTypeOfToken(token));
				IL_01C8:;
			}
			catch (BadImageFormatException)
			{
			}
			return default(Symbol);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00032574 File Offset: 0x00030774
		internal TypeSymbol GetMemberRefTypeSymbol(MemberReferenceHandle memberRef)
		{
			TypeSymbol typeSymbol;
			try
			{
				EntityHandle containingTypeOrThrow = this.Module.GetContainingTypeOrThrow(memberRef);
				HandleKind kind = containingTypeOrThrow.Kind;
				if (kind != HandleKind.TypeDefinition && kind != HandleKind.TypeReference && kind != HandleKind.TypeSpecification)
				{
					typeSymbol = default(TypeSymbol);
					typeSymbol = typeSymbol;
				}
				else
				{
					typeSymbol = this.GetTypeOfToken(containingTypeOrThrow);
				}
			}
			catch (BadImageFormatException)
			{
				typeSymbol = default(TypeSymbol);
			}
			return typeSymbol;
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x000325D8 File Offset: 0x000307D8
		internal MethodSymbol GetMethodSymbolForMethodDefOrMemberRef(EntityHandle memberToken, TypeSymbol container)
		{
			if (memberToken.Kind != HandleKind.MethodDefinition)
			{
				return this.GetMethodSymbolForMemberRef((MemberReferenceHandle)memberToken, container);
			}
			return this.FindMethodSymbolInType(container, (MethodDefinitionHandle)memberToken);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x000325FF File Offset: 0x000307FF
		internal FieldSymbol GetFieldSymbolForFieldDefOrMemberRef(EntityHandle memberToken, TypeSymbol container)
		{
			if (memberToken.Kind != HandleKind.FieldDefinition)
			{
				return this.GetFieldSymbolForMemberRef((MemberReferenceHandle)memberToken, container);
			}
			return this.FindFieldSymbolInType(container, (FieldDefinitionHandle)memberToken);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00032628 File Offset: 0x00030828
		internal bool DoPropertySignaturesMatch(ParamInfo<TypeSymbol>[] signature1, ParamInfo<TypeSymbol>[] signature2, bool comparingToSetter, bool compareParamByRef, bool compareReturnType)
		{
			int num = (comparingToSetter ? 1 : 0);
			if (signature2.Length - num != signature1.Length)
			{
				return false;
			}
			if (comparingToSetter && base.GetPrimitiveTypeCode(signature2[0].Type) != Microsoft.Cci.PrimitiveTypeCode.Void)
			{
				return false;
			}
			for (int i = (compareReturnType ? 0 : 1); i < signature1.Length; i++)
			{
				int num2 = ((i == 0 && comparingToSetter) ? signature1.Length : i);
				ParamInfo<TypeSymbol> paramInfo = signature1[i];
				ParamInfo<TypeSymbol> paramInfo2 = signature2[num2];
				if (compareParamByRef && paramInfo2.IsByRef != paramInfo.IsByRef)
				{
					return false;
				}
				if (!paramInfo2.Type.Equals(paramInfo.Type, TypeCompareKind.ConsiderEverything))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x000326D0 File Offset: 0x000308D0
		internal bool DoesSignatureMatchEvent(TypeSymbol eventType, ParamInfo<TypeSymbol>[] methodParams)
		{
			if (methodParams.Length != 2)
			{
				return false;
			}
			if (base.GetPrimitiveTypeCode(methodParams[0].Type) != Microsoft.Cci.PrimitiveTypeCode.Void)
			{
				return false;
			}
			ParamInfo<TypeSymbol> paramInfo = methodParams[1];
			return !paramInfo.IsByRef && paramInfo.Type.Equals(eventType);
		}

		// Token: 0x04000528 RID: 1320
		public readonly PEModule Module;

		// Token: 0x04000529 RID: 1321
		private readonly AssemblyIdentity _containingAssemblyIdentity;
	}
}
