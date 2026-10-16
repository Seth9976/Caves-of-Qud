using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts.Skill
{
	/// This part is not used in the base game.
	// Token: 0x020013AD RID: 5037
	[Serializable]
	public class TenfoldPath_Bin : BaseInitiatorySkill
	{
		// Token: 0x0600C299 RID: 49817 RVA: 0x00471942 File Offset: 0x0046FB42
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetTinkeringBonusEvent.ID;
		}

		// Token: 0x0600C29A RID: 49818 RVA: 0x00471958 File Offset: 0x0046FB58
		public override bool HandleEvent(GetTinkeringBonusEvent E)
		{
			if (E.Type == "Examine" || E.Type == "ReverseEngineerTurns" || E.Type == "Hacking")
			{
				E.Bonus++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C29B RID: 49819 RVA: 0x004719B0 File Offset: 0x0046FBB0
		public override bool AddSkill(GameObject Object)
		{
			Object.ModIntProperty(Scanning.GetScanPropertyName(Scanning.Scan.Bio), 1, false);
			Object.ModIntProperty(Scanning.GetScanPropertyName(Scanning.Scan.Tech), 1, false);
			Object.ModIntProperty(Scanning.GetScanPropertyName(Scanning.Scan.Structure), 1, false);
			return base.AddSkill(Object);
		}

		// Token: 0x0600C29C RID: 49820 RVA: 0x004719E6 File Offset: 0x0046FBE6
		public override bool RemoveSkill(GameObject Object)
		{
			Object.ModIntProperty(Scanning.GetScanPropertyName(Scanning.Scan.Bio), -1, true);
			Object.ModIntProperty(Scanning.GetScanPropertyName(Scanning.Scan.Tech), -1, true);
			Object.ModIntProperty(Scanning.GetScanPropertyName(Scanning.Scan.Structure), -1, true);
			return base.RemoveSkill(Object);
		}
	}
}
