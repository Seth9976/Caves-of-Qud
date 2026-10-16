using System;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000C32 RID: 3122
	[Serializable]
	public class ArtifactDetectionEffect : Effect
	{
		// Token: 0x06007F17 RID: 32535 RVA: 0x002EEC5D File Offset: 0x002ECE5D
		public ArtifactDetectionEffect()
		{
			this.Duration = 1;
		}

		// Token: 0x06007F18 RID: 32536 RVA: 0x002EEC73 File Offset: 0x002ECE73
		public ArtifactDetectionEffect(ArtifactDetection Detector, Examiner Examiner)
			: this()
		{
			this.Detector = Detector;
			this.Examiner = Examiner;
			this.Initialized = Examiner != null && Detector != null;
		}

		// Token: 0x06007F19 RID: 32537 RVA: 0x002EEC99 File Offset: 0x002ECE99
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06007F1A RID: 32538 RVA: 0x002EEC9C File Offset: 0x002ECE9C
		public override bool SuppressInLookDisplay()
		{
			return true;
		}

		// Token: 0x06007F1B RID: 32539 RVA: 0x002EEC9F File Offset: 0x002ECE9F
		public override int GetEffectType()
		{
			return 64;
		}

		// Token: 0x06007F1C RID: 32540 RVA: 0x002EECA3 File Offset: 0x002ECEA3
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x06007F1D RID: 32541 RVA: 0x002EECA8 File Offset: 0x002ECEA8
		public bool Initialize()
		{
			if (this.Initialized)
			{
				return true;
			}
			this.Initialized = true;
			this.Detector = this.Source.GetPart<ArtifactDetection>();
			this.Examiner = base.Object.GetPart<Examiner>();
			return this.Examiner != null && this.Detector != null;
		}

		// Token: 0x06007F1E RID: 32542 RVA: 0x002EECFC File Offset: 0x002ECEFC
		public bool CheckDetection()
		{
			if (!GameObject.Validate(ref this.Source) || base.Object.CurrentCell == null)
			{
				return this.Unapply();
			}
			if (!this.Initialize() || !this.Detector.WasReady())
			{
				return this.Unapply();
			}
			if (!this.Detector.DetectKnown && this.Examiner.GetEpistemicStatus() == 2)
			{
				return this.Unapply();
			}
			int num = base.Object.DistanceTo(The.Player);
			if (num > this.Radius)
			{
				return this.Unapply();
			}
			if (!this.Identified && this.IdentifyChance > 0 && ((int)((double)(100 + this.IdentifyChance) / Math.Pow((double)(num + 9), 2.0) * 100.0)).in100())
			{
				this.Identified = true;
			}
			return true;
		}

		// Token: 0x06007F1F RID: 32543 RVA: 0x002EEDD2 File Offset: 0x002ECFD2
		public override bool Apply(GameObject Object)
		{
			return this.Initialize() && this.Examiner.Complexity >= 0 && this.CheckDetection();
		}

		// Token: 0x06007F20 RID: 32544 RVA: 0x002EEDF4 File Offset: 0x002ECFF4
		private bool Unapply()
		{
			this.Source = null;
			base.Object.RemoveEffect(this, true);
			return false;
		}

		// Token: 0x06007F21 RID: 32545 RVA: 0x002EEE0C File Offset: 0x002ED00C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x06007F22 RID: 32546 RVA: 0x002EEE22 File Offset: 0x002ED022
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.CheckDetection();
			return base.HandleEvent(E);
		}

		// Token: 0x06007F23 RID: 32547 RVA: 0x002EEE34 File Offset: 0x002ED034
		public bool ValidTarget(GameObject Object)
		{
			Cell currentCell = Object.CurrentCell;
			if (currentCell == null)
			{
				this.Unapply();
				return false;
			}
			return !Object.IsVisible() || (!currentCell.IsLit() || !currentCell.IsExplored());
		}

		// Token: 0x06007F24 RID: 32548 RVA: 0x002EEE74 File Offset: 0x002ED074
		public override bool FinalRender(RenderEvent E)
		{
			if (E.CustomDraw)
			{
				return true;
			}
			if (!this.ValidTarget(base.Object))
			{
				return true;
			}
			if (!this.Initialize())
			{
				this.Unapply();
				return true;
			}
			if (!this.Detector.WasReady())
			{
				this.Unapply();
				return true;
			}
			if (this.Identified)
			{
				E.HighestLayer = 0;
				E.RenderString = base.Object.Render.RenderString;
				E.Tile = (Options.UseTiles ? base.Object.Render.Tile : null);
				base.Object.ComponentRender(E);
			}
			else
			{
				this.Examiner.Render(E);
			}
			E.CustomDraw = true;
			return false;
		}

		// Token: 0x040038B8 RID: 14520
		public int Radius = 1;

		// Token: 0x040038B9 RID: 14521
		public int IdentifyChance;

		// Token: 0x040038BA RID: 14522
		public bool Identified;

		// Token: 0x040038BB RID: 14523
		public GameObject Source;

		// Token: 0x040038BC RID: 14524
		[NonSerialized]
		private ArtifactDetection Detector;

		// Token: 0x040038BD RID: 14525
		[NonSerialized]
		private Examiner Examiner;

		// Token: 0x040038BE RID: 14526
		[NonSerialized]
		private bool Initialized;
	}
}
