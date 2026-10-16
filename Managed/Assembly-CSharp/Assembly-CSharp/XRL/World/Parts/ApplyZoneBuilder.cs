using System;

namespace XRL.World.Parts
{
	// Token: 0x0200124A RID: 4682
	[Serializable]
	public class ApplyZoneBuilder : IPart
	{
		// Token: 0x0600BA1E RID: 47646 RVA: 0x0043F8F0 File Offset: 0x0043DAF0
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA1F RID: 47647 RVA: 0x0043F8F3 File Offset: 0x0043DAF3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BA20 RID: 47648 RVA: 0x0043F909 File Offset: 0x0043DB09
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.ExecuteBuilder();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA21 RID: 47649 RVA: 0x0043F918 File Offset: 0x0043DB18
		public override bool FireEvent(Event E)
		{
			return base.FireEvent(E);
		}

		// Token: 0x0600BA22 RID: 47650 RVA: 0x0043F921 File Offset: 0x0043DB21
		private void ExecuteBuilder()
		{
			if (!this.Executed)
			{
				this.Executed = true;
				ZoneManager.ApplyBuilderToZone(this.ZoneBuilder, this.ParentObject.CurrentZone);
			}
		}

		// Token: 0x04004ACC RID: 19148
		public string ZoneBuilder;

		// Token: 0x04004ACD RID: 19149
		public bool Executed;
	}
}
