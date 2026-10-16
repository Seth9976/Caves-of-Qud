using System;
using XRL.Core;

namespace XRL.World.Effects
{
	// Token: 0x02000DAC RID: 3500
	[Serializable]
	public class WarmingUp : Effect
	{
		// Token: 0x06008BD1 RID: 35793 RVA: 0x0031A6AC File Offset: 0x003188AC
		public WarmingUp()
		{
			this.Duration = 1;
			this.DisplayName = "{{C|warming up}}";
		}

		// Token: 0x06008BD2 RID: 35794 RVA: 0x0031A6C6 File Offset: 0x003188C6
		public WarmingUp(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008BD3 RID: 35795 RVA: 0x0031A6D5 File Offset: 0x003188D5
		public override int GetEffectType()
		{
			return 117440514;
		}

		// Token: 0x06008BD4 RID: 35796 RVA: 0x0031A6DC File Offset: 0x003188DC
		public override string GetDetails()
		{
			return "Getting ready to act.";
		}

		// Token: 0x06008BD5 RID: 35797 RVA: 0x0031A6E3 File Offset: 0x003188E3
		public override bool Apply(GameObject Object)
		{
			return !Object.HasEffect<WarmingUp>();
		}

		// Token: 0x06008BD6 RID: 35798 RVA: 0x0031A6F0 File Offset: 0x003188F0
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 20;
			if (num > 10 && num < 20)
			{
				E.RenderString = "!";
				E.ColorString = "&W^R";
				E.DetailColor = "W";
				return false;
			}
			return true;
		}
	}
}
