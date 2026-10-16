using System;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B05 RID: 2821
	public class TunnelMaker
	{
		// Token: 0x060070A4 RID: 28836 RVA: 0x00288A10 File Offset: 0x00286C10
		public TunnelMaker(int _Width, int _Height, string _StartY, string _EndY, string _Directions)
		{
			this.Width = _Width;
			this.Height = _Height;
			this.StartY = _StartY.RollCached();
			this.EndY = _EndY.RollCached();
			this.Directions = _Directions;
			this.Map = new string[this.Width, this.Height];
			try
			{
				this.CreateTunnel();
			}
			catch
			{
				this.CreateTunnel();
			}
		}

		// Token: 0x060070A5 RID: 28837 RVA: 0x00288A8C File Offset: 0x00286C8C
		public bool CreateTunnel()
		{
			for (int i = 0; i < 100; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					for (int k = 0; k < this.Height; k++)
					{
						this.Map[j, k] = "";
					}
				}
				int num = 0;
				this.StartX = 0;
				this.EndX = this.Width - 1;
				if (this.Directions.Contains("W"))
				{
					num = this.Width - 1;
					this.StartX = num;
					this.EndX = 0;
				}
				int num2 = this.StartY;
				int num3 = 10;
				for (;;)
				{
					num3--;
					if (num3 == 0)
					{
						break;
					}
					string text = this.Directions.GetRandomElement(null).ToString();
					int num4 = num;
					int num5 = num2;
					XRL.Rules.Directions.ApplyDirection(text, ref num4, ref num5, 1);
					if (num4 >= 0 && num5 >= 0 && num4 < this.Width && num5 < this.Height)
					{
						if (this.Map[num4, num5] == "")
						{
							ref string ptr = ref this.Map[num, num2];
							ptr += text;
							num = num4;
							num2 = num5;
							this.Map[num, num2] = XRL.Rules.Directions.GetOppositeDirection(text);
							num3 = 10;
						}
						if (num == this.EndX && num2 == this.EndY)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0400341D RID: 13341
		public int Width;

		// Token: 0x0400341E RID: 13342
		public int Height;

		// Token: 0x0400341F RID: 13343
		public int StartX;

		// Token: 0x04003420 RID: 13344
		public int StartY;

		// Token: 0x04003421 RID: 13345
		public int EndX;

		// Token: 0x04003422 RID: 13346
		public int EndY;

		// Token: 0x04003423 RID: 13347
		public string Directions;

		// Token: 0x04003424 RID: 13348
		public string[,] Map;
	}
}
