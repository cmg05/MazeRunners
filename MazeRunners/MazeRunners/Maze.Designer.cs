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
        private void InitializeComponent()
        {
            Map = new PictureBox();
            upButton = new Button();
            downButton = new Button();
            leftButton = new Button();
            rightButton = new Button();
            pauseButton = new Button();
            ((System.ComponentModel.ISupportInitialize)Map).BeginInit();
            SuspendLayout();
            // 
            // Map
            // 
            Map.BackColor = Color.Transparent;
            Map.BorderStyle = BorderStyle.FixedSingle;
            Map.Location = new Point(12, 60);
            Map.Name = "Map";
            Map.Size = new Size(801, 801);
            Map.TabIndex = 0;
            Map.TabStop = false;
            Map.Paint += DrawMap;
            Map.MouseClick += Map_MouseClick;
            // 
            // upButton
            // 
            upButton.Location = new Point(1023, 751);
            upButton.Name = "upButton";
            upButton.Size = new Size(94, 29);
            upButton.TabIndex = 2;
            upButton.Text = "Up";
            upButton.UseVisualStyleBackColor = true;
            upButton.MouseClick += upButton_MouseClick;
            // 
            // downButton
            // 
            downButton.Location = new Point(1023, 786);
            downButton.Name = "downButton";
            downButton.Size = new Size(94, 29);
            downButton.TabIndex = 3;
            downButton.Text = "Down";
            downButton.UseVisualStyleBackColor = true;
            downButton.MouseClick += downButton_MouseClick;
            // 
            // leftButton
            // 
            leftButton.Location = new Point(923, 771);
            leftButton.Name = "leftButton";
            leftButton.Size = new Size(94, 29);
            leftButton.TabIndex = 4;
            leftButton.Text = "Left";
            leftButton.UseVisualStyleBackColor = true;
            leftButton.MouseClick += leftButton_MouseClick;
            // 
            // rightButton
            // 
            rightButton.Location = new Point(1123, 771);
            rightButton.Name = "rightButton";
            rightButton.Size = new Size(94, 29);
            rightButton.TabIndex = 5;
            rightButton.Text = "Right";
            rightButton.UseVisualStyleBackColor = true;
            rightButton.MouseClick += rightButton_MouseClick;
            // 
            // pauseButton
            // 
            pauseButton.Location = new Point(865, 21);
            pauseButton.Name = "pauseButton";
            pauseButton.Size = new Size(94, 29);
            pauseButton.TabIndex = 1;
            pauseButton.Text = "Pause";
            pauseButton.UseVisualStyleBackColor = true;
            // 
            // Maze
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1760, 953);
            Controls.Add(rightButton);
            Controls.Add(leftButton);
            Controls.Add(downButton);
            Controls.Add(upButton);
            Controls.Add(pauseButton);
            Controls.Add(Map);
            Name = "Maze";
            Text = "Maze";
            ((System.ComponentModel.ISupportInitialize)Map).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox Map;
        private Button upButton;
        private Button downButton;
        private Button leftButton;
        private Button rightButton;
        private Button pauseButton;
    }
}