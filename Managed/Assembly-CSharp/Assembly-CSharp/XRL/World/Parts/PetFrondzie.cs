using System;
using System.Collections.Generic;
using Qud.API;
using XRL.Language;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020011A5 RID: 4517
	[Serializable]
	public class PetFrondzie : IPart
	{
		// Token: 0x0600B397 RID: 45975 RVA: 0x004182D5 File Offset: 0x004164D5
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B398 RID: 45976 RVA: 0x004182EC File Offset: 0x004164EC
		public void taunt(GameObject target)
		{
			string randomElement = PetFrondzie.tauntStyles.GetRandomElement<string>(null);
			string randomElement2 = PetFrondzie.tauntTypes.GetRandomElement<string>(null);
			int num = Stat.Roll(1, 100);
			string text;
			if (num <= 50)
			{
				text = EncountersAPI.GetAnObject(null).an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false);
			}
			else if (num <= 90)
			{
				text = Factions.GetRandomFaction().DisplayName;
			}
			else
			{
				text = PetFrondzie.tauntTargets.GetRandomElement<string>(null);
			}
			IComponent<GameObject>.XDidY(this.ParentObject, "yell", string.Concat(new string[]
			{
				Grammar.A(randomElement, false),
				" ",
				randomElement2,
				" about ",
				text
			}), null, null, null, null, null, false, false, null, null, false, false, true, false, false, null);
			if (!target.MakeSave("Willpower", 12, this.ParentObject, "Ego", "Verbal Taunt", false, false, false, false, null))
			{
				target.Brain.Goals.Clear();
				target.Target = this.ParentObject;
				IComponent<GameObject>.XDidY(target, "are", "enraged by the mockery", "!", null, null, null, target, false, false, null, null, false, false, true, false, false, null);
			}
		}

		// Token: 0x0600B399 RID: 45977 RVA: 0x0041841C File Offset: 0x0041661C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				if (this.cooldown > 0)
				{
					this.cooldown--;
				}
				if (this.cooldown <= 0)
				{
					GameObject partyLeader = this.ParentObject.PartyLeader;
					if (partyLeader != null && partyLeader.IsInCombat() && this.ParentObject.CurrentCell != null)
					{
						List<GameObject> objects = this.ParentObject.CurrentZone.GetObjects((GameObject o) => o.Target == this.ParentObject.PartyLeader);
						if (objects.Count > 0)
						{
							this.cooldown = 20;
							this.taunt(objects.GetRandomElement<GameObject>(null));
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400486B RID: 18539
		public int cooldown = 20;

		// Token: 0x0400486C RID: 18540
		public static string[] tauntStyles = new string[]
		{
			"terrible", "ghastly", "corny", "shameful", "rude", "painful", "monstrous", "horrid", "puerile", "childish",
			"boring", "ridiculous"
		};

		// Token: 0x0400486D RID: 18541
		public static string[] tauntTypes = new string[] { "pun", "joke", "double entendre", "jape", "goof", "tall tale" };

		// Token: 0x0400486E RID: 18542
		public static string[] tauntTargets = new string[]
		{
			"the stilt bazaar", "the Canticles Chromaic", "Omonporch", "the Argent Fathers", "Bey Lah", "Kyakukya", "the Spindle", "the Rainbow Wood", "mutations", "combat",
			"pets", "the Dark Calculus", "tacos suprema"
		};
	}
}
