using System;
using System.Collections.Generic;
using XRL.Names;
using XRL.World.Anatomy;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000E5F RID: 3679
	[Serializable]
	public class Corpse : IPart
	{
		// Token: 0x060094CE RID: 38094 RVA: 0x0035CCB8 File Offset: 0x0035AEB8
		public override bool SameAs(IPart p)
		{
			Corpse corpse = p as Corpse;
			return corpse.CorpseChance == this.CorpseChance && !(corpse.CorpseBlueprint != this.CorpseBlueprint) && !(corpse.CorpseRequiresBodyPart != this.CorpseRequiresBodyPart) && corpse.BurntCorpseChance == this.BurntCorpseChance && !(corpse.BurntCorpseBlueprint != this.BurntCorpseBlueprint) && !(corpse.BurntCorpseRequiresBodyPart != this.BurntCorpseRequiresBodyPart) && corpse.VaporizedCorpseChance == this.VaporizedCorpseChance && !(corpse.VaporizedCorpseBlueprint != this.VaporizedCorpseBlueprint) && !(corpse.VaporizedCorpseRequiresBodyPart != this.VaporizedCorpseRequiresBodyPart) && corpse.BuildCorpseChance == this.BuildCorpseChance && base.SameAs(p);
		}

		// Token: 0x060094CF RID: 38095 RVA: 0x0035CD91 File Offset: 0x0035AF91
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDeathRemovalEvent.ID;
		}

		// Token: 0x060094D0 RID: 38096 RVA: 0x0035CDA7 File Offset: 0x0035AFA7
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (this.ParentObject.GetIntProperty("SuppressCorpseDrops", 0) <= 0)
			{
				this.ProcessCorpseDrop(E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060094D1 RID: 38097 RVA: 0x0035CDCB File Offset: 0x0035AFCB
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060094D2 RID: 38098 RVA: 0x0035CDD0 File Offset: 0x0035AFD0
		private void ProcessCorpseDrop(BeforeDeathRemovalEvent E)
		{
			IInventory dropInventory = this.ParentObject.GetDropInventory();
			if (dropInventory == null)
			{
				return;
			}
			Zone inventoryZone = dropInventory.GetInventoryZone();
			if (inventoryZone != null && !inventoryZone.Built && !this.BuildCorpseChance.in100())
			{
				return;
			}
			Body body = this.ParentObject.Body;
			GameObject gameObject = null;
			Physics physics = this.ParentObject.Physics;
			if (!(((physics != null) ? physics.LastDamagedByType : null) == "Fire"))
			{
				Physics physics2 = this.ParentObject.Physics;
				if (!(((physics2 != null) ? physics2.LastDamagedByType : null) == "Light"))
				{
					if (this.ParentObject.Physics.LastDamagedByType == "Vaporized")
					{
						if (this.VaporizedCorpseChance > 0 && (this.VaporizedCorpseRequiresBodyPart.IsNullOrEmpty() || ((body != null) ? body.GetFirstPart(this.VaporizedCorpseRequiresBodyPart) : null) != null) && this.VaporizedCorpseChance.in100())
						{
							gameObject = GameObject.Create(this.VaporizedCorpseBlueprint);
							goto IL_0179;
						}
						goto IL_0179;
					}
					else
					{
						if (this.CorpseChance > 0 && (this.CorpseRequiresBodyPart.IsNullOrEmpty() || ((body != null) ? body.GetFirstPart(this.CorpseRequiresBodyPart) : null) != null) && this.CorpseChance.in100())
						{
							gameObject = GameObject.Create(this.CorpseBlueprint);
							goto IL_0179;
						}
						goto IL_0179;
					}
				}
			}
			if (this.BurntCorpseChance > 0 && (this.BurntCorpseRequiresBodyPart.IsNullOrEmpty() || ((body != null) ? body.GetFirstPart(this.BurntCorpseRequiresBodyPart) : null) != null) && this.BurntCorpseChance.in100())
			{
				gameObject = GameObject.Create(this.BurntCorpseBlueprint);
			}
			IL_0179:
			if (gameObject == null)
			{
				return;
			}
			Temporary.CarryOver(this.ParentObject, gameObject, false);
			Phase.carryOver(this.ParentObject, gameObject);
			if (this.ParentObject.HasProperName)
			{
				gameObject.SetStringProperty("CreatureName", this.ParentObject.BaseDisplayName, false);
			}
			else
			{
				string text = NameMaker.MakeName(this.ParentObject, null, null, null, null, null, null, null, null, null, null, null, null, true, false, null, null);
				if (text != null)
				{
					gameObject.SetStringProperty("CreatureName", text, false);
				}
			}
			if (this.ParentObject.HasID)
			{
				gameObject.SetStringProperty("SourceID", this.ParentObject.ID, false);
			}
			gameObject.SetStringProperty("SourceBlueprint", this.ParentObject.Blueprint, false);
			if (E.Killer != null && E.Killer != this.ParentObject)
			{
				if (E.Killer.HasID)
				{
					gameObject.SetStringProperty("KillerID", E.Killer.ID, false);
				}
				gameObject.SetStringProperty("KillerBlueprint", E.Killer.Blueprint, false);
			}
			if (!E.ThirdPersonReason.IsNullOrEmpty())
			{
				gameObject.SetStringProperty("DeathReason", E.ThirdPersonReason, false);
			}
			if (this.ParentObject.HasProperty("StoredByPlayer") || this.ParentObject.HasProperty("FromStoredByPlayer"))
			{
				gameObject.SetIntProperty("FromStoredByPlayer", 1, false);
			}
			dropInventory.AddObjectToInventory(gameObject, null, false, false, true, null, E);
			string genotype = this.ParentObject.GetGenotype();
			if (!genotype.IsNullOrEmpty())
			{
				gameObject.SetStringProperty("FromGenotype", genotype, false);
			}
			if (body != null)
			{
				List<GameObject> list = null;
				foreach (BodyPart bodyPart in body.GetParts())
				{
					if (bodyPart.Cybernetics != null)
					{
						if (list == null)
						{
							list = Event.NewGameObjectList();
						}
						list.Add(bodyPart.Cybernetics);
						UnimplantedEvent.Send(this.ParentObject, bodyPart.Cybernetics, bodyPart, null, false);
						ImplantRemovedEvent.Send(this.ParentObject, bodyPart.Cybernetics, bodyPart, null, false);
					}
				}
				if (list != null)
				{
					gameObject.AddPart<CyberneticsButcherableCybernetic>(new CyberneticsButcherableCybernetic(list), true, false);
					gameObject.RemovePart<Food>();
				}
			}
			DroppedEvent.Send(this.ParentObject, gameObject, false);
		}

		// Token: 0x04003D91 RID: 15761
		public int CorpseChance;

		// Token: 0x04003D92 RID: 15762
		public string CorpseBlueprint;

		// Token: 0x04003D93 RID: 15763
		public string CorpseRequiresBodyPart;

		// Token: 0x04003D94 RID: 15764
		public int BurntCorpseChance;

		// Token: 0x04003D95 RID: 15765
		public string BurntCorpseBlueprint;

		// Token: 0x04003D96 RID: 15766
		public string BurntCorpseRequiresBodyPart;

		// Token: 0x04003D97 RID: 15767
		public int VaporizedCorpseChance;

		// Token: 0x04003D98 RID: 15768
		public string VaporizedCorpseBlueprint;

		// Token: 0x04003D99 RID: 15769
		public string VaporizedCorpseRequiresBodyPart;

		// Token: 0x04003D9A RID: 15770
		public int BuildCorpseChance = 100;
	}
}
