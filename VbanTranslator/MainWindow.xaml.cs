using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VbanTranslator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool ascii = false;

        public MainWindow()
        {
            InitializeComponent();

            L_Info.Content = "Sender must support ASCII\r\nsymbols in the form of \\12\r\nfor non-alphanumeric characters\r\nand symbols.";
        }

        private void BTN_Translate_Click(object sender, RoutedEventArgs e)
        {
            if (ascii && TB_SName.Text.Length != 16)
            {
                MessageBox.Show("Stream name has to be exactly 16 characters long for pure ASCII mode.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<byte> bytes = ascii ? [0x56, 0x42, 0x41, 0x4E, 0x52, 0x20, 0x20, 0x01] : [0x56, 0x42, 0x41, 0x4E, 0x52, 0x00, 0x00, 0x00];

            string sName = TB_SName.Text;
            bytes.AddRange(Encoding.ASCII.GetBytes(sName));

            if (ascii) bytes.AddRange([0x20, 0x20, 0x20, 0x20]);
            else for (int i = sName.Length; i < 20; i++) bytes.Add(0x00);

            string vText = TB_VBAN.Text;
            bytes.AddRange(Encoding.ASCII.GetBytes(vText));


            List<char> chars = [.. Encoding.ASCII.GetString([.. bytes]).ToCharArray()];

            if (!ascii)
            {
                int cc = chars.Count;

                for (int i = 0; i < cc; i++)
                {
                    if (chars[i] == 0)
                    {
                        chars.Insert(i, '\\');
                        i++;
                        chars[i] = '0';
                        chars.Insert(i, '0');
                        i++;
                        cc += 2;
                    }
                }
            }

            Clipboard.SetText(new string([.. chars]));

            MessageBox.Show("Text copied to clipboard.");
        }

        private void CKB_ASCII_Checked(object sender, RoutedEventArgs e)
        {
            ascii = true;
            L_Info.Content = "Stream name in VM/Matrix\r\nand this application needs to be\r\nexactly 16 characters long.";
        }

        private void CKB_ASCII_Unchecked(object sender, RoutedEventArgs e)
        { 
            ascii = false;
            L_Info.Content = "Sender must support ASCII\r\nsymbols in the form of \\12\r\nfor non-alphanumeric characters\r\nand symbols.";
        }
    }
}