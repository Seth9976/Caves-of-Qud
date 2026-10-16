using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.InteropServices;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200051F RID: 1311
	internal sealed class ImplicitNamedTypeSymbol : SourceMemberContainerTypeSymbol
	{
		// Token: 0x06006DEE RID: 28142 RVA: 0x001949DF File Offset: 0x00192BDF
		internal ImplicitNamedTypeSymbol(NamespaceOrTypeSymbol containingSymbol, MergedTypeDeclaration declaration, BindingDiagnosticBag diagnostics)
			: base(containingSymbol, declaration, diagnostics, null)
		{
			this.state.NotePartComplete(CompletionPart.EnumUnderlyingType);
		}

		// Token: 0x06006DEF RID: 28143 RVA: 0x001949FC File Offset: 0x00192BFC
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006DF0 RID: 28144 RVA: 0x00194A03 File Offset: 0x00192C03
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			this.state.NotePartComplete(CompletionPart.Attributes);
			return ImmutableArray<CSharpAttributeData>.Empty;
		}

		// Token: 0x06006DF1 RID: 28145 RVA: 0x00194A17 File Offset: 0x00192C17
		internal override AttributeUsageInfo GetAttributeUsageInfo()
		{
			return AttributeUsageInfo.Null;
		}

		// Token: 0x06006DF2 RID: 28146 RVA: 0x00194A1E File Offset: 0x00192C1E
		protected override Location GetCorrespondingBaseListLocation(NamedTypeSymbol @base)
		{
			return NoLocation.Singleton;
		}

		// Token: 0x17001CE6 RID: 7398
		// (get) Token: 0x06006DF3 RID: 28147 RVA: 0x00194A25 File Offset: 0x00192C25
		internal override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			get
			{
				if (!this.IsScriptClass)
				{
					return this.DeclaringCompilation.GetSpecialType(SpecialType.System_Object);
				}
				return null;
			}
		}

		// Token: 0x06006DF4 RID: 28148 RVA: 0x00194A40 File Offset: 0x00192C40
		protected override void CheckBase(BindingDiagnosticBag diagnostics)
		{
			diagnostics.ReportUseSite(this.DeclaringCompilation.GetSpecialType(SpecialType.System_Object), this.Locations[0]);
		}

		// Token: 0x06006DF5 RID: 28149 RVA: 0x00194A6F File Offset: 0x00192C6F
		internal override NamedTypeSymbol GetDeclaredBaseType(ConsList<TypeSymbol> basesBeingResolved)
		{
			return this.BaseTypeNoUseSiteDiagnostics;
		}

		// Token: 0x06006DF6 RID: 28150 RVA: 0x00194A77 File Offset: 0x00192C77
		internal override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics(ConsList<TypeSymbol> basesBeingResolved)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006DF7 RID: 28151 RVA: 0x00194A7E File Offset: 0x00192C7E
		internal override ImmutableArray<NamedTypeSymbol> GetDeclaredInterfaces(ConsList<TypeSymbol> basesBeingResolved)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006DF8 RID: 28152 RVA: 0x00194A85 File Offset: 0x00192C85
		protected override void CheckInterfaces(BindingDiagnosticBag diagnostics)
		{
		}

		// Token: 0x17001CE7 RID: 7399
		// (get) Token: 0x06006DF9 RID: 28153 RVA: 0x00194A87 File Offset: 0x00192C87
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x17001CE8 RID: 7400
		// (get) Token: 0x06006DFA RID: 28154 RVA: 0x00194A8E File Offset: 0x00192C8E
		internal override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotationsNoUseSiteDiagnostics
		{
			get
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x17001CE9 RID: 7401
		// (get) Token: 0x06006DFB RID: 28155 RVA: 0x00194A95 File Offset: 0x00192C95
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				NamedTypeSymbol containingType = this.ContainingType;
				if (containingType == null)
				{
					return this.ContainingModule.AreLocalsZeroed;
				}
				return containingType.AreLocalsZeroed;
			}
		}

		// Token: 0x17001CEA RID: 7402
		// (get) Token: 0x06006DFC RID: 28156 RVA: 0x00194AB2 File Offset: 0x00192CB2
		internal override bool IsComImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CEB RID: 7403
		// (get) Token: 0x06006DFD RID: 28157 RVA: 0x00194AB5 File Offset: 0x00192CB5
		internal override NamedTypeSymbol ComImportCoClass
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001CEC RID: 7404
		// (get) Token: 0x06006DFE RID: 28158 RVA: 0x00194AB8 File Offset: 0x00192CB8
		internal override bool HasSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CED RID: 7405
		// (get) Token: 0x06006DFF RID: 28159 RVA: 0x00194ABB File Offset: 0x00192CBB
		internal override bool ShouldAddWinRTMembers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CEE RID: 7406
		// (get) Token: 0x06006E00 RID: 28160 RVA: 0x00194ABE File Offset: 0x00192CBE
		internal sealed override bool IsWindowsRuntimeImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CEF RID: 7407
		// (get) Token: 0x06006E01 RID: 28161 RVA: 0x00194AC1 File Offset: 0x00192CC1
		public sealed override bool IsSerializable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CF0 RID: 7408
		// (get) Token: 0x06006E02 RID: 28162 RVA: 0x00194AC4 File Offset: 0x00192CC4
		internal sealed override TypeLayout Layout
		{
			get
			{
				return default(TypeLayout);
			}
		}

		// Token: 0x17001CF1 RID: 7409
		// (get) Token: 0x06006E03 RID: 28163 RVA: 0x00194ADA File Offset: 0x00192CDA
		internal bool HasStructLayoutAttribute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CF2 RID: 7410
		// (get) Token: 0x06006E04 RID: 28164 RVA: 0x00194ADD File Offset: 0x00192CDD
		internal override CharSet MarshallingCharSet
		{
			get
			{
				return base.DefaultMarshallingCharSet;
			}
		}

		// Token: 0x17001CF3 RID: 7411
		// (get) Token: 0x06006E05 RID: 28165 RVA: 0x00194AE5 File Offset: 0x00192CE5
		internal sealed override bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006E06 RID: 28166 RVA: 0x00194AE8 File Offset: 0x00192CE8
		internal sealed override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006E07 RID: 28167 RVA: 0x00194AEF File Offset: 0x00192CEF
		internal override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x17001CF4 RID: 7412
		// (get) Token: 0x06006E08 RID: 28168 RVA: 0x00194AF6 File Offset: 0x00192CF6
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001CF5 RID: 7413
		// (get) Token: 0x06006E09 RID: 28169 RVA: 0x00194AF9 File Offset: 0x00192CF9
		internal override bool HasCodeAnalysisEmbeddedAttribute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006E0A RID: 28170 RVA: 0x00194AFC File Offset: 0x00192CFC
		internal sealed override NamedTypeSymbol AsNativeInteger()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17001CF6 RID: 7414
		// (get) Token: 0x06006E0B RID: 28171 RVA: 0x00194B03 File Offset: 0x00192D03
		internal sealed override NamedTypeSymbol NativeIntegerUnderlyingType
		{
			get
			{
				return null;
			}
		}
	}
}
