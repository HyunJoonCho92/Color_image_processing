namespace Day16_01_Color_Image_Processing__Beta_1_
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.파일ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.열기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.저장ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.종료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.화소점처리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.동일이미지ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.반전이미지ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.밝게어둡게ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.흑백127기준ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.그레이스케일ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.파라볼라ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.감마ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.채도변경ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.색상추출ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.기하학처리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.이동ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.좌우반전ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.상하반전ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.축소ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.확대ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.회전역방향ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.화소영역처리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.엠보싱ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.블러링ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.샤프닝ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.고주파ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.수평수직ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.히스토그램ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.스트레칭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.엔드인ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.평활화ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파일ToolStripMenuItem,
            this.화소점처리ToolStripMenuItem,
            this.기하학처리ToolStripMenuItem,
            this.화소영역처리ToolStripMenuItem,
            this.히스토그램ToolStripMenuItem,
            this.toolStripComboBox1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(850, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 파일ToolStripMenuItem
            // 
            this.파일ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.열기ToolStripMenuItem,
            this.저장ToolStripMenuItem,
            this.종료ToolStripMenuItem});
            this.파일ToolStripMenuItem.Name = "파일ToolStripMenuItem";
            this.파일ToolStripMenuItem.Size = new System.Drawing.Size(53, 28);
            this.파일ToolStripMenuItem.Text = "파일";
            // 
            // 열기ToolStripMenuItem
            // 
            this.열기ToolStripMenuItem.Name = "열기ToolStripMenuItem";
            this.열기ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.열기ToolStripMenuItem.Text = "열기 Ctrl+O";
            this.열기ToolStripMenuItem.Click += new System.EventHandler(this.열기ToolStripMenuItem_Click);
            // 
            // 저장ToolStripMenuItem
            // 
            this.저장ToolStripMenuItem.Name = "저장ToolStripMenuItem";
            this.저장ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.저장ToolStripMenuItem.Text = "저장 Ctrl+S";
            this.저장ToolStripMenuItem.Click += new System.EventHandler(this.저장ToolStripMenuItem_Click);
            // 
            // 종료ToolStripMenuItem
            // 
            this.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
            this.종료ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.종료ToolStripMenuItem.Text = "종료";
            this.종료ToolStripMenuItem.Click += new System.EventHandler(this.종료ToolStripMenuItem_Click);
            // 
            // 화소점처리ToolStripMenuItem
            // 
            this.화소점처리ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.동일이미지ToolStripMenuItem,
            this.반전이미지ToolStripMenuItem,
            this.밝게어둡게ToolStripMenuItem,
            this.흑백127기준ToolStripMenuItem,
            this.그레이스케일ToolStripMenuItem,
            this.파라볼라ToolStripMenuItem,
            this.감마ToolStripMenuItem,
            this.채도변경ToolStripMenuItem,
            this.색상추출ToolStripMenuItem});
            this.화소점처리ToolStripMenuItem.Name = "화소점처리ToolStripMenuItem";
            this.화소점처리ToolStripMenuItem.Size = new System.Drawing.Size(103, 28);
            this.화소점처리ToolStripMenuItem.Text = "화소점 처리";
            // 
            // 동일이미지ToolStripMenuItem
            // 
            this.동일이미지ToolStripMenuItem.Name = "동일이미지ToolStripMenuItem";
            this.동일이미지ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.동일이미지ToolStripMenuItem.Text = "동일 이미지";
            this.동일이미지ToolStripMenuItem.Click += new System.EventHandler(this.동일이미지ToolStripMenuItem_Click);
            // 
            // 반전이미지ToolStripMenuItem
            // 
            this.반전이미지ToolStripMenuItem.Name = "반전이미지ToolStripMenuItem";
            this.반전이미지ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.반전이미지ToolStripMenuItem.Text = "반전 이미지";
            this.반전이미지ToolStripMenuItem.Click += new System.EventHandler(this.반전이미지ToolStripMenuItem_Click);
            // 
            // 밝게어둡게ToolStripMenuItem
            // 
            this.밝게어둡게ToolStripMenuItem.Name = "밝게어둡게ToolStripMenuItem";
            this.밝게어둡게ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.밝게어둡게ToolStripMenuItem.Text = "밝게/어둡게";
            this.밝게어둡게ToolStripMenuItem.Click += new System.EventHandler(this.밝게어둡게ToolStripMenuItem_Click);
            // 
            // 흑백127기준ToolStripMenuItem
            // 
            this.흑백127기준ToolStripMenuItem.Name = "흑백127기준ToolStripMenuItem";
            this.흑백127기준ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.흑백127기준ToolStripMenuItem.Text = "흑백(127기준)";
            this.흑백127기준ToolStripMenuItem.Click += new System.EventHandler(this.흑백127기준ToolStripMenuItem_Click);
            // 
            // 그레이스케일ToolStripMenuItem
            // 
            this.그레이스케일ToolStripMenuItem.Name = "그레이스케일ToolStripMenuItem";
            this.그레이스케일ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.그레이스케일ToolStripMenuItem.Text = "그레이 스케일";
            this.그레이스케일ToolStripMenuItem.Click += new System.EventHandler(this.그레이스케일ToolStripMenuItem_Click);
            // 
            // 파라볼라ToolStripMenuItem
            // 
            this.파라볼라ToolStripMenuItem.Name = "파라볼라ToolStripMenuItem";
            this.파라볼라ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.파라볼라ToolStripMenuItem.Text = "파라볼라";
            this.파라볼라ToolStripMenuItem.Click += new System.EventHandler(this.파라볼라ToolStripMenuItem_Click);
            // 
            // 감마ToolStripMenuItem
            // 
            this.감마ToolStripMenuItem.Name = "감마ToolStripMenuItem";
            this.감마ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.감마ToolStripMenuItem.Text = "감마";
            this.감마ToolStripMenuItem.Click += new System.EventHandler(this.감마ToolStripMenuItem_Click);
            // 
            // 채도변경ToolStripMenuItem
            // 
            this.채도변경ToolStripMenuItem.Name = "채도변경ToolStripMenuItem";
            this.채도변경ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.채도변경ToolStripMenuItem.Text = "채도변경";
            this.채도변경ToolStripMenuItem.Click += new System.EventHandler(this.채도변경ToolStripMenuItem_Click);
            // 
            // 색상추출ToolStripMenuItem
            // 
            this.색상추출ToolStripMenuItem.Name = "색상추출ToolStripMenuItem";
            this.색상추출ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.색상추출ToolStripMenuItem.Text = "색상 추출";
            this.색상추출ToolStripMenuItem.Click += new System.EventHandler(this.색상추출ToolStripMenuItem_Click);
            // 
            // 기하학처리ToolStripMenuItem
            // 
            this.기하학처리ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.이동ToolStripMenuItem,
            this.좌우반전ToolStripMenuItem,
            this.상하반전ToolStripMenuItem,
            this.축소ToolStripMenuItem,
            this.확대ToolStripMenuItem,
            this.회전역방향ToolStripMenuItem});
            this.기하학처리ToolStripMenuItem.Name = "기하학처리ToolStripMenuItem";
            this.기하학처리ToolStripMenuItem.Size = new System.Drawing.Size(103, 28);
            this.기하학처리ToolStripMenuItem.Text = "기하학 처리";
            // 
            // 이동ToolStripMenuItem
            // 
            this.이동ToolStripMenuItem.Name = "이동ToolStripMenuItem";
            this.이동ToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.이동ToolStripMenuItem.Text = "이동";
            this.이동ToolStripMenuItem.Click += new System.EventHandler(this.이동ToolStripMenuItem_Click);
            // 
            // 좌우반전ToolStripMenuItem
            // 
            this.좌우반전ToolStripMenuItem.Name = "좌우반전ToolStripMenuItem";
            this.좌우반전ToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.좌우반전ToolStripMenuItem.Text = "좌우 반전";
            this.좌우반전ToolStripMenuItem.Click += new System.EventHandler(this.좌우반전ToolStripMenuItem_Click);
            // 
            // 상하반전ToolStripMenuItem
            // 
            this.상하반전ToolStripMenuItem.Name = "상하반전ToolStripMenuItem";
            this.상하반전ToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.상하반전ToolStripMenuItem.Text = "상하 반전";
            this.상하반전ToolStripMenuItem.Click += new System.EventHandler(this.상하반전ToolStripMenuItem_Click);
            // 
            // 축소ToolStripMenuItem
            // 
            this.축소ToolStripMenuItem.Name = "축소ToolStripMenuItem";
            this.축소ToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.축소ToolStripMenuItem.Text = "축소";
            this.축소ToolStripMenuItem.Click += new System.EventHandler(this.축소ToolStripMenuItem_Click);
            // 
            // 확대ToolStripMenuItem
            // 
            this.확대ToolStripMenuItem.Name = "확대ToolStripMenuItem";
            this.확대ToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.확대ToolStripMenuItem.Text = "확대";
            this.확대ToolStripMenuItem.Click += new System.EventHandler(this.확대ToolStripMenuItem_Click);
            // 
            // 회전역방향ToolStripMenuItem
            // 
            this.회전역방향ToolStripMenuItem.Name = "회전역방향ToolStripMenuItem";
            this.회전역방향ToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.회전역방향ToolStripMenuItem.Text = "회전(역방향)";
            this.회전역방향ToolStripMenuItem.Click += new System.EventHandler(this.회전역방향ToolStripMenuItem_Click);
            // 
            // 화소영역처리ToolStripMenuItem
            // 
            this.화소영역처리ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.엠보싱ToolStripMenuItem,
            this.블러링ToolStripMenuItem,
            this.샤프닝ToolStripMenuItem,
            this.고주파ToolStripMenuItem,
            this.수평수직ToolStripMenuItem,
            this.loGToolStripMenuItem,
            this.doGToolStripMenuItem});
            this.화소영역처리ToolStripMenuItem.Name = "화소영역처리ToolStripMenuItem";
            this.화소영역처리ToolStripMenuItem.Size = new System.Drawing.Size(118, 28);
            this.화소영역처리ToolStripMenuItem.Text = "화소영역 처리";
            // 
            // 엠보싱ToolStripMenuItem
            // 
            this.엠보싱ToolStripMenuItem.Name = "엠보싱ToolStripMenuItem";
            this.엠보싱ToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.엠보싱ToolStripMenuItem.Text = "엠보싱 Alt + E";
            this.엠보싱ToolStripMenuItem.Click += new System.EventHandler(this.엠보싱ToolStripMenuItem_Click);
            // 
            // 블러링ToolStripMenuItem
            // 
            this.블러링ToolStripMenuItem.Name = "블러링ToolStripMenuItem";
            this.블러링ToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.블러링ToolStripMenuItem.Text = "블러링";
            this.블러링ToolStripMenuItem.Click += new System.EventHandler(this.블러링ToolStripMenuItem_Click);
            // 
            // 샤프닝ToolStripMenuItem
            // 
            this.샤프닝ToolStripMenuItem.Name = "샤프닝ToolStripMenuItem";
            this.샤프닝ToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.샤프닝ToolStripMenuItem.Text = "샤프닝";
            this.샤프닝ToolStripMenuItem.Click += new System.EventHandler(this.샤프닝ToolStripMenuItem_Click);
            // 
            // 고주파ToolStripMenuItem
            // 
            this.고주파ToolStripMenuItem.Name = "고주파ToolStripMenuItem";
            this.고주파ToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.고주파ToolStripMenuItem.Text = "고주파 필터 샤프닝";
            this.고주파ToolStripMenuItem.Click += new System.EventHandler(this.고주파ToolStripMenuItem_Click);
            // 
            // 수평수직ToolStripMenuItem
            // 
            this.수평수직ToolStripMenuItem.Name = "수평수직ToolStripMenuItem";
            this.수평수직ToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.수평수직ToolStripMenuItem.Text = "수평수직 엣지 검출";
            this.수평수직ToolStripMenuItem.Click += new System.EventHandler(this.수평수직ToolStripMenuItem_Click);
            // 
            // loGToolStripMenuItem
            // 
            this.loGToolStripMenuItem.Name = "loGToolStripMenuItem";
            this.loGToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.loGToolStripMenuItem.Text = "LoG";
            this.loGToolStripMenuItem.Click += new System.EventHandler(this.loGToolStripMenuItem_Click);
            // 
            // doGToolStripMenuItem
            // 
            this.doGToolStripMenuItem.Name = "doGToolStripMenuItem";
            this.doGToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.doGToolStripMenuItem.Text = "DoG";
            this.doGToolStripMenuItem.Click += new System.EventHandler(this.doGToolStripMenuItem_Click);
            // 
            // 히스토그램ToolStripMenuItem
            // 
            this.히스토그램ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.스트레칭ToolStripMenuItem,
            this.엔드인ToolStripMenuItem,
            this.평활화ToolStripMenuItem});
            this.히스토그램ToolStripMenuItem.Name = "히스토그램ToolStripMenuItem";
            this.히스토그램ToolStripMenuItem.Size = new System.Drawing.Size(98, 28);
            this.히스토그램ToolStripMenuItem.Text = "히스토그램";
            // 
            // 스트레칭ToolStripMenuItem
            // 
            this.스트레칭ToolStripMenuItem.Name = "스트레칭ToolStripMenuItem";
            this.스트레칭ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.스트레칭ToolStripMenuItem.Text = "스트레칭";
            this.스트레칭ToolStripMenuItem.Click += new System.EventHandler(this.스트레칭ToolStripMenuItem_Click);
            // 
            // 엔드인ToolStripMenuItem
            // 
            this.엔드인ToolStripMenuItem.Name = "엔드인ToolStripMenuItem";
            this.엔드인ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.엔드인ToolStripMenuItem.Text = "엔드인";
            this.엔드인ToolStripMenuItem.Click += new System.EventHandler(this.엔드인ToolStripMenuItem_Click);
            // 
            // 평활화ToolStripMenuItem
            // 
            this.평활화ToolStripMenuItem.Name = "평활화ToolStripMenuItem";
            this.평활화ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.평활화ToolStripMenuItem.Text = "평활화";
            this.평활화ToolStripMenuItem.Click += new System.EventHandler(this.평활화ToolStripMenuItem_Click);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "None",
            "Box",
            "Free"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 28);
            this.toolStripComboBox1.Text = "마우스 여부";
            this.toolStripComboBox1.Click += new System.EventHandler(this.toolStripComboBox1_Click);
            this.toolStripComboBox1.TextChanged += new System.EventHandler(this.toolStripComboBox1_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pictureBox1.Location = new System.Drawing.Point(23, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(428, 305);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 488);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(850, 26);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(152, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(152, 20);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(152, 20);
            this.toolStripStatusLabel3.Text = "toolStripStatusLabel3";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(152, 20);
            this.toolStripStatusLabel4.Text = "toolStripStatusLabel4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 514);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 파일ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 열기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 저장ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 화소점처리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 동일이미지ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 그레이스케일ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 밝게어둡게ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 기하학처리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 축소ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 확대ToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripMenuItem 화소영역처리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 엠보싱ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 반전이미지ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 흑백127기준ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 파라볼라ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 감마ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 이동ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 좌우반전ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 상하반전ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 블러링ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 샤프닝ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 고주파ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 수평수직ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doGToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripMenuItem 색상추출ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 채도변경ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 회전역방향ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 히스토그램ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 스트레칭ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 엔드인ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 평활화ToolStripMenuItem;
    }
}

