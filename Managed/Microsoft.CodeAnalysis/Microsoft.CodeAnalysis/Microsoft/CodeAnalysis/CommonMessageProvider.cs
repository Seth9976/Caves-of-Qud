using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200016C RID: 364
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class CommonMessageProvider
	{
		// Token: 0x06000E96 RID: 3734
		public abstract DiagnosticSeverity GetSeverity(int code);

		// Token: 0x06000E97 RID: 3735
		public abstract string LoadMessage(int code, [Nullable(2)] CultureInfo language);

		// Token: 0x06000E98 RID: 3736
		public abstract LocalizableString GetTitle(int code);

		// Token: 0x06000E99 RID: 3737
		public abstract LocalizableString GetDescription(int code);

		// Token: 0x06000E9A RID: 3738
		public abstract LocalizableString GetMessageFormat(int code);

		// Token: 0x06000E9B RID: 3739
		public abstract string GetHelpLink(int code);

		// Token: 0x06000E9C RID: 3740
		public abstract string GetCategory(int code);

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000E9D RID: 3741
		public abstract string CodePrefix { get; }

		// Token: 0x06000E9E RID: 3742
		public abstract int GetWarningLevel(int code);

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000E9F RID: 3743
		public abstract Type ErrorCodeType { get; }

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0002C699 File Offset: 0x0002A899
		public Diagnostic CreateDiagnostic(int code, Location location)
		{
			return this.CreateDiagnostic(code, location, Array.Empty<object>());
		}

		// Token: 0x06000EA1 RID: 3745
		public abstract Diagnostic CreateDiagnostic(DiagnosticInfo info);

		// Token: 0x06000EA2 RID: 3746
		public abstract Diagnostic CreateDiagnostic(int code, Location location, params object[] args);

		// Token: 0x06000EA3 RID: 3747
		public abstract string GetMessagePrefix(string id, DiagnosticSeverity severity, bool isWarningAsError, [Nullable(2)] CultureInfo culture);

		// Token: 0x06000EA4 RID: 3748
		public abstract string GetErrorDisplayString(ISymbol symbol);

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0002C6A8 File Offset: 0x0002A8A8
		public string GetIdForErrorCode(int errorCode)
		{
			return CommonMessageProvider.s_errorIdCache.GetOrAdd(new ValueTuple<string, int>(this.CodePrefix, errorCode), ([TupleElementNames(new string[] { "prefix", "code" })] [Nullable(new byte[] { 0, 1 })] ValueTuple<string, int> key) => key.Item1 + key.Item2.ToString("0000"));
		}

		// Token: 0x06000EA6 RID: 3750
		public abstract ReportDiagnostic GetDiagnosticReport(DiagnosticInfo diagnosticInfo, CompilationOptions options);

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0002C6E0 File Offset: 0x0002A8E0
		[return: Nullable(2)]
		public DiagnosticInfo FilterDiagnosticInfo(DiagnosticInfo diagnosticInfo, CompilationOptions options)
		{
			switch (this.GetDiagnosticReport(diagnosticInfo, options))
			{
			case ReportDiagnostic.Error:
				return diagnosticInfo.GetInstanceWithSeverity(DiagnosticSeverity.Error);
			case ReportDiagnostic.Warn:
				return diagnosticInfo.GetInstanceWithSeverity(DiagnosticSeverity.Warning);
			case ReportDiagnostic.Info:
				return diagnosticInfo.GetInstanceWithSeverity(DiagnosticSeverity.Info);
			case ReportDiagnostic.Hidden:
				return diagnosticInfo.GetInstanceWithSeverity(DiagnosticSeverity.Hidden);
			case ReportDiagnostic.Suppress:
				return null;
			default:
				return diagnosticInfo;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000EA8 RID: 3752
		public abstract int ERR_FailedToCreateTempFile { get; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000EA9 RID: 3753
		public abstract int ERR_MultipleAnalyzerConfigsInSameDir { get; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000EAA RID: 3754
		public abstract int ERR_ExpectedSingleScript { get; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000EAB RID: 3755
		public abstract int ERR_OpenResponseFile { get; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000EAC RID: 3756
		public abstract int ERR_InvalidPathMap { get; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000EAD RID: 3757
		public abstract int FTL_InvalidInputFileName { get; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000EAE RID: 3758
		public abstract int ERR_FileNotFound { get; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000EAF RID: 3759
		public abstract int ERR_NoSourceFile { get; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000EB0 RID: 3760
		public abstract int ERR_CantOpenFileWrite { get; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000EB1 RID: 3761
		public abstract int ERR_OutputWriteFailed { get; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000EB2 RID: 3762
		public abstract int WRN_NoConfigNotOnCommandLine { get; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000EB3 RID: 3763
		public abstract int ERR_BinaryFile { get; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000EB4 RID: 3764
		public abstract int WRN_UnableToLoadAnalyzer { get; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000EB5 RID: 3765
		public abstract int INF_UnableToLoadSomeTypesInAnalyzer { get; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000EB6 RID: 3766
		public abstract int WRN_AnalyzerCannotBeCreated { get; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000EB7 RID: 3767
		public abstract int WRN_NoAnalyzerInAssembly { get; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000EB8 RID: 3768
		public abstract int WRN_AnalyzerReferencesFramework { get; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000EB9 RID: 3769
		public abstract int ERR_CantReadRulesetFile { get; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000EBA RID: 3770
		public abstract int ERR_CompileCancelled { get; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000EBB RID: 3771
		public abstract int ERR_BadSourceCodeKind { get; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000EBC RID: 3772
		public abstract int ERR_BadDocumentationMode { get; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000EBD RID: 3773
		public abstract int ERR_BadCompilationOptionValue { get; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000EBE RID: 3774
		public abstract int ERR_MutuallyExclusiveOptions { get; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000EBF RID: 3775
		public abstract int ERR_InvalidDebugInformationFormat { get; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000EC0 RID: 3776
		public abstract int ERR_InvalidFileAlignment { get; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000EC1 RID: 3777
		public abstract int ERR_InvalidSubsystemVersion { get; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000EC2 RID: 3778
		public abstract int ERR_InvalidOutputName { get; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000EC3 RID: 3779
		public abstract int ERR_InvalidInstrumentationKind { get; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000EC4 RID: 3780
		public abstract int ERR_InvalidHashAlgorithmName { get; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000EC5 RID: 3781
		public abstract int ERR_MetadataFileNotAssembly { get; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000EC6 RID: 3782
		public abstract int ERR_MetadataFileNotModule { get; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000EC7 RID: 3783
		public abstract int ERR_InvalidAssemblyMetadata { get; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000EC8 RID: 3784
		public abstract int ERR_InvalidModuleMetadata { get; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000EC9 RID: 3785
		public abstract int ERR_ErrorOpeningAssemblyFile { get; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000ECA RID: 3786
		public abstract int ERR_ErrorOpeningModuleFile { get; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000ECB RID: 3787
		public abstract int ERR_MetadataFileNotFound { get; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000ECC RID: 3788
		public abstract int ERR_MetadataReferencesNotSupported { get; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000ECD RID: 3789
		public abstract int ERR_LinkedNetmoduleMetadataMustProvideFullPEImage { get; }

		// Token: 0x06000ECE RID: 3790
		public abstract void ReportDuplicateMetadataReferenceStrong(DiagnosticBag diagnostics, Location location, MetadataReference reference, AssemblyIdentity identity, MetadataReference equivalentReference, AssemblyIdentity equivalentIdentity);

		// Token: 0x06000ECF RID: 3791
		public abstract void ReportDuplicateMetadataReferenceWeak(DiagnosticBag diagnostics, Location location, MetadataReference reference, AssemblyIdentity identity, MetadataReference equivalentReference, AssemblyIdentity equivalentIdentity);

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000ED0 RID: 3792
		public abstract int ERR_PublicKeyFileFailure { get; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000ED1 RID: 3793
		public abstract int ERR_PublicKeyContainerFailure { get; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000ED2 RID: 3794
		public abstract int ERR_OptionMustBeAbsolutePath { get; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000ED3 RID: 3795
		public abstract int ERR_CantReadResource { get; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000ED4 RID: 3796
		public abstract int ERR_CantOpenWin32Resource { get; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000ED5 RID: 3797
		public abstract int ERR_CantOpenWin32Manifest { get; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000ED6 RID: 3798
		public abstract int ERR_CantOpenWin32Icon { get; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000ED7 RID: 3799
		public abstract int ERR_BadWin32Resource { get; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000ED8 RID: 3800
		public abstract int ERR_ErrorBuildingWin32Resource { get; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000ED9 RID: 3801
		public abstract int ERR_ResourceNotUnique { get; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000EDA RID: 3802
		public abstract int ERR_ResourceFileNameNotUnique { get; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000EDB RID: 3803
		public abstract int ERR_ResourceInModule { get; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000EDC RID: 3804
		public abstract int ERR_PermissionSetAttributeFileReadError { get; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000EDD RID: 3805
		public abstract int ERR_EncodinglessSyntaxTree { get; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000EDE RID: 3806
		public abstract int WRN_PdbUsingNameTooLong { get; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000EDF RID: 3807
		public abstract int WRN_PdbLocalNameTooLong { get; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000EE0 RID: 3808
		public abstract int ERR_PdbWritingFailed { get; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000EE1 RID: 3809
		public abstract int ERR_MetadataNameTooLong { get; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000EE2 RID: 3810
		public abstract int ERR_EncReferenceToAddedMember { get; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000EE3 RID: 3811
		public abstract int ERR_TooManyUserStrings { get; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000EE4 RID: 3812
		public abstract int ERR_PeWritingFailure { get; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000EE5 RID: 3813
		public abstract int ERR_ModuleEmitFailure { get; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000EE6 RID: 3814
		public abstract int ERR_EncUpdateFailedMissingAttribute { get; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000EE7 RID: 3815
		public abstract int ERR_InvalidDebugInfo { get; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000EE8 RID: 3816
		public abstract int WRN_GeneratorFailedDuringInitialization { get; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000EE9 RID: 3817
		public abstract int WRN_GeneratorFailedDuringGeneration { get; }

		// Token: 0x06000EEA RID: 3818 RVA: 0x0002C737 File Offset: 0x0002A937
		public void ReportStreamWriteException(Exception e, string filePath, DiagnosticBag diagnostics)
		{
			diagnostics.Add(this.CreateDiagnostic(this.ERR_OutputWriteFailed, Location.None, new object[] { filePath, e.Message }));
		}

		// Token: 0x06000EEB RID: 3819
		protected abstract void ReportInvalidAttributeArgument(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, int parameterIndex, AttributeData attribute);

		// Token: 0x06000EEC RID: 3820 RVA: 0x0002C764 File Offset: 0x0002A964
		public void ReportInvalidAttributeArgument(BindingDiagnosticBag diagnostics, SyntaxNode attributeSyntax, int parameterIndex, AttributeData attribute)
		{
			DiagnosticBag diagnosticBag = diagnostics.DiagnosticBag;
			if (diagnosticBag != null)
			{
				this.ReportInvalidAttributeArgument(diagnosticBag, attributeSyntax, parameterIndex, attribute);
			}
		}

		// Token: 0x06000EED RID: 3821
		protected abstract void ReportInvalidNamedArgument(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, int namedArgumentIndex, ITypeSymbol attributeClass, string parameterName);

		// Token: 0x06000EEE RID: 3822 RVA: 0x0002C788 File Offset: 0x0002A988
		public void ReportInvalidNamedArgument(BindingDiagnosticBag diagnostics, SyntaxNode attributeSyntax, int namedArgumentIndex, ITypeSymbol attributeClass, string parameterName)
		{
			DiagnosticBag diagnosticBag = diagnostics.DiagnosticBag;
			if (diagnosticBag != null)
			{
				this.ReportInvalidNamedArgument(diagnosticBag, attributeSyntax, namedArgumentIndex, attributeClass, parameterName);
			}
		}

		// Token: 0x06000EEF RID: 3823
		protected abstract void ReportParameterNotValidForType(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, int namedArgumentIndex);

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0002C7AC File Offset: 0x0002A9AC
		public void ReportParameterNotValidForType(BindingDiagnosticBag diagnostics, SyntaxNode attributeSyntax, int namedArgumentIndex)
		{
			DiagnosticBag diagnosticBag = diagnostics.DiagnosticBag;
			if (diagnosticBag != null)
			{
				this.ReportParameterNotValidForType(diagnosticBag, attributeSyntax, namedArgumentIndex);
			}
		}

		// Token: 0x06000EF1 RID: 3825
		protected abstract void ReportMarshalUnmanagedTypeNotValidForFields(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, int parameterIndex, string unmanagedTypeName, AttributeData attribute);

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0002C7CC File Offset: 0x0002A9CC
		public void ReportMarshalUnmanagedTypeNotValidForFields(BindingDiagnosticBag diagnostics, SyntaxNode attributeSyntax, int parameterIndex, string unmanagedTypeName, AttributeData attribute)
		{
			DiagnosticBag diagnosticBag = diagnostics.DiagnosticBag;
			if (diagnosticBag != null)
			{
				this.ReportMarshalUnmanagedTypeNotValidForFields(diagnosticBag, attributeSyntax, parameterIndex, unmanagedTypeName, attribute);
			}
		}

		// Token: 0x06000EF3 RID: 3827
		protected abstract void ReportMarshalUnmanagedTypeOnlyValidForFields(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, int parameterIndex, string unmanagedTypeName, AttributeData attribute);

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0002C7F0 File Offset: 0x0002A9F0
		public void ReportMarshalUnmanagedTypeOnlyValidForFields(BindingDiagnosticBag diagnostics, SyntaxNode attributeSyntax, int parameterIndex, string unmanagedTypeName, AttributeData attribute)
		{
			DiagnosticBag diagnosticBag = diagnostics.DiagnosticBag;
			if (diagnosticBag != null)
			{
				this.ReportMarshalUnmanagedTypeOnlyValidForFields(diagnosticBag, attributeSyntax, parameterIndex, unmanagedTypeName, attribute);
			}
		}

		// Token: 0x06000EF5 RID: 3829
		protected abstract void ReportAttributeParameterRequired(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, string parameterName);

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0002C814 File Offset: 0x0002AA14
		public void ReportAttributeParameterRequired(BindingDiagnosticBag diagnostics, SyntaxNode attributeSyntax, string parameterName)
		{
			DiagnosticBag diagnosticBag = diagnostics.DiagnosticBag;
			if (diagnosticBag != null)
			{
				this.ReportAttributeParameterRequired(diagnosticBag, attributeSyntax, parameterName);
			}
		}

		// Token: 0x06000EF7 RID: 3831
		protected abstract void ReportAttributeParameterRequired(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, string parameterName1, string parameterName2);

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0002C834 File Offset: 0x0002AA34
		public void ReportAttributeParameterRequired(BindingDiagnosticBag diagnostics, SyntaxNode attributeSyntax, string parameterName1, string parameterName2)
		{
			DiagnosticBag diagnosticBag = diagnostics.DiagnosticBag;
			if (diagnosticBag != null)
			{
				this.ReportAttributeParameterRequired(diagnosticBag, attributeSyntax, parameterName1, parameterName2);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000EF9 RID: 3833
		public abstract int ERR_BadAssemblyName { get; }

		// Token: 0x04000424 RID: 1060
		[TupleElementNames(new string[] { "prefix", "code" })]
		[Nullable(new byte[] { 1, 0, 1, 1 })]
		private static readonly ConcurrentDictionary<ValueTuple<string, int>, string> s_errorIdCache = new ConcurrentDictionary<ValueTuple<string, int>, string>();
	}
}
