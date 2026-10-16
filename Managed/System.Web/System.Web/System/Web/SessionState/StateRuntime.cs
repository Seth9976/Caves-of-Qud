using System;
using System.Security.Permissions;

namespace System.Web.SessionState
{
	/// <summary>Manages session data stored in the ASP.NET state service. This class cannot be inherited.</summary>
	// Token: 0x020004A4 RID: 1188
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class StateRuntime : IStateRuntime
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SessionState.StateRuntime" /> class.</summary>
		// Token: 0x060035D9 RID: 13785 RVA: 0x00002050 File Offset: 0x00000250
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public StateRuntime()
		{
		}

		/// <summary>Used by the ASP.NET state server to process session data.</summary>
		/// <param name="tracker">An <see cref="T:System.IntPtr" /> pointer to an object stored in the unmanaged ASP.NET state server.</param>
		/// <param name="verb">The action to take on the object.</param>
		/// <param name="uri">An identifier for the session.</param>
		/// <param name="exclusive">The type of access to objects in the store.</param>
		/// <param name="timeout">The number of minutes the session data is stored.</param>
		/// <param name="lockCookieExists">A value that indicates whether the lock cookie exists in the original request from the ASP.NET Web server to the ASP.NET state server.</param>
		/// <param name="lockCookie">The owner of the lock on the session state.</param>
		/// <param name="contentLength">The length, in bytes, of the data stored for the session.</param>
		/// <param name="content">An <see cref="T:System.IntPtr" /> pointer to the content stored for the session in the unmanaged ASP.NET state server.</param>
		// Token: 0x060035DA RID: 13786 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		public void ProcessRequest(IntPtr tracker, int verb, string uri, int exclusive, int timeout, int lockCookieExists, int lockCookie, int contentLength, IntPtr content)
		{
			throw new NotImplementedException();
		}

		/// <summary>Used by the ASP.NET state server to process session data.</summary>
		/// <param name="tracker">An <see cref="T:System.IntPtr" /> pointer to an object stored in the unmanaged ASP.NET state server.</param>
		/// <param name="verb">The action to take on the object.</param>
		/// <param name="uri">An identifier for the session.</param>
		/// <param name="exclusive">The type of access to objects in the store.</param>
		/// <param name="extraFlags">A value that indicates whether the current session is an uninitialized, cookieless session.</param>
		/// <param name="timeout">The number of minutes the session data is stored.</param>
		/// <param name="lockCookieExists">A value that indicates whether the lock cookie exists in the original request from the ASP.NET Web server to the ASP.NET state server.</param>
		/// <param name="lockCookie">The owner of the lock on the session state.</param>
		/// <param name="contentLength">The length, in bytes, of the data stored for the session.</param>
		/// <param name="content">An <see cref="T:System.IntPtr" /> pointer to the content stored for the session in the unmanaged ASP.NET state server.</param>
		// Token: 0x060035DB RID: 13787 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		public void ProcessRequest(IntPtr tracker, int verb, string uri, int exclusive, int extraFlags, int timeout, int lockCookieExists, int lockCookie, int contentLength, IntPtr content)
		{
			throw new NotImplementedException();
		}

		/// <summary>Stops the processing of session data stored in the ASP.NET state server.</summary>
		// Token: 0x060035DC RID: 13788 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public void StopProcessing()
		{
			throw new NotImplementedException();
		}
	}
}
