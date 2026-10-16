using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.DocumentationComments;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005E7 RID: 1511
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PEEventSymbol : EventSymbol
	{
		// Token: 0x06007DF4 RID: 32244 RVA: 0x001C5034 File Offset: 0x001C3234
		internal PEEventSymbol(PEModuleSymbol moduleSymbol, PENamedTypeSymbol containingType, EventDefinitionHandle handle, PEMethodSymbol addMethod, PEMethodSymbol removeMethod, MultiDictionary<string, PEFieldSymbol> privateFieldNameToSymbols)
		{
			this._addMethod = addMethod;
			this._removeMethod = removeMethod;
			this._handle = handle;
			this._containingType = containingType;
			EventAttributes eventAttributes = 0;
			EntityHandle entityHandle = default(EntityHandle);
			try
			{
				moduleSymbol.Module.GetEventDefPropsOrThrow(handle, out this._name, out eventAttributes, out entityHandle);
			}
			catch (BadImageFormatException ex)
			{
				this._name = this._name ?? string.Empty;
				this._lazyCachedUseSiteInfo.Initialize(new CSDiagnosticInfo(ErrorCode.ERR_BindToBogus, new object[] { this }));
				if (entityHandle.IsNil)
				{
					this._eventTypeWithAnnotations = TypeWithAnnotations.Create(new UnsupportedMetadataTypeSymbol(ex), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				}
			}
			TypeSymbol typeSymbol = this._eventTypeWithAnnotations.Type;
			if (!this._eventTypeWithAnnotations.HasType)
			{
				typeSymbol = new MetadataDecoder(moduleSymbol, containingType).GetTypeOfToken(entityHandle);
				TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(NativeIntegerTypeDecoder.TransformType(DynamicTypeDecoder.TransformType(typeSymbol, 0, handle, moduleSymbol, RefKind.None), handle, moduleSymbol), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				typeWithAnnotations = NullableTypeDecoder.TransformType(typeWithAnnotations, handle, moduleSymbol, this._containingType, this._containingType);
				typeWithAnnotations = TupleTypeDecoder.DecodeTupleTypesIfApplicable(typeWithAnnotations, handle, moduleSymbol);
				this._eventTypeWithAnnotations = typeWithAnnotations;
			}
			bool isWindowsRuntimeEvent = this.IsWindowsRuntimeEvent;
			if (isWindowsRuntimeEvent ? (!PEEventSymbol.DoModifiersMatch(this._addMethod, this._removeMethod)) : (!PEEventSymbol.DoSignaturesMatch(moduleSymbol, typeSymbol, this._addMethod, this._removeMethod)))
			{
				this._flags |= PEEventSymbol.Flags.CallMethodsDirectly;
			}
			else
			{
				this._addMethod.SetAssociatedEvent(this, MethodKind.EventAdd);
				this._removeMethod.SetAssociatedEvent(this, MethodKind.EventRemove);
				PEFieldSymbol associatedField = this.GetAssociatedField(privateFieldNameToSymbols, isWindowsRuntimeEvent);
				if (associatedField != null)
				{
					this._associatedFieldOpt = associatedField;
					associatedField.SetAssociatedEvent(this);
				}
			}
			if ((eventAttributes & 512) != null)
			{
				this._flags |= PEEventSymbol.Flags.IsSpecialName;
			}
			if ((eventAttributes & 1024) != null)
			{
				this._flags |= PEEventSymbol.Flags.IsRuntimeSpecialName;
			}
		}

		// Token: 0x06007DF5 RID: 32245 RVA: 0x001C524C File Offset: 0x001C344C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private PEFieldSymbol GetAssociatedField(MultiDictionary<string, PEFieldSymbol> privateFieldNameToSymbols, bool isWindowsRuntimeEvent)
		{
			foreach (PEFieldSymbol pefieldSymbol in privateFieldNameToSymbols[this._name])
			{
				TypeSymbol type = pefieldSymbol.Type;
				if (isWindowsRuntimeEvent)
				{
					if (TypeSymbol.Equals(((PEModuleSymbol)this.ContainingModule).EventRegistrationTokenTable_T, type.OriginalDefinition, TypeCompareKind.ConsiderEverything) && TypeSymbol.Equals(this._eventTypeWithAnnotations.Type, ((NamedTypeSymbol)type).TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[0].Type, TypeCompareKind.ConsiderEverything))
					{
						return pefieldSymbol;
					}
				}
				else if (TypeSymbol.Equals(type, this._eventTypeWithAnnotations.Type, TypeCompareKind.ConsiderEverything))
				{
					return pefieldSymbol;
				}
			}
			return null;
		}

		// Token: 0x1700252B RID: 9515
		// (get) Token: 0x06007DF6 RID: 32246 RVA: 0x001C5320 File Offset: 0x001C3520
		public override bool IsWindowsRuntimeEvent
		{
			get
			{
				NamedTypeSymbol eventRegistrationToken = ((PEModuleSymbol)this.ContainingModule).EventRegistrationToken;
				return TypeSymbol.Equals(this._addMethod.ReturnType, eventRegistrationToken, TypeCompareKind.ConsiderEverything) && this._addMethod.ParameterCount == 1 && this._removeMethod.ParameterCount == 1 && TypeSymbol.Equals(this._removeMethod.Parameters[0].Type, eventRegistrationToken, TypeCompareKind.ConsiderEverything);
			}
		}

		// Token: 0x1700252C RID: 9516
		// (get) Token: 0x06007DF7 RID: 32247 RVA: 0x001C5390 File Offset: 0x001C3590
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override FieldSymbol AssociatedField
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._associatedFieldOpt;
			}
		}

		// Token: 0x1700252D RID: 9517
		// (get) Token: 0x06007DF8 RID: 32248 RVA: 0x001C5398 File Offset: 0x001C3598
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x1700252E RID: 9518
		// (get) Token: 0x06007DF9 RID: 32249 RVA: 0x001C53A0 File Offset: 0x001C35A0
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x1700252F RID: 9519
		// (get) Token: 0x06007DFA RID: 32250 RVA: 0x001C53A8 File Offset: 0x001C35A8
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17002530 RID: 9520
		// (get) Token: 0x06007DFB RID: 32251 RVA: 0x001C53B0 File Offset: 0x001C35B0
		internal override bool HasSpecialName
		{
			get
			{
				return (this._flags & PEEventSymbol.Flags.IsSpecialName) > (PEEventSymbol.Flags)0;
			}
		}

		// Token: 0x17002531 RID: 9521
		// (get) Token: 0x06007DFC RID: 32252 RVA: 0x001C53BD File Offset: 0x001C35BD
		internal override bool HasRuntimeSpecialName
		{
			get
			{
				return (this._flags & PEEventSymbol.Flags.IsRuntimeSpecialName) > (PEEventSymbol.Flags)0;
			}
		}

		// Token: 0x17002532 RID: 9522
		// (get) Token: 0x06007DFD RID: 32253 RVA: 0x001C53CA File Offset: 0x001C35CA
		internal EventDefinitionHandle Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x17002533 RID: 9523
		// (get) Token: 0x06007DFE RID: 32254 RVA: 0x001C53D4 File Offset: 0x001C35D4
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				if (this._lazyDeclaredAccessibility == -1)
				{
					Accessibility declaredAccessibilityFromAccessors = PEPropertyOrEventHelpers.GetDeclaredAccessibilityFromAccessors(this._addMethod, this._removeMethod);
					Interlocked.CompareExchange(ref this._lazyDeclaredAccessibility, (int)declaredAccessibilityFromAccessors, -1);
				}
				return (Accessibility)this._lazyDeclaredAccessibility;
			}
		}

		// Token: 0x17002534 RID: 9524
		// (get) Token: 0x06007DFF RID: 32255 RVA: 0x001C5410 File Offset: 0x001C3610
		public override bool IsExtern
		{
			get
			{
				return this._addMethod.IsExtern || this._removeMethod.IsExtern;
			}
		}

		// Token: 0x17002535 RID: 9525
		// (get) Token: 0x06007E00 RID: 32256 RVA: 0x001C542C File Offset: 0x001C362C
		public override bool IsAbstract
		{
			get
			{
				return this._addMethod.IsAbstract || this._removeMethod.IsAbstract;
			}
		}

		// Token: 0x17002536 RID: 9526
		// (get) Token: 0x06007E01 RID: 32257 RVA: 0x001C5448 File Offset: 0x001C3648
		public override bool IsSealed
		{
			get
			{
				return this._addMethod.IsSealed || this._removeMethod.IsSealed;
			}
		}

		// Token: 0x17002537 RID: 9527
		// (get) Token: 0x06007E02 RID: 32258 RVA: 0x001C5464 File Offset: 0x001C3664
		public override bool IsVirtual
		{
			get
			{
				return !this.IsOverride && !this.IsAbstract && (this._addMethod.IsVirtual || this._removeMethod.IsVirtual);
			}
		}

		// Token: 0x17002538 RID: 9528
		// (get) Token: 0x06007E03 RID: 32259 RVA: 0x001C5492 File Offset: 0x001C3692
		public override bool IsOverride
		{
			get
			{
				return this._addMethod.IsOverride || this._removeMethod.IsOverride;
			}
		}

		// Token: 0x17002539 RID: 9529
		// (get) Token: 0x06007E04 RID: 32260 RVA: 0x001C54AE File Offset: 0x001C36AE
		public override bool IsStatic
		{
			get
			{
				return this._addMethod.IsStatic && this._removeMethod.IsStatic;
			}
		}

		// Token: 0x1700253A RID: 9530
		// (get) Token: 0x06007E05 RID: 32261 RVA: 0x001C54CA File Offset: 0x001C36CA
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this._eventTypeWithAnnotations;
			}
		}

		// Token: 0x1700253B RID: 9531
		// (get) Token: 0x06007E06 RID: 32262 RVA: 0x001C54D2 File Offset: 0x001C36D2
		public override MethodSymbol AddMethod
		{
			get
			{
				return this._addMethod;
			}
		}

		// Token: 0x1700253C RID: 9532
		// (get) Token: 0x06007E07 RID: 32263 RVA: 0x001C54DA File Offset: 0x001C36DA
		public override MethodSymbol RemoveMethod
		{
			get
			{
				return this._removeMethod;
			}
		}

		// Token: 0x1700253D RID: 9533
		// (get) Token: 0x06007E08 RID: 32264 RVA: 0x001C54E2 File Offset: 0x001C36E2
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._containingType.ContainingPEModule.MetadataLocation.Cast<MetadataLocation, Location>();
			}
		}

		// Token: 0x1700253E RID: 9534
		// (get) Token: 0x06007E09 RID: 32265 RVA: 0x001C54F9 File Offset: 0x001C36F9
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x06007E0A RID: 32266 RVA: 0x001C5500 File Offset: 0x001C3700
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			if (this._lazyCustomAttributes.IsDefault)
			{
				((PEModuleSymbol)this.ContainingModule).LoadCustomAttributes(this._handle, ref this._lazyCustomAttributes);
			}
			return this._lazyCustomAttributes;
		}

		// Token: 0x06007E0B RID: 32267 RVA: 0x001C5536 File Offset: 0x001C3736
		internal override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			return this.GetAttributes();
		}

		// Token: 0x1700253F RID: 9535
		// (get) Token: 0x06007E0C RID: 32268 RVA: 0x001C5544 File Offset: 0x001C3744
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<EventSymbol> ExplicitInterfaceImplementations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				if (this._addMethod.ExplicitInterfaceImplementations.Length == 0 && this._removeMethod.ExplicitInterfaceImplementations.Length == 0)
				{
					return ImmutableArray<EventSymbol>.Empty;
				}
				ISet<EventSymbol> eventsForExplicitlyImplementedAccessor = PEPropertyOrEventHelpers.GetEventsForExplicitlyImplementedAccessor(this._addMethod);
				eventsForExplicitlyImplementedAccessor.IntersectWith(PEPropertyOrEventHelpers.GetEventsForExplicitlyImplementedAccessor(this._removeMethod));
				ArrayBuilder<EventSymbol> instance = ArrayBuilder<EventSymbol>.GetInstance();
				foreach (EventSymbol eventSymbol in eventsForExplicitlyImplementedAccessor)
				{
					instance.Add(eventSymbol);
				}
				return instance.ToImmutableAndFree();
			}
		}

		// Token: 0x17002540 RID: 9536
		// (get) Token: 0x06007E0D RID: 32269 RVA: 0x001C55E4 File Offset: 0x001C37E4
		internal override bool MustCallMethodsDirectly
		{
			get
			{
				return (this._flags & PEEventSymbol.Flags.CallMethodsDirectly) > (PEEventSymbol.Flags)0;
			}
		}

		// Token: 0x06007E0E RID: 32270 RVA: 0x001C55F1 File Offset: 0x001C37F1
		private static bool DoSignaturesMatch(PEModuleSymbol moduleSymbol, TypeSymbol eventType, PEMethodSymbol addMethod, PEMethodSymbol removeMethod)
		{
			return (eventType.IsDelegateType() || eventType.IsErrorType()) && PEEventSymbol.DoesSignatureMatch(moduleSymbol, eventType, addMethod) && PEEventSymbol.DoesSignatureMatch(moduleSymbol, eventType, removeMethod) && PEEventSymbol.DoModifiersMatch(addMethod, removeMethod);
		}

		// Token: 0x06007E0F RID: 32271 RVA: 0x001C5620 File Offset: 0x001C3820
		private static bool DoModifiersMatch(PEMethodSymbol addMethod, PEMethodSymbol removeMethod)
		{
			return addMethod.IsExtern == removeMethod.IsExtern && addMethod.IsStatic == removeMethod.IsStatic;
		}

		// Token: 0x06007E10 RID: 32272 RVA: 0x001C5640 File Offset: 0x001C3840
		private static bool DoesSignatureMatch(PEModuleSymbol moduleSymbol, TypeSymbol eventType, PEMethodSymbol method)
		{
			MetadataDecoder metadataDecoder = new MetadataDecoder(moduleSymbol, method);
			SignatureHeader signatureHeader;
			BadImageFormatException ex;
			ParamInfo<TypeSymbol>[] signatureForMethod = metadataDecoder.GetSignatureForMethod(method.Handle, out signatureHeader, out ex, false);
			return ex == null && metadataDecoder.DoesSignatureMatchEvent(eventType, signatureForMethod);
		}

		// Token: 0x06007E11 RID: 32273 RVA: 0x001C5674 File Offset: 0x001C3874
		public override string GetDocumentationCommentXml([global::System.Runtime.CompilerServices.Nullable(2)] CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return PEDocumentationCommentUtils.GetDocumentationComment(this, this._containingType.ContainingPEModule, preferredCulture, cancellationToken, ref this._lazyDocComment);
		}

		// Token: 0x06007E12 RID: 32274 RVA: 0x001C5690 File Offset: 0x001C3890
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
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

		// Token: 0x17002541 RID: 9537
		// (get) Token: 0x06007E13 RID: 32275 RVA: 0x001C56DB File Offset: 0x001C38DB
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				ObsoleteAttributeHelpers.InitializeObsoleteDataFromMetadata(ref this._lazyObsoleteAttributeData, this._handle, (PEModuleSymbol)this.ContainingModule, false);
				return this._lazyObsoleteAttributeData;
			}
		}

		// Token: 0x17002542 RID: 9538
		// (get) Token: 0x06007E14 RID: 32276 RVA: 0x001C5705 File Offset: 0x001C3905
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x04001DAD RID: 7597
		private readonly string _name;

		// Token: 0x04001DAE RID: 7598
		private readonly PENamedTypeSymbol _containingType;

		// Token: 0x04001DAF RID: 7599
		private readonly EventDefinitionHandle _handle;

		// Token: 0x04001DB0 RID: 7600
		private readonly TypeWithAnnotations _eventTypeWithAnnotations;

		// Token: 0x04001DB1 RID: 7601
		private readonly PEMethodSymbol _addMethod;

		// Token: 0x04001DB2 RID: 7602
		private readonly PEMethodSymbol _removeMethod;

		// Token: 0x04001DB3 RID: 7603
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly PEFieldSymbol _associatedFieldOpt;

		// Token: 0x04001DB4 RID: 7604
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<CSharpAttributeData> _lazyCustomAttributes;

		// Token: 0x04001DB5 RID: 7605
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })]
		private Tuple<CultureInfo, string> _lazyDocComment;

		// Token: 0x04001DB6 RID: 7606
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private CachedUseSiteInfo<AssemblySymbol> _lazyCachedUseSiteInfo = CachedUseSiteInfo<AssemblySymbol>.Uninitialized;

		// Token: 0x04001DB7 RID: 7607
		private ObsoleteAttributeData _lazyObsoleteAttributeData = ObsoleteAttributeData.Uninitialized;

		// Token: 0x04001DB8 RID: 7608
		private const int UnsetAccessibility = -1;

		// Token: 0x04001DB9 RID: 7609
		private int _lazyDeclaredAccessibility = -1;

		// Token: 0x04001DBA RID: 7610
		private readonly PEEventSymbol.Flags _flags;

		// Token: 0x02000979 RID: 2425
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[Flags]
		private enum Flags : byte
		{
			// Token: 0x040028C5 RID: 10437
			IsSpecialName = 1,
			// Token: 0x040028C6 RID: 10438
			IsRuntimeSpecialName = 2,
			// Token: 0x040028C7 RID: 10439
			CallMethodsDirectly = 4
		}
	}
}
