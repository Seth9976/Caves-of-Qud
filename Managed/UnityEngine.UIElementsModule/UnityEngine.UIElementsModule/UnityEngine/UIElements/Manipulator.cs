using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002EA RID: 746
	public abstract class Manipulator : IManipulator
	{
		// Token: 0x06001544 RID: 5444
		protected abstract void RegisterCallbacksOnTarget();

		// Token: 0x06001545 RID: 5445
		protected abstract void UnregisterCallbacksFromTarget();

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x00055540 File Offset: 0x00053740
		// (set) Token: 0x06001547 RID: 5447 RVA: 0x00055558 File Offset: 0x00053758
		public VisualElement target
		{
			get
			{
				return this.m_Target;
			}
			set
			{
				bool flag = this.target != null;
				if (flag)
				{
					this.UnregisterCallbacksFromTarget();
				}
				this.m_Target = value;
				bool flag2 = this.target != null;
				if (flag2)
				{
					this.RegisterCallbacksOnTarget();
				}
			}
		}

		// Token: 0x04000B83 RID: 2947
		private VisualElement m_Target;
	}
}
