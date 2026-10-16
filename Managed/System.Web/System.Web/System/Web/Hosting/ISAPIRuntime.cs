using System;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	/// <summary>Manages <see cref="T:System.Web.HttpWorkerRequest" /> objects in the .NET Framework. This class cannot be inherited.</summary>
	// Token: 0x02000550 RID: 1360
	public sealed class ISAPIRuntime : MarshalByRefObject, IISAPIRuntime, IRegisteredObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.ISAPIRuntime" /> class. </summary>
		// Token: 0x06003AC6 RID: 15046 RVA: 0x00007A37 File Offset: 0x00005C37
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public ISAPIRuntime()
		{
		}

		/// <summary>Forces garbage collection.</summary>
		// Token: 0x06003AC7 RID: 15047 RVA: 0x00003946 File Offset: 0x00001B46
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public void DoGCCollect()
		{
		}

		/// <summary>Creates a new <see cref="T:System.Web.HttpWorkerRequest" /> object to process the current request.</summary>
		/// <returns>0 if <see cref="T:System.Web.HttpWorkerRequest" /> was created successfully; otherwise, 1.</returns>
		/// <param name="ecb">An ISAPI extension control block.</param>
		/// <param name="iWRType">0 to create an out-of-process request; otherwise, an in-process request is created.</param>
		// Token: 0x06003AC8 RID: 15048 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public int ProcessRequest(IntPtr ecb, int iWRType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Starts processing all items in the worker process pipeline.</summary>
		// Token: 0x06003AC9 RID: 15049 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public void StartProcessing()
		{
			throw new NotImplementedException();
		}

		/// <summary>Stops processing the items in the worker process pipeline.</summary>
		// Token: 0x06003ACA RID: 15050 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public void StopProcessing()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gives the <see cref="T:System.Web.Hosting.ISAPIRuntime" /> object an infinite lifetime by preventing a lease from being created. </summary>
		/// <returns>null to prevent a lease from being created.</returns>
		// Token: 0x06003ACB RID: 15051 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public override object InitializeLifetimeService()
		{
			throw new NotImplementedException();
		}

		/// <summary>Requests a registered object to unregister.</summary>
		/// <param name="immediate">true to indicate that the registered object should unregister from the hosting environment before returning; otherwise, false.</param>
		// Token: 0x06003ACC RID: 15052 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		void IRegisteredObject.Stop(bool immediate)
		{
			throw new NotImplementedException();
		}
	}
}
