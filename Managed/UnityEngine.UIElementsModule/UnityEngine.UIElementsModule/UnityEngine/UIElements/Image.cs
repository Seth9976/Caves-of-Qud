using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Properties;
using UnityEngine.Internal;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x0200010D RID: 269
	public class Image : VisualElement
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x000256AA File Offset: 0x000238AA
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x000256B4 File Offset: 0x000238B4
		[CreateProperty]
		public Texture image
		{
			get
			{
				return this.m_Image;
			}
			set
			{
				bool flag = this.m_Image == value && this.m_ImageIsInline;
				if (!flag)
				{
					this.m_ImageIsInline = value != null;
					this.SetProperty<Texture, Sprite, VectorImage>(value, ref this.m_Image, ref this.m_Sprite, ref this.m_VectorImage, Image.imageProperty);
				}
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0002570B File Offset: 0x0002390B
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x00025714 File Offset: 0x00023914
		[CreateProperty]
		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				bool flag = this.m_Sprite == value && this.m_ImageIsInline;
				if (!flag)
				{
					this.m_ImageIsInline = value != null;
					this.SetProperty<Sprite, Texture, VectorImage>(value, ref this.m_Sprite, ref this.m_Image, ref this.m_VectorImage, Image.spriteProperty);
				}
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x0002576B File Offset: 0x0002396B
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x00025774 File Offset: 0x00023974
		[CreateProperty]
		public VectorImage vectorImage
		{
			get
			{
				return this.m_VectorImage;
			}
			set
			{
				bool flag = this.m_VectorImage == value && this.m_ImageIsInline;
				if (!flag)
				{
					this.m_ImageIsInline = value != null;
					this.SetProperty<VectorImage, Texture, Sprite>(value, ref this.m_VectorImage, ref this.m_Image, ref this.m_Sprite, Image.vectorImageProperty);
				}
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x000257CB File Offset: 0x000239CB
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x000257D4 File Offset: 0x000239D4
		[CreateProperty]
		public Rect sourceRect
		{
			get
			{
				return this.GetSourceRect();
			}
			set
			{
				bool flag = this.GetSourceRect() == value;
				if (!flag)
				{
					bool flag2 = this.sprite != null;
					if (flag2)
					{
						Debug.LogError("Cannot set sourceRect on a sprite image");
					}
					else
					{
						this.CalculateUV(value);
						base.NotifyPropertyChanged(in Image.sourceRectProperty);
					}
				}
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x00025826 File Offset: 0x00023A26
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x00025830 File Offset: 0x00023A30
		[CreateProperty]
		public Rect uv
		{
			get
			{
				return this.m_UV;
			}
			set
			{
				bool flag = this.m_UV == value;
				if (!flag)
				{
					this.m_UV = value;
					base.NotifyPropertyChanged(in Image.uvProperty);
				}
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00025863 File Offset: 0x00023A63
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x0002586C File Offset: 0x00023A6C
		[CreateProperty]
		public ScaleMode scaleMode
		{
			get
			{
				return this.m_ScaleMode;
			}
			set
			{
				bool flag = this.m_ScaleMode == value && this.m_ScaleModeIsInline;
				if (!flag)
				{
					this.m_ScaleModeIsInline = true;
					this.SetScaleMode(value);
				}
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x000258A1 File Offset: 0x00023AA1
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x000258AC File Offset: 0x00023AAC
		[CreateProperty]
		public Color tintColor
		{
			get
			{
				return this.m_TintColor;
			}
			set
			{
				bool flag = this.m_TintColor == value && this.m_TintColorIsInline;
				if (!flag)
				{
					this.m_TintColorIsInline = true;
					this.SetTintColor(value);
				}
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x000258E8 File Offset: 0x00023AE8
		public Image()
		{
			base.AddToClassList(Image.ussClassName);
			this.m_ScaleMode = ScaleMode.ScaleToFit;
			this.m_TintColor = Color.white;
			this.m_UV = new Rect(0f, 0f, 1f, 1f);
			base.requireMeasureFunction = true;
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0002597C File Offset: 0x00023B7C
		private Vector2 GetTextureDisplaySize(Texture texture)
		{
			Vector2 zero = Vector2.zero;
			bool flag = texture != null;
			if (flag)
			{
				zero = new Vector2((float)texture.width, (float)texture.height);
			}
			return zero;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x000259B8 File Offset: 0x00023BB8
		private Vector2 GetTextureDisplaySize(Sprite sprite)
		{
			Vector2 vector = Vector2.zero;
			bool flag = sprite != null;
			if (flag)
			{
				float num = UIElementsUtility.PixelsPerUnitScaleForElement(this, sprite);
				vector = sprite.bounds.size * sprite.pixelsPerUnit * num;
			}
			return vector;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00025A10 File Offset: 0x00023C10
		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			float num = float.NaN;
			float num2 = float.NaN;
			bool flag = this.image == null && this.sprite == null && this.vectorImage == null;
			Vector2 vector;
			if (flag)
			{
				vector = new Vector2(num, num2);
			}
			else
			{
				Vector2 vector2 = Vector2.zero;
				bool flag2 = this.image != null;
				if (flag2)
				{
					vector2 = this.GetTextureDisplaySize(this.image);
				}
				else
				{
					bool flag3 = this.sprite != null;
					if (flag3)
					{
						vector2 = this.GetTextureDisplaySize(this.sprite);
					}
					else
					{
						vector2 = this.vectorImage.size;
					}
				}
				Rect sourceRect = this.sourceRect;
				bool flag4 = sourceRect != Rect.zero;
				num = (flag4 ? Mathf.Abs(sourceRect.width) : vector2.x);
				num2 = (flag4 ? Mathf.Abs(sourceRect.height) : vector2.y);
				bool flag5 = widthMode == VisualElement.MeasureMode.AtMost;
				if (flag5)
				{
					num = Mathf.Min(num, desiredWidth);
				}
				bool flag6 = heightMode == VisualElement.MeasureMode.AtMost;
				if (flag6)
				{
					num2 = Mathf.Min(num2, desiredHeight);
				}
				vector = new Vector2(num, num2);
			}
			return vector;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00025B3C File Offset: 0x00023D3C
		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			bool flag = this.image == null && this.sprite == null && this.vectorImage == null;
			if (!flag)
			{
				Rect rect = GUIUtility.AlignRectToDevice(base.contentRect);
				VisualElement visualElement = mgc.visualElement;
				Color color = ((visualElement != null) ? visualElement.playModeTintColor : Color.white);
				MeshGenerator.RectangleParams rectangleParams = default(MeshGenerator.RectangleParams);
				bool flag2 = this.image != null;
				if (flag2)
				{
					rectangleParams = MeshGenerator.RectangleParams.MakeTextured(rect, this.uv, this.image, this.scaleMode, color);
				}
				else
				{
					bool flag3 = this.sprite != null;
					if (flag3)
					{
						Vector4 zero = Vector4.zero;
						rectangleParams = MeshGenerator.RectangleParams.MakeSprite(rect, this.uv, this.sprite, this.scaleMode, color, false, ref zero, false);
					}
					else
					{
						bool flag4 = this.vectorImage != null;
						if (flag4)
						{
							rectangleParams = MeshGenerator.RectangleParams.MakeVectorTextured(rect, this.uv, this.vectorImage, this.scaleMode, color);
						}
					}
				}
				rectangleParams.color = this.tintColor;
				mgc.meshGenerator.DrawRectangle(rectangleParams);
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00025C58 File Offset: 0x00023E58
		private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			this.ReadCustomProperties(e.customStyle);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00025C68 File Offset: 0x00023E68
		private void ReadCustomProperties(ICustomStyle customStyleProvider)
		{
			bool flag = !this.m_ImageIsInline;
			if (flag)
			{
				Texture2D texture2D;
				bool flag2 = customStyleProvider.TryGetValue(Image.s_ImageProperty, out texture2D);
				if (flag2)
				{
					this.SetProperty<Texture, Sprite, VectorImage>(texture2D, ref this.m_Image, ref this.m_Sprite, ref this.m_VectorImage, Image.imageProperty);
				}
				else
				{
					Sprite sprite;
					bool flag3 = customStyleProvider.TryGetValue(Image.s_SpriteProperty, out sprite);
					if (flag3)
					{
						this.SetProperty<Sprite, Texture, VectorImage>(sprite, ref this.m_Sprite, ref this.m_Image, ref this.m_VectorImage, Image.spriteProperty);
					}
					else
					{
						VectorImage vectorImage;
						bool flag4 = customStyleProvider.TryGetValue(Image.s_VectorImageProperty, out vectorImage);
						if (flag4)
						{
							this.SetProperty<VectorImage, Texture, Sprite>(vectorImage, ref this.m_VectorImage, ref this.m_Image, ref this.m_Sprite, Image.vectorImageProperty);
						}
						else
						{
							this.ClearProperty();
						}
					}
				}
			}
			string text;
			bool flag5 = !this.m_ScaleModeIsInline && customStyleProvider.TryGetValue(Image.s_ScaleModeProperty, out text);
			if (flag5)
			{
				int num;
				StylePropertyUtil.TryGetEnumIntValue(StyleEnumType.ScaleMode, text, out num);
				this.SetScaleMode((ScaleMode)num);
			}
			bool flag6 = !this.m_TintColorIsInline;
			if (flag6)
			{
				Color color;
				bool flag7 = customStyleProvider.TryGetValue(Image.s_TintColorProperty, out color);
				if (flag7)
				{
					this.SetTintColor(color);
				}
				else
				{
					this.SetTintColor(Color.white);
				}
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00025DA4 File Offset: 0x00023FA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetProperty<T0, T1, T2>(T0 src, ref T0 dst, ref T1 alt0, ref T2 alt1, BindingId binding) where T0 : Object where T1 : Object where T2 : Object
		{
			bool flag = src == dst;
			if (!flag)
			{
				dst = src;
				bool flag2 = dst != null;
				if (flag2)
				{
					alt0 = default(T1);
					alt1 = default(T2);
				}
				bool flag3 = dst == null;
				if (flag3)
				{
					this.uv = new Rect(0f, 0f, 1f, 1f);
					this.ReadCustomProperties(base.customStyle);
				}
				base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
				base.NotifyPropertyChanged(in binding);
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00025E58 File Offset: 0x00024058
		private void ClearProperty()
		{
			bool imageIsInline = this.m_ImageIsInline;
			if (!imageIsInline)
			{
				this.image = null;
				this.sprite = null;
				this.vectorImage = null;
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00025E8C File Offset: 0x0002408C
		private void SetScaleMode(ScaleMode mode)
		{
			bool flag = this.m_ScaleMode != mode;
			if (flag)
			{
				this.m_ScaleMode = mode;
				base.IncrementVersion(VersionChangeType.Repaint);
				base.NotifyPropertyChanged(in Image.scaleModeProperty);
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00025ECC File Offset: 0x000240CC
		private void SetTintColor(Color color)
		{
			bool flag = this.m_TintColor != color;
			if (flag)
			{
				this.m_TintColor = color;
				base.IncrementVersion(VersionChangeType.Repaint);
				base.NotifyPropertyChanged(in Image.tintColorProperty);
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00025F0C File Offset: 0x0002410C
		private void CalculateUV(Rect srcRect)
		{
			this.m_UV = new Rect(0f, 0f, 1f, 1f);
			Vector2 vector = Vector2.zero;
			Texture image = this.image;
			bool flag = image != null;
			if (flag)
			{
				vector = this.GetTextureDisplaySize(image);
			}
			VectorImage vectorImage = this.vectorImage;
			bool flag2 = vectorImage != null;
			if (flag2)
			{
				vector = vectorImage.size;
			}
			bool flag3 = vector != Vector2.zero;
			if (flag3)
			{
				this.m_UV.x = srcRect.x / vector.x;
				this.m_UV.width = srcRect.width / vector.x;
				this.m_UV.height = srcRect.height / vector.y;
				this.m_UV.y = 1f - this.m_UV.height - srcRect.y / vector.y;
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00026004 File Offset: 0x00024204
		private Rect GetSourceRect()
		{
			Rect zero = Rect.zero;
			Vector2 vector = Vector2.zero;
			Texture image = this.image;
			bool flag = image != null;
			if (flag)
			{
				vector = this.GetTextureDisplaySize(image);
			}
			VectorImage vectorImage = this.vectorImage;
			bool flag2 = vectorImage != null;
			if (flag2)
			{
				vector = vectorImage.size;
			}
			bool flag3 = vector != Vector2.zero;
			if (flag3)
			{
				zero.x = this.uv.x * vector.x;
				zero.width = this.uv.width * vector.x;
				zero.y = (1f - this.uv.y - this.uv.height) * vector.y;
				zero.height = this.uv.height * vector.y;
			}
			return zero;
		}

		// Token: 0x040004D0 RID: 1232
		internal static readonly BindingId imageProperty = "image";

		// Token: 0x040004D1 RID: 1233
		internal static readonly BindingId spriteProperty = "sprite";

		// Token: 0x040004D2 RID: 1234
		internal static readonly BindingId vectorImageProperty = "vectorImage";

		// Token: 0x040004D3 RID: 1235
		internal static readonly BindingId sourceRectProperty = "sourceRect";

		// Token: 0x040004D4 RID: 1236
		internal static readonly BindingId uvProperty = "uv";

		// Token: 0x040004D5 RID: 1237
		internal static readonly BindingId scaleModeProperty = "scaleMode";

		// Token: 0x040004D6 RID: 1238
		internal static readonly BindingId tintColorProperty = "tintColor";

		// Token: 0x040004D7 RID: 1239
		private ScaleMode m_ScaleMode;

		// Token: 0x040004D8 RID: 1240
		private Texture m_Image;

		// Token: 0x040004D9 RID: 1241
		private Sprite m_Sprite;

		// Token: 0x040004DA RID: 1242
		private VectorImage m_VectorImage;

		// Token: 0x040004DB RID: 1243
		private Rect m_UV;

		// Token: 0x040004DC RID: 1244
		private Color m_TintColor;

		// Token: 0x040004DD RID: 1245
		internal bool m_ImageIsInline;

		// Token: 0x040004DE RID: 1246
		private bool m_ScaleModeIsInline;

		// Token: 0x040004DF RID: 1247
		private bool m_TintColorIsInline;

		// Token: 0x040004E0 RID: 1248
		public static readonly string ussClassName = "unity-image";

		// Token: 0x040004E1 RID: 1249
		private static CustomStyleProperty<Texture2D> s_ImageProperty = new CustomStyleProperty<Texture2D>("--unity-image");

		// Token: 0x040004E2 RID: 1250
		private static CustomStyleProperty<Sprite> s_SpriteProperty = new CustomStyleProperty<Sprite>("--unity-image");

		// Token: 0x040004E3 RID: 1251
		private static CustomStyleProperty<VectorImage> s_VectorImageProperty = new CustomStyleProperty<VectorImage>("--unity-image");

		// Token: 0x040004E4 RID: 1252
		private static CustomStyleProperty<string> s_ScaleModeProperty = new CustomStyleProperty<string>("--unity-image-size");

		// Token: 0x040004E5 RID: 1253
		private static CustomStyleProperty<Color> s_TintColorProperty = new CustomStyleProperty<Color>("--unity-image-tint-color");

		// Token: 0x0200010E RID: 270
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x0600089F RID: 2207 RVA: 0x000261CB File Offset: 0x000243CB
			public override object CreateInstance()
			{
				return new Image();
			}
		}

		// Token: 0x0200010F RID: 271
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Image, Image.UxmlTraits>
		{
		}

		// Token: 0x02000110 RID: 272
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x1700013F RID: 319
			// (get) Token: 0x060008A2 RID: 2210 RVA: 0x000261DC File Offset: 0x000243DC
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}
		}
	}
}
