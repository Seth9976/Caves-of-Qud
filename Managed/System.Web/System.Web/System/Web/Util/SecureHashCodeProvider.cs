using System;
using System.Collections;
using System.Globalization;

namespace System.Web.Util
{
	// Token: 0x02000143 RID: 323
	internal class SecureHashCodeProvider : IHashCodeProvider
	{
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0002796C File Offset: 0x00025B6C
		public static SecureHashCodeProvider Default
		{
			get
			{
				object obj = SecureHashCodeProvider.sync;
				SecureHashCodeProvider secureHashCodeProvider;
				lock (obj)
				{
					if (SecureHashCodeProvider.singleton == null)
					{
						SecureHashCodeProvider.singleton = new SecureHashCodeProvider();
					}
					else if (SecureHashCodeProvider.singleton.m_text == null)
					{
						if (!SecureHashCodeProvider.AreEqual(CultureInfo.CurrentCulture, CultureInfo.InvariantCulture))
						{
							SecureHashCodeProvider.singleton = new SecureHashCodeProvider();
						}
					}
					else if (!SecureHashCodeProvider.AreEqual(SecureHashCodeProvider.singleton.m_text, CultureInfo.CurrentCulture))
					{
						SecureHashCodeProvider.singleton = new SecureHashCodeProvider();
					}
					secureHashCodeProvider = SecureHashCodeProvider.singleton;
				}
				return secureHashCodeProvider;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x00027A08 File Offset: 0x00025C08
		public static SecureHashCodeProvider DefaultInvariant
		{
			get
			{
				return SecureHashCodeProvider.singletonInvariant;
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x00027A49 File Offset: 0x00025C49
		public SecureHashCodeProvider()
		{
			if (!SecureHashCodeProvider.AreEqual(CultureInfo.CurrentCulture, CultureInfo.InvariantCulture))
			{
				this.m_text = CultureInfo.CurrentCulture.TextInfo;
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x00027A72 File Offset: 0x00025C72
		public SecureHashCodeProvider(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (!SecureHashCodeProvider.AreEqual(culture, CultureInfo.InvariantCulture))
			{
				this.m_text = culture.TextInfo;
			}
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00027AA1 File Offset: 0x00025CA1
		private static bool AreEqual(CultureInfo a, CultureInfo b)
		{
			return a.LCID == b.LCID;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00027AB1 File Offset: 0x00025CB1
		private static bool AreEqual(TextInfo info, CultureInfo culture)
		{
			return info.LCID == culture.LCID;
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00027AC4 File Offset: 0x00025CC4
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			if (text == null)
			{
				return obj.GetHashCode();
			}
			int num = SecureHashCodeProvider.seed;
			if (this.m_text != null && !SecureHashCodeProvider.AreEqual(this.m_text, CultureInfo.InvariantCulture))
			{
				foreach (char c in this.m_text.ToLower(text))
				{
					num = num * 31 + (int)c;
				}
			}
			else
			{
				for (int j = 0; j < text.Length; j++)
				{
					char c = char.ToLower(text[j], CultureInfo.InvariantCulture);
					num = num * 31 + (int)c;
				}
			}
			return num;
		}

		// Token: 0x04001212 RID: 4626
		private static readonly SecureHashCodeProvider singletonInvariant = new SecureHashCodeProvider(CultureInfo.InvariantCulture);

		// Token: 0x04001213 RID: 4627
		private static SecureHashCodeProvider singleton;

		// Token: 0x04001214 RID: 4628
		private static readonly object sync = new object();

		// Token: 0x04001215 RID: 4629
		private static readonly int seed = (int)DateTime.UtcNow.Ticks;

		// Token: 0x04001216 RID: 4630
		private TextInfo m_text;
	}
}
