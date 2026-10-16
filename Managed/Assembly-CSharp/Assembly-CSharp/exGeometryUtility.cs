using System;
using UnityEngine;

// Token: 0x020000F1 RID: 241
public static class exGeometryUtility
{
	// Token: 0x0600093D RID: 2365 RVA: 0x0003AAB0 File Offset: 0x00038CB0
	public static int RectRect_Contains(Rect _a, Rect _b)
	{
		if (_a.xMin <= _b.xMin && _a.xMax >= _b.xMax && _a.yMin <= _b.yMin && _a.yMax >= _b.yMax)
		{
			return 1;
		}
		if (_b.xMin <= _a.xMin && _b.xMax >= _a.xMax && _b.yMin <= _a.yMin && _b.yMax >= _a.yMax)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x0003AB44 File Offset: 0x00038D44
	public static bool RectRect_Intersect(Rect _a, Rect _b)
	{
		return ((_a.xMin <= _b.xMin && _a.xMax >= _b.xMin) || (_b.xMin <= _a.xMin && _b.xMax >= _a.xMin)) && ((_a.yMin <= _b.yMin && _a.yMax >= _b.yMin) || (_b.yMin <= _a.yMin && _b.yMax >= _a.yMin));
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x0003ABD4 File Offset: 0x00038DD4
	public static Rect Rect_FloorToInt(Rect _rect)
	{
		return new Rect((float)Mathf.FloorToInt(_rect.x), (float)Mathf.FloorToInt(_rect.y), (float)Mathf.FloorToInt(_rect.width), (float)Mathf.FloorToInt(_rect.height));
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x0003AC10 File Offset: 0x00038E10
	public static Rect GetAABoundingRect(Vector3[] _vertices)
	{
		Rect rect = default(Rect);
		if (_vertices.Length != 0)
		{
			rect.x = _vertices[0].x;
			rect.y = _vertices[0].y;
			for (int i = 1; i < _vertices.Length; i++)
			{
				Vector3 vector = _vertices[i];
				if (vector.x < rect.xMin)
				{
					rect.xMin = vector.x;
				}
				else if (vector.x > rect.xMax)
				{
					rect.xMax = vector.x;
				}
				if (vector.y < rect.yMin)
				{
					rect.yMin = vector.y;
				}
				else if (vector.y > rect.yMax)
				{
					rect.yMax = vector.y;
				}
			}
		}
		return rect;
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x0003ACE4 File Offset: 0x00038EE4
	public static Vector2 GetConstrainOffset(Rect _rect, Rect _bound)
	{
		Vector2 zero = Vector2.zero;
		if (_bound.width > _rect.width)
		{
			float num = _bound.width - _rect.width;
			_rect.xMin -= num;
			_rect.xMax += num;
		}
		if (_bound.height > _rect.height)
		{
			float num2 = _bound.height - _rect.height;
			_rect.yMin -= num2;
			_rect.yMax += num2;
		}
		if (_bound.xMin < _rect.xMin)
		{
			zero.x += _rect.xMin - _bound.xMin;
		}
		if (_bound.xMax > _rect.xMax)
		{
			zero.x -= _bound.xMax - _rect.xMax;
		}
		if (_bound.yMin < _rect.yMin)
		{
			zero.y += _rect.yMin - _bound.yMin;
		}
		if (_bound.yMax > _rect.yMax)
		{
			zero.y -= _bound.yMax - _rect.yMax;
		}
		return zero;
	}
}
