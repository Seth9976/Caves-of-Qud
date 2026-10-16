using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using XRL.Collections;
using XRL.UI;
using XRL.World.Text;

namespace XRL.World.Parts
{
	// Token: 0x02000E4B RID: 3659
	[Serializable]
	public class ActivatedAbilities : IPart
	{
		// Token: 0x06009437 RID: 37943 RVA: 0x00355AB0 File Offset: 0x00353CB0
		public override void Attach()
		{
			this.ParentObject.Abilities = this;
		}

		// Token: 0x06009438 RID: 37944 RVA: 0x00355ABE File Offset: 0x00353CBE
		public override void Remove()
		{
			GameObject parentObject = this.ParentObject;
			if (((parentObject != null) ? parentObject.Abilities : null) == this)
			{
				this.ParentObject.Abilities = null;
				if (this.ParentObject.IsPlayer())
				{
					AbilityManager.RefreshPlayerAbilities();
				}
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06009439 RID: 37945 RVA: 0x00355AF3 File Offset: 0x00353CF3
		public override int Priority
		{
			get
			{
				return 90000;
			}
		}

		// Token: 0x0600943A RID: 37946 RVA: 0x00355AFA File Offset: 0x00353CFA
		public static int MinimumValueForCooldown(int Cooldown)
		{
			return Math.Max((int)Math.Round((double)Cooldown * 0.2, MidpointRounding.AwayFromZero), Math.Min(50, Cooldown));
		}

		// Token: 0x0600943B RID: 37947 RVA: 0x00355B1C File Offset: 0x00353D1C
		public int GetAbilityCount()
		{
			if (this.AbilityByGuid != null)
			{
				return this.AbilityByGuid.Count;
			}
			return 0;
		}

		// Token: 0x0600943C RID: 37948 RVA: 0x00355B34 File Offset: 0x00353D34
		public ActivatedAbilityEntry GetAbilityByCommand(string command)
		{
			if (this.AbilityByGuid != null)
			{
				foreach (KeyValuePair<Guid, ActivatedAbilityEntry> keyValuePair in this.AbilityByGuid)
				{
					if (keyValuePair.Value.Command == command)
					{
						return keyValuePair.Value;
					}
				}
			}
			return null;
		}

		// Token: 0x0600943D RID: 37949 RVA: 0x00355BAC File Offset: 0x00353DAC
		public ActivatedAbilityEntry GetAbility(Guid ID)
		{
			ActivatedAbilityEntry activatedAbilityEntry;
			if (this.AbilityByGuid != null && this.AbilityByGuid.TryGetValue(ID, out activatedAbilityEntry))
			{
				return activatedAbilityEntry;
			}
			return null;
		}

		// Token: 0x0600943E RID: 37950 RVA: 0x00355BD4 File Offset: 0x00353DD4
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			if (this.AbilityByGuid.IsNullOrEmpty<KeyValuePair<Guid, ActivatedAbilityEntry>>())
			{
				Writer.WriteOptimized(0);
			}
			else
			{
				Writer.WriteOptimized(this.AbilityByGuid.Count);
				foreach (KeyValuePair<Guid, ActivatedAbilityEntry> keyValuePair in this.AbilityByGuid)
				{
					Writer.WriteComposite<ActivatedAbilityEntry>(keyValuePair.Value);
				}
			}
			if (this.Cooldowns.IsNullOrEmpty<CommandCooldown>())
			{
				Writer.WriteOptimized(0);
				return;
			}
			Writer.WriteOptimized(this.Cooldowns.Count);
			foreach (CommandCooldown commandCooldown in this.Cooldowns)
			{
				Writer.WriteTokenized(commandCooldown);
			}
		}

		// Token: 0x0600943F RID: 37951 RVA: 0x00355CBC File Offset: 0x00353EBC
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			int num = Reader.ReadOptimizedInt32();
			this.AbilityByGuid = null;
			if (num > 0)
			{
				this.AbilityByGuid = new Dictionary<Guid, ActivatedAbilityEntry>(num);
				for (int i = 0; i < num; i++)
				{
					ActivatedAbilityEntry activatedAbilityEntry = Reader.ReadComposite<ActivatedAbilityEntry>();
					activatedAbilityEntry.Abilities = this;
					this.AbilityByGuid.Add(activatedAbilityEntry.ID, activatedAbilityEntry);
				}
			}
			num = Reader.ReadOptimizedInt32();
			if (num > 0)
			{
				this.Cooldowns = new List<CommandCooldown>(num);
				for (int j = 0; j < num; j++)
				{
					this.Cooldowns.Add((CommandCooldown)Reader.ReadTokenized());
				}
			}
		}

