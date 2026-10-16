using System;
using System.Collections.Generic;
using System.Text;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x020011C5 RID: 4549
	[Serializable]
	public class EquipCharge : IPoweredPart
	{
		// Token: 0x0600B4D8 RID: 46296 RVA: 0x0041FFEF File Offset: 0x0041E1EF
		public EquipCharge()
		{
			this.ChargeUse = 0;
			this.IsEMPSensitive = false;
			this.WorksOnHolder = true;
			this.WorksOnWearer = true;
		}

		// Token: 0x0600B4D9 RID: 46297 RVA: 0x00420024 File Offset: 0x0041E224
		public override bool SameAs(IPart p)
		{
			EquipCharge equipCharge = p as EquipCharge;
			return equipCharge.ChargeRate == this.ChargeRate && !(equipCharge.EquipperProperty != this.EquipperProperty) && equipCharge.Describe == this.Describe && !(equipCharge.PropertyConditionDescription != this.PropertyConditionDescription) && equipCharge.AlternateChargeRateInCombat == this.AlternateChargeRateInCombat && equipCharge.CombatChargeRate == this.CombatChargeRate && base.SameAs(p);
		}

		// Token: 0x0600B4DA RID: 46298 RVA: 0x004200A9 File Offset: 0x0041E2A9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetPassiveItemListEvent.ID || ID == GetShortDescriptionEvent.ID || ID == QueryChargeProductionEvent.ID || ID == QueryEquippableListEvent.ID || ID == TakenEvent.ID;
		}

		// Token: 0x0600B4DB RID: 46299 RVA: 0x004200E0 File Offset: 0x0041E2E0
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Describe)
			{
				StringBuilder postfix = E.Postfix;
				postfix.Append("\n{{rules|Regains charge when ");
				List<string> list = new List<string>();
				if (this.WorksOnWearer)
				{
					list.Add("worn");
				}
				if (this.WorksOnHolder)
				{
					list.Add("held in hand");
				}
				if (this.WorksOnCarrier)
				{
					list.Add("carried");
				}
				if (this.WorksOnImplantee)
				{
					list.Add("implanted");
				}
				if (this.WorksOnCellContents || this.WorksOnAdjacentCellContents)
				{
					list.Add("objects are nearby");
				}
				if (this.WorksOnEnclosed)
				{
					list.Add("enclosing something");
				}
				if (this.WorksOnInventory)
				{
					list.Add("containing inventory");
				}
				if (this.WorksOnSelf)
				{
					list.Add("existing");
				}
				if (list.Count > 0)
				{
					postfix.Append(Grammar.MakeOrList(list, true));
				}
				else
				{
					postfix.Append("used in some indeterminate fashion");
				}
				if (this.EquipperProperty != null)
				{
					if (this.PropertyConditionDescription != null)
					{
						postfix.Append(' ').Append(this.PropertyConditionDescription);
					}
					else
					{
						postfix.Append(" by someone ").Append(this.EquipperProperty);
					}
				}
				if (this.AlternateChargeRateInCombat)
				{
					if (this.ChargeRate <= 0 && this.CombatChargeRate > 0)
					{
						postfix.Append(" in combat");
					}
					else if (this.ChargeRate > 0 && this.CombatChargeRate <= 0)
					{
						postfix.Append(" not in combat");
					}
					else if (this.ChargeRate < this.CombatChargeRate)
					{
						if (this.ChargeRate < this.CombatChargeRate / 2)
						{
							postfix.Append(", much more quickly while in combat");
						}
						else
						{
							postfix.Append(", more quickly while in combat");
						}
					}
					else if (this.ChargeRate > this.CombatChargeRate)
					{
						if (this.ChargeRate > this.CombatChargeRate * 2)
						{
							postfix.Append(", much less quickly while in combat");
						}
						else
						{
							postfix.Append(", less quickly while in combat");
						}
					}
				}
				postfix.Append(".}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4DC RID: 46300 RVA: 0x004202E8 File Offset: 0x0041E4E8
		public override bool HandleEvent(QueryChargeProductionEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = ((this.AlternateChargeRateInCombat && !this.ForeachActivePartSubjectWhile(new Predicate<GameObject>(EquipCharge.CheckCombat), false)) ? this.CombatChargeRate : this.ChargeRate);
				if (!this.EquipperProperty.IsNullOrEmpty())
				{
					int intProperty = this.ParentObject.Equipped.GetIntProperty(this.EquipperProperty, 0);
					if (num > intProperty)
					{
						num = intProperty;
					}
				}
				if (this.ChargeUse > 0)
				{
					num -= this.ChargeUse;
				}
				if (num > 0)
				{
					E.Amount += num * E.Multiple;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4DD RID: 46301 RVA: 0x004203AC File Offset: 0x0041E5AC
		public override bool HandleEvent(QueryEquippableListEvent E)
		{
			if (E.RequireDesirable && E.Item == this.ParentObject && this.ParentObject.QueryChargeStorage(true, true, null) <= 0)
			{
				E.List.Remove(this.ParentObject);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4DE RID: 46302 RVA: 0x004203FB File Offset: 0x0041E5FB
		public override bool HandleEvent(AIGetPassiveItemListEvent E)
		{
			if (this.ParentObject.QueryChargeStorage(true, true, null) <= 0)
			{
				E.Add("CommandUnequipObject", 1, null, false, false, this.ParentObject, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4DF RID: 46303 RVA: 0x0042042C File Offset: 0x0041E62C
		public override bool HandleEvent(TakenEvent E)
		{
			if (E.Actor != null && !E.Actor.IsPlayer() && E.Actor.Brain != null && this.ParentObject.QueryChargeStorage(true, true, null) > 0)
			{
				E.Actor.Brain.DoReequip = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4E0 RID: 46304 RVA: 0x00420484 File Offset: 0x0041E684
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B4E1 RID: 46305 RVA: 0x00420487 File Offset: 0x0041E687
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.PerformCharging(Amount, true);
		}

		// Token: 0x0600B4E2 RID: 46306 RVA: 0x00420494 File Offset: 0x0041E694
		private void PerformCharging(int Turns = 1, bool CombatPossible = false)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = ((CombatPossible && this.AlternateChargeRateInCombat && !this.ForeachActivePartSubjectWhile(new Predicate<GameObject>(EquipCharge.CheckCombat), false)) ? this.CombatChargeRate : this.ChargeRate);
				if (!this.EquipperProperty.IsNullOrEmpty())
				{
					int intProperty = this.ParentObject.Equipped.GetIntProperty(this.EquipperProperty, 0);
					if (num > intProperty)
					{
						num = intProperty;
					}
				}
				if (this.ChargeUse > 0)
				{
					num -= this.ChargeUse;
				}
				if (num > 0)
				{
					this.ParentObject.ChargeAvailable(num, 0L, Turns, false, null);
				}
			}
		}

		// Token: 0x0600B4E3 RID: 46307 RVA: 0x0042054E File Offset: 0x0041E74E
		public static bool CheckCombat(GameObject obj)
		{
			return obj.OnWorldMap() || !obj.AreHostilesNearby();
		}

		// Token: 0x040048E4 RID: 18660
		public int ChargeRate = 1;

		// Token: 0x040048E5 RID: 18661
		public string EquipperProperty;

		// Token: 0x040048E6 RID: 18662
		public bool Describe;

		// Token: 0x040048E7 RID: 18663
		public string PropertyConditionDescription;

		// Token: 0x040048E8 RID: 18664
		public bool AlternateChargeRateInCombat;

		// Token: 0x040048E9 RID: 18665
		public int CombatChargeRate = 2;
	}
}
