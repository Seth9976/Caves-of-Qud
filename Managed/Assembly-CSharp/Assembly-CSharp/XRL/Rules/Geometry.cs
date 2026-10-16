using System;
using System.Collections.Generic;
using Genkit;
using UnityEngine;
using XRL.World;

namespace XRL.Rules
{
	// Token: 0x020005DD RID: 1501
	public class Geometry
	{
		// Token: 0x0600406C RID: 16492 RVA: 0x0013E180 File Offset: 0x0013C380
		public static int Distance(Point P1, Point P2)
		{
			int num = P1.X - P2.X;
			int num2 = P1.Y - P2.Y;
			return (int)Math.Sqrt((double)(num * num + num2 * num2));
		}

		// Token: 0x0600406D RID: 16493 RVA: 0x0013E1B8 File Offset: 0x0013C3B8
		public static int Distance(int X1, int Y1, int X2, int Y2)
		{
			int num = X1 - X2;
			int num2 = Y1 - Y2;
			return (int)Math.Sqrt((double)(num * num + num2 * num2));
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x0013E1DC File Offset: 0x0013C3DC
		public static int Distance(int X1, int Y1, XRL.World.GameObject obj)
		{
			int num = X1 - obj.Physics.CurrentCell.X;
			int num2 = Y1 - obj.Physics.CurrentCell.Y;
			return (int)Math.Sqrt((double)(num * num + num2 * num2));
		}

		// Token: 0x0600406F RID: 16495 RVA: 0x0013E21C File Offset: 0x0013C41C
		public static bool TestAngleTo(Location2D fulcrum, Location2D from, Location2D to, float MaxAngle)
		{
			Vector2 vector = new Vector2((float)(from.X - fulcrum.X), (float)(from.Y - fulcrum.Y));
			Vector2 vector2 = new Vector2((float)(to.X - fulcrum.X), (float)(to.Y - fulcrum.Y));
			if (Vector2.Angle(vector, vector2) <= MaxAngle)
			{
				return true;
			}
			float num = 0.25f;
			Vector2 vector3 = new Vector2((float)(from.X - fulcrum.X), (float)(from.Y - fulcrum.Y));
			vector2 = new Vector2((float)(to.X - fulcrum.X), (float)(to.Y - fulcrum.Y));
			if (Vector2.Angle(vector3, vector2) <= MaxAngle)
			{
				return true;
			}
			Vector2 vector4 = new Vector2((float)(from.X - fulcrum.X) - num, (float)(from.Y - fulcrum.Y) - num);
			vector2 = new Vector2((float)(to.X - fulcrum.X) - num, (float)(to.Y - fulcrum.Y) - num);
			if (Vector2.Angle(vector4, vector2) <= MaxAngle)
			{
				return true;
			}
			Vector2 vector5 = new Vector2((float)(from.X - fulcrum.X) - num, (float)(from.Y - fulcrum.Y) - num);
			vector2 = new Vector2((float)(to.X - fulcrum.X) + num, (float)(to.Y - fulcrum.Y) + num);
			if (Vector2.Angle(vector5, vector2) <= MaxAngle)
			{
				return true;
			}
			Vector2 vector6 = new Vector2((float)(from.X - fulcrum.X) + num, (float)(from.Y - fulcrum.Y) + num);
			vector2 = new Vector2((float)(to.X - fulcrum.X) - num, (float)(to.Y - fulcrum.Y) - num);
			if (Vector2.Angle(vector6, vector2) <= MaxAngle)
			{
				return true;
			}
			Vector2 vector7 = new Vector2((float)(from.X - fulcrum.X) - num, (float)(from.Y - fulcrum.Y) + num);
			vector2 = new Vector2((float)(to.X - fulcrum.X) + num, (float)(to.Y - fulcrum.Y) - num);
			if (Vector2.Angle(vector7, vector2) <= MaxAngle)
			{
				return true;
			}
			Vector2 vector8 = new Vector2((float)(from.X - fulcrum.X) + num, (float)(from.Y - fulcrum.Y) - num);
			vector2 = new Vector2((float)(to.X - fulcrum.X) - num, (float)(to.Y - fulcrum.Y) + num);
			if (Vector2.Angle(vector8, vector2) <= MaxAngle)
			{
				return true;
			}
			Vector2 vector9 = new Vector2((float)(from.X - fulcrum.X) + num, (float)(from.Y - fulcrum.Y) - num);
			vector2 = new Vector2((float)(to.X - fulcrum.X) - num, (float)(to.Y - fulcrum.Y) - num);
			if (Vector2.Angle(vector9, vector2) <= MaxAngle)
			{
				return true;
			}
			Vector2 vector10 = new Vector2((float)(from.X - fulcrum.X) - num, (float)(from.Y - fulcrum.Y) + num);
			vector2 = new Vector2((float)(to.X - fulcrum.X) - num, (float)(to.Y - fulcrum.Y) - num);
			if (Vector2.Angle(vector10, vector2) <= MaxAngle)
			{
				return true;
			}
			Vector2 vector11 = new Vector2((float)(from.X - fulcrum.X) - num, (float)(from.Y - fulcrum.Y) - num);
			vector2 = new Vector2((float)(to.X - fulcrum.X) - num, (float)(to.Y - fulcrum.Y) + num);
			if (Vector2.Angle(vector11, vector2) <= MaxAngle)
			{
				return true;
			}
			Vector2 vector12 = new Vector2((float)(from.X - fulcrum.X) - num, (float)(from.Y - fulcrum.Y) - num);
			vector2 = new Vector2((float)(to.X - fulcrum.X) + num, (float)(to.Y - fulcrum.Y) - num);
			if (Vector2.Angle(vector12, vector2) <= MaxAngle)
			{
				return true;
			}
			Vector2 vector13 = new Vector2((float)(from.X - fulcrum.X) + num, (float)(from.Y - fulcrum.Y) - num);
			vector2 = new Vector2((float)(to.X - fulcrum.X) + num, (float)(to.Y - fulcrum.Y) - num);
			if (Vector2.Angle(vector13, vector2) <= MaxAngle)
			{
				return true;
			}
			Vector2 vector14 = new Vector2((float)(from.X - fulcrum.X) - num, (float)(from.Y - fulcrum.Y) + num);
			vector2 = new Vector2((float)(to.X - fulcrum.X) - num, (float)(to.Y - fulcrum.Y) + num);
			if (Vector2.Angle(vector14, vector2) <= MaxAngle)
			{
				return true;
			}
			Vector2 vector15 = new Vector2((float)(from.X - fulcrum.X) + num, (float)(from.Y - fulcrum.Y) + num);
			vector2 = new Vector2((float)(to.X - fulcrum.X) + num, (float)(to.Y - fulcrum.Y) + num);
			return Vector2.Angle(vector15, vector2) <= MaxAngle;
		}

		// Token: 0x06004070 RID: 16496 RVA: 0x0013E6E4 File Offset: 0x0013C8E4
		public static List<Location2D> GetCone(Location2D source, Location2D target, int Length, int Angle, List<Location2D> result = null)
		{
			if (result == null)
			{
				result = new List<Location2D>();
			}
			else
			{
				result.Clear();
			}
			if (source.Distance(target) == 0)
			{
				result.Add(source);
				return result;
			}
			foreach (Location2D location2D in source.YieldAdjacent(Length + 1))
			{
				if (location2D.X < 80 && location2D.Y < 25 && location2D.Distance(source) <= Length && Geometry.TestAngleTo(source, location2D, target, (float)Angle / 2f))
				{
					result.Add(location2D);
				}
			}
			return result;
		}
	}
}
