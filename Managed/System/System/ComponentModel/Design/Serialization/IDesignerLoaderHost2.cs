using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides an interface that extends <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> to specify whether errors are tolerated while loading a design document.</summary>
	// Token: 0x02000489 RID: 1161
	public interface IDesignerLoaderHost2 : IDesignerLoaderHost, IDesignerHost, IServiceContainer, IServiceProvider
	{
		/// <summary>Gets or sets a value indicating whether errors should be ignored when <see cref="M:System.ComponentModel.Design.Serialization.IDesignerLoaderHost.Reload" /> is called.</summary>
		/// <returns>true if the designer loader will ignore errors when it reloads; otherwise, false. The default is false.</returns>
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x0600251F RID: 9503
		// (set) Token: 0x06002520 RID: 9504
		bool IgnoreErrorsDuringReload { get; set; }

		/// <summary>Gets or sets a value indicating whether it is possible to reload with errors. </summary>
		/// <returns>true if the designer loader can reload the design document when errors are detected; otherwise, false. The default is false.</returns>
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002521 RID: 9505
		// (set) Token: 0x06002522 RID: 9506
		bool CanReloadWithErrors { get; set; }
	}
}
