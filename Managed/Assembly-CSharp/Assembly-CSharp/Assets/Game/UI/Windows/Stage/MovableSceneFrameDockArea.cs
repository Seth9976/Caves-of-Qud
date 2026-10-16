using System;
using UnityEngine;
using XRL;

namespace Assets.Game.UI.Windows.Stage
{
	// Token: 0x020003E6 RID: 998
	public class MovableSceneFrameDockArea : MonoBehaviour
	{
		// Token: 0x0600255D RID: 9565 RVA: 0x000BE3B4 File Offset: 0x000BC5B4
		public void Update()
		{
			if (this.ours == null)
			{
				this.ours = base.GetComponent<RectTransform>();
			}
			if (this.content == null)
			{
				return;
			}
			if (GameManager.Instance.DockMovable > 0 && GameManager.MainCameraLetterbox)
			{
				if (this.content.parent != this.ours.parent)
				{
					this.oldParent = this.content.parent as RectTransform;
					this.oldTransform = MovableSceneFrameDockArea.RectTransformData.PullFromTransform(this.content);
					this.content.SetParent(this.ours.parent, false);
					this.contentFrame.SetActive(false);
				}
				this.content.CopyFrom(this.ours);
				if (this.content.pivot.x != 0f || this.content.pivot.y != 1f)
				{
					this.content.pivot = new Vector2(0f, 1f);
					return;
				}
			}
			else if (this.oldParent != null)
			{
				if (this.content.GetComponent<Canvas>() != null)
				{
					this.content.GetComponent<Canvas>().enabled = true;
				}
				this.content.transform.SetParent(this.oldParent, false);
				this.oldTransform.PushToTransform(this.content);
				this.contentFrame.SetActive(true);
				this.oldParent = null;
			}
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x000BE53B File Offset: 0x000BC73B
		public void LateUpdate()
		{
		}

		// Token: 0x040012AF RID: 4783
		public GameObject contentFrame;

		// Token: 0x040012B0 RID: 4784
		public RectTransform content;

		// Token: 0x040012B1 RID: 4785
		public RectTransform ours;

		// Token: 0x040012B2 RID: 4786
		public RectTransform oldParent;

		// Token: 0x040012B3 RID: 4787
		public MovableSceneFrameDockArea.RectTransformData oldTransform;

		// Token: 0x020017AE RID: 6062
		[Serializable]
		public class RectTransformData
		{
			// Token: 0x0600DE49 RID: 56905 RVA: 0x0050EA9C File Offset: 0x0050CC9C
			public static MovableSceneFrameDockArea.RectTransformData PullFromTransform(RectTransform transform)
			{
				return new MovableSceneFrameDockArea.RectTransformData
				{
					LocalPosition = transform.localPosition,
					AnchorMin = transform.anchorMin,
					AnchorMax = transform.anchorMax,
					Pivot = transform.pivot,
					AnchoredPosition = transform.anchoredPosition,
					SizeDelta = transform.sizeDelta,
					Rotation = transform.localRotation,
					Scale = transform.localScale
				};
			}

			// Token: 0x0600DE4A RID: 56906 RVA: 0x0050EB10 File Offset: 0x0050CD10
			public void PushToTransform(RectTransform transform)
			{
				transform.localPosition = this.LocalPosition;
				transform.anchorMin = this.AnchorMin;
				transform.anchorMax = this.AnchorMax;
				transform.pivot = this.Pivot;
				transform.anchoredPosition = this.AnchoredPosition;
				transform.sizeDelta = this.SizeDelta;
				transform.localRotation = this.Rotation;
				transform.localScale = this.Scale;
			}

			// Token: 0x04005F48 RID: 24392
			public Vector3 LocalPosition;

			// Token: 0x04005F49 RID: 24393
			public Vector2 AnchoredPosition;

			// Token: 0x04005F4A RID: 24394
			public Vector2 SizeDelta;

			// Token: 0x04005F4B RID: 24395
			public Vector2 AnchorMin;

			// Token: 0x04005F4C RID: 24396
			public Vector2 AnchorMax;

			// Token: 0x04005F4D RID: 24397
			public Vector2 Pivot;

			// Token: 0x04005F4E RID: 24398
			public Vector3 Scale;

			// Token: 0x04005F4F RID: 24399
			public Quaternion Rotation;
		}
	}
}
