using System;

namespace Mono.Cecil
{
	// Token: 0x0200024F RID: 591
	public class ModuleReference : IMetadataScope, IMetadataTokenProvider
	{
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x0002D490 File Offset: 0x0002B690
		// (set) Token: 0x06000E26 RID: 3622 RVA: 0x0002D498 File Offset: 0x0002B698
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

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x00018941 File Offset: 0x00016B41
		public virtual MetadataScopeType MetadataScopeType
		{
			get
			{
				return MetadataScopeType.ModuleReference;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0002D4A1 File Offset: 0x0002B6A1
		// (set) Token: 0x06000E29 RID: 3625 RVA: 0x0002D4A9 File Offset: 0x0002B6A9
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

		// Token: 0x06000E2A RID: 3626 RVA: 0x0002D4B2 File Offset: 0x0002B6B2
		internal ModuleReference()
		{
			this.token = new MetadataToken(TokenType.ModuleRef);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0002D4CA File Offset: 0x0002B6CA
		public ModuleReference(string name)
			: this()
		{
			this.name = name;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0002D490 File Offset: 0x0002B690
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x0400045D RID: 1117
		private string name;

		// Token: 0x0400045E RID: 1118
		internal MetadataToken token;
	}
}
