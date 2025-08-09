using RockBoxPlaylistTool.Data;
using RockBoxPlaylistTool.Music;
using RockBoxPlaylistTool.Playlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RockBoxPlaylistTool.Main
{
    public class MainViewModel : BindableBase
    {
        private MusicViewModel musicViewModel;
        private PlaylistViewModel playlistViewModel;
        private DelegateCommand addToPlaylistCommand;
        private DelegateCommand removeFromPlaylistCommand;
        private DelegateCommand moveSongUpCommand;
        private DelegateCommand moveSongDownCommand;
        private DelegateCommand savePlaylistCommand;
        private IFileWriter fileWriter;
        private string notificationText;
        private bool isError;
        public MainViewModel(IFileWriter fileWriter)
        {
            musicViewModel = new MusicViewModel();
            playlistViewModel = new PlaylistViewModel();
            addToPlaylistCommand = new DelegateCommand(addExecute, canAddExecute);
            removeFromPlaylistCommand = new DelegateCommand(removeExecute, canRemoveExecute);
            moveSongUpCommand = new DelegateCommand(moveUpExecute, canMoveUpExecute);
            moveSongDownCommand = new DelegateCommand(moveDownExecute, canMoveDownExecute);
            savePlaylistCommand = new DelegateCommand(saveExecute, canSaveExecute);
            this.fileWriter = fileWriter;
            musicViewModel.PropertyChanged += MusicViewModel_PropertyChanged;
            playlistViewModel.PropertyChanged += PlaylistViewModel_PropertyChanged;
            NotificationText = "Ready";
        }
        public string NotificationText
        { 
            get { return notificationText; }
            set { SetProperty(ref notificationText, value); }
        }
        public bool IsError
        { 
            get { return isError; }
            set { SetProperty(ref isError, value); }
        }
        private void PlaylistViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Selected")
            {
                removeFromPlaylistCommand.RaiseCanExecuteChanged();
                moveSongUpCommand.RaiseCanExecuteChanged();
                moveSongDownCommand.RaiseCanExecuteChanged();
                savePlaylistCommand.RaiseCanExecuteChanged();
            }
        }

        private void MusicViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Selected")
            {
                addToPlaylistCommand.RaiseCanExecuteChanged();
            }
        }

        public object Model { 
            get { return this; } 
        }
        public MusicViewModel MusicViewModel { get { return musicViewModel; } }
        public PlaylistViewModel PlaylistViewModel { get { return playlistViewModel; } }
        public ICommand AddToPlaylistCommand { 
            get { return addToPlaylistCommand; } 
        }
        public ICommand RemoveFromPlaylistCommand { 
            get { return removeFromPlaylistCommand; } 
        }
        public ICommand MoveSongUpCommand { 
            get { return moveSongUpCommand; } 
        }
        public ICommand MoveSongDownCommand { 
            get { return moveSongDownCommand; } 
        }
        public ICommand SavePlaylistCommand { 
            get { return savePlaylistCommand; } 
        }
        public bool canAddExecute()
        {
            return musicViewModel.Selected != null;
        }
        public bool canRemoveExecute()
        {
            return playlistViewModel.Selected != null;
        }
        public bool canMoveUpExecute()
        {
            return playlistViewModel.Selected != null;
        }
        public bool canMoveDownExecute()
        {
            return playlistViewModel.Selected != null;
        }
        public bool canSaveExecute()
        {
            return !string.IsNullOrWhiteSpace(playlistViewModel.Path);
        }
        public void addExecute()
        {
            foreach (var item in musicViewModel.Items)
            {
                if (!item.IsSelected) { continue; }
                playlistViewModel.AppendSong(item);
            }
        }
        public void removeExecute()
        {
            playlistViewModel.RemoveSong(playlistViewModel.Selected);
        }
        public void moveUpExecute()
        {
            playlistViewModel.MoveSongUp(playlistViewModel.Selected);
        }
        public void moveDownExecute()
        {
            playlistViewModel.MoveSongDown(playlistViewModel.Selected);
        }
        public void saveExecute()
        {
            var result = fileWriter.SavePlaylist(playlistViewModel.Path, playlistViewModel.Items);
            if (result)
            {
                NotificationText = "Success";
                IsError = false;
            }
            else
            {
                NotificationText = "Error: Failure to save playlist";
                IsError = true;
            }
        }
    }
}
