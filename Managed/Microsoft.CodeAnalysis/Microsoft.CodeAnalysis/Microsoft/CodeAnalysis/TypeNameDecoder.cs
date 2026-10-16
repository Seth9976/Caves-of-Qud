using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001AE RID: 430
	internal abstract class TypeNameDecoder<ModuleSymbol, TypeSymbol> where ModuleSymbol : class where TypeSymbol : class
	{
		// Token: 0x060011F1 RID: 4593 RVA: 0x000372CB File Offset: 0x000354CB
		internal TypeNameDecoder(SymbolFactory<ModuleSymbol, TypeSymbol> factory, ModuleSymbol moduleSymbol)
		{
			this._factory = factory;
			this.moduleSymbol = moduleSymbol;
		}

		// Token: 0x060011F2 RID: 4594
		protected abstract bool IsContainingAssembly(AssemblyIdentity identity);

		// Token: 0x060011F3 RID: 4595
		protected abstract TypeSymbol LookupTopLevelTypeDefSymbol(ref MetadataTypeName emittedName, out bool isNoPiaLocalType);

		// Token: 0x060011F4 RID: 4596
		protected abstract TypeSymbol LookupTopLevelTypeDefSymbol(int referencedAssemblyIndex, ref MetadataTypeName emittedName);

		// Token: 0x060011F5 RID: 4597
		protected abstract TypeSymbol LookupNestedTypeDefSymbol(TypeSymbol container, ref MetadataTypeName emittedName);

		// Token: 0x060011F6 RID: 4598
		protected abstract int GetIndexOfReferencedAssembly(AssemblyIdentity identity);

		// Token: 0x060011F7 RID: 4599 RVA: 0x000372E4 File Offset: 0x000354E4
		internal TypeSymbol GetTypeSymbolForSerializedType(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return this.GetUnsupportedMetadataTypeSymbol(null);
			}
			MetadataHelpers.AssemblyQualifiedTypeName assemblyQualifiedTypeName = MetadataHelpers.DecodeTypeName(s);
			bool flag;
			return this.GetTypeSymbol(assemblyQualifiedTypeName, out flag);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00037311 File Offset: 0x00035511
		protected TypeSymbol GetUnsupportedMetadataTypeSymbol(BadImageFormatException exception = null)
		{
			return this._factory.GetUnsupportedMetadataTypeSymbol(this.moduleSymbol, exception);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00037325 File Offset: 0x00035525
		protected TypeSymbol GetSZArrayTypeSymbol(TypeSymbol elementType, ImmutableArray<ModifierInfo<TypeSymbol>> customModifiers)
		{
			return this._factory.GetSZArrayTypeSymbol(this.moduleSymbol, elementType, customModifiers);
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0003733A File Offset: 0x0003553A
		protected TypeSymbol GetMDArrayTypeSymbol(int rank, TypeSymbol elementType, ImmutableArray<ModifierInfo<TypeSymbol>> customModifiers, ImmutableArray<int> sizes, ImmutableArray<int> lowerBounds)
		{
			return this._factory.GetMDArrayTypeSymbol(this.moduleSymbol, rank, elementType, customModifiers, sizes, lowerBounds);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00037354 File Offset: 0x00035554
		protected TypeSymbol MakePointerTypeSymbol(TypeSymbol type, ImmutableArray<ModifierInfo<TypeSymbol>> customModifiers)
		{
			return this._factory.MakePointerTypeSymbol(this.moduleSymbol, type, customModifiers);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00037369 File Offset: 0x00035569
		protected TypeSymbol MakeFunctionPointerTypeSymbol(CallingConvention callingConvention, ImmutableArray<ParamInfo<TypeSymbol>> retAndParamInfos)
		{
			return this._factory.MakeFunctionPointerTypeSymbol(callingConvention, retAndParamInfos);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00037378 File Offset: 0x00035578
		protected TypeSymbol GetSpecialType(SpecialType specialType)
		{
			return this._factory.GetSpecialType(this.moduleSymbol, specialType);
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0003738C File Offset: 0x0003558C
		protected TypeSymbol SystemTypeSymbol
		{
			get
			{
				return this._factory.GetSystemTypeSymbol(this.moduleSymbol);
			}
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0003739F File Offset: 0x0003559F
		protected TypeSymbol GetEnumUnderlyingType(TypeSymbol type)
		{
			return this._factory.GetEnumUnderlyingType(this.moduleSymbol, type);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x000373B3 File Offset: 0x000355B3
		protected PrimitiveTypeCode GetPrimitiveTypeCode(TypeSymbol type)
		{
			return this._factory.GetPrimitiveTypeCode(this.moduleSymbol, type);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x000373C7 File Offset: 0x000355C7
		protected TypeSymbol SubstituteWithUnboundIfGeneric(TypeSymbol type)
		{
			return this._factory.MakeUnboundIfGeneric(this.moduleSymbol, type);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x000373DB File Offset: 0x000355DB
		protected TypeSymbol SubstituteTypeParameters(TypeSymbol genericType, ImmutableArray<KeyValuePair<TypeSymbol, ImmutableArray<ModifierInfo<TypeSymbol>>>> arguments, ImmutableArray<bool> refersToNoPiaLocalType)
		{
			return this._factory.SubstituteTypeParameters(this.moduleSymbol, genericType, arguments, refersToNoPiaLocalType);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x000373F4 File Offset: 0x000355F4
		internal TypeSymbol GetTypeSymbol(MetadataHelpers.AssemblyQualifiedTypeName fullName, out bool refersToNoPiaLocalType)
		{
			int num;
			if (fullName.AssemblyName != null)
			{
				AssemblyIdentity assemblyIdentity;
				if (!AssemblyIdentity.TryParseDisplayName(fullName.AssemblyName, out assemblyIdentity))
				{
					refersToNoPiaLocalType = false;
					return this.GetUnsupportedMetadataTypeSymbol(null);
				}
				num = this.GetIndexOfReferencedAssembly(assemblyIdentity);
				if (num == -1 && !this.IsContainingAssembly(assemblyIdentity))
				{
					refersToNoPiaLocalType = false;
					return this.GetUnsupportedMetadataTypeSymbol(null);
				}
			}
			else
			{
				num = -1;
			}
			MetadataTypeName metadataTypeName = MetadataTypeName.FromFullName(fullName.TopLevelType, false, -1);
			TypeSymbol typeSymbol = this.LookupTopLevelTypeDefSymbol(ref metadataTypeName, num, out refersToNoPiaLocalType);
			if (fullName.NestedTypes != null)
			{
				if (refersToNoPiaLocalType)
				{
					refersToNoPiaLocalType = false;
					return this.GetUnsupportedMetadataTypeSymbol(null);
				}
				for (int i = 0; i < fullName.NestedTypes.Length; i++)
				{
					metadataTypeName = MetadataTypeName.FromTypeName(fullName.NestedTypes[i], false, -1);
					typeSymbol = this.LookupNestedTypeDefSymbol(typeSymbol, ref metadataTypeName);
				}
			}
			if (fullName.TypeArguments != null)
			{
				ImmutableArray<bool> immutableArray2;
				ImmutableArray<KeyValuePair<TypeSymbol, ImmutableArray<ModifierInfo<TypeSymbol>>>> immutableArray = this.ResolveTypeArguments(fullName.TypeArguments, out immutableArray2);
				typeSymbol = this.SubstituteTypeParameters(typeSymbol, immutableArray, immutableArray2);
				ImmutableArray<bool>.Enumerator enumerator = immutableArray2.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current)
					{
						refersToNoPiaLocalType = true;
						break;
					}
				}
			}
			else
			{
				typeSymbol = this.SubstituteWithUnboundIfGeneric(typeSymbol);
			}
			for (int j = 0; j < fullName.PointerCount; j++)
			{
				typeSymbol = this.MakePointerTypeSymbol(typeSymbol, ImmutableArray<ModifierInfo<TypeSymbol>>.Empty);
			}
			if (fullName.ArrayRanks != null)
			{
				foreach (int num2 in fullName.ArrayRanks)
				{
					typeSymbol = ((num2 == 0) ? this.GetSZArrayTypeSymbol(typeSymbol, default(ImmutableArray<ModifierInfo<TypeSymbol>>)) : this.GetMDArrayTypeSymbol(num2, typeSymbol, default(ImmutableArray<ModifierInfo<TypeSymbol>>), ImmutableArray<int>.Empty, default(ImmutableArray<int>)));
				}
			}
			return typeSymbol;
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00037580 File Offset: 0x00035780
		private ImmutableArray<KeyValuePair<TypeSymbol, ImmutableArray<ModifierInfo<TypeSymbol>>>> ResolveTypeArguments(MetadataHelpers.AssemblyQualifiedTypeName[] arguments, out ImmutableArray<bool> refersToNoPiaLocalType)
		{
			int num = arguments.Length;
			ArrayBuilder<KeyValuePair<TypeSymbol, ImmutableArray<ModifierInfo<TypeSymbol>>>> instance = ArrayBuilder<KeyValuePair<TypeSymbol, ImmutableArray<ModifierInfo<TypeSymbol>>>>.GetInstance(num);
			ArrayBuilder<bool> instance2 = ArrayBuilder<bool>.GetInstance(num);
			foreach (MetadataHelpers.AssemblyQualifiedTypeName assemblyQualifiedTypeName in arguments)
			{
				bool flag;
				instance.Add(new KeyValuePair<TypeSymbol, ImmutableArray<ModifierInfo<TypeSymbol>>>(this.GetTypeSymbol(assemblyQualifiedTypeName, out flag), ImmutableArray<ModifierInfo<TypeSymbol>>.Empty));
				instance2.Add(flag);
			}
			refersToNoPiaLocalType = instance2.ToImmutableAndFree();
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x000375EC File Offset: 0x000357EC
		private TypeSymbol LookupTopLevelTypeDefSymbol(ref MetadataTypeName emittedName, int referencedAssemblyIndex, out bool isNoPiaLocalType)
		{
			TypeSymbol typeSymbol;
			if (referencedAssemblyIndex >= 0)
			{
				isNoPiaLocalType = false;
				typeSymbol = this.LookupTopLevelTypeDefSymbol(referencedAssemblyIndex, ref emittedName);
			}
			else
			{
				typeSymbol = this.LookupTopLevelTypeDefSymbol(ref emittedName, out isNoPiaLocalType);
			}
			return typeSymbol;
		}

		// Token: 0x04000569 RID: 1385
		private readonly SymbolFactory<ModuleSymbol, TypeSymbol> _factory;

		// Token: 0x0400056A RID: 1386
		protected readonly ModuleSymbol moduleSymbol;
	}
}
