using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200020F RID: 527
	[NativeHeader("Runtime/Math/ColorUtility.h")]
	public class ColorUtility
	{
		// Token: 0x06001BC2 RID: 7106 RVA: 0x000321D4 File Offset: 0x000303D4
		[FreeFunction("TryParseHtmlColor", true)]
		internal unsafe static bool DoTryParseHtmlColor(string htmlString, out Color32 color)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(htmlString, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = htmlString.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = ColorUtility.DoTryParseHtmlColor_Injected(ref managedSpanWrapper, out color);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0003222C File Offset: 0x0003042C
		public static bool TryParseHtmlString(string htmlString, out Color color)
		{
			Color32 color2;
			bool flag = ColorUtility.DoTryParseHtmlColor(htmlString, out color2);
			color = color2;
			return flag;
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x00032254 File Offset: 0x00030454
		public static string ToHtmlStringRGB(Color color)
		{
			Color32 color2 = new Color32((byte)Mathf.Clamp(Mathf.RoundToInt(color.r * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.g * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.b * 255f), 0, 255), 1);
			return UnityString.Format("{0:X2}{1:X2}{2:X2}", new object[] { color2.r, color2.g, color2.b });
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x00032300 File Offset: 0x00030500
		public static string ToHtmlStringRGBA(Color color)
		{
			Color32 color2 = new Color32((byte)Mathf.Clamp(Mathf.RoundToInt(color.r * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.g * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.b * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.a * 255f), 0, 255));
			return UnityString.Format("{0:X2}{1:X2}{2:X2}{3:X2}", new object[] { color2.r, color2.g, color2.b, color2.a });
		}

		// Token: 0x06001BC7 RID: 7111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool DoTryParseHtmlColor_Injected(ref ManagedSpanWrapper htmlString, out Color32 color);
	}
}
