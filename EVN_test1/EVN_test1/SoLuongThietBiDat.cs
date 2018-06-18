using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVN_test1
{
    class SoLuongThietBiDat
    {
        private int maxMC;
        private int maxDTD;
        private int maxDB;
        private int currenMC = 0;
        private int currentDTD = 0;
        private int currentDB = 0;
        public SoLuongThietBiDat(double chiPhiDauTu, List<SearchPosition> set)
        {
            maxMC = Convert.ToInt32(chiPhiDauTu / Setting.Pr);
            maxDTD = Convert.ToInt32(chiPhiDauTu / Setting.Ps);
            maxDB = Convert.ToInt32(chiPhiDauTu / Setting.Pf);
            foreach (SearchPosition s in set)
            {
                s.reset();
            }
            int soMC = 0;
            int soDTD = 0;
            int soDB = 0;
            double F_tong = Double.MaxValue;
            while (soMC > 0)
            {
                List<double> temp = new List<double>();
                List<int> viTriDatTrongCay = new List<int>();
                List<int> viTriCay = new List<int>();
                for (int j = 0; j < set.Count; j++)
                {
                    SearchPosition a = set[j];
                    double tong = 0;
                    for (int k = 0; k < set.Count; k++)
                    {
                        if (k != j)
                        {
                            tong += set[k].sumF1();
                        }
                    }
                    for (int i = 0; i < a.numNode; i++)
                    {
                        if (!a.getMC(i))
                        {
                            if (a.l[i] != 0 || a.d[i] != 0 || a.w[i] != 0)
                            {
                                a.setMC(i);
                                temp.Add(a.sumF1() + tong);
                                viTriCay.Add(j);
                                viTriDatTrongCay.Add(i);
                                a.refresh();
                            }
                        }
                    }
                }
                int vitri = findMinPos(temp);
                if (vitri == -1)
                {
                    break;
                }
                if (temp[vitri] > F_tong)
                {
                    break;
                }
                else
                {
                    F_tong = temp[vitri];
                }
                set[viTriCay[vitri]].setLastMC(viTriDatTrongCay[vitri]);
                set[viTriCay[vitri]].refresh();
                soMC -= 1;

            }
            while (soDTD > 0)
            {
                List<double> temp = new List<double>();
                List<int> viTriDatTrongCay = new List<int>();
                List<int> viTriCay = new List<int>();
                for (int j = 0; j < set.Count; j++)
                {
                    SearchPosition a = set[j];
                    double tong = 0;
                    for (int k = 0; k < set.Count; k++)
                    {
                        if (k != j)
                        {
                            tong += set[k].sumF1();
                        }
                    }
                    for (int i = 0; i < a.numNode; i++)
                    {
                        if (!a.getMC(i) && !a.getDTD(i) && a.currentM(i) < 4)
                        {
                            if (a.l[i] != 0 || a.d[i] != 0 || a.w[i] != 0)
                            {
                                a.setDTD(i);
                                temp.Add(a.sumF1() + tong);
                                viTriCay.Add(j);
                                viTriDatTrongCay.Add(i);
                                a.refresh();
                            }
                        }
                    }
                }
                int vitri = findMinPos(temp);
                if (vitri == -1)
                {
                    break;
                }
                if (temp[vitri] > F_tong)
                {
                    break;
                }
                else
                {
                    F_tong = temp[vitri];
                }
                set[viTriCay[vitri]].setLastDTD(viTriDatTrongCay[vitri]);
                set[viTriCay[vitri]].refresh();
                soDTD -= 1;

            }
            while (soDB > 0)
            {
                List<double> temp = new List<double>();
                List<int> viTriDatTrongCay = new List<int>();
                List<int> viTriCay = new List<int>();
                for (int j = 0; j < set.Count; j++)
                {
                    SearchPosition a = set[j];
                    double tong = 0;
                    for (int k = 0; k < set.Count; k++)
                    {
                        if (k != j)
                        {
                            tong += set[k].sumF2();
                        }
                    }
                    for (int i = 0; i < a.numNode; i++)
                    {
                        if (a.l[i] != 0 || a.d[i] != 0 || a.w[i] != 0)
                        {
                            if (!a.getMC(i) && !a.getDTD(i) && !a.getDB(i))
                            {

                                a.setDB(i);
                                temp.Add(a.sumF2() + tong);
                                viTriCay.Add(j);
                                viTriDatTrongCay.Add(i);
                                a.refresh();
                            }
                        }
                    }
                }
                int vitri = findMinPos(temp);
                if (vitri == -1)
                {
                    break;
                }
                if (temp[vitri] > F_tong)
                {
                    break;
                }
                else
                {
                    F_tong = temp[vitri];
                }
                set[viTriCay[vitri]].setLastDB(viTriDatTrongCay[vitri]);
                set[viTriCay[vitri]].refresh();
                soDB -= 1;
            }
            this.current_F = F_tong;

        }
    }
}
