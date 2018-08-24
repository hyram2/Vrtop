
using System;

namespace StatisticSystem
{
    public class Statistic
    {
        private Guid _id;
        public Guid Id {
            get
            {
                if (_id == Guid.Empty)
                    _id = Guid.NewGuid();
                return _id;
            }
            set { _id = value; }
        }
      
        public Type MyType;
    }
}