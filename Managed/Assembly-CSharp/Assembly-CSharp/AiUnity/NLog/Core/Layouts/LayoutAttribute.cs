using System;
using System.ComponentModel;

namespace AiUnity.NLog.Core.Layouts
{
	// Token: 0x0200023E RID: 574
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class LayoutAttribute : DisplayNameAttribute
	{
		// Token: 0x0600151F RID: 5407 RVA: 0x000754C7 File Offset: 0x000736C7
		public LayoutAttribute(string name)
			: base(name)
		{
		}
	}
}
