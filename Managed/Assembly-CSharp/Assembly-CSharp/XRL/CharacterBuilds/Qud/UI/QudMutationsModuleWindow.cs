using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ConsoleLib.Console;
using Qud.UI;
using UnityEngine;
using UnityEngine.Events;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World.Parts.Mutation;

namespace XRL.CharacterBuilds.Qud.UI
{
	// Token: 0x020005BB RID: 1467
	[UIView("CharacterCreation:PickMutations", false, false, false, null, null, false, 0, false, NavCategory = "Chargen", UICanvas = "Chargen/PickMutations", UICanvasHost = 1)]
	public class QudMutationsModuleWindow : EmbarkBuilderModuleWindowPrefabBase<QudMutationsModule, CategoryMenusScroller>
	{
		// Token: 0x06003E7F RID: 15999 RVA: 0x00132A10 File Offset: 0x00130C10
		public override void ResetSelection()
		{
			QudMutationsModuleData qudMutationsModuleData = new QudMutationsModuleData();
			qudMutationsModuleData.mp = (int)base.module.builder.handleUIEvent(QudMutationsModuleWindow.EID_GET_BASE_MP, 0);
			if (qudMutationsModuleData.mp == 0)
			{
				base.module.setData(null);
				return;
			}
			base.module.setData(qudMutationsModuleData);
			this.UpdateNodesFromData();
			this.UpdateControls();
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x00132A78 File Offset: 0x00130C78
		public void UpdateDataFromNodes()
		{
			QudMutationsModuleData qudMutationsModuleData = new QudMutationsModuleData();
			qudMutationsModuleData.mp = (int)base.module.builder.handleUIEvent(QudMutationsModuleWindow.EID_GET_BASE_MP, 0);
			foreach (QudMutationsModuleWindow.MLNode mlnode in this.mutationNodes)
			{
				if (mlnode.Selected > 0)
				{
					qudMutationsModuleData.mp -= mlnode.Selected * mlnode.Entry.Cost;
					QudMutationModuleDataRow qudMutationModuleDataRow = new QudMutationModuleDataRow();
					qudMutationModuleDataRow.Mutation = mlnode.Entry.Name;
					qudMutationModuleDataRow.Count = mlnode.Selected;
					qudMutationModuleDataRow.Variant = mlnode.Variant;
					qudMutationsModuleData.selections.Add(qudMutationModuleDataRow);
				}
			}
			base.module.setData(qudMutationsModuleData);
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x00132B60 File Offset: 0x00130D60
		public override GameObject InstantiatePrefab(GameObject prefab)
		{
			prefab.GetComponentInChildren<CategoryMenusScroller>().allowVerticalLayout = false;
			return base.InstantiatePrefab(prefab);
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x00132B78 File Offset: 0x00130D78
		public void UpdateNodesFromData()
		{
			this.ClearNodes();
			int num = 0;
			using (List<QudMutationModuleDataRow>.Enumerator enumerator = base.module.data.selections.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					QudMutationModuleDataRow row = enumerator.Current;
					QudMutationsModuleWindow.MLNode mlnode = this.mutationNodes.Find((QudMutationsModuleWindow.MLNode n) => n.Entry != null && n.Entry.Name == row.Mutation);
					mlnode.Selected = row.Count;
					mlnode.Variant = row.Variant;
					num += mlnode.Selected * mlnode.Entry.Cost;
				}
			}
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x00132C30 File Offset: 0x00130E30
		public void UpdateControls()
		{
			using (IEnumerator<PrefixMenuOption> enumerator = this.categoryMenus.SelectMany<CategoryMenuData, PrefixMenuOption>((CategoryMenuData category) => category.menuOptions).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PrefixMenuOption menu = enumerator.Current;
					List<QudMutationsModuleWindow.MLNode> list = this.mutationNodes;
					QudMutationsModuleWindow.MLNode mlnode = ((list != null) ? list.Find(delegate(QudMutationsModuleWindow.MLNode n)
					{
						string text;
						if (n == null)
						{
							text = null;
						}
						else
						{
							MutationEntry entry = n.Entry;
							text = ((entry != null) ? entry.Name : null);
						}
						return text == menu.Id;
					}) : null);
					menu.Prefix = this.FormatNodePrefix(mlnode);
					menu.Description = this.FormatNodeDescription(mlnode, mlnode.Entry);
					if (mlnode.Entry.HasVariants && mlnode.Entry.CanSelectVariant)
					{
						BaseMutation exemplar = mlnode.Exemplar;
						menu.LongDescription = ((exemplar != null) ? (exemplar.GetDescription() + "\n\n" + exemplar.GetLevelText(1)) : "???");
						menu.Renderable = mlnode.Entry.GetRenderable();
						if (exemplar != null)
						{
							Renderable renderable = menu.Renderable as Renderable;
							if (renderable != null)
							{
								renderable.Tile = exemplar.GetIcon().getTile();
							}
						}
					}
				}
			}
			base.prefabComponent.BeforeShow(this.windowDescriptor, this.categoryMenus);
			base.prefabComponent.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HighlightMutation));
			base.GetOverlayWindow().UpdateMenuBars(this.windowDescriptor);
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x00132DCC File Offset: 0x00130FCC
		public override void RandomSelectionNoUI()
		{
			this.EnsureData();
			while (base.module.data.mp > 0)
			{
				QudMutationsModuleWindow.MLNode randomElement = this.mutationNodes.Where<QudMutationsModuleWindow.MLNode>((QudMutationsModuleWindow.MLNode m) => m.Entry != null && m.Entry.Cost <= base.module.data.mp && m.Selected < m.Entry.Maximum && m.Valid()).GetRandomElement<QudMutationsModuleWindow.MLNode>(null);
				randomElement.Selected++;
				randomElement.Randomize();
				this.UpdateDataFromNodes();
			}
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x00132E2C File Offset: 0x0013102C
		public override void RandomSelection()
		{
			while (base.module.data.mp > 0)
			{
				QudMutationsModuleWindow.MLNode randomElement = this.mutationNodes.Where<QudMutationsModuleWindow.MLNode>((QudMutationsModuleWindow.MLNode m) => m.Entry != null && m.Entry.Cost <= base.module.data.mp && m.Selected < m.Entry.Maximum && m.Valid()).GetRandomElement<QudMutationsModuleWindow.MLNode>(null);
				randomElement.Selected++;
				randomElement.Randomize();
				this.UpdateDataFromNodes();
				this.UpdateControls();
			}
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x00132E8C File Offset: 0x0013108C
		public void HighlightMutation(FrameworkDataElement dataElement)
		{
			QudMutationsModuleWindow.MLNode mlnode = this.mutationNodes.Find(delegate(QudMutationsModuleWindow.MLNode e)
			{
				string text;
				if (e == null)
				{
					text = null;
				}
				else
				{
					MutationEntry entry = e.Entry;
					text = ((entry != null) ? entry.Name : null);
				}
				FrameworkDataElement dataElement2 = dataElement;
				return text == ((dataElement2 != null) ? dataElement2.Id : null);
			});
			if (mlnode == null)
			{
				return;
			}
			this.selectedNode = mlnode;
			this.VariantSelectionEnabled = mlnode.Entry != null && mlnode.Entry.HasVariants && mlnode.Entry.CanSelectVariant;
			base.GetOverlayWindow().UpdateMenuBars(base.GetOverlayWindow().currentWindowDescriptor);
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x00132F08 File Offset: 0x00131108
		public void SelectMutation(FrameworkDataElement dataElement)
		{
			QudMutationsModuleWindow.MLNode mlnode = this.mutationNodes.Find(delegate(QudMutationsModuleWindow.MLNode e)
			{
				string text;
				if (e == null)
				{
					text = null;
				}
				else
				{
					MutationEntry entry = e.Entry;
					text = ((entry != null) ? entry.Name : null);
				}
				FrameworkDataElement dataElement2 = dataElement;
				return text == ((dataElement2 != null) ? dataElement2.Id : null);
			});
			if (mlnode == null)
			{
				return;
			}
			if (mlnode.Selected < mlnode.Entry.Maximum && mlnode.Entry.Cost <= base.module.data.mp && mlnode.Valid())
			{
				mlnode.Selected++;
			}
			else if (mlnode.Selected > 0)
			{
				mlnode.Selected = 0;
			}
			this.UpdateDataFromNodes();
			this.UpdateControls();
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x00132FA4 File Offset: 0x001311A4
		private string FormatNodeDescription(QudMutationsModuleWindow.MLNode node, MutationEntry entry)
		{
			this.sb.Clear();
			BaseMutation exemplar = node.Exemplar;
			exemplar.SetVariant(node.Variant);
			this.sb.Append(exemplar.GetCreateCharacterDisplayName());
			this.sb.Append((entry.HasVariants && entry.CanSelectVariant) ? (" [{{W|" + ControlManager.getCommandInputDescription("CmdChargenMutationVariant", true, false, ControlManager.InputDeviceType.Unknown, false) + "}}]") : "");
			return this.sb.ToString();
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x00133030 File Offset: 0x00131230
		private string FormatNodePrefix(QudMutationsModuleWindow.MLNode node)
		{
			bool flag = node.Selected > 0;
			object obj = (!node.Valid() || node.Entry.Cost > base.module.data.mp) && !flag;
			this.sb.Clear();
			object obj2 = obj;
			if (obj2 != null)
			{
				this.sb.Append("{{K|");
			}
			if (node.Selected == 0)
			{
				this.sb.Append("[ ]");
			}
			else if (node.Selected == 1 && node.Entry.Maximum == 1)
			{
				this.sb.Append("[■]");
			}
			else
			{
				this.sb.Append("[").Append(node.Selected).Append("]");
			}
			this.sb.Append("[{{");
			this.sb.Append((node.Entry.Cost > 0) ? "G" : "R");
			this.sb.Append("|").Append(node.Entry.Cost).Append("}}]");
			if (obj2 != null)
			{
				this.sb.Append("}}");
			}
			return this.sb.ToString();
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x00133180 File Offset: 0x00131380
		private PrefixMenuOption makeMenuOption(MutationEntry entry)
		{
			QudMutationsModuleWindow.MLNode mlnode = this.mutationNodes.Find(delegate(QudMutationsModuleWindow.MLNode n)
			{
				string text;
				if (n == null)
				{
					text = null;
				}
				else
				{
					MutationEntry entry2 = n.Entry;
					text = ((entry2 != null) ? entry2.Name : null);
				}
				MutationEntry entry3 = entry;
				return text == ((entry3 != null) ? entry3.Name : null);
			});
			BaseMutation exemplar = mlnode.Exemplar;
			return new PrefixMenuOption
			{
				Id = entry.Name,
				Prefix = this.FormatNodePrefix(mlnode),
				Description = this.FormatNodeDescription(mlnode, entry),
				LongDescription = ((exemplar != null) ? (exemplar.GetDescription() + "\n\n" + exemplar.GetLevelText(1)) : "???"),
				Renderable = ((exemplar != null) ? exemplar.GetIcon() : entry.GetRenderable())
			};
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x00133234 File Offset: 0x00131434
		public void ClearNodes()
		{
			this.mutationNodes = new List<QudMutationsModuleWindow.MLNode>();
			this.categoryMenus = new List<CategoryMenuData>();
			List<string> list = base.module.builder.handleUIEvent(QudMutationsModuleWindow.EID_GET_CATEGORIES, null) as List<string>;
			foreach (MutationCategory mutationCategory in MutationFactory.GetCategories())
			{
				if (!mutationCategory.Entries.All<MutationEntry>((MutationEntry x) => x.Hidden) && (list == null || list.Contains(mutationCategory.Name)))
				{
					CategoryMenuData categoryMenuData = new CategoryMenuData();
					this.categoryMenus.Add(categoryMenuData);
					categoryMenuData.Title = mutationCategory.DisplayName;
					categoryMenuData.menuOptions = new List<PrefixMenuOption>();
					QudMutationsModuleWindow.MLNode mlnode = new QudMutationsModuleWindow.MLNode();
					mlnode.bExpand = false;
					mlnode.Category = mutationCategory;
					mlnode.nodes = this.mutationNodes;
					this.mutationNodes.Add(mlnode);
					foreach (MutationEntry mutationEntry in mutationCategory.Entries)
					{
						if (!mutationEntry.Hidden)
						{
							QudMutationsModuleWindow.MLNode mlnode2 = new QudMutationsModuleWindow.MLNode();
							mlnode2.Exemplar = mutationEntry.CreateInstance();
							mlnode2.ParentNode = mlnode;
							mlnode2.Entry = mutationEntry;
							mlnode2.Variant = mutationEntry.Variant;
							mlnode2.nodes = this.mutationNodes;
							this.mutationNodes.Add(mlnode2);
						}
					}
					categoryMenuData.menuOptions.AddRange(mutationCategory.Entries.Where<MutationEntry>((MutationEntry x) => !x.Hidden).Select<MutationEntry, PrefixMenuOption>(new Func<MutationEntry, PrefixMenuOption>(this.makeMenuOption)));
				}
			}
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x00133448 File Offset: 0x00131648
		public void EnsureData()
		{
			if (base.module.data == null || (base.module.data.mp == 0 && base.module.data.selections.Count == 0))
			{
				QudMutationsModuleData qudMutationsModuleData = new QudMutationsModuleData();
				qudMutationsModuleData.mp = (int)base.module.builder.handleUIEvent(QudMutationsModuleWindow.EID_GET_BASE_MP, 0);
				base.module.setData(qudMutationsModuleData);
			}
			this.UpdateNodesFromData();
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x001334CC File Offset: 0x001316CC
		public override void BeforeShow(EmbarkBuilderModuleWindowDescriptor descriptor)
		{
			if (descriptor != null)
			{
				this.windowDescriptor = descriptor;
			}
			this.EnsureData();
			base.prefabComponent.onSelected.RemoveAllListeners();
			base.prefabComponent.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.SelectMutation));
			this.UpdateControls();
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x0013351B File Offset: 0x0013171B
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

		// Token: 0x06003E8F RID: 16015 RVA: 0x0013352B File Offset: 0x0013172B
		public override IEnumerable<MenuOption> GetKeyMenuBar()
		{
			yield return new MenuOption
			{
				Id = "ShowPoints",
				InputCommand = "",
				KeyDescription = null,
				Description = ((base.module.data.mp < 0) ? ("{{R|Points Remaining: " + base.module.data.mp.ToString() + "}}") : ("{{y|Points Remaining: " + base.module.data.mp.ToString() + "}}"))
			};
			if (this.VariantSelectionEnabled)
			{
				yield return new MenuOption
				{
					Id = "Variant",
					InputCommand = "CmdChargenMutationVariant",
					KeyDescription = ControlManager.getCommandInputDescription("CmdChargenMutationVariant", true, false, ControlManager.InputDeviceType.Unknown, false),
					Description = "Choose Variant"
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

		// Token: 0x06003E90 RID: 16016 RVA: 0x0013353C File Offset: 0x0013173C
		public override void HandleMenuOption(MenuOption menuOption)
		{
			if (menuOption.Id == "ShowPoints")
			{
				SummaryBlockData summaryBlock = base.module.GetSummaryBlock();
				string text = ((summaryBlock != null) ? summaryBlock.Description : null);
				if (!string.IsNullOrEmpty(text))
				{
					Popup.NewPopupMessageAsync(text, PopupMessage.AcceptButton, null, "Points Remaining: " + base.module.data.mp.ToString(), null, 0, null, null, null, true, true, false, default(CancellationToken), false, "", null, null, null).Start();
					return;
				}
			}
			else
			{
				if (menuOption.Id == "Variant")
				{
					this.SelectVariant();
					return;
				}
				base.HandleMenuOption(menuOption);
			}
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x001335E8 File Offset: 0x001317E8
		public async void SelectVariant()
		{
			BaseMutation baseMutation = this.selectedNode.Exemplar ?? this.selectedNode.Entry.Mutation;
			List<string> variants = baseMutation.GetVariants();
			string[] array = new string[variants.Count];
			IRenderable[] array2 = new IRenderable[variants.Count];
			for (int i = 0; i < variants.Count; i++)
			{
				string text = variants[i];
				array[i] = baseMutation.GetVariantName(text);
				array2[i] = baseMutation.GetIcon(text);
			}
			int num = await Popup.PickOptionAsync("Choose variant", null, "", array, null, array2, null, null, 0, 60, 0, -1, false, true, false, true);
			if (num >= 0)
			{
				this.selectedNode.Variant = variants[num];
				this.UpdateDataFromNodes();
				this.UpdateControls();
			}
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x00133620 File Offset: 0x00131820
		public override UIBreadcrumb GetBreadcrumb()
		{
			return new UIBreadcrumb
			{
				Id = base.GetType().FullName,
				Title = "Mutations",
				IconPath = "Items/sw_horns.bmp",
				IconDetailColor = Color.clear,
				IconForegroundColor = The.Color.Gray
			};
		}

		// Token: 0x04001ED6 RID: 7894
		public EmbarkBuilderModuleWindowDescriptor windowDescriptor;

		// Token: 0x04001ED7 RID: 7895
		public bool VariantSelectionEnabled;

		// Token: 0x04001ED8 RID: 7896
		private QudMutationsModuleWindow.MLNode selectedNode;

		// Token: 0x04001ED9 RID: 7897
		protected const string EMPTY_CHECK = "[ ]";

		// Token: 0x04001EDA RID: 7898
		protected const string CHECKED = "[■]";

		// Token: 0x04001EDB RID: 7899
		private StringBuilder sb = new StringBuilder();

		// Token: 0x04001EDC RID: 7900
		private List<CategoryMenuData> categoryMenus = new List<CategoryMenuData>();

		// Token: 0x04001EDD RID: 7901
		public static readonly string EID_GET_BASE_MP = "GetBaseMP";

		// Token: 0x04001EDE RID: 7902
		public static readonly string EID_GET_CATEGORIES = "GetMutationCategories";

		// Token: 0x04001EDF RID: 7903
		private List<QudMutationsModuleWindow.MLNode> mutationNodes = new List<QudMutationsModuleWindow.MLNode>();

		// Token: 0x04001EE0 RID: 7904
		private const string VARIANT_SELECT = "Variant";

		// Token: 0x04001EE1 RID: 7905
		public const string SHOW_POINTS = "ShowPoints";

		// Token: 0x020019C2 RID: 6594
		[Serializable]
		public class MLNode
		{
			// Token: 0x0600E4F0 RID: 58608 RVA: 0x0052CF98 File Offset: 0x0052B198
			public bool Valid()
			{
				if (this.Entry == null)
				{
					return true;
				}
				foreach (QudMutationsModuleWindow.MLNode mlnode in this.nodes)
				{
					if (mlnode.Entry != null && mlnode.Selected > 0 && !this.Entry.OkWith(mlnode.Entry, true, false))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600E4F1 RID: 58609 RVA: 0x0052D01C File Offset: 0x0052B21C
			public void Randomize()
			{
				this.Variant = null;
				if (this.Entry == null)
				{
					return;
				}
				if (!this.Entry.Variant.IsNullOrEmpty())
				{
					this.Variant = this.Entry.Variant;
					return;
				}
				if (this.Entry.HasVariants && this.Entry.CanSelectVariant)
				{
					BaseMutation baseMutation = this.Exemplar ?? this.Entry.Mutation;
					this.Variant = baseMutation.GetVariants().GetRandomElement<string>(new global::System.Random());
				}
			}

			// Token: 0x0400670D RID: 26381
			public MutationCategory Category;

			// Token: 0x0400670E RID: 26382
			public MutationEntry Entry;

			// Token: 0x0400670F RID: 26383
			public bool bExpand;

			// Token: 0x04006710 RID: 26384
			public QudMutationsModuleWindow.MLNode ParentNode;

			// Token: 0x04006711 RID: 26385
			public int Selected;

			// Token: 0x04006712 RID: 26386
			public string Variant;

			// Token: 0x04006713 RID: 26387
			public List<QudMutationsModuleWindow.MLNode> nodes;

			// Token: 0x04006714 RID: 26388
			public BaseMutation Exemplar;
		}
	}
}
