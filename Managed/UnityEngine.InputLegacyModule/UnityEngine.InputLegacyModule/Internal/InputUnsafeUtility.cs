using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Internal
{
	// Token: 0x02000016 RID: 22
	[NativeHeader("Runtime/Input/InputBindings.h")]
	internal static class InputUnsafeUtility
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00003030 File Offset: 0x00001230
		[NativeThrows]
		internal unsafe static bool GetKeyString(string name)
		{
			bool keyString_Injected;
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
				keyString_Injected = InputUnsafeUtility.GetKeyString_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return keyString_Injected;
		}

		// Token: 0x060000BC RID: 188
		[RequiredMember]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool GetKeyString__Unmanaged(byte* name, int nameLen);

		// Token: 0x060000BD RID: 189 RVA: 0x00003088 File Offset: 0x00001288
		[NativeThrows]
		internal unsafe static bool GetKeyUpString(string name)
		{
			bool keyUpString_Injected;
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
				keyUpString_Injected = InputUnsafeUtility.GetKeyUpString_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return keyUpString_Injected;
		}

		// Token: 0x060000BE RID: 190
		[NativeThrows]
		[RequiredMember]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool GetKeyUpString__Unmanaged(byte* name, int nameLen);

		// Token: 0x060000BF RID: 191 RVA: 0x000030E0 File Offset: 0x000012E0
		[NativeThrows]
		internal unsafe static bool GetKeyDownString(string name)
		{
			bool keyDownString_Injected;
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
				keyDownString_Injected = InputUnsafeUtility.GetKeyDownString_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return keyDownString_Injected;
		}

		// Token: 0x060000C0 RID: 192
		[NativeThrows]
		[RequiredMember]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool GetKeyDownString__Unmanaged(byte* name, int nameLen);

		// Token: 0x060000C1 RID: 193 RVA: 0x00003138 File Offset: 0x00001338
		[NativeThrows]
		internal unsafe static float GetAxis(string axisName)
		{
			float axis_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(axisName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = axisName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				axis_Injected = InputUnsafeUtility.GetAxis_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return axis_Injected;
		}

		// Token: 0x060000C2 RID: 194
		[RequiredMember]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern float GetAxis__Unmanaged(byte* axisName, int axisNameLen);

		// Token: 0x060000C3 RID: 195 RVA: 0x00003190 File Offset: 0x00001390
		[NativeThrows]
		internal unsafe static float GetAxisRaw(string axisName)
		{
			float axisRaw_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(axisName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = axisName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				axisRaw_Injected = InputUnsafeUtility.GetAxisRaw_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return axisRaw_Injected;
		}

		// Token: 0x060000C4 RID: 196
		[NativeThrows]
		[RequiredMember]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern float GetAxisRaw__Unmanaged(byte* axisName, int axisNameLen);

		// Token: 0x060000C5 RID: 197 RVA: 0x000031E8 File Offset: 0x000013E8
		[NativeThrows]
		internal unsafe static bool GetButton(string buttonName)
		{
			bool button_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(buttonName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = buttonName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				button_Injected = InputUnsafeUtility.GetButton_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return button_Injected;
		}

		// Token: 0x060000C6 RID: 198
		[NativeThrows]
		[RequiredMember]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool GetButton__Unmanaged(byte* buttonName, int buttonNameLen);

		// Token: 0x060000C7 RID: 199 RVA: 0x00003240 File Offset: 0x00001440
		[NativeThrows]
		internal unsafe static bool GetButtonDown(string buttonName)
		{
			bool buttonDown_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(buttonName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = buttonName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				buttonDown_Injected = InputUnsafeUtility.GetButtonDown_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return buttonDown_Injected;
		}

		// Token: 0x060000C8 RID: 200
		[NativeThrows]
		[RequiredMember]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern byte GetButtonDown__Unmanaged(byte* buttonName, int buttonNameLen);

		// Token: 0x060000C9 RID: 201 RVA: 0x00003298 File Offset: 0x00001498
		[NativeThrows]
		internal unsafe static bool GetButtonUp(string buttonName)
		{
			bool buttonUp_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(buttonName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = buttonName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				buttonUp_Injected = InputUnsafeUtility.GetButtonUp_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return buttonUp_Injected;
		}

		// Token: 0x060000CA RID: 202
		[RequiredMember]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool GetButtonUp__Unmanaged(byte* buttonName, int buttonNameLen);

		// Token: 0x060000CB RID: 203
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyString_Injected(ref ManagedSpanWrapper name);

		// Token: 0x060000CC RID: 204
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyUpString_Injected(ref ManagedSpanWrapper name);

		// Token: 0x060000CD RID: 205
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyDownString_Injected(ref ManagedSpanWrapper name);

		// Token: 0x060000CE RID: 206
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetAxis_Injected(ref ManagedSpanWrapper axisName);

		// Token: 0x060000CF RID: 207
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetAxisRaw_Injected(ref ManagedSpanWrapper axisName);

		// Token: 0x060000D0 RID: 208
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetButton_Injected(ref ManagedSpanWrapper buttonName);

		// Token: 0x060000D1 RID: 209
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetButtonDown_Injected(ref ManagedSpanWrapper buttonName);

		// Token: 0x060000D2 RID: 210
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetButtonUp_Injected(ref ManagedSpanWrapper buttonName);
	}
}
