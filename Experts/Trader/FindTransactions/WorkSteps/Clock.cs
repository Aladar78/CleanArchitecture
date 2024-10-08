﻿using Experts.Trader.FindTransactions.UserStory.OutputPort;

namespace Experts.Trader.FindTransactions.WorkSteps;

public class Clock(Clock.IClient client) : IClock {
    public DateTime GetTime() => client.Now;

    public interface IClient { DateTime Now { get; } }

    public class Client : IClient {
        public DateTime Now => DateTime.Now;
    }
}
