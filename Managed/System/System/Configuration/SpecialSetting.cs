using System;

namespace System.Configuration
{
	/// <summary>Specifies the special setting category of a application settings property.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DD RID: 477
	public enum SpecialSetting
	{
		/// <summary>The configuration property represents a connection string, typically for a data store or network resource. </summary>
		// Token: 0x040007DC RID: 2012
		ConnectionString,
		/// <summary>The configuration property represents a Uniform Resource Locator (URL) to a Web service.</summary>
		// Token: 0x040007DD RID: 2013
		WebServiceUrl
	}
}
