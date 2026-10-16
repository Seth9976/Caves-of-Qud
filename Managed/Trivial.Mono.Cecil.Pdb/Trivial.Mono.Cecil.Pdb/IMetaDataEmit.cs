using System;
using System.Runtime.InteropServices;

namespace Trivial.Mono.Cecil.Pdb
{
	// Token: 0x02000004 RID: 4
	[InterfaceType(1)]
	[Guid("BA3FEE4C-ECB9-4e41-83B7-183FA41CD859")]
	[ComImport]
	internal interface IMetaDataEmit
	{
		// Token: 0x06000009 RID: 9
		void SetModuleProps(string szName);

		// Token: 0x0600000A RID: 10
		void Save(string szFile, uint dwSaveFlags);

		// Token: 0x0600000B RID: 11
		void SaveToStream(IntPtr pIStream, uint dwSaveFlags);

		// Token: 0x0600000C RID: 12
		uint GetSaveSize(uint fSave);

		// Token: 0x0600000D RID: 13
		uint DefineTypeDef(IntPtr szTypeDef, uint dwTypeDefFlags, uint tkExtends, IntPtr rtkImplements);

		// Token: 0x0600000E RID: 14
		uint DefineNestedType(IntPtr szTypeDef, uint dwTypeDefFlags, uint tkExtends, IntPtr rtkImplements, uint tdEncloser);

