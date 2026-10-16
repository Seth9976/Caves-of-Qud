using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x020001EC RID: 492
	internal class DefaultDragAndDropClient : DragAndDropData, IDragAndDrop
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00043367 File Offset: 0x00041567
		public override DragVisualMode visualMode
		{
			get
			{
				return this.m_VisualMode;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x0004336F File Offset: 0x0004156F
		public override object source
		{
			get
			{
				return this.GetGenericData("__unity-drag-and-drop__source-view");
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0004337C File Offset: 0x0004157C
		public override IEnumerable<Object> unityObjectReferences
		{
			get
			{
				return this.m_UnityObjectReferences;
			}
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00043384 File Offset: 0x00041584
		public override object GetGenericData(string key)
		{
			return this.m_GenericData.ContainsKey(key) ? this.m_GenericData[key] : null;
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x000433B3 File Offset: 0x000415B3
		public override void SetGenericData(string key, object value)
		{
			this.m_GenericData[key] = value;
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x000433C4 File Offset: 0x000415C4
		public void StartDrag(StartDragArgs args, Vector3 pointerPosition)
		{
			bool flag = args.unityObjectReferences != null;
			if (flag)
			{
				this.m_UnityObjectReferences = args.unityObjectReferences.ToArray<Object>();
			}
			this.paths = args.assetPaths;
			this.m_VisualMode = args.visualMode;
			foreach (object obj in args.genericData)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.m_GenericData[(string)dictionaryEntry.Key] = dictionaryEntry.Value;
			}
			bool flag2 = string.IsNullOrWhiteSpace(args.title);
			if (!flag2)
			{
				VisualElement visualElement = this.source as VisualElement;
				VisualElement visualElement2 = ((visualElement != null) ? visualElement.panel.visualTree : null);
				bool flag3 = visualElement2 == null;
				if (!flag3)
				{
					if (this.m_DraggedInfoLabel == null)
					{
						this.m_DraggedInfoLabel = new Label
						{
							pickingMode = PickingMode.Ignore,
							style = 
							{
								position = Position.Absolute
							}
						};
					}
					this.m_DraggedInfoLabel.text = args.title;
					this.m_DraggedInfoLabel.style.top = pointerPosition.y;
					this.m_DraggedInfoLabel.style.left = pointerPosition.x;
					visualElement2.Add(this.m_DraggedInfoLabel);
				}
			}
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00043544 File Offset: 0x00041744
		public void UpdateDrag(Vector3 pointerPosition)
		{
			bool flag = this.m_DraggedInfoLabel == null;
			if (!flag)
			{
				this.m_DraggedInfoLabel.style.top = pointerPosition.y;
				this.m_DraggedInfoLabel.style.left = pointerPosition.x;
			}
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00002152 File Offset: 0x00000352
		public void AcceptDrag()
		{
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00043599 File Offset: 0x00041799
		public void SetVisualMode(DragVisualMode mode)
		{
			this.m_VisualMode = mode;
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x000435A3 File Offset: 0x000417A3
		public void DragCleanup()
		{
			this.paths = null;
			this.m_UnityObjectReferences = null;
			Hashtable genericData = this.m_GenericData;
			if (genericData != null)
			{
				genericData.Clear();
			}
			this.SetVisualMode(DragVisualMode.None);
			Label draggedInfoLabel = this.m_DraggedInfoLabel;
			if (draggedInfoLabel != null)
			{
				draggedInfoLabel.RemoveFromHierarchy();
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x000435E1 File Offset: 0x000417E1
		public DragAndDropData data
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04000948 RID: 2376
		private readonly Hashtable m_GenericData = new Hashtable();

		// Token: 0x04000949 RID: 2377
		private Label m_DraggedInfoLabel;

		// Token: 0x0400094A RID: 2378
		private DragVisualMode m_VisualMode;

		// Token: 0x0400094B RID: 2379
		private IEnumerable<Object> m_UnityObjectReferences;
	}
}
