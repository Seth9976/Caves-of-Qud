using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x0200127F RID: 4735
	[Serializable]
	public class RandomFlower : IPart
	{
		// Token: 0x0600BB7E RID: 47998 RVA: 0x00447BB3 File Offset: 0x00445DB3
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BB7F RID: 47999 RVA: 0x00447BB6 File Offset: 0x00445DB6
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ObjectCreated");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BB80 RID: 48000 RVA: 0x00447BCC File Offset: 0x00445DCC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ObjectCreated")
			{
				Render render = this.ParentObject.Render;
				render.Tile = "terrain/tile_flowers" + Stat.Random(1, 2).ToString() + ".bmp";
				int num = Stat.Random(1, 7);
				if (num == 1)
				{
					render.ColorString = "&R";
				}
				else if (num == 2)
				{
					render.ColorString = "&M";
				}
				else if (num == 3)
				{
					render.ColorString = "&B";
				}
				else if (num == 4)
				{
					render.ColorString = "&C";
				}
				else if (num == 5)
				{
					render.ColorString = "&Y";
				}
				else if (num == 6)
				{
					render.ColorString = "&G";
				}
				else if (num == 7)
				{
					render.ColorString = "&W";
				}
				if (Stat.Random(0, 1) == 0)
				{
					render.ColorString = render.ColorString.ToLower();
				}
				int num2 = Stat.Random(1, 5);
				if (num2 == 1)
				{
					render.RenderString = ",";
				}
				if (num2 == 2)
				{
					render.RenderString = ".";
				}
				if (num2 == 3)
				{
					render.RenderString = "ù";
				}
				if (num2 == 4)
				{
					render.RenderString = "ú";
				}
				this.ParentObject.RemovePart(this);
			}
			return base.FireEvent(E);
		}
	}
}