		// Token: 0x0600000F RID: 15
		void SetHandler([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06000010 RID: 16
		uint DefineMethod(uint td, IntPtr zName, uint dwMethodFlags, IntPtr pvSigBlob, uint cbSigBlob, uint ulCodeRVA, uint dwImplFlags);

		// Token: 0x06000011 RID: 17
		void DefineMethodImpl(uint td, uint tkBody, uint tkDecl);

		// Token: 0x06000012 RID: 18
		uint DefineTypeRefByName(uint tkResolutionScope, IntPtr szName);

		// Token: 0x06000013 RID: 19
		uint DefineImportType(IntPtr pAssemImport, IntPtr pbHashValue, uint cbHashValue, IMetaDataImport pImport, uint tdImport, IntPtr pAssemEmit);

		// Token: 0x06000014 RID: 20
		uint DefineMemberRef(uint tkImport, string szName, IntPtr pvSigBlob, uint cbSigBlob);

		// Token: 0x06000015 RID: 21
		uint DefineImportMember(IntPtr pAssemImport, IntPtr pbHashValue, uint cbHashValue, IMetaDataImport pImport, uint mbMember, IntPtr pAssemEmit, uint tkParent);

		// Token: 0x06000016 RID: 22
		uint DefineEvent(uint td, string szEvent, uint dwEventFlags, uint tkEventType, uint mdAddOn, uint mdRemoveOn, uint mdFire, IntPtr rmdOtherMethods);

		// Token: 0x06000017 RID: 23
		void SetClassLayout(uint td, uint dwPackSize, IntPtr rFieldOffsets, uint ulClassSize);

		// Token: 0x06000018 RID: 24
		void DeleteClassLayout(uint td);

		// Token: 0x06000019 RID: 25
		void SetFieldMarshal(uint tk, IntPtr pvNativeType, uint cbNativeType);

		// Token: 0x0600001A RID: 26
		void DeleteFieldMarshal(uint tk);

		// Token: 0x0600001B RID: 27
		uint DefinePermissionSet(uint tk, uint dwAction, IntPtr pvPermission, uint cbPermission);

		// Token: 0x0600001C RID: 28
		void SetRVA(uint md, uint ulRVA);

		// Token: 0x0600001D RID: 29
		uint GetTokenFromSig(IntPtr pvSig, uint cbSig);

		// Token: 0x0600001E RID: 30
		uint DefineModuleRef(string szName);

		// Token: 0x0600001F RID: 31
		void SetParent(uint mr, uint tk);

		// Token: 0x06000020 RID: 32
		uint GetTokenFromTypeSpec(IntPtr pvSig, uint cbSig);

		// Token: 0x06000021 RID: 33
		void SaveToMemory(IntPtr pbData, uint cbData);

		// Token: 0x06000022 RID: 34
		uint DefineUserString(string szString, uint cchString);

		// Token: 0x06000023 RID: 35
		void DeleteToken(uint tkObj);

		// Token: 0x06000024 RID: 36
		void SetMethodProps(uint md, uint dwMethodFlags, uint ulCodeRVA, uint dwImplFlags);

		// Token: 0x06000025 RID: 37
		void SetTypeDefProps(uint td, uint dwTypeDefFlags, uint tkExtends, IntPtr rtkImplements);

		// Token: 0x06000026 RID: 38
		void SetEventProps(uint ev, uint dwEventFlags, uint tkEventType, uint mdAddOn, uint mdRemoveOn, uint mdFire, IntPtr rmdOtherMethods);

		// Token: 0x06000027 RID: 39
		uint SetPermissionSetProps(uint tk, uint dwAction, IntPtr pvPermission, uint cbPermission);

		// Token: 0x06000028 RID: 40
		void DefinePinvokeMap(uint tk, uint dwMappingFlags, string szImportName, uint mrImportDLL);

		// Token: 0x06000029 RID: 41
		void SetPinvokeMap(uint tk, uint dwMappingFlags, string szImportName, uint mrImportDLL);

		// Token: 0x0600002A RID: 42
		void DeletePinvokeMap(uint tk);

		// Token: 0x0600002B RID: 43
		uint DefineCustomAttribute(uint tkObj, uint tkType, IntPtr pCustomAttribute, uint cbCustomAttribute);

		// Token: 0x0600002C RID: 44
		void SetCustomAttributeValue(uint pcv, IntPtr pCustomAttribute, uint cbCustomAttribute);

		// Token: 0x0600002D RID: 45
		uint DefineField(uint td, string szName, uint dwFieldFlags, IntPtr pvSigBlob, uint cbSigBlob, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue);

		// Token: 0x0600002E RID: 46
		uint DefineProperty(uint td, string szProperty, uint dwPropFlags, IntPtr pvSig, uint cbSig, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue, uint mdSetter, uint mdGetter, IntPtr rmdOtherMethods);

		// Token: 0x0600002F RID: 47
		uint DefineParam(uint md, uint ulParamSeq, string szName, uint dwParamFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue);

		// Token: 0x06000030 RID: 48
		void SetFieldProps(uint fd, uint dwFieldFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue);

		// Token: 0x06000031 RID: 49
		void SetPropertyProps(uint pr, uint dwPropFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue, uint mdSetter, uint mdGetter, IntPtr rmdOtherMethods);

		// Token: 0x06000032 RID: 50
		void SetParamProps(uint pd, string szName, uint dwParamFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue);

		// Token: 0x06000033 RID: 51
		uint DefineSecurityAttributeSet(uint tkObj, IntPtr rSecAttrs, uint cSecAttrs);

		// Token: 0x06000034 RID: 52
		void ApplyEditAndContinue([MarshalAs(25)] object pImport);

		// Token: 0x06000035 RID: 53
		uint TranslateSigWithScope(IntPtr pAssemImport, IntPtr pbHashValue, uint cbHashValue, IMetaDataImport import, IntPtr pbSigBlob, uint cbSigBlob, IntPtr pAssemEmit, IMetaDataEmit emit, IntPtr pvTranslatedSig, uint cbTranslatedSigMax);

		// Token: 0x06000036 RID: 54
		void SetMethodImplFlags(uint md, uint dwImplFlags);

		// Token: 0x06000037 RID: 55
		void SetFieldRVA(uint fd, uint ulRVA);

		// Token: 0x06000038 RID: 56
		void Merge(IMetaDataImport pImport, IntPtr pHostMapToken, [MarshalAs(25)] object pHandler);

		// Token: 0x06000039 RID: 57
		void MergeEnd();
	}
}
