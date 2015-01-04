using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Paprotski.Lab6.AudioPlayer
{
    public class AudioPlayer : IDisposable
    {
        #region Fields

        private readonly List<PlayListController> activePlayListsController;

        private readonly List<PlayListController> allPlayListsController;

        private readonly List<PlayListController> passivePlayListsController;

        private readonly List<PlayList> playLists;

        private bool disposed = false;

        #endregion

        #region Ctors

        public AudioPlayer(List<PlayList> playLists)
        {
            this.playLists = playLists;
            this.allPlayListsController = new List<PlayListController>();
            this.activePlayListsController = new List<PlayListController>();
            this.passivePlayListsController = new List<PlayListController>();

            foreach (var controller in playLists.Select(list => new PlayListController(list)))
            {
                controller.StateChanged += this.ControllerOnStateChanged;
                controller.PlaybackEnd += this.ControllerOnPlaybackEnd;
                this.allPlayListsController.Add(controller);
            }
        }

        ~AudioPlayer()
        {
            this.Dispose(false);
        }

        #endregion

        #region Public Events

        public event EventHandler StateChanged;

        #endregion

        #region Public Methods and Operators

        public void Execute(int id, CommandType type)
        {
            if (this.playLists.Find(list => list.Id == id) == null)
            {
                throw new NullReferenceException("Incorrect ID");
            }

            switch (type)
            {
                case CommandType.Play:
                    this.PushPlayListInController(id, ControllerType.Active, ControllerType.All);
                    break;
                case CommandType.Resume:
                    this.PushPlayListInController(id, ControllerType.Active, ControllerType.Passive);
                    break;
                case CommandType.Pause:
                    this.PushPlayListInController(id, ControllerType.Passive, ControllerType.Active);
                    break;
                case CommandType.Stop:
                    this.PushPlayListInController(id, ControllerType.All, ControllerType.Active);
                    this.PushPlayListInController(id, ControllerType.All, ControllerType.Passive);
                    break;
                default:
                    throw new InvalidEnumArgumentException("Invalid command");
            }
        }

        public Tuple<string, string> GetState(ControllerType type)
        {
            var playListBuilder = new StringBuilder();
            var currentPlaybackSong = new StringBuilder();

            switch (type)
            {
                case ControllerType.All:
                    foreach (var controller in this.allPlayListsController)
                    {
                        playListBuilder.AppendLine(controller.PlayList.ToString());
                    }
                    break;
                case ControllerType.Active:
                    foreach (var controller in this.activePlayListsController)
                    {
                        playListBuilder.AppendLine(controller.PlayList.ToString());
                        currentPlaybackSong.AppendLine(controller.ToString());
                    }
                    break;
                case ControllerType.Passive:
                    foreach (var controller in this.passivePlayListsController)
                    {
                        playListBuilder.AppendLine(controller.PlayList.ToString());
                        currentPlaybackSong.AppendLine(controller.ToString());
                    }
                    break;
                default:
                    throw new InvalidEnumArgumentException("Invalid controller type");
            }

            return new Tuple<string, string>(playListBuilder.ToString(), currentPlaybackSong.ToString());
        }

        #endregion

        #region Methods

        protected virtual void OnStateChanged()
        {
            var handler = this.StateChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void ControllerOnPlaybackEnd(object sender, EventArgs eventArgs)
        {
            var playListController = sender as PlayListController;
            if (playListController != null)
            {
                this.PushPlayListInController(playListController.PlayList.Id, ControllerType.All, ControllerType.Active);
            }
        }

        private void ControllerOnStateChanged(object sender, EventArgs eventArgs)
        {
            var playListController = sender as PlayListController;
            if (playListController != null)
            {
                this.OnStateChanged();
            }
        }

        private void PushPlayListInController(int id, ControllerType fromType, ControllerType toType)
        {
            PlayListController controller = null;

            switch (fromType)
            {
                case ControllerType.All:
                    switch (toType)
                    {
                        case ControllerType.Active:
                            controller = this.activePlayListsController.Find(control => control.PlayList.Id == id);
                            this.activePlayListsController.Remove(controller); 
                            break;
                        case ControllerType.Passive:
                            controller = this.passivePlayListsController.Find(control => control.PlayList.Id == id);
                            this.passivePlayListsController.Remove(controller);
                            break;
                    }

                    if (controller != null)
                    {
                        this.allPlayListsController.Add(controller);
                        controller.Stop();
                    }
                    break;
                case ControllerType.Active:
                    switch (toType)
                    {
                        case ControllerType.All:
                            controller = this.allPlayListsController.Find(control => control.PlayList.Id == id);
                            this.allPlayListsController.Remove(controller);
                            this.activePlayListsController.Add(controller);
                            controller.Start();
                            break;
                        case ControllerType.Passive:
                            controller = this.passivePlayListsController.Find(control => control.PlayList.Id == id);
                            this.passivePlayListsController.Remove(controller);
                            this.activePlayListsController.Add(controller);
                            controller.Resume();
                            break;
                    }
                    break;
                case ControllerType.Passive:
                    switch (toType)
                    {
                        case ControllerType.All:
                            controller = this.allPlayListsController.Find(control => control.PlayList.Id == id);
                            this.allPlayListsController.Remove(controller);
                            this.passivePlayListsController.Add(controller);
                            break;
                        case ControllerType.Active:
                            controller = this.activePlayListsController.Find(control => control.PlayList.Id == id);
                            this.activePlayListsController.Remove(controller);
                            this.passivePlayListsController.Add(controller);
                            controller.Pause();
                            break;
                    }
                    break;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                foreach (var controller in allPlayListsController)
                {
                    controller.StateChanged -= this.ControllerOnStateChanged;
                    controller.PlaybackEnd -= this.ControllerOnPlaybackEnd;
                    controller.Dispose();
                }

                foreach (var controller in activePlayListsController)
                {
                    controller.StateChanged -= this.ControllerOnStateChanged;
                    controller.PlaybackEnd -= this.ControllerOnPlaybackEnd;
                    controller.Dispose();
                }

                foreach (var controller in passivePlayListsController)
                {
                    controller.StateChanged -= this.ControllerOnStateChanged;
                    controller.PlaybackEnd -= this.ControllerOnPlaybackEnd;
                    controller.Dispose();
                }
            }

            disposed = false; 
        }

        #endregion
    }
}