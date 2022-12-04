using System.Collections.Generic;

namespace Phoebe.Neo.Base {
    public interface IObject
    {
        bool IsActive { get; }

        void Create(Dictionary<string, object> valueDic);

        void Hide();

        void Delete();
    }
}
