using System;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200027A RID: 634
	public abstract class TypeSystem
	{
		// Token: 0x06000FD2 RID: 4050 RVA: 0x00030CD2 File Offset: 0x0002EED2
		private TypeSystem(ModuleDefinition module)
		{
			this.module = module;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00030CE1 File Offset: 0x0002EEE1
		internal static TypeSystem CreateTypeSystem(ModuleDefinition module)
		{
			if (module.IsCoreLibrary())
			{
				return new TypeSystem.CoreTypeSystem(module);
			}
			return new TypeSystem.CommonTypeSystem(module);
		}

		// Token: 0x06000FD4 RID: 4052
		internal abstract TypeReference LookupType(string @namespace, string name);

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00030CF8 File Offset: 0x0002EEF8
		private TypeReference LookupSystemType(ref TypeReference reference, string name, ElementType element_type)
		{
			object syncRoot = this.module.SyncRoot;
			TypeReference typeReference;
			lock (syncRoot)
			{
				if (reference != null)
				{
					typeReference = reference;
				}
				else
				{
					TypeReference typeReference2 = this.LookupType("System", name);
					typeReference2.etype = element_type;
					TypeReference typeReference3;
					reference = (typeReference3 = typeReference2);
					typeReference = typeReference3;
				}
			}
			return typeReference;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00030D60 File Offset: 0x0002EF60
		private TypeReference LookupSystemValueType(ref TypeReference typeRef, string name, ElementType element_type)
		{
			object syncRoot = this.module.SyncRoot;
			TypeReference typeReference;
			lock (syncRoot)
			{
				if (typeRef != null)
				{
					typeReference = typeRef;
				}
				else
				{
					TypeReference typeReference2 = this.LookupType("System", name);
					typeReference2.etype = element_type;
					typeReference2.KnownValueType();
					TypeReference typeReference3;
					typeRef = (typeReference3 = typeReference2);
					typeReference = typeReference3;
				}
			}
			return typeReference;
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00030DCC File Offset: 0x0002EFCC
		[Obsolete("Use CoreLibrary")]
		public IMetadataScope Corlib
		{
			get
			{
				return this.CoreLibrary;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00030DD4 File Offset: 0x0002EFD4
		public IMetadataScope CoreLibrary
		{
			get
			{
				TypeSystem.CommonTypeSystem commonTypeSystem = this as TypeSystem.CommonTypeSystem;
				if (commonTypeSystem == null)
				{
					return this.module;
				}
				return commonTypeSystem.GetCoreLibraryReference();
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00030DF8 File Offset: 0x0002EFF8
		public TypeReference Object
		{
			get
			{
				return this.type_object ?? this.LookupSystemType(ref this.type_object, "Object", ElementType.Object);
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00030E17 File Offset: 0x0002F017
		public TypeReference Void
		{
			get
			{
				return this.type_void ?? this.LookupSystemType(ref this.type_void, "Void", ElementType.Void);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00030E35 File Offset: 0x0002F035
		public TypeReference Boolean
		{
			get
			{
				return this.type_bool ?? this.LookupSystemValueType(ref this.type_bool, "Boolean", ElementType.Boolean);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00030E53 File Offset: 0x0002F053
		public TypeReference Char
		{
			get
			{
				return this.type_char ?? this.LookupSystemValueType(ref this.type_char, "Char", ElementType.Char);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00030E71 File Offset: 0x0002F071
		public TypeReference SByte
		{
			get
			{
				return this.type_sbyte ?? this.LookupSystemValueType(ref this.type_sbyte, "SByte", ElementType.I1);
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00030E8F File Offset: 0x0002F08F
		public TypeReference Byte
		{
			get
			{
				return this.type_byte ?? this.LookupSystemValueType(ref this.type_byte, "Byte", ElementType.U1);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00030EAD File Offset: 0x0002F0AD
		public TypeReference Int16
		{
			get
			{
				return this.type_int16 ?? this.LookupSystemValueType(ref this.type_int16, "Int16", ElementType.I2);
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00030ECB File Offset: 0x0002F0CB
		public TypeReference UInt16
		{
			get
			{
				return this.type_uint16 ?? this.LookupSystemValueType(ref this.type_uint16, "UInt16", ElementType.U2);
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00030EE9 File Offset: 0x0002F0E9
		public TypeReference Int32
		{
			get
			{
				return this.type_int32 ?? this.LookupSystemValueType(ref this.type_int32, "Int32", ElementType.I4);
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x00030F07 File Offset: 0x0002F107
		public TypeReference UInt32
		{
			get
			{
				return this.type_uint32 ?? this.LookupSystemValueType(ref this.type_uint32, "UInt32", ElementType.U4);
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x00030F26 File Offset: 0x0002F126
		public TypeReference Int64
		{
			get
			{
				return this.type_int64 ?? this.LookupSystemValueType(ref this.type_int64, "Int64", ElementType.I8);
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x00030F45 File Offset: 0x0002F145
		public TypeReference UInt64
		{
			get
			{
				return this.type_uint64 ?? this.LookupSystemValueType(ref this.type_uint64, "UInt64", ElementType.U8);
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x00030F64 File Offset: 0x0002F164
		public TypeReference Single
		{
			get
			{
				return this.type_single ?? this.LookupSystemValueType(ref this.type_single, "Single", ElementType.R4);
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x00030F83 File Offset: 0x0002F183
		public TypeReference Double
		{
			get
			{
				return this.type_double ?? this.LookupSystemValueType(ref this.type_double, "Double", ElementType.R8);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00030FA2 File Offset: 0x0002F1A2
		public TypeReference IntPtr
		{
			get
			{
				return this.type_intptr ?? this.LookupSystemValueType(ref this.type_intptr, "IntPtr", ElementType.I);
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x00030FC1 File Offset: 0x0002F1C1
		public TypeReference UIntPtr
		{
			get
			{
				return this.type_uintptr ?? this.LookupSystemValueType(ref this.type_uintptr, "UIntPtr", ElementType.U);
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00030FE0 File Offset: 0x0002F1E0
		public TypeReference String
		{
			get
			{
				return this.type_string ?? this.LookupSystemType(ref this.type_string, "String", ElementType.String);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x00030FFF File Offset: 0x0002F1FF
		public TypeReference TypedReference
		{
			get
			{
				return this.type_typedref ?? this.LookupSystemValueType(ref this.type_typedref, "TypedReference", ElementType.TypedByRef);
			}
		}

		// Token: 0x04000582 RID: 1410
		private readonly ModuleDefinition module;

		// Token: 0x04000583 RID: 1411
		private TypeReference type_object;

		// Token: 0x04000584 RID: 1412
		private TypeReference type_void;

		// Token: 0x04000585 RID: 1413
		private TypeReference type_bool;

		// Token: 0x04000586 RID: 1414
		private TypeReference type_char;

		// Token: 0x04000587 RID: 1415
		private TypeReference type_sbyte;

		// Token: 0x04000588 RID: 1416
		private TypeReference type_byte;

		// Token: 0x04000589 RID: 1417
		private TypeReference type_int16;

		// Token: 0x0400058A RID: 1418
		private TypeReference type_uint16;

		// Token: 0x0400058B RID: 1419
		private TypeReference type_int32;

		// Token: 0x0400058C RID: 1420
		private TypeReference type_uint32;

		// Token: 0x0400058D RID: 1421
		private TypeReference type_int64;

		// Token: 0x0400058E RID: 1422
		private TypeReference type_uint64;

		// Token: 0x0400058F RID: 1423
		private TypeReference type_single;

		// Token: 0x04000590 RID: 1424
		private TypeReference type_double;

		// Token: 0x04000591 RID: 1425
		private TypeReference type_intptr;

		// Token: 0x04000592 RID: 1426
		private TypeReference type_uintptr;

		// Token: 0x04000593 RID: 1427
		private TypeReference type_string;

		// Token: 0x04000594 RID: 1428
		private TypeReference type_typedref;

		// Token: 0x0200027B RID: 635
		private sealed class CoreTypeSystem : TypeSystem
		{
			// Token: 0x06000FEB RID: 4075 RVA: 0x0003101E File Offset: 0x0002F21E
			public CoreTypeSystem(ModuleDefinition module)
				: base(module)
			{
			}

			// Token: 0x06000FEC RID: 4076 RVA: 0x00031028 File Offset: 0x0002F228
			internal override TypeReference LookupType(string @namespace, string name)
			{
				TypeReference typeReference = this.LookupTypeDefinition(@namespace, name) ?? this.LookupTypeForwarded(@namespace, name);
				if (typeReference != null)
				{
					return typeReference;
				}
				throw new NotSupportedException();
			}

			// Token: 0x06000FED RID: 4077 RVA: 0x00031054 File Offset: 0x0002F254
			private TypeReference LookupTypeDefinition(string @namespace, string name)
			{
				if (this.module.MetadataSystem.Types == null)
				{
					TypeSystem.CoreTypeSystem.Initialize(this.module.Types);
				}
				return this.module.Read<Row<string, string>, TypeDefinition>(new Row<string, string>(@namespace, name), delegate(Row<string, string> row, MetadataReader reader)
				{
					TypeDefinition[] types = reader.metadata.Types;
					for (int i = 0; i < types.Length; i++)
					{
						if (types[i] == null)
						{
							types[i] = reader.GetTypeDefinition((uint)(i + 1));
						}
						TypeDefinition typeDefinition = types[i];
						if (typeDefinition.Name == row.Col2 && typeDefinition.Namespace == row.Col1)
						{
							return typeDefinition;
						}
					}
					return null;
				});
			}

			// Token: 0x06000FEE RID: 4078 RVA: 0x000310B4 File Offset: 0x0002F2B4
			private TypeReference LookupTypeForwarded(string @namespace, string name)
			{
				if (!this.module.HasExportedTypes)
				{
					return null;
				}
				Collection<ExportedType> exportedTypes = this.module.ExportedTypes;
				for (int i = 0; i < exportedTypes.Count; i++)
				{
					ExportedType exportedType = exportedTypes[i];
					if (exportedType.Name == name && exportedType.Namespace == @namespace)
					{
						return exportedType.CreateReference();
					}
				}
				return null;
			}

			// Token: 0x06000FEF RID: 4079 RVA: 0x00018659 File Offset: 0x00016859
			private static void Initialize(object obj)
			{
			}
		}

		// Token: 0x0200027D RID: 637
		private sealed class CommonTypeSystem : TypeSystem
		{
			// Token: 0x06000FF3 RID: 4083 RVA: 0x0003101E File Offset: 0x0002F21E
			public CommonTypeSystem(ModuleDefinition module)
				: base(module)
			{
			}

			// Token: 0x06000FF4 RID: 4084 RVA: 0x0003118D File Offset: 0x0002F38D
			internal override TypeReference LookupType(string @namespace, string name)
			{
				return this.CreateTypeReference(@namespace, name);
			}

			// Token: 0x06000FF5 RID: 4085 RVA: 0x00031198 File Offset: 0x0002F398
			public AssemblyNameReference GetCoreLibraryReference()
			{
				if (this.core_library != null)
				{
					return this.core_library;
				}
				if (this.module.TryGetCoreLibraryReference(out this.core_library))
				{
					return this.core_library;
				}
				this.core_library = new AssemblyNameReference
				{
					Name = "mscorlib",
					Version = this.GetCorlibVersion(),
					PublicKeyToken = new byte[] { 183, 122, 92, 86, 25, 52, 224, 137 }
				};
				this.module.AssemblyReferences.Add(this.core_library);
				return this.core_library;
			}

			// Token: 0x06000FF6 RID: 4086 RVA: 0x00031228 File Offset: 0x0002F428
			private Version GetCorlibVersion()
			{
				switch (this.module.Runtime)
				{
				case TargetRuntime.Net_1_0:
				case TargetRuntime.Net_1_1:
					return new Version(1, 0, 0, 0);
				case TargetRuntime.Net_2_0:
					return new Version(2, 0, 0, 0);
				case TargetRuntime.Net_4_0:
					return new Version(4, 0, 0, 0);
				default:
					throw new NotSupportedException();
				}
			}

			// Token: 0x06000FF7 RID: 4087 RVA: 0x0003127C File Offset: 0x0002F47C
			private TypeReference CreateTypeReference(string @namespace, string name)
			{
				return new TypeReference(@namespace, name, this.module, this.GetCoreLibraryReference());
			}

			// Token: 0x04000597 RID: 1431
			private AssemblyNameReference core_library;
		}
	}
}
