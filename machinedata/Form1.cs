using System.Numerics;
using System.Text.RegularExpressions;

namespace machinedata
{
    public partial class Form1 : Form
    {
        private string formattedContent = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string input = formattedContent;

            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            string inputWithDateTime = input + " " + formattedDateTime;
            try
            {
                string compressedBase64 = Encode.CompressAndEncodeToBase64(inputWithDateTime);

                int iplength = input.Length;
                int comlength = compressedBase64.Length;
                //MessageBox.Show("Length of Compressed Base64: " + iplength.ToString());
                //MessageBox.Show("Length of Compressed Base64: " + comlength.ToString());

                Bitmap qrCodeImage = GenerateQRCode.GenerateAndDisplayQRCode(compressedBase64);

                pictureBox1.Image = qrCodeImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select the correct file.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;
                        string[] fileContent = File.ReadAllLines(filePath);

                        for (int i = 0; i < fileContent.Length; i++)
                        {
                            if (fileContent[i].StartsWith("#"))
                            {
                                fileContent[i] = fileContent[i] + "#";
                                formattedContent = Regex.Replace(fileContent[i], "(?<=\\w)Dev=", "#Dev=");
                            }
                        }

                        formattedContent = string.Join("", fileContent);

                        if (!formattedContent.EndsWith("#Dev=") && !formattedContent.EndsWith("#"))
                        {
                            formattedContent += "#";
                        }
                        textBox1.Text = filePath;
                        textBox1.BackColor = Color.White;
                        textBox1.Enabled = true;
                        textBox1.ReadOnly = true;
                        pictureBox1.Image = null;

                    }

                }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
