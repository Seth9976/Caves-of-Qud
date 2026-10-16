using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a simple implementation of the <see cref="T:System.ComponentModel.Design.IServiceContainer" /> interface. This class cannot be inherited.</summary>
	// Token: 0x02000475 RID: 1141
	public class ServiceContainer : IServiceContainer, IServiceProvider, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> class.</summary>
		// Token: 0x060024A6 RID: 9382 RVA: 0x0000219B File Offset: 0x0000039B
		public ServiceContainer()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> class using the specified parent service provider.</summary>
		/// <param name="parentProvider">A parent service provider. </param>
		// Token: 0x060024A7 RID: 9383 RVA: 0x00081ADD File Offset: 0x0007FCDD
		public ServiceContainer(IServiceProvider parentProvider)
		{
			this._parentProvider = parentProvider;
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060024A8 RID: 9384 RVA: 0x00081AEC File Offset: 0x0007FCEC
		private IServiceContainer Container
		{
			get
			{
				IServiceContainer serviceContainer = null;
				if (this._parentProvider != null)
				{
					serviceContainer = (IServiceContainer)this._parentProvider.GetService(typeof(IServiceContainer));
				}
				return serviceContainer;
			}
		}

		/// <summary>Gets the default services implemented directly by <see cref="T:System.ComponentModel.Design.ServiceContainer" />.</summary>
		/// <returns>The default services.</returns>
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060024A9 RID: 9385 RVA: 0x00081B1F File Offset: 0x0007FD1F
		protected virtual Type[] DefaultServices
		{
			get
			{
				return ServiceContainer.s_defaultServices;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060024AA RID: 9386 RVA: 0x00081B28 File Offset: 0x0007FD28
		private ServiceContainer.ServiceCollection<object> Services
		{
			get
			{
				ServiceContainer.ServiceCollection<object> serviceCollection;
				if ((serviceCollection = this._services) == null)
				{
					serviceCollection = (this._services = new ServiceContainer.ServiceCollection<object>());
				}
				return serviceCollection;
			}
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="serviceInstance">An instance of the service to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="serviceInstance" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x060024AB RID: 9387 RVA: 0x00081B4D File Offset: 0x0007FD4D
		public void AddService(Type serviceType, object serviceInstance)
		{
			this.AddService(serviceType, serviceInstance, false);
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter. </param>
		/// <param name="promote">true if this service should be added to any parent service containers; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="serviceInstance" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x060024AC RID: 9388 RVA: 0x00081B58 File Offset: 0x0007FD58
		public virtual void AddService(Type serviceType, object serviceInstance, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.AddService(serviceType, serviceInstance, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (serviceInstance == null)
			{
				throw new ArgumentNullException("serviceInstance");
			}
			if (!(serviceInstance is ServiceCreatorCallback) && !serviceInstance.GetType().IsCOMObject && !serviceType.IsInstanceOfType(serviceInstance))
			{
				throw new ArgumentException(SR.Format("The service instance must derive from or implement {0}.", serviceType.FullName));
			}
			if (this.Services.ContainsKey(serviceType))
			{
				throw new ArgumentException(SR.Format("The service {0} already exists in the service container.", serviceType.FullName), "serviceType");
			}
			this.Services[serviceType] = serviceInstance;
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="callback">A callback object that can create the service. This allows a service to be declared as available, but delays creation of the object until the service is requested. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="callback" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x060024AD RID: 9389 RVA: 0x00081C08 File Offset: 0x0007FE08
		public void AddService(Type serviceType, ServiceCreatorCallback callback)
		{
			this.AddService(serviceType, callback, false);
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="callback">A callback object that can create the service. This allows a service to be declared as available, but delays creation of the object until the service is requested. </param>
		/// <param name="promote">true if this service should be added to any parent service containers; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="callback" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x060024AE RID: 9390 RVA: 0x00081C14 File Offset: 0x0007FE14
		public virtual void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.AddService(serviceType, callback, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (this.Services.ContainsKey(serviceType))
			{
				throw new ArgumentException(SR.Format("The service {0} already exists in the service container.", serviceType.FullName), "serviceType");
			}
			this.Services[serviceType] = callback;
		}

		/// <summary>Disposes this service container.</summary>
		// Token: 0x060024AF RID: 9391 RVA: 0x00081C90 File Offset: 0x0007FE90
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Disposes this service container.</summary>
		/// <param name="disposing">true if the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> is in the process of being disposed of; otherwise, false.</param>
		// Token: 0x060024B0 RID: 9392 RVA: 0x00081C9C File Offset: 0x0007FE9C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				ServiceContainer.ServiceCollection<object> services = this._services;
				this._services = null;
				if (services != null)
				{
					foreach (object obj in services.Values)
					{
						if (obj is IDisposable)
						{
							((IDisposable)obj).Dispose();
						}
					}
				}
			}
		}

		/// <summary>Gets the requested service.</summary>
		/// <returns>An instance of the service if it could be found, or null if it could not be found.</returns>
		/// <param name="serviceType">The type of service to retrieve. </param>
		// Token: 0x060024B1 RID: 9393 RVA: 0x00081D10 File Offset: 0x0007FF10
		public virtual object GetService(Type serviceType)
		{
			object obj = null;
			Type[] defaultServices = this.DefaultServices;
			for (int i = 0; i < defaultServices.Length; i++)
			{
				if (serviceType.IsEquivalentTo(defaultServices[i]))
				{
					obj = this;
					break;
				}
			}
			if (obj == null)
			{
				this.Services.TryGetValue(serviceType, out obj);
			}
			if (obj is ServiceCreatorCallback)
			{
				obj = ((ServiceCreatorCallback)obj)(this, serviceType);
				if (obj != null && !obj.GetType().IsCOMObject && !serviceType.IsInstanceOfType(obj))
				{
					obj = null;
				}
				this.Services[serviceType] = obj;
			}
			if (obj == null && this._parentProvider != null)
			{
				obj = this._parentProvider.GetService(serviceType);
			}
			return obj;
		}

		/// <summary>Removes the specified service type from the service container.</summary>
		/// <param name="serviceType">The type of service to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> is null.</exception>
		// Token: 0x060024B2 RID: 9394 RVA: 0x00081DAC File Offset: 0x0007FFAC
		public void RemoveService(Type serviceType)
		{
			this.RemoveService(serviceType, false);
		}

		/// <summary>Removes the specified service type from the service container.</summary>
		/// <param name="serviceType">The type of service to remove. </param>
		/// <param name="promote">true if this service should be removed from any parent service containers; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> is null.</exception>
		// Token: 0x060024B3 RID: 9395 RVA: 0x00081DB8 File Offset: 0x0007FFB8
		public virtual void RemoveService(Type serviceType, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.RemoveService(serviceType, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			this.Services.Remove(serviceType);
		}

		// Token: 0x04001108 RID: 4360
		private ServiceContainer.ServiceCollection<object> _services;

		// Token: 0x04001109 RID: 4361
		private IServiceProvider _parentProvider;

		// Token: 0x0400110A RID: 4362
		private static Type[] s_defaultServices = new Type[]
		{
			typeof(IServiceContainer),
			typeof(ServiceContainer)
		};

		// Token: 0x0400110B RID: 4363
		private static TraceSwitch s_TRACESERVICE = new TraceSwitch("TRACESERVICE", "ServiceProvider: Trace service provider requests.");

		// Token: 0x02000476 RID: 1142
		private sealed class ServiceCollection<T> : Dictionary<Type, T>
		{
			// Token: 0x060024B5 RID: 9397 RVA: 0x00081E37 File Offset: 0x00080037
			public ServiceCollection()
				: base(ServiceContainer.ServiceCollection<T>.s_serviceTypeComparer)
			{
			}

			// Token: 0x0400110C RID: 4364
			private static ServiceContainer.ServiceCollection<T>.EmbeddedTypeAwareTypeComparer s_serviceTypeComparer = new ServiceContainer.ServiceCollection<T>.EmbeddedTypeAwareTypeComparer();

			// Token: 0x02000477 RID: 1143
			private sealed class EmbeddedTypeAwareTypeComparer : IEqualityComparer<Type>
			{
				// Token: 0x060024B7 RID: 9399 RVA: 0x00081E50 File Offset: 0x00080050
				public bool Equals(Type x, Type y)
				{
					return x.IsEquivalentTo(y);
				}

				// Token: 0x060024B8 RID: 9400 RVA: 0x00081E59 File Offset: 0x00080059
				public int GetHashCode(Type obj)
				{
					return obj.FullName.GetHashCode();
				}
			}
		}
	}
}
