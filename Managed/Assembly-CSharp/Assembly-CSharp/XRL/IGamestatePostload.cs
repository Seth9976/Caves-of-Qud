using System;
using XRL.World;

namespace XRL
{
	// Token: 0x020004CF RID: 1231
	[Obsolete("Use IComposite")]
	public interface IGamestatePostload
	{
		// Token: 0x060034FF RID: 13567
		void OnGamestatePostload(XRLGame game, SerializationReader reader);
	}
}
