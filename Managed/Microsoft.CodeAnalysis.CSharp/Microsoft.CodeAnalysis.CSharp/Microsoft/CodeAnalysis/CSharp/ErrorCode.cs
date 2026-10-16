using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000137 RID: 311
	internal enum ErrorCode
	{
		// Token: 0x040006E8 RID: 1768
		Void = -2,
		// Token: 0x040006E9 RID: 1769
		Unknown,
		// Token: 0x040006EA RID: 1770
		ERR_NoMetadataFile = 6,
		// Token: 0x040006EB RID: 1771
		FTL_MetadataCantOpenFile = 9,
		// Token: 0x040006EC RID: 1772
		ERR_NoTypeDef = 12,
		// Token: 0x040006ED RID: 1773
		ERR_OutputWriteFailed = 16,
		// Token: 0x040006EE RID: 1774
		ERR_MultipleEntryPoints,
		// Token: 0x040006EF RID: 1775
		ERR_BadBinaryOps = 19,
		// Token: 0x040006F0 RID: 1776
		ERR_IntDivByZero,
		// Token: 0x040006F1 RID: 1777
		ERR_BadIndexLHS,
		// Token: 0x040006F2 RID: 1778
		ERR_BadIndexCount,
		// Token: 0x040006F3 RID: 1779
		ERR_BadUnaryOp,
		// Token: 0x040006F4 RID: 1780
		ERR_ThisInStaticMeth = 26,
		// Token: 0x040006F5 RID: 1781
		ERR_ThisInBadContext,
		// Token: 0x040006F6 RID: 1782
		WRN_InvalidMainSig,
		// Token: 0x040006F7 RID: 1783
		ERR_NoImplicitConv,
		// Token: 0x040006F8 RID: 1784
		ERR_NoExplicitConv,
		// Token: 0x040006F9 RID: 1785
		ERR_ConstOutOfRange,
		// Token: 0x040006FA RID: 1786
		ERR_AmbigBinaryOps = 34,
		// Token: 0x040006FB RID: 1787
		ERR_AmbigUnaryOp,
		// Token: 0x040006FC RID: 1788
		ERR_InAttrOnOutParam,
		// Token: 0x040006FD RID: 1789
		ERR_ValueCantBeNull,
		// Token: 0x040006FE RID: 1790
		ERR_NoExplicitBuiltinConv = 39,
		// Token: 0x040006FF RID: 1791
		FTL_DebugEmitFailure = 41,
		// Token: 0x04000700 RID: 1792
		ERR_BadVisReturnType = 50,
		// Token: 0x04000701 RID: 1793
		ERR_BadVisParamType,
		// Token: 0x04000702 RID: 1794
		ERR_BadVisFieldType,
		// Token: 0x04000703 RID: 1795
		ERR_BadVisPropertyType,
		// Token: 0x04000704 RID: 1796
		ERR_BadVisIndexerReturn,
		// Token: 0x04000705 RID: 1797
		ERR_BadVisIndexerParam,
		// Token: 0x04000706 RID: 1798
		ERR_BadVisOpReturn,
		// Token: 0x04000707 RID: 1799
		ERR_BadVisOpParam,
		// Token: 0x04000708 RID: 1800
		ERR_BadVisDelegateReturn,
		// Token: 0x04000709 RID: 1801
		ERR_BadVisDelegateParam,
		// Token: 0x0400070A RID: 1802
		ERR_BadVisBaseClass,
		// Token: 0x0400070B RID: 1803
		ERR_BadVisBaseInterface,
		// Token: 0x0400070C RID: 1804
		ERR_EventNeedsBothAccessors = 65,
		// Token: 0x0400070D RID: 1805
		ERR_EventNotDelegate,
		// Token: 0x0400070E RID: 1806
		WRN_UnreferencedEvent,
		// Token: 0x0400070F RID: 1807
		ERR_InterfaceEventInitializer,
		// Token: 0x04000710 RID: 1808
		ERR_BadEventUsage = 70,
		// Token: 0x04000711 RID: 1809
		ERR_ExplicitEventFieldImpl,
		// Token: 0x04000712 RID: 1810
		ERR_CantOverrideNonEvent,
		// Token: 0x04000713 RID: 1811
		ERR_AddRemoveMustHaveBody,
		// Token: 0x04000714 RID: 1812
		ERR_AbstractEventInitializer,
		// Token: 0x04000715 RID: 1813
		ERR_PossibleBadNegCast,
		// Token: 0x04000716 RID: 1814
		ERR_ReservedEnumerator,
		// Token: 0x04000717 RID: 1815
		ERR_AsMustHaveReferenceType,
		// Token: 0x04000718 RID: 1816
		WRN_LowercaseEllSuffix,
		// Token: 0x04000719 RID: 1817
		ERR_BadEventUsageNoField,
		// Token: 0x0400071A RID: 1818
		ERR_ConstraintOnlyAllowedOnGenericDecl,
		// Token: 0x0400071B RID: 1819
		ERR_TypeParamMustBeIdentifier,
		// Token: 0x0400071C RID: 1820
		ERR_MemberReserved,
		// Token: 0x0400071D RID: 1821
		ERR_DuplicateParamName = 100,
		// Token: 0x0400071E RID: 1822
		ERR_DuplicateNameInNS,
		// Token: 0x0400071F RID: 1823
		ERR_DuplicateNameInClass,
		// Token: 0x04000720 RID: 1824
		ERR_NameNotInContext,
		// Token: 0x04000721 RID: 1825
		ERR_AmbigContext,
		// Token: 0x04000722 RID: 1826
		WRN_DuplicateUsing,
		// Token: 0x04000723 RID: 1827
		ERR_BadMemberFlag,
		// Token: 0x04000724 RID: 1828
		ERR_BadMemberProtection,
		// Token: 0x04000725 RID: 1829
		WRN_NewRequired,
		// Token: 0x04000726 RID: 1830
		WRN_NewNotRequired,
		// Token: 0x04000727 RID: 1831
		ERR_CircConstValue,
		// Token: 0x04000728 RID: 1832
		ERR_MemberAlreadyExists,
		// Token: 0x04000729 RID: 1833
		ERR_StaticNotVirtual,
		// Token: 0x0400072A RID: 1834
		ERR_OverrideNotNew,
		// Token: 0x0400072B RID: 1835
		WRN_NewOrOverrideExpected,
		// Token: 0x0400072C RID: 1836
		ERR_OverrideNotExpected,
		// Token: 0x0400072D RID: 1837
		ERR_NamespaceUnexpected,
		// Token: 0x0400072E RID: 1838
		ERR_NoSuchMember,
		// Token: 0x0400072F RID: 1839
		ERR_BadSKknown,
		// Token: 0x04000730 RID: 1840
		ERR_BadSKunknown,
		// Token: 0x04000731 RID: 1841
		ERR_ObjectRequired,
		// Token: 0x04000732 RID: 1842
		ERR_AmbigCall,
		// Token: 0x04000733 RID: 1843
		ERR_BadAccess,
		// Token: 0x04000734 RID: 1844
		ERR_MethDelegateMismatch,
		// Token: 0x04000735 RID: 1845
		ERR_RetObjectRequired = 126,
		// Token: 0x04000736 RID: 1846
		ERR_RetNoObjectRequired,
		// Token: 0x04000737 RID: 1847
		ERR_LocalDuplicate,
		// Token: 0x04000738 RID: 1848
		ERR_AssgLvalueExpected = 131,
		// Token: 0x04000739 RID: 1849
		ERR_StaticConstParam,
		// Token: 0x0400073A RID: 1850
		ERR_NotConstantExpression,
		// Token: 0x0400073B RID: 1851
		ERR_NotNullConstRefField,
		// Token: 0x0400073C RID: 1852
		ERR_LocalIllegallyOverrides = 136,
		// Token: 0x0400073D RID: 1853
		ERR_BadUsingNamespace = 138,
		// Token: 0x0400073E RID: 1854
		ERR_NoBreakOrCont,
		// Token: 0x0400073F RID: 1855
		ERR_DuplicateLabel,
		// Token: 0x04000740 RID: 1856
		ERR_NoConstructors = 143,
		// Token: 0x04000741 RID: 1857
		ERR_NoNewAbstract,
		// Token: 0x04000742 RID: 1858
		ERR_ConstValueRequired,
		// Token: 0x04000743 RID: 1859
		ERR_CircularBase,
		// Token: 0x04000744 RID: 1860
		ERR_BadDelegateConstructor = 148,
		// Token: 0x04000745 RID: 1861
		ERR_MethodNameExpected,
		// Token: 0x04000746 RID: 1862
		ERR_ConstantExpected,
		// Token: 0x04000747 RID: 1863
		ERR_V6SwitchGoverningTypeValueExpected,
		// Token: 0x04000748 RID: 1864
		ERR_DuplicateCaseLabel,
		// Token: 0x04000749 RID: 1865
		ERR_InvalidGotoCase,
		// Token: 0x0400074A RID: 1866
		ERR_PropertyLacksGet,
		// Token: 0x0400074B RID: 1867
		ERR_BadExceptionType,
		// Token: 0x0400074C RID: 1868
		ERR_BadEmptyThrow,
		// Token: 0x0400074D RID: 1869
		ERR_BadFinallyLeave,
		// Token: 0x0400074E RID: 1870
		ERR_LabelShadow,
		// Token: 0x0400074F RID: 1871
		ERR_LabelNotFound,
		// Token: 0x04000750 RID: 1872
		ERR_UnreachableCatch,
		// Token: 0x04000751 RID: 1873
		ERR_ReturnExpected,
		// Token: 0x04000752 RID: 1874
		WRN_UnreachableCode,
		// Token: 0x04000753 RID: 1875
		ERR_SwitchFallThrough,
		// Token: 0x04000754 RID: 1876
		WRN_UnreferencedLabel,
		// Token: 0x04000755 RID: 1877
		ERR_UseDefViolation,
		// Token: 0x04000756 RID: 1878
		WRN_UnreferencedVar = 168,
		// Token: 0x04000757 RID: 1879
		WRN_UnreferencedField,
		// Token: 0x04000758 RID: 1880
		ERR_UseDefViolationField,
		// Token: 0x04000759 RID: 1881
		ERR_UnassignedThis,
		// Token: 0x0400075A RID: 1882
		ERR_AmbigQM,
		// Token: 0x0400075B RID: 1883
		ERR_InvalidQM,
		// Token: 0x0400075C RID: 1884
		ERR_NoBaseClass,
		// Token: 0x0400075D RID: 1885
		ERR_BaseIllegal,
		// Token: 0x0400075E RID: 1886
		ERR_ObjectProhibited,
		// Token: 0x0400075F RID: 1887
		ERR_ParamUnassigned,
		// Token: 0x04000760 RID: 1888
		ERR_InvalidArray,
		// Token: 0x04000761 RID: 1889
		ERR_ExternHasBody,
		// Token: 0x04000762 RID: 1890
		ERR_AbstractAndExtern,
		// Token: 0x04000763 RID: 1891
		ERR_BadAttributeParamType,
		// Token: 0x04000764 RID: 1892
		ERR_BadAttributeArgument,
		// Token: 0x04000765 RID: 1893
		WRN_IsAlwaysTrue,
		// Token: 0x04000766 RID: 1894
		WRN_IsAlwaysFalse,
		// Token: 0x04000767 RID: 1895
		ERR_LockNeedsReference,
		// Token: 0x04000768 RID: 1896
		ERR_NullNotValid,
		// Token: 0x04000769 RID: 1897
		ERR_UseDefViolationThis = 188,
		// Token: 0x0400076A RID: 1898
		ERR_ArgsInvalid = 190,
		// Token: 0x0400076B RID: 1899
		ERR_AssgReadonly,
		// Token: 0x0400076C RID: 1900
		ERR_RefReadonly,
		// Token: 0x0400076D RID: 1901
		ERR_PtrExpected,
		// Token: 0x0400076E RID: 1902
		ERR_PtrIndexSingle = 196,
		// Token: 0x0400076F RID: 1903
		WRN_ByRefNonAgileField,
		// Token: 0x04000770 RID: 1904
		ERR_AssgReadonlyStatic,
		// Token: 0x04000771 RID: 1905
		ERR_RefReadonlyStatic,
		// Token: 0x04000772 RID: 1906
		ERR_AssgReadonlyProp,
		// Token: 0x04000773 RID: 1907
		ERR_IllegalStatement,
		// Token: 0x04000774 RID: 1908
		ERR_BadGetEnumerator,
		// Token: 0x04000775 RID: 1909
		ERR_TooManyLocals = 204,
		// Token: 0x04000776 RID: 1910
		ERR_AbstractBaseCall,
		// Token: 0x04000777 RID: 1911
		ERR_RefProperty,
		// Token: 0x04000778 RID: 1912
		ERR_ManagedAddr = 208,
		// Token: 0x04000779 RID: 1913
		ERR_BadFixedInitType,
		// Token: 0x0400077A RID: 1914
		ERR_FixedMustInit,
		// Token: 0x0400077B RID: 1915
		ERR_InvalidAddrOp,
		// Token: 0x0400077C RID: 1916
		ERR_FixedNeeded,
		// Token: 0x0400077D RID: 1917
		ERR_FixedNotNeeded,
		// Token: 0x0400077E RID: 1918
		ERR_UnsafeNeeded,
		// Token: 0x0400077F RID: 1919
		ERR_OpTFRetType,
		// Token: 0x04000780 RID: 1920
		ERR_OperatorNeedsMatch,
		// Token: 0x04000781 RID: 1921
		ERR_BadBoolOp,
		// Token: 0x04000782 RID: 1922
		ERR_MustHaveOpTF,
		// Token: 0x04000783 RID: 1923
		WRN_UnreferencedVarAssg,
		// Token: 0x04000784 RID: 1924
		ERR_CheckedOverflow,
		// Token: 0x04000785 RID: 1925
		ERR_ConstOutOfRangeChecked,
		// Token: 0x04000786 RID: 1926
		ERR_BadVarargs = 224,
		// Token: 0x04000787 RID: 1927
		ERR_ParamsMustBeArray,
		// Token: 0x04000788 RID: 1928
		ERR_IllegalArglist,
		// Token: 0x04000789 RID: 1929
		ERR_IllegalUnsafe,
		// Token: 0x0400078A RID: 1930
		ERR_AmbigMember = 229,
		// Token: 0x0400078B RID: 1931
		ERR_BadForeachDecl,
		// Token: 0x0400078C RID: 1932
		ERR_ParamsLast,
		// Token: 0x0400078D RID: 1933
		ERR_SizeofUnsafe = 233,
		// Token: 0x0400078E RID: 1934
		ERR_DottedTypeNameNotFoundInNS,
		// Token: 0x0400078F RID: 1935
		ERR_FieldInitRefNonstatic = 236,
		// Token: 0x04000790 RID: 1936
		ERR_SealedNonOverride = 238,
		// Token: 0x04000791 RID: 1937
		ERR_CantOverrideSealed,
		// Token: 0x04000792 RID: 1938
		ERR_VoidError = 242,
		// Token: 0x04000793 RID: 1939
		ERR_ConditionalOnOverride,
		// Token: 0x04000794 RID: 1940
		ERR_PointerInAsOrIs,
		// Token: 0x04000795 RID: 1941
		ERR_CallingFinalizeDeprecated,
		// Token: 0x04000796 RID: 1942
		ERR_SingleTypeNameNotFound,
		// Token: 0x04000797 RID: 1943
		ERR_NegativeStackAllocSize,
		// Token: 0x04000798 RID: 1944
		ERR_NegativeArraySize,
		// Token: 0x04000799 RID: 1945
		ERR_OverrideFinalizeDeprecated,
		// Token: 0x0400079A RID: 1946
		ERR_CallingBaseFinalizeDeprecated,
		// Token: 0x0400079B RID: 1947
		WRN_NegativeArrayIndex,
		// Token: 0x0400079C RID: 1948
		WRN_BadRefCompareLeft,
		// Token: 0x0400079D RID: 1949
		WRN_BadRefCompareRight,
		// Token: 0x0400079E RID: 1950
		ERR_BadCastInFixed,
		// Token: 0x0400079F RID: 1951
		ERR_StackallocInCatchFinally,
		// Token: 0x040007A0 RID: 1952
		ERR_VarargsLast = 257,
		// Token: 0x040007A1 RID: 1953
		ERR_MissingPartial = 260,
		// Token: 0x040007A2 RID: 1954
		ERR_PartialTypeKindConflict,
		// Token: 0x040007A3 RID: 1955
		ERR_PartialModifierConflict,
		// Token: 0x040007A4 RID: 1956
		ERR_PartialMultipleBases,
		// Token: 0x040007A5 RID: 1957
		ERR_PartialWrongTypeParams,
		// Token: 0x040007A6 RID: 1958
		ERR_PartialWrongConstraints,
		// Token: 0x040007A7 RID: 1959
		ERR_NoImplicitConvCast,
		// Token: 0x040007A8 RID: 1960
		ERR_PartialMisplaced,
		// Token: 0x040007A9 RID: 1961
		ERR_ImportedCircularBase,
		// Token: 0x040007AA RID: 1962
		ERR_UseDefViolationOut,
		// Token: 0x040007AB RID: 1963
		ERR_ArraySizeInDeclaration,
		// Token: 0x040007AC RID: 1964
		ERR_InaccessibleGetter,
		// Token: 0x040007AD RID: 1965
		ERR_InaccessibleSetter,
		// Token: 0x040007AE RID: 1966
		ERR_InvalidPropertyAccessMod,
		// Token: 0x040007AF RID: 1967
		ERR_DuplicatePropertyAccessMods,
		// Token: 0x040007B0 RID: 1968
		ERR_AccessModMissingAccessor = 276,
		// Token: 0x040007B1 RID: 1969
		ERR_UnimplementedInterfaceAccessor,
		// Token: 0x040007B2 RID: 1970
		WRN_PatternIsAmbiguous,
		// Token: 0x040007B3 RID: 1971
		WRN_PatternNotPublicOrNotInstance,
		// Token: 0x040007B4 RID: 1972
		WRN_PatternBadSignature,
		// Token: 0x040007B5 RID: 1973
		ERR_FriendRefNotEqualToThis,
		// Token: 0x040007B6 RID: 1974
		WRN_SequentialOnPartialClass,
		// Token: 0x040007B7 RID: 1975
		ERR_BadConstType,
		// Token: 0x040007B8 RID: 1976
		ERR_NoNewTyvar = 304,
		// Token: 0x040007B9 RID: 1977
		ERR_BadArity,
		// Token: 0x040007BA RID: 1978
		ERR_BadTypeArgument,
		// Token: 0x040007BB RID: 1979
		ERR_TypeArgsNotAllowed,
		// Token: 0x040007BC RID: 1980
		ERR_HasNoTypeVars,
		// Token: 0x040007BD RID: 1981
		ERR_NewConstraintNotSatisfied = 310,
		// Token: 0x040007BE RID: 1982
		ERR_GenericConstraintNotSatisfiedRefType,
		// Token: 0x040007BF RID: 1983
		ERR_GenericConstraintNotSatisfiedNullableEnum,
		// Token: 0x040007C0 RID: 1984
		ERR_GenericConstraintNotSatisfiedNullableInterface,
		// Token: 0x040007C1 RID: 1985
		ERR_GenericConstraintNotSatisfiedTyVar,
		// Token: 0x040007C2 RID: 1986
		ERR_GenericConstraintNotSatisfiedValType,
		// Token: 0x040007C3 RID: 1987
		ERR_DuplicateGeneratedName,
		// Token: 0x040007C4 RID: 1988
		ERR_GlobalSingleTypeNameNotFound = 400,
		// Token: 0x040007C5 RID: 1989
		ERR_NewBoundMustBeLast,
		// Token: 0x040007C6 RID: 1990
		WRN_MainCantBeGeneric,
		// Token: 0x040007C7 RID: 1991
		ERR_TypeVarCantBeNull,
		// Token: 0x040007C8 RID: 1992
		ERR_AttributeCantBeGeneric,
		// Token: 0x040007C9 RID: 1993
		ERR_DuplicateBound,
		// Token: 0x040007CA RID: 1994
		ERR_ClassBoundNotFirst,
		// Token: 0x040007CB RID: 1995
		ERR_BadRetType,
		// Token: 0x040007CC RID: 1996
		ERR_DuplicateConstraintClause = 409,
		// Token: 0x040007CD RID: 1997
		ERR_CantInferMethTypeArgs = 411,
		// Token: 0x040007CE RID: 1998
		ERR_LocalSameNameAsTypeParam,
		// Token: 0x040007CF RID: 1999
		ERR_AsWithTypeVar,
		// Token: 0x040007D0 RID: 2000
		WRN_UnreferencedFieldAssg,
		// Token: 0x040007D1 RID: 2001
		ERR_BadIndexerNameAttr,
		// Token: 0x040007D2 RID: 2002
		ERR_AttrArgWithTypeVars,
		// Token: 0x040007D3 RID: 2003
		ERR_NewTyvarWithArgs,
		// Token: 0x040007D4 RID: 2004
		ERR_AbstractSealedStatic,
		// Token: 0x040007D5 RID: 2005
		WRN_AmbiguousXMLReference,
		// Token: 0x040007D6 RID: 2006
		WRN_VolatileByRef,
		// Token: 0x040007D7 RID: 2007
		ERR_ComImportWithImpl = 423,
		// Token: 0x040007D8 RID: 2008
		ERR_ComImportWithBase,
		// Token: 0x040007D9 RID: 2009
		ERR_ImplBadConstraints,
		// Token: 0x040007DA RID: 2010
		ERR_DottedTypeNameNotFoundInAgg,
		// Token: 0x040007DB RID: 2011
		ERR_MethGrpToNonDel = 428,
		// Token: 0x040007DC RID: 2012
		ERR_BadExternAlias = 430,
		// Token: 0x040007DD RID: 2013
		ERR_ColColWithTypeAlias,
		// Token: 0x040007DE RID: 2014
		ERR_AliasNotFound,
		// Token: 0x040007DF RID: 2015
		ERR_SameFullNameAggAgg,
		// Token: 0x040007E0 RID: 2016
		ERR_SameFullNameNsAgg,
		// Token: 0x040007E1 RID: 2017
		WRN_SameFullNameThisNsAgg,
		// Token: 0x040007E2 RID: 2018
		WRN_SameFullNameThisAggAgg,
		// Token: 0x040007E3 RID: 2019
		WRN_SameFullNameThisAggNs,
		// Token: 0x040007E4 RID: 2020
		ERR_SameFullNameThisAggThisNs,
		// Token: 0x040007E5 RID: 2021
		ERR_ExternAfterElements,
		// Token: 0x040007E6 RID: 2022
		WRN_GlobalAliasDefn,
		// Token: 0x040007E7 RID: 2023
		ERR_SealedStaticClass,
		// Token: 0x040007E8 RID: 2024
		ERR_PrivateAbstractAccessor,
		// Token: 0x040007E9 RID: 2025
		ERR_ValueExpected,
		// Token: 0x040007EA RID: 2026
		ERR_UnboxNotLValue = 445,
		// Token: 0x040007EB RID: 2027
		ERR_AnonMethGrpInForEach,
		// Token: 0x040007EC RID: 2028
		ERR_BadIncDecRetType = 448,
		// Token: 0x040007ED RID: 2029
		ERR_TypeConstraintsMustBeUniqueAndFirst,
		// Token: 0x040007EE RID: 2030
		ERR_RefValBoundWithClass,
		// Token: 0x040007EF RID: 2031
		ERR_NewBoundWithVal,
		// Token: 0x040007F0 RID: 2032
		ERR_RefConstraintNotSatisfied,
		// Token: 0x040007F1 RID: 2033
		ERR_ValConstraintNotSatisfied,
		// Token: 0x040007F2 RID: 2034
		ERR_CircularConstraint,
		// Token: 0x040007F3 RID: 2035
		ERR_BaseConstraintConflict,
		// Token: 0x040007F4 RID: 2036
		ERR_ConWithValCon,
		// Token: 0x040007F5 RID: 2037
		ERR_AmbigUDConv,
		// Token: 0x040007F6 RID: 2038
		WRN_AlwaysNull,
		// Token: 0x040007F7 RID: 2039
		ERR_OverrideWithConstraints = 460,
		// Token: 0x040007F8 RID: 2040
		ERR_AmbigOverride = 462,
		// Token: 0x040007F9 RID: 2041
		ERR_DecConstError,
		// Token: 0x040007FA RID: 2042
		WRN_CmpAlwaysFalse,
		// Token: 0x040007FB RID: 2043
		WRN_FinalizeMethod,
		// Token: 0x040007FC RID: 2044
		ERR_ExplicitImplParams,
		// Token: 0x040007FD RID: 2045
		WRN_GotoCaseShouldConvert = 469,
		// Token: 0x040007FE RID: 2046
		ERR_MethodImplementingAccessor,
		// Token: 0x040007FF RID: 2047
		WRN_NubExprIsConstBool = 472,
		// Token: 0x04000800 RID: 2048
		WRN_ExplicitImplCollision,
		// Token: 0x04000801 RID: 2049
		ERR_AbstractHasBody = 500,
		// Token: 0x04000802 RID: 2050
		ERR_ConcreteMissingBody,
		// Token: 0x04000803 RID: 2051
		ERR_AbstractAndSealed,
		// Token: 0x04000804 RID: 2052
		ERR_AbstractNotVirtual,
		// Token: 0x04000805 RID: 2053
		ERR_StaticConstant,
		// Token: 0x04000806 RID: 2054
		ERR_CantOverrideNonFunction,
		// Token: 0x04000807 RID: 2055
		ERR_CantOverrideNonVirtual,
		// Token: 0x04000808 RID: 2056
		ERR_CantChangeAccessOnOverride,
		// Token: 0x04000809 RID: 2057
		ERR_CantChangeReturnTypeOnOverride,
		// Token: 0x0400080A RID: 2058
		ERR_CantDeriveFromSealedType,
		// Token: 0x0400080B RID: 2059
		ERR_AbstractInConcreteClass = 513,
		// Token: 0x0400080C RID: 2060
		ERR_StaticConstructorWithExplicitConstructorCall,
		// Token: 0x0400080D RID: 2061
		ERR_StaticConstructorWithAccessModifiers,
		// Token: 0x0400080E RID: 2062
		ERR_RecursiveConstructorCall,
		// Token: 0x0400080F RID: 2063
		ERR_ObjectCallingBaseConstructor,
		// Token: 0x04000810 RID: 2064
		ERR_PredefinedTypeNotFound,
		// Token: 0x04000811 RID: 2065
		ERR_StructWithBaseConstructorCall = 522,
		// Token: 0x04000812 RID: 2066
		ERR_StructLayoutCycle,
		// Token: 0x04000813 RID: 2067
		ERR_InterfacesCantContainFields = 525,
		// Token: 0x04000814 RID: 2068
		ERR_InterfacesCantContainConstructors,
		// Token: 0x04000815 RID: 2069
		ERR_NonInterfaceInInterfaceList,
		// Token: 0x04000816 RID: 2070
		ERR_DuplicateInterfaceInBaseList,
		// Token: 0x04000817 RID: 2071
		ERR_CycleInInterfaceInheritance,
		// Token: 0x04000818 RID: 2072
		ERR_HidingAbstractMethod = 533,
		// Token: 0x04000819 RID: 2073
		ERR_UnimplementedAbstractMethod,
		// Token: 0x0400081A RID: 2074
		ERR_UnimplementedInterfaceMember,
		// Token: 0x0400081B RID: 2075
		ERR_ObjectCantHaveBases = 537,
		// Token: 0x0400081C RID: 2076
		ERR_ExplicitInterfaceImplementationNotInterface,
		// Token: 0x0400081D RID: 2077
		ERR_InterfaceMemberNotFound,
		// Token: 0x0400081E RID: 2078
		ERR_ClassDoesntImplementInterface,
		// Token: 0x0400081F RID: 2079
		ERR_ExplicitInterfaceImplementationInNonClassOrStruct,
		// Token: 0x04000820 RID: 2080
		ERR_MemberNameSameAsType,
		// Token: 0x04000821 RID: 2081
		ERR_EnumeratorOverflow,
		// Token: 0x04000822 RID: 2082
		ERR_CantOverrideNonProperty,
		// Token: 0x04000823 RID: 2083
		ERR_NoGetToOverride,
		// Token: 0x04000824 RID: 2084
		ERR_NoSetToOverride,
		// Token: 0x04000825 RID: 2085
		ERR_PropertyCantHaveVoidType,
		// Token: 0x04000826 RID: 2086
		ERR_PropertyWithNoAccessors,
		// Token: 0x04000827 RID: 2087
		ERR_NewVirtualInSealed,
		// Token: 0x04000828 RID: 2088
		ERR_ExplicitPropertyAddingAccessor,
		// Token: 0x04000829 RID: 2089
		ERR_ExplicitPropertyMissingAccessor,
		// Token: 0x0400082A RID: 2090
		ERR_ConversionWithInterface,
		// Token: 0x0400082B RID: 2091
		ERR_ConversionWithBase,
		// Token: 0x0400082C RID: 2092
		ERR_ConversionWithDerived,
		// Token: 0x0400082D RID: 2093
		ERR_IdentityConversion,
		// Token: 0x0400082E RID: 2094
		ERR_ConversionNotInvolvingContainedType,
		// Token: 0x0400082F RID: 2095
		ERR_DuplicateConversionInClass,
		// Token: 0x04000830 RID: 2096
		ERR_OperatorsMustBeStatic,
		// Token: 0x04000831 RID: 2097
		ERR_BadIncDecSignature,
		// Token: 0x04000832 RID: 2098
		ERR_BadUnaryOperatorSignature = 562,
		// Token: 0x04000833 RID: 2099
		ERR_BadBinaryOperatorSignature,
		// Token: 0x04000834 RID: 2100
		ERR_BadShiftOperatorSignature,
		// Token: 0x04000835 RID: 2101
		ERR_InterfacesCantContainConversionOrEqualityOperators = 567,
		// Token: 0x04000836 RID: 2102
		ERR_StructsCantContainDefaultConstructor,
		// Token: 0x04000837 RID: 2103
		ERR_CantOverrideBogusMethod,
		// Token: 0x04000838 RID: 2104
		ERR_BindToBogus,
		// Token: 0x04000839 RID: 2105
		ERR_CantCallSpecialMethod,
		// Token: 0x0400083A RID: 2106
		ERR_BadTypeReference,
		// Token: 0x0400083B RID: 2107
		ERR_FieldInitializerInStruct,
		// Token: 0x0400083C RID: 2108
		ERR_BadDestructorName,
		// Token: 0x0400083D RID: 2109
		ERR_OnlyClassesCanContainDestructors,
		// Token: 0x0400083E RID: 2110
		ERR_ConflictAliasAndMember,
		// Token: 0x0400083F RID: 2111
		ERR_ConditionalOnSpecialMethod,
		// Token: 0x04000840 RID: 2112
		ERR_ConditionalMustReturnVoid,
		// Token: 0x04000841 RID: 2113
		ERR_DuplicateAttribute,
		// Token: 0x04000842 RID: 2114
		ERR_ConditionalOnInterfaceMethod = 582,
		// Token: 0x04000843 RID: 2115
		ERR_OperatorCantReturnVoid = 590,
		// Token: 0x04000844 RID: 2116
		ERR_InvalidAttributeArgument,
		// Token: 0x04000845 RID: 2117
		ERR_AttributeOnBadSymbolType,
		// Token: 0x04000846 RID: 2118
		ERR_FloatOverflow = 594,
		// Token: 0x04000847 RID: 2119
		ERR_InvalidReal,
		// Token: 0x04000848 RID: 2120
		ERR_ComImportWithoutUuidAttribute,
		// Token: 0x04000849 RID: 2121
		ERR_InvalidNamedArgument = 599,
		// Token: 0x0400084A RID: 2122
		ERR_DllImportOnInvalidMethod = 601,
		// Token: 0x0400084B RID: 2123
		ERR_FieldCantBeRefAny = 610,
		// Token: 0x0400084C RID: 2124
		ERR_ArrayElementCantBeRefAny,
		// Token: 0x0400084D RID: 2125
		WRN_DeprecatedSymbol,
		// Token: 0x0400084E RID: 2126
		ERR_NotAnAttributeClass = 616,
		// Token: 0x0400084F RID: 2127
		ERR_BadNamedAttributeArgument,
		// Token: 0x04000850 RID: 2128
		WRN_DeprecatedSymbolStr,
		// Token: 0x04000851 RID: 2129
		ERR_DeprecatedSymbolStr,
		// Token: 0x04000852 RID: 2130
		ERR_IndexerCantHaveVoidType,
		// Token: 0x04000853 RID: 2131
		ERR_VirtualPrivate,
		// Token: 0x04000854 RID: 2132
		ERR_ArrayInitToNonArrayType,
		// Token: 0x04000855 RID: 2133
		ERR_ArrayInitInBadPlace,
		// Token: 0x04000856 RID: 2134
		ERR_MissingStructOffset = 625,
		// Token: 0x04000857 RID: 2135
		WRN_ExternMethodNoImplementation,
		// Token: 0x04000858 RID: 2136
		WRN_ProtectedInSealed = 628,
		// Token: 0x04000859 RID: 2137
		ERR_InterfaceImplementedByConditional,
		// Token: 0x0400085A RID: 2138
		ERR_InterfaceImplementedImplicitlyByVariadic,
		// Token: 0x0400085B RID: 2139
		ERR_IllegalRefParam,
		// Token: 0x0400085C RID: 2140
		ERR_BadArgumentToAttribute = 633,
		// Token: 0x0400085D RID: 2141
		ERR_StructOffsetOnBadStruct = 636,
		// Token: 0x0400085E RID: 2142
		ERR_StructOffsetOnBadField,
		// Token: 0x0400085F RID: 2143
		ERR_AttributeUsageOnNonAttributeClass = 641,
		// Token: 0x04000860 RID: 2144
		WRN_PossibleMistakenNullStatement,
		// Token: 0x04000861 RID: 2145
		ERR_DuplicateNamedAttributeArgument,
		// Token: 0x04000862 RID: 2146
		ERR_DeriveFromEnumOrValueType,
		// Token: 0x04000863 RID: 2147
		ERR_DefaultMemberOnIndexedType = 646,
		// Token: 0x04000864 RID: 2148
		ERR_BogusType = 648,
		// Token: 0x04000865 RID: 2149
		WRN_UnassignedInternalField,
		// Token: 0x04000866 RID: 2150
		ERR_CStyleArray,
		// Token: 0x04000867 RID: 2151
		WRN_VacuousIntegralComp = 652,
		// Token: 0x04000868 RID: 2152
		ERR_AbstractAttributeClass,
		// Token: 0x04000869 RID: 2153
		ERR_BadNamedAttributeArgumentType = 655,
		// Token: 0x0400086A RID: 2154
		ERR_MissingPredefinedMember,
		// Token: 0x0400086B RID: 2155
		WRN_AttributeLocationOnBadDeclaration,
		// Token: 0x0400086C RID: 2156
		WRN_InvalidAttributeLocation,
		// Token: 0x0400086D RID: 2157
		WRN_EqualsWithoutGetHashCode,
		// Token: 0x0400086E RID: 2158
		WRN_EqualityOpWithoutEquals,
		// Token: 0x0400086F RID: 2159
		WRN_EqualityOpWithoutGetHashCode,
		// Token: 0x04000870 RID: 2160
		ERR_OutAttrOnRefParam,
		// Token: 0x04000871 RID: 2161
		ERR_OverloadRefKind,
		// Token: 0x04000872 RID: 2162
		ERR_LiteralDoubleCast,
		// Token: 0x04000873 RID: 2163
		WRN_IncorrectBooleanAssg,
		// Token: 0x04000874 RID: 2164
		ERR_ProtectedInStruct,
		// Token: 0x04000875 RID: 2165
		ERR_InconsistentIndexerNames = 668,
		// Token: 0x04000876 RID: 2166
		ERR_ComImportWithUserCtor,
		// Token: 0x04000877 RID: 2167
		ERR_FieldCantHaveVoidType,
		// Token: 0x04000878 RID: 2168
		WRN_NonObsoleteOverridingObsolete = 672,
		// Token: 0x04000879 RID: 2169
		ERR_SystemVoid,
		// Token: 0x0400087A RID: 2170
		ERR_ExplicitParamArray,
		// Token: 0x0400087B RID: 2171
		WRN_BitwiseOrSignExtend,
		// Token: 0x0400087C RID: 2172
		ERR_VolatileStruct = 677,
		// Token: 0x0400087D RID: 2173
		ERR_VolatileAndReadonly,
		// Token: 0x0400087E RID: 2174
		ERR_AbstractField = 681,
		// Token: 0x0400087F RID: 2175
		ERR_BogusExplicitImpl,
		// Token: 0x04000880 RID: 2176
		ERR_ExplicitMethodImplAccessor,
		// Token: 0x04000881 RID: 2177
		WRN_CoClassWithoutComImport,
		// Token: 0x04000882 RID: 2178
		ERR_ConditionalWithOutParam,
		// Token: 0x04000883 RID: 2179
		ERR_AccessorImplementingMethod,
		// Token: 0x04000884 RID: 2180
		ERR_AliasQualAsExpression,
		// Token: 0x04000885 RID: 2181
		ERR_DerivingFromATyVar = 689,
		// Token: 0x04000886 RID: 2182
		ERR_DuplicateTypeParameter = 692,
		// Token: 0x04000887 RID: 2183
		WRN_TypeParameterSameAsOuterTypeParameter,
		// Token: 0x04000888 RID: 2184
		ERR_TypeVariableSameAsParent,
		// Token: 0x04000889 RID: 2185
		ERR_UnifyingInterfaceInstantiations,
		// Token: 0x0400088A RID: 2186
		ERR_GenericDerivingFromAttribute = 698,
		// Token: 0x0400088B RID: 2187
		ERR_TyVarNotFoundInConstraint,
		// Token: 0x0400088C RID: 2188
		ERR_BadBoundType = 701,
		// Token: 0x0400088D RID: 2189
		ERR_SpecialTypeAsBound,
		// Token: 0x0400088E RID: 2190
		ERR_BadVisBound,
		// Token: 0x0400088F RID: 2191
		ERR_LookupInTypeVariable,
		// Token: 0x04000890 RID: 2192
		ERR_BadConstraintType = 706,
		// Token: 0x04000891 RID: 2193
		ERR_InstanceMemberInStaticClass = 708,
		// Token: 0x04000892 RID: 2194
		ERR_StaticBaseClass,
		// Token: 0x04000893 RID: 2195
		ERR_ConstructorInStaticClass,
		// Token: 0x04000894 RID: 2196
		ERR_DestructorInStaticClass,
		// Token: 0x04000895 RID: 2197
		ERR_InstantiatingStaticClass,
		// Token: 0x04000896 RID: 2198
		ERR_StaticDerivedFromNonObject,
		// Token: 0x04000897 RID: 2199
		ERR_StaticClassInterfaceImpl,
		// Token: 0x04000898 RID: 2200
		ERR_OperatorInStaticClass,
		// Token: 0x04000899 RID: 2201
		ERR_ConvertToStaticClass,
		// Token: 0x0400089A RID: 2202
		ERR_ConstraintIsStaticClass,
		// Token: 0x0400089B RID: 2203
		ERR_GenericArgIsStaticClass,
		// Token: 0x0400089C RID: 2204
		ERR_ArrayOfStaticClass,
		// Token: 0x0400089D RID: 2205
		ERR_IndexerInStaticClass,
		// Token: 0x0400089E RID: 2206
		ERR_ParameterIsStaticClass,
		// Token: 0x0400089F RID: 2207
		ERR_ReturnTypeIsStaticClass,
		// Token: 0x040008A0 RID: 2208
		ERR_VarDeclIsStaticClass,
		// Token: 0x040008A1 RID: 2209
		ERR_BadEmptyThrowInFinally,
		// Token: 0x040008A2 RID: 2210
		ERR_InvalidSpecifier = 726,
		// Token: 0x040008A3 RID: 2211
		WRN_AssignmentToLockOrDispose = 728,
		// Token: 0x040008A4 RID: 2212
		ERR_ForwardedTypeInThisAssembly,
		// Token: 0x040008A5 RID: 2213
		ERR_ForwardedTypeIsNested,
		// Token: 0x040008A6 RID: 2214
		ERR_CycleInTypeForwarder,
		// Token: 0x040008A7 RID: 2215
		ERR_AssemblyNameOnNonModule = 734,
		// Token: 0x040008A8 RID: 2216
		ERR_InvalidFwdType,
		// Token: 0x040008A9 RID: 2217
		ERR_CloseUnimplementedInterfaceMemberStatic,
		// Token: 0x040008AA RID: 2218
		ERR_CloseUnimplementedInterfaceMemberNotPublic,
		// Token: 0x040008AB RID: 2219
		ERR_CloseUnimplementedInterfaceMemberWrongReturnType,
		// Token: 0x040008AC RID: 2220
		ERR_DuplicateTypeForwarder,
		// Token: 0x040008AD RID: 2221
		ERR_ExpectedSelectOrGroup = 742,
		// Token: 0x040008AE RID: 2222
		ERR_ExpectedContextualKeywordOn,
		// Token: 0x040008AF RID: 2223
		ERR_ExpectedContextualKeywordEquals,
		// Token: 0x040008B0 RID: 2224
		ERR_ExpectedContextualKeywordBy,
		// Token: 0x040008B1 RID: 2225
		ERR_InvalidAnonymousTypeMemberDeclarator,
		// Token: 0x040008B2 RID: 2226
		ERR_InvalidInitializerElementInitializer,
		// Token: 0x040008B3 RID: 2227
		ERR_InconsistentLambdaParameterUsage,
		// Token: 0x040008B4 RID: 2228
		ERR_PartialMethodInvalidModifier = 750,
		// Token: 0x040008B5 RID: 2229
		ERR_PartialMethodOnlyInPartialClass,
		// Token: 0x040008B6 RID: 2230
		ERR_PartialMethodNotExplicit = 754,
		// Token: 0x040008B7 RID: 2231
		ERR_PartialMethodExtensionDifference,
		// Token: 0x040008B8 RID: 2232
		ERR_PartialMethodOnlyOneLatent,
		// Token: 0x040008B9 RID: 2233
		ERR_PartialMethodOnlyOneActual,
		// Token: 0x040008BA RID: 2234
		ERR_PartialMethodParamsDifference,
		// Token: 0x040008BB RID: 2235
		ERR_PartialMethodMustHaveLatent,
		// Token: 0x040008BC RID: 2236
		ERR_PartialMethodInconsistentConstraints = 761,
		// Token: 0x040008BD RID: 2237
		ERR_PartialMethodToDelegate,
		// Token: 0x040008BE RID: 2238
		ERR_PartialMethodStaticDifference,
		// Token: 0x040008BF RID: 2239
		ERR_PartialMethodUnsafeDifference,
		// Token: 0x040008C0 RID: 2240
		ERR_PartialMethodInExpressionTree,
		// Token: 0x040008C1 RID: 2241
		ERR_ExplicitImplCollisionOnRefOut = 767,
		// Token: 0x040008C2 RID: 2242
		ERR_IndirectRecursiveConstructorCall,
		// Token: 0x040008C3 RID: 2243
		WRN_ObsoleteOverridingNonObsolete = 809,
		// Token: 0x040008C4 RID: 2244
		WRN_DebugFullNameTooLong = 811,
		// Token: 0x040008C5 RID: 2245
		ERR_ImplicitlyTypedVariableAssignedBadValue = 815,
		// Token: 0x040008C6 RID: 2246
		ERR_ImplicitlyTypedVariableWithNoInitializer = 818,
		// Token: 0x040008C7 RID: 2247
		ERR_ImplicitlyTypedVariableMultipleDeclarator,
		// Token: 0x040008C8 RID: 2248
		ERR_ImplicitlyTypedVariableAssignedArrayInitializer,
		// Token: 0x040008C9 RID: 2249
		ERR_ImplicitlyTypedLocalCannotBeFixed,
		// Token: 0x040008CA RID: 2250
		ERR_ImplicitlyTypedVariableCannotBeConst,
		// Token: 0x040008CB RID: 2251
		WRN_ExternCtorNoImplementation = 824,
		// Token: 0x040008CC RID: 2252
		ERR_TypeVarNotFound,
		// Token: 0x040008CD RID: 2253
		ERR_ImplicitlyTypedArrayNoBestType,
		// Token: 0x040008CE RID: 2254
		ERR_AnonymousTypePropertyAssignedBadValue = 828,
		// Token: 0x040008CF RID: 2255
		ERR_ExpressionTreeContainsBaseAccess = 831,
		// Token: 0x040008D0 RID: 2256
		ERR_ExpressionTreeContainsAssignment,
		// Token: 0x040008D1 RID: 2257
		ERR_AnonymousTypeDuplicatePropertyName,
		// Token: 0x040008D2 RID: 2258
		ERR_StatementLambdaToExpressionTree,
		// Token: 0x040008D3 RID: 2259
		ERR_ExpressionTreeMustHaveDelegate,
		// Token: 0x040008D4 RID: 2260
		ERR_AnonymousTypeNotAvailable,
		// Token: 0x040008D5 RID: 2261
		ERR_LambdaInIsAs,
		// Token: 0x040008D6 RID: 2262
		ERR_ExpressionTreeContainsMultiDimensionalArrayInitializer,
		// Token: 0x040008D7 RID: 2263
		ERR_MissingArgument,
		// Token: 0x040008D8 RID: 2264
		ERR_VariableUsedBeforeDeclaration = 841,
		// Token: 0x040008D9 RID: 2265
		ERR_UnassignedThisAutoProperty = 843,
		// Token: 0x040008DA RID: 2266
		ERR_VariableUsedBeforeDeclarationAndHidesField,
		// Token: 0x040008DB RID: 2267
		ERR_ExpressionTreeContainsBadCoalesce,
		// Token: 0x040008DC RID: 2268
		ERR_ArrayInitializerExpected,
		// Token: 0x040008DD RID: 2269
		ERR_ArrayInitializerIncorrectLength,
		// Token: 0x040008DE RID: 2270
		ERR_ExpressionTreeContainsNamedArgument = 853,
		// Token: 0x040008DF RID: 2271
		ERR_ExpressionTreeContainsOptionalArgument,
		// Token: 0x040008E0 RID: 2272
		ERR_ExpressionTreeContainsIndexedProperty,
		// Token: 0x040008E1 RID: 2273
		ERR_IndexedPropertyRequiresParams,
		// Token: 0x040008E2 RID: 2274
		ERR_IndexedPropertyMustHaveAllOptionalParams,
		// Token: 0x040008E3 RID: 2275
		ERR_IdentifierExpected = 1001,
		// Token: 0x040008E4 RID: 2276
		ERR_SemicolonExpected,
		// Token: 0x040008E5 RID: 2277
		ERR_SyntaxError,
		// Token: 0x040008E6 RID: 2278
		ERR_DuplicateModifier,
		// Token: 0x040008E7 RID: 2279
		ERR_DuplicateAccessor = 1007,
		// Token: 0x040008E8 RID: 2280
		ERR_IntegralTypeExpected,
		// Token: 0x040008E9 RID: 2281
		ERR_IllegalEscape,
		// Token: 0x040008EA RID: 2282
		ERR_NewlineInConst,
		// Token: 0x040008EB RID: 2283
		ERR_EmptyCharConst,
		// Token: 0x040008EC RID: 2284
		ERR_TooManyCharsInConst,
		// Token: 0x040008ED RID: 2285
		ERR_InvalidNumber,
		// Token: 0x040008EE RID: 2286
		ERR_GetOrSetExpected,
		// Token: 0x040008EF RID: 2287
		ERR_ClassTypeExpected,
		// Token: 0x040008F0 RID: 2288
		ERR_NamedArgumentExpected,
		// Token: 0x040008F1 RID: 2289
		ERR_TooManyCatches,
		// Token: 0x040008F2 RID: 2290
		ERR_ThisOrBaseExpected,
		// Token: 0x040008F3 RID: 2291
		ERR_OvlUnaryOperatorExpected,
		// Token: 0x040008F4 RID: 2292
		ERR_OvlBinaryOperatorExpected,
		// Token: 0x040008F5 RID: 2293
		ERR_IntOverflow,
		// Token: 0x040008F6 RID: 2294
		ERR_EOFExpected,
		// Token: 0x040008F7 RID: 2295
		ERR_BadEmbeddedStmt,
		// Token: 0x040008F8 RID: 2296
		ERR_PPDirectiveExpected,
		// Token: 0x040008F9 RID: 2297
		ERR_EndOfPPLineExpected,
		// Token: 0x040008FA RID: 2298
		ERR_CloseParenExpected,
		// Token: 0x040008FB RID: 2299
		ERR_EndifDirectiveExpected,
		// Token: 0x040008FC RID: 2300
		ERR_UnexpectedDirective,
		// Token: 0x040008FD RID: 2301
		ERR_ErrorDirective,
		// Token: 0x040008FE RID: 2302
		WRN_WarningDirective,
		// Token: 0x040008FF RID: 2303
		ERR_TypeExpected,
		// Token: 0x04000900 RID: 2304
		ERR_PPDefFollowsToken,
		// Token: 0x04000901 RID: 2305
		ERR_OpenEndedComment = 1035,
		// Token: 0x04000902 RID: 2306
		ERR_OvlOperatorExpected = 1037,
		// Token: 0x04000903 RID: 2307
		ERR_EndRegionDirectiveExpected,
		// Token: 0x04000904 RID: 2308
		ERR_UnterminatedStringLit,
		// Token: 0x04000905 RID: 2309
		ERR_BadDirectivePlacement,
		// Token: 0x04000906 RID: 2310
		ERR_IdentifierExpectedKW,
		// Token: 0x04000907 RID: 2311
		ERR_SemiOrLBraceExpected = 1043,
		// Token: 0x04000908 RID: 2312
		ERR_MultiTypeInDeclaration,
		// Token: 0x04000909 RID: 2313
		ERR_AddOrRemoveExpected = 1055,
		// Token: 0x0400090A RID: 2314
		ERR_UnexpectedCharacter,
		// Token: 0x0400090B RID: 2315
		ERR_ProtectedInStatic,
		// Token: 0x0400090C RID: 2316
		WRN_UnreachableGeneralCatch,
		// Token: 0x0400090D RID: 2317
		ERR_IncrementLvalueExpected,
		// Token: 0x0400090E RID: 2318
		ERR_NoSuchMemberOrExtension = 1061,
		// Token: 0x0400090F RID: 2319
		WRN_DeprecatedCollectionInitAddStr,
		// Token: 0x04000910 RID: 2320
		ERR_DeprecatedCollectionInitAddStr,
		// Token: 0x04000911 RID: 2321
		WRN_DeprecatedCollectionInitAdd,
		// Token: 0x04000912 RID: 2322
		ERR_DefaultValueNotAllowed,
		// Token: 0x04000913 RID: 2323
		WRN_DefaultValueForUnconsumedLocation,
		// Token: 0x04000914 RID: 2324
		ERR_PartialWrongTypeParamsVariance,
		// Token: 0x04000915 RID: 2325
		ERR_GlobalSingleTypeNameNotFoundFwd,
		// Token: 0x04000916 RID: 2326
		ERR_DottedTypeNameNotFoundInNSFwd,
		// Token: 0x04000917 RID: 2327
		ERR_SingleTypeNameNotFoundFwd,
		// Token: 0x04000918 RID: 2328
		WRN_IdentifierOrNumericLiteralExpected = 1072,
		// Token: 0x04000919 RID: 2329
		ERR_UnexpectedToken,
		// Token: 0x0400091A RID: 2330
		ERR_BadThisParam = 1100,
		// Token: 0x0400091B RID: 2331
		ERR_BadTypeforThis = 1103,
		// Token: 0x0400091C RID: 2332
		ERR_BadParamModThis,
		// Token: 0x0400091D RID: 2333
		ERR_BadExtensionMeth,
		// Token: 0x0400091E RID: 2334
		ERR_BadExtensionAgg,
		// Token: 0x0400091F RID: 2335
		ERR_DupParamMod,
		// Token: 0x04000920 RID: 2336
		ERR_ExtensionMethodsDecl = 1109,
		// Token: 0x04000921 RID: 2337
		ERR_ExtensionAttrNotFound,
		// Token: 0x04000922 RID: 2338
		ERR_ExplicitExtension = 1112,
		// Token: 0x04000923 RID: 2339
		ERR_ValueTypeExtDelegate,
		// Token: 0x04000924 RID: 2340
		ERR_BadArgCount = 1501,
		// Token: 0x04000925 RID: 2341
		ERR_BadArgType = 1503,
		// Token: 0x04000926 RID: 2342
		ERR_NoSourceFile,
		// Token: 0x04000927 RID: 2343
		ERR_CantRefResource = 1507,
		// Token: 0x04000928 RID: 2344
		ERR_ResourceNotUnique,
		// Token: 0x04000929 RID: 2345
		ERR_ImportNonAssembly,
		// Token: 0x0400092A RID: 2346
		ERR_RefLvalueExpected,
		// Token: 0x0400092B RID: 2347
		ERR_BaseInStaticMeth,
		// Token: 0x0400092C RID: 2348
		ERR_BaseInBadContext,
		// Token: 0x0400092D RID: 2349
		ERR_RbraceExpected,
		// Token: 0x0400092E RID: 2350
		ERR_LbraceExpected,
		// Token: 0x0400092F RID: 2351
		ERR_InExpected,
		// Token: 0x04000930 RID: 2352
		ERR_InvalidPreprocExpr = 1517,
		// Token: 0x04000931 RID: 2353
		ERR_InvalidMemberDecl = 1519,
		// Token: 0x04000932 RID: 2354
		ERR_MemberNeedsType,
		// Token: 0x04000933 RID: 2355
		ERR_BadBaseType,
		// Token: 0x04000934 RID: 2356
		WRN_EmptySwitch,
		// Token: 0x04000935 RID: 2357
		ERR_ExpectedEndTry = 1524,
		// Token: 0x04000936 RID: 2358
		ERR_InvalidExprTerm,
		// Token: 0x04000937 RID: 2359
		ERR_BadNewExpr,
		// Token: 0x04000938 RID: 2360
		ERR_NoNamespacePrivate,
		// Token: 0x04000939 RID: 2361
		ERR_BadVarDecl,
		// Token: 0x0400093A RID: 2362
		ERR_UsingAfterElements,
		// Token: 0x0400093B RID: 2363
		ERR_BadBinOpArgs = 1534,
		// Token: 0x0400093C RID: 2364
		ERR_BadUnOpArgs,
		// Token: 0x0400093D RID: 2365
		ERR_NoVoidParameter,
		// Token: 0x0400093E RID: 2366
		ERR_DuplicateAlias,
		// Token: 0x0400093F RID: 2367
		ERR_BadProtectedAccess = 1540,
		// Token: 0x04000940 RID: 2368
		ERR_AddModuleAssembly = 1542,
		// Token: 0x04000941 RID: 2369
		ERR_BindToBogusProp2 = 1545,
		// Token: 0x04000942 RID: 2370
		ERR_BindToBogusProp1,
		// Token: 0x04000943 RID: 2371
		ERR_NoVoidHere,
		// Token: 0x04000944 RID: 2372
		ERR_IndexerNeedsParam = 1551,
		// Token: 0x04000945 RID: 2373
		ERR_BadArraySyntax,
		// Token: 0x04000946 RID: 2374
		ERR_BadOperatorSyntax,
		// Token: 0x04000947 RID: 2375
		ERR_MainClassNotFound = 1555,
		// Token: 0x04000948 RID: 2376
		ERR_MainClassNotClass,
		// Token: 0x04000949 RID: 2377
		ERR_NoMainInClass = 1558,
		// Token: 0x0400094A RID: 2378
		ERR_OutputNeedsName = 1562,
		// Token: 0x0400094B RID: 2379
		ERR_CantHaveWin32ResAndManifest = 1564,
		// Token: 0x0400094C RID: 2380
		ERR_CantHaveWin32ResAndIcon,
		// Token: 0x0400094D RID: 2381
		ERR_CantReadResource,
		// Token: 0x0400094E RID: 2382
		ERR_DocFileGen = 1569,
		// Token: 0x0400094F RID: 2383
		WRN_XMLParseError,
		// Token: 0x04000950 RID: 2384
		WRN_DuplicateParamTag,
		// Token: 0x04000951 RID: 2385
		WRN_UnmatchedParamTag,
		// Token: 0x04000952 RID: 2386
		WRN_MissingParamTag,
		// Token: 0x04000953 RID: 2387
		WRN_BadXMLRef,
		// Token: 0x04000954 RID: 2388
		ERR_BadStackAllocExpr,
		// Token: 0x04000955 RID: 2389
		ERR_InvalidLineNumber,
		// Token: 0x04000956 RID: 2390
		ERR_MissingPPFile = 1578,
		// Token: 0x04000957 RID: 2391
		ERR_ForEachMissingMember,
		// Token: 0x04000958 RID: 2392
		WRN_BadXMLRefParamType,
		// Token: 0x04000959 RID: 2393
		WRN_BadXMLRefReturnType,
		// Token: 0x0400095A RID: 2394
		ERR_BadWin32Res = 1583,
		// Token: 0x0400095B RID: 2395
		WRN_BadXMLRefSyntax,
		// Token: 0x0400095C RID: 2396
		ERR_BadModifierLocation,
		// Token: 0x0400095D RID: 2397
		ERR_MissingArraySize,
		// Token: 0x0400095E RID: 2398
		WRN_UnprocessedXMLComment,
		// Token: 0x0400095F RID: 2399
		WRN_FailedInclude = 1589,
		// Token: 0x04000960 RID: 2400
		WRN_InvalidInclude,
		// Token: 0x04000961 RID: 2401
		WRN_MissingXMLComment,
		// Token: 0x04000962 RID: 2402
		WRN_XMLParseIncludeError,
		// Token: 0x04000963 RID: 2403
		ERR_BadDelArgCount,
		// Token: 0x04000964 RID: 2404
		ERR_UnexpectedSemicolon = 1597,
		// Token: 0x04000965 RID: 2405
		ERR_MethodReturnCantBeRefAny = 1599,
		// Token: 0x04000966 RID: 2406
		ERR_CompileCancelled,
		// Token: 0x04000967 RID: 2407
		ERR_MethodArgCantBeRefAny,
		// Token: 0x04000968 RID: 2408
		ERR_AssgReadonlyLocal = 1604,
		// Token: 0x04000969 RID: 2409
		ERR_RefReadonlyLocal,
		// Token: 0x0400096A RID: 2410
		WRN_ALinkWarn = 1607,
		// Token: 0x0400096B RID: 2411
		ERR_CantUseRequiredAttribute,
		// Token: 0x0400096C RID: 2412
		ERR_NoModifiersOnAccessor,
		// Token: 0x0400096D RID: 2413
		ERR_ParamsCantBeWithModifier = 1611,
		// Token: 0x0400096E RID: 2414
		ERR_ReturnNotLValue,
		// Token: 0x0400096F RID: 2415
		ERR_MissingCoClass,
		// Token: 0x04000970 RID: 2416
		ERR_AmbiguousAttribute,
		// Token: 0x04000971 RID: 2417
		ERR_BadArgExtraRef,
		// Token: 0x04000972 RID: 2418
		WRN_CmdOptionConflictsSource,
		// Token: 0x04000973 RID: 2419
		ERR_BadCompatMode,
		// Token: 0x04000974 RID: 2420
		ERR_DelegateOnConditional,
		// Token: 0x04000975 RID: 2421
		ERR_CantMakeTempFile,
		// Token: 0x04000976 RID: 2422
		ERR_BadArgRef,
		// Token: 0x04000977 RID: 2423
		ERR_YieldInAnonMeth,
		// Token: 0x04000978 RID: 2424
		ERR_ReturnInIterator,
		// Token: 0x04000979 RID: 2425
		ERR_BadIteratorArgType,
		// Token: 0x0400097A RID: 2426
		ERR_BadIteratorReturn,
		// Token: 0x0400097B RID: 2427
		ERR_BadYieldInFinally,
		// Token: 0x0400097C RID: 2428
		ERR_BadYieldInTryOfCatch,
		// Token: 0x0400097D RID: 2429
		ERR_EmptyYield,
		// Token: 0x0400097E RID: 2430
		ERR_AnonDelegateCantUse,
		// Token: 0x0400097F RID: 2431
		ERR_IllegalInnerUnsafe,
		// Token: 0x04000980 RID: 2432
		ERR_BadYieldInCatch = 1631,
		// Token: 0x04000981 RID: 2433
		ERR_BadDelegateLeave,
		// Token: 0x04000982 RID: 2434
		WRN_IllegalPragma,
		// Token: 0x04000983 RID: 2435
		WRN_IllegalPPWarning,
		// Token: 0x04000984 RID: 2436
		WRN_BadRestoreNumber,
		// Token: 0x04000985 RID: 2437
		ERR_VarargsIterator,
		// Token: 0x04000986 RID: 2438
		ERR_UnsafeIteratorArgType,
		// Token: 0x04000987 RID: 2439
		ERR_BadCoClassSig = 1639,
		// Token: 0x04000988 RID: 2440
		ERR_MultipleIEnumOfT,
		// Token: 0x04000989 RID: 2441
		ERR_FixedDimsRequired,
		// Token: 0x0400098A RID: 2442
		ERR_FixedNotInStruct,
		// Token: 0x0400098B RID: 2443
		ERR_AnonymousReturnExpected,
		// Token: 0x0400098C RID: 2444
		WRN_NonECMAFeature = 1645,
		// Token: 0x0400098D RID: 2445
		ERR_ExpectedVerbatimLiteral,
		// Token: 0x0400098E RID: 2446
		ERR_AssgReadonly2 = 1648,
		// Token: 0x0400098F RID: 2447
		ERR_RefReadonly2,
		// Token: 0x04000990 RID: 2448
		ERR_AssgReadonlyStatic2,
		// Token: 0x04000991 RID: 2449
		ERR_RefReadonlyStatic2,
		// Token: 0x04000992 RID: 2450
		ERR_AssgReadonlyLocal2Cause = 1654,
		// Token: 0x04000993 RID: 2451
		ERR_RefReadonlyLocal2Cause,
		// Token: 0x04000994 RID: 2452
		ERR_AssgReadonlyLocalCause,
		// Token: 0x04000995 RID: 2453
		ERR_RefReadonlyLocalCause,
		// Token: 0x04000996 RID: 2454
		WRN_ErrorOverride,
		// Token: 0x04000997 RID: 2455
		ERR_AnonMethToNonDel = 1660,
		// Token: 0x04000998 RID: 2456
		ERR_CantConvAnonMethParams,
		// Token: 0x04000999 RID: 2457
		ERR_CantConvAnonMethReturns,
		// Token: 0x0400099A RID: 2458
		ERR_IllegalFixedType,
		// Token: 0x0400099B RID: 2459
		ERR_FixedOverflow,
		// Token: 0x0400099C RID: 2460
		ERR_InvalidFixedArraySize,
		// Token: 0x0400099D RID: 2461
		ERR_FixedBufferNotFixed,
		// Token: 0x0400099E RID: 2462
		ERR_AttributeNotOnAccessor,
		// Token: 0x0400099F RID: 2463
		WRN_InvalidSearchPathDir,
		// Token: 0x040009A0 RID: 2464
		ERR_IllegalVarArgs,
		// Token: 0x040009A1 RID: 2465
		ERR_IllegalParams,
		// Token: 0x040009A2 RID: 2466
		ERR_BadModifiersOnNamespace,
		// Token: 0x040009A3 RID: 2467
		ERR_BadPlatformType,
		// Token: 0x040009A4 RID: 2468
		ERR_ThisStructNotInAnonMeth,
		// Token: 0x040009A5 RID: 2469
		ERR_NoConvToIDisp,
		// Token: 0x040009A6 RID: 2470
		ERR_BadParamRef = 1676,
		// Token: 0x040009A7 RID: 2471
		ERR_BadParamExtraRef,
		// Token: 0x040009A8 RID: 2472
		ERR_BadParamType,
		// Token: 0x040009A9 RID: 2473
		ERR_BadExternIdentifier,
		// Token: 0x040009AA RID: 2474
		ERR_AliasMissingFile,
		// Token: 0x040009AB RID: 2475
		ERR_GlobalExternAlias,
		// Token: 0x040009AC RID: 2476
		WRN_MultiplePredefTypes = 1685,
		// Token: 0x040009AD RID: 2477
		ERR_LocalCantBeFixedAndHoisted,
		// Token: 0x040009AE RID: 2478
		WRN_TooManyLinesForDebugger,
		// Token: 0x040009AF RID: 2479
		ERR_CantConvAnonMethNoParams,
		// Token: 0x040009B0 RID: 2480
		ERR_ConditionalOnNonAttributeClass,
		// Token: 0x040009B1 RID: 2481
		WRN_CallOnNonAgileField,
		// Token: 0x040009B2 RID: 2482
		WRN_InvalidNumber = 1692,
		// Token: 0x040009B3 RID: 2483
		WRN_IllegalPPChecksum = 1695,
		// Token: 0x040009B4 RID: 2484
		WRN_EndOfPPLineExpected,
		// Token: 0x040009B5 RID: 2485
		WRN_ConflictingChecksum,
		// Token: 0x040009B6 RID: 2486
		WRN_InvalidAssemblyName = 1700,
		// Token: 0x040009B7 RID: 2487
		WRN_UnifyReferenceMajMin,
		// Token: 0x040009B8 RID: 2488
		WRN_UnifyReferenceBldRev,
		// Token: 0x040009B9 RID: 2489
		ERR_DuplicateImport,
		// Token: 0x040009BA RID: 2490
		ERR_DuplicateImportSimple,
		// Token: 0x040009BB RID: 2491
		ERR_AssemblyMatchBadVersion,
		// Token: 0x040009BC RID: 2492
		ERR_FixedNeedsLvalue = 1708,
		// Token: 0x040009BD RID: 2493
		WRN_DuplicateTypeParamTag = 1710,
		// Token: 0x040009BE RID: 2494
		WRN_UnmatchedTypeParamTag,
		// Token: 0x040009BF RID: 2495
		WRN_MissingTypeParamTag,
		// Token: 0x040009C0 RID: 2496
		ERR_CantChangeTypeOnOverride = 1715,
		// Token: 0x040009C1 RID: 2497
		ERR_DoNotUseFixedBufferAttr,
		// Token: 0x040009C2 RID: 2498
		WRN_AssignmentToSelf,
		// Token: 0x040009C3 RID: 2499
		WRN_ComparisonToSelf,
		// Token: 0x040009C4 RID: 2500
		ERR_CantOpenWin32Res,
		// Token: 0x040009C5 RID: 2501
		WRN_DotOnDefault,
		// Token: 0x040009C6 RID: 2502
		ERR_NoMultipleInheritance,
		// Token: 0x040009C7 RID: 2503
		ERR_BaseClassMustBeFirst,
		// Token: 0x040009C8 RID: 2504
		WRN_BadXMLRefTypeVar,
		// Token: 0x040009C9 RID: 2505
		ERR_FriendAssemblyBadArgs = 1725,
		// Token: 0x040009CA RID: 2506
		ERR_FriendAssemblySNReq,
		// Token: 0x040009CB RID: 2507
		ERR_DelegateOnNullable = 1728,
		// Token: 0x040009CC RID: 2508
		ERR_BadCtorArgCount,
		// Token: 0x040009CD RID: 2509
		ERR_GlobalAttributesNotFirst,
		// Token: 0x040009CE RID: 2510
		ERR_ExpressionExpected = 1733,
		// Token: 0x040009CF RID: 2511
		WRN_UnmatchedParamRefTag,
		// Token: 0x040009D0 RID: 2512
		WRN_UnmatchedTypeParamRefTag,
		// Token: 0x040009D1 RID: 2513
		ERR_DefaultValueMustBeConstant,
		// Token: 0x040009D2 RID: 2514
		ERR_DefaultValueBeforeRequiredValue,
		// Token: 0x040009D3 RID: 2515
		ERR_NamedArgumentSpecificationBeforeFixedArgument,
		// Token: 0x040009D4 RID: 2516
		ERR_BadNamedArgument,
		// Token: 0x040009D5 RID: 2517
		ERR_DuplicateNamedArgument,
		// Token: 0x040009D6 RID: 2518
		ERR_RefOutDefaultValue,
		// Token: 0x040009D7 RID: 2519
		ERR_NamedArgumentForArray,
		// Token: 0x040009D8 RID: 2520
		ERR_DefaultValueForExtensionParameter,
		// Token: 0x040009D9 RID: 2521
		ERR_NamedArgumentUsedInPositional,
		// Token: 0x040009DA RID: 2522
		ERR_DefaultValueUsedWithAttributes,
		// Token: 0x040009DB RID: 2523
		ERR_BadNamedArgumentForDelegateInvoke,
		// Token: 0x040009DC RID: 2524
		ERR_NoPIAAssemblyMissingAttribute,
		// Token: 0x040009DD RID: 2525
		ERR_NoCanonicalView,
		// Token: 0x040009DE RID: 2526
		ERR_NoConversionForDefaultParam = 1750,
		// Token: 0x040009DF RID: 2527
		ERR_DefaultValueForParamsParameter,
		// Token: 0x040009E0 RID: 2528
		ERR_NewCoClassOnLink,
		// Token: 0x040009E1 RID: 2529
		ERR_NoPIANestedType = 1754,
		// Token: 0x040009E2 RID: 2530
		ERR_InteropTypeMissingAttribute = 1756,
		// Token: 0x040009E3 RID: 2531
		ERR_InteropStructContainsMethods,
		// Token: 0x040009E4 RID: 2532
		ERR_InteropTypesWithSameNameAndGuid,
		// Token: 0x040009E5 RID: 2533
		ERR_NoPIAAssemblyMissingAttributes,
		// Token: 0x040009E6 RID: 2534
		ERR_AssemblySpecifiedForLinkAndRef,
		// Token: 0x040009E7 RID: 2535
		ERR_LocalTypeNameClash,
		// Token: 0x040009E8 RID: 2536
		WRN_ReferencedAssemblyReferencesLinkedPIA,
		// Token: 0x040009E9 RID: 2537
		ERR_NotNullRefDefaultParameter,
		// Token: 0x040009EA RID: 2538
		ERR_FixedLocalInLambda,
		// Token: 0x040009EB RID: 2539
		ERR_MissingMethodOnSourceInterface = 1766,
		// Token: 0x040009EC RID: 2540
		ERR_MissingSourceInterface,
		// Token: 0x040009ED RID: 2541
		ERR_GenericsUsedInNoPIAType,
		// Token: 0x040009EE RID: 2542
		ERR_GenericsUsedAcrossAssemblies,
		// Token: 0x040009EF RID: 2543
		ERR_NoConversionForNubDefaultParam,
		// Token: 0x040009F0 RID: 2544
		ERR_InvalidSubsystemVersion = 1773,
		// Token: 0x040009F1 RID: 2545
		ERR_InteropMethodWithBody,
		// Token: 0x040009F2 RID: 2546
		ERR_BadWarningLevel = 1900,
		// Token: 0x040009F3 RID: 2547
		ERR_BadDebugType = 1902,
		// Token: 0x040009F4 RID: 2548
		ERR_BadResourceVis = 1906,
		// Token: 0x040009F5 RID: 2549
		ERR_DefaultValueTypeMustMatch = 1908,
		// Token: 0x040009F6 RID: 2550
		ERR_DefaultValueBadValueType = 1910,
		// Token: 0x040009F7 RID: 2551
		ERR_MemberAlreadyInitialized = 1912,
		// Token: 0x040009F8 RID: 2552
		ERR_MemberCannotBeInitialized,
		// Token: 0x040009F9 RID: 2553
		ERR_StaticMemberInObjectInitializer,
		// Token: 0x040009FA RID: 2554
		ERR_ReadonlyValueTypeInObjectInitializer = 1917,
		// Token: 0x040009FB RID: 2555
		ERR_ValueTypePropertyInObjectInitializer,
		// Token: 0x040009FC RID: 2556
		ERR_UnsafeTypeInObjectCreation,
		// Token: 0x040009FD RID: 2557
		ERR_EmptyElementInitializer,
		// Token: 0x040009FE RID: 2558
		ERR_InitializerAddHasWrongSignature,
		// Token: 0x040009FF RID: 2559
		ERR_CollectionInitRequiresIEnumerable,
		// Token: 0x04000A00 RID: 2560
		ERR_CantOpenWin32Manifest = 1926,
		// Token: 0x04000A01 RID: 2561
		WRN_CantHaveManifestForModule,
		// Token: 0x04000A02 RID: 2562
		ERR_BadInstanceArgType = 1929,
		// Token: 0x04000A03 RID: 2563
		ERR_QueryDuplicateRangeVariable,
		// Token: 0x04000A04 RID: 2564
		ERR_QueryRangeVariableOverrides,
		// Token: 0x04000A05 RID: 2565
		ERR_QueryRangeVariableAssignedBadValue,
		// Token: 0x04000A06 RID: 2566
		ERR_QueryNoProviderCastable = 1934,
		// Token: 0x04000A07 RID: 2567
		ERR_QueryNoProviderStandard,
		// Token: 0x04000A08 RID: 2568
		ERR_QueryNoProvider,
		// Token: 0x04000A09 RID: 2569
		ERR_QueryOuterKey,
		// Token: 0x04000A0A RID: 2570
		ERR_QueryInnerKey,
		// Token: 0x04000A0B RID: 2571
		ERR_QueryOutRefRangeVariable,
		// Token: 0x04000A0C RID: 2572
		ERR_QueryMultipleProviders,
		// Token: 0x04000A0D RID: 2573
		ERR_QueryTypeInferenceFailedMulti,
		// Token: 0x04000A0E RID: 2574
		ERR_QueryTypeInferenceFailed,
		// Token: 0x04000A0F RID: 2575
		ERR_QueryTypeInferenceFailedSelectMany,
		// Token: 0x04000A10 RID: 2576
		ERR_ExpressionTreeContainsPointerOp,
		// Token: 0x04000A11 RID: 2577
		ERR_ExpressionTreeContainsAnonymousMethod,
		// Token: 0x04000A12 RID: 2578
		ERR_AnonymousMethodToExpressionTree,
		// Token: 0x04000A13 RID: 2579
		ERR_QueryRangeVariableReadOnly,
		// Token: 0x04000A14 RID: 2580
		ERR_QueryRangeVariableSameAsTypeParam,
		// Token: 0x04000A15 RID: 2581
		ERR_TypeVarNotFoundRangeVariable,
		// Token: 0x04000A16 RID: 2582
		ERR_BadArgTypesForCollectionAdd,
		// Token: 0x04000A17 RID: 2583
		ERR_ByRefParameterInExpressionTree,
		// Token: 0x04000A18 RID: 2584
		ERR_VarArgsInExpressionTree,
		// Token: 0x04000A19 RID: 2585
		ERR_InitializerAddHasParamModifiers = 1954,
		// Token: 0x04000A1A RID: 2586
		ERR_NonInvocableMemberCalled,
		// Token: 0x04000A1B RID: 2587
		WRN_MultipleRuntimeImplementationMatches,
		// Token: 0x04000A1C RID: 2588
		WRN_MultipleRuntimeOverrideMatches,
		// Token: 0x04000A1D RID: 2589
		ERR_ObjectOrCollectionInitializerWithDelegateCreation,
		// Token: 0x04000A1E RID: 2590
		ERR_InvalidConstantDeclarationType,
		// Token: 0x04000A1F RID: 2591
		ERR_IllegalVarianceSyntax,
		// Token: 0x04000A20 RID: 2592
		ERR_UnexpectedVariance,
		// Token: 0x04000A21 RID: 2593
		ERR_BadDynamicTypeof,
		// Token: 0x04000A22 RID: 2594
		ERR_ExpressionTreeContainsDynamicOperation,
		// Token: 0x04000A23 RID: 2595
		ERR_BadDynamicConversion,
		// Token: 0x04000A24 RID: 2596
		ERR_DeriveFromDynamic,
		// Token: 0x04000A25 RID: 2597
		ERR_DeriveFromConstructedDynamic,
		// Token: 0x04000A26 RID: 2598
		ERR_DynamicTypeAsBound,
		// Token: 0x04000A27 RID: 2599
		ERR_ConstructedDynamicTypeAsBound,
		// Token: 0x04000A28 RID: 2600
		ERR_DynamicRequiredTypesMissing,
		// Token: 0x04000A29 RID: 2601
		ERR_ExplicitDynamicAttr,
		// Token: 0x04000A2A RID: 2602
		ERR_NoDynamicPhantomOnBase,
		// Token: 0x04000A2B RID: 2603
		ERR_NoDynamicPhantomOnBaseIndexer,
		// Token: 0x04000A2C RID: 2604
		ERR_BadArgTypeDynamicExtension,
		// Token: 0x04000A2D RID: 2605
		WRN_DynamicDispatchToConditionalMethod,
		// Token: 0x04000A2E RID: 2606
		ERR_NoDynamicPhantomOnBaseCtor,
		// Token: 0x04000A2F RID: 2607
		ERR_BadDynamicMethodArgMemgrp,
		// Token: 0x04000A30 RID: 2608
		ERR_BadDynamicMethodArgLambda,
		// Token: 0x04000A31 RID: 2609
		ERR_BadDynamicMethodArg,
		// Token: 0x04000A32 RID: 2610
		ERR_BadDynamicQuery,
		// Token: 0x04000A33 RID: 2611
		ERR_DynamicAttributeMissing,
		// Token: 0x04000A34 RID: 2612
		WRN_IsDynamicIsConfusing,
		// Token: 0x04000A35 RID: 2613
		ERR_BadAsyncReturn = 1983,
		// Token: 0x04000A36 RID: 2614
		ERR_BadAwaitInFinally,
		// Token: 0x04000A37 RID: 2615
		ERR_BadAwaitInCatch,
		// Token: 0x04000A38 RID: 2616
		ERR_BadAwaitArg,
		// Token: 0x04000A39 RID: 2617
		ERR_BadAsyncArgType = 1988,
		// Token: 0x04000A3A RID: 2618
		ERR_BadAsyncExpressionTree,
		// Token: 0x04000A3B RID: 2619
		ERR_MixingWinRTEventWithRegular = 1991,
		// Token: 0x04000A3C RID: 2620
		ERR_BadAwaitWithoutAsync,
		// Token: 0x04000A3D RID: 2621
		ERR_BadAsyncLacksBody = 1994,
		// Token: 0x04000A3E RID: 2622
		ERR_BadAwaitInQuery,
		// Token: 0x04000A3F RID: 2623
		ERR_BadAwaitInLock,
		// Token: 0x04000A40 RID: 2624
		ERR_TaskRetNoObjectRequired,
		// Token: 0x04000A41 RID: 2625
		WRN_AsyncLacksAwaits,
		// Token: 0x04000A42 RID: 2626
		ERR_FileNotFound = 2001,
		// Token: 0x04000A43 RID: 2627
		WRN_FileAlreadyIncluded,
		// Token: 0x04000A44 RID: 2628
		ERR_NoFileSpec = 2005,
		// Token: 0x04000A45 RID: 2629
		ERR_SwitchNeedsString,
		// Token: 0x04000A46 RID: 2630
		ERR_BadSwitch,
		// Token: 0x04000A47 RID: 2631
		WRN_NoSources,
		// Token: 0x04000A48 RID: 2632
		ERR_OpenResponseFile = 2011,
		// Token: 0x04000A49 RID: 2633
		ERR_CantOpenFileWrite,
		// Token: 0x04000A4A RID: 2634
		ERR_BadBaseNumber,
		// Token: 0x04000A4B RID: 2635
		ERR_BinaryFile = 2015,
		// Token: 0x04000A4C RID: 2636
		FTL_BadCodepage,
		// Token: 0x04000A4D RID: 2637
		ERR_NoMainOnDLL,
		// Token: 0x04000A4E RID: 2638
		FTL_InvalidTarget = 2019,
		// Token: 0x04000A4F RID: 2639
		FTL_InvalidInputFileName = 2021,
		// Token: 0x04000A50 RID: 2640
		WRN_NoConfigNotOnCommandLine = 2023,
		// Token: 0x04000A51 RID: 2641
		ERR_InvalidFileAlignment,
		// Token: 0x04000A52 RID: 2642
		WRN_DefineIdentifierRequired = 2029,
		// Token: 0x04000A53 RID: 2643
		FTL_OutputFileExists = 2033,
		// Token: 0x04000A54 RID: 2644
		ERR_OneAliasPerReference,
		// Token: 0x04000A55 RID: 2645
		ERR_SwitchNeedsNumber,
		// Token: 0x04000A56 RID: 2646
		ERR_MissingDebugSwitch,
		// Token: 0x04000A57 RID: 2647
		ERR_ComRefCallInExpressionTree,
		// Token: 0x04000A58 RID: 2648
		WRN_BadUILang,
		// Token: 0x04000A59 RID: 2649
		ERR_InvalidFormatForGuidForOption,
		// Token: 0x04000A5A RID: 2650
		ERR_MissingGuidForOption,
		// Token: 0x04000A5B RID: 2651
		ERR_InvalidOutputName,
		// Token: 0x04000A5C RID: 2652
		ERR_InvalidDebugInformationFormat,
		// Token: 0x04000A5D RID: 2653
		ERR_LegacyObjectIdSyntax,
		// Token: 0x04000A5E RID: 2654
		ERR_SourceLinkRequiresPdb,
		// Token: 0x04000A5F RID: 2655
		ERR_CannotEmbedWithoutPdb,
		// Token: 0x04000A60 RID: 2656
		ERR_BadSwitchValue,
		// Token: 0x04000A61 RID: 2657
		WRN_CLS_NoVarArgs = 3000,
		// Token: 0x04000A62 RID: 2658
		WRN_CLS_BadArgType,
		// Token: 0x04000A63 RID: 2659
		WRN_CLS_BadReturnType,
		// Token: 0x04000A64 RID: 2660
		WRN_CLS_BadFieldPropType,
		// Token: 0x04000A65 RID: 2661
		WRN_CLS_BadIdentifierCase = 3005,
		// Token: 0x04000A66 RID: 2662
		WRN_CLS_OverloadRefOut,
		// Token: 0x04000A67 RID: 2663
		WRN_CLS_OverloadUnnamed,
		// Token: 0x04000A68 RID: 2664
		WRN_CLS_BadIdentifier,
		// Token: 0x04000A69 RID: 2665
		WRN_CLS_BadBase,
		// Token: 0x04000A6A RID: 2666
		WRN_CLS_BadInterfaceMember,
		// Token: 0x04000A6B RID: 2667
		WRN_CLS_NoAbstractMembers,
		// Token: 0x04000A6C RID: 2668
		WRN_CLS_NotOnModules,
		// Token: 0x04000A6D RID: 2669
		WRN_CLS_ModuleMissingCLS,
		// Token: 0x04000A6E RID: 2670
		WRN_CLS_AssemblyNotCLS,
		// Token: 0x04000A6F RID: 2671
		WRN_CLS_BadAttributeType,
		// Token: 0x04000A70 RID: 2672
		WRN_CLS_ArrayArgumentToAttribute,
		// Token: 0x04000A71 RID: 2673
		WRN_CLS_NotOnModules2,
		// Token: 0x04000A72 RID: 2674
		WRN_CLS_IllegalTrueInFalse,
		// Token: 0x04000A73 RID: 2675
		WRN_CLS_MeaninglessOnPrivateType,
		// Token: 0x04000A74 RID: 2676
		WRN_CLS_AssemblyNotCLS2 = 3021,
		// Token: 0x04000A75 RID: 2677
		WRN_CLS_MeaninglessOnParam,
		// Token: 0x04000A76 RID: 2678
		WRN_CLS_MeaninglessOnReturn,
		// Token: 0x04000A77 RID: 2679
		WRN_CLS_BadTypeVar,
		// Token: 0x04000A78 RID: 2680
		WRN_CLS_VolatileField = 3026,
		// Token: 0x04000A79 RID: 2681
		WRN_CLS_BadInterface,
		// Token: 0x04000A7A RID: 2682
		FTL_BadChecksumAlgorithm,
		// Token: 0x04000A7B RID: 2683
		ERR_BadAwaitArgIntrinsic = 4001,
		// Token: 0x04000A7C RID: 2684
		ERR_BadAwaitAsIdentifier = 4003,
		// Token: 0x04000A7D RID: 2685
		ERR_AwaitInUnsafeContext,
		// Token: 0x04000A7E RID: 2686
		ERR_UnsafeAsyncArgType,
		// Token: 0x04000A7F RID: 2687
		ERR_VarargsAsync,
		// Token: 0x04000A80 RID: 2688
		ERR_ByRefTypeAndAwait,
		// Token: 0x04000A81 RID: 2689
		ERR_BadAwaitArgVoidCall,
		// Token: 0x04000A82 RID: 2690
		ERR_NonTaskMainCantBeAsync,
		// Token: 0x04000A83 RID: 2691
		ERR_CantConvAsyncAnonFuncReturns,
		// Token: 0x04000A84 RID: 2692
		ERR_BadAwaiterPattern,
		// Token: 0x04000A85 RID: 2693
		ERR_BadSpecialByRefLocal,
		// Token: 0x04000A86 RID: 2694
		ERR_SpecialByRefInLambda,
		// Token: 0x04000A87 RID: 2695
		WRN_UnobservedAwaitableExpression,
		// Token: 0x04000A88 RID: 2696
		ERR_SynchronizedAsyncMethod,
		// Token: 0x04000A89 RID: 2697
		ERR_BadAsyncReturnExpression,
		// Token: 0x04000A8A RID: 2698
		ERR_NoConversionForCallerLineNumberParam,
		// Token: 0x04000A8B RID: 2699
		ERR_NoConversionForCallerFilePathParam,
		// Token: 0x04000A8C RID: 2700
		ERR_NoConversionForCallerMemberNameParam,
		// Token: 0x04000A8D RID: 2701
		ERR_BadCallerLineNumberParamWithoutDefaultValue,
		// Token: 0x04000A8E RID: 2702
		ERR_BadCallerFilePathParamWithoutDefaultValue,
		// Token: 0x04000A8F RID: 2703
		ERR_BadCallerMemberNameParamWithoutDefaultValue,
		// Token: 0x04000A90 RID: 2704
		ERR_BadPrefer32OnLib,
		// Token: 0x04000A91 RID: 2705
		WRN_CallerLineNumberParamForUnconsumedLocation,
		// Token: 0x04000A92 RID: 2706
		WRN_CallerFilePathParamForUnconsumedLocation,
		// Token: 0x04000A93 RID: 2707
		WRN_CallerMemberNameParamForUnconsumedLocation,
		// Token: 0x04000A94 RID: 2708
		ERR_DoesntImplementAwaitInterface,
		// Token: 0x04000A95 RID: 2709
		ERR_BadAwaitArg_NeedSystem,
		// Token: 0x04000A96 RID: 2710
		ERR_CantReturnVoid,
		// Token: 0x04000A97 RID: 2711
		ERR_SecurityCriticalOrSecuritySafeCriticalOnAsync,
		// Token: 0x04000A98 RID: 2712
		ERR_SecurityCriticalOrSecuritySafeCriticalOnAsyncInClassOrStruct,
		// Token: 0x04000A99 RID: 2713
		ERR_BadAwaitWithoutAsyncMethod,
		// Token: 0x04000A9A RID: 2714
		ERR_BadAwaitWithoutVoidAsyncMethod,
		// Token: 0x04000A9B RID: 2715
		ERR_BadAwaitWithoutAsyncLambda,
		// Token: 0x04000A9C RID: 2716
		ERR_NoSuchMemberOrExtensionNeedUsing = 4036,
		// Token: 0x04000A9D RID: 2717
		ERR_NoEntryPoint = 5001,
		// Token: 0x04000A9E RID: 2718
		ERR_UnexpectedAliasedName = 7000,
		// Token: 0x04000A9F RID: 2719
		ERR_UnexpectedGenericName = 7002,
		// Token: 0x04000AA0 RID: 2720
		ERR_UnexpectedUnboundGenericName,
		// Token: 0x04000AA1 RID: 2721
		ERR_GlobalStatement = 7006,
		// Token: 0x04000AA2 RID: 2722
		ERR_BadUsingType,
		// Token: 0x04000AA3 RID: 2723
		ERR_ReservedAssemblyName,
		// Token: 0x04000AA4 RID: 2724
		ERR_PPReferenceFollowsToken,
		// Token: 0x04000AA5 RID: 2725
		ERR_ExpectedPPFile,
		// Token: 0x04000AA6 RID: 2726
		ERR_ReferenceDirectiveOnlyAllowedInScripts,
		// Token: 0x04000AA7 RID: 2727
		ERR_NameNotInContextPossibleMissingReference,
		// Token: 0x04000AA8 RID: 2728
		ERR_MetadataNameTooLong,
		// Token: 0x04000AA9 RID: 2729
		ERR_AttributesNotAllowed,
		// Token: 0x04000AAA RID: 2730
		ERR_ExternAliasNotAllowed,
		// Token: 0x04000AAB RID: 2731
		ERR_ConflictingAliasAndDefinition,
		// Token: 0x04000AAC RID: 2732
		ERR_GlobalDefinitionOrStatementExpected,
		// Token: 0x04000AAD RID: 2733
		ERR_ExpectedSingleScript,
		// Token: 0x04000AAE RID: 2734
		ERR_RecursivelyTypedVariable,
		// Token: 0x04000AAF RID: 2735
		ERR_YieldNotAllowedInScript,
		// Token: 0x04000AB0 RID: 2736
		ERR_NamespaceNotAllowedInScript,
		// Token: 0x04000AB1 RID: 2737
		WRN_MainIgnored,
		// Token: 0x04000AB2 RID: 2738
		WRN_StaticInAsOrIs,
		// Token: 0x04000AB3 RID: 2739
		ERR_InvalidDelegateType,
		// Token: 0x04000AB4 RID: 2740
		ERR_BadVisEventType,
		// Token: 0x04000AB5 RID: 2741
		ERR_GlobalAttributesNotAllowed,
		// Token: 0x04000AB6 RID: 2742
		ERR_PublicKeyFileFailure,
		// Token: 0x04000AB7 RID: 2743
		ERR_PublicKeyContainerFailure,
		// Token: 0x04000AB8 RID: 2744
		ERR_FriendRefSigningMismatch,
		// Token: 0x04000AB9 RID: 2745
		ERR_CannotPassNullForFriendAssembly,
		// Token: 0x04000ABA RID: 2746
		ERR_SignButNoPrivateKey = 7032,
		// Token: 0x04000ABB RID: 2747
		WRN_DelaySignButNoKey,
		// Token: 0x04000ABC RID: 2748
		ERR_InvalidVersionFormat,
		// Token: 0x04000ABD RID: 2749
		WRN_InvalidVersionFormat,
		// Token: 0x04000ABE RID: 2750
		ERR_NoCorrespondingArgument,
		// Token: 0x04000ABF RID: 2751
		ERR_ModuleEmitFailure = 7038,
		// Token: 0x04000AC0 RID: 2752
		ERR_ResourceFileNameNotUnique = 7041,
		// Token: 0x04000AC1 RID: 2753
		ERR_DllImportOnGenericMethod,
		// Token: 0x04000AC2 RID: 2754
		ERR_EncUpdateFailedMissingAttribute,
		// Token: 0x04000AC3 RID: 2755
		ERR_ParameterNotValidForType = 7045,
		// Token: 0x04000AC4 RID: 2756
		ERR_AttributeParameterRequired1,
		// Token: 0x04000AC5 RID: 2757
		ERR_AttributeParameterRequired2,
		// Token: 0x04000AC6 RID: 2758
		ERR_SecurityAttributeMissingAction,
		// Token: 0x04000AC7 RID: 2759
		ERR_SecurityAttributeInvalidAction,
		// Token: 0x04000AC8 RID: 2760
		ERR_SecurityAttributeInvalidActionAssembly,
		// Token: 0x04000AC9 RID: 2761
		ERR_SecurityAttributeInvalidActionTypeOrMethod,
		// Token: 0x04000ACA RID: 2762
		ERR_PrincipalPermissionInvalidAction,
		// Token: 0x04000ACB RID: 2763
		ERR_FeatureNotValidInExpressionTree,
		// Token: 0x04000ACC RID: 2764
		ERR_MarshalUnmanagedTypeNotValidForFields,
		// Token: 0x04000ACD RID: 2765
		ERR_MarshalUnmanagedTypeOnlyValidForFields,
		// Token: 0x04000ACE RID: 2766
		ERR_PermissionSetAttributeInvalidFile,
		// Token: 0x04000ACF RID: 2767
		ERR_PermissionSetAttributeFileReadError,
		// Token: 0x04000AD0 RID: 2768
		ERR_InvalidVersionFormat2,
		// Token: 0x04000AD1 RID: 2769
		ERR_InvalidAssemblyCultureForExe,
		// Token: 0x04000AD2 RID: 2770
		ERR_DuplicateAttributeInNetModule = 7061,
		// Token: 0x04000AD3 RID: 2771
		ERR_CantOpenIcon = 7064,
		// Token: 0x04000AD4 RID: 2772
		ERR_ErrorBuildingWin32Resources,
		// Token: 0x04000AD5 RID: 2773
		ERR_IteratorInInteractive,
		// Token: 0x04000AD6 RID: 2774
		ERR_BadAttributeParamDefaultArgument,
		// Token: 0x04000AD7 RID: 2775
		ERR_MissingTypeInSource,
		// Token: 0x04000AD8 RID: 2776
		ERR_MissingTypeInAssembly,
		// Token: 0x04000AD9 RID: 2777
		ERR_SecurityAttributeInvalidTarget,
		// Token: 0x04000ADA RID: 2778
		ERR_InvalidAssemblyName,
		// Token: 0x04000ADB RID: 2779
		ERR_NoTypeDefFromModule = 7079,
		// Token: 0x04000ADC RID: 2780
		WRN_CallerFilePathPreferredOverCallerMemberName,
		// Token: 0x04000ADD RID: 2781
		WRN_CallerLineNumberPreferredOverCallerMemberName,
		// Token: 0x04000ADE RID: 2782
		WRN_CallerLineNumberPreferredOverCallerFilePath,
		// Token: 0x04000ADF RID: 2783
		ERR_InvalidDynamicCondition,
		// Token: 0x04000AE0 RID: 2784
		ERR_WinRtEventPassedByRef,
		// Token: 0x04000AE1 RID: 2785
		ERR_NetModuleNameMismatch = 7086,
		// Token: 0x04000AE2 RID: 2786
		ERR_BadModuleName,
		// Token: 0x04000AE3 RID: 2787
		ERR_BadCompilationOptionValue,
		// Token: 0x04000AE4 RID: 2788
		ERR_BadAppConfigPath,
		// Token: 0x04000AE5 RID: 2789
		WRN_AssemblyAttributeFromModuleIsOverridden,
		// Token: 0x04000AE6 RID: 2790
		ERR_CmdOptionConflictsSource,
		// Token: 0x04000AE7 RID: 2791
		ERR_FixedBufferTooManyDimensions,
		// Token: 0x04000AE8 RID: 2792
		ERR_CantReadConfigFile,
		// Token: 0x04000AE9 RID: 2793
		ERR_BadAwaitInCatchFilter,
		// Token: 0x04000AEA RID: 2794
		WRN_FilterIsConstantTrue,
		// Token: 0x04000AEB RID: 2795
		ERR_EncNoPIAReference,
		// Token: 0x04000AEC RID: 2796
		ERR_LinkedNetmoduleMetadataMustProvideFullPEImage = 7098,
		// Token: 0x04000AED RID: 2797
		ERR_MetadataReferencesNotSupported,
		// Token: 0x04000AEE RID: 2798
		ERR_InvalidAssemblyCulture,
		// Token: 0x04000AEF RID: 2799
		ERR_EncReferenceToAddedMember,
		// Token: 0x04000AF0 RID: 2800
		ERR_MutuallyExclusiveOptions,
		// Token: 0x04000AF1 RID: 2801
		ERR_InvalidDebugInfo,
		// Token: 0x04000AF2 RID: 2802
		WRN_UnimplementedCommandLineSwitch = 8001,
		// Token: 0x04000AF3 RID: 2803
		WRN_ReferencedAssemblyDoesNotHaveStrongName,
		// Token: 0x04000AF4 RID: 2804
		ERR_InvalidSignaturePublicKey,
		// Token: 0x04000AF5 RID: 2805
		ERR_ExportedTypeConflictsWithDeclaration,
		// Token: 0x04000AF6 RID: 2806
		ERR_ExportedTypesConflict,
		// Token: 0x04000AF7 RID: 2807
		ERR_ForwardedTypeConflictsWithDeclaration,
		// Token: 0x04000AF8 RID: 2808
		ERR_ForwardedTypesConflict,
		// Token: 0x04000AF9 RID: 2809
		ERR_ForwardedTypeConflictsWithExportedType,
		// Token: 0x04000AFA RID: 2810
		WRN_RefCultureMismatch,
		// Token: 0x04000AFB RID: 2811
		ERR_AgnosticToMachineModule,
		// Token: 0x04000AFC RID: 2812
		ERR_ConflictingMachineModule,
		// Token: 0x04000AFD RID: 2813
		WRN_ConflictingMachineAssembly,
		// Token: 0x04000AFE RID: 2814
		ERR_CryptoHashFailed,
		// Token: 0x04000AFF RID: 2815
		ERR_MissingNetModuleReference,
		// Token: 0x04000B00 RID: 2816
		ERR_NetModuleNameMustBeUnique,
		// Token: 0x04000B01 RID: 2817
		ERR_UnsupportedTransparentIdentifierAccess,
		// Token: 0x04000B02 RID: 2818
		ERR_ParamDefaultValueDiffersFromAttribute,
		// Token: 0x04000B03 RID: 2819
		WRN_UnqualifiedNestedTypeInCref,
		// Token: 0x04000B04 RID: 2820
		HDN_UnusedUsingDirective,
		// Token: 0x04000B05 RID: 2821
		HDN_UnusedExternAlias,
		// Token: 0x04000B06 RID: 2822
		WRN_NoRuntimeMetadataVersion,
		// Token: 0x04000B07 RID: 2823
		ERR_FeatureNotAvailableInVersion1,
		// Token: 0x04000B08 RID: 2824
		ERR_FeatureNotAvailableInVersion2,
		// Token: 0x04000B09 RID: 2825
		ERR_FeatureNotAvailableInVersion3,
		// Token: 0x04000B0A RID: 2826
		ERR_FeatureNotAvailableInVersion4,
		// Token: 0x04000B0B RID: 2827
		ERR_FeatureNotAvailableInVersion5,
		// Token: 0x04000B0C RID: 2828
		ERR_FieldHasMultipleDistinctConstantValues,
		// Token: 0x04000B0D RID: 2829
		ERR_ComImportWithInitializers,
		// Token: 0x04000B0E RID: 2830
		WRN_PdbLocalNameTooLong,
		// Token: 0x04000B0F RID: 2831
		ERR_RetNoObjectRequiredLambda,
		// Token: 0x04000B10 RID: 2832
		ERR_TaskRetNoObjectRequiredLambda,
		// Token: 0x04000B11 RID: 2833
		WRN_AnalyzerCannotBeCreated,
		// Token: 0x04000B12 RID: 2834
		WRN_NoAnalyzerInAssembly,
		// Token: 0x04000B13 RID: 2835
		WRN_UnableToLoadAnalyzer,
		// Token: 0x04000B14 RID: 2836
		ERR_CantReadRulesetFile,
		// Token: 0x04000B15 RID: 2837
		ERR_BadPdbData,
		// Token: 0x04000B16 RID: 2838
		INF_UnableToLoadSomeTypesInAnalyzer = 8040,
		// Token: 0x04000B17 RID: 2839
		ERR_InitializerOnNonAutoProperty = 8050,
		// Token: 0x04000B18 RID: 2840
		ERR_AutoPropertyMustHaveGetAccessor,
		// Token: 0x04000B19 RID: 2841
		ERR_InstancePropertyInitializerInInterface = 8053,
		// Token: 0x04000B1A RID: 2842
		ERR_EnumsCantContainDefaultConstructor,
		// Token: 0x04000B1B RID: 2843
		ERR_EncodinglessSyntaxTree,
		// Token: 0x04000B1C RID: 2844
		ERR_BlockBodyAndExpressionBody = 8057,
		// Token: 0x04000B1D RID: 2845
		ERR_FeatureIsExperimental,
		// Token: 0x04000B1E RID: 2846
		ERR_FeatureNotAvailableInVersion6,
		// Token: 0x04000B1F RID: 2847
		ERR_SwitchFallOut = 8070,
		// Token: 0x04000B20 RID: 2848
		ERR_NullPropagatingOpInExpressionTree = 8072,
		// Token: 0x04000B21 RID: 2849
		WRN_NubExprIsConstBool2,
		// Token: 0x04000B22 RID: 2850
		ERR_DictionaryInitializerInExpressionTree,
		// Token: 0x04000B23 RID: 2851
		ERR_ExtensionCollectionElementInitializerInExpressionTree,
		// Token: 0x04000B24 RID: 2852
		ERR_UnclosedExpressionHole,
		// Token: 0x04000B25 RID: 2853
		ERR_SingleLineCommentInExpressionHole,
		// Token: 0x04000B26 RID: 2854
		ERR_InsufficientStack,
		// Token: 0x04000B27 RID: 2855
		ERR_UseDefViolationProperty,
		// Token: 0x04000B28 RID: 2856
		ERR_AutoPropertyMustOverrideSet,
		// Token: 0x04000B29 RID: 2857
		ERR_ExpressionHasNoName,
		// Token: 0x04000B2A RID: 2858
		ERR_SubexpressionNotInNameof,
		// Token: 0x04000B2B RID: 2859
		ERR_AliasQualifiedNameNotAnExpression,
		// Token: 0x04000B2C RID: 2860
		ERR_NameofMethodGroupWithTypeParameters,
		// Token: 0x04000B2D RID: 2861
		ERR_NoAliasHere,
		// Token: 0x04000B2E RID: 2862
		ERR_UnescapedCurly,
		// Token: 0x04000B2F RID: 2863
		ERR_EscapedCurly,
		// Token: 0x04000B30 RID: 2864
		ERR_TrailingWhitespaceInFormatSpecifier,
		// Token: 0x04000B31 RID: 2865
		ERR_EmptyFormatSpecifier,
		// Token: 0x04000B32 RID: 2866
		ERR_ErrorInReferencedAssembly,
		// Token: 0x04000B33 RID: 2867
		ERR_ExternHasConstructorInitializer,
		// Token: 0x04000B34 RID: 2868
		ERR_ExpressionOrDeclarationExpected,
		// Token: 0x04000B35 RID: 2869
		ERR_NameofExtensionMethod,
		// Token: 0x04000B36 RID: 2870
		WRN_AlignmentMagnitude,
		// Token: 0x04000B37 RID: 2871
		ERR_ConstantStringTooLong,
		// Token: 0x04000B38 RID: 2872
		ERR_DebugEntryPointNotSourceMethodDefinition,
		// Token: 0x04000B39 RID: 2873
		ERR_LoadDirectiveOnlyAllowedInScripts,
		// Token: 0x04000B3A RID: 2874
		ERR_PPLoadFollowsToken,
		// Token: 0x04000B3B RID: 2875
		ERR_SourceFileReferencesNotSupported,
		// Token: 0x04000B3C RID: 2876
		ERR_BadAwaitInStaticVariableInitializer,
		// Token: 0x04000B3D RID: 2877
		ERR_InvalidPathMap,
		// Token: 0x04000B3E RID: 2878
		ERR_PublicSignButNoKey,
		// Token: 0x04000B3F RID: 2879
		ERR_TooManyUserStrings,
		// Token: 0x04000B40 RID: 2880
		ERR_PeWritingFailure,
		// Token: 0x04000B41 RID: 2881
		WRN_AttributeIgnoredWhenPublicSigning,
		// Token: 0x04000B42 RID: 2882
		ERR_OptionMustBeAbsolutePath,
		// Token: 0x04000B43 RID: 2883
		ERR_FeatureNotAvailableInVersion7,
		// Token: 0x04000B44 RID: 2884
		ERR_DynamicLocalFunctionParamsParameter,
		// Token: 0x04000B45 RID: 2885
		ERR_ExpressionTreeContainsLocalFunction = 8110,
		// Token: 0x04000B46 RID: 2886
		ERR_InvalidInstrumentationKind,
		// Token: 0x04000B47 RID: 2887
		ERR_LocalFunctionMissingBody,
		// Token: 0x04000B48 RID: 2888
		ERR_InvalidHashAlgorithmName,
		// Token: 0x04000B49 RID: 2889
		ERR_ThrowMisplaced = 8115,
		// Token: 0x04000B4A RID: 2890
		ERR_PatternNullableType,
		// Token: 0x04000B4B RID: 2891
		ERR_BadPatternExpression,
		// Token: 0x04000B4C RID: 2892
		ERR_SwitchExpressionValueExpected = 8119,
		// Token: 0x04000B4D RID: 2893
		ERR_SwitchCaseSubsumed,
		// Token: 0x04000B4E RID: 2894
		ERR_PatternWrongType,
		// Token: 0x04000B4F RID: 2895
		ERR_ExpressionTreeContainsIsMatch,
		// Token: 0x04000B50 RID: 2896
		WRN_TupleLiteralNameMismatch,
		// Token: 0x04000B51 RID: 2897
		ERR_TupleTooFewElements,
		// Token: 0x04000B52 RID: 2898
		ERR_TupleReservedElementName,
		// Token: 0x04000B53 RID: 2899
		ERR_TupleReservedElementNameAnyPosition,
		// Token: 0x04000B54 RID: 2900
		ERR_TupleDuplicateElementName,
		// Token: 0x04000B55 RID: 2901
		ERR_PredefinedTypeMemberNotFoundInAssembly,
		// Token: 0x04000B56 RID: 2902
		ERR_MissingDeconstruct,
		// Token: 0x04000B57 RID: 2903
		ERR_TypeInferenceFailedForImplicitlyTypedDeconstructionVariable,
		// Token: 0x04000B58 RID: 2904
		ERR_DeconstructRequiresExpression,
		// Token: 0x04000B59 RID: 2905
		ERR_DeconstructWrongCardinality,
		// Token: 0x04000B5A RID: 2906
		ERR_CannotDeconstructDynamic,
		// Token: 0x04000B5B RID: 2907
		ERR_DeconstructTooFewElements,
		// Token: 0x04000B5C RID: 2908
		ERR_ConversionNotTupleCompatible,
		// Token: 0x04000B5D RID: 2909
		ERR_DeconstructionVarFormDisallowsSpecificType,
		// Token: 0x04000B5E RID: 2910
		ERR_TupleElementNamesAttributeMissing,
		// Token: 0x04000B5F RID: 2911
		ERR_ExplicitTupleElementNamesAttribute,
		// Token: 0x04000B60 RID: 2912
		ERR_CantChangeTupleNamesOnOverride,
		// Token: 0x04000B61 RID: 2913
		ERR_DuplicateInterfaceWithTupleNamesInBaseList,
		// Token: 0x04000B62 RID: 2914
		ERR_ImplBadTupleNames,
		// Token: 0x04000B63 RID: 2915
		ERR_PartialMethodInconsistentTupleNames,
		// Token: 0x04000B64 RID: 2916
		ERR_ExpressionTreeContainsTupleLiteral,
		// Token: 0x04000B65 RID: 2917
		ERR_ExpressionTreeContainsTupleConversion,
		// Token: 0x04000B66 RID: 2918
		ERR_AutoPropertyCannotBeRefReturning,
		// Token: 0x04000B67 RID: 2919
		ERR_RefPropertyMustHaveGetAccessor,
		// Token: 0x04000B68 RID: 2920
		ERR_RefPropertyCannotHaveSetAccessor,
		// Token: 0x04000B69 RID: 2921
		ERR_CantChangeRefReturnOnOverride,
		// Token: 0x04000B6A RID: 2922
		ERR_MustNotHaveRefReturn,
		// Token: 0x04000B6B RID: 2923
		ERR_MustHaveRefReturn,
		// Token: 0x04000B6C RID: 2924
		ERR_RefReturnMustHaveIdentityConversion,
		// Token: 0x04000B6D RID: 2925
		ERR_CloseUnimplementedInterfaceMemberWrongRefReturn,
		// Token: 0x04000B6E RID: 2926
		ERR_RefReturningCallInExpressionTree,
		// Token: 0x04000B6F RID: 2927
		ERR_BadIteratorReturnRef,
		// Token: 0x04000B70 RID: 2928
		ERR_BadRefReturnExpressionTree,
		// Token: 0x04000B71 RID: 2929
		ERR_RefReturnLvalueExpected,
		// Token: 0x04000B72 RID: 2930
		ERR_RefReturnNonreturnableLocal,
		// Token: 0x04000B73 RID: 2931
		ERR_RefReturnNonreturnableLocal2,
		// Token: 0x04000B74 RID: 2932
		ERR_RefReturnRangeVariable,
		// Token: 0x04000B75 RID: 2933
		ERR_RefReturnReadonly,
		// Token: 0x04000B76 RID: 2934
		ERR_RefReturnReadonlyStatic,
		// Token: 0x04000B77 RID: 2935
		ERR_RefReturnReadonly2,
		// Token: 0x04000B78 RID: 2936
		ERR_RefReturnReadonlyStatic2,
		// Token: 0x04000B79 RID: 2937
		ERR_RefReturnParameter = 8166,
		// Token: 0x04000B7A RID: 2938
		ERR_RefReturnParameter2,
		// Token: 0x04000B7B RID: 2939
		ERR_RefReturnLocal,
		// Token: 0x04000B7C RID: 2940
		ERR_RefReturnLocal2,
		// Token: 0x04000B7D RID: 2941
		ERR_RefReturnStructThis,
		// Token: 0x04000B7E RID: 2942
		ERR_InitializeByValueVariableWithReference,
		// Token: 0x04000B7F RID: 2943
		ERR_InitializeByReferenceVariableWithValue,
		// Token: 0x04000B80 RID: 2944
		ERR_RefAssignmentMustHaveIdentityConversion,
		// Token: 0x04000B81 RID: 2945
		ERR_ByReferenceVariableMustBeInitialized,
		// Token: 0x04000B82 RID: 2946
		ERR_AnonDelegateCantUseLocal,
		// Token: 0x04000B83 RID: 2947
		ERR_BadIteratorLocalType,
		// Token: 0x04000B84 RID: 2948
		ERR_BadAsyncLocalType,
		// Token: 0x04000B85 RID: 2949
		ERR_RefReturningCallAndAwait,
		// Token: 0x04000B86 RID: 2950
		ERR_PredefinedValueTupleTypeNotFound,
		// Token: 0x04000B87 RID: 2951
		ERR_SemiOrLBraceOrArrowExpected,
		// Token: 0x04000B88 RID: 2952
		ERR_NewWithTupleTypeSyntax,
		// Token: 0x04000B89 RID: 2953
		ERR_PredefinedValueTupleTypeMustBeStruct,
		// Token: 0x04000B8A RID: 2954
		ERR_DiscardTypeInferenceFailed,
		// Token: 0x04000B8B RID: 2955
		ERR_DeclarationExpressionNotPermitted = 8185,
		// Token: 0x04000B8C RID: 2956
		ERR_MustDeclareForeachIteration,
		// Token: 0x04000B8D RID: 2957
		ERR_TupleElementNamesInDeconstruction,
		// Token: 0x04000B8E RID: 2958
		ERR_ExpressionTreeContainsThrowExpression,
		// Token: 0x04000B8F RID: 2959
		ERR_DelegateRefMismatch,
		// Token: 0x04000B90 RID: 2960
		ERR_BadSourceCodeKind,
		// Token: 0x04000B91 RID: 2961
		ERR_BadDocumentationMode,
		// Token: 0x04000B92 RID: 2962
		ERR_BadLanguageVersion,
		// Token: 0x04000B93 RID: 2963
		ERR_ImplicitlyTypedOutVariableUsedInTheSameArgumentList = 8196,
		// Token: 0x04000B94 RID: 2964
		ERR_TypeInferenceFailedForImplicitlyTypedOutVariable,
		// Token: 0x04000B95 RID: 2965
		ERR_ExpressionTreeContainsOutVariable,
		// Token: 0x04000B96 RID: 2966
		ERR_VarInvocationLvalueReserved,
		// Token: 0x04000B97 RID: 2967
		ERR_PublicSignNetModule = 8202,
		// Token: 0x04000B98 RID: 2968
		ERR_BadAssemblyName,
		// Token: 0x04000B99 RID: 2969
		ERR_BadAsyncMethodBuilderTaskProperty,
		// Token: 0x04000B9A RID: 2970
		ERR_AttributesInLocalFuncDecl,
		// Token: 0x04000B9B RID: 2971
		ERR_TypeForwardedToMultipleAssemblies,
		// Token: 0x04000B9C RID: 2972
		ERR_ExpressionTreeContainsDiscard,
		// Token: 0x04000B9D RID: 2973
		ERR_PatternDynamicType,
		// Token: 0x04000B9E RID: 2974
		ERR_VoidAssignment,
		// Token: 0x04000B9F RID: 2975
		ERR_VoidInTuple,
		// Token: 0x04000BA0 RID: 2976
		ERR_Merge_conflict_marker_encountered = 8300,
		// Token: 0x04000BA1 RID: 2977
		ERR_InvalidPreprocessingSymbol,
		// Token: 0x04000BA2 RID: 2978
		ERR_FeatureNotAvailableInVersion7_1,
		// Token: 0x04000BA3 RID: 2979
		ERR_LanguageVersionCannotHaveLeadingZeroes,
		// Token: 0x04000BA4 RID: 2980
		ERR_CompilerAndLanguageVersion,
		// Token: 0x04000BA5 RID: 2981
		WRN_Experimental,
		// Token: 0x04000BA6 RID: 2982
		ERR_TupleInferredNamesNotAvailable,
		// Token: 0x04000BA7 RID: 2983
		ERR_TypelessTupleInAs,
		// Token: 0x04000BA8 RID: 2984
		ERR_NoRefOutWhenRefOnly,
		// Token: 0x04000BA9 RID: 2985
		ERR_NoNetModuleOutputWhenRefOutOrRefOnly,
		// Token: 0x04000BAA RID: 2986
		ERR_BadOpOnNullOrDefaultOrNew,
		// Token: 0x04000BAB RID: 2987
		ERR_DefaultLiteralNotValid = 8312,
		// Token: 0x04000BAC RID: 2988
		ERR_PatternWrongGenericTypeInVersion = 8314,
		// Token: 0x04000BAD RID: 2989
		ERR_AmbigBinaryOpsOnDefault,
		// Token: 0x04000BAE RID: 2990
		ERR_FeatureNotAvailableInVersion7_2 = 8320,
		// Token: 0x04000BAF RID: 2991
		WRN_UnreferencedLocalFunction,
		// Token: 0x04000BB0 RID: 2992
		ERR_DynamicLocalFunctionTypeParameter,
		// Token: 0x04000BB1 RID: 2993
		ERR_BadNonTrailingNamedArgument,
		// Token: 0x04000BB2 RID: 2994
		ERR_NamedArgumentSpecificationBeforeFixedArgumentInDynamicInvocation,
		// Token: 0x04000BB3 RID: 2995
		ERR_RefConditionalAndAwait,
		// Token: 0x04000BB4 RID: 2996
		ERR_RefConditionalNeedsTwoRefs,
		// Token: 0x04000BB5 RID: 2997
		ERR_RefConditionalDifferentTypes,
		// Token: 0x04000BB6 RID: 2998
		ERR_BadParameterModifiers,
		// Token: 0x04000BB7 RID: 2999
		ERR_RefReadonlyNotField,
		// Token: 0x04000BB8 RID: 3000
		ERR_RefReadonlyNotField2,
		// Token: 0x04000BB9 RID: 3001
		ERR_AssignReadonlyNotField,
		// Token: 0x04000BBA RID: 3002
		ERR_AssignReadonlyNotField2,
		// Token: 0x04000BBB RID: 3003
		ERR_RefReturnReadonlyNotField,
		// Token: 0x04000BBC RID: 3004
		ERR_RefReturnReadonlyNotField2,
		// Token: 0x04000BBD RID: 3005
		ERR_ExplicitReservedAttr,
		// Token: 0x04000BBE RID: 3006
		ERR_TypeReserved,
		// Token: 0x04000BBF RID: 3007
		ERR_RefExtensionMustBeValueTypeOrConstrainedToOne,
		// Token: 0x04000BC0 RID: 3008
		ERR_InExtensionMustBeValueType,
		// Token: 0x04000BC1 RID: 3009
		ERR_FieldsInRoStruct = 8340,
		// Token: 0x04000BC2 RID: 3010
		ERR_AutoPropsInRoStruct,
		// Token: 0x04000BC3 RID: 3011
		ERR_FieldlikeEventsInRoStruct,
		// Token: 0x04000BC4 RID: 3012
		ERR_RefStructInterfaceImpl,
		// Token: 0x04000BC5 RID: 3013
		ERR_BadSpecialByRefIterator,
		// Token: 0x04000BC6 RID: 3014
		ERR_FieldAutoPropCantBeByRefLike,
		// Token: 0x04000BC7 RID: 3015
		ERR_StackAllocConversionNotPossible,
		// Token: 0x04000BC8 RID: 3016
		ERR_EscapeCall,
		// Token: 0x04000BC9 RID: 3017
		ERR_EscapeCall2,
		// Token: 0x04000BCA RID: 3018
		ERR_EscapeOther,
		// Token: 0x04000BCB RID: 3019
		ERR_CallArgMixing,
		// Token: 0x04000BCC RID: 3020
		ERR_MismatchedRefEscapeInTernary,
		// Token: 0x04000BCD RID: 3021
		ERR_EscapeLocal,
		// Token: 0x04000BCE RID: 3022
		ERR_EscapeStackAlloc,
		// Token: 0x04000BCF RID: 3023
		ERR_RefReturnThis,
		// Token: 0x04000BD0 RID: 3024
		ERR_OutAttrOnInParam,
		// Token: 0x04000BD1 RID: 3025
		ERR_PredefinedValueTupleTypeAmbiguous3,
		// Token: 0x04000BD2 RID: 3026
		ERR_InvalidVersionFormatDeterministic,
		// Token: 0x04000BD3 RID: 3027
		ERR_AttributeCtorInParameter,
		// Token: 0x04000BD4 RID: 3028
		WRN_FilterIsConstantFalse,
		// Token: 0x04000BD5 RID: 3029
		WRN_FilterIsConstantFalseRedundantTryCatch,
		// Token: 0x04000BD6 RID: 3030
		ERR_ConditionalInInterpolation,
		// Token: 0x04000BD7 RID: 3031
		ERR_CantUseVoidInArglist,
		// Token: 0x04000BD8 RID: 3032
		ERR_InDynamicMethodArg = 8364,
		// Token: 0x04000BD9 RID: 3033
		ERR_FeatureNotAvailableInVersion7_3 = 8370,
		// Token: 0x04000BDA RID: 3034
		WRN_AttributesOnBackingFieldsNotAvailable,
		// Token: 0x04000BDB RID: 3035
		ERR_DoNotUseFixedBufferAttrOnProperty,
		// Token: 0x04000BDC RID: 3036
		ERR_RefLocalOrParamExpected,
		// Token: 0x04000BDD RID: 3037
		ERR_RefAssignNarrower,
		// Token: 0x04000BDE RID: 3038
		ERR_NewBoundWithUnmanaged,
		// Token: 0x04000BDF RID: 3039
		ERR_UnmanagedConstraintNotSatisfied = 8377,
		// Token: 0x04000BE0 RID: 3040
		ERR_CantUseInOrOutInArglist,
		// Token: 0x04000BE1 RID: 3041
		ERR_ConWithUnmanagedCon,
		// Token: 0x04000BE2 RID: 3042
		ERR_UnmanagedBoundWithClass,
		// Token: 0x04000BE3 RID: 3043
		ERR_InvalidStackAllocArray,
		// Token: 0x04000BE4 RID: 3044
		ERR_ExpressionTreeContainsTupleBinOp,
		// Token: 0x04000BE5 RID: 3045
		WRN_TupleBinopLiteralNameMismatch,
		// Token: 0x04000BE6 RID: 3046
		ERR_TupleSizesMismatchForBinOps,
		// Token: 0x04000BE7 RID: 3047
		ERR_ExprCannotBeFixed,
		// Token: 0x04000BE8 RID: 3048
		ERR_InvalidObjectCreation,
		// Token: 0x04000BE9 RID: 3049
		WRN_TypeParameterSameAsOuterMethodTypeParameter,
		// Token: 0x04000BEA RID: 3050
		ERR_OutVariableCannotBeByRef,
		// Token: 0x04000BEB RID: 3051
		ERR_OmittedTypeArgument,
		// Token: 0x04000BEC RID: 3052
		ERR_FeatureNotAvailableInVersion8 = 8400,
		// Token: 0x04000BED RID: 3053
		ERR_AltInterpolatedVerbatimStringsNotAvailable,
		// Token: 0x04000BEE RID: 3054
		ERR_IteratorMustBeAsync = 8403,
		// Token: 0x04000BEF RID: 3055
		ERR_NoConvToIAsyncDisp = 8410,
		// Token: 0x04000BF0 RID: 3056
		ERR_AwaitForEachMissingMember,
		// Token: 0x04000BF1 RID: 3057
		ERR_BadGetAsyncEnumerator,
		// Token: 0x04000BF2 RID: 3058
		ERR_MultipleIAsyncEnumOfT,
		// Token: 0x04000BF3 RID: 3059
		ERR_ForEachMissingMemberWrongAsync,
		// Token: 0x04000BF4 RID: 3060
		ERR_AwaitForEachMissingMemberWrongAsync,
		// Token: 0x04000BF5 RID: 3061
		ERR_BadDynamicAwaitForEach,
		// Token: 0x04000BF6 RID: 3062
		ERR_NoConvToIAsyncDispWrongAsync,
		// Token: 0x04000BF7 RID: 3063
		ERR_NoConvToIDispWrongAsync,
		// Token: 0x04000BF8 RID: 3064
		ERR_PossibleAsyncIteratorWithoutYield,
		// Token: 0x04000BF9 RID: 3065
		ERR_PossibleAsyncIteratorWithoutYieldOrAwait,
		// Token: 0x04000BFA RID: 3066
		ERR_StaticLocalFunctionCannotCaptureVariable,
		// Token: 0x04000BFB RID: 3067
		ERR_StaticLocalFunctionCannotCaptureThis,
		// Token: 0x04000BFC RID: 3068
		ERR_AttributeNotOnEventAccessor,
		// Token: 0x04000BFD RID: 3069
		WRN_UnconsumedEnumeratorCancellationAttributeUsage,
		// Token: 0x04000BFE RID: 3070
		WRN_UndecoratedCancellationTokenParameter,
		// Token: 0x04000BFF RID: 3071
		ERR_MultipleEnumeratorCancellationAttributes,
		// Token: 0x04000C00 RID: 3072
		ERR_VarianceInterfaceNesting,
		// Token: 0x04000C01 RID: 3073
		ERR_ImplicitIndexIndexerWithName,
		// Token: 0x04000C02 RID: 3074
		ERR_ImplicitRangeIndexerWithName,
		// Token: 0x04000C03 RID: 3075
		ERR_WrongNumberOfSubpatterns = 8502,
		// Token: 0x04000C04 RID: 3076
		ERR_PropertyPatternNameMissing,
		// Token: 0x04000C05 RID: 3077
		ERR_MissingPattern,
		// Token: 0x04000C06 RID: 3078
		ERR_DefaultPattern,
		// Token: 0x04000C07 RID: 3079
		ERR_SwitchExpressionNoBestType,
		// Token: 0x04000C08 RID: 3080
		ERR_VarMayNotBindToType = 8508,
		// Token: 0x04000C09 RID: 3081
		WRN_SwitchExpressionNotExhaustive,
		// Token: 0x04000C0A RID: 3082
		ERR_SwitchArmSubsumed,
		// Token: 0x04000C0B RID: 3083
		ERR_ConstantPatternVsOpenType,
		// Token: 0x04000C0C RID: 3084
		WRN_CaseConstantNamedUnderscore,
		// Token: 0x04000C0D RID: 3085
		WRN_IsTypeNamedUnderscore,
		// Token: 0x04000C0E RID: 3086
		ERR_ExpressionTreeContainsSwitchExpression,
		// Token: 0x04000C0F RID: 3087
		ERR_SwitchGoverningExpressionRequiresParens,
		// Token: 0x04000C10 RID: 3088
		ERR_TupleElementNameMismatch,
		// Token: 0x04000C11 RID: 3089
		ERR_DeconstructParameterNameMismatch,
		// Token: 0x04000C12 RID: 3090
		ERR_IsPatternImpossible,
		// Token: 0x04000C13 RID: 3091
		WRN_GivenExpressionNeverMatchesPattern,
		// Token: 0x04000C14 RID: 3092
		WRN_GivenExpressionAlwaysMatchesConstant,
		// Token: 0x04000C15 RID: 3093
		ERR_PointerTypeInPatternMatching,
		// Token: 0x04000C16 RID: 3094
		ERR_ArgumentNameInITuplePattern,
		// Token: 0x04000C17 RID: 3095
		ERR_DiscardPatternInSwitchStatement,
		// Token: 0x04000C18 RID: 3096
		WRN_SwitchExpressionNotExhaustiveWithUnnamedEnumValue,
		// Token: 0x04000C19 RID: 3097
		WRN_ThrowPossibleNull = 8597,
		// Token: 0x04000C1A RID: 3098
		ERR_IllegalSuppression,
		// Token: 0x04000C1B RID: 3099
		WRN_ConvertingNullableToNonNullable = 8600,
		// Token: 0x04000C1C RID: 3100
		WRN_NullReferenceAssignment,
		// Token: 0x04000C1D RID: 3101
		WRN_NullReferenceReceiver,
		// Token: 0x04000C1E RID: 3102
		WRN_NullReferenceReturn,
		// Token: 0x04000C1F RID: 3103
		WRN_NullReferenceArgument,
		// Token: 0x04000C20 RID: 3104
		WRN_UnboxPossibleNull,
		// Token: 0x04000C21 RID: 3105
		WRN_DisallowNullAttributeForbidsMaybeNullAssignment = 8607,
		// Token: 0x04000C22 RID: 3106
		WRN_NullabilityMismatchInTypeOnOverride,
		// Token: 0x04000C23 RID: 3107
		WRN_NullabilityMismatchInReturnTypeOnOverride,
		// Token: 0x04000C24 RID: 3108
		WRN_NullabilityMismatchInParameterTypeOnOverride,
		// Token: 0x04000C25 RID: 3109
		WRN_NullabilityMismatchInParameterTypeOnPartial,
		// Token: 0x04000C26 RID: 3110
		WRN_NullabilityMismatchInTypeOnImplicitImplementation,
		// Token: 0x04000C27 RID: 3111
		WRN_NullabilityMismatchInReturnTypeOnImplicitImplementation,
		// Token: 0x04000C28 RID: 3112
		WRN_NullabilityMismatchInParameterTypeOnImplicitImplementation,
		// Token: 0x04000C29 RID: 3113
		WRN_NullabilityMismatchInTypeOnExplicitImplementation,
		// Token: 0x04000C2A RID: 3114
		WRN_NullabilityMismatchInReturnTypeOnExplicitImplementation,
		// Token: 0x04000C2B RID: 3115
		WRN_NullabilityMismatchInParameterTypeOnExplicitImplementation,
		// Token: 0x04000C2C RID: 3116
		WRN_UninitializedNonNullableField,
		// Token: 0x04000C2D RID: 3117
		WRN_NullabilityMismatchInAssignment,
		// Token: 0x04000C2E RID: 3118
		WRN_NullabilityMismatchInArgument,
		// Token: 0x04000C2F RID: 3119
		WRN_NullabilityMismatchInReturnTypeOfTargetDelegate,
		// Token: 0x04000C30 RID: 3120
		WRN_NullabilityMismatchInParameterTypeOfTargetDelegate,
		// Token: 0x04000C31 RID: 3121
		ERR_ExplicitNullableAttribute,
		// Token: 0x04000C32 RID: 3122
		WRN_NullabilityMismatchInArgumentForOutput,
		// Token: 0x04000C33 RID: 3123
		WRN_NullAsNonNullable,
		// Token: 0x04000C34 RID: 3124
		ERR_NullableUnconstrainedTypeParameter = 8627,
		// Token: 0x04000C35 RID: 3125
		ERR_AnnotationDisallowedInObjectCreation,
		// Token: 0x04000C36 RID: 3126
		WRN_NullableValueTypeMayBeNull,
		// Token: 0x04000C37 RID: 3127
		ERR_NullableOptionNotAvailable,
		// Token: 0x04000C38 RID: 3128
		WRN_NullabilityMismatchInTypeParameterConstraint,
		// Token: 0x04000C39 RID: 3129
		WRN_MissingNonNullTypesContextForAnnotation,
		// Token: 0x04000C3A RID: 3130
		WRN_NullabilityMismatchInConstraintsOnImplicitImplementation,
		// Token: 0x04000C3B RID: 3131
		WRN_NullabilityMismatchInTypeParameterReferenceTypeConstraint,
		// Token: 0x04000C3C RID: 3132
		ERR_TripleDotNotAllowed,
		// Token: 0x04000C3D RID: 3133
		ERR_BadNullableContextOption,
		// Token: 0x04000C3E RID: 3134
		ERR_NullableDirectiveQualifierExpected,
		// Token: 0x04000C3F RID: 3135
		ERR_BadNullableTypeof = 8639,
		// Token: 0x04000C40 RID: 3136
		ERR_ExpressionTreeCantContainRefStruct,
		// Token: 0x04000C41 RID: 3137
		ERR_ElseCannotStartStatement,
		// Token: 0x04000C42 RID: 3138
		ERR_ExpressionTreeCantContainNullCoalescingAssignment,
		// Token: 0x04000C43 RID: 3139
		WRN_NullabilityMismatchInExplicitlyImplementedInterface,
		// Token: 0x04000C44 RID: 3140
		WRN_NullabilityMismatchInInterfaceImplementedByBase,
		// Token: 0x04000C45 RID: 3141
		WRN_DuplicateInterfaceWithNullabilityMismatchInBaseList,
		// Token: 0x04000C46 RID: 3142
		ERR_DuplicateExplicitImpl,
		// Token: 0x04000C47 RID: 3143
		ERR_UsingVarInSwitchCase,
		// Token: 0x04000C48 RID: 3144
		ERR_GoToForwardJumpOverUsingVar,
		// Token: 0x04000C49 RID: 3145
		ERR_GoToBackwardJumpOverUsingVar,
		// Token: 0x04000C4A RID: 3146
		ERR_IsNullableType,
		// Token: 0x04000C4B RID: 3147
		ERR_AsNullableType,
		// Token: 0x04000C4C RID: 3148
		ERR_FeatureInPreview,
		// Token: 0x04000C4D RID: 3149
		WRN_SwitchExpressionNotExhaustiveForNull = 8655,
		// Token: 0x04000C4E RID: 3150
		WRN_ImplicitCopyInReadOnlyMember,
		// Token: 0x04000C4F RID: 3151
		ERR_StaticMemberCantBeReadOnly,
		// Token: 0x04000C50 RID: 3152
		ERR_AutoSetterCantBeReadOnly,
		// Token: 0x04000C51 RID: 3153
		ERR_AutoPropertyWithSetterCantBeReadOnly,
		// Token: 0x04000C52 RID: 3154
		ERR_InvalidPropertyReadOnlyMods,
		// Token: 0x04000C53 RID: 3155
		ERR_DuplicatePropertyReadOnlyMods,
		// Token: 0x04000C54 RID: 3156
		ERR_FieldLikeEventCantBeReadOnly,
		// Token: 0x04000C55 RID: 3157
		ERR_PartialMethodReadOnlyDifference,
		// Token: 0x04000C56 RID: 3158
		ERR_ReadOnlyModMissingAccessor,
		// Token: 0x04000C57 RID: 3159
		ERR_OverrideRefConstraintNotSatisfied,
		// Token: 0x04000C58 RID: 3160
		ERR_OverrideValConstraintNotSatisfied,
		// Token: 0x04000C59 RID: 3161
		WRN_NullabilityMismatchInConstraintsOnPartialImplementation,
		// Token: 0x04000C5A RID: 3162
		ERR_NullableDirectiveTargetExpected,
		// Token: 0x04000C5B RID: 3163
		WRN_MissingNonNullTypesContextForAnnotationInGeneratedCode,
		// Token: 0x04000C5C RID: 3164
		WRN_NullReferenceInitializer,
		// Token: 0x04000C5D RID: 3165
		ERR_MultipleAnalyzerConfigsInSameDir = 8700,
		// Token: 0x04000C5E RID: 3166
		ERR_RuntimeDoesNotSupportDefaultInterfaceImplementation,
		// Token: 0x04000C5F RID: 3167
		ERR_RuntimeDoesNotSupportDefaultInterfaceImplementationForMember,
		// Token: 0x04000C60 RID: 3168
		ERR_InvalidModifierForLanguageVersion,
		// Token: 0x04000C61 RID: 3169
		ERR_ImplicitImplementationOfNonPublicInterfaceMember,
		// Token: 0x04000C62 RID: 3170
		ERR_MostSpecificImplementationIsNotFound,
		// Token: 0x04000C63 RID: 3171
		ERR_LanguageVersionDoesNotSupportDefaultInterfaceImplementationForMember,
		// Token: 0x04000C64 RID: 3172
		ERR_RuntimeDoesNotSupportProtectedAccessForInterfaceMember,
		// Token: 0x04000C65 RID: 3173
		ERR_DefaultInterfaceImplementationInNoPIAType = 8711,
		// Token: 0x04000C66 RID: 3174
		ERR_AbstractEventHasAccessors,
		// Token: 0x04000C67 RID: 3175
		WRN_NullabilityMismatchInTypeParameterNotNullConstraint = 8714,
		// Token: 0x04000C68 RID: 3176
		ERR_DuplicateNullSuppression,
		// Token: 0x04000C69 RID: 3177
		ERR_DefaultLiteralNoTargetType,
		// Token: 0x04000C6A RID: 3178
		ERR_ReAbstractionInNoPIAType = 8750,
		// Token: 0x04000C6B RID: 3179
		ERR_InternalError,
		// Token: 0x04000C6C RID: 3180
		ERR_ImplicitObjectCreationIllegalTargetType,
		// Token: 0x04000C6D RID: 3181
		ERR_ImplicitObjectCreationNotValid,
		// Token: 0x04000C6E RID: 3182
		ERR_ImplicitObjectCreationNoTargetType,
		// Token: 0x04000C6F RID: 3183
		ERR_BadFuncPointerParamModifier,
		// Token: 0x04000C70 RID: 3184
		ERR_BadFuncPointerArgCount,
		// Token: 0x04000C71 RID: 3185
		ERR_MethFuncPtrMismatch,
		// Token: 0x04000C72 RID: 3186
		ERR_FuncPtrRefMismatch,
		// Token: 0x04000C73 RID: 3187
		ERR_FuncPtrMethMustBeStatic,
		// Token: 0x04000C74 RID: 3188
		ERR_ExternEventInitializer,
		// Token: 0x04000C75 RID: 3189
		ERR_AmbigBinaryOpsOnUnconstrainedDefault,
		// Token: 0x04000C76 RID: 3190
		WRN_ParameterConditionallyDisallowsNull,
		// Token: 0x04000C77 RID: 3191
		WRN_ShouldNotReturn,
		// Token: 0x04000C78 RID: 3192
		WRN_TopLevelNullabilityMismatchInReturnTypeOnOverride,
		// Token: 0x04000C79 RID: 3193
		WRN_TopLevelNullabilityMismatchInParameterTypeOnOverride,
		// Token: 0x04000C7A RID: 3194
		WRN_TopLevelNullabilityMismatchInReturnTypeOnImplicitImplementation,
		// Token: 0x04000C7B RID: 3195
		WRN_TopLevelNullabilityMismatchInParameterTypeOnImplicitImplementation,
		// Token: 0x04000C7C RID: 3196
		WRN_TopLevelNullabilityMismatchInReturnTypeOnExplicitImplementation,
		// Token: 0x04000C7D RID: 3197
		WRN_TopLevelNullabilityMismatchInParameterTypeOnExplicitImplementation,
		// Token: 0x04000C7E RID: 3198
		WRN_DoesNotReturnMismatch,
		// Token: 0x04000C7F RID: 3199
		ERR_NoOutputDirectory,
		// Token: 0x04000C80 RID: 3200
		ERR_StdInOptionProvidedButConsoleInputIsNotRedirected,
		// Token: 0x04000C81 RID: 3201
		ERR_FeatureNotAvailableInVersion9,
		// Token: 0x04000C82 RID: 3202
		WRN_MemberNotNull,
		// Token: 0x04000C83 RID: 3203
		WRN_MemberNotNullWhen,
		// Token: 0x04000C84 RID: 3204
		WRN_MemberNotNullBadMember,
		// Token: 0x04000C85 RID: 3205
		WRN_ParameterDisallowsNull,
		// Token: 0x04000C86 RID: 3206
		WRN_ConstOutOfRangeChecked,
		// Token: 0x04000C87 RID: 3207
		ERR_DuplicateInterfaceWithDifferencesInBaseList,
		// Token: 0x04000C88 RID: 3208
		ERR_DesignatorBeneathPatternCombinator,
		// Token: 0x04000C89 RID: 3209
		ERR_UnsupportedTypeForRelationalPattern,
		// Token: 0x04000C8A RID: 3210
		ERR_RelationalPatternWithNaN,
		// Token: 0x04000C8B RID: 3211
		ERR_ConditionalOnLocalFunction,
		// Token: 0x04000C8C RID: 3212
		WRN_GeneratorFailedDuringInitialization,
		// Token: 0x04000C8D RID: 3213
		WRN_GeneratorFailedDuringGeneration,
		// Token: 0x04000C8E RID: 3214
		ERR_WrongFuncPtrCallingConvention,
		// Token: 0x04000C8F RID: 3215
		ERR_MissingAddressOf,
		// Token: 0x04000C90 RID: 3216
		ERR_CannotUseReducedExtensionMethodInAddressOf,
		// Token: 0x04000C91 RID: 3217
		ERR_CannotUseFunctionPointerAsFixedLocal,
		// Token: 0x04000C92 RID: 3218
		ERR_ExpressionTreeContainsPatternIndexOrRangeIndexer,
		// Token: 0x04000C93 RID: 3219
		ERR_ExpressionTreeContainsFromEndIndexExpression,
		// Token: 0x04000C94 RID: 3220
		ERR_ExpressionTreeContainsRangeExpression,
		// Token: 0x04000C95 RID: 3221
		WRN_GivenExpressionAlwaysMatchesPattern,
		// Token: 0x04000C96 RID: 3222
		WRN_IsPatternAlways,
		// Token: 0x04000C97 RID: 3223
		ERR_PartialMethodWithAccessibilityModsMustHaveImplementation,
		// Token: 0x04000C98 RID: 3224
		ERR_PartialMethodWithNonVoidReturnMustHaveAccessMods,
		// Token: 0x04000C99 RID: 3225
		ERR_PartialMethodWithOutParamMustHaveAccessMods,
		// Token: 0x04000C9A RID: 3226
		ERR_PartialMethodWithExtendedModMustHaveAccessMods,
		// Token: 0x04000C9B RID: 3227
		ERR_PartialMethodAccessibilityDifference,
		// Token: 0x04000C9C RID: 3228
		ERR_PartialMethodExtendedModDifference,
		// Token: 0x04000C9D RID: 3229
		ERR_SimpleProgramLocalIsReferencedOutsideOfTopLevelStatement,
		// Token: 0x04000C9E RID: 3230
		ERR_SimpleProgramMultipleUnitsWithTopLevelStatements,
		// Token: 0x04000C9F RID: 3231
		ERR_TopLevelStatementAfterNamespaceOrType,
		// Token: 0x04000CA0 RID: 3232
		ERR_SimpleProgramDisallowsMainType,
		// Token: 0x04000CA1 RID: 3233
		ERR_SimpleProgramNotAnExecutable,
		// Token: 0x04000CA2 RID: 3234
		ERR_UnsupportedCallingConvention,
		// Token: 0x04000CA3 RID: 3235
		ERR_InvalidFunctionPointerCallingConvention,
		// Token: 0x04000CA4 RID: 3236
		ERR_InvalidFuncPointerReturnTypeModifier,
		// Token: 0x04000CA5 RID: 3237
		ERR_DupReturnTypeMod,
		// Token: 0x04000CA6 RID: 3238
		ERR_AddressOfMethodGroupInExpressionTree,
		// Token: 0x04000CA7 RID: 3239
		ERR_CannotConvertAddressOfToDelegate,
		// Token: 0x04000CA8 RID: 3240
		ERR_AddressOfToNonFunctionPointer,
		// Token: 0x04000CA9 RID: 3241
		ERR_ModuleInitializerMethodMustBeOrdinary,
		// Token: 0x04000CAA RID: 3242
		ERR_ModuleInitializerMethodMustBeAccessibleOutsideTopLevelType,
		// Token: 0x04000CAB RID: 3243
		ERR_ModuleInitializerMethodMustBeStaticParameterlessVoid,
		// Token: 0x04000CAC RID: 3244
		ERR_ModuleInitializerMethodAndContainingTypesMustNotBeGeneric,
		// Token: 0x04000CAD RID: 3245
		ERR_PartialMethodReturnTypeDifference,
		// Token: 0x04000CAE RID: 3246
		ERR_PartialMethodRefReturnDifference,
		// Token: 0x04000CAF RID: 3247
		WRN_NullabilityMismatchInReturnTypeOnPartial,
		// Token: 0x04000CB0 RID: 3248
		ERR_StaticAnonymousFunctionCannotCaptureVariable,
		// Token: 0x04000CB1 RID: 3249
		ERR_StaticAnonymousFunctionCannotCaptureThis,
		// Token: 0x04000CB2 RID: 3250
		ERR_OverrideDefaultConstraintNotSatisfied,
		// Token: 0x04000CB3 RID: 3251
		ERR_DefaultConstraintOverrideOnly,
		// Token: 0x04000CB4 RID: 3252
		WRN_ParameterNotNullIfNotNull,
		// Token: 0x04000CB5 RID: 3253
		WRN_ReturnNotNullIfNotNull,
		// Token: 0x04000CB6 RID: 3254
		ERR_RuntimeDoesNotSupportCovariantReturnsOfClasses = 8830,
		// Token: 0x04000CB7 RID: 3255
		ERR_RuntimeDoesNotSupportCovariantPropertiesOfClasses,
		// Token: 0x04000CB8 RID: 3256
		WRN_SwitchExpressionNotExhaustiveWithWhen = 8846,
		// Token: 0x04000CB9 RID: 3257
		WRN_SwitchExpressionNotExhaustiveForNullWithWhen,
		// Token: 0x04000CBA RID: 3258
		WRN_PrecedenceInversion,
		// Token: 0x04000CBB RID: 3259
		ERR_ExpressionTreeContainsWithExpression,
		// Token: 0x04000CBC RID: 3260
		WRN_AnalyzerReferencesFramework,
		// Token: 0x04000CBD RID: 3261
		WRN_RecordEqualsWithoutGetHashCode,
		// Token: 0x04000CBE RID: 3262
		ERR_AssignmentInitOnly,
		// Token: 0x04000CBF RID: 3263
		ERR_CantChangeInitOnlyOnOverride,
		// Token: 0x04000CC0 RID: 3264
		ERR_CloseUnimplementedInterfaceMemberWrongInitOnly,
		// Token: 0x04000CC1 RID: 3265
		ERR_ExplicitPropertyMismatchInitOnly,
		// Token: 0x04000CC2 RID: 3266
		ERR_BadInitAccessor,
		// Token: 0x04000CC3 RID: 3267
		ERR_InvalidWithReceiverType,
		// Token: 0x04000CC4 RID: 3268
		ERR_CannotClone,
		// Token: 0x04000CC5 RID: 3269
		ERR_CloneDisallowedInRecord,
		// Token: 0x04000CC6 RID: 3270
		WRN_RecordNamedDisallowed,
		// Token: 0x04000CC7 RID: 3271
		ERR_UnexpectedArgumentList,
		// Token: 0x04000CC8 RID: 3272
		ERR_UnexpectedOrMissingConstructorInitializerInRecord,
		// Token: 0x04000CC9 RID: 3273
		ERR_MultipleRecordParameterLists,
		// Token: 0x04000CCA RID: 3274
		ERR_BadRecordBase,
		// Token: 0x04000CCB RID: 3275
		ERR_BadInheritanceFromRecord,
		// Token: 0x04000CCC RID: 3276
		ERR_BadRecordMemberForPositionalParameter,
		// Token: 0x04000CCD RID: 3277
		ERR_NoCopyConstructorInBaseType,
		// Token: 0x04000CCE RID: 3278
		ERR_CopyConstructorMustInvokeBaseCopyConstructor,
		// Token: 0x04000CCF RID: 3279
		ERR_DoesNotOverrideMethodFromObject,
		// Token: 0x04000CD0 RID: 3280
		ERR_SealedAPIInRecord,
		// Token: 0x04000CD1 RID: 3281
		ERR_DoesNotOverrideBaseMethod,
		// Token: 0x04000CD2 RID: 3282
		ERR_NotOverridableAPIInRecord,
		// Token: 0x04000CD3 RID: 3283
		ERR_NonPublicAPIInRecord,
		// Token: 0x04000CD4 RID: 3284
		ERR_SignatureMismatchInRecord,
		// Token: 0x04000CD5 RID: 3285
		ERR_NonProtectedAPIInRecord,
		// Token: 0x04000CD6 RID: 3286
		ERR_DoesNotOverrideBaseEqualityContract,
		// Token: 0x04000CD7 RID: 3287
		ERR_StaticAPIInRecord,
		// Token: 0x04000CD8 RID: 3288
		ERR_CopyConstructorWrongAccessibility,
		// Token: 0x04000CD9 RID: 3289
		ERR_NonPrivateAPIInRecord,
		// Token: 0x04000CDA RID: 3290
		WRN_UnassignedThisAutoProperty,
		// Token: 0x04000CDB RID: 3291
		WRN_UnassignedThis,
		// Token: 0x04000CDC RID: 3292
		WRN_ParamUnassigned,
		// Token: 0x04000CDD RID: 3293
		WRN_UseDefViolationProperty,
		// Token: 0x04000CDE RID: 3294
		WRN_UseDefViolationField,
		// Token: 0x04000CDF RID: 3295
		WRN_UseDefViolationThis,
		// Token: 0x04000CE0 RID: 3296
		WRN_UseDefViolationOut,
		// Token: 0x04000CE1 RID: 3297
		WRN_UseDefViolation,
		// Token: 0x04000CE2 RID: 3298
		ERR_CannotSpecifyManagedWithUnmanagedSpecifiers,
		// Token: 0x04000CE3 RID: 3299
		ERR_RuntimeDoesNotSupportUnmanagedDefaultCallConv,
		// Token: 0x04000CE4 RID: 3300
		ERR_TypeNotFound,
		// Token: 0x04000CE5 RID: 3301
		ERR_TypeMustBePublic,
		// Token: 0x04000CE6 RID: 3302
		WRN_SyncAndAsyncEntryPoints,
		// Token: 0x04000CE7 RID: 3303
		ERR_InvalidUnmanagedCallersOnlyCallConv,
		// Token: 0x04000CE8 RID: 3304
		ERR_CannotUseManagedTypeInUnmanagedCallersOnly,
		// Token: 0x04000CE9 RID: 3305
		ERR_UnmanagedCallersOnlyMethodOrTypeCannotBeGeneric,
		// Token: 0x04000CEA RID: 3306
		ERR_UnmanagedCallersOnlyRequiresStatic,
		// Token: 0x04000CEB RID: 3307
		WRN_ParameterIsStaticClass,
		// Token: 0x04000CEC RID: 3308
		WRN_ReturnTypeIsStaticClass,
		// Token: 0x04000CED RID: 3309
		ERR_EntryPointCannotBeUnmanagedCallersOnly,
		// Token: 0x04000CEE RID: 3310
		ERR_ModuleInitializerCannotBeUnmanagedCallersOnly,
		// Token: 0x04000CEF RID: 3311
		ERR_UnmanagedCallersOnlyMethodsCannotBeCalledDirectly,
		// Token: 0x04000CF0 RID: 3312
		ERR_UnmanagedCallersOnlyMethodsCannotBeConvertedToDelegate,
		// Token: 0x04000CF1 RID: 3313
		ERR_InitCannotBeReadonly,
		// Token: 0x04000CF2 RID: 3314
		ERR_UnexpectedVarianceStaticMember,
		// Token: 0x04000CF3 RID: 3315
		ERR_FunctionPointersCannotBeCalledWithNamedArguments,
		// Token: 0x04000CF4 RID: 3316
		ERR_EqualityContractRequiresGetter,
		// Token: 0x04000CF5 RID: 3317
		WRN_UnreadRecordParameter,
		// Token: 0x04000CF6 RID: 3318
		ERR_BadFieldTypeInRecord,
		// Token: 0x04000CF7 RID: 3319
		WRN_DoNotCompareFunctionPointers,
		// Token: 0x04000CF8 RID: 3320
		ERR_RecordAmbigCtor,
		// Token: 0x04000CF9 RID: 3321
		ERR_FunctionPointerTypesInAttributeNotSupported,
		// Token: 0x04000CFA RID: 3322
		ERR_InheritingFromRecordWithSealedToString,
		// Token: 0x04000CFB RID: 3323
		ERR_HiddenPositionalMember,
		// Token: 0x04000CFC RID: 3324
		ERR_GlobalUsingInNamespace,
		// Token: 0x04000CFD RID: 3325
		ERR_GlobalUsingOutOfOrder,
		// Token: 0x04000CFE RID: 3326
		ERR_AttributesRequireParenthesizedLambdaExpression,
		// Token: 0x04000CFF RID: 3327
		ERR_CannotInferDelegateType
	}
}
