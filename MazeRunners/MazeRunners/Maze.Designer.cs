
namespace MazeRunners
{
    partial class Maze
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>

        private System.Windows.Forms.PictureBox pictureBoxBlueTeam;
        private System.Windows.Forms.PictureBox pictureBoxRedTeam;
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maze));
            pictureBoxBlueTeam = new PictureBox();
            pictureBoxRedTeam = new PictureBox();
            Map = new PictureBox();
            upButton = new Button();
            downButton = new Button();
            leftButton = new Button();
            rightButton = new Button();
            pictureBox = new PictureBox();
            _ = new Label();
            Description = new Label();
            Cooldown = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            turns = new Label();
            Turn = new Label();
            label5 = new Label();
            Chip = new Label();
            Red = new Label();
            Blue = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBlueTeam).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRedTeam).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Map).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxBlueTeam
            // 
            pictureBoxBlueTeam.Location = new Point(12, 12);
            pictureBoxBlueTeam.Name = "pictureBoxBlueTeam";
            pictureBoxBlueTeam.Size = new Size(400, 400);
            pictureBoxBlueTeam.TabIndex = 0;
            pictureBoxBlueTeam.TabStop = false;
            // 
            // pictureBoxRedTeam
            // 
            pictureBoxRedTeam.Location = new Point(430, 12);
            pictureBoxRedTeam.Name = "pictureBoxRedTeam";
            pictureBoxRedTeam.Size = new Size(400, 400);
            pictureBoxRedTeam.TabIndex = 1;
            pictureBoxRedTeam.TabStop = false;
            // 
            // Map
            // 
            Map.BackColor = Color.FromArgb(224, 224, 224);
            Map.BackgroundImage = (Image)resources.GetObject("Map.BackgroundImage");
            Map.BorderStyle = BorderStyle.Fixed3D;
            Map.Location = new Point(12, 51);
            Map.Name = "Map";
            Map.Size = new Size(801, 801);
            Map.TabIndex = 0;
            Map.TabStop = false;
            Map.Paint += DrawMap;
            Map.MouseClick += Map_MouseClick;
            // 
            // upButton
            // 
            upButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            upButton.ForeColor = SystemColors.WindowFrame;
            upButton.Location = new Point(1049, 725);
            upButton.Name = "upButton";
            upButton.Size = new Size(94, 29);
            upButton.TabIndex = 2;
            upButton.Text = "Up";
            upButton.UseVisualStyleBackColor = true;
            upButton.MouseClick += upButton_MouseClick;
            // 
            // downButton
            // 
            downButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            downButton.ForeColor = SystemColors.WindowFrame;
            downButton.Location = new Point(1049, 760);
            downButton.Name = "downButton";
            downButton.Size = new Size(94, 29);
            downButton.TabIndex = 3;
            downButton.Text = "Down";
            downButton.UseVisualStyleBackColor = true;
            downButton.MouseClick += downButton_MouseClick;
            // 
            // leftButton
            // 
            leftButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            leftButton.ForeColor = SystemColors.WindowFrame;
            leftButton.Location = new Point(949, 745);
            leftButton.Name = "leftButton";
            leftButton.Size = new Size(94, 29);
            leftButton.TabIndex = 4;
            leftButton.Text = "Left";
            leftButton.UseVisualStyleBackColor = true;
            leftButton.MouseClick += leftButton_MouseClick;
            // 
            // rightButton
            // 
            rightButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            rightButton.ForeColor = SystemColors.WindowFrame;
            rightButton.Location = new Point(1149, 745);
            rightButton.Name = "rightButton";
            rightButton.Size = new Size(94, 29);
            rightButton.TabIndex = 5;
            rightButton.Text = "Right";
            rightButton.UseVisualStyleBackColor = true;
            rightButton.MouseClick += rightButton_MouseClick;
            // 
            // pictureBox
            // 
            pictureBox.BackColor = SystemColors.Menu;
            pictureBox.BorderStyle = BorderStyle.Fixed3D;
            pictureBox.Location = new Point(872, 51);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(426, 221);
            pictureBox.TabIndex = 6;
            pictureBox.TabStop = false;
            // 
            // _
            // 
            _.AutoSize = true;
            _.BackColor = SystemColors.Menu;
            _.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            _.ForeColor = SystemColors.WindowFrame;
            _.Location = new Point(950, 117);
            _.Name = "_";
            _.Size = new Size(15, 20);
            _.TabIndex = 7;
            _.Text = "-";
            // 
            // Description
            // 
            Description.AutoSize = true;
            Description.BackColor = SystemColors.Menu;
            Description.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Description.ForeColor = SystemColors.WindowFrame;
            Description.Location = new Point(1002, 151);
            Description.Name = "Description";
            Description.Size = new Size(15, 20);
            Description.TabIndex = 8;
            Description.Text = "-";
            // 
            // Cooldown
            // 
            Cooldown.AutoSize = true;
            Cooldown.BackColor = SystemColors.Menu;
            Cooldown.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Cooldown.ForeColor = SystemColors.WindowFrame;
            Cooldown.Location = new Point(993, 188);
            Cooldown.Name = "Cooldown";
            Cooldown.Size = new Size(15, 20);
            Cooldown.TabIndex = 9;
            Cooldown.Text = "-";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Menu;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.WindowFrame;
            label1.Location = new Point(899, 117);
            label1.Name = "label1";
            label1.Size = new Size(45, 20);
            label1.TabIndex = 11;
            label1.Text = "Skill :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Menu;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.WindowFrame;
            label2.Location = new Point(899, 151);
            label2.Name = "label2";
            label2.Size = new Size(97, 20);
            label2.TabIndex = 12;
            label2.Text = "Description :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.Menu;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.WindowFrame;
            label3.Location = new Point(900, 188);
            label3.Name = "label3";
            label3.Size = new Size(87, 20);
            label3.TabIndex = 13;
            label3.Text = "Cooldown :";
            // 
            // turns
            // 
            turns.AutoSize = true;
            turns.BackColor = SystemColors.Menu;
            turns.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            turns.ForeColor = SystemColors.WindowFrame;
            turns.Location = new Point(1158, 227);
            turns.Name = "turns";
            turns.Size = new Size(15, 20);
            turns.TabIndex = 15;
            turns.Text = "-";
            // 
            // Turn
            // 
            Turn.AutoSize = true;
            Turn.BackColor = SystemColors.Menu;
            Turn.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Turn.ForeColor = SystemColors.WindowFrame;
            Turn.Location = new Point(956, 227);
            Turn.Name = "Turn";
            Turn.Size = new Size(15, 20);
            Turn.TabIndex = 16;
            Turn.Text = "-";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.Menu;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.WindowFrame;
            label5.Location = new Point(900, 227);
            label5.Name = "label5";
            label5.Size = new Size(49, 20);
            label5.TabIndex = 17;
            label5.Text = "Turn :";
            // 
            // Chip
            // 
            Chip.AutoSize = true;
            Chip.BackColor = SystemColors.Menu;
            Chip.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            Chip.ForeColor = SystemColors.WindowFrame;
            Chip.Location = new Point(899, 71);
            Chip.Name = "Chip";
            Chip.Size = new Size(60, 31);
            Chip.TabIndex = 18;
            Chip.Text = "chip";
            // 
            // Red
            // 
            Red.AutoSize = true;
            Red.BackColor = Color.Transparent;
            Red.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            Red.ForeColor = SystemColors.ScrollBar;
            Red.Location = new Point(940, 297);
            Red.Name = "Red";
            Red.Size = new Size(17, 23);
            Red.TabIndex = 19;
            Red.Text = "-";
            // 
            // Blue
            // 
            Blue.AutoSize = true;
            Blue.BackColor = Color.Transparent;
            Blue.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            Blue.ForeColor = SystemColors.ScrollBar;
            Blue.Location = new Point(940, 336);
            Blue.Name = "Blue";
            Blue.Size = new Size(17, 23);
            Blue.TabIndex = 20;
            Blue.Text = "-";
            // 
            // Maze
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 0, 64);
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1439, 953);
            Controls.Add(Blue);
            Controls.Add(Red);
            Controls.Add(Chip);
            Controls.Add(label5);
            Controls.Add(Turn);
            Controls.Add(turns);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Cooldown);
            Controls.Add(Description);
            Controls.Add(_);
            Controls.Add(pictureBox);
            Controls.Add(rightButton);
            Controls.Add(leftButton);
            Controls.Add(downButton);
            Controls.Add(upButton);
            Controls.Add(Map);
            Name = "Maze";
            Text = "Maze";
            ((System.ComponentModel.ISupportInitialize)pictureBoxBlueTeam).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRedTeam).EndInit();
            ((System.ComponentModel.ISupportInitialize)Map).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void Maze_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private PictureBox Map;
        private Button upButton;
        private Button downButton;
        private Button leftButton;
        private Button rightButton;
        private PictureBox pictureBox;
        private Label _;
        private Label Description;
        private Label Cooldown;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label turns;
        private Label Turn;
        private Label label5;
        private Label Chip;
        private Label Red;
        private Label Blue;
    }
}