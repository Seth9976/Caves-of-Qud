using System;
using XRL.World;

namespace XRL
{
	// Token: 0x020004CE RID: 1230
	[Obsolete("Use IComposite")]
	public interface IGamestatePostsave
	{
		// Token: 0x060034FE RID: 13566
		void OnGamestatePostsave(XRLGame game, SerializationWriter writer);
	}
}
