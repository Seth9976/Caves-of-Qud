using System;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D82 RID: 3458
	[Serializable]
	public class SenseRobotEffect : Effect
	{
		// Token: 0x06008957 RID: 35159 RVA: 0x0031225C File Offset: 0x0031045C
		public SenseRobotEffect()
		{
			this.Duration = 1;
		}

		// Token: 0x06008958 RID: 35160 RVA: 0x00312272 File Offset: 0x00310472
		public SenseRobotEffect(int Level = 1, GameObject Listener = null, GameObject Device = null)
			: this()
		{
			this.Level = Level;
			this.Listener = Listener;
			this.Device = Device;
		}

		// Token: 0x06008959 RID: 35161 RVA: 0x0031228F File Offset: 0x0031048F
		public override int GetEffectType()
		{
			return 16777280;
		}

		// Token: 0x0600895A RID: 35162 RVA: 0x00312296 File Offset: 0x00310496
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600895B RID: 35163 RVA: 0x00312299 File Offset: 0x00310499
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x0600895C RID: 35164 RVA: 0x0031229C File Offset: 0x0031049C
		private bool InvalidListen()
		{
			this.Listener = null;
			GameObject @object = base.Object;
			if (@object != null)
			{
				@object.RemoveEffect(this, true);
			}
			return true;
		}

		// Token: 0x0600895D RID: 35165 RVA: 0x003122BC File Offset: 0x003104BC
		public bool CheckListen()
		{
			if (!GameObject.Validate(base.Object) || !GameObject.Validate(ref this.Listener) || !this.Listener.IsPlayer() || this.Listener.IsNowhere())
			{
				return this.InvalidListen();
			}
			int num = base.Object.DistanceTo(this.Listener);
			if (num > this.Level)
			{
				return this.InvalidListen();
			}
			if (!CyberneticsElectromagneticSensor.WillSense(base.Object))
			{
				return this.InvalidListen();
			}
			if (!GameObject.Validate(ref this.Device) || this.Device.Implantee != this.Listener)
			{
				return this.InvalidListen();
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

		// Token: 0x0600895E RID: 35166 RVA: 0x003123C8 File Offset: 0x003105C8
		public override bool Apply(GameObject Object)
		{
			this.CheckListen();
			return true;
		}

		// Token: 0x0600895F RID: 35167 RVA: 0x003123D2 File Offset: 0x003105D2
		public override void RegisterActive(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(The.Game, this, PooledEvent<AfterPlayerBodyChangeEvent>.ID, 0, false);
		}

		// Token: 0x06008960 RID: 35168 RVA: 0x003123E7 File Offset: 0x003105E7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == EnteringCellEvent.ID;
		}

		// Token: 0x06008961 RID: 35169 RVA: 0x00312405 File Offset: 0x00310605
		public override bool HandleEvent(AfterPlayerBodyChangeEvent E)
		{
			this.CheckListen();
			return base.HandleEvent(E);
		}

		// Token: 0x06008962 RID: 35170 RVA: 0x00312415 File Offset: 0x00310615
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.CheckListen();
			return base.HandleEvent(E);
		}

		// Token: 0x06008963 RID: 35171 RVA: 0x00312425 File Offset: 0x00310625
		public override bool HandleEvent(EnteringCellEvent E)
		{
			this.CheckListen();
			return base.HandleEvent(E);
		}

		// Token: 0x06008964 RID: 35172 RVA: 0x00312438 File Offset: 0x00310638
		public override bool FinalRender(RenderEvent E)
		{
			if (base.Object == null || base.Object.IsPlayer())
			{
				return true;
			}
			Cell currentCell = base.Object.CurrentCell;
			if (currentCell != null && (!currentCell.IsLit() || !currentCell.IsExplored() || !currentCell.IsVisible()))
			{
				if (this.Identified)
				{
					E.HighestLayer = 0;
					E.NoWake = true;
					base.Object.ComponentRender(E);
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
				E.ColorString = "&C";
				E.DetailColor = "C";
				E.CustomDraw = true;
				return false;
			}
			return true;
		}

		// Token: 0x04003A77 RID: 14967
		public bool Identified;

		// Token: 0x04003A78 RID: 14968
		public int Level = 1;

		// Token: 0x04003A79 RID: 14969
		public GameObject Listener;

		// Token: 0x04003A7A RID: 14970
		public GameObject Device;
	}
}
