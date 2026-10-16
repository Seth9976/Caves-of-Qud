using System;

namespace Occult.Engine.CodeGeneration
{
	// Token: 0x020004B1 RID: 1201
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class GeneratePoolingPartial : Attribute
	{
		// Token: 0x04001A17 RID: 6679
		public string Reset;

		// Token: 0x04001A18 RID: 6680
		public string Pool;

		// Token: 0x04001A19 RID: 6681
		public Type PoolType;

		// Token: 0x04001A1A RID: 6682
		public int Capacity;
	}
}
