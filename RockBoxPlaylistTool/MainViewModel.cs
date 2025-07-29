using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RockBoxPlaylistTool
{
    public class MainViewModel
    {
        private MusicViewModel musicViewModel;
        private PlaylistViewModel playlistViewModel;
        private ICommand addToPlaylistCommand;
        private ICommand removeFromPlaylistCommand;
        private ICommand moveSongUpCommand;
        private ICommand moveSongDownCommand;
        private ICommand savePlaylistCommand;
        private IFileWriter fileWriter;
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
            playlistViewModel.AppendSong(musicViewModel.Selected);
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
            fileWriter.SavePlaylist(playlistViewModel.Path, playlistViewModel.Items);
        }
    }
}
