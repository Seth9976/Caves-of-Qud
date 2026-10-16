using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Manages an application domain protocol handler</summary>
	// Token: 0x02000763 RID: 1891
	public interface IAdphManager
	{
		/// <summary>Starts an application domain protocol listener channel.</summary>
		/// <param name="appId">The application ID</param>
		/// <param name="protocolId">The protocol ID.</param>
		/// <param name="listenerChannelCallback">The protocol listener channel callback.</param>
		// Token: 0x06004D08 RID: 19720
		void StartAppDomainProtocolListenerChannel([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, IListenerChannelCallback listenerChannelCallback);

		/// <summary>Stops an application domain protocol listener channel.</summary>
		/// <param name="appId">The application ID</param>
		/// <param name="protocolId">The protocol ID.</param>
		/// <param name="immediate">The protocol listener channel callback.</param>
		// Token: 0x06004D09 RID: 19721
		void StopAppDomainProtocol([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, bool immediate);

		/// <summary>Stops an application domain protocol listener channel.</summary>
		/// <param name="appId">The application ID</param>
		/// <param name="protocolId">The protocol ID.</param>
		/// <param name="listenerChannelId">The protocol listener channel callback.</param>
		/// <param name="immediate">Whether to stop the protocol listener channel immediately</param>
		// Token: 0x06004D0A RID: 19722
		void StopAppDomainProtocolListenerChannel([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, int listenerChannelId, bool immediate);
	}
}
