namespace Paprotski.Lab6.AudioPlayer
{
    public class AudioPlayerCommand : ICommand
    {
        #region Fields

        private readonly CommandType commandType;

        private readonly int id;

        private readonly AudioPlayer player;

        #endregion

        #region Ctors

        public AudioPlayerCommand(AudioPlayer player, int id, CommandType commandType)
        {
            this.player = player;
            this.id = id;
            this.commandType = commandType;
        }

        #endregion

        #region Public Methods and Operators

        public void Execute()
        {
            this.player.Execute(this.id, this.commandType);
        }

        #endregion
    }
}