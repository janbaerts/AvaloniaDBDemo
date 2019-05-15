using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace AvaloniaDBDemo
{
    public class MainWindow : Window
    {
        Grid grid;
        DataGrid dataGrid;
        MenuItem saveMemberMenuItem;
        MenuItem addMemberMenuItem;
        MenuItem removeMemberMenuItem;
        MenuItem extendMembershipMenuItem;

        IList<Member> members = new List<Member>();

        public MainWindow()
        {
            InitializeComponent();
            members = DBAccess.GetAllMembers();
            InitializeWindow();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void InitializeWindow()
        {
            grid = this.FindControl<Grid>("grid");

            dataGrid = this.FindControl<DataGrid>("dataGrid");
            dataGrid.Items = members;
            dataGrid.IsReadOnly = true;

            saveMemberMenuItem = this.FindControl<MenuItem>("editMemberMenuItem");
            saveMemberMenuItem.Click += SaveMember_Click;

            addMemberMenuItem = this.FindControl<MenuItem>("addMemberMenuItem");
            addMemberMenuItem.Click += AddMemberMenuItem_Click;

            removeMemberMenuItem = this.FindControl<MenuItem>("removeMemberMenuItem");
            removeMemberMenuItem.Click += RemoveMemberMenuItem_Click;

            extendMembershipMenuItem = this.FindControl<MenuItem>("extendMembershipMenuItem");
            extendMembershipMenuItem.Click += ExtendMembershipMenuItem_Click;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                this.CanResize = true;
            }
        }

        private void ExtendMembershipMenuItem_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex != -1)
            {
                Member extendMember = (Member)dataGrid.SelectedItem;
                extendMember.ExtendMembership();
                DBAccess.SaveMember(extendMember, true);
                dataGrid.Items = null;
                dataGrid.Items = members;
            }
        }

        private void RemoveMemberMenuItem_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex != -1)
            {
                DBAccess.RemoveMember((Member)dataGrid.SelectedItem);
                members.Remove((Member)dataGrid.SelectedItem);
                dataGrid.Items = null;
                dataGrid.Items = members;
            }
        }

        private void AddMemberMenuItem_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Member newmember = new Member();
            members.Add(newmember);
            NewMemberWindow nmw = new NewMemberWindow(newmember, false);
            nmw.Closing += NewMemberWindowClosed;
            nmw.ShowDialog(this);
        }

        private void SaveMember_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex != -1)
            {
                Member updatedMember = (Member)dataGrid.SelectedItem;
                NewMemberWindow nmw = new NewMemberWindow(updatedMember, true);
                nmw.Closing += NewMemberWindowClosed;
                nmw.ShowDialog(this);
            }
        }

        private void NewMemberWindowClosed(object sender, System.EventArgs e)
        {
            Debug.WriteLine("Dialog closed.");
            if (((NewMemberWindow)sender).isOk)
            {
                Debug.WriteLine("Is OK.");
                DBAccess.SaveMember(((NewMemberWindow)sender).member, ((NewMemberWindow)sender).isUpdate);
                dataGrid.Items = null;
                dataGrid.Items = members;
            }
            ((NewMemberWindow)sender).Closing -= NewMemberWindowClosed;
        }
    }
}
