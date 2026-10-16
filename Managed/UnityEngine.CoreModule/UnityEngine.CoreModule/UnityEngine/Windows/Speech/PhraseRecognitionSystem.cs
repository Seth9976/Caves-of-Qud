using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x0200030C RID: 780
	public static class PhraseRecognitionSystem
	{
		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060026AD RID: 9901
		public static extern bool isSupported
		{
			[ThreadSafe]
			[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060026AE RID: 9902
		public static extern SpeechSystemStatus Status
		{
			[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060026AF RID: 9903
		[NativeThrows]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Restart();

		// Token: 0x060026B0 RID: 9904
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Shutdown();

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060026B1 RID: 9905 RVA: 0x0004A724 File Offset: 0x00048924
		// (remove) Token: 0x060026B2 RID: 9906 RVA: 0x0004A758 File Offset: 0x00048958
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event PhraseRecognitionSystem.ErrorDelegate OnError;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060026B3 RID: 9907 RVA: 0x0004A78C File Offset: 0x0004898C
		// (remove) Token: 0x060026B4 RID: 9908 RVA: 0x0004A7C0 File Offset: 0x000489C0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event PhraseRecognitionSystem.StatusDelegate OnStatusChanged;

		// Token: 0x060026B5 RID: 9909 RVA: 0x0004A7F4 File Offset: 0x000489F4
		[RequiredByNativeCode]
		private static void PhraseRecognitionSystem_InvokeErrorEvent(SpeechError errorCode)
		{
			PhraseRecognitionSystem.ErrorDelegate onError = PhraseRecognitionSystem.OnError;
			bool flag = onError != null;
			if (flag)
			{
				onError(errorCode);
			}
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x0004A818 File Offset: 0x00048A18
		[RequiredByNativeCode]
		private static void PhraseRecognitionSystem_InvokeStatusChangedEvent(SpeechSystemStatus status)
		{
			PhraseRecognitionSystem.StatusDelegate onStatusChanged = PhraseRecognitionSystem.OnStatusChanged;
			bool flag = onStatusChanged != null;
			if (flag)
			{
				onStatusChanged(status);
			}
		}

		// Token: 0x0200030D RID: 781
		// (Invoke) Token: 0x060026B8 RID: 9912
		public delegate void ErrorDelegate(SpeechError errorCode);

		// Token: 0x0200030E RID: 782
		// (Invoke) Token: 0x060026BC RID: 9916
		public delegate void StatusDelegate(SpeechSystemStatus status);
	}
}
