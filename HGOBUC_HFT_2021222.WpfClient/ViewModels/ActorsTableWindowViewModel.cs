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
    public class ActorsTableWindowViewModel: ObservableRecipient
    {
        public RestCollection<Actors> Actor { get; set; }

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



        public ActorsTableWindowViewModel()
        {
           Actor = new RestCollection<Actors>("http://localhost:27826/", "actor", "hub");
           CreateActorCommand = new RelayCommand(
                    () =>
                    {
                        Actor.Add(new Actors()
                        {
                            ActorName = SelectedActor.ActorName
                        });
                    }
                );

            EditActorCommand = new RelayCommand(() =>
            {
                Actor.Update(SelectedActor);
            }
            );

            DeleteActorCommand = new RelayCommand(() =>
            {
               Actor.Delete(SelectedActor.ActorId);
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
