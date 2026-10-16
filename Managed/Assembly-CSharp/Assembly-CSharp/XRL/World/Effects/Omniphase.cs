using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D6A RID: 3434
	[Serializable]
	public class Omniphase : Effect, ITierInitialized
	{
		// Token: 0x060087E8 RID: 34792 RVA: 0x0030B8F0 File Offset: 0x00309AF0
		public Omniphase()
		{
			this.DisplayName = "{{Y|omniphase}}";
			this.FrameOffset = Stat.Random(1, 10000);
			this.Duration = 9999;
		}

		// Token: 0x060087E9 RID: 34793 RVA: 0x0030B93C File Offset: 0x00309B3C
		public Omniphase(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x060087EA RID: 34794 RVA: 0x0030B94B File Offset: 0x00309B4B
		public Omniphase(string SourceKey)
			: this()
		{
			this.SourceKey = SourceKey;
		}

		// Token: 0x060087EB RID: 34795 RVA: 0x0030B95A File Offset: 0x00309B5A
		public Omniphase(int Duration, string SourceKey, bool Visual = true)
			: this(Duration)
		{
			this.SourceKey = SourceKey;
			this.Visual = Visual;
		}

		// Token: 0x060087EC RID: 34796 RVA: 0x0030B974 File Offset: 0x00309B74
		public Omniphase(Omniphase Source)
			: this()
		{
			this.Duration = Source.Duration;
			this.FrameOffset = Source.FrameOffset;
			this.Tile = Source.Tile;
			this.RenderString = Source.RenderString;
			this.SourceKey = Source.SourceKey;
		}

		// Token: 0x060087ED RID: 34797 RVA: 0x0030B9C3 File Offset: 0x00309BC3
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(20, 100);
		}

		// Token: 0x060087EE RID: 34798 RVA: 0x0030B9D4 File Offset: 0x00309BD4
		public override int GetEffectType()
		{
			return 256;
		}

		// Token: 0x060087EF RID: 34799 RVA: 0x0030B9DC File Offset: 0x00309BDC
		public override bool SameAs(Effect e)
		{
			Omniphase omniphase = e as Omniphase;
			return !(omniphase.Tile != this.Tile) && !(omniphase.RenderString != this.RenderString) && base.SameAs(e);
		}

		// Token: 0x060087F0 RID: 34800 RVA: 0x0030BA21 File Offset: 0x00309C21
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060087F1 RID: 34801 RVA: 0x0030BA24 File Offset: 0x00309C24
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == WasDerivedFromEvent.ID;
		}

		// Token: 0x060087F2 RID: 34802 RVA: 0x0030BA3A File Offset: 0x00309C3A
		public override bool HandleEvent(WasDerivedFromEvent E)
		{
			E.Derivation.ForceApplyEffect(new Omniphase(this), null);
			return base.HandleEvent(E);
		}

		// Token: 0x060087F3 RID: 34803 RVA: 0x0030BA56 File Offset: 0x00309C56
		public override string GetDetails()
		{
			return "Physically interactive with both in-phase and out-of-phase creatures and objects.";
		}

		// Token: 0x060087F4 RID: 34804 RVA: 0x0030BA60 File Offset: 0x00309C60
		public override bool Apply(GameObject Object)
		{
			bool flag = Object.HasEffect<Omniphase>();
			if (!Object.FireEvent("ApplyOmniphase"))
			{
				return false;
			}
			if (!flag)
			{
				if (Object != null)
				{
					Object.PlayWorldSound("Sounds/Misc/sfx_phase_shiftOmni", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				Object.FireEvent("AfterOmniphaseStart");
			}
			this.Tile = Object.Render.Tile;
			this.RenderString = Object.Render.RenderString;
			base.FlushNavigationCaches();
			Object.Gravitate();
			return true;
		}

		// Token: 0x060087F5 RID: 34805 RVA: 0x0030BAF0 File Offset: 0x00309CF0
		public override void Remove(GameObject Object)
		{
			if (!Object.HasEffectOtherThan(typeof(Omniphase), this))
			{
				if (Object != null)
				{
					Object.PlayWorldSound("Sounds/Misc/sfx_phase_shiftIn", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				Object.FireEvent("AfterOmniphaseEnd");
			}
			base.FlushNavigationCaches();
			base.Remove(Object);
		}

		// Token: 0x060087F6 RID: 34806 RVA: 0x0030BB58 File Offset: 0x00309D58
		public override bool Render(RenderEvent E)
		{
			if (this.Visual && this.Duration > 0)
			{
				int num = (XRLCore.CurrentFrameLong10 + this.FrameOffset) % 10000;
				if ((num >= 2000 && num <= 2070) || (num >= 8380 && num <= 8450))
				{
					E.ColorString = "&Y";
				}
				else if ((num >= 3910 && num <= 3980) || (num > 6300 && num <= 6370))
				{
					E.ColorString = "&M";
				}
			}
			return true;
		}

		// Token: 0x04003A34 RID: 14900
		private int FrameOffset;

		// Token: 0x04003A35 RID: 14901
		public string Tile;

		// Token: 0x04003A36 RID: 14902
		public string RenderString = "@";

		// Token: 0x04003A37 RID: 14903
		public string SourceKey;

		// Token: 0x04003A38 RID: 14904
		public bool Visual = true;
	}
}
