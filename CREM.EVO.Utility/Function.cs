using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CREM.EVO.MODEL;

namespace CREM.EVO.Utility
{
    public class Function
    {
        public static string GetPowderType(UInt16 a)
        {
            string ret = "";
             PowderType tmp = (PowderType)a;
            switch (tmp)
            {
                case PowderType.POWER_NULL:
                    ret = "Not Used";
                    break;
                case PowderType.POWDER_ESPRESSO:
                    ret = "ESPRESSO";
                    break;
                case PowderType.POWDER_CHOCOLATE:
                    ret = "CHOCOLATE";
                    break;
                case PowderType.POWDER_SUGAR:
                    ret = "SUGAR";
                    break;
                case PowderType.POWDER_MILK:
                    ret = "MILK";
                    break;
                default:
                    break;
            }
            return ret;
        }
        public static string GetBeanType(UInt16 a)
        {
            string ret = "";
            GrinderType tmp = (GrinderType)a;


            switch (tmp)
            {
                case GrinderType.GRINDER_NULL:
                    ret = "Not Used";
                    break;
                case GrinderType.GRINDER_COFFEE1:
                    ret = "CoffeeBean1";
                    break;
                case GrinderType.GRINDER_COFFEE2:
                    ret = "CoffeeBean2";
                    break;
                default:
                    break;
            }

            return ret;
        }
        public static class XmlSerializer
        {
            public static void SaveToXml(string filePath, object sourceObj, Type type, string xmlRootName)
            {
                if (!string.IsNullOrWhiteSpace(filePath) && sourceObj != null)
                {
                    type = type != null ? type : sourceObj.GetType();

                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        System.Xml.Serialization.XmlSerializer xmlSerializer = string.IsNullOrWhiteSpace(xmlRootName) ?
                            new System.Xml.Serialization.XmlSerializer(type) :
                            new System.Xml.Serialization.XmlSerializer(type, new XmlRootAttribute(xmlRootName));
                        xmlSerializer.Serialize(writer, sourceObj);
                    }
                }
            }

            public static object LoadFromXml(string filePath, Type type)
            {
                object result = null;

                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);
                        result = xmlSerializer.Deserialize(reader);
                    }
                }

                return result;
            }
        }
        public static byte[] classToByteArray<T>(T myclass)
        {
            int res = Marshal.SizeOf(typeof(T));
            byte[] b_array = new byte[res];
            IntPtr buff = System.Runtime.InteropServices.Marshal.AllocHGlobal(res);
            System.Runtime.InteropServices.Marshal.StructureToPtr(myclass, buff, true);
            System.Runtime.InteropServices.Marshal.Copy(buff, b_array, 0, res);
            System.Runtime.InteropServices.Marshal.FreeHGlobal(buff);
            return b_array;
        }
        public static T byteArrayToClass<T>(byte[] myarray)
        {
            
            
            
            GCHandle hObject = GCHandle.Alloc(myarray, GCHandleType.Pinned);
            IntPtr pObject = hObject.AddrOfPinnedObject();

            if (hObject.IsAllocated)
                hObject.Free();

            return (T)System.Runtime.InteropServices.Marshal.PtrToStructure(pObject, typeof(T));

        }
    }
}
