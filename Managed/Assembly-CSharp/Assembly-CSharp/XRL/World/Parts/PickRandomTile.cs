using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012E1 RID: 4833
	[Serializable]
	public class PickRandomTile : IPart
	{
		// Token: 0x0600BE24 RID: 48676 RVA: 0x0045AD2E File Offset: 0x00458F2E
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600BE25 RID: 48677 RVA: 0x0045AD31 File Offset: 0x00458F31
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID || ID == SingletonEvent<RefreshTileEvent>.ID;
		}

		// Token: 0x0600BE26 RID: 48678 RVA: 0x0045AD4F File Offset: 0x00458F4F
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.RandomizeTile();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE27 RID: 48679 RVA: 0x0045AD5E File Offset: 0x00458F5E
		public override bool HandleEvent(RefreshTileEvent E)
		{
			this.RandomizeTile();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE28 RID: 48680 RVA: 0x0045AD6D File Offset: 0x00458F6D
		public void RandomizeTile()
		{
			this.ParentObject.Render.Tile = PickRandomTile.RandomVariant(this.Tile, null);
		}

		// Token: 0x0600BE29 RID: 48681 RVA: 0x0045AD8C File Offset: 0x00458F8C
		public static string RandomVariant(string Source, Random R = null)
		{
			string text = Source;
			for (int num = Source.IndexOf('~'); num != -1; num = text.IndexOf('~', num))
			{
				int num2 = num + ((Source[num + 1] == '#') ? 2 : 1);
				int num3 = text.IndexOf('~', num2);
				if (num3 == -1)
				{
					break;
				}
				int num4 = text.IndexOf('-', num2, num3 - num2);
				int num5;
				int num6;
				if (num4 == -1 || !int.TryParse(text.Substring(num2, num4 - num2), out num5) || !int.TryParse(text.Substring(num4 + 1, num3 - num4 - 1), out num6))
				{
					break;
				}
				if (R == null)
				{
					R = Stat.Rnd;
				}
				int num7 = R.Next(num5, num6 + 1);
				text = text.Remove(num, num3 - num + 1);
				text = text.Insert(num, num7.ToString());
			}
			return text;
		}

		// Token: 0x04004C93 RID: 19603
		public string Tile = "";
	}
}
