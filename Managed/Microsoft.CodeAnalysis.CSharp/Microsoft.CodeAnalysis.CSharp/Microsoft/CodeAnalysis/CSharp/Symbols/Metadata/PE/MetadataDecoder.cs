using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005E3 RID: 1507
	internal class MetadataDecoder : MetadataDecoder<PEModuleSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol, Microsoft.CodeAnalysis.CSharp.Symbol>
	{
		// Token: 0x06007DBA RID: 32186 RVA: 0x001C4228 File Offset: 0x001C2428
		public MetadataDecoder(PEModuleSymbol moduleSymbol, PENamedTypeSymbol context)
			: this(moduleSymbol, context, null)
		{
		}

		// Token: 0x06007DBB RID: 32187 RVA: 0x001C4233 File Offset: 0x001C2433
		public MetadataDecoder(PEModuleSymbol moduleSymbol, PEMethodSymbol context)
			: this(moduleSymbol, (PENamedTypeSymbol)context.ContainingType, context)
		{
		}

		// Token: 0x06007DBC RID: 32188 RVA: 0x001C4248 File Offset: 0x001C2448
		public MetadataDecoder(PEModuleSymbol moduleSymbol)
			: this(moduleSymbol, null, null)
		{
		}

		// Token: 0x06007DBD RID: 32189 RVA: 0x001C4253 File Offset: 0x001C2453
		private MetadataDecoder(PEModuleSymbol moduleSymbol, PENamedTypeSymbol typeContextOpt, PEMethodSymbol methodContextOpt)
			: base(moduleSymbol.Module, (moduleSymbol.ContainingAssembly is PEAssemblySymbol) ? moduleSymbol.ContainingAssembly.Identity : null, SymbolFactory.Instance, moduleSymbol)
		{
			this._typeContextOpt = typeContextOpt;
			this._methodContextOpt = methodContextOpt;
		}

		// Token: 0x1700251F RID: 9503
		// (get) Token: 0x06007DBE RID: 32190 RVA: 0x001C4290 File Offset: 0x001C2490
		internal PEModuleSymbol ModuleSymbol
		{
			get
			{
				return this.moduleSymbol;
			}
		}

		// Token: 0x06007DBF RID: 32191 RVA: 0x001C4298 File Offset: 0x001C2498
		protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetGenericMethodTypeParamSymbol(int position)
		{
			if (this._methodContextOpt == null)
			{
				return new UnsupportedMetadataTypeSymbol(null);
			}
			ImmutableArray<TypeParameterSymbol> typeParameters = this._methodContextOpt.TypeParameters;
			if (typeParameters.Length <= position)
			{
				return new UnsupportedMetadataTypeSymbol(null);
			}
			return typeParameters[position];
		}

		// Token: 0x06007DC0 RID: 32192 RVA: 0x001C42DC File Offset: 0x001C24DC
		protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetGenericTypeParamSymbol(int position)
		{
			PENamedTypeSymbol penamedTypeSymbol = this._typeContextOpt;
			while (penamedTypeSymbol != null && penamedTypeSymbol.MetadataArity - penamedTypeSymbol.Arity > position)
			{
				penamedTypeSymbol = penamedTypeSymbol.ContainingSymbol as PENamedTypeSymbol;
			}
			if (penamedTypeSymbol == null || penamedTypeSymbol.MetadataArity <= position)
			{
				return new UnsupportedMetadataTypeSymbol(null);
			}
			position -= penamedTypeSymbol.MetadataArity - penamedTypeSymbol.Arity;
			return penamedTypeSymbol.TypeParameters[position];
		}

		// Token: 0x06007DC1 RID: 32193 RVA: 0x001C4344 File Offset: 0x001C2544
		protected override ConcurrentDictionary<TypeDefinitionHandle, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol> GetTypeHandleToTypeMap()
		{
			return this.moduleSymbol.TypeHandleToTypeMap;
		}

		// Token: 0x06007DC2 RID: 32194 RVA: 0x001C4351 File Offset: 0x001C2551
		protected override ConcurrentDictionary<TypeReferenceHandle, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol> GetTypeRefHandleToTypeMap()
		{
			return this.moduleSymbol.TypeRefHandleToTypeMap;
		}

		// Token: 0x06007DC3 RID: 32195 RVA: 0x001C435E File Offset: 0x001C255E
		protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol LookupNestedTypeDefSymbol(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol container, ref MetadataTypeName emittedName)
		{
			return container.LookupMetadataType(ref emittedName);
		}

		// Token: 0x06007DC4 RID: 32196 RVA: 0x001C4368 File Offset: 0x001C2568
		protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol LookupTopLevelTypeDefSymbol(int referencedAssemblyIndex, ref MetadataTypeName emittedName)
		{
			AssemblySymbol referencedAssemblySymbol = this.moduleSymbol.GetReferencedAssemblySymbol(referencedAssemblyIndex);
			if (referencedAssemblySymbol == null)
			{
				return new UnsupportedMetadataTypeSymbol(null);
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol;
			try
			{
				typeSymbol = referencedAssemblySymbol.LookupTopLevelMetadataType(ref emittedName, true);
			}
			catch (Exception ex) when (FatalError.ReportAndPropagate(ex))
			{
				throw ExceptionUtilities.Unreachable;
			}
			return typeSymbol;
		}

		// Token: 0x06007DC5 RID: 32197 RVA: 0x001C43C8 File Offset: 0x001C25C8
		protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol LookupTopLevelTypeDefSymbol(string moduleName, ref MetadataTypeName emittedName, out bool isNoPiaLocalType)
		{
			foreach (Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol moduleSymbol in this.moduleSymbol.ContainingAssembly.Modules)
			{
				if (string.Equals(moduleSymbol.Name, moduleName, 5))
				{
					if (moduleSymbol == this.moduleSymbol)
					{
						return this.moduleSymbol.LookupTopLevelMetadataType(ref emittedName, out isNoPiaLocalType);
					}
					isNoPiaLocalType = false;
					return moduleSymbol.LookupTopLevelMetadataType(ref emittedName);
				}
			}
			isNoPiaLocalType = false;
			return new MissingMetadataTypeSymbol.TopLevel(new MissingModuleSymbolWithName(this.moduleSymbol.ContainingAssembly, moduleName), ref emittedName, SpecialType.None, null);
		}

		// Token: 0x06007DC6 RID: 32198 RVA: 0x001C444E File Offset: 0x001C264E
		protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol LookupTopLevelTypeDefSymbol(ref MetadataTypeName emittedName, out bool isNoPiaLocalType)
		{
			return this.moduleSymbol.LookupTopLevelMetadataType(ref emittedName, out isNoPiaLocalType);
		}

		// Token: 0x06007DC7 RID: 32199 RVA: 0x001C4460 File Offset: 0x001C2660
		protected override int GetIndexOfReferencedAssembly(AssemblyIdentity identity)
		{
			ImmutableArray<AssemblyIdentity> referencedAssemblies = this.moduleSymbol.GetReferencedAssemblies();
			for (int i = 0; i < referencedAssemblies.Length; i++)
			{
				if (identity.Equals(referencedAssemblies[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06007DC8 RID: 32200 RVA: 0x001C44A0 File Offset: 0x001C26A0
		public static bool IsOrClosedOverATypeFromAssemblies(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol symbol, ImmutableArray<AssemblySymbol> assemblies)
		{
			SymbolKind kind = symbol.Kind;
			if (kind <= SymbolKind.NamedType)
			{
				switch (kind)
				{
				case SymbolKind.ArrayType:
					return MetadataDecoder.IsOrClosedOverATypeFromAssemblies(((ArrayTypeSymbol)symbol).ElementType, assemblies);
				case SymbolKind.Assembly:
					goto IL_00DD;
				case SymbolKind.DynamicType:
					return false;
				case SymbolKind.ErrorType:
					break;
				default:
					if (kind != SymbolKind.NamedType)
					{
						goto IL_00DD;
					}
					break;
				}
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)symbol;
				AssemblySymbol containingAssembly = symbol.OriginalDefinition.ContainingAssembly;
				if (containingAssembly != null)
				{
					for (int i = 0; i < assemblies.Length; i++)
					{
						if (containingAssembly == assemblies[i])
						{
							return true;
						}
					}
				}
				for (;;)
				{
					ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotationsNoUseSiteDiagnostics = namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics;
					int length = typeArgumentsWithAnnotationsNoUseSiteDiagnostics.Length;
					for (int i = 0; i < length; i++)
					{
						if (MetadataDecoder.IsOrClosedOverATypeFromAssemblies(typeArgumentsWithAnnotationsNoUseSiteDiagnostics[i].Type, assemblies))
						{
							return true;
						}
					}
					namedTypeSymbol = namedTypeSymbol.ContainingType;
					if (namedTypeSymbol == null)
					{
						return false;
					}
				}
				return true;
			}
			if (kind == SymbolKind.PointerType)
			{
				return MetadataDecoder.IsOrClosedOverATypeFromAssemblies(((PointerTypeSymbol)symbol).PointedAtType, assemblies);
			}
			if (kind == SymbolKind.TypeParameter)
			{
				return false;
			}
			IL_00DD:
			throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
		}

		// Token: 0x06007DC9 RID: 32201 RVA: 0x001C459C File Offset: 0x001C279C
		protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol SubstituteNoPiaLocalType(TypeDefinitionHandle typeDef, ref MetadataTypeName name, string interfaceGuid, string scope, string identifier)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol2;
			try
			{
				bool flag = this.Module.IsInterfaceOrThrow(typeDef);
				Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol = null;
				if (!flag)
				{
					EntityHandle baseTypeOfTypeOrThrow = this.Module.GetBaseTypeOfTypeOrThrow(typeDef);
					if (!baseTypeOfTypeOrThrow.IsNil)
					{
						typeSymbol = base.GetTypeOfToken(baseTypeOfTypeOrThrow);
					}
				}
				typeSymbol2 = MetadataDecoder.SubstituteNoPiaLocalType(ref name, flag, typeSymbol, interfaceGuid, scope, identifier, this.moduleSymbol.ContainingAssembly);
			}
			catch (BadImageFormatException ex)
			{
				typeSymbol2 = base.GetUnsupportedMetadataTypeSymbol(ex);
			}
			return this.GetTypeHandleToTypeMap().GetOrAdd(typeDef, typeSymbol2);
		}

		// Token: 0x06007DCA RID: 32202 RVA: 0x001C4620 File Offset: 0x001C2820
		internal static NamedTypeSymbol SubstituteNoPiaLocalType(ref MetadataTypeName name, bool isInterface, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol baseType, string interfaceGuid, string scope, string identifier, AssemblySymbol referringAssembly)
		{
			NamedTypeSymbol namedTypeSymbol = null;
			Guid guid = default(Guid);
			bool flag = false;
			Guid guid2 = default(Guid);
			bool flag2 = false;
			if (isInterface && interfaceGuid != null)
			{
				flag = Guid.TryParse(interfaceGuid, ref guid);
				if (flag)
				{
					scope = null;
					identifier = null;
				}
			}
			if (scope != null)
			{
				flag2 = Guid.TryParse(scope, ref guid2);
			}
			foreach (AssemblySymbol assemblySymbol in referringAssembly.GetNoPiaResolutionAssemblies())
			{
				if (assemblySymbol != referringAssembly)
				{
					NamedTypeSymbol namedTypeSymbol2 = assemblySymbol.LookupTopLevelMetadataType(ref name, false);
					if (namedTypeSymbol2.Kind != SymbolKind.ErrorType && namedTypeSymbol2.ContainingAssembly == assemblySymbol && namedTypeSymbol2.DeclaredAccessibility == Accessibility.Public)
					{
						bool flag3 = false;
						Guid guid3 = default(Guid);
						TypeKind typeKind = namedTypeSymbol2.TypeKind;
						switch (typeKind)
						{
						case TypeKind.Delegate:
						case TypeKind.Enum:
							break;
						case TypeKind.Dynamic:
						case TypeKind.Error:
							continue;
						case TypeKind.Interface:
						{
							if (!isInterface)
							{
								continue;
							}
							string text;
							if (namedTypeSymbol2.GetGuidString(out text) && text != null)
							{
								flag3 = Guid.TryParse(text, ref guid3);
								goto IL_0127;
							}
							goto IL_0127;
						}
						default:
							if (typeKind != TypeKind.Struct)
							{
								continue;
							}
							break;
						}
						if (isInterface)
						{
							continue;
						}
						NamedTypeSymbol baseTypeNoUseSiteDiagnostics = namedTypeSymbol2.BaseTypeNoUseSiteDiagnostics;
						SpecialType specialType = ((baseTypeNoUseSiteDiagnostics != null) ? baseTypeNoUseSiteDiagnostics.SpecialType : SpecialType.None);
						if (specialType == SpecialType.None || specialType != ((baseType != null) ? baseType.SpecialType : SpecialType.None))
						{
							continue;
						}
						IL_0127:
						if (flag || flag3)
						{
							if (!flag || !flag3)
							{
								continue;
							}
							if (guid3 != guid)
							{
								continue;
							}
						}
						else
						{
							if (!flag2 || identifier == null || !identifier.Equals(name.FullName))
							{
								continue;
							}
							flag3 = false;
							string text;
							if (assemblySymbol.GetGuidString(out text) && text != null)
							{
								flag3 = Guid.TryParse(text, ref guid3);
							}
							if (!flag3 || guid2 != guid3)
							{
								continue;
							}
						}
						if (namedTypeSymbol != null)
						{
							namedTypeSymbol = new NoPiaAmbiguousCanonicalTypeSymbol(referringAssembly, namedTypeSymbol, namedTypeSymbol2, null);
							break;
						}
						namedTypeSymbol = namedTypeSymbol2;
					}
				}
			}
			if (namedTypeSymbol == null)
			{
				namedTypeSymbol = new NoPiaMissingCanonicalTypeSymbol(referringAssembly, name.FullName, interfaceGuid, scope, identifier, null);
			}
			return namedTypeSymbol;
		}

		// Token: 0x06007DCB RID: 32203 RVA: 0x001C47E8 File Offset: 0x001C29E8
		protected override Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol FindMethodSymbolInType(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol, MethodDefinitionHandle targetMethodDef)
		{
			foreach (Microsoft.CodeAnalysis.CSharp.Symbol symbol in typeSymbol.GetMembersUnordered())
			{
				PEMethodSymbol pemethodSymbol = symbol as PEMethodSymbol;
				if (pemethodSymbol != null && pemethodSymbol.Handle == targetMethodDef)
				{
					return pemethodSymbol;
				}
			}
			return null;
		}

		// Token: 0x06007DCC RID: 32204 RVA: 0x001C4830 File Offset: 0x001C2A30
		protected override Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol FindFieldSymbolInType(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol, FieldDefinitionHandle fieldDef)
		{
			foreach (Microsoft.CodeAnalysis.CSharp.Symbol symbol in typeSymbol.GetMembersUnordered())
			{
				PEFieldSymbol pefieldSymbol = symbol as PEFieldSymbol;
				if (pefieldSymbol != null && pefieldSymbol.Handle == fieldDef)
				{
					return pefieldSymbol;
				}
			}
			return null;
		}

		// Token: 0x06007DCD RID: 32205 RVA: 0x001C4878 File Offset: 0x001C2A78
		internal override Microsoft.CodeAnalysis.CSharp.Symbol GetSymbolForMemberRef(MemberReferenceHandle memberRef, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol scope = null, bool methodsOnly = false)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol = base.GetMemberRefTypeSymbol(memberRef);
			if (typeSymbol == null)
			{
				return null;
			}
			if (scope != null)
			{
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				if (!Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol.Equals(scope, typeSymbol, TypeCompareKind.ConsiderEverything) && !(typeSymbol.IsInterfaceType() ? (scope.AllInterfacesNoUseSiteDiagnostics.IndexOf((NamedTypeSymbol)typeSymbol, 0, SymbolEqualityComparer.CLRSignature) != -1) : scope.IsDerivedFrom(typeSymbol, TypeCompareKind.CLRSignatureCompareOptions, ref discarded)))
				{
					return null;
				}
			}
			if (!typeSymbol.IsTupleType)
			{
				typeSymbol = TupleTypeDecoder.DecodeTupleTypesIfApplicable(typeSymbol, default(ImmutableArray<string>));
			}
			return new MemberRefMetadataDecoder(this.moduleSymbol, typeSymbol).FindMember(typeSymbol, memberRef, methodsOnly);
		}

		// Token: 0x06007DCE RID: 32206 RVA: 0x001C490C File Offset: 0x001C2B0C
		protected override void EnqueueTypeSymbolInterfacesAndBaseTypes(Queue<TypeDefinitionHandle> typeDefsToSearch, Queue<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol> typeSymbolsToSearch, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol)
		{
			foreach (NamedTypeSymbol namedTypeSymbol in typeSymbol.InterfacesNoUseSiteDiagnostics(null))
			{
				this.EnqueueTypeSymbol(typeDefsToSearch, typeSymbolsToSearch, namedTypeSymbol);
			}
			this.EnqueueTypeSymbol(typeDefsToSearch, typeSymbolsToSearch, typeSymbol.BaseTypeNoUseSiteDiagnostics);
		}

		// Token: 0x06007DCF RID: 32207 RVA: 0x001C4954 File Offset: 0x001C2B54
		protected override void EnqueueTypeSymbol(Queue<TypeDefinitionHandle> typeDefsToSearch, Queue<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol> typeSymbolsToSearch, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol)
		{
			if (typeSymbol != null)
			{
				PENamedTypeSymbol penamedTypeSymbol = typeSymbol as PENamedTypeSymbol;
				if (penamedTypeSymbol != null && penamedTypeSymbol.ContainingPEModule == this.moduleSymbol)
				{
					typeDefsToSearch.Enqueue(penamedTypeSymbol.Handle);
					return;
				}
				typeSymbolsToSearch.Enqueue(typeSymbol);
			}
		}

		// Token: 0x06007DD0 RID: 32208 RVA: 0x001C4990 File Offset: 0x001C2B90
		protected override MethodDefinitionHandle GetMethodHandle(Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol method)
		{
			PEMethodSymbol pemethodSymbol = method as PEMethodSymbol;
			if (pemethodSymbol != null && pemethodSymbol.ContainingModule == this.moduleSymbol)
			{
				return pemethodSymbol.Handle;
			}
			return default(MethodDefinitionHandle);
		}

		// Token: 0x04001DA2 RID: 7586
		private readonly PENamedTypeSymbol _typeContextOpt;

		// Token: 0x04001DA3 RID: 7587
		private readonly PEMethodSymbol _methodContextOpt;
	}
}
