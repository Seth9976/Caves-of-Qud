using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Microsoft.DiaSymReader
{
	// Token: 0x020000ED RID: 237
	[ComVisible(false)]
	[InterfaceType(1)]
	[Guid("7DAC8207-D3AE-4c75-9B67-92801A497D44")]
	[ComImport]
	internal interface IMetadataImport
	{
		// Token: 0x0600079D RID: 1949
		[PreserveSig]
		unsafe void CloseEnum(void* enumHandle);

		// Token: 0x0600079E RID: 1950
		[PreserveSig]
		unsafe int CountEnum(void* enumHandle, out int count);

		// Token: 0x0600079F RID: 1951
		[PreserveSig]
		unsafe int ResetEnum(void* enumHandle, int position);

		// Token: 0x060007A0 RID: 1952
		[PreserveSig]
		unsafe int EnumTypeDefs(ref void* enumHandle, [Out] int* typeDefs, int bufferLength, [Out] int* count);

		// Token: 0x060007A1 RID: 1953
		[PreserveSig]
		unsafe int EnumInterfaceImpls(ref void* enumHandle, int typeDef, [Out] int* interfaceImpls, int bufferLength, [Out] int* count);

		// Token: 0x060007A2 RID: 1954
		[PreserveSig]
		unsafe int EnumTypeRefs(ref void* enumHandle, [Out] int* typeRefs, int bufferLength, [Out] int* count);

		// Token: 0x060007A3 RID: 1955
		[PreserveSig]
		int FindTypeDefByName(string name, int enclosingClass, out int typeDef);

		// Token: 0x060007A4 RID: 1956
		[PreserveSig]
		unsafe int GetScopeProps([Out] char* name, int bufferLength, [Out] int* nameLength, [Out] Guid* mvid);

		// Token: 0x060007A5 RID: 1957
		[PreserveSig]
		int GetModuleFromScope(out int moduleDef);

		// Token: 0x060007A6 RID: 1958
		[PreserveSig]
		unsafe int GetTypeDefProps(int typeDef, [Out] char* qualifiedName, int qualifiedNameBufferLength, [Out] int* qualifiedNameLength, [Out] TypeAttributes* attributes, [Out] int* baseType);

		// Token: 0x060007A7 RID: 1959
		[PreserveSig]
		unsafe int GetInterfaceImplProps(int interfaceImpl, [Out] int* typeDef, [Out] int* interfaceDefRefSpec);

		// Token: 0x060007A8 RID: 1960
		[PreserveSig]
		unsafe int GetTypeRefProps(int typeRef, [Out] int* resolutionScope, [Out] char* qualifiedName, int qualifiedNameBufferLength, [Out] int* qualifiedNameLength);

		// Token: 0x060007A9 RID: 1961
		[PreserveSig]
		int ResolveTypeRef(int typeRef, [In] ref Guid scopeInterfaceId, [MarshalAs(28)] out object scope, out int typeDef);

		// Token: 0x060007AA RID: 1962
		[PreserveSig]
		unsafe int EnumMembers(ref void* enumHandle, int typeDef, [Out] int* memberDefs, int bufferLength, [Out] int* count);

		// Token: 0x060007AB RID: 1963
		[PreserveSig]
		unsafe int EnumMembersWithName(ref void* enumHandle, int typeDef, string name, [Out] int* memberDefs, int bufferLength, [Out] int* count);

		// Token: 0x060007AC RID: 1964
		[PreserveSig]
		unsafe int EnumMethods(ref void* enumHandle, int typeDef, [Out] int* methodDefs, int bufferLength, [Out] int* count);

		// Token: 0x060007AD RID: 1965
		[PreserveSig]
		unsafe int EnumMethodsWithName(ref void* enumHandle, int typeDef, string name, [Out] int* methodDefs, int bufferLength, [Out] int* count);

		// Token: 0x060007AE RID: 1966
		[PreserveSig]
		unsafe int EnumFields(ref void* enumHandle, int typeDef, [Out] int* fieldDefs, int bufferLength, [Out] int* count);

		// Token: 0x060007AF RID: 1967
		[PreserveSig]
		unsafe int EnumFieldsWithName(ref void* enumHandle, int typeDef, string name, [Out] int* fieldDefs, int bufferLength, [Out] int* count);

		// Token: 0x060007B0 RID: 1968
		[PreserveSig]
		unsafe int EnumParams(ref void* enumHandle, int methodDef, [Out] int* paramDefs, int bufferLength, [Out] int* count);

		// Token: 0x060007B1 RID: 1969
		[PreserveSig]
		unsafe int EnumMemberRefs(ref void* enumHandle, int parentToken, [Out] int* memberRefs, int bufferLength, [Out] int* count);

		// Token: 0x060007B2 RID: 1970
		[PreserveSig]
		unsafe int EnumMethodImpls(ref void* enumHandle, int typeDef, [Out] int* implementationTokens, [Out] int* declarationTokens, int bufferLength, [Out] int* count);

		// Token: 0x060007B3 RID: 1971
		[PreserveSig]
		unsafe int EnumPermissionSets(ref void* enumHandle, int token, uint action, [Out] int* declSecurityTokens, int bufferLength, [Out] int* count);

		// Token: 0x060007B4 RID: 1972
		[PreserveSig]
		unsafe int FindMember(int typeDef, string name, [In] byte* signature, int signatureLength, out int memberDef);

		// Token: 0x060007B5 RID: 1973
		[PreserveSig]
		unsafe int FindMethod(int typeDef, string name, [In] byte* signature, int signatureLength, out int methodDef);

		// Token: 0x060007B6 RID: 1974
		[PreserveSig]
		unsafe int FindField(int typeDef, string name, [In] byte* signature, int signatureLength, out int fieldDef);

		// Token: 0x060007B7 RID: 1975
		[PreserveSig]
		unsafe int FindMemberRef(int typeDef, string name, [In] byte* signature, int signatureLength, out int memberRef);

		// Token: 0x060007B8 RID: 1976
		[PreserveSig]
		unsafe int GetMethodProps(int methodDef, [Out] int* declaringTypeDef, [Out] char* name, int nameBufferLength, [Out] int* nameLength, [Out] MethodAttributes* attributes, [Out] byte** signature, [Out] int* signatureLength, [Out] int* relativeVirtualAddress, [Out] MethodImplAttributes* implAttributes);

		// Token: 0x060007B9 RID: 1977
		[PreserveSig]
		unsafe int GetMemberRefProps(int memberRef, [Out] int* declaringType, [Out] char* name, int nameBufferLength, [Out] int* nameLength, [Out] byte** signature, [Out] int* signatureLength);

		// Token: 0x060007BA RID: 1978
		[PreserveSig]
		unsafe int EnumProperties(ref void* enumHandle, int typeDef, [Out] int* properties, int bufferLength, [Out] int* count);

		// Token: 0x060007BB RID: 1979
		[PreserveSig]
		unsafe uint EnumEvents(ref void* enumHandle, int typeDef, [Out] int* events, int bufferLength, [Out] int* count);

		// Token: 0x060007BC RID: 1980
		[PreserveSig]
		unsafe int GetEventProps(int @event, [Out] int* declaringTypeDef, [Out] char* name, int nameBufferLength, [Out] int* nameLength, [Out] int* attributes, [Out] int* eventType, [Out] int* adderMethodDef, [Out] int* removerMethodDef, [Out] int* raiserMethodDef, [Out] int* otherMethodDefs, int otherMethodDefBufferLength, [Out] int* methodMethodDefsLength);

		// Token: 0x060007BD RID: 1981
		[PreserveSig]
		unsafe int EnumMethodSemantics(ref void* enumHandle, int methodDef, [Out] int* eventsAndProperties, int bufferLength, [Out] int* count);

		// Token: 0x060007BE RID: 1982
		[PreserveSig]
		unsafe int GetMethodSemantics(int methodDef, int eventOrProperty, [Out] int* semantics);

		// Token: 0x060007BF RID: 1983
		[PreserveSig]
		unsafe int GetClassLayout(int typeDef, [Out] int* packSize, [Out] MetadataImportFieldOffset* fieldOffsets, int bufferLength, [Out] int* count, [Out] int* typeSize);

		// Token: 0x060007C0 RID: 1984
		[PreserveSig]
		unsafe int GetFieldMarshal(int fieldDef, [Out] byte** nativeTypeSignature, [Out] int* nativeTypeSignatureLengvth);

		// Token: 0x060007C1 RID: 1985
		[PreserveSig]
		unsafe int GetRVA(int methodDef, [Out] int* relativeVirtualAddress, [Out] int* implAttributes);

		// Token: 0x060007C2 RID: 1986
		[PreserveSig]
		unsafe int GetPermissionSetProps(int declSecurity, [Out] uint* action, [Out] byte** permissionBlob, [Out] int* permissionBlobLength);

		// Token: 0x060007C3 RID: 1987
		[PreserveSig]
		unsafe int GetSigFromToken(int standaloneSignature, [Out] byte** signature, [Out] int* signatureLength);

		// Token: 0x060007C4 RID: 1988
		[PreserveSig]
		unsafe int GetModuleRefProps(int moduleRef, [Out] char* name, int nameBufferLength, [Out] int* nameLength);

		// Token: 0x060007C5 RID: 1989
		[PreserveSig]
		unsafe int EnumModuleRefs(ref void* enumHandle, [Out] int* moduleRefs, int bufferLength, [Out] int* count);

		// Token: 0x060007C6 RID: 1990
		[PreserveSig]
		unsafe int GetTypeSpecFromToken(int typeSpec, [Out] byte** signature, [Out] int* signatureLength);

		// Token: 0x060007C7 RID: 1991
		[PreserveSig]
		unsafe int GetNameFromToken(int token, [Out] byte* nameUTF8);

		// Token: 0x060007C8 RID: 1992
		[PreserveSig]
		unsafe int EnumUnresolvedMethods(ref void* enumHandle, [Out] int* methodDefs, int bufferLength, [Out] int* count);

		// Token: 0x060007C9 RID: 1993
		[PreserveSig]
		unsafe int GetUserString(int userStringToken, [Out] char* buffer, int bufferLength, [Out] int* length);

		// Token: 0x060007CA RID: 1994
		[PreserveSig]
		unsafe int GetPinvokeMap(int memberDef, [Out] int* attributes, [Out] char* importName, int importNameBufferLength, [Out] int* importNameLength, [Out] int* moduleRef);

		// Token: 0x060007CB RID: 1995
		[PreserveSig]
		unsafe int EnumSignatures(ref void* enumHandle, [Out] int* signatureTokens, int bufferLength, [Out] int* count);

		// Token: 0x060007CC RID: 1996
		[PreserveSig]
		unsafe int EnumTypeSpecs(ref void* enumHandle, [Out] int* typeSpecs, int bufferLength, [Out] int* count);

		// Token: 0x060007CD RID: 1997
		[PreserveSig]
		unsafe int EnumUserStrings(ref void* enumHandle, [Out] int* userStrings, int bufferLength, [Out] int* count);

		// Token: 0x060007CE RID: 1998
		[PreserveSig]
		int GetParamForMethodIndex(int methodDef, int sequenceNumber, out int parameterToken);

		// Token: 0x060007CF RID: 1999
		[PreserveSig]
		unsafe int EnumCustomAttributes(ref void* enumHandle, int parent, int attributeType, [Out] int* customAttributes, int bufferLength, [Out] int* count);

		// Token: 0x060007D0 RID: 2000
		[PreserveSig]
		unsafe int GetCustomAttributeProps(int customAttribute, [Out] int* parent, [Out] int* constructor, [Out] byte** value, [Out] int* valueLength);

		// Token: 0x060007D1 RID: 2001
		[PreserveSig]
		int FindTypeRef(int resolutionScope, string name, out int typeRef);

		// Token: 0x060007D2 RID: 2002
		[PreserveSig]
		unsafe int GetMemberProps(int member, [Out] int* declaringTypeDef, [Out] char* name, int nameBufferLength, [Out] int* nameLength, [Out] int* attributes, [Out] byte** signature, [Out] int* signatureLength, [Out] int* relativeVirtualAddress, [Out] int* implAttributes, [Out] int* constantType, [Out] byte** constantValue, [Out] int* constantValueLength);

		// Token: 0x060007D3 RID: 2003
		[PreserveSig]
		unsafe int GetFieldProps(int fieldDef, [Out] int* declaringTypeDef, [Out] char* name, int nameBufferLength, [Out] int* nameLength, [Out] int* attributes, [Out] byte** signature, [Out] int* signatureLength, [Out] int* constantType, [Out] byte** constantValue, [Out] int* constantValueLength);

		// Token: 0x060007D4 RID: 2004
		[PreserveSig]
		unsafe int GetPropertyProps(int propertyDef, [Out] int* declaringTypeDef, [Out] char* name, int nameBufferLength, [Out] int* nameLength, [Out] int* attributes, [Out] byte** signature, [Out] int* signatureLength, [Out] int* constantType, [Out] byte** constantValue, [Out] int* constantValueLength, [Out] int* setterMethodDef, [Out] int* getterMethodDef, [Out] int* outerMethodDefs, int outerMethodDefsBufferLength, [Out] int* otherMethodDefCount);

		// Token: 0x060007D5 RID: 2005
		[PreserveSig]
		unsafe int GetParamProps(int parameter, [Out] int* declaringMethodDef, [Out] int* sequenceNumber, [Out] char* name, int nameBufferLength, [Out] int* nameLength, [Out] int* attributes, [Out] int* constantType, [Out] byte** constantValue, [Out] int* constantValueLength);

		// Token: 0x060007D6 RID: 2006
		[PreserveSig]
		unsafe int GetCustomAttributeByName(int parent, string name, [Out] byte** value, [Out] int* valueLength);

		// Token: 0x060007D7 RID: 2007
		[PreserveSig]
		bool IsValidToken(int token);

		// Token: 0x060007D8 RID: 2008
		[PreserveSig]
		int GetNestedClassProps(int nestedClass, out int enclosingClass);

		// Token: 0x060007D9 RID: 2009
		[PreserveSig]
		unsafe int GetNativeCallConvFromSig([In] byte* signature, int signatureLength, [Out] int* callingConvention);

		// Token: 0x060007DA RID: 2010
		[PreserveSig]
		int IsGlobal(int token, [Out] bool value);
	}
}
