using System;

namespace XRL
{
	/// <summary>A game state that will be initialized once on new game.</summary>
	// Token: 0x020004D1 RID: 1233
	[AttributeUsage(AttributeTargets.Class)]
	public class GameStateSingleton : Attribute
	{
		// Token: 0x06003501 RID: 13569 RVA: 0x0010024C File Offset: 0x000FE44C
		public GameStateSingleton()
		{
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x00100254 File Offset: 0x000FE454
		public GameStateSingleton(string ID)
		{
			this.ID = ID;
		}

		/// <summary>Defaults to the type name of the class.</summary>
		// Token: 0x04001AD2 RID: 6866
		public string ID;
	}
}
