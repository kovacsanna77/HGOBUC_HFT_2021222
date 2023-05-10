using HGOBUC_HFT_2021222.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace HGOBUC_HFT_2021222.WpfClient.ViewModels
{
    public class RolesTableWindowViewModel : ObservableRecipient
    {
        public RestCollection<Role> roles { get; set; }

        private Role selectedRole;

        public Role SelectedRole
        {
            get { return selectedRole; }
            set
            {
                SetProperty(ref selectedRole, value);
                //if (value != null)
                //{
                //    selectedRole = new Role()
                //    {
                //        RoleId = value.RoleId,
                //        RoleName = value.RoleName

                //    };
                //}
                OnPropertyChanged();
                (DeleteRoleCommand as RelayCommand).NotifyCanExecuteChanged();
                (EditRoleCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand CreateRoleCommand { get; set; }
        public ICommand DeleteRoleCommand { get; set; }
        public ICommand EditRoleCommand { get; set; }

        public RolesTableWindowViewModel()
        {
            roles = new RestCollection<Role>("http://localhost:27826/", "role", "hub");

            CreateRoleCommand = new RelayCommand(
                () =>
                {
                    roles.Add(new Role()
                    {
                        RoleName = SelectedRole.RoleName
                    });
                }
                );

            DeleteRoleCommand = new RelayCommand(
                () => roles.Delete(SelectedRole.RoleId)
                );

            EditRoleCommand = new RelayCommand(
                () => roles.Update(SelectedRole),
                () => SelectedRole != null
                );

            SelectedRole = new Role();
        }



    }
}
