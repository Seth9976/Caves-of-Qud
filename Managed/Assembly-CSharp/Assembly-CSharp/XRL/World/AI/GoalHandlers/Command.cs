using System;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B7C RID: 2940
	[Serializable]
	public class Command : GoalHandler
	{
		// Token: 0x060075B8 RID: 30136 RVA: 0x002C2DFC File Offset: 0x002C0FFC
		public Command()
		{
		}

		// Token: 0x060075B9 RID: 30137 RVA: 0x002C2E04 File Offset: 0x002C1004
		public Command(string Cmd)
			: this()
		{
			this.Cmd = Cmd;
		}

		// Token: 0x060075BA RID: 30138 RVA: 0x002C2E13 File Offset: 0x002C1013
		public override string GetDetails()
		{
			return this.Cmd;
		}

		// Token: 0x060075BB RID: 30139 RVA: 0x002C2E1B File Offset: 0x002C101B
		public override bool Finished()
		{
			return false;
		}

		// Token: 0x060075BC RID: 30140 RVA: 0x002C2E1E File Offset: 0x002C101E
		public override bool CanFight()
		{
			return false;
		}

		// Token: 0x060075BD RID: 30141 RVA: 0x002C2E24 File Offset: 0x002C1024
		public override void TakeAction()
		{
			try
			{
				CommandEvent.Send(base.ParentObject, this.Cmd, null, null, 0, false, false, null);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Command::Send", ex, "game_exception");
			}
			base.Pop();
		}

		// Token: 0x040035E6 RID: 13798
		public string Cmd;
	}
}
