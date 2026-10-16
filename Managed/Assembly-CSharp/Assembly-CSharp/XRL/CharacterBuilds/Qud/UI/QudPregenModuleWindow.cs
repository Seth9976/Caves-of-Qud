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
	// Token: 0x020005BC RID: 1468
	[UIView("CharacterCreation:Pregen", false, false, false, null, null, false, 0, false, NavCategory = "Chargen", UICanvas = "Chargen/PickSubtype", UICanvasHost = 1)]
	public class QudPregenModuleWindow : EmbarkBuilderModuleWindowPrefabBase<QudPregenModule, HorizontalScroller>
	{
		// Token: 0x06003E99 RID: 16025 RVA: 0x00133761 File Offset: 0x00131961
		public override void RandomSelection()
		{
			base.prefabComponent.scrollContext.GetContextAt(Stat.Random(0, base.prefabComponent.choices.Count - 1)).ActivateAndEnable();
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x00133791 File Offset: 0x00131991
		public IEnumerable<ChoiceWithColorIcon> GetSelections()
		{
			string genotype = base.GetModule<QudGenotypeModule>().data.Genotype;
			IEnumerable<QudPregenModule.QudPregenData> enumerable = base.module.pregens.Values.Where<QudPregenModule.QudPregenData>((QudPregenModule.QudPregenData p) => p.Genotype == genotype);
			foreach (QudPregenModule.QudPregenData qudPregenData in enumerable)
			{
				yield return new ChoiceWithColorIcon
				{
					Id = qudPregenData.Name,
					Title = qudPregenData.Name,
					IconPath = qudPregenData.Tile,
					HFlip = true,
					IconDetailColor = ConsoleLib.Console.ColorUtility.ColorMap[qudPregenData.Detail[0]],
					IconForegroundColor = ConsoleLib.Console.ColorUtility.ColorMap[qudPregenData.Foreground[0]],
					Description = qudPregenData.Description
				};
			}
			IEnumerator<QudPregenModule.QudPregenData> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x001337A1 File Offset: 0x001319A1
		public void onSelectPregen(FrameworkDataElement choice)
		{
			if (!TutorialManager.AllowSelectPregen(choice.Id))
			{
				return;
			}
			base.module.SelectPregen(choice.Id);
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x001337C4 File Offset: 0x001319C4
		public override void BeforeShow(EmbarkBuilderModuleWindowDescriptor descriptor)
		{
			base.prefabComponent.autoHotkey = true;
			base.prefabComponent.onSelected.RemoveAllListeners();
			base.prefabComponent.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.onSelectPregen));
			base.prefabComponent.BeforeShow(descriptor, this.GetSelections());
			base.BeforeShow(descriptor);
		}

		// Token: 0x06003E9D RID: 16029 RVA: 0x00133822 File Offset: 0x00131A22
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

		// Token: 0x06003E9E RID: 16030 RVA: 0x00133834 File Offset: 0x00131A34
		public override UIBreadcrumb GetBreadcrumb()
		{
			UIBreadcrumb uibreadcrumb = new UIBreadcrumb();
			uibreadcrumb.Id = base.GetType().FullName;
			uibreadcrumb.Title = "Pregens";
			QudPregenModule module = base.module;
			bool flag;
			if (module == null)
			{
				flag = null != null;
			}
			else
			{
				QudPregenModuleData data = module.data;
				flag = ((data != null) ? data.Pregen : null) != null;
			}
			string text2;
			if (flag)
			{
				QudPregenModule module2 = base.module;
				bool flag2;
				if (module2 == null)
				{
					flag2 = false;
				}
				else
				{
					Dictionary<string, QudPregenModule.QudPregenData> pregens = module2.pregens;
					QudPregenModule module3 = base.module;
					string text;
					if (module3 == null)
					{
						text = null;
					}
					else
					{
						QudPregenModuleData data2 = module3.data;
						text = ((data2 != null) ? data2.Pregen : null);
					}
					flag2 = pregens.ContainsKey(text);
				}
				if (flag2)
				{
					QudPregenModule module4 = base.module;
					if (module4 == null)
					{
						text2 = null;
						goto IL_00C1;
					}
					Dictionary<string, QudPregenModule.QudPregenData> pregens2 = module4.pregens;
					QudPregenModule module5 = base.module;
					string text3;
					if (module5 == null)
					{
						text3 = null;
					}
					else
					{
						QudPregenModuleData data3 = module5.data;
						text3 = ((data3 != null) ? data3.Pregen : null);
					}
					QudPregenModule.QudPregenData qudPregenData = pregens2[text3];
					text2 = ((qudPregenData != null) ? qudPregenData.Tile : null);
					goto IL_00C1;
				}
			}
			text2 = UIBreadcrumb.DEFAULT_ICON;
			IL_00C1:
			uibreadcrumb.IconPath = text2 ?? UIBreadcrumb.DEFAULT_ICON;
			uibreadcrumb.HFlip = true;
			uibreadcrumb.IconDetailColor = Color.clear;
			uibreadcrumb.IconForegroundColor = The.Color.Gray;
			return uibreadcrumb;
		}
	}
}
