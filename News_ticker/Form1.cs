using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace News_ticker
{
    public partial class Form1 : Form
    {
        Bitmap red, orange, yellow, green, blue, indigo, purple, white, black;
        List<Bitmap> colorImage;
        List<Button> colorButton;
        PictureBox[,] LED;
        List<int[]> pic = new List<int[]>();
        int cPos;

        // create color selections for LED lights
        void colorChoose()
        {
            tlp_color.ColumnCount = 0;
            tlp_color.RowCount = 1;

            red = Properties.Resources.red;
            orange = Properties.Resources.orange;
            yellow = Properties.Resources.yellow;
            green = Properties.Resources.green;
            blue = Properties.Resources.blue;
            indigo = Properties.Resources.indigo;
            purple = Properties.Resources.purple;
            white = Properties.Resources.white;
            black = Properties.Resources.black;

            colorImage = new List<Bitmap>();
            colorImage.Add(red);
            colorImage.Add(orange);
            colorImage.Add(yellow);
            colorImage.Add(green);
            colorImage.Add(blue);
            colorImage.Add(indigo);
            colorImage.Add(purple);
            colorImage.Add(white);

            colorButton = new List<Button>();
            for (int i = 0; i < 8; i++)
            {
                Button c = new Button();
                colorButton.Add(c);
                c.Click += colorButton_clicked;
                tlp_color.Controls.Add(c, i, 0);
                c.Image = colorImage[i];
                c.BackgroundImageLayout = ImageLayout.Stretch;     
                tlp_color.ColumnCount++;  
            }
        }

        //create lights
        void createLED()
        {
            black = Properties.Resources.black;
            LED = new PictureBox[15, 30];
            int w = ClientRectangle.Width / 30;
            for (int r = 0; r < 15; r++)
            {
                for (int c = 0; c < 30; c++)
                {
                    LED[r, c] = new PictureBox();
                    LED[r, c].Width = LED[r, c].Height = w;
                    LED[r, c].Left = 10 + c * w;
                    LED[r, c].Top = (3 + r) * w;
                    LED[r, c].Image = black;
                    LED[r, c].SizeMode = PictureBoxSizeMode.StretchImage;
                    this.Controls.Add(LED[r, c]);
                    LED[r, c].Click += LED_Clicked;
                }
            }
        }



        //create cycling pic
        int[,] recordPic()
        {
            int[,] aPic = new int[15,30];
            for(int r = 0; r < 15; r++)
            {
                for(int c = 0; c < 30; c++)
                {
                    if (LED[r, c].Image == red)
                    {
                        aPic[r,c] = 1;
                    }
                    else if (LED[r, c].Image == orange)
                    {
                        aPic[r,c] = 2;
                    }
                    else if (LED[r, c].Image == yellow)
                    {
                        aPic[r,c] = 3;
                    }
                    else if (LED[r, c].Image == green)
                    {
                        aPic[r,c] = 4;
                    }
                    else if (LED[r, c].Image == blue)
                    {
                        aPic[r,c] = 5;
                    }
                    else if (LED[r, c].Image == indigo)
                    {
                        aPic[r,c] = 6;
                    }
                    else if (LED[r, c].Image == purple)
                    {
                        aPic[r,c] = 7;
                    }
                    else if (LED[r, c].Image == white)
                    {
                        aPic[r,c] = 8;
                    }
                }
            }
            return aPic;
        }

        void showPic(int[,] pic) 
        {

            for (int r = 0; r < 15; r++)
            {
                for (int c = 0; c < 30; c++)
                {
                    if ((c + 1) < 30)
                    {
                        if (pic[r, c + 1] == 0) LED[r, c].Image = black;
                        else if (pic[r, c + 1] == 1) LED[r, c].Image = red;
                        else if (pic[r, c + 1] == 2) LED[r, c].Image = orange;
                        else if (pic[r, c + 1] == 3) LED[r, c].Image = yellow;
                        else if (pic[r, c + 1] == 4) LED[r, c].Image = green;
                        else if (pic[r, c + 1] == 5) LED[r, c].Image = blue;
                        else if (pic[r, c + 1] == 6) LED[r, c].Image = indigo;
                        else if (pic[r, c + 1] == 7) LED[r, c].Image = purple;
                        else if (pic[r, c + 1] == 8) LED[r, c].Image = white;
                    }
                    else
                    {
                        if (pic[r, 0] == 0) LED[r, c].Image = black;
                        else LED[r, c].Image = colorImage[pic[r, 0] - 1];
                    }                   
                }
            }
        }
        public Form1()
        {
            InitializeComponent();          
            colorChoose();
            createLED();  
        }

        //press the light
        private void colorButton_clicked(object sender, EventArgs e)
        {
            for(int i = 0; i < 8; i++)
            {
                if (sender == colorButton[i])
                {
                    cPos = i;
                }
            }
        }
        private void LED_Clicked(object sender, EventArgs e)
        {
            PictureBox light = (PictureBox)sender;
          
            if (light.Image == black)
            {
                light.Image = colorImage[cPos];
            }         
            else light.Image = black;
        }

        //run the light
        bool isRun = true;
        private void timer_run_Tick(object sender, EventArgs e)
        {
            if (isRun)
            {
                int[,] currentPic = recordPic();
             
                showPic(currentPic);
            }
        }
        private void button_RUN_Click(object sender, EventArgs e)
        {
            timer_run.Enabled = !timer_run.Enabled;
            if (timer_run.Enabled)
            {
                isRun = true;
                button_RUN.Text = "STOP";
            }
            else button_RUN.Text = "RUN";
        }
        private void button_clear_Click(object sender, EventArgs e)
        {
            for (int r = 0; r < 15; r++)
            {
                for (int c = 0; c < 30; c++)
                {
                    LED[r, c].Image = black;
                }
            }

        }
    }
}
