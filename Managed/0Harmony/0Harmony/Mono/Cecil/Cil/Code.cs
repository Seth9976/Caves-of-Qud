using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002B1 RID: 689
	public enum Code
	{
		// Token: 0x040006E5 RID: 1765
		Nop,
		// Token: 0x040006E6 RID: 1766
		Break,
		// Token: 0x040006E7 RID: 1767
		Ldarg_0,
		// Token: 0x040006E8 RID: 1768
		Ldarg_1,
		// Token: 0x040006E9 RID: 1769
		Ldarg_2,
		// Token: 0x040006EA RID: 1770
		Ldarg_3,
		// Token: 0x040006EB RID: 1771
		Ldloc_0,
		// Token: 0x040006EC RID: 1772
		Ldloc_1,
		// Token: 0x040006ED RID: 1773
		Ldloc_2,
		// Token: 0x040006EE RID: 1774
		Ldloc_3,
		// Token: 0x040006EF RID: 1775
		Stloc_0,
		// Token: 0x040006F0 RID: 1776
		Stloc_1,
		// Token: 0x040006F1 RID: 1777
		Stloc_2,
		// Token: 0x040006F2 RID: 1778
		Stloc_3,
		// Token: 0x040006F3 RID: 1779
		Ldarg_S,
		// Token: 0x040006F4 RID: 1780
		Ldarga_S,
		// Token: 0x040006F5 RID: 1781
		Starg_S,
		// Token: 0x040006F6 RID: 1782
		Ldloc_S,
		// Token: 0x040006F7 RID: 1783
		Ldloca_S,
		// Token: 0x040006F8 RID: 1784
		Stloc_S,
		// Token: 0x040006F9 RID: 1785
		Ldnull,
		// Token: 0x040006FA RID: 1786
		Ldc_I4_M1,
		// Token: 0x040006FB RID: 1787
		Ldc_I4_0,
		// Token: 0x040006FC RID: 1788
		Ldc_I4_1,
		// Token: 0x040006FD RID: 1789
		Ldc_I4_2,
		// Token: 0x040006FE RID: 1790
		Ldc_I4_3,
		// Token: 0x040006FF RID: 1791
		Ldc_I4_4,
		// Token: 0x04000700 RID: 1792
		Ldc_I4_5,
		// Token: 0x04000701 RID: 1793
		Ldc_I4_6,
		// Token: 0x04000702 RID: 1794
		Ldc_I4_7,
		// Token: 0x04000703 RID: 1795
		Ldc_I4_8,
		// Token: 0x04000704 RID: 1796
		Ldc_I4_S,
		// Token: 0x04000705 RID: 1797
		Ldc_I4,
		// Token: 0x04000706 RID: 1798
		Ldc_I8,
		// Token: 0x04000707 RID: 1799
		Ldc_R4,
		// Token: 0x04000708 RID: 1800
		Ldc_R8,
		// Token: 0x04000709 RID: 1801
		Dup,
		// Token: 0x0400070A RID: 1802
		Pop,
		// Token: 0x0400070B RID: 1803
		Jmp,
		// Token: 0x0400070C RID: 1804
		Call,
		// Token: 0x0400070D RID: 1805
		Calli,
		// Token: 0x0400070E RID: 1806
		Ret,
		// Token: 0x0400070F RID: 1807
		Br_S,
		// Token: 0x04000710 RID: 1808
		Brfalse_S,
		// Token: 0x04000711 RID: 1809
		Brtrue_S,
		// Token: 0x04000712 RID: 1810
		Beq_S,
		// Token: 0x04000713 RID: 1811
		Bge_S,
		// Token: 0x04000714 RID: 1812
		Bgt_S,
		// Token: 0x04000715 RID: 1813
		Ble_S,
		// Token: 0x04000716 RID: 1814
		Blt_S,
		// Token: 0x04000717 RID: 1815
		Bne_Un_S,
		// Token: 0x04000718 RID: 1816
		Bge_Un_S,
		// Token: 0x04000719 RID: 1817
		Bgt_Un_S,
		// Token: 0x0400071A RID: 1818
		Ble_Un_S,
		// Token: 0x0400071B RID: 1819
		Blt_Un_S,
		// Token: 0x0400071C RID: 1820
		Br,
		// Token: 0x0400071D RID: 1821
		Brfalse,
		// Token: 0x0400071E RID: 1822
		Brtrue,
		// Token: 0x0400071F RID: 1823
		Beq,
		// Token: 0x04000720 RID: 1824
		Bge,
		// Token: 0x04000721 RID: 1825
		Bgt,
		// Token: 0x04000722 RID: 1826
		Ble,
		// Token: 0x04000723 RID: 1827
		Blt,
		// Token: 0x04000724 RID: 1828
		Bne_Un,
		// Token: 0x04000725 RID: 1829
		Bge_Un,
		// Token: 0x04000726 RID: 1830
		Bgt_Un,
		// Token: 0x04000727 RID: 1831
		Ble_Un,
		// Token: 0x04000728 RID: 1832
		Blt_Un,
		// Token: 0x04000729 RID: 1833
		Switch,
		// Token: 0x0400072A RID: 1834
		Ldind_I1,
		// Token: 0x0400072B RID: 1835
		Ldind_U1,
		// Token: 0x0400072C RID: 1836
		Ldind_I2,
		// Token: 0x0400072D RID: 1837
		Ldind_U2,
		// Token: 0x0400072E RID: 1838
		Ldind_I4,
		// Token: 0x0400072F RID: 1839
		Ldind_U4,
		// Token: 0x04000730 RID: 1840
		Ldind_I8,
		// Token: 0x04000731 RID: 1841
		Ldind_I,
		// Token: 0x04000732 RID: 1842
		Ldind_R4,
		// Token: 0x04000733 RID: 1843
		Ldind_R8,
		// Token: 0x04000734 RID: 1844
		Ldind_Ref,
		// Token: 0x04000735 RID: 1845
		Stind_Ref,
		// Token: 0x04000736 RID: 1846
		Stind_I1,
		// Token: 0x04000737 RID: 1847
		Stind_I2,
		// Token: 0x04000738 RID: 1848
		Stind_I4,
		// Token: 0x04000739 RID: 1849
		Stind_I8,
		// Token: 0x0400073A RID: 1850
		Stind_R4,
		// Token: 0x0400073B RID: 1851
		Stind_R8,
		// Token: 0x0400073C RID: 1852
		Add,
		// Token: 0x0400073D RID: 1853
		Sub,
		// Token: 0x0400073E RID: 1854
		Mul,
		// Token: 0x0400073F RID: 1855
		Div,
		// Token: 0x04000740 RID: 1856
		Div_Un,
		// Token: 0x04000741 RID: 1857
		Rem,
		// Token: 0x04000742 RID: 1858
		Rem_Un,
		// Token: 0x04000743 RID: 1859
		And,
		// Token: 0x04000744 RID: 1860
		Or,
		// Token: 0x04000745 RID: 1861
		Xor,
		// Token: 0x04000746 RID: 1862
		Shl,
		// Token: 0x04000747 RID: 1863
		Shr,
		// Token: 0x04000748 RID: 1864
		Shr_Un,
		// Token: 0x04000749 RID: 1865
		Neg,
		// Token: 0x0400074A RID: 1866
		Not,
		// Token: 0x0400074B RID: 1867
		Conv_I1,
		// Token: 0x0400074C RID: 1868
		Conv_I2,
		// Token: 0x0400074D RID: 1869
		Conv_I4,
		// Token: 0x0400074E RID: 1870
		Conv_I8,
		// Token: 0x0400074F RID: 1871
		Conv_R4,
		// Token: 0x04000750 RID: 1872
		Conv_R8,
		// Token: 0x04000751 RID: 1873
		Conv_U4,
		// Token: 0x04000752 RID: 1874
		Conv_U8,
		// Token: 0x04000753 RID: 1875
		Callvirt,
		// Token: 0x04000754 RID: 1876
		Cpobj,
		// Token: 0x04000755 RID: 1877
		Ldobj,
		// Token: 0x04000756 RID: 1878
		Ldstr,
		// Token: 0x04000757 RID: 1879
		Newobj,
		// Token: 0x04000758 RID: 1880
		Castclass,
		// Token: 0x04000759 RID: 1881
		Isinst,
		// Token: 0x0400075A RID: 1882
		Conv_R_Un,
		// Token: 0x0400075B RID: 1883
		Unbox,
		// Token: 0x0400075C RID: 1884
		Throw,
		// Token: 0x0400075D RID: 1885
		Ldfld,
		// Token: 0x0400075E RID: 1886
		Ldflda,
		// Token: 0x0400075F RID: 1887
		Stfld,
		// Token: 0x04000760 RID: 1888
		Ldsfld,
		// Token: 0x04000761 RID: 1889
		Ldsflda,
		// Token: 0x04000762 RID: 1890
		Stsfld,
		// Token: 0x04000763 RID: 1891
		Stobj,
		// Token: 0x04000764 RID: 1892
		Conv_Ovf_I1_Un,
		// Token: 0x04000765 RID: 1893
		Conv_Ovf_I2_Un,
		// Token: 0x04000766 RID: 1894
		Conv_Ovf_I4_Un,
		// Token: 0x04000767 RID: 1895
		Conv_Ovf_I8_Un,
		// Token: 0x04000768 RID: 1896
		Conv_Ovf_U1_Un,
		// Token: 0x04000769 RID: 1897
		Conv_Ovf_U2_Un,
		// Token: 0x0400076A RID: 1898
		Conv_Ovf_U4_Un,
		// Token: 0x0400076B RID: 1899
		Conv_Ovf_U8_Un,
		// Token: 0x0400076C RID: 1900
		Conv_Ovf_I_Un,
		// Token: 0x0400076D RID: 1901
		Conv_Ovf_U_Un,
		// Token: 0x0400076E RID: 1902
		Box,
		// Token: 0x0400076F RID: 1903
		Newarr,
		// Token: 0x04000770 RID: 1904
		Ldlen,
		// Token: 0x04000771 RID: 1905
		Ldelema,
		// Token: 0x04000772 RID: 1906
		Ldelem_I1,
		// Token: 0x04000773 RID: 1907
		Ldelem_U1,
		// Token: 0x04000774 RID: 1908
		Ldelem_I2,
		// Token: 0x04000775 RID: 1909
		Ldelem_U2,
		// Token: 0x04000776 RID: 1910
		Ldelem_I4,
		// Token: 0x04000777 RID: 1911
		Ldelem_U4,
		// Token: 0x04000778 RID: 1912
		Ldelem_I8,
		// Token: 0x04000779 RID: 1913
		Ldelem_I,
		// Token: 0x0400077A RID: 1914
		Ldelem_R4,
		// Token: 0x0400077B RID: 1915
		Ldelem_R8,
		// Token: 0x0400077C RID: 1916
		Ldelem_Ref,
		// Token: 0x0400077D RID: 1917
		Stelem_I,
		// Token: 0x0400077E RID: 1918
		Stelem_I1,
		// Token: 0x0400077F RID: 1919
		Stelem_I2,
		// Token: 0x04000780 RID: 1920
		Stelem_I4,
		// Token: 0x04000781 RID: 1921
		Stelem_I8,
		// Token: 0x04000782 RID: 1922
		Stelem_R4,
		// Token: 0x04000783 RID: 1923
		Stelem_R8,
		// Token: 0x04000784 RID: 1924
		Stelem_Ref,
		// Token: 0x04000785 RID: 1925
		Ldelem_Any,
		// Token: 0x04000786 RID: 1926
		Stelem_Any,
		// Token: 0x04000787 RID: 1927
		Unbox_Any,
		// Token: 0x04000788 RID: 1928
		Conv_Ovf_I1,
		// Token: 0x04000789 RID: 1929
		Conv_Ovf_U1,
		// Token: 0x0400078A RID: 1930
		Conv_Ovf_I2,
		// Token: 0x0400078B RID: 1931
		Conv_Ovf_U2,
		// Token: 0x0400078C RID: 1932
		Conv_Ovf_I4,
		// Token: 0x0400078D RID: 1933
		Conv_Ovf_U4,
		// Token: 0x0400078E RID: 1934
		Conv_Ovf_I8,
		// Token: 0x0400078F RID: 1935
		Conv_Ovf_U8,
		// Token: 0x04000790 RID: 1936
		Refanyval,
		// Token: 0x04000791 RID: 1937
		Ckfinite,
		// Token: 0x04000792 RID: 1938
		Mkrefany,
		// Token: 0x04000793 RID: 1939
		Ldtoken,
		// Token: 0x04000794 RID: 1940
		Conv_U2,
		// Token: 0x04000795 RID: 1941
		Conv_U1,
		// Token: 0x04000796 RID: 1942
		Conv_I,
		// Token: 0x04000797 RID: 1943
		Conv_Ovf_I,
		// Token: 0x04000798 RID: 1944
		Conv_Ovf_U,
		// Token: 0x04000799 RID: 1945
		Add_Ovf,
		// Token: 0x0400079A RID: 1946
		Add_Ovf_Un,
		// Token: 0x0400079B RID: 1947
		Mul_Ovf,
		// Token: 0x0400079C RID: 1948
		Mul_Ovf_Un,
		// Token: 0x0400079D RID: 1949
		Sub_Ovf,
		// Token: 0x0400079E RID: 1950
		Sub_Ovf_Un,
		// Token: 0x0400079F RID: 1951
		Endfinally,
		// Token: 0x040007A0 RID: 1952
		Leave,
		// Token: 0x040007A1 RID: 1953
		Leave_S,
		// Token: 0x040007A2 RID: 1954
		Stind_I,
		// Token: 0x040007A3 RID: 1955
		Conv_U,
		// Token: 0x040007A4 RID: 1956
		Arglist,
		// Token: 0x040007A5 RID: 1957
		Ceq,
		// Token: 0x040007A6 RID: 1958
		Cgt,
		// Token: 0x040007A7 RID: 1959
		Cgt_Un,
		// Token: 0x040007A8 RID: 1960
		Clt,
		// Token: 0x040007A9 RID: 1961
		Clt_Un,
		// Token: 0x040007AA RID: 1962
		Ldftn,
		// Token: 0x040007AB RID: 1963
		Ldvirtftn,
		// Token: 0x040007AC RID: 1964
		Ldarg,
		// Token: 0x040007AD RID: 1965
		Ldarga,
		// Token: 0x040007AE RID: 1966
		Starg,
		// Token: 0x040007AF RID: 1967
		Ldloc,
		// Token: 0x040007B0 RID: 1968
		Ldloca,
		// Token: 0x040007B1 RID: 1969
		Stloc,
		// Token: 0x040007B2 RID: 1970
		Localloc,
		// Token: 0x040007B3 RID: 1971
		Endfilter,
		// Token: 0x040007B4 RID: 1972
		Unaligned,
		// Token: 0x040007B5 RID: 1973
		Volatile,
		// Token: 0x040007B6 RID: 1974
		Tail,
		// Token: 0x040007B7 RID: 1975
		Initobj,
		// Token: 0x040007B8 RID: 1976
		Constrained,
		// Token: 0x040007B9 RID: 1977
		Cpblk,
		// Token: 0x040007BA RID: 1978
		Initblk,
		// Token: 0x040007BB RID: 1979
		No,
		// Token: 0x040007BC RID: 1980
		Rethrow,
		// Token: 0x040007BD RID: 1981
		Sizeof,
		// Token: 0x040007BE RID: 1982
		Refanytype,
		// Token: 0x040007BF RID: 1983
		Readonly
	}
}
