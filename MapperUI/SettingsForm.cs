using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapperUI;
public partial class SettingsForm : Form
{
    private readonly MapperSettings settings;

    public SettingsForm(MapperSettings settings)
    {
        InitializeComponent();
        this.settings = settings;

        pathWidthInput.Value = (decimal)settings.PathWidth;
    }

    private void PathWidthInput_ValueChanged(object sender, EventArgs e)
    {
        settings.PathWidth = (int)pathWidthInput.Value;
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        settings.Save();
    }
}
