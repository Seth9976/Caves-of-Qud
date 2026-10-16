using System;
using System.Text;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x020012A8 RID: 4776
	[Serializable]
	public class ThrownWeapon : IPart
	{
		// Token: 0x0600BCAA RID: 48298 RVA: 0x00450FBC File Offset: 0x0044F1BC
		public override bool SameAs(IPart p)
		{
			ThrownWeapon thrownWeapon = p as ThrownWeapon;
			return !(thrownWeapon.Damage != this.Damage) && thrownWeapon.Penetration == this.Penetration && thrownWeapon.PenetrationBonus == this.PenetrationBonus && !(thrownWeapon.Attributes != this.Attributes) && base.SameAs(p);
		}

		// Token: 0x0600BCAB RID: 48299 RVA: 0x00451021 File Offset: 0x0044F221
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == QueryEquippableListEvent.ID;
		}

		// Token: 0x0600BCAC RID: 48300 RVA: 0x00451040 File Offset: 0x0044F240
		public override bool HandleEvent(QueryEquippableListEvent E)
		{
			if (E.SlotType == "Thrown Weapon" && E.Item == this.ParentObject && !E.List.Contains(E.Item))
			{
				if (!E.RequirePossible)
				{
					E.List.Add(E.Item);
				}
				else if (E.Actor.IsGiganticCreature)
				{
					if (E.Item.IsGiganticEquipment || E.Item.HasPropertyOrTag("GiganticEquippable") || E.Item.IsNatural())
					{
						E.List.Add(E.Item);
					}
				}
				else if (!E.Item.IsGiganticEquipment || E.Item.IsNatural())
				{
					E.List.Add(E.Item);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCAD RID: 48301 RVA: 0x00451124 File Offset: 0x0044F324
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !this.ParentObject.HasTagOrProperty("HideThrownWeaponPerformance"))
			{
				BodyPart bodyPart = this.ParentObject.EquippedOn();
				MeleeWeapon meleeWeapon;
				if (bodyPart == null || bodyPart.Type == "Thrown Weapon")
				{
					E.AddTag(this.GetPerformanceTag(), 0);
				}
				else if (!this.ParentObject.HasTagOrProperty("ShowMeleeWeaponStats") && this.ParentObject.TryGetPart<MeleeWeapon>(out meleeWeapon))
				{
					E.AddTag(meleeWeapon.GetSimplifiedStats(Options.ShowDetailedWeaponStats), 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCAE RID: 48302 RVA: 0x004511B5 File Offset: 0x0044F3B5
		public override bool HandleEvent(GenericQueryEvent E)
		{
			if (E.Query == "PhaseHarmonicEligible")
			{
				E.Result = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCAF RID: 48303 RVA: 0x004511D8 File Offset: 0x0044F3D8
		public string GetPerformanceTag()
		{
			GameObject equipped = this.ParentObject.Equipped;
			string damage = this.Damage;
			int penetration = this.Penetration;
			int penetrationBonus = this.PenetrationBonus;
			int num = ((equipped != null) ? equipped.Stat("Strength", 0) : 0);
			bool flag = this.Attributes != null && this.Attributes.HasDelimitedSubstring(' ', "Vorpal", StringComparison.Ordinal);
			GetThrownWeaponPerformanceEvent.GetFor(this.ParentObject, ref damage, ref penetration, ref penetrationBonus, ref num, ref flag, true, equipped, (equipped != null) ? equipped.Target : null);
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("{{c|").Append('\u001a').Append("}}");
			if (flag)
			{
				stringBuilder.Append('÷');
				if (penetrationBonus != 0)
				{
					stringBuilder.Append(penetrationBonus.Signed(false));
				}
			}
			else
			{
				stringBuilder.Append(Math.Min(num, penetration) + penetrationBonus + RuleSettings.VISUAL_PENETRATION_BONUS);
			}
			stringBuilder.Append(" {{r|").Append('\u0003').Append("}}")
				.Append(damage);
			return stringBuilder.ToString();
		}

		// Token: 0x0600BCB0 RID: 48304 RVA: 0x004512EA File Offset: 0x0044F4EA
		public bool AdjustDamageDieSize(int Amount)
		{
			this.Damage = DieRoll.AdjustDieSize(this.Damage, Amount);
			DamageDieSizeAdjustedEvent.Send(this.ParentObject, this, Amount);
			return true;
		}

		// Token: 0x0600BCB1 RID: 48305 RVA: 0x0045130C File Offset: 0x0044F50C
		public bool AdjustDamage(int Amount)
		{
			this.Damage = DieRoll.AdjustResult(this.Damage, Amount);
			DamageConstantAdjustedEvent.Send(this.ParentObject, this, Amount);
			return true;
		}

		// Token: 0x04004BA1 RID: 19361
		public string Damage = "1d2";

		// Token: 0x04004BA2 RID: 19362
		public int Penetration = 1;

		// Token: 0x04004BA3 RID: 19363
		public int PenetrationBonus;

		// Token: 0x04004BA4 RID: 19364
		public string Attributes;
	}
}
