using Common;
using Common.Maths;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Guid RoomId;
        private bool IsRunning;
        private int SendsPerSecond;

        public InputMessenger(EventManager events)
        {
            Events = events;
            States = new ConcurrentQueue<InputState>();
            SendThread = new Thread(Run);
            SendsPerSecond = 10;

            Events.InputEvent += HandleInputEvent;
        }

        private void HandleInputEvent(object sender, InputEventArgs e)
        {
            States.Enqueue(e.State);
            RoomId = e.RoomId;
        }

        public void Start()
        {
            IsRunning = true;
            SendThread.Start();
        }

        public void Run()
        {
            var timer = new Stopwatch();
            while(IsRunning)
            {
                timer.Restart();
                var numberToDequeue = States.Count;
                var inputsToSend = new List<InputState>();
                while (numberToDequeue-- > 0)
                    if (States.TryDequeue(out var e))
                        inputsToSend.Add(e);
                if(inputsToSend.Count > 0)
                    Events.OnSendInputEvent(this, new InputSendEventArgs { States = inputsToSend, RoomId = this.RoomId});
                timer.Stop();
                Thread.Sleep((int)TittyMaths.Max(0, (1000 / SendsPerSecond) - (int)timer.ElapsedMilliseconds));
            }   
        }

        public void Stop()
        {
            IsRunning = false;
            SendThread.Join();
        }
    }
}
