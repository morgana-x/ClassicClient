namespace ClassicConnect.Event
{
    public partial class LevelEvents
    {
        public class LoadChunkEventArgs : EventArgs
        {
            public byte bytePercent = 0;
            public float Percentage = 0;
            public LoadChunkEventArgs(byte percentage)
            {
                bytePercent = percentage;
                Percentage = (float)(percentage / 100.0f);
            }
        }
        public event EventHandler<LoadChunkEventArgs> LoadChunkEvent;
        internal void OnLoadChunk(LoadChunkEventArgs e)
        {
            LoadChunkEvent.Invoke(this, e);
        }

        public class StartLoadEventArgs : EventArgs
        {
            public StartLoadEventArgs()
            {
            }
        }
        public event EventHandler<StartLoadEventArgs> StartLoadEvent;
        internal void OnStartLoad(StartLoadEventArgs e)
        {
            StartLoadEvent.Invoke(this, e);
        }

        public class FinishLoadEventArgs : EventArgs
        {
            public FinishLoadEventArgs()
            {
            }
        }
        public event EventHandler<FinishLoadEventArgs> OnFinishLoadEvent;
        internal void OnFinishLoad(FinishLoadEventArgs e)
        {
            OnFinishLoadEvent.Invoke(this, e);
        }


        public class SetBlockEventArgs : EventArgs
        {
            public short X;
            public short Y;
            public short Z;
            public short Block;
            public SetBlockEventArgs(short x, short y, short z, short block)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
                this.Block = block;
            }
        }
        public event EventHandler<SetBlockEventArgs> SetBlockEvent;
        internal void OnSetBlock(SetBlockEventArgs e)
        { 
            if (SetBlockEvent == null)
            {
                Console.WriteLine("Null setblockevent?");
                return;
            }
            SetBlockEvent.Invoke(this, e);
        }
    }
}
