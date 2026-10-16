using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x020000FC RID: 252
	public abstract class DebugInformation : ICustomDebugInformationProvider, IMetadataTokenProvider
	{
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00022452 File Offset: 0x00020652
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x0002245A File Offset: 0x0002065A
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

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x00022463 File Offset: 0x00020663
		public bool HasCustomDebugInformations
		{
			get
			{
				return !this.custom_infos.IsNullOrEmpty<CustomDebugInformation>();
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x00022474 File Offset: 0x00020674
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

		// Token: 0x06000A19 RID: 2585 RVA: 0x00002F5F File Offset: 0x0000115F
		internal DebugInformation()
		{
		}

		// Token: 0x04000514 RID: 1300
		internal MetadataToken token;

		// Token: 0x04000515 RID: 1301
		internal Collection<CustomDebugInformation> custom_infos;
	}
}
