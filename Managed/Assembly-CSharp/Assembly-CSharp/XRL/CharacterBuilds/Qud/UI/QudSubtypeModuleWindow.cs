using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds.Qud.UI
{
	// Token: 0x020005BD RID: 1469
	[UIView("CharacterCreation:Subtype", false, false, false, null, null, false, 0, false, NavCategory = "Chargen", UICanvas = "Chargen/PickSubtype", UICanvasHost = 1)]
	public class QudSubtypeModuleWindow : EmbarkBuilderModuleWindowPrefabBase<QudSubtypeModule, HorizontalScroller>
	{
		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06003EA1 RID: 16033 RVA: 0x00133942 File Offset: 0x00131B42
		public override bool isWindowEnabled
		{
			get
			{
				return !base.module.hasCategories();
			}
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x00133952 File Offset: 0x00131B52
		public override void RandomSelectionNoUI()
		{
			base.module.SelectSubtype(base.module.getAvailableSelections().GetRandomElement<SubtypeEntry>(null).Name);
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x00133978 File Offset: 0x00131B78
		public override void RandomSelection()
		{
			int num = Stat.Random(0, base.prefabComponent.choices.Count - 1);
			base.prefabComponent.scrollContext.GetContextAt(num).ActivateAndEnable();
			base.module.SelectSubtype(base.prefabComponent.choices[num].Id);
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x001339D6 File Offset: 0x00131BD6
		public void onSelectSubtype(FrameworkDataElement choice)
		{
			base.module.SelectSubtype(choice.Id);
			base.module.builder.advance(false, false, false);
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x001339FC File Offset: 0x00131BFC
		public string getSubtypeTitle()
		{
			SubtypeEntry subtypeEntry = base.module.getAvailableSelections().FirstOrDefault<SubtypeEntry>();
			return ((subtypeEntry != null) ? subtypeEntry.SubtypeClass.ChargenTitle : null) ?? "choose subtype";
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x00133A28 File Offset: 0x00131C28
		public string getSubtypeSingularTitle()
		{
			SubtypeEntry subtypeEntry = base.module.getAvailableSelections().FirstOrDefault<SubtypeEntry>();
			return ((subtypeEntry != null) ? subtypeEntry.SubtypeClass.SingluarTitle : null) ?? "subtype";
		}

		// Token: 0x06003EA7 RID: 16039 RVA: 0x00133A54 File Offset: 0x00131C54
		public virtual IEnumerable<FrameworkDataElement> GetSelections()
		{
			return base.module.GetSelections();
		}

		// Token: 0x06003EA8 RID: 16040 RVA: 0x00133A64 File Offset: 0x00131C64
		public override void BeforeShow(EmbarkBuilderModuleWindowDescriptor descriptor)
		{
			base.prefabComponent.autoHotkey = true;
			base.prefabComponent.onSelected.RemoveAllListeners();
			base.prefabComponent.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.onSelectSubtype));
			HorizontalScrollerScroller scrollscroller = base.prefabComponent as HorizontalScrollerScroller;
			if (scrollscroller != null)
			{
				scrollscroller.onHighlight.RemoveAllListeners();
				scrollscroller.onHighlight.AddListener(delegate(FrameworkDataElement a)
				{
					scrollscroller.UpdateHighlightText();
				});
			}
			base.prefabComponent.BeforeShow(descriptor, this.GetSelections());
			base.prefabComponent.titleText.SetText(":" + this.getSubtypeTitle() + ":");
			base.BeforeShow(descriptor);
		}

		// Token: 0x06003EA9 RID: 16041 RVA: 0x00133B39 File Offset: 0x00131D39
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

		// Token: 0x06003EAA RID: 16042 RVA: 0x00133B4C File Offset: 0x00131D4C
		public override UIBreadcrumb GetBreadcrumb()
		{
			UIBreadcrumb uibreadcrumb = new UIBreadcrumb
			{
				Id = base.GetType().FullName,
				Title = Grammar.InitCapWithFormatting(this.getSubtypeSingularTitle()),
				IconPath = UIBreadcrumb.DEFAULT_ICON,
				HFlip = true,
				IconDetailColor = Color.clear,
				IconForegroundColor = The.Color.Gray
			};
			foreach (SubtypeEntry subtypeEntry in base.module.subtypes)
			{
				if (subtypeEntry.Name == base.module.getSelected())
				{
					uibreadcrumb.Title = subtypeEntry.DisplayName;
					uibreadcrumb.IconPath = subtypeEntry.Tile;
					uibreadcrumb.IconDetailColor = The.Color.DarkBlue;
					uibreadcrumb.IconForegroundColor = The.Color.Gray;
					break;
				}
			}
			return uibreadcrumb;
		}
	}
}
