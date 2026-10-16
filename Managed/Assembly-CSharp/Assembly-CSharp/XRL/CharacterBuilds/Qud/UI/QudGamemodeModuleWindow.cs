using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.Events;
using XRL.Rules;
using XRL.UI;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds.Qud.UI
{
	// Token: 0x020005B9 RID: 1465
	[UIView("CharacterCreation:Modes", false, false, false, null, null, false, 0, false, NavCategory = "Chargen", UICanvas = "Chargen/Modes", UICanvasHost = 1)]
	public class QudGamemodeModuleWindow : EmbarkBuilderModuleWindowPrefabBase<QudGamemodeModule, HorizontalScroller>
	{
		// Token: 0x06003E6D RID: 15981 RVA: 0x001324EC File Offset: 0x001306EC
		public IEnumerable<ChoiceWithColorIcon> GetSelections()
		{
			foreach (QudGamemodeModule.GameModeDescriptor gameModeDescriptor in base.module.GameModes.Values)
			{
				ChoiceWithColorIcon choiceWithColorIcon = new ChoiceWithColorIcon
				{
					Id = gameModeDescriptor.ID,
					Title = gameModeDescriptor.Title,
					IconPath = gameModeDescriptor.IconTile,
					IconDetailColor = ConsoleLib.Console.ColorUtility.ColorMap[gameModeDescriptor.IconDetail[0]],
					IconForegroundColor = ConsoleLib.Console.ColorUtility.ColorMap[gameModeDescriptor.IconForeground[0]],
					Description = gameModeDescriptor.Description.Replace("{day_of_year}", DateTime.Now.DayOfYear.ToString()).Replace("{year}", DateTime.Now.Year.ToString()),
					Chosen = new Predicate<ChoiceWithColorIcon>(this.IsChoiceSelected)
				};
				yield return choiceWithColorIcon;
			}
			Dictionary<string, QudGamemodeModule.GameModeDescriptor>.ValueCollection.Enumerator enumerator = default(Dictionary<string, QudGamemodeModule.GameModeDescriptor>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x001324FC File Offset: 0x001306FC
		public bool IsChoiceSelected(ChoiceWithColorIcon choice)
		{
			return ((choice != null) ? choice.Id : null) == base.module.GetMode();
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x0013251C File Offset: 0x0013071C
		public override UIBreadcrumb GetBreadcrumb()
		{
			UIBreadcrumb uibreadcrumb = new UIBreadcrumb
			{
				Id = "Gamemode",
				Title = "Choose Game Mode",
				IconPath = UIBreadcrumb.DEFAULT_ICON,
				IconDetailColor = Color.clear,
				IconForegroundColor = The.Color.Gray
			};
			foreach (QudGamemodeModule.GameModeDescriptor gameModeDescriptor in base.module.GameModes.Values)
			{
				if (gameModeDescriptor.ID == base.module.GetMode())
				{
					uibreadcrumb.Title = gameModeDescriptor.Title;
					uibreadcrumb.IconPath = gameModeDescriptor.IconTile;
					uibreadcrumb.IconDetailColor = ConsoleLib.Console.ColorUtility.ColorMap[gameModeDescriptor.IconDetail[0]];
					uibreadcrumb.IconForegroundColor = ConsoleLib.Console.ColorUtility.ColorMap[gameModeDescriptor.IconForeground[0]];
					break;
				}
			}
			return uibreadcrumb;
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x00132620 File Offset: 0x00130820
		public override IEnumerable<MenuOption> GetKeyMenuBar()
		{
			foreach (MenuOption menuOption in base.GetKeyMenuBar())
			{
				yield return menuOption;
			}
			IEnumerator<MenuOption> enumerator = null;
			if (Options.ShowQuickstartOption)
			{
				yield return QudGamemodeModuleWindow.QUICKSTART;
			}
			yield break;
			yield break;
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x00132630 File Offset: 0x00130830
		public override void RandomSelection()
		{
			base.prefabComponent.scrollContext.GetContextAt(Stat.Random(0, base.prefabComponent.choices.Count - 1)).ActivateAndEnable();
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x00132660 File Offset: 0x00130860
		public void ChoiceSelected(FrameworkDataElement data)
		{
			base.module.SelectMode(data.Id);
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x00132673 File Offset: 0x00130873
		public override void HandleMenuOption(MenuOption menuOption)
		{
			base.HandleMenuOption(menuOption);
			if (menuOption == QudGamemodeModuleWindow.QUICKSTART)
			{
				base.module.SelectMode(menuOption.Id);
			}
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x00132698 File Offset: 0x00130898
		public override void BeforeShow(EmbarkBuilderModuleWindowDescriptor descriptor)
		{
			TutorialManager.EndTutorial();
			base.prefabComponent.autoHotkey = true;
			base.prefabComponent.onSelected.RemoveAllListeners();
			base.prefabComponent.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.ChoiceSelected));
			List<ChoiceWithColorIcon> list = this.GetSelections().ToList<ChoiceWithColorIcon>();
			if (!Options.ModernUI)
			{
				ChoiceWithColorIcon choiceWithColorIcon = list.Where<ChoiceWithColorIcon>((ChoiceWithColorIcon mode) => mode.Id == "Tutorial").FirstOrDefault<ChoiceWithColorIcon>();
				if (choiceWithColorIcon != null)
				{
					list.Remove(choiceWithColorIcon);
				}
			}
			else if (Options.GetOption("OptionTutorialComplete", "") == "Yes")
			{
				ChoiceWithColorIcon choiceWithColorIcon2 = list.Where<ChoiceWithColorIcon>((ChoiceWithColorIcon mode) => mode.Id == "Tutorial").FirstOrDefault<ChoiceWithColorIcon>();
				if (choiceWithColorIcon2 != null)
				{
					list.Remove(choiceWithColorIcon2);
					list.Add(choiceWithColorIcon2);
				}
			}
			base.prefabComponent.BeforeShow(descriptor, list);
			base.BeforeShow(descriptor);
		}

		// Token: 0x04001ED5 RID: 7893
		public static readonly MenuOption QUICKSTART = new MenuOption
		{
			Id = "_Quickstart",
			InputCommand = "CmdDebugQuickstart",
			KeyDescription = "Q",
			Description = "[Debug] Quickstart"
		};
	}
}
