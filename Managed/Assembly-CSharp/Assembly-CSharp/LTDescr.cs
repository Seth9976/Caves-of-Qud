using System;
using System.Collections;
using QupKit;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000109 RID: 265
public class LTDescr
{
	// Token: 0x17000132 RID: 306
	// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0003D391 File Offset: 0x0003B591
	// (set) Token: 0x060009A6 RID: 2470 RVA: 0x0003D399 File Offset: 0x0003B599
	public Vector3 diff
	{
		get
		{
			return this._diff;
		}
		set
		{
			this._diff = value;
		}
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x0003D3A4 File Offset: 0x0003B5A4
	public override string ToString()
	{
		string[] array = new string[25];
		int num = 0;
		string text;
		if (!(this.trans != null))
		{
			text = "gameObject:null";
		}
		else
		{
			string text2 = "gameObject:";
			GameObject gameObject = this.trans.gameObject;
			text = text2 + ((gameObject != null) ? gameObject.ToString() : null);
		}
		array[num] = text;
		array[1] = " toggle:";
		array[2] = this.toggle.ToString();
		array[3] = " passed:";
		array[4] = this.passed.ToString();
		array[5] = " time:";
		array[6] = this.time.ToString();
		array[7] = " delay:";
		array[8] = this.delay.ToString();
		array[9] = " direction:";
		array[10] = this.direction.ToString();
		array[11] = " from:";
		int num2 = 12;
		Vector3 vector = this.from;
		array[num2] = vector.ToString();
		array[13] = " to:";
		int num3 = 14;
		vector = this.to;
		array[num3] = vector.ToString();
		array[15] = " type:";
		array[16] = this.type.ToString();
		array[17] = " ease:";
		array[18] = this.tweenType.ToString();
		array[19] = " useEstimatedTime:";
		array[20] = this.useEstimatedTime.ToString();
		array[21] = " id:";
		array[22] = this.id.ToString();
		array[23] = " hasInitiliazed:";
		array[24] = this.hasInitiliazed.ToString();
		return string.Concat(array);
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x0003D537 File Offset: 0x0003B737
	[Obsolete("Use 'LeanTween.cancel( id )' instead")]
	public LTDescr cancel(GameObject gameObject)
	{
		if (gameObject == this.trans.gameObject)
		{
			LeanTween.removeTween((int)this._id, this.uniqueId);
		}
		return this;
	}

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x060009AA RID: 2474 RVA: 0x0003D55E File Offset: 0x0003B75E
	public int uniqueId
	{
		get
		{
			return (int)(this._id | (this.counter << 16));
		}
	}

	// Token: 0x17000134 RID: 308
	// (get) Token: 0x060009AB RID: 2475 RVA: 0x0003D570 File Offset: 0x0003B770
	public int id
	{
		get
		{
			return this.uniqueId;
		}
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x0003D578 File Offset: 0x0003B778
	public void reset()
	{
		this.toggle = true;
		this.trans = null;
		this.passed = (this.delay = (this.lastVal = 0f));
		this.hasUpdateCallback = (this.useEstimatedTime = (this.useFrames = (this.hasInitiliazed = (this.onCompleteOnRepeat = (this.destroyOnComplete = (this.onCompleteOnStart = (this.useManualTime = false)))))));
		this.animationCurve = null;
		this.tweenType = LeanTweenType.linear;
		this.loopType = LeanTweenType.once;
		this.loopCount = 0;
		this.direction = (this.directionLast = (this.overshoot = 1f));
		this.period = 0.3f;
		this.point = Vector3.zero;
		this.cleanup();
		LTDescr.global_counter += 1U;
		if (LTDescr.global_counter > 32768U)
		{
			LTDescr.global_counter = 0U;
		}
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x0003D66C File Offset: 0x0003B86C
	public void cleanup()
	{
		this.onUpdateFloat = null;
		this.onUpdateFloatRatio = null;
		this.onUpdateVector2 = null;
		this.onUpdateVector3 = null;
		this.onUpdateFloatObject = null;
		this.onUpdateVector3Object = null;
		this.onUpdateColor = null;
		this.onComplete = null;
		this.onCompleteObject = null;
		this.onCompleteParam = null;
		this.onStart = null;
		this.controlLayout = null;
		this.rectTransform = null;
		this.uiText = null;
		this.uiImage = null;
		this.sprites = null;
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x0003D6EC File Offset: 0x0003B8EC
	public void init()
	{
		this.hasInitiliazed = true;
		if (this.onStart != null)
		{
			this.onStart();
		}
		switch (this.type)
		{
		case TweenAction.MOVE_X:
			this.from.x = this.trans.position.x;
			goto IL_0B73;
		case TweenAction.MOVE_Y:
			this.from.x = this.trans.position.y;
			goto IL_0B73;
		case TweenAction.MOVE_Z:
			this.from.x = this.trans.position.z;
			goto IL_0B73;
		case TweenAction.MOVE_LOCAL_X:
			this.from.x = this.trans.localPosition.x;
			goto IL_0B73;
		case TweenAction.MOVE_LOCAL_Y:
			this.from.x = this.trans.localPosition.y;
			goto IL_0B73;
		case TweenAction.MOVE_LOCAL_Z:
			this.from.x = this.trans.localPosition.z;
			goto IL_0B73;
		case TweenAction.MOVE_CURVED:
		case TweenAction.MOVE_CURVED_LOCAL:
		case TweenAction.MOVE_SPLINE:
		case TweenAction.MOVE_SPLINE_LOCAL:
			this.from.x = 0f;
			goto IL_0B73;
		case TweenAction.SCALE_X:
			this.from.x = this.trans.localScale.x;
			goto IL_0B73;
		case TweenAction.SCALE_Y:
			this.from.x = this.trans.localScale.y;
			goto IL_0B73;
		case TweenAction.SCALE_Z:
			this.from.x = this.trans.localScale.z;
			goto IL_0B73;
		case TweenAction.ROTATE_X:
			this.from.x = this.trans.eulerAngles.x;
			this.to.x = LeanTween.closestRot(this.from.x, this.to.x);
			goto IL_0B73;
		case TweenAction.ROTATE_Y:
			this.from.x = this.trans.eulerAngles.y;
			this.to.x = LeanTween.closestRot(this.from.x, this.to.x);
			goto IL_0B73;
		case TweenAction.ROTATE_Z:
			this.from.x = this.trans.eulerAngles.z;
			this.to.x = LeanTween.closestRot(this.from.x, this.to.x);
			goto IL_0B73;
		case TweenAction.ROTATE_AROUND:
			this.lastVal = 0f;
			this.from.x = 0f;
			this.origRotation = this.trans.rotation;
			goto IL_0B73;
		case TweenAction.ROTATE_AROUND_LOCAL:
			this.lastVal = 0f;
			this.from.x = 0f;
			this.origRotation = this.trans.localRotation;
			goto IL_0B73;
		case TweenAction.CANVAS_ROTATEAROUND:
		case TweenAction.CANVAS_ROTATEAROUND_LOCAL:
			this.lastVal = 0f;
			this.from.x = 0f;
			this.origRotation = this.rectTransform.rotation;
			goto IL_0B73;
		case TweenAction.CANVAS_PLAYSPRITE:
			this.uiImage = this.trans.gameObject.GetComponent<Image>();
			this.from.x = 0f;
			goto IL_0B73;
		case TweenAction.ALPHA:
		{
			SpriteRenderer component = this.trans.gameObject.GetComponent<SpriteRenderer>();
			if (component != null)
			{
				this.from.x = component.color.a;
				goto IL_0B73;
			}
			if (this.trans.gameObject.GetComponent<Image>() != null)
			{
				this.from.x = this.trans.gameObject.GetComponent<Image>().color.a;
				goto IL_0B73;
			}
			if (this.trans.gameObject.GetComponent<CanvasGroup>() != null)
			{
				this.from.x = this.trans.gameObject.GetComponent<CanvasGroup>().alpha;
				goto IL_0B73;
			}
			if (this.trans.gameObject.GetComponent<Renderer>() != null && this.trans.gameObject.GetComponent<Renderer>().material.HasProperty("_Color"))
			{
				this.from.x = this.trans.gameObject.GetComponent<Renderer>().material.color.a;
				goto IL_0B73;
			}
			if (this.trans.gameObject.GetComponent<Renderer>() != null && this.trans.gameObject.GetComponent<Renderer>().material.HasProperty("_TintColor"))
			{
				Color color = this.trans.gameObject.GetComponent<Renderer>().material.GetColor("_TintColor");
				this.from.x = color.a;
				goto IL_0B73;
			}
			if (this.trans.childCount <= 0)
			{
				goto IL_0B73;
			}
			using (IEnumerator enumerator = this.trans.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					if (transform.gameObject.GetComponent<Renderer>() != null)
					{
						Color color2 = transform.gameObject.GetComponent<Renderer>().material.color;
						this.from.x = color2.a;
						break;
					}
				}
				goto IL_0B73;
			}
			break;
		}
		case TweenAction.TEXT_ALPHA:
			this.uiText = this.trans.gameObject.GetComponent<Text>();
			if (this.uiText != null)
			{
				this.from.x = this.uiText.color.a;
				goto IL_0B73;
			}
			goto IL_0B73;
		case TweenAction.CANVAS_ALPHA:
			goto IL_0979;
		case TweenAction.ALPHA_VERTEX:
			this.from.x = (float)this.trans.GetComponent<MeshFilter>().mesh.colors32[0].a;
			goto IL_0B73;
		case TweenAction.COLOR:
		{
			SpriteRenderer component2 = this.trans.gameObject.GetComponent<SpriteRenderer>();
			if (component2 != null)
			{
				Color color3 = component2.color;
				this.setFromColor(color3);
				goto IL_0B73;
			}
			if (this.trans.gameObject.GetComponent<Renderer>() != null && this.trans.gameObject.GetComponent<Renderer>().material.HasProperty("_Color"))
			{
				Color color4 = this.trans.gameObject.GetComponent<Renderer>().material.color;
				this.setFromColor(color4);
				goto IL_0B73;
			}
			if (this.trans.gameObject.GetComponent<Renderer>() != null && this.trans.gameObject.GetComponent<Renderer>().material.HasProperty("_TintColor"))
			{
				Color color5 = this.trans.gameObject.GetComponent<Renderer>().material.GetColor("_TintColor");
				this.setFromColor(color5);
				goto IL_0B73;
			}
			if (this.trans.childCount > 0)
			{
				using (IEnumerator enumerator = this.trans.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						Transform transform2 = (Transform)obj2;
						if (transform2.gameObject.GetComponent<Renderer>() != null)
						{
							Color color6 = transform2.gameObject.GetComponent<Renderer>().material.color;
							this.setFromColor(color6);
							break;
						}
					}
					goto IL_0B73;
				}
				goto IL_0979;
			}
			goto IL_0B73;
		}
		case TweenAction.CALLBACK_COLOR:
			this.diff = new Vector3(1f, 0f, 0f);
			goto IL_0B73;
		case TweenAction.TEXT_COLOR:
			this.uiText = this.trans.gameObject.GetComponent<Text>();
			if (this.uiText != null)
			{
				this.setFromColor(this.uiText.color);
				goto IL_0B73;
			}
			goto IL_0B73;
		case TweenAction.CANVAS_COLOR:
			this.uiImage = this.trans.gameObject.GetComponent<Image>();
			if (this.uiImage != null)
			{
				this.setFromColor(this.uiImage.color);
				goto IL_0B73;
			}
			goto IL_0B73;
		case TweenAction.CANVAS_MOVE_X:
			this.from.x = this.rectTransform.anchoredPosition3D.x;
			goto IL_0B73;
		case TweenAction.CANVAS_MOVE_Y:
			this.from.x = this.rectTransform.anchoredPosition3D.y;
			goto IL_0B73;
		case TweenAction.CANVAS_MOVE_Z:
			this.from.x = this.rectTransform.anchoredPosition3D.z;
			goto IL_0B73;
		case TweenAction.CALLBACK:
		case TweenAction.VALUE3:
		case TweenAction.DELAYED_SOUND:
			goto IL_0B73;
		case TweenAction.MOVE:
			this.from = this.trans.position;
			goto IL_0B73;
		case TweenAction.MOVE_LOCAL:
			break;
		case TweenAction.ROTATE:
			this.from = this.trans.eulerAngles;
			this.to = new Vector3(LeanTween.closestRot(this.from.x, this.to.x), LeanTween.closestRot(this.from.y, this.to.y), LeanTween.closestRot(this.from.z, this.to.z));
			goto IL_0B73;
		case TweenAction.ROTATE_LOCAL:
			this.from = this.trans.localEulerAngles;
			this.to = new Vector3(LeanTween.closestRot(this.from.x, this.to.x), LeanTween.closestRot(this.from.y, this.to.y), LeanTween.closestRot(this.from.z, this.to.z));
			goto IL_0B73;
		case TweenAction.SCALE:
			this.from = this.trans.localScale;
			goto IL_0B73;
		case TweenAction.GUI_MOVE:
			this.from = new Vector3(this.ltRect.rect.x, this.ltRect.rect.y, 0f);
			goto IL_0B73;
		case TweenAction.GUI_MOVE_MARGIN:
			this.from = new Vector2(this.ltRect.margin.x, this.ltRect.margin.y);
			goto IL_0B73;
		case TweenAction.GUI_SCALE:
			this.from = new Vector3(this.ltRect.rect.width, this.ltRect.rect.height, 0f);
			goto IL_0B73;
		case TweenAction.GUI_ALPHA:
			this.from.x = this.ltRect.alpha;
			goto IL_0B73;
		case TweenAction.GUI_ROTATE:
			if (!this.ltRect.rotateEnabled)
			{
				this.ltRect.rotateEnabled = true;
				this.ltRect.resetForRotation();
			}
			this.from.x = this.ltRect.rotation;
			goto IL_0B73;
		case TweenAction.CANVAS_MOVE:
			this.from = this.rectTransform.anchoredPosition3D;
			goto IL_0B73;
		case TweenAction.CANVAS_SCALE:
			this.from = this.rectTransform.localScale;
			goto IL_0B73;
		case TweenAction.LAYOUT_MOVE:
			this.from = this.controlLayout.Offset;
			goto IL_0B73;
		default:
			goto IL_0B73;
		}
		this.from = this.trans.localPosition;
		goto IL_0B73;
		IL_0979:
		this.uiImage = this.trans.gameObject.GetComponent<Image>();
		if (this.uiImage != null)
		{
			this.from.x = this.uiImage.color.a;
		}
		IL_0B73:
		if (this.type != TweenAction.CALLBACK_COLOR && this.type != TweenAction.COLOR && this.type != TweenAction.TEXT_COLOR && this.type != TweenAction.CANVAS_COLOR)
		{
			this.diff = this.to - this.from;
			if (this.to.x != -150f)
			{
				float x = this.to.x;
			}
			if (this.from.x != -150f)
			{
				float x2 = this.from.x;
			}
		}
		if (this.onCompleteOnStart)
		{
			if (this.onComplete != null)
			{
				this.onComplete();
				return;
			}
			if (this.onCompleteObject != null)
			{
				this.onCompleteObject(this.onCompleteParam);
			}
		}
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x0003E344 File Offset: 0x0003C544
	public LTDescr setFromColor(Color col)
	{
		this.from = new Vector3(0f, col.a, 0f);
		this.diff = new Vector3(1f, 0f, 0f);
		this.axis = new Vector3(col.r, col.g, col.b);
		return this;
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x0003E3A4 File Offset: 0x0003C5A4
	public LTDescr pause()
	{
		if (this.direction != 0f)
		{
			this.directionLast = this.direction;
			this.direction = 0f;
		}
		return this;
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x0003E3CB File Offset: 0x0003C5CB
	public LTDescr resume()
	{
		this.direction = this.directionLast;
		return this;
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x0003E3DA File Offset: 0x0003C5DA
	public LTDescr setAxis(Vector3 axis)
	{
		this.axis = axis;
		return this;
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x0003E3E4 File Offset: 0x0003C5E4
	public LTDescr setDelay(float delay)
	{
		if (this.useEstimatedTime)
		{
			this.delay = delay;
		}
		else
		{
			this.delay = delay;
		}
		return this;
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x0003E3FF File Offset: 0x0003C5FF
	public LTDescr setEase(LeanTweenType easeType)
	{
		this.tweenType = easeType;
		return this;
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x0003E409 File Offset: 0x0003C609
	public LTDescr setOvershoot(float overshoot)
	{
		this.overshoot = overshoot;
		return this;
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x0003E413 File Offset: 0x0003C613
	public LTDescr setPeriod(float period)
	{
		this.period = period;
		return this;
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x0003E41D File Offset: 0x0003C61D
	public LTDescr setEase(AnimationCurve easeCurve)
	{
		this.animationCurve = easeCurve;
		return this;
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x0003E427 File Offset: 0x0003C627
	public LTDescr setTo(Vector3 to)
	{
		if (this.hasInitiliazed)
		{
			this.to = to;
			this.diff = to - this.from;
		}
		else
		{
			this.to = to;
		}
		return this;
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x0003E454 File Offset: 0x0003C654
	public LTDescr setFrom(Vector3 from)
	{
		if (this.trans)
		{
			this.init();
		}
		this.from = from;
		this.diff = this.to - this.from;
		return this;
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x0003E488 File Offset: 0x0003C688
	public LTDescr setFrom(float from)
	{
		return this.setFrom(new Vector3(from, 0f, 0f));
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x0003E4A0 File Offset: 0x0003C6A0
	public LTDescr setDiff(Vector3 diff)
	{
		this.diff = diff;
		return this;
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x0003E4AA File Offset: 0x0003C6AA
	public LTDescr setHasInitialized(bool has)
	{
		this.hasInitiliazed = has;
		return this;
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x0003E4B4 File Offset: 0x0003C6B4
	public LTDescr setId(uint id)
	{
		this._id = id;
		this.counter = LTDescr.global_counter;
		return this;
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x0003E4C9 File Offset: 0x0003C6C9
	public LTDescr setTime(float time)
	{
		this.time = time;
		return this;
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0003E4D4 File Offset: 0x0003C6D4
	public LTDescr setRepeat(int repeat)
	{
		this.loopCount = repeat;
		if ((repeat > 1 && this.loopType == LeanTweenType.once) || (repeat < 0 && this.loopType == LeanTweenType.once))
		{
			this.loopType = LeanTweenType.clamp;
		}
		if (this.type == TweenAction.CALLBACK || this.type == TweenAction.CALLBACK_COLOR)
		{
			this.setOnCompleteOnRepeat(true);
		}
		return this;
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x0003E529 File Offset: 0x0003C729
	public LTDescr setLoopType(LeanTweenType loopType)
	{
		this.loopType = loopType;
		return this;
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0003E533 File Offset: 0x0003C733
	public LTDescr setUseEstimatedTime(bool useEstimatedTime)
	{
		this.useEstimatedTime = useEstimatedTime;
		return this;
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x0003E53D File Offset: 0x0003C73D
	public LTDescr setIgnoreTimeScale(bool useUnScaledTime)
	{
		this.useEstimatedTime = useUnScaledTime;
		return this;
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x0003E547 File Offset: 0x0003C747
	public LTDescr setUseFrames(bool useFrames)
	{
		this.useFrames = useFrames;
		return this;
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x0003E551 File Offset: 0x0003C751
	public LTDescr setUseManualTime(bool useManualTime)
	{
		this.useManualTime = useManualTime;
		return this;
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x0003E55B File Offset: 0x0003C75B
	public LTDescr setLoopCount(int loopCount)
	{
		this.loopCount = loopCount;
		return this;
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x0003E565 File Offset: 0x0003C765
	public LTDescr setLoopOnce()
	{
		this.loopType = LeanTweenType.once;
		return this;
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x0003E570 File Offset: 0x0003C770
	public LTDescr setLoopClamp()
	{
		this.loopType = LeanTweenType.clamp;
		if (this.loopCount == 0)
		{
			this.loopCount = -1;
		}
		return this;
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x0003E58A File Offset: 0x0003C78A
	public LTDescr setLoopClamp(int loops)
	{
		this.loopCount = loops;
		return this;
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x0003E594 File Offset: 0x0003C794
	public LTDescr setLoopPingPong()
	{
		this.loopType = LeanTweenType.pingPong;
		if (this.loopCount == 0)
		{
			this.loopCount = -1;
		}
		return this;
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x0003E5AE File Offset: 0x0003C7AE
	public LTDescr setLoopPingPong(int loops)
	{
		this.loopType = LeanTweenType.pingPong;
		this.loopCount = ((loops == -1) ? loops : (loops * 2));
		return this;
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x0003E5C9 File Offset: 0x0003C7C9
	public LTDescr setOnComplete(Action onComplete)
	{
		this.onComplete = onComplete;
		return this;
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x0003E5D3 File Offset: 0x0003C7D3
	public LTDescr setOnComplete(Action<object> onComplete)
	{
		this.onCompleteObject = onComplete;
		return this;
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x0003E5DD File Offset: 0x0003C7DD
	public LTDescr setOnComplete(Action<object> onComplete, object onCompleteParam)
	{
		this.onCompleteObject = onComplete;
		if (onCompleteParam != null)
		{
			this.onCompleteParam = onCompleteParam;
		}
		return this;
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x0003E5F1 File Offset: 0x0003C7F1
	public LTDescr setOnCompleteParam(object onCompleteParam)
	{
		this.onCompleteParam = onCompleteParam;
		return this;
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x0003E5FB File Offset: 0x0003C7FB
	public LTDescr setOnUpdate(Action<float> onUpdate)
	{
		this.onUpdateFloat = onUpdate;
		this.hasUpdateCallback = true;
		return this;
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x0003E60C File Offset: 0x0003C80C
	public LTDescr setOnUpdateRatio(Action<float, float> onUpdate)
	{
		this.onUpdateFloatRatio = onUpdate;
		this.hasUpdateCallback = true;
		return this;
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x0003E61D File Offset: 0x0003C81D
	public LTDescr setOnUpdateObject(Action<float, object> onUpdate)
	{
		this.onUpdateFloatObject = onUpdate;
		this.hasUpdateCallback = true;
		return this;
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0003E62E File Offset: 0x0003C82E
	public LTDescr setOnUpdateVector2(Action<Vector2> onUpdate)
	{
		this.onUpdateVector2 = onUpdate;
		this.hasUpdateCallback = true;
		return this;
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x0003E63F File Offset: 0x0003C83F
	public LTDescr setOnUpdateVector3(Action<Vector3> onUpdate)
	{
		this.onUpdateVector3 = onUpdate;
		this.hasUpdateCallback = true;
		return this;
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x0003E650 File Offset: 0x0003C850
	public LTDescr setOnUpdateColor(Action<Color> onUpdate)
	{
		this.onUpdateColor = onUpdate;
		this.hasUpdateCallback = true;
		return this;
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x0003E661 File Offset: 0x0003C861
	public LTDescr setOnUpdate(Action<Color> onUpdate)
	{
		this.onUpdateColor = onUpdate;
		this.hasUpdateCallback = true;
		return this;
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x0003E672 File Offset: 0x0003C872
	public LTDescr setOnUpdate(Action<float, object> onUpdate, object onUpdateParam = null)
	{
		this.onUpdateFloatObject = onUpdate;
		this.hasUpdateCallback = true;
		if (onUpdateParam != null)
		{
			this.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x0003E68D File Offset: 0x0003C88D
	public LTDescr setOnUpdate(Action<Vector3, object> onUpdate, object onUpdateParam = null)
	{
		this.onUpdateVector3Object = onUpdate;
		this.hasUpdateCallback = true;
		if (onUpdateParam != null)
		{
			this.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x0003E6A8 File Offset: 0x0003C8A8
	public LTDescr setOnUpdate(Action<Vector2> onUpdate, object onUpdateParam = null)
	{
		this.onUpdateVector2 = onUpdate;
		this.hasUpdateCallback = true;
		if (onUpdateParam != null)
		{
			this.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x0003E6C3 File Offset: 0x0003C8C3
	public LTDescr setOnUpdate(Action<Vector3> onUpdate, object onUpdateParam = null)
	{
		this.onUpdateVector3 = onUpdate;
		this.hasUpdateCallback = true;
		if (onUpdateParam != null)
		{
			this.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x0003E6DE File Offset: 0x0003C8DE
	public LTDescr setOnUpdateParam(object onUpdateParam)
	{
		this.onUpdateParam = onUpdateParam;
		return this;
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x0003E6E8 File Offset: 0x0003C8E8
	public LTDescr setOrientToPath(bool doesOrient)
	{
		if (this.type == TweenAction.MOVE_CURVED || this.type == TweenAction.MOVE_CURVED_LOCAL)
		{
			if (this.path == null)
			{
				this.path = new LTBezierPath();
			}
			this.path.orientToPath = doesOrient;
		}
		else
		{
			this.spline.orientToPath = doesOrient;
		}
		return this;
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x0003E735 File Offset: 0x0003C935
	public LTDescr setOrientToPath2d(bool doesOrient2d)
	{
		this.setOrientToPath(doesOrient2d);
		if (this.type == TweenAction.MOVE_CURVED || this.type == TweenAction.MOVE_CURVED_LOCAL)
		{
			this.path.orientToPath2d = doesOrient2d;
		}
		else
		{
			this.spline.orientToPath2d = doesOrient2d;
		}
		return this;
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x0003E76C File Offset: 0x0003C96C
	public LTDescr setRect(LTRect rect)
	{
		this.ltRect = rect;
		return this;
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x0003E776 File Offset: 0x0003C976
	public LTDescr setRect(Rect rect)
	{
		this.ltRect = new LTRect(rect);
		return this;
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x0003E785 File Offset: 0x0003C985
	public LTDescr setPath(LTBezierPath path)
	{
		this.path = path;
		return this;
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x0003E78F File Offset: 0x0003C98F
	public LTDescr setPoint(Vector3 point)
	{
		this.point = point;
		return this;
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x0003E799 File Offset: 0x0003C999
	public LTDescr setDestroyOnComplete(bool doesDestroy)
	{
		this.destroyOnComplete = doesDestroy;
		return this;
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x0003E7A3 File Offset: 0x0003C9A3
	public LTDescr setAudio(object audio)
	{
		this.onCompleteParam = audio;
		return this;
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x0003E7AD File Offset: 0x0003C9AD
	public LTDescr setOnCompleteOnRepeat(bool isOn)
	{
		this.onCompleteOnRepeat = isOn;
		return this;
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x0003E7B7 File Offset: 0x0003C9B7
	public LTDescr setOnCompleteOnStart(bool isOn)
	{
		this.onCompleteOnStart = isOn;
		return this;
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x0003E7C1 File Offset: 0x0003C9C1
	public LTDescr setRect(RectTransform rect)
	{
		this.rectTransform = rect;
		return this;
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x0003E7CB File Offset: 0x0003C9CB
	public LTDescr setControlLayout(ControlLayout controlLayout)
	{
		this.controlLayout = controlLayout;
		return this;
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x0003E7D5 File Offset: 0x0003C9D5
	public LTDescr setSprites(Sprite[] sprites)
	{
		this.sprites = sprites;
		return this;
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x0003E7DF File Offset: 0x0003C9DF
	public LTDescr setFrameRate(float frameRate)
	{
		this.time = (float)this.sprites.Length / frameRate;
		return this;
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x0003E7F3 File Offset: 0x0003C9F3
	public LTDescr setOnStart(Action onStart)
	{
		this.onStart = onStart;
		return this;
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x0003E800 File Offset: 0x0003CA00
	public LTDescr setDirection(float direction)
	{
		if (this.direction != -1f && this.direction != 1f)
		{
			Debug.LogWarning("You have passed an incorrect direction of '" + direction.ToString() + "', direction must be -1f or 1f");
			return this;
		}
		if (this.direction != direction)
		{
			if (this.path != null)
			{
				this.path = new LTBezierPath(LTUtility.reverse(this.path.pts));
			}
			else if (this.spline != null)
			{
				this.spline = new LTSpline(LTUtility.reverse(this.spline.pts));
			}
		}
		return this;
	}

	// Token: 0x040006AE RID: 1710
	public bool toggle;

	// Token: 0x040006AF RID: 1711
	public bool useEstimatedTime;

	// Token: 0x040006B0 RID: 1712
	public bool useFrames;

	// Token: 0x040006B1 RID: 1713
	public bool useManualTime;

	// Token: 0x040006B2 RID: 1714
	public bool hasInitiliazed;

	// Token: 0x040006B3 RID: 1715
	public bool hasPhysics;

	// Token: 0x040006B4 RID: 1716
	public bool onCompleteOnRepeat;

	// Token: 0x040006B5 RID: 1717
	public bool onCompleteOnStart;

	// Token: 0x040006B6 RID: 1718
	public float passed;

	// Token: 0x040006B7 RID: 1719
	public float delay;

	// Token: 0x040006B8 RID: 1720
	public float time;

	// Token: 0x040006B9 RID: 1721
	public float lastVal;

	// Token: 0x040006BA RID: 1722
	private uint _id;

	// Token: 0x040006BB RID: 1723
	public int loopCount;

	// Token: 0x040006BC RID: 1724
	public uint counter;

	// Token: 0x040006BD RID: 1725
	public float direction;

	// Token: 0x040006BE RID: 1726
	public float directionLast;

	// Token: 0x040006BF RID: 1727
	public float overshoot;

	// Token: 0x040006C0 RID: 1728
	public float period;

	// Token: 0x040006C1 RID: 1729
	public bool destroyOnComplete;

	// Token: 0x040006C2 RID: 1730
	public Transform trans;

	// Token: 0x040006C3 RID: 1731
	public LTRect ltRect;

	// Token: 0x040006C4 RID: 1732
	public Vector3 from;

	// Token: 0x040006C5 RID: 1733
	public Vector3 to;

	// Token: 0x040006C6 RID: 1734
	public Vector3 _diff;

	// Token: 0x040006C7 RID: 1735
	public Vector3 point;

	// Token: 0x040006C8 RID: 1736
	public Vector3 axis;

	// Token: 0x040006C9 RID: 1737
	public Quaternion origRotation;

	// Token: 0x040006CA RID: 1738
	public LTBezierPath path;

	// Token: 0x040006CB RID: 1739
	public LTSpline spline;

	// Token: 0x040006CC RID: 1740
	public TweenAction type;

	// Token: 0x040006CD RID: 1741
	public LeanTweenType tweenType;

	// Token: 0x040006CE RID: 1742
	public AnimationCurve animationCurve;

	// Token: 0x040006CF RID: 1743
	public LeanTweenType loopType;

	// Token: 0x040006D0 RID: 1744
	public bool hasUpdateCallback;

	// Token: 0x040006D1 RID: 1745
	public Action<float> onUpdateFloat;

	// Token: 0x040006D2 RID: 1746
	public Action<float, float> onUpdateFloatRatio;

	// Token: 0x040006D3 RID: 1747
	public Action<float, object> onUpdateFloatObject;

	// Token: 0x040006D4 RID: 1748
	public Action<Vector2> onUpdateVector2;

	// Token: 0x040006D5 RID: 1749
	public Action<Vector3> onUpdateVector3;

	// Token: 0x040006D6 RID: 1750
	public Action<Vector3, object> onUpdateVector3Object;

	// Token: 0x040006D7 RID: 1751
	public Action<Color> onUpdateColor;

	// Token: 0x040006D8 RID: 1752
	public Action onComplete;

	// Token: 0x040006D9 RID: 1753
	public Action<object> onCompleteObject;

	// Token: 0x040006DA RID: 1754
	public object onCompleteParam;

	// Token: 0x040006DB RID: 1755
	public object onUpdateParam;

	// Token: 0x040006DC RID: 1756
	public Action onStart;

	// Token: 0x040006DD RID: 1757
	public RectTransform rectTransform;

	// Token: 0x040006DE RID: 1758
	public ControlLayout controlLayout;

	// Token: 0x040006DF RID: 1759
	public Text uiText;

	// Token: 0x040006E0 RID: 1760
	public Image uiImage;

	// Token: 0x040006E1 RID: 1761
	public Sprite[] sprites;

	// Token: 0x040006E2 RID: 1762
	private static uint global_counter;
}
