using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using XRL.UI;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds.Qud.UI
{
	// Token: 0x020005B4 RID: 1460
	[UIView("CharacterCreation:BuildSummary", false, false, false, null, null, false, 0, false, NavCategory = "Chargen", UICanvas = "Chargen/BuildSummary", UICanvasHost = 1)]
	public class QudBuildSummaryModuleWindow : EmbarkBuilderModuleWindowPrefabBase<QudBuildSummaryModule, HorizontalScroller>
	{
		// Token: 0x06003E35 RID: 15925 RVA: 0x00131561 File Offset: 0x0012F761
		public IEnumerable<SummaryBlockData> GetSelections()
		{
			List<SummaryBlockData> list = new List<SummaryBlockData>();
			foreach (AbstractEmbarkBuilderModule abstractEmbarkBuilderModule in base.module.builder.enabledModules)
			{
				SummaryBlockData summaryBlock = abstractEmbarkBuilderModule.GetSummaryBlock();
				if (summaryBlock != null)
				{
					list.Add(summaryBlock);
				}
			}
			list.Sort((SummaryBlockData a, SummaryBlockData b) => a.SortOrder - b.SortOrder);
			foreach (SummaryBlockData summaryBlockData in list)
			{
				yield return summaryBlockData;
			}
			List<SummaryBlockData>.Enumerator enumerator2 = default(List<SummaryBlockData>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x00131574 File Offset: 0x0012F774
		public override void Init()
		{
			base.prefabComponent.selectionPrefab = ((GameObject)Resources.Load("Prefabs/SummaryTextColumn")).GetComponent<FrameworkUnityScrollChild>();
			base.prefabComponent.gridLayout.cellSize = new Vector2(300f, 200f);
			base.prefabComponent.gridLayout.spacing = new Vector2(20f, 20f);
			UITextSkin descriptionText = base.prefabComponent.descriptionText;
			if (descriptionText != null)
			{
				descriptionText.gameObject.DestroyImmediate();
			}
			base.prefabComponent.descriptionText = null;
			base.Init();
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x0013160B File Offset: 0x0012F80B
		public override void BeforeShow(EmbarkBuilderModuleWindowDescriptor descriptor)
		{
			base.prefabComponent.scrollContext.wraps = false;
			base.prefabComponent.onSelected.RemoveAllListeners();
			base.prefabComponent.BeforeShow(descriptor, this.GetSelections());
			base.BeforeShow(descriptor);
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x00131647 File Offset: 0x0012F847
		public override void AfterShow(EmbarkBuilderModuleWindowDescriptor descriptor)
		{
			base.GetOverlayWindow().nextButton.navigationContext.ActivateAndEnable();
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x00131660 File Offset: 0x0012F860
		public override UIBreadcrumb GetBreadcrumb()
		{
			return new UIBreadcrumb
			{
				Id = base.GetType().FullName,
				Title = "Summary",
				IconPath = "Items/sw_unfurled_scroll1.bmp",
				IconDetailColor = Color.clear,
				IconForegroundColor = The.Color.Gray
			};
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x001316B4 File Offset: 0x0012F8B4
		public override IEnumerable<MenuOption> GetKeyMenuBar()
		{
			if (base.module.builder.GetModule<QudChartypeModule>().data.type == "Random")
			{
				yield return new MenuOption
				{
					Id = AbstractBuilderModuleWindowBase.RANDOM,
					InputCommand = "CmdChargenRandom",
					KeyDescription = ControlManager.getCommandInputDescription("CmdChargenRandom", true, false, ControlManager.InputDeviceType.Unknown, false),
					Description = "Re-Randomize Selections"
				};
			}
			if (CapabilityManager.AllowClipboardFunctionality)
			{
				yield return new MenuOption
				{
					Id = "CmdExportCode",
					InputCommand = "CmdExportCode",
					KeyDescription = "None",
					Description = "Export Code to Clipboard"
				};
			}
			yield return new MenuOption
			{
				Id = "CmdSaveBuildToLibrary",
				InputCommand = "CmdSaveBuildToLibrary",
				KeyDescription = "None",
				Description = "Save Build To Library"
			};
			yield break;
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x001316C4 File Offset: 0x0012F8C4
		public override async void HandleMenuOption(MenuOption menuOption)
		{
			if (menuOption.Id == "CmdSaveBuildToLibrary")
			{
				await QudBuildLibraryModuleWindow.AddBuild(base.module.builder.generateCode());
			}
			if (menuOption.Id == "CmdExportCode")
			{
				ClipboardHelper.SetClipboardData(base.module.builder.generateCode());
				Popup.NewPopupMessageAsync("Build code copied to clipboard.", null, null, null, null, 0, null, null, null, true, true, false, default(CancellationToken), false, "", null, null, null);
			}
			else if (menuOption.Id == AbstractBuilderModuleWindowBase.RANDOM)
			{
				base.module.builder.ShowWindow<QudChartypeModuleWindow>();
				base.module.builder.SkippingUIUpdates = true;
				try
				{
					base.module.builder.GetModule<QudChartypeModule>().selectType("Random");
				}
				finally
				{
					base.module.builder.SkippingUIUpdates = false;
				}
			}
			else
			{
				base.HandleMenuOption(menuOption);
			}
		}
	}
}
