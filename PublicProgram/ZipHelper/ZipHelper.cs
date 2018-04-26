using ICSharpCode.SharpZipLib.Zip;//引用第三方dll文件
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicProgram.ZipHelper
{
    /// <summary>
    /// zip压缩方法
    /// </summary>
    public class ZipHelper
    {

        /// <summary>
        /// 压缩单个文件
        /// </summary>
        /// <param name="fileToZipName">压缩前的文件名，全路径</param>
        /// <param name="ZipedFileName">压缩后的文件名，全路径</param>
        public void ZipFile(string fileToZipPath, string ZipedFilePath)
        {
            if (!File.Exists(fileToZipPath))
            {
                throw new FileNotFoundException("需要压缩的文件" + fileToZipPath + "不存在！");
            }
            using (FileStream stream = File.OpenRead(fileToZipPath))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();

                using (FileStream filestream = File.Create(ZipedFilePath))
                {
                    using (ZipOutputStream output = new ZipOutputStream(filestream))
                    {
                        string FileName = Path.GetFileName(ZipedFilePath);
                        var zipEntry = new ZipEntry(FileName)
                        {
                            DateTime = DateTime.Now,
                            IsUnicodeText = true,
                        };
                        output.PutNextEntry(zipEntry);
                        output.SetLevel(5);
                        output.Write(buffer, 0, buffer.Length);
                        output.Finish();
                        output.Close();
                    }
                }
            }
        }


    }
}