		// Token: 0x06009440 RID: 37952 RVA: 0x00355D4C File Offset: 0x00353F4C
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			ActivatedAbilities activatedAbilities = new ActivatedAbilities();
			activatedAbilities._ParentObject = Parent;
			if (this.AbilityByGuid != null)
			{
				activatedAbilities.AbilityByGuid = new Dictionary<Guid, ActivatedAbilityEntry>();
				foreach (Guid guid in this.AbilityByGuid.Keys)
				{
					ActivatedAbilityEntry activatedAbilityEntry = new ActivatedAbilityEntry(this.AbilityByGuid[guid]);
					activatedAbilityEntry.Abilities = activatedAbilities;
					activatedAbilities.AbilityByGuid.Add(guid, activatedAbilityEntry);
					if (activatedAbilityEntry.CommandCooldown.Segments > 0)
					{
						activatedAbilities.AddCooldown(activatedAbilityEntry.CommandCooldown);
					}
				}
			}
			return activatedAbilities;
		}

		// Token: 0x06009441 RID: 37953 RVA: 0x00355E00 File Offset: 0x00354000
		public void AddCooldown(CommandCooldown Cooldown)
		{
			if (this.Cooldowns == null)
			{
				this.Cooldowns = new List<CommandCooldown>();
			}
			this.Cooldowns.Add(Cooldown);
		}

		// Token: 0x06009442 RID: 37954 RVA: 0x00355E21 File Offset: 0x00354021
		public void RemoveCooldown(CommandCooldown Cooldown)
		{
			List<CommandCooldown> cooldowns = this.Cooldowns;
			if (cooldowns == null)
			{
				return;
			}
			cooldowns.Remove(Cooldown);
		}

		// Token: 0x06009443 RID: 37955 RVA: 0x00355E38 File Offset: 0x00354038
		public CommandCooldown FindCommandCooldown(string Command)
		{
			if (this.Cooldowns != null)
			{
				foreach (CommandCooldown commandCooldown in this.Cooldowns)
				{
					if (commandCooldown.Command == Command)
					{
						return commandCooldown;
					}
				}
			}
			return null;
		}

		// Token: 0x06009444 RID: 37956 RVA: 0x00355EA4 File Offset: 0x003540A4
		public void TickCooldowns(int Segments)
		{
			if (this.Cooldowns == null)
			{
				return;
			}
			for (int i = this.Cooldowns.Count - 1; i >= 0; i--)
			{
				CommandCooldown commandCooldown = this.Cooldowns[i];
				commandCooldown.Segments -= Segments;
				if (commandCooldown.Segments <= 0)
				{
					commandCooldown.Segments = 0;
					this.Cooldowns.RemoveAt(i);
					if (this.ParentObject.IsPlayer())
					{
						SoundManager.PlayUISound("sfx_cooldown_end", 0.25f, false, false, SoundRequest.SoundEffectType.None);
					}
				}
			}
		}

