using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace W2___SkillData_Editor.Funções
{
    public static class Pak
    {
        public static T ToStruct<T>(byte[] Data)
        {
            unsafe
            {
                fixed (byte* pBuffer = Data)
                {
                    return (T)Marshal.PtrToStructure(new IntPtr((void*)&pBuffer[0]), typeof(T));
                }
            }
        }
        public static T ToStruct<T>(byte[] Data, int Start)
        {
            unsafe
            {
                fixed (byte* pBuffer = Data)
                {
                    return (T)Marshal.PtrToStructure(new IntPtr((void*)&pBuffer[Start]), typeof(T));
                }
            }
        }

        public static byte[] ToByteArray<T>(T Struct)
        {
            byte[] Data = new byte[Marshal.SizeOf(Struct)];

            unsafe
            {
                fixed (byte* Buffer = Data)
                {
                    Marshal.StructureToPtr(Struct, new IntPtr((void*)Buffer), true);
                }
            }

            return Data;
        }
    }
    class Functions : Structs
    {
        public static BinaryType LoadFile<BinaryType>(byte[] rawData) where BinaryType : struct
        {
            var pinnedRawData = GCHandle.Alloc(rawData, GCHandleType.Pinned);
            try
            {
                var pinnedRawDataPtr = pinnedRawData.AddrOfPinnedObject();
                return (BinaryType)Marshal.PtrToStructure(pinnedRawDataPtr, typeof(BinaryType));
            }
            finally
            {
                pinnedRawData.Free();
            }
        }

        public static void SaveFile<BinaryType>(BinaryType SkillData)
        {
            try
            {
                byte[] arr = new byte[Marshal.SizeOf(SkillData)];

                IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(SkillData));
                Marshal.StructureToPtr(SkillData, ptr, false);
                Marshal.Copy(ptr, arr, 0, Marshal.SizeOf(SkillData));
                Marshal.FreeHGlobal(ptr);


                byte[] pKeyList = File.ReadAllBytes("./Keys.bin");
                Array.Resize(ref pKeyList, pKeyList.Length + 1);


                for (int i = 0; i < arr.Length; i++)
                    arr[i] ^= (pKeyList[i & 63]);

                File.WriteAllBytes("SkillData.bin", arr);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void SaveFile<BinaryType>(BinaryType SkillData, string FilePatch)
        {
            try
            {
                byte[] arr = new byte[Marshal.SizeOf(SkillData)];

                IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(SkillData));
                Marshal.StructureToPtr(SkillData, ptr, false);
                Marshal.Copy(ptr, arr, 0, Marshal.SizeOf(SkillData));
                Marshal.FreeHGlobal(ptr);

                byte[] pKeyList = File.ReadAllBytes("./Keys.bin");
                Array.Resize(ref pKeyList, pKeyList.Length + 1);


                for (int i = 0; i < arr.Length; i++)
                    arr[i] ^= (pKeyList[i & 63]);

                File.WriteAllBytes(FilePatch, arr);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public static bool ReadSkillData(string Path)
        {
            try
            {
                byte[] data;

                if (File.Exists(Path))
                    data = File.ReadAllBytes(Path);
                else
                {
                    MessageBox.Show("SkillData não foi encontrado", "W2 - SkillData Editor", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    return false;
                }

                byte[] pKeyList = File.ReadAllBytes("./Keys.bin");
                Array.Resize(ref pKeyList, pKeyList.Length + 1);

                if(data[1] == 0x5A)
                {
                    for (int i = 0; i < data.Length; i++)
                        data[i] ^= 0x5A;
                }
                else
                {
                    for (int i = 0; i < data.Length; i++)
                        data[i] ^= (pKeyList[i & 63]);
                }

                External.g_pSkillData = LoadFile<STRUCT_SKILLDATA>(data);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
