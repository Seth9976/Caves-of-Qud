using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000AF RID: 175
	public readonly struct ModuleDefinition
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x00011363 File Offset: 0x0000F563
		[NullableContext(1)]
		internal ModuleDefinition(MetadataReader reader)
		{
			this._reader = reader;
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001136C File Offset: 0x0000F56C
		public int Generation
		{
			get
			{
				return (int)this._reader.ModuleTable.GetGeneration();
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001137E File Offset: 0x0000F57E
		public StringHandle Name
		{
			get
			{
				return this._reader.ModuleTable.GetName();
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00011390 File Offset: 0x0000F590
		public GuidHandle Mvid
		{
			get
			{
				return this._reader.ModuleTable.GetMvid();
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x000113A2 File Offset: 0x0000F5A2
		public GuidHandle GenerationId
		{
			get
			{
				return this._reader.ModuleTable.GetEncId();
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x000113B4 File Offset: 0x0000F5B4
		public GuidHandle BaseGenerationId
		{
			get
			{
				return this._reader.ModuleTable.GetEncBaseId();
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000113C6 File Offset: 0x0000F5C6
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, EntityHandle.ModuleDefinition);
		}

		// Token: 0x04000436 RID: 1078
		private readonly MetadataReader _reader;
	}
}
