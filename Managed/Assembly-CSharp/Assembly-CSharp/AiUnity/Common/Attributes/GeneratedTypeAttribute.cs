using System;

namespace AiUnity.Common.Attributes
{
	// Token: 0x020002E9 RID: 745
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class GeneratedTypeAttribute : Attribute
	{
		// Token: 0x06001984 RID: 6532 RVA: 0x000817F0 File Offset: 0x0007F9F0
		public GeneratedTypeAttribute(string creationDate)
		{
			this.CreationDate = creationDate;
		}

		// Token: 0x04000EED RID: 3821
		public readonly string CreationDate;
	}
}
