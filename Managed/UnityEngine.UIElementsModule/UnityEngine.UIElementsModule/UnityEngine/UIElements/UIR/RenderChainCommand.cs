using System;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000635 RID: 1589
	internal class RenderChainCommand : LinkedPoolItem<RenderChainCommand>
	{
		// Token: 0x06002EAF RID: 11951 RVA: 0x000A8F60 File Offset: 0x000A7160
		internal void Reset()
		{
			this.owner = null;
			this.prev = (this.next = null);
			this.isTail = false;
			this.type = CommandType.Draw;
			this.state = default(State);
			this.mesh = null;
			this.indexOffset = (this.indexCount = 0);
			this.callback = null;
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x000A8FC0 File Offset: 0x000A71C0
		internal void ExecuteNonDrawMesh(DrawParams drawParams, float pixelsPerPoint, ref Exception immediateException)
		{
			switch (this.type)
			{
			case CommandType.ImmediateCull:
			{
				bool flag = !RenderChainCommand.RectPointsToPixelsAndFlipYAxis(this.owner.worldBound, pixelsPerPoint).Overlaps(Utility.GetActiveViewport());
				if (flag)
				{
					return;
				}
				break;
			}
			case CommandType.Immediate:
				break;
			case CommandType.PushView:
			{
				drawParams.view.Push(this.owner.worldTransform);
				GL.modelview = this.owner.worldTransform;
				VisualElement parent = this.owner.hierarchy.parent;
				bool flag2 = parent != null;
				Rect rect;
				if (flag2)
				{
					rect = parent.worldClip;
				}
				else
				{
					rect = DrawParams.k_FullNormalizedRect;
				}
				drawParams.scissor.Push(rect);
				Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(rect, pixelsPerPoint));
				return;
			}
			case CommandType.PopView:
			{
				drawParams.view.Pop();
				GL.modelview = drawParams.view.Peek();
				drawParams.scissor.Pop();
				Rect rect2 = drawParams.scissor.Peek();
				bool flag3 = rect2.x == DrawParams.k_UnlimitedRect.x;
				if (flag3)
				{
					Utility.DisableScissor();
				}
				else
				{
					Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(rect2, pixelsPerPoint));
				}
				return;
			}
			case CommandType.PushScissor:
				RenderChainCommand.PushScissor(drawParams, this.owner.worldClip, pixelsPerPoint);
				return;
			case CommandType.PopScissor:
				RenderChainCommand.PopScissor(drawParams, pixelsPerPoint);
				return;
			case CommandType.PushRenderTexture:
			{
				RectInt activeViewport = Utility.GetActiveViewport();
				RenderTexture temporary = RenderTexture.GetTemporary(activeViewport.width, activeViewport.height, 24, RenderTextureFormat.ARGBHalf);
				RenderTexture.active = temporary;
				GL.Clear(true, true, new Color(0f, 0f, 0f, 0f), 0.99f);
				drawParams.renderTexture.Add(RenderTexture.active);
				return;
			}
			case CommandType.PopRenderTexture:
			{
				int num = drawParams.renderTexture.Count - 1;
				Debug.Assert(num > 0);
				Debug.Assert(drawParams.renderTexture[num - 1] == RenderTexture.active, "Content of previous render texture was probably not blitted");
				RenderTexture renderTexture = drawParams.renderTexture[num];
				bool flag4 = renderTexture != null;
				if (flag4)
				{
					RenderTexture.ReleaseTemporary(renderTexture);
				}
				drawParams.renderTexture.RemoveAt(num);
				return;
			}
			case CommandType.BlitToPreviousRT:
			{
				RenderTexture renderTexture2 = drawParams.renderTexture[drawParams.renderTexture.Count - 1];
				RenderTexture renderTexture3 = drawParams.renderTexture[drawParams.renderTexture.Count - 2];
				Debug.Assert(renderTexture2 == RenderTexture.active, "Unexpected render target change: Current renderTarget is not the one on the top of the stack");
				this.Blit(renderTexture2, renderTexture3, 0f);
				return;
			}
			case CommandType.PushDefaultMaterial:
				return;
			case CommandType.PopDefaultMaterial:
				return;
			default:
				return;
			}
			bool flag5 = immediateException != null;
			if (!flag5)
			{
				Matrix4x4 unityProjectionMatrix = Utility.GetUnityProjectionMatrix();
				Camera current = Camera.current;
				RenderTexture active = RenderTexture.active;
				GL.modelview = this.owner.worldTransform;
				RenderChainCommand.PushScissor(drawParams, this.owner.worldClip, pixelsPerPoint);
				try
				{
					this.callback();
				}
				catch (Exception ex)
				{
					immediateException = ex;
				}
				RenderChainCommand.PopScissor(drawParams, pixelsPerPoint);
				Camera.SetupCurrent(current);
				RenderTexture.active = active;
				GL.modelview = drawParams.view.Peek();
				GL.LoadProjectionMatrix(unityProjectionMatrix);
				GL.invertCulling = false;
			}
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x000A9340 File Offset: 0x000A7540
		private void Blit(Texture source, RenderTexture destination, float depth)
		{
			GL.PushMatrix();
			GL.LoadOrtho();
			RenderTexture.active = destination;
			this.state.material.SetTexture(RenderChainCommand.k_ID_MainTex, source);
			this.state.material.SetPass(0);
			GL.Begin(7);
			GL.TexCoord2(0f, 0f);
			GL.Vertex3(0f, 0f, depth);
			GL.TexCoord2(0f, 1f);
			GL.Vertex3(0f, 1f, depth);
			GL.TexCoord2(1f, 1f);
			GL.Vertex3(1f, 1f, depth);
			GL.TexCoord2(1f, 0f);
			GL.Vertex3(1f, 0f, depth);
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x000A9424 File Offset: 0x000A7624
		private static Vector4 RectToClipSpace(Rect rc)
		{
			Matrix4x4 deviceProjectionMatrix = Utility.GetDeviceProjectionMatrix();
			Vector3 vector = deviceProjectionMatrix.MultiplyPoint(new Vector3(rc.xMin, rc.yMin, 0f));
			Vector3 vector2 = deviceProjectionMatrix.MultiplyPoint(new Vector3(rc.xMax, rc.yMax, 0f));
			return new Vector4(Mathf.Min(vector.x, vector2.x), Mathf.Min(vector.y, vector2.y), Mathf.Max(vector.x, vector2.x), Mathf.Max(vector.y, vector2.y));
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x000A94C8 File Offset: 0x000A76C8
		public static void PushScissor(DrawParams drawParams, Rect scissor, float pixelsPerPoint)
		{
			Rect rect = RenderChainCommand.CombineScissorRects(scissor, drawParams.scissor.Peek());
			drawParams.scissor.Push(rect);
			Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(rect, pixelsPerPoint));
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000A9504 File Offset: 0x000A7704
		public static void PopScissor(DrawParams drawParams, float pixelsPerPoint)
		{
			drawParams.scissor.Pop();
			Rect rect = drawParams.scissor.Peek();
			bool flag = rect.x == DrawParams.k_UnlimitedRect.x;
			if (flag)
			{
				Utility.DisableScissor();
			}
			else
			{
				Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(rect, pixelsPerPoint));
			}
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x000A955C File Offset: 0x000A775C
		private static Rect CombineScissorRects(Rect r0, Rect r1)
		{
			Rect rect = new Rect(0f, 0f, 0f, 0f);
			rect.x = Math.Max(r0.x, r1.x);
			rect.y = Math.Max(r0.y, r1.y);
			rect.xMax = Math.Max(rect.x, Math.Min(r0.xMax, r1.xMax));
			rect.yMax = Math.Max(rect.y, Math.Min(r0.yMax, r1.yMax));
			return rect;
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x000A9610 File Offset: 0x000A7810
		private static RectInt RectPointsToPixelsAndFlipYAxis(Rect rect, float pixelsPerPoint)
		{
			float num = (float)Utility.GetActiveViewport().height;
			return new RectInt(0, 0, 0, 0)
			{
				x = Mathf.RoundToInt(rect.x * pixelsPerPoint),
				y = Mathf.RoundToInt(num - rect.yMax * pixelsPerPoint),
				width = Mathf.RoundToInt(rect.width * pixelsPerPoint),
				height = Mathf.RoundToInt(rect.height * pixelsPerPoint)
			};
		}

		// Token: 0x040015A9 RID: 5545
		internal VisualElement owner;

		// Token: 0x040015AA RID: 5546
		internal RenderChainCommand prev;

		// Token: 0x040015AB RID: 5547
		internal RenderChainCommand next;

		// Token: 0x040015AC RID: 5548
		internal bool isTail;

		// Token: 0x040015AD RID: 5549
		internal CommandType type;

		// Token: 0x040015AE RID: 5550
		internal State state;

		// Token: 0x040015AF RID: 5551
		internal MeshHandle mesh;

		// Token: 0x040015B0 RID: 5552
		internal int indexOffset;

		// Token: 0x040015B1 RID: 5553
		internal int indexCount;

		// Token: 0x040015B2 RID: 5554
		internal Action callback;

		// Token: 0x040015B3 RID: 5555
		private static readonly int k_ID_MainTex = Shader.PropertyToID("_MainTex");

		// Token: 0x040015B4 RID: 5556
		private static ProfilerMarker s_ImmediateOverheadMarker = new ProfilerMarker("UIR.ImmediateOverhead");
	}
}
