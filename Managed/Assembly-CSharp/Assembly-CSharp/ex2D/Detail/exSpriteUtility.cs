using System;
using UnityEngine;

namespace ex2D.Detail
{
	// Token: 0x02000356 RID: 854
	public static class exSpriteUtility
	{
		// Token: 0x06001FB1 RID: 8113 RVA: 0x0009BEA4 File Offset: 0x0009A0A4
		public static exSprite NewSimpleSprite(GameObject _go, exTextureInfo _info, int _width, int _height, Color _color)
		{
			exSprite exSprite = _go.GetComponent<exSprite>();
			if (exSprite == null)
			{
				exSprite = _go.AddComponent<exSprite>();
			}
			if (exSprite.shader == null)
			{
				exSprite.shader = Shader.Find("ex2D/Alpha Blended");
			}
			exSprite.spriteType = exSpriteType.Simple;
			exSprite.textureInfo = _info;
			exSprite.customSize = true;
			exSprite.width = (float)_width;
			exSprite.height = (float)_height;
			exSprite.color = _color;
			return exSprite;
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x0009BF14 File Offset: 0x0009A114
		public static exSprite NewSlicedSprite(GameObject _go, exTextureInfo _info, int _left, int _right, int _top, int _bottom, int _width, int _height, Color _color, bool _borderOnly)
		{
			exSprite exSprite = _go.GetComponent<exSprite>();
			if (exSprite == null)
			{
				exSprite = _go.AddComponent<exSprite>();
			}
			if (exSprite.shader == null)
			{
				exSprite.shader = Shader.Find("ex2D/Alpha Blended");
			}
			exSprite.spriteType = exSpriteType.Sliced;
			exSprite.textureInfo = _info;
			exSprite.borderOnly = _borderOnly;
			exSprite.customBorderSize = true;
			exSprite.leftBorderSize = (float)_left;
			exSprite.rightBorderSize = (float)_right;
			exSprite.topBorderSize = (float)_top;
			exSprite.bottomBorderSize = (float)_bottom;
			exSprite.customSize = true;
			exSprite.width = (float)_width;
			exSprite.height = (float)_height;
			exSprite.color = _color;
			return exSprite;
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x0009BFB8 File Offset: 0x0009A1B8
		public static void GetDicingCount(exTextureInfo _ti, out int _colCount, out int _rowCount)
		{
			_colCount = 1;
			_rowCount = 1;
			if (_ti != null)
			{
				if (_ti.diceUnitWidth > 0 && _ti.width > 0)
				{
					_colCount = Mathf.CeilToInt((float)_ti.width / (float)_ti.diceUnitWidth);
				}
				if (_ti.diceUnitHeight > 0 && _ti.height > 0)
				{
					_rowCount = Mathf.CeilToInt((float)_ti.height / (float)_ti.diceUnitHeight);
				}
			}
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x0009C024 File Offset: 0x0009A224
		public static void GetTilingCount(exISprite _sprite, out int _colCount, out int _rowCount)
		{
			exTextureInfo textureInfo = _sprite.textureInfo;
			if (textureInfo != null && (float)textureInfo.width + _sprite.tiledSpacing.x != 0f && (float)textureInfo.height + _sprite.tiledSpacing.y != 0f)
			{
				_colCount = Mathf.Max(Mathf.CeilToInt(_sprite.width / ((float)textureInfo.width + _sprite.tiledSpacing.x)), 1);
				_rowCount = Mathf.Max(Mathf.CeilToInt(_sprite.height / ((float)textureInfo.height + _sprite.tiledSpacing.y)), 1);
				return;
			}
			_colCount = 1;
			_rowCount = 1;
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x0009C0D0 File Offset: 0x0009A2D0
		public static void SetTextureInfo(exSpriteBase _sprite, ref exTextureInfo _ti, exTextureInfo _newTi, bool _useTextureOffset, exSpriteType _spriteType)
		{
			exTextureInfo exTextureInfo = _ti;
			_ti = _newTi;
			if (_newTi != null)
			{
				if (_newTi.texture == null)
				{
					Debug.LogWarning("invalid textureInfo");
				}
				if (_spriteType == exSpriteType.Tiled)
				{
					if (exTextureInfo == null || exTextureInfo == _newTi || _newTi.width != exTextureInfo.width || _newTi.height != exTextureInfo.height)
					{
						(_sprite as exISprite).UpdateBufferSize();
						_sprite.updateFlags |= exUpdateFlags.Vertex;
					}
				}
				else if (_spriteType == exSpriteType.Diced)
				{
					(_sprite as exISprite).UpdateBufferSize();
					_sprite.updateFlags |= exUpdateFlags.Vertex;
				}
				else if (!_sprite.customSize && (exTextureInfo == null || _newTi.width != exTextureInfo.width || _newTi.height != exTextureInfo.height))
				{
					_sprite.updateFlags |= exUpdateFlags.Vertex;
				}
				if (_useTextureOffset)
				{
					_sprite.updateFlags |= exUpdateFlags.Vertex;
				}
				_sprite.updateFlags |= exUpdateFlags.UV;
				if (exTextureInfo == null || exTextureInfo.texture != _newTi.texture)
				{
					_sprite.updateFlags |= exUpdateFlags.Vertex | exUpdateFlags.UV;
					(_sprite as exISprite).UpdateMaterial();
				}
			}
		}
	}
}
