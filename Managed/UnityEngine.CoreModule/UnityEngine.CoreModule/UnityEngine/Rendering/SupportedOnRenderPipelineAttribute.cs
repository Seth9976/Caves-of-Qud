using System;
using System.Reflection;
using Unity.Collections;

namespace UnityEngine.Rendering
{
	// Token: 0x020004F4 RID: 1268
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class SupportedOnRenderPipelineAttribute : Attribute
	{
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060031A0 RID: 12704 RVA: 0x0005F5D8 File Offset: 0x0005D7D8
		public Type[] renderPipelineTypes { get; }

		// Token: 0x060031A1 RID: 12705 RVA: 0x0005F5E0 File Offset: 0x0005D7E0
		public SupportedOnRenderPipelineAttribute(Type renderPipeline)
			: this(new Type[] { renderPipeline })
		{
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x0005F5F4 File Offset: 0x0005D7F4
		public SupportedOnRenderPipelineAttribute(params Type[] renderPipeline)
		{
			bool flag = renderPipeline == null;
			if (flag)
			{
				Debug.LogError("The SupportedOnRenderPipelineAttribute parameters cannot be null.");
			}
			else
			{
				foreach (Type type in renderPipeline)
				{
					bool flag2 = type != null && typeof(RenderPipelineAsset).IsAssignableFrom(type);
					if (!flag2)
					{
						Debug.LogError("The SupportedOnRenderPipelineAttribute Attribute targets an invalid RenderPipelineAsset. One of the types cannot be assigned from RenderPipelineAsset: [" + renderPipeline.SerializedView<Type>((Type t) => t.Name) + "].");
						return;
					}
				}
				this.renderPipelineTypes = ((renderPipeline.Length == 0) ? SupportedOnRenderPipelineAttribute.k_DefaultRenderPipelineAsset.Value : renderPipeline);
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060031A3 RID: 12707 RVA: 0x0005F6B0 File Offset: 0x0005D8B0
		public bool isSupportedOnCurrentPipeline
		{
			get
			{
				return SupportedOnRenderPipelineAttribute.GetSupportedMode(this.renderPipelineTypes, GraphicsSettings.currentRenderPipelineAssetType) > SupportedOnRenderPipelineAttribute.SupportedMode.Unsupported;
			}
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x0005F6C5 File Offset: 0x0005D8C5
		public SupportedOnRenderPipelineAttribute.SupportedMode GetSupportedMode(Type renderPipelineAssetType)
		{
			return SupportedOnRenderPipelineAttribute.GetSupportedMode(this.renderPipelineTypes, renderPipelineAssetType);
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x0005F6D4 File Offset: 0x0005D8D4
		internal static SupportedOnRenderPipelineAttribute.SupportedMode GetSupportedMode(Type[] renderPipelineTypes, Type renderPipelineAssetType)
		{
			bool flag = renderPipelineTypes == null;
			if (flag)
			{
				throw new ArgumentNullException("Parameter renderPipelineTypes cannot be null.");
			}
			bool flag2 = renderPipelineAssetType == null;
			SupportedOnRenderPipelineAttribute.SupportedMode supportedMode;
			if (flag2)
			{
				supportedMode = SupportedOnRenderPipelineAttribute.SupportedMode.Unsupported;
			}
			else
			{
				for (int i = 0; i < renderPipelineTypes.Length; i++)
				{
					bool flag3 = renderPipelineTypes[i] == renderPipelineAssetType;
					if (flag3)
					{
						return SupportedOnRenderPipelineAttribute.SupportedMode.Supported;
					}
				}
				for (int j = 0; j < renderPipelineTypes.Length; j++)
				{
					bool flag4 = renderPipelineTypes[j].IsAssignableFrom(renderPipelineAssetType);
					if (flag4)
					{
						return SupportedOnRenderPipelineAttribute.SupportedMode.SupportedByBaseClass;
					}
				}
				supportedMode = SupportedOnRenderPipelineAttribute.SupportedMode.Unsupported;
			}
			return supportedMode;
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x0005F760 File Offset: 0x0005D960
		public static bool IsTypeSupportedOnRenderPipeline(Type type, Type renderPipelineAssetType)
		{
			SupportedOnRenderPipelineAttribute customAttribute = type.GetCustomAttribute<SupportedOnRenderPipelineAttribute>();
			return customAttribute == null || customAttribute.GetSupportedMode(renderPipelineAssetType) > SupportedOnRenderPipelineAttribute.SupportedMode.Unsupported;
		}

		// Token: 0x04000F54 RID: 3924
		private static readonly Lazy<Type[]> k_DefaultRenderPipelineAsset = new Lazy<Type[]>(() => new Type[] { typeof(RenderPipelineAsset) });

		// Token: 0x020004F5 RID: 1269
		public enum SupportedMode
		{
			// Token: 0x04000F57 RID: 3927
			Unsupported,
			// Token: 0x04000F58 RID: 3928
			Supported,
			// Token: 0x04000F59 RID: 3929
			SupportedByBaseClass
		}
	}
}
