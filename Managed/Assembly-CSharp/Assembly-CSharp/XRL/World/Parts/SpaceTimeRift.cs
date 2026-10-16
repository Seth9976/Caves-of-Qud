using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001289 RID: 4745
	[Serializable]
	public class SpaceTimeRift : SpaceTimeVortex
	{
		// Token: 0x0600BBB0 RID: 48048 RVA: 0x00448885 File Offset: 0x00446A85
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetPointsOfInterestEvent>.ID;
		}

		// Token: 0x0600BBB1 RID: 48049 RVA: 0x0044889C File Offset: 0x00446A9C
		public override bool HandleEvent(GetPointsOfInterestEvent E)
		{
			if (E.StandardChecks(this, E.Actor, null))
			{
				E.Add(this.ParentObject, this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false), null, null, null, null, null, 2, null, 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BBB2 RID: 48050 RVA: 0x004488F4 File Offset: 0x00446AF4
		public override bool Render(RenderEvent E)
		{
			if (Stat.RandomCosmetic(1, 60) < 3)
			{
				string text = "&C";
				if (Stat.RandomCosmetic(1, 3) == 1)
				{
					text = "&W";
				}
				if (Stat.RandomCosmetic(1, 3) == 1)
				{
					text = "&R";
				}
				if (Stat.RandomCosmetic(1, 3) == 1)
				{
					text = "&B";
				}
				Cell currentCell = this.ParentObject.CurrentCell;
				XRLCore.ParticleManager.AddRadial(text + "ù", (float)currentCell.X, (float)currentCell.Y, (float)Stat.RandomCosmetic(0, 7), (float)Stat.RandomCosmetic(5, 10), 0.01f * (float)Stat.RandomCosmetic(4, 6), -0.01f * (float)Stat.RandomCosmetic(3, 7));
			}
			int num = Stat.RandomCosmetic(0, 4);
			if (num == 0)
			{
				E.ColorString = "&B^k";
			}
			else if (num == 1)
			{
				E.ColorString = "&R^k";
			}
			else if (num == 2)
			{
				E.ColorString = "&C^k";
			}
			else if (num == 3)
			{
				E.ColorString = "&W^k";
			}
			else if (num == 4)
			{
				E.ColorString = "&K^k";
			}
			int num2 = Stat.RandomCosmetic(0, 3);
			if (num2 == 0)
			{
				E.RenderString = "\t";
			}
			else if (num2 == 1)
			{
				E.RenderString = "é";
			}
			else if (num2 == 2)
			{
				E.RenderString = "\u0015";
			}
			else if (num2 == 3)
			{
				E.RenderString = "\u000f";
			}
			return true;
		}

		// Token: 0x0600BBB3 RID: 48051 RVA: 0x00448A45 File Offset: 0x00446C45
		public override int SpaceTimeAnomalyEmergencePermillageBaseChance()
		{
			return 5;
		}

		// Token: 0x0600BBB4 RID: 48052 RVA: 0x00448A48 File Offset: 0x00446C48
		public override int SpaceTimeAnomalyEmergenceExplodePercentageBaseChance()
		{
			return 5;
		}

		// Token: 0x0600BBB5 RID: 48053 RVA: 0x00448A4B File Offset: 0x00446C4B
		public override bool SpaceTimeAnomalyStationary()
		{
			return true;
		}
	}
}
