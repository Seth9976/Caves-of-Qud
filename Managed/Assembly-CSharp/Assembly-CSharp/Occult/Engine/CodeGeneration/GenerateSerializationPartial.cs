using System;

namespace Occult.Engine.CodeGeneration
{
	// Token: 0x020004AE RID: 1198
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class GenerateSerializationPartial : Attribute
	{
		// Token: 0x04001A10 RID: 6672
		public string PreWrite;

		// Token: 0x04001A11 RID: 6673
		public string Write;

		// Token: 0x04001A12 RID: 6674
		public string PostWrite;

		// Token: 0x04001A13 RID: 6675
		public string PreRead;

		// Token: 0x04001A14 RID: 6676
		public string Read;

		// Token: 0x04001A15 RID: 6677
		public string PostRead;

		// Token: 0x04001A16 RID: 6678
		public bool? InvokeBase;
	}
}
