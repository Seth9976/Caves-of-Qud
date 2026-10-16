using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000090 RID: 144
	public class ModuleReference : IMetadataScope, IMetadataTokenProvider
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x000140C0 File Offset: 0x000122C0
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x000140C8 File Offset: 0x000122C8
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public virtual MetadataScopeType MetadataScopeType
		{
			get
			{
				return MetadataScopeType.ModuleReference;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x000140D1 File Offset: 0x000122D1
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x000140D9 File Offset: 0x000122D9
		public MetadataToken MetadataToken
		{
			get
			{
				return this.token;
			}
			set
			{
				this.token = value;
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000140E2 File Offset: 0x000122E2
		internal ModuleReference()
		{
			this.token = new MetadataToken(TokenType.ModuleRef);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x000140FA File Offset: 0x000122FA
		public ModuleReference(string name)
			: this()
		{
			this.name = name;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x000140C0 File Offset: 0x000122C0
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x040001DD RID: 477
		private string name;

		// Token: 0x040001DE RID: 478
		internal MetadataToken token;
	}
}
