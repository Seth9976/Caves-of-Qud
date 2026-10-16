using System;

namespace XRL.World.Parts
{
	// Token: 0x0200121A RID: 4634
	[Serializable]
	public class CallToArmsScore : IPart
	{
		// Token: 0x0600B819 RID: 47129 RVA: 0x0042EFC8 File Offset: 0x0042D1C8
		public CallToArmsScore()
		{
		}

		// Token: 0x0600B81A RID: 47130 RVA: 0x0042EFD0 File Offset: 0x0042D1D0
		public CallToArmsScore(int impact, string category, bool person = false)
			: this()
		{
			this.impact = impact;
			this.category = category;
			this.person = person;
		}

		// Token: 0x0600B81B RID: 47131 RVA: 0x0042EFED File Offset: 0x0042D1ED
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == OnDestroyObjectEvent.ID;
		}

		// Token: 0x0600B81C RID: 47132 RVA: 0x0042F00B File Offset: 0x0042D20B
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Impact", this.impact);
			E.AddEntry(this, "Category", this.category);
			E.AddEntry(this, "Person", this.person);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B81D RID: 47133 RVA: 0x0042F04C File Offset: 0x0042D24C
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			if (!this.ParentObject.IsTemporary && this.ParentObject.GetIntProperty("IsClone", 0) <= 0 && !this.ParentObject.HasProperty("EvilTwin"))
			{
				bool flag = true;
				string text = "CallToArmsObjectsDestroyed";
				string id = this.ParentObject.ID;
				if (The.Game.HasStringGameState(text))
				{
					if (The.Game.GetStringGameState(text, "").Split(',', StringSplitOptions.None).Contains(id))
					{
						flag = false;
					}
					else
					{
						The.Game.AppendStringGameState(text, id, ",");
					}
				}
				else
				{
					The.Game.SetStringGameState(text, id);
				}
				if (flag && this.person && this.ParentObject.HasProperName)
				{
					string text2 = "CallToArmsPersonsKilled";
					string text3 = this.ParentObject.BaseDisplayNameStripped.Replace(",", "");
					if (The.Game.HasStringGameState(text2))
					{
						if (The.Game.GetStringGameState(text2, "").Split(',', StringSplitOptions.None).Contains(text3))
						{
							MetricsManager.LogError(string.Concat(new string[]
							{
								"destroying person ",
								this.ParentObject.DebugName,
								" already recorded as destroyed in ",
								text2,
								" by name ",
								text3
							}));
							flag = false;
						}
						else
						{
							The.Game.AppendStringGameState(text2, text3, ",");
						}
					}
					else
					{
						The.Game.SetStringGameState(text2, text3);
					}
				}
				if (flag)
				{
					The.Game.IncrementIntGameState("CallToArmsScore", this.impact);
					The.Game.IncrementIntGameState(this.category, 1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B81E RID: 47134 RVA: 0x0042F1FE File Offset: 0x0042D3FE
		public static bool GiveCallToArmsReward_Top()
		{
			The.Game.PlayerReputation.Modify("Barathrumites", 300, "Quest", null, null, false, false);
			return true;
		}

		// Token: 0x0600B81F RID: 47135 RVA: 0x0042F224 File Offset: 0x0042D424
		public static bool GiveCallToArmsReward_Mid()
		{
			The.Game.PlayerReputation.Modify("Barathrumites", 200, "Quest", null, null, false, false);
			return true;
		}

		// Token: 0x0600B820 RID: 47136 RVA: 0x0042F24A File Offset: 0x0042D44A
		public static bool GiveCallToArmsReward_Bottom()
		{
			The.Game.PlayerReputation.Modify("Barathrumites", 100, "Quest", null, null, false, false);
			return true;
		}

		// Token: 0x04004A0E RID: 18958
		public bool person;

		// Token: 0x04004A0F RID: 18959
		public string category;

		// Token: 0x04004A10 RID: 18960
		public int impact;
	}
}
