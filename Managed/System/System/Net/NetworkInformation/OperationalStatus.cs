using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the operational state of a network interface.</summary>
	// Token: 0x020006FD RID: 1789
	public enum OperationalStatus
	{
		/// <summary>The network interface is up; it can transmit data packets.</summary>
		// Token: 0x040021BF RID: 8639
		Up = 1,
		/// <summary>The network interface is unable to transmit data packets.</summary>
		// Token: 0x040021C0 RID: 8640
		Down,
		/// <summary>The network interface is running tests.</summary>
		// Token: 0x040021C1 RID: 8641
		Testing,
		/// <summary>The network interface status is not known.</summary>
		// Token: 0x040021C2 RID: 8642
		Unknown,
		/// <summary>The network interface is not in a condition to transmit data packets; it is waiting for an external event.</summary>
		// Token: 0x040021C3 RID: 8643
		Dormant,
		/// <summary>The network interface is unable to transmit data packets because of a missing component, typically a hardware component.</summary>
		// Token: 0x040021C4 RID: 8644
		NotPresent,
		/// <summary>The network interface is unable to transmit data packets because it runs on top of one or more other interfaces, and at least one of these "lower layer" interfaces is down.</summary>
		// Token: 0x040021C5 RID: 8645
		LowerLayerDown
	}
}
