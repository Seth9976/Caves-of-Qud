using System;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013C9 RID: 5065
	[Serializable]
	public class Beak : BaseDefaultEquipmentMutation
	{
		// Token: 0x0600C3AE RID: 50094 RVA: 0x00476467 File Offset: 0x00474667
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			Beak beak = base.DeepCopy(Parent, MapInv) as Beak;
			beak.BeakObject = null;
			return beak;
		}

		// Token: 0x0600C3B0 RID: 50096 RVA: 0x00476490 File Offset: 0x00474690
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C3B1 RID: 50097 RVA: 0x00476493 File Offset: 0x00474693
		public override bool GeneratesEquipment()
		{
			return true;
		}

		// Token: 0x0600C3B2 RID: 50098 RVA: 0x00476496 File Offset: 0x00474696
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C3B3 RID: 50099 RVA: 0x004764A0 File Offset: 0x004746A0
		public override bool FireEvent(Event E)
		{
			return base.FireEvent(E);
		}

		// Token: 0x0600C3B4 RID: 50100 RVA: 0x004764A9 File Offset: 0x004746A9
		public override string GetDescription()
		{
			if (!this.Variant.IsNullOrEmpty())
			{
				return "Your face bears a sightly " + this.GetVariantName().ToLowerInvariant() + ".\n\n+1 Ego\nYou occasionally peck at your opponents.\n+200 reputation with {{w|birds}}";
			}
			return "Your face bears a sightly beak.\n\n+1 Ego\nYou occasionally peck at your opponents.\n+200 reputation with {{w|birds}}";
		}

		// Token: 0x0600C3B5 RID: 50101 RVA: 0x004764D8 File Offset: 0x004746D8
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C3B6 RID: 50102 RVA: 0x004764E0 File Offset: 0x004746E0
		public override void OnRegenerateDefaultEquipment(Body body)
		{
			BodyPart firstPart;
			if (!base.TryGetRegisteredSlot(body, this.BodyPartType, out firstPart, false))
			{
				firstPart = body.GetFirstPart(this.BodyPartType);
				if (firstPart != null)
				{
					base.RegisterSlot(this.BodyPartType, firstPart);
				}
			}
			if (firstPart != null)
			{
				this.BeakObject = GameObjectFactory.Factory.CreateObject(this.Variant ?? "Beak");
				MeleeWeapon part = this.BeakObject.GetPart<MeleeWeapon>();
				Armor part2 = this.BeakObject.GetPart<Armor>();
				part.Skill = "ShortBlades";
				part.BaseDamage = "1";
				part.Slot = firstPart.Type;
				part2.WornOn = firstPart.Type;
				part2.AV = 0;
				firstPart.DefaultBehavior = this.BeakObject;
				firstPart.DefaultBehavior.SetStringProperty("TemporaryDefaultBehavior", "Beak", false);
				this.BeakObject.SetStringProperty("HitSound", "Sounds/Abilities/sfx_ability_mutation_beak_peck", false);
				base.ResetDisplayName();
			}
			base.OnRegenerateDefaultEquipment(body);
		}

		// Token: 0x0600C3B7 RID: 50103 RVA: 0x004765D2 File Offset: 0x004747D2
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C3B8 RID: 50104 RVA: 0x004765DB File Offset: 0x004747DB
		public override bool Mutate(GameObject GO, int Level)
		{
			base.StatShifter.SetStatShift(this.ParentObject, "Ego", 1, true);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C3B9 RID: 50105 RVA: 0x004765FE File Offset: 0x004747FE
		public override bool Unmutate(GameObject GO)
		{
			base.StatShifter.RemoveStatShifts(this.ParentObject);
			base.CleanUpMutationEquipment(GO, ref this.BeakObject);
			return base.Unmutate(GO);
		}

		// Token: 0x04004DC0 RID: 19904
		public GameObject BeakObject;

		// Token: 0x04004DC1 RID: 19905
		public string BodyPartType = "Face";
	}
}
