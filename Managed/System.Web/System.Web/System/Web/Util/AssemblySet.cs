using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x02000124 RID: 292
	internal class AssemblySet : ObjectSet
	{
		// Token: 0x06000E11 RID: 3601 RVA: 0x000259C7 File Offset: 0x00023BC7
		internal AssemblySet()
		{
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x000259D7 File Offset: 0x00023BD7
		internal static AssemblySet Create(ICollection c)
		{
			AssemblySet assemblySet = new AssemblySet();
			assemblySet.AddCollection(c);
			return assemblySet;
		}
	}
}
