using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020005B6 RID: 1462
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class PropertyChangedEvent : EventBase<PropertyChangedEvent>, IChangeEvent
	{
		// Token: 0x06002B7E RID: 11134 RVA: 0x0009CFB9 File Offset: 0x0009B1B9
		static PropertyChangedEvent()
		{
			EventBase<PropertyChangedEvent>.SetCreateFunction(() => new PropertyChangedEvent());
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06002B7F RID: 11135 RVA: 0x0009CFD2 File Offset: 0x0009B1D2
		// (set) Token: 0x06002B80 RID: 11136 RVA: 0x0009CFDA File Offset: 0x0009B1DA
		public BindingId property { get; set; }

		// Token: 0x06002B81 RID: 11137 RVA: 0x0009CFE3 File Offset: 0x0009B1E3
		public PropertyChangedEvent()
		{
			base.bubbles = false;
			base.tricklesDown = false;
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x0009D000 File Offset: 0x0009B200
		public static PropertyChangedEvent GetPooled(in BindingId property)
		{
			PropertyChangedEvent pooled = EventBase<PropertyChangedEvent>.GetPooled();
			pooled.property = property;
			return pooled;
		}
	}
}
