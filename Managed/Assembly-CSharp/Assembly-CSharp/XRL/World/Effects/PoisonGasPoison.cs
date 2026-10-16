using System;
using XRL.Core;
using XRL.Rules;
using XRL.World.Capabilities;

namespace XRL.World.Effects
{
	// Token: 0x02000D71 RID: 3441
	[Serializable]
	public class PoisonGasPoison : Effect, ITierInitialized
	{
		// Token: 0x0600884B RID: 34891 RVA: 0x0030D1C2 File Offset: 0x0030B3C2
		public PoisonGasPoison()
		{
			this.DisplayName = "{{G|poisoned by gas}}";
		}

		// Token: 0x0600884C RID: 34892 RVA: 0x0030D1DC File Offset: 0x0030B3DC
		public PoisonGasPoison(int Duration, GameObject Owner)
			: this()
		{
			this.Owner = Owner;
			this.Duration = Duration;
		}

		// Token: 0x0600884D RID: 34893 RVA: 0x0030D1F4 File Offset: 0x0030B3F4
		public void Initialize(int Tier)
		{
			Tier = Tier.Constrain(Stat.Random(Tier - 2, Tier + 2));
			this.Duration = Stat.Random(4, 10);
			this.Damage = Stat.Roll(((int)((double)Tier * 1.5)).ToString() + "d2", null);
		}

		// Token: 0x0600884E RID: 34894 RVA: 0x0030D24C File Offset: 0x0030B44C
		public override int GetEffectType()
		{
			return 117506056;
		}

		// Token: 0x0600884F RID: 34895 RVA: 0x0030D253 File Offset: 0x0030B453
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008850 RID: 34896 RVA: 0x0030D256 File Offset: 0x0030B456
		public override string GetDetails()
		{
			return this.Damage.ToString() + " damage per turn.";
		}

		// Token: 0x06008851 RID: 34897 RVA: 0x0030D270 File Offset: 0x0030B470
		public override bool Apply(GameObject Object)
		{
			Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_poisoned", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			return Object.FireEvent("ApplyPoisonGasPoison") && ApplyEffectEvent.Check(Object, "PoisonGasPoison", this, null);
		}

		// Token: 0x06008852 RID: 34898 RVA: 0x0030D2C3 File Offset: 0x0030B4C3
		public override void Remove(GameObject Object)
		{
			base.Remove(Object);
		}

		// Token: 0x06008853 RID: 34899 RVA: 0x0030D2CC File Offset: 0x0030B4CC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeforeBeginTakeActionEvent>.ID || ID == SingletonEvent<GeneralAmnestyEvent>.ID;
		}

		// Token: 0x06008854 RID: 34900 RVA: 0x0030D2EC File Offset: 0x0030B4EC
		public override bool HandleEvent(BeforeBeginTakeActionEvent E)
		{
			GameObject.Validate(ref this.Owner);
			if (this.Duration > 0 && !base.Object.CurrentCell.HasObjectWithPart("GasPoison"))
			{
				base.Object.TakeDamage(this.Damage, "from %t {{g|poison}}!", "Poison Gas Unavoidable", null, null, this.Owner, null, null, null, null, false, false, true, false, false, false, false, false, false, 5, null);
				if (this.Duration > 0 && this.Duration != 9999)
				{
					this.Duration--;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008855 RID: 34901 RVA: 0x0030D383 File Offset: 0x0030B583
		public override bool HandleEvent(GeneralAmnestyEvent E)
		{
			this.Owner = null;
			return base.HandleEvent(E);
		}

		// Token: 0x06008856 RID: 34902 RVA: 0x0030D393 File Offset: 0x0030B593
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Recuperating");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008857 RID: 34903 RVA: 0x0030D3A8 File Offset: 0x0030B5A8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Recuperating")
			{
				this.Duration = 0;
				base.DidX("are", "no longer poisoned", "!", null, null, base.Object, null, false, false, null, null, false, false, false, false, false, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008858 RID: 34904 RVA: 0x0030D400 File Offset: 0x0030B600
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (num > 35 && num < 45)
			{
				E.Tile = null;
				E.RenderString = "!";
				E.ColorString = "&g^c";
			}
			return true;
		}

		// Token: 0x04003A49 RID: 14921
		public int Damage = 2;

		// Token: 0x04003A4A RID: 14922
		public GameObject Owner;
	}
}
