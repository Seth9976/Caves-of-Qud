using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F5E RID: 3934
	[Serializable]
	public class NotNameable : IPart
	{
		// Token: 0x06009CDD RID: 40157 RVA: 0x0038D624 File Offset: 0x0038B824
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanBeNamedEvent.ID;
		}

		// Token: 0x06009CDE RID: 40158 RVA: 0x0038D63A File Offset: 0x0038B83A
		public override bool HandleEvent(CanBeNamedEvent E)
		{
			return E.Item != this.ParentObject && base.HandleEvent(E);
		}
	}
}
