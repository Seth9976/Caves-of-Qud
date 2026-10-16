using System;
using ConsoleLib.Console;

namespace XRL.World.Parts
{
	// Token: 0x0200129F RID: 4767
	[Serializable]
	public class Walltrap : IPart
	{
		// Token: 0x0600BC69 RID: 48233 RVA: 0x0044E27C File Offset: 0x0044C47C
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BC6A RID: 48234 RVA: 0x0044E27F File Offset: 0x0044C47F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BC6B RID: 48235 RVA: 0x0044E295 File Offset: 0x0044C495
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.ProcessWalltrapTrigger();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BC6C RID: 48236 RVA: 0x0044E2A4 File Offset: 0x0044C4A4
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600BC6D RID: 48237 RVA: 0x0044E2A7 File Offset: 0x0044C4A7
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ProcessWalltrapTrigger();
		}

		// Token: 0x0600BC6E RID: 48238 RVA: 0x0044E2B0 File Offset: 0x0044C4B0
		public void ProcessWalltrapTrigger()
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone == null || !currentZone.IsActive() || currentZone.IsWorldMap())
			{
				return;
			}
			Render render = this.ParentObject.Render;
			int num = this.CurrentTurn + 1;
			this.CurrentTurn = num;
			if (num == this.TurnInterval - 1)
			{
				render.ColorString = this.ReadyColor;
			}
			else
			{
				render.ColorString = this.WarmColor;
			}
			char? c = new char?('y');
			char? c2 = new char?('k');
			ColorUtility.FindLastForegroundAndBackground(render.ColorString, ref c, ref c2);
			Render render2 = render;
			string text = "&";
			char? c3 = c;
			render2.TileColor = text + c3.ToString();
			render.DetailColor = c2.ToString();
			if (this.CurrentTurn >= this.TurnInterval)
			{
				this.ParentObject.FireEvent("WalltrapTrigger");
				this.CurrentTurn = 0;
			}
		}

		// Token: 0x04004B81 RID: 19329
		public int TurnInterval = 3;

		// Token: 0x04004B82 RID: 19330
		public int CurrentTurn;

		// Token: 0x04004B83 RID: 19331
		public string ReadyColor = "^g&R";

		// Token: 0x04004B84 RID: 19332
		public string WarmColor = "^g&r";
	}
}
