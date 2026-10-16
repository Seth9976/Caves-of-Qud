using System;
using UnityEngine;

// Token: 0x020000E6 RID: 230
public static class exUIUtility
{
	// Token: 0x060008A2 RID: 2210 RVA: 0x00037D00 File Offset: 0x00035F00
	public static Vector3 GetOffset(this exPlane _plane)
	{
		Vector2 vector = Vector2.zero;
		Vector2 vector2 = new Vector2(_plane.width, _plane.height);
		switch (_plane.anchor)
		{
		case Anchor.TopLeft:
			vector = new Vector3(-vector2.x * 0.5f, vector2.y * 0.5f, 0f);
			break;
		case Anchor.TopCenter:
			vector = new Vector3(0f, vector2.y * 0.5f, 0f);
			break;
		case Anchor.TopRight:
			vector = new Vector3(vector2.x * 0.5f, vector2.y * 0.5f, 0f);
			break;
		case Anchor.MidLeft:
			vector = new Vector3(-vector2.x * 0.5f, 0f, 0f);
			break;
		case Anchor.MidCenter:
			vector = new Vector3(0f, 0f, 0f);
			break;
		case Anchor.MidRight:
			vector = new Vector3(vector2.x * 0.5f, 0f, 0f);
			break;
		case Anchor.BotLeft:
			vector = new Vector3(-vector2.x * 0.5f, -vector2.y * 0.5f, 0f);
			break;
		case Anchor.BotCenter:
			vector = new Vector3(0f, -vector2.y * 0.5f, 0f);
			break;
		case Anchor.BotRight:
			vector = new Vector3(vector2.x * 0.5f, -vector2.y * 0.5f, 0f);
			break;
		}
		Vector3 vector3 = _plane.offset + vector;
		Vector3 lossyScale = _plane.transform.lossyScale;
		vector3.x *= lossyScale.x;
		vector3.y *= lossyScale.y;
		return vector;
	}
}
