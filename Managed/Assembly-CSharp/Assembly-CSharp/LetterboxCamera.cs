using System;
using UnityEngine;
using XRL.UI;

// Token: 0x02000167 RID: 359
public class LetterboxCamera : MonoBehaviour
{
	// Token: 0x06000E27 RID: 3623 RVA: 0x0005B582 File Offset: 0x00059782
	public void Refresh()
	{
		this._Refresh = true;
	}

	// Token: 0x17000156 RID: 342
	// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0005B58B File Offset: 0x0005978B
	// (set) Token: 0x06000E29 RID: 3625 RVA: 0x0005B593 File Offset: 0x00059793
	public float DesiredZoomFactor
	{
		get
		{
			return this._DesiredZoomFactor;
		}
		set
		{
			if (this._DesiredZoomFactor != value)
			{
				this._DesiredZoomFactor = value;
			}
		}
	}

	// Token: 0x17000157 RID: 343
	// (get) Token: 0x06000E2A RID: 3626 RVA: 0x0005B5A5 File Offset: 0x000597A5
	// (set) Token: 0x06000E2B RID: 3627 RVA: 0x0005B5B3 File Offset: 0x000597B3
	public Vector3 DesiredPan
	{
		get
		{
			return this.ClampPanPosition(this._DesiredPosition);
		}
		set
		{
			if (this._DesiredPosition != value)
			{
				this._DesiredPosition = value;
			}
		}
	}

	// Token: 0x17000158 RID: 344
	// (get) Token: 0x06000E2C RID: 3628 RVA: 0x0005B5CA File Offset: 0x000597CA
	public Vector3 CurrentPosition
	{
		get
		{
			return base.gameObject.transform.localPosition;
		}
	}

