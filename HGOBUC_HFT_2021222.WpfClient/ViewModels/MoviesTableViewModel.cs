using HGOBUC_HFT_2021222.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using HGOBUC_HFT_2021222.WpfClient.NonCrudWindows;

namespace HGOBUC_HFT_2021222.WpfClient.ViewModels
{
    public  class MoviesTableViewMode: ObservableRecipient
    {
        public RestCollection<Movie> Movies { get; set; }

        

        public List<string> nonCruds { get; set; }

        private string selectedNonCrud;

        public string SelectedNonCrud
        {
            get { return selectedNonCrud; }
            set { SetProperty(ref selectedNonCrud, value); }
        }


        private Movie selectedMovie;

        public Movie SelectedMovie
        {
            get { return selectedMovie; }
            set
            {
                SetProperty(ref selectedMovie, value);
                //if (value != null)
                //{
                //    selectedMovie = new Movie()
                //    {
                //        Title = value.Title,
                //        MovieId = value.MovieId,

                //    };
                //}
                OnPropertyChanged();
                (DeleteMovieCommand as RelayCommand).NotifyCanExecuteChanged();
                (EditMovieCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand CreateMovieCommand { get; set; }
        public ICommand DeleteMovieCommand { get; set; }
        public ICommand EditMovieCommand { get; set; }
        public ICommand ShowResultCommand { get; set; }

        public static bool IsInDesingMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public MoviesTableViewMode()
        {
            Movies = new RestCollection<Movie>("http://localhost:27826/", "movie", "hub");
           

            nonCruds = getListofNonCruds();
            //mindegyiknek külön ablak, hogy lehessen normélisan ábrázolni? 

            ShowResultCommand = new RelayCommand(() => OpenNonCrud(SelectedNonCrud));

            CreateMovieCommand = new RelayCommand(
                    () =>
                    {
                        Movies.Add(new Movie()
                        {
                            Title = SelectedMovie.getCopy().Title
                        });
                    }
                );

            EditMovieCommand = new RelayCommand(() =>
            {
                Movies.Update(SelectedMovie);
            }
            );

            DeleteMovieCommand = new RelayCommand(() =>
            {
                Movies.Delete(SelectedMovie.MovieId);
            },
                () =>
                {
                    return SelectedMovie != null;
                }
            );
            SelectedMovie = new Movie();
        }

        public List<string> getListofNonCruds()
        {
            nonCruds = new List<string>();
            nonCruds.Add("Average episodes per network");
            nonCruds.Add("Movies with 10 ratin and with main actor");
            nonCruds.Add("Average movie ratings by network");
            nonCruds.Add("Actors with 5 rated movies");
            nonCruds.Add("Actros with 10 ratings");
            return nonCruds;
        }

        public void OpenNonCrud(string selected)
        {
            if (selected == "ActorsWith10Rating")
            {
                new NoncrudWindow(selected).ShowDialog();
            }else if( selected == "AverageEpidodesPerNetwork"){
                new AvgEpisodesPerNetworkWindow().ShowDialog();
            
            }
        }
    }
}
