using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200012C RID: 300
public class SwipeCompass : MonoBehaviour
{
	// Token: 0x06000B60 RID: 2912 RVA: 0x0004BE1A File Offset: 0x0004A01A
	private IEnumerator Start()
	{
		if (this.clickEdgeToSwitch && !this.allowInput)
		{
			Debug.LogWarning("You have enabled clickEdgeToSwitch, but it will not work because allowInput is disabled!", this);
		}
		if (this.pxDistBetweenValues.x == 0f && this.pxDistBetweenValues.y == 0f)
		{
			Debug.LogWarning("pxDistBetweenValues is zero - you won't be able to swipe with this setting...", this);
		}
		yield return new WaitForSeconds(0.2f);
		if (!this.skipAutoSetup)
		{
			this.Setup();
		}
		yield break;
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x0004BE2C File Offset: 0x0004A02C
	public void Setup()
	{
		this.partFactor.x = 1f / this.pxDistBetweenValues.x;
		this.partFactor.y = 1f / this.pxDistBetweenValues.y;
		this.smoothValue.x = Mathf.Round(this.currentValue.x);
		this.smoothValue.y = Mathf.Round(this.currentValue.y);
		this.currentValue = this.startValue;
		if (this.activeArea != new Rect(0f, 0f, 0f, 0f))
		{
			this.SetActiveArea(this.activeArea);
		}
		if (this.leftEdgeRectForClickSwitch == new Rect(0f, 0f, 0f, 0f))
		{
			this.CalculateEdgeRectsFromActiveArea();
		}
		if (this.matrix == Matrix4x4.zero)
		{
			this.matrix = Matrix4x4.identity.inverse;
		}
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x0004BF36 File Offset: 0x0004A136
	public void SetActiveArea(Rect myRect)
	{
		this.activeArea = myRect;
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x0004BF3F File Offset: 0x0004A13F
	public void CalculateEdgeRectsFromActiveArea()
	{
		this.CalculateEdgeRectsFromActiveArea(this.activeArea);
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x0004BF50 File Offset: 0x0004A150
	public void CalculateEdgeRectsFromActiveArea(Rect myRect)
	{
		this.leftEdgeRectForClickSwitch.x = myRect.x;
		this.leftEdgeRectForClickSwitch.y = myRect.y;
		this.leftEdgeRectForClickSwitch.width = myRect.width * 0.5f;
		this.leftEdgeRectForClickSwitch.height = myRect.height;
		this.rightEdgeRectForClickSwitch.x = myRect.x + myRect.width * 0.5f;
		this.rightEdgeRectForClickSwitch.y = myRect.y;
		this.rightEdgeRectForClickSwitch.width = myRect.width * 0.5f;
		this.rightEdgeRectForClickSwitch.height = myRect.height;
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x0004C007 File Offset: 0x0004A207
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x0004C018 File Offset: 0x0004A218
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		if (arr.Length == 5)
		{
			return num * 0.2f;
		}
		return num / (float)arr.Length;
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x0004C054 File Offset: 0x0004A254
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x0004C074 File Offset: 0x0004A274
	private void Update()
	{
		if (this.controlEnabled)
		{
			this.touched = false;
			if (this.allowInput && (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)))
			{
				this.pos = new Vector3(Input.mousePosition[0], (float)Screen.height - Input.mousePosition[1], 0f);
				this.tPos = this.matrix.inverse.MultiplyPoint3x4(this.pos);
				if (Input.GetMouseButtonDown(0) && this.activeArea.Contains(this.tPos))
				{
					this.mouseStartArea = 1;
				}
				if (this.mouseStartArea == 1)
				{
					this.touched = true;
					if (Input.GetMouseButtonDown(0))
					{
						this.mStartPos = this.tPos;
						this.smoothStartPos.x = this.smoothValue.x + this.tPos.x * this.partFactor.x;
						this.smoothStartPos.y = this.smoothValue.y + this.tPos.y * this.partFactor.y;
						this.FillArrayWithValue(this.prevSmoothValueX, this.smoothValue.x);
						this.FillArrayWithValue(this.prevSmoothValueY, this.smoothValue.y);
					}
					this.smoothValue.x = this.smoothStartPos.x - this.tPos.x * this.partFactor.x;
					this.smoothValue.y = this.smoothStartPos.y - this.tPos.y * this.partFactor.y;
					if (this.smoothValue.x < -0.12f)
					{
						this.smoothValue.x = -0.12f;
					}
					else if (this.smoothValue.x > this.maxValue.x + 0.12f)
					{
						this.smoothValue.x = this.maxValue.x + 0.12f;
					}
					if (this.smoothValue.y < -0.12f)
					{
						this.smoothValue.y = -0.12f;
					}
					else if (this.smoothValue.y > this.maxValue.y + 0.12f)
					{
						this.smoothValue.y = this.maxValue.y + 0.12f;
					}
					if (Input.GetMouseButtonUp(0))
					{
						if ((this.tPos - this.mStartPos).sqrMagnitude < 25f)
						{
							if (this.clickEdgeToSwitch)
							{
								if (this.leftEdgeRectForClickSwitch.Contains(this.tPos))
								{
									this.currentValue.x = this.currentValue.x - 1f;
									if (this.currentValue.x < 0f)
									{
										this.currentValue.x = 0f;
									}
								}
								else if (this.rightEdgeRectForClickSwitch.Contains(this.tPos))
								{
									this.currentValue.x = this.currentValue.x + 1f;
									if (this.currentValue.x > this.maxValue.x)
									{
										this.currentValue.x = this.maxValue.x;
									}
								}
							}
							else
							{
								this.selected = !this.selected;
							}
						}
						else
						{
							if (this.currentValue.x - (this.smoothValue.x + (this.smoothValue.x - this.GetAvgValue(this.prevSmoothValueX))) > this.smoothDragOffset.x || this.currentValue.x - (this.smoothValue.x + (this.smoothValue.x - this.GetAvgValue(this.prevSmoothValueX))) < -this.smoothDragOffset.x)
							{
								this.currentValue.x = Mathf.Round(this.smoothValue.x + (this.smoothValue.x - this.GetAvgValue(this.prevSmoothValueX)));
								this.xVelocity = this.smoothValue.x - this.GetAvgValue(this.prevSmoothValueX);
								if (this.currentValue.x > this.maxValue.x)
								{
									this.currentValue.x = this.maxValue.x;
								}
								else if (this.currentValue.x < 0f)
								{
									this.currentValue.x = 0f;
								}
							}
							if (this.currentValue.y - (this.smoothValue.y + (this.smoothValue.y - this.GetAvgValue(this.prevSmoothValueY))) > this.smoothDragOffset.y || this.currentValue.y - (this.smoothValue.y + (this.smoothValue.y - this.GetAvgValue(this.prevSmoothValueY))) < -this.smoothDragOffset.y)
							{
								this.currentValue.y = Mathf.Round(this.smoothValue.y + (this.smoothValue.y - this.GetAvgValue(this.prevSmoothValueY)));
								this.yVelocity = this.smoothValue.y - this.GetAvgValue(this.prevSmoothValueY);
								if (this.currentValue.y > this.maxValue.y)
								{
									this.currentValue.y = this.maxValue.y;
								}
								else if (this.currentValue.y < 0f)
								{
									this.currentValue.y = 0f;
								}
							}
						}
						this.mouseStartArea = 0;
					}
					for (int i = 1; i < this.prevSmoothValueX.Length; i++)
					{
						this.prevSmoothValueX[i] = this.prevSmoothValueX[i - 1];
						this.prevSmoothValueY[i] = this.prevSmoothValueY[i - 1];
					}
					this.prevSmoothValueX[0] = this.smoothValue.x;
					this.prevSmoothValueY[0] = this.smoothValue.y;
				}
			}
			if (!this.touched)
			{
				this.smoothValue.x = Mathf.SmoothDamp(this.smoothValue.x, this.currentValue.x, ref this.xVelocity, 0.3f, this.maxSpeed.x, Time.realtimeSinceStartup - this.realtimeStamp);
				this.smoothValue.y = Mathf.SmoothDamp(this.smoothValue.y, this.currentValue.y, ref this.yVelocity, 0.3f, this.maxSpeed.y, Time.realtimeSinceStartup - this.realtimeStamp);
			}
			this.realtimeStamp = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x0004C744 File Offset: 0x0004A944
	private void OnGUI()
	{
		if (this.debug)
		{
			if (Input.touchCount > 0)
			{
				Rect rect = new Rect(Input.GetTouch(0).position.x + 15f, (float)Screen.height - Input.GetTouch(0).position.y - 60f, 200f, 100f);
				string text = "pos : ";
				Vector3 vector = this.pos;
				string text2 = vector.ToString();
				string text3 = "\ntPos: ";
				Vector2 vector2 = this.tPos;
				GUI.Label(rect, text + text2 + text3 + vector2.ToString());
			}
			GUI.matrix = this.matrix;
			GUI.Box(this.activeArea, GUIContent.none);
		}
	}

	// Token: 0x040007E1 RID: 2017
	public bool controlEnabled = true;

	// Token: 0x040007E2 RID: 2018
	public bool skipAutoSetup;

	// Token: 0x040007E3 RID: 2019
	public bool allowInput = true;

	// Token: 0x040007E4 RID: 2020
	public bool clickEdgeToSwitch = true;

	// Token: 0x040007E5 RID: 2021
	public Vector2 pxDistBetweenValues = new Vector2(200f, 50f);

	// Token: 0x040007E6 RID: 2022
	private Vector2 partFactor = new Vector2(1f, 1f);

	// Token: 0x040007E7 RID: 2023
	public Vector2 startValue = Vector2.zero;

	// Token: 0x040007E8 RID: 2024
	public Vector2 currentValue = Vector2.zero;

	// Token: 0x040007E9 RID: 2025
	public Vector2 maxValue = new Vector2(10f, 10f);

	// Token: 0x040007EA RID: 2026
	public Rect activeArea;

	// Token: 0x040007EB RID: 2027
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x040007EC RID: 2028
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x040007ED RID: 2029
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x040007EE RID: 2030
	private bool touched;

	// Token: 0x040007EF RID: 2031
	private int[] fingerStartArea = new int[20];

	// Token: 0x040007F0 RID: 2032
	private Vector2[] fingerStartPos = new Vector2[20];

	// Token: 0x040007F1 RID: 2033
	private int mouseStartArea;

	// Token: 0x040007F2 RID: 2034
	public Vector2 smoothValue = Vector2.zero;

	// Token: 0x040007F3 RID: 2035
	private Vector2 smoothStartPos = Vector2.zero;

	// Token: 0x040007F4 RID: 2036
	private Vector2 smoothDragOffset = new Vector2(0.2f, 0.2f);

	// Token: 0x040007F5 RID: 2037
	private float[] prevSmoothValueX = new float[5];

	// Token: 0x040007F6 RID: 2038
	private float[] prevSmoothValueY = new float[5];

	// Token: 0x040007F7 RID: 2039
	private float realtimeStamp;

	// Token: 0x040007F8 RID: 2040
	private float xVelocity;

	// Token: 0x040007F9 RID: 2041
	private float yVelocity;

	// Token: 0x040007FA RID: 2042
	public Vector2 maxSpeed = new Vector2(20f, 20f);

	// Token: 0x040007FB RID: 2043
	private Vector2 mStartPos;

	// Token: 0x040007FC RID: 2044
	private Vector3 pos;

	// Token: 0x040007FD RID: 2045
	private Vector2 tPos;

	// Token: 0x040007FE RID: 2046
	public bool selected;

	// Token: 0x040007FF RID: 2047
	public bool debug;
}
