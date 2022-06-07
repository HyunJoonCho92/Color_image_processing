using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace Day16_01_Color_Image_Processing__Beta_1_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "칼라 영상 처리 (SpinOff 1)";
            toolStripStatusLabel1.Text = "";
            toolStripStatusLabel2.Text = "";
            toolStripStatusLabel3.Text = "";
            toolStripStatusLabel4.Text = "";
            pictureBox1.BackColor = Color.Transparent;
            //menuStrip1.Items[1].Enabled = false;
            //menuStrip1.Items[1].Enabled = false;

            //// 픽처 박스를 화면의 완전 가운데. (this.Size)
            //int clientX = this.ClientSize.Width;
            //int clientY = this.ClientSize.Height + menuStrip1.Height;

            //pictureBox1.Left = (clientX - pictureBox1.Width) / 2;
            //pictureBox1.Top = (clientY - pictureBox1.Height) / 2;
        }

        //// 전역 변수부
        byte[,,] inImage = null, outImage = null;
        int inH, inW, outH, outW;
        string fileName;
        Bitmap paper, bitmap;
        const int RGB = 3, RR = 0, GG = 1, BB = 2;
        
        ///////////마우스///////////////////
        int sx = -1, sy = -1, ex = -1, ey = -1; // 마우스 관련
        
        //char mouseYN = '0';

        char processType = '0'; //영상처리 종류
        string mouseStatus = "None"; //마우스 선택 상태(none, Box,  Free, ...)

        /// ///////////////////////
        /// 자유 마우스 관련 -->
        /// ///////////////////////
        int[] mx = new int[5000];
        int[] my = new int[5000];
        int mCount = 0;

        bool pointInPolygon(int pntX, int pntY, int[] xArray, int[] yArray)
        {
            bool isInPoly = false;
            int crossCount = 0; // 교차 횟수 
            for (int i = 0; i < mCount; i++)
            {
                int j = (i + 1) % mCount;
                if ((yArray[i] > pntY) != (yArray[j] > pntY))
                {
                    double atX = ((((double)xArray[j] - xArray[i]) / ((double)yArray[j] - yArray[i])) * ((double)pntY - yArray[i])) + (double)xArray[i];
                    if (pntX < atX)
                        crossCount++;
                }

            } // 홀수면 내부, 짝수면 외부에 있음 
            if (0 == (crossCount % 2))
                isInPoly = false;
            else isInPoly = true;

            return isInPoly;
        }

        void drawPoint(int px, int py)
        {
            Color c;
            byte r, g, b;
            try
            {
                c = bitmap.GetPixel(px, py);
                r = (byte)(255 - c.R);
                g = (byte)(255 - c.G);
                b = (byte)(255 - c.B);
                c = Color.FromArgb(r, g, b);
                bitmap.SetPixel(px, py, c);

                pictureBox1.Image = bitmap;
            }
            catch { }
        }

        //rubber band 관련
        bool mouseDown = false;
        int oldX = -1, oldY = -1;


        ////  메뉴 이벤트 처리 함수부
        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openImage();
            //Console.WriteLine(mouseStatus);
        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveImage();
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 동일이미지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            equal_image();
        }

        private void 반전이미지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseStatus == "None")
                reverseImage();
            else
                processType = 'A';
        }

        private void 밝게어둡게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseStatus == "None")
                add_image();
            else
                processType = 'B';
        }

        private void 흑백127기준ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseStatus == "None")
                bw_image();
            else
                processType = 'C';
        }

        private void 그레이스케일ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseStatus == "None")
                grayscale_image();

            else //마우스
                processType = 'D';
        }

        private void 파라볼라ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseStatus == "None")
                paraImage();
            else //마우스
                processType = 'E';
        }

        private void 감마ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseStatus == "None")
                gamma();
            else //마우스
                processType = 'F';
        }

        private void 채도변경ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_satur();
        }

        private void 색상추출ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            color_pick();
        }

        private void 이동ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            move();
        }

        private void 좌우반전ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switchSides();
        }

        private void 상하반전ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            upsideDown();
        }

        private void 축소ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoomOut_image();
        }

        private void 확대ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoomIn_image();
        }

        private void 회전역방향ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rotate2_image();
        }

        private void 엠보싱ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseStatus == "None")
                emboss_image();
            else
                processType = 'G';
        }

        private void 블러링ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseStatus == "None")
                blurr();
            else
                processType = 'H';
        }

        private void 샤프닝ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseStatus == "None")
                sharpening();
            else
                processType = 'I';
        }

        private void 고주파ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseStatus == "None")
                hpfSharp();
            else
                processType = 'J';
        }

        private void 수평수직ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseStatus == "None")
                vert_hori_edge();
            else
                processType = 'K';
        }

        private void loGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseStatus == "None")
                LoG();
            else
                processType = 'L';
        }

        private void doGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseStatus == "None")
                DoG();
            else
                processType = 'M';
        }

        private void 스트레칭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseStatus == "None")
                histo_stretch();
            else
                processType = 'N';
        }

        private void 엔드인ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseStatus == "None")
                end_in();
            else
                processType = 'O';
        }

        private void 평활화ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseStatus == "None")
                histo_equalize();
            else
                processType = 'P';
        }

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            mouseStatus = toolStripComboBox1.Text;
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (processType == '0')
                return;

            if(mouseStatus == "Box")
            {
                sx = e.X;
                sy = e.Y;
                toolStripStatusLabel1.Text = sx + "," + sy;
                mouseDown = true;
            }
            else if (mouseStatus == "Free")
            {
                drawPoint(e.X, e.Y);
                mx[mCount] = e.Y;
                my[mCount] = e.X;
                mCount++;
                mouseDown = true;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (processType == '0')
                return;
            if (!mouseDown)
                return;

            if (mouseStatus == "None")
            {

            }
            else if(mouseStatus == "Box")
            {
                // 기존에 박스선을 그린적이 있으면 지우기.
                if (oldX != -1)
                {
                    // 다시 반전시키기.  sx,sy ~ oldX, oldY
                    drawRubberBand(sx, sy, oldX, oldY);
                }

                ex = e.X;
                ey = e.Y;

                // sx,sy ~ ex,ey 까지 박스 선만 반전하기.
                drawRubberBand(sx, sy, ex, ey);

                oldX = ex;
                oldY = ey;
            }
            else if (mouseStatus == "Free")
            {
                drawPoint(e.X, e.Y);
                mx[mCount] = e.Y;
                my[mCount] = e.X;
                mCount++;
            }
        }


        int minX, maxX, minY, maxY;


        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (processType == '0')
                return;

            if (mouseStatus == "None")
            {

            }
            else if(mouseStatus== "Box")
            {
                ex = e.X;
                ey = e.Y;

                // 시작점, 끝점 재배열
                if (sx > ex)
                {
                    int tmp = sx;
                    sx = ex;
                    ex = tmp;
                }
                if (sy > ey)
                {
                    int tmp = sy;
                    sy = ey;
                    ey = tmp;
                }

                // 마지막 네모 지우기
                drawRubberBand(sx, sy, oldX, oldY);

                switch (processType)
                {
                    case 'A': reverseImage(); break;
                    case 'B': add_image(); break;
                    case 'C': bw_image(); break;
                    case 'D': grayscale_image(); break;
                    case 'E': paraImage(); break;
                    case 'F': gamma(); break;
                    case 'G': emboss_image(); break;
                    case 'H': blurr(); break;
                    case 'I': sharpening(); break;
                    case 'J': hpfSharp(); break;
                    case 'K': vert_hori_edge(); break;
                    case 'L': LoG(); break;
                    case 'M': DoG(); break;
                    case 'N': histo_stretch(); break;
                    case 'O': end_in(); break;
                    case 'P': histo_equalize(); break;
                }

                processType = '0';
                mouseDown = false;
                oldX = oldY = -1;
            }
            else if (mouseStatus == "Free")
            {
                //잔상 제거
                for (int i = 0; i < mCount; i++)
                {
                    drawPoint(my[i], mx[i]);
                }

                //성능 향상 1 : 폴리곤의 포인트 개수 줄이기.
                const int SCALE = 5; //5배 축소 (5점을 1점으로 축소)
                int[] newMx = new int[mCount / SCALE];
                int[] newMy = new int[mCount / SCALE];

                for (int i = 0; i < mCount / SCALE; i++)
                {
                    try
                    {
                        newMx[i] = mx[i * SCALE];
                        newMy[i] = my[i * SCALE];
                    }
                    catch { }
                }

                mx = newMx;
                my = newMy;
                mCount = mCount / SCALE;

                //성능향상 2 : 포인트가 포함된 박스 찾기
                minX = maxX = mx[0];
                minY = maxY = my[0];

                for (int i = 0; i < mCount; i++)
                {
                    if (mx[i] < minX)
                        minX = mx[i];
                    if (mx[i] > maxX)
                        maxX = mx[i];
                    if (my[i] < minY)
                        minY = my[i];
                    if (my[i] > maxY)
                        maxY = my[i];
                }

                //시간 측정
                Stopwatch stw = new Stopwatch();//시간 측정 객체 선언
                stw.Start();



                switch (processType)
                {
                    case 'A': reverseImage(); break;
                    case 'B': add_image(); break;
                    case 'C': bw_image(); break;
                    case 'D': grayscale_image(); break;
                    case 'E': paraImage(); break;
                    case 'F': gamma(); break;
                    case 'G': emboss_image(); break;
                    case 'H': blurr(); break;
                    case 'I': sharpening(); break;
                    case 'J': hpfSharp(); break;
                    case 'K': vert_hori_edge(); break;
                    case 'L': LoG(); break;
                    case 'M': DoG(); break;
                    case 'N': histo_stretch(); break;
                    case 'O': end_in(); break;
                    case 'P': histo_equalize(); break;
                }

                stw.Stop();
                long mSec = stw.ElapsedMilliseconds; //밀리초 단위
                toolStripStatusLabel4.Text = mSec / 1000 + "초";

                ///////////////////
                processType = '0';
                mouseDown = false;
                mx = new int[5000];
                my = new int[5000];
                mCount = 0;
            }
        }

        void drawRubberBand(int startX, int startY, int endX, int endY)
        {
            // 네점을 재배열하기.
            if (startX > endX)
            {
                int tmp = startX;
                startX = endX;
                endX = tmp;
            }
            if (startY > endY)
            {
                int tmp = startY;
                startY = endY;
                endY = tmp;
            }
            Color c;
            byte r, g, b;

            try
            {
                for (int i = startX; i < endX; i++)
                {
                    c = bitmap.GetPixel(i, startY);
                    r = (byte)(255 - c.R);
                    g = (byte)(255 - c.G);
                    b = (byte)(255 - c.B);
                    c = Color.FromArgb(r, g, b);
                    bitmap.SetPixel(i, startY, c);

                    c = bitmap.GetPixel(i, endY);
                    r = (byte)(255 - c.R);
                    g = (byte)(255 - c.G);
                    b = (byte)(255 - c.B);
                    c = Color.FromArgb(r, g, b);
                    bitmap.SetPixel(i, endY, c);
                }
                for (int i = startY; i < endY; i++)
                {
                    c = bitmap.GetPixel(startX, i);
                    r = (byte)(255 - c.R);
                    g = (byte)(255 - c.G);
                    b = (byte)(255 - c.B);
                    c = Color.FromArgb(r, g, b);
                    bitmap.SetPixel(startX, i, c);

                    c = bitmap.GetPixel(endX, i);
                    r = (byte)(255 - c.R);
                    g = (byte)(255 - c.G);
                    b = (byte)(255 - c.B);
                    c = Color.FromArgb(r, g, b);
                    bitmap.SetPixel(endX, i, c);
                }
            }
            catch
            {

            }

            pictureBox1.Image = bitmap;

        }

        

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.O: openImage(); break;
                    case Keys.S: saveImage(); break;
             

                }
            }
            if(e.Alt)
            {
                switch(e.KeyCode)
                {
                    case Keys.E: emboss_image(); break;
                }
            }
        }



        ////  공통 함수부
        void openImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "";
            ofd.Filter = "칼라 필터 | *.jpg; *.png; *.bmp; *.tif";
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            fileName = ofd.FileName;
            // 파일 --> 비트맵
            bitmap = new Bitmap(fileName);
            // (중요!) 이미지 높이와 폭 알아내기
            inW = bitmap.Height;
            inH = bitmap.Width;
            // 입력 메모리 할당
            inImage = new byte[RGB, inH, inW];
            // 비트맵 --> 메모리(로딩)
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                {
                    Color c = bitmap.GetPixel(i, k);
                    inImage[RR, i, k] = c.R;
                    inImage[GG, i, k] = c.G;
                    inImage[BB, i, k] = c.B;
                }

            equal_image();
            // 메뉴 활성화
            menuStrip1.Items[1].Enabled = true;
            toolStripStatusLabel4.Text = "";
        }


        void saveImage()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "";
            sfd.Filter = "PNG 파일(*.png) | *.png";
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            String saveFname = sfd.FileName;
            Bitmap bitmap = new Bitmap(outH, outW); // 빈 비트맵 준비
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    Color c;
                    c = Color.FromArgb(outImage[RR, i, k], outImage[GG, i, k], outImage[BB, i, k]);
                    bitmap.SetPixel(i, k, c); // 비트맵에 한점 콕 찍기
                }

            // using System.Drawing.Imaging; 필요
            bitmap.Save(saveFname, ImageFormat.Png);
            toolStripStatusLabel4.Text = Path.GetFileName(saveFname) + " 저장!";

        }


        void displayImage()
        {
            // pb_outImage 위치 선정
            //pictureBox1.Location = new Point(10,80);

            // 크기 지정
            paper = new Bitmap(outH, outW); // 종이
            pictureBox1.Size = new Size(outH, outW); // 액자
            // this.Size = new Size(outH + 20, outW + 100);  // 벽
            this.ClientSize = new Size(outH+100, outW + menuStrip1.Height+100);
            //// 픽처 박스를 화면의 완전 가운데. (this.Size)
            int clientX = this.ClientSize.Width;
            int clientY = this.ClientSize.Height + menuStrip1.Height;
            pictureBox1.Left = (clientX - pictureBox1.Width) / 2;
            pictureBox1.Top = (clientY - pictureBox1.Height) / 2;

            Color pen; // 펜(콕콕 찍을 용도)
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    byte inkR = outImage[RR, i, k]; // 잉크(색상값)
                    byte inkG = outImage[GG, i, k]; // 잉크(색상값)
                    byte inkB = outImage[BB, i, k]; // 잉크(색상값)
                    pen = Color.FromArgb(inkR, inkG, inkB); // 펜에 잉크 묻히기
                    paper.SetPixel(i, k, pen); // 종이에 한점 콕 직기

                }
            }
            pictureBox1.Image = paper; // 액자에 종이 걸기
            toolStripStatusLabel1.Text = Path.GetFileName(fileName);
            toolStripStatusLabel2.Text = inH.ToString() + 'x' + inW.ToString();
            toolStripStatusLabel3.Text = outH.ToString() + 'x' + outW.ToString();

        }


        ////  *****  영상 처리 함수부 ****
        void equal_image()
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 --> 알고리즘에 영향
            outH = inH;
            outW = inW;
            // 출력 메모리 확보
            outImage = new byte[RGB, outH, outW];
            // **** 진짜 영상처리 알고리즘 ****
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < inH; i++)
                {
                    for (int k = 0; k < inW; k++)
                    {
                        outImage[rgb, i, k] = inImage[rgb, i, k];
                    }
                }
            }
            // ********************************
            displayImage();
        }


        void reverseImage() //반전 영상
        {
            if (inImage == null)
                return;

            // 중요! 출력 영상의 크기를 결정 --> 알고리즘에 따라서....
            outH = inH;
            outW = inW;
            // 출력 영상 메모리 할당.
            outImage = new byte[RGB, outH, outW];

            // ** 영상처리 알고리즘 **
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        if (mouseStatus == "None")
                        {
                            outImage[rgb, i, k] = (byte)(255 - inImage[rgb, i, k]);
                        }
                        else if(mouseStatus == "Box")
                        {
                            if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                            {
                                outImage[rgb, i, k] = (byte)(255 - inImage[rgb, i, k]);
                            }
                            else
                            {
                                outImage[RR, i, k] = inImage[RR, i, k];
                                outImage[GG, i, k] = inImage[GG, i, k];
                                outImage[BB, i, k] = inImage[BB, i, k];
                            }
                        }
                        else if(mouseStatus == "Free")
                        {
                            if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                            {
                                if (pointInPolygon(k, i, mx, my))
                                    outImage[rgb, i, k] = (byte)(255 - inImage[rgb, i, k]);
                                else
                                    outImage[rgb, i, k] = inImage[rgb, i, k];
                            }
                            else
                            {
                                outImage[rgb, i, k] = inImage[rgb, i, k];
                            }
                        }
                    }
                        

            displayImage();
        }

        double getValue()
        {
            double value = 0.0;
            Input1Form sub = new Input1Form();
            if (sub.ShowDialog() == DialogResult.Cancel)
                value = 0.0;
            else
                value = (double)sub.numericUpDown1.Value;

            return value;
        }


        void add_image()
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 --> 알고리즘에 영향
            outH = inH;
            outW = inW;
            // 출력 메모리 확보
            outImage = new byte[RGB, outH, outW];
            // **** 진짜 영상처리 알고리즘 ****
            if (processType == '0')
            {
                sx = 0; ex = inH;
                sy = 0; ey = inW;
            }

            int v = (int)getValue();
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < inH; i++)
                {
                    for (int k = 0; k < inW; k++)
                    {
                        if(mouseStatus == "None")
                        {
                            int px = inImage[rgb, i, k] + v;
                            if (px > 255)
                                px = 255;
                            else if (px < 0)
                                px = 0;

                            outImage[rgb, i, k] = (byte)px;
                        }
                        else if(mouseStatus == "Box")
                        {
                            if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                            {
                                int px = inImage[rgb, i, k] + v;
                                if (px > 255)
                                    px = 255;
                                else if (px < 0)
                                    px = 0;

                                outImage[rgb, i, k] = (byte)px;
                            }
                            else
                            {
                                outImage[rgb, i, k] = inImage[rgb, i, k];
                            }
                        }
                        else if(mouseStatus == "Free")
                        {
                            if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                            {
                                if (pointInPolygon(k, i, mx, my))
                                {
                                    int px = inImage[rgb, i, k] + v;
                                    if (px > 255)
                                        px = 255;
                                    else if (px < 0)
                                        px = 0;

                                    outImage[rgb, i, k] = (byte)px;
                                }
                                else
                                    outImage[rgb, i, k] = inImage[rgb, i, k];
                            }
                            else
                            {
                                outImage[rgb, i, k] = inImage[rgb, i, k];
                            }
                        }
                    }
                }
            }
            // ********************************
            displayImage();
        }


        void bw_image()
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기를 결정 --> 알고리즘에 영향
            outH = inH;
            outW = inW;
            // 출력 메모리 할당.
            outImage = new byte[RGB, outH, outW];

            // ** 영상처리 알고리즘 **
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                {
                    if (mouseStatus == "None")
                    {
                        int gray = (inImage[RR, i, k] + inImage[GG, i, k] + inImage[BB, i, k]) / 3;

                        if (gray > 128)
                            outImage[RR, i, k] = outImage[GG, i, k] = outImage[BB, i, k] = 255;
                        else
                            outImage[RR, i, k] = outImage[GG, i, k] = outImage[BB, i, k] = 0;
                    }
                    else if (mouseStatus == "Box")
                    {
                        if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                        {
                            int gray = (inImage[RR, i, k] + inImage[GG, i, k] + inImage[BB, i, k]) / 3;

                            if (gray > 128)
                                outImage[RR, i, k] = outImage[GG, i, k] = outImage[BB, i, k] = 255;
                            else
                                outImage[RR, i, k] = outImage[GG, i, k] = outImage[BB, i, k] = 0;
                        }
                        else
                        {
                            outImage[RR, i, k] = inImage[RR, i, k];
                            outImage[GG, i, k] = inImage[GG, i, k];
                            outImage[BB, i, k] = inImage[BB, i, k];
                        }
                    }
                    else if (mouseStatus == "Free")
                    {
                        if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                        {
                            if (pointInPolygon(k, i, mx, my))
                            {
                                int gray = (inImage[RR, i, k] + inImage[GG, i, k] + inImage[BB, i, k]) / 3;

                                if (gray > 128)
                                    outImage[RR, i, k] = outImage[GG, i, k] = outImage[BB, i, k] = 255;
                                else
                                    outImage[RR, i, k] = outImage[GG, i, k] = outImage[BB, i, k] = 0;
                            }
                            else
                            {
                                outImage[RR, i, k] = inImage[RR, i, k];
                                outImage[GG, i, k] = inImage[GG, i, k];
                                outImage[BB, i, k] = inImage[BB, i, k];
                            }
                        }
                        else
                        {
                            outImage[RR, i, k] = inImage[RR, i, k];
                            outImage[GG, i, k] = inImage[GG, i, k];
                            outImage[BB, i, k] = inImage[BB, i, k];
                        }
                    }
                }
            displayImage();
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            //mouseStatus = toolStripComboBox1.SelectedIndex;
        }



        void grayscale_image()
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 --> 알고리즘에 영향
            outH = inH;
            outW = inW;
            // 출력 메모리 확보
            outImage = new byte[RGB, outH, outW];
            // **** 진짜 영상처리 알고리즘 ****
            if (processType == '0')
            {
                sx = 0; ex = inH;
                sy = 0; ey = inW;
            }
            
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    if (mouseStatus == "None")
                    {
                        int gray = (inImage[RR, i, k] + inImage[GG, i, k] + inImage[BB, i, k]) / 3;
                        outImage[RR, i, k] = outImage[GG, i, k] = outImage[BB, i, k] = (byte)gray;
                            
                    }
                    else if(mouseStatus == "Box")
                    {
                        if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                        {
                            int gray = (inImage[RR, i, k] + inImage[GG, i, k] + inImage[BB, i, k]) / 3;
                            outImage[RR, i, k] = outImage[GG, i, k] = outImage[BB, i, k] = (byte)gray;
                        }
                        else
                        {
                            outImage[RR, i, k] = inImage[RR, i, k];
                            outImage[GG, i, k] = inImage[GG, i, k];
                            outImage[BB, i, k] = inImage[BB, i, k];
                        }
                    }
                    else if(mouseStatus == "Free")
                    {
                        if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                        {
                            if (pointInPolygon(k, i, mx, my))
                            {
                                int gray = (inImage[RR, i, k] + inImage[GG, i, k] + inImage[BB, i, k]) / 3;
                                outImage[RR, i, k] = outImage[GG, i, k] = outImage[BB, i, k] = (byte)gray;
                            }
                            else
                            {
                                outImage[RR, i, k] = inImage[RR, i, k];
                                outImage[GG, i, k] = inImage[GG, i, k];
                                outImage[BB, i, k] = inImage[BB, i, k];
                            }
                        }
                        else
                        {
                            outImage[RR, i, k] = inImage[RR, i, k];
                            outImage[GG, i, k] = inImage[GG, i, k];
                            outImage[BB, i, k] = inImage[BB, i, k];
                        }
                    }
                }
            }
            // ********************************
            displayImage();
        }


        void paraImage() //파라볼라 --> 잘 안되는거 같다. 흑백처럼 나옴
        {
            if (inImage == null)
                return;

            // 중요! 출력 영상의 크기를 결정 --> 알고리즘에 따라서....
            outH = inH;
            outW = inW;
            // 출력 영상 메모리 할당.
            outImage = new byte[RGB, outH, outW];

            // ** 영상처리 알고리즘 **
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    if (mouseStatus == "None")
                    {
                        double valueR = 255 - 255 * ((double)inImage[RR, i, k] / 128 - 1) * (inImage[RR, i, k] / 128 - 1);
                        double valueG = 255 - 255 * ((double)inImage[GG, i, k] / 128 - 1) * (inImage[GG, i, k] / 128 - 1);
                        double valueB = 255 - 255 * ((double)inImage[BB, i, k] / 128 - 1) * (inImage[BB, i, k] / 128 - 1);
                        outImage[RR, i, k] = (byte)valueR;
                        outImage[GG, i, k] = (byte)valueG;
                        outImage[BB, i, k] = (byte)valueB;

                    }
                    else if (mouseStatus == "Box")
                    {
                        if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                        {
                            double valueR = 255 - 255 * ((double)inImage[RR, i, k] / 128 - 1) * (inImage[RR, i, k] / 128 - 1);
                            double valueG = 255 - 255 * ((double)inImage[GG, i, k] / 128 - 1) * (inImage[GG, i, k] / 128 - 1);
                            double valueB = 255 - 255 * ((double)inImage[BB, i, k] / 128 - 1) * (inImage[BB, i, k] / 128 - 1);
                            outImage[RR, i, k] = (byte)valueR;
                            outImage[GG, i, k] = (byte)valueG;
                            outImage[BB, i, k] = (byte)valueB;
                        }
                        else
                        {
                            outImage[RR, i, k] = inImage[RR, i, k];
                            outImage[GG, i, k] = inImage[GG, i, k];
                            outImage[BB, i, k] = inImage[BB, i, k];
                        }
                    }
                    else if (mouseStatus == "Free")
                    {
                        if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                        {
                            if (pointInPolygon(k, i, mx, my))
                            {
                                double valueR = 255 - 255 * ((double)inImage[RR, i, k] / 128 - 1) * (inImage[RR, i, k] / 128 - 1);
                                double valueG = 255 - 255 * ((double)inImage[GG, i, k] / 128 - 1) * (inImage[GG, i, k] / 128 - 1);
                                double valueB = 255 - 255 * ((double)inImage[BB, i, k] / 128 - 1) * (inImage[BB, i, k] / 128 - 1);
                                outImage[RR, i, k] = (byte)valueR;
                                outImage[GG, i, k] = (byte)valueG;
                                outImage[BB, i, k] = (byte)valueB;
                            }
                            else
                            {
                                outImage[RR, i, k] = inImage[RR, i, k];
                                outImage[GG, i, k] = inImage[GG, i, k];
                                outImage[BB, i, k] = inImage[BB, i, k];
                            }
                        }
                        else
                        {
                            outImage[RR, i, k] = inImage[RR, i, k];
                            outImage[GG, i, k] = inImage[GG, i, k];
                            outImage[BB, i, k] = inImage[BB, i, k];
                        }
                    }
                }
            }

            displayImage();
        }


        void gamma()
        {
            if (inImage == null)
                return;

            // 감마 1.2 보정
            double value = 1.2;
            // 중요! 출력 영상의 크기를 결정 --> 알고리즘에 따라서....
            outH = inH;
            outW = inW;
            // 출력 영상 메모리 할당.
            outImage = new byte[RGB, outH, outW];

            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        if (mouseStatus == "None")
                        {
                            double temp = 0;
                            temp = Math.Pow(inImage[rgb, i, k], (1 / value));
                            if (outImage[rgb, i, k] > 255)
                                outImage[rgb, i, k] = 255;
                            else
                                outImage[rgb, i, k] = (byte)temp;
                        }
                        else if (mouseStatus == "Box")
                        {
                            if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                            {
                                double temp = 0;
                                temp = Math.Pow(inImage[rgb, i, k], (1 / value));
                                if (outImage[rgb, i, k] > 255)
                                    outImage[rgb, i, k] = 255;
                                else
                                    outImage[rgb, i, k] = (byte)temp;
                            }
                            else
                            {
                                outImage[rgb, i, k] = inImage[rgb, i, k];
                            }
                        }
                        else if (mouseStatus == "Free")
                        {
                            if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                            {
                                if (pointInPolygon(k, i, mx, my))
                                {
                                    double temp = 0;
                                    temp = Math.Pow(inImage[rgb, i, k], (1 / value));
                                    if (outImage[rgb, i, k] > 255)
                                        outImage[rgb, i, k] = 255;
                                    else
                                        outImage[rgb, i, k] = (byte)temp;
                                }
                                else
                                    outImage[rgb, i, k] = inImage[rgb, i, k];
                            }
                            else
                            {
                                outImage[rgb, i, k] = inImage[rgb, i, k];
                            }
                        }
                    }

            displayImage();
        }



        void change_satur()
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 --> 알고리즘에 영향
            outH = inH;
            outW = inW;
            // 출력 메모리 확보
            outImage = new byte[RGB, outH, outW];
            // **** 진짜 영상처리 알고리즘 ****
            Color c; // 한점
            double hh, ss, vv; // 색상, 채도, 밝기
            int rr, gg, bb; // 레드, 그린 블루


            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    rr = inImage[RR, i, k];
                    gg = inImage[GG, i, k];
                    bb = inImage[BB, i, k];
                    // RGB --> HSV
                    c = Color.FromArgb(rr, gg, bb);
                    hh = c.GetHue();
                    ss = c.GetSaturation();
                    vv = c.GetBrightness();

                    // 채도 변경
                    ss -= 0.2;

                    // HSV --> RGB
                    HsvToRgb(hh, ss, vv, out rr, out gg, out bb);

                    outImage[RR, i, k] = (byte)rr;
                    outImage[GG, i, k] = (byte)gg;
                    outImage[BB, i, k] = (byte)bb;
                }
            }

            // ********************************
            displayImage();
        }


        void HsvToRgb(double h, double S, double V, out int r, out int g, out int b)
        {
            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {
                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;
                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;
                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;
                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;
                    default:
                        R = G = B = V;
                        break;
                }
            }
            r = CheckRange((int)(R * 255.0));
            g = CheckRange((int)(G * 255.0));
            b = CheckRange((int)(B * 255.0));

            int CheckRange(int i)
            {
                if (i < 0) return 0;
                if (i > 255) return 255;
                return i;
            }
        }

        void color_pick()
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 --> 알고리즘에 영향
            outH = inH;
            outW = inW;
            // 출력 메모리 확보
            outImage = new byte[RGB, outH, outW];
            // **** 진짜 영상처리 알고리즘 ****
            Color c; // 한점
            double hh, ss, vv; // 색상, 채도, 밝기
            int rr, gg, bb; // 레드, 그린 블루


            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    rr = inImage[RR, i, k];
                    gg = inImage[GG, i, k];
                    bb = inImage[BB, i, k];
                    // RGB --> HSV
                    c = Color.FromArgb(rr, gg, bb);
                    hh = c.GetHue();
                    
                    //주화색
                    if(5 <= hh && hh <= 25)
                    {
                        for(int rgb = 0; rgb < RGB;rgb++)
                        {
                            int px = inImage[rgb, i, k];
                            if (px > 255)
                                px = 255;
                            else if(px < 0)
                                px = 0;
                            outImage[rgb, i, k] = (byte)px;
                        
                        }
                    }
                    else
                    {
                        int gray = inImage[RR, i, k] + inImage[GG, i, k] + inImage[BB, i, k];
                        gray -= 50;
                        if(gray > 255)
                            gray = 255;
                        else if(gray < 0)
                            gray = 0;
                        outImage[RR, i, k] = (byte)gray;
                        outImage[GG, i, k] = (byte)gray;
                        outImage[BB, i, k] = (byte)gray;


                    }
                }
            }

            // ********************************
            displayImage();
        }


        void move() //이동 영상
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기를 결정 --> 알고리즘에 따라서....
            outH = inH;
            outW = inW;
            // 출력 영상 메모리 할당.
            outImage = new byte[RGB, outH, outW];

            // ** 영상처리 알고리즘 **
            int value = (int)getValue();

            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                {
                    if (i < value || k < value)
                    {
                        outImage[RR, i, k] = 0;
                        outImage[GG, i, k] = 0;
                        outImage[BB, i, k] = 0;
                    }
                    else
                    {
                        outImage[RR, i, k] = inImage[RR, i - value, k - value];
                        outImage[GG, i, k] = inImage[GG, i - value, k - value];
                        outImage[BB, i, k] = inImage[BB, i - value, k - value];
                    }
                }

            displayImage();
        }


        void switchSides()
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기를 결정 --> 알고리즘에 따라서....
            outH = inH;
            outW = inW;
            // 출력 영상 메모리 할당.
            outImage = new byte[RGB, outH, outW];

            // ** 영상처리 알고리즘 **
            for (int i = 0; i < inH / 2; i++)
                for (int k = 0; k < inW; k++)
                {
                    outImage[RR, i, k] = inImage[RR, inH - i - 1, k];
                    outImage[GG, i, k] = inImage[GG, inH - i - 1, k];
                    outImage[BB, i, k] = inImage[BB, inH - i - 1, k];
                    
                    outImage[RR, inH - i - 1, k] = inImage[RR, i, k];
                    outImage[GG, inH - i - 1, k] = inImage[GG, i, k];
                    outImage[BB, inH - i - 1, k] = inImage[BB, i, k];
                }
                    

            displayImage();
        }


        void upsideDown()
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기를 결정 --> 알고리즘에 따라서....
            outH = inH;
            outW = inW;
            // 출력 영상 메모리 할당.
            outImage = new byte[RGB, outH, outW];

            // ** 영상처리 알고리즘 **
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW / 2; k++)
                {
                    outImage[RR, i, k] = inImage[RR, i, inW - k - 1];
                    outImage[GG, i, k] = inImage[GG, i, inW - k - 1];
                    outImage[BB, i, k] = inImage[BB, i, inW - k - 1];

                    outImage[RR, i, inW - k - 1] = inImage[RR, i, k];
                    outImage[GG, i, inW - k - 1] = inImage[GG, i, k];
                    outImage[BB, i, inW - k - 1] = inImage[BB, i, k];
                }

            displayImage();
        }


        void zoomOut_image()  // 축소 알고리즘
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기를 결정 --> 알고리즘에 따라서....
            int scale = (int)getValue();
            outH = inH / scale; outW = inW / scale;
            // 출력 영상 메모리 할당.
            outImage = new byte[RGB, outH, outW];

            // ** 영상처리 알고리즘 **
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                {
                    for (int k = 0; k < inW; k++)
                    {
                        if ((0 <= (i / scale) && (i / scale) < outH) && (0 <= (k / scale) && (k / scale) < outW))
                            outImage[rgb, i / scale, k / scale] = inImage[rgb, i, k];

                    }
                }
            ////////////////////////////
            displayImage();
        }
        void zoomIn_image()  // 확대 알고리즘
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기를 결정 --> 알고리즘에 따라서....
            double scale = (double)getValue();
            outH = (int)(inH * scale); outW = (int)(inW * scale);
            // 출력 영상 메모리 할당.
            outImage = new byte[RGB, outH, outW];

            // ** 영상처리 알고리즘 **
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < outH; i++)
                {
                    for (int k = 0; k < outW; k++)
                    {
                        if ((0 <= (int)(i / scale) && (int)(i / scale) < inH) && (0 <= (int)(k / scale) && (int)(k / scale) < inW))
                            outImage[rgb, i, k] = inImage[rgb, (int)(i / scale), (int)(k / scale)];
                    }
                }
            ////////////////////////////
            displayImage();
        }

        void rotate2_image()///////////좀 이상함
        {
            if (inImage == null)
            {
                return;
            }
            // 중요! 출력 영상의 크기를 결정 --> 알고리즘에 따라서....
            //xd = cos * xs - sin*ys
            //yd = sin *xs +cos*ys
            outH = inH;
            outW = inW;

            outImage = new byte[RGB, outH, outW];  //메모리를 할당

            double angle = getValue();
            double radian = angle * Math.PI / 180.0;
            int cx = outH / 2;
            int cy = outW / 2;
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < outH; i++)
                {
                    for (int k = 0; k < outW; k++)
                    {
                        int xs = (int)(Math.Cos(radian) * (i - cx) - Math.Sin(angle) * (k - cy));
                        int ys = (int)(Math.Sin(angle) * (i - cx) + Math.Cos(angle) * (k - cy));
                        xs += cx;
                        ys += cy;
                        if ((0 <= xs && xs < inH) && (0 <= ys && ys < inW))
                            outImage[rgb, i, k] = inImage[rgb, xs, ys];
                    }
                }
            }
            displayImage();
        }

        void emboss_image()  // 엠보싱 알고리즘
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기를 결정 --> 알고리즘에 따라서....
            outH = inH;
            outW = inW;
            // 출력 영상 메모리 할당.
            outImage = new byte[RGB, outH, outW];

            // ** 영상처리 알고리즘 **
            const int MSIZE = 3;
            double[,] mask = { { -1.0, 0.0, 0.0 },
                                { 0.0, 0.0, 0.0 },
                                { 0.0, 0.0, 1.0 }  };  // 엠보싱 마스크
            // 임시 입출력 메모리 확보
            double[,,] tmpInput = new double[RGB, inH + 2, inW + 2];
            double[,,] tmpOutput = new double[RGB, outH, outW];
            // 임시 입력을 초기화 (0, 127, 평균값)
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH + 2; i++)
                    for (int k = 0; k < inW + 2; k++)
                        tmpInput[rgb, i, k] = 127.0;
            // 입력 --> 임시 입력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpInput[rgb, i + 1, k + 1] = inImage[rgb, i, k];

            // 회선 연산
            if (processType == '0')
            {
                sx = 0; ex = inH;
                sy = 0; ey = inW;
            }

            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                {
                    for (int k = 0; k < inW; k++)
                    {
                        if (mouseStatus == "None")
                        {
                            double S = 0.0;
                            for (int m = 0; m < MSIZE; m++)
                                for (int n = 0; n < MSIZE; n++)
                                    S += mask[m, n] * tmpInput[rgb, m + i, n + k];

                            tmpOutput[rgb, i, k] = S;
                        }
                        else if (mouseStatus == "Box")
                        {
                            if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                            {
                                double S = 0.0;
                                for (int m = 0; m < MSIZE; m++)
                                    for (int n = 0; n < MSIZE; n++)
                                        S += mask[m, n] * tmpInput[rgb, m + i, n + k];

                                tmpOutput[rgb, i, k] = S;
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                        else if (mouseStatus == "Free")
                        {
                            if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                            {
                                if (pointInPolygon(k, i, mx, my))
                                {
                                    double S = 0.0;
                                    for (int m = 0; m < MSIZE; m++)
                                        for (int n = 0; n < MSIZE; n++)
                                            S += mask[m, n] * tmpInput[rgb, m + i, n + k];

                                    tmpOutput[rgb, i, k] = S;
                                }
                                else
                                    tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                    }
                }
            // 마스크의 합계가 0이면, 127 정도를 더해주기.
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < outH; i++)
                    for (int k = 0; k < outW; k++)
                    {
                        if (mouseStatus == "None")
                        {
                            tmpOutput[rgb, i, k] += 127;
                        }
                        else if (mouseStatus == "Box")
                        {
                            if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                            {
                                tmpOutput[rgb, i, k] += 127;
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                        else if (mouseStatus == "Free")
                        {
                            if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                            {
                                if (pointInPolygon(k, i, mx, my))
                                {
                                    tmpOutput[rgb, i, k] += 127;
                                }
                                else
                                    tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                    }
            // 임시 출력 --> 출력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < outH; i++)
                    for (int k = 0; k < outW; k++)
                    {
                        if (tmpOutput[rgb, i, k] < 0)
                            outImage[rgb, i, k] = 0;
                        else if (tmpOutput[rgb, i, k] > 255)
                            outImage[rgb, i, k] = 255;
                        else
                            outImage[rgb, i, k] = (byte)(tmpOutput[rgb, i, k]);
                    }
            ////////////////////////////
            displayImage();
        }


        void blurr()
        {
            if (inImage == null)
            {
                return;
            }
            // 중요! 출력 영상의 크기를 결정 --> 알고리즘에 따라서....
            outH = inH;
            outW = inW;
            // 출력 영상 메모리 할당.
            outImage = new byte[RGB, outH, outW];

            // ** 영상처리 알고리즘 **
            const int MSIZE = 3;
            double[,] mask = { { 0.11, 0.11, 0.11 },
                                {0.11, 0.11, 0.11 },
                                {0.11, 0.11, 0.11 }}; //블러링 마스크

            //임시 입출력 메모리 확보
            double[,,] tmpInput = new double[RGB, inH + 2, inW + 2];
            double[,,] tmpOutput = new double[RGB, outH, outW];

            //임시 입력을 초기화(0, 127, 평균값)
            for(int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH + 2; i++)
                    for (int k = 0; k < inW + 2; k++)
                        tmpInput[rgb, i, k] = 127.0;

            //입력 --> 임시 입력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpInput[rgb, i + 1, k + 1] = inImage[rgb, i, k];

            //회선 연산
            if (processType == '0')
            {
                sx = 0; ex = inH;
                sy = 0; ey = inW;
            }

            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        if (mouseStatus == "None")
                        {
                            double S = 0.0;
                            for (int m = 0; m < MSIZE; m++)
                                for (int n = 0; n < MSIZE; n++)
                                {
                                    S += mask[m, n] * tmpInput[rgb, m + i, n + k];
                                }
                            tmpOutput[rgb, i, k] = S;
                        }
                        else if (mouseStatus == "Box")
                        {
                            if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                            {
                                double S = 0.0;
                                for (int m = 0; m < MSIZE; m++)
                                    for (int n = 0; n < MSIZE; n++)
                                    {
                                        S += mask[m, n] * tmpInput[rgb, m + i, n + k];
                                    }
                                tmpOutput[rgb, i, k] = S;
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                        else if (mouseStatus == "Free")
                        {
                            if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                            {
                                if (pointInPolygon(k, i, mx, my))
                                {
                                    double S = 0.0;
                                    for (int m = 0; m < MSIZE; m++)
                                        for (int n = 0; n < MSIZE; n++)
                                        {
                                            S += mask[m, n] * tmpInput[rgb, m + i, n + k];
                                        }
                                    tmpOutput[rgb, i, k] = S;
                                }
                                else
                                    tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                    }


            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < outH; i++)
                    for (int k = 0; k < outW; k++)
                    {
                        if (tmpOutput[rgb, i, k] < 0)
                            outImage[rgb, i, k] = 0;
                        else if (tmpOutput[rgb, i, k] > 255)
                            outImage[rgb, i, k] = 255;
                        else
                            outImage[rgb, i, k] = (byte)tmpOutput[rgb, i, k];
                    }

            displayImage();
        }


        void sharpening()
        {
            if (inImage == null)
            {
                return;
            }
            // 중요! 출력 영상의 크기를 결정 --> 알고리즘에 따라서....
            outH = inH;
            outW = inW;
            // 출력 영상 메모리 할당.
            outImage = new byte[RGB, outH, outW];

            // ** 영상처리 알고리즘 **
            const int MSIZE = 3;
            //double[,] mask = { { 0, -1, 0 },
            //                    {-1, 5, -1 },
            //                    {0, -1, 0 }}; //샤프닝 마스크

            double[,] mask = { { -1, -1, -1 },
                                {-1, 9, -1 },
                                {-1, -1, -1 }}; //샤프닝 마스크

            //임시 입출력 메모리 확보
            double[,,] tmpInput = new double[RGB, inH + 2, inW + 2];
            double[,,] tmpOutput = new double[RGB, outH, outW];

            //임시 입력을 초기화(0, 127, 평균값)
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH + 2; i++)
                    for (int k = 0; k < inW + 2; k++)
                        tmpInput[rgb, i, k] = 127.0;

            //입력 --> 임시 입력
            for(int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpInput[rgb, i + 1, k + 1] = inImage[rgb, i, k];

            //회선 연산
            if (processType == '0')
            {
                sx = 0; ex = inH;
                sy = 0; ey = inW;
            }

            //회선 연산
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        if (mouseStatus == "None")
                        {
                            double S = 0.0;
                            for (int m = 0; m < MSIZE; m++)
                                for (int n = 0; n < MSIZE; n++)
                                {
                                    S += mask[m, n] * tmpInput[rgb, m + i, n + k];
                                }
                            tmpOutput[rgb, i, k] = S;
                        }
                        else if (mouseStatus == "Box")
                        {
                            if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                            {
                                double S = 0.0;
                                for (int m = 0; m < MSIZE; m++)
                                    for (int n = 0; n < MSIZE; n++)
                                    {
                                        S += mask[m, n] * tmpInput[rgb, m + i, n + k];
                                    }
                                tmpOutput[rgb, i, k] = S;
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                        else if (mouseStatus == "Free")
                        {
                            if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                            {
                                if (pointInPolygon(k, i, mx, my))
                                {
                                    double S = 0.0;
                                    for (int m = 0; m < MSIZE; m++)
                                        for (int n = 0; n < MSIZE; n++)
                                        {
                                            S += mask[m, n] * tmpInput[rgb, m + i, n + k];
                                        }
                                    tmpOutput[rgb, i, k] = S;
                                }
                                else
                                    tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                    }
                

            ////마스크의 합계가 0이면, 127 정도 더해주기.
            //for (int i = 0; i < outH; i++)
            //{
            //    for (int k = 0; k < outW; k++)
            //    {
            //        tmpOutput[i, k] += 127;
            //    }
            //}
            //임시 출력 -> 출력
            for(int rgb = 0; rgb <RGB; rgb++)  
                for (int i = 0; i < outH; i++)
                    for (int k = 0; k < outW; k++)
                    {
                        if (tmpOutput[rgb, i, k] < 0)
                            outImage[rgb, i, k] = 0;
                        else if (tmpOutput[rgb, i, k] > 255)
                            outImage[rgb, i, k] = 255;
                        else
                            outImage[rgb, i, k] = (byte)tmpOutput[rgb, i, k];
                    }

            displayImage();
        }


        void hpfSharp()
        {
            if (inImage == null)
            {
                return;
            }
            // 중요! 출력 영상의 크기를 결정 --> 알고리즘에 따라서....
            outH = inH;
            outW = inW;
            // 출력 영상 메모리 할당.
            outImage = new byte[RGB, outH, outW];

            // ** 영상처리 알고리즘 **
            const int MSIZE = 3;
            //double[,] mask = { { 0, -1, 0 },
            //                    {-1, 5, -1 },
            //                    {0, -1, 0 }}; //고주파 필터 샤프닝 마스크

            double[,] mask = {{-0.11, -0.11, -0.11},
                               {-0.11, 0.9, -0.11},
                               {-0.11, -0.11, -0.11}}; //샤프닝 마스크

            //임시 입출력 메모리 확보
            double[,,] tmpInput = new double[RGB, inH + 2, inW + 2];
            double[,,] tmpOutput = new double[RGB, outH, outW];

            //임시 입력을 초기화(0, 127, 평균값)
            for(int rgb = 0;rgb < RGB; rgb++)
                for (int i = 0; i < inH + 2; i++)
                    for (int k = 0; k < inW + 2; k++)
                        tmpInput[rgb, i, k] = 127.0;

            //입력 --> 임시 입력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpInput[rgb, i + 1, k + 1] = inImage[rgb, i, k];

            //회선 연산
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < inH; i++)
                {
                    for (int k = 0; k < inW; k++)
                    {
                        if (mouseStatus == "None")
                        {
                            double S = 0.0;
                            for (int m = 0; m < MSIZE; m++)
                                for (int n = 0; n < MSIZE; n++)
                                {
                                    S += mask[m, n] * tmpInput[rgb, m + i, n + k];
                                }
                            tmpOutput[rgb, i, k] = S;
                        }
                        else if (mouseStatus == "Box")
                        {
                            if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                            {
                                double S = 0.0;
                                for (int m = 0; m < MSIZE; m++)
                                    for (int n = 0; n < MSIZE; n++)
                                    {
                                        S += mask[m, n] * tmpInput[rgb, m + i, n + k];
                                    }
                                tmpOutput[rgb, i, k] = S;
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                        else if (mouseStatus == "Free")
                        {
                            if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                            {
                                if (pointInPolygon(k, i, mx, my))
                                {
                                    double S = 0.0;
                                    for (int m = 0; m < MSIZE; m++)
                                        for (int n = 0; n < MSIZE; n++)
                                        {
                                            S += mask[m, n] * tmpInput[rgb, m + i, n + k];
                                        }
                                    tmpOutput[rgb, i, k] = S;
                                }
                                else
                                {
                                    tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                                }
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                    }
                }
            }

            ////마스크의 합계가 0이면, 127 정도 더해주기.
            //for (int i = 0; i < outH; i++)
            //{
            //    for (int k = 0; k < outW; k++)
            //    {
            //        tmpOutput[i, k] += 127;
            //    }
            //}
            //임시 출력 -> 출력
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < outH; i++)
                {
                    for (int k = 0; k < outW; k++)
                    {
                        if (tmpOutput[rgb, i, k] < 0)
                            outImage[rgb, i, k] = 0;
                        else if (tmpOutput[rgb, i, k] > 255)
                            outImage[rgb, i, k] = 255;
                        else
                            outImage[rgb, i, k] = (byte)tmpOutput[rgb, i, k];
                    }
                }
            }
            displayImage();
        }


        void vert_hori_edge()
        {
            if (inImage == null)
                return;
            //중요! 출력영상의 크기를 결정 --> 알고리즘에 따라서 ....
            outH = inH;
            outW = inW;
            //출력영상 메모리 할당
            outImage = new byte[RGB, outH, outW];
            //  ** 영상처리 알고리즘 **

            const int MSIZE = 3;
            double[,] mask;

            mask = new double[,]{ {0,0,0},
                               {-1,1,0},
                               {0,0,0} };//수직에지검출마스크1

            //mask = new double[,]{ {0,-1,0},
            //                   {0,1,0},
            //                   {0,0,0} };//수평에지검출마스크2

            //입출력 메모리확보
            double[,,] tmpInput = new double[RGB, inH + 2, inW + 2];
            double[,,] tmpOutput = new double[RGB, outH, outW];
            //임시 입력을 초기화
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < inH + 2; i++)
                {
                    for (int k = 0; k < inW + 2; k++)
                    {
                        tmpInput[rgb, i, k] = 127.0;
                    }
                }
            }
            //입력-->임시입력
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < inH; i++)
                {
                    for (int k = 0; k < inW; k++)
                    {
                        tmpInput[rgb, i + 1, k + 1] = inImage[rgb, i, k];
                    }
                }
            }
            //회선연산
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < inH; i++)
                {
                    for (int k = 0; k < inW; k++)
                    {
                        if (mouseStatus == "None")
                        {
                            double S = 0.0;
                            for (int m = 0; m < MSIZE; m++)
                            {
                                for (int n = 0; n < MSIZE; n++)
                                {
                                    S += mask[m, n] * tmpInput[rgb, i + m, k + n];
                                }
                            }
                            tmpOutput[rgb, i, k] = S;
                        }
                        else if (mouseStatus == "Box")
                        {
                            if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                            {
                                double S = 0.0;
                                for (int m = 0; m < MSIZE; m++)
                                {
                                    for (int n = 0; n < MSIZE; n++)
                                    {
                                        S += mask[m, n] * tmpInput[rgb, i + m, k + n];
                                    }
                                }
                                tmpOutput[rgb, i, k] = S;
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                        else if (mouseStatus == "Free")
                        {
                            if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                            {
                                if (pointInPolygon(k, i, mx, my))
                                {
                                    double S = 0.0;
                                    for (int m = 0; m < MSIZE; m++)
                                    {
                                        for (int n = 0; n < MSIZE; n++)
                                        {
                                            S += mask[m, n] * tmpInput[rgb, i + m, k + n];
                                        }
                                    }
                                    tmpOutput[rgb, i, k] = S;
                                }
                                else
                                {
                                    tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                                }
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                    }
                }
            }
            //마스크의 합계가 0이면 127정도를 더해주기
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < outH; i++)
                    for (int k = 0; k < outW; k++)
                    {
                        if (mouseStatus == "None")
                        {
                            tmpOutput[rgb, i, k] += 127;
                        }
                        else if (mouseStatus == "Box")
                        {
                            if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                            {
                                tmpOutput[rgb, i, k] += 127;
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                        else if (mouseStatus == "Free")
                        {
                            if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                            {
                                if (pointInPolygon(k, i, mx, my))
                                {
                                    tmpOutput[rgb, i, k] += 127;
                                }
                                else
                                    tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                    }

            //임시출력 --> 출력
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < outH; i++)
                {
                    for (int k = 0; k < outW; k++)
                    {
                        if (tmpOutput[rgb, i, k] < 0)
                        {
                            outImage[rgb, i, k] = 0;
                        }
                        else if (tmpOutput[rgb, i, k] > 255)
                        {
                            outImage[rgb, i, k] = 255;
                        }
                        else
                        {
                            outImage[rgb, i, k] = (byte)tmpOutput[rgb, i, k];
                        }
                    }
                }
            }
            displayImage();
        }


        void LoG()
        {
            if (inImage == null)
                return;
            //중요! 출력영상의 크기를 결정 --> 알고리즘에 따라서 ....
            outH = inH;
            outW = inW;
            //출력영상 메모리 할당
            outImage = new byte[RGB, outH, outW];
            //  ** 영상처리 알고리즘 **
            const int MSIZE = 5;
            double[,] mask = { { 0.0, 0.0, -1, 0.0, 0.0 },
                        { 0.0, -1, -2, -1, 0.0 },
                        { -1, -2, 16, -2, -1 },
                        { 0.0, -1, -2, -1, 0.0 },
                        { 0.0, 0.0, -1, 0.0, 0.0 } };
            //입출력 메모리확보
            double[,,] tmpInput = new double[RGB, inH + 4, inW + 4];
            double[,,] tmpOutput = new double[RGB, outH, outW];
            //임시 입력을 초기화
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < inH + 4; i++)
                {
                    for (int k = 0; k < inW + 4; k++)
                    {
                        tmpInput[rgb, i, k] = 127.0;
                    }
                }
            }
            //입력-->임시입력
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < inH; i++)
                {
                    for (int k = 0; k < inW; k++)
                    {
                        tmpInput[rgb, i + 2, k + 2] = inImage[rgb, i, k];
                    }
                }
            }
            //회선연산
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < inH; i++)
                {
                    for (int k = 0; k < inW; k++)
                    {
                        if (mouseStatus == "None")
                        {
                            double S = 0.0;
                            for (int m = 0; m < MSIZE; m++)
                            {
                                for (int n = 0; n < MSIZE; n++)
                                {
                                    S += mask[m, n] * tmpInput[rgb, i + m, k + n];
                                }
                            }
                            tmpOutput[rgb, i, k] = S;
                        }
                        else if (mouseStatus == "Box")
                        {
                            if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                            {
                                double S = 0.0;
                                for (int m = 0; m < MSIZE; m++)
                                {
                                    for (int n = 0; n < MSIZE; n++)
                                    {
                                        S += mask[m, n] * tmpInput[rgb, i + m, k + n];
                                    }
                                }
                                tmpOutput[rgb, i, k] = S;
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 2, k + 2];
                            }
                        }
                        else if (mouseStatus == "Free")
                        {
                            if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                            {
                                if (pointInPolygon(k, i, mx, my))
                                {
                                    double S = 0.0;
                                    for (int m = 0; m < MSIZE; m++)
                                    {
                                        for (int n = 0; n < MSIZE; n++)
                                        {
                                            S += mask[m, n] * tmpInput[rgb, i + m, k + n];
                                        }
                                    }
                                    tmpOutput[rgb, i, k] = S;
                                }
                                else
                                {
                                    tmpOutput[rgb, i, k] = tmpInput[rgb, i + 2, k + 2];
                                }
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 2, k + 2];
                            }
                        }
                    }
                }
            }
            //마스크의 합계에 따라서 127정도를 더해주기
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < outH; i++)
                    for (int k = 0; k < outW; k++)
                    {
                        if (mouseStatus == "None")
                        {
                            tmpOutput[rgb, i, k] += 127;
                        }
                        else if (mouseStatus == "Box")
                        {
                            if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                            {
                                tmpOutput[rgb, i, k] += 127;
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                        else if (mouseStatus == "Free")
                        {
                            if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                            {
                                if (pointInPolygon(k, i, mx, my))
                                {
                                    tmpOutput[rgb, i, k] += 127;
                                }
                                else
                                    tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                    }

            //임시출력 --> 출력
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < outH; i++)
                {
                    for (int k = 0; k < outW; k++)
                    {
                        if (tmpOutput[rgb, i, k] < 0)
                        {
                            outImage[rgb, i, k] = 0;
                        }
                        else if (tmpOutput[rgb, i, k] > 255)
                        {
                            outImage[rgb, i, k] = 255;
                        }
                        else
                        {
                            outImage[rgb, i, k] = (byte)tmpOutput[rgb, i, k];
                        }
                    }
                }
            }
            displayImage();
        }



        void DoG()
        {
            if (inImage == null)
                return;
            //중요! 출력영상의 크기를 결정 --> 알고리즘에 따라서 ....
            outH = inH;
            outW = inW;
            //출력영상 메모리 할당
            outImage = new byte[RGB, outH, outW];
            //  ** 영상처리 알고리즘 **
            const int MSIZE = 7;
            double[,] mask = { { 0.0, 0.0, -1.0, -1.0, -1.0, 0.0, 0.0 },
                        { 0.0, -2.0, -3.0, -3.0, -3.0, -2.0, 0.0 },
                        { -1.0, -3.0, 5.0, 5.0, 5.0, -3.0, -1.0 },
                        { -1.0, -3.0, 5.0, 16.0, 5.0, -3.0, -1.0 },
                        { -1.0, -3.0, 5.0, 5.0, 5.0, -3.0, -1.0 },
                        { 0.0, -2.0, -3.0, -3.0, -3.0, -2.0, 0.0 },
                        { 0.0, 0.0, -1.0, -1.0, -1.0, 0.0, 0.0 } };
            //입출력 메모리확보
            double[,,] tmpInput = new double[RGB, inH + 6, inW + 6];
            double[,,] tmpOutput = new double[RGB, outH, outW];
            //임시 입력을 초기화
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < inH + 6; i++)
                {
                    for (int k = 0; k < inW + 6; k++)
                    {
                        tmpInput[rgb, i, k] = 127.0;
                    }
                }
            }
            //입력-->임시입력
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < inH; i++)
                {
                    for (int k = 0; k < inW; k++)
                    {
                        tmpInput[rgb, i + 3, k + 3] = inImage[rgb, i, k];
                    }
                }
            }
            //회선연산
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < inH; i++)
                {
                    for (int k = 0; k < inW; k++)
                    {
                        if (mouseStatus == "None")
                        {
                            double S = 0.0;
                            for (int m = 0; m < MSIZE; m++)
                            {
                                for (int n = 0; n < MSIZE; n++)
                                {
                                    S += mask[m, n] * tmpInput[rgb, i + m, k + n];
                                }
                            }
                            tmpOutput[rgb, i, k] = S;
                        }
                        else if (mouseStatus == "Box")
                        {
                            if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                            {
                                double S = 0.0;
                                for (int m = 0; m < MSIZE; m++)
                                {
                                    for (int n = 0; n < MSIZE; n++)
                                    {
                                        S += mask[m, n] * tmpInput[rgb, i + m, k + n];
                                    }
                                }
                                tmpOutput[rgb, i, k] = S;
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 3, k + 3];
                            }
                        }
                        else if (mouseStatus == "Free")
                        {
                            if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                            {
                                if (pointInPolygon(k, i, mx, my))
                                {
                                    double S = 0.0;
                                    for (int m = 0; m < MSIZE; m++)
                                    {
                                        for (int n = 0; n < MSIZE; n++)
                                        {
                                            S += mask[m, n] * tmpInput[rgb, i + m, k + n];
                                        }
                                    }
                                    tmpOutput[rgb, i, k] = S;
                                }
                                else
                                {
                                    tmpOutput[rgb, i, k] = tmpInput[rgb, i + 3, k + 3];
                                }
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 3, k + 3];
                            }
                        }

                        ////////////////////////////
                        //double S = 0.0;
                        //for (int m = 0; m < MSIZE; m++)
                        //{
                        //    for (int n = 0; n < MSIZE; n++)
                        //    {
                        //        S += mask[m, n] * tmpInput[rgb, i + m, k + n];
                        //    }
                        //}
                        //tmpOutput[rgb, i, k] = S;
                    }
                }
            }
            //마스크의 합계에 따라서 127정도를 더해주기
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < outH; i++)
                    for (int k = 0; k < outW; k++)
                    {
                        if (mouseStatus == "None")
                        {
                            tmpOutput[rgb, i, k] += 127;
                        }
                        else if (mouseStatus == "Box")
                        {
                            if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                            {
                                tmpOutput[rgb, i, k] += 127;
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                        else if (mouseStatus == "Free")
                        {
                            if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                            {
                                if (pointInPolygon(k, i, mx, my))
                                {
                                    tmpOutput[rgb, i, k] += 127;
                                }
                                else
                                    tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                            else
                            {
                                tmpOutput[rgb, i, k] = tmpInput[rgb, i + 1, k + 1];
                            }
                        }
                    }
            //임시출력 --> 출력
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < outH; i++)
                {
                    for (int k = 0; k < outW; k++)
                    {
                        if (tmpOutput[rgb, i, k] < 0)
                        {
                            outImage[rgb, i, k] = 0;
                        }
                        else if (tmpOutput[rgb, i, k] > 255)
                        {
                            outImage[rgb, i, k] = 255;
                        }
                        else
                        {
                            outImage[rgb, i, k] = (byte)tmpOutput[rgb, i, k];
                        }
                    }
                }
            }
            displayImage();
        }

        void histo_stretch()//스트레칭 영상 처리
        {
            if (inImage == null)
            {
                return;
            }
            // 중요! 출력 영상의 크기를 결정 --> 알고리즘에 따라서....
            outH = inH;
            outW = inW;
            // 출력 영상 메모리 할당.
            outImage = new byte[RGB, outH, outW];

            // ** 영상처리 알고리즘 **
            // out = (in - low) / (high - low) * 255
            byte[] low = new byte[RGB];
            byte[] high = new byte[RGB];
            for(int rgb = 0; rgb < RGB;rgb++)
            {
                low[rgb] = inImage[rgb, 0, 0];
                high[rgb] = inImage[rgb, 0, 0];
            }

            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        if (inImage[rgb, i, k] < low[rgb])
                            low[rgb] = inImage[rgb, i, k];
                        if (inImage[rgb, i, k] > high[rgb])
                            high[rgb] = inImage[rgb, i, k];
                    }

            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < inH; i++)
                {
                    for (int k = 0; k < inW; k++)
                    {
                        if (mouseStatus == "None")
                        {
                            double outValue = (inImage[rgb, i, k] - (double)low[rgb]) / (high[rgb] - (double)low[rgb]) * 255;
                            if (outValue < 0)
                                outValue = 0.0;
                            else if (outValue > 255)
                                outValue = 255.0;

                            outImage[rgb, i, k] = (byte)outValue;
                        }
                        else if (mouseStatus == "Box")
                        {
                            if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                            {
                                double outValue = (inImage[rgb, i, k] - (double)low[rgb]) / (high[rgb] - (double)low[rgb]) * 255;
                                if (outValue < 0)
                                    outValue = 0.0;
                                else if (outValue > 255)
                                    outValue = 255.0;

                                outImage[rgb, i, k] = (byte)outValue;
                            }
                            else
                            {
                                outImage[rgb, i, k] = inImage[rgb, i, k];
                            }
                        }
                        else if (mouseStatus == "Free")
                        {
                            if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                            {
                                if (pointInPolygon(k, i, mx, my)) 
                                {
                                    double outValue = (inImage[rgb, i, k] - (double)low[rgb]) / (high[rgb] - (double)low[rgb]) * 255;
                                    if (outValue < 0)
                                        outValue = 0.0;
                                    else if (outValue > 255)
                                        outValue = 255.0;

                                    outImage[rgb, i, k] = (byte)outValue;
                                }
                                else 
                                {
                                    outImage[rgb, i, k] = inImage[rgb, i, k];
                                }  
                            }
                            else
                            {
                                double outValue = (inImage[rgb, i, k] - (double)low[rgb]) / (high[rgb] - (double)low[rgb]) * 255;
                                if (outValue < 0)
                                    outValue = 0.0;
                                else if (outValue > 255)
                                    outValue = 255.0;

                                outImage[rgb, i, k] = (byte)outValue;
                            }
                        }
                    }
                }
            }
            displayImage();
        }

        void end_in()
        {
            if (inImage == null)
            {
                return;
            }
            // 중요! 출력 영상의 크기를 결정 --> 알고리즘에 따라서....
            outH = inH;
            outW = inW;
            // 출력 영상 메모리 할당.
            outImage = new byte[RGB, outH, outW];

            // ** 영상처리 알고리즘 **
            // out = (in - low) / (high - low) * 255
            int[] low = new int[RGB];
            int[] high = new int[RGB];

            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < inH; i++)
                {
                    for (int k = 0; k < inW; k++)
                    {
                        if (inImage[rgb, i, k] < low[rgb])
                            low[rgb] = inImage[rgb, i, k];
                        if (inImage[rgb, i, k] > high[rgb])
                            high[rgb] = inImage[rgb, i, k];
                    }
                }
                low[rgb] += 50;
                high[rgb] += 50;
            }


            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < inH; i++)
                {
                    for (int k = 0; k < inW; k++)
                    {
                        if (mouseStatus == "None")
                        {
                            double outValue = (inImage[rgb, i, k] - (double)low[rgb]) / (high[rgb] - low[rgb]) * 255;
                            if (outValue < 0)
                                outValue = 0.0;
                            else if (outValue > 255)
                                outValue = 255.0;

                            outImage[rgb, i, k] = (byte)outValue;
                        }
                        else if (mouseStatus == "Box")
                        {
                            if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                            {
                                double outValue = (inImage[rgb, i, k] - (double)low[rgb]) / (high[rgb] - low[rgb]) * 255;
                                if (outValue < 0)
                                    outValue = 0.0;
                                else if (outValue > 255)
                                    outValue = 255.0;

                                outImage[rgb, i, k] = (byte)outValue;
                            }
                            else
                            {
                                outImage[rgb, i, k] = inImage[rgb, i, k];
                            }
                        }
                        else if (mouseStatus == "Free")
                        {
                            if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                            {
                                if (pointInPolygon(k, i, mx, my))
                                {
                                    double outValue = (inImage[rgb, i, k] - (double)low[rgb]) / (high[rgb] - low[rgb]) * 255;
                                    if (outValue < 0)
                                        outValue = 0.0;
                                    else if (outValue > 255)
                                        outValue = 255.0;

                                    outImage[rgb, i, k] = (byte)outValue;
                                }
                                else
                                {
                                    outImage[rgb,i,k] = inImage[rgb,i,k];
                                }
                            }
                            else
                            {
                                outImage[rgb, i, k] = inImage[rgb, i, k];
                            }
                        }
                    }
                }
            }
            displayImage();
        }


        void histo_equalize()
        {
            if (inImage == null)
            {
                return;
            }
            // 중요! 출력 영상의 크기를 결정 --> 알고리즘에 따라서....
            outH = inH;
            outW = inW;
            // 출력 영상 메모리 할당.
            outImage = new byte[RGB, outH, outW];

            // ** 영상처리 알고리즘 **
            //1단계 : 히스토그램 생성
            int[,] hist = new int[RGB, 256];

            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < 256; i++)
                {
                    hist[rgb, i] = 0;
                }
            }

            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < inH; i++)
                {
                    for (int k = 0; k < inW; k++)
                    {
                        hist[rgb, inImage[rgb, i, k]] += 1;
                    }
                }
            }

            //2단계 : 누적 히스토그램 생성
            int[,] sumHist = new int[RGB, 256];
            int sValue = 0;

            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < 256; i++)
                {
                    sValue += hist[rgb, i];
                    sumHist[rgb, i] = sValue;
                }
            }

            //3단계 : 정규화된 누적히스토그램 생성
            //n = (sumHist / (행, 열)) * 255.0
            double[,] normalHist = new double[RGB,256];

            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < 256; i++)
                {
                    normalHist[rgb, i] = (sumHist[rgb, i] / (double)(inH * inW)) * 255.0;
                }
            }

            for (int rgb = 0; rgb < RGB; rgb++)
            {
                for (int i = 0; i < inH; i++)
                {
                    for (int k = 0; k < inW; k++)
                    {
                        if (mouseStatus == "None")
                        {
                            outImage[rgb, i, k] = (byte)normalHist[rgb, inImage[rgb, i, k]];
                        }
                        else if (mouseStatus == "Box")
                        {
                            if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                            {
                                outImage[rgb, i, k] = (byte)normalHist[rgb, inImage[rgb, i, k]];
                            }
                            else
                            {
                                outImage[rgb, i, k] = inImage[rgb, i, k];
                            }
                        }
                        else if (mouseStatus == "Free")
                        {
                            if ((minX <= k && k <= maxX) && (minY <= i && i <= maxY))
                            {
                                if (pointInPolygon(k, i, mx, my))
                                {
                                    outImage[rgb, i, k] = (byte)normalHist[rgb, inImage[rgb, i, k]];
                                }
                                else
                                {
                                    outImage[rgb, i, k] = inImage[rgb, i, k];
                                }
                            }
                            else
                            {
                                outImage[rgb, i, k] = inImage[rgb, i, k];
                            }
                        }
                    }
                }
            }

            displayImage();
        }
    }
}
