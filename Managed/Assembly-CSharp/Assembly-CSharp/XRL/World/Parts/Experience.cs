using System;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x02000EB1 RID: 3761
	[Serializable]
	public class Experience : IPart
	{
		// Token: 0x060096E8 RID: 38632 RVA: 0x0036BDC6 File Offset: 0x00369FC6
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x060096E9 RID: 38633 RVA: 0x0036BDC9 File Offset: 0x00369FC9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AwardXPEvent.ID;
		}

		// Token: 0x060096EA RID: 38634 RVA: 0x0036BDE0 File Offset: 0x00369FE0
		public override bool HandleEvent(AwardXPEvent E)
		{
			try
			{
				if (this.ParentObject.HasTagOrProperty("NoXPGain"))
				{
					return true;
				}
				if (AwardingXPEvent.Check(E, E.Actor))
				{
					int num = E.Amount;
					if (E.TierScaling && E.Tier >= 0)
					{
						int num2 = (this.ParentObject.HasStat("Level") ? (this.ParentObject.Stat("Level", 0) / 5) : 1) - E.Tier;
						if (num2 > 2)
						{
							num = 0;
						}
						else if (num2 > 1)
						{
							num /= 10;
						}
						else if (num2 > 0)
						{
							num /= 2;
						}
					}
					if (num < E.Minimum)
					{
						num = E.Minimum;
					}
					if (num > E.Maximum)
					{
						num = E.Maximum;
					}
					if (XRLCore.Core.XPMul != 1f)
					{
						num = (int)((float)num * XRLCore.Core.XPMul);
					}
					if (num > 0)
					{
						Statistic stat = this.ParentObject.GetStat("XP");
						if (stat != null)
						{
							stat.BaseValue += num;
							if (base.IsPlayer())
							{
								IComponent<GameObject>.AddPlayerMessage("You gain {{C|" + num.ToString() + "}} XP!", null, true);
							}
							AwardedXPEvent.Send(E, num);
						}
					}
					if (E.PassedDownFrom == null && E.PassedUpFrom != this.ParentObject && this.ParentObject.Brain != null)
					{
						GameObject partyLeader = this.ParentObject.PartyLeader;
						if (partyLeader != null)
						{
							partyLeader.AwardXP(E.Amount, E.Tier, 0, int.MaxValue, E.Kill, E.InfluencedBy, E.PassedUpFrom ?? this.ParentObject, null, null, E.Deed);
						}
						else if (E.PassedUpFrom == null && this.ParentObject.IsPlayer())
						{
							Cell currentCell = this.ParentObject.GetCurrentCell();
							if (currentCell != null && currentCell.ParentZone != null)
							{
								foreach (GameObject gameObject in currentCell.ParentZone.FastSquareSearch(currentCell.X, currentCell.Y, 10, "Brain"))
								{
									if (gameObject != E.Kill && gameObject.IsPlayerLed())
									{
										gameObject.AwardXP(E.Amount, E.Tier, 0, int.MaxValue, E.Kill, E.InfluencedBy, null, this.ParentObject, null, E.Deed);
									}
								}
							}
						}
					}
					E.Amount = num;
				}
				else
				{
					E.Amount = 0;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("Exception awarding XP", ex);
			}
			return base.HandleEvent(E);
		}
	}
}
