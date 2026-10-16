using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02001090 RID: 4240
	[Serializable]
	public class ReshephProjectionWidget : IPart
	{
		// Token: 0x0600A8B8 RID: 43192 RVA: 0x003DBA6A File Offset: 0x003D9C6A
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A8B9 RID: 43193 RVA: 0x003DBA6D File Offset: 0x003D9C6D
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckTakeover();
		}

		// Token: 0x0600A8BA RID: 43194 RVA: 0x003DBA75 File Offset: 0x003D9C75
		public override bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			GameManager.Instance.gameQueue.queueTask(new Action(this.CheckTakeover), 0);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8BB RID: 43195 RVA: 0x003DBA9C File Offset: 0x003D9C9C
		public void RegisterProjectors()
		{
			if (this.Registered || !this.ParentObject.CurrentZone.Built)
			{
				return;
			}
			foreach (GameObject gameObject in this.ParentObject.CurrentZone.IterateObjects())
			{
				HologramProjector hologramProjector;
				if (gameObject.TryGetPart<HologramProjector>(out hologramProjector))
				{
					if (this.Projectors == null)
					{
						this.Projectors = new List<HologramProjector>();
					}
					this.Projectors.Add(hologramProjector);
					gameObject.RegisterEvent(this, PowerSwitchFlippedEvent.ID, EventOrder.LATE, false);
				}
			}
			this.Registered = true;
		}

		// Token: 0x0600A8BC RID: 43196 RVA: 0x003DBB34 File Offset: 0x003D9D34
		public void CheckTakeover()
		{
			this.RegisterProjectors();
			if (!this.Registered || this.Projectors.IsNullOrEmpty<HologramProjector>())
			{
				return;
			}
			int num = 0;
			bool flag = false;
			foreach (HologramProjector hologramProjector in this.Projectors)
			{
				if (hologramProjector.IsReady(false, false, false, false, false, false, false, false, false, true, 1, null, false, 0L, null))
				{
					num++;
				}
				if (hologramProjector.Blueprint == "Resheph Hologram")
				{
					flag = true;
				}
			}
			bool flag2 = num == this.Projectors.Count;
			if (flag != flag2)
			{
				foreach (HologramProjector hologramProjector2 in this.Projectors)
				{
					GameObject parentObject = hologramProjector2.ParentObject;
					string text = hologramProjector2.Blueprint;
					if (flag2)
					{
						text = ((hologramProjector2.Blueprint == "Archon Hologram") ? "Resheph Hologram" : null);
					}
					else
					{
						text = parentObject.GetBlueprint(true).GetPartParameter<string>("HologramProjector", "Blueprint", text);
					}
					hologramProjector2.SetBlueprint(text, true);
					parentObject.Sparksplatter();
					parentObject.PlayWorldSound("sfx_spark", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
			}
		}

		// Token: 0x040045BD RID: 17853
		private List<HologramProjector> Projectors;

		// Token: 0x040045BE RID: 17854
		private bool Registered;
	}
}
