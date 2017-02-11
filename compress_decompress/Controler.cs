using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace compress_decompress
{
    public class Controler
    {
        String sfdfilename;
        String ofdfilename;
        Image inFile;
        //public void open(PictureBox pb, string file, TextBox tbinfo)
        public String open(PictureBox pb, String file, TextBox tbinfo)
        {
            PictureBox imagecontrol = new PictureBox();
            imagecontrol.Height = 400;
            imagecontrol.Width = 400;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images (" + file + ")|" + file;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(ofd.FileName);
                pb.Image = bmp;
                pb.SizeMode = PictureBoxSizeMode.CenterImage;
                inFile = Image.FromFile(ofd.FileName);

                tbinfo.Text += "File path:" + ofd.FileName;
                ofdfilename = ofd.FileName;
            }
            return ofdfilename;
        }

       // public void save(TextBox tbinfo, string file)   
        public String save(TextBox tbinfo, String file,String pathd)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images (" + file + ")|" + file;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                RichTextBox richtextBox1 = new RichTextBox();
                richtextBox1.SaveFile(sfd.FileName);
                sfdfilename = sfd.FileName;
                
                tbinfo.Text += "Save File with path:" + sfd.FileName;
            }
            pathd = sfdfilename;
            return sfdfilename;
        }

        public void compressWithBitMap()
        {
            Bitmap bm = new Bitmap(inFile);
            FileStream output = new FileStream("D:\\onu\\ДС\\compress_decompress\\byte.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(output);
            for (int i = 0; i < bm.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {
                    Color color = bm.GetPixel(i, j);
                    byte b = color.B;
                    byte g = color.G;
                    byte r = color.R;
                    string s = r.ToString() + g.ToString() + b.ToString();
                    sw.Write(s);
                }
            }
            sw.Dispose();
            sw.Close();
            output.Dispose();
            output.Close();
        }

       
    }
}
