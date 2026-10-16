using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000087 RID: 135
	public abstract class BaseListView : BaseVerticalCollectionView
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00017608 File Offset: 0x00015808
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x00017610 File Offset: 0x00015810
		[CreateProperty]
		public bool showBoundCollectionSize
		{
			get
			{
				return this.m_ShowBoundCollectionSize;
			}
			set
			{
				bool flag = this.m_ShowBoundCollectionSize == value;
				if (!flag)
				{
					this.m_ShowBoundCollectionSize = value;
					this.SetupArraySizeField();
					base.NotifyPropertyChanged(in BaseListView.showBoundCollectionSizeProperty);
				}
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00017647 File Offset: 0x00015847
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x00017650 File Offset: 0x00015850
		[CreateProperty]
		public bool showFoldoutHeader
		{
			get
			{
				return this.m_ShowFoldoutHeader;
			}
			set
			{
				bool showFoldoutHeader = this.m_ShowFoldoutHeader;
				this.m_ShowFoldoutHeader = value;
				try
				{
					bool flag = this.makeHeader != null;
					if (!flag)
					{
						base.EnableInClassList(BaseListView.listViewWithHeaderUssClassName, value);
						bool showFoldoutHeader2 = this.m_ShowFoldoutHeader;
						if (showFoldoutHeader2)
						{
							this.AddFoldout();
						}
						else
						{
							bool flag2 = this.m_Foldout != null;
							if (flag2)
							{
								VisualElement visualElement = this.drawnFooter;
								if (visualElement != null)
								{
									visualElement.RemoveFromHierarchy();
								}
								this.RemoveFoldout();
							}
						}
						this.SetupArraySizeField();
						this.UpdateListViewLabel();
						bool flag3 = this.makeFooter == null;
						if (flag3)
						{
							bool showAddRemoveFooter = this.showAddRemoveFooter;
							if (showAddRemoveFooter)
							{
								this.EnableFooter(true);
							}
						}
						else
						{
							bool showFoldoutHeader3 = this.m_ShowFoldoutHeader;
							if (showFoldoutHeader3)
							{
								VisualElement visualElement2 = this.drawnFooter;
								if (visualElement2 != null)
								{
									visualElement2.RemoveFromHierarchy();
								}
								Foldout foldout = this.m_Foldout;
								if (foldout != null)
								{
									foldout.contentContainer.Add(this.drawnFooter);
								}
							}
							else
							{
								base.hierarchy.Add(this.drawnFooter);
								base.hierarchy.BringToFront(this.drawnFooter);
							}
						}
					}
				}
				finally
				{
					bool flag4 = showFoldoutHeader != this.m_ShowFoldoutHeader;
					if (flag4)
					{
						base.NotifyPropertyChanged(in BaseListView.showFoldoutHeaderProperty);
					}
				}
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000177B0 File Offset: 0x000159B0
		private void AddFoldout()
		{
			bool flag = this.m_Foldout != null;
			if (!flag)
			{
				this.m_Foldout = new Foldout
				{
					name = BaseListView.foldoutHeaderUssClassName,
					text = this.m_HeaderTitle
				};
				this.m_Foldout.toggle.tabIndex = 10;
				this.m_Foldout.toggle.acceptClicksIfDisabled = true;
				this.m_Foldout.AddToClassList(BaseListView.foldoutHeaderUssClassName);
				base.hierarchy.Add(this.m_Foldout);
				this.m_Foldout.Add(base.scrollView);
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00017854 File Offset: 0x00015A54
		private void RemoveFoldout()
		{
			Foldout foldout = this.m_Foldout;
			if (foldout != null)
			{
				foldout.RemoveFromHierarchy();
			}
			this.m_Foldout = null;
			base.hierarchy.Add(base.scrollView);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00017890 File Offset: 0x00015A90
		internal void SetupArraySizeField()
		{
			bool flag = !this.showBoundCollectionSize || (!this.showFoldoutHeader && base.GetProperty("__unity-collection-view-internal-binding") == null) || this.drawnHeader != null;
			if (flag)
			{
				TextField arraySizeField = this.m_ArraySizeField;
				if (arraySizeField != null)
				{
					arraySizeField.RemoveFromHierarchy();
				}
			}
			else
			{
				bool flag2 = this.m_ArraySizeField == null;
				if (flag2)
				{
					this.m_ArraySizeField = new TextField
					{
						name = BaseListView.arraySizeFieldUssClassName,
						tabIndex = 20
					};
					this.m_ArraySizeField.AddToClassList(BaseListView.arraySizeFieldUssClassName);
					this.m_ArraySizeField.RegisterValueChangedCallback<string>(new EventCallback<ChangeEvent<string>>(this.OnArraySizeFieldChanged));
					this.m_ArraySizeField.isDelayed = true;
					this.m_ArraySizeField.focusable = true;
				}
				this.m_ArraySizeField.EnableInClassList(BaseListView.arraySizeFieldWithFooterUssClassName, this.showAddRemoveFooter);
				this.m_ArraySizeField.EnableInClassList(BaseListView.arraySizeFieldWithHeaderUssClassName, this.showFoldoutHeader);
				bool showFoldoutHeader = this.showFoldoutHeader;
				if (showFoldoutHeader)
				{
					this.m_ArraySizeField.label = string.Empty;
					base.hierarchy.Add(this.m_ArraySizeField);
				}
				else
				{
					this.m_ArraySizeField.label = BaseListView.k_SizeFieldLabel;
					base.hierarchy.Insert(0, this.m_ArraySizeField);
				}
				this.UpdateArraySizeField();
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000179EC File Offset: 0x00015BEC
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x000179F4 File Offset: 0x00015BF4
		[CreateProperty]
		public string headerTitle
		{
			get
			{
				return this.m_HeaderTitle;
			}
			set
			{
				string headerTitle = this.m_HeaderTitle;
				this.m_HeaderTitle = value;
				bool flag = this.m_Foldout != null;
				if (flag)
				{
					this.m_Foldout.text = this.m_HeaderTitle;
				}
				bool flag2 = string.CompareOrdinal(headerTitle, this.m_HeaderTitle) != 0;
				if (flag2)
				{
					base.NotifyPropertyChanged(in BaseListView.headerTitleProperty);
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00017A4E File Offset: 0x00015C4E
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x00017A58 File Offset: 0x00015C58
		[CreateProperty]
		public Func<VisualElement> makeHeader
		{
			get
			{
				return this.m_MakeHeader;
			}
			set
			{
				bool flag = value == this.m_MakeHeader;
				if (!flag)
				{
					this.RemoveFoldout();
					this.m_MakeHeader = value;
					bool flag2 = this.m_MakeHeader != null;
					if (flag2)
					{
						this.SetupArraySizeField();
						this.drawnHeader = this.m_MakeHeader();
						this.drawnHeader.tabIndex = 1;
						base.hierarchy.Add(this.drawnHeader);
						base.hierarchy.SendToBack(this.drawnHeader);
					}
					else
					{
						VisualElement visualElement = this.drawnHeader;
						if (visualElement != null)
						{
							visualElement.RemoveFromHierarchy();
						}
						this.drawnHeader = null;
						bool showFoldoutHeader = this.showFoldoutHeader;
						if (showFoldoutHeader)
						{
							this.AddFoldout();
							this.SetupArraySizeField();
							this.UpdateListViewLabel();
						}
					}
					bool flag3 = this.drawnFooter != null;
					if (flag3)
					{
						bool flag4 = this.m_Foldout != null;
						if (flag4)
						{
							this.drawnFooter.RemoveFromHierarchy();
							this.m_Foldout.contentContainer.hierarchy.Add(this.drawnFooter);
						}
						else
						{
							base.hierarchy.Add(this.drawnFooter);
							VisualElement visualElement2 = this.drawnFooter;
							if (visualElement2 != null)
							{
								visualElement2.BringToFront();
							}
						}
					}
					else
					{
						this.EnableFooter(this.showAddRemoveFooter);
					}
					base.NotifyPropertyChanged(in BaseListView.makeHeaderProperty);
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00017BBA File Offset: 0x00015DBA
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x00017BC4 File Offset: 0x00015DC4
		[CreateProperty]
		public Func<VisualElement> makeFooter
		{
			get
			{
				return this.m_MakeFooter;
			}
			set
			{
				bool flag = value == this.m_MakeFooter;
				if (!flag)
				{
					this.m_MakeFooter = value;
					bool flag2 = this.m_MakeFooter != null;
					if (flag2)
					{
						VisualElement footer = this.m_Footer;
						if (footer != null)
						{
							footer.RemoveFromHierarchy();
						}
						this.m_Footer = null;
						this.drawnFooter = this.m_MakeFooter();
						bool flag3 = this.m_Foldout != null;
						if (flag3)
						{
							this.m_Foldout.contentContainer.Add(this.drawnFooter);
						}
						else
						{
							base.hierarchy.Add(this.drawnFooter);
							base.hierarchy.BringToFront(this.drawnFooter);
						}
						base.EnableInClassList(BaseListView.listViewWithFooterUssClassName, true);
						base.scrollView.EnableInClassList(BaseListView.scrollViewWithFooterUssClassName, true);
					}
					else
					{
						VisualElement visualElement = this.drawnFooter;
						if (visualElement != null)
						{
							visualElement.RemoveFromHierarchy();
						}
						this.drawnFooter = null;
						this.EnableFooter(this.m_ShowAddRemoveFooter);
					}
					base.NotifyPropertyChanged(in BaseListView.makeFooterProperty);
				}
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00017CD3 File Offset: 0x00015ED3
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x00017CE0 File Offset: 0x00015EE0
		[CreateProperty]
		public bool showAddRemoveFooter
		{
			get
			{
				return this.m_Footer != null;
			}
			set
			{
				bool showAddRemoveFooter = this.showAddRemoveFooter;
				this.m_ShowAddRemoveFooter = value;
				bool flag = this.makeFooter == null;
				if (flag)
				{
					this.EnableFooter(value);
				}
				bool flag2 = value && this.m_ArraySizeField != null;
				if (flag2)
				{
					this.m_ArraySizeField.AddToClassList(BaseListView.arraySizeFieldWithFooterUssClassName);
				}
				bool flag3 = showAddRemoveFooter != this.showFoldoutHeader;
				if (flag3)
				{
					base.NotifyPropertyChanged(in BaseListView.showAddRemoveFooterProperty);
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00017D51 File Offset: 0x00015F51
		internal Foldout headerFoldout
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_Foldout;
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00017D5C File Offset: 0x00015F5C
		private void EnableFooter(bool enabled)
		{
			base.EnableInClassList(BaseListView.listViewWithFooterUssClassName, enabled);
			base.scrollView.EnableInClassList(BaseListView.scrollViewWithFooterUssClassName, enabled);
			if (enabled)
			{
				bool flag = this.m_Footer == null;
				if (flag)
				{
					this.m_Footer = new VisualElement
					{
						name = BaseListView.footerUssClassName
					};
					this.m_Footer.AddToClassList(BaseListView.footerUssClassName);
					this.m_AddButton = new Button(new Action(this.OnAddClicked))
					{
						name = BaseListView.footerAddButtonName,
						text = "+"
					};
					this.m_AddButton.SetEnabled(this.allowAdd);
					this.m_Footer.Add(this.m_AddButton);
					this.m_RemoveButton = new Button(new Action(this.OnRemoveClicked))
					{
						name = BaseListView.footerRemoveButtonName,
						text = "-"
					};
					this.m_Footer.Add(this.m_RemoveButton);
					this.UpdateRemoveButton();
				}
				bool flag2 = this.m_Foldout != null;
				if (flag2)
				{
					this.m_Foldout.contentContainer.Add(this.m_Footer);
				}
				else
				{
					base.hierarchy.Add(this.m_Footer);
				}
			}
			else
			{
				Button removeButton = this.m_RemoveButton;
				if (removeButton != null)
				{
					removeButton.RemoveFromHierarchy();
				}
				Button addButton = this.m_AddButton;
				if (addButton != null)
				{
					addButton.RemoveFromHierarchy();
				}
				VisualElement footer = this.m_Footer;
				if (footer != null)
				{
					footer.RemoveFromHierarchy();
				}
				this.m_RemoveButton = null;
				this.m_AddButton = null;
				this.m_Footer = null;
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000503 RID: 1283 RVA: 0x00017EF4 File Offset: 0x000160F4
		// (remove) Token: 0x06000504 RID: 1284 RVA: 0x00017F2C File Offset: 0x0001612C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<IEnumerable<int>> itemsAdded;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000505 RID: 1285 RVA: 0x00017F64 File Offset: 0x00016164
		// (remove) Token: 0x06000506 RID: 1286 RVA: 0x00017F9C File Offset: 0x0001619C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<IEnumerable<int>> itemsRemoved;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000507 RID: 1287 RVA: 0x00017FD4 File Offset: 0x000161D4
		// (remove) Token: 0x06000508 RID: 1288 RVA: 0x0001800C File Offset: 0x0001620C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action itemsSourceSizeChanged;

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00018044 File Offset: 0x00016244
		private IVisualElementScheduledItem trackItemCount
		{
			get
			{
				bool flag = this.m_TrackedItem != null;
				IVisualElementScheduledItem visualElementScheduledItem;
				if (flag)
				{
					visualElementScheduledItem = this.m_TrackedItem;
				}
				else
				{
					this.m_TrackedItem = base.schedule.Execute(this.trackCount).Until(this.untilManualBindingSourceSelectionMode);
					visualElementScheduledItem = this.m_TrackedItem;
				}
				return visualElementScheduledItem;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00018094 File Offset: 0x00016294
		private Action trackCount
		{
			get
			{
				Action action;
				if ((action = this.m_TrackCount) == null)
				{
					action = (this.m_TrackCount = delegate
					{
						IList itemsSource = base.itemsSource;
						int? num = ((itemsSource != null) ? new int?(itemsSource.Count) : null);
						int previousRefreshedCount = this.m_PreviousRefreshedCount;
						bool flag = !((num.GetValueOrDefault() == previousRefreshedCount) & (num != null));
						if (flag)
						{
							base.RefreshItems();
						}
					});
				}
				return action;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x000180C0 File Offset: 0x000162C0
		private Func<bool> untilManualBindingSourceSelectionMode
		{
			get
			{
				Func<bool> func;
				if ((func = this.m_WhileAutoAssign) == null)
				{
					func = (this.m_WhileAutoAssign = () => !this.autoAssignSource);
				}
				return func;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x000180EC File Offset: 0x000162EC
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x000180F4 File Offset: 0x000162F4
		[CreateProperty]
		public BindingSourceSelectionMode bindingSourceSelectionMode
		{
			get
			{
				return this.m_BindingSourceSelectionMode;
			}
			set
			{
				bool flag = this.m_BindingSourceSelectionMode == value;
				if (!flag)
				{
					this.m_BindingSourceSelectionMode = value;
					base.Rebuild();
					base.NotifyPropertyChanged(in BaseListView.bindingSourceSelectionModeProperty);
					bool autoAssignSource = this.autoAssignSource;
					if (autoAssignSource)
					{
						this.trackItemCount.Resume();
					}
				}
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00018141 File Offset: 0x00016341
		internal bool autoAssignSource
		{
			get
			{
				return this.bindingSourceSelectionMode == BindingSourceSelectionMode.AutoAssign;
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001814C File Offset: 0x0001634C
		private void AddItems(int itemCount)
		{
			BaseListViewController baseListViewController = base.GetOrCreateViewController() as BaseListViewController;
			bool flag = baseListViewController != null;
			if (flag)
			{
				baseListViewController.AddItems(itemCount);
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00018176 File Offset: 0x00016376
		private void RemoveItems(List<int> indices)
		{
			this.viewController.RemoveItems(indices);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00018188 File Offset: 0x00016388
		private void OnArraySizeFieldChanged(ChangeEvent<string> evt)
		{
			bool flag = this.m_ArraySizeField.showMixedValue && BaseField<string>.mixedValueString == evt.newValue;
			if (!flag)
			{
				int num;
				bool flag2 = !int.TryParse(evt.newValue, out num) || num < 0;
				if (flag2)
				{
					this.m_ArraySizeField.SetValueWithoutNotify(evt.previousValue);
				}
				else
				{
					int itemsCount = this.viewController.GetItemsCount();
					bool flag3 = itemsCount == 0 && num == this.viewController.GetItemsMinCount();
					if (!flag3)
					{
						bool flag4 = num > itemsCount;
						if (flag4)
						{
							this.viewController.AddItems(num - itemsCount);
						}
						else
						{
							bool flag5 = num < itemsCount;
							if (flag5)
							{
								this.viewController.RemoveItems(itemsCount - num);
							}
							else
							{
								bool flag6 = num == 0;
								if (flag6)
								{
									this.viewController.ClearItems();
									this.m_IsOverMultiEditLimit = false;
								}
							}
						}
						this.UpdateListViewLabel();
					}
				}
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00018279 File Offset: 0x00016479
		private void UpdateRemoveButton()
		{
			Button removeButton = this.m_RemoveButton;
			if (removeButton != null)
			{
				bool flag;
				if (this.allowRemove)
				{
					BaseListViewController viewController = this.viewController;
					flag = viewController != null && viewController.GetItemsCount() > 0;
				}
				else
				{
					flag = false;
				}
				removeButton.SetEnabled(flag);
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000182B0 File Offset: 0x000164B0
		internal void UpdateArraySizeField()
		{
			bool flag = !this.HasValidDataAndBindings() || this.m_ArraySizeField == null;
			if (!flag)
			{
				bool flag2 = !this.m_ArraySizeField.showMixedValue;
				if (flag2)
				{
					this.m_ArraySizeField.SetValueWithoutNotify(this.viewController.GetItemsMinCount().ToString());
				}
				VisualElement footer = this.footer;
				if (footer != null)
				{
					footer.SetEnabled(!this.m_IsOverMultiEditLimit);
				}
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00018324 File Offset: 0x00016524
		internal void UpdateListViewLabel()
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = base.itemsSource.Count == 0;
				bool isOverMultiEditLimit = this.m_IsOverMultiEditLimit;
				if (isOverMultiEditLimit)
				{
					if (this.m_ListViewLabel == null)
					{
						this.m_ListViewLabel = new Label();
					}
					this.m_ListViewLabel.text = this.m_MaxMultiEditStr;
					base.scrollView.contentViewport.Add(this.m_ListViewLabel);
				}
				else
				{
					bool flag3 = flag2;
					if (flag3)
					{
						bool flag4 = this.m_MakeNoneElement != null;
						if (flag4)
						{
							if (this.m_NoneElement == null)
							{
								this.m_NoneElement = this.m_MakeNoneElement();
							}
							base.scrollView.contentViewport.Add(this.m_NoneElement);
							Label listViewLabel = this.m_ListViewLabel;
							if (listViewLabel != null)
							{
								listViewLabel.RemoveFromHierarchy();
							}
							this.m_ListViewLabel = null;
						}
						else
						{
							if (this.m_ListViewLabel == null)
							{
								this.m_ListViewLabel = new Label();
							}
							this.m_ListViewLabel.text = BaseListView.k_EmptyListStr;
							base.scrollView.contentViewport.Add(this.m_ListViewLabel);
							VisualElement noneElement = this.m_NoneElement;
							if (noneElement != null)
							{
								noneElement.RemoveFromHierarchy();
							}
							this.m_NoneElement = null;
						}
					}
					else
					{
						VisualElement noneElement2 = this.m_NoneElement;
						if (noneElement2 != null)
						{
							noneElement2.RemoveFromHierarchy();
						}
						this.m_NoneElement = null;
						Label listViewLabel2 = this.m_ListViewLabel;
						if (listViewLabel2 != null)
						{
							listViewLabel2.RemoveFromHierarchy();
						}
						this.m_ListViewLabel = null;
					}
				}
				Label listViewLabel3 = this.m_ListViewLabel;
				if (listViewLabel3 != null)
				{
					listViewLabel3.EnableInClassList(BaseListView.emptyLabelUssClassName, flag2);
				}
				Label listViewLabel4 = this.m_ListViewLabel;
				if (listViewLabel4 != null)
				{
					listViewLabel4.EnableInClassList(BaseListView.overMaxMultiEditLimitClassName, this.m_IsOverMultiEditLimit);
				}
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000184C8 File Offset: 0x000166C8
		private void OnAddClicked()
		{
			BaseListView.<>c__DisplayClass82_0 CS$<>8__locals1 = new BaseListView.<>c__DisplayClass82_0();
			CS$<>8__locals1.<>4__this = this;
			BaseListView.<>c__DisplayClass82_0 CS$<>8__locals2 = CS$<>8__locals1;
			IList itemsSource = base.itemsSource;
			CS$<>8__locals2.itemsCountPreCallback = ((itemsSource != null) ? itemsSource.Count : 0);
			bool flag = this.overridingAddButtonBehavior != null;
			if (flag)
			{
				this.overridingAddButtonBehavior(this, this.m_AddButton);
			}
			else
			{
				bool flag2 = this.onAdd != null;
				if (flag2)
				{
					this.onAdd(this);
				}
				else
				{
					this.AddItems(1);
				}
			}
			bool flag3 = base.GetProperty("__unity-collection-view-internal-binding") == null;
			if (flag3)
			{
				this.OnAfterAddClicked(CS$<>8__locals1.itemsCountPreCallback);
			}
			else
			{
				base.schedule.Execute(delegate
				{
					CS$<>8__locals1.<>4__this.OnAfterAddClicked(CS$<>8__locals1.itemsCountPreCallback);
				}).ExecuteLater(100L);
			}
			bool flag4 = this.HasValidDataAndBindings() && this.m_ArraySizeField != null;
			if (flag4)
			{
				this.m_ArraySizeField.showMixedValue = false;
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000185B8 File Offset: 0x000167B8
		private void OnAfterAddClicked(int itemsCountPreCallback)
		{
			bool flag = base.itemsSource != null && itemsCountPreCallback != base.itemsSource.Count;
			if (flag)
			{
				this.OnItemsSourceSizeChanged();
				base.SetSelection(base.itemsSource.Count - 1);
				base.ScrollToItem(-1);
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001860C File Offset: 0x0001680C
		private void OnRemoveClicked()
		{
			bool flag = this.onRemove != null;
			if (flag)
			{
				this.onRemove(this);
			}
			else
			{
				bool flag2 = base.selectedIndices.Any<int>();
				if (flag2)
				{
					this.viewController.RemoveItems(base.selectedIndices.ToList<int>());
					base.ClearSelection();
				}
				else
				{
					IList itemsSource = base.itemsSource;
					bool flag3 = itemsSource != null && itemsSource.Count > 0;
					if (flag3)
					{
						int num = base.itemsSource.Count - 1;
						this.viewController.RemoveItem(num);
					}
				}
			}
			bool flag4 = this.HasValidDataAndBindings() && this.m_ArraySizeField != null;
			if (flag4)
			{
				this.m_ArraySizeField.showMixedValue = false;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x000186C6 File Offset: 0x000168C6
		internal TextField arraySizeField
		{
			get
			{
				return this.m_ArraySizeField;
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000186CE File Offset: 0x000168CE
		internal void SetOverMaxMultiEditLimit(bool isOverLimit, int maxMultiEditCount)
		{
			this.m_IsOverMultiEditLimit = isOverLimit;
			this.m_MaxMultiEditCount = maxMultiEditCount;
			this.m_MaxMultiEditStr = string.Format("This field cannot display arrays with more than {0} elements when multiple objects are selected.", this.m_MaxMultiEditCount);
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x000186FA File Offset: 0x000168FA
		internal VisualElement footer
		{
			get
			{
				return this.m_Footer;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00018702 File Offset: 0x00016902
		public new BaseListViewController viewController
		{
			get
			{
				return base.viewController as BaseListViewController;
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001870F File Offset: 0x0001690F
		private protected override void CreateVirtualizationController()
		{
			base.CreateVirtualizationController<ReusableListViewItem>();
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001871C File Offset: 0x0001691C
		public override void SetViewController(CollectionViewController controller)
		{
			if (this.m_ItemAddedCallback == null)
			{
				this.m_ItemAddedCallback = new Action<IEnumerable<int>>(this.OnItemAdded);
			}
			if (this.m_ItemRemovedCallback == null)
			{
				this.m_ItemRemovedCallback = new Action<IEnumerable<int>>(this.OnItemsRemoved);
			}
			if (this.m_ItemsSourceSizeChangedCallback == null)
			{
				this.m_ItemsSourceSizeChangedCallback = new Action(this.OnItemsSourceSizeChanged);
			}
			bool flag = this.viewController != null;
			if (flag)
			{
				this.viewController.itemsAdded -= this.m_ItemAddedCallback;
				this.viewController.itemsRemoved -= this.m_ItemRemovedCallback;
				this.viewController.itemsSourceSizeChanged -= this.m_ItemsSourceSizeChangedCallback;
			}
			base.SetViewController(controller);
			bool flag2 = this.viewController != null;
			if (flag2)
			{
				this.viewController.itemsAdded += this.m_ItemAddedCallback;
				this.viewController.itemsRemoved += this.m_ItemRemovedCallback;
				this.viewController.itemsSourceSizeChanged += this.m_ItemsSourceSizeChangedCallback;
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001880A File Offset: 0x00016A0A
		private void OnItemAdded(IEnumerable<int> indices)
		{
			Action<IEnumerable<int>> action = this.itemsAdded;
			if (action != null)
			{
				action(indices);
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00018820 File Offset: 0x00016A20
		private void OnItemsRemoved(IEnumerable<int> indices)
		{
			Action<IEnumerable<int>> action = this.itemsRemoved;
			if (action != null)
			{
				action(indices);
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00018838 File Offset: 0x00016A38
		private void OnItemsSourceSizeChanged()
		{
			bool flag = base.GetProperty("__unity-collection-view-internal-binding") == null;
			if (flag)
			{
				base.RefreshItems();
			}
			Action action = this.itemsSourceSizeChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000521 RID: 1313 RVA: 0x00018878 File Offset: 0x00016A78
		// (remove) Token: 0x06000522 RID: 1314 RVA: 0x000188B0 File Offset: 0x00016AB0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action reorderModeChanged;

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x000188E5 File Offset: 0x00016AE5
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x000188F0 File Offset: 0x00016AF0
		[CreateProperty]
		public ListViewReorderMode reorderMode
		{
			get
			{
				return this.m_ReorderMode;
			}
			set
			{
				bool flag = value == this.m_ReorderMode;
				if (!flag)
				{
					this.m_ReorderMode = value;
					base.InitializeDragAndDropController(base.reorderable);
					Action action = this.reorderModeChanged;
					if (action != null)
					{
						action();
					}
					base.Rebuild();
					base.NotifyPropertyChanged(in BaseListView.reorderModeProperty);
				}
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00018946 File Offset: 0x00016B46
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00018950 File Offset: 0x00016B50
		[CreateProperty]
		public Func<VisualElement> makeNoneElement
		{
			get
			{
				return this.m_MakeNoneElement;
			}
			set
			{
				bool flag = value == this.m_MakeNoneElement;
				if (!flag)
				{
					this.m_MakeNoneElement = value;
					VisualElement noneElement = this.m_NoneElement;
					if (noneElement != null)
					{
						noneElement.RemoveFromHierarchy();
					}
					this.m_NoneElement = null;
					base.RefreshItems();
					base.NotifyPropertyChanged(in BaseListView.makeNoneElementProperty);
				}
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x000189A3 File Offset: 0x00016BA3
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x000189AC File Offset: 0x00016BAC
		[CreateProperty]
		public bool allowAdd
		{
			get
			{
				return this.m_AllowAdd;
			}
			set
			{
				bool flag = value == this.m_AllowAdd;
				if (!flag)
				{
					this.m_AllowAdd = value;
					Button addButton = this.m_AddButton;
					if (addButton != null)
					{
						addButton.SetEnabled(this.m_AllowAdd);
					}
					base.NotifyPropertyChanged(in BaseListView.allowAddProperty);
				}
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x000189F4 File Offset: 0x00016BF4
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x000189FC File Offset: 0x00016BFC
		[CreateProperty]
		public Action<BaseListView, Button> overridingAddButtonBehavior
		{
			get
			{
				return this.m_OverridingAddButtonBehavior;
			}
			set
			{
				bool flag = value == this.m_OverridingAddButtonBehavior;
				if (!flag)
				{
					this.m_OverridingAddButtonBehavior = value;
					base.RefreshItems();
					base.NotifyPropertyChanged(in BaseListView.overridingAddButtonBehaviorProperty);
				}
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00018A36 File Offset: 0x00016C36
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x00018A40 File Offset: 0x00016C40
		[CreateProperty]
		public Action<BaseListView> onAdd
		{
			get
			{
				return this.m_OnAdd;
			}
			set
			{
				bool flag = value == this.m_OnAdd;
				if (!flag)
				{
					this.m_OnAdd = value;
					base.RefreshItems();
					base.NotifyPropertyChanged(in BaseListView.onAddProperty);
				}
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00018A7A File Offset: 0x00016C7A
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x00018A84 File Offset: 0x00016C84
		[CreateProperty]
		public bool allowRemove
		{
			get
			{
				return this.m_AllowRemove;
			}
			set
			{
				bool flag = value == this.m_AllowRemove;
				if (!flag)
				{
					this.m_AllowRemove = value;
					this.UpdateRemoveButton();
					base.NotifyPropertyChanged(in BaseListView.allowRemoveProperty);
				}
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00018ABB File Offset: 0x00016CBB
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x00018AC4 File Offset: 0x00016CC4
		[CreateProperty]
		public Action<BaseListView> onRemove
		{
			get
			{
				return this.m_OnRemove;
			}
			set
			{
				bool flag = value == this.m_OnRemove;
				if (!flag)
				{
					this.m_OnRemove = value;
					base.RefreshItems();
					base.NotifyPropertyChanged(in BaseListView.onRemoveProperty);
				}
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00018B00 File Offset: 0x00016D00
		internal override ListViewDragger CreateDragger()
		{
			bool flag = this.m_ReorderMode == ListViewReorderMode.Simple;
			ListViewDragger listViewDragger;
			if (flag)
			{
				listViewDragger = new ListViewDragger(this);
			}
			else
			{
				listViewDragger = new ListViewDraggerAnimated(this);
			}
			return listViewDragger;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00018B2E File Offset: 0x00016D2E
		internal override ICollectionDragAndDropController CreateDragAndDropController()
		{
			return new ListViewReorderableDragAndDropController(this);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00018B36 File Offset: 0x00016D36
		public BaseListView()
			: this(null, -1f)
		{
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00018B48 File Offset: 0x00016D48
		public BaseListView(IList itemsSource, float itemHeight = -1f)
			: base(itemsSource, itemHeight)
		{
			base.AddToClassList(BaseListView.ussClassName);
			base.pickingMode = PickingMode.Ignore;
			this.allowAdd = true;
			this.allowRemove = true;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00018B9F File Offset: 0x00016D9F
		private protected override void PostRefresh()
		{
			this.UpdateArraySizeField();
			this.UpdateRemoveButton();
			this.UpdateListViewLabel();
			base.PostRefresh();
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00018BC0 File Offset: 0x00016DC0
		private protected override bool HandleItemNavigation(bool moveIn, bool altPressed)
		{
			bool flag = false;
			foreach (int num in base.selectedIndices)
			{
				foreach (ReusableCollectionItem reusableCollectionItem in base.activeItems)
				{
					bool flag2 = reusableCollectionItem.index == num && base.GetProperty("__unity-collection-view-internal-binding") != null;
					if (flag2)
					{
						Foldout foldout = reusableCollectionItem.bindableElement.Q<Foldout>(null, null);
						bool flag3 = foldout != null;
						if (flag3)
						{
							foldout.value = moveIn;
							flag = true;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x04000274 RID: 628
		private static readonly string k_SizeFieldLabel = "Size";

		// Token: 0x04000275 RID: 629
		internal static readonly BindingId showBoundCollectionSizeProperty = "showBoundCollectionSize";

		// Token: 0x04000276 RID: 630
		internal static readonly BindingId showFoldoutHeaderProperty = "showFoldoutHeader";

		// Token: 0x04000277 RID: 631
		internal static readonly BindingId headerTitleProperty = "headerTitle";

		// Token: 0x04000278 RID: 632
		internal static readonly BindingId makeHeaderProperty = "makeHeader";

		// Token: 0x04000279 RID: 633
		internal static readonly BindingId makeFooterProperty = "makeFooter";

		// Token: 0x0400027A RID: 634
		internal static readonly BindingId showAddRemoveFooterProperty = "showAddRemoveFooter";

		// Token: 0x0400027B RID: 635
		internal static readonly BindingId bindingSourceSelectionModeProperty = "bindingSourceSelectionMode";

		// Token: 0x0400027C RID: 636
		internal static readonly BindingId reorderModeProperty = "reorderMode";

		// Token: 0x0400027D RID: 637
		internal static readonly BindingId makeNoneElementProperty = "makeNoneElement";

		// Token: 0x0400027E RID: 638
		internal static readonly BindingId allowAddProperty = "allowAdd";

		// Token: 0x0400027F RID: 639
		internal static readonly BindingId overridingAddButtonBehaviorProperty = "overridingAddButtonBehavior";

		// Token: 0x04000280 RID: 640
		internal static readonly BindingId onAddProperty = "onAdd";

		// Token: 0x04000281 RID: 641
		internal static readonly BindingId allowRemoveProperty = "allowRemove";

		// Token: 0x04000282 RID: 642
		internal static readonly BindingId onRemoveProperty = "onRemove";

		// Token: 0x04000283 RID: 643
		private const int k_FoldoutTabIndex = 10;

		// Token: 0x04000284 RID: 644
		private const int k_ArraySizeFieldTabIndex = 20;

		// Token: 0x04000285 RID: 645
		private bool m_ShowBoundCollectionSize = true;

		// Token: 0x04000286 RID: 646
		private bool m_ShowFoldoutHeader;

		// Token: 0x04000287 RID: 647
		private string m_HeaderTitle;

		// Token: 0x04000288 RID: 648
		private VisualElement drawnHeader;

		// Token: 0x04000289 RID: 649
		private Func<VisualElement> m_MakeHeader;

		// Token: 0x0400028A RID: 650
		private VisualElement drawnFooter;

		// Token: 0x0400028B RID: 651
		private Func<VisualElement> m_MakeFooter;

		// Token: 0x0400028C RID: 652
		private bool m_ShowAddRemoveFooter;

		// Token: 0x04000290 RID: 656
		private IVisualElementScheduledItem m_TrackedItem;

		// Token: 0x04000291 RID: 657
		private Action m_TrackCount;

		// Token: 0x04000292 RID: 658
		private Func<bool> m_WhileAutoAssign;

		// Token: 0x04000293 RID: 659
		private BindingSourceSelectionMode m_BindingSourceSelectionMode = BindingSourceSelectionMode.Manual;

		// Token: 0x04000294 RID: 660
		private Label m_ListViewLabel;

		// Token: 0x04000295 RID: 661
		private Foldout m_Foldout;

		// Token: 0x04000296 RID: 662
		private TextField m_ArraySizeField;

		// Token: 0x04000297 RID: 663
		private bool m_IsOverMultiEditLimit;

		// Token: 0x04000298 RID: 664
		private int m_MaxMultiEditCount;

		// Token: 0x04000299 RID: 665
		private VisualElement m_Footer;

		// Token: 0x0400029A RID: 666
		private Button m_AddButton;

		// Token: 0x0400029B RID: 667
		private Button m_RemoveButton;

		// Token: 0x0400029C RID: 668
		private Action<IEnumerable<int>> m_ItemAddedCallback;

		// Token: 0x0400029D RID: 669
		private Action<IEnumerable<int>> m_ItemRemovedCallback;

		// Token: 0x0400029E RID: 670
		private Action m_ItemsSourceSizeChangedCallback;

		// Token: 0x0400029F RID: 671
		private ListViewReorderMode m_ReorderMode;

		// Token: 0x040002A1 RID: 673
		private VisualElement m_NoneElement;

		// Token: 0x040002A2 RID: 674
		private Func<VisualElement> m_MakeNoneElement;

		// Token: 0x040002A3 RID: 675
		private bool m_AllowAdd = true;

		// Token: 0x040002A4 RID: 676
		private Action<BaseListView, Button> m_OverridingAddButtonBehavior;

		// Token: 0x040002A5 RID: 677
		private Action<BaseListView> m_OnAdd;

		// Token: 0x040002A6 RID: 678
		private bool m_AllowRemove = true;

		// Token: 0x040002A7 RID: 679
		private Action<BaseListView> m_OnRemove;

		// Token: 0x040002A8 RID: 680
		public new static readonly string ussClassName = "unity-list-view";

		// Token: 0x040002A9 RID: 681
		public new static readonly string itemUssClassName = BaseListView.ussClassName + "__item";

		// Token: 0x040002AA RID: 682
		public static readonly string emptyLabelUssClassName = BaseListView.ussClassName + "__empty-label";

		// Token: 0x040002AB RID: 683
		public static readonly string overMaxMultiEditLimitClassName = BaseListView.ussClassName + "__over-max-multi-edit-limit-label";

		// Token: 0x040002AC RID: 684
		public static readonly string reorderableUssClassName = BaseListView.ussClassName + "__reorderable";

		// Token: 0x040002AD RID: 685
		public static readonly string reorderableItemUssClassName = BaseListView.reorderableUssClassName + "-item";

		// Token: 0x040002AE RID: 686
		public static readonly string reorderableItemContainerUssClassName = BaseListView.reorderableItemUssClassName + "__container";

		// Token: 0x040002AF RID: 687
		public static readonly string reorderableItemHandleUssClassName = BaseListView.reorderableUssClassName + "-handle";

		// Token: 0x040002B0 RID: 688
		public static readonly string reorderableItemHandleBarUssClassName = BaseListView.reorderableItemHandleUssClassName + "-bar";

		// Token: 0x040002B1 RID: 689
		public static readonly string footerUssClassName = BaseListView.ussClassName + "__footer";

		// Token: 0x040002B2 RID: 690
		public static readonly string foldoutHeaderUssClassName = BaseListView.ussClassName + "__foldout-header";

		// Token: 0x040002B3 RID: 691
		public static readonly string arraySizeFieldUssClassName = BaseListView.ussClassName + "__size-field";

		// Token: 0x040002B4 RID: 692
		public static readonly string arraySizeFieldWithHeaderUssClassName = BaseListView.arraySizeFieldUssClassName + "--with-header";

		// Token: 0x040002B5 RID: 693
		public static readonly string arraySizeFieldWithFooterUssClassName = BaseListView.arraySizeFieldUssClassName + "--with-footer";

		// Token: 0x040002B6 RID: 694
		public static readonly string listViewWithHeaderUssClassName = BaseListView.ussClassName + "--with-header";

		// Token: 0x040002B7 RID: 695
		public static readonly string listViewWithFooterUssClassName = BaseListView.ussClassName + "--with-footer";

		// Token: 0x040002B8 RID: 696
		public static readonly string scrollViewWithFooterUssClassName = BaseListView.ussClassName + "__scroll-view--with-footer";

		// Token: 0x040002B9 RID: 697
		public static readonly string footerAddButtonName = BaseListView.ussClassName + "__add-button";

		// Token: 0x040002BA RID: 698
		public static readonly string footerRemoveButtonName = BaseListView.ussClassName + "__remove-button";

		// Token: 0x040002BB RID: 699
		private string m_MaxMultiEditStr;

		// Token: 0x040002BC RID: 700
		private static readonly string k_EmptyListStr = "List is empty";

		// Token: 0x02000088 RID: 136
		[ExcludeFromDocs]
		[Serializable]
		public new abstract class UxmlSerializedData : BaseVerticalCollectionView.UxmlSerializedData
		{
			// Token: 0x0600053A RID: 1338 RVA: 0x00018F6C File Offset: 0x0001716C
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(BaseListView.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("showFoldoutHeader", "show-foldout-header", null, Array.Empty<string>()),
					new UxmlAttributeNames("headerTitle", "header-title", null, Array.Empty<string>()),
					new UxmlAttributeNames("showAddRemoveFooter", "show-add-remove-footer", null, Array.Empty<string>()),
					new UxmlAttributeNames("allowAdd", "allow-add", null, Array.Empty<string>()),
					new UxmlAttributeNames("allowRemove", "allow-remove", null, Array.Empty<string>()),
					new UxmlAttributeNames("reorderMode", "reorder-mode", null, Array.Empty<string>()),
					new UxmlAttributeNames("showBoundCollectionSize", "show-bound-collection-size", null, Array.Empty<string>()),
					new UxmlAttributeNames("bindingSourceSelectionMode", "binding-source-selection-mode", null, Array.Empty<string>())
				});
			}

			// Token: 0x0600053B RID: 1339 RVA: 0x00019070 File Offset: 0x00017270
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				BaseListView baseListView = (BaseListView)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.showFoldoutHeader_UxmlAttributeFlags);
				if (flag)
				{
					baseListView.showFoldoutHeader = this.showFoldoutHeader;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.headerTitle_UxmlAttributeFlags);
				if (flag2)
				{
					baseListView.headerTitle = this.headerTitle;
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.showAddRemoveFooter_UxmlAttributeFlags);
				if (flag3)
				{
					baseListView.showAddRemoveFooter = this.showAddRemoveFooter;
				}
				bool flag4 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.allowAdd_UxmlAttributeFlags);
				if (flag4)
				{
					baseListView.allowAdd = this.allowAdd;
				}
				bool flag5 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.allowRemove_UxmlAttributeFlags);
				if (flag5)
				{
					baseListView.allowRemove = this.allowRemove;
				}
				bool flag6 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.reorderMode_UxmlAttributeFlags);
				if (flag6)
				{
					baseListView.reorderMode = this.reorderMode;
				}
				bool flag7 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.showBoundCollectionSize_UxmlAttributeFlags);
				if (flag7)
				{
					baseListView.showBoundCollectionSize = this.showBoundCollectionSize;
				}
				bool flag8 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.bindingSourceSelectionMode_UxmlAttributeFlags);
				if (flag8)
				{
					baseListView.bindingSourceSelectionMode = this.bindingSourceSelectionMode;
				}
			}

			// Token: 0x040002BD RID: 701
			[SerializeField]
			private string headerTitle;

			// Token: 0x040002BE RID: 702
			[SerializeField]
			private ListViewReorderMode reorderMode;

			// Token: 0x040002BF RID: 703
			[SerializeField]
			private BindingSourceSelectionMode bindingSourceSelectionMode;

			// Token: 0x040002C0 RID: 704
			[SerializeField]
			private bool showFoldoutHeader;

			// Token: 0x040002C1 RID: 705
			[SerializeField]
			private bool showAddRemoveFooter;

			// Token: 0x040002C2 RID: 706
			[SerializeField]
			private bool allowAdd;

			// Token: 0x040002C3 RID: 707
			[SerializeField]
			private bool allowRemove;

			// Token: 0x040002C4 RID: 708
			[SerializeField]
			private bool showBoundCollectionSize;

			// Token: 0x040002C5 RID: 709
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags showFoldoutHeader_UxmlAttributeFlags;

			// Token: 0x040002C6 RID: 710
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags headerTitle_UxmlAttributeFlags;

			// Token: 0x040002C7 RID: 711
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags showAddRemoveFooter_UxmlAttributeFlags;

			// Token: 0x040002C8 RID: 712
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags allowAdd_UxmlAttributeFlags;

			// Token: 0x040002C9 RID: 713
			[UxmlIgnore]
			[HideInInspector]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags allowRemove_UxmlAttributeFlags;

			// Token: 0x040002CA RID: 714
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags reorderMode_UxmlAttributeFlags;

			// Token: 0x040002CB RID: 715
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags showBoundCollectionSize_UxmlAttributeFlags;

			// Token: 0x040002CC RID: 716
			[UxmlIgnore]
			[SerializeField]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags bindingSourceSelectionMode_UxmlAttributeFlags;
		}

		// Token: 0x02000089 RID: 137
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseVerticalCollectionView.UxmlTraits
		{
			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x0600053D RID: 1341 RVA: 0x00019180 File Offset: 0x00017380
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x0600053E RID: 1342 RVA: 0x000191A0 File Offset: 0x000173A0
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				BaseListView baseListView = (BaseListView)ve;
				baseListView.reorderMode = this.m_ReorderMode.GetValueFromBag(bag, cc);
				baseListView.showFoldoutHeader = this.m_ShowFoldoutHeader.GetValueFromBag(bag, cc);
				baseListView.headerTitle = this.m_HeaderTitle.GetValueFromBag(bag, cc);
				baseListView.showAddRemoveFooter = this.m_ShowAddRemoveFooter.GetValueFromBag(bag, cc);
				baseListView.allowAdd = this.m_AllowAdd.GetValueFromBag(bag, cc);
				baseListView.allowRemove = this.m_AllowRemove.GetValueFromBag(bag, cc);
				baseListView.showBoundCollectionSize = this.m_ShowBoundCollectionSize.GetValueFromBag(bag, cc);
				baseListView.bindingSourceSelectionMode = this.m_BindingSourceSelectionMode.GetValueFromBag(bag, cc);
			}

			// Token: 0x0600053F RID: 1343 RVA: 0x00019260 File Offset: 0x00017460
			protected UxmlTraits()
			{
				this.m_PickingMode.defaultValue = PickingMode.Ignore;
			}

			// Token: 0x040002CD RID: 717
			private readonly UxmlBoolAttributeDescription m_ShowFoldoutHeader = new UxmlBoolAttributeDescription
			{
				name = "show-foldout-header",
				defaultValue = false
			};

			// Token: 0x040002CE RID: 718
			private readonly UxmlStringAttributeDescription m_HeaderTitle = new UxmlStringAttributeDescription
			{
				name = "header-title",
				defaultValue = string.Empty
			};

			// Token: 0x040002CF RID: 719
			private readonly UxmlBoolAttributeDescription m_ShowAddRemoveFooter = new UxmlBoolAttributeDescription
			{
				name = "show-add-remove-footer",
				defaultValue = false
			};

			// Token: 0x040002D0 RID: 720
			private readonly UxmlBoolAttributeDescription m_AllowAdd = new UxmlBoolAttributeDescription
			{
				name = "allow-add",
				defaultValue = true
			};

			// Token: 0x040002D1 RID: 721
			private readonly UxmlBoolAttributeDescription m_AllowRemove = new UxmlBoolAttributeDescription
			{
				name = "allow-remove",
				defaultValue = true
			};

			// Token: 0x040002D2 RID: 722
			private readonly UxmlEnumAttributeDescription<ListViewReorderMode> m_ReorderMode = new UxmlEnumAttributeDescription<ListViewReorderMode>
			{
				name = "reorder-mode",
				defaultValue = ListViewReorderMode.Simple
			};

			// Token: 0x040002D3 RID: 723
			private readonly UxmlBoolAttributeDescription m_ShowBoundCollectionSize = new UxmlBoolAttributeDescription
			{
				name = "show-bound-collection-size",
				defaultValue = true
			};

			// Token: 0x040002D4 RID: 724
			private readonly UxmlEnumAttributeDescription<BindingSourceSelectionMode> m_BindingSourceSelectionMode = new UxmlEnumAttributeDescription<BindingSourceSelectionMode>
			{
				name = "binding-source-selection-mode",
				defaultValue = BindingSourceSelectionMode.Manual
			};
		}
	}
}
