using System;

namespace NUnit.Framework
{
	// Token: 0x02000020 RID: 32
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class LevelOfParallelismAttribute : PropertyAttribute
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00003F99 File Offset: 0x00002199
		public LevelOfParallelismAttribute(int level)
			: base(level)
		{
		}
	}
}
