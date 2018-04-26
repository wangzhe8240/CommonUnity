using PublicProgram.HttpDown;
using PublicProgram.LogHelper;
using PublicProgram.ZipHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.Width = this.Width + 200;
                this.Height = this.Height + 200;

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// http下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownClick(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZipClick(object sender, EventArgs e)
        {
            ZipHelper zip = new ZipHelper();
            zip.ZipFile(this.fileToZipPath.Text, this.ZipedFilePath.Text);
        }

        private void chose(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog2 = new OpenFileDialog();
            fileDialog2.Multiselect = true;
            fileDialog2.ShowDialog();
            this.ZipedFilePath.Text = Path.GetDirectoryName(fileDialog2.FileName);
        }

        private void Click1(object sender, EventArgs e)
        {
            //选择多文件
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.ShowDialog();
            this.fileToZipPath.Text = Path.GetDirectoryName(ofd.FileName);
        }
    }
}
