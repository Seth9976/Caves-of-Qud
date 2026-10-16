using System;
using System.Collections.Generic;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02000FEF RID: 4079
	[Serializable]
	public class AddsRep : IActivePart
	{
		// Token: 0x0600A293 RID: 41619 RVA: 0x003B62B8 File Offset: 0x003B44B8
		public AddsRep()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A294 RID: 41620 RVA: 0x003B62D4 File Offset: 0x003B44D4
		public AddsRep(string Faction)
			: this()
		{
			if (!Factions.Exists(Faction))
			{
				if (Faction.Contains(','))
				{
					List<string> list = Faction.CachedCommaExpansion();
					bool flag = false;
					using (List<string>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (!Factions.Exists(enumerator.Current))
							{
								flag = true;
								break;
							}
						}
					}
					if (flag)
					{
						list = new List<string>(list.ToArray());
						bool flag2 = false;
						int i = 0;
						int count = list.Count;
						while (i < count)
						{
							string text = list[i];
							if (!Factions.Exists(text))
							{
								string newFaction = CompatManager.GetNewFaction(text);
								if (newFaction != null)
								{
									MetricsManager.LogWarning(string.Concat(new string[] { "faction name ", Faction, " should be ", newFaction, ", support will be removed after Q2 2024" }));
									list[i] = newFaction;
									flag2 = true;
								}
							}
							i++;
						}
						if (flag2)
						{
							Faction = string.Join(",", list.ToArray());
						}
					}
				}
				else
				{
					string newFaction2 = CompatManager.GetNewFaction(Faction);
					if (newFaction2 != null)
					{
						MetricsManager.LogWarning(string.Concat(new string[] { "faction name ", Faction, " should be ", newFaction2, ", support will be removed after Q2 2024" }));
						Faction = newFaction2;
					}
				}
			}
			this.Faction = Faction;
		}

		// Token: 0x0600A295 RID: 41621 RVA: 0x003B6438 File Offset: 0x003B4638
		public AddsRep(string Faction, int Value)
			: this(Faction)
		{
			this.Value = Value;
		}

		// Token: 0x0600A296 RID: 41622 RVA: 0x003B6448 File Offset: 0x003B4648
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			if (!Factions.Exists(this.Faction))
			{
				if (this.Faction.Contains(','))
				{
					List<string> list = this.Faction.CachedCommaExpansion();
					bool flag = false;
					using (List<string>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (!Factions.Exists(enumerator.Current))
							{
								flag = true;
								break;
							}
						}
					}
					if (flag)
					{
						list = new List<string>(list.ToArray());
						bool flag2 = false;
						int i = 0;
						int count = list.Count;
						while (i < count)
						{
							string text = list[i];
							if (!Factions.Exists(text))
							{
								string newFaction = CompatManager.GetNewFaction(text);
								if (newFaction != null)
								{
									if (Reader.FileVersion >= 350)
									{
										MetricsManager.LogWarning(string.Concat(new string[] { "faction name ", this.Faction, " should be ", newFaction, ", support will be removed after Q2 2024" }));
									}
									list[i] = newFaction;
									flag2 = true;
								}
								else
								{
									Faction byDisplayName = Factions.GetByDisplayName(this.Faction);
									if (byDisplayName != null)
									{
										list[i] = byDisplayName.Name;
										flag2 = true;
									}
								}
							}
							i++;
						}
						if (flag2)
						{
							this.Faction = string.Join(",", list.ToArray());
							return;
						}
					}
				}
				else
				{
					string newFaction2 = CompatManager.GetNewFaction(this.Faction);
					if (newFaction2 != null)
					{
						if (Reader.FileVersion >= 350)
						{
							MetricsManager.LogWarning(string.Concat(new string[] { "faction name ", this.Faction, " should be ", newFaction2, ", support will be removed after Q2 2024" }));
						}
						this.Faction = newFaction2;
						return;
					}
					Faction byDisplayName2 = Factions.GetByDisplayName(this.Faction);
					if (byDisplayName2 != null)
					{
						this.Faction = byDisplayName2.Name;
					}
				}
			}
		}

		// Token: 0x0600A297 RID: 41623 RVA: 0x003B6634 File Offset: 0x003B4834
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			AddsRep addsRep = base.DeepCopy(Parent, MapInv) as AddsRep;
			addsRep.AppliedBonus = false;
			return addsRep;
		}

		// Token: 0x0600A298 RID: 41624 RVA: 0x003B664C File Offset: 0x003B484C
		public override bool SameAs(IPart Part)
		{
			AddsRep addsRep = Part as AddsRep;
			return !(addsRep.Faction != this.Faction) && addsRep.Value == this.Value && addsRep.AppliedBonus == this.AppliedBonus && base.SameAs(Part);
		}

		// Token: 0x0600A299 RID: 41625 RVA: 0x003B669C File Offset: 0x003B489C
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A29A RID: 41626 RVA: 0x003B669F File Offset: 0x003B489F
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckApplyBonus(null, true, Amount);
		}

		// Token: 0x0600A29B RID: 41627 RVA: 0x003B66AC File Offset: 0x003B48AC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BootSequenceDoneEvent.ID || ID == BootSequenceInitializedEvent.ID || ID == CellChangedEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == EquippedEvent.ID || ID == GetShortDescriptionEvent.ID || ID == PowerSwitchFlippedEvent.ID || ID == UnequippedEvent.ID || ID == PooledEvent<AfterPlayerBodyChangeEvent>.ID || (this.WorksOnCarrier && ID == TakenEvent.ID) || (this.WorksOnCarrier && ID == DroppedEvent.ID);
		}

		// Token: 0x0600A29C RID: 41628 RVA: 0x003B6738 File Offset: 0x003B4938
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ParentObject.IsTakeable())
			{
				string text = ((this.IsPowerSwitchSensitive && this.ParentObject.HasPart<PowerSwitch>()) ? "When activated, " : "");
				string statusSummary = base.GetStatusSummary();
				string text2;
				if (statusSummary == null)
				{
					text2 = "";
				}
				else
				{
					text2 = " (" + statusSummary + ")";
				}
				AddsRep.AppendDescription(E.Postfix, this.Faction, this.Value, text, text2, true, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A29D RID: 41629 RVA: 0x003B67BC File Offset: 0x003B49BC
		public static void AppendDescription(StringBuilder SB, string Faction, int Value, string Prefix = null, string Postfix = null, bool Rules = false, bool SignedRules = false)
		{
			foreach (ReadOnlySpan<char> readOnlySpan in Faction.DelimitedBy(','))
			{
				int num = Value;
				string text = "";
				ReadOnlySpan<char> readOnlySpan2;
				ReadOnlySpan<char> readOnlySpan3;
				ReadOnlySpan<char> readOnlySpan4;
				readOnlySpan.Split(':', out readOnlySpan2, out readOnlySpan3, out readOnlySpan4);
				if (readOnlySpan4.Length <= 0 || !readOnlySpan4.SequenceEqual<char>("hidden"))
				{
					int num2;
					if (readOnlySpan3.Length > 0 && int.TryParse(readOnlySpan3, out num2))
					{
						num = num2;
					}
					if (readOnlySpan2.SequenceEqual<char>("*allvisiblefactions"))
					{
						text = "every faction";
					}
					else if (readOnlySpan2.SequenceEqual<char>("*alloldfactions"))
					{
						text = "every faction that existed during the time of the sultanate";
					}
					if (text.IsNullOrEmpty())
					{
						text = XRL.World.Faction.GetFormattedName(new string(readOnlySpan2));
					}
					SB.Append(Rules ? "\n{{rules|" : "\n");
					if (!Prefix.IsNullOrEmpty())
					{
						SB.Append(Prefix);
					}
					if (SignedRules)
					{
						SB.AppendSigned(num, "rules");
					}
					else
					{
						SB.AppendSigned(num);
					}
					SB.Append(" reputation with ").Append(text);
					if (!Postfix.IsNullOrEmpty())
					{
						SB.Append(Postfix);
					}
					if (Rules)
					{
						SB.Append("}}");
					}
				}
			}
		}

		// Token: 0x0600A29E RID: 41630 RVA: 0x003B68FE File Offset: 0x003B4AFE
		public override bool HandleEvent(EquippedEvent E)
		{
			this.CheckApplyBonus(E.Actor, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A29F RID: 41631 RVA: 0x003B6915 File Offset: 0x003B4B15
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.CheckApplyBonus(E.Actor, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2A0 RID: 41632 RVA: 0x003B692C File Offset: 0x003B4B2C
		public override bool HandleEvent(TakenEvent E)
		{
			this.CheckApplyBonus(E.Actor, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2A1 RID: 41633 RVA: 0x003B6943 File Offset: 0x003B4B43
		public override bool HandleEvent(DroppedEvent E)
		{
			this.CheckApplyBonus(E.Actor, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2A2 RID: 41634 RVA: 0x003B695A File Offset: 0x003B4B5A
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckApplyBonus(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2A3 RID: 41635 RVA: 0x003B696C File Offset: 0x003B4B6C
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.CheckApplyBonus(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2A4 RID: 41636 RVA: 0x003B697E File Offset: 0x003B4B7E
		public override bool HandleEvent(BootSequenceDoneEvent E)
		{
			this.CheckApplyBonus(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2A5 RID: 41637 RVA: 0x003B6990 File Offset: 0x003B4B90
		public override bool HandleEvent(BootSequenceInitializedEvent E)
		{
			this.CheckApplyBonus(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2A6 RID: 41638 RVA: 0x003B69A2 File Offset: 0x003B4BA2
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.CheckApplyBonus(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2A7 RID: 41639 RVA: 0x003B69B4 File Offset: 0x003B4BB4
		public override bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			this.CheckApplyBonus(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2A8 RID: 41640 RVA: 0x003B69C6 File Offset: 0x003B4BC6
		public override bool HandleEvent(AfterPlayerBodyChangeEvent E)
		{
			this.CheckApplyBonus(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2A9 RID: 41641 RVA: 0x003B69D8 File Offset: 0x003B4BD8
		public static AddsRep AddModifier(GameObject Object, string Faction)
		{
			AddsRep addsRep;
			if (Object.TryGetPart<AddsRep>(out addsRep))
			{
				foreach (string text in Faction.Split(',', StringSplitOptions.None))
				{
					if (addsRep.Faction != "")
					{
						AddsRep addsRep2 = addsRep;
						addsRep2.Faction += ",";
					}
					if (text.Contains(':'))
					{
						string[] array2 = text.Split(':', StringSplitOptions.None);
						string text2 = array2[0];
						int num = Convert.ToInt32(array2[1]);
						if (num == addsRep.Value && array2.Length <= 2)
						{
							AddsRep addsRep3 = addsRep;
							addsRep3.Faction += text2;
						}
						else
						{
							AddsRep addsRep4 = addsRep;
							addsRep4.Faction += text;
						}
						if (addsRep.AppliedBonus)
						{
							The.Game.PlayerReputation.Modify(text2, num, "AddsRepApply", null, null, true, true);
						}
					}
					else
					{
						AddsRep addsRep5 = addsRep;
						addsRep5.Faction += text;
						if (addsRep.AppliedBonus)
						{
							The.Game.PlayerReputation.Modify(text, addsRep.Value, "AddsRepApply", null, null, true, true);
						}
					}
				}
			}
			else
			{
				addsRep = new AddsRep(Faction);
				Object.AddPart<AddsRep>(addsRep, true, false);
			}
			return addsRep;
		}

		// Token: 0x0600A2AA RID: 41642 RVA: 0x003B6B10 File Offset: 0x003B4D10
		public static AddsRep AddModifier(GameObject Object, string Faction, int Value)
		{
			AddsRep addsRep;
			if (Object.TryGetPart<AddsRep>(out addsRep))
			{
				foreach (string text in Faction.Split(',', StringSplitOptions.None))
				{
					if (addsRep.Faction != "")
					{
						AddsRep addsRep2 = addsRep;
						addsRep2.Faction += ",";
					}
					if (text.Contains(':'))
					{
						string[] array2 = text.Split(':', StringSplitOptions.None);
						string text2 = array2[0];
						int num = Convert.ToInt32(array2[1]);
						if (num == addsRep.Value && array2.Length <= 2)
						{
							AddsRep addsRep3 = addsRep;
							addsRep3.Faction += text2;
						}
						else
						{
							AddsRep addsRep4 = addsRep;
							addsRep4.Faction += text;
						}
						if (addsRep.AppliedBonus)
						{
							The.Game.PlayerReputation.Modify(text2, num, "AddsRepApply", null, null, true, true);
						}
					}
					else
					{
						if (Value == addsRep.Value)
						{
							AddsRep addsRep5 = addsRep;
							addsRep5.Faction += text;
						}
						else
						{
							AddsRep addsRep6 = addsRep;
							addsRep6.Faction = addsRep6.Faction + text + ":" + Value.ToString();
						}
						if (addsRep.AppliedBonus)
						{
							The.Game.PlayerReputation.Modify(text, Value, "AddsRepApply", null, null, true, true);
						}
					}
				}
			}
			else
			{
				addsRep = new AddsRep(Faction, Value);
				Object.AddPart<AddsRep>(addsRep, true, false);
			}
			return addsRep;
		}

		// Token: 0x0600A2AB RID: 41643 RVA: 0x003B6C6C File Offset: 0x003B4E6C
		private void ApplyBonus(GameObject Subject)
		{
			if (!this.AppliedBonus && Subject != null && Subject.IsPlayer() && this.IsObjectActivePartSubject(Subject))
			{
				if (this.Faction.StartsWith("*allvisiblefactions:"))
				{
					int num = Convert.ToInt32(this.Faction.Split(':', StringSplitOptions.None)[1]);
					using (List<string>.Enumerator enumerator = Factions.GetVisibleFactionNames().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							The.Game.PlayerReputation.Modify(text, num, "AddsRepApply", null, null, true, true);
						}
						goto IL_018C;
					}
				}
				if (this.Faction == "*alloldfactions")
				{
					int value = this.Value;
					using (IEnumerator<Faction> enumerator2 = Factions.Loop().GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Faction faction = enumerator2.Current;
							if (faction.Visible && faction.Old)
							{
								The.Game.PlayerReputation.Modify(faction, value, null, null, "AddsRepApply", true, true, false, false);
							}
						}
						goto IL_018C;
					}
				}
				foreach (string text2 in this.Faction.Split(',', StringSplitOptions.None))
				{
					string text3 = text2;
					int num2 = this.Value;
					if (text2.Contains(':'))
					{
						string[] array2 = text2.Split(':', StringSplitOptions.None);
						text3 = array2[0];
						num2 = Convert.ToInt32(array2[1]);
					}
					The.Game.PlayerReputation.Modify(text3, num2, "AddsRepApply", null, null, true, true);
				}
				IL_018C:
				this.AppliedBonus = true;
			}
		}

		// Token: 0x0600A2AC RID: 41644 RVA: 0x003B6E28 File Offset: 0x003B5028
		private void UnapplyBonus()
		{
			if (this.AppliedBonus)
			{
				if (this.Faction.StartsWith("*allvisiblefactions:"))
				{
					int num = Convert.ToInt32(this.Faction.Split(':', StringSplitOptions.None)[1]);
					using (List<string>.Enumerator enumerator = Factions.GetVisibleFactionNames().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							The.Game.PlayerReputation.Modify(text, -num, "AddsRepUnapply", null, null, true, true);
						}
						goto IL_0172;
					}
				}
				if (this.Faction == "*alloldfactions")
				{
					int value = this.Value;
					using (IEnumerator<Faction> enumerator2 = Factions.Loop().GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Faction faction = enumerator2.Current;
							if (faction.Visible && faction.Old)
							{
								The.Game.PlayerReputation.Modify(faction, -value, null, null, "AddsRepUnapply", true, true, false, false);
							}
						}
						goto IL_0172;
					}
				}
				foreach (string text2 in this.Faction.Split(',', StringSplitOptions.None))
				{
					string text3 = text2;
					int num2 = this.Value;
					if (text2.Contains(':'))
					{
						string[] array2 = text2.Split(':', StringSplitOptions.None);
						text3 = array2[0];
						num2 = Convert.ToInt32(array2[1]);
					}
					The.Game.PlayerReputation.Modify(text3, -num2, "AddsRepUnapply", null, null, true, true);
				}
				IL_0172:
				this.AppliedBonus = false;
			}
		}

		// Token: 0x0600A2AD RID: 41645 RVA: 0x003B6FCC File Offset: 0x003B51CC
		public void CheckApplyBonus(GameObject Subject = null, bool UseCharge = false, int MultipleCharge = 1)
		{
			if (Subject == null)
			{
				Subject = this.GetActivePartFirstSubject();
			}
			if (this.AppliedBonus)
			{
				if (base.IsDisabled(UseCharge, false, false, false, false, false, false, false, false, false, MultipleCharge, null, false, 0L, null) || !Subject.IsPlayer())
				{
					this.UnapplyBonus();
					return;
				}
			}
			else if (base.IsReady(UseCharge, false, false, false, false, false, false, false, false, false, MultipleCharge, null, false, 0L, null))
			{
				this.ApplyBonus(Subject);
			}
		}

		// Token: 0x0400435F RID: 17247
		public string Faction = "";

		// Token: 0x04004360 RID: 17248
		public int Value;

		// Token: 0x04004361 RID: 17249
		public bool AppliedBonus;
	}
}
