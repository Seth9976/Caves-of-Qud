using System;
using System.Text;
using XRL.Core;

namespace XRL.World
{
	// Token: 0x0200097A RID: 2426
	[Serializable]
	public class GlobalLocation : IEquatable<Cell>, IEquatable<GlobalLocation>, IComposite
	{
		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06006698 RID: 26264 RVA: 0x001FED62 File Offset: 0x001FCF62
		// (set) Token: 0x06006699 RID: 26265 RVA: 0x001FED6A File Offset: 0x001FCF6A
		public string World
		{
			get
			{
				return this._World;
			}
			set
			{
				this._World = value;
				this._ZoneID = null;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x0600669A RID: 26266 RVA: 0x001FED7A File Offset: 0x001FCF7A
		// (set) Token: 0x0600669B RID: 26267 RVA: 0x001FED82 File Offset: 0x001FCF82
		public int ParasangX
		{
			get
			{
				return this._ParasangX;
			}
			set
			{
				this._ParasangX = value;
				this._ZoneID = null;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x0600669C RID: 26268 RVA: 0x001FED92 File Offset: 0x001FCF92
		// (set) Token: 0x0600669D RID: 26269 RVA: 0x001FED9A File Offset: 0x001FCF9A
		public int ParasangY
		{
			get
			{
				return this._ParasangY;
			}
			set
			{
				this._ParasangY = value;
				this._ZoneID = null;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x0600669E RID: 26270 RVA: 0x001FEDAA File Offset: 0x001FCFAA
		// (set) Token: 0x0600669F RID: 26271 RVA: 0x001FEDB2 File Offset: 0x001FCFB2
		public int ZoneX
		{
			get
			{
				return this._ZoneX;
			}
			set
			{
				this._ZoneX = value;
				this._ZoneID = null;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x060066A0 RID: 26272 RVA: 0x001FEDC2 File Offset: 0x001FCFC2
		// (set) Token: 0x060066A1 RID: 26273 RVA: 0x001FEDCA File Offset: 0x001FCFCA
		public int ZoneY
		{
			get
			{
				return this._ZoneY;
			}
			set
			{
				this._ZoneY = value;
				this._ZoneID = null;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x060066A2 RID: 26274 RVA: 0x001FEDDA File Offset: 0x001FCFDA
		// (set) Token: 0x060066A3 RID: 26275 RVA: 0x001FEDE2 File Offset: 0x001FCFE2
		public int ZoneZ
		{
			get
			{
				return this._ZoneZ;
			}
			set
			{
				this._ZoneZ = value;
				this._ZoneID = null;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x060066A4 RID: 26276 RVA: 0x001FEDF4 File Offset: 0x001FCFF4
		// (set) Token: 0x060066A5 RID: 26277 RVA: 0x001FEE98 File Offset: 0x001FD098
		public string ZoneID
		{
			get
			{
				if (this._ZoneID == null)
				{
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					stringBuilder.Append(this.World ?? "NULL").Append('.').Append(this.ParasangX)
						.Append('.')
						.Append(this.ParasangY)
						.Append('.')
						.Append(this.ZoneX)
						.Append('.')
						.Append(this.ZoneY)
						.Append('.')
						.Append(this.ZoneZ);
					this._ZoneID = stringBuilder.ToString();
				}
				return this._ZoneID;
			}
			set
			{
				this._ZoneID = value;
				string[] array = this._ZoneID.Split('.', StringSplitOptions.None);
				this._World = array[0];
				this._ParasangX = Convert.ToInt32(array[1]);
				this._ParasangY = Convert.ToInt32(array[2]);
				this._ZoneX = Convert.ToInt32(array[3]);
				this._ZoneY = Convert.ToInt32(array[4]);
				this._ZoneZ = Convert.ToInt32(array[5]);
			}
		}

		// Token: 0x060066A6 RID: 26278 RVA: 0x001FEF0A File Offset: 0x001FD10A
		public GlobalLocation()
		{
		}

		/// <summary>
		/// Expected to be in the format 'JoppaWorld.11.22.1.1.10@37,22'
		/// </summary>
		/// <param name="GlobalLocationSpec" />
		// Token: 0x060066A7 RID: 26279 RVA: 0x001FEF14 File Offset: 0x001FD114
		public GlobalLocation(string GlobalLocationSpec)
		{
			string[] array = GlobalLocationSpec.Split('@', StringSplitOptions.None);
			string[] array2 = array[0].Split('.', StringSplitOptions.None);
			string[] array3 = array[1].Split(',', StringSplitOptions.None);
			this.SetCell(array2[0], Convert.ToInt32(array2[1]), Convert.ToInt32(array2[2]), Convert.ToInt32(array2[3]), Convert.ToInt32(array2[4]), Convert.ToInt32(array2[5]), Convert.ToInt32(array3[0]), Convert.ToInt32(array3[1]));
		}

		// Token: 0x060066A8 RID: 26280 RVA: 0x001FEF88 File Offset: 0x001FD188
		public GlobalLocation(string World, int ParasangX, int ParasangY, int ZoneX, int ZoneY, int ZoneZ, int CellX, int CellY)
		{
			this.SetCell(World, ParasangX, ParasangY, ZoneX, ZoneY, ZoneZ, CellX, CellY);
		}

		// Token: 0x060066A9 RID: 26281 RVA: 0x001FEFB0 File Offset: 0x001FD1B0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.ZoneID,
				"@",
				this.CellX.ToString(),
				",",
				this.CellY.ToString()
			});
		}

		// Token: 0x060066AA RID: 26282 RVA: 0x001FEFFD File Offset: 0x001FD1FD
		public bool IsCell()
		{
			return !this.IsClear();
		}

		// Token: 0x060066AB RID: 26283 RVA: 0x001FF008 File Offset: 0x001FD208
		public bool IsClear()
		{
			return this.World == null;
		}

		// Token: 0x060066AC RID: 26284 RVA: 0x001FF013 File Offset: 0x001FD213
		public void Clear()
		{
			this.World = null;
		}

		// Token: 0x060066AD RID: 26285 RVA: 0x001FF01C File Offset: 0x001FD21C
		public void SetCell(Cell C)
		{
			if (C == null)
			{
				this.World = null;
				return;
			}
			Zone parentZone = C.ParentZone;
			if (parentZone == null)
			{
				return;
			}
			this.SetCell(parentZone.ZoneWorld, parentZone.wX, parentZone.wY, parentZone.X, parentZone.Y, parentZone.Z, C.X, C.Y);
		}

		// Token: 0x060066AE RID: 26286 RVA: 0x001FF078 File Offset: 0x001FD278
		public override int GetHashCode()
		{
			if (this.World == null)
			{
				return 0;
			}
			return this.World.GetHashCode() ^ this.ParasangX ^ (this.ParasangY << 2) ^ (this.ZoneX << 4) ^ (this.ZoneY << 10) ^ (this.ZoneZ << 16) ^ (this.CellX << 22) ^ (this.CellY << 26);
		}

		// Token: 0x060066AF RID: 26287 RVA: 0x001FF0DB File Offset: 0x001FD2DB
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return this.World == null;
			}
			if (obj is Cell)
			{
				return this.Equals(obj as Cell);
			}
			return obj is GlobalLocation && this.Equals(obj as GlobalLocation);
		}

		// Token: 0x060066B0 RID: 26288 RVA: 0x001FF118 File Offset: 0x001FD318
		public bool Equals(Cell c)
		{
			if (c == null)
			{
				return this.World == null;
			}
			Zone parentZone = c.ParentZone;
			return parentZone != null && (c.X == this.CellX && c.Y == this.CellY && parentZone.ZoneWorld == this.World && parentZone.wX == this.ParasangX && parentZone.wY == this.ParasangY && parentZone.X == this.ZoneX && parentZone.Y == this.ZoneY) && parentZone.Z == this.ZoneZ;
		}

		// Token: 0x060066B1 RID: 26289 RVA: 0x001FF1B8 File Offset: 0x001FD3B8
		public bool Equals(GlobalLocation c)
		{
			if (c.World == null)
			{
				return this.World == null;
			}
			return c.CellX == this.CellX && c.CellY == this.CellY && c.World == this.World && c.ParasangX == this.ParasangX && c.ParasangY == this.ParasangY && c.ZoneX == this.ZoneX && c.ZoneY == this.ZoneY && c.ZoneZ == this.ZoneZ;
		}

		// Token: 0x060066B2 RID: 26290 RVA: 0x001FF24E File Offset: 0x001FD44E
		public static GlobalLocation FromZoneId(string ZoneId, int CellX = 40, int CellY = 23)
		{
			return new GlobalLocation
			{
				ZoneID = ZoneId,
				CellX = CellX,
				CellY = CellY
			};
		}

		// Token: 0x060066B3 RID: 26291 RVA: 0x001FF26C File Offset: 0x001FD46C
		public GlobalLocation(Cell C)
			: this()
		{
			if (C != null)
			{
				Zone parentZone = C.ParentZone;
				if (parentZone != null)
				{
					this.World = parentZone.ZoneWorld;
					this.ParasangX = parentZone.wX;
					this.ParasangY = parentZone.wY;
					this.ZoneX = parentZone.X;
					this.ZoneY = parentZone.Y;
					this.ZoneZ = parentZone.Z;
					this._ZoneID = parentZone.ZoneID;
				}
				this.CellX = C.X;
				this.CellY = C.Y;
			}
		}

		// Token: 0x060066B4 RID: 26292 RVA: 0x001FF2F8 File Offset: 0x001FD4F8
		public GlobalLocation(GameObject obj)
			: this(obj.CurrentCell)
		{
		}

		// Token: 0x060066B5 RID: 26293 RVA: 0x001FF308 File Offset: 0x001FD508
		public void SetCell(string World, int ParasangX, int ParasangY, int ZoneX, int ZoneY, int ZoneZ, int CellX, int CellY)
		{
			this._World = World;
			this._ParasangX = ParasangX;
			this._ParasangY = ParasangY;
			this._ZoneX = ZoneX;
			this._ZoneY = ZoneY;
			this._ZoneZ = ZoneZ;
			this.CellX = CellX;
			this.CellY = CellY;
			this._ZoneID = null;
		}

		// Token: 0x060066B6 RID: 26294 RVA: 0x001FF35C File Offset: 0x001FD55C
		public Cell ResolveCell()
		{
			if (this.IsClear())
			{
				return null;
			}
			XRLCore core = XRLCore.Core;
			Zone zone;
			if (core == null)
			{
				zone = null;
			}
			else
			{
				XRLGame game = core.Game;
				if (game == null)
				{
					zone = null;
				}
				else
				{
					ZoneManager zoneManager = game.ZoneManager;
					zone = ((zoneManager != null) ? zoneManager.GetZone(this.ZoneID) : null);
				}
			}
			Zone zone2 = zone;
			if (zone2 == null)
			{
				return null;
			}
			return zone2.GetCell(this.CellX, this.CellY);
		}

		// Token: 0x060066B7 RID: 26295 RVA: 0x001FF3BC File Offset: 0x001FD5BC
		public bool Is(Cell C)
		{
			if (C == null)
			{
				return false;
			}
			string zoneID = this.ZoneID;
			Zone parentZone = C.ParentZone;
			return !(zoneID != ((parentZone != null) ? parentZone.ZoneID : null)) && this.CellX == C.X && this.CellY == C.Y;
		}

		// Token: 0x060066B8 RID: 26296 RVA: 0x001FF410 File Offset: 0x001FD610
		public int PathDistanceTo(Cell C)
		{
			if (C == null)
			{
				return 9999999;
			}
			if (C.ParentZone.IsWorldMap())
			{
				return 9999999;
			}
			int num = C.ParentZone.wX * Definitions.Width * 80 + C.ParentZone.X * 80 + C.X;
			int num2 = C.ParentZone.wY * Definitions.Height * 25 + C.ParentZone.Y * 25 + C.Y;
			int z = C.ParentZone.Z;
			int num3 = this._ParasangX * Definitions.Width * 80 + this._ZoneX * 80 + this.CellX;
			int num4 = this._ParasangY * Definitions.Height * 25 + this._ZoneY * 25 + this.CellY;
			int zoneZ = this._ZoneZ;
			if (num == num3 && num2 == num4)
			{
				return Math.Abs(zoneZ - z);
			}
			return Math.Max(Math.Max(Math.Abs(num3 - num), Math.Abs(num4 - num2)), Math.Abs(zoneZ - z));
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x060066B9 RID: 26297 RVA: 0x001FF51D File Offset: 0x001FD71D
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060066BA RID: 26298 RVA: 0x001FF520 File Offset: 0x001FD720
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.CellX);
			Writer.WriteOptimized(this.CellY);
			Writer.WriteOptimized(this._World);
			Writer.WriteOptimized(this._ParasangX);
			Writer.WriteOptimized(this._ParasangY);
			Writer.WriteOptimized(this._ZoneX);
			Writer.WriteOptimized(this._ZoneY);
			Writer.WriteOptimized(this._ZoneZ);
		}

		// Token: 0x060066BB RID: 26299 RVA: 0x001FF590 File Offset: 0x001FD790
		public void Read(SerializationReader Reader)
		{
			this.CellX = Reader.ReadOptimizedInt32();
			this.CellY = Reader.ReadOptimizedInt32();
			this._World = Reader.ReadOptimizedString();
			this._ParasangX = Reader.ReadOptimizedInt32();
			this._ParasangY = Reader.ReadOptimizedInt32();
			this._ZoneX = Reader.ReadOptimizedInt32();
			this._ZoneY = Reader.ReadOptimizedInt32();
			this._ZoneZ = Reader.ReadOptimizedInt32();
		}

		// Token: 0x04002F4E RID: 12110
		public int CellX;

		// Token: 0x04002F4F RID: 12111
		public int CellY;

		// Token: 0x04002F50 RID: 12112
		private string _World;

		// Token: 0x04002F51 RID: 12113
		private int _ParasangX;

		// Token: 0x04002F52 RID: 12114
		private int _ParasangY;

		// Token: 0x04002F53 RID: 12115
		private int _ZoneX;

		// Token: 0x04002F54 RID: 12116
		private int _ZoneY;

		// Token: 0x04002F55 RID: 12117
		private int _ZoneZ;

		// Token: 0x04002F56 RID: 12118
		[NonSerialized]
		private string _ZoneID;
	}
}
