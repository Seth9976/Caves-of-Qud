using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.DiaSymReader
{
	// Token: 0x020000EC RID: 236
	[InterfaceType(1)]
	[Guid("BA3FEE4C-ECB9-4e41-83B7-183FA41CD859")]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	internal interface IMetadataEmit
	{
		// Token: 0x0600076C RID: 1900
		void __SetModuleProps();

		// Token: 0x0600076D RID: 1901
		void __Save();

		// Token: 0x0600076E RID: 1902
		void __SaveToStream();

		// Token: 0x0600076F RID: 1903
		void __GetSaveSize();

		// Token: 0x06000770 RID: 1904
		void __DefineTypeDef();

		// Token: 0x06000771 RID: 1905
		void __DefineNestedType();

		// Token: 0x06000772 RID: 1906
		void __SetHandler();

		// Token: 0x06000773 RID: 1907
		void __DefineMethod();

		// Token: 0x06000774 RID: 1908
		void __DefineMethodImpl();

		// Token: 0x06000775 RID: 1909
		void __DefineTypeRefByName();

		// Token: 0x06000776 RID: 1910
		void __DefineImportType();

		// Token: 0x06000777 RID: 1911
		void __DefineMemberRef();

		// Token: 0x06000778 RID: 1912
		void __DefineImportMember();

		// Token: 0x06000779 RID: 1913
		void __DefineEvent();

		// Token: 0x0600077A RID: 1914
		void __SetClassLayout();

		// Token: 0x0600077B RID: 1915
		void __DeleteClassLayout();

		// Token: 0x0600077C RID: 1916
		void __SetFieldMarshal();

		// Token: 0x0600077D RID: 1917
		void __DeleteFieldMarshal();

		// Token: 0x0600077E RID: 1918
		void __DefinePermissionSet();

		// Token: 0x0600077F RID: 1919
		void __SetRVA();

		// Token: 0x06000780 RID: 1920
		unsafe int GetTokenFromSig(byte* voidPointerSig, int byteCountSig);

		// Token: 0x06000781 RID: 1921
		void __DefineModuleRef();

		// Token: 0x06000782 RID: 1922
		void __SetParent();

		// Token: 0x06000783 RID: 1923
		void __GetTokenFromTypeSpec();

		// Token: 0x06000784 RID: 1924
		void __SaveToMemory();

		// Token: 0x06000785 RID: 1925
		void __DefineUserString();

		// Token: 0x06000786 RID: 1926
		void __DeleteToken();

		// Token: 0x06000787 RID: 1927
		void __SetMethodProps();

		// Token: 0x06000788 RID: 1928
		void __SetTypeDefProps();

		// Token: 0x06000789 RID: 1929
		void __SetEventProps();

		// Token: 0x0600078A RID: 1930
		void __SetPermissionSetProps();

		// Token: 0x0600078B RID: 1931
		void __DefinePinvokeMap();

		// Token: 0x0600078C RID: 1932
		void __SetPinvokeMap();

		// Token: 0x0600078D RID: 1933
		void __DeletePinvokeMap();

		// Token: 0x0600078E RID: 1934
		void __DefineCustomAttribute();

		// Token: 0x0600078F RID: 1935
		void __SetCustomAttributeValue();

		// Token: 0x06000790 RID: 1936
		void __DefineField();

		// Token: 0x06000791 RID: 1937
		void __DefineProperty();

		// Token: 0x06000792 RID: 1938
		void __DefineParam();

		// Token: 0x06000793 RID: 1939
		void __SetFieldProps();

		// Token: 0x06000794 RID: 1940
		void __SetPropertyProps();

		// Token: 0x06000795 RID: 1941
		void __SetParamProps();

		// Token: 0x06000796 RID: 1942
		void __DefineSecurityAttributeSet();

		// Token: 0x06000797 RID: 1943
		void __ApplyEditAndContinue();

		// Token: 0x06000798 RID: 1944
		void __TranslateSigWithScope();

		// Token: 0x06000799 RID: 1945
		void __SetMethodImplFlags();

		// Token: 0x0600079A RID: 1946
		void __SetFieldRVA();

		// Token: 0x0600079B RID: 1947
		void __Merge();

		// Token: 0x0600079C RID: 1948
		void __MergeEnd();
	}
}
