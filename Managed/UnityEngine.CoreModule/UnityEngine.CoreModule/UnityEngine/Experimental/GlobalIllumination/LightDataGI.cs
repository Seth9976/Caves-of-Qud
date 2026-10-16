using System;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x02000581 RID: 1409
	[UsedByNativeCode]
	public struct LightDataGI
	{
		// Token: 0x0600368C RID: 13964 RVA: 0x0006B328 File Offset: 0x00069528
		public void Init(ref DirectionalLight light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = 0f;
			this.coneAngle = cookie.sizes.x;
			this.innerConeAngle = cookie.sizes.y;
			this.shape0 = light.penumbraWidthRadian;
			this.shape1 = 0f;
			this.type = LightType.Directional;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = FalloffType.Undefined;
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x0006B3FC File Offset: 0x000695FC
		public void Init(ref PointLight light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = light.range;
			this.coneAngle = 0f;
			this.innerConeAngle = 0f;
			this.shape0 = light.sphereRadius;
			this.shape1 = 0f;
			this.type = LightType.Point;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = light.falloff;
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x0006B4CC File Offset: 0x000696CC
		public void Init(ref SpotLight light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = light.range;
			this.coneAngle = light.coneAngle;
			this.innerConeAngle = light.innerConeAngle;
			this.shape0 = light.sphereRadius;
			this.shape1 = (float)light.angularFalloff;
			this.type = LightType.Spot;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = light.falloff;
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x0006B5A0 File Offset: 0x000697A0
		public void Init(ref RectangleLight light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = light.range;
			this.coneAngle = 0f;
			this.innerConeAngle = 0f;
			this.shape0 = light.width;
			this.shape1 = light.height;
			this.type = LightType.Rectangle;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = light.falloff;
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x0006B670 File Offset: 0x00069870
		public void Init(ref DiscLight light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = light.range;
			this.coneAngle = 0f;
			this.innerConeAngle = 0f;
			this.shape0 = light.radius;
			this.shape1 = 0f;
			this.type = LightType.Disc;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = light.falloff;
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x0006B740 File Offset: 0x00069940
		public void Init(ref SpotLightBoxShape light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = light.range;
			this.coneAngle = 0f;
			this.innerConeAngle = 0f;
			this.shape0 = light.width;
			this.shape1 = light.height;
			this.type = LightType.SpotBoxShape;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = FalloffType.Undefined;
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x0006B80C File Offset: 0x00069A0C
		public void Init(ref SpotLightPyramidShape light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = light.range;
			this.coneAngle = light.angle;
			this.innerConeAngle = 0f;
			this.shape0 = light.aspectRatio;
			this.shape1 = 0f;
			this.type = LightType.SpotPyramidShape;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = light.falloff;
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x0006B8DC File Offset: 0x00069ADC
		public void Init(ref DirectionalLight light)
		{
			Cookie cookie = Cookie.Defaults();
			this.Init(ref light, ref cookie);
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x0006B8FC File Offset: 0x00069AFC
		public void Init(ref PointLight light)
		{
			Cookie cookie = Cookie.Defaults();
			this.Init(ref light, ref cookie);
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x0006B91C File Offset: 0x00069B1C
		public void Init(ref SpotLight light)
		{
			Cookie cookie = Cookie.Defaults();
			this.Init(ref light, ref cookie);
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x0006B93C File Offset: 0x00069B3C
		public void Init(ref RectangleLight light)
		{
			Cookie cookie = Cookie.Defaults();
			this.Init(ref light, ref cookie);
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x0006B95C File Offset: 0x00069B5C
		public void Init(ref DiscLight light)
		{
			Cookie cookie = Cookie.Defaults();
			this.Init(ref light, ref cookie);
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x0006B97C File Offset: 0x00069B7C
		public void Init(ref SpotLightBoxShape light)
		{
			Cookie cookie = Cookie.Defaults();
			this.Init(ref light, ref cookie);
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x0006B99C File Offset: 0x00069B9C
		public void Init(ref SpotLightPyramidShape light)
		{
			Cookie cookie = Cookie.Defaults();
			this.Init(ref light, ref cookie);
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x0006B9BA File Offset: 0x00069BBA
		public void InitNoBake(int lightInstanceID)
		{
			this.instanceID = lightInstanceID;
			this.mode = LightMode.Unknown;
		}

		// Token: 0x040012C9 RID: 4809
		public int instanceID;

		// Token: 0x040012CA RID: 4810
		public int cookieID;

		// Token: 0x040012CB RID: 4811
		public float cookieScale;

		// Token: 0x040012CC RID: 4812
		public LinearColor color;

		// Token: 0x040012CD RID: 4813
		public LinearColor indirectColor;

		// Token: 0x040012CE RID: 4814
		public Quaternion orientation;

		// Token: 0x040012CF RID: 4815
		public Vector3 position;

		// Token: 0x040012D0 RID: 4816
		public float range;

		// Token: 0x040012D1 RID: 4817
		public float coneAngle;

		// Token: 0x040012D2 RID: 4818
		public float innerConeAngle;

		// Token: 0x040012D3 RID: 4819
		public float shape0;

		// Token: 0x040012D4 RID: 4820
		public float shape1;

		// Token: 0x040012D5 RID: 4821
		public LightType type;

		// Token: 0x040012D6 RID: 4822
		public LightMode mode;

		// Token: 0x040012D7 RID: 4823
		public byte shadow;

		// Token: 0x040012D8 RID: 4824
		public FalloffType falloff;
	}
}
