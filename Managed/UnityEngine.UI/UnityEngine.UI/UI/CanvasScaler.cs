using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200001B RID: 27
	[RequireComponent(typeof(Canvas))]
	[ExecuteAlways]
	[AddComponentMenu("Layout/Canvas Scaler", 101)]
	[DisallowMultipleComponent]
	public class CanvasScaler : UIBehaviour
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000D300 File Offset: 0x0000B500
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000D308 File Offset: 0x0000B508
		public CanvasScaler.ScaleMode uiScaleMode
		{
			get
			{
				return this.m_UiScaleMode;
			}
			set
			{
				this.m_UiScaleMode = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000D311 File Offset: 0x0000B511
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000D319 File Offset: 0x0000B519
		public float referencePixelsPerUnit
		{
			get
			{
				return this.m_ReferencePixelsPerUnit;
			}
			set
			{
				this.m_ReferencePixelsPerUnit = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000D322 File Offset: 0x0000B522
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000D32A File Offset: 0x0000B52A
		public float scaleFactor
		{
			get
			{
				return this.m_ScaleFactor;
			}
			set
			{
				this.m_ScaleFactor = Mathf.Max(0.01f, value);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000D33D File Offset: 0x0000B53D
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000D348 File Offset: 0x0000B548
		public Vector2 referenceResolution
		{
			get
			{
				return this.m_ReferenceResolution;
			}
			set
			{
				this.m_ReferenceResolution = value;
				if (this.m_ReferenceResolution.x > -1E-05f && this.m_ReferenceResolution.x < 1E-05f)
				{
					this.m_ReferenceResolution.x = 1E-05f * Mathf.Sign(this.m_ReferenceResolution.x);
				}
				if (this.m_ReferenceResolution.y > -1E-05f && this.m_ReferenceResolution.y < 1E-05f)
				{
					this.m_ReferenceResolution.y = 1E-05f * Mathf.Sign(this.m_ReferenceResolution.y);
				}
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000D3E6 File Offset: 0x0000B5E6
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000D3EE File Offset: 0x0000B5EE
		public CanvasScaler.ScreenMatchMode screenMatchMode
		{
			get
			{
				return this.m_ScreenMatchMode;
			}
			set
			{
				this.m_ScreenMatchMode = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000D3F7 File Offset: 0x0000B5F7
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000D3FF File Offset: 0x0000B5FF
		public float matchWidthOrHeight
		{
			get
			{
				return this.m_MatchWidthOrHeight;
			}
			set
			{
				this.m_MatchWidthOrHeight = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000D408 File Offset: 0x0000B608
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000D410 File Offset: 0x0000B610
		public CanvasScaler.Unit physicalUnit
		{
			get
			{
				return this.m_PhysicalUnit;
			}
			set
			{
				this.m_PhysicalUnit = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000D419 File Offset: 0x0000B619
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000D421 File Offset: 0x0000B621
		public float fallbackScreenDPI
		{
			get
			{
				return this.m_FallbackScreenDPI;
			}
			set
			{
				this.m_FallbackScreenDPI = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000D42A File Offset: 0x0000B62A
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000D432 File Offset: 0x0000B632
		public float defaultSpriteDPI
		{
			get
			{
				return this.m_DefaultSpriteDPI;
			}
			set
			{
				this.m_DefaultSpriteDPI = Mathf.Max(1f, value);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000D445 File Offset: 0x0000B645
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000D44D File Offset: 0x0000B64D
		public float dynamicPixelsPerUnit
		{
			get
			{
				return this.m_DynamicPixelsPerUnit;
			}
			set
			{
				this.m_DynamicPixelsPerUnit = value;
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000D458 File Offset: 0x0000B658
		protected CanvasScaler()
		{
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000D4D4 File Offset: 0x0000B6D4
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_Canvas = base.GetComponent<Canvas>();
			this.Handle();
			Canvas.preWillRenderCanvases += this.Canvas_preWillRenderCanvases;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000D4FF File Offset: 0x0000B6FF
		private void Canvas_preWillRenderCanvases()
		{
			this.Handle();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000D507 File Offset: 0x0000B707
		protected override void OnDisable()
		{
			this.SetScaleFactor(1f);
			this.SetReferencePixelsPerUnit(100f);
			Canvas.preWillRenderCanvases -= this.Canvas_preWillRenderCanvases;
			base.OnDisable();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000D538 File Offset: 0x0000B738
		protected virtual void Handle()
		{
			if (this.m_Canvas == null || !this.m_Canvas.isRootCanvas)
			{
				return;
			}
			if (this.m_Canvas.renderMode == RenderMode.WorldSpace)
			{
				this.HandleWorldCanvas();
				return;
			}
			switch (this.m_UiScaleMode)
			{
			case CanvasScaler.ScaleMode.ConstantPixelSize:
				this.HandleConstantPixelSize();
				return;
			case CanvasScaler.ScaleMode.ScaleWithScreenSize:
				this.HandleScaleWithScreenSize();
				return;
			case CanvasScaler.ScaleMode.ConstantPhysicalSize:
				this.HandleConstantPhysicalSize();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000D5A4 File Offset: 0x0000B7A4
		protected virtual void HandleWorldCanvas()
		{
			this.SetScaleFactor(this.m_DynamicPixelsPerUnit);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000D5BE File Offset: 0x0000B7BE
		protected virtual void HandleConstantPixelSize()
		{
			this.SetScaleFactor(this.m_ScaleFactor);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000D5D8 File Offset: 0x0000B7D8
		protected virtual void HandleScaleWithScreenSize()
		{
			Vector2 renderingDisplaySize = this.m_Canvas.renderingDisplaySize;
			int targetDisplay = this.m_Canvas.targetDisplay;
			if (targetDisplay > 0 && targetDisplay < Display.displays.Length)
			{
				Display display = Display.displays[targetDisplay];
				renderingDisplaySize = new Vector2((float)display.renderingWidth, (float)display.renderingHeight);
			}
			float num = 0f;
			switch (this.m_ScreenMatchMode)
			{
			case CanvasScaler.ScreenMatchMode.MatchWidthOrHeight:
			{
				float num2 = Mathf.Log(renderingDisplaySize.x / this.m_ReferenceResolution.x, 2f);
				float num3 = Mathf.Log(renderingDisplaySize.y / this.m_ReferenceResolution.y, 2f);
				float num4 = Mathf.Lerp(num2, num3, this.m_MatchWidthOrHeight);
				num = Mathf.Pow(2f, num4);
				break;
			}
			case CanvasScaler.ScreenMatchMode.Expand:
				num = Mathf.Min(renderingDisplaySize.x / this.m_ReferenceResolution.x, renderingDisplaySize.y / this.m_ReferenceResolution.y);
				break;
			case CanvasScaler.ScreenMatchMode.Shrink:
				num = Mathf.Max(renderingDisplaySize.x / this.m_ReferenceResolution.x, renderingDisplaySize.y / this.m_ReferenceResolution.y);
				break;
			}
			this.SetScaleFactor(num);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000D710 File Offset: 0x0000B910
		protected virtual void HandleConstantPhysicalSize()
		{
			float dpi = Screen.dpi;
			float num = ((dpi == 0f) ? this.m_FallbackScreenDPI : dpi);
			float num2 = 1f;
			switch (this.m_PhysicalUnit)
			{
			case CanvasScaler.Unit.Centimeters:
				num2 = 2.54f;
				break;
			case CanvasScaler.Unit.Millimeters:
				num2 = 25.4f;
				break;
			case CanvasScaler.Unit.Inches:
				num2 = 1f;
				break;
			case CanvasScaler.Unit.Points:
				num2 = 72f;
				break;
			case CanvasScaler.Unit.Picas:
				num2 = 6f;
				break;
			}
			this.SetScaleFactor(num / num2);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit * num2 / this.m_DefaultSpriteDPI);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000D7A2 File Offset: 0x0000B9A2
		protected void SetScaleFactor(float scaleFactor)
		{
			if (scaleFactor == this.m_PrevScaleFactor)
			{
				return;
			}
			this.m_Canvas.scaleFactor = scaleFactor;
			this.m_PrevScaleFactor = scaleFactor;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000D7C1 File Offset: 0x0000B9C1
		protected void SetReferencePixelsPerUnit(float referencePixelsPerUnit)
		{
			if (referencePixelsPerUnit == this.m_PrevReferencePixelsPerUnit)
			{
				return;
			}
			this.m_Canvas.referencePixelsPerUnit = referencePixelsPerUnit;
			this.m_PrevReferencePixelsPerUnit = referencePixelsPerUnit;
		}

		// Token: 0x040000C2 RID: 194
		[Tooltip("Determines how UI elements in the Canvas are scaled.")]
		[SerializeField]
		private CanvasScaler.ScaleMode m_UiScaleMode;

		// Token: 0x040000C3 RID: 195
		[Tooltip("If a sprite has this 'Pixels Per Unit' setting, then one pixel in the sprite will cover one unit in the UI.")]
		[SerializeField]
		protected float m_ReferencePixelsPerUnit = 100f;

		// Token: 0x040000C4 RID: 196
		[Tooltip("Scales all UI elements in the Canvas by this factor.")]
		[SerializeField]
		protected float m_ScaleFactor = 1f;

		// Token: 0x040000C5 RID: 197
		[Tooltip("The resolution the UI layout is designed for. If the screen resolution is larger, the UI will be scaled up, and if it's smaller, the UI will be scaled down. This is done in accordance with the Screen Match Mode.")]
		[SerializeField]
		protected Vector2 m_ReferenceResolution = new Vector2(800f, 600f);

		// Token: 0x040000C6 RID: 198
		[Tooltip("A mode used to scale the canvas area if the aspect ratio of the current resolution doesn't fit the reference resolution.")]
		[SerializeField]
		protected CanvasScaler.ScreenMatchMode m_ScreenMatchMode;

		// Token: 0x040000C7 RID: 199
		[Tooltip("Determines if the scaling is using the width or height as reference, or a mix in between.")]
		[Range(0f, 1f)]
		[SerializeField]
		protected float m_MatchWidthOrHeight;

		// Token: 0x040000C8 RID: 200
		private const float kLogBase = 2f;

		// Token: 0x040000C9 RID: 201
		[Tooltip("The physical unit to specify positions and sizes in.")]
		[SerializeField]
		protected CanvasScaler.Unit m_PhysicalUnit = CanvasScaler.Unit.Points;

		// Token: 0x040000CA RID: 202
		[Tooltip("The DPI to assume if the screen DPI is not known.")]
		[SerializeField]
		protected float m_FallbackScreenDPI = 96f;

		// Token: 0x040000CB RID: 203
		[Tooltip("The pixels per inch to use for sprites that have a 'Pixels Per Unit' setting that matches the 'Reference Pixels Per Unit' setting.")]
		[SerializeField]
		protected float m_DefaultSpriteDPI = 96f;

		// Token: 0x040000CC RID: 204
		[Tooltip("The amount of pixels per unit to use for dynamically created bitmaps in the UI, such as Text.")]
		[SerializeField]
		protected float m_DynamicPixelsPerUnit = 1f;

		// Token: 0x040000CD RID: 205
		private Canvas m_Canvas;

		// Token: 0x040000CE RID: 206
		[NonSerialized]
		private float m_PrevScaleFactor = 1f;

		// Token: 0x040000CF RID: 207
		[NonSerialized]
		private float m_PrevReferencePixelsPerUnit = 100f;

		// Token: 0x040000D0 RID: 208
		[SerializeField]
		protected bool m_PresetInfoIsWorld;

		// Token: 0x02000097 RID: 151
		public enum ScaleMode
		{
			// Token: 0x040002B9 RID: 697
			ConstantPixelSize,
			// Token: 0x040002BA RID: 698
			ScaleWithScreenSize,
			// Token: 0x040002BB RID: 699
			ConstantPhysicalSize
		}

		// Token: 0x02000098 RID: 152
		public enum ScreenMatchMode
		{
			// Token: 0x040002BD RID: 701
			MatchWidthOrHeight,
			// Token: 0x040002BE RID: 702
			Expand,
			// Token: 0x040002BF RID: 703
			Shrink
		}

		// Token: 0x02000099 RID: 153
		public enum Unit
		{
			// Token: 0x040002C1 RID: 705
			Centimeters,
			// Token: 0x040002C2 RID: 706
			Millimeters,
			// Token: 0x040002C3 RID: 707
			Inches,
			// Token: 0x040002C4 RID: 708
			Points,
			// Token: 0x040002C5 RID: 709
			Picas
		}
	}
}
