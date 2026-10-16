using System;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x02000606 RID: 1542
	internal class CachedTransportContext : TransportContext
	{
		// Token: 0x060030B2 RID: 12466 RVA: 0x000A76C1 File Offset: 0x000A58C1
		internal CachedTransportContext(ChannelBinding binding)
		{
			this.binding = binding;
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000A76D0 File Offset: 0x000A58D0
		public override ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			if (kind != ChannelBindingKind.Endpoint)
			{
				return null;
			}
			return this.binding;
		}

		// Token: 0x04001C5B RID: 7259
		private ChannelBinding binding;
	}
}
