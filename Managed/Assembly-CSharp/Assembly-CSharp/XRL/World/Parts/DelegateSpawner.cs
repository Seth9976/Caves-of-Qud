using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E99 RID: 3737
	[Serializable]
	public class DelegateSpawner : IPart
	{
		// Token: 0x0600964F RID: 38479 RVA: 0x0036796D File Offset: 0x00365B6D
		public DelegateSpawner()
		{
		}

		// Token: 0x06009650 RID: 38480 RVA: 0x00367980 File Offset: 0x00365B80
		public DelegateSpawner(string _faction)
		{
			this.Faction = _faction;
		}

		// Token: 0x06009651 RID: 38481 RVA: 0x0036799A File Offset: 0x00365B9A
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009652 RID: 38482 RVA: 0x0036799D File Offset: 0x00365B9D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009653 RID: 38483 RVA: 0x003679B4 File Offset: 0x00365BB4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				Cell currentCell = this.ParentObject.Physics.CurrentCell;
				List<GameObjectBlueprint> factionMembers = GameObjectFactory.Factory.GetFactionMembers(this.Faction, false, true);
				if (factionMembers.Count == 0)
				{
					foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
					{
						if (gameObjectBlueprint.Name == "Diplomacy Droid")
						{
							factionMembers.Add(gameObjectBlueprint);
						}
					}
				}
				Algorithms.RandomShuffleInPlace<GameObjectBlueprint>(factionMembers, Stat.Rand);
				GameObject gameObject = GameObject.Create(factionMembers[0].Name);
				gameObject.RequirePart<SocialRoles>(false).RequireRole("delegate for " + XRL.World.Faction.GetFormattedName(this.Faction));
				gameObject.RemovePartsDescendedFrom<IBondedLeader>();
				gameObject.Brain.Allegiance.Clear();
				gameObject.Brain.Allegiance.Add(this.Faction, 100);
				gameObject.Brain.Wanders = false;
				gameObject.Brain.WandersRandomly = false;
				gameObject.FireEvent("VillageInit");
				gameObject.SetIntProperty("IsDelegate", 1, false);
				gameObject.RequirePart<Calming>(false).Feeling = 100;
				XRLCore.Core.Game.ActionManager.AddActiveObject(gameObject);
				currentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				this.ParentObject.Destroy(null, false, false, null);
			}
			return true;
		}

		// Token: 0x04003E43 RID: 15939
		public string Faction = "";
	}
}
