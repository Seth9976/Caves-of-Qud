using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Emit.NoPia;
using Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.RuntimeMembers;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004B1 RID: 1201
	internal abstract class NamedTypeSymbol : TypeSymbol, ITypeReference, IReference, ITypeDefinition, IDefinition, INamedTypeReference, INamedEntity, INamedTypeDefinition, INamespaceTypeReference, INamespaceTypeDefinition, INestedTypeReference, ITypeMemberReference, INestedTypeDefinition, ITypeDefinitionMember, IGenericTypeInstanceReference, ISpecializedNestedTypeReference, INamedTypeSymbolInternal, ITypeSymbolInternal, INamespaceOrTypeSymbolInternal, ISymbolInternal
	{
		// Token: 0x17001871 RID: 6257
		// (get) Token: 0x060064C4 RID: 25796 RVA: 0x0017EC44 File Offset: 0x0017CE44
		bool ITypeReference.IsEnum
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.TypeKind == TypeKind.Enum;
			}
		}

		// Token: 0x17001872 RID: 6258
		// (get) Token: 0x060064C5 RID: 25797 RVA: 0x0017EC54 File Offset: 0x0017CE54
		bool ITypeReference.IsValueType
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.IsValueType;
			}
		}

		// Token: 0x060064C6 RID: 25798 RVA: 0x0017EC64 File Offset: 0x0017CE64
		ITypeDefinition ITypeReference.GetResolvedType(EmitContext context)
		{
			PEModuleBuilder pemoduleBuilder = (PEModuleBuilder)context.Module;
			return this.AsTypeDefinitionImpl(pemoduleBuilder);
		}

		// Token: 0x17001873 RID: 6259
		// (get) Token: 0x060064C7 RID: 25799 RVA: 0x0017EC84 File Offset: 0x0017CE84
		Microsoft.Cci.PrimitiveTypeCode ITypeReference.TypeCode
		{
			get
			{
				if (this.AdaptedNamedTypeSymbol.IsDefinition)
				{
					return this.AdaptedNamedTypeSymbol.PrimitiveTypeCode;
				}
				return Microsoft.Cci.PrimitiveTypeCode.NotPrimitive;
			}
		}

		// Token: 0x17001874 RID: 6260
		// (get) Token: 0x060064C8 RID: 25800 RVA: 0x0017ECA4 File Offset: 0x0017CEA4
		TypeDefinitionHandle ITypeReference.TypeDef
		{
			get
			{
				PENamedTypeSymbol penamedTypeSymbol = this.AdaptedNamedTypeSymbol as PENamedTypeSymbol;
				if (penamedTypeSymbol != null)
				{
					return penamedTypeSymbol.Handle;
				}
				return default(TypeDefinitionHandle);
			}
		}

		// Token: 0x17001875 RID: 6261
		// (get) Token: 0x060064C9 RID: 25801 RVA: 0x0017ECD0 File Offset: 0x0017CED0
		IGenericMethodParameterReference ITypeReference.AsGenericMethodParameterReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001876 RID: 6262
		// (get) Token: 0x060064CA RID: 25802 RVA: 0x0017ECD3 File Offset: 0x0017CED3
		IGenericTypeInstanceReference ITypeReference.AsGenericTypeInstanceReference
		{
			get
			{
				if (!this.AdaptedNamedTypeSymbol.IsDefinition && this.AdaptedNamedTypeSymbol.Arity > 0)
				{
					return this;
				}
				return null;
			}
		}

		// Token: 0x17001877 RID: 6263
		// (get) Token: 0x060064CB RID: 25803 RVA: 0x0017ECF3 File Offset: 0x0017CEF3
		IGenericTypeParameterReference ITypeReference.AsGenericTypeParameterReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001878 RID: 6264
		// (get) Token: 0x060064CC RID: 25804 RVA: 0x0017ECF6 File Offset: 0x0017CEF6
		INamespaceTypeReference ITypeReference.AsNamespaceTypeReference
		{
			get
			{
				if (this.AdaptedNamedTypeSymbol.IsDefinition && this.AdaptedNamedTypeSymbol.ContainingType == null)
				{
					return this;
				}
				return null;
			}
		}

		// Token: 0x060064CD RID: 25805 RVA: 0x0017ED18 File Offset: 0x0017CF18
		INamespaceTypeDefinition ITypeReference.AsNamespaceTypeDefinition(EmitContext context)
		{
			PEModuleBuilder pemoduleBuilder = (PEModuleBuilder)context.Module;
			if (this.AdaptedNamedTypeSymbol.ContainingType == null && this.AdaptedNamedTypeSymbol.IsDefinition && this.AdaptedNamedTypeSymbol.ContainingModule == pemoduleBuilder.SourceModule)
			{
				return this;
			}
			return null;
		}

		// Token: 0x17001879 RID: 6265
		// (get) Token: 0x060064CE RID: 25806 RVA: 0x0017ED66 File Offset: 0x0017CF66
		INestedTypeReference ITypeReference.AsNestedTypeReference
		{
			get
			{
				if (this.AdaptedNamedTypeSymbol.ContainingType != null)
				{
					return this;
				}
				return null;
			}
		}

		// Token: 0x060064CF RID: 25807 RVA: 0x0017ED78 File Offset: 0x0017CF78
		INestedTypeDefinition ITypeReference.AsNestedTypeDefinition(EmitContext context)
		{
			PEModuleBuilder pemoduleBuilder = (PEModuleBuilder)context.Module;
			return this.AsNestedTypeDefinitionImpl(pemoduleBuilder);
		}

		// Token: 0x060064D0 RID: 25808 RVA: 0x0017ED98 File Offset: 0x0017CF98
		private INestedTypeDefinition AsNestedTypeDefinitionImpl(PEModuleBuilder moduleBeingBuilt)
		{
			if (this.AdaptedNamedTypeSymbol.ContainingType != null && this.AdaptedNamedTypeSymbol.IsDefinition && this.AdaptedNamedTypeSymbol.ContainingModule == moduleBeingBuilt.SourceModule)
			{
				return this;
			}
			return null;
		}

		// Token: 0x1700187A RID: 6266
		// (get) Token: 0x060064D1 RID: 25809 RVA: 0x0017EDCF File Offset: 0x0017CFCF
		ISpecializedNestedTypeReference ITypeReference.AsSpecializedNestedTypeReference
		{
			get
			{
				if (!this.AdaptedNamedTypeSymbol.IsDefinition && (this.AdaptedNamedTypeSymbol.Arity == 0 || PEModuleBuilder.IsGenericType(this.AdaptedNamedTypeSymbol.ContainingType)))
				{
					return this;
				}
				return null;
			}
		}

		// Token: 0x060064D2 RID: 25810 RVA: 0x0017EE00 File Offset: 0x0017D000
		ITypeDefinition ITypeReference.AsTypeDefinition(EmitContext context)
		{
			PEModuleBuilder pemoduleBuilder = (PEModuleBuilder)context.Module;
			return this.AsTypeDefinitionImpl(pemoduleBuilder);
		}

		// Token: 0x060064D3 RID: 25811 RVA: 0x0017EE20 File Offset: 0x0017D020
		private ITypeDefinition AsTypeDefinitionImpl(PEModuleBuilder moduleBeingBuilt)
		{
			if (this.AdaptedNamedTypeSymbol.IsDefinition && this.AdaptedNamedTypeSymbol.ContainingModule == moduleBeingBuilt.SourceModule)
			{
				return this;
			}
			return null;
		}

		// Token: 0x060064D4 RID: 25812 RVA: 0x0017EE4A File Offset: 0x0017D04A
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060064D5 RID: 25813 RVA: 0x0017EE54 File Offset: 0x0017D054
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			PEModuleBuilder pemoduleBuilder = (PEModuleBuilder)context.Module;
			return this.AsTypeDefinitionImpl(pemoduleBuilder);
		}

		// Token: 0x060064D6 RID: 25814 RVA: 0x0017EE74 File Offset: 0x0017D074
		ITypeReference ITypeDefinition.GetBaseClass(EmitContext context)
		{
			PEModuleBuilder pemoduleBuilder = (PEModuleBuilder)context.Module;
			NamedTypeSymbol namedTypeSymbol = this.AdaptedNamedTypeSymbol.BaseTypeNoUseSiteDiagnostics;
			if (this.AdaptedNamedTypeSymbol.IsScriptClass)
			{
				namedTypeSymbol = this.AdaptedNamedTypeSymbol.ContainingAssembly.GetSpecialType(SpecialType.System_Object);
			}
			if (namedTypeSymbol == null)
			{
				return null;
			}
			return pemoduleBuilder.Translate(namedTypeSymbol, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, false, false);
		}

		// Token: 0x060064D7 RID: 25815 RVA: 0x0017EED7 File Offset: 0x0017D0D7
		IEnumerable<IEventDefinition> ITypeDefinition.GetEvents(EmitContext context)
		{
			foreach (EventSymbol eventSymbol in this.AdaptedNamedTypeSymbol.GetEventsToEmit())
			{
				IEventDefinition cciAdapter = eventSymbol.GetCciAdapter();
				if (cciAdapter.ShouldInclude(context) || !cciAdapter.GetAccessors(context).IsEmpty<IMethodReference>())
				{
					yield return cciAdapter;
				}
			}
			IEnumerator<EventSymbol> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060064D8 RID: 25816 RVA: 0x0017EEEE File Offset: 0x0017D0EE
		IEnumerable<Microsoft.Cci.MethodImplementation> ITypeDefinition.GetExplicitImplementationOverrides(EmitContext context)
		{
			PEModuleBuilder moduleBeingBuilt = (PEModuleBuilder)context.Module;
			foreach (Symbol symbol in this.AdaptedNamedTypeSymbol.GetMembers())
			{
				if (symbol.Kind == SymbolKind.Method)
				{
					MethodSymbol method = (MethodSymbol)symbol;
					if (method.ExplicitInterfaceImplementations.Length != 0)
					{
						MethodSymbol adapter = method.GetCciAdapter();
						foreach (MethodSymbol methodSymbol in method.ExplicitInterfaceImplementations)
						{
							yield return new Microsoft.Cci.MethodImplementation(adapter, moduleBeingBuilt.TranslateOverriddenMethodReference(methodSymbol, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics));
						}
						ImmutableArray<MethodSymbol>.Enumerator enumerator2 = default(ImmutableArray<MethodSymbol>.Enumerator);
						adapter = null;
					}
					if (!this.AdaptedNamedTypeSymbol.IsInterface)
					{
						bool flag;
						if (method.RequiresExplicitOverride(out flag))
						{
							yield return new Microsoft.Cci.MethodImplementation(method.GetCciAdapter(), moduleBeingBuilt.TranslateOverriddenMethodReference(method.OverriddenMethod, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics));
						}
						else if (method.MethodKind == MethodKind.Destructor && this.AdaptedNamedTypeSymbol.SpecialType != SpecialType.System_Object)
						{
							TypeSymbol specialType = this.AdaptedNamedTypeSymbol.DeclaringCompilation.GetSpecialType(SpecialType.System_Object);
							foreach (Symbol symbol2 in specialType.GetMembers("Finalize"))
							{
								MethodSymbol methodSymbol2 = symbol2 as MethodSymbol;
								if (methodSymbol2 != null && methodSymbol2.MethodKind == MethodKind.Destructor)
								{
									yield return new Microsoft.Cci.MethodImplementation(method.GetCciAdapter(), moduleBeingBuilt.TranslateOverriddenMethodReference(methodSymbol2, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics));
								}
							}
							ImmutableArray<Symbol>.Enumerator enumerator3 = default(ImmutableArray<Symbol>.Enumerator);
						}
						method = null;
					}
				}
			}
			ImmutableArray<Symbol>.Enumerator enumerator = default(ImmutableArray<Symbol>.Enumerator);
			if (this.AdaptedNamedTypeSymbol.IsInterface)
			{
				yield break;
			}
			IEnumerable<IMethodDefinition> synthesizedMethods = moduleBeingBuilt.GetSynthesizedMethods(this.AdaptedNamedTypeSymbol);
			if (synthesizedMethods != null)
			{
				foreach (IMethodDefinition i in synthesizedMethods)
				{
					MethodSymbol methodSymbol3 = i.GetInternalSymbol() as MethodSymbol;
					if (methodSymbol3 != null)
					{
						foreach (MethodSymbol methodSymbol4 in methodSymbol3.ExplicitInterfaceImplementations)
						{
							yield return new Microsoft.Cci.MethodImplementation(i, moduleBeingBuilt.TranslateOverriddenMethodReference(methodSymbol4, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics));
						}
						ImmutableArray<MethodSymbol>.Enumerator enumerator2 = default(ImmutableArray<MethodSymbol>.Enumerator);
					}
					i = null;
				}
				IEnumerator<IMethodDefinition> enumerator4 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060064D9 RID: 25817 RVA: 0x0017EF05 File Offset: 0x0017D105
		IEnumerable<IFieldDefinition> ITypeDefinition.GetFields(EmitContext context)
		{
			bool isStruct = this.AdaptedNamedTypeSymbol.IsStructType();
			foreach (FieldSymbol fieldSymbol in this.AdaptedNamedTypeSymbol.GetFieldsToEmit())
			{
				if (isStruct || fieldSymbol.GetCciAdapter().ShouldInclude(context))
				{
					yield return fieldSymbol.GetCciAdapter();
				}
			}
			IEnumerator<FieldSymbol> enumerator = null;
			IEnumerable<IFieldDefinition> synthesizedFields = ((PEModuleBuilder)context.Module).GetSynthesizedFields(this.AdaptedNamedTypeSymbol);
			if (synthesizedFields != null)
			{
				foreach (IFieldDefinition fieldDefinition in synthesizedFields)
				{
					if (isStruct || fieldDefinition.ShouldInclude(context))
					{
						yield return fieldDefinition;
					}
				}
				IEnumerator<IFieldDefinition> enumerator2 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x1700187B RID: 6267
		// (get) Token: 0x060064DA RID: 25818 RVA: 0x0017EF1C File Offset: 0x0017D11C
		IEnumerable<IGenericTypeParameter> ITypeDefinition.GenericParameters
		{
			get
			{
				foreach (TypeParameterSymbol typeParameterSymbol in this.AdaptedNamedTypeSymbol.TypeParameters)
				{
					yield return typeParameterSymbol.GetCciAdapter();
				}
				ImmutableArray<TypeParameterSymbol>.Enumerator enumerator = default(ImmutableArray<TypeParameterSymbol>.Enumerator);
				yield break;
			}
		}

		// Token: 0x1700187C RID: 6268
		// (get) Token: 0x060064DB RID: 25819 RVA: 0x0017EF2C File Offset: 0x0017D12C
		ushort ITypeDefinition.GenericParameterCount
		{
			get
			{
				return this.GenericParameterCountImpl;
			}
		}

		// Token: 0x1700187D RID: 6269
		// (get) Token: 0x060064DC RID: 25820 RVA: 0x0017EF34 File Offset: 0x0017D134
		private ushort GenericParameterCountImpl
		{
			get
			{
				return (ushort)this.AdaptedNamedTypeSymbol.Arity;
			}
		}

		// Token: 0x060064DD RID: 25821 RVA: 0x0017EF42 File Offset: 0x0017D142
		IEnumerable<TypeReferenceWithAttributes> ITypeDefinition.Interfaces(EmitContext context)
		{
			PEModuleBuilder moduleBeingBuilt = (PEModuleBuilder)context.Module;
			foreach (NamedTypeSymbol namedTypeSymbol in this.AdaptedNamedTypeSymbol.GetInterfacesToEmit())
			{
				INamedTypeReference namedTypeReference = moduleBeingBuilt.Translate(namedTypeSymbol, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, true, false);
				TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(namedTypeSymbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				yield return typeWithAnnotations.GetTypeRefWithAttributes(moduleBeingBuilt, this.AdaptedNamedTypeSymbol, namedTypeReference);
			}
			ImmutableArray<NamedTypeSymbol>.Enumerator enumerator = default(ImmutableArray<NamedTypeSymbol>.Enumerator);
			yield break;
		}

		// Token: 0x1700187E RID: 6270
		// (get) Token: 0x060064DE RID: 25822 RVA: 0x0017EF59 File Offset: 0x0017D159
		bool ITypeDefinition.IsAbstract
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.IsMetadataAbstract;
			}
		}

		// Token: 0x1700187F RID: 6271
		// (get) Token: 0x060064DF RID: 25823 RVA: 0x0017EF68 File Offset: 0x0017D168
		bool ITypeDefinition.IsBeforeFieldInit
		{
			get
			{
				switch (this.AdaptedNamedTypeSymbol.TypeKind)
				{
				case TypeKind.Delegate:
				case TypeKind.Enum:
				case TypeKind.Interface:
					return false;
				}
				ImmutableArray<Symbol>.Enumerator enumerator = this.AdaptedNamedTypeSymbol.GetMembers(".cctor").GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsImplicitlyDeclared)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17001880 RID: 6272
		// (get) Token: 0x060064E0 RID: 25824 RVA: 0x0017EFD6 File Offset: 0x0017D1D6
		bool ITypeDefinition.IsComObject
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.IsComImport;
			}
		}

		// Token: 0x17001881 RID: 6273
		// (get) Token: 0x060064E1 RID: 25825 RVA: 0x0017EFE3 File Offset: 0x0017D1E3
		bool ITypeDefinition.IsGeneric
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.Arity != 0;
			}
		}

		// Token: 0x17001882 RID: 6274
		// (get) Token: 0x060064E2 RID: 25826 RVA: 0x0017EFF3 File Offset: 0x0017D1F3
		bool ITypeDefinition.IsInterface
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.IsInterface;
			}
		}

		// Token: 0x17001883 RID: 6275
		// (get) Token: 0x060064E3 RID: 25827 RVA: 0x0017F000 File Offset: 0x0017D200
		bool ITypeDefinition.IsDelegate
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.IsDelegateType();
			}
		}

		// Token: 0x17001884 RID: 6276
		// (get) Token: 0x060064E4 RID: 25828 RVA: 0x0017F00D File Offset: 0x0017D20D
		bool ITypeDefinition.IsRuntimeSpecial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001885 RID: 6277
		// (get) Token: 0x060064E5 RID: 25829 RVA: 0x0017F010 File Offset: 0x0017D210
		bool ITypeDefinition.IsSerializable
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.IsSerializable;
			}
		}

		// Token: 0x17001886 RID: 6278
		// (get) Token: 0x060064E6 RID: 25830 RVA: 0x0017F01D File Offset: 0x0017D21D
		bool ITypeDefinition.IsSpecialName
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.HasSpecialName;
			}
		}

		// Token: 0x17001887 RID: 6279
		// (get) Token: 0x060064E7 RID: 25831 RVA: 0x0017F02A File Offset: 0x0017D22A
		bool ITypeDefinition.IsWindowsRuntimeImport
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.IsWindowsRuntimeImport;
			}
		}

		// Token: 0x17001888 RID: 6280
		// (get) Token: 0x060064E8 RID: 25832 RVA: 0x0017F037 File Offset: 0x0017D237
		bool ITypeDefinition.IsSealed
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.IsMetadataSealed;
			}
		}

		// Token: 0x060064E9 RID: 25833 RVA: 0x0017F044 File Offset: 0x0017D244
		IEnumerable<IMethodDefinition> ITypeDefinition.GetMethods(EmitContext context)
		{
			bool alwaysIncludeConstructors = context.IncludePrivateMembers || this.AdaptedNamedTypeSymbol.DeclaringCompilation.IsAttributeType(this.AdaptedNamedTypeSymbol);
			foreach (MethodSymbol methodSymbol in this.AdaptedNamedTypeSymbol.GetMethodsToEmit())
			{
				if ((alwaysIncludeConstructors && methodSymbol.MethodKind == MethodKind.Constructor) || methodSymbol.GetCciAdapter().ShouldInclude(context))
				{
					yield return methodSymbol.GetCciAdapter();
				}
			}
			IEnumerator<MethodSymbol> enumerator = null;
			IEnumerable<IMethodDefinition> synthesizedMethods = ((PEModuleBuilder)context.Module).GetSynthesizedMethods(this.AdaptedNamedTypeSymbol);
			if (synthesizedMethods != null)
			{
				foreach (IMethodDefinition methodDefinition in synthesizedMethods)
				{
					if ((alwaysIncludeConstructors && methodDefinition.IsConstructor) || methodDefinition.ShouldInclude(context))
					{
						yield return methodDefinition;
					}
				}
				IEnumerator<IMethodDefinition> enumerator2 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060064EA RID: 25834 RVA: 0x0017F05B File Offset: 0x0017D25B
		IEnumerable<INestedTypeDefinition> ITypeDefinition.GetNestedTypes(EmitContext context)
		{
			foreach (NamedTypeSymbol namedTypeSymbol in this.AdaptedNamedTypeSymbol.GetTypeMembers())
			{
				yield return namedTypeSymbol.GetCciAdapter();
			}
			ImmutableArray<NamedTypeSymbol>.Enumerator enumerator = default(ImmutableArray<NamedTypeSymbol>.Enumerator);
			IEnumerable<INestedTypeDefinition> synthesizedTypes = ((PEModuleBuilder)context.Module).GetSynthesizedTypes(this.AdaptedNamedTypeSymbol);
			if (synthesizedTypes != null)
			{
				foreach (INestedTypeDefinition nestedTypeDefinition in synthesizedTypes)
				{
					yield return nestedTypeDefinition;
				}
				IEnumerator<INestedTypeDefinition> enumerator2 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060064EB RID: 25835 RVA: 0x0017F072 File Offset: 0x0017D272
		IEnumerable<IPropertyDefinition> ITypeDefinition.GetProperties(EmitContext context)
		{
			foreach (PropertySymbol propertySymbol in this.AdaptedNamedTypeSymbol.GetPropertiesToEmit())
			{
				IPropertyDefinition cciAdapter = propertySymbol.GetCciAdapter();
				if (cciAdapter.ShouldInclude(context) || !cciAdapter.GetAccessors(context).IsEmpty<IMethodReference>())
				{
					yield return cciAdapter;
				}
			}
			IEnumerator<PropertySymbol> enumerator = null;
			IEnumerable<IPropertyDefinition> synthesizedProperties = ((PEModuleBuilder)context.Module).GetSynthesizedProperties(this.AdaptedNamedTypeSymbol);
			if (synthesizedProperties != null)
			{
				foreach (IPropertyDefinition propertyDefinition in synthesizedProperties)
				{
					if (propertyDefinition.ShouldInclude(context) || !propertyDefinition.GetAccessors(context).IsEmpty<IMethodReference>())
					{
						yield return propertyDefinition;
					}
				}
				IEnumerator<IPropertyDefinition> enumerator2 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x17001889 RID: 6281
		// (get) Token: 0x060064EC RID: 25836 RVA: 0x0017F089 File Offset: 0x0017D289
		bool ITypeDefinition.HasDeclarativeSecurity
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.HasDeclarativeSecurity;
			}
		}

		// Token: 0x1700188A RID: 6282
		// (get) Token: 0x060064ED RID: 25837 RVA: 0x0017F096 File Offset: 0x0017D296
		IEnumerable<SecurityAttribute> ITypeDefinition.SecurityAttributes
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.GetSecurityInformation() ?? SpecializedCollections.EmptyEnumerable<SecurityAttribute>();
			}
		}

		// Token: 0x1700188B RID: 6283
		// (get) Token: 0x060064EE RID: 25838 RVA: 0x0017F0AC File Offset: 0x0017D2AC
		ushort ITypeDefinition.Alignment
		{
			get
			{
				return (ushort)this.AdaptedNamedTypeSymbol.Layout.Alignment;
			}
		}

		// Token: 0x1700188C RID: 6284
		// (get) Token: 0x060064EF RID: 25839 RVA: 0x0017F0D0 File Offset: 0x0017D2D0
		LayoutKind ITypeDefinition.Layout
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.Layout.Kind;
			}
		}

		// Token: 0x1700188D RID: 6285
		// (get) Token: 0x060064F0 RID: 25840 RVA: 0x0017F0F0 File Offset: 0x0017D2F0
		uint ITypeDefinition.SizeOf
		{
			get
			{
				return (uint)this.AdaptedNamedTypeSymbol.Layout.Size;
			}
		}

		// Token: 0x1700188E RID: 6286
		// (get) Token: 0x060064F1 RID: 25841 RVA: 0x0017F110 File Offset: 0x0017D310
		CharSet ITypeDefinition.StringFormat
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.MarshallingCharSet;
			}
		}

		// Token: 0x1700188F RID: 6287
		// (get) Token: 0x060064F2 RID: 25842 RVA: 0x0017F11D File Offset: 0x0017D31D
		ushort INamedTypeReference.GenericParameterCount
		{
			get
			{
				return this.GenericParameterCountImpl;
			}
		}

		// Token: 0x17001890 RID: 6288
		// (get) Token: 0x060064F3 RID: 25843 RVA: 0x0017F125 File Offset: 0x0017D325
		bool INamedTypeReference.MangleName
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.MangleName;
			}
		}

		// Token: 0x17001891 RID: 6289
		// (get) Token: 0x060064F4 RID: 25844 RVA: 0x0017F132 File Offset: 0x0017D332
		string INamedEntity.Name
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.Name;
			}
		}

		// Token: 0x060064F5 RID: 25845 RVA: 0x0017F13F File Offset: 0x0017D33F
		IUnitReference INamespaceTypeReference.GetUnit(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.AdaptedNamedTypeSymbol.ContainingModule, context.Diagnostics);
		}

		// Token: 0x17001892 RID: 6290
		// (get) Token: 0x060064F6 RID: 25846 RVA: 0x0017F162 File Offset: 0x0017D362
		string INamespaceTypeReference.NamespaceName
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.ContainingNamespace.QualifiedName;
			}
		}

		// Token: 0x17001893 RID: 6291
		// (get) Token: 0x060064F7 RID: 25847 RVA: 0x0017F174 File Offset: 0x0017D374
		bool INamespaceTypeDefinition.IsPublic
		{
			get
			{
				return PEModuleBuilder.MemberVisibility(this.AdaptedNamedTypeSymbol) == TypeMemberVisibility.Public;
			}
		}

		// Token: 0x060064F8 RID: 25848 RVA: 0x0017F184 File Offset: 0x0017D384
		ITypeReference ITypeMemberReference.GetContainingType(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.AdaptedNamedTypeSymbol.ContainingType, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, false, this.AdaptedNamedTypeSymbol.IsDefinition);
		}

		// Token: 0x17001894 RID: 6292
		// (get) Token: 0x060064F9 RID: 25849 RVA: 0x0017F1BE File Offset: 0x0017D3BE
		ITypeDefinition ITypeDefinitionMember.ContainingTypeDefinition
		{
			get
			{
				return this.AdaptedNamedTypeSymbol.ContainingType.GetCciAdapter();
			}
		}

		// Token: 0x17001895 RID: 6293
		// (get) Token: 0x060064FA RID: 25850 RVA: 0x0017F1D0 File Offset: 0x0017D3D0
		TypeMemberVisibility ITypeDefinitionMember.Visibility
		{
			get
			{
				return PEModuleBuilder.MemberVisibility(this.AdaptedNamedTypeSymbol);
			}
		}

		// Token: 0x060064FB RID: 25851 RVA: 0x0017F1E0 File Offset: 0x0017D3E0
		ImmutableArray<ITypeReference> IGenericTypeInstanceReference.GetGenericArguments(EmitContext context)
		{
			PEModuleBuilder pemoduleBuilder = (PEModuleBuilder)context.Module;
			ArrayBuilder<ITypeReference> instance = ArrayBuilder<ITypeReference>.GetInstance();
			ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotationsNoUseSiteDiagnostics = this.AdaptedNamedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics;
			for (int i = 0; i < typeArgumentsWithAnnotationsNoUseSiteDiagnostics.Length; i++)
			{
				ITypeReference typeReference = pemoduleBuilder.Translate(typeArgumentsWithAnnotationsNoUseSiteDiagnostics[i].Type, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
				ImmutableArray<CustomModifier> customModifiers = typeArgumentsWithAnnotationsNoUseSiteDiagnostics[i].CustomModifiers;
				if (!customModifiers.IsDefaultOrEmpty)
				{
					typeReference = new ModifiedTypeReference(typeReference, ImmutableArray<ICustomModifier>.CastUp<CustomModifier>(customModifiers));
				}
				instance.Add(typeReference);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060064FC RID: 25852 RVA: 0x0017F281 File Offset: 0x0017D481
		INamedTypeReference IGenericTypeInstanceReference.GetGenericType(EmitContext context)
		{
			return this.GenericTypeImpl(context);
		}

		// Token: 0x060064FD RID: 25853 RVA: 0x0017F28A File Offset: 0x0017D48A
		private INamedTypeReference GenericTypeImpl(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.AdaptedNamedTypeSymbol.OriginalDefinition, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, false, true);
		}

		// Token: 0x060064FE RID: 25854 RVA: 0x0017F2BA File Offset: 0x0017D4BA
		INestedTypeReference ISpecializedNestedTypeReference.GetUnspecializedVersion(EmitContext context)
		{
			return this.GenericTypeImpl(context).AsNestedTypeReference;
		}

		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x060064FF RID: 25855 RVA: 0x0017F2C8 File Offset: 0x0017D4C8
		internal NamedTypeSymbol AdaptedNamedTypeSymbol
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06006500 RID: 25856 RVA: 0x0017F2CB File Offset: 0x0017D4CB
		internal new NamedTypeSymbol GetCciAdapter()
		{
			return this;
		}

		// Token: 0x06006501 RID: 25857 RVA: 0x0017F2CE File Offset: 0x0017D4CE
		internal virtual IEnumerable<EventSymbol> GetEventsToEmit()
		{
			foreach (Symbol symbol in this.GetMembers())
			{
				if (symbol.Kind == SymbolKind.Event)
				{
					yield return (EventSymbol)symbol;
				}
			}
			ImmutableArray<Symbol>.Enumerator enumerator = default(ImmutableArray<Symbol>.Enumerator);
			yield break;
		}

		// Token: 0x06006502 RID: 25858
		internal abstract IEnumerable<FieldSymbol> GetFieldsToEmit();

		// Token: 0x06006503 RID: 25859
		internal abstract ImmutableArray<NamedTypeSymbol> GetInterfacesToEmit();

		// Token: 0x06006504 RID: 25860 RVA: 0x0017F2E0 File Offset: 0x0017D4E0
		protected ImmutableArray<NamedTypeSymbol> CalculateInterfacesToEmit()
		{
			ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance();
			HashSet<NamedTypeSymbol> hashSet = null;
			NamedTypeSymbol.InterfacesVisit(this, instance, ref hashSet);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06006505 RID: 25861 RVA: 0x0017F304 File Offset: 0x0017D504
		private static void InterfacesVisit(NamedTypeSymbol namedType, ArrayBuilder<NamedTypeSymbol> builder, ref HashSet<NamedTypeSymbol> seen)
		{
			foreach (NamedTypeSymbol namedTypeSymbol in namedType.InterfacesNoUseSiteDiagnostics(null))
			{
				if (seen == null)
				{
					seen = new HashSet<NamedTypeSymbol>(SymbolEqualityComparer.CLRSignature);
				}
				if (seen.Add(namedTypeSymbol))
				{
					builder.Add(namedTypeSymbol);
					NamedTypeSymbol.InterfacesVisit(namedTypeSymbol, builder, ref seen);
				}
			}
		}

		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x06006506 RID: 25862 RVA: 0x0017F35D File Offset: 0x0017D55D
		internal virtual bool IsMetadataAbstract
		{
			get
			{
				return this.IsAbstract || this.IsStatic;
			}
		}

		// Token: 0x17001898 RID: 6296
		// (get) Token: 0x06006507 RID: 25863 RVA: 0x0017F36F File Offset: 0x0017D56F
		internal virtual bool IsMetadataSealed
		{
			get
			{
				return this.IsSealed || this.IsStatic;
			}
		}

		// Token: 0x06006508 RID: 25864 RVA: 0x0017F381 File Offset: 0x0017D581
		internal virtual IEnumerable<MethodSymbol> GetMethodsToEmit()
		{
			foreach (Symbol symbol in this.GetMembers())
			{
				if (symbol.Kind == SymbolKind.Method)
				{
					MethodSymbol methodSymbol = (MethodSymbol)symbol;
					if (methodSymbol.ShouldEmit())
					{
						yield return methodSymbol;
					}
				}
			}
			ImmutableArray<Symbol>.Enumerator enumerator = default(ImmutableArray<Symbol>.Enumerator);
			yield break;
		}

		// Token: 0x06006509 RID: 25865 RVA: 0x0017F391 File Offset: 0x0017D591
		internal virtual IEnumerable<PropertySymbol> GetPropertiesToEmit()
		{
			foreach (Symbol symbol in this.GetMembers())
			{
				if (symbol.Kind == SymbolKind.Property)
				{
					yield return (PropertySymbol)symbol;
				}
			}
			ImmutableArray<Symbol>.Enumerator enumerator = default(ImmutableArray<Symbol>.Enumerator);
			yield break;
		}

		// Token: 0x0600650A RID: 25866 RVA: 0x0017F3A1 File Offset: 0x0017D5A1
		internal NamedTypeSymbol(NamedTypeSymbol.TupleExtraData tupleData = null)
		{
			this._lazyTupleData = tupleData;
		}

		// Token: 0x17001899 RID: 6297
		// (get) Token: 0x0600650B RID: 25867
		public abstract int Arity { get; }

		// Token: 0x1700189A RID: 6298
		// (get) Token: 0x0600650C RID: 25868
		public abstract ImmutableArray<TypeParameterSymbol> TypeParameters { get; }

		// Token: 0x1700189B RID: 6299
		// (get) Token: 0x0600650D RID: 25869
		internal abstract ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotationsNoUseSiteDiagnostics { get; }

		// Token: 0x0600650E RID: 25870 RVA: 0x0017F3B0 File Offset: 0x0017D5B0
		internal ImmutableArray<TypeWithAnnotations> TypeArgumentsWithDefinitionUseSiteDiagnostics(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotationsNoUseSiteDiagnostics = this.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics;
			foreach (TypeWithAnnotations typeWithAnnotations in typeArgumentsWithAnnotationsNoUseSiteDiagnostics)
			{
				typeWithAnnotations.Type.OriginalDefinition.AddUseSiteInfo(ref useSiteInfo, true);
			}
			return typeArgumentsWithAnnotationsNoUseSiteDiagnostics;
		}

		// Token: 0x0600650F RID: 25871 RVA: 0x0017F3F4 File Offset: 0x0017D5F4
		internal TypeWithAnnotations TypeArgumentWithDefinitionUseSiteDiagnostics(int index, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			TypeWithAnnotations typeWithAnnotations = this.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[index];
			typeWithAnnotations.Type.OriginalDefinition.AddUseSiteInfo(ref useSiteInfo, true);
			return typeWithAnnotations;
		}

		// Token: 0x1700189C RID: 6300
		// (get) Token: 0x06006510 RID: 25872
		public abstract NamedTypeSymbol ConstructedFrom { get; }

		// Token: 0x1700189D RID: 6301
		// (get) Token: 0x06006511 RID: 25873 RVA: 0x0017F425 File Offset: 0x0017D625
		public virtual NamedTypeSymbol EnumUnderlyingType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700189E RID: 6302
		// (get) Token: 0x06006512 RID: 25874 RVA: 0x0017F428 File Offset: 0x0017D628
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this.ContainingSymbol as NamedTypeSymbol;
			}
		}

		// Token: 0x1700189F RID: 6303
		// (get) Token: 0x06006513 RID: 25875 RVA: 0x0017F435 File Offset: 0x0017D635
		internal virtual bool KnownCircularStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170018A0 RID: 6304
		// (get) Token: 0x06006514 RID: 25876 RVA: 0x0017F438 File Offset: 0x0017D638
		internal bool KnownToHaveNoDeclaredBaseCycles
		{
			get
			{
				return this._hasNoBaseCycles;
			}
		}

		// Token: 0x06006515 RID: 25877 RVA: 0x0017F440 File Offset: 0x0017D640
		internal void SetKnownToHaveNoDeclaredBaseCycles()
		{
			this._hasNoBaseCycles = true;
		}

		// Token: 0x170018A1 RID: 6305
		// (get) Token: 0x06006516 RID: 25878 RVA: 0x0017F449 File Offset: 0x0017D649
		internal virtual bool IsExplicitDefinitionOfNoPiaLocalType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006517 RID: 25879 RVA: 0x0017F44C File Offset: 0x0017D64C
		internal virtual bool GetGuidString(out string guidString)
		{
			return base.GetGuidStringDefaultImplementation(out guidString);
		}

		// Token: 0x170018A2 RID: 6306
		// (get) Token: 0x06006518 RID: 25880 RVA: 0x0017F458 File Offset: 0x0017D658
		public MethodSymbol DelegateInvokeMethod
		{
			get
			{
				if (this.TypeKind != TypeKind.Delegate)
				{
					return null;
				}
				ImmutableArray<Symbol> members = this.GetMembers("Invoke");
				if (members.Length != 1)
				{
					return null;
				}
				return members[0] as MethodSymbol;
			}
		}

		// Token: 0x06006519 RID: 25881 RVA: 0x0017F498 File Offset: 0x0017D698
		internal ImmutableArray<MethodSymbol> GetOperators(string name)
		{
			ImmutableArray<Symbol> simpleNonTypeMembers = this.GetSimpleNonTypeMembers(name);
			if (simpleNonTypeMembers.IsEmpty)
			{
				return ImmutableArray<MethodSymbol>.Empty;
			}
			ArrayBuilder<MethodSymbol> instance = ArrayBuilder<MethodSymbol>.GetInstance();
			foreach (MethodSymbol methodSymbol in simpleNonTypeMembers.OfType<MethodSymbol>())
			{
				if (methodSymbol.MethodKind == MethodKind.UserDefinedOperator || methodSymbol.MethodKind == MethodKind.Conversion)
				{
					instance.Add(methodSymbol);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x170018A3 RID: 6307
		// (get) Token: 0x0600651A RID: 25882 RVA: 0x0017F51C File Offset: 0x0017D71C
		public ImmutableArray<MethodSymbol> InstanceConstructors
		{
			get
			{
				return this.GetConstructors(true, false);
			}
		}

		// Token: 0x170018A4 RID: 6308
		// (get) Token: 0x0600651B RID: 25883 RVA: 0x0017F526 File Offset: 0x0017D726
		public ImmutableArray<MethodSymbol> StaticConstructors
		{
			get
			{
				return this.GetConstructors(false, true);
			}
		}

		// Token: 0x170018A5 RID: 6309
		// (get) Token: 0x0600651C RID: 25884 RVA: 0x0017F530 File Offset: 0x0017D730
		public ImmutableArray<MethodSymbol> Constructors
		{
			get
			{
				return this.GetConstructors(true, true);
			}
		}

		// Token: 0x0600651D RID: 25885 RVA: 0x0017F53C File Offset: 0x0017D73C
		private ImmutableArray<MethodSymbol> GetConstructors(bool includeInstance, bool includeStatic)
		{
			ImmutableArray<Symbol> immutableArray = (includeInstance ? this.GetMembers(".ctor") : ImmutableArray<Symbol>.Empty);
			ImmutableArray<Symbol> immutableArray2 = (includeStatic ? this.GetMembers(".cctor") : ImmutableArray<Symbol>.Empty);
			if (immutableArray.IsEmpty && immutableArray2.IsEmpty)
			{
				return ImmutableArray<MethodSymbol>.Empty;
			}
			ArrayBuilder<MethodSymbol> instance = ArrayBuilder<MethodSymbol>.GetInstance();
			foreach (Symbol symbol in immutableArray)
			{
				MethodSymbol methodSymbol = symbol as MethodSymbol;
				if (methodSymbol != null)
				{
					instance.Add(methodSymbol);
				}
			}
			foreach (Symbol symbol2 in immutableArray2)
			{
				MethodSymbol methodSymbol2 = symbol2 as MethodSymbol;
				if (methodSymbol2 != null)
				{
					instance.Add(methodSymbol2);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x170018A6 RID: 6310
		// (get) Token: 0x0600651E RID: 25886 RVA: 0x0017F5F4 File Offset: 0x0017D7F4
		public ImmutableArray<PropertySymbol> Indexers
		{
			get
			{
				ImmutableArray<Symbol> simpleNonTypeMembers = this.GetSimpleNonTypeMembers("this[]");
				if (simpleNonTypeMembers.IsEmpty)
				{
					return ImmutableArray<PropertySymbol>.Empty;
				}
				ArrayBuilder<PropertySymbol> instance = ArrayBuilder<PropertySymbol>.GetInstance();
				foreach (Symbol symbol in simpleNonTypeMembers)
				{
					if (symbol.Kind == SymbolKind.Property)
					{
						instance.Add((PropertySymbol)symbol);
					}
				}
				return instance.ToImmutableAndFree();
			}
		}

		// Token: 0x170018A7 RID: 6311
		// (get) Token: 0x0600651F RID: 25887
		public abstract bool MightContainExtensionMethods { get; }

		// Token: 0x06006520 RID: 25888 RVA: 0x0017F659 File Offset: 0x0017D859
		internal void GetExtensionMethods(ArrayBuilder<MethodSymbol> methods, string nameOpt, int arity, LookupOptions options)
		{
			if (this.MightContainExtensionMethods)
			{
				this.DoGetExtensionMethods(methods, nameOpt, arity, options);
			}
		}

		// Token: 0x06006521 RID: 25889 RVA: 0x0017F670 File Offset: 0x0017D870
		internal void DoGetExtensionMethods(ArrayBuilder<MethodSymbol> methods, string nameOpt, int arity, LookupOptions options)
		{
			foreach (Symbol symbol in ((nameOpt == null) ? this.GetMembersUnordered() : this.GetSimpleNonTypeMembers(nameOpt)))
			{
				if (symbol.Kind == SymbolKind.Method)
				{
					MethodSymbol methodSymbol = (MethodSymbol)symbol;
					if (methodSymbol.IsExtensionMethod && ((options & LookupOptions.AllMethodsOnArityZero) != LookupOptions.Default || arity == methodSymbol.Arity))
					{
						ParameterSymbol parameterSymbol = methodSymbol.Parameters.First<ParameterSymbol>();
						if ((parameterSymbol.RefKind != RefKind.Ref || parameterSymbol.Type.IsValueType) && (parameterSymbol.RefKind != RefKind.In || parameterSymbol.Type.TypeKind == TypeKind.Struct))
						{
							methods.Add(methodSymbol);
						}
					}
				}
			}
		}

		// Token: 0x170018A8 RID: 6312
		// (get) Token: 0x06006522 RID: 25890 RVA: 0x0017F720 File Offset: 0x0017D920
		public override bool IsReferenceType
		{
			get
			{
				TypeKind typeKind = this.TypeKind;
				return typeKind != TypeKind.Enum && typeKind != TypeKind.Struct && typeKind != TypeKind.Error;
			}
		}

		// Token: 0x170018A9 RID: 6313
		// (get) Token: 0x06006523 RID: 25891 RVA: 0x0017F748 File Offset: 0x0017D948
		public override bool IsValueType
		{
			get
			{
				TypeKind typeKind = this.TypeKind;
				return typeKind == TypeKind.Struct || typeKind == TypeKind.Enum;
			}
		}

		// Token: 0x06006524 RID: 25892 RVA: 0x0017F767 File Offset: 0x0017D967
		internal override ManagedKind GetManagedKind(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return BaseTypeAnalysis.GetManagedKind(this, ref useSiteInfo);
		}

		// Token: 0x06006525 RID: 25893
		internal abstract AttributeUsageInfo GetAttributeUsageInfo();

		// Token: 0x170018AA RID: 6314
		// (get) Token: 0x06006526 RID: 25894 RVA: 0x0017F770 File Offset: 0x0017D970
		public virtual bool IsScriptClass
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170018AB RID: 6315
		// (get) Token: 0x06006527 RID: 25895 RVA: 0x0017F773 File Offset: 0x0017D973
		internal bool IsSubmissionClass
		{
			get
			{
				return this.TypeKind == TypeKind.Submission;
			}
		}

		// Token: 0x06006528 RID: 25896 RVA: 0x0017F77F File Offset: 0x0017D97F
		internal SynthesizedInstanceConstructor GetScriptConstructor()
		{
			return (SynthesizedInstanceConstructor)this.InstanceConstructors.Single<MethodSymbol>();
		}

		// Token: 0x06006529 RID: 25897 RVA: 0x0017F791 File Offset: 0x0017D991
		internal SynthesizedInteractiveInitializerMethod GetScriptInitializer()
		{
			return (SynthesizedInteractiveInitializerMethod)this.GetMembers("<Initialize>").Single<Symbol>();
		}

		// Token: 0x0600652A RID: 25898 RVA: 0x0017F7A8 File Offset: 0x0017D9A8
		internal SynthesizedEntryPointSymbol GetScriptEntryPoint()
		{
			string text = ((this.TypeKind == TypeKind.Submission) ? "<Factory>" : "<Main>");
			return (SynthesizedEntryPointSymbol)this.GetMembers(text).Single<Symbol>();
		}

		// Token: 0x170018AC RID: 6316
		// (get) Token: 0x0600652B RID: 25899 RVA: 0x0017F7DD File Offset: 0x0017D9DD
		public virtual bool IsImplicitClass
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170018AD RID: 6317
		// (get) Token: 0x0600652C RID: 25900
		public abstract override string Name { get; }

		// Token: 0x170018AE RID: 6318
		// (get) Token: 0x0600652D RID: 25901 RVA: 0x0017F7E0 File Offset: 0x0017D9E0
		public override string MetadataName
		{
			get
			{
				if (!this.MangleName)
				{
					return this.Name;
				}
				return MetadataHelpers.ComposeAritySuffixedMetadataName(this.Name, this.Arity);
			}
		}

		// Token: 0x170018AF RID: 6319
		// (get) Token: 0x0600652E RID: 25902
		internal abstract bool MangleName { get; }

		// Token: 0x170018B0 RID: 6320
		// (get) Token: 0x0600652F RID: 25903
		public abstract IEnumerable<string> MemberNames { get; }

		// Token: 0x06006530 RID: 25904
		public abstract override ImmutableArray<Symbol> GetMembers();

		// Token: 0x06006531 RID: 25905
		public abstract override ImmutableArray<Symbol> GetMembers(string name);

		// Token: 0x06006532 RID: 25906
		internal abstract bool HasPossibleWellKnownCloneMethod();

		// Token: 0x06006533 RID: 25907 RVA: 0x0017F802 File Offset: 0x0017DA02
		internal virtual ImmutableArray<Symbol> GetSimpleNonTypeMembers(string name)
		{
			return this.GetMembers(name);
		}

		// Token: 0x06006534 RID: 25908
		public abstract override ImmutableArray<NamedTypeSymbol> GetTypeMembers();

		// Token: 0x06006535 RID: 25909
		public abstract override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name);

		// Token: 0x06006536 RID: 25910
		public abstract override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity);

		// Token: 0x06006537 RID: 25911 RVA: 0x0017F80B File Offset: 0x0017DA0B
		internal virtual IEnumerable<Symbol> GetInstanceFieldsAndEvents()
		{
			return this.GetMembersUnordered().Where<Symbol>(NamedTypeSymbol.IsInstanceFieldOrEvent);
		}

		// Token: 0x170018B1 RID: 6321
		// (get) Token: 0x06006538 RID: 25912
		public abstract override Accessibility DeclaredAccessibility { get; }

		// Token: 0x06006539 RID: 25913 RVA: 0x0017F81D File Offset: 0x0017DA1D
		internal override TResult Accept<TArgument, TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument argument)
		{
			return visitor.VisitNamedType(this, argument);
		}

		// Token: 0x0600653A RID: 25914 RVA: 0x0017F827 File Offset: 0x0017DA27
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitNamedType(this);
		}

		// Token: 0x0600653B RID: 25915 RVA: 0x0017F830 File Offset: 0x0017DA30
		public override TResult Accept<TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitNamedType(this);
		}

		// Token: 0x0600653C RID: 25916
		internal abstract ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers();

		// Token: 0x0600653D RID: 25917
		internal abstract ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers(string name);

		// Token: 0x170018B2 RID: 6322
		// (get) Token: 0x0600653E RID: 25918 RVA: 0x0017F839 File Offset: 0x0017DA39
		public override SymbolKind Kind
		{
			get
			{
				return SymbolKind.NamedType;
			}
		}

		// Token: 0x0600653F RID: 25919
		internal abstract NamedTypeSymbol GetDeclaredBaseType(ConsList<TypeSymbol> basesBeingResolved);

		// Token: 0x06006540 RID: 25920
		internal abstract ImmutableArray<NamedTypeSymbol> GetDeclaredInterfaces(ConsList<TypeSymbol> basesBeingResolved);

		// Token: 0x06006541 RID: 25921 RVA: 0x0017F83D File Offset: 0x0017DA3D
		public override int GetHashCode()
		{
			if (this.SpecialType == SpecialType.System_Object)
			{
				return 1;
			}
			return RuntimeHelpers.GetHashCode(this.OriginalDefinition);
		}

		// Token: 0x06006542 RID: 25922 RVA: 0x0017F858 File Offset: 0x0017DA58
		internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
		{
			if (t2 == this)
			{
				return true;
			}
			if (t2 == null)
			{
				return false;
			}
			if ((comparison & TypeCompareKind.IgnoreDynamic) != TypeCompareKind.ConsiderEverything && t2.TypeKind == TypeKind.Dynamic && this.SpecialType == SpecialType.System_Object)
			{
				return true;
			}
			NamedTypeSymbol namedTypeSymbol = t2 as NamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				return false;
			}
			NamedTypeSymbol originalDefinition = this.OriginalDefinition;
			NamedTypeSymbol originalDefinition2 = namedTypeSymbol.OriginalDefinition;
			bool flag = this == originalDefinition;
			bool flag2 = namedTypeSymbol == originalDefinition2;
			return (!flag || !flag2) && ((!flag && !flag2) || (comparison & (TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes | TypeCompareKind.ObliviousNullableModifierMatchesAny)) != TypeCompareKind.ConsiderEverything) && TypeSymbol.Equals(originalDefinition, originalDefinition2, comparison) && this.EqualsComplicatedCases(namedTypeSymbol, comparison);
		}

		// Token: 0x06006543 RID: 25923 RVA: 0x0017F8D8 File Offset: 0x0017DAD8
		private bool EqualsComplicatedCases(NamedTypeSymbol other, TypeCompareKind comparison)
		{
			if (this.ContainingType != null && !this.ContainingType.Equals(other.ContainingType, comparison))
			{
				return false;
			}
			bool flag = this.ConstructedFrom == this;
			bool flag2 = other.ConstructedFrom == other;
			if (flag && flag2)
			{
				return true;
			}
			if (this.IsUnboundGenericType != other.IsUnboundGenericType)
			{
				return false;
			}
			if ((flag || flag2) && (comparison & (TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes | TypeCompareKind.ObliviousNullableModifierMatchesAny)) == TypeCompareKind.ConsiderEverything)
			{
				return false;
			}
			ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotationsNoUseSiteDiagnostics = this.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics;
			ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotationsNoUseSiteDiagnostics2 = other.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics;
			int length = typeArgumentsWithAnnotationsNoUseSiteDiagnostics.Length;
			for (int i = 0; i < length; i++)
			{
				TypeWithAnnotations typeWithAnnotations = typeArgumentsWithAnnotationsNoUseSiteDiagnostics[i];
				TypeWithAnnotations typeWithAnnotations2 = typeArgumentsWithAnnotationsNoUseSiteDiagnostics2[i];
				if (!typeWithAnnotations.Equals(typeWithAnnotations2, comparison))
				{
					return false;
				}
			}
			return !this.IsTupleType || this.<EqualsComplicatedCases>g__tupleNamesEquals|195_0(other, comparison);
		}

		// Token: 0x06006544 RID: 25924 RVA: 0x0017F99C File Offset: 0x0017DB9C
		internal override void AddNullableTransforms(ArrayBuilder<byte> transforms)
		{
			NamedTypeSymbol containingType = this.ContainingType;
			if (containingType != null)
			{
				containingType.AddNullableTransforms(transforms);
			}
			foreach (TypeWithAnnotations typeWithAnnotations in this.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics)
			{
				typeWithAnnotations.AddNullableTransforms(transforms);
			}
		}

		// Token: 0x06006545 RID: 25925 RVA: 0x0017F9E8 File Offset: 0x0017DBE8
		internal override bool ApplyNullableTransforms(byte defaultTransformFlag, ImmutableArray<byte> transforms, ref int position, out TypeSymbol result)
		{
			if (!this.IsGenericType)
			{
				result = this;
				return true;
			}
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			this.GetAllTypeArgumentsNoUseSiteDiagnostics(instance);
			bool flag = false;
			for (int i = 0; i < instance.Count; i++)
			{
				TypeWithAnnotations typeWithAnnotations = instance[i];
				TypeWithAnnotations typeWithAnnotations2;
				if (!typeWithAnnotations.ApplyNullableTransforms(defaultTransformFlag, transforms, ref position, out typeWithAnnotations2))
				{
					instance.Free();
					result = this;
					return false;
				}
				if (!typeWithAnnotations.IsSameAs(typeWithAnnotations2))
				{
					instance[i] = typeWithAnnotations2;
					flag = true;
				}
			}
			result = (flag ? this.WithTypeArguments(instance.ToImmutable()) : this);
			instance.Free();
			return true;
		}

		// Token: 0x06006546 RID: 25926 RVA: 0x0017FA78 File Offset: 0x0017DC78
		internal override TypeSymbol SetNullabilityForReferenceTypes(Func<TypeWithAnnotations, TypeWithAnnotations> transform)
		{
			if (!this.IsGenericType)
			{
				return this;
			}
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			this.GetAllTypeArgumentsNoUseSiteDiagnostics(instance);
			bool flag = false;
			for (int i = 0; i < instance.Count; i++)
			{
				TypeWithAnnotations typeWithAnnotations = instance[i];
				TypeWithAnnotations typeWithAnnotations2 = transform.Invoke(typeWithAnnotations);
				if (!typeWithAnnotations.IsSameAs(typeWithAnnotations2))
				{
					instance[i] = typeWithAnnotations2;
					flag = true;
				}
			}
			TypeSymbol typeSymbol = (flag ? this.WithTypeArguments(instance.ToImmutable()) : this);
			instance.Free();
			return typeSymbol;
		}

		// Token: 0x06006547 RID: 25927 RVA: 0x0017FAF0 File Offset: 0x0017DCF0
		internal NamedTypeSymbol WithTypeArguments(ImmutableArray<TypeWithAnnotations> allTypeArguments)
		{
			NamedTypeSymbol originalDefinition = this.OriginalDefinition;
			return new TypeMap(originalDefinition.GetAllTypeParameters(), allTypeArguments, false).SubstituteNamedType(originalDefinition).WithTupleDataFrom(this);
		}

		// Token: 0x06006548 RID: 25928 RVA: 0x0017FB20 File Offset: 0x0017DD20
		internal override TypeSymbol MergeEquivalentTypes(TypeSymbol other, VarianceKind variance)
		{
			if (!this.IsGenericType)
			{
				if (!other.IsDynamic())
				{
					return this;
				}
				return other;
			}
			else
			{
				ArrayBuilder<TypeParameterSymbol> instance = ArrayBuilder<TypeParameterSymbol>.GetInstance();
				ArrayBuilder<TypeWithAnnotations> instance2 = ArrayBuilder<TypeWithAnnotations>.GetInstance();
				NamedTypeSymbol namedTypeSymbol;
				if (NamedTypeSymbol.MergeEquivalentTypeArguments(this, (NamedTypeSymbol)other, variance, instance, instance2))
				{
					namedTypeSymbol = new TypeMap(instance.ToImmutable(), instance2.ToImmutable(), false).SubstituteNamedType(this.OriginalDefinition);
				}
				else
				{
					namedTypeSymbol = this;
				}
				instance2.Free();
				instance.Free();
				if (!this.IsTupleType)
				{
					return namedTypeSymbol;
				}
				return this.MergeTupleNames((NamedTypeSymbol)other, namedTypeSymbol);
			}
		}

		// Token: 0x06006549 RID: 25929 RVA: 0x0017FBA4 File Offset: 0x0017DDA4
		private static bool MergeEquivalentTypeArguments(NamedTypeSymbol typeA, NamedTypeSymbol typeB, VarianceKind variance, ArrayBuilder<TypeParameterSymbol> allTypeParameters, ArrayBuilder<TypeWithAnnotations> allTypeArguments)
		{
			bool isTupleType = typeA.IsTupleType;
			NamedTypeSymbol namedTypeSymbol = typeA.OriginalDefinition;
			bool flag = false;
			for (;;)
			{
				ImmutableArray<TypeParameterSymbol> typeParameters = namedTypeSymbol.TypeParameters;
				if (typeParameters.Length > 0)
				{
					ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotationsNoUseSiteDiagnostics = typeA.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics;
					ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotationsNoUseSiteDiagnostics2 = typeB.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics;
					allTypeParameters.AddRange(typeParameters);
					for (int i = 0; i < typeArgumentsWithAnnotationsNoUseSiteDiagnostics.Length; i++)
					{
						TypeWithAnnotations typeWithAnnotations = typeArgumentsWithAnnotationsNoUseSiteDiagnostics[i];
						TypeWithAnnotations typeWithAnnotations2 = typeArgumentsWithAnnotationsNoUseSiteDiagnostics2[i];
						VarianceKind typeArgumentVariance = NamedTypeSymbol.GetTypeArgumentVariance(variance, isTupleType ? VarianceKind.Out : typeParameters[i].Variance);
						TypeWithAnnotations typeWithAnnotations3 = typeWithAnnotations.MergeEquivalentTypes(typeWithAnnotations2, typeArgumentVariance);
						allTypeArguments.Add(typeWithAnnotations3);
						if (!typeWithAnnotations.IsSameAs(typeWithAnnotations3))
						{
							flag = true;
						}
					}
				}
				namedTypeSymbol = namedTypeSymbol.ContainingType;
				if (namedTypeSymbol == null)
				{
					break;
				}
				typeA = typeA.ContainingType;
				typeB = typeB.ContainingType;
				variance = VarianceKind.None;
			}
			return flag;
		}

		// Token: 0x0600654A RID: 25930 RVA: 0x0017FC7A File Offset: 0x0017DE7A
		private static VarianceKind GetTypeArgumentVariance(VarianceKind typeVariance, VarianceKind typeParameterVariance)
		{
			if (typeVariance == VarianceKind.Out)
			{
				return typeParameterVariance;
			}
			if (typeVariance != VarianceKind.In)
			{
				return VarianceKind.None;
			}
			if (typeParameterVariance == VarianceKind.Out)
			{
				return VarianceKind.In;
			}
			if (typeParameterVariance == VarianceKind.In)
			{
				return VarianceKind.Out;
			}
			return VarianceKind.None;
		}

		// Token: 0x0600654B RID: 25931 RVA: 0x0017FC95 File Offset: 0x0017DE95
		public NamedTypeSymbol Construct(params TypeSymbol[] typeArguments)
		{
			return this.ConstructWithoutModifiers(typeArguments.AsImmutableOrNull<TypeSymbol>(), false);
		}

		// Token: 0x0600654C RID: 25932 RVA: 0x0017FCA4 File Offset: 0x0017DEA4
		public NamedTypeSymbol Construct(ImmutableArray<TypeSymbol> typeArguments)
		{
			return this.ConstructWithoutModifiers(typeArguments, false);
		}

		// Token: 0x0600654D RID: 25933 RVA: 0x0017FCAE File Offset: 0x0017DEAE
		public NamedTypeSymbol Construct(IEnumerable<TypeSymbol> typeArguments)
		{
			return this.ConstructWithoutModifiers(typeArguments.AsImmutableOrNull<TypeSymbol>(), false);
		}

		// Token: 0x0600654E RID: 25934 RVA: 0x0017FCBD File Offset: 0x0017DEBD
		public NamedTypeSymbol ConstructUnboundGenericType()
		{
			return this.OriginalDefinition.AsUnboundGenericType();
		}

		// Token: 0x0600654F RID: 25935 RVA: 0x0017FCCA File Offset: 0x0017DECA
		internal NamedTypeSymbol GetUnboundGenericTypeOrSelf()
		{
			if (!this.IsGenericType)
			{
				return this;
			}
			return this.ConstructUnboundGenericType();
		}

		// Token: 0x170018B3 RID: 6323
		// (get) Token: 0x06006550 RID: 25936
		internal abstract bool HasCodeAnalysisEmbeddedAttribute { get; }

		// Token: 0x06006551 RID: 25937 RVA: 0x0017FCDC File Offset: 0x0017DEDC
		private NamedTypeSymbol ConstructWithoutModifiers(ImmutableArray<TypeSymbol> typeArguments, bool unbound)
		{
			ImmutableArray<TypeWithAnnotations> immutableArray;
			if (typeArguments.IsDefault)
			{
				immutableArray = default(ImmutableArray<TypeWithAnnotations>);
			}
			else
			{
				immutableArray = typeArguments.SelectAsArray<TypeSymbol, TypeWithAnnotations>((TypeSymbol t) => TypeWithAnnotations.Create(t, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
			}
			return this.Construct(immutableArray, unbound);
		}

		// Token: 0x06006552 RID: 25938 RVA: 0x0017FD2A File Offset: 0x0017DF2A
		internal NamedTypeSymbol Construct(ImmutableArray<TypeWithAnnotations> typeArguments)
		{
			return this.Construct(typeArguments, false);
		}

		// Token: 0x06006553 RID: 25939 RVA: 0x0017FD34 File Offset: 0x0017DF34
		internal NamedTypeSymbol Construct(ImmutableArray<TypeWithAnnotations> typeArguments, bool unbound)
		{
			if (this != this.ConstructedFrom)
			{
				throw new InvalidOperationException(CSharpResources.CannotCreateConstructedFromConstructed);
			}
			if (this.Arity == 0)
			{
				throw new InvalidOperationException(CSharpResources.CannotCreateConstructedFromNongeneric);
			}
			if (typeArguments.IsDefault)
			{
				throw new ArgumentNullException("typeArguments");
			}
			if (typeArguments.Any<TypeWithAnnotations>(NamedTypeSymbol.TypeWithAnnotationsIsNullFunction))
			{
				throw new ArgumentException(CSharpResources.TypeArgumentCannotBeNull, "typeArguments");
			}
			if (typeArguments.Length != this.Arity)
			{
				throw new ArgumentException(CSharpResources.WrongNumberOfTypeArguments, "typeArguments");
			}
			if (ConstructedNamedTypeSymbol.TypeParametersMatchTypeArguments(this.TypeParameters, typeArguments))
			{
				return this;
			}
			return this.ConstructCore(typeArguments, unbound);
		}

		// Token: 0x06006554 RID: 25940 RVA: 0x0017FDD0 File Offset: 0x0017DFD0
		protected virtual NamedTypeSymbol ConstructCore(ImmutableArray<TypeWithAnnotations> typeArguments, bool unbound)
		{
			return new ConstructedNamedTypeSymbol(this, typeArguments, unbound, null);
		}

		// Token: 0x170018B4 RID: 6324
		// (get) Token: 0x06006555 RID: 25941 RVA: 0x0017FDDC File Offset: 0x0017DFDC
		public bool IsGenericType
		{
			get
			{
				for (NamedTypeSymbol namedTypeSymbol = this; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.ContainingType)
				{
					if (namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics.Length != 0)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170018B5 RID: 6325
		// (get) Token: 0x06006556 RID: 25942 RVA: 0x0017FE0A File Offset: 0x0017E00A
		public virtual bool IsUnboundGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006557 RID: 25943 RVA: 0x0017FE10 File Offset: 0x0017E010
		internal void GetAllTypeArguments(ArrayBuilder<TypeSymbol> builder, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			NamedTypeSymbol containingType = this.ContainingType;
			if (containingType != null)
			{
				containingType.GetAllTypeArguments(builder, ref useSiteInfo);
			}
			foreach (TypeWithAnnotations typeWithAnnotations in this.TypeArgumentsWithDefinitionUseSiteDiagnostics(ref useSiteInfo))
			{
				builder.Add(typeWithAnnotations.Type);
			}
		}

		// Token: 0x06006558 RID: 25944 RVA: 0x0017FE60 File Offset: 0x0017E060
		internal ImmutableArray<TypeWithAnnotations> GetAllTypeArguments(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			this.GetAllTypeArguments(instance, ref useSiteInfo);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06006559 RID: 25945 RVA: 0x0017FE84 File Offset: 0x0017E084
		internal void GetAllTypeArguments(ArrayBuilder<TypeWithAnnotations> builder, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			NamedTypeSymbol containingType = this.ContainingType;
			if (containingType != null)
			{
				containingType.GetAllTypeArguments(builder, ref useSiteInfo);
			}
			builder.AddRange(this.TypeArgumentsWithDefinitionUseSiteDiagnostics(ref useSiteInfo));
		}

		// Token: 0x0600655A RID: 25946 RVA: 0x0017FEB0 File Offset: 0x0017E0B0
		internal void GetAllTypeArgumentsNoUseSiteDiagnostics(ArrayBuilder<TypeWithAnnotations> builder)
		{
			NamedTypeSymbol containingType = this.ContainingType;
			if (containingType != null)
			{
				containingType.GetAllTypeArgumentsNoUseSiteDiagnostics(builder);
			}
			builder.AddRange(this.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics);
		}

		// Token: 0x0600655B RID: 25947 RVA: 0x0017FED0 File Offset: 0x0017E0D0
		internal int AllTypeArgumentCount()
		{
			int num = this.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics.Length;
			NamedTypeSymbol containingType = this.ContainingType;
			if (containingType != null)
			{
				num += containingType.AllTypeArgumentCount();
			}
			return num;
		}

		// Token: 0x0600655C RID: 25948 RVA: 0x0017FF00 File Offset: 0x0017E100
		internal ImmutableArray<TypeWithAnnotations> GetTypeParametersAsTypeArguments()
		{
			return TypeMap.TypeParametersAsTypeSymbolsWithAnnotations(this.TypeParameters);
		}

		// Token: 0x170018B6 RID: 6326
		// (get) Token: 0x0600655D RID: 25949 RVA: 0x0017FF0D File Offset: 0x0017E10D
		public new virtual NamedTypeSymbol OriginalDefinition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170018B7 RID: 6327
		// (get) Token: 0x0600655E RID: 25950 RVA: 0x0017FF10 File Offset: 0x0017E110
		protected sealed override TypeSymbol OriginalTypeSymbolDefinition
		{
			get
			{
				return this.OriginalDefinition;
			}
		}

		// Token: 0x170018B8 RID: 6328
		// (get) Token: 0x0600655F RID: 25951 RVA: 0x0017FF18 File Offset: 0x0017E118
		internal virtual TypeMap TypeSubstitution
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006560 RID: 25952 RVA: 0x0017FF1B File Offset: 0x0017E11B
		internal virtual NamedTypeSymbol AsMember(NamedTypeSymbol newOwner)
		{
			if (!newOwner.IsDefinition)
			{
				return new SubstitutedNestedTypeSymbol((SubstitutedNamedTypeSymbol)newOwner, this);
			}
			return this;
		}

		// Token: 0x06006561 RID: 25953 RVA: 0x0017FF34 File Offset: 0x0017E134
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			UseSiteInfo<AssemblySymbol> useSiteInfo = new UseSiteInfo<AssemblySymbol>(base.PrimaryDependency);
			if (base.IsDefinition)
			{
				return useSiteInfo;
			}
			if (!base.DeriveUseSiteInfoFromType(ref useSiteInfo, this.OriginalDefinition))
			{
				this.DeriveUseSiteDiagnosticFromTypeArguments(ref useSiteInfo);
			}
			return useSiteInfo;
		}

		// Token: 0x06006562 RID: 25954 RVA: 0x0017FF74 File Offset: 0x0017E174
		private bool DeriveUseSiteDiagnosticFromTypeArguments(ref UseSiteInfo<AssemblySymbol> result)
		{
			NamedTypeSymbol namedTypeSymbol = this;
			for (;;)
			{
				foreach (TypeWithAnnotations typeWithAnnotations in namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics)
				{
					if (base.DeriveUseSiteInfoFromType(ref result, typeWithAnnotations, Symbol.AllowedRequiredModifierType.None))
					{
						return true;
					}
				}
				namedTypeSymbol = namedTypeSymbol.ContainingType;
				if (namedTypeSymbol == null || namedTypeSymbol.IsDefinition)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006563 RID: 25955 RVA: 0x0017FFC8 File Offset: 0x0017E1C8
		internal DiagnosticInfo CalculateUseSiteDiagnostic()
		{
			DiagnosticInfo diagnosticInfo = null;
			if (base.MergeUseSiteDiagnostics(ref diagnosticInfo, this.DeriveUseSiteDiagnosticFromBase()))
			{
				return diagnosticInfo;
			}
			if (this.ContainingModule.HasUnifiedReferences)
			{
				HashSet<TypeSymbol> hashSet = null;
				this.GetUnificationUseSiteDiagnosticRecursive(ref diagnosticInfo, this, ref hashSet);
				return diagnosticInfo;
			}
			return diagnosticInfo;
		}

		// Token: 0x06006564 RID: 25956 RVA: 0x00180008 File Offset: 0x0017E208
		private DiagnosticInfo DeriveUseSiteDiagnosticFromBase()
		{
			for (NamedTypeSymbol namedTypeSymbol = this.BaseTypeNoUseSiteDiagnostics; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.BaseTypeNoUseSiteDiagnostics)
			{
				if (namedTypeSymbol.IsErrorType() && namedTypeSymbol is NoPiaIllegalGenericInstantiationSymbol)
				{
					return namedTypeSymbol.GetUseSiteInfo().DiagnosticInfo;
				}
			}
			return null;
		}

		// Token: 0x06006565 RID: 25957 RVA: 0x00180048 File Offset: 0x0017E248
		internal override bool GetUnificationUseSiteDiagnosticRecursive(ref DiagnosticInfo result, Symbol owner, ref HashSet<TypeSymbol> checkedTypes)
		{
			if (!this.MarkCheckedIfNecessary(ref checkedTypes))
			{
				return false;
			}
			if (owner.ContainingModule.GetUnificationUseSiteDiagnostic(ref result, this))
			{
				return true;
			}
			NamedTypeSymbol baseTypeNoUseSiteDiagnostics = this.BaseTypeNoUseSiteDiagnostics;
			return (baseTypeNoUseSiteDiagnostics != null && baseTypeNoUseSiteDiagnostics.GetUnificationUseSiteDiagnosticRecursive(ref result, owner, ref checkedTypes)) || Symbol.GetUnificationUseSiteDiagnosticRecursive<NamedTypeSymbol>(ref result, this.InterfacesNoUseSiteDiagnostics(null), owner, ref checkedTypes) || Symbol.GetUnificationUseSiteDiagnosticRecursive(ref result, this.TypeParameters, owner, ref checkedTypes);
		}

		// Token: 0x170018B9 RID: 6329
		// (get) Token: 0x06006566 RID: 25958 RVA: 0x001800A9 File Offset: 0x0017E2A9
		internal virtual bool IsDirectlyExcludedFromCodeCoverage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170018BA RID: 6330
		// (get) Token: 0x06006567 RID: 25959
		internal abstract bool HasSpecialName { get; }

		// Token: 0x170018BB RID: 6331
		// (get) Token: 0x06006568 RID: 25960
		internal abstract bool IsComImport { get; }

		// Token: 0x170018BC RID: 6332
		// (get) Token: 0x06006569 RID: 25961
		internal abstract bool IsWindowsRuntimeImport { get; }

		// Token: 0x170018BD RID: 6333
		// (get) Token: 0x0600656A RID: 25962
		internal abstract bool ShouldAddWinRTMembers { get; }

		// Token: 0x170018BE RID: 6334
		// (get) Token: 0x0600656B RID: 25963 RVA: 0x001800AC File Offset: 0x0017E2AC
		internal bool IsConditional
		{
			get
			{
				if (this.GetAppliedConditionalSymbols().Any<string>())
				{
					return true;
				}
				NamedTypeSymbol baseTypeNoUseSiteDiagnostics = this.BaseTypeNoUseSiteDiagnostics;
				return baseTypeNoUseSiteDiagnostics != null && baseTypeNoUseSiteDiagnostics.IsConditional;
			}
		}

		// Token: 0x170018BF RID: 6335
		// (get) Token: 0x0600656C RID: 25964
		public abstract bool IsSerializable { get; }

		// Token: 0x170018C0 RID: 6336
		// (get) Token: 0x0600656D RID: 25965
		public abstract bool AreLocalsZeroed { get; }

		// Token: 0x170018C1 RID: 6337
		// (get) Token: 0x0600656E RID: 25966
		internal abstract TypeLayout Layout { get; }

		// Token: 0x170018C2 RID: 6338
		// (get) Token: 0x0600656F RID: 25967 RVA: 0x001800DC File Offset: 0x0017E2DC
		protected CharSet DefaultMarshallingCharSet
		{
			get
			{
				CharSet? effectiveDefaultMarshallingCharSet = base.GetEffectiveDefaultMarshallingCharSet();
				if (effectiveDefaultMarshallingCharSet == null)
				{
					return 2;
				}
				return effectiveDefaultMarshallingCharSet.GetValueOrDefault();
			}
		}

		// Token: 0x170018C3 RID: 6339
		// (get) Token: 0x06006570 RID: 25968
		internal abstract CharSet MarshallingCharSet { get; }

		// Token: 0x170018C4 RID: 6340
		// (get) Token: 0x06006571 RID: 25969
		internal abstract bool HasDeclarativeSecurity { get; }

		// Token: 0x06006572 RID: 25970
		internal abstract IEnumerable<SecurityAttribute> GetSecurityInformation();

		// Token: 0x06006573 RID: 25971
		internal abstract ImmutableArray<string> GetAppliedConditionalSymbols();

		// Token: 0x170018C5 RID: 6341
		// (get) Token: 0x06006574 RID: 25972 RVA: 0x00180102 File Offset: 0x0017E302
		internal virtual NamedTypeSymbol ComImportCoClass
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170018C6 RID: 6342
		// (get) Token: 0x06006575 RID: 25973 RVA: 0x00180105 File Offset: 0x0017E305
		internal virtual FieldSymbol FixedElementField
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170018C7 RID: 6343
		// (get) Token: 0x06006576 RID: 25974
		internal abstract bool IsInterface { get; }

		// Token: 0x06006577 RID: 25975 RVA: 0x00180108 File Offset: 0x0017E308
		internal bool IsTupleTypeOfCardinality(out int tupleCardinality)
		{
			if (!this.IsUnboundGenericType)
			{
				Symbol containingSymbol = this.ContainingSymbol;
				if (containingSymbol != null && containingSymbol.Kind == SymbolKind.Namespace)
				{
					NamespaceSymbol containingNamespace = this.ContainingNamespace.ContainingNamespace;
					if (containingNamespace != null && containingNamespace.IsGlobalNamespace && this.Name == "ValueTuple" && this.ContainingNamespace.Name == "System")
					{
						int num = this.Arity;
						if (num >= 0 && num < 8)
						{
							tupleCardinality = num;
							return true;
						}
						if (num == 8 && !base.IsDefinition)
						{
							TypeSymbol typeSymbol = this;
							int num2 = 0;
							do
							{
								num2++;
								typeSymbol = ((NamedTypeSymbol)typeSymbol).TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[7].Type;
							}
							while (TypeSymbol.Equals(typeSymbol.OriginalDefinition, this.OriginalDefinition, TypeCompareKind.ConsiderEverything) && !typeSymbol.IsDefinition);
							NamedTypeSymbol namedTypeSymbol = typeSymbol as NamedTypeSymbol;
							num = ((namedTypeSymbol != null) ? namedTypeSymbol.Arity : 0);
							if (num > 0 && num < 8 && ((NamedTypeSymbol)typeSymbol).IsTupleTypeOfCardinality(out tupleCardinality))
							{
								tupleCardinality += 7 * num2;
								return true;
							}
						}
					}
				}
			}
			tupleCardinality = 0;
			return false;
		}

		// Token: 0x06006578 RID: 25976
		internal abstract NamedTypeSymbol AsNativeInteger();

		// Token: 0x170018C8 RID: 6344
		// (get) Token: 0x06006579 RID: 25977
		internal abstract NamedTypeSymbol NativeIntegerUnderlyingType { get; }

		// Token: 0x0600657A RID: 25978 RVA: 0x0018021F File Offset: 0x0017E41F
		protected override ISymbol CreateISymbol()
		{
			return new NonErrorNamedTypeSymbol(this, base.DefaultNullableAnnotation);
		}

		// Token: 0x0600657B RID: 25979 RVA: 0x0018022D File Offset: 0x0017E42D
		protected override ITypeSymbol CreateITypeSymbol(NullableAnnotation nullableAnnotation)
		{
			return new NonErrorNamedTypeSymbol(this, nullableAnnotation);
		}

		// Token: 0x170018C9 RID: 6345
		// (get) Token: 0x0600657C RID: 25980 RVA: 0x00180236 File Offset: 0x0017E436
		INamedTypeSymbolInternal INamedTypeSymbolInternal.EnumUnderlyingType
		{
			get
			{
				return this.EnumUnderlyingType;
			}
		}

		// Token: 0x0600657D RID: 25981 RVA: 0x00180240 File Offset: 0x0017E440
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		internal static NamedTypeSymbol CreateTuple(Location locationOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<TypeWithAnnotations> elementTypesWithAnnotations, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<Location> elementLocations, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<string> elementNames, [global::System.Runtime.CompilerServices.Nullable(1)] CSharpCompilation compilation, bool shouldCheckConstraints, bool includeNullability, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<bool> errorPositions, CSharpSyntaxNode syntax = null, BindingDiagnosticBag diagnostics = null)
		{
			int length = elementTypesWithAnnotations.Length;
			if (length <= 1)
			{
				throw ExceptionUtilities.Unreachable;
			}
			NamedTypeSymbol namedTypeSymbol = NamedTypeSymbol.<CreateTuple>g__getTupleUnderlyingType|283_0(elementTypesWithAnnotations, syntax, compilation, diagnostics);
			if (length >= 8 && diagnostics != null && !namedTypeSymbol.IsErrorType())
			{
				WellKnownMember tupleTypeMember = NamedTypeSymbol.GetTupleTypeMember(8, 8);
				NamedTypeSymbol.GetWellKnownMemberInType(namedTypeSymbol.OriginalDefinition, tupleTypeMember, diagnostics, syntax);
			}
			if (diagnostics != null && diagnostics.DiagnosticBag != null && ((SourceModuleSymbol)compilation.SourceModule).AnyReferencedAssembliesAreLinked)
			{
				EmbeddedTypesManager.IsValidEmbeddableType(namedTypeSymbol, syntax, diagnostics.DiagnosticBag, null);
			}
			ImmutableArray<Location> immutableArray = ((locationOpt == null) ? ImmutableArray<Location>.Empty : ImmutableArray.Create<Location>(locationOpt));
			NamedTypeSymbol namedTypeSymbol2 = NamedTypeSymbol.CreateTuple(namedTypeSymbol, elementNames, errorPositions, elementLocations, immutableArray);
			if (shouldCheckConstraints && diagnostics != null)
			{
				NamedTypeSymbol namedTypeSymbol3 = namedTypeSymbol2;
				ConstraintsHelper.CheckConstraintsArgs checkConstraintsArgs = new ConstraintsHelper.CheckConstraintsArgs(compilation, compilation.Conversions, includeNullability, syntax.Location, diagnostics);
				namedTypeSymbol3.CheckConstraints(in checkConstraintsArgs, syntax, elementLocations, includeNullability ? diagnostics : null);
			}
			return namedTypeSymbol2;
		}

		// Token: 0x0600657E RID: 25982 RVA: 0x00180319 File Offset: 0x0017E519
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static NamedTypeSymbol CreateTuple(NamedTypeSymbol tupleCompatibleType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<string> elementNames = default(ImmutableArray<string>), [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<bool> errorPositions = default(ImmutableArray<bool>), [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<Location> elementLocations = default(ImmutableArray<Location>), [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Location> locations = default(ImmutableArray<Location>))
		{
			return tupleCompatibleType.WithElementNames(elementNames, elementLocations, errorPositions, locations);
		}

		// Token: 0x0600657F RID: 25983 RVA: 0x00180328 File Offset: 0x0017E528
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal NamedTypeSymbol WithTupleDataFrom(NamedTypeSymbol original)
		{
			if (!this.IsTupleType || (original._lazyTupleData == null && this._lazyTupleData == null) || this.TupleData.EqualsIgnoringTupleUnderlyingType(original.TupleData))
			{
				return this;
			}
			return this.WithElementNames(original.TupleElementNames, original.TupleElementLocations, original.TupleErrorPositions, original.Locations);
		}

		// Token: 0x170018CA RID: 6346
		// (get) Token: 0x06006580 RID: 25984 RVA: 0x00180380 File Offset: 0x0017E580
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal NamedTypeSymbol TupleUnderlyingType
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				if (this._lazyTupleData != null)
				{
					return this.TupleData.TupleUnderlyingType;
				}
				if (!this.IsTupleType)
				{
					return null;
				}
				return this;
			}
		}

		// Token: 0x06006581 RID: 25985 RVA: 0x001803A4 File Offset: 0x0017E5A4
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		internal NamedTypeSymbol WithElementTypes(ImmutableArray<TypeWithAnnotations> newElementTypes)
		{
			NamedTypeSymbol namedTypeSymbol;
			NamedTypeSymbol namedTypeSymbol2;
			if (this.Arity < 8)
			{
				namedTypeSymbol = this.OriginalDefinition;
				namedTypeSymbol2 = null;
			}
			else
			{
				namedTypeSymbol2 = this.OriginalDefinition;
				NamedTypeSymbol namedTypeSymbol3 = this;
				do
				{
					namedTypeSymbol3 = (NamedTypeSymbol)namedTypeSymbol3.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[7].Type;
				}
				while (namedTypeSymbol3.Arity >= 8);
				namedTypeSymbol = namedTypeSymbol3.OriginalDefinition;
			}
			NamedTypeSymbol namedTypeSymbol4 = NamedTypeSymbol.ConstructTupleUnderlyingType(namedTypeSymbol, namedTypeSymbol2, newElementTypes);
			ImmutableArray<string> tupleElementNames = this.TupleElementNames;
			ImmutableArray<Location> tupleElementLocations = this.TupleElementLocations;
			return NamedTypeSymbol.CreateTuple(namedTypeSymbol4, tupleElementNames, this.TupleErrorPositions, tupleElementLocations, this.Locations);
		}

		// Token: 0x06006582 RID: 25986 RVA: 0x00180425 File Offset: 0x0017E625
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		internal NamedTypeSymbol WithElementNames([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<string> newElementNames, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<Location> newElementLocations, ImmutableArray<bool> errorPositions, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Location> locations)
		{
			return this.WithTupleData(new NamedTypeSymbol.TupleExtraData(this.TupleUnderlyingType, newElementNames, newElementLocations, errorPositions, locations));
		}

		// Token: 0x06006583 RID: 25987 RVA: 0x00180440 File Offset: 0x0017E640
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private NamedTypeSymbol WithTupleData(NamedTypeSymbol.TupleExtraData newData)
		{
			if (newData.EqualsIgnoringTupleUnderlyingType(this.TupleData))
			{
				return this;
			}
			if (!base.IsDefinition)
			{
				return this.WithTupleDataCore(newData);
			}
			if (newData.ElementNames.IsDefault)
			{
				return this;
			}
			return this.ConstructCore(this.GetTypeParametersAsTypeArguments(), false).WithTupleData(newData);
		}

		// Token: 0x06006584 RID: 25988
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected abstract NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData);

		// Token: 0x06006585 RID: 25989 RVA: 0x00180494 File Offset: 0x0017E694
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static void GetUnderlyingTypeChain(NamedTypeSymbol underlyingTupleType, ArrayBuilder<NamedTypeSymbol> underlyingTupleTypeChain)
		{
			NamedTypeSymbol namedTypeSymbol = underlyingTupleType;
			for (;;)
			{
				underlyingTupleTypeChain.Add(namedTypeSymbol);
				if (namedTypeSymbol.Arity != 8)
				{
					break;
				}
				namedTypeSymbol = (NamedTypeSymbol)namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[7].Type;
			}
		}

		// Token: 0x06006586 RID: 25990 RVA: 0x001804D2 File Offset: 0x0017E6D2
		private static int NumberOfValueTuples(int numElements, out int remainder)
		{
			remainder = (numElements - 1) % 7 + 1;
			return (numElements - 1) / 7 + 1;
		}

		// Token: 0x06006587 RID: 25991 RVA: 0x001804E4 File Offset: 0x0017E6E4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private static NamedTypeSymbol ConstructTupleUnderlyingType(NamedTypeSymbol firstTupleType, [global::System.Runtime.CompilerServices.Nullable(2)] NamedTypeSymbol chainedTupleTypeOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<TypeWithAnnotations> elementTypes)
		{
			int num2;
			int num = NamedTypeSymbol.NumberOfValueTuples(elementTypes.Length, out num2);
			NamedTypeSymbol namedTypeSymbol = firstTupleType.Construct(ImmutableArray.Create<TypeWithAnnotations>(elementTypes, (num - 1) * 7, num2));
			for (int i = num - 1; i > 0; i--)
			{
				ImmutableArray<TypeWithAnnotations> immutableArray = ImmutableArray.Create<TypeWithAnnotations>(elementTypes, (i - 1) * 7, 7).Add(TypeWithAnnotations.Create(namedTypeSymbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
				namedTypeSymbol = chainedTupleTypeOpt.Construct(immutableArray);
			}
			return namedTypeSymbol;
		}

		// Token: 0x06006588 RID: 25992 RVA: 0x00180553 File Offset: 0x0017E753
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private static void ReportUseSiteAndObsoleteDiagnostics([global::System.Runtime.CompilerServices.Nullable(2)] CSharpSyntaxNode syntax, BindingDiagnosticBag diagnostics, NamedTypeSymbol firstTupleType)
		{
			Binder.ReportUseSite(firstTupleType, diagnostics, syntax);
			Binder.ReportDiagnosticsIfObsoleteInternal(diagnostics, firstTupleType, syntax, firstTupleType.ContainingType, BinderFlags.None);
		}

		// Token: 0x06006589 RID: 25993 RVA: 0x00180574 File Offset: 0x0017E774
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static void VerifyTupleTypePresent(int cardinality, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpSyntaxNode syntax, CSharpCompilation compilation, BindingDiagnosticBag diagnostics)
		{
			int num2;
			int num = NamedTypeSymbol.NumberOfValueTuples(cardinality, out num2);
			NamedTypeSymbol wellKnownType = compilation.GetWellKnownType(NamedTypeSymbol.GetTupleType(num2));
			NamedTypeSymbol.ReportUseSiteAndObsoleteDiagnostics(syntax, diagnostics, wellKnownType);
			if (num > 1)
			{
				NamedTypeSymbol wellKnownType2 = compilation.GetWellKnownType(NamedTypeSymbol.GetTupleType(8));
				NamedTypeSymbol.ReportUseSiteAndObsoleteDiagnostics(syntax, diagnostics, wellKnownType2);
			}
		}

		// Token: 0x0600658A RID: 25994 RVA: 0x001805B8 File Offset: 0x0017E7B8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static void ReportTupleNamesMismatchesIfAny(TypeSymbol destination, BoundTupleLiteral literal, BindingDiagnosticBag diagnostics)
		{
			ImmutableArray<string> argumentNamesOpt = literal.ArgumentNamesOpt;
			if (argumentNamesOpt.IsDefault)
			{
				return;
			}
			ImmutableArray<bool> inferredNamesOpt = literal.InferredNamesOpt;
			bool isDefault = inferredNamesOpt.IsDefault;
			ImmutableArray<string> tupleElementNames = destination.TupleElementNames;
			int length = argumentNamesOpt.Length;
			bool isDefault2 = tupleElementNames.IsDefault;
			for (int i = 0; i < length; i++)
			{
				string text = argumentNamesOpt[i];
				bool flag = !isDefault && inferredNamesOpt[i];
				if (text != null && !flag && (isDefault2 || string.CompareOrdinal(tupleElementNames[i], text) != 0))
				{
					diagnostics.Add(ErrorCode.WRN_TupleLiteralNameMismatch, literal.Arguments[i].Syntax.Parent.Location, new object[] { text, destination });
				}
			}
		}

		// Token: 0x0600658B RID: 25995 RVA: 0x00180687 File Offset: 0x0017E887
		private static WellKnownType GetTupleType(int arity)
		{
			if (arity > 8)
			{
				throw ExceptionUtilities.Unreachable;
			}
			return NamedTypeSymbol.tupleTypes[arity - 1];
		}

		// Token: 0x0600658C RID: 25996 RVA: 0x0018069C File Offset: 0x0017E89C
		internal static WellKnownMember GetTupleCtor(int arity)
		{
			if (arity > 8)
			{
				throw ExceptionUtilities.Unreachable;
			}
			return NamedTypeSymbol.tupleCtors[arity - 1];
		}

		// Token: 0x0600658D RID: 25997 RVA: 0x001806B1 File Offset: 0x0017E8B1
		internal static WellKnownMember GetTupleTypeMember(int arity, int position)
		{
			return NamedTypeSymbol.tupleMembers[arity - 1][position - 1];
		}

		// Token: 0x0600658E RID: 25998 RVA: 0x001806C0 File Offset: 0x0017E8C0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static string TupleMemberName(int position)
		{
			return "Item" + position.ToString();
		}

		// Token: 0x0600658F RID: 25999 RVA: 0x001806D3 File Offset: 0x0017E8D3
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static int IsTupleElementNameReserved(string name)
		{
			if (NamedTypeSymbol.<IsTupleElementNameReserved>g__isElementNameForbidden|305_0(name))
			{
				return 0;
			}
			return NamedTypeSymbol.<IsTupleElementNameReserved>g__matchesCanonicalElementName|305_1(name);
		}

		// Token: 0x06006590 RID: 26000 RVA: 0x001806E8 File Offset: 0x0017E8E8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static Symbol GetWellKnownMemberInType(NamedTypeSymbol type, WellKnownMember relativeMember, BindingDiagnosticBag diagnostics, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode syntax)
		{
			Symbol symbol = NamedTypeSymbol.<GetWellKnownMemberInType>g__GetWellKnownMemberInType|306_0(type, relativeMember);
			if (symbol == null)
			{
				MemberDescriptor descriptor = WellKnownMembers.GetDescriptor(relativeMember);
				Binder.Error(diagnostics, ErrorCode.ERR_PredefinedTypeMemberNotFoundInAssembly, syntax, new object[] { descriptor.Name, type, type.ContainingAssembly });
			}
			else
			{
				UseSiteInfo<AssemblySymbol> useSiteInfo = symbol.GetUseSiteInfo();
				DiagnosticInfo diagnosticInfo = useSiteInfo.DiagnosticInfo;
				if (diagnosticInfo == null || diagnosticInfo.Severity != DiagnosticSeverity.Error)
				{
					useSiteInfo = useSiteInfo.AdjustDiagnosticInfo(null);
				}
				diagnostics.Add(useSiteInfo, ((syntax != null) ? syntax.GetLocation() : null) ?? Location.None);
			}
			return symbol;
		}

		// Token: 0x170018CB RID: 6347
		// (get) Token: 0x06006591 RID: 26001 RVA: 0x00180780 File Offset: 0x0017E980
		public sealed override bool IsTupleType
		{
			get
			{
				int num;
				return this.IsTupleTypeOfCardinality(out num);
			}
		}

		// Token: 0x170018CC RID: 6348
		// (get) Token: 0x06006592 RID: 26002 RVA: 0x00180795 File Offset: 0x0017E995
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal NamedTypeSymbol.TupleExtraData TupleData
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				if (!this.IsTupleType)
				{
					return null;
				}
				if (this._lazyTupleData == null)
				{
					Interlocked.CompareExchange<NamedTypeSymbol.TupleExtraData>(ref this._lazyTupleData, new NamedTypeSymbol.TupleExtraData(this), null);
				}
				return this._lazyTupleData;
			}
		}

		// Token: 0x170018CD RID: 6349
		// (get) Token: 0x06006593 RID: 26003 RVA: 0x001807C4 File Offset: 0x0017E9C4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		public sealed override ImmutableArray<string> TupleElementNames
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				if (this._lazyTupleData != null)
				{
					return this._lazyTupleData.ElementNames;
				}
				return default(ImmutableArray<string>);
			}
		}

		// Token: 0x170018CE RID: 6350
		// (get) Token: 0x06006594 RID: 26004 RVA: 0x001807F0 File Offset: 0x0017E9F0
		private ImmutableArray<bool> TupleErrorPositions
		{
			get
			{
				if (this._lazyTupleData != null)
				{
					return this._lazyTupleData.ErrorPositions;
				}
				return default(ImmutableArray<bool>);
			}
		}

		// Token: 0x170018CF RID: 6351
		// (get) Token: 0x06006595 RID: 26005 RVA: 0x0018081C File Offset: 0x0017EA1C
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		private ImmutableArray<Location> TupleElementLocations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				if (this._lazyTupleData != null)
				{
					return this._lazyTupleData.ElementLocations;
				}
				return default(ImmutableArray<Location>);
			}
		}

		// Token: 0x170018D0 RID: 6352
		// (get) Token: 0x06006596 RID: 26006 RVA: 0x00180848 File Offset: 0x0017EA48
		public sealed override ImmutableArray<TypeWithAnnotations> TupleElementTypesWithAnnotations
		{
			get
			{
				if (!this.IsTupleType)
				{
					return default(ImmutableArray<TypeWithAnnotations>);
				}
				return this.TupleData.TupleElementTypesWithAnnotations(this);
			}
		}

		// Token: 0x170018D1 RID: 6353
		// (get) Token: 0x06006597 RID: 26007 RVA: 0x00180874 File Offset: 0x0017EA74
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public sealed override ImmutableArray<FieldSymbol> TupleElements
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				if (!this.IsTupleType)
				{
					return default(ImmutableArray<FieldSymbol>);
				}
				return this.TupleData.TupleElements(this);
			}
		}

		// Token: 0x170018D2 RID: 6354
		// (get) Token: 0x06006598 RID: 26008 RVA: 0x0018089F File Offset: 0x0017EA9F
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		public SmallDictionary<FieldSymbol, int> TupleFieldDefinitionsToIndexMap
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
			get
			{
				if (!this.IsTupleType)
				{
					return null;
				}
				if (!base.IsDefinition)
				{
					return this.OriginalDefinition.TupleFieldDefinitionsToIndexMap;
				}
				return this.TupleData.GetFieldDefinitionsToIndexMap(this);
			}
		}

		// Token: 0x06006599 RID: 26009 RVA: 0x001808CB File Offset: 0x0017EACB
		public virtual void InitializeTupleFieldDefinitionsToIndexMap()
		{
			this.GetMembers();
		}

		// Token: 0x0600659A RID: 26010 RVA: 0x001808D4 File Offset: 0x0017EAD4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public TMember GetTupleMemberSymbolForUnderlyingMember<[global::System.Runtime.CompilerServices.Nullable(0)] TMember>(TMember underlyingMemberOpt) where TMember : Symbol
		{
			if (!this.IsTupleType)
			{
				return default(TMember);
			}
			return this.TupleData.GetTupleMemberSymbolForUnderlyingMember<TMember>(underlyingMemberOpt);
		}

		// Token: 0x0600659B RID: 26011 RVA: 0x00180900 File Offset: 0x0017EB00
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected ArrayBuilder<Symbol> AddOrWrapTupleMembers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> currentMembers)
		{
			ImmutableArray<TypeWithAnnotations> tupleElementTypesWithAnnotations = this.TupleElementTypesWithAnnotations;
			ArrayBuilder<bool> instance = ArrayBuilder<bool>.GetInstance(tupleElementTypesWithAnnotations.Length, false);
			ArrayBuilder<Symbol> instance2 = ArrayBuilder<Symbol>.GetInstance(currentMembers.Length);
			ArrayBuilder<Symbol> instance3 = ArrayBuilder<Symbol>.GetInstance();
			SmallDictionary<FieldSymbol, int> smallDictionary = (base.IsDefinition ? new SmallDictionary<FieldSymbol, int>(ReferenceEqualityComparer.Instance) : null);
			NamedTypeSymbol namedTypeSymbol = this;
			int num = 0;
			ArrayBuilder<FieldSymbol> instance4 = ArrayBuilder<FieldSymbol>.GetInstance(namedTypeSymbol.Arity);
			NamedTypeSymbol.<AddOrWrapTupleMembers>g__collectTargetTupleFields|325_1(namedTypeSymbol.Arity, NamedTypeSymbol.<AddOrWrapTupleMembers>g__getOriginalFields|325_3(currentMembers), instance4);
			ImmutableArray<string> tupleElementNames = this.TupleElementNames;
			ImmutableArray<Location> elementLocations = this.TupleData.ElementLocations;
			Symbol symbol;
			for (;;)
			{
				ImmutableArray<Symbol>.Enumerator enumerator = currentMembers.GetEnumerator();
				while (enumerator.MoveNext())
				{
					symbol = enumerator.Current;
					SymbolKind kind = symbol.Kind;
					switch (kind)
					{
					case SymbolKind.Event:
					case SymbolKind.Method:
						break;
					case SymbolKind.Field:
					{
						FieldSymbol fieldSymbol = (FieldSymbol)symbol;
						if (fieldSymbol is TupleVirtualElementFieldSymbol)
						{
							continue;
						}
						TupleElementFieldSymbol tupleElementFieldSymbol = fieldSymbol as TupleElementFieldSymbol;
						FieldSymbol fieldSymbol2 = ((tupleElementFieldSymbol != null) ? tupleElementFieldSymbol.UnderlyingField.OriginalDefinition : fieldSymbol.OriginalDefinition);
						int num2 = instance4.IndexOf(fieldSymbol2, ReferenceEqualityComparer.Instance);
						if (fieldSymbol2 is TupleErrorFieldSymbol)
						{
							continue;
						}
						if (num2 >= 0)
						{
							if (num != 0)
							{
								num2 += 7 * num;
							}
							string text = (tupleElementNames.IsDefault ? null : tupleElementNames[num2]);
							ImmutableArray<Location> immutableArray = NamedTypeSymbol.<AddOrWrapTupleMembers>g__getElementLocations|325_4(in elementLocations, num2);
							string text2 = NamedTypeSymbol.TupleMemberName(num2 + 1);
							bool flag = text != text2;
							FieldSymbol fieldSymbol3 = fieldSymbol2.AsMember(namedTypeSymbol);
							FieldSymbol fieldSymbol4;
							if (num != 0)
							{
								fieldSymbol4 = new TupleVirtualElementFieldSymbol(this, fieldSymbol3, text2, num2, immutableArray, false, flag, null);
							}
							else if (base.IsDefinition)
							{
								fieldSymbol4 = fieldSymbol;
								smallDictionary.Add(fieldSymbol, num2);
							}
							else
							{
								fieldSymbol4 = new TupleElementFieldSymbol(this, fieldSymbol3, num2, immutableArray, flag, null);
							}
							instance2.Add(fieldSymbol4);
							if (flag && !string.IsNullOrEmpty(text))
							{
								ImmutableArray<bool> tupleErrorPositions = this.TupleErrorPositions;
								bool flag2 = !tupleErrorPositions.IsDefault && tupleErrorPositions[num2];
								instance2.Add(new TupleVirtualElementFieldSymbol(this, fieldSymbol3, text, num2, immutableArray, flag2, false, fieldSymbol4));
							}
							instance[num2] = true;
							continue;
						}
						if (num == 0)
						{
							instance2.Add(fieldSymbol);
							continue;
						}
						continue;
					}
					case SymbolKind.Label:
					case SymbolKind.Local:
					case SymbolKind.NetModule:
						goto IL_0234;
					case SymbolKind.NamedType:
						continue;
					default:
						if (kind != SymbolKind.Property)
						{
							goto IL_0234;
						}
						break;
					}
					if (num == 0)
					{
						instance3.Add(symbol);
						continue;
					}
					continue;
					IL_0234:
					if (num == 0)
					{
						goto Block_17;
					}
				}
				if (namedTypeSymbol.Arity != 8)
				{
					goto IL_02BD;
				}
				namedTypeSymbol = (NamedTypeSymbol)namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[7].Type;
				num++;
				if (namedTypeSymbol.Arity != 8)
				{
					currentMembers = namedTypeSymbol.GetMembers();
					instance4.Clear();
					NamedTypeSymbol.<AddOrWrapTupleMembers>g__collectTargetTupleFields|325_1(namedTypeSymbol.Arity, NamedTypeSymbol.<AddOrWrapTupleMembers>g__getOriginalFields|325_3(currentMembers), instance4);
				}
			}
			Block_17:
			throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
			IL_02BD:
			instance4.Free();
			for (int i = 0; i < instance.Count; i++)
			{
				if (!instance[i])
				{
					int num4;
					int num3 = NamedTypeSymbol.NumberOfValueTuples(i + 1, out num4);
					NamedTypeSymbol originalDefinition = NamedTypeSymbol.<AddOrWrapTupleMembers>g__getNestedTupleUnderlyingType|325_0(this, num3 - 1).OriginalDefinition;
					CSDiagnosticInfo csdiagnosticInfo = (originalDefinition.IsErrorType() ? null : new CSDiagnosticInfo(ErrorCode.ERR_PredefinedTypeMemberNotFoundInAssembly, new object[]
					{
						NamedTypeSymbol.TupleMemberName(num4),
						originalDefinition,
						originalDefinition.ContainingAssembly
					}));
					string text3 = (tupleElementNames.IsDefault ? null : tupleElementNames[i]);
					Location location = (elementLocations.IsDefault ? null : elementLocations[i]);
					string text4 = NamedTypeSymbol.TupleMemberName(i + 1);
					bool flag3 = text3 != text4;
					TupleErrorFieldSymbol tupleErrorFieldSymbol = new TupleErrorFieldSymbol(this, text4, i, flag3 ? null : location, tupleElementTypesWithAnnotations[i], csdiagnosticInfo, flag3, null);
					instance2.Add(tupleErrorFieldSymbol);
					if (flag3 && !string.IsNullOrEmpty(text3))
					{
						instance2.Add(new TupleErrorFieldSymbol(this, text3, i, location, tupleElementTypesWithAnnotations[i], csdiagnosticInfo, false, tupleErrorFieldSymbol));
					}
				}
			}
			instance.Free();
			instance2.AddRange(instance3);
			instance3.Free();
			if (smallDictionary != null)
			{
				this.TupleData.SetFieldDefinitionsToIndexMap(smallDictionary);
			}
			return instance2;
		}

		// Token: 0x0600659C RID: 26012 RVA: 0x00180D10 File Offset: 0x0017EF10
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private TypeSymbol MergeTupleNames(NamedTypeSymbol other, NamedTypeSymbol mergedType)
		{
			ImmutableArray<string> tupleElementNames = this.TupleElementNames;
			ImmutableArray<string> tupleElementNames2 = other.TupleElementNames;
			ImmutableArray<string> immutableArray;
			if (tupleElementNames.IsDefault || tupleElementNames2.IsDefault)
			{
				immutableArray = default(ImmutableArray<string>);
			}
			else
			{
				immutableArray = tupleElementNames.ZipAsArray<string, string, string>(tupleElementNames2, delegate(string n1, string n2)
				{
					if (string.CompareOrdinal(n1, n2) != 0)
					{
						return null;
					}
					return n1;
				});
				if (immutableArray.All<string>((string n) => n == null))
				{
					immutableArray = default(ImmutableArray<string>);
				}
			}
			if (!(immutableArray.IsDefault ? this.TupleElementNames.IsDefault : immutableArray.SequenceEqual<string, string>(this.TupleElementNames, null)) || !this.Equals(mergedType, TypeCompareKind.ConsiderEverything))
			{
				return NamedTypeSymbol.CreateTuple(mergedType, immutableArray, this.TupleErrorPositions, this.TupleElementLocations, this.Locations);
			}
			return this;
		}

		// Token: 0x0600659E RID: 26014 RVA: 0x00180F14 File Offset: 0x0017F114
		[CompilerGenerated]
		private bool <EqualsComplicatedCases>g__tupleNamesEquals|195_0(NamedTypeSymbol other, TypeCompareKind comparison)
		{
			if ((comparison & TypeCompareKind.IgnoreTupleNames) != TypeCompareKind.ConsiderEverything)
			{
				return true;
			}
			ImmutableArray<string> tupleElementNames = this.TupleElementNames;
			ImmutableArray<string> tupleElementNames2 = other.TupleElementNames;
			if (!tupleElementNames.IsDefault)
			{
				return !tupleElementNames2.IsDefault && tupleElementNames.SequenceEqual<string, string>(tupleElementNames2, null);
			}
			return tupleElementNames2.IsDefault;
		}

		// Token: 0x0600659F RID: 26015 RVA: 0x00180F5C File Offset: 0x0017F15C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[CompilerGenerated]
		internal static NamedTypeSymbol <CreateTuple>g__getTupleUnderlyingType|283_0([global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<TypeWithAnnotations> elementTypes, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpSyntaxNode syntax, CSharpCompilation compilation, [global::System.Runtime.CompilerServices.Nullable(2)] BindingDiagnosticBag diagnostics)
		{
			int num2;
			int num = NamedTypeSymbol.NumberOfValueTuples(elementTypes.Length, out num2);
			NamedTypeSymbol wellKnownType = compilation.GetWellKnownType(NamedTypeSymbol.GetTupleType(num2));
			if (diagnostics != null && syntax != null)
			{
				NamedTypeSymbol.ReportUseSiteAndObsoleteDiagnostics(syntax, diagnostics, wellKnownType);
			}
			NamedTypeSymbol namedTypeSymbol = null;
			if (num > 1)
			{
				namedTypeSymbol = compilation.GetWellKnownType(NamedTypeSymbol.GetTupleType(8));
				if (diagnostics != null && syntax != null)
				{
					NamedTypeSymbol.ReportUseSiteAndObsoleteDiagnostics(syntax, diagnostics, namedTypeSymbol);
				}
			}
			return NamedTypeSymbol.ConstructTupleUnderlyingType(wellKnownType, namedTypeSymbol, elementTypes);
		}

		// Token: 0x060065A0 RID: 26016 RVA: 0x00180FBC File Offset: 0x0017F1BC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[CompilerGenerated]
		internal static bool <IsTupleElementNameReserved>g__isElementNameForbidden|305_0(string name)
		{
			return name == "CompareTo" || name == "Deconstruct" || name == "Equals" || name == "GetHashCode" || name == "Rest" || name == "ToString";
		}

		// Token: 0x060065A1 RID: 26017 RVA: 0x0018101C File Offset: 0x0017F21C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[CompilerGenerated]
		internal static int <IsTupleElementNameReserved>g__matchesCanonicalElementName|305_1(string name)
		{
			int num;
			if (name.StartsWith("Item", 4) && int.TryParse(name.Substring(4), ref num) && num > 0 && string.Equals(name, NamedTypeSymbol.TupleMemberName(num), 4))
			{
				return num;
			}
			return -1;
		}

		// Token: 0x060065A2 RID: 26018 RVA: 0x00181060 File Offset: 0x0017F260
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[CompilerGenerated]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static Symbol <GetWellKnownMemberInType>g__GetWellKnownMemberInType|306_0(NamedTypeSymbol type, WellKnownMember relativeMember)
		{
			MemberDescriptor descriptor = WellKnownMembers.GetDescriptor(relativeMember);
			return CSharpCompilation.GetRuntimeMember(type.GetMembers(descriptor.Name), in descriptor, CSharpCompilation.SpecialMembersSignatureComparer.Instance, null);
		}

		// Token: 0x060065A3 RID: 26019 RVA: 0x00181090 File Offset: 0x0017F290
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[CompilerGenerated]
		internal static NamedTypeSymbol <AddOrWrapTupleMembers>g__getNestedTupleUnderlyingType|325_0(NamedTypeSymbol topLevelUnderlyingType, int depth)
		{
			NamedTypeSymbol namedTypeSymbol = topLevelUnderlyingType;
			for (int i = 0; i < depth; i++)
			{
				namedTypeSymbol = (NamedTypeSymbol)namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[7].Type;
			}
			return namedTypeSymbol;
		}

		// Token: 0x060065A4 RID: 26020 RVA: 0x001810CC File Offset: 0x0017F2CC
		[CompilerGenerated]
		internal static void <AddOrWrapTupleMembers>g__collectTargetTupleFields|325_1(int arity, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> members, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 2 })] ArrayBuilder<FieldSymbol> fieldsForElements)
		{
			int num = Math.Min(arity, 7);
			for (int i = 0; i < num; i++)
			{
				WellKnownMember tupleTypeMember = NamedTypeSymbol.GetTupleTypeMember(arity, i + 1);
				fieldsForElements.Add((FieldSymbol)NamedTypeSymbol.<AddOrWrapTupleMembers>g__GetWellKnownMemberInType|325_2(members, tupleTypeMember));
			}
		}

		// Token: 0x060065A5 RID: 26021 RVA: 0x0018110C File Offset: 0x0017F30C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		internal static Symbol <AddOrWrapTupleMembers>g__GetWellKnownMemberInType|325_2([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> members, WellKnownMember relativeMember)
		{
			MemberDescriptor descriptor = WellKnownMembers.GetDescriptor(relativeMember);
			return CSharpCompilation.GetRuntimeMember(members, in descriptor, CSharpCompilation.SpecialMembersSignatureComparer.Instance, null);
		}

		// Token: 0x060065A6 RID: 26022 RVA: 0x00181130 File Offset: 0x0017F330
		[CompilerGenerated]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<Symbol> <AddOrWrapTupleMembers>g__getOriginalFields|325_3([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> members)
		{
			ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance();
			foreach (Symbol symbol in members)
			{
				if (!(symbol is TupleVirtualElementFieldSymbol))
				{
					TupleElementFieldSymbol tupleElementFieldSymbol = symbol as TupleElementFieldSymbol;
					if (tupleElementFieldSymbol != null)
					{
						instance.Add(tupleElementFieldSymbol.UnderlyingField.OriginalDefinition);
					}
					else
					{
						FieldSymbol fieldSymbol = symbol as FieldSymbol;
						if (fieldSymbol != null)
						{
							instance.Add(fieldSymbol.OriginalDefinition);
						}
					}
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060065A7 RID: 26023 RVA: 0x001811A4 File Offset: 0x0017F3A4
		[CompilerGenerated]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<Location> <AddOrWrapTupleMembers>g__getElementLocations|325_4([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] in ImmutableArray<Location> elementLocations, int tupleFieldIndex)
		{
			ImmutableArray<Location> immutableArray = elementLocations;
			if (immutableArray.IsDefault)
			{
				return ImmutableArray<Location>.Empty;
			}
			immutableArray = elementLocations;
			Location location = immutableArray[tupleFieldIndex];
			if (!(location == null))
			{
				return ImmutableArray.Create<Location>(location);
			}
			return ImmutableArray<Location>.Empty;
		}

		// Token: 0x04001992 RID: 6546
		private bool _hasNoBaseCycles;

		// Token: 0x04001993 RID: 6547
		protected static Func<Symbol, bool> IsInstanceFieldOrEvent = delegate(Symbol symbol)
		{
			if (!symbol.IsStatic)
			{
				SymbolKind kind = symbol.Kind;
				if (kind - SymbolKind.Event <= 1)
				{
					return true;
				}
			}
			return false;
		};

		// Token: 0x04001994 RID: 6548
		internal static readonly Func<TypeWithAnnotations, bool> TypeWithAnnotationsIsNullFunction = (TypeWithAnnotations type) => !type.HasType;

		// Token: 0x04001995 RID: 6549
		internal static readonly Func<TypeWithAnnotations, bool> TypeWithAnnotationsIsErrorType = (TypeWithAnnotations type) => type.HasType && type.Type.IsErrorType();

		// Token: 0x04001996 RID: 6550
		internal const int ValueTupleRestPosition = 8;

		// Token: 0x04001997 RID: 6551
		internal const int ValueTupleRestIndex = 7;

		// Token: 0x04001998 RID: 6552
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal const string ValueTupleTypeName = "ValueTuple";

		// Token: 0x04001999 RID: 6553
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal const string ValueTupleRestFieldName = "Rest";

		// Token: 0x0400199A RID: 6554
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private NamedTypeSymbol.TupleExtraData _lazyTupleData;

		// Token: 0x0400199B RID: 6555
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private static readonly WellKnownType[] tupleTypes = new WellKnownType[]
		{
			WellKnownType.System_ValueTuple_T1,
			WellKnownType.System_ValueTuple_T2,
			WellKnownType.System_ValueTuple_T3,
			WellKnownType.System_ValueTuple_T4,
			WellKnownType.System_ValueTuple_T5,
			WellKnownType.System_ValueTuple_T6,
			WellKnownType.System_ValueTuple_T7,
			WellKnownType.System_ValueTuple_TRest
		};

		// Token: 0x0400199C RID: 6556
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private static readonly WellKnownMember[] tupleCtors = new WellKnownMember[]
		{
			WellKnownMember.System_ValueTuple_T1__ctor,
			WellKnownMember.System_ValueTuple_T2__ctor,
			WellKnownMember.System_ValueTuple_T3__ctor,
			WellKnownMember.System_ValueTuple_T4__ctor,
			WellKnownMember.System_ValueTuple_T5__ctor,
			WellKnownMember.System_ValueTuple_T6__ctor,
			WellKnownMember.System_ValueTuple_T7__ctor,
			WellKnownMember.System_ValueTuple_TRest__ctor
		};

		// Token: 0x0400199D RID: 6557
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private static readonly WellKnownMember[][] tupleMembers = new WellKnownMember[][]
		{
			new WellKnownMember[] { WellKnownMember.System_ValueTuple_T1__Item1 },
			new WellKnownMember[]
			{
				WellKnownMember.System_ValueTuple_T2__Item1,
				WellKnownMember.System_ValueTuple_T2__Item2
			},
			new WellKnownMember[]
			{
				WellKnownMember.System_ValueTuple_T3__Item1,
				WellKnownMember.System_ValueTuple_T3__Item2,
				WellKnownMember.System_ValueTuple_T3__Item3
			},
			new WellKnownMember[]
			{
				WellKnownMember.System_ValueTuple_T4__Item1,
				WellKnownMember.System_ValueTuple_T4__Item2,
				WellKnownMember.System_ValueTuple_T4__Item3,
				WellKnownMember.System_ValueTuple_T4__Item4
			},
			new WellKnownMember[]
			{
				WellKnownMember.System_ValueTuple_T5__Item1,
				WellKnownMember.System_ValueTuple_T5__Item2,
				WellKnownMember.System_ValueTuple_T5__Item3,
				WellKnownMember.System_ValueTuple_T5__Item4,
				WellKnownMember.System_ValueTuple_T5__Item5
			},
			new WellKnownMember[]
			{
				WellKnownMember.System_ValueTuple_T6__Item1,
				WellKnownMember.System_ValueTuple_T6__Item2,
				WellKnownMember.System_ValueTuple_T6__Item3,
				WellKnownMember.System_ValueTuple_T6__Item4,
				WellKnownMember.System_ValueTuple_T6__Item5,
				WellKnownMember.System_ValueTuple_T6__Item6
			},
			new WellKnownMember[]
			{
				WellKnownMember.System_ValueTuple_T7__Item1,
				WellKnownMember.System_ValueTuple_T7__Item2,
				WellKnownMember.System_ValueTuple_T7__Item3,
				WellKnownMember.System_ValueTuple_T7__Item4,
				WellKnownMember.System_ValueTuple_T7__Item5,
				WellKnownMember.System_ValueTuple_T7__Item6,
				WellKnownMember.System_ValueTuple_T7__Item7
			},
			new WellKnownMember[]
			{
				WellKnownMember.System_ValueTuple_TRest__Item1,
				WellKnownMember.System_ValueTuple_TRest__Item2,
				WellKnownMember.System_ValueTuple_TRest__Item3,
				WellKnownMember.System_ValueTuple_TRest__Item4,
				WellKnownMember.System_ValueTuple_TRest__Item5,
				WellKnownMember.System_ValueTuple_TRest__Item6,
				WellKnownMember.System_ValueTuple_TRest__Item7,
				WellKnownMember.System_ValueTuple_TRest__Rest
			}
		};

		// Token: 0x020008D7 RID: 2263
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal sealed class TupleExtraData
		{
			// Token: 0x1700284A RID: 10314
			// (get) Token: 0x06008E58 RID: 36440 RVA: 0x001F61F1 File Offset: 0x001F43F1
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			internal ImmutableArray<string> ElementNames
			{
				[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
				get;
			}

			// Token: 0x1700284B RID: 10315
			// (get) Token: 0x06008E59 RID: 36441 RVA: 0x001F61F9 File Offset: 0x001F43F9
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			internal ImmutableArray<Location> ElementLocations
			{
				[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
				get;
			}

			// Token: 0x1700284C RID: 10316
			// (get) Token: 0x06008E5A RID: 36442 RVA: 0x001F6201 File Offset: 0x001F4401
			[global::System.Runtime.CompilerServices.Nullable(0)]
			internal ImmutableArray<bool> ErrorPositions
			{
				[global::System.Runtime.CompilerServices.NullableContext(0)]
				get;
			}

			// Token: 0x1700284D RID: 10317
			// (get) Token: 0x06008E5B RID: 36443 RVA: 0x001F6209 File Offset: 0x001F4409
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			internal ImmutableArray<Location> Locations
			{
				[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
				get;
			}

			// Token: 0x1700284E RID: 10318
			// (get) Token: 0x06008E5C RID: 36444 RVA: 0x001F6211 File Offset: 0x001F4411
			internal NamedTypeSymbol TupleUnderlyingType { get; }

			// Token: 0x06008E5D RID: 36445 RVA: 0x001F6219 File Offset: 0x001F4419
			internal TupleExtraData(NamedTypeSymbol underlyingType)
			{
				this.TupleUnderlyingType = underlyingType;
				this.Locations = ImmutableArray<Location>.Empty;
			}

			// Token: 0x06008E5E RID: 36446 RVA: 0x001F6233 File Offset: 0x001F4433
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			internal TupleExtraData([global::System.Runtime.CompilerServices.Nullable(1)] NamedTypeSymbol underlyingType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<string> elementNames, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<Location> elementLocations, ImmutableArray<bool> errorPositions, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Location> locations)
				: this(underlyingType)
			{
				this.ElementNames = elementNames;
				this.ElementLocations = elementLocations;
				this.ErrorPositions = errorPositions;
				this.Locations = locations.NullToEmpty<Location>();
			}

			// Token: 0x06008E5F RID: 36447 RVA: 0x001F6260 File Offset: 0x001F4460
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			internal bool EqualsIgnoringTupleUnderlyingType(NamedTypeSymbol.TupleExtraData other)
			{
				return (other == null && this.ElementNames.IsDefault && this.ElementLocations.IsDefault && this.ErrorPositions.IsDefault) || (other != null && NamedTypeSymbol.TupleExtraData.<EqualsIgnoringTupleUnderlyingType>g__areEqual|21_0<string>(this.ElementNames, other.ElementNames) && NamedTypeSymbol.TupleExtraData.<EqualsIgnoringTupleUnderlyingType>g__areEqual|21_0<Location>(this.ElementLocations, other.ElementLocations) && NamedTypeSymbol.TupleExtraData.<EqualsIgnoringTupleUnderlyingType>g__areEqual|21_0<bool>(this.ErrorPositions, other.ErrorPositions));
			}

			// Token: 0x06008E60 RID: 36448 RVA: 0x001F62DE File Offset: 0x001F44DE
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			public ImmutableArray<TypeWithAnnotations> TupleElementTypesWithAnnotations([global::System.Runtime.CompilerServices.Nullable(1)] NamedTypeSymbol tuple)
			{
				if (this._lazyElementTypes.IsDefault)
				{
					ImmutableInterlocked.InterlockedInitialize<TypeWithAnnotations>(ref this._lazyElementTypes, NamedTypeSymbol.TupleExtraData.<TupleElementTypesWithAnnotations>g__collectTupleElementTypesWithAnnotations|22_0(tuple));
				}
				return this._lazyElementTypes;
			}

			// Token: 0x06008E61 RID: 36449 RVA: 0x001F6305 File Offset: 0x001F4505
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<FieldSymbol> TupleElements(NamedTypeSymbol tuple)
			{
				if (this._lazyDefaultElementFields.IsDefault)
				{
					ImmutableInterlocked.InterlockedInitialize<FieldSymbol>(ref this._lazyDefaultElementFields, this.<TupleElements>g__collectTupleElementFields|23_0(tuple));
				}
				return this._lazyDefaultElementFields;
			}

			// Token: 0x06008E62 RID: 36450 RVA: 0x001F632D File Offset: 0x001F452D
			internal SmallDictionary<FieldSymbol, int> GetFieldDefinitionsToIndexMap(NamedTypeSymbol tuple)
			{
				if (this._lazyFieldDefinitionsToIndexMap == null)
				{
					tuple.InitializeTupleFieldDefinitionsToIndexMap();
				}
				return this._lazyFieldDefinitionsToIndexMap;
			}

			// Token: 0x06008E63 RID: 36451 RVA: 0x001F6343 File Offset: 0x001F4543
			internal void SetFieldDefinitionsToIndexMap(SmallDictionary<FieldSymbol, int> map)
			{
				Interlocked.CompareExchange<SmallDictionary<FieldSymbol, int>>(ref this._lazyFieldDefinitionsToIndexMap, map, null);
			}

			// Token: 0x1700284F RID: 10319
			// (get) Token: 0x06008E64 RID: 36452 RVA: 0x001F6354 File Offset: 0x001F4554
			internal SmallDictionary<Symbol, Symbol> UnderlyingDefinitionToMemberMap
			{
				get
				{
					SmallDictionary<Symbol, Symbol> smallDictionary;
					if ((smallDictionary = this._lazyUnderlyingDefinitionToMemberMap) == null)
					{
						smallDictionary = (this._lazyUnderlyingDefinitionToMemberMap = this.<get_UnderlyingDefinitionToMemberMap>g__computeDefinitionToMemberMap|27_0());
					}
					return smallDictionary;
				}
			}

			// Token: 0x06008E65 RID: 36453 RVA: 0x001F637C File Offset: 0x001F457C
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public TMember GetTupleMemberSymbolForUnderlyingMember<[global::System.Runtime.CompilerServices.Nullable(0)] TMember>(TMember underlyingMemberOpt) where TMember : Symbol
			{
				if (underlyingMemberOpt == null)
				{
					return default(TMember);
				}
				Symbol symbol = underlyingMemberOpt.OriginalDefinition;
				TupleElementFieldSymbol tupleElementFieldSymbol = symbol as TupleElementFieldSymbol;
				if (tupleElementFieldSymbol != null)
				{
					symbol = tupleElementFieldSymbol.UnderlyingField;
				}
				Symbol symbol2;
				if (TypeSymbol.Equals(symbol.ContainingType, this.TupleUnderlyingType.OriginalDefinition, TypeCompareKind.ConsiderEverything) && this.UnderlyingDefinitionToMemberMap.TryGetValue(symbol, out symbol2))
				{
					return (TMember)((object)symbol2);
				}
				return default(TMember);
			}

			// Token: 0x06008E66 RID: 36454 RVA: 0x001F63F1 File Offset: 0x001F45F1
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			[CompilerGenerated]
			internal static bool <EqualsIgnoringTupleUnderlyingType>g__areEqual|21_0<T>(ImmutableArray<T> one, ImmutableArray<T> other)
			{
				return (one.IsDefault && other.IsDefault) || (one.IsDefault == other.IsDefault && one.SequenceEqual<T, T>(other, null));
			}

			// Token: 0x06008E67 RID: 36455 RVA: 0x001F6424 File Offset: 0x001F4624
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			[CompilerGenerated]
			internal static ImmutableArray<TypeWithAnnotations> <TupleElementTypesWithAnnotations>g__collectTupleElementTypesWithAnnotations|22_0([global::System.Runtime.CompilerServices.Nullable(1)] NamedTypeSymbol tuple)
			{
				ImmutableArray<TypeWithAnnotations> immutableArray;
				if (tuple.Arity == 8)
				{
					ImmutableArray<TypeWithAnnotations> tupleElementTypesWithAnnotations = tuple.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[7].Type.TupleElementTypesWithAnnotations;
					ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(7 + tupleElementTypesWithAnnotations.Length);
					instance.AddRange(tuple.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics, 7);
					instance.AddRange(tupleElementTypesWithAnnotations);
					immutableArray = instance.ToImmutableAndFree();
				}
				else
				{
					immutableArray = tuple.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics;
				}
				return immutableArray;
			}

			// Token: 0x06008E68 RID: 36456 RVA: 0x001F648C File Offset: 0x001F468C
			[CompilerGenerated]
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<FieldSymbol> <TupleElements>g__collectTupleElementFields|23_0(NamedTypeSymbol tuple)
			{
				ArrayBuilder<FieldSymbol> instance = ArrayBuilder<FieldSymbol>.GetInstance(this.TupleElementTypesWithAnnotations(tuple).Length, null);
				foreach (Symbol symbol in tuple.GetMembers())
				{
					if (symbol.Kind == SymbolKind.Field)
					{
						FieldSymbol fieldSymbol = (FieldSymbol)symbol;
						int tupleElementIndex = fieldSymbol.TupleElementIndex;
						if (tupleElementIndex >= 0)
						{
							FieldSymbol fieldSymbol2 = instance[tupleElementIndex];
							if (fieldSymbol2 == null || fieldSymbol2.IsDefaultTupleElement)
							{
								instance[tupleElementIndex] = fieldSymbol;
							}
						}
					}
				}
				return instance.ToImmutableAndFree();
			}

			// Token: 0x06008E69 RID: 36457 RVA: 0x001F651C File Offset: 0x001F471C
			[CompilerGenerated]
			private SmallDictionary<Symbol, Symbol> <get_UnderlyingDefinitionToMemberMap>g__computeDefinitionToMemberMap|27_0()
			{
				SmallDictionary<Symbol, Symbol> smallDictionary = new SmallDictionary<Symbol, Symbol>(ReferenceEqualityComparer.Instance);
				ImmutableArray<Symbol> members = this.TupleUnderlyingType.GetMembers();
				int i = members.Length - 1;
				while (i >= 0)
				{
					Symbol symbol = members[i];
					SymbolKind kind = symbol.Kind;
					switch (kind)
					{
					case SymbolKind.Event:
					{
						EventSymbol eventSymbol = (EventSymbol)symbol;
						FieldSymbol associatedField = eventSymbol.AssociatedField;
						if (associatedField != null)
						{
							smallDictionary.Add(associatedField.OriginalDefinition, associatedField);
						}
						smallDictionary.Add(eventSymbol.OriginalDefinition, symbol);
						break;
					}
					case SymbolKind.Field:
					{
						FieldSymbol tupleUnderlyingField = ((FieldSymbol)symbol).TupleUnderlyingField;
						if (tupleUnderlyingField != null)
						{
							smallDictionary[tupleUnderlyingField.OriginalDefinition] = symbol;
						}
						break;
					}
					case SymbolKind.Label:
					case SymbolKind.Local:
					case SymbolKind.NetModule:
						goto IL_00C6;
					case SymbolKind.Method:
					case SymbolKind.NamedType:
						goto IL_0062;
					default:
						if (kind == SymbolKind.Property)
						{
							goto IL_0062;
						}
						goto IL_00C6;
					}
					IL_00D7:
					i--;
					continue;
					IL_0062:
					smallDictionary.Add(symbol.OriginalDefinition, symbol);
					goto IL_00D7;
					IL_00C6:
					throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
				}
				return smallDictionary;
			}

			// Token: 0x04002675 RID: 9845
			[global::System.Runtime.CompilerServices.Nullable(0)]
			private ImmutableArray<TypeWithAnnotations> _lazyElementTypes;

			// Token: 0x04002676 RID: 9846
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<FieldSymbol> _lazyDefaultElementFields;

			// Token: 0x04002677 RID: 9847
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
			private SmallDictionary<FieldSymbol, int> _lazyFieldDefinitionsToIndexMap;

			// Token: 0x04002678 RID: 9848
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })]
			private SmallDictionary<Symbol, Symbol> _lazyUnderlyingDefinitionToMemberMap;
		}
	}
}
