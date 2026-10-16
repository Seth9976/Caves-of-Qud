using System;
using Unity;

namespace System.Web.Hosting
{
	/// <summary>Provides support for protocol handlers.</summary>
	// Token: 0x0200076D RID: 1901
	public abstract class ProcessProtocolHandler : MarshalByRefObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.ProcessProtocolHandler" /> class.</summary>
		// Token: 0x06004D2E RID: 19758 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected ProcessProtocolHandler()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Starts a protocol listener channel.</summary>
		/// <param name="listenerChannelCallback">The callback for the listener channel.</param>
		/// <param name="AdphManager">The application domain handler manager that is associated with the listener channel.</param>
		// Token: 0x06004D2F RID: 19759
		public abstract void StartListenerChannel(IListenerChannelCallback listenerChannelCallback, IAdphManager AdphManager);

		/// <summary>Stops the specified process protocol handler.</summary>
		/// <param name="listenerChannelId">The callback for the listener channel.</param>
		/// <param name="immediate">true to stop the protocol immediately; otherwise, false.</param>
		// Token: 0x06004D30 RID: 19760
		public abstract void StopListenerChannel(int listenerChannelId, bool immediate);

		/// <summary>Stops a process protocol handler.</summary>
		/// <param name="immediate">true to stop the protocol immediately; otherwise, false.</param>
		// Token: 0x06004D31 RID: 19761
		public abstract void StopProtocol(bool immediate);
	}
}
