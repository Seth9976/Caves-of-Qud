using System;
using System.Linq;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000DAF RID: 3503
	[Serializable]
	public class IsAFollowerGettingTeleport : Effect
	{
		// Token: 0x06008BE7 RID: 35815 RVA: 0x0031AE4D File Offset: 0x0031904D
		public IsAFollowerGettingTeleport()
		{
			this.Duration = 3;
		}

		// Token: 0x06008BE8 RID: 35816 RVA: 0x0031AE67 File Offset: 0x00319067
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x06008BE9 RID: 35817 RVA: 0x0031AE6A File Offset: 0x0031906A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x06008BEA RID: 35818 RVA: 0x0031AE80 File Offset: 0x00319080
		public override bool Apply(GameObject Object)
		{
			if (this.effectGuid == Guid.Empty)
			{
				ItemEffectBonusMutation<Teleportation> itemEffectBonusMutation = new ItemEffectBonusMutation<Teleportation>();
				itemEffectBonusMutation.DisplayName = "{{C|spurred to teleport}}";
				if (Object.ApplyEffect(itemEffectBonusMutation, null))
				{
					this.effectGuid = itemEffectBonusMutation.ID;
				}
			}
			return base.Apply(Object);
		}

		// Token: 0x06008BEB RID: 35819 RVA: 0x0031AED0 File Offset: 0x003190D0
		public override void Remove(GameObject Object)
		{
			if (this.effectGuid != Guid.Empty)
			{
				Effect effect = Object.Effects.Where<Effect>((Effect e) => e.ID == this.effectGuid).FirstOrDefault<Effect>();
				if (effect != null)
				{
					effect.Duration = 0;
					effect.Remove(Object);
				}
			}
			base.Remove(Object);
		}

		// Token: 0x06008BEC RID: 35820 RVA: 0x0031AF24 File Offset: 0x00319124
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.Duration--;
			return base.HandleEvent(E);
		}

		// Token: 0x04003AE5 RID: 15077
		public Guid effectGuid = Guid.Empty;
	}
}
