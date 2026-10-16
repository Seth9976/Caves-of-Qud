using System;
using System.Collections.Generic;
using System.Linq;
using Qud.API;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D21 RID: 3361
	[Serializable]
	public class FungalSporeInfection : Effect, ITierInitialized
	{
		// Token: 0x06008466 RID: 33894 RVA: 0x002FD1F9 File Offset: 0x002FB3F9
		public FungalSporeInfection()
		{
			this.DisplayName = "{{w|itchy skin}}";
			this.Duration = 1;
		}

		// Token: 0x06008467 RID: 33895 RVA: 0x002FD225 File Offset: 0x002FB425
		public FungalSporeInfection(int Duration, string infection)
			: this()
		{
			this.TurnsLeft = Duration;
			this.InfectionObject = infection;
		}

		// Token: 0x06008468 RID: 33896 RVA: 0x002FD23B File Offset: 0x002FB43B
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(20, 30) * 120;
			this.TurnsLeft = this.Duration;
			this.InfectionObject = SporePuffer.InfectionObjectList.GetRandomElement<string>(null);
		}

		// Token: 0x06008469 RID: 33897 RVA: 0x002FD26C File Offset: 0x002FB46C
		public override int GetEffectType()
		{
			return 117456900;
		}

		// Token: 0x0600846A RID: 33898 RVA: 0x002FD273 File Offset: 0x002FB473
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600846B RID: 33899 RVA: 0x002FD276 File Offset: 0x002FB476
		public override string GetDetails()
		{
			return "Skin flakes and itches. Is something growing there?";
		}

		// Token: 0x0600846C RID: 33900 RVA: 0x002FD27D File Offset: 0x002FB47D
		public override bool Apply(GameObject Object)
		{
			return Object.FireEvent("ApplyFungalSporeInfection");
		}

		// Token: 0x0600846D RID: 33901 RVA: 0x002FD28A File Offset: 0x002FB48A
		public override void Remove(GameObject Object)
		{
		}

		// Token: 0x0600846E RID: 33902 RVA: 0x002FD28C File Offset: 0x002FB48C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GeneralAmnestyEvent>.ID;
		}

		// Token: 0x0600846F RID: 33903 RVA: 0x002FD2A2 File Offset: 0x002FB4A2
		public override bool HandleEvent(GeneralAmnestyEvent E)
		{
			this.Owner = null;
			return base.HandleEvent(E);
		}

		// Token: 0x06008470 RID: 33904 RVA: 0x002FD2B2 File Offset: 0x002FB4B2
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyFungalSporeInfection");
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008471 RID: 33905 RVA: 0x002FD2D4 File Offset: 0x002FB4D4
		public static bool BodyPartSuitableForFungalInfection(BodyPart Part, bool IgnoreBodyPartCategory = false)
		{
			if (Part.Abstract)
			{
				return false;
			}
			if (Part.Extrinsic)
			{
				return false;
			}
			if (!Part.Contact)
			{
				return false;
			}
			if (!IgnoreBodyPartCategory && Part.Category != 1 && Part.Category != 2 && Part.Category != 3 && Part.Category != 4 && Part.Category != 5 && Part.Category != 9 && Part.Category != 12 && Part.Category != 13 && Part.Category != 14 && Part.Category != 16 && Part.Category != 7 && Part.Category != 8 && Part.Category != 19 && Part.Category != 18 && Part.Category != 20 && Part.Category != 21)
			{
				return false;
			}
			if (Part.Equipped != null)
			{
				if (Part.Equipped == Part.Cybernetics)
				{
					return false;
				}
				if (!Part.Equipped.CanBeUnequipped(null, null, false, true))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06008472 RID: 33906 RVA: 0x002FD3D5 File Offset: 0x002FB5D5
		public static bool BodyPartPreferableForFungalInfection(BodyPart Part)
		{
			return FungalSporeInfection.BodyPartSuitableForFungalInfection(Part, false) && Part.Type == "Fungal Outcrop";
		}

		// Token: 0x06008473 RID: 33907 RVA: 0x002FD3F2 File Offset: 0x002FB5F2
		public static bool ChooseLimbForInfection(string FungusName, out BodyPart Target, out string Name)
		{
			return FungalSporeInfection.ChooseLimbForInfection(The.Player.Body.GetParts(), FungusName, out Target, out Name, false);
		}

		// Token: 0x06008474 RID: 33908 RVA: 0x002FD40C File Offset: 0x002FB60C
		public static bool ChooseLimbForInfection(List<BodyPart> Parts, string FungusName, out BodyPart Target, out string Name, bool IgnoreBodyPartCategory = false)
		{
			Target = null;
			Name = null;
			List<BodyPart> infectable = Parts.Where<BodyPart>(new Func<BodyPart, bool>(FungalSporeInfection.BodyPartPreferableForFungalInfection)).ToList<BodyPart>();
			infectable.AddRange(Parts.Where<BodyPart>((BodyPart x) => !infectable.Contains(x) && FungalSporeInfection.BodyPartSuitableForFungalInfection(x, IgnoreBodyPartCategory)));
			if (infectable.Count == 0)
			{
				Popup.Show("You have no infectable body parts.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			string[] array = infectable.Select<BodyPart, string>((BodyPart x) => x.GetOrdinalName()).ToArray<string>();
			int num = Popup.PickOption("Choose a limb to infect with " + FungusName + ".", null, "", "Sounds/UI/ui_notification", array, null, null, null, null, null, null, 1, 75, 0, -1, true, false, false, true, false, null, null);
			if (num < 0)
			{
				return false;
			}
			Target = infectable[num];
			Name = array[num];
			return true;
		}

		// Token: 0x06008475 RID: 33909 RVA: 0x002FD50C File Offset: 0x002FB70C
		public static bool ApplyFungalInfection(GameObject Object, string InfectionBlueprint, BodyPart SelectedPart = null)
		{
			if (Object.HasTagOrProperty("ImmuneToFungus"))
			{
				return false;
			}
			if (InfectionBlueprint == "PaxInfection" && (Object.IsPlayer() || Object.HasIntProperty("HasPax")))
			{
				return true;
			}
			Body body = Object.Body;
			if (body == null)
			{
				return true;
			}
			List<BodyPart> list;
			if (SelectedPart == null)
			{
				list = body.GetParts();
				list.ShuffleInPlace<BodyPart>(null);
			}
			else
			{
				list = new List<BodyPart>();
				list.Add(SelectedPart);
			}
			BodyPart bodyPart = null;
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				if (FungalSporeInfection.BodyPartPreferableForFungalInfection(list[i]) && (list[i].Equipped == null || list[i].TryUnequip(false, true, false, false)))
				{
					bodyPart = list[i];
					break;
				}
				i++;
			}
			if (bodyPart == null)
			{
				int j = 0;
				int count2 = list.Count;
				while (j < count2)
				{
					if (FungalSporeInfection.BodyPartSuitableForFungalInfection(list[j], false) && (list[j].Equipped == null || list[j].TryUnequip(false, true, false, false)))
					{
						bodyPart = list[j];
						break;
					}
					j++;
				}
			}
			if (bodyPart == null)
			{
				return false;
			}
			GameObject gameObject = GameObject.Create(InfectionBlueprint);
			gameObject.UsesSlots = bodyPart.VariantType;
			if (bodyPart.SupportsDependent != null)
			{
				foreach (BodyPart bodyPart2 in Object.Body.GetParts())
				{
					if (bodyPart2 != bodyPart && bodyPart2.DependsOn == bodyPart.SupportsDependent && FungalSporeInfection.BodyPartSuitableForFungalInfection(bodyPart2, false))
					{
						GameObject equipped = bodyPart2.Equipped;
						if (equipped == null || !equipped.HasPropertyOrTag("FungalInfection"))
						{
							GameObject gameObject2 = gameObject;
							gameObject2.UsesSlots = gameObject2.UsesSlots + "," + bodyPart2.VariantType;
							break;
						}
						break;
					}
				}
			}
			if (bodyPart.Type == "Hand")
			{
				MeleeWeapon part = gameObject.GetPart<MeleeWeapon>();
				if (InfectionBlueprint == "WaxInfection")
				{
					part.BaseDamage = "2d3";
					part.Skill = "Cudgel";
					part.PenBonus = 0;
					part.MaxStrengthBonus = 999;
				}
				else
				{
					part.BaseDamage = "1d4";
					part.Skill = "Cudgel";
					part.PenBonus = 0;
					part.MaxStrengthBonus = 999;
				}
				gameObject.SetIntProperty("ShowMeleeWeaponStats", 1, false);
			}
			else if (bodyPart.Type == "Body")
			{
				if (InfectionBlueprint == "WaxInfection")
				{
					gameObject.GetPart<Armor>().AV = 6;
					gameObject.GetPart<Armor>().DV = -12;
					gameObject.GetPart<Armor>().SpeedPenalty = 4;
				}
				else
				{
					gameObject.GetPart<Armor>().AV = 3;
				}
			}
			else if (bodyPart.Type == "Feet" || bodyPart.Type == "Head")
			{
				if (InfectionBlueprint == "WaxInfection")
				{
					gameObject.GetPart<Armor>().AV = 3;
					gameObject.GetPart<Armor>().DV = -3;
					gameObject.GetPart<Armor>().SpeedPenalty = 2;
				}
				else
				{
					gameObject.GetPart<Armor>().AV = 1;
				}
			}
			else if (bodyPart.Type == "Hand" || bodyPart.Type == "Hands")
			{
				if (InfectionBlueprint == "WaxInfection")
				{
					gameObject.GetPart<Armor>().AV = 2;
					gameObject.GetPart<Armor>().DV = -2;
					gameObject.GetPart<Armor>().SpeedPenalty = 2;
				}
				else
				{
					gameObject.GetPart<Armor>().AV = 1;
				}
			}
			else if (InfectionBlueprint == "WaxInfection")
			{
				gameObject.GetPart<Armor>().AV = 2;
				gameObject.GetPart<Armor>().DV = -4;
				gameObject.GetPart<Armor>().SpeedPenalty = 2;
			}
			else
			{
				gameObject.GetPart<Armor>().AV = 1;
			}
			if (!bodyPart.Equip(gameObject, new int?(0), true, false, false, true))
			{
				gameObject.Destroy(null, false, false, null);
				return false;
			}
			if (Object.IsPlayer())
			{
				if (Object != null)
				{
					Object.PlayWorldSound("sfx_fungalInfection_gain", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				JournalAPI.AddAccomplishment(string.Concat(new string[]
				{
					"You contracted ",
					gameObject.DisplayNameOnly,
					" on your ",
					bodyPart.GetOrdinalName(),
					", endearing ",
					Object.itself,
					" to fungi across Qud."
				}), string.Concat(new string[]
				{
					"Bless the ",
					Calendar.GetDay(),
					" of ",
					Calendar.GetMonth(),
					", when =name= cemented a historic alliance with fungi by contracting ",
					gameObject.ShortDisplayName,
					" on ",
					The.Player.GetPronounProvider(false).PossessiveAdjective,
					" ",
					bodyPart.GetOrdinalName(),
					"!"
				}), string.Concat(new string[]
				{
					"While traveling around ",
					The.Player.CurrentZone.GetTerrainDisplayName(true),
					", =name= discovered ",
					JournalAPI.GetLandmarkNearestPlayer().Text,
					". There ",
					The.Player.GetPronounProvider(false).Subjective,
					" befriened fungi and contracted ",
					gameObject.DisplayNameOnly,
					" on ",
					The.Player.GetPronounProvider(false).PossessiveAdjective,
					" ",
					bodyPart.GetOrdinalName(),
					"."
				}), null, "general", MuralCategory.BodyExperienceNeutral, MuralWeight.Medium, null, -1L, true);
				Popup.Show(string.Concat(new string[]
				{
					"You've contracted ",
					gameObject.DisplayNameOnly,
					" on your ",
					bodyPart.GetOrdinalName(),
					"."
				}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			if (InfectionBlueprint == "PaxInfection")
			{
				Object.SetIntProperty("HasPax", 1, false);
			}
			return true;
		}

		// Token: 0x06008476 RID: 33910 RVA: 0x002FDB1C File Offset: 0x002FBD1C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				GameObject.Validate(ref this.Owner);
				if (base.Object != null && !base.Object.FireEvent("ApplySpores"))
				{
					this.Duration = 0;
					return true;
				}
				if (this.TurnsLeft % 300 == 0 && !this.bSpawned && base.Object != null && base.Object.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("Your skin itches.", null, true);
				}
				if (this.TurnsLeft > 0)
				{
					this.TurnsLeft--;
				}
				if (this.TurnsLeft <= 0 && !this.bSpawned && base.Object != null)
				{
					this.Duration = 0;
					this.bSpawned = true;
					if (!this.Fake)
					{
						FungalSporeInfection.ApplyFungalInfection(base.Object, this.InfectionObject, null);
					}
				}
			}
			else if (E.ID == "ApplyFungalSporeInfection")
			{
				return false;
			}
			return true;
		}

		// Token: 0x04003979 RID: 14713
		public string InfectionObject = "LuminousInfection";

		// Token: 0x0400397A RID: 14714
		public bool Fake;

		// Token: 0x0400397B RID: 14715
		public int TurnsLeft;

		// Token: 0x0400397C RID: 14716
		public int Damage = 2;

		// Token: 0x0400397D RID: 14717
		public GameObject Owner;

		// Token: 0x0400397E RID: 14718
		public bool bSpawned;
	}
}
