using System;
using System.Collections.ObjectModel;
using Unity;

namespace System.Web.UI.Design.Directives
{
	/// <summary>Provides support when creating directive IntelliSense at design time.</summary>
	// Token: 0x020001CE RID: 462
	public static class DirectiveRegistry
	{
		/// <summary>Returns a collection of directives based on the <paramref name="frameworkVersion" /> parameter and the <paramref name="extension" /> parameter.</summary>
		/// <returns>A read only collection.</returns>
		/// <param name="frameworkVersion">The .NET Framework version.</param>
		/// <param name="extension">The file name extension.</param>
		// Token: 0x06000BE8 RID: 3048 RVA: 0x000168F3 File Offset: 0x00014AF3
		public static ReadOnlyCollection<Type> GetDirectives(Version frameworkVersion, string extension)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
