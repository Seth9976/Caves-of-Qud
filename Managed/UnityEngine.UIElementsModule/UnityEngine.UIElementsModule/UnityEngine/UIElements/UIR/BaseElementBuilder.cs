using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200061C RID: 1564
	internal abstract class BaseElementBuilder
	{
		// Token: 0x06002E3E RID: 11838
		public abstract bool RequiresStencilMask(VisualElement ve);

		// Token: 0x06002E3F RID: 11839 RVA: 0x000A48D4 File Offset: 0x000A2AD4
		public void Build(MeshGenerationContext mgc)
		{
			VisualElement visualElement = mgc.visualElement;
			Debug.Assert(visualElement.areAncestorsAndSelfDisplayed);
			bool shouldCutRenderChain = visualElement.shouldCutRenderChain;
			if (shouldCutRenderChain)
			{
				mgc.entryRecorder.CutRenderChain(mgc.parentEntry);
			}
			bool isGroupTransform = visualElement.renderChainData.isGroupTransform;
			bool flag = isGroupTransform;
			if (flag)
			{
				mgc.entryRecorder.PushGroupMatrix(mgc.parentEntry);
			}
			bool flag2 = visualElement.subRenderTargetMode > VisualElement.RenderTargetMode.None;
			bool flag3 = flag2;
			if (flag3)
			{
				mgc.entryRecorder.PushRenderTexture(mgc.parentEntry);
			}
			bool flag4 = visualElement.defaultMaterial != null;
			bool flag5 = flag4;
			if (flag5)
			{
				mgc.entryRecorder.PushDefaultMaterial(mgc.parentEntry, visualElement.defaultMaterial);
			}
			bool flag6 = false;
			bool visible = visualElement.visible;
			if (visible)
			{
				this.DrawVisualElementBackground(mgc);
				this.DrawVisualElementBorder(mgc);
				this.PushVisualElementClipping(mgc);
				flag6 = true;
				BaseElementBuilder.InvokeGenerateVisualContent(mgc);
			}
			else
			{
				bool flag7 = visualElement.renderChainData.clipMethod == ClipMethod.Stencil;
				bool flag8 = visualElement.renderChainData.clipMethod == ClipMethod.Scissor;
				bool flag9 = flag8 || flag7;
				if (flag9)
				{
					flag6 = true;
					this.PushVisualElementClipping(mgc);
				}
			}
			mgc.entryRecorder.DrawChildren(mgc.parentEntry);
			bool flag10 = flag6;
			if (flag10)
			{
				BaseElementBuilder.PopVisualElementClipping(mgc);
			}
			bool flag11 = flag4;
			if (flag11)
			{
				mgc.entryRecorder.PopDefaultMaterial(mgc.parentEntry);
			}
			bool flag12 = isGroupTransform;
			if (flag12)
			{
				mgc.entryRecorder.PopGroupMatrix(mgc.parentEntry);
			}
			bool flag13 = flag2;
			if (flag13)
			{
				mgc.entryRecorder.BlitAndPopRenderTexture(mgc.parentEntry);
			}
		}

		// Token: 0x06002E40 RID: 11840
		protected abstract void DrawVisualElementBackground(MeshGenerationContext mgc);

		// Token: 0x06002E41 RID: 11841
		protected abstract void DrawVisualElementBorder(MeshGenerationContext mgc);

		// Token: 0x06002E42 RID: 11842
		protected abstract void DrawVisualElementStencilMask(MeshGenerationContext mgc);

		// Token: 0x06002E43 RID: 11843
		public abstract void ScheduleMeshGenerationJobs(MeshGenerationContext mgc);

		// Token: 0x06002E44 RID: 11844 RVA: 0x000A4A64 File Offset: 0x000A2C64
		private void PushVisualElementClipping(MeshGenerationContext mgc)
		{
			VisualElement visualElement = mgc.visualElement;
			bool flag = visualElement.renderChainData.clipMethod == ClipMethod.Scissor;
			if (flag)
			{
				mgc.entryRecorder.PushScissors(mgc.parentEntry);
			}
			else
			{
				bool flag2 = visualElement.renderChainData.clipMethod == ClipMethod.Stencil;
				if (flag2)
				{
					mgc.entryRecorder.BeginStencilMask(mgc.parentEntry);
					this.DrawVisualElementStencilMask(mgc);
					mgc.entryRecorder.EndStencilMask(mgc.parentEntry);
				}
			}
			mgc.entryRecorder.PushClippingRect(mgc.parentEntry);
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x000A4AF4 File Offset: 0x000A2CF4
		private static void PopVisualElementClipping(MeshGenerationContext mgc)
		{
			VisualElement visualElement = mgc.visualElement;
			mgc.entryRecorder.PopClippingRect(mgc.parentEntry);
			bool flag = visualElement.renderChainData.clipMethod == ClipMethod.Scissor;
			if (flag)
			{
				mgc.entryRecorder.PopScissors(mgc.parentEntry);
			}
			else
			{
				bool flag2 = visualElement.renderChainData.clipMethod == ClipMethod.Stencil;
				if (flag2)
				{
					mgc.entryRecorder.PopStencilMask(mgc.parentEntry);
				}
			}
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x000A4B68 File Offset: 0x000A2D68
		private static void InvokeGenerateVisualContent(MeshGenerationContext mgc)
		{
			VisualElement visualElement = mgc.visualElement;
			Painter2D.isPainterActive = true;
			visualElement.InvokeGenerateVisualContent(mgc);
			Painter2D.isPainterActive = false;
		}
	}
}
