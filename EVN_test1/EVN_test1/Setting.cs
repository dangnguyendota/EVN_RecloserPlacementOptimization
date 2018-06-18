using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVN_test1
{
    public class Setting
    {
        public const double maxDistance = 500;//do dai toi da giua 2 co dien 500 mét
        public const int Ce = 1000; // gia dien nang 1000/ 1 kWh
        public const double Tr = (double)1 / 30; //  1/30 h thoi gian giua 2 lan ngat
        public const int Cr = 10000000; // chi phi 1 lan cat Recloser 100 trieu
        public const int Cs = 1000000; // chi phi 1 lan cat dao tu dong 10 trieu
        public const double vanToc = 5000; // toc do di chuyen 5000 m/h
        public const double tieuHao = 100; //cong suat tieu thu 100kWh/m
        public const double Pr = 210000000;//gia 1 recloser 180 - 210 trieu
        public const double Ps = 120000000;// gia 1 dao tu dong 100 - 120 trieu
        public const double Pf = 12000000; // gia 1 den bao 10 - 12 trieu
    }
}
