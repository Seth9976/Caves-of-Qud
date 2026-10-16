using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.Events;
using XRL.Rules;
using XRL.UI;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds.Qud.UI
{
	// Token: 0x020005B5 RID: 1461
	[UIView("CharacterCreation:Chartype", false, false, false, null, null, false, 0, false, NavCategory = "Chargen", UICanvas = "Chargen/Chartype", UICanvasHost = 1)]
	public class QudChartypeModuleWindow : EmbarkBuilderModuleWindowPrefabBase<QudChartypeModule, HorizontalScroller>
	{
		// Token: 0x06003E3E RID: 15934 RVA: 0x00131714 File Offset: 0x0012F914
		public IEnumerable<ChoiceWithColorIcon> GetSelections()
		{
			foreach (QudChartypeModule.GameTypeDescriptor gameTypeDescriptor in base.module.GameTypes.Values)
			{
				yield return new ChoiceWithColorIcon
				{
					Id = gameTypeDescriptor.ID,
					Title = gameTypeDescriptor.Title,
					IconPath = gameTypeDescriptor.IconTile,
					IconDetailColor = ConsoleLib.Console.ColorUtility.ColorMap[gameTypeDescriptor.IconDetail[0]],
					IconForegroundColor = ConsoleLib.Console.ColorUtility.ColorMap[gameTypeDescriptor.IconForeground[0]],
					Description = gameTypeDescriptor.Description,
					Chosen = new Predicate<ChoiceWithColorIcon>(this.IsChoiceSelected)
				};
			}
			Dictionary<string, QudChartypeModule.GameTypeDescriptor>.ValueCollection.Enumerator enumerator = default(Dictionary<string, QudChartypeModule.GameTypeDescriptor>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x00131724 File Offset: 0x0012F924
		public bool IsChoiceSelected(ChoiceWithColorIcon choice)
		{
			return base.module.getSelectedType() == ((choice != null) ? choice.Id : null);
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x00131744 File Offset: 0x0012F944
		public override UIBreadcrumb GetBreadcrumb()
		{
			UIBreadcrumb uibreadcrumb = new UIBreadcrumb
			{
				Id = "CharOption",
				Title = "Select Character Option",
				IconPath = UIBreadcrumb.DEFAULT_ICON,
				IconDetailColor = Color.clear,
				IconForegroundColor = The.Color.Gray
			};
			foreach (QudChartypeModule.GameTypeDescriptor gameTypeDescriptor in base.module.GameTypes.Values)
			{
				if (gameTypeDescriptor.ID == base.module.getSelectedType())
				{
					uibreadcrumb.Title = gameTypeDescriptor.Title;
					uibreadcrumb.IconPath = gameTypeDescriptor.IconTile;
					uibreadcrumb.IconDetailColor = ConsoleLib.Console.ColorUtility.ColorMap[gameTypeDescriptor.IconDetail[0]];
					uibreadcrumb.IconForegroundColor = ConsoleLib.Console.ColorUtility.ColorMap[gameTypeDescriptor.IconForeground[0]];
					break;
				}
			}
			return uibreadcrumb;
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x00131848 File Offset: 0x0012FA48
		public override void DebugQuickstart(string type)
		{
			base.module.selectType("Random");
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x0013185A File Offset: 0x0012FA5A
		public void ChoiceSelected(FrameworkDataElement choice)
		{
			base.module.selectType(choice.Id);
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x0013186D File Offset: 0x0012FA6D
		public override void RandomSelection()
		{
			base.prefabComponent.scrollContext.GetContextAt(Stat.Random(0, base.prefabComponent.choices.Count - 1)).ActivateAndEnable();
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x0013189D File Offset: 0x0012FA9D
		public override void DailySelection()
		{
			base.module.selectType("Daily");
			this._module.builder.RefreshActiveWindow();
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x001318C0 File Offset: 0x0012FAC0
		public override void BeforeShow(EmbarkBuilderModuleWindowDescriptor descriptor)
		{
			base.prefabComponent.autoHotkey = true;
			base.prefabComponent.scrollContext.wraps = false;
			base.prefabComponent.onSelected.RemoveAllListeners();
			base.prefabComponent.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.ChoiceSelected));
			base.prefabComponent.BeforeShow(descriptor, this.GetSelections());
			base.BeforeShow(descriptor);
		}
	}
}
