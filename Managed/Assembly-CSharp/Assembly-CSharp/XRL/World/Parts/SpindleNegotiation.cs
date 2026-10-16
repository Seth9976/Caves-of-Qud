using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02001220 RID: 4640
	[Serializable]
	public class SpindleNegotiation : IPart
	{
		// Token: 0x0600B83F RID: 47167 RVA: 0x00430136 File Offset: 0x0042E336
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x0600B840 RID: 47168 RVA: 0x0043014C File Offset: 0x0042E34C
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (The.Game.HasFinishedQuestStep("The Earl of Omonporch", "Secure the Spindle") && !this.bRemovedDelegates)
			{
				Zone currentZone = this.ParentObject.CurrentZone;
				for (int i = 0; i < currentZone.Height; i++)
				{
					for (int j = 0; j < currentZone.Width; j++)
					{
						Cell cell = currentZone.GetCell(j, i);
						foreach (GameObject gameObject in cell.GetObjectsWithPart("Brain"))
						{
							if (gameObject.HasIntProperty("IsDelegate"))
							{
								cell.RemoveObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
							}
						}
					}
				}
				this.bRemovedDelegates = true;
			}
			if (!this.bArrived && this.bQualified && Calendar.TotalTimeTicks - this.TimeArrived >= 3600L)
			{
				this.bArrived = true;
				this.ParentObject.GetPart<ConversationScript>().ConversationID = "AsphodelArrived";
				XRLCore.Core.Game.SetIntGameState("DelegationOn", 1);
				this.AddDelegates();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B841 RID: 47169 RVA: 0x0043028C File Offset: 0x0042E48C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B842 RID: 47170 RVA: 0x0043028F File Offset: 0x0042E48F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginSpindleNegotiation");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B843 RID: 47171 RVA: 0x004302A4 File Offset: 0x0042E4A4
		public List<string> GetTop4Factions()
		{
			List<string> list = new List<string>();
			int i = 0;
			List<string> factionNames = Factions.GetFactionNames();
			factionNames.Sort(new FactionRepComparerRandomSortEqual());
			int num = 0;
			while (i < 4)
			{
				if (Factions.Get(factionNames[num]).Visible)
				{
					list.Add(factionNames[num]);
					i++;
				}
				num++;
			}
			return list;
		}

		// Token: 0x0600B844 RID: 47172 RVA: 0x004302FB File Offset: 0x0042E4FB
		public GameObject GetDelegateForFaction(string Faction)
		{
			GameObject gameObject = GameObject.Create("Delegate");
			gameObject.AddPart<DelegateSpawner>(new DelegateSpawner(Faction), true, false);
			return gameObject;
		}

		// Token: 0x0600B845 RID: 47173 RVA: 0x00430318 File Offset: 0x0042E518
		public void AddDelegates()
		{
			List<Cell> list = new List<Cell>();
			this.ParentObject.Physics.CurrentCell.GetConnectedSpawnLocations(4, list, true);
			if (list.Count < 4)
			{
				List<Cell> emptyCells = this.ParentObject.Physics.CurrentCell.ParentZone.GetEmptyCells(null, null);
				int num = 0;
				while (num < emptyCells.Count && list.Count < 4)
				{
					list.Add(emptyCells[num]);
					num++;
				}
			}
			Algorithms.RandomShuffleInPlace<Cell>(list, Stat.Rand);
			int num2 = 0;
			foreach (string text in this.GetTop4Factions())
			{
				GameObject delegateForFaction = this.GetDelegateForFaction(text);
				list[num2].AddObject(delegateForFaction, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				num2++;
				this.DelegateFactions.Add(text);
			}
		}

		// Token: 0x0600B846 RID: 47174 RVA: 0x00430418 File Offset: 0x0042E618
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginSpindleNegotiation")
			{
				if (this.bChaosed)
				{
					Popup.Show("That was awful!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return true;
				}
				if (this.bNegotiated)
				{
					Popup.Show("Your friends may lease the Spindle, as we agreed.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return true;
				}
				if (this.bArrived)
				{
					string[] array = new string[5];
					List<string> list = ((this.DelegateFactions.Count >= 4) ? this.DelegateFactions : this.GetTop4Factions());
					List<string> list2 = new List<string>();
					for (int i = 0; i < list.Count; i++)
					{
						list2.Add(Faction.GetFormattedName(list[i]));
					}
					array[0] = "Share the burden across all allies. [-{{C|50}} reputation with each attending faction]";
					array[1] = "Share the burden between two allies. [-{{C|100}} reputation with two attending factions of your choice]";
					array[2] = "Spare one faction of all obligation by betraying a second faction and selling their secrets to Asphodel. [-{{C|800}} with the betrayed faction, +{{C|200}} reputation with the spared faction + a faction heirloom]";
					array[3] = "Invoke the Chaos Spiel. [????????, +{{C|300}} reputation with {{C|highly entropic beings}}]";
					array[4] = "Take time to weigh the options.";
					char[] array2 = new char[] { 'a', 'b', 'c', 'd', 'e' };
					string text = "";
					for (int j = 0; j < list.Count; j++)
					{
						text = text + list[j] + ", ";
					}
					int num = Popup.PickOption("", "The First Council of Omonporch has begun. Choose how to appease Asphodel.", "", "Sounds/UI/ui_notification", array, array2, null, null, null, null, null, 1, 75, 0, -1, false, false, false, true, false, null, null);
					if (num == 4 || num < 0)
					{
						return true;
					}
					if (num == 0)
					{
						Popup.Show("The pact is struck. The Barathrumites may lease control of the Spindle, and all the attending factions owe a debt to Asphodel.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						foreach (string text2 in list)
						{
							The.Game.PlayerReputation.Modify(text2, -50, "Quest", null, null, false, false);
						}
						XRLCore.Core.Game.FinishQuestStep("The Earl of Omonporch", "Secure the Spindle", -1, true, null);
						this.bNegotiated = true;
						this.ParentObject.GetPart<ConversationScript>().ConversationID = "AsphodelDone";
						return true;
					}
					if (num == 1)
					{
						int num2 = Popup.PickOption("", "Choose a faction to share the burden. [-{{C|100}} reputation]", "", "Sounds/UI/ui_notification", list2.ToArray(), null, null, null, null, null, null, 1, 60, 0, -1, false, false, false, true, false, null, null);
						string text3 = list[num2];
						if (num2 < 0)
						{
							return true;
						}
						list2.Remove(Faction.GetFormattedName(text3));
						list.Remove(text3);
						int num3 = Popup.PickOption("", "Choose a faction to share the burden. [-{{C|100}} reputation]", "", "Sounds/UI/ui_notification", list2.ToArray(), null, null, null, null, null, null, 1, 60, 0, -1, false, false, false, true, false, null, null);
						if (num3 < 0)
						{
							return true;
						}
						string text4 = list[num3];
						Popup.Show("The pact is struck. The Barathrumites may lease control of the Spindle, and the chosen factions owe a debt to Asphodel.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						XRLCore.Core.Game.PlayerReputation.Modify(text3, -100, "Quest", null, null, false, false);
						The.Game.PlayerReputation.Modify(text4, -100, "Quest", null, null, false, false);
						XRLCore.Core.Game.FinishQuestStep("The Earl of Omonporch", "Secure the Spindle", -1, true, null);
						this.bNegotiated = true;
						this.ParentObject.GetPart<ConversationScript>().ConversationID = "AsphodelDone";
						return true;
					}
					else if (num == 2)
					{
						int num4 = Popup.PickOption("", "Choose a faction to betray. [-{{C|800}} reputation]", "", "Sounds/UI/ui_notification", list2.ToArray(), null, null, null, null, null, null, 1, 60, 0, -1, false, false, false, true, false, null, null);
						string text5 = list[num4];
						if (num4 < 0)
						{
							return true;
						}
						list2.Remove(Faction.GetFormattedName(text5));
						list.Remove(text5);
						int num5 = Popup.PickOption("", "Choose a faction to spare from obligation to Asphodel. [+{{C|200}} reputation and a faction heirloom]", "", "Sounds/UI/ui_notification", list2.ToArray(), null, null, null, null, null, null, 1, 60, 0, -1, false, false, false, true, false, null, null);
						if (num5 < 0)
						{
							return true;
						}
						string text6 = list[num5];
						GameObject heirloom = Factions.Get(text6).GetHeirloom();
						Popup.Show("The pact is struck. The Barathrumites may lease control the Spindle.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						Popup.Show(string.Concat(new string[]
						{
							"The delegate for ",
							Faction.GetFormattedName(text6),
							" says, 'Live and drink, ",
							IComponent<GameObject>.ThePlayer.formalAddressTerm,
							". We won't forget this.'"
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						The.Game.PlayerReputation.Modify(text6, 200, "Quest", null, null, false, false);
						IComponent<GameObject>.ThePlayer.ReceiveObject(heirloom, false, null);
						Popup.Show(string.Concat(new string[]
						{
							"The delegate for ",
							Faction.GetFormattedName(text6),
							" gives you ",
							heirloom.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
							"!"
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						Popup.Show("The delegate for " + Faction.GetFormattedName(text5) + " says, 'Betrayer! May you choke on your own spittle! We won't forget this.'", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						The.Game.PlayerReputation.Modify(text5, -800, "Quest", null, null, false, false);
						XRLCore.Core.Game.FinishQuestStep("The Earl of Omonporch", "Secure the Spindle", -1, true, null);
						this.bNegotiated = true;
						this.ParentObject.GetPart<ConversationScript>().ConversationID = "AsphodelDone";
						return true;
					}
					else if (num == 3)
					{
						Popup.Show("You ponder how best to sow chaos with your words.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						The.Game.PlayerReputation.Modify("Entropic", GivesRep.VaryRep(300), "Quest", null, null, false, false);
						List<string> top4Factions = this.GetTop4Factions();
						top4Factions.Add("Flowers");
						top4Factions.Add("Consortium");
						string text7 = "";
						string text8 = "";
						bool flag = false;
						for (int k = 0; k < 3; k++)
						{
							while (!flag)
							{
								text7 = top4Factions.GetRandomElement<string>(null);
								text8 = top4Factions.GetRandomElement<string>(null);
								if (!text7.Equals(text8))
								{
									flag = true;
								}
							}
							flag = false;
							string text9 = ((text8 == "Entropic") ? GenerateFriendOrFoe_HEB.getHateReason() : GenerateFriendOrFoe.getHateReason());
							Popup.Show(string.Concat(new string[]
							{
								"You yell, 'I cannot believe {{C|",
								Faction.GetFormattedName(text7),
								"}} don't despise {{C|",
								Faction.GetFormattedName(text8),
								"}} for ",
								text9,
								".'"
							}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
							Popup.Show(string.Concat(new string[]
							{
								"Due to your revelation, ",
								Faction.GetFormattedName(text7),
								" change their opinion of ",
								Faction.GetFormattedName(text8),
								"."
							}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
							Factions.Get(text7).SetFactionFeeling(text8, -100);
							The.Game.PlayerReputation.Modify(text7, GivesRep.VaryRep(200), "Quest", null, null, false, false);
							The.Game.PlayerReputation.Modify(text8, GivesRep.VaryRep(-200), "Quest", null, null, false, false);
						}
						this.bChaosed = true;
						this.ParentObject.GetPart<ConversationScript>().ConversationID = "AsphodelChaos";
						XRLCore.Core.Game.SetIntGameState("DelegationOn", 0);
						Popup.Show("Asphodel yells, '{{R|You ruined the First Council of Omonporch, you barbaric lout!}}'", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						this.ParentObject.Brain.AddOpinion<OpinionChaosSpiel>(IComponent<GameObject>.ThePlayer, 1f);
						this.ParentObject.Brain.FactionFeelings["Player"] = -100;
						foreach (GameObject gameObject in this.ParentObject.CurrentZone.FindObjectsWithPart("Calming"))
						{
							gameObject.RemovePart<Calming>();
						}
						AchievementManager.SetAchievement("ACH_CHAOS_SPIEL");
						Achievement.CHAOS_SPIEL.Unlock();
						return true;
					}
				}
				else
				{
					if (!this.HasEnoughFriends())
					{
						Popup.Show("You don't have enough allied factions. Come back when you're favored by {{C|4}} or more factions.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						return true;
					}
					this.ParentObject.GetPart<ConversationScript>().ConversationID = "AsphodelWaiting";
					if (this.TimeArrived == 0L)
					{
						this.TimeArrived = Calendar.TotalTimeTicks;
					}
					int num6 = Math.Max(1, 3 - (int)Math.Ceiling((double)((float)(Calendar.TotalTimeTicks - this.TimeArrived) / 1200f)));
					Popup.Show(string.Concat(new string[]
					{
						"The council will be convened! Come back in ",
						num6.ToString(),
						" ",
						(num6 == 1) ? "day" : "days",
						"."
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					this.bQualified = true;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B847 RID: 47175 RVA: 0x00430D0C File Offset: 0x0042EF0C
		public bool HasEnoughFriends()
		{
			List<string> top4Factions = this.GetTop4Factions();
			return The.Game.PlayerReputation.Get(top4Factions[3]) >= RuleSettings.REPUTATION_LIKED;
		}

		// Token: 0x04004A21 RID: 18977
		public bool bQualified;

		// Token: 0x04004A22 RID: 18978
		public bool bArrived;

		// Token: 0x04004A23 RID: 18979
		public bool bNegotiated;

		// Token: 0x04004A24 RID: 18980
		public bool bChaosed;

		// Token: 0x04004A25 RID: 18981
		public bool bRemovedDelegates;

		// Token: 0x04004A26 RID: 18982
		public long TimeArrived;

		// Token: 0x04004A27 RID: 18983
		public List<string> DelegateFactions = new List<string>();
	}
}
