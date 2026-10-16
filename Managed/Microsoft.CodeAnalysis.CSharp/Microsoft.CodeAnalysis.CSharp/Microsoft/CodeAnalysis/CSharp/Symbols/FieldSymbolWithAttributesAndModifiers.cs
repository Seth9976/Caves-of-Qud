using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200051C RID: 1308
	internal abstract class FieldSymbolWithAttributesAndModifiers : FieldSymbol, IAttributeTargetSymbol
	{
		// Token: 0x17001CCC RID: 7372
		// (get) Token: 0x06006DC2 RID: 28098
		internal abstract Location ErrorLocation { get; }

		// Token: 0x17001CCD RID: 7373
		// (get) Token: 0x06006DC3 RID: 28099
		protected abstract DeclarationModifiers Modifiers { get; }

		// Token: 0x17001CCE RID: 7374
		// (get) Token: 0x06006DC4 RID: 28100
		protected abstract SyntaxList<AttributeListSyntax> AttributeDeclarationSyntaxList { get; }

		// Token: 0x17001CCF RID: 7375
		// (get) Token: 0x06006DC5 RID: 28101
		protected abstract IAttributeTargetSymbol AttributeOwner { get; }

		// Token: 0x17001CD0 RID: 7376
		// (get) Token: 0x06006DC6 RID: 28102 RVA: 0x001940E5 File Offset: 0x001922E5
		IAttributeTargetSymbol IAttributeTargetSymbol.AttributesOwner
		{
			get
			{
				return this.AttributeOwner;
			}
		}

		// Token: 0x17001CD1 RID: 7377
		// (get) Token: 0x06006DC7 RID: 28103 RVA: 0x001940ED File Offset: 0x001922ED
		AttributeLocation IAttributeTargetSymbol.DefaultAttributeLocation
		{
			get
			{
				return AttributeLocation.Field;
			}
		}

		// Token: 0x17001CD2 RID: 7378
		// (get) Token: 0x06006DC8 RID: 28104 RVA: 0x001940F1 File Offset: 0x001922F1
		AttributeLocation IAttributeTargetSymbol.AllowedAttributeLocations
		{
			get
			{
				return AttributeLocation.Field;
			}
		}

		// Token: 0x06006DC9 RID: 28105 RVA: 0x001940F5 File Offset: 0x001922F5
		internal sealed override bool HasComplete(CompletionPart part)
		{
			return this.state.HasComplete(part);
		}

		// Token: 0x17001CD3 RID: 7379
		// (get) Token: 0x06006DCA RID: 28106 RVA: 0x00194103 File Offset: 0x00192303
		public sealed override bool IsStatic
		{
			get
			{
				return (this.Modifiers & DeclarationModifiers.Static) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001CD4 RID: 7380
		// (get) Token: 0x06006DCB RID: 28107 RVA: 0x00194110 File Offset: 0x00192310
		public sealed override bool IsReadOnly
		{
			get
			{
				return (this.Modifiers & DeclarationModifiers.ReadOnly) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001CD5 RID: 7381
		// (get) Token: 0x06006DCC RID: 28108 RVA: 0x00194121 File Offset: 0x00192321
		public sealed override Accessibility DeclaredAccessibility
		{
			get
			{
				return ModifierUtils.EffectiveAccessibility(this.Modifiers);
			}
		}

		// Token: 0x17001CD6 RID: 7382
		// (get) Token: 0x06006DCD RID: 28109 RVA: 0x0019412E File Offset: 0x0019232E
		public sealed override bool IsConst
		{
			get
			{
				return (this.Modifiers & DeclarationModifiers.Const) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001CD7 RID: 7383
		// (get) Token: 0x06006DCE RID: 28110 RVA: 0x0019413F File Offset: 0x0019233F
		public sealed override bool IsVolatile
		{
			get
			{
				return (this.Modifiers & DeclarationModifiers.Volatile) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001CD8 RID: 7384
		// (get) Token: 0x06006DCF RID: 28111 RVA: 0x00194150 File Offset: 0x00192350
		public sealed override bool IsFixedSizeBuffer
		{
			get
			{
				return (this.Modifiers & DeclarationModifiers.Fixed) > DeclarationModifiers.None;
			}
		}

		// Token: 0x06006DD0 RID: 28112 RVA: 0x00194161 File Offset: 0x00192361
		public sealed override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.GetAttributesBag().Attributes;
		}

		// Token: 0x06006DD1 RID: 28113 RVA: 0x00194170 File Offset: 0x00192370
		private CustomAttributesBag<CSharpAttributeData> GetAttributesBag()
		{
			CustomAttributesBag<CSharpAttributeData> lazyCustomAttributesBag = this._lazyCustomAttributesBag;
			if (lazyCustomAttributesBag != null && lazyCustomAttributesBag.IsSealed)
			{
				return lazyCustomAttributesBag;
			}
			if (base.LoadAndValidateAttributes(OneOrMany.Create<SyntaxList<AttributeListSyntax>>(this.AttributeDeclarationSyntaxList), ref this._lazyCustomAttributesBag, AttributeLocation.None, false, null, null))
			{
				this.state.NotePartComplete(CompletionPart.Attributes);
			}
			return this._lazyCustomAttributesBag;
		}

		// Token: 0x06006DD2 RID: 28114 RVA: 0x001941C4 File Offset: 0x001923C4
		protected FieldWellKnownAttributeData GetDecodedWellKnownAttributeData()
		{
			CustomAttributesBag<CSharpAttributeData> customAttributesBag = this._lazyCustomAttributesBag;
			if (customAttributesBag == null || !customAttributesBag.IsDecodedWellKnownAttributeDataComputed)
			{
				customAttributesBag = this.GetAttributesBag();
			}
			return (FieldWellKnownAttributeData)customAttributesBag.DecodedWellKnownAttributeData;
		}

		// Token: 0x06006DD3 RID: 28115 RVA: 0x001941F8 File Offset: 0x001923F8
		internal sealed override CSharpAttributeData EarlyDecodeWellKnownAttribute(ref EarlyDecodeWellKnownAttributeArguments<EarlyWellKnownAttributeBinder, NamedTypeSymbol, AttributeSyntax, AttributeLocation> arguments)
		{
			CSharpAttributeData csharpAttributeData;
			ObsoleteAttributeData obsoleteAttributeData;
			if (Symbol.EarlyDecodeDeprecatedOrExperimentalOrObsoleteAttribute(ref arguments, out csharpAttributeData, out obsoleteAttributeData))
			{
				if (obsoleteAttributeData != null)
				{
					arguments.GetOrCreateData<CommonFieldEarlyWellKnownAttributeData>().ObsoleteAttributeData = obsoleteAttributeData;
				}
				return csharpAttributeData;
			}
			return base.EarlyDecodeWellKnownAttribute(ref arguments);
		}

		// Token: 0x17001CD9 RID: 7385
		// (get) Token: 0x06006DD4 RID: 28116 RVA: 0x0019422C File Offset: 0x0019242C
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				if (!((SourceMemberContainerTypeSymbol)this.ContainingType).AnyMemberHasAttributes)
				{
					return null;
				}
				CustomAttributesBag<CSharpAttributeData> lazyCustomAttributesBag = this._lazyCustomAttributesBag;
				if (lazyCustomAttributesBag == null || !lazyCustomAttributesBag.IsEarlyDecodedWellKnownAttributeDataComputed)
				{
					return ObsoleteAttributeData.Uninitialized;
				}
				CommonFieldEarlyWellKnownAttributeData commonFieldEarlyWellKnownAttributeData = (CommonFieldEarlyWellKnownAttributeData)lazyCustomAttributesBag.EarlyDecodedWellKnownAttributeData;
				if (commonFieldEarlyWellKnownAttributeData == null)
				{
					return null;
				}
				return commonFieldEarlyWellKnownAttributeData.ObsoleteAttributeData;
			}
		}

		// Token: 0x06006DD5 RID: 28117 RVA: 0x0019427C File Offset: 0x0019247C
		internal override void DecodeWellKnownAttribute(ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			BindingDiagnosticBag bindingDiagnosticBag = (BindingDiagnosticBag)arguments.Diagnostics;
			CSharpAttributeData attribute = arguments.Attribute;
			if (attribute.IsTargetAttribute(this, AttributeDescription.SpecialNameAttribute))
			{
				arguments.GetOrCreateData<FieldWellKnownAttributeData>().HasSpecialNameAttribute = true;
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.NonSerializedAttribute))
			{
				arguments.GetOrCreateData<FieldWellKnownAttributeData>().HasNonSerializedAttribute = true;
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.FieldOffsetAttribute))
			{
				if (this.IsStatic || this.IsConst)
				{
					bindingDiagnosticBag.Add(ErrorCode.ERR_StructOffsetOnBadField, arguments.AttributeSyntaxOpt.Name.Location, new object[] { arguments.AttributeSyntaxOpt.GetErrorDisplayName() });
					return;
				}
				int num = attribute.CommonConstructorArguments[0].DecodeValue<int>(SpecialType.System_Int32);
				if (num < 0)
				{
					CSharpSyntaxNode attributeArgumentSyntax = attribute.GetAttributeArgumentSyntax(0, arguments.AttributeSyntaxOpt);
					bindingDiagnosticBag.Add(ErrorCode.ERR_InvalidAttributeArgument, attributeArgumentSyntax.Location, new object[] { arguments.AttributeSyntaxOpt.GetErrorDisplayName() });
					num = 0;
				}
				arguments.GetOrCreateData<FieldWellKnownAttributeData>().SetFieldOffset(num);
				return;
			}
			else
			{
				if (attribute.IsTargetAttribute(this, AttributeDescription.MarshalAsAttribute))
				{
					MarshalAsAttributeDecoder<FieldWellKnownAttributeData, AttributeSyntax, CSharpAttributeData, AttributeLocation>.Decode(ref arguments, 256, MessageProvider.Instance);
					return;
				}
				if (!base.ReportExplicitUseOfReservedAttributes(in arguments, Symbol.ReservedAttributes.DynamicAttribute | Symbol.ReservedAttributes.IsReadOnlyAttribute | Symbol.ReservedAttributes.IsUnmanagedAttribute | Symbol.ReservedAttributes.IsByRefLikeAttribute | Symbol.ReservedAttributes.TupleElementNamesAttribute | Symbol.ReservedAttributes.NullableAttribute | Symbol.ReservedAttributes.NativeIntegerAttribute))
				{
					if (attribute.IsTargetAttribute(this, AttributeDescription.DateTimeConstantAttribute))
					{
						this.VerifyConstantValueMatches(attribute.DecodeDateTimeConstantValue(), ref arguments);
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.DecimalConstantAttribute))
					{
						this.VerifyConstantValueMatches(attribute.DecodeDecimalConstantValue(), ref arguments);
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.AllowNullAttribute))
					{
						arguments.GetOrCreateData<FieldWellKnownAttributeData>().HasAllowNullAttribute = true;
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.DisallowNullAttribute))
					{
						arguments.GetOrCreateData<FieldWellKnownAttributeData>().HasDisallowNullAttribute = true;
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.MaybeNullAttribute))
					{
						arguments.GetOrCreateData<FieldWellKnownAttributeData>().HasMaybeNullAttribute = true;
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.NotNullAttribute))
					{
						arguments.GetOrCreateData<FieldWellKnownAttributeData>().HasNotNullAttribute = true;
					}
				}
				return;
			}
		}

		// Token: 0x17001CDA RID: 7386
		// (get) Token: 0x06006DD6 RID: 28118 RVA: 0x0019445B File Offset: 0x0019265B
		public override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return FieldSymbolWithAttributesAndModifiers.DecodeFlowAnalysisAttributes(this.GetDecodedWellKnownAttributeData());
			}
		}

		// Token: 0x06006DD7 RID: 28119 RVA: 0x00194468 File Offset: 0x00192668
		private static FlowAnalysisAnnotations DecodeFlowAnalysisAttributes(FieldWellKnownAttributeData attributeData)
		{
			FlowAnalysisAnnotations flowAnalysisAnnotations = FlowAnalysisAnnotations.None;
			if (attributeData != null)
			{
				if (attributeData.HasAllowNullAttribute)
				{
					flowAnalysisAnnotations |= FlowAnalysisAnnotations.AllowNull;
				}
				if (attributeData.HasDisallowNullAttribute)
				{
					flowAnalysisAnnotations |= FlowAnalysisAnnotations.DisallowNull;
				}
				if (attributeData.HasMaybeNullAttribute)
				{
					flowAnalysisAnnotations |= FlowAnalysisAnnotations.MaybeNull;
				}
				if (attributeData.HasNotNullAttribute)
				{
					flowAnalysisAnnotations |= FlowAnalysisAnnotations.NotNull;
				}
			}
			return flowAnalysisAnnotations;
		}

		// Token: 0x06006DD8 RID: 28120 RVA: 0x001944B0 File Offset: 0x001926B0
		private void VerifyConstantValueMatches(ConstantValue attrValue, ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			if (!attrValue.IsBad)
			{
				FieldWellKnownAttributeData orCreateData = arguments.GetOrCreateData<FieldWellKnownAttributeData>();
				BindingDiagnosticBag bindingDiagnosticBag = (BindingDiagnosticBag)arguments.Diagnostics;
				if (this.IsConst)
				{
					if (base.Type.SpecialType == SpecialType.System_Decimal)
					{
						ConstantValue constantValue = this.GetConstantValue(ConstantFieldsInProgress.Empty, false);
						if (constantValue != null && !constantValue.IsBad && constantValue != attrValue)
						{
							bindingDiagnosticBag.Add(ErrorCode.ERR_FieldHasMultipleDistinctConstantValues, arguments.AttributeSyntaxOpt.Location);
						}
					}
					else
					{
						bindingDiagnosticBag.Add(ErrorCode.ERR_FieldHasMultipleDistinctConstantValues, arguments.AttributeSyntaxOpt.Location);
					}
					if (orCreateData.ConstValue == Microsoft.CodeAnalysis.ConstantValue.Unset)
					{
						orCreateData.ConstValue = attrValue;
						return;
					}
				}
				else
				{
					ConstantValue constantValue = orCreateData.ConstValue;
					if (constantValue != Microsoft.CodeAnalysis.ConstantValue.Unset)
					{
						if (constantValue != attrValue)
						{
							bindingDiagnosticBag.Add(ErrorCode.ERR_FieldHasMultipleDistinctConstantValues, arguments.AttributeSyntaxOpt.Location);
							return;
						}
					}
					else
					{
						orCreateData.ConstValue = attrValue;
					}
				}
			}
		}

		// Token: 0x06006DD9 RID: 28121 RVA: 0x0019459C File Offset: 0x0019279C
		internal override void PostDecodeWellKnownAttributes(ImmutableArray<CSharpAttributeData> boundAttributes, ImmutableArray<AttributeSyntax> allAttributeSyntaxNodes, BindingDiagnosticBag diagnostics, AttributeLocation symbolPart, WellKnownAttributeData decodedData)
		{
			FieldWellKnownAttributeData fieldWellKnownAttributeData = (FieldWellKnownAttributeData)decodedData;
			int? num = ((fieldWellKnownAttributeData != null) ? fieldWellKnownAttributeData.Offset : default(int?));
			if (num != null)
			{
				if (this.ContainingType.Layout.Kind != 2)
				{
					int num2 = boundAttributes.IndexOfAttribute(this, AttributeDescription.FieldOffsetAttribute);
					diagnostics.Add(ErrorCode.ERR_StructOffsetOnBadStruct, allAttributeSyntaxNodes[num2].Name.Location);
				}
			}
			else if (!this.IsStatic && !this.IsConst && this.ContainingType.Layout.Kind == 2)
			{
				diagnostics.Add(ErrorCode.ERR_MissingStructOffset, this.ErrorLocation, new object[] { this.AttributeOwner });
			}
			base.PostDecodeWellKnownAttributes(boundAttributes, allAttributeSyntaxNodes, diagnostics, symbolPart, decodedData);
		}

		// Token: 0x17001CDB RID: 7387
		// (get) Token: 0x06006DDA RID: 28122 RVA: 0x00194668 File Offset: 0x00192868
		internal sealed override bool HasSpecialName
		{
			get
			{
				if (this.HasRuntimeSpecialName)
				{
					return true;
				}
				FieldWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasSpecialNameAttribute;
			}
		}

		// Token: 0x17001CDC RID: 7388
		// (get) Token: 0x06006DDB RID: 28123 RVA: 0x00194694 File Offset: 0x00192894
		internal sealed override bool IsNotSerialized
		{
			get
			{
				FieldWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasNonSerializedAttribute;
			}
		}

		// Token: 0x17001CDD RID: 7389
		// (get) Token: 0x06006DDC RID: 28124 RVA: 0x001946B4 File Offset: 0x001928B4
		internal sealed override MarshalPseudoCustomAttributeData MarshallingInformation
		{
			get
			{
				FieldWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				if (decodedWellKnownAttributeData == null)
				{
					return null;
				}
				return decodedWellKnownAttributeData.MarshallingInformation;
			}
		}

		// Token: 0x17001CDE RID: 7390
		// (get) Token: 0x06006DDD RID: 28125 RVA: 0x001946D4 File Offset: 0x001928D4
		internal sealed override int? TypeLayoutOffset
		{
			get
			{
				FieldWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				if (decodedWellKnownAttributeData == null)
				{
					return default(int?);
				}
				return decodedWellKnownAttributeData.Offset;
			}
		}

		// Token: 0x06006DDE RID: 28126 RVA: 0x001946FC File Offset: 0x001928FC
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			TypeWithAnnotations typeWithAnnotations = base.TypeWithAnnotations;
			if (typeWithAnnotations.Type.ContainsDynamic())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeDynamicAttribute(typeWithAnnotations.Type, typeWithAnnotations.CustomModifiers.Length, RefKind.None));
			}
			if (typeWithAnnotations.Type.ContainsNativeInteger())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNativeIntegerAttribute(this, typeWithAnnotations.Type));
			}
			if (typeWithAnnotations.Type.ContainsTupleNames())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeTupleNamesAttribute(typeWithAnnotations.Type));
			}
			if (declaringCompilation.ShouldEmitNullableAttributes(this))
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNullableAttributeIfNecessary(this, this.ContainingType.GetNullableContextValue(), typeWithAnnotations));
			}
		}

		// Token: 0x04001B1B RID: 6939
		private CustomAttributesBag<CSharpAttributeData> _lazyCustomAttributesBag;

		// Token: 0x04001B1C RID: 6940
		protected SymbolCompletionState state;
	}
}
