using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A4F RID: 2639
	[Serializable]
	public sealed class RoomData : IComposite
	{
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06006DE9 RID: 28137 RVA: 0x00254DCC File Offset: 0x00252FCC
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006DEA RID: 28138 RVA: 0x00254DD0 File Offset: 0x00252FD0
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Left);
			Writer.WriteOptimized(this.Right);
			Writer.WriteOptimized(this.Top);
			Writer.WriteOptimized(this.Bottom);
			Writer.WriteOptimized(this.Width);
			Writer.WriteOptimized(this.Height);
			Writer.WriteOptimized(this.Size);
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					Writer.WriteOptimized(this.Room[i, j]);
				}
			}
		}

		// Token: 0x06006DEB RID: 28139 RVA: 0x00254E68 File Offset: 0x00253068
		public void Read(SerializationReader Reader)
		{
			this.Left = Reader.ReadOptimizedInt32();
			this.Right = Reader.ReadOptimizedInt32();
			this.Top = Reader.ReadOptimizedInt32();
			this.Bottom = Reader.ReadOptimizedInt32();
			this.Width = Reader.ReadOptimizedInt32();
			this.Height = Reader.ReadOptimizedInt32();
			this.Size = Reader.ReadOptimizedInt32();
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					this.Room[i, j] = Reader.ReadOptimizedInt32();
				}
			}
		}

		// Token: 0x04003250 RID: 12880
		public int Left = 999;

		// Token: 0x04003251 RID: 12881
		public int Right;

		// Token: 0x04003252 RID: 12882
		public int Top = 999;

		// Token: 0x04003253 RID: 12883
		public int Bottom;

		// Token: 0x04003254 RID: 12884
		public int Width;

		// Token: 0x04003255 RID: 12885
		public int Height;

		// Token: 0x04003256 RID: 12886
		public int Size;

		// Token: 0x04003257 RID: 12887
		public int[,] Room;
	}
}
