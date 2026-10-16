using System;
using UnityEngine;

namespace ex2D.Detail
{
	// Token: 0x02000357 RID: 855
	public static class exISpriteExtends
	{
		// Token: 0x06001FB6 RID: 8118 RVA: 0x0009C1FC File Offset: 0x0009A3FC
		public static Vector2 GetTextureOffset(this exISprite _sprite)
		{
			Vector2 zero = Vector2.zero;
			if (_sprite.useTextureOffset)
			{
				exTextureInfo textureInfo = _sprite.textureInfo;
				switch (_sprite.anchor)
				{
				case Anchor.TopLeft:
					zero.x = (float)textureInfo.trim_x;
					zero.y = (float)(textureInfo.trim_y - (textureInfo.rawHeight - textureInfo.height));
					break;
				case Anchor.TopCenter:
					zero.x = (float)textureInfo.trim_x - (float)(textureInfo.rawWidth - textureInfo.width) * 0.5f;
					zero.y = (float)(textureInfo.trim_y - (textureInfo.rawHeight - textureInfo.height));
					break;
				case Anchor.TopRight:
					zero.x = (float)(textureInfo.trim_x - (textureInfo.rawWidth - textureInfo.width));
					zero.y = (float)(textureInfo.trim_y - (textureInfo.rawHeight - textureInfo.height));
					break;
				case Anchor.MidLeft:
					zero.x = (float)textureInfo.trim_x;
					zero.y = (float)textureInfo.trim_y - (float)(textureInfo.rawHeight - textureInfo.height) * 0.5f;
					break;
				case Anchor.MidCenter:
					zero.x = (float)textureInfo.trim_x - (float)(textureInfo.rawWidth - textureInfo.width) * 0.5f;
					zero.y = (float)textureInfo.trim_y - (float)(textureInfo.rawHeight - textureInfo.height) * 0.5f;
					break;
				case Anchor.MidRight:
					zero.x = (float)(textureInfo.trim_x - (textureInfo.rawWidth - textureInfo.width));
					zero.y = (float)textureInfo.trim_y - (float)(textureInfo.rawHeight - textureInfo.height) * 0.5f;
					break;
				case Anchor.BotLeft:
					zero.x = (float)textureInfo.trim_x;
					zero.y = (float)textureInfo.trim_y;
					break;
				case Anchor.BotCenter:
					zero.x = (float)textureInfo.trim_x - (float)(textureInfo.rawWidth - textureInfo.width) * 0.5f;
					zero.y = (float)textureInfo.trim_y;
					break;
				case Anchor.BotRight:
					zero.x = (float)(textureInfo.trim_x - (textureInfo.rawWidth - textureInfo.width));
					zero.y = (float)textureInfo.trim_y;
					break;
				default:
					zero.x = (float)textureInfo.trim_x - (float)(textureInfo.rawWidth - textureInfo.width) * 0.5f;
					zero.y = (float)textureInfo.trim_y - (float)(textureInfo.rawHeight - textureInfo.height) * 0.5f;
					break;
				}
				Vector2 vector = new Vector2(_sprite.width / (float)_sprite.textureInfo.width, _sprite.height / (float)_sprite.textureInfo.height);
				zero.x *= vector.x;
				zero.y *= vector.y;
			}
			return zero;
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x0009C4E4 File Offset: 0x0009A6E4
		public static void GetVertexAndIndexCount(this exISprite _sprite, out int _vertexCount, out int _indexCount)
		{
			switch (_sprite.spriteType)
			{
			case exSpriteType.Simple:
				_vertexCount = 4;
				_indexCount = 6;
				return;
			case exSpriteType.Sliced:
				_vertexCount = 16;
				_indexCount = 54;
				if (_sprite.borderOnly)
				{
					_indexCount -= 6;
					return;
				}
				break;
			case exSpriteType.Tiled:
			{
				int num;
				int num2;
				exSpriteUtility.GetTilingCount(_sprite, out num, out num2);
				int num3 = num * num2;
				_vertexCount = num3 * 4;
				if (_vertexCount > 65000)
				{
					Debug.LogWarning(_sprite.gameObject.name + " is too big. Consider using a bigger texture.", _sprite.gameObject);
					int num4 = (int)Mathf.Sqrt(16250f);
					if (num > num4)
					{
						_sprite.width = ((float)_sprite.textureInfo.width + _sprite.tiledSpacing.x) * (float)num4;
					}
					if (num2 > num4)
					{
						_sprite.height = ((float)_sprite.textureInfo.height + _sprite.tiledSpacing.y) * (float)num4;
					}
					exSpriteUtility.GetTilingCount(_sprite, out num, out num2);
					num3 = num * num2;
					_vertexCount = num3 * 4;
				}
				_indexCount = num3 * 6;
				return;
			}
			case exSpriteType.Diced:
			{
				exTextureInfo textureInfo = _sprite.textureInfo;
				if (textureInfo == null || !textureInfo.isDiced)
				{
					_vertexCount = 4;
					_indexCount = 6;
					return;
				}
				int num5 = 0;
				DiceEnumerator dices = _sprite.textureInfo.dices;
				while (dices.MoveNext())
				{
					if (dices.Current.sizeType != exTextureInfo.DiceType.Empty)
					{
						num5++;
					}
				}
				if (num5 == 0)
				{
					num5 = 1;
				}
				_vertexCount = num5 * 4;
				_indexCount = num5 * 6;
				if (_vertexCount > 65000)
				{
					Debug.LogError("The texture info [" + _sprite.textureInfo.name + "] has too many dices! Please using a bigger dice value.", _sprite.textureInfo);
					_vertexCount = 4;
					_indexCount = 6;
					return;
				}
				break;
			}
			default:
				_vertexCount = 4;
				_indexCount = 6;
				break;
			}
		}
	}
}
