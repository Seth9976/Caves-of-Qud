using System;

namespace Mono.Cecil
{
	// Token: 0x02000245 RID: 581
	public sealed class ModuleParameters
	{
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0002BFCD File Offset: 0x0002A1CD
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x0002BFD5 File Offset: 0x0002A1D5
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

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0002BFDE File Offset: 0x0002A1DE
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x0002BFE6 File Offset: 0x0002A1E6
		public TargetRuntime Runtime
		{
			get
			{
				return this.runtime;
			}
			set
			{
				this.runtime = value;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0002BFEF File Offset: 0x0002A1EF
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x0002BFF7 File Offset: 0x0002A1F7
		public uint? Timestamp
		{
			get
			{
				return this.timestamp;
			}
			set
			{
				this.timestamp = value;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x0002C000 File Offset: 0x0002A200
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x0002C008 File Offset: 0x0002A208
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

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0002C011 File Offset: 0x0002A211
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x0002C019 File Offset: 0x0002A219
		public IAssemblyResolver AssemblyResolver
		{
			get
			{
				return this.assembly_resolver;
			}
			set
			{
				this.assembly_resolver = value;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0002C022 File Offset: 0x0002A222
		// (set) Token: 0x06000D7F RID: 3455 RVA: 0x0002C02A File Offset: 0x0002A22A
		public IMetadataResolver MetadataResolver
		{
			get
			{
				return this.metadata_resolver;
			}
			set
			{
				this.metadata_resolver = value;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x0002C033 File Offset: 0x0002A233
		// (set) Token: 0x06000D81 RID: 3457 RVA: 0x0002C03B File Offset: 0x0002A23B
		public IMetadataImporterProvider MetadataImporterProvider
		{
			get
			{
				return this.metadata_importer_provider;
			}
			set
			{
				this.metadata_importer_provider = value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x0002C044 File Offset: 0x0002A244
		// (set) Token: 0x06000D83 RID: 3459 RVA: 0x0002C04C File Offset: 0x0002A24C
		public IReflectionImporterProvider ReflectionImporterProvider
		{
			get
			{
				return this.reflection_importer_provider;
			}
			set
			{
				this.reflection_importer_provider = value;
			}
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0002C055 File Offset: 0x0002A255
		public ModuleParameters()
		{
			this.kind = ModuleKind.Dll;
			this.Runtime = ModuleParameters.GetCurrentRuntime();
			this.architecture = TargetArchitecture.I386;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0002C07A File Offset: 0x0002A27A
		private static TargetRuntime GetCurrentRuntime()
		{
			return typeof(object).Assembly.ImageRuntimeVersion.ParseRuntime();
		}

		// Token: 0x040003F6 RID: 1014
		private ModuleKind kind;

		// Token: 0x040003F7 RID: 1015
		private TargetRuntime runtime;

		// Token: 0x040003F8 RID: 1016
		private uint? timestamp;

		// Token: 0x040003F9 RID: 1017
		private TargetArchitecture architecture;

		// Token: 0x040003FA RID: 1018
		private IAssemblyResolver assembly_resolver;

		// Token: 0x040003FB RID: 1019
		private IMetadataResolver metadata_resolver;

		// Token: 0x040003FC RID: 1020
		private IMetadataImporterProvider metadata_importer_provider;

		// Token: 0x040003FD RID: 1021
		private IReflectionImporterProvider reflection_importer_provider;
	}
}
