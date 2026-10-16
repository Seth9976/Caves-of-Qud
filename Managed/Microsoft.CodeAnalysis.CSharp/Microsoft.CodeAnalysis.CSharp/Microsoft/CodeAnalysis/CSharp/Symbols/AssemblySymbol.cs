using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004C3 RID: 1219
	internal abstract class AssemblySymbol : Symbol, IAssemblySymbolInternal, ISymbolInternal
	{
		// Token: 0x17001A1B RID: 6683
		// (get) Token: 0x060067F4 RID: 26612 RVA: 0x001873C1 File Offset: 0x001855C1
		internal AssemblySymbol CorLibrary
		{
			get
			{
				return this._corLibrary;
			}
		}

		// Token: 0x060067F5 RID: 26613 RVA: 0x001873C9 File Offset: 0x001855C9
		internal void SetCorLibrary(AssemblySymbol corLibrary)
		{
			this._corLibrary = corLibrary;
		}

		// Token: 0x17001A1C RID: 6684
		// (get) Token: 0x060067F6 RID: 26614 RVA: 0x001873D2 File Offset: 0x001855D2
		public override string Name
		{
			get
			{
				return this.Identity.Name;
			}
		}

		// Token: 0x17001A1D RID: 6685
		// (get) Token: 0x060067F7 RID: 26615
		public abstract AssemblyIdentity Identity { get; }

		// Token: 0x17001A1E RID: 6686
		// (get) Token: 0x060067F8 RID: 26616 RVA: 0x001873DF File Offset: 0x001855DF
		AssemblyIdentity IAssemblySymbolInternal.Identity
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x17001A1F RID: 6687
		// (get) Token: 0x060067F9 RID: 26617 RVA: 0x001873E7 File Offset: 0x001855E7
		IAssemblySymbolInternal IAssemblySymbolInternal.CorLibrary
		{
			get
			{
				return this.CorLibrary;
			}
		}

		// Token: 0x17001A20 RID: 6688
		// (get) Token: 0x060067FA RID: 26618
		public abstract Version AssemblyVersionPattern { get; }

		// Token: 0x17001A21 RID: 6689
		// (get) Token: 0x060067FB RID: 26619 RVA: 0x001873F0 File Offset: 0x001855F0
		internal Machine Machine
		{
			get
			{
				return this.Modules[0].Machine;
			}
		}

		// Token: 0x17001A22 RID: 6690
		// (get) Token: 0x060067FC RID: 26620 RVA: 0x00187414 File Offset: 0x00185614
		internal bool Bit32Required
		{
			get
			{
				return this.Modules[0].Bit32Required;
			}
		}

		// Token: 0x17001A23 RID: 6691
		// (get) Token: 0x060067FD RID: 26621
		public abstract NamespaceSymbol GlobalNamespace { get; }

		// Token: 0x060067FE RID: 26622 RVA: 0x00187438 File Offset: 0x00185638
		internal NamespaceSymbol GetAssemblyNamespace(NamespaceSymbol namespaceSymbol)
		{
			if (namespaceSymbol.IsGlobalNamespace)
			{
				return this.GlobalNamespace;
			}
			NamespaceSymbol containingNamespace = namespaceSymbol.ContainingNamespace;
			if (containingNamespace == null)
			{
				return this.GlobalNamespace;
			}
			if (namespaceSymbol.NamespaceKind == NamespaceKind.Assembly && namespaceSymbol.ContainingAssembly == this)
			{
				return namespaceSymbol;
			}
			NamespaceSymbol assemblyNamespace = this.GetAssemblyNamespace(containingNamespace);
			if (assemblyNamespace == containingNamespace)
			{
				return namespaceSymbol;
			}
			if (assemblyNamespace == null)
			{
				return null;
			}
			return assemblyNamespace.GetNestedNamespace(namespaceSymbol.Name);
		}

		// Token: 0x17001A24 RID: 6692
		// (get) Token: 0x060067FF RID: 26623
		public abstract ImmutableArray<ModuleSymbol> Modules { get; }

		// Token: 0x06006800 RID: 26624 RVA: 0x0018749D File Offset: 0x0018569D
		internal override TResult Accept<TArgument, TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument argument)
		{
			return visitor.VisitAssembly(this, argument);
		}

		// Token: 0x06006801 RID: 26625 RVA: 0x001874A7 File Offset: 0x001856A7
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitAssembly(this);
		}

		// Token: 0x06006802 RID: 26626 RVA: 0x001874B0 File Offset: 0x001856B0
		public override TResult Accept<TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitAssembly(this);
		}

		// Token: 0x17001A25 RID: 6693
		// (get) Token: 0x06006803 RID: 26627 RVA: 0x001874B9 File Offset: 0x001856B9
		public sealed override SymbolKind Kind
		{
			get
			{
				return SymbolKind.Assembly;
			}
		}

		// Token: 0x17001A26 RID: 6694
		// (get) Token: 0x06006804 RID: 26628 RVA: 0x001874BC File Offset: 0x001856BC
		public sealed override AssemblySymbol ContainingAssembly
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006805 RID: 26629 RVA: 0x001874BF File Offset: 0x001856BF
		internal AssemblySymbol()
		{
		}

		// Token: 0x17001A27 RID: 6695
		// (get) Token: 0x06006806 RID: 26630
		internal abstract bool IsMissing { get; }

		// Token: 0x17001A28 RID: 6696
		// (get) Token: 0x06006807 RID: 26631 RVA: 0x001874C7 File Offset: 0x001856C7
		public sealed override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.NotApplicable;
			}
		}

		// Token: 0x17001A29 RID: 6697
		// (get) Token: 0x06006808 RID: 26632 RVA: 0x001874CA File Offset: 0x001856CA
		public sealed override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A2A RID: 6698
		// (get) Token: 0x06006809 RID: 26633 RVA: 0x001874CD File Offset: 0x001856CD
		public sealed override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A2B RID: 6699
		// (get) Token: 0x0600680A RID: 26634 RVA: 0x001874D0 File Offset: 0x001856D0
		public sealed override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A2C RID: 6700
		// (get) Token: 0x0600680B RID: 26635 RVA: 0x001874D3 File Offset: 0x001856D3
		public sealed override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A2D RID: 6701
		// (get) Token: 0x0600680C RID: 26636 RVA: 0x001874D6 File Offset: 0x001856D6
		public sealed override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A2E RID: 6702
		// (get) Token: 0x0600680D RID: 26637 RVA: 0x001874D9 File Offset: 0x001856D9
		public sealed override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A2F RID: 6703
		// (get) Token: 0x0600680E RID: 26638 RVA: 0x001874DC File Offset: 0x001856DC
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001A30 RID: 6704
		// (get) Token: 0x0600680F RID: 26639 RVA: 0x001874DF File Offset: 0x001856DF
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17001A31 RID: 6705
		// (get) Token: 0x06006810 RID: 26640 RVA: 0x001874E6 File Offset: 0x001856E6
		public virtual bool IsInteractive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A32 RID: 6706
		// (get) Token: 0x06006811 RID: 26641 RVA: 0x001874E9 File Offset: 0x001856E9
		public sealed override Symbol ContainingSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006812 RID: 26642 RVA: 0x001874EC File Offset: 0x001856EC
		internal NamedTypeSymbol LookupTopLevelMetadataType(ref MetadataTypeName emittedName, bool digThroughForwardedTypes)
		{
			return this.LookupTopLevelMetadataTypeWithCycleDetection(ref emittedName, null, digThroughForwardedTypes);
		}

		// Token: 0x06006813 RID: 26643
		internal abstract NamedTypeSymbol LookupTopLevelMetadataTypeWithCycleDetection(ref MetadataTypeName emittedName, ConsList<AssemblySymbol> visitedAssemblies, bool digThroughForwardedTypes);

		// Token: 0x06006814 RID: 26644 RVA: 0x001874F8 File Offset: 0x001856F8
		public NamedTypeSymbol ResolveForwardedType(string fullyQualifiedMetadataName)
		{
			if (fullyQualifiedMetadataName == null)
			{
				throw new ArgumentNullException("fullyQualifiedMetadataName");
			}
			MetadataTypeName metadataTypeName = MetadataTypeName.FromFullName(fullyQualifiedMetadataName, false, -1);
			return this.TryLookupForwardedMetadataType(ref metadataTypeName);
		}

		// Token: 0x06006815 RID: 26645 RVA: 0x00187524 File Offset: 0x00185724
		internal NamedTypeSymbol TryLookupForwardedMetadataType(ref MetadataTypeName emittedName)
		{
			return this.TryLookupForwardedMetadataTypeWithCycleDetection(ref emittedName, null);
		}

		// Token: 0x06006816 RID: 26646 RVA: 0x0018752E File Offset: 0x0018572E
		internal virtual NamedTypeSymbol TryLookupForwardedMetadataTypeWithCycleDetection(ref MetadataTypeName emittedName, ConsList<AssemblySymbol> visitedAssemblies)
		{
			return null;
		}

		// Token: 0x06006817 RID: 26647 RVA: 0x00187534 File Offset: 0x00185734
		internal ErrorTypeSymbol CreateCycleInTypeForwarderErrorTypeSymbol(ref MetadataTypeName emittedName)
		{
			DiagnosticInfo diagnosticInfo = new CSDiagnosticInfo(ErrorCode.ERR_CycleInTypeForwarder, new object[] { emittedName.FullName, this.Name });
			return new MissingMetadataTypeSymbol.TopLevel(this.Modules[0], ref emittedName, diagnosticInfo);
		}

		// Token: 0x06006818 RID: 26648 RVA: 0x0018757C File Offset: 0x0018577C
		internal ErrorTypeSymbol CreateMultipleForwardingErrorTypeSymbol(ref MetadataTypeName emittedName, ModuleSymbol forwardingModule, AssemblySymbol destination1, AssemblySymbol destination2)
		{
			CSDiagnosticInfo csdiagnosticInfo = new CSDiagnosticInfo(ErrorCode.ERR_TypeForwardedToMultipleAssemblies, new object[] { forwardingModule, this, emittedName.FullName, destination1, destination2 });
			return new MissingMetadataTypeSymbol.TopLevel(forwardingModule, ref emittedName, csdiagnosticInfo);
		}

		// Token: 0x06006819 RID: 26649
		internal abstract IEnumerable<NamedTypeSymbol> GetAllTopLevelForwardedTypes();

		// Token: 0x0600681A RID: 26650
		internal abstract NamedTypeSymbol GetDeclaredSpecialType(SpecialType type);

		// Token: 0x0600681B RID: 26651 RVA: 0x001875BC File Offset: 0x001857BC
		internal virtual void RegisterDeclaredSpecialType(NamedTypeSymbol corType)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17001A33 RID: 6707
		// (get) Token: 0x0600681C RID: 26652 RVA: 0x001875C3 File Offset: 0x001857C3
		internal virtual bool KeepLookingForDeclaredSpecialTypes
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x0600681D RID: 26653 RVA: 0x001875CA File Offset: 0x001857CA
		internal virtual NamedTypeSymbol GetNativeIntegerType(NamedTypeSymbol underlyingType)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17001A34 RID: 6708
		// (get) Token: 0x0600681E RID: 26654 RVA: 0x001875D1 File Offset: 0x001857D1
		internal bool RuntimeSupportsDefaultInterfaceImplementation
		{
			get
			{
				return this.RuntimeSupportsFeature(SpecialMember.System_Runtime_CompilerServices_RuntimeFeature__DefaultImplementationsOfInterfaces);
			}
		}

		// Token: 0x0600681F RID: 26655 RVA: 0x001875DC File Offset: 0x001857DC
		private bool RuntimeSupportsFeature(SpecialMember feature)
		{
			NamedTypeSymbol specialType = this.GetSpecialType(SpecialType.System_Runtime_CompilerServices_RuntimeFeature);
			return specialType != null && specialType.TypeKind == TypeKind.Class && specialType.IsStatic && this.GetSpecialTypeMember(feature) != null;
		}

		// Token: 0x17001A35 RID: 6709
		// (get) Token: 0x06006820 RID: 26656 RVA: 0x00187612 File Offset: 0x00185812
		internal bool RuntimeSupportsUnmanagedSignatureCallingConvention
		{
			get
			{
				return this.RuntimeSupportsFeature(SpecialMember.System_Runtime_CompilerServices_RuntimeFeature__UnmanagedSignatureCallingConvention);
			}
		}

		// Token: 0x17001A36 RID: 6710
		// (get) Token: 0x06006821 RID: 26657 RVA: 0x0018761C File Offset: 0x0018581C
		internal bool RuntimeSupportsCovariantReturnsOfClasses
		{
			get
			{
				if (this.RuntimeSupportsFeature(SpecialMember.System_Runtime_CompilerServices_RuntimeFeature__CovariantReturnsOfClasses))
				{
					NamedTypeSymbol specialType = this.GetSpecialType(SpecialType.System_Runtime_CompilerServices_PreserveBaseOverridesAttribute);
					return specialType != null && specialType.TypeKind == TypeKind.Class;
				}
				return false;
			}
		}

		// Token: 0x06006822 RID: 26658
		internal abstract ImmutableArray<AssemblySymbol> GetNoPiaResolutionAssemblies();

		// Token: 0x06006823 RID: 26659
		internal abstract void SetNoPiaResolutionAssemblies(ImmutableArray<AssemblySymbol> assemblies);

		// Token: 0x06006824 RID: 26660
		internal abstract ImmutableArray<AssemblySymbol> GetLinkedReferencedAssemblies();

		// Token: 0x06006825 RID: 26661
		internal abstract void SetLinkedReferencedAssemblies(ImmutableArray<AssemblySymbol> assemblies);

		// Token: 0x06006826 RID: 26662
		internal abstract IEnumerable<ImmutableArray<byte>> GetInternalsVisibleToPublicKeys(string simpleName);

		// Token: 0x06006827 RID: 26663
		internal abstract bool AreInternalsVisibleToThisAssembly(AssemblySymbol other);

		// Token: 0x17001A37 RID: 6711
		// (get) Token: 0x06006828 RID: 26664
		internal abstract bool IsLinked { get; }

		// Token: 0x06006829 RID: 26665 RVA: 0x0018764C File Offset: 0x0018584C
		internal virtual bool GetGuidString(out string guidString)
		{
			return base.GetGuidStringDefaultImplementation(out guidString);
		}

		// Token: 0x17001A38 RID: 6712
		// (get) Token: 0x0600682A RID: 26666
		public abstract ICollection<string> TypeNames { get; }

		// Token: 0x17001A39 RID: 6713
		// (get) Token: 0x0600682B RID: 26667
		public abstract ICollection<string> NamespaceNames { get; }

		// Token: 0x17001A3A RID: 6714
		// (get) Token: 0x0600682C RID: 26668
		public abstract bool MightContainExtensionMethods { get; }

		// Token: 0x0600682D RID: 26669 RVA: 0x00187655 File Offset: 0x00185855
		internal NamedTypeSymbol GetSpecialType(SpecialType type)
		{
			return this.CorLibrary.GetDeclaredSpecialType(type);
		}

		// Token: 0x17001A3B RID: 6715
		// (get) Token: 0x0600682E RID: 26670 RVA: 0x00187663 File Offset: 0x00185863
		internal static TypeSymbol DynamicType
		{
			get
			{
				return DynamicTypeSymbol.Instance;
			}
		}

		// Token: 0x17001A3C RID: 6716
		// (get) Token: 0x0600682F RID: 26671 RVA: 0x0018766A File Offset: 0x0018586A
		internal NamedTypeSymbol ObjectType
		{
			get
			{
				return this.GetSpecialType(SpecialType.System_Object);
			}
		}

		// Token: 0x06006830 RID: 26672 RVA: 0x00187673 File Offset: 0x00185873
		internal NamedTypeSymbol GetPrimitiveType(PrimitiveTypeCode type)
		{
			return this.GetSpecialType(SpecialTypes.GetTypeFromMetadataName(type));
		}

		// Token: 0x06006831 RID: 26673 RVA: 0x00187684 File Offset: 0x00185884
		public NamedTypeSymbol GetTypeByMetadataName(string fullyQualifiedMetadataName)
		{
			if (fullyQualifiedMetadataName == null)
			{
				throw new ArgumentNullException("fullyQualifiedMetadataName");
			}
			ValueTuple<AssemblySymbol, AssemblySymbol> valueTuple;
			return this.GetTypeByMetadataName(fullyQualifiedMetadataName, false, false, out valueTuple, false, null, false);
		}

		// Token: 0x06006832 RID: 26674 RVA: 0x001876B0 File Offset: 0x001858B0
		internal NamedTypeSymbol GetTypeByMetadataName(string metadataName, bool includeReferences, bool isWellKnownType, out ValueTuple<AssemblySymbol, AssemblySymbol> conflicts, bool useCLSCompliantNameArityEncoding = false, DiagnosticBag warnings = null, bool ignoreCorLibraryDuplicatedTypes = false)
		{
			NamedTypeSymbol namedTypeSymbol;
			if (metadataName.IndexOf('+') >= 0)
			{
				string[] array = metadataName.Split(AssemblySymbol.s_nestedTypeNameSeparators);
				MetadataTypeName metadataTypeName = MetadataTypeName.FromFullName(array[0], useCLSCompliantNameArityEncoding, -1);
				namedTypeSymbol = this.GetTopLevelTypeByMetadataName(ref metadataTypeName, null, includeReferences, isWellKnownType, out conflicts, warnings, ignoreCorLibraryDuplicatedTypes);
				int num = 1;
				while (namedTypeSymbol != null && !namedTypeSymbol.IsErrorType())
				{
					if (num >= array.Length)
					{
						break;
					}
					metadataTypeName = MetadataTypeName.FromTypeName(array[num], false, -1);
					NamedTypeSymbol namedTypeSymbol2 = namedTypeSymbol.LookupMetadataType(ref metadataTypeName);
					namedTypeSymbol = ((!isWellKnownType || this.IsValidWellKnownType(namedTypeSymbol2)) ? namedTypeSymbol2 : null);
					num++;
				}
			}
			else
			{
				MetadataTypeName metadataTypeName = MetadataTypeName.FromFullName(metadataName, useCLSCompliantNameArityEncoding, -1);
				namedTypeSymbol = this.GetTopLevelTypeByMetadataName(ref metadataTypeName, null, includeReferences, isWellKnownType, out conflicts, warnings, ignoreCorLibraryDuplicatedTypes);
			}
			if (namedTypeSymbol != null && !namedTypeSymbol.IsErrorType())
			{
				return namedTypeSymbol;
			}
			return null;
		}

		// Token: 0x06006833 RID: 26675 RVA: 0x00187760 File Offset: 0x00185960
		internal TypeSymbol GetTypeByReflectionType(Type type, bool includeReferences)
		{
			TypeInfo typeInfo = IntrospectionExtensions.GetTypeInfo(type);
			if (typeInfo.IsArray)
			{
				TypeSymbol typeByReflectionType = this.GetTypeByReflectionType(typeInfo.GetElementType(), includeReferences);
				if (typeByReflectionType == null)
				{
					return null;
				}
				int arrayRank = typeInfo.GetArrayRank();
				return ArrayTypeSymbol.CreateCSharpArray(this, TypeWithAnnotations.Create(typeByReflectionType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), arrayRank);
			}
			else if (typeInfo.IsPointer)
			{
				TypeSymbol typeByReflectionType2 = this.GetTypeByReflectionType(typeInfo.GetElementType(), includeReferences);
				if (typeByReflectionType2 == null)
				{
					return null;
				}
				return new PointerTypeSymbol(TypeWithAnnotations.Create(typeByReflectionType2, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
			}
			else if (typeInfo.DeclaringType != null)
			{
				Type[] genericTypeArguments = typeInfo.GenericTypeArguments;
				int num = 0;
				TypeInfo typeInfo2 = (typeInfo.IsGenericType ? IntrospectionExtensions.GetTypeInfo(typeInfo.GetGenericTypeDefinition()) : typeInfo);
				ArrayBuilder<TypeInfo> instance = ArrayBuilder<TypeInfo>.GetInstance();
				for (;;)
				{
					instance.Add(typeInfo2);
					if (typeInfo2.DeclaringType == null)
					{
						break;
					}
					typeInfo2 = IntrospectionExtensions.GetTypeInfo(typeInfo2.DeclaringType);
				}
				int num2 = instance.Count - 1;
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)this.GetTypeByReflectionType(instance[num2].AsType(), includeReferences);
				if (namedTypeSymbol == null)
				{
					return null;
				}
				while (--num2 >= 0)
				{
					int num3 = instance[num2].GenericTypeParameters.Length - instance[num2 + 1].GenericTypeParameters.Length;
					MetadataTypeName metadataTypeName = MetadataTypeName.FromTypeName(instance[num2].Name, false, num3);
					namedTypeSymbol = namedTypeSymbol.LookupMetadataType(ref metadataTypeName);
					if (namedTypeSymbol == null || namedTypeSymbol.IsErrorType())
					{
						return null;
					}
					namedTypeSymbol = this.ApplyGenericArguments(namedTypeSymbol, genericTypeArguments, ref num, includeReferences);
					if (namedTypeSymbol == null)
					{
						return null;
					}
				}
				instance.Free();
				return namedTypeSymbol;
			}
			else
			{
				AssemblyIdentity assemblyIdentity = AssemblyIdentity.FromAssemblyDefinition(typeInfo.Assembly);
				MetadataTypeName metadataTypeName2 = MetadataTypeName.FromNamespaceAndTypeName(typeInfo.Namespace ?? string.Empty, typeInfo.Name, false, typeInfo.GenericTypeArguments.Length);
				ValueTuple<AssemblySymbol, AssemblySymbol> valueTuple;
				NamedTypeSymbol topLevelTypeByMetadataName = this.GetTopLevelTypeByMetadataName(ref metadataTypeName2, assemblyIdentity, includeReferences, false, out valueTuple, null, false);
				if (topLevelTypeByMetadataName == null || topLevelTypeByMetadataName.IsErrorType())
				{
					return null;
				}
				int num4 = 0;
				Type[] genericTypeArguments2 = typeInfo.GenericTypeArguments;
				return this.ApplyGenericArguments(topLevelTypeByMetadataName, genericTypeArguments2, ref num4, includeReferences);
			}
		}

		// Token: 0x06006834 RID: 26676 RVA: 0x00187964 File Offset: 0x00185B64
		private NamedTypeSymbol ApplyGenericArguments(NamedTypeSymbol symbol, Type[] typeArguments, ref int currentTypeArgument, bool includeReferences)
		{
			if (typeArguments.Length - currentTypeArgument == 0)
			{
				return symbol;
			}
			int length = symbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics.Length;
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(length);
			for (int i = 0; i < length; i++)
			{
				int num = currentTypeArgument;
				currentTypeArgument = num + 1;
				TypeSymbol typeByReflectionType = this.GetTypeByReflectionType(typeArguments[num], includeReferences);
				if (typeByReflectionType == null)
				{
					return null;
				}
				instance.Add(TypeWithAnnotations.Create(typeByReflectionType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
			}
			return symbol.ConstructIfGeneric(instance.ToImmutableAndFree());
		}

		// Token: 0x06006835 RID: 26677 RVA: 0x001879E0 File Offset: 0x00185BE0
		internal NamedTypeSymbol GetTopLevelTypeByMetadataName(ref MetadataTypeName metadataName, AssemblyIdentity assemblyOpt, bool includeReferences, bool isWellKnownType, out ValueTuple<AssemblySymbol, AssemblySymbol> conflicts, DiagnosticBag warnings = null, bool ignoreCorLibraryDuplicatedTypes = false)
		{
			conflicts = default(ValueTuple<AssemblySymbol, AssemblySymbol>);
			NamedTypeSymbol namedTypeSymbol = AssemblySymbol.GetTopLevelTypeByMetadataName(this, ref metadataName, assemblyOpt);
			if (isWellKnownType && !this.IsValidWellKnownType(namedTypeSymbol))
			{
				namedTypeSymbol = null;
			}
			if (namedTypeSymbol != null || !includeReferences)
			{
				return namedTypeSymbol;
			}
			bool flag = isWellKnownType && warnings != null;
			bool flag2 = false;
			if (this.CorLibrary != this && !this.CorLibrary.IsMissing && !flag && !ignoreCorLibraryDuplicatedTypes)
			{
				NamedTypeSymbol topLevelTypeByMetadataName = AssemblySymbol.GetTopLevelTypeByMetadataName(this.CorLibrary, ref metadataName, assemblyOpt);
				flag2 = true;
				if (this.<GetTopLevelTypeByMetadataName>g__isValidCandidate|101_0(topLevelTypeByMetadataName, isWellKnownType))
				{
					return topLevelTypeByMetadataName;
				}
			}
			ArrayBuilder<AssemblySymbol> instance = ArrayBuilder<AssemblySymbol>.GetInstance();
			if (assemblyOpt != null)
			{
				instance.AddRange(this.DeclaringCompilation.GetBoundReferenceManager().ReferencedAssemblies);
			}
			else
			{
				this.DeclaringCompilation.GetUnaliasedReferencedAssemblies(instance);
			}
			foreach (AssemblySymbol assemblySymbol in instance)
			{
				if (!flag2 || assemblySymbol != this.CorLibrary)
				{
					NamedTypeSymbol topLevelTypeByMetadataName2 = AssemblySymbol.GetTopLevelTypeByMetadataName(assemblySymbol, ref metadataName, assemblyOpt);
					if (this.<GetTopLevelTypeByMetadataName>g__isValidCandidate|101_0(topLevelTypeByMetadataName2, isWellKnownType))
					{
						if (namedTypeSymbol != null)
						{
							if (ignoreCorLibraryDuplicatedTypes)
							{
								if (this.IsInCorLib(topLevelTypeByMetadataName2))
								{
									continue;
								}
								if (this.IsInCorLib(namedTypeSymbol))
								{
									namedTypeSymbol = topLevelTypeByMetadataName2;
									continue;
								}
							}
							if (warnings == null)
							{
								conflicts = new ValueTuple<AssemblySymbol, AssemblySymbol>(namedTypeSymbol.ContainingAssembly, topLevelTypeByMetadataName2.ContainingAssembly);
								namedTypeSymbol = null;
								break;
							}
							warnings.Add(ErrorCode.WRN_MultiplePredefTypes, NoLocation.Singleton, new object[] { namedTypeSymbol, namedTypeSymbol.ContainingAssembly });
							break;
						}
						else
						{
							namedTypeSymbol = topLevelTypeByMetadataName2;
						}
					}
				}
			}
			instance.Free();
			return namedTypeSymbol;
		}

		// Token: 0x06006836 RID: 26678 RVA: 0x00187B4E File Offset: 0x00185D4E
		private bool IsInCorLib(NamedTypeSymbol type)
		{
			return type.ContainingAssembly == this.CorLibrary;
		}

		// Token: 0x06006837 RID: 26679 RVA: 0x00187B5E File Offset: 0x00185D5E
		private bool IsValidWellKnownType(NamedTypeSymbol result)
		{
			return result != null && result.TypeKind != TypeKind.Error && (result.DeclaredAccessibility == Accessibility.Public || Symbol.IsSymbolAccessible(result, this));
		}

		// Token: 0x06006838 RID: 26680 RVA: 0x00187B80 File Offset: 0x00185D80
		private static NamedTypeSymbol GetTopLevelTypeByMetadataName(AssemblySymbol assembly, ref MetadataTypeName metadataName, AssemblyIdentity assemblyOpt)
		{
			NamedTypeSymbol namedTypeSymbol = assembly.LookupTopLevelMetadataType(ref metadataName, false);
			if (!AssemblySymbol.IsAcceptableMatchForGetTypeByMetadataName(namedTypeSymbol))
			{
				return null;
			}
			if (assemblyOpt != null && !assemblyOpt.Equals(assembly.Identity))
			{
				return null;
			}
			return namedTypeSymbol;
		}

		// Token: 0x06006839 RID: 26681 RVA: 0x00187BBA File Offset: 0x00185DBA
		private static bool IsAcceptableMatchForGetTypeByMetadataName(NamedTypeSymbol candidate)
		{
			return candidate.Kind != SymbolKind.ErrorType || !(candidate is MissingMetadataTypeSymbol);
		}

		// Token: 0x0600683A RID: 26682 RVA: 0x00187BD3 File Offset: 0x00185DD3
		internal virtual Symbol GetDeclaredSpecialTypeMember(SpecialMember member)
		{
			return null;
		}

		// Token: 0x0600683B RID: 26683 RVA: 0x00187BD6 File Offset: 0x00185DD6
		internal virtual Symbol GetSpecialTypeMember(SpecialMember member)
		{
			return this.CorLibrary.GetDeclaredSpecialTypeMember(member);
		}

		// Token: 0x17001A3D RID: 6717
		// (get) Token: 0x0600683C RID: 26684
		internal abstract ImmutableArray<byte> PublicKey { get; }

		// Token: 0x0600683D RID: 26685
		public abstract AssemblyMetadata GetMetadata();

		// Token: 0x0600683E RID: 26686 RVA: 0x00187BE4 File Offset: 0x00185DE4
		protected override ISymbol CreateISymbol()
		{
			return new NonSourceAssemblySymbol(this);
		}

		// Token: 0x06006840 RID: 26688 RVA: 0x00187BFE File Offset: 0x00185DFE
		[CompilerGenerated]
		private bool <GetTopLevelTypeByMetadataName>g__isValidCandidate|101_0(NamedTypeSymbol candidate, bool isWellKnownType)
		{
			return candidate != null && (!isWellKnownType || this.IsValidWellKnownType(candidate)) && !candidate.IsHiddenByCodeAnalysisEmbeddedAttribute();
		}

		// Token: 0x040019CD RID: 6605
		private AssemblySymbol _corLibrary;

		// Token: 0x040019CE RID: 6606
		private static readonly char[] s_nestedTypeNameSeparators = new char[] { '+' };
	}
}
