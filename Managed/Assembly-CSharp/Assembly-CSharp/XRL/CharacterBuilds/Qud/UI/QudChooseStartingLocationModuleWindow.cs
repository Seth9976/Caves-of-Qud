using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using XRL.Rules;
using XRL.UI;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds.Qud.UI
{
	// Token: 0x020005B6 RID: 1462
	[UIView("CharacterCreation:ChooseStartingLocation", false, false, false, null, null, false, 0, false, NavCategory = "Chargen", UICanvas = "Chargen/ChooseStartingLocation", UICanvasHost = 1)]
	public class QudChooseStartingLocationModuleWindow : EmbarkBuilderModuleWindowPrefabBase<QudChooseStartingLocationModule, HorizontalScroller>
	{
		// Token: 0x06003E47 RID: 15943 RVA: 0x00131937 File Offset: 0x0012FB37
		public IEnumerable<StartingLocationData> GetSelections()
		{
			foreach (StartingLocationData startingLocationData in base.module.startingLocations.Values)
			{
				yield return startingLocationData;
			}
			Dictionary<string, StartingLocationData>.ValueCollection.Enumerator enumerator = default(Dictionary<string, StartingLocationData>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x00131947 File Offset: 0x0012FB47
		public void onNext()
		{
			base.module.onNext();
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x00131954 File Offset: 0x0012FB54
		public void onBack()
		{
			base.module.onBack();
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x00131961 File Offset: 0x0012FB61
		public void onHighlightStartingLocation(FrameworkDataElement element)
		{
			base.module.data.StartingLocation = element.Id;
			base.module.setData(base.module.data);
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x0013198F File Offset: 0x0012FB8F
		public void onSelectStartingLocation(FrameworkDataElement element)
		{
			base.module.data.StartingLocation = element.Id;
			base.module.setData(base.module.data);
			this.onNext();
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x001319C3 File Offset: 0x0012FBC3
		public override void ResetSelection()
		{
			base.module.setData(new QudChooseStartingLocationModuleData((from s in this.GetSelections()
				where s.Set == this.currentSet
				select s).FirstOrDefault<StartingLocationData>().Id));
			base.prefabComponent.selectedPosition = 0;
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x00131A02 File Offset: 0x0012FC02
		public override void DebugQuickstart(string type)
		{
			base.module.data.StartingLocation = "Joppa";
			base.module.setData(base.module.data);
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x00131A30 File Offset: 0x0012FC30
		public override void RandomSelectionNoUI()
		{
			int num = Stat.Random(0, base.prefabComponent.choices.Count - 1);
			base.module.data.StartingLocation = base.prefabComponent.choices[num].Id;
			base.module.setData(base.module.data);
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x00131A94 File Offset: 0x0012FC94
		public override void RandomSelection()
		{
			int num = Stat.Random(0, base.prefabComponent.choices.Count - 1);
			base.prefabComponent.scrollContext.GetContextAt(num).ActivateAndEnable();
			base.module.data.StartingLocation = base.prefabComponent.choices[num].Id;
			base.module.setData(base.module.data);
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x00131B10 File Offset: 0x0012FD10
		public override void Init()
		{
			base.prefabComponent.selectionPrefab = ((GameObject)Resources.Load("Prefabs/StartingLocationButton")).GetComponent<FrameworkUnityScrollChild>();
			base.prefabComponent.gridLayout.cellSize = new Vector2(200f, 225f);
			base.prefabComponent.gridLayout.spacing = new Vector2(20f, 20f);
			base.Init();
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x00131B80 File Offset: 0x0012FD80
		public override void BeforeShow(EmbarkBuilderModuleWindowDescriptor descriptor)
		{
			base.module.data = new QudChooseStartingLocationModuleData();
			base.module.data.StartingLocation = "Joppa";
			base.prefabComponent.autoHotkey = true;
			base.prefabComponent.scrollContext.wraps = false;
			base.prefabComponent.onSelected.RemoveAllListeners();
			base.prefabComponent.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.onSelectStartingLocation));
			base.prefabComponent.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.onHighlightStartingLocation));
			QudChooseStartingLocationModule module = base.module;
			object obj;
			if (module == null)
			{
				obj = null;
			}
			else
			{
				EmbarkBuilder builder = module.builder;
				obj = ((builder != null) ? builder.handleUIEvent(QudChooseStartingLocationModuleWindow.EventNames.EID_GET_STARTING_LOCATION_SET, null) : null);
			}
			this.currentSet = obj as string;
			base.prefabComponent.BeforeShow(descriptor, from s in this.GetSelections()
				where s.Set == this.currentSet
				select s);
			base.BeforeShow(descriptor);
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x00131C70 File Offset: 0x0012FE70
		public override UIBreadcrumb GetBreadcrumb()
		{
			UIBreadcrumb uibreadcrumb = new UIBreadcrumb
			{
				Id = "StartingLocation",
				Title = "Select Starting Location",
				IconPath = UIBreadcrumb.DEFAULT_ICON,
				IconDetailColor = Color.clear,
				IconForegroundColor = The.Color.Gray
			};
			StartingLocationData startingLocation = base.module.startingLocation;
			if (startingLocation != null)
			{
				uibreadcrumb.Title = startingLocation.Name.Split('\n', StringSplitOptions.None)[0].Strip();
				uibreadcrumb.IconPath = startingLocation.grid["21"].Tile;
				uibreadcrumb.IconDetailColor = The.Color[startingLocation.grid["21"].Detail[0]];
				uibreadcrumb.IconForegroundColor = The.Color[startingLocation.grid["21"].Foreground[0]];
			}
			return uibreadcrumb;
		}

		// Token: 0x04001ECD RID: 7885
		public string currentSet;

		// Token: 0x020019B3 RID: 6579
		public static class EventNames
		{
			// Token: 0x040066C9 RID: 26313
			public static readonly string EID_GET_STARTING_LOCATION_SET = "QudChooseStartingLocationModuleGetStartingLocationSet";
		}
	}
}
