using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.DocumentationComments;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005E8 RID: 1512
	internal sealed class PEFieldSymbol : FieldSymbol
	{
		// Token: 0x06007E15 RID: 32277 RVA: 0x001C5708 File Offset: 0x001C3908
		internal PEFieldSymbol(PEModuleSymbol moduleSymbol, PENamedTypeSymbol containingType, FieldDefinitionHandle fieldDef)
		{
			this._handle = fieldDef;
			this._containingType = containingType;
			this._packedFlags = default(PEFieldSymbol.PackedFlags);
			try
			{
				moduleSymbol.Module.GetFieldDefPropsOrThrow(fieldDef, out this._name, out this._flags);
			}
			catch (BadImageFormatException)
			{
				if (this._name == null)
				{
					this._name = string.Empty;
				}
				this._lazyCachedUseSiteInfo.Initialize(new CSDiagnosticInfo(ErrorCode.ERR_BindToBogus, new object[] { this }));
			}
		}

		// Token: 0x17002543 RID: 9539
		// (get) Token: 0x06007E16 RID: 32278 RVA: 0x001C57B8 File Offset: 0x001C39B8
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17002544 RID: 9540
		// (get) Token: 0x06007E17 RID: 32279 RVA: 0x001C57C0 File Offset: 0x001C39C0
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17002545 RID: 9541
		// (get) Token: 0x06007E18 RID: 32280 RVA: 0x001C57C8 File Offset: 0x001C39C8
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17002546 RID: 9542
		// (get) Token: 0x06007E19 RID: 32281 RVA: 0x001C57D0 File Offset: 0x001C39D0
		internal FieldAttributes Flags
		{
			get
			{
				return this._flags;
			}
		}

		// Token: 0x17002547 RID: 9543
		// (get) Token: 0x06007E1A RID: 32282 RVA: 0x001C57D8 File Offset: 0x001C39D8
		internal override bool HasSpecialName
		{
			get
			{
				return (this._flags & 512) > 0;
			}
		}

		// Token: 0x17002548 RID: 9544
		// (get) Token: 0x06007E1B RID: 32283 RVA: 0x001C57E9 File Offset: 0x001C39E9
		internal override bool HasRuntimeSpecialName
		{
			get
			{
				return (this._flags & 1024) > 0;
			}
		}

		// Token: 0x17002549 RID: 9545
		// (get) Token: 0x06007E1C RID: 32284 RVA: 0x001C57FA File Offset: 0x001C39FA
		internal override bool IsNotSerialized
		{
			get
			{
				return (this._flags & 128) > 0;
			}
		}

		// Token: 0x1700254A RID: 9546
		// (get) Token: 0x06007E1D RID: 32285 RVA: 0x001C580B File Offset: 0x001C3A0B
		internal override MarshalPseudoCustomAttributeData MarshallingInformation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700254B RID: 9547
		// (get) Token: 0x06007E1E RID: 32286 RVA: 0x001C580E File Offset: 0x001C3A0E
		internal override bool IsMarshalledExplicitly
		{
			get
			{
				return (this._flags & 4096) > 0;
			}
		}

		// Token: 0x1700254C RID: 9548
		// (get) Token: 0x06007E1F RID: 32287 RVA: 0x001C581F File Offset: 0x001C3A1F
		internal override UnmanagedType MarshallingType
		{
			get
			{
				if ((this._flags & 4096) == null)
				{
					return 0;
				}
				return this._containingType.ContainingPEModule.Module.GetMarshallingType(this._handle);
			}
		}

		// Token: 0x1700254D RID: 9549
		// (get) Token: 0x06007E20 RID: 32288 RVA: 0x001C5854 File Offset: 0x001C3A54
		internal override ImmutableArray<byte> MarshallingDescriptor
		{
			get
			{
				if ((this._flags & 4096) == null)
				{
					return default(ImmutableArray<byte>);
				}
				return this._containingType.ContainingPEModule.Module.GetMarshallingDescriptor(this._handle);
			}
		}

		// Token: 0x1700254E RID: 9550
		// (get) Token: 0x06007E21 RID: 32289 RVA: 0x001C5899 File Offset: 0x001C3A99
		internal override int? TypeLayoutOffset
		{
			get
			{
				return this._containingType.ContainingPEModule.Module.GetFieldOffset(this._handle);
			}
		}

		// Token: 0x1700254F RID: 9551
		// (get) Token: 0x06007E22 RID: 32290 RVA: 0x001C58B6 File Offset: 0x001C3AB6
		internal FieldDefinitionHandle Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x06007E23 RID: 32291 RVA: 0x001C58BE File Offset: 0x001C3ABE
		internal void SetAssociatedEvent(PEEventSymbol eventSymbol)
		{
			if (this._associatedEventOpt == null)
			{
				this._associatedEventOpt = eventSymbol;
			}
		}

		// Token: 0x06007E24 RID: 32292 RVA: 0x001C58D0 File Offset: 0x001C3AD0
		private void EnsureSignatureIsLoaded()
		{
			if (this._lazyType == null)
			{
				PEModuleSymbol containingPEModule = this._containingType.ContainingPEModule;
				ImmutableArray<ModifierInfo<TypeSymbol>> immutableArray;
				TypeSymbol typeSymbol = new MetadataDecoder(containingPEModule, this._containingType).DecodeFieldSignature(this._handle, out immutableArray);
				ImmutableArray<CustomModifier> immutableArray2 = CSharpCustomModifier.Convert(immutableArray);
				TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(NativeIntegerTypeDecoder.TransformType(DynamicTypeDecoder.TransformType(typeSymbol, immutableArray2.Length, this._handle, containingPEModule, RefKind.None), this._handle, containingPEModule), NullableAnnotation.Oblivious, immutableArray2);
				typeWithAnnotations = NullableTypeDecoder.TransformType(typeWithAnnotations, this._handle, containingPEModule, this, this._containingType);
				typeWithAnnotations = TupleTypeDecoder.DecodeTupleTypesIfApplicable(typeWithAnnotations, this._handle, containingPEModule);
				this._lazyIsVolatile = immutableArray2.Any<CustomModifier>((CustomModifier m) => !m.IsOptional && m.Modifier.SpecialType == SpecialType.System_Runtime_CompilerServices_IsVolatile);
				int num;
				TypeSymbol typeSymbol2;
				if (immutableArray2.IsEmpty && this.IsFixedBuffer(out num, out typeSymbol2))
				{
					this._lazyFixedSize = num;
					this._lazyFixedImplementationType = typeWithAnnotations.Type as NamedTypeSymbol;
					typeWithAnnotations = TypeWithAnnotations.Create(new PointerTypeSymbol(TypeWithAnnotations.Create(typeSymbol2, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>))), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				}
				Interlocked.CompareExchange<TypeWithAnnotations.Boxed>(ref this._lazyType, new TypeWithAnnotations.Boxed(typeWithAnnotations), null);
			}
		}

		// Token: 0x06007E25 RID: 32293 RVA: 0x001C5A08 File Offset: 0x001C3C08
		private bool IsFixedBuffer(out int fixedSize, out TypeSymbol fixedElementType)
		{
			fixedSize = 0;
			fixedElementType = null;
			PEModuleSymbol containingPEModule = this.ContainingPEModule;
			string text;
			int num;
			if (containingPEModule.Module.HasFixedBufferAttribute(this._handle, out text, out num))
			{
				TypeSymbol typeSymbolForSerializedType = new MetadataDecoder(containingPEModule).GetTypeSymbolForSerializedType(text);
				if (typeSymbolForSerializedType.FixedBufferElementSizeInBytes() != 0)
				{
					fixedSize = num;
					fixedElementType = typeSymbolForSerializedType;
					return true;
				}
			}
			return false;
		}

		// Token: 0x17002550 RID: 9552
		// (get) Token: 0x06007E26 RID: 32294 RVA: 0x001C5A5C File Offset: 0x001C3C5C
		private PEModuleSymbol ContainingPEModule
		{
			get
			{
				return ((PENamespaceSymbol)this.ContainingNamespace).ContainingPEModule;
			}
		}

		// Token: 0x06007E27 RID: 32295 RVA: 0x001C5A6E File Offset: 0x001C3C6E
		internal override TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound)
		{
			this.EnsureSignatureIsLoaded();
			return this._lazyType.Value;
		}

		// Token: 0x17002551 RID: 9553
		// (get) Token: 0x06007E28 RID: 32296 RVA: 0x001C5A84 File Offset: 0x001C3C84
		public override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				FlowAnalysisAnnotations flowAnalysisAnnotations;
				if (!this._packedFlags.TryGetFlowAnalysisAnnotations(out flowAnalysisAnnotations))
				{
					flowAnalysisAnnotations = PEFieldSymbol.DecodeFlowAnalysisAttributes(this._containingType.ContainingPEModule.Module, this._handle);
					this._packedFlags.SetFlowAnalysisAnnotations(flowAnalysisAnnotations);
				}
				return flowAnalysisAnnotations;
			}
		}

		// Token: 0x06007E29 RID: 32297 RVA: 0x001C5ACC File Offset: 0x001C3CCC
		private static FlowAnalysisAnnotations DecodeFlowAnalysisAttributes(PEModule module, FieldDefinitionHandle handle)
		{
			FlowAnalysisAnnotations flowAnalysisAnnotations = FlowAnalysisAnnotations.None;
			if (module.HasAttribute(handle, AttributeDescription.AllowNullAttribute))
			{
				flowAnalysisAnnotations |= FlowAnalysisAnnotations.AllowNull;
			}
			if (module.HasAttribute(handle, AttributeDescription.DisallowNullAttribute))
			{
				flowAnalysisAnnotations |= FlowAnalysisAnnotations.DisallowNull;
			}
			if (module.HasAttribute(handle, AttributeDescription.MaybeNullAttribute))
			{
				flowAnalysisAnnotations |= FlowAnalysisAnnotations.MaybeNull;
			}
			if (module.HasAttribute(handle, AttributeDescription.NotNullAttribute))
			{
				flowAnalysisAnnotations |= FlowAnalysisAnnotations.NotNull;
			}
			return flowAnalysisAnnotations;
		}

		// Token: 0x17002552 RID: 9554
		// (get) Token: 0x06007E2A RID: 32298 RVA: 0x001C5B3A File Offset: 0x001C3D3A
		public override bool IsFixedSizeBuffer
		{
			get
			{
				this.EnsureSignatureIsLoaded();
				return this._lazyFixedImplementationType != null;
			}
		}

		// Token: 0x17002553 RID: 9555
		// (get) Token: 0x06007E2B RID: 32299 RVA: 0x001C5B4B File Offset: 0x001C3D4B
		public override int FixedSize
		{
			get
			{
				this.EnsureSignatureIsLoaded();
				return this._lazyFixedSize;
			}
		}

		// Token: 0x06007E2C RID: 32300 RVA: 0x001C5B59 File Offset: 0x001C3D59
		internal override NamedTypeSymbol FixedImplementationType(PEModuleBuilder emitModule)
		{
			this.EnsureSignatureIsLoaded();
			return this._lazyFixedImplementationType;
		}

		// Token: 0x17002554 RID: 9556
		// (get) Token: 0x06007E2D RID: 32301 RVA: 0x001C5B67 File Offset: 0x001C3D67
		public override Symbol AssociatedSymbol
		{
			get
			{
				return this._associatedEventOpt;
			}
		}

		// Token: 0x17002555 RID: 9557
		// (get) Token: 0x06007E2E RID: 32302 RVA: 0x001C5B6F File Offset: 0x001C3D6F
		public override bool IsReadOnly
		{
			get
			{
				return (this._flags & 32) > 0;
			}
		}

		// Token: 0x17002556 RID: 9558
		// (get) Token: 0x06007E2F RID: 32303 RVA: 0x001C5B7D File Offset: 0x001C3D7D
		public override bool IsVolatile
		{
			get
			{
				this.EnsureSignatureIsLoaded();
				return this._lazyIsVolatile;
			}
		}

		// Token: 0x17002557 RID: 9559
		// (get) Token: 0x06007E30 RID: 32304 RVA: 0x001C5B8B File Offset: 0x001C3D8B
		public override bool IsConst
		{
			get
			{
				return (this._flags & 64) != null || this.GetConstantValue(ConstantFieldsInProgress.Empty, false) != null;
			}
		}

		// Token: 0x06007E31 RID: 32305 RVA: 0x001C5BAC File Offset: 0x001C3DAC
		internal override ConstantValue GetConstantValue(ConstantFieldsInProgress inProgress, bool earlyDecodingWellKnownAttributes)
		{
			if (this._lazyConstantValue == Microsoft.CodeAnalysis.ConstantValue.Unset)
			{
				ConstantValue constantValue = null;
				if ((this._flags & 64) != null)
				{
					constantValue = this._containingType.ContainingPEModule.Module.GetConstantFieldValue(this._handle);
				}
				ConstantValue constantValue2;
				if (base.Type.SpecialType == SpecialType.System_Decimal && this._containingType.ContainingPEModule.Module.HasDecimalConstantAttribute(this.Handle, out constantValue2))
				{
					constantValue = constantValue2;
				}
				Interlocked.CompareExchange<ConstantValue>(ref this._lazyConstantValue, constantValue, Microsoft.CodeAnalysis.ConstantValue.Unset);
			}
			return this._lazyConstantValue;
		}

		// Token: 0x17002558 RID: 9560
		// (get) Token: 0x06007E32 RID: 32306 RVA: 0x001C5C41 File Offset: 0x001C3E41
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._containingType.ContainingPEModule.MetadataLocation.Cast<MetadataLocation, Location>();
			}
		}

		// Token: 0x17002559 RID: 9561
		// (get) Token: 0x06007E33 RID: 32307 RVA: 0x001C5C58 File Offset: 0x001C3E58
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x1700255A RID: 9562
		// (get) Token: 0x06007E34 RID: 32308 RVA: 0x001C5C60 File Offset: 0x001C3E60
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				Accessibility accessibility;
				switch (this._flags & 7)
				{
				case 0:
				case 1:
					accessibility = Accessibility.Private;
					break;
				case 2:
					accessibility = Accessibility.ProtectedAndInternal;
					break;
				case 3:
					accessibility = Accessibility.Internal;
					break;
				case 4:
					accessibility = Accessibility.Protected;
					break;
				case 5:
					accessibility = Accessibility.ProtectedOrInternal;
					break;
				case 6:
					accessibility = Accessibility.Public;
					break;
				default:
					accessibility = Accessibility.Private;
					break;
				}
				return accessibility;
			}
		}

		// Token: 0x1700255B RID: 9563
		// (get) Token: 0x06007E35 RID: 32309 RVA: 0x001C5CB7 File Offset: 0x001C3EB7
		public override bool IsStatic
		{
			get
			{
				return (this._flags & 16) > 0;
			}
		}

		// Token: 0x06007E36 RID: 32310 RVA: 0x001C5CC8 File Offset: 0x001C3EC8
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			if (this._lazyCustomAttributes.IsDefault)
			{
				PEModuleSymbol pemoduleSymbol = (PEModuleSymbol)this.ContainingModule;
				if (this.FilterOutDecimalConstantAttribute())
				{
					CustomAttributeHandle customAttributeHandle;
					ImmutableArray<CSharpAttributeData> customAttributesForToken = pemoduleSymbol.GetCustomAttributesForToken(this._handle, out customAttributeHandle, AttributeDescription.DecimalConstantAttribute);
					ImmutableInterlocked.InterlockedInitialize<CSharpAttributeData>(ref this._lazyCustomAttributes, customAttributesForToken);
				}
				else
				{
					pemoduleSymbol.LoadCustomAttributes(this._handle, ref this._lazyCustomAttributes);
				}
			}
			return this._lazyCustomAttributes;
		}

		// Token: 0x06007E37 RID: 32311 RVA: 0x001C5D3C File Offset: 0x001C3F3C
		private bool FilterOutDecimalConstantAttribute()
		{
			ConstantValue constantValue;
			return base.Type.SpecialType == SpecialType.System_Decimal && (constantValue = this.GetConstantValue(ConstantFieldsInProgress.Empty, false)) != null && constantValue.Discriminator == ConstantValueTypeDiscriminator.Decimal;
		}

		// Token: 0x06007E38 RID: 32312 RVA: 0x001C5D74 File Offset: 0x001C3F74
		internal override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			foreach (CSharpAttributeData csharpAttributeData in this.GetAttributes())
			{
				yield return csharpAttributeData;
			}
			ImmutableArray<CSharpAttributeData>.Enumerator enumerator = default(ImmutableArray<CSharpAttributeData>.Enumerator);
			if (this.FilterOutDecimalConstantAttribute())
			{
				PEModuleSymbol containingPEModule = this._containingType.ContainingPEModule;
				yield return new PEAttributeData(containingPEModule, containingPEModule.Module.FindLastTargetAttribute(this._handle, AttributeDescription.DecimalConstantAttribute).Handle);
			}
			yield break;
		}

		// Token: 0x06007E39 RID: 32313 RVA: 0x001C5D84 File Offset: 0x001C3F84
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return PEDocumentationCommentUtils.GetDocumentationComment(this, this._containingType.ContainingPEModule, preferredCulture, cancellationToken, ref this._lazyDocComment);
		}

		// Token: 0x06007E3A RID: 32314 RVA: 0x001C5DA0 File Offset: 0x001C3FA0
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

		// Token: 0x1700255C RID: 9564
		// (get) Token: 0x06007E3B RID: 32315 RVA: 0x001C5DEB File Offset: 0x001C3FEB
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				ObsoleteAttributeHelpers.InitializeObsoleteDataFromMetadata(ref this._lazyObsoleteAttributeData, this._handle, (PEModuleSymbol)this.ContainingModule, false);
				return this._lazyObsoleteAttributeData;
			}
		}

		// Token: 0x1700255D RID: 9565
		// (get) Token: 0x06007E3C RID: 32316 RVA: 0x001C5E15 File Offset: 0x001C4015
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001DBB RID: 7611
		private readonly FieldDefinitionHandle _handle;

		// Token: 0x04001DBC RID: 7612
		private readonly string _name;

		// Token: 0x04001DBD RID: 7613
		private readonly FieldAttributes _flags;

		// Token: 0x04001DBE RID: 7614
		private readonly PENamedTypeSymbol _containingType;

		// Token: 0x04001DBF RID: 7615
		private bool _lazyIsVolatile;

		// Token: 0x04001DC0 RID: 7616
		private ImmutableArray<CSharpAttributeData> _lazyCustomAttributes;

		// Token: 0x04001DC1 RID: 7617
		private ConstantValue _lazyConstantValue = Microsoft.CodeAnalysis.ConstantValue.Unset;

		// Token: 0x04001DC2 RID: 7618
		private Tuple<CultureInfo, string> _lazyDocComment;

		// Token: 0x04001DC3 RID: 7619
		private CachedUseSiteInfo<AssemblySymbol> _lazyCachedUseSiteInfo = CachedUseSiteInfo<AssemblySymbol>.Uninitialized;

		// Token: 0x04001DC4 RID: 7620
		private ObsoleteAttributeData _lazyObsoleteAttributeData = ObsoleteAttributeData.Uninitialized;

		// Token: 0x04001DC5 RID: 7621
		private TypeWithAnnotations.Boxed _lazyType;

		// Token: 0x04001DC6 RID: 7622
		private int _lazyFixedSize;

		// Token: 0x04001DC7 RID: 7623
		private NamedTypeSymbol _lazyFixedImplementationType;

		// Token: 0x04001DC8 RID: 7624
		private PEEventSymbol _associatedEventOpt;

		// Token: 0x04001DC9 RID: 7625
		private PEFieldSymbol.PackedFlags _packedFlags;

		// Token: 0x0200097A RID: 2426
		private struct PackedFlags
		{
			// Token: 0x06009200 RID: 37376 RVA: 0x001FF518 File Offset: 0x001FD718
			public bool SetFlowAnalysisAnnotations(FlowAnalysisAnnotations value)
			{
				int num = 16;
				if ((value & FlowAnalysisAnnotations.DisallowNull) != FlowAnalysisAnnotations.None)
				{
					num |= 1;
				}
				if ((value & FlowAnalysisAnnotations.AllowNull) != FlowAnalysisAnnotations.None)
				{
					num |= 2;
				}
				if ((value & FlowAnalysisAnnotations.MaybeNull) != FlowAnalysisAnnotations.None)
				{
					num |= 4;
				}
				if ((value & FlowAnalysisAnnotations.NotNull) != FlowAnalysisAnnotations.None)
				{
					num |= 8;
				}
				return ThreadSafeFlagOperations.Set(ref this._bits, num);
			}

			// Token: 0x06009201 RID: 37377 RVA: 0x001FF55A File Offset: 0x001FD75A
			public bool TryGetFlowAnalysisAnnotations(out FlowAnalysisAnnotations value)
			{
				int bits = this._bits;
				value = FlowAnalysisAnnotations.None;
				if ((bits & 1) != 0)
				{
					value |= FlowAnalysisAnnotations.DisallowNull;
				}
				if ((bits & 2) != 0)
				{
					value |= FlowAnalysisAnnotations.AllowNull;
				}
				if ((bits & 4) != 0)
				{
					value |= FlowAnalysisAnnotations.MaybeNull;
				}
				if ((bits & 8) != 0)
				{
					value |= FlowAnalysisAnnotations.NotNull;
				}
				return (bits & 16) != 0;
			}

			// Token: 0x040028C8 RID: 10440
			private const int HasDisallowNullAttribute = 1;

			// Token: 0x040028C9 RID: 10441
			private const int HasAllowNullAttribute = 2;

			// Token: 0x040028CA RID: 10442
			private const int HasMaybeNullAttribute = 4;

			// Token: 0x040028CB RID: 10443
			private const int HasNotNullAttribute = 8;

			// Token: 0x040028CC RID: 10444
			private const int FlowAnalysisAnnotationsCompletionBit = 16;

			// Token: 0x040028CD RID: 10445
			private int _bits;
		}
	}
}
