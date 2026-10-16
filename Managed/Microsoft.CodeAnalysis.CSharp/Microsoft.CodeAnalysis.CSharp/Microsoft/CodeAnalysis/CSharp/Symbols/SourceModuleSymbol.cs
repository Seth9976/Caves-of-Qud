using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000545 RID: 1349
	internal sealed class SourceModuleSymbol : NonMissingModuleSymbol, IAttributeTargetSymbol
	{
		// Token: 0x060071C7 RID: 29127 RVA: 0x001A6D9D File Offset: 0x001A4F9D
		internal SourceModuleSymbol(SourceAssemblySymbol assemblySymbol, DeclarationTable declarations, string moduleName)
		{
			this._assemblySymbol = assemblySymbol;
			this._sources = declarations;
			this._name = moduleName;
		}

		// Token: 0x060071C8 RID: 29128 RVA: 0x001A6DBA File Offset: 0x001A4FBA
		internal void RecordPresenceOfBadAttributes()
		{
			this._hasBadAttributes = true;
		}

		// Token: 0x17001EB3 RID: 7859
		// (get) Token: 0x060071C9 RID: 29129 RVA: 0x001A6DC3 File Offset: 0x001A4FC3
		internal bool HasBadAttributes
		{
			get
			{
				return this._hasBadAttributes;
			}
		}

		// Token: 0x17001EB4 RID: 7860
		// (get) Token: 0x060071CA RID: 29130 RVA: 0x001A6DCB File Offset: 0x001A4FCB
		internal override int Ordinal
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001EB5 RID: 7861
		// (get) Token: 0x060071CB RID: 29131 RVA: 0x001A6DD0 File Offset: 0x001A4FD0
		internal override Machine Machine
		{
			get
			{
				switch (this.DeclaringCompilation.Options.Platform)
				{
				case Platform.X64:
					return Machine.Amd64;
				case Platform.Itanium:
					return Machine.IA64;
				case Platform.Arm:
					return Machine.ArmThumb2;
				case Platform.Arm64:
					return Machine.Arm64;
				}
				return Machine.I386;
			}
		}

		// Token: 0x17001EB6 RID: 7862
		// (get) Token: 0x060071CC RID: 29132 RVA: 0x001A6E29 File Offset: 0x001A5029
		internal override bool Bit32Required
		{
			get
			{
				return this.DeclaringCompilation.Options.Platform == Platform.X86;
			}
		}

		// Token: 0x17001EB7 RID: 7863
		// (get) Token: 0x060071CD RID: 29133 RVA: 0x001A6E40 File Offset: 0x001A5040
		internal bool AnyReferencedAssembliesAreLinked
		{
			get
			{
				return this.GetAssembliesToEmbedTypesFrom().Length > 0;
			}
		}

		// Token: 0x060071CE RID: 29134 RVA: 0x001A6E5E File Offset: 0x001A505E
		internal bool MightContainNoPiaLocalTypes()
		{
			return this.AnyReferencedAssembliesAreLinked || this.ContainsExplicitDefinitionOfNoPiaLocalTypes;
		}

		// Token: 0x060071CF RID: 29135 RVA: 0x001A6E70 File Offset: 0x001A5070
		internal ImmutableArray<AssemblySymbol> GetAssembliesToEmbedTypesFrom()
		{
			if (this._lazyAssembliesToEmbedTypesFrom.IsDefault)
			{
				ArrayBuilder<AssemblySymbol> instance = ArrayBuilder<AssemblySymbol>.GetInstance();
				foreach (AssemblySymbol assemblySymbol in this.GetReferencedAssemblySymbols())
				{
					if (assemblySymbol.IsLinked)
					{
						instance.Add(assemblySymbol);
					}
				}
				ImmutableInterlocked.InterlockedCompareExchange<AssemblySymbol>(ref this._lazyAssembliesToEmbedTypesFrom, instance.ToImmutableAndFree(), default(ImmutableArray<AssemblySymbol>));
			}
			return this._lazyAssembliesToEmbedTypesFrom;
		}

		// Token: 0x17001EB8 RID: 7864
		// (get) Token: 0x060071D0 RID: 29136 RVA: 0x001A6EE2 File Offset: 0x001A50E2
		internal bool ContainsExplicitDefinitionOfNoPiaLocalTypes
		{
			get
			{
				if (this._lazyContainsExplicitDefinitionOfNoPiaLocalTypes == ThreeState.Unknown)
				{
					this._lazyContainsExplicitDefinitionOfNoPiaLocalTypes = SourceModuleSymbol.NamespaceContainsExplicitDefinitionOfNoPiaLocalTypes(this.GlobalNamespace).ToThreeState();
				}
				return this._lazyContainsExplicitDefinitionOfNoPiaLocalTypes == ThreeState.True;
			}
		}

		// Token: 0x060071D1 RID: 29137 RVA: 0x001A6F0C File Offset: 0x001A510C
		private static bool NamespaceContainsExplicitDefinitionOfNoPiaLocalTypes(NamespaceSymbol ns)
		{
			foreach (Symbol symbol in ns.GetMembersUnordered())
			{
				SymbolKind kind = symbol.Kind;
				if (kind != SymbolKind.NamedType)
				{
					if (kind == SymbolKind.Namespace && SourceModuleSymbol.NamespaceContainsExplicitDefinitionOfNoPiaLocalTypes((NamespaceSymbol)symbol))
					{
						return true;
					}
				}
				else if (((NamedTypeSymbol)symbol).IsExplicitDefinitionOfNoPiaLocalType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17001EB9 RID: 7865
		// (get) Token: 0x060071D2 RID: 29138 RVA: 0x001A6F6C File Offset: 0x001A516C
		public override NamespaceSymbol GlobalNamespace
		{
			get
			{
				if (this._globalNamespace == null)
				{
					BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
					SourceNamespaceSymbol sourceNamespaceSymbol = new SourceNamespaceSymbol(this, this, this.DeclaringCompilation.MergedRootDeclaration, instance);
					if (Interlocked.CompareExchange<NamespaceSymbol>(ref this._globalNamespace, sourceNamespaceSymbol, null) == null)
					{
						this.AddDeclarationDiagnostics(instance);
					}
					instance.Free();
				}
				return this._globalNamespace;
			}
		}

		// Token: 0x17001EBA RID: 7866
		// (get) Token: 0x060071D3 RID: 29139 RVA: 0x001A6FC3 File Offset: 0x001A51C3
		internal sealed override bool RequiresCompletion
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060071D4 RID: 29140 RVA: 0x001A6FC6 File Offset: 0x001A51C6
		internal sealed override bool HasComplete(CompletionPart part)
		{
			return this._state.HasComplete(part);
		}

		// Token: 0x060071D5 RID: 29141 RVA: 0x001A6FD4 File Offset: 0x001A51D4
		internal override void ForceComplete(SourceLocation locationOpt, CancellationToken cancellationToken)
		{
			for (;;)
			{
				cancellationToken.ThrowIfCancellationRequested();
				CompletionPart nextIncompletePart = this._state.NextIncompletePart;
				if (nextIncompletePart <= CompletionPart.Attributes)
				{
					if (nextIncompletePart == CompletionPart.None)
					{
						return;
					}
					if (nextIncompletePart != CompletionPart.Attributes)
					{
						goto IL_00DC;
					}
					this.GetAttributes();
				}
				else if (nextIncompletePart != CompletionPart.StartBaseType)
				{
					if (nextIncompletePart != CompletionPart.FinishBaseType)
					{
						if (nextIncompletePart != CompletionPart.MembersCompleted)
						{
							goto IL_00DC;
						}
						this.GlobalNamespace.ForceComplete(locationOpt, cancellationToken);
						if (!this.GlobalNamespace.HasComplete(CompletionPart.MembersCompleted))
						{
							break;
						}
						this._state.NotePartComplete(CompletionPart.MembersCompleted);
					}
					else
					{
						this._state.SpinWaitComplete(CompletionPart.FinishBaseType, cancellationToken);
					}
				}
				else
				{
					BindingDiagnosticBag bindingDiagnosticBag = null;
					if (this.AnyReferencedAssembliesAreLinked)
					{
						bindingDiagnosticBag = BindingDiagnosticBag.GetInstance();
						this.ValidateLinkedAssemblies(bindingDiagnosticBag, cancellationToken);
					}
					if (this._state.NotePartComplete(CompletionPart.StartBaseType))
					{
						if (bindingDiagnosticBag != null)
						{
							this._assemblySymbol.AddDeclarationDiagnostics(bindingDiagnosticBag);
						}
						this._state.NotePartComplete(CompletionPart.FinishBaseType);
					}
					if (bindingDiagnosticBag != null)
					{
						bindingDiagnosticBag.Free();
					}
				}
				IL_00E9:
				this._state.SpinWaitComplete(nextIncompletePart, cancellationToken);
				continue;
				IL_00DC:
				this._state.NotePartComplete(nextIncompletePart);
				goto IL_00E9;
			}
		}

		// Token: 0x060071D6 RID: 29142 RVA: 0x001A70DC File Offset: 0x001A52DC
		private void ValidateLinkedAssemblies(BindingDiagnosticBag diagnostics, CancellationToken cancellationToken)
		{
			foreach (AssemblySymbol assemblySymbol in this.GetReferencedAssemblySymbols())
			{
				cancellationToken.ThrowIfCancellationRequested();
				if (!assemblySymbol.IsMissing && assemblySymbol.IsLinked)
				{
					bool flag = false;
					bool flag2 = false;
					foreach (CSharpAttributeData csharpAttributeData in assemblySymbol.GetAttributes())
					{
						if (csharpAttributeData.IsTargetAttribute(assemblySymbol, AttributeDescription.GuidAttribute))
						{
							string text;
							if (csharpAttributeData.TryGetGuidAttributeValue(out text))
							{
								flag = true;
							}
						}
						else if (csharpAttributeData.IsTargetAttribute(assemblySymbol, AttributeDescription.ImportedFromTypeLibAttribute))
						{
							if (csharpAttributeData.CommonConstructorArguments.Length == 1)
							{
								flag2 = true;
							}
						}
						else if (csharpAttributeData.IsTargetAttribute(assemblySymbol, AttributeDescription.PrimaryInteropAssemblyAttribute) && csharpAttributeData.CommonConstructorArguments.Length == 2)
						{
							flag2 = true;
						}
						if (flag && flag2)
						{
							break;
						}
					}
					if (!flag)
					{
						diagnostics.Add(ErrorCode.ERR_NoPIAAssemblyMissingAttribute, NoLocation.Singleton, new object[]
						{
							assemblySymbol,
							AttributeDescription.GuidAttribute.FullName
						});
					}
					if (!flag2)
					{
						diagnostics.Add(ErrorCode.ERR_NoPIAAssemblyMissingAttributes, NoLocation.Singleton, new object[]
						{
							assemblySymbol,
							AttributeDescription.ImportedFromTypeLibAttribute.FullName,
							AttributeDescription.PrimaryInteropAssemblyAttribute.FullName
						});
					}
				}
			}
		}

		// Token: 0x17001EBB RID: 7867
		// (get) Token: 0x060071D7 RID: 29143 RVA: 0x001A723C File Offset: 0x001A543C
		public override ImmutableArray<Location> Locations
		{
			get
			{
				if (this._locations.IsDefault)
				{
					ImmutableInterlocked.InterlockedInitialize<Location>(ref this._locations, this.DeclaringCompilation.MergedRootDeclaration.Declarations.SelectAsArray<SingleNamespaceDeclaration, Location>((SingleNamespaceDeclaration d) => d.Location));
				}
				return this._locations;
			}
		}

		// Token: 0x17001EBC RID: 7868
		// (get) Token: 0x060071D8 RID: 29144 RVA: 0x001A729C File Offset: 0x001A549C
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001EBD RID: 7869
		// (get) Token: 0x060071D9 RID: 29145 RVA: 0x001A72A4 File Offset: 0x001A54A4
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._assemblySymbol;
			}
		}

		// Token: 0x17001EBE RID: 7870
		// (get) Token: 0x060071DA RID: 29146 RVA: 0x001A72AC File Offset: 0x001A54AC
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._assemblySymbol;
			}
		}

		// Token: 0x17001EBF RID: 7871
		// (get) Token: 0x060071DB RID: 29147 RVA: 0x001A72B4 File Offset: 0x001A54B4
		internal SourceAssemblySymbol ContainingSourceAssembly
		{
			get
			{
				return this._assemblySymbol;
			}
		}

		// Token: 0x17001EC0 RID: 7872
		// (get) Token: 0x060071DC RID: 29148 RVA: 0x001A72BC File Offset: 0x001A54BC
		internal override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return this._assemblySymbol.DeclaringCompilation;
			}
		}

		// Token: 0x17001EC1 RID: 7873
		// (get) Token: 0x060071DD RID: 29149 RVA: 0x001A72C9 File Offset: 0x001A54C9
		internal override ICollection<string> TypeNames
		{
			get
			{
				return this._sources.TypeNames;
			}
		}

		// Token: 0x17001EC2 RID: 7874
		// (get) Token: 0x060071DE RID: 29150 RVA: 0x001A72D6 File Offset: 0x001A54D6
		internal override ICollection<string> NamespaceNames
		{
			get
			{
				return this._sources.NamespaceNames;
			}
		}

		// Token: 0x17001EC3 RID: 7875
		// (get) Token: 0x060071DF RID: 29151 RVA: 0x001A72E3 File Offset: 0x001A54E3
		IAttributeTargetSymbol IAttributeTargetSymbol.AttributesOwner
		{
			get
			{
				return this._assemblySymbol;
			}
		}

		// Token: 0x17001EC4 RID: 7876
		// (get) Token: 0x060071E0 RID: 29152 RVA: 0x001A72EB File Offset: 0x001A54EB
		AttributeLocation IAttributeTargetSymbol.DefaultAttributeLocation
		{
			get
			{
				return AttributeLocation.Module;
			}
		}

		// Token: 0x17001EC5 RID: 7877
		// (get) Token: 0x060071E1 RID: 29153 RVA: 0x001A72EE File Offset: 0x001A54EE
		AttributeLocation IAttributeTargetSymbol.AllowedAttributeLocations
		{
			get
			{
				if (!this.ContainingAssembly.IsInteractive)
				{
					return AttributeLocation.Assembly | AttributeLocation.Module;
				}
				return AttributeLocation.None;
			}
		}

		// Token: 0x060071E2 RID: 29154 RVA: 0x001A7300 File Offset: 0x001A5500
		private CustomAttributesBag<CSharpAttributeData> GetAttributesBag()
		{
			if (this._lazyCustomAttributesBag == null || !this._lazyCustomAttributesBag.IsSealed)
			{
				ImmutableArray<SyntaxList<AttributeListSyntax>> attributeDeclarations = ((SourceAssemblySymbol)this.ContainingAssembly).GetAttributeDeclarations();
				if (base.LoadAndValidateAttributes(OneOrMany.Create<SyntaxList<AttributeListSyntax>>(attributeDeclarations), ref this._lazyCustomAttributesBag, AttributeLocation.None, false, null, null))
				{
					this._state.NotePartComplete(CompletionPart.Attributes);
				}
			}
			return this._lazyCustomAttributesBag;
		}

		// Token: 0x060071E3 RID: 29155 RVA: 0x001A735E File Offset: 0x001A555E
		public sealed override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.GetAttributesBag().Attributes;
		}

		// Token: 0x060071E4 RID: 29156 RVA: 0x001A736C File Offset: 0x001A556C
		private ModuleWellKnownAttributeData GetDecodedWellKnownAttributeData()
		{
			CustomAttributesBag<CSharpAttributeData> customAttributesBag = this._lazyCustomAttributesBag;
			if (customAttributesBag == null || !customAttributesBag.IsDecodedWellKnownAttributeDataComputed)
			{
				customAttributesBag = this.GetAttributesBag();
			}
			return (ModuleWellKnownAttributeData)customAttributesBag.DecodedWellKnownAttributeData;
		}

		// Token: 0x060071E5 RID: 29157 RVA: 0x001A73A0 File Offset: 0x001A55A0
		internal override void DecodeWellKnownAttribute(ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			CSharpAttributeData attribute = arguments.Attribute;
			if (!attribute.IsTargetAttribute(this, AttributeDescription.DefaultCharSetAttribute))
			{
				if (!base.ReportExplicitUseOfReservedAttributes(in arguments, Symbol.ReservedAttributes.NullableContextAttribute | Symbol.ReservedAttributes.NullablePublicOnlyAttribute) && attribute.IsTargetAttribute(this, AttributeDescription.SkipLocalsInitAttribute))
				{
					CSharpAttributeData.DecodeSkipLocalsInitAttribute<ModuleWellKnownAttributeData>(this.DeclaringCompilation, ref arguments);
				}
				return;
			}
			CharSet constructorArgument = attribute.GetConstructorArgument<CharSet>(0, SpecialType.System_Enum);
			if (!CommonModuleWellKnownAttributeData.IsValidCharSet(constructorArgument))
			{
				CSharpSyntaxNode attributeArgumentSyntax = attribute.GetAttributeArgumentSyntax(0, arguments.AttributeSyntaxOpt);
				((BindingDiagnosticBag)arguments.Diagnostics).Add(ErrorCode.ERR_InvalidAttributeArgument, attributeArgumentSyntax.Location, new object[] { arguments.AttributeSyntaxOpt.GetErrorDisplayName() });
				return;
			}
			arguments.GetOrCreateData<ModuleWellKnownAttributeData>().DefaultCharacterSet = constructorArgument;
		}

		// Token: 0x060071E6 RID: 29158 RVA: 0x001A7448 File Offset: 0x001A5648
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			CSharpCompilation declaringCompilation = this._assemblySymbol.DeclaringCompilation;
			if (declaringCompilation.Options.AllowUnsafe && !(declaringCompilation.GetWellKnownType(WellKnownType.System_Security_UnverifiableCodeAttribute) is MissingMetadataTypeSymbol))
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Security_UnverifiableCodeAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
			}
			if (moduleBuilder.ShouldEmitNullablePublicOnlyAttribute())
			{
				ImmutableArray<TypedConstant> immutableArray = ImmutableArray.Create<TypedConstant>(new TypedConstant(declaringCompilation.GetSpecialType(SpecialType.System_Boolean), TypedConstantKind.Primitive, this._assemblySymbol.InternalsAreVisible));
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNullablePublicOnlyAttribute(immutableArray));
			}
		}

		// Token: 0x17001EC6 RID: 7878
		// (get) Token: 0x060071E7 RID: 29159 RVA: 0x001A74E4 File Offset: 0x001A56E4
		internal override bool HasAssemblyCompilationRelaxationsAttribute
		{
			get
			{
				CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> sourceDecodedWellKnownAttributeData = ((SourceAssemblySymbol)this.ContainingAssembly).GetSourceDecodedWellKnownAttributeData();
				return sourceDecodedWellKnownAttributeData != null && sourceDecodedWellKnownAttributeData.HasCompilationRelaxationsAttribute;
			}
		}

		// Token: 0x17001EC7 RID: 7879
		// (get) Token: 0x060071E8 RID: 29160 RVA: 0x001A7510 File Offset: 0x001A5710
		internal override bool HasAssemblyRuntimeCompatibilityAttribute
		{
			get
			{
				CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> sourceDecodedWellKnownAttributeData = ((SourceAssemblySymbol)this.ContainingAssembly).GetSourceDecodedWellKnownAttributeData();
				return sourceDecodedWellKnownAttributeData != null && sourceDecodedWellKnownAttributeData.HasRuntimeCompatibilityAttribute;
			}
		}

		// Token: 0x17001EC8 RID: 7880
		// (get) Token: 0x060071E9 RID: 29161 RVA: 0x001A753C File Offset: 0x001A573C
		internal override CharSet? DefaultMarshallingCharSet
		{
			get
			{
				ModuleWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				if (decodedWellKnownAttributeData == null || !decodedWellKnownAttributeData.HasDefaultCharSetAttribute)
				{
					return default(CharSet?);
				}
				return new CharSet?(decodedWellKnownAttributeData.DefaultCharacterSet);
			}
		}

		// Token: 0x17001EC9 RID: 7881
		// (get) Token: 0x060071EA RID: 29162 RVA: 0x001A7570 File Offset: 0x001A5770
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				ModuleWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData == null || !decodedWellKnownAttributeData.HasSkipLocalsInitAttribute;
			}
		}

		// Token: 0x060071EB RID: 29163 RVA: 0x001A7586 File Offset: 0x001A5786
		public override ModuleMetadata GetMetadata()
		{
			return null;
		}

		// Token: 0x04001BB5 RID: 7093
		private readonly SourceAssemblySymbol _assemblySymbol;

		// Token: 0x04001BB6 RID: 7094
		private ImmutableArray<AssemblySymbol> _lazyAssembliesToEmbedTypesFrom;

		// Token: 0x04001BB7 RID: 7095
		private ThreeState _lazyContainsExplicitDefinitionOfNoPiaLocalTypes;

		// Token: 0x04001BB8 RID: 7096
		private readonly DeclarationTable _sources;

		// Token: 0x04001BB9 RID: 7097
		private SymbolCompletionState _state;

		// Token: 0x04001BBA RID: 7098
		private CustomAttributesBag<CSharpAttributeData> _lazyCustomAttributesBag;

		// Token: 0x04001BBB RID: 7099
		private ImmutableArray<Location> _locations;

		// Token: 0x04001BBC RID: 7100
		private NamespaceSymbol _globalNamespace;

		// Token: 0x04001BBD RID: 7101
		private bool _hasBadAttributes;

		// Token: 0x04001BBE RID: 7102
		private readonly string _name;
	}
}
