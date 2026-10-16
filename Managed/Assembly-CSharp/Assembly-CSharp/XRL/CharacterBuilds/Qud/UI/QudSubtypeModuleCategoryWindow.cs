using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using XRL.Rules;
using XRL.UI;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds.Qud.UI
{
	// Token: 0x020005BE RID: 1470
	[UIView("CharacterCreation:SubtypeCategory", false, false, false, null, null, false, 0, false, NavCategory = "Chargen", UICanvas = "Chargen/PickSubtypeCategory", UICanvasHost = 1)]
	public class QudSubtypeModuleCategoryWindow : QudSubtypeModuleWindow
	{
		// Token: 0x06003EAD RID: 16045 RVA: 0x00133C58 File Offset: 0x00131E58
		public override void RandomSelection()
		{
			var list = this.GetSelections().SelectMany((FrameworkDataElement cat, int catn) => (cat as IFrameworkDataList).getChildren().Select((FrameworkDataElement choice, int choicen) => new { catn, cat, choice, choicen })).ToList();
			int num = Stat.Random(0, list.Count - 1);
			var anon = list[num];
			base.prefabComponent.GetPrefabForIndex(anon.catn).GetComponent<FrameworkScroller>().scrollContext.GetContextAt(anon.choicen).ActivateAndEnable();
			base.module.SelectSubtype(anon.choice.Id);
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06003EAE RID: 16046 RVA: 0x00133CEE File Offset: 0x00131EEE
		public override bool isWindowEnabled
		{
			get
			{
				return base.module.hasCategories();
			}
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x00133CFB File Offset: 0x00131EFB
		public override IEnumerable<FrameworkDataElement> GetSelections()
		{
			return base.module.GetSelectionCategories();
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x00133D08 File Offset: 0x00131F08
		public override void Init()
		{
			base.prefabComponent.selectionPrefab = ((GameObject)Resources.Load("Prefabs/CategoryIconScroller")).GetComponent<FrameworkUnityScrollChild>();
			base.prefabComponent.GetComponentInChildren<GridLayoutGroup>(true).cellSize = new Vector2(500f, 200f);
		}
	}
}
