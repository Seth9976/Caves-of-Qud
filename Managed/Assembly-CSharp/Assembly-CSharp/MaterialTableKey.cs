using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F7 RID: 247
public struct MaterialTableKey
{
	// Token: 0x06000960 RID: 2400 RVA: 0x0003B376 File Offset: 0x00039576
	public MaterialTableKey(Shader _shader, Texture _texture)
	{
		this.shader = _shader;
		this.texture = _texture;
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x0003B386 File Offset: 0x00039586
	public MaterialTableKey(Material _material)
	{
		this = new MaterialTableKey(_material.shader, _material.mainTexture);
	}

	// Token: 0x04000624 RID: 1572
	public Shader shader;

	// Token: 0x04000625 RID: 1573
	public Texture texture;

	// Token: 0x020015DD RID: 5597
	public class Comparer : IEqualityComparer<MaterialTableKey>
	{
		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x0600D63D RID: 54845 RVA: 0x004F7D24 File Offset: 0x004F5F24
		public static MaterialTableKey.Comparer instance
		{
			get
			{
				if (MaterialTableKey.Comparer.instance_ == null)
				{
					MaterialTableKey.Comparer.instance_ = new MaterialTableKey.Comparer();
				}
				return MaterialTableKey.Comparer.instance_;
			}
		}

		// Token: 0x0600D63E RID: 54846 RVA: 0x004F7D3C File Offset: 0x004F5F3C
		public bool Equals(MaterialTableKey _lhs, MaterialTableKey _rhs)
		{
			return _lhs.shader == _rhs.shader && _lhs.texture == _rhs.texture;
		}

		// Token: 0x0600D63F RID: 54847 RVA: 0x004F7D5C File Offset: 0x004F5F5C
		public int GetHashCode(MaterialTableKey _obj)
		{
			int num;
			if (_obj.shader != null)
			{
				num = _obj.shader.GetHashCode();
			}
			else
			{
				num = 0;
			}
			int num2;
			if (_obj.texture != null)
			{
				num2 = _obj.texture.GetHashCode() * 1313;
			}
			else
			{
				num2 = 0;
			}
			return num ^ num2;
		}

		// Token: 0x04005830 RID: 22576
		private static MaterialTableKey.Comparer instance_;
	}
}
