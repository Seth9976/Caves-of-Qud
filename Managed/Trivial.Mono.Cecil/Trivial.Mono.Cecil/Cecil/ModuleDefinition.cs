using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Cecil.PE;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200008C RID: 140
	public sealed class ModuleDefinition : ModuleReference, ICustomAttributeProvider, IMetadataTokenProvider, ICustomDebugInformationProvider, IDisposable
	{
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00012E40 File Offset: 0x00011040
		public bool IsMain
		{
			get
			{
				return this.kind != ModuleKind.NetModule;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00012E4E File Offset: 0x0001104E
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x00012E56 File Offset: 0x00011056
		public ModuleKind Kind
		{
			get
			{
				return this.kind;
			}
			set
			{
				this.kind = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00012E5F File Offset: 0x0001105F
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x00012E67 File Offset: 0x00011067
		public MetadataKind MetadataKind
		{
			get
			{
				return this.metadata_kind;
			}
			set
			{
				this.metadata_kind = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00012E70 File Offset: 0x00011070
		internal WindowsRuntimeProjections Projections
		{
			get
			{
				if (this.projections == null)
				{
					Interlocked.CompareExchange<WindowsRuntimeProjections>(ref this.projections, new WindowsRuntimeProjections(this), null);
				}
				return this.projections;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00012E93 File Offset: 0x00011093
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x00012E9B File Offset: 0x0001109B
		public TargetRuntime Runtime
		{
			get
			{
				return this.runtime;
			}
			set
			{
				this.runtime = value;
				this.runtime_version = this.runtime.RuntimeVersionString();
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00012EB5 File Offset: 0x000110B5
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x00012EBD File Offset: 0x000110BD
		public string RuntimeVersion
		{
			get
			{
				return this.runtime_version;
			}
			set
			{
				this.runtime_version = value;
				this.runtime = this.runtime_version.ParseRuntime();
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00012ED7 File Offset: 0x000110D7
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x00012EDF File Offset: 0x000110DF
		public TargetArchitecture Architecture
		{
			get
			{
				return this.architecture;
			}
			set
			{
				this.architecture = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00012EE8 File Offset: 0x000110E8
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x00012EF0 File Offset: 0x000110F0
		public ModuleAttributes Attributes
		{
			get
			{
				return this.attributes;
			}
			set
			{
				this.attributes = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00012EF9 File Offset: 0x000110F9
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x00012F01 File Offset: 0x00011101
		public ModuleCharacteristics Characteristics
		{
			get
			{
				return this.characteristics;
			}
			set
			{
				this.characteristics = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00012F0A File Offset: 0x0001110A
		[Obsolete("Use FileName")]
		public string FullyQualifiedName
		{
			get
			{
				return this.file_name;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00012F0A File Offset: 0x0001110A
		public string FileName
		{
			get
			{
				return this.file_name;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00012F12 File Offset: 0x00011112
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x00012F1A File Offset: 0x0001111A
		public Guid Mvid
		{
			get
			{
				return this.mvid;
			}
			set
			{
				this.mvid = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00012F23 File Offset: 0x00011123
		internal bool HasImage
		{
			get
			{
				return this.Image != null;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00012F2E File Offset: 0x0001112E
		public bool HasSymbols
		{
			get
			{
				return this.symbol_reader != null;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00012F39 File Offset: 0x00011139
		public ISymbolReader SymbolReader
		{
			get
			{
				return this.symbol_reader;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x000030E0 File Offset: 0x000012E0
		public override MetadataScopeType MetadataScopeType
		{
			get
			{
				return MetadataScopeType.ModuleDefinition;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00012F41 File Offset: 0x00011141
		public AssemblyDefinition Assembly
		{
			get
			{
				return this.assembly;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00012F49 File Offset: 0x00011149
		internal IReflectionImporter ReflectionImporter
		{
			get
			{
				if (this.reflection_importer == null)
				{
					Interlocked.CompareExchange<IReflectionImporter>(ref this.reflection_importer, new ReflectionImporter(this), null);
				}
				return this.reflection_importer;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00012F6C File Offset: 0x0001116C
		internal IMetadataImporter MetadataImporter
		{
			get
			{
				if (this.metadata_importer == null)
				{
					Interlocked.CompareExchange<IMetadataImporter>(ref this.metadata_importer, new MetadataImporter(this), null);
				}
				return this.metadata_importer;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00012F90 File Offset: 0x00011190
		public IAssemblyResolver AssemblyResolver
		{
			get
			{
				if (this.assembly_resolver.value == null)
				{
					object obj = this.module_lock;
					lock (obj)
					{
						this.assembly_resolver = Disposable.Owned<IAssemblyResolver>(new DefaultAssemblyResolver());
					}
				}
				return this.assembly_resolver.value;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00012FEC File Offset: 0x000111EC
		public IMetadataResolver MetadataResolver
		{
			get
			{
				if (this.metadata_resolver == null)
				{
					Interlocked.CompareExchange<IMetadataResolver>(ref this.metadata_resolver, new MetadataResolver(this.AssemblyResolver), null);
				}
				return this.metadata_resolver;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00013014 File Offset: 0x00011214
		public TypeSystem TypeSystem
		{
			get
			{
				if (this.type_system == null)
				{
					Interlocked.CompareExchange<TypeSystem>(ref this.type_system, TypeSystem.CreateTypeSystem(this), null);
				}
				return this.type_system;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00013037 File Offset: 0x00011237
		public bool HasAssemblyReferences
		{
			get
			{
				if (this.references != null)
				{
					return this.references.Count > 0;
				}
				return this.HasImage && this.Image.HasTable(Table.AssemblyRef);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00013068 File Offset: 0x00011268
		public Collection<AssemblyNameReference> AssemblyReferences
		{
			get
			{
				if (this.references != null)
				{
					return this.references;
				}
				if (this.HasImage)
				{
					return this.Read<ModuleDefinition, Collection<AssemblyNameReference>>(ref this.references, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadAssemblyReferences());
				}
				return this.references = new Collection<AssemblyNameReference>();
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x000130C7 File Offset: 0x000112C7
		public bool HasModuleReferences
		{
			get
			{
				if (this.modules != null)
				{
					return this.modules.Count > 0;
				}
				return this.HasImage && this.Image.HasTable(Table.ModuleRef);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x000130F8 File Offset: 0x000112F8
		public Collection<ModuleReference> ModuleReferences
		{
			get
			{
				if (this.modules != null)
				{
					return this.modules;
				}
				if (this.HasImage)
				{
					return this.Read<ModuleDefinition, Collection<ModuleReference>>(ref this.modules, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadModuleReferences());
				}
				return this.modules = new Collection<ModuleReference>();
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x00013158 File Offset: 0x00011358
		public bool HasResources
		{
			get
			{
				if (this.resources != null)
				{
					return this.resources.Count > 0;
				}
				if (!this.HasImage)
				{
					return false;
				}
				if (!this.Image.HasTable(Table.ManifestResource))
				{
					return this.Read<ModuleDefinition, bool>(this, (ModuleDefinition _, MetadataReader reader) => reader.HasFileResource());
				}
				return true;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x000131C0 File Offset: 0x000113C0
		public Collection<Resource> Resources
		{
			get
			{
				if (this.resources != null)
				{
					return this.resources;
				}
				if (this.HasImage)
				{
					return this.Read<ModuleDefinition, Collection<Resource>>(ref this.resources, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadResources());
				}
				return this.resources = new Collection<Resource>();
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0001321F File Offset: 0x0001141F
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this);
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0001323F File Offset: 0x0001143F
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this);
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00013258 File Offset: 0x00011458
		public bool HasTypes
		{
			get
			{
				if (this.types != null)
				{
					return this.types.Count > 0;
				}
				return this.HasImage && this.Image.HasTable(Table.TypeDef);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x00013288 File Offset: 0x00011488
		public Collection<TypeDefinition> Types
		{
			get
			{
				if (this.types != null)
				{
					return this.types;
				}
				if (this.HasImage)
				{
					return this.Read<ModuleDefinition, TypeDefinitionCollection>(ref this.types, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadTypes());
				}
				return this.types = new TypeDefinitionCollection(this);
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x000132E8 File Offset: 0x000114E8
		public bool HasExportedTypes
		{
			get
			{
				if (this.exported_types != null)
				{
					return this.exported_types.Count > 0;
				}
				return this.HasImage && this.Image.HasTable(Table.ExportedType);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00013318 File Offset: 0x00011518
		public Collection<ExportedType> ExportedTypes
		{
			get
			{
				if (this.exported_types != null)
				{
					return this.exported_types;
				}
				if (this.HasImage)
				{
					return this.Read<ModuleDefinition, Collection<ExportedType>>(ref this.exported_types, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadExportedTypes());
				}
				return this.exported_types = new Collection<ExportedType>();
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00013378 File Offset: 0x00011578
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x000133D3 File Offset: 0x000115D3
		public MethodDefinition EntryPoint
		{
			get
			{
				if (this.entry_point != null)
				{
					return this.entry_point;
				}
				if (this.HasImage)
				{
					return this.Read<ModuleDefinition, MethodDefinition>(ref this.entry_point, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadEntryPoint());
				}
				return this.entry_point = null;
			}
			set
			{
				this.entry_point = value;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x000133DC File Offset: 0x000115DC
		public bool HasCustomDebugInformations
		{
			get
			{
				return this.custom_infos != null && this.custom_infos.Count > 0;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x000133F8 File Offset: 0x000115F8
		public Collection<CustomDebugInformation> CustomDebugInformations
		{
			get
			{
				Collection<CustomDebugInformation> collection;
				if ((collection = this.custom_infos) == null)
				{
					collection = (this.custom_infos = new Collection<CustomDebugInformation>());
				}
				return collection;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0001341D File Offset: 0x0001161D
		internal object SyncRoot
		{
			get
			{
				return this.module_lock;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00013425 File Offset: 0x00011625
		public bool HasDebugHeader
		{
			get
			{
				return this.Image != null && this.Image.DebugHeader != null;
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001343F File Offset: 0x0001163F
		internal ModuleDefinition()
		{
			this.MetadataSystem = new MetadataSystem();
			this.token = new MetadataToken(TokenType.Module, 1);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001346C File Offset: 0x0001166C
		internal ModuleDefinition(Image image)
			: this()
		{
			this.Image = image;
			this.kind = image.Kind;
			this.RuntimeVersion = image.RuntimeVersion;
			this.architecture = image.Architecture;
			this.attributes = image.Attributes;
			this.characteristics = image.Characteristics;
			this.file_name = image.FileName;
			this.timestamp = image.Timestamp;
			this.reader = new MetadataReader(this);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000134E6 File Offset: 0x000116E6
		public void Dispose()
		{
			if (this.Image != null)
			{
				this.Image.Dispose();
			}
			if (this.symbol_reader != null)
			{
				this.symbol_reader.Dispose();
			}
			if (this.assembly_resolver.value != null)
			{
				this.assembly_resolver.Dispose();
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00013526 File Offset: 0x00011726
		public bool HasTypeReference(string fullName)
		{
			return this.HasTypeReference(string.Empty, fullName);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00013534 File Offset: 0x00011734
		public bool HasTypeReference(string scope, string fullName)
		{
			Mixin.CheckFullName(fullName);
			return this.HasImage && this.GetTypeReference(scope, fullName) != null;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00013551 File Offset: 0x00011751
		public bool TryGetTypeReference(string fullName, out TypeReference type)
		{
			return this.TryGetTypeReference(string.Empty, fullName, out type);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00013560 File Offset: 0x00011760
		public bool TryGetTypeReference(string scope, string fullName, out TypeReference type)
		{
			Mixin.CheckFullName(fullName);
			if (!this.HasImage)
			{
				type = null;
				return false;
			}
			TypeReference typeReference;
			type = (typeReference = this.GetTypeReference(scope, fullName));
			return typeReference != null;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00013590 File Offset: 0x00011790
		private TypeReference GetTypeReference(string scope, string fullname)
		{
			return this.Read<Row<string, string>, TypeReference>(new Row<string, string>(scope, fullname), (Row<string, string> row, MetadataReader reader) => reader.GetTypeReference(row.Col1, row.Col2));
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000135BE File Offset: 0x000117BE
		public IEnumerable<TypeReference> GetTypeReferences()
		{
			if (!this.HasImage)
			{
				return Empty<TypeReference>.Array;
			}
			return this.Read<ModuleDefinition, IEnumerable<TypeReference>>(this, (ModuleDefinition _, MetadataReader reader) => reader.GetTypeReferences());
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000135F4 File Offset: 0x000117F4
		public IEnumerable<MemberReference> GetMemberReferences()
		{
			if (!this.HasImage)
			{
				return Empty<MemberReference>.Array;
			}
			return this.Read<ModuleDefinition, IEnumerable<MemberReference>>(this, (ModuleDefinition _, MetadataReader reader) => reader.GetMemberReferences());
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001362A File Offset: 0x0001182A
		public IEnumerable<CustomAttribute> GetCustomAttributes()
		{
			if (!this.HasImage)
			{
				return Empty<CustomAttribute>.Array;
			}
			return this.Read<ModuleDefinition, IEnumerable<CustomAttribute>>(this, (ModuleDefinition _, MetadataReader reader) => reader.GetCustomAttributes());
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00013660 File Offset: 0x00011860
		public TypeReference GetType(string fullName, bool runtimeName)
		{
			if (!runtimeName)
			{
				return this.GetType(fullName);
			}
			return TypeParser.ParseType(this, fullName);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00013674 File Offset: 0x00011874
		public TypeDefinition GetType(string fullName)
		{
			Mixin.CheckFullName(fullName);
			if (fullName.IndexOf('/') > 0)
			{
				return this.GetNestedType(fullName);
			}
			return ((TypeDefinitionCollection)this.Types).GetType(fullName);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000136A0 File Offset: 0x000118A0
		public TypeDefinition GetType(string @namespace, string name)
		{
			Mixin.CheckName(name);
			return ((TypeDefinitionCollection)this.Types).GetType(@namespace ?? string.Empty, name);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000136C3 File Offset: 0x000118C3
		public IEnumerable<TypeDefinition> GetTypes()
		{
			return ModuleDefinition.GetTypes(this.Types);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000136D0 File Offset: 0x000118D0
		private static IEnumerable<TypeDefinition> GetTypes(Collection<TypeDefinition> types)
		{
			int num;
			for (int i = 0; i < types.Count; i = num + 1)
			{
				TypeDefinition type = types[i];
				yield return type;
				if (type.HasNestedTypes)
				{
					foreach (TypeDefinition typeDefinition in ModuleDefinition.GetTypes(type.NestedTypes))
					{
						yield return typeDefinition;
					}
					IEnumerator<TypeDefinition> enumerator = null;
				}
				type = null;
				num = i;
			}
			yield break;
			yield break;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000136E0 File Offset: 0x000118E0
		private TypeDefinition GetNestedType(string fullname)
		{
			string[] array = fullname.Split(new char[] { '/' });
			TypeDefinition typeDefinition = this.GetType(array[0]);
			if (typeDefinition == null)
			{
				return null;
			}
			for (int i = 1; i < array.Length; i++)
			{
				TypeDefinition nestedType = typeDefinition.GetNestedType(array[i]);
				if (nestedType == null)
				{
					return null;
				}
				typeDefinition = nestedType;
			}
			return typeDefinition;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001372E File Offset: 0x0001192E
		internal FieldDefinition Resolve(FieldReference field)
		{
			return this.MetadataResolver.Resolve(field);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001373C File Offset: 0x0001193C
		internal MethodDefinition Resolve(MethodReference method)
		{
			return this.MetadataResolver.Resolve(method);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001374A File Offset: 0x0001194A
		internal TypeDefinition Resolve(TypeReference type)
		{
			return this.MetadataResolver.Resolve(type);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00013758 File Offset: 0x00011958
		private static void CheckContext(IGenericParameterProvider context, ModuleDefinition module)
		{
			if (context == null || context.Module == module)
			{
				return;
			}
			throw new ArgumentException();
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001376C File Offset: 0x0001196C
		[Obsolete("Use ImportReference", false)]
		public TypeReference Import(Type type)
		{
			return this.ImportReference(type, null);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001376C File Offset: 0x0001196C
		public TypeReference ImportReference(Type type)
		{
			return this.ImportReference(type, null);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00013776 File Offset: 0x00011976
		[Obsolete("Use ImportReference", false)]
		public TypeReference Import(Type type, IGenericParameterProvider context)
		{
			return this.ImportReference(type, context);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00013780 File Offset: 0x00011980
		public TypeReference ImportReference(Type type, IGenericParameterProvider context)
		{
			Mixin.CheckType(type);
			ModuleDefinition.CheckContext(context, this);
			return this.ReflectionImporter.ImportReference(type, context);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001379C File Offset: 0x0001199C
		[Obsolete("Use ImportReference", false)]
		public FieldReference Import(FieldInfo field)
		{
			return this.ImportReference(field, null);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000137A6 File Offset: 0x000119A6
		[Obsolete("Use ImportReference", false)]
		public FieldReference Import(FieldInfo field, IGenericParameterProvider context)
		{
			return this.ImportReference(field, context);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001379C File Offset: 0x0001199C
		public FieldReference ImportReference(FieldInfo field)
		{
			return this.ImportReference(field, null);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x000137B0 File Offset: 0x000119B0
		public FieldReference ImportReference(FieldInfo field, IGenericParameterProvider context)
		{
			Mixin.CheckField(field);
			ModuleDefinition.CheckContext(context, this);
			return this.ReflectionImporter.ImportReference(field, context);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000137CC File Offset: 0x000119CC
		[Obsolete("Use ImportReference", false)]
		public MethodReference Import(MethodBase method)
		{
			return this.ImportReference(method, null);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000137D6 File Offset: 0x000119D6
		[Obsolete("Use ImportReference", false)]
		public MethodReference Import(MethodBase method, IGenericParameterProvider context)
		{
			return this.ImportReference(method, context);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000137CC File Offset: 0x000119CC
		public MethodReference ImportReference(MethodBase method)
		{
			return this.ImportReference(method, null);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000137E0 File Offset: 0x000119E0
		public MethodReference ImportReference(MethodBase method, IGenericParameterProvider context)
		{
			Mixin.CheckMethod(method);
			ModuleDefinition.CheckContext(context, this);
			return this.ReflectionImporter.ImportReference(method, context);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x000137FC File Offset: 0x000119FC
		[Obsolete("Use ImportReference", false)]
		public TypeReference Import(TypeReference type)
		{
			return this.ImportReference(type, null);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00013806 File Offset: 0x00011A06
		[Obsolete("Use ImportReference", false)]
		public TypeReference Import(TypeReference type, IGenericParameterProvider context)
		{
			return this.ImportReference(type, context);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000137FC File Offset: 0x000119FC
		public TypeReference ImportReference(TypeReference type)
		{
			return this.ImportReference(type, null);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00013810 File Offset: 0x00011A10
		public TypeReference ImportReference(TypeReference type, IGenericParameterProvider context)
		{
			Mixin.CheckType(type);
			if (type.Module == this)
			{
				return type;
			}
			ModuleDefinition.CheckContext(context, this);
			return this.MetadataImporter.ImportReference(type, context);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00013837 File Offset: 0x00011A37
		[Obsolete("Use ImportReference", false)]
		public FieldReference Import(FieldReference field)
		{
			return this.ImportReference(field, null);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00013841 File Offset: 0x00011A41
		[Obsolete("Use ImportReference", false)]
		public FieldReference Import(FieldReference field, IGenericParameterProvider context)
		{
			return this.ImportReference(field, context);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00013837 File Offset: 0x00011A37
		public FieldReference ImportReference(FieldReference field)
		{
			return this.ImportReference(field, null);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001384B File Offset: 0x00011A4B
		public FieldReference ImportReference(FieldReference field, IGenericParameterProvider context)
		{
			Mixin.CheckField(field);
			if (field.Module == this)
			{
				return field;
			}
			ModuleDefinition.CheckContext(context, this);
			return this.MetadataImporter.ImportReference(field, context);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00013872 File Offset: 0x00011A72
		[Obsolete("Use ImportReference", false)]
		public MethodReference Import(MethodReference method)
		{
			return this.ImportReference(method, null);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001387C File Offset: 0x00011A7C
		[Obsolete("Use ImportReference", false)]
		public MethodReference Import(MethodReference method, IGenericParameterProvider context)
		{
			return this.ImportReference(method, context);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00013872 File Offset: 0x00011A72
		public MethodReference ImportReference(MethodReference method)
		{
			return this.ImportReference(method, null);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00013886 File Offset: 0x00011A86
		public MethodReference ImportReference(MethodReference method, IGenericParameterProvider context)
		{
			Mixin.CheckMethod(method);
			if (method.Module == this)
			{
				return method;
			}
			ModuleDefinition.CheckContext(context, this);
			return this.MetadataImporter.ImportReference(method, context);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000138AD File Offset: 0x00011AAD
		public IMetadataTokenProvider LookupToken(int token)
		{
			return this.LookupToken(new MetadataToken((uint)token));
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000138BB File Offset: 0x00011ABB
		public IMetadataTokenProvider LookupToken(MetadataToken token)
		{
			return this.Read<MetadataToken, IMetadataTokenProvider>(token, (MetadataToken t, MetadataReader reader) => reader.LookupToken(t));
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000138E4 File Offset: 0x00011AE4
		internal TRet Read<TItem, TRet>(TItem item, Func<TItem, MetadataReader, TRet> read)
		{
			object obj = this.module_lock;
			TRet tret2;
			lock (obj)
			{
				int position = this.reader.position;
				IGenericContext context = this.reader.context;
				TRet tret = read.Invoke(item, this.reader);
				this.reader.position = position;
				this.reader.context = context;
				tret2 = tret;
			}
			return tret2;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001395C File Offset: 0x00011B5C
		internal TRet Read<TItem, TRet>(ref TRet variable, TItem item, Func<TItem, MetadataReader, TRet> read) where TRet : class
		{
			object obj = this.module_lock;
			TRet tret;
			lock (obj)
			{
				if (variable != null)
				{
					tret = variable;
				}
				else
				{
					int position = this.reader.position;
					IGenericContext context = this.reader.context;
					TRet tret2 = read.Invoke(item, this.reader);
					this.reader.position = position;
					this.reader.context = context;
					tret = (variable = tret2);
				}
			}
			return tret;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x000139F4 File Offset: 0x00011BF4
		public ImageDebugHeader GetDebugHeader()
		{
			return this.Image.DebugHeader ?? new ImageDebugHeader();
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00013A0A File Offset: 0x00011C0A
		public static ModuleDefinition CreateModule(string name, ModuleKind kind)
		{
			return ModuleDefinition.CreateModule(name, new ModuleParameters
			{
				Kind = kind
			});
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00013A20 File Offset: 0x00011C20
		public static ModuleDefinition CreateModule(string name, ModuleParameters parameters)
		{
			Mixin.CheckName(name);
			Mixin.CheckParameters(parameters);
			ModuleDefinition moduleDefinition = new ModuleDefinition
			{
				Name = name,
				kind = parameters.Kind,
				timestamp = (parameters.Timestamp ?? Mixin.GetTimestamp()),
				Runtime = parameters.Runtime,
				architecture = parameters.Architecture,
				mvid = Guid.NewGuid(),
				Attributes = ModuleAttributes.ILOnly,
				Characteristics = (ModuleCharacteristics.DynamicBase | ModuleCharacteristics.NoSEH | ModuleCharacteristics.NXCompat | ModuleCharacteristics.TerminalServerAware)
			};
			if (parameters.AssemblyResolver != null)
			{
				moduleDefinition.assembly_resolver = Disposable.NotOwned<IAssemblyResolver>(parameters.AssemblyResolver);
			}
			if (parameters.MetadataResolver != null)
			{
				moduleDefinition.metadata_resolver = parameters.MetadataResolver;
			}
			if (parameters.MetadataImporterProvider != null)
			{
				moduleDefinition.metadata_importer = parameters.MetadataImporterProvider.GetMetadataImporter(moduleDefinition);
			}
			if (parameters.ReflectionImporterProvider != null)
			{
				moduleDefinition.reflection_importer = parameters.ReflectionImporterProvider.GetReflectionImporter(moduleDefinition);
			}
			if (parameters.Kind != ModuleKind.NetModule)
			{
				AssemblyDefinition assemblyDefinition = (moduleDefinition.assembly = new AssemblyDefinition());
				moduleDefinition.assembly.Name = ModuleDefinition.CreateAssemblyName(name);
				assemblyDefinition.main_module = moduleDefinition;
			}
			moduleDefinition.Types.Add(new TypeDefinition(string.Empty, "<Module>", TypeAttributes.NotPublic));
			return moduleDefinition;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00013B58 File Offset: 0x00011D58
		private static AssemblyNameDefinition CreateAssemblyName(string name)
		{
			if (name.EndsWith(".dll") || name.EndsWith(".exe"))
			{
				name = name.Substring(0, name.Length - 4);
			}
			return new AssemblyNameDefinition(name, Mixin.ZeroVersion);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00013B90 File Offset: 0x00011D90
		public void ReadSymbols()
		{
			if (string.IsNullOrEmpty(this.file_name))
			{
				throw new InvalidOperationException();
			}
			DefaultSymbolReaderProvider defaultSymbolReaderProvider = new DefaultSymbolReaderProvider(true);
			this.ReadSymbols(defaultSymbolReaderProvider.GetSymbolReader(this, this.file_name));
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00013BCC File Offset: 0x00011DCC
		public void ReadSymbols(ISymbolReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.symbol_reader = reader;
			if (!this.symbol_reader.ProcessDebugHeader(this.GetDebugHeader()))
			{
				this.symbol_reader = null;
				throw new InvalidOperationException();
			}
			if (this.HasImage && this.ReadingMode == ReadingMode.Immediate)
			{
				new ImmediateModuleReader(this.Image).ReadSymbols(this);
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00013C30 File Offset: 0x00011E30
		public static ModuleDefinition ReadModule(string fileName)
		{
			return ModuleDefinition.ReadModule(fileName, new ReaderParameters(ReadingMode.Deferred));
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00013C40 File Offset: 0x00011E40
		public static ModuleDefinition ReadModule(string fileName, ReaderParameters parameters)
		{
			Stream stream = ModuleDefinition.GetFileStream(fileName, 3, (!parameters.ReadWrite) ? 1 : 3, 1);
			if (parameters.InMemory)
			{
				MemoryStream memoryStream = new MemoryStream((int)(stream.CanSeek ? stream.Length : 0L));
				using (stream)
				{
					stream.CopyTo(memoryStream);
				}
				memoryStream.Position = 0L;
				stream = memoryStream;
			}
			ModuleDefinition moduleDefinition;
			try
			{
				moduleDefinition = ModuleDefinition.ReadModule(Disposable.Owned<Stream>(stream), fileName, parameters);
			}
			catch (Exception)
			{
				stream.Dispose();
				throw;
			}
			return moduleDefinition;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00013CDC File Offset: 0x00011EDC
		private static Stream GetFileStream(string fileName, FileMode mode, FileAccess access, FileShare share)
		{
			Mixin.CheckFileName(fileName);
			return new FileStream(fileName, mode, access, share);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00013CED File Offset: 0x00011EED
		public static ModuleDefinition ReadModule(Stream stream)
		{
			return ModuleDefinition.ReadModule(stream, new ReaderParameters(ReadingMode.Deferred));
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00013CFB File Offset: 0x00011EFB
		public static ModuleDefinition ReadModule(Stream stream, ReaderParameters parameters)
		{
			Mixin.CheckStream(stream);
			Mixin.CheckReadSeek(stream);
			return ModuleDefinition.ReadModule(Disposable.NotOwned<Stream>(stream), stream.GetFileName(), parameters);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00013D1B File Offset: 0x00011F1B
		private static ModuleDefinition ReadModule(Disposable<Stream> stream, string fileName, ReaderParameters parameters)
		{
			Mixin.CheckParameters(parameters);
			return ModuleReader.CreateModule(ImageReader.ReadImage(stream, fileName), parameters);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00013D30 File Offset: 0x00011F30
		public void Write(string fileName)
		{
			this.Write(fileName, new WriterParameters());
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00013D40 File Offset: 0x00011F40
		public void Write(string fileName, WriterParameters parameters)
		{
			Mixin.CheckParameters(parameters);
			Stream fileStream = ModuleDefinition.GetFileStream(fileName, 2, 3, 1);
			ModuleWriter.WriteModule(this, Disposable.Owned<Stream>(fileStream), parameters);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00013D6A File Offset: 0x00011F6A
		public void Write()
		{
			this.Write(new WriterParameters());
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00013D77 File Offset: 0x00011F77
		public void Write(WriterParameters parameters)
		{
			if (!this.HasImage)
			{
				throw new InvalidOperationException();
			}
			this.Write(this.Image.Stream.value, parameters);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00013D9E File Offset: 0x00011F9E
		public void Write(Stream stream)
		{
			this.Write(stream, new WriterParameters());
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00013DAC File Offset: 0x00011FAC
		public void Write(Stream stream, WriterParameters parameters)
		{
			Mixin.CheckStream(stream);
			Mixin.CheckWriteSeek(stream);
			Mixin.CheckParameters(parameters);
			ModuleWriter.WriteModule(this, Disposable.NotOwned<Stream>(stream), parameters);
		}

		// Token: 0x040001AF RID: 431
		internal Image Image;

		// Token: 0x040001B0 RID: 432
		internal MetadataSystem MetadataSystem;

		// Token: 0x040001B1 RID: 433
		internal ReadingMode ReadingMode;

		// Token: 0x040001B2 RID: 434
		internal ISymbolReaderProvider SymbolReaderProvider;

		// Token: 0x040001B3 RID: 435
		internal ISymbolReader symbol_reader;

		// Token: 0x040001B4 RID: 436
		internal Disposable<IAssemblyResolver> assembly_resolver;

		// Token: 0x040001B5 RID: 437
		internal IMetadataResolver metadata_resolver;

		// Token: 0x040001B6 RID: 438
		internal TypeSystem type_system;

		// Token: 0x040001B7 RID: 439
		internal readonly MetadataReader reader;

		// Token: 0x040001B8 RID: 440
		private readonly string file_name;

		// Token: 0x040001B9 RID: 441
		internal string runtime_version;

		// Token: 0x040001BA RID: 442
		internal ModuleKind kind;

		// Token: 0x040001BB RID: 443
		private WindowsRuntimeProjections projections;

		// Token: 0x040001BC RID: 444
		private MetadataKind metadata_kind;

		// Token: 0x040001BD RID: 445
		private TargetRuntime runtime;

		// Token: 0x040001BE RID: 446
		private TargetArchitecture architecture;

		// Token: 0x040001BF RID: 447
		private ModuleAttributes attributes;

		// Token: 0x040001C0 RID: 448
		private ModuleCharacteristics characteristics;

		// Token: 0x040001C1 RID: 449
		private Guid mvid;

		// Token: 0x040001C2 RID: 450
		internal uint timestamp;

		// Token: 0x040001C3 RID: 451
		internal AssemblyDefinition assembly;

		// Token: 0x040001C4 RID: 452
		private MethodDefinition entry_point;

		// Token: 0x040001C5 RID: 453
		internal IReflectionImporter reflection_importer;

		// Token: 0x040001C6 RID: 454
		internal IMetadataImporter metadata_importer;

		// Token: 0x040001C7 RID: 455
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x040001C8 RID: 456
		private Collection<AssemblyNameReference> references;

		// Token: 0x040001C9 RID: 457
		private Collection<ModuleReference> modules;

		// Token: 0x040001CA RID: 458
		private Collection<Resource> resources;

		// Token: 0x040001CB RID: 459
		private Collection<ExportedType> exported_types;

		// Token: 0x040001CC RID: 460
		private TypeDefinitionCollection types;

		// Token: 0x040001CD RID: 461
		internal Collection<CustomDebugInformation> custom_infos;

		// Token: 0x040001CE RID: 462
		private readonly object module_lock = new object();
	}
}
