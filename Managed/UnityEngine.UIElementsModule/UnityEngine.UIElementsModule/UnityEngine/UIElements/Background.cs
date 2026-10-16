using System;
using System.Collections.Generic;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000346 RID: 838
	public struct Background : IEquatable<Background>
	{
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x0005DAA0 File Offset: 0x0005BCA0
		// (set) Token: 0x06001843 RID: 6211 RVA: 0x0005DAB8 File Offset: 0x0005BCB8
		public Texture2D texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				bool flag = this.m_Texture == value;
				if (!flag)
				{
					this.m_Texture = value;
					this.m_Sprite = null;
					this.m_RenderTexture = null;
					this.m_VectorImage = null;
				}
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x0005DAF4 File Offset: 0x0005BCF4
		// (set) Token: 0x06001845 RID: 6213 RVA: 0x0005DB0C File Offset: 0x0005BD0C
		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				bool flag = this.m_Sprite == value;
				if (!flag)
				{
					this.m_Texture = null;
					this.m_Sprite = value;
					this.m_RenderTexture = null;
					this.m_VectorImage = null;
				}
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x0005DB48 File Offset: 0x0005BD48
		// (set) Token: 0x06001847 RID: 6215 RVA: 0x0005DB60 File Offset: 0x0005BD60
		public RenderTexture renderTexture
		{
			get
			{
				return this.m_RenderTexture;
			}
			set
			{
				bool flag = this.m_RenderTexture == value;
				if (!flag)
				{
					this.m_Texture = null;
					this.m_Sprite = null;
					this.m_RenderTexture = value;
					this.m_VectorImage = null;
				}
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x0005DB9C File Offset: 0x0005BD9C
		// (set) Token: 0x06001849 RID: 6217 RVA: 0x0005DBB4 File Offset: 0x0005BDB4
		public VectorImage vectorImage
		{
			get
			{
				return this.m_VectorImage;
			}
			set
			{
				bool flag = this.vectorImage == value;
				if (!flag)
				{
					this.m_Texture = null;
					this.m_Sprite = null;
					this.m_RenderTexture = null;
					this.m_VectorImage = value;
				}
			}
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0005DBF0 File Offset: 0x0005BDF0
		[Obsolete("Use Background.FromTexture2D instead")]
		public Background(Texture2D t)
		{
			this.m_Texture = t;
			this.m_Sprite = null;
			this.m_RenderTexture = null;
			this.m_VectorImage = null;
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0005DC10 File Offset: 0x0005BE10
		public static Background FromTexture2D(Texture2D t)
		{
			return new Background
			{
				texture = t
			};
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0005DC34 File Offset: 0x0005BE34
		public static Background FromRenderTexture(RenderTexture rt)
		{
			return new Background
			{
				renderTexture = rt
			};
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0005DC58 File Offset: 0x0005BE58
		public static Background FromSprite(Sprite s)
		{
			return new Background
			{
				sprite = s
			};
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0005DC7C File Offset: 0x0005BE7C
		public static Background FromVectorImage(VectorImage vi)
		{
			return new Background
			{
				vectorImage = vi
			};
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x0005DCA0 File Offset: 0x0005BEA0
		internal static Background FromObject(object obj)
		{
			Texture2D texture2D = obj as Texture2D;
			bool flag = texture2D != null;
			Background background;
			if (flag)
			{
				background = Background.FromTexture2D(texture2D);
			}
			else
			{
				RenderTexture renderTexture = obj as RenderTexture;
				bool flag2 = renderTexture != null;
				if (flag2)
				{
					background = Background.FromRenderTexture(renderTexture);
				}
				else
				{
					Sprite sprite = obj as Sprite;
					bool flag3 = sprite != null;
					if (flag3)
					{
						background = Background.FromSprite(sprite);
					}
					else
					{
						VectorImage vectorImage = obj as VectorImage;
						bool flag4 = vectorImage != null;
						if (flag4)
						{
							background = Background.FromVectorImage(vectorImage);
						}
						else
						{
							background = default(Background);
						}
					}
				}
			}
			return background;
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x0005DD38 File Offset: 0x0005BF38
		internal static IEnumerable<Type> allowedAssetTypes
		{
			get
			{
				yield return typeof(Texture2D);
				yield return typeof(RenderTexture);
				yield return typeof(Sprite);
				yield return typeof(VectorImage);
				yield break;
			}
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0005DD50 File Offset: 0x0005BF50
		public Object GetSelectedImage()
		{
			bool flag = this.texture != null;
			Object @object;
			if (flag)
			{
				@object = this.texture;
			}
			else
			{
				bool flag2 = this.sprite != null;
				if (flag2)
				{
					@object = this.sprite;
				}
				else
				{
					bool flag3 = this.renderTexture != null;
					if (flag3)
					{
						@object = this.renderTexture;
					}
					else
					{
						bool flag4 = this.vectorImage != null;
						if (flag4)
						{
							@object = this.vectorImage;
						}
						else
						{
							@object = null;
						}
					}
				}
			}
			return @object;
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0005DDCC File Offset: 0x0005BFCC
		public bool IsEmpty()
		{
			return this.texture == null && this.sprite == null && this.vectorImage == null && this.renderTexture == null;
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0005DE18 File Offset: 0x0005C018
		public static bool operator ==(Background lhs, Background rhs)
		{
			return lhs.texture == rhs.texture && lhs.sprite == rhs.sprite && lhs.renderTexture == rhs.renderTexture && lhs.vectorImage == rhs.vectorImage;
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0005DE80 File Offset: 0x0005C080
		public static bool operator !=(Background lhs, Background rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0005DE9C File Offset: 0x0005C09C
		public static implicit operator Background(Texture2D v)
		{
			return Background.FromTexture2D(v);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0005DEB4 File Offset: 0x0005C0B4
		public bool Equals(Background other)
		{
			return other == this;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0005DED4 File Offset: 0x0005C0D4
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Background);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Background background = (Background)obj;
				flag2 = background == this;
			}
			return flag2;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0005DF10 File Offset: 0x0005C110
		public override int GetHashCode()
		{
			int num = 851985039;
			bool flag = this.texture != null;
			if (flag)
			{
				num = num * -1521134295 + this.texture.GetHashCode();
			}
			bool flag2 = this.sprite != null;
			if (flag2)
			{
				num = num * -1521134295 + this.sprite.GetHashCode();
			}
			bool flag3 = this.renderTexture != null;
			if (flag3)
			{
				num = num * -1521134295 + this.renderTexture.GetHashCode();
			}
			bool flag4 = this.vectorImage != null;
			if (flag4)
			{
				num = num * -1521134295 + this.vectorImage.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0005DFB4 File Offset: 0x0005C1B4
		public override string ToString()
		{
			bool flag = this.texture != null;
			string text;
			if (flag)
			{
				text = this.texture.ToString();
			}
			else
			{
				bool flag2 = this.sprite != null;
				if (flag2)
				{
					text = this.sprite.ToString();
				}
				else
				{
					bool flag3 = this.renderTexture != null;
					if (flag3)
					{
						text = this.renderTexture.ToString();
					}
					else
					{
						bool flag4 = this.vectorImage != null;
						if (flag4)
						{
							text = this.vectorImage.ToString();
						}
						else
						{
							text = "";
						}
					}
				}
			}
			return text;
		}

		// Token: 0x04000D3A RID: 3386
		private Texture2D m_Texture;

		// Token: 0x04000D3B RID: 3387
		private Sprite m_Sprite;

		// Token: 0x04000D3C RID: 3388
		private RenderTexture m_RenderTexture;

		// Token: 0x04000D3D RID: 3389
		private VectorImage m_VectorImage;

		// Token: 0x02000347 RID: 839
		internal class PropertyBag : ContainerPropertyBag<Background>
		{
			// Token: 0x0600185A RID: 6234 RVA: 0x0005E045 File Offset: 0x0005C245
			public PropertyBag()
			{
				base.AddProperty<Texture2D>(new Background.PropertyBag.TextureProperty());
				base.AddProperty<Sprite>(new Background.PropertyBag.SpriteProperty());
				base.AddProperty<RenderTexture>(new Background.PropertyBag.RenderTextureProperty());
				base.AddProperty<VectorImage>(new Background.PropertyBag.VectorImageProperty());
			}

			// Token: 0x02000348 RID: 840
			private class TextureProperty : Property<Background, Texture2D>
			{
				// Token: 0x1700049D RID: 1181
				// (get) Token: 0x0600185B RID: 6235 RVA: 0x0005E07F File Offset: 0x0005C27F
				public override string Name { get; } = "texture";

				// Token: 0x1700049E RID: 1182
				// (get) Token: 0x0600185C RID: 6236 RVA: 0x0005E087 File Offset: 0x0005C287
				public override bool IsReadOnly { get; } = false;

				// Token: 0x0600185D RID: 6237 RVA: 0x0005E08F File Offset: 0x0005C28F
				public override Texture2D GetValue(ref Background container)
				{
					return container.texture;
				}

				// Token: 0x0600185E RID: 6238 RVA: 0x0005E097 File Offset: 0x0005C297
				public override void SetValue(ref Background container, Texture2D value)
				{
					container.texture = value;
				}
			}

			// Token: 0x02000349 RID: 841
			private class SpriteProperty : Property<Background, Sprite>
			{
				// Token: 0x1700049F RID: 1183
				// (get) Token: 0x06001860 RID: 6240 RVA: 0x0005E0BC File Offset: 0x0005C2BC
				public override string Name { get; } = "sprite";

				// Token: 0x170004A0 RID: 1184
				// (get) Token: 0x06001861 RID: 6241 RVA: 0x0005E0C4 File Offset: 0x0005C2C4
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001862 RID: 6242 RVA: 0x0005E0CC File Offset: 0x0005C2CC
				public override Sprite GetValue(ref Background container)
				{
					return container.sprite;
				}

				// Token: 0x06001863 RID: 6243 RVA: 0x0005E0D4 File Offset: 0x0005C2D4
				public override void SetValue(ref Background container, Sprite value)
				{
					container.sprite = value;
				}
			}

			// Token: 0x0200034A RID: 842
			private class RenderTextureProperty : Property<Background, RenderTexture>
			{
				// Token: 0x170004A1 RID: 1185
				// (get) Token: 0x06001865 RID: 6245 RVA: 0x0005E0F9 File Offset: 0x0005C2F9
				public override string Name { get; } = "renderTexture";

				// Token: 0x170004A2 RID: 1186
				// (get) Token: 0x06001866 RID: 6246 RVA: 0x0005E101 File Offset: 0x0005C301
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001867 RID: 6247 RVA: 0x0005E109 File Offset: 0x0005C309
				public override RenderTexture GetValue(ref Background container)
				{
					return container.renderTexture;
				}

				// Token: 0x06001868 RID: 6248 RVA: 0x0005E111 File Offset: 0x0005C311
				public override void SetValue(ref Background container, RenderTexture value)
				{
					container.renderTexture = value;
				}
			}

			// Token: 0x0200034B RID: 843
			private class VectorImageProperty : Property<Background, VectorImage>
			{
				// Token: 0x170004A3 RID: 1187
				// (get) Token: 0x0600186A RID: 6250 RVA: 0x0005E136 File Offset: 0x0005C336
				public override string Name { get; } = "vectorImage";

				// Token: 0x170004A4 RID: 1188
				// (get) Token: 0x0600186B RID: 6251 RVA: 0x0005E13E File Offset: 0x0005C33E
				public override bool IsReadOnly { get; } = false;

				// Token: 0x0600186C RID: 6252 RVA: 0x0005E146 File Offset: 0x0005C346
				public override VectorImage GetValue(ref Background container)
				{
					return container.vectorImage;
				}

				// Token: 0x0600186D RID: 6253 RVA: 0x0005E14E File Offset: 0x0005C34E
				public override void SetValue(ref Background container, VectorImage value)
				{
					container.vectorImage = value;
				}
			}
		}
	}
}
