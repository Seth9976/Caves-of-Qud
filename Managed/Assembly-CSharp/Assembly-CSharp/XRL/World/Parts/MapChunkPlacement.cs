using System;
using XRL.EditorFormats.Map;

namespace XRL.World.Parts
{
	// Token: 0x02001302 RID: 4866
	[Serializable]
	public class MapChunkPlacement : IPart
	{
		// Token: 0x0600BEAD RID: 48813 RVA: 0x0045D118 File Offset: 0x0045B318
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BEAE RID: 48814 RVA: 0x0045D11B File Offset: 0x0045B31B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BEAF RID: 48815 RVA: 0x0045D130 File Offset: 0x0045B330
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				int num;
				MapChunkPlacement.PlaceFromFile(this.ParentObject.GetCurrentCell(), this.Map, this.Width, this.Height, int.TryParse(base.GetTag("ChunkPadding", "2"), out num) ? num : 2, this.Rotation, base.GetTag("ChunkHint", null), null, null);
			}
			return true;
		}

		// Token: 0x0600BEB0 RID: 48816 RVA: 0x0045D1A4 File Offset: 0x0045B3A4
		public static void PlaceFromFile(Cell Cell, string File, int Width, int Height, int Padding = 2, int Rotation = 0, string Hint = null, Action<Cell> PreAction = null, Action<Cell> PostAction = null)
		{
			MapChunkPlacement.PlaceFromFile(Cell.ParentZone, File, Cell.X, Cell.Y, Width, Height, Padding, Rotation, Hint, PreAction, PostAction);
		}

		// Token: 0x0600BEB1 RID: 48817 RVA: 0x0045D1D8 File Offset: 0x0045B3D8
		public static void PlaceFromFile(Zone Z, string File, int X, int Y, int Width, int Height, int Padding = 2, int Rotation = 0, string Hint = null, Action<Cell> PreAction = null, Action<Cell> PostAction = null)
		{
			MapFile mapFile = MapFile.Resolve(File, true);
			int num = ((Rotation % 2 == 0) ? Width : Height);
			int num2 = ((Rotation % 2 == 0) ? Height : Width);
			if (X + num + Padding >= Z.Width)
			{
				X -= X + num + Padding - Z.Width;
			}
			if (Y + num2 + Padding >= Z.Height)
			{
				Y -= Y + num2 + Padding - Z.Height;
			}
			if (Hint == "Center")
			{
				X = Z.Width / 2 - num / 2;
				Y = Z.Height / 2 - num2 / 2;
			}
			if (PreAction == null)
			{
				PreAction = delegate(Cell X)
				{
					X.ClearWalls();
				};
			}
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					Cell cell = Z.GetCell(X + j, Y + i);
					if (cell != null)
					{
						int num3;
						int num4;
						MapChunkPlacement.Translate(j, i, Rotation, Width, Height, out num3, out num4);
						mapFile.Cells[num3, num4].ApplyTo(cell, true, PreAction, PostAction, null, null, null, null, null);
					}
				}
			}
		}

		// Token: 0x0600BEB2 RID: 48818 RVA: 0x0045D2F8 File Offset: 0x0045B4F8
		public static void Translate(int X, int Y, int Rotation, int Width, int Height, out int TX, out int TY)
		{
			Width--;
			Height--;
			switch (Rotation)
			{
			case 1:
				TX = Y;
				TY = Height - X;
				return;
			case 2:
				TX = Width - X;
				TY = Height - Y;
				return;
			case 3:
				TX = Width - Y;
				TY = X;
				return;
			default:
				TX = X;
				TY = Y;
				return;
			}
		}

		// Token: 0x04004CC1 RID: 19649
		public string Map;

		// Token: 0x04004CC2 RID: 19650
		public int Width;

		// Token: 0x04004CC3 RID: 19651
		public int Height;

		/// <summary>Clockwise 90-degree rotation of RPM contents.</summary>
		// Token: 0x04004CC4 RID: 19652
		public int Rotation;
	}
}
