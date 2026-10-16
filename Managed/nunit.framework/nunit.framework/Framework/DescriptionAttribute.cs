using System;

namespace NUnit.Framework
{
	// Token: 0x0200001A RID: 26
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class DescriptionAttribute : PropertyAttribute
	{
		// Token: 0x0600013B RID: 315 RVA: 0x00003DF1 File Offset: 0x00001FF1
		public DescriptionAttribute(string description)
			: base("Description", description)
		{
		}
	}
}
