using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x0200030F RID: 783
	public abstract class PhraseRecognizer : IDisposable
	{
		// Token: 0x060026BF RID: 9919
		[NativeThrows]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected static extern IntPtr CreateFromKeywords(object self, [Unmarshalled] string[] keywords, ConfidenceLevel minimumConfidence);

		// Token: 0x060026C0 RID: 9920 RVA: 0x0004A83C File Offset: 0x00048A3C
		[NativeThrows]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		protected unsafe static IntPtr CreateFromGrammarFile(object self, string grammarFilePath, ConfidenceLevel minimumConfidence)
		{
			IntPtr intPtr;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(grammarFilePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = grammarFilePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				intPtr = PhraseRecognizer.CreateFromGrammarFile_Injected(self, ref managedSpanWrapper, minimumConfidence);
			}
			finally
			{
				char* ptr = null;
			}
			return intPtr;
		}

		// Token: 0x060026C1 RID: 9921
		[NativeThrows]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Start_Internal(IntPtr recognizer);

		// Token: 0x060026C2 RID: 9922
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop_Internal(IntPtr recognizer);

		// Token: 0x060026C3 RID: 9923
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsRunning_Internal(IntPtr recognizer);

		// Token: 0x060026C4 RID: 9924
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr recognizer);

		// Token: 0x060026C5 RID: 9925
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyThreaded(IntPtr recognizer);

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060026C6 RID: 9926 RVA: 0x0004A894 File Offset: 0x00048A94
		// (remove) Token: 0x060026C7 RID: 9927 RVA: 0x0004A8CC File Offset: 0x00048ACC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PhraseRecognizer.PhraseRecognizedDelegate OnPhraseRecognized;

		// Token: 0x060026C8 RID: 9928 RVA: 0x00030B93 File Offset: 0x0002ED93
		internal PhraseRecognizer()
		{
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x0004A904 File Offset: 0x00048B04
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Recognizer != IntPtr.Zero;
				if (flag)
				{
					PhraseRecognizer.DestroyThreaded(this.m_Recognizer);
					this.m_Recognizer = IntPtr.Zero;
					GC.SuppressFinalize(this);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x0004A964 File Offset: 0x00048B64
		public void Start()
		{
			bool flag = this.m_Recognizer == IntPtr.Zero;
			if (!flag)
			{
				PhraseRecognizer.Start_Internal(this.m_Recognizer);
			}
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x0004A994 File Offset: 0x00048B94
		public void Stop()
		{
			bool flag = this.m_Recognizer == IntPtr.Zero;
			if (!flag)
			{
				PhraseRecognizer.Stop_Internal(this.m_Recognizer);
			}
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x0004A9C4 File Offset: 0x00048BC4
		public void Dispose()
		{
			bool flag = this.m_Recognizer != IntPtr.Zero;
			if (flag)
			{
				PhraseRecognizer.Destroy(this.m_Recognizer);
				this.m_Recognizer = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060026CD RID: 9933 RVA: 0x0004AA08 File Offset: 0x00048C08
		public bool IsRunning
		{
			get
			{
				return this.m_Recognizer != IntPtr.Zero && PhraseRecognizer.IsRunning_Internal(this.m_Recognizer);
			}
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x0004AA3C File Offset: 0x00048C3C
		[RequiredByNativeCode]
		private unsafe void InvokePhraseRecognizedEvent(IntPtr rawText, int rawTextLength, ConfidenceLevel confidence, SemanticMeaning[] semanticMeanings, long phraseStartFileTime, long phraseDurationTicks)
		{
			PhraseRecognizer.PhraseRecognizedDelegate onPhraseRecognized = this.OnPhraseRecognized;
			bool flag = onPhraseRecognized != null;
			if (flag)
			{
				onPhraseRecognized(new PhraseRecognizedEventArgs(new string((char*)(void*)rawText, 0, rawTextLength), confidence, semanticMeanings, DateTime.FromFileTime(phraseStartFileTime), TimeSpan.FromTicks(phraseDurationTicks)));
			}
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x0004AA84 File Offset: 0x00048C84
		[RequiredByNativeCode]
		private unsafe static SemanticMeaning[] MarshalSemanticMeaning(IntPtr keys, IntPtr values, IntPtr valueSizes, int valueCount)
		{
			SemanticMeaning[] array = new SemanticMeaning[valueCount];
			int num = 0;
			for (int i = 0; i < valueCount; i++)
			{
				uint num2 = *(uint*)((byte*)(void*)valueSizes + (IntPtr)i * 4);
				SemanticMeaning semanticMeaning = new SemanticMeaning
				{
					key = new string(*(IntPtr*)((byte*)(void*)keys + (IntPtr)i * (IntPtr)sizeof(char*))),
					values = new string[num2]
				};
				int num3 = 0;
				while ((long)num3 < (long)((ulong)num2))
				{
					semanticMeaning.values[num3] = new string(*(IntPtr*)((byte*)(void*)values + (IntPtr)(num + num3) * (IntPtr)sizeof(char*)));
					num3++;
				}
				array[i] = semanticMeaning;
				num += (int)num2;
			}
			return array;
		}

		// Token: 0x060026D0 RID: 9936
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateFromGrammarFile_Injected(object self, ref ManagedSpanWrapper grammarFilePath, ConfidenceLevel minimumConfidence);

		// Token: 0x04000AC7 RID: 2759
		protected IntPtr m_Recognizer;

		// Token: 0x02000310 RID: 784
		// (Invoke) Token: 0x060026D2 RID: 9938
		public delegate void PhraseRecognizedDelegate(PhraseRecognizedEventArgs args);
	}
}
