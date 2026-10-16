using System;

namespace UnityEngine
{
	// Token: 0x02000043 RID: 67
	internal struct SliderHandler
	{
		// Token: 0x060004A3 RID: 1187 RVA: 0x00011894 File Offset: 0x0000FA94
		public SliderHandler(Rect position, float currentValue, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id, GUIStyle thumbExtent = null)
		{
			this.position = position;
			this.currentValue = currentValue;
			this.size = size;
			this.start = start;
			this.end = end;
			this.slider = slider;
			this.thumb = thumb;
			this.thumbExtent = thumbExtent;
			this.horiz = horiz;
			this.id = id;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000118F0 File Offset: 0x0000FAF0
		public float Handle()
		{
			bool flag = this.slider == null || this.thumb == null;
			float num;
			if (flag)
			{
				num = this.currentValue;
			}
			else
			{
				EventType eventType = this.CurrentEventType();
				EventType eventType2 = eventType;
				switch (eventType2)
				{
				case EventType.MouseDown:
					return this.OnMouseDown();
				case EventType.MouseUp:
					return this.OnMouseUp();
				case EventType.MouseMove:
					break;
				case EventType.MouseDrag:
					return this.OnMouseDrag();
				default:
					if (eventType2 == EventType.Repaint)
					{
						return this.OnRepaint();
					}
					break;
				}
				num = this.currentValue;
			}
			return num;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00011974 File Offset: 0x0000FB74
		private float OnMouseDown()
		{
			Rect rect = this.ThumbSelectionRect();
			bool flag = GUIUtility.HitTest(rect, this.CurrentEvent());
			Rect zero = Rect.zero;
			zero.xMin = Math.Min(this.position.xMin, rect.xMin);
			zero.xMax = Math.Max(this.position.xMax, rect.xMax);
			zero.yMin = Math.Min(this.position.yMin, rect.yMin);
			zero.yMax = Math.Max(this.position.yMax, rect.yMax);
			bool flag2 = this.IsEmptySlider() || (!GUIUtility.HitTest(zero, this.CurrentEvent()) && !flag);
			float num;
			if (flag2)
			{
				num = this.currentValue;
			}
			else
			{
				GUI.scrollTroughSide = 0;
				GUIUtility.hotControl = this.id;
				this.CurrentEvent().Use();
				bool flag3 = flag;
				if (flag3)
				{
					this.StartDraggingWithValue(this.ClampedCurrentValue());
					num = this.currentValue;
				}
				else
				{
					GUI.changed = true;
					bool flag4 = this.SupportsPageMovements();
					if (flag4)
					{
						this.SliderState().isDragging = false;
						GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(250.0);
						GUI.scrollTroughSide = this.CurrentScrollTroughSide();
						num = this.PageMovementValue();
					}
					else
					{
						float num2 = this.ValueForCurrentMousePosition();
						this.StartDraggingWithValue(num2);
						num = this.Clamp(num2);
					}
				}
			}
			return num;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00011B08 File Offset: 0x0000FD08
		private float OnMouseDrag()
		{
			bool flag = GUIUtility.hotControl != this.id;
			float num;
			if (flag)
			{
				num = this.currentValue;
			}
			else
			{
				SliderState sliderState = this.SliderState();
				bool flag2 = !sliderState.isDragging;
				if (flag2)
				{
					num = this.currentValue;
				}
				else
				{
					GUI.changed = true;
					this.CurrentEvent().Use();
					float num2 = this.MousePosition() - sliderState.dragStartPos;
					float num3 = sliderState.dragStartValue + num2 / this.ValuesPerPixel();
					num = this.Clamp(num3);
				}
			}
			return num;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00011B94 File Offset: 0x0000FD94
		private float OnMouseUp()
		{
			bool flag = GUIUtility.hotControl == this.id;
			if (flag)
			{
				this.CurrentEvent().Use();
				GUIUtility.hotControl = 0;
			}
			return this.currentValue;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00011BD4 File Offset: 0x0000FDD4
		private float OnRepaint()
		{
			bool flag = GUIUtility.HitTest(this.position, this.CurrentEvent());
			this.slider.Draw(this.position, GUIContent.none, this.id, false, flag);
			bool flag2 = this.currentValue >= Mathf.Min(this.start, this.end) && this.currentValue <= Mathf.Max(this.start, this.end);
			if (flag2)
			{
				bool flag3 = this.thumbExtent != null;
				if (flag3)
				{
					this.thumbExtent.Draw(this.ThumbExtRect(), GUIContent.none, this.id, false, flag);
				}
				this.thumb.Draw(this.ThumbRect(), GUIContent.none, this.id, false, flag);
			}
			bool flag4 = GUIUtility.hotControl != this.id || !flag || this.IsEmptySlider();
			float num;
			if (flag4)
			{
				num = this.currentValue;
			}
			else
			{
				Rect rect = this.ThumbRect();
				bool flag5 = this.horiz;
				if (flag5)
				{
					rect.y = this.position.y;
					rect.height = this.position.height;
				}
				else
				{
					rect.x = this.position.x;
					rect.width = this.position.width;
				}
				bool flag6 = GUIUtility.HitTest(rect, this.CurrentEvent());
				if (flag6)
				{
					bool flag7 = GUI.scrollTroughSide != 0;
					if (flag7)
					{
						GUIUtility.hotControl = 0;
					}
					num = this.currentValue;
				}
				else
				{
					GUI.InternalRepaintEditorWindow();
					bool flag8 = SystemClock.now < GUI.nextScrollStepTime;
					if (flag8)
					{
						num = this.currentValue;
					}
					else
					{
						bool flag9 = this.CurrentScrollTroughSide() != GUI.scrollTroughSide;
						if (flag9)
						{
							num = this.currentValue;
						}
						else
						{
							GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(30.0);
							bool flag10 = this.SupportsPageMovements();
							if (flag10)
							{
								this.SliderState().isDragging = false;
								GUI.changed = true;
								num = this.PageMovementValue();
							}
							else
							{
								num = this.ClampedCurrentValue();
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00011E0C File Offset: 0x0001000C
		private EventType CurrentEventType()
		{
			return this.CurrentEvent().GetTypeForControl(this.id);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00011E30 File Offset: 0x00010030
		private int CurrentScrollTroughSide()
		{
			float num = (this.horiz ? this.CurrentEvent().mousePosition.x : this.CurrentEvent().mousePosition.y);
			float num2 = (this.horiz ? this.ThumbRect().x : this.ThumbRect().y);
			return (num > num2) ? 1 : (-1);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00011E9C File Offset: 0x0001009C
		private bool IsEmptySlider()
		{
			return this.start == this.end;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00011EBC File Offset: 0x000100BC
		private bool SupportsPageMovements()
		{
			return this.size != 0f && GUI.usePageScrollbars;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00011EE4 File Offset: 0x000100E4
		private float PageMovementValue()
		{
			float num = this.currentValue;
			int num2 = ((this.start > this.end) ? (-1) : 1);
			bool flag = this.MousePosition() > this.PageUpMovementBound();
			if (flag)
			{
				num += this.size * (float)num2 * 0.9f;
			}
			else
			{
				num -= this.size * (float)num2 * 0.9f;
			}
			return this.Clamp(num);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00011F50 File Offset: 0x00010150
		private float PageUpMovementBound()
		{
			bool flag = this.horiz;
			float num;
			if (flag)
			{
				num = this.ThumbRect().xMax - this.position.x;
			}
			else
			{
				num = this.ThumbRect().yMax - this.position.y;
			}
			return num;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00011FAC File Offset: 0x000101AC
		private Event CurrentEvent()
		{
			return Event.current;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00011FC4 File Offset: 0x000101C4
		private float ValueForCurrentMousePosition()
		{
			bool flag = this.horiz;
			float num;
			if (flag)
			{
				num = (this.MousePosition() - this.ThumbRect().width * 0.5f) / this.ValuesPerPixel() + this.start - this.size * 0.5f;
			}
			else
			{
				num = (this.MousePosition() - this.ThumbRect().height * 0.5f) / this.ValuesPerPixel() + this.start - this.size * 0.5f;
			}
			return num;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00012050 File Offset: 0x00010250
		private float Clamp(float value)
		{
			return Mathf.Clamp(value, this.MinValue(), this.MaxValue());
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00012074 File Offset: 0x00010274
		private Rect ThumbSelectionRect()
		{
			return this.ThumbRect();
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00012090 File Offset: 0x00010290
		private void StartDraggingWithValue(float dragStartValue)
		{
			SliderState sliderState = this.SliderState();
			sliderState.dragStartPos = this.MousePosition();
			sliderState.dragStartValue = dragStartValue;
			sliderState.isDragging = true;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x000120C0 File Offset: 0x000102C0
		private SliderState SliderState()
		{
			return (SliderState)GUIUtility.GetStateObject(typeof(SliderState), this.id);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000120EC File Offset: 0x000102EC
		private Rect ThumbExtRect()
		{
			return new Rect(0f, 0f, this.thumbExtent.fixedWidth, this.thumbExtent.fixedHeight)
			{
				center = this.ThumbRect().center
			};
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00012140 File Offset: 0x00010340
		private Rect ThumbRect()
		{
			return this.horiz ? this.HorizontalThumbRect() : this.VerticalThumbRect();
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00012168 File Offset: 0x00010368
		private Rect VerticalThumbRect()
		{
			Rect rect = this.thumb.margin.Remove(this.slider.padding.Remove(this.position));
			float num = ((this.thumb.fixedWidth != 0f) ? this.thumb.fixedWidth : rect.width);
			float num2 = this.ThumbSize();
			float num3 = this.ValuesPerPixel();
			bool flag = this.start < this.end;
			Rect rect2;
			if (flag)
			{
				rect2 = new Rect(rect.x, (this.ClampedCurrentValue() - this.start) * num3 + rect.y, num, this.size * num3 + num2);
			}
			else
			{
				rect2 = new Rect(rect.x, (this.ClampedCurrentValue() + this.size - this.start) * num3 + rect.y, num, this.size * -num3 + num2);
			}
			return rect2;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00012258 File Offset: 0x00010458
		private Rect HorizontalThumbRect()
		{
			Rect rect = this.thumb.margin.Remove(this.slider.padding.Remove(this.position));
			float num = ((this.thumb.fixedHeight != 0f) ? this.thumb.fixedHeight : rect.height);
			float num2 = this.ThumbSize();
			float num3 = this.ValuesPerPixel();
			bool flag = this.start < this.end;
			Rect rect2;
			if (flag)
			{
				rect2 = new Rect((this.ClampedCurrentValue() - this.start) * num3 + rect.x, rect.y, this.size * num3 + num2, num);
			}
			else
			{
				rect2 = new Rect((this.ClampedCurrentValue() + this.size - this.start) * num3 + rect.x, rect.y, this.size * -num3 + num2, num);
			}
			return rect2;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00012344 File Offset: 0x00010544
		private float ClampedCurrentValue()
		{
			return this.Clamp(this.currentValue);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00012364 File Offset: 0x00010564
		private float MousePosition()
		{
			bool flag = this.horiz;
			float num;
			if (flag)
			{
				num = this.CurrentEvent().mousePosition.x - this.position.x;
			}
			else
			{
				num = this.CurrentEvent().mousePosition.y - this.position.y;
			}
			return num;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000123C4 File Offset: 0x000105C4
		private float ValuesPerPixel()
		{
			float num = ((this.end == this.start) ? 1f : (this.end - this.start));
			bool flag = this.horiz;
			float num2;
			if (flag)
			{
				num2 = (this.position.width - (float)this.slider.padding.horizontal - this.ThumbSize()) / num;
			}
			else
			{
				num2 = (this.position.height - (float)this.slider.padding.vertical - this.ThumbSize()) / num;
			}
			return num2;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00012458 File Offset: 0x00010658
		private float ThumbSize()
		{
			bool flag = this.horiz;
			float num;
			if (flag)
			{
				num = ((this.thumb.fixedWidth != 0f) ? this.thumb.fixedWidth : ((float)this.thumb.padding.horizontal));
			}
			else
			{
				num = ((this.thumb.fixedHeight != 0f) ? this.thumb.fixedHeight : ((float)this.thumb.padding.vertical));
			}
			return num;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000124D8 File Offset: 0x000106D8
		private float MaxValue()
		{
			return Mathf.Max(this.start, this.end) - this.size;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00012504 File Offset: 0x00010704
		private float MinValue()
		{
			return Mathf.Min(this.start, this.end);
		}

		// Token: 0x0400014B RID: 331
		private readonly Rect position;

		// Token: 0x0400014C RID: 332
		private readonly float currentValue;

		// Token: 0x0400014D RID: 333
		private readonly float size;

		// Token: 0x0400014E RID: 334
		private readonly float start;

		// Token: 0x0400014F RID: 335
		private readonly float end;

		// Token: 0x04000150 RID: 336
		private readonly GUIStyle slider;

		// Token: 0x04000151 RID: 337
		private readonly GUIStyle thumb;

		// Token: 0x04000152 RID: 338
		private readonly GUIStyle thumbExtent;

		// Token: 0x04000153 RID: 339
		private readonly bool horiz;

		// Token: 0x04000154 RID: 340
		private readonly int id;
	}
}
