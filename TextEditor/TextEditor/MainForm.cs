using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public interface IMainForm
    {
        string FilePath { get; }
        string Content { get; set; }
        void SetSymbolCount(int count);
        event EventHandler FileOpenClick;
        event EventHandler FileSaveClick;
        event EventHandler ContentChanged;
    }

    public partial class MainForm : Form, IMainForm
    {
        public MainForm()
        {
            InitializeComponent();
            btnOpenFile.Click += BtnOpenFile_Click;
            btnSaveFile.Click += BtnSaveFile_Click;
            tbContent.TextChanged += TbContent_TextChanged;
            btnSelectFile.Click += btnSelectFile_Click;
            numFont.ValueChanged += NumFont_ValueChanged;
        }



        #region Проброс событий
        private void TbContent_TextChanged(object sender, EventArgs e)
        {
            if (ContentChanged != null) ContentChanged(this, EventArgs.Empty);
        }
        private void BtnSaveFile_Click(object sender, EventArgs e)
        {
            if (FileSaveClick != null) FileSaveClick(this, EventArgs.Empty);
        }
        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            if (FileOpenClick != null) FileOpenClick(this, EventArgs.Empty);
        }
        #endregion

        #region Реализация интерфейса IMainForm
        public string FilePath
        {
            get { return tbFilePath.Text; }
        }
        public string Content
        {
            get { return tbContent.Text; }
            set { tbContent.Text = value; }
        }

        public void SetSymbolCount(int count)
        {
            lbCount.Text = count.ToString();
        }

        public event EventHandler FileOpenClick;
        public event EventHandler FileSaveClick;
        public event EventHandler ContentChanged;

        #endregion

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tbFilePath.Text = dlg.FileName;
                if (FileOpenClick != null) FileOpenClick(this, EventArgs.Empty);
            }
        }

        private void NumFont_ValueChanged(object sender, EventArgs e)
        {
            tbContent.Font = new Font("Calibri", (float)numFont.Value);
        }
    }
}
