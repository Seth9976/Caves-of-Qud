using System;

namespace System.Web.Util
{
	/// <summary>Provides the interface for implementing factories for Web objects.</summary>
	// Token: 0x0200011A RID: 282
	public interface IWebObjectFactory
	{
		/// <summary>Creates a new <see cref="T:System.Web.Util.IWebObjectFactory" /> instance.</summary>
		/// <returns>A new <see cref="T:System.Web.Util.IWebObjectFactory" /></returns>
		// Token: 0x06000DF7 RID: 3575
		object CreateInstance();
	}
}
