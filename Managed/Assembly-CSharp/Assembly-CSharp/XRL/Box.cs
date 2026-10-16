using System;
using System.Collections.Generic;
using Genkit;
using XRL.Rules;
using XRL.World;

namespace XRL
{
	// Token: 0x020004FC RID: 1276
	[Serializable]
	public class Box : IComposite
	{
		// Token: 0x06003702 RID: 14082 RVA: 0x0010A2F3 File Offset: 0x001084F3
		public Box()
		{
			this.x1 = 0;
			this.y1 = 0;
			this.x2 = 0;
			this.y2 = 0;
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x0010A318 File Offset: 0x00108518
		public Box(int _x1, int _y1, int _x2, int _y2)
		{
			this.x1 = _x1;
			this.y1 = _y1;
			this.x2 = _x2;
			this.y2 = _y2;
			if (this.x2 < this.x1)
			{
				int num = this.x1;
				this.x1 = this.x2;
				this.x2 = num;
			}
			if (this.y2 < this.y1)
			{
				int num2 = this.y1;
				this.y1 = this.y2;
				this.y2 = num2;
			}
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x0010A398 File Offset: 0x00108598
		public static implicit operator Rect2D(Box B)
		{
			return new Rect2D(B.x1, B.y1, B.x2, B.y2);
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x0010A3B7 File Offset: 0x001085B7
		public static Box fromRect(Rect2D rect)
		{
			return new Box(rect.x1, rect.y1, rect.x2, rect.y2);
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06003706 RID: 14086 RVA: 0x0010A3D6 File Offset: 0x001085D6
		public Rect2D rect
		{
			get
			{
				return new Rect2D(this.x1, this.y1, this.x2, this.y2);
			}
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x0010A3F8 File Offset: 0x001085F8
		public Box clamp(int minx, int miny, int maxx, int maxy)
		{
			Box box = new Box(this.x1, this.y1, this.x2, this.y2);
			if (box.x1 < minx)
			{
				box.x1 = minx;
			}
			if (box.x2 < minx)
			{
				box.x2 = minx;
			}
			if (box.y1 < miny)
			{
				box.y1 = miny;
			}
			if (box.y2 < miny)
			{
				box.y2 = miny;
			}
			if (box.x1 > maxx)
			{
				box.x1 = maxx;
			}
			if (box.x2 > maxx)
			{
				box.x2 = maxx;
			}
			if (box.y1 > maxy)
			{
				box.y1 = maxy;
			}
			if (box.y2 > maxy)
			{
				box.y2 = maxy;
			}
			return box;
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06003708 RID: 14088 RVA: 0x0010A4A8 File Offset: 0x001086A8
		public Point RandomPoint
		{
			get
			{
				return new Point(0, 0)
				{
					X = Stat.Random(this.x1, this.x2),
					Y = Stat.Random(this.y1, this.y2)
				};
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06003709 RID: 14089 RVA: 0x0010A4DF File Offset: 0x001086DF
		public int Volume
		{
			get
			{
				return (this.x2 - this.x1) * (this.y2 - this.y1);
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600370A RID: 14090 RVA: 0x0010A4FC File Offset: 0x001086FC
		public bool Valid
		{
			get
			{
				return this.x1 >= 0 && this.x1 <= 79 && this.x2 >= 0 && this.x2 <= 79 && this.y1 >= 0 && this.y1 <= 24 && this.y2 >= 0 && this.y2 <= 24;
			}
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x0010A55A File Offset: 0x0010875A
		public bool Equals(Box B)
		{
			return B.x1 == this.x1 && B.x2 == this.x2 && B.y1 == this.y1 && B.y2 == this.y2;
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x0010A596 File Offset: 0x00108796
		public Box Grow(int Size)
		{
			return new Box(this.x1 - Size, this.y1 - Size, this.x2 + Size, this.y2 + Size);
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600370D RID: 14093 RVA: 0x0010A5BD File Offset: 0x001087BD
		public Location2D center
		{
			get
			{
				return Location2D.Get(this.MidX, this.MidY);
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600370E RID: 14094 RVA: 0x0010A5D0 File Offset: 0x001087D0
		public int MidX
		{
			get
			{
				return (this.x2 + this.x1) / 2;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x0600370F RID: 14095 RVA: 0x0010A5E1 File Offset: 0x001087E1
		public int MidY
		{
			get
			{
				return (this.y2 + this.y1) / 2;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06003710 RID: 14096 RVA: 0x0010A5F2 File Offset: 0x001087F2
		public int Width
		{
			get
			{
				return this.x2 - this.x1 + 1;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06003711 RID: 14097 RVA: 0x0010A603 File Offset: 0x00108803
		public int Height
		{
			get
			{
				return this.y2 - this.y1 + 1;
			}
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x0010A614 File Offset: 0x00108814
		public bool contains(Location2D location)
		{
			return this.contains(location.X, location.Y);
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x0010A628 File Offset: 0x00108828
		public bool contains(int x, int y)
		{
			return x >= this.x1 && x <= this.x2 && y >= this.y1 && y <= this.y2;
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x0010A657 File Offset: 0x00108857
		public IEnumerable<Location2D> contents()
		{
			int num;
			for (int x = this.x1; x <= this.x2; x = num + 1)
			{
				for (int y = this.y1; y <= this.y2; y = num + 1)
				{
					yield return Location2D.Get(x, y);
					num = y;
				}
				num = x;
			}
			yield break;
		}

		// Token: 0x04001B63 RID: 7011
		public int x1;

		// Token: 0x04001B64 RID: 7012
		public int y1;

		// Token: 0x04001B65 RID: 7013
		public int x2;

		// Token: 0x04001B66 RID: 7014
		public int y2;
	}
}
