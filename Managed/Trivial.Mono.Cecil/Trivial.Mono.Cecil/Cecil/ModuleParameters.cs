using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200008E RID: 142
	public sealed class ModuleParameters
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00013DCD File Offset: 0x00011FCD
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x00013DD5 File Offset: 0x00011FD5
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

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00013DDE File Offset: 0x00011FDE
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x00013DE6 File Offset: 0x00011FE6
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

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00013DEF File Offset: 0x00011FEF
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x00013DF7 File Offset: 0x00011FF7
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

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00013E00 File Offset: 0x00012000
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x00013E08 File Offset: 0x00012008
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

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00013E11 File Offset: 0x00012011
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x00013E19 File Offset: 0x00012019
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

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00013E22 File Offset: 0x00012022
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x00013E2A File Offset: 0x0001202A
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

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x00013E33 File Offset: 0x00012033
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x00013E3B File Offset: 0x0001203B
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

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00013E44 File Offset: 0x00012044
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x00013E4C File Offset: 0x0001204C
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

		// Token: 0x0600062B RID: 1579 RVA: 0x00013E55 File Offset: 0x00012055
		public ModuleParameters()
		{
			this.kind = ModuleKind.Dll;
			this.Runtime = ModuleParameters.GetCurrentRuntime();
			this.architecture = TargetArchitecture.I386;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00013E7A File Offset: 0x0001207A
		private static TargetRuntime GetCurrentRuntime()
		{
			return typeof(object).Assembly.ImageRuntimeVersion.ParseRuntime();
		}

		// Token: 0x040001D4 RID: 468
		private ModuleKind kind;

		// Token: 0x040001D5 RID: 469
		private TargetRuntime runtime;

		// Token: 0x040001D6 RID: 470
		private uint? timestamp;

		// Token: 0x040001D7 RID: 471
		private TargetArchitecture architecture;

		// Token: 0x040001D8 RID: 472
		private IAssemblyResolver assembly_resolver;

		// Token: 0x040001D9 RID: 473
		private IMetadataResolver metadata_resolver;

		// Token: 0x040001DA RID: 474
		private IMetadataImporterProvider metadata_importer_provider;

		// Token: 0x040001DB RID: 475
		private IReflectionImporterProvider reflection_importer_provider;
	}
}
