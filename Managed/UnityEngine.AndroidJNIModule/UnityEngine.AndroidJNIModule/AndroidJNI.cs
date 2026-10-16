using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000F RID: 15
	[StaticAccessor("AndroidJNIBindingsHelpers", StaticAccessorType.DoubleColon)]
	[NativeConditional("PLATFORM_ANDROID")]
	[NativeHeader("Modules/AndroidJNI/Public/AndroidJNIBindingsHelpers.h")]
	public static class AndroidJNI
	{
		// Token: 0x060000AF RID: 175 RVA: 0x000072F0 File Offset: 0x000054F0
		[ThreadSafe]
		private static void ReleaseStringChars(AndroidJNI.JStringBinding str)
		{
			AndroidJNI.ReleaseStringChars_Injected(ref str);
		}

		// Token: 0x060000B0 RID: 176
		[StaticAccessor("jni", StaticAccessorType.DoubleColon)]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetJavaVM();

		// Token: 0x060000B1 RID: 177
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int AttachCurrentThread();

		// Token: 0x060000B2 RID: 178
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int DetachCurrentThread();

		// Token: 0x060000B3 RID: 179 RVA: 0x00007304 File Offset: 0x00005504
		[RequiredByNativeCode]
		private static void InvokeAction(Action action)
		{
			action();
		}

		// Token: 0x060000B4 RID: 180
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void InvokeAttached(Action action);

		// Token: 0x060000B5 RID: 181
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetVersion();

		// Token: 0x060000B6 RID: 182 RVA: 0x00007310 File Offset: 0x00005510
		[ThreadSafe]
		public unsafe static IntPtr FindClass(string name)
		{
			IntPtr intPtr;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				intPtr = AndroidJNI.FindClass_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return intPtr;
		}

		// Token: 0x060000B7 RID: 183
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr FromReflectedMethod(IntPtr refMethod);

		// Token: 0x060000B8 RID: 184
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr FromReflectedField(IntPtr refField);

		// Token: 0x060000B9 RID: 185
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToReflectedMethod(IntPtr clazz, IntPtr methodID, bool isStatic);

		// Token: 0x060000BA RID: 186
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToReflectedField(IntPtr clazz, IntPtr fieldID, bool isStatic);

		// Token: 0x060000BB RID: 187
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetSuperclass(IntPtr clazz);

		// Token: 0x060000BC RID: 188
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsAssignableFrom(IntPtr clazz1, IntPtr clazz2);

		// Token: 0x060000BD RID: 189
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Throw(IntPtr obj);

		// Token: 0x060000BE RID: 190 RVA: 0x00007368 File Offset: 0x00005568
		[ThreadSafe]
		public unsafe static int ThrowNew(IntPtr clazz, string message)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(message, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = message.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = AndroidJNI.ThrowNew_Injected(clazz, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x060000BF RID: 191
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ExceptionOccurred();

		// Token: 0x060000C0 RID: 192
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ExceptionDescribe();

		// Token: 0x060000C1 RID: 193
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ExceptionClear();

		// Token: 0x060000C2 RID: 194 RVA: 0x000073C0 File Offset: 0x000055C0
		[ThreadSafe]
		public unsafe static void FatalError(string message)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(message, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = message.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				AndroidJNI.FatalError_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060000C3 RID: 195
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int PushLocalFrame(int capacity);

		// Token: 0x060000C4 RID: 196
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr PopLocalFrame(IntPtr ptr);

		// Token: 0x060000C5 RID: 197
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewGlobalRef(IntPtr obj);

		// Token: 0x060000C6 RID: 198
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteGlobalRef(IntPtr obj);

		// Token: 0x060000C7 RID: 199
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void QueueDeleteGlobalRef(IntPtr obj);

		// Token: 0x060000C8 RID: 200
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetQueueGlobalRefsCount();

		// Token: 0x060000C9 RID: 201
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CleanQueueGlobalRefs();

		// Token: 0x060000CA RID: 202
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewWeakGlobalRef(IntPtr obj);

		// Token: 0x060000CB RID: 203
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteWeakGlobalRef(IntPtr obj);

		// Token: 0x060000CC RID: 204
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewLocalRef(IntPtr obj);

		// Token: 0x060000CD RID: 205
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteLocalRef(IntPtr obj);

		// Token: 0x060000CE RID: 206
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsSameObject(IntPtr obj1, IntPtr obj2);

		// Token: 0x060000CF RID: 207
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int EnsureLocalCapacity(int capacity);

		// Token: 0x060000D0 RID: 208
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr AllocObject(IntPtr clazz);

		// Token: 0x060000D1 RID: 209 RVA: 0x00007414 File Offset: 0x00005614
		public static IntPtr NewObject(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.NewObject(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007434 File Offset: 0x00005634
		public unsafe static IntPtr NewObject(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.NewObjectA(clazz, methodID, ptr);
			}
		}

		// Token: 0x060000D3 RID: 211
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr NewObjectA(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x060000D4 RID: 212
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetObjectClass(IntPtr obj);

		// Token: 0x060000D5 RID: 213
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsInstanceOf(IntPtr obj, IntPtr clazz);

		// Token: 0x060000D6 RID: 214 RVA: 0x0000745C File Offset: 0x0000565C
		[ThreadSafe]
		public unsafe static IntPtr GetMethodID(IntPtr clazz, string name, string sig)
		{
			IntPtr methodID_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(sig, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = sig.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				methodID_Injected = AndroidJNI.GetMethodID_Injected(clazz, ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return methodID_Injected;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000074E8 File Offset: 0x000056E8
		[ThreadSafe]
		public unsafe static IntPtr GetFieldID(IntPtr clazz, string name, string sig)
		{
			IntPtr fieldID_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(sig, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = sig.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				fieldID_Injected = AndroidJNI.GetFieldID_Injected(clazz, ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return fieldID_Injected;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00007574 File Offset: 0x00005774
		[ThreadSafe]
		public unsafe static IntPtr GetStaticMethodID(IntPtr clazz, string name, string sig)
		{
			IntPtr staticMethodID_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(sig, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = sig.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				staticMethodID_Injected = AndroidJNI.GetStaticMethodID_Injected(clazz, ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return staticMethodID_Injected;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00007600 File Offset: 0x00005800
		[ThreadSafe]
		public unsafe static IntPtr GetStaticFieldID(IntPtr clazz, string name, string sig)
		{
			IntPtr staticFieldID_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(sig, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = sig.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				staticFieldID_Injected = AndroidJNI.GetStaticFieldID_Injected(clazz, ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return staticFieldID_Injected;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000768C File Offset: 0x0000588C
		public static IntPtr NewString(string chars)
		{
			return AndroidJNI.NewStringFromStr(chars);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000076A4 File Offset: 0x000058A4
		[ThreadSafe]
		private unsafe static IntPtr NewStringFromStr(string chars)
		{
			IntPtr intPtr;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(chars, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = chars.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				intPtr = AndroidJNI.NewStringFromStr_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return intPtr;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000076FC File Offset: 0x000058FC
		[ThreadSafe]
		public unsafe static IntPtr NewString(char[] chars)
		{
			Span<char> span = new Span<char>(chars);
			IntPtr intPtr;
			fixed (char* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				intPtr = AndroidJNI.NewString_Injected(ref managedSpanWrapper);
			}
			return intPtr;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00007734 File Offset: 0x00005934
		[ThreadSafe]
		public unsafe static IntPtr NewStringUTF(string bytes)
		{
			IntPtr intPtr;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(bytes, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = bytes.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				intPtr = AndroidJNI.NewStringUTF_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return intPtr;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000778C File Offset: 0x0000598C
		public static string GetStringChars(IntPtr str)
		{
			string text;
			using (AndroidJNI.JStringBinding stringCharsInternal = AndroidJNI.GetStringCharsInternal(str))
			{
				text = stringCharsInternal.ToString();
			}
			return text;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000077D4 File Offset: 0x000059D4
		[ThreadSafe]
		private static AndroidJNI.JStringBinding GetStringCharsInternal(IntPtr str)
		{
			AndroidJNI.JStringBinding jstringBinding;
			AndroidJNI.GetStringCharsInternal_Injected(str, out jstringBinding);
			return jstringBinding;
		}

		// Token: 0x060000E0 RID: 224
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetStringLength(IntPtr str);

		// Token: 0x060000E1 RID: 225
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetStringUTFLength(IntPtr str);

		// Token: 0x060000E2 RID: 226 RVA: 0x000077EC File Offset: 0x000059EC
		[ThreadSafe]
		public static string GetStringUTFChars(IntPtr str)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				AndroidJNI.GetStringUTFChars_Injected(str, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000781C File Offset: 0x00005A1C
		public static string CallStringMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallStringMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000783C File Offset: 0x00005A3C
		public unsafe static string CallStringMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallStringMethodUnsafe(obj, methodID, ptr);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00007864 File Offset: 0x00005A64
		public unsafe static string CallStringMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args)
		{
			string text;
			using (AndroidJNI.JStringBinding jstringBinding = AndroidJNI.CallStringMethodUnsafeInternal(obj, methodID, args))
			{
				text = jstringBinding.ToString();
			}
			return text;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000078AC File Offset: 0x00005AAC
		[ThreadSafe]
		private unsafe static AndroidJNI.JStringBinding CallStringMethodUnsafeInternal(IntPtr obj, IntPtr methodID, jvalue* args)
		{
			AndroidJNI.JStringBinding jstringBinding;
			AndroidJNI.CallStringMethodUnsafeInternal_Injected(obj, methodID, args, out jstringBinding);
			return jstringBinding;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000078C4 File Offset: 0x00005AC4
		public static IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallObjectMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000078E4 File Offset: 0x00005AE4
		public unsafe static IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallObjectMethodUnsafe(obj, methodID, ptr);
			}
		}

		// Token: 0x060000E9 RID: 233
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr CallObjectMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x060000EA RID: 234 RVA: 0x0000790C File Offset: 0x00005B0C
		public static int CallIntMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallIntMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000792C File Offset: 0x00005B2C
		public unsafe static int CallIntMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallIntMethodUnsafe(obj, methodID, ptr);
			}
		}

		// Token: 0x060000EC RID: 236
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int CallIntMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x060000ED RID: 237 RVA: 0x00007954 File Offset: 0x00005B54
		public static bool CallBooleanMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallBooleanMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00007974 File Offset: 0x00005B74
		public unsafe static bool CallBooleanMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallBooleanMethodUnsafe(obj, methodID, ptr);
			}
		}

		// Token: 0x060000EF RID: 239
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern bool CallBooleanMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x060000F0 RID: 240 RVA: 0x0000799C File Offset: 0x00005B9C
		public static short CallShortMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallShortMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000079BC File Offset: 0x00005BBC
		public unsafe static short CallShortMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallShortMethodUnsafe(obj, methodID, ptr);
			}
		}

		// Token: 0x060000F2 RID: 242
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern short CallShortMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x060000F3 RID: 243 RVA: 0x000079E4 File Offset: 0x00005BE4
		[Obsolete("AndroidJNI.CallByteMethod is obsolete. Use AndroidJNI.CallSByteMethod method instead")]
		public static byte CallByteMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return (byte)AndroidJNI.CallSByteMethod(obj, methodID, args);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007A00 File Offset: 0x00005C00
		public static sbyte CallSByteMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallSByteMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007A20 File Offset: 0x00005C20
		public unsafe static sbyte CallSByteMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallSByteMethodUnsafe(obj, methodID, ptr);
			}
		}

		// Token: 0x060000F6 RID: 246
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern sbyte CallSByteMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x060000F7 RID: 247 RVA: 0x00007A48 File Offset: 0x00005C48
		public static char CallCharMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallCharMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00007A68 File Offset: 0x00005C68
		public unsafe static char CallCharMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallCharMethodUnsafe(obj, methodID, ptr);
			}
		}

		// Token: 0x060000F9 RID: 249
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern char CallCharMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x060000FA RID: 250 RVA: 0x00007A90 File Offset: 0x00005C90
		public static float CallFloatMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallFloatMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00007AB0 File Offset: 0x00005CB0
		public unsafe static float CallFloatMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallFloatMethodUnsafe(obj, methodID, ptr);
			}
		}

		// Token: 0x060000FC RID: 252
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern float CallFloatMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x060000FD RID: 253 RVA: 0x00007AD8 File Offset: 0x00005CD8
		public static double CallDoubleMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallDoubleMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00007AF8 File Offset: 0x00005CF8
		public unsafe static double CallDoubleMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallDoubleMethodUnsafe(obj, methodID, ptr);
			}
		}

		// Token: 0x060000FF RID: 255
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern double CallDoubleMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x06000100 RID: 256 RVA: 0x00007B20 File Offset: 0x00005D20
		public static long CallLongMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallLongMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00007B40 File Offset: 0x00005D40
		public unsafe static long CallLongMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallLongMethodUnsafe(obj, methodID, ptr);
			}
		}

		// Token: 0x06000102 RID: 258
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern long CallLongMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x06000103 RID: 259 RVA: 0x00007B67 File Offset: 0x00005D67
		public static void CallVoidMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			AndroidJNI.CallVoidMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00007B78 File Offset: 0x00005D78
		public unsafe static void CallVoidMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				AndroidJNI.CallVoidMethodUnsafe(obj, methodID, ptr);
			}
		}

		// Token: 0x06000105 RID: 261
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void CallVoidMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x06000106 RID: 262 RVA: 0x00007BA4 File Offset: 0x00005DA4
		public static string GetStringField(IntPtr obj, IntPtr fieldID)
		{
			string text;
			using (AndroidJNI.JStringBinding stringFieldInternal = AndroidJNI.GetStringFieldInternal(obj, fieldID))
			{
				text = stringFieldInternal.ToString();
			}
			return text;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00007BEC File Offset: 0x00005DEC
		[ThreadSafe]
		private static AndroidJNI.JStringBinding GetStringFieldInternal(IntPtr obj, IntPtr fieldID)
		{
			AndroidJNI.JStringBinding jstringBinding;
			AndroidJNI.GetStringFieldInternal_Injected(obj, fieldID, out jstringBinding);
			return jstringBinding;
		}

		// Token: 0x06000108 RID: 264
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetObjectField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000109 RID: 265
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetBooleanField(IntPtr obj, IntPtr fieldID);

		// Token: 0x0600010A RID: 266 RVA: 0x00007C04 File Offset: 0x00005E04
		[Obsolete("AndroidJNI.GetByteField is obsolete. Use AndroidJNI.GetSByteField method instead")]
		public static byte GetByteField(IntPtr obj, IntPtr fieldID)
		{
			return (byte)AndroidJNI.GetSByteField(obj, fieldID);
		}

		// Token: 0x0600010B RID: 267
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte GetSByteField(IntPtr obj, IntPtr fieldID);

		// Token: 0x0600010C RID: 268
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char GetCharField(IntPtr obj, IntPtr fieldID);

		// Token: 0x0600010D RID: 269
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short GetShortField(IntPtr obj, IntPtr fieldID);

		// Token: 0x0600010E RID: 270
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIntField(IntPtr obj, IntPtr fieldID);

		// Token: 0x0600010F RID: 271
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetLongField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000110 RID: 272
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetFloatField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000111 RID: 273
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double GetDoubleField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000112 RID: 274 RVA: 0x00007C20 File Offset: 0x00005E20
		[ThreadSafe]
		public unsafe static void SetStringField(IntPtr obj, IntPtr fieldID, string val)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(val, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = val.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				AndroidJNI.SetStringField_Injected(obj, fieldID, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000113 RID: 275
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetObjectField(IntPtr obj, IntPtr fieldID, IntPtr val);

		// Token: 0x06000114 RID: 276
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetBooleanField(IntPtr obj, IntPtr fieldID, bool val);

		// Token: 0x06000115 RID: 277 RVA: 0x00007C78 File Offset: 0x00005E78
		[Obsolete("AndroidJNI.SetByteField is obsolete. Use AndroidJNI.SetSByteField method instead")]
		public static void SetByteField(IntPtr obj, IntPtr fieldID, byte val)
		{
			AndroidJNI.SetSByteField(obj, fieldID, (sbyte)val);
		}

		// Token: 0x06000116 RID: 278
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetSByteField(IntPtr obj, IntPtr fieldID, sbyte val);

		// Token: 0x06000117 RID: 279
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCharField(IntPtr obj, IntPtr fieldID, char val);

		// Token: 0x06000118 RID: 280
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetShortField(IntPtr obj, IntPtr fieldID, short val);

		// Token: 0x06000119 RID: 281
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetIntField(IntPtr obj, IntPtr fieldID, int val);

		// Token: 0x0600011A RID: 282
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLongField(IntPtr obj, IntPtr fieldID, long val);

		// Token: 0x0600011B RID: 283
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetFloatField(IntPtr obj, IntPtr fieldID, float val);

		// Token: 0x0600011C RID: 284
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetDoubleField(IntPtr obj, IntPtr fieldID, double val);

		// Token: 0x0600011D RID: 285 RVA: 0x00007C88 File Offset: 0x00005E88
		public static string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallStaticStringMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00007CA8 File Offset: 0x00005EA8
		public unsafe static string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallStaticStringMethodUnsafe(clazz, methodID, ptr);
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00007CD0 File Offset: 0x00005ED0
		public unsafe static string CallStaticStringMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args)
		{
			string text;
			using (AndroidJNI.JStringBinding jstringBinding = AndroidJNI.CallStaticStringMethodUnsafeInternal(clazz, methodID, args))
			{
				text = jstringBinding.ToString();
			}
			return text;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00007D18 File Offset: 0x00005F18
		[ThreadSafe]
		private unsafe static AndroidJNI.JStringBinding CallStaticStringMethodUnsafeInternal(IntPtr clazz, IntPtr methodID, jvalue* args)
		{
			AndroidJNI.JStringBinding jstringBinding;
			AndroidJNI.CallStaticStringMethodUnsafeInternal_Injected(clazz, methodID, args, out jstringBinding);
			return jstringBinding;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00007D30 File Offset: 0x00005F30
		public static IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallStaticObjectMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00007D50 File Offset: 0x00005F50
		public unsafe static IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallStaticObjectMethodUnsafe(clazz, methodID, ptr);
			}
		}

		// Token: 0x06000123 RID: 291
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr CallStaticObjectMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x06000124 RID: 292 RVA: 0x00007D78 File Offset: 0x00005F78
		public static int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallStaticIntMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007D98 File Offset: 0x00005F98
		public unsafe static int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallStaticIntMethodUnsafe(clazz, methodID, ptr);
			}
		}

		// Token: 0x06000126 RID: 294
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int CallStaticIntMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x06000127 RID: 295 RVA: 0x00007DC0 File Offset: 0x00005FC0
		public static bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallStaticBooleanMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007DE0 File Offset: 0x00005FE0
		public unsafe static bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallStaticBooleanMethodUnsafe(clazz, methodID, ptr);
			}
		}

		// Token: 0x06000129 RID: 297
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern bool CallStaticBooleanMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x0600012A RID: 298 RVA: 0x00007E08 File Offset: 0x00006008
		public static short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallStaticShortMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00007E28 File Offset: 0x00006028
		public unsafe static short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallStaticShortMethodUnsafe(clazz, methodID, ptr);
			}
		}

		// Token: 0x0600012C RID: 300
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern short CallStaticShortMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x0600012D RID: 301 RVA: 0x00007E50 File Offset: 0x00006050
		[Obsolete("AndroidJNI.CallStaticByteMethod is obsolete. Use AndroidJNI.CallStaticSByteMethod method instead")]
		public static byte CallStaticByteMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return (byte)AndroidJNI.CallStaticSByteMethod(clazz, methodID, args);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007E6C File Offset: 0x0000606C
		public static sbyte CallStaticSByteMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallStaticSByteMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007E8C File Offset: 0x0000608C
		public unsafe static sbyte CallStaticSByteMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallStaticSByteMethodUnsafe(clazz, methodID, ptr);
			}
		}

		// Token: 0x06000130 RID: 304
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern sbyte CallStaticSByteMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x06000131 RID: 305 RVA: 0x00007EB4 File Offset: 0x000060B4
		public static char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallStaticCharMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00007ED4 File Offset: 0x000060D4
		public unsafe static char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallStaticCharMethodUnsafe(clazz, methodID, ptr);
			}
		}

		// Token: 0x06000133 RID: 307
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern char CallStaticCharMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x06000134 RID: 308 RVA: 0x00007EFC File Offset: 0x000060FC
		public static float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallStaticFloatMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007F1C File Offset: 0x0000611C
		public unsafe static float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallStaticFloatMethodUnsafe(clazz, methodID, ptr);
			}
		}

		// Token: 0x06000136 RID: 310
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern float CallStaticFloatMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x06000137 RID: 311 RVA: 0x00007F44 File Offset: 0x00006144
		public static double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallStaticDoubleMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007F64 File Offset: 0x00006164
		public unsafe static double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallStaticDoubleMethodUnsafe(clazz, methodID, ptr);
			}
		}

		// Token: 0x06000139 RID: 313
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern double CallStaticDoubleMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x0600013A RID: 314 RVA: 0x00007F8C File Offset: 0x0000618C
		public static long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallStaticLongMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007FAC File Offset: 0x000061AC
		public unsafe static long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				return AndroidJNI.CallStaticLongMethodUnsafe(clazz, methodID, ptr);
			}
		}

		// Token: 0x0600013C RID: 316
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern long CallStaticLongMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x0600013D RID: 317 RVA: 0x00007FD3 File Offset: 0x000061D3
		public static void CallStaticVoidMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			AndroidJNI.CallStaticVoidMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007FE4 File Offset: 0x000061E4
		public unsafe static void CallStaticVoidMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* ptr = pinnableReference;
				AndroidJNI.CallStaticVoidMethodUnsafe(clazz, methodID, ptr);
			}
		}

		// Token: 0x0600013F RID: 319
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void CallStaticVoidMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x06000140 RID: 320 RVA: 0x00008010 File Offset: 0x00006210
		public static string GetStaticStringField(IntPtr clazz, IntPtr fieldID)
		{
			string text;
			using (AndroidJNI.JStringBinding staticStringFieldInternal = AndroidJNI.GetStaticStringFieldInternal(clazz, fieldID))
			{
				text = staticStringFieldInternal.ToString();
			}
			return text;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00008058 File Offset: 0x00006258
		[ThreadSafe]
		private static AndroidJNI.JStringBinding GetStaticStringFieldInternal(IntPtr clazz, IntPtr fieldID)
		{
			AndroidJNI.JStringBinding jstringBinding;
			AndroidJNI.GetStaticStringFieldInternal_Injected(clazz, fieldID, out jstringBinding);
			return jstringBinding;
		}

		// Token: 0x06000142 RID: 322
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetStaticObjectField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000143 RID: 323
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetStaticBooleanField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000144 RID: 324 RVA: 0x00008070 File Offset: 0x00006270
		[Obsolete("AndroidJNI.GetStaticByteField is obsolete. Use AndroidJNI.GetStaticSByteField method instead")]
		public static byte GetStaticByteField(IntPtr clazz, IntPtr fieldID)
		{
			return (byte)AndroidJNI.GetStaticSByteField(clazz, fieldID);
		}

		// Token: 0x06000145 RID: 325
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte GetStaticSByteField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000146 RID: 326
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char GetStaticCharField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000147 RID: 327
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short GetStaticShortField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000148 RID: 328
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetStaticIntField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000149 RID: 329
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetStaticLongField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x0600014A RID: 330
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetStaticFloatField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x0600014B RID: 331
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double GetStaticDoubleField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x0600014C RID: 332 RVA: 0x0000808C File Offset: 0x0000628C
		[ThreadSafe]
		public unsafe static void SetStaticStringField(IntPtr clazz, IntPtr fieldID, string val)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(val, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = val.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				AndroidJNI.SetStaticStringField_Injected(clazz, fieldID, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600014D RID: 333
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticObjectField(IntPtr clazz, IntPtr fieldID, IntPtr val);

		// Token: 0x0600014E RID: 334
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticBooleanField(IntPtr clazz, IntPtr fieldID, bool val);

		// Token: 0x0600014F RID: 335 RVA: 0x000080E4 File Offset: 0x000062E4
		[Obsolete("AndroidJNI.SetStaticByteField is obsolete. Use AndroidJNI.SetStaticSByteField method instead")]
		public static void SetStaticByteField(IntPtr clazz, IntPtr fieldID, byte val)
		{
			AndroidJNI.SetStaticSByteField(clazz, fieldID, (sbyte)val);
		}

		// Token: 0x06000150 RID: 336
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticSByteField(IntPtr clazz, IntPtr fieldID, sbyte val);

		// Token: 0x06000151 RID: 337
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticCharField(IntPtr clazz, IntPtr fieldID, char val);

		// Token: 0x06000152 RID: 338
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticShortField(IntPtr clazz, IntPtr fieldID, short val);

		// Token: 0x06000153 RID: 339
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticIntField(IntPtr clazz, IntPtr fieldID, int val);

		// Token: 0x06000154 RID: 340
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticLongField(IntPtr clazz, IntPtr fieldID, long val);

		// Token: 0x06000155 RID: 341
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticFloatField(IntPtr clazz, IntPtr fieldID, float val);

		// Token: 0x06000156 RID: 342
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticDoubleField(IntPtr clazz, IntPtr fieldID, double val);

		// Token: 0x06000157 RID: 343 RVA: 0x000080F4 File Offset: 0x000062F4
		[ThreadSafe]
		private unsafe static IntPtr ConvertToBooleanArray(bool[] array)
		{
			Span<bool> span = new Span<bool>(array);
			IntPtr intPtr;
			fixed (bool* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				intPtr = AndroidJNI.ConvertToBooleanArray_Injected(ref managedSpanWrapper);
			}
			return intPtr;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000812C File Offset: 0x0000632C
		public static IntPtr ToBooleanArray(bool[] array)
		{
			return (array == null) ? IntPtr.Zero : AndroidJNI.ConvertToBooleanArray(array);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008150 File Offset: 0x00006350
		[Obsolete("AndroidJNI.ToByteArray is obsolete. Use AndroidJNI.ToSByteArray method instead")]
		[ThreadSafe]
		public unsafe static IntPtr ToByteArray(byte[] array)
		{
			Span<byte> span = new Span<byte>(array);
			IntPtr intPtr;
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				intPtr = AndroidJNI.ToByteArray_Injected(ref managedSpanWrapper);
			}
			return intPtr;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00008188 File Offset: 0x00006388
		public unsafe static IntPtr ToSByteArray(sbyte[] array)
		{
			bool flag = array == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				sbyte* ptr;
				if (array == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				intPtr = AndroidJNI.ToSByteArray(ptr, array.Length);
			}
			return intPtr;
		}

		// Token: 0x0600015B RID: 347
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr ToSByteArray(sbyte* array, int length);

		// Token: 0x0600015C RID: 348 RVA: 0x000081D0 File Offset: 0x000063D0
		public unsafe static IntPtr ToCharArray(char[] array)
		{
			bool flag = array == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				char* ptr;
				if (array == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				intPtr = AndroidJNI.ToCharArray(ptr, array.Length);
			}
			return intPtr;
		}

		// Token: 0x0600015D RID: 349
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr ToCharArray(char* array, int length);

		// Token: 0x0600015E RID: 350 RVA: 0x00008218 File Offset: 0x00006418
		public unsafe static IntPtr ToShortArray(short[] array)
		{
			bool flag = array == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				short* ptr;
				if (array == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				intPtr = AndroidJNI.ToShortArray(ptr, array.Length);
			}
			return intPtr;
		}

		// Token: 0x0600015F RID: 351
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr ToShortArray(short* array, int length);

		// Token: 0x06000160 RID: 352 RVA: 0x00008260 File Offset: 0x00006460
		public unsafe static IntPtr ToIntArray(int[] array)
		{
			bool flag = array == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				int* ptr;
				if (array == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				intPtr = AndroidJNI.ToIntArray(ptr, array.Length);
			}
			return intPtr;
		}

		// Token: 0x06000161 RID: 353
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr ToIntArray(int* array, int length);

		// Token: 0x06000162 RID: 354 RVA: 0x000082A8 File Offset: 0x000064A8
		public unsafe static IntPtr ToLongArray(long[] array)
		{
			bool flag = array == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				long* ptr;
				if (array == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				intPtr = AndroidJNI.ToLongArray(ptr, array.Length);
			}
			return intPtr;
		}

		// Token: 0x06000163 RID: 355
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr ToLongArray(long* array, int length);

		// Token: 0x06000164 RID: 356 RVA: 0x000082F0 File Offset: 0x000064F0
		public unsafe static IntPtr ToFloatArray(float[] array)
		{
			bool flag = array == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				float* ptr;
				if (array == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				intPtr = AndroidJNI.ToFloatArray(ptr, array.Length);
			}
			return intPtr;
		}

		// Token: 0x06000165 RID: 357
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr ToFloatArray(float* array, int length);

		// Token: 0x06000166 RID: 358 RVA: 0x00008338 File Offset: 0x00006538
		public unsafe static IntPtr ToDoubleArray(double[] array)
		{
			bool flag = array == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				double* ptr;
				if (array == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				intPtr = AndroidJNI.ToDoubleArray(ptr, array.Length);
			}
			return intPtr;
		}

		// Token: 0x06000167 RID: 359
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr ToDoubleArray(double* array, int length);

		// Token: 0x06000168 RID: 360
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr ToObjectArray(IntPtr* array, int length, IntPtr arrayClass);

		// Token: 0x06000169 RID: 361 RVA: 0x00008380 File Offset: 0x00006580
		public unsafe static IntPtr ToObjectArray(IntPtr[] array, IntPtr arrayClass)
		{
			bool flag = array == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				IntPtr* ptr;
				if (array == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				intPtr = AndroidJNI.ToObjectArray(ptr, array.Length, arrayClass);
			}
			return intPtr;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000083C8 File Offset: 0x000065C8
		public static IntPtr ToObjectArray(IntPtr[] array)
		{
			return AndroidJNI.ToObjectArray(array, IntPtr.Zero);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000083E8 File Offset: 0x000065E8
		[ThreadSafe]
		public static bool[] FromBooleanArray(IntPtr array)
		{
			bool[] array3;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				AndroidJNI.FromBooleanArray_Injected(array, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				bool[] array2;
				blittableArrayWrapper.Unmarshal<bool>(ref array2);
				array3 = array2;
			}
			return array3;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000841C File Offset: 0x0000661C
		[ThreadSafe]
		[Obsolete("AndroidJNI.FromByteArray is obsolete. Use AndroidJNI.FromSByteArray method instead")]
		public static byte[] FromByteArray(IntPtr array)
		{
			byte[] array3;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				AndroidJNI.FromByteArray_Injected(array, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				byte[] array2;
				blittableArrayWrapper.Unmarshal<byte>(ref array2);
				array3 = array2;
			}
			return array3;
		}

		// Token: 0x0600016D RID: 365
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Unmarshalled]
		public static extern sbyte[] FromSByteArray(IntPtr array);

		// Token: 0x0600016E RID: 366
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Unmarshalled]
		public static extern char[] FromCharArray(IntPtr array);

		// Token: 0x0600016F RID: 367
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Unmarshalled]
		public static extern short[] FromShortArray(IntPtr array);

		// Token: 0x06000170 RID: 368
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Unmarshalled]
		public static extern int[] FromIntArray(IntPtr array);

		// Token: 0x06000171 RID: 369
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Unmarshalled]
		public static extern long[] FromLongArray(IntPtr array);

		// Token: 0x06000172 RID: 370
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Unmarshalled]
		public static extern float[] FromFloatArray(IntPtr array);

		// Token: 0x06000173 RID: 371
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Unmarshalled]
		public static extern double[] FromDoubleArray(IntPtr array);

		// Token: 0x06000174 RID: 372 RVA: 0x00008450 File Offset: 0x00006650
		[ThreadSafe]
		public static IntPtr[] FromObjectArray(IntPtr array)
		{
			IntPtr[] array3;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				AndroidJNI.FromObjectArray_Injected(array, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				IntPtr[] array2;
				blittableArrayWrapper.Unmarshal<IntPtr>(ref array2);
				array3 = array2;
			}
			return array3;
		}

		// Token: 0x06000175 RID: 373
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetArrayLength(IntPtr array);

		// Token: 0x06000176 RID: 374
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewBooleanArray(int size);

		// Token: 0x06000177 RID: 375 RVA: 0x00008484 File Offset: 0x00006684
		[Obsolete("AndroidJNI.NewByteArray is obsolete. Use AndroidJNI.NewSByteArray method instead")]
		public static IntPtr NewByteArray(int size)
		{
			return AndroidJNI.NewSByteArray(size);
		}

		// Token: 0x06000178 RID: 376
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewSByteArray(int size);

		// Token: 0x06000179 RID: 377
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewCharArray(int size);

		// Token: 0x0600017A RID: 378
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewShortArray(int size);

		// Token: 0x0600017B RID: 379
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewIntArray(int size);

		// Token: 0x0600017C RID: 380
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewLongArray(int size);

		// Token: 0x0600017D RID: 381
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewFloatArray(int size);

		// Token: 0x0600017E RID: 382
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewDoubleArray(int size);

		// Token: 0x0600017F RID: 383
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewObjectArray(int size, IntPtr clazz, IntPtr obj);

		// Token: 0x06000180 RID: 384
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetBooleanArrayElement(IntPtr array, int index);

		// Token: 0x06000181 RID: 385 RVA: 0x0000849C File Offset: 0x0000669C
		[Obsolete("AndroidJNI.GetByteArrayElement is obsolete. Use AndroidJNI.GetSByteArrayElement method instead")]
		public static byte GetByteArrayElement(IntPtr array, int index)
		{
			return (byte)AndroidJNI.GetSByteArrayElement(array, index);
		}

		// Token: 0x06000182 RID: 386
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte GetSByteArrayElement(IntPtr array, int index);

		// Token: 0x06000183 RID: 387
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char GetCharArrayElement(IntPtr array, int index);

		// Token: 0x06000184 RID: 388
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short GetShortArrayElement(IntPtr array, int index);

		// Token: 0x06000185 RID: 389
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIntArrayElement(IntPtr array, int index);

		// Token: 0x06000186 RID: 390
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetLongArrayElement(IntPtr array, int index);

		// Token: 0x06000187 RID: 391
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetFloatArrayElement(IntPtr array, int index);

		// Token: 0x06000188 RID: 392
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double GetDoubleArrayElement(IntPtr array, int index);

		// Token: 0x06000189 RID: 393
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetObjectArrayElement(IntPtr array, int index);

		// Token: 0x0600018A RID: 394 RVA: 0x000084B6 File Offset: 0x000066B6
		[Obsolete("AndroidJNI.SetBooleanArrayElement(IntPtr, int, byte) is obsolete. Use AndroidJNI.SetBooleanArrayElement(IntPtr, int, bool) method instead")]
		public static void SetBooleanArrayElement(IntPtr array, int index, byte val)
		{
			AndroidJNI.SetBooleanArrayElement(array, index, val > 0);
		}

		// Token: 0x0600018B RID: 395
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetBooleanArrayElement(IntPtr array, int index, bool val);

		// Token: 0x0600018C RID: 396 RVA: 0x000084C5 File Offset: 0x000066C5
		[Obsolete("AndroidJNI.SetByteArrayElement is obsolete. Use AndroidJNI.SetSByteArrayElement method instead")]
		public static void SetByteArrayElement(IntPtr array, int index, sbyte val)
		{
			AndroidJNI.SetSByteArrayElement(array, index, val);
		}

		// Token: 0x0600018D RID: 397
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetSByteArrayElement(IntPtr array, int index, sbyte val);

		// Token: 0x0600018E RID: 398
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCharArrayElement(IntPtr array, int index, char val);

		// Token: 0x0600018F RID: 399
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetShortArrayElement(IntPtr array, int index, short val);

		// Token: 0x06000190 RID: 400
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetIntArrayElement(IntPtr array, int index, int val);

		// Token: 0x06000191 RID: 401
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLongArrayElement(IntPtr array, int index, long val);

		// Token: 0x06000192 RID: 402
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetFloatArrayElement(IntPtr array, int index, float val);

		// Token: 0x06000193 RID: 403
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetDoubleArrayElement(IntPtr array, int index, double val);

		// Token: 0x06000194 RID: 404
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetObjectArrayElement(IntPtr array, int index, IntPtr obj);

		// Token: 0x06000195 RID: 405
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr NewDirectByteBuffer(byte* buffer, long capacity);

		// Token: 0x06000196 RID: 406 RVA: 0x000084D4 File Offset: 0x000066D4
		public static IntPtr NewDirectByteBuffer(NativeArray<byte> buffer)
		{
			return AndroidJNI.NewDirectByteBufferFromNativeArray<byte>(buffer);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000084EC File Offset: 0x000066EC
		public static IntPtr NewDirectByteBuffer(NativeArray<sbyte> buffer)
		{
			return AndroidJNI.NewDirectByteBufferFromNativeArray<sbyte>(buffer);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008504 File Offset: 0x00006704
		private unsafe static IntPtr NewDirectByteBufferFromNativeArray<T>(NativeArray<T> buffer) where T : struct
		{
			bool flag = !buffer.IsCreated || buffer.Length <= 0;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				intPtr = AndroidJNI.NewDirectByteBuffer((byte*)buffer.GetUnsafePtr<T>(), (long)buffer.Length);
			}
			return intPtr;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008550 File Offset: 0x00006750
		public unsafe static sbyte* GetDirectBufferAddress(IntPtr buffer)
		{
			return null;
		}

		// Token: 0x0600019A RID: 410
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetDirectBufferCapacity(IntPtr buffer);

		// Token: 0x0600019B RID: 411 RVA: 0x00008564 File Offset: 0x00006764
		private unsafe static NativeArray<T> GetDirectBuffer<T>(IntPtr buffer) where T : struct
		{
			bool flag = buffer == IntPtr.Zero;
			NativeArray<T> nativeArray;
			if (flag)
			{
				nativeArray = default(NativeArray<T>);
			}
			else
			{
				sbyte* directBufferAddress = AndroidJNI.GetDirectBufferAddress(buffer);
				bool flag2 = directBufferAddress == null;
				if (flag2)
				{
					nativeArray = default(NativeArray<T>);
				}
				else
				{
					long directBufferCapacity = AndroidJNI.GetDirectBufferCapacity(buffer);
					bool flag3 = directBufferCapacity > 2147483647L;
					if (flag3)
					{
						throw new Exception(string.Format("Direct buffer is too large ({0}) for NativeArray (max {1})", directBufferCapacity, int.MaxValue));
					}
					nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)directBufferAddress, (int)directBufferCapacity, Allocator.None);
				}
			}
			return nativeArray;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000085F4 File Offset: 0x000067F4
		public static NativeArray<byte> GetDirectByteBuffer(IntPtr buffer)
		{
			return AndroidJNI.GetDirectBuffer<byte>(buffer);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000860C File Offset: 0x0000680C
		public static NativeArray<sbyte> GetDirectSByteBuffer(IntPtr buffer)
		{
			return AndroidJNI.GetDirectBuffer<sbyte>(buffer);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00008624 File Offset: 0x00006824
		public static int RegisterNatives(IntPtr clazz, JNINativeMethod[] methods)
		{
			bool flag = methods == null || methods.Length == 0;
			int num;
			if (flag)
			{
				num = -1;
			}
			else
			{
				foreach (JNINativeMethod jninativeMethod in methods)
				{
					bool flag2 = string.IsNullOrEmpty(jninativeMethod.name) || string.IsNullOrEmpty(jninativeMethod.signature);
					if (flag2)
					{
						return -1;
					}
				}
				IntPtr intPtr = AndroidJNI.RegisterNativesAllocate(methods.Length);
				for (int j = 0; j < methods.Length; j++)
				{
					AndroidJNI.RegisterNativesSet(intPtr, j, methods[j].name, methods[j].signature, methods[j].fnPtr);
				}
				num = AndroidJNI.RegisterNativesAndFree(clazz, intPtr, methods.Length);
			}
			return num;
		}

		// Token: 0x0600019F RID: 415
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr RegisterNativesAllocate(int length);

		// Token: 0x060001A0 RID: 416 RVA: 0x000086F0 File Offset: 0x000068F0
		[ThreadSafe]
		private unsafe static void RegisterNativesSet(IntPtr natives, int idx, string name, string signature, IntPtr fnPtr)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(signature, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = signature.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				AndroidJNI.RegisterNativesSet_Injected(natives, idx, ref managedSpanWrapper, ref managedSpanWrapper2, fnPtr);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x060001A1 RID: 417
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RegisterNativesAndFree(IntPtr clazz, IntPtr natives, int n);

		// Token: 0x060001A2 RID: 418
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int UnregisterNatives(IntPtr clazz);

		// Token: 0x060001A3 RID: 419
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseStringChars_Injected([In] ref AndroidJNI.JStringBinding str);

		// Token: 0x060001A4 RID: 420
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr FindClass_Injected(ref ManagedSpanWrapper name);

		// Token: 0x060001A5 RID: 421
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ThrowNew_Injected(IntPtr clazz, ref ManagedSpanWrapper message);

		// Token: 0x060001A6 RID: 422
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FatalError_Injected(ref ManagedSpanWrapper message);

		// Token: 0x060001A7 RID: 423
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetMethodID_Injected(IntPtr clazz, ref ManagedSpanWrapper name, ref ManagedSpanWrapper sig);

		// Token: 0x060001A8 RID: 424
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetFieldID_Injected(IntPtr clazz, ref ManagedSpanWrapper name, ref ManagedSpanWrapper sig);

		// Token: 0x060001A9 RID: 425
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetStaticMethodID_Injected(IntPtr clazz, ref ManagedSpanWrapper name, ref ManagedSpanWrapper sig);

		// Token: 0x060001AA RID: 426
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetStaticFieldID_Injected(IntPtr clazz, ref ManagedSpanWrapper name, ref ManagedSpanWrapper sig);

		// Token: 0x060001AB RID: 427
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr NewStringFromStr_Injected(ref ManagedSpanWrapper chars);

		// Token: 0x060001AC RID: 428
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr NewString_Injected(ref ManagedSpanWrapper chars);

		// Token: 0x060001AD RID: 429
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr NewStringUTF_Injected(ref ManagedSpanWrapper bytes);

		// Token: 0x060001AE RID: 430
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStringCharsInternal_Injected(IntPtr str, out AndroidJNI.JStringBinding ret);

		// Token: 0x060001AF RID: 431
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStringUTFChars_Injected(IntPtr str, out ManagedSpanWrapper ret);

		// Token: 0x060001B0 RID: 432
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void CallStringMethodUnsafeInternal_Injected(IntPtr obj, IntPtr methodID, jvalue* args, out AndroidJNI.JStringBinding ret);

		// Token: 0x060001B1 RID: 433
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStringFieldInternal_Injected(IntPtr obj, IntPtr fieldID, out AndroidJNI.JStringBinding ret);

		// Token: 0x060001B2 RID: 434
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStringField_Injected(IntPtr obj, IntPtr fieldID, ref ManagedSpanWrapper val);

		// Token: 0x060001B3 RID: 435
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void CallStaticStringMethodUnsafeInternal_Injected(IntPtr clazz, IntPtr methodID, jvalue* args, out AndroidJNI.JStringBinding ret);

		// Token: 0x060001B4 RID: 436
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStaticStringFieldInternal_Injected(IntPtr clazz, IntPtr fieldID, out AndroidJNI.JStringBinding ret);

		// Token: 0x060001B5 RID: 437
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStaticStringField_Injected(IntPtr clazz, IntPtr fieldID, ref ManagedSpanWrapper val);

		// Token: 0x060001B6 RID: 438
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr ConvertToBooleanArray_Injected(ref ManagedSpanWrapper array);

		// Token: 0x060001B7 RID: 439
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr ToByteArray_Injected(ref ManagedSpanWrapper array);

		// Token: 0x060001B8 RID: 440
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FromBooleanArray_Injected(IntPtr array, out BlittableArrayWrapper ret);

		// Token: 0x060001B9 RID: 441
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FromByteArray_Injected(IntPtr array, out BlittableArrayWrapper ret);

		// Token: 0x060001BA RID: 442
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FromObjectArray_Injected(IntPtr array, out BlittableArrayWrapper ret);

		// Token: 0x060001BB RID: 443
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterNativesSet_Injected(IntPtr natives, int idx, ref ManagedSpanWrapper name, ref ManagedSpanWrapper signature, IntPtr fnPtr);

		// Token: 0x02000010 RID: 16
		private struct JStringBinding : IDisposable
		{
			// Token: 0x060001BC RID: 444 RVA: 0x0000877C File Offset: 0x0000697C
			public unsafe override string ToString()
			{
				bool flag = this.length == 0;
				string text;
				if (flag)
				{
					text = ((this.chars == IntPtr.Zero) ? null : string.Empty);
				}
				else
				{
					text = new string((char*)(void*)this.chars, 0, this.length);
				}
				return text;
			}

			// Token: 0x060001BD RID: 445 RVA: 0x000087D0 File Offset: 0x000069D0
			public void Dispose()
			{
				bool flag = this.length > 0;
				if (flag)
				{
					AndroidJNI.ReleaseStringChars(this);
				}
			}

			// Token: 0x04000022 RID: 34
			private IntPtr javaString;

			// Token: 0x04000023 RID: 35
			private IntPtr chars;

			// Token: 0x04000024 RID: 36
			private int length;

			// Token: 0x04000025 RID: 37
			private bool ownsRef;
		}
	}
}
