using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000712 RID: 1810
	internal interface INetworkChange : IDisposable
	{
		// Token: 0x1400006B RID: 107
		// (add) Token: 0x060039D3 RID: 14803
		// (remove) Token: 0x060039D4 RID: 14804
		event NetworkAddressChangedEventHandler NetworkAddressChanged;

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x060039D5 RID: 14805
		// (remove) Token: 0x060039D6 RID: 14806
		event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged;

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x060039D7 RID: 14807
		bool HasRegisteredEvents { get; }
	}
}
