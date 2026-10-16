using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x0200056C RID: 1388
	[NativeHeader("Runtime/Shaders/RayTracing/RayTracingAccelerationStructure.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/RayTracing/RayTracingShader.h")]
	[MovedFrom("UnityEngine.Experimental.Rendering")]
	internal class RayTracingShaderHelpURLAttribute : HelpURLAttribute
	{
		// Token: 0x060035F5 RID: 13813 RVA: 0x00069DAF File Offset: 0x00067FAF
		public RayTracingShaderHelpURLAttribute()
			: base(null)
		{
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x060035F6 RID: 13814 RVA: 0x00069DBC File Offset: 0x00067FBC
		public override string URL
		{
			get
			{
				return string.Format("https://docs.unity3d.com//{0}.{1}/Documentation/ScriptReference/Rendering.RayTracingShader.html", Application.unityVersionVer, Application.unityVersionMaj);
			}
		}
	}
}
