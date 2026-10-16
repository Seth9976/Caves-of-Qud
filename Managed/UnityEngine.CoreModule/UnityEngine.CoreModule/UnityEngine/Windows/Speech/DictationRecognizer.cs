using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x02000311 RID: 785
	public sealed class DictationRecognizer : IDisposable
	{
		// Token: 0x060026D5 RID: 9941
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(object self, ConfidenceLevel minimumConfidence, DictationTopicConstraint topicConstraint);

		// Token: 0x060026D6 RID: 9942
		[NativeThrows]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Start(IntPtr self);

		// Token: 0x060026D7 RID: 9943
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop(IntPtr self);

		// Token: 0x060026D8 RID: 9944
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr self);

		// Token: 0x060026D9 RID: 9945
		[ThreadSafe]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyThreaded(IntPtr self);

		// Token: 0x060026DA RID: 9946
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpeechSystemStatus GetStatus(IntPtr self);

		// Token: 0x060026DB RID: 9947
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetAutoSilenceTimeoutSeconds(IntPtr self);

		// Token: 0x060026DC RID: 9948
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAutoSilenceTimeoutSeconds(IntPtr self, float value);

		// Token: 0x060026DD RID: 9949
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetInitialSilenceTimeoutSeconds(IntPtr self);

		// Token: 0x060026DE RID: 9950
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInitialSilenceTimeoutSeconds(IntPtr self, float value);

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060026DF RID: 9951 RVA: 0x0004AB48 File Offset: 0x00048D48
		// (remove) Token: 0x060026E0 RID: 9952 RVA: 0x0004AB80 File Offset: 0x00048D80
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DictationRecognizer.DictationHypothesisDelegate DictationHypothesis;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060026E1 RID: 9953 RVA: 0x0004ABB8 File Offset: 0x00048DB8
		// (remove) Token: 0x060026E2 RID: 9954 RVA: 0x0004ABF0 File Offset: 0x00048DF0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DictationRecognizer.DictationResultDelegate DictationResult;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060026E3 RID: 9955 RVA: 0x0004AC28 File Offset: 0x00048E28
		// (remove) Token: 0x060026E4 RID: 9956 RVA: 0x0004AC60 File Offset: 0x00048E60
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DictationRecognizer.DictationCompletedDelegate DictationComplete;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x060026E5 RID: 9957 RVA: 0x0004AC98 File Offset: 0x00048E98
		// (remove) Token: 0x060026E6 RID: 9958 RVA: 0x0004ACD0 File Offset: 0x00048ED0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DictationRecognizer.DictationErrorHandler DictationError;

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060026E7 RID: 9959 RVA: 0x0004AD08 File Offset: 0x00048F08
		public SpeechSystemStatus Status
		{
			get
			{
				return (this.m_Recognizer != IntPtr.Zero) ? DictationRecognizer.GetStatus(this.m_Recognizer) : SpeechSystemStatus.Stopped;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060026E8 RID: 9960 RVA: 0x0004AD3C File Offset: 0x00048F3C
		// (set) Token: 0x060026E9 RID: 9961 RVA: 0x0004AD78 File Offset: 0x00048F78
		public float AutoSilenceTimeoutSeconds
		{
			get
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				float num;
				if (flag)
				{
					num = 0f;
				}
				else
				{
					num = DictationRecognizer.GetAutoSilenceTimeoutSeconds(this.m_Recognizer);
				}
				return num;
			}
			set
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				if (!flag)
				{
					DictationRecognizer.SetAutoSilenceTimeoutSeconds(this.m_Recognizer, value);
				}
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060026EA RID: 9962 RVA: 0x0004ADAC File Offset: 0x00048FAC
		// (set) Token: 0x060026EB RID: 9963 RVA: 0x0004ADE8 File Offset: 0x00048FE8
		public float InitialSilenceTimeoutSeconds
		{
			get
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				float num;
				if (flag)
				{
					num = 0f;
				}
				else
				{
					num = DictationRecognizer.GetInitialSilenceTimeoutSeconds(this.m_Recognizer);
				}
				return num;
			}
			set
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				if (!flag)
				{
					DictationRecognizer.SetInitialSilenceTimeoutSeconds(this.m_Recognizer, value);
				}
			}
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x0004AE19 File Offset: 0x00049019
		public DictationRecognizer()
			: this(ConfidenceLevel.Medium, DictationTopicConstraint.Dictation)
		{
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x0004AE25 File Offset: 0x00049025
		public DictationRecognizer(ConfidenceLevel confidenceLevel)
			: this(confidenceLevel, DictationTopicConstraint.Dictation)
		{
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x0004AE31 File Offset: 0x00049031
		public DictationRecognizer(DictationTopicConstraint topic)
			: this(ConfidenceLevel.Medium, topic)
		{
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x0004AE3D File Offset: 0x0004903D
		public DictationRecognizer(ConfidenceLevel minimumConfidence, DictationTopicConstraint topic)
		{
			this.m_Recognizer = DictationRecognizer.Create(this, minimumConfidence, topic);
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x0004AE58 File Offset: 0x00049058
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Recognizer != IntPtr.Zero;
				if (flag)
				{
					DictationRecognizer.DestroyThreaded(this.m_Recognizer);
					this.m_Recognizer = IntPtr.Zero;
					GC.SuppressFinalize(this);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x0004AEB8 File Offset: 0x000490B8
		public void Start()
		{
			bool flag = this.m_Recognizer == IntPtr.Zero;
			if (!flag)
			{
				DictationRecognizer.Start(this.m_Recognizer);
			}
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x0004AEE8 File Offset: 0x000490E8
		public void Stop()
		{
			bool flag = this.m_Recognizer == IntPtr.Zero;
			if (!flag)
			{
				DictationRecognizer.Stop(this.m_Recognizer);
			}
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x0004AF18 File Offset: 0x00049118
		public void Dispose()
		{
			bool flag = this.m_Recognizer != IntPtr.Zero;
			if (flag)
			{
				DictationRecognizer.Destroy(this.m_Recognizer);
				this.m_Recognizer = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x0004AF5C File Offset: 0x0004915C
		[RequiredByNativeCode]
		private unsafe void DictationRecognizer_InvokeHypothesisGeneratedEvent(IntPtr keyword, int keywordLength)
		{
			DictationRecognizer.DictationHypothesisDelegate dictationHypothesis = this.DictationHypothesis;
			bool flag = dictationHypothesis != null;
			if (flag)
			{
				dictationHypothesis(new string((char*)(void*)keyword, 0, keywordLength));
			}
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x0004AF90 File Offset: 0x00049190
		[RequiredByNativeCode]
		private unsafe void DictationRecognizer_InvokeResultGeneratedEvent(IntPtr keyword, int keywordLength, ConfidenceLevel minimumConfidence)
		{
			DictationRecognizer.DictationResultDelegate dictationResult = this.DictationResult;
			bool flag = dictationResult != null;
			if (flag)
			{
				dictationResult(new string((char*)(void*)keyword, 0, keywordLength), minimumConfidence);
			}
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x0004AFC4 File Offset: 0x000491C4
		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeCompletedEvent(DictationCompletionCause cause)
		{
			DictationRecognizer.DictationCompletedDelegate dictationComplete = this.DictationComplete;
			bool flag = dictationComplete != null;
			if (flag)
			{
				dictationComplete(cause);
			}
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x0004AFEC File Offset: 0x000491EC
		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeErrorEvent(string error, int hresult)
		{
			DictationRecognizer.DictationErrorHandler dictationError = this.DictationError;
			bool flag = dictationError != null;
			if (flag)
			{
				dictationError(error, hresult);
			}
		}

		// Token: 0x04000AC9 RID: 2761
		private IntPtr m_Recognizer;

		// Token: 0x02000312 RID: 786
		// (Invoke) Token: 0x060026F9 RID: 9977
		public delegate void DictationHypothesisDelegate(string text);

		// Token: 0x02000313 RID: 787
		// (Invoke) Token: 0x060026FD RID: 9981
		public delegate void DictationResultDelegate(string text, ConfidenceLevel confidence);

		// Token: 0x02000314 RID: 788
		// (Invoke) Token: 0x06002701 RID: 9985
		public delegate void DictationCompletedDelegate(DictationCompletionCause cause);

		// Token: 0x02000315 RID: 789
		// (Invoke) Token: 0x06002705 RID: 9989
		public delegate void DictationErrorHandler(string error, int hresult);
	}
}
