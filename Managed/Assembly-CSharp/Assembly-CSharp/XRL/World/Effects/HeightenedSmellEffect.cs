using System;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D46 RID: 3398
	[Serializable]
	public class HeightenedSmellEffect : Effect
	{
		// Token: 0x06008637 RID: 34359 RVA: 0x00304C6C File Offset: 0x00302E6C
		public HeightenedSmellEffect()
		{
			this.Duration = 1;
		}

		// Token: 0x06008638 RID: 34360 RVA: 0x00304C82 File Offset: 0x00302E82
		public HeightenedSmellEffect(int Level, GameObject Smeller)
			: this()
		{
			this.Level = Level;
			this.Smeller = Smeller;
		}

		// Token: 0x06008639 RID: 34361 RVA: 0x00304C98 File Offset: 0x00302E98
		public override int GetEffectType()
		{
			return 512;
		}

		// Token: 0x0600863A RID: 34362 RVA: 0x00304C9F File Offset: 0x00302E9F
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600863B RID: 34363 RVA: 0x00304CA2 File Offset: 0x00302EA2
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x0600863C RID: 34364 RVA: 0x00304CA5 File Offset: 0x00302EA5
		public override void RegisterActive(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(The.Game, this, PooledEvent<AfterPlayerBodyChangeEvent>.ID, 0, false);
		}

		// Token: 0x0600863D RID: 34365 RVA: 0x00304CBA File Offset: 0x00302EBA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == EnteringCellEvent.ID;
		}

		// Token: 0x0600863E RID: 34366 RVA: 0x00304CD8 File Offset: 0x00302ED8
		public override bool HandleEvent(AfterPlayerBodyChangeEvent E)
		{
			this.CheckSmell();
			return base.HandleEvent(E);
		}

		// Token: 0x0600863F RID: 34367 RVA: 0x00304CE8 File Offset: 0x00302EE8
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.CheckSmell();
			return base.HandleEvent(E);
		}

		// Token: 0x06008640 RID: 34368 RVA: 0x00304CF8 File Offset: 0x00302EF8
		public override bool HandleEvent(EnteringCellEvent E)
		{
			this.CheckSmell();
			return base.HandleEvent(E);
		}

		// Token: 0x06008641 RID: 34369 RVA: 0x00304D08 File Offset: 0x00302F08
		private bool BadSmeller()
		{
			this.Smeller = null;
			GameObject @object = base.Object;
			if (@object != null)
			{
				@object.RemoveEffect(this, true);
			}
			return true;
		}

		// Token: 0x06008642 RID: 34370 RVA: 0x00304D28 File Offset: 0x00302F28
		public bool CheckSmell()
		{
			if (!GameObject.Validate(base.Object) || !GameObject.Validate(ref this.Smeller) || !this.Smeller.IsPlayer())
			{
				return this.BadSmeller();
			}
			HeightenedSmell part = this.Smeller.GetPart<HeightenedSmell>();
			if (part == null || part.Level <= 0)
			{
				return this.BadSmeller();
			}
			int num = base.Object.DistanceTo(this.Smeller);
			if (num > part.GetRadius())
			{
				return this.BadSmeller();
			}
			if (!base.Object.IsSmellable(this.Smeller))
			{
				return this.BadSmeller();
			}
			if (this.Identified)
			{
				return true;
			}
			if (base.Object.CurrentCell == null)
			{
				return true;
			}
			if (((int)((double)(100 + 20 * this.Level) / Math.Pow((double)(num + 9), 2.0) * 100.0)).in100())
			{
				this.Identified = true;
				if (this.Smeller.IsPlayer())
				{
					AutoAct.CheckHostileInterrupt();
				}
			}
			return false;
		}

		// Token: 0x06008643 RID: 34371 RVA: 0x00304E25 File Offset: 0x00303025
		public override bool Apply(GameObject Object)
		{
			this.CheckSmell();
			return true;
		}

		// Token: 0x06008644 RID: 34372 RVA: 0x00304E30 File Offset: 0x00303030
		public bool NotSeen(GameObject obj)
		{
			if (obj == null || obj.IsPlayer())
			{
				return false;
			}
			if (!obj.IsVisible())
			{
				return true;
			}
			Cell currentCell = obj.CurrentCell;
			return currentCell != null && (!currentCell.IsLit() || !currentCell.IsExplored());
		}

		// Token: 0x06008645 RID: 34373 RVA: 0x00304E74 File Offset: 0x00303074
		public override bool FinalRender(RenderEvent E)
		{
			if (this.NotSeen(base.Object) && base.Object.CanHypersensesDetect())
			{
				if (this.Identified)
				{
					E.HighestLayer = 0;
					E.NoWake = true;
					base.Object.ComponentRender(E);
					E.RenderString = base.Object.Render.RenderString;
					if (Options.UseTiles)
					{
						E.HFlip = base.Object.Render.getHFlip();
						E.VFlip = base.Object.Render.getVFlip();
						E.Tile = base.Object.Render.Tile;
					}
					else
					{
						E.Tile = null;
					}
				}
				else
				{
					E.Tile = null;
					E.RenderString = "?";
				}
				E.ColorString = "&w";
				E.DetailColor = "w";
				E.CustomDraw = true;
				return false;
			}
			return true;
		}

		// Token: 0x040039D3 RID: 14803
		public bool Identified;

		// Token: 0x040039D4 RID: 14804
		public int Level = 1;

		// Token: 0x040039D5 RID: 14805
		public GameObject Smeller;
	}
}
