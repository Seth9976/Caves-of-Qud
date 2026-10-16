using System;
using XRL;
using XRL.World;
using XRL.World.Parts;

// Token: 0x02000181 RID: 385
[PlayerMutator]
public class WishMenu_PlayerMutator : IPlayerMutator
{
	// Token: 0x06000EDA RID: 3802 RVA: 0x0005ED88 File Offset: 0x0005CF88
	public void mutate(GameObject player)
	{
		player.RequirePart<WishMenu>(false);
	}
}
