using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004A4 RID: 1188
	public struct RenderTargetIdentifier : IEquatable<RenderTargetIdentifier>
	{
		// Token: 0x06002DA2 RID: 11682 RVA: 0x00056725 File Offset: 0x00054925
		public RenderTargetIdentifier(BuiltinRenderTextureType type)
		{
			this.m_Type = type;
			this.m_NameID = -1;
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = 0;
			this.m_CubeFace = CubemapFace.Unknown;
			this.m_DepthSlice = 0;
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x0005675D File Offset: 0x0005495D
		public RenderTargetIdentifier(BuiltinRenderTextureType type, int mipLevel = 0, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			this.m_Type = type;
			this.m_NameID = -1;
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x00056796 File Offset: 0x00054996
		public RenderTargetIdentifier(string name)
		{
			this.m_Type = BuiltinRenderTextureType.PropertyName;
			this.m_NameID = Shader.PropertyToID(name);
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = 0;
			this.m_CubeFace = CubemapFace.Unknown;
			this.m_DepthSlice = 0;
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000567D4 File Offset: 0x000549D4
		public RenderTargetIdentifier(string name, int mipLevel = 0, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			this.m_Type = BuiltinRenderTextureType.PropertyName;
			this.m_NameID = Shader.PropertyToID(name);
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x00056813 File Offset: 0x00054A13
		public RenderTargetIdentifier(int nameID)
		{
			this.m_Type = BuiltinRenderTextureType.PropertyName;
			this.m_NameID = nameID;
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = 0;
			this.m_CubeFace = CubemapFace.Unknown;
			this.m_DepthSlice = 0;
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x0005684C File Offset: 0x00054A4C
		public RenderTargetIdentifier(int nameID, int mipLevel = 0, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			this.m_Type = BuiltinRenderTextureType.PropertyName;
			this.m_NameID = nameID;
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x00056888 File Offset: 0x00054A88
		public RenderTargetIdentifier(RenderTargetIdentifier renderTargetIdentifier, int mipLevel, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			this.m_Type = renderTargetIdentifier.m_Type;
			this.m_NameID = renderTargetIdentifier.m_NameID;
			this.m_InstanceID = renderTargetIdentifier.m_InstanceID;
			this.m_BufferPointer = renderTargetIdentifier.m_BufferPointer;
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000568DC File Offset: 0x00054ADC
		public RenderTargetIdentifier(Texture tex)
		{
			bool flag = tex == null;
			if (flag)
			{
				this.m_Type = BuiltinRenderTextureType.None;
			}
			else
			{
				bool flag2 = tex is RenderTexture;
				if (flag2)
				{
					this.m_Type = BuiltinRenderTextureType.RenderTexture;
				}
				else
				{
					this.m_Type = BuiltinRenderTextureType.BindableTexture;
				}
			}
			this.m_BufferPointer = IntPtr.Zero;
			this.m_NameID = -1;
			this.m_InstanceID = (tex ? tex.GetInstanceID() : 0);
			this.m_MipLevel = 0;
			this.m_CubeFace = CubemapFace.Unknown;
			this.m_DepthSlice = 0;
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x00056960 File Offset: 0x00054B60
		public RenderTargetIdentifier(Texture tex, int mipLevel = 0, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			bool flag = tex == null;
			if (flag)
			{
				this.m_Type = BuiltinRenderTextureType.None;
			}
			else
			{
				bool flag2 = tex is RenderTexture;
				if (flag2)
				{
					this.m_Type = BuiltinRenderTextureType.RenderTexture;
				}
				else
				{
					this.m_Type = BuiltinRenderTextureType.BindableTexture;
				}
			}
			this.m_BufferPointer = IntPtr.Zero;
			this.m_NameID = -1;
			this.m_InstanceID = (tex ? tex.GetInstanceID() : 0);
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000569E5 File Offset: 0x00054BE5
		public RenderTargetIdentifier(RenderBuffer buf, int mipLevel = 0, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			this.m_Type = BuiltinRenderTextureType.BufferPtr;
			this.m_NameID = -1;
			this.m_InstanceID = buf.m_RenderTextureInstanceID;
			this.m_BufferPointer = buf.m_BufferPtr;
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x00056A28 File Offset: 0x00054C28
		public static implicit operator RenderTargetIdentifier(BuiltinRenderTextureType type)
		{
			return new RenderTargetIdentifier(type);
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x00056A40 File Offset: 0x00054C40
		public static implicit operator RenderTargetIdentifier(string name)
		{
			return new RenderTargetIdentifier(name);
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x00056A58 File Offset: 0x00054C58
		public static implicit operator RenderTargetIdentifier(int nameID)
		{
			return new RenderTargetIdentifier(nameID);
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x00056A70 File Offset: 0x00054C70
		public static implicit operator RenderTargetIdentifier(Texture tex)
		{
			return new RenderTargetIdentifier(tex);
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x00056A88 File Offset: 0x00054C88
		public static implicit operator RenderTargetIdentifier(RenderBuffer buf)
		{
			return new RenderTargetIdentifier(buf, 0, CubemapFace.Unknown, 0);
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x00056AA4 File Offset: 0x00054CA4
		public override string ToString()
		{
			return UnityString.Format("Type {0} NameID {1} InstanceID {2} BufferPointer {3} MipLevel {4} CubeFace {5} DepthSlice {6}", new object[] { this.m_Type, this.m_NameID, this.m_InstanceID, this.m_BufferPointer, this.m_MipLevel, this.m_CubeFace, this.m_DepthSlice });
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x00056B28 File Offset: 0x00054D28
		public override int GetHashCode()
		{
			return (this.m_Type.GetHashCode() * 23 + this.m_NameID.GetHashCode()) * 23 + this.m_InstanceID.GetHashCode();
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x00056B6C File Offset: 0x00054D6C
		public bool Equals(RenderTargetIdentifier rhs)
		{
			return this.m_Type == rhs.m_Type && this.m_NameID == rhs.m_NameID && this.m_InstanceID == rhs.m_InstanceID && this.m_BufferPointer == rhs.m_BufferPointer && this.m_MipLevel == rhs.m_MipLevel && this.m_CubeFace == rhs.m_CubeFace && this.m_DepthSlice == rhs.m_DepthSlice;
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x00056BE8 File Offset: 0x00054DE8
		public override bool Equals(object obj)
		{
			bool flag = !(obj is RenderTargetIdentifier);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				RenderTargetIdentifier renderTargetIdentifier = (RenderTargetIdentifier)obj;
				flag2 = this.Equals(renderTargetIdentifier);
			}
			return flag2;
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x00056C1C File Offset: 0x00054E1C
		public static bool operator ==(RenderTargetIdentifier lhs, RenderTargetIdentifier rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x00056C38 File Offset: 0x00054E38
		public static bool operator !=(RenderTargetIdentifier lhs, RenderTargetIdentifier rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000DD3 RID: 3539
		public const int AllDepthSlices = -1;

		// Token: 0x04000DD4 RID: 3540
		private BuiltinRenderTextureType m_Type;

		// Token: 0x04000DD5 RID: 3541
		private int m_NameID;

		// Token: 0x04000DD6 RID: 3542
		private int m_InstanceID;

		// Token: 0x04000DD7 RID: 3543
		private IntPtr m_BufferPointer;

		// Token: 0x04000DD8 RID: 3544
		private int m_MipLevel;

		// Token: 0x04000DD9 RID: 3545
		private CubemapFace m_CubeFace;

		// Token: 0x04000DDA RID: 3546
		private int m_DepthSlice;
	}
}
