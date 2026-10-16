using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace System.Resources
{
	// Token: 0x02000866 RID: 2150
	internal interface IResourceGroveler
	{
		// Token: 0x06004774 RID: 18292
		ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists, ref StackCrawlMark stackMark);

		// Token: 0x06004775 RID: 18293
		bool HasNeutralResources(CultureInfo culture, string defaultResName);
	}
}
