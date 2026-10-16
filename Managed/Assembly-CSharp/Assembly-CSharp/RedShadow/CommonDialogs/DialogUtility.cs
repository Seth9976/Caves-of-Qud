using System;
using UnityEngine;
using UnityEngine.UI;

namespace RedShadow.CommonDialogs
{
	// Token: 0x020001F7 RID: 503
	public static class DialogUtility
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x0006B410 File Offset: 0x00069610
		public static string prettyPrintSize(long size)
		{
			string[] array = new string[] { "B", "KB", "MB", "GB", "TB" };
			int num = 0;
			while (size >= 1024L && num < array.Length - 1)
			{
				num++;
				size /= 1024L;
			}
			return string.Format("{0:0.##} {1}", size, array[num]);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0006B484 File Offset: 0x00069684
		public static void centerOnItem(GameObject contentPanel, RectTransform target)
		{
			RectTransform component = contentPanel.GetComponent<RectTransform>();
			RectTransform component2 = contentPanel.transform.parent.GetComponent<RectTransform>();
			RectTransform component3 = component2.transform.parent.GetComponent<RectTransform>();
			ScrollRect component4 = component2.transform.parent.GetComponent<ScrollRect>();
			Vector3 vector = component2.position + component2.rect.center;
			Vector3 position = target.position;
			Vector3 vector2 = vector - position;
			vector2.z = 0f;
			Vector2 vector3 = new Vector2(vector2.x / (component.rect.size.x - component3.rect.size.x), vector2.y / (component.rect.size.y - component3.rect.size.y));
			Vector2 vector4 = component4.normalizedPosition - vector3;
			if (component4.movementType != ScrollRect.MovementType.Unrestricted)
			{
				vector4.x = Mathf.Clamp01(vector4.x);
				vector4.y = Mathf.Clamp01(vector4.y);
			}
			component4.normalizedPosition = vector4;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x0006B5B4 File Offset: 0x000697B4
		public static Vector2 sizeToParent(RawImage image, float padding = 0f)
		{
			RectTransform componentInParent = image.transform.parent.GetComponentInParent<RectTransform>();
			RectTransform component = image.GetComponent<RectTransform>();
			if (!componentInParent)
			{
				return component.sizeDelta;
			}
			float num = (float)image.texture.width / (float)image.texture.height;
			Rect rect = new Rect(0f, 0f, componentInParent.rect.width, componentInParent.rect.height);
			if (Mathf.RoundToInt(component.eulerAngles.z) % 180 == 90)
			{
				rect.size = new Vector2(rect.height, rect.width);
			}
			float num2 = rect.height;
			float num3 = num2 * num;
			if (num3 > rect.width)
			{
				num3 = rect.width;
				num2 = num3 / num;
			}
			component.sizeDelta = new Vector2(num3 - padding * 2f, num2 - padding * 2f);
			return component.sizeDelta;
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0006B6B4 File Offset: 0x000698B4
		public static Rect rectTransformToScreenSpace(RectTransform transform)
		{
			Vector2 vector = Vector2.Scale(transform.rect.size, transform.lossyScale);
			float num = transform.position.x - transform.anchoredPosition.x;
			float num2 = (float)Screen.height - transform.position.y - transform.anchoredPosition.y;
			return new Rect(num, num2, vector.x, vector.y);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0006B728 File Offset: 0x00069928
		public static Rect getScreenCoordinates(RectTransform transform)
		{
			Vector3[] array = new Vector3[4];
			transform.GetWorldCorners(array);
			return new Rect(array[0].x, array[0].y, array[2].x - array[0].x, array[2].y - array[0].y);
		}
	}
}
