using System;
using XRL.Language;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200144A RID: 5194
	[Serializable]
	public class Horns : BaseMutation
	{
		// Token: 0x0600CAD1 RID: 51921 RVA: 0x0049BA29 File Offset: 0x00499C29
		public override bool GeneratesEquipment()
		{
			return true;
		}

		// Token: 0x0600CAD2 RID: 51922 RVA: 0x0049BA2C File Offset: 0x00499C2C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<RegenerateDefaultEquipmentEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600CAD3 RID: 51923 RVA: 0x0049BA4A File Offset: 0x00499C4A
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("might", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CAD4 RID: 51924 RVA: 0x0049BA72 File Offset: 0x00499C72
		public override bool HandleEvent(RegenerateDefaultEquipmentEvent E)
		{
			this.RegrowHorns(false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600CAD5 RID: 51925 RVA: 0x0049BA84 File Offset: 0x00499C84
		public override string GetDescription()
		{
			if (this.Variant == null)
			{
				return "Horns jut out of your head.";
			}
			GameObjectBlueprint blueprint = GameObjectFactory.Factory.GetBlueprint(this.Variant);
			string propertyOrTag = blueprint.GetPropertyOrTag("Gender");
			string cachedDisplayNameStripped = blueprint.CachedDisplayNameStripped;
			if (propertyOrTag == "plural")
			{
				return Grammar.InitCap(cachedDisplayNameStripped) + " jut out of your head.";
			}
			return Grammar.A(cachedDisplayNameStripped, true) + " juts out of your head.";
		}

		// Token: 0x0600CAD6 RID: 51926 RVA: 0x0049BAF0 File Offset: 0x00499CF0
		public int GetAV(int Level)
		{
			return 1 + (Level - 1) / 3;
		}

		// Token: 0x0600CAD7 RID: 51927 RVA: 0x0049BAFC File Offset: 0x00499CFC
		public string GetBaseDamage(int Level)
		{
			return "2d" + (3 + Level / 3).ToString();
		}

		// Token: 0x0600CAD8 RID: 51928 RVA: 0x0049BB20 File Offset: 0x00499D20
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			Horns horns = base.DeepCopy(Parent, MapInv) as Horns;
			horns.HornsObject = null;
			return horns;
		}

		// Token: 0x0600CAD9 RID: 51929 RVA: 0x0049BB38 File Offset: 0x00499D38
		public override string GetLevelText(int Level)
		{
			string baseDamage = this.GetBaseDamage(Level);
			int av = this.GetAV(Level);
			string text = "20% chance on melee attack to gore your opponent\n";
			text = text + "Damage increment: {{rules|" + baseDamage + "}}\n";
			text = text + "To-hit bonus: {{rules|" + HornsProperties.GetToHitBonus(Level).ToString() + "}}\n";
			if (Level == base.Level)
			{
				text += "Goring attacks may cause bleeding\n";
			}
			else if (Level % 4 == 1)
			{
				text += "{{rules|Increased bleeding save difficulty and intensity}}\n";
			}
			else
			{
				text += "{{rules|Increased bleeding save difficulty}}\n";
			}
			string text2 = "plural";
			string text3;
			if (this.Variant == null)
			{
				text3 = "horns";
			}
			else
			{
				GameObjectBlueprint blueprint = GameObjectFactory.Factory.GetBlueprint(this.Variant);
				text2 = blueprint.GetPropertyOrTag("Gender");
				text3 = blueprint.CachedDisplayNameStripped;
			}
			if (text2 == "plural")
			{
				text = text + Grammar.InitCap(text3) + " are a short-blade class natural weapon.\n";
			}
			else
			{
				text = text + Grammar.InitCap(text3) + " is a short-blade class natural weapon.\n";
			}
			text = text + "+{{rules|" + av.ToString() + " AV}}\n";
			text += "Cannot wear helmets\n";
			return text + "+100 reputation with {{w|antelopes}} and {{w|goatfolk}}";
		}

		// Token: 0x0600CADA RID: 51930 RVA: 0x0049BC64 File Offset: 0x00499E64
		public void RegrowHorns(bool Force = false)
		{
			Body body = this.ParentObject.Body;
			if (body == null || this.Variant.IsNullOrEmpty())
			{
				return;
			}
			string partParameter = GameObjectFactory.Factory.GetBlueprint(this.Variant).GetPartParameter<string>("MeleeWeapon", "Slot", "Head");
			GameObject hornsObject = this.HornsObject;
			BodyPart bodyPart = ((hornsObject != null) ? hornsObject.DefaultOrEquippedPart() : null);
			if (bodyPart == null || bodyPart.Type != partParameter)
			{
				bodyPart = body.GetFirstPart(partParameter);
			}
			if (bodyPart == null || (!Force && this.HornsObject.IsValid() && this.HornsObject.Blueprint == this.Variant && this.HornsObject.DefaultOrEquippedPart() == bodyPart))
			{
				return;
			}
			if (this.HornsObject != null && (!this.HornsObject.IsValid() || this.HornsObject.Blueprint != this.Variant))
			{
				GameObject.Release(ref this.HornsObject, true, true);
			}
			if (this.HornsObject == null)
			{
				this.HornsObject = GameObject.Create(this.Variant);
			}
			MeleeWeapon part = this.HornsObject.GetPart<MeleeWeapon>();
			part.Slot = bodyPart.Type;
			part.MaxStrengthBonus = 100;
			part.BaseDamage = this.GetBaseDamage(base.Level);
			Armor part2 = this.HornsObject.GetPart<Armor>();
			part2.WornOn = bodyPart.Type;
			part2.AV = this.GetAV(base.Level);
			this.ParentObject.ForceEquipObject(this.HornsObject, bodyPart, true, new int?(0));
			base.ResetDisplayName();
		}

		// Token: 0x0600CADB RID: 51931 RVA: 0x0049BDE4 File Offset: 0x00499FE4
		public override bool ChangeLevel(int NewLevel)
		{
			this.RegrowHorns(true);
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CADC RID: 51932 RVA: 0x0049BDF4 File Offset: 0x00499FF4
		public override void SetVariant(string Variant)
		{
			base.SetVariant(Variant);
			if (this.HornsObject != null && this.HornsObject.Blueprint != Variant)
			{
				this.RegrowHorns(true);
			}
		}

		// Token: 0x0600CADD RID: 51933 RVA: 0x0049BE1F File Offset: 0x0049A01F
		public override bool Mutate(GameObject GO, int Level)
		{
			if (this.Variant.IsNullOrEmpty())
			{
				this.Variant = this.GetVariants().GetRandomElement<string>(null);
				base.ResetDisplayName();
			}
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CADE RID: 51934 RVA: 0x0049BE4E File Offset: 0x0049A04E
		public override bool Unmutate(GameObject GO)
		{
			base.CleanUpMutationEquipment(GO, ref this.HornsObject);
			return base.Unmutate(GO);
		}

		// Token: 0x04004EF9 RID: 20217
		public GameObject HornsObject;
	}
}
