using CASCLib;
using System;
using System.IO;
using System.Windows.Forms;

namespace CASCExplorer
{
    public partial class OpenStorageForm : Form
    {
        public string StoragePath { get; private set; }
        public string Product { get; private set; }

        private CASCGameType _gameType;

        public OpenStorageForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (storageFolderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                DialogResult = DialogResult.Cancel;
                return;
            }

            string path = storageFolderBrowserDialog.SelectedPath;

            if (!File.Exists(Path.Combine(path, ".build.info")))
            {
                MessageBox.Show("Invalid storage folder selected!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _gameType = CASCGame.DetectLocalGame(path);

            if (_gameType == CASCGameType.Unknown)
                return;

            textBox1.Text = path;

            if (_gameType == CASCGameType.WoW)
                wowProductComboBox.Enabled = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (_gameType == CASCGameType.WoW && wowProductComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Must select type of World of Warcraft product!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StoragePath = textBox1.Text;
            Product = (string)wowProductComboBox.SelectedItem;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
