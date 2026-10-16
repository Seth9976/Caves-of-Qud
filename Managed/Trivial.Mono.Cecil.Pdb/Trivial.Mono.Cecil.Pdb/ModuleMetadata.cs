using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Trivial.Mono.Cecil.Pdb
{
	// Token: 0x02000008 RID: 8
	internal class ModuleMetadata : IMetaDataEmit, IMetaDataImport
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00002290 File Offset: 0x00000490
		public ModuleMetadata(ModuleDefinition module)
		{
			this.module = module;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000229F File Offset: 0x0000049F
		private bool TryGetType(uint token, out TypeDefinition type)
		{
			if (this.types == null)
			{
				this.InitializeMetadata(this.module);
			}
			return this.types.TryGetValue(token, ref type);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000022C2 File Offset: 0x000004C2
		private bool TryGetMethod(uint token, out MethodDefinition method)
		{
			if (this.methods == null)
			{
				this.InitializeMetadata(this.module);
			}
			return this.methods.TryGetValue(token, ref method);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000022E8 File Offset: 0x000004E8
		private void InitializeMetadata(ModuleDefinition module)
		{
			this.types = new Dictionary<uint, TypeDefinition>();
			this.methods = new Dictionary<uint, MethodDefinition>();
			foreach (TypeDefinition typeDefinition in module.GetTypes())
			{
				this.types.Add(typeDefinition.MetadataToken.ToUInt32(), typeDefinition);
				this.InitializeMethods(typeDefinition);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002368 File Offset: 0x00000568
		private void InitializeMethods(TypeDefinition type)
		{
			foreach (MethodDefinition methodDefinition in type.Methods)
			{
				this.methods.Add(methodDefinition.MetadataToken.ToUInt32(), methodDefinition);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SetModuleProps(string szName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000023D0 File Offset: 0x000005D0
		public void Save(string szFile, uint dwSaveFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SaveToStream(IntPtr pIStream, uint dwSaveFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetSaveSize(uint fSave)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint DefineTypeDef(IntPtr szTypeDef, uint dwTypeDefFlags, uint tkExtends, IntPtr rtkImplements)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint DefineNestedType(IntPtr szTypeDef, uint dwTypeDefFlags, uint tkExtends, IntPtr rtkImplements, uint tdEncloser)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SetHandler(object pUnk)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint DefineMethod(uint td, IntPtr zName, uint dwMethodFlags, IntPtr pvSigBlob, uint cbSigBlob, uint ulCodeRVA, uint dwImplFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000023D0 File Offset: 0x000005D0
		public void DefineMethodImpl(uint td, uint tkBody, uint tkDecl)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint DefineTypeRefByName(uint tkResolutionScope, IntPtr szName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint DefineImportType(IntPtr pAssemImport, IntPtr pbHashValue, uint cbHashValue, IMetaDataImport pImport, uint tdImport, IntPtr pAssemEmit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint DefineMemberRef(uint tkImport, string szName, IntPtr pvSigBlob, uint cbSigBlob)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint DefineImportMember(IntPtr pAssemImport, IntPtr pbHashValue, uint cbHashValue, IMetaDataImport pImport, uint mbMember, IntPtr pAssemEmit, uint tkParent)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint DefineEvent(uint td, string szEvent, uint dwEventFlags, uint tkEventType, uint mdAddOn, uint mdRemoveOn, uint mdFire, IntPtr rmdOtherMethods)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SetClassLayout(uint td, uint dwPackSize, IntPtr rFieldOffsets, uint ulClassSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000023D0 File Offset: 0x000005D0
		public void DeleteClassLayout(uint td)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SetFieldMarshal(uint tk, IntPtr pvNativeType, uint cbNativeType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000023D0 File Offset: 0x000005D0
		public void DeleteFieldMarshal(uint tk)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint DefinePermissionSet(uint tk, uint dwAction, IntPtr pvPermission, uint cbPermission)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SetRVA(uint md, uint ulRVA)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetTokenFromSig(IntPtr pvSig, uint cbSig)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint DefineModuleRef(string szName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SetParent(uint mr, uint tk)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetTokenFromTypeSpec(IntPtr pvSig, uint cbSig)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SaveToMemory(IntPtr pbData, uint cbData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint DefineUserString(string szString, uint cchString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000023D0 File Offset: 0x000005D0
		public void DeleteToken(uint tkObj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SetMethodProps(uint md, uint dwMethodFlags, uint ulCodeRVA, uint dwImplFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SetTypeDefProps(uint td, uint dwTypeDefFlags, uint tkExtends, IntPtr rtkImplements)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SetEventProps(uint ev, uint dwEventFlags, uint tkEventType, uint mdAddOn, uint mdRemoveOn, uint mdFire, IntPtr rmdOtherMethods)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint SetPermissionSetProps(uint tk, uint dwAction, IntPtr pvPermission, uint cbPermission)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000023D0 File Offset: 0x000005D0
		public void DefinePinvokeMap(uint tk, uint dwMappingFlags, string szImportName, uint mrImportDLL)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SetPinvokeMap(uint tk, uint dwMappingFlags, string szImportName, uint mrImportDLL)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000023D0 File Offset: 0x000005D0
		public void DeletePinvokeMap(uint tk)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint DefineCustomAttribute(uint tkObj, uint tkType, IntPtr pCustomAttribute, uint cbCustomAttribute)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SetCustomAttributeValue(uint pcv, IntPtr pCustomAttribute, uint cbCustomAttribute)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint DefineField(uint td, string szName, uint dwFieldFlags, IntPtr pvSigBlob, uint cbSigBlob, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint DefineProperty(uint td, string szProperty, uint dwPropFlags, IntPtr pvSig, uint cbSig, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue, uint mdSetter, uint mdGetter, IntPtr rmdOtherMethods)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint DefineParam(uint md, uint ulParamSeq, string szName, uint dwParamFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SetFieldProps(uint fd, uint dwFieldFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SetPropertyProps(uint pr, uint dwPropFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue, uint mdSetter, uint mdGetter, IntPtr rmdOtherMethods)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SetParamProps(uint pd, string szName, uint dwParamFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint DefineSecurityAttributeSet(uint tkObj, IntPtr rSecAttrs, uint cSecAttrs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000023D0 File Offset: 0x000005D0
		public void ApplyEditAndContinue(object pImport)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint TranslateSigWithScope(IntPtr pAssemImport, IntPtr pbHashValue, uint cbHashValue, IMetaDataImport import, IntPtr pbSigBlob, uint cbSigBlob, IntPtr pAssemEmit, IMetaDataEmit emit, IntPtr pvTranslatedSig, uint cbTranslatedSigMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SetMethodImplFlags(uint md, uint dwImplFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SetFieldRVA(uint fd, uint ulRVA)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000023D0 File Offset: 0x000005D0
		public void Merge(IMetaDataImport pImport, IntPtr pHostMapToken, object pHandler)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000023D0 File Offset: 0x000005D0
		public void MergeEnd()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000023D0 File Offset: 0x000005D0
		public void CloseEnum(uint hEnum)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint CountEnum(uint hEnum)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000023D0 File Offset: 0x000005D0
		public void ResetEnum(uint hEnum, uint ulPos)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumTypeDefs(ref uint phEnum, uint[] rTypeDefs, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumInterfaceImpls(ref uint phEnum, uint td, uint[] rImpls, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumTypeRefs(ref uint phEnum, uint[] rTypeRefs, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint FindTypeDefByName(string szTypeDef, uint tkEnclosingClass)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000023D0 File Offset: 0x000005D0
		public Guid GetScopeProps(StringBuilder szName, uint cchName, out uint pchName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetModuleFromScope()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000023D8 File Offset: 0x000005D8
		public uint GetTypeDefProps(uint td, IntPtr szTypeDef, uint cchTypeDef, out uint pchTypeDef, IntPtr pdwTypeDefFlags)
		{
			TypeDefinition typeDefinition;
			if (!this.TryGetType(td, out typeDefinition))
			{
				Marshal.WriteInt16(szTypeDef, 0);
				pchTypeDef = 1U;
				return 0U;
			}
			ModuleMetadata.WriteString(typeDefinition.IsNested ? typeDefinition.Name : typeDefinition.FullName, szTypeDef, cchTypeDef, out pchTypeDef);
			ModuleMetadata.WriteIntPtr(pdwTypeDefFlags, (uint)typeDefinition.Attributes);
			if (typeDefinition.BaseType == null)
			{
				return 0U;
			}
			return typeDefinition.BaseType.MetadataToken.ToUInt32();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00002446 File Offset: 0x00000646
		private static void WriteIntPtr(IntPtr ptr, uint value)
		{
			if (!(ptr == IntPtr.Zero))
			{
				Marshal.WriteInt32(ptr, (int)value);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000245C File Offset: 0x0000065C
		private static void WriteString(string str, IntPtr buffer, uint bufferSize, out uint chars)
		{
			uint num = (((long)(str.Length + 1) >= (long)((ulong)bufferSize)) ? (bufferSize - 1U) : ((uint)str.Length));
			chars = num + 1U;
			int num2 = 0;
			int num3 = 0;
			while ((long)num3 < (long)((ulong)num))
			{
				Marshal.WriteInt16(buffer, num2, str.get_Chars(num3));
				num2 += 2;
				num3++;
			}
			Marshal.WriteInt16(buffer, num2, 0);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetInterfaceImplProps(uint iiImpl, out uint pClass)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetTypeRefProps(uint tr, out uint ptkResolutionScope, StringBuilder szName, uint cchName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint ResolveTypeRef(uint tr, ref Guid riid, out object ppIScope)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumMembers(ref uint phEnum, uint cl, uint[] rMembers, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumMembersWithName(ref uint phEnum, uint cl, string szName, uint[] rMembers, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumMethods(ref uint phEnum, uint cl, IntPtr rMethods, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumMethodsWithName(ref uint phEnum, uint cl, string szName, uint[] rMethods, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumFields(ref uint phEnum, uint cl, IntPtr rFields, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumFieldsWithName(ref uint phEnum, uint cl, string szName, uint[] rFields, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumParams(ref uint phEnum, uint mb, uint[] rParams, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumMemberRefs(ref uint phEnum, uint tkParent, uint[] rMemberRefs, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumMethodImpls(ref uint phEnum, uint td, uint[] rMethodBody, uint[] rMethodDecl, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumPermissionSets(ref uint phEnum, uint tk, uint dwActions, uint[] rPermission, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint FindMember(uint td, string szName, byte[] pvSigBlob, uint cbSigBlob)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint FindMethod(uint td, string szName, byte[] pvSigBlob, uint cbSigBlob)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint FindField(uint td, string szName, byte[] pvSigBlob, uint cbSigBlob)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint FindMemberRef(uint td, string szName, byte[] pvSigBlob, uint cbSigBlob)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000024B4 File Offset: 0x000006B4
		public uint GetMethodProps(uint mb, out uint pClass, IntPtr szMethod, uint cchMethod, out uint pchMethod, IntPtr pdwAttr, IntPtr ppvSigBlob, IntPtr pcbSigBlob, IntPtr pulCodeRVA)
		{
			MethodDefinition methodDefinition;
			if (!this.TryGetMethod(mb, out methodDefinition))
			{
				Marshal.WriteInt16(szMethod, 0);
				pchMethod = 1U;
				pClass = 0U;
				return 0U;
			}
			pClass = methodDefinition.DeclaringType.MetadataToken.ToUInt32();
			ModuleMetadata.WriteString(methodDefinition.Name, szMethod, cchMethod, out pchMethod);
			ModuleMetadata.WriteIntPtr(pdwAttr, (uint)methodDefinition.Attributes);
			ModuleMetadata.WriteIntPtr(pulCodeRVA, (uint)methodDefinition.RVA);
			return (uint)methodDefinition.ImplAttributes;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetMemberRefProps(uint mr, ref uint ptk, StringBuilder szMember, uint cchMember, out uint pchMember, out IntPtr ppvSigBlob)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumProperties(ref uint phEnum, uint td, IntPtr rProperties, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumEvents(ref uint phEnum, uint td, IntPtr rEvents, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetEventProps(uint ev, out uint pClass, StringBuilder szEvent, uint cchEvent, out uint pchEvent, out uint pdwEventFlags, out uint ptkEventType, out uint pmdAddOn, out uint pmdRemoveOn, out uint pmdFire, uint[] rmdOtherMethod, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumMethodSemantics(ref uint phEnum, uint mb, uint[] rEventProp, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetMethodSemantics(uint mb, uint tkEventProp)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetClassLayout(uint td, out uint pdwPackSize, IntPtr rFieldOffset, uint cMax, out uint pcFieldOffset)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetFieldMarshal(uint tk, out IntPtr ppvNativeType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetRVA(uint tk, out uint pulCodeRVA)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetPermissionSetProps(uint pm, out uint pdwAction, out IntPtr ppvPermission)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetSigFromToken(uint mdSig, out IntPtr ppvSig)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetModuleRefProps(uint mur, StringBuilder szName, uint cchName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumModuleRefs(ref uint phEnum, uint[] rModuleRefs, uint cmax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetTypeSpecFromToken(uint typespec, out IntPtr ppvSig)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetNameFromToken(uint tk)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumUnresolvedMethods(ref uint phEnum, uint[] rMethods, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetUserString(uint stk, StringBuilder szString, uint cchString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetPinvokeMap(uint tk, out uint pdwMappingFlags, StringBuilder szImportName, uint cchImportName, out uint pchImportName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumSignatures(ref uint phEnum, uint[] rSignatures, uint cmax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumTypeSpecs(ref uint phEnum, uint[] rTypeSpecs, uint cmax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumUserStrings(ref uint phEnum, uint[] rStrings, uint cmax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000023D0 File Offset: 0x000005D0
		public int GetParamForMethodIndex(uint md, uint ulParamSeq, out uint pParam)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint EnumCustomAttributes(ref uint phEnum, uint tk, uint tkType, uint[] rCustomAttributes, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetCustomAttributeProps(uint cv, out uint ptkObj, out uint ptkType, out IntPtr ppBlob)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint FindTypeRef(uint tkResolutionScope, string szName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetMemberProps(uint mb, out uint pClass, StringBuilder szMember, uint cchMember, out uint pchMember, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pulCodeRVA, out uint pdwImplFlags, out uint pdwCPlusTypeFlag, out IntPtr ppValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetFieldProps(uint mb, out uint pClass, StringBuilder szField, uint cchField, out uint pchField, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pdwCPlusTypeFlag, out IntPtr ppValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetPropertyProps(uint prop, out uint pClass, StringBuilder szProperty, uint cchProperty, out uint pchProperty, out uint pdwPropFlags, out IntPtr ppvSig, out uint pbSig, out uint pdwCPlusTypeFlag, out IntPtr ppDefaultValue, out uint pcchDefaultValue, out uint pmdSetter, out uint pmdGetter, uint[] rmdOtherMethod, uint cMax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetParamProps(uint tk, out uint pmd, out uint pulSequence, StringBuilder szName, uint cchName, out uint pchName, out uint pdwAttr, out uint pdwCPlusTypeFlag, out IntPtr ppValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetCustomAttributeByName(uint tkObj, string szName, out IntPtr ppData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000023D0 File Offset: 0x000005D0
		public bool IsValidToken(uint tk)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00002524 File Offset: 0x00000724
		public uint GetNestedClassProps(uint tdNestedClass)
		{
			TypeDefinition typeDefinition;
			if (!this.TryGetType(tdNestedClass, out typeDefinition))
			{
				return 0U;
			}
			if (!typeDefinition.IsNested)
			{
				return 0U;
			}
			return typeDefinition.DeclaringType.MetadataToken.ToUInt32();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000023D0 File Offset: 0x000005D0
		public uint GetNativeCallConvFromSig(IntPtr pvSig, uint cbSig)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000023D0 File Offset: 0x000005D0
		public int IsGlobal(uint pd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400000B RID: 11
		private readonly ModuleDefinition module;

		// Token: 0x0400000C RID: 12
		private Dictionary<uint, TypeDefinition> types;

		// Token: 0x0400000D RID: 13
		private Dictionary<uint, MethodDefinition> methods;
	}
}
