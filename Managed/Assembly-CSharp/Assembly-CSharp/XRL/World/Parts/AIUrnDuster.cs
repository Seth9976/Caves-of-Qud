using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E32 RID: 3634
	[Serializable]
	public class AIUrnDuster : AIBehaviorPart
	{
		// Token: 0x06009385 RID: 37765 RVA: 0x00351C37 File Offset: 0x0034FE37
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetDebugInternalsEvent>.ID;
		}

		// Token: 0x06009386 RID: 37766 RVA: 0x00351C50 File Offset: 0x0034FE50
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			bool flag = this.IsInGlobalUrnArea();
			E.AddEntry(this, "Urn dusting mode", flag ? "global" : "local");
			if (this.ParentObject.HasGoal("DustAnUrnGoal"))
			{
				E.AddEntry(this, "Urn dusting status", "Trying to dust an urn");
			}
			else
			{
				E.AddEntry(this, "Urn dusting status", "Not currently trying to dust an urn");
				if (this.LastUrnSelected != -1)
				{
					E.AddEntry(this, "Last urn dusted", this.LastUrnSelected.ToString() + " of " + this.LastUrnCount.ToString());
				}
			}
			if (this.LastUrnSelected != -1)
			{
				if (this.LastUrnLocation == null)
				{
					E.AddEntry(this, "Last urn location", "no longer available");
				}
				else
				{
					E.AddEntry(this, "Last urn location", string.Concat(new string[]
					{
						this.LastUrnLocation.CellX.ToString(),
						", ",
						this.LastUrnLocation.CellY.ToString(),
						" in ",
						this.LastUrnLocation.ZoneID
					}));
				}
			}
			else
			{
				E.AddEntry(this, "Last urn location", "has not dusted an urn");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009387 RID: 37767 RVA: 0x00351D83 File Offset: 0x0034FF83
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009388 RID: 37768 RVA: 0x00351D86 File Offset: 0x0034FF86
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckDustUrn();
		}

		// Token: 0x06009389 RID: 37769 RVA: 0x00351D90 File Offset: 0x0034FF90
		public bool CheckDustUrn()
		{
			if (this.ParentObject.IsBusy())
			{
				return false;
			}
			if (!this.ParentObject.FireEvent("CanAIDoIndependentBehavior"))
			{
				return false;
			}
			if (this.ParentObject.IsPlayerControlled())
			{
				return false;
			}
			List<GlobalLocation> urns = AIUrnDuster.GetUrns(this.ParentObject.CurrentZone);
			if (urns == null)
			{
				return false;
			}
			bool flag = this.IsInGlobalUrnArea();
			if (urns.Count < 3 && flag)
			{
				return false;
			}
			if (this.DustingIndex == -1)
			{
				this.DustingIndex = Stat.Random(0, urns.Count - 1);
			}
			int dustingIndex = this.DustingIndex;
			int num = dustingIndex % urns.Count;
			int num2 = (dustingIndex + 16) % urns.Count;
			int num3 = (dustingIndex + 32) % urns.Count;
			int num4 = num;
			if (this.LastUrnSelected == num)
			{
				num4 = num2;
			}
			else if (this.LastUrnSelected == num2)
			{
				num4 = num3;
			}
			GlobalLocation globalLocation = urns[num4];
			this.ParentObject.Brain.PushGoal(new DustAnUrnGoal(globalLocation));
			this.LastUrnSelected = num4;
			this.LastUrnCount = urns.Count;
			this.LastUrnLocation = globalLocation;
			return true;
		}

		// Token: 0x0600938A RID: 37770 RVA: 0x00351E9C File Offset: 0x0035009C
		public bool IsInGlobalUrnArea()
		{
			return AIUrnDuster.IsInGlobalUrnArea(this.ParentObject.CurrentZone);
		}

		// Token: 0x0600938B RID: 37771 RVA: 0x00351EAE File Offset: 0x003500AE
		public static bool IsInGlobalUrnArea(Zone Z)
		{
			return "JoppaWorld.53.3.0.0.9,JoppaWorld.53.3.0.1.9,JoppaWorld.53.3.0.2.9".CachedCommaExpansion().Contains((Z != null) ? Z.ZoneID : null);
		}

		// Token: 0x0600938C RID: 37772 RVA: 0x00351ECC File Offset: 0x003500CC
		public static List<GlobalLocation> GetUrns(Zone Z)
		{
			if (AIUrnDuster.IsInGlobalUrnArea(Z))
			{
				if (AIUrnDuster.GlobalUrns == null)
				{
					AIUrnDuster.GlobalUrns = AIUrnDuster.FindGlobalUrns();
				}
				return AIUrnDuster.GlobalUrns;
			}
			if (Z == null)
			{
				return null;
			}
			if (Z.ZoneID == AIUrnDuster.LocalUrnsZoneID)
			{
				return AIUrnDuster.LocalUrns;
			}
			AIUrnDuster.LocalUrnsZoneID = Z.ZoneID;
			AIUrnDuster.LocalUrns = null;
			Z.ForeachObjectWithPart("EaterUrn", delegate(GameObject o)
			{
				if (AIUrnDuster.LocalUrns == null)
				{
					AIUrnDuster.LocalUrns = new List<GlobalLocation>();
				}
				AIUrnDuster.LocalUrns.Add(new GlobalLocation(o));
			});
			return AIUrnDuster.LocalUrns;
		}

		// Token: 0x0600938D RID: 37773 RVA: 0x00351F55 File Offset: 0x00350155
		public static void UrnFoundDestroyed(Zone Z)
		{
			if ("JoppaWorld.53.3.0.0.9,JoppaWorld.53.3.0.1.9,JoppaWorld.53.3.0.2.9".CachedCommaExpansion().Contains((Z != null) ? Z.ZoneID : null))
			{
				AIUrnDuster.GlobalUrns = null;
			}
		}

		// Token: 0x0600938E RID: 37774 RVA: 0x00351F7C File Offset: 0x0035017C
		private static List<GlobalLocation> FindGlobalUrns()
		{
			List<GlobalLocation> list = new List<GlobalLocation>();
			foreach (string text in "JoppaWorld.53.3.0.0.9,JoppaWorld.53.3.0.1.9,JoppaWorld.53.3.0.2.9".CachedCommaExpansion())
			{
				if (The.ZoneManager.IsZoneLive(text))
				{
					Zone zone = The.ZoneManager.GetZone(text);
					for (int i = 0; i < zone.Height; i++)
					{
						for (int j = 0; j < zone.Width; j++)
						{
							Cell cell = zone.GetCell(j, i);
							int k = 0;
							int count = cell.Objects.Count;
							while (k < count)
							{
								GameObject gameObject = cell.Objects[k];
								if (gameObject.HasPart("EaterUrn"))
								{
									list.Add(new GlobalLocation(gameObject));
								}
								k++;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x04003D01 RID: 15617
		public const string URN_ZONES = "JoppaWorld.53.3.0.0.9,JoppaWorld.53.3.0.1.9,JoppaWorld.53.3.0.2.9";

		// Token: 0x04003D02 RID: 15618
		public const string URN_PART = "EaterUrn";

		// Token: 0x04003D03 RID: 15619
		public int LastUrnSelected = -1;

		// Token: 0x04003D04 RID: 15620
		[NonSerialized]
		private GlobalLocation LastUrnLocation;

		// Token: 0x04003D05 RID: 15621
		[NonSerialized]
		private int LastUrnCount;

		// Token: 0x04003D06 RID: 15622
		[NonSerialized]
		private int DustingIndex = -1;

		// Token: 0x04003D07 RID: 15623
		[NonSerialized]
		private static List<GlobalLocation> GlobalUrns;

		// Token: 0x04003D08 RID: 15624
		[NonSerialized]
		private static List<GlobalLocation> LocalUrns;

		// Token: 0x04003D09 RID: 15625
		[NonSerialized]
		private static string LocalUrnsZoneID;
	}
}
