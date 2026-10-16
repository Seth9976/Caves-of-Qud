using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002E0 RID: 736
	internal class ImmediateModeException : Exception
	{
		// Token: 0x06001519 RID: 5401 RVA: 0x00054FAB File Offset: 0x000531AB
		public ImmediateModeException(Exception inner)
			: base("", inner)
		{
		}
	}
}
