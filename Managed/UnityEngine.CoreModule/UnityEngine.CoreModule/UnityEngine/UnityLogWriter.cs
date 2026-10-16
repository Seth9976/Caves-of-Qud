using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200020C RID: 524
	[NativeHeader("Runtime/Export/Logging/UnityLogWriter.bindings.h")]
	internal class UnityLogWriter : TextWriter
	{
		// Token: 0x06001B80 RID: 7040 RVA: 0x00030FE8 File Offset: 0x0002F1E8
		[ThreadAndSerializationSafe]
		public static void WriteStringToUnityLog(string s)
		{
			bool flag = s == null;
			if (!flag)
			{
				UnityLogWriter.WriteStringToUnityLogImpl(s);
			}
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x00031008 File Offset: 0x0002F208
		[FreeFunction(IsThreadSafe = true)]
		private unsafe static void WriteStringToUnityLogImpl(string s)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(s, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = s.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				UnityLogWriter.WriteStringToUnityLogImpl_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0003105C File Offset: 0x0002F25C
		public static void Init()
		{
			TextWriter textWriter = TextWriter.Synchronized(new UnityLogWriter());
			Console.SetOut(textWriter);
			Console.SetError(textWriter);
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x00031084 File Offset: 0x0002F284
		public override Encoding Encoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0003109B File Offset: 0x0002F29B
		public override void Write(char value)
		{
			UnityLogWriter.WriteStringToUnityLog(value.ToString());
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x000310AB File Offset: 0x0002F2AB
		public override void Write(string s)
		{
			UnityLogWriter.WriteStringToUnityLog(s);
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x000310B5 File Offset: 0x0002F2B5
		public override void Write(char[] buffer, int index, int count)
		{
			UnityLogWriter.WriteStringToUnityLogImpl(new string(buffer, index, count));
		}

		// Token: 0x06001B88 RID: 7048
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WriteStringToUnityLogImpl_Injected(ref ManagedSpanWrapper s);
	}
}
