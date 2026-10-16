using System;
using System.ComponentModel;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Filters
{
	// Token: 0x02000285 RID: 645
	[AttributeUsage(AttributeTargets.Class)]
	[Preserve]
	public sealed class FilterAttribute : DisplayNameAttribute
	{
		// Token: 0x06001710 RID: 5904 RVA: 0x0007A089 File Offset: 0x00078289
		public FilterAttribute(string name)
			: base(name)
		{
		}
	}
}
