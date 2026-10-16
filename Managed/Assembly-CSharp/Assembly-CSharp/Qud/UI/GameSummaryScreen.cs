using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaundryBear.PlatformServices;
using Platform.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XRL;
using XRL.CharacterBuilds.UI;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World.Conversations.Parts;

namespace Qud.UI
{
	// Token: 0x02000437 RID: 1079
	[UIView("GameSummaryScreen", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "GameSummary", UICanvasHost = 1)]
	public class GameSummaryScreen : SingletonWindowBase<GameSummaryScreen>
	{
		// Token: 0x060028F2 RID: 10482 RVA: 0x000D44F0 File Offset: 0x000D26F0
		public static async Task<bool> ShowGameSummary(string name, string cause, string details, bool real)
		{
			return await SingletonWindowBase<GameSummaryScreen>.instance._ShowGameSummary(name, cause, details, real);
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x000D454C File Offset: 0x000D274C
		public void SetEndmark(string mark, bool ultra)
		{
			this.tombstoneImage.sprite = this.endmarks.Where<Sprite>((Sprite e) => e.name == mark).FirstOrDefault<Sprite>();
			this.ultraMark.enabled = ultra;
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000D459C File Offset: 0x000D279C
		public async Task<bool> _ShowGameSummary(string name, string cause, string details, bool real)
		{
			this.Details = details;
			this.Name = name;
			this.bReal = real;
			this.completionSource = new TaskCompletionSource<bool>();
			await The.UiContext;
			ControlManager.ResetInput(false, true);
			this.Show();
			if (EndGame.IsAnyEnding)
			{
				if (The.Game.HasStringGameState("FinalEndmark"))
				{
					this.SetEndmark(The.Game.GetStringGameState("FinalEndmark", ""), EndGame.IsUltra);
				}
				else
				{
					this.topperBackground.enabled = true;
					if (EndGame.IsBrightsheol)
					{
						this.SetEndmark("brightsheol", EndGame.IsUltimate);
						if (EndGame.IsUltimate)
						{
							cause = "You annulled the plagues of the Gyre.\n\nThen you freed the Spindle for another's ascent and crossed into Brightsheol";
						}
						else
						{
							cause = "You freed the Spindle for another's ascent and crossed into Brightsheol.";
						}
					}
					else if (EndGame.IsMarooned)
					{
						The.Game.SetStringGameState("FinalEndmark", "marooned");
						this.SetEndmark("marooned", EndGame.IsUltimate);
						if (EndGame.IsUltimate)
						{
							cause = "You annulled the plagues of the Gyre.\n\nThen you destroyed Resheph to save the burgeoning world but marooned yourself at the North Sheva.";
						}
						else
						{
							cause = "You destroyed Resheph to save the burgeoning world but marooned yourself at the North Sheva.";
						}
					}
					else if (EndGame.IsCovenant)
					{
						The.Game.SetStringGameState("FinalEndmark", "covenant");
						this.SetEndmark("covenant", EndGame.IsUltimate);
						if (EndGame.IsUltimate)
						{
							cause = "You annulled the plagues of the Gyre.\n\nThen you entered into a covenant with Resheph to help prepare Qud for the Coven's return.";
						}
						else
						{
							cause = "You entered into a covenant with Resheph to help prepare Qud for the Coven's return.";
						}
					}
					else if (EndGame.IsReturn)
					{
						The.Game.SetStringGameState("FinalEndmark", "return");
						this.SetEndmark("return", EndGame.IsUltimate);
						if (EndGame.IsArkOpened)
						{
							if (EndGame.IsUltimate)
							{
								cause = "You annulled the plagues of the Gyre.\n\nThen you destroyed Resheph and returned to Qud to help garden the burgeoning world.";
							}
							else
							{
								cause = "You destroyed Resheph and returned to Qud to help garden the burgeoning world.";
							}
						}
						else if (EndGame.IsUltimate)
						{
							cause = "You annulled the plagues of the Gyre.\n\nThen you returned to Qud to help garden the burgeoning world.";
						}
						else
						{
							cause = "You rebuked Resheph and returned to Qud to help garden the burgeoning world.";
						}
					}
					else if (EndGame.IsAccede)
					{
						The.Game.SetStringGameState("FinalEndmark", "accede");
						this.SetEndmark("accede", EndGame.IsUltimate);
						if (EndGame.IsUltimate)
						{
							cause = "You annulled the plagues of the Gyre.\n\nThen you reversed course and acceded to Resheph's plan to purge the world of higher life, in preparation for the Coven's return.";
						}
						else
						{
							cause = "You acceded to Resheph's plan to purge the world of higher life in preparation for the Coven's return.";
						}
					}
					else if (EndGame.IsLaunch)
					{
						The.Game.SetStringGameState("FinalEndmark", "spaceship");
						this.SetEndmark("spaceship", EndGame.IsUltimate);
						if (EndGame.IsArkOpened)
						{
							if (EndGame.IsWithBarathrum)
							{
								if (EndGame.IsUltimate)
								{
									cause = "You annulled the plagues of the Gyre.\n\nThen you destroyed Resheph and launched yourself into the dusted cosmos to ply the stars with Barathrum.";
								}
								else
								{
									cause = "You destroyed Resheph and launched yourself into the dusted cosmos to ply the stars with Barathrum.";
								}
							}
							else if (EndGame.IsUltimate)
							{
								cause = "You annulled the plagues of the Gyre.\n\nThen you destroyed Resheph and launched yourself into the dusted cosmos to ply the stars.";
							}
							else
							{
								cause = "You destroyed Resheph and launched yourself into the dusted cosmos to ply the stars.";
							}
						}
						else if (EndGame.IsWithBarathrum)
						{
							if (EndGame.IsUltimate)
							{
								cause = "You annulled the plagues of the Gyre.\n\nThen you launched yourself into the dusted cosmos to ply the stars with Barathrum.";
							}
							else
							{
								cause = "You launched yourself into the dusted cosmos to ply the stars with Barathrum.";
							}
						}
						else if (EndGame.IsUltimate)
						{
							cause = "You annulled the plagues of the Gyre.\n\nThen you launched yourself into the dusted cosmos to ply the stars.";
						}
						else
						{
							cause = "You launched yourself into the dusted cosmos to ply the stars.";
						}
					}
					else
					{
						this.topperBackground.enabled = EndGame.IsUltimate;
						The.Game.SetStringGameState("FinalEndmark", "tombstone");
						this.SetEndmark("tombstone", EndGame.IsUltimate);
					}
				}
			}
			else
			{
				this.SetEndmark("tombstone", false);
			}
			this.nameText.SetText(name ?? "");
			this.causeText.SetText(cause);
			this.detailsText.SetText(details);
			bool info = await this.completionSource.Task;
			await The.UiContext;
			this.Hide();
			if (EndGame.IsAnyEnding)
			{
				FadeToBlackHigh.FadeIn(10f);
			}
			return info;
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x000D4600 File Offset: 0x000D2800
		public void Exit()
		{
			TaskCompletionSource<bool> taskCompletionSource = this.completionSource;
			if (taskCompletionSource == null)
			{
				return;
			}
			taskCompletionSource.TrySetResult(false);
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000D4614 File Offset: 0x000D2814
		public void Update()
		{
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000D4616 File Offset: 0x000D2816
		public void SetupContext()
		{
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060028F8 RID: 10488 RVA: 0x000D4618 File Offset: 0x000D2818
		public static MenuOption BACK_BUTTON
		{
			get
			{
				return EmbarkBuilderOverlayWindow.BackMenuOption;
			}
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000D4620 File Offset: 0x000D2820
		public void UpdateMenuBars()
		{
			this.keyMenuOptions.Clear();
			if (!Application.isConsolePlatform)
			{
				this.keyMenuOptions.Add(new MenuOption
				{
					InputCommand = "CmdHelp",
					Description = "Save Tombstone File"
				});
			}
			this.keyMenuOptions.Add(new MenuOption
			{
				InputCommand = "Cancel",
				Description = "Exit"
			});
			this.hotkeyBar.BeforeShow(null, this.keyMenuOptions);
			this.hotkeyBar.GetNavigationContext().disabled = false;
			this.hotkeyBar.onSelected.RemoveAllListeners();
			this.hotkeyBar.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleMenuOption));
			foreach (NavigationContext navigationContext in this.hotkeyBar.scrollContext.contexts.GetRange(0, this.keyMenuOptions.Count))
			{
				navigationContext.disabled = true;
			}
			NavigationContext navigationContext2 = this.vertNav;
			if (navigationContext2.commandHandlers == null)
			{
				navigationContext2.commandHandlers = new Dictionary<string, Action>();
			}
			this.vertNav.commandHandlers["Cancel"] = global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Exit));
			if (!Application.isConsolePlatform)
			{
				this.vertNav.commandHandlers["CmdHelp"] = global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.SaveTombstone));
			}
			navigationContext2 = this.vertNav;
			if (navigationContext2.axisHandlers == null)
			{
				navigationContext2.axisHandlers = new Dictionary<InputAxisTypes, Action>();
			}
			this.vertNav.axisHandlers.Set(InputAxisTypes.NavigationYAxis, global::XRL.UI.Framework.Event.Helpers.Handle(global::XRL.UI.Framework.Event.Helpers.Axis(new Action(this.summaryScroller.DoScrollDown), new Action(this.summaryScroller.DoScrollUp), null)));
			this.vertNav.axisHandlers.Set(InputAxisTypes.NavigationPageYAxis, global::XRL.UI.Framework.Event.Helpers.Handle(global::XRL.UI.Framework.Event.Helpers.Axis(new Action(this.summaryScroller.DoPageDown), new Action(this.summaryScroller.DoPageUp), null)));
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x000D4840 File Offset: 0x000D2A40
		public void SaveTombstone()
		{
			Debug.Log("Saving tombstone...");
			if (!this.bReal)
			{
				this.Exit();
				Debug.Log("Not real tombstone... early outing");
				return;
			}
			string text = string.Concat(new string[]
			{
				this.Name,
				"-",
				DateTime.Now.ToShortDateString(),
				"-",
				DateTime.Now.ToShortTimeString()
			});
			string text2 = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
			for (int i = 0; i < text2.Length; i++)
			{
				text = text.Replace(text2[i].ToString(), "");
			}
			text = DataManager.SavePath(text + ".txt");
			StorageResult storageResult = Blob.WriteAllText(text, this.Details);
			if (storageResult.WasSuccessful())
			{
				Popup.Show("Your tombstone file was saved:\n\n" + text.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return;
			}
			Popup.Show(string.Format("There was an error {0} saving: ", storageResult) + text.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x000D4973 File Offset: 0x000D2B73
		public void HandleMenuOption(FrameworkDataElement data)
		{
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x000D4975 File Offset: 0x000D2B75
		public IEnumerable<FrameworkDataElement> GetMenuItems()
		{
			yield return new CyberneticsTerminalLineData
			{
				Text = "Back"
			};
			yield break;
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x000D4980 File Offset: 0x000D2B80
		public override void Show()
		{
			base.Show();
			this.summaryScroller.scrollContext.wraps = false;
			this.summaryScroller.scrollContext.selectedPosition = 0;
			this.UpdateMenuBars();
			this.SetupContext();
			this.summaryScroller.scrollRect.normalizedPosition = new Vector2(0f, 1f);
			this.vertNav.disabled = false;
			this.vertNav.ActivateAndEnable();
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x000D49F8 File Offset: 0x000D2BF8
		public override void Hide()
		{
			NavigationContext activeContext = NavigationController.instance.activeContext;
			if (activeContext != null && activeContext.IsInside(this.vertNav))
			{
				NavigationController.instance.activeContext = null;
			}
			this.vertNav.disabled = true;
			base.Hide();
			ControlManager.ResetInput(false, true);
			base.gameObject.SetActive(false);
		}

		// Token: 0x040014A2 RID: 5282
		public TaskCompletionSource<bool> completionSource;

		// Token: 0x040014A3 RID: 5283
		public FrameworkScroller hotkeyBar;

		// Token: 0x040014A4 RID: 5284
		public FrameworkScroller summaryScroller;

		// Token: 0x040014A5 RID: 5285
		public NavigationContext vertNav = new NavigationContext();

		// Token: 0x040014A6 RID: 5286
		public Sprite[] endmarks;

		// Token: 0x040014A7 RID: 5287
		public Image ultraMark;

		// Token: 0x040014A8 RID: 5288
		public Image tombstoneImage;

		// Token: 0x040014A9 RID: 5289
		public UITextSkin detailsText;

		// Token: 0x040014AA RID: 5290
		public UITextSkin nameText;

		// Token: 0x040014AB RID: 5291
		public UITextSkin causeText;

		// Token: 0x040014AC RID: 5292
		public string Name;

		// Token: 0x040014AD RID: 5293
		public string Details;

		// Token: 0x040014AE RID: 5294
		private bool bReal;

		// Token: 0x040014AF RID: 5295
		public Image topperBackground;

		// Token: 0x040014B0 RID: 5296
		public bool wasInScroller;

		// Token: 0x040014B1 RID: 5297
		public List<MenuOption> keyMenuOptions = new List<MenuOption>();
	}
}
