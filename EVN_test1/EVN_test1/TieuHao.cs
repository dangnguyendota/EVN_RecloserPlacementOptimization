using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVN_test1
{
    public class ChiPhi
    {
        private int numMC = 0;
        private int numDTD = 0;
        private int numDB = 0;
        private double F = 0.0;
        private List<int[]> vi_triMC ;
        private List<int[]> vi_triDTD ;
        private List<int[]> vi_triDB ;
        public ChiPhi(int numMC, int numDTD, int numDB, double F, List<int[]> vi_triMC, List<int[]> vi_triDTD, List<int[]> vi_triDB)
        {
            this.numMC = numMC;
            this.numDTD = numDTD;
            this.numDB = numDB;
            this.F = F;
            this.vi_triMC = vi_triMC;
            this.vi_triDTD = vi_triDTD;
            this.vi_triDB = vi_triDB;
        }
        public double getF()
        {
            return this.F;
        }
        public int[] getSL()
        {
            return new int[3] { numMC, numDTD, numDB }; 
        }
        public int getSL_MC()
        {
            return this.numMC;
        }
        public int getSL_DTD()
        {
            return this.numDTD;
        }
        public int getSL_DB()
        {
            return this.numDB;
        }
        //public void setMC_positions(List<int[]> vitri)
        //{
        //    vi_triMC = vitri;
        //}
        //public void setDTD_position(List<int[]> vitri)
        //{
        //    vi_triDTD = vitri;
        //}
        //public void setDB_postion(List<int[]> vitri)
        //{
        //    vi_triDB = vitri;
        //}
        public List<int[]> getMC_position()
        {
            return this.vi_triMC;
        }
        public List<int[]> getDTD_position()
        {
            return this.vi_triDTD;
        }
        public List<int[]> getDB_position()
        {
            return this.vi_triDB;
        }

    }
    public class TieuHao
    {
        private List<ChiPhi> chi_phi = new List<ChiPhi>();
        public void Add(int numMC, int numDTD, int numDB, double F, List<int[]> vi_triMC, List<int[]> vi_triDTD, List<int[]> vi_triDB)
        {
            foreach(ChiPhi i in chi_phi)
            {
                if(i.getSL_MC() == numMC && i.getSL_DTD() == numDTD && i.getSL_DB() == numDB)
                {
                    return;
                }
            }
            chi_phi.Add(new ChiPhi(numMC, numDTD, numDB, F, vi_triMC, vi_triDTD, vi_triDB));
        }
        public double getF(int numMC, int numDTD, int numDB)
        {
            foreach (ChiPhi i in chi_phi)
            {
                if (i.getSL_MC() == numMC && i.getSL_DTD() == numDTD && i.getSL_DB() == numDB)
                {
                    return i.getF();
                }
            }
            return 0.0;
        }
        public List<int[]> getMC_position(int numMC, int numDTD, int numDB)
        {
            foreach (ChiPhi i in chi_phi)
            {
                if (i.getSL_MC() == numMC && i.getSL_DTD() == numDTD && i.getSL_DB() == numDB)
                {
                    return i.getMC_position();
                }
            }
            return new List<int[]>();
        }
        public List<int[]> getDTD_position(int numMC, int numDTD, int numDB)
        {
            foreach (ChiPhi i in chi_phi)
            {
                if (i.getSL_MC() == numMC && i.getSL_DTD() == numDTD && i.getSL_DB() == numDB)
                {
                    return i.getDTD_position();
                }
            }
            return null;
        }
        public List<int[]> getDB_position(int numMC, int numDTD, int numDB)
        {
            foreach (ChiPhi i in chi_phi)
            {
                if (i.getSL_MC() == numMC && i.getSL_DTD() == numDTD && i.getSL_DB() == numDB)
                {
                    return i.getDB_position();
                }
            }
            return null;
        }
        public int currentLength()
        {
            return this.chi_phi.Count;
        }
        public ChiPhi getAt(int index)
        {
            return this.chi_phi[index];
        }
    }
}
