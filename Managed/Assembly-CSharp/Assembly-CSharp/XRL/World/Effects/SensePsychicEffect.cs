using System;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D81 RID: 3457
	[Serializable]
	public class SensePsychicEffect : Effect
	{
		// Token: 0x06008948 RID: 35144 RVA: 0x00311F63 File Offset: 0x00310163
		public SensePsychicEffect()
		{
			this.Duration = 1;
		}

		// Token: 0x06008949 RID: 35145 RVA: 0x00311F79 File Offset: 0x00310179
		public SensePsychicEffect(int Level, GameObject Listener)
			: this()
		{
			this.Level = Level;
			this.Listener = Listener;
		}

		// Token: 0x0600894A RID: 35146 RVA: 0x00311F8F File Offset: 0x0031018F
		public override int GetEffectType()
		{
			return 32770;
		}

		// Token: 0x0600894B RID: 35147 RVA: 0x00311F96 File Offset: 0x00310196
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600894C RID: 35148 RVA: 0x00311F99 File Offset: 0x00310199
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x0600894D RID: 35149 RVA: 0x00311F9C File Offset: 0x0031019C
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

		// Token: 0x0600894E RID: 35150 RVA: 0x00311FBC File Offset: 0x003101BC
		public bool CheckListen()
		{
			if (!GameObject.Validate(base.Object) || !GameObject.Validate(ref this.Listener) || !this.Listener.IsPlayer())
			{
				return this.BadListener();
			}
			SensePsychic part = this.Listener.GetPart<SensePsychic>();
			if (part == null || part.Level <= 0)
			{
				return this.BadListener();
			}
			int num = base.Object.DistanceTo(this.Listener);
			if (num > part.Radius)
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
			if (this.LastDistance > 0 && num >= this.LastDistance)
			{
				return true;
			}
			this.LastDistance = num;
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

		// Token: 0x0600894F RID: 35151 RVA: 0x003120BA File Offset: 0x003102BA
		public override bool Apply(GameObject Object)
		{
			this.CheckListen();
			return true;
		}

		// Token: 0x06008950 RID: 35152 RVA: 0x003120C4 File Offset: 0x003102C4
		public override void RegisterActive(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(The.Game, this, PooledEvent<AfterPlayerBodyChangeEvent>.ID, 0, false);
		}

		// Token: 0x06008951 RID: 35153 RVA: 0x003120D9 File Offset: 0x003102D9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == EnteringCellEvent.ID;
		}

		// Token: 0x06008952 RID: 35154 RVA: 0x003120F7 File Offset: 0x003102F7
		public override bool HandleEvent(AfterPlayerBodyChangeEvent E)
		{
			this.CheckListen();
			return base.HandleEvent(E);
		}

		// Token: 0x06008953 RID: 35155 RVA: 0x00312107 File Offset: 0x00310307
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.CheckListen();
			return base.HandleEvent(E);
		}

		// Token: 0x06008954 RID: 35156 RVA: 0x00312117 File Offset: 0x00310317
		public override bool HandleEvent(EnteringCellEvent E)
		{
			this.CheckListen();
			return base.HandleEvent(E);
		}

		// Token: 0x06008955 RID: 35157 RVA: 0x00312128 File Offset: 0x00310328
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

		// Token: 0x06008956 RID: 35158 RVA: 0x0031216C File Offset: 0x0031036C
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
				E.ColorString = "&B";
				E.DetailColor = "B";
				E.CustomDraw = true;
				return false;
			}
			return true;
		}

		// Token: 0x04003A73 RID: 14963
		public bool Identified;

		// Token: 0x04003A74 RID: 14964
		public int Level = 1;

		// Token: 0x04003A75 RID: 14965
		public GameObject Listener;

		// Token: 0x04003A76 RID: 14966
		private int LastDistance;
	}
}
