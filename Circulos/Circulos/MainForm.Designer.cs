/*
 * Created by SharpDevelop.
 * User: edblo
 * Date: 06/02/2022
 * Time: 12:14 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Circulos
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.PictureBox pictureBoxImage;
		private System.Windows.Forms.Button buttonSelectionImage;
		private System.Windows.Forms.Button buttonCreateGraph;
		private System.Windows.Forms.Label labelListBoxTitle;
		private System.Windows.Forms.TreeView treeViewGraph;
		private System.Windows.Forms.Button buttonAnimation;
		private System.Windows.Forms.Label labelInfo;
		private System.Windows.Forms.NumericUpDown numericUpDownVel;
		private System.Windows.Forms.Label labelVel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonPredator;
		private System.Windows.Forms.Button buttonAgent;
		private System.Windows.Forms.Button buttonDestination;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownSkills;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numericUpDownPredatorRadar;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.pictureBoxImage = new System.Windows.Forms.PictureBox();
			this.buttonSelectionImage = new System.Windows.Forms.Button();
			this.buttonCreateGraph = new System.Windows.Forms.Button();
			this.labelListBoxTitle = new System.Windows.Forms.Label();
			this.treeViewGraph = new System.Windows.Forms.TreeView();
			this.buttonAnimation = new System.Windows.Forms.Button();
			this.labelInfo = new System.Windows.Forms.Label();
			this.numericUpDownVel = new System.Windows.Forms.NumericUpDown();
			this.labelVel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonPredator = new System.Windows.Forms.Button();
			this.buttonAgent = new System.Windows.Forms.Button();
			this.buttonDestination = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDownSkills = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.numericUpDownPredatorRadar = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownVel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSkills)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPredatorRadar)).BeginInit();
			this.SuspendLayout();
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// pictureBoxImage
			// 
			this.pictureBoxImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.pictureBoxImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBoxImage.Location = new System.Drawing.Point(12, 12);
			this.pictureBoxImage.Name = "pictureBoxImage";
			this.pictureBoxImage.Size = new System.Drawing.Size(957, 720);
			this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBoxImage.TabIndex = 0;
			this.pictureBoxImage.TabStop = false;
			this.pictureBoxImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBoxImageMouseClick);
			this.pictureBoxImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBoxImageMouseMove);
			// 
			// buttonSelectionImage
			// 
			this.buttonSelectionImage.Location = new System.Drawing.Point(975, 12);
			this.buttonSelectionImage.Name = "buttonSelectionImage";
			this.buttonSelectionImage.Size = new System.Drawing.Size(360, 23);
			this.buttonSelectionImage.TabIndex = 1;
			this.buttonSelectionImage.Text = "Seleccionar imagen";
			this.buttonSelectionImage.UseVisualStyleBackColor = true;
			this.buttonSelectionImage.Click += new System.EventHandler(this.ButtonSelectionImageClick);
			// 
			// buttonCreateGraph
			// 
			this.buttonCreateGraph.Location = new System.Drawing.Point(975, 52);
			this.buttonCreateGraph.Name = "buttonCreateGraph";
			this.buttonCreateGraph.Size = new System.Drawing.Size(360, 27);
			this.buttonCreateGraph.TabIndex = 3;
			this.buttonCreateGraph.Text = "Crear grafo";
			this.buttonCreateGraph.UseVisualStyleBackColor = true;
			this.buttonCreateGraph.Click += new System.EventHandler(this.ButtonCreateGraphClick);
			// 
			// labelListBoxTitle
			// 
			this.labelListBoxTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
			this.labelListBoxTitle.Location = new System.Drawing.Point(975, 102);
			this.labelListBoxTitle.Name = "labelListBoxTitle";
			this.labelListBoxTitle.Size = new System.Drawing.Size(128, 24);
			this.labelListBoxTitle.TabIndex = 6;
			this.labelListBoxTitle.Text = "Vértices";
			// 
			// treeViewGraph
			// 
			this.treeViewGraph.Location = new System.Drawing.Point(982, 129);
			this.treeViewGraph.Name = "treeViewGraph";
			this.treeViewGraph.Size = new System.Drawing.Size(353, 210);
			this.treeViewGraph.TabIndex = 9;
			// 
			// buttonAnimation
			// 
			this.buttonAnimation.Location = new System.Drawing.Point(975, 692);
			this.buttonAnimation.Name = "buttonAnimation";
			this.buttonAnimation.Size = new System.Drawing.Size(360, 23);
			this.buttonAnimation.TabIndex = 10;
			this.buttonAnimation.Text = "Comenzar animación";
			this.buttonAnimation.UseVisualStyleBackColor = true;
			this.buttonAnimation.Click += new System.EventHandler(this.ButtonAnimationClick);
			// 
			// labelInfo
			// 
			this.labelInfo.Location = new System.Drawing.Point(982, 357);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new System.Drawing.Size(353, 27);
			this.labelInfo.TabIndex = 12;
			// 
			// numericUpDownVel
			// 
			this.numericUpDownVel.Increment = new decimal(new int[] {
			5,
			0,
			0,
			0});
			this.numericUpDownVel.Location = new System.Drawing.Point(1163, 540);
			this.numericUpDownVel.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownVel.Name = "numericUpDownVel";
			this.numericUpDownVel.Size = new System.Drawing.Size(53, 22);
			this.numericUpDownVel.TabIndex = 13;
			this.numericUpDownVel.Value = new decimal(new int[] {
			10,
			0,
			0,
			0});
			// 
			// labelVel
			// 
			this.labelVel.Location = new System.Drawing.Point(982, 540);
			this.labelVel.Name = "labelVel";
			this.labelVel.Size = new System.Drawing.Size(175, 25);
			this.labelVel.TabIndex = 14;
			this.labelVel.Text = "Velocidad de las presas";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(982, 410);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(218, 17);
			this.label1.TabIndex = 19;
			this.label1.Text = "Objeto que se crea al dar clik:";
			// 
			// buttonPredator
			// 
			this.buttonPredator.Location = new System.Drawing.Point(1111, 445);
			this.buttonPredator.Name = "buttonPredator";
			this.buttonPredator.Size = new System.Drawing.Size(105, 41);
			this.buttonPredator.TabIndex = 21;
			this.buttonPredator.Text = "Depredador";
			this.buttonPredator.UseVisualStyleBackColor = true;
			this.buttonPredator.Click += new System.EventHandler(this.ButtonPredatorClick);
			// 
			// buttonAgent
			// 
			this.buttonAgent.Location = new System.Drawing.Point(982, 445);
			this.buttonAgent.Name = "buttonAgent";
			this.buttonAgent.Size = new System.Drawing.Size(94, 41);
			this.buttonAgent.TabIndex = 23;
			this.buttonAgent.Text = "Presa";
			this.buttonAgent.UseVisualStyleBackColor = true;
			this.buttonAgent.Click += new System.EventHandler(this.ButtonAgentClick);
			// 
			// buttonDestination
			// 
			this.buttonDestination.Location = new System.Drawing.Point(1241, 445);
			this.buttonDestination.Name = "buttonDestination";
			this.buttonDestination.Size = new System.Drawing.Size(94, 41);
			this.buttonDestination.TabIndex = 24;
			this.buttonDestination.Text = "Objetivo";
			this.buttonDestination.UseVisualStyleBackColor = true;
			this.buttonDestination.Click += new System.EventHandler(this.ButtonDestinationClick);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(982, 565);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(175, 25);
			this.label2.TabIndex = 25;
			this.label2.Text = "Cargas de sigilo";
			// 
			// numericUpDownSkills
			// 
			this.numericUpDownSkills.Location = new System.Drawing.Point(1163, 568);
			this.numericUpDownSkills.Maximum = new decimal(new int[] {
			10,
			0,
			0,
			0});
			this.numericUpDownSkills.Name = "numericUpDownSkills";
			this.numericUpDownSkills.Size = new System.Drawing.Size(53, 22);
			this.numericUpDownSkills.TabIndex = 26;
			this.numericUpDownSkills.Value = new decimal(new int[] {
			3,
			0,
			0,
			0});
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(982, 614);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(175, 48);
			this.label3.TabIndex = 27;
			this.label3.Text = "Radio de caza de los depredadores";
			// 
			// numericUpDownPredatorRadar
			// 
			this.numericUpDownPredatorRadar.Increment = new decimal(new int[] {
			10,
			0,
			0,
			0});
			this.numericUpDownPredatorRadar.Location = new System.Drawing.Point(1163, 623);
			this.numericUpDownPredatorRadar.Maximum = new decimal(new int[] {
			300,
			0,
			0,
			0});
			this.numericUpDownPredatorRadar.Minimum = new decimal(new int[] {
			40,
			0,
			0,
			0});
			this.numericUpDownPredatorRadar.Name = "numericUpDownPredatorRadar";
			this.numericUpDownPredatorRadar.Size = new System.Drawing.Size(53, 22);
			this.numericUpDownPredatorRadar.TabIndex = 28;
			this.numericUpDownPredatorRadar.Value = new decimal(new int[] {
			100,
			0,
			0,
			0});
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1347, 744);
			this.Controls.Add(this.numericUpDownPredatorRadar);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.numericUpDownSkills);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonDestination);
			this.Controls.Add(this.buttonAgent);
			this.Controls.Add(this.buttonPredator);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.labelVel);
			this.Controls.Add(this.numericUpDownVel);
			this.Controls.Add(this.labelInfo);
			this.Controls.Add(this.buttonAnimation);
			this.Controls.Add(this.treeViewGraph);
			this.Controls.Add(this.labelListBoxTitle);
			this.Controls.Add(this.buttonCreateGraph);
			this.Controls.Add(this.buttonSelectionImage);
			this.Controls.Add(this.pictureBoxImage);
			this.Name = "MainForm";
			this.Text = "Circulos";
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownVel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSkills)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPredatorRadar)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
