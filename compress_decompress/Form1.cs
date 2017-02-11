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
    public partial class Form1 : Form
    {
        Controler control = new Controler();
        Classcontrol classcontrol = new Classcontrol();
        String pathc;
        String pathd;

        public Form1()
        {
            InitializeComponent();
        }

        private void btncompress_Click(object sender, EventArgs e)
        {
            classcontrol.compressrle(/*control.open(pbprimary, "*.bmp;*.tiff", tbinfo)*/"D:\\onu\\ДС\\compress_decompress\\byte.txt", "compressed.rle", tbinfo);
            
             //control.open(pbprimary, "*.bmp;*.tiff", tbinfo);
            //control.save(tbinfo, "*.rle;*.rle");
            //control.compressrle(/*pbprimary,*/ tbinfo /*,"*.bmp;*.tiff", "*.rle"*/);
        }

        private void btndecompress_Click(object sender, EventArgs e)
        {
           // classcontrol.decompressrle("compressed.rle", "decompressed.bmp", tbinfo);
            //control.open(pbdecompress, "decompressed.bmp", tbinfo);


            //classcontrol.decompressrle("compressed.rle", control.save(tbinfo, "*.bmp;*.tiff",pathd), tbinfo);
            //control.open(pbdecompress, pathd, tbinfo);
           
            classcontrol.decompressrle("compressed.rle", control.save(tbinfo, "*.bmp;*.tiff",pathd), tbinfo);
            control.open(pbdecompress, "*.BMP;*.TIFF", tbinfo);

           // control.open(pbdecompress, "*.BMP;*.TIFF", tbinfo);
            //control.decompressrle(pbdecompress, tbinfo, "*.rle", "*.bmp");
        }

        private void btnchoose_Click(object sender, EventArgs e)
        {
           //control.open(pbprimary, "*.bmp;*.tiff", tbinfo);
           pathc = control.open(pbprimary, "*.bmp;*.tiff",tbinfo);
           control.compressWithBitMap();
          // pathd = control.save(tbinfo, "*.bmp;*.tiff");
         
       }

        
    }
}
