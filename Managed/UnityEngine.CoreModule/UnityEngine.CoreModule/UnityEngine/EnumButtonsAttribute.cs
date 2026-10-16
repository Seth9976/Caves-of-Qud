using System;

namespace UnityEngine
{
	// Token: 0x0200023D RID: 573
	public class EnumButtonsAttribute : PropertyAttribute
	{
		// Token: 0x06001E71 RID: 7793 RVA: 0x0003AA75 File Offset: 0x00038C75
		public EnumButtonsAttribute(bool includeObsolete = false)
		{
			this.includeObsolete = includeObsolete;
		}

		// Token: 0x04000912 RID: 2322
		public bool includeObsolete;
	}
}
