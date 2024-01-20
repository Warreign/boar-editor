using BoarEngine.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomControlLibrary.Misc
{
    public class Transform : ViewModelBase
    {
        private double _x;
        private double _y;
        private double _z;

        public double X
        {
            get { return _x; }
            set { SetProperty(ref _x, value); }
        }

        public double Y
        {
            get { return _y; }
            set { SetProperty(ref _y, value); }
        }
        public double Z
        {
            get { return _z; }
            set { SetProperty(ref _z, value); }
        }

        public Transform(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public Transform()
        {
            X = 0; Y = 0; Z = 0;
        }
    }
}
