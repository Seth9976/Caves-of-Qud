using System;
using System.Collections.Generic;
using System.Text;
using XRL.Language;
using XRL.Messages;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Tinkering
{
	// Token: 0x02000B38 RID: 2872
	public class Disassembly : OngoingAction
	{
		// Token: 0x06007407 RID: 29703 RVA: 0x002BC268 File Offset: 0x002BA468
		public Disassembly(GameObject Object = null, int NumberWanted = 1, bool Auto = false, List<Action<GameObject>> Alarms = null, int EnergyCostPer = 1000)
		{
			this.Object = Object;
			this.NumberWanted = NumberWanted;
			this.TotalNumberWanted = this.NumberWanted;
			this.Auto = Auto;
			this.Alarms = Alarms;
			this.EnergyCostPer = EnergyCostPer;
			this.OriginalCount = this.Object.Count;
			this.TotalOriginalCount = this.OriginalCount;
			this.WasTemporary = true;
		}

		// Token: 0x06007408 RID: 29704 RVA: 0x002BC2F1 File Offset: 0x002BA4F1
		public override string GetDescription()
		{
			return "disassembling";
		}

		// Token: 0x06007409 RID: 29705 RVA: 0x002BC2F8 File Offset: 0x002BA4F8
		public override bool ShouldHostilesInterrupt()
		{
			return this.TotalNumberWanted > 1;
		}

		// Token: 0x0600740A RID: 29706 RVA: 0x002BC304 File Offset: 0x002BA504
		public override bool Continue()
		{
			GameObject player = The.Player;
			if (!GameObject.Validate(ref this.Object))
			{
				this.InterruptBecause = "the item you were working on disappeared";
				return false;
			}
			if (this.Object.IsInGraveyard())
			{
				this.InterruptBecause = "the item you were working on was destroyed";
				return false;
			}
			if (this.Object.IsNowhere())
			{
				this.InterruptBecause = "the item you were working on disappeared";
				return false;
			}
			if (this.Object.InInventory != player && !this.Object.InSameOrAdjacentCellTo(player))
			{
				this.InterruptBecause = this.Object.does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " no longer within your reach";
				return false;
			}
			if (this.Object.IsInStasis())
			{
				this.InterruptBecause = "you can no longer interact with " + this.Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
				return false;
			}
			TinkerItem tinkerItem;
			if (!this.Object.TryGetPart<TinkerItem>(out tinkerItem))
			{
				this.InterruptBecause = this.Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " can no longer be disassembled";
				return false;
			}
			if (!player.HasSkill("Tinkering_Disassemble"))
			{
				this.InterruptBecause = "you no longer know how to disassemble things";
				return false;
			}
			if (!tinkerItem.CanBeDisassembled(player))
			{
				this.InterruptBecause = this.Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " can no longer be disassembled";
				return false;
			}
			if (!player.CanMoveExtremities("Disassemble", false, false, true))
			{
				this.InterruptBecause = "you can no longer move your extremities";
				return false;
			}
			int num = 0;
			try
			{
				bool flag = false;
				if (this.BitChance == -2147483648)
				{
					if (tinkerItem.Bits.Length == 1)
					{
						this.BitChance = 0;
					}
					else
					{
						int num2 = player.GetIntProperty("DisassembleBonus", 0);
						this.BitChance = 50;
						num2 = GetTinkeringBonusEvent.GetFor(player, this.Object, "Disassemble", this.BitChance, num2, ref flag, true, false);
						if (flag)
						{
							return false;
						}
						this.BitChance += num2;
					}
				}
				string activeBlueprint = tinkerItem.ActiveBlueprint;
				TinkerData tinkerData = null;
				List<TinkerData> list = null;
				if (player.HasSkill("Tinkering_ReverseEngineer"))
				{
					foreach (TinkerData tinkerData2 in TinkerData.TinkerRecipes)
					{
						if (tinkerData2.Type == "Build" && tinkerData2.Blueprint == activeBlueprint)
						{
							tinkerData = tinkerData2;
							using (List<TinkerData>.Enumerator enumerator2 = TinkerData.KnownRecipes.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									if (enumerator2.Current.Blueprint == activeBlueprint)
									{
										tinkerData = null;
										break;
									}
								}
								break;
							}
						}
					}
					foreach (TinkerData tinkerData3 in TinkerData.TinkerRecipes)
					{
						if (tinkerData3.Type == "Mod" && this.Object.HasPart(tinkerData3.PartName))
						{
							bool flag2 = false;
							using (List<TinkerData>.Enumerator enumerator2 = TinkerData.KnownRecipes.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									if (enumerator2.Current.Blueprint == tinkerData3.Blueprint)
									{
										flag2 = true;
										break;
									}
								}
							}
							if (!flag2)
							{
								if (list == null)
								{
									list = new List<TinkerData>();
								}
								list.Add(tinkerData3);
							}
						}
					}
				}
				int num3 = 0;
				int num4 = 0;
				if (tinkerData != null || (list != null && list.Count > 0))
				{
					num3 = 15;
					num4 = GetTinkeringBonusEvent.GetFor(player, this.Object, "ReverseEngineer", num3, num4, ref flag, true, false);
					if (flag)
					{
						return false;
					}
					num3 += num4;
				}
				bool flag3 = Options.SifrahReverseEngineer && (tinkerData != null || (list != null && list.Count > 0));
				bool flag4 = false;
				if (flag3)
				{
					flag4 = Popup.ShowYesNo("Do you want to try to reverse engineer " + this.Object.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + "?", "Sounds/UI/ui_notification", false, DialogResult.Yes, null) == DialogResult.Yes;
				}
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				if (flag4)
				{
					num5 = player.Stat("Intelligence", 0) + num4;
					Examiner part = this.Object.GetPart<Examiner>();
					num6 = ((part != null) ? part.Complexity : this.Object.GetTier());
					num7 = ((part != null) ? part.Difficulty : 0);
				}
				try
				{
					InventoryActionEvent.Check(this.Object, player, this.Object, "EmptyForDisassemble", false, false, false, false, false, 0, 0, 0, null, null, null, null);
				}
				catch (Exception ex)
				{
					MetricsManager.LogError("EmptyForDisassemble", ex);
				}
				bool flag5 = this.NumberWanted > 1 && this.OriginalCount > 1;
				if (!this.Object.IsTemporary)
				{
					this.WasTemporary = false;
				}
				if (this.DisassemblingWhat == null)
				{
					this.DisassemblingWhat = this.Object.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
					if (flag5 || this.TotalNumberWanted > 1)
					{
						MessageQueue.AddPlayerMessage("You start disassembling " + this.Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, true);
					}
				}
				if (this.DisassemblingWhere == null && this.Object.CurrentCell != null)
				{
					this.DisassemblingWhere = player.DescribeDirectionToward(this.Object, false, false);
				}
				bool flag6 = false;
				if (this.NumberDone < this.NumberWanted)
				{
					string text = "";
					bool flag7 = true;
					bool flag8 = false;
					bool flag9 = false;
					int num8 = 0;
					if (!this.WasTemporary)
					{
						if (tinkerItem.Bits.Length == 1)
						{
							if (tinkerItem.NumberMade <= 1 || Stat.Random(1, tinkerItem.NumberMade + 1) == 1)
							{
								text += tinkerItem.Bits;
							}
						}
						else
						{
							int num9 = tinkerItem.Bits.Length - 1;
							for (int i = 0; i < tinkerItem.Bits.Length; i++)
							{
								if ((num9 == i || this.BitChance.in100()) && (tinkerItem.NumberMade <= 1 || Stat.Random(1, tinkerItem.NumberMade + 1) == 1))
								{
									text += tinkerItem.Bits[i].ToString();
								}
							}
						}
						if (flag3)
						{
							if (flag4)
							{
								ReverseEngineeringSifrah reverseEngineeringSifrah = new ReverseEngineeringSifrah(this.Object, num6, num7, num5, tinkerData);
								reverseEngineeringSifrah.Play(this.Object);
								if (reverseEngineeringSifrah.Succeeded)
								{
									flag8 = true;
									if (list != null)
									{
										if (reverseEngineeringSifrah.Mods > 0)
										{
											if (list.Count > reverseEngineeringSifrah.Mods)
											{
												List<TinkerData> list2 = new List<TinkerData>();
												for (int j = 0; j < reverseEngineeringSifrah.Mods; j++)
												{
													list2.Add(list[j]);
												}
												list = list2;
											}
										}
										else
										{
											list = null;
										}
									}
									if (reverseEngineeringSifrah.Critical)
									{
										flag7 = false;
										flag9 = true;
									}
									num8 = reverseEngineeringSifrah.XP;
								}
								else
								{
									tinkerData = null;
									list = null;
									if (reverseEngineeringSifrah.Critical)
									{
										this.Abort = true;
										this.BitsDone = "";
									}
								}
							}
							else
							{
								tinkerData = null;
								list = null;
							}
						}
						else if (!num3.in100())
						{
							tinkerData = null;
							list = null;
						}
						if (tinkerData != null || (list != null && list.Count > 0))
						{
							bool flag10 = false;
							string text2 = null;
							if (tinkerData != null)
							{
								GameObject gameObject = GameObject.CreateSample(tinkerData.Blueprint);
								tinkerData.DisplayName = gameObject.DisplayNameOnlyDirect;
								text2 = "build " + (gameObject.IsPlural ? gameObject.DisplayNameOnlyDirect : gameObject.GetPluralName(true, true, false, true));
								flag10 = true;
							}
							if (list != null)
							{
								List<string> list3 = new List<string>();
								foreach (TinkerData tinkerData4 in list)
								{
									list3.Add(tinkerData4.DisplayName);
								}
								string text3 = "mod items with the " + Grammar.MakeAndList(list3, true) + " " + ((list3.Count == 1) ? "mod" : "mods");
								if (text2 == null)
								{
									text2 = text3;
								}
								else
								{
									text2 = text2 + " and " + text3;
								}
							}
							if (text2 != null)
							{
								string text4 = "{{G|Eureka! You may now " + text2;
								if (flag7)
								{
									text4 += ".";
								}
								else
								{
									text4 = text4 + "... and were able to work out how without needing to destroy " + (flag10 ? (this.Object.IsPlural ? "these" : "this one") : this.Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)) + "!";
								}
								text4 += "}}";
								if (this.ReverseEngineeringMessage.IsNullOrEmpty())
								{
									this.ReverseEngineeringMessage = text4;
								}
								else
								{
									this.ReverseEngineeringMessage = this.ReverseEngineeringMessage + "\n\n" + text4;
								}
							}
							if (tinkerData != null)
							{
								TinkerData.KnownRecipes.Add(tinkerData);
							}
							if (list != null)
							{
								TinkerData.KnownRecipes.AddRange(list);
							}
						}
						else if (flag8)
						{
							string text5 = "You are unable to make further progress reverse engineering " + this.Object.poss("modding", true) + ".";
							if (this.ReverseEngineeringMessage.IsNullOrEmpty())
							{
								this.ReverseEngineeringMessage = text5;
							}
							else
							{
								this.ReverseEngineeringMessage = this.ReverseEngineeringMessage + "\n\n" + text5;
							}
						}
						if (num8 > 0)
						{
							player.AwardXP(num8, -1, 0, int.MaxValue, null, this.Object, null, null, null, null);
						}
						if (flag9)
						{
							TinkeringSifrah.AwardInsight();
						}
					}
					this.NumberDone++;
					this.TotalNumberDone++;
					if (this.TotalNumberWanted > 1)
					{
						Loading.SetLoadingStatus(string.Concat(new string[]
						{
							"Disassembled ",
							this.TotalNumberDone.Things("item", null),
							" of ",
							this.TotalNumberWanted.ToString(),
							"..."
						}), false);
					}
					if (!this.Abort)
					{
						if (player.HasRegisteredEvent("ModifyBitsReceived"))
						{
							Event @event = Event.New("ModifyBitsReceived", "Item", this.Object, "Bits", text);
							player.FireEvent(@event);
							text = @event.GetStringParameter("Bits", "");
						}
						this.BitsDone += text;
					}
					num += this.EnergyCostPer;
					this.DoBitMessage = true;
					this.Object.PlayWorldOrUISound("Sounds/Misc/sfx_interact_artifact_disassemble", null);
					if (flag7)
					{
						if (this.Alarms != null)
						{
							foreach (Action<GameObject> action in this.Alarms)
							{
								action(player);
							}
							this.Alarms = null;
						}
						this.Object.Destroy(null, false, false, null);
						if (!GameObject.Validate(this.Object) || this.Object.IsNowhere())
						{
							flag6 = false;
						}
					}
				}
				if (this.NumberDone >= this.NumberWanted || flag6)
				{
					this.ProcessDisassemblingWhat();
					if (!this.Abort)
					{
						if (!this.Queue.IsNullOrEmpty<GameObject>())
						{
							this.Object = this.Queue[0];
							this.Queue.RemoveAt(0);
							this.NumberDone = 0;
							this.OriginalCount = this.Object.Count;
							if (this.QueueNumberWanted == null || !this.QueueNumberWanted.TryGetValue(this.Object, out this.NumberWanted))
							{
								this.NumberWanted = this.OriginalCount;
							}
							this.Alarms = null;
							Dictionary<GameObject, List<Action<GameObject>>> queueAlarms = this.QueueAlarms;
							if (queueAlarms != null)
							{
								queueAlarms.TryGetValue(this.Object, out this.Alarms);
							}
						}
						else
						{
							this.Abort = true;
						}
					}
				}
			}
			finally
			{
				if (num > 0)
				{
					player.UseEnergy(num, "Skill Tinkering Disassemble", null, null, false);
				}
			}
			return true;
		}

		// Token: 0x0600740B RID: 29707 RVA: 0x002BD030 File Offset: 0x002BB230
		public override string GetInterruptBecause()
		{
			return this.InterruptBecause;
		}

		// Token: 0x0600740C RID: 29708 RVA: 0x002BD038 File Offset: 0x002BB238
		public override bool CanComplete()
		{
			return this.Abort || this.TotalNumberDone >= this.TotalNumberWanted;
		}

		// Token: 0x0600740D RID: 29709 RVA: 0x002BD055 File Offset: 0x002BB255
		public override void Interrupt()
		{
			if (this.TotalNumberWanted > 1)
			{
				Loading.SetLoadingStatus("Interrupted!", false);
			}
			base.Interrupt();
		}

		// Token: 0x0600740E RID: 29710 RVA: 0x002BD071 File Offset: 0x002BB271
		public override void Complete()
		{
			if (this.TotalNumberWanted > 1)
			{
				Loading.SetLoadingStatus("Finished disassembling.", false);
			}
			base.Complete();
		}

		// Token: 0x0600740F RID: 29711 RVA: 0x002BD090 File Offset: 0x002BB290
		public override void End()
		{
			GameObject player = The.Player;
			if (this.TotalNumberDone > 0)
			{
				this.ProcessDisassemblingWhat();
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				stringBuilder.Append("You disassemble ");
				if (this.DisassembledWhats.IsNullOrEmpty<string>())
				{
					stringBuilder.Append(this.DisassembledWhat ?? "something");
					if (!this.DisassembledWhere.IsNullOrEmpty())
					{
						stringBuilder.Append(' ').Append(this.DisassembledWhere);
					}
				}
				else
				{
					List<string> list = new List<string>();
					List<string> list2 = new List<string>();
					string text = null;
					foreach (string text2 in this.DisassembledWhats)
					{
						string text3;
						this.DisassembledWhatsWhere.TryGetValue(text2, out text3);
						if (text3 != text && list2.Count > 0)
						{
							string text4 = Grammar.MakeAndList(list2, true);
							if (!text.IsNullOrEmpty())
							{
								text4 = text4 + " " + text;
							}
							list.Add(text4);
							list2.Clear();
						}
						text = text3;
						list2.Add(text2);
					}
					string text5 = Grammar.MakeAndList(list2, true);
					if (!text.IsNullOrEmpty())
					{
						text5 = text5 + " " + text;
					}
					list.Add(text5);
					stringBuilder.Append(Grammar.MakeAndList(list, true));
				}
				stringBuilder.Append('.');
				if (!this.ReverseEngineeringMessage.IsNullOrEmpty())
				{
					if (this.ReverseEngineeringMessage.Contains('\n'))
					{
						stringBuilder.Append("\n\n").Append(this.ReverseEngineeringMessage).Append("\n\n");
					}
					else
					{
						stringBuilder.Compound(this.ReverseEngineeringMessage, ' ');
					}
				}
				string text6 = null;
				if (!this.BitsDone.IsNullOrEmpty())
				{
					player.RequirePart<BitLocker>(false).AddBits(this.BitsDone);
					if (this.DoBitMessage)
					{
						text6 = "You receive tinkering bits <{{|" + BitType.GetDisplayString(this.BitsDone) + "}}>.";
					}
				}
				else if (this.WasTemporary)
				{
					text6 = "The parts crumble into dust.";
				}
				if (!text6.IsNullOrEmpty())
				{
					stringBuilder.Compound(text6, ' ');
				}
				if (this.Auto && this.ReverseEngineeringMessage.IsNullOrEmpty())
				{
					MessageQueue.AddPlayerMessage(stringBuilder.ToString(), null, true);
				}
				else
				{
					Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			base.End();
		}

		// Token: 0x06007410 RID: 29712 RVA: 0x002BD300 File Offset: 0x002BB500
		public void Enqueue(GameObject Object, int? NumberWanted = null, bool Auto = false, List<Action<GameObject>> Alarms = null)
		{
			int num = NumberWanted ?? Object.Count;
			if (this.Queue == null)
			{
				this.Queue = new List<GameObject>();
			}
			if (this.Queue.Contains(Object))
			{
				if (this.QueueNumberWanted == null)
				{
					this.QueueNumberWanted = new Dictionary<GameObject, int>();
				}
				int num2;
				if (this.QueueNumberWanted.TryGetValue(Object, out num2))
				{
					this.QueueNumberWanted[Object] = num2 + num;
				}
				else
				{
					this.QueueNumberWanted[Object] = num;
				}
				if (!Alarms.IsNullOrEmpty<Action<GameObject>>())
				{
					if (this.QueueAlarms == null)
					{
						this.QueueAlarms = new Dictionary<GameObject, List<Action<GameObject>>>();
					}
					List<Action<GameObject>> list;
					if (this.QueueAlarms.TryGetValue(Object, out list))
					{
						list.AddRange(Alarms);
					}
					else
					{
						this.QueueAlarms[Object] = Alarms;
					}
				}
			}
			else
			{
				this.Queue.Add(Object);
				if (this.QueueNumberWanted == null)
				{
					this.QueueNumberWanted = new Dictionary<GameObject, int>();
				}
				this.QueueNumberWanted[Object] = num;
				if (!Alarms.IsNullOrEmpty<Action<GameObject>>())
				{
					if (this.QueueAlarms == null)
					{
						this.QueueAlarms = new Dictionary<GameObject, List<Action<GameObject>>>();
					}
					this.QueueAlarms[Object] = Alarms;
				}
			}
			this.TotalNumberWanted += num;
			if (!Auto && this.Auto)
			{
				this.Auto = false;
			}
		}

		// Token: 0x06007411 RID: 29713 RVA: 0x002BD44C File Offset: 0x002BB64C
		private void ProcessDisassemblingWhat()
		{
			if (!this.DisassemblingWhat.IsNullOrEmpty())
			{
				string text = this.DisassemblingWhat;
				if (this.NumberDone > 1 || this.NumberWanted > 1)
				{
					text = text + " x" + this.NumberDone.ToString();
				}
				if (this.DisassembledWhat == null)
				{
					this.DisassembledWhat = text;
					this.DisassembledWhere = this.DisassemblingWhere;
				}
				else
				{
					if (this.DisassembledWhats.IsNullOrEmpty<string>())
					{
						if (this.DisassembledWhats == null)
						{
							this.DisassembledWhats = new List<string>();
						}
						if (this.DisassembledWhatsWhere == null)
						{
							this.DisassembledWhatsWhere = new Dictionary<string, string>();
						}
						this.DisassembledWhats.Add(this.DisassembledWhat);
						this.DisassembledWhatsWhere[this.DisassembledWhat] = this.DisassembledWhere;
					}
					this.DisassembledWhats.Add(text);
					this.DisassembledWhatsWhere[text] = this.DisassemblingWhere;
				}
				this.DisassemblingWhat = null;
				this.DisassemblingWhere = null;
			}
		}

		// Token: 0x04003566 RID: 13670
		public GameObject Object;

		// Token: 0x04003567 RID: 13671
		public int NumberWanted;

		// Token: 0x04003568 RID: 13672
		public int NumberDone;

		// Token: 0x04003569 RID: 13673
		public int TotalNumberWanted;

		// Token: 0x0400356A RID: 13674
		public int TotalNumberDone;

		// Token: 0x0400356B RID: 13675
		public int OriginalCount;

		// Token: 0x0400356C RID: 13676
		public int TotalOriginalCount;

		// Token: 0x0400356D RID: 13677
		public int BitChance = int.MinValue;

		// Token: 0x0400356E RID: 13678
		public int EnergyCostPer = 1000;

		// Token: 0x0400356F RID: 13679
		public bool Auto;

		// Token: 0x04003570 RID: 13680
		public bool WasTemporary;

		// Token: 0x04003571 RID: 13681
		public bool Abort;

		// Token: 0x04003572 RID: 13682
		public bool DoBitMessage;

		// Token: 0x04003573 RID: 13683
		public string DisassemblingWhat;

		// Token: 0x04003574 RID: 13684
		public string DisassemblingWhere;

		// Token: 0x04003575 RID: 13685
		public string DisassembledWhat;

		// Token: 0x04003576 RID: 13686
		public string DisassembledWhere;

		// Token: 0x04003577 RID: 13687
		public string ReverseEngineeringMessage;

		// Token: 0x04003578 RID: 13688
		public string BitsDone = "";

		// Token: 0x04003579 RID: 13689
		public string InterruptBecause;

		// Token: 0x0400357A RID: 13690
		public List<string> DisassembledWhats;

		// Token: 0x0400357B RID: 13691
		public Dictionary<string, string> DisassembledWhatsWhere;

		// Token: 0x0400357C RID: 13692
		public List<Action<GameObject>> Alarms;

		// Token: 0x0400357D RID: 13693
		public List<GameObject> Queue;

		// Token: 0x0400357E RID: 13694
		public Dictionary<GameObject, int> QueueNumberWanted;

		// Token: 0x0400357F RID: 13695
		public Dictionary<GameObject, List<Action<GameObject>>> QueueAlarms;
	}
}
