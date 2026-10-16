using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D90 RID: 3472
	[Serializable]
	public class SporeCloudPoison : Effect, ITierInitialized
	{
		// Token: 0x06008A3E RID: 35390 RVA: 0x0031532B File Offset: 0x0031352B
		public SporeCloudPoison()
		{
			this.DisplayName = "{{W|covered in spores}}";
		}

		// Token: 0x06008A3F RID: 35391 RVA: 0x00315345 File Offset: 0x00313545
		public SporeCloudPoison(int Duration, GameObject Owner)
			: this()
		{
			this.Owner = Owner;
			this.Duration = Duration;
		}

		// Token: 0x06008A40 RID: 35392 RVA: 0x0031535B File Offset: 0x0031355B
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(2, 5);
		}

		// Token: 0x06008A41 RID: 35393 RVA: 0x0031536A File Offset: 0x0031356A
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008A42 RID: 35394 RVA: 0x0031536D File Offset: 0x0031356D
		public override int GetEffectType()
		{
			return 117440516;
		}

		// Token: 0x06008A43 RID: 35395 RVA: 0x00315374 File Offset: 0x00313574
		public override bool SameAs(Effect e)
		{
			SporeCloudPoison sporeCloudPoison = e as SporeCloudPoison;
			return sporeCloudPoison.Damage == this.Damage && sporeCloudPoison.Owner == this.Owner && base.SameAs(e);
		}

		// Token: 0x06008A44 RID: 35396 RVA: 0x003153AF File Offset: 0x003135AF
		public override string GetDetails()
		{
			return this.Damage.ToString() + " damage per turn.";
		}

		// Token: 0x06008A45 RID: 35397 RVA: 0x003153C8 File Offset: 0x003135C8
		public override bool Apply(GameObject Object)
		{
			Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_poisoned", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			return Object.FireEvent(Event.New("ApplySporeCloudInfection", 0, 0, 0));
		}

		// Token: 0x06008A46 RID: 35398 RVA: 0x00315417 File Offset: 0x00313617
		public override void Remove(GameObject Object)
		{
		}

		// Token: 0x06008A47 RID: 35399 RVA: 0x00315419 File Offset: 0x00313619
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == SingletonEvent<GeneralAmnestyEvent>.ID;
		}

		// Token: 0x06008A48 RID: 35400 RVA: 0x00315438 File Offset: 0x00313638
		public override bool HandleEvent(EndTurnEvent E)
		{
			GameObject.Validate(ref this.Owner);
			if (this.Duration > 0)
			{
				base.Object.TakeDamage(this.Damage, "from %t spores!", "Poison Gas Fungal Spores Unavoidable", null, null, this.Owner, null, null, null, null, false, false, true, false, false, false, false, false, false, 5, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A49 RID: 35401 RVA: 0x00315494 File Offset: 0x00313694
		public override bool HandleEvent(GeneralAmnestyEvent E)
		{
			this.Owner = null;
			return base.HandleEvent(E);
		}

		// Token: 0x06008A4A RID: 35402 RVA: 0x003154A4 File Offset: 0x003136A4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Recuperating");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008A4B RID: 35403 RVA: 0x003154BC File Offset: 0x003136BC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Recuperating")
			{
				this.Duration = 0;
				base.DidX("shake", "the spores off", null, null, null, base.Object, null, false, false, null, null, false, false, false, false, false, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008A4C RID: 35404 RVA: 0x0031550D File Offset: 0x0031370D
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				E.RenderEffectIndicator("à", "Tiles2/status_spores.bmp", "&M", "M", 45, 10);
			}
			return true;
		}

		// Token: 0x04003A98 RID: 15000
		public int Damage = 2;

		// Token: 0x04003A99 RID: 15001
		public GameObject Owner;
	}
}
