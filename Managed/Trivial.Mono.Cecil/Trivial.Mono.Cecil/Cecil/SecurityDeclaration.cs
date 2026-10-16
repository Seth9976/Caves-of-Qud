using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000B1 RID: 177
	public sealed class SecurityDeclaration
	{
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x00015CC0 File Offset: 0x00013EC0
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x00015CC8 File Offset: 0x00013EC8
		public SecurityAction Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00015CD1 File Offset: 0x00013ED1
		public bool HasSecurityAttributes
		{
			get
			{
				this.Resolve();
				return !this.security_attributes.IsNullOrEmpty<SecurityAttribute>();
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00015CE8 File Offset: 0x00013EE8
		public Collection<SecurityAttribute> SecurityAttributes
		{
			get
			{
				this.Resolve();
				Collection<SecurityAttribute> collection;
				if ((collection = this.security_attributes) == null)
				{
					collection = (this.security_attributes = new Collection<SecurityAttribute>());
				}
				return collection;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00015D13 File Offset: 0x00013F13
		internal bool HasImage
		{
			get
			{
				return this.module != null && this.module.HasImage;
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00015D2A File Offset: 0x00013F2A
		internal SecurityDeclaration(SecurityAction action, uint signature, ModuleDefinition module)
		{
			this.action = action;
			this.signature = signature;
			this.module = module;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00015D47 File Offset: 0x00013F47
		public SecurityDeclaration(SecurityAction action)
		{
			this.action = action;
			this.resolved = true;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00015D5D File Offset: 0x00013F5D
		public SecurityDeclaration(SecurityAction action, byte[] blob)
		{
			this.action = action;
			this.resolved = false;
			this.blob = blob;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00015D7C File Offset: 0x00013F7C
		public byte[] GetBlob()
		{
			if (this.blob != null)
			{
				return this.blob;
			}
			if (!this.HasImage || this.signature == 0U)
			{
				throw new NotSupportedException();
			}
			return this.blob = this.module.Read<SecurityDeclaration, byte[]>(this, (SecurityDeclaration declaration, MetadataReader reader) => reader.ReadSecurityDeclarationBlob(declaration.signature));
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00015DE2 File Offset: 0x00013FE2
		private void Resolve()
		{
			if (!this.resolved && this.HasImage)
			{
				this.module.Read<SecurityDeclaration, SecurityDeclaration>(this, delegate(SecurityDeclaration declaration, MetadataReader reader)
				{
					reader.ReadSecurityDeclarationSignature(declaration);
					return this;
				});
				this.resolved = true;
			}
		}

		// Token: 0x0400026E RID: 622
		internal readonly uint signature;

		// Token: 0x0400026F RID: 623
		private byte[] blob;

		// Token: 0x04000270 RID: 624
		private readonly ModuleDefinition module;

		// Token: 0x04000271 RID: 625
		internal bool resolved;

		// Token: 0x04000272 RID: 626
		private SecurityAction action;

		// Token: 0x04000273 RID: 627
		internal Collection<SecurityAttribute> security_attributes;
	}
}