		// Token: 0x06009445 RID: 37957 RVA: 0x00355F28 File Offset: 0x00354128
		public void ClearCooldowns()
		{
			if (this.Cooldowns != null)
			{
				for (int i = this.Cooldowns.Count - 1; i >= 0; i--)
				{
					this.Cooldowns[i].Segments = 0;
					this.Cooldowns.RemoveAt(i);
				}
			}
			foreach (KeyValuePair<Guid, ActivatedAbilityEntry> keyValuePair in this.AbilityByGuid)
			{
				Guid guid;
				ActivatedAbilityEntry activatedAbilityEntry;
				keyValuePair.Deconstruct(out guid, out activatedAbilityEntry);
				activatedAbilityEntry.CommandCooldown.Segments = 0;
			}
		}

		// Token: 0x06009446 RID: 37958 RVA: 0x00355FCC File Offset: 0x003541CC
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009447 RID: 37959 RVA: 0x00355FCF File Offset: 0x003541CF
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009448 RID: 37960 RVA: 0x00355FD4 File Offset: 0x003541D4
		public string GetNextAvailableCommandString(string Base)
		{
			this.RecalculateAbilityByCommand();
			int num = 1;
			string text;
			using (TextBuilder textBuilder = TextBuilder.Get(Base.Length + 4).Append(Base).Append(":"))
			{
				int length = textBuilder.Length;
				while (ActivatedAbilities._abilityByCommand.ContainsKey(textBuilder.Append(num).AsSpan()))
				{
					num++;
					textBuilder.Length = length;
					if (num > 100)
					{
						MetricsManager.LogError("Next command string seems to be very far down the list: ${Base} ${count}");
						break;
					}
				}
				text = textBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06009449 RID: 37961 RVA: 0x00356070 File Offset: 0x00354270
		public Guid AddAbility(string Name, string Command, string Class, string Description = null, string Icon = "\a", string DisabledMessage = null, bool Toggleable = false, bool DefaultToggleState = false, bool ActiveToggle = false, bool IsAttack = false, bool IsRealityDistortionBased = false, bool IsWorldMapUsable = false, bool Silent = false, bool AIDisable = false, bool AlwaysAllowToggleOff = true, bool AffectedByWillpower = true, bool TickPerTurn = false, bool Distinct = false, int Cooldown = -1, string CommandForDescription = null, Renderable UITileDefault = null, Renderable UITileToggleOn = null, Renderable UITileDisabled = null, Renderable UITileCoolingDown = null)
		{
			if (Distinct)
			{
				ActivatedAbilityEntry abilityByCommand = this.GetAbilityByCommand(Command);
				if (abilityByCommand != null)
				{
					return abilityByCommand.ID;
				}
			}
			Guid guid = Guid.NewGuid();
			if (this.AbilityByGuid == null)
			{
				this.AbilityByGuid = new Dictionary<Guid, ActivatedAbilityEntry>();
			}
			ActivatedAbilityEntry activatedAbilityEntry = new ActivatedAbilityEntry();
			activatedAbilityEntry.Abilities = this;
			activatedAbilityEntry.ID = guid;
			activatedAbilityEntry.DisplayName = Name;
			activatedAbilityEntry.Command = Command;
			activatedAbilityEntry.CommandForDescription = CommandForDescription;
			activatedAbilityEntry.Class = Class;
			activatedAbilityEntry.Description = Description;
			activatedAbilityEntry.Icon = Icon;
			activatedAbilityEntry.DisabledMessage = DisabledMessage;
			activatedAbilityEntry.Toggleable = Toggleable;
			activatedAbilityEntry.ToggleState = DefaultToggleState;
			activatedAbilityEntry.ActiveToggle = ActiveToggle;
			activatedAbilityEntry.IsAttack = IsAttack;
			activatedAbilityEntry.IsRealityDistortionBased = IsRealityDistortionBased;
			activatedAbilityEntry.IsWorldMapUsable = IsWorldMapUsable;
			activatedAbilityEntry.AIDisable = AIDisable;
			activatedAbilityEntry.AlwaysAllowToggleOff = AlwaysAllowToggleOff;
			activatedAbilityEntry.AffectedByWillpower = AffectedByWillpower;
			activatedAbilityEntry.TickPerTurn = TickPerTurn;
			activatedAbilityEntry.UITileDefault = UITileDefault;
			activatedAbilityEntry.UITileToggleOn = UITileToggleOn;
			activatedAbilityEntry.UITileDisabled = UITileDisabled;
			activatedAbilityEntry.UITileCoolingDown = UITileCoolingDown;
			Templates.Template template;
			if (Description == null && Templates.TemplateByID.TryGetValue("ActivatedAbility." + activatedAbilityEntry.CommandForDescription, out template))
			{
				activatedAbilityEntry.Description = template.Build(null);
			}
			activatedAbilityEntry.CommandCooldown = this.FindCommandCooldown(Command);
			if (activatedAbilityEntry.CommandCooldown == null)
			{
				activatedAbilityEntry.CommandCooldown = new CommandCooldown();
				activatedAbilityEntry.CommandCooldown.Command = Command;
				if (Cooldown > 0)
				{
					activatedAbilityEntry.Cooldown = Cooldown;
				}
			}
			this.AbilityByGuid.Add(guid, activatedAbilityEntry);
			if (this.ParentObject.IsPlayer())
			{
				ActivatedAbilities.ClearCache();
				AbilityManager.UpdateFavorites();
				if (!Silent && !this.Silent)
				{
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					stringBuilder.Append("You have gained the activated ability {{Y|").Append(Name).Append("}}.");
					if (this.ParentObject.GetIntProperty("HasAccessedAbilities", 0) < 3 && CommandBindingManager.GetKeyFromCommand("CmdAbilities") == 65)
					{
						stringBuilder.Append("\n(press {{W|a}} to use activated abilities)");
					}
					Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			return guid;
		}

		// Token: 0x0600944A RID: 37962 RVA: 0x0035626C File Offset: 0x0035446C
		public bool RemoveAbility(Guid ID)
		{
			ActivatedAbilityEntry activatedAbilityEntry;
			return this.AbilityByGuid.TryGetValue(ID, out activatedAbilityEntry) && this.RemoveAbility(activatedAbilityEntry);
		}

		// Token: 0x0600944B RID: 37963 RVA: 0x00356294 File Offset: 0x00354494
		public bool RemoveAbilityByCommand(string Command)
		{
			foreach (KeyValuePair<Guid, ActivatedAbilityEntry> keyValuePair in this.AbilityByGuid)
			{
				ActivatedAbilityEntry value = keyValuePair.Value;
				if (value.Command == Command)
				{
					return this.RemoveAbility(value);
				}
			}
			return false;
		}

		// Token: 0x0600944C RID: 37964 RVA: 0x00356304 File Offset: 0x00354504
		public bool RemoveAbility(ActivatedAbilityEntry Entry)
		{
			if (!this.AbilityByGuid.Remove(Entry.ID))
			{
				return false;
			}
			if (this.ParentObject.IsPlayer())
			{
				ActivatedAbilities.ClearCache();
				AbilityManager.UpdateFavorites();
			}
			return true;
		}

		// Token: 0x0600944D RID: 37965 RVA: 0x00356333 File Offset: 0x00354533
		public IEnumerable<ActivatedAbilityEntry> YieldAbilitiesWithClass(string Class)
		{
			if (this.AbilityByGuid.IsNullOrEmpty<KeyValuePair<Guid, ActivatedAbilityEntry>>())
			{
				yield break;
			}
			foreach (KeyValuePair<Guid, ActivatedAbilityEntry> keyValuePair in this.AbilityByGuid)
			{
				Guid guid;
				ActivatedAbilityEntry activatedAbilityEntry;
				keyValuePair.Deconstruct(out guid, out activatedAbilityEntry);
				ActivatedAbilityEntry activatedAbilityEntry2 = activatedAbilityEntry;
				if (activatedAbilityEntry2.Class == Class)
				{
					yield return activatedAbilityEntry2;
				}
			}
			Dictionary<Guid, ActivatedAbilityEntry>.Enumerator enumerator = default(Dictionary<Guid, ActivatedAbilityEntry>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x0600944E RID: 37966 RVA: 0x0035634C File Offset: 0x0035454C
		// (set) Token: 0x0600944F RID: 37967 RVA: 0x003563B8 File Offset: 0x003545B8
		public static IEnumerable<string> PreferenceOrder
		{
			get
			{
				if (ActivatedAbilities._PreferenceOrder == null)
				{
					string option = Options.GetOption("AbilityListOrder", "");
					if (!string.IsNullOrEmpty(option))
					{
						ActivatedAbilities._PreferenceOrder = option.Split(",", StringSplitOptions.None).ToList<string>();
					}
					else
					{
						ActivatedAbilities._PreferenceOrder = new List<string> { "CommandToggleRunning", "CommandSurvivalCamp" };
					}
				}
				return ActivatedAbilities._PreferenceOrder;
			}
			set
			{
				List<string> list = new List<string>(value);
				HashSet<string> hashSet = new HashSet<string>(list);
				int i = 0;
				for (int j = 0; j < ActivatedAbilities._PreferenceOrder.Count; j++)
				{
					if (hashSet.Contains(ActivatedAbilities._PreferenceOrder[j]))
					{
						hashSet.Remove(ActivatedAbilities._PreferenceOrder[j]);
						ActivatedAbilities._PreferenceOrder[j] = list[i++];
					}
				}
				while (i < list.Count)
				{
					ActivatedAbilities._PreferenceOrder.Add(list[i++]);
				}
				string text = ActivatedAbilities._PreferenceOrder.Where<string>(delegate(string cmd)
				{
					Guid guid;
					return !Guid.TryParse(cmd, out guid);
				}).Distinct<string>().Aggregate("", delegate(string a, string b)
				{
					if (!string.IsNullOrEmpty(a))
					{
						return a + "," + b;
					}
					return b;
				});
				Options.SetOption("AbilityListOrder", text);
				ActivatedAbilities.ClearCache();
			}
		}

		// Token: 0x06009450 RID: 37968 RVA: 0x003564B4 File Offset: 0x003546B4
		private static void ClearCache()
		{
			GameObject player = The.Player;
			if (player == null)
			{
				return;
			}
			ActivatedAbilities part = player.GetPart<ActivatedAbilities>();
			if (part == null)
			{
				return;
			}
			List<ActivatedAbilityEntry> abilityListOrderedByPreferenceCache = part._AbilityListOrderedByPreferenceCache;
			if (abilityListOrderedByPreferenceCache == null)
			{
				return;
			}
			abilityListOrderedByPreferenceCache.Clear();
		}

		// Token: 0x06009451 RID: 37969 RVA: 0x003564D9 File Offset: 0x003546D9
		public IEnumerable<ActivatedAbilityEntry> GetAbilityListOrderedByPreference()
		{
			if (this._AbilityListOrderedByPreferenceCache == null)
			{
				this._AbilityListOrderedByPreferenceCache = new List<ActivatedAbilityEntry>(32);
			}
			if (this._AbilityListOrderedByPreferenceCache.Count == 0)
			{
				this._AbilityListOrderedByPreferenceCache.AddRange(this._GetAbilityListOrderedByPreference());
			}
			return this._AbilityListOrderedByPreferenceCache;
		}

		// Token: 0x06009452 RID: 37970 RVA: 0x00356514 File Offset: 0x00354714
		private void RecalculateAbilityByCommand()
		{
			if (ActivatedAbilities._abilityByCommand == null)
			{
				ActivatedAbilities._abilityByCommand = new StringMap<ActivatedAbilityEntry>();
			}
			StringMap<ActivatedAbilityEntry> abilityByCommand = ActivatedAbilities._abilityByCommand;
			if (abilityByCommand != null)
			{
				abilityByCommand.Clear();
			}
			if (this.AbilityByGuid != null)
			{
				foreach (ActivatedAbilityEntry activatedAbilityEntry in this.AbilityByGuid.Values)
				{
					if (activatedAbilityEntry != null)
					{
						StringMap<ActivatedAbilityEntry> abilityByCommand2 = ActivatedAbilities._abilityByCommand;
						if (abilityByCommand2 != null)
						{
							abilityByCommand2.TryAdd(activatedAbilityEntry.Command, activatedAbilityEntry);
						}
					}
				}
			}
		}

		// Token: 0x06009453 RID: 37971 RVA: 0x003565AC File Offset: 0x003547AC
		private IEnumerable<ActivatedAbilityEntry> _GetAbilityListOrderedByPreference()
		{
			if (this.AbilityByGuid == null)
			{
				yield break;
			}
			this.RecalculateAbilityByCommand();
			HashSet<ActivatedAbilityEntry> used = new HashSet<ActivatedAbilityEntry>();
			foreach (string text in ActivatedAbilities.PreferenceOrder)
			{
				ActivatedAbilityEntry activatedAbilityEntry;
				if (ActivatedAbilities._abilityByCommand.TryGetValue(text, out activatedAbilityEntry) && !used.Contains(activatedAbilityEntry))
				{
					used.Add(activatedAbilityEntry);
					yield return activatedAbilityEntry;
				}
			}
			IEnumerator<string> enumerator = null;
			if (used.Count<ActivatedAbilityEntry>() != ActivatedAbilities._abilityByCommand.Count<KeyValuePair<string, ActivatedAbilityEntry>>())
			{
				foreach (ActivatedAbilityEntry activatedAbilityEntry2 in this.AbilityByGuid.Values)
				{
					if (!used.Contains(activatedAbilityEntry2))
					{
						ActivatedAbilities._PreferenceOrder.Add(activatedAbilityEntry2.Command);
						yield return activatedAbilityEntry2;
					}
				}
				Dictionary<Guid, ActivatedAbilityEntry>.ValueCollection.Enumerator enumerator2 = default(Dictionary<Guid, ActivatedAbilityEntry>.ValueCollection.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x04003D47 RID: 15687
		public const string DEFAULT_ICON = "\a";

		// Token: 0x04003D48 RID: 15688
		[NonSerialized]
		public Dictionary<Guid, ActivatedAbilityEntry> AbilityByGuid;

		// Token: 0x04003D49 RID: 15689
		[NonSerialized]
		public List<CommandCooldown> Cooldowns;

		// Token: 0x04003D4A RID: 15690
		[NonSerialized]
		public bool Silent;

		// Token: 0x04003D4B RID: 15691
		[NonSerialized]
		private static List<string> _PreferenceOrder = null;

		// Token: 0x04003D4C RID: 15692
		private static StringMap<ActivatedAbilityEntry> _abilityByCommand = new StringMap<ActivatedAbilityEntry>();

		// Token: 0x04003D4D RID: 15693
		[NonSerialized]
		private List<ActivatedAbilityEntry> _AbilityListOrderedByPreferenceCache;

		// Token: 0x02001C68 RID: 7272
		[HasModSensitiveStaticCache]
		public static class XmlData
		{
			// Token: 0x0600F11B RID: 61723 RVA: 0x0054B63C File Offset: 0x0054983C
			public static ActivatedAbilities.XmlData.Data DataForCommand(string command)
			{
				ActivatedAbilities.XmlData.Data data;
				if (!ActivatedAbilities.XmlData.DataByCommand.TryGetValue(command, out data))
				{
					return new ActivatedAbilities.XmlData.Data
					{
						Command = command
					};
				}
				return data;
			}

			// Token: 0x0600F11C RID: 61724 RVA: 0x0054B668 File Offset: 0x00549868
			[ModSensitiveCacheInit]
			public static void Init()
			{
				foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("activatedabilities", true, true))
				{
					ActivatedAbilities.XmlData.HandleNodes(xmlDataHelper);
				}
			}

			// Token: 0x0600F11D RID: 61725 RVA: 0x0054B6B8 File Offset: 0x005498B8
			public static void HandleNodes(XmlDataHelper xml)
			{
				xml.HandleNodes(ActivatedAbilities.XmlData.XmlNodes, null);
			}

			// Token: 0x0600F11E RID: 61726 RVA: 0x0054B6C8 File Offset: 0x005498C8
			public static void HandleAbilityNode(XmlDataHelper xml)
			{
				string text = xml.ParseAttribute<string>("Command", null, true, null);
				ActivatedAbilities.XmlData.Data data;
				if (!ActivatedAbilities.XmlData.DataByCommand.TryGetValue(text, out data))
				{
					data = new ActivatedAbilities.XmlData.Data
					{
						Command = text
					};
					ActivatedAbilities.XmlData.DataByCommand.Add(text, data);
				}
				ActivatedAbilities.XmlData.currentData = data;
				xml.HandleNodes(ActivatedAbilities.XmlData.AbilityNodes, null);
			}

			// Token: 0x0600F11F RID: 61727 RVA: 0x0054B71E File Offset: 0x0054991E
			public static void HandleDescriptionNode(XmlDataHelper xml)
			{
				Templates.LoadTemplateFromExternal("ActivatedAbility." + ActivatedAbilities.XmlData.currentData.Command, xml);
			}

			// Token: 0x0600F120 RID: 61728 RVA: 0x0054B73C File Offset: 0x0054993C
			public static void HandleTagNode(XmlDataHelper xml)
			{
				string text = xml.ParseAttribute<string>("Name", "", false, null);
				string text2 = xml.ParseAttribute<string>("Value", "", false, null);
				if (!xml.IsEmptyElement)
				{
					xml.Read();
					string value = xml.Value;
					if (text.IsNullOrEmpty())
					{
						text = value;
					}
					else if (text2.IsNullOrEmpty())
					{
						text2 = value;
					}
					xml.Read();
				}
				ActivatedAbilities.XmlData.currentData.TryAddTag(text, text2);
			}

			// Token: 0x0600F121 RID: 61729 RVA: 0x0054B7B0 File Offset: 0x005499B0
			public static ActivatedAbilities.XmlData.UITileStates ParseTileState(string state)
			{
				ActivatedAbilities.XmlData.UITileStates uitileStates;
				if (!(state == "Default"))
				{
					if (!(state == "CoolingDown"))
					{
						if (!(state == "ToggleOn"))
						{
							if (!(state == "Disabled"))
							{
								throw new Exception("Unable to parse UITile State \"" + state + "\"");
							}
							uitileStates = ActivatedAbilities.XmlData.UITileStates.Disabled;
						}
						else
						{
							uitileStates = ActivatedAbilities.XmlData.UITileStates.ToggleOn;
						}
					}
					else
					{
						uitileStates = ActivatedAbilities.XmlData.UITileStates.CoolingDown;
					}
				}
				else
				{
					uitileStates = ActivatedAbilities.XmlData.UITileStates.Default;
				}
				return uitileStates;
			}

			// Token: 0x0600F122 RID: 61730 RVA: 0x0054B81C File Offset: 0x00549A1C
			public static void HandleUITileNode(XmlDataHelper xml)
			{
				ActivatedAbilities.XmlData.UITileStates uitileStates = xml.ParseAttribute<ActivatedAbilities.XmlData.UITileStates>("State", ActivatedAbilities.XmlData.UITileStates.Default, false, new XmlDataHelper.AttributeParser<ActivatedAbilities.XmlData.UITileStates>.ParseDelegate(ActivatedAbilities.XmlData.ParseTileState));
				Renderable renderable = Renderable.UITile(xml.ParseAttribute<string>("Tile", "", true, null), xml.ParseAttribute<char>("Foreground", 'w', false, null), xml.ParseAttribute<char>("Detail", 'W', false, null), " ", '\0');
				ActivatedAbilities.XmlData.currentData.UITiles[uitileStates] = renderable;
			}

			// Token: 0x0400700B RID: 28683
			[ModSensitiveStaticCache(true)]
			public static Dictionary<string, ActivatedAbilities.XmlData.Data> DataByCommand = new Dictionary<string, ActivatedAbilities.XmlData.Data>();

			// Token: 0x0400700C RID: 28684
			public static Dictionary<string, Action<XmlDataHelper>> XmlNodes = new Dictionary<string, Action<XmlDataHelper>>
			{
				{
					"activatedabilities",
					new Action<XmlDataHelper>(ActivatedAbilities.XmlData.HandleNodes)
				},
				{
					"ability",
					new Action<XmlDataHelper>(ActivatedAbilities.XmlData.HandleAbilityNode)
				}
			};

			// Token: 0x0400700D RID: 28685
			public static ActivatedAbilities.XmlData.Data currentData;

			// Token: 0x0400700E RID: 28686
			public static Dictionary<string, Action<XmlDataHelper>> AbilityNodes = new Dictionary<string, Action<XmlDataHelper>>
			{
				{
					"description",
					new Action<XmlDataHelper>(ActivatedAbilities.XmlData.HandleDescriptionNode)
				},
				{
					"tag",
					new Action<XmlDataHelper>(ActivatedAbilities.XmlData.HandleTagNode)
				},
				{
					"UITile",
					new Action<XmlDataHelper>(ActivatedAbilities.XmlData.HandleUITileNode)
				}
			};

			// Token: 0x02001E94 RID: 7828
			public enum UITileStates
			{
				// Token: 0x040076BE RID: 30398
				Default,
				// Token: 0x040076BF RID: 30399
				ToggleOn,
				// Token: 0x040076C0 RID: 30400
				CoolingDown,
				// Token: 0x040076C1 RID: 30401
				Disabled
			}

			// Token: 0x02001E95 RID: 7829
			public class Data
			{
				// Token: 0x17001053 RID: 4179
				// (get) Token: 0x0600F7DB RID: 63451 RVA: 0x0055D454 File Offset: 0x0055B654
				public Templates.Template Description
				{
					get
					{
						Templates.Template template;
						if (!Templates.TemplateByID.TryGetValue("ActivatedAbility." + this.Command, out template))
						{
							return null;
						}
						return template;
					}
				}

				// Token: 0x0600F7DC RID: 63452 RVA: 0x0055D482 File Offset: 0x0055B682
				public bool TryGetTag(string Name, out string Value)
				{
					if (this.Tags == null)
					{
						Value = null;
						return false;
					}
					return this.Tags.TryGetValue(Name, out Value);
				}

				// Token: 0x0600F7DD RID: 63453 RVA: 0x0055D49E File Offset: 0x0055B69E
				public bool TryAddTag(string Name, string Value)
				{
					if (this.Tags == null)
					{
						this.Tags = new Dictionary<string, string>();
					}
					return this.Tags.TryAdd(Name, Value);
				}

				// Token: 0x0600F7DE RID: 63454 RVA: 0x0055D4C0 File Offset: 0x0055B6C0
				public bool HasTag(string Name)
				{
					return this.Tags != null && this.Tags.ContainsKey(Name);
				}

				// Token: 0x040076C2 RID: 30402
				public string Command;

				// Token: 0x040076C3 RID: 30403
				public Dictionary<ActivatedAbilities.XmlData.UITileStates, Renderable> UITiles = new Dictionary<ActivatedAbilities.XmlData.UITileStates, Renderable>();

				// Token: 0x040076C4 RID: 30404
				public Dictionary<string, string> Tags;
			}
		}
	}
}
