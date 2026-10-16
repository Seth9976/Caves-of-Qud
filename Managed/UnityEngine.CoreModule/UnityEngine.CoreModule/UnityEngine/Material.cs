using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200018B RID: 395
	[NativeHeader("Runtime/Shaders/Material.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	public class Material : Object
	{
		// Token: 0x0600120C RID: 4620 RVA: 0x0001F174 File Offset: 0x0001D374
		[Obsolete("Creating materials from shader source string will be removed in the future. Use Shader assets instead.", false)]
		public static Material Create(string scriptContents)
		{
			return new Material(scriptContents);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0001F18C File Offset: 0x0001D38C
		[FreeFunction("MaterialScripting::CreateWithShader")]
		private static void CreateWithShader([Writable] Material self, [NotNull] Shader shader)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			Material.CreateWithShader_Injected(self, intPtr);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0001F1C4 File Offset: 0x0001D3C4
		[FreeFunction("MaterialScripting::CreateWithMaterial")]
		private static void CreateWithMaterial([Writable] Material self, [NotNull] Material source)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(source);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			Material.CreateWithMaterial_Injected(self, intPtr);
		}

		// Token: 0x0600120F RID: 4623
		[FreeFunction("MaterialScripting::CreateWithString")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateWithString([Writable] Material self);

		// Token: 0x06001210 RID: 4624 RVA: 0x0001F1FB File Offset: 0x0001D3FB
		public Material(Shader shader)
		{
			Material.CreateWithShader(this, shader);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0001F20D File Offset: 0x0001D40D
		[RequiredByNativeCode]
		public Material(Material source)
		{
			Material.CreateWithMaterial(this, source);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0001F21F File Offset: 0x0001D41F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Creating materials from shader source string is no longer supported. Use Shader assets instead.", false)]
		public Material(string contents)
		{
			Material.CreateWithString(this);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0001F230 File Offset: 0x0001D430
		internal static Material GetDefaultMaterial()
		{
			return Unmarshal.UnmarshalUnityObject<Material>(Material.GetDefaultMaterial_Injected());
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0001F248 File Offset: 0x0001D448
		internal static Material GetDefaultParticleMaterial()
		{
			return Unmarshal.UnmarshalUnityObject<Material>(Material.GetDefaultParticleMaterial_Injected());
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0001F260 File Offset: 0x0001D460
		internal static Material GetDefaultLineMaterial()
		{
			return Unmarshal.UnmarshalUnityObject<Material>(Material.GetDefaultLineMaterial_Injected());
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x0001F278 File Offset: 0x0001D478
		// (set) Token: 0x06001217 RID: 4631 RVA: 0x0001F2A0 File Offset: 0x0001D4A0
		public Shader shader
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Shader>(Material.get_shader_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Material.set_shader_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Shader>(value));
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x0001F2C8 File Offset: 0x0001D4C8
		// (set) Token: 0x06001219 RID: 4633 RVA: 0x0001F308 File Offset: 0x0001D508
		public Color color
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainColor);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Color color;
				if (flag)
				{
					color = this.GetColor(firstPropertyNameIdByAttribute);
				}
				else
				{
					color = this.GetColor(Material.k_ColorId);
				}
				return color;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainColor);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetColor(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetColor(Material.k_ColorId, value);
				}
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x0001F348 File Offset: 0x0001D548
		// (set) Token: 0x0600121B RID: 4635 RVA: 0x0001F388 File Offset: 0x0001D588
		public Texture mainTexture
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Texture texture;
				if (flag)
				{
					texture = this.GetTexture(firstPropertyNameIdByAttribute);
				}
				else
				{
					texture = this.GetTexture(Material.k_MainTexId);
				}
				return texture;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetTexture(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetTexture(Material.k_MainTexId, value);
				}
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x0001F3C8 File Offset: 0x0001D5C8
		// (set) Token: 0x0600121D RID: 4637 RVA: 0x0001F408 File Offset: 0x0001D608
		public Vector2 mainTextureOffset
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Vector2 vector;
				if (flag)
				{
					vector = this.GetTextureOffset(firstPropertyNameIdByAttribute);
				}
				else
				{
					vector = this.GetTextureOffset(Material.k_MainTexId);
				}
				return vector;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetTextureOffset(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetTextureOffset(Material.k_MainTexId, value);
				}
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x0001F448 File Offset: 0x0001D648
		// (set) Token: 0x0600121F RID: 4639 RVA: 0x0001F488 File Offset: 0x0001D688
		public Vector2 mainTextureScale
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Vector2 vector;
				if (flag)
				{
					vector = this.GetTextureScale(firstPropertyNameIdByAttribute);
				}
				else
				{
					vector = this.GetTextureScale(Material.k_MainTexId);
				}
				return vector;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetTextureScale(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetTextureScale(Material.k_MainTexId, value);
				}
			}
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0001F4C8 File Offset: 0x0001D6C8
		[NativeName("GetFirstPropertyNameIdByAttributeFromScript")]
		private int GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags attributeFlag)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetFirstPropertyNameIdByAttribute_Injected(intPtr, attributeFlag);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0001F4EC File Offset: 0x0001D6EC
		[NativeName("HasPropertyFromScript")]
		public bool HasProperty(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.HasProperty_Injected(intPtr, nameID);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0001F510 File Offset: 0x0001D710
		public bool HasProperty(string name)
		{
			return this.HasProperty(Shader.PropertyToID(name));
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0001F530 File Offset: 0x0001D730
		[NativeName("HasFloatFromScript")]
		private bool HasFloatImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.HasFloatImpl_Injected(intPtr, name);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0001F554 File Offset: 0x0001D754
		public bool HasFloat(string name)
		{
			return this.HasFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0001F574 File Offset: 0x0001D774
		public bool HasFloat(int nameID)
		{
			return this.HasFloatImpl(nameID);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0001F590 File Offset: 0x0001D790
		public bool HasInt(string name)
		{
			return this.HasFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0001F5B0 File Offset: 0x0001D7B0
		public bool HasInt(int nameID)
		{
			return this.HasFloatImpl(nameID);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0001F5CC File Offset: 0x0001D7CC
		[NativeName("HasIntegerFromScript")]
		private bool HasIntImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.HasIntImpl_Injected(intPtr, name);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0001F5F0 File Offset: 0x0001D7F0
		public bool HasInteger(string name)
		{
			return this.HasIntImpl(Shader.PropertyToID(name));
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0001F610 File Offset: 0x0001D810
		public bool HasInteger(int nameID)
		{
			return this.HasIntImpl(nameID);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0001F62C File Offset: 0x0001D82C
		[NativeName("HasTextureFromScript")]
		private bool HasTextureImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.HasTextureImpl_Injected(intPtr, name);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0001F650 File Offset: 0x0001D850
		public bool HasTexture(string name)
		{
			return this.HasTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0001F670 File Offset: 0x0001D870
		public bool HasTexture(int nameID)
		{
			return this.HasTextureImpl(nameID);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0001F68C File Offset: 0x0001D88C
		[NativeName("HasMatrixFromScript")]
		private bool HasMatrixImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.HasMatrixImpl_Injected(intPtr, name);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0001F6B0 File Offset: 0x0001D8B0
		public bool HasMatrix(string name)
		{
			return this.HasMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0001F6D0 File Offset: 0x0001D8D0
		public bool HasMatrix(int nameID)
		{
			return this.HasMatrixImpl(nameID);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0001F6EC File Offset: 0x0001D8EC
		[NativeName("HasVectorFromScript")]
		private bool HasVectorImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.HasVectorImpl_Injected(intPtr, name);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0001F710 File Offset: 0x0001D910
		public bool HasVector(string name)
		{
			return this.HasVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0001F730 File Offset: 0x0001D930
		public bool HasVector(int nameID)
		{
			return this.HasVectorImpl(nameID);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0001F74C File Offset: 0x0001D94C
		public bool HasColor(string name)
		{
			return this.HasVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0001F76C File Offset: 0x0001D96C
		public bool HasColor(int nameID)
		{
			return this.HasVectorImpl(nameID);
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0001F788 File Offset: 0x0001D988
		[NativeName("HasBufferFromScript")]
		private bool HasBufferImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.HasBufferImpl_Injected(intPtr, name);
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0001F7AC File Offset: 0x0001D9AC
		public bool HasBuffer(string name)
		{
			return this.HasBufferImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0001F7CC File Offset: 0x0001D9CC
		public bool HasBuffer(int nameID)
		{
			return this.HasBufferImpl(nameID);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0001F7E8 File Offset: 0x0001D9E8
		[NativeName("HasConstantBufferFromScript")]
		private bool HasConstantBufferImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.HasConstantBufferImpl_Injected(intPtr, name);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0001F80C File Offset: 0x0001DA0C
		public bool HasConstantBuffer(string name)
		{
			return this.HasConstantBufferImpl(Shader.PropertyToID(name));
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0001F82C File Offset: 0x0001DA2C
		public bool HasConstantBuffer(int nameID)
		{
			return this.HasConstantBufferImpl(nameID);
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x0001F848 File Offset: 0x0001DA48
		// (set) Token: 0x0600123D RID: 4669 RVA: 0x0001F86C File Offset: 0x0001DA6C
		public int renderQueue
		{
			[NativeName("GetActualRenderQueue")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Material.get_renderQueue_Injected(intPtr);
			}
			[NativeName("SetCustomRenderQueue")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Material.set_renderQueue_Injected(intPtr, value);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x0001F890 File Offset: 0x0001DA90
		public int rawRenderQueue
		{
			[NativeName("GetCustomRenderQueue")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Material.get_rawRenderQueue_Injected(intPtr);
			}
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0001F8B4 File Offset: 0x0001DAB4
		public unsafe void EnableKeyword(string keyword)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Material.EnableKeyword_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0001F918 File Offset: 0x0001DB18
		public unsafe void DisableKeyword(string keyword)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Material.DisableKeyword_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0001F97C File Offset: 0x0001DB7C
		public unsafe bool IsKeywordEnabled(string keyword)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = Material.IsKeywordEnabled_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0001F9E4 File Offset: 0x0001DBE4
		[FreeFunction("MaterialScripting::EnableKeyword", HasExplicitThis = true)]
		private void EnableLocalKeyword(LocalKeyword keyword)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.EnableLocalKeyword_Injected(intPtr, ref keyword);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0001FA08 File Offset: 0x0001DC08
		[FreeFunction("MaterialScripting::DisableKeyword", HasExplicitThis = true)]
		private void DisableLocalKeyword(LocalKeyword keyword)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.DisableLocalKeyword_Injected(intPtr, ref keyword);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0001FA2C File Offset: 0x0001DC2C
		[FreeFunction("MaterialScripting::SetKeyword", HasExplicitThis = true)]
		private void SetLocalKeyword(LocalKeyword keyword, bool value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetLocalKeyword_Injected(intPtr, ref keyword, value);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0001FA54 File Offset: 0x0001DC54
		[FreeFunction("MaterialScripting::IsKeywordEnabled", HasExplicitThis = true)]
		private bool IsLocalKeywordEnabled(LocalKeyword keyword)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.IsLocalKeywordEnabled_Injected(intPtr, ref keyword);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0001FA78 File Offset: 0x0001DC78
		public void EnableKeyword(in LocalKeyword keyword)
		{
			this.EnableLocalKeyword(keyword);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0001FA88 File Offset: 0x0001DC88
		public void DisableKeyword(in LocalKeyword keyword)
		{
			this.DisableLocalKeyword(keyword);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0001FA98 File Offset: 0x0001DC98
		public void SetKeyword(in LocalKeyword keyword, bool value)
		{
			this.SetLocalKeyword(keyword, value);
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0001FAAC File Offset: 0x0001DCAC
		public bool IsKeywordEnabled(in LocalKeyword keyword)
		{
			return this.IsLocalKeywordEnabled(keyword);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0001FACC File Offset: 0x0001DCCC
		[FreeFunction("MaterialScripting::GetEnabledKeywords", HasExplicitThis = true)]
		private LocalKeyword[] GetEnabledKeywords()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetEnabledKeywords_Injected(intPtr);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0001FAF0 File Offset: 0x0001DCF0
		[FreeFunction("MaterialScripting::SetEnabledKeywords", HasExplicitThis = true)]
		private void SetEnabledKeywords(LocalKeyword[] keywords)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetEnabledKeywords_Injected(intPtr, keywords);
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0001FB14 File Offset: 0x0001DD14
		// (set) Token: 0x0600124D RID: 4685 RVA: 0x0001FB2C File Offset: 0x0001DD2C
		public LocalKeyword[] enabledKeywords
		{
			get
			{
				return this.GetEnabledKeywords();
			}
			set
			{
				this.SetEnabledKeywords(value);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x0001FB38 File Offset: 0x0001DD38
		// (set) Token: 0x0600124F RID: 4687 RVA: 0x0001FB5C File Offset: 0x0001DD5C
		public MaterialGlobalIlluminationFlags globalIlluminationFlags
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Material.get_globalIlluminationFlags_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Material.set_globalIlluminationFlags_Injected(intPtr, value);
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x0001FB80 File Offset: 0x0001DD80
		// (set) Token: 0x06001251 RID: 4689 RVA: 0x0001FBA4 File Offset: 0x0001DDA4
		public bool doubleSidedGI
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Material.get_doubleSidedGI_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Material.set_doubleSidedGI_Injected(intPtr, value);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x0001FBC8 File Offset: 0x0001DDC8
		// (set) Token: 0x06001253 RID: 4691 RVA: 0x0001FBEC File Offset: 0x0001DDEC
		[NativeProperty("EnableInstancingVariants")]
		public bool enableInstancing
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Material.get_enableInstancing_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Material.set_enableInstancing_Injected(intPtr, value);
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x0001FC10 File Offset: 0x0001DE10
		public int passCount
		{
			[NativeName("GetShader()->GetPassCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Material.get_passCount_Injected(intPtr);
			}
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0001FC34 File Offset: 0x0001DE34
		[FreeFunction("MaterialScripting::SetShaderPassEnabled", HasExplicitThis = true)]
		public unsafe void SetShaderPassEnabled(string passName, bool enabled)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(passName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = passName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Material.SetShaderPassEnabled_Injected(intPtr, ref managedSpanWrapper, enabled);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0001FC9C File Offset: 0x0001DE9C
		[FreeFunction("MaterialScripting::GetShaderPassEnabled", HasExplicitThis = true)]
		public unsafe bool GetShaderPassEnabled(string passName)
		{
			bool shaderPassEnabled_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(passName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = passName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				shaderPassEnabled_Injected = Material.GetShaderPassEnabled_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return shaderPassEnabled_Injected;
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0001FD04 File Offset: 0x0001DF04
		public string GetPassName(int pass)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				Material.GetPassName_Injected(intPtr, pass, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0001FD44 File Offset: 0x0001DF44
		public unsafe int FindPass(string passName)
		{
			int num;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(passName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = passName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = Material.FindPass_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0001FDAC File Offset: 0x0001DFAC
		public unsafe void SetOverrideTag(string tag, string val)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(val, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = val.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				Material.SetOverrideTag_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0001FE44 File Offset: 0x0001E044
		[NativeName("GetTag")]
		private unsafe string GetTagImpl(string tag, bool currentSubShaderOnly, string defaultValue)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(defaultValue, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = defaultValue.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper3;
				Material.GetTagImpl_Injected(intPtr, ref managedSpanWrapper, currentSubShaderOnly, ref managedSpanWrapper2, out managedSpanWrapper3);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
				ManagedSpanWrapper managedSpanWrapper3;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper3);
			}
			return stringAndDispose;
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0001FEE8 File Offset: 0x0001E0E8
		public string GetTag(string tag, bool searchFallbacks, string defaultValue)
		{
			return this.GetTagImpl(tag, !searchFallbacks, defaultValue);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0001FF08 File Offset: 0x0001E108
		public string GetTag(string tag, bool searchFallbacks)
		{
			return this.GetTagImpl(tag, !searchFallbacks, "");
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0001FF2C File Offset: 0x0001E12C
		[FreeFunction("MaterialScripting::Lerp", HasExplicitThis = true)]
		[NativeThrows]
		public void Lerp(Material start, Material end, float t)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.Lerp_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(start), Object.MarshalledUnityObject.Marshal<Material>(end), t);
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0001FF5C File Offset: 0x0001E15C
		[FreeFunction("MaterialScripting::SetPass", HasExplicitThis = true)]
		public bool SetPass(int pass)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.SetPass_Injected(intPtr, pass);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0001FF80 File Offset: 0x0001E180
		[FreeFunction("MaterialScripting::CopyPropertiesFrom", HasExplicitThis = true)]
		public void CopyPropertiesFromMaterial(Material mat)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.CopyPropertiesFromMaterial_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(mat));
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0001FFA8 File Offset: 0x0001E1A8
		[FreeFunction("MaterialScripting::CopyMatchingPropertiesFrom", HasExplicitThis = true)]
		public void CopyMatchingPropertiesFromMaterial(Material mat)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.CopyMatchingPropertiesFromMaterial_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(mat));
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0001FFD0 File Offset: 0x0001E1D0
		[FreeFunction("MaterialScripting::GetShaderKeywords", HasExplicitThis = true)]
		private string[] GetShaderKeywords()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetShaderKeywords_Injected(intPtr);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0001FFF4 File Offset: 0x0001E1F4
		[FreeFunction("MaterialScripting::SetShaderKeywords", HasExplicitThis = true)]
		private void SetShaderKeywords(string[] names)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetShaderKeywords_Injected(intPtr, names);
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x00020018 File Offset: 0x0001E218
		// (set) Token: 0x06001264 RID: 4708 RVA: 0x00020030 File Offset: 0x0001E230
		public string[] shaderKeywords
		{
			get
			{
				return this.GetShaderKeywords();
			}
			set
			{
				this.SetShaderKeywords(value);
			}
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0002003C File Offset: 0x0001E23C
		[FreeFunction("MaterialScripting::GetPropertyNames", HasExplicitThis = true)]
		private string[] GetPropertyNamesImpl(int propertyType)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetPropertyNamesImpl_Injected(intPtr, propertyType);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00020060 File Offset: 0x0001E260
		public int ComputeCRC()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.ComputeCRC_Injected(intPtr);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00020084 File Offset: 0x0001E284
		[FreeFunction("MaterialScripting::GetTexturePropertyNames", HasExplicitThis = true)]
		public string[] GetTexturePropertyNames()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetTexturePropertyNames_Injected(intPtr);
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x000200A8 File Offset: 0x0001E2A8
		[FreeFunction("MaterialScripting::GetTexturePropertyNameIDs", HasExplicitThis = true)]
		public int[] GetTexturePropertyNameIDs()
		{
			int[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Material.GetTexturePropertyNameIDs_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				int[] array;
				blittableArrayWrapper.Unmarshal<int>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x000200EC File Offset: 0x0001E2EC
		[FreeFunction("MaterialScripting::GetTexturePropertyNamesInternal", HasExplicitThis = true)]
		private void GetTexturePropertyNamesInternal(object outNames)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.GetTexturePropertyNamesInternal_Injected(intPtr, outNames);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00020110 File Offset: 0x0001E310
		[FreeFunction("MaterialScripting::GetTexturePropertyNameIDsInternal", HasExplicitThis = true)]
		private void GetTexturePropertyNameIDsInternal(object outNames)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.GetTexturePropertyNameIDsInternal_Injected(intPtr, outNames);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00020134 File Offset: 0x0001E334
		public void GetTexturePropertyNames(List<string> outNames)
		{
			bool flag = outNames == null;
			if (flag)
			{
				throw new ArgumentNullException("outNames");
			}
			this.GetTexturePropertyNamesInternal(outNames);
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00020160 File Offset: 0x0001E360
		public void GetTexturePropertyNameIDs(List<int> outNames)
		{
			bool flag = outNames == null;
			if (flag)
			{
				throw new ArgumentNullException("outNames");
			}
			this.GetTexturePropertyNameIDsInternal(outNames);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0002018C File Offset: 0x0001E38C
		[NativeName("SetIntFromScript")]
		private void SetIntImpl(int name, int value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetIntImpl_Injected(intPtr, name, value);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x000201B0 File Offset: 0x0001E3B0
		[NativeName("SetFloatFromScript")]
		private void SetFloatImpl(int name, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetFloatImpl_Injected(intPtr, name, value);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x000201D4 File Offset: 0x0001E3D4
		[NativeName("SetColorFromScript")]
		private void SetColorImpl(int name, Color value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetColorImpl_Injected(intPtr, name, ref value);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x000201FC File Offset: 0x0001E3FC
		[NativeName("SetMatrixFromScript")]
		private void SetMatrixImpl(int name, Matrix4x4 value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetMatrixImpl_Injected(intPtr, name, ref value);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00020224 File Offset: 0x0001E424
		[NativeName("SetTextureFromScript")]
		private void SetTextureImpl(int name, Texture value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetTextureImpl_Injected(intPtr, name, Object.MarshalledUnityObject.Marshal<Texture>(value));
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00020250 File Offset: 0x0001E450
		[NativeName("SetRenderTextureFromScript")]
		private void SetRenderTextureImpl(int name, RenderTexture value, RenderTextureSubElement element)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetRenderTextureImpl_Injected(intPtr, name, Object.MarshalledUnityObject.Marshal<RenderTexture>(value), element);
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0002027C File Offset: 0x0001E47C
		[NativeName("SetBufferFromScript")]
		private void SetBufferImpl(int name, ComputeBuffer value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetBufferImpl_Injected(intPtr, name, (value == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(value));
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000202B0 File Offset: 0x0001E4B0
		[NativeName("SetBufferFromScript")]
		private void SetGraphicsBufferImpl(int name, GraphicsBuffer value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetGraphicsBufferImpl_Injected(intPtr, name, (value == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(value));
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000202E4 File Offset: 0x0001E4E4
		[NativeName("SetConstantBufferFromScript")]
		private void SetConstantBufferImpl(int name, ComputeBuffer value, int offset, int size)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetConstantBufferImpl_Injected(intPtr, name, (value == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(value), offset, size);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0002031C File Offset: 0x0001E51C
		[NativeName("SetConstantBufferFromScript")]
		private void SetConstantGraphicsBufferImpl(int name, GraphicsBuffer value, int offset, int size)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetConstantGraphicsBufferImpl_Injected(intPtr, name, (value == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(value), offset, size);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00020354 File Offset: 0x0001E554
		[NativeName("GetIntFromScript")]
		private int GetIntImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetIntImpl_Injected(intPtr, name);
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00020378 File Offset: 0x0001E578
		[NativeName("GetFloatFromScript")]
		private float GetFloatImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetFloatImpl_Injected(intPtr, name);
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0002039C File Offset: 0x0001E59C
		[NativeName("GetColorFromScript")]
		private Color GetColorImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Color color;
			Material.GetColorImpl_Injected(intPtr, name, out color);
			return color;
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x000203C4 File Offset: 0x0001E5C4
		[NativeName("GetMatrixFromScript")]
		private Matrix4x4 GetMatrixImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Matrix4x4 matrix4x;
			Material.GetMatrixImpl_Injected(intPtr, name, out matrix4x);
			return matrix4x;
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x000203EC File Offset: 0x0001E5EC
		[NativeName("GetTextureFromScript")]
		private Texture GetTextureImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Texture>(Material.GetTextureImpl_Injected(intPtr, name));
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00020414 File Offset: 0x0001E614
		[NativeName("GetBufferFromScript")]
		private GraphicsBufferHandle GetBufferImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsBufferHandle graphicsBufferHandle;
			Material.GetBufferImpl_Injected(intPtr, name, out graphicsBufferHandle);
			return graphicsBufferHandle;
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0002043C File Offset: 0x0001E63C
		[NativeName("GetConstantBufferFromScript")]
		private GraphicsBufferHandle GetConstantBufferImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsBufferHandle graphicsBufferHandle;
			Material.GetConstantBufferImpl_Injected(intPtr, name, out graphicsBufferHandle);
			return graphicsBufferHandle;
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00020464 File Offset: 0x0001E664
		[FreeFunction(Name = "MaterialScripting::SetFloatArray", HasExplicitThis = true)]
		private unsafe void SetFloatArrayImpl(int name, float[] values, int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<float> span = new Span<float>(values);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Material.SetFloatArrayImpl_Injected(intPtr, name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x000204B0 File Offset: 0x0001E6B0
		[FreeFunction(Name = "MaterialScripting::SetVectorArray", HasExplicitThis = true)]
		private unsafe void SetVectorArrayImpl(int name, Vector4[] values, int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector4> span = new Span<Vector4>(values);
			fixed (Vector4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Material.SetVectorArrayImpl_Injected(intPtr, name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x000204FC File Offset: 0x0001E6FC
		[FreeFunction(Name = "MaterialScripting::SetColorArray", HasExplicitThis = true)]
		private unsafe void SetColorArrayImpl(int name, Color[] values, int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color> span = new Span<Color>(values);
			fixed (Color* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Material.SetColorArrayImpl_Injected(intPtr, name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00020548 File Offset: 0x0001E748
		[FreeFunction(Name = "MaterialScripting::SetMatrixArray", HasExplicitThis = true)]
		private unsafe void SetMatrixArrayImpl(int name, Matrix4x4[] values, int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(values);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Material.SetMatrixArrayImpl_Injected(intPtr, name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00020594 File Offset: 0x0001E794
		[FreeFunction(Name = "MaterialScripting::GetFloatArray", HasExplicitThis = true)]
		private float[] GetFloatArrayImpl(int name)
		{
			float[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Material.GetFloatArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				float[] array;
				blittableArrayWrapper.Unmarshal<float>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x000205D8 File Offset: 0x0001E7D8
		[FreeFunction(Name = "MaterialScripting::GetVectorArray", HasExplicitThis = true)]
		private Vector4[] GetVectorArrayImpl(int name)
		{
			Vector4[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Material.GetVectorArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Vector4[] array;
				blittableArrayWrapper.Unmarshal<Vector4>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0002061C File Offset: 0x0001E81C
		[FreeFunction(Name = "MaterialScripting::GetColorArray", HasExplicitThis = true)]
		private Color[] GetColorArrayImpl(int name)
		{
			Color[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Material.GetColorArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Color[] array;
				blittableArrayWrapper.Unmarshal<Color>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00020660 File Offset: 0x0001E860
		[FreeFunction(Name = "MaterialScripting::GetMatrixArray", HasExplicitThis = true)]
		private Matrix4x4[] GetMatrixArrayImpl(int name)
		{
			Matrix4x4[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Material.GetMatrixArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Matrix4x4[] array;
				blittableArrayWrapper.Unmarshal<Matrix4x4>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x000206A4 File Offset: 0x0001E8A4
		[FreeFunction(Name = "MaterialScripting::GetFloatArrayCount", HasExplicitThis = true)]
		private int GetFloatArrayCountImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetFloatArrayCountImpl_Injected(intPtr, name);
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x000206C8 File Offset: 0x0001E8C8
		[FreeFunction(Name = "MaterialScripting::GetVectorArrayCount", HasExplicitThis = true)]
		private int GetVectorArrayCountImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetVectorArrayCountImpl_Injected(intPtr, name);
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000206EC File Offset: 0x0001E8EC
		[FreeFunction(Name = "MaterialScripting::GetColorArrayCount", HasExplicitThis = true)]
		private int GetColorArrayCountImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetColorArrayCountImpl_Injected(intPtr, name);
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00020710 File Offset: 0x0001E910
		[FreeFunction(Name = "MaterialScripting::GetMatrixArrayCount", HasExplicitThis = true)]
		private int GetMatrixArrayCountImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetMatrixArrayCountImpl_Injected(intPtr, name);
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00020734 File Offset: 0x0001E934
		[FreeFunction(Name = "MaterialScripting::ExtractFloatArray", HasExplicitThis = true)]
		private unsafe void ExtractFloatArrayImpl(int name, [Out] float[] val)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (val != null)
				{
					fixed (float[] array = val)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				Material.ExtractFloatArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				float[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<float>(ref array);
			}
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00020794 File Offset: 0x0001E994
		[FreeFunction(Name = "MaterialScripting::ExtractVectorArray", HasExplicitThis = true)]
		private unsafe void ExtractVectorArrayImpl(int name, [Out] Vector4[] val)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (val != null)
				{
					fixed (Vector4[] array = val)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				Material.ExtractVectorArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				Vector4[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Vector4>(ref array);
			}
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x000207F4 File Offset: 0x0001E9F4
		[FreeFunction(Name = "MaterialScripting::ExtractColorArray", HasExplicitThis = true)]
		private unsafe void ExtractColorArrayImpl(int name, [Out] Color[] val)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (val != null)
				{
					fixed (Color[] array = val)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				Material.ExtractColorArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				Color[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Color>(ref array);
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00020854 File Offset: 0x0001EA54
		[FreeFunction(Name = "MaterialScripting::ExtractMatrixArray", HasExplicitThis = true)]
		private unsafe void ExtractMatrixArrayImpl(int name, [Out] Matrix4x4[] val)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (val != null)
				{
					fixed (Matrix4x4[] array = val)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				Material.ExtractMatrixArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				Matrix4x4[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Matrix4x4>(ref array);
			}
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x000208B4 File Offset: 0x0001EAB4
		[NativeName("GetTextureScaleAndOffsetFromScript")]
		private Vector4 GetTextureScaleAndOffsetImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector4 vector;
			Material.GetTextureScaleAndOffsetImpl_Injected(intPtr, name, out vector);
			return vector;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x000208DC File Offset: 0x0001EADC
		[NativeName("SetTextureOffsetFromScript")]
		private void SetTextureOffsetImpl(int name, Vector2 offset)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetTextureOffsetImpl_Injected(intPtr, name, ref offset);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00020904 File Offset: 0x0001EB04
		[NativeName("SetTextureScaleFromScript")]
		private void SetTextureScaleImpl(int name, Vector2 scale)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetTextureScaleImpl_Injected(intPtr, name, ref scale);
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0002092C File Offset: 0x0001EB2C
		private void SetFloatArray(int name, float[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetFloatArrayImpl(name, values, count);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00020980 File Offset: 0x0001EB80
		private void SetVectorArray(int name, Vector4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetVectorArrayImpl(name, values, count);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x000209D4 File Offset: 0x0001EBD4
		private void SetColorArray(int name, Color[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetColorArrayImpl(name, values, count);
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00020A28 File Offset: 0x0001EC28
		private void SetMatrixArray(int name, Matrix4x4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetMatrixArrayImpl(name, values, count);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00020A7C File Offset: 0x0001EC7C
		private void ExtractFloatArray(int name, List<float> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int floatArrayCountImpl = this.GetFloatArrayCountImpl(name);
			bool flag2 = floatArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<float>(values, floatArrayCountImpl);
				this.ExtractFloatArrayImpl(name, NoAllocHelpers.ExtractArrayFromList<float>(values));
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00020ACC File Offset: 0x0001ECCC
		private void ExtractVectorArray(int name, List<Vector4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int vectorArrayCountImpl = this.GetVectorArrayCountImpl(name);
			bool flag2 = vectorArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Vector4>(values, vectorArrayCountImpl);
				this.ExtractVectorArrayImpl(name, NoAllocHelpers.ExtractArrayFromList<Vector4>(values));
			}
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00020B1C File Offset: 0x0001ED1C
		private void ExtractColorArray(int name, List<Color> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int colorArrayCountImpl = this.GetColorArrayCountImpl(name);
			bool flag2 = colorArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Color>(values, colorArrayCountImpl);
				this.ExtractColorArrayImpl(name, NoAllocHelpers.ExtractArrayFromList<Color>(values));
			}
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00020B6C File Offset: 0x0001ED6C
		private void ExtractMatrixArray(int name, List<Matrix4x4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int matrixArrayCountImpl = this.GetMatrixArrayCountImpl(name);
			bool flag2 = matrixArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Matrix4x4>(values, matrixArrayCountImpl);
				this.ExtractMatrixArrayImpl(name, NoAllocHelpers.ExtractArrayFromList<Matrix4x4>(values));
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00020BBC File Offset: 0x0001EDBC
		public void SetInt(string name, int value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), (float)value);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00020BCE File Offset: 0x0001EDCE
		public void SetInt(int nameID, int value)
		{
			this.SetFloatImpl(nameID, (float)value);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00020BDB File Offset: 0x0001EDDB
		public void SetFloat(string name, float value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00020BEC File Offset: 0x0001EDEC
		public void SetFloat(int nameID, float value)
		{
			this.SetFloatImpl(nameID, value);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00020BF8 File Offset: 0x0001EDF8
		public void SetInteger(string name, int value)
		{
			this.SetIntImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00020C09 File Offset: 0x0001EE09
		public void SetInteger(int nameID, int value)
		{
			this.SetIntImpl(nameID, value);
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00020C15 File Offset: 0x0001EE15
		public void SetColor(string name, Color value)
		{
			this.SetColorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00020C26 File Offset: 0x0001EE26
		public void SetColor(int nameID, Color value)
		{
			this.SetColorImpl(nameID, value);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00020C32 File Offset: 0x0001EE32
		public void SetVector(string name, Vector4 value)
		{
			this.SetColorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00020C48 File Offset: 0x0001EE48
		public void SetVector(int nameID, Vector4 value)
		{
			this.SetColorImpl(nameID, value);
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00020C59 File Offset: 0x0001EE59
		public void SetMatrix(string name, Matrix4x4 value)
		{
			this.SetMatrixImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00020C6A File Offset: 0x0001EE6A
		public void SetMatrix(int nameID, Matrix4x4 value)
		{
			this.SetMatrixImpl(nameID, value);
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00020C76 File Offset: 0x0001EE76
		public void SetTexture(string name, Texture value)
		{
			this.SetTextureImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00020C87 File Offset: 0x0001EE87
		public void SetTexture(int nameID, Texture value)
		{
			this.SetTextureImpl(nameID, value);
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00020C93 File Offset: 0x0001EE93
		public void SetTexture(string name, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(Shader.PropertyToID(name), value, element);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00020CA5 File Offset: 0x0001EEA5
		public void SetTexture(int nameID, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(nameID, value, element);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00020CB2 File Offset: 0x0001EEB2
		public void SetBuffer(string name, ComputeBuffer value)
		{
			this.SetBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00020CC3 File Offset: 0x0001EEC3
		public void SetBuffer(int nameID, ComputeBuffer value)
		{
			this.SetBufferImpl(nameID, value);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00020CCF File Offset: 0x0001EECF
		public void SetBuffer(string name, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00020CE0 File Offset: 0x0001EEE0
		public void SetBuffer(int nameID, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(nameID, value);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00020CEC File Offset: 0x0001EEEC
		public void SetConstantBuffer(string name, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00020D00 File Offset: 0x0001EF00
		public void SetConstantBuffer(int nameID, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00020D0F File Offset: 0x0001EF0F
		public void SetConstantBuffer(string name, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00020D23 File Offset: 0x0001EF23
		public void SetConstantBuffer(int nameID, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00020D32 File Offset: 0x0001EF32
		public void SetFloatArray(string name, List<float> values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromList<float>(values), values.Count);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00020D4E File Offset: 0x0001EF4E
		public void SetFloatArray(int nameID, List<float> values)
		{
			this.SetFloatArray(nameID, NoAllocHelpers.ExtractArrayFromList<float>(values), values.Count);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00020D65 File Offset: 0x0001EF65
		public void SetFloatArray(string name, float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00020D79 File Offset: 0x0001EF79
		public void SetFloatArray(int nameID, float[] values)
		{
			this.SetFloatArray(nameID, values, values.Length);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00020D88 File Offset: 0x0001EF88
		public void SetColorArray(string name, List<Color> values)
		{
			this.SetColorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromList<Color>(values), values.Count);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00020DA4 File Offset: 0x0001EFA4
		public void SetColorArray(int nameID, List<Color> values)
		{
			this.SetColorArray(nameID, NoAllocHelpers.ExtractArrayFromList<Color>(values), values.Count);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00020DBB File Offset: 0x0001EFBB
		public void SetColorArray(string name, Color[] values)
		{
			this.SetColorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00020DCF File Offset: 0x0001EFCF
		public void SetColorArray(int nameID, Color[] values)
		{
			this.SetColorArray(nameID, values, values.Length);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00020DDE File Offset: 0x0001EFDE
		public void SetVectorArray(string name, List<Vector4> values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromList<Vector4>(values), values.Count);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00020DFA File Offset: 0x0001EFFA
		public void SetVectorArray(int nameID, List<Vector4> values)
		{
			this.SetVectorArray(nameID, NoAllocHelpers.ExtractArrayFromList<Vector4>(values), values.Count);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00020E11 File Offset: 0x0001F011
		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00020E25 File Offset: 0x0001F025
		public void SetVectorArray(int nameID, Vector4[] values)
		{
			this.SetVectorArray(nameID, values, values.Length);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00020E34 File Offset: 0x0001F034
		public void SetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromList<Matrix4x4>(values), values.Count);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00020E50 File Offset: 0x0001F050
		public void SetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.SetMatrixArray(nameID, NoAllocHelpers.ExtractArrayFromList<Matrix4x4>(values), values.Count);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00020E67 File Offset: 0x0001F067
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00020E7B File Offset: 0x0001F07B
		public void SetMatrixArray(int nameID, Matrix4x4[] values)
		{
			this.SetMatrixArray(nameID, values, values.Length);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00020E8C File Offset: 0x0001F08C
		public int GetInt(string name)
		{
			return (int)this.GetFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00020EAC File Offset: 0x0001F0AC
		public int GetInt(int nameID)
		{
			return (int)this.GetFloatImpl(nameID);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00020EC8 File Offset: 0x0001F0C8
		public float GetFloat(string name)
		{
			return this.GetFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00020EE8 File Offset: 0x0001F0E8
		public float GetFloat(int nameID)
		{
			return this.GetFloatImpl(nameID);
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00020F04 File Offset: 0x0001F104
		public int GetInteger(string name)
		{
			return this.GetIntImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00020F24 File Offset: 0x0001F124
		public int GetInteger(int nameID)
		{
			return this.GetIntImpl(nameID);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00020F40 File Offset: 0x0001F140
		public Color GetColor(string name)
		{
			return this.GetColorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00020F60 File Offset: 0x0001F160
		public Color GetColor(int nameID)
		{
			return this.GetColorImpl(nameID);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00020F7C File Offset: 0x0001F17C
		public Vector4 GetVector(string name)
		{
			return this.GetColorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00020FA0 File Offset: 0x0001F1A0
		public Vector4 GetVector(int nameID)
		{
			return this.GetColorImpl(nameID);
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00020FC0 File Offset: 0x0001F1C0
		public Matrix4x4 GetMatrix(string name)
		{
			return this.GetMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00020FE0 File Offset: 0x0001F1E0
		public Matrix4x4 GetMatrix(int nameID)
		{
			return this.GetMatrixImpl(nameID);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00020FFC File Offset: 0x0001F1FC
		public Texture GetTexture(string name)
		{
			return this.GetTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0002101C File Offset: 0x0001F21C
		public Texture GetTexture(int nameID)
		{
			return this.GetTextureImpl(nameID);
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00021038 File Offset: 0x0001F238
		public GraphicsBufferHandle GetBuffer(string name)
		{
			return this.GetBufferImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00021058 File Offset: 0x0001F258
		public GraphicsBufferHandle GetConstantBuffer(string name)
		{
			return this.GetConstantBufferImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00021078 File Offset: 0x0001F278
		public float[] GetFloatArray(string name)
		{
			return this.GetFloatArray(Shader.PropertyToID(name));
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00021098 File Offset: 0x0001F298
		public float[] GetFloatArray(int nameID)
		{
			return (this.GetFloatArrayCountImpl(nameID) != 0) ? this.GetFloatArrayImpl(nameID) : null;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x000210C0 File Offset: 0x0001F2C0
		public Color[] GetColorArray(string name)
		{
			return this.GetColorArray(Shader.PropertyToID(name));
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x000210E0 File Offset: 0x0001F2E0
		public Color[] GetColorArray(int nameID)
		{
			return (this.GetColorArrayCountImpl(nameID) != 0) ? this.GetColorArrayImpl(nameID) : null;
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00021108 File Offset: 0x0001F308
		public Vector4[] GetVectorArray(string name)
		{
			return this.GetVectorArray(Shader.PropertyToID(name));
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00021128 File Offset: 0x0001F328
		public Vector4[] GetVectorArray(int nameID)
		{
			return (this.GetVectorArrayCountImpl(nameID) != 0) ? this.GetVectorArrayImpl(nameID) : null;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00021150 File Offset: 0x0001F350
		public Matrix4x4[] GetMatrixArray(string name)
		{
			return this.GetMatrixArray(Shader.PropertyToID(name));
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00021170 File Offset: 0x0001F370
		public Matrix4x4[] GetMatrixArray(int nameID)
		{
			return (this.GetMatrixArrayCountImpl(nameID) != 0) ? this.GetMatrixArrayImpl(nameID) : null;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00021195 File Offset: 0x0001F395
		public void GetFloatArray(string name, List<float> values)
		{
			this.ExtractFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x000211A6 File Offset: 0x0001F3A6
		public void GetFloatArray(int nameID, List<float> values)
		{
			this.ExtractFloatArray(nameID, values);
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x000211B2 File Offset: 0x0001F3B2
		public void GetColorArray(string name, List<Color> values)
		{
			this.ExtractColorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x000211C3 File Offset: 0x0001F3C3
		public void GetColorArray(int nameID, List<Color> values)
		{
			this.ExtractColorArray(nameID, values);
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x000211CF File Offset: 0x0001F3CF
		public void GetVectorArray(string name, List<Vector4> values)
		{
			this.ExtractVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x000211E0 File Offset: 0x0001F3E0
		public void GetVectorArray(int nameID, List<Vector4> values)
		{
			this.ExtractVectorArray(nameID, values);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x000211EC File Offset: 0x0001F3EC
		public void GetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x000211FD File Offset: 0x0001F3FD
		public void GetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(nameID, values);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00021209 File Offset: 0x0001F409
		public void SetTextureOffset(string name, Vector2 value)
		{
			this.SetTextureOffsetImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0002121A File Offset: 0x0001F41A
		public void SetTextureOffset(int nameID, Vector2 value)
		{
			this.SetTextureOffsetImpl(nameID, value);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00021226 File Offset: 0x0001F426
		public void SetTextureScale(string name, Vector2 value)
		{
			this.SetTextureScaleImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00021237 File Offset: 0x0001F437
		public void SetTextureScale(int nameID, Vector2 value)
		{
			this.SetTextureScaleImpl(nameID, value);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00021244 File Offset: 0x0001F444
		public Vector2 GetTextureOffset(string name)
		{
			return this.GetTextureOffset(Shader.PropertyToID(name));
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00021264 File Offset: 0x0001F464
		public Vector2 GetTextureOffset(int nameID)
		{
			Vector4 textureScaleAndOffsetImpl = this.GetTextureScaleAndOffsetImpl(nameID);
			return new Vector2(textureScaleAndOffsetImpl.z, textureScaleAndOffsetImpl.w);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00021290 File Offset: 0x0001F490
		public Vector2 GetTextureScale(string name)
		{
			return this.GetTextureScale(Shader.PropertyToID(name));
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x000212B0 File Offset: 0x0001F4B0
		public Vector2 GetTextureScale(int nameID)
		{
			Vector4 textureScaleAndOffsetImpl = this.GetTextureScaleAndOffsetImpl(nameID);
			return new Vector2(textureScaleAndOffsetImpl.x, textureScaleAndOffsetImpl.y);
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x000212DC File Offset: 0x0001F4DC
		public string[] GetPropertyNames(MaterialPropertyType type)
		{
			return this.GetPropertyNamesImpl((int)type);
		}

		// Token: 0x060012EB RID: 4843
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateWithShader_Injected([Writable] Material self, IntPtr shader);

		// Token: 0x060012EC RID: 4844
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateWithMaterial_Injected([Writable] Material self, IntPtr source);

		// Token: 0x060012ED RID: 4845
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDefaultMaterial_Injected();

		// Token: 0x060012EE RID: 4846
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDefaultParticleMaterial_Injected();

		// Token: 0x060012EF RID: 4847
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDefaultLineMaterial_Injected();

		// Token: 0x060012F0 RID: 4848
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_shader_Injected(IntPtr _unity_self);

		// Token: 0x060012F1 RID: 4849
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shader_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060012F2 RID: 4850
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetFirstPropertyNameIdByAttribute_Injected(IntPtr _unity_self, ShaderPropertyFlags attributeFlag);

		// Token: 0x060012F3 RID: 4851
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasProperty_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060012F4 RID: 4852
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasFloatImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x060012F5 RID: 4853
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasIntImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x060012F6 RID: 4854
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasTextureImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x060012F7 RID: 4855
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasMatrixImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x060012F8 RID: 4856
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasVectorImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x060012F9 RID: 4857
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasBufferImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x060012FA RID: 4858
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasConstantBufferImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x060012FB RID: 4859
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_renderQueue_Injected(IntPtr _unity_self);

		// Token: 0x060012FC RID: 4860
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_renderQueue_Injected(IntPtr _unity_self, int value);

		// Token: 0x060012FD RID: 4861
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_rawRenderQueue_Injected(IntPtr _unity_self);

		// Token: 0x060012FE RID: 4862
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableKeyword_Injected(IntPtr _unity_self, ref ManagedSpanWrapper keyword);

		// Token: 0x060012FF RID: 4863
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableKeyword_Injected(IntPtr _unity_self, ref ManagedSpanWrapper keyword);

		// Token: 0x06001300 RID: 4864
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsKeywordEnabled_Injected(IntPtr _unity_self, ref ManagedSpanWrapper keyword);

		// Token: 0x06001301 RID: 4865
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableLocalKeyword_Injected(IntPtr _unity_self, [In] ref LocalKeyword keyword);

		// Token: 0x06001302 RID: 4866
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableLocalKeyword_Injected(IntPtr _unity_self, [In] ref LocalKeyword keyword);

		// Token: 0x06001303 RID: 4867
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalKeyword_Injected(IntPtr _unity_self, [In] ref LocalKeyword keyword, bool value);

		// Token: 0x06001304 RID: 4868
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsLocalKeywordEnabled_Injected(IntPtr _unity_self, [In] ref LocalKeyword keyword);

		// Token: 0x06001305 RID: 4869
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LocalKeyword[] GetEnabledKeywords_Injected(IntPtr _unity_self);

		// Token: 0x06001306 RID: 4870
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetEnabledKeywords_Injected(IntPtr _unity_self, LocalKeyword[] keywords);

		// Token: 0x06001307 RID: 4871
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MaterialGlobalIlluminationFlags get_globalIlluminationFlags_Injected(IntPtr _unity_self);

		// Token: 0x06001308 RID: 4872
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_globalIlluminationFlags_Injected(IntPtr _unity_self, MaterialGlobalIlluminationFlags value);

		// Token: 0x06001309 RID: 4873
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_doubleSidedGI_Injected(IntPtr _unity_self);

		// Token: 0x0600130A RID: 4874
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_doubleSidedGI_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600130B RID: 4875
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enableInstancing_Injected(IntPtr _unity_self);

		// Token: 0x0600130C RID: 4876
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enableInstancing_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600130D RID: 4877
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_passCount_Injected(IntPtr _unity_self);

		// Token: 0x0600130E RID: 4878
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetShaderPassEnabled_Injected(IntPtr _unity_self, ref ManagedSpanWrapper passName, bool enabled);

		// Token: 0x0600130F RID: 4879
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetShaderPassEnabled_Injected(IntPtr _unity_self, ref ManagedSpanWrapper passName);

		// Token: 0x06001310 RID: 4880
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPassName_Injected(IntPtr _unity_self, int pass, out ManagedSpanWrapper ret);

		// Token: 0x06001311 RID: 4881
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int FindPass_Injected(IntPtr _unity_self, ref ManagedSpanWrapper passName);

		// Token: 0x06001312 RID: 4882
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetOverrideTag_Injected(IntPtr _unity_self, ref ManagedSpanWrapper tag, ref ManagedSpanWrapper val);

		// Token: 0x06001313 RID: 4883
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTagImpl_Injected(IntPtr _unity_self, ref ManagedSpanWrapper tag, bool currentSubShaderOnly, ref ManagedSpanWrapper defaultValue, out ManagedSpanWrapper ret);

		// Token: 0x06001314 RID: 4884
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Lerp_Injected(IntPtr _unity_self, IntPtr start, IntPtr end, float t);

		// Token: 0x06001315 RID: 4885
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetPass_Injected(IntPtr _unity_self, int pass);

		// Token: 0x06001316 RID: 4886
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPropertiesFromMaterial_Injected(IntPtr _unity_self, IntPtr mat);

		// Token: 0x06001317 RID: 4887
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyMatchingPropertiesFromMaterial_Injected(IntPtr _unity_self, IntPtr mat);

		// Token: 0x06001318 RID: 4888
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetShaderKeywords_Injected(IntPtr _unity_self);

		// Token: 0x06001319 RID: 4889
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetShaderKeywords_Injected(IntPtr _unity_self, string[] names);

		// Token: 0x0600131A RID: 4890
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetPropertyNamesImpl_Injected(IntPtr _unity_self, int propertyType);

		// Token: 0x0600131B RID: 4891
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ComputeCRC_Injected(IntPtr _unity_self);

		// Token: 0x0600131C RID: 4892
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetTexturePropertyNames_Injected(IntPtr _unity_self);

		// Token: 0x0600131D RID: 4893
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTexturePropertyNameIDs_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x0600131E RID: 4894
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTexturePropertyNamesInternal_Injected(IntPtr _unity_self, object outNames);

		// Token: 0x0600131F RID: 4895
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTexturePropertyNameIDsInternal_Injected(IntPtr _unity_self, object outNames);

		// Token: 0x06001320 RID: 4896
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIntImpl_Injected(IntPtr _unity_self, int name, int value);

		// Token: 0x06001321 RID: 4897
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatImpl_Injected(IntPtr _unity_self, int name, float value);

		// Token: 0x06001322 RID: 4898
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetColorImpl_Injected(IntPtr _unity_self, int name, [In] ref Color value);

		// Token: 0x06001323 RID: 4899
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrixImpl_Injected(IntPtr _unity_self, int name, [In] ref Matrix4x4 value);

		// Token: 0x06001324 RID: 4900
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTextureImpl_Injected(IntPtr _unity_self, int name, IntPtr value);

		// Token: 0x06001325 RID: 4901
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderTextureImpl_Injected(IntPtr _unity_self, int name, IntPtr value, RenderTextureSubElement element);

		// Token: 0x06001326 RID: 4902
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBufferImpl_Injected(IntPtr _unity_self, int name, IntPtr value);

		// Token: 0x06001327 RID: 4903
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGraphicsBufferImpl_Injected(IntPtr _unity_self, int name, IntPtr value);

		// Token: 0x06001328 RID: 4904
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetConstantBufferImpl_Injected(IntPtr _unity_self, int name, IntPtr value, int offset, int size);

		// Token: 0x06001329 RID: 4905
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetConstantGraphicsBufferImpl_Injected(IntPtr _unity_self, int name, IntPtr value, int offset, int size);

		// Token: 0x0600132A RID: 4906
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIntImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x0600132B RID: 4907
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloatImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x0600132C RID: 4908
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetColorImpl_Injected(IntPtr _unity_self, int name, out Color ret);

		// Token: 0x0600132D RID: 4909
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMatrixImpl_Injected(IntPtr _unity_self, int name, out Matrix4x4 ret);

		// Token: 0x0600132E RID: 4910
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetTextureImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x0600132F RID: 4911
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBufferImpl_Injected(IntPtr _unity_self, int name, out GraphicsBufferHandle ret);

		// Token: 0x06001330 RID: 4912
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetConstantBufferImpl_Injected(IntPtr _unity_self, int name, out GraphicsBufferHandle ret);

		// Token: 0x06001331 RID: 4913
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatArrayImpl_Injected(IntPtr _unity_self, int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x06001332 RID: 4914
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVectorArrayImpl_Injected(IntPtr _unity_self, int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x06001333 RID: 4915
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetColorArrayImpl_Injected(IntPtr _unity_self, int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x06001334 RID: 4916
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrixArrayImpl_Injected(IntPtr _unity_self, int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x06001335 RID: 4917
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetFloatArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper ret);

		// Token: 0x06001336 RID: 4918
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVectorArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper ret);

		// Token: 0x06001337 RID: 4919
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetColorArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper ret);

		// Token: 0x06001338 RID: 4920
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMatrixArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper ret);

		// Token: 0x06001339 RID: 4921
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetFloatArrayCountImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x0600133A RID: 4922
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVectorArrayCountImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x0600133B RID: 4923
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetColorArrayCountImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x0600133C RID: 4924
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMatrixArrayCountImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x0600133D RID: 4925
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractFloatArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper val);

		// Token: 0x0600133E RID: 4926
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractVectorArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper val);

		// Token: 0x0600133F RID: 4927
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractColorArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper val);

		// Token: 0x06001340 RID: 4928
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractMatrixArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper val);

		// Token: 0x06001341 RID: 4929
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTextureScaleAndOffsetImpl_Injected(IntPtr _unity_self, int name, out Vector4 ret);

		// Token: 0x06001342 RID: 4930
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTextureOffsetImpl_Injected(IntPtr _unity_self, int name, [In] ref Vector2 offset);

		// Token: 0x06001343 RID: 4931
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTextureScaleImpl_Injected(IntPtr _unity_self, int name, [In] ref Vector2 scale);

		// Token: 0x04000501 RID: 1281
		private static readonly int k_ColorId = Shader.PropertyToID("_Color");

		// Token: 0x04000502 RID: 1282
		private static readonly int k_MainTexId = Shader.PropertyToID("_MainTex");
	}
}
