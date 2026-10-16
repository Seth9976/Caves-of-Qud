using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200024B RID: 587
	public class InputEvent : EventBase<InputEvent>
	{
		// Token: 0x0600115F RID: 4447 RVA: 0x0004A73D File Offset: 0x0004893D
		static InputEvent()
		{
			EventBase<InputEvent>.SetCreateFunction(() => new InputEvent());
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x0004A756 File Offset: 0x00048956
		// (set) Token: 0x06001161 RID: 4449 RVA: 0x0004A75E File Offset: 0x0004895E
		public string previousData { get; protected set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x0004A767 File Offset: 0x00048967
		// (set) Token: 0x06001163 RID: 4451 RVA: 0x0004A76F File Offset: 0x0004896F
		public string newData { get; protected set; }

		// Token: 0x06001164 RID: 4452 RVA: 0x0004A778 File Offset: 0x00048978
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0004A789 File Offset: 0x00048989
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
			this.previousData = null;
			this.newData = null;
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0004A7A4 File Offset: 0x000489A4
		public static InputEvent GetPooled(string previousData, string newData)
		{
			InputEvent pooled = EventBase<InputEvent>.GetPooled();
			pooled.previousData = previousData;
			pooled.newData = newData;
			return pooled;
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0004A7CD File Offset: 0x000489CD
		public InputEvent()
		{
			this.LocalInit();
		}
	}
}
