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
   public class Classcontrol
    {
       
        public void compressrle(String filein, String fileout, TextBox tbinfo)
        {

            try
            {
                FileStream input = new FileStream(filein, FileMode.Open, FileAccess.Read);
                FileStream output = new FileStream(fileout, FileMode.OpenOrCreate);

                int next = 0;
                int numBytesRead = 0;
                byte[] bytes = new byte[input.Length];
                int numBytesToRead = (int)input.Length;
                int count = input.Read(bytes, numBytesRead, numBytesToRead);

                while (numBytesToRead > 0)
                {
                    int n = input.Read(bytes, numBytesRead, numBytesToRead);
                    if (n == 0)
                        break;

                    numBytesRead += n;
                    numBytesToRead -= n;
                }
                numBytesToRead = bytes.Length;
                tbinfo.Text += bytes + " " + numBytesRead + " " + numBytesToRead;
                output.Write(bytes, 0, numBytesToRead);
                tbinfo.Text += " output File open";

                while ((next = input.Read(bytes, numBytesRead, numBytesToRead)) >= 0)
                {
                    int counter = 1;
                    if (count == next)
                    {
                        counter++;
                        while (next == (count = input.Read(bytes, numBytesRead, numBytesToRead)))
                        {
                            counter++;
                        }
                        while (counter >= 63)
                        {
                            output.Write(bytes, 0, 255);
                            output.Write(bytes, 0, next);
                            counter -= 63;
                        }
                        if (counter > 1)
                        {
                            output.Write(bytes, 0, 0xc0 + counter);
                            output.Write(bytes, 0, next);
                        }
                    }
                    else
                    {
                        if (count <= 0xc0)
                        {
                            output.Write(bytes, 0, count);
                            count = next;
                        }
                        else
                        {
                            output.Write(bytes, 0, 0xc1);
                            output.Write(bytes, 0, count);
                            count = next;
                        }
                    }
                }
                if (count <= 0xc0)
                {
                    output.Write(bytes, 0, count);
                }
                else
                {
                    output.Write(bytes, 0, 0xc1);
                    output.Write(bytes, 0, count);
                }

                input.Dispose();
                output.Dispose();
                input.Close();
                output.Close();
            }

            catch (IOException e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void decompressrle(String filein, String fileout, TextBox tbinfo)
        {
            try
            {
                FileStream input = new FileStream(filein, FileMode.Open, FileAccess.Read);
                byte[] bytes = new byte[input.Length];
                int numBytesToRead = (int)input.Length;
                FileStream output = new FileStream(fileout, FileMode.OpenOrCreate, FileAccess.Write);
                output.Write(bytes, 0, numBytesToRead);
 
                int count = 0;
                int numBytesRead = 0;
                while ((count = input.Read(bytes, numBytesRead, numBytesToRead)) >= 0)
                {
                    if (count == 0xc1)
                    {
                        output.Write(bytes, 0, input.Read(bytes, numBytesRead, numBytesToRead));
                    }
                    else if (count <= 0xc0)
                    {
                        output.Write(bytes, 0, count);
                    }
                    else if (count > 0xc1)  
                    {
                        int next = input.Read(bytes, numBytesRead, numBytesToRead);
                        for (int i = 0; i < (count - 0xc0); i++)
                        {
                            output.Write(bytes, 0, next);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        }
    }

