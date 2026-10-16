using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols.Retargeting;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005EB RID: 1515
	internal sealed class PEModuleSymbol : NonMissingModuleSymbol
	{
		// Token: 0x06007EA8 RID: 32424 RVA: 0x001C77CF File Offset: 0x001C59CF
		internal PEModuleSymbol(PEAssemblySymbol assemblySymbol, PEModule module, MetadataImportOptions importOptions, int ordinal)
			: this(assemblySymbol, module, importOptions, ordinal)
		{
		}

		// Token: 0x06007EA9 RID: 32425 RVA: 0x001C77DC File Offset: 0x001C59DC
		internal PEModuleSymbol(SourceAssemblySymbol assemblySymbol, PEModule module, MetadataImportOptions importOptions, int ordinal)
			: this(assemblySymbol, module, importOptions, ordinal)
		{
		}

		// Token: 0x06007EAA RID: 32426 RVA: 0x001C77E9 File Offset: 0x001C59E9
		internal PEModuleSymbol(RetargetingAssemblySymbol assemblySymbol, PEModule module, MetadataImportOptions importOptions, int ordinal)
			: this(assemblySymbol, module, importOptions, ordinal)
		{
		}

		// Token: 0x06007EAB RID: 32427 RVA: 0x001C77F8 File Offset: 0x001C59F8
		private PEModuleSymbol(AssemblySymbol assemblySymbol, PEModule module, MetadataImportOptions importOptions, int ordinal)
		{
			this._assemblySymbol = assemblySymbol;
			this._ordinal = ordinal;
			this._module = module;
			this.ImportOptions = importOptions;
			this._globalNamespace = new PEGlobalNamespaceSymbol(this);
			this.MetadataLocation = ImmutableArray.Create<MetadataLocation>(new MetadataLocation(this));
		}

		// Token: 0x170025A5 RID: 9637
		// (get) Token: 0x06007EAC RID: 32428 RVA: 0x001C7861 File Offset: 0x001C5A61
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x170025A6 RID: 9638
		// (get) Token: 0x06007EAD RID: 32429 RVA: 0x001C7868 File Offset: 0x001C5A68
		internal override int Ordinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x170025A7 RID: 9639
		// (get) Token: 0x06007EAE RID: 32430 RVA: 0x001C7870 File Offset: 0x001C5A70
		internal override Machine Machine
		{
			get
			{
				return this._module.Machine;
			}
		}

		// Token: 0x170025A8 RID: 9640
		// (get) Token: 0x06007EAF RID: 32431 RVA: 0x001C787D File Offset: 0x001C5A7D
		internal override bool Bit32Required
		{
			get
			{
				return this._module.Bit32Required;
			}
		}

		// Token: 0x170025A9 RID: 9641
		// (get) Token: 0x06007EB0 RID: 32432 RVA: 0x001C788A File Offset: 0x001C5A8A
		internal PEModule Module
		{
			get
			{
				return this._module;
			}
		}

		// Token: 0x170025AA RID: 9642
		// (get) Token: 0x06007EB1 RID: 32433 RVA: 0x001C7892 File Offset: 0x001C5A92
		public override NamespaceSymbol GlobalNamespace
		{
			get
			{
				return this._globalNamespace;
			}
		}

		// Token: 0x170025AB RID: 9643
		// (get) Token: 0x06007EB2 RID: 32434 RVA: 0x001C789A File Offset: 0x001C5A9A
		public override string Name
		{
			get
			{
				return this._module.Name;
			}
		}

		// Token: 0x170025AC RID: 9644
		// (get) Token: 0x06007EB3 RID: 32435 RVA: 0x001C78A7 File Offset: 0x001C5AA7
		private static EntityHandle Token
		{
			get
			{
				return EntityHandle.ModuleDefinition;
			}
		}

		// Token: 0x170025AD RID: 9645
		// (get) Token: 0x06007EB4 RID: 32436 RVA: 0x001C78B3 File Offset: 0x001C5AB3
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._assemblySymbol;
			}
		}

		// Token: 0x170025AE RID: 9646
		// (get) Token: 0x06007EB5 RID: 32437 RVA: 0x001C78BB File Offset: 0x001C5ABB
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._assemblySymbol;
			}
		}

		// Token: 0x170025AF RID: 9647
		// (get) Token: 0x06007EB6 RID: 32438 RVA: 0x001C78C3 File Offset: 0x001C5AC3
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this.MetadataLocation.Cast<MetadataLocation, Location>();
			}
		}

		// Token: 0x06007EB7 RID: 32439 RVA: 0x001C78D0 File Offset: 0x001C5AD0
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			if (this._lazyCustomAttributes.IsDefault)
			{
				this.LoadCustomAttributes(PEModuleSymbol.Token, ref this._lazyCustomAttributes);
			}
			return this._lazyCustomAttributes;
		}

		// Token: 0x06007EB8 RID: 32440 RVA: 0x001C78F8 File Offset: 0x001C5AF8
		internal ImmutableArray<CSharpAttributeData> GetAssemblyAttributes()
		{
			if (this._lazyAssemblyAttributes.IsDefault)
			{
				ArrayBuilder<CSharpAttributeData> arrayBuilder = null;
				string name = this.ContainingAssembly.CorLibrary.Name;
				EntityHandle entityHandle = this.Module.GetAssemblyRef(name);
				if (!entityHandle.IsNil)
				{
					string[,] dummyAssemblyAttributeParentQualifier = MetadataWriter.dummyAssemblyAttributeParentQualifier;
					int upperBound = dummyAssemblyAttributeParentQualifier.GetUpperBound(0);
					int upperBound2 = dummyAssemblyAttributeParentQualifier.GetUpperBound(1);
					for (int i = dummyAssemblyAttributeParentQualifier.GetLowerBound(0); i <= upperBound; i++)
					{
						for (int j = dummyAssemblyAttributeParentQualifier.GetLowerBound(1); j <= upperBound2; j++)
						{
							string text = dummyAssemblyAttributeParentQualifier[i, j];
							EntityHandle typeRef = this.Module.GetTypeRef(entityHandle, "System.Runtime.CompilerServices", "AssemblyAttributesGoHere" + text);
							if (!typeRef.IsNil)
							{
								try
								{
									foreach (CustomAttributeHandle customAttributeHandle in this.Module.GetCustomAttributesOrThrow(typeRef))
									{
										if (arrayBuilder == null)
										{
											arrayBuilder = new ArrayBuilder<CSharpAttributeData>();
										}
										arrayBuilder.Add(new PEAttributeData(this, customAttributeHandle));
									}
								}
								catch (BadImageFormatException)
								{
								}
							}
						}
					}
				}
				ImmutableInterlocked.InterlockedCompareExchange<CSharpAttributeData>(ref this._lazyAssemblyAttributes, (arrayBuilder != null) ? arrayBuilder.ToImmutableAndFree() : ImmutableArray<CSharpAttributeData>.Empty, default(ImmutableArray<CSharpAttributeData>));
			}
			return this._lazyAssemblyAttributes;
		}

		// Token: 0x06007EB9 RID: 32441 RVA: 0x001C7A6C File Offset: 0x001C5C6C
		internal void LoadCustomAttributes(EntityHandle token, ref ImmutableArray<CSharpAttributeData> customAttributes)
		{
			ImmutableArray<CSharpAttributeData> customAttributesForToken = this.GetCustomAttributesForToken(token);
			ImmutableInterlocked.InterlockedInitialize<CSharpAttributeData>(ref customAttributes, customAttributesForToken);
		}

		// Token: 0x06007EBA RID: 32442 RVA: 0x001C7A8C File Offset: 0x001C5C8C
		internal void LoadCustomAttributesFilterCompilerAttributes(EntityHandle token, ref ImmutableArray<CSharpAttributeData> customAttributes, out bool foundExtension, out bool foundReadOnly)
		{
			ImmutableArray<CSharpAttributeData> customAttributesFilterCompilerAttributes = this.GetCustomAttributesFilterCompilerAttributes(token, out foundExtension, out foundReadOnly);
			ImmutableInterlocked.InterlockedInitialize<CSharpAttributeData>(ref customAttributes, customAttributesFilterCompilerAttributes);
		}

		// Token: 0x06007EBB RID: 32443 RVA: 0x001C7AAC File Offset: 0x001C5CAC
		internal void LoadCustomAttributesFilterExtensions(EntityHandle token, ref ImmutableArray<CSharpAttributeData> customAttributes)
		{
			bool flag;
			bool flag2;
			ImmutableArray<CSharpAttributeData> customAttributesFilterCompilerAttributes = this.GetCustomAttributesFilterCompilerAttributes(token, out flag, out flag2);
			ImmutableInterlocked.InterlockedInitialize<CSharpAttributeData>(ref customAttributes, customAttributesFilterCompilerAttributes);
		}

		// Token: 0x06007EBC RID: 32444 RVA: 0x001C7AD0 File Offset: 0x001C5CD0
		internal ImmutableArray<CSharpAttributeData> GetCustomAttributesForToken(EntityHandle token, out CustomAttributeHandle filteredOutAttribute1, AttributeDescription filterOut1)
		{
			CustomAttributeHandle customAttributeHandle;
			CustomAttributeHandle customAttributeHandle2;
			CustomAttributeHandle customAttributeHandle3;
			return this.GetCustomAttributesForToken(token, out filteredOutAttribute1, filterOut1, out customAttributeHandle, default(AttributeDescription), out customAttributeHandle2, default(AttributeDescription), out customAttributeHandle3, default(AttributeDescription));
		}

		// Token: 0x06007EBD RID: 32445 RVA: 0x001C7B08 File Offset: 0x001C5D08
		internal ImmutableArray<CSharpAttributeData> GetCustomAttributesForToken(EntityHandle token, out CustomAttributeHandle filteredOutAttribute1, AttributeDescription filterOut1, out CustomAttributeHandle filteredOutAttribute2, AttributeDescription filterOut2, out CustomAttributeHandle filteredOutAttribute3, AttributeDescription filterOut3, out CustomAttributeHandle filteredOutAttribute4, AttributeDescription filterOut4)
		{
			filteredOutAttribute1 = default(CustomAttributeHandle);
			filteredOutAttribute2 = default(CustomAttributeHandle);
			filteredOutAttribute3 = default(CustomAttributeHandle);
			filteredOutAttribute4 = default(CustomAttributeHandle);
			ArrayBuilder<CSharpAttributeData> arrayBuilder = null;
			try
			{
				foreach (CustomAttributeHandle customAttributeHandle in this._module.GetCustomAttributesOrThrow(token))
				{
					if (this.<GetCustomAttributesForToken>g__matchesFilter|50_0(customAttributeHandle, filterOut1))
					{
						filteredOutAttribute1 = customAttributeHandle;
					}
					else if (this.<GetCustomAttributesForToken>g__matchesFilter|50_0(customAttributeHandle, filterOut2))
					{
						filteredOutAttribute2 = customAttributeHandle;
					}
					else if (this.<GetCustomAttributesForToken>g__matchesFilter|50_0(customAttributeHandle, filterOut3))
					{
						filteredOutAttribute3 = customAttributeHandle;
					}
					else if (this.<GetCustomAttributesForToken>g__matchesFilter|50_0(customAttributeHandle, filterOut4))
					{
						filteredOutAttribute4 = customAttributeHandle;
					}
					else
					{
						if (arrayBuilder == null)
						{
							arrayBuilder = ArrayBuilder<CSharpAttributeData>.GetInstance();
						}
						arrayBuilder.Add(new PEAttributeData(this, customAttributeHandle));
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			if (arrayBuilder != null)
			{
				return arrayBuilder.ToImmutableAndFree();
			}
			return ImmutableArray<CSharpAttributeData>.Empty;
		}

		// Token: 0x06007EBE RID: 32446 RVA: 0x001C7C08 File Offset: 0x001C5E08
		internal ImmutableArray<CSharpAttributeData> GetCustomAttributesForToken(EntityHandle token)
		{
			CustomAttributeHandle customAttributeHandle;
			return this.GetCustomAttributesForToken(token, out customAttributeHandle, default(AttributeDescription));
		}

		// Token: 0x06007EBF RID: 32447 RVA: 0x001C7C27 File Offset: 0x001C5E27
		internal ImmutableArray<CSharpAttributeData> GetCustomAttributesForToken(EntityHandle token, out CustomAttributeHandle paramArrayAttribute)
		{
			return this.GetCustomAttributesForToken(token, out paramArrayAttribute, AttributeDescription.ParamArrayAttribute);
		}

		// Token: 0x06007EC0 RID: 32448 RVA: 0x001C7C38 File Offset: 0x001C5E38
		internal bool HasAnyCustomAttributes(EntityHandle token)
		{
			try
			{
				using (CustomAttributeHandleCollection.Enumerator enumerator = this._module.GetCustomAttributesOrThrow(token).GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						CustomAttributeHandle customAttributeHandle = enumerator.Current;
						return true;
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			return false;
		}

		// Token: 0x06007EC1 RID: 32449 RVA: 0x001C7CA8 File Offset: 0x001C5EA8
		internal TypeSymbol TryDecodeAttributeWithTypeArgument(EntityHandle handle, AttributeDescription attributeDescription)
		{
			string text;
			if (this._module.HasStringValuedAttribute(handle, attributeDescription, out text))
			{
				return new MetadataDecoder(this).GetTypeSymbolForSerializedType(text);
			}
			return null;
		}

		// Token: 0x06007EC2 RID: 32450 RVA: 0x001C7CD4 File Offset: 0x001C5ED4
		internal ImmutableArray<CSharpAttributeData> GetCustomAttributesFilterCompilerAttributes(EntityHandle token, out bool foundExtension, out bool foundReadOnly)
		{
			CustomAttributeHandle customAttributeHandle;
			CustomAttributeHandle customAttributeHandle2;
			CustomAttributeHandle customAttributeHandle3;
			CustomAttributeHandle customAttributeHandle4;
			ImmutableArray<CSharpAttributeData> customAttributesForToken = this.GetCustomAttributesForToken(token, out customAttributeHandle, AttributeDescription.CaseSensitiveExtensionAttribute, out customAttributeHandle2, AttributeDescription.IsReadOnlyAttribute, out customAttributeHandle3, default(AttributeDescription), out customAttributeHandle4, default(AttributeDescription));
			foundExtension = !customAttributeHandle.IsNil;
			foundReadOnly = !customAttributeHandle2.IsNil;
			return customAttributesForToken;
		}

		// Token: 0x06007EC3 RID: 32451 RVA: 0x001C7D28 File Offset: 0x001C5F28
		internal void OnNewTypeDeclarationsLoaded(Dictionary<string, ImmutableArray<PENamedTypeSymbol>> typesDict)
		{
			bool flag = this._ordinal == 0 && this._assemblySymbol.KeepLookingForDeclaredSpecialTypes;
			foreach (ImmutableArray<PENamedTypeSymbol> immutableArray in typesDict.Values)
			{
				foreach (PENamedTypeSymbol penamedTypeSymbol in immutableArray)
				{
					this.TypeHandleToTypeMap.TryAdd(penamedTypeSymbol.Handle, penamedTypeSymbol);
					if (flag && penamedTypeSymbol.SpecialType != SpecialType.None)
					{
						this._assemblySymbol.RegisterDeclaredSpecialType(penamedTypeSymbol);
						flag = this._assemblySymbol.KeepLookingForDeclaredSpecialTypes;
					}
				}
			}
		}

		// Token: 0x170025B0 RID: 9648
		// (get) Token: 0x06007EC4 RID: 32452 RVA: 0x001C7DE4 File Offset: 0x001C5FE4
		internal override ICollection<string> TypeNames
		{
			get
			{
				if (this._lazyTypeNames == null)
				{
					Interlocked.CompareExchange<ICollection<string>>(ref this._lazyTypeNames, this._module.TypeNames.AsCaseSensitiveCollection(), null);
				}
				return this._lazyTypeNames;
			}
		}

		// Token: 0x170025B1 RID: 9649
		// (get) Token: 0x06007EC5 RID: 32453 RVA: 0x001C7E11 File Offset: 0x001C6011
		internal override ICollection<string> NamespaceNames
		{
			get
			{
				if (this._lazyNamespaceNames == null)
				{
					Interlocked.CompareExchange<ICollection<string>>(ref this._lazyNamespaceNames, this._module.NamespaceNames.AsCaseSensitiveCollection(), null);
				}
				return this._lazyNamespaceNames;
			}
		}

		// Token: 0x06007EC6 RID: 32454 RVA: 0x001C7E3E File Offset: 0x001C603E
		internal override ImmutableArray<byte> GetHash(AssemblyHashAlgorithm algorithmId)
		{
			return this._module.GetHash(algorithmId);
		}

		// Token: 0x170025B2 RID: 9650
		// (get) Token: 0x06007EC7 RID: 32455 RVA: 0x001C7E4C File Offset: 0x001C604C
		internal DocumentationProvider DocumentationProvider
		{
			get
			{
				PEAssemblySymbol peassemblySymbol = this._assemblySymbol as PEAssemblySymbol;
				if (peassemblySymbol != null)
				{
					return peassemblySymbol.DocumentationProvider;
				}
				return DocumentationProvider.Default;
			}
		}

		// Token: 0x170025B3 RID: 9651
		// (get) Token: 0x06007EC8 RID: 32456 RVA: 0x001C7E74 File Offset: 0x001C6074
		internal NamedTypeSymbol EventRegistrationToken
		{
			get
			{
				if (this._lazyEventRegistrationTokenSymbol == null)
				{
					Interlocked.CompareExchange<NamedTypeSymbol>(ref this._lazyEventRegistrationTokenSymbol, this.GetTypeSymbolForWellKnownType(WellKnownType.System_Runtime_InteropServices_WindowsRuntime_EventRegistrationToken), null);
				}
				return this._lazyEventRegistrationTokenSymbol;
			}
		}

		// Token: 0x170025B4 RID: 9652
		// (get) Token: 0x06007EC9 RID: 32457 RVA: 0x001C7E9C File Offset: 0x001C609C
		internal NamedTypeSymbol EventRegistrationTokenTable_T
		{
			get
			{
				if (this._lazyEventRegistrationTokenTableSymbol == null)
				{
					Interlocked.CompareExchange<NamedTypeSymbol>(ref this._lazyEventRegistrationTokenTableSymbol, this.GetTypeSymbolForWellKnownType(WellKnownType.System_Runtime_InteropServices_WindowsRuntime_EventRegistrationTokenTable_T), null);
				}
				return this._lazyEventRegistrationTokenTableSymbol;
			}
		}

		// Token: 0x170025B5 RID: 9653
		// (get) Token: 0x06007ECA RID: 32458 RVA: 0x001C7EC4 File Offset: 0x001C60C4
		internal NamedTypeSymbol SystemTypeSymbol
		{
			get
			{
				if (this._lazySystemTypeSymbol == null)
				{
					Interlocked.CompareExchange<NamedTypeSymbol>(ref this._lazySystemTypeSymbol, this.GetTypeSymbolForWellKnownType(WellKnownType.System_Type), null);
				}
				return this._lazySystemTypeSymbol;
			}
		}

		// Token: 0x06007ECB RID: 32459 RVA: 0x001C7EEC File Offset: 0x001C60EC
		private NamedTypeSymbol GetTypeSymbolForWellKnownType(WellKnownType type)
		{
			MetadataTypeName metadataTypeName = MetadataTypeName.FromFullName(type.GetMetadataName(), true, -1);
			NamedTypeSymbol namedTypeSymbol = this.LookupTopLevelMetadataType(ref metadataTypeName);
			if (PEModuleSymbol.IsAcceptableSystemTypeSymbol(namedTypeSymbol))
			{
				return namedTypeSymbol;
			}
			NamedTypeSymbol namedTypeSymbol2 = null;
			foreach (AssemblySymbol assemblySymbol in this.GetReferencedAssemblySymbols())
			{
				NamedTypeSymbol namedTypeSymbol3 = assemblySymbol.LookupTopLevelMetadataType(ref metadataTypeName, true);
				if (PEModuleSymbol.IsAcceptableSystemTypeSymbol(namedTypeSymbol3))
				{
					if (namedTypeSymbol2 == null)
					{
						namedTypeSymbol2 = namedTypeSymbol3;
					}
					else
					{
						if (!TypeSymbol.Equals(namedTypeSymbol2, namedTypeSymbol3, TypeCompareKind.ConsiderEverything))
						{
							namedTypeSymbol2 = null;
							break;
						}
						break;
					}
				}
			}
			if (namedTypeSymbol2 != null)
			{
				return namedTypeSymbol2;
			}
			return namedTypeSymbol;
		}

		// Token: 0x06007ECC RID: 32460 RVA: 0x001C7F6E File Offset: 0x001C616E
		private static bool IsAcceptableSystemTypeSymbol(NamedTypeSymbol candidate)
		{
			return candidate.Kind != SymbolKind.ErrorType || !(candidate is MissingMetadataTypeSymbol);
		}

		// Token: 0x170025B6 RID: 9654
		// (get) Token: 0x06007ECD RID: 32461 RVA: 0x001C7F87 File Offset: 0x001C6187
		internal override bool HasAssemblyCompilationRelaxationsAttribute
		{
			get
			{
				return this.GetAssemblyAttributes().IndexOfAttribute(this, AttributeDescription.CompilationRelaxationsAttribute) >= 0;
			}
		}

		// Token: 0x170025B7 RID: 9655
		// (get) Token: 0x06007ECE RID: 32462 RVA: 0x001C7FA0 File Offset: 0x001C61A0
		internal override bool HasAssemblyRuntimeCompatibilityAttribute
		{
			get
			{
				return this.GetAssemblyAttributes().IndexOfAttribute(this, AttributeDescription.RuntimeCompatibilityAttribute) >= 0;
			}
		}

		// Token: 0x170025B8 RID: 9656
		// (get) Token: 0x06007ECF RID: 32463 RVA: 0x001C7FB9 File Offset: 0x001C61B9
		internal override CharSet? DefaultMarshallingCharSet
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x170025B9 RID: 9657
		// (get) Token: 0x06007ED0 RID: 32464 RVA: 0x001C7FC0 File Offset: 0x001C61C0
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007ED1 RID: 32465 RVA: 0x001C7FC4 File Offset: 0x001C61C4
		internal NamedTypeSymbol LookupTopLevelMetadataType(ref MetadataTypeName emittedName, out bool isNoPiaLocalType)
		{
			PENamespaceSymbol penamespaceSymbol = (PENamespaceSymbol)this.GlobalNamespace.LookupNestedNamespace(emittedName.NamespaceSegments);
			NamedTypeSymbol namedTypeSymbol;
			if (penamespaceSymbol == null)
			{
				isNoPiaLocalType = false;
				namedTypeSymbol = new MissingMetadataTypeSymbol.TopLevel(this, ref emittedName, null);
			}
			else
			{
				namedTypeSymbol = penamespaceSymbol.LookupMetadataType(ref emittedName, out isNoPiaLocalType);
			}
			return namedTypeSymbol;
		}

		// Token: 0x06007ED2 RID: 32466 RVA: 0x001C8004 File Offset: 0x001C6204
		[return: TupleElementNames(new string[] { "FirstSymbol", "SecondSymbol" })]
		internal ValueTuple<AssemblySymbol, AssemblySymbol> GetAssembliesForForwardedType(ref MetadataTypeName fullName)
		{
			string text;
			ValueTuple<int, int> assemblyRefsForForwardedType = this.Module.GetAssemblyRefsForForwardedType(fullName.FullName, false, out text);
			int item = assemblyRefsForForwardedType.Item1;
			int item2 = assemblyRefsForForwardedType.Item2;
			if (item < 0)
			{
				return new ValueTuple<AssemblySymbol, AssemblySymbol>(null, null);
			}
			AssemblySymbol referencedAssemblySymbol = base.GetReferencedAssemblySymbol(item);
			if (item2 < 0)
			{
				return new ValueTuple<AssemblySymbol, AssemblySymbol>(referencedAssemblySymbol, null);
			}
			AssemblySymbol referencedAssemblySymbol2 = base.GetReferencedAssemblySymbol(item2);
			return new ValueTuple<AssemblySymbol, AssemblySymbol>(referencedAssemblySymbol, referencedAssemblySymbol2);
		}

		// Token: 0x06007ED3 RID: 32467 RVA: 0x001C8063 File Offset: 0x001C6263
		internal IEnumerable<NamedTypeSymbol> GetForwardedTypes()
		{
			foreach (KeyValuePair<string, ValueTuple<int, int>> keyValuePair in this.Module.GetForwardedTypes())
			{
				MetadataTypeName metadataTypeName = MetadataTypeName.FromFullName(keyValuePair.Key, false, -1);
				AssemblySymbol referencedAssemblySymbol = base.GetReferencedAssemblySymbol(keyValuePair.Value.Item1);
				if (keyValuePair.Value.Item2 >= 0)
				{
					AssemblySymbol referencedAssemblySymbol2 = base.GetReferencedAssemblySymbol(keyValuePair.Value.Item2);
					yield return this.ContainingAssembly.CreateMultipleForwardingErrorTypeSymbol(ref metadataTypeName, this, referencedAssemblySymbol, referencedAssemblySymbol2);
				}
				else
				{
					yield return referencedAssemblySymbol.LookupTopLevelMetadataType(ref metadataTypeName, true);
				}
			}
			IEnumerator<KeyValuePair<string, ValueTuple<int, int>>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06007ED4 RID: 32468 RVA: 0x001C8073 File Offset: 0x001C6273
		public override ModuleMetadata GetMetadata()
		{
			return this._module.GetNonDisposableMetadata();
		}

		// Token: 0x06007ED5 RID: 32469 RVA: 0x001C8080 File Offset: 0x001C6280
		internal bool ShouldDecodeNullableAttributes(Symbol symbol)
		{
			if (this._lazyNullableMemberMetadata == PEModuleSymbol.NullableMemberMetadata.Unknown)
			{
				bool flag;
				this._lazyNullableMemberMetadata = (this._module.HasNullablePublicOnlyAttribute(PEModuleSymbol.Token, out flag) ? (flag ? PEModuleSymbol.NullableMemberMetadata.Internal : PEModuleSymbol.NullableMemberMetadata.Public) : PEModuleSymbol.NullableMemberMetadata.All);
			}
			PEModuleSymbol.NullableMemberMetadata lazyNullableMemberMetadata = this._lazyNullableMemberMetadata;
			if (lazyNullableMemberMetadata == PEModuleSymbol.NullableMemberMetadata.All)
			{
				return true;
			}
			bool flag2;
			if (!AccessCheck.IsEffectivelyPublicOrInternal(symbol, out flag2))
			{
				return false;
			}
			if (lazyNullableMemberMetadata == PEModuleSymbol.NullableMemberMetadata.Public)
			{
				return !flag2;
			}
			if (lazyNullableMemberMetadata != PEModuleSymbol.NullableMemberMetadata.Internal)
			{
				throw ExceptionUtilities.UnexpectedValue(lazyNullableMemberMetadata);
			}
			return true;
		}

		// Token: 0x06007ED6 RID: 32470 RVA: 0x001C80EE File Offset: 0x001C62EE
		[CompilerGenerated]
		private bool <GetCustomAttributesForToken>g__matchesFilter|50_0(CustomAttributeHandle handle, AttributeDescription filter)
		{
			return filter.Signatures != null && this.Module.GetTargetAttributeSignatureIndex(handle, filter) != -1;
		}

		// Token: 0x04001DD6 RID: 7638
		private readonly AssemblySymbol _assemblySymbol;

		// Token: 0x04001DD7 RID: 7639
		private readonly int _ordinal;

		// Token: 0x04001DD8 RID: 7640
		private readonly PEModule _module;

		// Token: 0x04001DD9 RID: 7641
		private readonly PENamespaceSymbol _globalNamespace;

		// Token: 0x04001DDA RID: 7642
		private NamedTypeSymbol _lazySystemTypeSymbol;

		// Token: 0x04001DDB RID: 7643
		private NamedTypeSymbol _lazyEventRegistrationTokenSymbol;

		// Token: 0x04001DDC RID: 7644
		private NamedTypeSymbol _lazyEventRegistrationTokenTableSymbol;

		// Token: 0x04001DDD RID: 7645
		private const int DefaultTypeMapCapacity = 31;

		// Token: 0x04001DDE RID: 7646
		internal readonly ConcurrentDictionary<TypeDefinitionHandle, TypeSymbol> TypeHandleToTypeMap = new ConcurrentDictionary<TypeDefinitionHandle, TypeSymbol>(2, 31);

		// Token: 0x04001DDF RID: 7647
		internal readonly ConcurrentDictionary<TypeReferenceHandle, TypeSymbol> TypeRefHandleToTypeMap = new ConcurrentDictionary<TypeReferenceHandle, TypeSymbol>(2, 31);

		// Token: 0x04001DE0 RID: 7648
		internal readonly ImmutableArray<MetadataLocation> MetadataLocation;

		// Token: 0x04001DE1 RID: 7649
		internal readonly MetadataImportOptions ImportOptions;

		// Token: 0x04001DE2 RID: 7650
		private ImmutableArray<CSharpAttributeData> _lazyCustomAttributes;

		// Token: 0x04001DE3 RID: 7651
		private ImmutableArray<CSharpAttributeData> _lazyAssemblyAttributes;

		// Token: 0x04001DE4 RID: 7652
		private ICollection<string> _lazyTypeNames;

		// Token: 0x04001DE5 RID: 7653
		private ICollection<string> _lazyNamespaceNames;

		// Token: 0x04001DE6 RID: 7654
		private PEModuleSymbol.NullableMemberMetadata _lazyNullableMemberMetadata;

		// Token: 0x02000981 RID: 2433
		private enum NullableMemberMetadata
		{
			// Token: 0x040028FF RID: 10495
			Unknown,
			// Token: 0x04002900 RID: 10496
			Public,
			// Token: 0x04002901 RID: 10497
			Internal,
			// Token: 0x04002902 RID: 10498
			All
		}
	}
}
