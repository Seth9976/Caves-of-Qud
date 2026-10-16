using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000206 RID: 518
	[NullableContext(1)]
	[Nullable(0)]
	public class SymbolDisplayFormat
	{
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x00040EF8 File Offset: 0x0003F0F8
		public static SymbolDisplayFormat CSharpErrorMessageFormat { get; } = new SymbolDisplayFormat(SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining, SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces, SymbolDisplayGenericsOptions.IncludeTypeParameters, SymbolDisplayMemberOptions.IncludeExplicitInterface | SymbolDisplayMemberOptions.IncludeParameters | SymbolDisplayMemberOptions.IncludeContainingType, SymbolDisplayDelegateStyle.NameOnly, SymbolDisplayExtensionMethodStyle.Default, SymbolDisplayParameterOptions.IncludeParamsRefOut | SymbolDisplayParameterOptions.IncludeType, SymbolDisplayPropertyStyle.NameOnly, SymbolDisplayLocalOptions.None, SymbolDisplayKindOptions.None, SymbolDisplayMiscellaneousOptions.UseSpecialTypes | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers | SymbolDisplayMiscellaneousOptions.UseAsterisksInMultiDimensionalArrays | SymbolDisplayMiscellaneousOptions.UseErrorTypeSymbolName | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier);

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x00040EFF File Offset: 0x0003F0FF
		public static SymbolDisplayFormat CSharpShortErrorMessageFormat { get; } = new SymbolDisplayFormat(SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining, SymbolDisplayTypeQualificationStyle.NameAndContainingTypes, SymbolDisplayGenericsOptions.IncludeTypeParameters, SymbolDisplayMemberOptions.IncludeExplicitInterface | SymbolDisplayMemberOptions.IncludeParameters | SymbolDisplayMemberOptions.IncludeContainingType, SymbolDisplayDelegateStyle.NameOnly, SymbolDisplayExtensionMethodStyle.Default, SymbolDisplayParameterOptions.IncludeParamsRefOut | SymbolDisplayParameterOptions.IncludeType, SymbolDisplayPropertyStyle.NameOnly, SymbolDisplayLocalOptions.None, SymbolDisplayKindOptions.None, SymbolDisplayMiscellaneousOptions.UseSpecialTypes | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers | SymbolDisplayMiscellaneousOptions.UseAsterisksInMultiDimensionalArrays | SymbolDisplayMiscellaneousOptions.UseErrorTypeSymbolName | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier);

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x00040F06 File Offset: 0x0003F106
		public static SymbolDisplayFormat VisualBasicErrorMessageFormat { get; } = new SymbolDisplayFormat(SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining, SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces, SymbolDisplayGenericsOptions.IncludeTypeParameters | SymbolDisplayGenericsOptions.IncludeTypeConstraints | SymbolDisplayGenericsOptions.IncludeVariance, SymbolDisplayMemberOptions.IncludeType | SymbolDisplayMemberOptions.IncludeModifiers | SymbolDisplayMemberOptions.IncludeAccessibility | SymbolDisplayMemberOptions.IncludeParameters | SymbolDisplayMemberOptions.IncludeRef, SymbolDisplayDelegateStyle.NameOnly, SymbolDisplayExtensionMethodStyle.Default, SymbolDisplayParameterOptions.IncludeExtensionThis | SymbolDisplayParameterOptions.IncludeParamsRefOut | SymbolDisplayParameterOptions.IncludeType | SymbolDisplayParameterOptions.IncludeName | SymbolDisplayParameterOptions.IncludeDefaultValue | SymbolDisplayParameterOptions.IncludeOptionalBrackets, SymbolDisplayPropertyStyle.ShowReadWriteDescriptor, SymbolDisplayLocalOptions.None, SymbolDisplayKindOptions.IncludeMemberKeyword, SymbolDisplayMiscellaneousOptions.UseSpecialTypes | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers | SymbolDisplayMiscellaneousOptions.UseAsterisksInMultiDimensionalArrays | SymbolDisplayMiscellaneousOptions.UseErrorTypeSymbolName);

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00040F0D File Offset: 0x0003F10D
		public static SymbolDisplayFormat VisualBasicShortErrorMessageFormat { get; } = new SymbolDisplayFormat(SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining, SymbolDisplayTypeQualificationStyle.NameAndContainingTypes, SymbolDisplayGenericsOptions.IncludeTypeParameters | SymbolDisplayGenericsOptions.IncludeTypeConstraints | SymbolDisplayGenericsOptions.IncludeVariance, SymbolDisplayMemberOptions.IncludeType | SymbolDisplayMemberOptions.IncludeModifiers | SymbolDisplayMemberOptions.IncludeAccessibility | SymbolDisplayMemberOptions.IncludeParameters | SymbolDisplayMemberOptions.IncludeRef, SymbolDisplayDelegateStyle.NameOnly, SymbolDisplayExtensionMethodStyle.Default, SymbolDisplayParameterOptions.IncludeExtensionThis | SymbolDisplayParameterOptions.IncludeParamsRefOut | SymbolDisplayParameterOptions.IncludeType | SymbolDisplayParameterOptions.IncludeName | SymbolDisplayParameterOptions.IncludeDefaultValue | SymbolDisplayParameterOptions.IncludeOptionalBrackets, SymbolDisplayPropertyStyle.ShowReadWriteDescriptor, SymbolDisplayLocalOptions.None, SymbolDisplayKindOptions.IncludeMemberKeyword, SymbolDisplayMiscellaneousOptions.UseSpecialTypes | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers | SymbolDisplayMiscellaneousOptions.UseAsterisksInMultiDimensionalArrays | SymbolDisplayMiscellaneousOptions.UseErrorTypeSymbolName);

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x00040F14 File Offset: 0x0003F114
		public static SymbolDisplayFormat FullyQualifiedFormat { get; } = new SymbolDisplayFormat(SymbolDisplayGlobalNamespaceStyle.Included, SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces, SymbolDisplayGenericsOptions.IncludeTypeParameters, SymbolDisplayMemberOptions.None, SymbolDisplayDelegateStyle.NameOnly, SymbolDisplayExtensionMethodStyle.Default, SymbolDisplayParameterOptions.None, SymbolDisplayPropertyStyle.NameOnly, SymbolDisplayLocalOptions.None, SymbolDisplayKindOptions.None, SymbolDisplayMiscellaneousOptions.UseSpecialTypes | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers);

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x00040F1B File Offset: 0x0003F11B
		public static SymbolDisplayFormat MinimallyQualifiedFormat { get; } = new SymbolDisplayFormat(SymbolDisplayGlobalNamespaceStyle.Omitted, SymbolDisplayTypeQualificationStyle.NameOnly, SymbolDisplayGenericsOptions.IncludeTypeParameters, SymbolDisplayMemberOptions.IncludeType | SymbolDisplayMemberOptions.IncludeParameters | SymbolDisplayMemberOptions.IncludeContainingType | SymbolDisplayMemberOptions.IncludeRef, SymbolDisplayDelegateStyle.NameOnly, SymbolDisplayExtensionMethodStyle.Default, SymbolDisplayParameterOptions.IncludeParamsRefOut | SymbolDisplayParameterOptions.IncludeType | SymbolDisplayParameterOptions.IncludeName | SymbolDisplayParameterOptions.IncludeDefaultValue, SymbolDisplayPropertyStyle.NameOnly, SymbolDisplayLocalOptions.IncludeType, SymbolDisplayKindOptions.IncludeMemberKeyword, SymbolDisplayMiscellaneousOptions.UseSpecialTypes | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier);

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x00040F22 File Offset: 0x0003F122
		public SymbolDisplayGlobalNamespaceStyle GlobalNamespaceStyle { get; }

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x00040F2A File Offset: 0x0003F12A
		public SymbolDisplayTypeQualificationStyle TypeQualificationStyle { get; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x00040F32 File Offset: 0x0003F132
		public SymbolDisplayGenericsOptions GenericsOptions { get; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x00040F3A File Offset: 0x0003F13A
		public SymbolDisplayMemberOptions MemberOptions { get; }

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x00040F42 File Offset: 0x0003F142
		public SymbolDisplayParameterOptions ParameterOptions { get; }

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x00040F4A File Offset: 0x0003F14A
		public SymbolDisplayDelegateStyle DelegateStyle { get; }

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x00040F52 File Offset: 0x0003F152
		public SymbolDisplayExtensionMethodStyle ExtensionMethodStyle { get; }

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x00040F5A File Offset: 0x0003F15A
		public SymbolDisplayPropertyStyle PropertyStyle { get; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x00040F62 File Offset: 0x0003F162
		public SymbolDisplayLocalOptions LocalOptions { get; }

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x00040F6A File Offset: 0x0003F16A
		public SymbolDisplayKindOptions KindOptions { get; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x00040F72 File Offset: 0x0003F172
		public SymbolDisplayMiscellaneousOptions MiscellaneousOptions { get; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x00040F7A File Offset: 0x0003F17A
		internal SymbolDisplayCompilerInternalOptions CompilerInternalOptions { get; }

		// Token: 0x060014A5 RID: 5285 RVA: 0x00040F84 File Offset: 0x0003F184
		public SymbolDisplayFormat(SymbolDisplayGlobalNamespaceStyle globalNamespaceStyle = SymbolDisplayGlobalNamespaceStyle.Omitted, SymbolDisplayTypeQualificationStyle typeQualificationStyle = SymbolDisplayTypeQualificationStyle.NameOnly, SymbolDisplayGenericsOptions genericsOptions = SymbolDisplayGenericsOptions.None, SymbolDisplayMemberOptions memberOptions = SymbolDisplayMemberOptions.None, SymbolDisplayDelegateStyle delegateStyle = SymbolDisplayDelegateStyle.NameOnly, SymbolDisplayExtensionMethodStyle extensionMethodStyle = SymbolDisplayExtensionMethodStyle.Default, SymbolDisplayParameterOptions parameterOptions = SymbolDisplayParameterOptions.None, SymbolDisplayPropertyStyle propertyStyle = SymbolDisplayPropertyStyle.NameOnly, SymbolDisplayLocalOptions localOptions = SymbolDisplayLocalOptions.None, SymbolDisplayKindOptions kindOptions = SymbolDisplayKindOptions.None, SymbolDisplayMiscellaneousOptions miscellaneousOptions = SymbolDisplayMiscellaneousOptions.None)
			: this(SymbolDisplayCompilerInternalOptions.None, globalNamespaceStyle, typeQualificationStyle, genericsOptions, memberOptions, parameterOptions, delegateStyle, extensionMethodStyle, propertyStyle, localOptions, kindOptions, miscellaneousOptions)
		{
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00040FAC File Offset: 0x0003F1AC
		internal SymbolDisplayFormat(SymbolDisplayCompilerInternalOptions compilerInternalOptions, SymbolDisplayGlobalNamespaceStyle globalNamespaceStyle = SymbolDisplayGlobalNamespaceStyle.Omitted, SymbolDisplayTypeQualificationStyle typeQualificationStyle = SymbolDisplayTypeQualificationStyle.NameOnly, SymbolDisplayGenericsOptions genericsOptions = SymbolDisplayGenericsOptions.None, SymbolDisplayMemberOptions memberOptions = SymbolDisplayMemberOptions.None, SymbolDisplayParameterOptions parameterOptions = SymbolDisplayParameterOptions.None, SymbolDisplayDelegateStyle delegateStyle = SymbolDisplayDelegateStyle.NameOnly, SymbolDisplayExtensionMethodStyle extensionMethodStyle = SymbolDisplayExtensionMethodStyle.Default, SymbolDisplayPropertyStyle propertyStyle = SymbolDisplayPropertyStyle.NameOnly, SymbolDisplayLocalOptions localOptions = SymbolDisplayLocalOptions.None, SymbolDisplayKindOptions kindOptions = SymbolDisplayKindOptions.None, SymbolDisplayMiscellaneousOptions miscellaneousOptions = SymbolDisplayMiscellaneousOptions.None)
		{
			this.GlobalNamespaceStyle = globalNamespaceStyle;
			this.TypeQualificationStyle = typeQualificationStyle;
			this.GenericsOptions = genericsOptions;
			this.MemberOptions = memberOptions;
			this.ParameterOptions = parameterOptions;
			this.DelegateStyle = delegateStyle;
			this.ExtensionMethodStyle = extensionMethodStyle;
			this.PropertyStyle = propertyStyle;
			this.LocalOptions = localOptions;
			this.KindOptions = kindOptions;
			this.MiscellaneousOptions = miscellaneousOptions;
			this.CompilerInternalOptions = compilerInternalOptions;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0004101C File Offset: 0x0003F21C
		public SymbolDisplayFormat WithMiscellaneousOptions(SymbolDisplayMiscellaneousOptions options)
		{
			return new SymbolDisplayFormat(this.CompilerInternalOptions, this.GlobalNamespaceStyle, this.TypeQualificationStyle, this.GenericsOptions, this.MemberOptions, this.ParameterOptions, this.DelegateStyle, this.ExtensionMethodStyle, this.PropertyStyle, this.LocalOptions, this.KindOptions, options);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00041071 File Offset: 0x0003F271
		public SymbolDisplayFormat AddMiscellaneousOptions(SymbolDisplayMiscellaneousOptions options)
		{
			return this.WithMiscellaneousOptions(this.MiscellaneousOptions | options);
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00041081 File Offset: 0x0003F281
		public SymbolDisplayFormat RemoveMiscellaneousOptions(SymbolDisplayMiscellaneousOptions options)
		{
			return this.WithMiscellaneousOptions(this.MiscellaneousOptions & ~options);
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00041094 File Offset: 0x0003F294
		public SymbolDisplayFormat WithGenericsOptions(SymbolDisplayGenericsOptions options)
		{
			return new SymbolDisplayFormat(this.CompilerInternalOptions, this.GlobalNamespaceStyle, this.TypeQualificationStyle, options, this.MemberOptions, this.ParameterOptions, this.DelegateStyle, this.ExtensionMethodStyle, this.PropertyStyle, this.LocalOptions, this.KindOptions, this.MiscellaneousOptions);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x000410E9 File Offset: 0x0003F2E9
		public SymbolDisplayFormat AddGenericsOptions(SymbolDisplayGenericsOptions options)
		{
			return this.WithGenericsOptions(this.GenericsOptions | options);
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x000410F9 File Offset: 0x0003F2F9
		public SymbolDisplayFormat RemoveGenericsOptions(SymbolDisplayGenericsOptions options)
		{
			return this.WithGenericsOptions(this.GenericsOptions & ~options);
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0004110C File Offset: 0x0003F30C
		public SymbolDisplayFormat WithMemberOptions(SymbolDisplayMemberOptions options)
		{
			return new SymbolDisplayFormat(this.CompilerInternalOptions, this.GlobalNamespaceStyle, this.TypeQualificationStyle, this.GenericsOptions, options, this.ParameterOptions, this.DelegateStyle, this.ExtensionMethodStyle, this.PropertyStyle, this.LocalOptions, this.KindOptions, this.MiscellaneousOptions);
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00041161 File Offset: 0x0003F361
		public SymbolDisplayFormat AddMemberOptions(SymbolDisplayMemberOptions options)
		{
			return this.WithMemberOptions(this.MemberOptions | options);
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x00041171 File Offset: 0x0003F371
		public SymbolDisplayFormat RemoveMemberOptions(SymbolDisplayMemberOptions options)
		{
			return this.WithMemberOptions(this.MemberOptions & ~options);
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00041184 File Offset: 0x0003F384
		public SymbolDisplayFormat WithKindOptions(SymbolDisplayKindOptions options)
		{
			return new SymbolDisplayFormat(this.CompilerInternalOptions, this.GlobalNamespaceStyle, this.TypeQualificationStyle, this.GenericsOptions, this.MemberOptions, this.ParameterOptions, this.DelegateStyle, this.ExtensionMethodStyle, this.PropertyStyle, this.LocalOptions, options, this.MiscellaneousOptions);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x000411D9 File Offset: 0x0003F3D9
		public SymbolDisplayFormat AddKindOptions(SymbolDisplayKindOptions options)
		{
			return this.WithKindOptions(this.KindOptions | options);
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x000411E9 File Offset: 0x0003F3E9
		public SymbolDisplayFormat RemoveKindOptions(SymbolDisplayKindOptions options)
		{
			return this.WithKindOptions(this.KindOptions & ~options);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x000411FC File Offset: 0x0003F3FC
		public SymbolDisplayFormat WithParameterOptions(SymbolDisplayParameterOptions options)
		{
			return new SymbolDisplayFormat(this.CompilerInternalOptions, this.GlobalNamespaceStyle, this.TypeQualificationStyle, this.GenericsOptions, this.MemberOptions, options, this.DelegateStyle, this.ExtensionMethodStyle, this.PropertyStyle, this.LocalOptions, this.KindOptions, this.MiscellaneousOptions);
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00041251 File Offset: 0x0003F451
		public SymbolDisplayFormat AddParameterOptions(SymbolDisplayParameterOptions options)
		{
			return this.WithParameterOptions(this.ParameterOptions | options);
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x00041261 File Offset: 0x0003F461
		public SymbolDisplayFormat RemoveParameterOptions(SymbolDisplayParameterOptions options)
		{
			return this.WithParameterOptions(this.ParameterOptions & ~options);
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x00041274 File Offset: 0x0003F474
		public SymbolDisplayFormat WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle style)
		{
			return new SymbolDisplayFormat(this.CompilerInternalOptions, style, this.TypeQualificationStyle, this.GenericsOptions, this.MemberOptions, this.ParameterOptions, this.DelegateStyle, this.ExtensionMethodStyle, this.PropertyStyle, this.LocalOptions, this.KindOptions, this.MiscellaneousOptions);
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x000412CC File Offset: 0x0003F4CC
		public SymbolDisplayFormat WithLocalOptions(SymbolDisplayLocalOptions options)
		{
			return new SymbolDisplayFormat(this.CompilerInternalOptions, this.GlobalNamespaceStyle, this.TypeQualificationStyle, this.GenericsOptions, this.MemberOptions, this.ParameterOptions, this.DelegateStyle, this.ExtensionMethodStyle, this.PropertyStyle, options, this.KindOptions, this.MiscellaneousOptions);
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00041321 File Offset: 0x0003F521
		public SymbolDisplayFormat AddLocalOptions(SymbolDisplayLocalOptions options)
		{
			return this.WithLocalOptions(this.LocalOptions | options);
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x00041331 File Offset: 0x0003F531
		public SymbolDisplayFormat RemoveLocalOptions(SymbolDisplayLocalOptions options)
		{
			return this.WithLocalOptions(this.LocalOptions & ~options);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00041344 File Offset: 0x0003F544
		internal SymbolDisplayFormat WithCompilerInternalOptions(SymbolDisplayCompilerInternalOptions options)
		{
			return new SymbolDisplayFormat(options, this.GlobalNamespaceStyle, this.TypeQualificationStyle, this.GenericsOptions, this.MemberOptions, this.ParameterOptions, this.DelegateStyle, this.ExtensionMethodStyle, this.PropertyStyle, this.LocalOptions, this.KindOptions, this.MiscellaneousOptions);
		}

		// Token: 0x0400071F RID: 1823
		internal static readonly SymbolDisplayFormat TestFormat = new SymbolDisplayFormat(SymbolDisplayCompilerInternalOptions.UseMetadataMethodNames | SymbolDisplayCompilerInternalOptions.FlagMissingMetadataTypes | SymbolDisplayCompilerInternalOptions.IncludeScriptType | SymbolDisplayCompilerInternalOptions.IncludeCustomModifiers, SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining, SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces, SymbolDisplayGenericsOptions.IncludeTypeParameters | SymbolDisplayGenericsOptions.IncludeVariance, SymbolDisplayMemberOptions.IncludeType | SymbolDisplayMemberOptions.IncludeExplicitInterface | SymbolDisplayMemberOptions.IncludeParameters | SymbolDisplayMemberOptions.IncludeContainingType | SymbolDisplayMemberOptions.IncludeRef, SymbolDisplayParameterOptions.IncludeExtensionThis | SymbolDisplayParameterOptions.IncludeParamsRefOut | SymbolDisplayParameterOptions.IncludeType | SymbolDisplayParameterOptions.IncludeName | SymbolDisplayParameterOptions.IncludeDefaultValue | SymbolDisplayParameterOptions.IncludeOptionalBrackets, SymbolDisplayDelegateStyle.NameOnly, SymbolDisplayExtensionMethodStyle.Default, SymbolDisplayPropertyStyle.ShowReadWriteDescriptor, SymbolDisplayLocalOptions.IncludeType, SymbolDisplayKindOptions.IncludeMemberKeyword, SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers | SymbolDisplayMiscellaneousOptions.UseErrorTypeSymbolName | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier);

		// Token: 0x04000720 RID: 1824
		internal static readonly SymbolDisplayFormat TestFormatWithConstraints = SymbolDisplayFormat.TestFormat.WithGenericsOptions(SymbolDisplayFormat.TestFormat.GenericsOptions | SymbolDisplayGenericsOptions.IncludeTypeConstraints).AddMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.IncludeNotNullableReferenceTypeModifier).WithCompilerInternalOptions(SymbolDisplayCompilerInternalOptions.None);

		// Token: 0x04000721 RID: 1825
		internal static readonly SymbolDisplayFormat QualifiedNameOnlyFormat = new SymbolDisplayFormat(SymbolDisplayGlobalNamespaceStyle.Omitted, SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces, SymbolDisplayGenericsOptions.None, SymbolDisplayMemberOptions.None, SymbolDisplayDelegateStyle.NameOnly, SymbolDisplayExtensionMethodStyle.Default, SymbolDisplayParameterOptions.None, SymbolDisplayPropertyStyle.NameOnly, SymbolDisplayLocalOptions.None, SymbolDisplayKindOptions.None, SymbolDisplayMiscellaneousOptions.None);

		// Token: 0x04000722 RID: 1826
		internal static readonly SymbolDisplayFormat QualifiedNameArityFormat = new SymbolDisplayFormat(SymbolDisplayCompilerInternalOptions.UseArityForGenericTypes | SymbolDisplayCompilerInternalOptions.UseValueTuple, SymbolDisplayGlobalNamespaceStyle.Omitted, SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces, SymbolDisplayGenericsOptions.None, SymbolDisplayMemberOptions.None, SymbolDisplayParameterOptions.None, SymbolDisplayDelegateStyle.NameOnly, SymbolDisplayExtensionMethodStyle.Default, SymbolDisplayPropertyStyle.NameOnly, SymbolDisplayLocalOptions.None, SymbolDisplayKindOptions.None, SymbolDisplayMiscellaneousOptions.None);

		// Token: 0x04000723 RID: 1827
		internal static readonly SymbolDisplayFormat ShortFormat = new SymbolDisplayFormat(SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining, SymbolDisplayTypeQualificationStyle.NameOnly, SymbolDisplayGenericsOptions.None, SymbolDisplayMemberOptions.None, SymbolDisplayDelegateStyle.NameOnly, SymbolDisplayExtensionMethodStyle.Default, SymbolDisplayParameterOptions.IncludeName, SymbolDisplayPropertyStyle.NameOnly, SymbolDisplayLocalOptions.None, SymbolDisplayKindOptions.None, SymbolDisplayMiscellaneousOptions.UseSpecialTypes | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier);

		// Token: 0x04000724 RID: 1828
		internal static readonly SymbolDisplayFormat ILVisualizationFormat = new SymbolDisplayFormat(SymbolDisplayCompilerInternalOptions.UseMetadataMethodNames | SymbolDisplayCompilerInternalOptions.UseValueTuple, SymbolDisplayGlobalNamespaceStyle.Omitted, SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces, SymbolDisplayGenericsOptions.IncludeTypeParameters, SymbolDisplayMemberOptions.IncludeType | SymbolDisplayMemberOptions.IncludeParameters | SymbolDisplayMemberOptions.IncludeContainingType | SymbolDisplayMemberOptions.IncludeRef, SymbolDisplayParameterOptions.IncludeParamsRefOut | SymbolDisplayParameterOptions.IncludeType, SymbolDisplayDelegateStyle.NameOnly, SymbolDisplayExtensionMethodStyle.Default, SymbolDisplayPropertyStyle.NameOnly, SymbolDisplayLocalOptions.None, SymbolDisplayKindOptions.IncludeMemberKeyword, SymbolDisplayMiscellaneousOptions.UseSpecialTypes);

		// Token: 0x04000725 RID: 1829
		internal static readonly SymbolDisplayFormat ExplicitInterfaceImplementationFormat = new SymbolDisplayFormat(SymbolDisplayCompilerInternalOptions.ReverseArrayRankSpecifiers, SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining, SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces, SymbolDisplayGenericsOptions.IncludeTypeParameters, SymbolDisplayMemberOptions.None, SymbolDisplayParameterOptions.None, SymbolDisplayDelegateStyle.NameOnly, SymbolDisplayExtensionMethodStyle.Default, SymbolDisplayPropertyStyle.NameOnly, SymbolDisplayLocalOptions.None, SymbolDisplayKindOptions.None, SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers);
	}
}