	// Token: 0x17000159 RID: 345
	// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0005B5DC File Offset: 0x000597DC
	public Vector3 ActualPan
	{
		get
		{
			return this.CurrentPosition - this.targetAreaCameraCentered;
		}
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x0005B5EF File Offset: 0x000597EF
	public void Awake()
	{
		this.DesiredPan = new Vector3(0f, 0f, 0f);
		this.suspendUpdate = false;
	}

	// Token: 0x06000E2F RID: 3631 RVA: 0x0005B612 File Offset: 0x00059812
	public void SetPositionImmediately(Vector3 position)
	{
		if (this.UpdateDelay <= 0)
		{
			base.gameObject.transform.localPosition = position;
		}
		this.DesiredPan = position;
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x0005B638 File Offset: 0x00059838
	private Rect GetWorldCoordinates(RectTransform uiElement)
	{
		uiElement.GetWorldCorners(this._worldCorners);
		Vector3 vector = this.myCamera.ScreenToWorldPoint(this._worldCorners[0]);
		Vector3 vector2 = this.myCamera.ScreenToWorldPoint(this._worldCorners[2]);
		return new Rect(vector.x, vector.y, vector2.x - vector.x, vector2.y - vector.y);
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x0005B6AC File Offset: 0x000598AC
	public void SetTargetArea(RectTransform area)
	{
		if (this.lastArea == null || area != this.lastArea || this.DebugTargetArea.sizeDelta.x != area.rect.width || this.DebugTargetArea.sizeDelta.y != area.rect.height || this.DebugTargetArea.anchoredPosition.x != area.anchoredPosition.x || this.DebugTargetArea.anchoredPosition.y != area.anchoredPosition.y)
		{
			this._Refresh = true;
			this.lastArea = area;
			this.DebugTargetArea.sizeDelta = new Vector2(area.rect.width, area.rect.height);
			this.DebugTargetArea.anchoredPosition = new Vector3(area.anchoredPosition.x, area.anchoredPosition.y);
		}
		this.TargetArea = this.GetWorldCoordinates(area);
		if (this.ShowDebugTargetArea)
		{
			Rect worldCoordinates = this.GetWorldCoordinates(this.DebugTargetArea);
			this.TargetArea != worldCoordinates;
		}
		this.AdjustedTargetArea = new Rect(this.TargetArea.x - base.transform.position.x, this.TargetArea.y - base.transform.position.y, this.TargetArea.width, this.TargetArea.height);
	}

	// Token: 0x1700015A RID: 346
	// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0005B83F File Offset: 0x00059A3F
	public Camera myCamera
	{
		get
		{
			if (this._camera == null)
			{
				this._camera = base.GetComponent<Camera>();
			}
			return this._camera;
		}
	}

	// Token: 0x1700015B RID: 347
	// (get) Token: 0x06000E33 RID: 3635 RVA: 0x0005B864 File Offset: 0x00059A64
	private Vector3 targetAreaCameraCentered
	{
		get
		{
			return new Vector3(-(this.AdjustedTargetArea.width / 2f + this.AdjustedTargetArea.xMin), -(this.AdjustedTargetArea.height / 2f + this.AdjustedTargetArea.yMin), -10f);
		}
	}

	// Token: 0x06000E34 RID: 3636 RVA: 0x0005B8B8 File Offset: 0x00059AB8
	private Vector3 dockHuggingVector()
	{
		Vector3 letterboxTotalArea = this.GetLetterboxTotalArea();
		if (!this.ForcingFullscreen && letterboxTotalArea.x > 0f)
		{
			if (this.TileAreaAlignment == LetterboxCamera.TileAreaAlignmentType.Left)
			{
				return new Vector3(letterboxTotalArea.x / 2f, 0f, 0f);
			}
			if (this.TileAreaAlignment == LetterboxCamera.TileAreaAlignmentType.Right)
			{
				return new Vector3(-letterboxTotalArea.x / 2f, 0f, 0f);
			}
		}
		return Vector3.zero;
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x0005B930 File Offset: 0x00059B30
	private Vector3 ClampPanPosition(Vector3 p)
	{
		Vector3 vector = new Vector3(Mathf.Ceil(Mathf.Max(0f, this.tilesWide * this.tileWidth - this.AdjustedTargetArea.width) / 2f) * 2f, Mathf.Ceil(Mathf.Max(0f, this.tilesHigh * this.tileHeight - this.AdjustedTargetArea.height) / 2f) * 2f, 0f);
		Bounds bounds = new Bounds(Vector3.zero, vector);
		if (bounds.Contains(p))
		{
			return p;
		}
		return bounds.ClosestPoint(p);
	}

	// Token: 0x06000E36 RID: 3638 RVA: 0x0005B9D2 File Offset: 0x00059BD2
	public Vector3 GetLetterboxTotalArea()
	{
		return new Vector3(this.AdjustedTargetArea.width - this.tilesWide * this.tileWidth, this.AdjustedTargetArea.height - this.tilesHigh * this.tileHeight, 0f);
	}

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x06000E37 RID: 3639 RVA: 0x0005BA10 File Offset: 0x00059C10
	public bool isPixelPerfect
	{
		get
		{
			return GameManager.Instance.TargetZoomFactor > 0f && GameManager.Instance.TileScale >= 1;
		}
	}

	// Token: 0x1700015D RID: 349
	// (get) Token: 0x06000E38 RID: 3640 RVA: 0x0005BA35 File Offset: 0x00059C35
	public bool IsOverflowing
	{
		get
		{
			return this.AdjustedTargetArea.width < this.tilesWide * this.tileWidth || this.AdjustedTargetArea.height < this.tilesHigh * this.tileHeight;
		}
	}

	// Token: 0x06000E39 RID: 3641 RVA: 0x0005BA70 File Offset: 0x00059C70
	public void OnUpdate()
	{
		GameManager instance = GameManager.Instance;
		if (((instance != null) ? instance._ActiveGameView : null) == "Cinematic")
		{
			return;
		}
		this.AdjustedTargetArea = new Rect(this.TargetArea.x - base.transform.position.x, this.TargetArea.y - base.transform.position.y, this.TargetArea.width, this.TargetArea.height);
		if (this.DebugTargetArea.gameObject.activeInHierarchy != this.ShowDebugTargetArea)
		{
			RectTransform debugTargetArea = this.DebugTargetArea;
			if (debugTargetArea != null)
			{
				debugTargetArea.gameObject.SetActive(this.ShowDebugTargetArea);
			}
		}
		if (this.UpdateDelay > 0)
		{
			this.UpdateDelay--;
			return;
		}
		Vector3 vector = this.targetAreaCameraCentered + this.dockHuggingVector();
		if (this.Pannable)
		{
			vector += this.ClampPanPosition(this.DesiredPan);
		}
		if (Options.PlayScale == Options.PlayAreaScaleTypes.Fit || GameManager.Instance.TargetZoomFactor == -1f || this.ForcingFullscreen)
		{
			this.baseOrthographicStageScale = Mathf.Min(this.AdjustedTargetArea.width / (this.tilesWide * this.tileWidth), this.AdjustedTargetArea.height / (this.tilesHigh * this.tileHeight));
		}
		else
		{
			this.baseOrthographicStageScale = Mathf.Max(this.AdjustedTargetArea.width / (this.tilesWide * this.tileWidth), this.AdjustedTargetArea.height / (this.tilesHigh * this.tileHeight));
		}
		if (this.isPixelPerfect)
		{
			this.baseOrthographicSize = (float)(Screen.height / (2 * GameManager.Instance.TileScale));
		}
		else
		{
			this.baseOrthographicSize = this.myCamera.orthographicSize / this.baseOrthographicStageScale;
		}
		if (float.IsNaN(this.baseOrthographicSize) || this.baseOrthographicSize <= 0f)
		{
			return;
		}
		if (this.baseOrthographicSize < 32f)
		{
			this.baseOrthographicSize = 32f;
		}
		if (this.baseOrthographicSize > 1000f)
		{
			this.baseOrthographicSize = 1000f;
		}
		float num = this.baseOrthographicSize / this.DesiredZoomFactor;
		if (this.ForcingFullscreen)
		{
			num = Math.Max(this.myCamera.orthographicSize / this.baseOrthographicStageScale, num);
		}
		if (!Mathf.Approximately(this.myCamera.orthographicSize, num))
		{
			this.myCamera.orthographicSize = num;
			this.UnitsPerPixel = num * 2f / (float)Screen.height;
			if (this.lastArea != null)
			{
				this.SetTargetArea(this.lastArea);
			}
		}
		if (this.suspendUpdate)
		{
			this.DesiredZoomFactor = Math.Max(1f, GameManager.Instance.TargetZoomFactor);
			return;
		}
		if (this._Refresh || Screen.width != this.LastWidth || Screen.height != this.LastHeight || this.DesiredZoomFactor != this.LastZoomFactor || GameManager.Instance.ModernUI != this.lastPrerelaseStage || GameManager.Instance.StageScale != this.lastStageScale || (base.transform.localPosition - vector).magnitude > 1.1f)
		{
			this._Refresh = false;
			this.LastWidth = Screen.width;
			this.LastHeight = Screen.height;
			this.LastZoomFactor = this.DesiredZoomFactor;
			this.lastPrerelaseStage = GameManager.Instance.ModernUI;
			this.lastStageScale = GameManager.Instance.StageScale;
			base.GetComponent<CC_AnalogTV>().scanlinesCount = 1853f;
			if (this.Pannable)
			{
				base.transform.localPosition = this.targetAreaCameraCentered + this.dockHuggingVector() + this.ClampPanPosition(this.DesiredPan);
				return;
			}
			base.transform.localPosition = this.targetAreaCameraCentered + this.dockHuggingVector();
		}
	}

	// Token: 0x06000E3A RID: 3642 RVA: 0x0005BE50 File Offset: 0x0005A050
	public void DockLeft(float fudgeFactor = 1f)
	{
		this.DesiredPan = new Vector3(this.GetEdgeDistance(fudgeFactor), 0f, -10f);
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x0005BE6E File Offset: 0x0005A06E
	public void DockRight(float fudgeFactor = 1f)
	{
		this.DesiredPan = new Vector3(-this.GetEdgeDistance(fudgeFactor), 0f, -10f);
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x0005BE90 File Offset: 0x0005A090
	public float GetEdgeDistance(float fudgeFactor)
	{
		float num = (float)this.LastHeight / 2f / this.LastOrthographicSize;
		float num2 = this.LastOrthographicSize / ((float)this.LastHeight / 2f);
		return ((float)this.LastWidth - this.DesiredWidth * fudgeFactor * num) / 2f * num2;
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x0005BEE4 File Offset: 0x0005A0E4
	public float GetStageEdge()
	{
		float num = (float)this.LastHeight / 2f / this.LastOrthographicSize;
		return ((float)this.LastWidth - this.DesiredWidth * num) / (float)GameManager.Instance.StageScale;
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x0005BF22 File Offset: 0x0005A122
	public float GetScale()
	{
		return (float)this.LastHeight / 2f / this.LastOrthographicSize;
	}

	// Token: 0x040009DB RID: 2523
	private bool _Refresh;

	// Token: 0x040009DC RID: 2524
	public bool ForcingFullscreen;

	// Token: 0x040009DD RID: 2525
	public int UpdateDelay;

	// Token: 0x040009DE RID: 2526
	public float _DesiredZoomFactor = 1f;

	// Token: 0x040009DF RID: 2527
	public bool Pannable;

	// Token: 0x040009E0 RID: 2528
	public Vector3 _DesiredPosition;

	// Token: 0x040009E1 RID: 2529
	public bool ShowDebugTargetArea = true;

	// Token: 0x040009E2 RID: 2530
	public RectTransform DebugTargetArea;

	// Token: 0x040009E3 RID: 2531
	public Rect TargetArea;

	// Token: 0x040009E4 RID: 2532
	public float InitialDesiredWidth = 1280f;

	// Token: 0x040009E5 RID: 2533
	public float DesiredWidth = 1280f;

	// Token: 0x040009E6 RID: 2534
	public float LastOrthographicSize;

	// Token: 0x040009E7 RID: 2535
	private int LastWidth;

	// Token: 0x040009E8 RID: 2536
	private int LastHeight;

	// Token: 0x040009E9 RID: 2537
	private float LastZoomFactor;

	// Token: 0x040009EA RID: 2538
	private bool lastPrerelaseStage;

	// Token: 0x040009EB RID: 2539
	private double lastStageScale;

	// Token: 0x040009EC RID: 2540
	private Vector3[] _worldCorners = new Vector3[4];

	// Token: 0x040009ED RID: 2541
	public RectTransform lastArea;

	// Token: 0x040009EE RID: 2542
	public Rect AdjustedTargetArea;

	// Token: 0x040009EF RID: 2543
	public LetterboxCamera.TileAreaAlignmentType TileAreaAlignment;

	// Token: 0x040009F0 RID: 2544
	public bool suspendUpdate;

	// Token: 0x040009F1 RID: 2545
	public float widePadding = 216f;

	// Token: 0x040009F2 RID: 2546
	public float wideLetterboxMagic = 600f;

	// Token: 0x040009F3 RID: 2547
	public float tileWidth = 16f;

	// Token: 0x040009F4 RID: 2548
	public float tileHeight = 24f;

	// Token: 0x040009F5 RID: 2549
	public float tilesWide = 80f;

	// Token: 0x040009F6 RID: 2550
	public float tilesHigh = 25f;

	// Token: 0x040009F7 RID: 2551
	private Camera _camera;

	// Token: 0x040009F8 RID: 2552
	public float baseOrthographicStageScale;

	// Token: 0x040009F9 RID: 2553
	public float baseOrthographicSize;

	// Token: 0x040009FA RID: 2554
	public float UnitsPerPixel;

	// Token: 0x0200160F RID: 5647
	public enum TileAreaAlignmentType
	{
		// Token: 0x040058E8 RID: 22760
		Left,
		// Token: 0x040058E9 RID: 22761
		Right,
		// Token: 0x040058EA RID: 22762
		Center
	}
}
