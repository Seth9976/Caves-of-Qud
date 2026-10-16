using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x02000582 RID: 1410
	public static class LightmapperUtils
	{
		// Token: 0x0600369B RID: 13979 RVA: 0x0006B9CC File Offset: 0x00069BCC
		public static LightMode Extract(LightmapBakeType baketype)
		{
			return (baketype == LightmapBakeType.Realtime) ? LightMode.Realtime : ((baketype == LightmapBakeType.Mixed) ? LightMode.Mixed : LightMode.Baked);
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x0006B9F0 File Offset: 0x00069BF0
		public static LinearColor ExtractIndirect(Light l)
		{
			return LinearColor.Convert(l.color, l.intensity * l.bounceIntensity);
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x0006BA1C File Offset: 0x00069C1C
		public static float ExtractInnerCone(Light l)
		{
			return 2f * Mathf.Atan(Mathf.Tan(l.spotAngle * 0.5f * 0.017453292f) * 46f / 64f);
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x0006BA5C File Offset: 0x00069C5C
		private static Color ExtractColorTemperature(Light l)
		{
			Color color = new Color(1f, 1f, 1f);
			bool flag = l.useColorTemperature && GraphicsSettings.lightsUseLinearIntensity;
			if (flag)
			{
				color = Mathf.CorrelatedColorTemperatureToRGB(l.colorTemperature);
			}
			return color;
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x0006BAA5 File Offset: 0x00069CA5
		private static void ApplyColorTemperature(Color cct, ref LinearColor lightColor)
		{
			lightColor.red *= cct.r;
			lightColor.green *= cct.g;
			lightColor.blue *= cct.b;
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x0006BAE4 File Offset: 0x00069CE4
		public static void Extract(Light l, ref DirectionalLight dir)
		{
			dir.instanceID = l.GetInstanceID();
			dir.mode = LightmapperUtils.Extract(l.bakingOutput.lightmapBakeType);
			dir.shadow = l.shadows > LightShadows.None;
			dir.position = l.transform.position;
			dir.orientation = l.transform.rotation;
			Color color = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor linearColor = LinearColor.Convert(l.color, l.intensity);
			LinearColor linearColor2 = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(color, ref linearColor);
			LightmapperUtils.ApplyColorTemperature(color, ref linearColor2);
			dir.color = linearColor;
			dir.indirectColor = linearColor2;
			dir.penumbraWidthRadian = 0f;
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x0006BB90 File Offset: 0x00069D90
		public static void Extract(Light l, ref PointLight point)
		{
			point.instanceID = l.GetInstanceID();
			point.mode = LightmapperUtils.Extract(l.bakingOutput.lightmapBakeType);
			point.shadow = l.shadows > LightShadows.None;
			point.position = l.transform.position;
			point.orientation = l.transform.rotation;
			Color color = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor linearColor = LinearColor.Convert(l.color, l.intensity);
			LinearColor linearColor2 = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(color, ref linearColor);
			LightmapperUtils.ApplyColorTemperature(color, ref linearColor2);
			point.color = linearColor;
			point.indirectColor = linearColor2;
			point.range = l.range;
			point.sphereRadius = 0f;
			point.falloff = FalloffType.Legacy;
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x0006BC50 File Offset: 0x00069E50
		public static void Extract(Light l, ref SpotLight spot)
		{
			spot.instanceID = l.GetInstanceID();
			spot.mode = LightmapperUtils.Extract(l.bakingOutput.lightmapBakeType);
			spot.shadow = l.shadows > LightShadows.None;
			spot.position = l.transform.position;
			spot.orientation = l.transform.rotation;
			Color color = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor linearColor = LinearColor.Convert(l.color, l.intensity);
			LinearColor linearColor2 = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(color, ref linearColor);
			LightmapperUtils.ApplyColorTemperature(color, ref linearColor2);
			spot.color = linearColor;
			spot.indirectColor = linearColor2;
			spot.range = l.range;
			spot.sphereRadius = 0f;
			spot.coneAngle = l.spotAngle * 0.017453292f;
			spot.innerConeAngle = LightmapperUtils.ExtractInnerCone(l);
			spot.falloff = FalloffType.Legacy;
			spot.angularFalloff = AngularFalloffType.LUT;
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x0006BD34 File Offset: 0x00069F34
		public static void Extract(Light l, ref RectangleLight rect)
		{
			rect.instanceID = l.GetInstanceID();
			rect.mode = LightmapperUtils.Extract(l.bakingOutput.lightmapBakeType);
			rect.shadow = l.shadows > LightShadows.None;
			rect.position = l.transform.position;
			rect.orientation = l.transform.rotation;
			Color color = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor linearColor = LinearColor.Convert(l.color, l.intensity);
			LinearColor linearColor2 = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(color, ref linearColor);
			LightmapperUtils.ApplyColorTemperature(color, ref linearColor2);
			rect.color = linearColor;
			rect.indirectColor = linearColor2;
			rect.range = l.dilatedRange;
			rect.width = 0f;
			rect.height = 0f;
			rect.falloff = FalloffType.Legacy;
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x0006BE00 File Offset: 0x0006A000
		public static void Extract(Light l, ref DiscLight disc)
		{
			disc.instanceID = l.GetInstanceID();
			disc.mode = LightmapperUtils.Extract(l.bakingOutput.lightmapBakeType);
			disc.shadow = l.shadows > LightShadows.None;
			disc.position = l.transform.position;
			disc.orientation = l.transform.rotation;
			Color color = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor linearColor = LinearColor.Convert(l.color, l.intensity);
			LinearColor linearColor2 = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(color, ref linearColor);
			LightmapperUtils.ApplyColorTemperature(color, ref linearColor2);
			disc.color = linearColor;
			disc.indirectColor = linearColor2;
			disc.range = l.dilatedRange;
			disc.radius = 0f;
			disc.falloff = FalloffType.Legacy;
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x0006BEC0 File Offset: 0x0006A0C0
		public static void Extract(Light l, out Cookie cookie)
		{
			cookie.instanceID = (l.cookie ? l.cookie.GetInstanceID() : 0);
			cookie.scale = 1f;
			cookie.sizes = ((l.type == LightType.Directional && l.cookie) ? new Vector2(l.cookieSize, l.cookieSize) : new Vector2(1f, 1f));
		}
	}
}
