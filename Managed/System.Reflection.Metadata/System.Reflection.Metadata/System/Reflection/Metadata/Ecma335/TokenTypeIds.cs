using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000124 RID: 292
	internal static class TokenTypeIds
	{
		// Token: 0x06000A44 RID: 2628 RVA: 0x0001DA7D File Offset: 0x0001BC7D
		internal static bool IsEntityOrUserStringToken(uint vToken)
		{
			return (vToken & 2130706432U) <= 1879048192U;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0001DA90 File Offset: 0x0001BC90
		internal static bool IsEntityToken(uint vToken)
		{
			return (vToken & 2130706432U) < 1879048192U;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001DAA0 File Offset: 0x0001BCA0
		internal static bool IsValidRowId(uint rowId)
		{
			return (rowId & 4278190080U) == 0U;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0001DAAC File Offset: 0x0001BCAC
		internal static bool IsValidRowId(int rowId)
		{
			return ((long)rowId & (long)((ulong)(-16777216))) == 0L;
		}

		// Token: 0x04000700 RID: 1792
		internal const uint Module = 0U;

		// Token: 0x04000701 RID: 1793
		internal const uint TypeRef = 16777216U;

		// Token: 0x04000702 RID: 1794
		internal const uint TypeDef = 33554432U;

		// Token: 0x04000703 RID: 1795
		internal const uint FieldDef = 67108864U;

		// Token: 0x04000704 RID: 1796
		internal const uint MethodDef = 100663296U;

		// Token: 0x04000705 RID: 1797
		internal const uint ParamDef = 134217728U;

		// Token: 0x04000706 RID: 1798
		internal const uint InterfaceImpl = 150994944U;

		// Token: 0x04000707 RID: 1799
		internal const uint MemberRef = 167772160U;

		// Token: 0x04000708 RID: 1800
		internal const uint Constant = 184549376U;

		// Token: 0x04000709 RID: 1801
		internal const uint CustomAttribute = 201326592U;

		// Token: 0x0400070A RID: 1802
		internal const uint DeclSecurity = 234881024U;

		// Token: 0x0400070B RID: 1803
		internal const uint Signature = 285212672U;

		// Token: 0x0400070C RID: 1804
		internal const uint EventMap = 301989888U;

		// Token: 0x0400070D RID: 1805
		internal const uint Event = 335544320U;

		// Token: 0x0400070E RID: 1806
		internal const uint PropertyMap = 352321536U;

		// Token: 0x0400070F RID: 1807
		internal const uint Property = 385875968U;

		// Token: 0x04000710 RID: 1808
		internal const uint MethodSemantics = 402653184U;

		// Token: 0x04000711 RID: 1809
		internal const uint MethodImpl = 419430400U;

		// Token: 0x04000712 RID: 1810
		internal const uint ModuleRef = 436207616U;

		// Token: 0x04000713 RID: 1811
		internal const uint TypeSpec = 452984832U;

		// Token: 0x04000714 RID: 1812
		internal const uint Assembly = 536870912U;

		// Token: 0x04000715 RID: 1813
		internal const uint AssemblyRef = 587202560U;

		// Token: 0x04000716 RID: 1814
		internal const uint File = 637534208U;

		// Token: 0x04000717 RID: 1815
		internal const uint ExportedType = 654311424U;

		// Token: 0x04000718 RID: 1816
		internal const uint ManifestResource = 671088640U;

		// Token: 0x04000719 RID: 1817
		internal const uint NestedClass = 687865856U;

		// Token: 0x0400071A RID: 1818
		internal const uint GenericParam = 704643072U;

		// Token: 0x0400071B RID: 1819
		internal const uint MethodSpec = 721420288U;

		// Token: 0x0400071C RID: 1820
		internal const uint GenericParamConstraint = 738197504U;

		// Token: 0x0400071D RID: 1821
		internal const uint Document = 805306368U;

		// Token: 0x0400071E RID: 1822
		internal const uint MethodDebugInformation = 822083584U;

		// Token: 0x0400071F RID: 1823
		internal const uint LocalScope = 838860800U;

		// Token: 0x04000720 RID: 1824
		internal const uint LocalVariable = 855638016U;

		// Token: 0x04000721 RID: 1825
		internal const uint LocalConstant = 872415232U;

		// Token: 0x04000722 RID: 1826
		internal const uint ImportScope = 889192448U;

		// Token: 0x04000723 RID: 1827
		internal const uint AsyncMethod = 905969664U;

		// Token: 0x04000724 RID: 1828
		internal const uint CustomDebugInformation = 922746880U;

		// Token: 0x04000725 RID: 1829
		internal const uint UserString = 1879048192U;

		// Token: 0x04000726 RID: 1830
		internal const int RowIdBitCount = 24;

		// Token: 0x04000727 RID: 1831
		internal const uint RIDMask = 16777215U;

		// Token: 0x04000728 RID: 1832
		internal const uint TypeMask = 2130706432U;

		// Token: 0x04000729 RID: 1833
		internal const uint VirtualBit = 2147483648U;
	}
}
