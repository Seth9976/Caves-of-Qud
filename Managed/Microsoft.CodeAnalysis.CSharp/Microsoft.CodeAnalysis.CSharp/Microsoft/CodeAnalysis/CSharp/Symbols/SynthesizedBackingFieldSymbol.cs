using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000581 RID: 1409
	internal sealed class SynthesizedBackingFieldSymbol : FieldSymbolWithAttributesAndModifiers
	{
		// Token: 0x17002069 RID: 8297
		// (get) Token: 0x060075AB RID: 30123 RVA: 0x001B68B8 File Offset: 0x001B4AB8
		internal bool HasInitializer { get; }

		// Token: 0x1700206A RID: 8298
		// (get) Token: 0x060075AC RID: 30124 RVA: 0x001B68C0 File Offset: 0x001B4AC0
		protected override DeclarationModifiers Modifiers { get; }

		// Token: 0x060075AD RID: 30125 RVA: 0x001B68C8 File Offset: 0x001B4AC8
		public SynthesizedBackingFieldSymbol(SourcePropertySymbolBase property, string name, bool isReadOnly, bool isStatic, bool hasInitializer)
		{
			this._name = name;
			this.Modifiers = DeclarationModifiers.Private | (isReadOnly ? DeclarationModifiers.ReadOnly : DeclarationModifiers.None) | (isStatic ? DeclarationModifiers.Static : DeclarationModifiers.None);
			this._property = property;
			this.HasInitializer = hasInitializer;
		}

		// Token: 0x1700206B RID: 8299
		// (get) Token: 0x060075AE RID: 30126 RVA: 0x001B6906 File Offset: 0x001B4B06
		protected override IAttributeTargetSymbol AttributeOwner
		{
			get
			{
				return this._property.AttributesOwner;
			}
		}

		// Token: 0x1700206C RID: 8300
		// (get) Token: 0x060075AF RID: 30127 RVA: 0x001B6913 File Offset: 0x001B4B13
		internal override Location ErrorLocation
		{
			get
			{
				return this._property.Location;
			}
		}

		// Token: 0x1700206D RID: 8301
		// (get) Token: 0x060075B0 RID: 30128 RVA: 0x001B6920 File Offset: 0x001B4B20
		protected override SyntaxList<AttributeListSyntax> AttributeDeclarationSyntaxList
		{
			get
			{
				return this._property.AttributeDeclarationSyntaxList;
			}
		}

		// Token: 0x1700206E RID: 8302
		// (get) Token: 0x060075B1 RID: 30129 RVA: 0x001B692D File Offset: 0x001B4B2D
		public override Symbol AssociatedSymbol
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x1700206F RID: 8303
		// (get) Token: 0x060075B2 RID: 30130 RVA: 0x001B6935 File Offset: 0x001B4B35
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._property.Locations;
			}
		}

		// Token: 0x060075B3 RID: 30131 RVA: 0x001B6942 File Offset: 0x001B4B42
		internal override TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound)
		{
			return this._property.TypeWithAnnotations;
		}

		// Token: 0x17002070 RID: 8304
		// (get) Token: 0x060075B4 RID: 30132 RVA: 0x001B694F File Offset: 0x001B4B4F
		internal override bool HasPointerType
		{
			get
			{
				return this._property.HasPointerType;
			}
		}

		// Token: 0x060075B5 RID: 30133 RVA: 0x001B695C File Offset: 0x001B4B5C
		internal sealed override void DecodeWellKnownAttribute(ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			if (arguments.Attribute.IsTargetAttribute(this, AttributeDescription.FixedBufferAttribute))
			{
				((BindingDiagnosticBag)arguments.Diagnostics).Add(ErrorCode.ERR_DoNotUseFixedBufferAttrOnProperty, arguments.AttributeSyntaxOpt.Name.Location);
				return;
			}
			base.DecodeWellKnownAttribute(ref arguments);
		}

		// Token: 0x060075B6 RID: 30134 RVA: 0x001B69AC File Offset: 0x001B4BAC
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			if (!this.ContainingType.IsImplicitlyDeclared)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_CompilerGeneratedAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
			}
			Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeDebuggerBrowsableNeverAttribute());
		}

		// Token: 0x17002071 RID: 8305
		// (get) Token: 0x060075B7 RID: 30135 RVA: 0x001B6A02 File Offset: 0x001B4C02
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x060075B8 RID: 30136 RVA: 0x001B6A0A File Offset: 0x001B4C0A
		internal override ConstantValue GetConstantValue(ConstantFieldsInProgress inProgress, bool earlyDecodingWellKnownAttributes)
		{
			return null;
		}

		// Token: 0x17002072 RID: 8306
		// (get) Token: 0x060075B9 RID: 30137 RVA: 0x001B6A0D File Offset: 0x001B4C0D
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._property.ContainingSymbol;
			}
		}

		// Token: 0x17002073 RID: 8307
		// (get) Token: 0x060075BA RID: 30138 RVA: 0x001B6A1A File Offset: 0x001B4C1A
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._property.ContainingType;
			}
		}

		// Token: 0x17002074 RID: 8308
		// (get) Token: 0x060075BB RID: 30139 RVA: 0x001B6A27 File Offset: 0x001B4C27
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17002075 RID: 8309
		// (get) Token: 0x060075BC RID: 30140 RVA: 0x001B6A2E File Offset: 0x001B4C2E
		internal override bool HasRuntimeSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002076 RID: 8310
		// (get) Token: 0x060075BD RID: 30141 RVA: 0x001B6A31 File Offset: 0x001B4C31
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060075BE RID: 30142 RVA: 0x001B6A34 File Offset: 0x001B4C34
		internal override void PostDecodeWellKnownAttributes(ImmutableArray<CSharpAttributeData> boundAttributes, ImmutableArray<AttributeSyntax> allAttributeSyntaxNodes, BindingDiagnosticBag diagnostics, AttributeLocation symbolPart, WellKnownAttributeData decodedData)
		{
			base.PostDecodeWellKnownAttributes(boundAttributes, allAttributeSyntaxNodes, diagnostics, symbolPart, decodedData);
			if (!allAttributeSyntaxNodes.IsEmpty && this._property.IsAutoPropertyWithGetAccessor)
			{
				this.CheckForFieldTargetedAttribute(diagnostics);
			}
		}

		// Token: 0x060075BF RID: 30143 RVA: 0x001B6A60 File Offset: 0x001B4C60
		private void CheckForFieldTargetedAttribute(BindingDiagnosticBag diagnostics)
		{
			LanguageVersion languageVersion = this.DeclaringCompilation.LanguageVersion;
			if (languageVersion.AllowAttributesOnBackingFields())
			{
				return;
			}
			foreach (AttributeListSyntax attributeListSyntax in this.AttributeDeclarationSyntaxList)
			{
				AttributeTargetSpecifierSyntax target = attributeListSyntax.Target;
				if (target != null && target.GetAttributeLocation() == AttributeLocation.Field)
				{
					diagnostics.Add(new CSDiagnosticInfo(ErrorCode.WRN_AttributesOnBackingFieldsNotAvailable, new object[]
					{
						languageVersion.ToDisplayString(),
						new CSharpRequiredLanguageVersion(MessageID.IDS_FeatureAttributesOnBackingFields.RequiredVersion())
					}), attributeListSyntax.Target.Location);
				}
			}
		}

		// Token: 0x04001CA6 RID: 7334
		private readonly SourcePropertySymbolBase _property;

		// Token: 0x04001CA7 RID: 7335
		private readonly string _name;
	}
}
