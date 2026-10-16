using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200063A RID: 1594
	internal class EntryProcessor
	{
		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x000A9BD5 File Offset: 0x000A7DD5
		// (set) Token: 0x06002ECC RID: 11980 RVA: 0x000A9BDD File Offset: 0x000A7DDD
		public RenderChainCommand firstHeadCommand { get; private set; }

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x000A9BE6 File Offset: 0x000A7DE6
		// (set) Token: 0x06002ECE RID: 11982 RVA: 0x000A9BEE File Offset: 0x000A7DEE
		public RenderChainCommand lastHeadCommand { get; private set; }

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06002ECF RID: 11983 RVA: 0x000A9BF7 File Offset: 0x000A7DF7
		// (set) Token: 0x06002ED0 RID: 11984 RVA: 0x000A9BFF File Offset: 0x000A7DFF
		public RenderChainCommand firstTailCommand { get; private set; }

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06002ED1 RID: 11985 RVA: 0x000A9C08 File Offset: 0x000A7E08
		// (set) Token: 0x06002ED2 RID: 11986 RVA: 0x000A9C10 File Offset: 0x000A7E10
		public RenderChainCommand lastTailCommand { get; private set; }

		// Token: 0x06002ED3 RID: 11987 RVA: 0x000A9C1C File Offset: 0x000A7E1C
		public void Init(Entry root, RenderChain renderChain, VisualElement ve)
		{
			UIRenderDevice device = renderChain.device;
			this.m_RenderChain = renderChain;
			this.m_CurrentElement = ve;
			this.m_PreProcessor.PreProcess(root);
			bool flag = this.m_PreProcessor.headAllocs.Count == 0 && ve.renderChainData.headMesh != null;
			if (flag)
			{
				device.Free(ve.renderChainData.headMesh);
				ve.renderChainData.headMesh = null;
			}
			bool flag2 = this.m_PreProcessor.tailAllocs.Count == 0 && ve.renderChainData.tailMesh != null;
			if (flag2)
			{
				device.Free(ve.renderChainData.tailMesh);
				ve.renderChainData.tailMesh = null;
			}
			bool hasExtraMeshes = ve.renderChainData.hasExtraMeshes;
			if (hasExtraMeshes)
			{
				renderChain.FreeExtraMeshes(ve);
			}
			renderChain.ResetTextures(ve);
			VisualElement parent = this.m_CurrentElement.hierarchy.parent;
			bool isGroupTransform = this.m_CurrentElement.renderChainData.isGroupTransform;
			bool flag3 = parent != null;
			if (flag3)
			{
				this.m_MaskDepthPopped = parent.renderChainData.childrenMaskDepth;
				this.m_StencilRefPopped = parent.renderChainData.childrenStencilRef;
				this.m_ClipRectIdPopped = (isGroupTransform ? UIRVEShaderInfoAllocator.infiniteClipRect : parent.renderChainData.clipRectID);
			}
			else
			{
				this.m_MaskDepthPopped = 0;
				this.m_StencilRefPopped = 0;
				this.m_ClipRectIdPopped = UIRVEShaderInfoAllocator.infiniteClipRect;
			}
			this.m_MaskDepthPushed = this.m_MaskDepthPopped + 1;
			this.m_StencilRefPushed = this.m_MaskDepthPopped;
			this.m_ClipRectIdPushed = this.m_CurrentElement.renderChainData.clipRectID;
			this.m_MaskDepth = this.m_MaskDepthPopped;
			this.m_StencilRef = this.m_StencilRefPopped;
			this.m_ClipRectId = this.m_ClipRectIdPopped;
			this.m_VertexDataComputed = false;
			this.m_Transform = Matrix4x4.identity;
			this.m_TextCoreSettingsPage = new Color32(0, 0, 0, 0);
			this.m_MaskMeshes.Clear();
			this.m_IsDrawingMask = false;
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x000A9E14 File Offset: 0x000A8014
		public void ClearReferences()
		{
			this.m_PreProcessor.ClearReferences();
			this.m_RenderChain = null;
			this.m_CurrentElement = null;
			this.m_Mesh = null;
			this.m_FirstCommand = null;
			this.m_LastCommand = null;
			this.firstHeadCommand = null;
			this.lastHeadCommand = null;
			this.firstTailCommand = null;
			this.lastTailCommand = null;
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000A9E74 File Offset: 0x000A8074
		public void ProcessHead()
		{
			this.m_IsTail = false;
			this.ProcessFirstAlloc(this.m_PreProcessor.headAllocs, ref this.m_CurrentElement.renderChainData.headMesh);
			this.m_FirstCommand = null;
			this.m_LastCommand = null;
			this.ProcessRange(0, this.m_PreProcessor.childrenIndex - 1);
			this.firstHeadCommand = this.m_FirstCommand;
			this.lastHeadCommand = this.m_LastCommand;
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000A9EE8 File Offset: 0x000A80E8
		public void ProcessTail()
		{
			this.m_IsTail = true;
			this.ProcessFirstAlloc(this.m_PreProcessor.tailAllocs, ref this.m_CurrentElement.renderChainData.tailMesh);
			this.m_FirstCommand = null;
			this.m_LastCommand = null;
			this.ProcessRange(this.m_PreProcessor.childrenIndex + 1, this.m_PreProcessor.flattenedEntries.Count - 1);
			this.firstTailCommand = this.m_FirstCommand;
			this.lastTailCommand = this.m_LastCommand;
			Debug.Assert(this.m_MaskDepth == this.m_MaskDepthPopped);
			Debug.Assert(this.m_MaskMeshes.Count == 0);
			Debug.Assert(!this.m_IsDrawingMask);
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000A9FA4 File Offset: 0x000A81A4
		private void ProcessRange(int first, int last)
		{
			List<Entry> flattenedEntries = this.m_PreProcessor.flattenedEntries;
			for (int i = first; i <= last; i++)
			{
				Entry entry = flattenedEntries[i];
				switch (entry.type)
				{
				case EntryType.DrawSolidMesh:
					this.m_RenderType = VertexFlags.IsSolid;
					this.ProcessMeshEntry(entry, TextureId.invalid);
					break;
				case EntryType.DrawTexturedMesh:
				{
					Texture texture = entry.texture;
					TextureId textureId = TextureId.invalid;
					bool flag = texture != null;
					if (flag)
					{
						RectInt rectInt;
						bool flag2 = this.m_RenderChain.atlas != null && this.m_RenderChain.atlas.TryGetAtlas(this.m_CurrentElement, texture as Texture2D, out textureId, out rectInt);
						if (flag2)
						{
							this.m_RenderType = VertexFlags.IsDynamic;
							this.m_AtlasRect = new Rect((float)rectInt.x, (float)rectInt.y, (float)rectInt.width, (float)rectInt.height);
							this.m_RemapUVs = true;
							this.m_RenderChain.InsertTexture(this.m_CurrentElement, texture, textureId, true);
						}
						else
						{
							this.m_RenderType = VertexFlags.IsTextured;
							textureId = TextureRegistry.instance.Acquire(texture);
							this.m_RenderChain.InsertTexture(this.m_CurrentElement, texture, textureId, false);
						}
					}
					else
					{
						this.m_RenderType = VertexFlags.IsSolid;
					}
					this.ProcessMeshEntry(entry, textureId);
					this.m_RemapUVs = false;
					break;
				}
				case EntryType.DrawTexturedMeshSkipAtlas:
				{
					this.m_RenderType = VertexFlags.IsTextured;
					TextureId textureId2 = TextureRegistry.instance.Acquire(entry.texture);
					this.m_RenderChain.InsertTexture(this.m_CurrentElement, entry.texture, textureId2, false);
					this.ProcessMeshEntry(entry, textureId2);
					break;
				}
				case EntryType.DrawTextMesh:
				{
					this.m_RenderType = VertexFlags.IsText;
					TextureId textureId3 = TextureRegistry.instance.Acquire(entry.texture);
					this.m_RenderChain.InsertTexture(this.m_CurrentElement, entry.texture, textureId3, false);
					this.ProcessMeshEntry(entry, textureId3);
					break;
				}
				case EntryType.DrawGradients:
				{
					this.m_RenderType = VertexFlags.IsSvgGradients;
					GradientRemap gradientRemap = this.m_RenderChain.vectorImageManager.AddUser(entry.gradientsOwner, this.m_CurrentElement);
					this.m_GradientSettingIndexOffset = gradientRemap.destIndex;
					bool flag3 = gradientRemap.atlas != TextureId.invalid;
					TextureId textureId4;
					if (flag3)
					{
						textureId4 = gradientRemap.atlas;
					}
					else
					{
						textureId4 = TextureRegistry.instance.Acquire(entry.gradientsOwner.atlas);
						this.m_RenderChain.InsertTexture(this.m_CurrentElement, entry.gradientsOwner.atlas, textureId4, false);
					}
					this.ProcessMeshEntry(entry, textureId4);
					this.m_GradientSettingIndexOffset = -1;
					break;
				}
				case EntryType.DrawImmediate:
				{
					RenderChainCommand renderChainCommand = this.m_RenderChain.AllocCommand();
					renderChainCommand.type = CommandType.Immediate;
					renderChainCommand.owner = this.m_CurrentElement;
					renderChainCommand.isTail = this.m_IsTail;
					renderChainCommand.callback = entry.immediateCallback;
					this.AppendCommand(renderChainCommand);
					break;
				}
				case EntryType.DrawImmediateCull:
				{
					RenderChainCommand renderChainCommand2 = this.m_RenderChain.AllocCommand();
					renderChainCommand2.type = CommandType.ImmediateCull;
					renderChainCommand2.owner = this.m_CurrentElement;
					renderChainCommand2.isTail = this.m_IsTail;
					renderChainCommand2.callback = entry.immediateCallback;
					this.AppendCommand(renderChainCommand2);
					break;
				}
				case EntryType.DrawChildren:
				case EntryType.DedicatedPlaceholder:
					Debug.Assert(false);
					break;
				case EntryType.BeginStencilMask:
					Debug.Assert(this.m_MaskDepth == this.m_MaskDepthPopped);
					Debug.Assert(!this.m_IsDrawingMask);
					this.m_IsDrawingMask = true;
					this.m_StencilRef = this.m_StencilRefPushed;
					Debug.Assert(this.m_MaskDepth == this.m_StencilRef);
					break;
				case EntryType.EndStencilMask:
					Debug.Assert(this.m_IsDrawingMask);
					this.m_IsDrawingMask = false;
					this.m_MaskDepth = this.m_MaskDepthPushed;
					break;
				case EntryType.PopStencilMask:
					Debug.Assert(this.m_MaskDepth == this.m_StencilRef + 1);
					this.DrawReverseMask();
					this.m_MaskDepth = this.m_MaskDepthPopped;
					this.m_StencilRef = this.m_StencilRefPopped;
					break;
				case EntryType.PushClippingRect:
					this.m_ClipRectId = this.m_ClipRectIdPushed;
					break;
				case EntryType.PopClippingRect:
					this.m_ClipRectId = this.m_ClipRectIdPopped;
					break;
				case EntryType.PushScissors:
				{
					RenderChainCommand renderChainCommand3 = this.m_RenderChain.AllocCommand();
					renderChainCommand3.type = CommandType.PushScissor;
					renderChainCommand3.owner = this.m_CurrentElement;
					renderChainCommand3.isTail = this.m_IsTail;
					this.AppendCommand(renderChainCommand3);
					break;
				}
				case EntryType.PopScissors:
				{
					RenderChainCommand renderChainCommand4 = this.m_RenderChain.AllocCommand();
					renderChainCommand4.type = CommandType.PopScissor;
					renderChainCommand4.owner = this.m_CurrentElement;
					renderChainCommand4.isTail = this.m_IsTail;
					this.AppendCommand(renderChainCommand4);
					break;
				}
				case EntryType.PushGroupMatrix:
				{
					RenderChainCommand renderChainCommand5 = this.m_RenderChain.AllocCommand();
					renderChainCommand5.type = CommandType.PushView;
					renderChainCommand5.owner = this.m_CurrentElement;
					renderChainCommand5.isTail = this.m_IsTail;
					this.AppendCommand(renderChainCommand5);
					break;
				}
				case EntryType.PopGroupMatrix:
				{
					RenderChainCommand renderChainCommand6 = this.m_RenderChain.AllocCommand();
					renderChainCommand6.type = CommandType.PopView;
					renderChainCommand6.owner = this.m_CurrentElement;
					renderChainCommand6.isTail = this.m_IsTail;
					this.AppendCommand(renderChainCommand6);
					break;
				}
				case EntryType.PushRenderTexture:
				{
					Debug.Assert(this.m_MaskDepth == 0, "The RenderTargetMode feature must not be used within a stencil mask.");
					RenderChainCommand renderChainCommand7 = this.m_RenderChain.AllocCommand();
					renderChainCommand7.type = CommandType.PushRenderTexture;
					renderChainCommand7.owner = this.m_CurrentElement;
					renderChainCommand7.isTail = this.m_IsTail;
					this.AppendCommand(renderChainCommand7);
					break;
				}
				case EntryType.BlitAndPopRenderTexture:
				{
					RenderChainCommand renderChainCommand8 = this.m_RenderChain.AllocCommand();
					renderChainCommand8.type = CommandType.BlitToPreviousRT;
					renderChainCommand8.owner = this.m_CurrentElement;
					renderChainCommand8.isTail = this.m_IsTail;
					renderChainCommand8.state.material = EntryProcessor.GetBlitMaterial(this.m_CurrentElement.subRenderTargetMode);
					Debug.Assert(renderChainCommand8.state.material != null);
					this.AppendCommand(renderChainCommand8);
					RenderChainCommand renderChainCommand9 = this.m_RenderChain.AllocCommand();
					renderChainCommand9.type = CommandType.PopRenderTexture;
					renderChainCommand9.owner = this.m_CurrentElement;
					renderChainCommand9.isTail = this.m_IsTail;
					this.AppendCommand(renderChainCommand9);
					break;
				}
				case EntryType.PushDefaultMaterial:
				{
					RenderChainCommand renderChainCommand10 = this.m_RenderChain.AllocCommand();
					renderChainCommand10.type = CommandType.PushDefaultMaterial;
					renderChainCommand10.owner = this.m_CurrentElement;
					renderChainCommand10.isTail = this.m_IsTail;
					renderChainCommand10.state.material = entry.material;
					this.AppendCommand(renderChainCommand10);
					break;
				}
				case EntryType.PopDefaultMaterial:
				{
					RenderChainCommand renderChainCommand11 = this.m_RenderChain.AllocCommand();
					renderChainCommand11.type = CommandType.PopDefaultMaterial;
					renderChainCommand11.owner = this.m_CurrentElement;
					renderChainCommand11.isTail = this.m_IsTail;
					this.AppendCommand(renderChainCommand11);
					break;
				}
				case EntryType.CutRenderChain:
				{
					RenderChainCommand renderChainCommand12 = this.m_RenderChain.AllocCommand();
					renderChainCommand12.type = CommandType.CutRenderChain;
					renderChainCommand12.owner = this.m_CurrentElement;
					renderChainCommand12.isTail = this.m_IsTail;
					this.AppendCommand(renderChainCommand12);
					break;
				}
				default:
					throw new NotImplementedException();
				}
			}
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x000AA6D0 File Offset: 0x000A88D0
		private void ProcessMeshEntry(Entry entry, TextureId textureId)
		{
			int length = entry.vertices.Length;
			int length2 = entry.indices.Length;
			Debug.Assert(length > 0 == length2 > 0);
			bool flag = length > 0 && length2 > 0;
			if (flag)
			{
				bool flag2 = this.m_VertsFilled + length > this.m_AllocVertexCount;
				if (flag2)
				{
					this.ProcessNextAlloc();
					Debug.Assert(this.m_VertsFilled + length <= this.m_AllocVertexCount);
				}
				bool flag3 = !this.m_VertexDataComputed;
				if (flag3)
				{
					UIRUtility.GetVerticesTransformInfo(this.m_CurrentElement, out this.m_Transform);
					this.m_CurrentElement.renderChainData.verticesSpace = this.m_Transform;
					this.m_TransformData = this.m_RenderChain.shaderInfoAllocator.TransformAllocToVertexData(this.m_CurrentElement.renderChainData.transformID);
					this.m_OpacityData = this.m_RenderChain.shaderInfoAllocator.OpacityAllocToVertexData(this.m_CurrentElement.renderChainData.opacityID);
					this.m_VertexDataComputed = true;
				}
				Color32 color = new Color32(this.m_OpacityData.r, this.m_OpacityData.g, 0, 0);
				Color32 color2 = this.m_RenderChain.shaderInfoAllocator.ClipRectAllocToVertexData(this.m_ClipRectId);
				Color32 color3 = new Color32(this.m_TransformData.b, color2.b, this.m_OpacityData.b, 0);
				Color32 color4 = new Color32(this.m_TransformData.r, this.m_TransformData.g, color2.r, color2.g);
				Color32 color5 = new Color32((byte)this.m_RenderType, 0, 0, 0);
				bool flag4 = (entry.flags & EntryFlags.UsesTextCoreSettings) > (EntryFlags)0;
				if (flag4)
				{
					Color32 color6 = this.m_RenderChain.shaderInfoAllocator.TextCoreSettingsToVertexData(this.m_CurrentElement.renderChainData.textCoreSettingsID);
					this.m_TextCoreSettingsPage.r = color6.r;
					this.m_TextCoreSettingsPage.g = color6.g;
					color3.a = color6.b;
				}
				NativeSlice<Vertex> nativeSlice = this.m_Verts.Slice<Vertex>(this.m_VertsFilled, length);
				int num = this.m_VertsFilled + (int)this.m_IndexOffset;
				NativeSlice<ushort> nativeSlice2 = this.m_Indices.Slice<ushort>(this.m_IndicesFilled, length2);
				bool flag5 = UIRUtility.ShapeWindingIsClockwise(this.m_MaskDepth, this.m_StencilRef);
				bool worldFlipsWinding = this.m_CurrentElement.renderChainData.worldFlipsWinding;
				ConvertMeshJobData convertMeshJobData = new ConvertMeshJobData
				{
					vertSrc = (IntPtr)entry.vertices.GetUnsafePtr<Vertex>(),
					vertDst = (IntPtr)nativeSlice.GetUnsafePtr<Vertex>(),
					vertCount = length,
					transform = this.m_Transform,
					xformClipPages = color4,
					ids = color3,
					addFlags = color5,
					opacityPage = color,
					textCoreSettingsPage = this.m_TextCoreSettingsPage,
					usesTextCoreSettings = (((entry.flags & EntryFlags.UsesTextCoreSettings) != (EntryFlags)0) ? 1 : 0),
					textureId = textureId.ConvertToGpu(),
					gradientSettingsIndexOffset = this.m_GradientSettingIndexOffset,
					indexSrc = (IntPtr)entry.indices.GetUnsafePtr<ushort>(),
					indexDst = (IntPtr)nativeSlice2.GetUnsafePtr<ushort>(),
					indexCount = nativeSlice2.Length,
					indexOffset = num,
					flipIndices = ((flag5 == worldFlipsWinding) ? 1 : 0),
					forceZ = (this.m_RenderChain.isFlat ? 1 : 0),
					positionZ = (this.m_IsDrawingMask ? 1f : 0f),
					remapUVs = (this.m_RemapUVs ? 1 : 0),
					atlasRect = this.m_AtlasRect
				};
				this.m_RenderChain.jobManager.Add(ref convertMeshJobData);
				bool isDrawingMask = this.m_IsDrawingMask;
				if (isDrawingMask)
				{
					this.m_MaskMeshes.Push(new EntryProcessor.MaskMesh
					{
						vertices = nativeSlice,
						indices = nativeSlice2,
						indexOffset = num
					});
				}
				RenderChainCommand renderChainCommand = this.CreateMeshDrawCommand(this.m_Mesh, length2, this.m_IndicesFilled, entry.material, textureId);
				this.AppendCommand(renderChainCommand);
				bool flag6 = entry.type == EntryType.DrawTextMesh;
				if (flag6)
				{
					renderChainCommand.state.sdfScale = entry.textScale;
					renderChainCommand.state.sharpness = entry.fontSharpness;
				}
				this.m_VertsFilled += length;
				this.m_IndicesFilled += length2;
			}
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x000AAB54 File Offset: 0x000A8D54
		private void DrawReverseMask()
		{
			for (;;)
			{
				EntryProcessor.MaskMesh maskMesh;
				bool flag = this.m_MaskMeshes.TryPop(out maskMesh);
				if (!flag)
				{
					break;
				}
				Debug.Assert(maskMesh.indices.Length > 0 == maskMesh.vertices.Length > 0);
				bool flag2 = maskMesh.indices.Length > 0 && maskMesh.vertices.Length > 0;
				if (flag2)
				{
					RenderChainCommand renderChainCommand = this.CreateMeshDrawCommand(this.m_Mesh, maskMesh.indices.Length, this.m_IndicesFilled, null, TextureId.invalid);
					this.AppendCommand(renderChainCommand);
					NativeSlice<Vertex> nativeSlice = this.m_Verts.Slice<Vertex>(this.m_VertsFilled, maskMesh.vertices.Length);
					NativeSlice<ushort> nativeSlice2 = this.m_Indices.Slice<ushort>(this.m_IndicesFilled, maskMesh.indices.Length);
					CopyMeshJobData copyMeshJobData = new CopyMeshJobData
					{
						vertSrc = (IntPtr)maskMesh.vertices.GetUnsafePtr<Vertex>(),
						vertDst = (IntPtr)nativeSlice.GetUnsafePtr<Vertex>(),
						vertCount = maskMesh.vertices.Length,
						indexSrc = (IntPtr)maskMesh.indices.GetUnsafePtr<ushort>(),
						indexDst = (IntPtr)nativeSlice2.GetUnsafePtr<ushort>(),
						indexCount = maskMesh.indices.Length,
						indexOffset = (int)this.m_IndexOffset + this.m_VertsFilled - maskMesh.indexOffset
					};
					this.m_RenderChain.jobManager.Add(ref copyMeshJobData);
					this.m_IndicesFilled += maskMesh.indices.Length;
					this.m_VertsFilled += maskMesh.vertices.Length;
				}
			}
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x000AAD20 File Offset: 0x000A8F20
		private RenderChainCommand CreateMeshDrawCommand(MeshHandle mesh, int indexCount, int indexOffset, Material material, TextureId texture)
		{
			RenderChainCommand renderChainCommand = this.m_RenderChain.AllocCommand();
			renderChainCommand.type = CommandType.Draw;
			renderChainCommand.state = new State
			{
				material = material,
				texture = texture,
				stencilRef = this.m_StencilRef
			};
			renderChainCommand.mesh = mesh;
			renderChainCommand.indexOffset = indexOffset;
			renderChainCommand.indexCount = indexCount;
			renderChainCommand.owner = this.m_CurrentElement;
			renderChainCommand.isTail = this.m_IsTail;
			return renderChainCommand;
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x000AADA4 File Offset: 0x000A8FA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void AppendCommand(RenderChainCommand next)
		{
			bool flag = this.m_FirstCommand == null;
			if (flag)
			{
				this.m_FirstCommand = next;
				this.m_LastCommand = next;
			}
			else
			{
				next.prev = this.m_LastCommand;
				this.m_LastCommand.next = next;
				this.m_LastCommand = next;
			}
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x000AADF4 File Offset: 0x000A8FF4
		private void ProcessFirstAlloc(List<EntryPreProcessor.AllocSize> allocList, ref MeshHandle mesh)
		{
			bool flag = allocList.Count > 0;
			if (flag)
			{
				EntryPreProcessor.AllocSize allocSize = allocList[0];
				EntryProcessor.UpdateOrAllocate(ref mesh, allocSize.vertexCount, allocSize.indexCount, this.m_RenderChain.device, out this.m_Verts, out this.m_Indices, out this.m_IndexOffset, this.m_RenderChain.statsByRef);
				this.m_AllocVertexCount = (int)mesh.allocVerts.size;
			}
			else
			{
				Debug.Assert(mesh == null);
				this.m_Verts = default(NativeSlice<Vertex>);
				this.m_Indices = default(NativeSlice<ushort>);
				this.m_IndexOffset = 0;
				this.m_AllocVertexCount = 0;
			}
			this.m_Mesh = mesh;
			this.m_VertsFilled = 0;
			this.m_IndicesFilled = 0;
			this.m_AllocIndex = 0;
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000AAEB8 File Offset: 0x000A90B8
		private void ProcessNextAlloc()
		{
			List<EntryPreProcessor.AllocSize> list = (this.m_IsTail ? this.m_PreProcessor.tailAllocs : this.m_PreProcessor.headAllocs);
			Debug.Assert(this.m_AllocIndex < list.Count - 1);
			List<EntryPreProcessor.AllocSize> list2 = list;
			int num = this.m_AllocIndex + 1;
			this.m_AllocIndex = num;
			EntryPreProcessor.AllocSize allocSize = list2[num];
			this.m_Mesh = null;
			EntryProcessor.UpdateOrAllocate(ref this.m_Mesh, allocSize.vertexCount, allocSize.indexCount, this.m_RenderChain.device, out this.m_Verts, out this.m_Indices, out this.m_IndexOffset, this.m_RenderChain.statsByRef);
			this.m_AllocVertexCount = (int)this.m_Mesh.allocVerts.size;
			this.m_RenderChain.InsertExtraMesh(this.m_CurrentElement, this.m_Mesh);
			this.m_VertsFilled = 0;
			this.m_IndicesFilled = 0;
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x000AAF98 File Offset: 0x000A9198
		private static void UpdateOrAllocate(ref MeshHandle data, int vertexCount, int indexCount, UIRenderDevice device, out NativeSlice<Vertex> verts, out NativeSlice<ushort> indices, out ushort indexOffset, ref ChainBuilderStats stats)
		{
			bool flag = data != null;
			if (flag)
			{
				bool flag2 = (ulong)data.allocVerts.size >= (ulong)((long)vertexCount) && (ulong)data.allocIndices.size >= (ulong)((long)indexCount);
				if (flag2)
				{
					device.Update(data, (uint)vertexCount, (uint)indexCount, out verts, out indices, out indexOffset);
					stats.updatedMeshAllocations += 1U;
				}
				else
				{
					device.Free(data);
					data = device.Allocate((uint)vertexCount, (uint)indexCount, out verts, out indices, out indexOffset);
					stats.newMeshAllocations += 1U;
				}
			}
			else
			{
				data = device.Allocate((uint)vertexCount, (uint)indexCount, out verts, out indices, out indexOffset);
				stats.newMeshAllocations += 1U;
			}
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x000AB048 File Offset: 0x000A9248
		private static Material CreateBlitShader(float colorConversion)
		{
			bool flag = EntryProcessor.s_blitShader == null;
			if (flag)
			{
				EntryProcessor.s_blitShader = Shader.Find(Shaders.k_ColorConversionBlit);
			}
			Debug.Assert(EntryProcessor.s_blitShader != null, "UI Tollkit Render Event: Shader Not found");
			Material material = new Material(EntryProcessor.s_blitShader);
			material.hideFlags |= HideFlags.DontSaveInEditor;
			material.SetFloat("_ColorConversion", colorConversion);
			return material;
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000AB0B8 File Offset: 0x000A92B8
		private static Material GetBlitMaterial(VisualElement.RenderTargetMode mode)
		{
			Material material;
			switch (mode)
			{
			case VisualElement.RenderTargetMode.NoColorConversion:
			{
				bool flag = EntryProcessor.s_blitMaterial_NoChange == null;
				if (flag)
				{
					EntryProcessor.s_blitMaterial_NoChange = EntryProcessor.CreateBlitShader(0f);
				}
				material = EntryProcessor.s_blitMaterial_NoChange;
				break;
			}
			case VisualElement.RenderTargetMode.LinearToGamma:
			{
				bool flag2 = EntryProcessor.s_blitMaterial_LinearToGamma == null;
				if (flag2)
				{
					EntryProcessor.s_blitMaterial_LinearToGamma = EntryProcessor.CreateBlitShader(1f);
				}
				material = EntryProcessor.s_blitMaterial_LinearToGamma;
				break;
			}
			case VisualElement.RenderTargetMode.GammaToLinear:
			{
				bool flag3 = EntryProcessor.s_blitMaterial_GammaToLinear == null;
				if (flag3)
				{
					EntryProcessor.s_blitMaterial_GammaToLinear = EntryProcessor.CreateBlitShader(-1f);
				}
				material = EntryProcessor.s_blitMaterial_GammaToLinear;
				break;
			}
			default:
				Debug.LogError(string.Format("No Shader for Unsupported RenderTargetMode: {0}", mode));
				material = null;
				break;
			}
			return material;
		}

		// Token: 0x040015C5 RID: 5573
		private EntryPreProcessor m_PreProcessor = new EntryPreProcessor();

		// Token: 0x040015C6 RID: 5574
		private RenderChain m_RenderChain;

		// Token: 0x040015C7 RID: 5575
		private VisualElement m_CurrentElement;

		// Token: 0x040015C8 RID: 5576
		private int m_MaskDepth;

		// Token: 0x040015C9 RID: 5577
		private int m_MaskDepthPopped;

		// Token: 0x040015CA RID: 5578
		private int m_MaskDepthPushed;

		// Token: 0x040015CB RID: 5579
		private int m_StencilRef;

		// Token: 0x040015CC RID: 5580
		private int m_StencilRefPopped;

		// Token: 0x040015CD RID: 5581
		private int m_StencilRefPushed;

		// Token: 0x040015CE RID: 5582
		private BMPAlloc m_ClipRectId;

		// Token: 0x040015CF RID: 5583
		private BMPAlloc m_ClipRectIdPopped;

		// Token: 0x040015D0 RID: 5584
		private BMPAlloc m_ClipRectIdPushed;

		// Token: 0x040015D1 RID: 5585
		private bool m_IsDrawingMask;

		// Token: 0x040015D2 RID: 5586
		private Stack<EntryProcessor.MaskMesh> m_MaskMeshes = new Stack<EntryProcessor.MaskMesh>(1);

		// Token: 0x040015D3 RID: 5587
		private bool m_VertexDataComputed;

		// Token: 0x040015D4 RID: 5588
		private Matrix4x4 m_Transform;

		// Token: 0x040015D5 RID: 5589
		private Color32 m_TransformData;

		// Token: 0x040015D6 RID: 5590
		private Color32 m_OpacityData;

		// Token: 0x040015D7 RID: 5591
		private Color32 m_TextCoreSettingsPage;

		// Token: 0x040015D8 RID: 5592
		private MeshHandle m_Mesh;

		// Token: 0x040015D9 RID: 5593
		private NativeSlice<Vertex> m_Verts;

		// Token: 0x040015DA RID: 5594
		private NativeSlice<ushort> m_Indices;

		// Token: 0x040015DB RID: 5595
		private ushort m_IndexOffset;

		// Token: 0x040015DC RID: 5596
		private int m_AllocVertexCount;

		// Token: 0x040015DD RID: 5597
		private int m_AllocIndex;

		// Token: 0x040015DE RID: 5598
		private int m_VertsFilled;

		// Token: 0x040015DF RID: 5599
		private int m_IndicesFilled;

		// Token: 0x040015E0 RID: 5600
		private VertexFlags m_RenderType;

		// Token: 0x040015E1 RID: 5601
		private bool m_RemapUVs;

		// Token: 0x040015E2 RID: 5602
		private Rect m_AtlasRect;

		// Token: 0x040015E3 RID: 5603
		private int m_GradientSettingIndexOffset;

		// Token: 0x040015E4 RID: 5604
		private bool m_IsTail;

		// Token: 0x040015E5 RID: 5605
		private RenderChainCommand m_FirstCommand;

		// Token: 0x040015E6 RID: 5606
		private RenderChainCommand m_LastCommand;

		// Token: 0x040015EB RID: 5611
		private static Material s_blitMaterial_LinearToGamma;

		// Token: 0x040015EC RID: 5612
		private static Material s_blitMaterial_GammaToLinear;

		// Token: 0x040015ED RID: 5613
		private static Material s_blitMaterial_NoChange;

		// Token: 0x040015EE RID: 5614
		private static Shader s_blitShader;

		// Token: 0x0200063B RID: 1595
		private struct MaskMesh
		{
			// Token: 0x040015EF RID: 5615
			public NativeSlice<Vertex> vertices;

			// Token: 0x040015F0 RID: 5616
			public NativeSlice<ushort> indices;

			// Token: 0x040015F1 RID: 5617
			public int indexOffset;
		}
	}
}
