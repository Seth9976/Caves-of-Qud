using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000215 RID: 533
	internal struct AttributeUsageInfo : IEquatable<AttributeUsageInfo>
	{
		// Token: 0x060014C5 RID: 5317 RVA: 0x00042DF0 File Offset: 0x00040FF0
		internal AttributeUsageInfo(AttributeTargets validTargets, bool allowMultiple, bool inherited)
		{
			this._flags = validTargets | 32768;
			if (allowMultiple)
			{
				this._flags |= AttributeUsageInfo.PackedAttributeUsage.AllowMultiple;
			}
			if (inherited)
			{
				this._flags |= AttributeUsageInfo.PackedAttributeUsage.Inherited;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x00042E29 File Offset: 0x00041029
		public bool IsNull
		{
			get
			{
				return (this._flags & AttributeUsageInfo.PackedAttributeUsage.Initialized) == AttributeUsageInfo.PackedAttributeUsage.None;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x00042E3A File Offset: 0x0004103A
		internal AttributeTargets ValidTargets
		{
			get
			{
				return this._flags & AttributeUsageInfo.PackedAttributeUsage.All;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x00042E48 File Offset: 0x00041048
		internal bool AllowMultiple
		{
			get
			{
				return (this._flags & AttributeUsageInfo.PackedAttributeUsage.AllowMultiple) > AttributeUsageInfo.PackedAttributeUsage.None;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x00042E59 File Offset: 0x00041059
		internal bool Inherited
		{
			get
			{
				return (this._flags & AttributeUsageInfo.PackedAttributeUsage.Inherited) > AttributeUsageInfo.PackedAttributeUsage.None;
			}
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x00042E6A File Offset: 0x0004106A
		public static bool operator ==(AttributeUsageInfo left, AttributeUsageInfo right)
		{
			return left._flags == right._flags;
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00042E7A File Offset: 0x0004107A
		public static bool operator !=(AttributeUsageInfo left, AttributeUsageInfo right)
		{
			return left._flags != right._flags;
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00042E8D File Offset: 0x0004108D
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is AttributeUsageInfo && this.Equals((AttributeUsageInfo)obj);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00042EA5 File Offset: 0x000410A5
		public bool Equals(AttributeUsageInfo other)
		{
			return this == other;
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00042EB3 File Offset: 0x000410B3
		public override int GetHashCode()
		{
			return this._flags.GetHashCode();
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x00042EC8 File Offset: 0x000410C8
		internal bool HasValidAttributeTargets
		{
			get
			{
				int validTargets = this.ValidTargets;
				return validTargets != 0 && (validTargets & -32768) == 0;
			}
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00042EEC File Offset: 0x000410EC
		[NullableContext(1)]
		internal object GetValidTargetsErrorArgument()
		{
			int i = this.ValidTargets;
			if (!this.HasValidAttributeTargets)
			{
				return string.Empty;
			}
			ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance();
			int num = 0;
			while (i > 0)
			{
				if ((i & 1) != 0)
				{
					instance.Add(AttributeUsageInfo.GetErrorDisplayNameResourceId(1 << num));
				}
				i >>= 1;
				num++;
			}
			return new AttributeUsageInfo.ValidTargetsStringLocalizableErrorArgument(instance.ToArrayAndFree());
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00042F4C File Offset: 0x0004114C
		[NullableContext(1)]
		private static string GetErrorDisplayNameResourceId(AttributeTargets target)
		{
			if (target <= 128)
			{
				if (target <= 16)
				{
					switch (target)
					{
					case 1:
						return "Assembly";
					case 2:
						return "Module";
					case 3:
						break;
					case 4:
						return "Class1";
					default:
						if (target == 8)
						{
							return "Struct1";
						}
						if (target == 16)
						{
							return "Enum1";
						}
						break;
					}
				}
				else
				{
					if (target == 32)
					{
						return "Constructor";
					}
					if (target == 64)
					{
						return "Method";
					}
					if (target == 128)
					{
						return "Property";
					}
				}
			}
			else if (target <= 1024)
			{
				if (target == 256)
				{
					return "Field";
				}
				if (target == 512)
				{
					return "Event1";
				}
				if (target == 1024)
				{
					return "Interface1";
				}
			}
			else if (target <= 4096)
			{
				if (target == 2048)
				{
					return "Parameter";
				}
				if (target == 4096)
				{
					return "Delegate1";
				}
			}
			else
			{
				if (target == 8192)
				{
					return "Return1";
				}
				if (target == 16384)
				{
					return "TypeParameter";
				}
			}
			throw ExceptionUtilities.UnexpectedValue(target);
		}

		// Token: 0x04000896 RID: 2198
		private readonly AttributeUsageInfo.PackedAttributeUsage _flags;

		// Token: 0x04000897 RID: 2199
		internal static readonly AttributeUsageInfo Default = new AttributeUsageInfo(32767, false, true);

		// Token: 0x04000898 RID: 2200
		internal static readonly AttributeUsageInfo Null = default(AttributeUsageInfo);

		// Token: 0x020005EC RID: 1516
		[Flags]
		private enum PackedAttributeUsage
		{
			// Token: 0x04001710 RID: 5904
			None = 0,
			// Token: 0x04001711 RID: 5905
			Assembly = 1,
			// Token: 0x04001712 RID: 5906
			Module = 2,
			// Token: 0x04001713 RID: 5907
			Class = 4,
			// Token: 0x04001714 RID: 5908
			Struct = 8,
			// Token: 0x04001715 RID: 5909
			Enum = 16,
			// Token: 0x04001716 RID: 5910
			Constructor = 32,
			// Token: 0x04001717 RID: 5911
			Method = 64,
			// Token: 0x04001718 RID: 5912
			Property = 128,
			// Token: 0x04001719 RID: 5913
			Field = 256,
			// Token: 0x0400171A RID: 5914
			Event = 512,
			// Token: 0x0400171B RID: 5915
			Interface = 1024,
			// Token: 0x0400171C RID: 5916
			Parameter = 2048,
			// Token: 0x0400171D RID: 5917
			Delegate = 4096,
			// Token: 0x0400171E RID: 5918
			ReturnValue = 8192,
			// Token: 0x0400171F RID: 5919
			GenericParameter = 16384,
			// Token: 0x04001720 RID: 5920
			All = 32767,
			// Token: 0x04001721 RID: 5921
			Initialized = 32768,
			// Token: 0x04001722 RID: 5922
			AllowMultiple = 65536,
			// Token: 0x04001723 RID: 5923
			Inherited = 131072
		}

		// Token: 0x020005ED RID: 1517
		private struct ValidTargetsStringLocalizableErrorArgument : IFormattable
		{
			// Token: 0x060036EB RID: 14059 RVA: 0x0009A1A5 File Offset: 0x000983A5
			[NullableContext(1)]
			internal ValidTargetsStringLocalizableErrorArgument(string[] targetResourceIds)
			{
				this._targetResourceIds = targetResourceIds;
			}

			// Token: 0x060036EC RID: 14060 RVA: 0x0009A1AE File Offset: 0x000983AE
			[NullableContext(1)]
			public override string ToString()
			{
				return this.ToString(null, null);
			}

			// Token: 0x060036ED RID: 14061 RVA: 0x0009A1B8 File Offset: 0x000983B8
			[NullableContext(2)]
			[return: Nullable(1)]
			public string ToString(string format, IFormatProvider formatProvider)
			{
				PooledStringBuilder instance = PooledStringBuilder.GetInstance();
				CultureInfo cultureInfo = formatProvider as CultureInfo;
				if (this._targetResourceIds != null)
				{
					foreach (string text in this._targetResourceIds)
					{
						if (instance.Builder.Length > 0)
						{
							instance.Builder.Append(", ");
						}
						instance.Builder.Append(CodeAnalysisResources.ResourceManager.GetString(text, cultureInfo));
					}
				}
				string text2 = instance.Builder.ToString();
				instance.Free();
				return text2;
			}

			// Token: 0x04001724 RID: 5924
			[Nullable(new byte[] { 2, 1 })]
			private readonly string[] _targetResourceIds;
		}
	}
}
