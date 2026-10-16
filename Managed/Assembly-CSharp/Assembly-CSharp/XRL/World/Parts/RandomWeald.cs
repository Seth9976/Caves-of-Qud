using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001282 RID: 4738
	[Serializable]
	public class RandomWeald : IPart
	{
		// Token: 0x0600BB90 RID: 48016 RVA: 0x00448294 File Offset: 0x00446494
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BB91 RID: 48017 RVA: 0x00448297 File Offset: 0x00446497
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600BB92 RID: 48018 RVA: 0x004482B0 File Offset: 0x004464B0
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			int num = Stat.Random(1, 4);
			if (num == 1)
			{
				this.ParentObject.Render.ColorString = "&w";
			}
			else if (num == 2)
			{
				this.ParentObject.Render.ColorString = "&W";
			}
			else if (num == 3)
			{
				this.ParentObject.Render.ColorString = "&G";
			}
			else if (num == 4)
			{
				this.ParentObject.Render.ColorString = "&g";
			}
			if (Stat.Random(0, 1) == 0)
			{
				this.ParentObject.Render.ColorString = this.ParentObject.Render.ColorString.ToLower();
			}
			int num2 = Stat.Random(1, 10);
			if (num2 == 1)
			{
				this.ParentObject.Render.RenderString = ",";
			}
			if (num2 == 2)
			{
				this.ParentObject.Render.RenderString = ".";
			}
			if (num2 >= 3 && num2 <= 6)
			{
				this.ParentObject.Render.RenderString = "õ";
			}
			if (num2 > 6 && num2 <= 9)
			{
				this.ParentObject.Render.RenderString = "\u009d";
			}
			if (num2 == 10)
			{
				this.ParentObject.Render.RenderString = "\u009f";
			}
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}
	}
}
