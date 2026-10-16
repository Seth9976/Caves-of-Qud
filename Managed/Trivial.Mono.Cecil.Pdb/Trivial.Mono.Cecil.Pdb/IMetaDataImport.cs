using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Trivial.Mono.Cecil.Pdb
{
	// Token: 0x02000005 RID: 5
	[InterfaceType(1)]
	[Guid("7DAC8207-D3AE-4c75-9B67-92801A497D44")]
	[ComImport]
	internal interface IMetaDataImport
	{
		// Token: 0x0600003A RID: 58
		[PreserveSig]
		void CloseEnum(uint hEnum);

		// Token: 0x0600003B RID: 59
		uint CountEnum(uint hEnum);

		// Token: 0x0600003C RID: 60
		void ResetEnum(uint hEnum, uint ulPos);

		// Token: 0x0600003D RID: 61
		uint EnumTypeDefs(ref uint phEnum, [MarshalAs(42, SizeParamIndex = 2)] uint[] rTypeDefs, uint cMax);

		// Token: 0x0600003E RID: 62
		uint EnumInterfaceImpls(ref uint phEnum, uint td, [MarshalAs(42, SizeParamIndex = 3)] uint[] rImpls, uint cMax);

		// Token: 0x0600003F RID: 63
		uint EnumTypeRefs(ref uint phEnum, [MarshalAs(42, SizeParamIndex = 2)] uint[] rTypeRefs, uint cMax);

		// Token: 0x06000040 RID: 64
		uint FindTypeDefByName(string szTypeDef, uint tkEnclosingClass);

		// Token: 0x06000041 RID: 65
		Guid GetScopeProps(StringBuilder szName, uint cchName, out uint pchName);

		// Token: 0x06000042 RID: 66
		uint GetModuleFromScope();

		// Token: 0x06000043 RID: 67
		uint GetTypeDefProps(uint td, IntPtr szTypeDef, uint cchTypeDef, out uint pchTypeDef, IntPtr pdwTypeDefFlags);

		// Token: 0x06000044 RID: 68
		uint GetInterfaceImplProps(uint iiImpl, out uint pClass);

		// Token: 0x06000045 RID: 69
		uint GetTypeRefProps(uint tr, out uint ptkResolutionScope, StringBuilder szName, uint cchName);

		// Token: 0x06000046 RID: 70
		uint ResolveTypeRef(uint tr, [In] ref Guid riid, [MarshalAs(28)] out object ppIScope);

		// Token: 0x06000047 RID: 71
		uint EnumMembers(ref uint phEnum, uint cl, [MarshalAs(42, SizeParamIndex = 3)] uint[] rMembers, uint cMax);

		// Token: 0x06000048 RID: 72
		uint EnumMembersWithName(ref uint phEnum, uint cl, string szName, [MarshalAs(42, SizeParamIndex = 4)] uint[] rMembers, uint cMax);

		// Token: 0x06000049 RID: 73
		uint EnumMethods(ref uint phEnum, uint cl, IntPtr rMethods, uint cMax);

		// Token: 0x0600004A RID: 74
		uint EnumMethodsWithName(ref uint phEnum, uint cl, string szName, [MarshalAs(42, SizeParamIndex = 4)] uint[] rMethods, uint cMax);

		// Token: 0x0600004B RID: 75
		uint EnumFields(ref uint phEnum, uint cl, IntPtr rFields, uint cMax);

		// Token: 0x0600004C RID: 76
		uint EnumFieldsWithName(ref uint phEnum, uint cl, string szName, [MarshalAs(42, SizeParamIndex = 4)] uint[] rFields, uint cMax);

		// Token: 0x0600004D RID: 77
		uint EnumParams(ref uint phEnum, uint mb, [MarshalAs(42, SizeParamIndex = 3)] uint[] rParams, uint cMax);

		// Token: 0x0600004E RID: 78
		uint EnumMemberRefs(ref uint phEnum, uint tkParent, [MarshalAs(42, SizeParamIndex = 3)] uint[] rMemberRefs, uint cMax);

		// Token: 0x0600004F RID: 79
		uint EnumMethodImpls(ref uint phEnum, uint td, [MarshalAs(42, SizeParamIndex = 4)] uint[] rMethodBody, [MarshalAs(42, SizeParamIndex = 4)] uint[] rMethodDecl, uint cMax);

		// Token: 0x06000050 RID: 80
		uint EnumPermissionSets(ref uint phEnum, uint tk, uint dwActions, [MarshalAs(42, SizeParamIndex = 4)] uint[] rPermission, uint cMax);

		// Token: 0x06000051 RID: 81
		uint FindMember(uint td, string szName, [MarshalAs(42, SizeParamIndex = 3)] byte[] pvSigBlob, uint cbSigBlob);

		// Token: 0x06000052 RID: 82
		uint FindMethod(uint td, string szName, [MarshalAs(42, SizeParamIndex = 3)] byte[] pvSigBlob, uint cbSigBlob);

		// Token: 0x06000053 RID: 83
		uint FindField(uint td, string szName, [MarshalAs(42, SizeParamIndex = 3)] byte[] pvSigBlob, uint cbSigBlob);

		// Token: 0x06000054 RID: 84
		uint FindMemberRef(uint td, string szName, [MarshalAs(42, SizeParamIndex = 3)] byte[] pvSigBlob, uint cbSigBlob);

		// Token: 0x06000055 RID: 85
		uint GetMethodProps(uint mb, out uint pClass, IntPtr szMethod, uint cchMethod, out uint pchMethod, IntPtr pdwAttr, IntPtr ppvSigBlob, IntPtr pcbSigBlob, IntPtr pulCodeRVA);

		// Token: 0x06000056 RID: 86
		uint GetMemberRefProps(uint mr, ref uint ptk, StringBuilder szMember, uint cchMember, out uint pchMember, out IntPtr ppvSigBlob);

		// Token: 0x06000057 RID: 87
		uint EnumProperties(ref uint phEnum, uint td, IntPtr rProperties, uint cMax);

		// Token: 0x06000058 RID: 88
		uint EnumEvents(ref uint phEnum, uint td, IntPtr rEvents, uint cMax);

		// Token: 0x06000059 RID: 89
		uint GetEventProps(uint ev, out uint pClass, StringBuilder szEvent, uint cchEvent, out uint pchEvent, out uint pdwEventFlags, out uint ptkEventType, out uint pmdAddOn, out uint pmdRemoveOn, out uint pmdFire, [MarshalAs(42, SizeParamIndex = 11)] uint[] rmdOtherMethod, uint cMax);

		// Token: 0x0600005A RID: 90
		uint EnumMethodSemantics(ref uint phEnum, uint mb, [MarshalAs(42, SizeParamIndex = 3)] uint[] rEventProp, uint cMax);

		// Token: 0x0600005B RID: 91
		uint GetMethodSemantics(uint mb, uint tkEventProp);

		// Token: 0x0600005C RID: 92
		uint GetClassLayout(uint td, out uint pdwPackSize, [MarshalAs(42, SizeParamIndex = 3)] IntPtr rFieldOffset, uint cMax, out uint pcFieldOffset);

		// Token: 0x0600005D RID: 93
		uint GetFieldMarshal(uint tk, out IntPtr ppvNativeType);

		// Token: 0x0600005E RID: 94
		uint GetRVA(uint tk, out uint pulCodeRVA);

		// Token: 0x0600005F RID: 95
		uint GetPermissionSetProps(uint pm, out uint pdwAction, out IntPtr ppvPermission);

		// Token: 0x06000060 RID: 96
		uint GetSigFromToken(uint mdSig, out IntPtr ppvSig);

		// Token: 0x06000061 RID: 97
		uint GetModuleRefProps(uint mur, StringBuilder szName, uint cchName);

		// Token: 0x06000062 RID: 98
		uint EnumModuleRefs(ref uint phEnum, [MarshalAs(42, SizeParamIndex = 2)] uint[] rModuleRefs, uint cmax);

		// Token: 0x06000063 RID: 99
		uint GetTypeSpecFromToken(uint typespec, out IntPtr ppvSig);

		// Token: 0x06000064 RID: 100
		uint GetNameFromToken(uint tk);

		// Token: 0x06000065 RID: 101
		uint EnumUnresolvedMethods(ref uint phEnum, [MarshalAs(42, SizeParamIndex = 2)] uint[] rMethods, uint cMax);

		// Token: 0x06000066 RID: 102
		uint GetUserString(uint stk, StringBuilder szString, uint cchString);

		// Token: 0x06000067 RID: 103
		uint GetPinvokeMap(uint tk, out uint pdwMappingFlags, StringBuilder szImportName, uint cchImportName, out uint pchImportName);

		// Token: 0x06000068 RID: 104
		uint EnumSignatures(ref uint phEnum, [MarshalAs(42, SizeParamIndex = 2)] uint[] rSignatures, uint cmax);

		// Token: 0x06000069 RID: 105
		uint EnumTypeSpecs(ref uint phEnum, [MarshalAs(42, SizeParamIndex = 2)] uint[] rTypeSpecs, uint cmax);

		// Token: 0x0600006A RID: 106
		uint EnumUserStrings(ref uint phEnum, [MarshalAs(42, SizeParamIndex = 2)] uint[] rStrings, uint cmax);

		// Token: 0x0600006B RID: 107
		[PreserveSig]
		int GetParamForMethodIndex(uint md, uint ulParamSeq, out uint pParam);

		// Token: 0x0600006C RID: 108
		uint EnumCustomAttributes(ref uint phEnum, uint tk, uint tkType, [MarshalAs(42, SizeParamIndex = 4)] uint[] rCustomAttributes, uint cMax);

		// Token: 0x0600006D RID: 109
		uint GetCustomAttributeProps(uint cv, out uint ptkObj, out uint ptkType, out IntPtr ppBlob);

		// Token: 0x0600006E RID: 110
		uint FindTypeRef(uint tkResolutionScope, string szName);

		// Token: 0x0600006F RID: 111
		uint GetMemberProps(uint mb, out uint pClass, StringBuilder szMember, uint cchMember, out uint pchMember, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pulCodeRVA, out uint pdwImplFlags, out uint pdwCPlusTypeFlag, out IntPtr ppValue);

		// Token: 0x06000070 RID: 112
		uint GetFieldProps(uint mb, out uint pClass, StringBuilder szField, uint cchField, out uint pchField, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pdwCPlusTypeFlag, out IntPtr ppValue);

		// Token: 0x06000071 RID: 113
		uint GetPropertyProps(uint prop, out uint pClass, StringBuilder szProperty, uint cchProperty, out uint pchProperty, out uint pdwPropFlags, out IntPtr ppvSig, out uint pbSig, out uint pdwCPlusTypeFlag, out IntPtr ppDefaultValue, out uint pcchDefaultValue, out uint pmdSetter, out uint pmdGetter, [MarshalAs(42, SizeParamIndex = 14)] uint[] rmdOtherMethod, uint cMax);

		// Token: 0x06000072 RID: 114
		uint GetParamProps(uint tk, out uint pmd, out uint pulSequence, StringBuilder szName, uint cchName, out uint pchName, out uint pdwAttr, out uint pdwCPlusTypeFlag, out IntPtr ppValue);

		// Token: 0x06000073 RID: 115
		uint GetCustomAttributeByName(uint tkObj, string szName, out IntPtr ppData);

		// Token: 0x06000074 RID: 116
		[PreserveSig]
		[return: MarshalAs(2)]
		bool IsValidToken(uint tk);

		// Token: 0x06000075 RID: 117
		uint GetNestedClassProps(uint tdNestedClass);

		// Token: 0x06000076 RID: 118
		uint GetNativeCallConvFromSig(IntPtr pvSig, uint cbSig);

		// Token: 0x06000077 RID: 119
		int IsGlobal(uint pd);
	}
}
