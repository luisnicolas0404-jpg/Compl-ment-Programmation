using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestionMail
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEnvoyer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(txtExpediteur.Text);
                mail.To.Add(txtDestinataire.Text);
                mail.Subject = txtObjet.Text;
                mail.Body = txtMessage.Text;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(
                    txtExpediteur.Text,
                    txtMotDePasse.Password
                );

                smtp.Send(mail);

                MessageBox.Show("Mail envoyé avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible d'envoyer le mail.\n\n" + ex.Message);
            }
        }
    }
}
