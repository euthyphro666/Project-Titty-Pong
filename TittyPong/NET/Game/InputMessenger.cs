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
        private ConcurrentQueue<InputState> States;
        private Thread SendThread;
        private bool IsRunning;
        private int SendsPerSecond;

        public InputMessenger(EventManager events)
        {
            Events = events;
            States = new ConcurrentQueue<InputState>();
            SendThread = new Thread(Run);
            SendsPerSecond = 20;

            Events.InputEvent += HandleInputEvent;
        }

        private void HandleInputEvent(object sender, InputEventArgs e)
        {
            States.Enqueue(e.State);
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
                var numberToDequeue = States.Count;
                var inputsToSend = new List<InputState>();
                while (numberToDequeue-- > 0)
                    if (States.TryDequeue(out var e))
                        inputsToSend.Add(e);
                Events.OnSendInputEvent(this, new InputSendEventArgs { States = inputsToSend });
                Thread.Sleep(1000 / SendsPerSecond);
            }   
        }

        public void Stop()
        {
            IsRunning = false;
            SendThread.Join();
        }
    }
}
