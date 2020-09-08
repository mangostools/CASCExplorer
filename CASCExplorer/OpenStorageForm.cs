using CASCLib;
using System;
using System.Collections.Generic;
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

        private readonly Dictionary<CASCGameType, string[]> sharedInstallProducts = new Dictionary<CASCGameType, string[]>
        {
            [CASCGameType.WoW] = new string[] { "wow", "wowt", "wow_beta", "wowe1", "wow_classic", "wow_classic_beta", "wow_classic_ptr" },
            [CASCGameType.WC3] = new string[] { "w3", "w3t" },
        };

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

            productComboBox.Items.Clear();
            productComboBox.Enabled = sharedInstallProducts.TryGetValue(_gameType, out var products);
            if (productComboBox.Enabled)
            {
                productComboBox.Items.AddRange(products);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (sharedInstallProducts.ContainsKey(_gameType) && productComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Must select type of game product!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StoragePath = textBox1.Text;
            Product = (string)productComboBox.SelectedItem;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
