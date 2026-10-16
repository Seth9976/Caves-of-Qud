using System;
using XRL.World;

namespace XRL
{
	// Token: 0x020004D3 RID: 1235
	[Obsolete("Use IComposite.WantFieldReflection")]
	public interface IObjectGamestateCustomSerializer
	{
		// Token: 0x06003504 RID: 13572
		IGameStateSingleton GameLoad(SerializationReader reader);

		// Token: 0x06003505 RID: 13573
		void GameSave(SerializationWriter writer);
	}
}
