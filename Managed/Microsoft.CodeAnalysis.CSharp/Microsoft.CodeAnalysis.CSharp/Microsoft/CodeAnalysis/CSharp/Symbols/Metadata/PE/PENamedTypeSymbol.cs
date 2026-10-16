using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.DocumentationComments;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005EC RID: 1516
	internal abstract class PENamedTypeSymbol : NamedTypeSymbol
	{
		// Token: 0x06007ED7 RID: 32471 RVA: 0x001C8110 File Offset: 0x001C6310
		private PENamedTypeSymbol.UncommonProperties GetUncommonProperties()
		{
			PENamedTypeSymbol.UncommonProperties uncommonProperties = this._lazyUncommonProperties;
			if (uncommonProperties != null)
			{
				return uncommonProperties;
			}
			if (this.IsUncommon())
			{
				uncommonProperties = new PENamedTypeSymbol.UncommonProperties();
				return Interlocked.CompareExchange<PENamedTypeSymbol.UncommonProperties>(ref this._lazyUncommonProperties, uncommonProperties, null) ?? uncommonProperties;
			}
			return this._lazyUncommonProperties = PENamedTypeSymbol.s_noUncommonProperties;
		}

		// Token: 0x06007ED8 RID: 32472 RVA: 0x001C8158 File Offset: 0x001C6358
		private bool IsUncommon()
		{
			return this.ContainingPEModule.HasAnyCustomAttributes(this._handle) || this.TypeKind == TypeKind.Enum;
		}

		// Token: 0x06007ED9 RID: 32473 RVA: 0x001C8180 File Offset: 0x001C6380
		internal static PENamedTypeSymbol Create(PEModuleSymbol moduleSymbol, PENamespaceSymbol containingNamespace, TypeDefinitionHandle handle, string emittedNamespaceName)
		{
			BadImageFormatException ex = null;
			GenericParameterHandleCollection genericParameterHandleCollection;
			ushort num;
			PENamedTypeSymbol.GetGenericInfo(moduleSymbol, handle, out genericParameterHandleCollection, out num, out ex);
			PENamedTypeSymbol penamedTypeSymbol;
			if (num == 0)
			{
				bool flag;
				penamedTypeSymbol = new PENamedTypeSymbol.PENamedTypeSymbolNonGeneric(moduleSymbol, containingNamespace, handle, emittedNamespaceName, out flag);
			}
			else
			{
				bool flag;
				penamedTypeSymbol = new PENamedTypeSymbol.PENamedTypeSymbolGeneric(moduleSymbol, containingNamespace, handle, emittedNamespaceName, genericParameterHandleCollection, num, out flag);
			}
			if (ex != null)
			{
				penamedTypeSymbol._lazyCachedUseSiteInfo.Initialize(new CSDiagnosticInfo(ErrorCode.ERR_BogusType, new object[] { penamedTypeSymbol }));
			}
			return penamedTypeSymbol;
		}

		// Token: 0x06007EDA RID: 32474 RVA: 0x001C81E4 File Offset: 0x001C63E4
		private static void GetGenericInfo(PEModuleSymbol moduleSymbol, TypeDefinitionHandle handle, out GenericParameterHandleCollection genericParameterHandles, out ushort arity, out BadImageFormatException mrEx)
		{
			try
			{
				genericParameterHandles = moduleSymbol.Module.GetTypeDefGenericParamsOrThrow(handle);
				arity = (ushort)genericParameterHandles.Count;
				mrEx = null;
			}
			catch (BadImageFormatException ex)
			{
				arity = 0;
				genericParameterHandles = default(GenericParameterHandleCollection);
				mrEx = ex;
			}
		}

		// Token: 0x06007EDB RID: 32475 RVA: 0x001C8234 File Offset: 0x001C6434
		internal static PENamedTypeSymbol Create(PEModuleSymbol moduleSymbol, PENamedTypeSymbol containingType, TypeDefinitionHandle handle)
		{
			BadImageFormatException ex = null;
			GenericParameterHandleCollection genericParameterHandleCollection;
			ushort num;
			PENamedTypeSymbol.GetGenericInfo(moduleSymbol, handle, out genericParameterHandleCollection, out num, out ex);
			ushort num2 = 0;
			int metadataArity = containingType.MetadataArity;
			if ((int)num > metadataArity)
			{
				num2 = (ushort)((int)num - metadataArity);
			}
			PENamedTypeSymbol penamedTypeSymbol;
			if (num == 0)
			{
				bool flag;
				penamedTypeSymbol = new PENamedTypeSymbol.PENamedTypeSymbolNonGeneric(moduleSymbol, containingType, handle, null, out flag);
			}
			else
			{
				bool flag;
				penamedTypeSymbol = new PENamedTypeSymbol.PENamedTypeSymbolGeneric(moduleSymbol, containingType, handle, null, genericParameterHandleCollection, num2, out flag);
			}
			if (ex != null || (int)num < metadataArity)
			{
				penamedTypeSymbol._lazyCachedUseSiteInfo.Initialize(new CSDiagnosticInfo(ErrorCode.ERR_BogusType, new object[] { penamedTypeSymbol }));
			}
			return penamedTypeSymbol;
		}

		// Token: 0x06007EDC RID: 32476 RVA: 0x001C82B4 File Offset: 0x001C64B4
		private PENamedTypeSymbol(PEModuleSymbol moduleSymbol, NamespaceOrTypeSymbol container, TypeDefinitionHandle handle, string emittedNamespaceName, ushort arity, out bool mangleName)
			: base(null)
		{
			bool flag = false;
			string text;
			try
			{
				text = moduleSymbol.Module.GetTypeDefNameOrThrow(handle);
			}
			catch (BadImageFormatException)
			{
				text = string.Empty;
				flag = true;
			}
			this._handle = handle;
			this._container = container;
			try
			{
				this._flags = moduleSymbol.Module.GetTypeDefFlagsOrThrow(handle);
			}
			catch (BadImageFormatException)
			{
				flag = true;
			}
			if (arity == 0)
			{
				this._name = text;
				mangleName = false;
			}
			else
			{
				this._name = MetadataHelpers.UnmangleMetadataNameForArity(text, (int)arity);
				mangleName = this._name != text;
			}
			if (emittedNamespaceName != null && moduleSymbol.ContainingAssembly.KeepLookingForDeclaredSpecialTypes && this.DeclaredAccessibility == Accessibility.Public)
			{
				this._corTypeId = SpecialTypes.GetTypeFromMetadataName(MetadataHelpers.BuildQualifiedName(emittedNamespaceName, text));
			}
			else
			{
				this._corTypeId = SpecialType.None;
			}
			if (flag)
			{
				this._lazyCachedUseSiteInfo.Initialize(new CSDiagnosticInfo(ErrorCode.ERR_BogusType, new object[] { this }));
			}
		}

		// Token: 0x170025BA RID: 9658
		// (get) Token: 0x06007EDD RID: 32477 RVA: 0x001C83D0 File Offset: 0x001C65D0
		public override SpecialType SpecialType
		{
			get
			{
				return this._corTypeId;
			}
		}

		// Token: 0x170025BB RID: 9659
		// (get) Token: 0x06007EDE RID: 32478 RVA: 0x001C83D8 File Offset: 0x001C65D8
		internal PEModuleSymbol ContainingPEModule
		{
			get
			{
				Symbol symbol = this._container;
				while (symbol.Kind != SymbolKind.Namespace)
				{
					symbol = symbol.ContainingSymbol;
				}
				return ((PENamespaceSymbol)symbol).ContainingPEModule;
			}
		}

		// Token: 0x170025BC RID: 9660
		// (get) Token: 0x06007EDF RID: 32479 RVA: 0x001C840A File Offset: 0x001C660A
		internal override ModuleSymbol ContainingModule
		{
			get
			{
				return this.ContainingPEModule;
			}
		}

		// Token: 0x170025BD RID: 9661
		// (get) Token: 0x06007EE0 RID: 32480
		public abstract override int Arity { get; }

		// Token: 0x170025BE RID: 9662
		// (get) Token: 0x06007EE1 RID: 32481
		internal abstract override bool MangleName { get; }

		// Token: 0x170025BF RID: 9663
		// (get) Token: 0x06007EE2 RID: 32482
		internal abstract int MetadataArity { get; }

		// Token: 0x170025C0 RID: 9664
		// (get) Token: 0x06007EE3 RID: 32483 RVA: 0x001C8412 File Offset: 0x001C6612
		internal TypeDefinitionHandle Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x170025C1 RID: 9665
		// (get) Token: 0x06007EE4 RID: 32484 RVA: 0x001C841C File Offset: 0x001C661C
		internal override bool HasCodeAnalysisEmbeddedAttribute
		{
			get
			{
				PENamedTypeSymbol.UncommonProperties uncommonProperties = this.GetUncommonProperties();
				if (uncommonProperties == PENamedTypeSymbol.s_noUncommonProperties)
				{
					return false;
				}
				if (!uncommonProperties.lazyHasEmbeddedAttribute.HasValue())
				{
					uncommonProperties.lazyHasEmbeddedAttribute = this.ContainingPEModule.Module.HasCodeAnalysisEmbeddedAttribute(this._handle).ToThreeState();
				}
				return uncommonProperties.lazyHasEmbeddedAttribute.Value();
			}
		}

		// Token: 0x170025C2 RID: 9666
		// (get) Token: 0x06007EE5 RID: 32485 RVA: 0x001C8478 File Offset: 0x001C6678
		internal override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			get
			{
				if (this._lazyBaseType == ErrorTypeSymbol.UnknownResultType)
				{
					Interlocked.CompareExchange<NamedTypeSymbol>(ref this._lazyBaseType, this.MakeAcyclicBaseType(), ErrorTypeSymbol.UnknownResultType);
				}
				return this._lazyBaseType;
			}
		}

		// Token: 0x06007EE6 RID: 32486 RVA: 0x001C84A4 File Offset: 0x001C66A4
		internal override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics(ConsList<TypeSymbol> basesBeingResolved = null)
		{
			if (this._lazyInterfaces.IsDefault)
			{
				ImmutableInterlocked.InterlockedCompareExchange<NamedTypeSymbol>(ref this._lazyInterfaces, this.MakeAcyclicInterfaces(), default(ImmutableArray<NamedTypeSymbol>));
			}
			return this._lazyInterfaces;
		}

		// Token: 0x06007EE7 RID: 32487 RVA: 0x001C84DF File Offset: 0x001C66DF
		internal override ImmutableArray<NamedTypeSymbol> GetInterfacesToEmit()
		{
			return this.InterfacesNoUseSiteDiagnostics(null);
		}

		// Token: 0x06007EE8 RID: 32488 RVA: 0x001C84E8 File Offset: 0x001C66E8
		internal override NamedTypeSymbol GetDeclaredBaseType(ConsList<TypeSymbol> basesBeingResolved)
		{
			return this.GetDeclaredBaseType(false);
		}

		// Token: 0x06007EE9 RID: 32489 RVA: 0x001C84F4 File Offset: 0x001C66F4
		private NamedTypeSymbol GetDeclaredBaseType(bool skipTransformsIfNecessary)
		{
			if (this._lazyDeclaredBaseType == ErrorTypeSymbol.UnknownResultType)
			{
				NamedTypeSymbol namedTypeSymbol = this.MakeDeclaredBaseType();
				if (namedTypeSymbol != null)
				{
					if (skipTransformsIfNecessary)
					{
						return namedTypeSymbol;
					}
					PEModuleSymbol containingPEModule = this.ContainingPEModule;
					namedTypeSymbol = (NamedTypeSymbol)NullableTypeDecoder.TransformType(TypeWithAnnotations.Create(TupleTypeDecoder.DecodeTupleTypesIfApplicable(NativeIntegerTypeDecoder.TransformType(DynamicTypeDecoder.TransformType(namedTypeSymbol, 0, this._handle, containingPEModule, RefKind.None), this._handle, containingPEModule), this._handle, containingPEModule), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), this._handle, containingPEModule, this, this).Type;
				}
				Interlocked.CompareExchange<NamedTypeSymbol>(ref this._lazyDeclaredBaseType, namedTypeSymbol, ErrorTypeSymbol.UnknownResultType);
			}
			return this._lazyDeclaredBaseType;
		}

		// Token: 0x06007EEA RID: 32490 RVA: 0x001C85A8 File Offset: 0x001C67A8
		internal override ImmutableArray<NamedTypeSymbol> GetDeclaredInterfaces(ConsList<TypeSymbol> basesBeingResolved)
		{
			if (this._lazyDeclaredInterfaces.IsDefault)
			{
				ImmutableInterlocked.InterlockedCompareExchange<NamedTypeSymbol>(ref this._lazyDeclaredInterfaces, this.MakeDeclaredInterfaces(), default(ImmutableArray<NamedTypeSymbol>));
			}
			return this._lazyDeclaredInterfaces;
		}

		// Token: 0x06007EEB RID: 32491 RVA: 0x001C85E4 File Offset: 0x001C67E4
		private NamedTypeSymbol MakeDeclaredBaseType()
		{
			if (!this._flags.IsInterface())
			{
				try
				{
					PEModuleSymbol containingPEModule = this.ContainingPEModule;
					EntityHandle baseTypeOfTypeOrThrow = containingPEModule.Module.GetBaseTypeOfTypeOrThrow(this._handle);
					if (!baseTypeOfTypeOrThrow.IsNil)
					{
						return (NamedTypeSymbol)new MetadataDecoder(containingPEModule, this).GetTypeOfToken(baseTypeOfTypeOrThrow);
					}
				}
				catch (BadImageFormatException ex)
				{
					return new UnsupportedMetadataTypeSymbol(ex);
				}
			}
			return null;
		}

		// Token: 0x06007EEC RID: 32492 RVA: 0x001C8654 File Offset: 0x001C6854
		private ImmutableArray<NamedTypeSymbol> MakeDeclaredInterfaces()
		{
			ImmutableArray<NamedTypeSymbol> immutableArray;
			try
			{
				PEModuleSymbol containingPEModule = this.ContainingPEModule;
				InterfaceImplementationHandleCollection interfaceImplementationsOrThrow = containingPEModule.Module.GetInterfaceImplementationsOrThrow(this._handle);
				if (interfaceImplementationsOrThrow.Count > 0)
				{
					ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance(interfaceImplementationsOrThrow.Count);
					MetadataDecoder metadataDecoder = new MetadataDecoder(containingPEModule, this);
					foreach (InterfaceImplementationHandle interfaceImplementationHandle in interfaceImplementationsOrThrow)
					{
						EntityHandle @interface = containingPEModule.Module.MetadataReader.GetInterfaceImplementation(interfaceImplementationHandle).Interface;
						NamedTypeSymbol namedTypeSymbol = (NullableTypeDecoder.TransformType(TypeWithAnnotations.Create(TupleTypeDecoder.DecodeTupleTypesIfApplicable(NativeIntegerTypeDecoder.TransformType(metadataDecoder.GetTypeOfToken(@interface), interfaceImplementationHandle, containingPEModule), interfaceImplementationHandle, containingPEModule), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), interfaceImplementationHandle, containingPEModule, this, this).Type as NamedTypeSymbol) ?? new UnsupportedMetadataTypeSymbol(null);
						instance.Add(namedTypeSymbol);
					}
					immutableArray = instance.ToImmutableAndFree();
				}
				else
				{
					immutableArray = ImmutableArray<NamedTypeSymbol>.Empty;
				}
			}
			catch (BadImageFormatException ex)
			{
				immutableArray = ImmutableArray.Create<NamedTypeSymbol>(new UnsupportedMetadataTypeSymbol(ex));
			}
			return immutableArray;
		}

		// Token: 0x170025C3 RID: 9667
		// (get) Token: 0x06007EED RID: 32493 RVA: 0x001C87A8 File Offset: 0x001C69A8
		public override NamedTypeSymbol ConstructedFrom
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170025C4 RID: 9668
		// (get) Token: 0x06007EEE RID: 32494 RVA: 0x001C87AB File Offset: 0x001C69AB
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x170025C5 RID: 9669
		// (get) Token: 0x06007EEF RID: 32495 RVA: 0x001C87B3 File Offset: 0x001C69B3
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._container as NamedTypeSymbol;
			}
		}

		// Token: 0x170025C6 RID: 9670
		// (get) Token: 0x06007EF0 RID: 32496 RVA: 0x001C87C0 File Offset: 0x001C69C0
		internal override bool IsRecord
		{
			get
			{
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				return SynthesizedRecordClone.FindValidCloneMethod(this, ref discarded) != null;
			}
		}

		// Token: 0x170025C7 RID: 9671
		// (get) Token: 0x06007EF1 RID: 32497 RVA: 0x001C87E1 File Offset: 0x001C69E1
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170025C8 RID: 9672
		// (get) Token: 0x06007EF2 RID: 32498 RVA: 0x001C87E4 File Offset: 0x001C69E4
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				Accessibility accessibility;
				switch (this._flags & 7)
				{
				case 0:
					accessibility = Accessibility.Internal;
					break;
				case 1:
				case 2:
					accessibility = Accessibility.Public;
					break;
				case 3:
					accessibility = Accessibility.Private;
					break;
				case 4:
					accessibility = Accessibility.Protected;
					break;
				case 5:
					accessibility = Accessibility.Internal;
					break;
				case 6:
					accessibility = Accessibility.ProtectedAndInternal;
					break;
				case 7:
					accessibility = Accessibility.ProtectedOrInternal;
					break;
				default:
					throw ExceptionUtilities.UnexpectedValue(this._flags & 7);
				}
				return accessibility;
			}
		}

		// Token: 0x170025C9 RID: 9673
		// (get) Token: 0x06007EF3 RID: 32499 RVA: 0x001C8854 File Offset: 0x001C6A54
		public override NamedTypeSymbol EnumUnderlyingType
		{
			get
			{
				PENamedTypeSymbol.UncommonProperties uncommonProperties = this.GetUncommonProperties();
				if (uncommonProperties == PENamedTypeSymbol.s_noUncommonProperties)
				{
					return null;
				}
				this.EnsureEnumUnderlyingTypeIsLoaded(uncommonProperties);
				return uncommonProperties.lazyEnumUnderlyingType;
			}
		}

		// Token: 0x06007EF4 RID: 32500 RVA: 0x001C8880 File Offset: 0x001C6A80
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			PENamedTypeSymbol.UncommonProperties uncommonProperties = this.GetUncommonProperties();
			if (uncommonProperties == PENamedTypeSymbol.s_noUncommonProperties)
			{
				return ImmutableArray<CSharpAttributeData>.Empty;
			}
			if (uncommonProperties.lazyCustomAttributes.IsDefault)
			{
				CustomAttributeHandle customAttributeHandle;
				CustomAttributeHandle customAttributeHandle2;
				CustomAttributeHandle customAttributeHandle3;
				CustomAttributeHandle customAttributeHandle4;
				ImmutableArray<CSharpAttributeData> customAttributesForToken = this.ContainingPEModule.GetCustomAttributesForToken(this.Handle, out customAttributeHandle, this.MightContainExtensionMethods ? AttributeDescription.CaseSensitiveExtensionAttribute : default(AttributeDescription), out customAttributeHandle2, (this.IsRefLikeType && this.ObsoleteAttributeData == null) ? AttributeDescription.ObsoleteAttribute : default(AttributeDescription), out customAttributeHandle3, this.IsReadOnly ? AttributeDescription.IsReadOnlyAttribute : default(AttributeDescription), out customAttributeHandle4, this.IsRefLikeType ? AttributeDescription.IsByRefLikeAttribute : default(AttributeDescription));
				ImmutableInterlocked.InterlockedInitialize<CSharpAttributeData>(ref uncommonProperties.lazyCustomAttributes, customAttributesForToken);
			}
			return uncommonProperties.lazyCustomAttributes;
		}

		// Token: 0x06007EF5 RID: 32501 RVA: 0x001C8950 File Offset: 0x001C6B50
		internal override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			return this.GetAttributes();
		}

		// Token: 0x06007EF6 RID: 32502 RVA: 0x001C8960 File Offset: 0x001C6B60
		internal override byte? GetNullableContextValue()
		{
			byte? b;
			if (!this._lazyNullableContextValue.TryGetByte(out b))
			{
				byte b2;
				b = (this.ContainingPEModule.Module.HasNullableContextAttribute(this._handle, out b2) ? new byte?(b2) : this._container.GetNullableContextValue());
				this._lazyNullableContextValue = b.ToNullableContextFlags();
			}
			return b;
		}

		// Token: 0x06007EF7 RID: 32503 RVA: 0x001C89BC File Offset: 0x001C6BBC
		internal override byte? GetLocalNullableContextValue()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x170025CA RID: 9674
		// (get) Token: 0x06007EF8 RID: 32504 RVA: 0x001C89C3 File Offset: 0x001C6BC3
		public override IEnumerable<string> MemberNames
		{
			get
			{
				this.EnsureNonTypeMemberNamesAreLoaded();
				return this._lazyMemberNames;
			}
		}

		// Token: 0x06007EF9 RID: 32505 RVA: 0x001C89D4 File Offset: 0x001C6BD4
		private void EnsureNonTypeMemberNamesAreLoaded()
		{
			if (this._lazyMemberNames == null)
			{
				PEModule module = this.ContainingPEModule.Module;
				HashSet<string> hashSet = new HashSet<string>();
				try
				{
					foreach (MethodDefinitionHandle methodDefinitionHandle in module.GetMethodsOfTypeOrThrow(this._handle))
					{
						try
						{
							hashSet.Add(module.GetMethodDefNameOrThrow(methodDefinitionHandle));
						}
						catch (BadImageFormatException)
						{
						}
					}
				}
				catch (BadImageFormatException)
				{
				}
				try
				{
					foreach (PropertyDefinitionHandle propertyDefinitionHandle in module.GetPropertiesOfTypeOrThrow(this._handle))
					{
						try
						{
							hashSet.Add(module.GetPropertyDefNameOrThrow(propertyDefinitionHandle));
						}
						catch (BadImageFormatException)
						{
						}
					}
				}
				catch (BadImageFormatException)
				{
				}
				try
				{
					foreach (EventDefinitionHandle eventDefinitionHandle in module.GetEventsOfTypeOrThrow(this._handle))
					{
						try
						{
							hashSet.Add(module.GetEventDefNameOrThrow(eventDefinitionHandle));
						}
						catch (BadImageFormatException)
						{
						}
					}
				}
				catch (BadImageFormatException)
				{
				}
				try
				{
					foreach (FieldDefinitionHandle fieldDefinitionHandle in module.GetFieldsOfTypeOrThrow(this._handle))
					{
						try
						{
							hashSet.Add(module.GetFieldDefNameOrThrow(fieldDefinitionHandle));
						}
						catch (BadImageFormatException)
						{
						}
					}
				}
				catch (BadImageFormatException)
				{
				}
				if (this.IsValueType)
				{
					hashSet.Add(".ctor");
				}
				Interlocked.CompareExchange<ICollection<string>>(ref this._lazyMemberNames, PENamedTypeSymbol.CreateReadOnlyMemberNames(hashSet), null);
			}
		}

		// Token: 0x06007EFA RID: 32506 RVA: 0x001C8C0C File Offset: 0x001C6E0C
		private static ICollection<string> CreateReadOnlyMemberNames(HashSet<string> names)
		{
			switch (names.Count)
			{
			case 0:
				return SpecializedCollections.EmptySet<string>();
			case 1:
				return SpecializedCollections.SingletonCollection<string>(Enumerable.First<string>(names));
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
				return ImmutableArray.CreateRange<string>(names);
			default:
				return SpecializedCollections.ReadOnlySet<string>(names);
			}
		}

		// Token: 0x06007EFB RID: 32507 RVA: 0x001C8C68 File Offset: 0x001C6E68
		public override ImmutableArray<Symbol> GetMembers()
		{
			this.EnsureAllMembersAreLoaded();
			return this._lazyMembersInDeclarationOrder;
		}

		// Token: 0x06007EFC RID: 32508 RVA: 0x001C8C76 File Offset: 0x001C6E76
		private IEnumerable<FieldSymbol> GetEnumFieldsToEmit()
		{
			PENamedTypeSymbol.UncommonProperties uncommon = this.GetUncommonProperties();
			if (uncommon == PENamedTypeSymbol.s_noUncommonProperties)
			{
				yield break;
			}
			PEModuleSymbol containingPEModule = this.ContainingPEModule;
			PEModule module = containingPEModule.Module;
			ArrayBuilder<FieldDefinitionHandle> fieldDefs = ArrayBuilder<FieldDefinitionHandle>.GetInstance();
			try
			{
				foreach (FieldDefinitionHandle fieldDefinitionHandle in module.GetFieldsOfTypeOrThrow(this._handle))
				{
					fieldDefs.Add(fieldDefinitionHandle);
				}
			}
			catch (BadImageFormatException)
			{
			}
			if (uncommon.lazyInstanceEnumFields.IsDefault)
			{
				ArrayBuilder<PEFieldSymbol> instance = ArrayBuilder<PEFieldSymbol>.GetInstance();
				foreach (FieldDefinitionHandle fieldDefinitionHandle2 in fieldDefs)
				{
					try
					{
						FieldAttributes fieldDefFlagsOrThrow = module.GetFieldDefFlagsOrThrow(fieldDefinitionHandle2);
						if ((fieldDefFlagsOrThrow & 16) == null && ModuleExtensions.ShouldImportField(fieldDefFlagsOrThrow, containingPEModule.ImportOptions))
						{
							instance.Add(new PEFieldSymbol(containingPEModule, this, fieldDefinitionHandle2));
						}
					}
					catch (BadImageFormatException)
					{
					}
				}
				ImmutableInterlocked.InterlockedInitialize<PEFieldSymbol>(ref uncommon.lazyInstanceEnumFields, instance.ToImmutableAndFree());
			}
			int staticIndex = 0;
			ImmutableArray<Symbol> staticFields = this.GetMembers();
			int instanceIndex = 0;
			foreach (FieldDefinitionHandle fieldDefinitionHandle3 in fieldDefs)
			{
				if (instanceIndex < uncommon.lazyInstanceEnumFields.Length && uncommon.lazyInstanceEnumFields[instanceIndex].Handle == fieldDefinitionHandle3)
				{
					yield return uncommon.lazyInstanceEnumFields[instanceIndex];
					int num = instanceIndex;
					instanceIndex = num + 1;
				}
				else if (staticIndex < staticFields.Length && staticFields[staticIndex].Kind == SymbolKind.Field)
				{
					PEFieldSymbol pefieldSymbol = (PEFieldSymbol)staticFields[staticIndex];
					if (pefieldSymbol.Handle == fieldDefinitionHandle3)
					{
						yield return pefieldSymbol;
						int num = staticIndex;
						staticIndex = num + 1;
					}
				}
			}
			ArrayBuilder<FieldDefinitionHandle>.Enumerator enumerator3 = default(ArrayBuilder<FieldDefinitionHandle>.Enumerator);
			fieldDefs.Free();
			yield break;
		}

		// Token: 0x06007EFD RID: 32509 RVA: 0x001C8C88 File Offset: 0x001C6E88
		internal override IEnumerable<FieldSymbol> GetFieldsToEmit()
		{
			if (this.TypeKind == TypeKind.Enum)
			{
				return this.GetEnumFieldsToEmit();
			}
			IEnumerable<FieldSymbol> members = PENamedTypeSymbol.GetMembers<FieldSymbol>(this.GetMembers().WhereAsArray<Symbol>((Symbol m) => !(m is TupleErrorFieldSymbol)), SymbolKind.Field, 0);
			ArrayBuilder<FieldSymbol> arrayBuilder = null;
			foreach (EventSymbol eventSymbol in this.GetEventsToEmit())
			{
				FieldSymbol associatedField = eventSymbol.AssociatedField;
				if (associatedField != null)
				{
					if (arrayBuilder == null)
					{
						arrayBuilder = ArrayBuilder<FieldSymbol>.GetInstance();
					}
					arrayBuilder.Add(associatedField);
				}
			}
			if (arrayBuilder == null)
			{
				return members;
			}
			SmallDictionary<FieldDefinitionHandle, FieldSymbol> smallDictionary = new SmallDictionary<FieldDefinitionHandle, FieldSymbol>();
			int num = 0;
			foreach (FieldSymbol fieldSymbol in members)
			{
				PEFieldSymbol pefieldSymbol = (PEFieldSymbol)fieldSymbol;
				smallDictionary.Add(pefieldSymbol.Handle, pefieldSymbol);
				num++;
			}
			foreach (FieldSymbol fieldSymbol2 in arrayBuilder)
			{
				PEFieldSymbol pefieldSymbol2 = (PEFieldSymbol)fieldSymbol2;
				smallDictionary.Add(pefieldSymbol2.Handle, pefieldSymbol2);
			}
			num += arrayBuilder.Count;
			arrayBuilder.Free();
			ArrayBuilder<FieldSymbol> instance = ArrayBuilder<FieldSymbol>.GetInstance(num);
			try
			{
				foreach (FieldDefinitionHandle fieldDefinitionHandle in this.ContainingPEModule.Module.GetFieldsOfTypeOrThrow(this._handle))
				{
					FieldSymbol fieldSymbol3;
					if (smallDictionary.TryGetValue(fieldDefinitionHandle, out fieldSymbol3))
					{
						instance.Add(fieldSymbol3);
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06007EFE RID: 32510 RVA: 0x001C8E58 File Offset: 0x001C7058
		internal override IEnumerable<MethodSymbol> GetMethodsToEmit()
		{
			ImmutableArray<Symbol> members = this.GetMembers();
			int index = PENamedTypeSymbol.GetIndexOfFirstMember(members, SymbolKind.Method);
			if (!this.IsInterfaceType())
			{
				while (index < members.Length)
				{
					if (members[index].Kind != SymbolKind.Method)
					{
						IL_02BF:
						yield break;
					}
					MethodSymbol methodSymbol = (MethodSymbol)members[index];
					if (!methodSymbol.IsDefaultValueTypeConstructor())
					{
						yield return methodSymbol;
					}
					int num = index;
					index = num + 1;
				}
				goto IL_02BF;
			}
			if (index >= members.Length || members[index].Kind != SymbolKind.Method)
			{
				yield break;
			}
			PEMethodSymbol method = (PEMethodSymbol)members[index];
			PEModule module = this.ContainingPEModule.Module;
			ArrayBuilder<MethodDefinitionHandle> methodDefs = ArrayBuilder<MethodDefinitionHandle>.GetInstance();
			try
			{
				foreach (MethodDefinitionHandle methodDefinitionHandle in module.GetMethodsOfTypeOrThrow(this._handle))
				{
					methodDefs.Add(methodDefinitionHandle);
				}
			}
			catch (BadImageFormatException)
			{
			}
			foreach (MethodDefinitionHandle methodDefinitionHandle2 in methodDefs)
			{
				if (method.Handle == methodDefinitionHandle2)
				{
					yield return method;
					int num = index;
					index = num + 1;
					if (index == members.Length || members[index].Kind != SymbolKind.Method)
					{
						methodDefs.Free();
						yield break;
					}
					method = (PEMethodSymbol)members[index];
				}
				else
				{
					int gapSize;
					try
					{
						gapSize = ModuleExtensions.GetVTableGapSize(module.GetMethodDefNameOrThrow(methodDefinitionHandle2));
					}
					catch (BadImageFormatException)
					{
						gapSize = 1;
					}
					do
					{
						yield return null;
						int num = gapSize;
						gapSize = num - 1;
					}
					while (gapSize > 0);
				}
			}
			ArrayBuilder<MethodDefinitionHandle>.Enumerator enumerator2 = default(ArrayBuilder<MethodDefinitionHandle>.Enumerator);
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06007EFF RID: 32511 RVA: 0x001C8E68 File Offset: 0x001C7068
		internal override IEnumerable<PropertySymbol> GetPropertiesToEmit()
		{
			return PENamedTypeSymbol.GetMembers<PropertySymbol>(this.GetMembers(), SymbolKind.Property, -1);
		}

		// Token: 0x06007F00 RID: 32512 RVA: 0x001C8E78 File Offset: 0x001C7078
		internal override IEnumerable<EventSymbol> GetEventsToEmit()
		{
			return PENamedTypeSymbol.GetMembers<EventSymbol>(this.GetMembers(), SymbolKind.Event, -1);
		}

		// Token: 0x06007F01 RID: 32513 RVA: 0x001C8E87 File Offset: 0x001C7087
		internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers()
		{
			return this.GetMembersUnordered();
		}

		// Token: 0x06007F02 RID: 32514 RVA: 0x001C8E8F File Offset: 0x001C708F
		internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers(string name)
		{
			return this.GetMembers(name);
		}

		// Token: 0x06007F03 RID: 32515 RVA: 0x001C8E98 File Offset: 0x001C7098
		private void EnsureEnumUnderlyingTypeIsLoaded(PENamedTypeSymbol.UncommonProperties uncommon)
		{
			if (uncommon.lazyEnumUnderlyingType == null && this.TypeKind == TypeKind.Enum)
			{
				PEModuleSymbol containingPEModule = this.ContainingPEModule;
				PEModule module = containingPEModule.Module;
				MetadataDecoder metadataDecoder = new MetadataDecoder(containingPEModule, this);
				NamedTypeSymbol namedTypeSymbol = null;
				try
				{
					foreach (FieldDefinitionHandle fieldDefinitionHandle in module.GetFieldsOfTypeOrThrow(this._handle))
					{
						FieldAttributes fieldDefFlagsOrThrow;
						try
						{
							fieldDefFlagsOrThrow = module.GetFieldDefFlagsOrThrow(fieldDefinitionHandle);
						}
						catch (BadImageFormatException)
						{
							continue;
						}
						if ((fieldDefFlagsOrThrow & 16) == null)
						{
							ImmutableArray<ModifierInfo<TypeSymbol>> immutableArray;
							TypeSymbol typeSymbol = metadataDecoder.DecodeFieldSignature(fieldDefinitionHandle, out immutableArray);
							if (typeSymbol.SpecialType.IsValidEnumUnderlyingType() && !immutableArray.AnyRequired<TypeSymbol>())
							{
								if (namedTypeSymbol == null)
								{
									namedTypeSymbol = (NamedTypeSymbol)typeSymbol;
								}
								else
								{
									namedTypeSymbol = new UnsupportedMetadataTypeSymbol(null);
								}
							}
						}
					}
					if (namedTypeSymbol == null)
					{
						namedTypeSymbol = new UnsupportedMetadataTypeSymbol(null);
					}
				}
				catch (BadImageFormatException ex)
				{
					if (namedTypeSymbol == null)
					{
						namedTypeSymbol = new UnsupportedMetadataTypeSymbol(ex);
					}
				}
				Interlocked.CompareExchange<NamedTypeSymbol>(ref uncommon.lazyEnumUnderlyingType, namedTypeSymbol, null);
			}
		}

		// Token: 0x06007F04 RID: 32516 RVA: 0x001C8FAC File Offset: 0x001C71AC
		private void EnsureAllMembersAreLoaded()
		{
			if (this._lazyMembersByName == null)
			{
				this.LoadMembers();
			}
		}

		// Token: 0x06007F05 RID: 32517 RVA: 0x001C8FBC File Offset: 0x001C71BC
		private void LoadMembers()
		{
			ArrayBuilder<Symbol> arrayBuilder = null;
			if (this._lazyMembersInDeclarationOrder.IsDefault)
			{
				this.EnsureNestedTypesAreLoaded();
				arrayBuilder = ArrayBuilder<Symbol>.GetInstance();
				if (this.TypeKind == TypeKind.Enum)
				{
					this.EnsureEnumUnderlyingTypeIsLoaded(this.GetUncommonProperties());
					PEModuleSymbol containingPEModule = this.ContainingPEModule;
					PEModule module = containingPEModule.Module;
					try
					{
						foreach (FieldDefinitionHandle fieldDefinitionHandle in module.GetFieldsOfTypeOrThrow(this._handle))
						{
							FieldAttributes fieldAttributes;
							try
							{
								fieldAttributes = module.GetFieldDefFlagsOrThrow(fieldDefinitionHandle);
								if ((fieldAttributes & 16) == null)
								{
									continue;
								}
							}
							catch (BadImageFormatException)
							{
								fieldAttributes = 0;
							}
							if (ModuleExtensions.ShouldImportField(fieldAttributes, containingPEModule.ImportOptions))
							{
								PEFieldSymbol pefieldSymbol = new PEFieldSymbol(containingPEModule, this, fieldDefinitionHandle);
								arrayBuilder.Add(pefieldSymbol);
							}
						}
					}
					catch (BadImageFormatException)
					{
					}
					SynthesizedInstanceConstructor synthesizedInstanceConstructor = new SynthesizedInstanceConstructor(this);
					arrayBuilder.Add(synthesizedInstanceConstructor);
				}
				else
				{
					ArrayBuilder<PEFieldSymbol> instance = ArrayBuilder<PEFieldSymbol>.GetInstance();
					ArrayBuilder<Symbol> instance2 = ArrayBuilder<Symbol>.GetInstance();
					MultiDictionary<string, PEFieldSymbol> multiDictionary = this.CreateFields(instance);
					PooledDictionary<MethodDefinitionHandle, PEMethodSymbol> pooledDictionary = this.CreateMethods(instance2);
					if (this.TypeKind == TypeKind.Struct)
					{
						bool flag = false;
						ArrayBuilder<Symbol>.Enumerator enumerator2 = instance2.GetEnumerator();
						while (enumerator2.MoveNext())
						{
							if (((MethodSymbol)enumerator2.Current).IsParameterlessConstructor())
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							instance2.Insert(0, new SynthesizedInstanceConstructor(this));
						}
					}
					this.CreateProperties(pooledDictionary, instance2);
					this.CreateEvents(multiDictionary, pooledDictionary, instance2);
					foreach (PEFieldSymbol pefieldSymbol2 in instance)
					{
						if (pefieldSymbol2.AssociatedSymbol == null)
						{
							arrayBuilder.Add(pefieldSymbol2);
						}
					}
					arrayBuilder.AddRange(instance2);
					instance2.Free();
					instance.Free();
					pooledDictionary.Free();
				}
				int num = arrayBuilder.Count;
				foreach (ImmutableArray<PENamedTypeSymbol> immutableArray in this._lazyNestedTypes.Values)
				{
					arrayBuilder.AddRange<PENamedTypeSymbol>(immutableArray);
				}
				arrayBuilder.Sort(num, PENamedTypeSymbol.DeclarationOrderTypeSymbolComparer.Instance);
				if (this.IsTupleType)
				{
					int count = arrayBuilder.Count;
					arrayBuilder = base.AddOrWrapTupleMembers(arrayBuilder.ToImmutableAndFree());
					num += arrayBuilder.Count - count;
				}
				ImmutableArray<Symbol> immutableArray2 = arrayBuilder.ToImmutable();
				if (!ImmutableInterlocked.InterlockedInitialize<Symbol>(ref this._lazyMembersInDeclarationOrder, immutableArray2))
				{
					arrayBuilder.Free();
					arrayBuilder = null;
				}
				else
				{
					arrayBuilder.Clip(num);
				}
			}
			if (this._lazyMembersByName == null)
			{
				if (arrayBuilder == null)
				{
					arrayBuilder = ArrayBuilder<Symbol>.GetInstance();
					foreach (Symbol symbol in this._lazyMembersInDeclarationOrder)
					{
						if (symbol.Kind == SymbolKind.NamedType)
						{
							break;
						}
						arrayBuilder.Add(symbol);
					}
				}
				Dictionary<string, ImmutableArray<Symbol>> dictionary = PENamedTypeSymbol.GroupByName(arrayBuilder);
				if (Interlocked.CompareExchange<Dictionary<string, ImmutableArray<Symbol>>>(ref this._lazyMembersByName, dictionary, null) == null)
				{
					ICollection<string> collection = SpecializedCollections.ReadOnlyCollection<string>(dictionary.Keys);
					Interlocked.Exchange<ICollection<string>>(ref this._lazyMemberNames, collection);
				}
			}
			if (arrayBuilder != null)
			{
				arrayBuilder.Free();
			}
		}

		// Token: 0x06007F06 RID: 32518 RVA: 0x001C92C8 File Offset: 0x001C74C8
		internal override ImmutableArray<Symbol> GetSimpleNonTypeMembers(string name)
		{
			this.EnsureAllMembersAreLoaded();
			ImmutableArray<Symbol> empty;
			if (!this._lazyMembersByName.TryGetValue(name, ref empty))
			{
				empty = ImmutableArray<Symbol>.Empty;
			}
			return empty;
		}

		// Token: 0x06007F07 RID: 32519 RVA: 0x001C92F4 File Offset: 0x001C74F4
		public override ImmutableArray<Symbol> GetMembers(string name)
		{
			this.EnsureAllMembersAreLoaded();
			ImmutableArray<Symbol> immutableArray;
			if (!this._lazyMembersByName.TryGetValue(name, ref immutableArray))
			{
				immutableArray = ImmutableArray<Symbol>.Empty;
			}
			ImmutableArray<PENamedTypeSymbol> immutableArray2;
			if (this._lazyNestedTypes.TryGetValue(name, ref immutableArray2))
			{
				immutableArray = immutableArray.Concat<Symbol>(StaticCast<Symbol>.From<PENamedTypeSymbol>(immutableArray2));
			}
			return immutableArray;
		}

		// Token: 0x06007F08 RID: 32520 RVA: 0x001C933B File Offset: 0x001C753B
		internal sealed override bool HasPossibleWellKnownCloneMethod()
		{
			return this.MemberNames.Contains("<Clone>$");
		}

		// Token: 0x170025CB RID: 9675
		// (get) Token: 0x06007F09 RID: 32521 RVA: 0x001C9350 File Offset: 0x001C7550
		internal override FieldSymbol FixedElementField
		{
			get
			{
				FieldSymbol fieldSymbol = null;
				ImmutableArray<Symbol> members = this.GetMembers("FixedElementField");
				if (!members.IsDefault && members.Length == 1)
				{
					fieldSymbol = members[0] as FieldSymbol;
				}
				return fieldSymbol;
			}
		}

		// Token: 0x06007F0A RID: 32522 RVA: 0x001C938D File Offset: 0x001C758D
		internal override ImmutableArray<NamedTypeSymbol> GetTypeMembersUnordered()
		{
			return this.GetTypeMembers().ConditionallyDeOrder<NamedTypeSymbol>();
		}

		// Token: 0x06007F0B RID: 32523 RVA: 0x001C939A File Offset: 0x001C759A
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			this.EnsureNestedTypesAreLoaded();
			return this.GetMemberTypesPrivate();
		}

		// Token: 0x06007F0C RID: 32524 RVA: 0x001C93A8 File Offset: 0x001C75A8
		private ImmutableArray<NamedTypeSymbol> GetMemberTypesPrivate()
		{
			ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance();
			foreach (ImmutableArray<PENamedTypeSymbol> immutableArray in this._lazyNestedTypes.Values)
			{
				instance.AddRange<PENamedTypeSymbol>(immutableArray);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06007F0D RID: 32525 RVA: 0x001C940C File Offset: 0x001C760C
		private void EnsureNestedTypesAreLoaded()
		{
			if (this._lazyNestedTypes == null)
			{
				ArrayBuilder<PENamedTypeSymbol> instance = ArrayBuilder<PENamedTypeSymbol>.GetInstance();
				instance.AddRange(this.CreateNestedTypes());
				Dictionary<string, ImmutableArray<PENamedTypeSymbol>> dictionary = PENamedTypeSymbol.GroupByName(instance);
				if (Interlocked.CompareExchange<Dictionary<string, ImmutableArray<PENamedTypeSymbol>>>(ref this._lazyNestedTypes, dictionary, null) == null)
				{
					this.ContainingPEModule.OnNewTypeDeclarationsLoaded(dictionary);
				}
				instance.Free();
			}
		}

		// Token: 0x06007F0E RID: 32526 RVA: 0x001C945C File Offset: 0x001C765C
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			this.EnsureNestedTypesAreLoaded();
			ImmutableArray<PENamedTypeSymbol> immutableArray;
			if (this._lazyNestedTypes.TryGetValue(name, ref immutableArray))
			{
				return StaticCast<NamedTypeSymbol>.From<PENamedTypeSymbol>(immutableArray);
			}
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06007F0F RID: 32527 RVA: 0x001C948B File Offset: 0x001C768B
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
		{
			return this.GetTypeMembers(name).WhereAsArray((NamedTypeSymbol type, int arity) => type.Arity == arity, arity);
		}

		// Token: 0x170025CC RID: 9676
		// (get) Token: 0x06007F10 RID: 32528 RVA: 0x001C94B9 File Offset: 0x001C76B9
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this.ContainingPEModule.MetadataLocation.Cast<MetadataLocation, Location>();
			}
		}

		// Token: 0x170025CD RID: 9677
		// (get) Token: 0x06007F11 RID: 32529 RVA: 0x001C94CB File Offset: 0x001C76CB
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x170025CE RID: 9678
		// (get) Token: 0x06007F12 RID: 32530 RVA: 0x001C94D2 File Offset: 0x001C76D2
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170025CF RID: 9679
		// (get) Token: 0x06007F13 RID: 32531 RVA: 0x001C94DA File Offset: 0x001C76DA
		internal override bool HasSpecialName
		{
			get
			{
				return (this._flags & 1024) > 0;
			}
		}

		// Token: 0x170025D0 RID: 9680
		// (get) Token: 0x06007F14 RID: 32532 RVA: 0x001C94EB File Offset: 0x001C76EB
		internal override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotationsNoUseSiteDiagnostics
		{
			get
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x170025D1 RID: 9681
		// (get) Token: 0x06007F15 RID: 32533 RVA: 0x001C94F2 File Offset: 0x001C76F2
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x170025D2 RID: 9682
		// (get) Token: 0x06007F16 RID: 32534 RVA: 0x001C94F9 File Offset: 0x001C76F9
		public override bool IsStatic
		{
			get
			{
				return (this._flags & 256) != null && (this._flags & 128) > 0;
			}
		}

		// Token: 0x170025D3 RID: 9683
		// (get) Token: 0x06007F17 RID: 32535 RVA: 0x001C951A File Offset: 0x001C771A
		public override bool IsAbstract
		{
			get
			{
				return (this._flags & 128) != null && (this._flags & 256) == 0;
			}
		}

		// Token: 0x170025D4 RID: 9684
		// (get) Token: 0x06007F18 RID: 32536 RVA: 0x001C953B File Offset: 0x001C773B
		internal override bool IsMetadataAbstract
		{
			get
			{
				return (this._flags & 128) > 0;
			}
		}

		// Token: 0x170025D5 RID: 9685
		// (get) Token: 0x06007F19 RID: 32537 RVA: 0x001C954C File Offset: 0x001C774C
		public override bool IsSealed
		{
			get
			{
				return (this._flags & 256) != null && (this._flags & 128) == 0;
			}
		}

		// Token: 0x170025D6 RID: 9686
		// (get) Token: 0x06007F1A RID: 32538 RVA: 0x001C956D File Offset: 0x001C776D
		internal override bool IsMetadataSealed
		{
			get
			{
				return (this._flags & 256) > 0;
			}
		}

		// Token: 0x170025D7 RID: 9687
		// (get) Token: 0x06007F1B RID: 32539 RVA: 0x001C957E File Offset: 0x001C777E
		internal TypeAttributes Flags
		{
			get
			{
				return this._flags;
			}
		}

		// Token: 0x170025D8 RID: 9688
		// (get) Token: 0x06007F1C RID: 32540 RVA: 0x001C9586 File Offset: 0x001C7786
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x170025D9 RID: 9689
		// (get) Token: 0x06007F1D RID: 32541 RVA: 0x001C9590 File Offset: 0x001C7790
		public override bool MightContainExtensionMethods
		{
			get
			{
				PENamedTypeSymbol.UncommonProperties uncommonProperties = this.GetUncommonProperties();
				if (uncommonProperties == PENamedTypeSymbol.s_noUncommonProperties)
				{
					return false;
				}
				if (!uncommonProperties.lazyContainsExtensionMethods.HasValue())
				{
					ThreeState threeState = ThreeState.False;
					TypeKind typeKind = this.TypeKind;
					if (typeKind - TypeKind.Class <= 1 || typeKind == TypeKind.Struct)
					{
						bool flag = this.ContainingPEModule.Module.HasExtensionAttribute(this._handle, false);
						PEAssemblySymbol peassemblySymbol = this.ContainingAssembly as PEAssemblySymbol;
						if (peassemblySymbol != null)
						{
							threeState = (flag && peassemblySymbol.MightContainExtensionMethods).ToThreeState();
						}
						else
						{
							threeState = flag.ToThreeState();
						}
					}
					uncommonProperties.lazyContainsExtensionMethods = threeState;
				}
				return uncommonProperties.lazyContainsExtensionMethods.Value();
			}
		}

		// Token: 0x170025DA RID: 9690
		// (get) Token: 0x06007F1E RID: 32542 RVA: 0x001C962C File Offset: 0x001C782C
		public override TypeKind TypeKind
		{
			get
			{
				TypeKind typeKind = this._lazyKind;
				if (typeKind == TypeKind.Unknown)
				{
					if (this._flags.IsInterface())
					{
						typeKind = TypeKind.Interface;
					}
					else
					{
						TypeSymbol declaredBaseType = this.GetDeclaredBaseType(true);
						typeKind = TypeKind.Class;
						if (declaredBaseType != null)
						{
							switch (declaredBaseType.SpecialType)
							{
							case SpecialType.System_Enum:
								typeKind = TypeKind.Enum;
								break;
							case SpecialType.System_MulticastDelegate:
								typeKind = TypeKind.Delegate;
								break;
							case SpecialType.System_ValueType:
								if (this.SpecialType != SpecialType.System_Enum)
								{
									typeKind = TypeKind.Struct;
								}
								break;
							}
						}
					}
					this._lazyKind = typeKind;
				}
				return typeKind;
			}
		}

		// Token: 0x170025DB RID: 9691
		// (get) Token: 0x06007F1F RID: 32543 RVA: 0x001C969E File Offset: 0x001C789E
		internal sealed override bool IsInterface
		{
			get
			{
				return this._flags.IsInterface();
			}
		}

		// Token: 0x06007F20 RID: 32544 RVA: 0x001C96AC File Offset: 0x001C78AC
		private static ExtendedErrorTypeSymbol CyclicInheritanceError(PENamedTypeSymbol type, TypeSymbol declaredBase)
		{
			CSDiagnosticInfo csdiagnosticInfo = new CSDiagnosticInfo(ErrorCode.ERR_ImportedCircularBase, new object[] { declaredBase, type });
			return new ExtendedErrorTypeSymbol(declaredBase, LookupResultKind.NotReferencable, csdiagnosticInfo, true);
		}

		// Token: 0x06007F21 RID: 32545 RVA: 0x001C96DC File Offset: 0x001C78DC
		private NamedTypeSymbol MakeAcyclicBaseType()
		{
			NamedTypeSymbol declaredBaseType = this.GetDeclaredBaseType(null);
			if (declaredBaseType == null)
			{
				return null;
			}
			if (BaseTypeAnalysis.TypeDependsOn(declaredBaseType, this))
			{
				return PENamedTypeSymbol.CyclicInheritanceError(this, declaredBaseType);
			}
			base.SetKnownToHaveNoDeclaredBaseCycles();
			return declaredBaseType;
		}

		// Token: 0x06007F22 RID: 32546 RVA: 0x001C9710 File Offset: 0x001C7910
		private ImmutableArray<NamedTypeSymbol> MakeAcyclicInterfaces()
		{
			ImmutableArray<NamedTypeSymbol> declaredInterfaces = this.GetDeclaredInterfaces(null);
			if (!this.IsInterface)
			{
				return declaredInterfaces;
			}
			return declaredInterfaces.SelectAsArray<NamedTypeSymbol, NamedTypeSymbol>(delegate(NamedTypeSymbol t)
			{
				if (!BaseTypeAnalysis.TypeDependsOn(t, this))
				{
					return t;
				}
				return PENamedTypeSymbol.CyclicInheritanceError(this, t);
			});
		}

		// Token: 0x06007F23 RID: 32547 RVA: 0x001C9741 File Offset: 0x001C7941
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return PEDocumentationCommentUtils.GetDocumentationComment(this, this.ContainingPEModule, preferredCulture, cancellationToken, ref this._lazyDocComment);
		}

		// Token: 0x06007F24 RID: 32548 RVA: 0x001C9757 File Offset: 0x001C7957
		private IEnumerable<PENamedTypeSymbol> CreateNestedTypes()
		{
			PEModuleSymbol moduleSymbol = this.ContainingPEModule;
			PEModule module = moduleSymbol.Module;
			ImmutableArray<TypeDefinitionHandle> nestedTypeDefsOrThrow;
			try
			{
				nestedTypeDefsOrThrow = module.GetNestedTypeDefsOrThrow(this._handle);
			}
			catch (BadImageFormatException)
			{
				yield break;
			}
			foreach (TypeDefinitionHandle typeDefinitionHandle in nestedTypeDefsOrThrow)
			{
				if (module.ShouldImportNestedType(typeDefinitionHandle))
				{
					yield return PENamedTypeSymbol.Create(moduleSymbol, this, typeDefinitionHandle);
				}
			}
			ImmutableArray<TypeDefinitionHandle>.Enumerator enumerator = default(ImmutableArray<TypeDefinitionHandle>.Enumerator);
			yield break;
		}

		// Token: 0x06007F25 RID: 32549 RVA: 0x001C9768 File Offset: 0x001C7968
		private MultiDictionary<string, PEFieldSymbol> CreateFields(ArrayBuilder<PEFieldSymbol> fieldMembers)
		{
			MultiDictionary<string, PEFieldSymbol> multiDictionary = new MultiDictionary<string, PEFieldSymbol>();
			PEModuleSymbol containingPEModule = this.ContainingPEModule;
			PEModule module = containingPEModule.Module;
			bool flag = false;
			bool flag2 = false;
			if (this.TypeKind == TypeKind.Struct)
			{
				if (this.SpecialType == SpecialType.None)
				{
					flag = true;
					flag2 = this.ContainingAssembly.IsLinked;
				}
				else
				{
					flag = this.SpecialType == SpecialType.System_Nullable_T;
				}
			}
			try
			{
				foreach (FieldDefinitionHandle fieldDefinitionHandle in module.GetFieldsOfTypeOrThrow(this._handle))
				{
					try
					{
						if (!flag2 && (!flag || (module.GetFieldDefFlagsOrThrow(fieldDefinitionHandle) & 16) != null) && !module.ShouldImportField(fieldDefinitionHandle, containingPEModule.ImportOptions))
						{
							continue;
						}
					}
					catch (BadImageFormatException)
					{
					}
					PEFieldSymbol pefieldSymbol = new PEFieldSymbol(containingPEModule, this, fieldDefinitionHandle);
					fieldMembers.Add(pefieldSymbol);
					if (pefieldSymbol.DeclaredAccessibility == Accessibility.Private)
					{
						string name = pefieldSymbol.Name;
						if (name.Length > 0)
						{
							multiDictionary.Add(name, pefieldSymbol);
						}
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			return multiDictionary;
		}

		// Token: 0x06007F26 RID: 32550 RVA: 0x001C988C File Offset: 0x001C7A8C
		private PooledDictionary<MethodDefinitionHandle, PEMethodSymbol> CreateMethods(ArrayBuilder<Symbol> members)
		{
			PEModuleSymbol containingPEModule = this.ContainingPEModule;
			PEModule module = containingPEModule.Module;
			PooledDictionary<MethodDefinitionHandle, PEMethodSymbol> instance = PooledDictionary<MethodDefinitionHandle, PEMethodSymbol>.GetInstance();
			bool flag = this.TypeKind == TypeKind.Struct && this.SpecialType == SpecialType.None && this.ContainingAssembly.IsLinked;
			try
			{
				foreach (MethodDefinitionHandle methodDefinitionHandle in module.GetMethodsOfTypeOrThrow(this._handle))
				{
					if (flag || module.ShouldImportMethod(methodDefinitionHandle, containingPEModule.ImportOptions))
					{
						PEMethodSymbol pemethodSymbol = new PEMethodSymbol(containingPEModule, this, methodDefinitionHandle);
						members.Add(pemethodSymbol);
						instance.Add(methodDefinitionHandle, pemethodSymbol);
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			return instance;
		}

		// Token: 0x06007F27 RID: 32551 RVA: 0x001C995C File Offset: 0x001C7B5C
		private void CreateProperties(Dictionary<MethodDefinitionHandle, PEMethodSymbol> methodHandleToSymbol, ArrayBuilder<Symbol> members)
		{
			PEModuleSymbol containingPEModule = this.ContainingPEModule;
			PEModule module = containingPEModule.Module;
			try
			{
				foreach (PropertyDefinitionHandle propertyDefinitionHandle in module.GetPropertiesOfTypeOrThrow(this._handle))
				{
					try
					{
						PropertyAccessors propertyMethodsOrThrow = module.GetPropertyMethodsOrThrow(propertyDefinitionHandle);
						PEMethodSymbol accessorMethod = this.GetAccessorMethod(module, methodHandleToSymbol, propertyMethodsOrThrow.Getter);
						PEMethodSymbol accessorMethod2 = this.GetAccessorMethod(module, methodHandleToSymbol, propertyMethodsOrThrow.Setter);
						if (accessorMethod != null || accessorMethod2 != null)
						{
							members.Add(PEPropertySymbol.Create(containingPEModule, this, propertyDefinitionHandle, accessorMethod, accessorMethod2));
						}
					}
					catch (BadImageFormatException)
					{
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
		}

		// Token: 0x06007F28 RID: 32552 RVA: 0x001C9A2C File Offset: 0x001C7C2C
		private void CreateEvents(MultiDictionary<string, PEFieldSymbol> privateFieldNameToSymbols, Dictionary<MethodDefinitionHandle, PEMethodSymbol> methodHandleToSymbol, ArrayBuilder<Symbol> members)
		{
			PEModuleSymbol containingPEModule = this.ContainingPEModule;
			PEModule module = containingPEModule.Module;
			try
			{
				foreach (EventDefinitionHandle eventDefinitionHandle in module.GetEventsOfTypeOrThrow(this._handle))
				{
					try
					{
						EventAccessors eventMethodsOrThrow = module.GetEventMethodsOrThrow(eventDefinitionHandle);
						PEMethodSymbol accessorMethod = this.GetAccessorMethod(module, methodHandleToSymbol, eventMethodsOrThrow.Adder);
						PEMethodSymbol accessorMethod2 = this.GetAccessorMethod(module, methodHandleToSymbol, eventMethodsOrThrow.Remover);
						if (accessorMethod != null || accessorMethod2 != null)
						{
							members.Add(new PEEventSymbol(containingPEModule, this, eventDefinitionHandle, accessorMethod, accessorMethod2, privateFieldNameToSymbols));
						}
					}
					catch (BadImageFormatException)
					{
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
		}

		// Token: 0x06007F29 RID: 32553 RVA: 0x001C9AFC File Offset: 0x001C7CFC
		private PEMethodSymbol GetAccessorMethod(PEModule module, Dictionary<MethodDefinitionHandle, PEMethodSymbol> methodHandleToSymbol, MethodDefinitionHandle methodDef)
		{
			if (methodDef.IsNil)
			{
				return null;
			}
			PEMethodSymbol pemethodSymbol;
			methodHandleToSymbol.TryGetValue(methodDef, ref pemethodSymbol);
			return pemethodSymbol;
		}

		// Token: 0x06007F2A RID: 32554 RVA: 0x001C9B1F File Offset: 0x001C7D1F
		private static Dictionary<string, ImmutableArray<Symbol>> GroupByName(ArrayBuilder<Symbol> symbols)
		{
			return symbols.ToDictionary<string>((Symbol s) => s.Name, StringOrdinalComparer.Instance);
		}

		// Token: 0x06007F2B RID: 32555 RVA: 0x001C9B4B File Offset: 0x001C7D4B
		private static Dictionary<string, ImmutableArray<PENamedTypeSymbol>> GroupByName(ArrayBuilder<PENamedTypeSymbol> symbols)
		{
			if (symbols.Count == 0)
			{
				return PENamedTypeSymbol.s_emptyNestedTypes;
			}
			return symbols.ToDictionary<string>((PENamedTypeSymbol s) => s.Name, StringOrdinalComparer.Instance);
		}

		// Token: 0x06007F2C RID: 32556 RVA: 0x001C9B88 File Offset: 0x001C7D88
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			if (!this._lazyCachedUseSiteInfo.IsInitialized)
			{
				AssemblySymbol primaryDependency = base.PrimaryDependency;
				this._lazyCachedUseSiteInfo.Initialize(primaryDependency, new UseSiteInfo<AssemblySymbol>(primaryDependency).AdjustDiagnosticInfo(this.GetUseSiteDiagnosticImpl()));
			}
			return this._lazyCachedUseSiteInfo.ToUseSiteInfo(base.PrimaryDependency);
		}

		// Token: 0x06007F2D RID: 32557 RVA: 0x001C9BDC File Offset: 0x001C7DDC
		protected virtual DiagnosticInfo GetUseSiteDiagnosticImpl()
		{
			DiagnosticInfo diagnosticInfo = null;
			if (!base.MergeUseSiteDiagnostics(ref diagnosticInfo, base.CalculateUseSiteDiagnostic()))
			{
				if (this.ContainingPEModule.Module.HasRequiredAttributeAttribute(this._handle))
				{
					diagnosticInfo = new CSDiagnosticInfo(ErrorCode.ERR_BogusType, new object[] { this });
				}
				else if (this.TypeKind == TypeKind.Class && this.SpecialType != SpecialType.System_Enum)
				{
					TypeSymbol declaredBaseType = this.GetDeclaredBaseType(null);
					if (declaredBaseType != null && declaredBaseType.SpecialType == SpecialType.None)
					{
						AssemblySymbol containingAssembly = declaredBaseType.ContainingAssembly;
						if (containingAssembly != null && containingAssembly.IsMissing)
						{
							MissingMetadataTypeSymbol.TopLevel topLevel = declaredBaseType as MissingMetadataTypeSymbol.TopLevel;
							if (topLevel != null && topLevel.Arity == 0)
							{
								SpecialType typeFromMetadataName = SpecialTypes.GetTypeFromMetadataName(MetadataHelpers.BuildQualifiedName(topLevel.NamespaceName, topLevel.MetadataName));
								if (typeFromMetadataName - SpecialType.System_Enum <= 1 || typeFromMetadataName == SpecialType.System_ValueType)
								{
									diagnosticInfo = topLevel.GetUseSiteInfo().DiagnosticInfo;
								}
							}
						}
					}
				}
			}
			return diagnosticInfo;
		}

		// Token: 0x170025DC RID: 9692
		// (get) Token: 0x06007F2E RID: 32558 RVA: 0x001C9CAC File Offset: 0x001C7EAC
		internal string DefaultMemberName
		{
			get
			{
				PENamedTypeSymbol.UncommonProperties uncommonProperties = this.GetUncommonProperties();
				if (uncommonProperties == PENamedTypeSymbol.s_noUncommonProperties)
				{
					return "";
				}
				if (uncommonProperties.lazyDefaultMemberName == null)
				{
					string text;
					this.ContainingPEModule.Module.HasDefaultMemberAttribute(this._handle, out text);
					Interlocked.CompareExchange<string>(ref uncommonProperties.lazyDefaultMemberName, text ?? "", null);
				}
				return uncommonProperties.lazyDefaultMemberName;
			}
		}

		// Token: 0x170025DD RID: 9693
		// (get) Token: 0x06007F2F RID: 32559 RVA: 0x001C9D11 File Offset: 0x001C7F11
		internal override bool IsComImport
		{
			get
			{
				return (this._flags & 4096) > 0;
			}
		}

		// Token: 0x170025DE RID: 9694
		// (get) Token: 0x06007F30 RID: 32560 RVA: 0x001C9D22 File Offset: 0x001C7F22
		internal override bool ShouldAddWinRTMembers
		{
			get
			{
				return this.IsWindowsRuntimeImport;
			}
		}

		// Token: 0x170025DF RID: 9695
		// (get) Token: 0x06007F31 RID: 32561 RVA: 0x001C9D2A File Offset: 0x001C7F2A
		internal override bool IsWindowsRuntimeImport
		{
			get
			{
				return (this._flags & 16384) > 0;
			}
		}

		// Token: 0x06007F32 RID: 32562 RVA: 0x001C9D3B File Offset: 0x001C7F3B
		internal override bool GetGuidString(out string guidString)
		{
			return this.ContainingPEModule.Module.HasGuidAttribute(this._handle, out guidString);
		}

		// Token: 0x170025E0 RID: 9696
		// (get) Token: 0x06007F33 RID: 32563 RVA: 0x001C9D59 File Offset: 0x001C7F59
		internal override TypeLayout Layout
		{
			get
			{
				return this.ContainingPEModule.Module.GetTypeLayout(this._handle);
			}
		}

		// Token: 0x170025E1 RID: 9697
		// (get) Token: 0x06007F34 RID: 32564 RVA: 0x001C9D74 File Offset: 0x001C7F74
		internal override CharSet MarshallingCharSet
		{
			get
			{
				CharSet charSet = this._flags.ToCharSet();
				if (charSet == null)
				{
					return 2;
				}
				return charSet;
			}
		}

		// Token: 0x170025E2 RID: 9698
		// (get) Token: 0x06007F35 RID: 32565 RVA: 0x001C9D93 File Offset: 0x001C7F93
		public override bool IsSerializable
		{
			get
			{
				return (this._flags & 8192) > 0;
			}
		}

		// Token: 0x170025E3 RID: 9699
		// (get) Token: 0x06007F36 RID: 32566 RVA: 0x001C9DA4 File Offset: 0x001C7FA4
		public override bool IsRefLikeType
		{
			get
			{
				PENamedTypeSymbol.UncommonProperties uncommonProperties = this.GetUncommonProperties();
				if (uncommonProperties == PENamedTypeSymbol.s_noUncommonProperties)
				{
					return false;
				}
				if (!uncommonProperties.lazyIsByRefLike.HasValue())
				{
					ThreeState threeState = ThreeState.False;
					if (this.TypeKind == TypeKind.Struct)
					{
						threeState = this.ContainingPEModule.Module.HasIsByRefLikeAttribute(this._handle).ToThreeState();
					}
					uncommonProperties.lazyIsByRefLike = threeState;
				}
				return uncommonProperties.lazyIsByRefLike.Value();
			}
		}

		// Token: 0x170025E4 RID: 9700
		// (get) Token: 0x06007F37 RID: 32567 RVA: 0x001C9E10 File Offset: 0x001C8010
		public override bool IsReadOnly
		{
			get
			{
				PENamedTypeSymbol.UncommonProperties uncommonProperties = this.GetUncommonProperties();
				if (uncommonProperties == PENamedTypeSymbol.s_noUncommonProperties)
				{
					return false;
				}
				if (!uncommonProperties.lazyIsReadOnly.HasValue())
				{
					ThreeState threeState = ThreeState.False;
					if (this.TypeKind == TypeKind.Struct)
					{
						threeState = this.ContainingPEModule.Module.HasIsReadOnlyAttribute(this._handle).ToThreeState();
					}
					uncommonProperties.lazyIsReadOnly = threeState;
				}
				return uncommonProperties.lazyIsReadOnly.Value();
			}
		}

		// Token: 0x170025E5 RID: 9701
		// (get) Token: 0x06007F38 RID: 32568 RVA: 0x001C9E7A File Offset: 0x001C807A
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				return (this._flags & 262144) > 0;
			}
		}

		// Token: 0x06007F39 RID: 32569 RVA: 0x001C9E8B File Offset: 0x001C808B
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x170025E6 RID: 9702
		// (get) Token: 0x06007F3A RID: 32570 RVA: 0x001C9E94 File Offset: 0x001C8094
		internal override NamedTypeSymbol ComImportCoClass
		{
			get
			{
				if (!this.IsInterfaceType())
				{
					return null;
				}
				PENamedTypeSymbol.UncommonProperties uncommonProperties = this.GetUncommonProperties();
				if (uncommonProperties == PENamedTypeSymbol.s_noUncommonProperties)
				{
					return null;
				}
				if (uncommonProperties.lazyComImportCoClassType == ErrorTypeSymbol.UnknownResultType)
				{
					TypeSymbol typeSymbol = this.ContainingPEModule.TryDecodeAttributeWithTypeArgument(this.Handle, AttributeDescription.CoClassAttribute);
					NamedTypeSymbol namedTypeSymbol = ((typeSymbol != null && (typeSymbol.TypeKind == TypeKind.Class || typeSymbol.IsErrorType())) ? ((NamedTypeSymbol)typeSymbol) : null);
					Interlocked.CompareExchange<NamedTypeSymbol>(ref uncommonProperties.lazyComImportCoClassType, namedTypeSymbol, ErrorTypeSymbol.UnknownResultType);
				}
				return uncommonProperties.lazyComImportCoClassType;
			}
		}

		// Token: 0x06007F3B RID: 32571 RVA: 0x001C9F1C File Offset: 0x001C811C
		internal override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			PENamedTypeSymbol.UncommonProperties uncommonProperties = this.GetUncommonProperties();
			if (uncommonProperties == PENamedTypeSymbol.s_noUncommonProperties)
			{
				return ImmutableArray<string>.Empty;
			}
			if (uncommonProperties.lazyConditionalAttributeSymbols.IsDefault)
			{
				ImmutableArray<string> conditionalAttributeValues = this.ContainingPEModule.Module.GetConditionalAttributeValues(this._handle);
				ImmutableInterlocked.InterlockedCompareExchange<string>(ref uncommonProperties.lazyConditionalAttributeSymbols, conditionalAttributeValues, default(ImmutableArray<string>));
			}
			return uncommonProperties.lazyConditionalAttributeSymbols;
		}

		// Token: 0x170025E7 RID: 9703
		// (get) Token: 0x06007F3C RID: 32572 RVA: 0x001C9F84 File Offset: 0x001C8184
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				PENamedTypeSymbol.UncommonProperties uncommonProperties = this.GetUncommonProperties();
				if (uncommonProperties == PENamedTypeSymbol.s_noUncommonProperties)
				{
					return null;
				}
				bool isRefLikeType = this.IsRefLikeType;
				ObsoleteAttributeHelpers.InitializeObsoleteDataFromMetadata(ref uncommonProperties.lazyObsoleteAttributeData, this._handle, this.ContainingPEModule, isRefLikeType);
				return uncommonProperties.lazyObsoleteAttributeData;
			}
		}

		// Token: 0x06007F3D RID: 32573 RVA: 0x001C9FCC File Offset: 0x001C81CC
		internal override AttributeUsageInfo GetAttributeUsageInfo()
		{
			PENamedTypeSymbol.UncommonProperties uncommonProperties = this.GetUncommonProperties();
			if (uncommonProperties != PENamedTypeSymbol.s_noUncommonProperties)
			{
				if (uncommonProperties.lazyAttributeUsageInfo.IsNull)
				{
					uncommonProperties.lazyAttributeUsageInfo = this.DecodeAttributeUsageInfo();
				}
				return uncommonProperties.lazyAttributeUsageInfo;
			}
			if (this.BaseTypeNoUseSiteDiagnostics == null)
			{
				return AttributeUsageInfo.Default;
			}
			return this.BaseTypeNoUseSiteDiagnostics.GetAttributeUsageInfo();
		}

		// Token: 0x06007F3E RID: 32574 RVA: 0x001CA024 File Offset: 0x001C8224
		private AttributeUsageInfo DecodeAttributeUsageInfo()
		{
			CustomAttributeHandle attributeUsageAttributeHandle = this.ContainingPEModule.Module.GetAttributeUsageAttributeHandle(this._handle);
			TypedConstant[] array;
			KeyValuePair<string, TypedConstant>[] array2;
			if (!attributeUsageAttributeHandle.IsNil && new MetadataDecoder(this.ContainingPEModule).GetCustomAttribute(attributeUsageAttributeHandle, out array, out array2))
			{
				AttributeUsageInfo attributeUsageInfo = AttributeData.DecodeAttributeUsageAttribute(array[0], array2.AsImmutableOrNull<KeyValuePair<string, TypedConstant>>());
				if (!attributeUsageInfo.HasValidAttributeTargets)
				{
					return AttributeUsageInfo.Default;
				}
				return attributeUsageInfo;
			}
			else
			{
				if (this.BaseTypeNoUseSiteDiagnostics == null)
				{
					return AttributeUsageInfo.Default;
				}
				return this.BaseTypeNoUseSiteDiagnostics.GetAttributeUsageInfo();
			}
		}

		// Token: 0x170025E8 RID: 9704
		// (get) Token: 0x06007F3F RID: 32575 RVA: 0x001CA0AA File Offset: 0x001C82AA
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170025E9 RID: 9705
		// (get) Token: 0x06007F40 RID: 32576 RVA: 0x001CA0AD File Offset: 0x001C82AD
		// (set) Token: 0x06007F41 RID: 32577 RVA: 0x001CA0B5 File Offset: 0x001C82B5
		public IEnumerable<object> fieldDefs { get; set; }

		// Token: 0x06007F42 RID: 32578 RVA: 0x001CA0C0 File Offset: 0x001C82C0
		private static int GetIndexOfFirstMember(ImmutableArray<Symbol> members, SymbolKind kind)
		{
			int length = members.Length;
			for (int i = 0; i < length; i++)
			{
				if (members[i].Kind == kind)
				{
					return i;
				}
			}
			return length;
		}

		// Token: 0x06007F43 RID: 32579 RVA: 0x001CA0F4 File Offset: 0x001C82F4
		private static IEnumerable<TSymbol> GetMembers<TSymbol>(ImmutableArray<Symbol> members, SymbolKind kind, int offset = -1) where TSymbol : Symbol
		{
			if (offset < 0)
			{
				offset = PENamedTypeSymbol.GetIndexOfFirstMember(members, kind);
			}
			int i = members.Length;
			int num;
			for (int j = offset; j < i; j = num + 1)
			{
				Symbol symbol = members[j];
				if (symbol.Kind != kind)
				{
					yield break;
				}
				yield return (TSymbol)((object)symbol);
				num = j;
			}
			yield break;
		}

		// Token: 0x04001DE7 RID: 7655
		private static readonly Dictionary<string, ImmutableArray<PENamedTypeSymbol>> s_emptyNestedTypes = new Dictionary<string, ImmutableArray<PENamedTypeSymbol>>(EmptyComparer.Instance);

		// Token: 0x04001DE8 RID: 7656
		private readonly NamespaceOrTypeSymbol _container;

		// Token: 0x04001DE9 RID: 7657
		private readonly TypeDefinitionHandle _handle;

		// Token: 0x04001DEA RID: 7658
		private readonly string _name;

		// Token: 0x04001DEB RID: 7659
		private readonly TypeAttributes _flags;

		// Token: 0x04001DEC RID: 7660
		private readonly SpecialType _corTypeId;

		// Token: 0x04001DED RID: 7661
		private ICollection<string> _lazyMemberNames;

		// Token: 0x04001DEE RID: 7662
		private ImmutableArray<Symbol> _lazyMembersInDeclarationOrder;

		// Token: 0x04001DEF RID: 7663
		private Dictionary<string, ImmutableArray<Symbol>> _lazyMembersByName;

		// Token: 0x04001DF0 RID: 7664
		private Dictionary<string, ImmutableArray<PENamedTypeSymbol>> _lazyNestedTypes;

		// Token: 0x04001DF1 RID: 7665
		private TypeKind _lazyKind;

		// Token: 0x04001DF2 RID: 7666
		private NullableContextKind _lazyNullableContextValue;

		// Token: 0x04001DF3 RID: 7667
		private NamedTypeSymbol _lazyBaseType = ErrorTypeSymbol.UnknownResultType;

		// Token: 0x04001DF4 RID: 7668
		private ImmutableArray<NamedTypeSymbol> _lazyInterfaces;

		// Token: 0x04001DF5 RID: 7669
		private NamedTypeSymbol _lazyDeclaredBaseType = ErrorTypeSymbol.UnknownResultType;

		// Token: 0x04001DF6 RID: 7670
		private ImmutableArray<NamedTypeSymbol> _lazyDeclaredInterfaces;

		// Token: 0x04001DF7 RID: 7671
		private Tuple<CultureInfo, string> _lazyDocComment;

		// Token: 0x04001DF8 RID: 7672
		private CachedUseSiteInfo<AssemblySymbol> _lazyCachedUseSiteInfo = CachedUseSiteInfo<AssemblySymbol>.Uninitialized;

		// Token: 0x04001DF9 RID: 7673
		private static readonly PENamedTypeSymbol.UncommonProperties s_noUncommonProperties = new PENamedTypeSymbol.UncommonProperties();

		// Token: 0x04001DFA RID: 7674
		private PENamedTypeSymbol.UncommonProperties _lazyUncommonProperties;

		// Token: 0x02000983 RID: 2435
		private class UncommonProperties
		{
			// Token: 0x04002908 RID: 10504
			internal ImmutableArray<PEFieldSymbol> lazyInstanceEnumFields;

			// Token: 0x04002909 RID: 10505
			internal NamedTypeSymbol lazyEnumUnderlyingType;

			// Token: 0x0400290A RID: 10506
			internal ImmutableArray<CSharpAttributeData> lazyCustomAttributes;

			// Token: 0x0400290B RID: 10507
			internal ImmutableArray<string> lazyConditionalAttributeSymbols;

			// Token: 0x0400290C RID: 10508
			internal ObsoleteAttributeData lazyObsoleteAttributeData = ObsoleteAttributeData.Uninitialized;

			// Token: 0x0400290D RID: 10509
			internal AttributeUsageInfo lazyAttributeUsageInfo = AttributeUsageInfo.Null;

			// Token: 0x0400290E RID: 10510
			internal ThreeState lazyContainsExtensionMethods;

			// Token: 0x0400290F RID: 10511
			internal ThreeState lazyIsByRefLike;

			// Token: 0x04002910 RID: 10512
			internal ThreeState lazyIsReadOnly;

			// Token: 0x04002911 RID: 10513
			internal string lazyDefaultMemberName;

			// Token: 0x04002912 RID: 10514
			internal NamedTypeSymbol lazyComImportCoClassType = ErrorTypeSymbol.UnknownResultType;

			// Token: 0x04002913 RID: 10515
			internal ThreeState lazyHasEmbeddedAttribute;
		}

		// Token: 0x02000984 RID: 2436
		private class DeclarationOrderTypeSymbolComparer : IComparer<Symbol>
		{
			// Token: 0x06009241 RID: 37441 RVA: 0x001FFCE0 File Offset: 0x001FDEE0
			private DeclarationOrderTypeSymbolComparer()
			{
			}

			// Token: 0x06009242 RID: 37442 RVA: 0x001FFCE8 File Offset: 0x001FDEE8
			public int Compare(Symbol x, Symbol y)
			{
				return HandleComparer.Default.Compare(((PENamedTypeSymbol)x).Handle, ((PENamedTypeSymbol)y).Handle);
			}

			// Token: 0x04002914 RID: 10516
			public static readonly PENamedTypeSymbol.DeclarationOrderTypeSymbolComparer Instance = new PENamedTypeSymbol.DeclarationOrderTypeSymbolComparer();
		}

		// Token: 0x02000985 RID: 2437
		private sealed class PENamedTypeSymbolNonGeneric : PENamedTypeSymbol
		{
			// Token: 0x06009244 RID: 37444 RVA: 0x001FFD20 File Offset: 0x001FDF20
			internal PENamedTypeSymbolNonGeneric(PEModuleSymbol moduleSymbol, NamespaceOrTypeSymbol container, TypeDefinitionHandle handle, string emittedNamespaceName, out bool mangleName)
				: base(moduleSymbol, container, handle, emittedNamespaceName, 0, out mangleName)
			{
			}

			// Token: 0x06009245 RID: 37445 RVA: 0x001FFD30 File Offset: 0x001FDF30
			protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x17002917 RID: 10519
			// (get) Token: 0x06009246 RID: 37446 RVA: 0x001FFD37 File Offset: 0x001FDF37
			public override int Arity
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17002918 RID: 10520
			// (get) Token: 0x06009247 RID: 37447 RVA: 0x001FFD3A File Offset: 0x001FDF3A
			internal override bool MangleName
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002919 RID: 10521
			// (get) Token: 0x06009248 RID: 37448 RVA: 0x001FFD40 File Offset: 0x001FDF40
			internal override int MetadataArity
			{
				get
				{
					PENamedTypeSymbol penamedTypeSymbol = this._container as PENamedTypeSymbol;
					if (penamedTypeSymbol != null)
					{
						return penamedTypeSymbol.MetadataArity;
					}
					return 0;
				}
			}

			// Token: 0x06009249 RID: 37449 RVA: 0x001FFD64 File Offset: 0x001FDF64
			internal override NamedTypeSymbol AsNativeInteger()
			{
				return this.ContainingAssembly.GetNativeIntegerType(this);
			}

			// Token: 0x1700291A RID: 10522
			// (get) Token: 0x0600924A RID: 37450 RVA: 0x001FFD72 File Offset: 0x001FDF72
			internal override NamedTypeSymbol NativeIntegerUnderlyingType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600924B RID: 37451 RVA: 0x001FFD78 File Offset: 0x001FDF78
			internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
			{
				NativeIntegerTypeSymbol nativeIntegerTypeSymbol = t2 as NativeIntegerTypeSymbol;
				if (nativeIntegerTypeSymbol == null)
				{
					return base.Equals(t2, comparison);
				}
				return nativeIntegerTypeSymbol.Equals(this, comparison);
			}
		}

		// Token: 0x02000986 RID: 2438
		private sealed class PENamedTypeSymbolGeneric : PENamedTypeSymbol
		{
			// Token: 0x0600924C RID: 37452 RVA: 0x001FFDA0 File Offset: 0x001FDFA0
			internal PENamedTypeSymbolGeneric(PEModuleSymbol moduleSymbol, NamespaceOrTypeSymbol container, TypeDefinitionHandle handle, string emittedNamespaceName, GenericParameterHandleCollection genericParameterHandles, ushort arity, out bool mangleName)
				: base(moduleSymbol, container, handle, emittedNamespaceName, arity, out mangleName)
			{
				this._arity = arity;
				this._genericParameterHandles = genericParameterHandles;
				this._mangleName = mangleName;
			}

			// Token: 0x0600924D RID: 37453 RVA: 0x001FFDCA File Offset: 0x001FDFCA
			protected sealed override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x1700291B RID: 10523
			// (get) Token: 0x0600924E RID: 37454 RVA: 0x001FFDD1 File Offset: 0x001FDFD1
			public override int Arity
			{
				get
				{
					return (int)this._arity;
				}
			}

			// Token: 0x1700291C RID: 10524
			// (get) Token: 0x0600924F RID: 37455 RVA: 0x001FFDD9 File Offset: 0x001FDFD9
			internal override bool MangleName
			{
				get
				{
					return this._mangleName;
				}
			}

			// Token: 0x1700291D RID: 10525
			// (get) Token: 0x06009250 RID: 37456 RVA: 0x001FFDE1 File Offset: 0x001FDFE1
			internal override int MetadataArity
			{
				get
				{
					return this._genericParameterHandles.Count;
				}
			}

			// Token: 0x1700291E RID: 10526
			// (get) Token: 0x06009251 RID: 37457 RVA: 0x001FFDEE File Offset: 0x001FDFEE
			internal override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotationsNoUseSiteDiagnostics
			{
				get
				{
					return base.GetTypeParametersAsTypeArguments();
				}
			}

			// Token: 0x1700291F RID: 10527
			// (get) Token: 0x06009252 RID: 37458 RVA: 0x001FFDF6 File Offset: 0x001FDFF6
			public override ImmutableArray<TypeParameterSymbol> TypeParameters
			{
				get
				{
					this.EnsureTypeParametersAreLoaded();
					return this._lazyTypeParameters;
				}
			}

			// Token: 0x06009253 RID: 37459 RVA: 0x001FFE04 File Offset: 0x001FE004
			internal sealed override NamedTypeSymbol AsNativeInteger()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x17002920 RID: 10528
			// (get) Token: 0x06009254 RID: 37460 RVA: 0x001FFE0B File Offset: 0x001FE00B
			internal sealed override NamedTypeSymbol NativeIntegerUnderlyingType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06009255 RID: 37461 RVA: 0x001FFE10 File Offset: 0x001FE010
			private void EnsureTypeParametersAreLoaded()
			{
				if (this._lazyTypeParameters.IsDefault)
				{
					PEModuleSymbol containingPEModule = base.ContainingPEModule;
					int num = this._genericParameterHandles.Count - (int)this._arity;
					TypeParameterSymbol[] array = new TypeParameterSymbol[(int)this._arity];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = new PETypeParameterSymbol(containingPEModule, this, (ushort)i, this._genericParameterHandles[num + i]);
					}
					ImmutableInterlocked.InterlockedInitialize<TypeParameterSymbol>(ref this._lazyTypeParameters, ImmutableArray.Create<TypeParameterSymbol>(array));
				}
			}

			// Token: 0x06009256 RID: 37462 RVA: 0x001FFE8C File Offset: 0x001FE08C
			protected override DiagnosticInfo GetUseSiteDiagnosticImpl()
			{
				DiagnosticInfo diagnosticInfo = null;
				if (!base.MergeUseSiteDiagnostics(ref diagnosticInfo, base.GetUseSiteDiagnosticImpl()) && !this.MatchesContainingTypeParameters())
				{
					diagnosticInfo = new CSDiagnosticInfo(ErrorCode.ERR_BogusType, new object[] { this });
				}
				return diagnosticInfo;
			}

			// Token: 0x06009257 RID: 37463 RVA: 0x001FFECC File Offset: 0x001FE0CC
			private bool MatchesContainingTypeParameters()
			{
				NamedTypeSymbol containingType = this.ContainingType;
				if (containingType == null)
				{
					return true;
				}
				ImmutableArray<TypeParameterSymbol> allTypeParameters = containingType.GetAllTypeParameters();
				int length = allTypeParameters.Length;
				if (length == 0)
				{
					return true;
				}
				ImmutableArray<TypeParameterSymbol> typeParameters = PENamedTypeSymbol.Create(base.ContainingPEModule, (PENamespaceSymbol)this.ContainingNamespace, this._handle, null).TypeParameters;
				TypeMap typeMap = new TypeMap(allTypeParameters, IndexedTypeParameterSymbol.Take(length), false);
				TypeMap typeMap2 = new TypeMap(typeParameters, IndexedTypeParameterSymbol.Take(typeParameters.Length), false);
				for (int i = 0; i < length; i++)
				{
					TypeParameterSymbol typeParameterSymbol = allTypeParameters[i];
					TypeParameterSymbol typeParameterSymbol2 = typeParameters[i];
					if (!MemberSignatureComparer.HaveSameConstraints(typeParameterSymbol, typeMap, typeParameterSymbol2, typeMap2))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x04002915 RID: 10517
			private readonly GenericParameterHandleCollection _genericParameterHandles;

			// Token: 0x04002916 RID: 10518
			private readonly ushort _arity;

			// Token: 0x04002917 RID: 10519
			private readonly bool _mangleName;

			// Token: 0x04002918 RID: 10520
			private ImmutableArray<TypeParameterSymbol> _lazyTypeParameters;
		}
	}
}
