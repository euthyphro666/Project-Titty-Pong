using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TittyPong.Events;
using TittyPong.Events.Args;

namespace TittyPong.NET.Game
{
    public class InputMessenger
    {
        private EventManager Events;
        private ConcurrentQueue<InputEventArgs> States;
        private Thread SendThread;
        private bool IsRunning;
        private ManualResetEvent Reset;

        public InputMessenger(EventManager events)
        {
            Events = events;
            States = new ConcurrentQueue<InputEventArgs>();
            SendThread = new Thread(Run);
            Reset = new ManualResetEvent(true);

            Events.InputEvent += HandleInputEvent;
        }

        private void HandleInputEvent(object sender, InputEventArgs e)
        {
            States.Enqueue(e);
            Reset.Set();
        }

        public void Start()
        {
            IsRunning = true;
            SendThread.Start();
        }

        public void Run()
        {
            while(IsRunning)
            {
                while (!States.IsEmpty)
                {
                    if (States.TryDequeue(out var e))
                        Events.OnSendInputEvent(this, e);
                    else
                        break;
                }
                Reset.WaitOne();
            }   
        }

        public void Stop()
        {
            IsRunning = false;
            SendThread.Join();
        }
    }
}
