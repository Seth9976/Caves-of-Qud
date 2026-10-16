using System;

namespace XRL
{
	// Token: 0x020004D2 RID: 1234
	[Obsolete("Use GameStateSingleton")]
	[AttributeUsage(AttributeTargets.Class)]
	public class GamestateSingleton : Attribute
	{
		// Token: 0x06003503 RID: 13571 RVA: 0x00100263 File Offset: 0x000FE463
		public GamestateSingleton(string ID)
		{
		}

		// Token: 0x04001AD3 RID: 6867
		public string id;
	}
}
