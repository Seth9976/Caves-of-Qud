using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ConsoleLib.Console;
using Qud.UI;
using UnityEngine;
using UnityEngine.Events;
using XRL.Rules;
using XRL.UI;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds.Qud.UI
{
	// Token: 0x020005B3 RID: 1459
	[UIView("CharacterCreation:BuildLibrary", false, false, false, null, null, false, 0, false, NavCategory = "Chargen", UICanvas = "Chargen/BuildLibrary", UICanvasHost = 1)]
	public class QudBuildLibraryModuleWindow : EmbarkBuilderModuleWindowPrefabBase<QudBuildLibraryModule, HorizontalScroller>
	{
		// Token: 0x06003E26 RID: 15910 RVA: 0x0013129A File Offset: 0x0012F49A
		public override void RandomSelection()
		{
			base.prefabComponent.scrollContext.GetContextAt(Stat.Random(0, base.prefabComponent.choices.Count - 1)).ActivateAndEnable();
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x001312CA File Offset: 0x0012F4CA
		public void onBack()
		{
			base.module.onBack();
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x001312D7 File Offset: 0x0012F4D7
		public IEnumerable<ChoiceWithColorIcon> GetSelections()
		{
			List<BuildEntry> buildEntries = BuildLibrary.BuildEntries;
			foreach (BuildEntry buildEntry in buildEntries)
			{
				yield return new ChoiceWithColorIcon
				{
					Id = "code:" + buildEntry.Code,
					Title = buildEntry.Name,
					IconPath = buildEntry.Tile,
					IconDetailColor = ConsoleLib.Console.ColorUtility.colorFromChar(string.IsNullOrEmpty(buildEntry.Detail) ? 'w' : buildEntry.Detail[0]),
					IconForegroundColor = ConsoleLib.Console.ColorUtility.colorFromChar(string.IsNullOrEmpty(buildEntry.Foreground) ? 'y' : buildEntry.Foreground[0]),
					Description = ""
				};
			}
			List<BuildEntry>.Enumerator enumerator = default(List<BuildEntry>.Enumerator);
			new List<ChoiceWithColorIcon>();
			yield return new ChoiceWithColorIcon
			{
				Id = "___CreateNew",
				Title = "Add a new build code",
				IconPath = "UI/sw_newchar.bmp",
				IconDetailColor = Color.yellow,
				IconForegroundColor = Color.gray,
				Description = ""
			};
			yield break;
			yield break;
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x001312E0 File Offset: 0x0012F4E0
		public async void onSelect(FrameworkDataElement choice)
		{
			if (choice.Id.StartsWith("code:"))
			{
				string text = choice.Id.Split(':', StringSplitOptions.None)[1];
				base.module.builder.InitModulesFromCode(text, false);
				base.module.setData(new QudBuildLibraryModuleData(text));
				EmbarkBuilder builder = base.module.builder;
				EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor = builder.activeWindow;
				string text2;
				do
				{
					builder.advance(false, false, false);
					if (builder.activeWindow == null || builder.activeWindow == embarkBuilderModuleWindowDescriptor)
					{
						break;
					}
					embarkBuilderModuleWindowDescriptor = builder.activeWindow;
					if (builder == null)
					{
						text2 = null;
					}
					else
					{
						EmbarkBuilderModuleWindowDescriptor activeWindow = builder.activeWindow;
						text2 = ((activeWindow != null) ? activeWindow.viewID : null);
					}
				}
				while (text2 != "Chargen/BuildSummary");
			}
			else if (choice.Id.StartsWith("___CreateNew"))
			{
				string text3 = await Popup.AskStringAsync("Paste build code:", "", 80, 0, null, true, true, new bool?(false), false, null);
				if (text3 != null)
				{
					await QudBuildLibraryModuleWindow.AddBuild(text3);
					base.prefabComponent.BeforeShow(this.descriptor, this.GetSelections());
					base.prefabComponent.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HighlightBuild));
				}
			}
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x00131320 File Offset: 0x0012F520
		public static async Task AddBuild(string code)
		{
			EmbarkBuilder builder = new EmbarkBuilder();
			int num = 0;
			try
			{
				builder = EmbarkBuilder.FromCode(code, true);
			}
			catch
			{
				num = 1;
			}
			if (num == 1)
			{
				await Popup.NewPopupMessageAsync("That code appears to be invalid.", null, null, null, null, 0, null, null, null, true, true, false, default(CancellationToken), false, "", null, null, null);
			}
			else if (BuildLibrary.HasBuild(code))
			{
				await Popup.NewPopupMessageAsync("That code is already in your library. It's named " + BuildLibrary.GetBuild(code).Name + ".", null, null, null, null, 0, null, null, null, true, true, false, default(CancellationToken), false, "", null, null, null);
			}
			else
			{
				string text = await Popup.AskStringAsync("Name this build:", "", 80, 0, null, true, true, null, false, null);
				if (text != null)
				{
					EmbarkBuilder embarkBuilder = builder;
					string text2;
					if (embarkBuilder == null)
					{
						text2 = null;
					}
					else
					{
						QudSubtypeModule module = embarkBuilder.GetModule<QudSubtypeModule>();
						if (module == null)
						{
							text2 = null;
						}
						else
						{
							QudSubtypeModuleData data = module.data;
							if (data == null)
							{
								text2 = null;
							}
							else
							{
								SubtypeEntry entry = data.Entry;
								text2 = ((entry != null) ? entry.Tile : null);
							}
						}
					}
					string text3 = text2 ?? "Assets_Content_Textures_Creatures_sw_farmer.bmp";
					string text4 = "y";
					EmbarkBuilder embarkBuilder2 = builder;
					string text5;
					if (embarkBuilder2 == null)
					{
						text5 = null;
					}
					else
					{
						QudSubtypeModule module2 = embarkBuilder2.GetModule<QudSubtypeModule>();
						if (module2 == null)
						{
							text5 = null;
						}
						else
						{
							QudSubtypeModuleData data2 = module2.data;
							if (data2 == null)
							{
								text5 = null;
							}
							else
							{
								SubtypeEntry entry2 = data2.Entry;
								text5 = ((entry2 != null) ? entry2.DetailColor : null);
							}
						}
					}
					BuildLibrary.AddBuild(code, text, text3, text4, text5 ?? "w");
				}
			}
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x00131364 File Offset: 0x0012F564
		public override async void HandleMenuOption(MenuOption menuOption)
		{
			if (((menuOption != null) ? menuOption.Id : null) == "CmdChargenItemOptions" && this.hasOptions)
			{
				BuildEntry build = BuildLibrary.GetBuild(this.selected.Split(':', StringSplitOptions.None)[1]);
				QudMenuItem qudMenuItem = await Popup.NewPopupMessageAsync("Manage Build: " + build.Name, PopupMessage.AcceptCancelButton, new List<QudMenuItem>
				{
					new QudMenuItem
					{
						text = "Delete Build",
						command = "delete"
					},
					new QudMenuItem
					{
						text = "Rename Build",
						command = "rename"
					}
				}, null, null, 0, null, null, null, true, true, false, default(CancellationToken), false, "", null, null, null);
				if (qudMenuItem.command != "Cancel")
				{
					if (qudMenuItem.command == "delete")
					{
						BuildLibrary.DeleteBuild(build.Code);
						this.BeforeShow(this.descriptor);
					}
					else if (qudMenuItem.command == "rename")
					{
						string text = await Popup.AskStringAsync("Name this build:", "", 80, 0, null, true, true, null, false, null);
						if (text != null)
						{
							build.Name = text;
							BuildLibrary.UpdateBuild(build);
							base.prefabComponent.BeforeShow(this.descriptor, this.GetSelections());
							base.prefabComponent.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HighlightBuild));
							this.BeforeShow(this.descriptor);
						}
					}
				}
				build = null;
			}
			base.HandleMenuOption(menuOption);
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x001313A4 File Offset: 0x0012F5A4
		public override void BeforeShow(EmbarkBuilderModuleWindowDescriptor descriptor)
		{
			base.prefabComponent.onSelected.RemoveAllListeners();
			base.prefabComponent.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.onSelect));
			base.prefabComponent.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HighlightBuild));
			base.prefabComponent.BeforeShow(descriptor, this.GetSelections());
			base.prefabComponent.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HighlightBuild));
			this.HighlightBuild(base.prefabComponent.scrollContext.data[base.prefabComponent.selectedPosition]);
			base.BeforeShow(descriptor);
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x00131454 File Offset: 0x0012F654
		public override IEnumerable<MenuOption> GetKeyLegend()
		{
			foreach (MenuOption menuOption in base.GetKeyLegend())
			{
				yield return menuOption;
			}
			IEnumerator<MenuOption> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x00131464 File Offset: 0x0012F664
		public void HighlightBuild(FrameworkDataElement dataElement)
		{
			this.selected = ((dataElement != null) ? dataElement.Id : null);
			bool? flag;
			if (dataElement == null)
			{
				flag = null;
			}
			else
			{
				string id = dataElement.Id;
				flag = ((id != null) ? new bool?(id.Contains("code:")) : null);
			}
			bool? flag2 = flag;
			this.hasOptions = flag2.GetValueOrDefault();
			base.GetOverlayWindow().UpdateMenuBars(base.GetOverlayWindow().currentWindowDescriptor);
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x001314D9 File Offset: 0x0012F6D9
		public override IEnumerable<MenuOption> GetKeyMenuBar()
		{
			if (this.hasOptions)
			{
				yield return new MenuOption
				{
					Id = "CmdChargenItemOptions",
					InputCommand = "CmdChargenItemOptions",
					KeyDescription = ControlManager.getCommandInputDescription("CmdChargenItemOptions", true, false, ControlManager.InputDeviceType.Unknown, false),
					Description = "Options"
				};
			}
			foreach (MenuOption menuOption in base.GetKeyMenuBar())
			{
				yield return menuOption;
			}
			IEnumerator<MenuOption> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x001314EC File Offset: 0x0012F6EC
		public override UIBreadcrumb GetBreadcrumb()
		{
			return new UIBreadcrumb
			{
				Id = base.GetType().FullName,
				Title = "Build Library",
				IconPath = "Items/sw_bookshelf3.bmp",
				IconDetailColor = Color.clear,
				IconForegroundColor = The.Color.Gray
			};
		}

		// Token: 0x04001ECB RID: 7883
		private string selected;

		// Token: 0x04001ECC RID: 7884
		private bool hasOptions;
	}
}
