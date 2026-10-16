using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010E5 RID: 4325
	[Serializable]
	public class LeaderShiftShare : IPart
	{
		// Token: 0x0600ABA1 RID: 43937 RVA: 0x003EE1AC File Offset: 0x003EC3AC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquipperEquippedEvent.ID;
		}

		// Token: 0x0600ABA2 RID: 43938 RVA: 0x003EE1C4 File Offset: 0x003EC3C4
		public override bool HandleEvent(EquipperEquippedEvent E)
		{
			GameObject partyLeader = this.ParentObject.PartyLeader;
			if (partyLeader == null)
			{
				return base.HandleEvent(E);
			}
			if ((this.RequiresAncestor.IsNullOrEmpty() || E.Item.GetBlueprint(true).DescendsFrom(this.RequiresAncestor)) && (this.RequiresPart.IsNullOrEmpty() || E.Item.HasPart(this.RequiresPart)))
			{
				Effect effect = partyLeader.GetEffect(delegate(Effect fx)
				{
					ShiftShareEffect shiftShareEffect = fx as ShiftShareEffect;
					return shiftShareEffect != null && shiftShareEffect.Source == E.Item;
				});
				if (effect != null)
				{
					partyLeader.RemoveEffect(effect, true);
				}
				partyLeader.ApplyEffect(new ShiftShareEffect
				{
					Duration = 9999,
					Source = E.Item
				}, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040046E4 RID: 18148
		public string RequiresAncestor;

		// Token: 0x040046E5 RID: 18149
		public string RequiresPart;
	}
}
