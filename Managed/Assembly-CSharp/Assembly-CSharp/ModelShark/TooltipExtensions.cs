using System;
using Qud.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ModelShark
{
	// Token: 0x02000198 RID: 408
	public static class TooltipExtensions
	{
		// Token: 0x06000F81 RID: 3969 RVA: 0x00061670 File Offset: 0x0005F870
		public static void SetPosition(this Tooltip tooltip, TooltipTrigger trigger, Canvas canvas, Camera camera)
		{
			Vector3[] array = new Vector3[4];
			RectTransform component = trigger.gameObject.GetComponent<RectTransform>();
			if (component != null)
			{
				component.GetWorldCorners(array);
			}
			else
			{
				Collider component2 = trigger.GetComponent<Collider>();
				Vector3 center = component2.bounds.center;
				Vector3 extents = component2.bounds.extents;
				Vector3 vector = new Vector3(center.x - extents.x, center.y - extents.y, center.z - extents.z);
				Vector3 vector2 = new Vector3(center.x - extents.x, center.y + extents.y, center.z - extents.z);
				Vector3 vector3 = new Vector3(center.x + extents.x, center.y + extents.y, center.z - extents.z);
				Vector3 vector4 = new Vector3(center.x + extents.x, center.y - extents.y, center.z - extents.z);
				array[0] = camera.WorldToScreenPoint(vector);
				array[1] = camera.WorldToScreenPoint(vector2);
				array[2] = camera.WorldToScreenPoint(vector3);
				array[3] = camera.WorldToScreenPoint(vector4);
			}
			tooltip.SetPosition(trigger.tipPosition, trigger.tooltipStyle, array, tooltip.BackgroundImage, tooltip.RectTransform, canvas, camera, true);
			if (!TooltipManager.Instance.overflowProtection)
			{
				return;
			}
			Vector3[] array2 = new Vector3[4];
			tooltip.RectTransform.GetWorldCorners(array2);
			if (canvas.renderMode == RenderMode.ScreenSpaceCamera || canvas.renderMode == RenderMode.WorldSpace)
			{
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = RectTransformUtility.WorldToScreenPoint(camera, array2[i]);
				}
			}
			else if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = RectTransformUtility.WorldToScreenPoint(null, array2[j]);
				}
			}
			Rect rect = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
			TooltipOverflow tooltipOverflow = new TooltipOverflow
			{
				BottomLeftCorner = !rect.Contains(array2[0]),
				TopLeftCorner = !rect.Contains(array2[1]),
				TopRightCorner = !rect.Contains(array2[2]),
				BottomRightCorner = !rect.Contains(array2[3])
			};
			if (tooltipOverflow.IsAny)
			{
				tooltip.SetPosition(tooltipOverflow.SuggestNewPosition(trigger.tipPosition), trigger.tooltipStyle, array, tooltip.BackgroundImage, tooltip.RectTransform, canvas, camera, false);
			}
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00061944 File Offset: 0x0005FB44
		private static void SetPosition(this Tooltip tooltip, TipPosition tipPosition, TooltipStyle style, Vector3[] triggerCorners, Image bkgImage, RectTransform tooltipRectTrans, Canvas canvas, Camera camera, bool willTryFlip = false)
		{
			Vector3 vector = Vector3.zero;
			Vector3 zero = Vector3.zero;
			bool flag = tipPosition == TipPosition.MouseBottomLeftCorner || tipPosition == TipPosition.MouseTopLeftCorner || tipPosition == TipPosition.MouseBottomRightCorner || tipPosition == TipPosition.MouseTopRightCorner || tipPosition == TipPosition.MouseTopMiddle || tipPosition == TipPosition.MouseLeftMiddle || tipPosition == TipPosition.MouseRightMiddle || tipPosition == TipPosition.MouseBottomMiddle;
			Vector3 vector2 = Input.mousePosition;
			if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
			{
				vector2.z = canvas.planeDistance;
				vector2 = camera.ScreenToWorldPoint(vector2);
			}
			switch (tipPosition)
			{
			case TipPosition.TopRightCorner:
			case TipPosition.MouseTopRightCorner:
				zero = new Vector3((float)(-1 * style.tipOffset), (float)(-1 * style.tipOffset));
				vector = (flag ? vector2 : triggerCorners[2]);
				tooltipRectTrans.pivot = new Vector2(0f, 0f);
				bkgImage.sprite = style.bottomLeftCorner;
				break;
			case TipPosition.BottomRightCorner:
			case TipPosition.MouseBottomRightCorner:
				zero = new Vector3((float)(-1 * style.tipOffset), (float)style.tipOffset);
				vector = (flag ? vector2 : triggerCorners[3]);
				tooltipRectTrans.pivot = new Vector2(0f, 1f);
				bkgImage.sprite = style.topLeftCorner;
				break;
			case TipPosition.TopLeftCorner:
			case TipPosition.MouseTopLeftCorner:
				zero = new Vector3((float)style.tipOffset, (float)(-1 * style.tipOffset));
				vector = (flag ? vector2 : triggerCorners[1]);
				tooltipRectTrans.pivot = new Vector2(1f, 0f);
				bkgImage.sprite = style.bottomRightCorner;
				break;
			case TipPosition.BottomLeftCorner:
			case TipPosition.MouseBottomLeftCorner:
				zero = new Vector3((float)style.tipOffset, (float)style.tipOffset);
				vector = (flag ? vector2 : triggerCorners[0]);
				tooltipRectTrans.pivot = new Vector2(1f, 1f);
				bkgImage.sprite = style.topRightCorner;
				break;
			case TipPosition.TopMiddle:
			case TipPosition.MouseTopMiddle:
				zero = new Vector3(0f, (float)(-1 * style.tipOffset));
				vector = (flag ? vector2 : (triggerCorners[1] + (triggerCorners[2] - triggerCorners[1]) / 2f));
				tooltipRectTrans.pivot = new Vector2(0.5f, 0f);
				bkgImage.sprite = style.topMiddle;
				break;
			case TipPosition.BottomMiddle:
			case TipPosition.MouseBottomMiddle:
				zero = new Vector3(0f, (float)style.tipOffset);
				vector = (flag ? vector2 : (triggerCorners[0] + (triggerCorners[3] - triggerCorners[0]) / 2f));
				tooltipRectTrans.pivot = new Vector2(0.5f, 1f);
				bkgImage.sprite = style.bottomMiddle;
				break;
			case TipPosition.RightMiddle:
			case TipPosition.MouseRightMiddle:
				zero = new Vector3((float)(-1 * style.tipOffset), 0f);
				vector = (flag ? vector2 : (triggerCorners[3] + (triggerCorners[2] - triggerCorners[3]) / 2f));
				tooltipRectTrans.pivot = new Vector2(0f, 0.5f);
				bkgImage.sprite = style.rightMiddle;
				break;
			case TipPosition.LeftMiddle:
			case TipPosition.MouseLeftMiddle:
				zero = new Vector3((float)style.tipOffset, 0f);
				vector = (flag ? vector2 : (triggerCorners[0] + (triggerCorners[1] - triggerCorners[0]) / 2f));
				tooltipRectTrans.pivot = new Vector2(1f, 0.5f);
				bkgImage.sprite = style.leftMiddle;
				break;
			}
			tooltip.GameObject.transform.position = vector;
			tooltipRectTrans.anchoredPosition += zero;
			Vector3[] array = new Vector3[4];
			tooltip.RectTransform.GetWorldCorners(array);
			if (canvas.renderMode == RenderMode.ScreenSpaceCamera || canvas.renderMode == RenderMode.WorldSpace)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = RectTransformUtility.WorldToScreenPoint(camera, array[i]);
				}
			}
			else if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = RectTransformUtility.WorldToScreenPoint(null, array[j]);
				}
			}
			if (willTryFlip)
			{
				return;
			}
			Rect rect = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
			TooltipOverflow tooltipOverflow = new TooltipOverflow
			{
				BottomLeftCorner = !rect.Contains(array[0]),
				TopLeftCorner = !rect.Contains(array[1]),
				TopRightCorner = !rect.Contains(array[2]),
				BottomRightCorner = !rect.Contains(array[3])
			};
			if (tooltipOverflow.IsAny)
			{
				if (tooltipOverflow.BottomEdge)
				{
					tooltip.RectTransform.position += new Vector3(0f, -array[0].y, 0f);
				}
				if (tooltipOverflow.TopEdge)
				{
					tooltip.RectTransform.position -= new Vector3(0f, array[1].y - (float)Screen.height, 0f);
				}
				if (tooltipOverflow.RightEdge)
				{
					tooltip.RectTransform.position -= new Vector3(array[2].x - (float)Screen.width, 0f, 0f);
				}
				if (tooltipOverflow.LeftEdge)
				{
					tooltip.RectTransform.position += new Vector3(-array[0].x, 0f, 0f);
				}
			}
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x00061F24 File Offset: 0x00060124
		public static void SetPositionManual(this Tooltip tooltip, Vector3 screenPos, TipPosition tipPosition, TooltipStyle style, Image bkgImage, RectTransform tooltipRectTrans, Canvas canvas, Camera camera)
		{
			Vector3 zero = Vector3.zero;
			tipPosition = TipPosition.TopRightCorner;
			if (screenPos.x < (float)(Screen.width / 2))
			{
				if (screenPos.y < (float)(Screen.height / 2))
				{
					tipPosition = TipPosition.TopRightCorner;
				}
				else
				{
					tipPosition = TipPosition.BottomRightCorner;
				}
			}
			else if (screenPos.y < (float)(Screen.height / 2))
			{
				tipPosition = TipPosition.TopLeftCorner;
			}
			else
			{
				tipPosition = TipPosition.BottomLeftCorner;
			}
			switch (tipPosition)
			{
			case TipPosition.TopRightCorner:
			case TipPosition.MouseTopRightCorner:
				zero = new Vector3((float)(-1 * style.tipOffset), (float)(-1 * style.tipOffset));
				tooltipRectTrans.pivot = new Vector2(0f, 0f);
				bkgImage.sprite = style.bottomLeftCorner;
				break;
			case TipPosition.BottomRightCorner:
			case TipPosition.MouseBottomRightCorner:
				zero = new Vector3((float)(-1 * style.tipOffset), (float)style.tipOffset);
				tooltipRectTrans.pivot = new Vector2(0f, 1f);
				bkgImage.sprite = style.topLeftCorner;
				break;
			case TipPosition.TopLeftCorner:
			case TipPosition.MouseTopLeftCorner:
				zero = new Vector3((float)style.tipOffset, (float)(-1 * style.tipOffset));
				tooltipRectTrans.pivot = new Vector2(1f, 0f);
				bkgImage.sprite = style.bottomRightCorner;
				break;
			case TipPosition.BottomLeftCorner:
			case TipPosition.MouseBottomLeftCorner:
				zero = new Vector3((float)style.tipOffset, (float)style.tipOffset);
				tooltipRectTrans.pivot = new Vector2(1f, 1f);
				bkgImage.sprite = style.topRightCorner;
				break;
			case TipPosition.TopMiddle:
			case TipPosition.MouseTopMiddle:
				zero = new Vector3(0f, (float)(-1 * style.tipOffset));
				tooltipRectTrans.pivot = new Vector2(0.5f, 0f);
				bkgImage.sprite = style.topMiddle;
				break;
			case TipPosition.BottomMiddle:
			case TipPosition.MouseBottomMiddle:
				zero = new Vector3(0f, (float)style.tipOffset);
				tooltipRectTrans.pivot = new Vector2(0.5f, 1f);
				bkgImage.sprite = style.bottomMiddle;
				break;
			case TipPosition.RightMiddle:
			case TipPosition.MouseRightMiddle:
				zero = new Vector3((float)(-1 * style.tipOffset), 0f);
				tooltipRectTrans.pivot = new Vector2(0f, 0.5f);
				bkgImage.sprite = style.rightMiddle;
				break;
			case TipPosition.LeftMiddle:
			case TipPosition.MouseLeftMiddle:
				zero = new Vector3((float)style.tipOffset, 0f);
				tooltipRectTrans.pivot = new Vector2(1f, 0.5f);
				bkgImage.sprite = style.leftMiddle;
				break;
			}
			if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				Vector2 vector = screenPos;
				vector.x = screenPos.x / (float)Screen.width * (canvas.transform as RectTransform).rect.width;
				vector.y = screenPos.y / (float)Screen.height * (canvas.transform as RectTransform).rect.height;
				tooltipRectTrans.position = vector * UIManager.scale;
				tooltipRectTrans.anchoredPosition += zero;
			}
			else
			{
				tooltipRectTrans.position = camera.ScreenToWorldPoint(screenPos);
				tooltipRectTrans.anchoredPosition += zero;
			}
			Vector3[] array = new Vector3[4];
			tooltip.RectTransform.GetWorldCorners(array);
			if (canvas.renderMode == RenderMode.ScreenSpaceCamera || canvas.renderMode == RenderMode.WorldSpace)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = RectTransformUtility.WorldToScreenPoint(camera, array[i]);
				}
			}
			else if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = RectTransformUtility.WorldToScreenPoint(null, array[j]);
				}
			}
			Rect rect = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
			TooltipOverflow tooltipOverflow = new TooltipOverflow
			{
				BottomLeftCorner = !rect.Contains(array[0]),
				TopLeftCorner = !rect.Contains(array[1]),
				TopRightCorner = !rect.Contains(array[2]),
				BottomRightCorner = !rect.Contains(array[3])
			};
			if (tooltipOverflow.IsAny)
			{
				if (tooltipOverflow.BottomEdge)
				{
					tooltip.RectTransform.position += new Vector3(0f, -array[0].y, 0f);
				}
				if (tooltipOverflow.TopEdge)
				{
					tooltip.RectTransform.position -= new Vector3(0f, array[1].y - (float)Screen.height, 0f);
				}
				if (tooltipOverflow.RightEdge)
				{
					tooltip.RectTransform.position -= new Vector3(array[2].x - (float)Screen.width, 0f, 0f);
				}
				if (tooltipOverflow.LeftEdge)
				{
					tooltip.RectTransform.position += new Vector3(-array[0].x, 0f, 0f);
				}
			}
		}
	}
}
