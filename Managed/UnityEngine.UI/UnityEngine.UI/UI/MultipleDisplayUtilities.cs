using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200002F RID: 47
	internal static class MultipleDisplayUtilities
	{
		// Token: 0x06000309 RID: 777 RVA: 0x00010140 File Offset: 0x0000E340
		public static bool GetRelativeMousePositionForDrag(PointerEventData eventData, ref Vector2 position)
		{
			int displayIndex = eventData.pointerPressRaycast.displayIndex;
			Vector3 vector = MultipleDisplayUtilities.RelativeMouseAtScaled(eventData.position, eventData.displayIndex);
			if ((int)vector.z != displayIndex)
			{
				return false;
			}
			position = ((displayIndex != 0) ? vector : eventData.position);
			return true;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00010190 File Offset: 0x0000E390
		internal static Vector3 GetRelativeMousePositionForRaycast(PointerEventData eventData)
		{
			Vector3 vector = MultipleDisplayUtilities.RelativeMouseAtScaled(eventData.position, eventData.displayIndex);
			if (vector == Vector3.zero)
			{
				vector = eventData.position;
			}
			if (eventData.displayIndex > 0)
			{
				vector.z = (float)eventData.displayIndex;
			}
			return vector;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000101E0 File Offset: 0x0000E3E0
		public static Vector3 RelativeMouseAtScaled(Vector2 position, int displayIndex)
		{
			if (Display.main.renderingWidth != Display.main.systemWidth || Display.main.renderingHeight != Display.main.systemHeight)
			{
				float num = (float)Display.main.systemWidth / (float)Display.main.systemHeight;
				Vector2 vector = new Vector2((float)Display.main.renderingWidth, (float)Display.main.renderingHeight);
				Vector2 zero = Vector2.zero;
				if (Screen.fullScreen)
				{
					float num2 = (float)Screen.width / (float)Screen.height;
					if ((float)Display.main.systemHeight * num2 < (float)Display.main.systemWidth)
					{
						vector.x = (float)Display.main.renderingHeight * num;
						zero.x = (vector.x - (float)Display.main.renderingWidth) * 0.5f;
					}
					else
					{
						vector.y = (float)Display.main.renderingWidth / num;
						zero.y = (vector.y - (float)Display.main.renderingHeight) * 0.5f;
					}
				}
				Vector2 vector2 = vector - zero;
				if (position.y < -zero.y || position.y > vector2.y || position.x < -zero.x || position.x > vector2.x)
				{
					Vector2 vector3 = position;
					if (!Screen.fullScreen)
					{
						vector3.x -= (float)(Display.main.renderingWidth - Display.main.systemWidth) * 0.5f;
						vector3.y -= (float)(Display.main.renderingHeight - Display.main.systemHeight) * 0.5f;
					}
					else
					{
						vector3 += zero;
						vector3.x *= (float)Display.main.systemWidth / vector.x;
						vector3.y *= (float)Display.main.systemHeight / vector.y;
					}
					Vector3 vector4 = new Vector3(vector3.x, vector3.y, (float)displayIndex);
					if (vector4.z != 0f)
					{
						return vector4;
					}
				}
				return new Vector3(position.x, position.y, 0f);
			}
			return new Vector3(position.x, position.y, (float)displayIndex);
		}
	}
}
