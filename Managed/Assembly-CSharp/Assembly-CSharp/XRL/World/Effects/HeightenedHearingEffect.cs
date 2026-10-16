using System;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D45 RID: 3397
	[Serializable]
	public class HeightenedHearingEffect : Effect
	{
		// Token: 0x06008628 RID: 34344 RVA: 0x0030498F File Offset: 0x00302B8F
		public HeightenedHearingEffect()
		{
			this.Duration = 1;
		}

		// Token: 0x06008629 RID: 34345 RVA: 0x003049A5 File Offset: 0x00302BA5
		public HeightenedHearingEffect(int Level, GameObject Listener)
			: this()
		{
			this.Level = Level;
			this.Listener = Listener;
		}

		// Token: 0x0600862A RID: 34346 RVA: 0x003049BB File Offset: 0x00302BBB
		public override int GetEffectType()
		{
			return 2048;
		}

		// Token: 0x0600862B RID: 34347 RVA: 0x003049C2 File Offset: 0x00302BC2
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600862C RID: 34348 RVA: 0x003049C5 File Offset: 0x00302BC5
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x0600862D RID: 34349 RVA: 0x003049C8 File Offset: 0x00302BC8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == EnteringCellEvent.ID;
		}

		// Token: 0x0600862E RID: 34350 RVA: 0x003049E6 File Offset: 0x00302BE6
		public override bool HandleEvent(AfterPlayerBodyChangeEvent E)
		{
			this.CheckListen();
			return base.HandleEvent(E);
		}

		// Token: 0x0600862F RID: 34351 RVA: 0x003049F6 File Offset: 0x00302BF6
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.CheckListen();
			return base.HandleEvent(E);
		}

		// Token: 0x06008630 RID: 34352 RVA: 0x00304A06 File Offset: 0x00302C06
		public override bool HandleEvent(EnteringCellEvent E)
		{
			this.CheckListen();
			return base.HandleEvent(E);
		}

		// Token: 0x06008631 RID: 34353 RVA: 0x00304A16 File Offset: 0x00302C16
		private bool BadListener()
		{
			this.Listener = null;
			GameObject @object = base.Object;
			if (@object != null)
			{
				@object.RemoveEffect(this, true);
			}
			return true;
		}

		// Token: 0x06008632 RID: 34354 RVA: 0x00304A34 File Offset: 0x00302C34
		public bool CheckListen()
		{
			if (!GameObject.Validate(base.Object))
			{
				return true;
			}
			if (!GameObject.Validate(ref this.Listener) || !this.Listener.IsPlayer())
			{
				return this.BadListener();
			}
			HeightenedHearing part = this.Listener.GetPart<HeightenedHearing>();
			if (part == null || part.Level <= 0)
			{
				return this.BadListener();
			}
			int num = base.Object.DistanceTo(this.Listener);
			if (num > part.GetRadius())
			{
				return this.BadListener();
			}
			if (this.Identified)
			{
				return true;
			}
			if (base.Object.CurrentCell == null)
			{
				return true;
			}
			if (((int)((double)(100 + 10 * this.Level) / Math.Pow((double)(num + 9), 2.0) * 100.0)).in100())
			{
				this.Identified = true;
				if (this.Listener.IsPlayer())
				{
					AutoAct.CheckHostileInterrupt();
				}
			}
			return false;
		}

		// Token: 0x06008633 RID: 34355 RVA: 0x00304B19 File Offset: 0x00302D19
		public override bool Apply(GameObject Object)
		{
			this.CheckListen();
			return true;
		}

		// Token: 0x06008634 RID: 34356 RVA: 0x00304B23 File Offset: 0x00302D23
		public override void RegisterActive(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(The.Game, this, PooledEvent<AfterPlayerBodyChangeEvent>.ID, 0, false);
		}

		// Token: 0x06008635 RID: 34357 RVA: 0x00304B38 File Offset: 0x00302D38
		public bool HeardAndNotSeen(GameObject obj)
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

		// Token: 0x06008636 RID: 34358 RVA: 0x00304B7C File Offset: 0x00302D7C
		public override bool FinalRender(RenderEvent E)
		{
			if (this.HeardAndNotSeen(base.Object) && base.Object.CanHypersensesDetect())
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
				E.ColorString = "&K";
				E.DetailColor = "K";
				E.CustomDraw = true;
				return false;
			}
			return true;
		}

		// Token: 0x040039D0 RID: 14800
		public bool Identified;

		// Token: 0x040039D1 RID: 14801
		public int Level = 1;

		// Token: 0x040039D2 RID: 14802
		public GameObject Listener;
	}
}
