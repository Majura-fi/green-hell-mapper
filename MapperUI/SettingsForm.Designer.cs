namespace MapperUI;

partial class SettingsForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        pathWidthInput = new NumericUpDown();
        label1 = new Label();
        btnSave = new Button();
        ((System.ComponentModel.ISupportInitialize)pathWidthInput).BeginInit();
        SuspendLayout();
        // 
        // pathWidthInput
        // 
        pathWidthInput.DecimalPlaces = 1;
        pathWidthInput.Location = new Point(82, 7);
        pathWidthInput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        pathWidthInput.Name = "pathWidthInput";
        pathWidthInput.Size = new Size(94, 23);
        pathWidthInput.TabIndex = 0;
        pathWidthInput.Value = new decimal(new int[] { 3, 0, 0, 0 });
        pathWidthInput.ValueChanged += PathWidthInput_ValueChanged;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(12, 9);
        label1.Name = "label1";
        label1.Size = new Size(64, 15);
        label1.TabIndex = 1;
        label1.Text = "Path width";
        // 
        // btnSave
        // 
        btnSave.Location = new Point(101, 57);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(75, 23);
        btnSave.TabIndex = 3;
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += BtnSave_Click;
        // 
        // SettingsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(192, 92);
        Controls.Add(btnSave);
        Controls.Add(label1);
        Controls.Add(pathWidthInput);
        FormBorderStyle = FormBorderStyle.SizableToolWindow;
        Name = "SettingsForm";
        Text = "Mapper Settings";
        ((System.ComponentModel.ISupportInitialize)pathWidthInput).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private NumericUpDown pathWidthInput;
    private Label label1;
    private Button btnSave;
}