using System.Collections.Generic;

namespace Phoebe.Neo.Base {
    public interface IGameManager
    {
        ManagerStatus status { get; }

        void Startup();
    }

}
