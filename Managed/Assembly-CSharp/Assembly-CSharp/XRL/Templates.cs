using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using XRL.Language;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace XRL
{
	// Token: 0x0200052C RID: 1324
	[HasModSensitiveStaticCache]
	public static class Templates
	{
		// Token: 0x060038A7 RID: 14503 RVA: 0x00116518 File Offset: 0x00114718
		[ModSensitiveCacheInit]
		public static void Init()
		{
			foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("templates", true, true))
			{
				Templates.HandleNodes(xmlDataHelper);
			}
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x00116568 File Offset: 0x00114768
		public static void HandleNodes(XmlDataHelper xml)
		{
			xml.HandleNodes(Templates._nodes, null);
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x00116578 File Offset: 0x00114778
		public static void ReadTemplateNode(XmlDataHelper xml)
		{
			Templates.Template template = Templates.Template.HandleTemplateNode(xml);
			if (template != null)
			{
				Templates.TemplateByID[template.ID] = template;
			}
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x001165A0 File Offset: 0x001147A0
		public static void LoadTemplateFromExternal(string ID, XmlDataHelper xml)
		{
			Templates.Template template;
			if (!Templates.TemplateByID.TryGetValue(ID, out template))
			{
				template = new Templates.Template();
				template.ID = ID;
			}
			template.Nodes.Clear();
			template.Nodes.AddRange(Templates.ParseXMLTemplateNodes(xml, true));
			Templates.TemplateByID[ID] = template;
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x001165F2 File Offset: 0x001147F2
		public static IEnumerable<Templates.ITemplateNode> ParseP(XmlDataHelper xml, Templates.XMLTemplateParserAdditionalData data)
		{
			foreach (Templates.ITemplateNode templateNode in Templates.ParseXMLTemplateNodes(xml, false))
			{
				yield return templateNode;
			}
			IEnumerator<Templates.ITemplateNode> enumerator = null;
			yield return Templates.NEWLINE;
			yield break;
			yield break;
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x00116602 File Offset: 0x00114802
		public static IEnumerable<Templates.ITemplateNode> ParseBR(XmlDataHelper xml, Templates.XMLTemplateParserAdditionalData data)
		{
			yield return Templates.NEWLINE;
			xml.DoneWithElement();
			yield break;
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x00116612 File Offset: 0x00114812
		public static IEnumerable<Templates.ITemplateNode> ParseXMLTemplateNodes(XmlDataHelper xml, bool ignoreWhitespace = true)
		{
			xml.WhitespaceHandling = WhitespaceHandling.All;
			if (xml.IsEmptyElement || xml.NodeType == XmlNodeType.EndElement)
			{
				yield break;
			}
			Templates.XMLTemplateParserAdditionalData data = new Templates.XMLTemplateParserAdditionalData(0);
			string NameAtOpen = xml.Name;
			while (xml.Read())
			{
				XmlNodeType nodeType = xml.NodeType;
				if (nodeType <= XmlNodeType.Text)
				{
					Templates.XMLTemplateParser xmltemplateParser;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.Text)
						{
							yield return new Templates.TextNode(xml.Value);
						}
					}
					else if (Templates.TemplateParsers.TryGetValue(xml.Name, out xmltemplateParser))
					{
						foreach (Templates.ITemplateNode templateNode in xmltemplateParser(xml, data))
						{
							yield return templateNode;
						}
						IEnumerator<Templates.ITemplateNode> enumerator = null;
					}
					else
					{
						xml.ParseWarning("Unknown " + xml.Name + " node in template");
					}
				}
				else if (nodeType != XmlNodeType.Comment)
				{
					switch (nodeType)
					{
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						if (ignoreWhitespace)
						{
							continue;
						}
						yield return new Templates.TextNode(xml.Value);
						break;
					case XmlNodeType.EndElement:
						if (xml.Name == NameAtOpen)
						{
							yield break;
						}
						xml.ParseWarning(string.Format("Unexpected EndElement for \"{0}\"", xml.Name));
						break;
					}
				}
				data = new Templates.XMLTemplateParserAdditionalData(data.siblingIndex + 1);
			}
			xml.WhitespaceHandling = WhitespaceHandling.None;
			yield break;
			yield break;
		}

		// Token: 0x04001C8B RID: 7307
		[ModSensitiveStaticCache(true)]
		public static Dictionary<string, Templates.Template> TemplateByID = new Dictionary<string, Templates.Template>();

		// Token: 0x04001C8C RID: 7308
		public static Dictionary<string, Action<XmlDataHelper>> _nodes = new Dictionary<string, Action<XmlDataHelper>>
		{
			{
				"templates",
				new Action<XmlDataHelper>(Templates.HandleNodes)
			},
			{
				"template",
				new Action<XmlDataHelper>(Templates.ReadTemplateNode)
			}
		};

		// Token: 0x04001C8D RID: 7309
		private static Dictionary<string, Templates.XMLTemplateParser> TemplateParsers = new Dictionary<string, Templates.XMLTemplateParser>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"p",
				new Templates.XMLTemplateParser(Templates.ParseP)
			},
			{
				"br",
				new Templates.XMLTemplateParser(Templates.ParseBR)
			},
			{
				"statline",
				new Templates.XMLTemplateParser(Templates.StatLineNode.Parse)
			},
			{
				"stat",
				new Templates.XMLTemplateParser(Templates.StatNode.Parse)
			},
			{
				"template",
				new Templates.XMLTemplateParser(Templates.TemplateNode.Parse)
			},
			{
				"switch",
				new Templates.XMLTemplateParser(Templates.SwitchNode.Parse)
			}
		};

		// Token: 0x04001C8E RID: 7310
		public static Templates.TextNode NEWLINE = new Templates.TextNode("\n");

		// Token: 0x02001950 RID: 6480
		public interface ITemplateNode
		{
			// Token: 0x0600E318 RID: 58136
			void Build(StringBuilder SB, Templates.StatCollector values);
		}

		// Token: 0x02001951 RID: 6481
		public class StatCollector
		{
			// Token: 0x0600E319 RID: 58137 RVA: 0x00526396 File Offset: 0x00524596
			public Templates.StatCollector Clear()
			{
				this.values.Clear();
				this.modifiers.Clear();
				this.prefix = "";
				this.postfix = "";
				return this;
			}

			// Token: 0x0600E31A RID: 58138 RVA: 0x005263C5 File Offset: 0x005245C5
			public Templates.StatCollector Reset(string mode)
			{
				this.mode = mode;
				return this.Clear();
			}

			// Token: 0x0600E31B RID: 58139 RVA: 0x005263D4 File Offset: 0x005245D4
			public void Set(string Key, string Value, bool changes = false, int changedState = 0)
			{
				this.values.Set(Key, new ValueTuple<string, bool, int>(Value, changes, changedState));
			}

			// Token: 0x0600E31C RID: 58140 RVA: 0x005263EB File Offset: 0x005245EB
			public void Set(string Key, int Value, bool changes = false, int changedState = 0)
			{
				this.values.Set(Key, new ValueTuple<string, bool, int>(Value.ToString(), changes, changedState));
			}

			// Token: 0x0600E31D RID: 58141 RVA: 0x00526408 File Offset: 0x00524608
			public string Get(string Key)
			{
				ValueTuple<string, bool, int> valueTuple;
				if (!this.values.TryGetValue(Key ?? "", out valueTuple))
				{
					return null;
				}
				return valueTuple.Item1;
			}

			// Token: 0x0600E31E RID: 58142 RVA: 0x00526436 File Offset: 0x00524636
			public void RenderPrefix(StringBuilder SB)
			{
				if (!string.IsNullOrEmpty(this.prefix))
				{
					SB.Append(this.prefix);
				}
			}

			// Token: 0x0600E31F RID: 58143 RVA: 0x00526452 File Offset: 0x00524652
			public void RenderPostfix(StringBuilder SB)
			{
				if (!string.IsNullOrEmpty(this.postfix))
				{
					SB.Append(this.postfix);
				}
			}

			// Token: 0x0600E320 RID: 58144 RVA: 0x00526470 File Offset: 0x00524670
			public void Render(StringBuilder SB, string Key, string Unit = null)
			{
				if (Key == "prefix")
				{
					SB.Append(this.prefix);
					return;
				}
				if (Key == "postfix")
				{
					SB.Append(this.postfix);
					return;
				}
				ValueTuple<string, bool, int> valueTuple;
				if (this.values.TryGetValue(Key, out valueTuple))
				{
					if (valueTuple.Item3 > 0)
					{
						SB.AppendColored("G", valueTuple.Item1);
					}
					else if (valueTuple.Item3 < 0)
					{
						SB.AppendColored("R", valueTuple.Item1);
					}
					else if (valueTuple.Item2)
					{
						SB.AppendColored("rules", valueTuple.Item1);
					}
					else
					{
						SB.Append(valueTuple.Item1);
					}
					if (Unit != null)
					{
						int num;
						if (int.TryParse(valueTuple.Item1, out num))
						{
							SB.Append(" ").Append((Math.Abs(num) != 1) ? Grammar.Pluralize(Unit) : Unit);
							return;
						}
						SB.Append(" ").Append(Unit);
						return;
					}
				}
				else
				{
					SB.AppendColored("K", Key);
				}
			}

			// Token: 0x0600E321 RID: 58145 RVA: 0x00526580 File Offset: 0x00524780
			public int CollectComputePowerAdjustDown(ActivatedAbilityEntry ability, string what, int baseValue, float factor, int floorDivisor = 2)
			{
				int num = GetAvailableComputePowerEvent.AdjustDown(ability.ParentObject, baseValue, factor, floorDivisor);
				this.AddComputePowerPostfix(what, num - baseValue);
				return num;
			}

			// Token: 0x0600E322 RID: 58146 RVA: 0x005265AC File Offset: 0x005247AC
			public int CollectComputePowerAdjustDown(ActivatedAbilityEntry ability, string what, int baseValue, float factor = 1f)
			{
				int num = GetAvailableComputePowerEvent.AdjustDown(ability.ParentObject, baseValue, factor, 2);
				this.AddComputePowerPostfix(what, num - baseValue);
				return num;
			}

			// Token: 0x0600E323 RID: 58147 RVA: 0x005265D4 File Offset: 0x005247D4
			public int CollectComputePowerAdjustUp(ActivatedAbilityEntry ability, string what, int baseValue, float factor = 1f)
			{
				int num = GetAvailableComputePowerEvent.AdjustUp(ability.ParentObject, baseValue, factor);
				this.AddComputePowerPostfix(what, num - baseValue);
				return num;
			}

			// Token: 0x0600E324 RID: 58148 RVA: 0x005265FC File Offset: 0x005247FC
			[return: TupleElementNames(new string[] { "resultRange", "changeSign" })]
			public ValueTuple<DieRoll, int> CollectComputePowerAdjustRangeUp(ActivatedAbilityEntry ability, string what, DieRoll range, float factor = 1f)
			{
				int num = GetAvailableComputePowerEvent.AdjustUp(ability.ParentObject, range.Min(), factor);
				int num2 = GetAvailableComputePowerEvent.AdjustUp(ability.ParentObject, range.Max(), factor);
				int num3 = this.AddComputePowerPostfix(what, num - range.Min(), num2 - range.Max());
				return new ValueTuple<DieRoll, int>(string.Format("{0}-{1}", num, num2).GetCachedDieRoll(), num3);
			}

			// Token: 0x0600E325 RID: 58149 RVA: 0x0052666C File Offset: 0x0052486C
			public int AddChangePostfix(string what, int change, string dueTo)
			{
				if (change > 0)
				{
					this.postfix += string.Format("\n{0} increased by {1} due to {2}.", what, change, dueTo);
					return 1;
				}
				if (change < 0)
				{
					this.postfix += string.Format("\n{0} reduced by {1} due to {2}.", what, -change, dueTo);
					return -1;
				}
				return 0;
			}

			// Token: 0x0600E326 RID: 58150 RVA: 0x005266D0 File Offset: 0x005248D0
			public int AddPercentChangePostfix(string what, int change, string dueTo)
			{
				if (change > 0)
				{
					this.postfix += string.Format("\n{0} increased by {1}% due to {2}.", what, change, dueTo);
					return 1;
				}
				if (change < 0)
				{
					this.postfix += string.Format("\n{0} reduced by {1}% due to {2}.", what, -change, dueTo);
					return -1;
				}
				return 0;
			}

			// Token: 0x0600E327 RID: 58151 RVA: 0x00526734 File Offset: 0x00524934
			public int AddChangePostfix(string what, int changeMin, int changeMax, string dueTo)
			{
				if (changeMin == changeMax)
				{
					return this.AddChangePostfix(what, changeMin, dueTo);
				}
				if (changeMin > 0 || changeMax > 0)
				{
					this.postfix += string.Format("\n{0} increased by {1}-{2} due to {3}.", new object[] { what, changeMin, changeMax, dueTo });
					return 1;
				}
				if (changeMax < 0 || changeMin < 0)
				{
					this.postfix += string.Format("\n{0} reduced by {1}-{2} due to {3}.", new object[]
					{
						what,
						0 - changeMax,
						0 - changeMin,
						dueTo
					});
					return -1;
				}
				return 0;
			}

			// Token: 0x0600E328 RID: 58152 RVA: 0x005267E1 File Offset: 0x005249E1
			public void AddComputePowerPostfix(string what, int change)
			{
				this.AddChangePostfix(what, change, "compute power");
			}

			// Token: 0x0600E329 RID: 58153 RVA: 0x005267F1 File Offset: 0x005249F1
			public int AddComputePowerPostfix(string what, int changeMin, int changeMax)
			{
				return this.AddChangePostfix(what, changeMin, changeMax, "compute power");
			}

			// Token: 0x0600E32A RID: 58154 RVA: 0x00526804 File Offset: 0x00524A04
			public void CollectCooldownTurns(ActivatedAbilityEntry ability, int BaseTurns, int previousCooldownDiff = 0)
			{
				if (this.mode.Contains("ability") && ability != null)
				{
					GetCooldownEvent getCooldownEvent = GetCooldownEvent.TryCalculateFor(ability.ParentObject, ability, BaseTurns * 10, 0, 0, true);
					if (getCooldownEvent == null)
					{
						this.Set("Cooldown", BaseTurns, false, 0);
						return;
					}
					int num = -(getCooldownEvent.Result - getCooldownEvent.Base);
					int num2 = num + previousCooldownDiff;
					this.Set("Cooldown", (getCooldownEvent.Result == 60 && getCooldownEvent.ResultUncapped != 60) ? 5 : ((int)Math.Ceiling((double)((float)getCooldownEvent.Result / 10f))), num2 != 0, num2);
					if (num != 0)
					{
						foreach (GetCooldownEvent.CooldownCalculation cooldownCalculation in getCooldownEvent.Calculations)
						{
							num = 0;
							if (cooldownCalculation.PercentageReduction != 0)
							{
								num -= cooldownCalculation.PercentageReduction * BaseTurns / 100;
							}
							if (cooldownCalculation.LinearReduction != 0)
							{
								num -= cooldownCalculation.LinearReduction / 10;
							}
							if (num > 0)
							{
								this.postfix += string.Format("\nCooldown increased by {0} due to {1}.", num, cooldownCalculation.Reason);
							}
							else if (num < 0)
							{
								this.postfix += string.Format("\nCooldown reduced by {0} due to {1}.", -num, cooldownCalculation.Reason);
							}
						}
						if (getCooldownEvent.ResultUncapped != getCooldownEvent.Result)
						{
							string text = ((getCooldownEvent.Result == 60) ? "5" : (getCooldownEvent.Result / 10).ToString());
							this.postfix = this.postfix + "\nCooldown cannot be reduced below " + text + " rounds.";
							return;
						}
					}
				}
				else
				{
					this.Set("Cooldown", BaseTurns, false, 0);
				}
			}

			// Token: 0x0600E32B RID: 58155 RVA: 0x005269DC File Offset: 0x00524BDC
			public void CollectCooldownTurns(ActivatedAbilityEntry ability, string BaseTurns)
			{
				if (this.mode.Contains("ability") && ability != null)
				{
					int num = BaseTurns.RollMinCached() * 10;
					int num2 = BaseTurns.RollMaxCached() * 10;
					GetCooldownEvent getCooldownEvent = GetCooldownEvent.TryCalculateFor(ability.ParentObject, ability, num, 0, 0, true);
					if (getCooldownEvent == null)
					{
						this.Set("Cooldown", BaseTurns, false, 0);
						return;
					}
					GetCooldownEvent getCooldownEvent2 = GetCooldownEvent.TryCalculateFor(ability.ParentObject, ability, num2, 0, 0, true);
					int num3 = -(getCooldownEvent2.Result - getCooldownEvent2.Base);
					if (getCooldownEvent.Result == getCooldownEvent2.Result)
					{
						this.Set("Cooldown", (int)Math.Ceiling((double)((float)getCooldownEvent.Result / 10f)), num3 != 0, num3);
					}
					else
					{
						this.Set("Cooldown", ((int)Math.Ceiling((double)((float)getCooldownEvent.Result / 10f))).ToString() + "-" + ((int)Math.Ceiling((double)((float)getCooldownEvent2.Result / 10f))).ToString(), num3 != 0, num3);
					}
					if (num3 != 0)
					{
						foreach (GetCooldownEvent.CooldownCalculation cooldownCalculation in getCooldownEvent2.Calculations)
						{
							if (cooldownCalculation.PercentageReduction > 0)
							{
								this.postfix += string.Format("\nCooldown reduced by {0}% due to {1}.", cooldownCalculation.PercentageReduction, cooldownCalculation.Reason);
							}
							else if (cooldownCalculation.PercentageReduction < 0)
							{
								this.postfix += string.Format("\nCooldown increased by {0}% due to {1}.", -cooldownCalculation.PercentageReduction, cooldownCalculation.Reason);
							}
							if (cooldownCalculation.LinearReduction > 0)
							{
								this.postfix += string.Format("\nCooldown reduced by {0} due to {1}.", cooldownCalculation.LinearReduction / 10, cooldownCalculation.Reason);
							}
							else if (cooldownCalculation.LinearReduction < 0)
							{
								this.postfix += string.Format("\nCooldown increased by {0} due to {1}.", -cooldownCalculation.LinearReduction / 10, cooldownCalculation.Reason);
							}
						}
						if (getCooldownEvent.ResultUncapped != getCooldownEvent.Result)
						{
							string text = ((getCooldownEvent.Result == 60) ? "5" : (getCooldownEvent.Result / 10).ToString());
							this.postfix = this.postfix + "\nCooldown cannot be reduced below " + text + " rounds.";
							return;
						}
					}
				}
				else
				{
					this.Set("Cooldown", BaseTurns, false, 0);
				}
			}

			// Token: 0x0600E32C RID: 58156 RVA: 0x00526C90 File Offset: 0x00524E90
			public void AddLinearBonusModifier(string stat, int bonus, string source)
			{
				List<Templates.StatCollector.ModifierInfo> list;
				if (!this.modifiers.TryGetValue(stat, out list))
				{
					list = new List<Templates.StatCollector.ModifierInfo>();
					this.modifiers.Add(stat, list);
				}
				list.Add(new Templates.StatCollector.ModifierInfo
				{
					Bonus = bonus,
					Type = Templates.StatCollector.BonusType.Linear,
					Source = source
				});
			}

			// Token: 0x0600E32D RID: 58157 RVA: 0x00526CE8 File Offset: 0x00524EE8
			public void AddPercentageBonusModifier(string stat, int bonus, string source)
			{
				List<Templates.StatCollector.ModifierInfo> list;
				if (!this.modifiers.TryGetValue(stat, out list))
				{
					list = new List<Templates.StatCollector.ModifierInfo>();
					this.modifiers.Add(stat, list);
				}
				list.Add(new Templates.StatCollector.ModifierInfo
				{
					Bonus = bonus,
					Type = Templates.StatCollector.BonusType.Percentage,
					Source = source
				});
			}

			// Token: 0x0600E32E RID: 58158 RVA: 0x00526D40 File Offset: 0x00524F40
			public int CollectBonusModifiers(string stat, int baseValue, string statDisplayName = null, bool increaseAsGood = true)
			{
				long num = (long)baseValue;
				if (string.IsNullOrEmpty(statDisplayName))
				{
					statDisplayName = stat;
				}
				List<Templates.StatCollector.ModifierInfo> list;
				if (this.modifiers.TryGetValue(stat, out list))
				{
					foreach (Templates.StatCollector.ModifierInfo modifierInfo in list.Where<Templates.StatCollector.ModifierInfo>((Templates.StatCollector.ModifierInfo bonus) => bonus.Type == Templates.StatCollector.BonusType.Linear && bonus.Bonus > 0))
					{
						this.AddChangePostfix(statDisplayName, modifierInfo.Bonus, modifierInfo.Source);
						num += (long)modifierInfo.Bonus;
					}
					int num2 = 100;
					foreach (Templates.StatCollector.ModifierInfo modifierInfo2 in list.Where<Templates.StatCollector.ModifierInfo>((Templates.StatCollector.ModifierInfo bonus) => bonus.Type == Templates.StatCollector.BonusType.Percentage && bonus.Bonus > 0))
					{
						this.AddPercentChangePostfix(statDisplayName, modifierInfo2.Bonus, modifierInfo2.Source);
						num2 += modifierInfo2.Bonus;
					}
					num *= (long)num2;
					num2 = 100;
					foreach (Templates.StatCollector.ModifierInfo modifierInfo3 in list.Where<Templates.StatCollector.ModifierInfo>((Templates.StatCollector.ModifierInfo bonus) => bonus.Type == Templates.StatCollector.BonusType.Percentage && bonus.Bonus < 0))
					{
						this.AddPercentChangePostfix(statDisplayName, modifierInfo3.Bonus, modifierInfo3.Source);
						num2 += modifierInfo3.Bonus;
					}
					num *= (long)num2;
					num /= 10000L;
					foreach (Templates.StatCollector.ModifierInfo modifierInfo4 in list.Where<Templates.StatCollector.ModifierInfo>((Templates.StatCollector.ModifierInfo bonus) => bonus.Type == Templates.StatCollector.BonusType.Linear && bonus.Bonus < 0))
					{
						this.AddChangePostfix(statDisplayName, modifierInfo4.Bonus, modifierInfo4.Source);
						num += (long)modifierInfo4.Bonus;
					}
				}
				int num3 = (int)num;
				this.Set(stat, num3, num3 != baseValue, (num3 - baseValue) * (increaseAsGood ? 1 : (-1)));
				return num3;
			}

			// Token: 0x04006579 RID: 25977
			public static Templates.StatCollector Instance = new Templates.StatCollector();

			// Token: 0x0400657A RID: 25978
			public string mode;

			// Token: 0x0400657B RID: 25979
			public string prefix = "";

			// Token: 0x0400657C RID: 25980
			public string postfix = "";

			// Token: 0x0400657D RID: 25981
			[TupleElementNames(new string[] { "text", "changes", "changedState" })]
			public Dictionary<string, ValueTuple<string, bool, int>> values = new Dictionary<string, ValueTuple<string, bool, int>>();

			// Token: 0x0400657E RID: 25982
			public Dictionary<string, List<Templates.StatCollector.ModifierInfo>> modifiers = new Dictionary<string, List<Templates.StatCollector.ModifierInfo>>();

			// Token: 0x0400657F RID: 25983
			public List<string> changes = new List<string>();

			// Token: 0x02001E86 RID: 7814
			public enum BonusType
			{
				// Token: 0x04007692 RID: 30354
				Linear,
				// Token: 0x04007693 RID: 30355
				Percentage
			}

			// Token: 0x02001E87 RID: 7815
			public struct ModifierInfo
			{
				// Token: 0x04007694 RID: 30356
				public int Bonus;

				// Token: 0x04007695 RID: 30357
				public Templates.StatCollector.BonusType Type;

				// Token: 0x04007696 RID: 30358
				public string Source;
			}
		}

		// Token: 0x02001952 RID: 6482
		public class StatLineNode : Templates.StatNode
		{
			// Token: 0x0600E331 RID: 58161 RVA: 0x00526FDF File Offset: 0x005251DF
			public StatLineNode(XmlDataHelper xml)
				: base(xml)
			{
				this.DisplayName = xml.ParseAttribute<string>("DisplayName", this.Key, false, null);
				this.HideValue = xml.ParseAttribute<string>("HideValue", this.HideValue, false, null);
			}

			// Token: 0x0600E332 RID: 58162 RVA: 0x0052701A File Offset: 0x0052521A
			public new static IEnumerable<Templates.ITemplateNode> Parse(XmlDataHelper xml, Templates.XMLTemplateParserAdditionalData data)
			{
				yield return new Templates.StatLineNode(xml);
				xml.DoneWithElement();
				yield return Templates.NEWLINE;
				yield break;
			}

			// Token: 0x0600E333 RID: 58163 RVA: 0x0052702C File Offset: 0x0052522C
			public override void Build(StringBuilder SB, Templates.StatCollector values)
			{
				if (this.HideValue != null && this.HideValue == values.Get(this.Key))
				{
					return;
				}
				if (this.Key == null)
				{
					SB.Append("{{K|%%}}");
					return;
				}
				SB.Append(this.DisplayName).Append(": ");
				base.Build(SB, values);
			}

			// Token: 0x04006580 RID: 25984
			public string DisplayName;

			// Token: 0x04006581 RID: 25985
			public string HideValue;

			// Token: 0x04006582 RID: 25986
			public new const string NODE_NAME = "statline";
		}

		// Token: 0x02001953 RID: 6483
		public class StatNode : Templates.ITemplateNode
		{
			// Token: 0x0600E334 RID: 58164 RVA: 0x00527090 File Offset: 0x00525290
			public StatNode(XmlDataHelper xml)
			{
				this.Key = xml.ParseAttribute<string>("Name", null, true, null);
				this.Filters = xml.ParseAttribute<string>("Filters", null, false, null).CachedCommaExpansion();
				string text = null;
				Templates.StatNode.DefaultUnits.TryGetValue(this.Key, out text);
				this.Unit = xml.ParseAttribute<string>("Unit", text, false, null);
			}

			// Token: 0x0600E335 RID: 58165 RVA: 0x005270F9 File Offset: 0x005252F9
			public StatNode(string Key, string Filters)
			{
				this.Key = Key;
				this.Filters = Filters.CachedCommaExpansion();
			}

			// Token: 0x0600E336 RID: 58166 RVA: 0x00527114 File Offset: 0x00525314
			public static IEnumerable<Templates.ITemplateNode> Parse(XmlDataHelper xml, Templates.XMLTemplateParserAdditionalData data)
			{
				yield return new Templates.StatNode(xml);
				xml.DoneWithElement();
				yield break;
			}

			// Token: 0x0600E337 RID: 58167 RVA: 0x00527124 File Offset: 0x00525324
			public virtual void Build(StringBuilder SB, Templates.StatCollector values)
			{
				if (this.Key == null)
				{
					SB.Append("{{K|%%}}");
					return;
				}
				if (values != null)
				{
					values.Render(SB, this.Key, this.Unit);
				}
			}

			// Token: 0x04006583 RID: 25987
			public string Key;

			// Token: 0x04006584 RID: 25988
			public List<string> Filters;

			// Token: 0x04006585 RID: 25989
			public string Unit;

			// Token: 0x04006586 RID: 25990
			public static Dictionary<string, string> DefaultUnits = new Dictionary<string, string>
			{
				{ "Cooldown", "round" },
				{ "Duration", "round" }
			};

			// Token: 0x04006587 RID: 25991
			public const string NODE_NAME = "stat";
		}

		// Token: 0x02001954 RID: 6484
		public class SwitchNode : Templates.ITemplateNode
		{
			// Token: 0x0600E339 RID: 58169 RVA: 0x00527180 File Offset: 0x00525380
			public SwitchNode(XmlDataHelper xml)
			{
				this.Key = xml.ParseAttribute<string>("Name", null, true, null);
				xml.HandleNodes(new Dictionary<string, Action<XmlDataHelper>>
				{
					{
						"case",
						new Action<XmlDataHelper>(this.ParseCaseNode)
					},
					{
						"default",
						new Action<XmlDataHelper>(this.ParseDefaultNode)
					}
				}, null);
			}

			// Token: 0x0600E33A RID: 58170 RVA: 0x005271EC File Offset: 0x005253EC
			public SwitchNode(string Key)
			{
				this.Key = Key;
			}

			// Token: 0x0600E33B RID: 58171 RVA: 0x00527206 File Offset: 0x00525406
			public static IEnumerable<Templates.ITemplateNode> Parse(XmlDataHelper xml, Templates.XMLTemplateParserAdditionalData data)
			{
				yield return new Templates.SwitchNode(xml);
				yield break;
			}

			// Token: 0x0600E33C RID: 58172 RVA: 0x00527218 File Offset: 0x00525418
			public void ParseCaseNode(XmlDataHelper xml)
			{
				Templates.CaseNode caseNode = new Templates.CaseNode(xml, false);
				this.caseNodes.Add(caseNode.Value, caseNode);
			}

			// Token: 0x0600E33D RID: 58173 RVA: 0x00527240 File Offset: 0x00525440
			public void ParseDefaultNode(XmlDataHelper xml)
			{
				if (this.defaultNode != null)
				{
					throw new Exception("Multiple 'default' nodes.");
				}
				Templates.CaseNode caseNode = new Templates.CaseNode(xml, true);
				this.defaultNode = caseNode;
			}

			// Token: 0x0600E33E RID: 58174 RVA: 0x00527270 File Offset: 0x00525470
			public virtual void Build(StringBuilder SB, Templates.StatCollector values)
			{
				string text = ((values != null) ? values.Get(this.Key) : null);
				Templates.CaseNode caseNode;
				if (text != null && this.caseNodes.TryGetValue(text, out caseNode))
				{
					caseNode.Build(SB, values);
					return;
				}
				if (this.defaultNode != null)
				{
					this.defaultNode.Build(SB, values);
				}
			}

			// Token: 0x04006588 RID: 25992
			public string Key;

			// Token: 0x04006589 RID: 25993
			public Dictionary<string, Templates.CaseNode> caseNodes = new Dictionary<string, Templates.CaseNode>();

			// Token: 0x0400658A RID: 25994
			public Templates.CaseNode defaultNode;

			// Token: 0x0400658B RID: 25995
			public const string NODE_NAME = "switch";
		}

		// Token: 0x02001955 RID: 6485
		public class CaseNode : Templates.ITemplateNode
		{
			// Token: 0x0600E33F RID: 58175 RVA: 0x005272C4 File Offset: 0x005254C4
			public CaseNode(XmlDataHelper xml, bool isDefault)
			{
				if (!isDefault)
				{
					this.Value = xml.ParseAttribute<string>("Value", this.Value, true, null);
				}
				this.Nodes.Clear();
				this.Nodes.AddRange(Templates.ParseXMLTemplateNodes(xml, false));
			}

			// Token: 0x0600E340 RID: 58176 RVA: 0x0052731B File Offset: 0x0052551B
			public CaseNode()
			{
			}

			// Token: 0x0600E341 RID: 58177 RVA: 0x00527330 File Offset: 0x00525530
			public virtual void Build(StringBuilder SB, Templates.StatCollector values)
			{
				foreach (Templates.ITemplateNode templateNode in this.Nodes)
				{
					templateNode.Build(SB, values);
				}
			}

			// Token: 0x0400658C RID: 25996
			public string Value;

			// Token: 0x0400658D RID: 25997
			public List<Templates.ITemplateNode> Nodes = new List<Templates.ITemplateNode>();

			// Token: 0x0400658E RID: 25998
			public const string NODE_NAME = "case";
		}

		// Token: 0x02001956 RID: 6486
		public class Template
		{
			// Token: 0x0600E342 RID: 58178 RVA: 0x00527384 File Offset: 0x00525584
			public static Templates.Template HandleTemplateNode(XmlDataHelper xml)
			{
				string text = xml.ParseAttribute<string>("ID", null, true, null);
				Templates.Template template;
				if (!Templates.TemplateByID.TryGetValue(text, out template))
				{
					template = new Templates.Template();
					template.ID = text;
				}
				template.Nodes.Clear();
				template.Nodes.AddRange(Templates.ParseXMLTemplateNodes(xml, true));
				return template;
			}

			// Token: 0x0600E343 RID: 58179 RVA: 0x005273DA File Offset: 0x005255DA
			public string Build(Action<Templates.StatCollector> act, string mode = null)
			{
				act(Templates.StatCollector.Instance.Reset(mode));
				return this.Build(Templates.StatCollector.Instance);
			}

			// Token: 0x0600E344 RID: 58180 RVA: 0x005273F8 File Offset: 0x005255F8
			public string Build(Templates.StatCollector values)
			{
				Templates.Template.SB.Clear();
				if (values != null)
				{
					values.Render(Templates.Template.SB, "prefix", null);
				}
				foreach (Templates.ITemplateNode templateNode in this.Nodes)
				{
					templateNode.Build(Templates.Template.SB, values);
				}
				if (values != null)
				{
					values.Render(Templates.Template.SB, "postfix", null);
				}
				return Templates.Template.SB.ToString();
			}

			// Token: 0x0400658F RID: 25999
			public string ID = "";

			// Token: 0x04006590 RID: 26000
			public List<Templates.ITemplateNode> Nodes = new List<Templates.ITemplateNode>();

			// Token: 0x04006591 RID: 26001
			public static StringBuilder SB = new StringBuilder();
		}

		// Token: 0x02001957 RID: 6487
		public class TemplateNode : Templates.ITemplateNode
		{
			// Token: 0x0600E347 RID: 58183 RVA: 0x005274B6 File Offset: 0x005256B6
			public TemplateNode(XmlDataHelper xml)
			{
				this.ID = xml.ParseAttribute<string>("ID", null, true, null);
				this.affixes = xml.ParseAttribute<bool>("affixes", false, false, null);
				xml.DoneWithElement();
			}

			// Token: 0x0600E348 RID: 58184 RVA: 0x005274EC File Offset: 0x005256EC
			public static IEnumerable<Templates.ITemplateNode> Parse(XmlDataHelper xml, Templates.XMLTemplateParserAdditionalData data)
			{
				yield return new Templates.TemplateNode(xml);
				yield break;
			}

			// Token: 0x0600E349 RID: 58185 RVA: 0x005274FC File Offset: 0x005256FC
			public void Build(StringBuilder SB, Templates.StatCollector values)
			{
				Templates.Template template;
				if (this.ID != null && Templates.TemplateByID.TryGetValue(this.ID, out template))
				{
					foreach (Templates.ITemplateNode templateNode in template.Nodes)
					{
						templateNode.Build(SB, values);
					}
				}
			}

			// Token: 0x04006592 RID: 26002
			public string ID;

			// Token: 0x04006593 RID: 26003
			public bool affixes;

			// Token: 0x04006594 RID: 26004
			public const string NODE_NAME = "template";
		}

		// Token: 0x02001958 RID: 6488
		public class TextNode : Templates.ITemplateNode
		{
			// Token: 0x0600E34A RID: 58186 RVA: 0x0052756C File Offset: 0x0052576C
			public TextNode(string Text)
			{
				this.Text = Text;
			}

			// Token: 0x0600E34B RID: 58187 RVA: 0x0052757B File Offset: 0x0052577B
			public void Build(StringBuilder SB, Templates.StatCollector values)
			{
				SB.Append(this.Text);
			}

			// Token: 0x04006595 RID: 26005
			public string Text;
		}

		// Token: 0x02001959 RID: 6489
		public struct XMLTemplateParserAdditionalData
		{
			// Token: 0x0600E34C RID: 58188 RVA: 0x0052758A File Offset: 0x0052578A
			public XMLTemplateParserAdditionalData(int siblingIndex)
			{
				this.siblingIndex = siblingIndex;
			}

			// Token: 0x17000E60 RID: 3680
			// (get) Token: 0x0600E34D RID: 58189 RVA: 0x00527593 File Offset: 0x00525793
			public bool firstChild
			{
				get
				{
					return this.siblingIndex == 0;
				}
			}

			// Token: 0x04006596 RID: 26006
			public int siblingIndex;
		}

		// Token: 0x0200195A RID: 6490
		// (Invoke) Token: 0x0600E34F RID: 58191
		public delegate IEnumerable<Templates.ITemplateNode> XMLTemplateParser(XmlDataHelper xml, Templates.XMLTemplateParserAdditionalData data);
	}
}
