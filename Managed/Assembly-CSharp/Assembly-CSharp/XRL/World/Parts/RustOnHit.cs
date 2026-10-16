using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.AI;
using XRL.World.Anatomy;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E8B RID: 3723
	[Serializable]
	public class RustOnHit : IPart
	{
		// Token: 0x060095EF RID: 38383 RVA: 0x0036457C File Offset: 0x0036277C
		protected GameObject GetRandomItemFrom(GameObject Object, bool AffectInventory, bool AffectEquipment)
		{
			bool flag;
			if (AffectInventory)
			{
				Inventory inventory = Object.Inventory;
				if (((inventory != null) ? inventory.Objects : null) != null)
				{
					flag = Object.Inventory.Objects.Count > 0;
					goto IL_002D;
				}
			}
			flag = false;
			IL_002D:
			bool flag2 = flag;
			bool flag3 = AffectEquipment && Object.Body != null && Object.Body.GetBody().GetEquippedObjectCount() > 0;
			if (!flag2 && !flag3)
			{
				return null;
			}
			if (flag3 && !this.PreferPartType.IsNullOrEmpty())
			{
				List<GameObject> list = Event.NewGameObjectList();
				foreach (BodyPart bodyPart in Object.Body.LoopPart(this.PreferPartType))
				{
					if (bodyPart.Equipped != null)
					{
						list.Add(bodyPart.Equipped);
					}
				}
				if (!list.IsNullOrEmpty<GameObject>())
				{
					return list.GetRandomElement<GameObject>(null);
				}
			}
			if (flag2 && flag3)
			{
				int equippedObjectCount = Object.Body.GetBody().GetEquippedObjectCount();
				int count = Object.Inventory.Objects.Count;
				if (Stat.Rnd.Next(equippedObjectCount + count) < equippedObjectCount)
				{
					return Object.Body.GetEquippedObjectsReadonly().GetRandomElement<GameObject>(null) ?? Object.Inventory.Objects.GetRandomElement<GameObject>(null);
				}
				return Object.Inventory.Objects.GetRandomElement<GameObject>(null) ?? Object.Body.GetEquippedObjectsReadonly().GetRandomElement<GameObject>(null);
			}
			else
			{
				if (flag2)
				{
					return Object.Inventory.Objects.GetRandomElement<GameObject>(null);
				}
				return Object.Body.GetEquippedObjectsReadonly().GetRandomElement<GameObject>(null);
			}
		}

		// Token: 0x060095F0 RID: 38384 RVA: 0x00364718 File Offset: 0x00362918
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060095F1 RID: 38385 RVA: 0x0036471B File Offset: 0x0036291B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			if (Object.IsCreature)
			{
				Registrar.Register("AttackerHit");
				return;
			}
			if (Object.IsProjectile)
			{
				Registrar.Register("ProjectileHit");
				return;
			}
			Registrar.Register("WeaponHit");
			Registrar.Register("WeaponThrowHit");
		}

		// Token: 0x060095F2 RID: 38386 RVA: 0x0036475C File Offset: 0x0036295C
		public override bool FireEvent(Event E)
		{
			if (E.ID.EndsWith("Hit"))
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Weapon");
				GameObject gameObjectParameter4 = E.GetGameObjectParameter("Projectile");
				if (GameObject.Validate(ref gameObjectParameter2))
				{
					int num = this.Amount.RollCached();
					if (num <= 0)
					{
						return base.FireEvent(E);
					}
					GameObject gameObject = gameObjectParameter;
					GameObject gameObject2 = gameObjectParameter3;
					string text = "Part RustOnHit Activation";
					GameObject gameObject3 = gameObjectParameter2;
					GameObject gameObject4 = gameObjectParameter4;
					int @for = GetSpecialEffectChanceEvent.GetFor(gameObject, gameObject2, text, this.Chance, gameObject3, gameObject4, true, false);
					bool flag = this.AffectInventory;
					bool flag2 = this.AffectEquipment;
					if (gameObjectParameter2.HasRegisteredEvent("GetRustableContents"))
					{
						Event @event = Event.New("GetRustableContents", "Inventory", flag ? 1 : 0, "Equipment", flag2 ? 1 : 0);
						gameObjectParameter2.FireEvent(@event);
						flag = @event.GetIntParameter("Inventory", 0) >= 1;
						flag2 = @event.GetIntParameter("Equipment", 0) >= 1;
						if (!flag && !flag2)
						{
							return base.FireEvent(E);
						}
					}
					for (int i = 0; i < num; i++)
					{
						if (@for.in100())
						{
							GameObject randomItemFrom = this.GetRandomItemFrom(gameObjectParameter2, flag, flag2);
							if (randomItemFrom != null)
							{
								randomItemFrom.ApplyEffect(new Rusted(), null);
							}
						}
					}
					if (!gameObjectParameter2.IsHostileTowards(gameObjectParameter))
					{
						gameObjectParameter2.AddOpinion<OpinionAttack>(gameObjectParameter, gameObjectParameter3, 1f);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003DFE RID: 15870
		public int Chance = 15;

		// Token: 0x04003DFF RID: 15871
		public string Amount = "1";

		// Token: 0x04003E00 RID: 15872
		public string PreferPartType;

		// Token: 0x04003E01 RID: 15873
		public bool AffectInventory = true;

		// Token: 0x04003E02 RID: 15874
		public bool AffectEquipment = true;
	}
}
