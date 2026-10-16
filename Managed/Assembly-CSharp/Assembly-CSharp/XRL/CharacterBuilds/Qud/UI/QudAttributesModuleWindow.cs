using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds.Qud.UI
{
	// Token: 0x020005B1 RID: 1457
	[UIView("CharacterCreation:PickAttributes", false, false, false, null, null, false, 0, false, NavCategory = "Chargen", UICanvas = "Chargen/PickAttributes", UICanvasHost = 1)]
	public class QudAttributesModuleWindow : EmbarkBuilderModuleWindowPrefabBase<QudAttributesModule, HorizontalScroller>
	{
		// Token: 0x06003E0A RID: 15882 RVA: 0x00130CC0 File Offset: 0x0012EEC0
		public void updateData()
		{
			base.module.data = new QudAttributesModuleData();
			base.module.data.apSpent = this.apRemaining - this.baseAP;
			base.module.data.apRemaining = this.apRemaining;
			base.module.data.baseAp = this.baseAP;
			this.attributes.ForEach(delegate(AttributeDataElement e)
			{
				base.module.data.PointsPurchased.Add(e.Attribute, e.Purchased);
			});
			if (!base.module.builder.SkippingUIUpdates)
			{
				base.GetOverlayWindow().UpdateMenuBars(base.GetOverlayWindow().currentWindowDescriptor);
			}
			base.module.setData(base.module.data);
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x00130D7B File Offset: 0x0012EF7B
		public IEnumerable<AttributeDataElement> GetSelections()
		{
			object obj = base.module.builder.handleUIEvent(QudAttributesModuleWindow.EID_BEFORE_GET_BASE_ATTRIBUTES, new List<AttributeDataElement>());
			obj = base.module.builder.handleUIEvent(QudAttributesModuleWindow.EID_GET_BASE_ATTRIBUTES, obj);
			foreach (AttributeDataElement attributeDataElement in (obj as List<AttributeDataElement>))
			{
				yield return attributeDataElement;
			}
			List<AttributeDataElement>.Enumerator enumerator = default(List<AttributeDataElement>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x00130D8B File Offset: 0x0012EF8B
		public override IEnumerable<MenuOption> GetKeyMenuBar()
		{
			yield return new MenuOption
			{
				Id = null,
				InputCommand = "",
				KeyDescription = null,
				Description = ((base.module.data.apRemaining < 0) ? ("{{R|Points Remaining: " + base.module.data.apRemaining.ToString() + "}}") : ("{{y|Points Remaining: " + base.module.data.apRemaining.ToString() + "}}"))
			};
			foreach (MenuOption menuOption in base.GetKeyMenuBar())
			{
				yield return menuOption;
			}
			IEnumerator<MenuOption> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x00130D9C File Offset: 0x0012EF9C
		public override void Init()
		{
			base.prefabComponent.selectionPrefab = ((GameObject)Resources.Load("Prefabs/AttributeSelectionControl")).GetComponent<FrameworkUnityScrollChild>();
			base.prefabComponent.GetComponentInChildren<GridLayoutGroup>(true).cellSize = new Vector2(153f, base.prefabComponent.GetComponentInChildren<GridLayoutGroup>().cellSize.y);
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x00130DF8 File Offset: 0x0012EFF8
		public void UpdateControls()
		{
			if (this.attributes == null)
			{
				return;
			}
			this.attributes.ForEach(delegate(AttributeDataElement a)
			{
				a.window = this;
				QudAttributesModule module = base.module;
				bool flag;
				if (module == null)
				{
					flag = null != null;
				}
				else
				{
					QudAttributesModuleData data = module.data;
					flag = ((data != null) ? data.PointsPurchased : null) != null;
				}
				if (!flag)
				{
					a.Purchased = 0;
					return;
				}
				int num;
				if (base.module.data.PointsPurchased.TryGetValue(a.Attribute, out num))
				{
					a.Purchased = num;
					return;
				}
				a.Purchased = 0;
			});
			foreach (AttributeDataElement attributeDataElement in this.attributes)
			{
				if (attributeDataElement != null)
				{
					AttributeSelectionControl control = attributeDataElement.control;
					if (control != null)
					{
						control.Updated();
					}
				}
			}
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x00130E7C File Offset: 0x0012F07C
		public override void ResetSelection()
		{
			QudAttributesModuleData qudAttributesModuleData = new QudAttributesModuleData();
			this.baseAP = (int)base.module.builder.handleUIEvent(QudAttributesModuleWindow.EID_GET_BASE_AP, 0);
			base.module.setData(qudAttributesModuleData);
			this.UpdateControls();
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x00130EC8 File Offset: 0x0012F0C8
		public override void BeforeShow(EmbarkBuilderModuleWindowDescriptor descriptor)
		{
			this.attributes = new List<AttributeDataElement>(this.GetSelections());
			this.UpdateControls();
			base.prefabComponent.BeforeShow(descriptor, this.attributes);
			base.prefabComponent.scrollContext.wraps = false;
			foreach (AbstractScrollContext abstractScrollContext in this.GetGridSiblings())
			{
				abstractScrollContext.gridSiblings = new Func<IEnumerable<AbstractScrollContext>>(this.GetGridSiblings);
			}
			this.EnsureBaseAP();
			base.BeforeShow(descriptor);
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x00130F68 File Offset: 0x0012F168
		public void EnsureBaseAP()
		{
			this.baseAP = (int)base.module.builder.handleUIEvent(QudAttributesModuleWindow.EID_GET_BASE_AP, 0);
			this.updateData();
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x00130F96 File Offset: 0x0012F196
		public override void RandomSelectionNoUI()
		{
			this.attributes = new List<AttributeDataElement>(this.GetSelections());
			this.EnsureBaseAP();
			this.UpdateControls();
			this.RandomSelection();
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x00130FBB File Offset: 0x0012F1BB
		public override void RandomSelection()
		{
			while (this.apRemaining > 0)
			{
				this.attributes.GetRandomElement<AttributeDataElement>(null).raise();
			}
			this.UpdateControls();
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x00130FDF File Offset: 0x0012F1DF
		public IEnumerable<AbstractScrollContext> GetGridSiblings()
		{
			foreach (NavigationContext navigationContext in base.prefabComponent.scrollContext.contexts)
			{
				AbstractScrollContext abstractScrollContext = ((ScrollChildContext)navigationContext).proxyTo as AbstractScrollContext;
				if (abstractScrollContext != null)
				{
					yield return abstractScrollContext;
				}
			}
			List<NavigationContext>.Enumerator enumerator = default(List<NavigationContext>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06003E15 RID: 15893 RVA: 0x00130FF0 File Offset: 0x0012F1F0
		public int apRemaining
		{
			get
			{
				if (this.attributes == null)
				{
					return this.baseAP;
				}
				return this.baseAP - this.attributes.Sum<AttributeDataElement>((AttributeDataElement a) => a.APSpent);
			}
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x0013103D File Offset: 0x0012F23D
		public void AttributeUpdated(AttributeDataElement att)
		{
			this.updateData();
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x00131048 File Offset: 0x0012F248
		public override UIBreadcrumb GetBreadcrumb()
		{
			return new UIBreadcrumb
			{
				Id = base.GetType().FullName,
				Title = "Attributes",
				IconPath = "Items/sw_sign_medical.bmp",
				IconDetailColor = Color.clear,
				IconForegroundColor = The.Color.Gray
			};
		}

		// Token: 0x04001EBC RID: 7868
		public static readonly string EID_BEFORE_GET_BASE_ATTRIBUTES = "BeforeGetBaseAttributes";

		// Token: 0x04001EBD RID: 7869
		public static readonly string EID_GET_BASE_ATTRIBUTES = "GetBaseAttributes";

		// Token: 0x04001EBE RID: 7870
		public static readonly string EID_GET_BASE_AP = "GetBaseAP";

		// Token: 0x04001EBF RID: 7871
		public List<AttributeDataElement> attributes;

		// Token: 0x04001EC0 RID: 7872
		private int baseAP;
	}
}
