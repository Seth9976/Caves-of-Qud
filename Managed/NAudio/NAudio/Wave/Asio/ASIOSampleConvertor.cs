using System;

namespace NAudio.Wave.Asio
{
	// Token: 0x02000147 RID: 327
	internal class ASIOSampleConvertor
	{
		// Token: 0x060006FD RID: 1789 RVA: 0x00014B34 File Offset: 0x00012D34
		public static ASIOSampleConvertor.SampleConvertor SelectSampleConvertor(WaveFormat waveFormat, AsioSampleType asioType)
		{
			ASIOSampleConvertor.SampleConvertor sampleConvertor = null;
			bool flag = waveFormat.Channels == 2;
			switch (asioType)
			{
			case AsioSampleType.Int16LSB:
			{
				int bitsPerSample = waveFormat.BitsPerSample;
				if (bitsPerSample != 16)
				{
					if (bitsPerSample == 32)
					{
						sampleConvertor = (flag ? new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorFloatToShort2Channels) : new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorFloatToShortGeneric));
					}
				}
				else
				{
					sampleConvertor = (flag ? new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorShortToShort2Channels) : new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorShortToShortGeneric));
				}
				break;
			}
			case AsioSampleType.Int24LSB:
			{
				int bitsPerSample2 = waveFormat.BitsPerSample;
				if (bitsPerSample2 == 16)
				{
					throw new ArgumentException("Not a supported conversion");
				}
				if (bitsPerSample2 == 32)
				{
					sampleConvertor = new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConverterFloatTo24LSBGeneric);
				}
				break;
			}
			case AsioSampleType.Int32LSB:
			{
				int bitsPerSample3 = waveFormat.BitsPerSample;
				if (bitsPerSample3 != 16)
				{
					if (bitsPerSample3 == 32)
					{
						sampleConvertor = (flag ? new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorFloatToInt2Channels) : new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorFloatToIntGeneric));
					}
				}
				else
				{
					sampleConvertor = (flag ? new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorShortToInt2Channels) : new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorShortToIntGeneric));
				}
				break;
			}
			case AsioSampleType.Float32LSB:
			{
				int bitsPerSample4 = waveFormat.BitsPerSample;
				if (bitsPerSample4 == 16)
				{
					throw new ArgumentException("Not a supported conversion");
				}
				if (bitsPerSample4 == 32)
				{
					sampleConvertor = new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConverterFloatToFloatGeneric);
				}
				break;
			}
			default:
				throw new ArgumentException(string.Format("ASIO Buffer Type {0} is not yet supported.", Enum.GetName(typeof(AsioSampleType), asioType)));
			}
			return sampleConvertor;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00014CB0 File Offset: 0x00012EB0
		public unsafe static void ConvertorShortToInt2Channels(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
		{
			short* ptr = (short*)(void*)inputInterleavedBuffer;
			short* ptr2 = (short*)(void*)asioOutputBuffers[0];
			short* ptr3 = (short*)(void*)asioOutputBuffers[1];
			ptr2++;
			ptr3++;
			for (int i = 0; i < nbSamples; i++)
			{
				*ptr2 = *ptr;
				*ptr3 = ptr[1];
				ptr += 2;
				ptr2 += 2;
				ptr3 += 2;
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00014D18 File Offset: 0x00012F18
		public unsafe static void ConvertorShortToIntGeneric(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
		{
			short* ptr = (short*)(void*)inputInterleavedBuffer;
			short*[] array = new short*[nbChannels];
			for (int i = 0; i < nbChannels; i++)
			{
				array[i] = (short*)(void*)asioOutputBuffers[i];
				short*[] array2;
				IntPtr intPtr;
				(array2 = array)[(int)(intPtr = (IntPtr)i)] = array2[(int)intPtr] + 1;
			}
			for (int j = 0; j < nbSamples; j++)
			{
				for (int k = 0; k < nbChannels; k++)
				{
					*array[k] = *(ptr++);
					short*[] array3;
					IntPtr intPtr2;
					(array3 = array)[(int)(intPtr2 = (IntPtr)k)] = array3[(int)intPtr2] + 2;
				}
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00014DA0 File Offset: 0x00012FA0
		public unsafe static void ConvertorFloatToInt2Channels(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
		{
			float* ptr = (float*)(void*)inputInterleavedBuffer;
			int* ptr2 = (int*)(void*)asioOutputBuffers[0];
			int* ptr3 = (int*)(void*)asioOutputBuffers[1];
			for (int i = 0; i < nbSamples; i++)
			{
				*(ptr2++) = ASIOSampleConvertor.clampToInt((double)(*ptr));
				*(ptr3++) = ASIOSampleConvertor.clampToInt((double)ptr[1]);
				ptr += 2;
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00014E0C File Offset: 0x0001300C
		public unsafe static void ConvertorFloatToIntGeneric(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
		{
			float* ptr = (float*)(void*)inputInterleavedBuffer;
			int*[] array = new int*[nbChannels];
			for (int i = 0; i < nbChannels; i++)
			{
				array[i] = (int*)(void*)asioOutputBuffers[i];
			}
			for (int j = 0; j < nbSamples; j++)
			{
				for (int k = 0; k < nbChannels; k++)
				{
					int*[] array2;
					IntPtr intPtr;
					int* ptr2;
					(array2 = array)[(int)(intPtr = (IntPtr)k)] = (ptr2 = array2[(int)intPtr]) + 1;
					*ptr2 = ASIOSampleConvertor.clampToInt((double)(*(ptr++)));
				}
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00014E88 File Offset: 0x00013088
		public unsafe static void ConvertorShortToShort2Channels(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
		{
			short* ptr = (short*)(void*)inputInterleavedBuffer;
			short* ptr2 = (short*)(void*)asioOutputBuffers[0];
			short* ptr3 = (short*)(void*)asioOutputBuffers[1];
			for (int i = 0; i < nbSamples; i++)
			{
				*(ptr2++) = *ptr;
				*(ptr3++) = ptr[1];
				ptr += 2;
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00014EE8 File Offset: 0x000130E8
		public unsafe static void ConvertorShortToShortGeneric(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
		{
			short* ptr = (short*)(void*)inputInterleavedBuffer;
			short*[] array = new short*[nbChannels];
			for (int i = 0; i < nbChannels; i++)
			{
				array[i] = (short*)(void*)asioOutputBuffers[i];
			}
			for (int j = 0; j < nbSamples; j++)
			{
				for (int k = 0; k < nbChannels; k++)
				{
					short*[] array2;
					IntPtr intPtr;
					short* ptr2;
					(array2 = array)[(int)(intPtr = (IntPtr)k)] = (ptr2 = array2[(int)intPtr]) + 1;
					*ptr2 = *(ptr++);
				}
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00014F60 File Offset: 0x00013160
		public unsafe static void ConvertorFloatToShort2Channels(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
		{
			float* ptr = (float*)(void*)inputInterleavedBuffer;
			short* ptr2 = (short*)(void*)asioOutputBuffers[0];
			short* ptr3 = (short*)(void*)asioOutputBuffers[1];
			for (int i = 0; i < nbSamples; i++)
			{
				*(ptr2++) = ASIOSampleConvertor.clampToShort((double)(*ptr));
				*(ptr3++) = ASIOSampleConvertor.clampToShort((double)ptr[1]);
				ptr += 2;
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00014FCC File Offset: 0x000131CC
		public unsafe static void ConvertorFloatToShortGeneric(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
		{
			float* ptr = (float*)(void*)inputInterleavedBuffer;
			short*[] array = new short*[nbChannels];
			for (int i = 0; i < nbChannels; i++)
			{
				array[i] = (short*)(void*)asioOutputBuffers[i];
			}
			for (int j = 0; j < nbSamples; j++)
			{
				for (int k = 0; k < nbChannels; k++)
				{
					short*[] array2;
					IntPtr intPtr;
					short* ptr2;
					(array2 = array)[(int)(intPtr = (IntPtr)k)] = (ptr2 = array2[(int)intPtr]) + 1;
					*ptr2 = ASIOSampleConvertor.clampToShort((double)(*(ptr++)));
				}
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00015048 File Offset: 0x00013248
		public unsafe static void ConverterFloatTo24LSBGeneric(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
		{
			float* ptr = (float*)(void*)inputInterleavedBuffer;
			byte*[] array = new byte*[nbChannels];
			for (int i = 0; i < nbChannels; i++)
			{
				array[i] = (byte*)(void*)asioOutputBuffers[i];
			}
			for (int j = 0; j < nbSamples; j++)
			{
				for (int k = 0; k < nbChannels; k++)
				{
					int num = ASIOSampleConvertor.clampTo24Bit((double)(*(ptr++)));
					byte*[] array2;
					IntPtr intPtr;
					byte* ptr2;
					(array2 = array)[(int)(intPtr = (IntPtr)k)] = (ptr2 = array2[(int)intPtr]) + 1;
					*ptr2 = (byte)num;
					byte*[] array3;
					IntPtr intPtr2;
					byte* ptr3;
					(array3 = array)[(int)(intPtr2 = (IntPtr)k)] = (ptr3 = array3[(int)intPtr2]) + 1;
					*ptr3 = (byte)(num >> 8);
					byte*[] array4;
					IntPtr intPtr3;
					byte* ptr4;
					(array4 = array)[(int)(intPtr3 = (IntPtr)k)] = (ptr4 = array4[(int)intPtr3]) + 1;
					*ptr4 = (byte)(num >> 16);
				}
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00015104 File Offset: 0x00013304
		public unsafe static void ConverterFloatToFloatGeneric(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
		{
			float* ptr = (float*)(void*)inputInterleavedBuffer;
			float*[] array = new float*[nbChannels];
			for (int i = 0; i < nbChannels; i++)
			{
				array[i] = (float*)(void*)asioOutputBuffers[i];
			}
			for (int j = 0; j < nbSamples; j++)
			{
				for (int k = 0; k < nbChannels; k++)
				{
					float*[] array2;
					IntPtr intPtr;
					float* ptr2;
					(array2 = array)[(int)(intPtr = (IntPtr)k)] = (ptr2 = array2[(int)intPtr]) + 1;
					*ptr2 = *(ptr++);
				}
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001517A File Offset: 0x0001337A
		private static int clampTo24Bit(double sampleValue)
		{
			sampleValue = ((sampleValue < -1.0) ? (-1.0) : ((sampleValue > 1.0) ? 1.0 : sampleValue));
			return (int)(sampleValue * 8388607.0);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000151B9 File Offset: 0x000133B9
		private static int clampToInt(double sampleValue)
		{
			sampleValue = ((sampleValue < -1.0) ? (-1.0) : ((sampleValue > 1.0) ? 1.0 : sampleValue));
			return (int)(sampleValue * 2147483647.0);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000151F8 File Offset: 0x000133F8
		private static short clampToShort(double sampleValue)
		{
			sampleValue = ((sampleValue < -1.0) ? (-1.0) : ((sampleValue > 1.0) ? 1.0 : sampleValue));
			return (short)(sampleValue * 32767.0);
		}

		// Token: 0x02000148 RID: 328
		// (Invoke) Token: 0x0600070D RID: 1805
		public delegate void SampleConvertor(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples);
	}
}
