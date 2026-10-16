using System;
using System.Collections.Generic;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000074 RID: 116
	internal sealed class MetadataSystem
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		private static void InitializePrimitives()
		{
			Dictionary<string, Row<ElementType, bool>> dictionary = new Dictionary<string, Row<ElementType, bool>>(18, StringComparer.Ordinal);
			dictionary.Add("Void", new Row<ElementType, bool>(ElementType.Void, false));
			dictionary.Add("Boolean", new Row<ElementType, bool>(ElementType.Boolean, true));
			dictionary.Add("Char", new Row<ElementType, bool>(ElementType.Char, true));
			dictionary.Add("SByte", new Row<ElementType, bool>(ElementType.I1, true));
			dictionary.Add("Byte", new Row<ElementType, bool>(ElementType.U1, true));
			dictionary.Add("Int16", new Row<ElementType, bool>(ElementType.I2, true));
			dictionary.Add("UInt16", new Row<ElementType, bool>(ElementType.U2, true));
			dictionary.Add("Int32", new Row<ElementType, bool>(ElementType.I4, true));
			dictionary.Add("UInt32", new Row<ElementType, bool>(ElementType.U4, true));
			dictionary.Add("Int64", new Row<ElementType, bool>(ElementType.I8, true));
			dictionary.Add("UInt64", new Row<ElementType, bool>(ElementType.U8, true));
			dictionary.Add("Single", new Row<ElementType, bool>(ElementType.R4, true));
			dictionary.Add("Double", new Row<ElementType, bool>(ElementType.R8, true));
			dictionary.Add("String", new Row<ElementType, bool>(ElementType.String, false));
			dictionary.Add("TypedReference", new Row<ElementType, bool>(ElementType.TypedByRef, false));
			dictionary.Add("IntPtr", new Row<ElementType, bool>(ElementType.I, true));
			dictionary.Add("UIntPtr", new Row<ElementType, bool>(ElementType.U, true));
			dictionary.Add("Object", new Row<ElementType, bool>(ElementType.Object, false));
			MetadataSystem.primitive_value_types = dictionary;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000F30C File Offset: 0x0000D50C
		public static void TryProcessPrimitiveTypeReference(TypeReference type)
		{
			if (!(type.Namespace != "System"))
			{
				IMetadataScope scope = type.scope;
				Row<ElementType, bool> row;
				if (scope != null && scope.MetadataScopeType == MetadataScopeType.AssemblyNameReference && MetadataSystem.TryGetPrimitiveData(type, out row))
				{
					type.etype = row.Col1;
					type.IsValueType = row.Col2;
				}
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000F360 File Offset: 0x0000D560
		public static bool TryGetPrimitiveElementType(TypeDefinition type, out ElementType etype)
		{
			etype = ElementType.None;
			if (type.Namespace != "System")
			{
				return false;
			}
			Row<ElementType, bool> row;
			if (MetadataSystem.TryGetPrimitiveData(type, out row))
			{
				etype = row.Col1;
				return true;
			}
			return false;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000F399 File Offset: 0x0000D599
		private static bool TryGetPrimitiveData(TypeReference type, out Row<ElementType, bool> primitive_data)
		{
			if (MetadataSystem.primitive_value_types == null)
			{
				MetadataSystem.InitializePrimitives();
			}
			return MetadataSystem.primitive_value_types.TryGetValue(type.Name, ref primitive_data);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
		public void Clear()
		{
			if (this.NestedTypes != null)
			{
				this.NestedTypes = new Dictionary<uint, Collection<uint>>(0);
			}
			if (this.ReverseNestedTypes != null)
			{
				this.ReverseNestedTypes = new Dictionary<uint, uint>(0);
			}
			if (this.Interfaces != null)
			{
				this.Interfaces = new Dictionary<uint, Collection<Row<uint, MetadataToken>>>(0);
			}
			if (this.ClassLayouts != null)
			{
				this.ClassLayouts = new Dictionary<uint, Row<ushort, uint>>(0);
			}
			if (this.FieldLayouts != null)
			{
				this.FieldLayouts = new Dictionary<uint, uint>(0);
			}
			if (this.FieldRVAs != null)
			{
				this.FieldRVAs = new Dictionary<uint, uint>(0);
			}
			if (this.FieldMarshals != null)
			{
				this.FieldMarshals = new Dictionary<MetadataToken, uint>(0);
			}
			if (this.Constants != null)
			{
				this.Constants = new Dictionary<MetadataToken, Row<ElementType, uint>>(0);
			}
			if (this.Overrides != null)
			{
				this.Overrides = new Dictionary<uint, Collection<MetadataToken>>(0);
			}
			if (this.CustomAttributes != null)
			{
				this.CustomAttributes = new Dictionary<MetadataToken, Range[]>(0);
			}
			if (this.SecurityDeclarations != null)
			{
				this.SecurityDeclarations = new Dictionary<MetadataToken, Range[]>(0);
			}
			if (this.Events != null)
			{
				this.Events = new Dictionary<uint, Range>(0);
			}
			if (this.Properties != null)
			{
				this.Properties = new Dictionary<uint, Range>(0);
			}
			if (this.Semantics != null)
			{
				this.Semantics = new Dictionary<uint, Row<MethodSemanticsAttributes, MetadataToken>>(0);
			}
			if (this.PInvokes != null)
			{
				this.PInvokes = new Dictionary<uint, Row<PInvokeAttributes, uint, uint>>(0);
			}
			if (this.GenericParameters != null)
			{
				this.GenericParameters = new Dictionary<MetadataToken, Range[]>(0);
			}
			if (this.GenericConstraints != null)
			{
				this.GenericConstraints = new Dictionary<uint, Collection<MetadataToken>>(0);
			}
			this.Documents = Empty<Document>.Array;
			this.ImportScopes = Empty<ImportDebugInformation>.Array;
			if (this.LocalScopes != null)
			{
				this.LocalScopes = new Dictionary<uint, Collection<Row<uint, Range, Range, uint, uint, uint>>>(0);
			}
			if (this.StateMachineMethods != null)
			{
				this.StateMachineMethods = new Dictionary<uint, uint>(0);
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000F557 File Offset: 0x0000D757
		public AssemblyNameReference GetAssemblyNameReference(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.AssemblyReferences.Length))
			{
				return null;
			}
			return this.AssemblyReferences[(int)(rid - 1U)];
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000F576 File Offset: 0x0000D776
		public TypeDefinition GetTypeDefinition(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.Types.Length))
			{
				return null;
			}
			return this.Types[(int)(rid - 1U)];
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000F595 File Offset: 0x0000D795
		public void AddTypeDefinition(TypeDefinition type)
		{
			this.Types[(int)(type.token.RID - 1U)] = type;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000F5AC File Offset: 0x0000D7AC
		public TypeReference GetTypeReference(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.TypeReferences.Length))
			{
				return null;
			}
			return this.TypeReferences[(int)(rid - 1U)];
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000F5CB File Offset: 0x0000D7CB
		public void AddTypeReference(TypeReference type)
		{
			this.TypeReferences[(int)(type.token.RID - 1U)] = type;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000F5E2 File Offset: 0x0000D7E2
		public FieldDefinition GetFieldDefinition(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.Fields.Length))
			{
				return null;
			}
			return this.Fields[(int)(rid - 1U)];
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000F601 File Offset: 0x0000D801
		public void AddFieldDefinition(FieldDefinition field)
		{
			this.Fields[(int)(field.token.RID - 1U)] = field;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000F618 File Offset: 0x0000D818
		public MethodDefinition GetMethodDefinition(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.Methods.Length))
			{
				return null;
			}
			return this.Methods[(int)(rid - 1U)];
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000F637 File Offset: 0x0000D837
		public void AddMethodDefinition(MethodDefinition method)
		{
			this.Methods[(int)(method.token.RID - 1U)] = method;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000F64E File Offset: 0x0000D84E
		public MemberReference GetMemberReference(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.MemberReferences.Length))
			{
				return null;
			}
			return this.MemberReferences[(int)(rid - 1U)];
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000F66D File Offset: 0x0000D86D
		public void AddMemberReference(MemberReference member)
		{
			this.MemberReferences[(int)(member.token.RID - 1U)] = member;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000F684 File Offset: 0x0000D884
		public bool TryGetNestedTypeMapping(TypeDefinition type, out Collection<uint> mapping)
		{
			return this.NestedTypes.TryGetValue(type.token.RID, ref mapping);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000F69D File Offset: 0x0000D89D
		public void SetNestedTypeMapping(uint type_rid, Collection<uint> mapping)
		{
			this.NestedTypes[type_rid] = mapping;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000F6AC File Offset: 0x0000D8AC
		public void RemoveNestedTypeMapping(TypeDefinition type)
		{
			this.NestedTypes.Remove(type.token.RID);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000F6C5 File Offset: 0x0000D8C5
		public bool TryGetReverseNestedTypeMapping(TypeDefinition type, out uint declaring)
		{
			return this.ReverseNestedTypes.TryGetValue(type.token.RID, ref declaring);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000F6DE File Offset: 0x0000D8DE
		public void SetReverseNestedTypeMapping(uint nested, uint declaring)
		{
			this.ReverseNestedTypes[nested] = declaring;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000F6ED File Offset: 0x0000D8ED
		public void RemoveReverseNestedTypeMapping(TypeDefinition type)
		{
			this.ReverseNestedTypes.Remove(type.token.RID);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000F706 File Offset: 0x0000D906
		public bool TryGetInterfaceMapping(TypeDefinition type, out Collection<Row<uint, MetadataToken>> mapping)
		{
			return this.Interfaces.TryGetValue(type.token.RID, ref mapping);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000F71F File Offset: 0x0000D91F
		public void SetInterfaceMapping(uint type_rid, Collection<Row<uint, MetadataToken>> mapping)
		{
			this.Interfaces[type_rid] = mapping;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000F72E File Offset: 0x0000D92E
		public void RemoveInterfaceMapping(TypeDefinition type)
		{
			this.Interfaces.Remove(type.token.RID);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000F747 File Offset: 0x0000D947
		public void AddPropertiesRange(uint type_rid, Range range)
		{
			this.Properties.Add(type_rid, range);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000F756 File Offset: 0x0000D956
		public bool TryGetPropertiesRange(TypeDefinition type, out Range range)
		{
			return this.Properties.TryGetValue(type.token.RID, ref range);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000F76F File Offset: 0x0000D96F
		public void RemovePropertiesRange(TypeDefinition type)
		{
			this.Properties.Remove(type.token.RID);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000F788 File Offset: 0x0000D988
		public void AddEventsRange(uint type_rid, Range range)
		{
			this.Events.Add(type_rid, range);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000F797 File Offset: 0x0000D997
		public bool TryGetEventsRange(TypeDefinition type, out Range range)
		{
			return this.Events.TryGetValue(type.token.RID, ref range);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000F7B0 File Offset: 0x0000D9B0
		public void RemoveEventsRange(TypeDefinition type)
		{
			this.Events.Remove(type.token.RID);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000F7C9 File Offset: 0x0000D9C9
		public bool TryGetGenericParameterRanges(IGenericParameterProvider owner, out Range[] ranges)
		{
			return this.GenericParameters.TryGetValue(owner.MetadataToken, ref ranges);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000F7DD File Offset: 0x0000D9DD
		public void RemoveGenericParameterRange(IGenericParameterProvider owner)
		{
			this.GenericParameters.Remove(owner.MetadataToken);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000F7F1 File Offset: 0x0000D9F1
		public bool TryGetCustomAttributeRanges(ICustomAttributeProvider owner, out Range[] ranges)
		{
			return this.CustomAttributes.TryGetValue(owner.MetadataToken, ref ranges);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000F805 File Offset: 0x0000DA05
		public void RemoveCustomAttributeRange(ICustomAttributeProvider owner)
		{
			this.CustomAttributes.Remove(owner.MetadataToken);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000F819 File Offset: 0x0000DA19
		public bool TryGetSecurityDeclarationRanges(ISecurityDeclarationProvider owner, out Range[] ranges)
		{
			return this.SecurityDeclarations.TryGetValue(owner.MetadataToken, ref ranges);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000F82D File Offset: 0x0000DA2D
		public void RemoveSecurityDeclarationRange(ISecurityDeclarationProvider owner)
		{
			this.SecurityDeclarations.Remove(owner.MetadataToken);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000F841 File Offset: 0x0000DA41
		public bool TryGetGenericConstraintMapping(GenericParameter generic_parameter, out Collection<MetadataToken> mapping)
		{
			return this.GenericConstraints.TryGetValue(generic_parameter.token.RID, ref mapping);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000F85A File Offset: 0x0000DA5A
		public void SetGenericConstraintMapping(uint gp_rid, Collection<MetadataToken> mapping)
		{
			this.GenericConstraints[gp_rid] = mapping;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000F869 File Offset: 0x0000DA69
		public void RemoveGenericConstraintMapping(GenericParameter generic_parameter)
		{
			this.GenericConstraints.Remove(generic_parameter.token.RID);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000F882 File Offset: 0x0000DA82
		public bool TryGetOverrideMapping(MethodDefinition method, out Collection<MetadataToken> mapping)
		{
			return this.Overrides.TryGetValue(method.token.RID, ref mapping);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000F89B File Offset: 0x0000DA9B
		public void SetOverrideMapping(uint rid, Collection<MetadataToken> mapping)
		{
			this.Overrides[rid] = mapping;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000F8AA File Offset: 0x0000DAAA
		public void RemoveOverrideMapping(MethodDefinition method)
		{
			this.Overrides.Remove(method.token.RID);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000F8C3 File Offset: 0x0000DAC3
		public Document GetDocument(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.Documents.Length))
			{
				return null;
			}
			return this.Documents[(int)(rid - 1U)];
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000F8E4 File Offset: 0x0000DAE4
		public bool TryGetLocalScopes(MethodDefinition method, out Collection<Row<uint, Range, Range, uint, uint, uint>> scopes)
		{
			return this.LocalScopes.TryGetValue(method.MetadataToken.RID, ref scopes);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000F90B File Offset: 0x0000DB0B
		public void SetLocalScopes(uint method_rid, Collection<Row<uint, Range, Range, uint, uint, uint>> records)
		{
			this.LocalScopes[method_rid] = records;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000F91A File Offset: 0x0000DB1A
		public ImportDebugInformation GetImportScope(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.ImportScopes.Length))
			{
				return null;
			}
			return this.ImportScopes[(int)(rid - 1U)];
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000F93C File Offset: 0x0000DB3C
		public bool TryGetStateMachineKickOffMethod(MethodDefinition method, out uint rid)
		{
			return this.StateMachineMethods.TryGetValue(method.MetadataToken.RID, ref rid);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000F963 File Offset: 0x0000DB63
		public TypeDefinition GetFieldDeclaringType(uint field_rid)
		{
			return MetadataSystem.BinaryRangeSearch(this.Types, field_rid, true);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000F972 File Offset: 0x0000DB72
		public TypeDefinition GetMethodDeclaringType(uint method_rid)
		{
			return MetadataSystem.BinaryRangeSearch(this.Types, method_rid, false);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000F984 File Offset: 0x0000DB84
		private static TypeDefinition BinaryRangeSearch(TypeDefinition[] types, uint rid, bool field)
		{
			int i = 0;
			int num = types.Length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i) / 2;
				TypeDefinition typeDefinition = types[num2];
				Range range = (field ? typeDefinition.fields_range : typeDefinition.methods_range);
				if (rid < range.Start)
				{
					num = num2 - 1;
				}
				else
				{
					if (rid < range.Start + range.Length)
					{
						return typeDefinition;
					}
					i = num2 + 1;
				}
			}
			return null;
		}

		// Token: 0x040000FD RID: 253
		internal AssemblyNameReference[] AssemblyReferences;

		// Token: 0x040000FE RID: 254
		internal ModuleReference[] ModuleReferences;

		// Token: 0x040000FF RID: 255
		internal TypeDefinition[] Types;

		// Token: 0x04000100 RID: 256
		internal TypeReference[] TypeReferences;

		// Token: 0x04000101 RID: 257
		internal FieldDefinition[] Fields;

		// Token: 0x04000102 RID: 258
		internal MethodDefinition[] Methods;

		// Token: 0x04000103 RID: 259
		internal MemberReference[] MemberReferences;

		// Token: 0x04000104 RID: 260
		internal Dictionary<uint, Collection<uint>> NestedTypes;

		// Token: 0x04000105 RID: 261
		internal Dictionary<uint, uint> ReverseNestedTypes;

		// Token: 0x04000106 RID: 262
		internal Dictionary<uint, Collection<Row<uint, MetadataToken>>> Interfaces;

		// Token: 0x04000107 RID: 263
		internal Dictionary<uint, Row<ushort, uint>> ClassLayouts;

		// Token: 0x04000108 RID: 264
		internal Dictionary<uint, uint> FieldLayouts;

		// Token: 0x04000109 RID: 265
		internal Dictionary<uint, uint> FieldRVAs;

		// Token: 0x0400010A RID: 266
		internal Dictionary<MetadataToken, uint> FieldMarshals;

		// Token: 0x0400010B RID: 267
		internal Dictionary<MetadataToken, Row<ElementType, uint>> Constants;

		// Token: 0x0400010C RID: 268
		internal Dictionary<uint, Collection<MetadataToken>> Overrides;

		// Token: 0x0400010D RID: 269
		internal Dictionary<MetadataToken, Range[]> CustomAttributes;

		// Token: 0x0400010E RID: 270
		internal Dictionary<MetadataToken, Range[]> SecurityDeclarations;

		// Token: 0x0400010F RID: 271
		internal Dictionary<uint, Range> Events;

		// Token: 0x04000110 RID: 272
		internal Dictionary<uint, Range> Properties;

		// Token: 0x04000111 RID: 273
		internal Dictionary<uint, Row<MethodSemanticsAttributes, MetadataToken>> Semantics;

		// Token: 0x04000112 RID: 274
		internal Dictionary<uint, Row<PInvokeAttributes, uint, uint>> PInvokes;

		// Token: 0x04000113 RID: 275
		internal Dictionary<MetadataToken, Range[]> GenericParameters;

		// Token: 0x04000114 RID: 276
		internal Dictionary<uint, Collection<MetadataToken>> GenericConstraints;

		// Token: 0x04000115 RID: 277
		internal Document[] Documents;

		// Token: 0x04000116 RID: 278
		internal Dictionary<uint, Collection<Row<uint, Range, Range, uint, uint, uint>>> LocalScopes;

		// Token: 0x04000117 RID: 279
		internal ImportDebugInformation[] ImportScopes;

		// Token: 0x04000118 RID: 280
		internal Dictionary<uint, uint> StateMachineMethods;

		// Token: 0x04000119 RID: 281
		internal Dictionary<MetadataToken, Row<Guid, uint, uint>[]> CustomDebugInformations;

		// Token: 0x0400011A RID: 282
		private static Dictionary<string, Row<ElementType, bool>> primitive_value_types;
	}
}
