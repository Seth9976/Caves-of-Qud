using System;

namespace XRL.World.Parts
{
	// Token: 0x0200109D RID: 4253
	[Serializable]
	public class SpiralIron : IPart
	{
		// Token: 0x0600A92E RID: 43310 RVA: 0x003DDB47 File Offset: 0x003DBD47
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A92F RID: 43311 RVA: 0x003DDB68 File Offset: 0x003DBD68
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Press", "press", SpiralIron.COMMAND_NAME, null, 'p', false, 10, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A930 RID: 43312 RVA: 0x003DDBA0 File Offset: 0x003DBDA0
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == SpiralIron.COMMAND_NAME)
			{
				this.PressSpiralIron(E.Actor, !E.Auto);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A931 RID: 43313 RVA: 0x003DDBD1 File Offset: 0x003DBDD1
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A932 RID: 43314 RVA: 0x003DDBD4 File Offset: 0x003DBDD4
		public bool PressSpiralIron(GameObject Actor, bool FromDialog)
		{
			base.PlayWorldSound("Sounds/Interact/sfx_interact_curlingIron_press", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			base.DidX("hiss", null, null, null, null, null, null, false, false, null, null, false, false, false, FromDialog, Actor.IsPlayer(), null);
			return true;
		}

		// Token: 0x040045D7 RID: 17879
		public static readonly string COMMAND_NAME = "PressIron";
	}
}
