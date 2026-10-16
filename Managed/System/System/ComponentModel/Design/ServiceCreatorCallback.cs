using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a callback mechanism that can create an instance of a service on demand.</summary>
	/// <returns>The service specified by <paramref name="serviceType" />, or null if the service could not be created. </returns>
	/// <param name="container">The service container that requested the creation of the service. </param>
	/// <param name="serviceType">The type of service to create. </param>
	// Token: 0x02000474 RID: 1140
	// (Invoke) Token: 0x060024A3 RID: 9379
	public delegate object ServiceCreatorCallback(IServiceContainer container, Type serviceType);
}
