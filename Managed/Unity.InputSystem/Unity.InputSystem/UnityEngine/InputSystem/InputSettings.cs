using System;
using System.Collections.Generic;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200005B RID: 91
	public class InputSettings : ScriptableObject
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00034053 File Offset: 0x00032253
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x0003405B File Offset: 0x0003225B
		public InputSettings.UpdateMode updateMode
		{
			get
			{
				return this.m_UpdateMode;
			}
			set
			{
				if (this.m_UpdateMode == value)
				{
					return;
				}
				this.m_UpdateMode = value;
				this.OnChange();
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x00034074 File Offset: 0x00032274
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x0003407C File Offset: 0x0003227C
		public InputSettings.ScrollDeltaBehavior scrollDeltaBehavior
		{
			get
			{
				return this.m_ScrollDeltaBehavior;
			}
			set
			{
				if (this.m_ScrollDeltaBehavior == value)
				{
					return;
				}
				this.m_ScrollDeltaBehavior = value;
				this.OnChange();
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x00034095 File Offset: 0x00032295
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x0003409D File Offset: 0x0003229D
		public bool compensateForScreenOrientation
		{
			get
			{
				return this.m_CompensateForScreenOrientation;
			}
			set
			{
				if (this.m_CompensateForScreenOrientation == value)
				{
					return;
				}
				this.m_CompensateForScreenOrientation = value;
				this.OnChange();
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x000340B6 File Offset: 0x000322B6
		// (set) Token: 0x06000913 RID: 2323 RVA: 0x000340B9 File Offset: 0x000322B9
		[Obsolete("filterNoiseOnCurrent is deprecated, filtering of noise is always enabled now.", false)]
		public bool filterNoiseOnCurrent
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x000340BB File Offset: 0x000322BB
		// (set) Token: 0x06000915 RID: 2325 RVA: 0x000340C3 File Offset: 0x000322C3
		public float defaultDeadzoneMin
		{
			get
			{
				return this.m_DefaultDeadzoneMin;
			}
			set
			{
				if (this.m_DefaultDeadzoneMin == value)
				{
					return;
				}
				this.m_DefaultDeadzoneMin = value;
				this.OnChange();
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x000340DC File Offset: 0x000322DC
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x000340E4 File Offset: 0x000322E4
		public float defaultDeadzoneMax
		{
			get
			{
				return this.m_DefaultDeadzoneMax;
			}
			set
			{
				if (this.m_DefaultDeadzoneMax == value)
				{
					return;
				}
				this.m_DefaultDeadzoneMax = value;
				this.OnChange();
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x000340FD File Offset: 0x000322FD
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x00034105 File Offset: 0x00032305
		public float defaultButtonPressPoint
		{
			get
			{
				return this.m_DefaultButtonPressPoint;
			}
			set
			{
				if (this.m_DefaultButtonPressPoint == value)
				{
					return;
				}
				this.m_DefaultButtonPressPoint = Mathf.Clamp(value, 0.0001f, float.MaxValue);
				this.OnChange();
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0003412D File Offset: 0x0003232D
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x00034135 File Offset: 0x00032335
		public float buttonReleaseThreshold
		{
			get
			{
				return this.m_ButtonReleaseThreshold;
			}
			set
			{
				if (this.m_ButtonReleaseThreshold == value)
				{
					return;
				}
				this.m_ButtonReleaseThreshold = value;
				this.OnChange();
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0003414E File Offset: 0x0003234E
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x00034156 File Offset: 0x00032356
		public float defaultTapTime
		{
			get
			{
				return this.m_DefaultTapTime;
			}
			set
			{
				if (this.m_DefaultTapTime == value)
				{
					return;
				}
				this.m_DefaultTapTime = value;
				this.OnChange();
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0003416F File Offset: 0x0003236F
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x00034177 File Offset: 0x00032377
		public float defaultSlowTapTime
		{
			get
			{
				return this.m_DefaultSlowTapTime;
			}
			set
			{
				if (this.m_DefaultSlowTapTime == value)
				{
					return;
				}
				this.m_DefaultSlowTapTime = value;
				this.OnChange();
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00034190 File Offset: 0x00032390
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x00034198 File Offset: 0x00032398
		public float defaultHoldTime
		{
			get
			{
				return this.m_DefaultHoldTime;
			}
			set
			{
				if (this.m_DefaultHoldTime == value)
				{
					return;
				}
				this.m_DefaultHoldTime = value;
				this.OnChange();
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x000341B1 File Offset: 0x000323B1
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x000341B9 File Offset: 0x000323B9
		public float tapRadius
		{
			get
			{
				return this.m_TapRadius;
			}
			set
			{
				if (this.m_TapRadius == value)
				{
					return;
				}
				this.m_TapRadius = value;
				this.OnChange();
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x000341D2 File Offset: 0x000323D2
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x000341DA File Offset: 0x000323DA
		public float multiTapDelayTime
		{
			get
			{
				return this.m_MultiTapDelayTime;
			}
			set
			{
				if (this.m_MultiTapDelayTime == value)
				{
					return;
				}
				this.m_MultiTapDelayTime = value;
				this.OnChange();
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x000341F3 File Offset: 0x000323F3
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x000341FB File Offset: 0x000323FB
		public InputSettings.BackgroundBehavior backgroundBehavior
		{
			get
			{
				return this.m_BackgroundBehavior;
			}
			set
			{
				if (this.m_BackgroundBehavior == value)
				{
					return;
				}
				this.m_BackgroundBehavior = value;
				this.OnChange();
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x00034214 File Offset: 0x00032414
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x0003421C File Offset: 0x0003241C
		public InputSettings.EditorInputBehaviorInPlayMode editorInputBehaviorInPlayMode
		{
			get
			{
				return this.m_EditorInputBehaviorInPlayMode;
			}
			set
			{
				if (this.m_EditorInputBehaviorInPlayMode == value)
				{
					return;
				}
				this.m_EditorInputBehaviorInPlayMode = value;
				this.OnChange();
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00034235 File Offset: 0x00032435
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x0003423D File Offset: 0x0003243D
		public InputSettings.InputActionPropertyDrawerMode inputActionPropertyDrawerMode
		{
			get
			{
				return this.m_InputActionPropertyDrawerMode;
			}
			set
			{
				if (this.m_InputActionPropertyDrawerMode == value)
				{
					return;
				}
				this.m_InputActionPropertyDrawerMode = value;
				this.OnChange();
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x00034256 File Offset: 0x00032456
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x0003425E File Offset: 0x0003245E
		public int maxEventBytesPerUpdate
		{
			get
			{
				return this.m_MaxEventBytesPerUpdate;
			}
			set
			{
				if (this.m_MaxEventBytesPerUpdate == value)
				{
					return;
				}
				this.m_MaxEventBytesPerUpdate = value;
				this.OnChange();
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00034277 File Offset: 0x00032477
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x0003427F File Offset: 0x0003247F
		public int maxQueuedEventsPerUpdate
		{
			get
			{
				return this.m_MaxQueuedEventsPerUpdate;
			}
			set
			{
				if (this.m_MaxQueuedEventsPerUpdate == value)
				{
					return;
				}
				this.m_MaxQueuedEventsPerUpdate = value;
				this.OnChange();
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00034298 File Offset: 0x00032498
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x000342A8 File Offset: 0x000324A8
		public ReadOnlyArray<string> supportedDevices
		{
			get
			{
				return new ReadOnlyArray<string>(this.m_SupportedDevices);
			}
			set
			{
				if (this.supportedDevices.Count == value.Count)
				{
					bool flag = false;
					for (int i = 0; i < this.supportedDevices.Count; i++)
					{
						if (this.m_SupportedDevices[i] != value[i])
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return;
					}
				}
				this.m_SupportedDevices = value.ToArray();
				this.OnChange();
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00034319 File Offset: 0x00032519
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x00034321 File Offset: 0x00032521
		public bool disableRedundantEventsMerging
		{
			get
			{
				return this.m_DisableRedundantEventsMerging;
			}
			set
			{
				if (this.m_DisableRedundantEventsMerging == value)
				{
					return;
				}
				this.m_DisableRedundantEventsMerging = value;
				this.OnChange();
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0003433A File Offset: 0x0003253A
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x00034342 File Offset: 0x00032542
		public bool shortcutKeysConsumeInput
		{
			get
			{
				return this.m_ShortcutKeysConsumeInputs;
			}
			set
			{
				if (this.m_ShortcutKeysConsumeInputs == value)
				{
					return;
				}
				this.m_ShortcutKeysConsumeInputs = value;
				this.OnChange();
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0003435C File Offset: 0x0003255C
		public void SetInternalFeatureFlag(string featureName, bool enabled)
		{
			if (string.IsNullOrEmpty(featureName))
			{
				throw new ArgumentNullException("featureName");
			}
			if (this.m_FeatureFlags == null)
			{
				this.m_FeatureFlags = new HashSet<string>();
			}
			if (enabled)
			{
				this.m_FeatureFlags.Add(featureName.ToUpperInvariant());
			}
			else
			{
				this.m_FeatureFlags.Remove(featureName.ToUpperInvariant());
			}
			this.OnChange();
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000343BE File Offset: 0x000325BE
		internal bool IsFeatureEnabled(string featureName)
		{
			return this.m_FeatureFlags != null && this.m_FeatureFlags.Contains(featureName.ToUpperInvariant());
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000343DB File Offset: 0x000325DB
		internal void OnChange()
		{
			if (InputSystem.settings == this)
			{
				InputSystem.s_Manager.ApplySettings();
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000343F4 File Offset: 0x000325F4
		private static bool CompareFloats(float a, float b)
		{
			return a - b <= float.Epsilon;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00034404 File Offset: 0x00032604
		private static bool CompareSets<T>(ReadOnlyArray<T> a, ReadOnlyArray<T> b)
		{
			if (a == null)
			{
				return b == null;
			}
			if (b == null)
			{
				return false;
			}
			for (int i = 0; i < a.Count; i++)
			{
				bool flag = false;
				for (int j = 0; j < b.Count; j++)
				{
					T t = a[i];
					if (t.Equals(b[j]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00034482 File Offset: 0x00032682
		private static bool CompareFeatureFlag(InputSettings a, InputSettings b, string featureName)
		{
			return a.IsFeatureEnabled(featureName) == b.IsFeatureEnabled(featureName);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00034494 File Offset: 0x00032694
		internal static bool AreEqual(InputSettings a, InputSettings b)
		{
			if (a == null)
			{
				return b == null;
			}
			return b != null && (a == b || (a.updateMode == b.updateMode && a.compensateForScreenOrientation == b.compensateForScreenOrientation && InputSettings.CompareFloats(a.defaultDeadzoneMin, b.defaultDeadzoneMin) && InputSettings.CompareFloats(a.defaultDeadzoneMax, b.defaultDeadzoneMax) && InputSettings.CompareFloats(a.defaultButtonPressPoint, b.defaultButtonPressPoint) && InputSettings.CompareFloats(a.buttonReleaseThreshold, b.buttonReleaseThreshold) && InputSettings.CompareFloats(a.defaultTapTime, b.defaultTapTime) && InputSettings.CompareFloats(a.defaultSlowTapTime, b.defaultSlowTapTime) && InputSettings.CompareFloats(a.defaultHoldTime, b.defaultHoldTime) && InputSettings.CompareFloats(a.tapRadius, b.tapRadius) && InputSettings.CompareFloats(a.multiTapDelayTime, b.multiTapDelayTime) && a.backgroundBehavior == b.backgroundBehavior && a.editorInputBehaviorInPlayMode == b.editorInputBehaviorInPlayMode && a.inputActionPropertyDrawerMode == b.inputActionPropertyDrawerMode && a.maxEventBytesPerUpdate == b.maxEventBytesPerUpdate && a.maxQueuedEventsPerUpdate == b.maxQueuedEventsPerUpdate && InputSettings.CompareSets<string>(a.supportedDevices, b.supportedDevices) && a.disableRedundantEventsMerging == b.disableRedundantEventsMerging && a.shortcutKeysConsumeInput == b.shortcutKeysConsumeInput && InputSettings.CompareFeatureFlag(a, b, "USE_OPTIMIZED_CONTROLS") && InputSettings.CompareFeatureFlag(a, b, "USE_READ_VALUE_CACHING") && InputSettings.CompareFeatureFlag(a, b, "PARANOID_READ_VALUE_CACHING_CHECKS") && InputSettings.CompareFeatureFlag(a, b, "DISABLE_UNITY_REMOTE_SUPPORT") && InputSettings.CompareFeatureFlag(a, b, "RUN_PLAYER_UPDATES_IN_EDIT_MODE") && InputSettings.CompareFeatureFlag(a, b, "USE_IMGUI_EDITOR_FOR_ASSETS")));
		}

		// Token: 0x040002C7 RID: 711
		[Tooltip("Determine which type of devices are used by the application. By default, this is empty meaning that all devices recognized by Unity will be used. Restricting the set of supported devices will make only those devices appear in the input system.")]
		[SerializeField]
		private string[] m_SupportedDevices;

		// Token: 0x040002C8 RID: 712
		[Tooltip("Determine when Unity processes events. By default, accumulated input events are flushed out before each fixed update and before each dynamic update. This setting can be used to restrict event processing to only where the application needs it.")]
		[SerializeField]
		private InputSettings.UpdateMode m_UpdateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;

		// Token: 0x040002C9 RID: 713
		[SerializeField]
		private InputSettings.ScrollDeltaBehavior m_ScrollDeltaBehavior;

		// Token: 0x040002CA RID: 714
		[SerializeField]
		private int m_MaxEventBytesPerUpdate = 5242880;

		// Token: 0x040002CB RID: 715
		[SerializeField]
		private int m_MaxQueuedEventsPerUpdate = 1000;

		// Token: 0x040002CC RID: 716
		[SerializeField]
		private bool m_CompensateForScreenOrientation = true;

		// Token: 0x040002CD RID: 717
		[SerializeField]
		private InputSettings.BackgroundBehavior m_BackgroundBehavior;

		// Token: 0x040002CE RID: 718
		[SerializeField]
		private InputSettings.EditorInputBehaviorInPlayMode m_EditorInputBehaviorInPlayMode;

		// Token: 0x040002CF RID: 719
		[SerializeField]
		private InputSettings.InputActionPropertyDrawerMode m_InputActionPropertyDrawerMode;

		// Token: 0x040002D0 RID: 720
		[SerializeField]
		private float m_DefaultDeadzoneMin = 0.125f;

		// Token: 0x040002D1 RID: 721
		[SerializeField]
		private float m_DefaultDeadzoneMax = 0.925f;

		// Token: 0x040002D2 RID: 722
		[Min(0.0001f)]
		[SerializeField]
		private float m_DefaultButtonPressPoint = 0.5f;

		// Token: 0x040002D3 RID: 723
		[SerializeField]
		private float m_ButtonReleaseThreshold = 0.75f;

		// Token: 0x040002D4 RID: 724
		[SerializeField]
		private float m_DefaultTapTime = 0.2f;

		// Token: 0x040002D5 RID: 725
		[SerializeField]
		private float m_DefaultSlowTapTime = 0.5f;

		// Token: 0x040002D6 RID: 726
		[SerializeField]
		private float m_DefaultHoldTime = 0.4f;

		// Token: 0x040002D7 RID: 727
		[SerializeField]
		private float m_TapRadius = 5f;

		// Token: 0x040002D8 RID: 728
		[SerializeField]
		private float m_MultiTapDelayTime = 0.75f;

		// Token: 0x040002D9 RID: 729
		[SerializeField]
		private bool m_DisableRedundantEventsMerging;

		// Token: 0x040002DA RID: 730
		[SerializeField]
		private bool m_ShortcutKeysConsumeInputs;

		// Token: 0x040002DB RID: 731
		[NonSerialized]
		internal HashSet<string> m_FeatureFlags;

		// Token: 0x040002DC RID: 732
		internal const int s_OldUnsupportedFixedAndDynamicUpdateSetting = 0;

		// Token: 0x020001B6 RID: 438
		public enum UpdateMode
		{
			// Token: 0x04000950 RID: 2384
			ProcessEventsInDynamicUpdate = 1,
			// Token: 0x04000951 RID: 2385
			ProcessEventsInFixedUpdate,
			// Token: 0x04000952 RID: 2386
			ProcessEventsManually
		}

		// Token: 0x020001B7 RID: 439
		public enum ScrollDeltaBehavior
		{
			// Token: 0x04000954 RID: 2388
			UniformAcrossAllPlatforms,
			// Token: 0x04000955 RID: 2389
			KeepPlatformSpecificInputRange
		}

		// Token: 0x020001B8 RID: 440
		public enum BackgroundBehavior
		{
			// Token: 0x04000957 RID: 2391
			ResetAndDisableNonBackgroundDevices,
			// Token: 0x04000958 RID: 2392
			ResetAndDisableAllDevices,
			// Token: 0x04000959 RID: 2393
			IgnoreFocus
		}

		// Token: 0x020001B9 RID: 441
		public enum EditorInputBehaviorInPlayMode
		{
			// Token: 0x0400095B RID: 2395
			PointersAndKeyboardsRespectGameViewFocus,
			// Token: 0x0400095C RID: 2396
			AllDevicesRespectGameViewFocus,
			// Token: 0x0400095D RID: 2397
			AllDeviceInputAlwaysGoesToGameView
		}

		// Token: 0x020001BA RID: 442
		public enum InputActionPropertyDrawerMode
		{
			// Token: 0x0400095F RID: 2399
			Compact,
			// Token: 0x04000960 RID: 2400
			MultilineEffective,
			// Token: 0x04000961 RID: 2401
			MultilineBoth
		}
	}
}
