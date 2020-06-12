using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IzradaPreglednika
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            webBrowser1.DocumentCompleted += (o, e) =>
            {
                toolStripStatusLabel1.Text = "Stranica je uspješno učitana";
            };
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Stranica se učitava";
            webBrowser1.GoHome();
           
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Stranica se učitava";

            if (String.IsNullOrEmpty(txtUrl.Text) || txtUrl.Text.Equals("about:blank"))
            {
                toolStripStatusLabel1.Text = "Greška";
                MessageBox.Show("Enter a valid URL.");
                txtUrl.Focus();
                return;
            }
            
            OpenURLInBrowser(txtUrl.Text);
          
        }

        private void txtUrl_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                toolStripStatusLabel1.Text = "Stranica se učitava";

                string url = UrediUnos(txtUrl.Text);

                if (ProvjeraUnosa(url))
                {

                    OpenURLInBrowser(url);
                 
                }

            }
        }

        private void OpenURLInBrowser(string url)
        {
            toolStripStatusLabel1.Text = "Stranica se učitava";

            try
                {
                    webBrowser1.Navigate(new Uri(url));
                    
            }
                catch (System.UriFormatException)
                {
                toolStripStatusLabel1.Text = "Greška";
                return;
                }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
                toolStripStatusLabel1.Text = "Stranica se učitava";
                webBrowser1.GoBack();
               
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
                toolStripStatusLabel1.Text = "Stranica se učitava";
            webBrowser1.GoForward();
            
        }

        
        

        private string UrediUnos(string url)
        {
           
            if (!url.StartsWith("https://www.") && !url.StartsWith("https://www."))
            {
                url = "http://www." + url;
            }
            else if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                url = "http://" + url;
            }

            return url;
            
        }

        private bool ProvjeraUnosa(string url)
        {
            int count;
            if (String.IsNullOrEmpty(url) || url.Equals("about:blank"))
            {
                toolStripStatusLabel1.Text = "Greška";
                MessageBox.Show("No URL!!!");
                txtUrl.Focus();               
                return false;
            }
            else if (!((count = url.Count(c => c == '.')) == 2))
            {
                toolStripStatusLabel1.Text = "Greška";
                MessageBox.Show("Not a valid URL.");
                txtUrl.Focus();               
                return false;
            }

            Uri adresa;
            if (Uri.TryCreate(url, UriKind.Absolute, out adresa))
            {
                return true;
            }
            else
            {
                toolStripStatusLabel1.Text = "Greška";
                MessageBox.Show("Bad Url!");
                return false;

            }

          
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Prekid učitavanja";
            webBrowser1.Stop();
        }

      
    }
}
