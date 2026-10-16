using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F57 RID: 3927
	[Serializable]
	public class DisplayNameColor : IPart
	{
		// Token: 0x06009CA2 RID: 40098 RVA: 0x0038C7F8 File Offset: 0x0038A9F8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID;
		}

		// Token: 0x06009CA3 RID: 40099 RVA: 0x0038C80E File Offset: 0x0038AA0E
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!this.Color.IsNullOrEmpty())
			{
				E.AddColor(this.Color, this.ColorPriority);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009CA4 RID: 40100 RVA: 0x0038C836 File Offset: 0x0038AA36
		public void SetColor(string Color)
		{
			this.Color = Color;
		}

		// Token: 0x06009CA5 RID: 40101 RVA: 0x0038C83F File Offset: 0x0038AA3F
		public void SetColorAndPriority(string Color, int Priority)
		{
			this.Color = Color;
			this.ColorPriority = Priority;
		}

		// Token: 0x06009CA6 RID: 40102 RVA: 0x0038C84F File Offset: 0x0038AA4F
		public void SetColorByPriority(string Color, int Priority)
		{
			if (this.Color.IsNullOrEmpty() || this.ColorPriority < Priority)
			{
				this.Color = Color;
				this.ColorPriority = Priority;
			}
		}

		// Token: 0x040040C5 RID: 16581
		public string Color;

		// Token: 0x040040C6 RID: 16582
		public int ColorPriority = 20;
	}
}
