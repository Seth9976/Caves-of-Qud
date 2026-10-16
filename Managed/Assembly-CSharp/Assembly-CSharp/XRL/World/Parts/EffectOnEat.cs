using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F7F RID: 3967
	[Serializable]
	public class EffectOnEat : IPart
	{
		// Token: 0x06009DC6 RID: 40390 RVA: 0x003921D4 File Offset: 0x003903D4
		public override bool SameAs(IPart p)
		{
			EffectOnEat effectOnEat = p as EffectOnEat;
			return effectOnEat.Effect == this.Effect && effectOnEat.Duration == this.Duration;
		}

		// Token: 0x06009DC7 RID: 40391 RVA: 0x0039220E File Offset: 0x0039040E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009DC8 RID: 40392 RVA: 0x00392224 File Offset: 0x00390424
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				Effect effect = Activator.CreateInstance(ModManager.ResolveType("XRL.World.Effects." + this.Effect, false, false, true)) as Effect;
				if (!string.IsNullOrEmpty(this.Duration))
				{
					effect.Duration = this.Duration.RollCached();
				}
				if (!string.IsNullOrEmpty(this.DisplayName))
				{
					effect.DisplayName = this.DisplayName;
				}
				E.GetGameObjectParameter("Eater").ApplyEffect(effect, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400413B RID: 16699
		public string DisplayName;

		// Token: 0x0400413C RID: 16700
		public string Effect = "CookingDomainReflect_Reflect100_ProceduralCookingTriggeredAction_Effect";

		// Token: 0x0400413D RID: 16701
		public string Duration;
	}
}
