using System;
using System.Linq;
using System.Text;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A6C RID: 2668
	public abstract class IConnectionBuilder
	{
		// Token: 0x06006E51 RID: 28241 RVA: 0x0025B26B File Offset: 0x0025946B
		public virtual int GetMouthX(Zone Z)
		{
			return Stat.Rnd.Next(this.Range, Z.Width - this.Range);
		}

		// Token: 0x06006E52 RID: 28242 RVA: 0x0025B28A File Offset: 0x0025948A
		public virtual int GetMouthY(Zone Z)
		{
			return Stat.Rnd.Next(this.Range, Z.Height - this.Range);
		}

		// Token: 0x06006E53 RID: 28243 RVA: 0x0025B2AC File Offset: 0x002594AC
		public bool ConnectionMouth(Zone Z, string Type, string Direction, string Suffix = "Mouth")
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(Type).Append(Direction).Append(Suffix);
			string full = stringBuilder.ToString();
			if (The.ZoneManager.GetZoneConnections(Z.ZoneID).Any<ZoneConnection>((ZoneConnection ZC) => ZC.Type == full))
			{
				return true;
			}
			string text;
			string text2;
			int num;
			int num2;
			int num3;
			int num4;
			if (!(Direction == "North"))
			{
				if (!(Direction == "East"))
				{
					if (!(Direction == "South"))
					{
						if (!(Direction == "West"))
						{
							MetricsManager.LogError("Invalid connection direction: " + Direction);
							return true;
						}
						text = stringBuilder.Clear().Append(Type).Append("East")
							.Append(Suffix)
							.ToString();
						if (this.MatchConnection(Z, text, text2 = "w", full, 0, -1))
						{
							return true;
						}
						num = 0;
						num2 = ((this.Y < 0) ? this.GetMouthY(Z) : this.Y);
						num3 = Z.Width - 1;
						num4 = num2;
					}
					else
					{
						text = stringBuilder.Clear().Append(Type).Append("North")
							.Append(Suffix)
							.ToString();
						if (this.MatchConnection(Z, text, text2 = "s", full, -1, Z.Height - 1))
						{
							return true;
						}
						num = ((this.X < 0) ? this.GetMouthX(Z) : this.X);
						num2 = Z.Height - 1;
						num3 = num;
						num4 = 0;
					}
				}
				else
				{
					text = stringBuilder.Clear().Append(Type).Append("West")
						.Append(Suffix)
						.ToString();
					if (this.MatchConnection(Z, text, text2 = "e", full, Z.Width - 1, -1))
					{
						return true;
					}
					num = Z.Width - 1;
					num2 = ((this.Y < 0) ? this.GetMouthY(Z) : this.Y);
					num3 = 0;
					num4 = num2;
				}
			}
			else
			{
				text = stringBuilder.Clear().Append(Type).Append("South")
					.Append(Suffix)
					.ToString();
				if (this.MatchConnection(Z, text, text2 = "n", full, -1, 0))
				{
					return true;
				}
				num = ((this.X < 0) ? this.GetMouthX(Z) : this.X);
				num2 = 0;
				num3 = num;
				num4 = Z.Height - 1;
			}
			Z.GetCell(num, num2).Clear(null, false, false, null);
			Z.CacheZoneConnection("-", num, num2, full, null);
			Z.CacheZoneConnection(text2, num3, num4, text, null);
			return true;
		}

		// Token: 0x06006E54 RID: 28244 RVA: 0x0025B550 File Offset: 0x00259750
		public bool MatchConnection(Zone Z, string Type, string Direction, string Origin, int X, int Y)
		{
			string zoneFromIDAndDirection = The.ZoneManager.GetZoneFromIDAndDirection(Z.ZoneID, Direction);
			if (!The.ZoneManager.IsZoneBuilt(zoneFromIDAndDirection))
			{
				return false;
			}
			foreach (ZoneConnection zoneConnection in The.ZoneManager.GetZoneConnections(zoneFromIDAndDirection))
			{
				if (zoneConnection.Type == Type)
				{
					X = ((X == -1) ? zoneConnection.X : X);
					Y = ((Y == -1) ? zoneConnection.Y : Y);
					Z.GetCell(X, Y).Clear(null, false, false, null);
					Z.CacheZoneConnection("-", X, Y, Origin, null);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006E55 RID: 28245 RVA: 0x0025B620 File Offset: 0x00259820
		public virtual void GetStartPosition(Zone Z, out int X, out int Y)
		{
			int num = 0;
			do
			{
				X = Stat.Random(5, 75);
				Y = Stat.Random(5, 15);
			}
			while (!Z.GetCell(X, Y).IsEmpty() && num++ < 1000);
		}

		// Token: 0x06006E56 RID: 28246 RVA: 0x0025B660 File Offset: 0x00259860
		public bool ConnectionStart(Zone Z, string Type, string Suffix = "Mouth")
		{
			string full = Event.NewStringBuilder(Type).Append("Start").Append(Suffix)
				.ToString();
			if (The.ZoneManager.GetZoneConnections(Z.ZoneID).Any<ZoneConnection>((ZoneConnection ZC) => ZC.Type == full))
			{
				return true;
			}
			if (this.X < 0 || this.Y < 0)
			{
				this.GetStartPosition(Z, out this.X, out this.Y);
			}
			Z.CacheZoneConnection("-", this.X, this.Y, full, null);
			return true;
		}

		// Token: 0x04003277 RID: 12919
		public int X = -1;

		// Token: 0x04003278 RID: 12920
		public int Y = -1;

		// Token: 0x04003279 RID: 12921
		public int Range = 5;
	}
}
