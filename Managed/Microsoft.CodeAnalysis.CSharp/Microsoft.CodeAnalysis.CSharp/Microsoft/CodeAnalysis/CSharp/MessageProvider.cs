using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000142 RID: 322
	internal sealed class MessageProvider : CommonMessageProvider, IObjectWritable
	{
		// Token: 0x0600192E RID: 6446 RVA: 0x00087AFA File Offset: 0x00085CFA
		static MessageProvider()
		{
			ObjectBinder.RegisterTypeReader(typeof(MessageProvider), (ObjectReader r) => MessageProvider.Instance);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00087B25 File Offset: 0x00085D25
		private MessageProvider()
		{
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x00087B2D File Offset: 0x00085D2D
		bool IObjectWritable.ShouldReuseInSerialization
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00087B30 File Offset: 0x00085D30
		void IObjectWritable.WriteTo(ObjectWriter writer)
		{
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00087B32 File Offset: 0x00085D32
		public override DiagnosticSeverity GetSeverity(int code)
		{
			return ErrorFacts.GetSeverity((ErrorCode)code);
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00087B3A File Offset: 0x00085D3A
		public override string LoadMessage(int code, CultureInfo language)
		{
			return ErrorFacts.GetMessage((ErrorCode)code, language);
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00087B43 File Offset: 0x00085D43
		public override LocalizableString GetMessageFormat(int code)
		{
			return ErrorFacts.GetMessageFormat((ErrorCode)code);
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00087B4B File Offset: 0x00085D4B
		public override LocalizableString GetDescription(int code)
		{
			return ErrorFacts.GetDescription((ErrorCode)code);
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00087B53 File Offset: 0x00085D53
		public override LocalizableString GetTitle(int code)
		{
			return ErrorFacts.GetTitle((ErrorCode)code);
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00087B5B File Offset: 0x00085D5B
		public override string GetHelpLink(int code)
		{
			return ErrorFacts.GetHelpLink((ErrorCode)code);
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00087B63 File Offset: 0x00085D63
		public override string GetCategory(int code)
		{
			return ErrorFacts.GetCategory((ErrorCode)code);
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x00087B6B File Offset: 0x00085D6B
		public override string CodePrefix
		{
			get
			{
				return "CS";
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00087B72 File Offset: 0x00085D72
		public override string GetMessagePrefix(string id, DiagnosticSeverity severity, bool isWarningAsError, CultureInfo culture)
		{
			return string.Format(culture, "{0} {1}", (severity == DiagnosticSeverity.Error || isWarningAsError) ? "error" : "warning", id);
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00087B95 File Offset: 0x00085D95
		public override int GetWarningLevel(int code)
		{
			return ErrorFacts.GetWarningLevel((ErrorCode)code);
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x00087B9D File Offset: 0x00085D9D
		public override Type ErrorCodeType
		{
			get
			{
				return typeof(ErrorCode);
			}
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00087BA9 File Offset: 0x00085DA9
		public override Diagnostic CreateDiagnostic(int code, Location location, params object[] args)
		{
			return new CSDiagnostic(new CSDiagnosticInfo((ErrorCode)code, args, ImmutableArray<Symbol>.Empty, ImmutableArray<Location>.Empty), location, false);
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00087BC3 File Offset: 0x00085DC3
		public override Diagnostic CreateDiagnostic(DiagnosticInfo info)
		{
			return new CSDiagnostic(info, Location.None, false);
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00087BD1 File Offset: 0x00085DD1
		public override string GetErrorDisplayString(ISymbol symbol)
		{
			if (symbol.Kind == SymbolKind.Assembly || symbol.Kind == SymbolKind.Namespace)
			{
				return symbol.ToString();
			}
			return SymbolDisplay.ToDisplayString(symbol, SymbolDisplayFormat.CSharpShortErrorMessageFormat);
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00087BF8 File Offset: 0x00085DF8
		public override ReportDiagnostic GetDiagnosticReport(DiagnosticInfo diagnosticInfo, CompilationOptions options)
		{
			bool flag;
			return CSharpDiagnosticFilter.GetDiagnosticReport(diagnosticInfo.Severity, true, diagnosticInfo.MessageIdentifier, diagnosticInfo.WarningLevel, Location.None, diagnosticInfo.Category, options.WarningLevel, ((CSharpCompilationOptions)options).NullableContextOptions, options.GeneralDiagnosticOption, options.SpecificDiagnosticOptions, options.SyntaxTreeOptionsProvider, CancellationToken.None, out flag);
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x00087C52 File Offset: 0x00085E52
		public override int ERR_FailedToCreateTempFile
		{
			get
			{
				return 1619;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x00087C59 File Offset: 0x00085E59
		public override int ERR_MultipleAnalyzerConfigsInSameDir
		{
			get
			{
				return 8700;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x00087C60 File Offset: 0x00085E60
		public override int ERR_ExpectedSingleScript
		{
			get
			{
				return 7018;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x00087C67 File Offset: 0x00085E67
		public override int ERR_OpenResponseFile
		{
			get
			{
				return 2011;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x00087C6E File Offset: 0x00085E6E
		public override int ERR_InvalidPathMap
		{
			get
			{
				return 8101;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x00087C75 File Offset: 0x00085E75
		public override int FTL_InvalidInputFileName
		{
			get
			{
				return 2021;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x00087C7C File Offset: 0x00085E7C
		public override int ERR_FileNotFound
		{
			get
			{
				return 2001;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x00087C83 File Offset: 0x00085E83
		public override int ERR_NoSourceFile
		{
			get
			{
				return 1504;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x00087C8A File Offset: 0x00085E8A
		public override int ERR_CantOpenFileWrite
		{
			get
			{
				return 2012;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x00087C91 File Offset: 0x00085E91
		public override int ERR_OutputWriteFailed
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x00087C95 File Offset: 0x00085E95
		public override int WRN_NoConfigNotOnCommandLine
		{
			get
			{
				return 2023;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x00087C9C File Offset: 0x00085E9C
		public override int ERR_BinaryFile
		{
			get
			{
				return 2015;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x00087CA3 File Offset: 0x00085EA3
		public override int WRN_AnalyzerCannotBeCreated
		{
			get
			{
				return 8032;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x00087CAA File Offset: 0x00085EAA
		public override int WRN_NoAnalyzerInAssembly
		{
			get
			{
				return 8033;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x00087CB1 File Offset: 0x00085EB1
		public override int WRN_UnableToLoadAnalyzer
		{
			get
			{
				return 8034;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x00087CB8 File Offset: 0x00085EB8
		public override int WRN_AnalyzerReferencesFramework
		{
			get
			{
				return 8850;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x00087CBF File Offset: 0x00085EBF
		public override int INF_UnableToLoadSomeTypesInAnalyzer
		{
			get
			{
				return 8040;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001952 RID: 6482 RVA: 0x00087CC6 File Offset: 0x00085EC6
		public override int ERR_CantReadRulesetFile
		{
			get
			{
				return 8035;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x00087CCD File Offset: 0x00085ECD
		public override int ERR_CompileCancelled
		{
			get
			{
				return 1600;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x00087CD4 File Offset: 0x00085ED4
		public override int ERR_BadSourceCodeKind
		{
			get
			{
				return 8190;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x00087CDB File Offset: 0x00085EDB
		public override int ERR_BadDocumentationMode
		{
			get
			{
				return 8191;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x00087CE2 File Offset: 0x00085EE2
		public override int ERR_BadCompilationOptionValue
		{
			get
			{
				return 7088;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x00087CE9 File Offset: 0x00085EE9
		public override int ERR_MutuallyExclusiveOptions
		{
			get
			{
				return 7102;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001958 RID: 6488 RVA: 0x00087CF0 File Offset: 0x00085EF0
		public override int ERR_InvalidDebugInformationFormat
		{
			get
			{
				return 2042;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x00087CF7 File Offset: 0x00085EF7
		public override int ERR_InvalidOutputName
		{
			get
			{
				return 2041;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x00087CFE File Offset: 0x00085EFE
		public override int ERR_InvalidFileAlignment
		{
			get
			{
				return 2024;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x00087D05 File Offset: 0x00085F05
		public override int ERR_InvalidSubsystemVersion
		{
			get
			{
				return 1773;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x00087D0C File Offset: 0x00085F0C
		public override int ERR_InvalidInstrumentationKind
		{
			get
			{
				return 8111;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x00087D13 File Offset: 0x00085F13
		public override int ERR_InvalidHashAlgorithmName
		{
			get
			{
				return 8113;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x00087D1A File Offset: 0x00085F1A
		public override int ERR_MetadataFileNotAssembly
		{
			get
			{
				return 1509;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x00087D21 File Offset: 0x00085F21
		public override int ERR_MetadataFileNotModule
		{
			get
			{
				return 1542;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x00087D28 File Offset: 0x00085F28
		public override int ERR_InvalidAssemblyMetadata
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x00087D2C File Offset: 0x00085F2C
		public override int ERR_InvalidModuleMetadata
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x00087D30 File Offset: 0x00085F30
		public override int ERR_ErrorOpeningAssemblyFile
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x00087D34 File Offset: 0x00085F34
		public override int ERR_ErrorOpeningModuleFile
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x00087D38 File Offset: 0x00085F38
		public override int ERR_MetadataFileNotFound
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001965 RID: 6501 RVA: 0x00087D3B File Offset: 0x00085F3B
		public override int ERR_MetadataReferencesNotSupported
		{
			get
			{
				return 7099;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x00087D42 File Offset: 0x00085F42
		public override int ERR_LinkedNetmoduleMetadataMustProvideFullPEImage
		{
			get
			{
				return 7098;
			}
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00087D49 File Offset: 0x00085F49
		public override void ReportDuplicateMetadataReferenceStrong(DiagnosticBag diagnostics, Location location, MetadataReference reference, AssemblyIdentity identity, MetadataReference equivalentReference, AssemblyIdentity equivalentIdentity)
		{
			diagnostics.Add(ErrorCode.ERR_DuplicateImport, location, new object[]
			{
				reference.Display ?? identity.GetDisplayName(false),
				equivalentReference.Display ?? equivalentIdentity.GetDisplayName(false)
			});
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00087D89 File Offset: 0x00085F89
		public override void ReportDuplicateMetadataReferenceWeak(DiagnosticBag diagnostics, Location location, MetadataReference reference, AssemblyIdentity identity, MetadataReference equivalentReference, AssemblyIdentity equivalentIdentity)
		{
			diagnostics.Add(ErrorCode.ERR_DuplicateImportSimple, location, new object[]
			{
				identity.Name,
				reference.Display ?? identity.GetDisplayName(false)
			});
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x00087DBD File Offset: 0x00085FBD
		public override int ERR_PublicKeyFileFailure
		{
			get
			{
				return 7027;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x00087DC4 File Offset: 0x00085FC4
		public override int ERR_PublicKeyContainerFailure
		{
			get
			{
				return 7028;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600196B RID: 6507 RVA: 0x00087DCB File Offset: 0x00085FCB
		public override int ERR_OptionMustBeAbsolutePath
		{
			get
			{
				return 8106;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600196C RID: 6508 RVA: 0x00087DD2 File Offset: 0x00085FD2
		public override int ERR_CantReadResource
		{
			get
			{
				return 1566;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x00087DD9 File Offset: 0x00085FD9
		public override int ERR_CantOpenWin32Resource
		{
			get
			{
				return 1719;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x00087DE0 File Offset: 0x00085FE0
		public override int ERR_CantOpenWin32Manifest
		{
			get
			{
				return 1926;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x00087DE7 File Offset: 0x00085FE7
		public override int ERR_CantOpenWin32Icon
		{
			get
			{
				return 7064;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x00087DEE File Offset: 0x00085FEE
		public override int ERR_ErrorBuildingWin32Resource
		{
			get
			{
				return 7065;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x00087DF5 File Offset: 0x00085FF5
		public override int ERR_BadWin32Resource
		{
			get
			{
				return 1583;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x00087DFC File Offset: 0x00085FFC
		public override int ERR_ResourceFileNameNotUnique
		{
			get
			{
				return 7041;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x00087E03 File Offset: 0x00086003
		public override int ERR_ResourceNotUnique
		{
			get
			{
				return 1508;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x00087E0A File Offset: 0x0008600A
		public override int ERR_ResourceInModule
		{
			get
			{
				return 1507;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x00087E11 File Offset: 0x00086011
		public override int ERR_PermissionSetAttributeFileReadError
		{
			get
			{
				return 7057;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x00087E18 File Offset: 0x00086018
		public override int ERR_EncodinglessSyntaxTree
		{
			get
			{
				return 8055;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001977 RID: 6519 RVA: 0x00087E1F File Offset: 0x0008601F
		public override int WRN_PdbUsingNameTooLong
		{
			get
			{
				return 811;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x00087E26 File Offset: 0x00086026
		public override int WRN_PdbLocalNameTooLong
		{
			get
			{
				return 8029;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x00087E2D File Offset: 0x0008602D
		public override int ERR_PdbWritingFailed
		{
			get
			{
				return 41;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600197A RID: 6522 RVA: 0x00087E31 File Offset: 0x00086031
		public override int ERR_MetadataNameTooLong
		{
			get
			{
				return 7013;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600197B RID: 6523 RVA: 0x00087E38 File Offset: 0x00086038
		public override int ERR_EncReferenceToAddedMember
		{
			get
			{
				return 7101;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600197C RID: 6524 RVA: 0x00087E3F File Offset: 0x0008603F
		public override int ERR_TooManyUserStrings
		{
			get
			{
				return 8103;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600197D RID: 6525 RVA: 0x00087E46 File Offset: 0x00086046
		public override int ERR_PeWritingFailure
		{
			get
			{
				return 8104;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x00087E4D File Offset: 0x0008604D
		public override int ERR_ModuleEmitFailure
		{
			get
			{
				return 7038;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x00087E54 File Offset: 0x00086054
		public override int ERR_EncUpdateFailedMissingAttribute
		{
			get
			{
				return 7043;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x00087E5B File Offset: 0x0008605B
		public override int ERR_InvalidDebugInfo
		{
			get
			{
				return 7103;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x00087E62 File Offset: 0x00086062
		public override int WRN_GeneratorFailedDuringInitialization
		{
			get
			{
				return 8784;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x00087E69 File Offset: 0x00086069
		public override int WRN_GeneratorFailedDuringGeneration
		{
			get
			{
				return 8785;
			}
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x00087E70 File Offset: 0x00086070
		protected override void ReportInvalidAttributeArgument(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, int parameterIndex, AttributeData attribute)
		{
			AttributeSyntax attributeSyntax2 = (AttributeSyntax)attributeSyntax;
			CSharpSyntaxNode attributeArgumentSyntax = attribute.GetAttributeArgumentSyntax(parameterIndex, attributeSyntax2);
			diagnostics.Add(ErrorCode.ERR_InvalidAttributeArgument, attributeArgumentSyntax.Location, new object[] { attributeSyntax2.GetErrorDisplayName() });
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x00087EB0 File Offset: 0x000860B0
		protected override void ReportInvalidNamedArgument(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, int namedArgumentIndex, ITypeSymbol attributeClass, string parameterName)
		{
			AttributeSyntax attributeSyntax2 = (AttributeSyntax)attributeSyntax;
			diagnostics.Add(ErrorCode.ERR_InvalidNamedArgument, attributeSyntax2.ArgumentList.Arguments[namedArgumentIndex].Location, new object[] { parameterName });
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00087EF4 File Offset: 0x000860F4
		protected override void ReportParameterNotValidForType(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, int namedArgumentIndex)
		{
			AttributeSyntax attributeSyntax2 = (AttributeSyntax)attributeSyntax;
			diagnostics.Add(ErrorCode.ERR_ParameterNotValidForType, attributeSyntax2.ArgumentList.Arguments[namedArgumentIndex].Location);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x00087F30 File Offset: 0x00086130
		protected override void ReportMarshalUnmanagedTypeNotValidForFields(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, int parameterIndex, string unmanagedTypeName, AttributeData attribute)
		{
			AttributeSyntax attributeSyntax2 = (AttributeSyntax)attributeSyntax;
			CSharpSyntaxNode attributeArgumentSyntax = attribute.GetAttributeArgumentSyntax(parameterIndex, attributeSyntax2);
			diagnostics.Add(ErrorCode.ERR_MarshalUnmanagedTypeNotValidForFields, attributeArgumentSyntax.Location, new object[] { unmanagedTypeName });
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00087F6C File Offset: 0x0008616C
		protected override void ReportMarshalUnmanagedTypeOnlyValidForFields(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, int parameterIndex, string unmanagedTypeName, AttributeData attribute)
		{
			AttributeSyntax attributeSyntax2 = (AttributeSyntax)attributeSyntax;
			CSharpSyntaxNode attributeArgumentSyntax = attribute.GetAttributeArgumentSyntax(parameterIndex, attributeSyntax2);
			diagnostics.Add(ErrorCode.ERR_MarshalUnmanagedTypeOnlyValidForFields, attributeArgumentSyntax.Location, new object[] { unmanagedTypeName });
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x00087FA8 File Offset: 0x000861A8
		protected override void ReportAttributeParameterRequired(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, string parameterName)
		{
			AttributeSyntax attributeSyntax2 = (AttributeSyntax)attributeSyntax;
			diagnostics.Add(ErrorCode.ERR_AttributeParameterRequired1, attributeSyntax2.Name.Location, new object[] { parameterName });
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00087FE0 File Offset: 0x000861E0
		protected override void ReportAttributeParameterRequired(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, string parameterName1, string parameterName2)
		{
			AttributeSyntax attributeSyntax2 = (AttributeSyntax)attributeSyntax;
			diagnostics.Add(ErrorCode.ERR_AttributeParameterRequired2, attributeSyntax2.Name.Location, new object[] { parameterName1, parameterName2 });
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600198A RID: 6538 RVA: 0x0008801A File Offset: 0x0008621A
		public override int ERR_BadAssemblyName
		{
			get
			{
				return 8203;
			}
		}

		// Token: 0x04000DCF RID: 3535
		public static readonly MessageProvider Instance = new MessageProvider();
	}
}
