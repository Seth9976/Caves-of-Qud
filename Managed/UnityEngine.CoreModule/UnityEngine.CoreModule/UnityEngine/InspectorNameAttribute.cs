using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000231 RID: 561
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	[UsedByNativeCode]
	public class InspectorNameAttribute : PropertyAttribute
	{
		// Token: 0x06001E5F RID: 7775 RVA: 0x0003A814 File Offset: 0x00038A14
		public InspectorNameAttribute(string displayName)
			: base(true)
		{
			this.displayName = displayName;
		}

		// Token: 0x04000900 RID: 2304
		public readonly string displayName;
	}
}
