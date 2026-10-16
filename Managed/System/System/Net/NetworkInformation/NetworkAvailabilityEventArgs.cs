using System;
using Unity;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides data for the <see cref="E:System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged" /> event.</summary>
	// Token: 0x020006F4 RID: 1780
	public class NetworkAvailabilityEventArgs : EventArgs
	{
		// Token: 0x06003940 RID: 14656 RVA: 0x000C833C File Offset: 0x000C653C
		internal NetworkAvailabilityEventArgs(bool isAvailable)
		{
			this.isAvailable = isAvailable;
		}

		/// <summary>Gets the current status of the network connection.</summary>
		/// <returns>true if the network is available; otherwise, false.</returns>
		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06003941 RID: 14657 RVA: 0x000C834B File Offset: 0x000C654B
		public bool IsAvailable
		{
			get
			{
				return this.isAvailable;
			}
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal NetworkAvailabilityEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040021B2 RID: 8626
		private bool isAvailable;
	}
}
