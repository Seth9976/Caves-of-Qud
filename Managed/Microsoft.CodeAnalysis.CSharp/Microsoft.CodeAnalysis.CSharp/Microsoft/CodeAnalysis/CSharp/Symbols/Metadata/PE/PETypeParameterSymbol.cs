using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005F1 RID: 1521
	internal sealed class PETypeParameterSymbol : TypeParameterSymbol
	{
		// Token: 0x06007FB3 RID: 32691 RVA: 0x001CBC3C File Offset: 0x001C9E3C
		internal PETypeParameterSymbol(PEModuleSymbol moduleSymbol, PENamedTypeSymbol definingNamedType, ushort ordinal, GenericParameterHandle handle)
			: this(moduleSymbol, definingNamedType, ordinal, handle)
		{
		}

		// Token: 0x06007FB4 RID: 32692 RVA: 0x001CBC49 File Offset: 0x001C9E49
		internal PETypeParameterSymbol(PEModuleSymbol moduleSymbol, PEMethodSymbol definingMethod, ushort ordinal, GenericParameterHandle handle)
			: this(moduleSymbol, definingMethod, ordinal, handle)
		{
		}

		// Token: 0x06007FB5 RID: 32693 RVA: 0x001CBC58 File Offset: 0x001C9E58
		private PETypeParameterSymbol(PEModuleSymbol moduleSymbol, Symbol definingSymbol, ushort ordinal, GenericParameterHandle handle)
		{
			this._containingSymbol = definingSymbol;
			GenericParameterAttributes genericParameterAttributes = 0;
			try
			{
				moduleSymbol.Module.GetGenericParamPropsOrThrow(handle, out this._name, out genericParameterAttributes);
			}
			catch (BadImageFormatException)
			{
				if (this._name == null)
				{
					this._name = string.Empty;
				}
				this._lazyCachedConstraintsUseSiteInfo.Initialize(new CSDiagnosticInfo(ErrorCode.ERR_BindToBogus, new object[] { this }));
			}
			this._flags = (((genericParameterAttributes & 8) == null) ? genericParameterAttributes : (genericParameterAttributes & -17));
			this._ordinal = ordinal;
			this._handle = handle;
		}

		// Token: 0x17002634 RID: 9780
		// (get) Token: 0x06007FB6 RID: 32694 RVA: 0x001CBD08 File Offset: 0x001C9F08
		public override TypeParameterKind TypeParameterKind
		{
			get
			{
				if (this.ContainingSymbol.Kind != SymbolKind.Method)
				{
					return TypeParameterKind.Type;
				}
				return TypeParameterKind.Method;
			}
		}

		// Token: 0x17002635 RID: 9781
		// (get) Token: 0x06007FB7 RID: 32695 RVA: 0x001CBD1C File Offset: 0x001C9F1C
		public override int Ordinal
		{
			get
			{
				return (int)this._ordinal;
			}
		}

		// Token: 0x17002636 RID: 9782
		// (get) Token: 0x06007FB8 RID: 32696 RVA: 0x001CBD24 File Offset: 0x001C9F24
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17002637 RID: 9783
		// (get) Token: 0x06007FB9 RID: 32697 RVA: 0x001CBD2C File Offset: 0x001C9F2C
		internal GenericParameterHandle Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x17002638 RID: 9784
		// (get) Token: 0x06007FBA RID: 32698 RVA: 0x001CBD34 File Offset: 0x001C9F34
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x17002639 RID: 9785
		// (get) Token: 0x06007FBB RID: 32699 RVA: 0x001CBD3C File Offset: 0x001C9F3C
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._containingSymbol.ContainingAssembly;
			}
		}

		// Token: 0x06007FBC RID: 32700 RVA: 0x001CBD4C File Offset: 0x001C9F4C
		private ImmutableArray<TypeWithAnnotations> GetDeclaredConstraintTypes(ConsList<PETypeParameterSymbol> inProgress)
		{
			if (this._lazyDeclaredConstraintTypes.IsDefault)
			{
				PEModuleSymbol pemoduleSymbol = (PEModuleSymbol)this.ContainingModule;
				PEModule module = pemoduleSymbol.Module;
				GenericParameterConstraintHandleCollection constraintHandleCollection = this.GetConstraintHandleCollection(module);
				bool flag = false;
				ImmutableArray<TypeWithAnnotations> immutableArray;
				if (constraintHandleCollection.Count > 0)
				{
					ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
					MetadataDecoder decoderForConstraintTypes = this.GetDecoderForConstraintTypes(pemoduleSymbol);
					TypeWithAnnotations typeWithAnnotations = default(TypeWithAnnotations);
					MetadataReader metadataReader = module.MetadataReader;
					foreach (GenericParameterConstraintHandle genericParameterConstraintHandle in constraintHandleCollection)
					{
						TypeWithAnnotations constraintTypeOrDefault = this.GetConstraintTypeOrDefault(pemoduleSymbol, metadataReader, decoderForConstraintTypes, genericParameterConstraintHandle, ref flag);
						if (constraintTypeOrDefault.HasType && !ConstraintsHelper.IsObjectConstraint(constraintTypeOrDefault, ref typeWithAnnotations))
						{
							instance.Add(constraintTypeOrDefault);
						}
					}
					if (typeWithAnnotations.HasType && ConstraintsHelper.IsObjectConstraintSignificant(base.CalculateIsNotNullableFromNonTypeConstraints(), typeWithAnnotations))
					{
						if (instance.Count == 0)
						{
							if (typeWithAnnotations.NullableAnnotation.IsOblivious() && !this.HasReferenceTypeConstraint)
							{
								typeWithAnnotations = default(TypeWithAnnotations);
							}
						}
						else
						{
							inProgress = inProgress.Prepend(this);
							ArrayBuilder<TypeWithAnnotations>.Enumerator enumerator2 = instance.GetEnumerator();
							while (enumerator2.MoveNext())
							{
								bool flag2;
								if (!ConstraintsHelper.IsObjectConstraintSignificant(PETypeParameterSymbol.IsNotNullableFromConstraintType(enumerator2.Current, inProgress, out flag2), typeWithAnnotations))
								{
									typeWithAnnotations = default(TypeWithAnnotations);
									break;
								}
							}
						}
						if (typeWithAnnotations.HasType)
						{
							instance.Insert(0, typeWithAnnotations);
						}
					}
					immutableArray = instance.ToImmutableAndFree();
				}
				else
				{
					immutableArray = ImmutableArray<TypeWithAnnotations>.Empty;
				}
				if ((flag && (this._flags & 8) == null) || flag != module.HasIsUnmanagedAttribute(this._handle))
				{
					flag = false;
					this._lazyCachedConstraintsUseSiteInfo.InterlockedCompareExchange(null, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_BindToBogus, new object[] { this })));
				}
				this._lazyHasIsUnmanagedConstraint = flag.ToThreeState();
				ImmutableInterlocked.InterlockedInitialize<TypeWithAnnotations>(ref this._lazyDeclaredConstraintTypes, immutableArray);
			}
			return this._lazyDeclaredConstraintTypes;
		}

		// Token: 0x06007FBD RID: 32701 RVA: 0x001CBF30 File Offset: 0x001CA130
		private MetadataDecoder GetDecoderForConstraintTypes(PEModuleSymbol moduleSymbol)
		{
			MetadataDecoder metadataDecoder;
			if (this._containingSymbol.Kind == SymbolKind.Method)
			{
				metadataDecoder = new MetadataDecoder(moduleSymbol, (PEMethodSymbol)this._containingSymbol);
			}
			else
			{
				metadataDecoder = new MetadataDecoder(moduleSymbol, (PENamedTypeSymbol)this._containingSymbol);
			}
			return metadataDecoder;
		}

		// Token: 0x06007FBE RID: 32702 RVA: 0x001CBF74 File Offset: 0x001CA174
		private TypeWithAnnotations GetConstraintTypeOrDefault(PEModuleSymbol moduleSymbol, MetadataReader metadataReader, MetadataDecoder tokenDecoder, GenericParameterConstraintHandle constraintHandle, ref bool hasUnmanagedModreqPattern)
		{
			ImmutableArray<ModifierInfo<TypeSymbol>> immutableArray;
			TypeSymbol typeSymbol = tokenDecoder.DecodeGenericParameterConstraint(metadataReader.GetGenericParameterConstraint(constraintHandle).Type, out immutableArray);
			if (!immutableArray.IsDefaultOrEmpty && immutableArray.Length > 1)
			{
				typeSymbol = new UnsupportedMetadataTypeSymbol(null);
			}
			else if (typeSymbol.SpecialType == SpecialType.System_ValueType)
			{
				if (!immutableArray.IsDefaultOrEmpty)
				{
					ModifierInfo<TypeSymbol> modifierInfo = immutableArray.Single<ModifierInfo<TypeSymbol>>();
					if (!modifierInfo.IsOptional && modifierInfo.Modifier.IsWellKnownTypeUnmanagedType())
					{
						hasUnmanagedModreqPattern = true;
					}
					else
					{
						typeSymbol = new UnsupportedMetadataTypeSymbol(null);
					}
				}
				if (typeSymbol.SpecialType == SpecialType.System_ValueType && (this._flags & 8) != null)
				{
					return default(TypeWithAnnotations);
				}
			}
			else if (!immutableArray.IsDefaultOrEmpty)
			{
				typeSymbol = new UnsupportedMetadataTypeSymbol(null);
			}
			return TupleTypeDecoder.DecodeTupleTypesIfApplicable(NullableTypeDecoder.TransformType(TypeWithAnnotations.Create(typeSymbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), constraintHandle, moduleSymbol, this._containingSymbol, this._containingSymbol), constraintHandle, moduleSymbol);
		}

		// Token: 0x06007FBF RID: 32703 RVA: 0x001CC058 File Offset: 0x001CA258
		private static bool? IsNotNullableFromConstraintType(TypeWithAnnotations constraintType, ConsList<PETypeParameterSymbol> inProgress, out bool isNonNullableValueType)
		{
			PETypeParameterSymbol petypeParameterSymbol = constraintType.Type as PETypeParameterSymbol;
			if (petypeParameterSymbol == null || petypeParameterSymbol.ContainingSymbol != inProgress.Head.ContainingSymbol || petypeParameterSymbol.GetConstraintHandleCollection().Count == 0)
			{
				return TypeParameterSymbol.IsNotNullableFromConstraintType(constraintType, out isNonNullableValueType);
			}
			bool? flag = petypeParameterSymbol.CalculateIsNotNullable(inProgress, out isNonNullableValueType);
			if (isNonNullableValueType)
			{
				return new bool?(true);
			}
			if (!constraintType.NullableAnnotation.IsAnnotated())
			{
				bool? flag2 = flag;
				bool flag3 = false;
				if (!((flag2.GetValueOrDefault() == flag3) & (flag2 != null)))
				{
					if (constraintType.NullableAnnotation.IsOblivious() || flag == null)
					{
						return default(bool?);
					}
					return new bool?(true);
				}
			}
			return new bool?(false);
		}

		// Token: 0x06007FC0 RID: 32704 RVA: 0x001CC108 File Offset: 0x001CA308
		private bool? CalculateIsNotNullable(ConsList<PETypeParameterSymbol> inProgress, out bool isNonNullableValueType)
		{
			if (inProgress.ContainsReference(this))
			{
				isNonNullableValueType = false;
				return new bool?(false);
			}
			if (this.HasValueTypeConstraint)
			{
				isNonNullableValueType = true;
				return new bool?(true);
			}
			bool? flag = base.CalculateIsNotNullableFromNonTypeConstraints();
			ImmutableArray<TypeWithAnnotations> declaredConstraintTypes = this.GetDeclaredConstraintTypes(inProgress);
			if (declaredConstraintTypes.IsEmpty)
			{
				isNonNullableValueType = false;
				return flag;
			}
			bool? flag2 = PETypeParameterSymbol.IsNotNullableFromConstraintTypes(declaredConstraintTypes, inProgress, out isNonNullableValueType);
			if (isNonNullableValueType)
			{
				return new bool?(true);
			}
			bool? flag3 = flag2;
			bool flag4 = true;
			if (!((flag3.GetValueOrDefault() == flag4) & (flag3 != null)))
			{
				flag3 = flag;
				flag4 = false;
				if (!((flag3.GetValueOrDefault() == flag4) & (flag3 != null)))
				{
					return flag;
				}
			}
			return flag2;
		}

		// Token: 0x06007FC1 RID: 32705 RVA: 0x001CC1A4 File Offset: 0x001CA3A4
		private static bool? IsNotNullableFromConstraintTypes(ImmutableArray<TypeWithAnnotations> constraintTypes, ConsList<PETypeParameterSymbol> inProgress, out bool isNonNullableValueType)
		{
			isNonNullableValueType = false;
			bool? flag = new bool?(false);
			foreach (TypeWithAnnotations typeWithAnnotations in constraintTypes)
			{
				bool? flag2 = PETypeParameterSymbol.IsNotNullableFromConstraintType(typeWithAnnotations, inProgress, out isNonNullableValueType);
				if (isNonNullableValueType)
				{
					return new bool?(true);
				}
				bool? flag3 = flag2;
				bool flag4 = true;
				if ((flag3.GetValueOrDefault() == flag4) & (flag3 != null))
				{
					flag = new bool?(true);
				}
				else if (flag2 == null)
				{
					flag3 = flag;
					flag4 = false;
					if ((flag3.GetValueOrDefault() == flag4) & (flag3 != null))
					{
						flag = default(bool?);
					}
				}
			}
			return flag;
		}

		// Token: 0x06007FC2 RID: 32706 RVA: 0x001CC23C File Offset: 0x001CA43C
		private GenericParameterConstraintHandleCollection GetConstraintHandleCollection(PEModule module)
		{
			GenericParameterConstraintHandleCollection genericParameterConstraintHandleCollection;
			try
			{
				genericParameterConstraintHandleCollection = module.MetadataReader.GetGenericParameter(this._handle).GetConstraints();
			}
			catch (BadImageFormatException)
			{
				genericParameterConstraintHandleCollection = default(GenericParameterConstraintHandleCollection);
				this._lazyCachedConstraintsUseSiteInfo.InterlockedCompareExchange(null, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_BindToBogus, new object[] { this })));
			}
			return genericParameterConstraintHandleCollection;
		}

		// Token: 0x06007FC3 RID: 32707 RVA: 0x001CC2A8 File Offset: 0x001CA4A8
		private GenericParameterConstraintHandleCollection GetConstraintHandleCollection()
		{
			return this.GetConstraintHandleCollection(((PEModuleSymbol)this.ContainingModule).Module);
		}

		// Token: 0x1700263A RID: 9786
		// (get) Token: 0x06007FC4 RID: 32708 RVA: 0x001CC2C0 File Offset: 0x001CA4C0
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._containingSymbol.Locations;
			}
		}

		// Token: 0x1700263B RID: 9787
		// (get) Token: 0x06007FC5 RID: 32709 RVA: 0x001CC2CD File Offset: 0x001CA4CD
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x1700263C RID: 9788
		// (get) Token: 0x06007FC6 RID: 32710 RVA: 0x001CC2D4 File Offset: 0x001CA4D4
		public override bool HasConstructorConstraint
		{
			get
			{
				return (this._flags & 16) > 0;
			}
		}

		// Token: 0x1700263D RID: 9789
		// (get) Token: 0x06007FC7 RID: 32711 RVA: 0x001CC2E2 File Offset: 0x001CA4E2
		public override bool HasReferenceTypeConstraint
		{
			get
			{
				return (this._flags & 4) > 0;
			}
		}

		// Token: 0x1700263E RID: 9790
		// (get) Token: 0x06007FC8 RID: 32712 RVA: 0x001CC2EF File Offset: 0x001CA4EF
		public override bool IsReferenceTypeFromConstraintTypes
		{
			get
			{
				return TypeParameterSymbol.CalculateIsReferenceTypeFromConstraintTypes(base.ConstraintTypesNoUseSiteDiagnostics);
			}
		}

		// Token: 0x06007FC9 RID: 32713 RVA: 0x001CC2FC File Offset: 0x001CA4FC
		private byte GetNullableAttributeValue()
		{
			byte b;
			ImmutableArray<byte> immutableArray;
			if (((PEModuleSymbol)this.ContainingModule).Module.HasNullableAttribute(this._handle, out b, out immutableArray))
			{
				return b;
			}
			return this._containingSymbol.GetNullableContextValue().GetValueOrDefault();
		}

		// Token: 0x1700263F RID: 9791
		// (get) Token: 0x06007FCA RID: 32714 RVA: 0x001CC344 File Offset: 0x001CA544
		internal override bool? ReferenceTypeConstraintIsNullable
		{
			get
			{
				if (!this.HasReferenceTypeConstraint)
				{
					return new bool?(false);
				}
				byte nullableAttributeValue = this.GetNullableAttributeValue();
				if (nullableAttributeValue == 1)
				{
					return new bool?(false);
				}
				if (nullableAttributeValue == 2)
				{
					return new bool?(true);
				}
				return default(bool?);
			}
		}

		// Token: 0x17002640 RID: 9792
		// (get) Token: 0x06007FCB RID: 32715 RVA: 0x001CC386 File Offset: 0x001CA586
		public override bool HasNotNullConstraint
		{
			get
			{
				return (this._flags & 12) == null && this.GetNullableAttributeValue() == 1;
			}
		}

		// Token: 0x17002641 RID: 9793
		// (get) Token: 0x06007FCC RID: 32716 RVA: 0x001CC3A0 File Offset: 0x001CA5A0
		internal override bool? IsNotNullable
		{
			get
			{
				if ((this._flags & 12) == null && !this.HasNotNullConstraint)
				{
					PEModuleSymbol pemoduleSymbol = (PEModuleSymbol)this.ContainingModule;
					PEModule module = pemoduleSymbol.Module;
					GenericParameterConstraintHandleCollection constraintHandleCollection = this.GetConstraintHandleCollection(module);
					if (constraintHandleCollection.Count == 0)
					{
						if (this.GetNullableAttributeValue() == 2)
						{
							return new bool?(false);
						}
						return default(bool?);
					}
					else if (this.GetDeclaredConstraintTypes(ConsList<PETypeParameterSymbol>.Empty).IsEmpty)
					{
						ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
						MetadataDecoder decoderForConstraintTypes = this.GetDecoderForConstraintTypes(pemoduleSymbol);
						bool flag = false;
						MetadataReader metadataReader = module.MetadataReader;
						foreach (GenericParameterConstraintHandle genericParameterConstraintHandle in constraintHandleCollection)
						{
							TypeWithAnnotations constraintTypeOrDefault = this.GetConstraintTypeOrDefault(pemoduleSymbol, metadataReader, decoderForConstraintTypes, genericParameterConstraintHandle, ref flag);
							if (constraintTypeOrDefault.HasType)
							{
								instance.Add(constraintTypeOrDefault);
							}
						}
						return TypeParameterSymbol.IsNotNullableFromConstraintTypes(instance.ToImmutableAndFree());
					}
				}
				return base.CalculateIsNotNullable();
			}
		}

		// Token: 0x17002642 RID: 9794
		// (get) Token: 0x06007FCD RID: 32717 RVA: 0x001CC4A8 File Offset: 0x001CA6A8
		public override bool HasValueTypeConstraint
		{
			get
			{
				return (this._flags & 8) > 0;
			}
		}

		// Token: 0x17002643 RID: 9795
		// (get) Token: 0x06007FCE RID: 32718 RVA: 0x001CC4B5 File Offset: 0x001CA6B5
		public override bool IsValueTypeFromConstraintTypes
		{
			get
			{
				return TypeParameterSymbol.CalculateIsValueTypeFromConstraintTypes(base.ConstraintTypesNoUseSiteDiagnostics);
			}
		}

		// Token: 0x17002644 RID: 9796
		// (get) Token: 0x06007FCF RID: 32719 RVA: 0x001CC4C2 File Offset: 0x001CA6C2
		public override bool HasUnmanagedTypeConstraint
		{
			get
			{
				this.GetDeclaredConstraintTypes(ConsList<PETypeParameterSymbol>.Empty);
				return this._lazyHasIsUnmanagedConstraint.Value();
			}
		}

		// Token: 0x17002645 RID: 9797
		// (get) Token: 0x06007FD0 RID: 32720 RVA: 0x001CC4DB File Offset: 0x001CA6DB
		public override VarianceKind Variance
		{
			get
			{
				return this._flags & 3;
			}
		}

		// Token: 0x06007FD1 RID: 32721 RVA: 0x001CC4E8 File Offset: 0x001CA6E8
		internal override void EnsureAllConstraintsAreResolved()
		{
			if (!this._lazyBounds.IsSet())
			{
				TypeParameterSymbol.EnsureAllConstraintsAreResolved((this._containingSymbol.Kind == SymbolKind.Method) ? ((PEMethodSymbol)this._containingSymbol).TypeParameters : ((PENamedTypeSymbol)this._containingSymbol).TypeParameters);
			}
		}

		// Token: 0x06007FD2 RID: 32722 RVA: 0x001CC538 File Offset: 0x001CA738
		internal override ImmutableArray<TypeWithAnnotations> GetConstraintTypes(ConsList<TypeParameterSymbol> inProgress)
		{
			TypeParameterBounds bounds = this.GetBounds(inProgress);
			if (bounds == null)
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
			return bounds.ConstraintTypes;
		}

		// Token: 0x06007FD3 RID: 32723 RVA: 0x001CC55C File Offset: 0x001CA75C
		internal override ImmutableArray<NamedTypeSymbol> GetInterfaces(ConsList<TypeParameterSymbol> inProgress)
		{
			TypeParameterBounds bounds = this.GetBounds(inProgress);
			if (bounds == null)
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}
			return bounds.Interfaces;
		}

		// Token: 0x06007FD4 RID: 32724 RVA: 0x001CC580 File Offset: 0x001CA780
		internal override NamedTypeSymbol GetEffectiveBaseClass(ConsList<TypeParameterSymbol> inProgress)
		{
			TypeParameterBounds bounds = this.GetBounds(inProgress);
			if (bounds == null)
			{
				return this.GetDefaultBaseType();
			}
			return bounds.EffectiveBaseClass;
		}

		// Token: 0x06007FD5 RID: 32725 RVA: 0x001CC5A8 File Offset: 0x001CA7A8
		internal override TypeSymbol GetDeducedBaseType(ConsList<TypeParameterSymbol> inProgress)
		{
			TypeParameterBounds bounds = this.GetBounds(inProgress);
			if (bounds == null)
			{
				return this.GetDefaultBaseType();
			}
			return bounds.DeducedBaseType;
		}

		// Token: 0x06007FD6 RID: 32726 RVA: 0x001CC5D0 File Offset: 0x001CA7D0
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			if (this._lazyCustomAttributes.IsDefault)
			{
				CustomAttributeHandle customAttributeHandle;
				ImmutableArray<CSharpAttributeData> customAttributesForToken = ((PEModuleSymbol)this.ContainingModule).GetCustomAttributesForToken(this.Handle, out customAttributeHandle, this.HasUnmanagedTypeConstraint ? AttributeDescription.IsUnmanagedAttribute : default(AttributeDescription));
				ImmutableInterlocked.InterlockedInitialize<CSharpAttributeData>(ref this._lazyCustomAttributes, customAttributesForToken);
			}
			return this._lazyCustomAttributes;
		}

		// Token: 0x06007FD7 RID: 32727 RVA: 0x001CC634 File Offset: 0x001CA834
		private TypeParameterBounds GetBounds(ConsList<TypeParameterSymbol> inProgress)
		{
			if (this._lazyBounds == TypeParameterBounds.Unset)
			{
				ImmutableArray<TypeWithAnnotations> declaredConstraintTypes = this.GetDeclaredConstraintTypes(ConsList<PETypeParameterSymbol>.Empty);
				ArrayBuilder<TypeParameterDiagnosticInfo> instance = ArrayBuilder<TypeParameterDiagnosticInfo>.GetInstance();
				ArrayBuilder<TypeParameterDiagnosticInfo> arrayBuilder = null;
				bool flag = this._containingSymbol.Kind == SymbolKind.Method && ((MethodSymbol)this._containingSymbol).IsOverride;
				TypeParameterBounds typeParameterBounds = this.ResolveBounds(this.ContainingAssembly.CorLibrary, inProgress.Prepend(this), declaredConstraintTypes, flag, null, instance, ref arrayBuilder, default(CompoundUseSiteInfo<AssemblySymbol>));
				if (arrayBuilder != null)
				{
					instance.AddRange(arrayBuilder);
				}
				AssemblySymbol primaryDependency = base.PrimaryDependency;
				UseSiteInfo<AssemblySymbol> useSiteInfo = new UseSiteInfo<AssemblySymbol>(primaryDependency);
				foreach (TypeParameterDiagnosticInfo typeParameterDiagnosticInfo in instance)
				{
					base.MergeUseSiteInfo(ref useSiteInfo, typeParameterDiagnosticInfo.UseSiteInfo);
					DiagnosticInfo diagnosticInfo = useSiteInfo.DiagnosticInfo;
					if (diagnosticInfo != null && diagnosticInfo.Severity == DiagnosticSeverity.Error)
					{
						break;
					}
				}
				instance.Free();
				this._lazyCachedConstraintsUseSiteInfo.InterlockedCompareExchange(primaryDependency, useSiteInfo);
				Interlocked.CompareExchange<TypeParameterBounds>(ref this._lazyBounds, typeParameterBounds, TypeParameterBounds.Unset);
			}
			return this._lazyBounds;
		}

		// Token: 0x06007FD8 RID: 32728 RVA: 0x001CC73F File Offset: 0x001CA93F
		internal override UseSiteInfo<AssemblySymbol> GetConstraintsUseSiteErrorInfo()
		{
			this.EnsureAllConstraintsAreResolved();
			return this._lazyCachedConstraintsUseSiteInfo.ToUseSiteInfo(base.PrimaryDependency);
		}

		// Token: 0x06007FD9 RID: 32729 RVA: 0x001CC758 File Offset: 0x001CA958
		private NamedTypeSymbol GetDefaultBaseType()
		{
			return this.ContainingAssembly.GetSpecialType(SpecialType.System_Object);
		}

		// Token: 0x17002646 RID: 9798
		// (get) Token: 0x06007FDA RID: 32730 RVA: 0x001CC766 File Offset: 0x001CA966
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001E1E RID: 7710
		private readonly Symbol _containingSymbol;

		// Token: 0x04001E1F RID: 7711
		private readonly GenericParameterHandle _handle;

		// Token: 0x04001E20 RID: 7712
		private readonly string _name;

		// Token: 0x04001E21 RID: 7713
		private readonly ushort _ordinal;

		// Token: 0x04001E22 RID: 7714
		private CachedUseSiteInfo<AssemblySymbol> _lazyCachedConstraintsUseSiteInfo = CachedUseSiteInfo<AssemblySymbol>.Uninitialized;

		// Token: 0x04001E23 RID: 7715
		private readonly GenericParameterAttributes _flags;

		// Token: 0x04001E24 RID: 7716
		private ThreeState _lazyHasIsUnmanagedConstraint;

		// Token: 0x04001E25 RID: 7717
		private TypeParameterBounds _lazyBounds = TypeParameterBounds.Unset;

		// Token: 0x04001E26 RID: 7718
		private ImmutableArray<TypeWithAnnotations> _lazyDeclaredConstraintTypes;

		// Token: 0x04001E27 RID: 7719
		private ImmutableArray<CSharpAttributeData> _lazyCustomAttributes;
	}
}
