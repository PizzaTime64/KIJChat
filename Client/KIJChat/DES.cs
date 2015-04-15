using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KIJChat
{
    static class RandomUtil
    {
        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path.Substring(0, 8);
        }
    }
    class DES
    {
        /*static void Main(string[] args)
        {
            //Console.WriteLine(EncryptDES("sayasaya", "halohalo"));

            string plaintext = "tes123";
            string iv = "aaaabbbb";
            string key = "bbbbaaaa";

            Console.WriteLine("Plaintext : " + plaintext);
            Console.WriteLine("IV : " + iv);
            Console.WriteLine("key : " + key);

            Console.WriteLine("Plaintext : ");
            printba(strToBit(plaintext));

            BitArray chipertext = ofbDES(strToBit(iv), strToBit(key), strToBit(plaintext));
            Console.WriteLine("Chipertext : ");
            printba(chipertext);

            BitArray ptext = ofbDES(strToBit(iv), strToBit(key), chipertext);
            Console.WriteLine("Ptext : ");
            printba(ptext);

            //string b = ofbDES("lalalala", "lalalala", "???z?z?z");
            //Console.WriteLine(b);

            Console.ReadLine();

            
        }
        */
        public static BitArray ofbDES(BitArray iv, BitArray key, BitArray text)
        {
            List<bool> lb = new List<bool>();
            List<BitArray> hasil = new List<BitArray>();
            for(int i=0;i<text.Length;i++)lb.Add(text[i]);
            
            //padding
            if (lb.Count % 64 != 0)
            {
                while (lb.Count % 64 != 0)
                {
                    lb.Add(false);
                }
            }

            int it=0;
            List<List<bool>> lb64 = new List<List<bool>>();
            for (int i = 0; i < lb.Count/64; i++)
            {
                List<bool> lbtmp = new List<bool>();
                for (int j = i * 64; j < i * 64 + 64; j++)
                {
                    lbtmp.Add(lb[it]);
                    it++;
                }
                lb64.Add(lbtmp);
            }

            BitArray output = EncryptDES(iv, key);
            foreach (List<bool> p in lb64)
            {
                BitArray hasilXor = new BitArray(64);
                for (int i = 0; i < output.Length; i++)
                {
                    hasilXor[i] = output[i] ^ p[i];
                }

                hasil.Add(hasilXor);

                output = EncryptDES(output, key);
            }

            BitArray final = new BitArray(lb.Count);
            it = 0;
            foreach (BitArray ba in hasil)
            {
                for (int i = 0; i < ba.Length; i++)
                {
                    final[it] = ba[i];
                    it++;
                }
            }
            return final;
        }

        static BitArray EncryptDES(BitArray plaintext, BitArray key)
        {
            BitArray baPlaintext = plaintext;
            BitArray baKey = key;
            BitArray left = new BitArray(32);
            BitArray right = new BitArray(32);
            BitArray baChiperText = new BitArray(64);

            // Console.Write("plaintext : \t");
            //printba(baPlaintext);

            //Console.Write("key : \t\t");
            //printba(baKey);

            baPlaintext = initialPermutation(baPlaintext);
            BitArray baKeyPC1 = permutedChoice1(baKey);

            //Console.Write("IP : \t\t");
            //printba(baPlaintext);

            //Console.Write("keyPC1 : \t");
            //printba(baKeyPC1);

            //Console.WriteLine();

            for (int i = 0; i < 32; i++)
            {
                left[i] = baPlaintext[i];
                right[i] = baPlaintext[i + 32];
            }

            for (int r = 1; r <= 16; r++)
            {
                BitArray afterEP = expansionPermutation(right);

                BitArray keyshifted = shiftLeft(baKeyPC1, r);
                baKeyPC1 = keyshifted;


                BitArray keyAfterPC2 = permutedChoice2(keyshifted);

                BitArray afterSubtitution = subtitution(afterEP, keyAfterPC2);

                BitArray afterP = permutation(afterSubtitution);

                BitArray afterXor = new BitArray(32);
                for (int i = 0; i < 32; i++)
                {
                    afterXor[i] = afterP[i] ^ left[i];
                }

                left = right;
                right = afterXor;

                for (int i = 0; i < 32; i++)
                {
                    baChiperText[i] = left[i];
                    baChiperText[i + 32] = right[i];
                }

                //Console.Write("round "+r+": \t");
                //printba(baChiperText);
                //Console.WriteLine();
            }


            baChiperText = finalPermutation(baChiperText);
            //Console.Write("encrypt : \t");
            //printba(baChiperText);

            return baChiperText;
        }

        //in 64, out 64 (benar)
        static BitArray initialPermutation(BitArray M)
        {
            BitArray dataawal = M;
            BitArray datahasil = new BitArray(64);
            int[] IP = new int[64] {58, 50, 42, 34, 26, 18, 10, 2,
                                    60, 52, 44, 36, 28, 20, 12, 4,
                                    62, 54, 46, 38, 30, 22, 14, 6,
                                    64, 56, 48, 40, 32, 24, 16, 8,
                                    57, 49, 41, 33, 25, 17, 9, 1,
                                    59, 51, 43, 35, 27, 19, 11, 3,
                                    61, 53, 45, 37, 29, 21, 13, 5,
                                    63, 55, 47, 39, 31, 23, 15, 7};

            for (int i = 1; i <= IP.Length; i++)
            {
                datahasil[i - 1] = dataawal[IP[i - 1] - 1];
            }
            
            return datahasil;
        }

        //in 32, out 48 (benar)
        static BitArray expansionPermutation(BitArray E)
        {
            BitArray dataawal = E;
            BitArray datahasil = new BitArray(48);
            int[] IE = new int[48] {32,1,2,3,4,5,
                                    4,5,6,7,8,9,
                                    8,9,10,11,12,13,
                                    12,13,14,15,16,17,
                                    16,17,18,19,20,21,
                                    20,21,22,23,24,25,
                                    24,25,26,27,28,29,
                                    28,29,30,31,32,1 };

            for (int i = 1; i <= IE.Length; i++)
            {
                datahasil[i - 1] = dataawal[IE[i - 1]-1];
            }
            return datahasil;
        }

        //in 48, out 32
        static BitArray subtitution(BitArray D, BitArray K)
        {
            BitArray hasilxor = new BitArray(48);
            BitArray data = D;
            BitArray key = K;
            int[] hasilSboxInt = new int[8];
            BitArray toInt = new BitArray(2);
            int baris, kolom;
            //nge-xor
            for (int i = 0; i < 48; i++)
            {
                hasilxor[i] = data[i] ^ key[i];
            }

            ///subtitution

            //0-5
            int[,] s1 = new int[4,16]{
            {14,4,13,1,2,15,11,8,3,10,6,12,5,9,0,7},
            {0,15,7,4,14,2,13,1,10,6,12,11,9,5,3,8},
            {4,1,14,8,13,6,2,11,15,12,9,7,3,10,5,0},
            {15,12,8,2,4,9,1,7,5,11,3,14,10,0,6,13}
            };
            kolom = bitToInt(hasilxor, 1, 4);
            toInt[0] = hasilxor[0]; toInt[1] = hasilxor[5];
            baris = bitToInt(toInt, 0, 2);

            hasilSboxInt[0] = s1[baris, kolom];

            //6-11
            int[,] s2 = new int[4, 16] {
            {15,1,8,14,6,11,3,4,9,7,2,13,12,0,5,10},
            {3,13,4,7,15,2,8,14,12,0,1,10,6,9,11,5},
            {0,14,7,11,10,4,13,1,5,8,12,6,9,3,2,15},
            {13,8,10,1,3,15,4,2,11,6,7,12,0,5,14,9}
            };
            kolom = bitToInt(hasilxor, 7, 4);
            toInt[0] = hasilxor[6]; toInt[1] = hasilxor[11];
            baris = bitToInt(toInt, 0, 2);

            hasilSboxInt[1] = s2[baris, kolom];

            //12-17
            int[,] s3 = new int[4, 16] {
            {10,0,9,14,6,3,15,5,1,13,12,7,11,4,2,8},
            {13,7,0,9,3,4,6,10,2,8,5,14,12,11,15,1},
            {13,6,4,9,8,15,3,0,11,1,2,12,5,10,14,7},
            {1,10,13,0,6,9,8,7,4,15,14,3,11,5,2,12} 
            };
            kolom = bitToInt(hasilxor, 13, 4);
            toInt[0] = hasilxor[12]; toInt[1] = hasilxor[17];
            baris = bitToInt(toInt, 0, 2);

            hasilSboxInt[2] = s3[baris, kolom];

            //18-23
            int[,] s4 = new int[4, 16] {
            {7,13,14,3,0,6,9,10,1,2,8,5,11,12,4,15},
            {13,8,11,5,6,15,0,3,4,7,2,12,1,10,14,9},
            {10,6,9,0,12,11,7,13,15,1,3,14,5,2,8,4},
            {3,15,0,6,10,1,13,8,9,4,5,11,12,7,2,14}
            };
            kolom = bitToInt(hasilxor, 19, 4);
            toInt[0] = hasilxor[18]; toInt[1] = hasilxor[23];
            baris = bitToInt(toInt, 0, 2);

            hasilSboxInt[3] = s4[baris, kolom];

            //24-29
            int[,] s5 = new int[4, 16]{ 
            {2,12,4,1,7,10,11,6,8,5,3,15,13,0,14,9},
            {14,11,2,12,4,7,13,1,5,0,15,10,3,9,8,6},
            {4,2,1,11,10,13,7,8,15,9,12,5,6,3,0,14},
            {11,8,12,7,1,14,2,13,6,15,0,9,10,4,5,3} 
            };
            kolom = bitToInt(hasilxor, 25, 4);
            toInt[0] = hasilxor[24]; toInt[1] = hasilxor[29];
            baris = bitToInt(toInt, 0, 2);

            hasilSboxInt[4] = s5[baris, kolom];


            //30-35
            int[,] s6 = new int[4, 16]{ 
            {12,1,10,15,9,2,6,8,0,13,3,4,14,7,5,11},
            {10,15,4,2,7,12,9,5,6,1,13,14,0,11,3,8},
            {9,14,15,5,2,8,12,3,7,0,4,10,1,13,11,6},
            {4,3,2,12,9,5,15,10,11,14,1,7,6,0,8,13} };
            kolom = bitToInt(hasilxor, 31, 4);
            toInt[0] = hasilxor[30]; toInt[1] = hasilxor[35];
            baris = bitToInt(toInt, 0, 2);

            hasilSboxInt[5] = s6[baris, kolom];

            //36-41
            int[,] s7 = new int[4, 16] 
            {{4,11,2,14,15,0,8,13,3,12,9,7,5,10,6,1},
            {13,0,11,7,4,9,1,10,14,3,5,12,2,15,8,6},
            {1,4,11,13,12,3,7,14,10,15,6,8,0,5,9,2},
            {6,11,13,8,1,4,10,7,9,5,0,15,14,2,3,12 }};
            kolom = bitToInt(hasilxor, 37, 4);
            toInt[0] = hasilxor[36]; toInt[1] = hasilxor[41];
            baris = bitToInt(toInt, 0, 2);

            hasilSboxInt[6] = s7[baris, kolom];

            //42-47
            int[,] s8 = new int[4, 16]{
            {13,2,8,4,6,15,11,1,10,9,3,14,5,0,12,7},
            { 1,15,13,8,10,3,7,4,12,5,6,11,0,14,9,2},
            { 7,11,4,1,9,12,14,2,0,6,10,13,15,3,5,8},
            { 2,1,14,7,4,10,8,13,15,12,9,0,3,5,6,11} 
            };
            kolom = bitToInt(hasilxor, 43, 4);
            toInt[0] = hasilxor[42]; toInt[1] = hasilxor[47];
            baris = bitToInt(toInt, 0, 2);

            hasilSboxInt[7] = s8[baris, kolom];

            
            return new BitArray(new int[] { hasilSboxInt[0],hasilSboxInt[1],hasilSboxInt[2],hasilSboxInt[3],hasilSboxInt[4],hasilSboxInt[5],hasilSboxInt[6],hasilSboxInt[7] });
        }

        //in 32, out 32
        static BitArray permutation(BitArray D)
        {
            BitArray dataawal = D;
            BitArray datahasil = new BitArray(32);
            int[] P = new int[32]{  16,7,20,21,29,12,28,17,
                                    1,15,23,26,5,18,31,10,
                                    2,8,24,14,32,27,3,9,
                                    19,13,30,6,22,11,4,25};

            for (int i = 1; i <= P.Length; i++)
            {
                datahasil[i-1] = dataawal[P[i-1]-1];
            }

            return datahasil;
        }

        //in 64, out 64
        static BitArray finalPermutation(BitArray D)
        {
            BitArray dataawal = D;
            BitArray datahasil = new BitArray(64);
            int[] P = new int[64]{  40,8,48,16,56,24,64,32,
                                    39,7,47,15,55,23,63,31,
                                    38,6,46,14,54,22,62,30,
                                    37,5,45,13,53,21,61,29,
                                    36,4,44,12,52,20,60,28,
                                    35,3,43,11,51,19,59,27,
                                    34,2,42,10,50,18,58,26,
                                    33,1,41,9,49,17,57,25};

            for (int i = 1; i <= P.Length; i++)
            {
                datahasil[i - 1] = dataawal[P[i - 1] - 1];
            }

            return datahasil;
        }

        //key gen
        static BitArray permutedChoice1(BitArray K)
        {
            BitArray dataawal = K;
            BitArray hasilpc1 = new BitArray(56);
            BitArray left = new BitArray(28);
            BitArray right = new BitArray(28);

            int[] PC1 = new int[56]{
                57,49,41,33,25,17,9,
                1,58,50,42,34,26,18,
                10,2,59,51,43,35,27,
                19,11,3,60,52,44,36,
                63,55,47,39,31,23,15,
                7,62,54,46,38,30,22,
                14,6,61,53,45,37,29,
                21,13,5,28,20,12,4
            };
            for (int i = 1; i <= PC1.Length; i++)
            {
                hasilpc1[i - 1] = dataawal[PC1[i - 1] - 1];
            }

            return hasilpc1;
        }


        static BitArray shiftLeft(BitArray K, int round)
        {
            BitArray hasilpc1 = K;
            BitArray left = new BitArray(28);
            BitArray right = new BitArray(28);
            for (int i = 0; i < 28; i++) left[i] = hasilpc1[i];
            for (int i = 28; i < 56; i++) right[i - 28] = hasilpc1[i];

            if (round == 1 || round == 2 || round == 9 || round == 16)
            {
                bool tmp = left[0];
                for (int i = 0; i < 27; i++)
                {
                    left[i] = left[i + 1];
                }
                left[27] = tmp;

                tmp = right[0];
                for (int i = 0; i < 27; i++)
                {
                    right[i] = right[i + 1];
                }
                right[27] = tmp;
            }
            else if (round == 3 || round == 4 || round == 5 || round == 6 || round == 7 || round == 8 || round == 10 || round == 11 || round == 12 || round == 13 || round == 14 || round == 15)
            {
                bool tmp1 = left[0];
                bool tmp2 = left[1];
                for (int i = 0; i < 26; i++)
                {
                    left[i] = left[i + 2];
                }
                left[26] = tmp1;
                left[27] = tmp2;

                tmp1 = right[0];
                tmp2 = right[1];
                for (int i = 0; i < 26; i++)
                {
                    right[i] = right[i + 2];
                }
                right[26] = tmp1;
                right[27] = tmp2;
            }

            BitArray shifted = new BitArray(56);
            for (int i = 0; i < 28; i++)
            {
                shifted[i] = left[i];
                shifted[i + 28] = right[i];
            }

            return shifted;
        }

        static BitArray permutedChoice2(BitArray K)
        {
            BitArray dataawal = K;
            BitArray datahasil = new BitArray(48);
            int[] PC2 = new int[48]{  14,17,11,24,1,5,3,28,
                                    15,6,21,10,23,19,12,4,
                                    26,8,16,7,27,20,13,2,
                                    41,52,31,37,47,55,30,40,
                                    51,45,33,48,44,49,39,56,
                                    34,53,46,42,50,36,29,32};

            for (int i = 1; i <= PC2.Length; i++)
            {
                datahasil[i - 1] = dataawal[PC2[i - 1] - 1];
            }

            return datahasil;
        }


        //fungsi pendukung
        //pasti bener
        public static BitArray strToBit(string text)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(text);
            BitArray ba = new BitArray(text.Length * 8);
            int iba = 0;
            for(int i=0;i<asciiBytes.Length;i++)
            {
                BitArray lapanbit = new BitArray(8);
                
                int it=7;
                while (asciiBytes[i] > 0 )
                {
                  int bit    = asciiBytes[i] % 2 ;
                  asciiBytes[i] = (byte)(asciiBytes[i] / 2) ;
                  if (bit == 1) lapanbit[it] = true;
                  else if (bit == 0) lapanbit[it] = false;
                  it--;
                }

                for (int j = 0; j < 8; j++)
                {
                    ba[iba] = lapanbit[j];
                    iba++;
                }
            }

            return ba;
        }

        public static string bitToStr(BitArray bit)
        {
            string data = "";
            for (int i = 0; i < bit.Length; i++)
            {
                if (bit[i] == true) data += "1";
                else if (bit[i] == false) data += "0";
            }
            
            
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }

            return Encoding.ASCII.GetString(byteList.ToArray());
            /*
            byte[] bytHasil = new byte[1000001];
            byte[] bytes = new byte[bit.Length];
            bit.CopyTo(bytes, 0);
            return System.Text.Encoding.ASCII.GetString(bytes);
             */
        }

        public static int bitToInt(BitArray bit, int startIndex, int length)
        {
            int hasil = 0;
            for (int i = length - startIndex - 1, num = 0; i >= startIndex; i--, num++)
            {
                int d;
                if (bit[i]) d = 1;
                else d = 0;
                hasil = hasil + (int)(Math.Pow(2, num) * d);
                //Console.WriteLine(hasil);
            }
            return hasil;
        }

        public static void printba(BitArray bit)
        {
            int i = 1;
            foreach (bool a in bit)
            {
                if (a) Console.Write("1");
                else Console.Write("0");
                if (i % 8 == 0) Console.Write(" ");
                i++;
                
            }
            Console.WriteLine();
        }

        public static BitArray bitStringToBitArray(string bit)
        {
            char[] bitChar = bit.ToArray();
            BitArray ba = new BitArray(bit.Length);
            for(int i=0;i<bitChar.Length;i++){
                if (bitChar[i] == '1') ba[i] = true;
                else if (bitChar[i] == 0) ba[i] = false;
            }
            return ba;
        }

        public static string bitToHexa(BitArray bit)
        {
            string hasil = "";
            for(int i=0;i<bit.Length/4;i++)
            {
                if(bit[i*4]== false && bit[i*4+1]==false && bit[i*4+2]==false && bit[i*4+3]==false)//0
                {
                    hasil = hasil + "0";
                }
                else if(bit[i*4]== false && bit[i*4+1]==false && bit[i*4+2]==false && bit[i*4+3]==true)//1
                {
                    hasil = hasil + "1";
                }
                else if(bit[i*4]== false && bit[i*4+1]==false && bit[i*4+2]==true && bit[i*4+3]==false)//2
                {
                    hasil = hasil + "2";
                }
                else if(bit[i*4]== false && bit[i*4+1]==false && bit[i*4+2]==true && bit[i*4+3]==true)//3
                {
                    hasil = hasil + "3";
                }
                else if(bit[i*4]== false && bit[i*4+1]==true && bit[i*4+2]==false && bit[i*4+3]==false)//4
                {
                    hasil = hasil + "4";
                }
                else if(bit[i*4]== false && bit[i*4+1]==true && bit[i*4+2]==false && bit[i*4+3]==true)//5
                {
                    hasil = hasil + "5";
                }
                else if(bit[i*4]== false && bit[i*4+1]==true && bit[i*4+2]==true && bit[i*4+3]==false)//6
                {
                    hasil = hasil + "6";
                }
                else if(bit[i*4]== false && bit[i*4+1]==true && bit[i*4+2]==true && bit[i*4+3]==true)//7
                {
                    hasil = hasil + "7";
                }
                else if(bit[i*4]== true && bit[i*4+1]==false && bit[i*4+2]==false && bit[i*4+3]==false)//8
                {
                    hasil = hasil + "8";
                }
                else if(bit[i*4]== true && bit[i*4+1]==false && bit[i*4+2]==false && bit[i*4+3]==true)//9
                {
                    hasil = hasil + "9";
                }
                else if(bit[i*4]== true && bit[i*4+1]==false && bit[i*4+2]==true && bit[i*4+3]==false)//A
                {
                    hasil = hasil + "A";
                }
                else if(bit[i*4]== true && bit[i*4+1]==false && bit[i*4+2]==true && bit[i*4+3]==true)//B
                {
                    hasil = hasil + "B";
                }
                else if(bit[i*4]== true && bit[i*4+1]==true && bit[i*4+2]==false && bit[i*4+3]==false)//C
                {
                    hasil = hasil + "C";
                }
                else if(bit[i*4]== true && bit[i*4+1]==true && bit[i*4+2]==false && bit[i*4+3]==true)//D
                {
                    hasil = hasil + "D";
                }
                else if(bit[i*4]== true && bit[i*4+1]==true && bit[i*4+2]==true && bit[i*4+3]==false)//E
                {
                    hasil = hasil + "E";
                }
                else if(bit[i*4]== true && bit[i*4+1]==true && bit[i*4+2]==true && bit[i*4+3]==false)//F
                {
                    hasil = hasil + "F";
                }
            }
            return hasil;
        }

        public static BitArray hexToBit(string hex)
        {
            BitArray hasil = new BitArray(hex.Length * 4);
            char[] hexchar = hex.ToArray();
            for (int i = 0; i < hex.Length; i++)
            {
                if (hexchar[i] == '0')//0
                {
                    hasil[i * 4] = false;
                    hasil[i * 4 + 1] = false;
                    hasil[i * 4 + 2] = false;
                    hasil[i * 4 + 3] = false;
                }
                else if (hexchar[i] == '1')//1
                {
                    hasil[i * 4] = false;
                    hasil[i * 4 + 1] = false;
                    hasil[i * 4 + 2] = false;
                    hasil[i * 4 + 3] = true;
                }
                else if (hexchar[i] == '2')//2
                {
                    hasil[i * 4] = false ;
                    hasil[i * 4 + 1] = false; 
                    hasil[i * 4 + 2] = true ;
                    hasil[i * 4 + 3] = false;
                }
                else if (hexchar[i] == '3')//3
                {
                    hasil[i * 4] = false;
                    hasil[i * 4 + 1] = false;
                    hasil[i * 4 + 2] = true;
                    hasil[i * 4 + 3] = true;
                }
                else if (hexchar[i] == '4')//4
                {
                    hasil[i * 4] = false;
                    hasil[i * 4 + 1] = true;
                    hasil[i * 4 + 2] = false;
                    hasil[i * 4 + 3] = false;
                }
                else if (hexchar[i] == '5')//5
                {
                    hasil[i * 4] = false;
                    hasil[i * 4 + 1] = true;
                    hasil[i * 4 + 2] = false;
                    hasil[i * 4 + 3] = true;
                }
                else if (hexchar[i] == '6')//6
                {
                    hasil[i * 4] = false;
                    hasil[i * 4 + 1] = true;
                    hasil[i * 4 + 2] = true;
                    hasil[i * 4 + 3] = false;
                }
                else if (hexchar[i] == '7')//7
                {
                    hasil[i * 4] = false;
                    hasil[i * 4 + 1] = true;
                    hasil[i * 4 + 2] = true;
                    hasil[i * 4 + 3] = true;
                }
                else if (hexchar[i] == '8')//8
                {
                    hasil[i * 4] = true;
                    hasil[i * 4 + 1] = false;
                    hasil[i * 4 + 2] = false;
                    hasil[i * 4 + 3] = false;
                }
                else if (hexchar[i] == '9')//9
                {
                    hasil[i * 4] = true;
                    hasil[i * 4 + 1] = false;
                    hasil[i * 4 + 2] = false;
                    hasil[i * 4 + 3] = true;
                }
                else if (hexchar[i] == 'A')//A
                {
                    hasil[i * 4] = true;
                    hasil[i * 4 + 1] = false;
                    hasil[i * 4 + 2] = true;
                    hasil[i * 4 + 3] = false;
                }
                else if (hexchar[i] == 'B')//B
                {
                    hasil[i * 4] = true;
                    hasil[i * 4 + 1] = false;
                    hasil[i * 4 + 2] = true;
                    hasil[i * 4 + 3] = true;
                }
                else if (hexchar[i] == 'C')//C
                {
                    hasil[i * 4] = true;
                    hasil[i * 4 + 1] = true;
                    hasil[i * 4 + 2] = false;
                    hasil[i * 4 + 3] = false;
                }
                else if (hexchar[i] == 'D')//D
                {
                    hasil[i * 4] = true;
                    hasil[i * 4 + 1] = true;
                    hasil[i * 4 + 2] = false;
                    hasil[i * 4 + 3] = true;
                }
                else if (hexchar[i] == 'E')//E
                {
                    hasil[i * 4] = true;
                    hasil[i * 4 + 1] = true;
                    hasil[i * 4 + 2] = true;
                    hasil[i * 4 + 3] = false;
                }
                else if (hexchar[i] == 'F')//F
                {
                    hasil[i * 4] = true;
                    hasil[i * 4 + 1] = true;
                    hasil[i * 4 + 2] = true;
                    hasil[i * 4 + 3] = true;
                }
            }
            return hasil;
        }
    }
}
