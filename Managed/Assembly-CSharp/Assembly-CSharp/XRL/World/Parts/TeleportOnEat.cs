using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000F8F RID: 3983
	[Serializable]
	public class TeleportOnEat : IPart
	{
		// Token: 0x06009E0E RID: 40462 RVA: 0x0039378A File Offset: 0x0039198A
		public override bool SameAs(IPart p)
		{
			return (p as TeleportOnEat).Controlled == this.Controlled && base.SameAs(p);
		}

		// Token: 0x06009E0F RID: 40463 RVA: 0x003937A8 File Offset: 0x003919A8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x06009E10 RID: 40464 RVA: 0x003937BE File Offset: 0x003919BE
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("travel", 5);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E11 RID: 40465 RVA: 0x003937E1 File Offset: 0x003919E1
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009E12 RID: 40466 RVA: 0x003937F8 File Offset: 0x003919F8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				GameObject actor = E.GetGameObjectParameter("Eater");
				if (actor.IsRealityDistortionUsable(null, null))
				{
					if (!this.Controlled)
					{
						Cell currentCell = actor.CurrentCell;
						if (currentCell != null && !currentCell.ParentZone.IsWorldMap() && IComponent<GameObject>.CheckRealityDistortionUsability(actor, null, actor, this.ParentObject, null, null, 0))
						{
							List<Cell> emptyCells = currentCell.ParentZone.GetEmptyCells((Cell c) => IComponent<GameObject>.CheckRealityDistortionAccessibility(null, c, actor, this.ParentObject, null, 0), null);
							emptyCells.Remove(currentCell);
							Cell randomElement = emptyCells.GetRandomElement<Cell>(null);
							if (randomElement != null)
							{
								actor.ParticleBlip("&C\u000f", 10, 0L, false);
								actor.TeleportTo(randomElement, new int?(0), true, false, false, false, "disappear", "appear");
								actor.TeleportSwirl(null, "&C", true, null, 'ù', false);
								actor.ParticleBlip("&C\u000f", 10, 0L, false);
								if (actor.IsPlayer())
								{
									Popup.Show("You are transported!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
								}
							}
						}
					}
					else
					{
						Teleportation teleportation = null;
						GameObject actor2 = actor;
						Teleportation.Cast(teleportation, this.Level, null, null, actor2, false, 0);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400415F RID: 16735
		public bool Controlled;

		// Token: 0x04004160 RID: 16736
		public string Level = "5-6";
	}
}
