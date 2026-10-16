using System;

namespace Genkit
{
	// Token: 0x02000381 RID: 897
	[Serializable]
	public class MazeCell3D
	{
		// Token: 0x060021C5 RID: 8645 RVA: 0x000A81BC File Offset: 0x000A63BC
		public MazeCell3D()
		{
			this.N = false;
			this.S = false;
			this.E = false;
			this.W = false;
			this.U = false;
			this.D = false;
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x000A81EE File Offset: 0x000A63EE
		public MazeCell3D(bool Init)
		{
			this.N = Init;
			this.S = Init;
			this.E = Init;
			this.W = Init;
			this.U = Init;
			this.D = Init;
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x000A8220 File Offset: 0x000A6420
		public bool GetBit(int n)
		{
			return ((int)this.Dir & (1 << n)) != 0;
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x000A8232 File Offset: 0x000A6432
		public void SetBit(int n, bool val)
		{
			if (val)
			{
				this.Dir |= (byte)(1 << n);
				return;
			}
			if (this.GetBit(n))
			{
				this.Dir -= (byte)(1 << n);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060021C9 RID: 8649 RVA: 0x000A826B File Offset: 0x000A646B
		// (set) Token: 0x060021CA RID: 8650 RVA: 0x000A8274 File Offset: 0x000A6474
		public bool N
		{
			get
			{
				return this.GetBit(0);
			}
			set
			{
				this.SetBit(0, value);
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x000A827E File Offset: 0x000A647E
		// (set) Token: 0x060021CC RID: 8652 RVA: 0x000A8287 File Offset: 0x000A6487
		public bool S
		{
			get
			{
				return this.GetBit(1);
			}
			set
			{
				this.SetBit(1, value);
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x000A8291 File Offset: 0x000A6491
		// (set) Token: 0x060021CE RID: 8654 RVA: 0x000A829A File Offset: 0x000A649A
		public bool E
		{
			get
			{
				return this.GetBit(2);
			}
			set
			{
				this.SetBit(2, value);
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x000A82A4 File Offset: 0x000A64A4
		// (set) Token: 0x060021D0 RID: 8656 RVA: 0x000A82AD File Offset: 0x000A64AD
		public bool W
		{
			get
			{
				return this.GetBit(3);
			}
			set
			{
				this.SetBit(3, value);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060021D1 RID: 8657 RVA: 0x000A82B7 File Offset: 0x000A64B7
		// (set) Token: 0x060021D2 RID: 8658 RVA: 0x000A82C0 File Offset: 0x000A64C0
		public bool U
		{
			get
			{
				return this.GetBit(4);
			}
			set
			{
				this.SetBit(4, value);
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x000A82CA File Offset: 0x000A64CA
		// (set) Token: 0x060021D4 RID: 8660 RVA: 0x000A82D3 File Offset: 0x000A64D3
		public bool D
		{
			get
			{
				return this.GetBit(5);
			}
			set
			{
				this.SetBit(5, value);
			}
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x000A82DD File Offset: 0x000A64DD
		public bool AnyOpen()
		{
			return this.N || this.S || this.E || this.W || this.U || this.D;
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x000A830F File Offset: 0x000A650F
		public bool DeadEnd()
		{
			return this.NorthOnly() || this.SouthOnly() || this.EastOnly() || this.WestOnly() || this.UpOnly() || this.DownOnly();
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x000A8341 File Offset: 0x000A6541
		public bool UpOnly()
		{
			return !this.N && !this.S && !this.E && !this.W && this.U && !this.D;
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x000A8376 File Offset: 0x000A6576
		public bool DownOnly()
		{
			return this.N && !this.S && !this.E && !this.W && !this.U && this.D;
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x000A83A8 File Offset: 0x000A65A8
		public bool NorthOnly()
		{
			return this.N && !this.S && !this.E && !this.W && !this.U && !this.D;
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x000A83DD File Offset: 0x000A65DD
		public bool SouthOnly()
		{
			return !this.N && this.S && !this.E && !this.W && !this.U && !this.D;
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000A8412 File Offset: 0x000A6612
		public bool EastOnly()
		{
			return !this.N && !this.S && this.E && !this.W && !this.U && !this.D;
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x000A8447 File Offset: 0x000A6647
		public bool WestOnly()
		{
			return !this.N && !this.S && !this.E && this.W && !this.U && !this.D;
		}

		// Token: 0x040010F2 RID: 4338
		public byte Dir;

		// Token: 0x040010F3 RID: 4339
		public short x;

		// Token: 0x040010F4 RID: 4340
		public short y;

		// Token: 0x040010F5 RID: 4341
		public short z;
	}
}
