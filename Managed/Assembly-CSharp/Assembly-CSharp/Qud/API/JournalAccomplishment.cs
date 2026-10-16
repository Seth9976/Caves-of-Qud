using System;
using ConsoleLib.Console;
using XRL;
using XRL.World;

namespace Qud.API
{
	// Token: 0x02000400 RID: 1024
	[Serializable]
	public class JournalAccomplishment : IBaseJournalEntry
	{
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06002678 RID: 9848 RVA: 0x000C5C95 File Offset: 0x000C3E95
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x000C5C98 File Offset: 0x000C3E98
		public void SetScreenshot(int x, int y, IRenderable renderable)
		{
			if (x == 5)
			{
			}
			if (this.Screenshot == null)
			{
				this.Screenshot = new SnapshotRenderable[45];
			}
			this.Screenshot[x + y * 9] = new SnapshotRenderable(renderable);
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x000C5CCC File Offset: 0x000C3ECC
		public void UpdateScreenshot()
		{
			if (!The.Player.IsNowhere())
			{
				int num = Math.Clamp(The.Player.CurrentCell.X - 4, 0, The.Player.CurrentZone.Width - 9);
				int num2 = Math.Clamp(The.Player.CurrentCell.Y - 2, 0, The.Player.CurrentZone.Height - 5);
				using (RenderEvent renderEvent = RenderEvent.Pool.Get())
				{
					Zone currentZone = The.Player.CurrentZone;
					for (int i = num2; i < num2 + 5; i++)
					{
						for (int j = num; j < num + 9; j++)
						{
							currentZone.GetCell(j, i).Render(renderEvent, true, LightLevel.Light, true, false, false, null);
							this.SetScreenshot(j - num, i - num2, renderEvent);
						}
					}
				}
			}
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x000C5DB8 File Offset: 0x000C3FB8
		public override void Write(SerializationWriter Writer)
		{
			base.Write(Writer);
			Writer.WriteOptimized(this.Time);
			Writer.WriteOptimized(this.Category);
			Writer.WriteOptimized(this.MuralText);
			Writer.WriteOptimized(this.GospelText);
			Writer.WriteOptimized(this.AggregateWith);
			Writer.WriteOptimized((int)this.MuralCategory);
			Writer.WriteOptimized((int)this.MuralWeight);
			SnapshotRenderable[] screenshot = this.Screenshot;
			int num = ((screenshot != null) ? screenshot.Length : 0);
			Writer.WriteOptimized(num);
			for (int i = 0; i < num; i++)
			{
				Writer.WriteComposite<SnapshotRenderable>(this.Screenshot[i]);
			}
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x000C5E54 File Offset: 0x000C4054
		public override void Read(SerializationReader Reader)
		{
			base.Read(Reader);
			this.Time = Reader.ReadOptimizedInt64();
			this.Category = Reader.ReadOptimizedString();
			this.MuralText = Reader.ReadOptimizedString();
			this.GospelText = Reader.ReadOptimizedString();
			this.AggregateWith = Reader.ReadOptimizedString();
			this.MuralCategory = (MuralCategory)Reader.ReadOptimizedInt32();
			this.MuralWeight = (MuralWeight)Reader.ReadOptimizedInt32();
			int num = Reader.ReadOptimizedInt32();
			if (num <= 0)
			{
				this.Screenshot = Array.Empty<SnapshotRenderable>();
				return;
			}
			this.Screenshot = new SnapshotRenderable[num];
			for (int i = 0; i < num; i++)
			{
				this.Screenshot[i] = Reader.ReadComposite<SnapshotRenderable>();
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600267D RID: 9853 RVA: 0x000C5EF9 File Offset: 0x000C40F9
		// (set) Token: 0x0600267E RID: 9854 RVA: 0x000C5F01 File Offset: 0x000C4101
		[Obsolete]
		public string category
		{
			get
			{
				return this.Category;
			}
			set
			{
				this.Category = value;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x000C5F0A File Offset: 0x000C410A
		// (set) Token: 0x06002680 RID: 9856 RVA: 0x000C5F12 File Offset: 0x000C4112
		[Obsolete]
		public long time
		{
			get
			{
				return this.Time;
			}
			set
			{
				this.Time = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x000C5F1B File Offset: 0x000C411B
		// (set) Token: 0x06002682 RID: 9858 RVA: 0x000C5F23 File Offset: 0x000C4123
		[Obsolete]
		public string muralText
		{
			get
			{
				return this.MuralText;
			}
			set
			{
				this.MuralText = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06002683 RID: 9859 RVA: 0x000C5F2C File Offset: 0x000C412C
		// (set) Token: 0x06002684 RID: 9860 RVA: 0x000C5F34 File Offset: 0x000C4134
		[Obsolete]
		public MuralCategory muralCategory
		{
			get
			{
				return this.MuralCategory;
			}
			set
			{
				this.MuralCategory = value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06002685 RID: 9861 RVA: 0x000C5F3D File Offset: 0x000C413D
		// (set) Token: 0x06002686 RID: 9862 RVA: 0x000C5F45 File Offset: 0x000C4145
		[Obsolete]
		public MuralWeight muralWeight
		{
			get
			{
				return this.MuralWeight;
			}
			set
			{
				this.MuralWeight = value;
			}
		}

		// Token: 0x04001326 RID: 4902
		public long Time;

		// Token: 0x04001327 RID: 4903
		public string Category;

		// Token: 0x04001328 RID: 4904
		public string MuralText;

		// Token: 0x04001329 RID: 4905
		public string GospelText;

		// Token: 0x0400132A RID: 4906
		public string AggregateWith;

		// Token: 0x0400132B RID: 4907
		public MuralCategory MuralCategory;

		// Token: 0x0400132C RID: 4908
		public MuralWeight MuralWeight;

		// Token: 0x0400132D RID: 4909
		public SnapshotRenderable[] Screenshot;

		// Token: 0x0400132E RID: 4910
		public const int SCREENSHOT_WIDTH = 9;

		// Token: 0x0400132F RID: 4911
		public const int SCREENSHOT_HEIGHT = 5;
	}
}
