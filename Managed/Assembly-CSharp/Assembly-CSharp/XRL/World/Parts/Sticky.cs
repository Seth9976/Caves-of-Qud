using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200128D RID: 4749
	[Serializable]
	public class Sticky : IPart
	{
		// Token: 0x0600BBDC RID: 48092 RVA: 0x0044991D File Offset: 0x00447B1D
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BBDD RID: 48093 RVA: 0x00449920 File Offset: 0x00447B20
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetNavigationWeightEvent.ID || ID == LeftCellEvent.ID || ID == ObjectEnteredCellEvent.ID || ID == OnDestroyObjectEvent.ID;
		}

		// Token: 0x0600BBDE RID: 48094 RVA: 0x0044994E File Offset: 0x00447B4E
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (E.Smart && !E.Juggernaut && E.PhaseMatches(this.ParentObject))
			{
				E.MinWeight(E.Autoexploring ? 70 : 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BBDF RID: 48095 RVA: 0x00449988 File Offset: 0x00447B88
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (!E.Object.HasEffect<Greased>() && E.Object.GetMatterPhase() <= 1 && !E.Object.HasTag("ExcavatoryTerrainFeature") && E.Object.PhaseMatches(this.ParentObject) && !this.ParentObject.IsBroken() && !this.ParentObject.IsRusted())
			{
				E.Object.ApplyEffect(new Stuck(this.Duration, this.SaveTarget, this.SaveVs, this.DestroyOnBreak ? this.ParentObject : null, "stuck", "in", this.ParentObject.ID, "Move", 0.25f, 1f, 1f, false, false), null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BBE0 RID: 48096 RVA: 0x00449A5F File Offset: 0x00447C5F
		public override bool HandleEvent(LeftCellEvent E)
		{
			this.StripStuck(E.Cell);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BBE1 RID: 48097 RVA: 0x00449A74 File Offset: 0x00447C74
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			this.StripStuck(this.ParentObject.CurrentCell);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BBE2 RID: 48098 RVA: 0x00449A8E File Offset: 0x00447C8E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyStuck");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BBE3 RID: 48099 RVA: 0x00449AA3 File Offset: 0x00447CA3
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "ApplyStuck") && base.FireEvent(E);
		}

		// Token: 0x0600BBE4 RID: 48100 RVA: 0x00449AC0 File Offset: 0x00447CC0
		private bool IsOurs(Effect GFX)
		{
			Stuck stuck = GFX as Stuck;
			return stuck != null && this.ParentObject.IDMatch(stuck.DependsOn);
		}

		// Token: 0x0600BBE5 RID: 48101 RVA: 0x00449AF0 File Offset: 0x00447CF0
		private void StripStuck(Cell C)
		{
			if (C != null)
			{
				int i = 0;
				int count = C.Objects.Count;
				while (i < count)
				{
					Effect effect = C.Objects[i].GetEffect(typeof(Stuck), new Predicate<Effect>(this.IsOurs));
					if (effect != null)
					{
						effect.Duration = 0;
					}
					i++;
				}
			}
		}

		// Token: 0x04004B4B RID: 19275
		public bool DestroyOnBreak;

		// Token: 0x04004B4C RID: 19276
		[Obsolete("Retired sticky checking weight")]
		public int MaxWeight = 1000;

		// Token: 0x04004B4D RID: 19277
		public int SaveTarget = 15;

		// Token: 0x04004B4E RID: 19278
		public string SaveVs = "Web Stuck Restraint";

		// Token: 0x04004B4F RID: 19279
		public int Duration = 12;
	}
}
