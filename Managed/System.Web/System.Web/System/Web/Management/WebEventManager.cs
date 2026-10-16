using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides functionality for managing Web events in an application. This class cannot be inherited.</summary>
	// Token: 0x02000753 RID: 1875
	public static class WebEventManager
	{
		/// <summary>Flushes the event buffer for all providers that are in the healthMonitoring section.</summary>
		// Token: 0x06004CC3 RID: 19651 RVA: 0x0000B3D8 File Offset: 0x000095D8
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static void Flush()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Flushes the event buffer for the specified provider.</summary>
		/// <param name="providerName">The name of the provider.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="providerName" /> does not match any of the existing providers.</exception>
		// Token: 0x06004CC4 RID: 19652 RVA: 0x0000B3D8 File Offset: 0x000095D8
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static void Flush(string providerName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
