using System;

namespace Cysharp.Text
{
	// Token: 0x02000014 RID: 20
	internal static class FastNumberWriter
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00008AA8 File Offset: 0x00006CA8
		public unsafe static bool TryWriteInt64(Span<char> buffer, out int charsWritten, long value)
		{
			int num = 0;
			charsWritten = 0;
			long num2 = value;
			if (value < 0L)
			{
				if (value == -9223372036854775808L)
				{
					if (buffer.Length < 20)
					{
						return false;
					}
					*buffer[num++] = '-';
					*buffer[num++] = '9';
					*buffer[num++] = '2';
					*buffer[num++] = '2';
					*buffer[num++] = '3';
					*buffer[num++] = '3';
					*buffer[num++] = '7';
					*buffer[num++] = '2';
					*buffer[num++] = '0';
					*buffer[num++] = '3';
					*buffer[num++] = '6';
					*buffer[num++] = '8';
					*buffer[num++] = '5';
					*buffer[num++] = '4';
					*buffer[num++] = '7';
					*buffer[num++] = '7';
					*buffer[num++] = '5';
					*buffer[num++] = '8';
					*buffer[num++] = '0';
					*buffer[num++] = '8';
					charsWritten = num;
					return true;
				}
				else
				{
					if (buffer.Length < 1)
					{
						return false;
					}
					*buffer[num++] = '-';
					num2 = -value;
				}
			}
			long num7;
			if (num2 < 10000L)
			{
				if (num2 < 10L)
				{
					if (buffer.Length < 1)
					{
						return false;
					}
					goto IL_0677;
				}
				else if (num2 < 100L)
				{
					if (buffer.Length < 2)
					{
						return false;
					}
					goto IL_064E;
				}
				else if (num2 < 1000L)
				{
					if (buffer.Length < 3)
					{
						return false;
					}
					goto IL_0625;
				}
				else if (buffer.Length < 4)
				{
					return false;
				}
			}
			else
			{
				long num3 = num2 / 10000L;
				num2 -= num3 * 10000L;
				if (num3 < 10000L)
				{
					if (num3 < 10L)
					{
						if (buffer.Length < 5)
						{
							return false;
						}
						goto IL_05E6;
					}
					else if (num3 < 100L)
					{
						if (buffer.Length < 6)
						{
							return false;
						}
						goto IL_05BD;
					}
					else if (num3 < 1000L)
					{
						if (buffer.Length < 7)
						{
							return false;
						}
						goto IL_0594;
					}
					else if (buffer.Length < 8)
					{
						return false;
					}
				}
				else
				{
					long num4 = num3 / 10000L;
					num3 -= num4 * 10000L;
					if (num4 < 10000L)
					{
						if (num4 < 10L)
						{
							if (buffer.Length < 9)
							{
								return false;
							}
							goto IL_0555;
						}
						else if (num4 < 100L)
						{
							if (buffer.Length < 10)
							{
								return false;
							}
							goto IL_052C;
						}
						else if (num4 < 1000L)
						{
							if (buffer.Length < 11)
							{
								return false;
							}
							goto IL_0503;
						}
						else if (buffer.Length < 12)
						{
							return false;
						}
					}
					else
					{
						long num5 = num4 / 10000L;
						num4 -= num5 * 10000L;
						if (num5 < 10000L)
						{
							if (num5 < 10L)
							{
								if (buffer.Length < 13)
								{
									return false;
								}
								goto IL_04C3;
							}
							else if (num5 < 100L)
							{
								if (buffer.Length < 14)
								{
									return false;
								}
								goto IL_0497;
							}
							else if (num5 < 1000L)
							{
								if (buffer.Length < 15)
								{
									return false;
								}
								goto IL_046B;
							}
							else if (buffer.Length < 16)
							{
								return false;
							}
						}
						else
						{
							long num6 = num5 / 10000L;
							num5 -= num6 * 10000L;
							if (num6 < 10000L)
							{
								if (num6 < 10L)
								{
									if (buffer.Length < 17)
									{
										return false;
									}
									goto IL_0428;
								}
								else if (num6 < 100L)
								{
									if (buffer.Length < 18)
									{
										return false;
									}
									goto IL_03FC;
								}
								else if (num6 < 1000L)
								{
									if (buffer.Length < 19)
									{
										return false;
									}
									goto IL_03D0;
								}
								else if (buffer.Length < 20)
								{
									return false;
								}
							}
							*buffer[num++] = (char)(48L + (num7 = num6 * 8389L >> 23));
							num6 -= num7 * 1000L;
							IL_03D0:
							*buffer[num++] = (char)(48L + (num7 = num6 * 5243L >> 19));
							num6 -= num7 * 100L;
							IL_03FC:
							*buffer[num++] = (char)(48L + (num7 = num6 * 6554L >> 16));
							num6 -= num7 * 10L;
							IL_0428:
							*buffer[num++] = (char)(48L + num6);
						}
						*buffer[num++] = (char)(48L + (num7 = num5 * 8389L >> 23));
						num5 -= num7 * 1000L;
						IL_046B:
						*buffer[num++] = (char)(48L + (num7 = num5 * 5243L >> 19));
						num5 -= num7 * 100L;
						IL_0497:
						*buffer[num++] = (char)(48L + (num7 = num5 * 6554L >> 16));
						num5 -= num7 * 10L;
						IL_04C3:
						*buffer[num++] = (char)(48L + num5);
					}
					*buffer[num++] = (char)(48L + (num7 = num4 * 8389L >> 23));
					num4 -= num7 * 1000L;
					IL_0503:
					*buffer[num++] = (char)(48L + (num7 = num4 * 5243L >> 19));
					num4 -= num7 * 100L;
					IL_052C:
					*buffer[num++] = (char)(48L + (num7 = num4 * 6554L >> 16));
					num4 -= num7 * 10L;
					IL_0555:
					*buffer[num++] = (char)(48L + num4);
				}
				*buffer[num++] = (char)(48L + (num7 = num3 * 8389L >> 23));
				num3 -= num7 * 1000L;
				IL_0594:
				*buffer[num++] = (char)(48L + (num7 = num3 * 5243L >> 19));
				num3 -= num7 * 100L;
				IL_05BD:
				*buffer[num++] = (char)(48L + (num7 = num3 * 6554L >> 16));
				num3 -= num7 * 10L;
				IL_05E6:
				*buffer[num++] = (char)(48L + num3);
			}
			*buffer[num++] = (char)(48L + (num7 = num2 * 8389L >> 23));
			num2 -= num7 * 1000L;
			IL_0625:
			*buffer[num++] = (char)(48L + (num7 = num2 * 5243L >> 19));
			num2 -= num7 * 100L;
			IL_064E:
			*buffer[num++] = (char)(48L + (num7 = num2 * 6554L >> 16));
			num2 -= num7 * 10L;
			IL_0677:
			*buffer[num++] = (char)(48L + num2);
			charsWritten = num;
			return true;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00009144 File Offset: 0x00007344
		public unsafe static bool TryWriteUInt64(Span<char> buffer, out int charsWritten, ulong value)
		{
			ulong num = value;
			charsWritten = 0;
			int num2 = 0;
			ulong num7;
			if (num < 10000UL)
			{
				if (num < 10UL)
				{
					if (buffer.Length < 1)
					{
						return false;
					}
					goto IL_0518;
				}
				else if (num < 100UL)
				{
					if (buffer.Length < 2)
					{
						return false;
					}
					goto IL_04ED;
				}
				else if (num < 1000UL)
				{
					if (buffer.Length < 3)
					{
						return false;
					}
					goto IL_04C2;
				}
				else if (buffer.Length < 4)
				{
					return false;
				}
			}
			else
			{
				ulong num3 = num / 10000UL;
				num -= num3 * 10000UL;
				if (num3 < 10000UL)
				{
					if (num3 < 10UL)
					{
						if (buffer.Length < 5)
						{
							return false;
						}
						goto IL_047F;
					}
					else if (num3 < 100UL)
					{
						if (buffer.Length < 6)
						{
							return false;
						}
						goto IL_0454;
					}
					else if (num3 < 1000UL)
					{
						if (buffer.Length < 7)
						{
							return false;
						}
						goto IL_0429;
					}
					else if (buffer.Length < 8)
					{
						return false;
					}
				}
				else
				{
					ulong num4 = num3 / 10000UL;
					num3 -= num4 * 10000UL;
					if (num4 < 10000UL)
					{
						if (num4 < 10UL)
						{
							if (buffer.Length < 9)
							{
								return false;
							}
							goto IL_03E6;
						}
						else if (num4 < 100UL)
						{
							if (buffer.Length < 10)
							{
								return false;
							}
							goto IL_03BB;
						}
						else if (num4 < 1000UL)
						{
							if (buffer.Length < 11)
							{
								return false;
							}
							goto IL_0390;
						}
						else if (buffer.Length < 12)
						{
							return false;
						}
					}
					else
					{
						ulong num5 = num4 / 10000UL;
						num4 -= num5 * 10000UL;
						if (num5 < 10000UL)
						{
							if (num5 < 10UL)
							{
								if (buffer.Length < 13)
								{
									return false;
								}
								goto IL_034D;
							}
							else if (num5 < 100UL)
							{
								if (buffer.Length < 14)
								{
									return false;
								}
								goto IL_0322;
							}
							else if (num5 < 1000UL)
							{
								if (buffer.Length < 15)
								{
									return false;
								}
								goto IL_02F7;
							}
							else if (buffer.Length < 16)
							{
								return false;
							}
						}
						else
						{
							ulong num6 = num5 / 10000UL;
							num5 -= num6 * 10000UL;
							if (num6 < 10000UL)
							{
								if (num6 < 10UL)
								{
									if (buffer.Length < 17)
									{
										return false;
									}
									goto IL_02B3;
								}
								else if (num6 < 100UL)
								{
									if (buffer.Length < 18)
									{
										return false;
									}
									goto IL_0285;
								}
								else if (num6 < 1000UL)
								{
									if (buffer.Length < 19)
									{
										return false;
									}
									goto IL_0257;
								}
								else if (buffer.Length < 20)
								{
									return false;
								}
							}
							*buffer[num2++] = (char)(48UL + (num7 = num6 * 8389UL >> 23));
							num6 -= num7 * 1000UL;
							IL_0257:
							*buffer[num2++] = (char)(48UL + (num7 = num6 * 5243UL >> 19));
							num6 -= num7 * 100UL;
							IL_0285:
							*buffer[num2++] = (char)(48UL + (num7 = num6 * 6554UL >> 16));
							num6 -= num7 * 10UL;
							IL_02B3:
							*buffer[num2++] = (char)(48UL + num6);
						}
						*buffer[num2++] = (char)(48UL + (num7 = num5 * 8389UL >> 23));
						num5 -= num7 * 1000UL;
						IL_02F7:
						*buffer[num2++] = (char)(48UL + (num7 = num5 * 5243UL >> 19));
						num5 -= num7 * 100UL;
						IL_0322:
						*buffer[num2++] = (char)(48UL + (num7 = num5 * 6554UL >> 16));
						num5 -= num7 * 10UL;
						IL_034D:
						*buffer[num2++] = (char)(48UL + num5);
					}
					*buffer[num2++] = (char)(48UL + (num7 = num4 * 8389UL >> 23));
					num4 -= num7 * 1000UL;
					IL_0390:
					*buffer[num2++] = (char)(48UL + (num7 = num4 * 5243UL >> 19));
					num4 -= num7 * 100UL;
					IL_03BB:
					*buffer[num2++] = (char)(48UL + (num7 = num4 * 6554UL >> 16));
					num4 -= num7 * 10UL;
					IL_03E6:
					*buffer[num2++] = (char)(48UL + num4);
				}
				*buffer[num2++] = (char)(48UL + (num7 = num3 * 8389UL >> 23));
				num3 -= num7 * 1000UL;
				IL_0429:
				*buffer[num2++] = (char)(48UL + (num7 = num3 * 5243UL >> 19));
				num3 -= num7 * 100UL;
				IL_0454:
				*buffer[num2++] = (char)(48UL + (num7 = num3 * 6554UL >> 16));
				num3 -= num7 * 10UL;
				IL_047F:
				*buffer[num2++] = (char)(48UL + num3);
			}
			*buffer[num2++] = (char)(48UL + (num7 = num * 8389UL >> 23));
			num -= num7 * 1000UL;
			IL_04C2:
			*buffer[num2++] = (char)(48UL + (num7 = num * 5243UL >> 19));
			num -= num7 * 100UL;
			IL_04ED:
			*buffer[num2++] = (char)(48UL + (num7 = num * 6554UL >> 16));
			num -= num7 * 10UL;
			IL_0518:
			*buffer[num2++] = (char)(48UL + num);
			charsWritten = num2;
			return true;
		}
	}
}
