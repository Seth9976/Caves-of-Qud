using System;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D34 RID: 3380
	[Serializable]
	public class EpistemicDisguise : Effect
	{
		// Token: 0x06008561 RID: 34145 RVA: 0x003028A4 File Offset: 0x00300AA4
		public EpistemicDisguise()
		{
			this.Duration = 1;
		}

		// Token: 0x06008562 RID: 34146 RVA: 0x003028B3 File Offset: 0x00300AB3
		public EpistemicDisguise(GameObject Sample)
			: this()
		{
			this.Sample = Sample;
		}

		// Token: 0x06008563 RID: 34147 RVA: 0x003028C4 File Offset: 0x00300AC4
		public static bool ApplyTo(GameObject Object, GameObject Sample)
		{
			EpistemicDisguise effect = Object.GetEffect<EpistemicDisguise>();
			if (effect != null)
			{
				effect.Sample = Sample;
				return true;
			}
			return Object.ApplyEffect(new EpistemicDisguise(Sample), null);
		}

		// Token: 0x06008564 RID: 34148 RVA: 0x003028F1 File Offset: 0x00300AF1
		public override bool SameAs(Effect FX)
		{
			return FX is EpistemicDisguise;
		}

		// Token: 0x06008565 RID: 34149 RVA: 0x003028FC File Offset: 0x00300AFC
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x06008566 RID: 34150 RVA: 0x00302900 File Offset: 0x00300B00
		public override void FinalizeRead(SerializationReader Reader)
		{
			Examiner examiner;
			if (base.Object.TryGetPart<Examiner>(out examiner) && !examiner.KeepTile)
			{
				this.Sample = examiner.GetActiveSample(examiner.EpistemicStatus);
				if (this.Sample != null)
				{
					return;
				}
			}
			base.Object.RemoveEffect(this, true);
		}

		// Token: 0x06008567 RID: 34151 RVA: 0x00302950 File Offset: 0x00300B50
		public override bool Render(RenderEvent E)
		{
			if (!E.AsIfKnown)
			{
				GameObject sample = this.Sample;
				Render render = sample.Render;
				E.RenderString = render.RenderString;
				E.HFlip = render.getHFlip();
				E.VFlip = render.getVFlip();
				if (The.Core.TilesEnabled && !string.IsNullOrEmpty(render.TileColor))
				{
					E.ColorString = render.TileColor;
				}
				else
				{
					E.ColorString = render.ColorString;
				}
				E.DetailColor = render.DetailColor;
				E.HighestLayer = render.RenderLayer;
				if (The.Core.TilesEnabled)
				{
					E.Tile = render.Tile;
				}
				sample.ComponentRender(E);
			}
			return base.Render(E);
		}

		// Token: 0x06008568 RID: 34152 RVA: 0x00302A0C File Offset: 0x00300C0C
		public override bool OverlayRender(RenderEvent E)
		{
			if (!E.AsIfKnown)
			{
				GameObject sample = this.Sample;
				Render render = sample.Render;
				E.RenderString = render.RenderString;
				E.HFlip = render.getHFlip();
				E.VFlip = render.getVFlip();
				E.HighestLayer = render.RenderLayer;
				if (The.Core.TilesEnabled)
				{
					E.Tile = render.Tile;
				}
				sample.OverlayRender(E);
			}
			return base.OverlayRender(E);
		}

		// Token: 0x040039B7 RID: 14775
		[NonSerialized]
		public GameObject Sample;
	}
}
