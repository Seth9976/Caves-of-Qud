using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using Cysharp.Text;
using Genkit;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using XRL;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World;
using XRL.World.Parts;

namespace Qud.UI
{
	// Token: 0x02000487 RID: 1159
	[UIView("AbilityBar", false, false, false, null, null, false, 0, false, NavCategory = "Adventure", UICanvas = "AbilityBar", UICanvasHost = 1)]
	public class AbilityBar : SingletonWindowBase<AbilityBar>
	{
		// Token: 0x06002C8C RID: 11404 RVA: 0x000EFE08 File Offset: 0x000EE008
		public AbilityBar()
		{
			ControlManager.onActiveDeviceChanged += delegate(InputDevice d)
			{
				AbilityBar.markDirty();
			};
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000EFEC5 File Offset: 0x000EE0C5
		public static void markDirty()
		{
			if (SingletonWindowBase<AbilityBar>.instance != null)
			{
				SingletonWindowBase<AbilityBar>.instance.abilitiesDirty = true;
			}
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000EFEDF File Offset: 0x000EE0DF
		public override bool AllowPassthroughInput()
		{
			return true;
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000EFEE2 File Offset: 0x000EE0E2
		public override void Init()
		{
			base.Init();
			XRLCore.RegisterAfterRenderCallback(new Action<XRLCore, ScreenBuffer>(this.AfterRender));
			this.missileWeaponArea.Init();
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x000EFF08 File Offset: 0x000EE108
		public static void UpdateActiveEffects()
		{
			global::XRL.World.GameObject player = The.Player;
			if (player == null)
			{
				return;
			}
			SingletonWindowBase<AbilityBar>.instance.InternalUpdateActiveEffects(player);
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x000EFF2C File Offset: 0x000EE12C
		private void InternalUpdateActiveEffects(global::XRL.World.GameObject Object)
		{
			this.Descriptions.Clear();
			uint num = 16777619U;
			foreach (Effect effect in Object.Effects)
			{
				if (!effect.SuppressInStageDisplay() && effect.Duration > 0)
				{
					string description = effect.GetDescription();
					if (!description.IsNullOrEmpty())
					{
						this.Descriptions.Add(description);
						num = Hash.FNV1A32(description, num);
					}
				}
			}
			if (num == this.LastEffectsHash)
			{
				return;
			}
			this.LastEffectsHash = num;
			string text = "";
			Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder();
			try
			{
				utf16ValueStringBuilder.Append("{{Y|<color=#508d75>ACTIVE EFFECTS:</color>}} ");
				int i = 0;
				int count = this.Descriptions.Count;
				while (i < count)
				{
					if (i != 0)
					{
						utf16ValueStringBuilder.Append(", ");
					}
					Sidebar.FormatToRTF(Markup.Wrap(this.Descriptions[i]), ref utf16ValueStringBuilder, "FF");
					i++;
				}
				text = utf16ValueStringBuilder.ToString();
			}
			finally
			{
				utf16ValueStringBuilder.Dispose();
			}
			object obj = this.effectLock;
			lock (obj)
			{
				this.effectText = text;
				this.effectTextDirty = true;
			}
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x000F009C File Offset: 0x000EE29C
		private void AfterRender(XRLCore core, ScreenBuffer sb)
		{
			global::XRL.World.GameObject player = The.Player;
			if (player != null)
			{
				string text = null;
				using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
				{
					utf16ValueStringBuilder.Append("{{Y|<color=#508d75>ACTIVE EFFECTS:</color>}} ");
					bool flag = true;
					foreach (Effect effect in player.Effects)
					{
						if (!effect.SuppressInStageDisplay())
						{
							string description = effect.GetDescription();
							if (!string.IsNullOrEmpty(description))
							{
								if (!flag)
								{
									utf16ValueStringBuilder.Append(", ");
								}
								else
								{
									flag = false;
								}
								utf16ValueStringBuilder.Append(Markup.Wrap(description));
							}
						}
					}
					text = utf16ValueStringBuilder.ToString();
				}
				if (this.lastEffectText != text)
				{
					object obj = this.effectLock;
					lock (obj)
					{
						this.effectText = ((text != null) ? text.ToRTFCached(-1, false) : null) ?? "";
						this.lastEffectText = text;
						this.effectTextDirty = true;
					}
				}
				ActivatedAbilities activatedAbilities = player.ActivatedAbilities;
				if (activatedAbilities != null && activatedAbilities.AbilityByGuid != null)
				{
					if (this.currentAbilities == null)
					{
						this.currentAbilities = new List<AbilityBar.AbilityDescription>(activatedAbilities.AbilityByGuid.Count);
					}
					this.currentAbilities.Clear();
					using (IEnumerator<ActivatedAbilityEntry> enumerator2 = activatedAbilities.GetAbilityListOrderedByPreference().GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ActivatedAbilityEntry activatedAbilityEntry = enumerator2.Current;
							int num = 0;
							this.currentAbilities.Add(AbilityBar.AbilityDescription.next().Set(num, activatedAbilityEntry));
						}
						goto IL_01B8;
					}
				}
				if (this.currentAbilities == null && this.currentAbilities == null)
				{
					this.currentAbilities = new List<AbilityBar.AbilityDescription>();
				}
				this.currentAbilities.Clear();
				IL_01B8:
				if (this.currentAbilities.Count != this.lastAbilities.Count || !this.currentAbilities.SequenceEqual<AbilityBar.AbilityDescription>(this.lastAbilities))
				{
					List<AbilityBar.AbilityDescription> list = this.abilities;
					lock (list)
					{
						for (int i = 0; i < this.lastAbilities.Count; i++)
						{
							this.lastAbilities[i].free();
						}
						this.lastAbilities.Clear();
						this.lastAbilities.AddRange(this.currentAbilities);
						this.abilities.Clear();
						this.abilities.AddRange(this.currentAbilities);
						this.abilitiesDirty = true;
						goto IL_02AA;
					}
				}
				for (int j = 0; j < this.currentAbilities.Count; j++)
				{
					this.currentAbilities[j].free();
				}
				this.currentAbilities.Clear();
				IL_02AA:
				global::XRL.World.GameObject currentTarget = Sidebar.CurrentTarget;
				if (currentTarget != null)
				{
					string text2 = null;
					string text3 = null;
					StringBuilder sb2 = this.SB;
					lock (sb2)
					{
						this.SB.Clear().Append("{{C|<color=#3e83a5>TARGET:</color> ").Append(currentTarget.DisplayName)
							.Append("}}");
						text3 = this.SB.ToString();
						Description part = currentTarget.GetPart<Description>();
						this.SB.Clear().Append(Strings.WoundLevel(currentTarget));
						if (part != null)
						{
							if (!string.IsNullOrEmpty(part.GetFeelingDescription(null)))
							{
								this.SB.Append(", ").Append(Markup.Wrap(part.GetFeelingDescription(null)));
							}
							if (!string.IsNullOrEmpty(part.GetDifficultyDescription(null)))
							{
								this.SB.Append(", ").Append(Markup.Wrap(part.GetDifficultyDescription(null)));
							}
						}
						text2 = this.SB.ToString();
					}
					if (!(this.lastTargetText != text3) && !(this.lastTargetHealthText != text2))
					{
						return;
					}
					object obj = this.targetLock;
					lock (obj)
					{
						this.lastTargetText = text3;
						this.lastTargetHealthText = text2;
						this.targetText = text3 ?? "";
						this.targetHealthText = text2 ?? "";
						this.targetTextDirty = true;
						return;
					}
				}
				string text4 = "{{K|TARGET: [none]}}";
				if (this.lastTargetText != text4)
				{
					object obj = this.targetLock;
					lock (obj)
					{
						this.lastTargetText = text4;
						this.lastTargetHealthText = "";
						this.targetText = this.lastTargetText ?? "";
						this.targetHealthText = "";
						this.targetTextDirty = true;
					}
				}
			}
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x000F05A8 File Offset: 0x000EE7A8
		public void OnTargetAreaClicked()
		{
			ConsoleLib.Console.Keyboard.PushMouseEvent("Command:CmdTarget", null);
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000F05B5 File Offset: 0x000EE7B5
		public void OnEffectsAreaClicked()
		{
			ConsoleLib.Console.Keyboard.PushMouseEvent("Command:CmdShowEffects", null);
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x000F05C2 File Offset: 0x000EE7C2
		public override void Show()
		{
			base.Show();
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x000F05CA File Offset: 0x000EE7CA
		public void OnEffectsClick()
		{
			Debug.Log("Click effects");
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06002C97 RID: 11415 RVA: 0x000F05D8 File Offset: 0x000EE7D8
		public ActivatedAbilityEntry currentlySelectedAbility
		{
			get
			{
				if (this.SelectedAbility >= 0 && this.SelectedAbility < this.AbilityButtons.Count)
				{
					int num = this.offset + this.SelectedAbility;
					if (num >= 0 && num < this.abilities.Count)
					{
						return this.abilities[num].Entry;
					}
				}
				return null;
			}
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x000F0634 File Offset: 0x000EE834
		public void Update()
		{
			if (!base.canvas.enabled)
			{
				return;
			}
			if (GameManager.Instance.CurrentGameView == "Stage")
			{
				if (ControlManager.isCommandDown("Ability Page Up", true, false, false))
				{
					this.MovePage(-1);
				}
				if (ControlManager.isCommandDown("Ability Page Down", true, false, false))
				{
					this.MovePage(1);
				}
				if (ControlManager.isCommandDown("Next Ability", true, false, false))
				{
					this.MoveSelection(1);
				}
				if (ControlManager.isCommandDown("Previous Ability", true, false, false))
				{
					this.MoveSelection(-1);
				}
				if (ControlManager.isCommandDown("Use Ability", true, false, false))
				{
					ActivatedAbilityEntry currentlySelectedAbility = this.currentlySelectedAbility;
					if (currentlySelectedAbility != null)
					{
						currentlySelectedAbility.TrySendCommandEventOnPlayer();
					}
				}
			}
			string commandInputDescription = ControlManager.getCommandInputDescription("CmdAbilities", true, false, ControlManager.InputDeviceType.Unknown, false);
			if (this.AbilityScreenHotkey.text != commandInputDescription)
			{
				this.AbilityScreenHotkey.text = commandInputDescription;
			}
			if (this.effectTextDirty)
			{
				object obj = this.effectLock;
				lock (obj)
				{
					if (this.EffectText.text != this.effectText)
					{
						this.EffectText.SetText(this.effectText);
					}
					this.effectTextDirty = false;
				}
			}
			if (this.targetTextDirty)
			{
				object obj = this.targetLock;
				lock (obj)
				{
					if (this.TargetArea.gameObject.activeSelf != (this.targetText != null))
					{
						this.TargetArea.gameObject.SetActive(this.targetText != null);
					}
					if (this.TargetText.text != this.targetText)
					{
						this.TargetText.SetText(this.targetText);
					}
					if (this.TargetHealthText.text != this.targetHealthText)
					{
						this.TargetHealthText.SetText(this.targetHealthText);
					}
					this.targetTextDirty = false;
				}
			}
			if (this.abilitiesDirty)
			{
				List<AbilityBar.AbilityDescription> list = this.abilities;
				lock (list)
				{
					Queue<ActivatedAbilityEntry> entryPool = AbilityBar.AbilityDescription.entryPool;
					lock (entryPool)
					{
						foreach (ActivatedAbilityEntry activatedAbilityEntry in AbilityBar.hotkeyAbilityCommands.Values)
						{
							if (activatedAbilityEntry != null)
							{
								activatedAbilityEntry.Clear();
								AbilityBar.AbilityDescription.entryPool.Enqueue(activatedAbilityEntry);
							}
						}
					}
					AbilityBar.hotkeyAbilityCommands.Clear();
					if (this.AbilityButtons.Count != this.abilities.Count)
					{
						using (IEnumerator enumerator2 = this.ButtonArea.transform.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								Transform transform = (Transform)obj2;
								if (!this.AbilityButtons.Contains(transform.gameObject))
								{
									global::UnityEngine.Object.Destroy(transform.gameObject);
								}
							}
							goto IL_0322;
						}
						IL_02ED:
						this.AbilityButtons[this.AbilityButtons.Count - 1].Destroy();
						this.AbilityButtons.RemoveAt(this.AbilityButtons.Count - 1);
						IL_0322:
						if (this.AbilityButtons.Count > this.abilities.Count)
						{
							goto IL_02ED;
						}
						while (this.AbilityButtons.Count < this.abilities.Count)
						{
							global::UnityEngine.GameObject gameObject = this.ButtonPrefab.Instantiate<global::UnityEngine.GameObject>();
							gameObject.transform.SetParent(this.ButtonArea.transform, false);
							this.AbilityButtons.Add(gameObject);
						}
					}
					this.usedHotkeys.Clear();
					for (int i = 0; i < this.abilities.Count; i++)
					{
						if (this.abilities[i].Entry != null)
						{
							string commandInputDescription2 = ControlManager.getCommandInputDescription(this.abilities[i].Entry.Command, true, false, ControlManager.InputDeviceType.Unknown, false);
							this.abilities[i].Entry.DisplayForHotkey = commandInputDescription2;
							if (!string.IsNullOrEmpty(commandInputDescription2))
							{
								this.usedHotkeys.Add(commandInputDescription2);
							}
						}
					}
					for (int j = 0; j < this.abilities.Count; j++)
					{
						AbilityBarButton component = this.AbilityButtons[j].GetComponent<AbilityBarButton>();
						component.Icon.FromRenderable(this.abilities[j].Entry.GetUITile(), true);
						global::UnityEngine.GameObject gameObject2 = component.gameObject;
						string text = "AbilityBar:Button:";
						ActivatedAbilityEntry entry = this.abilities[j].Entry;
						ControlId.Assign(gameObject2, text + ((entry != null) ? entry.Command : null), false);
						using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
						{
							utf16ValueStringBuilder.Append((Options.AbilityBarMode == 1) ? "" : ((this.abilities[j].Entry.Enabled ? "&C" : "&c") + this.abilities[j].Entry.DisplayName));
							if (!this.abilities[j].Entry.Enabled)
							{
								utf16ValueStringBuilder.Append(" {{K|[disabled]}}");
							}
							else if (this.abilities[j].Entry.Cooldown > 0)
							{
								utf16ValueStringBuilder.Append(" {{C|[" + this.abilities[j].Entry.CooldownRounds.ToString() + "]}}");
							}
							if (this.abilities[j].Entry.Toggleable)
							{
								if (this.abilities[j].Entry.ToggleState)
								{
									utf16ValueStringBuilder.Append(" {{K|[{{g|on}}]}}");
								}
								else
								{
									utf16ValueStringBuilder.Append(" {{K|[{{y|off}}]}}");
								}
							}
							if (Options.ModernUI)
							{
								string text2 = this.abilities[j].Entry.DisplayForHotkey;
								string commandInputDescription3 = ControlManager.getCommandInputDescription("CmdAbility" + (j - this.offset + 1).ToString(), true, false, ControlManager.InputDeviceType.Unknown, false);
								if (!this.usedHotkeys.Contains(commandInputDescription3))
								{
									AbilityBar.hotkeyAbilityCommands.Add("CmdAbility" + (j - this.offset + 1).ToString(), AbilityBar.AbilityDescription.nextEntry().CopyFrom(this.abilities[j].Entry));
									if (string.IsNullOrEmpty(text2))
									{
										text2 = commandInputDescription3;
									}
								}
								if (!string.IsNullOrEmpty(text2))
								{
									utf16ValueStringBuilder.Append(" {{Y|<{{w|" + text2 + "}}>}}");
								}
							}
							else if (CapabilityManager.AllowKeyboardHotkeys && this.abilities[j].KeyCode != 2147483647)
							{
								utf16ValueStringBuilder.Append(" {{Y|<{{w|" + ConsoleLib.Console.Keyboard.MetaToString(this.abilities[j].KeyCode) + "}}>}}");
							}
							component.disabled = !this.abilities[j].Entry.Enabled || this.abilities[j].Entry.Cooldown > 0;
							component.command = this.abilities[j].Entry.Command;
							string text3 = utf16ValueStringBuilder.ToString();
							string text4 = ((text3 != null) ? text3.ToRTFCached(-1, false) : null) ?? "";
							if (component.Text.text != text4)
							{
								TMP_Text text5 = component.Text;
								string text6 = utf16ValueStringBuilder.ToString();
								text5.SetText(((text6 != null) ? text6.ToRTFCached(-1, false) : null) ?? "");
							}
						}
					}
					this.abilitiesDirty = false;
				}
				this.UpdateAbilitiesText();
			}
			int num = 175;
			if (Options.AbilityBarMode == 1)
			{
				num = 90;
			}
			if (this.ButtonArea != null && Math.Floor((double)(this.ButtonArea.rect.width / (float)num)) != (double)this.numPerPage)
			{
				if (this.numPerPage != 0)
				{
					this.offset /= Math.Max(1, this.numPerPage);
				}
				this.numPerPage = Math.Max(1, (int)this.ButtonArea.rect.width / num);
				this.offset *= this.numPerPage;
			}
			if (this.numPerPage > 0)
			{
				while (this.offset >= this.AbilityButtons.Count)
				{
					this.offset -= this.numPerPage;
				}
				while (this.offset >= this.abilities.Count)
				{
					this.offset -= this.numPerPage;
				}
				this.offset = Math.Max(0, this.offset);
			}
			this.PageText.text = ((this.numPerPage == 0) ? "0" : (this.offset / Math.Max(1, this.numPerPage) + 1).ToStringCached());
			this.UpdateAbilitiesText();
			this.PagerArea.SetActive(this.AbilityButtons.Count == 0 || this.AbilityButtons.Count > this.numPerPage);
			this.VisibleAbilityButtons = 0;
			for (int k = 0; k < this.AbilityButtons.Count; k++)
			{
				bool flag3 = k >= this.offset && k < this.offset + this.numPerPage;
				if (this.AbilityButtons[k].activeSelf != flag3)
				{
					this.AbilityButtons[k].SetActive(flag3);
				}
				if (flag3)
				{
					this.VisibleAbilityButtons++;
				}
			}
			if (this.SelectedAbility < 0)
			{
				this.SelectedAbility = this.numPerPage - 1;
			}
			if (this.SelectedAbility >= this.VisibleAbilityButtons)
			{
				this.SelectedAbility = 0;
			}
			bool flag4 = ControlManager.isCommandMapped("Use Ability");
			int num2 = this.offset;
			while (num2 < this.offset + this.numPerPage && num2 < this.AbilityButtons.Count)
			{
				this.AbilityButtons[num2].GetComponent<AbilityBarButton>().highlighted = num2 - this.offset == this.SelectedAbility && flag4;
				num2++;
			}
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x000F1138 File Offset: 0x000EF338
		public void AbilitiesButtonClicked()
		{
			ConsoleLib.Console.Keyboard.PushMouseEvent("Command:CmdAbilities", null);
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x000F1148 File Offset: 0x000EF348
		public void MoveSelection(int direction)
		{
			this.SelectedAbility += direction;
			if (this.SelectedAbility < 0)
			{
				this.SelectedAbility = this.VisibleAbilityButtons - 1;
			}
			if (this.SelectedAbility >= this.VisibleAbilityButtons)
			{
				this.SelectedAbility = 0;
			}
			this.abilitiesDirty = true;
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x000F1196 File Offset: 0x000EF396
		public void MoveToNextPage()
		{
			this.MovePage(1);
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x000F119F File Offset: 0x000EF39F
		public void MoveToPreviousPage()
		{
			this.MovePage(-1);
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x000F11A8 File Offset: 0x000EF3A8
		public void MoveToPage(int page)
		{
			this.offset = 0;
			for (int i = 0; i < page; i++)
			{
				this.MovePage(1);
			}
			this.abilitiesDirty = true;
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x000F11D8 File Offset: 0x000EF3D8
		public void UpdateAbilitiesText()
		{
			int count = this.AbilityButtons.Count;
			int num = Math.Max(1, this.numPerPage);
			if (count != this.lastCount || num != this.lastMax)
			{
				this.lastCount = count;
				this.lastMax = num;
				if (this.AbilityButtons.Count / Math.Max(1, this.numPerPage) > 0)
				{
					this.CycleCommandText.GetComponent<UITextSkin>().text = ControlManager.getCommandInputDescription("Ability Page Up", true, true, ControlManager.InputDeviceType.Unknown, false) + " " + ControlManager.getCommandInputDescription("Ability Page Down", true, true, ControlManager.InputDeviceType.Unknown, false);
					this.CycleCommandText.GetComponent<UITextSkin>().Apply();
					this.AbilityCommandText.GetComponent<UITextSkin>().text = string.Format("ABILITIES\npage {0} of {1}", this.offset / Math.Max(1, this.numPerPage) + 1, (this.AbilityButtons.Count - 1) / Math.Max(1, this.numPerPage) + 1);
					this.AbilityCommandText.GetComponent<UITextSkin>().Apply();
					return;
				}
				this.CycleCommandText.GetComponent<UITextSkin>().text = "";
				this.CycleCommandText.GetComponent<UITextSkin>().Apply();
				this.AbilityCommandText.GetComponent<UITextSkin>().text = "ABILITIES";
				this.AbilityCommandText.GetComponent<UITextSkin>().Apply();
			}
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x000F1338 File Offset: 0x000EF538
		public void MovePage(int direction)
		{
			this.offset += direction * this.numPerPage;
			if (this.offset >= this.AbilityButtons.Count)
			{
				this.offset = 0;
			}
			if (this.offset < 0)
			{
				this.offset = Math.Max(0, (int)Math.Floor((double)(((float)this.AbilityButtons.Count - 1f) / (float)Math.Max(1, this.numPerPage))) * this.numPerPage);
			}
			this.abilitiesDirty = true;
			this.UpdateAbilitiesText();
		}

		// Token: 0x040018B1 RID: 6321
		public UITextSkin EffectText;

		// Token: 0x040018B2 RID: 6322
		public RectTransform ButtonArea;

		// Token: 0x040018B3 RID: 6323
		public RectTransform TargetArea;

		// Token: 0x040018B4 RID: 6324
		public UITextSkin TargetText;

		// Token: 0x040018B5 RID: 6325
		public UITextSkin TargetHealthText;

		// Token: 0x040018B6 RID: 6326
		public global::UnityEngine.GameObject ButtonPrefab;

		// Token: 0x040018B7 RID: 6327
		public TextMeshProUGUI PageText;

		// Token: 0x040018B8 RID: 6328
		public global::UnityEngine.GameObject PagerArea;

		// Token: 0x040018B9 RID: 6329
		public UITextSkin AbilityCommandText;

		// Token: 0x040018BA RID: 6330
		public UITextSkin CycleCommandText;

		// Token: 0x040018BB RID: 6331
		public TextMeshProUGUI AbilityScreenHotkey;

		// Token: 0x040018BC RID: 6332
		public MissileWeaponArea missileWeaponArea;

		// Token: 0x040018BD RID: 6333
		private List<global::UnityEngine.GameObject> AbilityButtons = new List<global::UnityEngine.GameObject>();

		// Token: 0x040018BE RID: 6334
		private List<string> Descriptions = new List<string>(16);

		// Token: 0x040018BF RID: 6335
		private StringBuilder SB = new StringBuilder();

		// Token: 0x040018C0 RID: 6336
		private object effectLock = new object();

		// Token: 0x040018C1 RID: 6337
		private string effectText;

		// Token: 0x040018C2 RID: 6338
		private string lastEffectText;

		// Token: 0x040018C3 RID: 6339
		private uint LastEffectsHash;

		// Token: 0x040018C4 RID: 6340
		private bool effectTextDirty;

		// Token: 0x040018C5 RID: 6341
		private object targetLock = new object();

		// Token: 0x040018C6 RID: 6342
		private string targetText;

		// Token: 0x040018C7 RID: 6343
		private string targetHealthText;

		// Token: 0x040018C8 RID: 6344
		private string lastTargetText = "not null";

		// Token: 0x040018C9 RID: 6345
		private string lastTargetHealthText = "";

		// Token: 0x040018CA RID: 6346
		private bool targetTextDirty;

		// Token: 0x040018CB RID: 6347
		private int offset;

		// Token: 0x040018CC RID: 6348
		private int numPerPage = 9;

		// Token: 0x040018CD RID: 6349
		private static Queue<AbilityBar.AbilityDescription> abilityDescriptionPool = new Queue<AbilityBar.AbilityDescription>();

		// Token: 0x040018CE RID: 6350
		private List<AbilityBar.AbilityDescription> abilities = new List<AbilityBar.AbilityDescription>();

		// Token: 0x040018CF RID: 6351
		private List<AbilityBar.AbilityDescription> lastAbilities = new List<AbilityBar.AbilityDescription>();

		// Token: 0x040018D0 RID: 6352
		private bool abilitiesDirty;

		// Token: 0x040018D1 RID: 6353
		private List<AbilityBar.AbilityDescription> currentAbilities;

		// Token: 0x040018D2 RID: 6354
		public static List<string> ABILITY_COMMANDS = new List<string> { "CmdAbility1", "CmdAbility2", "CmdAbility3", "CmdAbility4", "CmdAbility5", "CmdAbility6", "CmdAbility7", "CmdAbility8", "CmdAbility9", "CmdAbility10" };

		// Token: 0x040018D3 RID: 6355
		public static Dictionary<string, ActivatedAbilityEntry> hotkeyAbilityCommands = new Dictionary<string, ActivatedAbilityEntry>();

		// Token: 0x040018D4 RID: 6356
		public HashSet<string> usedHotkeys = new HashSet<string>();

		// Token: 0x040018D5 RID: 6357
		private int SelectedAbility;

		// Token: 0x040018D6 RID: 6358
		private int VisibleAbilityButtons;

		// Token: 0x040018D7 RID: 6359
		private int lastCount = -1;

		// Token: 0x040018D8 RID: 6360
		private int lastMax = -1;

		// Token: 0x020018EB RID: 6379
		private class AbilityDescription : IEquatable<AbilityBar.AbilityDescription>
		{
			// Token: 0x0600E1C7 RID: 57799 RVA: 0x005201CC File Offset: 0x0051E3CC
			public override bool Equals(object obj)
			{
				if (obj == null || base.GetType() != obj.GetType())
				{
					return false;
				}
				AbilityBar.AbilityDescription abilityDescription = (AbilityBar.AbilityDescription)obj;
				return this.KeyCode == abilityDescription.KeyCode && this.Entry.Equals(abilityDescription.Entry);
			}

			// Token: 0x0600E1C8 RID: 57800 RVA: 0x00520219 File Offset: 0x0051E419
			public bool Equals(AbilityBar.AbilityDescription obj)
			{
				return this.KeyCode == obj.KeyCode && this.Entry.Equals(obj.Entry);
			}

			// Token: 0x0600E1C9 RID: 57801 RVA: 0x0052023C File Offset: 0x0051E43C
			public override int GetHashCode()
			{
				return new { this.KeyCode, this.Entry }.GetHashCode();
			}

			// Token: 0x0600E1CA RID: 57802 RVA: 0x00520254 File Offset: 0x0051E454
			public static AbilityBar.AbilityDescription next()
			{
				if (AbilityBar.abilityDescriptionPool.Count > 0)
				{
					return AbilityBar.abilityDescriptionPool.Dequeue();
				}
				return new AbilityBar.AbilityDescription();
			}

			// Token: 0x0600E1CB RID: 57803 RVA: 0x00520274 File Offset: 0x0051E474
			public void free()
			{
				if (this.Entry != null)
				{
					this.Entry.Clear();
					Queue<ActivatedAbilityEntry> queue = AbilityBar.AbilityDescription.entryPool;
					lock (queue)
					{
						AbilityBar.AbilityDescription.entryPool.Enqueue(this.Entry);
					}
				}
				this.Entry = null;
				this.KeyCode = 0;
				AbilityBar.abilityDescriptionPool.Enqueue(this);
			}

			// Token: 0x0600E1CC RID: 57804 RVA: 0x005202EC File Offset: 0x0051E4EC
			public static ActivatedAbilityEntry nextEntry()
			{
				Queue<ActivatedAbilityEntry> queue = AbilityBar.AbilityDescription.entryPool;
				ActivatedAbilityEntry activatedAbilityEntry;
				lock (queue)
				{
					if (AbilityBar.AbilityDescription.entryPool.Count > 0)
					{
						activatedAbilityEntry = AbilityBar.AbilityDescription.entryPool.Dequeue();
					}
					else
					{
						activatedAbilityEntry = new ActivatedAbilityEntry();
					}
				}
				return activatedAbilityEntry;
			}

			// Token: 0x0600E1CD RID: 57805 RVA: 0x00520348 File Offset: 0x0051E548
			public AbilityBar.AbilityDescription Set(int KeyCode, ActivatedAbilityEntry Entry)
			{
				this.KeyCode = KeyCode;
				this.Entry = AbilityBar.AbilityDescription.nextEntry().CopyFrom(Entry);
				return this;
			}

			// Token: 0x0400641A RID: 25626
			public int KeyCode;

			// Token: 0x0400641B RID: 25627
			public ActivatedAbilityEntry Entry;

			// Token: 0x0400641C RID: 25628
			public static Queue<ActivatedAbilityEntry> entryPool = new Queue<ActivatedAbilityEntry>();
		}
	}
}
