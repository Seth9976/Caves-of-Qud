using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine
{
	// Token: 0x02000227 RID: 551
	[MovedFrom("UnityEditor")]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEditor.UIBuilderModule" })]
	internal class NumericFieldDraggerUtility
	{
		// Token: 0x06001E0C RID: 7692 RVA: 0x00039C74 File Offset: 0x00037E74
		public static float Acceleration(bool shiftPressed, bool altPressed)
		{
			return (float)(shiftPressed ? 4 : 1) * (altPressed ? 0.25f : 1f);
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x00039CA0 File Offset: 0x00037EA0
		public static float NiceDelta(Vector2 deviceDelta, float acceleration)
		{
			deviceDelta.y = -deviceDelta.y;
			bool flag = Mathf.Abs(Mathf.Abs(deviceDelta.x) - Mathf.Abs(deviceDelta.y)) / Mathf.Max(Mathf.Abs(deviceDelta.x), Mathf.Abs(deviceDelta.y)) > 0.1f;
			if (flag)
			{
				bool flag2 = Mathf.Abs(deviceDelta.x) > Mathf.Abs(deviceDelta.y);
				if (flag2)
				{
					NumericFieldDraggerUtility.s_UseYSign = false;
				}
				else
				{
					NumericFieldDraggerUtility.s_UseYSign = true;
				}
			}
			bool flag3 = NumericFieldDraggerUtility.s_UseYSign;
			float num;
			if (flag3)
			{
				num = Mathf.Sign(deviceDelta.y) * deviceDelta.magnitude * acceleration;
			}
			else
			{
				num = Mathf.Sign(deviceDelta.x) * deviceDelta.magnitude * acceleration;
			}
			return num;
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x00039D68 File Offset: 0x00037F68
		public static double CalculateFloatDragSensitivity(double value)
		{
			bool flag = double.IsInfinity(value) || double.IsNaN(value);
			double num;
			if (flag)
			{
				num = 0.0;
			}
			else
			{
				num = Math.Max(1.0, Math.Pow(Math.Abs(value), 0.5)) * 0.029999999329447746;
			}
			return num;
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x00039DC8 File Offset: 0x00037FC8
		public static double CalculateFloatDragSensitivity(double value, double minValue, double maxValue)
		{
			bool flag = double.IsInfinity(value) || double.IsNaN(value);
			double num;
			if (flag)
			{
				num = 0.0;
			}
			else
			{
				double num2 = Math.Abs(maxValue - minValue);
				num = num2 / 100.0 * 0.029999999329447746;
			}
			return num;
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x00039E1C File Offset: 0x0003801C
		public static long CalculateIntDragSensitivity(long value)
		{
			return (long)NumericFieldDraggerUtility.CalculateIntDragSensitivity((double)value);
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x00039E38 File Offset: 0x00038038
		public static ulong CalculateIntDragSensitivity(ulong value)
		{
			return (ulong)NumericFieldDraggerUtility.CalculateIntDragSensitivity(value);
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x00039E54 File Offset: 0x00038054
		public static double CalculateIntDragSensitivity(double value)
		{
			return Math.Max(1.0, Math.Pow(Math.Abs(value), 0.5) * 0.029999999329447746);
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x00039E94 File Offset: 0x00038094
		public static long CalculateIntDragSensitivity(long value, long minValue, long maxValue)
		{
			long num = Math.Abs(maxValue - minValue);
			return Math.Max(1L, (long)(0.03f * (float)num / 100f));
		}

		// Token: 0x040008F5 RID: 2293
		private static bool s_UseYSign;

		// Token: 0x040008F6 RID: 2294
		private const float kDragSensitivity = 0.03f;
	}
}
