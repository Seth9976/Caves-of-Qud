using System;
using System.Diagnostics;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x02000076 RID: 118
	internal class ReusableCollectionItem
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00014772 File Offset: 0x00012972
		public virtual VisualElement rootElement
		{
			get
			{
				return this.bindableElement;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0001477A File Offset: 0x0001297A
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00014782 File Offset: 0x00012982
		public VisualElement bindableElement { get; protected set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0001478B File Offset: 0x0001298B
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00014793 File Offset: 0x00012993
		public ValueAnimation<StyleValues> animator { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0001479C File Offset: 0x0001299C
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x000147A4 File Offset: 0x000129A4
		public int index { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x000147AD File Offset: 0x000129AD
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x000147B5 File Offset: 0x000129B5
		public int id { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x000147BE File Offset: 0x000129BE
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x000147C6 File Offset: 0x000129C6
		internal bool isDragGhost { get; private set; }

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600045F RID: 1119 RVA: 0x000147D0 File Offset: 0x000129D0
		// (remove) Token: 0x06000460 RID: 1120 RVA: 0x00014808 File Offset: 0x00012A08
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ReusableCollectionItem> onGeometryChanged;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000461 RID: 1121 RVA: 0x00014840 File Offset: 0x00012A40
		// (remove) Token: 0x06000462 RID: 1122 RVA: 0x00014878 File Offset: 0x00012A78
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<ReusableCollectionItem> onDestroy;

		// Token: 0x06000463 RID: 1123 RVA: 0x000148B0 File Offset: 0x00012AB0
		public ReusableCollectionItem()
		{
			this.index = (this.id = -1);
			this.m_GeometryChangedEventCallback = new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000148EA File Offset: 0x00012AEA
		public virtual void Init(VisualElement item)
		{
			this.bindableElement = item;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000148F5 File Offset: 0x00012AF5
		public virtual void PreAttachElement()
		{
			this.rootElement.AddToClassList(BaseVerticalCollectionView.itemUssClassName);
			this.rootElement.RegisterCallback<GeometryChangedEvent>(this.m_GeometryChangedEventCallback, TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001491C File Offset: 0x00012B1C
		public virtual void DetachElement()
		{
			this.rootElement.RemoveFromClassList(BaseVerticalCollectionView.itemUssClassName);
			this.rootElement.UnregisterCallback<GeometryChangedEvent>(this.m_GeometryChangedEventCallback, TrickleDown.NoTrickleDown);
			VisualElement rootElement = this.rootElement;
			if (rootElement != null)
			{
				rootElement.RemoveFromHierarchy();
			}
			this.SetSelected(false);
			this.SetDragGhost(false);
			this.index = (this.id = -1);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00014982 File Offset: 0x00012B82
		public virtual void DestroyElement()
		{
			Action<ReusableCollectionItem> action = this.onDestroy;
			if (action != null)
			{
				action(this);
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00014998 File Offset: 0x00012B98
		public virtual void SetSelected(bool selected)
		{
			if (selected)
			{
				this.rootElement.AddToClassList(BaseVerticalCollectionView.itemSelectedVariantUssClassName);
				this.rootElement.pseudoStates |= PseudoStates.Checked;
			}
			else
			{
				this.rootElement.RemoveFromClassList(BaseVerticalCollectionView.itemSelectedVariantUssClassName);
				this.rootElement.pseudoStates &= ~PseudoStates.Checked;
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000149FC File Offset: 0x00012BFC
		public virtual void SetDragGhost(bool dragGhost)
		{
			this.isDragGhost = dragGhost;
			this.rootElement.style.maxHeight = (this.isDragGhost ? StyleKeyword.Undefined : StyleKeyword.Initial);
			this.bindableElement.style.display = (this.isDragGhost ? DisplayStyle.None : DisplayStyle.Flex);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00014A56 File Offset: 0x00012C56
		protected virtual void OnGeometryChanged(GeometryChangedEvent evt)
		{
			this.rootElement.UpdateWorldTransform();
			this.bindableElement.UpdateWorldTransform();
			this.bindableElement.IncrementVersion(VersionChangeType.Transform);
			Action<ReusableCollectionItem> action = this.onGeometryChanged;
			if (action != null)
			{
				action(this);
			}
		}

		// Token: 0x04000225 RID: 549
		public const int UndefinedIndex = -1;

		// Token: 0x0400022C RID: 556
		protected EventCallback<GeometryChangedEvent> m_GeometryChangedEventCallback;
	}
}
