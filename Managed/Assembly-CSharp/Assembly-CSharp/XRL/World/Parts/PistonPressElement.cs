using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x0200126E RID: 4718
	[Serializable]
	public class PistonPressElement : IPart
	{
		// Token: 0x0600BAF5 RID: 47861 RVA: 0x00444D5C File Offset: 0x00442F5C
		public override bool SameAs(IPart p)
		{
			return base.SameAs(p);
		}

		// Token: 0x0600BAF6 RID: 47862 RVA: 0x00444D65 File Offset: 0x00442F65
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BAF7 RID: 47863 RVA: 0x00444D70 File Offset: 0x00442F70
		public override bool Render(RenderEvent E)
		{
			if (this.Danger)
			{
				int num = XRLCore.CurrentFrame % 30;
				if (num > 0 && num < 15)
				{
					E.Tile = null;
					E.RenderString = Directions.GetArrowForDirection(this.Direction);
					E.ColorString = "&R";
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600BAF8 RID: 47864 RVA: 0x00444DBD File Offset: 0x00442FBD
		public override bool FireEvent(Event E)
		{
			return true;
		}

		// Token: 0x04004B10 RID: 19216
		public string Direction;

		// Token: 0x04004B11 RID: 19217
		public bool Danger;
	}
}
