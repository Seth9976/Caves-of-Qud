using System;
using Cysharp.Text;
using UnityEngine;
using XRL.World;

// Token: 0x0200016C RID: 364
[Serializable]
public class Vector2i : IComposite
{
	// Token: 0x1700015E RID: 350
	// (get) Token: 0x06000E4D RID: 3661 RVA: 0x0005C250 File Offset: 0x0005A450
	public bool WantFieldReflection
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000E4E RID: 3662 RVA: 0x0005C253 File Offset: 0x0005A453
	public Vector2i()
	{
	}

	// Token: 0x06000E4F RID: 3663 RVA: 0x0005C25B File Offset: 0x0005A45B
	public Vector2i(int _x, int _y)
	{
		this.x = _x;
		this.y = _y;
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x0005C271 File Offset: 0x0005A471
	public Vector2i Clamp(int minx = 0, int miny = 0, int maxx = 79, int maxy = 24)
	{
		Vector2i vector2i = new Vector2i(this.x, this.y);
		vector2i.x = Mathf.Clamp(vector2i.x, minx, maxx);
		vector2i.y = Mathf.Clamp(vector2i.y, miny, maxy);
		return vector2i;
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x0005C2AC File Offset: 0x0005A4AC
	public string DirectionTo(Vector2i destination)
	{
		string text;
		using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
		{
			if (destination.y > this.y)
			{
				utf16ValueStringBuilder.Append("S");
			}
			if (destination.y < this.y)
			{
				utf16ValueStringBuilder.Append("N");
			}
			if (destination.x < this.x)
			{
				utf16ValueStringBuilder.Append("W");
			}
			if (destination.x > this.x)
			{
				utf16ValueStringBuilder.Append("E");
			}
			if (utf16ValueStringBuilder.Length == 0)
			{
				text = ".";
			}
			else
			{
				text = utf16ValueStringBuilder.ToString();
			}
		}
		return text;
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x0005C364 File Offset: 0x0005A564
	public void Write(SerializationWriter Writer)
	{
		Writer.WriteOptimized(this.x);
		Writer.WriteOptimized(this.y);
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x0005C37E File Offset: 0x0005A57E
	public void Read(SerializationReader Reader)
	{
		this.x = Reader.ReadOptimizedInt32();
		this.y = Reader.ReadOptimizedInt32();
	}

	// Token: 0x06000E54 RID: 3668 RVA: 0x0005C398 File Offset: 0x0005A598
	public override bool Equals(object obj)
	{
		return obj != null && obj is Vector2i && this == (Vector2i)obj;
	}

	// Token: 0x06000E55 RID: 3669 RVA: 0x0005C3B5 File Offset: 0x0005A5B5
	public override int GetHashCode()
	{
		return this.x.GetHashCode() ^ this.y.GetHashCode();
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x0005C3CE File Offset: 0x0005A5CE
	public static bool operator ==(Vector2i x, Vector2i y)
	{
		if (x == null)
		{
			return y == null;
		}
		return y != null && x.x == y.x && x.y == y.y;
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x0005C3FB File Offset: 0x0005A5FB
	public static bool operator !=(Vector2i x, Vector2i y)
	{
		return !(x == y);
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x0005C407 File Offset: 0x0005A607
	public int DistanceTo(Vector2i d)
	{
		return (int)Mathf.Sqrt((float)((d.x - this.x) * (d.x - this.x) + (d.y - this.y) * (d.y - this.y)));
	}

	// Token: 0x040009FF RID: 2559
	public int x;

	// Token: 0x04000A00 RID: 2560
	public int y;
}
