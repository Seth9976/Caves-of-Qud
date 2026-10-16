using System;
using System.Collections;
using System.Collections.Generic;
using QupKit;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200010B RID: 267
public class LeanTween : MonoBehaviour
{
	// Token: 0x060009ED RID: 2541 RVA: 0x0003E8E6 File Offset: 0x0003CAE6
	public static void init()
	{
		LeanTween.init(LeanTween.maxTweens);
	}

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x060009EE RID: 2542 RVA: 0x0003E8F2 File Offset: 0x0003CAF2
	public static int maxSearch
	{
		get
		{
			return LeanTween.tweenMaxSearch;
		}
	}

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x060009EF RID: 2543 RVA: 0x0003E8FC File Offset: 0x0003CAFC
	public static int tweensRunning
	{
		get
		{
			int num = 0;
			for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
			{
				if (LeanTween.tweens[i].toggle)
				{
					num++;
				}
			}
			return num;
		}
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x0003E930 File Offset: 0x0003CB30
	public static void init(int maxSimultaneousTweens)
	{
		if (LeanTween.tweens == null)
		{
			LeanTween.maxTweens = maxSimultaneousTweens;
			LeanTween.tweens = new LTDescr[LeanTween.maxTweens];
			LeanTween.tweensFinished = new int[LeanTween.maxTweens];
			LeanTween._tweenEmpty = new GameObject();
			LeanTween._tweenEmpty.name = "~LeanTween";
			LeanTween._tweenEmpty.AddComponent(typeof(LeanTween));
			LeanTween._tweenEmpty.isStatic = true;
			LeanTween._tweenEmpty.hideFlags = HideFlags.HideAndDontSave;
			global::UnityEngine.Object.DontDestroyOnLoad(LeanTween._tweenEmpty);
			for (int i = 0; i < LeanTween.maxTweens; i++)
			{
				LeanTween.tweens[i] = new LTDescr();
			}
			SceneManager.sceneLoaded += LeanTween.SceneManager_sceneLoaded;
		}
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x0003E9E7 File Offset: 0x0003CBE7
	private static void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		LTGUI.reset();
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x0003E9F0 File Offset: 0x0003CBF0
	public static void reset()
	{
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			LeanTween.tweens[i].toggle = false;
		}
		LeanTween.tweens = null;
		global::UnityEngine.Object.Destroy(LeanTween._tweenEmpty);
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x0003EA2A File Offset: 0x0003CC2A
	public void Update()
	{
		LeanTween.update(false, -1);
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x0003EA34 File Offset: 0x0003CC34
	public static void update(bool bForceFinish = false, int onlyI = -1)
	{
		if (bForceFinish || LeanTween.frameRendered != Time.frameCount)
		{
			LeanTween.init();
			if (LeanTween.dtEstimated < 0f)
			{
				LeanTween.dtEstimated = 0f;
			}
			else
			{
				LeanTween.dtEstimated = Time.unscaledDeltaTime;
			}
			LeanTween.dtActual = Time.deltaTime;
			LeanTween.maxTweenReached = 0;
			LeanTween.finishedCnt = 0;
			int num = 0;
			while (num <= LeanTween.tweenMaxSearch && num < LeanTween.maxTweens)
			{
				if ((onlyI == -1 || num == onlyI) && LeanTween.tweens[num].toggle)
				{
					LeanTween.maxTweenReached = num;
					LeanTween.tween = LeanTween.tweens[num];
					LeanTween.trans = LeanTween.tween.trans;
					LeanTween.timeTotal = LeanTween.tween.time;
					LeanTween.tweenAction = LeanTween.tween.type;
					if (LeanTween.tween.useEstimatedTime)
					{
						LeanTween.dt = LeanTween.dtEstimated;
					}
					else if (LeanTween.tween.useFrames)
					{
						LeanTween.dt = 1f;
					}
					else if (LeanTween.tween.useManualTime)
					{
						LeanTween.dt = LeanTween.dtManual;
					}
					else if (LeanTween.tween.direction == 0f)
					{
						LeanTween.dt = 0f;
					}
					else
					{
						LeanTween.dt = LeanTween.dtActual;
					}
					if (LeanTween.trans == null && LeanTween.tween.controlLayout == null)
					{
						LeanTween.removeTween(num);
					}
					else
					{
						if (bForceFinish)
						{
							LeanTween.tween.delay = 0f;
						}
						LeanTween.isTweenFinished = false;
						if (LeanTween.tween.delay <= 0f)
						{
							if (LeanTween.tween.passed + LeanTween.dt >= LeanTween.tween.time && LeanTween.tween.direction > 0f)
							{
								LeanTween.isTweenFinished = true;
								LeanTween.tween.passed = LeanTween.tween.time;
							}
							else if (LeanTween.tween.direction < 0f && LeanTween.tween.passed - LeanTween.dt <= 0f)
							{
								LeanTween.isTweenFinished = true;
								LeanTween.tween.passed = Mathf.Epsilon;
							}
						}
						if (!LeanTween.tween.hasInitiliazed && (((double)LeanTween.tween.passed == 0.0 && (double)LeanTween.tween.delay == 0.0) || (double)LeanTween.tween.passed > 0.0))
						{
							LeanTween.tween.init();
						}
						if (LeanTween.tween.delay <= 0f)
						{
							if (LeanTween.timeTotal <= 0f)
							{
								LeanTween.ratioPassed = 1f;
							}
							else
							{
								LeanTween.ratioPassed = LeanTween.tween.passed / LeanTween.timeTotal;
							}
							if (LeanTween.ratioPassed > 1f)
							{
								LeanTween.ratioPassed = 1f;
							}
							else if (LeanTween.ratioPassed < 0f)
							{
								LeanTween.ratioPassed = 0f;
							}
							if (LeanTween.tweenAction >= TweenAction.MOVE_X && LeanTween.tweenAction < TweenAction.MOVE)
							{
								if (LeanTween.tween.animationCurve != null)
								{
									LeanTween.val = LeanTween.tweenOnCurve(LeanTween.tween, LeanTween.ratioPassed);
								}
								else
								{
									switch (LeanTween.tween.tweenType)
									{
									case LeanTweenType.linear:
										LeanTween.val = LeanTween.tween.from.x + LeanTween.tween.diff.x * LeanTween.ratioPassed;
										break;
									case LeanTweenType.easeOutQuad:
										LeanTween.val = LeanTween.easeOutQuadOpt(LeanTween.tween.from.x, LeanTween.tween.diff.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeInQuad:
										LeanTween.val = LeanTween.easeInQuadOpt(LeanTween.tween.from.x, LeanTween.tween.diff.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeInOutQuad:
										LeanTween.val = LeanTween.easeInOutQuadOpt(LeanTween.tween.from.x, LeanTween.tween.diff.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeInCubic:
										LeanTween.val = LeanTween.easeInCubic(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeOutCubic:
										LeanTween.val = LeanTween.easeOutCubic(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeInOutCubic:
										LeanTween.val = LeanTween.easeInOutCubic(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeInQuart:
										LeanTween.val = LeanTween.easeInQuart(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeOutQuart:
										LeanTween.val = LeanTween.easeOutQuart(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeInOutQuart:
										LeanTween.val = LeanTween.easeInOutQuart(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeInQuint:
										LeanTween.val = LeanTween.easeInQuint(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeOutQuint:
										LeanTween.val = LeanTween.easeOutQuint(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeInOutQuint:
										LeanTween.val = LeanTween.easeInOutQuint(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeInSine:
										LeanTween.val = LeanTween.easeInSine(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeOutSine:
										LeanTween.val = LeanTween.easeOutSine(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeInOutSine:
										LeanTween.val = LeanTween.easeInOutSine(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeInExpo:
										LeanTween.val = LeanTween.easeInExpo(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeOutExpo:
										LeanTween.val = LeanTween.easeOutExpo(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeInOutExpo:
										LeanTween.val = LeanTween.easeInOutExpo(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeInCirc:
										LeanTween.val = LeanTween.easeInCirc(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeOutCirc:
										LeanTween.val = LeanTween.easeOutCirc(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeInOutCirc:
										LeanTween.val = LeanTween.easeInOutCirc(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeInBounce:
										LeanTween.val = LeanTween.easeInBounce(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeOutBounce:
										LeanTween.val = LeanTween.easeOutBounce(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeInOutBounce:
										LeanTween.val = LeanTween.easeInOutBounce(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeInBack:
										LeanTween.val = LeanTween.easeInBack(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed, LeanTween.tween.overshoot);
										break;
									case LeanTweenType.easeOutBack:
										LeanTween.val = LeanTween.easeOutBack(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed, LeanTween.tween.overshoot);
										break;
									case LeanTweenType.easeInOutBack:
										LeanTween.val = LeanTween.easeInOutBack(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed, LeanTween.tween.overshoot);
										break;
									case LeanTweenType.easeInElastic:
										LeanTween.val = LeanTween.easeInElastic(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed, LeanTween.tween.overshoot, LeanTween.tween.period);
										break;
									case LeanTweenType.easeOutElastic:
										LeanTween.val = LeanTween.easeOutElastic(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed, LeanTween.tween.overshoot, LeanTween.tween.period);
										break;
									case LeanTweenType.easeInOutElastic:
										LeanTween.val = LeanTween.easeInOutElastic(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed, LeanTween.tween.overshoot, LeanTween.tween.period);
										break;
									case LeanTweenType.easeSpring:
										LeanTween.val = LeanTween.spring(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed);
										break;
									case LeanTweenType.easeShake:
									case LeanTweenType.punch:
										if (LeanTween.tween.tweenType == LeanTweenType.punch)
										{
											LeanTween.tween.animationCurve = LeanTween.punch;
										}
										else if (LeanTween.tween.tweenType == LeanTweenType.easeShake)
										{
											LeanTween.tween.animationCurve = LeanTween.shake;
										}
										LeanTween.tween.to.x = LeanTween.tween.from.x + LeanTween.tween.to.x;
										LeanTween.tween.diff = new Vector3(LeanTween.tween.to.x - LeanTween.tween.from.x, 0f, 0f);
										LeanTween.val = LeanTween.tweenOnCurve(LeanTween.tween, LeanTween.ratioPassed);
										break;
									default:
										LeanTween.val = LeanTween.tween.from.x + LeanTween.tween.diff.x * LeanTween.ratioPassed;
										break;
									}
								}
								if (LeanTween.tweenAction == TweenAction.MOVE_X)
								{
									LeanTween.trans.position = new Vector3(LeanTween.val, LeanTween.trans.position.y, LeanTween.trans.position.z);
								}
								else if (LeanTween.tweenAction == TweenAction.MOVE_Y)
								{
									LeanTween.trans.position = new Vector3(LeanTween.trans.position.x, LeanTween.val, LeanTween.trans.position.z);
								}
								else if (LeanTween.tweenAction == TweenAction.MOVE_Z)
								{
									LeanTween.trans.position = new Vector3(LeanTween.trans.position.x, LeanTween.trans.position.y, LeanTween.val);
								}
								if (LeanTween.tweenAction == TweenAction.MOVE_LOCAL_X)
								{
									LeanTween.trans.localPosition = new Vector3(LeanTween.val, LeanTween.trans.localPosition.y, LeanTween.trans.localPosition.z);
								}
								else if (LeanTween.tweenAction == TweenAction.MOVE_LOCAL_Y)
								{
									LeanTween.trans.localPosition = new Vector3(LeanTween.trans.localPosition.x, LeanTween.val, LeanTween.trans.localPosition.z);
								}
								else if (LeanTween.tweenAction == TweenAction.MOVE_LOCAL_Z)
								{
									LeanTween.trans.localPosition = new Vector3(LeanTween.trans.localPosition.x, LeanTween.trans.localPosition.y, LeanTween.val);
								}
								else if (LeanTween.tweenAction == TweenAction.MOVE_CURVED)
								{
									if (LeanTween.tween.path.orientToPath)
									{
										if (LeanTween.tween.path.orientToPath2d)
										{
											LeanTween.tween.path.place2d(LeanTween.trans, LeanTween.val);
										}
										else
										{
											LeanTween.tween.path.place(LeanTween.trans, LeanTween.val);
										}
									}
									else
									{
										LeanTween.trans.position = LeanTween.tween.path.point(LeanTween.val);
									}
								}
								else if (LeanTween.tweenAction == TweenAction.MOVE_CURVED_LOCAL)
								{
									if (LeanTween.tween.path.orientToPath)
									{
										if (LeanTween.tween.path.orientToPath2d)
										{
											LeanTween.tween.path.placeLocal2d(LeanTween.trans, LeanTween.val);
										}
										else
										{
											LeanTween.tween.path.placeLocal(LeanTween.trans, LeanTween.val);
										}
									}
									else
									{
										LeanTween.trans.localPosition = LeanTween.tween.path.point(LeanTween.val);
									}
								}
								else if (LeanTween.tweenAction == TweenAction.MOVE_SPLINE)
								{
									if (LeanTween.tween.spline.orientToPath)
									{
										if (LeanTween.tween.spline.orientToPath2d)
										{
											LeanTween.tween.spline.place2d(LeanTween.trans, LeanTween.val);
										}
										else
										{
											LeanTween.tween.spline.place(LeanTween.trans, LeanTween.val);
										}
									}
									else
									{
										LeanTween.trans.position = LeanTween.tween.spline.point(LeanTween.val);
									}
								}
								else if (LeanTween.tweenAction == TweenAction.MOVE_SPLINE_LOCAL)
								{
									if (LeanTween.tween.spline.orientToPath)
									{
										if (LeanTween.tween.spline.orientToPath2d)
										{
											LeanTween.tween.spline.placeLocal2d(LeanTween.trans, LeanTween.val);
										}
										else
										{
											LeanTween.tween.spline.placeLocal(LeanTween.trans, LeanTween.val);
										}
									}
									else
									{
										LeanTween.trans.localPosition = LeanTween.tween.spline.point(LeanTween.val);
									}
								}
								else if (LeanTween.tweenAction == TweenAction.SCALE_X)
								{
									LeanTween.trans.localScale = new Vector3(LeanTween.val, LeanTween.trans.localScale.y, LeanTween.trans.localScale.z);
								}
								else if (LeanTween.tweenAction == TweenAction.SCALE_Y)
								{
									LeanTween.trans.localScale = new Vector3(LeanTween.trans.localScale.x, LeanTween.val, LeanTween.trans.localScale.z);
								}
								else if (LeanTween.tweenAction == TweenAction.SCALE_Z)
								{
									LeanTween.trans.localScale = new Vector3(LeanTween.trans.localScale.x, LeanTween.trans.localScale.y, LeanTween.val);
								}
								else if (LeanTween.tweenAction == TweenAction.ROTATE_X)
								{
									LeanTween.trans.eulerAngles = new Vector3(LeanTween.val, LeanTween.trans.eulerAngles.y, LeanTween.trans.eulerAngles.z);
								}
								else if (LeanTween.tweenAction == TweenAction.ROTATE_Y)
								{
									LeanTween.trans.eulerAngles = new Vector3(LeanTween.trans.eulerAngles.x, LeanTween.val, LeanTween.trans.eulerAngles.z);
								}
								else if (LeanTween.tweenAction == TweenAction.ROTATE_Z)
								{
									LeanTween.trans.eulerAngles = new Vector3(LeanTween.trans.eulerAngles.x, LeanTween.trans.eulerAngles.y, LeanTween.val);
								}
								else if (LeanTween.tweenAction == TweenAction.ROTATE_AROUND)
								{
									Vector3 localPosition = LeanTween.trans.localPosition;
									Vector3 vector = LeanTween.trans.TransformPoint(LeanTween.tween.point);
									LeanTween.trans.RotateAround(vector, LeanTween.tween.axis, -LeanTween.val);
									Vector3 vector2 = localPosition - LeanTween.trans.localPosition;
									LeanTween.trans.localPosition = localPosition - vector2;
									LeanTween.trans.rotation = LeanTween.tween.origRotation;
									vector = LeanTween.trans.TransformPoint(LeanTween.tween.point);
									LeanTween.trans.RotateAround(vector, LeanTween.tween.axis, LeanTween.val);
								}
								else if (LeanTween.tweenAction == TweenAction.ROTATE_AROUND_LOCAL)
								{
									Vector3 localPosition2 = LeanTween.trans.localPosition;
									LeanTween.trans.RotateAround(LeanTween.trans.TransformPoint(LeanTween.tween.point), LeanTween.trans.TransformDirection(LeanTween.tween.axis), -LeanTween.val);
									Vector3 vector3 = localPosition2 - LeanTween.trans.localPosition;
									LeanTween.trans.localPosition = localPosition2 - vector3;
									LeanTween.trans.localRotation = LeanTween.tween.origRotation;
									Vector3 vector4 = LeanTween.trans.TransformPoint(LeanTween.tween.point);
									LeanTween.trans.RotateAround(vector4, LeanTween.trans.TransformDirection(LeanTween.tween.axis), LeanTween.val);
								}
								else
								{
									if (LeanTween.tweenAction == TweenAction.ALPHA)
									{
										SpriteRenderer component = LeanTween.trans.gameObject.GetComponent<SpriteRenderer>();
										if (component != null)
										{
											component.color = new Color(component.color.r, component.color.g, component.color.b, LeanTween.val);
											goto IL_30D3;
										}
										if (LeanTween.trans.gameObject.GetComponent<Image>() != null)
										{
											Color color = LeanTween.trans.gameObject.GetComponent<Image>().color;
											LeanTween.trans.gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, LeanTween.val);
										}
										else if (LeanTween.trans.gameObject.GetComponent<CanvasGroup>() != null)
										{
											LeanTween.trans.gameObject.GetComponent<CanvasGroup>().alpha = LeanTween.val;
										}
										else if (LeanTween.trans.gameObject.GetComponent<Renderer>() != null)
										{
											foreach (Material material in LeanTween.trans.gameObject.GetComponent<Renderer>().materials)
											{
												if (material.HasProperty("_Color"))
												{
													material.color = new Color(material.color.r, material.color.g, material.color.b, LeanTween.val);
												}
												else if (material.HasProperty("_TintColor"))
												{
													Color color2 = material.GetColor("_TintColor");
													material.SetColor("_TintColor", new Color(color2.r, color2.g, color2.b, LeanTween.val));
												}
											}
										}
										if (LeanTween.trans.childCount <= 0)
										{
											goto IL_30D3;
										}
										using (IEnumerator enumerator = LeanTween.trans.GetEnumerator())
										{
											while (enumerator.MoveNext())
											{
												object obj = enumerator.Current;
												Transform transform = (Transform)obj;
												if (transform.gameObject.GetComponent<Renderer>() != null)
												{
													foreach (Material material2 in transform.gameObject.GetComponent<Renderer>().materials)
													{
														material2.color = new Color(material2.color.r, material2.color.g, material2.color.b, LeanTween.val);
													}
												}
											}
											goto IL_30D3;
										}
									}
									if (LeanTween.tweenAction == TweenAction.ALPHA_VERTEX)
									{
										Mesh mesh = LeanTween.trans.GetComponent<MeshFilter>().mesh;
										Vector3[] vertices = mesh.vertices;
										Color32[] array2 = new Color32[vertices.Length];
										Color32 color3 = mesh.colors32[0];
										color3 = new Color((float)color3.r, (float)color3.g, (float)color3.b, LeanTween.val);
										for (int j = 0; j < vertices.Length; j++)
										{
											array2[j] = color3;
										}
										mesh.colors32 = array2;
									}
									else if (LeanTween.tweenAction == TweenAction.COLOR || LeanTween.tweenAction == TweenAction.CALLBACK_COLOR)
									{
										Color color4 = LeanTween.tweenColor(LeanTween.tween, LeanTween.val);
										SpriteRenderer component2 = LeanTween.trans.gameObject.GetComponent<SpriteRenderer>();
										if (component2 != null)
										{
											component2.color = color4;
										}
										else if (LeanTween.tweenAction == TweenAction.COLOR)
										{
											if (LeanTween.trans.gameObject.GetComponent<Renderer>() != null)
											{
												Material[] array = LeanTween.trans.gameObject.GetComponent<Renderer>().materials;
												for (int i = 0; i < array.Length; i++)
												{
													array[i].color = color4;
												}
											}
											if (LeanTween.trans.childCount > 0)
											{
												foreach (object obj2 in LeanTween.trans)
												{
													Transform transform2 = (Transform)obj2;
													if (transform2.gameObject.GetComponent<Renderer>() != null)
													{
														Material[] array = transform2.gameObject.GetComponent<Renderer>().materials;
														for (int i = 0; i < array.Length; i++)
														{
															array[i].color = color4;
														}
													}
												}
											}
										}
										if (LeanTween.dt != 0f && LeanTween.tween.onUpdateColor != null)
										{
											LeanTween.tween.onUpdateColor(color4);
										}
									}
									else if (LeanTween.tweenAction == TweenAction.CANVAS_ALPHA)
									{
										Color color5 = LeanTween.tween.uiImage.color;
										color5.a = LeanTween.val;
										LeanTween.tween.uiImage.color = color5;
									}
									else if (LeanTween.tweenAction == TweenAction.CANVAS_COLOR)
									{
										Color color6 = LeanTween.tweenColor(LeanTween.tween, LeanTween.val);
										LeanTween.tween.uiImage.color = color6;
										if (LeanTween.dt != 0f && LeanTween.tween.onUpdateColor != null)
										{
											LeanTween.tween.onUpdateColor(color6);
										}
									}
									else if (LeanTween.tweenAction == TweenAction.TEXT_ALPHA)
									{
										LeanTween.textAlphaRecursive(LeanTween.trans, LeanTween.val);
									}
									else
									{
										if (LeanTween.tweenAction == TweenAction.TEXT_COLOR)
										{
											Color color7 = LeanTween.tweenColor(LeanTween.tween, LeanTween.val);
											LeanTween.tween.uiText.color = color7;
											if (LeanTween.dt != 0f && LeanTween.tween.onUpdateColor != null)
											{
												LeanTween.tween.onUpdateColor(color7);
											}
											if (LeanTween.trans.childCount <= 0)
											{
												goto IL_30D3;
											}
											using (IEnumerator enumerator = LeanTween.trans.GetEnumerator())
											{
												while (enumerator.MoveNext())
												{
													object obj3 = enumerator.Current;
													Text component3 = ((Transform)obj3).gameObject.GetComponent<Text>();
													if (component3 != null)
													{
														component3.color = color7;
													}
												}
												goto IL_30D3;
											}
										}
										if (LeanTween.tweenAction == TweenAction.CANVAS_ROTATEAROUND)
										{
											RectTransform rectTransform = LeanTween.tween.rectTransform;
											Vector3 localPosition3 = rectTransform.localPosition;
											rectTransform.RotateAround(rectTransform.TransformPoint(LeanTween.tween.point), LeanTween.tween.axis, -LeanTween.val);
											Vector3 vector5 = localPosition3 - rectTransform.localPosition;
											rectTransform.localPosition = localPosition3 - vector5;
											rectTransform.rotation = LeanTween.tween.origRotation;
											rectTransform.RotateAround(rectTransform.TransformPoint(LeanTween.tween.point), LeanTween.tween.axis, LeanTween.val);
										}
										else if (LeanTween.tweenAction == TweenAction.CANVAS_ROTATEAROUND_LOCAL)
										{
											RectTransform rectTransform2 = LeanTween.tween.rectTransform;
											Vector3 localPosition4 = rectTransform2.localPosition;
											rectTransform2.RotateAround(rectTransform2.TransformPoint(LeanTween.tween.point), rectTransform2.TransformDirection(LeanTween.tween.axis), -LeanTween.val);
											Vector3 vector6 = localPosition4 - rectTransform2.localPosition;
											rectTransform2.localPosition = localPosition4 - vector6;
											rectTransform2.rotation = LeanTween.tween.origRotation;
											rectTransform2.RotateAround(rectTransform2.TransformPoint(LeanTween.tween.point), rectTransform2.TransformDirection(LeanTween.tween.axis), LeanTween.val);
										}
										else if (LeanTween.tweenAction == TweenAction.CANVAS_PLAYSPRITE)
										{
											int num2 = (int)Mathf.Round(LeanTween.val);
											LeanTween.tween.uiImage.sprite = LeanTween.tween.sprites[num2];
										}
										else if (LeanTween.tweenAction == TweenAction.CANVAS_MOVE_X)
										{
											Vector3 anchoredPosition3D = LeanTween.tween.rectTransform.anchoredPosition3D;
											LeanTween.tween.rectTransform.anchoredPosition3D = new Vector3(LeanTween.val, anchoredPosition3D.y, anchoredPosition3D.z);
										}
										else if (LeanTween.tweenAction == TweenAction.CANVAS_MOVE_Y)
										{
											Vector3 anchoredPosition3D2 = LeanTween.tween.rectTransform.anchoredPosition3D;
											LeanTween.tween.rectTransform.anchoredPosition3D = new Vector3(anchoredPosition3D2.x, LeanTween.val, anchoredPosition3D2.z);
										}
										else if (LeanTween.tweenAction == TweenAction.CANVAS_MOVE_Z)
										{
											Vector3 anchoredPosition3D3 = LeanTween.tween.rectTransform.anchoredPosition3D;
											LeanTween.tween.rectTransform.anchoredPosition3D = new Vector3(anchoredPosition3D3.x, anchoredPosition3D3.y, LeanTween.val);
										}
									}
								}
							}
							else if (LeanTween.tweenAction >= TweenAction.MOVE)
							{
								if (LeanTween.tween.animationCurve != null)
								{
									LeanTween.newVect = LeanTween.tweenOnCurveVector(LeanTween.tween, LeanTween.ratioPassed);
								}
								else if (LeanTween.tween.tweenType == LeanTweenType.linear)
								{
									LeanTween.newVect = new Vector3(LeanTween.tween.from.x + LeanTween.tween.diff.x * LeanTween.ratioPassed, LeanTween.tween.from.y + LeanTween.tween.diff.y * LeanTween.ratioPassed, LeanTween.tween.from.z + LeanTween.tween.diff.z * LeanTween.ratioPassed);
								}
								else if (LeanTween.tween.tweenType >= LeanTweenType.linear)
								{
									switch (LeanTween.tween.tweenType)
									{
									case LeanTweenType.easeOutQuad:
										LeanTween.newVect = new Vector3(LeanTween.easeOutQuadOpt(LeanTween.tween.from.x, LeanTween.tween.diff.x, LeanTween.ratioPassed), LeanTween.easeOutQuadOpt(LeanTween.tween.from.y, LeanTween.tween.diff.y, LeanTween.ratioPassed), LeanTween.easeOutQuadOpt(LeanTween.tween.from.z, LeanTween.tween.diff.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeInQuad:
										LeanTween.newVect = new Vector3(LeanTween.easeInQuadOpt(LeanTween.tween.from.x, LeanTween.tween.diff.x, LeanTween.ratioPassed), LeanTween.easeInQuadOpt(LeanTween.tween.from.y, LeanTween.tween.diff.y, LeanTween.ratioPassed), LeanTween.easeInQuadOpt(LeanTween.tween.from.z, LeanTween.tween.diff.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeInOutQuad:
										LeanTween.newVect = new Vector3(LeanTween.easeInOutQuadOpt(LeanTween.tween.from.x, LeanTween.tween.diff.x, LeanTween.ratioPassed), LeanTween.easeInOutQuadOpt(LeanTween.tween.from.y, LeanTween.tween.diff.y, LeanTween.ratioPassed), LeanTween.easeInOutQuadOpt(LeanTween.tween.from.z, LeanTween.tween.diff.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeInCubic:
										LeanTween.newVect = new Vector3(LeanTween.easeInCubic(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeInCubic(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeInCubic(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeOutCubic:
										LeanTween.newVect = new Vector3(LeanTween.easeOutCubic(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeOutCubic(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeOutCubic(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeInOutCubic:
										LeanTween.newVect = new Vector3(LeanTween.easeInOutCubic(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeInOutCubic(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeInOutCubic(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeInQuart:
										LeanTween.newVect = new Vector3(LeanTween.easeInQuart(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeInQuart(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeInQuart(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeOutQuart:
										LeanTween.newVect = new Vector3(LeanTween.easeOutQuart(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeOutQuart(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeOutQuart(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeInOutQuart:
										LeanTween.newVect = new Vector3(LeanTween.easeInOutQuart(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeInOutQuart(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeInOutQuart(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeInQuint:
										LeanTween.newVect = new Vector3(LeanTween.easeInQuint(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeInQuint(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeInQuint(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeOutQuint:
										LeanTween.newVect = new Vector3(LeanTween.easeOutQuint(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeOutQuint(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeOutQuint(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeInOutQuint:
										LeanTween.newVect = new Vector3(LeanTween.easeInOutQuint(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeInOutQuint(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeInOutQuint(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeInSine:
										LeanTween.newVect = new Vector3(LeanTween.easeInSine(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeInSine(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeInSine(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeOutSine:
										LeanTween.newVect = new Vector3(LeanTween.easeOutSine(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeOutSine(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeOutSine(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeInOutSine:
										LeanTween.newVect = new Vector3(LeanTween.easeInOutSine(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeInOutSine(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeInOutSine(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeInExpo:
										LeanTween.newVect = new Vector3(LeanTween.easeInExpo(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeInExpo(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeInExpo(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeOutExpo:
										LeanTween.newVect = new Vector3(LeanTween.easeOutExpo(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeOutExpo(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeOutExpo(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeInOutExpo:
										LeanTween.newVect = new Vector3(LeanTween.easeInOutExpo(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeInOutExpo(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeInOutExpo(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeInCirc:
										LeanTween.newVect = new Vector3(LeanTween.easeInCirc(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeInCirc(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeInCirc(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeOutCirc:
										LeanTween.newVect = new Vector3(LeanTween.easeOutCirc(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeOutCirc(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeOutCirc(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeInOutCirc:
										LeanTween.newVect = new Vector3(LeanTween.easeInOutCirc(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeInOutCirc(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeInOutCirc(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeInBounce:
										LeanTween.newVect = new Vector3(LeanTween.easeInBounce(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeInBounce(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeInBounce(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeOutBounce:
										LeanTween.newVect = new Vector3(LeanTween.easeOutBounce(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeOutBounce(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeOutBounce(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeInOutBounce:
										LeanTween.newVect = new Vector3(LeanTween.easeInOutBounce(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.easeInOutBounce(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.easeInOutBounce(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeInBack:
										LeanTween.newVect = new Vector3(LeanTween.easeInBack(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed, 1f), LeanTween.easeInBack(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed, 1f), LeanTween.easeInBack(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed, 1f));
										break;
									case LeanTweenType.easeOutBack:
										LeanTween.newVect = new Vector3(LeanTween.easeOutBack(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed, LeanTween.tween.overshoot), LeanTween.easeOutBack(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed, LeanTween.tween.overshoot), LeanTween.easeOutBack(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed, LeanTween.tween.overshoot));
										break;
									case LeanTweenType.easeInOutBack:
										LeanTween.newVect = new Vector3(LeanTween.easeInOutBack(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed, LeanTween.tween.overshoot), LeanTween.easeInOutBack(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed, LeanTween.tween.overshoot), LeanTween.easeInOutBack(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed, LeanTween.tween.overshoot));
										break;
									case LeanTweenType.easeInElastic:
										LeanTween.newVect = new Vector3(LeanTween.easeInElastic(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed, LeanTween.tween.overshoot, LeanTween.tween.period), LeanTween.easeInElastic(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed, LeanTween.tween.overshoot, LeanTween.tween.period), LeanTween.easeInElastic(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed, LeanTween.tween.overshoot, LeanTween.tween.period));
										break;
									case LeanTweenType.easeOutElastic:
										LeanTween.newVect = new Vector3(LeanTween.easeOutElastic(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed, LeanTween.tween.overshoot, LeanTween.tween.period), LeanTween.easeOutElastic(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed, LeanTween.tween.overshoot, LeanTween.tween.period), LeanTween.easeOutElastic(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed, LeanTween.tween.overshoot, LeanTween.tween.period));
										break;
									case LeanTweenType.easeInOutElastic:
										LeanTween.newVect = new Vector3(LeanTween.easeInOutElastic(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed, LeanTween.tween.overshoot, LeanTween.tween.period), LeanTween.easeInOutElastic(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed, LeanTween.tween.overshoot, LeanTween.tween.period), LeanTween.easeInOutElastic(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed, LeanTween.tween.overshoot, LeanTween.tween.period));
										break;
									case LeanTweenType.easeSpring:
										LeanTween.newVect = new Vector3(LeanTween.spring(LeanTween.tween.from.x, LeanTween.tween.to.x, LeanTween.ratioPassed), LeanTween.spring(LeanTween.tween.from.y, LeanTween.tween.to.y, LeanTween.ratioPassed), LeanTween.spring(LeanTween.tween.from.z, LeanTween.tween.to.z, LeanTween.ratioPassed));
										break;
									case LeanTweenType.easeShake:
									case LeanTweenType.punch:
										if (LeanTween.tween.tweenType == LeanTweenType.punch)
										{
											LeanTween.tween.animationCurve = LeanTween.punch;
										}
										else if (LeanTween.tween.tweenType == LeanTweenType.easeShake)
										{
											LeanTween.tween.animationCurve = LeanTween.shake;
										}
										LeanTween.tween.to = LeanTween.tween.from + LeanTween.tween.to;
										LeanTween.tween.diff = LeanTween.tween.to - LeanTween.tween.from;
										if (LeanTween.tweenAction == TweenAction.ROTATE || LeanTween.tweenAction == TweenAction.ROTATE_LOCAL)
										{
											LeanTween.tween.to = new Vector3(LeanTween.closestRot(LeanTween.tween.from.x, LeanTween.tween.to.x), LeanTween.closestRot(LeanTween.tween.from.y, LeanTween.tween.to.y), LeanTween.closestRot(LeanTween.tween.from.z, LeanTween.tween.to.z));
										}
										LeanTween.newVect = LeanTween.tweenOnCurveVector(LeanTween.tween, LeanTween.ratioPassed);
										break;
									}
								}
								else
								{
									LeanTween.newVect = new Vector3(LeanTween.tween.from.x + LeanTween.tween.diff.x * LeanTween.ratioPassed, LeanTween.tween.from.y + LeanTween.tween.diff.y * LeanTween.ratioPassed, LeanTween.tween.from.z + LeanTween.tween.diff.z * LeanTween.ratioPassed);
								}
								if (LeanTween.tweenAction == TweenAction.LAYOUT_MOVE)
								{
									LeanTween.tween.controlLayout.Offset = LeanTween.newVect;
									LeanTween.tween.controlLayout.Apply();
								}
								else if (LeanTween.tweenAction == TweenAction.MOVE)
								{
									LeanTween.trans.position = LeanTween.newVect;
								}
								else if (LeanTween.tweenAction == TweenAction.MOVE_LOCAL)
								{
									LeanTween.trans.localPosition = LeanTween.newVect;
								}
								else if (LeanTween.tweenAction == TweenAction.ROTATE)
								{
									LeanTween.trans.eulerAngles = LeanTween.newVect;
								}
								else if (LeanTween.tweenAction == TweenAction.ROTATE_LOCAL)
								{
									LeanTween.trans.localEulerAngles = LeanTween.newVect;
								}
								else if (LeanTween.tweenAction == TweenAction.SCALE)
								{
									LeanTween.trans.localScale = LeanTween.newVect;
								}
								else if (LeanTween.tweenAction == TweenAction.GUI_MOVE)
								{
									LeanTween.tween.ltRect.rect = new Rect(LeanTween.newVect.x, LeanTween.newVect.y, LeanTween.tween.ltRect.rect.width, LeanTween.tween.ltRect.rect.height);
								}
								else if (LeanTween.tweenAction == TweenAction.GUI_MOVE_MARGIN)
								{
									LeanTween.tween.ltRect.margin = new Vector2(LeanTween.newVect.x, LeanTween.newVect.y);
								}
								else if (LeanTween.tweenAction == TweenAction.GUI_SCALE)
								{
									LeanTween.tween.ltRect.rect = new Rect(LeanTween.tween.ltRect.rect.x, LeanTween.tween.ltRect.rect.y, LeanTween.newVect.x, LeanTween.newVect.y);
								}
								else if (LeanTween.tweenAction == TweenAction.GUI_ALPHA)
								{
									LeanTween.tween.ltRect.alpha = LeanTween.newVect.x;
								}
								else if (LeanTween.tweenAction == TweenAction.GUI_ROTATE)
								{
									LeanTween.tween.ltRect.rotation = LeanTween.newVect.x;
								}
								else if (LeanTween.tweenAction == TweenAction.CANVAS_MOVE)
								{
									LeanTween.tween.rectTransform.anchoredPosition3D = LeanTween.newVect;
								}
								else if (LeanTween.tweenAction == TweenAction.CANVAS_SCALE)
								{
									LeanTween.tween.rectTransform.localScale = LeanTween.newVect;
								}
							}
							IL_30D3:
							if (LeanTween.dt != 0f && LeanTween.tween.hasUpdateCallback)
							{
								if (LeanTween.tween.onUpdateFloat != null)
								{
									LeanTween.tween.onUpdateFloat(LeanTween.val);
								}
								if (LeanTween.tween.onUpdateFloatRatio != null)
								{
									LeanTween.tween.onUpdateFloatRatio(LeanTween.val, LeanTween.ratioPassed);
								}
								else if (LeanTween.tween.onUpdateFloatObject != null)
								{
									LeanTween.tween.onUpdateFloatObject(LeanTween.val, LeanTween.tween.onUpdateParam);
								}
								else if (LeanTween.tween.onUpdateVector3Object != null)
								{
									LeanTween.tween.onUpdateVector3Object(LeanTween.newVect, LeanTween.tween.onUpdateParam);
								}
								else if (LeanTween.tween.onUpdateVector3 != null)
								{
									LeanTween.tween.onUpdateVector3(LeanTween.newVect);
								}
								else if (LeanTween.tween.onUpdateVector2 != null)
								{
									LeanTween.tween.onUpdateVector2(new Vector2(LeanTween.newVect.x, LeanTween.newVect.y));
								}
							}
						}
						if (LeanTween.isTweenFinished)
						{
							if (LeanTween.tween.loopType == LeanTweenType.once || LeanTween.tween.loopCount == 1)
							{
								LeanTween.tweensFinished[LeanTween.finishedCnt] = num;
								LeanTween.finishedCnt++;
								if (LeanTween.tweenAction == TweenAction.GUI_ROTATE)
								{
									LeanTween.tween.ltRect.rotateFinished = true;
								}
								if (LeanTween.tweenAction == TweenAction.DELAYED_SOUND)
								{
									AudioSource.PlayClipAtPoint((AudioClip)LeanTween.tween.onCompleteParam, LeanTween.tween.to, LeanTween.tween.from.x);
								}
							}
							else
							{
								if ((LeanTween.tween.loopCount < 0 && LeanTween.tween.type == TweenAction.CALLBACK) || LeanTween.tween.onCompleteOnRepeat)
								{
									if (LeanTween.tweenAction == TweenAction.DELAYED_SOUND)
									{
										AudioSource.PlayClipAtPoint((AudioClip)LeanTween.tween.onCompleteParam, LeanTween.tween.to, LeanTween.tween.from.x);
									}
									if (LeanTween.tween.onComplete != null)
									{
										LeanTween.tween.onComplete();
									}
									else if (LeanTween.tween.onCompleteObject != null)
									{
										LeanTween.tween.onCompleteObject(LeanTween.tween.onCompleteParam);
									}
								}
								if (LeanTween.tween.loopCount >= 1)
								{
									LeanTween.tween.loopCount--;
								}
								if (LeanTween.tween.loopType == LeanTweenType.pingPong)
								{
									LeanTween.tween.direction = 0f - LeanTween.tween.direction;
								}
								else
								{
									LeanTween.tween.passed = Mathf.Epsilon;
								}
							}
						}
						else if (LeanTween.tween.delay <= 0f)
						{
							LeanTween.tween.passed += LeanTween.dt * LeanTween.tween.direction;
						}
						else
						{
							LeanTween.tween.delay -= LeanTween.dt;
							if (LeanTween.tween.delay < 0f)
							{
								LeanTween.tween.passed = 0f;
								LeanTween.tween.delay = 0f;
							}
						}
					}
				}
				num++;
			}
			LeanTween.tweenMaxSearch = LeanTween.maxTweenReached;
			LeanTween.frameRendered = Time.frameCount;
			for (int k = 0; k < LeanTween.finishedCnt; k++)
			{
				LeanTween.j = LeanTween.tweensFinished[k];
				LeanTween.tween = LeanTween.tweens[LeanTween.j];
				if (LeanTween.tween.onComplete != null)
				{
					Action onComplete = LeanTween.tween.onComplete;
					LeanTween.removeTween(LeanTween.j);
					onComplete();
				}
				else if (LeanTween.tween.onCompleteObject != null)
				{
					Action<object> onCompleteObject = LeanTween.tween.onCompleteObject;
					object onCompleteParam = LeanTween.tween.onCompleteParam;
					LeanTween.removeTween(LeanTween.j);
					onCompleteObject(onCompleteParam);
				}
				else
				{
					LeanTween.removeTween(LeanTween.j);
				}
			}
		}
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x00041F28 File Offset: 0x00040128
	private static void textAlphaRecursive(Transform trans, float val)
	{
		Text component = trans.gameObject.GetComponent<Text>();
		if (component != null)
		{
			Color color = component.color;
			color.a = val;
			component.color = color;
		}
		if (trans.childCount > 0)
		{
			foreach (object obj in trans)
			{
				LeanTween.textAlphaRecursive((Transform)obj, val);
			}
		}
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x00041FB0 File Offset: 0x000401B0
	private static Color tweenColor(LTDescr tween, float val)
	{
		Vector3 vector = tween.point - tween.axis;
		float num = tween.to.y - tween.from.y;
		return new Color(tween.axis.x + vector.x * val, tween.axis.y + vector.y * val, tween.axis.z + vector.z * val, tween.from.y + num * val);
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x00042037 File Offset: 0x00040237
	public static void removeTween(int i, int uniqueId)
	{
		if (LeanTween.tweens[i].uniqueId == uniqueId)
		{
			LeanTween.removeTween(i);
		}
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x00042050 File Offset: 0x00040250
	public static void removeTween(int i)
	{
		if (LeanTween.tweens[i].toggle)
		{
			LeanTween.tweens[i].toggle = false;
			if (LeanTween.tweens[i].destroyOnComplete)
			{
				if (LeanTween.tweens[i].ltRect != null)
				{
					LTGUI.destroy(LeanTween.tweens[i].ltRect.id);
				}
				else if (LeanTween.tweens[i].trans.gameObject != LeanTween._tweenEmpty)
				{
					global::UnityEngine.Object.Destroy(LeanTween.tweens[i].trans.gameObject);
				}
			}
			LeanTween.tweens[i].cleanup();
			LeanTween.startSearch = i;
			if (i + 1 >= LeanTween.tweenMaxSearch)
			{
				LeanTween.startSearch = 0;
			}
		}
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x00042104 File Offset: 0x00040304
	public static Vector3[] add(Vector3[] a, Vector3 b)
	{
		Vector3[] array = new Vector3[a.Length];
		LeanTween.i = 0;
		while (LeanTween.i < a.Length)
		{
			array[LeanTween.i] = a[LeanTween.i] + b;
			LeanTween.i++;
		}
		return array;
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x00042158 File Offset: 0x00040358
	public static float closestRot(float from, float to)
	{
		float num = 0f - (360f - to);
		float num2 = 360f + to;
		float num3 = Mathf.Abs(to - from);
		float num4 = Mathf.Abs(num - from);
		float num5 = Mathf.Abs(num2 - from);
		if (num3 < num4 && num3 < num5)
		{
			return to;
		}
		if (num4 < num5)
		{
			return num;
		}
		return num2;
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x000421AA File Offset: 0x000403AA
	public static void cancelAll()
	{
		LeanTween.cancelAll(false);
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x000421B4 File Offset: 0x000403B4
	public static void cancelAll(bool callComplete)
	{
		LeanTween.init();
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			if (LeanTween.tweens[i].trans != null)
			{
				if (callComplete && LeanTween.tweens[i].onComplete != null)
				{
					LeanTween.tweens[i].onComplete();
				}
				LeanTween.removeTween(i);
			}
		}
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x00042212 File Offset: 0x00040412
	public static void cancel(GameObject gameObject)
	{
		LeanTween.cancel(gameObject, false);
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x0004221C File Offset: 0x0004041C
	public static void cancel(GameObject gameObject, bool callOnComplete)
	{
		LeanTween.init();
		Transform transform = gameObject.transform;
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			if (LeanTween.tweens[i].toggle && LeanTween.tweens[i].trans == transform)
			{
				if (callOnComplete && LeanTween.tweens[i].onComplete != null)
				{
					LeanTween.tweens[i].onComplete();
				}
				LeanTween.removeTween(i);
			}
		}
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x00042290 File Offset: 0x00040490
	public static void cancel(ControlLayout controlLayout, bool callOnComplete = false)
	{
		LeanTween.init();
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			if (LeanTween.tweens[i].toggle && LeanTween.tweens[i].controlLayout == controlLayout)
			{
				if (callOnComplete && LeanTween.tweens[i].onComplete != null)
				{
					LeanTween.tweens[i].onComplete();
				}
				LeanTween.removeTween(i);
			}
		}
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x000422F8 File Offset: 0x000404F8
	public static void cancel(GameObject gameObject, int uniqueId)
	{
		if (uniqueId >= 0)
		{
			LeanTween.init();
			int num = uniqueId & 65535;
			int num2 = uniqueId >> 16;
			if (LeanTween.tweens[num].trans == null || (LeanTween.tweens[num].trans.gameObject == gameObject && (ulong)LeanTween.tweens[num].counter == (ulong)((long)num2)))
			{
				LeanTween.removeTween(num);
			}
		}
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x00042360 File Offset: 0x00040560
	public static void cancel(LTRect ltRect, int uniqueId)
	{
		if (uniqueId >= 0)
		{
			LeanTween.init();
			int num = uniqueId & 65535;
			int num2 = uniqueId >> 16;
			if (LeanTween.tweens[num].ltRect == ltRect && (ulong)LeanTween.tweens[num].counter == (ulong)((long)num2))
			{
				LeanTween.removeTween(num);
			}
		}
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x000423A9 File Offset: 0x000405A9
	public static void cancel(int uniqueId)
	{
		LeanTween.cancel(uniqueId, false);
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x000423B4 File Offset: 0x000405B4
	public static void cancel(int uniqueId, bool callOnComplete)
	{
		if (uniqueId >= 0)
		{
			LeanTween.init();
			int num = uniqueId & 65535;
			int num2 = uniqueId >> 16;
			if ((ulong)LeanTween.tweens[num].counter == (ulong)((long)num2))
			{
				if (callOnComplete && LeanTween.tweens[num].onComplete != null)
				{
					LeanTween.tweens[num].onComplete();
				}
				LeanTween.removeTween(num);
			}
		}
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x00042410 File Offset: 0x00040610
	public static void finish(GameObject gameObject)
	{
		Transform transform = gameObject.transform;
		for (int i = 0; i < LeanTween.tweenMaxSearch; i++)
		{
			if (LeanTween.tweens[i].trans == transform)
			{
				LeanTween.update(true, i);
			}
		}
		LeanTween.cancel(gameObject);
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x00042458 File Offset: 0x00040658
	public static void finish(ControlLayout controlLayout)
	{
		for (int i = 0; i < LeanTween.tweenMaxSearch; i++)
		{
			if (LeanTween.tweens[i].toggle && LeanTween.tweens[i].controlLayout == controlLayout)
			{
				LeanTween.tweens[i].passed = LeanTween.tweens[i].time;
				LeanTween.update(true, i);
				if (controlLayout.Offset.x != -150f)
				{
					float x = controlLayout.Offset.x;
				}
				LeanTween.cancel(i);
				if (controlLayout.Offset.x != -150f)
				{
					float x2 = controlLayout.Offset.x;
				}
			}
		}
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x0004250C File Offset: 0x0004070C
	public static LTDescr descr(int uniqueId)
	{
		int num = uniqueId & 65535;
		int num2 = uniqueId >> 16;
		if (LeanTween.tweens[num] != null && LeanTween.tweens[num].uniqueId == uniqueId && (ulong)LeanTween.tweens[num].counter == (ulong)((long)num2))
		{
			return LeanTween.tweens[num];
		}
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			if (LeanTween.tweens[i].uniqueId == uniqueId && (ulong)LeanTween.tweens[i].counter == (ulong)((long)num2))
			{
				return LeanTween.tweens[i];
			}
		}
		return null;
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x00042590 File Offset: 0x00040790
	public static LTDescr description(int uniqueId)
	{
		return LeanTween.descr(uniqueId);
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x00042598 File Offset: 0x00040798
	public static LTDescr[] descriptions(GameObject gameObject = null)
	{
		if (gameObject == null)
		{
			return null;
		}
		List<LTDescr> list = new List<LTDescr>();
		Transform transform = gameObject.transform;
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			if (LeanTween.tweens[i].toggle && LeanTween.tweens[i].trans == transform)
			{
				list.Add(LeanTween.tweens[i]);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x00042602 File Offset: 0x00040802
	[Obsolete("Use 'pause( id )' instead")]
	public static void pause(GameObject gameObject, int uniqueId)
	{
		LeanTween.pause(uniqueId);
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x0004260C File Offset: 0x0004080C
	public static void pause(int uniqueId)
	{
		int num = uniqueId & 65535;
		int num2 = uniqueId >> 16;
		if ((ulong)LeanTween.tweens[num].counter == (ulong)((long)num2))
		{
			LeanTween.tweens[num].pause();
		}
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x00042644 File Offset: 0x00040844
	public static void pause(GameObject gameObject)
	{
		Transform transform = gameObject.transform;
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			if (LeanTween.tweens[i].trans == transform)
			{
				LeanTween.tweens[i].pause();
			}
		}
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x0004268C File Offset: 0x0004088C
	public static void pauseAll()
	{
		LeanTween.init();
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			LeanTween.tweens[i].pause();
		}
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x000426BC File Offset: 0x000408BC
	public static void resumeAll()
	{
		LeanTween.init();
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			LeanTween.tweens[i].resume();
		}
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x000426EB File Offset: 0x000408EB
	[Obsolete("Use 'resume( id )' instead")]
	public static void resume(GameObject gameObject, int uniqueId)
	{
		LeanTween.resume(uniqueId);
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x000426F4 File Offset: 0x000408F4
	public static void resume(int uniqueId)
	{
		int num = uniqueId & 65535;
		int num2 = uniqueId >> 16;
		if ((ulong)LeanTween.tweens[num].counter == (ulong)((long)num2))
		{
			LeanTween.tweens[num].resume();
		}
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x0004272C File Offset: 0x0004092C
	public static void resume(GameObject gameObject)
	{
		Transform transform = gameObject.transform;
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			if (LeanTween.tweens[i].trans == transform)
			{
				LeanTween.tweens[i].resume();
			}
		}
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x00042774 File Offset: 0x00040974
	public static bool isTweening(GameObject gameObject = null)
	{
		if (gameObject == null)
		{
			for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
			{
				if (LeanTween.tweens[i].toggle)
				{
					return true;
				}
			}
			return false;
		}
		Transform transform = gameObject.transform;
		for (int j = 0; j <= LeanTween.tweenMaxSearch; j++)
		{
			if (LeanTween.tweens[j].toggle && LeanTween.tweens[j].trans == transform)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x000427E8 File Offset: 0x000409E8
	public static bool isTweening(int uniqueId)
	{
		int num = uniqueId & 65535;
		int num2 = uniqueId >> 16;
		return num >= 0 && num < LeanTween.maxTweens && ((ulong)LeanTween.tweens[num].counter == (ulong)((long)num2) && LeanTween.tweens[num].toggle);
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x00042834 File Offset: 0x00040A34
	public static bool isTweening(LTRect ltRect)
	{
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			if (LeanTween.tweens[i].toggle && LeanTween.tweens[i].ltRect == ltRect)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x00042874 File Offset: 0x00040A74
	public static void drawBezierPath(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float arrowSize = 0f, Transform arrowTransform = null)
	{
		Vector3 vector = a;
		Vector3 vector2 = -a + 3f * (b - c) + d;
		Vector3 vector3 = 3f * (a + c) - 6f * b;
		Vector3 vector4 = 3f * (b - a);
		if (arrowSize > 0f)
		{
			Vector3 position = arrowTransform.position;
			Quaternion rotation = arrowTransform.rotation;
			float num = 0f;
			for (float num2 = 1f; num2 <= 120f; num2 += 1f)
			{
				float num3 = num2 / 120f;
				Vector3 vector5 = ((vector2 * num3 + vector3) * num3 + vector4) * num3 + a;
				Gizmos.DrawLine(vector, vector5);
				num += (vector5 - vector).magnitude;
				if (num > 1f)
				{
					num -= 1f;
					arrowTransform.position = vector5;
					arrowTransform.LookAt(vector, Vector3.forward);
					Vector3 vector6 = arrowTransform.TransformDirection(Vector3.right);
					Vector3 normalized = (vector - vector5).normalized;
					Gizmos.DrawLine(vector5, vector5 + (vector6 + normalized) * arrowSize);
					vector6 = arrowTransform.TransformDirection(-Vector3.right);
					Gizmos.DrawLine(vector5, vector5 + (vector6 + normalized) * arrowSize);
				}
				vector = vector5;
			}
			arrowTransform.position = position;
			arrowTransform.rotation = rotation;
			return;
		}
		for (float num4 = 1f; num4 <= 30f; num4 += 1f)
		{
			float num3 = num4 / 30f;
			Vector3 vector5 = ((vector2 * num3 + vector3) * num3 + vector4) * num3 + a;
			Gizmos.DrawLine(vector, vector5);
			vector = vector5;
		}
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x00042A76 File Offset: 0x00040C76
	public static object logError(string error)
	{
		if (LeanTween.throwErrors)
		{
			Debug.LogError(error);
		}
		else
		{
			Debug.Log(error);
		}
		return null;
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x00042A8E File Offset: 0x00040C8E
	public static LTDescr options(LTDescr seed)
	{
		Debug.LogError("error this function is no longer used");
		return null;
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x00042A9C File Offset: 0x00040C9C
	public static LTDescr options()
	{
		LeanTween.init();
		bool flag = false;
		LeanTween.j = 0;
		LeanTween.i = LeanTween.startSearch;
		while (LeanTween.j < LeanTween.maxTweens)
		{
			if (LeanTween.i >= LeanTween.maxTweens - 1)
			{
				LeanTween.i = 0;
			}
			if (!LeanTween.tweens[LeanTween.i].toggle)
			{
				if (LeanTween.i + 1 > LeanTween.tweenMaxSearch)
				{
					LeanTween.tweenMaxSearch = LeanTween.i + 1;
				}
				LeanTween.startSearch = LeanTween.i + 1;
				flag = true;
				break;
			}
			LeanTween.j++;
			if (LeanTween.j >= LeanTween.maxTweens)
			{
				return LeanTween.logError("LeanTween - You have run out of available spaces for tweening. To avoid this error increase the number of spaces to available for tweening when you initialize the LeanTween class ex: LeanTween.init( " + (LeanTween.maxTweens * 2).ToString() + " );") as LTDescr;
			}
			LeanTween.i++;
		}
		if (!flag)
		{
			LeanTween.logError("no available tween found!");
		}
		LeanTween.tweens[LeanTween.i].reset();
		LeanTween.tweens[LeanTween.i].setId((uint)LeanTween.i);
		return LeanTween.tweens[LeanTween.i];
	}

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x06000A18 RID: 2584 RVA: 0x00042BB0 File Offset: 0x00040DB0
	public static GameObject tweenEmpty
	{
		get
		{
			LeanTween.init(LeanTween.maxTweens);
			return LeanTween._tweenEmpty;
		}
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x00042BC4 File Offset: 0x00040DC4
	private static LTDescr pushNewTween(GameObject gameObject, Vector3 to, float time, TweenAction tweenAction, LTDescr tween)
	{
		LeanTween.init(LeanTween.maxTweens);
		if (gameObject == null || tween == null)
		{
			return null;
		}
		tween.trans = gameObject.transform;
		tween.to = to;
		tween.time = time;
		tween.type = tweenAction;
		if (tween.to.x != -150f)
		{
			float x = tween.to.x;
		}
		return tween;
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x00042C38 File Offset: 0x00040E38
	public static LTDescr play(RectTransform rectTransform, Sprite[] sprites)
	{
		float num = 0.25f * (float)sprites.Length;
		return LeanTween.pushNewTween(rectTransform.gameObject, new Vector3((float)sprites.Length - 1f, 0f, 0f), num, TweenAction.CANVAS_PLAYSPRITE, LeanTween.options().setSprites(sprites).setRepeat(-1));
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x00042C88 File Offset: 0x00040E88
	public static LTDescr alpha(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.ALPHA, LeanTween.options());
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x00042CA8 File Offset: 0x00040EA8
	public static LTDescr alpha(LTRect ltRect, float to, float time)
	{
		ltRect.alphaEnabled = true;
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, new Vector3(to, 0f, 0f), time, TweenAction.GUI_ALPHA, LeanTween.options().setRect(ltRect));
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x00042CD9 File Offset: 0x00040ED9
	public static LTDescr textAlpha(RectTransform rectTransform, float to, float time)
	{
		return LeanTween.pushNewTween(rectTransform.gameObject, new Vector3(to, 0f, 0f), time, TweenAction.TEXT_ALPHA, LeanTween.options());
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x00042CFE File Offset: 0x00040EFE
	public static LTDescr alphaVertex(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.ALPHA_VERTEX, LeanTween.options());
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x00042D20 File Offset: 0x00040F20
	public static LTDescr color(GameObject gameObject, Color to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, to.a, 0f), time, TweenAction.COLOR, LeanTween.options().setPoint(new Vector3(to.r, to.g, to.b)));
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x00042D6C File Offset: 0x00040F6C
	public static LTDescr textColor(RectTransform rectTransform, Color to, float time)
	{
		return LeanTween.pushNewTween(rectTransform.gameObject, new Vector3(1f, to.a, 0f), time, TweenAction.TEXT_COLOR, LeanTween.options().setPoint(new Vector3(to.r, to.g, to.b)));
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x00042DBD File Offset: 0x00040FBD
	public static LTDescr delayedCall(float delayTime, Action callback)
	{
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, Vector3.zero, delayTime, TweenAction.CALLBACK, LeanTween.options().setOnComplete(callback));
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x00042DDC File Offset: 0x00040FDC
	public static LTDescr delayedCall(float delayTime, Action<object> callback)
	{
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, Vector3.zero, delayTime, TweenAction.CALLBACK, LeanTween.options().setOnComplete(callback));
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x00042DFB File Offset: 0x00040FFB
	public static LTDescr delayedCall(GameObject gameObject, float delayTime, Action callback)
	{
		return LeanTween.pushNewTween(gameObject, Vector3.zero, delayTime, TweenAction.CALLBACK, LeanTween.options().setOnComplete(callback));
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x00042E16 File Offset: 0x00041016
	public static LTDescr delayedCall(GameObject gameObject, float delayTime, Action<object> callback)
	{
		return LeanTween.pushNewTween(gameObject, Vector3.zero, delayTime, TweenAction.CALLBACK, LeanTween.options().setOnComplete(callback));
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x00042E31 File Offset: 0x00041031
	public static LTDescr destroyAfter(LTRect rect, float delayTime)
	{
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, Vector3.zero, delayTime, TweenAction.CALLBACK, LeanTween.options().setRect(rect).setDestroyOnComplete(true));
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x00042E56 File Offset: 0x00041056
	public static LTDescr move(GameObject gameObject, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, to, time, TweenAction.MOVE, LeanTween.options());
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x00042E67 File Offset: 0x00041067
	public static LTDescr move(GameObject gameObject, Vector2 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to.x, to.y, gameObject.transform.position.z), time, TweenAction.MOVE, LeanTween.options());
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x00042E98 File Offset: 0x00041098
	public static LTDescr move(GameObject gameObject, Vector3[] to, float time)
	{
		LeanTween.d = LeanTween.options();
		if (LeanTween.d.path == null)
		{
			LeanTween.d.path = new LTBezierPath(to);
		}
		else
		{
			LeanTween.d.path.setPoints(to);
		}
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, TweenAction.MOVE_CURVED, LeanTween.d);
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x00042EFE File Offset: 0x000410FE
	public static LTDescr move(GameObject gameObject, LTBezierPath to, float time)
	{
		LeanTween.d = LeanTween.options();
		LeanTween.d.path = to;
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, TweenAction.MOVE_CURVED, LeanTween.d);
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x00042F36 File Offset: 0x00041136
	public static LTDescr move(GameObject gameObject, LTSpline to, float time)
	{
		LeanTween.d = LeanTween.options();
		LeanTween.d.spline = to;
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, TweenAction.MOVE_SPLINE, LeanTween.d);
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x00042F6E File Offset: 0x0004116E
	public static LTDescr moveSpline(GameObject gameObject, Vector3[] to, float time)
	{
		LeanTween.d = LeanTween.options();
		LeanTween.d.spline = new LTSpline(to);
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, TweenAction.MOVE_SPLINE, LeanTween.d);
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x00042FAB File Offset: 0x000411AB
	public static LTDescr moveSplineLocal(GameObject gameObject, Vector3[] to, float time)
	{
		LeanTween.d = LeanTween.options();
		LeanTween.d.spline = new LTSpline(to);
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, TweenAction.MOVE_SPLINE_LOCAL, LeanTween.d);
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x00042FE9 File Offset: 0x000411E9
	public static LTDescr move(LTRect ltRect, Vector2 to, float time)
	{
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, to, time, TweenAction.GUI_MOVE, LeanTween.options().setRect(ltRect));
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x00043009 File Offset: 0x00041209
	public static LTDescr moveMargin(LTRect ltRect, Vector2 to, float time)
	{
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, to, time, TweenAction.GUI_MOVE_MARGIN, LeanTween.options().setRect(ltRect));
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x00043029 File Offset: 0x00041229
	public static LTDescr moveX(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.MOVE_X, LeanTween.options());
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x00043048 File Offset: 0x00041248
	public static LTDescr moveY(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.MOVE_Y, LeanTween.options());
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x00043067 File Offset: 0x00041267
	public static LTDescr moveZ(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.MOVE_Z, LeanTween.options());
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x00043086 File Offset: 0x00041286
	public static LTDescr moveLocal(GameObject gameObject, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, to, time, TweenAction.MOVE_LOCAL, LeanTween.options());
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x00043098 File Offset: 0x00041298
	public static LTDescr moveLocal(GameObject gameObject, Vector3[] to, float time)
	{
		LeanTween.d = LeanTween.options();
		if (LeanTween.d.path == null)
		{
			LeanTween.d.path = new LTBezierPath(to);
		}
		else
		{
			LeanTween.d.path.setPoints(to);
		}
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, TweenAction.MOVE_CURVED_LOCAL, LeanTween.d);
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x000430FE File Offset: 0x000412FE
	public static LTDescr moveLocalX(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.MOVE_LOCAL_X, LeanTween.options());
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x0004311D File Offset: 0x0004131D
	public static LTDescr moveLocalY(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.MOVE_LOCAL_Y, LeanTween.options());
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x0004313C File Offset: 0x0004133C
	public static LTDescr moveLocalZ(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.MOVE_LOCAL_Z, LeanTween.options());
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x0004315B File Offset: 0x0004135B
	public static LTDescr moveLocal(GameObject gameObject, LTBezierPath to, float time)
	{
		LeanTween.d = LeanTween.options();
		LeanTween.d.path = to;
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, TweenAction.MOVE_CURVED_LOCAL, LeanTween.d);
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x00043193 File Offset: 0x00041393
	public static LTDescr moveLocal(GameObject gameObject, LTSpline to, float time)
	{
		LeanTween.d = LeanTween.options();
		LeanTween.d.spline = to;
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, TweenAction.MOVE_SPLINE_LOCAL, LeanTween.d);
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x000431CC File Offset: 0x000413CC
	public static LTDescr rotate(GameObject gameObject, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, to, time, TweenAction.ROTATE, LeanTween.options());
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x000431DD File Offset: 0x000413DD
	public static LTDescr rotate(LTRect ltRect, float to, float time)
	{
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, new Vector3(to, 0f, 0f), time, TweenAction.GUI_ROTATE, LeanTween.options().setRect(ltRect));
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x00043207 File Offset: 0x00041407
	public static LTDescr rotateLocal(GameObject gameObject, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, to, time, TweenAction.ROTATE_LOCAL, LeanTween.options());
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x00043218 File Offset: 0x00041418
	public static LTDescr rotateX(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.ROTATE_X, LeanTween.options());
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x00043238 File Offset: 0x00041438
	public static LTDescr rotateY(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.ROTATE_Y, LeanTween.options());
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x00043258 File Offset: 0x00041458
	public static LTDescr rotateZ(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.ROTATE_Z, LeanTween.options());
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x00043278 File Offset: 0x00041478
	public static LTDescr rotateAround(GameObject gameObject, Vector3 axis, float add, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(add, 0f, 0f), time, TweenAction.ROTATE_AROUND, LeanTween.options().setAxis(axis));
	}

	// Token: 0x06000A40 RID: 2624 RVA: 0x0004329E File Offset: 0x0004149E
	public static LTDescr rotateAroundLocal(GameObject gameObject, Vector3 axis, float add, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(add, 0f, 0f), time, TweenAction.ROTATE_AROUND_LOCAL, LeanTween.options().setAxis(axis));
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x000432C4 File Offset: 0x000414C4
	public static LTDescr scale(GameObject gameObject, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, to, time, TweenAction.SCALE, LeanTween.options());
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x000432D5 File Offset: 0x000414D5
	public static LTDescr scale(LTRect ltRect, Vector2 to, float time)
	{
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, to, time, TweenAction.GUI_SCALE, LeanTween.options().setRect(ltRect));
	}

	// Token: 0x06000A43 RID: 2627 RVA: 0x000432F5 File Offset: 0x000414F5
	public static LTDescr scaleX(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.SCALE_X, LeanTween.options());
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x00043315 File Offset: 0x00041515
	public static LTDescr scaleY(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.SCALE_Y, LeanTween.options());
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x00043335 File Offset: 0x00041535
	public static LTDescr scaleZ(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.SCALE_Z, LeanTween.options());
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x00043355 File Offset: 0x00041555
	public static LTDescr value(GameObject gameObject, float from, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.CALLBACK, LeanTween.options().setFrom(new Vector3(from, 0f, 0f)));
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x0004338C File Offset: 0x0004158C
	public static LTDescr value(GameObject gameObject, Vector2 from, Vector2 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to.x, to.y, 0f), time, TweenAction.VALUE3, LeanTween.options().setTo(new Vector3(to.x, to.y, 0f)).setFrom(new Vector3(from.x, from.y, 0f)));
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x000433F3 File Offset: 0x000415F3
	public static LTDescr value(GameObject gameObject, Vector3 from, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, to, time, TweenAction.VALUE3, LeanTween.options().setFrom(from));
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x0004340C File Offset: 0x0004160C
	public static LTDescr value(GameObject gameObject, Color from, Color to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, to.a, 0f), time, TweenAction.CALLBACK_COLOR, LeanTween.options().setPoint(new Vector3(to.r, to.g, to.b)).setFromColor(from)
			.setHasInitialized(false));
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x00043464 File Offset: 0x00041664
	public static LTDescr value(GameObject gameObject, Action<float> callOnUpdate, float from, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.CALLBACK, LeanTween.options().setTo(new Vector3(to, 0f, 0f)).setFrom(new Vector3(from, 0f, 0f))
			.setOnUpdate(callOnUpdate));
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x000434C0 File Offset: 0x000416C0
	public static LTDescr value(GameObject gameObject, Action<float, float> callOnUpdateRatio, float from, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.CALLBACK, LeanTween.options().setTo(new Vector3(to, 0f, 0f)).setFrom(new Vector3(from, 0f, 0f))
			.setOnUpdateRatio(callOnUpdateRatio));
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x0004351C File Offset: 0x0004171C
	public static LTDescr value(GameObject gameObject, Action<Color> callOnUpdate, Color from, Color to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, to.a, 0f), time, TweenAction.CALLBACK_COLOR, LeanTween.options().setPoint(new Vector3(to.r, to.g, to.b)).setAxis(new Vector3(from.r, from.g, from.b))
			.setFrom(new Vector3(0f, from.a, 0f))
			.setHasInitialized(false)
			.setOnUpdateColor(callOnUpdate));
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x000435AC File Offset: 0x000417AC
	public static LTDescr value(GameObject gameObject, Action<Vector2> callOnUpdate, Vector2 from, Vector2 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to.x, to.y, 0f), time, TweenAction.VALUE3, LeanTween.options().setTo(new Vector3(to.x, to.y, 0f)).setFrom(new Vector3(from.x, from.y, 0f))
			.setOnUpdateVector2(callOnUpdate));
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x0004361A File Offset: 0x0004181A
	public static LTDescr value(GameObject gameObject, Action<Vector3> callOnUpdate, Vector3 from, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, to, time, TweenAction.VALUE3, LeanTween.options().setTo(to).setFrom(from)
			.setOnUpdateVector3(callOnUpdate));
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x00043640 File Offset: 0x00041840
	public static LTDescr value(GameObject gameObject, Action<float, object> callOnUpdate, float from, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, TweenAction.CALLBACK, LeanTween.options().setTo(new Vector3(to, 0f, 0f)).setFrom(new Vector3(from, 0f, 0f))
			.setOnUpdateObject(callOnUpdate));
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x0004369C File Offset: 0x0004189C
	public static LTDescr delayedSound(AudioClip audio, Vector3 pos, float volume)
	{
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, pos, 0f, TweenAction.DELAYED_SOUND, LeanTween.options().setTo(pos).setFrom(new Vector3(volume, 0f, 0f))
			.setAudio(audio));
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x000436D6 File Offset: 0x000418D6
	public static LTDescr delayedSound(GameObject gameObject, AudioClip audio, Vector3 pos, float volume)
	{
		return LeanTween.pushNewTween(gameObject, pos, 0f, TweenAction.DELAYED_SOUND, LeanTween.options().setTo(pos).setFrom(new Vector3(volume, 0f, 0f))
			.setAudio(audio));
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x0004370C File Offset: 0x0004190C
	public static LTDescr move(RectTransform rectTrans, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, to, time, TweenAction.CANVAS_MOVE, LeanTween.options().setRect(rectTrans));
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x00043728 File Offset: 0x00041928
	public static LTDescr move(ControlLayout controlLayout, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(controlLayout.Parent.rootObject, to, time, TweenAction.LAYOUT_MOVE, LeanTween.options().setControlLayout(controlLayout));
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x00043749 File Offset: 0x00041949
	public static LTDescr moveX(RectTransform rectTrans, float to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, TweenAction.CANVAS_MOVE_X, LeanTween.options().setRect(rectTrans));
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x00043774 File Offset: 0x00041974
	public static LTDescr moveY(RectTransform rectTrans, float to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, TweenAction.CANVAS_MOVE_Y, LeanTween.options().setRect(rectTrans));
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x0004379F File Offset: 0x0004199F
	public static LTDescr moveZ(RectTransform rectTrans, float to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, TweenAction.CANVAS_MOVE_Z, LeanTween.options().setRect(rectTrans));
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x000437CA File Offset: 0x000419CA
	public static LTDescr rotate(RectTransform rectTrans, float to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, TweenAction.CANVAS_ROTATEAROUND, LeanTween.options().setRect(rectTrans).setAxis(Vector3.forward));
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x000437FF File Offset: 0x000419FF
	public static LTDescr rotateAround(RectTransform rectTrans, Vector3 axis, float to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, TweenAction.CANVAS_ROTATEAROUND, LeanTween.options().setRect(rectTrans).setAxis(axis));
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x00043830 File Offset: 0x00041A30
	public static LTDescr rotateAroundLocal(RectTransform rectTrans, Vector3 axis, float to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, TweenAction.CANVAS_ROTATEAROUND_LOCAL, LeanTween.options().setRect(rectTrans).setAxis(axis));
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x00043861 File Offset: 0x00041A61
	public static LTDescr scale(RectTransform rectTrans, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, to, time, TweenAction.CANVAS_SCALE, LeanTween.options().setRect(rectTrans));
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x0004387D File Offset: 0x00041A7D
	public static LTDescr alpha(RectTransform rectTrans, float to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, TweenAction.CANVAS_ALPHA, LeanTween.options().setRect(rectTrans));
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x000438A8 File Offset: 0x00041AA8
	public static LTDescr color(RectTransform rectTrans, Color to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, new Vector3(1f, to.a, 0f), time, TweenAction.CANVAS_COLOR, LeanTween.options().setRect(rectTrans).setPoint(new Vector3(to.r, to.g, to.b)));
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x000438FF File Offset: 0x00041AFF
	private static float tweenOnCurve(LTDescr tweenDescr, float ratioPassed)
	{
		return tweenDescr.from.x + tweenDescr.diff.x * tweenDescr.animationCurve.Evaluate(ratioPassed);
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x00043928 File Offset: 0x00041B28
	private static Vector3 tweenOnCurveVector(LTDescr tweenDescr, float ratioPassed)
	{
		return new Vector3(tweenDescr.from.x + tweenDescr.diff.x * tweenDescr.animationCurve.Evaluate(ratioPassed), tweenDescr.from.y + tweenDescr.diff.y * tweenDescr.animationCurve.Evaluate(ratioPassed), tweenDescr.from.z + tweenDescr.diff.z * tweenDescr.animationCurve.Evaluate(ratioPassed));
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x000439A6 File Offset: 0x00041BA6
	private static float easeOutQuadOpt(float start, float diff, float ratioPassed)
	{
		return -diff * ratioPassed * (ratioPassed - 2f) + start;
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x000439B6 File Offset: 0x00041BB6
	private static float easeInQuadOpt(float start, float diff, float ratioPassed)
	{
		return diff * ratioPassed * ratioPassed + start;
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x000439C0 File Offset: 0x00041BC0
	private static float easeInOutQuadOpt(float start, float diff, float ratioPassed)
	{
		ratioPassed /= 0.5f;
		if (ratioPassed < 1f)
		{
			return diff / 2f * ratioPassed * ratioPassed + start;
		}
		ratioPassed -= 1f;
		return -diff / 2f * (ratioPassed * (ratioPassed - 2f) - 1f) + start;
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x00043A0F File Offset: 0x00041C0F
	private static float linear(float start, float end, float val)
	{
		return Mathf.Lerp(start, end, val);
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x00043A1C File Offset: 0x00041C1C
	private static float clerp(float start, float end, float val)
	{
		float num = 0f;
		float num2 = 360f;
		float num3 = Mathf.Abs((num2 - num) / 2f);
		float num5;
		if (end - start < -num3)
		{
			float num4 = (num2 - start + end) * val;
			num5 = start + num4;
		}
		else if (end - start > num3)
		{
			float num4 = -(num2 - end + start) * val;
			num5 = start + num4;
		}
		else
		{
			num5 = start + (end - start) * val;
		}
		return num5;
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x00043A88 File Offset: 0x00041C88
	private static float spring(float start, float end, float val)
	{
		val = Mathf.Clamp01(val);
		val = (Mathf.Sin(val * 3.1415927f * (0.2f + 2.5f * val * val * val)) * Mathf.Pow(1f - val, 2.2f) + val) * (1f + 1.2f * (1f - val));
		return start + (end - start) * val;
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x00043AEC File Offset: 0x00041CEC
	private static float easeInQuad(float start, float end, float val)
	{
		end -= start;
		return end * val * val + start;
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x00043AFA File Offset: 0x00041CFA
	private static float easeOutQuad(float start, float end, float val)
	{
		end -= start;
		return -end * val * (val - 2f) + start;
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x00043B10 File Offset: 0x00041D10
	private static float easeInOutQuad(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return end / 2f * val * val + start;
		}
		val -= 1f;
		return -end / 2f * (val * (val - 2f) - 1f) + start;
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x00043B64 File Offset: 0x00041D64
	private static float easeInCubic(float start, float end, float val)
	{
		end -= start;
		return end * val * val * val + start;
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x00043B74 File Offset: 0x00041D74
	private static float easeOutCubic(float start, float end, float val)
	{
		val -= 1f;
		end -= start;
		return end * (val * val * val + 1f) + start;
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x00043B94 File Offset: 0x00041D94
	private static float easeInOutCubic(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return end / 2f * val * val * val + start;
		}
		val -= 2f;
		return end / 2f * (val * val * val + 2f) + start;
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x00043BE5 File Offset: 0x00041DE5
	private static float easeInQuart(float start, float end, float val)
	{
		end -= start;
		return end * val * val * val * val + start;
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x00043BF7 File Offset: 0x00041DF7
	private static float easeOutQuart(float start, float end, float val)
	{
		val -= 1f;
		end -= start;
		return -end * (val * val * val * val - 1f) + start;
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x00043C1C File Offset: 0x00041E1C
	private static float easeInOutQuart(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return end / 2f * val * val * val * val + start;
		}
		val -= 2f;
		return -end / 2f * (val * val * val * val - 2f) + start;
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x00043C72 File Offset: 0x00041E72
	private static float easeInQuint(float start, float end, float val)
	{
		end -= start;
		return end * val * val * val * val * val + start;
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x00043C86 File Offset: 0x00041E86
	private static float easeOutQuint(float start, float end, float val)
	{
		val -= 1f;
		end -= start;
		return end * (val * val * val * val * val + 1f) + start;
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x00043CAC File Offset: 0x00041EAC
	private static float easeInOutQuint(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return end / 2f * val * val * val * val * val + start;
		}
		val -= 2f;
		return end / 2f * (val * val * val * val * val + 2f) + start;
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x00043D05 File Offset: 0x00041F05
	private static float easeInSine(float start, float end, float val)
	{
		end -= start;
		return -end * Mathf.Cos(val / 1f * 1.5707964f) + end + start;
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x00043D25 File Offset: 0x00041F25
	private static float easeOutSine(float start, float end, float val)
	{
		end -= start;
		return end * Mathf.Sin(val / 1f * 1.5707964f) + start;
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x00043D42 File Offset: 0x00041F42
	private static float easeInOutSine(float start, float end, float val)
	{
		end -= start;
		return -end / 2f * (Mathf.Cos(3.1415927f * val / 1f) - 1f) + start;
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x00043D6C File Offset: 0x00041F6C
	private static float easeInExpo(float start, float end, float val)
	{
		end -= start;
		return end * Mathf.Pow(2f, 10f * (val / 1f - 1f)) + start;
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x00043D94 File Offset: 0x00041F94
	private static float easeOutExpo(float start, float end, float val)
	{
		end -= start;
		return end * (-Mathf.Pow(2f, -10f * val / 1f) + 1f) + start;
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x00043DC0 File Offset: 0x00041FC0
	private static float easeInOutExpo(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return end / 2f * Mathf.Pow(2f, 10f * (val - 1f)) + start;
		}
		val -= 1f;
		return end / 2f * (-Mathf.Pow(2f, -10f * val) + 2f) + start;
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x00043E30 File Offset: 0x00042030
	private static float easeInCirc(float start, float end, float val)
	{
		end -= start;
		return -end * (Mathf.Sqrt(1f - val * val) - 1f) + start;
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x00043E50 File Offset: 0x00042050
	private static float easeOutCirc(float start, float end, float val)
	{
		val -= 1f;
		end -= start;
		return end * Mathf.Sqrt(1f - val * val) + start;
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x00043E74 File Offset: 0x00042074
	private static float easeInOutCirc(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return -end / 2f * (Mathf.Sqrt(1f - val * val) - 1f) + start;
		}
		val -= 2f;
		return end / 2f * (Mathf.Sqrt(1f - val * val) + 1f) + start;
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x00043EE0 File Offset: 0x000420E0
	private static float easeInBounce(float start, float end, float val)
	{
		end -= start;
		float num = 1f;
		return end - LeanTween.easeOutBounce(0f, end, num - val) + start;
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x00043F0C File Offset: 0x0004210C
	private static float easeOutBounce(float start, float end, float val)
	{
		val /= 1f;
		end -= start;
		if (val < 0.36363637f)
		{
			return end * (7.5625f * val * val) + start;
		}
		if (val < 0.72727275f)
		{
			val -= 0.54545456f;
			return end * (7.5625f * val * val + 0.75f) + start;
		}
		if ((double)val < 0.9090909090909091)
		{
			val -= 0.8181818f;
			return end * (7.5625f * val * val + 0.9375f) + start;
		}
		val -= 0.95454544f;
		return end * (7.5625f * val * val + 0.984375f) + start;
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x00043FA8 File Offset: 0x000421A8
	private static float easeInOutBounce(float start, float end, float val)
	{
		end -= start;
		float num = 1f;
		if (val < num / 2f)
		{
			return LeanTween.easeInBounce(0f, end, val * 2f) * 0.5f + start;
		}
		return LeanTween.easeOutBounce(0f, end, val * 2f - num) * 0.5f + end * 0.5f + start;
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x0004400C File Offset: 0x0004220C
	private static float easeInBack(float start, float end, float val, float overshoot = 1f)
	{
		end -= start;
		val /= 1f;
		float num = 1.70158f * overshoot;
		return end * val * val * ((num + 1f) * val - num) + start;
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x00044044 File Offset: 0x00042244
	private static float easeOutBack(float start, float end, float val, float overshoot = 1f)
	{
		float num = 1.70158f * overshoot;
		end -= start;
		val = val / 1f - 1f;
		return end * (val * val * ((num + 1f) * val + num) + 1f) + start;
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x00044088 File Offset: 0x00042288
	private static float easeInOutBack(float start, float end, float val, float overshoot = 1f)
	{
		float num = 1.70158f * overshoot;
		end -= start;
		val /= 0.5f;
		if (val < 1f)
		{
			num *= 1.525f * overshoot;
			return end / 2f * (val * val * ((num + 1f) * val - num)) + start;
		}
		val -= 2f;
		num *= 1.525f * overshoot;
		return end / 2f * (val * val * ((num + 1f) * val + num) + 2f) + start;
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x0004410C File Offset: 0x0004230C
	private static float easeInElastic(float start, float end, float val, float overshoot = 1f, float period = 0.3f)
	{
		end -= start;
		float num = 0f;
		if (val == 0f)
		{
			return start;
		}
		if (val == 1f)
		{
			return start + end;
		}
		float num2;
		if (num == 0f || num < Mathf.Abs(end))
		{
			num = end;
			num2 = period / 4f;
		}
		else
		{
			num2 = period / 6.2831855f * Mathf.Asin(end / num);
		}
		if (overshoot > 1f && val > 0.6f)
		{
			overshoot = 1f + (1f - val) / 0.4f * (overshoot - 1f);
		}
		val -= 1f;
		return start - num * Mathf.Pow(2f, 10f * val) * Mathf.Sin((val - num2) * 6.2831855f / period) * overshoot;
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x000441D0 File Offset: 0x000423D0
	private static float easeOutElastic(float start, float end, float val, float overshoot = 1f, float period = 0.3f)
	{
		end -= start;
		float num = 0f;
		if (val == 0f)
		{
			return start;
		}
		if (val == 1f)
		{
			return start + end;
		}
		float num2;
		if (num == 0f || num < Mathf.Abs(end))
		{
			num = end;
			num2 = period / 4f;
		}
		else
		{
			num2 = period / 6.2831855f * Mathf.Asin(end / num);
		}
		if (overshoot > 1f && val < 0.4f)
		{
			overshoot = 1f + val / 0.4f * (overshoot - 1f);
		}
		return start + end + num * Mathf.Pow(2f, -10f * val) * Mathf.Sin((val - num2) * 6.2831855f / period) * overshoot;
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x00044288 File Offset: 0x00042488
	private static float easeInOutElastic(float start, float end, float val, float overshoot = 1f, float period = 0.3f)
	{
		end -= start;
		float num = 0f;
		if (val == 0f)
		{
			return start;
		}
		val /= 0.5f;
		if (val == 2f)
		{
			return start + end;
		}
		float num2;
		if (num == 0f || num < Mathf.Abs(end))
		{
			num = end;
			num2 = period / 4f;
		}
		else
		{
			num2 = period / 6.2831855f * Mathf.Asin(end / num);
		}
		if (overshoot > 1f)
		{
			if (val < 0.2f)
			{
				overshoot = 1f + val / 0.2f * (overshoot - 1f);
			}
			else if (val > 0.8f)
			{
				overshoot = 1f + (1f - val) / 0.2f * (overshoot - 1f);
			}
		}
		if (val < 1f)
		{
			val -= 1f;
			return start - 0.5f * (num * Mathf.Pow(2f, 10f * val) * Mathf.Sin((val - num2) * 6.2831855f / period)) * overshoot;
		}
		val -= 1f;
		return end + start + num * Mathf.Pow(2f, -10f * val) * Mathf.Sin((val - num2) * 6.2831855f / period) * 0.5f * overshoot;
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x000443BD File Offset: 0x000425BD
	public static void addListener(int eventId, Action<LTEvent> callback)
	{
		LeanTween.addListener(LeanTween.tweenEmpty, eventId, callback);
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x000443CC File Offset: 0x000425CC
	public static void addListener(GameObject caller, int eventId, Action<LTEvent> callback)
	{
		if (LeanTween.eventListeners == null)
		{
			LeanTween.INIT_LISTENERS_MAX = LeanTween.LISTENERS_MAX;
			LeanTween.eventListeners = new Action<LTEvent>[LeanTween.EVENTS_MAX * LeanTween.LISTENERS_MAX];
			LeanTween.goListeners = new GameObject[LeanTween.EVENTS_MAX * LeanTween.LISTENERS_MAX];
		}
		LeanTween.i = 0;
		while (LeanTween.i < LeanTween.INIT_LISTENERS_MAX)
		{
			int num = eventId * LeanTween.INIT_LISTENERS_MAX + LeanTween.i;
			if (LeanTween.goListeners[num] == null || LeanTween.eventListeners[num] == null)
			{
				LeanTween.eventListeners[num] = callback;
				LeanTween.goListeners[num] = caller;
				if (LeanTween.i >= LeanTween.eventsMaxSearch)
				{
					LeanTween.eventsMaxSearch = LeanTween.i + 1;
				}
				return;
			}
			if (LeanTween.goListeners[num] == caller && object.Equals(LeanTween.eventListeners[num], callback))
			{
				return;
			}
			LeanTween.i++;
		}
		Debug.LogError("You ran out of areas to add listeners, consider increasing INIT_LISTENERS_MAX, ex: LeanTween.INIT_LISTENERS_MAX = " + (LeanTween.INIT_LISTENERS_MAX * 2).ToString());
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x000444C3 File Offset: 0x000426C3
	public static bool removeListener(int eventId, Action<LTEvent> callback)
	{
		return LeanTween.removeListener(LeanTween.tweenEmpty, eventId, callback);
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x000444D4 File Offset: 0x000426D4
	public static bool removeListener(GameObject caller, int eventId, Action<LTEvent> callback)
	{
		LeanTween.i = 0;
		while (LeanTween.i < LeanTween.eventsMaxSearch)
		{
			int num = eventId * LeanTween.INIT_LISTENERS_MAX + LeanTween.i;
			if (LeanTween.goListeners[num] == caller && object.Equals(LeanTween.eventListeners[num], callback))
			{
				LeanTween.eventListeners[num] = null;
				LeanTween.goListeners[num] = null;
				return true;
			}
			LeanTween.i++;
		}
		return false;
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x00044540 File Offset: 0x00042740
	public static void dispatchEvent(int eventId)
	{
		LeanTween.dispatchEvent(eventId, null);
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x0004454C File Offset: 0x0004274C
	public static void dispatchEvent(int eventId, object data)
	{
		for (int i = 0; i < LeanTween.eventsMaxSearch; i++)
		{
			int num = eventId * LeanTween.INIT_LISTENERS_MAX + i;
			if (LeanTween.eventListeners[num] != null)
			{
				if (LeanTween.goListeners[num])
				{
					LeanTween.eventListeners[num](new LTEvent(eventId, data));
				}
				else
				{
					LeanTween.eventListeners[num] = null;
				}
			}
		}
	}

	// Token: 0x040006E3 RID: 1763
	public static bool throwErrors = true;

	// Token: 0x040006E4 RID: 1764
	public static float tau = 6.2831855f;

	// Token: 0x040006E5 RID: 1765
	private static LTDescr[] tweens;

	// Token: 0x040006E6 RID: 1766
	private static int[] tweensFinished;

	// Token: 0x040006E7 RID: 1767
	private static LTDescr tween;

	// Token: 0x040006E8 RID: 1768
	private static int tweenMaxSearch = -1;

	// Token: 0x040006E9 RID: 1769
	private static int maxTweens = 2000;

	// Token: 0x040006EA RID: 1770
	private static int frameRendered = -1;

	// Token: 0x040006EB RID: 1771
	private static GameObject _tweenEmpty;

	// Token: 0x040006EC RID: 1772
	private static float dtEstimated = -1f;

	// Token: 0x040006ED RID: 1773
	public static float dtManual;

	// Token: 0x040006EE RID: 1774
	private static float dt;

	// Token: 0x040006EF RID: 1775
	private static float dtActual;

	// Token: 0x040006F0 RID: 1776
	private static int i;

	// Token: 0x040006F1 RID: 1777
	private static int j;

	// Token: 0x040006F2 RID: 1778
	private static int finishedCnt;

	// Token: 0x040006F3 RID: 1779
	private static AnimationCurve punch = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(0.112586f, 0.9976035f),
		new Keyframe(0.3120486f, -0.1720615f),
		new Keyframe(0.4316337f, 0.07030682f),
		new Keyframe(0.5524869f, -0.03141804f),
		new Keyframe(0.6549395f, 0.003909959f),
		new Keyframe(0.770987f, -0.009817753f),
		new Keyframe(0.8838775f, 0.001939224f),
		new Keyframe(1f, 0f)
	});

	// Token: 0x040006F4 RID: 1780
	private static AnimationCurve shake = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(0.25f, 1f),
		new Keyframe(0.75f, -1f),
		new Keyframe(1f, 0f)
	});

	// Token: 0x040006F5 RID: 1781
	private static Transform trans;

	// Token: 0x040006F6 RID: 1782
	private static float timeTotal;

	// Token: 0x040006F7 RID: 1783
	private static TweenAction tweenAction;

	// Token: 0x040006F8 RID: 1784
	private static float ratioPassed;

	// Token: 0x040006F9 RID: 1785
	private static float from;

	// Token: 0x040006FA RID: 1786
	private static float val;

	// Token: 0x040006FB RID: 1787
	private static bool isTweenFinished;

	// Token: 0x040006FC RID: 1788
	private static int maxTweenReached;

	// Token: 0x040006FD RID: 1789
	private static Vector3 newVect;

	// Token: 0x040006FE RID: 1790
	private static GameObject target;

	// Token: 0x040006FF RID: 1791
	private static GameObject customTarget;

	// Token: 0x04000700 RID: 1792
	public static int startSearch = 0;

	// Token: 0x04000701 RID: 1793
	public static LTDescr d;

	// Token: 0x04000702 RID: 1794
	private static Action<LTEvent>[] eventListeners;

	// Token: 0x04000703 RID: 1795
	private static GameObject[] goListeners;

	// Token: 0x04000704 RID: 1796
	private static int eventsMaxSearch = 0;

	// Token: 0x04000705 RID: 1797
	public static int EVENTS_MAX = 10;

	// Token: 0x04000706 RID: 1798
	public static int LISTENERS_MAX = 10;

	// Token: 0x04000707 RID: 1799
	private static int INIT_LISTENERS_MAX = LeanTween.LISTENERS_MAX;
}
