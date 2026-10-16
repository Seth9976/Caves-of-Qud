using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000142 RID: 322
[AddComponentMenu("Accessibility/Core/UAP Manager")]
public class UAP_AccessibilityManager : MonoBehaviour
{
	// Token: 0x1700014B RID: 331
	// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0004F46B File Offset: 0x0004D66B
	public bool WindowsUseExploreByTouch
	{
		get
		{
			return this.m_WindowsUseMouseSwipes;
		}
	}

	// Token: 0x1700014C RID: 332
	// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0004F473 File Offset: 0x0004D673
	public static string GoogleTTSAPIKey
	{
		get
		{
			if (UAP_AccessibilityManager.instance == null)
			{
				return "";
			}
			return UAP_AccessibilityManager.instance.m_GoogleTTSAPIKey;
		}
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x0004F494 File Offset: 0x0004D694
	private UAP_AccessibilityManager()
	{
		this.m_ActiveContainers.Clear();
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x0004F6CE File Offset: 0x0004D8CE
	private void Awake()
	{
		if (UAP_AccessibilityManager.instance != this && UAP_AccessibilityManager.instance != null)
		{
			UAP_AccessibilityManager.Log("Found another UAP Accessibility Manager in the scene. Destroying this one.");
			global::UnityEngine.Object.DestroyImmediate(base.gameObject);
			return;
		}
		UAP_AccessibilityManager.instance = this;
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x0004F706 File Offset: 0x0004D906
	private void Start()
	{
		UAP_AccessibilityManager.Initialize();
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		SceneManager.sceneLoaded += this.OnSceneLoaded;
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x0004F729 File Offset: 0x0004D929
	private void OnDestroy()
	{
		if (UAP_AccessibilityManager.instance == this)
		{
			UAP_AccessibilityManager.isDestroyed = true;
		}
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x0004F740 File Offset: 0x0004D940
	private static void Initialize()
	{
		if (UAP_AccessibilityManager.m_IsInitialized)
		{
			return;
		}
		UAP_AccessibilityManager.m_IsInitialized = true;
		if (UAP_AccessibilityManager.instance == null)
		{
			UAP_AccessibilityManager.instance = (global::UnityEngine.Object.Instantiate(Resources.Load("Accessibility Manager")) as GameObject).GetComponent<UAP_AccessibilityManager>();
		}
		if (UAP_AccessibilityManager.instance.m_CanvasRoot == null)
		{
			UAP_AccessibilityManager.instance.m_CanvasRoot = UAP_AccessibilityManager.instance.transform.GetChild(0).GetComponent<Canvas>();
		}
		if (UAP_AccessibilityManager.instance.m_AudioPlayer == null)
		{
			UAP_AccessibilityManager.instance.m_AudioPlayer = UAP_AccessibilityManager.instance.m_CanvasRoot.gameObject.GetComponent<AudioSource>();
			if (UAP_AccessibilityManager.instance.m_AudioPlayer == null || UAP_AccessibilityManager.instance.m_AudioPlayer == UAP_AccessibilityManager.instance.m_SFXPlayer)
			{
				UAP_AccessibilityManager.instance.m_AudioPlayer = UAP_AccessibilityManager.instance.m_CanvasRoot.gameObject.AddComponent<AudioSource>();
			}
		}
		if (UAP_AccessibilityManager.instance.m_SFXPlayer == null)
		{
			UAP_AccessibilityManager.instance.m_SFXPlayer = UAP_AccessibilityManager.instance.m_CanvasRoot.gameObject.AddComponent<AudioSource>();
		}
		if (UAP_AccessibilityManager.instance.m_AudioQueue == null)
		{
			GameObject gameObject = new GameObject("Audio Queue");
			UAP_AccessibilityManager.instance.m_AudioQueue = gameObject.AddComponent<UAP_AudioQueue>();
			UAP_AccessibilityManager.instance.m_AudioQueue.Initialize();
			gameObject.transform.SetParent(UAP_AccessibilityManager.instance.transform, false);
		}
		if (PlayerPrefs.HasKey("UAP_Language"))
		{
			UAP_AccessibilityManager.m_CurrentLanguage = PlayerPrefs.GetString("UAP_Language", Application.systemLanguage.ToString());
		}
		else
		{
			UAP_AccessibilityManager.m_CurrentLanguage = Application.systemLanguage.ToString();
		}
		UAP_AccessibilityManager.instance.LoadLocalization();
		UAP_AccessibilityManager.InitNGUI();
		UAP_AccessibilityManager.HideElementFrame();
		UAP_AccessibilityManager.m_IsEnabled = UAP_AccessibilityManager.ShouldAutoEnable();
		UAP_AccessibilityManager.instance.EnableTouchBlocker(UAP_AccessibilityManager.m_IsEnabled);
		UAP_AccessibilityManager.SavePluginEnabledState();
		bool isEnabled = UAP_AccessibilityManager.m_IsEnabled;
		UAP_AccessibilityManager.Log("Initializing UAP Accessibility Manager");
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x0004F940 File Offset: 0x0004DB40
	private void LoadLocalization()
	{
		if (UAP_AccessibilityManager.m_CurrentLocalizationTable == null)
		{
			UAP_AccessibilityManager.m_CurrentLocalizationTable = new Dictionary<string, string>();
		}
		else
		{
			UAP_AccessibilityManager.m_CurrentLocalizationTable.Clear();
		}
		TextAsset textAsset = Resources.Load("UAP_InternalLocalization") as TextAsset;
		if (textAsset == null)
		{
			Debug.LogError("[Accessibility] Localization table 'UAP_InternalLocalization.txt' not found.");
			return;
		}
		string[] array = textAsset.text.Split('\r', StringSplitOptions.None);
		if (array.Length < 2)
		{
			array = textAsset.text.Split('\n', StringSplitOptions.None);
			if (array.Length < 2)
			{
				Debug.LogError("[Accessibility] Localization table empty or invalid");
				return;
			}
		}
		string[] array2 = array[0].Split('\t', StringSplitOptions.None);
		if (array2.Length < 2)
		{
			Debug.LogError("[Accessibility] Localization table invalid");
			return;
		}
		int num = -1;
		for (int i = 1; i < array2.Length; i++)
		{
			if (UAP_AccessibilityManager.m_CurrentLanguage.ToLower().CompareTo(array2[i].ToLower()) == 0)
			{
				num = i;
			}
		}
		if (num < 0)
		{
			num = 1;
			Debug.LogWarning("[Accessibility] Current language '" + UAP_AccessibilityManager.m_CurrentLanguage + "' is not supported. Defaulting to language " + array2[num]);
		}
		for (int j = 1; j < array.Length; j++)
		{
			string[] array3 = array[j].Split('\t', StringSplitOptions.None);
			string text = array3[0].Trim();
			if (!string.IsNullOrEmpty(text) && array3.Length > num)
			{
				UAP_AccessibilityManager.m_CurrentLocalizationTable.Add(text, array3[num]);
			}
		}
		UAP_AccessibilityManager.Log("Localization table loaded successfully. Entries: " + UAP_AccessibilityManager.m_CurrentLocalizationTable.Count.ToString("N0") + " Current Language:" + UAP_AccessibilityManager.m_CurrentLanguage);
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x0004FAB0 File Offset: 0x0004DCB0
	public static void SetLanguage(string language)
	{
		if (language.ToLower().CompareTo(UAP_AccessibilityManager.m_CurrentLanguage.ToLower()) != 0)
		{
			UAP_AccessibilityManager.m_CurrentLanguage = language;
			PlayerPrefs.SetString("UAP_Language", language);
			PlayerPrefs.Save();
			if (UAP_AccessibilityManager.instance != null)
			{
				UAP_AccessibilityManager.instance.LoadLocalization();
			}
		}
		UAP_AccessibilityManager.DetectEuropeanLanguage();
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x0004FB06 File Offset: 0x0004DD06
	private static void InitNGUI()
	{
		UAP_AccessibilityManager.HideElementFrame();
		UAP_AccessibilityManager.instance.EnableTouchBlocker(UAP_AccessibilityManager.m_IsEnabled);
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x0004FB1C File Offset: 0x0004DD1C
	private void OnSceneLoaded(Scene newScene, LoadSceneMode sceneLoadMode)
	{
		UAP_AccessibilityManager.Log("Level loaded, clearing everything.");
		UAP_AccessibilityManager.InitNGUI();
		this.m_ActiveContainers.Clear();
		this.m_SuspendedContainers.Clear();
		this.m_SuspendedActiveContainerIndex.Clear();
		this.m_AudioQueue.Stop();
		this.m_CurrentItem = null;
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x0004FB6B File Offset: 0x0004DD6B
	private static void CreateNGUIItemFrame()
	{
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x0004FB6D File Offset: 0x0004DD6D
	public static bool ShouldUseBuiltInKeyboard()
	{
		return !(UAP_AccessibilityManager.instance == null) && UAP_AccessibilityManager.instance.m_AllowBuiltInVirtualKeyboard && (Application.platform == RuntimePlatform.IPhonePlayer || (Application.isMobilePlatform && UAP_VirtualKeyboard.SupportsSystemLanguage()));
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x0004FBA4 File Offset: 0x0004DDA4
	private static bool ShouldAutoEnable()
	{
		bool flag = UAP_AccessibilityManager.instance.m_DefaultState;
		bool flag2 = UAP_AccessibilityManager.instance.m_SaveEnabledState && PlayerPrefs.HasKey("UAP_Enabled_State");
		if (flag2)
		{
			flag = PlayerPrefs.GetInt("UAP_Enabled_State", 0) == 1;
		}
		if (!flag2 && UAP_AccessibilityManager.instance.m_AutoTurnOnIfScreenReaderDetected && WindowsTTS.IsScreenReaderDetected())
		{
			flag = true;
		}
		return flag;
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x0004FBFF File Offset: 0x0004DDFF
	private static void SavePluginEnabledState()
	{
		PlayerPrefs.SetInt("UAP_Enabled_State", UAP_AccessibilityManager.m_IsEnabled ? 1 : 0);
		PlayerPrefs.Save();
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x0004FC1C File Offset: 0x0004DE1C
	private static void ReadItem(AccessibleUIGroupRoot.Accessible_UIElement element, bool quickOnly = false)
	{
		UAP_AccessibilityManager.Initialize();
		if (!UAP_AccessibilityManager.m_IsEnabled || UAP_AccessibilityManager.m_Paused)
		{
			return;
		}
		if (UAP_AccessibilityManager.instance.m_AudioPlayer.isPlaying)
		{
			UAP_AccessibilityManager.instance.m_AudioPlayer.Stop();
		}
		if (element == null)
		{
			return;
		}
		UAP_AccessibilityManager.UpdateElementFrame(ref element);
		UAP_AccessibilityManager.SpeakElement_Text(ref element);
		UAP_AccessibilityManager.instance.ReadDisabledState();
		UAP_AccessibilityManager.instance.ReadValue(true, false);
		if (element.ReadType())
		{
			if (UAP_AccessibilityManager.instance.m_ContinuousReading)
			{
				UAP_AccessibilityManager.instance.SayPause(UAP_AccessibilityManager.instance.m_TypeDelay * 0.5f);
			}
			else
			{
				UAP_AccessibilityManager.instance.SayPause(UAP_AccessibilityManager.instance.m_TypeDelay);
			}
			UAP_AccessibilityManager.instance.ReadType();
		}
		if (!quickOnly)
		{
			UAP_AccessibilityManager.instance.SayPause(UAP_AccessibilityManager.instance.m_HintDelay);
			UAP_AccessibilityManager.instance.ReadHint();
		}
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x0004FCF4 File Offset: 0x0004DEF4
	private static void UpdateElementFrame(ref AccessibleUIGroupRoot.Accessible_UIElement element)
	{
		if (UAP_AccessibilityManager.instance.m_Frame == null)
		{
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(UAP_AccessibilityManager.instance.m_FrameTemplate, UAP_AccessibilityManager.instance.m_TouchBlocker.transform);
			UAP_AccessibilityManager.instance.m_Frame = gameObject.transform as RectTransform;
		}
		UAP_AccessibilityManager.HideElementFrame();
		if (element.m_Object.Is3DElement())
		{
			UAP_BaseElement_3D uap_BaseElement_3D = element.m_Object as UAP_BaseElement_3D;
			Camera camera = ((uap_BaseElement_3D.m_CameraRenderingThisObject != null) ? uap_BaseElement_3D.m_CameraRenderingThisObject : Camera.main);
			GameObject gameObject2 = (element.m_Object.m_UseTargetForOutline ? element.m_Object.GetTargetGameObject() : element.m_Object.gameObject);
			Vector2 vector = camera.WorldToScreenPoint(gameObject2.transform.position);
			UAP_AccessibilityManager.instance.m_Frame.gameObject.SetActive(true);
			UAP_AccessibilityManager.instance.m_Frame.transform.SetParent(UAP_AccessibilityManager.instance.m_TouchBlocker.transform);
			UAP_AccessibilityManager.instance.m_Frame.position = vector;
			UAP_AccessibilityManager.instance.m_Frame.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, uap_BaseElement_3D.GetPixelHeight());
			UAP_AccessibilityManager.instance.m_Frame.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, uap_BaseElement_3D.GetPixelWidth());
			return;
		}
		RectTransform elementRect = UAP_AccessibilityManager.GetElementRect(ref element);
		if (elementRect != null)
		{
			UAP_AccessibilityManager.instance.m_Frame.gameObject.SetActive(true);
			if (UAP_AccessibilityManager.GetOwningCanvas(ref element) == null)
			{
				return;
			}
			if (UAP_AccessibilityManager.instance.m_Frame.transform.parent != elementRect.transform)
			{
				UAP_AccessibilityManager.instance.m_Frame.transform.SetParent(elementRect.transform, false);
				(UAP_AccessibilityManager.instance.m_Frame.transform as RectTransform).anchoredPosition3D = Vector3.zero;
				UAP_AccessibilityManager.instance.m_Frame.transform.localScale = Vector3.one;
			}
			UAP_AccessibilityManager.instance.m_Frame.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, elementRect.rect.height);
			UAP_AccessibilityManager.instance.m_Frame.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, elementRect.rect.width);
		}
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x0004FF28 File Offset: 0x0004E128
	private static void HideElementFrame()
	{
		if (UAP_AccessibilityManager.instance.m_Frame != null && UAP_AccessibilityManager.instance.m_Frame.gameObject != null)
		{
			UAP_AccessibilityManager.instance.m_Frame.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x0004FF68 File Offset: 0x0004E168
	private static Canvas GetOwningCanvas(ref AccessibleUIGroupRoot.Accessible_UIElement element)
	{
		return element.m_Object.gameObject.GetComponentInParent<Canvas>();
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x0004FF7C File Offset: 0x0004E17C
	private static RectTransform GetElementRect(ref AccessibleUIGroupRoot.Accessible_UIElement element)
	{
		GameObject gameObject = (element.m_Object.m_UseTargetForOutline ? element.m_Object.GetTargetGameObject() : element.m_Object.gameObject);
		RectTransform component = gameObject.GetComponent<RectTransform>();
		if (component != null)
		{
			bool flag = !gameObject.GetComponentInParent<UAP_ScrollControl>();
			if (element.m_Object.m_IsInsideScrollView && flag)
			{
				ScrollRect componentInParent = gameObject.GetComponentInParent<ScrollRect>();
				if (componentInParent != null)
				{
					bool flag2 = true;
					if (componentInParent.viewport == null)
					{
						Transform transform = componentInParent.transform.Find("Viewport");
						componentInParent.viewport = ((transform != null) ? ((RectTransform)transform) : null);
						if (componentInParent.viewport == null)
						{
							componentInParent.viewport = (RectTransform)componentInParent.transform.GetChild(0);
						}
						if (componentInParent.viewport == componentInParent.content)
						{
							componentInParent.viewport = (RectTransform)componentInParent.transform;
						}
					}
					RectTransform rectTransform = (RectTransform)componentInParent.viewport.transform;
					Vector3[] array = new Vector3[4];
					rectTransform.GetWorldCorners(array);
					Vector3[] array2 = new Vector3[4];
					component.GetWorldCorners(array2);
					bool flag3 = array2[0].x < array[0].x || array2[2].x > array[2].x;
					bool flag4 = array2[0].y < array[0].y || array2[2].y > array[2].y;
					if (!componentInParent.vertical)
					{
						flag4 = false;
					}
					if (!componentInParent.horizontal)
					{
						flag3 = false;
					}
					if (flag3 || flag4)
					{
						flag2 = false;
					}
					if (!flag2)
					{
						Vector3[] array3 = new Vector3[4];
						componentInParent.content.GetWorldCorners(array3);
						if (componentInParent.horizontal && flag3)
						{
							if (array3[2].x == 0f)
							{
								array3[2].x = 0.01f;
							}
							float num = array[2].x - array[0].x;
							float num2 = array3[2].x - array3[0].x;
							float num3 = array2[0].x - array3[0].x;
							float num4 = num2 - num;
							if (num3 >= num4)
							{
								componentInParent.horizontalNormalizedPosition = 1f;
							}
							else
							{
								float num5 = num3 / num4;
								componentInParent.horizontalNormalizedPosition = num5;
							}
						}
						if (componentInParent.vertical && flag4)
						{
							if (array3[2].y == 0f)
							{
								array3[2].y = 0.01f;
							}
							float num6 = array[2].y - array[0].y;
							float num7 = array3[2].y - array3[0].y;
							float num8 = array2[0].y - array3[0].y;
							float num9 = num7 - num6;
							if (num8 >= num9)
							{
								componentInParent.verticalNormalizedPosition = 1f;
							}
							else
							{
								float num10 = num8 / num9;
								componentInParent.verticalNormalizedPosition = num10;
							}
						}
					}
				}
			}
		}
		return component;
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x000502D0 File Offset: 0x0004E4D0
	private bool IsPositionOverElement(Vector2 fingerPos, AccessibleUIGroupRoot.Accessible_UIElement element)
	{
		if (element.m_Object.Is3DElement())
		{
			UAP_BaseElement_3D uap_BaseElement_3D = element.m_Object as UAP_BaseElement_3D;
			Camera camera = ((uap_BaseElement_3D.m_CameraRenderingThisObject != null) ? uap_BaseElement_3D.m_CameraRenderingThisObject : Camera.main);
			GameObject gameObject = (element.m_Object.m_UseTargetForOutline ? element.m_Object.GetTargetGameObject() : element.m_Object.gameObject);
			Vector2 vector = camera.WorldToScreenPoint(gameObject.transform.position);
			float pixelWidth = uap_BaseElement_3D.GetPixelWidth();
			float pixelHeight = uap_BaseElement_3D.GetPixelHeight();
			bool flag = true;
			Vector2 vector2 = vector - fingerPos;
			Vector2 vector3 = new Vector2(0f, 0f)
			{
				x = pixelWidth * 0.5f,
				y = pixelHeight * 0.5f
			};
			if (vector3.x < Mathf.Abs(vector2.x))
			{
				flag = false;
			}
			if (vector3.y < Mathf.Abs(vector2.y))
			{
				flag = false;
			}
			if (flag)
			{
				return true;
			}
		}
		RectTransform elementRect = UAP_AccessibilityManager.GetElementRect(ref element);
		if (elementRect != null)
		{
			Canvas componentInParent = element.m_Object.GetComponentInParent<Canvas>();
			if (componentInParent.renderMode != RenderMode.ScreenSpaceOverlay)
			{
				Vector3[] array = new Vector3[4];
				elementRect.GetWorldCorners(array);
				Camera worldCamera = componentInParent.worldCamera;
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = worldCamera.WorldToScreenPoint(array[i]);
				}
				if (fingerPos.x >= array[0].x && fingerPos.x <= array[2].x && fingerPos.y >= array[3].y && fingerPos.y <= array[1].y)
				{
					return true;
				}
			}
			else
			{
				CanvasScaler componentInParent2 = element.m_Object.GetComponentInParent<CanvasScaler>();
				float num = (float)Screen.width;
				float num2 = (float)Screen.height;
				if (componentInParent2 != null && componentInParent2.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize)
				{
					num2 = componentInParent2.referenceResolution.y;
					num = componentInParent2.referenceResolution.x;
				}
				float num3 = (float)Screen.width / num;
				float num4 = (float)Screen.height / num2;
				Vector2 vector4 = elementRect.position;
				float num5 = elementRect.rect.width;
				float num6 = elementRect.rect.height;
				num5 *= num3;
				num6 *= num4;
				vector4.x -= (elementRect.pivot.x - 0.5f) * num5;
				vector4.y -= (elementRect.pivot.y - 0.5f) * num6;
				bool flag2 = true;
				Vector2 vector5 = vector4 - fingerPos;
				Vector2 vector6 = new Vector2(0f, 0f)
				{
					x = num5 * 0.5f,
					y = num6 * 0.5f
				};
				if (vector6.x < Mathf.Abs(vector5.x))
				{
					flag2 = false;
				}
				if (vector6.y < Mathf.Abs(vector5.y))
				{
					flag2 = false;
				}
				if (flag2)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x000505F1 File Offset: 0x0004E7F1
	private void SayAudio(AudioClip clip, string altText, UAP_AudioQueue.EAudioType type, bool allowVoiceOver, UAP_AudioQueue.EInterrupt interrupts)
	{
		this.SayAudio(clip, altText, type, allowVoiceOver, null, interrupts);
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x00050604 File Offset: 0x0004E804
	private void SayAudio(AudioClip clip, string altText, UAP_AudioQueue.EAudioType type, bool allowVoiceOver, UAP_AudioQueue.UAP_GenericCallback callbackOnDone = null, UAP_AudioQueue.EInterrupt interrupts = UAP_AudioQueue.EInterrupt.None)
	{
		if (clip == null && (altText.Length < 1 || altText == null))
		{
			return;
		}
		if (clip != null)
		{
			this.m_AudioQueue.QueueAudio(clip, type, callbackOnDone, interrupts, true);
			return;
		}
		this.m_AudioQueue.QueueAudio(altText, type, allowVoiceOver, callbackOnDone, interrupts, true);
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x00050657 File Offset: 0x0004E857
	private void SayPause(float durationInSec)
	{
		this.m_AudioQueue.QueuePause(durationInSec);
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x00050668 File Offset: 0x0004E868
	private void ReadDisabledState()
	{
		if (this.m_CurrentItem == null)
		{
			return;
		}
		if ((this.m_CurrentItem.m_Type == AccessibleUIGroupRoot.EUIElement.EButton || this.m_CurrentItem.m_Type == AccessibleUIGroupRoot.EUIElement.ESlider || this.m_CurrentItem.m_Type == AccessibleUIGroupRoot.EUIElement.EToggle || this.m_CurrentItem.m_Type == AccessibleUIGroupRoot.EUIElement.ETextEdit || this.m_CurrentItem.m_Type == AccessibleUIGroupRoot.EUIElement.EDropDown) && !this.m_CurrentItem.m_Object.IsInteractable())
		{
			this.SayPause(this.m_DisabledDelay);
			this.SayAudio(this.m_DisabledAsAudio, UAP_AccessibilityManager.Localize_Internal("ElementDisabled"), UAP_AudioQueue.EAudioType.Element_Hint, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
		}
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x0005070C File Offset: 0x0004E90C
	private void ReadType()
	{
		if (this.m_CurrentItem == null)
		{
			return;
		}
		switch (this.m_CurrentItem.m_Type)
		{
		case AccessibleUIGroupRoot.EUIElement.EButton:
			this.SayAudio(this.m_ButtonAsAudio, UAP_AccessibilityManager.Localize_Internal("Element_Button"), UAP_AudioQueue.EAudioType.Element_Type, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
			return;
		case AccessibleUIGroupRoot.EUIElement.ELabel:
			break;
		case AccessibleUIGroupRoot.EUIElement.EToggle:
			this.SayAudio(this.m_ToggleAsAudio, UAP_AccessibilityManager.Localize_Internal("Element_Toggle"), UAP_AudioQueue.EAudioType.Element_Type, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
			return;
		case AccessibleUIGroupRoot.EUIElement.ESlider:
			this.SayAudio(this.m_SliderAsAudio, UAP_AccessibilityManager.Localize_Internal("Element_Slider"), UAP_AudioQueue.EAudioType.Element_Type, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
			return;
		case AccessibleUIGroupRoot.EUIElement.ETextEdit:
			this.SayAudio(this.m_TextEditAsAudio, UAP_AccessibilityManager.Localize_Internal("Element_TextEdit"), UAP_AudioQueue.EAudioType.Element_Type, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
			return;
		case AccessibleUIGroupRoot.EUIElement.EDropDown:
			this.SayAudio(this.m_DropDownAsAudio, UAP_AccessibilityManager.Localize_Internal("Element_Dropdown"), UAP_AudioQueue.EAudioType.Element_Type, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
			break;
		default:
			return;
		}
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x00050820 File Offset: 0x0004EA20
	private void ReadValue(bool allowPause = true, bool interrupt = false)
	{
		if (this.m_CurrentItem == null)
		{
			return;
		}
		AudioClip currentValueAsAudio = this.m_CurrentItem.m_Object.GetCurrentValueAsAudio();
		string currentValueAsText = this.m_CurrentItem.m_Object.GetCurrentValueAsText();
		if (currentValueAsAudio != null || (currentValueAsText != null && currentValueAsText.Length > 0))
		{
			if (allowPause)
			{
				this.SayPause(UAP_AccessibilityManager.instance.m_ValueDelay);
			}
			this.SayAudio(currentValueAsAudio, currentValueAsText, UAP_AudioQueue.EAudioType.Element_Text, this.m_CurrentItem.m_Object.m_AllowVoiceOver, interrupt ? UAP_AudioQueue.EInterrupt.Elements : UAP_AudioQueue.EInterrupt.None);
		}
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x000508A4 File Offset: 0x0004EAA4
	private void ReadHint()
	{
		if (this.m_CurrentItem == null)
		{
			return;
		}
		if (this.m_CurrentItem.m_Object.m_CustomHint)
		{
			this.SayAudio(this.m_CurrentItem.m_Object.m_HintAsAudio, this.m_CurrentItem.m_Object.GetCustomHint(), UAP_AudioQueue.EAudioType.Element_Hint, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
			return;
		}
		switch (this.m_CurrentItem.m_Type)
		{
		case AccessibleUIGroupRoot.EUIElement.EButton:
			if (Application.isMobilePlatform)
			{
				this.SayAudio(null, UAP_AccessibilityManager.Localize_Internal("Mobile_HintButton"), UAP_AudioQueue.EAudioType.Element_Hint, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
				return;
			}
			this.SayAudio(null, UAP_AccessibilityManager.Localize_Internal("Desktop_HintButton"), UAP_AudioQueue.EAudioType.Element_Hint, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
			return;
		case AccessibleUIGroupRoot.EUIElement.ELabel:
			break;
		case AccessibleUIGroupRoot.EUIElement.EToggle:
			if (Application.isMobilePlatform)
			{
				this.SayAudio(null, UAP_AccessibilityManager.Localize_Internal("Mobile_HintToggle"), UAP_AudioQueue.EAudioType.Element_Hint, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
				return;
			}
			this.SayAudio(null, UAP_AccessibilityManager.Localize_Internal("Desktop_HintToggle"), UAP_AudioQueue.EAudioType.Element_Hint, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
			return;
		case AccessibleUIGroupRoot.EUIElement.ESlider:
			if (Application.isMobilePlatform)
			{
				this.SayAudio(null, UAP_AccessibilityManager.Localize_Internal("Mobile_HintSlider"), UAP_AudioQueue.EAudioType.Element_Hint, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
				return;
			}
			this.SayAudio(null, UAP_AccessibilityManager.Localize_Internal("Desktop_HintSlider"), UAP_AudioQueue.EAudioType.Element_Hint, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
			break;
		case AccessibleUIGroupRoot.EUIElement.ETextEdit:
			if (Application.isMobilePlatform)
			{
				this.SayAudio(null, UAP_AccessibilityManager.Localize_Internal("Mobile_HintTextEdit"), UAP_AudioQueue.EAudioType.Element_Hint, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
				return;
			}
			this.SayAudio(null, UAP_AccessibilityManager.Localize_Internal("Desktop_HintTextEdit"), UAP_AudioQueue.EAudioType.Element_Hint, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
			return;
		case AccessibleUIGroupRoot.EUIElement.EDropDown:
			if (Application.isMobilePlatform)
			{
				this.SayAudio(null, UAP_AccessibilityManager.Localize_Internal("Mobile_HintDropdown"), UAP_AudioQueue.EAudioType.Element_Hint, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
				return;
			}
			this.SayAudio(null, UAP_AccessibilityManager.Localize_Internal("Desktop_HintDropdown"), UAP_AudioQueue.EAudioType.Element_Hint, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x00050AC7 File Offset: 0x0004ECC7
	private static void SpeakElement_Text(ref AccessibleUIGroupRoot.Accessible_UIElement element)
	{
		UAP_AccessibilityManager.instance.SayAudio(element.m_Object.m_TextAsAudio, element.m_Object.GetTextToRead(), UAP_AudioQueue.EAudioType.Element_Hint, element.AllowsVoiceOver(), UAP_AudioQueue.EInterrupt.Elements);
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x00050AF8 File Offset: 0x0004ECF8
	public static void PauseAccessibility(bool pause, bool forceRepeatCurrentItem = false)
	{
		if (UAP_AccessibilityManager.isDestroyed)
		{
			return;
		}
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager.StopContinuousReading();
		if (UAP_AccessibilityManager.m_Paused == pause)
		{
			return;
		}
		UAP_AccessibilityManager.m_Paused = pause;
		UAP_AccessibilityManager.instance.m_AudioQueue.Stop();
		UAP_AccessibilityManager.instance.m_AudioPlayer.Stop();
		UAP_AccessibilityManager.instance.EnableTouchBlocker(!pause);
		if (pause)
		{
			UAP_AccessibilityManager.HideElementFrame();
		}
		if (!pause && forceRepeatCurrentItem)
		{
			UAP_AccessibilityManager.instance.m_PreviousItem = null;
			UAP_AccessibilityManager.instance.m_CurrentItem = null;
			UAP_AccessibilityManager.instance.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.Internal, false);
			UAP_AccessibilityManager.ReadItem(UAP_AccessibilityManager.instance.m_CurrentItem, false);
			return;
		}
		UAP_AccessibilityManager.instance.m_PreviousItem = UAP_AccessibilityManager.instance.m_CurrentItem;
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x00050BA9 File Offset: 0x0004EDA9
	private static void StopContinuousReading()
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager.instance.m_ContinuousReading = false;
		UAP_AccessibilityManager.instance.m_ContinuousReading_WaitInputClear = false;
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x00050BC6 File Offset: 0x0004EDC6
	private void EnableTouchBlocker(bool enable)
	{
		if (!enable || !UAP_AccessibilityManager.m_IsEnabled)
		{
			this.m_TouchBlocker.SetActive(false);
			return;
		}
		if (!UAP_AccessibilityManager.m_Paused)
		{
			this.m_TouchBlocker.SetActive(this.m_HandleUI);
		}
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x00050BF7 File Offset: 0x0004EDF7
	public static void BlockInput(bool block, bool stopSpeakingOnBlock = true)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager.StopContinuousReading();
		UAP_AccessibilityManager.instance.m_BlockInput = block;
		if (block && stopSpeakingOnBlock)
		{
			UAP_AccessibilityManager.StopSpeaking();
		}
	}

	// Token: 0x06000C39 RID: 3129 RVA: 0x00050C18 File Offset: 0x0004EE18
	public static void ElementRemoved(AccessibleUIGroupRoot.Accessible_UIElement element)
	{
		if (UAP_AccessibilityManager.instance == null || UAP_AccessibilityManager.isDestroyed)
		{
			return;
		}
		if (element != UAP_AccessibilityManager.instance.m_CurrentItem || element == null)
		{
			return;
		}
		if (UAP_AccessibilityManager.instance.m_PreviousItem == element)
		{
			UAP_AccessibilityManager.instance.m_PreviousItem = null;
		}
		UAP_AccessibilityManager.instance.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.Internal, true);
		UAP_AccessibilityManager.ReadItem(UAP_AccessibilityManager.instance.m_CurrentItem, false);
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x00050C80 File Offset: 0x0004EE80
	public static void ActivateContainer(AccessibleUIGroupRoot container, bool activate)
	{
		if (UAP_AccessibilityManager.isDestroyed)
		{
			return;
		}
		UAP_AccessibilityManager.Initialize();
		if (activate)
		{
			UAP_AccessibilityManager.Log("Activating UI Container " + container.name);
		}
		else
		{
			UAP_AccessibilityManager.Log("Deactivating UI Container " + container.name);
		}
		if (activate)
		{
			if (UAP_AccessibilityManager.m_ContainersToActivate.Contains(container))
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < UAP_AccessibilityManager.m_ContainersToActivate.Count; i++)
			{
				if (container.m_Priority > UAP_AccessibilityManager.m_ContainersToActivate[i].m_Priority)
				{
					UAP_AccessibilityManager.m_ContainersToActivate.Insert(i, container);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				UAP_AccessibilityManager.m_ContainersToActivate.Add(container);
				return;
			}
		}
		else
		{
			if (UAP_AccessibilityManager.m_ContainersToActivate.Contains(container))
			{
				UAP_AccessibilityManager.m_ContainersToActivate.Remove(container);
			}
			UAP_AccessibilityManager.instance.ActivateContainer_Internal(container, false, false);
			UAP_AccessibilityManager.instance.m_ReadItemNextUpdate = !UAP_AccessibilityManager.instance.m_BlockInput;
		}
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x00050D68 File Offset: 0x0004EF68
	private AccessibleUIGroupRoot GetActivePopup()
	{
		foreach (AccessibleUIGroupRoot accessibleUIGroupRoot in this.m_ActiveContainers)
		{
			if (accessibleUIGroupRoot.m_PopUp)
			{
				return accessibleUIGroupRoot;
			}
		}
		return null;
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x00050DC4 File Offset: 0x0004EFC4
	private void ActivateContainer_Internal(AccessibleUIGroupRoot container, bool activate, bool readCurrentItem = true)
	{
		if (container == null)
		{
			return;
		}
		if (activate)
		{
			if (this.m_ActiveContainers.Contains(container))
			{
				return;
			}
			if (container.m_PopUp)
			{
				if (UAP_AccessibilityManager.m_IsEnabled)
				{
					this.PlaySFX(this.m_UIPopUpOpen);
				}
				List<AccessibleUIGroupRoot> list = new List<AccessibleUIGroupRoot>();
				foreach (AccessibleUIGroupRoot accessibleUIGroupRoot in this.m_ActiveContainers)
				{
					list.Add(accessibleUIGroupRoot);
				}
				this.m_SuspendedContainers.Add(list);
				this.m_SuspendedActiveContainerIndex.Add(this.m_ActiveContainerIndex);
				this.m_ActiveContainers.Clear();
				this.SelectNothing(UAP_BaseElement.EHighlightSource.Internal);
			}
			else
			{
				AccessibleUIGroupRoot activePopup = this.GetActivePopup();
				if (activePopup != null && !activePopup.m_AllowExternalJoining && !container.transform.IsChildOf(activePopup.transform))
				{
					if (this.m_SuspendedContainers.Count > 0)
					{
						List<AccessibleUIGroupRoot> list2 = this.m_SuspendedContainers[this.m_SuspendedContainers.Count - 1];
						bool flag = false;
						for (int i = 0; i < list2.Count; i++)
						{
							if (container.m_Priority > list2[i].m_Priority)
							{
								list2.Insert(i, container);
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							list2.Add(container);
							return;
						}
					}
					else
					{
						List<AccessibleUIGroupRoot> list3 = new List<AccessibleUIGroupRoot>();
						list3.Add(container);
						this.m_SuspendedContainers.Add(list3);
						this.m_SuspendedActiveContainerIndex.Add(0);
					}
					return;
				}
			}
			bool flag2 = false;
			for (int j = 0; j < this.m_ActiveContainers.Count; j++)
			{
				if (container.m_Priority > this.m_ActiveContainers[j].m_Priority)
				{
					this.m_ActiveContainers.Insert(j, container);
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				this.m_ActiveContainers.Add(container);
			}
			if (this.m_ActiveContainers.Count == 1)
			{
				this.m_ActiveContainerIndex = 0;
				this.ReadContainerName();
				this.m_CurrentItem = this.m_ActiveContainers[0].GetCurrentElement(this.m_CyclicMenus);
				this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.Internal, false);
				if (this.m_ActiveContainers[0] == container && container.m_AutoRead)
				{
					UAP_AccessibilityManager.ReadFromTop();
					return;
				}
				if (readCurrentItem)
				{
					UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, false);
					return;
				}
			}
			else if (this.m_CurrentItem == null)
			{
				this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.Internal, false);
				if (this.m_ActiveContainers[this.m_ActiveContainerIndex] == container && container.m_AutoRead)
				{
					UAP_AccessibilityManager.ReadFromTop();
					return;
				}
				if (readCurrentItem)
				{
					UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, false);
					return;
				}
			}
		}
		else
		{
			if (!this.m_ActiveContainers.Contains(container))
			{
				for (int k = 0; k < this.m_SuspendedContainers.Count; k++)
				{
					List<AccessibleUIGroupRoot> list4 = this.m_SuspendedContainers[k];
					int l = 0;
					while (l < list4.Count)
					{
						if (list4[l] == container)
						{
							list4.Remove(container);
							if (this.m_SuspendedActiveContainerIndex[k] == l)
							{
								this.m_SuspendedActiveContainerIndex[k] = 0;
							}
							if (list4.Count == 0)
							{
								this.m_SuspendedContainers.Remove(list4);
								this.m_SuspendedActiveContainerIndex.RemoveAt(k);
								break;
							}
							break;
						}
						else
						{
							l++;
						}
					}
				}
				return;
			}
			if (container.m_PopUp)
			{
				if (UAP_AccessibilityManager.m_IsEnabled)
				{
					this.PlaySFX(this.m_UIPopUpClose);
				}
				this.m_ActiveContainers.Clear();
				this.m_ActiveContainerIndex = -1;
				if (this.m_SuspendedContainers.Count > 0)
				{
					foreach (AccessibleUIGroupRoot accessibleUIGroupRoot2 in this.m_SuspendedContainers[this.m_SuspendedContainers.Count - 1])
					{
						this.m_ActiveContainers.Add(accessibleUIGroupRoot2);
					}
					this.m_SuspendedContainers.RemoveAt(this.m_SuspendedContainers.Count - 1);
					this.m_ActiveContainerIndex = this.m_SuspendedActiveContainerIndex[this.m_SuspendedActiveContainerIndex.Count - 1];
					this.m_SuspendedActiveContainerIndex.RemoveAt(this.m_SuspendedActiveContainerIndex.Count - 1);
				}
				if (this.m_ActiveContainers.Count == 0)
				{
					this.m_ActiveContainerIndex = -1;
				}
			}
			else
			{
				int num = this.m_ActiveContainers.IndexOf(container);
				if (num == this.m_ActiveContainerIndex)
				{
					this.m_ActiveContainerIndex = this.m_ActiveContainers.Count - (num + 2);
				}
				else if (num < this.m_ActiveContainerIndex)
				{
					this.m_ActiveContainerIndex--;
				}
				this.m_ActiveContainers.Remove(container);
			}
			if (this.m_ActiveContainerIndex < 0)
			{
				this.m_CurrentItem = null;
				this.m_PreviousItem = null;
				return;
			}
			this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.Internal, false);
			if (readCurrentItem)
			{
				UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, false);
			}
		}
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x000512B8 File Offset: 0x0004F4B8
	public static void ResetCurrentContainerFocus()
	{
		if (UAP_AccessibilityManager.instance == null || !UAP_AccessibilityManager.IsActive() || UAP_AccessibilityManager.instance.m_ActiveContainerIndex < 0)
		{
			return;
		}
		if (UAP_AccessibilityManager.instance.m_ActiveContainers.Count > UAP_AccessibilityManager.instance.m_ActiveContainerIndex)
		{
			UAP_AccessibilityManager.instance.m_ActiveContainers[UAP_AccessibilityManager.instance.m_ActiveContainerIndex].ResetToStart();
		}
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x00051320 File Offset: 0x0004F520
	private void SelectNothing(UAP_BaseElement.EHighlightSource selectionSource)
	{
		this.m_ActiveContainerIndex = 0;
		if (this.m_PreviousItem != null && this.m_PreviousItem.m_Object != null)
		{
			this.m_PreviousItem.m_Object.HoverHighlight(false, selectionSource);
		}
		if (this.m_CurrentItem != null && this.m_CurrentItem != this.m_PreviousItem && this.m_CurrentItem.m_Object != null)
		{
			this.m_CurrentItem.m_Object.HoverHighlight(false, selectionSource);
		}
		this.m_CurrentItem = null;
		this.m_PreviousItem = null;
		UAP_AccessibilityManager.HideElementFrame();
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x000513B0 File Offset: 0x0004F5B0
	private void UpdateCurrentItem(UAP_BaseElement.EHighlightSource selectionSource, bool makeSureItemIsSelected = false)
	{
		this.m_CurrentItem = null;
		bool flag = UAP_AccessibilityManager.IsEnabled();
		if (this.m_ActiveContainers.Count == 0)
		{
			UAP_AccessibilityManager.Log("Nothing on screen to select");
			this.m_ActiveContainerIndex = -1;
			if (flag && this.m_PreviousItem != null && this.m_PreviousItem.m_Object != null)
			{
				this.m_PreviousItem.m_Object.HoverHighlight(false, selectionSource);
			}
			return;
		}
		int num = this.m_ActiveContainerIndex;
		if (num < 0)
		{
			num = 0;
		}
		for (int i = 0; i < this.m_ActiveContainers.Count; i++)
		{
			int j;
			for (j = i + num; j >= this.m_ActiveContainers.Count; j -= this.m_ActiveContainers.Count)
			{
			}
			if (this.m_ActiveContainers.Count > 0)
			{
				this.m_CurrentItem = this.m_ActiveContainers[j].GetCurrentElement(this.m_CyclicMenus);
			}
			else
			{
				this.m_CurrentItem = null;
			}
			if (this.m_CurrentItem == null)
			{
				UAP_AccessibilityManager.Log("Nothing selected");
				if (makeSureItemIsSelected)
				{
					this.DecrementUIElement();
					this.UpdateCurrentItem(selectionSource, false);
					return;
				}
			}
			if (this.m_CurrentItem != null)
			{
				this.m_ActiveContainerIndex = j;
				if (this.m_PreviousItem != this.m_CurrentItem && (this.m_PreviousItem == null || this.m_CurrentItem == null || this.m_PreviousItem.m_Object != this.m_CurrentItem.m_Object))
				{
					UAP_AccessibilityManager.Log(string.Concat(new string[]
					{
						"Selected item was updated",
						(this.m_CurrentItem != null) ? (" to " + this.m_CurrentItem.m_Object.GetTextToRead()) : " NONE",
						" (Frame: ",
						Time.frameCount.ToString(),
						") + previous item: ",
						(this.m_PreviousItem != null) ? this.m_PreviousItem.m_Object.GetTextToRead() : "null"
					}));
					if (flag)
					{
						if (this.m_PreviousItem != null && this.m_PreviousItem.m_Object != null)
						{
							this.m_PreviousItem.m_Object.HoverHighlight(false, selectionSource);
						}
						if (this.m_CurrentItem != null && this.m_CurrentItem.m_Object != null)
						{
							this.m_CurrentItem.m_Object.HoverHighlight(true, selectionSource);
						}
					}
				}
				this.m_PreviousItem = this.m_CurrentItem;
				return;
			}
		}
	}

	// Token: 0x06000C40 RID: 3136 RVA: 0x00051600 File Offset: 0x0004F800
	public static bool IsEnabled()
	{
		if (UAP_AccessibilityManager.isDestroyed)
		{
			return false;
		}
		UAP_AccessibilityManager.Initialize();
		return UAP_AccessibilityManager.m_IsEnabled;
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x00051618 File Offset: 0x0004F818
	public static string GetInstanceID()
	{
		if (UAP_AccessibilityManager.instance == null)
		{
			return "Not initialized";
		}
		return UAP_AccessibilityManager.instance.gameObject.GetInstanceID().ToString("0");
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x00051654 File Offset: 0x0004F854
	public static bool IsActive()
	{
		if (UAP_AccessibilityManager.isDestroyed)
		{
			return false;
		}
		UAP_AccessibilityManager.Initialize();
		return UAP_AccessibilityManager.m_IsEnabled && !UAP_AccessibilityManager.m_Paused;
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x00051675 File Offset: 0x0004F875
	public static bool IsCurrentPlatformSupported()
	{
		return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor;
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x0005169B File Offset: 0x0004F89B
	public static void EnableMagicGestures(bool enable)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager.instance.m_HandleMagicGestures = enable;
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x000516AD File Offset: 0x0004F8AD
	public static bool IsMagicGesturesEnabled()
	{
		return !(UAP_AccessibilityManager.instance == null) && UAP_AccessibilityManager.instance.m_HandleMagicGestures;
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x000516C8 File Offset: 0x0004F8C8
	public static void EnableAccessibility(bool enable, bool readNotification = false)
	{
		if (UAP_AccessibilityManager.isDestroyed)
		{
			return;
		}
		if (enable == UAP_AccessibilityManager.m_IsEnabled)
		{
			return;
		}
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager.StopContinuousReading();
		if (enable)
		{
			if (PlayerPrefs.GetInt("Accessibility_FirstTimeStartup", 1) == 1)
			{
				if (Application.isMobilePlatform)
				{
					UAP_AccessibilityManager.instance.SayAudio(null, UAP_AccessibilityManager.Localize_Internal("Mobile_General"), UAP_AudioQueue.EAudioType.App, true, UAP_AudioQueue.EInterrupt.All);
				}
				else
				{
					UAP_AccessibilityManager.instance.SayAudio(null, UAP_AccessibilityManager.Localize_Internal("Desktop_General"), UAP_AudioQueue.EAudioType.App, true, UAP_AudioQueue.EInterrupt.All);
				}
				PlayerPrefs.SetInt("Accessibility_FirstTimeStartup", 0);
				PlayerPrefs.Save();
				readNotification = false;
			}
			bool isEnabled = UAP_AccessibilityManager.m_IsEnabled;
			UAP_AccessibilityManager.m_IsEnabled = true;
			UAP_AccessibilityManager.m_Paused = false;
			UAP_AccessibilityManager.instance.OnEnable();
			if (readNotification)
			{
				UAP_AccessibilityManager.instance.Say_Internal(UAP_AccessibilityManager.Localize_Internal("EnabledAccessibility"), false, true, UAP_AudioQueue.EInterrupt.All);
			}
			if (!isEnabled)
			{
				UAP_AccessibilityManager.instance.StartScreenOver();
			}
		}
		else
		{
			UAP_AccessibilityManager.StopSpeaking();
			if (readNotification)
			{
				UAP_AccessibilityManager.instance.Say_Internal(UAP_AccessibilityManager.Localize_Internal("DisabledAccessibility"), false, true, UAP_AudioQueue.EInterrupt.All);
			}
			UAP_AccessibilityManager.HideElementFrame();
			UAP_AccessibilityManager.m_IsEnabled = false;
			UAP_AccessibilityManager.instance.OnDisable();
		}
		UAP_AccessibilityManager.SavePluginEnabledState();
		if (UAP_AccessibilityManager.instance.m_OnAccessibilityModeChanged != null)
		{
			UAP_AccessibilityManager.instance.m_OnAccessibilityModeChanged(enable);
		}
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x000517F0 File Offset: 0x0004F9F0
	private void OnEnable()
	{
		this.EnableTouchBlocker(true);
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x000517F9 File Offset: 0x0004F9F9
	private void OnDisable()
	{
		if (this.m_AudioQueue != null)
		{
			this.m_AudioQueue.Stop();
		}
		if (this.m_TouchBlocker != null)
		{
			this.EnableTouchBlocker(false);
		}
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x0005182C File Offset: 0x0004FA2C
	private void UpdateMagicTapDetection()
	{
		bool flag = false;
		if (this.m_WaitingForMagicTap && Time.unscaledTime > this.m_MagicTap_LastTapTime + 0.2f)
		{
			this.m_WaitingForMagicTap = false;
			if (this.m_OnTwoFingerSingleTapCallbacks != null)
			{
				this.m_OnTwoFingerSingleTapCallbacks();
			}
		}
		int touchCount = this.GetTouchCount();
		if (this.m_MagicTap_TouchCountHelper < 2 && touchCount == 2)
		{
			if (Time.unscaledTime < this.m_MagicTap_LastTapTime + 0.2f)
			{
				flag = true;
				this.m_WaitingForMagicTap = false;
			}
			else
			{
				this.m_WaitingForMagicTap = true;
			}
			this.m_MagicTap_LastTapTime = Time.unscaledTime;
		}
		this.m_MagicTap_TouchCountHelper = touchCount;
		if (flag)
		{
			this.m_MagicTap_LastTapTime = -1f;
			if (this.m_HandleMagicGestures && this.m_OnPauseToggleCallbacks != null)
			{
				this.m_OnPauseToggleCallbacks();
			}
		}
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x000518E4 File Offset: 0x0004FAE4
	private void UpdateThreeFingerTapDetection()
	{
		if (this.m_WaitingForThreeFingerTap && Time.unscaledTime > this.m_TripleTap_LastTapTime + 0.2f)
		{
			this.m_WaitingForThreeFingerTap = false;
			if (this.m_TripleTap_Count == 1)
			{
				if (UAP_AccessibilityManager.m_IsEnabled && this.m_OnThreeFingerSingleTapCallbacks != null)
				{
					this.m_OnThreeFingerSingleTapCallbacks();
				}
			}
			else if (this.m_TripleTap_Count == 2 && UAP_AccessibilityManager.m_IsEnabled && this.m_OnThreeFingerDoubleTapCallbacks != null)
			{
				this.m_OnThreeFingerDoubleTapCallbacks();
			}
			this.m_TripleTap_Count = 0;
		}
		int touchCount = this.GetTouchCount();
		bool flag = false;
		if (this.m_TripleTap_TouchCountHelper < 3 && touchCount == 3)
		{
			if (Time.unscaledTime < this.m_TripleTap_LastTapTime + 0.2f)
			{
				this.m_TripleTap_Count++;
				if (this.m_TripleTap_Count == 3)
				{
					this.m_WaitingForThreeFingerTap = false;
					flag = true;
				}
			}
			else
			{
				this.m_WaitingForThreeFingerTap = true;
				this.m_TripleTap_Count = 1;
			}
			this.m_TripleTap_LastTapTime = Time.unscaledTime;
			if (this.m_DebugOutputLabel != null && UAP_AccessibilityManager.instance.m_DebugOutput)
			{
				this.m_DebugOutputLabel.text = "Three finger touch - Count: " + this.m_TripleTap_Count.ToString();
			}
		}
		this.m_TripleTap_TouchCountHelper = touchCount;
		if (flag)
		{
			if (this.m_DebugOutputLabel != null && UAP_AccessibilityManager.instance.m_DebugOutput)
			{
				this.m_DebugOutputLabel.text = "Three finger triple tap detected.";
			}
			this.m_TripleTap_LastTapTime = -1f;
			this.m_TripleTap_Count = 0;
			if (this.m_HandleMagicGestures)
			{
				UAP_AccessibilityManager.ToggleAccessibility();
			}
		}
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x00051A59 File Offset: 0x0004FC59
	public static void ToggleAccessibility()
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager.instance.ToggleAccessibility_Internal();
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x00051A6C File Offset: 0x0004FC6C
	private void ToggleAccessibility_Internal()
	{
		bool flag = UAP_AccessibilityManager.IsEnabled();
		if (!flag)
		{
			UAP_AccessibilityManager.instance.Say_Internal(UAP_AccessibilityManager.Localize_Internal("EnabledAccessibility"), false, true, UAP_AudioQueue.EInterrupt.All);
		}
		UAP_AccessibilityManager.EnableAccessibility(!flag, false);
		if (flag)
		{
			this.Say_Internal(UAP_AccessibilityManager.Localize_Internal("DisabledAccessibility"), false, true, UAP_AudioQueue.EInterrupt.All);
		}
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x00051ABC File Offset: 0x0004FCBC
	private void Update()
	{
		this.UpdateContainerActivations();
		this.UpdateThreeFingerTapDetection();
		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.LeftWindows) || Input.GetKey(KeyCode.LeftMeta)))
		{
			UAP_AccessibilityManager.ToggleAccessibility();
		}
		if (!UAP_AccessibilityManager.m_IsEnabled)
		{
			return;
		}
		if (this.m_LastUpdateTouchCount == 0)
		{
			int touchCount = this.GetTouchCount();
			if (touchCount > 1)
			{
				this.m_AudioQueue.StopAllInterruptibles();
			}
			else if (touchCount == 1)
			{
				this.m_AudioQueue.InterruptAppAnnouncement();
			}
		}
		if (this.m_HandleMagicGestures)
		{
			this.HandlePauseGestures();
		}
		if (UAP_AccessibilityManager.m_Paused)
		{
			return;
		}
		if (this.m_WindowsUseMouseSwipes)
		{
			this.UpdateScrubDetection();
			this.UpdateSwipeDetection();
		}
		if (this.m_ContinuousReading && !this.m_ContinuousReading_WaitInputClear && Input.anyKey)
		{
			UAP_AccessibilityManager.StopContinuousReading();
		}
		if (this.m_ContinuousReading_WaitInputClear && !Input.anyKey)
		{
			this.m_ContinuousReading_WaitInputClear = false;
		}
		if (this.HandleUI())
		{
			if (this.m_ContinuousReading)
			{
				this.UpdateContinuousReading();
			}
			if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer && !this.m_CurrentElementHasSoleFocus && EventSystem.current != null)
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
			if (this.m_CurrentElementHasSoleFocus)
			{
				AccessibleUIGroupRoot.EUIElement type = this.m_CurrentItem.m_Type;
			}
			if (this.m_CurrentItem != null)
			{
				UAP_AccessibilityManager.UpdateElementFrame(ref this.m_CurrentItem);
			}
			else
			{
				UAP_AccessibilityManager.HideElementFrame();
			}
			if (this.m_WindowsUseMouseSwipes)
			{
				this.UpdateDoubleTapDetection();
			}
			if ((this.m_ExploreByTouch && !this.m_CurrentElementHasSoleFocus) || UAP_VirtualKeyboard.IsOpen())
			{
				this.UpdateExploreByTouch2();
			}
			if (this.m_WindowsUseKeys)
			{
				this.UpdateKeyboardInput();
			}
		}
		this.m_LastUpdateTouchCount = this.GetTouchCount();
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x00051C5C File Offset: 0x0004FE5C
	private void UpdateContinuousReading()
	{
		if (this.m_AudioQueue.IsCompletelyEmpty())
		{
			int num = this.m_ActiveContainerIndex;
			int activeContainerIndex = this.m_ActiveContainerIndex;
			if (!this.m_ActiveContainers[num].IncrementCurrentItem(false))
			{
				if (num == this.m_ActiveContainers.Count - 1)
				{
					UAP_AccessibilityManager.StopContinuousReading();
				}
				else
				{
					bool flag;
					do
					{
						num++;
						if (num < this.m_ActiveContainers.Count)
						{
							this.m_ActiveContainers[num].JumpToFirst();
							flag = this.m_ActiveContainers[num].GetCurrentElementIndex() < 0;
						}
						else
						{
							flag = false;
						}
					}
					while (flag);
					if (num == this.m_ActiveContainers.Count - 1 && this.m_ActiveContainers[num].GetCurrentElementIndex() < 0)
					{
						UAP_AccessibilityManager.StopContinuousReading();
					}
					else
					{
						this.m_ActiveContainerIndex = num;
					}
				}
			}
			if (this.m_ContinuousReading)
			{
				this.m_CurrentItem = this.m_ActiveContainers[this.m_ActiveContainerIndex].GetCurrentElement(false);
				if (activeContainerIndex != this.m_ActiveContainerIndex)
				{
					this.ReadContainerName();
				}
				this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.Internal, false);
				UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, true);
			}
		}
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x00051D70 File Offset: 0x0004FF70
	private void HandlePauseGestures()
	{
		if (this.m_WindowsUseMouseSwipes)
		{
			this.UpdateMagicTapDetection();
		}
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x00051D80 File Offset: 0x0004FF80
	private bool TouchExploreMinDistanceReach(float movedDistance, float moveTime)
	{
		float num = Screen.dpi * 0.75f * (moveTime / 0.25f);
		if (num == 0f)
		{
			num = (float)Mathf.Min(Screen.width, Screen.height) * 0.1f;
		}
		return movedDistance <= num;
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x00051DC8 File Offset: 0x0004FFC8
	private void UpdateExploreByTouch2()
	{
		if (!this.HandleUI())
		{
			return;
		}
		if (this.GetTouchCount() > 1)
		{
			this.m_TouchExplore_Active = false;
			this.m_TouchExplore_CheckVelocityTimer = -1f;
			this.m_TouchExplore_WaitForDoubleTapToExpireTimer = -1f;
			return;
		}
		if (!this.m_TouchExplore_Active)
		{
			if (this.m_TouchExplore_WaitForDoubleTapToExpireTimer > 0f)
			{
				if (this.GetTouchCount() != 0 || this.m_DoubleTapFoundThisFrame)
				{
					this.m_TouchExplore_Active = false;
					this.m_TouchExplore_CheckVelocityTimer = -1f;
					this.m_TouchExplore_WaitForDoubleTapToExpireTimer = -1f;
					return;
				}
				this.m_TouchExplore_WaitForDoubleTapToExpireTimer -= Time.unscaledDeltaTime;
				if (this.m_TouchExplore_WaitForDoubleTapToExpireTimer <= 0f)
				{
					this.ExploreByTouch_SelectElementUnderFinger(this.GetTouchPosition());
					this.m_SwipeActive = false;
					this.m_SwipeWaitForLift = true;
				}
			}
			else if (this.m_TouchExplore_CheckVelocityTimer > 0f)
			{
				this.m_TouchExplore_CheckVelocityTimer -= Time.unscaledDeltaTime;
				if (this.m_TouchExplore_CheckVelocityTimer <= 0f || this.GetTouchCount() == 0)
				{
					float num = 0.15f - this.m_TouchExplore_CheckVelocityTimer;
					this.m_TouchExplore_CheckVelocityTimer = -1f;
					Vector3 vector = this.GetTouchPosition() - this.m_TouchExplore_CheckStartPosition;
					float magnitude = vector.magnitude;
					if (this.TouchExploreMinDistanceReach(vector.magnitude, num))
					{
						this.m_TouchExplore_WaitForDoubleTapToExpireTimer = 0.2f - num;
						if (this.GetTouchCount() == 0 && this.m_TouchExplore_WaitForDoubleTapToExpireTimer > 0f)
						{
							this.m_TouchExplore_CheckVelocityTimer = -1f;
							return;
						}
						this.m_TouchExplore_Active = true;
						this.ExploreByTouch_SelectElementUnderFinger(this.GetTouchPosition());
						this.m_SwipeActive = false;
						this.m_SwipeWaitForLift = true;
					}
				}
			}
			if (this.m_LastUpdateTouchCount == 0 && this.GetTouchCount() == 1)
			{
				this.m_TouchExplore_CheckVelocityTimer = 0.15f;
				this.m_TouchExplore_CheckStartPosition = this.GetTouchPosition();
				this.m_TouchExplore_Active = false;
			}
			return;
		}
		if (this.GetTouchCount() != 1)
		{
			this.m_TouchExplore_Active = false;
			this.m_TouchExplore_CheckVelocityTimer = -1f;
			this.m_TouchExplore_WaitForDoubleTapToExpireTimer = -1f;
			return;
		}
		this.ExploreByTouch_SelectElementUnderFinger(this.GetTouchPosition());
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x00051FB8 File Offset: 0x000501B8
	private void UpdateExploreByTouch()
	{
		if (!this.HandleUI())
		{
			this.CancelExploreByTouch();
			this.m_ExploreByTouch_SingleTapWaitTimer = -1f;
			return;
		}
		bool flag = true;
		if (!this.WindowsUseExploreByTouch)
		{
			flag = false;
		}
		if (!flag)
		{
			this.CancelExploreByTouch();
			this.m_ExploreByTouch_SingleTapWaitTimer = -1f;
			return;
		}
		bool flag2 = this.GetTouchCount() == 1;
		if (this.m_ExploreByTouch_IsActive)
		{
			if (!flag2)
			{
				this.CancelExploreByTouch();
				this.m_ExploreByTouch_SingleTapWaitTimer = -1f;
				return;
			}
			Vector3 touchPosition = this.GetTouchPosition();
			Vector3 vector = new Vector2(touchPosition.x, touchPosition.y);
			if (this.m_DebugOutputLabel != null && UAP_AccessibilityManager.instance.m_DebugOutput)
			{
				this.m_DebugOutputLabel.text = "Explore by Touch - Drag active";
			}
			this.ExploreByTouch_SelectElementUnderFinger(vector);
			return;
		}
		else
		{
			if (flag2)
			{
				if (this.m_ExploreByTouch_SingleTapWaitTimer > 0f)
				{
					this.m_ExploreByTouch_SingleTapWaitTimer -= Time.unscaledDeltaTime;
				}
				else if (Input.GetMouseButtonDown(0))
				{
					this.m_ExploreByTouch_SingleTapWaitTimer = 0.2f;
					this.m_ExploreByTouch_SingleTapStartPosition = this.GetTouchPosition();
				}
			}
			else if (this.m_ExploreByTouch_SingleTapWaitTimer > 0f)
			{
				this.m_ExploreByTouch_SingleTapWaitTimer -= Time.unscaledDeltaTime;
				if (this.m_ExploreByTouch_SingleTapWaitTimer < 0f)
				{
					Vector3 vector2 = this.GetTouchPosition() - this.m_ExploreByTouch_SingleTapStartPosition;
					float num = Screen.dpi * 0.2f;
					float num2 = 0.5f * (float)(Screen.width + Screen.height) * 0.1f;
					if (num == 0f || num > num2)
					{
						num = num2;
					}
					if (vector2.sqrMagnitude < num * num)
					{
						Vector3 touchPosition2 = this.GetTouchPosition();
						if (this.m_DebugOutputLabel != null && UAP_AccessibilityManager.instance.m_DebugOutput)
						{
							Text debugOutputLabel = this.m_DebugOutputLabel;
							string[] array = new string[9];
							array[0] = "Explore by Touch Single Tap \nFinger Delta: ";
							array[1] = vector2.magnitude.ToString("000.00000");
							array[2] = " on a screen with ";
							array[3] = Screen.dpi.ToString();
							array[4] = " dpi. (Start: ";
							int num3 = 5;
							Vector3 vector3 = this.m_ExploreByTouch_StartPosition;
							array[num3] = vector3.ToString();
							array[6] = " End: ";
							int num4 = 7;
							vector3 = touchPosition2;
							array[num4] = vector3.ToString();
							array[8] = ")";
							debugOutputLabel.text = string.Concat(array);
						}
						Vector3 vector4 = new Vector2(touchPosition2.x, touchPosition2.y);
						this.m_CurrentItem = null;
						this.ExploreByTouch_SelectElementUnderFinger(vector4);
					}
				}
			}
			if (this.m_ExploreByTouch_WaitTimer < 0f)
			{
				this.m_ExploreByTouch_WaitTimer = this.m_ExploreByTouchDelay;
				this.m_ExploreByTouch_StartPosition = this.GetTouchPosition();
				return;
			}
			if (!flag2)
			{
				this.CancelExploreByTouch();
				return;
			}
			this.m_ExploreByTouch_WaitTimer -= Time.unscaledDeltaTime;
			if (this.m_ExploreByTouch_WaitTimer < 0f)
			{
				this.m_ExploreByTouch_SingleTapWaitTimer = -1f;
				Vector3 touchPosition3 = this.GetTouchPosition();
				float magnitude = (touchPosition3 - this.m_ExploreByTouch_StartPosition).magnitude;
				float num5 = magnitude / 0.2f;
				if (num5 < 600f)
				{
					if (this.m_DebugOutputLabel != null && UAP_AccessibilityManager.instance.m_DebugOutput)
					{
						this.m_DebugOutputLabel.text = "Explore by Touch. Ratio: " + num5.ToString("0.#####") + " distance: " + magnitude.ToString("0.####");
					}
					this.m_ExploreByTouch_IsActive = true;
					this.m_CurrentItem = null;
					Vector3 vector5 = new Vector2(touchPosition3.x, touchPosition3.y);
					this.ExploreByTouch_SelectElementUnderFinger(vector5);
					return;
				}
				this.CancelExploreByTouch();
			}
			return;
		}
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x00052354 File Offset: 0x00050554
	private void ExploreByTouch_SelectElementUnderFinger(Vector3 fingerPos)
	{
		bool flag = false;
		if (this.m_CurrentItem != null && this.IsPositionOverElement(fingerPos, this.m_CurrentItem))
		{
			return;
		}
		for (int i = 0; i < this.m_ActiveContainers.Count; i++)
		{
			List<AccessibleUIGroupRoot.Accessible_UIElement> elements = this.m_ActiveContainers[i].GetElements();
			int j = 0;
			while (j < elements.Count)
			{
				if (elements[j].m_Object.IsElementActive() && this.IsPositionOverElement(fingerPos, elements[j]))
				{
					flag = true;
					if (this.m_ActiveContainers[i].m_AllowTouchExplore)
					{
						this.m_CurrentItem = elements[j];
						this.m_ActiveContainerIndex = i;
						this.m_ActiveContainers[i].SetActiveElementIndex(j, this.m_CyclicMenus);
						this.PlaySFX(this.m_UINavigationClick);
						this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.TouchExplore, false);
						UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, false);
						break;
					}
					if (this.m_ActiveContainerIndex != i)
					{
						this.m_ActiveContainerIndex = i;
						this.ReadContainerName();
						this.m_CurrentItem = this.m_ActiveContainers[i].GetCurrentElement(this.m_CyclicMenus);
						this.PlaySFX(this.m_UINavigationClick);
						this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.TouchExplore, false);
						UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, false);
						break;
					}
					break;
				}
				else
				{
					j++;
				}
			}
			if (flag)
			{
				break;
			}
		}
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x000524B0 File Offset: 0x000506B0
	private void ReadContainerName()
	{
		if (!UAP_AccessibilityManager.m_IsEnabled)
		{
			return;
		}
		string containerName = this.m_ActiveContainers[this.m_ActiveContainerIndex].GetContainerName(false);
		if (containerName.Length > 0)
		{
			this.SayAudio(null, containerName, UAP_AudioQueue.EAudioType.Container_Name, true, null, UAP_AudioQueue.EInterrupt.None);
		}
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x000524F3 File Offset: 0x000506F3
	private void CancelExploreByTouch()
	{
		this.m_ExploreByTouch_IsActive = false;
		this.m_ExploreByTouch_WaitTimer = -1f;
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x00052508 File Offset: 0x00050708
	private void UpdateDoubleTapDetection()
	{
		this.m_DoubleTapFoundThisFrame = false;
		int touchCount = this.GetTouchCount();
		if (Input.GetMouseButtonDown(0) && touchCount == 1)
		{
			if (Time.unscaledTime < this.m_DoubleTap_LastTapTime + 0.2f)
			{
				this.m_DoubleTapFoundThisFrame = true;
			}
			this.m_DoubleTap_LastTapTime = Time.unscaledTime;
		}
		if (this.m_DoubleTapFoundThisFrame)
		{
			if (this.m_CurrentItem == null)
			{
				return;
			}
			this.CancelExploreByTouch();
			this.m_ExploreByTouch_SingleTapWaitTimer = -1f;
			if (this.m_CurrentElementHasSoleFocus && !UAP_VirtualKeyboard.IsOpen())
			{
				this.m_CurrentItem.m_Object.InteractEnd();
				this.LeaveFocussedItem();
				return;
			}
			if (this.m_CurrentItem.m_Object.IsInteractable())
			{
				this.InteractWithElement(this.m_CurrentItem);
			}
		}
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x000525BA File Offset: 0x000507BA
	public static void FinishCurrentInteraction()
	{
		if (!UAP_AccessibilityManager.m_IsInitialized)
		{
			return;
		}
		UAP_AccessibilityManager.instance.m_CurrentItem.m_Object.InteractEnd();
		UAP_AccessibilityManager.instance.LeaveFocussedItem();
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x000525E2 File Offset: 0x000507E2
	public static bool GetSpeakDisabledInteractables()
	{
		UAP_AccessibilityManager.Initialize();
		return UAP_AccessibilityManager.instance.m_ReadDisabledInteractables;
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x000525F4 File Offset: 0x000507F4
	private void LeaveFocussedItem()
	{
		this.PlaySFX(this.m_UIFocusLeave);
		this.m_CurrentElementHasSoleFocus = false;
		this.Say_Internal("", true, true, UAP_AudioQueue.EInterrupt.Elements);
		if (this.m_CurrentItem.m_Type == AccessibleUIGroupRoot.EUIElement.ETextEdit)
		{
			this.ReadValue(false, true);
			return;
		}
		if (this.m_CurrentItem.m_Type == AccessibleUIGroupRoot.EUIElement.EDropDown)
		{
			string text = UAP_AccessibilityManager.Localize_Internal("DropdownItemSelected");
			string currentValueAsText = this.m_CurrentItem.m_Object.GetCurrentValueAsText();
			text = text.Replace("TT", currentValueAsText);
			this.SayAudio(null, text, UAP_AudioQueue.EAudioType.Element_Text, this.m_CurrentItem.m_Object.m_AllowVoiceOver, UAP_AudioQueue.EInterrupt.Elements);
		}
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x0005268C File Offset: 0x0005088C
	private void CancelFocussedItem()
	{
		this.PlaySFX(this.m_UIFocusLeave);
		this.m_CurrentItem.m_Object.InteractAbort();
		this.Say_Internal("", true, true, UAP_AudioQueue.EInterrupt.Elements);
		this.m_CurrentElementHasSoleFocus = false;
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x000526C0 File Offset: 0x000508C0
	private bool IsActiveContainer2DNavigation()
	{
		return this.m_ActiveContainerIndex >= 0 && this.m_ActiveContainers[this.m_ActiveContainerIndex].m_2DNavigation;
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x000526E4 File Offset: 0x000508E4
	private void UpdateKeyboardInput()
	{
		if (this.m_CurrentElementHasSoleFocus && !UAP_VirtualKeyboard.IsOpen())
		{
			if (this.m_CurrentItem.m_Type == AccessibleUIGroupRoot.EUIElement.ESlider)
			{
				bool flag = false;
				if (Input.GetKeyDown(this.m_SliderIncrementKey))
				{
					this.m_CurrentItem.m_Object.Increment();
					flag = true;
				}
				else if (Input.GetKeyDown(this.m_SliderDecrementKey))
				{
					this.m_CurrentItem.m_Object.Decrement();
					flag = true;
				}
				if (flag)
				{
					this.ReadValue(false, true);
				}
			}
			else if (this.m_CurrentItem.m_Type == AccessibleUIGroupRoot.EUIElement.EDropDown)
			{
				bool flag2 = false;
				bool flag3 = false;
				if (Input.GetKeyDown(this.m_DropDownNextKey))
				{
					if (!this.m_CurrentItem.m_Object.Increment())
					{
						flag3 = true;
					}
					flag2 = true;
				}
				else if (Input.GetKeyDown(this.m_DropDownPreviousKey))
				{
					if (!this.m_CurrentItem.m_Object.Decrement())
					{
						flag3 = true;
					}
					flag2 = true;
				}
				if (flag2)
				{
					this.ReadValue(false, true);
					this.ReadDropdownItemIndex(ref this.m_CurrentItem);
				}
				if (flag3)
				{
					this.PlaySFX(this.m_UIBoundsReached);
				}
			}
			if (Input.GetKeyDown(this.m_InteractKey))
			{
				this.m_CurrentItem.m_Object.InteractEnd();
				this.LeaveFocussedItem();
			}
			if (Input.GetKeyDown(this.m_AbortKey))
			{
				this.CancelFocussedItem();
				return;
			}
		}
		else if (this.HandleUI())
		{
			if (this.IsActiveContainer2DNavigation())
			{
				if (Input.GetKeyDown(this.m_DownKey))
				{
					this.Navigate2DUIElement(UAP_AccessibilityManager.ESDirection.EDown);
				}
				if (Input.GetKeyDown(this.m_UpKey))
				{
					this.Navigate2DUIElement(UAP_AccessibilityManager.ESDirection.EUp);
				}
				if (Input.GetKeyDown(this.m_RightKey))
				{
					this.Navigate2DUIElement(UAP_AccessibilityManager.ESDirection.ERight);
				}
				if (Input.GetKeyDown(this.m_LeftKey))
				{
					this.Navigate2DUIElement(UAP_AccessibilityManager.ESDirection.ELeft);
				}
				if (Input.GetKeyDown(this.m_DownKey) || Input.GetKeyDown(this.m_UpKey) || Input.GetKeyDown(this.m_RightKey) || Input.GetKeyDown(this.m_LeftKey))
				{
					return;
				}
			}
			else
			{
				if (Input.GetKeyDown(this.m_NextElementKey))
				{
					this.IncrementUIElement();
					return;
				}
				if (Input.GetKeyDown(this.m_PreviousElementKey))
				{
					this.DecrementUIElement();
					return;
				}
				if (Input.GetKeyDown(this.m_PreviousContainerKey) || (this.m_UseTabAndShiftTabForContainerJumping && Input.GetKeyDown(KeyCode.Tab) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))))
				{
					int activeContainerIndex = this.m_ActiveContainerIndex;
					if (this.DecrementContainer(true))
					{
						this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.UserInput, false);
						this.PlaySFX(this.m_UINavigationClick);
					}
					else
					{
						this.PlaySFX(this.m_UIBoundsReached);
					}
					if (activeContainerIndex != this.m_ActiveContainerIndex)
					{
						this.ReadContainerName();
					}
					UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, false);
					return;
				}
				if (Input.GetKeyDown(this.m_NextContainerKey) || (this.m_UseTabAndShiftTabForContainerJumping && Input.GetKeyDown(KeyCode.Tab)))
				{
					int activeContainerIndex2 = this.m_ActiveContainerIndex;
					if (this.IncrementContainer(true))
					{
						this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.UserInput, false);
						this.PlaySFX(this.m_UINavigationClick);
					}
					else
					{
						this.PlaySFX(this.m_UIBoundsReached);
					}
					if (activeContainerIndex2 != this.m_ActiveContainerIndex)
					{
						this.ReadContainerName();
					}
					UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, false);
					return;
				}
			}
			if (Input.GetKeyDown(this.m_InteractKey))
			{
				this.InteractWithElement(this.m_CurrentItem);
			}
		}
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x000529F0 File Offset: 0x00050BF0
	private void InteractWithElement(AccessibleUIGroupRoot.Accessible_UIElement item)
	{
		if (item == null)
		{
			return;
		}
		if (!item.m_Object.IsInteractable())
		{
			return;
		}
		this.Say_Internal("", true, true, UAP_AudioQueue.EInterrupt.Elements);
		switch (item.m_Type)
		{
		case AccessibleUIGroupRoot.EUIElement.EButton:
			this.PlaySFX(this.m_UIInteract);
			item.m_Object.Interact();
			return;
		case AccessibleUIGroupRoot.EUIElement.ELabel:
			break;
		case AccessibleUIGroupRoot.EUIElement.EToggle:
			this.PlaySFX(this.m_UIInteract);
			item.m_Object.Interact();
			UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, false);
			return;
		case AccessibleUIGroupRoot.EUIElement.ESlider:
			this.PlaySFX(this.m_UIFocusEnter);
			item.m_Object.Interact();
			this.m_CurrentElementHasSoleFocus = true;
			return;
		case AccessibleUIGroupRoot.EUIElement.ETextEdit:
			this.PlaySFX(this.m_UIFocusEnter);
			item.m_Object.Interact();
			this.m_CurrentElementHasSoleFocus = true;
			return;
		case AccessibleUIGroupRoot.EUIElement.EDropDown:
			this.PlaySFX(this.m_UIFocusEnter);
			item.m_Object.Interact();
			this.m_CurrentElementHasSoleFocus = true;
			this.ReadValue(false, true);
			this.ReadDropdownItemIndex(ref item);
			break;
		default:
			return;
		}
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x00052AF0 File Offset: 0x00050CF0
	private void ReadDropdownItemIndex(ref AccessibleUIGroupRoot.Accessible_UIElement item)
	{
		int itemCount = ((AccessibleDropdown)item.m_Object).GetItemCount();
		int selectedItemIndex = ((AccessibleDropdown)item.m_Object).GetSelectedItemIndex();
		string text = UAP_AccessibilityManager.Localize_Internal("DropdownItemIndex");
		text = text.Replace("XX", selectedItemIndex.ToString("0"));
		text = text.Replace("YY", itemCount.ToString("0"));
		this.SayPause(this.m_TypeDelay);
		this.SayAudio(null, text, UAP_AudioQueue.EAudioType.Element_Text, this.m_CurrentItem.m_Object.m_AllowVoiceOver, null, UAP_AudioQueue.EInterrupt.None);
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x00052B84 File Offset: 0x00050D84
	private void UpdateContainerActivations()
	{
		bool flag = this.m_ReadItemNextUpdate;
		this.m_ReadItemNextUpdate = false;
		if (UAP_AccessibilityManager.m_ContainersToActivate.Count > 0)
		{
			flag = true;
			foreach (AccessibleUIGroupRoot accessibleUIGroupRoot in UAP_AccessibilityManager.m_ContainersToActivate)
			{
				this.ActivateContainer_Internal(accessibleUIGroupRoot, true, false);
			}
			UAP_AccessibilityManager.m_ContainersToActivate.Clear();
		}
		this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.Internal, false);
		if (flag)
		{
			UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, false);
		}
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x00052C18 File Offset: 0x00050E18
	private void CancelTaps()
	{
		this.m_TripleTap_TouchCountHelper = 0;
		this.m_TripleTap_LastTapTime = -1f;
		this.m_WaitingForThreeFingerTap = false;
		this.m_TripleTap_Count = 0;
		this.m_ExploreByTouch_SingleTapWaitTimer = -1f;
		this.m_DoubleTap_LastTapTime = -1f;
		this.m_MagicTap_TouchCountHelper = 0;
		this.m_MagicTap_LastTapTime = -1f;
		this.m_WaitingForMagicTap = false;
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x00052C74 File Offset: 0x00050E74
	public void OnSwipe(UAP_AccessibilityManager.ESDirection dir, int fingerCount)
	{
		this.CancelExploreByTouch();
		this.CancelTaps();
		if (this.m_CurrentElementHasSoleFocus && !UAP_VirtualKeyboard.IsOpen())
		{
			if (fingerCount == 1)
			{
				if (this.m_CurrentItem.m_Type == AccessibleUIGroupRoot.EUIElement.ESlider)
				{
					bool flag = false;
					if (dir == UAP_AccessibilityManager.ESDirection.EUp)
					{
						((AccessibleSlider)this.m_CurrentItem.m_Object).Increment();
						flag = true;
					}
					else if (dir == UAP_AccessibilityManager.ESDirection.EDown)
					{
						((AccessibleSlider)this.m_CurrentItem.m_Object).Decrement();
						flag = true;
					}
					if (flag)
					{
						this.ReadValue(false, true);
						return;
					}
				}
				else if (this.m_CurrentItem.m_Type == AccessibleUIGroupRoot.EUIElement.EDropDown)
				{
					bool flag2 = false;
					bool flag3 = false;
					if (dir == UAP_AccessibilityManager.ESDirection.EDown || dir == UAP_AccessibilityManager.ESDirection.ERight)
					{
						if (!this.m_CurrentItem.m_Object.Increment())
						{
							flag3 = true;
						}
						flag2 = true;
					}
					else if (dir == UAP_AccessibilityManager.ESDirection.EUp || dir == UAP_AccessibilityManager.ESDirection.ELeft)
					{
						if (!this.m_CurrentItem.m_Object.Decrement())
						{
							flag3 = true;
						}
						flag2 = true;
					}
					if (flag2)
					{
						this.ReadValue(false, true);
						this.ReadDropdownItemIndex(ref this.m_CurrentItem);
					}
					if (flag3)
					{
						this.PlaySFX(this.m_UIBoundsReached);
						return;
					}
				}
			}
		}
		else if (fingerCount == 1)
		{
			if (this.HandleUI())
			{
				if (this.IsActiveContainer2DNavigation())
				{
					this.Navigate2DUIElement(dir);
					return;
				}
				if (dir == UAP_AccessibilityManager.ESDirection.ERight)
				{
					this.IncrementUIElement();
					return;
				}
				if (dir == UAP_AccessibilityManager.ESDirection.ELeft)
				{
					this.DecrementUIElement();
					return;
				}
				if (dir == UAP_AccessibilityManager.ESDirection.EDown)
				{
					int activeContainerIndex = this.m_ActiveContainerIndex;
					if (this.IncrementContainer(true))
					{
						this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.UserInput, false);
						this.PlaySFX(this.m_UINavigationClick);
					}
					else
					{
						this.PlaySFX(this.m_UIBoundsReached);
					}
					if (activeContainerIndex != this.m_ActiveContainerIndex)
					{
						this.ReadContainerName();
					}
					UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, false);
					return;
				}
				if (dir == UAP_AccessibilityManager.ESDirection.EUp)
				{
					int activeContainerIndex2 = this.m_ActiveContainerIndex;
					if (this.DecrementContainer(true))
					{
						this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.UserInput, false);
						this.PlaySFX(this.m_UINavigationClick);
					}
					else
					{
						this.PlaySFX(this.m_UIBoundsReached);
					}
					if (activeContainerIndex2 != this.m_ActiveContainerIndex)
					{
						this.ReadContainerName();
					}
					UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, false);
					return;
				}
			}
		}
		else if (fingerCount == 2)
		{
			if (dir == UAP_AccessibilityManager.ESDirection.EUp)
			{
				if (this.m_OnTwoFingerSwipeUpHandler != null)
				{
					this.m_OnTwoFingerSwipeUpHandler();
					return;
				}
				this.StartReadingfromTop();
				return;
			}
			else if (dir == UAP_AccessibilityManager.ESDirection.EDown)
			{
				if (this.m_OnTwoFingerSwipeDownHandler != null)
				{
					this.m_OnTwoFingerSwipeDownHandler();
					return;
				}
				this.StartReadingFromCurrentElement();
				return;
			}
			else if (dir == UAP_AccessibilityManager.ESDirection.ELeft)
			{
				if (this.m_OnTwoFingerSwipeLeftHandler != null)
				{
					this.m_OnTwoFingerSwipeLeftHandler();
					return;
				}
			}
			else if (dir == UAP_AccessibilityManager.ESDirection.ERight && this.m_OnTwoFingerSwipeRightHandler != null)
			{
				this.m_OnTwoFingerSwipeRightHandler();
				return;
			}
		}
		else if (fingerCount == 3 && dir == UAP_AccessibilityManager.ESDirection.EUp)
		{
			this.HandleUI();
		}
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x00052ED6 File Offset: 0x000510D6
	public static void SetFocusToTopOfPage()
	{
		if (UAP_AccessibilityManager.instance == null || !UAP_AccessibilityManager.IsActive())
		{
			return;
		}
		UAP_AccessibilityManager.instance.SetFocusToTopOfPage_Internal();
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x00052EF8 File Offset: 0x000510F8
	private void SetFocusToTopOfPage_Internal()
	{
		if (this.HandleUI() && this.m_ActiveContainers.Count > 0)
		{
			int activeContainerIndex = this.m_ActiveContainerIndex;
			this.m_ActiveContainerIndex = 0;
			this.m_ActiveContainers[this.m_ActiveContainerIndex].JumpToFirst();
			this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.Internal, false);
			if (this.m_CurrentItem != null)
			{
				if (activeContainerIndex != 0)
				{
					this.ReadContainerName();
				}
				UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, true);
			}
		}
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x00052F64 File Offset: 0x00051164
	private void StartReadingfromTop()
	{
		if (this.HandleUI() && this.m_ActiveContainers.Count > 0)
		{
			this.SetFocusToTopOfPage_Internal();
			this.m_ContinuousReading = true;
			this.m_ContinuousReading_WaitInputClear = true;
		}
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x00052F90 File Offset: 0x00051190
	public static void ReadFromCurrent()
	{
		if (!UAP_AccessibilityManager.IsEnabled())
		{
			return;
		}
		UAP_AccessibilityManager.instance.StartReadingFromCurrentElement();
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x00052FA4 File Offset: 0x000511A4
	private void StartReadingFromCurrentElement()
	{
		if (this.HandleUI() && this.m_ActiveContainers.Count > 0)
		{
			this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.Internal, false);
			if (this.m_CurrentItem != null)
			{
				UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, true);
			}
			this.m_ContinuousReading = true;
			this.m_ContinuousReading_WaitInputClear = true;
		}
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x00052FF1 File Offset: 0x000511F1
	public static void ReadFromTop()
	{
		if (!UAP_AccessibilityManager.IsEnabled())
		{
			return;
		}
		UAP_AccessibilityManager.instance.StartReadingfromTop();
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x00053008 File Offset: 0x00051208
	private void Navigate2DUIElement(UAP_AccessibilityManager.ESDirection direction)
	{
		if (!this.HandleUI())
		{
			return;
		}
		this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.UserInput, false);
		if (this.m_ActiveContainerIndex >= 0)
		{
			bool flag = this.m_ActiveContainers[this.m_ActiveContainerIndex].MoveFocus2D(direction);
			int activeContainerIndex = this.m_ActiveContainerIndex;
			bool flag2 = true;
			if (!flag)
			{
				if (!this.m_ActiveContainers[this.m_ActiveContainerIndex].IsConstrainedToContainer(direction) && this.m_ActiveContainers.Count > 1)
				{
					if (this.IncrementContainer(false))
					{
						this.m_ActiveContainers[this.m_ActiveContainerIndex].JumpToFirst();
					}
					else
					{
						this.PlaySFX(this.m_UIBoundsReached);
					}
				}
				else
				{
					this.PlaySFX(this.m_UIBoundsReached);
					flag2 = false;
				}
			}
			this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.UserInput, false);
			if (flag2)
			{
				this.PlaySFX(this.m_UINavigationClick);
			}
			if (activeContainerIndex != this.m_ActiveContainerIndex)
			{
				this.ReadContainerName();
			}
			UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, false);
		}
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x000530EC File Offset: 0x000512EC
	private void DecrementUIElement()
	{
		if (!this.HandleUI())
		{
			return;
		}
		AccessibleUIGroupRoot.Accessible_UIElement currentItem = this.m_CurrentItem;
		int activeContainerIndex = this.m_ActiveContainerIndex;
		this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.UserInput, false);
		if (this.m_ActiveContainerIndex >= 0)
		{
			if (!this.m_ActiveContainers[this.m_ActiveContainerIndex].DecrementCurrentItem(this.m_CyclicMenus) && this.m_ActiveContainers.Count > 1 && this.DecrementContainer(false))
			{
				this.m_ActiveContainers[this.m_ActiveContainerIndex].JumpToLast();
			}
			this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.UserInput, false);
			if (currentItem != this.m_CurrentItem)
			{
				this.PlaySFX(this.m_UINavigationClick);
			}
			else
			{
				this.PlaySFX(this.m_UIBoundsReached);
			}
			if (activeContainerIndex != this.m_ActiveContainerIndex)
			{
				this.ReadContainerName();
			}
			UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, false);
		}
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x000531B8 File Offset: 0x000513B8
	private bool DecrementContainer(bool resetToStartItem = false)
	{
		if (!this.HandleUI())
		{
			return false;
		}
		if (this.m_ActiveContainerIndex > 0 || this.m_CyclicMenus)
		{
			this.m_ActiveContainerIndex--;
			if (this.m_ActiveContainerIndex < 0)
			{
				this.m_ActiveContainerIndex = this.m_ActiveContainers.Count - 1;
			}
			if (resetToStartItem && this.m_ActiveContainers.Count > 0)
			{
				this.m_ActiveContainers[this.m_ActiveContainerIndex].ResetToStart();
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x00053238 File Offset: 0x00051438
	private void IncrementUIElement()
	{
		if (!this.HandleUI())
		{
			return;
		}
		AccessibleUIGroupRoot.Accessible_UIElement currentItem = this.m_CurrentItem;
		int activeContainerIndex = this.m_ActiveContainerIndex;
		this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.UserInput, false);
		if (this.m_ActiveContainerIndex >= 0)
		{
			if (!this.m_ActiveContainers[this.m_ActiveContainerIndex].IncrementCurrentItem(this.m_CyclicMenus) && this.m_ActiveContainers.Count > 1 && this.IncrementContainer(false))
			{
				this.m_ActiveContainers[this.m_ActiveContainerIndex].JumpToFirst();
			}
			this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.UserInput, false);
			if (currentItem != this.m_CurrentItem)
			{
				this.PlaySFX(this.m_UINavigationClick);
			}
			else
			{
				this.PlaySFX(this.m_UIBoundsReached);
			}
			if (activeContainerIndex != this.m_ActiveContainerIndex)
			{
				this.ReadContainerName();
			}
			UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, false);
		}
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x00053301 File Offset: 0x00051501
	private void PlaySFX(AudioClip clip)
	{
		if (clip == null || this.m_SFXPlayer == null)
		{
			return;
		}
		this.m_SFXPlayer.PlayOneShot(clip, 0.8f);
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x0005332C File Offset: 0x0005152C
	private bool IncrementContainer(bool resetToStartItem = false)
	{
		if (!this.HandleUI())
		{
			return false;
		}
		if (this.m_ActiveContainerIndex < this.m_ActiveContainers.Count - 1 || this.m_CyclicMenus)
		{
			this.m_ActiveContainerIndex++;
			if (this.m_ActiveContainerIndex >= this.m_ActiveContainers.Count)
			{
				this.m_ActiveContainerIndex = 0;
			}
			if (resetToStartItem && this.m_ActiveContainers.Count > 0)
			{
				this.m_ActiveContainers[this.m_ActiveContainerIndex].ResetToStart();
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x000533B4 File Offset: 0x000515B4
	private void UpdateScrubDetection()
	{
		if (this.m_ExploreByTouch_IsActive)
		{
			return;
		}
		int touchCount = this.GetTouchCount();
		if (this.m_ScrubWaitForLift)
		{
			if (touchCount == 0)
			{
				this.m_ScrubWaitForLift = false;
			}
			return;
		}
		if (touchCount != 2)
		{
			if (this.m_ScrubPhaseIndex != 0)
			{
				if (this.m_ScrubPhaseIndex == 3)
				{
					this.m_ScrubPhaseLastPoint = this.GetTouchPosition();
					float num = Mathf.Abs(this.m_ScrubPhaseLastPoint.x - this.m_ScrubPhaseStartPoint.x);
					float num2 = this.m_ScrubScreenFractionMinswipeWidth * (float)Mathf.Min(Screen.height, Screen.width);
					if (num < num2)
					{
						this.AbortScrubDetection();
						return;
					}
					if (this.m_HandleMagicGestures && this.m_OnBackCallbacks != null)
					{
						this.m_OnBackCallbacks();
					}
				}
				this.AbortScrubDetection();
			}
			return;
		}
		if (this.m_ScrubPhaseIndex == 0)
		{
			this.m_ScrubPhaseIndex = 1;
			this.m_ScrubPhaseTimeoutTimer = this.m_ScrubPhaseMaxDuration;
			this.m_ScrubPhaseStartPoint = this.GetTouchPosition();
			this.m_ScrubPhaseLastPoint = this.m_ScrubPhaseStartPoint;
			return;
		}
		this.m_ScrubPhaseTimeoutTimer -= Time.unscaledDeltaTime;
		if (this.m_ScrubPhaseTimeoutTimer <= 0f)
		{
			this.AbortScrubDetection();
			return;
		}
		Vector3 touchPosition = this.GetTouchPosition();
		if (Mathf.Abs(touchPosition.x - this.m_ScrubPhaseLastPoint.x) < 5f)
		{
			return;
		}
		bool flag;
		if (this.m_ScrubPhaseIndex == 1 || this.m_ScrubPhaseIndex == 3)
		{
			flag = touchPosition.x > this.m_ScrubPhaseLastPoint.x;
		}
		else
		{
			flag = touchPosition.x < this.m_ScrubPhaseLastPoint.x;
		}
		if (flag)
		{
			this.m_ScrubPhaseLastPoint = touchPosition;
			return;
		}
		if (this.m_ScrubPhaseIndex == 3)
		{
			this.AbortScrubDetection();
			return;
		}
		float num3 = this.m_ScrubScreenFractionMinswipeWidth * (float)Mathf.Min(Screen.height, Screen.width);
		if (Mathf.Abs(this.m_ScrubPhaseLastPoint.x - this.m_ScrubPhaseStartPoint.x) < num3)
		{
			this.AbortScrubDetection();
			return;
		}
		this.m_ScrubPhaseIndex++;
		this.m_ScrubPhaseTimeoutTimer = this.m_ScrubPhaseMaxDuration;
		this.m_ScrubPhaseStartPoint = this.m_ScrubPhaseLastPoint;
		this.m_ScrubPhaseLastPoint = touchPosition;
		if (this.m_ScrubPhaseIndex == 3)
		{
			this.m_SwipeActive = false;
			this.m_SwipeWaitForLift = true;
		}
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x000535C5 File Offset: 0x000517C5
	private void AbortScrubDetection()
	{
		this.m_ScrubWaitForLift = true;
		this.m_ScrubPhaseIndex = 0;
		this.m_ScrubPhaseTimeoutTimer = -1f;
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x000535E0 File Offset: 0x000517E0
	private void UpdateSwipeDetection()
	{
		if (this.m_ExploreByTouch_IsActive)
		{
			return;
		}
		int touchCount = this.GetTouchCount();
		if (!this.m_SwipeActive)
		{
			if (this.m_SwipeWaitForLift)
			{
				if (touchCount == 0)
				{
					this.m_SwipeWaitForLift = false;
					return;
				}
			}
			else if (touchCount > 0)
			{
				this.m_SwipeActive = true;
				this.m_SwipeStartPos = new Vector2(0f, 0f);
				this.m_SwipeDeltaTime = 0f;
				this.m_SwipeTouchCount = touchCount;
				this.m_SwipeStartPos = this.GetTouchPosition();
				return;
			}
		}
		else if (touchCount < this.m_SwipeTouchCount)
		{
			Vector2 vector = this.m_SwipeCurrPos - this.m_SwipeStartPos;
			float magnitude = vector.magnitude;
			bool flag = true;
			if (this.m_SwipeDeltaTime < 0.08f)
			{
				flag = false;
			}
			float num = (float)Screen.height * 0.05882353f;
			if (vector.magnitude < num)
			{
				if (this.m_DebugOutputLabel != null && UAP_AccessibilityManager.instance.m_DebugOutput)
				{
					this.m_DebugOutputLabel.text = "No swipe, minimum distance not reached";
				}
				flag = false;
			}
			if (magnitude / this.m_SwipeDeltaTime < 600f)
			{
				if (this.m_DebugOutputLabel != null && UAP_AccessibilityManager.instance.m_DebugOutput)
				{
					this.m_DebugOutputLabel.text = "No swipe, distance/time ratio to small";
				}
				flag = false;
			}
			if (flag)
			{
				float num2 = this.m_SwipeCurrPos.x - this.m_SwipeStartPos.x;
				float num3 = this.m_SwipeCurrPos.y - this.m_SwipeStartPos.y;
				UAP_AccessibilityManager.ESDirection esdirection;
				if (Mathf.Abs(num2) > Mathf.Abs(num3))
				{
					if (num2 > 0f)
					{
						esdirection = UAP_AccessibilityManager.ESDirection.ERight;
					}
					else
					{
						esdirection = UAP_AccessibilityManager.ESDirection.ELeft;
					}
				}
				else if (num3 > 0f)
				{
					esdirection = UAP_AccessibilityManager.ESDirection.EUp;
				}
				else
				{
					esdirection = UAP_AccessibilityManager.ESDirection.EDown;
				}
				if (this.m_DebugOutputLabel != null && UAP_AccessibilityManager.instance.m_DebugOutput)
				{
					this.m_DebugOutputLabel.text = "Swipe found. Direction " + esdirection.ToString() + " - Finger Count: " + this.m_SwipeTouchCount.ToString();
				}
				if (!UAP_AccessibilityManager.m_Paused && UAP_AccessibilityManager.m_IsEnabled && this.HandleUI())
				{
					this.OnSwipe(esdirection, this.m_SwipeTouchCount);
				}
			}
			this.m_SwipeActive = false;
			if (this.m_SwipeTouchCount > 0)
			{
				this.m_SwipeWaitForLift = true;
				return;
			}
			this.m_SwipeWaitForLift = false;
			return;
		}
		else
		{
			this.m_SwipeCurrPos = this.GetTouchPosition();
			this.m_SwipeTouchCount = this.GetTouchCount();
			this.m_SwipeDeltaTime += Time.unscaledDeltaTime;
		}
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x00053846 File Offset: 0x00051A46
	private Vector3 GetTouchPosition()
	{
		return Input.mousePosition;
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x00053850 File Offset: 0x00051A50
	private int GetTouchCount()
	{
		if (Input.GetMouseButton(0) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.LeftMeta)))
		{
			return 2;
		}
		if (Input.GetMouseButton(1) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.LeftMeta)))
		{
			return 3;
		}
		if (Input.GetMouseButton(0))
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x000538AC File Offset: 0x00051AAC
	public static void Say(string textToSay, bool canBeInterrupted = true, bool allowVoiceOver = true, UAP_AudioQueue.EInterrupt interrupts = UAP_AudioQueue.EInterrupt.Elements)
	{
		if (!UAP_AccessibilityManager.IsEnabled())
		{
			return;
		}
		UAP_AccessibilityManager.instance.Say_Internal(textToSay, canBeInterrupted, allowVoiceOver, interrupts);
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x000538C4 File Offset: 0x00051AC4
	public static void SaySkippable(string textToSay)
	{
		if (!UAP_AccessibilityManager.IsEnabled())
		{
			return;
		}
		if (UAP_AccessibilityManager.instance.m_AudioQueue.IsPlayingExceptAppOrSkippable())
		{
			return;
		}
		UAP_AccessibilityManager.instance.m_AudioQueue.QueueAudio(textToSay, UAP_AudioQueue.EAudioType.Skippable, true, null, UAP_AudioQueue.EInterrupt.None, true);
	}

	// Token: 0x06000C75 RID: 3189 RVA: 0x000538F6 File Offset: 0x00051AF6
	public static void SayAs(string textToSay, UAP_AudioQueue.EAudioType sayAs, UAP_AudioQueue.UAP_GenericCallback callback = null)
	{
		if (!UAP_AccessibilityManager.IsEnabled())
		{
			return;
		}
		UAP_AccessibilityManager.instance.m_AudioQueue.QueueAudio(textToSay, sayAs, true, callback, UAP_AudioQueue.EInterrupt.Elements, true);
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x00053916 File Offset: 0x00051B16
	private void Say_Internal(string textToSay, bool canBeInterrupted = true, bool allowVoiceOver = true, UAP_AudioQueue.EInterrupt interrupts = UAP_AudioQueue.EInterrupt.Elements)
	{
		this.m_AudioQueue.QueueAudio(textToSay, UAP_AudioQueue.EAudioType.App, allowVoiceOver, null, interrupts, canBeInterrupted);
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x0005392B File Offset: 0x00051B2B
	public static bool IsSpeaking()
	{
		UAP_AccessibilityManager.Initialize();
		return UAP_AccessibilityManager.instance.m_AudioQueue.IsPlaying();
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x00053944 File Offset: 0x00051B44
	private void OnApplicationPause(bool paused)
	{
		if (!UAP_AccessibilityManager.m_IsInitialized)
		{
			return;
		}
		if (paused)
		{
			UAP_AccessibilityManager.SavePluginEnabledState();
		}
		UAP_AccessibilityManager.StopContinuousReading();
		if (!UAP_AccessibilityManager.m_IsEnabled && !paused && this.m_RecheckAutoEnablingOnResume && UAP_AccessibilityManager.ShouldAutoEnable())
		{
			UAP_AccessibilityManager.EnableAccessibility(true, false);
			if (this.m_OnAccessibilityModeChanged != null)
			{
				this.m_OnAccessibilityModeChanged(true);
			}
		}
		if (!UAP_AccessibilityManager.m_IsEnabled || UAP_AccessibilityManager.m_Paused)
		{
			return;
		}
		if (!paused)
		{
			this.StartScreenOver();
		}
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x000539B2 File Offset: 0x00051BB2
	private void StartScreenOver()
	{
		if (this.m_ActiveContainers.Count > 0)
		{
			this.m_ActiveContainers[0].ResetToStart();
			this.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.Internal, false);
			UAP_AccessibilityManager.ReadItem(this.m_CurrentItem, false);
		}
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x000539E7 File Offset: 0x00051BE7
	public static bool IsTalkBackEnabledAndTouchExploreActive()
	{
		return false;
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x000539EA File Offset: 0x00051BEA
	public static bool IsTalkBackEnabled()
	{
		return false;
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x000539ED File Offset: 0x00051BED
	public static void RegisterOnPauseToggledCallback(UAP_AccessibilityManager.OnPauseToggleCallbackFunc func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager uap_AccessibilityManager = UAP_AccessibilityManager.instance;
		uap_AccessibilityManager.m_OnPauseToggleCallbacks = (UAP_AccessibilityManager.OnPauseToggleCallbackFunc)Delegate.Combine(uap_AccessibilityManager.m_OnPauseToggleCallbacks, func);
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x00053A0F File Offset: 0x00051C0F
	public static void UnregisterOnPauseToggledCallback(UAP_AccessibilityManager.OnPauseToggleCallbackFunc func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager uap_AccessibilityManager = UAP_AccessibilityManager.instance;
		uap_AccessibilityManager.m_OnPauseToggleCallbacks = (UAP_AccessibilityManager.OnPauseToggleCallbackFunc)Delegate.Remove(uap_AccessibilityManager.m_OnPauseToggleCallbacks, func);
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x00053A31 File Offset: 0x00051C31
	public static void RegisterOnBackCallback(UAP_AccessibilityManager.OnTapEvent func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager uap_AccessibilityManager = UAP_AccessibilityManager.instance;
		uap_AccessibilityManager.m_OnBackCallbacks = (UAP_AccessibilityManager.OnTapEvent)Delegate.Combine(uap_AccessibilityManager.m_OnBackCallbacks, func);
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x00053A53 File Offset: 0x00051C53
	public static void UnregisterOnBackCallback(UAP_AccessibilityManager.OnTapEvent func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager uap_AccessibilityManager = UAP_AccessibilityManager.instance;
		uap_AccessibilityManager.m_OnBackCallbacks = (UAP_AccessibilityManager.OnTapEvent)Delegate.Remove(uap_AccessibilityManager.m_OnBackCallbacks, func);
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x00053A75 File Offset: 0x00051C75
	public static void RegisterOnTwoFingerSingleTapCallback(UAP_AccessibilityManager.OnTapEvent func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager uap_AccessibilityManager = UAP_AccessibilityManager.instance;
		uap_AccessibilityManager.m_OnTwoFingerSingleTapCallbacks = (UAP_AccessibilityManager.OnTapEvent)Delegate.Combine(uap_AccessibilityManager.m_OnTwoFingerSingleTapCallbacks, func);
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x00053A97 File Offset: 0x00051C97
	public static void UnregisterOnTwoFingerSingleTapCallback(UAP_AccessibilityManager.OnTapEvent func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager uap_AccessibilityManager = UAP_AccessibilityManager.instance;
		uap_AccessibilityManager.m_OnTwoFingerSingleTapCallbacks = (UAP_AccessibilityManager.OnTapEvent)Delegate.Remove(uap_AccessibilityManager.m_OnTwoFingerSingleTapCallbacks, func);
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x00053AB9 File Offset: 0x00051CB9
	public static void RegisterOnThreeFingerSingleTapCallback(UAP_AccessibilityManager.OnTapEvent func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager uap_AccessibilityManager = UAP_AccessibilityManager.instance;
		uap_AccessibilityManager.m_OnThreeFingerSingleTapCallbacks = (UAP_AccessibilityManager.OnTapEvent)Delegate.Combine(uap_AccessibilityManager.m_OnThreeFingerSingleTapCallbacks, func);
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x00053ADB File Offset: 0x00051CDB
	public static void UnregisterOnThreeFingerSingleTapCallback(UAP_AccessibilityManager.OnTapEvent func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager uap_AccessibilityManager = UAP_AccessibilityManager.instance;
		uap_AccessibilityManager.m_OnThreeFingerSingleTapCallbacks = (UAP_AccessibilityManager.OnTapEvent)Delegate.Remove(uap_AccessibilityManager.m_OnThreeFingerSingleTapCallbacks, func);
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x00053AFD File Offset: 0x00051CFD
	public static void RegisterOnThreeFingerDoubleTapCallback(UAP_AccessibilityManager.OnTapEvent func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager uap_AccessibilityManager = UAP_AccessibilityManager.instance;
		uap_AccessibilityManager.m_OnThreeFingerDoubleTapCallbacks = (UAP_AccessibilityManager.OnTapEvent)Delegate.Combine(uap_AccessibilityManager.m_OnThreeFingerDoubleTapCallbacks, func);
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x00053B1F File Offset: 0x00051D1F
	public static void UnregisterOnThreeFingerDoubleTapCallback(UAP_AccessibilityManager.OnTapEvent func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager uap_AccessibilityManager = UAP_AccessibilityManager.instance;
		uap_AccessibilityManager.m_OnThreeFingerDoubleTapCallbacks = (UAP_AccessibilityManager.OnTapEvent)Delegate.Remove(uap_AccessibilityManager.m_OnThreeFingerDoubleTapCallbacks, func);
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x00053B41 File Offset: 0x00051D41
	public static void RegisterAccessibilityModeChangeCallback(UAP_AccessibilityManager.OnAccessibilityModeChanged func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager uap_AccessibilityManager = UAP_AccessibilityManager.instance;
		uap_AccessibilityManager.m_OnAccessibilityModeChanged = (UAP_AccessibilityManager.OnAccessibilityModeChanged)Delegate.Combine(uap_AccessibilityManager.m_OnAccessibilityModeChanged, func);
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x00053B63 File Offset: 0x00051D63
	public static void UnregisterAccessibilityModeChangeCallback(UAP_AccessibilityManager.OnAccessibilityModeChanged func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager uap_AccessibilityManager = UAP_AccessibilityManager.instance;
		uap_AccessibilityManager.m_OnAccessibilityModeChanged = (UAP_AccessibilityManager.OnAccessibilityModeChanged)Delegate.Remove(uap_AccessibilityManager.m_OnAccessibilityModeChanged, func);
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x00053B85 File Offset: 0x00051D85
	public static void SetTwoFingerSwipeUpHandler(UAP_AccessibilityManager.OnTapEvent func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager.instance.m_OnTwoFingerSwipeUpHandler = func;
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x00053B97 File Offset: 0x00051D97
	public static void ResetTwoFingerSwipeUpHandler()
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager.instance.m_OnTwoFingerSwipeUpHandler = null;
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x00053BA9 File Offset: 0x00051DA9
	public static void RegisterOnTwoFingerSwipeLeftCallback(UAP_AccessibilityManager.OnTapEvent func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager uap_AccessibilityManager = UAP_AccessibilityManager.instance;
		uap_AccessibilityManager.m_OnTwoFingerSwipeLeftHandler = (UAP_AccessibilityManager.OnTapEvent)Delegate.Combine(uap_AccessibilityManager.m_OnTwoFingerSwipeLeftHandler, func);
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x00053BCB File Offset: 0x00051DCB
	public static void UnregisterOnTwoFingerSwipeLeftCallback(UAP_AccessibilityManager.OnTapEvent func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager uap_AccessibilityManager = UAP_AccessibilityManager.instance;
		uap_AccessibilityManager.m_OnTwoFingerSwipeLeftHandler = (UAP_AccessibilityManager.OnTapEvent)Delegate.Remove(uap_AccessibilityManager.m_OnTwoFingerSwipeLeftHandler, func);
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x00053BED File Offset: 0x00051DED
	public static void RegisterOnTwoFingerSwipeRightCallback(UAP_AccessibilityManager.OnTapEvent func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager uap_AccessibilityManager = UAP_AccessibilityManager.instance;
		uap_AccessibilityManager.m_OnTwoFingerSwipeRightHandler = (UAP_AccessibilityManager.OnTapEvent)Delegate.Combine(uap_AccessibilityManager.m_OnTwoFingerSwipeRightHandler, func);
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x00053C0F File Offset: 0x00051E0F
	public static void UnregisterOnTwoFingerSwipeRightCallback(UAP_AccessibilityManager.OnTapEvent func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager uap_AccessibilityManager = UAP_AccessibilityManager.instance;
		uap_AccessibilityManager.m_OnTwoFingerSwipeRightHandler = (UAP_AccessibilityManager.OnTapEvent)Delegate.Remove(uap_AccessibilityManager.m_OnTwoFingerSwipeRightHandler, func);
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x00053C31 File Offset: 0x00051E31
	public static void SetTwoFingerSwipeDownHandler(UAP_AccessibilityManager.OnTapEvent func)
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager.instance.m_OnTwoFingerSwipeDownHandler = func;
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x00053C43 File Offset: 0x00051E43
	public static void ResetTwoFingerSwipeDownHandler()
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager.instance.m_OnTwoFingerSwipeDownHandler = null;
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x00053C58 File Offset: 0x00051E58
	public static bool SelectElement(GameObject element, bool forceRepeatItem = false)
	{
		if (element == null)
		{
			return false;
		}
		if (UAP_AccessibilityManager.instance.m_DebugOutput)
		{
			Debug.Log("[Accessibility] Direct Select of GameObject: " + element.name);
		}
		UAP_BaseElement component = element.GetComponent<UAP_BaseElement>();
		if (component == null)
		{
			Debug.LogWarning("[Accessibility] SelectElement: No Accessibility component found on GameObject " + element.name);
			return false;
		}
		return component.SelectItem(forceRepeatItem);
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x00053CC0 File Offset: 0x00051EC0
	public static bool MakeActiveContainer(AccessibleUIGroupRoot container, bool forceRepeatActiveItem = false)
	{
		if (UAP_AccessibilityManager.instance == null)
		{
			return false;
		}
		if (!UAP_AccessibilityManager.instance.m_ActiveContainers.Contains(container))
		{
			if (UAP_AccessibilityManager.instance.m_DebugOutput && UAP_AccessibilityManager.m_IsEnabled)
			{
				Debug.LogWarning("[Accessibility] Trying to select an item in a container that is inactive. Ignoring call.");
			}
			return false;
		}
		UAP_AccessibilityManager.StopContinuousReading();
		int num = 0;
		while (num < UAP_AccessibilityManager.instance.m_ActiveContainers.Count && !(UAP_AccessibilityManager.instance.m_ActiveContainers[num] == container))
		{
			num++;
		}
		AccessibleUIGroupRoot.Accessible_UIElement currentElement = UAP_AccessibilityManager.instance.m_ActiveContainers[num].GetCurrentElement(UAP_AccessibilityManager.instance.m_CyclicMenus);
		int num2 = ((UAP_AccessibilityManager.instance.m_CurrentItem == currentElement) ? 1 : 0);
		UAP_AccessibilityManager.instance.m_CurrentElementHasSoleFocus = false;
		UAP_AccessibilityManager.instance.m_CurrentItem = currentElement;
		UAP_AccessibilityManager.instance.m_ActiveContainerIndex = num;
		UAP_AccessibilityManager.instance.UpdateCurrentItem(UAP_BaseElement.EHighlightSource.Internal, false);
		if (num2 == 0 || forceRepeatActiveItem)
		{
			UAP_AccessibilityManager.ReadItem(UAP_AccessibilityManager.instance.m_CurrentItem, false);
		}
		return true;
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x00053DB6 File Offset: 0x00051FB6
	public static GameObject GetCurrentFocusObject()
	{
		UAP_AccessibilityManager.Initialize();
		if (UAP_AccessibilityManager.instance.m_CurrentItem == null)
		{
			return null;
		}
		return UAP_AccessibilityManager.instance.m_CurrentItem.m_Object.gameObject;
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x00053DDF File Offset: 0x00051FDF
	private bool HandleUI()
	{
		return !this.m_BlockInput && this.m_HandleUI;
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x00053DF1 File Offset: 0x00051FF1
	public static bool UseAndroidTTS()
	{
		UAP_AccessibilityManager.Initialize();
		return UAP_AccessibilityManager.instance.m_AndroidTTS;
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x00053E02 File Offset: 0x00052002
	public static bool UseiOSTTS()
	{
		UAP_AccessibilityManager.Initialize();
		return UAP_AccessibilityManager.instance.m_iOSTTS;
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x00053E13 File Offset: 0x00052013
	public static bool UseWindowsTTS()
	{
		UAP_AccessibilityManager.Initialize();
		return UAP_AccessibilityManager.instance.m_WindowsTTS;
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x00053E24 File Offset: 0x00052024
	public static bool UseMacOSTTS()
	{
		UAP_AccessibilityManager.Initialize();
		return UAP_AccessibilityManager.instance.m_MacOSTTS;
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x00053E35 File Offset: 0x00052035
	public static bool UseWebGLTTS()
	{
		UAP_AccessibilityManager.Initialize();
		return UAP_AccessibilityManager.instance.m_WebGLTTS;
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x00053E48 File Offset: 0x00052048
	public static void RecalculateUIElementsOrder(GameObject parent = null)
	{
		AccessibleUIGroupRoot[] array;
		if (parent != null)
		{
			array = parent.GetComponentsInChildren<AccessibleUIGroupRoot>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].RefreshContainer();
			}
			return;
		}
		array = global::UnityEngine.Object.FindObjectsByType(typeof(AccessibleUIGroupRoot), FindObjectsSortMode.InstanceID) as AccessibleUIGroupRoot[];
		for (int i = 0; i < array.Length; i++)
		{
			array[i].RefreshContainer();
		}
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x00053EA8 File Offset: 0x000520A8
	public static int GetSpeechRate()
	{
		UAP_AccessibilityManager.Initialize();
		return UAP_AccessibilityManager.instance.m_AudioQueue.GetSpeechRate();
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x00053EBE File Offset: 0x000520BE
	public static int SetSpeechRate(int speechRate)
	{
		UAP_AccessibilityManager.Initialize();
		return UAP_AccessibilityManager.instance.m_AudioQueue.SetSpeechRate(speechRate);
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x00053ED5 File Offset: 0x000520D5
	public static void StopSpeaking()
	{
		UAP_AccessibilityManager.Initialize();
		UAP_AccessibilityManager.instance.m_AudioQueue.Stop();
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x00053EEB File Offset: 0x000520EB
	public static string Localize(string key)
	{
		return UAP_AccessibilityManager.Localize_Internal(key);
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x00053EF3 File Offset: 0x000520F3
	public static string Localize_Internal(string key)
	{
		if (UAP_AccessibilityManager.m_CurrentLocalizationTable == null)
		{
			return key;
		}
		if (UAP_AccessibilityManager.m_CurrentLocalizationTable.ContainsKey(key))
		{
			return UAP_AccessibilityManager.m_CurrentLocalizationTable[key];
		}
		Debug.LogWarning("[Accessibility] No localization available for key '" + key + "'");
		return key;
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x00053F2D File Offset: 0x0005212D
	public static bool IsVoiceOverAllowed()
	{
		return UAP_AccessibilityManager.instance == null || UAP_AccessibilityManager.instance.m_AllowVoiceOverGlobal;
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x00053F48 File Offset: 0x00052148
	public static string FormatNumberToCurrentLocale(ulong intNumber)
	{
		string text;
		if (UAP_AccessibilityManager.sIsEuropeanLanguage)
		{
			text = string.Format(CultureInfo.GetCultureInfoByIetfLanguageTag("de-DE"), "{0:n0}", intNumber);
		}
		else
		{
			text = string.Format(CultureInfo.CurrentUICulture, "{0:n0}", intNumber);
		}
		return text;
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x00053F90 File Offset: 0x00052190
	public static string FormatNumberToCurrentLocale(double floatNumber)
	{
		string text;
		if (UAP_AccessibilityManager.sIsEuropeanLanguage)
		{
			text = string.Format(CultureInfo.GetCultureInfoByIetfLanguageTag("de-DE"), "{0:0.##}", floatNumber);
		}
		else
		{
			text = string.Format(CultureInfo.CurrentUICulture, "{0:0.##}", floatNumber);
		}
		return text;
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x00053FD8 File Offset: 0x000521D8
	private static void DetectEuropeanLanguage()
	{
		string text = UAP_AccessibilityManager.m_CurrentLanguage.ToLower();
		if (string.IsNullOrEmpty(text))
		{
			text = Application.systemLanguage.ToString().ToLower();
		}
		UAP_AccessibilityManager.sIsEuropeanLanguage = false;
		if (text == "german" || text == "portuguese" || text == "spanish" || text == "french" || text == "italian")
		{
			UAP_AccessibilityManager.sIsEuropeanLanguage = true;
			return;
		}
		UAP_AccessibilityManager.sIsEuropeanLanguage = false;
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x00054065 File Offset: 0x00052265
	private static void Log(string message)
	{
		if (UAP_AccessibilityManager.instance == null || !UAP_AccessibilityManager.instance.m_DebugOutput)
		{
			return;
		}
		Debug.Log("[Accessibility] " + message);
	}

	// Token: 0x0400086C RID: 2156
	public static string PluginVersion = "1.1.1";

	// Token: 0x0400086D RID: 2157
	public static float PluginVersionAsFloat = 1.11f;

	// Token: 0x0400086E RID: 2158
	public bool m_DefaultState;

	// Token: 0x0400086F RID: 2159
	public bool m_AutoTurnOnIfScreenReaderDetected = true;

	// Token: 0x04000870 RID: 2160
	public bool m_RecheckAutoEnablingOnResume;

	// Token: 0x04000871 RID: 2161
	public bool m_SaveEnabledState;

	// Token: 0x04000872 RID: 2162
	public bool m_HandleUI = true;

	// Token: 0x04000873 RID: 2163
	private bool m_BlockInput;

	// Token: 0x04000874 RID: 2164
	public bool m_HandleMagicGestures = true;

	// Token: 0x04000875 RID: 2165
	public bool m_ExploreByTouch = true;

	// Token: 0x04000876 RID: 2166
	private float m_ExploreByTouchDelay = 0.75f;

	// Token: 0x04000877 RID: 2167
	public bool m_ReadDisabledInteractables = true;

	// Token: 0x04000878 RID: 2168
	public bool m_CyclicMenus;

	// Token: 0x04000879 RID: 2169
	public bool m_AllowBuiltInVirtualKeyboard = true;

	// Token: 0x0400087A RID: 2170
	public bool m_DebugOutput;

	// Token: 0x0400087B RID: 2171
	private float m_HintDelay = 0.6f;

	// Token: 0x0400087C RID: 2172
	private float m_DisabledDelay = 0.2f;

	// Token: 0x0400087D RID: 2173
	private float m_ValueDelay = 0.25f;

	// Token: 0x0400087E RID: 2174
	private float m_TypeDelay = 1f;

	// Token: 0x0400087F RID: 2175
	public bool m_WindowsUseMouseSwipes;

	// Token: 0x04000880 RID: 2176
	private Canvas m_CanvasRoot;

	// Token: 0x04000881 RID: 2177
	public bool m_EditorOverride;

	// Token: 0x04000882 RID: 2178
	public bool m_EditorEnabledState;

	// Token: 0x04000883 RID: 2179
	[Header("WebGL")]
	public bool m_WebGLTTS = true;

	// Token: 0x04000884 RID: 2180
	public string m_GoogleTTSAPIKey = "";

	// Token: 0x04000885 RID: 2181
	[Header("Windows")]
	public bool m_WindowsTTS = true;

	// Token: 0x04000886 RID: 2182
	public int m_WindowsTTSVolume = 100;

	// Token: 0x04000887 RID: 2183
	public bool m_WindowsUseKeys = true;

	// Token: 0x04000888 RID: 2184
	private KeyCode m_NextElementKey = KeyCode.DownArrow;

	// Token: 0x04000889 RID: 2185
	private KeyCode m_PreviousElementKey = KeyCode.UpArrow;

	// Token: 0x0400088A RID: 2186
	private KeyCode m_NextContainerKey = KeyCode.RightArrow;

	// Token: 0x0400088B RID: 2187
	private KeyCode m_PreviousContainerKey = KeyCode.LeftArrow;

	// Token: 0x0400088C RID: 2188
	private bool m_UseTabAndShiftTabForContainerJumping = true;

	// Token: 0x0400088D RID: 2189
	private KeyCode m_InteractKey = KeyCode.Return;

	// Token: 0x0400088E RID: 2190
	private KeyCode m_SliderIncrementKey = KeyCode.UpArrow;

	// Token: 0x0400088F RID: 2191
	private KeyCode m_SliderDecrementKey = KeyCode.DownArrow;

	// Token: 0x04000890 RID: 2192
	private KeyCode m_DropDownPreviousKey = KeyCode.UpArrow;

	// Token: 0x04000891 RID: 2193
	private KeyCode m_DropDownNextKey = KeyCode.DownArrow;

	// Token: 0x04000892 RID: 2194
	private KeyCode m_AbortKey = KeyCode.Escape;

	// Token: 0x04000893 RID: 2195
	private KeyCode m_DownKey = KeyCode.DownArrow;

	// Token: 0x04000894 RID: 2196
	private KeyCode m_UpKey = KeyCode.UpArrow;

	// Token: 0x04000895 RID: 2197
	private KeyCode m_RightKey = KeyCode.RightArrow;

	// Token: 0x04000896 RID: 2198
	private KeyCode m_LeftKey = KeyCode.LeftArrow;

	// Token: 0x04000897 RID: 2199
	[Header("Android")]
	public bool m_AndroidTTS = true;

	// Token: 0x04000898 RID: 2200
	public bool m_AndroidUseUpAndDownForElements;

	// Token: 0x04000899 RID: 2201
	[Header("iOS")]
	public bool m_iOSTTS = true;

	// Token: 0x0400089A RID: 2202
	[Header("Mac OS")]
	public bool m_MacOSTTS = true;

	// Token: 0x0400089B RID: 2203
	[Header("Sound Effects")]
	public AudioClip m_UINavigationClick;

	// Token: 0x0400089C RID: 2204
	public AudioClip m_UIInteract;

	// Token: 0x0400089D RID: 2205
	public AudioClip m_UIFocusEnter;

	// Token: 0x0400089E RID: 2206
	public AudioClip m_UIFocusLeave;

	// Token: 0x0400089F RID: 2207
	public AudioClip m_UIBoundsReached;

	// Token: 0x040008A0 RID: 2208
	public AudioClip m_UIPopUpOpen;

	// Token: 0x040008A1 RID: 2209
	public AudioClip m_UIPopUpClose;

	// Token: 0x040008A2 RID: 2210
	[Header("Types")]
	private AudioClip m_DisabledAsAudio;

	// Token: 0x040008A3 RID: 2211
	private AudioClip m_ButtonAsAudio;

	// Token: 0x040008A4 RID: 2212
	private AudioClip m_ToggleAsAudio;

	// Token: 0x040008A5 RID: 2213
	private AudioClip m_SliderAsAudio;

	// Token: 0x040008A6 RID: 2214
	private AudioClip m_TextEditAsAudio;

	// Token: 0x040008A7 RID: 2215
	private AudioClip m_DropDownAsAudio;

	// Token: 0x040008A8 RID: 2216
	[Header("Localization")]
	private static string m_CurrentLanguage = "English";

	// Token: 0x040008A9 RID: 2217
	private static Dictionary<string, string> m_CurrentLocalizationTable = null;

	// Token: 0x040008AA RID: 2218
	[Header("Other Resources")]
	public AudioSource m_AudioPlayer;

	// Token: 0x040008AB RID: 2219
	public AudioSource m_SFXPlayer;

	// Token: 0x040008AC RID: 2220
	public RectTransform m_Frame;

	// Token: 0x040008AD RID: 2221
	public GameObject m_FrameTemplate;

	// Token: 0x040008AE RID: 2222
	public GameObject m_TouchBlocker;

	// Token: 0x040008AF RID: 2223
	public Text m_DebugOutputLabel;

	// Token: 0x040008B0 RID: 2224
	public bool m_AllowVoiceOverGlobal = true;

	// Token: 0x040008B1 RID: 2225
	public bool m_DetectVoiceOverAtRuntime = true;

	// Token: 0x040008B2 RID: 2226
	private UAP_AudioQueue m_AudioQueue;

	// Token: 0x040008B3 RID: 2227
	private static UAP_AccessibilityManager instance = null;

	// Token: 0x040008B4 RID: 2228
	private static bool isDestroyed = false;

	// Token: 0x040008B5 RID: 2229
	private static bool m_IsInitialized = false;

	// Token: 0x040008B6 RID: 2230
	private static bool m_IsEnabled = false;

	// Token: 0x040008B7 RID: 2231
	private static bool m_Paused = false;

	// Token: 0x040008B8 RID: 2232
	private List<AccessibleUIGroupRoot> m_ActiveContainers = new List<AccessibleUIGroupRoot>();

	// Token: 0x040008B9 RID: 2233
	private List<List<AccessibleUIGroupRoot>> m_SuspendedContainers = new List<List<AccessibleUIGroupRoot>>();

	// Token: 0x040008BA RID: 2234
	private List<int> m_SuspendedActiveContainerIndex = new List<int>();

	// Token: 0x040008BB RID: 2235
	private AccessibleUIGroupRoot.Accessible_UIElement m_CurrentItem;

	// Token: 0x040008BC RID: 2236
	private AccessibleUIGroupRoot.Accessible_UIElement m_PreviousItem;

	// Token: 0x040008BD RID: 2237
	private static List<AccessibleUIGroupRoot> m_ContainersToActivate = new List<AccessibleUIGroupRoot>();

	// Token: 0x040008BE RID: 2238
	private int m_ActiveContainerIndex = -1;

	// Token: 0x040008BF RID: 2239
	private bool m_ReadItemNextUpdate;

	// Token: 0x040008C0 RID: 2240
	private bool m_CurrentElementHasSoleFocus;

	// Token: 0x040008C1 RID: 2241
	private int m_LastUpdateTouchCount;

	// Token: 0x040008C2 RID: 2242
	private UAP_AccessibilityManager.OnPauseToggleCallbackFunc m_OnPauseToggleCallbacks;

	// Token: 0x040008C3 RID: 2243
	private UAP_AccessibilityManager.OnTapEvent m_OnTwoFingerSingleTapCallbacks;

	// Token: 0x040008C4 RID: 2244
	private UAP_AccessibilityManager.OnTapEvent m_OnThreeFingerSingleTapCallbacks;

	// Token: 0x040008C5 RID: 2245
	private UAP_AccessibilityManager.OnTapEvent m_OnThreeFingerDoubleTapCallbacks;

	// Token: 0x040008C6 RID: 2246
	private UAP_AccessibilityManager.OnTapEvent m_OnTwoFingerSwipeUpHandler;

	// Token: 0x040008C7 RID: 2247
	private UAP_AccessibilityManager.OnTapEvent m_OnTwoFingerSwipeDownHandler;

	// Token: 0x040008C8 RID: 2248
	private UAP_AccessibilityManager.OnTapEvent m_OnTwoFingerSwipeLeftHandler;

	// Token: 0x040008C9 RID: 2249
	private UAP_AccessibilityManager.OnTapEvent m_OnTwoFingerSwipeRightHandler;

	// Token: 0x040008CA RID: 2250
	private UAP_AccessibilityManager.OnAccessibilityModeChanged m_OnAccessibilityModeChanged;

	// Token: 0x040008CB RID: 2251
	private UAP_AccessibilityManager.OnTapEvent m_OnBackCallbacks;

	// Token: 0x040008CC RID: 2252
	private bool m_SwipeActive;

	// Token: 0x040008CD RID: 2253
	private int m_SwipeTouchCount;

	// Token: 0x040008CE RID: 2254
	private bool m_SwipeWaitForLift;

	// Token: 0x040008CF RID: 2255
	private Vector2 m_SwipeStartPos;

	// Token: 0x040008D0 RID: 2256
	private Vector2 m_SwipeCurrPos;

	// Token: 0x040008D1 RID: 2257
	private float m_SwipeDeltaTime;

	// Token: 0x040008D2 RID: 2258
	private float m_DoubleTap_LastTapTime = -1f;

	// Token: 0x040008D3 RID: 2259
	private const float m_DoubleTapTime = 0.2f;

	// Token: 0x040008D4 RID: 2260
	private bool m_DoubleTapFoundThisFrame;

	// Token: 0x040008D5 RID: 2261
	private float m_MagicTap_LastTapTime = -1f;

	// Token: 0x040008D6 RID: 2262
	private int m_MagicTap_TouchCountHelper;

	// Token: 0x040008D7 RID: 2263
	private bool m_WaitingForMagicTap;

	// Token: 0x040008D8 RID: 2264
	private int m_TripleTap_Count;

	// Token: 0x040008D9 RID: 2265
	private float m_TripleTap_LastTapTime = -1f;

	// Token: 0x040008DA RID: 2266
	private int m_TripleTap_TouchCountHelper;

	// Token: 0x040008DB RID: 2267
	private bool m_WaitingForThreeFingerTap;

	// Token: 0x040008DC RID: 2268
	private bool m_ExploreByTouch_IsActive;

	// Token: 0x040008DD RID: 2269
	private float m_ExploreByTouch_WaitTimer = -1f;

	// Token: 0x040008DE RID: 2270
	private Vector3 m_ExploreByTouch_StartPosition = new Vector3(0f, 0f, 0f);

	// Token: 0x040008DF RID: 2271
	private float m_ExploreByTouch_SingleTapWaitTimer = -1f;

	// Token: 0x040008E0 RID: 2272
	private Vector3 m_ExploreByTouch_SingleTapStartPosition = new Vector3(0f, 0f, 0f);

	// Token: 0x040008E1 RID: 2273
	private bool m_ContinuousReading;

	// Token: 0x040008E2 RID: 2274
	private bool m_ContinuousReading_WaitInputClear;

	// Token: 0x040008E3 RID: 2275
	private static bool sIsEuropeanLanguage = false;

	// Token: 0x040008E4 RID: 2276
	private bool m_TouchExplore_Active;

	// Token: 0x040008E5 RID: 2277
	private float m_TouchExplore_CheckVelocityTimer = -1f;

	// Token: 0x040008E6 RID: 2278
	private float m_TouchExplore_WaitForDoubleTapToExpireTimer = -1f;

	// Token: 0x040008E7 RID: 2279
	private Vector3 m_TouchExplore_CheckStartPosition = Vector3.one;

	// Token: 0x040008E8 RID: 2280
	private const float m_TouchExplore_CheckDuration = 0.15f;

	// Token: 0x040008E9 RID: 2281
	private float m_ScrubPhaseMaxDuration = 1f;

	// Token: 0x040008EA RID: 2282
	private float m_ScrubScreenFractionMinswipeWidth = 0.1f;

	// Token: 0x040008EB RID: 2283
	private float m_ScrubPhaseTimeoutTimer = -1f;

	// Token: 0x040008EC RID: 2284
	private bool m_ScrubWaitForLift;

	// Token: 0x040008ED RID: 2285
	private int m_ScrubPhaseIndex;

	// Token: 0x040008EE RID: 2286
	private Vector3 m_ScrubPhaseStartPoint;

	// Token: 0x040008EF RID: 2287
	private Vector3 m_ScrubPhaseLastPoint;

	// Token: 0x020015ED RID: 5613
	// (Invoke) Token: 0x0600D66D RID: 54893
	public delegate void OnPauseToggleCallbackFunc();

	// Token: 0x020015EE RID: 5614
	// (Invoke) Token: 0x0600D671 RID: 54897
	public delegate void OnTapEvent();

	// Token: 0x020015EF RID: 5615
	// (Invoke) Token: 0x0600D675 RID: 54901
	public delegate void OnSwipeEvent();

	// Token: 0x020015F0 RID: 5616
	// (Invoke) Token: 0x0600D679 RID: 54905
	public delegate void OnAccessibilityModeChanged(bool enabled);

	// Token: 0x020015F1 RID: 5617
	public enum ESDirection
	{
		// Token: 0x04005865 RID: 22629
		EUp,
		// Token: 0x04005866 RID: 22630
		EDown,
		// Token: 0x04005867 RID: 22631
		ELeft,
		// Token: 0x04005868 RID: 22632
		ERight
	}
}
