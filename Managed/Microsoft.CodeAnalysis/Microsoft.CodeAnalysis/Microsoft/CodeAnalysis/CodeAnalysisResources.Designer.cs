using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000298 RID: 664
	internal static class CodeAnalysisResources
	{
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x000525B9 File Offset: 0x000507B9
		internal static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager resourceManager;
				if ((resourceManager = CodeAnalysisResources.s_resourceManager) == null)
				{
					resourceManager = (CodeAnalysisResources.s_resourceManager = new ResourceManager(typeof(CodeAnalysisResources)));
				}
				return resourceManager;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x000525D9 File Offset: 0x000507D9
		// (set) Token: 0x06001B40 RID: 6976 RVA: 0x000525E0 File Offset: 0x000507E0
		internal static CultureInfo Culture { get; set; }

		// Token: 0x06001B41 RID: 6977 RVA: 0x000525E8 File Offset: 0x000507E8
		[MethodImpl(256)]
		internal static string GetResourceString(string resourceKey, string defaultValue = null)
		{
			return CodeAnalysisResources.ResourceManager.GetString(resourceKey, CodeAnalysisResources.Culture);
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x000525FA File Offset: 0x000507FA
		internal static string OutputKindNotSupported
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("OutputKindNotSupported", null);
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x00052607 File Offset: 0x00050807
		internal static string PathReturnedByResolveMetadataFileMustBeAbsolute
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("PathReturnedByResolveMetadataFileMustBeAbsolute", null);
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x00052614 File Offset: 0x00050814
		internal static string AssemblyMustHaveAtLeastOneModule
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("AssemblyMustHaveAtLeastOneModule", null);
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x00052621 File Offset: 0x00050821
		internal static string ModuleCopyCannotBeUsedToCreateAssemblyMetadata
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ModuleCopyCannotBeUsedToCreateAssemblyMetadata", null);
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x0005262E File Offset: 0x0005082E
		internal static string Unresolved
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Unresolved", null);
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x0005263B File Offset: 0x0005083B
		internal static string Assembly
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Assembly", null);
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x00052648 File Offset: 0x00050848
		internal static string Class1
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Class1", null);
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x00052655 File Offset: 0x00050855
		internal static string Constructor
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Constructor", null);
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001B4A RID: 6986 RVA: 0x00052662 File Offset: 0x00050862
		internal static string Delegate1
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Delegate1", null);
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001B4B RID: 6987 RVA: 0x0005266F File Offset: 0x0005086F
		internal static string Enum1
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Enum1", null);
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x0005267C File Offset: 0x0005087C
		internal static string Event1
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Event1", null);
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x00052689 File Offset: 0x00050889
		internal static string Field
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Field", null);
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x00052696 File Offset: 0x00050896
		internal static string TypeParameter
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("TypeParameter", null);
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x000526A3 File Offset: 0x000508A3
		internal static string Interface1
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Interface1", null);
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x000526B0 File Offset: 0x000508B0
		internal static string Method
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Method", null);
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x000526BD File Offset: 0x000508BD
		internal static string Module
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Module", null);
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x000526CA File Offset: 0x000508CA
		internal static string Parameter
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Parameter", null);
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x000526D7 File Offset: 0x000508D7
		internal static string Property
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Property", null);
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x000526E4 File Offset: 0x000508E4
		internal static string Return1
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Return1", null);
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x000526F1 File Offset: 0x000508F1
		internal static string Struct1
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Struct1", null);
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x000526FE File Offset: 0x000508FE
		internal static string CannotCreateReferenceToSubmission
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("CannotCreateReferenceToSubmission", null);
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x0005270B File Offset: 0x0005090B
		internal static string CannotCreateReferenceToModule
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("CannotCreateReferenceToModule", null);
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x00052718 File Offset: 0x00050918
		internal static string InMemoryAssembly
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InMemoryAssembly", null);
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x00052725 File Offset: 0x00050925
		internal static string InMemoryModule
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InMemoryModule", null);
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x00052732 File Offset: 0x00050932
		internal static string SizeHasToBePositive
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("SizeHasToBePositive", null);
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x0005273F File Offset: 0x0005093F
		internal static string AssemblyFileNotFound
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("AssemblyFileNotFound", null);
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x0005274C File Offset: 0x0005094C
		internal static string CannotEmbedInteropTypesFromModule
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("CannotEmbedInteropTypesFromModule", null);
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x00052759 File Offset: 0x00050959
		internal static string CannotAliasModule
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("CannotAliasModule", null);
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001B5E RID: 7006 RVA: 0x00052766 File Offset: 0x00050966
		internal static string InvalidAlias
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidAlias", null);
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x00052773 File Offset: 0x00050973
		internal static string GetMetadataMustReturnInstance
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("GetMetadataMustReturnInstance", null);
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x00052780 File Offset: 0x00050980
		internal static string Value_too_large_to_be_represented_as_a_30_bit_unsigned_integer
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Value_too_large_to_be_represented_as_a_30_bit_unsigned_integer", null);
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x0005278D File Offset: 0x0005098D
		internal static string Arrays_with_more_than_one_dimension_cannot_be_serialized
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Arrays_with_more_than_one_dimension_cannot_be_serialized", null);
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x0005279A File Offset: 0x0005099A
		internal static string InvalidAssemblyName
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidAssemblyName", null);
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x000527A7 File Offset: 0x000509A7
		internal static string AbsolutePathExpected
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("AbsolutePathExpected", null);
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x000527B4 File Offset: 0x000509B4
		internal static string EmptyKeyInPathMap
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("EmptyKeyInPathMap", null);
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x000527C1 File Offset: 0x000509C1
		internal static string NullValueInPathMap
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("NullValueInPathMap", null);
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x000527CE File Offset: 0x000509CE
		internal static string CompilationOptionsMustNotHaveErrors
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("CompilationOptionsMustNotHaveErrors", null);
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x000527DB File Offset: 0x000509DB
		internal static string ReturnTypeCannotBeValuePointerbyRefOrOpen
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ReturnTypeCannotBeValuePointerbyRefOrOpen", null);
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x000527E8 File Offset: 0x000509E8
		internal static string ReturnTypeCannotBeVoidByRefOrOpen
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ReturnTypeCannotBeVoidByRefOrOpen", null);
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x000527F5 File Offset: 0x000509F5
		internal static string TypeMustBeSameAsHostObjectTypeOfPreviousSubmission
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("TypeMustBeSameAsHostObjectTypeOfPreviousSubmission", null);
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x00052802 File Offset: 0x00050A02
		internal static string PreviousSubmissionHasErrors
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("PreviousSubmissionHasErrors", null);
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x0005280F File Offset: 0x00050A0F
		internal static string InvalidOutputKindForSubmission
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidOutputKindForSubmission", null);
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x0005281C File Offset: 0x00050A1C
		internal static string InvalidCompilationOptions
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidCompilationOptions", null);
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x00052829 File Offset: 0x00050A29
		internal static string ResourceStreamProviderShouldReturnNonNullStream
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ResourceStreamProviderShouldReturnNonNullStream", null);
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x00052836 File Offset: 0x00050A36
		internal static string ReferenceResolverShouldReturnReadableNonNullStream
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ReferenceResolverShouldReturnReadableNonNullStream", null);
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x00052843 File Offset: 0x00050A43
		internal static string EmptyOrInvalidResourceName
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("EmptyOrInvalidResourceName", null);
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x00052850 File Offset: 0x00050A50
		internal static string EmptyOrInvalidFileName
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("EmptyOrInvalidFileName", null);
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x0005285D File Offset: 0x00050A5D
		internal static string ResourceDataProviderShouldReturnNonNullStream
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ResourceDataProviderShouldReturnNonNullStream", null);
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x0005286A File Offset: 0x00050A6A
		internal static string FileNotFound
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("FileNotFound", null);
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x00052877 File Offset: 0x00050A77
		internal static string PathReturnedByResolveStrongNameKeyFileMustBeAbsolute
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("PathReturnedByResolveStrongNameKeyFileMustBeAbsolute", null);
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x00052884 File Offset: 0x00050A84
		internal static string TypeMustBeASubclassOfSyntaxAnnotation
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("TypeMustBeASubclassOfSyntaxAnnotation", null);
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x00052891 File Offset: 0x00050A91
		internal static string InvalidModuleName
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidModuleName", null);
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x0005289E File Offset: 0x00050A9E
		internal static string FileSizeExceedsMaximumAllowed
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("FileSizeExceedsMaximumAllowed", null);
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x000528AB File Offset: 0x00050AAB
		internal static string NameCannotBeNull
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("NameCannotBeNull", null);
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x000528B8 File Offset: 0x00050AB8
		internal static string NameCannotBeEmpty
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("NameCannotBeEmpty", null);
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x000528C5 File Offset: 0x00050AC5
		internal static string NameCannotStartWithWhitespace
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("NameCannotStartWithWhitespace", null);
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x000528D2 File Offset: 0x00050AD2
		internal static string NameContainsInvalidCharacter
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("NameContainsInvalidCharacter", null);
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x000528DF File Offset: 0x00050ADF
		internal static string SpanDoesNotIncludeStartOfLine
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("SpanDoesNotIncludeStartOfLine", null);
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001B7C RID: 7036 RVA: 0x000528EC File Offset: 0x00050AEC
		internal static string SpanDoesNotIncludeEndOfLine
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("SpanDoesNotIncludeEndOfLine", null);
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x000528F9 File Offset: 0x00050AF9
		internal static string StartMustNotBeNegative
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("StartMustNotBeNegative", null);
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x00052906 File Offset: 0x00050B06
		internal static string EndMustNotBeLessThanStart
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("EndMustNotBeLessThanStart", null);
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x00052913 File Offset: 0x00050B13
		internal static string InvalidContentType
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidContentType", null);
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x00052920 File Offset: 0x00050B20
		internal static string ExpectedNonEmptyPublicKey
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ExpectedNonEmptyPublicKey", null);
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x0005292D File Offset: 0x00050B2D
		internal static string InvalidSizeOfPublicKeyToken
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidSizeOfPublicKeyToken", null);
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x0005293A File Offset: 0x00050B3A
		internal static string InvalidCharactersInAssemblyName
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidCharactersInAssemblyName", null);
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x00052947 File Offset: 0x00050B47
		internal static string InvalidCharactersInAssemblyCultureName
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidCharactersInAssemblyCultureName", null);
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x00052954 File Offset: 0x00050B54
		internal static string StreamMustSupportReadAndSeek
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("StreamMustSupportReadAndSeek", null);
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x00052961 File Offset: 0x00050B61
		internal static string StreamMustSupportRead
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("StreamMustSupportRead", null);
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x0005296E File Offset: 0x00050B6E
		internal static string StreamMustSupportWrite
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("StreamMustSupportWrite", null);
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x0005297B File Offset: 0x00050B7B
		internal static string PdbStreamUnexpectedWhenEmbedding
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("PdbStreamUnexpectedWhenEmbedding", null);
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x00052988 File Offset: 0x00050B88
		internal static string PdbStreamUnexpectedWhenEmittingMetadataOnly
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("PdbStreamUnexpectedWhenEmittingMetadataOnly", null);
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x00052995 File Offset: 0x00050B95
		internal static string MetadataPeStreamUnexpectedWhenEmittingMetadataOnly
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("MetadataPeStreamUnexpectedWhenEmittingMetadataOnly", null);
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x000529A2 File Offset: 0x00050BA2
		internal static string IncludingPrivateMembersUnexpectedWhenEmittingToMetadataPeStream
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("IncludingPrivateMembersUnexpectedWhenEmittingToMetadataPeStream", null);
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x000529AF File Offset: 0x00050BAF
		internal static string MustIncludePrivateMembersUnlessRefAssembly
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("MustIncludePrivateMembersUnlessRefAssembly", null);
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x000529BC File Offset: 0x00050BBC
		internal static string EmbeddingPdbUnexpectedWhenEmittingMetadata
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("EmbeddingPdbUnexpectedWhenEmittingMetadata", null);
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x000529C9 File Offset: 0x00050BC9
		internal static string CannotTargetNetModuleWhenEmittingRefAssembly
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("CannotTargetNetModuleWhenEmittingRefAssembly", null);
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x000529D6 File Offset: 0x00050BD6
		internal static string InvalidHash
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidHash", null);
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001B8F RID: 7055 RVA: 0x000529E3 File Offset: 0x00050BE3
		internal static string UnsupportedHashAlgorithm
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("UnsupportedHashAlgorithm", null);
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x000529F0 File Offset: 0x00050BF0
		internal static string InconsistentLanguageVersions
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InconsistentLanguageVersions", null);
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x000529FD File Offset: 0x00050BFD
		internal static string CoffResourceInvalidRelocation
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("CoffResourceInvalidRelocation", null);
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001B92 RID: 7058 RVA: 0x00052A0A File Offset: 0x00050C0A
		internal static string CoffResourceInvalidSectionSize
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("CoffResourceInvalidSectionSize", null);
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x00052A17 File Offset: 0x00050C17
		internal static string CoffResourceInvalidSymbol
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("CoffResourceInvalidSymbol", null);
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x00052A24 File Offset: 0x00050C24
		internal static string CoffResourceMissingSection
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("CoffResourceMissingSection", null);
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x00052A31 File Offset: 0x00050C31
		internal static string IconStreamUnexpectedFormat
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("IconStreamUnexpectedFormat", null);
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x00052A3E File Offset: 0x00050C3E
		internal static string InvalidCultureName
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidCultureName", null);
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x00052A4B File Offset: 0x00050C4B
		internal static string WinRTIdentityCantBeRetargetable
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("WinRTIdentityCantBeRetargetable", null);
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x00052A58 File Offset: 0x00050C58
		internal static string PEImageNotAvailable
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("PEImageNotAvailable", null);
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x00052A65 File Offset: 0x00050C65
		internal static string AssemblySigningNotSupported
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("AssemblySigningNotSupported", null);
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x00052A72 File Offset: 0x00050C72
		internal static string XmlReferencesNotSupported
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("XmlReferencesNotSupported", null);
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x00052A7F File Offset: 0x00050C7F
		internal static string FailedToResolveRuleSetName
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("FailedToResolveRuleSetName", null);
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x00052A8C File Offset: 0x00050C8C
		internal static string InvalidRuleSetInclude
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidRuleSetInclude", null);
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x00052A99 File Offset: 0x00050C99
		internal static string CompilerAnalyzerFailure
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("CompilerAnalyzerFailure", null);
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x00052AA6 File Offset: 0x00050CA6
		internal static string CompilerAnalyzerThrows
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("CompilerAnalyzerThrows", null);
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x00052AB3 File Offset: 0x00050CB3
		internal static string CompilerAnalyzerThrowsDescription
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("CompilerAnalyzerThrowsDescription", null);
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x00052AC0 File Offset: 0x00050CC0
		internal static string AnalyzerDriverFailure
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("AnalyzerDriverFailure", null);
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x00052ACD File Offset: 0x00050CCD
		internal static string AnalyzerDriverThrows
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("AnalyzerDriverThrows", null);
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x00052ADA File Offset: 0x00050CDA
		internal static string AnalyzerDriverThrowsDescription
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("AnalyzerDriverThrowsDescription", null);
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x00052AE7 File Offset: 0x00050CE7
		internal static string PEImageDoesntContainManagedMetadata
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("PEImageDoesntContainManagedMetadata", null);
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x00052AF4 File Offset: 0x00050CF4
		internal static string ChangesMustNotOverlap
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ChangesMustNotOverlap", null);
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x00052B01 File Offset: 0x00050D01
		internal static string DiagnosticIdCantBeNullOrWhitespace
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("DiagnosticIdCantBeNullOrWhitespace", null);
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x00052B0E File Offset: 0x00050D0E
		internal static string SuppressionIdCantBeNullOrWhitespace
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("SuppressionIdCantBeNullOrWhitespace", null);
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x00052B1B File Offset: 0x00050D1B
		internal static string RuleSetHasDuplicateRules
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("RuleSetHasDuplicateRules", null);
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x00052B28 File Offset: 0x00050D28
		internal static string CantCreateModuleReferenceToAssembly
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("CantCreateModuleReferenceToAssembly", null);
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x00052B35 File Offset: 0x00050D35
		internal static string CantCreateReferenceToDynamicAssembly
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("CantCreateReferenceToDynamicAssembly", null);
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x00052B42 File Offset: 0x00050D42
		internal static string CantCreateReferenceToAssemblyWithoutLocation
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("CantCreateReferenceToAssemblyWithoutLocation", null);
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x00052B4F File Offset: 0x00050D4F
		internal static string ArgumentCannotBeEmpty
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ArgumentCannotBeEmpty", null);
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x00052B5C File Offset: 0x00050D5C
		internal static string ArgumentElementCannotBeNull
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ArgumentElementCannotBeNull", null);
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x00052B69 File Offset: 0x00050D69
		internal static string UnsupportedDiagnosticReported
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("UnsupportedDiagnosticReported", null);
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x00052B76 File Offset: 0x00050D76
		internal static string UnsupportedSuppressionReported
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("UnsupportedSuppressionReported", null);
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x00052B83 File Offset: 0x00050D83
		internal static string InvalidDiagnosticSuppressionReported
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidDiagnosticSuppressionReported", null);
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x00052B90 File Offset: 0x00050D90
		internal static string NonReportedDiagnosticCannotBeSuppressed
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("NonReportedDiagnosticCannotBeSuppressed", null);
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x00052B9D File Offset: 0x00050D9D
		internal static string InvalidDiagnosticIdReported
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidDiagnosticIdReported", null);
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x00052BAA File Offset: 0x00050DAA
		internal static string InvalidDiagnosticLocationReported
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidDiagnosticLocationReported", null);
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x00052BB7 File Offset: 0x00050DB7
		internal static string SupportedDiagnosticsHasNullDescriptor
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("SupportedDiagnosticsHasNullDescriptor", null);
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x00052BC4 File Offset: 0x00050DC4
		internal static string SupportedSuppressionsHasNullDescriptor
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("SupportedSuppressionsHasNullDescriptor", null);
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x00052BD1 File Offset: 0x00050DD1
		internal static string The_type_0_is_not_understood_by_the_serialization_binder
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("The_type_0_is_not_understood_by_the_serialization_binder", null);
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x00052BDE File Offset: 0x00050DDE
		internal static string Cannot_deserialize_type_0
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Cannot_deserialize_type_0", null);
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x00052BEB File Offset: 0x00050DEB
		internal static string Cannot_serialize_type_0
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Cannot_serialize_type_0", null);
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x00052BF8 File Offset: 0x00050DF8
		internal static string InvalidNodeToTrack
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidNodeToTrack", null);
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x00052C05 File Offset: 0x00050E05
		internal static string NodeOrTokenOutOfSequence
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("NodeOrTokenOutOfSequence", null);
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x00052C12 File Offset: 0x00050E12
		internal static string UnexpectedTypeOfNodeInList
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("UnexpectedTypeOfNodeInList", null);
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x00052C1F File Offset: 0x00050E1F
		internal static string MissingListItem
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("MissingListItem", null);
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x00052C2C File Offset: 0x00050E2C
		internal static string InvalidPublicKey
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidPublicKey", null);
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x00052C39 File Offset: 0x00050E39
		internal static string InvalidPublicKeyToken
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidPublicKeyToken", null);
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x00052C46 File Offset: 0x00050E46
		internal static string InvalidDataAtOffset
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidDataAtOffset", null);
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x00052C53 File Offset: 0x00050E53
		internal static string SymWriterNotDeterministic
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("SymWriterNotDeterministic", null);
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x00052C60 File Offset: 0x00050E60
		internal static string SymWriterOlderVersionThanRequired
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("SymWriterOlderVersionThanRequired", null);
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x00052C6D File Offset: 0x00050E6D
		internal static string SymWriterDoesNotSupportSourceLink
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("SymWriterDoesNotSupportSourceLink", null);
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x00052C7A File Offset: 0x00050E7A
		internal static string RuleSetBadAttributeValue
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("RuleSetBadAttributeValue", null);
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x00052C87 File Offset: 0x00050E87
		internal static string RuleSetMissingAttribute
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("RuleSetMissingAttribute", null);
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x00052C94 File Offset: 0x00050E94
		internal static string KeepAliveIsNotAnInteger
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("KeepAliveIsNotAnInteger", null);
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x00052CA1 File Offset: 0x00050EA1
		internal static string KeepAliveIsTooSmall
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("KeepAliveIsTooSmall", null);
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x00052CAE File Offset: 0x00050EAE
		internal static string KeepAliveWithoutShared
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("KeepAliveWithoutShared", null);
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x00052CBB File Offset: 0x00050EBB
		internal static string MismatchedVersion
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("MismatchedVersion", null);
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x00052CC8 File Offset: 0x00050EC8
		internal static string MissingKeepAlive
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("MissingKeepAlive", null);
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x00052CD5 File Offset: 0x00050ED5
		internal static string AnalyzerTotalExecutionTime
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("AnalyzerTotalExecutionTime", null);
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x00052CE2 File Offset: 0x00050EE2
		internal static string MultithreadedAnalyzerExecutionNote
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("MultithreadedAnalyzerExecutionNote", null);
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001BCB RID: 7115 RVA: 0x00052CEF File Offset: 0x00050EEF
		internal static string AnalyzerExecutionTimeColumnHeader
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("AnalyzerExecutionTimeColumnHeader", null);
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x00052CFC File Offset: 0x00050EFC
		internal static string AnalyzerNameColumnHeader
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("AnalyzerNameColumnHeader", null);
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x00052D09 File Offset: 0x00050F09
		internal static string NoAnalyzersFound
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("NoAnalyzersFound", null);
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x00052D16 File Offset: 0x00050F16
		internal static string DuplicateAnalyzerInstances
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("DuplicateAnalyzerInstances", null);
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x00052D23 File Offset: 0x00050F23
		internal static string UnsupportedAnalyzerInstance
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("UnsupportedAnalyzerInstance", null);
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x00052D30 File Offset: 0x00050F30
		internal static string InvalidTree
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidTree", null);
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x00052D3D File Offset: 0x00050F3D
		internal static string InvalidAdditionalFile
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidAdditionalFile", null);
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x00052D4A File Offset: 0x00050F4A
		internal static string ResourceStreamEndedUnexpectedly
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ResourceStreamEndedUnexpectedly", null);
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x00052D57 File Offset: 0x00050F57
		internal static string SharedArgumentMissing
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("SharedArgumentMissing", null);
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x00052D64 File Offset: 0x00050F64
		internal static string ExceptionContext
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ExceptionContext", null);
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x00052D71 File Offset: 0x00050F71
		internal static string AnonymousTypeMemberAndNamesCountMismatch2
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("AnonymousTypeMemberAndNamesCountMismatch2", null);
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x00052D7E File Offset: 0x00050F7E
		internal static string AnonymousTypeArgumentCountMismatch2
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("AnonymousTypeArgumentCountMismatch2", null);
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x00052D8B File Offset: 0x00050F8B
		internal static string InconsistentSyntaxTreeFeature
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InconsistentSyntaxTreeFeature", null);
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x00052D98 File Offset: 0x00050F98
		internal static string ReferenceOfTypeIsInvalid1
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ReferenceOfTypeIsInvalid1", null);
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x00052DA5 File Offset: 0x00050FA5
		internal static string MetadataRefNotFoundToRemove1
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("MetadataRefNotFoundToRemove1", null);
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x00052DB2 File Offset: 0x00050FB2
		internal static string TupleElementNameCountMismatch
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("TupleElementNameCountMismatch", null);
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x00052DBF File Offset: 0x00050FBF
		internal static string TupleElementNameEmpty
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("TupleElementNameEmpty", null);
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x00052DCC File Offset: 0x00050FCC
		internal static string TupleElementLocationCountMismatch
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("TupleElementLocationCountMismatch", null);
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x00052DD9 File Offset: 0x00050FD9
		internal static string TupleElementNullableAnnotationCountMismatch
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("TupleElementNullableAnnotationCountMismatch", null);
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x00052DE6 File Offset: 0x00050FE6
		internal static string TuplesNeedAtLeastTwoElements
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("TuplesNeedAtLeastTwoElements", null);
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x00052DF3 File Offset: 0x00050FF3
		internal static string CompilationReferencesAssembliesWithDifferentAutoGeneratedVersion
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("CompilationReferencesAssembliesWithDifferentAutoGeneratedVersion", null);
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x00052E00 File Offset: 0x00051000
		internal static string TupleUnderlyingTypeMustBeTupleCompatible
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("TupleUnderlyingTypeMustBeTupleCompatible", null);
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x00052E0D File Offset: 0x0005100D
		internal static string UnrecognizedResourceFileFormat
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("UnrecognizedResourceFileFormat", null);
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x00052E1A File Offset: 0x0005101A
		internal static string SourceTextCannotBeEmbedded
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("SourceTextCannotBeEmbedded", null);
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x00052E27 File Offset: 0x00051027
		internal static string StreamIsTooLong
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("StreamIsTooLong", null);
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x00052E34 File Offset: 0x00051034
		internal static string EmbeddedTextsRequirePdb
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("EmbeddedTextsRequirePdb", null);
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x00052E41 File Offset: 0x00051041
		internal static string TheStreamCannotBeWrittenTo
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("TheStreamCannotBeWrittenTo", null);
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x00052E4E File Offset: 0x0005104E
		internal static string ElementIsExpected
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ElementIsExpected", null);
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x00052E5B File Offset: 0x0005105B
		internal static string SeparatorIsExpected
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("SeparatorIsExpected", null);
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x00052E68 File Offset: 0x00051068
		internal static string TheStreamCannotBeReadFrom
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("TheStreamCannotBeReadFrom", null);
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x00052E75 File Offset: 0x00051075
		internal static string Deserialization_reader_for_0_read_incorrect_number_of_values
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Deserialization_reader_for_0_read_incorrect_number_of_values", null);
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x00052E82 File Offset: 0x00051082
		internal static string Stream_contains_invalid_data
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Stream_contains_invalid_data", null);
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x00052E8F File Offset: 0x0005108F
		internal static string InvalidDiagnosticSpanReported
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidDiagnosticSpanReported", null);
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x00052E9C File Offset: 0x0005109C
		internal static string ExceptionEnablingMulticoreJit
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ExceptionEnablingMulticoreJit", null);
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x00052EA9 File Offset: 0x000510A9
		internal static string NotARootOperation
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("NotARootOperation", null);
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x00052EB6 File Offset: 0x000510B6
		internal static string OperationHasNullSemanticModel
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("OperationHasNullSemanticModel", null);
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x00052EC3 File Offset: 0x000510C3
		internal static string InvalidOperationBlockForAnalysisContext
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("InvalidOperationBlockForAnalysisContext", null);
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x00052ED0 File Offset: 0x000510D0
		internal static string IsSymbolAccessibleBadWithin
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("IsSymbolAccessibleBadWithin", null);
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x00052EDD File Offset: 0x000510DD
		internal static string IsSymbolAccessibleWrongAssembly
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("IsSymbolAccessibleWrongAssembly", null);
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x00052EEA File Offset: 0x000510EA
		internal static string OperationMustNotBeControlFlowGraphPart
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("OperationMustNotBeControlFlowGraphPart", null);
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x00052EF7 File Offset: 0x000510F7
		internal static string A_language_name_cannot_be_specified_for_this_option
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("A_language_name_cannot_be_specified_for_this_option", null);
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x00052F04 File Offset: 0x00051104
		internal static string A_language_name_must_be_specified_for_this_option
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("A_language_name_must_be_specified_for_this_option", null);
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x00052F11 File Offset: 0x00051111
		internal static string WRN_InvalidSeverityInAnalyzerConfig
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("WRN_InvalidSeverityInAnalyzerConfig", null);
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x00052F1E File Offset: 0x0005111E
		internal static string WRN_InvalidSeverityInAnalyzerConfig_Title
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("WRN_InvalidSeverityInAnalyzerConfig_Title", null);
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x00052F2B File Offset: 0x0005112B
		internal static string SuppressionDiagnosticDescriptorTitle
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("SuppressionDiagnosticDescriptorTitle", null);
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x00052F38 File Offset: 0x00051138
		internal static string SuppressionDiagnosticDescriptorMessage
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("SuppressionDiagnosticDescriptorMessage", null);
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x00052F45 File Offset: 0x00051145
		internal static string ModuleHasInvalidAttributes
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ModuleHasInvalidAttributes", null);
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x00052F52 File Offset: 0x00051152
		internal static string UnableToDetermineSpecificCauseOfFailure
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("UnableToDetermineSpecificCauseOfFailure", null);
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x00052F5F File Offset: 0x0005115F
		internal static string ChangingVersionOfAssemblyReferenceIsNotAllowedDuringDebugging
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ChangingVersionOfAssemblyReferenceIsNotAllowedDuringDebugging", null);
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x00052F6C File Offset: 0x0005116C
		internal static string DisableAnalyzerDiagnosticsMessage
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("DisableAnalyzerDiagnosticsMessage", null);
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x00052F79 File Offset: 0x00051179
		internal static string Single_type_per_generator_0
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("Single_type_per_generator_0", null);
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x00052F86 File Offset: 0x00051186
		internal static string WRN_MultipleGlobalAnalyzerKeys
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("WRN_MultipleGlobalAnalyzerKeys", null);
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x00052F93 File Offset: 0x00051193
		internal static string WRN_MultipleGlobalAnalyzerKeys_Title
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("WRN_MultipleGlobalAnalyzerKeys_Title", null);
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x00052FA0 File Offset: 0x000511A0
		internal static string HintNameUniquePerGenerator
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("HintNameUniquePerGenerator", null);
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x00052FAD File Offset: 0x000511AD
		internal static string HintNameInvalidChar
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("HintNameInvalidChar", null);
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x00052FBA File Offset: 0x000511BA
		internal static string SourceTextRequiresEncoding
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("SourceTextRequiresEncoding", null);
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x00052FC7 File Offset: 0x000511C7
		internal static string AssemblyReferencesNetFramework
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("AssemblyReferencesNetFramework", null);
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x00052FD4 File Offset: 0x000511D4
		internal static string WRN_InvalidGlobalSectionName
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("WRN_InvalidGlobalSectionName", null);
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x00052FE1 File Offset: 0x000511E1
		internal static string WRN_InvalidGlobalSectionName_Title
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("WRN_InvalidGlobalSectionName_Title", null);
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x00052FEE File Offset: 0x000511EE
		internal static string ChangesMustBeWithinBoundsOfSourceText
		{
			get
			{
				return CodeAnalysisResources.GetResourceString("ChangesMustBeWithinBoundsOfSourceText", null);
			}
		}

		// Token: 0x04000D44 RID: 3396
		private static ResourceManager s_resourceManager;
	}
}
