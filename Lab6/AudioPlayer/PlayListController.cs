using System;
using System.Threading;

namespace Paprotski.Lab6.AudioPlayer
{
    public class PlayListController : IDisposable
    {
        #region Fields

        private readonly Thread thread;

        private TimeSpan currentSongTime;

        private TimeSpan currentPlaybackTime;

        private int currentPlaybackSongId;

        private bool disposed = false; 

        #endregion

        #region Ctors

        public PlayListController(PlayList playList)
        {
            this.PlayList = playList;
            this.Song = playList.SongList[currentPlaybackSongId];

            this.thread = new Thread(this.Play);
            this.IsPlaying = false;
            this.IsPaused = false;

            this.currentPlaybackTime = TimeSpan.Zero;
            this.currentSongTime = TimeSpan.Zero;
        }

        ~PlayListController()
        {
            this.Dispose(false);
        }

        #endregion

        #region Public Events

        public event EventHandler StateChanged;

        public event EventHandler PlaybackEnd;

        #endregion

        #region Public Properties

        public PlayList PlayList { get; private set; }

        public Song Song { get; private set; }

        public bool IsPlaying { get; private set; }

        public bool IsPaused { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void Start()
        {
            if (ReferenceEquals(this.thread, null))
            {
                throw new NullReferenceException();
            }

            if (!this.thread.IsAlive)
            {
                this.IsPlaying = true; 
                this.thread.Start();
            }
        }

        public void Stop()
        {
            if (ReferenceEquals(this.thread, null))
            {
                throw new NullReferenceException();
            }

            if (!this.thread.IsAlive)
            {
                return;
            }

            try
            {
                this.IsPaused = false; 
                this.IsPlaying = false;
                this.thread.Abort();
            }
            catch (ThreadAbortException)
            {   
                Thread.ResetAbort();
            }
        }

        public void Pause()
        {
            if (ReferenceEquals(this.thread, null))
            {
                throw new NullReferenceException();
            }

            if (this.thread.IsAlive)
            {
                this.IsPaused = true; 
                this.thread.Suspend();
            }
        }

        public void Resume()
        {
            if (ReferenceEquals(this.thread, null))
            {
                throw new NullReferenceException();
            }

            if (this.thread.ThreadState == ThreadState.Suspended)
            {
                this.IsPaused = false; 
                this.thread.Resume();
            }
        }

        protected virtual void OnStateChanged()
        {
            var handler = this.StateChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void OnPlaybackEnd()
        {
            var handler = this.PlaybackEnd;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public override string ToString()
        {
            return string.Format("[{0}] - {1}", this.currentSongTime, this.Song);
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
                this.Stop(); 
            }

            disposed = false;
        }

        #endregion

        #region Methods

        private void Play()
        {
            while (this.IsPlaying)
            {
                this.UpdateState();
                this.OnStateChanged();
                Thread.Sleep(2000);
            }
        }

        private void UpdateState()
        {
            var counter = TimeSpan.FromSeconds(2);

            this.currentPlaybackTime += counter;
            this.currentSongTime += counter;

            if (this.PlayList.PlayListTime - this.currentPlaybackTime <= TimeSpan.Zero)
            {
                this.IsPlaying = false;
                this.OnPlaybackEnd();
                return;
            }

            if (this.Song.TrackTime - this.currentSongTime <= TimeSpan.Zero)
            {
                this.Song = this.PlayList.SongList[++currentPlaybackSongId];
                this.currentSongTime = TimeSpan.Zero;
            }
        }

        #endregion
    }
}