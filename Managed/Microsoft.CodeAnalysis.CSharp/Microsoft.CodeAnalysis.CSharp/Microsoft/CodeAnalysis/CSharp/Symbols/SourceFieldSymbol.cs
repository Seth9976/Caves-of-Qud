using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000537 RID: 1335
	internal abstract class SourceFieldSymbol : FieldSymbolWithAttributesAndModifiers
	{
		// Token: 0x06007006 RID: 28678 RVA: 0x0019B799 File Offset: 0x00199999
		protected SourceFieldSymbol(SourceMemberContainerTypeSymbol containingType)
		{
			this.containingType = containingType;
		}

		// Token: 0x17001E13 RID: 7699
		// (get) Token: 0x06007007 RID: 28679
		public abstract override string Name { get; }

		// Token: 0x17001E14 RID: 7700
		// (get) Token: 0x06007008 RID: 28680 RVA: 0x0019B7A8 File Offset: 0x001999A8
		protected override IAttributeTargetSymbol AttributeOwner
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001E15 RID: 7701
		// (get) Token: 0x06007009 RID: 28681 RVA: 0x0019B7AB File Offset: 0x001999AB
		internal sealed override bool RequiresCompletion
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001E16 RID: 7702
		// (get) Token: 0x0600700A RID: 28682 RVA: 0x0019B7AE File Offset: 0x001999AE
		internal bool IsNew
		{
			get
			{
				return (this.Modifiers & DeclarationModifiers.New) > DeclarationModifiers.None;
			}
		}

		// Token: 0x0600700B RID: 28683 RVA: 0x0019B7BC File Offset: 0x001999BC
		protected void CheckAccessibility(BindingDiagnosticBag diagnostics)
		{
			CSDiagnosticInfo csdiagnosticInfo = ModifierUtils.CheckAccessibility(this.Modifiers, this, false);
			if (csdiagnosticInfo != null)
			{
				diagnostics.Add(new CSDiagnostic(csdiagnosticInfo, this.ErrorLocation, false));
			}
		}

		// Token: 0x0600700C RID: 28684 RVA: 0x0019B7F0 File Offset: 0x001999F0
		protected void ReportModifiersDiagnostics(BindingDiagnosticBag diagnostics)
		{
			if (this.ContainingType.IsSealed && this.DeclaredAccessibility.HasProtected())
			{
				diagnostics.Add(AccessCheck.GetProtectedMemberInSealedTypeError(this.containingType), this.ErrorLocation, new object[] { this });
				return;
			}
			if (this.IsVolatile && this.IsReadOnly)
			{
				diagnostics.Add(ErrorCode.ERR_VolatileAndReadonly, this.ErrorLocation, new object[] { this });
				return;
			}
			if (this.containingType.IsStatic && !this.IsStatic)
			{
				diagnostics.Add(ErrorCode.ERR_InstanceMemberInStaticClass, this.ErrorLocation, new object[] { this });
				return;
			}
			if (!this.IsStatic && !this.IsReadOnly && this.containingType.IsReadOnly)
			{
				diagnostics.Add(ErrorCode.ERR_FieldsInRoStruct, this.ErrorLocation);
			}
		}

		// Token: 0x17001E17 RID: 7703
		// (get) Token: 0x0600700D RID: 28685 RVA: 0x0019B8C8 File Offset: 0x00199AC8
		protected ImmutableArray<CustomModifier> RequiredCustomModifiers
		{
			get
			{
				if (!this.IsVolatile)
				{
					return ImmutableArray<CustomModifier>.Empty;
				}
				return ImmutableArray.Create<CustomModifier>(CSharpCustomModifier.CreateRequired(this.ContainingAssembly.GetSpecialType(SpecialType.System_Runtime_CompilerServices_IsVolatile)));
			}
		}

		// Token: 0x17001E18 RID: 7704
		// (get) Token: 0x0600700E RID: 28686 RVA: 0x0019B8EF File Offset: 0x00199AEF
		public sealed override Symbol ContainingSymbol
		{
			get
			{
				return this.containingType;
			}
		}

		// Token: 0x17001E19 RID: 7705
		// (get) Token: 0x0600700F RID: 28687 RVA: 0x0019B8F7 File Offset: 0x00199AF7
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this.containingType;
			}
		}

		// Token: 0x06007010 RID: 28688 RVA: 0x0019B900 File Offset: 0x00199B00
		internal sealed override void DecodeWellKnownAttribute(ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			if (arguments.Attribute.IsTargetAttribute(this, AttributeDescription.FixedBufferAttribute))
			{
				((BindingDiagnosticBag)arguments.Diagnostics).Add(ErrorCode.ERR_DoNotUseFixedBufferAttr, arguments.AttributeSyntaxOpt.Name.Location);
				return;
			}
			base.DecodeWellKnownAttribute(ref arguments);
		}

		// Token: 0x06007011 RID: 28689 RVA: 0x0019B950 File Offset: 0x00199B50
		internal override void AfterAddingTypeMembersChecks(ConversionsBase conversions, BindingDiagnosticBag diagnostics)
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			Location errorLocation = this.ErrorLocation;
			if (base.Type.ContainsNativeInteger())
			{
				declaringCompilation.EnsureNativeIntegerAttributeExists(diagnostics, errorLocation, true);
			}
			if (declaringCompilation.ShouldEmitNullableAttributes(this) && base.TypeWithAnnotations.NeedsNullableAttribute())
			{
				declaringCompilation.EnsureNullableAttributeExists(diagnostics, errorLocation, true);
			}
		}

		// Token: 0x17001E1A RID: 7706
		// (get) Token: 0x06007012 RID: 28690 RVA: 0x0019B9A3 File Offset: 0x00199BA3
		internal sealed override bool HasRuntimeSpecialName
		{
			get
			{
				return this.Name == "value__";
			}
		}

		// Token: 0x04001B71 RID: 7025
		protected readonly SourceMemberContainerTypeSymbol containingType;
	}
}
