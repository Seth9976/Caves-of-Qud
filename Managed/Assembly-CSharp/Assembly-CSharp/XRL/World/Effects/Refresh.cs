using System;
using XRL.Core;

namespace XRL.World.Effects
{
	// Token: 0x02000D78 RID: 3448
	[Serializable]
	public class Refresh : Effect
	{
		// Token: 0x060088CE RID: 35022 RVA: 0x00310414 File Offset: 0x0030E614
		public Refresh()
		{
			this.DisplayName = "{{G|refreshed}}";
		}

		// Token: 0x060088CF RID: 35023 RVA: 0x0031042E File Offset: 0x0030E62E
		public Refresh(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x060088D0 RID: 35024 RVA: 0x0031043D File Offset: 0x0030E63D
		public override int GetEffectType()
		{
			return 4;
		}

		// Token: 0x060088D1 RID: 35025 RVA: 0x00310440 File Offset: 0x0030E640
		public override bool SameAs(Effect e)
		{
			return (e as Refresh).Damage == this.Damage && base.SameAs(e);
		}

		// Token: 0x060088D2 RID: 35026 RVA: 0x0031045E File Offset: 0x0030E65E
		public override string GetDetails()
		{
			return "5% HP healed per turn.";
		}

		// Token: 0x060088D3 RID: 35027 RVA: 0x00310468 File Offset: 0x0030E668
		public override bool Apply(GameObject Object)
		{
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_positiveVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return true;
		}

		// Token: 0x060088D4 RID: 35028 RVA: 0x003104A3 File Offset: 0x0030E6A3
		public override void Remove(GameObject Object)
		{
			base.Remove(Object);
		}

		// Token: 0x060088D5 RID: 35029 RVA: 0x003104AC File Offset: 0x0030E6AC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID;
		}

		// Token: 0x060088D6 RID: 35030 RVA: 0x003104C4 File Offset: 0x0030E6C4
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0)
			{
				if (base.Object.HasStat("Hitpoints"))
				{
					base.Object.Heal(Math.Max(base.Object.GetStat("Hitpoints").BaseValue / 20, 1), false, false, false);
				}
				if (this.Duration != 9999)
				{
					this.Duration--;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060088D7 RID: 35031 RVA: 0x0031053B File Offset: 0x0030E73B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Recuperating");
			base.Register(Object, Registrar);
		}

		// Token: 0x060088D8 RID: 35032 RVA: 0x00310550 File Offset: 0x0030E750
		public override bool Render(RenderEvent E)
		{
			if (this.Duration <= 0)
			{
				return true;
			}
			int num = XRLCore.CurrentFrame % 60;
			if (num > 35 && num < 45)
			{
				E.Tile = null;
				E.RenderString = "\u0001";
				E.ColorString = "&G";
			}
			return true;
		}

		// Token: 0x04003A61 RID: 14945
		public int Damage = 2;
	}
}
