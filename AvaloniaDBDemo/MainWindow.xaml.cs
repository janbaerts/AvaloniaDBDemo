﻿using Avalonia;
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
        MenuItem editMemberMenuItem;
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

            editMemberMenuItem = this.FindControl<MenuItem>("editMemberMenuItem");
            editMemberMenuItem.Click += EditMember_Click;

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
            NewMemberWindow nmw = new NewMemberWindow(newmember, false);
            nmw.Closing += NewMemberWindowClosed;
            nmw.ShowDialog(this);
        }

        private void EditMember_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
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
            NewMemberWindow nmw = (NewMemberWindow)sender;
            Debug.WriteLine("Dialog closed.");
            if (nmw.isOk)
            {
                Debug.WriteLine("Is OK.");
                DBAccess.SaveMember(nmw.member, nmw.isUpdate);
                if (!nmw.isUpdate)
                {
                    members.Add(nmw.member);
                }
                dataGrid.Items = null;
                dataGrid.Items = members;
            }
            nmw.Closing -= NewMemberWindowClosed;
        }
    }
}
