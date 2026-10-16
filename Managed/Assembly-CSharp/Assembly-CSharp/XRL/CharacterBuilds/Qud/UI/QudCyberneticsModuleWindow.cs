using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.Events;
using XRL.Rules;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;

namespace XRL.CharacterBuilds.Qud.UI
{
	// Token: 0x020005B8 RID: 1464
	[UIView("CharacterCreation:PickCybernetics", false, false, false, null, null, false, 0, false, NavCategory = "Chargen", UICanvas = "Chargen/PickCybernetics", UICanvasHost = 1)]
	public class QudCyberneticsModuleWindow : EmbarkBuilderModuleWindowPrefabBase<QudCyberneticsModule, CategoryMenusScroller>
	{
		// Token: 0x06003E63 RID: 15971 RVA: 0x00132108 File Offset: 0x00130308
		public void pickCybernetic(int n)
		{
			QudCyberneticsModuleData qudCyberneticsModuleData = new QudCyberneticsModuleData();
			QudCyberneticsModuleDataRow qudCyberneticsModuleDataRow = new QudCyberneticsModuleDataRow();
			qudCyberneticsModuleDataRow.Count = 1;
			QudCyberneticsModuleDataRow qudCyberneticsModuleDataRow2 = qudCyberneticsModuleDataRow;
			GameObjectBlueprint blueprint = base.module.cybernetics[n].blueprint;
			qudCyberneticsModuleDataRow2.Cybernetic = ((blueprint != null) ? blueprint.Name : null);
			qudCyberneticsModuleDataRow.Variant = base.module.cybernetics[n].slot;
			qudCyberneticsModuleData.selections.Add(qudCyberneticsModuleDataRow);
			base.module.setData(qudCyberneticsModuleData);
			this.UpdateControls();
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x0013218C File Offset: 0x0013038C
		public override void BeforeShow(EmbarkBuilderModuleWindowDescriptor descriptor)
		{
			if (descriptor != null)
			{
				this.windowDescriptor = descriptor;
			}
			if (base.module.data == null)
			{
				base.module.setData(new QudCyberneticsModuleData());
			}
			base.prefabComponent.onSelected.RemoveAllListeners();
			base.prefabComponent.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.SelectCyber));
			this.UpdateControls();
			base.BeforeShow(descriptor);
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x001321FC File Offset: 0x001303FC
		public void SelectCyber(FrameworkDataElement dataElement)
		{
			int num = this.cyberneticsMenuState[0].menuOptions.FindIndex((PrefixMenuOption d) => d == dataElement);
			this.pickCybernetic(num);
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x00132240 File Offset: 0x00130440
		public void UpdateControls()
		{
			this.cyberneticsMenuState = new List<CategoryMenuData>();
			CategoryMenuData categoryMenuData = new CategoryMenuData();
			this.cyberneticsMenuState.Add(categoryMenuData);
			categoryMenuData.Title = "Cybernetics";
			categoryMenuData.menuOptions = new List<PrefixMenuOption>();
			for (int i = 0; i < base.module.cybernetics.Count; i++)
			{
				PrefixMenuOption prefixMenuOption = new PrefixMenuOption
				{
					Prefix = (base.module.IsSelected(base.module.cybernetics[i]) ? "[■]" : "[ ]"),
					Description = base.module.cybernetics[i].GetDescription(),
					LongDescription = base.module.cybernetics[i].GetLongDescription(),
					Renderable = base.module.cybernetics[i].GetRenderable()
				};
				categoryMenuData.menuOptions.Add(prefixMenuOption);
			}
			if (!base.module.builder.SkippingUIUpdates)
			{
				base.prefabComponent.BeforeShow(this.windowDescriptor, this.cyberneticsMenuState);
			}
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x00132369 File Offset: 0x00130569
		public override void ResetSelection()
		{
			base.module.setData(new QudCyberneticsModuleData());
			this.UpdateControls();
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x00132384 File Offset: 0x00130584
		public override void RandomSelectionNoUI()
		{
			int num = Stat.Roll(0, base.module.cybernetics.Count - 1);
			this.pickCybernetic(num);
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x001323B4 File Offset: 0x001305B4
		public override void RandomSelection()
		{
			int num = Stat.Roll(0, base.module.cybernetics.Count - 1);
			base.prefabComponent.ContextFor(0, num).ActivateAndEnable();
			this.pickCybernetic(num);
			this.UpdateControls();
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x001323FA File Offset: 0x001305FA
		public override global::UnityEngine.GameObject InstantiatePrefab(global::UnityEngine.GameObject prefab)
		{
			prefab.GetComponentInChildren<CategoryMenusScroller>().allowVerticalLayout = false;
			return base.InstantiatePrefab(prefab);
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x00132410 File Offset: 0x00130610
		public override UIBreadcrumb GetBreadcrumb()
		{
			QudCyberneticsModule module = base.module;
			QudCyberneticsModule.CyberneticsChoice? cyberneticsChoice = ((module != null) ? new QudCyberneticsModule.CyberneticsChoice?(module.SelectedChoice()) : null);
			Renderable renderable = ((cyberneticsChoice != null) ? cyberneticsChoice.GetValueOrDefault().GetRenderable() : null);
			return new UIBreadcrumb
			{
				Id = base.GetType().FullName,
				Title = "Cybernetics",
				IconPath = (((renderable != null) ? renderable.getTile() : null) ?? "Terrain/sw_cyberterminal.bmp"),
				IconDetailColor = ConsoleLib.Console.ColorUtility.ColorMap[(renderable != null) ? renderable.getColorChars().detail : 'W'],
				IconForegroundColor = ConsoleLib.Console.ColorUtility.ColorMap[(renderable != null) ? renderable.getColorChars().foreground : 'w']
			};
		}

		// Token: 0x04001ED0 RID: 7888
		public EmbarkBuilderModuleWindowDescriptor windowDescriptor;

		// Token: 0x04001ED1 RID: 7889
		protected const string ID_SEP = "::";

		// Token: 0x04001ED2 RID: 7890
		protected const string EMPTY_CHECK = "[ ]";

		// Token: 0x04001ED3 RID: 7891
		protected const string CHECKED = "[■]";

		// Token: 0x04001ED4 RID: 7892
		private List<CategoryMenuData> cyberneticsMenuState = new List<CategoryMenuData>();
	}
}
