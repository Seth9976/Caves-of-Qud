using System;
using System.Collections.Generic;
using System.Threading;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000235 RID: 565
	internal sealed class MetadataSystem
	{
		// Token: 0x06000C4D RID: 3149 RVA: 0x0002A1DC File Offset: 0x000283DC
		private static void InitializePrimitives()
		{
			Dictionary<string, Row<ElementType, bool>> dictionary = new Dictionary<string, Row<ElementType, bool>>(18, StringComparer.Ordinal)
			{
				{
					"Void",
					new Row<ElementType, bool>(ElementType.Void, false)
				},
				{
					"Boolean",
					new Row<ElementType, bool>(ElementType.Boolean, true)
				},
				{
					"Char",
					new Row<ElementType, bool>(ElementType.Char, true)
				},
				{
					"SByte",
					new Row<ElementType, bool>(ElementType.I1, true)
				},
				{
					"Byte",
					new Row<ElementType, bool>(ElementType.U1, true)
				},
				{
					"Int16",
					new Row<ElementType, bool>(ElementType.I2, true)
				},
				{
					"UInt16",
					new Row<ElementType, bool>(ElementType.U2, true)
				},
				{
					"Int32",
					new Row<ElementType, bool>(ElementType.I4, true)
				},
				{
					"UInt32",
					new Row<ElementType, bool>(ElementType.U4, true)
				},
				{
					"Int64",
					new Row<ElementType, bool>(ElementType.I8, true)
				},
				{
					"UInt64",
					new Row<ElementType, bool>(ElementType.U8, true)
				},
				{
					"Single",
					new Row<ElementType, bool>(ElementType.R4, true)
				},
				{
					"Double",
					new Row<ElementType, bool>(ElementType.R8, true)
				},
				{
					"String",
					new Row<ElementType, bool>(ElementType.String, false)
				},
				{
					"TypedReference",
					new Row<ElementType, bool>(ElementType.TypedByRef, false)
				},
				{
					"IntPtr",
					new Row<ElementType, bool>(ElementType.I, true)
				},
				{
					"UIntPtr",
					new Row<ElementType, bool>(ElementType.U, true)
				},
				{
					"Object",
					new Row<ElementType, bool>(ElementType.Object, false)
				}
			};
			Interlocked.CompareExchange<Dictionary<string, Row<ElementType, bool>>>(ref MetadataSystem.primitive_value_types, dictionary, null);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0002A354 File Offset: 0x00028554
		public static void TryProcessPrimitiveTypeReference(TypeReference type)
		{
			if (type.Namespace != "System")
			{
				return;
			}
			IMetadataScope scope = type.scope;
			if (scope == null || scope.MetadataScopeType != MetadataScopeType.AssemblyNameReference)
			{
				return;
			}
			Row<ElementType, bool> row;
			if (!MetadataSystem.TryGetPrimitiveData(type, out row))
			{
				return;
			}
			type.etype = row.Col1;
			type.IsValueType = row.Col2;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002A3AC File Offset: 0x000285AC
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

		// Token: 0x06000C50 RID: 3152 RVA: 0x0002A3E5 File Offset: 0x000285E5
		private static bool TryGetPrimitiveData(TypeReference type, out Row<ElementType, bool> primitive_data)
		{
			if (MetadataSystem.primitive_value_types == null)
			{
				MetadataSystem.InitializePrimitives();
			}
			return MetadataSystem.primitive_value_types.TryGetValue(type.Name, out primitive_data);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002A404 File Offset: 0x00028604
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
				this.GenericConstraints = new Dictionary<uint, Collection<Row<uint, MetadataToken>>>(0);
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

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002A5A3 File Offset: 0x000287A3
		public AssemblyNameReference GetAssemblyNameReference(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.AssemblyReferences.Length))
			{
				return null;
			}
			return this.AssemblyReferences[(int)(rid - 1U)];
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002A5C2 File Offset: 0x000287C2
		public TypeDefinition GetTypeDefinition(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.Types.Length))
			{
				return null;
			}
			return this.Types[(int)(rid - 1U)];
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002A5E1 File Offset: 0x000287E1
		public void AddTypeDefinition(TypeDefinition type)
		{
			this.Types[(int)(type.token.RID - 1U)] = type;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002A5F8 File Offset: 0x000287F8
		public TypeReference GetTypeReference(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.TypeReferences.Length))
			{
				return null;
			}
			return this.TypeReferences[(int)(rid - 1U)];
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0002A617 File Offset: 0x00028817
		public void AddTypeReference(TypeReference type)
		{
			this.TypeReferences[(int)(type.token.RID - 1U)] = type;
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002A62E File Offset: 0x0002882E
		public FieldDefinition GetFieldDefinition(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.Fields.Length))
			{
				return null;
			}
			return this.Fields[(int)(rid - 1U)];
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002A64D File Offset: 0x0002884D
		public void AddFieldDefinition(FieldDefinition field)
		{
			this.Fields[(int)(field.token.RID - 1U)] = field;
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002A664 File Offset: 0x00028864
		public MethodDefinition GetMethodDefinition(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.Methods.Length))
			{
				return null;
			}
			return this.Methods[(int)(rid - 1U)];
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002A683 File Offset: 0x00028883
		public void AddMethodDefinition(MethodDefinition method)
		{
			this.Methods[(int)(method.token.RID - 1U)] = method;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002A69A File Offset: 0x0002889A
		public MemberReference GetMemberReference(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.MemberReferences.Length))
			{
				return null;
			}
			return this.MemberReferences[(int)(rid - 1U)];
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002A6B9 File Offset: 0x000288B9
		public void AddMemberReference(MemberReference member)
		{
			this.MemberReferences[(int)(member.token.RID - 1U)] = member;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002A6D0 File Offset: 0x000288D0
		public bool TryGetNestedTypeMapping(TypeDefinition type, out Collection<uint> mapping)
		{
			return this.NestedTypes.TryGetValue(type.token.RID, out mapping);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0002A6E9 File Offset: 0x000288E9
		public void SetNestedTypeMapping(uint type_rid, Collection<uint> mapping)
		{
			this.NestedTypes[type_rid] = mapping;
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0002A6F8 File Offset: 0x000288F8
		public void RemoveNestedTypeMapping(TypeDefinition type)
		{
			this.NestedTypes.Remove(type.token.RID);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0002A711 File Offset: 0x00028911
		public bool TryGetReverseNestedTypeMapping(TypeDefinition type, out uint declaring)
		{
			return this.ReverseNestedTypes.TryGetValue(type.token.RID, out declaring);
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002A72A File Offset: 0x0002892A
		public void SetReverseNestedTypeMapping(uint nested, uint declaring)
		{
			this.ReverseNestedTypes[nested] = declaring;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002A739 File Offset: 0x00028939
		public void RemoveReverseNestedTypeMapping(TypeDefinition type)
		{
			this.ReverseNestedTypes.Remove(type.token.RID);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002A752 File Offset: 0x00028952
		public bool TryGetInterfaceMapping(TypeDefinition type, out Collection<Row<uint, MetadataToken>> mapping)
		{
			return this.Interfaces.TryGetValue(type.token.RID, out mapping);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002A76B File Offset: 0x0002896B
		public void SetInterfaceMapping(uint type_rid, Collection<Row<uint, MetadataToken>> mapping)
		{
			this.Interfaces[type_rid] = mapping;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002A77A File Offset: 0x0002897A
		public void RemoveInterfaceMapping(TypeDefinition type)
		{
			this.Interfaces.Remove(type.token.RID);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002A793 File Offset: 0x00028993
		public void AddPropertiesRange(uint type_rid, Range range)
		{
			this.Properties.Add(type_rid, range);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002A7A2 File Offset: 0x000289A2
		public bool TryGetPropertiesRange(TypeDefinition type, out Range range)
		{
			return this.Properties.TryGetValue(type.token.RID, out range);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002A7BB File Offset: 0x000289BB
		public void RemovePropertiesRange(TypeDefinition type)
		{
			this.Properties.Remove(type.token.RID);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002A7D4 File Offset: 0x000289D4
		public void AddEventsRange(uint type_rid, Range range)
		{
			this.Events.Add(type_rid, range);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002A7E3 File Offset: 0x000289E3
		public bool TryGetEventsRange(TypeDefinition type, out Range range)
		{
			return this.Events.TryGetValue(type.token.RID, out range);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002A7FC File Offset: 0x000289FC
		public void RemoveEventsRange(TypeDefinition type)
		{
			this.Events.Remove(type.token.RID);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002A815 File Offset: 0x00028A15
		public bool TryGetGenericParameterRanges(IGenericParameterProvider owner, out Range[] ranges)
		{
			return this.GenericParameters.TryGetValue(owner.MetadataToken, out ranges);
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002A829 File Offset: 0x00028A29
		public void RemoveGenericParameterRange(IGenericParameterProvider owner)
		{
			this.GenericParameters.Remove(owner.MetadataToken);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002A83D File Offset: 0x00028A3D
		public bool TryGetCustomAttributeRanges(ICustomAttributeProvider owner, out Range[] ranges)
		{
			return this.CustomAttributes.TryGetValue(owner.MetadataToken, out ranges);
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0002A851 File Offset: 0x00028A51
		public void RemoveCustomAttributeRange(ICustomAttributeProvider owner)
		{
			this.CustomAttributes.Remove(owner.MetadataToken);
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0002A865 File Offset: 0x00028A65
		public bool TryGetSecurityDeclarationRanges(ISecurityDeclarationProvider owner, out Range[] ranges)
		{
			return this.SecurityDeclarations.TryGetValue(owner.MetadataToken, out ranges);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0002A879 File Offset: 0x00028A79
		public void RemoveSecurityDeclarationRange(ISecurityDeclarationProvider owner)
		{
			this.SecurityDeclarations.Remove(owner.MetadataToken);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0002A88D File Offset: 0x00028A8D
		public bool TryGetGenericConstraintMapping(GenericParameter generic_parameter, out Collection<Row<uint, MetadataToken>> mapping)
		{
			return this.GenericConstraints.TryGetValue(generic_parameter.token.RID, out mapping);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0002A8A6 File Offset: 0x00028AA6
		public void SetGenericConstraintMapping(uint gp_rid, Collection<Row<uint, MetadataToken>> mapping)
		{
			this.GenericConstraints[gp_rid] = mapping;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0002A8B5 File Offset: 0x00028AB5
		public void RemoveGenericConstraintMapping(GenericParameter generic_parameter)
		{
			this.GenericConstraints.Remove(generic_parameter.token.RID);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0002A8CE File Offset: 0x00028ACE
		public bool TryGetOverrideMapping(MethodDefinition method, out Collection<MetadataToken> mapping)
		{
			return this.Overrides.TryGetValue(method.token.RID, out mapping);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0002A8E7 File Offset: 0x00028AE7
		public void SetOverrideMapping(uint rid, Collection<MetadataToken> mapping)
		{
			this.Overrides[rid] = mapping;
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0002A8F6 File Offset: 0x00028AF6
		public void RemoveOverrideMapping(MethodDefinition method)
		{
			this.Overrides.Remove(method.token.RID);
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0002A90F File Offset: 0x00028B0F
		public Document GetDocument(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.Documents.Length))
			{
				return null;
			}
			return this.Documents[(int)(rid - 1U)];
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0002A930 File Offset: 0x00028B30
		public bool TryGetLocalScopes(MethodDefinition method, out Collection<Row<uint, Range, Range, uint, uint, uint>> scopes)
		{
			return this.LocalScopes.TryGetValue(method.MetadataToken.RID, out scopes);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0002A957 File Offset: 0x00028B57
		public void SetLocalScopes(uint method_rid, Collection<Row<uint, Range, Range, uint, uint, uint>> records)
		{
			this.LocalScopes[method_rid] = records;
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0002A966 File Offset: 0x00028B66
		public ImportDebugInformation GetImportScope(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.ImportScopes.Length))
			{
				return null;
			}
			return this.ImportScopes[(int)(rid - 1U)];
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0002A988 File Offset: 0x00028B88
		public bool TryGetStateMachineKickOffMethod(MethodDefinition method, out uint rid)
		{
			return this.StateMachineMethods.TryGetValue(method.MetadataToken.RID, out rid);
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002A9AF File Offset: 0x00028BAF
		public TypeDefinition GetFieldDeclaringType(uint field_rid)
		{
			return MetadataSystem.BinaryRangeSearch(this.Types, field_rid, true);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0002A9BE File Offset: 0x00028BBE
		public TypeDefinition GetMethodDeclaringType(uint method_rid)
		{
			return MetadataSystem.BinaryRangeSearch(this.Types, method_rid, false);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0002A9D0 File Offset: 0x00028BD0
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

		// Token: 0x04000371 RID: 881
		internal AssemblyNameReference[] AssemblyReferences;

		// Token: 0x04000372 RID: 882
		internal ModuleReference[] ModuleReferences;

		// Token: 0x04000373 RID: 883
		internal TypeDefinition[] Types;

		// Token: 0x04000374 RID: 884
		internal TypeReference[] TypeReferences;

		// Token: 0x04000375 RID: 885
		internal FieldDefinition[] Fields;

		// Token: 0x04000376 RID: 886
		internal MethodDefinition[] Methods;

		// Token: 0x04000377 RID: 887
		internal MemberReference[] MemberReferences;

		// Token: 0x04000378 RID: 888
		internal Dictionary<uint, Collection<uint>> NestedTypes;

		// Token: 0x04000379 RID: 889
		internal Dictionary<uint, uint> ReverseNestedTypes;

		// Token: 0x0400037A RID: 890
		internal Dictionary<uint, Collection<Row<uint, MetadataToken>>> Interfaces;

		// Token: 0x0400037B RID: 891
		internal Dictionary<uint, Row<ushort, uint>> ClassLayouts;

		// Token: 0x0400037C RID: 892
		internal Dictionary<uint, uint> FieldLayouts;

		// Token: 0x0400037D RID: 893
		internal Dictionary<uint, uint> FieldRVAs;

		// Token: 0x0400037E RID: 894
		internal Dictionary<MetadataToken, uint> FieldMarshals;

		// Token: 0x0400037F RID: 895
		internal Dictionary<MetadataToken, Row<ElementType, uint>> Constants;

		// Token: 0x04000380 RID: 896
		internal Dictionary<uint, Collection<MetadataToken>> Overrides;

		// Token: 0x04000381 RID: 897
		internal Dictionary<MetadataToken, Range[]> CustomAttributes;

		// Token: 0x04000382 RID: 898
		internal Dictionary<MetadataToken, Range[]> SecurityDeclarations;

		// Token: 0x04000383 RID: 899
		internal Dictionary<uint, Range> Events;

		// Token: 0x04000384 RID: 900
		internal Dictionary<uint, Range> Properties;

		// Token: 0x04000385 RID: 901
		internal Dictionary<uint, Row<MethodSemanticsAttributes, MetadataToken>> Semantics;

		// Token: 0x04000386 RID: 902
		internal Dictionary<uint, Row<PInvokeAttributes, uint, uint>> PInvokes;

		// Token: 0x04000387 RID: 903
		internal Dictionary<MetadataToken, Range[]> GenericParameters;

		// Token: 0x04000388 RID: 904
		internal Dictionary<uint, Collection<Row<uint, MetadataToken>>> GenericConstraints;

		// Token: 0x04000389 RID: 905
		internal Document[] Documents;

		// Token: 0x0400038A RID: 906
		internal Dictionary<uint, Collection<Row<uint, Range, Range, uint, uint, uint>>> LocalScopes;

		// Token: 0x0400038B RID: 907
		internal ImportDebugInformation[] ImportScopes;

		// Token: 0x0400038C RID: 908
		internal Dictionary<uint, uint> StateMachineMethods;

		// Token: 0x0400038D RID: 909
		internal Dictionary<MetadataToken, Row<Guid, uint, uint>[]> CustomDebugInformations;

		// Token: 0x0400038E RID: 910
		private static Dictionary<string, Row<ElementType, bool>> primitive_value_types;
	}
}
