using System;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000263 RID: 611
	public sealed class SecurityDeclaration
	{
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0002E1A7 File Offset: 0x0002C3A7
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x0002E1AF File Offset: 0x0002C3AF
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

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x0002E1B8 File Offset: 0x0002C3B8
		public bool HasSecurityAttributes
		{
			get
			{
				this.Resolve();
				return !this.security_attributes.IsNullOrEmpty<SecurityAttribute>();
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0002E1CE File Offset: 0x0002C3CE
		public Collection<SecurityAttribute> SecurityAttributes
		{
			get
			{
				this.Resolve();
				if (this.security_attributes == null)
				{
					Interlocked.CompareExchange<Collection<SecurityAttribute>>(ref this.security_attributes, new Collection<SecurityAttribute>(), null);
				}
				return this.security_attributes;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0002E1F6 File Offset: 0x0002C3F6
		internal bool HasImage
		{
			get
			{
				return this.module != null && this.module.HasImage;
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0002E20D File Offset: 0x0002C40D
		internal SecurityDeclaration(SecurityAction action, uint signature, ModuleDefinition module)
		{
			this.action = action;
			this.signature = signature;
			this.module = module;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0002E22A File Offset: 0x0002C42A
		public SecurityDeclaration(SecurityAction action)
		{
			this.action = action;
			this.resolved = true;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0002E240 File Offset: 0x0002C440
		public SecurityDeclaration(SecurityAction action, byte[] blob)
		{
			this.action = action;
			this.resolved = false;
			this.blob = blob;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0002E260 File Offset: 0x0002C460
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
			return this.module.Read<SecurityDeclaration, byte[]>(ref this.blob, this, (SecurityDeclaration declaration, MetadataReader reader) => reader.ReadSecurityDeclarationBlob(declaration.signature));
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0002E2C4 File Offset: 0x0002C4C4
		private void Resolve()
		{
			if (this.resolved || !this.HasImage)
			{
				return;
			}
			object syncRoot = this.module.SyncRoot;
			lock (syncRoot)
			{
				if (!this.resolved)
				{
					this.module.Read<SecurityDeclaration>(this, delegate(SecurityDeclaration declaration, MetadataReader reader)
					{
						reader.ReadSecurityDeclarationSignature(declaration);
					});
					this.resolved = true;
				}
			}
		}

		// Token: 0x040004D9 RID: 1241
		internal readonly uint signature;

		// Token: 0x040004DA RID: 1242
		private byte[] blob;

		// Token: 0x040004DB RID: 1243
		private readonly ModuleDefinition module;

		// Token: 0x040004DC RID: 1244
		internal bool resolved;

		// Token: 0x040004DD RID: 1245
		private SecurityAction action;

		// Token: 0x040004DE RID: 1246
		internal Collection<SecurityAttribute> security_attributes;
	}
}
