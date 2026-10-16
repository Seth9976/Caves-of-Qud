using System;

namespace System.Reflection.Metadata
{
	// Token: 0x020000C5 RID: 197
	public readonly struct ImportDefinition
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x0001211E File Offset: 0x0001031E
		public ImportDefinitionKind Kind { get; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x00012126 File Offset: 0x00010326
		public BlobHandle Alias { get; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0001212E File Offset: 0x0001032E
		public AssemblyReferenceHandle TargetAssembly { get; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x00012136 File Offset: 0x00010336
		public BlobHandle TargetNamespace
		{
			get
			{
				return (BlobHandle)this._typeOrNamespace;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00012143 File Offset: 0x00010343
		public EntityHandle TargetType
		{
			get
			{
				return (EntityHandle)this._typeOrNamespace;
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00012150 File Offset: 0x00010350
		internal ImportDefinition(ImportDefinitionKind kind, BlobHandle alias = default(BlobHandle), AssemblyReferenceHandle assembly = default(AssemblyReferenceHandle), Handle typeOrNamespace = default(Handle))
		{
			this.Kind = kind;
			this.Alias = alias;
			this.TargetAssembly = assembly;
			this._typeOrNamespace = typeOrNamespace;
		}

		// Token: 0x04000471 RID: 1137
		private readonly Handle _typeOrNamespace;
	}
}
