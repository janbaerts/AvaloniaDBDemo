using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Runtime.InteropServices;

namespace AvaloniaDBDemo
{
    public class NewMemberWindow : Window
    {
        public Member member;
        public bool isUpdate;
        public bool isOk;
        TextBox[] fields = new TextBox[3];
        TextBlock statusBar;

        public NewMemberWindow(Member member, bool isUpdate)
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.member = member;
            this.isUpdate = isUpdate;
            InitializeWindow();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void InitializeWindow()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                this.CanResize = true;
            }
            TextBox idTextBox = this.FindControl<TextBox>("idTextBox");
            idTextBox.Text = member.Id.ToString();

            fields[0] = this.FindControl<TextBox>("lastnameTextBox");
            if (member.LastName != null) { fields[0].Text = member.LastName; }
            fields[1] = this.FindControl<TextBox>("firstnameTextBox");
            if (member.FirstName != null) { fields[1].Text = member.FirstName; }
            fields[2] = this.FindControl<TextBox>("mailAddressTextBox");
            if (member.MailAddress != null) { fields[2].Text = member.MailAddress; }

            TextBox membershipExpiryDateTextBox = this.FindControl<TextBox>("membershipExpiryDateTextBox");
            membershipExpiryDateTextBox.Text = member.MembershipExpiryDate;
            
            Button okButton = this.FindControl<Button>("okButton");
            Button cancelButton = this.FindControl<Button>("cancelButton");

            statusBar = this.FindControl<TextBlock>("statusBar");

            okButton.Click += OkButton_Click;
            cancelButton.Click += CancelButton_Click;
        }

        private void CancelButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.Close();
        }

        private void OkButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (fields[0].Text != "" && fields[1].Text != "" && fields[2].Text != "")
            {
                isOk = true;
                member.LastName = fields[0].Text;
                member.FirstName = fields[1].Text;
                member.MailAddress = fields[2].Text;
                this.Close();
            }
            else
            {
                isOk = false;
                statusBar.Text = "Gelieve alle velden in te vullen.";
            }
        }
    }
}
