using System;
using System.Collections.Generic;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x020011B2 RID: 4530
	[Serializable]
	public class EitherOrPetSpawner : IPart
	{
		// Token: 0x0600B3EB RID: 46059 RVA: 0x0041A5D4 File Offset: 0x004187D4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B3EC RID: 46060 RVA: 0x0041A5EC File Offset: 0x004187EC
		public void spawn(Cell c, bool either)
		{
			GameObject gameObject = null;
			if (either)
			{
				gameObject = GameObject.Create("EitherPet");
			}
			if (!either)
			{
				gameObject = GameObject.Create("OrPet");
			}
			gameObject.Render.Tile = IComponent<GameObject>.ThePlayer.Render.Tile;
			gameObject.SetAlliedLeader<AllyPet>(IComponent<GameObject>.ThePlayer, 0, false);
			gameObject.SetActive();
			c.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
		}

		// Token: 0x0600B3ED RID: 46061 RVA: 0x0041A65C File Offset: 0x0041885C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell" && IComponent<GameObject>.ThePlayer != null && IComponent<GameObject>.ThePlayer.CurrentCell != null)
			{
				List<Cell> list = new List<Cell>();
				IComponent<GameObject>.ThePlayer.CurrentCell.GetConnectedSpawnLocations(2, list, true);
				if (list.Count >= 2)
				{
					this.spawn(list[0], true);
					this.spawn(list[1], false);
					this.ParentObject.Destroy(null, false, false, null);
				}
			}
			return base.FireEvent(E);
		}
	}
}
