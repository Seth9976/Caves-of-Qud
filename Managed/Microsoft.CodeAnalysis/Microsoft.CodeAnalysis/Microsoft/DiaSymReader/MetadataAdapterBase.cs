using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Microsoft.DiaSymReader
{
	// Token: 0x020000EF RID: 239
	internal class MetadataAdapterBase : IMetadataImport, IMetadataEmit
	{
		// Token: 0x060007DE RID: 2014 RVA: 0x00018447 File Offset: 0x00016647
		public unsafe virtual int GetTokenFromSig(byte* voidPointerSig, int byteCountSig)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001844E File Offset: 0x0001664E
		public unsafe virtual int GetSigFromToken(int standaloneSignature, [Out] byte** signature, [Out] int* signatureLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00018455 File Offset: 0x00016655
		public unsafe virtual int GetTypeDefProps(int typeDef, [Out] char* qualifiedName, int qualifiedNameBufferLength, [Out] int* qualifiedNameLength, [Out] TypeAttributes* attributes, [Out] int* baseType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001845C File Offset: 0x0001665C
		public unsafe virtual int GetTypeRefProps(int typeRef, [Out] int* resolutionScope, [Out] char* qualifiedName, int qualifiedNameBufferLength, [Out] int* qualifiedNameLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00018463 File Offset: 0x00016663
		public virtual int GetNestedClassProps(int nestedClass, out int enclosingClass)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001846A File Offset: 0x0001666A
		public unsafe virtual int GetMethodProps(int methodDef, [Out] int* declaringTypeDef, [Out] char* name, int nameBufferLength, [Out] int* nameLength, [Out] MethodAttributes* attributes, [Out] byte** signature, [Out] int* signatureLength, [Out] int* relativeVirtualAddress, [Out] MethodImplAttributes* implAttributes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00018471 File Offset: 0x00016671
		unsafe void IMetadataImport.CloseEnum(void* enumHandle)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00018478 File Offset: 0x00016678
		unsafe int IMetadataImport.CountEnum(void* enumHandle, out int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001847F File Offset: 0x0001667F
		unsafe int IMetadataImport.ResetEnum(void* enumHandle, int position)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00018486 File Offset: 0x00016686
		unsafe int IMetadataImport.EnumTypeDefs(ref void* enumHandle, int* typeDefs, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001848D File Offset: 0x0001668D
		unsafe int IMetadataImport.EnumInterfaceImpls(ref void* enumHandle, int typeDef, int* interfaceImpls, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00018494 File Offset: 0x00016694
		unsafe int IMetadataImport.EnumTypeRefs(ref void* enumHandle, int* typeRefs, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001849B File Offset: 0x0001669B
		int IMetadataImport.FindTypeDefByName(string name, int enclosingClass, out int typeDef)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000184A2 File Offset: 0x000166A2
		unsafe int IMetadataImport.GetScopeProps(char* name, int bufferLength, int* nameLength, Guid* mvid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x000184A9 File Offset: 0x000166A9
		int IMetadataImport.GetModuleFromScope(out int moduleDef)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x000184B0 File Offset: 0x000166B0
		unsafe int IMetadataImport.GetInterfaceImplProps(int interfaceImpl, int* typeDef, int* interfaceDefRefSpec)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000184B7 File Offset: 0x000166B7
		int IMetadataImport.ResolveTypeRef(int typeRef, ref Guid scopeInterfaceId, out object scope, out int typeDef)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x000184BE File Offset: 0x000166BE
		unsafe int IMetadataImport.EnumMembers(ref void* enumHandle, int typeDef, int* memberDefs, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000184C5 File Offset: 0x000166C5
		unsafe int IMetadataImport.EnumMembersWithName(ref void* enumHandle, int typeDef, string name, int* memberDefs, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000184CC File Offset: 0x000166CC
		unsafe int IMetadataImport.EnumMethods(ref void* enumHandle, int typeDef, int* methodDefs, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000184D3 File Offset: 0x000166D3
		unsafe int IMetadataImport.EnumMethodsWithName(ref void* enumHandle, int typeDef, string name, int* methodDefs, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000184DA File Offset: 0x000166DA
		unsafe int IMetadataImport.EnumFields(ref void* enumHandle, int typeDef, int* fieldDefs, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x000184E1 File Offset: 0x000166E1
		unsafe int IMetadataImport.EnumFieldsWithName(ref void* enumHandle, int typeDef, string name, int* fieldDefs, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000184E8 File Offset: 0x000166E8
		unsafe int IMetadataImport.EnumParams(ref void* enumHandle, int methodDef, int* paramDefs, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x000184EF File Offset: 0x000166EF
		unsafe int IMetadataImport.EnumMemberRefs(ref void* enumHandle, int parentToken, int* memberRefs, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000184F6 File Offset: 0x000166F6
		unsafe int IMetadataImport.EnumMethodImpls(ref void* enumHandle, int typeDef, int* implementationTokens, int* declarationTokens, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000184FD File Offset: 0x000166FD
		unsafe int IMetadataImport.EnumPermissionSets(ref void* enumHandle, int token, uint action, int* declSecurityTokens, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00018504 File Offset: 0x00016704
		unsafe int IMetadataImport.FindMember(int typeDef, string name, byte* signature, int signatureLength, out int memberDef)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001850B File Offset: 0x0001670B
		unsafe int IMetadataImport.FindMethod(int typeDef, string name, byte* signature, int signatureLength, out int methodDef)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00018512 File Offset: 0x00016712
		unsafe int IMetadataImport.FindField(int typeDef, string name, byte* signature, int signatureLength, out int fieldDef)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00018519 File Offset: 0x00016719
		unsafe int IMetadataImport.FindMemberRef(int typeDef, string name, byte* signature, int signatureLength, out int memberRef)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00018520 File Offset: 0x00016720
		unsafe int IMetadataImport.GetMemberRefProps(int memberRef, int* declaringType, char* name, int nameBufferLength, int* nameLength, byte** signature, int* signatureLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00018527 File Offset: 0x00016727
		unsafe int IMetadataImport.EnumProperties(ref void* enumHandle, int typeDef, int* properties, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001852E File Offset: 0x0001672E
		unsafe uint IMetadataImport.EnumEvents(ref void* enumHandle, int typeDef, int* events, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00018535 File Offset: 0x00016735
		unsafe int IMetadataImport.GetEventProps(int @event, int* declaringTypeDef, char* name, int nameBufferLength, int* nameLength, int* attributes, int* eventType, int* adderMethodDef, int* removerMethodDef, int* raiserMethodDef, int* otherMethodDefs, int otherMethodDefBufferLength, int* methodMethodDefsLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001853C File Offset: 0x0001673C
		unsafe int IMetadataImport.EnumMethodSemantics(ref void* enumHandle, int methodDef, int* eventsAndProperties, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00018543 File Offset: 0x00016743
		unsafe int IMetadataImport.GetMethodSemantics(int methodDef, int eventOrProperty, int* semantics)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001854A File Offset: 0x0001674A
		unsafe int IMetadataImport.GetClassLayout(int typeDef, int* packSize, MetadataImportFieldOffset* fieldOffsets, int bufferLength, int* count, int* typeSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00018551 File Offset: 0x00016751
		unsafe int IMetadataImport.GetFieldMarshal(int fieldDef, byte** nativeTypeSignature, int* nativeTypeSignatureLengvth)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00018558 File Offset: 0x00016758
		unsafe int IMetadataImport.GetRVA(int methodDef, int* relativeVirtualAddress, int* implAttributes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001855F File Offset: 0x0001675F
		unsafe int IMetadataImport.GetPermissionSetProps(int declSecurity, uint* action, byte** permissionBlob, int* permissionBlobLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00018566 File Offset: 0x00016766
		unsafe int IMetadataImport.GetModuleRefProps(int moduleRef, char* name, int nameBufferLength, int* nameLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001856D File Offset: 0x0001676D
		unsafe int IMetadataImport.EnumModuleRefs(ref void* enumHandle, int* moduleRefs, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00018574 File Offset: 0x00016774
		unsafe int IMetadataImport.GetTypeSpecFromToken(int typeSpec, byte** signature, int* signatureLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001857B File Offset: 0x0001677B
		unsafe int IMetadataImport.GetNameFromToken(int token, byte* nameUTF8)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00018582 File Offset: 0x00016782
		unsafe int IMetadataImport.EnumUnresolvedMethods(ref void* enumHandle, int* methodDefs, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00018589 File Offset: 0x00016789
		unsafe int IMetadataImport.GetUserString(int userStringToken, char* buffer, int bufferLength, int* length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00018590 File Offset: 0x00016790
		unsafe int IMetadataImport.GetPinvokeMap(int memberDef, int* attributes, char* importName, int importNameBufferLength, int* importNameLength, int* moduleRef)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00018597 File Offset: 0x00016797
		unsafe int IMetadataImport.EnumSignatures(ref void* enumHandle, int* signatureTokens, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001859E File Offset: 0x0001679E
		unsafe int IMetadataImport.EnumTypeSpecs(ref void* enumHandle, int* typeSpecs, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x000185A5 File Offset: 0x000167A5
		unsafe int IMetadataImport.EnumUserStrings(ref void* enumHandle, int* userStrings, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x000185AC File Offset: 0x000167AC
		int IMetadataImport.GetParamForMethodIndex(int methodDef, int sequenceNumber, out int parameterToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x000185B3 File Offset: 0x000167B3
		unsafe int IMetadataImport.EnumCustomAttributes(ref void* enumHandle, int parent, int attributeType, int* customAttributes, int bufferLength, int* count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x000185BA File Offset: 0x000167BA
		unsafe int IMetadataImport.GetCustomAttributeProps(int customAttribute, int* parent, int* constructor, byte** value, int* valueLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x000185C1 File Offset: 0x000167C1
		int IMetadataImport.FindTypeRef(int resolutionScope, string name, out int typeRef)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x000185C8 File Offset: 0x000167C8
		unsafe int IMetadataImport.GetMemberProps(int member, int* declaringTypeDef, char* name, int nameBufferLength, int* nameLength, int* attributes, byte** signature, int* signatureLength, int* relativeVirtualAddress, int* implAttributes, int* constantType, byte** constantValue, int* constantValueLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000185CF File Offset: 0x000167CF
		unsafe int IMetadataImport.GetFieldProps(int fieldDef, int* declaringTypeDef, char* name, int nameBufferLength, int* nameLength, int* attributes, byte** signature, int* signatureLength, int* constantType, byte** constantValue, int* constantValueLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000185D6 File Offset: 0x000167D6
		unsafe int IMetadataImport.GetPropertyProps(int propertyDef, int* declaringTypeDef, char* name, int nameBufferLength, int* nameLength, int* attributes, byte** signature, int* signatureLength, int* constantType, byte** constantValue, int* constantValueLength, int* setterMethodDef, int* getterMethodDef, int* outerMethodDefs, int outerMethodDefsBufferLength, int* otherMethodDefCount)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x000185DD File Offset: 0x000167DD
		unsafe int IMetadataImport.GetParamProps(int parameter, int* declaringMethodDef, int* sequenceNumber, char* name, int nameBufferLength, int* nameLength, int* attributes, int* constantType, byte** constantValue, int* constantValueLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x000185E4 File Offset: 0x000167E4
		unsafe int IMetadataImport.GetCustomAttributeByName(int parent, string name, byte** value, int* valueLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x000185EB File Offset: 0x000167EB
		bool IMetadataImport.IsValidToken(int token)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x000185F2 File Offset: 0x000167F2
		unsafe int IMetadataImport.GetNativeCallConvFromSig(byte* signature, int signatureLength, int* callingConvention)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x000185F9 File Offset: 0x000167F9
		int IMetadataImport.IsGlobal(int token, bool value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00018600 File Offset: 0x00016800
		void IMetadataEmit.__SetModuleProps()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00018607 File Offset: 0x00016807
		void IMetadataEmit.__Save()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001860E File Offset: 0x0001680E
		void IMetadataEmit.__SaveToStream()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00018615 File Offset: 0x00016815
		void IMetadataEmit.__GetSaveSize()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001861C File Offset: 0x0001681C
		void IMetadataEmit.__DefineTypeDef()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00018623 File Offset: 0x00016823
		void IMetadataEmit.__DefineNestedType()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001862A File Offset: 0x0001682A
		void IMetadataEmit.__SetHandler()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00018631 File Offset: 0x00016831
		void IMetadataEmit.__DefineMethod()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00018638 File Offset: 0x00016838
		void IMetadataEmit.__DefineMethodImpl()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001863F File Offset: 0x0001683F
		void IMetadataEmit.__DefineTypeRefByName()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00018646 File Offset: 0x00016846
		void IMetadataEmit.__DefineImportType()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001864D File Offset: 0x0001684D
		void IMetadataEmit.__DefineMemberRef()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00018654 File Offset: 0x00016854
		void IMetadataEmit.__DefineImportMember()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001865B File Offset: 0x0001685B
		void IMetadataEmit.__DefineEvent()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00018662 File Offset: 0x00016862
		void IMetadataEmit.__SetClassLayout()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00018669 File Offset: 0x00016869
		void IMetadataEmit.__DeleteClassLayout()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00018670 File Offset: 0x00016870
		void IMetadataEmit.__SetFieldMarshal()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00018677 File Offset: 0x00016877
		void IMetadataEmit.__DeleteFieldMarshal()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001867E File Offset: 0x0001687E
		void IMetadataEmit.__DefinePermissionSet()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00018685 File Offset: 0x00016885
		void IMetadataEmit.__SetRVA()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001868C File Offset: 0x0001688C
		void IMetadataEmit.__DefineModuleRef()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00018693 File Offset: 0x00016893
		void IMetadataEmit.__SetParent()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001869A File Offset: 0x0001689A
		void IMetadataEmit.__GetTokenFromTypeSpec()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000186A1 File Offset: 0x000168A1
		void IMetadataEmit.__SaveToMemory()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x000186A8 File Offset: 0x000168A8
		void IMetadataEmit.__DefineUserString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x000186AF File Offset: 0x000168AF
		void IMetadataEmit.__DeleteToken()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x000186B6 File Offset: 0x000168B6
		void IMetadataEmit.__SetMethodProps()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000186BD File Offset: 0x000168BD
		void IMetadataEmit.__SetTypeDefProps()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x000186C4 File Offset: 0x000168C4
		void IMetadataEmit.__SetEventProps()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x000186CB File Offset: 0x000168CB
		void IMetadataEmit.__SetPermissionSetProps()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000186D2 File Offset: 0x000168D2
		void IMetadataEmit.__DefinePinvokeMap()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000186D9 File Offset: 0x000168D9
		void IMetadataEmit.__SetPinvokeMap()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000186E0 File Offset: 0x000168E0
		void IMetadataEmit.__DeletePinvokeMap()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x000186E7 File Offset: 0x000168E7
		void IMetadataEmit.__DefineCustomAttribute()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000186EE File Offset: 0x000168EE
		void IMetadataEmit.__SetCustomAttributeValue()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000186F5 File Offset: 0x000168F5
		void IMetadataEmit.__DefineField()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000186FC File Offset: 0x000168FC
		void IMetadataEmit.__DefineProperty()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00018703 File Offset: 0x00016903
		void IMetadataEmit.__DefineParam()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001870A File Offset: 0x0001690A
		void IMetadataEmit.__SetFieldProps()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00018711 File Offset: 0x00016911
		void IMetadataEmit.__SetPropertyProps()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00018718 File Offset: 0x00016918
		void IMetadataEmit.__SetParamProps()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001871F File Offset: 0x0001691F
		void IMetadataEmit.__DefineSecurityAttributeSet()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00018726 File Offset: 0x00016926
		void IMetadataEmit.__ApplyEditAndContinue()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001872D File Offset: 0x0001692D
		void IMetadataEmit.__TranslateSigWithScope()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00018734 File Offset: 0x00016934
		void IMetadataEmit.__SetMethodImplFlags()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001873B File Offset: 0x0001693B
		void IMetadataEmit.__SetFieldRVA()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00018742 File Offset: 0x00016942
		void IMetadataEmit.__Merge()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00018749 File Offset: 0x00016949
		void IMetadataEmit.__MergeEnd()
		{
			throw new NotImplementedException();
		}
	}
}
