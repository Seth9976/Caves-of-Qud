using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Emit.NoPia;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Emit.NoPia
{
	// Token: 0x020004A2 RID: 1186
	internal sealed class EmbeddedTypesManager : EmbeddedTypesManager<PEModuleBuilder, ModuleCompilationState, EmbeddedTypesManager, SyntaxNode, CSharpAttributeData, Symbol, AssemblySymbol, NamedTypeSymbol, FieldSymbol, MethodSymbol, EventSymbol, PropertySymbol, ParameterSymbol, TypeParameterSymbol, EmbeddedType, EmbeddedField, EmbeddedMethod, EmbeddedEvent, EmbeddedProperty, EmbeddedParameter, EmbeddedTypeParameter>
	{
		// Token: 0x06006229 RID: 25129 RVA: 0x00178EBC File Offset: 0x001770BC
		public EmbeddedTypesManager(PEModuleBuilder moduleBeingBuilt)
			: base(moduleBeingBuilt)
		{
			this._lazyWellKnownTypeMethods = new MethodSymbol[418];
			for (int i = 0; i < this._lazyWellKnownTypeMethods.Length; i++)
			{
				this._lazyWellKnownTypeMethods[i] = ErrorMethodSymbol.UnknownMethod;
			}
		}

		// Token: 0x0600622A RID: 25130 RVA: 0x00178F2C File Offset: 0x0017712C
		public NamedTypeSymbol GetSystemStringType(SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			if (this._lazySystemStringType == ErrorTypeSymbol.UnknownResultType)
			{
				NamedTypeSymbol namedTypeSymbol = this.ModuleBeingBuilt.Compilation.GetSpecialType(SpecialType.System_String);
				UseSiteInfo<AssemblySymbol> useSiteInfo = namedTypeSymbol.GetUseSiteInfo();
				if (namedTypeSymbol.IsErrorType())
				{
					namedTypeSymbol = null;
				}
				if (TypeSymbol.Equals(Interlocked.CompareExchange<NamedTypeSymbol>(ref this._lazySystemStringType, namedTypeSymbol, ErrorTypeSymbol.UnknownResultType), ErrorTypeSymbol.UnknownResultType, TypeCompareKind.ConsiderEverything) && useSiteInfo.DiagnosticInfo != null)
				{
					Symbol.ReportUseSiteDiagnostic(useSiteInfo.DiagnosticInfo, diagnostics, (syntaxNodeOpt != null) ? syntaxNodeOpt.Location : NoLocation.Singleton);
				}
			}
			return this._lazySystemStringType;
		}

		// Token: 0x0600622B RID: 25131 RVA: 0x00178FB3 File Offset: 0x001771B3
		public MethodSymbol GetWellKnownMethod(WellKnownMember method, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			return this.LazyGetWellKnownTypeMethod(ref this._lazyWellKnownTypeMethods[(int)method], method, syntaxNodeOpt, diagnostics);
		}

		// Token: 0x0600622C RID: 25132 RVA: 0x00178FCC File Offset: 0x001771CC
		private MethodSymbol LazyGetWellKnownTypeMethod(ref MethodSymbol lazyMethod, WellKnownMember member, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			if (lazyMethod == ErrorMethodSymbol.UnknownMethod)
			{
				UseSiteInfo<AssemblySymbol> useSiteInfo;
				MethodSymbol methodSymbol = (MethodSymbol)Binder.GetWellKnownTypeMember(this.ModuleBeingBuilt.Compilation, member, out useSiteInfo, false);
				DiagnosticInfo diagnosticInfo = useSiteInfo.DiagnosticInfo;
				if (diagnosticInfo != null && diagnosticInfo.Severity == DiagnosticSeverity.Error)
				{
					methodSymbol = null;
				}
				if (Interlocked.CompareExchange<MethodSymbol>(ref lazyMethod, methodSymbol, ErrorMethodSymbol.UnknownMethod) == ErrorMethodSymbol.UnknownMethod && useSiteInfo.DiagnosticInfo != null)
				{
					Symbol.ReportUseSiteDiagnostic(useSiteInfo.DiagnosticInfo, diagnostics, (syntaxNodeOpt != null) ? syntaxNodeOpt.Location : NoLocation.Singleton);
				}
			}
			return lazyMethod;
		}

		// Token: 0x0600622D RID: 25133 RVA: 0x00179055 File Offset: 0x00177255
		internal override int GetTargetAttributeSignatureIndex(Symbol underlyingSymbol, CSharpAttributeData attrData, AttributeDescription description)
		{
			return attrData.GetTargetAttributeSignatureIndex(underlyingSymbol.AdaptedSymbol, description);
		}

		// Token: 0x0600622E RID: 25134 RVA: 0x00179064 File Offset: 0x00177264
		internal override CSharpAttributeData CreateSynthesizedAttribute(WellKnownMember constructor, CSharpAttributeData attrData, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			MethodSymbol wellKnownMethod = this.GetWellKnownMethod(constructor, syntaxNodeOpt, diagnostics);
			if (wellKnownMethod == null)
			{
				return null;
			}
			if (constructor == WellKnownMember.System_Runtime_InteropServices_CoClassAttribute__ctor)
			{
				return new SynthesizedAttributeData(wellKnownMethod, ImmutableArray.Create<TypedConstant>(new TypedConstant(wellKnownMethod.Parameters[0].Type, TypedConstantKind.Type, wellKnownMethod.ContainingAssembly.GetSpecialType(SpecialType.System_Object))), ImmutableArray<KeyValuePair<string, TypedConstant>>.Empty);
			}
			if (constructor == WellKnownMember.System_Runtime_InteropServices_ComEventInterfaceAttribute__ctor)
			{
				return new SynthesizedAttributeData(wellKnownMethod, ImmutableArray.Create<TypedConstant>(attrData.CommonConstructorArguments[0], attrData.CommonConstructorArguments[0]), ImmutableArray<KeyValuePair<string, TypedConstant>>.Empty);
			}
			return new SynthesizedAttributeData(wellKnownMethod, attrData.CommonConstructorArguments, attrData.CommonNamedArguments);
		}

		// Token: 0x0600622F RID: 25135 RVA: 0x00179104 File Offset: 0x00177304
		internal string GetAssemblyGuidString(AssemblySymbol assembly)
		{
			string text;
			if (this._assemblyGuidMap.TryGetValue(assembly, ref text))
			{
				return text;
			}
			assembly.GetGuidString(out text);
			return this._assemblyGuidMap.GetOrAdd(assembly, text);
		}

		// Token: 0x06006230 RID: 25136 RVA: 0x0017913C File Offset: 0x0017733C
		protected override void OnGetTypesCompleted(ImmutableArray<EmbeddedType> types, DiagnosticBag diagnostics)
		{
			foreach (EmbeddedType embeddedType in types)
			{
				this._assemblyGuidMap.TryAdd(embeddedType.UnderlyingNamedType.AdaptedSymbol.ContainingAssembly, null);
			}
			foreach (AssemblySymbol assemblySymbol in this.ModuleBeingBuilt.GetReferencedAssembliesUsedSoFar())
			{
				this.ReportIndirectReferencesToLinkedAssemblies(assemblySymbol, diagnostics);
			}
		}

		// Token: 0x06006231 RID: 25137 RVA: 0x001791C8 File Offset: 0x001773C8
		protected override void ReportNameCollisionBetweenEmbeddedTypes(EmbeddedType typeA, EmbeddedType typeB, DiagnosticBag diagnostics)
		{
			NamedTypeSymbol underlyingNamedType = typeA.UnderlyingNamedType;
			NamedTypeSymbol underlyingNamedType2 = typeB.UnderlyingNamedType;
			EmbeddedTypesManager.Error(diagnostics, ErrorCode.ERR_InteropTypesWithSameNameAndGuid, null, new object[]
			{
				underlyingNamedType.AdaptedNamedTypeSymbol,
				underlyingNamedType.AdaptedSymbol.ContainingAssembly,
				underlyingNamedType2.AdaptedSymbol.ContainingAssembly
			});
		}

		// Token: 0x06006232 RID: 25138 RVA: 0x0017921C File Offset: 0x0017741C
		protected override void ReportNameCollisionWithAlreadyDeclaredType(EmbeddedType type, DiagnosticBag diagnostics)
		{
			NamedTypeSymbol underlyingNamedType = type.UnderlyingNamedType;
			EmbeddedTypesManager.Error(diagnostics, ErrorCode.ERR_LocalTypeNameClash, null, new object[]
			{
				underlyingNamedType.AdaptedNamedTypeSymbol,
				underlyingNamedType.AdaptedSymbol.ContainingAssembly
			});
		}

		// Token: 0x06006233 RID: 25139 RVA: 0x0017925C File Offset: 0x0017745C
		internal override void ReportIndirectReferencesToLinkedAssemblies(AssemblySymbol a, DiagnosticBag diagnostics)
		{
			foreach (ModuleSymbol moduleSymbol in a.Modules)
			{
				foreach (AssemblySymbol assemblySymbol in moduleSymbol.GetReferencedAssemblySymbols())
				{
					if (!assemblySymbol.IsMissing && assemblySymbol.IsLinked && this._assemblyGuidMap.ContainsKey(assemblySymbol))
					{
						EmbeddedTypesManager.Error(diagnostics, ErrorCode.WRN_ReferencedAssemblyReferencesLinkedPIA, null, new object[] { assemblySymbol, a });
					}
				}
			}
		}

		// Token: 0x06006234 RID: 25140 RVA: 0x001792E8 File Offset: 0x001774E8
		internal static bool IsValidEmbeddableType(NamedTypeSymbol namedType, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics, EmbeddedTypesManager optTypeManager = null)
		{
			if (namedType.SpecialType != SpecialType.None || namedType.IsErrorType() || !namedType.ContainingAssembly.IsLinked)
			{
				return false;
			}
			ErrorCode errorCode = ErrorCode.Unknown;
			TypeKind typeKind = namedType.TypeKind;
			switch (typeKind)
			{
			case TypeKind.Delegate:
			case TypeKind.Enum:
				break;
			case TypeKind.Dynamic:
			case TypeKind.Error:
				goto IL_00BF;
			case TypeKind.Interface:
				foreach (Symbol symbol in namedType.GetMembersUnordered())
				{
					if (symbol.Kind != SymbolKind.NamedType)
					{
						if (!symbol.IsAbstract)
						{
							errorCode = ErrorCode.ERR_DefaultInterfaceImplementationInNoPIAType;
							break;
						}
						if (symbol.IsSealed)
						{
							errorCode = ErrorCode.ERR_ReAbstractionInNoPIAType;
							break;
						}
					}
				}
				if (errorCode != ErrorCode.Unknown)
				{
					goto IL_00C5;
				}
				break;
			default:
				if (typeKind != TypeKind.Struct)
				{
					goto IL_00BF;
				}
				break;
			}
			if (namedType.ContainingType != null)
			{
				errorCode = ErrorCode.ERR_NoPIANestedType;
				goto IL_00C5;
			}
			if (namedType.IsGenericType)
			{
				errorCode = ErrorCode.ERR_GenericsUsedInNoPIAType;
				goto IL_00C5;
			}
			goto IL_00C5;
			IL_00BF:
			errorCode = ErrorCode.ERR_NewCoClassOnLink;
			IL_00C5:
			if (errorCode != ErrorCode.Unknown)
			{
				EmbeddedTypesManager.ReportNotEmbeddableSymbol(errorCode, namedType, syntaxNodeOpt, diagnostics, optTypeManager);
				return false;
			}
			return true;
		}

		// Token: 0x06006235 RID: 25141 RVA: 0x001793CB File Offset: 0x001775CB
		private static void ReportNotEmbeddableSymbol(ErrorCode error, Symbol symbol, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics, EmbeddedTypesManager optTypeManager)
		{
			if (optTypeManager == null || optTypeManager._reportedSymbolsMap.TryAdd(symbol.OriginalDefinition, true))
			{
				EmbeddedTypesManager.Error(diagnostics, error, syntaxNodeOpt, new object[] { symbol.OriginalDefinition });
			}
		}

		// Token: 0x06006236 RID: 25142 RVA: 0x001793FD File Offset: 0x001775FD
		internal static void Error(DiagnosticBag diagnostics, ErrorCode code, SyntaxNode syntaxOpt, params object[] args)
		{
			EmbeddedTypesManager.Error(diagnostics, syntaxOpt, new CSDiagnosticInfo(code, args));
		}

		// Token: 0x06006237 RID: 25143 RVA: 0x0017940D File Offset: 0x0017760D
		private static void Error(DiagnosticBag diagnostics, SyntaxNode syntaxOpt, DiagnosticInfo info)
		{
			diagnostics.Add(new CSDiagnostic(info, (syntaxOpt == null) ? NoLocation.Singleton : syntaxOpt.Location, false));
		}

		// Token: 0x06006238 RID: 25144 RVA: 0x0017942C File Offset: 0x0017762C
		internal INamedTypeReference EmbedTypeIfNeedTo(NamedTypeSymbol namedType, bool fromImplements, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			if (EmbeddedTypesManager.IsValidEmbeddableType(namedType, syntaxNodeOpt, diagnostics, this))
			{
				return this.EmbedType(namedType, fromImplements, syntaxNodeOpt, diagnostics);
			}
			return null;
		}

		// Token: 0x06006239 RID: 25145 RVA: 0x00179448 File Offset: 0x00177648
		private EmbeddedType EmbedType(NamedTypeSymbol namedType, bool fromImplements, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			NamedTypeSymbol cciAdapter = namedType.GetCciAdapter();
			EmbeddedType embeddedType = new EmbeddedType(this, cciAdapter);
			EmbeddedType orAdd = this.EmbeddedTypesMap.GetOrAdd(cciAdapter, embeddedType);
			bool isInterface = namedType.IsInterface;
			if (isInterface && fromImplements)
			{
				orAdd.EmbedAllMembersOfImplementedInterface(syntaxNodeOpt, diagnostics);
			}
			if (embeddedType != orAdd)
			{
				return orAdd;
			}
			new TypeReferenceIndexer(new EmitContext(this.ModuleBeingBuilt, syntaxNodeOpt, diagnostics, false, true)).VisitTypeDefinitionNoMembers(embeddedType);
			if (!isInterface)
			{
				if (namedType.TypeKind != TypeKind.Struct)
				{
					TypeKind typeKind = namedType.TypeKind;
				}
				foreach (FieldSymbol fieldSymbol in namedType.GetFieldsToEmit())
				{
					this.EmbedField(embeddedType, fieldSymbol.GetCciAdapter(), syntaxNodeOpt, diagnostics);
				}
				foreach (MethodSymbol methodSymbol in namedType.GetMethodsToEmit())
				{
					if (methodSymbol != null)
					{
						this.EmbedMethod(embeddedType, methodSymbol.GetCciAdapter(), syntaxNodeOpt, diagnostics);
					}
				}
			}
			return embeddedType;
		}

		// Token: 0x0600623A RID: 25146 RVA: 0x00179564 File Offset: 0x00177764
		internal override EmbeddedField EmbedField(EmbeddedType type, FieldSymbol field, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			EmbeddedField embeddedField = new EmbeddedField(type, field);
			EmbeddedField orAdd = this.EmbeddedFieldsMap.GetOrAdd(field, embeddedField);
			if (embeddedField != orAdd)
			{
				return orAdd;
			}
			base.EmbedReferences(embeddedField, syntaxNodeOpt, diagnostics);
			TypeKind typeKind = field.AdaptedFieldSymbol.ContainingType.TypeKind;
			if (typeKind == TypeKind.Interface || typeKind == TypeKind.Delegate || (typeKind == TypeKind.Struct && (field.AdaptedFieldSymbol.IsStatic || field.AdaptedFieldSymbol.DeclaredAccessibility != Accessibility.Public)))
			{
				EmbeddedTypesManager.ReportNotEmbeddableSymbol(ErrorCode.ERR_InteropStructContainsMethods, field.AdaptedFieldSymbol.ContainingType, syntaxNodeOpt, diagnostics, this);
			}
			return embeddedField;
		}

		// Token: 0x0600623B RID: 25147 RVA: 0x001795EC File Offset: 0x001777EC
		internal override EmbeddedMethod EmbedMethod(EmbeddedType type, MethodSymbol method, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			EmbeddedMethod embeddedMethod = new EmbeddedMethod(type, method);
			EmbeddedMethod orAdd = this.EmbeddedMethodsMap.GetOrAdd(method, embeddedMethod);
			if (embeddedMethod != orAdd)
			{
				return orAdd;
			}
			base.EmbedReferences(embeddedMethod, syntaxNodeOpt, diagnostics);
			TypeKind typeKind = type.UnderlyingNamedType.AdaptedNamedTypeSymbol.TypeKind;
			if (typeKind == TypeKind.Enum || typeKind == TypeKind.Struct)
			{
				EmbeddedTypesManager.ReportNotEmbeddableSymbol(ErrorCode.ERR_InteropStructContainsMethods, type.UnderlyingNamedType.AdaptedNamedTypeSymbol, syntaxNodeOpt, diagnostics, this);
			}
			else if (embeddedMethod.HasBody())
			{
				EmbeddedTypesManager.Error(diagnostics, ErrorCode.ERR_InteropMethodWithBody, syntaxNodeOpt, new object[] { method.AdaptedMethodSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat) });
			}
			Symbol associatedSymbol = method.AdaptedMethodSymbol.AssociatedSymbol;
			if (associatedSymbol != null)
			{
				SymbolKind kind = associatedSymbol.Kind;
				if (kind != SymbolKind.Event)
				{
					if (kind != SymbolKind.Property)
					{
						throw ExceptionUtilities.UnexpectedValue(associatedSymbol.Kind);
					}
					this.EmbedProperty(type, ((PropertySymbol)associatedSymbol).GetCciAdapter(), syntaxNodeOpt, diagnostics);
				}
				else
				{
					this.EmbedEvent(type, ((EventSymbol)associatedSymbol).GetCciAdapter(), syntaxNodeOpt, diagnostics, false);
				}
			}
			return embeddedMethod;
		}

		// Token: 0x0600623C RID: 25148 RVA: 0x001796E8 File Offset: 0x001778E8
		internal override EmbeddedProperty EmbedProperty(EmbeddedType type, PropertySymbol property, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			MethodSymbol getMethod = property.AdaptedPropertySymbol.GetMethod;
			MethodSymbol methodSymbol = ((getMethod != null) ? getMethod.GetCciAdapter() : null);
			MethodSymbol setMethod = property.AdaptedPropertySymbol.SetMethod;
			MethodSymbol methodSymbol2 = ((setMethod != null) ? setMethod.GetCciAdapter() : null);
			EmbeddedMethod embeddedMethod = ((methodSymbol != null) ? this.EmbedMethod(type, methodSymbol, syntaxNodeOpt, diagnostics) : null);
			EmbeddedMethod embeddedMethod2 = ((methodSymbol2 != null) ? this.EmbedMethod(type, methodSymbol2, syntaxNodeOpt, diagnostics) : null);
			EmbeddedProperty embeddedProperty = new EmbeddedProperty(property, embeddedMethod, embeddedMethod2);
			EmbeddedProperty orAdd = this.EmbeddedPropertiesMap.GetOrAdd(property, embeddedProperty);
			if (embeddedProperty != orAdd)
			{
				return orAdd;
			}
			base.EmbedReferences(embeddedProperty, syntaxNodeOpt, diagnostics);
			return embeddedProperty;
		}

		// Token: 0x0600623D RID: 25149 RVA: 0x0017977C File Offset: 0x0017797C
		internal override EmbeddedEvent EmbedEvent(EmbeddedType type, EventSymbol @event, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics, bool isUsedForComAwareEventBinding)
		{
			MethodSymbol addMethod = @event.AdaptedEventSymbol.AddMethod;
			MethodSymbol methodSymbol = ((addMethod != null) ? addMethod.GetCciAdapter() : null);
			MethodSymbol removeMethod = @event.AdaptedEventSymbol.RemoveMethod;
			MethodSymbol methodSymbol2 = ((removeMethod != null) ? removeMethod.GetCciAdapter() : null);
			EmbeddedMethod embeddedMethod = ((methodSymbol != null) ? this.EmbedMethod(type, methodSymbol, syntaxNodeOpt, diagnostics) : null);
			EmbeddedMethod embeddedMethod2 = ((methodSymbol2 != null) ? this.EmbedMethod(type, methodSymbol2, syntaxNodeOpt, diagnostics) : null);
			EmbeddedEvent embeddedEvent = new EmbeddedEvent(@event, embeddedMethod, embeddedMethod2);
			EmbeddedEvent orAdd = this.EmbeddedEventsMap.GetOrAdd(@event, embeddedEvent);
			if (embeddedEvent != orAdd)
			{
				if (isUsedForComAwareEventBinding)
				{
					orAdd.EmbedCorrespondingComEventInterfaceMethod(syntaxNodeOpt, diagnostics, isUsedForComAwareEventBinding);
				}
				return orAdd;
			}
			base.EmbedReferences(embeddedEvent, syntaxNodeOpt, diagnostics);
			embeddedEvent.EmbedCorrespondingComEventInterfaceMethod(syntaxNodeOpt, diagnostics, isUsedForComAwareEventBinding);
			return embeddedEvent;
		}

		// Token: 0x0600623E RID: 25150 RVA: 0x0017982C File Offset: 0x00177A2C
		protected override EmbeddedType GetEmbeddedTypeForMember(Symbol member, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			NamedTypeSymbol containingType = member.AdaptedSymbol.ContainingType;
			if (EmbeddedTypesManager.IsValidEmbeddableType(containingType, syntaxNodeOpt, diagnostics, this))
			{
				return this.EmbedType(containingType, false, syntaxNodeOpt, diagnostics);
			}
			return null;
		}

		// Token: 0x0600623F RID: 25151 RVA: 0x0017985C File Offset: 0x00177A5C
		internal static ImmutableArray<EmbeddedParameter> EmbedParameters(EmbeddedTypesManager<PEModuleBuilder, ModuleCompilationState, EmbeddedTypesManager, SyntaxNode, CSharpAttributeData, Symbol, AssemblySymbol, NamedTypeSymbol, FieldSymbol, MethodSymbol, EventSymbol, PropertySymbol, ParameterSymbol, TypeParameterSymbol, EmbeddedType, EmbeddedField, EmbeddedMethod, EmbeddedEvent, EmbeddedProperty, EmbeddedParameter, EmbeddedTypeParameter>.CommonEmbeddedMember containingPropertyOrMethod, ImmutableArray<ParameterSymbol> underlyingParameters)
		{
			return underlyingParameters.SelectAsArray((ParameterSymbol p, EmbeddedTypesManager<PEModuleBuilder, ModuleCompilationState, EmbeddedTypesManager, SyntaxNode, CSharpAttributeData, Symbol, AssemblySymbol, NamedTypeSymbol, FieldSymbol, MethodSymbol, EventSymbol, PropertySymbol, ParameterSymbol, TypeParameterSymbol, EmbeddedType, EmbeddedField, EmbeddedMethod, EmbeddedEvent, EmbeddedProperty, EmbeddedParameter, EmbeddedTypeParameter>.CommonEmbeddedMember c) => new EmbeddedParameter(c, p.GetCciAdapter()), containingPropertyOrMethod);
		}

		// Token: 0x06006240 RID: 25152 RVA: 0x00179884 File Offset: 0x00177A84
		protected override CSharpAttributeData CreateCompilerGeneratedAttribute()
		{
			return this.ModuleBeingBuilt.Compilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_CompilerGeneratedAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false);
		}

		// Token: 0x04001978 RID: 6520
		private readonly ConcurrentDictionary<AssemblySymbol, string> _assemblyGuidMap = new ConcurrentDictionary<AssemblySymbol, string>(ReferenceEqualityComparer.Instance);

		// Token: 0x04001979 RID: 6521
		private readonly ConcurrentDictionary<Symbol, bool> _reportedSymbolsMap = new ConcurrentDictionary<Symbol, bool>(ReferenceEqualityComparer.Instance);

		// Token: 0x0400197A RID: 6522
		private NamedTypeSymbol _lazySystemStringType = ErrorTypeSymbol.UnknownResultType;

		// Token: 0x0400197B RID: 6523
		private readonly MethodSymbol[] _lazyWellKnownTypeMethods;
	}
}
