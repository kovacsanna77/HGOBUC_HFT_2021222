using HGOBUC_HFT_2021222.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HGOBUC_HFT_2021222.WpfClient.ViewModels
{
    public class ActrosTableWindowViewModel: ObservableRecipient
    {
        public RestCollection<Actors> Actors { get; set; }

        private Actors selectedActor;

        public Actors SelectedActor
        {
            get { return selectedActor;; }
            set {
                if (value != null)
                {
                    selectedActor = new Actors()
                    {
                        ActorName = value.ActorName,
                        ActorId = value.ActorId

                    };
                }
                OnPropertyChanged();
                (DeleteActorCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand CreateActorCommand { get; set; }
        public ICommand DeleteActorCommand { get; set; }
        public ICommand EditActorCommand { get; set; }



        public ActrosTableWindowViewModel()
        {
            Actors = new RestCollection<Actors>("http://localhost:27826/", "actor");
           CreateActorCommand = new RelayCommand(
                    () =>
                    {
                        Actors.Add(new Actors()
                        {
                            ActorName = SelectedActor.ActorName
                        });
                    }
                );

            EditActorCommand = new RelayCommand(() =>
            {
                Actors.Update(SelectedActor);
            }
            );

            DeleteActorCommand = new RelayCommand(() =>
            {
               Actors.Delete(SelectedActor.ActorId);
            },
                () =>
                {
                    return SelectedActor != null;
                }
            );
            SelectedActor = new Actors();
        }
     }
}
