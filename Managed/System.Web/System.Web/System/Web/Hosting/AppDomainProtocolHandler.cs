using System;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	/// <summary>Provides support for programmatic access to application domain protocols.</summary>
	// Token: 0x0200052E RID: 1326
	public abstract class AppDomainProtocolHandler : MarshalByRefObject, IRegisteredObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.AppDomainProtocolHandler" /> class.</summary>
		// Token: 0x06003A18 RID: 14872 RVA: 0x0009AD61 File Offset: 0x00098F61
		protected AppDomainProtocolHandler()
		{
			HostingEnvironment.RegisterObject(this);
		}

		/// <summary>Gives the protocol handler an infinite lifetime by preventing a lease from being created.</summary>
		/// <returns>true if the service is initiated; otherwise, false.</returns>
		// Token: 0x06003A19 RID: 14873 RVA: 0x00003BF6 File Offset: 0x00001DF6
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		/// <summary>Starts a protocol listener channel.</summary>
		/// <param name="listenerChannelCallback">The callback for the listener channel.</param>
		// Token: 0x06003A1A RID: 14874
		public abstract void StartListenerChannel(IListenerChannelCallback listenerChannelCallback);

		/// <summary>Stops the specified process protocol handler.</summary>
		/// <param name="listenerChannelId">The callback for the listener channel.</param>
		/// <param name="immediate">true to stop the protocol immediately; otherwise, false.</param>
		// Token: 0x06003A1B RID: 14875
		public abstract void StopListenerChannel(int listenerChannelId, bool immediate);

		/// <summary>Stops a protocol.</summary>
		/// <param name="immediate">true to stop the protocol immediately.</param>
		// Token: 0x06003A1C RID: 14876
		public abstract void StopProtocol(bool immediate);

		/// <summary>Stops a queue.</summary>
		/// <param name="immediate">true to stop the queue immediately.</param>
		// Token: 0x06003A1D RID: 14877 RVA: 0x0009AD6F File Offset: 0x00098F6F
		public virtual void Stop(bool immediate)
		{
			this.StopProtocol(true);
			HostingEnvironment.UnregisterObject(this);
		}
	}
}
