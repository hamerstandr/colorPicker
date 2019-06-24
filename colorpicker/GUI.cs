using MetroFramework.Forms;
using System;
using System.Windows.Forms;

namespace DesktopColorPicker
{
    public partial class GUI : MetroForm
    {
        public GUI()
        {
            InitializeComponent();
            magnifyingGlass1.UpdateTimer.Start();
            magnifyingGlass1.MovingGlass.MagnifyingGlass.Click += new EventHandler(MagnifyingGlass_Click);
            magnifyingGlass1.MovingGlass.VisibleChanged += new EventHandler(MovingGlass_VisibleChanged);
            magnifyingGlass1.MovingGlass.MagnifyingGlass.BeforeMakingScreenshot += new MagnifyingGlass.MakingScreenshotDelegate(MagnifyingGlass_BeforeMakingScreenshot);
            magnifyingGlass1.MovingGlass.MagnifyingGlass.AfterMakingScreenshot += new MagnifyingGlass.MakingScreenshotDelegate(MagnifyingGlass_AfterMakingScreenshot);
        }

        private void MovingGlass_VisibleChanged(object sender, EventArgs e)
        {
            // Make this not the top window if the moving glass is shown because it will hide the glass display otherwise
            TopMost = !magnifyingGlass1.MovingGlass.Visible;
        }

        private void MagnifyingGlass_Click(object sender, EventArgs e)
        {
            // Select the color trough the moving glass
            SelectColor();
        }

        private void magnifyingGlass1_DisplayUpdated(MagnifyingGlass sender)
        {
            // Update the current color preview panels background color
            panel1.BackColor = magnifyingGlass1.PixelColor;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Troggle the pixel view
            magnifyingGlass1.ShowPixel = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            // Troggle the position view
            magnifyingGlass1.ShowPosition = checkBox2.Checked;
        }

        private void GUI_Deactivate(object sender, EventArgs e)
        {
            // Select the current color if the form lost the focus
            if (!magnifyingGlass1.MovingGlass.Visible)
            {
                SelectColor();
            }
        }

        private void SelectColor()
        {
            // Show the selected color and properties
            panel2.BackColor = panel1.BackColor;
            textBox1.Text = "R: " + panel2.BackColor.R.ToString();
            textBox2.Text = "G: " + panel2.BackColor.G.ToString();
            textBox3.Text = "B: " + panel2.BackColor.B.ToString();
            HextextBox.Text = "#" + panel2.BackColor.Name;
            // Try to activate the form after selecting a color because we lost the focus for sure
            Activate();
        }

        private void MagnifyingGlass_AfterMakingScreenshot(object sender)
        {
            // Show this again after the screenshot has been taken
            Show();
        }

        private void MagnifyingGlass_BeforeMakingScreenshot(object sender)
        {
            // Hide this before the moving glass will take thescreenshot for working with it as screen image
            Hide();
        }
    }
}
