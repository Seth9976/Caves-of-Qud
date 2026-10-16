using System;
using Unity;

namespace System.Web.Hosting
{
	/// <summary>Represents a process host.</summary>
	// Token: 0x0200076B RID: 1899
	public sealed class ProcessHost : MarshalByRefObject, IAdphManager, IApplicationPreloadManager, IPphManager, IProcessHost, IProcessHostIdleAndHealthCheck
	{
		// Token: 0x06004D1D RID: 19741 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal ProcessHost()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Provides access to an enumerable set of application domains. </summary>
		/// <param name="appDomainInfoEnum">Information about the application domains.</param>
		// Token: 0x06004D1E RID: 19742 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void EnumerateAppDomains(out IAppDomainInfoEnum appDomainInfoEnum)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the state of a process.</summary>
		/// <returns>true if the process host is idle; otherwise, false.</returns>
		// Token: 0x06004D1F RID: 19743 RVA: 0x000C8980 File Offset: 0x000C6B80
		public bool IsIdle()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Pings a process.</summary>
		/// <param name="callback">The callback to handle the ping response.</param>
		// Token: 0x06004D20 RID: 19744 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Ping(IProcessPingCallback callback)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Notifies ASP.NET that a particular application that is running on IIS 7.0 is configured to be preloaded.</summary>
		/// <param name="context">The application context. </param>
		/// <param name="appId">The unique ID of the application.</param>
		/// <param name="enabled">true to indicate the application should be preloaded; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Web.Hosting.IApplicationPreloadUtil" /> instance that contains information for preloading the application is null or empty.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="appId" /> is null or empty. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enabled" /> is set to true and the process host was not previously passed an <see cref="T:System.Web.Hosting.IApplicationPreloadUtil" /> interface to its <see cref="M:System.Web.Hosting.IApplicationPreloadManager.SetApplicationPreloadUtil(System.Web.Hosting.IApplicationPreloadUtil)" /> method</exception>
		// Token: 0x06004D21 RID: 19745 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void SetApplicationPreloadState(string context, string appId, bool enabled)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Creates a type that calls IIS 7.0 to get information that is required in order to preload an application.</summary>
		/// <param name="applicationPreloadUtil">The handle to an unmanaged interface in IIS 7.0 that ASP.NET calls to get information. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.Hosting.IApplicationPreloadUtil" /> instance that contains information for preloading the application is not null. </exception>
		// Token: 0x06004D22 RID: 19746 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void SetApplicationPreloadUtil(IApplicationPreloadUtil applicationPreloadUtil)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Unloads the process host. </summary>
		// Token: 0x06004D23 RID: 19747 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Shutdown()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Shuts down the specified application.</summary>
		/// <param name="appId">The unique identifier of the application to shut down.</param>
		// Token: 0x06004D24 RID: 19748 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void ShutdownApplication(string appId)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Starts an application domain protocol listener channel.</summary>
		/// <param name="appId">The application ID.</param>
		/// <param name="protocolId">The protocol ID.</param>
		/// <param name="listenerChannelCallback">The protocol listener channel callback.</param>
		// Token: 0x06004D25 RID: 19749 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void StartAppDomainProtocolListenerChannel(string appId, string protocolId, IListenerChannelCallback listenerChannelCallback)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Starts the specified application.</summary>
		/// <param name="appId">The application ID.</param>
		/// <param name="appPath">The virtual path to the application.</param>
		/// <param name="runtimeInterface">A runtime manager interface.</param>
		// Token: 0x06004D26 RID: 19750 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void StartApplication(string appId, string appPath, out object runtimeInterface)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Starts a process protocol listener channel.</summary>
		/// <param name="protocolId">The protocol ID.</param>
		/// <param name="listenerChannelCallback">The protocol listener channel callback.</param>
		// Token: 0x06004D27 RID: 19751 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void StartProcessProtocolListenerChannel(string protocolId, IListenerChannelCallback listenerChannelCallback)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Stops an application domain protocol listener channel.</summary>
		/// <param name="appId">The application ID.</param>
		/// <param name="protocolId">The protocol ID.</param>
		/// <param name="immediate">Whether to stop the protocol immediately.</param>
		// Token: 0x06004D28 RID: 19752 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void StopAppDomainProtocol(string appId, string protocolId, bool immediate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Stops an application domain protocol listener channel.</summary>
		/// <param name="appId">The application ID</param>
		/// <param name="protocolId">The protocol ID.</param>
		/// <param name="listenerChannelId">The protocol listener channel ID.</param>
		/// <param name="immediate">Whether to stop the protocol listener channel immediately.</param>
		// Token: 0x06004D29 RID: 19753 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void StopAppDomainProtocolListenerChannel(string appId, string protocolId, int listenerChannelId, bool immediate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Stops a process protocol.</summary>
		/// <param name="protocolId">The protocol ID.</param>
		/// <param name="immediate">Whether to stop the protocol immediately.</param>
		// Token: 0x06004D2A RID: 19754 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void StopProcessProtocol(string protocolId, bool immediate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Stops a protocol listener channel.</summary>
		/// <param name="protocolId">The protocol ID.</param>
		/// <param name="listenerChannelId">The protocol listener channel callback.</param>
		/// <param name="immediate">Whether to stop the protocol listener channel immediately.</param>
		// Token: 0x06004D2B RID: 19755 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void StopProcessProtocolListenerChannel(string protocolId, int listenerChannelId, bool immediate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
