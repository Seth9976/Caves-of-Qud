using System;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000CA RID: 202
	public abstract class TypeSystem
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x0001A390 File Offset: 0x00018590
		[Obsolete("Use CoreLibrary")]
		public IMetadataScope Corlib
		{
			get
			{
				return this.CoreLibrary;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0001A398 File Offset: 0x00018598
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

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0001A3BC File Offset: 0x000185BC
		public TypeReference Object
		{
			get
			{
				return this.type_object ?? this.LookupSystemType(ref this.type_object, "Object", ElementType.Object);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0001A3DB File Offset: 0x000185DB
		public TypeReference Void
		{
			get
			{
				return this.type_void ?? this.LookupSystemType(ref this.type_void, "Void", ElementType.Void);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0001A3F9 File Offset: 0x000185F9
		public TypeReference Boolean
		{
			get
			{
				return this.type_bool ?? this.LookupSystemValueType(ref this.type_bool, "Boolean", ElementType.Boolean);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0001A417 File Offset: 0x00018617
		public TypeReference Char
		{
			get
			{
				return this.type_char ?? this.LookupSystemValueType(ref this.type_char, "Char", ElementType.Char);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0001A435 File Offset: 0x00018635
		public TypeReference SByte
		{
			get
			{
				return this.type_sbyte ?? this.LookupSystemValueType(ref this.type_sbyte, "SByte", ElementType.I1);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0001A453 File Offset: 0x00018653
		public TypeReference Byte
		{
			get
			{
				return this.type_byte ?? this.LookupSystemValueType(ref this.type_byte, "Byte", ElementType.U1);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0001A471 File Offset: 0x00018671
		public TypeReference Int16
		{
			get
			{
				return this.type_int16 ?? this.LookupSystemValueType(ref this.type_int16, "Int16", ElementType.I2);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0001A48F File Offset: 0x0001868F
		public TypeReference UInt16
		{
			get
			{
				return this.type_uint16 ?? this.LookupSystemValueType(ref this.type_uint16, "UInt16", ElementType.U2);
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x0001A4AD File Offset: 0x000186AD
		public TypeReference Int32
		{
			get
			{
				return this.type_int32 ?? this.LookupSystemValueType(ref this.type_int32, "Int32", ElementType.I4);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x0001A4CB File Offset: 0x000186CB
		public TypeReference UInt32
		{
			get
			{
				return this.type_uint32 ?? this.LookupSystemValueType(ref this.type_uint32, "UInt32", ElementType.U4);
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0001A4EA File Offset: 0x000186EA
		public TypeReference Int64
		{
			get
			{
				return this.type_int64 ?? this.LookupSystemValueType(ref this.type_int64, "Int64", ElementType.I8);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0001A509 File Offset: 0x00018709
		public TypeReference UInt64
		{
			get
			{
				return this.type_uint64 ?? this.LookupSystemValueType(ref this.type_uint64, "UInt64", ElementType.U8);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x0001A528 File Offset: 0x00018728
		public TypeReference Single
		{
			get
			{
				return this.type_single ?? this.LookupSystemValueType(ref this.type_single, "Single", ElementType.R4);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x0001A547 File Offset: 0x00018747
		public TypeReference Double
		{
			get
			{
				return this.type_double ?? this.LookupSystemValueType(ref this.type_double, "Double", ElementType.R8);
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0001A566 File Offset: 0x00018766
		public TypeReference IntPtr
		{
			get
			{
				return this.type_intptr ?? this.LookupSystemValueType(ref this.type_intptr, "IntPtr", ElementType.I);
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x0001A585 File Offset: 0x00018785
		public TypeReference UIntPtr
		{
			get
			{
				return this.type_uintptr ?? this.LookupSystemValueType(ref this.type_uintptr, "UIntPtr", ElementType.U);
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0001A5A4 File Offset: 0x000187A4
		public TypeReference String
		{
			get
			{
				return this.type_string ?? this.LookupSystemType(ref this.type_string, "String", ElementType.String);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0001A5C3 File Offset: 0x000187C3
		public TypeReference TypedReference
		{
			get
			{
				return this.type_typedref ?? this.LookupSystemValueType(ref this.type_typedref, "TypedReference", ElementType.TypedByRef);
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001A5E2 File Offset: 0x000187E2
		private TypeSystem(ModuleDefinition module)
		{
			this.module = module;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001A5F1 File Offset: 0x000187F1
		internal static TypeSystem CreateTypeSystem(ModuleDefinition module)
		{
			if (module.IsCoreLibrary())
			{
				return new TypeSystem.CoreTypeSystem(module);
			}
			return new TypeSystem.CommonTypeSystem(module);
		}

		// Token: 0x06000887 RID: 2183
		internal abstract TypeReference LookupType(string @namespace, string name);

		// Token: 0x06000888 RID: 2184 RVA: 0x0001A608 File Offset: 0x00018808
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

		// Token: 0x06000889 RID: 2185 RVA: 0x0001A668 File Offset: 0x00018868
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
					typeReference2.IsValueType = true;
					TypeReference typeReference3;
					typeRef = (typeReference3 = typeReference2);
					typeReference = typeReference3;
				}
			}
			return typeReference;
		}

		// Token: 0x040002FB RID: 763
		private readonly ModuleDefinition module;

		// Token: 0x040002FC RID: 764
		private TypeReference type_object;

		// Token: 0x040002FD RID: 765
		private TypeReference type_void;

		// Token: 0x040002FE RID: 766
		private TypeReference type_bool;

		// Token: 0x040002FF RID: 767
		private TypeReference type_char;

		// Token: 0x04000300 RID: 768
		private TypeReference type_sbyte;

		// Token: 0x04000301 RID: 769
		private TypeReference type_byte;

		// Token: 0x04000302 RID: 770
		private TypeReference type_int16;

		// Token: 0x04000303 RID: 771
		private TypeReference type_uint16;

		// Token: 0x04000304 RID: 772
		private TypeReference type_int32;

		// Token: 0x04000305 RID: 773
		private TypeReference type_uint32;

		// Token: 0x04000306 RID: 774
		private TypeReference type_int64;

		// Token: 0x04000307 RID: 775
		private TypeReference type_uint64;

		// Token: 0x04000308 RID: 776
		private TypeReference type_single;

		// Token: 0x04000309 RID: 777
		private TypeReference type_double;

		// Token: 0x0400030A RID: 778
		private TypeReference type_intptr;

		// Token: 0x0400030B RID: 779
		private TypeReference type_uintptr;

		// Token: 0x0400030C RID: 780
		private TypeReference type_string;

		// Token: 0x0400030D RID: 781
		private TypeReference type_typedref;

		// Token: 0x0200014B RID: 331
		private sealed class CoreTypeSystem : TypeSystem
		{
			// Token: 0x06000BDB RID: 3035 RVA: 0x000263BB File Offset: 0x000245BB
			public CoreTypeSystem(ModuleDefinition module)
				: base(module)
			{
			}

			// Token: 0x06000BDC RID: 3036 RVA: 0x000263C4 File Offset: 0x000245C4
			internal override TypeReference LookupType(string @namespace, string name)
			{
				TypeReference typeReference = this.LookupTypeDefinition(@namespace, name) ?? this.LookupTypeForwarded(@namespace, name);
				if (typeReference != null)
				{
					return typeReference;
				}
				throw new NotSupportedException();
			}

			// Token: 0x06000BDD RID: 3037 RVA: 0x000263F0 File Offset: 0x000245F0
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

			// Token: 0x06000BDE RID: 3038 RVA: 0x00026450 File Offset: 0x00024650
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

			// Token: 0x06000BDF RID: 3039 RVA: 0x00002921 File Offset: 0x00000B21
			private static void Initialize(object obj)
			{
			}
		}

		// Token: 0x0200014C RID: 332
		private sealed class CommonTypeSystem : TypeSystem
		{
			// Token: 0x06000BE0 RID: 3040 RVA: 0x000263BB File Offset: 0x000245BB
			public CommonTypeSystem(ModuleDefinition module)
				: base(module)
			{
			}

			// Token: 0x06000BE1 RID: 3041 RVA: 0x000264B5 File Offset: 0x000246B5
			internal override TypeReference LookupType(string @namespace, string name)
			{
				return this.CreateTypeReference(@namespace, name);
			}

			// Token: 0x06000BE2 RID: 3042 RVA: 0x000264C0 File Offset: 0x000246C0
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

			// Token: 0x06000BE3 RID: 3043 RVA: 0x00026550 File Offset: 0x00024750
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

			// Token: 0x06000BE4 RID: 3044 RVA: 0x000265A4 File Offset: 0x000247A4
			private TypeReference CreateTypeReference(string @namespace, string name)
			{
				return new TypeReference(@namespace, name, this.module, this.GetCoreLibraryReference());
			}

			// Token: 0x0400074F RID: 1871
			private AssemblyNameReference core_library;
		}
	}
}
