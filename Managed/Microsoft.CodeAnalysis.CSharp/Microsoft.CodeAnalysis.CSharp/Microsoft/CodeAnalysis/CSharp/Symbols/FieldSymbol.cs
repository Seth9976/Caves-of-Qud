using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.InteropServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004AE RID: 1198
	internal abstract class FieldSymbol : Symbol, IFieldReference, ITypeMemberReference, IReference, INamedEntity, IFieldDefinition, ITypeDefinitionMember, IDefinition, ISpecializedFieldReference, IFieldSymbolInternal, ISymbolInternal
	{
		// Token: 0x06006384 RID: 25476 RVA: 0x0017D340 File Offset: 0x0017B540
		ITypeReference IFieldReference.GetType(EmitContext context)
		{
			PEModuleBuilder pemoduleBuilder = (PEModuleBuilder)context.Module;
			TypeWithAnnotations typeWithAnnotations = this.AdaptedFieldSymbol.TypeWithAnnotations;
			ImmutableArray<CustomModifier> customModifiers = typeWithAnnotations.CustomModifiers;
			bool isFixedSizeBuffer = this.AdaptedFieldSymbol.IsFixedSizeBuffer;
			TypeSymbol typeSymbol = (isFixedSizeBuffer ? this.AdaptedFieldSymbol.FixedImplementationType(pemoduleBuilder) : typeWithAnnotations.Type);
			ITypeReference typeReference = pemoduleBuilder.Translate(typeSymbol, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
			if (isFixedSizeBuffer || customModifiers.Length == 0)
			{
				return typeReference;
			}
			return new ModifiedTypeReference(typeReference, ImmutableArray<ICustomModifier>.CastUp<CustomModifier>(customModifiers));
		}

		// Token: 0x06006385 RID: 25477 RVA: 0x0017D3C6 File Offset: 0x0017B5C6
		IFieldDefinition IFieldReference.GetResolvedField(EmitContext context)
		{
			return this.ResolvedFieldImpl((PEModuleBuilder)context.Module);
		}

		// Token: 0x06006386 RID: 25478 RVA: 0x0017D3D9 File Offset: 0x0017B5D9
		private IFieldDefinition ResolvedFieldImpl(PEModuleBuilder moduleBeingBuilt)
		{
			if (this.AdaptedFieldSymbol.IsDefinition && this.AdaptedFieldSymbol.ContainingModule == moduleBeingBuilt.SourceModule)
			{
				return this;
			}
			return null;
		}

		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x06006387 RID: 25479 RVA: 0x0017D403 File Offset: 0x0017B603
		ISpecializedFieldReference IFieldReference.AsSpecializedFieldReference
		{
			get
			{
				if (!this.AdaptedFieldSymbol.IsDefinition)
				{
					return this;
				}
				return null;
			}
		}

		// Token: 0x06006388 RID: 25480 RVA: 0x0017D415 File Offset: 0x0017B615
		ITypeReference ITypeMemberReference.GetContainingType(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.AdaptedFieldSymbol.ContainingType, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, false, this.AdaptedFieldSymbol.IsDefinition);
		}

		// Token: 0x06006389 RID: 25481 RVA: 0x0017D450 File Offset: 0x0017B650
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			if (!this.AdaptedFieldSymbol.IsDefinition)
			{
				visitor.Visit(this);
				return;
			}
			if (this.AdaptedFieldSymbol.ContainingModule == ((PEModuleBuilder)visitor.Context.Module).SourceModule)
			{
				visitor.Visit(this);
				return;
			}
			visitor.Visit(this);
		}

		// Token: 0x0600638A RID: 25482 RVA: 0x0017D4A8 File Offset: 0x0017B6A8
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			PEModuleBuilder pemoduleBuilder = (PEModuleBuilder)context.Module;
			return this.ResolvedFieldImpl(pemoduleBuilder);
		}

		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x0600638B RID: 25483 RVA: 0x0017D4C8 File Offset: 0x0017B6C8
		string INamedEntity.Name
		{
			get
			{
				return this.AdaptedFieldSymbol.MetadataName;
			}
		}

		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x0600638C RID: 25484 RVA: 0x0017D4D5 File Offset: 0x0017B6D5
		bool IFieldReference.IsContextualNamedEntity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600638D RID: 25485 RVA: 0x0017D4D8 File Offset: 0x0017B6D8
		MetadataConstant IFieldDefinition.GetCompileTimeValue(EmitContext context)
		{
			return this.GetMetadataConstantValue(context);
		}

		// Token: 0x0600638E RID: 25486 RVA: 0x0017D4E4 File Offset: 0x0017B6E4
		internal MetadataConstant GetMetadataConstantValue(EmitContext context)
		{
			if (this.AdaptedFieldSymbol.IsMetadataConstant)
			{
				return ((PEModuleBuilder)context.Module).CreateConstant(this.AdaptedFieldSymbol.Type, this.AdaptedFieldSymbol.ConstantValue, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
			}
			return null;
		}

		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x0600638F RID: 25487 RVA: 0x0017D538 File Offset: 0x0017B738
		ImmutableArray<byte> IFieldDefinition.MappedData
		{
			get
			{
				return default(ImmutableArray<byte>);
			}
		}

		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x06006390 RID: 25488 RVA: 0x0017D54E File Offset: 0x0017B74E
		bool IFieldDefinition.IsCompileTimeConstant
		{
			get
			{
				return this.AdaptedFieldSymbol.IsMetadataConstant;
			}
		}

		// Token: 0x170017A9 RID: 6057
		// (get) Token: 0x06006391 RID: 25489 RVA: 0x0017D55B File Offset: 0x0017B75B
		bool IFieldDefinition.IsNotSerialized
		{
			get
			{
				return this.AdaptedFieldSymbol.IsNotSerialized;
			}
		}

		// Token: 0x170017AA RID: 6058
		// (get) Token: 0x06006392 RID: 25490 RVA: 0x0017D568 File Offset: 0x0017B768
		bool IFieldDefinition.IsReadOnly
		{
			get
			{
				return this.AdaptedFieldSymbol.IsReadOnly || (this.AdaptedFieldSymbol.IsConst && !this.AdaptedFieldSymbol.IsMetadataConstant);
			}
		}

		// Token: 0x170017AB RID: 6059
		// (get) Token: 0x06006393 RID: 25491 RVA: 0x0017D596 File Offset: 0x0017B796
		bool IFieldDefinition.IsRuntimeSpecial
		{
			get
			{
				return this.AdaptedFieldSymbol.HasRuntimeSpecialName;
			}
		}

		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x06006394 RID: 25492 RVA: 0x0017D5A3 File Offset: 0x0017B7A3
		bool IFieldDefinition.IsSpecialName
		{
			get
			{
				return this.AdaptedFieldSymbol.HasSpecialName;
			}
		}

		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x06006395 RID: 25493 RVA: 0x0017D5B0 File Offset: 0x0017B7B0
		bool IFieldDefinition.IsStatic
		{
			get
			{
				return this.AdaptedFieldSymbol.IsStatic;
			}
		}

		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x06006396 RID: 25494 RVA: 0x0017D5BD File Offset: 0x0017B7BD
		bool IFieldDefinition.IsMarshalledExplicitly
		{
			get
			{
				return this.AdaptedFieldSymbol.IsMarshalledExplicitly;
			}
		}

		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x06006397 RID: 25495 RVA: 0x0017D5CA File Offset: 0x0017B7CA
		IMarshallingInformation IFieldDefinition.MarshallingInformation
		{
			get
			{
				return this.AdaptedFieldSymbol.MarshallingInformation;
			}
		}

		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x06006398 RID: 25496 RVA: 0x0017D5D7 File Offset: 0x0017B7D7
		ImmutableArray<byte> IFieldDefinition.MarshallingDescriptor
		{
			get
			{
				return this.AdaptedFieldSymbol.MarshallingDescriptor;
			}
		}

		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x06006399 RID: 25497 RVA: 0x0017D5E4 File Offset: 0x0017B7E4
		int IFieldDefinition.Offset
		{
			get
			{
				return this.AdaptedFieldSymbol.TypeLayoutOffset.GetValueOrDefault();
			}
		}

		// Token: 0x170017B2 RID: 6066
		// (get) Token: 0x0600639A RID: 25498 RVA: 0x0017D604 File Offset: 0x0017B804
		ITypeDefinition ITypeDefinitionMember.ContainingTypeDefinition
		{
			get
			{
				return this.AdaptedFieldSymbol.ContainingType.GetCciAdapter();
			}
		}

		// Token: 0x170017B3 RID: 6067
		// (get) Token: 0x0600639B RID: 25499 RVA: 0x0017D616 File Offset: 0x0017B816
		TypeMemberVisibility ITypeDefinitionMember.Visibility
		{
			get
			{
				return PEModuleBuilder.MemberVisibility(this.AdaptedFieldSymbol);
			}
		}

		// Token: 0x170017B4 RID: 6068
		// (get) Token: 0x0600639C RID: 25500 RVA: 0x0017D623 File Offset: 0x0017B823
		IFieldReference ISpecializedFieldReference.UnspecializedVersion
		{
			get
			{
				return this.AdaptedFieldSymbol.OriginalDefinition.GetCciAdapter();
			}
		}

		// Token: 0x170017B5 RID: 6069
		// (get) Token: 0x0600639D RID: 25501 RVA: 0x0017D635 File Offset: 0x0017B835
		internal FieldSymbol AdaptedFieldSymbol
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600639E RID: 25502 RVA: 0x0017D638 File Offset: 0x0017B838
		internal new FieldSymbol GetCciAdapter()
		{
			return this;
		}

		// Token: 0x170017B6 RID: 6070
		// (get) Token: 0x0600639F RID: 25503 RVA: 0x0017D63B File Offset: 0x0017B83B
		internal virtual bool IsMarshalledExplicitly
		{
			get
			{
				return this.MarshallingInformation != null;
			}
		}

		// Token: 0x170017B7 RID: 6071
		// (get) Token: 0x060063A0 RID: 25504 RVA: 0x0017D648 File Offset: 0x0017B848
		internal virtual ImmutableArray<byte> MarshallingDescriptor
		{
			get
			{
				return default(ImmutableArray<byte>);
			}
		}

		// Token: 0x060063A1 RID: 25505 RVA: 0x0017D65E File Offset: 0x0017B85E
		internal FieldSymbol()
		{
		}

		// Token: 0x170017B8 RID: 6072
		// (get) Token: 0x060063A2 RID: 25506 RVA: 0x0017D666 File Offset: 0x0017B866
		public new virtual FieldSymbol OriginalDefinition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170017B9 RID: 6073
		// (get) Token: 0x060063A3 RID: 25507 RVA: 0x0017D669 File Offset: 0x0017B869
		protected sealed override Symbol OriginalSymbolDefinition
		{
			get
			{
				return this.OriginalDefinition;
			}
		}

		// Token: 0x170017BA RID: 6074
		// (get) Token: 0x060063A4 RID: 25508 RVA: 0x0017D671 File Offset: 0x0017B871
		public TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this.GetFieldType(ConsList<FieldSymbol>.Empty);
			}
		}

		// Token: 0x170017BB RID: 6075
		// (get) Token: 0x060063A5 RID: 25509
		public abstract FlowAnalysisAnnotations FlowAnalysisAnnotations { get; }

		// Token: 0x170017BC RID: 6076
		// (get) Token: 0x060063A6 RID: 25510 RVA: 0x0017D680 File Offset: 0x0017B880
		public TypeSymbol Type
		{
			get
			{
				return this.TypeWithAnnotations.Type;
			}
		}

		// Token: 0x060063A7 RID: 25511
		internal abstract TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound);

		// Token: 0x170017BD RID: 6077
		// (get) Token: 0x060063A8 RID: 25512
		public abstract Symbol AssociatedSymbol { get; }

		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x060063A9 RID: 25513
		public abstract bool IsReadOnly { get; }

		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x060063AA RID: 25514
		public abstract bool IsVolatile { get; }

		// Token: 0x170017C0 RID: 6080
		// (get) Token: 0x060063AB RID: 25515 RVA: 0x0017D69B File Offset: 0x0017B89B
		public virtual bool RequiresInstanceReceiver
		{
			get
			{
				return !this.IsStatic;
			}
		}

		// Token: 0x170017C1 RID: 6081
		// (get) Token: 0x060063AC RID: 25516 RVA: 0x0017D6A6 File Offset: 0x0017B8A6
		public virtual bool IsFixedSizeBuffer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017C2 RID: 6082
		// (get) Token: 0x060063AD RID: 25517 RVA: 0x0017D6A9 File Offset: 0x0017B8A9
		public virtual int FixedSize
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060063AE RID: 25518 RVA: 0x0017D6AC File Offset: 0x0017B8AC
		internal virtual NamedTypeSymbol FixedImplementationType(PEModuleBuilder emitModule)
		{
			return null;
		}

		// Token: 0x170017C3 RID: 6083
		// (get) Token: 0x060063AF RID: 25519 RVA: 0x0017D6AF File Offset: 0x0017B8AF
		internal virtual bool IsCapturedFrame
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017C4 RID: 6084
		// (get) Token: 0x060063B0 RID: 25520
		public abstract bool IsConst { get; }

		// Token: 0x170017C5 RID: 6085
		// (get) Token: 0x060063B1 RID: 25521 RVA: 0x0017D6B2 File Offset: 0x0017B8B2
		public bool IsMetadataConstant
		{
			get
			{
				return this.IsConst && this.Type.SpecialType != SpecialType.System_Decimal;
			}
		}

		// Token: 0x170017C6 RID: 6086
		// (get) Token: 0x060063B2 RID: 25522 RVA: 0x0017D6D0 File Offset: 0x0017B8D0
		public virtual bool HasConstantValue
		{
			get
			{
				if (!this.IsConst)
				{
					return false;
				}
				ConstantValue constantValue = this.GetConstantValue(ConstantFieldsInProgress.Empty, false);
				return constantValue != null && !constantValue.IsBad;
			}
		}

		// Token: 0x170017C7 RID: 6087
		// (get) Token: 0x060063B3 RID: 25523 RVA: 0x0017D708 File Offset: 0x0017B908
		public virtual object ConstantValue
		{
			get
			{
				if (!this.IsConst)
				{
					return null;
				}
				ConstantValue constantValue = this.GetConstantValue(ConstantFieldsInProgress.Empty, false);
				if (!(constantValue == null))
				{
					return constantValue.Value;
				}
				return null;
			}
		}

		// Token: 0x060063B4 RID: 25524
		internal abstract ConstantValue GetConstantValue(ConstantFieldsInProgress inProgress, bool earlyDecodingWellKnownAttributes);

		// Token: 0x170017C8 RID: 6088
		// (get) Token: 0x060063B5 RID: 25525 RVA: 0x0017D73D File Offset: 0x0017B93D
		public sealed override SymbolKind Kind
		{
			get
			{
				return SymbolKind.Field;
			}
		}

		// Token: 0x060063B6 RID: 25526 RVA: 0x0017D740 File Offset: 0x0017B940
		internal override TResult Accept<TArgument, TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument argument)
		{
			return visitor.VisitField(this, argument);
		}

		// Token: 0x060063B7 RID: 25527 RVA: 0x0017D74A File Offset: 0x0017B94A
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitField(this);
		}

		// Token: 0x060063B8 RID: 25528 RVA: 0x0017D753 File Offset: 0x0017B953
		public override TResult Accept<TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitField(this);
		}

		// Token: 0x170017C9 RID: 6089
		// (get) Token: 0x060063B9 RID: 25529 RVA: 0x0017D75C File Offset: 0x0017B95C
		public sealed override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017CA RID: 6090
		// (get) Token: 0x060063BA RID: 25530 RVA: 0x0017D75F File Offset: 0x0017B95F
		public sealed override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017CB RID: 6091
		// (get) Token: 0x060063BB RID: 25531 RVA: 0x0017D762 File Offset: 0x0017B962
		public sealed override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x060063BC RID: 25532 RVA: 0x0017D765 File Offset: 0x0017B965
		public sealed override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017CD RID: 6093
		// (get) Token: 0x060063BD RID: 25533 RVA: 0x0017D768 File Offset: 0x0017B968
		public sealed override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017CE RID: 6094
		// (get) Token: 0x060063BE RID: 25534
		internal abstract bool HasSpecialName { get; }

		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x060063BF RID: 25535
		internal abstract bool HasRuntimeSpecialName { get; }

		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x060063C0 RID: 25536
		internal abstract bool IsNotSerialized { get; }

		// Token: 0x170017D1 RID: 6097
		// (get) Token: 0x060063C1 RID: 25537 RVA: 0x0017D76B File Offset: 0x0017B96B
		internal virtual bool HasPointerType
		{
			get
			{
				return this.Type.IsPointerOrFunctionPointer();
			}
		}

		// Token: 0x170017D2 RID: 6098
		// (get) Token: 0x060063C2 RID: 25538
		internal abstract MarshalPseudoCustomAttributeData MarshallingInformation { get; }

		// Token: 0x170017D3 RID: 6099
		// (get) Token: 0x060063C3 RID: 25539 RVA: 0x0017D778 File Offset: 0x0017B978
		internal virtual UnmanagedType MarshallingType
		{
			get
			{
				MarshalPseudoCustomAttributeData marshallingInformation = this.MarshallingInformation;
				if (marshallingInformation == null)
				{
					return 0;
				}
				return marshallingInformation.UnmanagedType;
			}
		}

		// Token: 0x170017D4 RID: 6100
		// (get) Token: 0x060063C4 RID: 25540
		internal abstract int? TypeLayoutOffset { get; }

		// Token: 0x060063C5 RID: 25541 RVA: 0x0017D797 File Offset: 0x0017B997
		internal virtual FieldSymbol AsMember(NamedTypeSymbol newOwner)
		{
			if (!newOwner.IsDefinition)
			{
				return new SubstitutedFieldSymbol(newOwner as SubstitutedNamedTypeSymbol, this);
			}
			return this;
		}

		// Token: 0x060063C6 RID: 25542 RVA: 0x0017D7AF File Offset: 0x0017B9AF
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			if (base.IsDefinition)
			{
				return new UseSiteInfo<AssemblySymbol>(base.PrimaryDependency);
			}
			return this.OriginalDefinition.GetUseSiteInfo();
		}

		// Token: 0x060063C7 RID: 25543 RVA: 0x0017D7D0 File Offset: 0x0017B9D0
		internal bool CalculateUseSiteDiagnostic(ref UseSiteInfo<AssemblySymbol> result)
		{
			if (base.DeriveUseSiteInfoFromType(ref result, this.TypeWithAnnotations, Symbol.AllowedRequiredModifierType.System_Runtime_CompilerServices_Volatile))
			{
				return true;
			}
			if (this.ContainingModule.HasUnifiedReferences)
			{
				HashSet<TypeSymbol> hashSet = null;
				DiagnosticInfo diagnosticInfo = result.DiagnosticInfo;
				if (this.TypeWithAnnotations.GetUnificationUseSiteDiagnosticRecursive(ref diagnosticInfo, this, ref hashSet))
				{
					result = result.AdjustDiagnosticInfo(diagnosticInfo);
					return true;
				}
				result = result.AdjustDiagnosticInfo(diagnosticInfo);
			}
			return false;
		}

		// Token: 0x170017D5 RID: 6101
		// (get) Token: 0x060063C8 RID: 25544 RVA: 0x0017D837 File Offset: 0x0017BA37
		protected override int HighestPriorityUseSiteError
		{
			get
			{
				return 570;
			}
		}

		// Token: 0x170017D6 RID: 6102
		// (get) Token: 0x060063C9 RID: 25545 RVA: 0x0017D840 File Offset: 0x0017BA40
		public sealed override bool HasUnsupportedMetadata
		{
			get
			{
				DiagnosticInfo diagnosticInfo = this.GetUseSiteInfo().DiagnosticInfo;
				return diagnosticInfo != null && diagnosticInfo.Code == 570;
			}
		}

		// Token: 0x170017D7 RID: 6103
		// (get) Token: 0x060063CA RID: 25546 RVA: 0x0017D86B File Offset: 0x0017BA6B
		public virtual bool IsVirtualTupleField
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017D8 RID: 6104
		// (get) Token: 0x060063CB RID: 25547 RVA: 0x0017D86E File Offset: 0x0017BA6E
		public virtual bool IsDefaultTupleElement
		{
			get
			{
				return this.TupleElementIndex >= 0;
			}
		}

		// Token: 0x170017D9 RID: 6105
		// (get) Token: 0x060063CC RID: 25548 RVA: 0x0017D87C File Offset: 0x0017BA7C
		public virtual bool IsExplicitlyNamedTupleElement
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017DA RID: 6106
		// (get) Token: 0x060063CD RID: 25549 RVA: 0x0017D87F File Offset: 0x0017BA7F
		public virtual FieldSymbol TupleUnderlyingField
		{
			get
			{
				if (!this.ContainingType.IsTupleType)
				{
					return null;
				}
				return this;
			}
		}

		// Token: 0x170017DB RID: 6107
		// (get) Token: 0x060063CE RID: 25550 RVA: 0x0017D891 File Offset: 0x0017BA91
		public virtual FieldSymbol CorrespondingTupleField
		{
			get
			{
				if (this.TupleElementIndex < 0)
				{
					return null;
				}
				return this;
			}
		}

		// Token: 0x060063CF RID: 25551 RVA: 0x0017D89F File Offset: 0x0017BA9F
		internal bool IsTupleElement()
		{
			return this.CorrespondingTupleField != null;
		}

		// Token: 0x170017DC RID: 6108
		// (get) Token: 0x060063D0 RID: 25552 RVA: 0x0017D8AC File Offset: 0x0017BAAC
		public virtual int TupleElementIndex
		{
			get
			{
				SmallDictionary<FieldSymbol, int> tupleFieldDefinitionsToIndexMap = this.ContainingType.TupleFieldDefinitionsToIndexMap;
				int num;
				if (tupleFieldDefinitionsToIndexMap != null && tupleFieldDefinitionsToIndexMap.TryGetValue(this.OriginalDefinition, out num))
				{
					return num;
				}
				return -1;
			}
		}

		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x060063D1 RID: 25553 RVA: 0x0017D8DB File Offset: 0x0017BADB
		bool IFieldSymbolInternal.IsVolatile
		{
			get
			{
				return this.IsVolatile;
			}
		}

		// Token: 0x060063D2 RID: 25554 RVA: 0x0017D8E3 File Offset: 0x0017BAE3
		protected override ISymbol CreateISymbol()
		{
			return new FieldSymbol(this);
		}

		// Token: 0x060063D3 RID: 25555 RVA: 0x0017D8EC File Offset: 0x0017BAEC
		public override bool Equals(Symbol other, TypeCompareKind compareKind)
		{
			SubstitutedFieldSymbol substitutedFieldSymbol = other as SubstitutedFieldSymbol;
			if (substitutedFieldSymbol != null)
			{
				return substitutedFieldSymbol.Equals(this, compareKind);
			}
			return base.Equals(other, compareKind);
		}

		// Token: 0x060063D4 RID: 25556 RVA: 0x0017D914 File Offset: 0x0017BB14
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
