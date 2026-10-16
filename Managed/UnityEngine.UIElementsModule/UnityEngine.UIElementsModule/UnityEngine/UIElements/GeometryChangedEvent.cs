using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000254 RID: 596
	[EventCategory(EventCategory.Geometry)]
	public class GeometryChangedEvent : EventBase<GeometryChangedEvent>
	{
		// Token: 0x06001194 RID: 4500 RVA: 0x0004AD7D File Offset: 0x00048F7D
		static GeometryChangedEvent()
		{
			EventBase<GeometryChangedEvent>.SetCreateFunction(() => new GeometryChangedEvent());
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0004AD98 File Offset: 0x00048F98
		public static GeometryChangedEvent GetPooled(Rect oldRect, Rect newRect)
		{
			GeometryChangedEvent pooled = EventBase<GeometryChangedEvent>.GetPooled();
			pooled.oldRect = oldRect;
			pooled.newRect = newRect;
			return pooled;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0004ADC1 File Offset: 0x00048FC1
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0004ADD2 File Offset: 0x00048FD2
		private void LocalInit()
		{
			this.oldRect = Rect.zero;
			this.newRect = Rect.zero;
			this.layoutPass = 0;
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x0004ADF5 File Offset: 0x00048FF5
		// (set) Token: 0x06001199 RID: 4505 RVA: 0x0004ADFD File Offset: 0x00048FFD
		public Rect oldRect { get; private set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x0004AE06 File Offset: 0x00049006
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x0004AE0E File Offset: 0x0004900E
		public Rect newRect { get; private set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x0004AE17 File Offset: 0x00049017
		// (set) Token: 0x0600119D RID: 4509 RVA: 0x0004AE1F File Offset: 0x0004901F
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal int layoutPass { get; set; }

		// Token: 0x0600119E RID: 4510 RVA: 0x0004AE28 File Offset: 0x00049028
		public GeometryChangedEvent()
		{
			this.LocalInit();
		}
	}
}
