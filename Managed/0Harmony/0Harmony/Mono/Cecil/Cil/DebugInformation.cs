using System;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002DA RID: 730
	public abstract class DebugInformation : ICustomDebugInformationProvider, IMetadataTokenProvider
	{
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x0003C93B File Offset: 0x0003AB3B
		// (set) Token: 0x06001296 RID: 4758 RVA: 0x0003C943 File Offset: 0x0003AB43
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

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x0003C94C File Offset: 0x0003AB4C
		public bool HasCustomDebugInformations
		{
			get
			{
				return !this.custom_infos.IsNullOrEmpty<CustomDebugInformation>();
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x0003C95C File Offset: 0x0003AB5C
		public Collection<CustomDebugInformation> CustomDebugInformations
		{
			get
			{
				if (this.custom_infos == null)
				{
					Interlocked.CompareExchange<Collection<CustomDebugInformation>>(ref this.custom_infos, new Collection<CustomDebugInformation>(), null);
				}
				return this.custom_infos;
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00002AED File Offset: 0x00000CED
		internal DebugInformation()
		{
		}

		// Token: 0x04000969 RID: 2409
		internal MetadataToken token;

		// Token: 0x0400096A RID: 2410
		internal Collection<CustomDebugInformation> custom_infos;
	}
}
