using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Debugging;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Emit.NoPia
{
	// Token: 0x02000497 RID: 1175
	internal abstract class EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter> : CommonEmbeddedTypesManager where TPEModuleBuilder : CommonPEModuleBuilder where TModuleCompilationState : CommonModuleCompilationState where TEmbeddedTypesManager : EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter> where TSyntaxNode : SyntaxNode where TAttributeData : AttributeData, ICustomAttribute where TAssemblySymbol : class where TNamedTypeSymbol : class, TSymbol, INamespaceTypeReference where TFieldSymbol : class, TSymbol, IFieldReference where TMethodSymbol : class, TSymbol, IMethodReference where TEventSymbol : class, TSymbol, ITypeMemberReference where TPropertySymbol : class, TSymbol, ITypeMemberReference where TParameterSymbol : class, TSymbol, IParameterListEntry, INamedEntity where TTypeParameterSymbol : class, TSymbol, IGenericMethodParameterReference where TEmbeddedType : EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.CommonEmbeddedType where TEmbeddedField : EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.CommonEmbeddedField where TEmbeddedMethod : EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.CommonEmbeddedMethod where TEmbeddedEvent : EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.CommonEmbeddedEvent where TEmbeddedProperty : EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.CommonEmbeddedProperty where TEmbeddedParameter : EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.CommonEmbeddedParameter where TEmbeddedTypeParameter : EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.CommonEmbeddedTypeParameter
	{
		// Token: 0x06002E3A RID: 11834 RVA: 0x00082FB0 File Offset: 0x000811B0
		protected EmbeddedTypesManager(TPEModuleBuilder moduleBeingBuilt)
		{
			this.ModuleBeingBuilt = moduleBeingBuilt;
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x0008301A File Offset: 0x0008121A
		public override bool IsFrozen
		{
			get
			{
				return !this._frozen.IsDefault;
			}
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x0008302C File Offset: 0x0008122C
		public override ImmutableArray<INamespaceTypeDefinition> GetTypes(DiagnosticBag diagnostics, HashSet<string> namesOfTopLevelTypes)
		{
			if (this._frozen.IsDefault)
			{
				ArrayBuilder<TEmbeddedType> instance = ArrayBuilder<TEmbeddedType>.GetInstance();
				instance.AddRange(this.EmbeddedTypesMap.Values);
				instance.Sort(EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.TypeComparer.Instance);
				if (ImmutableInterlocked.InterlockedInitialize<TEmbeddedType>(ref this._frozen, instance.ToImmutableAndFree()) && this._frozen.Length > 0)
				{
					INamespaceTypeDefinition namespaceTypeDefinition = this._frozen[0];
					bool flag = this.HasNameConflict(namesOfTopLevelTypes, this._frozen[0], diagnostics);
					for (int i = 1; i < this._frozen.Length; i++)
					{
						INamespaceTypeDefinition namespaceTypeDefinition2 = this._frozen[i];
						if (namespaceTypeDefinition.NamespaceName == namespaceTypeDefinition2.NamespaceName && namespaceTypeDefinition.Name == namespaceTypeDefinition2.Name)
						{
							if (!flag)
							{
								this.ReportNameCollisionBetweenEmbeddedTypes(this._frozen[i - 1], this._frozen[i], diagnostics);
								flag = true;
							}
						}
						else
						{
							namespaceTypeDefinition = namespaceTypeDefinition2;
							flag = this.HasNameConflict(namesOfTopLevelTypes, this._frozen[i], diagnostics);
						}
					}
					this.OnGetTypesCompleted(this._frozen, diagnostics);
				}
			}
			return StaticCast<INamespaceTypeDefinition>.From<TEmbeddedType>(this._frozen);
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x00083168 File Offset: 0x00081368
		private bool HasNameConflict(HashSet<string> namesOfTopLevelTypes, TEmbeddedType type, DiagnosticBag diagnostics)
		{
			INamespaceTypeDefinition namespaceTypeDefinition = type;
			if (namesOfTopLevelTypes.Contains(MetadataHelpers.BuildQualifiedName(namespaceTypeDefinition.NamespaceName, namespaceTypeDefinition.Name)))
			{
				this.ReportNameCollisionWithAlreadyDeclaredType(type, diagnostics);
				return true;
			}
			return false;
		}

		// Token: 0x06002E3E RID: 11838
		internal abstract int GetTargetAttributeSignatureIndex(TSymbol underlyingSymbol, TAttributeData attrData, AttributeDescription description);

		// Token: 0x06002E3F RID: 11839 RVA: 0x000831A0 File Offset: 0x000813A0
		internal bool IsTargetAttribute(TSymbol underlyingSymbol, TAttributeData attrData, AttributeDescription description)
		{
			return this.GetTargetAttributeSignatureIndex(underlyingSymbol, attrData, description) != -1;
		}

		// Token: 0x06002E40 RID: 11840
		internal abstract TAttributeData CreateSynthesizedAttribute(WellKnownMember constructor, TAttributeData attrData, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics);

		// Token: 0x06002E41 RID: 11841
		internal abstract void ReportIndirectReferencesToLinkedAssemblies(TAssemblySymbol assembly, DiagnosticBag diagnostics);

		// Token: 0x06002E42 RID: 11842
		protected abstract void OnGetTypesCompleted(ImmutableArray<TEmbeddedType> types, DiagnosticBag diagnostics);

		// Token: 0x06002E43 RID: 11843
		protected abstract void ReportNameCollisionBetweenEmbeddedTypes(TEmbeddedType typeA, TEmbeddedType typeB, DiagnosticBag diagnostics);

		// Token: 0x06002E44 RID: 11844
		protected abstract void ReportNameCollisionWithAlreadyDeclaredType(TEmbeddedType type, DiagnosticBag diagnostics);

		// Token: 0x06002E45 RID: 11845
		protected abstract TAttributeData CreateCompilerGeneratedAttribute();

		// Token: 0x06002E46 RID: 11846 RVA: 0x000831B1 File Offset: 0x000813B1
		protected void EmbedReferences(ITypeDefinitionMember embeddedMember, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			new TypeReferenceIndexer(new EmitContext(this.ModuleBeingBuilt, syntaxNodeOpt, diagnostics, false, true)).Visit(embeddedMember);
		}

		// Token: 0x06002E47 RID: 11847
		protected abstract TEmbeddedType GetEmbeddedTypeForMember(TSymbol member, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics);

		// Token: 0x06002E48 RID: 11848
		internal abstract TEmbeddedField EmbedField(TEmbeddedType type, TFieldSymbol field, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics);

		// Token: 0x06002E49 RID: 11849
		internal abstract TEmbeddedMethod EmbedMethod(TEmbeddedType type, TMethodSymbol method, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics);

		// Token: 0x06002E4A RID: 11850
		internal abstract TEmbeddedProperty EmbedProperty(TEmbeddedType type, TPropertySymbol property, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics);

		// Token: 0x06002E4B RID: 11851
		internal abstract TEmbeddedEvent EmbedEvent(TEmbeddedType type, TEventSymbol @event, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics, bool isUsedForComAwareEventBinding);

		// Token: 0x06002E4C RID: 11852 RVA: 0x000831D8 File Offset: 0x000813D8
		internal IFieldReference EmbedFieldIfNeedTo(TFieldSymbol fieldSymbol, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			TEmbeddedType embeddedTypeForMember = this.GetEmbeddedTypeForMember((TSymbol)((object)fieldSymbol), syntaxNodeOpt, diagnostics);
			if (embeddedTypeForMember != null)
			{
				return this.EmbedField(embeddedTypeForMember, fieldSymbol, syntaxNodeOpt, diagnostics);
			}
			return fieldSymbol;
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x00083218 File Offset: 0x00081418
		internal IMethodReference EmbedMethodIfNeedTo(TMethodSymbol methodSymbol, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			TEmbeddedType embeddedTypeForMember = this.GetEmbeddedTypeForMember((TSymbol)((object)methodSymbol), syntaxNodeOpt, diagnostics);
			if (embeddedTypeForMember != null)
			{
				return this.EmbedMethod(embeddedTypeForMember, methodSymbol, syntaxNodeOpt, diagnostics);
			}
			return methodSymbol;
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x00083258 File Offset: 0x00081458
		internal void EmbedEventIfNeedTo(TEventSymbol eventSymbol, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics, bool isUsedForComAwareEventBinding)
		{
			TEmbeddedType embeddedTypeForMember = this.GetEmbeddedTypeForMember((TSymbol)((object)eventSymbol), syntaxNodeOpt, diagnostics);
			if (embeddedTypeForMember != null)
			{
				this.EmbedEvent(embeddedTypeForMember, eventSymbol, syntaxNodeOpt, diagnostics, isUsedForComAwareEventBinding);
			}
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x00083290 File Offset: 0x00081490
		internal void EmbedPropertyIfNeedTo(TPropertySymbol propertySymbol, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			TEmbeddedType embeddedTypeForMember = this.GetEmbeddedTypeForMember((TSymbol)((object)propertySymbol), syntaxNodeOpt, diagnostics);
			if (embeddedTypeForMember != null)
			{
				this.EmbedProperty(embeddedTypeForMember, propertySymbol, syntaxNodeOpt, diagnostics);
			}
		}

		// Token: 0x0400126A RID: 4714
		public readonly TPEModuleBuilder ModuleBeingBuilt;

		// Token: 0x0400126B RID: 4715
		public readonly ConcurrentDictionary<TNamedTypeSymbol, TEmbeddedType> EmbeddedTypesMap = new ConcurrentDictionary<TNamedTypeSymbol, TEmbeddedType>(ReferenceEqualityComparer.Instance);

		// Token: 0x0400126C RID: 4716
		public readonly ConcurrentDictionary<TFieldSymbol, TEmbeddedField> EmbeddedFieldsMap = new ConcurrentDictionary<TFieldSymbol, TEmbeddedField>(ReferenceEqualityComparer.Instance);

		// Token: 0x0400126D RID: 4717
		public readonly ConcurrentDictionary<TMethodSymbol, TEmbeddedMethod> EmbeddedMethodsMap = new ConcurrentDictionary<TMethodSymbol, TEmbeddedMethod>(ReferenceEqualityComparer.Instance);

		// Token: 0x0400126E RID: 4718
		public readonly ConcurrentDictionary<TPropertySymbol, TEmbeddedProperty> EmbeddedPropertiesMap = new ConcurrentDictionary<TPropertySymbol, TEmbeddedProperty>(ReferenceEqualityComparer.Instance);

		// Token: 0x0400126F RID: 4719
		public readonly ConcurrentDictionary<TEventSymbol, TEmbeddedEvent> EmbeddedEventsMap = new ConcurrentDictionary<TEventSymbol, TEmbeddedEvent>(ReferenceEqualityComparer.Instance);

		// Token: 0x04001270 RID: 4720
		private ImmutableArray<TEmbeddedType> _frozen;

		// Token: 0x0200073F RID: 1855
		internal abstract class CommonEmbeddedEvent : EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.CommonEmbeddedMember<TEventSymbol>, IEventDefinition, ITypeDefinitionMember, ITypeMemberReference, IReference, INamedEntity, IDefinition
		{
			// Token: 0x06003C1A RID: 15386 RVA: 0x000AC817 File Offset: 0x000AAA17
			protected CommonEmbeddedEvent(TEventSymbol underlyingEvent, TEmbeddedMethod adder, TEmbeddedMethod remover, TEmbeddedMethod caller)
				: base(underlyingEvent)
			{
				this._adder = adder;
				this._remover = remover;
				this._caller = caller;
			}

			// Token: 0x1700101A RID: 4122
			// (get) Token: 0x06003C1B RID: 15387 RVA: 0x000AC836 File Offset: 0x000AAA36
			internal override TEmbeddedTypesManager TypeManager
			{
				get
				{
					return this.AnAccessor.TypeManager;
				}
			}

			// Token: 0x1700101B RID: 4123
			// (get) Token: 0x06003C1C RID: 15388
			protected abstract bool IsRuntimeSpecial { get; }

			// Token: 0x1700101C RID: 4124
			// (get) Token: 0x06003C1D RID: 15389
			protected abstract bool IsSpecialName { get; }

			// Token: 0x06003C1E RID: 15390
			protected abstract ITypeReference GetType(TPEModuleBuilder moduleBuilder, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics);

			// Token: 0x1700101D RID: 4125
			// (get) Token: 0x06003C1F RID: 15391
			protected abstract TEmbeddedType ContainingType { get; }

			// Token: 0x1700101E RID: 4126
			// (get) Token: 0x06003C20 RID: 15392
			protected abstract TypeMemberVisibility Visibility { get; }

			// Token: 0x1700101F RID: 4127
			// (get) Token: 0x06003C21 RID: 15393
			protected abstract string Name { get; }

			// Token: 0x17001020 RID: 4128
			// (get) Token: 0x06003C22 RID: 15394 RVA: 0x000AC848 File Offset: 0x000AAA48
			public TEventSymbol UnderlyingEvent
			{
				get
				{
					return this.UnderlyingSymbol;
				}
			}

			// Token: 0x06003C23 RID: 15395
			protected abstract void EmbedCorrespondingComEventInterfaceMethodInternal(TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics, bool isUsedForComAwareEventBinding);

			// Token: 0x06003C24 RID: 15396 RVA: 0x000AC850 File Offset: 0x000AAA50
			internal void EmbedCorrespondingComEventInterfaceMethod(TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics, bool isUsedForComAwareEventBinding)
			{
				if (this._isUsedForComAwareEventBinding == 0 && (!isUsedForComAwareEventBinding || Interlocked.CompareExchange(ref this._isUsedForComAwareEventBinding, 1, 0) == 0))
				{
					this.EmbedCorrespondingComEventInterfaceMethodInternal(syntaxNodeOpt, diagnostics, isUsedForComAwareEventBinding);
				}
			}

			// Token: 0x17001021 RID: 4129
			// (get) Token: 0x06003C25 RID: 15397 RVA: 0x000AC875 File Offset: 0x000AAA75
			IMethodReference IEventDefinition.Adder
			{
				get
				{
					return this._adder;
				}
			}

			// Token: 0x17001022 RID: 4130
			// (get) Token: 0x06003C26 RID: 15398 RVA: 0x000AC882 File Offset: 0x000AAA82
			IMethodReference IEventDefinition.Remover
			{
				get
				{
					return this._remover;
				}
			}

			// Token: 0x17001023 RID: 4131
			// (get) Token: 0x06003C27 RID: 15399 RVA: 0x000AC88F File Offset: 0x000AAA8F
			IMethodReference IEventDefinition.Caller
			{
				get
				{
					return this._caller;
				}
			}

			// Token: 0x06003C28 RID: 15400 RVA: 0x000AC89C File Offset: 0x000AAA9C
			IEnumerable<IMethodReference> IEventDefinition.GetAccessors(EmitContext context)
			{
				if (this._adder != null)
				{
					yield return this._adder;
				}
				if (this._remover != null)
				{
					yield return this._remover;
				}
				if (this._caller != null)
				{
					yield return this._caller;
				}
				yield break;
			}

			// Token: 0x17001024 RID: 4132
			// (get) Token: 0x06003C29 RID: 15401 RVA: 0x000AC8AC File Offset: 0x000AAAAC
			bool IEventDefinition.IsRuntimeSpecial
			{
				get
				{
					return this.IsRuntimeSpecial;
				}
			}

			// Token: 0x17001025 RID: 4133
			// (get) Token: 0x06003C2A RID: 15402 RVA: 0x000AC8B4 File Offset: 0x000AAAB4
			bool IEventDefinition.IsSpecialName
			{
				get
				{
					return this.IsSpecialName;
				}
			}

			// Token: 0x06003C2B RID: 15403 RVA: 0x000AC8BC File Offset: 0x000AAABC
			ITypeReference IEventDefinition.GetType(EmitContext context)
			{
				return this.GetType((TPEModuleBuilder)((object)context.Module), (TSyntaxNode)((object)context.SyntaxNode), context.Diagnostics);
			}

			// Token: 0x17001026 RID: 4134
			// (get) Token: 0x06003C2C RID: 15404 RVA: 0x000AC8E0 File Offset: 0x000AAAE0
			protected TEmbeddedMethod AnAccessor
			{
				get
				{
					TEmbeddedMethod tembeddedMethod;
					if ((tembeddedMethod = this._adder) == null)
					{
						tembeddedMethod = this._remover;
					}
					return tembeddedMethod;
				}
			}

			// Token: 0x17001027 RID: 4135
			// (get) Token: 0x06003C2D RID: 15405 RVA: 0x000AC8F7 File Offset: 0x000AAAF7
			ITypeDefinition ITypeDefinitionMember.ContainingTypeDefinition
			{
				get
				{
					return this.ContainingType;
				}
			}

			// Token: 0x17001028 RID: 4136
			// (get) Token: 0x06003C2E RID: 15406 RVA: 0x000AC904 File Offset: 0x000AAB04
			TypeMemberVisibility ITypeDefinitionMember.Visibility
			{
				get
				{
					return this.Visibility;
				}
			}

			// Token: 0x06003C2F RID: 15407 RVA: 0x000AC90C File Offset: 0x000AAB0C
			ITypeReference ITypeMemberReference.GetContainingType(EmitContext context)
			{
				return this.ContainingType;
			}

			// Token: 0x06003C30 RID: 15408 RVA: 0x000AC919 File Offset: 0x000AAB19
			void IReference.Dispatch(MetadataVisitor visitor)
			{
				visitor.Visit(this);
			}

			// Token: 0x06003C31 RID: 15409 RVA: 0x000AC922 File Offset: 0x000AAB22
			IDefinition IReference.AsDefinition(EmitContext context)
			{
				return this;
			}

			// Token: 0x17001029 RID: 4137
			// (get) Token: 0x06003C32 RID: 15410 RVA: 0x000AC925 File Offset: 0x000AAB25
			string INamedEntity.Name
			{
				get
				{
					return this.Name;
				}
			}

			// Token: 0x04001CCB RID: 7371
			private readonly TEmbeddedMethod _adder;

			// Token: 0x04001CCC RID: 7372
			private readonly TEmbeddedMethod _remover;

			// Token: 0x04001CCD RID: 7373
			private readonly TEmbeddedMethod _caller;

			// Token: 0x04001CCE RID: 7374
			private int _isUsedForComAwareEventBinding;
		}

		// Token: 0x02000740 RID: 1856
		internal abstract class CommonEmbeddedField : EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.CommonEmbeddedMember<TFieldSymbol>, IFieldDefinition, ITypeDefinitionMember, ITypeMemberReference, IReference, INamedEntity, IDefinition, IFieldReference
		{
			// Token: 0x06003C33 RID: 15411 RVA: 0x000AC92D File Offset: 0x000AAB2D
			protected CommonEmbeddedField(TEmbeddedType containingType, TFieldSymbol underlyingField)
				: base(underlyingField)
			{
				this.ContainingType = containingType;
			}

			// Token: 0x1700102A RID: 4138
			// (get) Token: 0x06003C34 RID: 15412 RVA: 0x000AC93D File Offset: 0x000AAB3D
			public TFieldSymbol UnderlyingField
			{
				get
				{
					return this.UnderlyingSymbol;
				}
			}

			// Token: 0x06003C35 RID: 15413
			protected abstract MetadataConstant GetCompileTimeValue(EmitContext context);

			// Token: 0x1700102B RID: 4139
			// (get) Token: 0x06003C36 RID: 15414
			protected abstract bool IsCompileTimeConstant { get; }

			// Token: 0x1700102C RID: 4140
			// (get) Token: 0x06003C37 RID: 15415
			protected abstract bool IsNotSerialized { get; }

			// Token: 0x1700102D RID: 4141
			// (get) Token: 0x06003C38 RID: 15416
			protected abstract bool IsReadOnly { get; }

			// Token: 0x1700102E RID: 4142
			// (get) Token: 0x06003C39 RID: 15417
			protected abstract bool IsRuntimeSpecial { get; }

			// Token: 0x1700102F RID: 4143
			// (get) Token: 0x06003C3A RID: 15418
			protected abstract bool IsSpecialName { get; }

			// Token: 0x17001030 RID: 4144
			// (get) Token: 0x06003C3B RID: 15419
			protected abstract bool IsStatic { get; }

			// Token: 0x17001031 RID: 4145
			// (get) Token: 0x06003C3C RID: 15420
			protected abstract bool IsMarshalledExplicitly { get; }

			// Token: 0x17001032 RID: 4146
			// (get) Token: 0x06003C3D RID: 15421
			protected abstract IMarshallingInformation MarshallingInformation { get; }

			// Token: 0x17001033 RID: 4147
			// (get) Token: 0x06003C3E RID: 15422
			protected abstract ImmutableArray<byte> MarshallingDescriptor { get; }

			// Token: 0x17001034 RID: 4148
			// (get) Token: 0x06003C3F RID: 15423
			protected abstract int? TypeLayoutOffset { get; }

			// Token: 0x17001035 RID: 4149
			// (get) Token: 0x06003C40 RID: 15424
			protected abstract TypeMemberVisibility Visibility { get; }

			// Token: 0x17001036 RID: 4150
			// (get) Token: 0x06003C41 RID: 15425
			protected abstract string Name { get; }

			// Token: 0x06003C42 RID: 15426 RVA: 0x000AC945 File Offset: 0x000AAB45
			MetadataConstant IFieldDefinition.GetCompileTimeValue(EmitContext context)
			{
				return this.GetCompileTimeValue(context);
			}

			// Token: 0x17001037 RID: 4151
			// (get) Token: 0x06003C43 RID: 15427 RVA: 0x000AC950 File Offset: 0x000AAB50
			ImmutableArray<byte> IFieldDefinition.MappedData
			{
				get
				{
					return default(ImmutableArray<byte>);
				}
			}

			// Token: 0x17001038 RID: 4152
			// (get) Token: 0x06003C44 RID: 15428 RVA: 0x000AC966 File Offset: 0x000AAB66
			bool IFieldDefinition.IsCompileTimeConstant
			{
				get
				{
					return this.IsCompileTimeConstant;
				}
			}

			// Token: 0x17001039 RID: 4153
			// (get) Token: 0x06003C45 RID: 15429 RVA: 0x000AC96E File Offset: 0x000AAB6E
			bool IFieldDefinition.IsNotSerialized
			{
				get
				{
					return this.IsNotSerialized;
				}
			}

			// Token: 0x1700103A RID: 4154
			// (get) Token: 0x06003C46 RID: 15430 RVA: 0x000AC976 File Offset: 0x000AAB76
			bool IFieldDefinition.IsReadOnly
			{
				get
				{
					return this.IsReadOnly;
				}
			}

			// Token: 0x1700103B RID: 4155
			// (get) Token: 0x06003C47 RID: 15431 RVA: 0x000AC97E File Offset: 0x000AAB7E
			bool IFieldDefinition.IsRuntimeSpecial
			{
				get
				{
					return this.IsRuntimeSpecial;
				}
			}

			// Token: 0x1700103C RID: 4156
			// (get) Token: 0x06003C48 RID: 15432 RVA: 0x000AC986 File Offset: 0x000AAB86
			bool IFieldDefinition.IsSpecialName
			{
				get
				{
					return this.IsSpecialName;
				}
			}

			// Token: 0x1700103D RID: 4157
			// (get) Token: 0x06003C49 RID: 15433 RVA: 0x000AC98E File Offset: 0x000AAB8E
			bool IFieldDefinition.IsStatic
			{
				get
				{
					return this.IsStatic;
				}
			}

			// Token: 0x1700103E RID: 4158
			// (get) Token: 0x06003C4A RID: 15434 RVA: 0x000AC996 File Offset: 0x000AAB96
			bool IFieldDefinition.IsMarshalledExplicitly
			{
				get
				{
					return this.IsMarshalledExplicitly;
				}
			}

			// Token: 0x1700103F RID: 4159
			// (get) Token: 0x06003C4B RID: 15435 RVA: 0x000AC99E File Offset: 0x000AAB9E
			IMarshallingInformation IFieldDefinition.MarshallingInformation
			{
				get
				{
					return this.MarshallingInformation;
				}
			}

			// Token: 0x17001040 RID: 4160
			// (get) Token: 0x06003C4C RID: 15436 RVA: 0x000AC9A6 File Offset: 0x000AABA6
			ImmutableArray<byte> IFieldDefinition.MarshallingDescriptor
			{
				get
				{
					return this.MarshallingDescriptor;
				}
			}

			// Token: 0x17001041 RID: 4161
			// (get) Token: 0x06003C4D RID: 15437 RVA: 0x000AC9B0 File Offset: 0x000AABB0
			int IFieldDefinition.Offset
			{
				get
				{
					return this.TypeLayoutOffset.GetValueOrDefault();
				}
			}

			// Token: 0x17001042 RID: 4162
			// (get) Token: 0x06003C4E RID: 15438 RVA: 0x000AC9CB File Offset: 0x000AABCB
			ITypeDefinition ITypeDefinitionMember.ContainingTypeDefinition
			{
				get
				{
					return this.ContainingType;
				}
			}

			// Token: 0x17001043 RID: 4163
			// (get) Token: 0x06003C4F RID: 15439 RVA: 0x000AC9D8 File Offset: 0x000AABD8
			TypeMemberVisibility ITypeDefinitionMember.Visibility
			{
				get
				{
					return this.Visibility;
				}
			}

			// Token: 0x06003C50 RID: 15440 RVA: 0x000AC9E0 File Offset: 0x000AABE0
			ITypeReference ITypeMemberReference.GetContainingType(EmitContext context)
			{
				return this.ContainingType;
			}

			// Token: 0x06003C51 RID: 15441 RVA: 0x000AC9ED File Offset: 0x000AABED
			void IReference.Dispatch(MetadataVisitor visitor)
			{
				visitor.Visit(this);
			}

			// Token: 0x06003C52 RID: 15442 RVA: 0x000AC9F6 File Offset: 0x000AABF6
			IDefinition IReference.AsDefinition(EmitContext context)
			{
				return this;
			}

			// Token: 0x17001044 RID: 4164
			// (get) Token: 0x06003C53 RID: 15443 RVA: 0x000AC9F9 File Offset: 0x000AABF9
			string INamedEntity.Name
			{
				get
				{
					return this.Name;
				}
			}

			// Token: 0x06003C54 RID: 15444 RVA: 0x000ACA01 File Offset: 0x000AAC01
			ITypeReference IFieldReference.GetType(EmitContext context)
			{
				return this.UnderlyingField.GetType(context);
			}

			// Token: 0x06003C55 RID: 15445 RVA: 0x000ACA14 File Offset: 0x000AAC14
			IFieldDefinition IFieldReference.GetResolvedField(EmitContext context)
			{
				return this;
			}

			// Token: 0x17001045 RID: 4165
			// (get) Token: 0x06003C56 RID: 15446 RVA: 0x000ACA17 File Offset: 0x000AAC17
			ISpecializedFieldReference IFieldReference.AsSpecializedFieldReference
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001046 RID: 4166
			// (get) Token: 0x06003C57 RID: 15447 RVA: 0x000ACA1A File Offset: 0x000AAC1A
			bool IFieldReference.IsContextualNamedEntity
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04001CCF RID: 7375
			public readonly TEmbeddedType ContainingType;
		}

		// Token: 0x02000741 RID: 1857
		internal abstract class CommonEmbeddedMember
		{
			// Token: 0x17001047 RID: 4167
			// (get) Token: 0x06003C58 RID: 15448
			internal abstract TEmbeddedTypesManager TypeManager { get; }
		}

		// Token: 0x02000742 RID: 1858
		internal abstract class CommonEmbeddedMember<TMember> : EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.CommonEmbeddedMember, IReference where TMember : TSymbol, ITypeMemberReference
		{
			// Token: 0x06003C5A RID: 15450 RVA: 0x000ACA25 File Offset: 0x000AAC25
			protected CommonEmbeddedMember(TMember underlyingSymbol)
			{
				this.UnderlyingSymbol = underlyingSymbol;
			}

			// Token: 0x06003C5B RID: 15451
			protected abstract IEnumerable<TAttributeData> GetCustomAttributesToEmit(TPEModuleBuilder moduleBuilder);

			// Token: 0x06003C5C RID: 15452 RVA: 0x000ACA34 File Offset: 0x000AAC34
			protected virtual TAttributeData PortAttributeIfNeedTo(TAttributeData attrData, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
			{
				return default(TAttributeData);
			}

			// Token: 0x06003C5D RID: 15453 RVA: 0x000ACA4C File Offset: 0x000AAC4C
			private ImmutableArray<TAttributeData> GetAttributes(TPEModuleBuilder moduleBuilder, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
			{
				ArrayBuilder<TAttributeData> instance = ArrayBuilder<TAttributeData>.GetInstance();
				foreach (TAttributeData tattributeData in this.GetCustomAttributesToEmit(moduleBuilder))
				{
					if (this.TypeManager.IsTargetAttribute((TSymbol)((object)this.UnderlyingSymbol), tattributeData, AttributeDescription.DispIdAttribute))
					{
						if (tattributeData.CommonConstructorArguments.Length == 1)
						{
							instance.AddOptional(this.TypeManager.CreateSynthesizedAttribute(WellKnownMember.System_Runtime_InteropServices_DispIdAttribute__ctor, tattributeData, syntaxNodeOpt, diagnostics));
						}
					}
					else
					{
						instance.AddOptional(this.PortAttributeIfNeedTo(tattributeData, syntaxNodeOpt, diagnostics));
					}
				}
				return instance.ToImmutableAndFree();
			}

			// Token: 0x06003C5E RID: 15454 RVA: 0x000ACB0C File Offset: 0x000AAD0C
			IEnumerable<ICustomAttribute> IReference.GetAttributes(EmitContext context)
			{
				if (this._lazyAttributes.IsDefault)
				{
					DiagnosticBag instance = DiagnosticBag.GetInstance();
					ImmutableArray<TAttributeData> attributes = this.GetAttributes((TPEModuleBuilder)((object)context.Module), (TSyntaxNode)((object)context.SyntaxNode), instance);
					if (ImmutableInterlocked.InterlockedInitialize<TAttributeData>(ref this._lazyAttributes, attributes))
					{
						context.Diagnostics.AddRange(instance);
					}
					instance.Free();
				}
				return this._lazyAttributes;
			}

			// Token: 0x06003C5F RID: 15455 RVA: 0x000ACB75 File Offset: 0x000AAD75
			void IReference.Dispatch(MetadataVisitor visitor)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06003C60 RID: 15456 RVA: 0x000ACB7C File Offset: 0x000AAD7C
			IDefinition IReference.AsDefinition(EmitContext context)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06003C61 RID: 15457 RVA: 0x000ACB83 File Offset: 0x000AAD83
			ISymbolInternal IReference.GetInternalSymbol()
			{
				return null;
			}

			// Token: 0x06003C62 RID: 15458 RVA: 0x000ACB86 File Offset: 0x000AAD86
			public sealed override bool Equals(object obj)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06003C63 RID: 15459 RVA: 0x000ACB8D File Offset: 0x000AAD8D
			public sealed override int GetHashCode()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x04001CD0 RID: 7376
			protected readonly TMember UnderlyingSymbol;

			// Token: 0x04001CD1 RID: 7377
			private ImmutableArray<TAttributeData> _lazyAttributes;
		}

		// Token: 0x02000743 RID: 1859
		internal abstract class CommonEmbeddedMethod : EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.CommonEmbeddedMember<TMethodSymbol>, IMethodDefinition, ITypeDefinitionMember, ITypeMemberReference, IReference, INamedEntity, IDefinition, IMethodReference, ISignature
		{
			// Token: 0x06003C64 RID: 15460 RVA: 0x000ACB94 File Offset: 0x000AAD94
			protected CommonEmbeddedMethod(TEmbeddedType containingType, TMethodSymbol underlyingMethod)
				: base(underlyingMethod)
			{
				this.ContainingType = containingType;
				this._typeParameters = this.GetTypeParameters();
				this._parameters = this.GetParameters();
			}

			// Token: 0x06003C65 RID: 15461
			protected abstract ImmutableArray<TEmbeddedTypeParameter> GetTypeParameters();

			// Token: 0x06003C66 RID: 15462
			protected abstract ImmutableArray<TEmbeddedParameter> GetParameters();

			// Token: 0x17001048 RID: 4168
			// (get) Token: 0x06003C67 RID: 15463
			protected abstract bool IsAbstract { get; }

			// Token: 0x17001049 RID: 4169
			// (get) Token: 0x06003C68 RID: 15464
			protected abstract bool IsAccessCheckedOnOverride { get; }

			// Token: 0x1700104A RID: 4170
			// (get) Token: 0x06003C69 RID: 15465
			protected abstract bool IsConstructor { get; }

			// Token: 0x1700104B RID: 4171
			// (get) Token: 0x06003C6A RID: 15466
			protected abstract bool IsExternal { get; }

			// Token: 0x1700104C RID: 4172
			// (get) Token: 0x06003C6B RID: 15467
			protected abstract bool IsHiddenBySignature { get; }

			// Token: 0x1700104D RID: 4173
			// (get) Token: 0x06003C6C RID: 15468
			protected abstract bool IsNewSlot { get; }

			// Token: 0x1700104E RID: 4174
			// (get) Token: 0x06003C6D RID: 15469
			protected abstract IPlatformInvokeInformation PlatformInvokeData { get; }

			// Token: 0x1700104F RID: 4175
			// (get) Token: 0x06003C6E RID: 15470
			protected abstract bool IsRuntimeSpecial { get; }

			// Token: 0x17001050 RID: 4176
			// (get) Token: 0x06003C6F RID: 15471
			protected abstract bool IsSpecialName { get; }

			// Token: 0x17001051 RID: 4177
			// (get) Token: 0x06003C70 RID: 15472
			protected abstract bool IsSealed { get; }

			// Token: 0x17001052 RID: 4178
			// (get) Token: 0x06003C71 RID: 15473
			protected abstract bool IsStatic { get; }

			// Token: 0x17001053 RID: 4179
			// (get) Token: 0x06003C72 RID: 15474
			protected abstract bool IsVirtual { get; }

			// Token: 0x06003C73 RID: 15475
			protected abstract MethodImplAttributes GetImplementationAttributes(EmitContext context);

			// Token: 0x17001054 RID: 4180
			// (get) Token: 0x06003C74 RID: 15476
			protected abstract bool ReturnValueIsMarshalledExplicitly { get; }

			// Token: 0x17001055 RID: 4181
			// (get) Token: 0x06003C75 RID: 15477
			protected abstract IMarshallingInformation ReturnValueMarshallingInformation { get; }

			// Token: 0x17001056 RID: 4182
			// (get) Token: 0x06003C76 RID: 15478
			protected abstract ImmutableArray<byte> ReturnValueMarshallingDescriptor { get; }

			// Token: 0x17001057 RID: 4183
			// (get) Token: 0x06003C77 RID: 15479
			protected abstract TypeMemberVisibility Visibility { get; }

			// Token: 0x17001058 RID: 4184
			// (get) Token: 0x06003C78 RID: 15480
			protected abstract string Name { get; }

			// Token: 0x17001059 RID: 4185
			// (get) Token: 0x06003C79 RID: 15481
			protected abstract bool AcceptsExtraArguments { get; }

			// Token: 0x1700105A RID: 4186
			// (get) Token: 0x06003C7A RID: 15482
			protected abstract ISignature UnderlyingMethodSignature { get; }

			// Token: 0x1700105B RID: 4187
			// (get) Token: 0x06003C7B RID: 15483
			protected abstract INamespace ContainingNamespace { get; }

			// Token: 0x1700105C RID: 4188
			// (get) Token: 0x06003C7C RID: 15484 RVA: 0x000ACBBC File Offset: 0x000AADBC
			public TMethodSymbol UnderlyingMethod
			{
				get
				{
					return this.UnderlyingSymbol;
				}
			}

			// Token: 0x06003C7D RID: 15485 RVA: 0x000ACBC4 File Offset: 0x000AADC4
			protected sealed override TAttributeData PortAttributeIfNeedTo(TAttributeData attrData, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
			{
				if (this.TypeManager.IsTargetAttribute((TSymbol)((object)this.UnderlyingMethod), attrData, AttributeDescription.LCIDConversionAttribute) && attrData.CommonConstructorArguments.Length == 1)
				{
					return this.TypeManager.CreateSynthesizedAttribute(WellKnownMember.System_Runtime_InteropServices_LCIDConversionAttribute__ctor, attrData, syntaxNodeOpt, diagnostics);
				}
				return default(TAttributeData);
			}

			// Token: 0x06003C7E RID: 15486 RVA: 0x000ACC2E File Offset: 0x000AAE2E
			IMethodBody IMethodDefinition.GetBody(EmitContext context)
			{
				if (this.HasBody())
				{
					return new EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.CommonEmbeddedMethod.EmptyBody(this);
				}
				return null;
			}

			// Token: 0x1700105D RID: 4189
			// (get) Token: 0x06003C7F RID: 15487 RVA: 0x000ACC40 File Offset: 0x000AAE40
			IEnumerable<IGenericMethodParameter> IMethodDefinition.GenericParameters
			{
				get
				{
					return this._typeParameters;
				}
			}

			// Token: 0x1700105E RID: 4190
			// (get) Token: 0x06003C80 RID: 15488 RVA: 0x000ACC4D File Offset: 0x000AAE4D
			bool IMethodDefinition.HasDeclarativeSecurity
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700105F RID: 4191
			// (get) Token: 0x06003C81 RID: 15489 RVA: 0x000ACC50 File Offset: 0x000AAE50
			bool IMethodDefinition.IsAbstract
			{
				get
				{
					return this.IsAbstract;
				}
			}

			// Token: 0x17001060 RID: 4192
			// (get) Token: 0x06003C82 RID: 15490 RVA: 0x000ACC58 File Offset: 0x000AAE58
			bool IMethodDefinition.IsAccessCheckedOnOverride
			{
				get
				{
					return this.IsAccessCheckedOnOverride;
				}
			}

			// Token: 0x17001061 RID: 4193
			// (get) Token: 0x06003C83 RID: 15491 RVA: 0x000ACC60 File Offset: 0x000AAE60
			bool IMethodDefinition.IsConstructor
			{
				get
				{
					return this.IsConstructor;
				}
			}

			// Token: 0x17001062 RID: 4194
			// (get) Token: 0x06003C84 RID: 15492 RVA: 0x000ACC68 File Offset: 0x000AAE68
			bool IMethodDefinition.IsExternal
			{
				get
				{
					return this.IsExternal;
				}
			}

			// Token: 0x17001063 RID: 4195
			// (get) Token: 0x06003C85 RID: 15493 RVA: 0x000ACC70 File Offset: 0x000AAE70
			bool IMethodDefinition.IsHiddenBySignature
			{
				get
				{
					return this.IsHiddenBySignature;
				}
			}

			// Token: 0x17001064 RID: 4196
			// (get) Token: 0x06003C86 RID: 15494 RVA: 0x000ACC78 File Offset: 0x000AAE78
			bool IMethodDefinition.IsNewSlot
			{
				get
				{
					return this.IsNewSlot;
				}
			}

			// Token: 0x17001065 RID: 4197
			// (get) Token: 0x06003C87 RID: 15495 RVA: 0x000ACC80 File Offset: 0x000AAE80
			bool IMethodDefinition.IsPlatformInvoke
			{
				get
				{
					return this.PlatformInvokeData != null;
				}
			}

			// Token: 0x17001066 RID: 4198
			// (get) Token: 0x06003C88 RID: 15496 RVA: 0x000ACC8B File Offset: 0x000AAE8B
			IPlatformInvokeInformation IMethodDefinition.PlatformInvokeData
			{
				get
				{
					return this.PlatformInvokeData;
				}
			}

			// Token: 0x17001067 RID: 4199
			// (get) Token: 0x06003C89 RID: 15497 RVA: 0x000ACC93 File Offset: 0x000AAE93
			bool IMethodDefinition.IsRuntimeSpecial
			{
				get
				{
					return this.IsRuntimeSpecial;
				}
			}

			// Token: 0x17001068 RID: 4200
			// (get) Token: 0x06003C8A RID: 15498 RVA: 0x000ACC9B File Offset: 0x000AAE9B
			bool IMethodDefinition.IsSpecialName
			{
				get
				{
					return this.IsSpecialName;
				}
			}

			// Token: 0x17001069 RID: 4201
			// (get) Token: 0x06003C8B RID: 15499 RVA: 0x000ACCA3 File Offset: 0x000AAEA3
			bool IMethodDefinition.IsSealed
			{
				get
				{
					return this.IsSealed;
				}
			}

			// Token: 0x1700106A RID: 4202
			// (get) Token: 0x06003C8C RID: 15500 RVA: 0x000ACCAB File Offset: 0x000AAEAB
			bool IMethodDefinition.IsStatic
			{
				get
				{
					return this.IsStatic;
				}
			}

			// Token: 0x1700106B RID: 4203
			// (get) Token: 0x06003C8D RID: 15501 RVA: 0x000ACCB3 File Offset: 0x000AAEB3
			bool IMethodDefinition.IsVirtual
			{
				get
				{
					return this.IsVirtual;
				}
			}

			// Token: 0x06003C8E RID: 15502 RVA: 0x000ACCBB File Offset: 0x000AAEBB
			MethodImplAttributes IMethodDefinition.GetImplementationAttributes(EmitContext context)
			{
				return this.GetImplementationAttributes(context);
			}

			// Token: 0x1700106C RID: 4204
			// (get) Token: 0x06003C8F RID: 15503 RVA: 0x000ACCC4 File Offset: 0x000AAEC4
			ImmutableArray<IParameterDefinition> IMethodDefinition.Parameters
			{
				get
				{
					return StaticCast<IParameterDefinition>.From<TEmbeddedParameter>(this._parameters);
				}
			}

			// Token: 0x1700106D RID: 4205
			// (get) Token: 0x06003C90 RID: 15504 RVA: 0x000ACCD1 File Offset: 0x000AAED1
			bool IMethodDefinition.RequiresSecurityObject
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06003C91 RID: 15505 RVA: 0x000ACCD4 File Offset: 0x000AAED4
			IEnumerable<ICustomAttribute> IMethodDefinition.GetReturnValueAttributes(EmitContext context)
			{
				return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
			}

			// Token: 0x1700106E RID: 4206
			// (get) Token: 0x06003C92 RID: 15506 RVA: 0x000ACCDB File Offset: 0x000AAEDB
			bool IMethodDefinition.ReturnValueIsMarshalledExplicitly
			{
				get
				{
					return this.ReturnValueIsMarshalledExplicitly;
				}
			}

			// Token: 0x1700106F RID: 4207
			// (get) Token: 0x06003C93 RID: 15507 RVA: 0x000ACCE3 File Offset: 0x000AAEE3
			IMarshallingInformation IMethodDefinition.ReturnValueMarshallingInformation
			{
				get
				{
					return this.ReturnValueMarshallingInformation;
				}
			}

			// Token: 0x17001070 RID: 4208
			// (get) Token: 0x06003C94 RID: 15508 RVA: 0x000ACCEB File Offset: 0x000AAEEB
			ImmutableArray<byte> IMethodDefinition.ReturnValueMarshallingDescriptor
			{
				get
				{
					return this.ReturnValueMarshallingDescriptor;
				}
			}

			// Token: 0x17001071 RID: 4209
			// (get) Token: 0x06003C95 RID: 15509 RVA: 0x000ACCF3 File Offset: 0x000AAEF3
			IEnumerable<SecurityAttribute> IMethodDefinition.SecurityAttributes
			{
				get
				{
					return SpecializedCollections.EmptyEnumerable<SecurityAttribute>();
				}
			}

			// Token: 0x17001072 RID: 4210
			// (get) Token: 0x06003C96 RID: 15510 RVA: 0x000ACCFA File Offset: 0x000AAEFA
			ITypeDefinition ITypeDefinitionMember.ContainingTypeDefinition
			{
				get
				{
					return this.ContainingType;
				}
			}

			// Token: 0x17001073 RID: 4211
			// (get) Token: 0x06003C97 RID: 15511 RVA: 0x000ACD07 File Offset: 0x000AAF07
			INamespace IMethodDefinition.ContainingNamespace
			{
				get
				{
					return this.ContainingNamespace;
				}
			}

			// Token: 0x17001074 RID: 4212
			// (get) Token: 0x06003C98 RID: 15512 RVA: 0x000ACD0F File Offset: 0x000AAF0F
			TypeMemberVisibility ITypeDefinitionMember.Visibility
			{
				get
				{
					return this.Visibility;
				}
			}

			// Token: 0x06003C99 RID: 15513 RVA: 0x000ACD17 File Offset: 0x000AAF17
			ITypeReference ITypeMemberReference.GetContainingType(EmitContext context)
			{
				return this.ContainingType;
			}

			// Token: 0x06003C9A RID: 15514 RVA: 0x000ACD24 File Offset: 0x000AAF24
			void IReference.Dispatch(MetadataVisitor visitor)
			{
				visitor.Visit(this);
			}

			// Token: 0x06003C9B RID: 15515 RVA: 0x000ACD2D File Offset: 0x000AAF2D
			IDefinition IReference.AsDefinition(EmitContext context)
			{
				return this;
			}

			// Token: 0x17001075 RID: 4213
			// (get) Token: 0x06003C9C RID: 15516 RVA: 0x000ACD30 File Offset: 0x000AAF30
			string INamedEntity.Name
			{
				get
				{
					return this.Name;
				}
			}

			// Token: 0x17001076 RID: 4214
			// (get) Token: 0x06003C9D RID: 15517 RVA: 0x000ACD38 File Offset: 0x000AAF38
			bool IMethodReference.AcceptsExtraArguments
			{
				get
				{
					return this.AcceptsExtraArguments;
				}
			}

			// Token: 0x17001077 RID: 4215
			// (get) Token: 0x06003C9E RID: 15518 RVA: 0x000ACD40 File Offset: 0x000AAF40
			ushort IMethodReference.GenericParameterCount
			{
				get
				{
					return (ushort)this._typeParameters.Length;
				}
			}

			// Token: 0x17001078 RID: 4216
			// (get) Token: 0x06003C9F RID: 15519 RVA: 0x000ACD5C File Offset: 0x000AAF5C
			bool IMethodReference.IsGeneric
			{
				get
				{
					return this._typeParameters.Length > 0;
				}
			}

			// Token: 0x06003CA0 RID: 15520 RVA: 0x000ACD7A File Offset: 0x000AAF7A
			IMethodDefinition IMethodReference.GetResolvedMethod(EmitContext context)
			{
				return this;
			}

			// Token: 0x17001079 RID: 4217
			// (get) Token: 0x06003CA1 RID: 15521 RVA: 0x000ACD7D File Offset: 0x000AAF7D
			ImmutableArray<IParameterTypeInformation> IMethodReference.ExtraParameters
			{
				get
				{
					return ImmutableArray<IParameterTypeInformation>.Empty;
				}
			}

			// Token: 0x1700107A RID: 4218
			// (get) Token: 0x06003CA2 RID: 15522 RVA: 0x000ACD84 File Offset: 0x000AAF84
			IGenericMethodInstanceReference IMethodReference.AsGenericMethodInstanceReference
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700107B RID: 4219
			// (get) Token: 0x06003CA3 RID: 15523 RVA: 0x000ACD87 File Offset: 0x000AAF87
			ISpecializedMethodReference IMethodReference.AsSpecializedMethodReference
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700107C RID: 4220
			// (get) Token: 0x06003CA4 RID: 15524 RVA: 0x000ACD8A File Offset: 0x000AAF8A
			CallingConvention ISignature.CallingConvention
			{
				get
				{
					return this.UnderlyingMethodSignature.CallingConvention;
				}
			}

			// Token: 0x1700107D RID: 4221
			// (get) Token: 0x06003CA5 RID: 15525 RVA: 0x000ACD98 File Offset: 0x000AAF98
			ushort ISignature.ParameterCount
			{
				get
				{
					return (ushort)this._parameters.Length;
				}
			}

			// Token: 0x06003CA6 RID: 15526 RVA: 0x000ACDB4 File Offset: 0x000AAFB4
			ImmutableArray<IParameterTypeInformation> ISignature.GetParameters(EmitContext context)
			{
				return StaticCast<IParameterTypeInformation>.From<TEmbeddedParameter>(this._parameters);
			}

			// Token: 0x1700107E RID: 4222
			// (get) Token: 0x06003CA7 RID: 15527 RVA: 0x000ACDC1 File Offset: 0x000AAFC1
			ImmutableArray<ICustomModifier> ISignature.RefCustomModifiers
			{
				get
				{
					return this.UnderlyingMethodSignature.RefCustomModifiers;
				}
			}

			// Token: 0x1700107F RID: 4223
			// (get) Token: 0x06003CA8 RID: 15528 RVA: 0x000ACDCE File Offset: 0x000AAFCE
			ImmutableArray<ICustomModifier> ISignature.ReturnValueCustomModifiers
			{
				get
				{
					return this.UnderlyingMethodSignature.ReturnValueCustomModifiers;
				}
			}

			// Token: 0x17001080 RID: 4224
			// (get) Token: 0x06003CA9 RID: 15529 RVA: 0x000ACDDB File Offset: 0x000AAFDB
			bool ISignature.ReturnValueIsByRef
			{
				get
				{
					return this.UnderlyingMethodSignature.ReturnValueIsByRef;
				}
			}

			// Token: 0x06003CAA RID: 15530 RVA: 0x000ACDE8 File Offset: 0x000AAFE8
			ITypeReference ISignature.GetType(EmitContext context)
			{
				return this.UnderlyingMethodSignature.GetType(context);
			}

			// Token: 0x06003CAB RID: 15531 RVA: 0x000ACDF6 File Offset: 0x000AAFF6
			public override string ToString()
			{
				return this.UnderlyingMethod.GetInternalSymbol().GetISymbol().ToDisplayString(SymbolDisplayFormat.ILVisualizationFormat);
			}

			// Token: 0x04001CD2 RID: 7378
			public readonly TEmbeddedType ContainingType;

			// Token: 0x04001CD3 RID: 7379
			private readonly ImmutableArray<TEmbeddedTypeParameter> _typeParameters;

			// Token: 0x04001CD4 RID: 7380
			private readonly ImmutableArray<TEmbeddedParameter> _parameters;

			// Token: 0x020007F3 RID: 2035
			private sealed class EmptyBody : IMethodBody
			{
				// Token: 0x0600407F RID: 16511 RVA: 0x000B442F File Offset: 0x000B262F
				public EmptyBody(EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.CommonEmbeddedMethod method)
				{
					this._method = method;
				}

				// Token: 0x1700119E RID: 4510
				// (get) Token: 0x06004080 RID: 16512 RVA: 0x000B443E File Offset: 0x000B263E
				ImmutableArray<ExceptionHandlerRegion> IMethodBody.ExceptionRegions
				{
					get
					{
						return ImmutableArray<ExceptionHandlerRegion>.Empty;
					}
				}

				// Token: 0x1700119F RID: 4511
				// (get) Token: 0x06004081 RID: 16513 RVA: 0x000B4445 File Offset: 0x000B2645
				bool IMethodBody.HasStackalloc
				{
					get
					{
						return false;
					}
				}

				// Token: 0x170011A0 RID: 4512
				// (get) Token: 0x06004082 RID: 16514 RVA: 0x000B4448 File Offset: 0x000B2648
				bool IMethodBody.AreLocalsZeroed
				{
					get
					{
						return false;
					}
				}

				// Token: 0x170011A1 RID: 4513
				// (get) Token: 0x06004083 RID: 16515 RVA: 0x000B444B File Offset: 0x000B264B
				ImmutableArray<ILocalDefinition> IMethodBody.LocalVariables
				{
					get
					{
						return ImmutableArray<ILocalDefinition>.Empty;
					}
				}

				// Token: 0x170011A2 RID: 4514
				// (get) Token: 0x06004084 RID: 16516 RVA: 0x000B4452 File Offset: 0x000B2652
				IMethodDefinition IMethodBody.MethodDefinition
				{
					get
					{
						return this._method;
					}
				}

				// Token: 0x170011A3 RID: 4515
				// (get) Token: 0x06004085 RID: 16517 RVA: 0x000B445A File Offset: 0x000B265A
				ushort IMethodBody.MaxStack
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x170011A4 RID: 4516
				// (get) Token: 0x06004086 RID: 16518 RVA: 0x000B445D File Offset: 0x000B265D
				ImmutableArray<byte> IMethodBody.IL
				{
					get
					{
						return ImmutableArray<byte>.Empty;
					}
				}

				// Token: 0x170011A5 RID: 4517
				// (get) Token: 0x06004087 RID: 16519 RVA: 0x000B4464 File Offset: 0x000B2664
				ImmutableArray<Microsoft.Cci.SequencePoint> IMethodBody.SequencePoints
				{
					get
					{
						return ImmutableArray<Microsoft.Cci.SequencePoint>.Empty;
					}
				}

				// Token: 0x170011A6 RID: 4518
				// (get) Token: 0x06004088 RID: 16520 RVA: 0x000B446B File Offset: 0x000B266B
				bool IMethodBody.HasDynamicLocalVariables
				{
					get
					{
						return false;
					}
				}

				// Token: 0x170011A7 RID: 4519
				// (get) Token: 0x06004089 RID: 16521 RVA: 0x000B446E File Offset: 0x000B266E
				StateMachineMoveNextBodyDebugInfo IMethodBody.MoveNextBodyInfo
				{
					get
					{
						return null;
					}
				}

				// Token: 0x170011A8 RID: 4520
				// (get) Token: 0x0600408A RID: 16522 RVA: 0x000B4471 File Offset: 0x000B2671
				DynamicAnalysisMethodBodyData IMethodBody.DynamicAnalysisData
				{
					get
					{
						return null;
					}
				}

				// Token: 0x170011A9 RID: 4521
				// (get) Token: 0x0600408B RID: 16523 RVA: 0x000B4474 File Offset: 0x000B2674
				ImmutableArray<Microsoft.Cci.LocalScope> IMethodBody.LocalScopes
				{
					get
					{
						return ImmutableArray<Microsoft.Cci.LocalScope>.Empty;
					}
				}

				// Token: 0x170011AA RID: 4522
				// (get) Token: 0x0600408C RID: 16524 RVA: 0x000B447B File Offset: 0x000B267B
				IImportScope IMethodBody.ImportScope
				{
					get
					{
						return null;
					}
				}

				// Token: 0x170011AB RID: 4523
				// (get) Token: 0x0600408D RID: 16525 RVA: 0x000B4480 File Offset: 0x000B2680
				ImmutableArray<StateMachineHoistedLocalScope> IMethodBody.StateMachineHoistedLocalScopes
				{
					get
					{
						return default(ImmutableArray<StateMachineHoistedLocalScope>);
					}
				}

				// Token: 0x170011AC RID: 4524
				// (get) Token: 0x0600408E RID: 16526 RVA: 0x000B4496 File Offset: 0x000B2696
				string IMethodBody.StateMachineTypeName
				{
					get
					{
						return null;
					}
				}

				// Token: 0x170011AD RID: 4525
				// (get) Token: 0x0600408F RID: 16527 RVA: 0x000B449C File Offset: 0x000B269C
				ImmutableArray<EncHoistedLocalInfo> IMethodBody.StateMachineHoistedLocalSlots
				{
					get
					{
						return default(ImmutableArray<EncHoistedLocalInfo>);
					}
				}

				// Token: 0x170011AE RID: 4526
				// (get) Token: 0x06004090 RID: 16528 RVA: 0x000B44B4 File Offset: 0x000B26B4
				ImmutableArray<ITypeReference> IMethodBody.StateMachineAwaiterSlots
				{
					get
					{
						return default(ImmutableArray<ITypeReference>);
					}
				}

				// Token: 0x170011AF RID: 4527
				// (get) Token: 0x06004091 RID: 16529 RVA: 0x000B44CC File Offset: 0x000B26CC
				ImmutableArray<ClosureDebugInfo> IMethodBody.ClosureDebugInfo
				{
					get
					{
						return default(ImmutableArray<ClosureDebugInfo>);
					}
				}

				// Token: 0x170011B0 RID: 4528
				// (get) Token: 0x06004092 RID: 16530 RVA: 0x000B44E4 File Offset: 0x000B26E4
				ImmutableArray<LambdaDebugInfo> IMethodBody.LambdaDebugInfo
				{
					get
					{
						return default(ImmutableArray<LambdaDebugInfo>);
					}
				}

				// Token: 0x170011B1 RID: 4529
				// (get) Token: 0x06004093 RID: 16531 RVA: 0x000B44FC File Offset: 0x000B26FC
				public DebugId MethodId
				{
					get
					{
						return default(DebugId);
					}
				}

				// Token: 0x04001E65 RID: 7781
				private readonly EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.CommonEmbeddedMethod _method;
			}
		}

		// Token: 0x02000744 RID: 1860
		internal abstract class CommonEmbeddedParameter : IParameterDefinition, IDefinition, IReference, INamedEntity, IParameterTypeInformation, IParameterListEntry
		{
			// Token: 0x06003CAC RID: 15532 RVA: 0x000ACE17 File Offset: 0x000AB017
			protected CommonEmbeddedParameter(EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.CommonEmbeddedMember containingPropertyOrMethod, TParameterSymbol underlyingParameter)
			{
				this.ContainingPropertyOrMethod = containingPropertyOrMethod;
				this.UnderlyingParameter = underlyingParameter;
			}

			// Token: 0x17001081 RID: 4225
			// (get) Token: 0x06003CAD RID: 15533 RVA: 0x000ACE2D File Offset: 0x000AB02D
			protected TEmbeddedTypesManager TypeManager
			{
				get
				{
					return this.ContainingPropertyOrMethod.TypeManager;
				}
			}

			// Token: 0x17001082 RID: 4226
			// (get) Token: 0x06003CAE RID: 15534
			protected abstract bool HasDefaultValue { get; }

			// Token: 0x06003CAF RID: 15535
			protected abstract MetadataConstant GetDefaultValue(EmitContext context);

			// Token: 0x17001083 RID: 4227
			// (get) Token: 0x06003CB0 RID: 15536
			protected abstract bool IsIn { get; }

			// Token: 0x17001084 RID: 4228
			// (get) Token: 0x06003CB1 RID: 15537
			protected abstract bool IsOut { get; }

			// Token: 0x17001085 RID: 4229
			// (get) Token: 0x06003CB2 RID: 15538
			protected abstract bool IsOptional { get; }

			// Token: 0x17001086 RID: 4230
			// (get) Token: 0x06003CB3 RID: 15539
			protected abstract bool IsMarshalledExplicitly { get; }

			// Token: 0x17001087 RID: 4231
			// (get) Token: 0x06003CB4 RID: 15540
			protected abstract IMarshallingInformation MarshallingInformation { get; }

			// Token: 0x17001088 RID: 4232
			// (get) Token: 0x06003CB5 RID: 15541
			protected abstract ImmutableArray<byte> MarshallingDescriptor { get; }

			// Token: 0x17001089 RID: 4233
			// (get) Token: 0x06003CB6 RID: 15542
			protected abstract string Name { get; }

			// Token: 0x1700108A RID: 4234
			// (get) Token: 0x06003CB7 RID: 15543
			protected abstract IParameterTypeInformation UnderlyingParameterTypeInformation { get; }

			// Token: 0x1700108B RID: 4235
			// (get) Token: 0x06003CB8 RID: 15544
			protected abstract ushort Index { get; }

			// Token: 0x06003CB9 RID: 15545
			protected abstract IEnumerable<TAttributeData> GetCustomAttributesToEmit(TPEModuleBuilder moduleBuilder);

			// Token: 0x06003CBA RID: 15546 RVA: 0x000ACE3A File Offset: 0x000AB03A
			private bool IsTargetAttribute(TAttributeData attrData, AttributeDescription description)
			{
				return this.TypeManager.IsTargetAttribute((TSymbol)((object)this.UnderlyingParameter), attrData, description);
			}

			// Token: 0x06003CBB RID: 15547 RVA: 0x000ACE60 File Offset: 0x000AB060
			private ImmutableArray<TAttributeData> GetAttributes(TPEModuleBuilder moduleBuilder, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
			{
				ArrayBuilder<TAttributeData> instance = ArrayBuilder<TAttributeData>.GetInstance();
				foreach (TAttributeData tattributeData in this.GetCustomAttributesToEmit(moduleBuilder))
				{
					if (this.IsTargetAttribute(tattributeData, AttributeDescription.ParamArrayAttribute))
					{
						if (tattributeData.CommonConstructorArguments.Length == 0)
						{
							instance.AddOptional(this.TypeManager.CreateSynthesizedAttribute(WellKnownMember.System_ParamArrayAttribute__ctor, tattributeData, syntaxNodeOpt, diagnostics));
						}
					}
					else if (this.IsTargetAttribute(tattributeData, AttributeDescription.DateTimeConstantAttribute))
					{
						if (tattributeData.CommonConstructorArguments.Length == 1)
						{
							instance.AddOptional(this.TypeManager.CreateSynthesizedAttribute(WellKnownMember.System_Runtime_CompilerServices_DateTimeConstantAttribute__ctor, tattributeData, syntaxNodeOpt, diagnostics));
						}
					}
					else
					{
						int targetAttributeSignatureIndex = this.TypeManager.GetTargetAttributeSignatureIndex((TSymbol)((object)this.UnderlyingParameter), tattributeData, AttributeDescription.DecimalConstantAttribute);
						if (targetAttributeSignatureIndex != -1)
						{
							if (tattributeData.CommonConstructorArguments.Length == 5)
							{
								instance.AddOptional(this.TypeManager.CreateSynthesizedAttribute((targetAttributeSignatureIndex == 0) ? WellKnownMember.System_Runtime_CompilerServices_DecimalConstantAttribute__ctor : WellKnownMember.System_Runtime_CompilerServices_DecimalConstantAttribute__ctorByteByteInt32Int32Int32, tattributeData, syntaxNodeOpt, diagnostics));
							}
						}
						else if (this.IsTargetAttribute(tattributeData, AttributeDescription.DefaultParameterValueAttribute) && tattributeData.CommonConstructorArguments.Length == 1)
						{
							instance.AddOptional(this.TypeManager.CreateSynthesizedAttribute(WellKnownMember.System_Runtime_InteropServices_DefaultParameterValueAttribute__ctor, tattributeData, syntaxNodeOpt, diagnostics));
						}
					}
				}
				return instance.ToImmutableAndFree();
			}

			// Token: 0x1700108C RID: 4236
			// (get) Token: 0x06003CBC RID: 15548 RVA: 0x000ACFFC File Offset: 0x000AB1FC
			bool IParameterDefinition.HasDefaultValue
			{
				get
				{
					return this.HasDefaultValue;
				}
			}

			// Token: 0x06003CBD RID: 15549 RVA: 0x000AD004 File Offset: 0x000AB204
			MetadataConstant IParameterDefinition.GetDefaultValue(EmitContext context)
			{
				return this.GetDefaultValue(context);
			}

			// Token: 0x1700108D RID: 4237
			// (get) Token: 0x06003CBE RID: 15550 RVA: 0x000AD00D File Offset: 0x000AB20D
			bool IParameterDefinition.IsIn
			{
				get
				{
					return this.IsIn;
				}
			}

			// Token: 0x1700108E RID: 4238
			// (get) Token: 0x06003CBF RID: 15551 RVA: 0x000AD015 File Offset: 0x000AB215
			bool IParameterDefinition.IsOut
			{
				get
				{
					return this.IsOut;
				}
			}

			// Token: 0x1700108F RID: 4239
			// (get) Token: 0x06003CC0 RID: 15552 RVA: 0x000AD01D File Offset: 0x000AB21D
			bool IParameterDefinition.IsOptional
			{
				get
				{
					return this.IsOptional;
				}
			}

			// Token: 0x17001090 RID: 4240
			// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x000AD025 File Offset: 0x000AB225
			bool IParameterDefinition.IsMarshalledExplicitly
			{
				get
				{
					return this.IsMarshalledExplicitly;
				}
			}

			// Token: 0x17001091 RID: 4241
			// (get) Token: 0x06003CC2 RID: 15554 RVA: 0x000AD02D File Offset: 0x000AB22D
			IMarshallingInformation IParameterDefinition.MarshallingInformation
			{
				get
				{
					return this.MarshallingInformation;
				}
			}

			// Token: 0x17001092 RID: 4242
			// (get) Token: 0x06003CC3 RID: 15555 RVA: 0x000AD035 File Offset: 0x000AB235
			ImmutableArray<byte> IParameterDefinition.MarshallingDescriptor
			{
				get
				{
					return this.MarshallingDescriptor;
				}
			}

			// Token: 0x06003CC4 RID: 15556 RVA: 0x000AD040 File Offset: 0x000AB240
			IEnumerable<ICustomAttribute> IReference.GetAttributes(EmitContext context)
			{
				if (this._lazyAttributes.IsDefault)
				{
					DiagnosticBag instance = DiagnosticBag.GetInstance();
					ImmutableArray<TAttributeData> attributes = this.GetAttributes((TPEModuleBuilder)((object)context.Module), (TSyntaxNode)((object)context.SyntaxNode), instance);
					if (ImmutableInterlocked.InterlockedInitialize<TAttributeData>(ref this._lazyAttributes, attributes))
					{
						context.Diagnostics.AddRange(instance);
					}
					instance.Free();
				}
				return this._lazyAttributes;
			}

			// Token: 0x06003CC5 RID: 15557 RVA: 0x000AD0A9 File Offset: 0x000AB2A9
			void IReference.Dispatch(MetadataVisitor visitor)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06003CC6 RID: 15558 RVA: 0x000AD0B0 File Offset: 0x000AB2B0
			IDefinition IReference.AsDefinition(EmitContext context)
			{
				return this;
			}

			// Token: 0x06003CC7 RID: 15559 RVA: 0x000AD0B3 File Offset: 0x000AB2B3
			ISymbolInternal IReference.GetInternalSymbol()
			{
				return null;
			}

			// Token: 0x17001093 RID: 4243
			// (get) Token: 0x06003CC8 RID: 15560 RVA: 0x000AD0B6 File Offset: 0x000AB2B6
			string INamedEntity.Name
			{
				get
				{
					return this.Name;
				}
			}

			// Token: 0x17001094 RID: 4244
			// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x000AD0BE File Offset: 0x000AB2BE
			ImmutableArray<ICustomModifier> IParameterTypeInformation.CustomModifiers
			{
				get
				{
					return this.UnderlyingParameterTypeInformation.CustomModifiers;
				}
			}

			// Token: 0x17001095 RID: 4245
			// (get) Token: 0x06003CCA RID: 15562 RVA: 0x000AD0CB File Offset: 0x000AB2CB
			bool IParameterTypeInformation.IsByReference
			{
				get
				{
					return this.UnderlyingParameterTypeInformation.IsByReference;
				}
			}

			// Token: 0x17001096 RID: 4246
			// (get) Token: 0x06003CCB RID: 15563 RVA: 0x000AD0D8 File Offset: 0x000AB2D8
			ImmutableArray<ICustomModifier> IParameterTypeInformation.RefCustomModifiers
			{
				get
				{
					return this.UnderlyingParameterTypeInformation.RefCustomModifiers;
				}
			}

			// Token: 0x06003CCC RID: 15564 RVA: 0x000AD0E5 File Offset: 0x000AB2E5
			ITypeReference IParameterTypeInformation.GetType(EmitContext context)
			{
				return this.UnderlyingParameterTypeInformation.GetType(context);
			}

			// Token: 0x17001097 RID: 4247
			// (get) Token: 0x06003CCD RID: 15565 RVA: 0x000AD0F3 File Offset: 0x000AB2F3
			ushort IParameterListEntry.Index
			{
				get
				{
					return this.Index;
				}
			}

			// Token: 0x06003CCE RID: 15566 RVA: 0x000AD0FB File Offset: 0x000AB2FB
			public override string ToString()
			{
				return ((ISymbol)((object)this.UnderlyingParameter)).ToDisplayString(SymbolDisplayFormat.ILVisualizationFormat);
			}

			// Token: 0x06003CCF RID: 15567 RVA: 0x000AD117 File Offset: 0x000AB317
			public sealed override bool Equals(object obj)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06003CD0 RID: 15568 RVA: 0x000AD11E File Offset: 0x000AB31E
			public sealed override int GetHashCode()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x04001CD5 RID: 7381
			public readonly EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.CommonEmbeddedMember ContainingPropertyOrMethod;

			// Token: 0x04001CD6 RID: 7382
			public readonly TParameterSymbol UnderlyingParameter;

			// Token: 0x04001CD7 RID: 7383
			private ImmutableArray<TAttributeData> _lazyAttributes;
		}

		// Token: 0x02000745 RID: 1861
		internal abstract class CommonEmbeddedProperty : EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.CommonEmbeddedMember<TPropertySymbol>, IPropertyDefinition, ISignature, ITypeDefinitionMember, ITypeMemberReference, IReference, INamedEntity, IDefinition
		{
			// Token: 0x06003CD1 RID: 15569 RVA: 0x000AD125 File Offset: 0x000AB325
			protected CommonEmbeddedProperty(TPropertySymbol underlyingProperty, TEmbeddedMethod getter, TEmbeddedMethod setter)
				: base(underlyingProperty)
			{
				this._getter = getter;
				this._setter = setter;
				this._parameters = this.GetParameters();
			}

			// Token: 0x17001098 RID: 4248
			// (get) Token: 0x06003CD2 RID: 15570 RVA: 0x000AD148 File Offset: 0x000AB348
			internal override TEmbeddedTypesManager TypeManager
			{
				get
				{
					return this.AnAccessor.TypeManager;
				}
			}

			// Token: 0x06003CD3 RID: 15571
			protected abstract ImmutableArray<TEmbeddedParameter> GetParameters();

			// Token: 0x17001099 RID: 4249
			// (get) Token: 0x06003CD4 RID: 15572
			protected abstract bool IsRuntimeSpecial { get; }

			// Token: 0x1700109A RID: 4250
			// (get) Token: 0x06003CD5 RID: 15573
			protected abstract bool IsSpecialName { get; }

			// Token: 0x1700109B RID: 4251
			// (get) Token: 0x06003CD6 RID: 15574
			protected abstract ISignature UnderlyingPropertySignature { get; }

			// Token: 0x1700109C RID: 4252
			// (get) Token: 0x06003CD7 RID: 15575
			protected abstract TEmbeddedType ContainingType { get; }

			// Token: 0x1700109D RID: 4253
			// (get) Token: 0x06003CD8 RID: 15576
			protected abstract TypeMemberVisibility Visibility { get; }

			// Token: 0x1700109E RID: 4254
			// (get) Token: 0x06003CD9 RID: 15577
			protected abstract string Name { get; }

			// Token: 0x1700109F RID: 4255
			// (get) Token: 0x06003CDA RID: 15578 RVA: 0x000AD15A File Offset: 0x000AB35A
			public TPropertySymbol UnderlyingProperty
			{
				get
				{
					return this.UnderlyingSymbol;
				}
			}

			// Token: 0x170010A0 RID: 4256
			// (get) Token: 0x06003CDB RID: 15579 RVA: 0x000AD162 File Offset: 0x000AB362
			IMethodReference IPropertyDefinition.Getter
			{
				get
				{
					return this._getter;
				}
			}

			// Token: 0x170010A1 RID: 4257
			// (get) Token: 0x06003CDC RID: 15580 RVA: 0x000AD16F File Offset: 0x000AB36F
			IMethodReference IPropertyDefinition.Setter
			{
				get
				{
					return this._setter;
				}
			}

			// Token: 0x06003CDD RID: 15581 RVA: 0x000AD17C File Offset: 0x000AB37C
			IEnumerable<IMethodReference> IPropertyDefinition.GetAccessors(EmitContext context)
			{
				if (this._getter != null)
				{
					yield return this._getter;
				}
				if (this._setter != null)
				{
					yield return this._setter;
				}
				yield break;
			}

			// Token: 0x170010A2 RID: 4258
			// (get) Token: 0x06003CDE RID: 15582 RVA: 0x000AD18C File Offset: 0x000AB38C
			bool IPropertyDefinition.HasDefaultValue
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170010A3 RID: 4259
			// (get) Token: 0x06003CDF RID: 15583 RVA: 0x000AD18F File Offset: 0x000AB38F
			MetadataConstant IPropertyDefinition.DefaultValue
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170010A4 RID: 4260
			// (get) Token: 0x06003CE0 RID: 15584 RVA: 0x000AD192 File Offset: 0x000AB392
			bool IPropertyDefinition.IsRuntimeSpecial
			{
				get
				{
					return this.IsRuntimeSpecial;
				}
			}

			// Token: 0x170010A5 RID: 4261
			// (get) Token: 0x06003CE1 RID: 15585 RVA: 0x000AD19A File Offset: 0x000AB39A
			bool IPropertyDefinition.IsSpecialName
			{
				get
				{
					return this.IsSpecialName;
				}
			}

			// Token: 0x170010A6 RID: 4262
			// (get) Token: 0x06003CE2 RID: 15586 RVA: 0x000AD1A2 File Offset: 0x000AB3A2
			ImmutableArray<IParameterDefinition> IPropertyDefinition.Parameters
			{
				get
				{
					return StaticCast<IParameterDefinition>.From<TEmbeddedParameter>(this._parameters);
				}
			}

			// Token: 0x170010A7 RID: 4263
			// (get) Token: 0x06003CE3 RID: 15587 RVA: 0x000AD1AF File Offset: 0x000AB3AF
			CallingConvention ISignature.CallingConvention
			{
				get
				{
					return this.UnderlyingPropertySignature.CallingConvention;
				}
			}

			// Token: 0x170010A8 RID: 4264
			// (get) Token: 0x06003CE4 RID: 15588 RVA: 0x000AD1BC File Offset: 0x000AB3BC
			ushort ISignature.ParameterCount
			{
				get
				{
					return (ushort)this._parameters.Length;
				}
			}

			// Token: 0x06003CE5 RID: 15589 RVA: 0x000AD1D8 File Offset: 0x000AB3D8
			ImmutableArray<IParameterTypeInformation> ISignature.GetParameters(EmitContext context)
			{
				return StaticCast<IParameterTypeInformation>.From<TEmbeddedParameter>(this._parameters);
			}

			// Token: 0x170010A9 RID: 4265
			// (get) Token: 0x06003CE6 RID: 15590 RVA: 0x000AD1E5 File Offset: 0x000AB3E5
			ImmutableArray<ICustomModifier> ISignature.ReturnValueCustomModifiers
			{
				get
				{
					return this.UnderlyingPropertySignature.ReturnValueCustomModifiers;
				}
			}

			// Token: 0x170010AA RID: 4266
			// (get) Token: 0x06003CE7 RID: 15591 RVA: 0x000AD1F2 File Offset: 0x000AB3F2
			ImmutableArray<ICustomModifier> ISignature.RefCustomModifiers
			{
				get
				{
					return this.UnderlyingPropertySignature.RefCustomModifiers;
				}
			}

			// Token: 0x170010AB RID: 4267
			// (get) Token: 0x06003CE8 RID: 15592 RVA: 0x000AD1FF File Offset: 0x000AB3FF
			bool ISignature.ReturnValueIsByRef
			{
				get
				{
					return this.UnderlyingPropertySignature.ReturnValueIsByRef;
				}
			}

			// Token: 0x06003CE9 RID: 15593 RVA: 0x000AD20C File Offset: 0x000AB40C
			ITypeReference ISignature.GetType(EmitContext context)
			{
				return this.UnderlyingPropertySignature.GetType(context);
			}

			// Token: 0x170010AC RID: 4268
			// (get) Token: 0x06003CEA RID: 15594 RVA: 0x000AD21A File Offset: 0x000AB41A
			protected TEmbeddedMethod AnAccessor
			{
				get
				{
					TEmbeddedMethod tembeddedMethod;
					if ((tembeddedMethod = this._getter) == null)
					{
						tembeddedMethod = this._setter;
					}
					return tembeddedMethod;
				}
			}

			// Token: 0x170010AD RID: 4269
			// (get) Token: 0x06003CEB RID: 15595 RVA: 0x000AD231 File Offset: 0x000AB431
			ITypeDefinition ITypeDefinitionMember.ContainingTypeDefinition
			{
				get
				{
					return this.ContainingType;
				}
			}

			// Token: 0x170010AE RID: 4270
			// (get) Token: 0x06003CEC RID: 15596 RVA: 0x000AD23E File Offset: 0x000AB43E
			TypeMemberVisibility ITypeDefinitionMember.Visibility
			{
				get
				{
					return this.Visibility;
				}
			}

			// Token: 0x06003CED RID: 15597 RVA: 0x000AD246 File Offset: 0x000AB446
			ITypeReference ITypeMemberReference.GetContainingType(EmitContext context)
			{
				return this.ContainingType;
			}

			// Token: 0x06003CEE RID: 15598 RVA: 0x000AD253 File Offset: 0x000AB453
			void IReference.Dispatch(MetadataVisitor visitor)
			{
				visitor.Visit(this);
			}

			// Token: 0x06003CEF RID: 15599 RVA: 0x000AD25C File Offset: 0x000AB45C
			IDefinition IReference.AsDefinition(EmitContext context)
			{
				return this;
			}

			// Token: 0x170010AF RID: 4271
			// (get) Token: 0x06003CF0 RID: 15600 RVA: 0x000AD25F File Offset: 0x000AB45F
			string INamedEntity.Name
			{
				get
				{
					return this.Name;
				}
			}

			// Token: 0x04001CD8 RID: 7384
			private readonly ImmutableArray<TEmbeddedParameter> _parameters;

			// Token: 0x04001CD9 RID: 7385
			private readonly TEmbeddedMethod _getter;

			// Token: 0x04001CDA RID: 7386
			private readonly TEmbeddedMethod _setter;
		}

		// Token: 0x02000746 RID: 1862
		internal abstract class CommonEmbeddedType : INamespaceTypeDefinition, INamedTypeDefinition, ITypeDefinition, IDefinition, IReference, ITypeReference, INamedTypeReference, INamedEntity, INamespaceTypeReference
		{
			// Token: 0x06003CF1 RID: 15601 RVA: 0x000AD267 File Offset: 0x000AB467
			protected CommonEmbeddedType(TEmbeddedTypesManager typeManager, TNamedTypeSymbol underlyingNamedType)
			{
				this.TypeManager = typeManager;
				this.UnderlyingNamedType = underlyingNamedType;
			}

			// Token: 0x06003CF2 RID: 15602
			protected abstract int GetAssemblyRefIndex();

			// Token: 0x06003CF3 RID: 15603
			protected abstract IEnumerable<TFieldSymbol> GetFieldsToEmit();

			// Token: 0x06003CF4 RID: 15604
			protected abstract IEnumerable<TMethodSymbol> GetMethodsToEmit();

			// Token: 0x06003CF5 RID: 15605
			protected abstract IEnumerable<TEventSymbol> GetEventsToEmit();

			// Token: 0x06003CF6 RID: 15606
			protected abstract IEnumerable<TPropertySymbol> GetPropertiesToEmit();

			// Token: 0x170010B0 RID: 4272
			// (get) Token: 0x06003CF7 RID: 15607
			protected abstract bool IsPublic { get; }

			// Token: 0x170010B1 RID: 4273
			// (get) Token: 0x06003CF8 RID: 15608
			protected abstract bool IsAbstract { get; }

			// Token: 0x06003CF9 RID: 15609
			protected abstract ITypeReference GetBaseClass(TPEModuleBuilder moduleBuilder, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics);

			// Token: 0x06003CFA RID: 15610
			protected abstract IEnumerable<TypeReferenceWithAttributes> GetInterfaces(EmitContext context);

			// Token: 0x170010B2 RID: 4274
			// (get) Token: 0x06003CFB RID: 15611
			protected abstract bool IsBeforeFieldInit { get; }

			// Token: 0x170010B3 RID: 4275
			// (get) Token: 0x06003CFC RID: 15612
			protected abstract bool IsComImport { get; }

			// Token: 0x170010B4 RID: 4276
			// (get) Token: 0x06003CFD RID: 15613
			protected abstract bool IsInterface { get; }

			// Token: 0x170010B5 RID: 4277
			// (get) Token: 0x06003CFE RID: 15614
			protected abstract bool IsDelegate { get; }

			// Token: 0x170010B6 RID: 4278
			// (get) Token: 0x06003CFF RID: 15615
			protected abstract bool IsSerializable { get; }

			// Token: 0x170010B7 RID: 4279
			// (get) Token: 0x06003D00 RID: 15616
			protected abstract bool IsSpecialName { get; }

			// Token: 0x170010B8 RID: 4280
			// (get) Token: 0x06003D01 RID: 15617
			protected abstract bool IsWindowsRuntimeImport { get; }

			// Token: 0x170010B9 RID: 4281
			// (get) Token: 0x06003D02 RID: 15618
			protected abstract bool IsSealed { get; }

			// Token: 0x06003D03 RID: 15619
			protected abstract TypeLayout? GetTypeLayoutIfStruct();

			// Token: 0x170010BA RID: 4282
			// (get) Token: 0x06003D04 RID: 15620
			protected abstract CharSet StringFormat { get; }

			// Token: 0x06003D05 RID: 15621
			protected abstract TAttributeData CreateTypeIdentifierAttribute(bool hasGuid, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics);

			// Token: 0x06003D06 RID: 15622
			protected abstract void EmbedDefaultMembers(string defaultMember, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics);

			// Token: 0x06003D07 RID: 15623
			protected abstract IEnumerable<TAttributeData> GetCustomAttributesToEmit(TPEModuleBuilder moduleBuilder);

			// Token: 0x06003D08 RID: 15624
			protected abstract void ReportMissingAttribute(AttributeDescription description, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics);

			// Token: 0x06003D09 RID: 15625 RVA: 0x000AD284 File Offset: 0x000AB484
			private bool IsTargetAttribute(TAttributeData attrData, AttributeDescription description)
			{
				return this.TypeManager.IsTargetAttribute((TSymbol)((object)this.UnderlyingNamedType), attrData, description);
			}

			// Token: 0x06003D0A RID: 15626 RVA: 0x000AD2A8 File Offset: 0x000AB4A8
			private ImmutableArray<TAttributeData> GetAttributes(TPEModuleBuilder moduleBuilder, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
			{
				ArrayBuilder<TAttributeData> instance = ArrayBuilder<TAttributeData>.GetInstance();
				instance.AddOptional(this.TypeManager.CreateCompilerGeneratedAttribute());
				bool flag = false;
				bool flag2 = false;
				foreach (TAttributeData tattributeData in this.GetCustomAttributesToEmit(moduleBuilder))
				{
					if (this.IsTargetAttribute(tattributeData, AttributeDescription.GuidAttribute))
					{
						string text;
						if (tattributeData.TryGetGuidAttributeValue(out text))
						{
							flag = true;
							instance.AddOptional(this.TypeManager.CreateSynthesizedAttribute(WellKnownMember.System_Runtime_InteropServices_GuidAttribute__ctor, tattributeData, syntaxNodeOpt, diagnostics));
						}
					}
					else if (this.IsTargetAttribute(tattributeData, AttributeDescription.ComEventInterfaceAttribute))
					{
						if (tattributeData.CommonConstructorArguments.Length == 2)
						{
							flag2 = true;
							instance.AddOptional(this.TypeManager.CreateSynthesizedAttribute(WellKnownMember.System_Runtime_InteropServices_ComEventInterfaceAttribute__ctor, tattributeData, syntaxNodeOpt, diagnostics));
						}
					}
					else
					{
						int targetAttributeSignatureIndex = this.TypeManager.GetTargetAttributeSignatureIndex((TSymbol)((object)this.UnderlyingNamedType), tattributeData, AttributeDescription.InterfaceTypeAttribute);
						if (targetAttributeSignatureIndex != -1)
						{
							if (tattributeData.CommonConstructorArguments.Length == 1)
							{
								instance.AddOptional(this.TypeManager.CreateSynthesizedAttribute((targetAttributeSignatureIndex == 0) ? WellKnownMember.System_Runtime_InteropServices_InterfaceTypeAttribute__ctorInt16 : WellKnownMember.System_Runtime_InteropServices_InterfaceTypeAttribute__ctorComInterfaceType, tattributeData, syntaxNodeOpt, diagnostics));
							}
						}
						else if (this.IsTargetAttribute(tattributeData, AttributeDescription.BestFitMappingAttribute))
						{
							if (tattributeData.CommonConstructorArguments.Length == 1)
							{
								instance.AddOptional(this.TypeManager.CreateSynthesizedAttribute(WellKnownMember.System_Runtime_InteropServices_BestFitMappingAttribute__ctor, tattributeData, syntaxNodeOpt, diagnostics));
							}
						}
						else if (this.IsTargetAttribute(tattributeData, AttributeDescription.CoClassAttribute))
						{
							if (tattributeData.CommonConstructorArguments.Length == 1)
							{
								instance.AddOptional(this.TypeManager.CreateSynthesizedAttribute(WellKnownMember.System_Runtime_InteropServices_CoClassAttribute__ctor, tattributeData, syntaxNodeOpt, diagnostics));
							}
						}
						else if (this.IsTargetAttribute(tattributeData, AttributeDescription.FlagsAttribute))
						{
							if (tattributeData.CommonConstructorArguments.Length == 0 && this.UnderlyingNamedType.IsEnum)
							{
								instance.AddOptional(this.TypeManager.CreateSynthesizedAttribute(WellKnownMember.System_FlagsAttribute__ctor, tattributeData, syntaxNodeOpt, diagnostics));
							}
						}
						else if (this.IsTargetAttribute(tattributeData, AttributeDescription.DefaultMemberAttribute))
						{
							if (tattributeData.CommonConstructorArguments.Length == 1)
							{
								instance.AddOptional(this.TypeManager.CreateSynthesizedAttribute(WellKnownMember.System_Reflection_DefaultMemberAttribute__ctor, tattributeData, syntaxNodeOpt, diagnostics));
								string text2 = tattributeData.CommonConstructorArguments[0].ValueInternal as string;
								if (text2 != null)
								{
									this.EmbedDefaultMembers(text2, syntaxNodeOpt, diagnostics);
								}
							}
						}
						else if (this.IsTargetAttribute(tattributeData, AttributeDescription.UnmanagedFunctionPointerAttribute) && tattributeData.CommonConstructorArguments.Length == 1)
						{
							instance.AddOptional(this.TypeManager.CreateSynthesizedAttribute(WellKnownMember.System_Runtime_InteropServices_UnmanagedFunctionPointerAttribute__ctor, tattributeData, syntaxNodeOpt, diagnostics));
						}
					}
				}
				if (this.IsInterface && !flag2)
				{
					if (!this.IsComImport)
					{
						this.ReportMissingAttribute(AttributeDescription.ComImportAttribute, syntaxNodeOpt, diagnostics);
					}
					else if (!flag)
					{
						this.ReportMissingAttribute(AttributeDescription.GuidAttribute, syntaxNodeOpt, diagnostics);
					}
				}
				instance.AddOptional(this.CreateTypeIdentifierAttribute(flag && this.IsInterface, syntaxNodeOpt, diagnostics));
				return instance.ToImmutableAndFree();
			}

			// Token: 0x170010BB RID: 4283
			// (get) Token: 0x06003D0B RID: 15627 RVA: 0x000AD62C File Offset: 0x000AB82C
			public int AssemblyRefIndex
			{
				get
				{
					if (this._lazyAssemblyRefIndex == -1)
					{
						this._lazyAssemblyRefIndex = this.GetAssemblyRefIndex();
					}
					return this._lazyAssemblyRefIndex;
				}
			}

			// Token: 0x170010BC RID: 4284
			// (get) Token: 0x06003D0C RID: 15628 RVA: 0x000AD649 File Offset: 0x000AB849
			bool INamespaceTypeDefinition.IsPublic
			{
				get
				{
					return this.IsPublic;
				}
			}

			// Token: 0x06003D0D RID: 15629 RVA: 0x000AD651 File Offset: 0x000AB851
			ITypeReference ITypeDefinition.GetBaseClass(EmitContext context)
			{
				return this.GetBaseClass((TPEModuleBuilder)((object)context.Module), (TSyntaxNode)((object)context.SyntaxNode), context.Diagnostics);
			}

			// Token: 0x06003D0E RID: 15630 RVA: 0x000AD678 File Offset: 0x000AB878
			IEnumerable<IEventDefinition> ITypeDefinition.GetEvents(EmitContext context)
			{
				if (this._lazyEvents.IsDefault)
				{
					ArrayBuilder<IEventDefinition> instance = ArrayBuilder<IEventDefinition>.GetInstance();
					foreach (TEventSymbol teventSymbol in this.GetEventsToEmit())
					{
						TEmbeddedEvent tembeddedEvent;
						if (this.TypeManager.EmbeddedEventsMap.TryGetValue(teventSymbol, ref tembeddedEvent))
						{
							instance.Add(tembeddedEvent);
						}
					}
					ImmutableInterlocked.InterlockedInitialize<IEventDefinition>(ref this._lazyEvents, instance.ToImmutableAndFree());
				}
				return this._lazyEvents;
			}

			// Token: 0x06003D0F RID: 15631 RVA: 0x000AD714 File Offset: 0x000AB914
			IEnumerable<Microsoft.Cci.MethodImplementation> ITypeDefinition.GetExplicitImplementationOverrides(EmitContext context)
			{
				return SpecializedCollections.EmptyEnumerable<Microsoft.Cci.MethodImplementation>();
			}

			// Token: 0x06003D10 RID: 15632 RVA: 0x000AD71C File Offset: 0x000AB91C
			IEnumerable<IFieldDefinition> ITypeDefinition.GetFields(EmitContext context)
			{
				if (this._lazyFields.IsDefault)
				{
					ArrayBuilder<IFieldDefinition> instance = ArrayBuilder<IFieldDefinition>.GetInstance();
					foreach (TFieldSymbol tfieldSymbol in this.GetFieldsToEmit())
					{
						TEmbeddedField tembeddedField;
						if (this.TypeManager.EmbeddedFieldsMap.TryGetValue(tfieldSymbol, ref tembeddedField))
						{
							instance.Add(tembeddedField);
						}
					}
					ImmutableInterlocked.InterlockedInitialize<IFieldDefinition>(ref this._lazyFields, instance.ToImmutableAndFree());
				}
				return this._lazyFields;
			}

			// Token: 0x170010BD RID: 4285
			// (get) Token: 0x06003D11 RID: 15633 RVA: 0x000AD7B8 File Offset: 0x000AB9B8
			IEnumerable<IGenericTypeParameter> ITypeDefinition.GenericParameters
			{
				get
				{
					return SpecializedCollections.EmptyEnumerable<IGenericTypeParameter>();
				}
			}

			// Token: 0x170010BE RID: 4286
			// (get) Token: 0x06003D12 RID: 15634 RVA: 0x000AD7BF File Offset: 0x000AB9BF
			ushort ITypeDefinition.GenericParameterCount
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170010BF RID: 4287
			// (get) Token: 0x06003D13 RID: 15635 RVA: 0x000AD7C2 File Offset: 0x000AB9C2
			bool ITypeDefinition.HasDeclarativeSecurity
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06003D14 RID: 15636 RVA: 0x000AD7C5 File Offset: 0x000AB9C5
			IEnumerable<TypeReferenceWithAttributes> ITypeDefinition.Interfaces(EmitContext context)
			{
				return this.GetInterfaces(context);
			}

			// Token: 0x170010C0 RID: 4288
			// (get) Token: 0x06003D15 RID: 15637 RVA: 0x000AD7CE File Offset: 0x000AB9CE
			bool ITypeDefinition.IsAbstract
			{
				get
				{
					return this.IsAbstract;
				}
			}

			// Token: 0x170010C1 RID: 4289
			// (get) Token: 0x06003D16 RID: 15638 RVA: 0x000AD7D6 File Offset: 0x000AB9D6
			bool ITypeDefinition.IsBeforeFieldInit
			{
				get
				{
					return this.IsBeforeFieldInit;
				}
			}

			// Token: 0x170010C2 RID: 4290
			// (get) Token: 0x06003D17 RID: 15639 RVA: 0x000AD7DE File Offset: 0x000AB9DE
			bool ITypeDefinition.IsComObject
			{
				get
				{
					return this.IsInterface || this.IsComImport;
				}
			}

			// Token: 0x170010C3 RID: 4291
			// (get) Token: 0x06003D18 RID: 15640 RVA: 0x000AD7F0 File Offset: 0x000AB9F0
			bool ITypeDefinition.IsGeneric
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170010C4 RID: 4292
			// (get) Token: 0x06003D19 RID: 15641 RVA: 0x000AD7F3 File Offset: 0x000AB9F3
			bool ITypeDefinition.IsInterface
			{
				get
				{
					return this.IsInterface;
				}
			}

			// Token: 0x170010C5 RID: 4293
			// (get) Token: 0x06003D1A RID: 15642 RVA: 0x000AD7FB File Offset: 0x000AB9FB
			bool ITypeDefinition.IsDelegate
			{
				get
				{
					return this.IsDelegate;
				}
			}

			// Token: 0x170010C6 RID: 4294
			// (get) Token: 0x06003D1B RID: 15643 RVA: 0x000AD803 File Offset: 0x000ABA03
			bool ITypeDefinition.IsRuntimeSpecial
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170010C7 RID: 4295
			// (get) Token: 0x06003D1C RID: 15644 RVA: 0x000AD806 File Offset: 0x000ABA06
			bool ITypeDefinition.IsSerializable
			{
				get
				{
					return this.IsSerializable;
				}
			}

			// Token: 0x170010C8 RID: 4296
			// (get) Token: 0x06003D1D RID: 15645 RVA: 0x000AD80E File Offset: 0x000ABA0E
			bool ITypeDefinition.IsSpecialName
			{
				get
				{
					return this.IsSpecialName;
				}
			}

			// Token: 0x170010C9 RID: 4297
			// (get) Token: 0x06003D1E RID: 15646 RVA: 0x000AD816 File Offset: 0x000ABA16
			bool ITypeDefinition.IsWindowsRuntimeImport
			{
				get
				{
					return this.IsWindowsRuntimeImport;
				}
			}

			// Token: 0x170010CA RID: 4298
			// (get) Token: 0x06003D1F RID: 15647 RVA: 0x000AD81E File Offset: 0x000ABA1E
			bool ITypeDefinition.IsSealed
			{
				get
				{
					return this.IsSealed;
				}
			}

			// Token: 0x170010CB RID: 4299
			// (get) Token: 0x06003D20 RID: 15648 RVA: 0x000AD828 File Offset: 0x000ABA28
			LayoutKind ITypeDefinition.Layout
			{
				get
				{
					TypeLayout? typeLayoutIfStruct = this.GetTypeLayoutIfStruct();
					if (typeLayoutIfStruct == null)
					{
						return 3;
					}
					return typeLayoutIfStruct.GetValueOrDefault().Kind;
				}
			}

			// Token: 0x170010CC RID: 4300
			// (get) Token: 0x06003D21 RID: 15649 RVA: 0x000AD858 File Offset: 0x000ABA58
			ushort ITypeDefinition.Alignment
			{
				get
				{
					TypeLayout? typeLayoutIfStruct = this.GetTypeLayoutIfStruct();
					return (ushort)((typeLayoutIfStruct != null) ? typeLayoutIfStruct.GetValueOrDefault().Alignment : 0);
				}
			}

			// Token: 0x170010CD RID: 4301
			// (get) Token: 0x06003D22 RID: 15650 RVA: 0x000AD888 File Offset: 0x000ABA88
			uint ITypeDefinition.SizeOf
			{
				get
				{
					TypeLayout? typeLayoutIfStruct = this.GetTypeLayoutIfStruct();
					if (typeLayoutIfStruct == null)
					{
						return 0U;
					}
					return (uint)typeLayoutIfStruct.GetValueOrDefault().Size;
				}
			}

			// Token: 0x06003D23 RID: 15651 RVA: 0x000AD8B8 File Offset: 0x000ABAB8
			IEnumerable<IMethodDefinition> ITypeDefinition.GetMethods(EmitContext context)
			{
				if (this._lazyMethods.IsDefault)
				{
					ArrayBuilder<IMethodDefinition> instance = ArrayBuilder<IMethodDefinition>.GetInstance();
					int num = 1;
					int num2 = 0;
					foreach (TMethodSymbol tmethodSymbol in this.GetMethodsToEmit())
					{
						if (tmethodSymbol != null)
						{
							TEmbeddedMethod tembeddedMethod;
							if (this.TypeManager.EmbeddedMethodsMap.TryGetValue(tmethodSymbol, ref tembeddedMethod))
							{
								if (num2 > 0)
								{
									instance.Add(new VtblGap(this, ModuleExtensions.GetVTableGapName(num, num2)));
									num++;
									num2 = 0;
								}
								instance.Add(tembeddedMethod);
							}
							else
							{
								num2++;
							}
						}
						else
						{
							num2++;
						}
					}
					ImmutableInterlocked.InterlockedInitialize<IMethodDefinition>(ref this._lazyMethods, instance.ToImmutableAndFree());
				}
				return this._lazyMethods;
			}

			// Token: 0x06003D24 RID: 15652 RVA: 0x000AD990 File Offset: 0x000ABB90
			IEnumerable<INestedTypeDefinition> ITypeDefinition.GetNestedTypes(EmitContext context)
			{
				return SpecializedCollections.EmptyEnumerable<INestedTypeDefinition>();
			}

			// Token: 0x06003D25 RID: 15653 RVA: 0x000AD998 File Offset: 0x000ABB98
			IEnumerable<IPropertyDefinition> ITypeDefinition.GetProperties(EmitContext context)
			{
				if (this._lazyProperties.IsDefault)
				{
					ArrayBuilder<IPropertyDefinition> instance = ArrayBuilder<IPropertyDefinition>.GetInstance();
					foreach (TPropertySymbol tpropertySymbol in this.GetPropertiesToEmit())
					{
						TEmbeddedProperty tembeddedProperty;
						if (this.TypeManager.EmbeddedPropertiesMap.TryGetValue(tpropertySymbol, ref tembeddedProperty))
						{
							instance.Add(tembeddedProperty);
						}
					}
					ImmutableInterlocked.InterlockedInitialize<IPropertyDefinition>(ref this._lazyProperties, instance.ToImmutableAndFree());
				}
				return this._lazyProperties;
			}

			// Token: 0x170010CE RID: 4302
			// (get) Token: 0x06003D26 RID: 15654 RVA: 0x000ADA34 File Offset: 0x000ABC34
			IEnumerable<SecurityAttribute> ITypeDefinition.SecurityAttributes
			{
				get
				{
					return SpecializedCollections.EmptyEnumerable<SecurityAttribute>();
				}
			}

			// Token: 0x170010CF RID: 4303
			// (get) Token: 0x06003D27 RID: 15655 RVA: 0x000ADA3B File Offset: 0x000ABC3B
			CharSet ITypeDefinition.StringFormat
			{
				get
				{
					return this.StringFormat;
				}
			}

			// Token: 0x06003D28 RID: 15656 RVA: 0x000ADA44 File Offset: 0x000ABC44
			IEnumerable<ICustomAttribute> IReference.GetAttributes(EmitContext context)
			{
				if (this._lazyAttributes.IsDefault)
				{
					DiagnosticBag instance = DiagnosticBag.GetInstance();
					ImmutableArray<TAttributeData> attributes = this.GetAttributes((TPEModuleBuilder)((object)context.Module), (TSyntaxNode)((object)context.SyntaxNode), instance);
					if (ImmutableInterlocked.InterlockedInitialize<TAttributeData>(ref this._lazyAttributes, attributes))
					{
						context.Diagnostics.AddRange(instance);
					}
					instance.Free();
				}
				return this._lazyAttributes;
			}

			// Token: 0x06003D29 RID: 15657 RVA: 0x000ADAAD File Offset: 0x000ABCAD
			void IReference.Dispatch(MetadataVisitor visitor)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06003D2A RID: 15658 RVA: 0x000ADAB4 File Offset: 0x000ABCB4
			IDefinition IReference.AsDefinition(EmitContext context)
			{
				return this;
			}

			// Token: 0x06003D2B RID: 15659 RVA: 0x000ADAB7 File Offset: 0x000ABCB7
			ISymbolInternal IReference.GetInternalSymbol()
			{
				return null;
			}

			// Token: 0x170010D0 RID: 4304
			// (get) Token: 0x06003D2C RID: 15660 RVA: 0x000ADABA File Offset: 0x000ABCBA
			bool ITypeReference.IsEnum
			{
				get
				{
					return this.UnderlyingNamedType.IsEnum;
				}
			}

			// Token: 0x170010D1 RID: 4305
			// (get) Token: 0x06003D2D RID: 15661 RVA: 0x000ADACC File Offset: 0x000ABCCC
			bool ITypeReference.IsValueType
			{
				get
				{
					return this.UnderlyingNamedType.IsValueType;
				}
			}

			// Token: 0x06003D2E RID: 15662 RVA: 0x000ADADE File Offset: 0x000ABCDE
			ITypeDefinition ITypeReference.GetResolvedType(EmitContext context)
			{
				return this;
			}

			// Token: 0x170010D2 RID: 4306
			// (get) Token: 0x06003D2F RID: 15663 RVA: 0x000ADAE1 File Offset: 0x000ABCE1
			Microsoft.Cci.PrimitiveTypeCode ITypeReference.TypeCode
			{
				get
				{
					return Microsoft.Cci.PrimitiveTypeCode.NotPrimitive;
				}
			}

			// Token: 0x170010D3 RID: 4307
			// (get) Token: 0x06003D30 RID: 15664 RVA: 0x000ADAE8 File Offset: 0x000ABCE8
			TypeDefinitionHandle ITypeReference.TypeDef
			{
				get
				{
					return default(TypeDefinitionHandle);
				}
			}

			// Token: 0x170010D4 RID: 4308
			// (get) Token: 0x06003D31 RID: 15665 RVA: 0x000ADAFE File Offset: 0x000ABCFE
			IGenericMethodParameterReference ITypeReference.AsGenericMethodParameterReference
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170010D5 RID: 4309
			// (get) Token: 0x06003D32 RID: 15666 RVA: 0x000ADB01 File Offset: 0x000ABD01
			IGenericTypeInstanceReference ITypeReference.AsGenericTypeInstanceReference
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170010D6 RID: 4310
			// (get) Token: 0x06003D33 RID: 15667 RVA: 0x000ADB04 File Offset: 0x000ABD04
			IGenericTypeParameterReference ITypeReference.AsGenericTypeParameterReference
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06003D34 RID: 15668 RVA: 0x000ADB07 File Offset: 0x000ABD07
			INamespaceTypeDefinition ITypeReference.AsNamespaceTypeDefinition(EmitContext context)
			{
				return this;
			}

			// Token: 0x170010D7 RID: 4311
			// (get) Token: 0x06003D35 RID: 15669 RVA: 0x000ADB0A File Offset: 0x000ABD0A
			INamespaceTypeReference ITypeReference.AsNamespaceTypeReference
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06003D36 RID: 15670 RVA: 0x000ADB0D File Offset: 0x000ABD0D
			INestedTypeDefinition ITypeReference.AsNestedTypeDefinition(EmitContext context)
			{
				return null;
			}

			// Token: 0x170010D8 RID: 4312
			// (get) Token: 0x06003D37 RID: 15671 RVA: 0x000ADB10 File Offset: 0x000ABD10
			INestedTypeReference ITypeReference.AsNestedTypeReference
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170010D9 RID: 4313
			// (get) Token: 0x06003D38 RID: 15672 RVA: 0x000ADB13 File Offset: 0x000ABD13
			ISpecializedNestedTypeReference ITypeReference.AsSpecializedNestedTypeReference
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06003D39 RID: 15673 RVA: 0x000ADB16 File Offset: 0x000ABD16
			ITypeDefinition ITypeReference.AsTypeDefinition(EmitContext context)
			{
				return this;
			}

			// Token: 0x170010DA RID: 4314
			// (get) Token: 0x06003D3A RID: 15674 RVA: 0x000ADB19 File Offset: 0x000ABD19
			ushort INamedTypeReference.GenericParameterCount
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170010DB RID: 4315
			// (get) Token: 0x06003D3B RID: 15675 RVA: 0x000ADB1C File Offset: 0x000ABD1C
			bool INamedTypeReference.MangleName
			{
				get
				{
					return this.UnderlyingNamedType.MangleName;
				}
			}

			// Token: 0x170010DC RID: 4316
			// (get) Token: 0x06003D3C RID: 15676 RVA: 0x000ADB2E File Offset: 0x000ABD2E
			string INamedEntity.Name
			{
				get
				{
					return this.UnderlyingNamedType.Name;
				}
			}

			// Token: 0x06003D3D RID: 15677 RVA: 0x000ADB40 File Offset: 0x000ABD40
			IUnitReference INamespaceTypeReference.GetUnit(EmitContext context)
			{
				return this.TypeManager.ModuleBeingBuilt;
			}

			// Token: 0x170010DD RID: 4317
			// (get) Token: 0x06003D3E RID: 15678 RVA: 0x000ADB57 File Offset: 0x000ABD57
			string INamespaceTypeReference.NamespaceName
			{
				get
				{
					return this.UnderlyingNamedType.NamespaceName;
				}
			}

			// Token: 0x06003D3F RID: 15679 RVA: 0x000ADB69 File Offset: 0x000ABD69
			public override string ToString()
			{
				return this.UnderlyingNamedType.GetInternalSymbol().GetISymbol().ToDisplayString(SymbolDisplayFormat.ILVisualizationFormat);
			}

			// Token: 0x06003D40 RID: 15680 RVA: 0x000ADB8A File Offset: 0x000ABD8A
			public sealed override bool Equals(object obj)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06003D41 RID: 15681 RVA: 0x000ADB91 File Offset: 0x000ABD91
			public sealed override int GetHashCode()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x04001CDB RID: 7387
			public readonly TEmbeddedTypesManager TypeManager;

			// Token: 0x04001CDC RID: 7388
			public readonly TNamedTypeSymbol UnderlyingNamedType;

			// Token: 0x04001CDD RID: 7389
			private ImmutableArray<IFieldDefinition> _lazyFields;

			// Token: 0x04001CDE RID: 7390
			private ImmutableArray<IMethodDefinition> _lazyMethods;

			// Token: 0x04001CDF RID: 7391
			private ImmutableArray<IPropertyDefinition> _lazyProperties;

			// Token: 0x04001CE0 RID: 7392
			private ImmutableArray<IEventDefinition> _lazyEvents;

			// Token: 0x04001CE1 RID: 7393
			private ImmutableArray<TAttributeData> _lazyAttributes;

			// Token: 0x04001CE2 RID: 7394
			private int _lazyAssemblyRefIndex = -1;
		}

		// Token: 0x02000747 RID: 1863
		internal abstract class CommonEmbeddedTypeParameter : IGenericMethodParameter, IGenericParameter, IGenericParameterReference, ITypeReference, IReference, INamedEntity, IParameterListEntry, IGenericMethodParameterReference
		{
			// Token: 0x06003D42 RID: 15682 RVA: 0x000ADB98 File Offset: 0x000ABD98
			protected CommonEmbeddedTypeParameter(TEmbeddedMethod containingMethod, TTypeParameterSymbol underlyingTypeParameter)
			{
				this.ContainingMethod = containingMethod;
				this.UnderlyingTypeParameter = underlyingTypeParameter;
			}

			// Token: 0x06003D43 RID: 15683
			protected abstract IEnumerable<TypeReferenceWithAttributes> GetConstraints(EmitContext context);

			// Token: 0x170010DE RID: 4318
			// (get) Token: 0x06003D44 RID: 15684
			protected abstract bool MustBeReferenceType { get; }

			// Token: 0x170010DF RID: 4319
			// (get) Token: 0x06003D45 RID: 15685
			protected abstract bool MustBeValueType { get; }

			// Token: 0x170010E0 RID: 4320
			// (get) Token: 0x06003D46 RID: 15686
			protected abstract bool MustHaveDefaultConstructor { get; }

			// Token: 0x170010E1 RID: 4321
			// (get) Token: 0x06003D47 RID: 15687
			protected abstract string Name { get; }

			// Token: 0x170010E2 RID: 4322
			// (get) Token: 0x06003D48 RID: 15688
			protected abstract ushort Index { get; }

			// Token: 0x170010E3 RID: 4323
			// (get) Token: 0x06003D49 RID: 15689 RVA: 0x000ADBAE File Offset: 0x000ABDAE
			IMethodDefinition IGenericMethodParameter.DefiningMethod
			{
				get
				{
					return this.ContainingMethod;
				}
			}

			// Token: 0x06003D4A RID: 15690 RVA: 0x000ADBBB File Offset: 0x000ABDBB
			IEnumerable<TypeReferenceWithAttributes> IGenericParameter.GetConstraints(EmitContext context)
			{
				return this.GetConstraints(context);
			}

			// Token: 0x170010E4 RID: 4324
			// (get) Token: 0x06003D4B RID: 15691 RVA: 0x000ADBC4 File Offset: 0x000ABDC4
			bool IGenericParameter.MustBeReferenceType
			{
				get
				{
					return this.MustBeReferenceType;
				}
			}

			// Token: 0x170010E5 RID: 4325
			// (get) Token: 0x06003D4C RID: 15692 RVA: 0x000ADBCC File Offset: 0x000ABDCC
			bool IGenericParameter.MustBeValueType
			{
				get
				{
					return this.MustBeValueType;
				}
			}

			// Token: 0x170010E6 RID: 4326
			// (get) Token: 0x06003D4D RID: 15693 RVA: 0x000ADBD4 File Offset: 0x000ABDD4
			bool IGenericParameter.MustHaveDefaultConstructor
			{
				get
				{
					return this.MustHaveDefaultConstructor;
				}
			}

			// Token: 0x170010E7 RID: 4327
			// (get) Token: 0x06003D4E RID: 15694 RVA: 0x000ADBDC File Offset: 0x000ABDDC
			TypeParameterVariance IGenericParameter.Variance
			{
				get
				{
					return TypeParameterVariance.NonVariant;
				}
			}

			// Token: 0x170010E8 RID: 4328
			// (get) Token: 0x06003D4F RID: 15695 RVA: 0x000ADBDF File Offset: 0x000ABDDF
			IGenericMethodParameter IGenericParameter.AsGenericMethodParameter
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170010E9 RID: 4329
			// (get) Token: 0x06003D50 RID: 15696 RVA: 0x000ADBE2 File Offset: 0x000ABDE2
			IGenericTypeParameter IGenericParameter.AsGenericTypeParameter
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170010EA RID: 4330
			// (get) Token: 0x06003D51 RID: 15697 RVA: 0x000ADBE5 File Offset: 0x000ABDE5
			bool ITypeReference.IsEnum
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170010EB RID: 4331
			// (get) Token: 0x06003D52 RID: 15698 RVA: 0x000ADBE8 File Offset: 0x000ABDE8
			bool ITypeReference.IsValueType
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06003D53 RID: 15699 RVA: 0x000ADBEB File Offset: 0x000ABDEB
			ITypeDefinition ITypeReference.GetResolvedType(EmitContext context)
			{
				return null;
			}

			// Token: 0x170010EC RID: 4332
			// (get) Token: 0x06003D54 RID: 15700 RVA: 0x000ADBEE File Offset: 0x000ABDEE
			Microsoft.Cci.PrimitiveTypeCode ITypeReference.TypeCode
			{
				get
				{
					return Microsoft.Cci.PrimitiveTypeCode.NotPrimitive;
				}
			}

			// Token: 0x170010ED RID: 4333
			// (get) Token: 0x06003D55 RID: 15701 RVA: 0x000ADBF4 File Offset: 0x000ABDF4
			TypeDefinitionHandle ITypeReference.TypeDef
			{
				get
				{
					return default(TypeDefinitionHandle);
				}
			}

			// Token: 0x170010EE RID: 4334
			// (get) Token: 0x06003D56 RID: 15702 RVA: 0x000ADC0A File Offset: 0x000ABE0A
			IGenericMethodParameterReference ITypeReference.AsGenericMethodParameterReference
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170010EF RID: 4335
			// (get) Token: 0x06003D57 RID: 15703 RVA: 0x000ADC0D File Offset: 0x000ABE0D
			IGenericTypeInstanceReference ITypeReference.AsGenericTypeInstanceReference
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170010F0 RID: 4336
			// (get) Token: 0x06003D58 RID: 15704 RVA: 0x000ADC10 File Offset: 0x000ABE10
			IGenericTypeParameterReference ITypeReference.AsGenericTypeParameterReference
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06003D59 RID: 15705 RVA: 0x000ADC13 File Offset: 0x000ABE13
			INamespaceTypeDefinition ITypeReference.AsNamespaceTypeDefinition(EmitContext context)
			{
				return null;
			}

			// Token: 0x170010F1 RID: 4337
			// (get) Token: 0x06003D5A RID: 15706 RVA: 0x000ADC16 File Offset: 0x000ABE16
			INamespaceTypeReference ITypeReference.AsNamespaceTypeReference
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06003D5B RID: 15707 RVA: 0x000ADC19 File Offset: 0x000ABE19
			INestedTypeDefinition ITypeReference.AsNestedTypeDefinition(EmitContext context)
			{
				return null;
			}

			// Token: 0x170010F2 RID: 4338
			// (get) Token: 0x06003D5C RID: 15708 RVA: 0x000ADC1C File Offset: 0x000ABE1C
			INestedTypeReference ITypeReference.AsNestedTypeReference
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170010F3 RID: 4339
			// (get) Token: 0x06003D5D RID: 15709 RVA: 0x000ADC1F File Offset: 0x000ABE1F
			ISpecializedNestedTypeReference ITypeReference.AsSpecializedNestedTypeReference
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06003D5E RID: 15710 RVA: 0x000ADC22 File Offset: 0x000ABE22
			ITypeDefinition ITypeReference.AsTypeDefinition(EmitContext context)
			{
				return null;
			}

			// Token: 0x06003D5F RID: 15711 RVA: 0x000ADC25 File Offset: 0x000ABE25
			IEnumerable<ICustomAttribute> IReference.GetAttributes(EmitContext context)
			{
				return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
			}

			// Token: 0x06003D60 RID: 15712 RVA: 0x000ADC2C File Offset: 0x000ABE2C
			void IReference.Dispatch(MetadataVisitor visitor)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06003D61 RID: 15713 RVA: 0x000ADC33 File Offset: 0x000ABE33
			IDefinition IReference.AsDefinition(EmitContext context)
			{
				return null;
			}

			// Token: 0x06003D62 RID: 15714 RVA: 0x000ADC36 File Offset: 0x000ABE36
			ISymbolInternal IReference.GetInternalSymbol()
			{
				return null;
			}

			// Token: 0x170010F4 RID: 4340
			// (get) Token: 0x06003D63 RID: 15715 RVA: 0x000ADC39 File Offset: 0x000ABE39
			string INamedEntity.Name
			{
				get
				{
					return this.Name;
				}
			}

			// Token: 0x170010F5 RID: 4341
			// (get) Token: 0x06003D64 RID: 15716 RVA: 0x000ADC41 File Offset: 0x000ABE41
			ushort IParameterListEntry.Index
			{
				get
				{
					return this.Index;
				}
			}

			// Token: 0x170010F6 RID: 4342
			// (get) Token: 0x06003D65 RID: 15717 RVA: 0x000ADC49 File Offset: 0x000ABE49
			IMethodReference IGenericMethodParameterReference.DefiningMethod
			{
				get
				{
					return this.ContainingMethod;
				}
			}

			// Token: 0x06003D66 RID: 15718 RVA: 0x000ADC56 File Offset: 0x000ABE56
			public sealed override bool Equals(object obj)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06003D67 RID: 15719 RVA: 0x000ADC5D File Offset: 0x000ABE5D
			public sealed override int GetHashCode()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x04001CE3 RID: 7395
			public readonly TEmbeddedMethod ContainingMethod;

			// Token: 0x04001CE4 RID: 7396
			public readonly TTypeParameterSymbol UnderlyingTypeParameter;
		}

		// Token: 0x02000748 RID: 1864
		private sealed class TypeComparer : IComparer<TEmbeddedType>
		{
			// Token: 0x06003D68 RID: 15720 RVA: 0x000ADC64 File Offset: 0x000ABE64
			private TypeComparer()
			{
			}

			// Token: 0x06003D69 RID: 15721 RVA: 0x000ADC6C File Offset: 0x000ABE6C
			public int Compare(TEmbeddedType x, TEmbeddedType y)
			{
				INamespaceTypeDefinition namespaceTypeDefinition = x;
				INamespaceTypeDefinition namespaceTypeDefinition2 = y;
				int num = string.Compare(namespaceTypeDefinition.NamespaceName, namespaceTypeDefinition2.NamespaceName, 4);
				if (num == 0)
				{
					num = string.Compare(namespaceTypeDefinition.Name, namespaceTypeDefinition2.Name, 4);
					if (num == 0)
					{
						num = x.AssemblyRefIndex - y.AssemblyRefIndex;
					}
				}
				return num;
			}

			// Token: 0x04001CE5 RID: 7397
			public static readonly EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.TypeComparer Instance = new EmbeddedTypesManager<TPEModuleBuilder, TModuleCompilationState, TEmbeddedTypesManager, TSyntaxNode, TAttributeData, TSymbol, TAssemblySymbol, TNamedTypeSymbol, TFieldSymbol, TMethodSymbol, TEventSymbol, TPropertySymbol, TParameterSymbol, TTypeParameterSymbol, TEmbeddedType, TEmbeddedField, TEmbeddedMethod, TEmbeddedEvent, TEmbeddedProperty, TEmbeddedParameter, TEmbeddedTypeParameter>.TypeComparer();
		}
	}
}
