
namespace Buses
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.workPlace = new System.Windows.Forms.Panel();
            this.passengers = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.allPeople = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.infoSpeed = new System.Windows.Forms.Label();
            this.infoNumOfSeats = new System.Windows.Forms.Label();
            this.infoNumBuses = new System.Windows.Forms.Label();
            this.infoDisp = new System.Windows.Forms.Label();
            this.infoCoeffB = new System.Windows.Forms.Label();
            this.infoAboutSeats = new System.Windows.Forms.Label();
            this.infoAboutBuses = new System.Windows.Forms.Label();
            this.tops = new System.Windows.Forms.ListBox();
            this.infoAboutBus = new System.Windows.Forms.Label();
            this.infoAboutTop = new System.Windows.Forms.Label();
            this.pauseModeling = new System.Windows.Forms.Button();
            this.progressOfModeling = new System.Windows.Forms.ProgressBar();
            this.modelStart = new System.Windows.Forms.Button();
            this.speed = new System.Windows.Forms.TextBox();
            this.setCoeffAndDisp = new System.Windows.Forms.Button();
            this.numberOfSeats = new System.Windows.Forms.TextBox();
            this.numberOfBuses = new System.Windows.Forms.TextBox();
            this.dispersion = new System.Windows.Forms.TextBox();
            this.coeffB = new System.Windows.Forms.TextBox();
            this.busList = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.resetRoute = new System.Windows.Forms.Button();
            this.forPaint = new System.Windows.Forms.PictureBox();
            this.timerForGreeting = new System.Windows.Forms.Timer(this.components);
            this.timerForModeling = new System.Windows.Forms.Timer(this.components);
            this.busTick = new System.Windows.Forms.Timer(this.components);
            this.workPlace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.passengers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.allPeople)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.forPaint)).BeginInit();
            this.SuspendLayout();
            // 
            // workPlace
            // 
            this.workPlace.BackColor = System.Drawing.SystemColors.ControlLight;
            this.workPlace.Controls.Add(this.passengers);
            this.workPlace.Controls.Add(this.allPeople);
            this.workPlace.Controls.Add(this.infoSpeed);
            this.workPlace.Controls.Add(this.infoNumOfSeats);
            this.workPlace.Controls.Add(this.infoNumBuses);
            this.workPlace.Controls.Add(this.infoDisp);
            this.workPlace.Controls.Add(this.infoCoeffB);
            this.workPlace.Controls.Add(this.infoAboutSeats);
            this.workPlace.Controls.Add(this.infoAboutBuses);
            this.workPlace.Controls.Add(this.tops);
            this.workPlace.Controls.Add(this.infoAboutBus);
            this.workPlace.Controls.Add(this.infoAboutTop);
            this.workPlace.Controls.Add(this.pauseModeling);
            this.workPlace.Controls.Add(this.progressOfModeling);
            this.workPlace.Controls.Add(this.modelStart);
            this.workPlace.Controls.Add(this.speed);
            this.workPlace.Controls.Add(this.setCoeffAndDisp);
            this.workPlace.Controls.Add(this.numberOfSeats);
            this.workPlace.Controls.Add(this.numberOfBuses);
            this.workPlace.Controls.Add(this.dispersion);
            this.workPlace.Controls.Add(this.coeffB);
            this.workPlace.Controls.Add(this.busList);
            this.workPlace.Controls.Add(this.listBox1);
            this.workPlace.Controls.Add(this.resetRoute);
            this.workPlace.Controls.Add(this.forPaint);
            this.workPlace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workPlace.Location = new System.Drawing.Point(0, 0);
            this.workPlace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.workPlace.MinimumSize = new System.Drawing.Size(813, 375);
            this.workPlace.Name = "workPlace";
            this.workPlace.Size = new System.Drawing.Size(1068, 597);
            this.workPlace.TabIndex = 1;
            // 
            // passengers
            // 
            legend1.Name = "Legend1";
            this.passengers.Legends.Add(legend1);
            this.passengers.Location = new System.Drawing.Point(393, 288);
            this.passengers.Name = "passengers";
            this.passengers.Size = new System.Drawing.Size(349, 231);
            this.passengers.TabIndex = 12;
            this.passengers.Text = "chart1";
            this.passengers.Visible = false;
            // 
            // allPeople
            // 
            legend2.Name = "Legend1";
            this.allPeople.Legends.Add(legend2);
            this.allPeople.Location = new System.Drawing.Point(18, 288);
            this.allPeople.Name = "allPeople";
            this.allPeople.Size = new System.Drawing.Size(337, 231);
            this.allPeople.TabIndex = 12;
            this.allPeople.Text = "chart1";
            this.allPeople.Visible = false;
            // 
            // infoSpeed
            // 
            this.infoSpeed.AutoSize = true;
            this.infoSpeed.Location = new System.Drawing.Point(903, 99);
            this.infoSpeed.Name = "infoSpeed";
            this.infoSpeed.Size = new System.Drawing.Size(133, 17);
            this.infoSpeed.TabIndex = 11;
            this.infoSpeed.Text = "Скорость автобуса";
            this.infoSpeed.Visible = false;
            // 
            // infoNumOfSeats
            // 
            this.infoNumOfSeats.AutoSize = true;
            this.infoNumOfSeats.Location = new System.Drawing.Point(705, 141);
            this.infoNumOfSeats.Name = "infoNumOfSeats";
            this.infoNumOfSeats.Size = new System.Drawing.Size(187, 17);
            this.infoNumOfSeats.TabIndex = 11;
            this.infoNumOfSeats.Text = "Кол-во сидений в автобусе";
            this.infoNumOfSeats.Visible = false;
            // 
            // infoNumBuses
            // 
            this.infoNumBuses.AutoSize = true;
            this.infoNumBuses.Location = new System.Drawing.Point(705, 109);
            this.infoNumBuses.Name = "infoNumBuses";
            this.infoNumBuses.Size = new System.Drawing.Size(124, 17);
            this.infoNumBuses.TabIndex = 11;
            this.infoNumBuses.Text = "Кол-во автобусов";
            this.infoNumBuses.Visible = false;
            // 
            // infoDisp
            // 
            this.infoDisp.AutoSize = true;
            this.infoDisp.Location = new System.Drawing.Point(705, 77);
            this.infoDisp.Name = "infoDisp";
            this.infoDisp.Size = new System.Drawing.Size(148, 17);
            this.infoDisp.TabIndex = 11;
            this.infoDisp.Text = "Дисперсия значений";
            this.infoDisp.Visible = false;
            // 
            // infoCoeffB
            // 
            this.infoCoeffB.AutoSize = true;
            this.infoCoeffB.Location = new System.Drawing.Point(695, 34);
            this.infoCoeffB.Name = "infoCoeffB";
            this.infoCoeffB.Size = new System.Drawing.Size(185, 17);
            this.infoCoeffB.TabIndex = 11;
            this.infoCoeffB.Text = "Изначальное число людей";
            this.infoCoeffB.Visible = false;
            // 
            // infoAboutSeats
            // 
            this.infoAboutSeats.AutoSize = true;
            this.infoAboutSeats.Location = new System.Drawing.Point(264, 216);
            this.infoAboutSeats.Name = "infoAboutSeats";
            this.infoAboutSeats.Size = new System.Drawing.Size(46, 17);
            this.infoAboutSeats.TabIndex = 10;
            this.infoAboutSeats.Text = "label1";
            this.infoAboutSeats.Visible = false;
            // 
            // infoAboutBuses
            // 
            this.infoAboutBuses.AutoSize = true;
            this.infoAboutBuses.Location = new System.Drawing.Point(264, 175);
            this.infoAboutBuses.Name = "infoAboutBuses";
            this.infoAboutBuses.Size = new System.Drawing.Size(46, 17);
            this.infoAboutBuses.TabIndex = 10;
            this.infoAboutBuses.Text = "label1";
            this.infoAboutBuses.Visible = false;
            // 
            // tops
            // 
            this.tops.FormattingEnabled = true;
            this.tops.ItemHeight = 16;
            this.tops.Location = new System.Drawing.Point(393, 172);
            this.tops.Name = "tops";
            this.tops.Size = new System.Drawing.Size(120, 84);
            this.tops.TabIndex = 9;
            this.tops.Visible = false;
            this.tops.SelectedIndexChanged += new System.EventHandler(this.tops_SelectedIndexChanged);
            // 
            // infoAboutBus
            // 
            this.infoAboutBus.AutoSize = true;
            this.infoAboutBus.Location = new System.Drawing.Point(264, 134);
            this.infoAboutBus.Name = "infoAboutBus";
            this.infoAboutBus.Size = new System.Drawing.Size(46, 17);
            this.infoAboutBus.TabIndex = 8;
            this.infoAboutBus.Text = "label1";
            this.infoAboutBus.Visible = false;
            // 
            // infoAboutTop
            // 
            this.infoAboutTop.AutoSize = true;
            this.infoAboutTop.Location = new System.Drawing.Point(115, 175);
            this.infoAboutTop.Name = "infoAboutTop";
            this.infoAboutTop.Size = new System.Drawing.Size(46, 17);
            this.infoAboutTop.TabIndex = 8;
            this.infoAboutTop.Text = "label1";
            this.infoAboutTop.Visible = false;
            // 
            // pauseModeling
            // 
            this.pauseModeling.Location = new System.Drawing.Point(12, 525);
            this.pauseModeling.Name = "pauseModeling";
            this.pauseModeling.Size = new System.Drawing.Size(130, 60);
            this.pauseModeling.TabIndex = 7;
            this.pauseModeling.Text = "Приостановить моделирование";
            this.pauseModeling.UseVisualStyleBackColor = true;
            this.pauseModeling.Visible = false;
            this.pauseModeling.Click += new System.EventHandler(this.pauseModeling_Click);
            // 
            // progressOfModeling
            // 
            this.progressOfModeling.Location = new System.Drawing.Point(148, 34);
            this.progressOfModeling.Name = "progressOfModeling";
            this.progressOfModeling.Size = new System.Drawing.Size(365, 23);
            this.progressOfModeling.TabIndex = 6;
            this.progressOfModeling.Visible = false;
            // 
            // modelStart
            // 
            this.modelStart.Location = new System.Drawing.Point(906, 525);
            this.modelStart.Name = "modelStart";
            this.modelStart.Size = new System.Drawing.Size(130, 60);
            this.modelStart.TabIndex = 5;
            this.modelStart.Text = "Начать моделирование";
            this.modelStart.UseVisualStyleBackColor = true;
            this.modelStart.Visible = false;
            this.modelStart.Click += new System.EventHandler(this.modelStart_Click);
            // 
            // speed
            // 
            this.speed.Location = new System.Drawing.Point(906, 127);
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(100, 22);
            this.speed.TabIndex = 3;
            this.speed.Visible = false;
            // 
            // setCoeffAndDisp
            // 
            this.setCoeffAndDisp.Location = new System.Drawing.Point(906, 34);
            this.setCoeffAndDisp.Name = "setCoeffAndDisp";
            this.setCoeffAndDisp.Size = new System.Drawing.Size(110, 50);
            this.setCoeffAndDisp.TabIndex = 4;
            this.setCoeffAndDisp.Text = "Записать значения";
            this.setCoeffAndDisp.UseVisualStyleBackColor = true;
            this.setCoeffAndDisp.Visible = false;
            this.setCoeffAndDisp.Click += new System.EventHandler(this.setCoeffAndDisp_Click);
            // 
            // numberOfSeats
            // 
            this.numberOfSeats.Location = new System.Drawing.Point(780, 127);
            this.numberOfSeats.Name = "numberOfSeats";
            this.numberOfSeats.Size = new System.Drawing.Size(100, 22);
            this.numberOfSeats.TabIndex = 3;
            this.numberOfSeats.Visible = false;
            // 
            // numberOfBuses
            // 
            this.numberOfBuses.Location = new System.Drawing.Point(780, 99);
            this.numberOfBuses.Name = "numberOfBuses";
            this.numberOfBuses.Size = new System.Drawing.Size(100, 22);
            this.numberOfBuses.TabIndex = 3;
            this.numberOfBuses.Visible = false;
            // 
            // dispersion
            // 
            this.dispersion.Location = new System.Drawing.Point(780, 71);
            this.dispersion.Name = "dispersion";
            this.dispersion.Size = new System.Drawing.Size(100, 22);
            this.dispersion.TabIndex = 3;
            this.dispersion.Visible = false;
            // 
            // coeffB
            // 
            this.coeffB.Location = new System.Drawing.Point(780, 43);
            this.coeffB.Name = "coeffB";
            this.coeffB.Size = new System.Drawing.Size(100, 22);
            this.coeffB.TabIndex = 3;
            this.coeffB.Visible = false;
            // 
            // busList
            // 
            this.busList.FormattingEnabled = true;
            this.busList.ItemHeight = 16;
            this.busList.Location = new System.Drawing.Point(167, 92);
            this.busList.Name = "busList";
            this.busList.Size = new System.Drawing.Size(82, 164);
            this.busList.TabIndex = 2;
            this.busList.Visible = false;
            this.busList.SelectedIndexChanged += new System.EventHandler(this.busList_SelectedIndexChanged);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(17, 92);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(82, 164);
            this.listBox1.TabIndex = 2;
            this.listBox1.Visible = false;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // resetRoute
            // 
            this.resetRoute.Location = new System.Drawing.Point(520, 92);
            this.resetRoute.Name = "resetRoute";
            this.resetRoute.Size = new System.Drawing.Size(110, 50);
            this.resetRoute.TabIndex = 2;
            this.resetRoute.Text = "Сбросить";
            this.resetRoute.UseVisualStyleBackColor = true;
            this.resetRoute.Visible = false;
            this.resetRoute.Click += new System.EventHandler(this.resetRoute_Click);
            // 
            // forPaint
            // 
            this.forPaint.Location = new System.Drawing.Point(795, 182);
            this.forPaint.Name = "forPaint";
            this.forPaint.Size = new System.Drawing.Size(261, 285);
            this.forPaint.TabIndex = 1;
            this.forPaint.TabStop = false;
            this.forPaint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.forPaint_MouseClick);
            // 
            // timerForGreeting
            // 
            this.timerForGreeting.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerForModeling
            // 
            this.timerForModeling.Tick += new System.EventHandler(this.timerForModeling_Tick);
            // 
            // busTick
            // 
            this.busTick.Tick += new System.EventHandler(this.busTick_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 597);
            this.Controls.Add(this.workPlace);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(829, 413);
            this.Name = "Form1";
            this.Text = "Form1";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.workPlace.ResumeLayout(false);
            this.workPlace.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.passengers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.allPeople)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.forPaint)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel workPlace;
        private System.Windows.Forms.Timer timerForGreeting;
        private System.Windows.Forms.PictureBox forPaint;
        private System.Windows.Forms.Button resetRoute;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox dispersion;
        private System.Windows.Forms.TextBox coeffB;
        private System.Windows.Forms.Button setCoeffAndDisp;
        private System.Windows.Forms.Button modelStart;
        private System.Windows.Forms.ProgressBar progressOfModeling;
        private System.Windows.Forms.Timer timerForModeling;
        private System.Windows.Forms.Button pauseModeling;
        private System.Windows.Forms.TextBox numberOfBuses;
        private System.Windows.Forms.ListBox tops;
        private System.Windows.Forms.Label infoAboutBuses;
        private System.Windows.Forms.Label infoNumOfSeats;
        private System.Windows.Forms.Label infoNumBuses;
        private System.Windows.Forms.Label infoDisp;
        private System.Windows.Forms.Label infoCoeffB;
        private System.Windows.Forms.TextBox numberOfSeats;
        private System.Windows.Forms.Label infoAboutSeats;
        private System.Windows.Forms.Timer busTick;
        private System.Windows.Forms.Label infoSpeed;
        private System.Windows.Forms.TextBox speed;
        private System.Windows.Forms.ListBox busList;
        private System.Windows.Forms.Label infoAboutBus;
        private System.Windows.Forms.Label infoAboutTop;
        private System.Windows.Forms.DataVisualization.Charting.Chart passengers;
        private System.Windows.Forms.DataVisualization.Charting.Chart allPeople;
    }
}

