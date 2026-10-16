using System;
using System.Runtime.InteropServices;

namespace System.Configuration
{
	/// <summary>Provides standard configuration methods.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B5 RID: 437
	[ComVisible(false)]
	public interface IConfigurationSystem
	{
		/// <summary>Gets the specified configuration.</summary>
		/// <returns>The object representing the configuration.</returns>
		/// <param name="configKey">The configuration key.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B86 RID: 2950
		object GetConfig(string configKey);

		/// <summary>Used for initialization.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B87 RID: 2951
		void Init();
	}
}
