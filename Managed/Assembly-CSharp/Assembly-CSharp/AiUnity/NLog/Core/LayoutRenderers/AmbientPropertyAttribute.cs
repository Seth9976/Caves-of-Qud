using System;
using System.ComponentModel;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x02000243 RID: 579
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	[Preserve]
	public sealed class AmbientPropertyAttribute : DisplayNameAttribute
	{
		// Token: 0x06001548 RID: 5448 RVA: 0x00075FF8 File Offset: 0x000741F8
		public AmbientPropertyAttribute(string name)
			: base(name)
		{
		}
	}
}
