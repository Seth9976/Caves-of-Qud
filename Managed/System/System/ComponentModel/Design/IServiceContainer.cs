using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a container for services.</summary>
	// Token: 0x0200046D RID: 1133
	public interface IServiceContainer : IServiceProvider
	{
		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter. </param>
		// Token: 0x0600247C RID: 9340
		void AddService(Type serviceType, object serviceInstance);

		/// <summary>Adds the specified service to the service container, and optionally promotes the service to any parent service containers.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter. </param>
		/// <param name="promote">true to promote this request to any parent service containers; otherwise, false. </param>
		// Token: 0x0600247D RID: 9341
		void AddService(Type serviceType, object serviceInstance, bool promote);

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="callback">A callback object that is used to create the service. This allows a service to be declared as available, but delays the creation of the object until the service is requested. </param>
		// Token: 0x0600247E RID: 9342
		void AddService(Type serviceType, ServiceCreatorCallback callback);

		/// <summary>Adds the specified service to the service container, and optionally promotes the service to parent service containers.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="callback">A callback object that is used to create the service. This allows a service to be declared as available, but delays the creation of the object until the service is requested. </param>
		/// <param name="promote">true to promote this request to any parent service containers; otherwise, false. </param>
		// Token: 0x0600247F RID: 9343
		void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote);

		/// <summary>Removes the specified service type from the service container.</summary>
		/// <param name="serviceType">The type of service to remove. </param>
		// Token: 0x06002480 RID: 9344
		void RemoveService(Type serviceType);

		/// <summary>Removes the specified service type from the service container, and optionally promotes the service to parent service containers.</summary>
		/// <param name="serviceType">The type of service to remove. </param>
		/// <param name="promote">true to promote this request to any parent service containers; otherwise, false. </param>
		// Token: 0x06002481 RID: 9345
		void RemoveService(Type serviceType, bool promote);
	}
}
