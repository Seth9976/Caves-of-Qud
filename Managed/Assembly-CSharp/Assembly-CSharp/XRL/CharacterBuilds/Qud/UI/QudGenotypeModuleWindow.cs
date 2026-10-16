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
	// Token: 0x020005BA RID: 1466
	[UIView("CharacterCreation:Mutations", false, false, false, null, null, false, 0, false, NavCategory = "Chargen", UICanvas = "Chargen/PickMutations", UICanvasHost = 1)]
	public class QudGenotypeModuleWindow : EmbarkBuilderModuleWindowPrefabBase<QudGenotypeModule, HorizontalScroller>
	{
		// Token: 0x06003E78 RID: 15992 RVA: 0x001327E4 File Offset: 0x001309E4
		public override void RandomSelection()
		{
			int num = Stat.Random(0, base.prefabComponent.choices.Count - 1);
			base.prefabComponent.scrollContext.GetContextAt(num).ActivateAndEnable();
			base.module.SelectGenotype(base.prefabComponent.choices[num].Id);
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x00132842 File Offset: 0x00130A42
		public virtual IEnumerable<ChoiceWithColorIcon> GetSelections()
		{
			foreach (GenotypeEntry genotypeEntry in base.module.genotypes)
			{
				yield return new ChoiceWithColorIcon
				{
					Id = genotypeEntry.Name,
					Title = genotypeEntry.DisplayName,
					IconPath = genotypeEntry.Tile,
					IconDetailColor = (genotypeEntry.DetailColor.IsNullOrEmpty() ? The.Color.Brown : ConsoleLib.Console.ColorUtility.ColorMap[genotypeEntry.DetailColor[0]]),
					IconForegroundColor = The.Color.Gray,
					Description = genotypeEntry.GetFlatChargenInfo(),
					Chosen = new Predicate<ChoiceWithColorIcon>(this.IsChoiceSelected)
				};
			}
			List<GenotypeEntry>.Enumerator enumerator = default(List<GenotypeEntry>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x00132852 File Offset: 0x00130A52
		public bool IsChoiceSelected(ChoiceWithColorIcon choice)
		{
			return base.module.getSelected() == ((choice != null) ? choice.Id : null);
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x00132870 File Offset: 0x00130A70
		public override UIBreadcrumb GetBreadcrumb()
		{
			UIBreadcrumb uibreadcrumb = new UIBreadcrumb
			{
				Id = base.GetType().FullName,
				Title = "Choose Genotype",
				IconPath = UIBreadcrumb.DEFAULT_ICON,
				IconDetailColor = Color.clear,
				IconForegroundColor = The.Color.Gray
			};
			foreach (GenotypeEntry genotypeEntry in base.module.genotypes)
			{
				if (genotypeEntry.Name == base.module.getSelected())
				{
					uibreadcrumb.Title = genotypeEntry.DisplayName;
					uibreadcrumb.IconPath = genotypeEntry.Tile;
					uibreadcrumb.IconDetailColor = The.Color.DarkBlue;
					uibreadcrumb.IconForegroundColor = The.Color.Gray;
					break;
				}
			}
			return uibreadcrumb;
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x0013295C File Offset: 0x00130B5C
		public void onSelectGenotype(FrameworkDataElement choice)
		{
			if (!TutorialManager.AllowSelectGenotype((choice != null) ? choice.Id : null))
			{
				return;
			}
			base.module.SelectGenotype(choice.Id);
			base.module.builder.advance(false, false, false);
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x00132998 File Offset: 0x00130B98
		public override void BeforeShow(EmbarkBuilderModuleWindowDescriptor descriptor)
		{
			base.prefabComponent.autoHotkey = true;
			base.prefabComponent.scrollContext.wraps = false;
			base.prefabComponent.onSelected.RemoveAllListeners();
			base.prefabComponent.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.onSelectGenotype));
			base.prefabComponent.BeforeShow(descriptor, this.GetSelections());
			base.BeforeShow(descriptor);
		}
	}
}
