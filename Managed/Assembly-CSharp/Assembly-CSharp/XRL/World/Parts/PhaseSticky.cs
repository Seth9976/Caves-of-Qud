using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001279 RID: 4729
	[Serializable]
	public class PhaseSticky : IPart
	{
		// Token: 0x0600BB37 RID: 47927 RVA: 0x00445EFB File Offset: 0x004440FB
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BB38 RID: 47928 RVA: 0x00445EFE File Offset: 0x004440FE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID || ID == GetNavigationWeightEvent.ID || ID == LeftCellEvent.ID || ID == ObjectEnteredCellEvent.ID || ID == OnDestroyObjectEvent.ID || ID == PooledEvent<RealityStabilizeEvent>.ID;
		}

		// Token: 0x0600BB39 RID: 47929 RVA: 0x00445F3C File Offset: 0x0044413C
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				if (this.ParentObject.HasTag("Astral"))
				{
					this.ParentObject.Render.Tile = null;
					int num = (XRLCore.CurrentFrame + this.FrameOffset) % 400;
					if (num < 4)
					{
						this.ParentObject.Render.ColorString = "&Y";
						this.ParentObject.Render.DetailColor = "k";
					}
					else if (num < 8)
					{
						this.ParentObject.Render.ColorString = "&y";
						this.ParentObject.Render.DetailColor = "K";
					}
					else if (num < 12)
					{
						this.ParentObject.Render.ColorString = "&k";
						this.ParentObject.Render.DetailColor = "y";
					}
					else
					{
						this.ParentObject.Render.ColorString = "&K";
						this.ParentObject.Render.DetailColor = "y";
					}
					if (!Options.DisableTextAnimationEffects)
					{
						this.FrameOffset += Stat.Random(0, 20);
					}
					if (Stat.Random(1, 400) == 1 || this.FlickerFrame > 0)
					{
						this.ParentObject.Render.ColorString = "&K";
					}
				}
				else
				{
					int num = (XRLCore.CurrentFrame + this.FrameOffset) % 60;
					num /= 2;
					num %= 6;
					string text = null;
					if (num == 0)
					{
						text = "&k";
					}
					else if (num == 1)
					{
						text = "&K";
					}
					else if (num == 2)
					{
						text = "&c";
					}
					else if (num == 4)
					{
						text = "&y";
					}
					if (!text.IsNullOrEmpty())
					{
						E.ApplyColors(text, PhaseSticky.ICON_COLOR_PRIORITY);
					}
				}
			}
			return base.Render(E);
		}

		// Token: 0x0600BB3A RID: 47930 RVA: 0x004460FA File Offset: 0x004442FA
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (E.Smart && !E.Juggernaut && E.PhaseMatches(this.ParentObject))
			{
				E.MinWeight(E.Autoexploring ? 75 : 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB3B RID: 47931 RVA: 0x00446134 File Offset: 0x00444334
		public override bool HandleEvent(RealityStabilizeEvent E)
		{
			if (E.Check(false))
			{
				Sticky sticky = this.ParentObject.RequirePart<Sticky>(false);
				sticky.DestroyOnBreak = this.DestroyOnBreak;
				sticky.SaveTarget = this.SaveTarget;
				sticky.Duration = this.Duration;
				this.ParentObject.RemovePart(this);
				if (this.ParentObject.DisplayNameOnlyDirect == "phase web")
				{
					this.ParentObject.DisplayName = "web";
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB3C RID: 47932 RVA: 0x004461B4 File Offset: 0x004443B4
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (!E.Object.HasEffect<Greased>() && E.Object.GetMatterPhase() <= 1 && !E.Object.HasTag("ExcavatoryTerrainFeature") && E.Object.PhaseMatches(this.ParentObject) && !this.ParentObject.IsBroken() && !this.ParentObject.IsRusted() && E.Object.ApplyEffect(new Stuck(12, this.SaveTarget, "Web Stuck Restraint", this.DestroyOnBreak ? this.ParentObject : null, "stuck", "in", this.ParentObject.ID, "Move", 0.25f, 1f, 1f, false, false), null))
			{
				E.Object.ApplyEffect(new PhasedWhileStuck(4), null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB3D RID: 47933 RVA: 0x0044629D File Offset: 0x0044449D
		public override bool HandleEvent(LeftCellEvent E)
		{
			this.StripStuck(E.Cell);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB3E RID: 47934 RVA: 0x004462B2 File Offset: 0x004444B2
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			this.StripStuck(this.ParentObject.CurrentCell);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB3F RID: 47935 RVA: 0x004462CC File Offset: 0x004444CC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyStuck");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BB40 RID: 47936 RVA: 0x004462E1 File Offset: 0x004444E1
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "ApplyStuck") && base.FireEvent(E);
		}

		// Token: 0x0600BB41 RID: 47937 RVA: 0x00446300 File Offset: 0x00444500
		private bool IsOurs(Effect GFX)
		{
			Stuck stuck = GFX as Stuck;
			return stuck != null && stuck.DestroyOnBreak == this.ParentObject;
		}

		// Token: 0x0600BB42 RID: 47938 RVA: 0x00446328 File Offset: 0x00444528
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

		// Token: 0x04004B25 RID: 19237
		public static readonly int ICON_COLOR_PRIORITY = 30;

		// Token: 0x04004B26 RID: 19238
		public bool DestroyOnBreak;

		// Token: 0x04004B27 RID: 19239
		[Obsolete("Retired sticky max weight checking")]
		public int MaxWeight = 1000;

		// Token: 0x04004B28 RID: 19240
		public int SaveTarget = 15;

		// Token: 0x04004B29 RID: 19241
		public int Duration = 12;

		// Token: 0x04004B2A RID: 19242
		private int FrameOffset;

		// Token: 0x04004B2B RID: 19243
		private int FlickerFrame;
	}
}
