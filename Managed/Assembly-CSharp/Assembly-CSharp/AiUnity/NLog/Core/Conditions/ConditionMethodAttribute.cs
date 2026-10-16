using System;
using System.ComponentModel;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x020002A6 RID: 678
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class ConditionMethodAttribute : DisplayNameAttribute
	{
		// Token: 0x060017ED RID: 6125 RVA: 0x0007D2FC File Offset: 0x0007B4FC
		public ConditionMethodAttribute(string name)
			: base(name)
		{
		}
	}
}
