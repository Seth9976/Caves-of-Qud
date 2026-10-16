using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020002E7 RID: 743
	[NativeHeader("Runtime/Export/TouchScreenKeyboard/TouchScreenKeyboard.bindings.h")]
	[NativeConditional("ENABLE_ONSCREEN_KEYBOARD")]
	[NativeHeader("Runtime/Input/KeyboardOnScreen.h")]
	public class TouchScreenKeyboard
	{
		// Token: 0x06002490 RID: 9360
		[FreeFunction("TouchScreenKeyboard_Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06002491 RID: 9361 RVA: 0x000468A8 File Offset: 0x00044AA8
		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				TouchScreenKeyboard.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000468EC File Offset: 0x00044AEC
		~TouchScreenKeyboard()
		{
			this.Destroy();
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x0004691C File Offset: 0x00044B1C
		public TouchScreenKeyboard(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert, string textPlaceholder, int characterLimit)
		{
			TouchScreenKeyboard_InternalConstructorHelperArguments touchScreenKeyboard_InternalConstructorHelperArguments = default(TouchScreenKeyboard_InternalConstructorHelperArguments);
			touchScreenKeyboard_InternalConstructorHelperArguments.keyboardType = Convert.ToUInt32(keyboardType);
			touchScreenKeyboard_InternalConstructorHelperArguments.autocorrection = Convert.ToUInt32(autocorrection);
			touchScreenKeyboard_InternalConstructorHelperArguments.multiline = Convert.ToUInt32(multiline);
			touchScreenKeyboard_InternalConstructorHelperArguments.secure = Convert.ToUInt32(secure);
			touchScreenKeyboard_InternalConstructorHelperArguments.alert = Convert.ToUInt32(alert);
			touchScreenKeyboard_InternalConstructorHelperArguments.characterLimit = characterLimit;
			this.m_Ptr = TouchScreenKeyboard.TouchScreenKeyboard_InternalConstructorHelper(ref touchScreenKeyboard_InternalConstructorHelperArguments, text, textPlaceholder);
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x0004699C File Offset: 0x00044B9C
		[FreeFunction("TouchScreenKeyboard_InternalConstructorHelper")]
		private unsafe static IntPtr TouchScreenKeyboard_InternalConstructorHelper(ref TouchScreenKeyboard_InternalConstructorHelperArguments arguments, string text, string textPlaceholder)
		{
			IntPtr intPtr;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(text, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = text.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(textPlaceholder, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = textPlaceholder.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				intPtr = TouchScreenKeyboard.TouchScreenKeyboard_InternalConstructorHelper_Injected(ref arguments, ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return intPtr;
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06002495 RID: 9365 RVA: 0x00046A28 File Offset: 0x00044C28
		public static bool isSupported
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				RuntimePlatform runtimePlatform = platform;
				RuntimePlatform runtimePlatform2 = runtimePlatform;
				if (runtimePlatform2 <= RuntimePlatform.MetroPlayerARM)
				{
					if (runtimePlatform2 != RuntimePlatform.IPhonePlayer && runtimePlatform2 != RuntimePlatform.Android && runtimePlatform2 - RuntimePlatform.WebGLPlayer > 3)
					{
						goto IL_0051;
					}
				}
				else if (runtimePlatform2 <= RuntimePlatform.Switch)
				{
					if (runtimePlatform2 != RuntimePlatform.PS4 && runtimePlatform2 - RuntimePlatform.tvOS > 1)
					{
						goto IL_0051;
					}
				}
				else if (runtimePlatform2 - RuntimePlatform.GameCoreXboxSeries > 2 && runtimePlatform2 - RuntimePlatform.VisionOS > 1)
				{
					goto IL_0051;
				}
				return true;
				IL_0051:
				return false;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06002496 RID: 9366 RVA: 0x00046A8B File Offset: 0x00044C8B
		// (set) Token: 0x06002497 RID: 9367 RVA: 0x00046A92 File Offset: 0x00044C92
		internal static bool disableInPlaceEditing { get; set; }

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06002498 RID: 9368 RVA: 0x00046A9C File Offset: 0x00044C9C
		public static bool isInPlaceEditingAllowed
		{
			get
			{
				bool disableInPlaceEditing = TouchScreenKeyboard.disableInPlaceEditing;
				return !disableInPlaceEditing && TouchScreenKeyboard.IsInPlaceEditingAllowed();
			}
		}

		// Token: 0x06002499 RID: 9369
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsInPlaceEditingAllowed();

		// Token: 0x0600249A RID: 9370 RVA: 0x00046AC0 File Offset: 0x00044CC0
		public static TouchScreenKeyboard Open(string text, [DefaultValue("TouchScreenKeyboardType.Default")] TouchScreenKeyboardType keyboardType, [DefaultValue("true")] bool autocorrection, [DefaultValue("false")] bool multiline, [DefaultValue("false")] bool secure, [DefaultValue("false")] bool alert, [DefaultValue("\"\"")] string textPlaceholder, [DefaultValue("0")] int characterLimit)
		{
			return new TouchScreenKeyboard(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, characterLimit);
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x00046AE4 File Offset: 0x00044CE4
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert, string textPlaceholder)
		{
			int num = 0;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, num);
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x00046B08 File Offset: 0x00044D08
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert)
		{
			int num = 0;
			string text2 = "";
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, text2, num);
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x00046B34 File Offset: 0x00044D34
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure)
		{
			int num = 0;
			string text2 = "";
			bool flag = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, flag, text2, num);
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x00046B60 File Offset: 0x00044D60
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline)
		{
			int num = 0;
			string text2 = "";
			bool flag = false;
			bool flag2 = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, flag2, flag, text2, num);
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x00046B90 File Offset: 0x00044D90
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection)
		{
			int num = 0;
			string text2 = "";
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, flag3, flag2, flag, text2, num);
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x00046BC4 File Offset: 0x00044DC4
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType)
		{
			int num = 0;
			string text2 = "";
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = true;
			return TouchScreenKeyboard.Open(text, keyboardType, flag4, flag3, flag2, flag, text2, num);
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x00046BFC File Offset: 0x00044DFC
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text)
		{
			int num = 0;
			string text2 = "";
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = true;
			TouchScreenKeyboardType touchScreenKeyboardType = TouchScreenKeyboardType.Default;
			return TouchScreenKeyboard.Open(text, touchScreenKeyboardType, flag4, flag3, flag2, flag, text2, num);
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060024A2 RID: 9378 RVA: 0x00046C38 File Offset: 0x00044E38
		// (set) Token: 0x060024A3 RID: 9379 RVA: 0x00046C78 File Offset: 0x00044E78
		public unsafe string text
		{
			[NativeName("GetText")]
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					TouchScreenKeyboard.get_text_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			[NativeName("SetText")]
			set
			{
				try
				{
					IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					TouchScreenKeyboard.set_text_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060024A4 RID: 9380
		// (set) Token: 0x060024A5 RID: 9381
		public static extern bool hideInput
		{
			[NativeName("IsInputHidden")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetInputHidden")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060024A6 RID: 9382
		public static extern TouchScreenKeyboard.InputFieldAppearance inputFieldAppearance
		{
			[NativeName("GetInputFieldAppearance")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060024A7 RID: 9383 RVA: 0x00046CDC File Offset: 0x00044EDC
		// (set) Token: 0x060024A8 RID: 9384 RVA: 0x00046D00 File Offset: 0x00044F00
		public bool active
		{
			[NativeName("IsActive")]
			get
			{
				IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TouchScreenKeyboard.get_active_Injected(intPtr);
			}
			[NativeName("SetActive")]
			set
			{
				IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TouchScreenKeyboard.set_active_Injected(intPtr, value);
			}
		}

		// Token: 0x060024A9 RID: 9385
		[FreeFunction("TouchScreenKeyboard_GetDone")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetDone(IntPtr ptr);

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060024AA RID: 9386 RVA: 0x00046D24 File Offset: 0x00044F24
		[Obsolete("Property done is deprecated, use status instead")]
		public bool done
		{
			get
			{
				return TouchScreenKeyboard.GetDone(this.m_Ptr);
			}
		}

		// Token: 0x060024AB RID: 9387
		[FreeFunction("TouchScreenKeyboard_GetWasCanceled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetWasCanceled(IntPtr ptr);

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x00046D44 File Offset: 0x00044F44
		[Obsolete("Property wasCanceled is deprecated, use status instead.")]
		public bool wasCanceled
		{
			get
			{
				return TouchScreenKeyboard.GetWasCanceled(this.m_Ptr);
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060024AD RID: 9389 RVA: 0x00046D64 File Offset: 0x00044F64
		public TouchScreenKeyboard.Status status
		{
			[NativeName("GetKeyboardStatus")]
			get
			{
				IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TouchScreenKeyboard.get_status_Injected(intPtr);
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060024AE RID: 9390 RVA: 0x00046D88 File Offset: 0x00044F88
		// (set) Token: 0x060024AF RID: 9391 RVA: 0x00046DAC File Offset: 0x00044FAC
		public int characterLimit
		{
			[NativeName("GetCharacterLimit")]
			get
			{
				IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TouchScreenKeyboard.get_characterLimit_Injected(intPtr);
			}
			[NativeName("SetCharacterLimit")]
			set
			{
				IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TouchScreenKeyboard.set_characterLimit_Injected(intPtr, value);
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060024B0 RID: 9392 RVA: 0x00046DD0 File Offset: 0x00044FD0
		public bool canGetSelection
		{
			[NativeName("CanGetSelection")]
			get
			{
				IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TouchScreenKeyboard.get_canGetSelection_Injected(intPtr);
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060024B1 RID: 9393 RVA: 0x00046DF4 File Offset: 0x00044FF4
		public bool canSetSelection
		{
			[NativeName("CanSetSelection")]
			get
			{
				IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TouchScreenKeyboard.get_canSetSelection_Injected(intPtr);
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x00046E18 File Offset: 0x00045018
		// (set) Token: 0x060024B3 RID: 9395 RVA: 0x00046E40 File Offset: 0x00045040
		public RangeInt selection
		{
			get
			{
				RangeInt rangeInt;
				TouchScreenKeyboard.GetSelection(out rangeInt.start, out rangeInt.length);
				return rangeInt;
			}
			set
			{
				bool flag = string.IsNullOrEmpty(this.text);
				if (flag)
				{
					TouchScreenKeyboard.SetSelection(0, 0);
				}
				else
				{
					bool flag2 = value.start < 0 || value.length < 0 || value.start + value.length > this.text.Length;
					if (flag2)
					{
						throw new ArgumentOutOfRangeException("selection", "Selection is out of range.");
					}
					TouchScreenKeyboard.SetSelection(value.start, value.length);
				}
			}
		}

		// Token: 0x060024B4 RID: 9396
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSelection(out int start, out int length);

		// Token: 0x060024B5 RID: 9397
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSelection(int start, int length);

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x00046EBC File Offset: 0x000450BC
		public TouchScreenKeyboardType type
		{
			[NativeName("GetKeyboardType")]
			get
			{
				IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TouchScreenKeyboard.get_type_Injected(intPtr);
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060024B7 RID: 9399 RVA: 0x00046EE0 File Offset: 0x000450E0
		// (set) Token: 0x060024B8 RID: 9400 RVA: 0x00002925 File Offset: 0x00000B25
		public int targetDisplay
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060024B9 RID: 9401 RVA: 0x00046EF4 File Offset: 0x000450F4
		[NativeConditional("ENABLE_ONSCREEN_KEYBOARD", "RectT<float>()")]
		public static Rect area
		{
			[NativeName("GetRect")]
			get
			{
				Rect rect;
				TouchScreenKeyboard.get_area_Injected(out rect);
				return rect;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060024BA RID: 9402
		public static extern bool visible
		{
			[NativeName("IsVisible")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060024BB RID: 9403
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr TouchScreenKeyboard_InternalConstructorHelper_Injected(ref TouchScreenKeyboard_InternalConstructorHelperArguments arguments, ref ManagedSpanWrapper text, ref ManagedSpanWrapper textPlaceholder);

		// Token: 0x060024BC RID: 9404
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_text_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x060024BD RID: 9405
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_text_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x060024BE RID: 9406
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_active_Injected(IntPtr _unity_self);

		// Token: 0x060024BF RID: 9407
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_active_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060024C0 RID: 9408
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TouchScreenKeyboard.Status get_status_Injected(IntPtr _unity_self);

		// Token: 0x060024C1 RID: 9409
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_characterLimit_Injected(IntPtr _unity_self);

		// Token: 0x060024C2 RID: 9410
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_characterLimit_Injected(IntPtr _unity_self, int value);

		// Token: 0x060024C3 RID: 9411
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canGetSelection_Injected(IntPtr _unity_self);

		// Token: 0x060024C4 RID: 9412
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canSetSelection_Injected(IntPtr _unity_self);

		// Token: 0x060024C5 RID: 9413
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TouchScreenKeyboardType get_type_Injected(IntPtr _unity_self);

		// Token: 0x060024C6 RID: 9414
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_area_Injected(out Rect ret);

		// Token: 0x04000A59 RID: 2649
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x020002E8 RID: 744
		public enum Status
		{
			// Token: 0x04000A5C RID: 2652
			Visible,
			// Token: 0x04000A5D RID: 2653
			Done,
			// Token: 0x04000A5E RID: 2654
			Canceled,
			// Token: 0x04000A5F RID: 2655
			LostFocus
		}

		// Token: 0x020002E9 RID: 745
		public enum InputFieldAppearance
		{
			// Token: 0x04000A61 RID: 2657
			Customizable,
			// Token: 0x04000A62 RID: 2658
			AlwaysVisible,
			// Token: 0x04000A63 RID: 2659
			AlwaysHidden
		}

		// Token: 0x020002EA RID: 746
		public class Android
		{
			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x060024C7 RID: 9415 RVA: 0x00046F0C File Offset: 0x0004510C
			// (set) Token: 0x060024C8 RID: 9416 RVA: 0x00046F23 File Offset: 0x00045123
			[Obsolete("TouchScreenKeyboard.Android.closeKeyboardOnOutsideTap is obsolete. Use TouchScreenKeyboard.Android.consumesOutsideTouches instead (UnityUpgradable) -> UnityEngine.TouchScreenKeyboard/Android.consumesOutsideTouches")]
			public static bool closeKeyboardOnOutsideTap
			{
				get
				{
					return TouchScreenKeyboard.Android.consumesOutsideTouches;
				}
				set
				{
					TouchScreenKeyboard.Android.consumesOutsideTouches = value;
				}
			}

			// Token: 0x170005FA RID: 1530
			// (get) Token: 0x060024CA RID: 9418 RVA: 0x00046F38 File Offset: 0x00045138
			// (set) Token: 0x060024C9 RID: 9417 RVA: 0x00046F2D File Offset: 0x0004512D
			[Obsolete("consumesOutsideTouches is deprecated and will be removed in a future version where Unity will always process touch input outside of the on-screen keyboard (consumesOutsideTouches = false)")]
			public static bool consumesOutsideTouches
			{
				get
				{
					return TouchScreenKeyboard.Android.TouchScreenKeyboard_GetAndroidKeyboardConsumesOutsideTouches();
				}
				set
				{
					TouchScreenKeyboard.Android.TouchScreenKeyboard_SetAndroidKeyboardConsumesOutsideTouches(value);
				}
			}

			// Token: 0x060024CB RID: 9419
			[FreeFunction("TouchScreenKeyboard_SetAndroidKeyboardConsumesOutsideTouches")]
			[NativeConditional("PLATFORM_ANDROID")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void TouchScreenKeyboard_SetAndroidKeyboardConsumesOutsideTouches(bool enable);

			// Token: 0x060024CC RID: 9420
			[FreeFunction("TouchScreenKeyboard_GetAndroidKeyboardConsumesOutsideTouches")]
			[NativeConditional("PLATFORM_ANDROID")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool TouchScreenKeyboard_GetAndroidKeyboardConsumesOutsideTouches();
		}

		// Token: 0x020002EB RID: 747
		internal static class BindingsMarshaller
		{
			// Token: 0x060024CE RID: 9422 RVA: 0x00046F4F File Offset: 0x0004514F
			public static IntPtr ConvertToNative(TouchScreenKeyboard touchScreenKeyboard)
			{
				return touchScreenKeyboard.m_Ptr;
			}
		}
	}
}
