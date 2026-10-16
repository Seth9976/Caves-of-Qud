using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02001228 RID: 4648
	[Serializable]
	public class Shield : IPart
	{
		// Token: 0x0600B868 RID: 47208 RVA: 0x0043130C File Offset: 0x0042F50C
		public override bool SameAs(IPart p)
		{
			Shield shield = p as Shield;
			return !(shield.WornOn != this.WornOn) && shield.AV == this.AV && shield.DV == this.DV && shield.SpeedPenalty == this.SpeedPenalty && base.SameAs(p);
		}

		// Token: 0x0600B869 RID: 47209 RVA: 0x0043136C File Offset: 0x0042F56C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetShieldBlockPreferenceEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == QueryEquippableListEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600B86A RID: 47210 RVA: 0x004313AA File Offset: 0x0042F5AA
		public override bool HandleEvent(GetShieldBlockPreferenceEvent E)
		{
			E.Preference += this.AV * 100;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B86B RID: 47211 RVA: 0x004313CC File Offset: 0x0042F5CC
		public override bool HandleEvent(QueryEquippableListEvent E)
		{
			if (E.SlotType == this.WornOn && E.Item == this.ParentObject && !E.List.Contains(E.Item) && (!E.RequireDesirable || E.Actor.HasSkill("Shield_Block")))
			{
				if (!E.RequirePossible)
				{
					E.List.Add(E.Item);
				}
				else
				{
					string usesSlots = E.Item.UsesSlots;
					if (!usesSlots.IsNullOrEmpty() && (E.SlotType != "Thrown Weapon" || usesSlots.Contains("Thrown Weapon")) && (E.SlotType != "Hand" || usesSlots.Contains("Hand")))
					{
						if (E.Actor.IsGiganticCreature)
						{
							if (E.Item.IsGiganticEquipment || E.Item.HasPropertyOrTag("GiganticEquippable") || E.Item.IsNatural())
							{
								E.List.Add(E.Item);
							}
						}
						else if (E.SlotType == "Hand" || E.SlotType == "Missile Weapon" || !E.Item.IsGiganticEquipment || !E.Item.IsNatural())
						{
							E.List.Add(E.Item);
						}
					}
					else if (!E.Actor.IsGiganticCreature || E.Item.IsGiganticEquipment || E.Item.HasPropertyOrTag("GiganticEquippable") || E.Item.IsNatural())
					{
						int slotsRequiredFor = E.Item.GetSlotsRequiredFor(E.Actor, E.SlotType, false);
						if (slotsRequiredFor > 0 && slotsRequiredFor <= E.Actor.GetBodyPartCount(E.SlotType))
						{
							E.List.Add(E.Item);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B86C RID: 47212 RVA: 0x004315D4 File Offset: 0x0042F7D4
		public override bool HandleEvent(EquippedEvent E)
		{
			base.StatShifter.SetStatShift(E.Actor, "DV", this.DV, false);
			base.StatShifter.SetStatShift(E.Actor, "Speed", -this.SpeedPenalty, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B86D RID: 47213 RVA: 0x00431625 File Offset: 0x0042F825
		public override bool HandleEvent(UnequippedEvent E)
		{
			base.StatShifter.RemoveStatShifts(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B86E RID: 47214 RVA: 0x00431640 File Offset: 0x0042F840
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (this.DisplayStats && E.Understood())
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				stringBuilder.Append("{{b|").Append('\u0004').Append("}}")
					.Append(this.AV)
					.Append(" {{K|")
					.Append('\t')
					.Append("}}")
					.Append(this.DV);
				E.AddTag(stringBuilder.ToString(), -20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B86F RID: 47215 RVA: 0x004316C6 File Offset: 0x0042F8C6
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Shields only grant their AV when you successfully block an attack.");
			return base.HandleEvent(E);
		}

		// Token: 0x04004A49 RID: 19017
		public string WornOn = "Arm";

		// Token: 0x04004A4A RID: 19018
		public int AV;

		// Token: 0x04004A4B RID: 19019
		public int DV;

		// Token: 0x04004A4C RID: 19020
		public int SpeedPenalty;

		// Token: 0x04004A4D RID: 19021
		[FieldSaveVersion(399)]
		public bool DisplayStats = true;

		// Token: 0x04004A4E RID: 19022
		[NonSerialized]
		public int Blocks;
	}
}
