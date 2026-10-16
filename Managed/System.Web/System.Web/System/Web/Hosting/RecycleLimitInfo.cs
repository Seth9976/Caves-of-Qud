using System;
using Unity;

namespace System.Web.Hosting
{
	// Token: 0x0200075F RID: 1887
	public sealed class RecycleLimitInfo
	{
		// Token: 0x06004D00 RID: 19712 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public RecycleLimitInfo(long currentPrivateBytes, long recycleLimit, RecycleLimitNotificationFrequency recycleLimitNearFrequency)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x170017BC RID: 6076
		// (get) Token: 0x06004D01 RID: 19713 RVA: 0x000C88F4 File Offset: 0x000C6AF4
		public long CurrentPrivateBytes
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		// Token: 0x170017BD RID: 6077
		// (get) Token: 0x06004D02 RID: 19714 RVA: 0x000C8910 File Offset: 0x000C6B10
		public long RecycleLimit
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x06004D03 RID: 19715 RVA: 0x000C892C File Offset: 0x000C6B2C
		// (set) Token: 0x06004D04 RID: 19716 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool RequestGC
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x06004D05 RID: 19717 RVA: 0x000C8948 File Offset: 0x000C6B48
		public RecycleLimitNotificationFrequency TrimFrequency
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return RecycleLimitNotificationFrequency.High;
			}
		}
	}
}
