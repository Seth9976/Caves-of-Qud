using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200023F RID: 575
	internal static class CSharpResources
	{
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06002F25 RID: 12069 RVA: 0x000F2943 File Offset: 0x000F0B43
		internal static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager resourceManager;
				if ((resourceManager = CSharpResources.s_resourceManager) == null)
				{
					resourceManager = (CSharpResources.s_resourceManager = new ResourceManager(typeof(CSharpResources)));
				}
				return resourceManager;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06002F26 RID: 12070 RVA: 0x000F2963 File Offset: 0x000F0B63
		// (set) Token: 0x06002F27 RID: 12071 RVA: 0x000F296A File Offset: 0x000F0B6A
		internal static CultureInfo Culture { get; set; }

		// Token: 0x06002F28 RID: 12072 RVA: 0x000F2972 File Offset: 0x000F0B72
		[MethodImpl(256)]
		internal static string GetResourceString(string resourceKey, string defaultValue = null)
		{
			return CSharpResources.ResourceManager.GetString(resourceKey, CSharpResources.Culture);
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06002F29 RID: 12073 RVA: 0x000F2984 File Offset: 0x000F0B84
		internal static string IDS_NULL
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_NULL", null);
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06002F2A RID: 12074 RVA: 0x000F2991 File Offset: 0x000F0B91
		internal static string IDS_ThrowExpression
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_ThrowExpression", null);
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06002F2B RID: 12075 RVA: 0x000F299E File Offset: 0x000F0B9E
		internal static string IDS_FeatureSwitchExpression
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureSwitchExpression", null);
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06002F2C RID: 12076 RVA: 0x000F29AB File Offset: 0x000F0BAB
		internal static string IDS_FeatureLocalFunctionAttributes
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureLocalFunctionAttributes", null);
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06002F2D RID: 12077 RVA: 0x000F29B8 File Offset: 0x000F0BB8
		internal static string IDS_FeatureExternLocalFunctions
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureExternLocalFunctions", null);
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x000F29C5 File Offset: 0x000F0BC5
		internal static string IDS_RELATEDERROR
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_RELATEDERROR", null);
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x000F29D2 File Offset: 0x000F0BD2
		internal static string IDS_RELATEDWARNING
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_RELATEDWARNING", null);
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06002F30 RID: 12080 RVA: 0x000F29DF File Offset: 0x000F0BDF
		internal static string IDS_XMLIGNORED
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_XMLIGNORED", null);
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06002F31 RID: 12081 RVA: 0x000F29EC File Offset: 0x000F0BEC
		internal static string IDS_XMLIGNORED2
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_XMLIGNORED2", null);
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06002F32 RID: 12082 RVA: 0x000F29F9 File Offset: 0x000F0BF9
		internal static string IDS_XMLFAILEDINCLUDE
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_XMLFAILEDINCLUDE", null);
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x000F2A06 File Offset: 0x000F0C06
		internal static string IDS_XMLBADINCLUDE
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_XMLBADINCLUDE", null);
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06002F34 RID: 12084 RVA: 0x000F2A13 File Offset: 0x000F0C13
		internal static string IDS_XMLNOINCLUDE
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_XMLNOINCLUDE", null);
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06002F35 RID: 12085 RVA: 0x000F2A20 File Offset: 0x000F0C20
		internal static string IDS_XMLMISSINGINCLUDEFILE
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_XMLMISSINGINCLUDEFILE", null);
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06002F36 RID: 12086 RVA: 0x000F2A2D File Offset: 0x000F0C2D
		internal static string IDS_XMLMISSINGINCLUDEPATH
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_XMLMISSINGINCLUDEPATH", null);
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06002F37 RID: 12087 RVA: 0x000F2A3A File Offset: 0x000F0C3A
		internal static string IDS_GlobalNamespace
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_GlobalNamespace", null);
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06002F38 RID: 12088 RVA: 0x000F2A47 File Offset: 0x000F0C47
		internal static string IDS_FeatureGenerics
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureGenerics", null);
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x000F2A54 File Offset: 0x000F0C54
		internal static string IDS_FeatureAnonDelegates
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureAnonDelegates", null);
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x000F2A61 File Offset: 0x000F0C61
		internal static string IDS_FeatureModuleAttrLoc
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureModuleAttrLoc", null);
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x000F2A6E File Offset: 0x000F0C6E
		internal static string IDS_FeatureGlobalNamespace
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureGlobalNamespace", null);
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06002F3C RID: 12092 RVA: 0x000F2A7B File Offset: 0x000F0C7B
		internal static string IDS_FeatureFixedBuffer
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureFixedBuffer", null);
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06002F3D RID: 12093 RVA: 0x000F2A88 File Offset: 0x000F0C88
		internal static string IDS_FeaturePragma
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeaturePragma", null);
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06002F3E RID: 12094 RVA: 0x000F2A95 File Offset: 0x000F0C95
		internal static string IDS_FeatureStaticClasses
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureStaticClasses", null);
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06002F3F RID: 12095 RVA: 0x000F2AA2 File Offset: 0x000F0CA2
		internal static string IDS_FeatureReadOnlyStructs
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureReadOnlyStructs", null);
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06002F40 RID: 12096 RVA: 0x000F2AAF File Offset: 0x000F0CAF
		internal static string IDS_FeaturePartialTypes
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeaturePartialTypes", null);
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06002F41 RID: 12097 RVA: 0x000F2ABC File Offset: 0x000F0CBC
		internal static string IDS_FeatureAsync
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureAsync", null);
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06002F42 RID: 12098 RVA: 0x000F2AC9 File Offset: 0x000F0CC9
		internal static string IDS_FeatureSwitchOnBool
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureSwitchOnBool", null);
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06002F43 RID: 12099 RVA: 0x000F2AD6 File Offset: 0x000F0CD6
		internal static string IDS_MethodGroup
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_MethodGroup", null);
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06002F44 RID: 12100 RVA: 0x000F2AE3 File Offset: 0x000F0CE3
		internal static string IDS_AnonMethod
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_AnonMethod", null);
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06002F45 RID: 12101 RVA: 0x000F2AF0 File Offset: 0x000F0CF0
		internal static string IDS_Lambda
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_Lambda", null);
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06002F46 RID: 12102 RVA: 0x000F2AFD File Offset: 0x000F0CFD
		internal static string IDS_Collection
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_Collection", null);
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06002F47 RID: 12103 RVA: 0x000F2B0A File Offset: 0x000F0D0A
		internal static string IDS_Disposable
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_Disposable", null);
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06002F48 RID: 12104 RVA: 0x000F2B17 File Offset: 0x000F0D17
		internal static string IDS_FeaturePropertyAccessorMods
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeaturePropertyAccessorMods", null);
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002F49 RID: 12105 RVA: 0x000F2B24 File Offset: 0x000F0D24
		internal static string IDS_FeatureExternAlias
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureExternAlias", null);
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002F4A RID: 12106 RVA: 0x000F2B31 File Offset: 0x000F0D31
		internal static string IDS_FeatureIterators
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureIterators", null);
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06002F4B RID: 12107 RVA: 0x000F2B3E File Offset: 0x000F0D3E
		internal static string IDS_FeatureDefault
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureDefault", null);
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06002F4C RID: 12108 RVA: 0x000F2B4B File Offset: 0x000F0D4B
		internal static string IDS_FeatureAsyncStreams
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureAsyncStreams", null);
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06002F4D RID: 12109 RVA: 0x000F2B58 File Offset: 0x000F0D58
		internal static string IDS_FeatureUnmanagedConstructedTypes
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureUnmanagedConstructedTypes", null);
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06002F4E RID: 12110 RVA: 0x000F2B65 File Offset: 0x000F0D65
		internal static string IDS_FeatureReadOnlyMembers
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureReadOnlyMembers", null);
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06002F4F RID: 12111 RVA: 0x000F2B72 File Offset: 0x000F0D72
		internal static string IDS_FeatureDefaultLiteral
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureDefaultLiteral", null);
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06002F50 RID: 12112 RVA: 0x000F2B7F File Offset: 0x000F0D7F
		internal static string IDS_FeaturePrivateProtected
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeaturePrivateProtected", null);
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06002F51 RID: 12113 RVA: 0x000F2B8C File Offset: 0x000F0D8C
		internal static string IDS_FeatureTupleEquality
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureTupleEquality", null);
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06002F52 RID: 12114 RVA: 0x000F2B99 File Offset: 0x000F0D99
		internal static string IDS_FeatureNullable
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureNullable", null);
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06002F53 RID: 12115 RVA: 0x000F2BA6 File Offset: 0x000F0DA6
		internal static string IDS_FeaturePatternMatching
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeaturePatternMatching", null);
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06002F54 RID: 12116 RVA: 0x000F2BB3 File Offset: 0x000F0DB3
		internal static string IDS_FeatureExpressionBodiedAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureExpressionBodiedAccessor", null);
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06002F55 RID: 12117 RVA: 0x000F2BC0 File Offset: 0x000F0DC0
		internal static string IDS_FeatureExpressionBodiedDeOrConstructor
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureExpressionBodiedDeOrConstructor", null);
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06002F56 RID: 12118 RVA: 0x000F2BCD File Offset: 0x000F0DCD
		internal static string IDS_FeatureThrowExpression
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureThrowExpression", null);
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06002F57 RID: 12119 RVA: 0x000F2BDA File Offset: 0x000F0DDA
		internal static string IDS_FeatureImplicitArray
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureImplicitArray", null);
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x000F2BE7 File Offset: 0x000F0DE7
		internal static string IDS_FeatureImplicitLocal
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureImplicitLocal", null);
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06002F59 RID: 12121 RVA: 0x000F2BF4 File Offset: 0x000F0DF4
		internal static string IDS_FeatureAnonymousTypes
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureAnonymousTypes", null);
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06002F5A RID: 12122 RVA: 0x000F2C01 File Offset: 0x000F0E01
		internal static string IDS_FeatureAutoImplementedProperties
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureAutoImplementedProperties", null);
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06002F5B RID: 12123 RVA: 0x000F2C0E File Offset: 0x000F0E0E
		internal static string IDS_FeatureReadonlyAutoImplementedProperties
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureReadonlyAutoImplementedProperties", null);
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06002F5C RID: 12124 RVA: 0x000F2C1B File Offset: 0x000F0E1B
		internal static string IDS_FeatureObjectInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureObjectInitializer", null);
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06002F5D RID: 12125 RVA: 0x000F2C28 File Offset: 0x000F0E28
		internal static string IDS_FeatureCollectionInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureCollectionInitializer", null);
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06002F5E RID: 12126 RVA: 0x000F2C35 File Offset: 0x000F0E35
		internal static string IDS_FeatureQueryExpression
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureQueryExpression", null);
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06002F5F RID: 12127 RVA: 0x000F2C42 File Offset: 0x000F0E42
		internal static string IDS_FeatureExtensionMethod
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureExtensionMethod", null);
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06002F60 RID: 12128 RVA: 0x000F2C4F File Offset: 0x000F0E4F
		internal static string IDS_FeaturePartialMethod
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeaturePartialMethod", null);
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06002F61 RID: 12129 RVA: 0x000F2C5C File Offset: 0x000F0E5C
		internal static string IDS_SK_METHOD
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_SK_METHOD", null);
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06002F62 RID: 12130 RVA: 0x000F2C69 File Offset: 0x000F0E69
		internal static string IDS_SK_TYPE
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_SK_TYPE", null);
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06002F63 RID: 12131 RVA: 0x000F2C76 File Offset: 0x000F0E76
		internal static string IDS_SK_NAMESPACE
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_SK_NAMESPACE", null);
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06002F64 RID: 12132 RVA: 0x000F2C83 File Offset: 0x000F0E83
		internal static string IDS_SK_FIELD
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_SK_FIELD", null);
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06002F65 RID: 12133 RVA: 0x000F2C90 File Offset: 0x000F0E90
		internal static string IDS_SK_PROPERTY
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_SK_PROPERTY", null);
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002F66 RID: 12134 RVA: 0x000F2C9D File Offset: 0x000F0E9D
		internal static string IDS_SK_UNKNOWN
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_SK_UNKNOWN", null);
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06002F67 RID: 12135 RVA: 0x000F2CAA File Offset: 0x000F0EAA
		internal static string IDS_SK_VARIABLE
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_SK_VARIABLE", null);
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06002F68 RID: 12136 RVA: 0x000F2CB7 File Offset: 0x000F0EB7
		internal static string IDS_SK_LABEL
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_SK_LABEL", null);
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06002F69 RID: 12137 RVA: 0x000F2CC4 File Offset: 0x000F0EC4
		internal static string IDS_SK_EVENT
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_SK_EVENT", null);
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06002F6A RID: 12138 RVA: 0x000F2CD1 File Offset: 0x000F0ED1
		internal static string IDS_SK_TYVAR
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_SK_TYVAR", null);
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06002F6B RID: 12139 RVA: 0x000F2CDE File Offset: 0x000F0EDE
		internal static string IDS_SK_ALIAS
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_SK_ALIAS", null);
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06002F6C RID: 12140 RVA: 0x000F2CEB File Offset: 0x000F0EEB
		internal static string IDS_SK_EXTERNALIAS
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_SK_EXTERNALIAS", null);
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06002F6D RID: 12141 RVA: 0x000F2CF8 File Offset: 0x000F0EF8
		internal static string IDS_SK_CONSTRUCTOR
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_SK_CONSTRUCTOR", null);
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06002F6E RID: 12142 RVA: 0x000F2D05 File Offset: 0x000F0F05
		internal static string IDS_FOREACHLOCAL
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FOREACHLOCAL", null);
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06002F6F RID: 12143 RVA: 0x000F2D12 File Offset: 0x000F0F12
		internal static string IDS_FIXEDLOCAL
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FIXEDLOCAL", null);
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06002F70 RID: 12144 RVA: 0x000F2D1F File Offset: 0x000F0F1F
		internal static string IDS_USINGLOCAL
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_USINGLOCAL", null);
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002F71 RID: 12145 RVA: 0x000F2D2C File Offset: 0x000F0F2C
		internal static string IDS_Contravariant
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_Contravariant", null);
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002F72 RID: 12146 RVA: 0x000F2D39 File Offset: 0x000F0F39
		internal static string IDS_Contravariantly
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_Contravariantly", null);
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002F73 RID: 12147 RVA: 0x000F2D46 File Offset: 0x000F0F46
		internal static string IDS_Covariant
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_Covariant", null);
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06002F74 RID: 12148 RVA: 0x000F2D53 File Offset: 0x000F0F53
		internal static string IDS_Covariantly
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_Covariantly", null);
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06002F75 RID: 12149 RVA: 0x000F2D60 File Offset: 0x000F0F60
		internal static string IDS_Invariantly
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_Invariantly", null);
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06002F76 RID: 12150 RVA: 0x000F2D6D File Offset: 0x000F0F6D
		internal static string IDS_FeatureDynamic
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureDynamic", null);
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06002F77 RID: 12151 RVA: 0x000F2D7A File Offset: 0x000F0F7A
		internal static string IDS_FeatureNamedArgument
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureNamedArgument", null);
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06002F78 RID: 12152 RVA: 0x000F2D87 File Offset: 0x000F0F87
		internal static string IDS_FeatureOptionalParameter
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureOptionalParameter", null);
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06002F79 RID: 12153 RVA: 0x000F2D94 File Offset: 0x000F0F94
		internal static string IDS_FeatureExceptionFilter
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureExceptionFilter", null);
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06002F7A RID: 12154 RVA: 0x000F2DA1 File Offset: 0x000F0FA1
		internal static string IDS_FeatureTypeVariance
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureTypeVariance", null);
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06002F7B RID: 12155 RVA: 0x000F2DAE File Offset: 0x000F0FAE
		internal static string IDS_Parameter
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_Parameter", null);
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06002F7C RID: 12156 RVA: 0x000F2DBB File Offset: 0x000F0FBB
		internal static string IDS_Return
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_Return", null);
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06002F7D RID: 12157 RVA: 0x000F2DC8 File Offset: 0x000F0FC8
		internal static string XML_InvalidToken
		{
			get
			{
				return CSharpResources.GetResourceString("XML_InvalidToken", null);
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06002F7E RID: 12158 RVA: 0x000F2DD5 File Offset: 0x000F0FD5
		internal static string XML_IncorrectComment
		{
			get
			{
				return CSharpResources.GetResourceString("XML_IncorrectComment", null);
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06002F7F RID: 12159 RVA: 0x000F2DE2 File Offset: 0x000F0FE2
		internal static string XML_InvalidCharEntity
		{
			get
			{
				return CSharpResources.GetResourceString("XML_InvalidCharEntity", null);
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06002F80 RID: 12160 RVA: 0x000F2DEF File Offset: 0x000F0FEF
		internal static string XML_ExpectedEndOfTag
		{
			get
			{
				return CSharpResources.GetResourceString("XML_ExpectedEndOfTag", null);
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06002F81 RID: 12161 RVA: 0x000F2DFC File Offset: 0x000F0FFC
		internal static string XML_ExpectedIdentifier
		{
			get
			{
				return CSharpResources.GetResourceString("XML_ExpectedIdentifier", null);
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06002F82 RID: 12162 RVA: 0x000F2E09 File Offset: 0x000F1009
		internal static string XML_InvalidUnicodeChar
		{
			get
			{
				return CSharpResources.GetResourceString("XML_InvalidUnicodeChar", null);
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06002F83 RID: 12163 RVA: 0x000F2E16 File Offset: 0x000F1016
		internal static string XML_InvalidWhitespace
		{
			get
			{
				return CSharpResources.GetResourceString("XML_InvalidWhitespace", null);
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06002F84 RID: 12164 RVA: 0x000F2E23 File Offset: 0x000F1023
		internal static string XML_LessThanInAttributeValue
		{
			get
			{
				return CSharpResources.GetResourceString("XML_LessThanInAttributeValue", null);
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06002F85 RID: 12165 RVA: 0x000F2E30 File Offset: 0x000F1030
		internal static string XML_MissingEqualsAttribute
		{
			get
			{
				return CSharpResources.GetResourceString("XML_MissingEqualsAttribute", null);
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06002F86 RID: 12166 RVA: 0x000F2E3D File Offset: 0x000F103D
		internal static string XML_RefUndefinedEntity_1
		{
			get
			{
				return CSharpResources.GetResourceString("XML_RefUndefinedEntity_1", null);
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06002F87 RID: 12167 RVA: 0x000F2E4A File Offset: 0x000F104A
		internal static string XML_StringLiteralNoStartQuote
		{
			get
			{
				return CSharpResources.GetResourceString("XML_StringLiteralNoStartQuote", null);
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06002F88 RID: 12168 RVA: 0x000F2E57 File Offset: 0x000F1057
		internal static string XML_StringLiteralNoEndQuote
		{
			get
			{
				return CSharpResources.GetResourceString("XML_StringLiteralNoEndQuote", null);
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06002F89 RID: 12169 RVA: 0x000F2E64 File Offset: 0x000F1064
		internal static string XML_StringLiteralNonAsciiQuote
		{
			get
			{
				return CSharpResources.GetResourceString("XML_StringLiteralNonAsciiQuote", null);
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06002F8A RID: 12170 RVA: 0x000F2E71 File Offset: 0x000F1071
		internal static string XML_EndTagNotExpected
		{
			get
			{
				return CSharpResources.GetResourceString("XML_EndTagNotExpected", null);
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06002F8B RID: 12171 RVA: 0x000F2E7E File Offset: 0x000F107E
		internal static string XML_ElementTypeMatch
		{
			get
			{
				return CSharpResources.GetResourceString("XML_ElementTypeMatch", null);
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002F8C RID: 12172 RVA: 0x000F2E8B File Offset: 0x000F108B
		internal static string XML_EndTagExpected
		{
			get
			{
				return CSharpResources.GetResourceString("XML_EndTagExpected", null);
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002F8D RID: 12173 RVA: 0x000F2E98 File Offset: 0x000F1098
		internal static string XML_WhitespaceMissing
		{
			get
			{
				return CSharpResources.GetResourceString("XML_WhitespaceMissing", null);
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06002F8E RID: 12174 RVA: 0x000F2EA5 File Offset: 0x000F10A5
		internal static string XML_ExpectedEndOfXml
		{
			get
			{
				return CSharpResources.GetResourceString("XML_ExpectedEndOfXml", null);
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06002F8F RID: 12175 RVA: 0x000F2EB2 File Offset: 0x000F10B2
		internal static string XML_CDataEndTagNotAllowed
		{
			get
			{
				return CSharpResources.GetResourceString("XML_CDataEndTagNotAllowed", null);
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002F90 RID: 12176 RVA: 0x000F2EBF File Offset: 0x000F10BF
		internal static string XML_DuplicateAttribute
		{
			get
			{
				return CSharpResources.GetResourceString("XML_DuplicateAttribute", null);
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002F91 RID: 12177 RVA: 0x000F2ECC File Offset: 0x000F10CC
		internal static string ERR_NoMetadataFile
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoMetadataFile", null);
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06002F92 RID: 12178 RVA: 0x000F2ED9 File Offset: 0x000F10D9
		internal static string ERR_MetadataReferencesNotSupported
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MetadataReferencesNotSupported", null);
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06002F93 RID: 12179 RVA: 0x000F2EE6 File Offset: 0x000F10E6
		internal static string FTL_MetadataCantOpenFile
		{
			get
			{
				return CSharpResources.GetResourceString("FTL_MetadataCantOpenFile", null);
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06002F94 RID: 12180 RVA: 0x000F2EF3 File Offset: 0x000F10F3
		internal static string ERR_NoTypeDef
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoTypeDef", null);
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06002F95 RID: 12181 RVA: 0x000F2F00 File Offset: 0x000F1100
		internal static string ERR_NoTypeDefFromModule
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoTypeDefFromModule", null);
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06002F96 RID: 12182 RVA: 0x000F2F0D File Offset: 0x000F110D
		internal static string ERR_OutputWriteFailed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OutputWriteFailed", null);
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06002F97 RID: 12183 RVA: 0x000F2F1A File Offset: 0x000F111A
		internal static string ERR_MultipleEntryPoints
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MultipleEntryPoints", null);
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06002F98 RID: 12184 RVA: 0x000F2F27 File Offset: 0x000F1127
		internal static string ERR_BadBinaryOps
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadBinaryOps", null);
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06002F99 RID: 12185 RVA: 0x000F2F34 File Offset: 0x000F1134
		internal static string ERR_AmbigBinaryOpsOnUnconstrainedDefault
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AmbigBinaryOpsOnUnconstrainedDefault", null);
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06002F9A RID: 12186 RVA: 0x000F2F41 File Offset: 0x000F1141
		internal static string ERR_IntDivByZero
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IntDivByZero", null);
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06002F9B RID: 12187 RVA: 0x000F2F4E File Offset: 0x000F114E
		internal static string ERR_BadIndexLHS
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadIndexLHS", null);
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06002F9C RID: 12188 RVA: 0x000F2F5B File Offset: 0x000F115B
		internal static string ERR_BadIndexCount
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadIndexCount", null);
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06002F9D RID: 12189 RVA: 0x000F2F68 File Offset: 0x000F1168
		internal static string ERR_BadUnaryOp
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadUnaryOp", null);
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06002F9E RID: 12190 RVA: 0x000F2F75 File Offset: 0x000F1175
		internal static string ERR_BadOpOnNullOrDefaultOrNew
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadOpOnNullOrDefaultOrNew", null);
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06002F9F RID: 12191 RVA: 0x000F2F82 File Offset: 0x000F1182
		internal static string ERR_ThisInStaticMeth
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ThisInStaticMeth", null);
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x000F2F8F File Offset: 0x000F118F
		internal static string ERR_ThisInBadContext
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ThisInBadContext", null);
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06002FA1 RID: 12193 RVA: 0x000F2F9C File Offset: 0x000F119C
		internal static string ERR_OmittedTypeArgument
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OmittedTypeArgument", null);
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x000F2FA9 File Offset: 0x000F11A9
		internal static string WRN_InvalidMainSig
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_InvalidMainSig", null);
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x000F2FB6 File Offset: 0x000F11B6
		internal static string WRN_InvalidMainSig_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_InvalidMainSig_Title", null);
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06002FA4 RID: 12196 RVA: 0x000F2FC3 File Offset: 0x000F11C3
		internal static string ERR_NoImplicitConv
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoImplicitConv", null);
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06002FA5 RID: 12197 RVA: 0x000F2FD0 File Offset: 0x000F11D0
		internal static string ERR_NoExplicitConv
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoExplicitConv", null);
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x000F2FDD File Offset: 0x000F11DD
		internal static string ERR_ConstOutOfRange
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConstOutOfRange", null);
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06002FA7 RID: 12199 RVA: 0x000F2FEA File Offset: 0x000F11EA
		internal static string ERR_AmbigBinaryOps
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AmbigBinaryOps", null);
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06002FA8 RID: 12200 RVA: 0x000F2FF7 File Offset: 0x000F11F7
		internal static string ERR_AmbigBinaryOpsOnDefault
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AmbigBinaryOpsOnDefault", null);
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06002FA9 RID: 12201 RVA: 0x000F3004 File Offset: 0x000F1204
		internal static string ERR_AmbigUnaryOp
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AmbigUnaryOp", null);
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06002FAA RID: 12202 RVA: 0x000F3011 File Offset: 0x000F1211
		internal static string ERR_InAttrOnOutParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InAttrOnOutParam", null);
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06002FAB RID: 12203 RVA: 0x000F301E File Offset: 0x000F121E
		internal static string ERR_ValueCantBeNull
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ValueCantBeNull", null);
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06002FAC RID: 12204 RVA: 0x000F302B File Offset: 0x000F122B
		internal static string ERR_NoExplicitBuiltinConv
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoExplicitBuiltinConv", null);
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06002FAD RID: 12205 RVA: 0x000F3038 File Offset: 0x000F1238
		internal static string FTL_DebugEmitFailure
		{
			get
			{
				return CSharpResources.GetResourceString("FTL_DebugEmitFailure", null);
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x000F3045 File Offset: 0x000F1245
		internal static string ERR_BadVisReturnType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadVisReturnType", null);
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06002FAF RID: 12207 RVA: 0x000F3052 File Offset: 0x000F1252
		internal static string ERR_BadVisParamType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadVisParamType", null);
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x000F305F File Offset: 0x000F125F
		internal static string ERR_BadVisFieldType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadVisFieldType", null);
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x000F306C File Offset: 0x000F126C
		internal static string ERR_BadVisPropertyType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadVisPropertyType", null);
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x000F3079 File Offset: 0x000F1279
		internal static string ERR_BadVisIndexerReturn
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadVisIndexerReturn", null);
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x000F3086 File Offset: 0x000F1286
		internal static string ERR_BadVisIndexerParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadVisIndexerParam", null);
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x000F3093 File Offset: 0x000F1293
		internal static string ERR_BadVisOpReturn
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadVisOpReturn", null);
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06002FB5 RID: 12213 RVA: 0x000F30A0 File Offset: 0x000F12A0
		internal static string ERR_BadVisOpParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadVisOpParam", null);
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002FB6 RID: 12214 RVA: 0x000F30AD File Offset: 0x000F12AD
		internal static string ERR_BadVisDelegateReturn
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadVisDelegateReturn", null);
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002FB7 RID: 12215 RVA: 0x000F30BA File Offset: 0x000F12BA
		internal static string ERR_BadVisDelegateParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadVisDelegateParam", null);
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x000F30C7 File Offset: 0x000F12C7
		internal static string ERR_BadVisBaseClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadVisBaseClass", null);
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002FB9 RID: 12217 RVA: 0x000F30D4 File Offset: 0x000F12D4
		internal static string ERR_BadVisBaseInterface
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadVisBaseInterface", null);
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002FBA RID: 12218 RVA: 0x000F30E1 File Offset: 0x000F12E1
		internal static string ERR_EventNeedsBothAccessors
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EventNeedsBothAccessors", null);
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x000F30EE File Offset: 0x000F12EE
		internal static string ERR_AbstractEventHasAccessors
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AbstractEventHasAccessors", null);
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x000F30FB File Offset: 0x000F12FB
		internal static string ERR_EventNotDelegate
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EventNotDelegate", null);
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06002FBD RID: 12221 RVA: 0x000F3108 File Offset: 0x000F1308
		internal static string WRN_UnreferencedEvent
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreferencedEvent", null);
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06002FBE RID: 12222 RVA: 0x000F3115 File Offset: 0x000F1315
		internal static string WRN_UnreferencedEvent_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreferencedEvent_Title", null);
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06002FBF RID: 12223 RVA: 0x000F3122 File Offset: 0x000F1322
		internal static string ERR_InterfaceEventInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InterfaceEventInitializer", null);
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x000F312F File Offset: 0x000F132F
		internal static string ERR_BadEventUsage
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadEventUsage", null);
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x000F313C File Offset: 0x000F133C
		internal static string ERR_ExplicitEventFieldImpl
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExplicitEventFieldImpl", null);
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002FC2 RID: 12226 RVA: 0x000F3149 File Offset: 0x000F1349
		internal static string ERR_CantOverrideNonEvent
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantOverrideNonEvent", null);
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x000F3156 File Offset: 0x000F1356
		internal static string ERR_AddRemoveMustHaveBody
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AddRemoveMustHaveBody", null);
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06002FC4 RID: 12228 RVA: 0x000F3163 File Offset: 0x000F1363
		internal static string ERR_AbstractEventInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AbstractEventInitializer", null);
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06002FC5 RID: 12229 RVA: 0x000F3170 File Offset: 0x000F1370
		internal static string ERR_ReservedAssemblyName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ReservedAssemblyName", null);
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06002FC6 RID: 12230 RVA: 0x000F317D File Offset: 0x000F137D
		internal static string ERR_ReservedEnumerator
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ReservedEnumerator", null);
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06002FC7 RID: 12231 RVA: 0x000F318A File Offset: 0x000F138A
		internal static string ERR_AsMustHaveReferenceType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AsMustHaveReferenceType", null);
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002FC8 RID: 12232 RVA: 0x000F3197 File Offset: 0x000F1397
		internal static string WRN_LowercaseEllSuffix
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_LowercaseEllSuffix", null);
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06002FC9 RID: 12233 RVA: 0x000F31A4 File Offset: 0x000F13A4
		internal static string WRN_LowercaseEllSuffix_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_LowercaseEllSuffix_Title", null);
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06002FCA RID: 12234 RVA: 0x000F31B1 File Offset: 0x000F13B1
		internal static string ERR_BadEventUsageNoField
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadEventUsageNoField", null);
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002FCB RID: 12235 RVA: 0x000F31BE File Offset: 0x000F13BE
		internal static string ERR_ConstraintOnlyAllowedOnGenericDecl
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConstraintOnlyAllowedOnGenericDecl", null);
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002FCC RID: 12236 RVA: 0x000F31CB File Offset: 0x000F13CB
		internal static string ERR_TypeParamMustBeIdentifier
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TypeParamMustBeIdentifier", null);
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002FCD RID: 12237 RVA: 0x000F31D8 File Offset: 0x000F13D8
		internal static string ERR_MemberReserved
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MemberReserved", null);
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06002FCE RID: 12238 RVA: 0x000F31E5 File Offset: 0x000F13E5
		internal static string ERR_DuplicateParamName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateParamName", null);
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06002FCF RID: 12239 RVA: 0x000F31F2 File Offset: 0x000F13F2
		internal static string ERR_DuplicateNameInNS
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateNameInNS", null);
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06002FD0 RID: 12240 RVA: 0x000F31FF File Offset: 0x000F13FF
		internal static string ERR_DuplicateNameInClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateNameInClass", null);
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x000F320C File Offset: 0x000F140C
		internal static string ERR_NameNotInContext
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NameNotInContext", null);
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06002FD2 RID: 12242 RVA: 0x000F3219 File Offset: 0x000F1419
		internal static string ERR_NameNotInContextPossibleMissingReference
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NameNotInContextPossibleMissingReference", null);
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x000F3226 File Offset: 0x000F1426
		internal static string ERR_AmbigContext
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AmbigContext", null);
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x000F3233 File Offset: 0x000F1433
		internal static string WRN_DuplicateUsing
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DuplicateUsing", null);
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x000F3240 File Offset: 0x000F1440
		internal static string WRN_DuplicateUsing_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DuplicateUsing_Title", null);
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06002FD6 RID: 12246 RVA: 0x000F324D File Offset: 0x000F144D
		internal static string ERR_BadMemberFlag
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadMemberFlag", null);
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06002FD7 RID: 12247 RVA: 0x000F325A File Offset: 0x000F145A
		internal static string ERR_BadInitAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadInitAccessor", null);
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06002FD8 RID: 12248 RVA: 0x000F3267 File Offset: 0x000F1467
		internal static string ERR_BadMemberProtection
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadMemberProtection", null);
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002FD9 RID: 12249 RVA: 0x000F3274 File Offset: 0x000F1474
		internal static string WRN_NewRequired
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NewRequired", null);
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06002FDA RID: 12250 RVA: 0x000F3281 File Offset: 0x000F1481
		internal static string WRN_NewRequired_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NewRequired_Title", null);
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06002FDB RID: 12251 RVA: 0x000F328E File Offset: 0x000F148E
		internal static string WRN_NewRequired_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NewRequired_Description", null);
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06002FDC RID: 12252 RVA: 0x000F329B File Offset: 0x000F149B
		internal static string WRN_NewNotRequired
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NewNotRequired", null);
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06002FDD RID: 12253 RVA: 0x000F32A8 File Offset: 0x000F14A8
		internal static string WRN_NewNotRequired_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NewNotRequired_Title", null);
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06002FDE RID: 12254 RVA: 0x000F32B5 File Offset: 0x000F14B5
		internal static string ERR_CircConstValue
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CircConstValue", null);
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06002FDF RID: 12255 RVA: 0x000F32C2 File Offset: 0x000F14C2
		internal static string ERR_MemberAlreadyExists
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MemberAlreadyExists", null);
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002FE0 RID: 12256 RVA: 0x000F32CF File Offset: 0x000F14CF
		internal static string ERR_StaticNotVirtual
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StaticNotVirtual", null);
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002FE1 RID: 12257 RVA: 0x000F32DC File Offset: 0x000F14DC
		internal static string ERR_OverrideNotNew
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OverrideNotNew", null);
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002FE2 RID: 12258 RVA: 0x000F32E9 File Offset: 0x000F14E9
		internal static string WRN_NewOrOverrideExpected
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NewOrOverrideExpected", null);
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002FE3 RID: 12259 RVA: 0x000F32F6 File Offset: 0x000F14F6
		internal static string WRN_NewOrOverrideExpected_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NewOrOverrideExpected_Title", null);
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06002FE4 RID: 12260 RVA: 0x000F3303 File Offset: 0x000F1503
		internal static string ERR_OverrideNotExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OverrideNotExpected", null);
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002FE5 RID: 12261 RVA: 0x000F3310 File Offset: 0x000F1510
		internal static string ERR_NamespaceUnexpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NamespaceUnexpected", null);
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002FE6 RID: 12262 RVA: 0x000F331D File Offset: 0x000F151D
		internal static string ERR_NoSuchMember
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoSuchMember", null);
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002FE7 RID: 12263 RVA: 0x000F332A File Offset: 0x000F152A
		internal static string ERR_BadSKknown
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadSKknown", null);
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x000F3337 File Offset: 0x000F1537
		internal static string ERR_BadSKunknown
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadSKunknown", null);
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x000F3344 File Offset: 0x000F1544
		internal static string ERR_ObjectRequired
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ObjectRequired", null);
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06002FEA RID: 12266 RVA: 0x000F3351 File Offset: 0x000F1551
		internal static string ERR_AmbigCall
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AmbigCall", null);
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06002FEB RID: 12267 RVA: 0x000F335E File Offset: 0x000F155E
		internal static string ERR_BadAccess
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAccess", null);
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06002FEC RID: 12268 RVA: 0x000F336B File Offset: 0x000F156B
		internal static string ERR_MethDelegateMismatch
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MethDelegateMismatch", null);
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06002FED RID: 12269 RVA: 0x000F3378 File Offset: 0x000F1578
		internal static string ERR_RetObjectRequired
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RetObjectRequired", null);
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06002FEE RID: 12270 RVA: 0x000F3385 File Offset: 0x000F1585
		internal static string ERR_RetNoObjectRequired
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RetNoObjectRequired", null);
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002FEF RID: 12271 RVA: 0x000F3392 File Offset: 0x000F1592
		internal static string ERR_LocalDuplicate
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LocalDuplicate", null);
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06002FF0 RID: 12272 RVA: 0x000F339F File Offset: 0x000F159F
		internal static string ERR_AssgLvalueExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AssgLvalueExpected", null);
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06002FF1 RID: 12273 RVA: 0x000F33AC File Offset: 0x000F15AC
		internal static string ERR_StaticConstParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StaticConstParam", null);
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002FF2 RID: 12274 RVA: 0x000F33B9 File Offset: 0x000F15B9
		internal static string ERR_NotConstantExpression
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NotConstantExpression", null);
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06002FF3 RID: 12275 RVA: 0x000F33C6 File Offset: 0x000F15C6
		internal static string ERR_NotNullConstRefField
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NotNullConstRefField", null);
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002FF4 RID: 12276 RVA: 0x000F33D3 File Offset: 0x000F15D3
		internal static string ERR_LocalIllegallyOverrides
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LocalIllegallyOverrides", null);
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002FF5 RID: 12277 RVA: 0x000F33E0 File Offset: 0x000F15E0
		internal static string ERR_BadUsingNamespace
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadUsingNamespace", null);
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002FF6 RID: 12278 RVA: 0x000F33ED File Offset: 0x000F15ED
		internal static string ERR_BadUsingType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadUsingType", null);
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002FF7 RID: 12279 RVA: 0x000F33FA File Offset: 0x000F15FA
		internal static string ERR_NoAliasHere
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoAliasHere", null);
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002FF8 RID: 12280 RVA: 0x000F3407 File Offset: 0x000F1607
		internal static string ERR_NoBreakOrCont
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoBreakOrCont", null);
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002FF9 RID: 12281 RVA: 0x000F3414 File Offset: 0x000F1614
		internal static string ERR_DuplicateLabel
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateLabel", null);
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06002FFA RID: 12282 RVA: 0x000F3421 File Offset: 0x000F1621
		internal static string ERR_NoConstructors
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoConstructors", null);
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06002FFB RID: 12283 RVA: 0x000F342E File Offset: 0x000F162E
		internal static string ERR_NoNewAbstract
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoNewAbstract", null);
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002FFC RID: 12284 RVA: 0x000F343B File Offset: 0x000F163B
		internal static string ERR_ConstValueRequired
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConstValueRequired", null);
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002FFD RID: 12285 RVA: 0x000F3448 File Offset: 0x000F1648
		internal static string ERR_CircularBase
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CircularBase", null);
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002FFE RID: 12286 RVA: 0x000F3455 File Offset: 0x000F1655
		internal static string ERR_BadDelegateConstructor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadDelegateConstructor", null);
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002FFF RID: 12287 RVA: 0x000F3462 File Offset: 0x000F1662
		internal static string ERR_MethodNameExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MethodNameExpected", null);
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06003000 RID: 12288 RVA: 0x000F346F File Offset: 0x000F166F
		internal static string ERR_ConstantExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConstantExpected", null);
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06003001 RID: 12289 RVA: 0x000F347C File Offset: 0x000F167C
		internal static string ERR_V6SwitchGoverningTypeValueExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_V6SwitchGoverningTypeValueExpected", null);
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06003002 RID: 12290 RVA: 0x000F3489 File Offset: 0x000F1689
		internal static string ERR_IntegralTypeValueExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IntegralTypeValueExpected", null);
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06003003 RID: 12291 RVA: 0x000F3496 File Offset: 0x000F1696
		internal static string ERR_DuplicateCaseLabel
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateCaseLabel", null);
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06003004 RID: 12292 RVA: 0x000F34A3 File Offset: 0x000F16A3
		internal static string ERR_InvalidGotoCase
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidGotoCase", null);
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06003005 RID: 12293 RVA: 0x000F34B0 File Offset: 0x000F16B0
		internal static string ERR_PropertyLacksGet
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PropertyLacksGet", null);
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06003006 RID: 12294 RVA: 0x000F34BD File Offset: 0x000F16BD
		internal static string ERR_BadExceptionType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadExceptionType", null);
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06003007 RID: 12295 RVA: 0x000F34CA File Offset: 0x000F16CA
		internal static string ERR_BadEmptyThrow
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadEmptyThrow", null);
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06003008 RID: 12296 RVA: 0x000F34D7 File Offset: 0x000F16D7
		internal static string ERR_BadFinallyLeave
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadFinallyLeave", null);
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06003009 RID: 12297 RVA: 0x000F34E4 File Offset: 0x000F16E4
		internal static string ERR_LabelShadow
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LabelShadow", null);
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600300A RID: 12298 RVA: 0x000F34F1 File Offset: 0x000F16F1
		internal static string ERR_LabelNotFound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LabelNotFound", null);
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600300B RID: 12299 RVA: 0x000F34FE File Offset: 0x000F16FE
		internal static string ERR_UnreachableCatch
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnreachableCatch", null);
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600300C RID: 12300 RVA: 0x000F350B File Offset: 0x000F170B
		internal static string WRN_FilterIsConstantTrue
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_FilterIsConstantTrue", null);
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x0600300D RID: 12301 RVA: 0x000F3518 File Offset: 0x000F1718
		internal static string WRN_FilterIsConstantTrue_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_FilterIsConstantTrue_Title", null);
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600300E RID: 12302 RVA: 0x000F3525 File Offset: 0x000F1725
		internal static string ERR_ReturnExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ReturnExpected", null);
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x0600300F RID: 12303 RVA: 0x000F3532 File Offset: 0x000F1732
		internal static string WRN_UnreachableCode
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreachableCode", null);
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06003010 RID: 12304 RVA: 0x000F353F File Offset: 0x000F173F
		internal static string WRN_UnreachableCode_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreachableCode_Title", null);
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06003011 RID: 12305 RVA: 0x000F354C File Offset: 0x000F174C
		internal static string ERR_SwitchFallThrough
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SwitchFallThrough", null);
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06003012 RID: 12306 RVA: 0x000F3559 File Offset: 0x000F1759
		internal static string WRN_UnreferencedLabel
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreferencedLabel", null);
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06003013 RID: 12307 RVA: 0x000F3566 File Offset: 0x000F1766
		internal static string WRN_UnreferencedLabel_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreferencedLabel_Title", null);
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06003014 RID: 12308 RVA: 0x000F3573 File Offset: 0x000F1773
		internal static string ERR_UseDefViolation
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UseDefViolation", null);
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06003015 RID: 12309 RVA: 0x000F3580 File Offset: 0x000F1780
		internal static string WRN_UseDefViolation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UseDefViolation", null);
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06003016 RID: 12310 RVA: 0x000F358D File Offset: 0x000F178D
		internal static string WRN_UseDefViolation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UseDefViolation_Title", null);
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06003017 RID: 12311 RVA: 0x000F359A File Offset: 0x000F179A
		internal static string WRN_UnreferencedVar
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreferencedVar", null);
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06003018 RID: 12312 RVA: 0x000F35A7 File Offset: 0x000F17A7
		internal static string WRN_UnreferencedVar_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreferencedVar_Title", null);
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06003019 RID: 12313 RVA: 0x000F35B4 File Offset: 0x000F17B4
		internal static string WRN_UnreferencedField
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreferencedField", null);
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x0600301A RID: 12314 RVA: 0x000F35C1 File Offset: 0x000F17C1
		internal static string WRN_UnreferencedField_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreferencedField_Title", null);
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x0600301B RID: 12315 RVA: 0x000F35CE File Offset: 0x000F17CE
		internal static string ERR_UseDefViolationField
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UseDefViolationField", null);
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x0600301C RID: 12316 RVA: 0x000F35DB File Offset: 0x000F17DB
		internal static string WRN_UseDefViolationField
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UseDefViolationField", null);
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x0600301D RID: 12317 RVA: 0x000F35E8 File Offset: 0x000F17E8
		internal static string WRN_UseDefViolationField_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UseDefViolationField_Title", null);
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x0600301E RID: 12318 RVA: 0x000F35F5 File Offset: 0x000F17F5
		internal static string ERR_UseDefViolationProperty
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UseDefViolationProperty", null);
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x0600301F RID: 12319 RVA: 0x000F3602 File Offset: 0x000F1802
		internal static string WRN_UseDefViolationProperty
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UseDefViolationProperty", null);
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06003020 RID: 12320 RVA: 0x000F360F File Offset: 0x000F180F
		internal static string WRN_UseDefViolationProperty_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UseDefViolationProperty_Title", null);
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06003021 RID: 12321 RVA: 0x000F361C File Offset: 0x000F181C
		internal static string ERR_UnassignedThis
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnassignedThis", null);
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06003022 RID: 12322 RVA: 0x000F3629 File Offset: 0x000F1829
		internal static string WRN_UnassignedThis
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnassignedThis", null);
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06003023 RID: 12323 RVA: 0x000F3636 File Offset: 0x000F1836
		internal static string WRN_UnassignedThis_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnassignedThis_Title", null);
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06003024 RID: 12324 RVA: 0x000F3643 File Offset: 0x000F1843
		internal static string ERR_AmbigQM
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AmbigQM", null);
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06003025 RID: 12325 RVA: 0x000F3650 File Offset: 0x000F1850
		internal static string ERR_InvalidQM
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidQM", null);
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06003026 RID: 12326 RVA: 0x000F365D File Offset: 0x000F185D
		internal static string ERR_NoBaseClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoBaseClass", null);
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06003027 RID: 12327 RVA: 0x000F366A File Offset: 0x000F186A
		internal static string ERR_BaseIllegal
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BaseIllegal", null);
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06003028 RID: 12328 RVA: 0x000F3677 File Offset: 0x000F1877
		internal static string ERR_ObjectProhibited
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ObjectProhibited", null);
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06003029 RID: 12329 RVA: 0x000F3684 File Offset: 0x000F1884
		internal static string ERR_ParamUnassigned
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ParamUnassigned", null);
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x0600302A RID: 12330 RVA: 0x000F3691 File Offset: 0x000F1891
		internal static string WRN_ParamUnassigned
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ParamUnassigned", null);
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x0600302B RID: 12331 RVA: 0x000F369E File Offset: 0x000F189E
		internal static string WRN_ParamUnassigned_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ParamUnassigned_Title", null);
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x0600302C RID: 12332 RVA: 0x000F36AB File Offset: 0x000F18AB
		internal static string ERR_InvalidArray
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidArray", null);
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x0600302D RID: 12333 RVA: 0x000F36B8 File Offset: 0x000F18B8
		internal static string ERR_ExternHasBody
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExternHasBody", null);
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600302E RID: 12334 RVA: 0x000F36C5 File Offset: 0x000F18C5
		internal static string ERR_ExternHasConstructorInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExternHasConstructorInitializer", null);
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x0600302F RID: 12335 RVA: 0x000F36D2 File Offset: 0x000F18D2
		internal static string ERR_AbstractAndExtern
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AbstractAndExtern", null);
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06003030 RID: 12336 RVA: 0x000F36DF File Offset: 0x000F18DF
		internal static string ERR_BadAttributeParamType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAttributeParamType", null);
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06003031 RID: 12337 RVA: 0x000F36EC File Offset: 0x000F18EC
		internal static string ERR_BadAttributeArgument
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAttributeArgument", null);
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06003032 RID: 12338 RVA: 0x000F36F9 File Offset: 0x000F18F9
		internal static string ERR_BadAttributeParamDefaultArgument
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAttributeParamDefaultArgument", null);
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06003033 RID: 12339 RVA: 0x000F3706 File Offset: 0x000F1906
		internal static string WRN_IsAlwaysTrue
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IsAlwaysTrue", null);
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06003034 RID: 12340 RVA: 0x000F3713 File Offset: 0x000F1913
		internal static string WRN_IsAlwaysTrue_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IsAlwaysTrue_Title", null);
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06003035 RID: 12341 RVA: 0x000F3720 File Offset: 0x000F1920
		internal static string WRN_IsAlwaysFalse
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IsAlwaysFalse", null);
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06003036 RID: 12342 RVA: 0x000F372D File Offset: 0x000F192D
		internal static string WRN_IsAlwaysFalse_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IsAlwaysFalse_Title", null);
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06003037 RID: 12343 RVA: 0x000F373A File Offset: 0x000F193A
		internal static string ERR_LockNeedsReference
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LockNeedsReference", null);
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06003038 RID: 12344 RVA: 0x000F3747 File Offset: 0x000F1947
		internal static string ERR_NullNotValid
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NullNotValid", null);
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06003039 RID: 12345 RVA: 0x000F3754 File Offset: 0x000F1954
		internal static string ERR_DefaultLiteralNotValid
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DefaultLiteralNotValid", null);
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x0600303A RID: 12346 RVA: 0x000F3761 File Offset: 0x000F1961
		internal static string ERR_UseDefViolationThis
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UseDefViolationThis", null);
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x0600303B RID: 12347 RVA: 0x000F376E File Offset: 0x000F196E
		internal static string WRN_UseDefViolationThis
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UseDefViolationThis", null);
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x0600303C RID: 12348 RVA: 0x000F377B File Offset: 0x000F197B
		internal static string WRN_UseDefViolationThis_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UseDefViolationThis_Title", null);
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600303D RID: 12349 RVA: 0x000F3788 File Offset: 0x000F1988
		internal static string ERR_ArgsInvalid
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ArgsInvalid", null);
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600303E RID: 12350 RVA: 0x000F3795 File Offset: 0x000F1995
		internal static string ERR_PtrExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PtrExpected", null);
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x000F37A2 File Offset: 0x000F19A2
		internal static string ERR_PtrIndexSingle
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PtrIndexSingle", null);
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06003040 RID: 12352 RVA: 0x000F37AF File Offset: 0x000F19AF
		internal static string WRN_ByRefNonAgileField
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ByRefNonAgileField", null);
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06003041 RID: 12353 RVA: 0x000F37BC File Offset: 0x000F19BC
		internal static string WRN_ByRefNonAgileField_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ByRefNonAgileField_Title", null);
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06003042 RID: 12354 RVA: 0x000F37C9 File Offset: 0x000F19C9
		internal static string ERR_AssgReadonlyStatic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AssgReadonlyStatic", null);
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06003043 RID: 12355 RVA: 0x000F37D6 File Offset: 0x000F19D6
		internal static string ERR_RefReadonlyStatic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReadonlyStatic", null);
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06003044 RID: 12356 RVA: 0x000F37E3 File Offset: 0x000F19E3
		internal static string ERR_AssgReadonlyProp
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AssgReadonlyProp", null);
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06003045 RID: 12357 RVA: 0x000F37F0 File Offset: 0x000F19F0
		internal static string ERR_IllegalStatement
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IllegalStatement", null);
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06003046 RID: 12358 RVA: 0x000F37FD File Offset: 0x000F19FD
		internal static string ERR_BadGetEnumerator
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadGetEnumerator", null);
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06003047 RID: 12359 RVA: 0x000F380A File Offset: 0x000F1A0A
		internal static string ERR_BadGetAsyncEnumerator
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadGetAsyncEnumerator", null);
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06003048 RID: 12360 RVA: 0x000F3817 File Offset: 0x000F1A17
		internal static string ERR_TooManyLocals
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TooManyLocals", null);
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06003049 RID: 12361 RVA: 0x000F3824 File Offset: 0x000F1A24
		internal static string ERR_AbstractBaseCall
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AbstractBaseCall", null);
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x0600304A RID: 12362 RVA: 0x000F3831 File Offset: 0x000F1A31
		internal static string ERR_RefProperty
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefProperty", null);
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x0600304B RID: 12363 RVA: 0x000F383E File Offset: 0x000F1A3E
		internal static string ERR_ManagedAddr
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ManagedAddr", null);
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x0600304C RID: 12364 RVA: 0x000F384B File Offset: 0x000F1A4B
		internal static string ERR_BadFixedInitType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadFixedInitType", null);
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x0600304D RID: 12365 RVA: 0x000F3858 File Offset: 0x000F1A58
		internal static string ERR_FixedMustInit
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FixedMustInit", null);
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x0600304E RID: 12366 RVA: 0x000F3865 File Offset: 0x000F1A65
		internal static string ERR_InvalidAddrOp
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidAddrOp", null);
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x0600304F RID: 12367 RVA: 0x000F3872 File Offset: 0x000F1A72
		internal static string ERR_FixedNeeded
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FixedNeeded", null);
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x000F387F File Offset: 0x000F1A7F
		internal static string ERR_FixedNotNeeded
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FixedNotNeeded", null);
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06003051 RID: 12369 RVA: 0x000F388C File Offset: 0x000F1A8C
		internal static string ERR_ExprCannotBeFixed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExprCannotBeFixed", null);
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06003052 RID: 12370 RVA: 0x000F3899 File Offset: 0x000F1A99
		internal static string ERR_UnsafeNeeded
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnsafeNeeded", null);
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06003053 RID: 12371 RVA: 0x000F38A6 File Offset: 0x000F1AA6
		internal static string ERR_OpTFRetType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OpTFRetType", null);
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06003054 RID: 12372 RVA: 0x000F38B3 File Offset: 0x000F1AB3
		internal static string ERR_OperatorNeedsMatch
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OperatorNeedsMatch", null);
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06003055 RID: 12373 RVA: 0x000F38C0 File Offset: 0x000F1AC0
		internal static string ERR_BadBoolOp
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadBoolOp", null);
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06003056 RID: 12374 RVA: 0x000F38CD File Offset: 0x000F1ACD
		internal static string ERR_MustHaveOpTF
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MustHaveOpTF", null);
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06003057 RID: 12375 RVA: 0x000F38DA File Offset: 0x000F1ADA
		internal static string WRN_UnreferencedVarAssg
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreferencedVarAssg", null);
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06003058 RID: 12376 RVA: 0x000F38E7 File Offset: 0x000F1AE7
		internal static string WRN_UnreferencedVarAssg_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreferencedVarAssg_Title", null);
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06003059 RID: 12377 RVA: 0x000F38F4 File Offset: 0x000F1AF4
		internal static string ERR_CheckedOverflow
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CheckedOverflow", null);
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600305A RID: 12378 RVA: 0x000F3901 File Offset: 0x000F1B01
		internal static string ERR_ConstOutOfRangeChecked
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConstOutOfRangeChecked", null);
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x0600305B RID: 12379 RVA: 0x000F390E File Offset: 0x000F1B0E
		internal static string ERR_BadVarargs
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadVarargs", null);
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x0600305C RID: 12380 RVA: 0x000F391B File Offset: 0x000F1B1B
		internal static string ERR_ParamsMustBeArray
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ParamsMustBeArray", null);
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x0600305D RID: 12381 RVA: 0x000F3928 File Offset: 0x000F1B28
		internal static string ERR_IllegalArglist
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IllegalArglist", null);
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x0600305E RID: 12382 RVA: 0x000F3935 File Offset: 0x000F1B35
		internal static string ERR_IllegalUnsafe
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IllegalUnsafe", null);
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x000F3942 File Offset: 0x000F1B42
		internal static string ERR_AmbigMember
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AmbigMember", null);
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06003060 RID: 12384 RVA: 0x000F394F File Offset: 0x000F1B4F
		internal static string ERR_BadForeachDecl
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadForeachDecl", null);
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06003061 RID: 12385 RVA: 0x000F395C File Offset: 0x000F1B5C
		internal static string ERR_ParamsLast
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ParamsLast", null);
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06003062 RID: 12386 RVA: 0x000F3969 File Offset: 0x000F1B69
		internal static string ERR_SizeofUnsafe
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SizeofUnsafe", null);
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06003063 RID: 12387 RVA: 0x000F3976 File Offset: 0x000F1B76
		internal static string ERR_DottedTypeNameNotFoundInNS
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DottedTypeNameNotFoundInNS", null);
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06003064 RID: 12388 RVA: 0x000F3983 File Offset: 0x000F1B83
		internal static string ERR_FieldInitRefNonstatic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FieldInitRefNonstatic", null);
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06003065 RID: 12389 RVA: 0x000F3990 File Offset: 0x000F1B90
		internal static string ERR_SealedNonOverride
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SealedNonOverride", null);
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06003066 RID: 12390 RVA: 0x000F399D File Offset: 0x000F1B9D
		internal static string ERR_CantOverrideSealed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantOverrideSealed", null);
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06003067 RID: 12391 RVA: 0x000F39AA File Offset: 0x000F1BAA
		internal static string ERR_VoidError
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_VoidError", null);
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06003068 RID: 12392 RVA: 0x000F39B7 File Offset: 0x000F1BB7
		internal static string ERR_ConditionalOnOverride
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConditionalOnOverride", null);
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06003069 RID: 12393 RVA: 0x000F39C4 File Offset: 0x000F1BC4
		internal static string ERR_ConditionalOnLocalFunction
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConditionalOnLocalFunction", null);
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x0600306A RID: 12394 RVA: 0x000F39D1 File Offset: 0x000F1BD1
		internal static string ERR_PointerInAsOrIs
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PointerInAsOrIs", null);
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x0600306B RID: 12395 RVA: 0x000F39DE File Offset: 0x000F1BDE
		internal static string ERR_CallingFinalizeDeprecated
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CallingFinalizeDeprecated", null);
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x0600306C RID: 12396 RVA: 0x000F39EB File Offset: 0x000F1BEB
		internal static string ERR_SingleTypeNameNotFound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SingleTypeNameNotFound", null);
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x0600306D RID: 12397 RVA: 0x000F39F8 File Offset: 0x000F1BF8
		internal static string ERR_NegativeStackAllocSize
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NegativeStackAllocSize", null);
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x0600306E RID: 12398 RVA: 0x000F3A05 File Offset: 0x000F1C05
		internal static string ERR_NegativeArraySize
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NegativeArraySize", null);
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x0600306F RID: 12399 RVA: 0x000F3A12 File Offset: 0x000F1C12
		internal static string ERR_OverrideFinalizeDeprecated
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OverrideFinalizeDeprecated", null);
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06003070 RID: 12400 RVA: 0x000F3A1F File Offset: 0x000F1C1F
		internal static string ERR_CallingBaseFinalizeDeprecated
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CallingBaseFinalizeDeprecated", null);
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06003071 RID: 12401 RVA: 0x000F3A2C File Offset: 0x000F1C2C
		internal static string WRN_NegativeArrayIndex
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NegativeArrayIndex", null);
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06003072 RID: 12402 RVA: 0x000F3A39 File Offset: 0x000F1C39
		internal static string WRN_NegativeArrayIndex_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NegativeArrayIndex_Title", null);
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06003073 RID: 12403 RVA: 0x000F3A46 File Offset: 0x000F1C46
		internal static string WRN_BadRefCompareLeft
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadRefCompareLeft", null);
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06003074 RID: 12404 RVA: 0x000F3A53 File Offset: 0x000F1C53
		internal static string WRN_BadRefCompareLeft_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadRefCompareLeft_Title", null);
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06003075 RID: 12405 RVA: 0x000F3A60 File Offset: 0x000F1C60
		internal static string WRN_BadRefCompareRight
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadRefCompareRight", null);
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06003076 RID: 12406 RVA: 0x000F3A6D File Offset: 0x000F1C6D
		internal static string WRN_BadRefCompareRight_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadRefCompareRight_Title", null);
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06003077 RID: 12407 RVA: 0x000F3A7A File Offset: 0x000F1C7A
		internal static string ERR_BadCastInFixed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadCastInFixed", null);
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06003078 RID: 12408 RVA: 0x000F3A87 File Offset: 0x000F1C87
		internal static string ERR_StackallocInCatchFinally
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StackallocInCatchFinally", null);
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06003079 RID: 12409 RVA: 0x000F3A94 File Offset: 0x000F1C94
		internal static string ERR_VarargsLast
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_VarargsLast", null);
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x0600307A RID: 12410 RVA: 0x000F3AA1 File Offset: 0x000F1CA1
		internal static string ERR_MissingPartial
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MissingPartial", null);
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x0600307B RID: 12411 RVA: 0x000F3AAE File Offset: 0x000F1CAE
		internal static string ERR_PartialTypeKindConflict
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialTypeKindConflict", null);
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x0600307C RID: 12412 RVA: 0x000F3ABB File Offset: 0x000F1CBB
		internal static string ERR_PartialModifierConflict
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialModifierConflict", null);
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600307D RID: 12413 RVA: 0x000F3AC8 File Offset: 0x000F1CC8
		internal static string ERR_PartialMultipleBases
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMultipleBases", null);
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x000F3AD5 File Offset: 0x000F1CD5
		internal static string ERR_PartialWrongTypeParams
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialWrongTypeParams", null);
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x0600307F RID: 12415 RVA: 0x000F3AE2 File Offset: 0x000F1CE2
		internal static string ERR_PartialWrongConstraints
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialWrongConstraints", null);
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x000F3AEF File Offset: 0x000F1CEF
		internal static string ERR_NoImplicitConvCast
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoImplicitConvCast", null);
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06003081 RID: 12417 RVA: 0x000F3AFC File Offset: 0x000F1CFC
		internal static string ERR_PartialMisplaced
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMisplaced", null);
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x000F3B09 File Offset: 0x000F1D09
		internal static string ERR_ImportedCircularBase
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImportedCircularBase", null);
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06003083 RID: 12419 RVA: 0x000F3B16 File Offset: 0x000F1D16
		internal static string ERR_UseDefViolationOut
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UseDefViolationOut", null);
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x000F3B23 File Offset: 0x000F1D23
		internal static string WRN_UseDefViolationOut
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UseDefViolationOut", null);
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06003085 RID: 12421 RVA: 0x000F3B30 File Offset: 0x000F1D30
		internal static string WRN_UseDefViolationOut_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UseDefViolationOut_Title", null);
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06003086 RID: 12422 RVA: 0x000F3B3D File Offset: 0x000F1D3D
		internal static string ERR_ArraySizeInDeclaration
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ArraySizeInDeclaration", null);
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06003087 RID: 12423 RVA: 0x000F3B4A File Offset: 0x000F1D4A
		internal static string ERR_InaccessibleGetter
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InaccessibleGetter", null);
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06003088 RID: 12424 RVA: 0x000F3B57 File Offset: 0x000F1D57
		internal static string ERR_InaccessibleSetter
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InaccessibleSetter", null);
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06003089 RID: 12425 RVA: 0x000F3B64 File Offset: 0x000F1D64
		internal static string ERR_InvalidPropertyAccessMod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidPropertyAccessMod", null);
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x0600308A RID: 12426 RVA: 0x000F3B71 File Offset: 0x000F1D71
		internal static string ERR_DuplicatePropertyAccessMods
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicatePropertyAccessMods", null);
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x0600308B RID: 12427 RVA: 0x000F3B7E File Offset: 0x000F1D7E
		internal static string ERR_AccessModMissingAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AccessModMissingAccessor", null);
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x0600308C RID: 12428 RVA: 0x000F3B8B File Offset: 0x000F1D8B
		internal static string ERR_UnimplementedInterfaceAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnimplementedInterfaceAccessor", null);
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x000F3B98 File Offset: 0x000F1D98
		internal static string WRN_PatternIsAmbiguous
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_PatternIsAmbiguous", null);
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x0600308E RID: 12430 RVA: 0x000F3BA5 File Offset: 0x000F1DA5
		internal static string WRN_PatternIsAmbiguous_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_PatternIsAmbiguous_Title", null);
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x0600308F RID: 12431 RVA: 0x000F3BB2 File Offset: 0x000F1DB2
		internal static string WRN_PatternNotPublicOrNotInstance
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_PatternNotPublicOrNotInstance", null);
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06003090 RID: 12432 RVA: 0x000F3BBF File Offset: 0x000F1DBF
		internal static string WRN_PatternNotPublicOrNotInstance_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_PatternNotPublicOrNotInstance_Title", null);
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06003091 RID: 12433 RVA: 0x000F3BCC File Offset: 0x000F1DCC
		internal static string WRN_PatternBadSignature
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_PatternBadSignature", null);
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x000F3BD9 File Offset: 0x000F1DD9
		internal static string WRN_PatternBadSignature_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_PatternBadSignature_Title", null);
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x000F3BE6 File Offset: 0x000F1DE6
		internal static string ERR_FriendRefNotEqualToThis
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FriendRefNotEqualToThis", null);
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06003094 RID: 12436 RVA: 0x000F3BF3 File Offset: 0x000F1DF3
		internal static string ERR_FriendRefSigningMismatch
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FriendRefSigningMismatch", null);
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x000F3C00 File Offset: 0x000F1E00
		internal static string WRN_SequentialOnPartialClass
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SequentialOnPartialClass", null);
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x000F3C0D File Offset: 0x000F1E0D
		internal static string WRN_SequentialOnPartialClass_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SequentialOnPartialClass_Title", null);
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x000F3C1A File Offset: 0x000F1E1A
		internal static string ERR_BadConstType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadConstType", null);
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x000F3C27 File Offset: 0x000F1E27
		internal static string ERR_NoNewTyvar
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoNewTyvar", null);
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x000F3C34 File Offset: 0x000F1E34
		internal static string ERR_BadArity
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadArity", null);
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x000F3C41 File Offset: 0x000F1E41
		internal static string ERR_BadTypeArgument
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadTypeArgument", null);
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x0600309B RID: 12443 RVA: 0x000F3C4E File Offset: 0x000F1E4E
		internal static string ERR_TypeArgsNotAllowed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TypeArgsNotAllowed", null);
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x000F3C5B File Offset: 0x000F1E5B
		internal static string ERR_HasNoTypeVars
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_HasNoTypeVars", null);
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x000F3C68 File Offset: 0x000F1E68
		internal static string ERR_NewConstraintNotSatisfied
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NewConstraintNotSatisfied", null);
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x000F3C75 File Offset: 0x000F1E75
		internal static string ERR_GenericConstraintNotSatisfiedRefType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GenericConstraintNotSatisfiedRefType", null);
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x0600309F RID: 12447 RVA: 0x000F3C82 File Offset: 0x000F1E82
		internal static string ERR_GenericConstraintNotSatisfiedNullableEnum
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GenericConstraintNotSatisfiedNullableEnum", null);
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x000F3C8F File Offset: 0x000F1E8F
		internal static string ERR_GenericConstraintNotSatisfiedNullableInterface
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GenericConstraintNotSatisfiedNullableInterface", null);
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x000F3C9C File Offset: 0x000F1E9C
		internal static string ERR_GenericConstraintNotSatisfiedTyVar
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GenericConstraintNotSatisfiedTyVar", null);
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060030A2 RID: 12450 RVA: 0x000F3CA9 File Offset: 0x000F1EA9
		internal static string ERR_GenericConstraintNotSatisfiedValType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GenericConstraintNotSatisfiedValType", null);
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060030A3 RID: 12451 RVA: 0x000F3CB6 File Offset: 0x000F1EB6
		internal static string ERR_DuplicateGeneratedName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateGeneratedName", null);
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060030A4 RID: 12452 RVA: 0x000F3CC3 File Offset: 0x000F1EC3
		internal static string ERR_GlobalSingleTypeNameNotFound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GlobalSingleTypeNameNotFound", null);
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060030A5 RID: 12453 RVA: 0x000F3CD0 File Offset: 0x000F1ED0
		internal static string ERR_NewBoundMustBeLast
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NewBoundMustBeLast", null);
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060030A6 RID: 12454 RVA: 0x000F3CDD File Offset: 0x000F1EDD
		internal static string WRN_MainCantBeGeneric
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MainCantBeGeneric", null);
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060030A7 RID: 12455 RVA: 0x000F3CEA File Offset: 0x000F1EEA
		internal static string WRN_MainCantBeGeneric_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MainCantBeGeneric_Title", null);
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060030A8 RID: 12456 RVA: 0x000F3CF7 File Offset: 0x000F1EF7
		internal static string ERR_TypeVarCantBeNull
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TypeVarCantBeNull", null);
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x060030A9 RID: 12457 RVA: 0x000F3D04 File Offset: 0x000F1F04
		internal static string ERR_AttributeCantBeGeneric
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AttributeCantBeGeneric", null);
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060030AA RID: 12458 RVA: 0x000F3D11 File Offset: 0x000F1F11
		internal static string ERR_DuplicateBound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateBound", null);
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060030AB RID: 12459 RVA: 0x000F3D1E File Offset: 0x000F1F1E
		internal static string ERR_ClassBoundNotFirst
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ClassBoundNotFirst", null);
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060030AC RID: 12460 RVA: 0x000F3D2B File Offset: 0x000F1F2B
		internal static string ERR_BadRetType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadRetType", null);
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060030AD RID: 12461 RVA: 0x000F3D38 File Offset: 0x000F1F38
		internal static string ERR_DelegateRefMismatch
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DelegateRefMismatch", null);
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x000F3D45 File Offset: 0x000F1F45
		internal static string ERR_DuplicateConstraintClause
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateConstraintClause", null);
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060030AF RID: 12463 RVA: 0x000F3D52 File Offset: 0x000F1F52
		internal static string ERR_CantInferMethTypeArgs
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantInferMethTypeArgs", null);
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060030B0 RID: 12464 RVA: 0x000F3D5F File Offset: 0x000F1F5F
		internal static string ERR_LocalSameNameAsTypeParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LocalSameNameAsTypeParam", null);
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x060030B1 RID: 12465 RVA: 0x000F3D6C File Offset: 0x000F1F6C
		internal static string ERR_AsWithTypeVar
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AsWithTypeVar", null);
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x000F3D79 File Offset: 0x000F1F79
		internal static string WRN_UnreferencedFieldAssg
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreferencedFieldAssg", null);
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060030B3 RID: 12467 RVA: 0x000F3D86 File Offset: 0x000F1F86
		internal static string WRN_UnreferencedFieldAssg_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreferencedFieldAssg_Title", null);
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060030B4 RID: 12468 RVA: 0x000F3D93 File Offset: 0x000F1F93
		internal static string ERR_BadIndexerNameAttr
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadIndexerNameAttr", null);
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x000F3DA0 File Offset: 0x000F1FA0
		internal static string ERR_AttrArgWithTypeVars
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AttrArgWithTypeVars", null);
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060030B6 RID: 12470 RVA: 0x000F3DAD File Offset: 0x000F1FAD
		internal static string ERR_NewTyvarWithArgs
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NewTyvarWithArgs", null);
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060030B7 RID: 12471 RVA: 0x000F3DBA File Offset: 0x000F1FBA
		internal static string ERR_AbstractSealedStatic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AbstractSealedStatic", null);
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x000F3DC7 File Offset: 0x000F1FC7
		internal static string WRN_AmbiguousXMLReference
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AmbiguousXMLReference", null);
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x060030B9 RID: 12473 RVA: 0x000F3DD4 File Offset: 0x000F1FD4
		internal static string WRN_AmbiguousXMLReference_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AmbiguousXMLReference_Title", null);
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060030BA RID: 12474 RVA: 0x000F3DE1 File Offset: 0x000F1FE1
		internal static string WRN_VolatileByRef
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_VolatileByRef", null);
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060030BB RID: 12475 RVA: 0x000F3DEE File Offset: 0x000F1FEE
		internal static string WRN_VolatileByRef_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_VolatileByRef_Title", null);
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x000F3DFB File Offset: 0x000F1FFB
		internal static string WRN_VolatileByRef_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_VolatileByRef_Description", null);
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060030BD RID: 12477 RVA: 0x000F3E08 File Offset: 0x000F2008
		internal static string ERR_ComImportWithImpl
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ComImportWithImpl", null);
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060030BE RID: 12478 RVA: 0x000F3E15 File Offset: 0x000F2015
		internal static string ERR_ComImportWithBase
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ComImportWithBase", null);
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060030BF RID: 12479 RVA: 0x000F3E22 File Offset: 0x000F2022
		internal static string ERR_ImplBadConstraints
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImplBadConstraints", null);
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060030C0 RID: 12480 RVA: 0x000F3E2F File Offset: 0x000F202F
		internal static string ERR_ImplBadTupleNames
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImplBadTupleNames", null);
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060030C1 RID: 12481 RVA: 0x000F3E3C File Offset: 0x000F203C
		internal static string ERR_DottedTypeNameNotFoundInAgg
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DottedTypeNameNotFoundInAgg", null);
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060030C2 RID: 12482 RVA: 0x000F3E49 File Offset: 0x000F2049
		internal static string ERR_MethGrpToNonDel
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MethGrpToNonDel", null);
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060030C3 RID: 12483 RVA: 0x000F3E56 File Offset: 0x000F2056
		internal static string ERR_BadExternAlias
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadExternAlias", null);
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x000F3E63 File Offset: 0x000F2063
		internal static string ERR_ColColWithTypeAlias
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ColColWithTypeAlias", null);
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060030C5 RID: 12485 RVA: 0x000F3E70 File Offset: 0x000F2070
		internal static string ERR_AliasNotFound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AliasNotFound", null);
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060030C6 RID: 12486 RVA: 0x000F3E7D File Offset: 0x000F207D
		internal static string ERR_SameFullNameAggAgg
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SameFullNameAggAgg", null);
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060030C7 RID: 12487 RVA: 0x000F3E8A File Offset: 0x000F208A
		internal static string ERR_SameFullNameNsAgg
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SameFullNameNsAgg", null);
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060030C8 RID: 12488 RVA: 0x000F3E97 File Offset: 0x000F2097
		internal static string WRN_SameFullNameThisNsAgg
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SameFullNameThisNsAgg", null);
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060030C9 RID: 12489 RVA: 0x000F3EA4 File Offset: 0x000F20A4
		internal static string WRN_SameFullNameThisNsAgg_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SameFullNameThisNsAgg_Title", null);
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060030CA RID: 12490 RVA: 0x000F3EB1 File Offset: 0x000F20B1
		internal static string WRN_SameFullNameThisAggAgg
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SameFullNameThisAggAgg", null);
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060030CB RID: 12491 RVA: 0x000F3EBE File Offset: 0x000F20BE
		internal static string WRN_SameFullNameThisAggAgg_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SameFullNameThisAggAgg_Title", null);
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060030CC RID: 12492 RVA: 0x000F3ECB File Offset: 0x000F20CB
		internal static string WRN_SameFullNameThisAggNs
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SameFullNameThisAggNs", null);
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060030CD RID: 12493 RVA: 0x000F3ED8 File Offset: 0x000F20D8
		internal static string WRN_SameFullNameThisAggNs_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SameFullNameThisAggNs_Title", null);
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060030CE RID: 12494 RVA: 0x000F3EE5 File Offset: 0x000F20E5
		internal static string ERR_SameFullNameThisAggThisNs
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SameFullNameThisAggThisNs", null);
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060030CF RID: 12495 RVA: 0x000F3EF2 File Offset: 0x000F20F2
		internal static string ERR_ExternAfterElements
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExternAfterElements", null);
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060030D0 RID: 12496 RVA: 0x000F3EFF File Offset: 0x000F20FF
		internal static string WRN_GlobalAliasDefn
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_GlobalAliasDefn", null);
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060030D1 RID: 12497 RVA: 0x000F3F0C File Offset: 0x000F210C
		internal static string WRN_GlobalAliasDefn_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_GlobalAliasDefn_Title", null);
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060030D2 RID: 12498 RVA: 0x000F3F19 File Offset: 0x000F2119
		internal static string ERR_SealedStaticClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SealedStaticClass", null);
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060030D3 RID: 12499 RVA: 0x000F3F26 File Offset: 0x000F2126
		internal static string ERR_PrivateAbstractAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PrivateAbstractAccessor", null);
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060030D4 RID: 12500 RVA: 0x000F3F33 File Offset: 0x000F2133
		internal static string ERR_ValueExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ValueExpected", null);
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060030D5 RID: 12501 RVA: 0x000F3F40 File Offset: 0x000F2140
		internal static string ERR_UnboxNotLValue
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnboxNotLValue", null);
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060030D6 RID: 12502 RVA: 0x000F3F4D File Offset: 0x000F214D
		internal static string ERR_AnonMethGrpInForEach
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AnonMethGrpInForEach", null);
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060030D7 RID: 12503 RVA: 0x000F3F5A File Offset: 0x000F215A
		internal static string ERR_BadIncDecRetType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadIncDecRetType", null);
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060030D8 RID: 12504 RVA: 0x000F3F67 File Offset: 0x000F2167
		internal static string ERR_TypeConstraintsMustBeUniqueAndFirst
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TypeConstraintsMustBeUniqueAndFirst", null);
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060030D9 RID: 12505 RVA: 0x000F3F74 File Offset: 0x000F2174
		internal static string ERR_RefValBoundWithClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefValBoundWithClass", null);
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060030DA RID: 12506 RVA: 0x000F3F81 File Offset: 0x000F2181
		internal static string ERR_UnmanagedBoundWithClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnmanagedBoundWithClass", null);
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060030DB RID: 12507 RVA: 0x000F3F8E File Offset: 0x000F218E
		internal static string ERR_NewBoundWithVal
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NewBoundWithVal", null);
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060030DC RID: 12508 RVA: 0x000F3F9B File Offset: 0x000F219B
		internal static string ERR_RefConstraintNotSatisfied
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefConstraintNotSatisfied", null);
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060030DD RID: 12509 RVA: 0x000F3FA8 File Offset: 0x000F21A8
		internal static string ERR_ValConstraintNotSatisfied
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ValConstraintNotSatisfied", null);
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060030DE RID: 12510 RVA: 0x000F3FB5 File Offset: 0x000F21B5
		internal static string ERR_CircularConstraint
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CircularConstraint", null);
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060030DF RID: 12511 RVA: 0x000F3FC2 File Offset: 0x000F21C2
		internal static string ERR_BaseConstraintConflict
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BaseConstraintConflict", null);
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060030E0 RID: 12512 RVA: 0x000F3FCF File Offset: 0x000F21CF
		internal static string ERR_ConWithValCon
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConWithValCon", null);
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060030E1 RID: 12513 RVA: 0x000F3FDC File Offset: 0x000F21DC
		internal static string ERR_AmbigUDConv
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AmbigUDConv", null);
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060030E2 RID: 12514 RVA: 0x000F3FE9 File Offset: 0x000F21E9
		internal static string WRN_AlwaysNull
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AlwaysNull", null);
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060030E3 RID: 12515 RVA: 0x000F3FF6 File Offset: 0x000F21F6
		internal static string WRN_AlwaysNull_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AlwaysNull_Title", null);
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060030E4 RID: 12516 RVA: 0x000F4003 File Offset: 0x000F2203
		internal static string ERR_RefReturnThis
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnThis", null);
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060030E5 RID: 12517 RVA: 0x000F4010 File Offset: 0x000F2210
		internal static string ERR_AttributeCtorInParameter
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AttributeCtorInParameter", null);
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060030E6 RID: 12518 RVA: 0x000F401D File Offset: 0x000F221D
		internal static string ERR_OverrideWithConstraints
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OverrideWithConstraints", null);
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060030E7 RID: 12519 RVA: 0x000F402A File Offset: 0x000F222A
		internal static string ERR_AmbigOverride
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AmbigOverride", null);
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060030E8 RID: 12520 RVA: 0x000F4037 File Offset: 0x000F2237
		internal static string ERR_DecConstError
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DecConstError", null);
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060030E9 RID: 12521 RVA: 0x000F4044 File Offset: 0x000F2244
		internal static string WRN_CmpAlwaysFalse
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CmpAlwaysFalse", null);
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060030EA RID: 12522 RVA: 0x000F4051 File Offset: 0x000F2251
		internal static string WRN_CmpAlwaysFalse_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CmpAlwaysFalse_Title", null);
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060030EB RID: 12523 RVA: 0x000F405E File Offset: 0x000F225E
		internal static string WRN_FinalizeMethod
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_FinalizeMethod", null);
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060030EC RID: 12524 RVA: 0x000F406B File Offset: 0x000F226B
		internal static string WRN_FinalizeMethod_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_FinalizeMethod_Title", null);
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060030ED RID: 12525 RVA: 0x000F4078 File Offset: 0x000F2278
		internal static string WRN_FinalizeMethod_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_FinalizeMethod_Description", null);
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060030EE RID: 12526 RVA: 0x000F4085 File Offset: 0x000F2285
		internal static string ERR_ExplicitImplParams
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExplicitImplParams", null);
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x060030EF RID: 12527 RVA: 0x000F4092 File Offset: 0x000F2292
		internal static string WRN_GotoCaseShouldConvert
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_GotoCaseShouldConvert", null);
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060030F0 RID: 12528 RVA: 0x000F409F File Offset: 0x000F229F
		internal static string WRN_GotoCaseShouldConvert_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_GotoCaseShouldConvert_Title", null);
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060030F1 RID: 12529 RVA: 0x000F40AC File Offset: 0x000F22AC
		internal static string ERR_MethodImplementingAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MethodImplementingAccessor", null);
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x000F40B9 File Offset: 0x000F22B9
		internal static string WRN_NubExprIsConstBool
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NubExprIsConstBool", null);
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060030F3 RID: 12531 RVA: 0x000F40C6 File Offset: 0x000F22C6
		internal static string WRN_NubExprIsConstBool_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NubExprIsConstBool_Title", null);
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060030F4 RID: 12532 RVA: 0x000F40D3 File Offset: 0x000F22D3
		internal static string WRN_NubExprIsConstBool2
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NubExprIsConstBool2", null);
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060030F5 RID: 12533 RVA: 0x000F40E0 File Offset: 0x000F22E0
		internal static string WRN_NubExprIsConstBool2_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NubExprIsConstBool2_Title", null);
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060030F6 RID: 12534 RVA: 0x000F40ED File Offset: 0x000F22ED
		internal static string WRN_ExplicitImplCollision
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ExplicitImplCollision", null);
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060030F7 RID: 12535 RVA: 0x000F40FA File Offset: 0x000F22FA
		internal static string WRN_ExplicitImplCollision_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ExplicitImplCollision_Title", null);
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060030F8 RID: 12536 RVA: 0x000F4107 File Offset: 0x000F2307
		internal static string ERR_AbstractHasBody
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AbstractHasBody", null);
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060030F9 RID: 12537 RVA: 0x000F4114 File Offset: 0x000F2314
		internal static string ERR_ConcreteMissingBody
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConcreteMissingBody", null);
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x060030FA RID: 12538 RVA: 0x000F4121 File Offset: 0x000F2321
		internal static string ERR_AbstractAndSealed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AbstractAndSealed", null);
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x060030FB RID: 12539 RVA: 0x000F412E File Offset: 0x000F232E
		internal static string ERR_AbstractNotVirtual
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AbstractNotVirtual", null);
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x000F413B File Offset: 0x000F233B
		internal static string ERR_StaticConstant
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StaticConstant", null);
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x060030FD RID: 12541 RVA: 0x000F4148 File Offset: 0x000F2348
		internal static string ERR_CantOverrideNonFunction
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantOverrideNonFunction", null);
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x000F4155 File Offset: 0x000F2355
		internal static string ERR_CantOverrideNonVirtual
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantOverrideNonVirtual", null);
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x060030FF RID: 12543 RVA: 0x000F4162 File Offset: 0x000F2362
		internal static string ERR_CantChangeAccessOnOverride
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantChangeAccessOnOverride", null);
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x000F416F File Offset: 0x000F236F
		internal static string ERR_CantChangeTupleNamesOnOverride
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantChangeTupleNamesOnOverride", null);
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06003101 RID: 12545 RVA: 0x000F417C File Offset: 0x000F237C
		internal static string ERR_CantChangeReturnTypeOnOverride
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantChangeReturnTypeOnOverride", null);
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x000F4189 File Offset: 0x000F2389
		internal static string ERR_CantDeriveFromSealedType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantDeriveFromSealedType", null);
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06003103 RID: 12547 RVA: 0x000F4196 File Offset: 0x000F2396
		internal static string ERR_AbstractInConcreteClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AbstractInConcreteClass", null);
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x000F41A3 File Offset: 0x000F23A3
		internal static string ERR_StaticConstructorWithExplicitConstructorCall
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StaticConstructorWithExplicitConstructorCall", null);
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06003105 RID: 12549 RVA: 0x000F41B0 File Offset: 0x000F23B0
		internal static string ERR_StaticConstructorWithAccessModifiers
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StaticConstructorWithAccessModifiers", null);
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x000F41BD File Offset: 0x000F23BD
		internal static string ERR_RecursiveConstructorCall
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RecursiveConstructorCall", null);
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06003107 RID: 12551 RVA: 0x000F41CA File Offset: 0x000F23CA
		internal static string ERR_IndirectRecursiveConstructorCall
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IndirectRecursiveConstructorCall", null);
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x000F41D7 File Offset: 0x000F23D7
		internal static string ERR_ObjectCallingBaseConstructor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ObjectCallingBaseConstructor", null);
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06003109 RID: 12553 RVA: 0x000F41E4 File Offset: 0x000F23E4
		internal static string ERR_PredefinedTypeNotFound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PredefinedTypeNotFound", null);
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x000F41F1 File Offset: 0x000F23F1
		internal static string ERR_PredefinedValueTupleTypeNotFound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PredefinedValueTupleTypeNotFound", null);
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x000F41FE File Offset: 0x000F23FE
		internal static string ERR_PredefinedValueTupleTypeAmbiguous3
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PredefinedValueTupleTypeAmbiguous3", null);
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x000F420B File Offset: 0x000F240B
		internal static string ERR_StructWithBaseConstructorCall
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StructWithBaseConstructorCall", null);
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x0600310D RID: 12557 RVA: 0x000F4218 File Offset: 0x000F2418
		internal static string ERR_StructLayoutCycle
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StructLayoutCycle", null);
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x0600310E RID: 12558 RVA: 0x000F4225 File Offset: 0x000F2425
		internal static string ERR_InterfacesCantContainFields
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InterfacesCantContainFields", null);
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x000F4232 File Offset: 0x000F2432
		internal static string ERR_InterfacesCantContainConstructors
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InterfacesCantContainConstructors", null);
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06003110 RID: 12560 RVA: 0x000F423F File Offset: 0x000F243F
		internal static string ERR_NonInterfaceInInterfaceList
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NonInterfaceInInterfaceList", null);
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06003111 RID: 12561 RVA: 0x000F424C File Offset: 0x000F244C
		internal static string ERR_DuplicateInterfaceInBaseList
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateInterfaceInBaseList", null);
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x000F4259 File Offset: 0x000F2459
		internal static string ERR_DuplicateInterfaceWithTupleNamesInBaseList
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateInterfaceWithTupleNamesInBaseList", null);
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06003113 RID: 12563 RVA: 0x000F4266 File Offset: 0x000F2466
		internal static string ERR_DuplicateInterfaceWithDifferencesInBaseList
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateInterfaceWithDifferencesInBaseList", null);
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06003114 RID: 12564 RVA: 0x000F4273 File Offset: 0x000F2473
		internal static string ERR_CycleInInterfaceInheritance
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CycleInInterfaceInheritance", null);
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06003115 RID: 12565 RVA: 0x000F4280 File Offset: 0x000F2480
		internal static string ERR_HidingAbstractMethod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_HidingAbstractMethod", null);
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x000F428D File Offset: 0x000F248D
		internal static string ERR_UnimplementedAbstractMethod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnimplementedAbstractMethod", null);
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06003117 RID: 12567 RVA: 0x000F429A File Offset: 0x000F249A
		internal static string ERR_UnimplementedInterfaceMember
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnimplementedInterfaceMember", null);
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06003118 RID: 12568 RVA: 0x000F42A7 File Offset: 0x000F24A7
		internal static string ERR_ObjectCantHaveBases
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ObjectCantHaveBases", null);
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x000F42B4 File Offset: 0x000F24B4
		internal static string ERR_ExplicitInterfaceImplementationNotInterface
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExplicitInterfaceImplementationNotInterface", null);
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x000F42C1 File Offset: 0x000F24C1
		internal static string ERR_InterfaceMemberNotFound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InterfaceMemberNotFound", null);
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x000F42CE File Offset: 0x000F24CE
		internal static string ERR_ClassDoesntImplementInterface
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ClassDoesntImplementInterface", null);
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x0600311C RID: 12572 RVA: 0x000F42DB File Offset: 0x000F24DB
		internal static string ERR_ExplicitInterfaceImplementationInNonClassOrStruct
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExplicitInterfaceImplementationInNonClassOrStruct", null);
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x0600311D RID: 12573 RVA: 0x000F42E8 File Offset: 0x000F24E8
		internal static string ERR_MemberNameSameAsType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MemberNameSameAsType", null);
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x0600311E RID: 12574 RVA: 0x000F42F5 File Offset: 0x000F24F5
		internal static string ERR_EnumeratorOverflow
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EnumeratorOverflow", null);
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x0600311F RID: 12575 RVA: 0x000F4302 File Offset: 0x000F2502
		internal static string ERR_CantOverrideNonProperty
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantOverrideNonProperty", null);
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06003120 RID: 12576 RVA: 0x000F430F File Offset: 0x000F250F
		internal static string ERR_NoGetToOverride
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoGetToOverride", null);
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06003121 RID: 12577 RVA: 0x000F431C File Offset: 0x000F251C
		internal static string ERR_NoSetToOverride
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoSetToOverride", null);
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06003122 RID: 12578 RVA: 0x000F4329 File Offset: 0x000F2529
		internal static string ERR_PropertyCantHaveVoidType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PropertyCantHaveVoidType", null);
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06003123 RID: 12579 RVA: 0x000F4336 File Offset: 0x000F2536
		internal static string ERR_PropertyWithNoAccessors
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PropertyWithNoAccessors", null);
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06003124 RID: 12580 RVA: 0x000F4343 File Offset: 0x000F2543
		internal static string ERR_CantUseVoidInArglist
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantUseVoidInArglist", null);
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06003125 RID: 12581 RVA: 0x000F4350 File Offset: 0x000F2550
		internal static string ERR_NewVirtualInSealed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NewVirtualInSealed", null);
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06003126 RID: 12582 RVA: 0x000F435D File Offset: 0x000F255D
		internal static string ERR_ExplicitPropertyAddingAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExplicitPropertyAddingAccessor", null);
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06003127 RID: 12583 RVA: 0x000F436A File Offset: 0x000F256A
		internal static string ERR_ExplicitPropertyMismatchInitOnly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExplicitPropertyMismatchInitOnly", null);
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06003128 RID: 12584 RVA: 0x000F4377 File Offset: 0x000F2577
		internal static string ERR_ExplicitPropertyMissingAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExplicitPropertyMissingAccessor", null);
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06003129 RID: 12585 RVA: 0x000F4384 File Offset: 0x000F2584
		internal static string ERR_ConversionWithInterface
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConversionWithInterface", null);
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x0600312A RID: 12586 RVA: 0x000F4391 File Offset: 0x000F2591
		internal static string ERR_ConversionWithBase
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConversionWithBase", null);
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x0600312B RID: 12587 RVA: 0x000F439E File Offset: 0x000F259E
		internal static string ERR_ConversionWithDerived
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConversionWithDerived", null);
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x000F43AB File Offset: 0x000F25AB
		internal static string ERR_IdentityConversion
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IdentityConversion", null);
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x0600312D RID: 12589 RVA: 0x000F43B8 File Offset: 0x000F25B8
		internal static string ERR_ConversionNotInvolvingContainedType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConversionNotInvolvingContainedType", null);
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x000F43C5 File Offset: 0x000F25C5
		internal static string ERR_DuplicateConversionInClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateConversionInClass", null);
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x0600312F RID: 12591 RVA: 0x000F43D2 File Offset: 0x000F25D2
		internal static string ERR_OperatorsMustBeStatic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OperatorsMustBeStatic", null);
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06003130 RID: 12592 RVA: 0x000F43DF File Offset: 0x000F25DF
		internal static string ERR_BadIncDecSignature
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadIncDecSignature", null);
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06003131 RID: 12593 RVA: 0x000F43EC File Offset: 0x000F25EC
		internal static string ERR_BadUnaryOperatorSignature
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadUnaryOperatorSignature", null);
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06003132 RID: 12594 RVA: 0x000F43F9 File Offset: 0x000F25F9
		internal static string ERR_BadBinaryOperatorSignature
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadBinaryOperatorSignature", null);
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06003133 RID: 12595 RVA: 0x000F4406 File Offset: 0x000F2606
		internal static string ERR_BadShiftOperatorSignature
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadShiftOperatorSignature", null);
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06003134 RID: 12596 RVA: 0x000F4413 File Offset: 0x000F2613
		internal static string ERR_InterfacesCantContainConversionOrEqualityOperators
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InterfacesCantContainConversionOrEqualityOperators", null);
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06003135 RID: 12597 RVA: 0x000F4420 File Offset: 0x000F2620
		internal static string ERR_StructsCantContainDefaultConstructor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StructsCantContainDefaultConstructor", null);
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06003136 RID: 12598 RVA: 0x000F442D File Offset: 0x000F262D
		internal static string ERR_EnumsCantContainDefaultConstructor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EnumsCantContainDefaultConstructor", null);
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06003137 RID: 12599 RVA: 0x000F443A File Offset: 0x000F263A
		internal static string ERR_CantOverrideBogusMethod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantOverrideBogusMethod", null);
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06003138 RID: 12600 RVA: 0x000F4447 File Offset: 0x000F2647
		internal static string ERR_BindToBogus
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BindToBogus", null);
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06003139 RID: 12601 RVA: 0x000F4454 File Offset: 0x000F2654
		internal static string ERR_CantCallSpecialMethod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantCallSpecialMethod", null);
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x0600313A RID: 12602 RVA: 0x000F4461 File Offset: 0x000F2661
		internal static string ERR_BadTypeReference
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadTypeReference", null);
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x0600313B RID: 12603 RVA: 0x000F446E File Offset: 0x000F266E
		internal static string ERR_FieldInitializerInStruct
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FieldInitializerInStruct", null);
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x0600313C RID: 12604 RVA: 0x000F447B File Offset: 0x000F267B
		internal static string ERR_BadDestructorName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadDestructorName", null);
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x0600313D RID: 12605 RVA: 0x000F4488 File Offset: 0x000F2688
		internal static string ERR_OnlyClassesCanContainDestructors
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OnlyClassesCanContainDestructors", null);
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x0600313E RID: 12606 RVA: 0x000F4495 File Offset: 0x000F2695
		internal static string ERR_ConflictAliasAndMember
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConflictAliasAndMember", null);
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x0600313F RID: 12607 RVA: 0x000F44A2 File Offset: 0x000F26A2
		internal static string ERR_ConflictingAliasAndDefinition
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConflictingAliasAndDefinition", null);
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06003140 RID: 12608 RVA: 0x000F44AF File Offset: 0x000F26AF
		internal static string ERR_ConditionalOnSpecialMethod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConditionalOnSpecialMethod", null);
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06003141 RID: 12609 RVA: 0x000F44BC File Offset: 0x000F26BC
		internal static string ERR_ConditionalMustReturnVoid
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConditionalMustReturnVoid", null);
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06003142 RID: 12610 RVA: 0x000F44C9 File Offset: 0x000F26C9
		internal static string ERR_DuplicateAttribute
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateAttribute", null);
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06003143 RID: 12611 RVA: 0x000F44D6 File Offset: 0x000F26D6
		internal static string ERR_DuplicateAttributeInNetModule
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateAttributeInNetModule", null);
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06003144 RID: 12612 RVA: 0x000F44E3 File Offset: 0x000F26E3
		internal static string ERR_ConditionalOnInterfaceMethod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConditionalOnInterfaceMethod", null);
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06003145 RID: 12613 RVA: 0x000F44F0 File Offset: 0x000F26F0
		internal static string ERR_OperatorCantReturnVoid
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OperatorCantReturnVoid", null);
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06003146 RID: 12614 RVA: 0x000F44FD File Offset: 0x000F26FD
		internal static string ERR_BadDynamicConversion
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadDynamicConversion", null);
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06003147 RID: 12615 RVA: 0x000F450A File Offset: 0x000F270A
		internal static string ERR_InvalidAttributeArgument
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidAttributeArgument", null);
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06003148 RID: 12616 RVA: 0x000F4517 File Offset: 0x000F2717
		internal static string ERR_ParameterNotValidForType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ParameterNotValidForType", null);
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06003149 RID: 12617 RVA: 0x000F4524 File Offset: 0x000F2724
		internal static string ERR_AttributeParameterRequired1
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AttributeParameterRequired1", null);
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x0600314A RID: 12618 RVA: 0x000F4531 File Offset: 0x000F2731
		internal static string ERR_AttributeParameterRequired2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AttributeParameterRequired2", null);
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600314B RID: 12619 RVA: 0x000F453E File Offset: 0x000F273E
		internal static string ERR_MarshalUnmanagedTypeNotValidForFields
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MarshalUnmanagedTypeNotValidForFields", null);
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x0600314C RID: 12620 RVA: 0x000F454B File Offset: 0x000F274B
		internal static string ERR_MarshalUnmanagedTypeOnlyValidForFields
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MarshalUnmanagedTypeOnlyValidForFields", null);
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x0600314D RID: 12621 RVA: 0x000F4558 File Offset: 0x000F2758
		internal static string ERR_AttributeOnBadSymbolType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AttributeOnBadSymbolType", null);
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x0600314E RID: 12622 RVA: 0x000F4565 File Offset: 0x000F2765
		internal static string ERR_FloatOverflow
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FloatOverflow", null);
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x0600314F RID: 12623 RVA: 0x000F4572 File Offset: 0x000F2772
		internal static string ERR_ComImportWithoutUuidAttribute
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ComImportWithoutUuidAttribute", null);
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x000F457F File Offset: 0x000F277F
		internal static string ERR_InvalidNamedArgument
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidNamedArgument", null);
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x000F458C File Offset: 0x000F278C
		internal static string ERR_DllImportOnInvalidMethod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DllImportOnInvalidMethod", null);
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06003152 RID: 12626 RVA: 0x000F4599 File Offset: 0x000F2799
		internal static string ERR_EncUpdateFailedMissingAttribute
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EncUpdateFailedMissingAttribute", null);
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06003153 RID: 12627 RVA: 0x000F45A6 File Offset: 0x000F27A6
		internal static string ERR_DllImportOnGenericMethod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DllImportOnGenericMethod", null);
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06003154 RID: 12628 RVA: 0x000F45B3 File Offset: 0x000F27B3
		internal static string ERR_FieldCantBeRefAny
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FieldCantBeRefAny", null);
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06003155 RID: 12629 RVA: 0x000F45C0 File Offset: 0x000F27C0
		internal static string ERR_FieldAutoPropCantBeByRefLike
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FieldAutoPropCantBeByRefLike", null);
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06003156 RID: 12630 RVA: 0x000F45CD File Offset: 0x000F27CD
		internal static string ERR_ArrayElementCantBeRefAny
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ArrayElementCantBeRefAny", null);
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06003157 RID: 12631 RVA: 0x000F45DA File Offset: 0x000F27DA
		internal static string WRN_DeprecatedSymbol
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DeprecatedSymbol", null);
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x000F45E7 File Offset: 0x000F27E7
		internal static string WRN_DeprecatedSymbol_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DeprecatedSymbol_Title", null);
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06003159 RID: 12633 RVA: 0x000F45F4 File Offset: 0x000F27F4
		internal static string ERR_NotAnAttributeClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NotAnAttributeClass", null);
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x0600315A RID: 12634 RVA: 0x000F4601 File Offset: 0x000F2801
		internal static string ERR_BadNamedAttributeArgument
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadNamedAttributeArgument", null);
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x000F460E File Offset: 0x000F280E
		internal static string WRN_DeprecatedSymbolStr
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DeprecatedSymbolStr", null);
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x000F461B File Offset: 0x000F281B
		internal static string WRN_DeprecatedSymbolStr_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DeprecatedSymbolStr_Title", null);
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x000F4628 File Offset: 0x000F2828
		internal static string ERR_DeprecatedSymbolStr
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DeprecatedSymbolStr", null);
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x000F4635 File Offset: 0x000F2835
		internal static string ERR_IndexerCantHaveVoidType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IndexerCantHaveVoidType", null);
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x000F4642 File Offset: 0x000F2842
		internal static string ERR_VirtualPrivate
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_VirtualPrivate", null);
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x000F464F File Offset: 0x000F284F
		internal static string ERR_ArrayInitToNonArrayType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ArrayInitToNonArrayType", null);
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x000F465C File Offset: 0x000F285C
		internal static string ERR_ArrayInitInBadPlace
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ArrayInitInBadPlace", null);
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06003162 RID: 12642 RVA: 0x000F4669 File Offset: 0x000F2869
		internal static string ERR_MissingStructOffset
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MissingStructOffset", null);
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06003163 RID: 12643 RVA: 0x000F4676 File Offset: 0x000F2876
		internal static string WRN_ExternMethodNoImplementation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ExternMethodNoImplementation", null);
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06003164 RID: 12644 RVA: 0x000F4683 File Offset: 0x000F2883
		internal static string WRN_ExternMethodNoImplementation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ExternMethodNoImplementation_Title", null);
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06003165 RID: 12645 RVA: 0x000F4690 File Offset: 0x000F2890
		internal static string WRN_ProtectedInSealed
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ProtectedInSealed", null);
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06003166 RID: 12646 RVA: 0x000F469D File Offset: 0x000F289D
		internal static string WRN_ProtectedInSealed_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ProtectedInSealed_Title", null);
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x000F46AA File Offset: 0x000F28AA
		internal static string ERR_InterfaceImplementedByConditional
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InterfaceImplementedByConditional", null);
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06003168 RID: 12648 RVA: 0x000F46B7 File Offset: 0x000F28B7
		internal static string ERR_InterfaceImplementedImplicitlyByVariadic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InterfaceImplementedImplicitlyByVariadic", null);
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06003169 RID: 12649 RVA: 0x000F46C4 File Offset: 0x000F28C4
		internal static string ERR_IllegalRefParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IllegalRefParam", null);
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x000F46D1 File Offset: 0x000F28D1
		internal static string ERR_BadArgumentToAttribute
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadArgumentToAttribute", null);
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x0600316B RID: 12651 RVA: 0x000F46DE File Offset: 0x000F28DE
		internal static string ERR_StructOffsetOnBadStruct
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StructOffsetOnBadStruct", null);
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x0600316C RID: 12652 RVA: 0x000F46EB File Offset: 0x000F28EB
		internal static string ERR_StructOffsetOnBadField
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StructOffsetOnBadField", null);
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x0600316D RID: 12653 RVA: 0x000F46F8 File Offset: 0x000F28F8
		internal static string ERR_AttributeUsageOnNonAttributeClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AttributeUsageOnNonAttributeClass", null);
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x0600316E RID: 12654 RVA: 0x000F4705 File Offset: 0x000F2905
		internal static string WRN_PossibleMistakenNullStatement
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_PossibleMistakenNullStatement", null);
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x0600316F RID: 12655 RVA: 0x000F4712 File Offset: 0x000F2912
		internal static string WRN_PossibleMistakenNullStatement_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_PossibleMistakenNullStatement_Title", null);
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06003170 RID: 12656 RVA: 0x000F471F File Offset: 0x000F291F
		internal static string ERR_DuplicateNamedAttributeArgument
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateNamedAttributeArgument", null);
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06003171 RID: 12657 RVA: 0x000F472C File Offset: 0x000F292C
		internal static string ERR_DeriveFromEnumOrValueType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DeriveFromEnumOrValueType", null);
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06003172 RID: 12658 RVA: 0x000F4739 File Offset: 0x000F2939
		internal static string ERR_DefaultMemberOnIndexedType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DefaultMemberOnIndexedType", null);
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06003173 RID: 12659 RVA: 0x000F4746 File Offset: 0x000F2946
		internal static string ERR_BogusType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BogusType", null);
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06003174 RID: 12660 RVA: 0x000F4753 File Offset: 0x000F2953
		internal static string WRN_UnassignedInternalField
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnassignedInternalField", null);
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06003175 RID: 12661 RVA: 0x000F4760 File Offset: 0x000F2960
		internal static string WRN_UnassignedInternalField_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnassignedInternalField_Title", null);
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06003176 RID: 12662 RVA: 0x000F476D File Offset: 0x000F296D
		internal static string ERR_CStyleArray
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CStyleArray", null);
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06003177 RID: 12663 RVA: 0x000F477A File Offset: 0x000F297A
		internal static string WRN_VacuousIntegralComp
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_VacuousIntegralComp", null);
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06003178 RID: 12664 RVA: 0x000F4787 File Offset: 0x000F2987
		internal static string WRN_VacuousIntegralComp_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_VacuousIntegralComp_Title", null);
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06003179 RID: 12665 RVA: 0x000F4794 File Offset: 0x000F2994
		internal static string ERR_AbstractAttributeClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AbstractAttributeClass", null);
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x0600317A RID: 12666 RVA: 0x000F47A1 File Offset: 0x000F29A1
		internal static string ERR_BadNamedAttributeArgumentType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadNamedAttributeArgumentType", null);
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x0600317B RID: 12667 RVA: 0x000F47AE File Offset: 0x000F29AE
		internal static string ERR_MissingPredefinedMember
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MissingPredefinedMember", null);
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x0600317C RID: 12668 RVA: 0x000F47BB File Offset: 0x000F29BB
		internal static string WRN_AttributeLocationOnBadDeclaration
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AttributeLocationOnBadDeclaration", null);
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x0600317D RID: 12669 RVA: 0x000F47C8 File Offset: 0x000F29C8
		internal static string WRN_AttributeLocationOnBadDeclaration_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AttributeLocationOnBadDeclaration_Title", null);
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x0600317E RID: 12670 RVA: 0x000F47D5 File Offset: 0x000F29D5
		internal static string WRN_InvalidAttributeLocation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_InvalidAttributeLocation", null);
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x000F47E2 File Offset: 0x000F29E2
		internal static string WRN_InvalidAttributeLocation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_InvalidAttributeLocation_Title", null);
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06003180 RID: 12672 RVA: 0x000F47EF File Offset: 0x000F29EF
		internal static string WRN_EqualsWithoutGetHashCode
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_EqualsWithoutGetHashCode", null);
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06003181 RID: 12673 RVA: 0x000F47FC File Offset: 0x000F29FC
		internal static string WRN_EqualsWithoutGetHashCode_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_EqualsWithoutGetHashCode_Title", null);
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06003182 RID: 12674 RVA: 0x000F4809 File Offset: 0x000F2A09
		internal static string WRN_EqualityOpWithoutEquals
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_EqualityOpWithoutEquals", null);
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06003183 RID: 12675 RVA: 0x000F4816 File Offset: 0x000F2A16
		internal static string WRN_EqualityOpWithoutEquals_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_EqualityOpWithoutEquals_Title", null);
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06003184 RID: 12676 RVA: 0x000F4823 File Offset: 0x000F2A23
		internal static string WRN_EqualityOpWithoutGetHashCode
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_EqualityOpWithoutGetHashCode", null);
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06003185 RID: 12677 RVA: 0x000F4830 File Offset: 0x000F2A30
		internal static string WRN_EqualityOpWithoutGetHashCode_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_EqualityOpWithoutGetHashCode_Title", null);
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06003186 RID: 12678 RVA: 0x000F483D File Offset: 0x000F2A3D
		internal static string ERR_OutAttrOnRefParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OutAttrOnRefParam", null);
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06003187 RID: 12679 RVA: 0x000F484A File Offset: 0x000F2A4A
		internal static string ERR_OverloadRefKind
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OverloadRefKind", null);
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06003188 RID: 12680 RVA: 0x000F4857 File Offset: 0x000F2A57
		internal static string ERR_LiteralDoubleCast
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LiteralDoubleCast", null);
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06003189 RID: 12681 RVA: 0x000F4864 File Offset: 0x000F2A64
		internal static string WRN_IncorrectBooleanAssg
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IncorrectBooleanAssg", null);
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x0600318A RID: 12682 RVA: 0x000F4871 File Offset: 0x000F2A71
		internal static string WRN_IncorrectBooleanAssg_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IncorrectBooleanAssg_Title", null);
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x0600318B RID: 12683 RVA: 0x000F487E File Offset: 0x000F2A7E
		internal static string ERR_ProtectedInStruct
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ProtectedInStruct", null);
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x0600318C RID: 12684 RVA: 0x000F488B File Offset: 0x000F2A8B
		internal static string ERR_InconsistentIndexerNames
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InconsistentIndexerNames", null);
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x0600318D RID: 12685 RVA: 0x000F4898 File Offset: 0x000F2A98
		internal static string ERR_ComImportWithUserCtor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ComImportWithUserCtor", null);
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x0600318E RID: 12686 RVA: 0x000F48A5 File Offset: 0x000F2AA5
		internal static string ERR_FieldCantHaveVoidType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FieldCantHaveVoidType", null);
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x0600318F RID: 12687 RVA: 0x000F48B2 File Offset: 0x000F2AB2
		internal static string WRN_NonObsoleteOverridingObsolete
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NonObsoleteOverridingObsolete", null);
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06003190 RID: 12688 RVA: 0x000F48BF File Offset: 0x000F2ABF
		internal static string WRN_NonObsoleteOverridingObsolete_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NonObsoleteOverridingObsolete_Title", null);
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06003191 RID: 12689 RVA: 0x000F48CC File Offset: 0x000F2ACC
		internal static string ERR_SystemVoid
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SystemVoid", null);
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06003192 RID: 12690 RVA: 0x000F48D9 File Offset: 0x000F2AD9
		internal static string ERR_ExplicitParamArray
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExplicitParamArray", null);
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06003193 RID: 12691 RVA: 0x000F48E6 File Offset: 0x000F2AE6
		internal static string WRN_BitwiseOrSignExtend
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BitwiseOrSignExtend", null);
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06003194 RID: 12692 RVA: 0x000F48F3 File Offset: 0x000F2AF3
		internal static string WRN_BitwiseOrSignExtend_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BitwiseOrSignExtend_Title", null);
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06003195 RID: 12693 RVA: 0x000F4900 File Offset: 0x000F2B00
		internal static string WRN_BitwiseOrSignExtend_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BitwiseOrSignExtend_Description", null);
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06003196 RID: 12694 RVA: 0x000F490D File Offset: 0x000F2B0D
		internal static string ERR_VolatileStruct
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_VolatileStruct", null);
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06003197 RID: 12695 RVA: 0x000F491A File Offset: 0x000F2B1A
		internal static string ERR_VolatileAndReadonly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_VolatileAndReadonly", null);
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06003198 RID: 12696 RVA: 0x000F4927 File Offset: 0x000F2B27
		internal static string ERR_AbstractField
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AbstractField", null);
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06003199 RID: 12697 RVA: 0x000F4934 File Offset: 0x000F2B34
		internal static string ERR_BogusExplicitImpl
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BogusExplicitImpl", null);
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600319A RID: 12698 RVA: 0x000F4941 File Offset: 0x000F2B41
		internal static string ERR_ExplicitMethodImplAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExplicitMethodImplAccessor", null);
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x000F494E File Offset: 0x000F2B4E
		internal static string WRN_CoClassWithoutComImport
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CoClassWithoutComImport", null);
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x000F495B File Offset: 0x000F2B5B
		internal static string WRN_CoClassWithoutComImport_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CoClassWithoutComImport_Title", null);
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x000F4968 File Offset: 0x000F2B68
		internal static string ERR_ConditionalWithOutParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConditionalWithOutParam", null);
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x0600319E RID: 12702 RVA: 0x000F4975 File Offset: 0x000F2B75
		internal static string ERR_AccessorImplementingMethod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AccessorImplementingMethod", null);
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x0600319F RID: 12703 RVA: 0x000F4982 File Offset: 0x000F2B82
		internal static string ERR_AliasQualAsExpression
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AliasQualAsExpression", null);
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x060031A0 RID: 12704 RVA: 0x000F498F File Offset: 0x000F2B8F
		internal static string ERR_DerivingFromATyVar
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DerivingFromATyVar", null);
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x000F499C File Offset: 0x000F2B9C
		internal static string ERR_DuplicateTypeParameter
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateTypeParameter", null);
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x060031A2 RID: 12706 RVA: 0x000F49A9 File Offset: 0x000F2BA9
		internal static string WRN_TypeParameterSameAsOuterTypeParameter
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TypeParameterSameAsOuterTypeParameter", null);
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x060031A3 RID: 12707 RVA: 0x000F49B6 File Offset: 0x000F2BB6
		internal static string WRN_TypeParameterSameAsOuterTypeParameter_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TypeParameterSameAsOuterTypeParameter_Title", null);
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x060031A4 RID: 12708 RVA: 0x000F49C3 File Offset: 0x000F2BC3
		internal static string WRN_TypeParameterSameAsOuterMethodTypeParameter
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TypeParameterSameAsOuterMethodTypeParameter", null);
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x060031A5 RID: 12709 RVA: 0x000F49D0 File Offset: 0x000F2BD0
		internal static string WRN_TypeParameterSameAsOuterMethodTypeParameter_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TypeParameterSameAsOuterMethodTypeParameter_Title", null);
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x060031A6 RID: 12710 RVA: 0x000F49DD File Offset: 0x000F2BDD
		internal static string ERR_TypeVariableSameAsParent
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TypeVariableSameAsParent", null);
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x060031A7 RID: 12711 RVA: 0x000F49EA File Offset: 0x000F2BEA
		internal static string ERR_UnifyingInterfaceInstantiations
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnifyingInterfaceInstantiations", null);
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x060031A8 RID: 12712 RVA: 0x000F49F7 File Offset: 0x000F2BF7
		internal static string ERR_GenericDerivingFromAttribute
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GenericDerivingFromAttribute", null);
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x060031A9 RID: 12713 RVA: 0x000F4A04 File Offset: 0x000F2C04
		internal static string ERR_TyVarNotFoundInConstraint
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TyVarNotFoundInConstraint", null);
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x060031AA RID: 12714 RVA: 0x000F4A11 File Offset: 0x000F2C11
		internal static string ERR_BadBoundType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadBoundType", null);
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x060031AB RID: 12715 RVA: 0x000F4A1E File Offset: 0x000F2C1E
		internal static string ERR_SpecialTypeAsBound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SpecialTypeAsBound", null);
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x060031AC RID: 12716 RVA: 0x000F4A2B File Offset: 0x000F2C2B
		internal static string ERR_BadVisBound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadVisBound", null);
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x060031AD RID: 12717 RVA: 0x000F4A38 File Offset: 0x000F2C38
		internal static string ERR_LookupInTypeVariable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LookupInTypeVariable", null);
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x060031AE RID: 12718 RVA: 0x000F4A45 File Offset: 0x000F2C45
		internal static string ERR_BadConstraintType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadConstraintType", null);
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x060031AF RID: 12719 RVA: 0x000F4A52 File Offset: 0x000F2C52
		internal static string ERR_InstanceMemberInStaticClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InstanceMemberInStaticClass", null);
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x060031B0 RID: 12720 RVA: 0x000F4A5F File Offset: 0x000F2C5F
		internal static string ERR_StaticBaseClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StaticBaseClass", null);
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x060031B1 RID: 12721 RVA: 0x000F4A6C File Offset: 0x000F2C6C
		internal static string ERR_ConstructorInStaticClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConstructorInStaticClass", null);
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x060031B2 RID: 12722 RVA: 0x000F4A79 File Offset: 0x000F2C79
		internal static string ERR_DestructorInStaticClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DestructorInStaticClass", null);
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x060031B3 RID: 12723 RVA: 0x000F4A86 File Offset: 0x000F2C86
		internal static string ERR_InstantiatingStaticClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InstantiatingStaticClass", null);
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x060031B4 RID: 12724 RVA: 0x000F4A93 File Offset: 0x000F2C93
		internal static string ERR_StaticDerivedFromNonObject
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StaticDerivedFromNonObject", null);
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x060031B5 RID: 12725 RVA: 0x000F4AA0 File Offset: 0x000F2CA0
		internal static string ERR_StaticClassInterfaceImpl
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StaticClassInterfaceImpl", null);
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x060031B6 RID: 12726 RVA: 0x000F4AAD File Offset: 0x000F2CAD
		internal static string ERR_RefStructInterfaceImpl
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefStructInterfaceImpl", null);
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060031B7 RID: 12727 RVA: 0x000F4ABA File Offset: 0x000F2CBA
		internal static string ERR_OperatorInStaticClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OperatorInStaticClass", null);
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060031B8 RID: 12728 RVA: 0x000F4AC7 File Offset: 0x000F2CC7
		internal static string ERR_ConvertToStaticClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConvertToStaticClass", null);
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x060031B9 RID: 12729 RVA: 0x000F4AD4 File Offset: 0x000F2CD4
		internal static string ERR_ConstraintIsStaticClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConstraintIsStaticClass", null);
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060031BA RID: 12730 RVA: 0x000F4AE1 File Offset: 0x000F2CE1
		internal static string ERR_GenericArgIsStaticClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GenericArgIsStaticClass", null);
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060031BB RID: 12731 RVA: 0x000F4AEE File Offset: 0x000F2CEE
		internal static string ERR_ArrayOfStaticClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ArrayOfStaticClass", null);
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060031BC RID: 12732 RVA: 0x000F4AFB File Offset: 0x000F2CFB
		internal static string ERR_IndexerInStaticClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IndexerInStaticClass", null);
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060031BD RID: 12733 RVA: 0x000F4B08 File Offset: 0x000F2D08
		internal static string ERR_ParameterIsStaticClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ParameterIsStaticClass", null);
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060031BE RID: 12734 RVA: 0x000F4B15 File Offset: 0x000F2D15
		internal static string WRN_ParameterIsStaticClass
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ParameterIsStaticClass", null);
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060031BF RID: 12735 RVA: 0x000F4B22 File Offset: 0x000F2D22
		internal static string WRN_ParameterIsStaticClass_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ParameterIsStaticClass_Title", null);
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060031C0 RID: 12736 RVA: 0x000F4B2F File Offset: 0x000F2D2F
		internal static string ERR_ReturnTypeIsStaticClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ReturnTypeIsStaticClass", null);
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x060031C1 RID: 12737 RVA: 0x000F4B3C File Offset: 0x000F2D3C
		internal static string WRN_ReturnTypeIsStaticClass
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ReturnTypeIsStaticClass", null);
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x000F4B49 File Offset: 0x000F2D49
		internal static string WRN_ReturnTypeIsStaticClass_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ReturnTypeIsStaticClass_Title", null);
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060031C3 RID: 12739 RVA: 0x000F4B56 File Offset: 0x000F2D56
		internal static string ERR_VarDeclIsStaticClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_VarDeclIsStaticClass", null);
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x000F4B63 File Offset: 0x000F2D63
		internal static string ERR_BadEmptyThrowInFinally
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadEmptyThrowInFinally", null);
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x060031C5 RID: 12741 RVA: 0x000F4B70 File Offset: 0x000F2D70
		internal static string ERR_InvalidSpecifier
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidSpecifier", null);
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x000F4B7D File Offset: 0x000F2D7D
		internal static string WRN_AssignmentToLockOrDispose
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AssignmentToLockOrDispose", null);
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060031C7 RID: 12743 RVA: 0x000F4B8A File Offset: 0x000F2D8A
		internal static string WRN_AssignmentToLockOrDispose_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AssignmentToLockOrDispose_Title", null);
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060031C8 RID: 12744 RVA: 0x000F4B97 File Offset: 0x000F2D97
		internal static string ERR_ForwardedTypeInThisAssembly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ForwardedTypeInThisAssembly", null);
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060031C9 RID: 12745 RVA: 0x000F4BA4 File Offset: 0x000F2DA4
		internal static string ERR_ForwardedTypeIsNested
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ForwardedTypeIsNested", null);
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060031CA RID: 12746 RVA: 0x000F4BB1 File Offset: 0x000F2DB1
		internal static string ERR_CycleInTypeForwarder
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CycleInTypeForwarder", null);
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060031CB RID: 12747 RVA: 0x000F4BBE File Offset: 0x000F2DBE
		internal static string ERR_AssemblyNameOnNonModule
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AssemblyNameOnNonModule", null);
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060031CC RID: 12748 RVA: 0x000F4BCB File Offset: 0x000F2DCB
		internal static string ERR_InvalidAssemblyName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidAssemblyName", null);
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060031CD RID: 12749 RVA: 0x000F4BD8 File Offset: 0x000F2DD8
		internal static string ERR_InvalidFwdType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidFwdType", null);
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x060031CE RID: 12750 RVA: 0x000F4BE5 File Offset: 0x000F2DE5
		internal static string ERR_CloseUnimplementedInterfaceMemberStatic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CloseUnimplementedInterfaceMemberStatic", null);
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060031CF RID: 12751 RVA: 0x000F4BF2 File Offset: 0x000F2DF2
		internal static string ERR_CloseUnimplementedInterfaceMemberNotPublic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CloseUnimplementedInterfaceMemberNotPublic", null);
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x000F4BFF File Offset: 0x000F2DFF
		internal static string ERR_CloseUnimplementedInterfaceMemberWrongReturnType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CloseUnimplementedInterfaceMemberWrongReturnType", null);
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060031D1 RID: 12753 RVA: 0x000F4C0C File Offset: 0x000F2E0C
		internal static string ERR_DuplicateTypeForwarder
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateTypeForwarder", null);
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060031D2 RID: 12754 RVA: 0x000F4C19 File Offset: 0x000F2E19
		internal static string ERR_ExpectedSelectOrGroup
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpectedSelectOrGroup", null);
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060031D3 RID: 12755 RVA: 0x000F4C26 File Offset: 0x000F2E26
		internal static string ERR_ExpectedContextualKeywordOn
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpectedContextualKeywordOn", null);
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060031D4 RID: 12756 RVA: 0x000F4C33 File Offset: 0x000F2E33
		internal static string ERR_ExpectedContextualKeywordEquals
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpectedContextualKeywordEquals", null);
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060031D5 RID: 12757 RVA: 0x000F4C40 File Offset: 0x000F2E40
		internal static string ERR_ExpectedContextualKeywordBy
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpectedContextualKeywordBy", null);
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060031D6 RID: 12758 RVA: 0x000F4C4D File Offset: 0x000F2E4D
		internal static string ERR_InvalidAnonymousTypeMemberDeclarator
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidAnonymousTypeMemberDeclarator", null);
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060031D7 RID: 12759 RVA: 0x000F4C5A File Offset: 0x000F2E5A
		internal static string ERR_InvalidInitializerElementInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidInitializerElementInitializer", null);
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060031D8 RID: 12760 RVA: 0x000F4C67 File Offset: 0x000F2E67
		internal static string ERR_InconsistentLambdaParameterUsage
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InconsistentLambdaParameterUsage", null);
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060031D9 RID: 12761 RVA: 0x000F4C74 File Offset: 0x000F2E74
		internal static string ERR_PartialMethodInvalidModifier
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodInvalidModifier", null);
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060031DA RID: 12762 RVA: 0x000F4C81 File Offset: 0x000F2E81
		internal static string ERR_PartialMethodOnlyInPartialClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodOnlyInPartialClass", null);
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060031DB RID: 12763 RVA: 0x000F4C8E File Offset: 0x000F2E8E
		internal static string ERR_PartialMethodNotExplicit
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodNotExplicit", null);
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060031DC RID: 12764 RVA: 0x000F4C9B File Offset: 0x000F2E9B
		internal static string ERR_PartialMethodExtensionDifference
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodExtensionDifference", null);
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060031DD RID: 12765 RVA: 0x000F4CA8 File Offset: 0x000F2EA8
		internal static string ERR_PartialMethodOnlyOneLatent
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodOnlyOneLatent", null);
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060031DE RID: 12766 RVA: 0x000F4CB5 File Offset: 0x000F2EB5
		internal static string ERR_PartialMethodOnlyOneActual
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodOnlyOneActual", null);
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x000F4CC2 File Offset: 0x000F2EC2
		internal static string ERR_PartialMethodParamsDifference
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodParamsDifference", null);
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060031E0 RID: 12768 RVA: 0x000F4CCF File Offset: 0x000F2ECF
		internal static string ERR_PartialMethodMustHaveLatent
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodMustHaveLatent", null);
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060031E1 RID: 12769 RVA: 0x000F4CDC File Offset: 0x000F2EDC
		internal static string ERR_PartialMethodInconsistentTupleNames
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodInconsistentTupleNames", null);
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060031E2 RID: 12770 RVA: 0x000F4CE9 File Offset: 0x000F2EE9
		internal static string ERR_PartialMethodInconsistentConstraints
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodInconsistentConstraints", null);
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060031E3 RID: 12771 RVA: 0x000F4CF6 File Offset: 0x000F2EF6
		internal static string ERR_PartialMethodToDelegate
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodToDelegate", null);
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060031E4 RID: 12772 RVA: 0x000F4D03 File Offset: 0x000F2F03
		internal static string ERR_PartialMethodStaticDifference
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodStaticDifference", null);
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x000F4D10 File Offset: 0x000F2F10
		internal static string ERR_PartialMethodUnsafeDifference
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodUnsafeDifference", null);
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060031E6 RID: 12774 RVA: 0x000F4D1D File Offset: 0x000F2F1D
		internal static string ERR_PartialMethodInExpressionTree
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodInExpressionTree", null);
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060031E7 RID: 12775 RVA: 0x000F4D2A File Offset: 0x000F2F2A
		internal static string WRN_ObsoleteOverridingNonObsolete
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ObsoleteOverridingNonObsolete", null);
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060031E8 RID: 12776 RVA: 0x000F4D37 File Offset: 0x000F2F37
		internal static string WRN_ObsoleteOverridingNonObsolete_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ObsoleteOverridingNonObsolete_Title", null);
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x000F4D44 File Offset: 0x000F2F44
		internal static string WRN_DebugFullNameTooLong
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DebugFullNameTooLong", null);
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060031EA RID: 12778 RVA: 0x000F4D51 File Offset: 0x000F2F51
		internal static string WRN_DebugFullNameTooLong_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DebugFullNameTooLong_Title", null);
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x000F4D5E File Offset: 0x000F2F5E
		internal static string ERR_ImplicitlyTypedVariableAssignedBadValue
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImplicitlyTypedVariableAssignedBadValue", null);
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x000F4D6B File Offset: 0x000F2F6B
		internal static string ERR_ImplicitlyTypedVariableWithNoInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImplicitlyTypedVariableWithNoInitializer", null);
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x060031ED RID: 12781 RVA: 0x000F4D78 File Offset: 0x000F2F78
		internal static string ERR_ImplicitlyTypedVariableMultipleDeclarator
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImplicitlyTypedVariableMultipleDeclarator", null);
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x060031EE RID: 12782 RVA: 0x000F4D85 File Offset: 0x000F2F85
		internal static string ERR_ImplicitlyTypedVariableAssignedArrayInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImplicitlyTypedVariableAssignedArrayInitializer", null);
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060031EF RID: 12783 RVA: 0x000F4D92 File Offset: 0x000F2F92
		internal static string ERR_ImplicitlyTypedLocalCannotBeFixed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImplicitlyTypedLocalCannotBeFixed", null);
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060031F0 RID: 12784 RVA: 0x000F4D9F File Offset: 0x000F2F9F
		internal static string ERR_ImplicitlyTypedVariableCannotBeConst
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImplicitlyTypedVariableCannotBeConst", null);
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060031F1 RID: 12785 RVA: 0x000F4DAC File Offset: 0x000F2FAC
		internal static string WRN_ExternCtorNoImplementation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ExternCtorNoImplementation", null);
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060031F2 RID: 12786 RVA: 0x000F4DB9 File Offset: 0x000F2FB9
		internal static string WRN_ExternCtorNoImplementation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ExternCtorNoImplementation_Title", null);
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060031F3 RID: 12787 RVA: 0x000F4DC6 File Offset: 0x000F2FC6
		internal static string ERR_TypeVarNotFound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TypeVarNotFound", null);
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x060031F4 RID: 12788 RVA: 0x000F4DD3 File Offset: 0x000F2FD3
		internal static string ERR_ImplicitlyTypedArrayNoBestType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImplicitlyTypedArrayNoBestType", null);
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060031F5 RID: 12789 RVA: 0x000F4DE0 File Offset: 0x000F2FE0
		internal static string ERR_AnonymousTypePropertyAssignedBadValue
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AnonymousTypePropertyAssignedBadValue", null);
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060031F6 RID: 12790 RVA: 0x000F4DED File Offset: 0x000F2FED
		internal static string ERR_ExpressionTreeContainsBaseAccess
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsBaseAccess", null);
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x060031F7 RID: 12791 RVA: 0x000F4DFA File Offset: 0x000F2FFA
		internal static string ERR_ExpressionTreeContainsTupleBinOp
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsTupleBinOp", null);
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060031F8 RID: 12792 RVA: 0x000F4E07 File Offset: 0x000F3007
		internal static string ERR_ExpressionTreeContainsAssignment
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsAssignment", null);
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x060031F9 RID: 12793 RVA: 0x000F4E14 File Offset: 0x000F3014
		internal static string ERR_AnonymousTypeDuplicatePropertyName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AnonymousTypeDuplicatePropertyName", null);
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x060031FA RID: 12794 RVA: 0x000F4E21 File Offset: 0x000F3021
		internal static string ERR_StatementLambdaToExpressionTree
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StatementLambdaToExpressionTree", null);
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060031FB RID: 12795 RVA: 0x000F4E2E File Offset: 0x000F302E
		internal static string ERR_ExpressionTreeMustHaveDelegate
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeMustHaveDelegate", null);
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060031FC RID: 12796 RVA: 0x000F4E3B File Offset: 0x000F303B
		internal static string ERR_AnonymousTypeNotAvailable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AnonymousTypeNotAvailable", null);
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060031FD RID: 12797 RVA: 0x000F4E48 File Offset: 0x000F3048
		internal static string ERR_LambdaInIsAs
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LambdaInIsAs", null);
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060031FE RID: 12798 RVA: 0x000F4E55 File Offset: 0x000F3055
		internal static string ERR_TypelessTupleInAs
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TypelessTupleInAs", null);
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060031FF RID: 12799 RVA: 0x000F4E62 File Offset: 0x000F3062
		internal static string ERR_ExpressionTreeContainsMultiDimensionalArrayInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsMultiDimensionalArrayInitializer", null);
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06003200 RID: 12800 RVA: 0x000F4E6F File Offset: 0x000F306F
		internal static string ERR_MissingArgument
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MissingArgument", null);
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06003201 RID: 12801 RVA: 0x000F4E7C File Offset: 0x000F307C
		internal static string ERR_VariableUsedBeforeDeclaration
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_VariableUsedBeforeDeclaration", null);
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06003202 RID: 12802 RVA: 0x000F4E89 File Offset: 0x000F3089
		internal static string ERR_RecursivelyTypedVariable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RecursivelyTypedVariable", null);
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06003203 RID: 12803 RVA: 0x000F4E96 File Offset: 0x000F3096
		internal static string ERR_UnassignedThisAutoProperty
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnassignedThisAutoProperty", null);
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06003204 RID: 12804 RVA: 0x000F4EA3 File Offset: 0x000F30A3
		internal static string WRN_UnassignedThisAutoProperty
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnassignedThisAutoProperty", null);
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06003205 RID: 12805 RVA: 0x000F4EB0 File Offset: 0x000F30B0
		internal static string WRN_UnassignedThisAutoProperty_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnassignedThisAutoProperty_Title", null);
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06003206 RID: 12806 RVA: 0x000F4EBD File Offset: 0x000F30BD
		internal static string ERR_VariableUsedBeforeDeclarationAndHidesField
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_VariableUsedBeforeDeclarationAndHidesField", null);
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06003207 RID: 12807 RVA: 0x000F4ECA File Offset: 0x000F30CA
		internal static string ERR_ExpressionTreeContainsBadCoalesce
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsBadCoalesce", null);
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06003208 RID: 12808 RVA: 0x000F4ED7 File Offset: 0x000F30D7
		internal static string ERR_IdentifierExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IdentifierExpected", null);
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06003209 RID: 12809 RVA: 0x000F4EE4 File Offset: 0x000F30E4
		internal static string ERR_SemicolonExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SemicolonExpected", null);
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x0600320A RID: 12810 RVA: 0x000F4EF1 File Offset: 0x000F30F1
		internal static string ERR_SyntaxError
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SyntaxError", null);
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x0600320B RID: 12811 RVA: 0x000F4EFE File Offset: 0x000F30FE
		internal static string ERR_DuplicateModifier
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateModifier", null);
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x0600320C RID: 12812 RVA: 0x000F4F0B File Offset: 0x000F310B
		internal static string ERR_DuplicateAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateAccessor", null);
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x0600320D RID: 12813 RVA: 0x000F4F18 File Offset: 0x000F3118
		internal static string ERR_IntegralTypeExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IntegralTypeExpected", null);
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x0600320E RID: 12814 RVA: 0x000F4F25 File Offset: 0x000F3125
		internal static string ERR_IllegalEscape
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IllegalEscape", null);
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x0600320F RID: 12815 RVA: 0x000F4F32 File Offset: 0x000F3132
		internal static string ERR_NewlineInConst
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NewlineInConst", null);
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06003210 RID: 12816 RVA: 0x000F4F3F File Offset: 0x000F313F
		internal static string ERR_EmptyCharConst
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EmptyCharConst", null);
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06003211 RID: 12817 RVA: 0x000F4F4C File Offset: 0x000F314C
		internal static string ERR_TooManyCharsInConst
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TooManyCharsInConst", null);
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06003212 RID: 12818 RVA: 0x000F4F59 File Offset: 0x000F3159
		internal static string ERR_InvalidNumber
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidNumber", null);
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06003213 RID: 12819 RVA: 0x000F4F66 File Offset: 0x000F3166
		internal static string ERR_GetOrSetExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GetOrSetExpected", null);
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06003214 RID: 12820 RVA: 0x000F4F73 File Offset: 0x000F3173
		internal static string ERR_ClassTypeExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ClassTypeExpected", null);
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06003215 RID: 12821 RVA: 0x000F4F80 File Offset: 0x000F3180
		internal static string ERR_NamedArgumentExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NamedArgumentExpected", null);
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06003216 RID: 12822 RVA: 0x000F4F8D File Offset: 0x000F318D
		internal static string ERR_TooManyCatches
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TooManyCatches", null);
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06003217 RID: 12823 RVA: 0x000F4F9A File Offset: 0x000F319A
		internal static string ERR_ThisOrBaseExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ThisOrBaseExpected", null);
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06003218 RID: 12824 RVA: 0x000F4FA7 File Offset: 0x000F31A7
		internal static string ERR_OvlUnaryOperatorExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OvlUnaryOperatorExpected", null);
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x000F4FB4 File Offset: 0x000F31B4
		internal static string ERR_OvlBinaryOperatorExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OvlBinaryOperatorExpected", null);
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x0600321A RID: 12826 RVA: 0x000F4FC1 File Offset: 0x000F31C1
		internal static string ERR_IntOverflow
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IntOverflow", null);
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x0600321B RID: 12827 RVA: 0x000F4FCE File Offset: 0x000F31CE
		internal static string ERR_EOFExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EOFExpected", null);
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x000F4FDB File Offset: 0x000F31DB
		internal static string ERR_GlobalDefinitionOrStatementExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GlobalDefinitionOrStatementExpected", null);
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x0600321D RID: 12829 RVA: 0x000F4FE8 File Offset: 0x000F31E8
		internal static string ERR_BadEmbeddedStmt
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadEmbeddedStmt", null);
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x000F4FF5 File Offset: 0x000F31F5
		internal static string ERR_PPDirectiveExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PPDirectiveExpected", null);
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x0600321F RID: 12831 RVA: 0x000F5002 File Offset: 0x000F3202
		internal static string ERR_EndOfPPLineExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EndOfPPLineExpected", null);
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06003220 RID: 12832 RVA: 0x000F500F File Offset: 0x000F320F
		internal static string ERR_CloseParenExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CloseParenExpected", null);
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06003221 RID: 12833 RVA: 0x000F501C File Offset: 0x000F321C
		internal static string ERR_EndifDirectiveExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EndifDirectiveExpected", null);
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x000F5029 File Offset: 0x000F3229
		internal static string ERR_UnexpectedDirective
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnexpectedDirective", null);
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x000F5036 File Offset: 0x000F3236
		internal static string ERR_ErrorDirective
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ErrorDirective", null);
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x000F5043 File Offset: 0x000F3243
		internal static string WRN_WarningDirective
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_WarningDirective", null);
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06003225 RID: 12837 RVA: 0x000F5050 File Offset: 0x000F3250
		internal static string WRN_WarningDirective_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_WarningDirective_Title", null);
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x000F505D File Offset: 0x000F325D
		internal static string ERR_TypeExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TypeExpected", null);
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06003227 RID: 12839 RVA: 0x000F506A File Offset: 0x000F326A
		internal static string ERR_PPDefFollowsToken
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PPDefFollowsToken", null);
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x000F5077 File Offset: 0x000F3277
		internal static string ERR_PPReferenceFollowsToken
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PPReferenceFollowsToken", null);
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x000F5084 File Offset: 0x000F3284
		internal static string ERR_OpenEndedComment
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OpenEndedComment", null);
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x000F5091 File Offset: 0x000F3291
		internal static string ERR_Merge_conflict_marker_encountered
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_Merge_conflict_marker_encountered", null);
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600322B RID: 12843 RVA: 0x000F509E File Offset: 0x000F329E
		internal static string ERR_NoRefOutWhenRefOnly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoRefOutWhenRefOnly", null);
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600322C RID: 12844 RVA: 0x000F50AB File Offset: 0x000F32AB
		internal static string ERR_NoNetModuleOutputWhenRefOutOrRefOnly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoNetModuleOutputWhenRefOutOrRefOnly", null);
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x0600322D RID: 12845 RVA: 0x000F50B8 File Offset: 0x000F32B8
		internal static string ERR_OvlOperatorExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OvlOperatorExpected", null);
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x0600322E RID: 12846 RVA: 0x000F50C5 File Offset: 0x000F32C5
		internal static string ERR_EndRegionDirectiveExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EndRegionDirectiveExpected", null);
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x0600322F RID: 12847 RVA: 0x000F50D2 File Offset: 0x000F32D2
		internal static string ERR_UnterminatedStringLit
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnterminatedStringLit", null);
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06003230 RID: 12848 RVA: 0x000F50DF File Offset: 0x000F32DF
		internal static string ERR_BadDirectivePlacement
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadDirectivePlacement", null);
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06003231 RID: 12849 RVA: 0x000F50EC File Offset: 0x000F32EC
		internal static string ERR_IdentifierExpectedKW
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IdentifierExpectedKW", null);
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x000F50F9 File Offset: 0x000F32F9
		internal static string ERR_SemiOrLBraceExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SemiOrLBraceExpected", null);
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x000F5106 File Offset: 0x000F3306
		internal static string ERR_MultiTypeInDeclaration
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MultiTypeInDeclaration", null);
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06003234 RID: 12852 RVA: 0x000F5113 File Offset: 0x000F3313
		internal static string ERR_AddOrRemoveExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AddOrRemoveExpected", null);
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06003235 RID: 12853 RVA: 0x000F5120 File Offset: 0x000F3320
		internal static string ERR_UnexpectedCharacter
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnexpectedCharacter", null);
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x000F512D File Offset: 0x000F332D
		internal static string ERR_UnexpectedToken
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnexpectedToken", null);
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06003237 RID: 12855 RVA: 0x000F513A File Offset: 0x000F333A
		internal static string ERR_ProtectedInStatic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ProtectedInStatic", null);
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06003238 RID: 12856 RVA: 0x000F5147 File Offset: 0x000F3347
		internal static string WRN_UnreachableGeneralCatch
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreachableGeneralCatch", null);
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06003239 RID: 12857 RVA: 0x000F5154 File Offset: 0x000F3354
		internal static string WRN_UnreachableGeneralCatch_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreachableGeneralCatch_Title", null);
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x0600323A RID: 12858 RVA: 0x000F5161 File Offset: 0x000F3361
		internal static string WRN_UnreachableGeneralCatch_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreachableGeneralCatch_Description", null);
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x000F516E File Offset: 0x000F336E
		internal static string ERR_IncrementLvalueExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IncrementLvalueExpected", null);
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x0600323C RID: 12860 RVA: 0x000F517B File Offset: 0x000F337B
		internal static string ERR_NoSuchMemberOrExtension
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoSuchMemberOrExtension", null);
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x0600323D RID: 12861 RVA: 0x000F5188 File Offset: 0x000F3388
		internal static string ERR_NoSuchMemberOrExtensionNeedUsing
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoSuchMemberOrExtensionNeedUsing", null);
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x0600323E RID: 12862 RVA: 0x000F5195 File Offset: 0x000F3395
		internal static string ERR_BadThisParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadThisParam", null);
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x0600323F RID: 12863 RVA: 0x000F51A2 File Offset: 0x000F33A2
		internal static string ERR_BadParameterModifiers
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadParameterModifiers", null);
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06003240 RID: 12864 RVA: 0x000F51AF File Offset: 0x000F33AF
		internal static string ERR_BadTypeforThis
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadTypeforThis", null);
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06003241 RID: 12865 RVA: 0x000F51BC File Offset: 0x000F33BC
		internal static string ERR_BadParamModThis
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadParamModThis", null);
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06003242 RID: 12866 RVA: 0x000F51C9 File Offset: 0x000F33C9
		internal static string ERR_BadExtensionMeth
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadExtensionMeth", null);
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06003243 RID: 12867 RVA: 0x000F51D6 File Offset: 0x000F33D6
		internal static string ERR_BadExtensionAgg
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadExtensionAgg", null);
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06003244 RID: 12868 RVA: 0x000F51E3 File Offset: 0x000F33E3
		internal static string ERR_DupParamMod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DupParamMod", null);
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06003245 RID: 12869 RVA: 0x000F51F0 File Offset: 0x000F33F0
		internal static string ERR_ExtensionMethodsDecl
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExtensionMethodsDecl", null);
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06003246 RID: 12870 RVA: 0x000F51FD File Offset: 0x000F33FD
		internal static string ERR_ExtensionAttrNotFound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExtensionAttrNotFound", null);
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06003247 RID: 12871 RVA: 0x000F520A File Offset: 0x000F340A
		internal static string ERR_ExplicitExtension
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExplicitExtension", null);
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06003248 RID: 12872 RVA: 0x000F5217 File Offset: 0x000F3417
		internal static string ERR_ExplicitDynamicAttr
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExplicitDynamicAttr", null);
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06003249 RID: 12873 RVA: 0x000F5224 File Offset: 0x000F3424
		internal static string ERR_NoDynamicPhantomOnBaseCtor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoDynamicPhantomOnBaseCtor", null);
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x0600324A RID: 12874 RVA: 0x000F5231 File Offset: 0x000F3431
		internal static string ERR_ValueTypeExtDelegate
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ValueTypeExtDelegate", null);
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x0600324B RID: 12875 RVA: 0x000F523E File Offset: 0x000F343E
		internal static string ERR_BadArgCount
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadArgCount", null);
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x0600324C RID: 12876 RVA: 0x000F524B File Offset: 0x000F344B
		internal static string ERR_BadArgType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadArgType", null);
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x0600324D RID: 12877 RVA: 0x000F5258 File Offset: 0x000F3458
		internal static string ERR_NoSourceFile
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoSourceFile", null);
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x0600324E RID: 12878 RVA: 0x000F5265 File Offset: 0x000F3465
		internal static string ERR_CantRefResource
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantRefResource", null);
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x0600324F RID: 12879 RVA: 0x000F5272 File Offset: 0x000F3472
		internal static string ERR_ResourceNotUnique
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ResourceNotUnique", null);
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06003250 RID: 12880 RVA: 0x000F527F File Offset: 0x000F347F
		internal static string ERR_ResourceFileNameNotUnique
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ResourceFileNameNotUnique", null);
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06003251 RID: 12881 RVA: 0x000F528C File Offset: 0x000F348C
		internal static string ERR_ImportNonAssembly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImportNonAssembly", null);
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06003252 RID: 12882 RVA: 0x000F5299 File Offset: 0x000F3499
		internal static string ERR_RefLvalueExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefLvalueExpected", null);
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06003253 RID: 12883 RVA: 0x000F52A6 File Offset: 0x000F34A6
		internal static string ERR_BaseInStaticMeth
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BaseInStaticMeth", null);
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06003254 RID: 12884 RVA: 0x000F52B3 File Offset: 0x000F34B3
		internal static string ERR_BaseInBadContext
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BaseInBadContext", null);
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06003255 RID: 12885 RVA: 0x000F52C0 File Offset: 0x000F34C0
		internal static string ERR_RbraceExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RbraceExpected", null);
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06003256 RID: 12886 RVA: 0x000F52CD File Offset: 0x000F34CD
		internal static string ERR_LbraceExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LbraceExpected", null);
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06003257 RID: 12887 RVA: 0x000F52DA File Offset: 0x000F34DA
		internal static string ERR_InExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InExpected", null);
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06003258 RID: 12888 RVA: 0x000F52E7 File Offset: 0x000F34E7
		internal static string ERR_InvalidPreprocExpr
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidPreprocExpr", null);
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x000F52F4 File Offset: 0x000F34F4
		internal static string ERR_InvalidMemberDecl
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidMemberDecl", null);
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x0600325A RID: 12890 RVA: 0x000F5301 File Offset: 0x000F3501
		internal static string ERR_MemberNeedsType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MemberNeedsType", null);
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x000F530E File Offset: 0x000F350E
		internal static string ERR_BadBaseType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadBaseType", null);
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x0600325C RID: 12892 RVA: 0x000F531B File Offset: 0x000F351B
		internal static string WRN_EmptySwitch
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_EmptySwitch", null);
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x000F5328 File Offset: 0x000F3528
		internal static string WRN_EmptySwitch_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_EmptySwitch_Title", null);
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x000F5335 File Offset: 0x000F3535
		internal static string ERR_ExpectedEndTry
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpectedEndTry", null);
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x0600325F RID: 12895 RVA: 0x000F5342 File Offset: 0x000F3542
		internal static string ERR_InvalidExprTerm
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidExprTerm", null);
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x000F534F File Offset: 0x000F354F
		internal static string ERR_BadNewExpr
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadNewExpr", null);
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x000F535C File Offset: 0x000F355C
		internal static string ERR_NoNamespacePrivate
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoNamespacePrivate", null);
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x000F5369 File Offset: 0x000F3569
		internal static string ERR_BadVarDecl
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadVarDecl", null);
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06003263 RID: 12899 RVA: 0x000F5376 File Offset: 0x000F3576
		internal static string ERR_UsingAfterElements
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UsingAfterElements", null);
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x000F5383 File Offset: 0x000F3583
		internal static string ERR_BadBinOpArgs
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadBinOpArgs", null);
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x000F5390 File Offset: 0x000F3590
		internal static string ERR_BadUnOpArgs
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadUnOpArgs", null);
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06003266 RID: 12902 RVA: 0x000F539D File Offset: 0x000F359D
		internal static string ERR_NoVoidParameter
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoVoidParameter", null);
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06003267 RID: 12903 RVA: 0x000F53AA File Offset: 0x000F35AA
		internal static string ERR_DuplicateAlias
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateAlias", null);
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06003268 RID: 12904 RVA: 0x000F53B7 File Offset: 0x000F35B7
		internal static string ERR_BadProtectedAccess
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadProtectedAccess", null);
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06003269 RID: 12905 RVA: 0x000F53C4 File Offset: 0x000F35C4
		internal static string ERR_AddModuleAssembly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AddModuleAssembly", null);
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x0600326A RID: 12906 RVA: 0x000F53D1 File Offset: 0x000F35D1
		internal static string ERR_BindToBogusProp2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BindToBogusProp2", null);
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x0600326B RID: 12907 RVA: 0x000F53DE File Offset: 0x000F35DE
		internal static string ERR_BindToBogusProp1
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BindToBogusProp1", null);
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x0600326C RID: 12908 RVA: 0x000F53EB File Offset: 0x000F35EB
		internal static string ERR_NoVoidHere
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoVoidHere", null);
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x0600326D RID: 12909 RVA: 0x000F53F8 File Offset: 0x000F35F8
		internal static string ERR_IndexerNeedsParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IndexerNeedsParam", null);
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x0600326E RID: 12910 RVA: 0x000F5405 File Offset: 0x000F3605
		internal static string ERR_BadArraySyntax
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadArraySyntax", null);
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x0600326F RID: 12911 RVA: 0x000F5412 File Offset: 0x000F3612
		internal static string ERR_BadOperatorSyntax
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadOperatorSyntax", null);
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06003270 RID: 12912 RVA: 0x000F541F File Offset: 0x000F361F
		internal static string ERR_MainClassNotFound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MainClassNotFound", null);
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06003271 RID: 12913 RVA: 0x000F542C File Offset: 0x000F362C
		internal static string ERR_MainClassNotClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MainClassNotClass", null);
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06003272 RID: 12914 RVA: 0x000F5439 File Offset: 0x000F3639
		internal static string ERR_NoMainInClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoMainInClass", null);
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06003273 RID: 12915 RVA: 0x000F5446 File Offset: 0x000F3646
		internal static string ERR_MainClassIsImport
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MainClassIsImport", null);
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06003274 RID: 12916 RVA: 0x000F5453 File Offset: 0x000F3653
		internal static string ERR_OutputNeedsName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OutputNeedsName", null);
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06003275 RID: 12917 RVA: 0x000F5460 File Offset: 0x000F3660
		internal static string ERR_NoOutputDirectory
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoOutputDirectory", null);
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06003276 RID: 12918 RVA: 0x000F546D File Offset: 0x000F366D
		internal static string ERR_CantHaveWin32ResAndManifest
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantHaveWin32ResAndManifest", null);
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06003277 RID: 12919 RVA: 0x000F547A File Offset: 0x000F367A
		internal static string ERR_CantHaveWin32ResAndIcon
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantHaveWin32ResAndIcon", null);
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06003278 RID: 12920 RVA: 0x000F5487 File Offset: 0x000F3687
		internal static string ERR_CantReadResource
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantReadResource", null);
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x000F5494 File Offset: 0x000F3694
		internal static string ERR_DocFileGen
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DocFileGen", null);
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x0600327A RID: 12922 RVA: 0x000F54A1 File Offset: 0x000F36A1
		internal static string WRN_XMLParseError
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_XMLParseError", null);
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x0600327B RID: 12923 RVA: 0x000F54AE File Offset: 0x000F36AE
		internal static string WRN_XMLParseError_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_XMLParseError_Title", null);
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x0600327C RID: 12924 RVA: 0x000F54BB File Offset: 0x000F36BB
		internal static string WRN_DuplicateParamTag
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DuplicateParamTag", null);
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x000F54C8 File Offset: 0x000F36C8
		internal static string WRN_DuplicateParamTag_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DuplicateParamTag_Title", null);
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x0600327E RID: 12926 RVA: 0x000F54D5 File Offset: 0x000F36D5
		internal static string WRN_UnmatchedParamTag
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnmatchedParamTag", null);
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x000F54E2 File Offset: 0x000F36E2
		internal static string WRN_UnmatchedParamTag_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnmatchedParamTag_Title", null);
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06003280 RID: 12928 RVA: 0x000F54EF File Offset: 0x000F36EF
		internal static string WRN_UnmatchedParamRefTag
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnmatchedParamRefTag", null);
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x000F54FC File Offset: 0x000F36FC
		internal static string WRN_UnmatchedParamRefTag_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnmatchedParamRefTag_Title", null);
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06003282 RID: 12930 RVA: 0x000F5509 File Offset: 0x000F3709
		internal static string WRN_MissingParamTag
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MissingParamTag", null);
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x000F5516 File Offset: 0x000F3716
		internal static string WRN_MissingParamTag_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MissingParamTag_Title", null);
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06003284 RID: 12932 RVA: 0x000F5523 File Offset: 0x000F3723
		internal static string WRN_BadXMLRef
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadXMLRef", null);
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x000F5530 File Offset: 0x000F3730
		internal static string WRN_BadXMLRef_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadXMLRef_Title", null);
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06003286 RID: 12934 RVA: 0x000F553D File Offset: 0x000F373D
		internal static string ERR_BadStackAllocExpr
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadStackAllocExpr", null);
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06003287 RID: 12935 RVA: 0x000F554A File Offset: 0x000F374A
		internal static string ERR_InvalidLineNumber
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidLineNumber", null);
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06003288 RID: 12936 RVA: 0x000F5557 File Offset: 0x000F3757
		internal static string ERR_MissingPPFile
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MissingPPFile", null);
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06003289 RID: 12937 RVA: 0x000F5564 File Offset: 0x000F3764
		internal static string ERR_ExpectedPPFile
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpectedPPFile", null);
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x0600328A RID: 12938 RVA: 0x000F5571 File Offset: 0x000F3771
		internal static string ERR_ReferenceDirectiveOnlyAllowedInScripts
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ReferenceDirectiveOnlyAllowedInScripts", null);
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x0600328B RID: 12939 RVA: 0x000F557E File Offset: 0x000F377E
		internal static string ERR_ForEachMissingMember
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ForEachMissingMember", null);
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x0600328C RID: 12940 RVA: 0x000F558B File Offset: 0x000F378B
		internal static string ERR_AwaitForEachMissingMember
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AwaitForEachMissingMember", null);
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x0600328D RID: 12941 RVA: 0x000F5598 File Offset: 0x000F3798
		internal static string ERR_ForEachMissingMemberWrongAsync
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ForEachMissingMemberWrongAsync", null);
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x0600328E RID: 12942 RVA: 0x000F55A5 File Offset: 0x000F37A5
		internal static string ERR_AwaitForEachMissingMemberWrongAsync
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AwaitForEachMissingMemberWrongAsync", null);
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x0600328F RID: 12943 RVA: 0x000F55B2 File Offset: 0x000F37B2
		internal static string ERR_PossibleAsyncIteratorWithoutYield
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PossibleAsyncIteratorWithoutYield", null);
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06003290 RID: 12944 RVA: 0x000F55BF File Offset: 0x000F37BF
		internal static string ERR_PossibleAsyncIteratorWithoutYieldOrAwait
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PossibleAsyncIteratorWithoutYieldOrAwait", null);
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06003291 RID: 12945 RVA: 0x000F55CC File Offset: 0x000F37CC
		internal static string ERR_StaticLocalFunctionCannotCaptureVariable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StaticLocalFunctionCannotCaptureVariable", null);
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x000F55D9 File Offset: 0x000F37D9
		internal static string ERR_StaticLocalFunctionCannotCaptureThis
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StaticLocalFunctionCannotCaptureThis", null);
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06003293 RID: 12947 RVA: 0x000F55E6 File Offset: 0x000F37E6
		internal static string WRN_BadXMLRefParamType
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadXMLRefParamType", null);
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06003294 RID: 12948 RVA: 0x000F55F3 File Offset: 0x000F37F3
		internal static string WRN_BadXMLRefParamType_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadXMLRefParamType_Title", null);
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06003295 RID: 12949 RVA: 0x000F5600 File Offset: 0x000F3800
		internal static string WRN_BadXMLRefReturnType
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadXMLRefReturnType", null);
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06003296 RID: 12950 RVA: 0x000F560D File Offset: 0x000F380D
		internal static string WRN_BadXMLRefReturnType_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadXMLRefReturnType_Title", null);
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06003297 RID: 12951 RVA: 0x000F561A File Offset: 0x000F381A
		internal static string ERR_BadWin32Res
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadWin32Res", null);
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06003298 RID: 12952 RVA: 0x000F5627 File Offset: 0x000F3827
		internal static string WRN_BadXMLRefSyntax
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadXMLRefSyntax", null);
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06003299 RID: 12953 RVA: 0x000F5634 File Offset: 0x000F3834
		internal static string WRN_BadXMLRefSyntax_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadXMLRefSyntax_Title", null);
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x0600329A RID: 12954 RVA: 0x000F5641 File Offset: 0x000F3841
		internal static string ERR_BadModifierLocation
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadModifierLocation", null);
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x0600329B RID: 12955 RVA: 0x000F564E File Offset: 0x000F384E
		internal static string ERR_MissingArraySize
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MissingArraySize", null);
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x0600329C RID: 12956 RVA: 0x000F565B File Offset: 0x000F385B
		internal static string WRN_UnprocessedXMLComment
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnprocessedXMLComment", null);
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x0600329D RID: 12957 RVA: 0x000F5668 File Offset: 0x000F3868
		internal static string WRN_UnprocessedXMLComment_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnprocessedXMLComment_Title", null);
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x0600329E RID: 12958 RVA: 0x000F5675 File Offset: 0x000F3875
		internal static string WRN_FailedInclude
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_FailedInclude", null);
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x0600329F RID: 12959 RVA: 0x000F5682 File Offset: 0x000F3882
		internal static string WRN_FailedInclude_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_FailedInclude_Title", null);
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x060032A0 RID: 12960 RVA: 0x000F568F File Offset: 0x000F388F
		internal static string WRN_InvalidInclude
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_InvalidInclude", null);
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x060032A1 RID: 12961 RVA: 0x000F569C File Offset: 0x000F389C
		internal static string WRN_InvalidInclude_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_InvalidInclude_Title", null);
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x060032A2 RID: 12962 RVA: 0x000F56A9 File Offset: 0x000F38A9
		internal static string WRN_MissingXMLComment
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MissingXMLComment", null);
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x060032A3 RID: 12963 RVA: 0x000F56B6 File Offset: 0x000F38B6
		internal static string WRN_MissingXMLComment_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MissingXMLComment_Title", null);
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x060032A4 RID: 12964 RVA: 0x000F56C3 File Offset: 0x000F38C3
		internal static string WRN_MissingXMLComment_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MissingXMLComment_Description", null);
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x060032A5 RID: 12965 RVA: 0x000F56D0 File Offset: 0x000F38D0
		internal static string WRN_XMLParseIncludeError
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_XMLParseIncludeError", null);
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x060032A6 RID: 12966 RVA: 0x000F56DD File Offset: 0x000F38DD
		internal static string WRN_XMLParseIncludeError_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_XMLParseIncludeError_Title", null);
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x060032A7 RID: 12967 RVA: 0x000F56EA File Offset: 0x000F38EA
		internal static string ERR_BadDelArgCount
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadDelArgCount", null);
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x060032A8 RID: 12968 RVA: 0x000F56F7 File Offset: 0x000F38F7
		internal static string ERR_UnexpectedSemicolon
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnexpectedSemicolon", null);
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x060032A9 RID: 12969 RVA: 0x000F5704 File Offset: 0x000F3904
		internal static string ERR_MethodReturnCantBeRefAny
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MethodReturnCantBeRefAny", null);
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x060032AA RID: 12970 RVA: 0x000F5711 File Offset: 0x000F3911
		internal static string ERR_CompileCancelled
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CompileCancelled", null);
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x060032AB RID: 12971 RVA: 0x000F571E File Offset: 0x000F391E
		internal static string ERR_MethodArgCantBeRefAny
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MethodArgCantBeRefAny", null);
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x060032AC RID: 12972 RVA: 0x000F572B File Offset: 0x000F392B
		internal static string ERR_AssgReadonlyLocal
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AssgReadonlyLocal", null);
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060032AD RID: 12973 RVA: 0x000F5738 File Offset: 0x000F3938
		internal static string ERR_RefReadonlyLocal
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReadonlyLocal", null);
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060032AE RID: 12974 RVA: 0x000F5745 File Offset: 0x000F3945
		internal static string ERR_CantUseRequiredAttribute
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantUseRequiredAttribute", null);
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060032AF RID: 12975 RVA: 0x000F5752 File Offset: 0x000F3952
		internal static string ERR_NoModifiersOnAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoModifiersOnAccessor", null);
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060032B0 RID: 12976 RVA: 0x000F575F File Offset: 0x000F395F
		internal static string ERR_ParamsCantBeWithModifier
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ParamsCantBeWithModifier", null);
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060032B1 RID: 12977 RVA: 0x000F576C File Offset: 0x000F396C
		internal static string ERR_ReturnNotLValue
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ReturnNotLValue", null);
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060032B2 RID: 12978 RVA: 0x000F5779 File Offset: 0x000F3979
		internal static string ERR_MissingCoClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MissingCoClass", null);
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060032B3 RID: 12979 RVA: 0x000F5786 File Offset: 0x000F3986
		internal static string ERR_AmbiguousAttribute
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AmbiguousAttribute", null);
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060032B4 RID: 12980 RVA: 0x000F5793 File Offset: 0x000F3993
		internal static string ERR_BadArgExtraRef
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadArgExtraRef", null);
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060032B5 RID: 12981 RVA: 0x000F57A0 File Offset: 0x000F39A0
		internal static string WRN_CmdOptionConflictsSource
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CmdOptionConflictsSource", null);
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060032B6 RID: 12982 RVA: 0x000F57AD File Offset: 0x000F39AD
		internal static string WRN_CmdOptionConflictsSource_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CmdOptionConflictsSource_Title", null);
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060032B7 RID: 12983 RVA: 0x000F57BA File Offset: 0x000F39BA
		internal static string WRN_CmdOptionConflictsSource_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CmdOptionConflictsSource_Description", null);
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060032B8 RID: 12984 RVA: 0x000F57C7 File Offset: 0x000F39C7
		internal static string ERR_BadCompatMode
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadCompatMode", null);
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060032B9 RID: 12985 RVA: 0x000F57D4 File Offset: 0x000F39D4
		internal static string ERR_DelegateOnConditional
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DelegateOnConditional", null);
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x060032BA RID: 12986 RVA: 0x000F57E1 File Offset: 0x000F39E1
		internal static string ERR_CantMakeTempFile
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantMakeTempFile", null);
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x060032BB RID: 12987 RVA: 0x000F57EE File Offset: 0x000F39EE
		internal static string ERR_BadArgRef
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadArgRef", null);
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x060032BC RID: 12988 RVA: 0x000F57FB File Offset: 0x000F39FB
		internal static string ERR_YieldInAnonMeth
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_YieldInAnonMeth", null);
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x060032BD RID: 12989 RVA: 0x000F5808 File Offset: 0x000F3A08
		internal static string ERR_ReturnInIterator
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ReturnInIterator", null);
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060032BE RID: 12990 RVA: 0x000F5815 File Offset: 0x000F3A15
		internal static string ERR_BadIteratorArgType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadIteratorArgType", null);
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060032BF RID: 12991 RVA: 0x000F5822 File Offset: 0x000F3A22
		internal static string ERR_BadIteratorReturn
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadIteratorReturn", null);
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x000F582F File Offset: 0x000F3A2F
		internal static string ERR_BadYieldInFinally
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadYieldInFinally", null);
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x060032C1 RID: 12993 RVA: 0x000F583C File Offset: 0x000F3A3C
		internal static string ERR_IteratorMustBeAsync
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IteratorMustBeAsync", null);
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x060032C2 RID: 12994 RVA: 0x000F5849 File Offset: 0x000F3A49
		internal static string ERR_BadYieldInTryOfCatch
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadYieldInTryOfCatch", null);
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x060032C3 RID: 12995 RVA: 0x000F5856 File Offset: 0x000F3A56
		internal static string ERR_EmptyYield
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EmptyYield", null);
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x060032C4 RID: 12996 RVA: 0x000F5863 File Offset: 0x000F3A63
		internal static string ERR_AnonDelegateCantUse
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AnonDelegateCantUse", null);
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x060032C5 RID: 12997 RVA: 0x000F5870 File Offset: 0x000F3A70
		internal static string ERR_IllegalInnerUnsafe
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IllegalInnerUnsafe", null);
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x060032C6 RID: 12998 RVA: 0x000F587D File Offset: 0x000F3A7D
		internal static string ERR_BadYieldInCatch
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadYieldInCatch", null);
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x060032C7 RID: 12999 RVA: 0x000F588A File Offset: 0x000F3A8A
		internal static string ERR_BadDelegateLeave
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadDelegateLeave", null);
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x060032C8 RID: 13000 RVA: 0x000F5897 File Offset: 0x000F3A97
		internal static string ERR_IllegalSuppression
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IllegalSuppression", null);
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x060032C9 RID: 13001 RVA: 0x000F58A4 File Offset: 0x000F3AA4
		internal static string WRN_IllegalPragma
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IllegalPragma", null);
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x060032CA RID: 13002 RVA: 0x000F58B1 File Offset: 0x000F3AB1
		internal static string WRN_IllegalPragma_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IllegalPragma_Title", null);
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x060032CB RID: 13003 RVA: 0x000F58BE File Offset: 0x000F3ABE
		internal static string WRN_IllegalPPWarning
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IllegalPPWarning", null);
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x060032CC RID: 13004 RVA: 0x000F58CB File Offset: 0x000F3ACB
		internal static string WRN_IllegalPPWarning_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IllegalPPWarning_Title", null);
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x060032CD RID: 13005 RVA: 0x000F58D8 File Offset: 0x000F3AD8
		internal static string WRN_BadRestoreNumber
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadRestoreNumber", null);
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x060032CE RID: 13006 RVA: 0x000F58E5 File Offset: 0x000F3AE5
		internal static string WRN_BadRestoreNumber_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadRestoreNumber_Title", null);
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x060032CF RID: 13007 RVA: 0x000F58F2 File Offset: 0x000F3AF2
		internal static string ERR_VarargsIterator
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_VarargsIterator", null);
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x060032D0 RID: 13008 RVA: 0x000F58FF File Offset: 0x000F3AFF
		internal static string ERR_UnsafeIteratorArgType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnsafeIteratorArgType", null);
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x060032D1 RID: 13009 RVA: 0x000F590C File Offset: 0x000F3B0C
		internal static string ERR_BadCoClassSig
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadCoClassSig", null);
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x060032D2 RID: 13010 RVA: 0x000F5919 File Offset: 0x000F3B19
		internal static string ERR_MultipleIEnumOfT
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MultipleIEnumOfT", null);
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x060032D3 RID: 13011 RVA: 0x000F5926 File Offset: 0x000F3B26
		internal static string ERR_MultipleIAsyncEnumOfT
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MultipleIAsyncEnumOfT", null);
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060032D4 RID: 13012 RVA: 0x000F5933 File Offset: 0x000F3B33
		internal static string ERR_FixedDimsRequired
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FixedDimsRequired", null);
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060032D5 RID: 13013 RVA: 0x000F5940 File Offset: 0x000F3B40
		internal static string ERR_FixedNotInStruct
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FixedNotInStruct", null);
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060032D6 RID: 13014 RVA: 0x000F594D File Offset: 0x000F3B4D
		internal static string ERR_AnonymousReturnExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AnonymousReturnExpected", null);
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060032D7 RID: 13015 RVA: 0x000F595A File Offset: 0x000F3B5A
		internal static string WRN_NonECMAFeature
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NonECMAFeature", null);
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060032D8 RID: 13016 RVA: 0x000F5967 File Offset: 0x000F3B67
		internal static string WRN_NonECMAFeature_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NonECMAFeature_Title", null);
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060032D9 RID: 13017 RVA: 0x000F5974 File Offset: 0x000F3B74
		internal static string ERR_ExpectedVerbatimLiteral
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpectedVerbatimLiteral", null);
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x060032DA RID: 13018 RVA: 0x000F5981 File Offset: 0x000F3B81
		internal static string ERR_RefReadonly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReadonly", null);
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x060032DB RID: 13019 RVA: 0x000F598E File Offset: 0x000F3B8E
		internal static string ERR_RefReadonly2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReadonly2", null);
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x060032DC RID: 13020 RVA: 0x000F599B File Offset: 0x000F3B9B
		internal static string ERR_AssgReadonly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AssgReadonly", null);
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x060032DD RID: 13021 RVA: 0x000F59A8 File Offset: 0x000F3BA8
		internal static string ERR_AssgReadonly2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AssgReadonly2", null);
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x060032DE RID: 13022 RVA: 0x000F59B5 File Offset: 0x000F3BB5
		internal static string ERR_RefReadonlyNotField
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReadonlyNotField", null);
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x060032DF RID: 13023 RVA: 0x000F59C2 File Offset: 0x000F3BC2
		internal static string ERR_RefReadonlyNotField2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReadonlyNotField2", null);
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x060032E0 RID: 13024 RVA: 0x000F59CF File Offset: 0x000F3BCF
		internal static string ERR_AssignReadonlyNotField
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AssignReadonlyNotField", null);
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x060032E1 RID: 13025 RVA: 0x000F59DC File Offset: 0x000F3BDC
		internal static string ERR_AssignReadonlyNotField2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AssignReadonlyNotField2", null);
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x060032E2 RID: 13026 RVA: 0x000F59E9 File Offset: 0x000F3BE9
		internal static string ERR_RefReturnReadonlyNotField
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnReadonlyNotField", null);
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x060032E3 RID: 13027 RVA: 0x000F59F6 File Offset: 0x000F3BF6
		internal static string ERR_RefReturnReadonlyNotField2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnReadonlyNotField2", null);
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060032E4 RID: 13028 RVA: 0x000F5A03 File Offset: 0x000F3C03
		internal static string ERR_AssgReadonlyStatic2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AssgReadonlyStatic2", null);
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x060032E5 RID: 13029 RVA: 0x000F5A10 File Offset: 0x000F3C10
		internal static string ERR_RefReadonlyStatic2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReadonlyStatic2", null);
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x060032E6 RID: 13030 RVA: 0x000F5A1D File Offset: 0x000F3C1D
		internal static string ERR_AssgReadonlyLocal2Cause
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AssgReadonlyLocal2Cause", null);
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x060032E7 RID: 13031 RVA: 0x000F5A2A File Offset: 0x000F3C2A
		internal static string ERR_RefReadonlyLocal2Cause
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReadonlyLocal2Cause", null);
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x060032E8 RID: 13032 RVA: 0x000F5A37 File Offset: 0x000F3C37
		internal static string ERR_AssgReadonlyLocalCause
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AssgReadonlyLocalCause", null);
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x060032E9 RID: 13033 RVA: 0x000F5A44 File Offset: 0x000F3C44
		internal static string ERR_RefReadonlyLocalCause
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReadonlyLocalCause", null);
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x060032EA RID: 13034 RVA: 0x000F5A51 File Offset: 0x000F3C51
		internal static string WRN_ErrorOverride
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ErrorOverride", null);
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060032EB RID: 13035 RVA: 0x000F5A5E File Offset: 0x000F3C5E
		internal static string WRN_ErrorOverride_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ErrorOverride_Title", null);
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x060032EC RID: 13036 RVA: 0x000F5A6B File Offset: 0x000F3C6B
		internal static string WRN_ErrorOverride_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ErrorOverride_Description", null);
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x060032ED RID: 13037 RVA: 0x000F5A78 File Offset: 0x000F3C78
		internal static string ERR_AnonMethToNonDel
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AnonMethToNonDel", null);
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x060032EE RID: 13038 RVA: 0x000F5A85 File Offset: 0x000F3C85
		internal static string ERR_CantConvAnonMethParams
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantConvAnonMethParams", null);
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x060032EF RID: 13039 RVA: 0x000F5A92 File Offset: 0x000F3C92
		internal static string ERR_CantConvAnonMethReturns
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantConvAnonMethReturns", null);
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x060032F0 RID: 13040 RVA: 0x000F5A9F File Offset: 0x000F3C9F
		internal static string ERR_BadAsyncReturnExpression
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAsyncReturnExpression", null);
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060032F1 RID: 13041 RVA: 0x000F5AAC File Offset: 0x000F3CAC
		internal static string ERR_CantConvAsyncAnonFuncReturns
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantConvAsyncAnonFuncReturns", null);
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060032F2 RID: 13042 RVA: 0x000F5AB9 File Offset: 0x000F3CB9
		internal static string ERR_IllegalFixedType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IllegalFixedType", null);
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x060032F3 RID: 13043 RVA: 0x000F5AC6 File Offset: 0x000F3CC6
		internal static string ERR_FixedOverflow
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FixedOverflow", null);
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060032F4 RID: 13044 RVA: 0x000F5AD3 File Offset: 0x000F3CD3
		internal static string ERR_InvalidFixedArraySize
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidFixedArraySize", null);
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060032F5 RID: 13045 RVA: 0x000F5AE0 File Offset: 0x000F3CE0
		internal static string ERR_FixedBufferNotFixed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FixedBufferNotFixed", null);
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x060032F6 RID: 13046 RVA: 0x000F5AED File Offset: 0x000F3CED
		internal static string ERR_AttributeNotOnAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AttributeNotOnAccessor", null);
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060032F7 RID: 13047 RVA: 0x000F5AFA File Offset: 0x000F3CFA
		internal static string WRN_InvalidSearchPathDir
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_InvalidSearchPathDir", null);
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x060032F8 RID: 13048 RVA: 0x000F5B07 File Offset: 0x000F3D07
		internal static string WRN_InvalidSearchPathDir_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_InvalidSearchPathDir_Title", null);
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x060032F9 RID: 13049 RVA: 0x000F5B14 File Offset: 0x000F3D14
		internal static string ERR_IllegalVarArgs
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IllegalVarArgs", null);
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x060032FA RID: 13050 RVA: 0x000F5B21 File Offset: 0x000F3D21
		internal static string ERR_IllegalParams
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IllegalParams", null);
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x060032FB RID: 13051 RVA: 0x000F5B2E File Offset: 0x000F3D2E
		internal static string ERR_BadModifiersOnNamespace
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadModifiersOnNamespace", null);
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x060032FC RID: 13052 RVA: 0x000F5B3B File Offset: 0x000F3D3B
		internal static string ERR_BadPlatformType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadPlatformType", null);
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060032FD RID: 13053 RVA: 0x000F5B48 File Offset: 0x000F3D48
		internal static string ERR_ThisStructNotInAnonMeth
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ThisStructNotInAnonMeth", null);
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060032FE RID: 13054 RVA: 0x000F5B55 File Offset: 0x000F3D55
		internal static string ERR_NoConvToIDisp
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoConvToIDisp", null);
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x000F5B62 File Offset: 0x000F3D62
		internal static string ERR_NoConvToIDispWrongAsync
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoConvToIDispWrongAsync", null);
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06003300 RID: 13056 RVA: 0x000F5B6F File Offset: 0x000F3D6F
		internal static string ERR_NoConvToIAsyncDisp
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoConvToIAsyncDisp", null);
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x000F5B7C File Offset: 0x000F3D7C
		internal static string ERR_NoConvToIAsyncDispWrongAsync
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoConvToIAsyncDispWrongAsync", null);
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06003302 RID: 13058 RVA: 0x000F5B89 File Offset: 0x000F3D89
		internal static string ERR_BadParamRef
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadParamRef", null);
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06003303 RID: 13059 RVA: 0x000F5B96 File Offset: 0x000F3D96
		internal static string ERR_BadParamExtraRef
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadParamExtraRef", null);
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06003304 RID: 13060 RVA: 0x000F5BA3 File Offset: 0x000F3DA3
		internal static string ERR_BadParamType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadParamType", null);
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06003305 RID: 13061 RVA: 0x000F5BB0 File Offset: 0x000F3DB0
		internal static string ERR_BadExternIdentifier
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadExternIdentifier", null);
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06003306 RID: 13062 RVA: 0x000F5BBD File Offset: 0x000F3DBD
		internal static string ERR_AliasMissingFile
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AliasMissingFile", null);
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06003307 RID: 13063 RVA: 0x000F5BCA File Offset: 0x000F3DCA
		internal static string ERR_GlobalExternAlias
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GlobalExternAlias", null);
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06003308 RID: 13064 RVA: 0x000F5BD7 File Offset: 0x000F3DD7
		internal static string ERR_MissingTypeInSource
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MissingTypeInSource", null);
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06003309 RID: 13065 RVA: 0x000F5BE4 File Offset: 0x000F3DE4
		internal static string ERR_MissingTypeInAssembly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MissingTypeInAssembly", null);
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x0600330A RID: 13066 RVA: 0x000F5BF1 File Offset: 0x000F3DF1
		internal static string WRN_MultiplePredefTypes
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MultiplePredefTypes", null);
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x0600330B RID: 13067 RVA: 0x000F5BFE File Offset: 0x000F3DFE
		internal static string WRN_MultiplePredefTypes_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MultiplePredefTypes_Title", null);
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x0600330C RID: 13068 RVA: 0x000F5C0B File Offset: 0x000F3E0B
		internal static string WRN_MultiplePredefTypes_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MultiplePredefTypes_Description", null);
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x0600330D RID: 13069 RVA: 0x000F5C18 File Offset: 0x000F3E18
		internal static string ERR_LocalCantBeFixedAndHoisted
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LocalCantBeFixedAndHoisted", null);
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x0600330E RID: 13070 RVA: 0x000F5C25 File Offset: 0x000F3E25
		internal static string WRN_TooManyLinesForDebugger
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TooManyLinesForDebugger", null);
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x0600330F RID: 13071 RVA: 0x000F5C32 File Offset: 0x000F3E32
		internal static string WRN_TooManyLinesForDebugger_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TooManyLinesForDebugger_Title", null);
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06003310 RID: 13072 RVA: 0x000F5C3F File Offset: 0x000F3E3F
		internal static string ERR_CantConvAnonMethNoParams
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantConvAnonMethNoParams", null);
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06003311 RID: 13073 RVA: 0x000F5C4C File Offset: 0x000F3E4C
		internal static string ERR_ConditionalOnNonAttributeClass
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConditionalOnNonAttributeClass", null);
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06003312 RID: 13074 RVA: 0x000F5C59 File Offset: 0x000F3E59
		internal static string WRN_CallOnNonAgileField
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CallOnNonAgileField", null);
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06003313 RID: 13075 RVA: 0x000F5C66 File Offset: 0x000F3E66
		internal static string WRN_CallOnNonAgileField_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CallOnNonAgileField_Title", null);
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x000F5C73 File Offset: 0x000F3E73
		internal static string WRN_CallOnNonAgileField_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CallOnNonAgileField_Description", null);
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x000F5C80 File Offset: 0x000F3E80
		internal static string WRN_BadWarningNumber
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadWarningNumber", null);
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x000F5C8D File Offset: 0x000F3E8D
		internal static string WRN_BadWarningNumber_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadWarningNumber_Title", null);
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06003317 RID: 13079 RVA: 0x000F5C9A File Offset: 0x000F3E9A
		internal static string WRN_BadWarningNumber_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadWarningNumber_Description", null);
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06003318 RID: 13080 RVA: 0x000F5CA7 File Offset: 0x000F3EA7
		internal static string WRN_InvalidNumber
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_InvalidNumber", null);
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06003319 RID: 13081 RVA: 0x000F5CB4 File Offset: 0x000F3EB4
		internal static string WRN_InvalidNumber_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_InvalidNumber_Title", null);
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x0600331A RID: 13082 RVA: 0x000F5CC1 File Offset: 0x000F3EC1
		internal static string WRN_FileNameTooLong
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_FileNameTooLong", null);
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x0600331B RID: 13083 RVA: 0x000F5CCE File Offset: 0x000F3ECE
		internal static string WRN_FileNameTooLong_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_FileNameTooLong_Title", null);
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x0600331C RID: 13084 RVA: 0x000F5CDB File Offset: 0x000F3EDB
		internal static string WRN_IllegalPPChecksum
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IllegalPPChecksum", null);
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x0600331D RID: 13085 RVA: 0x000F5CE8 File Offset: 0x000F3EE8
		internal static string WRN_IllegalPPChecksum_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IllegalPPChecksum_Title", null);
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x0600331E RID: 13086 RVA: 0x000F5CF5 File Offset: 0x000F3EF5
		internal static string WRN_EndOfPPLineExpected
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_EndOfPPLineExpected", null);
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x0600331F RID: 13087 RVA: 0x000F5D02 File Offset: 0x000F3F02
		internal static string WRN_EndOfPPLineExpected_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_EndOfPPLineExpected_Title", null);
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06003320 RID: 13088 RVA: 0x000F5D0F File Offset: 0x000F3F0F
		internal static string WRN_ConflictingChecksum
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ConflictingChecksum", null);
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06003321 RID: 13089 RVA: 0x000F5D1C File Offset: 0x000F3F1C
		internal static string WRN_ConflictingChecksum_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ConflictingChecksum_Title", null);
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06003322 RID: 13090 RVA: 0x000F5D29 File Offset: 0x000F3F29
		internal static string WRN_InvalidAssemblyName
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_InvalidAssemblyName", null);
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06003323 RID: 13091 RVA: 0x000F5D36 File Offset: 0x000F3F36
		internal static string WRN_InvalidAssemblyName_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_InvalidAssemblyName_Title", null);
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06003324 RID: 13092 RVA: 0x000F5D43 File Offset: 0x000F3F43
		internal static string WRN_InvalidAssemblyName_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_InvalidAssemblyName_Description", null);
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06003325 RID: 13093 RVA: 0x000F5D50 File Offset: 0x000F3F50
		internal static string WRN_UnifyReferenceMajMin
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnifyReferenceMajMin", null);
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06003326 RID: 13094 RVA: 0x000F5D5D File Offset: 0x000F3F5D
		internal static string WRN_UnifyReferenceMajMin_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnifyReferenceMajMin_Title", null);
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06003327 RID: 13095 RVA: 0x000F5D6A File Offset: 0x000F3F6A
		internal static string WRN_UnifyReferenceMajMin_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnifyReferenceMajMin_Description", null);
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06003328 RID: 13096 RVA: 0x000F5D77 File Offset: 0x000F3F77
		internal static string WRN_UnifyReferenceBldRev
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnifyReferenceBldRev", null);
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06003329 RID: 13097 RVA: 0x000F5D84 File Offset: 0x000F3F84
		internal static string WRN_UnifyReferenceBldRev_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnifyReferenceBldRev_Title", null);
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x0600332A RID: 13098 RVA: 0x000F5D91 File Offset: 0x000F3F91
		internal static string WRN_UnifyReferenceBldRev_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnifyReferenceBldRev_Description", null);
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x0600332B RID: 13099 RVA: 0x000F5D9E File Offset: 0x000F3F9E
		internal static string ERR_DuplicateImport
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateImport", null);
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x0600332C RID: 13100 RVA: 0x000F5DAB File Offset: 0x000F3FAB
		internal static string ERR_DuplicateImportSimple
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateImportSimple", null);
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x0600332D RID: 13101 RVA: 0x000F5DB8 File Offset: 0x000F3FB8
		internal static string ERR_AssemblyMatchBadVersion
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AssemblyMatchBadVersion", null);
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x0600332E RID: 13102 RVA: 0x000F5DC5 File Offset: 0x000F3FC5
		internal static string ERR_FixedNeedsLvalue
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FixedNeedsLvalue", null);
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x0600332F RID: 13103 RVA: 0x000F5DD2 File Offset: 0x000F3FD2
		internal static string WRN_DuplicateTypeParamTag
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DuplicateTypeParamTag", null);
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06003330 RID: 13104 RVA: 0x000F5DDF File Offset: 0x000F3FDF
		internal static string WRN_DuplicateTypeParamTag_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DuplicateTypeParamTag_Title", null);
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06003331 RID: 13105 RVA: 0x000F5DEC File Offset: 0x000F3FEC
		internal static string WRN_UnmatchedTypeParamTag
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnmatchedTypeParamTag", null);
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x000F5DF9 File Offset: 0x000F3FF9
		internal static string WRN_UnmatchedTypeParamTag_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnmatchedTypeParamTag_Title", null);
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06003333 RID: 13107 RVA: 0x000F5E06 File Offset: 0x000F4006
		internal static string WRN_UnmatchedTypeParamRefTag
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnmatchedTypeParamRefTag", null);
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06003334 RID: 13108 RVA: 0x000F5E13 File Offset: 0x000F4013
		internal static string WRN_UnmatchedTypeParamRefTag_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnmatchedTypeParamRefTag_Title", null);
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06003335 RID: 13109 RVA: 0x000F5E20 File Offset: 0x000F4020
		internal static string WRN_MissingTypeParamTag
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MissingTypeParamTag", null);
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x000F5E2D File Offset: 0x000F402D
		internal static string WRN_MissingTypeParamTag_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MissingTypeParamTag_Title", null);
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06003337 RID: 13111 RVA: 0x000F5E3A File Offset: 0x000F403A
		internal static string ERR_CantChangeTypeOnOverride
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantChangeTypeOnOverride", null);
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06003338 RID: 13112 RVA: 0x000F5E47 File Offset: 0x000F4047
		internal static string ERR_DoNotUseFixedBufferAttr
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DoNotUseFixedBufferAttr", null);
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06003339 RID: 13113 RVA: 0x000F5E54 File Offset: 0x000F4054
		internal static string ERR_DoNotUseFixedBufferAttrOnProperty
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DoNotUseFixedBufferAttrOnProperty", null);
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x0600333A RID: 13114 RVA: 0x000F5E61 File Offset: 0x000F4061
		internal static string WRN_AssignmentToSelf
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AssignmentToSelf", null);
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600333B RID: 13115 RVA: 0x000F5E6E File Offset: 0x000F406E
		internal static string WRN_AssignmentToSelf_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AssignmentToSelf_Title", null);
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x000F5E7B File Offset: 0x000F407B
		internal static string WRN_ComparisonToSelf
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ComparisonToSelf", null);
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x0600333D RID: 13117 RVA: 0x000F5E88 File Offset: 0x000F4088
		internal static string WRN_ComparisonToSelf_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ComparisonToSelf_Title", null);
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x0600333E RID: 13118 RVA: 0x000F5E95 File Offset: 0x000F4095
		internal static string ERR_CantOpenWin32Res
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantOpenWin32Res", null);
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x000F5EA2 File Offset: 0x000F40A2
		internal static string WRN_DotOnDefault
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DotOnDefault", null);
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06003340 RID: 13120 RVA: 0x000F5EAF File Offset: 0x000F40AF
		internal static string WRN_DotOnDefault_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DotOnDefault_Title", null);
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06003341 RID: 13121 RVA: 0x000F5EBC File Offset: 0x000F40BC
		internal static string ERR_NoMultipleInheritance
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoMultipleInheritance", null);
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06003342 RID: 13122 RVA: 0x000F5EC9 File Offset: 0x000F40C9
		internal static string ERR_BaseClassMustBeFirst
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BaseClassMustBeFirst", null);
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06003343 RID: 13123 RVA: 0x000F5ED6 File Offset: 0x000F40D6
		internal static string WRN_BadXMLRefTypeVar
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadXMLRefTypeVar", null);
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06003344 RID: 13124 RVA: 0x000F5EE3 File Offset: 0x000F40E3
		internal static string WRN_BadXMLRefTypeVar_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadXMLRefTypeVar_Title", null);
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06003345 RID: 13125 RVA: 0x000F5EF0 File Offset: 0x000F40F0
		internal static string ERR_FriendAssemblyBadArgs
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FriendAssemblyBadArgs", null);
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06003346 RID: 13126 RVA: 0x000F5EFD File Offset: 0x000F40FD
		internal static string ERR_FriendAssemblySNReq
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FriendAssemblySNReq", null);
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06003347 RID: 13127 RVA: 0x000F5F0A File Offset: 0x000F410A
		internal static string ERR_DelegateOnNullable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DelegateOnNullable", null);
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06003348 RID: 13128 RVA: 0x000F5F17 File Offset: 0x000F4117
		internal static string ERR_BadCtorArgCount
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadCtorArgCount", null);
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06003349 RID: 13129 RVA: 0x000F5F24 File Offset: 0x000F4124
		internal static string ERR_GlobalAttributesNotFirst
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GlobalAttributesNotFirst", null);
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x0600334A RID: 13130 RVA: 0x000F5F31 File Offset: 0x000F4131
		internal static string ERR_ExpressionExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionExpected", null);
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x0600334B RID: 13131 RVA: 0x000F5F3E File Offset: 0x000F413E
		internal static string ERR_InvalidSubsystemVersion
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidSubsystemVersion", null);
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x0600334C RID: 13132 RVA: 0x000F5F4B File Offset: 0x000F414B
		internal static string ERR_InteropMethodWithBody
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InteropMethodWithBody", null);
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x0600334D RID: 13133 RVA: 0x000F5F58 File Offset: 0x000F4158
		internal static string ERR_BadWarningLevel
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadWarningLevel", null);
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x0600334E RID: 13134 RVA: 0x000F5F65 File Offset: 0x000F4165
		internal static string ERR_BadDebugType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadDebugType", null);
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x0600334F RID: 13135 RVA: 0x000F5F72 File Offset: 0x000F4172
		internal static string ERR_BadResourceVis
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadResourceVis", null);
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06003350 RID: 13136 RVA: 0x000F5F7F File Offset: 0x000F417F
		internal static string ERR_DefaultValueTypeMustMatch
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DefaultValueTypeMustMatch", null);
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06003351 RID: 13137 RVA: 0x000F5F8C File Offset: 0x000F418C
		internal static string ERR_DefaultValueBadValueType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DefaultValueBadValueType", null);
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06003352 RID: 13138 RVA: 0x000F5F99 File Offset: 0x000F4199
		internal static string ERR_MemberAlreadyInitialized
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MemberAlreadyInitialized", null);
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06003353 RID: 13139 RVA: 0x000F5FA6 File Offset: 0x000F41A6
		internal static string ERR_MemberCannotBeInitialized
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MemberCannotBeInitialized", null);
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06003354 RID: 13140 RVA: 0x000F5FB3 File Offset: 0x000F41B3
		internal static string ERR_StaticMemberInObjectInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StaticMemberInObjectInitializer", null);
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06003355 RID: 13141 RVA: 0x000F5FC0 File Offset: 0x000F41C0
		internal static string ERR_ReadonlyValueTypeInObjectInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ReadonlyValueTypeInObjectInitializer", null);
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06003356 RID: 13142 RVA: 0x000F5FCD File Offset: 0x000F41CD
		internal static string ERR_ValueTypePropertyInObjectInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ValueTypePropertyInObjectInitializer", null);
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06003357 RID: 13143 RVA: 0x000F5FDA File Offset: 0x000F41DA
		internal static string ERR_UnsafeTypeInObjectCreation
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnsafeTypeInObjectCreation", null);
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06003358 RID: 13144 RVA: 0x000F5FE7 File Offset: 0x000F41E7
		internal static string ERR_EmptyElementInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EmptyElementInitializer", null);
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06003359 RID: 13145 RVA: 0x000F5FF4 File Offset: 0x000F41F4
		internal static string ERR_InitializerAddHasWrongSignature
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InitializerAddHasWrongSignature", null);
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x0600335A RID: 13146 RVA: 0x000F6001 File Offset: 0x000F4201
		internal static string ERR_CollectionInitRequiresIEnumerable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CollectionInitRequiresIEnumerable", null);
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x0600335B RID: 13147 RVA: 0x000F600E File Offset: 0x000F420E
		internal static string ERR_CantSetWin32Manifest
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantSetWin32Manifest", null);
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x000F601B File Offset: 0x000F421B
		internal static string WRN_CantHaveManifestForModule
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CantHaveManifestForModule", null);
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x0600335D RID: 13149 RVA: 0x000F6028 File Offset: 0x000F4228
		internal static string WRN_CantHaveManifestForModule_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CantHaveManifestForModule_Title", null);
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x0600335E RID: 13150 RVA: 0x000F6035 File Offset: 0x000F4235
		internal static string ERR_BadInstanceArgType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadInstanceArgType", null);
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x0600335F RID: 13151 RVA: 0x000F6042 File Offset: 0x000F4242
		internal static string ERR_QueryDuplicateRangeVariable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_QueryDuplicateRangeVariable", null);
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06003360 RID: 13152 RVA: 0x000F604F File Offset: 0x000F424F
		internal static string ERR_QueryRangeVariableOverrides
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_QueryRangeVariableOverrides", null);
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06003361 RID: 13153 RVA: 0x000F605C File Offset: 0x000F425C
		internal static string ERR_QueryRangeVariableAssignedBadValue
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_QueryRangeVariableAssignedBadValue", null);
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06003362 RID: 13154 RVA: 0x000F6069 File Offset: 0x000F4269
		internal static string ERR_QueryNoProviderCastable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_QueryNoProviderCastable", null);
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06003363 RID: 13155 RVA: 0x000F6076 File Offset: 0x000F4276
		internal static string ERR_QueryNoProviderStandard
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_QueryNoProviderStandard", null);
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06003364 RID: 13156 RVA: 0x000F6083 File Offset: 0x000F4283
		internal static string ERR_QueryNoProvider
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_QueryNoProvider", null);
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06003365 RID: 13157 RVA: 0x000F6090 File Offset: 0x000F4290
		internal static string ERR_QueryOuterKey
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_QueryOuterKey", null);
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06003366 RID: 13158 RVA: 0x000F609D File Offset: 0x000F429D
		internal static string ERR_QueryInnerKey
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_QueryInnerKey", null);
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06003367 RID: 13159 RVA: 0x000F60AA File Offset: 0x000F42AA
		internal static string ERR_QueryOutRefRangeVariable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_QueryOutRefRangeVariable", null);
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06003368 RID: 13160 RVA: 0x000F60B7 File Offset: 0x000F42B7
		internal static string ERR_QueryMultipleProviders
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_QueryMultipleProviders", null);
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06003369 RID: 13161 RVA: 0x000F60C4 File Offset: 0x000F42C4
		internal static string ERR_QueryTypeInferenceFailedMulti
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_QueryTypeInferenceFailedMulti", null);
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x0600336A RID: 13162 RVA: 0x000F60D1 File Offset: 0x000F42D1
		internal static string ERR_QueryTypeInferenceFailed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_QueryTypeInferenceFailed", null);
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x0600336B RID: 13163 RVA: 0x000F60DE File Offset: 0x000F42DE
		internal static string ERR_QueryTypeInferenceFailedSelectMany
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_QueryTypeInferenceFailedSelectMany", null);
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x0600336C RID: 13164 RVA: 0x000F60EB File Offset: 0x000F42EB
		internal static string ERR_ExpressionTreeContainsPointerOp
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsPointerOp", null);
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x0600336D RID: 13165 RVA: 0x000F60F8 File Offset: 0x000F42F8
		internal static string ERR_ExpressionTreeContainsAnonymousMethod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsAnonymousMethod", null);
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x000F6105 File Offset: 0x000F4305
		internal static string ERR_AnonymousMethodToExpressionTree
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AnonymousMethodToExpressionTree", null);
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x0600336F RID: 13167 RVA: 0x000F6112 File Offset: 0x000F4312
		internal static string ERR_QueryRangeVariableReadOnly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_QueryRangeVariableReadOnly", null);
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x000F611F File Offset: 0x000F431F
		internal static string ERR_QueryRangeVariableSameAsTypeParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_QueryRangeVariableSameAsTypeParam", null);
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06003371 RID: 13169 RVA: 0x000F612C File Offset: 0x000F432C
		internal static string ERR_TypeVarNotFoundRangeVariable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TypeVarNotFoundRangeVariable", null);
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06003372 RID: 13170 RVA: 0x000F6139 File Offset: 0x000F4339
		internal static string ERR_BadArgTypesForCollectionAdd
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadArgTypesForCollectionAdd", null);
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06003373 RID: 13171 RVA: 0x000F6146 File Offset: 0x000F4346
		internal static string ERR_ByRefParameterInExpressionTree
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ByRefParameterInExpressionTree", null);
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06003374 RID: 13172 RVA: 0x000F6153 File Offset: 0x000F4353
		internal static string ERR_VarArgsInExpressionTree
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_VarArgsInExpressionTree", null);
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06003375 RID: 13173 RVA: 0x000F6160 File Offset: 0x000F4360
		internal static string ERR_MemGroupInExpressionTree
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MemGroupInExpressionTree", null);
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06003376 RID: 13174 RVA: 0x000F616D File Offset: 0x000F436D
		internal static string ERR_InitializerAddHasParamModifiers
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InitializerAddHasParamModifiers", null);
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06003377 RID: 13175 RVA: 0x000F617A File Offset: 0x000F437A
		internal static string ERR_NonInvocableMemberCalled
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NonInvocableMemberCalled", null);
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06003378 RID: 13176 RVA: 0x000F6187 File Offset: 0x000F4387
		internal static string WRN_MultipleRuntimeImplementationMatches
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MultipleRuntimeImplementationMatches", null);
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06003379 RID: 13177 RVA: 0x000F6194 File Offset: 0x000F4394
		internal static string WRN_MultipleRuntimeImplementationMatches_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MultipleRuntimeImplementationMatches_Title", null);
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x0600337A RID: 13178 RVA: 0x000F61A1 File Offset: 0x000F43A1
		internal static string WRN_MultipleRuntimeImplementationMatches_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MultipleRuntimeImplementationMatches_Description", null);
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x0600337B RID: 13179 RVA: 0x000F61AE File Offset: 0x000F43AE
		internal static string WRN_MultipleRuntimeOverrideMatches
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MultipleRuntimeOverrideMatches", null);
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x0600337C RID: 13180 RVA: 0x000F61BB File Offset: 0x000F43BB
		internal static string WRN_MultipleRuntimeOverrideMatches_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MultipleRuntimeOverrideMatches_Title", null);
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x0600337D RID: 13181 RVA: 0x000F61C8 File Offset: 0x000F43C8
		internal static string ERR_ObjectOrCollectionInitializerWithDelegateCreation
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ObjectOrCollectionInitializerWithDelegateCreation", null);
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x0600337E RID: 13182 RVA: 0x000F61D5 File Offset: 0x000F43D5
		internal static string ERR_InvalidConstantDeclarationType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidConstantDeclarationType", null);
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x0600337F RID: 13183 RVA: 0x000F61E2 File Offset: 0x000F43E2
		internal static string ERR_FileNotFound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FileNotFound", null);
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06003380 RID: 13184 RVA: 0x000F61EF File Offset: 0x000F43EF
		internal static string WRN_FileAlreadyIncluded
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_FileAlreadyIncluded", null);
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06003381 RID: 13185 RVA: 0x000F61FC File Offset: 0x000F43FC
		internal static string WRN_FileAlreadyIncluded_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_FileAlreadyIncluded_Title", null);
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06003382 RID: 13186 RVA: 0x000F6209 File Offset: 0x000F4409
		internal static string ERR_NoFileSpec
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoFileSpec", null);
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06003383 RID: 13187 RVA: 0x000F6216 File Offset: 0x000F4416
		internal static string ERR_SwitchNeedsString
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SwitchNeedsString", null);
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06003384 RID: 13188 RVA: 0x000F6223 File Offset: 0x000F4423
		internal static string ERR_BadSwitch
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadSwitch", null);
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06003385 RID: 13189 RVA: 0x000F6230 File Offset: 0x000F4430
		internal static string WRN_NoSources
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NoSources", null);
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06003386 RID: 13190 RVA: 0x000F623D File Offset: 0x000F443D
		internal static string WRN_NoSources_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NoSources_Title", null);
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06003387 RID: 13191 RVA: 0x000F624A File Offset: 0x000F444A
		internal static string ERR_ExpectedSingleScript
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpectedSingleScript", null);
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06003388 RID: 13192 RVA: 0x000F6257 File Offset: 0x000F4457
		internal static string ERR_OpenResponseFile
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OpenResponseFile", null);
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06003389 RID: 13193 RVA: 0x000F6264 File Offset: 0x000F4464
		internal static string ERR_CantOpenFileWrite
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantOpenFileWrite", null);
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x0600338A RID: 13194 RVA: 0x000F6271 File Offset: 0x000F4471
		internal static string ERR_BadBaseNumber
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadBaseNumber", null);
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x0600338B RID: 13195 RVA: 0x000F627E File Offset: 0x000F447E
		internal static string ERR_BinaryFile
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BinaryFile", null);
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x0600338C RID: 13196 RVA: 0x000F628B File Offset: 0x000F448B
		internal static string FTL_BadCodepage
		{
			get
			{
				return CSharpResources.GetResourceString("FTL_BadCodepage", null);
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x0600338D RID: 13197 RVA: 0x000F6298 File Offset: 0x000F4498
		internal static string FTL_BadChecksumAlgorithm
		{
			get
			{
				return CSharpResources.GetResourceString("FTL_BadChecksumAlgorithm", null);
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x0600338E RID: 13198 RVA: 0x000F62A5 File Offset: 0x000F44A5
		internal static string ERR_NoMainOnDLL
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoMainOnDLL", null);
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x0600338F RID: 13199 RVA: 0x000F62B2 File Offset: 0x000F44B2
		internal static string FTL_InvalidTarget
		{
			get
			{
				return CSharpResources.GetResourceString("FTL_InvalidTarget", null);
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06003390 RID: 13200 RVA: 0x000F62BF File Offset: 0x000F44BF
		internal static string FTL_InvalidInputFileName
		{
			get
			{
				return CSharpResources.GetResourceString("FTL_InvalidInputFileName", null);
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06003391 RID: 13201 RVA: 0x000F62CC File Offset: 0x000F44CC
		internal static string WRN_NoConfigNotOnCommandLine
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NoConfigNotOnCommandLine", null);
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06003392 RID: 13202 RVA: 0x000F62D9 File Offset: 0x000F44D9
		internal static string WRN_NoConfigNotOnCommandLine_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NoConfigNotOnCommandLine_Title", null);
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06003393 RID: 13203 RVA: 0x000F62E6 File Offset: 0x000F44E6
		internal static string ERR_InvalidFileAlignment
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidFileAlignment", null);
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06003394 RID: 13204 RVA: 0x000F62F3 File Offset: 0x000F44F3
		internal static string ERR_InvalidOutputName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidOutputName", null);
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06003395 RID: 13205 RVA: 0x000F6300 File Offset: 0x000F4500
		internal static string ERR_InvalidDebugInformationFormat
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidDebugInformationFormat", null);
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06003396 RID: 13206 RVA: 0x000F630D File Offset: 0x000F450D
		internal static string ERR_LegacyObjectIdSyntax
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LegacyObjectIdSyntax", null);
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06003397 RID: 13207 RVA: 0x000F631A File Offset: 0x000F451A
		internal static string WRN_DefineIdentifierRequired
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DefineIdentifierRequired", null);
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06003398 RID: 13208 RVA: 0x000F6327 File Offset: 0x000F4527
		internal static string WRN_DefineIdentifierRequired_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DefineIdentifierRequired_Title", null);
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06003399 RID: 13209 RVA: 0x000F6334 File Offset: 0x000F4534
		internal static string FTL_OutputFileExists
		{
			get
			{
				return CSharpResources.GetResourceString("FTL_OutputFileExists", null);
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x0600339A RID: 13210 RVA: 0x000F6341 File Offset: 0x000F4541
		internal static string ERR_OneAliasPerReference
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OneAliasPerReference", null);
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x000F634E File Offset: 0x000F454E
		internal static string ERR_SwitchNeedsNumber
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SwitchNeedsNumber", null);
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x000F635B File Offset: 0x000F455B
		internal static string ERR_MissingDebugSwitch
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MissingDebugSwitch", null);
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x0600339D RID: 13213 RVA: 0x000F6368 File Offset: 0x000F4568
		internal static string ERR_ComRefCallInExpressionTree
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ComRefCallInExpressionTree", null);
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600339E RID: 13214 RVA: 0x000F6375 File Offset: 0x000F4575
		internal static string ERR_InvalidFormatForGuidForOption
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidFormatForGuidForOption", null);
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600339F RID: 13215 RVA: 0x000F6382 File Offset: 0x000F4582
		internal static string ERR_MissingGuidForOption
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MissingGuidForOption", null);
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x060033A0 RID: 13216 RVA: 0x000F638F File Offset: 0x000F458F
		internal static string WRN_CLS_NoVarArgs
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_NoVarArgs", null);
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x060033A1 RID: 13217 RVA: 0x000F639C File Offset: 0x000F459C
		internal static string WRN_CLS_NoVarArgs_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_NoVarArgs_Title", null);
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x060033A2 RID: 13218 RVA: 0x000F63A9 File Offset: 0x000F45A9
		internal static string WRN_CLS_BadArgType
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadArgType", null);
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x060033A3 RID: 13219 RVA: 0x000F63B6 File Offset: 0x000F45B6
		internal static string WRN_CLS_BadArgType_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadArgType_Title", null);
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x060033A4 RID: 13220 RVA: 0x000F63C3 File Offset: 0x000F45C3
		internal static string WRN_CLS_BadReturnType
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadReturnType", null);
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x060033A5 RID: 13221 RVA: 0x000F63D0 File Offset: 0x000F45D0
		internal static string WRN_CLS_BadReturnType_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadReturnType_Title", null);
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x060033A6 RID: 13222 RVA: 0x000F63DD File Offset: 0x000F45DD
		internal static string WRN_CLS_BadFieldPropType
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadFieldPropType", null);
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x060033A7 RID: 13223 RVA: 0x000F63EA File Offset: 0x000F45EA
		internal static string WRN_CLS_BadFieldPropType_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadFieldPropType_Title", null);
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x060033A8 RID: 13224 RVA: 0x000F63F7 File Offset: 0x000F45F7
		internal static string WRN_CLS_BadFieldPropType_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadFieldPropType_Description", null);
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x060033A9 RID: 13225 RVA: 0x000F6404 File Offset: 0x000F4604
		internal static string WRN_CLS_BadIdentifierCase
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadIdentifierCase", null);
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x060033AA RID: 13226 RVA: 0x000F6411 File Offset: 0x000F4611
		internal static string WRN_CLS_BadIdentifierCase_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadIdentifierCase_Title", null);
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x060033AB RID: 13227 RVA: 0x000F641E File Offset: 0x000F461E
		internal static string WRN_CLS_OverloadRefOut
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_OverloadRefOut", null);
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x060033AC RID: 13228 RVA: 0x000F642B File Offset: 0x000F462B
		internal static string WRN_CLS_OverloadRefOut_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_OverloadRefOut_Title", null);
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x060033AD RID: 13229 RVA: 0x000F6438 File Offset: 0x000F4638
		internal static string WRN_CLS_OverloadUnnamed
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_OverloadUnnamed", null);
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x060033AE RID: 13230 RVA: 0x000F6445 File Offset: 0x000F4645
		internal static string WRN_CLS_OverloadUnnamed_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_OverloadUnnamed_Title", null);
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x060033AF RID: 13231 RVA: 0x000F6452 File Offset: 0x000F4652
		internal static string WRN_CLS_OverloadUnnamed_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_OverloadUnnamed_Description", null);
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x060033B0 RID: 13232 RVA: 0x000F645F File Offset: 0x000F465F
		internal static string WRN_CLS_BadIdentifier
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadIdentifier", null);
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x060033B1 RID: 13233 RVA: 0x000F646C File Offset: 0x000F466C
		internal static string WRN_CLS_BadIdentifier_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadIdentifier_Title", null);
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x060033B2 RID: 13234 RVA: 0x000F6479 File Offset: 0x000F4679
		internal static string WRN_CLS_BadBase
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadBase", null);
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x060033B3 RID: 13235 RVA: 0x000F6486 File Offset: 0x000F4686
		internal static string WRN_CLS_BadBase_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadBase_Title", null);
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x060033B4 RID: 13236 RVA: 0x000F6493 File Offset: 0x000F4693
		internal static string WRN_CLS_BadBase_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadBase_Description", null);
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x060033B5 RID: 13237 RVA: 0x000F64A0 File Offset: 0x000F46A0
		internal static string WRN_CLS_BadInterfaceMember
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadInterfaceMember", null);
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x060033B6 RID: 13238 RVA: 0x000F64AD File Offset: 0x000F46AD
		internal static string WRN_CLS_BadInterfaceMember_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadInterfaceMember_Title", null);
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x060033B7 RID: 13239 RVA: 0x000F64BA File Offset: 0x000F46BA
		internal static string WRN_CLS_NoAbstractMembers
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_NoAbstractMembers", null);
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x060033B8 RID: 13240 RVA: 0x000F64C7 File Offset: 0x000F46C7
		internal static string WRN_CLS_NoAbstractMembers_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_NoAbstractMembers_Title", null);
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x060033B9 RID: 13241 RVA: 0x000F64D4 File Offset: 0x000F46D4
		internal static string WRN_CLS_NotOnModules
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_NotOnModules", null);
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x060033BA RID: 13242 RVA: 0x000F64E1 File Offset: 0x000F46E1
		internal static string WRN_CLS_NotOnModules_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_NotOnModules_Title", null);
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060033BB RID: 13243 RVA: 0x000F64EE File Offset: 0x000F46EE
		internal static string WRN_CLS_ModuleMissingCLS
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_ModuleMissingCLS", null);
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060033BC RID: 13244 RVA: 0x000F64FB File Offset: 0x000F46FB
		internal static string WRN_CLS_ModuleMissingCLS_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_ModuleMissingCLS_Title", null);
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060033BD RID: 13245 RVA: 0x000F6508 File Offset: 0x000F4708
		internal static string WRN_CLS_AssemblyNotCLS
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_AssemblyNotCLS", null);
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x060033BE RID: 13246 RVA: 0x000F6515 File Offset: 0x000F4715
		internal static string WRN_CLS_AssemblyNotCLS_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_AssemblyNotCLS_Title", null);
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060033BF RID: 13247 RVA: 0x000F6522 File Offset: 0x000F4722
		internal static string WRN_CLS_BadAttributeType
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadAttributeType", null);
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x060033C0 RID: 13248 RVA: 0x000F652F File Offset: 0x000F472F
		internal static string WRN_CLS_BadAttributeType_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadAttributeType_Title", null);
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060033C1 RID: 13249 RVA: 0x000F653C File Offset: 0x000F473C
		internal static string WRN_CLS_ArrayArgumentToAttribute
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_ArrayArgumentToAttribute", null);
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060033C2 RID: 13250 RVA: 0x000F6549 File Offset: 0x000F4749
		internal static string WRN_CLS_ArrayArgumentToAttribute_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_ArrayArgumentToAttribute_Title", null);
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x060033C3 RID: 13251 RVA: 0x000F6556 File Offset: 0x000F4756
		internal static string WRN_CLS_NotOnModules2
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_NotOnModules2", null);
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x060033C4 RID: 13252 RVA: 0x000F6563 File Offset: 0x000F4763
		internal static string WRN_CLS_NotOnModules2_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_NotOnModules2_Title", null);
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x060033C5 RID: 13253 RVA: 0x000F6570 File Offset: 0x000F4770
		internal static string WRN_CLS_IllegalTrueInFalse
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_IllegalTrueInFalse", null);
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x060033C6 RID: 13254 RVA: 0x000F657D File Offset: 0x000F477D
		internal static string WRN_CLS_IllegalTrueInFalse_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_IllegalTrueInFalse_Title", null);
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x060033C7 RID: 13255 RVA: 0x000F658A File Offset: 0x000F478A
		internal static string WRN_CLS_MeaninglessOnPrivateType
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_MeaninglessOnPrivateType", null);
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x060033C8 RID: 13256 RVA: 0x000F6597 File Offset: 0x000F4797
		internal static string WRN_CLS_MeaninglessOnPrivateType_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_MeaninglessOnPrivateType_Title", null);
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x060033C9 RID: 13257 RVA: 0x000F65A4 File Offset: 0x000F47A4
		internal static string WRN_CLS_AssemblyNotCLS2
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_AssemblyNotCLS2", null);
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x060033CA RID: 13258 RVA: 0x000F65B1 File Offset: 0x000F47B1
		internal static string WRN_CLS_AssemblyNotCLS2_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_AssemblyNotCLS2_Title", null);
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x060033CB RID: 13259 RVA: 0x000F65BE File Offset: 0x000F47BE
		internal static string WRN_CLS_MeaninglessOnParam
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_MeaninglessOnParam", null);
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x060033CC RID: 13260 RVA: 0x000F65CB File Offset: 0x000F47CB
		internal static string WRN_CLS_MeaninglessOnParam_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_MeaninglessOnParam_Title", null);
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060033CD RID: 13261 RVA: 0x000F65D8 File Offset: 0x000F47D8
		internal static string WRN_CLS_MeaninglessOnReturn
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_MeaninglessOnReturn", null);
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060033CE RID: 13262 RVA: 0x000F65E5 File Offset: 0x000F47E5
		internal static string WRN_CLS_MeaninglessOnReturn_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_MeaninglessOnReturn_Title", null);
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x060033CF RID: 13263 RVA: 0x000F65F2 File Offset: 0x000F47F2
		internal static string WRN_CLS_BadTypeVar
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadTypeVar", null);
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x060033D0 RID: 13264 RVA: 0x000F65FF File Offset: 0x000F47FF
		internal static string WRN_CLS_BadTypeVar_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadTypeVar_Title", null);
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x060033D1 RID: 13265 RVA: 0x000F660C File Offset: 0x000F480C
		internal static string WRN_CLS_VolatileField
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_VolatileField", null);
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060033D2 RID: 13266 RVA: 0x000F6619 File Offset: 0x000F4819
		internal static string WRN_CLS_VolatileField_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_VolatileField_Title", null);
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x060033D3 RID: 13267 RVA: 0x000F6626 File Offset: 0x000F4826
		internal static string WRN_CLS_BadInterface
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadInterface", null);
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x060033D4 RID: 13268 RVA: 0x000F6633 File Offset: 0x000F4833
		internal static string WRN_CLS_BadInterface_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CLS_BadInterface_Title", null);
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x060033D5 RID: 13269 RVA: 0x000F6640 File Offset: 0x000F4840
		internal static string ERR_BadAwaitArg
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAwaitArg", null);
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060033D6 RID: 13270 RVA: 0x000F664D File Offset: 0x000F484D
		internal static string ERR_BadAwaitArgIntrinsic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAwaitArgIntrinsic", null);
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060033D7 RID: 13271 RVA: 0x000F665A File Offset: 0x000F485A
		internal static string ERR_BadAwaiterPattern
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAwaiterPattern", null);
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060033D8 RID: 13272 RVA: 0x000F6667 File Offset: 0x000F4867
		internal static string ERR_BadAwaitArg_NeedSystem
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAwaitArg_NeedSystem", null);
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060033D9 RID: 13273 RVA: 0x000F6674 File Offset: 0x000F4874
		internal static string ERR_BadAwaitArgVoidCall
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAwaitArgVoidCall", null);
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x060033DA RID: 13274 RVA: 0x000F6681 File Offset: 0x000F4881
		internal static string ERR_BadAwaitAsIdentifier
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAwaitAsIdentifier", null);
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x060033DB RID: 13275 RVA: 0x000F668E File Offset: 0x000F488E
		internal static string ERR_DoesntImplementAwaitInterface
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DoesntImplementAwaitInterface", null);
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060033DC RID: 13276 RVA: 0x000F669B File Offset: 0x000F489B
		internal static string ERR_TaskRetNoObjectRequired
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TaskRetNoObjectRequired", null);
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x060033DD RID: 13277 RVA: 0x000F66A8 File Offset: 0x000F48A8
		internal static string ERR_BadAsyncReturn
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAsyncReturn", null);
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x060033DE RID: 13278 RVA: 0x000F66B5 File Offset: 0x000F48B5
		internal static string ERR_CantReturnVoid
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantReturnVoid", null);
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x060033DF RID: 13279 RVA: 0x000F66C2 File Offset: 0x000F48C2
		internal static string ERR_VarargsAsync
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_VarargsAsync", null);
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x060033E0 RID: 13280 RVA: 0x000F66CF File Offset: 0x000F48CF
		internal static string ERR_ByRefTypeAndAwait
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ByRefTypeAndAwait", null);
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x000F66DC File Offset: 0x000F48DC
		internal static string ERR_UnsafeAsyncArgType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnsafeAsyncArgType", null);
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x060033E2 RID: 13282 RVA: 0x000F66E9 File Offset: 0x000F48E9
		internal static string ERR_BadAsyncArgType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAsyncArgType", null);
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x000F66F6 File Offset: 0x000F48F6
		internal static string ERR_BadAwaitWithoutAsync
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAwaitWithoutAsync", null);
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x060033E4 RID: 13284 RVA: 0x000F6703 File Offset: 0x000F4903
		internal static string ERR_BadAwaitWithoutAsyncLambda
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAwaitWithoutAsyncLambda", null);
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x060033E5 RID: 13285 RVA: 0x000F6710 File Offset: 0x000F4910
		internal static string ERR_BadAwaitWithoutAsyncMethod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAwaitWithoutAsyncMethod", null);
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x060033E6 RID: 13286 RVA: 0x000F671D File Offset: 0x000F491D
		internal static string ERR_BadAwaitWithoutVoidAsyncMethod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAwaitWithoutVoidAsyncMethod", null);
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x060033E7 RID: 13287 RVA: 0x000F672A File Offset: 0x000F492A
		internal static string ERR_BadAwaitInFinally
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAwaitInFinally", null);
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x060033E8 RID: 13288 RVA: 0x000F6737 File Offset: 0x000F4937
		internal static string ERR_BadAwaitInCatch
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAwaitInCatch", null);
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x060033E9 RID: 13289 RVA: 0x000F6744 File Offset: 0x000F4944
		internal static string ERR_BadAwaitInCatchFilter
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAwaitInCatchFilter", null);
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x060033EA RID: 13290 RVA: 0x000F6751 File Offset: 0x000F4951
		internal static string ERR_BadAwaitInLock
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAwaitInLock", null);
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x060033EB RID: 13291 RVA: 0x000F675E File Offset: 0x000F495E
		internal static string ERR_BadAwaitInStaticVariableInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAwaitInStaticVariableInitializer", null);
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x060033EC RID: 13292 RVA: 0x000F676B File Offset: 0x000F496B
		internal static string ERR_AwaitInUnsafeContext
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AwaitInUnsafeContext", null);
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x060033ED RID: 13293 RVA: 0x000F6778 File Offset: 0x000F4978
		internal static string ERR_BadAsyncLacksBody
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAsyncLacksBody", null);
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x060033EE RID: 13294 RVA: 0x000F6785 File Offset: 0x000F4985
		internal static string ERR_BadSpecialByRefLocal
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadSpecialByRefLocal", null);
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x060033EF RID: 13295 RVA: 0x000F6792 File Offset: 0x000F4992
		internal static string ERR_BadSpecialByRefIterator
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadSpecialByRefIterator", null);
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x060033F0 RID: 13296 RVA: 0x000F679F File Offset: 0x000F499F
		internal static string ERR_SecurityCriticalOrSecuritySafeCriticalOnAsync
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SecurityCriticalOrSecuritySafeCriticalOnAsync", null);
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060033F1 RID: 13297 RVA: 0x000F67AC File Offset: 0x000F49AC
		internal static string ERR_SecurityCriticalOrSecuritySafeCriticalOnAsyncInClassOrStruct
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SecurityCriticalOrSecuritySafeCriticalOnAsyncInClassOrStruct", null);
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x060033F2 RID: 13298 RVA: 0x000F67B9 File Offset: 0x000F49B9
		internal static string ERR_BadAwaitInQuery
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAwaitInQuery", null);
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x060033F3 RID: 13299 RVA: 0x000F67C6 File Offset: 0x000F49C6
		internal static string WRN_AsyncLacksAwaits
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AsyncLacksAwaits", null);
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x060033F4 RID: 13300 RVA: 0x000F67D3 File Offset: 0x000F49D3
		internal static string WRN_AsyncLacksAwaits_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AsyncLacksAwaits_Title", null);
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x060033F5 RID: 13301 RVA: 0x000F67E0 File Offset: 0x000F49E0
		internal static string WRN_UnobservedAwaitableExpression
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnobservedAwaitableExpression", null);
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x060033F6 RID: 13302 RVA: 0x000F67ED File Offset: 0x000F49ED
		internal static string WRN_UnobservedAwaitableExpression_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnobservedAwaitableExpression_Title", null);
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x060033F7 RID: 13303 RVA: 0x000F67FA File Offset: 0x000F49FA
		internal static string WRN_UnobservedAwaitableExpression_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnobservedAwaitableExpression_Description", null);
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x060033F8 RID: 13304 RVA: 0x000F6807 File Offset: 0x000F4A07
		internal static string ERR_SynchronizedAsyncMethod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SynchronizedAsyncMethod", null);
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x060033F9 RID: 13305 RVA: 0x000F6814 File Offset: 0x000F4A14
		internal static string ERR_NoConversionForCallerLineNumberParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoConversionForCallerLineNumberParam", null);
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x060033FA RID: 13306 RVA: 0x000F6821 File Offset: 0x000F4A21
		internal static string ERR_NoConversionForCallerFilePathParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoConversionForCallerFilePathParam", null);
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x060033FB RID: 13307 RVA: 0x000F682E File Offset: 0x000F4A2E
		internal static string ERR_NoConversionForCallerMemberNameParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoConversionForCallerMemberNameParam", null);
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x060033FC RID: 13308 RVA: 0x000F683B File Offset: 0x000F4A3B
		internal static string ERR_BadCallerLineNumberParamWithoutDefaultValue
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadCallerLineNumberParamWithoutDefaultValue", null);
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x060033FD RID: 13309 RVA: 0x000F6848 File Offset: 0x000F4A48
		internal static string ERR_BadCallerFilePathParamWithoutDefaultValue
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadCallerFilePathParamWithoutDefaultValue", null);
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x060033FE RID: 13310 RVA: 0x000F6855 File Offset: 0x000F4A55
		internal static string ERR_BadCallerMemberNameParamWithoutDefaultValue
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadCallerMemberNameParamWithoutDefaultValue", null);
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x060033FF RID: 13311 RVA: 0x000F6862 File Offset: 0x000F4A62
		internal static string WRN_CallerLineNumberParamForUnconsumedLocation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CallerLineNumberParamForUnconsumedLocation", null);
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06003400 RID: 13312 RVA: 0x000F686F File Offset: 0x000F4A6F
		internal static string WRN_CallerLineNumberParamForUnconsumedLocation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CallerLineNumberParamForUnconsumedLocation_Title", null);
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06003401 RID: 13313 RVA: 0x000F687C File Offset: 0x000F4A7C
		internal static string WRN_CallerFilePathParamForUnconsumedLocation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CallerFilePathParamForUnconsumedLocation", null);
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06003402 RID: 13314 RVA: 0x000F6889 File Offset: 0x000F4A89
		internal static string WRN_CallerFilePathParamForUnconsumedLocation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CallerFilePathParamForUnconsumedLocation_Title", null);
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06003403 RID: 13315 RVA: 0x000F6896 File Offset: 0x000F4A96
		internal static string WRN_CallerMemberNameParamForUnconsumedLocation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CallerMemberNameParamForUnconsumedLocation", null);
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06003404 RID: 13316 RVA: 0x000F68A3 File Offset: 0x000F4AA3
		internal static string WRN_CallerMemberNameParamForUnconsumedLocation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CallerMemberNameParamForUnconsumedLocation_Title", null);
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06003405 RID: 13317 RVA: 0x000F68B0 File Offset: 0x000F4AB0
		internal static string ERR_NoEntryPoint
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoEntryPoint", null);
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06003406 RID: 13318 RVA: 0x000F68BD File Offset: 0x000F4ABD
		internal static string ERR_ArrayInitializerIncorrectLength
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ArrayInitializerIncorrectLength", null);
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06003407 RID: 13319 RVA: 0x000F68CA File Offset: 0x000F4ACA
		internal static string ERR_ArrayInitializerExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ArrayInitializerExpected", null);
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06003408 RID: 13320 RVA: 0x000F68D7 File Offset: 0x000F4AD7
		internal static string ERR_IllegalVarianceSyntax
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IllegalVarianceSyntax", null);
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06003409 RID: 13321 RVA: 0x000F68E4 File Offset: 0x000F4AE4
		internal static string ERR_UnexpectedAliasedName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnexpectedAliasedName", null);
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x0600340A RID: 13322 RVA: 0x000F68F1 File Offset: 0x000F4AF1
		internal static string ERR_UnexpectedGenericName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnexpectedGenericName", null);
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x0600340B RID: 13323 RVA: 0x000F68FE File Offset: 0x000F4AFE
		internal static string ERR_UnexpectedUnboundGenericName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnexpectedUnboundGenericName", null);
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x0600340C RID: 13324 RVA: 0x000F690B File Offset: 0x000F4B0B
		internal static string ERR_GlobalStatement
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GlobalStatement", null);
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x0600340D RID: 13325 RVA: 0x000F6918 File Offset: 0x000F4B18
		internal static string ERR_NamedArgumentForArray
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NamedArgumentForArray", null);
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x0600340E RID: 13326 RVA: 0x000F6925 File Offset: 0x000F4B25
		internal static string ERR_NotYetImplementedInRoslyn
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NotYetImplementedInRoslyn", null);
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x0600340F RID: 13327 RVA: 0x000F6932 File Offset: 0x000F4B32
		internal static string ERR_DefaultValueNotAllowed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DefaultValueNotAllowed", null);
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06003410 RID: 13328 RVA: 0x000F693F File Offset: 0x000F4B3F
		internal static string ERR_CantOpenIcon
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantOpenIcon", null);
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06003411 RID: 13329 RVA: 0x000F694C File Offset: 0x000F4B4C
		internal static string ERR_CantOpenWin32Manifest
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantOpenWin32Manifest", null);
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06003412 RID: 13330 RVA: 0x000F6959 File Offset: 0x000F4B59
		internal static string ERR_ErrorBuildingWin32Resources
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ErrorBuildingWin32Resources", null);
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06003413 RID: 13331 RVA: 0x000F6966 File Offset: 0x000F4B66
		internal static string ERR_DefaultValueBeforeRequiredValue
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DefaultValueBeforeRequiredValue", null);
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06003414 RID: 13332 RVA: 0x000F6973 File Offset: 0x000F4B73
		internal static string ERR_ExplicitImplCollisionOnRefOut
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExplicitImplCollisionOnRefOut", null);
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06003415 RID: 13333 RVA: 0x000F6980 File Offset: 0x000F4B80
		internal static string ERR_PartialWrongTypeParamsVariance
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialWrongTypeParamsVariance", null);
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06003416 RID: 13334 RVA: 0x000F698D File Offset: 0x000F4B8D
		internal static string ERR_UnexpectedVariance
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnexpectedVariance", null);
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06003417 RID: 13335 RVA: 0x000F699A File Offset: 0x000F4B9A
		internal static string ERR_UnexpectedVarianceStaticMember
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnexpectedVarianceStaticMember", null);
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06003418 RID: 13336 RVA: 0x000F69A7 File Offset: 0x000F4BA7
		internal static string ERR_DeriveFromDynamic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DeriveFromDynamic", null);
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06003419 RID: 13337 RVA: 0x000F69B4 File Offset: 0x000F4BB4
		internal static string ERR_DeriveFromConstructedDynamic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DeriveFromConstructedDynamic", null);
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x0600341A RID: 13338 RVA: 0x000F69C1 File Offset: 0x000F4BC1
		internal static string ERR_DynamicTypeAsBound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DynamicTypeAsBound", null);
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x0600341B RID: 13339 RVA: 0x000F69CE File Offset: 0x000F4BCE
		internal static string ERR_ConstructedDynamicTypeAsBound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConstructedDynamicTypeAsBound", null);
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x0600341C RID: 13340 RVA: 0x000F69DB File Offset: 0x000F4BDB
		internal static string ERR_DynamicRequiredTypesMissing
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DynamicRequiredTypesMissing", null);
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x0600341D RID: 13341 RVA: 0x000F69E8 File Offset: 0x000F4BE8
		internal static string ERR_MetadataNameTooLong
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MetadataNameTooLong", null);
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x0600341E RID: 13342 RVA: 0x000F69F5 File Offset: 0x000F4BF5
		internal static string ERR_AttributesNotAllowed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AttributesNotAllowed", null);
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x0600341F RID: 13343 RVA: 0x000F6A02 File Offset: 0x000F4C02
		internal static string ERR_AttributesRequireParenthesizedLambdaExpression
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AttributesRequireParenthesizedLambdaExpression", null);
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06003420 RID: 13344 RVA: 0x000F6A0F File Offset: 0x000F4C0F
		internal static string ERR_ExternAliasNotAllowed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExternAliasNotAllowed", null);
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06003421 RID: 13345 RVA: 0x000F6A1C File Offset: 0x000F4C1C
		internal static string WRN_IsDynamicIsConfusing
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IsDynamicIsConfusing", null);
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06003422 RID: 13346 RVA: 0x000F6A29 File Offset: 0x000F4C29
		internal static string WRN_IsDynamicIsConfusing_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IsDynamicIsConfusing_Title", null);
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06003423 RID: 13347 RVA: 0x000F6A36 File Offset: 0x000F4C36
		internal static string ERR_YieldNotAllowedInScript
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_YieldNotAllowedInScript", null);
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06003424 RID: 13348 RVA: 0x000F6A43 File Offset: 0x000F4C43
		internal static string ERR_NamespaceNotAllowedInScript
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NamespaceNotAllowedInScript", null);
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06003425 RID: 13349 RVA: 0x000F6A50 File Offset: 0x000F4C50
		internal static string ERR_GlobalAttributesNotAllowed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GlobalAttributesNotAllowed", null);
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06003426 RID: 13350 RVA: 0x000F6A5D File Offset: 0x000F4C5D
		internal static string ERR_InvalidDelegateType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidDelegateType", null);
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06003427 RID: 13351 RVA: 0x000F6A6A File Offset: 0x000F4C6A
		internal static string WRN_MainIgnored
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MainIgnored", null);
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06003428 RID: 13352 RVA: 0x000F6A77 File Offset: 0x000F4C77
		internal static string WRN_MainIgnored_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MainIgnored_Title", null);
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06003429 RID: 13353 RVA: 0x000F6A84 File Offset: 0x000F4C84
		internal static string WRN_StaticInAsOrIs
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_StaticInAsOrIs", null);
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x0600342A RID: 13354 RVA: 0x000F6A91 File Offset: 0x000F4C91
		internal static string WRN_StaticInAsOrIs_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_StaticInAsOrIs_Title", null);
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x0600342B RID: 13355 RVA: 0x000F6A9E File Offset: 0x000F4C9E
		internal static string ERR_BadVisEventType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadVisEventType", null);
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x0600342C RID: 13356 RVA: 0x000F6AAB File Offset: 0x000F4CAB
		internal static string ERR_NamedArgumentSpecificationBeforeFixedArgument
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NamedArgumentSpecificationBeforeFixedArgument", null);
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x0600342D RID: 13357 RVA: 0x000F6AB8 File Offset: 0x000F4CB8
		internal static string ERR_NamedArgumentSpecificationBeforeFixedArgumentInDynamicInvocation
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NamedArgumentSpecificationBeforeFixedArgumentInDynamicInvocation", null);
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x0600342E RID: 13358 RVA: 0x000F6AC5 File Offset: 0x000F4CC5
		internal static string ERR_BadNamedArgument
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadNamedArgument", null);
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x0600342F RID: 13359 RVA: 0x000F6AD2 File Offset: 0x000F4CD2
		internal static string ERR_BadNamedArgumentForDelegateInvoke
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadNamedArgumentForDelegateInvoke", null);
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06003430 RID: 13360 RVA: 0x000F6ADF File Offset: 0x000F4CDF
		internal static string ERR_DuplicateNamedArgument
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateNamedArgument", null);
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06003431 RID: 13361 RVA: 0x000F6AEC File Offset: 0x000F4CEC
		internal static string ERR_NamedArgumentUsedInPositional
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NamedArgumentUsedInPositional", null);
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06003432 RID: 13362 RVA: 0x000F6AF9 File Offset: 0x000F4CF9
		internal static string ERR_BadNonTrailingNamedArgument
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadNonTrailingNamedArgument", null);
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06003433 RID: 13363 RVA: 0x000F6B06 File Offset: 0x000F4D06
		internal static string ERR_DefaultValueUsedWithAttributes
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DefaultValueUsedWithAttributes", null);
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06003434 RID: 13364 RVA: 0x000F6B13 File Offset: 0x000F4D13
		internal static string ERR_DefaultValueMustBeConstant
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DefaultValueMustBeConstant", null);
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06003435 RID: 13365 RVA: 0x000F6B20 File Offset: 0x000F4D20
		internal static string ERR_RefOutDefaultValue
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefOutDefaultValue", null);
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06003436 RID: 13366 RVA: 0x000F6B2D File Offset: 0x000F4D2D
		internal static string ERR_DefaultValueForExtensionParameter
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DefaultValueForExtensionParameter", null);
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x000F6B3A File Offset: 0x000F4D3A
		internal static string ERR_DefaultValueForParamsParameter
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DefaultValueForParamsParameter", null);
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06003438 RID: 13368 RVA: 0x000F6B47 File Offset: 0x000F4D47
		internal static string ERR_NoConversionForDefaultParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoConversionForDefaultParam", null);
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x000F6B54 File Offset: 0x000F4D54
		internal static string ERR_NoConversionForNubDefaultParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoConversionForNubDefaultParam", null);
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x0600343A RID: 13370 RVA: 0x000F6B61 File Offset: 0x000F4D61
		internal static string ERR_NotNullRefDefaultParameter
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NotNullRefDefaultParameter", null);
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x0600343B RID: 13371 RVA: 0x000F6B6E File Offset: 0x000F4D6E
		internal static string WRN_DefaultValueForUnconsumedLocation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DefaultValueForUnconsumedLocation", null);
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x0600343C RID: 13372 RVA: 0x000F6B7B File Offset: 0x000F4D7B
		internal static string WRN_DefaultValueForUnconsumedLocation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DefaultValueForUnconsumedLocation_Title", null);
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x0600343D RID: 13373 RVA: 0x000F6B88 File Offset: 0x000F4D88
		internal static string ERR_PublicKeyFileFailure
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PublicKeyFileFailure", null);
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x0600343E RID: 13374 RVA: 0x000F6B95 File Offset: 0x000F4D95
		internal static string ERR_PublicKeyContainerFailure
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PublicKeyContainerFailure", null);
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x0600343F RID: 13375 RVA: 0x000F6BA2 File Offset: 0x000F4DA2
		internal static string ERR_BadDynamicTypeof
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadDynamicTypeof", null);
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06003440 RID: 13376 RVA: 0x000F6BAF File Offset: 0x000F4DAF
		internal static string ERR_BadNullableTypeof
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadNullableTypeof", null);
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06003441 RID: 13377 RVA: 0x000F6BBC File Offset: 0x000F4DBC
		internal static string ERR_ExpressionTreeContainsDynamicOperation
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsDynamicOperation", null);
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06003442 RID: 13378 RVA: 0x000F6BC9 File Offset: 0x000F4DC9
		internal static string ERR_BadAsyncExpressionTree
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAsyncExpressionTree", null);
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06003443 RID: 13379 RVA: 0x000F6BD6 File Offset: 0x000F4DD6
		internal static string ERR_DynamicAttributeMissing
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DynamicAttributeMissing", null);
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06003444 RID: 13380 RVA: 0x000F6BE3 File Offset: 0x000F4DE3
		internal static string ERR_CannotPassNullForFriendAssembly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CannotPassNullForFriendAssembly", null);
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06003445 RID: 13381 RVA: 0x000F6BF0 File Offset: 0x000F4DF0
		internal static string ERR_SignButNoPrivateKey
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SignButNoPrivateKey", null);
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06003446 RID: 13382 RVA: 0x000F6BFD File Offset: 0x000F4DFD
		internal static string ERR_PublicSignButNoKey
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PublicSignButNoKey", null);
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06003447 RID: 13383 RVA: 0x000F6C0A File Offset: 0x000F4E0A
		internal static string ERR_PublicSignNetModule
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PublicSignNetModule", null);
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06003448 RID: 13384 RVA: 0x000F6C17 File Offset: 0x000F4E17
		internal static string WRN_DelaySignButNoKey
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DelaySignButNoKey", null);
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06003449 RID: 13385 RVA: 0x000F6C24 File Offset: 0x000F4E24
		internal static string WRN_DelaySignButNoKey_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DelaySignButNoKey_Title", null);
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x0600344A RID: 13386 RVA: 0x000F6C31 File Offset: 0x000F4E31
		internal static string ERR_InvalidVersionFormat
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidVersionFormat", null);
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x0600344B RID: 13387 RVA: 0x000F6C3E File Offset: 0x000F4E3E
		internal static string ERR_InvalidVersionFormatDeterministic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidVersionFormatDeterministic", null);
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x0600344C RID: 13388 RVA: 0x000F6C4B File Offset: 0x000F4E4B
		internal static string ERR_InvalidVersionFormat2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidVersionFormat2", null);
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x0600344D RID: 13389 RVA: 0x000F6C58 File Offset: 0x000F4E58
		internal static string WRN_InvalidVersionFormat
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_InvalidVersionFormat", null);
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x0600344E RID: 13390 RVA: 0x000F6C65 File Offset: 0x000F4E65
		internal static string WRN_InvalidVersionFormat_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_InvalidVersionFormat_Title", null);
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x0600344F RID: 13391 RVA: 0x000F6C72 File Offset: 0x000F4E72
		internal static string ERR_InvalidAssemblyCultureForExe
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidAssemblyCultureForExe", null);
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06003450 RID: 13392 RVA: 0x000F6C7F File Offset: 0x000F4E7F
		internal static string ERR_NoCorrespondingArgument
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoCorrespondingArgument", null);
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06003451 RID: 13393 RVA: 0x000F6C8C File Offset: 0x000F4E8C
		internal static string WRN_UnimplementedCommandLineSwitch
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnimplementedCommandLineSwitch", null);
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06003452 RID: 13394 RVA: 0x000F6C99 File Offset: 0x000F4E99
		internal static string WRN_UnimplementedCommandLineSwitch_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnimplementedCommandLineSwitch_Title", null);
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06003453 RID: 13395 RVA: 0x000F6CA6 File Offset: 0x000F4EA6
		internal static string ERR_ModuleEmitFailure
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ModuleEmitFailure", null);
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06003454 RID: 13396 RVA: 0x000F6CB3 File Offset: 0x000F4EB3
		internal static string ERR_FixedLocalInLambda
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FixedLocalInLambda", null);
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06003455 RID: 13397 RVA: 0x000F6CC0 File Offset: 0x000F4EC0
		internal static string ERR_ExpressionTreeContainsNamedArgument
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsNamedArgument", null);
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06003456 RID: 13398 RVA: 0x000F6CCD File Offset: 0x000F4ECD
		internal static string ERR_ExpressionTreeContainsOptionalArgument
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsOptionalArgument", null);
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06003457 RID: 13399 RVA: 0x000F6CDA File Offset: 0x000F4EDA
		internal static string ERR_ExpressionTreeContainsIndexedProperty
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsIndexedProperty", null);
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06003458 RID: 13400 RVA: 0x000F6CE7 File Offset: 0x000F4EE7
		internal static string ERR_IndexedPropertyRequiresParams
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IndexedPropertyRequiresParams", null);
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06003459 RID: 13401 RVA: 0x000F6CF4 File Offset: 0x000F4EF4
		internal static string ERR_IndexedPropertyMustHaveAllOptionalParams
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IndexedPropertyMustHaveAllOptionalParams", null);
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x0600345A RID: 13402 RVA: 0x000F6D01 File Offset: 0x000F4F01
		internal static string ERR_SpecialByRefInLambda
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SpecialByRefInLambda", null);
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x0600345B RID: 13403 RVA: 0x000F6D0E File Offset: 0x000F4F0E
		internal static string ERR_SecurityAttributeMissingAction
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SecurityAttributeMissingAction", null);
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x0600345C RID: 13404 RVA: 0x000F6D1B File Offset: 0x000F4F1B
		internal static string ERR_SecurityAttributeInvalidAction
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SecurityAttributeInvalidAction", null);
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x0600345D RID: 13405 RVA: 0x000F6D28 File Offset: 0x000F4F28
		internal static string ERR_SecurityAttributeInvalidActionAssembly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SecurityAttributeInvalidActionAssembly", null);
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x0600345E RID: 13406 RVA: 0x000F6D35 File Offset: 0x000F4F35
		internal static string ERR_SecurityAttributeInvalidActionTypeOrMethod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SecurityAttributeInvalidActionTypeOrMethod", null);
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x0600345F RID: 13407 RVA: 0x000F6D42 File Offset: 0x000F4F42
		internal static string ERR_PrincipalPermissionInvalidAction
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PrincipalPermissionInvalidAction", null);
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06003460 RID: 13408 RVA: 0x000F6D4F File Offset: 0x000F4F4F
		internal static string ERR_FeatureNotValidInExpressionTree
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FeatureNotValidInExpressionTree", null);
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06003461 RID: 13409 RVA: 0x000F6D5C File Offset: 0x000F4F5C
		internal static string ERR_PermissionSetAttributeInvalidFile
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PermissionSetAttributeInvalidFile", null);
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06003462 RID: 13410 RVA: 0x000F6D69 File Offset: 0x000F4F69
		internal static string ERR_PermissionSetAttributeFileReadError
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PermissionSetAttributeFileReadError", null);
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06003463 RID: 13411 RVA: 0x000F6D76 File Offset: 0x000F4F76
		internal static string ERR_GlobalSingleTypeNameNotFoundFwd
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GlobalSingleTypeNameNotFoundFwd", null);
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06003464 RID: 13412 RVA: 0x000F6D83 File Offset: 0x000F4F83
		internal static string ERR_DottedTypeNameNotFoundInNSFwd
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DottedTypeNameNotFoundInNSFwd", null);
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06003465 RID: 13413 RVA: 0x000F6D90 File Offset: 0x000F4F90
		internal static string ERR_SingleTypeNameNotFoundFwd
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SingleTypeNameNotFoundFwd", null);
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06003466 RID: 13414 RVA: 0x000F6D9D File Offset: 0x000F4F9D
		internal static string ERR_AssemblySpecifiedForLinkAndRef
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AssemblySpecifiedForLinkAndRef", null);
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06003467 RID: 13415 RVA: 0x000F6DAA File Offset: 0x000F4FAA
		internal static string WRN_DeprecatedCollectionInitAdd
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DeprecatedCollectionInitAdd", null);
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06003468 RID: 13416 RVA: 0x000F6DB7 File Offset: 0x000F4FB7
		internal static string WRN_DeprecatedCollectionInitAdd_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DeprecatedCollectionInitAdd_Title", null);
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06003469 RID: 13417 RVA: 0x000F6DC4 File Offset: 0x000F4FC4
		internal static string WRN_DeprecatedCollectionInitAddStr
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DeprecatedCollectionInitAddStr", null);
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x0600346A RID: 13418 RVA: 0x000F6DD1 File Offset: 0x000F4FD1
		internal static string WRN_DeprecatedCollectionInitAddStr_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DeprecatedCollectionInitAddStr_Title", null);
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x0600346B RID: 13419 RVA: 0x000F6DDE File Offset: 0x000F4FDE
		internal static string ERR_DeprecatedCollectionInitAddStr
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DeprecatedCollectionInitAddStr", null);
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x0600346C RID: 13420 RVA: 0x000F6DEB File Offset: 0x000F4FEB
		internal static string ERR_IteratorInInteractive
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IteratorInInteractive", null);
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x000F6DF8 File Offset: 0x000F4FF8
		internal static string ERR_SecurityAttributeInvalidTarget
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SecurityAttributeInvalidTarget", null);
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x0600346E RID: 13422 RVA: 0x000F6E05 File Offset: 0x000F5005
		internal static string ERR_BadDynamicMethodArg
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadDynamicMethodArg", null);
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000F6E12 File Offset: 0x000F5012
		internal static string ERR_BadDynamicMethodArgLambda
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadDynamicMethodArgLambda", null);
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06003470 RID: 13424 RVA: 0x000F6E1F File Offset: 0x000F501F
		internal static string ERR_BadDynamicMethodArgMemgrp
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadDynamicMethodArgMemgrp", null);
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x000F6E2C File Offset: 0x000F502C
		internal static string ERR_NoDynamicPhantomOnBase
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoDynamicPhantomOnBase", null);
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06003472 RID: 13426 RVA: 0x000F6E39 File Offset: 0x000F5039
		internal static string ERR_BadDynamicQuery
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadDynamicQuery", null);
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06003473 RID: 13427 RVA: 0x000F6E46 File Offset: 0x000F5046
		internal static string ERR_NoDynamicPhantomOnBaseIndexer
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoDynamicPhantomOnBaseIndexer", null);
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06003474 RID: 13428 RVA: 0x000F6E53 File Offset: 0x000F5053
		internal static string WRN_DynamicDispatchToConditionalMethod
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DynamicDispatchToConditionalMethod", null);
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06003475 RID: 13429 RVA: 0x000F6E60 File Offset: 0x000F5060
		internal static string WRN_DynamicDispatchToConditionalMethod_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DynamicDispatchToConditionalMethod_Title", null);
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06003476 RID: 13430 RVA: 0x000F6E6D File Offset: 0x000F506D
		internal static string ERR_BadArgTypeDynamicExtension
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadArgTypeDynamicExtension", null);
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06003477 RID: 13431 RVA: 0x000F6E7A File Offset: 0x000F507A
		internal static string WRN_CallerFilePathPreferredOverCallerMemberName
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CallerFilePathPreferredOverCallerMemberName", null);
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06003478 RID: 13432 RVA: 0x000F6E87 File Offset: 0x000F5087
		internal static string WRN_CallerFilePathPreferredOverCallerMemberName_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CallerFilePathPreferredOverCallerMemberName_Title", null);
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06003479 RID: 13433 RVA: 0x000F6E94 File Offset: 0x000F5094
		internal static string WRN_CallerLineNumberPreferredOverCallerMemberName
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CallerLineNumberPreferredOverCallerMemberName", null);
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x0600347A RID: 13434 RVA: 0x000F6EA1 File Offset: 0x000F50A1
		internal static string WRN_CallerLineNumberPreferredOverCallerMemberName_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CallerLineNumberPreferredOverCallerMemberName_Title", null);
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x0600347B RID: 13435 RVA: 0x000F6EAE File Offset: 0x000F50AE
		internal static string WRN_CallerLineNumberPreferredOverCallerFilePath
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CallerLineNumberPreferredOverCallerFilePath", null);
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x0600347C RID: 13436 RVA: 0x000F6EBB File Offset: 0x000F50BB
		internal static string WRN_CallerLineNumberPreferredOverCallerFilePath_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CallerLineNumberPreferredOverCallerFilePath_Title", null);
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x0600347D RID: 13437 RVA: 0x000F6EC8 File Offset: 0x000F50C8
		internal static string ERR_InvalidDynamicCondition
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidDynamicCondition", null);
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000F6ED5 File Offset: 0x000F50D5
		internal static string ERR_MixingWinRTEventWithRegular
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MixingWinRTEventWithRegular", null);
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x0600347F RID: 13439 RVA: 0x000F6EE2 File Offset: 0x000F50E2
		internal static string WRN_CA2000_DisposeObjectsBeforeLosingScope1
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CA2000_DisposeObjectsBeforeLosingScope1", null);
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06003480 RID: 13440 RVA: 0x000F6EEF File Offset: 0x000F50EF
		internal static string WRN_CA2000_DisposeObjectsBeforeLosingScope1_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CA2000_DisposeObjectsBeforeLosingScope1_Title", null);
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06003481 RID: 13441 RVA: 0x000F6EFC File Offset: 0x000F50FC
		internal static string WRN_CA2000_DisposeObjectsBeforeLosingScope2
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CA2000_DisposeObjectsBeforeLosingScope2", null);
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x000F6F09 File Offset: 0x000F5109
		internal static string WRN_CA2000_DisposeObjectsBeforeLosingScope2_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CA2000_DisposeObjectsBeforeLosingScope2_Title", null);
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06003483 RID: 13443 RVA: 0x000F6F16 File Offset: 0x000F5116
		internal static string WRN_CA2202_DoNotDisposeObjectsMultipleTimes
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CA2202_DoNotDisposeObjectsMultipleTimes", null);
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x000F6F23 File Offset: 0x000F5123
		internal static string WRN_CA2202_DoNotDisposeObjectsMultipleTimes_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CA2202_DoNotDisposeObjectsMultipleTimes_Title", null);
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x000F6F30 File Offset: 0x000F5130
		internal static string ERR_NewCoClassOnLink
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NewCoClassOnLink", null);
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x000F6F3D File Offset: 0x000F513D
		internal static string ERR_NoPIANestedType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoPIANestedType", null);
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06003487 RID: 13447 RVA: 0x000F6F4A File Offset: 0x000F514A
		internal static string ERR_GenericsUsedInNoPIAType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GenericsUsedInNoPIAType", null);
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06003488 RID: 13448 RVA: 0x000F6F57 File Offset: 0x000F5157
		internal static string ERR_InteropStructContainsMethods
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InteropStructContainsMethods", null);
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06003489 RID: 13449 RVA: 0x000F6F64 File Offset: 0x000F5164
		internal static string ERR_WinRtEventPassedByRef
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_WinRtEventPassedByRef", null);
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x0600348A RID: 13450 RVA: 0x000F6F71 File Offset: 0x000F5171
		internal static string ERR_MissingMethodOnSourceInterface
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MissingMethodOnSourceInterface", null);
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x0600348B RID: 13451 RVA: 0x000F6F7E File Offset: 0x000F517E
		internal static string ERR_MissingSourceInterface
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MissingSourceInterface", null);
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x0600348C RID: 13452 RVA: 0x000F6F8B File Offset: 0x000F518B
		internal static string ERR_InteropTypeMissingAttribute
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InteropTypeMissingAttribute", null);
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x000F6F98 File Offset: 0x000F5198
		internal static string ERR_NoPIAAssemblyMissingAttribute
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoPIAAssemblyMissingAttribute", null);
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x0600348E RID: 13454 RVA: 0x000F6FA5 File Offset: 0x000F51A5
		internal static string ERR_NoPIAAssemblyMissingAttributes
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoPIAAssemblyMissingAttributes", null);
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x0600348F RID: 13455 RVA: 0x000F6FB2 File Offset: 0x000F51B2
		internal static string ERR_InteropTypesWithSameNameAndGuid
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InteropTypesWithSameNameAndGuid", null);
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06003490 RID: 13456 RVA: 0x000F6FBF File Offset: 0x000F51BF
		internal static string ERR_LocalTypeNameClash
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LocalTypeNameClash", null);
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06003491 RID: 13457 RVA: 0x000F6FCC File Offset: 0x000F51CC
		internal static string WRN_ReferencedAssemblyReferencesLinkedPIA
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ReferencedAssemblyReferencesLinkedPIA", null);
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06003492 RID: 13458 RVA: 0x000F6FD9 File Offset: 0x000F51D9
		internal static string WRN_ReferencedAssemblyReferencesLinkedPIA_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ReferencedAssemblyReferencesLinkedPIA_Title", null);
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06003493 RID: 13459 RVA: 0x000F6FE6 File Offset: 0x000F51E6
		internal static string WRN_ReferencedAssemblyReferencesLinkedPIA_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ReferencedAssemblyReferencesLinkedPIA_Description", null);
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06003494 RID: 13460 RVA: 0x000F6FF3 File Offset: 0x000F51F3
		internal static string ERR_GenericsUsedAcrossAssemblies
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GenericsUsedAcrossAssemblies", null);
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x000F7000 File Offset: 0x000F5200
		internal static string ERR_NoCanonicalView
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoCanonicalView", null);
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06003496 RID: 13462 RVA: 0x000F700D File Offset: 0x000F520D
		internal static string ERR_NetModuleNameMismatch
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NetModuleNameMismatch", null);
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06003497 RID: 13463 RVA: 0x000F701A File Offset: 0x000F521A
		internal static string ERR_BadModuleName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadModuleName", null);
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x000F7027 File Offset: 0x000F5227
		internal static string ERR_BadCompilationOptionValue
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadCompilationOptionValue", null);
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06003499 RID: 13465 RVA: 0x000F7034 File Offset: 0x000F5234
		internal static string ERR_BadAppConfigPath
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAppConfigPath", null);
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x0600349A RID: 13466 RVA: 0x000F7041 File Offset: 0x000F5241
		internal static string WRN_AssemblyAttributeFromModuleIsOverridden
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AssemblyAttributeFromModuleIsOverridden", null);
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x0600349B RID: 13467 RVA: 0x000F704E File Offset: 0x000F524E
		internal static string WRN_AssemblyAttributeFromModuleIsOverridden_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AssemblyAttributeFromModuleIsOverridden_Title", null);
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x0600349C RID: 13468 RVA: 0x000F705B File Offset: 0x000F525B
		internal static string ERR_CmdOptionConflictsSource
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CmdOptionConflictsSource", null);
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x0600349D RID: 13469 RVA: 0x000F7068 File Offset: 0x000F5268
		internal static string ERR_FixedBufferTooManyDimensions
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FixedBufferTooManyDimensions", null);
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x0600349E RID: 13470 RVA: 0x000F7075 File Offset: 0x000F5275
		internal static string WRN_ReferencedAssemblyDoesNotHaveStrongName
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ReferencedAssemblyDoesNotHaveStrongName", null);
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x0600349F RID: 13471 RVA: 0x000F7082 File Offset: 0x000F5282
		internal static string WRN_ReferencedAssemblyDoesNotHaveStrongName_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ReferencedAssemblyDoesNotHaveStrongName_Title", null);
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x060034A0 RID: 13472 RVA: 0x000F708F File Offset: 0x000F528F
		internal static string ERR_InvalidSignaturePublicKey
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidSignaturePublicKey", null);
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060034A1 RID: 13473 RVA: 0x000F709C File Offset: 0x000F529C
		internal static string ERR_ExportedTypeConflictsWithDeclaration
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExportedTypeConflictsWithDeclaration", null);
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060034A2 RID: 13474 RVA: 0x000F70A9 File Offset: 0x000F52A9
		internal static string ERR_ExportedTypesConflict
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExportedTypesConflict", null);
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060034A3 RID: 13475 RVA: 0x000F70B6 File Offset: 0x000F52B6
		internal static string ERR_ForwardedTypeConflictsWithDeclaration
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ForwardedTypeConflictsWithDeclaration", null);
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060034A4 RID: 13476 RVA: 0x000F70C3 File Offset: 0x000F52C3
		internal static string ERR_ForwardedTypesConflict
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ForwardedTypesConflict", null);
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x060034A5 RID: 13477 RVA: 0x000F70D0 File Offset: 0x000F52D0
		internal static string ERR_ForwardedTypeConflictsWithExportedType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ForwardedTypeConflictsWithExportedType", null);
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060034A6 RID: 13478 RVA: 0x000F70DD File Offset: 0x000F52DD
		internal static string WRN_RefCultureMismatch
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_RefCultureMismatch", null);
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060034A7 RID: 13479 RVA: 0x000F70EA File Offset: 0x000F52EA
		internal static string WRN_RefCultureMismatch_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_RefCultureMismatch_Title", null);
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x060034A8 RID: 13480 RVA: 0x000F70F7 File Offset: 0x000F52F7
		internal static string ERR_AgnosticToMachineModule
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AgnosticToMachineModule", null);
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060034A9 RID: 13481 RVA: 0x000F7104 File Offset: 0x000F5304
		internal static string ERR_ConflictingMachineModule
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConflictingMachineModule", null);
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060034AA RID: 13482 RVA: 0x000F7111 File Offset: 0x000F5311
		internal static string WRN_ConflictingMachineAssembly
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ConflictingMachineAssembly", null);
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x060034AB RID: 13483 RVA: 0x000F711E File Offset: 0x000F531E
		internal static string WRN_ConflictingMachineAssembly_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ConflictingMachineAssembly_Title", null);
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x060034AC RID: 13484 RVA: 0x000F712B File Offset: 0x000F532B
		internal static string ERR_CryptoHashFailed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CryptoHashFailed", null);
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x060034AD RID: 13485 RVA: 0x000F7138 File Offset: 0x000F5338
		internal static string ERR_MissingNetModuleReference
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MissingNetModuleReference", null);
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x060034AE RID: 13486 RVA: 0x000F7145 File Offset: 0x000F5345
		internal static string ERR_NetModuleNameMustBeUnique
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NetModuleNameMustBeUnique", null);
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x060034AF RID: 13487 RVA: 0x000F7152 File Offset: 0x000F5352
		internal static string ERR_CantReadConfigFile
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantReadConfigFile", null);
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x060034B0 RID: 13488 RVA: 0x000F715F File Offset: 0x000F535F
		internal static string ERR_EncNoPIAReference
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EncNoPIAReference", null);
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x060034B1 RID: 13489 RVA: 0x000F716C File Offset: 0x000F536C
		internal static string ERR_EncReferenceToAddedMember
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EncReferenceToAddedMember", null);
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x060034B2 RID: 13490 RVA: 0x000F7179 File Offset: 0x000F5379
		internal static string ERR_MutuallyExclusiveOptions
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MutuallyExclusiveOptions", null);
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x060034B3 RID: 13491 RVA: 0x000F7186 File Offset: 0x000F5386
		internal static string ERR_LinkedNetmoduleMetadataMustProvideFullPEImage
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LinkedNetmoduleMetadataMustProvideFullPEImage", null);
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x060034B4 RID: 13492 RVA: 0x000F7193 File Offset: 0x000F5393
		internal static string ERR_BadPrefer32OnLib
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadPrefer32OnLib", null);
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x060034B5 RID: 13493 RVA: 0x000F71A0 File Offset: 0x000F53A0
		internal static string IDS_PathList
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_PathList", null);
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x060034B6 RID: 13494 RVA: 0x000F71AD File Offset: 0x000F53AD
		internal static string IDS_Text
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_Text", null);
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x060034B7 RID: 13495 RVA: 0x000F71BA File Offset: 0x000F53BA
		internal static string IDS_FeatureNullPropagatingOperator
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureNullPropagatingOperator", null);
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x060034B8 RID: 13496 RVA: 0x000F71C7 File Offset: 0x000F53C7
		internal static string IDS_FeatureExpressionBodiedMethod
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureExpressionBodiedMethod", null);
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x060034B9 RID: 13497 RVA: 0x000F71D4 File Offset: 0x000F53D4
		internal static string IDS_FeatureExpressionBodiedProperty
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureExpressionBodiedProperty", null);
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x060034BA RID: 13498 RVA: 0x000F71E1 File Offset: 0x000F53E1
		internal static string IDS_FeatureExpressionBodiedIndexer
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureExpressionBodiedIndexer", null);
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x060034BB RID: 13499 RVA: 0x000F71EE File Offset: 0x000F53EE
		internal static string IDS_FeatureAutoPropertyInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureAutoPropertyInitializer", null);
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x060034BC RID: 13500 RVA: 0x000F71FB File Offset: 0x000F53FB
		internal static string IDS_Namespace1
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_Namespace1", null);
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x060034BD RID: 13501 RVA: 0x000F7208 File Offset: 0x000F5408
		internal static string IDS_FeatureRefLocalsReturns
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureRefLocalsReturns", null);
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x060034BE RID: 13502 RVA: 0x000F7215 File Offset: 0x000F5415
		internal static string IDS_FeatureReadOnlyReferences
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureReadOnlyReferences", null);
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x060034BF RID: 13503 RVA: 0x000F7222 File Offset: 0x000F5422
		internal static string IDS_FeatureRefStructs
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureRefStructs", null);
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x060034C0 RID: 13504 RVA: 0x000F722F File Offset: 0x000F542F
		internal static string IDS_FeatureRefConditional
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureRefConditional", null);
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x060034C1 RID: 13505 RVA: 0x000F723C File Offset: 0x000F543C
		internal static string IDS_FeatureRefReassignment
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureRefReassignment", null);
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x060034C2 RID: 13506 RVA: 0x000F7249 File Offset: 0x000F5449
		internal static string IDS_FeatureRefFor
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureRefFor", null);
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x060034C3 RID: 13507 RVA: 0x000F7256 File Offset: 0x000F5456
		internal static string IDS_FeatureRefForEach
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureRefForEach", null);
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x060034C4 RID: 13508 RVA: 0x000F7263 File Offset: 0x000F5463
		internal static string IDS_FeatureExtensibleFixedStatement
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureExtensibleFixedStatement", null);
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x060034C5 RID: 13509 RVA: 0x000F7270 File Offset: 0x000F5470
		internal static string CompilationC
		{
			get
			{
				return CSharpResources.GetResourceString("CompilationC", null);
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x060034C6 RID: 13510 RVA: 0x000F727D File Offset: 0x000F547D
		internal static string SyntaxNodeIsNotWithinSynt
		{
			get
			{
				return CSharpResources.GetResourceString("SyntaxNodeIsNotWithinSynt", null);
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x060034C7 RID: 13511 RVA: 0x000F728A File Offset: 0x000F548A
		internal static string LocationMustBeProvided
		{
			get
			{
				return CSharpResources.GetResourceString("LocationMustBeProvided", null);
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x060034C8 RID: 13512 RVA: 0x000F7297 File Offset: 0x000F5497
		internal static string SyntaxTreeSemanticModelMust
		{
			get
			{
				return CSharpResources.GetResourceString("SyntaxTreeSemanticModelMust", null);
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x060034C9 RID: 13513 RVA: 0x000F72A4 File Offset: 0x000F54A4
		internal static string CantReferenceCompilationOf
		{
			get
			{
				return CSharpResources.GetResourceString("CantReferenceCompilationOf", null);
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x060034CA RID: 13514 RVA: 0x000F72B1 File Offset: 0x000F54B1
		internal static string SyntaxTreeAlreadyPresent
		{
			get
			{
				return CSharpResources.GetResourceString("SyntaxTreeAlreadyPresent", null);
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x060034CB RID: 13515 RVA: 0x000F72BE File Offset: 0x000F54BE
		internal static string SubmissionCanOnlyInclude
		{
			get
			{
				return CSharpResources.GetResourceString("SubmissionCanOnlyInclude", null);
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x060034CC RID: 13516 RVA: 0x000F72CB File Offset: 0x000F54CB
		internal static string SubmissionCanHaveAtMostOne
		{
			get
			{
				return CSharpResources.GetResourceString("SubmissionCanHaveAtMostOne", null);
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x000F72D8 File Offset: 0x000F54D8
		internal static string SyntaxTreeNotFoundToRemove
		{
			get
			{
				return CSharpResources.GetResourceString("SyntaxTreeNotFoundToRemove", null);
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x060034CE RID: 13518 RVA: 0x000F72E5 File Offset: 0x000F54E5
		internal static string TreeMustHaveARootNodeWith
		{
			get
			{
				return CSharpResources.GetResourceString("TreeMustHaveARootNodeWith", null);
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x060034CF RID: 13519 RVA: 0x000F72F2 File Offset: 0x000F54F2
		internal static string TypeArgumentCannotBeNull
		{
			get
			{
				return CSharpResources.GetResourceString("TypeArgumentCannotBeNull", null);
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x060034D0 RID: 13520 RVA: 0x000F72FF File Offset: 0x000F54FF
		internal static string WrongNumberOfTypeArguments
		{
			get
			{
				return CSharpResources.GetResourceString("WrongNumberOfTypeArguments", null);
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x060034D1 RID: 13521 RVA: 0x000F730C File Offset: 0x000F550C
		internal static string NameConflictForName
		{
			get
			{
				return CSharpResources.GetResourceString("NameConflictForName", null);
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x060034D2 RID: 13522 RVA: 0x000F7319 File Offset: 0x000F5519
		internal static string LookupOptionsHasInvalidCombo
		{
			get
			{
				return CSharpResources.GetResourceString("LookupOptionsHasInvalidCombo", null);
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x060034D3 RID: 13523 RVA: 0x000F7326 File Offset: 0x000F5526
		internal static string ItemsMustBeNonEmpty
		{
			get
			{
				return CSharpResources.GetResourceString("ItemsMustBeNonEmpty", null);
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x060034D4 RID: 13524 RVA: 0x000F7333 File Offset: 0x000F5533
		internal static string UseVerbatimIdentifier
		{
			get
			{
				return CSharpResources.GetResourceString("UseVerbatimIdentifier", null);
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x060034D5 RID: 13525 RVA: 0x000F7340 File Offset: 0x000F5540
		internal static string UseLiteralForTokens
		{
			get
			{
				return CSharpResources.GetResourceString("UseLiteralForTokens", null);
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x060034D6 RID: 13526 RVA: 0x000F734D File Offset: 0x000F554D
		internal static string UseLiteralForNumeric
		{
			get
			{
				return CSharpResources.GetResourceString("UseLiteralForNumeric", null);
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x060034D7 RID: 13527 RVA: 0x000F735A File Offset: 0x000F555A
		internal static string ThisMethodCanOnlyBeUsedToCreateTokens
		{
			get
			{
				return CSharpResources.GetResourceString("ThisMethodCanOnlyBeUsedToCreateTokens", null);
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x060034D8 RID: 13528 RVA: 0x000F7367 File Offset: 0x000F5567
		internal static string GenericParameterDefinition
		{
			get
			{
				return CSharpResources.GetResourceString("GenericParameterDefinition", null);
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x060034D9 RID: 13529 RVA: 0x000F7374 File Offset: 0x000F5574
		internal static string InvalidGetDeclarationNameMultipleDeclarators
		{
			get
			{
				return CSharpResources.GetResourceString("InvalidGetDeclarationNameMultipleDeclarators", null);
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x060034DA RID: 13530 RVA: 0x000F7381 File Offset: 0x000F5581
		internal static string TreeNotPartOfCompilation
		{
			get
			{
				return CSharpResources.GetResourceString("TreeNotPartOfCompilation", null);
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x060034DB RID: 13531 RVA: 0x000F738E File Offset: 0x000F558E
		internal static string PositionIsNotWithinSyntax
		{
			get
			{
				return CSharpResources.GetResourceString("PositionIsNotWithinSyntax", null);
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x060034DC RID: 13532 RVA: 0x000F739B File Offset: 0x000F559B
		internal static string WRN_BadUILang
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadUILang", null);
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x000F73A8 File Offset: 0x000F55A8
		internal static string WRN_BadUILang_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_BadUILang_Title", null);
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x060034DE RID: 13534 RVA: 0x000F73B5 File Offset: 0x000F55B5
		internal static string ERR_UnsupportedTransparentIdentifierAccess
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnsupportedTransparentIdentifierAccess", null);
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x000F73C2 File Offset: 0x000F55C2
		internal static string ERR_ParamDefaultValueDiffersFromAttribute
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ParamDefaultValueDiffersFromAttribute", null);
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x060034E0 RID: 13536 RVA: 0x000F73CF File Offset: 0x000F55CF
		internal static string ERR_FieldHasMultipleDistinctConstantValues
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FieldHasMultipleDistinctConstantValues", null);
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x060034E1 RID: 13537 RVA: 0x000F73DC File Offset: 0x000F55DC
		internal static string WRN_UnqualifiedNestedTypeInCref
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnqualifiedNestedTypeInCref", null);
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x060034E2 RID: 13538 RVA: 0x000F73E9 File Offset: 0x000F55E9
		internal static string WRN_UnqualifiedNestedTypeInCref_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnqualifiedNestedTypeInCref_Title", null);
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x060034E3 RID: 13539 RVA: 0x000F73F6 File Offset: 0x000F55F6
		internal static string NotACSharpSymbol
		{
			get
			{
				return CSharpResources.GetResourceString("NotACSharpSymbol", null);
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x060034E4 RID: 13540 RVA: 0x000F7403 File Offset: 0x000F5603
		internal static string HDN_UnusedUsingDirective
		{
			get
			{
				return CSharpResources.GetResourceString("HDN_UnusedUsingDirective", null);
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x060034E5 RID: 13541 RVA: 0x000F7410 File Offset: 0x000F5610
		internal static string HDN_UnusedExternAlias
		{
			get
			{
				return CSharpResources.GetResourceString("HDN_UnusedExternAlias", null);
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x060034E6 RID: 13542 RVA: 0x000F741D File Offset: 0x000F561D
		internal static string ElementsCannotBeNull
		{
			get
			{
				return CSharpResources.GetResourceString("ElementsCannotBeNull", null);
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x060034E7 RID: 13543 RVA: 0x000F742A File Offset: 0x000F562A
		internal static string IDS_LIB_ENV
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_LIB_ENV", null);
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x060034E8 RID: 13544 RVA: 0x000F7437 File Offset: 0x000F5637
		internal static string IDS_LIB_OPTION
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_LIB_OPTION", null);
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x060034E9 RID: 13545 RVA: 0x000F7444 File Offset: 0x000F5644
		internal static string IDS_REFERENCEPATH_OPTION
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_REFERENCEPATH_OPTION", null);
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x060034EA RID: 13546 RVA: 0x000F7451 File Offset: 0x000F5651
		internal static string IDS_DirectoryDoesNotExist
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_DirectoryDoesNotExist", null);
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x060034EB RID: 13547 RVA: 0x000F745E File Offset: 0x000F565E
		internal static string IDS_DirectoryHasInvalidPath
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_DirectoryHasInvalidPath", null);
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x060034EC RID: 13548 RVA: 0x000F746B File Offset: 0x000F566B
		internal static string WRN_NoRuntimeMetadataVersion
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NoRuntimeMetadataVersion", null);
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x060034ED RID: 13549 RVA: 0x000F7478 File Offset: 0x000F5678
		internal static string WRN_NoRuntimeMetadataVersion_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NoRuntimeMetadataVersion_Title", null);
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x060034EE RID: 13550 RVA: 0x000F7485 File Offset: 0x000F5685
		internal static string WrongSemanticModelType
		{
			get
			{
				return CSharpResources.GetResourceString("WrongSemanticModelType", null);
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x060034EF RID: 13551 RVA: 0x000F7492 File Offset: 0x000F5692
		internal static string IDS_FeatureLambda
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureLambda", null);
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x060034F0 RID: 13552 RVA: 0x000F749F File Offset: 0x000F569F
		internal static string ERR_FeatureNotAvailableInVersion1
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FeatureNotAvailableInVersion1", null);
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x060034F1 RID: 13553 RVA: 0x000F74AC File Offset: 0x000F56AC
		internal static string ERR_FeatureNotAvailableInVersion2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FeatureNotAvailableInVersion2", null);
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x060034F2 RID: 13554 RVA: 0x000F74B9 File Offset: 0x000F56B9
		internal static string ERR_FeatureNotAvailableInVersion3
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FeatureNotAvailableInVersion3", null);
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x060034F3 RID: 13555 RVA: 0x000F74C6 File Offset: 0x000F56C6
		internal static string ERR_FeatureNotAvailableInVersion4
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FeatureNotAvailableInVersion4", null);
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x060034F4 RID: 13556 RVA: 0x000F74D3 File Offset: 0x000F56D3
		internal static string ERR_FeatureNotAvailableInVersion5
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FeatureNotAvailableInVersion5", null);
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x060034F5 RID: 13557 RVA: 0x000F74E0 File Offset: 0x000F56E0
		internal static string ERR_FeatureNotAvailableInVersion6
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FeatureNotAvailableInVersion6", null);
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x060034F6 RID: 13558 RVA: 0x000F74ED File Offset: 0x000F56ED
		internal static string ERR_FeatureNotAvailableInVersion7
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FeatureNotAvailableInVersion7", null);
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x060034F7 RID: 13559 RVA: 0x000F74FA File Offset: 0x000F56FA
		internal static string ERR_FeatureIsExperimental
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FeatureIsExperimental", null);
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x060034F8 RID: 13560 RVA: 0x000F7507 File Offset: 0x000F5707
		internal static string IDS_VersionExperimental
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_VersionExperimental", null);
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x060034F9 RID: 13561 RVA: 0x000F7514 File Offset: 0x000F5714
		internal static string PositionNotWithinTree
		{
			get
			{
				return CSharpResources.GetResourceString("PositionNotWithinTree", null);
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x060034FA RID: 13562 RVA: 0x000F7521 File Offset: 0x000F5721
		internal static string SpeculatedSyntaxNodeCannotBelongToCurrentCompilation
		{
			get
			{
				return CSharpResources.GetResourceString("SpeculatedSyntaxNodeCannotBelongToCurrentCompilation", null);
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x060034FB RID: 13563 RVA: 0x000F752E File Offset: 0x000F572E
		internal static string ChainingSpeculativeModelIsNotSupported
		{
			get
			{
				return CSharpResources.GetResourceString("ChainingSpeculativeModelIsNotSupported", null);
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x060034FC RID: 13564 RVA: 0x000F753B File Offset: 0x000F573B
		internal static string IDS_ToolName
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_ToolName", null);
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x060034FD RID: 13565 RVA: 0x000F7548 File Offset: 0x000F5748
		internal static string IDS_LogoLine1
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_LogoLine1", null);
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x060034FE RID: 13566 RVA: 0x000F7555 File Offset: 0x000F5755
		internal static string IDS_LogoLine2
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_LogoLine2", null);
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x060034FF RID: 13567 RVA: 0x000F7562 File Offset: 0x000F5762
		internal static string IDS_LangVersions
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_LangVersions", null);
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06003500 RID: 13568 RVA: 0x000F756F File Offset: 0x000F576F
		internal static string IDS_CSCHelp
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_CSCHelp", null);
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06003501 RID: 13569 RVA: 0x000F757C File Offset: 0x000F577C
		internal static string ERR_ComImportWithInitializers
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ComImportWithInitializers", null);
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06003502 RID: 13570 RVA: 0x000F7589 File Offset: 0x000F5789
		internal static string WRN_PdbLocalNameTooLong
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_PdbLocalNameTooLong", null);
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06003503 RID: 13571 RVA: 0x000F7596 File Offset: 0x000F5796
		internal static string WRN_PdbLocalNameTooLong_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_PdbLocalNameTooLong_Title", null);
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06003504 RID: 13572 RVA: 0x000F75A3 File Offset: 0x000F57A3
		internal static string ERR_RetNoObjectRequiredLambda
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RetNoObjectRequiredLambda", null);
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06003505 RID: 13573 RVA: 0x000F75B0 File Offset: 0x000F57B0
		internal static string ERR_TaskRetNoObjectRequiredLambda
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TaskRetNoObjectRequiredLambda", null);
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06003506 RID: 13574 RVA: 0x000F75BD File Offset: 0x000F57BD
		internal static string WRN_AnalyzerCannotBeCreated
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AnalyzerCannotBeCreated", null);
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06003507 RID: 13575 RVA: 0x000F75CA File Offset: 0x000F57CA
		internal static string WRN_AnalyzerCannotBeCreated_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AnalyzerCannotBeCreated_Title", null);
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06003508 RID: 13576 RVA: 0x000F75D7 File Offset: 0x000F57D7
		internal static string WRN_NoAnalyzerInAssembly
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NoAnalyzerInAssembly", null);
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06003509 RID: 13577 RVA: 0x000F75E4 File Offset: 0x000F57E4
		internal static string WRN_NoAnalyzerInAssembly_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NoAnalyzerInAssembly_Title", null);
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x0600350A RID: 13578 RVA: 0x000F75F1 File Offset: 0x000F57F1
		internal static string WRN_UnableToLoadAnalyzer
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnableToLoadAnalyzer", null);
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x0600350B RID: 13579 RVA: 0x000F75FE File Offset: 0x000F57FE
		internal static string WRN_UnableToLoadAnalyzer_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnableToLoadAnalyzer_Title", null);
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x0600350C RID: 13580 RVA: 0x000F760B File Offset: 0x000F580B
		internal static string INF_UnableToLoadSomeTypesInAnalyzer
		{
			get
			{
				return CSharpResources.GetResourceString("INF_UnableToLoadSomeTypesInAnalyzer", null);
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x0600350D RID: 13581 RVA: 0x000F7618 File Offset: 0x000F5818
		internal static string ERR_CantReadRulesetFile
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantReadRulesetFile", null);
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x0600350E RID: 13582 RVA: 0x000F7625 File Offset: 0x000F5825
		internal static string ERR_BadPdbData
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadPdbData", null);
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x0600350F RID: 13583 RVA: 0x000F7632 File Offset: 0x000F5832
		internal static string IDS_OperationCausedStackOverflow
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_OperationCausedStackOverflow", null);
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06003510 RID: 13584 RVA: 0x000F763F File Offset: 0x000F583F
		internal static string WRN_IdentifierOrNumericLiteralExpected
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IdentifierOrNumericLiteralExpected", null);
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06003511 RID: 13585 RVA: 0x000F764C File Offset: 0x000F584C
		internal static string WRN_IdentifierOrNumericLiteralExpected_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IdentifierOrNumericLiteralExpected_Title", null);
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06003512 RID: 13586 RVA: 0x000F7659 File Offset: 0x000F5859
		internal static string ERR_InitializerOnNonAutoProperty
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InitializerOnNonAutoProperty", null);
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x000F7666 File Offset: 0x000F5866
		internal static string ERR_InstancePropertyInitializerInInterface
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InstancePropertyInitializerInInterface", null);
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06003514 RID: 13588 RVA: 0x000F7673 File Offset: 0x000F5873
		internal static string ERR_AutoPropertyMustHaveGetAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AutoPropertyMustHaveGetAccessor", null);
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06003515 RID: 13589 RVA: 0x000F7680 File Offset: 0x000F5880
		internal static string ERR_AutoPropertyMustOverrideSet
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AutoPropertyMustOverrideSet", null);
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06003516 RID: 13590 RVA: 0x000F768D File Offset: 0x000F588D
		internal static string ERR_InitializerInStructWithoutExplicitConstructor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InitializerInStructWithoutExplicitConstructor", null);
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06003517 RID: 13591 RVA: 0x000F769A File Offset: 0x000F589A
		internal static string ERR_EncodinglessSyntaxTree
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EncodinglessSyntaxTree", null);
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06003518 RID: 13592 RVA: 0x000F76A7 File Offset: 0x000F58A7
		internal static string ERR_BlockBodyAndExpressionBody
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BlockBodyAndExpressionBody", null);
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x000F76B4 File Offset: 0x000F58B4
		internal static string ERR_SwitchFallOut
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SwitchFallOut", null);
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x0600351A RID: 13594 RVA: 0x000F76C1 File Offset: 0x000F58C1
		internal static string ERR_UnexpectedBoundGenericName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnexpectedBoundGenericName", null);
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x000F76CE File Offset: 0x000F58CE
		internal static string ERR_NullPropagatingOpInExpressionTree
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NullPropagatingOpInExpressionTree", null);
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x0600351C RID: 13596 RVA: 0x000F76DB File Offset: 0x000F58DB
		internal static string ERR_DictionaryInitializerInExpressionTree
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DictionaryInitializerInExpressionTree", null);
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x0600351D RID: 13597 RVA: 0x000F76E8 File Offset: 0x000F58E8
		internal static string ERR_ExtensionCollectionElementInitializerInExpressionTree
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExtensionCollectionElementInitializerInExpressionTree", null);
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x0600351E RID: 13598 RVA: 0x000F76F5 File Offset: 0x000F58F5
		internal static string IDS_FeatureNameof
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureNameof", null);
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x0600351F RID: 13599 RVA: 0x000F7702 File Offset: 0x000F5902
		internal static string IDS_FeatureDictionaryInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureDictionaryInitializer", null);
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06003520 RID: 13600 RVA: 0x000F770F File Offset: 0x000F590F
		internal static string ERR_UnclosedExpressionHole
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnclosedExpressionHole", null);
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06003521 RID: 13601 RVA: 0x000F771C File Offset: 0x000F591C
		internal static string ERR_SingleLineCommentInExpressionHole
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SingleLineCommentInExpressionHole", null);
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06003522 RID: 13602 RVA: 0x000F7729 File Offset: 0x000F5929
		internal static string ERR_InsufficientStack
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InsufficientStack", null);
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06003523 RID: 13603 RVA: 0x000F7736 File Offset: 0x000F5936
		internal static string ERR_ExpressionHasNoName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionHasNoName", null);
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06003524 RID: 13604 RVA: 0x000F7743 File Offset: 0x000F5943
		internal static string ERR_SubexpressionNotInNameof
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SubexpressionNotInNameof", null);
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06003525 RID: 13605 RVA: 0x000F7750 File Offset: 0x000F5950
		internal static string ERR_AliasQualifiedNameNotAnExpression
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AliasQualifiedNameNotAnExpression", null);
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x000F775D File Offset: 0x000F595D
		internal static string ERR_NameofMethodGroupWithTypeParameters
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NameofMethodGroupWithTypeParameters", null);
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06003527 RID: 13607 RVA: 0x000F776A File Offset: 0x000F596A
		internal static string NoNoneSearchCriteria
		{
			get
			{
				return CSharpResources.GetResourceString("NoNoneSearchCriteria", null);
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06003528 RID: 13608 RVA: 0x000F7777 File Offset: 0x000F5977
		internal static string ERR_InvalidAssemblyCulture
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidAssemblyCulture", null);
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06003529 RID: 13609 RVA: 0x000F7784 File Offset: 0x000F5984
		internal static string IDS_FeatureUsingStatic
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureUsingStatic", null);
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x0600352A RID: 13610 RVA: 0x000F7791 File Offset: 0x000F5991
		internal static string IDS_FeatureInterpolatedStrings
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureInterpolatedStrings", null);
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x0600352B RID: 13611 RVA: 0x000F779E File Offset: 0x000F599E
		internal static string IDS_FeatureAltInterpolatedVerbatimStrings
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureAltInterpolatedVerbatimStrings", null);
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x0600352C RID: 13612 RVA: 0x000F77AB File Offset: 0x000F59AB
		internal static string IDS_AwaitInCatchAndFinally
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_AwaitInCatchAndFinally", null);
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x0600352D RID: 13613 RVA: 0x000F77B8 File Offset: 0x000F59B8
		internal static string IDS_FeatureBinaryLiteral
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureBinaryLiteral", null);
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x0600352E RID: 13614 RVA: 0x000F77C5 File Offset: 0x000F59C5
		internal static string IDS_FeatureDigitSeparator
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureDigitSeparator", null);
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x0600352F RID: 13615 RVA: 0x000F77D2 File Offset: 0x000F59D2
		internal static string IDS_FeatureLocalFunctions
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureLocalFunctions", null);
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06003530 RID: 13616 RVA: 0x000F77DF File Offset: 0x000F59DF
		internal static string ERR_UnescapedCurly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnescapedCurly", null);
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06003531 RID: 13617 RVA: 0x000F77EC File Offset: 0x000F59EC
		internal static string ERR_EscapedCurly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EscapedCurly", null);
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06003532 RID: 13618 RVA: 0x000F77F9 File Offset: 0x000F59F9
		internal static string ERR_TrailingWhitespaceInFormatSpecifier
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TrailingWhitespaceInFormatSpecifier", null);
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06003533 RID: 13619 RVA: 0x000F7806 File Offset: 0x000F5A06
		internal static string ERR_EmptyFormatSpecifier
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EmptyFormatSpecifier", null);
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06003534 RID: 13620 RVA: 0x000F7813 File Offset: 0x000F5A13
		internal static string ERR_ErrorInReferencedAssembly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ErrorInReferencedAssembly", null);
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06003535 RID: 13621 RVA: 0x000F7820 File Offset: 0x000F5A20
		internal static string ERR_ExpressionOrDeclarationExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionOrDeclarationExpected", null);
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06003536 RID: 13622 RVA: 0x000F782D File Offset: 0x000F5A2D
		internal static string ERR_NameofExtensionMethod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NameofExtensionMethod", null);
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06003537 RID: 13623 RVA: 0x000F783A File Offset: 0x000F5A3A
		internal static string WRN_AlignmentMagnitude
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AlignmentMagnitude", null);
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06003538 RID: 13624 RVA: 0x000F7847 File Offset: 0x000F5A47
		internal static string HDN_UnusedExternAlias_Title
		{
			get
			{
				return CSharpResources.GetResourceString("HDN_UnusedExternAlias_Title", null);
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06003539 RID: 13625 RVA: 0x000F7854 File Offset: 0x000F5A54
		internal static string HDN_UnusedUsingDirective_Title
		{
			get
			{
				return CSharpResources.GetResourceString("HDN_UnusedUsingDirective_Title", null);
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x0600353A RID: 13626 RVA: 0x000F7861 File Offset: 0x000F5A61
		internal static string INF_UnableToLoadSomeTypesInAnalyzer_Title
		{
			get
			{
				return CSharpResources.GetResourceString("INF_UnableToLoadSomeTypesInAnalyzer_Title", null);
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x0600353B RID: 13627 RVA: 0x000F786E File Offset: 0x000F5A6E
		internal static string WRN_AlignmentMagnitude_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AlignmentMagnitude_Title", null);
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x0600353C RID: 13628 RVA: 0x000F787B File Offset: 0x000F5A7B
		internal static string ERR_ConstantStringTooLong
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConstantStringTooLong", null);
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x0600353D RID: 13629 RVA: 0x000F7888 File Offset: 0x000F5A88
		internal static string ERR_TupleTooFewElements
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TupleTooFewElements", null);
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x0600353E RID: 13630 RVA: 0x000F7895 File Offset: 0x000F5A95
		internal static string ERR_DebugEntryPointNotSourceMethodDefinition
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DebugEntryPointNotSourceMethodDefinition", null);
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x0600353F RID: 13631 RVA: 0x000F78A2 File Offset: 0x000F5AA2
		internal static string ERR_LoadDirectiveOnlyAllowedInScripts
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LoadDirectiveOnlyAllowedInScripts", null);
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06003540 RID: 13632 RVA: 0x000F78AF File Offset: 0x000F5AAF
		internal static string ERR_PPLoadFollowsToken
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PPLoadFollowsToken", null);
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06003541 RID: 13633 RVA: 0x000F78BC File Offset: 0x000F5ABC
		internal static string CouldNotFindFile
		{
			get
			{
				return CSharpResources.GetResourceString("CouldNotFindFile", null);
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06003542 RID: 13634 RVA: 0x000F78C9 File Offset: 0x000F5AC9
		internal static string SyntaxTreeFromLoadNoRemoveReplace
		{
			get
			{
				return CSharpResources.GetResourceString("SyntaxTreeFromLoadNoRemoveReplace", null);
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06003543 RID: 13635 RVA: 0x000F78D6 File Offset: 0x000F5AD6
		internal static string ERR_SourceFileReferencesNotSupported
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SourceFileReferencesNotSupported", null);
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06003544 RID: 13636 RVA: 0x000F78E3 File Offset: 0x000F5AE3
		internal static string ERR_InvalidPathMap
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidPathMap", null);
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06003545 RID: 13637 RVA: 0x000F78F0 File Offset: 0x000F5AF0
		internal static string ERR_InvalidReal
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidReal", null);
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06003546 RID: 13638 RVA: 0x000F78FD File Offset: 0x000F5AFD
		internal static string ERR_AutoPropertyCannotBeRefReturning
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AutoPropertyCannotBeRefReturning", null);
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06003547 RID: 13639 RVA: 0x000F790A File Offset: 0x000F5B0A
		internal static string ERR_RefPropertyMustHaveGetAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefPropertyMustHaveGetAccessor", null);
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06003548 RID: 13640 RVA: 0x000F7917 File Offset: 0x000F5B17
		internal static string ERR_RefPropertyCannotHaveSetAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefPropertyCannotHaveSetAccessor", null);
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06003549 RID: 13641 RVA: 0x000F7924 File Offset: 0x000F5B24
		internal static string ERR_CantChangeRefReturnOnOverride
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantChangeRefReturnOnOverride", null);
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x0600354A RID: 13642 RVA: 0x000F7931 File Offset: 0x000F5B31
		internal static string ERR_CantChangeInitOnlyOnOverride
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantChangeInitOnlyOnOverride", null);
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x0600354B RID: 13643 RVA: 0x000F793E File Offset: 0x000F5B3E
		internal static string ERR_MustNotHaveRefReturn
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MustNotHaveRefReturn", null);
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x0600354C RID: 13644 RVA: 0x000F794B File Offset: 0x000F5B4B
		internal static string ERR_MustHaveRefReturn
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MustHaveRefReturn", null);
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x0600354D RID: 13645 RVA: 0x000F7958 File Offset: 0x000F5B58
		internal static string ERR_RefReturnMustHaveIdentityConversion
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnMustHaveIdentityConversion", null);
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x0600354E RID: 13646 RVA: 0x000F7965 File Offset: 0x000F5B65
		internal static string ERR_CloseUnimplementedInterfaceMemberWrongRefReturn
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CloseUnimplementedInterfaceMemberWrongRefReturn", null);
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x0600354F RID: 13647 RVA: 0x000F7972 File Offset: 0x000F5B72
		internal static string ERR_CloseUnimplementedInterfaceMemberWrongInitOnly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CloseUnimplementedInterfaceMemberWrongInitOnly", null);
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06003550 RID: 13648 RVA: 0x000F797F File Offset: 0x000F5B7F
		internal static string ERR_BadIteratorReturnRef
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadIteratorReturnRef", null);
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06003551 RID: 13649 RVA: 0x000F798C File Offset: 0x000F5B8C
		internal static string ERR_BadRefReturnExpressionTree
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadRefReturnExpressionTree", null);
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06003552 RID: 13650 RVA: 0x000F7999 File Offset: 0x000F5B99
		internal static string ERR_RefReturningCallInExpressionTree
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturningCallInExpressionTree", null);
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06003553 RID: 13651 RVA: 0x000F79A6 File Offset: 0x000F5BA6
		internal static string ERR_RefReturnLvalueExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnLvalueExpected", null);
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06003554 RID: 13652 RVA: 0x000F79B3 File Offset: 0x000F5BB3
		internal static string ERR_RefReturnNonreturnableLocal
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnNonreturnableLocal", null);
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06003555 RID: 13653 RVA: 0x000F79C0 File Offset: 0x000F5BC0
		internal static string ERR_RefReturnNonreturnableLocal2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnNonreturnableLocal2", null);
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06003556 RID: 13654 RVA: 0x000F79CD File Offset: 0x000F5BCD
		internal static string ERR_RefReturnReadonlyLocal
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnReadonlyLocal", null);
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06003557 RID: 13655 RVA: 0x000F79DA File Offset: 0x000F5BDA
		internal static string ERR_RefReturnRangeVariable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnRangeVariable", null);
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06003558 RID: 13656 RVA: 0x000F79E7 File Offset: 0x000F5BE7
		internal static string ERR_RefReturnReadonlyLocalCause
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnReadonlyLocalCause", null);
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06003559 RID: 13657 RVA: 0x000F79F4 File Offset: 0x000F5BF4
		internal static string ERR_RefReturnReadonlyLocal2Cause
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnReadonlyLocal2Cause", null);
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x0600355A RID: 13658 RVA: 0x000F7A01 File Offset: 0x000F5C01
		internal static string ERR_RefReturnReadonly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnReadonly", null);
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x0600355B RID: 13659 RVA: 0x000F7A0E File Offset: 0x000F5C0E
		internal static string ERR_RefReturnReadonlyStatic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnReadonlyStatic", null);
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x0600355C RID: 13660 RVA: 0x000F7A1B File Offset: 0x000F5C1B
		internal static string ERR_RefReturnReadonly2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnReadonly2", null);
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x0600355D RID: 13661 RVA: 0x000F7A28 File Offset: 0x000F5C28
		internal static string ERR_RefReturnReadonlyStatic2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnReadonlyStatic2", null);
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x0600355E RID: 13662 RVA: 0x000F7A35 File Offset: 0x000F5C35
		internal static string ERR_RefReturnParameter
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnParameter", null);
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x0600355F RID: 13663 RVA: 0x000F7A42 File Offset: 0x000F5C42
		internal static string ERR_RefReturnParameter2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnParameter2", null);
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x000F7A4F File Offset: 0x000F5C4F
		internal static string ERR_RefReturnLocal
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnLocal", null);
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06003561 RID: 13665 RVA: 0x000F7A5C File Offset: 0x000F5C5C
		internal static string ERR_RefReturnLocal2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnLocal2", null);
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06003562 RID: 13666 RVA: 0x000F7A69 File Offset: 0x000F5C69
		internal static string ERR_RefReturnStructThis
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturnStructThis", null);
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06003563 RID: 13667 RVA: 0x000F7A76 File Offset: 0x000F5C76
		internal static string ERR_EscapeOther
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EscapeOther", null);
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06003564 RID: 13668 RVA: 0x000F7A83 File Offset: 0x000F5C83
		internal static string ERR_EscapeLocal
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EscapeLocal", null);
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06003565 RID: 13669 RVA: 0x000F7A90 File Offset: 0x000F5C90
		internal static string ERR_EscapeCall
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EscapeCall", null);
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06003566 RID: 13670 RVA: 0x000F7A9D File Offset: 0x000F5C9D
		internal static string ERR_EscapeCall2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EscapeCall2", null);
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06003567 RID: 13671 RVA: 0x000F7AAA File Offset: 0x000F5CAA
		internal static string ERR_CallArgMixing
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CallArgMixing", null);
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06003568 RID: 13672 RVA: 0x000F7AB7 File Offset: 0x000F5CB7
		internal static string ERR_MismatchedRefEscapeInTernary
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MismatchedRefEscapeInTernary", null);
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06003569 RID: 13673 RVA: 0x000F7AC4 File Offset: 0x000F5CC4
		internal static string ERR_EscapeStackAlloc
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EscapeStackAlloc", null);
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x0600356A RID: 13674 RVA: 0x000F7AD1 File Offset: 0x000F5CD1
		internal static string ERR_InitializeByValueVariableWithReference
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InitializeByValueVariableWithReference", null);
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x0600356B RID: 13675 RVA: 0x000F7ADE File Offset: 0x000F5CDE
		internal static string ERR_InitializeByReferenceVariableWithValue
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InitializeByReferenceVariableWithValue", null);
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x0600356C RID: 13676 RVA: 0x000F7AEB File Offset: 0x000F5CEB
		internal static string ERR_RefAssignmentMustHaveIdentityConversion
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefAssignmentMustHaveIdentityConversion", null);
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x0600356D RID: 13677 RVA: 0x000F7AF8 File Offset: 0x000F5CF8
		internal static string ERR_ByReferenceVariableMustBeInitialized
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ByReferenceVariableMustBeInitialized", null);
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x0600356E RID: 13678 RVA: 0x000F7B05 File Offset: 0x000F5D05
		internal static string ERR_AnonDelegateCantUseLocal
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AnonDelegateCantUseLocal", null);
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x0600356F RID: 13679 RVA: 0x000F7B12 File Offset: 0x000F5D12
		internal static string ERR_BadIteratorLocalType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadIteratorLocalType", null);
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06003570 RID: 13680 RVA: 0x000F7B1F File Offset: 0x000F5D1F
		internal static string ERR_BadAsyncLocalType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAsyncLocalType", null);
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06003571 RID: 13681 RVA: 0x000F7B2C File Offset: 0x000F5D2C
		internal static string ERR_RefReturningCallAndAwait
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefReturningCallAndAwait", null);
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06003572 RID: 13682 RVA: 0x000F7B39 File Offset: 0x000F5D39
		internal static string ERR_RefConditionalAndAwait
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefConditionalAndAwait", null);
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06003573 RID: 13683 RVA: 0x000F7B46 File Offset: 0x000F5D46
		internal static string ERR_RefConditionalNeedsTwoRefs
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefConditionalNeedsTwoRefs", null);
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06003574 RID: 13684 RVA: 0x000F7B53 File Offset: 0x000F5D53
		internal static string ERR_RefConditionalDifferentTypes
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefConditionalDifferentTypes", null);
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06003575 RID: 13685 RVA: 0x000F7B60 File Offset: 0x000F5D60
		internal static string ERR_ExpressionTreeContainsLocalFunction
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsLocalFunction", null);
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06003576 RID: 13686 RVA: 0x000F7B6D File Offset: 0x000F5D6D
		internal static string ERR_DynamicLocalFunctionParamsParameter
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DynamicLocalFunctionParamsParameter", null);
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06003577 RID: 13687 RVA: 0x000F7B7A File Offset: 0x000F5D7A
		internal static string SyntaxTreeIsNotASubmission
		{
			get
			{
				return CSharpResources.GetResourceString("SyntaxTreeIsNotASubmission", null);
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06003578 RID: 13688 RVA: 0x000F7B87 File Offset: 0x000F5D87
		internal static string ERR_TooManyUserStrings
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TooManyUserStrings", null);
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06003579 RID: 13689 RVA: 0x000F7B94 File Offset: 0x000F5D94
		internal static string ERR_PatternNullableType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PatternNullableType", null);
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x0600357A RID: 13690 RVA: 0x000F7BA1 File Offset: 0x000F5DA1
		internal static string ERR_IsNullableType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IsNullableType", null);
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x0600357B RID: 13691 RVA: 0x000F7BAE File Offset: 0x000F5DAE
		internal static string ERR_AsNullableType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AsNullableType", null);
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x0600357C RID: 13692 RVA: 0x000F7BBB File Offset: 0x000F5DBB
		internal static string ERR_BadPatternExpression
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadPatternExpression", null);
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x0600357D RID: 13693 RVA: 0x000F7BC8 File Offset: 0x000F5DC8
		internal static string ERR_PeWritingFailure
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PeWritingFailure", null);
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x0600357E RID: 13694 RVA: 0x000F7BD5 File Offset: 0x000F5DD5
		internal static string ERR_TupleDuplicateElementName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TupleDuplicateElementName", null);
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x0600357F RID: 13695 RVA: 0x000F7BE2 File Offset: 0x000F5DE2
		internal static string ERR_TupleReservedElementName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TupleReservedElementName", null);
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06003580 RID: 13696 RVA: 0x000F7BEF File Offset: 0x000F5DEF
		internal static string ERR_TupleReservedElementNameAnyPosition
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TupleReservedElementNameAnyPosition", null);
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06003581 RID: 13697 RVA: 0x000F7BFC File Offset: 0x000F5DFC
		internal static string ERR_PredefinedTypeMemberNotFoundInAssembly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PredefinedTypeMemberNotFoundInAssembly", null);
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06003582 RID: 13698 RVA: 0x000F7C09 File Offset: 0x000F5E09
		internal static string IDS_FeatureTuples
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureTuples", null);
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06003583 RID: 13699 RVA: 0x000F7C16 File Offset: 0x000F5E16
		internal static string ERR_MissingDeconstruct
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MissingDeconstruct", null);
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06003584 RID: 13700 RVA: 0x000F7C23 File Offset: 0x000F5E23
		internal static string ERR_DeconstructRequiresExpression
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DeconstructRequiresExpression", null);
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06003585 RID: 13701 RVA: 0x000F7C30 File Offset: 0x000F5E30
		internal static string ERR_SwitchExpressionValueExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SwitchExpressionValueExpected", null);
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06003586 RID: 13702 RVA: 0x000F7C3D File Offset: 0x000F5E3D
		internal static string ERR_SwitchCaseSubsumed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SwitchCaseSubsumed", null);
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06003587 RID: 13703 RVA: 0x000F7C4A File Offset: 0x000F5E4A
		internal static string ERR_StdInOptionProvidedButConsoleInputIsNotRedirected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StdInOptionProvidedButConsoleInputIsNotRedirected", null);
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06003588 RID: 13704 RVA: 0x000F7C57 File Offset: 0x000F5E57
		internal static string ERR_SwitchArmSubsumed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SwitchArmSubsumed", null);
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06003589 RID: 13705 RVA: 0x000F7C64 File Offset: 0x000F5E64
		internal static string ERR_PatternWrongType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PatternWrongType", null);
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x0600358A RID: 13706 RVA: 0x000F7C71 File Offset: 0x000F5E71
		internal static string ERR_ConstantPatternVsOpenType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConstantPatternVsOpenType", null);
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x0600358B RID: 13707 RVA: 0x000F7C7E File Offset: 0x000F5E7E
		internal static string WRN_AttributeIgnoredWhenPublicSigning
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AttributeIgnoredWhenPublicSigning", null);
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x0600358C RID: 13708 RVA: 0x000F7C8B File Offset: 0x000F5E8B
		internal static string WRN_AttributeIgnoredWhenPublicSigning_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AttributeIgnoredWhenPublicSigning_Title", null);
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x0600358D RID: 13709 RVA: 0x000F7C98 File Offset: 0x000F5E98
		internal static string ERR_OptionMustBeAbsolutePath
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OptionMustBeAbsolutePath", null);
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x0600358E RID: 13710 RVA: 0x000F7CA5 File Offset: 0x000F5EA5
		internal static string ERR_ConversionNotTupleCompatible
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConversionNotTupleCompatible", null);
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x0600358F RID: 13711 RVA: 0x000F7CB2 File Offset: 0x000F5EB2
		internal static string IDS_FeatureOutVar
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureOutVar", null);
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06003590 RID: 13712 RVA: 0x000F7CBF File Offset: 0x000F5EBF
		internal static string ERR_ImplicitlyTypedOutVariableUsedInTheSameArgumentList
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImplicitlyTypedOutVariableUsedInTheSameArgumentList", null);
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06003591 RID: 13713 RVA: 0x000F7CCC File Offset: 0x000F5ECC
		internal static string ERR_TypeInferenceFailedForImplicitlyTypedOutVariable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TypeInferenceFailedForImplicitlyTypedOutVariable", null);
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06003592 RID: 13714 RVA: 0x000F7CD9 File Offset: 0x000F5ED9
		internal static string ERR_TypeInferenceFailedForImplicitlyTypedDeconstructionVariable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TypeInferenceFailedForImplicitlyTypedDeconstructionVariable", null);
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06003593 RID: 13715 RVA: 0x000F7CE6 File Offset: 0x000F5EE6
		internal static string ERR_DiscardTypeInferenceFailed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DiscardTypeInferenceFailed", null);
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06003594 RID: 13716 RVA: 0x000F7CF3 File Offset: 0x000F5EF3
		internal static string ERR_DeconstructWrongCardinality
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DeconstructWrongCardinality", null);
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06003595 RID: 13717 RVA: 0x000F7D00 File Offset: 0x000F5F00
		internal static string ERR_CannotDeconstructDynamic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CannotDeconstructDynamic", null);
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06003596 RID: 13718 RVA: 0x000F7D0D File Offset: 0x000F5F0D
		internal static string ERR_DeconstructTooFewElements
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DeconstructTooFewElements", null);
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06003597 RID: 13719 RVA: 0x000F7D1A File Offset: 0x000F5F1A
		internal static string WRN_TupleLiteralNameMismatch
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TupleLiteralNameMismatch", null);
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06003598 RID: 13720 RVA: 0x000F7D27 File Offset: 0x000F5F27
		internal static string WRN_TupleLiteralNameMismatch_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TupleLiteralNameMismatch_Title", null);
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06003599 RID: 13721 RVA: 0x000F7D34 File Offset: 0x000F5F34
		internal static string WRN_TupleBinopLiteralNameMismatch
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TupleBinopLiteralNameMismatch", null);
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x0600359A RID: 13722 RVA: 0x000F7D41 File Offset: 0x000F5F41
		internal static string WRN_TupleBinopLiteralNameMismatch_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TupleBinopLiteralNameMismatch_Title", null);
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x0600359B RID: 13723 RVA: 0x000F7D4E File Offset: 0x000F5F4E
		internal static string ERR_PredefinedValueTupleTypeMustBeStruct
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PredefinedValueTupleTypeMustBeStruct", null);
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x0600359C RID: 13724 RVA: 0x000F7D5B File Offset: 0x000F5F5B
		internal static string ERR_NewWithTupleTypeSyntax
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NewWithTupleTypeSyntax", null);
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x0600359D RID: 13725 RVA: 0x000F7D68 File Offset: 0x000F5F68
		internal static string ERR_DeconstructionVarFormDisallowsSpecificType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DeconstructionVarFormDisallowsSpecificType", null);
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x0600359E RID: 13726 RVA: 0x000F7D75 File Offset: 0x000F5F75
		internal static string ERR_TupleElementNamesAttributeMissing
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TupleElementNamesAttributeMissing", null);
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x0600359F RID: 13727 RVA: 0x000F7D82 File Offset: 0x000F5F82
		internal static string ERR_ExplicitTupleElementNamesAttribute
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExplicitTupleElementNamesAttribute", null);
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x060035A0 RID: 13728 RVA: 0x000F7D8F File Offset: 0x000F5F8F
		internal static string ERR_ExpressionTreeContainsOutVariable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsOutVariable", null);
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x060035A1 RID: 13729 RVA: 0x000F7D9C File Offset: 0x000F5F9C
		internal static string ERR_ExpressionTreeContainsDiscard
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsDiscard", null);
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x060035A2 RID: 13730 RVA: 0x000F7DA9 File Offset: 0x000F5FA9
		internal static string ERR_ExpressionTreeContainsIsMatch
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsIsMatch", null);
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x060035A3 RID: 13731 RVA: 0x000F7DB6 File Offset: 0x000F5FB6
		internal static string ERR_ExpressionTreeContainsTupleLiteral
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsTupleLiteral", null);
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x060035A4 RID: 13732 RVA: 0x000F7DC3 File Offset: 0x000F5FC3
		internal static string ERR_ExpressionTreeContainsTupleConversion
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsTupleConversion", null);
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x060035A5 RID: 13733 RVA: 0x000F7DD0 File Offset: 0x000F5FD0
		internal static string ERR_SourceLinkRequiresPdb
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SourceLinkRequiresPdb", null);
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x060035A6 RID: 13734 RVA: 0x000F7DDD File Offset: 0x000F5FDD
		internal static string ERR_CannotEmbedWithoutPdb
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CannotEmbedWithoutPdb", null);
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x060035A7 RID: 13735 RVA: 0x000F7DEA File Offset: 0x000F5FEA
		internal static string ERR_InvalidInstrumentationKind
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidInstrumentationKind", null);
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x060035A8 RID: 13736 RVA: 0x000F7DF7 File Offset: 0x000F5FF7
		internal static string ERR_InvalidHashAlgorithmName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidHashAlgorithmName", null);
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x060035A9 RID: 13737 RVA: 0x000F7E04 File Offset: 0x000F6004
		internal static string ERR_VarInvocationLvalueReserved
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_VarInvocationLvalueReserved", null);
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x060035AA RID: 13738 RVA: 0x000F7E11 File Offset: 0x000F6011
		internal static string ERR_SemiOrLBraceOrArrowExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SemiOrLBraceOrArrowExpected", null);
			}
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x060035AB RID: 13739 RVA: 0x000F7E1E File Offset: 0x000F601E
		internal static string ERR_ThrowMisplaced
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ThrowMisplaced", null);
			}
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x060035AC RID: 13740 RVA: 0x000F7E2B File Offset: 0x000F602B
		internal static string ERR_DeclarationExpressionNotPermitted
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DeclarationExpressionNotPermitted", null);
			}
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x060035AD RID: 13741 RVA: 0x000F7E38 File Offset: 0x000F6038
		internal static string ERR_MustDeclareForeachIteration
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MustDeclareForeachIteration", null);
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x060035AE RID: 13742 RVA: 0x000F7E45 File Offset: 0x000F6045
		internal static string ERR_TupleElementNamesInDeconstruction
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TupleElementNamesInDeconstruction", null);
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x060035AF RID: 13743 RVA: 0x000F7E52 File Offset: 0x000F6052
		internal static string ERR_PossibleBadNegCast
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PossibleBadNegCast", null);
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x060035B0 RID: 13744 RVA: 0x000F7E5F File Offset: 0x000F605F
		internal static string ERR_ExpressionTreeContainsThrowExpression
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsThrowExpression", null);
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x060035B1 RID: 13745 RVA: 0x000F7E6C File Offset: 0x000F606C
		internal static string ERR_ExpressionTreeContainsWithExpression
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsWithExpression", null);
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x060035B2 RID: 13746 RVA: 0x000F7E79 File Offset: 0x000F6079
		internal static string ERR_BadAssemblyName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAssemblyName", null);
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x060035B3 RID: 13747 RVA: 0x000F7E86 File Offset: 0x000F6086
		internal static string ERR_BadAsyncMethodBuilderTaskProperty
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadAsyncMethodBuilderTaskProperty", null);
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x060035B4 RID: 13748 RVA: 0x000F7E93 File Offset: 0x000F6093
		internal static string ERR_AttributesInLocalFuncDecl
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AttributesInLocalFuncDecl", null);
			}
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x060035B5 RID: 13749 RVA: 0x000F7EA0 File Offset: 0x000F60A0
		internal static string ERR_TypeForwardedToMultipleAssemblies
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TypeForwardedToMultipleAssemblies", null);
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x060035B6 RID: 13750 RVA: 0x000F7EAD File Offset: 0x000F60AD
		internal static string ERR_PatternDynamicType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PatternDynamicType", null);
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x060035B7 RID: 13751 RVA: 0x000F7EBA File Offset: 0x000F60BA
		internal static string ERR_BadDocumentationMode
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadDocumentationMode", null);
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x060035B8 RID: 13752 RVA: 0x000F7EC7 File Offset: 0x000F60C7
		internal static string ERR_BadSourceCodeKind
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadSourceCodeKind", null);
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x060035B9 RID: 13753 RVA: 0x000F7ED4 File Offset: 0x000F60D4
		internal static string ERR_BadLanguageVersion
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadLanguageVersion", null);
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x060035BA RID: 13754 RVA: 0x000F7EE1 File Offset: 0x000F60E1
		internal static string ERR_InvalidPreprocessingSymbol
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidPreprocessingSymbol", null);
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x060035BB RID: 13755 RVA: 0x000F7EEE File Offset: 0x000F60EE
		internal static string ERR_FeatureNotAvailableInVersion7_1
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FeatureNotAvailableInVersion7_1", null);
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x060035BC RID: 13756 RVA: 0x000F7EFB File Offset: 0x000F60FB
		internal static string ERR_FeatureNotAvailableInVersion7_2
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FeatureNotAvailableInVersion7_2", null);
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x060035BD RID: 13757 RVA: 0x000F7F08 File Offset: 0x000F6108
		internal static string ERR_FeatureNotAvailableInVersion7_3
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FeatureNotAvailableInVersion7_3", null);
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x060035BE RID: 13758 RVA: 0x000F7F15 File Offset: 0x000F6115
		internal static string ERR_FeatureNotAvailableInVersion8
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FeatureNotAvailableInVersion8", null);
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x060035BF RID: 13759 RVA: 0x000F7F22 File Offset: 0x000F6122
		internal static string ERR_LanguageVersionCannotHaveLeadingZeroes
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LanguageVersionCannotHaveLeadingZeroes", null);
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x060035C0 RID: 13760 RVA: 0x000F7F2F File Offset: 0x000F612F
		internal static string ERR_VoidAssignment
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_VoidAssignment", null);
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x060035C1 RID: 13761 RVA: 0x000F7F3C File Offset: 0x000F613C
		internal static string WRN_Experimental
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_Experimental", null);
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x060035C2 RID: 13762 RVA: 0x000F7F49 File Offset: 0x000F6149
		internal static string WRN_Experimental_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_Experimental_Title", null);
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x060035C3 RID: 13763 RVA: 0x000F7F56 File Offset: 0x000F6156
		internal static string ERR_CompilerAndLanguageVersion
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CompilerAndLanguageVersion", null);
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x060035C4 RID: 13764 RVA: 0x000F7F63 File Offset: 0x000F6163
		internal static string IDS_FeatureAsyncMain
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureAsyncMain", null);
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x060035C5 RID: 13765 RVA: 0x000F7F70 File Offset: 0x000F6170
		internal static string ERR_TupleInferredNamesNotAvailable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TupleInferredNamesNotAvailable", null);
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x060035C6 RID: 13766 RVA: 0x000F7F7D File Offset: 0x000F617D
		internal static string ERR_AltInterpolatedVerbatimStringsNotAvailable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AltInterpolatedVerbatimStringsNotAvailable", null);
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x060035C7 RID: 13767 RVA: 0x000F7F8A File Offset: 0x000F618A
		internal static string WRN_AttributesOnBackingFieldsNotAvailable
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AttributesOnBackingFieldsNotAvailable", null);
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x060035C8 RID: 13768 RVA: 0x000F7F97 File Offset: 0x000F6197
		internal static string WRN_AttributesOnBackingFieldsNotAvailable_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AttributesOnBackingFieldsNotAvailable_Title", null);
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x060035C9 RID: 13769 RVA: 0x000F7FA4 File Offset: 0x000F61A4
		internal static string ERR_VoidInTuple
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_VoidInTuple", null);
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x060035CA RID: 13770 RVA: 0x000F7FB1 File Offset: 0x000F61B1
		internal static string IDS_FeatureNullableReferenceTypes
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureNullableReferenceTypes", null);
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x060035CB RID: 13771 RVA: 0x000F7FBE File Offset: 0x000F61BE
		internal static string IDS_FeaturePragmaWarningEnable
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeaturePragmaWarningEnable", null);
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x060035CC RID: 13772 RVA: 0x000F7FCB File Offset: 0x000F61CB
		internal static string WRN_ConvertingNullableToNonNullable
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ConvertingNullableToNonNullable", null);
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x060035CD RID: 13773 RVA: 0x000F7FD8 File Offset: 0x000F61D8
		internal static string WRN_ConvertingNullableToNonNullable_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ConvertingNullableToNonNullable_Title", null);
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x060035CE RID: 13774 RVA: 0x000F7FE5 File Offset: 0x000F61E5
		internal static string WRN_NullReferenceAssignment
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullReferenceAssignment", null);
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x060035CF RID: 13775 RVA: 0x000F7FF2 File Offset: 0x000F61F2
		internal static string WRN_NullReferenceAssignment_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullReferenceAssignment_Title", null);
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x060035D0 RID: 13776 RVA: 0x000F7FFF File Offset: 0x000F61FF
		internal static string WRN_NullReferenceReceiver
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullReferenceReceiver", null);
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x060035D1 RID: 13777 RVA: 0x000F800C File Offset: 0x000F620C
		internal static string WRN_NullReferenceReceiver_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullReferenceReceiver_Title", null);
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x060035D2 RID: 13778 RVA: 0x000F8019 File Offset: 0x000F6219
		internal static string WRN_NullReferenceReturn
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullReferenceReturn", null);
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x060035D3 RID: 13779 RVA: 0x000F8026 File Offset: 0x000F6226
		internal static string WRN_NullReferenceReturn_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullReferenceReturn_Title", null);
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x060035D4 RID: 13780 RVA: 0x000F8033 File Offset: 0x000F6233
		internal static string WRN_NullReferenceArgument
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullReferenceArgument", null);
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x060035D5 RID: 13781 RVA: 0x000F8040 File Offset: 0x000F6240
		internal static string WRN_NullReferenceArgument_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullReferenceArgument_Title", null);
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x060035D6 RID: 13782 RVA: 0x000F804D File Offset: 0x000F624D
		internal static string WRN_ThrowPossibleNull
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ThrowPossibleNull", null);
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x060035D7 RID: 13783 RVA: 0x000F805A File Offset: 0x000F625A
		internal static string WRN_ThrowPossibleNull_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ThrowPossibleNull_Title", null);
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x060035D8 RID: 13784 RVA: 0x000F8067 File Offset: 0x000F6267
		internal static string WRN_UnboxPossibleNull
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnboxPossibleNull", null);
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x060035D9 RID: 13785 RVA: 0x000F8074 File Offset: 0x000F6274
		internal static string WRN_UnboxPossibleNull_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnboxPossibleNull_Title", null);
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x060035DA RID: 13786 RVA: 0x000F8081 File Offset: 0x000F6281
		internal static string WRN_NullabilityMismatchInTypeOnOverride
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInTypeOnOverride", null);
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x060035DB RID: 13787 RVA: 0x000F808E File Offset: 0x000F628E
		internal static string WRN_NullabilityMismatchInTypeOnOverride_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInTypeOnOverride_Title", null);
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x060035DC RID: 13788 RVA: 0x000F809B File Offset: 0x000F629B
		internal static string WRN_NullabilityMismatchInReturnTypeOnOverride
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInReturnTypeOnOverride", null);
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x060035DD RID: 13789 RVA: 0x000F80A8 File Offset: 0x000F62A8
		internal static string WRN_NullabilityMismatchInReturnTypeOnOverride_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInReturnTypeOnOverride_Title", null);
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x060035DE RID: 13790 RVA: 0x000F80B5 File Offset: 0x000F62B5
		internal static string WRN_TopLevelNullabilityMismatchInReturnTypeOnOverride
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TopLevelNullabilityMismatchInReturnTypeOnOverride", null);
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x060035DF RID: 13791 RVA: 0x000F80C2 File Offset: 0x000F62C2
		internal static string WRN_TopLevelNullabilityMismatchInReturnTypeOnOverride_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TopLevelNullabilityMismatchInReturnTypeOnOverride_Title", null);
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x060035E0 RID: 13792 RVA: 0x000F80CF File Offset: 0x000F62CF
		internal static string WRN_NullabilityMismatchInParameterTypeOnOverride
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInParameterTypeOnOverride", null);
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x060035E1 RID: 13793 RVA: 0x000F80DC File Offset: 0x000F62DC
		internal static string WRN_NullabilityMismatchInParameterTypeOnOverride_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInParameterTypeOnOverride_Title", null);
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x060035E2 RID: 13794 RVA: 0x000F80E9 File Offset: 0x000F62E9
		internal static string WRN_TopLevelNullabilityMismatchInParameterTypeOnOverride
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TopLevelNullabilityMismatchInParameterTypeOnOverride", null);
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x060035E3 RID: 13795 RVA: 0x000F80F6 File Offset: 0x000F62F6
		internal static string WRN_TopLevelNullabilityMismatchInParameterTypeOnOverride_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TopLevelNullabilityMismatchInParameterTypeOnOverride_Title", null);
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x060035E4 RID: 13796 RVA: 0x000F8103 File Offset: 0x000F6303
		internal static string WRN_NullabilityMismatchInParameterTypeOnPartial
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInParameterTypeOnPartial", null);
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x060035E5 RID: 13797 RVA: 0x000F8110 File Offset: 0x000F6310
		internal static string WRN_NullabilityMismatchInParameterTypeOnPartial_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInParameterTypeOnPartial_Title", null);
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x060035E6 RID: 13798 RVA: 0x000F811D File Offset: 0x000F631D
		internal static string WRN_NullabilityMismatchInReturnTypeOnPartial
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInReturnTypeOnPartial", null);
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x060035E7 RID: 13799 RVA: 0x000F812A File Offset: 0x000F632A
		internal static string WRN_NullabilityMismatchInReturnTypeOnPartial_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInReturnTypeOnPartial_Title", null);
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x060035E8 RID: 13800 RVA: 0x000F8137 File Offset: 0x000F6337
		internal static string WRN_NullabilityMismatchInTypeOnImplicitImplementation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInTypeOnImplicitImplementation", null);
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x060035E9 RID: 13801 RVA: 0x000F8144 File Offset: 0x000F6344
		internal static string WRN_NullabilityMismatchInTypeOnImplicitImplementation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInTypeOnImplicitImplementation_Title", null);
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x060035EA RID: 13802 RVA: 0x000F8151 File Offset: 0x000F6351
		internal static string WRN_NullabilityMismatchInReturnTypeOnImplicitImplementation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInReturnTypeOnImplicitImplementation", null);
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x060035EB RID: 13803 RVA: 0x000F815E File Offset: 0x000F635E
		internal static string WRN_NullabilityMismatchInReturnTypeOnImplicitImplementation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInReturnTypeOnImplicitImplementation_Title", null);
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x060035EC RID: 13804 RVA: 0x000F816B File Offset: 0x000F636B
		internal static string WRN_NullabilityMismatchInParameterTypeOnImplicitImplementation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInParameterTypeOnImplicitImplementation", null);
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x060035ED RID: 13805 RVA: 0x000F8178 File Offset: 0x000F6378
		internal static string WRN_NullabilityMismatchInParameterTypeOnImplicitImplementation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInParameterTypeOnImplicitImplementation_Title", null);
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x060035EE RID: 13806 RVA: 0x000F8185 File Offset: 0x000F6385
		internal static string WRN_TopLevelNullabilityMismatchInReturnTypeOnImplicitImplementation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TopLevelNullabilityMismatchInReturnTypeOnImplicitImplementation", null);
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x060035EF RID: 13807 RVA: 0x000F8192 File Offset: 0x000F6392
		internal static string WRN_TopLevelNullabilityMismatchInReturnTypeOnImplicitImplementation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TopLevelNullabilityMismatchInReturnTypeOnImplicitImplementation_Title", null);
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x060035F0 RID: 13808 RVA: 0x000F819F File Offset: 0x000F639F
		internal static string WRN_TopLevelNullabilityMismatchInParameterTypeOnImplicitImplementation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TopLevelNullabilityMismatchInParameterTypeOnImplicitImplementation", null);
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x060035F1 RID: 13809 RVA: 0x000F81AC File Offset: 0x000F63AC
		internal static string WRN_TopLevelNullabilityMismatchInParameterTypeOnImplicitImplementation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TopLevelNullabilityMismatchInParameterTypeOnImplicitImplementation_Title", null);
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x060035F2 RID: 13810 RVA: 0x000F81B9 File Offset: 0x000F63B9
		internal static string WRN_NullabilityMismatchInTypeOnExplicitImplementation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInTypeOnExplicitImplementation", null);
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x060035F3 RID: 13811 RVA: 0x000F81C6 File Offset: 0x000F63C6
		internal static string WRN_NullabilityMismatchInTypeOnExplicitImplementation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInTypeOnExplicitImplementation_Title", null);
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x060035F4 RID: 13812 RVA: 0x000F81D3 File Offset: 0x000F63D3
		internal static string WRN_NullabilityMismatchInReturnTypeOnExplicitImplementation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInReturnTypeOnExplicitImplementation", null);
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x060035F5 RID: 13813 RVA: 0x000F81E0 File Offset: 0x000F63E0
		internal static string WRN_NullabilityMismatchInReturnTypeOnExplicitImplementation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInReturnTypeOnExplicitImplementation_Title", null);
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x060035F6 RID: 13814 RVA: 0x000F81ED File Offset: 0x000F63ED
		internal static string WRN_NullabilityMismatchInParameterTypeOnExplicitImplementation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInParameterTypeOnExplicitImplementation", null);
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x060035F7 RID: 13815 RVA: 0x000F81FA File Offset: 0x000F63FA
		internal static string WRN_NullabilityMismatchInParameterTypeOnExplicitImplementation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInParameterTypeOnExplicitImplementation_Title", null);
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x060035F8 RID: 13816 RVA: 0x000F8207 File Offset: 0x000F6407
		internal static string WRN_TopLevelNullabilityMismatchInReturnTypeOnExplicitImplementation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TopLevelNullabilityMismatchInReturnTypeOnExplicitImplementation", null);
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x060035F9 RID: 13817 RVA: 0x000F8214 File Offset: 0x000F6414
		internal static string WRN_TopLevelNullabilityMismatchInReturnTypeOnExplicitImplementation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TopLevelNullabilityMismatchInReturnTypeOnExplicitImplementation_Title", null);
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x060035FA RID: 13818 RVA: 0x000F8221 File Offset: 0x000F6421
		internal static string WRN_TopLevelNullabilityMismatchInParameterTypeOnExplicitImplementation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TopLevelNullabilityMismatchInParameterTypeOnExplicitImplementation", null);
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x060035FB RID: 13819 RVA: 0x000F822E File Offset: 0x000F642E
		internal static string WRN_TopLevelNullabilityMismatchInParameterTypeOnExplicitImplementation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_TopLevelNullabilityMismatchInParameterTypeOnExplicitImplementation_Title", null);
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x060035FC RID: 13820 RVA: 0x000F823B File Offset: 0x000F643B
		internal static string WRN_UninitializedNonNullableField
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UninitializedNonNullableField", null);
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x060035FD RID: 13821 RVA: 0x000F8248 File Offset: 0x000F6448
		internal static string WRN_UninitializedNonNullableField_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UninitializedNonNullableField_Title", null);
			}
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x060035FE RID: 13822 RVA: 0x000F8255 File Offset: 0x000F6455
		internal static string WRN_NullabilityMismatchInAssignment
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInAssignment", null);
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x060035FF RID: 13823 RVA: 0x000F8262 File Offset: 0x000F6462
		internal static string WRN_NullabilityMismatchInAssignment_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInAssignment_Title", null);
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06003600 RID: 13824 RVA: 0x000F826F File Offset: 0x000F646F
		internal static string WRN_ImplicitCopyInReadOnlyMember
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ImplicitCopyInReadOnlyMember", null);
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06003601 RID: 13825 RVA: 0x000F827C File Offset: 0x000F647C
		internal static string WRN_ImplicitCopyInReadOnlyMember_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ImplicitCopyInReadOnlyMember_Title", null);
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06003602 RID: 13826 RVA: 0x000F8289 File Offset: 0x000F6489
		internal static string ERR_StaticMemberCantBeReadOnly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StaticMemberCantBeReadOnly", null);
			}
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06003603 RID: 13827 RVA: 0x000F8296 File Offset: 0x000F6496
		internal static string ERR_AutoSetterCantBeReadOnly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AutoSetterCantBeReadOnly", null);
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06003604 RID: 13828 RVA: 0x000F82A3 File Offset: 0x000F64A3
		internal static string ERR_AutoPropertyWithSetterCantBeReadOnly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AutoPropertyWithSetterCantBeReadOnly", null);
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06003605 RID: 13829 RVA: 0x000F82B0 File Offset: 0x000F64B0
		internal static string ERR_InvalidPropertyReadOnlyMods
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidPropertyReadOnlyMods", null);
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06003606 RID: 13830 RVA: 0x000F82BD File Offset: 0x000F64BD
		internal static string ERR_DuplicatePropertyReadOnlyMods
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicatePropertyReadOnlyMods", null);
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06003607 RID: 13831 RVA: 0x000F82CA File Offset: 0x000F64CA
		internal static string ERR_FieldLikeEventCantBeReadOnly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FieldLikeEventCantBeReadOnly", null);
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06003608 RID: 13832 RVA: 0x000F82D7 File Offset: 0x000F64D7
		internal static string ERR_PartialMethodReadOnlyDifference
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodReadOnlyDifference", null);
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06003609 RID: 13833 RVA: 0x000F82E4 File Offset: 0x000F64E4
		internal static string ERR_ReadOnlyModMissingAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ReadOnlyModMissingAccessor", null);
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x0600360A RID: 13834 RVA: 0x000F82F1 File Offset: 0x000F64F1
		internal static string WRN_NullabilityMismatchInArgument
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInArgument", null);
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x0600360B RID: 13835 RVA: 0x000F82FE File Offset: 0x000F64FE
		internal static string WRN_NullabilityMismatchInArgument_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInArgument_Title", null);
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x0600360C RID: 13836 RVA: 0x000F830B File Offset: 0x000F650B
		internal static string WRN_NullabilityMismatchInArgumentForOutput
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInArgumentForOutput", null);
			}
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x000F8318 File Offset: 0x000F6518
		internal static string WRN_NullabilityMismatchInArgumentForOutput_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInArgumentForOutput_Title", null);
			}
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x0600360E RID: 13838 RVA: 0x000F8325 File Offset: 0x000F6525
		internal static string WRN_DisallowNullAttributeForbidsMaybeNullAssignment
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DisallowNullAttributeForbidsMaybeNullAssignment", null);
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x0600360F RID: 13839 RVA: 0x000F8332 File Offset: 0x000F6532
		internal static string WRN_DisallowNullAttributeForbidsMaybeNullAssignment_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DisallowNullAttributeForbidsMaybeNullAssignment_Title", null);
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06003610 RID: 13840 RVA: 0x000F833F File Offset: 0x000F653F
		internal static string WRN_ParameterConditionallyDisallowsNull
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ParameterConditionallyDisallowsNull", null);
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06003611 RID: 13841 RVA: 0x000F834C File Offset: 0x000F654C
		internal static string WRN_ParameterConditionallyDisallowsNull_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ParameterConditionallyDisallowsNull_Title", null);
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06003612 RID: 13842 RVA: 0x000F8359 File Offset: 0x000F6559
		internal static string WRN_ParameterDisallowsNull
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ParameterDisallowsNull", null);
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06003613 RID: 13843 RVA: 0x000F8366 File Offset: 0x000F6566
		internal static string WRN_ParameterDisallowsNull_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ParameterDisallowsNull_Title", null);
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06003614 RID: 13844 RVA: 0x000F8373 File Offset: 0x000F6573
		internal static string WRN_ParameterNotNullIfNotNull
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ParameterNotNullIfNotNull", null);
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06003615 RID: 13845 RVA: 0x000F8380 File Offset: 0x000F6580
		internal static string WRN_ParameterNotNullIfNotNull_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ParameterNotNullIfNotNull_Title", null);
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06003616 RID: 13846 RVA: 0x000F838D File Offset: 0x000F658D
		internal static string WRN_ReturnNotNullIfNotNull
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ReturnNotNullIfNotNull", null);
			}
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06003617 RID: 13847 RVA: 0x000F839A File Offset: 0x000F659A
		internal static string WRN_ReturnNotNullIfNotNull_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ReturnNotNullIfNotNull_Title", null);
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06003618 RID: 13848 RVA: 0x000F83A7 File Offset: 0x000F65A7
		internal static string WRN_MemberNotNull
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MemberNotNull", null);
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06003619 RID: 13849 RVA: 0x000F83B4 File Offset: 0x000F65B4
		internal static string WRN_MemberNotNull_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MemberNotNull_Title", null);
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x0600361A RID: 13850 RVA: 0x000F83C1 File Offset: 0x000F65C1
		internal static string WRN_MemberNotNullBadMember
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MemberNotNullBadMember", null);
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x0600361B RID: 13851 RVA: 0x000F83CE File Offset: 0x000F65CE
		internal static string WRN_MemberNotNullBadMember_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MemberNotNullBadMember_Title", null);
			}
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x0600361C RID: 13852 RVA: 0x000F83DB File Offset: 0x000F65DB
		internal static string WRN_MemberNotNullWhen
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MemberNotNullWhen", null);
			}
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x0600361D RID: 13853 RVA: 0x000F83E8 File Offset: 0x000F65E8
		internal static string WRN_MemberNotNullWhen_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MemberNotNullWhen_Title", null);
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x0600361E RID: 13854 RVA: 0x000F83F5 File Offset: 0x000F65F5
		internal static string WRN_ShouldNotReturn
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ShouldNotReturn", null);
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x0600361F RID: 13855 RVA: 0x000F8402 File Offset: 0x000F6602
		internal static string WRN_ShouldNotReturn_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ShouldNotReturn_Title", null);
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06003620 RID: 13856 RVA: 0x000F840F File Offset: 0x000F660F
		internal static string WRN_DoesNotReturnMismatch
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DoesNotReturnMismatch", null);
			}
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06003621 RID: 13857 RVA: 0x000F841C File Offset: 0x000F661C
		internal static string WRN_DoesNotReturnMismatch_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DoesNotReturnMismatch_Title", null);
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06003622 RID: 13858 RVA: 0x000F8429 File Offset: 0x000F6629
		internal static string WRN_NullabilityMismatchInReturnTypeOfTargetDelegate
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInReturnTypeOfTargetDelegate", null);
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06003623 RID: 13859 RVA: 0x000F8436 File Offset: 0x000F6636
		internal static string WRN_NullabilityMismatchInReturnTypeOfTargetDelegate_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInReturnTypeOfTargetDelegate_Title", null);
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06003624 RID: 13860 RVA: 0x000F8443 File Offset: 0x000F6643
		internal static string WRN_NullabilityMismatchInParameterTypeOfTargetDelegate
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInParameterTypeOfTargetDelegate", null);
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06003625 RID: 13861 RVA: 0x000F8450 File Offset: 0x000F6650
		internal static string WRN_NullabilityMismatchInParameterTypeOfTargetDelegate_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInParameterTypeOfTargetDelegate_Title", null);
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x000F845D File Offset: 0x000F665D
		internal static string WRN_NullAsNonNullable
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullAsNonNullable", null);
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06003627 RID: 13863 RVA: 0x000F846A File Offset: 0x000F666A
		internal static string WRN_NullAsNonNullable_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullAsNonNullable_Title", null);
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06003628 RID: 13864 RVA: 0x000F8477 File Offset: 0x000F6677
		internal static string ERR_AnnotationDisallowedInObjectCreation
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AnnotationDisallowedInObjectCreation", null);
			}
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06003629 RID: 13865 RVA: 0x000F8484 File Offset: 0x000F6684
		internal static string WRN_NullableValueTypeMayBeNull
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullableValueTypeMayBeNull", null);
			}
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x0600362A RID: 13866 RVA: 0x000F8491 File Offset: 0x000F6691
		internal static string WRN_NullableValueTypeMayBeNull_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullableValueTypeMayBeNull_Title", null);
			}
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x0600362B RID: 13867 RVA: 0x000F849E File Offset: 0x000F669E
		internal static string WRN_NullabilityMismatchInTypeParameterConstraint
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInTypeParameterConstraint", null);
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x0600362C RID: 13868 RVA: 0x000F84AB File Offset: 0x000F66AB
		internal static string WRN_NullabilityMismatchInTypeParameterConstraint_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInTypeParameterConstraint_Title", null);
			}
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x0600362D RID: 13869 RVA: 0x000F84B8 File Offset: 0x000F66B8
		internal static string WRN_MissingNonNullTypesContextForAnnotation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MissingNonNullTypesContextForAnnotation", null);
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x0600362E RID: 13870 RVA: 0x000F84C5 File Offset: 0x000F66C5
		internal static string WRN_MissingNonNullTypesContextForAnnotation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MissingNonNullTypesContextForAnnotation_Title", null);
			}
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x0600362F RID: 13871 RVA: 0x000F84D2 File Offset: 0x000F66D2
		internal static string ERR_ExplicitNullableAttribute
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExplicitNullableAttribute", null);
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06003630 RID: 13872 RVA: 0x000F84DF File Offset: 0x000F66DF
		internal static string ERR_NullableUnconstrainedTypeParameter
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NullableUnconstrainedTypeParameter", null);
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06003631 RID: 13873 RVA: 0x000F84EC File Offset: 0x000F66EC
		internal static string ERR_NullableOptionNotAvailable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NullableOptionNotAvailable", null);
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06003632 RID: 13874 RVA: 0x000F84F9 File Offset: 0x000F66F9
		internal static string ERR_NonTaskMainCantBeAsync
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NonTaskMainCantBeAsync", null);
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06003633 RID: 13875 RVA: 0x000F8506 File Offset: 0x000F6706
		internal static string ERR_PatternWrongGenericTypeInVersion
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PatternWrongGenericTypeInVersion", null);
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06003634 RID: 13876 RVA: 0x000F8513 File Offset: 0x000F6713
		internal static string WRN_UnreferencedLocalFunction
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreferencedLocalFunction", null);
			}
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x06003635 RID: 13877 RVA: 0x000F8520 File Offset: 0x000F6720
		internal static string WRN_UnreferencedLocalFunction_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreferencedLocalFunction_Title", null);
			}
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06003636 RID: 13878 RVA: 0x000F852D File Offset: 0x000F672D
		internal static string ERR_LocalFunctionMissingBody
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LocalFunctionMissingBody", null);
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x000F853A File Offset: 0x000F673A
		internal static string ERR_InvalidDebugInfo
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidDebugInfo", null);
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06003638 RID: 13880 RVA: 0x000F8547 File Offset: 0x000F6747
		internal static string IConversionExpressionIsNotCSharpConversion
		{
			get
			{
				return CSharpResources.GetResourceString("IConversionExpressionIsNotCSharpConversion", null);
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06003639 RID: 13881 RVA: 0x000F8554 File Offset: 0x000F6754
		internal static string ERR_DynamicLocalFunctionTypeParameter
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DynamicLocalFunctionTypeParameter", null);
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x0600363A RID: 13882 RVA: 0x000F8561 File Offset: 0x000F6761
		internal static string IDS_FeatureLeadingDigitSeparator
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureLeadingDigitSeparator", null);
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x0600363B RID: 13883 RVA: 0x000F856E File Offset: 0x000F676E
		internal static string ERR_ExplicitReservedAttr
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExplicitReservedAttr", null);
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x0600363C RID: 13884 RVA: 0x000F857B File Offset: 0x000F677B
		internal static string ERR_TypeReserved
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TypeReserved", null);
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x0600363D RID: 13885 RVA: 0x000F8588 File Offset: 0x000F6788
		internal static string ERR_InExtensionMustBeValueType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InExtensionMustBeValueType", null);
			}
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x0600363E RID: 13886 RVA: 0x000F8595 File Offset: 0x000F6795
		internal static string ERR_FieldsInRoStruct
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FieldsInRoStruct", null);
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x0600363F RID: 13887 RVA: 0x000F85A2 File Offset: 0x000F67A2
		internal static string ERR_AutoPropsInRoStruct
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AutoPropsInRoStruct", null);
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06003640 RID: 13888 RVA: 0x000F85AF File Offset: 0x000F67AF
		internal static string ERR_FieldlikeEventsInRoStruct
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FieldlikeEventsInRoStruct", null);
			}
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06003641 RID: 13889 RVA: 0x000F85BC File Offset: 0x000F67BC
		internal static string IDS_FeatureRefExtensionMethods
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureRefExtensionMethods", null);
			}
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06003642 RID: 13890 RVA: 0x000F85C9 File Offset: 0x000F67C9
		internal static string ERR_StackAllocConversionNotPossible
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StackAllocConversionNotPossible", null);
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06003643 RID: 13891 RVA: 0x000F85D6 File Offset: 0x000F67D6
		internal static string ERR_RefExtensionMustBeValueTypeOrConstrainedToOne
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefExtensionMustBeValueTypeOrConstrainedToOne", null);
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06003644 RID: 13892 RVA: 0x000F85E3 File Offset: 0x000F67E3
		internal static string ERR_OutAttrOnInParam
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OutAttrOnInParam", null);
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06003645 RID: 13893 RVA: 0x000F85F0 File Offset: 0x000F67F0
		internal static string ICompoundAssignmentOperationIsNotCSharpCompoundAssignment
		{
			get
			{
				return CSharpResources.GetResourceString("ICompoundAssignmentOperationIsNotCSharpCompoundAssignment", null);
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06003646 RID: 13894 RVA: 0x000F85FD File Offset: 0x000F67FD
		internal static string WRN_FilterIsConstantFalse
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_FilterIsConstantFalse", null);
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06003647 RID: 13895 RVA: 0x000F860A File Offset: 0x000F680A
		internal static string WRN_FilterIsConstantFalse_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_FilterIsConstantFalse_Title", null);
			}
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06003648 RID: 13896 RVA: 0x000F8617 File Offset: 0x000F6817
		internal static string WRN_FilterIsConstantFalseRedundantTryCatch
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_FilterIsConstantFalseRedundantTryCatch", null);
			}
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06003649 RID: 13897 RVA: 0x000F8624 File Offset: 0x000F6824
		internal static string WRN_FilterIsConstantFalseRedundantTryCatch_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_FilterIsConstantFalseRedundantTryCatch_Title", null);
			}
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x0600364A RID: 13898 RVA: 0x000F8631 File Offset: 0x000F6831
		internal static string ERR_ConditionalInInterpolation
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConditionalInInterpolation", null);
			}
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x0600364B RID: 13899 RVA: 0x000F863E File Offset: 0x000F683E
		internal static string ERR_InDynamicMethodArg
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InDynamicMethodArg", null);
			}
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x0600364C RID: 13900 RVA: 0x000F864B File Offset: 0x000F684B
		internal static string ERR_TupleSizesMismatchForBinOps
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TupleSizesMismatchForBinOps", null);
			}
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x0600364D RID: 13901 RVA: 0x000F8658 File Offset: 0x000F6858
		internal static string ERR_RefLocalOrParamExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefLocalOrParamExpected", null);
			}
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x0600364E RID: 13902 RVA: 0x000F8665 File Offset: 0x000F6865
		internal static string ERR_RefAssignNarrower
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RefAssignNarrower", null);
			}
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x0600364F RID: 13903 RVA: 0x000F8672 File Offset: 0x000F6872
		internal static string IDS_FeatureEnumGenericTypeConstraint
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureEnumGenericTypeConstraint", null);
			}
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06003650 RID: 13904 RVA: 0x000F867F File Offset: 0x000F687F
		internal static string IDS_FeatureDelegateGenericTypeConstraint
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureDelegateGenericTypeConstraint", null);
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06003651 RID: 13905 RVA: 0x000F868C File Offset: 0x000F688C
		internal static string IDS_FeatureUnmanagedGenericTypeConstraint
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureUnmanagedGenericTypeConstraint", null);
			}
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06003652 RID: 13906 RVA: 0x000F8699 File Offset: 0x000F6899
		internal static string ERR_NewBoundWithUnmanaged
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NewBoundWithUnmanaged", null);
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06003653 RID: 13907 RVA: 0x000F86A6 File Offset: 0x000F68A6
		internal static string ERR_UnmanagedConstraintNotSatisfied
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnmanagedConstraintNotSatisfied", null);
			}
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06003654 RID: 13908 RVA: 0x000F86B3 File Offset: 0x000F68B3
		internal static string ERR_ConWithUnmanagedCon
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ConWithUnmanagedCon", null);
			}
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06003655 RID: 13909 RVA: 0x000F86C0 File Offset: 0x000F68C0
		internal static string IDS_FeatureStackAllocInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureStackAllocInitializer", null);
			}
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06003656 RID: 13910 RVA: 0x000F86CD File Offset: 0x000F68CD
		internal static string ERR_InvalidStackAllocArray
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidStackAllocArray", null);
			}
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06003657 RID: 13911 RVA: 0x000F86DA File Offset: 0x000F68DA
		internal static string IDS_FeatureExpressionVariablesInQueriesAndInitializers
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureExpressionVariablesInQueriesAndInitializers", null);
			}
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06003658 RID: 13912 RVA: 0x000F86E7 File Offset: 0x000F68E7
		internal static string ERR_MissingPattern
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MissingPattern", null);
			}
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06003659 RID: 13913 RVA: 0x000F86F4 File Offset: 0x000F68F4
		internal static string IDS_FeatureRecursivePatterns
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureRecursivePatterns", null);
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x0600365A RID: 13914 RVA: 0x000F8701 File Offset: 0x000F6901
		internal static string IDS_FeatureNullPointerConstantPattern
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureNullPointerConstantPattern", null);
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x0600365B RID: 13915 RVA: 0x000F870E File Offset: 0x000F690E
		internal static string IDS_FeatureDefaultTypeParameterConstraint
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureDefaultTypeParameterConstraint", null);
			}
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x0600365C RID: 13916 RVA: 0x000F871B File Offset: 0x000F691B
		internal static string ERR_WrongNumberOfSubpatterns
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_WrongNumberOfSubpatterns", null);
			}
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x0600365D RID: 13917 RVA: 0x000F8728 File Offset: 0x000F6928
		internal static string ERR_PropertyPatternNameMissing
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PropertyPatternNameMissing", null);
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x0600365E RID: 13918 RVA: 0x000F8735 File Offset: 0x000F6935
		internal static string ERR_DefaultPattern
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DefaultPattern", null);
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x0600365F RID: 13919 RVA: 0x000F8742 File Offset: 0x000F6942
		internal static string ERR_SwitchExpressionNoBestType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SwitchExpressionNoBestType", null);
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06003660 RID: 13920 RVA: 0x000F874F File Offset: 0x000F694F
		internal static string ERR_DefaultLiteralNoTargetType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DefaultLiteralNoTargetType", null);
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06003661 RID: 13921 RVA: 0x000F875C File Offset: 0x000F695C
		internal static string ERR_CannotInferDelegateType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CannotInferDelegateType", null);
			}
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06003662 RID: 13922 RVA: 0x000F8769 File Offset: 0x000F6969
		internal static string ERR_SingleElementPositionalPatternRequiresDisambiguation
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SingleElementPositionalPatternRequiresDisambiguation", null);
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06003663 RID: 13923 RVA: 0x000F8776 File Offset: 0x000F6976
		internal static string ERR_VarMayNotBindToType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_VarMayNotBindToType", null);
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06003664 RID: 13924 RVA: 0x000F8783 File Offset: 0x000F6983
		internal static string WRN_SwitchExpressionNotExhaustive
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SwitchExpressionNotExhaustive", null);
			}
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06003665 RID: 13925 RVA: 0x000F8790 File Offset: 0x000F6990
		internal static string WRN_SwitchExpressionNotExhaustive_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SwitchExpressionNotExhaustive_Title", null);
			}
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06003666 RID: 13926 RVA: 0x000F879D File Offset: 0x000F699D
		internal static string WRN_SwitchExpressionNotExhaustiveWithWhen
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SwitchExpressionNotExhaustiveWithWhen", null);
			}
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06003667 RID: 13927 RVA: 0x000F87AA File Offset: 0x000F69AA
		internal static string WRN_SwitchExpressionNotExhaustiveWithWhen_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SwitchExpressionNotExhaustiveWithWhen_Title", null);
			}
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06003668 RID: 13928 RVA: 0x000F87B7 File Offset: 0x000F69B7
		internal static string WRN_SwitchExpressionNotExhaustiveWithUnnamedEnumValue
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SwitchExpressionNotExhaustiveWithUnnamedEnumValue", null);
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06003669 RID: 13929 RVA: 0x000F87C4 File Offset: 0x000F69C4
		internal static string WRN_SwitchExpressionNotExhaustiveWithUnnamedEnumValue_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SwitchExpressionNotExhaustiveWithUnnamedEnumValue_Title", null);
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x0600366A RID: 13930 RVA: 0x000F87D1 File Offset: 0x000F69D1
		internal static string WRN_CaseConstantNamedUnderscore
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CaseConstantNamedUnderscore", null);
			}
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x0600366B RID: 13931 RVA: 0x000F87DE File Offset: 0x000F69DE
		internal static string WRN_CaseConstantNamedUnderscore_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_CaseConstantNamedUnderscore_Title", null);
			}
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x0600366C RID: 13932 RVA: 0x000F87EB File Offset: 0x000F69EB
		internal static string WRN_IsTypeNamedUnderscore
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IsTypeNamedUnderscore", null);
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x0600366D RID: 13933 RVA: 0x000F87F8 File Offset: 0x000F69F8
		internal static string WRN_IsTypeNamedUnderscore_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IsTypeNamedUnderscore_Title", null);
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x0600366E RID: 13934 RVA: 0x000F8805 File Offset: 0x000F6A05
		internal static string ERR_ExpressionTreeContainsSwitchExpression
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsSwitchExpression", null);
			}
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x0600366F RID: 13935 RVA: 0x000F8812 File Offset: 0x000F6A12
		internal static string ERR_InvalidObjectCreation
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidObjectCreation", null);
			}
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06003670 RID: 13936 RVA: 0x000F881F File Offset: 0x000F6A1F
		internal static string IDS_FeatureIndexingMovableFixedBuffers
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureIndexingMovableFixedBuffers", null);
			}
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06003671 RID: 13937 RVA: 0x000F882C File Offset: 0x000F6A2C
		internal static string ERR_CantUseInOrOutInArglist
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CantUseInOrOutInArglist", null);
			}
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06003672 RID: 13938 RVA: 0x000F8839 File Offset: 0x000F6A39
		internal static string SyntaxTreeNotFound
		{
			get
			{
				return CSharpResources.GetResourceString("SyntaxTreeNotFound", null);
			}
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06003673 RID: 13939 RVA: 0x000F8846 File Offset: 0x000F6A46
		internal static string ERR_OutVariableCannotBeByRef
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OutVariableCannotBeByRef", null);
			}
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06003674 RID: 13940 RVA: 0x000F8853 File Offset: 0x000F6A53
		internal static string ERR_MultipleAnalyzerConfigsInSameDir
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MultipleAnalyzerConfigsInSameDir", null);
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06003675 RID: 13941 RVA: 0x000F8860 File Offset: 0x000F6A60
		internal static string IDS_FeatureCoalesceAssignmentExpression
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureCoalesceAssignmentExpression", null);
			}
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06003676 RID: 13942 RVA: 0x000F886D File Offset: 0x000F6A6D
		internal static string CannotCreateConstructedFromConstructed
		{
			get
			{
				return CSharpResources.GetResourceString("CannotCreateConstructedFromConstructed", null);
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06003677 RID: 13943 RVA: 0x000F887A File Offset: 0x000F6A7A
		internal static string CannotCreateConstructedFromNongeneric
		{
			get
			{
				return CSharpResources.GetResourceString("CannotCreateConstructedFromNongeneric", null);
			}
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06003678 RID: 13944 RVA: 0x000F8887 File Offset: 0x000F6A87
		internal static string IDS_FeatureUnconstrainedTypeParameterInNullCoalescingOperator
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureUnconstrainedTypeParameterInNullCoalescingOperator", null);
			}
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06003679 RID: 13945 RVA: 0x000F8894 File Offset: 0x000F6A94
		internal static string WRN_NullabilityMismatchInConstraintsOnImplicitImplementation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInConstraintsOnImplicitImplementation", null);
			}
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x0600367A RID: 13946 RVA: 0x000F88A1 File Offset: 0x000F6AA1
		internal static string WRN_NullabilityMismatchInConstraintsOnImplicitImplementation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInConstraintsOnImplicitImplementation_Title", null);
			}
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x0600367B RID: 13947 RVA: 0x000F88AE File Offset: 0x000F6AAE
		internal static string WRN_NullabilityMismatchInTypeParameterReferenceTypeConstraint
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInTypeParameterReferenceTypeConstraint", null);
			}
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x0600367C RID: 13948 RVA: 0x000F88BB File Offset: 0x000F6ABB
		internal static string WRN_NullabilityMismatchInTypeParameterReferenceTypeConstraint_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInTypeParameterReferenceTypeConstraint_Title", null);
			}
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x0600367D RID: 13949 RVA: 0x000F88C8 File Offset: 0x000F6AC8
		internal static string ERR_TripleDotNotAllowed
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TripleDotNotAllowed", null);
			}
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x0600367E RID: 13950 RVA: 0x000F88D5 File Offset: 0x000F6AD5
		internal static string IDS_FeatureIndexOperator
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureIndexOperator", null);
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x000F88E2 File Offset: 0x000F6AE2
		internal static string IDS_FeatureRangeOperator
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureRangeOperator", null);
			}
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06003680 RID: 13952 RVA: 0x000F88EF File Offset: 0x000F6AEF
		internal static string IDS_FeatureStaticLocalFunctions
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureStaticLocalFunctions", null);
			}
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06003681 RID: 13953 RVA: 0x000F88FC File Offset: 0x000F6AFC
		internal static string IDS_FeatureNameShadowingInNestedFunctions
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureNameShadowingInNestedFunctions", null);
			}
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06003682 RID: 13954 RVA: 0x000F8909 File Offset: 0x000F6B09
		internal static string IDS_FeatureLambdaDiscardParameters
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureLambdaDiscardParameters", null);
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06003683 RID: 13955 RVA: 0x000F8916 File Offset: 0x000F6B16
		internal static string IDS_FeatureMemberNotNull
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureMemberNotNull", null);
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06003684 RID: 13956 RVA: 0x000F8923 File Offset: 0x000F6B23
		internal static string IDS_FeatureNativeInt
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureNativeInt", null);
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06003685 RID: 13957 RVA: 0x000F8930 File Offset: 0x000F6B30
		internal static string ERR_BadDynamicAwaitForEach
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadDynamicAwaitForEach", null);
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06003686 RID: 13958 RVA: 0x000F893D File Offset: 0x000F6B3D
		internal static string ERR_NullableDirectiveQualifierExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NullableDirectiveQualifierExpected", null);
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x06003687 RID: 13959 RVA: 0x000F894A File Offset: 0x000F6B4A
		internal static string ERR_NullableDirectiveTargetExpected
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NullableDirectiveTargetExpected", null);
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06003688 RID: 13960 RVA: 0x000F8957 File Offset: 0x000F6B57
		internal static string WRN_MissingNonNullTypesContextForAnnotationInGeneratedCode
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MissingNonNullTypesContextForAnnotationInGeneratedCode", null);
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06003689 RID: 13961 RVA: 0x000F8964 File Offset: 0x000F6B64
		internal static string WRN_MissingNonNullTypesContextForAnnotationInGeneratedCode_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_MissingNonNullTypesContextForAnnotationInGeneratedCode_Title", null);
			}
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x0600368A RID: 13962 RVA: 0x000F8971 File Offset: 0x000F6B71
		internal static string WRN_NullReferenceInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullReferenceInitializer", null);
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x0600368B RID: 13963 RVA: 0x000F897E File Offset: 0x000F6B7E
		internal static string WRN_NullReferenceInitializer_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullReferenceInitializer_Title", null);
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x0600368C RID: 13964 RVA: 0x000F898B File Offset: 0x000F6B8B
		internal static string ERR_ExpressionTreeCantContainRefStruct
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeCantContainRefStruct", null);
			}
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x0600368D RID: 13965 RVA: 0x000F8998 File Offset: 0x000F6B98
		internal static string ERR_ElseCannotStartStatement
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ElseCannotStartStatement", null);
			}
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x0600368E RID: 13966 RVA: 0x000F89A5 File Offset: 0x000F6BA5
		internal static string ERR_ExpressionTreeCantContainNullCoalescingAssignment
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeCantContainNullCoalescingAssignment", null);
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x0600368F RID: 13967 RVA: 0x000F89B2 File Offset: 0x000F6BB2
		internal static string ERR_BadNullableContextOption
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadNullableContextOption", null);
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06003690 RID: 13968 RVA: 0x000F89BF File Offset: 0x000F6BBF
		internal static string ERR_SwitchGoverningExpressionRequiresParens
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SwitchGoverningExpressionRequiresParens", null);
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06003691 RID: 13969 RVA: 0x000F89CC File Offset: 0x000F6BCC
		internal static string ERR_TupleElementNameMismatch
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TupleElementNameMismatch", null);
			}
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06003692 RID: 13970 RVA: 0x000F89D9 File Offset: 0x000F6BD9
		internal static string ERR_DeconstructParameterNameMismatch
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DeconstructParameterNameMismatch", null);
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06003693 RID: 13971 RVA: 0x000F89E6 File Offset: 0x000F6BE6
		internal static string ERR_IsPatternImpossible
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_IsPatternImpossible", null);
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06003694 RID: 13972 RVA: 0x000F89F3 File Offset: 0x000F6BF3
		internal static string WRN_IsPatternAlways
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IsPatternAlways", null);
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06003695 RID: 13973 RVA: 0x000F8A00 File Offset: 0x000F6C00
		internal static string WRN_IsPatternAlways_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_IsPatternAlways_Title", null);
			}
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06003696 RID: 13974 RVA: 0x000F8A0D File Offset: 0x000F6C0D
		internal static string WRN_GivenExpressionNeverMatchesPattern
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_GivenExpressionNeverMatchesPattern", null);
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06003697 RID: 13975 RVA: 0x000F8A1A File Offset: 0x000F6C1A
		internal static string WRN_GivenExpressionNeverMatchesPattern_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_GivenExpressionNeverMatchesPattern_Title", null);
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06003698 RID: 13976 RVA: 0x000F8A27 File Offset: 0x000F6C27
		internal static string WRN_GivenExpressionAlwaysMatchesConstant
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_GivenExpressionAlwaysMatchesConstant", null);
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06003699 RID: 13977 RVA: 0x000F8A34 File Offset: 0x000F6C34
		internal static string WRN_GivenExpressionAlwaysMatchesConstant_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_GivenExpressionAlwaysMatchesConstant_Title", null);
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x0600369A RID: 13978 RVA: 0x000F8A41 File Offset: 0x000F6C41
		internal static string WRN_GivenExpressionAlwaysMatchesPattern
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_GivenExpressionAlwaysMatchesPattern", null);
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x0600369B RID: 13979 RVA: 0x000F8A4E File Offset: 0x000F6C4E
		internal static string WRN_GivenExpressionAlwaysMatchesPattern_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_GivenExpressionAlwaysMatchesPattern_Title", null);
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x0600369C RID: 13980 RVA: 0x000F8A5B File Offset: 0x000F6C5B
		internal static string ERR_FeatureNotAvailableInVersion8_0
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FeatureNotAvailableInVersion8_0", null);
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x0600369D RID: 13981 RVA: 0x000F8A68 File Offset: 0x000F6C68
		internal static string ERR_PointerTypeInPatternMatching
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PointerTypeInPatternMatching", null);
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x0600369E RID: 13982 RVA: 0x000F8A75 File Offset: 0x000F6C75
		internal static string ERR_ArgumentNameInITuplePattern
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ArgumentNameInITuplePattern", null);
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x0600369F RID: 13983 RVA: 0x000F8A82 File Offset: 0x000F6C82
		internal static string ERR_DiscardPatternInSwitchStatement
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DiscardPatternInSwitchStatement", null);
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x060036A0 RID: 13984 RVA: 0x000F8A8F File Offset: 0x000F6C8F
		internal static string WRN_NullabilityMismatchInExplicitlyImplementedInterface
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInExplicitlyImplementedInterface", null);
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x060036A1 RID: 13985 RVA: 0x000F8A9C File Offset: 0x000F6C9C
		internal static string WRN_NullabilityMismatchInExplicitlyImplementedInterface_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInExplicitlyImplementedInterface_Title", null);
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x060036A2 RID: 13986 RVA: 0x000F8AA9 File Offset: 0x000F6CA9
		internal static string WRN_NullabilityMismatchInInterfaceImplementedByBase
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInInterfaceImplementedByBase", null);
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x060036A3 RID: 13987 RVA: 0x000F8AB6 File Offset: 0x000F6CB6
		internal static string WRN_NullabilityMismatchInInterfaceImplementedByBase_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInInterfaceImplementedByBase_Title", null);
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x060036A4 RID: 13988 RVA: 0x000F8AC3 File Offset: 0x000F6CC3
		internal static string WRN_DuplicateInterfaceWithNullabilityMismatchInBaseList
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DuplicateInterfaceWithNullabilityMismatchInBaseList", null);
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x060036A5 RID: 13989 RVA: 0x000F8AD0 File Offset: 0x000F6CD0
		internal static string WRN_DuplicateInterfaceWithNullabilityMismatchInBaseList_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DuplicateInterfaceWithNullabilityMismatchInBaseList_Title", null);
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x060036A6 RID: 13990 RVA: 0x000F8ADD File Offset: 0x000F6CDD
		internal static string ERR_DuplicateExplicitImpl
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateExplicitImpl", null);
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x060036A7 RID: 13991 RVA: 0x000F8AEA File Offset: 0x000F6CEA
		internal static string ERR_UsingVarInSwitchCase
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UsingVarInSwitchCase", null);
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x060036A8 RID: 13992 RVA: 0x000F8AF7 File Offset: 0x000F6CF7
		internal static string ERR_GoToForwardJumpOverUsingVar
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GoToForwardJumpOverUsingVar", null);
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x060036A9 RID: 13993 RVA: 0x000F8B04 File Offset: 0x000F6D04
		internal static string ERR_GoToBackwardJumpOverUsingVar
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GoToBackwardJumpOverUsingVar", null);
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x060036AA RID: 13994 RVA: 0x000F8B11 File Offset: 0x000F6D11
		internal static string IDS_FeatureUsingDeclarations
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureUsingDeclarations", null);
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x060036AB RID: 13995 RVA: 0x000F8B1E File Offset: 0x000F6D1E
		internal static string ERR_FeatureInPreview
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FeatureInPreview", null);
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x060036AC RID: 13996 RVA: 0x000F8B2B File Offset: 0x000F6D2B
		internal static string IDS_DefaultInterfaceImplementation
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_DefaultInterfaceImplementation", null);
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x060036AD RID: 13997 RVA: 0x000F8B38 File Offset: 0x000F6D38
		internal static string ERR_RuntimeDoesNotSupportDefaultInterfaceImplementation
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RuntimeDoesNotSupportDefaultInterfaceImplementation", null);
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x060036AE RID: 13998 RVA: 0x000F8B45 File Offset: 0x000F6D45
		internal static string ERR_RuntimeDoesNotSupportDefaultInterfaceImplementationForMember
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RuntimeDoesNotSupportDefaultInterfaceImplementationForMember", null);
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x060036AF RID: 13999 RVA: 0x000F8B52 File Offset: 0x000F6D52
		internal static string ERR_InvalidModifierForLanguageVersion
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidModifierForLanguageVersion", null);
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x060036B0 RID: 14000 RVA: 0x000F8B5F File Offset: 0x000F6D5F
		internal static string ERR_ImplicitImplementationOfNonPublicInterfaceMember
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImplicitImplementationOfNonPublicInterfaceMember", null);
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x060036B1 RID: 14001 RVA: 0x000F8B6C File Offset: 0x000F6D6C
		internal static string ERR_MostSpecificImplementationIsNotFound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MostSpecificImplementationIsNotFound", null);
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x060036B2 RID: 14002 RVA: 0x000F8B79 File Offset: 0x000F6D79
		internal static string ERR_LanguageVersionDoesNotSupportDefaultInterfaceImplementationForMember
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_LanguageVersionDoesNotSupportDefaultInterfaceImplementationForMember", null);
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x060036B3 RID: 14003 RVA: 0x000F8B86 File Offset: 0x000F6D86
		internal static string ERR_RuntimeDoesNotSupportProtectedAccessForInterfaceMember
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RuntimeDoesNotSupportProtectedAccessForInterfaceMember", null);
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x060036B4 RID: 14004 RVA: 0x000F8B93 File Offset: 0x000F6D93
		internal static string ERR_DefaultInterfaceImplementationInNoPIAType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DefaultInterfaceImplementationInNoPIAType", null);
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x060036B5 RID: 14005 RVA: 0x000F8BA0 File Offset: 0x000F6DA0
		internal static string WRN_SwitchExpressionNotExhaustiveForNull
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SwitchExpressionNotExhaustiveForNull", null);
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x060036B6 RID: 14006 RVA: 0x000F8BAD File Offset: 0x000F6DAD
		internal static string WRN_SwitchExpressionNotExhaustiveForNull_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SwitchExpressionNotExhaustiveForNull_Title", null);
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x060036B7 RID: 14007 RVA: 0x000F8BBA File Offset: 0x000F6DBA
		internal static string WRN_SwitchExpressionNotExhaustiveForNullWithWhen
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SwitchExpressionNotExhaustiveForNullWithWhen", null);
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x060036B8 RID: 14008 RVA: 0x000F8BC7 File Offset: 0x000F6DC7
		internal static string WRN_SwitchExpressionNotExhaustiveForNullWithWhen_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SwitchExpressionNotExhaustiveForNullWithWhen_Title", null);
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x060036B9 RID: 14009 RVA: 0x000F8BD4 File Offset: 0x000F6DD4
		internal static string ERR_AttributeNotOnEventAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AttributeNotOnEventAccessor", null);
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x060036BA RID: 14010 RVA: 0x000F8BE1 File Offset: 0x000F6DE1
		internal static string IDS_FeatureObsoleteOnPropertyAccessor
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureObsoleteOnPropertyAccessor", null);
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x060036BB RID: 14011 RVA: 0x000F8BEE File Offset: 0x000F6DEE
		internal static string WRN_UnconsumedEnumeratorCancellationAttributeUsage
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnconsumedEnumeratorCancellationAttributeUsage", null);
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x060036BC RID: 14012 RVA: 0x000F8BFB File Offset: 0x000F6DFB
		internal static string WRN_UnconsumedEnumeratorCancellationAttributeUsage_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnconsumedEnumeratorCancellationAttributeUsage_Title", null);
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x060036BD RID: 14013 RVA: 0x000F8C08 File Offset: 0x000F6E08
		internal static string WRN_UndecoratedCancellationTokenParameter
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UndecoratedCancellationTokenParameter", null);
			}
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x060036BE RID: 14014 RVA: 0x000F8C15 File Offset: 0x000F6E15
		internal static string WRN_UndecoratedCancellationTokenParameter_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UndecoratedCancellationTokenParameter_Title", null);
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x060036BF RID: 14015 RVA: 0x000F8C22 File Offset: 0x000F6E22
		internal static string ERR_MultipleEnumeratorCancellationAttributes
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MultipleEnumeratorCancellationAttributes", null);
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x060036C0 RID: 14016 RVA: 0x000F8C2F File Offset: 0x000F6E2F
		internal static string ERR_OverrideRefConstraintNotSatisfied
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OverrideRefConstraintNotSatisfied", null);
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x060036C1 RID: 14017 RVA: 0x000F8C3C File Offset: 0x000F6E3C
		internal static string ERR_OverrideValConstraintNotSatisfied
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OverrideValConstraintNotSatisfied", null);
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x060036C2 RID: 14018 RVA: 0x000F8C49 File Offset: 0x000F6E49
		internal static string ERR_OverrideDefaultConstraintNotSatisfied
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_OverrideDefaultConstraintNotSatisfied", null);
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x060036C3 RID: 14019 RVA: 0x000F8C56 File Offset: 0x000F6E56
		internal static string ERR_DefaultConstraintOverrideOnly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DefaultConstraintOverrideOnly", null);
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x060036C4 RID: 14020 RVA: 0x000F8C63 File Offset: 0x000F6E63
		internal static string IDS_OverrideWithConstraints
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_OverrideWithConstraints", null);
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x060036C5 RID: 14021 RVA: 0x000F8C70 File Offset: 0x000F6E70
		internal static string WRN_NullabilityMismatchInConstraintsOnPartialImplementation
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInConstraintsOnPartialImplementation", null);
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x060036C6 RID: 14022 RVA: 0x000F8C7D File Offset: 0x000F6E7D
		internal static string WRN_NullabilityMismatchInConstraintsOnPartialImplementation_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInConstraintsOnPartialImplementation_Title", null);
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x060036C7 RID: 14023 RVA: 0x000F8C8A File Offset: 0x000F6E8A
		internal static string IDS_FeatureNestedStackalloc
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureNestedStackalloc", null);
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x060036C8 RID: 14024 RVA: 0x000F8C97 File Offset: 0x000F6E97
		internal static string WRN_NullabilityMismatchInTypeParameterNotNullConstraint
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInTypeParameterNotNullConstraint", null);
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x060036C9 RID: 14025 RVA: 0x000F8CA4 File Offset: 0x000F6EA4
		internal static string WRN_NullabilityMismatchInTypeParameterNotNullConstraint_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_NullabilityMismatchInTypeParameterNotNullConstraint_Title", null);
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x060036CA RID: 14026 RVA: 0x000F8CB1 File Offset: 0x000F6EB1
		internal static string IDS_FeatureNotNullGenericTypeConstraint
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureNotNullGenericTypeConstraint", null);
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x060036CB RID: 14027 RVA: 0x000F8CBE File Offset: 0x000F6EBE
		internal static string ERR_DuplicateNullSuppression
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DuplicateNullSuppression", null);
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x060036CC RID: 14028 RVA: 0x000F8CCB File Offset: 0x000F6ECB
		internal static string ERR_ReAbstractionInNoPIAType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ReAbstractionInNoPIAType", null);
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x060036CD RID: 14029 RVA: 0x000F8CD8 File Offset: 0x000F6ED8
		internal static string ERR_BadSwitchValue
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadSwitchValue", null);
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x060036CE RID: 14030 RVA: 0x000F8CE5 File Offset: 0x000F6EE5
		internal static string IDS_FeatureFunctionPointers
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureFunctionPointers", null);
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x060036CF RID: 14031 RVA: 0x000F8CF2 File Offset: 0x000F6EF2
		internal static string IDS_AddressOfMethodGroup
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_AddressOfMethodGroup", null);
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x060036D0 RID: 14032 RVA: 0x000F8CFF File Offset: 0x000F6EFF
		internal static string ERR_InvalidFunctionPointerCallingConvention
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidFunctionPointerCallingConvention", null);
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x060036D1 RID: 14033 RVA: 0x000F8D0C File Offset: 0x000F6F0C
		internal static string ERR_TypeNotFound
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TypeNotFound", null);
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x060036D2 RID: 14034 RVA: 0x000F8D19 File Offset: 0x000F6F19
		internal static string ERR_TypeMustBePublic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TypeMustBePublic", null);
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x060036D3 RID: 14035 RVA: 0x000F8D26 File Offset: 0x000F6F26
		internal static string WRN_SyncAndAsyncEntryPoints
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_SyncAndAsyncEntryPoints", null);
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x060036D4 RID: 14036 RVA: 0x000F8D33 File Offset: 0x000F6F33
		internal static string ERR_InternalError
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InternalError", null);
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x060036D5 RID: 14037 RVA: 0x000F8D40 File Offset: 0x000F6F40
		internal static string IDS_FeatureStaticAnonymousFunction
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureStaticAnonymousFunction", null);
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x060036D6 RID: 14038 RVA: 0x000F8D4D File Offset: 0x000F6F4D
		internal static string ERR_StaticAnonymousFunctionCannotCaptureThis
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StaticAnonymousFunctionCannotCaptureThis", null);
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x060036D7 RID: 14039 RVA: 0x000F8D5A File Offset: 0x000F6F5A
		internal static string ERR_StaticAnonymousFunctionCannotCaptureVariable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StaticAnonymousFunctionCannotCaptureVariable", null);
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x060036D8 RID: 14040 RVA: 0x000F8D67 File Offset: 0x000F6F67
		internal static string IDS_FeatureAsyncUsing
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureAsyncUsing", null);
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x060036D9 RID: 14041 RVA: 0x000F8D74 File Offset: 0x000F6F74
		internal static string IDS_FeatureParenthesizedPattern
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureParenthesizedPattern", null);
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x060036DA RID: 14042 RVA: 0x000F8D81 File Offset: 0x000F6F81
		internal static string IDS_FeatureOrPattern
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureOrPattern", null);
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x060036DB RID: 14043 RVA: 0x000F8D8E File Offset: 0x000F6F8E
		internal static string IDS_FeatureAndPattern
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureAndPattern", null);
			}
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x060036DC RID: 14044 RVA: 0x000F8D9B File Offset: 0x000F6F9B
		internal static string IDS_FeatureNotPattern
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureNotPattern", null);
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x060036DD RID: 14045 RVA: 0x000F8DA8 File Offset: 0x000F6FA8
		internal static string IDS_FeatureTypePattern
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureTypePattern", null);
			}
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x060036DE RID: 14046 RVA: 0x000F8DB5 File Offset: 0x000F6FB5
		internal static string IDS_FeatureRelationalPattern
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureRelationalPattern", null);
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x060036DF RID: 14047 RVA: 0x000F8DC2 File Offset: 0x000F6FC2
		internal static string ERR_VarianceInterfaceNesting
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_VarianceInterfaceNesting", null);
			}
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x060036E0 RID: 14048 RVA: 0x000F8DCF File Offset: 0x000F6FCF
		internal static string ERR_ExternEventInitializer
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExternEventInitializer", null);
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x060036E1 RID: 14049 RVA: 0x000F8DDC File Offset: 0x000F6FDC
		internal static string ERR_ImplicitIndexIndexerWithName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImplicitIndexIndexerWithName", null);
			}
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x060036E2 RID: 14050 RVA: 0x000F8DE9 File Offset: 0x000F6FE9
		internal static string ERR_ImplicitRangeIndexerWithName
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImplicitRangeIndexerWithName", null);
			}
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x060036E3 RID: 14051 RVA: 0x000F8DF6 File Offset: 0x000F6FF6
		internal static string ERR_ImplicitObjectCreationIllegalTargetType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImplicitObjectCreationIllegalTargetType", null);
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x060036E4 RID: 14052 RVA: 0x000F8E03 File Offset: 0x000F7003
		internal static string ERR_ImplicitObjectCreationNotValid
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImplicitObjectCreationNotValid", null);
			}
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x060036E5 RID: 14053 RVA: 0x000F8E10 File Offset: 0x000F7010
		internal static string ERR_ImplicitObjectCreationNoTargetType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ImplicitObjectCreationNoTargetType", null);
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x060036E6 RID: 14054 RVA: 0x000F8E1D File Offset: 0x000F701D
		internal static string IDS_FeatureImplicitObjectCreation
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureImplicitObjectCreation", null);
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x060036E7 RID: 14055 RVA: 0x000F8E2A File Offset: 0x000F702A
		internal static string ERR_ExpressionTreeContainsPatternIndexOrRangeIndexer
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsPatternIndexOrRangeIndexer", null);
			}
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x060036E8 RID: 14056 RVA: 0x000F8E37 File Offset: 0x000F7037
		internal static string ERR_ExpressionTreeContainsFromEndIndexExpression
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsFromEndIndexExpression", null);
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x060036E9 RID: 14057 RVA: 0x000F8E44 File Offset: 0x000F7044
		internal static string ERR_ExpressionTreeContainsRangeExpression
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ExpressionTreeContainsRangeExpression", null);
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x060036EA RID: 14058 RVA: 0x000F8E51 File Offset: 0x000F7051
		internal static string WRN_GeneratorFailedDuringGeneration
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_GeneratorFailedDuringGeneration", null);
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x060036EB RID: 14059 RVA: 0x000F8E5E File Offset: 0x000F705E
		internal static string WRN_GeneratorFailedDuringInitialization
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_GeneratorFailedDuringInitialization", null);
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x060036EC RID: 14060 RVA: 0x000F8E6B File Offset: 0x000F706B
		internal static string WRN_GeneratorFailedDuringGeneration_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_GeneratorFailedDuringGeneration_Title", null);
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x060036ED RID: 14061 RVA: 0x000F8E78 File Offset: 0x000F7078
		internal static string WRN_GeneratorFailedDuringInitialization_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_GeneratorFailedDuringInitialization_Title", null);
			}
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x060036EE RID: 14062 RVA: 0x000F8E85 File Offset: 0x000F7085
		internal static string WRN_GeneratorFailedDuringGeneration_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_GeneratorFailedDuringGeneration_Description", null);
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x060036EF RID: 14063 RVA: 0x000F8E92 File Offset: 0x000F7092
		internal static string WRN_GeneratorFailedDuringInitialization_Description
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_GeneratorFailedDuringInitialization_Description", null);
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x060036F0 RID: 14064 RVA: 0x000F8E9F File Offset: 0x000F709F
		internal static string IDS_FeatureRecords
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureRecords", null);
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x060036F1 RID: 14065 RVA: 0x000F8EAC File Offset: 0x000F70AC
		internal static string IDS_FeatureInitOnlySetters
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureInitOnlySetters", null);
			}
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x060036F2 RID: 14066 RVA: 0x000F8EB9 File Offset: 0x000F70B9
		internal static string ERR_InvalidWithReceiverType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidWithReceiverType", null);
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x060036F3 RID: 14067 RVA: 0x000F8EC6 File Offset: 0x000F70C6
		internal static string ERR_CannotClone
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CannotClone", null);
			}
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x060036F4 RID: 14068 RVA: 0x000F8ED3 File Offset: 0x000F70D3
		internal static string ERR_AssignmentInitOnly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AssignmentInitOnly", null);
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x060036F5 RID: 14069 RVA: 0x000F8EE0 File Offset: 0x000F70E0
		internal static string ERR_DesignatorBeneathPatternCombinator
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DesignatorBeneathPatternCombinator", null);
			}
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x060036F6 RID: 14070 RVA: 0x000F8EED File Offset: 0x000F70ED
		internal static string ERR_UnsupportedTypeForRelationalPattern
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnsupportedTypeForRelationalPattern", null);
			}
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x060036F7 RID: 14071 RVA: 0x000F8EFA File Offset: 0x000F70FA
		internal static string ERR_RelationalPatternWithNaN
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RelationalPatternWithNaN", null);
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x060036F8 RID: 14072 RVA: 0x000F8F07 File Offset: 0x000F7107
		internal static string IDS_FeatureExtendedPartialMethods
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureExtendedPartialMethods", null);
			}
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x060036F9 RID: 14073 RVA: 0x000F8F14 File Offset: 0x000F7114
		internal static string IDS_FeatureConstantInterpolatedStrings
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureConstantInterpolatedStrings", null);
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x060036FA RID: 14074 RVA: 0x000F8F21 File Offset: 0x000F7121
		internal static string ERR_PartialMethodWithNonVoidReturnMustHaveAccessMods
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodWithNonVoidReturnMustHaveAccessMods", null);
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x060036FB RID: 14075 RVA: 0x000F8F2E File Offset: 0x000F712E
		internal static string ERR_PartialMethodWithOutParamMustHaveAccessMods
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodWithOutParamMustHaveAccessMods", null);
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x060036FC RID: 14076 RVA: 0x000F8F3B File Offset: 0x000F713B
		internal static string ERR_PartialMethodWithAccessibilityModsMustHaveImplementation
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodWithAccessibilityModsMustHaveImplementation", null);
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x060036FD RID: 14077 RVA: 0x000F8F48 File Offset: 0x000F7148
		internal static string ERR_PartialMethodWithExtendedModMustHaveAccessMods
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodWithExtendedModMustHaveAccessMods", null);
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x060036FE RID: 14078 RVA: 0x000F8F55 File Offset: 0x000F7155
		internal static string ERR_PartialMethodAccessibilityDifference
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodAccessibilityDifference", null);
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x060036FF RID: 14079 RVA: 0x000F8F62 File Offset: 0x000F7162
		internal static string ERR_PartialMethodExtendedModDifference
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodExtendedModDifference", null);
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06003700 RID: 14080 RVA: 0x000F8F6F File Offset: 0x000F716F
		internal static string ERR_PartialMethodReturnTypeDifference
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodReturnTypeDifference", null);
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06003701 RID: 14081 RVA: 0x000F8F7C File Offset: 0x000F717C
		internal static string ERR_PartialMethodRefReturnDifference
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_PartialMethodRefReturnDifference", null);
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06003702 RID: 14082 RVA: 0x000F8F89 File Offset: 0x000F7189
		internal static string IDS_TopLevelStatements
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_TopLevelStatements", null);
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06003703 RID: 14083 RVA: 0x000F8F96 File Offset: 0x000F7196
		internal static string ERR_SimpleProgramLocalIsReferencedOutsideOfTopLevelStatement
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SimpleProgramLocalIsReferencedOutsideOfTopLevelStatement", null);
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06003704 RID: 14084 RVA: 0x000F8FA3 File Offset: 0x000F71A3
		internal static string ERR_SimpleProgramMultipleUnitsWithTopLevelStatements
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SimpleProgramMultipleUnitsWithTopLevelStatements", null);
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06003705 RID: 14085 RVA: 0x000F8FB0 File Offset: 0x000F71B0
		internal static string ERR_TopLevelStatementAfterNamespaceOrType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_TopLevelStatementAfterNamespaceOrType", null);
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06003706 RID: 14086 RVA: 0x000F8FBD File Offset: 0x000F71BD
		internal static string ERR_SimpleProgramDisallowsMainType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SimpleProgramDisallowsMainType", null);
			}
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06003707 RID: 14087 RVA: 0x000F8FCA File Offset: 0x000F71CA
		internal static string ERR_SimpleProgramNotAnExecutable
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SimpleProgramNotAnExecutable", null);
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06003708 RID: 14088 RVA: 0x000F8FD7 File Offset: 0x000F71D7
		internal static string ERR_InvalidFuncPointerReturnTypeModifier
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidFuncPointerReturnTypeModifier", null);
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06003709 RID: 14089 RVA: 0x000F8FE4 File Offset: 0x000F71E4
		internal static string ERR_DupReturnTypeMod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DupReturnTypeMod", null);
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x0600370A RID: 14090 RVA: 0x000F8FF1 File Offset: 0x000F71F1
		internal static string ERR_BadFuncPointerParamModifier
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadFuncPointerParamModifier", null);
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x0600370B RID: 14091 RVA: 0x000F8FFE File Offset: 0x000F71FE
		internal static string ERR_BadFuncPointerArgCount
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadFuncPointerArgCount", null);
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x0600370C RID: 14092 RVA: 0x000F900B File Offset: 0x000F720B
		internal static string ERR_MethFuncPtrMismatch
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MethFuncPtrMismatch", null);
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x0600370D RID: 14093 RVA: 0x000F9018 File Offset: 0x000F7218
		internal static string ERR_FuncPtrRefMismatch
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FuncPtrRefMismatch", null);
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x0600370E RID: 14094 RVA: 0x000F9025 File Offset: 0x000F7225
		internal static string ERR_FuncPtrMethMustBeStatic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FuncPtrMethMustBeStatic", null);
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x0600370F RID: 14095 RVA: 0x000F9032 File Offset: 0x000F7232
		internal static string ERR_AddressOfMethodGroupInExpressionTree
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AddressOfMethodGroupInExpressionTree", null);
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06003710 RID: 14096 RVA: 0x000F903F File Offset: 0x000F723F
		internal static string ERR_WrongFuncPtrCallingConvention
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_WrongFuncPtrCallingConvention", null);
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06003711 RID: 14097 RVA: 0x000F904C File Offset: 0x000F724C
		internal static string ERR_MissingAddressOf
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MissingAddressOf", null);
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06003712 RID: 14098 RVA: 0x000F9059 File Offset: 0x000F7259
		internal static string ERR_CannotUseReducedExtensionMethodInAddressOf
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CannotUseReducedExtensionMethodInAddressOf", null);
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06003713 RID: 14099 RVA: 0x000F9066 File Offset: 0x000F7266
		internal static string ERR_CannotUseFunctionPointerAsFixedLocal
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CannotUseFunctionPointerAsFixedLocal", null);
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06003714 RID: 14100 RVA: 0x000F9073 File Offset: 0x000F7273
		internal static string ERR_UnsupportedCallingConvention
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnsupportedCallingConvention", null);
			}
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06003715 RID: 14101 RVA: 0x000F9080 File Offset: 0x000F7280
		internal static string ERR_RuntimeDoesNotSupportUnmanagedDefaultCallConv
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RuntimeDoesNotSupportUnmanagedDefaultCallConv", null);
			}
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06003716 RID: 14102 RVA: 0x000F908D File Offset: 0x000F728D
		internal static string NotSameNumberParameterTypesAndRefKinds
		{
			get
			{
				return CSharpResources.GetResourceString("NotSameNumberParameterTypesAndRefKinds", null);
			}
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06003717 RID: 14103 RVA: 0x000F909A File Offset: 0x000F729A
		internal static string OutIsNotValidForReturn
		{
			get
			{
				return CSharpResources.GetResourceString("OutIsNotValidForReturn", null);
			}
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06003718 RID: 14104 RVA: 0x000F90A7 File Offset: 0x000F72A7
		internal static string CallingConventionTypesRequireUnmanaged
		{
			get
			{
				return CSharpResources.GetResourceString("CallingConventionTypesRequireUnmanaged", null);
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06003719 RID: 14105 RVA: 0x000F90B4 File Offset: 0x000F72B4
		internal static string CallingConventionTypeIsInvalid
		{
			get
			{
				return CSharpResources.GetResourceString("CallingConventionTypeIsInvalid", null);
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x0600371A RID: 14106 RVA: 0x000F90C1 File Offset: 0x000F72C1
		internal static string ERR_CannotConvertAddressOfToDelegate
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CannotConvertAddressOfToDelegate", null);
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x0600371B RID: 14107 RVA: 0x000F90CE File Offset: 0x000F72CE
		internal static string ERR_AddressOfToNonFunctionPointer
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_AddressOfToNonFunctionPointer", null);
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x0600371C RID: 14108 RVA: 0x000F90DB File Offset: 0x000F72DB
		internal static string ERR_CannotSpecifyManagedWithUnmanagedSpecifiers
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CannotSpecifyManagedWithUnmanagedSpecifiers", null);
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x0600371D RID: 14109 RVA: 0x000F90E8 File Offset: 0x000F72E8
		internal static string ERR_FeatureNotAvailableInVersion9
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FeatureNotAvailableInVersion9", null);
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x0600371E RID: 14110 RVA: 0x000F90F5 File Offset: 0x000F72F5
		internal static string ERR_UnexpectedArgumentList
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnexpectedArgumentList", null);
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x0600371F RID: 14111 RVA: 0x000F9102 File Offset: 0x000F7302
		internal static string ERR_UnexpectedOrMissingConstructorInitializerInRecord
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnexpectedOrMissingConstructorInitializerInRecord", null);
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06003720 RID: 14112 RVA: 0x000F910F File Offset: 0x000F730F
		internal static string ERR_MultipleRecordParameterLists
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_MultipleRecordParameterLists", null);
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06003721 RID: 14113 RVA: 0x000F911C File Offset: 0x000F731C
		internal static string ERR_BadRecordBase
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadRecordBase", null);
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06003722 RID: 14114 RVA: 0x000F9129 File Offset: 0x000F7329
		internal static string ERR_BadInheritanceFromRecord
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadInheritanceFromRecord", null);
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06003723 RID: 14115 RVA: 0x000F9136 File Offset: 0x000F7336
		internal static string ERR_BadRecordMemberForPositionalParameter
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadRecordMemberForPositionalParameter", null);
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06003724 RID: 14116 RVA: 0x000F9143 File Offset: 0x000F7343
		internal static string ERR_NoCopyConstructorInBaseType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NoCopyConstructorInBaseType", null);
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06003725 RID: 14117 RVA: 0x000F9150 File Offset: 0x000F7350
		internal static string ERR_CopyConstructorMustInvokeBaseCopyConstructor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CopyConstructorMustInvokeBaseCopyConstructor", null);
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06003726 RID: 14118 RVA: 0x000F915D File Offset: 0x000F735D
		internal static string IDS_FeatureTargetTypedConditional
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureTargetTypedConditional", null);
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06003727 RID: 14119 RVA: 0x000F916A File Offset: 0x000F736A
		internal static string ERR_DoesNotOverrideMethodFromObject
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DoesNotOverrideMethodFromObject", null);
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06003728 RID: 14120 RVA: 0x000F9177 File Offset: 0x000F7377
		internal static string IDS_FeatureCovariantReturnsForOverrides
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureCovariantReturnsForOverrides", null);
			}
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06003729 RID: 14121 RVA: 0x000F9184 File Offset: 0x000F7384
		internal static string ERR_RuntimeDoesNotSupportCovariantReturnsOfClasses
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RuntimeDoesNotSupportCovariantReturnsOfClasses", null);
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x0600372A RID: 14122 RVA: 0x000F9191 File Offset: 0x000F7391
		internal static string ERR_RuntimeDoesNotSupportCovariantPropertiesOfClasses
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RuntimeDoesNotSupportCovariantPropertiesOfClasses", null);
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x0600372B RID: 14123 RVA: 0x000F919E File Offset: 0x000F739E
		internal static string ERR_SealedAPIInRecord
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SealedAPIInRecord", null);
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x0600372C RID: 14124 RVA: 0x000F91AB File Offset: 0x000F73AB
		internal static string ERR_DoesNotOverrideBaseMethod
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DoesNotOverrideBaseMethod", null);
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x0600372D RID: 14125 RVA: 0x000F91B8 File Offset: 0x000F73B8
		internal static string WRN_ConstOutOfRangeChecked
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ConstOutOfRangeChecked", null);
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x0600372E RID: 14126 RVA: 0x000F91C5 File Offset: 0x000F73C5
		internal static string WRN_ConstOutOfRangeChecked_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_ConstOutOfRangeChecked_Title", null);
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x0600372F RID: 14127 RVA: 0x000F91D2 File Offset: 0x000F73D2
		internal static string ERR_CloneDisallowedInRecord
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CloneDisallowedInRecord", null);
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06003730 RID: 14128 RVA: 0x000F91DF File Offset: 0x000F73DF
		internal static string WRN_RecordNamedDisallowed
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_RecordNamedDisallowed", null);
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06003731 RID: 14129 RVA: 0x000F91EC File Offset: 0x000F73EC
		internal static string WRN_RecordNamedDisallowed_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_RecordNamedDisallowed_Title", null);
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06003732 RID: 14130 RVA: 0x000F91F9 File Offset: 0x000F73F9
		internal static string ERR_NotOverridableAPIInRecord
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NotOverridableAPIInRecord", null);
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06003733 RID: 14131 RVA: 0x000F9206 File Offset: 0x000F7406
		internal static string ERR_NonPublicAPIInRecord
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NonPublicAPIInRecord", null);
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06003734 RID: 14132 RVA: 0x000F9213 File Offset: 0x000F7413
		internal static string ERR_SignatureMismatchInRecord
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_SignatureMismatchInRecord", null);
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06003735 RID: 14133 RVA: 0x000F9220 File Offset: 0x000F7420
		internal static string ERR_NonProtectedAPIInRecord
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NonProtectedAPIInRecord", null);
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06003736 RID: 14134 RVA: 0x000F922D File Offset: 0x000F742D
		internal static string ERR_DoesNotOverrideBaseEqualityContract
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_DoesNotOverrideBaseEqualityContract", null);
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06003737 RID: 14135 RVA: 0x000F923A File Offset: 0x000F743A
		internal static string ERR_StaticAPIInRecord
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_StaticAPIInRecord", null);
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06003738 RID: 14136 RVA: 0x000F9247 File Offset: 0x000F7447
		internal static string ERR_CopyConstructorWrongAccessibility
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CopyConstructorWrongAccessibility", null);
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06003739 RID: 14137 RVA: 0x000F9254 File Offset: 0x000F7454
		internal static string ERR_NonPrivateAPIInRecord
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_NonPrivateAPIInRecord", null);
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x0600373A RID: 14138 RVA: 0x000F9261 File Offset: 0x000F7461
		internal static string WRN_PrecedenceInversion
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_PrecedenceInversion", null);
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x0600373B RID: 14139 RVA: 0x000F926E File Offset: 0x000F746E
		internal static string WRN_PrecedenceInversion_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_PrecedenceInversion_Title", null);
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x0600373C RID: 14140 RVA: 0x000F927B File Offset: 0x000F747B
		internal static string IDS_FeatureModuleInitializers
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureModuleInitializers", null);
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x0600373D RID: 14141 RVA: 0x000F9288 File Offset: 0x000F7488
		internal static string ERR_ModuleInitializerMethodMustBeAccessibleOutsideTopLevelType
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ModuleInitializerMethodMustBeAccessibleOutsideTopLevelType", null);
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x0600373E RID: 14142 RVA: 0x000F9295 File Offset: 0x000F7495
		internal static string ERR_ModuleInitializerMethodMustBeStaticParameterlessVoid
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ModuleInitializerMethodMustBeStaticParameterlessVoid", null);
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x0600373F RID: 14143 RVA: 0x000F92A2 File Offset: 0x000F74A2
		internal static string ERR_ModuleInitializerMethodAndContainingTypesMustNotBeGeneric
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ModuleInitializerMethodAndContainingTypesMustNotBeGeneric", null);
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06003740 RID: 14144 RVA: 0x000F92AF File Offset: 0x000F74AF
		internal static string ERR_ModuleInitializerMethodMustBeOrdinary
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ModuleInitializerMethodMustBeOrdinary", null);
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06003741 RID: 14145 RVA: 0x000F92BC File Offset: 0x000F74BC
		internal static string IDS_FeatureExtensionGetAsyncEnumerator
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureExtensionGetAsyncEnumerator", null);
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06003742 RID: 14146 RVA: 0x000F92C9 File Offset: 0x000F74C9
		internal static string IDS_FeatureExtensionGetEnumerator
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureExtensionGetEnumerator", null);
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06003743 RID: 14147 RVA: 0x000F92D6 File Offset: 0x000F74D6
		internal static string ERR_UnmanagedCallersOnlyRequiresStatic
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnmanagedCallersOnlyRequiresStatic", null);
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06003744 RID: 14148 RVA: 0x000F92E3 File Offset: 0x000F74E3
		internal static string ERR_InvalidUnmanagedCallersOnlyCallConv
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InvalidUnmanagedCallersOnlyCallConv", null);
			}
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06003745 RID: 14149 RVA: 0x000F92F0 File Offset: 0x000F74F0
		internal static string ERR_CannotUseManagedTypeInUnmanagedCallersOnly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_CannotUseManagedTypeInUnmanagedCallersOnly", null);
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06003746 RID: 14150 RVA: 0x000F92FD File Offset: 0x000F74FD
		internal static string ERR_UnmanagedCallersOnlyMethodOrTypeCannotBeGeneric
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnmanagedCallersOnlyMethodOrTypeCannotBeGeneric", null);
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06003747 RID: 14151 RVA: 0x000F930A File Offset: 0x000F750A
		internal static string ERR_UnmanagedCallersOnlyMethodsCannotBeCalledDirectly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnmanagedCallersOnlyMethodsCannotBeCalledDirectly", null);
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06003748 RID: 14152 RVA: 0x000F9317 File Offset: 0x000F7517
		internal static string ERR_UnmanagedCallersOnlyMethodsCannotBeConvertedToDelegate
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_UnmanagedCallersOnlyMethodsCannotBeConvertedToDelegate", null);
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06003749 RID: 14153 RVA: 0x000F9324 File Offset: 0x000F7524
		internal static string ERR_EntryPointCannotBeUnmanagedCallersOnly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EntryPointCannotBeUnmanagedCallersOnly", null);
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x0600374A RID: 14154 RVA: 0x000F9331 File Offset: 0x000F7531
		internal static string ERR_ModuleInitializerCannotBeUnmanagedCallersOnly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_ModuleInitializerCannotBeUnmanagedCallersOnly", null);
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x0600374B RID: 14155 RVA: 0x000F933E File Offset: 0x000F753E
		internal static string WRN_RecordEqualsWithoutGetHashCode
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_RecordEqualsWithoutGetHashCode", null);
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x0600374C RID: 14156 RVA: 0x000F934B File Offset: 0x000F754B
		internal static string WRN_RecordEqualsWithoutGetHashCode_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_RecordEqualsWithoutGetHashCode_Title", null);
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x0600374D RID: 14157 RVA: 0x000F9358 File Offset: 0x000F7558
		internal static string ERR_InitCannotBeReadonly
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InitCannotBeReadonly", null);
			}
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x0600374E RID: 14158 RVA: 0x000F9365 File Offset: 0x000F7565
		internal static string IDS_FeatureDiscards
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureDiscards", null);
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x0600374F RID: 14159 RVA: 0x000F9372 File Offset: 0x000F7572
		internal static string IDS_FeatureMixedDeclarationsAndExpressionsInDeconstruction
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureMixedDeclarationsAndExpressionsInDeconstruction", null);
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06003750 RID: 14160 RVA: 0x000F937F File Offset: 0x000F757F
		internal static string IDS_FeatureRecordStructs
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureRecordStructs", null);
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06003751 RID: 14161 RVA: 0x000F938C File Offset: 0x000F758C
		internal static string IDS_FeatureWithOnStructs
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureWithOnStructs", null);
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06003752 RID: 14162 RVA: 0x000F9399 File Offset: 0x000F7599
		internal static string IDS_FeaturePositionalFieldsInRecords
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeaturePositionalFieldsInRecords", null);
			}
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06003753 RID: 14163 RVA: 0x000F93A6 File Offset: 0x000F75A6
		internal static string IDS_FeatureVarianceSafetyForStaticInterfaceMembers
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureVarianceSafetyForStaticInterfaceMembers", null);
			}
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06003754 RID: 14164 RVA: 0x000F93B3 File Offset: 0x000F75B3
		internal static string ERR_EqualityContractRequiresGetter
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_EqualityContractRequiresGetter", null);
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06003755 RID: 14165 RVA: 0x000F93C0 File Offset: 0x000F75C0
		internal static string WRN_AnalyzerReferencesFramework
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AnalyzerReferencesFramework", null);
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06003756 RID: 14166 RVA: 0x000F93CD File Offset: 0x000F75CD
		internal static string WRN_AnalyzerReferencesFramework_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_AnalyzerReferencesFramework_Title", null);
			}
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06003757 RID: 14167 RVA: 0x000F93DA File Offset: 0x000F75DA
		internal static string ERR_BadFieldTypeInRecord
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_BadFieldTypeInRecord", null);
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06003758 RID: 14168 RVA: 0x000F93E7 File Offset: 0x000F75E7
		internal static string ERR_FunctionPointersCannotBeCalledWithNamedArguments
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FunctionPointersCannotBeCalledWithNamedArguments", null);
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06003759 RID: 14169 RVA: 0x000F93F4 File Offset: 0x000F75F4
		internal static string WRN_UnreadRecordParameter
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreadRecordParameter", null);
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x0600375A RID: 14170 RVA: 0x000F9401 File Offset: 0x000F7601
		internal static string WRN_UnreadRecordParameter_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_UnreadRecordParameter_Title", null);
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x0600375B RID: 14171 RVA: 0x000F940E File Offset: 0x000F760E
		internal static string ERR_RecordAmbigCtor
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_RecordAmbigCtor", null);
			}
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x0600375C RID: 14172 RVA: 0x000F941B File Offset: 0x000F761B
		internal static string IDS_FeatureLambdaAttributes
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureLambdaAttributes", null);
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x0600375D RID: 14173 RVA: 0x000F9428 File Offset: 0x000F7628
		internal static string IDS_FeatureInferredDelegateType
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureInferredDelegateType", null);
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x0600375E RID: 14174 RVA: 0x000F9435 File Offset: 0x000F7635
		internal static string WRN_DoNotCompareFunctionPointers
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DoNotCompareFunctionPointers", null);
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x0600375F RID: 14175 RVA: 0x000F9442 File Offset: 0x000F7642
		internal static string WRN_DoNotCompareFunctionPointers_Title
		{
			get
			{
				return CSharpResources.GetResourceString("WRN_DoNotCompareFunctionPointers_Title", null);
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06003760 RID: 14176 RVA: 0x000F944F File Offset: 0x000F764F
		internal static string ERR_FunctionPointerTypesInAttributeNotSupported
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_FunctionPointerTypesInAttributeNotSupported", null);
			}
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06003761 RID: 14177 RVA: 0x000F945C File Offset: 0x000F765C
		internal static string IDS_FeatureSealedToStringInRecord
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureSealedToStringInRecord", null);
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06003762 RID: 14178 RVA: 0x000F9469 File Offset: 0x000F7669
		internal static string ERR_InheritingFromRecordWithSealedToString
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_InheritingFromRecordWithSealedToString", null);
			}
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06003763 RID: 14179 RVA: 0x000F9476 File Offset: 0x000F7676
		internal static string ERR_HiddenPositionalMember
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_HiddenPositionalMember", null);
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06003764 RID: 14180 RVA: 0x000F9483 File Offset: 0x000F7683
		internal static string IDS_FeatureGlobalUsing
		{
			get
			{
				return CSharpResources.GetResourceString("IDS_FeatureGlobalUsing", null);
			}
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06003765 RID: 14181 RVA: 0x000F9490 File Offset: 0x000F7690
		internal static string ERR_GlobalUsingInNamespace
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GlobalUsingInNamespace", null);
			}
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06003766 RID: 14182 RVA: 0x000F949D File Offset: 0x000F769D
		internal static string ERR_GlobalUsingOutOfOrder
		{
			get
			{
				return CSharpResources.GetResourceString("ERR_GlobalUsingOutOfOrder", null);
			}
		}

		// Token: 0x0400138D RID: 5005
		private static ResourceManager s_resourceManager;
	}
}
