using System;
using System.ComponentModel;

namespace AiUnity.NLog.Core.Time
{
	// Token: 0x02000217 RID: 535
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class TimeSourceAttribute : DisplayNameAttribute
	{
		// Token: 0x06001381 RID: 4993 RVA: 0x00070713 File Offset: 0x0006E913
		public TimeSourceAttribute(string name)
			: base(name)
		{
		}
	}
}
