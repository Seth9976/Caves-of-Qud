using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000EE RID: 238
	[StructLayout(LayoutKind.Explicit, Size = 24)]
	public struct TextEvent : IInputEventTypeInfo
	{
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x00047B8A File Offset: 0x00045D8A
		public FourCC typeStatic
		{
			get
			{
				return 1413830740;
			}
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00047B98 File Offset: 0x00045D98
		public unsafe static TextEvent* From(InputEventPtr eventPtr)
		{
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			if (!eventPtr.IsA<TextEvent>())
			{
				throw new InvalidCastException(string.Format("Cannot cast event with type '{0}' into TextEvent", eventPtr.type));
			}
			return (TextEvent*)eventPtr.data;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00047BE8 File Offset: 0x00045DE8
		public static TextEvent Create(int deviceId, char character, double time = -1.0)
		{
			return new TextEvent
			{
				baseEvent = new InputEvent(1413830740, 24, deviceId, time),
				character = (int)character
			};
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00047C20 File Offset: 0x00045E20
		public static TextEvent Create(int deviceId, int character, double time = -1.0)
		{
			return new TextEvent
			{
				baseEvent = new InputEvent(1413830740, 24, deviceId, time),
				character = character
			};
		}

		// Token: 0x040005E9 RID: 1513
		public const int Type = 1413830740;

		// Token: 0x040005EA RID: 1514
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x040005EB RID: 1515
		[FieldOffset(20)]
		public int character;
	}
}
