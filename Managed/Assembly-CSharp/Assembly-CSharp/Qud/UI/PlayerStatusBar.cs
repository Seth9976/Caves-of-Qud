using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using Kobold;
using UnityEngine;
using UnityEngine.UI;
using XRL;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace Qud.UI
{
	// Token: 0x020004A1 RID: 1185
	[ExecuteAlways]
	[UIView("PlayerStatusBar", false, false, false, null, null, false, 0, false, NavCategory = "Adventure", UICanvas = "PlayerStatusBar", UICanvasHost = 1)]
	public class PlayerStatusBar : SingletonWindowBase<PlayerStatusBar>
	{
		// Token: 0x06002D62 RID: 11618 RVA: 0x000F5711 File Offset: 0x000F3911
		public void Start()
		{
			this.playerChar = new ConsoleChar();
			this.lastPlayerChar = new ConsoleChar();
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000F572C File Offset: 0x000F392C
		private void UpdateStat(string statName, Statistic stat, global::XRL.World.GameObject Player)
		{
			string text = stat.GetDisplayValue();
			if (stat.Name == "DV")
			{
				text = Stats.GetCombatDV(Player).ToStringCached();
			}
			else if (stat.Name == "MA")
			{
				text = Stats.GetCombatMA(Player).ToStringCached();
			}
			string text2;
			if (this.lastPlayerStats.TryGetValue(statName, out text2) && text2 == text)
			{
				return;
			}
			Dictionary<string, string> dictionary = this.playerStats;
			lock (dictionary)
			{
				this.lastPlayerStats[statName] = (this.playerStats[statName] = text);
				this.playerStatsDirty = true;
			}
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000F57EC File Offset: 0x000F39EC
		private void UpdateBool(PlayerStatusBar.BoolDataType dataType, bool value)
		{
			bool flag;
			if (this.lastBoolData.TryGetValue(dataType, out flag) && flag == value)
			{
				return;
			}
			object obj = this.boolLock;
			lock (obj)
			{
				this.boolData[dataType] = value;
				this.lastBoolData[dataType] = value;
				this.boolDataDirty = true;
			}
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000F585C File Offset: 0x000F3A5C
		private bool Compare(StringBuilder s1, string s2)
		{
			if (s1.Length != s2.Length)
			{
				return false;
			}
			for (int i = 0; i < s1.Length; i++)
			{
				if (s1[i] != s2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000F58A0 File Offset: 0x000F3AA0
		private void UpdateString(PlayerStatusBar.StringDataType type, StringBuilder data, bool toRTF = false)
		{
			string text;
			if (this.lastPlayerStringData.TryGetValue(type, out text) && this.Compare(data, text))
			{
				return;
			}
			Dictionary<PlayerStatusBar.StringDataType, string> dictionary = this.playerStringData;
			lock (dictionary)
			{
				this.lastPlayerStringData[type] = data.ToString();
				this.playerStringData[type] = data.ToString();
				this.playerStringsDirty = true;
				string text2 = this.lastPlayerStringData[type];
				if (((text2 != null) ? text2.Length : 0) != data.ToString().Length)
				{
					this.playerStringsSizeDirty = true;
				}
			}
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000F594C File Offset: 0x000F3B4C
		private void UpdateString(PlayerStatusBar.StringDataType type, string data, bool toRTF = false)
		{
			string text;
			if (this.lastPlayerStringData.TryGetValue(type, out text) && text == data)
			{
				return;
			}
			Dictionary<PlayerStatusBar.StringDataType, string> dictionary = this.playerStringData;
			lock (dictionary)
			{
				this.lastPlayerStringData[type] = data;
				this.playerStringData[type] = data;
				this.playerStringsDirty = true;
				string text2 = this.lastPlayerStringData[type];
				if (((text2 != null) ? text2.Length : 0) != data.Length)
				{
					this.playerStringsSizeDirty = true;
				}
			}
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000F59E8 File Offset: 0x000F3BE8
		public override void Init()
		{
			XRLCore.RegisterAfterRenderCallback(new Action<XRLCore, ScreenBuffer>(this.AfterRender));
			XRLCore.RegisterOnBeginPlayerTurnCallback(new Action<XRLCore>(this.BeginEndTurn));
			XRLCore.RegisterOnEndPlayerTurnCallback(new Action<XRLCore>(this.BeginEndTurn), true);
			XRLCore.RegisterOnPassedTenPlayerTurnCallback(new Action<XRLCore>(this.BeginEndTurn));
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000F5A3C File Offset: 0x000F3C3C
		private void AfterRender(XRLCore core, ScreenBuffer buffer)
		{
			global::XRL.World.GameObject player = The.Player;
			if (player != null && player.Render.Visible)
			{
				Cell currentCell = player.GetCurrentCell();
				if (currentCell != null)
				{
					ConsoleChar consoleChar = buffer[currentCell.X, currentCell.Y];
					if (consoleChar != this.lastPlayerChar && this.playerChar != null)
					{
						ConsoleChar consoleChar2 = this.playerChar;
						lock (consoleChar2)
						{
							this.playerChar.Copy(consoleChar);
							this.lastPlayerChar.Copy(consoleChar);
							this.playerCharDirty = true;
						}
					}
				}
			}
			this.BeginEndTurn(core);
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000F5AF0 File Offset: 0x000F3CF0
		public void Awake()
		{
			this.LastWidth = 0f;
			this.RelayoutTop = true;
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000F5B04 File Offset: 0x000F3D04
		private void BeginEndTurn(XRLCore core)
		{
			global::XRL.World.GameObject player = The.Player;
			if (player != null)
			{
				this.UpdateBool(PlayerStatusBar.BoolDataType.AutoExploreActive, AutoAct.IsAnyExploration());
				Cell currentCell = player.GetCurrentCell();
				if (currentCell != null && currentCell.ParentZone != null)
				{
					this.UpdateString(PlayerStatusBar.StringDataType.Zone, currentCell.ParentZone.DisplayName, false);
					this.UpdateString(PlayerStatusBar.StringDataType.ZoneOnly, currentCell.ParentZone.DisplayName, false);
				}
				foreach (KeyValuePair<string, Statistic> keyValuePair in player.Statistics)
				{
					this.UpdateStat(keyValuePair.Key, keyValuePair.Value, player);
				}
				this.UpdateString(PlayerStatusBar.StringDataType.PlayerName, player.DisplayNameOnlyDirect, false);
				Stomach part = player.GetPart<Stomach>();
				if (part != null)
				{
					this.sb.Length = 0;
					this.sb.Append(part.FoodStatus()).Append(" ").Append(part.WaterStatus());
					this.UpdateString(PlayerStatusBar.StringDataType.FoodWater, this.sb, false);
				}
				bool inventory = player.Inventory != null;
				Body body = player.Body;
				if (inventory && body != null)
				{
					int carriedWeight = player.GetCarriedWeight();
					int maxCarriedWeight = player.GetMaxCarriedWeight();
					int freeDrams = player.GetFreeDrams("water", null, null, null, false);
					this.sb.Length = 0;
					this.sb.Append(carriedWeight).Append("/").Append(maxCarriedWeight.ToStringCached())
						.Append("# {{blue|")
						.Append(freeDrams.ToStringCached())
						.Append("$}}");
					this.UpdateString(PlayerStatusBar.StringDataType.Weight, this.sb, false);
				}
				this.sb.Length = 0;
				this.sb.Append("T:").Append(player.Physics.Temperature.ToStringCached()).Append('ø');
				this.UpdateString(PlayerStatusBar.StringDataType.Temp, this.sb, false);
				this.sb.Length = 0;
				this.sb.Append(Calendar.GetTime()).Append(" ").Append(Calendar.GetDay())
					.Append(" of ")
					.Append(Calendar.GetMonth());
				this.UpdateString(PlayerStatusBar.StringDataType.Time, this.sb, false);
				int num = -1;
				Zone currentZone = player.GetCurrentZone();
				string text = ((currentZone != null) ? currentZone.ResolveZoneWorld() : null);
				if (text == "JoppaWorld")
				{
					int num2 = Calendar.CurrentDaySegment / 10;
					num2 += 875;
					num2 %= 1200;
					int num3 = 675;
					if (num2 < num3)
					{
						num = num2 * 7 / num3;
					}
					else
					{
						num2 -= num3;
						num3 = 525;
						num = 7 + num2 * 3 / num3;
					}
				}
				string text2;
				if (text == null)
				{
					text2 = null;
				}
				else
				{
					WorldFactory factory = WorldFactory.Factory;
					if (factory == null)
					{
						text2 = null;
					}
					else
					{
						WorldBlueprint world = factory.getWorld(text);
						text2 = ((world != null) ? world.CustomClock : null);
					}
				}
				this.customClock = text2;
				if (num != this.lastTimeOfDayImage || this.customClock != this.lastCustomClock)
				{
					object obj = this.timeLock;
					lock (obj)
					{
						this.lastCustomClock = this.customClock;
						this.lastTimeOfDayImage = (this.timeOfDayImage = num);
						this.timeOfDayImageDirty = true;
					}
				}
				if (player.GetIntProperty("Analgesia", 0) > 0)
				{
					this.sb.Length = 0;
					this.sb.Append("HP: ").Append(Strings.WoundLevel(player));
					this.UpdateString(PlayerStatusBar.StringDataType.HPBar, this.sb, false);
					int num4 = Math.Max(0, Strings.WoundLevelN(player));
					if (this.hpbarValue.Value == num4 && this.hpbarValue.Max == 5)
					{
						return;
					}
					object obj = this.barLock;
					lock (obj)
					{
						this.hpbarValue.Min = 0;
						this.hpbarValue.Value = num4;
						this.hpbarValue.Max = 5;
						this.barValueDirty = true;
						return;
					}
				}
				this.sb.Length = 0;
				this.sb.Append("{{Y|HP: {{").Append(Strings.HealthStatusColor(player)).Append("|")
					.Append(player.hitpoints.ToStringCached())
					.Append("}} / ")
					.Append(player.baseHitpoints.ToStringCached())
					.Append("}}");
				this.UpdateString(PlayerStatusBar.StringDataType.HPBar, this.sb, false);
				if (this.hpbarValue.Value != player.hitpoints || this.hpbarValue.Max != player.baseHitpoints)
				{
					object obj = this.barLock;
					lock (obj)
					{
						this.hpbarValue.Value = player.hitpoints;
						this.hpbarValue.Max = player.baseHitpoints;
						this.barValueDirty = true;
					}
				}
			}
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000F6018 File Offset: 0x000F4218
		public override bool AllowPassthroughInput()
		{
			return true;
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000F601C File Offset: 0x000F421C
		public void Update()
		{
			if (this.playerCharDirty)
			{
				ConsoleChar consoleChar = this.playerChar;
				lock (consoleChar)
				{
					UIThreeColorProperties playerBody3c = this.PlayerBody3c;
					global::XRL.World.GameObject player = The.Player;
					playerBody3c.FromRenderable((player != null) ? player.RenderForUI("PlayerStatus", false) : null, true);
					if (this.PlayerBody3c.Background == The.Color.DarkBlack)
					{
						this.PlayerBody3c.Background = Color.clear;
					}
					this.playerCharDirty = false;
				}
			}
			if (this.playerStatsDirty)
			{
				Dictionary<string, string> dictionary = this.playerStats;
				lock (dictionary)
				{
					this.TopLeftSecondaryStatBlock.GetComponent<StatusBarStatBlock>().UpdateStats(this.playerStats);
					this.XPBar.text.SetText(string.Format("LVL: {0} Exp: {1} / {2}", this.playerStats["Level"], this.playerStats["XP"], Leveler.GetXPForLevel(Convert.ToInt32(this.playerStats["Level"]) + 1)));
					this.XPBar.BarStart = Leveler.GetXPForLevel(Convert.ToInt32(this.playerStats["Level"]));
					this.XPBar.BarEnd = Leveler.GetXPForLevel(Convert.ToInt32(this.playerStats["Level"]) + 1);
					this.XPBar.BarValue = Convert.ToInt32(this.playerStats["XP"]);
					this.XPBar.UpdateBar();
					this.playerStatsDirty = false;
				}
			}
			if (this.timeOfDayImageDirty)
			{
				object obj = this.timeLock;
				lock (obj)
				{
					if (!this.customClock.IsNullOrEmpty())
					{
						Sprite unitySprite;
						if (!this.customClocks.TryGetValue(this.customClock, out unitySprite))
						{
							unitySprite = SpriteManager.GetUnitySprite(this.customClock);
						}
						this.TimeClockImage.sprite = unitySprite;
					}
					else if (this.timeOfDayImage >= 0)
					{
						this.TimeClockImage.sprite = this.QudTimeImages[this.timeOfDayImage];
					}
					else
					{
						this.TimeClockImage.sprite = null;
					}
					this.timeOfDayImageDirty = false;
				}
			}
			if (Application.isPlaying)
			{
				this.UpdateBool(PlayerStatusBar.BoolDataType.ObjectFinderActive, UIManager.getWindow("NearbyItems").Visible);
				this.UpdateBool(PlayerStatusBar.BoolDataType.MinimapActive, UIManager.getWindow("Minimap").Visible);
				this.UpdateBool(PlayerStatusBar.BoolDataType.WindowsPinned, UIManager.WindowFramePin == 0);
			}
			if (this.boolDataDirty)
			{
				object obj = this.boolLock;
				lock (obj)
				{
					this.ExploreButton.IsActive = this.boolData[PlayerStatusBar.BoolDataType.AutoExploreActive];
					this.FindButton.IsActive = this.boolData[PlayerStatusBar.BoolDataType.ObjectFinderActive];
					this.MinimapButton.IsActive = this.boolData[PlayerStatusBar.BoolDataType.MinimapActive];
					this.WindowPinButton.IsActive = this.boolData[PlayerStatusBar.BoolDataType.WindowsPinned];
					this.boolDataDirty = false;
				}
			}
			if (this.playerStringsDirty)
			{
				Dictionary<PlayerStatusBar.StringDataType, string> dictionary2 = this.playerStringData;
				lock (dictionary2)
				{
					if (this.FoodStatusText.text != this.playerStringData[PlayerStatusBar.StringDataType.FoodWater])
					{
						this.FoodStatusText.SetText(this.playerStringData[PlayerStatusBar.StringDataType.FoodWater]);
					}
					UITextSkin component = this.TimeText.GetComponent<UITextSkin>();
					if (this.playerStringData.ContainsKey(PlayerStatusBar.StringDataType.Time))
					{
						if (component.text != this.playerStringData[PlayerStatusBar.StringDataType.Time])
						{
							component.SetText(this.playerStringData[PlayerStatusBar.StringDataType.Time]);
						}
						this.TimeText.GetComponent<LayoutElement>().preferredWidth = Math.Min(350f, component.preferredWidth + 5f);
					}
					if (this.playerStringData.ContainsKey(PlayerStatusBar.StringDataType.Temp) && this.TempText.text != this.playerStringData[PlayerStatusBar.StringDataType.Temp])
					{
						this.TempText.SetText(this.playerStringData[PlayerStatusBar.StringDataType.Temp]);
					}
					if (this.playerStringData.ContainsKey(PlayerStatusBar.StringDataType.Weight) && this.WeightText.text != this.playerStringData[PlayerStatusBar.StringDataType.Weight])
					{
						this.WeightText.SetText(this.playerStringData[PlayerStatusBar.StringDataType.Weight]);
					}
					if (this.playerStringData.ContainsKey(PlayerStatusBar.StringDataType.Zone) && this.ZoneText.text != this.playerStringData[PlayerStatusBar.StringDataType.Zone])
					{
						this.ZoneText.SetText(this.playerStringData[PlayerStatusBar.StringDataType.Zone]);
						UITextSkin minimapLabel = GameManager.Instance.minimapLabel;
						if (minimapLabel != null)
						{
							minimapLabel.SetText(" " + this.playerStringData[PlayerStatusBar.StringDataType.ZoneOnly]);
						}
					}
					if (this.playerStringData.ContainsKey(PlayerStatusBar.StringDataType.HPBar) && this.HPBar.text.text != this.playerStringData[PlayerStatusBar.StringDataType.HPBar])
					{
						this.HPBar.text.SetText(this.playerStringData[PlayerStatusBar.StringDataType.HPBar]);
					}
					if (this.playerStringData.ContainsKey(PlayerStatusBar.StringDataType.PlayerName) && this.PlayerNameText.text != this.playerStringData[PlayerStatusBar.StringDataType.PlayerName])
					{
						this.PlayerNameText.SetText(this.playerStringData[PlayerStatusBar.StringDataType.PlayerName]);
					}
					this.playerStringsDirty = false;
				}
			}
			if (this.playerStringsSizeDirty)
			{
				Dictionary<PlayerStatusBar.StringDataType, string> dictionary2 = this.playerStringData;
				lock (dictionary2)
				{
					this.RelayoutTop = true;
					this.playerStringsSizeDirty = false;
				}
			}
			if (this.barValueDirty)
			{
				this.barValueDirty = false;
				this.hpbarValue.SetBar(this.HPBar);
			}
			this.HandleSizeUpdate();
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000F667C File Offset: 0x000F487C
		public void HandleSizeUpdate()
		{
			if (this.RelayoutTop)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.topRectTransform);
			}
			if (this.LastWidth != base.rectTransform.rect.width)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.bottomRectTransform);
			}
			if (this.LastWidth != base.rectTransform.rect.width || this.RelayoutTop)
			{
				if (!this.Spacers[0].activeSelf)
				{
					this.Spacers[0].SetActive(true);
				}
				if (!this.PlayerNameText.gameObject.activeSelf)
				{
					this.PlayerNameText.gameObject.SetActive(true);
				}
				if (!this.Spacers[1].activeSelf)
				{
					this.Spacers[1].SetActive(true);
				}
				if (!this.TimeText.gameObject.activeSelf)
				{
					this.TimeText.gameObject.SetActive(true);
				}
				if (!this.Spacers[2].activeSelf)
				{
					this.Spacers[2].SetActive(true);
				}
				if (!this.TopLeftSecondaryStatBlock.gameObject.activeSelf)
				{
					this.TopLeftSecondaryStatBlock.gameObject.SetActive(true);
				}
				if (!this.Spacers[3].activeSelf)
				{
					this.Spacers[3].SetActive(true);
				}
				if (!this.ZoneText.gameObject.activeSelf)
				{
					this.ZoneText.gameObject.SetActive(true);
				}
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.topRectTransform);
				if (this.Spacers[0].activeInHierarchy && this.Spacers[0].GetComponent<RectTransform>().sizeDelta.x < 15f)
				{
					this.Spacers[0].SetActive(false);
					this.PlayerNameText.gameObject.SetActive(false);
					LayoutRebuilder.ForceRebuildLayoutImmediate(this.topRectTransform);
				}
				if (this.Spacers[2].activeInHierarchy && this.Spacers[2].GetComponent<RectTransform>().sizeDelta.x < 15f)
				{
					this.Spacers[2].SetActive(false);
					this.TimeText.gameObject.SetActive(false);
					LayoutRebuilder.ForceRebuildLayoutImmediate(this.topRectTransform);
				}
				if (this.Spacers[3].activeInHierarchy && this.Spacers[1].activeInHierarchy && this.Spacers[1].GetComponent<RectTransform>().sizeDelta.x < 15f)
				{
					this.Spacers[3].SetActive(false);
					this.ZoneText.gameObject.SetActive(false);
					LayoutRebuilder.ForceRebuildLayoutImmediate(this.topRectTransform);
				}
				if (this.Spacers[1].activeInHierarchy && this.Spacers[1].GetComponent<RectTransform>().sizeDelta.x < 15f)
				{
					this.Spacers[1].SetActive(false);
					this.TopLeftSecondaryStatBlock.gameObject.SetActive(false);
					LayoutRebuilder.ForceRebuildLayoutImmediate(this.topRectTransform);
				}
			}
			this.LastWidth = base.rectTransform.rect.width;
			this.RelayoutTop = false;
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x000F69D4 File Offset: 0x000F4BD4
		public void ClickButton(string button)
		{
			if (button == "Explore")
			{
				Keyboard.PushMouseEvent("Command:CmdAutoExplore", null);
				return;
			}
			if (button == "Up")
			{
				Keyboard.PushMouseEvent("Command:CmdMoveU", null);
				return;
			}
			if (button == "Down")
			{
				Keyboard.PushMouseEvent("Command:CmdMoveD", null);
				return;
			}
			if (button == "Char")
			{
				Keyboard.PushMouseEvent("Command:CmdCharacter", null);
				return;
			}
			if (button == "Rest")
			{
				Keyboard.PushMouseEvent("Command:CmdWaitMenu", null);
				return;
			}
			if (button == "Look")
			{
				Keyboard.PushMouseEvent("Command:CmdLook", null);
				return;
			}
			if (button == "SystemMenu")
			{
				Keyboard.PushMouseEvent("Command:CmdSystemMenu", null);
				return;
			}
			if (button == "POI")
			{
				Keyboard.PushMouseEvent("Command:CmdMoveToPointOfInterest", null);
				return;
			}
			if (button == "Finder")
			{
				UIManager.getWindow<NearbyItemsWindow>("NearbyItems").TogglePreferredState();
				return;
			}
			if (button == "Minimap")
			{
				UIManager.getWindow<MinimapWindow>("Minimap").TogglePreferredState();
				return;
			}
			if (button == "WindowLock")
			{
				if (UIManager.WindowFramePin == 1)
				{
					UIManager.WindowFramePin = 0;
					return;
				}
				UIManager.WindowFramePin = 1;
			}
		}

		// Token: 0x04001979 RID: 6521
		public UIThreeColorProperties PlayerBody3c;

		// Token: 0x0400197A RID: 6522
		public global::UnityEngine.GameObject TopLeftSecondaryStatBlock;

		// Token: 0x0400197B RID: 6523
		public global::UnityEngine.GameObject TimeText;

		// Token: 0x0400197C RID: 6524
		public UITextSkin FoodStatusText;

		// Token: 0x0400197D RID: 6525
		public UITextSkin TempText;

		// Token: 0x0400197E RID: 6526
		public UITextSkin WeightText;

		// Token: 0x0400197F RID: 6527
		public UITextSkin ZoneText;

		// Token: 0x04001980 RID: 6528
		public UITextSkin PlayerNameText;

		// Token: 0x04001981 RID: 6529
		public Image TimeClockImage;

		// Token: 0x04001982 RID: 6530
		public List<Sprite> QudTimeImages;

		// Token: 0x04001983 RID: 6531
		public ActiveButton WindowPinButton;

		// Token: 0x04001984 RID: 6532
		public ActiveButton ExploreButton;

		// Token: 0x04001985 RID: 6533
		public ActiveButton FindButton;

		// Token: 0x04001986 RID: 6534
		public ActiveButton MinimapButton;

		// Token: 0x04001987 RID: 6535
		public List<global::UnityEngine.GameObject> Spacers;

		// Token: 0x04001988 RID: 6536
		public HPBar HPBar;

		// Token: 0x04001989 RID: 6537
		public HPBar XPBar;

		// Token: 0x0400198A RID: 6538
		private StringBuilder SB = new StringBuilder();

		// Token: 0x0400198B RID: 6539
		private ConsoleChar playerChar;

		// Token: 0x0400198C RID: 6540
		private ConsoleChar lastPlayerChar;

		// Token: 0x0400198D RID: 6541
		private bool playerCharDirty;

		// Token: 0x0400198E RID: 6542
		private Dictionary<string, string> playerStats = new Dictionary<string, string>
		{
			{ "Level", "1" },
			{ "XP", "0" }
		};

		// Token: 0x0400198F RID: 6543
		private Dictionary<string, string> lastPlayerStats = new Dictionary<string, string>();

		// Token: 0x04001990 RID: 6544
		private bool playerStatsDirty;

		// Token: 0x04001991 RID: 6545
		private object barLock = new object();

		// Token: 0x04001992 RID: 6546
		private PlayerStatusBar.BarValue hpbarValue;

		// Token: 0x04001993 RID: 6547
		private bool barValueDirty;

		// Token: 0x04001994 RID: 6548
		private object boolLock = new object();

		// Token: 0x04001995 RID: 6549
		private Dictionary<PlayerStatusBar.BoolDataType, bool> boolData = new Dictionary<PlayerStatusBar.BoolDataType, bool>
		{
			{
				PlayerStatusBar.BoolDataType.ObjectFinderActive,
				false
			},
			{
				PlayerStatusBar.BoolDataType.AutoExploreActive,
				false
			},
			{
				PlayerStatusBar.BoolDataType.MinimapActive,
				false
			},
			{
				PlayerStatusBar.BoolDataType.WindowsPinned,
				false
			}
		};

		// Token: 0x04001996 RID: 6550
		private Dictionary<PlayerStatusBar.BoolDataType, bool> lastBoolData = new Dictionary<PlayerStatusBar.BoolDataType, bool>();

		// Token: 0x04001997 RID: 6551
		private bool boolDataDirty;

		// Token: 0x04001998 RID: 6552
		private string customClock;

		// Token: 0x04001999 RID: 6553
		private string lastCustomClock;

		// Token: 0x0400199A RID: 6554
		private int timeOfDayImage = -2;

		// Token: 0x0400199B RID: 6555
		private int lastTimeOfDayImage = -2;

		// Token: 0x0400199C RID: 6556
		private bool timeOfDayImageDirty;

		// Token: 0x0400199D RID: 6557
		private object timeLock = new object();

		// Token: 0x0400199E RID: 6558
		private Dictionary<PlayerStatusBar.StringDataType, string> playerStringData = new Dictionary<PlayerStatusBar.StringDataType, string> { 
		{
			PlayerStatusBar.StringDataType.FoodWater,
			""
		} };

		// Token: 0x0400199F RID: 6559
		private Dictionary<PlayerStatusBar.StringDataType, string> lastPlayerStringData = new Dictionary<PlayerStatusBar.StringDataType, string>();

		// Token: 0x040019A0 RID: 6560
		private bool playerStringsDirty;

		// Token: 0x040019A1 RID: 6561
		private bool playerStringsSizeDirty;

		// Token: 0x040019A2 RID: 6562
		private StringBuilder sb = new StringBuilder(1024);

		// Token: 0x040019A3 RID: 6563
		private StringBuilder UpdateSB = new StringBuilder();

		// Token: 0x040019A4 RID: 6564
		private float LastWidth;

		// Token: 0x040019A5 RID: 6565
		private bool RelayoutTop = true;

		// Token: 0x040019A6 RID: 6566
		private Dictionary<string, Sprite> customClocks = new Dictionary<string, Sprite>();

		// Token: 0x02001907 RID: 6407
		private struct BarValue
		{
			// Token: 0x0600E21C RID: 57884 RVA: 0x00521787 File Offset: 0x0051F987
			public void SetBar(HPBar bar)
			{
				bar.BarStart = this.Min;
				bar.BarEnd = this.Max;
				bar.BarValue = this.Value;
				bar.WantsUpdate = true;
			}

			// Token: 0x04006474 RID: 25716
			public int Min;

			// Token: 0x04006475 RID: 25717
			public int Max;

			// Token: 0x04006476 RID: 25718
			public int Value;
		}

		// Token: 0x02001908 RID: 6408
		private enum BoolDataType
		{
			// Token: 0x04006478 RID: 25720
			AutoExploreActive,
			// Token: 0x04006479 RID: 25721
			ObjectFinderActive,
			// Token: 0x0400647A RID: 25722
			MinimapActive,
			// Token: 0x0400647B RID: 25723
			WindowsPinned
		}

		// Token: 0x02001909 RID: 6409
		private enum StringDataType
		{
			// Token: 0x0400647D RID: 25725
			FoodWater,
			// Token: 0x0400647E RID: 25726
			Time,
			// Token: 0x0400647F RID: 25727
			Temp,
			// Token: 0x04006480 RID: 25728
			Weight,
			// Token: 0x04006481 RID: 25729
			Zone,
			// Token: 0x04006482 RID: 25730
			HPBar,
			// Token: 0x04006483 RID: 25731
			PlayerName,
			// Token: 0x04006484 RID: 25732
			ZoneOnly
		}
	}
}
