using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.DocumentationComments;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005F0 RID: 1520
	internal class PEPropertySymbol : PropertySymbol
	{
		// Token: 0x06007F8C RID: 32652 RVA: 0x001CB190 File Offset: 0x001C9390
		internal static PEPropertySymbol Create(PEModuleSymbol moduleSymbol, PENamedTypeSymbol containingType, PropertyDefinitionHandle handle, PEMethodSymbol getMethod, PEMethodSymbol setMethod)
		{
			MetadataDecoder metadataDecoder = new MetadataDecoder(moduleSymbol, containingType);
			SignatureHeader signatureHeader;
			BadImageFormatException ex;
			ParamInfo<TypeSymbol>[] signatureForProperty = metadataDecoder.GetSignatureForProperty(handle, out signatureHeader, out ex);
			ParamInfo<TypeSymbol> paramInfo = signatureForProperty[0];
			PEPropertySymbol pepropertySymbol = ((paramInfo.CustomModifiers.IsDefaultOrEmpty && paramInfo.RefCustomModifiers.IsDefaultOrEmpty) ? new PEPropertySymbol(moduleSymbol, containingType, handle, getMethod, setMethod, signatureForProperty, metadataDecoder) : new PEPropertySymbol.PEPropertySymbolWithCustomModifiers(moduleSymbol, containingType, handle, getMethod, setMethod, signatureForProperty, metadataDecoder));
			bool flag = pepropertySymbol.RefKind == RefKind.In != pepropertySymbol.RefCustomModifiers.HasInAttributeModifier();
			if (ex != null || flag)
			{
				pepropertySymbol._lazyCachedUseSiteInfo.Initialize(new CSDiagnosticInfo(ErrorCode.ERR_BindToBogus, new object[] { pepropertySymbol }));
			}
			return pepropertySymbol;
		}

		// Token: 0x06007F8D RID: 32653 RVA: 0x001CB240 File Offset: 0x001C9440
		private PEPropertySymbol(PEModuleSymbol moduleSymbol, PENamedTypeSymbol containingType, PropertyDefinitionHandle handle, PEMethodSymbol getMethod, PEMethodSymbol setMethod, ParamInfo<TypeSymbol>[] propertyParams, MetadataDecoder metadataDecoder)
		{
			this._containingType = containingType;
			PEModule module = moduleSymbol.Module;
			PropertyAttributes propertyAttributes = 0;
			BadImageFormatException ex = null;
			try
			{
				module.GetPropertyDefPropsOrThrow(handle, out this._name, out propertyAttributes);
			}
			catch (BadImageFormatException ex)
			{
				if (this._name == null)
				{
					this._name = string.Empty;
				}
			}
			this._getMethod = getMethod;
			this._setMethod = setMethod;
			this._handle = handle;
			BadImageFormatException ex2 = null;
			SignatureHeader signatureHeader;
			ParamInfo<TypeSymbol>[] array = ((getMethod == null) ? null : metadataDecoder.GetSignatureForMethod(getMethod.Handle, out signatureHeader, out ex2, true));
			BadImageFormatException ex3 = null;
			ParamInfo<TypeSymbol>[] array2 = ((setMethod == null) ? null : metadataDecoder.GetSignatureForMethod(setMethod.Handle, out signatureHeader, out ex3, true));
			bool flag;
			this._parameters = ((array2 == null) ? PEPropertySymbol.GetParameters(moduleSymbol, this, getMethod, propertyParams, array, out flag) : PEPropertySymbol.GetParameters(moduleSymbol, this, setMethod, propertyParams, array2, out flag));
			if (ex2 != null || ex3 != null || ex != null || flag)
			{
				this._lazyCachedUseSiteInfo.Initialize(new CSDiagnosticInfo(ErrorCode.ERR_BindToBogus, new object[] { this }));
			}
			ParamInfo<TypeSymbol> paramInfo = propertyParams[0];
			ImmutableArray<CustomModifier> immutableArray = CSharpCustomModifier.Convert(paramInfo.CustomModifiers);
			if (paramInfo.IsByRef)
			{
				if (moduleSymbol.Module.HasIsReadOnlyAttribute(handle))
				{
					this._refKind = RefKind.In;
				}
				else
				{
					this._refKind = RefKind.Ref;
				}
			}
			else
			{
				this._refKind = RefKind.None;
			}
			TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(NativeIntegerTypeDecoder.TransformType(DynamicTypeDecoder.TransformType(paramInfo.Type, immutableArray.Length, handle, moduleSymbol, this._refKind), handle, moduleSymbol).AsDynamicIfNoPia(this._containingType), NullableAnnotation.Oblivious, immutableArray);
			typeWithAnnotations = NullableTypeDecoder.TransformType(typeWithAnnotations, handle, moduleSymbol, this._containingType, this._containingType);
			typeWithAnnotations = TupleTypeDecoder.DecodeTupleTypesIfApplicable(typeWithAnnotations, handle, moduleSymbol);
			this._propertyTypeWithAnnotations = typeWithAnnotations;
			bool flag2 = !PEPropertySymbol.DoSignaturesMatch(module, metadataDecoder, propertyParams, this._getMethod, array, this._setMethod, array2) || this.MustCallMethodsDirectlyCore() || PEPropertySymbol.<.ctor>g__anyUnexpectedRequiredModifiers|17_0(propertyParams);
			if (!flag2)
			{
				if (this._getMethod != null)
				{
					this._getMethod.SetAssociatedProperty(this, MethodKind.PropertyGet);
				}
				if (this._setMethod != null)
				{
					this._setMethod.SetAssociatedProperty(this, MethodKind.PropertySet);
				}
			}
			if (flag2)
			{
				this._flags |= PEPropertySymbol.Flags.CallMethodsDirectly;
			}
			if ((propertyAttributes & 512) != null)
			{
				this._flags |= PEPropertySymbol.Flags.IsSpecialName;
			}
			if ((propertyAttributes & 1024) != null)
			{
				this._flags |= PEPropertySymbol.Flags.IsRuntimeSpecialName;
			}
		}

		// Token: 0x06007F8E RID: 32654 RVA: 0x001CB4C4 File Offset: 0x001C96C4
		private bool MustCallMethodsDirectlyCore()
		{
			if (this.RefKind != RefKind.None && this._setMethod != null)
			{
				return true;
			}
			if (base.ParameterCount == 0)
			{
				return false;
			}
			if (this.IsIndexedProperty)
			{
				return this.IsStatic;
			}
			return !this.IsIndexer || this.HasRefOrOutParameter();
		}

		// Token: 0x17002617 RID: 9751
		// (get) Token: 0x06007F8F RID: 32655 RVA: 0x001CB512 File Offset: 0x001C9712
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17002618 RID: 9752
		// (get) Token: 0x06007F90 RID: 32656 RVA: 0x001CB51A File Offset: 0x001C971A
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17002619 RID: 9753
		// (get) Token: 0x06007F91 RID: 32657 RVA: 0x001CB522 File Offset: 0x001C9722
		public override string Name
		{
			get
			{
				if (!this.IsIndexer)
				{
					return this._name;
				}
				return "this[]";
			}
		}

		// Token: 0x1700261A RID: 9754
		// (get) Token: 0x06007F92 RID: 32658 RVA: 0x001CB538 File Offset: 0x001C9738
		internal override bool HasSpecialName
		{
			get
			{
				return (this._flags & PEPropertySymbol.Flags.IsSpecialName) > (PEPropertySymbol.Flags)0;
			}
		}

		// Token: 0x1700261B RID: 9755
		// (get) Token: 0x06007F93 RID: 32659 RVA: 0x001CB545 File Offset: 0x001C9745
		public override string MetadataName
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700261C RID: 9756
		// (get) Token: 0x06007F94 RID: 32660 RVA: 0x001CB54D File Offset: 0x001C974D
		internal PropertyDefinitionHandle Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x1700261D RID: 9757
		// (get) Token: 0x06007F95 RID: 32661 RVA: 0x001CB558 File Offset: 0x001C9758
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				if (this._declaredAccessibility == -1)
				{
					Accessibility accessibility3;
					if (this.IsOverride)
					{
						bool flag = false;
						Accessibility accessibility = Accessibility.NotApplicable;
						Accessibility accessibility2 = Accessibility.NotApplicable;
						PropertySymbol propertySymbol = this;
						for (;;)
						{
							if (accessibility == Accessibility.NotApplicable)
							{
								MethodSymbol getMethod = propertySymbol.GetMethod;
								if (getMethod != null)
								{
									Accessibility declaredAccessibility = getMethod.DeclaredAccessibility;
									accessibility = ((declaredAccessibility == Accessibility.ProtectedOrInternal && flag) ? Accessibility.Protected : declaredAccessibility);
								}
							}
							if (accessibility2 == Accessibility.NotApplicable)
							{
								MethodSymbol setMethod = propertySymbol.SetMethod;
								if (setMethod != null)
								{
									Accessibility declaredAccessibility2 = setMethod.DeclaredAccessibility;
									accessibility2 = ((declaredAccessibility2 == Accessibility.ProtectedOrInternal && flag) ? Accessibility.Protected : declaredAccessibility2);
								}
							}
							if (accessibility != Accessibility.NotApplicable && accessibility2 != Accessibility.NotApplicable)
							{
								break;
							}
							PropertySymbol overriddenProperty = propertySymbol.OverriddenProperty;
							if (overriddenProperty == null)
							{
								break;
							}
							if (!flag && !propertySymbol.ContainingAssembly.HasInternalAccessTo(overriddenProperty.ContainingAssembly))
							{
								flag = true;
							}
							propertySymbol = overriddenProperty;
						}
						accessibility3 = PEPropertyOrEventHelpers.GetDeclaredAccessibilityFromAccessors(accessibility, accessibility2);
					}
					else
					{
						accessibility3 = PEPropertyOrEventHelpers.GetDeclaredAccessibilityFromAccessors(this.GetMethod, this.SetMethod);
					}
					Interlocked.CompareExchange(ref this._declaredAccessibility, (int)accessibility3, -1);
				}
				return (Accessibility)this._declaredAccessibility;
			}
		}

		// Token: 0x1700261E RID: 9758
		// (get) Token: 0x06007F96 RID: 32662 RVA: 0x001CB63B File Offset: 0x001C983B
		public override bool IsExtern
		{
			get
			{
				return (this._getMethod != null && this._getMethod.IsExtern) || (this._setMethod != null && this._setMethod.IsExtern);
			}
		}

		// Token: 0x1700261F RID: 9759
		// (get) Token: 0x06007F97 RID: 32663 RVA: 0x001CB669 File Offset: 0x001C9869
		public override bool IsAbstract
		{
			get
			{
				return (this._getMethod != null && this._getMethod.IsAbstract) || (this._setMethod != null && this._setMethod.IsAbstract);
			}
		}

		// Token: 0x17002620 RID: 9760
		// (get) Token: 0x06007F98 RID: 32664 RVA: 0x001CB697 File Offset: 0x001C9897
		public override bool IsSealed
		{
			get
			{
				return (this._getMethod == null || this._getMethod.IsSealed) && (this._setMethod == null || this._setMethod.IsSealed);
			}
		}

		// Token: 0x17002621 RID: 9761
		// (get) Token: 0x06007F99 RID: 32665 RVA: 0x001CB6C5 File Offset: 0x001C98C5
		public override bool IsVirtual
		{
			get
			{
				return !this.IsOverride && !this.IsAbstract && ((this._getMethod != null && this._getMethod.IsVirtual) || (this._setMethod != null && this._setMethod.IsVirtual));
			}
		}

		// Token: 0x17002622 RID: 9762
		// (get) Token: 0x06007F9A RID: 32666 RVA: 0x001CB705 File Offset: 0x001C9905
		public override bool IsOverride
		{
			get
			{
				return (this._getMethod != null && this._getMethod.IsOverride) || (this._setMethod != null && this._setMethod.IsOverride);
			}
		}

		// Token: 0x17002623 RID: 9763
		// (get) Token: 0x06007F9B RID: 32667 RVA: 0x001CB733 File Offset: 0x001C9933
		public override bool IsStatic
		{
			get
			{
				return (this._getMethod == null || this._getMethod.IsStatic) && (this._setMethod == null || this._setMethod.IsStatic);
			}
		}

		// Token: 0x17002624 RID: 9764
		// (get) Token: 0x06007F9C RID: 32668 RVA: 0x001CB761 File Offset: 0x001C9961
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17002625 RID: 9765
		// (get) Token: 0x06007F9D RID: 32669 RVA: 0x001CB76C File Offset: 0x001C996C
		public override bool IsIndexer
		{
			get
			{
				if (base.ParameterCount > 0)
				{
					string defaultMemberName = this._containingType.DefaultMemberName;
					return this._name == defaultMemberName || (this.GetMethod != null && this.GetMethod.Name == defaultMemberName) || (this.SetMethod != null && this.SetMethod.Name == defaultMemberName);
				}
				return false;
			}
		}

		// Token: 0x17002626 RID: 9766
		// (get) Token: 0x06007F9E RID: 32670 RVA: 0x001CB7D6 File Offset: 0x001C99D6
		public override bool IsIndexedProperty
		{
			get
			{
				return base.ParameterCount > 0 && this._containingType.IsComImport;
			}
		}

		// Token: 0x17002627 RID: 9767
		// (get) Token: 0x06007F9F RID: 32671 RVA: 0x001CB7EE File Offset: 0x001C99EE
		public override RefKind RefKind
		{
			get
			{
				return this._refKind;
			}
		}

		// Token: 0x17002628 RID: 9768
		// (get) Token: 0x06007FA0 RID: 32672 RVA: 0x001CB7F6 File Offset: 0x001C99F6
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this._propertyTypeWithAnnotations;
			}
		}

		// Token: 0x17002629 RID: 9769
		// (get) Token: 0x06007FA1 RID: 32673 RVA: 0x001CB7FE File Offset: 0x001C99FE
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x1700262A RID: 9770
		// (get) Token: 0x06007FA2 RID: 32674 RVA: 0x001CB805 File Offset: 0x001C9A05
		public override MethodSymbol GetMethod
		{
			get
			{
				return this._getMethod;
			}
		}

		// Token: 0x1700262B RID: 9771
		// (get) Token: 0x06007FA3 RID: 32675 RVA: 0x001CB80D File Offset: 0x001C9A0D
		public override MethodSymbol SetMethod
		{
			get
			{
				return this._setMethod;
			}
		}

		// Token: 0x1700262C RID: 9772
		// (get) Token: 0x06007FA4 RID: 32676 RVA: 0x001CB818 File Offset: 0x001C9A18
		internal override CallingConvention CallingConvention
		{
			get
			{
				return (CallingConvention)new MetadataDecoder(this._containingType.ContainingPEModule, this._containingType).GetSignatureHeaderForProperty(this._handle).RawValue;
			}
		}

		// Token: 0x1700262D RID: 9773
		// (get) Token: 0x06007FA5 RID: 32677 RVA: 0x001CB84E File Offset: 0x001C9A4E
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._containingType.ContainingPEModule.MetadataLocation.Cast<MetadataLocation, Location>();
			}
		}

		// Token: 0x1700262E RID: 9774
		// (get) Token: 0x06007FA6 RID: 32678 RVA: 0x001CB865 File Offset: 0x001C9A65
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x06007FA7 RID: 32679 RVA: 0x001CB86C File Offset: 0x001C9A6C
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			if (this._lazyCustomAttributes.IsDefault)
			{
				CustomAttributeHandle customAttributeHandle;
				ImmutableArray<CSharpAttributeData> customAttributesForToken = ((PEModuleSymbol)this.ContainingModule).GetCustomAttributesForToken(this._handle, out customAttributeHandle, (this.RefKind == RefKind.In) ? AttributeDescription.IsReadOnlyAttribute : default(AttributeDescription));
				ImmutableInterlocked.InterlockedInitialize<CSharpAttributeData>(ref this._lazyCustomAttributes, customAttributesForToken);
			}
			return this._lazyCustomAttributes;
		}

		// Token: 0x06007FA8 RID: 32680 RVA: 0x001CB8D0 File Offset: 0x001C9AD0
		internal override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			return this.GetAttributes();
		}

		// Token: 0x1700262F RID: 9775
		// (get) Token: 0x06007FA9 RID: 32681 RVA: 0x001CB8E0 File Offset: 0x001C9AE0
		public override ImmutableArray<PropertySymbol> ExplicitInterfaceImplementations
		{
			get
			{
				if ((this._getMethod == null || this._getMethod.ExplicitInterfaceImplementations.Length == 0) && (this._setMethod == null || this._setMethod.ExplicitInterfaceImplementations.Length == 0))
				{
					return ImmutableArray<PropertySymbol>.Empty;
				}
				IEnumerable<PropertySymbol> propertiesForExplicitlyImplementedAccessor = PEPropertyOrEventHelpers.GetPropertiesForExplicitlyImplementedAccessor(this._getMethod);
				ISet<PropertySymbol> propertiesForExplicitlyImplementedAccessor2 = PEPropertyOrEventHelpers.GetPropertiesForExplicitlyImplementedAccessor(this._setMethod);
				ArrayBuilder<PropertySymbol> instance = ArrayBuilder<PropertySymbol>.GetInstance();
				foreach (PropertySymbol propertySymbol in propertiesForExplicitlyImplementedAccessor)
				{
					if (!propertySymbol.SetMethod.IsImplementable() || propertiesForExplicitlyImplementedAccessor2.Contains(propertySymbol))
					{
						instance.Add(propertySymbol);
					}
				}
				foreach (PropertySymbol propertySymbol2 in propertiesForExplicitlyImplementedAccessor2)
				{
					if (!propertySymbol2.GetMethod.IsImplementable())
					{
						instance.Add(propertySymbol2);
					}
				}
				return instance.ToImmutableAndFree();
			}
		}

		// Token: 0x17002630 RID: 9776
		// (get) Token: 0x06007FAA RID: 32682 RVA: 0x001CB9EC File Offset: 0x001C9BEC
		internal override bool MustCallMethodsDirectly
		{
			get
			{
				return (this._flags & PEPropertySymbol.Flags.CallMethodsDirectly) > (PEPropertySymbol.Flags)0;
			}
		}

		// Token: 0x06007FAB RID: 32683 RVA: 0x001CB9FC File Offset: 0x001C9BFC
		private static bool DoSignaturesMatch(PEModule module, MetadataDecoder metadataDecoder, ParamInfo<TypeSymbol>[] propertyParams, PEMethodSymbol getMethod, ParamInfo<TypeSymbol>[] getMethodParams, PEMethodSymbol setMethod, ParamInfo<TypeSymbol>[] setMethodParams)
		{
			bool flag = getMethodParams != null;
			bool flag2 = setMethodParams != null;
			if (flag && !metadataDecoder.DoPropertySignaturesMatch(propertyParams, getMethodParams, false, true, true))
			{
				return false;
			}
			if (flag2 && !metadataDecoder.DoPropertySignaturesMatch(propertyParams, setMethodParams, true, true, true))
			{
				return false;
			}
			if (flag && flag2)
			{
				int num = propertyParams.Length - 1;
				ParameterHandle handle = getMethodParams[num].Handle;
				ParameterHandle handle2 = setMethodParams[num].Handle;
				bool flag3 = !handle.IsNil && module.HasParamsAttribute(handle);
				bool flag4 = !handle2.IsNil && module.HasParamsAttribute(handle2);
				if (flag3 != flag4)
				{
					return false;
				}
				if (getMethod.IsExtern != setMethod.IsExtern || getMethod.IsSealed != setMethod.IsSealed || getMethod.IsOverride != setMethod.IsOverride || getMethod.IsStatic != setMethod.IsStatic)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06007FAC RID: 32684 RVA: 0x001CBAE0 File Offset: 0x001C9CE0
		private static ImmutableArray<ParameterSymbol> GetParameters(PEModuleSymbol moduleSymbol, PEPropertySymbol property, PEMethodSymbol accessor, ParamInfo<TypeSymbol>[] propertyParams, ParamInfo<TypeSymbol>[] accessorParams, out bool anyParameterIsBad)
		{
			anyParameterIsBad = false;
			if (propertyParams.Length < 2)
			{
				return ImmutableArray<ParameterSymbol>.Empty;
			}
			int num = accessorParams.Length;
			ParameterSymbol[] array = new ParameterSymbol[propertyParams.Length - 1];
			for (int i = 1; i < propertyParams.Length; i++)
			{
				ParamInfo<TypeSymbol> paramInfo = propertyParams[i];
				ParameterHandle parameterHandle;
				Symbol symbol;
				if (i < num)
				{
					parameterHandle = accessorParams[i].Handle;
					symbol = accessor;
				}
				else
				{
					parameterHandle = paramInfo.Handle;
					symbol = property;
				}
				int num2 = i - 1;
				bool flag;
				array[num2] = PEParameterSymbol.Create(moduleSymbol, property, accessor.IsMetadataVirtual(false), num2, parameterHandle, paramInfo, symbol, out flag);
				if (flag)
				{
					anyParameterIsBad = true;
				}
			}
			return array.AsImmutableOrNull<ParameterSymbol>();
		}

		// Token: 0x06007FAD RID: 32685 RVA: 0x001CBB74 File Offset: 0x001C9D74
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return PEDocumentationCommentUtils.GetDocumentationComment(this, this._containingType.ContainingPEModule, preferredCulture, cancellationToken, ref this._lazyDocComment);
		}

		// Token: 0x06007FAE RID: 32686 RVA: 0x001CBB90 File Offset: 0x001C9D90
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			AssemblySymbol primaryDependency = base.PrimaryDependency;
			if (!this._lazyCachedUseSiteInfo.IsInitialized)
			{
				UseSiteInfo<AssemblySymbol> useSiteInfo = new UseSiteInfo<AssemblySymbol>(primaryDependency);
				base.CalculateUseSiteDiagnostic(ref useSiteInfo);
				this._lazyCachedUseSiteInfo.Initialize(primaryDependency, useSiteInfo);
			}
			return this._lazyCachedUseSiteInfo.ToUseSiteInfo(primaryDependency);
		}

		// Token: 0x17002631 RID: 9777
		// (get) Token: 0x06007FAF RID: 32687 RVA: 0x001CBBDB File Offset: 0x001C9DDB
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				ObsoleteAttributeHelpers.InitializeObsoleteDataFromMetadata(ref this._lazyObsoleteAttributeData, this._handle, (PEModuleSymbol)this.ContainingModule, false);
				return this._lazyObsoleteAttributeData;
			}
		}

		// Token: 0x17002632 RID: 9778
		// (get) Token: 0x06007FB0 RID: 32688 RVA: 0x001CBC05 File Offset: 0x001C9E05
		internal override bool HasRuntimeSpecialName
		{
			get
			{
				return (this._flags & PEPropertySymbol.Flags.IsRuntimeSpecialName) > (PEPropertySymbol.Flags)0;
			}
		}

		// Token: 0x17002633 RID: 9779
		// (get) Token: 0x06007FB1 RID: 32689 RVA: 0x001CBC12 File Offset: 0x001C9E12
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007FB2 RID: 32690 RVA: 0x001CBC15 File Offset: 0x001C9E15
		[CompilerGenerated]
		internal static bool <.ctor>g__anyUnexpectedRequiredModifiers|17_0(ParamInfo<TypeSymbol>[] propertyParams)
		{
			return Enumerable.Any<ParamInfo<TypeSymbol>>(propertyParams, delegate(ParamInfo<TypeSymbol> p)
			{
				if (!p.RefCustomModifiers.IsDefaultOrEmpty)
				{
					if (p.RefCustomModifiers.Any<ModifierInfo<TypeSymbol>>((ModifierInfo<TypeSymbol> m) => !m.IsOptional && !m.Modifier.IsWellKnownTypeInAttribute()))
					{
						return true;
					}
				}
				return p.CustomModifiers.AnyRequired<TypeSymbol>();
			});
		}

		// Token: 0x04001E0F RID: 7695
		private readonly string _name;

		// Token: 0x04001E10 RID: 7696
		private readonly PENamedTypeSymbol _containingType;

		// Token: 0x04001E11 RID: 7697
		private readonly PropertyDefinitionHandle _handle;

		// Token: 0x04001E12 RID: 7698
		private readonly ImmutableArray<ParameterSymbol> _parameters;

		// Token: 0x04001E13 RID: 7699
		private readonly RefKind _refKind;

		// Token: 0x04001E14 RID: 7700
		private readonly TypeWithAnnotations _propertyTypeWithAnnotations;

		// Token: 0x04001E15 RID: 7701
		private readonly PEMethodSymbol _getMethod;

		// Token: 0x04001E16 RID: 7702
		private readonly PEMethodSymbol _setMethod;

		// Token: 0x04001E17 RID: 7703
		private ImmutableArray<CSharpAttributeData> _lazyCustomAttributes;

		// Token: 0x04001E18 RID: 7704
		private Tuple<CultureInfo, string> _lazyDocComment;

		// Token: 0x04001E19 RID: 7705
		private CachedUseSiteInfo<AssemblySymbol> _lazyCachedUseSiteInfo = CachedUseSiteInfo<AssemblySymbol>.Uninitialized;

		// Token: 0x04001E1A RID: 7706
		private ObsoleteAttributeData _lazyObsoleteAttributeData = ObsoleteAttributeData.Uninitialized;

		// Token: 0x04001E1B RID: 7707
		private const int UnsetAccessibility = -1;

		// Token: 0x04001E1C RID: 7708
		private int _declaredAccessibility = -1;

		// Token: 0x04001E1D RID: 7709
		private readonly PEPropertySymbol.Flags _flags;

		// Token: 0x02000991 RID: 2449
		[Flags]
		private enum Flags : byte
		{
			// Token: 0x04002964 RID: 10596
			IsSpecialName = 1,
			// Token: 0x04002965 RID: 10597
			IsRuntimeSpecialName = 2,
			// Token: 0x04002966 RID: 10598
			CallMethodsDirectly = 4
		}

		// Token: 0x02000992 RID: 2450
		private sealed class PEPropertySymbolWithCustomModifiers : PEPropertySymbol
		{
			// Token: 0x06009293 RID: 37523 RVA: 0x00200C00 File Offset: 0x001FEE00
			public PEPropertySymbolWithCustomModifiers(PEModuleSymbol moduleSymbol, PENamedTypeSymbol containingType, PropertyDefinitionHandle handle, PEMethodSymbol getMethod, PEMethodSymbol setMethod, ParamInfo<TypeSymbol>[] propertyParams, MetadataDecoder metadataDecoder)
				: base(moduleSymbol, containingType, handle, getMethod, setMethod, propertyParams, metadataDecoder)
			{
				ParamInfo<TypeSymbol> paramInfo = propertyParams[0];
				this._refCustomModifiers = CSharpCustomModifier.Convert(paramInfo.RefCustomModifiers);
			}

			// Token: 0x1700292E RID: 10542
			// (get) Token: 0x06009294 RID: 37524 RVA: 0x00200C38 File Offset: 0x001FEE38
			public override ImmutableArray<CustomModifier> RefCustomModifiers
			{
				get
				{
					return this._refCustomModifiers;
				}
			}

			// Token: 0x04002967 RID: 10599
			private readonly ImmutableArray<CustomModifier> _refCustomModifiers;
		}
	}
}
