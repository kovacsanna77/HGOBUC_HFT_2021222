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

namespace HGOBUC_HFT_2021222.WpfClient.ViewModels
{
    public  class MoviesTableViewMode: ObservableRecipient
    {
        public RestCollection<Movie> Movies { get; set; }

        private Movie selectedMovie;

        public Movie SelectedMovie
        {
            get { return selectedMovie; }
            set
            {
                if (value != null)
                {
                    selectedMovie = new Movie()
                    {
                        Title = value.Title,
                        MovieId = value.MovieId,

                    };
                }
                OnPropertyChanged();
                (DeleteMovieCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand CreateMovieCommand { get; set; }
        public ICommand DeleteMovieCommand { get; set; }
        public ICommand EditMovieCommand { get; set; }

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

            CreateMovieCommand = new RelayCommand(
                    () =>
                    {
                        Movies.Add(new Movie()
                        {
                            Title = SelectedMovie.Title
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
    }
}
